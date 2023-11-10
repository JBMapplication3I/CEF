// <copyright file="JBMTargetOrderCheckoutExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target order checkout extensions class</summary>
namespace Clarity.Ecommerce.Providers.Checkouts.TargetOrder
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Clients.JBM;
    using Clients.JBM.JBMModels;
    using ComponentSpace.SAML2.Assertions;
    using DataModel;
    using Interfaces.Models;
    using Models;
    using Newtonsoft.Json;
    using Org.BouncyCastle.Bcpg;
    using RestSharp;
    using RestSharp.Authenticators;
    using RestSharp.Extensions;
    using Utilities;

    public partial class JBMTargetOrderCheckoutProvider : TargetOrderCheckoutProvider
    {
        private RestClient? Client { get; set; }

        public async Task<string?> BuildFusionSalesOrderAndSendAsync(
            ISalesGroupModel salesGroup,
            string? contextProfileName,
            int currentUserId,
            int? currentAccountID = null)
        {
            // hook from target order checkout
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            Account? originalOrderAcct = null;
            if (currentAccountID.HasValue)
            {
                originalOrderAcct = (await context.Accounts
                        .FilterByID(currentAccountID!.Value)
                        .Select(x => new
                        {
                            JsonAttributes = x.JsonAttributes ?? string.Empty,
                            CustomKey = x.CustomKey ?? string.Empty,
                            x.ID,
                            x.Name,
                        })
                        .ToListAsync()
                    .ConfigureAwait(false))
                    .Select(x => new Account
                    {
                        JsonAttributes = x.JsonAttributes,
                        CustomKey = x.CustomKey,
                        ID = x.ID,
                        Name = x.Name,
                    })
                    .FirstOrDefault();
                salesGroup.AccountID = originalOrderAcct.ID;
                salesGroup.AccountName = originalOrderAcct.Name;
                salesGroup.AccountKey = originalOrderAcct.CustomKey;
                salesGroup.Account ??= new AccountModel();
                salesGroup.Account.SerializableAttributes = originalOrderAcct.JsonAttributes.DeserializeAttributesDictionary();
            }
            await VerifyShippingExistsInFusionOrCreate(salesGroup, currentUserId, contextProfileName).ConfigureAwait(false);
            var fusionOrder = await MapCEFSalesOrderToFusionSalesOrder(salesGroup, currentUserId, contextProfileName);
            if (fusionOrder is null)
            {
                return $"ERROR! Unable to transform order# {salesGroup.SubSalesOrders!.First().ID} into a Fusion salses order.";
            }
            var result = await SendOrderToFusion(fusionOrder, context.ContextProfileName);
            if (Contract.CheckAnyValidKey(result.errorMessage, result.payload))
            {
                try
                {
                    await SendOrderErrorMessage(salesGroup, result.errorMessage, result.payload);
                    return result.errorMessage;
                }
                catch (Exception)
                {
                    try
                    {
                        await Logger.LogErrorAsync(
                            "Failed Fusion Order",
                            $"Failed to push order {salesGroup.SalesOrderMasters.First().CustomKey} to Fusion",
                            context.ContextProfileName);
                        return result.errorMessage;
                    }
                    catch
                    {
                        return result.errorMessage;
                    }
                }
            }
            return null;
        }

        private async Task<(string? errorMessage, string? payload)> SendOrderToFusion(FusionSalesOrder order, string? contextProfileName)
        {
            if (Client is null)
            {
                Client = GenerateClient();
            }
            var serializer = new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore };
            using var writer = new StringWriter();
            serializer.Serialize(writer, order);
            var payload = writer.ToString();
            if (payload is null)
            {
                return ($"ERROR! Could not serialize Fusion sales order# {order?.SourceTransactionNumber}", null);
            }
            var resp = await Client.ExecuteAsync(new RestRequest
            {
                Method = Method.POST,
                Resource = $"{JBMConfig.JBMSalesAPI}{JBMConfig.JBMFusionSalesOrderURLExtension}",
            }.AddJsonBody(payload, "application/json"));
            writer.Flush();
            if (!resp.IsSuccessful)
            {
                await Logger.LogErrorAsync(
                    "Failed Fusion Order",
                    $"Failed to push order {order.OrderNumber} to Fusion",
                    false,
                    new Exception(resp.Content),
                    payload,
                    contextProfileName);
            }
            return !resp.IsSuccessful ? (resp.Content, payload) : (null, null);
        }

        private async Task<FusionSalesOrder?> MapCEFSalesOrderToFusionSalesOrder(ISalesGroupModel salesGroup, int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            List<JBMCustomShippingMethod>? shippingMethods = JsonConvert.DeserializeObject<List<JBMCustomShippingMethod>>(JBMConfig.JBMCustomShippingMethods!);
            var shippingMethodExists = Contract.CheckValidKey(salesGroup.SubSalesOrders.First().RateQuotes.FirstOrDefault().ShipCarrierMethodName);
            JBMCustomShippingMethod? shipMethod = null;
            if (shippingMethodExists)
            {
                shipMethod = shippingMethods.SingleOrDefault(x => x.ShippingMethodName == salesGroup.SubSalesOrders.First().RateQuotes.FirstOrDefault().ShipCarrierMethodName);
            }
            var paidByCreditCard = Contract.CheckValidKey(salesGroup.SalesOrderMasters.FirstOrDefault()?.PaymentTransactionID);
            DataModel.Payment? payment = null;
            DataModel.Wallet? wallet = null;
            if (paidByCreditCard)
            {
                payment = (await context.Payments
                        .FilterPaymentsByTransactionNumber(salesGroup.SalesOrderMasters.First().PaymentTransactionID)
                        .Select(x => new
                        {
                            x.Amount,
                            x.TransactionNumber,
                            x.ExpirationMonth,
                            x.ExpirationYear,
                            x.Last4CardDigits,
                        })
                        .ToListAsync()
                    .ConfigureAwait(false))
                    .Select(x => new DataModel.Payment
                    {
                        Amount = x.Amount,
                        TransactionNumber = x.TransactionNumber,
                        ExpirationMonth = x.ExpirationMonth,
                        ExpirationYear = x.ExpirationYear,
                        Last4CardDigits = x.Last4CardDigits,
                    })
                    .FirstOrDefault();
                wallet = await context.Wallets
                    .FilterWalletsByUserID(userID)
                    .FilterWalletsByMatchExpirationMonth(payment.ExpirationMonth, false)
                    .FilterWalletsByMatchExpirationYear(payment.ExpirationYear, false)
                    .FilterWalletsByCreditCardNumber(payment.Last4CardDigits, true, false)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            var order = await MapToFusionOrderHeaderAsync(salesGroup, shipMethod, paidByCreditCard, payment!, wallet!, contextProfileName).ConfigureAwait(false);
            order.Lines ??= new List<FusionSalesOrderLine>();
            var count = 0;
            if (salesGroup.SubSalesOrders.Count() > 1)
            {
                foreach (var subOrder in salesGroup.SubSalesOrders!)
                {
                    foreach (var line in subOrder.SalesItems!)
                    {
                        if (line.UnitOfMeasure is null)
                        {
                            return null;
                        }
                        shippingMethodExists = Contract.CheckValidKey(subOrder.RateQuotes.FirstOrDefault().ShipCarrierMethodName);
                        if (shippingMethodExists)
                        {
                            shipMethod = shippingMethods.SingleOrDefault(x => x.ShippingMethodName == subOrder.RateQuotes.FirstOrDefault().ShipCarrierMethodName);
                        }
                        order.Lines.Add(MapToFusionSalesOrderLine(
                            salesItem: line,
                            count: count,
                            includeShipping: true,
                            userID: userID,
                            paidByCreditCard,
                            payment: payment!,
                            wallet: wallet!,
                            accountKey: salesGroup.AccountKey,
                            siteId: subOrder.ShippingContact!.CustomKey,
                            shipMethod));
                        count++;
                    }
                }
                count = 0;
            }
            else
            {
                foreach (var line in salesGroup.SubSalesOrders.First().SalesItems!)
                {
                    shippingMethodExists = Contract.CheckValidKey(salesGroup.SubSalesOrders.First().RateQuotes.FirstOrDefault().ShipCarrierMethodName);
                    if (shippingMethodExists)
                    {
                        shipMethod = shippingMethods.SingleOrDefault(x => x.ShippingMethodName == salesGroup.SubSalesOrders.First().RateQuotes.FirstOrDefault().ShipCarrierMethodName);
                    }
                    if (line.UnitOfMeasure is null)
                    {
                        return null;
                    }
                    order.Lines.Add(MapToFusionSalesOrderLine(
                        line,
                        count,
                        true,
                        userID,
                        paidByCreditCard,
                        payment: payment!,
                        wallet: wallet!,
                        salesGroup.AccountKey,
                        salesGroup.SubSalesOrders.First().ShippingContact!.CustomKey,
                        shipMethod));
                    count++;
                }
                count = 0;
            }
            return order!;
        }

        private FusionSalesOrderLine MapToFusionSalesOrderLine(
            ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel> salesItem,
            int count,
            bool includeShipping,
            int userID,
            bool paidByCreditCard,
            DataModel.Payment payment,
            DataModel.Wallet wallet,
            string? accountKey = null,
            string? siteId = null,
            JBMCustomShippingMethod? shippingMethod = null)
        {
            var fusionOrderLine = new FusionSalesOrderLine();
            fusionOrderLine.OrderedQuantity = (int)salesItem.Quantity;
            fusionOrderLine.OrderedUOMCode = salesItem.UnitOfMeasure!;
            fusionOrderLine.ProductNumber = salesItem.Sku;
            fusionOrderLine.SourceScheduleNumber = $"{count + 1}";
            fusionOrderLine.SourceTransactionLineId = $"{count + 1}";
            fusionOrderLine.SourceTransactionLineNumber = $"{count + 1}";
            fusionOrderLine.SourceTransactionScheduleId = $"{count + 1}";
            fusionOrderLine.TransactionCategoryCode = "ORDER";
            fusionOrderLine.TransactionBusinessCategoryName = "Sales Transaction";
            fusionOrderLine.SubstitutionAllowedFlag = false;
            if (includeShipping && Contract.CheckAllValidKeys(accountKey, siteId))
            {
                fusionOrderLine.ShipToCustomer = new List<ShipToCustomer>
                {
                    new ShipToCustomer
                    {
                        PartyId = accountKey,
                        SiteId = siteId,
                    },
                };
                if (Contract.CheckNotNull(shippingMethod))
                {
                    fusionOrderLine.ShippingCarrierId = shippingMethod!.ShippingCarrierId;
                    fusionOrderLine.ShippingCarrier = shippingMethod!.ShippingCarrier;
                    fusionOrderLine.ShippingServiceLevel = shippingMethod!.ShippingServiceLevel;
                    fusionOrderLine.ShippingServiceLevelCode = shippingMethod!.ShippingServiceLevelCode;
                    fusionOrderLine.ShippingMode = shippingMethod!.ShippingMode;
                    fusionOrderLine.ShippingModeCode = shippingMethod!.ShippingModeCode;
                }
            }
            //if (paidByCreditCard)
            //{
            //    fusionOrderLine.payments = paidByCreditCard && Contract.CheckNotNull(payment)
            //    ? new List<Clients.JBM.Payment>
            //    {
            //        new Clients.JBM.Payment
            //        {
            //            OriginalSystemPaymentReference = payment!.TransactionNumber,
            //            Amount = payment.Amount,
            //            CardTokenNumber = wallet?.Token,
            //            CardExpirationDate = $"{payment.ExpirationYear}-{payment.ExpirationMonth!.Value.ToString().PadLeft(2, '0')}-01",
            //            CardIssuerCode = wallet?.CardType,
            //            CardFirstName = wallet?.CardHolderName?.Split(' ').FirstOrDefault(),
            //            CardLastName = wallet?.CardHolderName?.Split(' ').LastOrDefault(),
            //            MaskedCardNumber = $"XXXXXXXXXXXX{wallet?.CreditCardNumber}",
            //        },
            //    }
            //    : null;
            //}
            return fusionOrderLine;
        }

        private async Task<FusionSalesOrder> MapToFusionOrderHeaderAsync(ISalesGroupModel salesGroup, JBMCustomShippingMethod? shipMethod, bool? paidByCreditCard, DataModel.Payment payment, DataModel.Wallet wallet, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return new FusionSalesOrder
            {
                SourceTransactionId = salesGroup.SalesOrderMasters.First().ID.ToString(),
                SourceTransactionNumber = salesGroup.SalesOrderMasters.First().CustomKey,
                SourceTransactionRevisionNumber = 1,
                SourceTransactionSystem = "ECOM",
                TransactionalCurrencyCode = "USD",
                SubmittedFlag = false,
                CustomerPONumber = salesGroup.SalesOrderMasters.First().PurchaseOrderNumber,
                TransactionType = salesGroup.Account!.SerializableAttributes.TryGetValue("orderType", out var orderType)
                    && !string.IsNullOrWhiteSpace(orderType?.Value)
                        ? orderType!.Value
                        : (await context.Brands
                            .AsNoTracking()
                            .FilterByID(salesGroup.BrandID)
                            .Select(x => x.CustomKey)
                            .SingleOrDefaultAsync()
                            .ConfigureAwait(false))
                            ?.ToLower() == "medsurg"
                                ? JBMConfig.JBMMedsurgeOrderType
                                : JBMConfig.JBMEmsOrderType,
                BusinessUnitId = long.Parse(JBMConfig.JBMBusinessUnitId),
                PaymentTerms = salesGroup.Account!.SerializableAttributes.TryGetValue("paymentTerms", out var terms)
                    ? terms.Value
                    : null,
                RequestedShipDate = DateExtensions.GenDateTime,
                ShippingInstructions = salesGroup.Notes?.FirstOrDefault()?.Note1,
                RequestingBusinessUnitId = long.Parse(JBMConfig.JBMBusinessUnitId),
                BuyingPartyNumber = salesGroup.Account!.SerializableAttributes.TryGetValue("partyNumber", out var partyNumber)
                    ? partyNumber.Value
                    : null,
                FreezePriceFlag = false,
                FreezeShippingChargeFlag = false,
                FreezeTaxFlag = false,
                RequestedFulfillmentOrganizationName = "JBM",
                BillToCustomer = new List<BillToCustomer>
                {
                    new BillToCustomer()
                    {
                        CustomerAccountId = salesGroup.Account!.SerializableAttributes["customerAccountId"].Value,
                        SiteUseId = salesGroup.BillingContact!.SerializableAttributes.TryGetValue("billToSiteUseId", out var bill)
                            ? bill?.Value ?? string.Empty
                            : string.Empty,
                    },
                },
                ShipToCustomer = new List<ShipToCustomer>
                {
                    new ShipToCustomer
                    {
                        PartyId = salesGroup.AccountKey,
                        SiteId = salesGroup.SubSalesOrders.First().ShippingContact!.CustomKey,
                    },
                },
                Payments = paidByCreditCard!.Value && Contract.CheckNotNull(payment!)
                ? new List<Clients.JBM.Payment>
                {
                    new Clients.JBM.Payment
                    {
                        OriginalSystemPaymentReference = payment!.TransactionNumber,
                        Amount = payment.Amount,
                        CardTokenNumber = wallet?.Token,
                        CardExpirationDate = $"{payment.ExpirationYear}-{payment.ExpirationMonth!.Value.ToString().PadLeft(2, '0')}-01",
                        CardIssuerCode = wallet?.CardType,
                        CardFirstName = wallet?.CardHolderName?.Split(' ').FirstOrDefault(),
                        CardLastName = wallet?.CardHolderName?.Split(' ').LastOrDefault(),
                        MaskedCardNumber = $"XXXXXXXXXXXX{wallet?.CreditCardNumber}",
                    },
                }
                : null,
                ShippingCarrier = Contract.CheckNotNull(shipMethod)
                    ? shipMethod?.ShippingCarrier
                    : null,
                ShippingCarrierId = Contract.CheckNotNull(shipMethod)
                    ? shipMethod?.ShippingCarrierId
                    : null,
                ShippingServiceLevel = Contract.CheckNotNull(shipMethod)
                    ? shipMethod?.ShippingServiceLevel
                    : null,
                ShippingServiceLevelCode = Contract.CheckNotNull(shipMethod)
                    ? shipMethod?.ShippingServiceLevelCode
                    : null,
                ShippingMode = Contract.CheckNotNull(shipMethod)
                    ? shipMethod?.ShippingMode
                    : null,
                ShippingModeCode = Contract.CheckNotNull(shipMethod)
                    ? shipMethod?.ShippingModeCode
                    : null,
            };
        }

        private RestClient GenerateClient()
        {
            return new RestClient
            {
                Authenticator = new HttpBasicAuthenticator(JBMConfig.JBMFusionUsername, JBMConfig.JBMFusionPassword),
                BaseUrl = new(JBMConfig.JBMFusionBaseURL),
            };
        }

        private async Task SendOrderErrorMessage(ISalesGroupModel salesGroup, string? fusionError, string? orderPayload)
        {
            var mailClient = new SmtpClient
            {
                Port = 587,
                Host = "smtp.office365.com",
                EnableSsl = true,
                Credentials = new NetworkCredential("oci-noreply@jandbmedical.com", "$THLK8p4G*hwkgkV"),
            };

            var itemsAndQuantities = string.Join(string.Empty, salesGroup.SubSalesOrders
                .SelectMany(x => x.SalesItems)
                .Select(y => new { ItemNumber = y.Sku, Quantity = y.TotalQuantity, })
                .Select(z => $"{z.ItemNumber} : {z.Quantity}\n"));

            //var body = $"""
            //    Master Order: {salesGroup.SalesOrderMasters.FirstOrDefault().CustomKey}
            //    Sub Order(s): {string.Join(", ", salesGroup.SubSalesOrders.Select(x => x.CustomKey))}
            //    PO Number: {salesGroup.SalesOrderMasters.FirstOrDefault().PurchaseOrderNumber ?? "N/A"}
            //    Transaction ID: {salesGroup.SalesOrderMasters.FirstOrDefault().PaymentTransactionID ?? "N/A"}
            //    Items and Quantities (as Item Number : Quantity Ordered)
            //    {itemsAndQuantities}

            //    Fusion Error:
            //    {fusionError}

            //    Json Order Payload:
            //    {orderPayload}
            //    """;
            var body = string.Empty;
            var message = new MailMessage(
                "oci-noreply@jandbmedical.com",
                "e-commerceteam@jandbmedical.com",
                $"Order `{salesGroup.SalesOrderMasters.FirstOrDefault().CustomKey}` failed to push to Fusion",
                body);
            await mailClient.SendMailAsync(message);
        }
    }
}