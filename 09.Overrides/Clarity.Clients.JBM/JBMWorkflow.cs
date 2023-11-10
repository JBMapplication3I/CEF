// <copyright file="JBMWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the jbm workflow class</summary>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce;
    using Ecommerce.DataModel;
    using Ecommerce.Interfaces.Models;
    using Ecommerce.Models;
    using Ecommerce.Utilities;
    using MoreLinq;
    using Newtonsoft.Json;

    public partial class JBMWorkflow
    {
        public static async Task<CEFActionResponse> UpsertAccountsContactsAndUsersFromFusionAsync(
            List<CustomerAddress> addresses,
            List<CustomerAccountSiteInformation> sites,
            string? contextProfileName)
        {
            var existing = false;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var accountInfo = await context.Accounts
                .AsNoTracking()
                .FilterByCustomKey(addresses.Select(x => x.PartyId).First().ToString())
                .Select(x => new
                {
                    x.ID,
                    x.Name,
                })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(accountInfo.ID))
            {
                return CEFAR.FailingCEFAR("Could not locate account.");
            }
            var timeStamp = DateExtensions.GenDateTime;
            foreach (var addr in addresses!.AsEnumerable().DistinctBy(x => x.AddressId))
            {
                existing = false;
                try
                {
                    Contract.RequiresAllValidKeys(addr.State, addr.Country, addr.Address1, addr.AddressNumber, addr.AddressType);
                }
                catch (Exception)
                {
                    continue;
                }
                var accountContact = await context.AccountContacts
                    .FilterByCustomKey($"{accountInfo.ID}|{addr.AddressNumber}")
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                var associatedSite = sites.AsEnumerable().DistinctBy(x => x.AddressId)
                    .Where(x => x.AddressId == addr.AddressId.ToString())
                    .SingleOrDefault();
                var orderType = associatedSite!.ShipMethods.FirstOrDefault(x => Contract.CheckValidKey(x.OrderType))?.OrderType;
                if (Contract.CheckNotNull(associatedSite)
                    && associatedSite.SiteUses.Any(x => x.SiteUseCode is "BILL_TO")
                    && Contract.CheckValidKey(orderType))
                {
                    var account = await context.Accounts.FilterByCustomKey(addr.PartyId.ToString()).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (Contract.CheckNotNull(account))
                    {
                        SerializableAttributesDictionary? acctAttrs = null;
                        if (Contract.CheckValidKey(account.JsonAttributes))
                        {
                            acctAttrs = account!.JsonAttributes.DeserializeAttributesDictionary();
                        }
                        else
                        {
                            acctAttrs = new SerializableAttributesDictionary();
                        }
                        acctAttrs["orderType"] = new SerializableAttributeObject
                        {
                            Key = "orderType",
                            Value = orderType!,
                        };
                        account.JsonAttributes = acctAttrs.SerializeAttributesDictionary();
                        context.Accounts.Add(account);
                        context.Entry(account).State = EntityState.Modified;
                        await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                    }
                }
                int? associatedUserID = null;
                try
                {
                    associatedUserID = await context.Users
                        .Where(x => x.Contact!.FirstName == associatedSite!.ContactFirstName && x.Contact.LastName == associatedSite.ContactLastName)
                        .Select(x => x.ID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // Do Nothing
                }
                if (Contract.CheckNull(accountContact))
                {
                    accountContact = new AccountContact
                    {
                        Active = true,
                        CreatedDate = timeStamp,
                        CustomKey = $"{accountInfo.ID}|{addr.AddressNumber}",
                        IsBilling = addr.AddressType!.Contains("BILL_TO"),
                        IsPrimary = false,
                        MasterID = accountInfo.ID,
                        EndDate = Contract.CheckValidKey(associatedSite.EndDate)
                            ? DateTime.Parse(associatedSite.EndDate)
                            : null,
                        Slave = new Contact
                        {
                            Active = true,
                            CreatedDate = timeStamp,
                            CustomKey = addr.AddressId.ToString(),
                            FirstName = Contract.CheckNotNull(associatedSite) ? associatedSite.ContactFirstName : string.Empty,
                            LastName = Contract.CheckNotNull(associatedSite) ? associatedSite.ContactLastName : string.Empty,
                            FullName = Contract.CheckNotNull(associatedSite)
                                    ? $"{associatedSite.ContactFirstName} {associatedSite.ContactLastName}"
                                    : accountInfo.Name,
                            //TypeID = Contract.CheckNotNull(associatedSite)
                            //    && Contract.CheckNotNull(associatedSite?.SetCode)
                            //    && associatedSite!.SetCode!.Contains("ENTERPRISE")
                            //    ? 4
                            //    : 1,
                            TypeID = Contract.CheckNotNull(associatedSite)
                                && !addr.AddressType!.Contains("BILL_TO")
                                && Contract.CheckNotNull(associatedUserID)
                                && Contract.CheckAllValidKeys(associatedSite?.ContactFirstName, associatedSite?.ContactLastName)
                                ? 1
                                : 4,
                            Address = new Address
                            {
                                Active = true,
                                CreatedDate = timeStamp,
                                City = addr.City,
                                CountryID = 1,
                                CustomKey = addr.AddressNumber,
                                PostalCode = addr.PostalCode,
                                RegionID = await context.Regions
                                        .AsNoTracking()
                                        .FilterRegionsByCode(addr.State, false, false)
                                        .Select(x => x.ID)
                                        .SingleOrDefaultAsync()
                                        .ConfigureAwait(false),
                                Street1 = addr.Address1,
                                Street2 = addr.Address2,
                            },
                        },
                    };
                }
                else
                {
                    existing = true;
                    accountContact!.Active = true;
                    accountContact.UpdatedDate = timeStamp;
                    accountContact.CustomKey = $"{accountInfo.ID}|{addr.AddressNumber}";
                    accountContact.IsBilling = addr.AddressType!.Contains("BILL_TO");
                    accountContact.IsPrimary = false;
                    accountContact.EndDate = Contract.CheckValidKey(associatedSite.EndDate)
                        ? DateTime.Parse(associatedSite.EndDate)
                        : null;
                    accountContact.MasterID = accountInfo.ID;
                    accountContact.Slave!.Active = true;
                    accountContact.Slave.UpdatedDate = timeStamp;
                    accountContact.Slave.CustomKey = addr.AddressId.ToString();
                    accountContact.Slave.FirstName = Contract.CheckNotNull(associatedSite) ? associatedSite.ContactFirstName : string.Empty;
                    accountContact.Slave.LastName = Contract.CheckNotNull(associatedSite) ? associatedSite.ContactLastName : string.Empty;
                    accountContact.Slave.FullName = Contract.CheckNotNull(associatedSite)
                            ? $"{associatedSite.ContactFirstName} {associatedSite.ContactLastName}"
                            : accountInfo.Name;
                    //accountContact.Slave.TypeID = Contract.CheckNotNull(associatedSite)
                    //            && Contract.CheckNotNull(associatedSite?.SetCode)
                    //            && associatedSite!.SetCode!.Contains("ENTERPRISE")
                    //            ? 4
                    //            : 1;
                    accountContact.Slave.TypeID = Contract.CheckNotNull(associatedSite)
                                && !addr.AddressType!.Contains("BILL_TO")
                                && Contract.CheckNotNull(associatedUserID)
                                && Contract.CheckAllValidKeys(associatedSite?.ContactFirstName, associatedSite?.ContactLastName)
                                ? 1
                                : 4;
                    accountContact.Slave.Address!.Active = true;
                    accountContact.Slave.Address.UpdatedDate = timeStamp;
                    accountContact.Slave.Address.City = addr.City;
                    accountContact.Slave.Address.CountryID = 1;
                    accountContact.Slave.Address.CustomKey = addr.AddressNumber;
                    accountContact.Slave.Address.PostalCode = addr.PostalCode;
                    accountContact.Slave.Address.RegionID = await context.Regions
                            .FilterRegionsByCode(addr.State, false, false)
                            .Select(x => x.ID)
                            .SingleOrDefaultAsync()
                            .ConfigureAwait(false);
                    accountContact.Slave.Address.Street1 = addr.Address1;
                    accountContact.Slave.Address.Street2 = addr.Address2;
                }
                var sad = new SerializableAttributesDictionary();
                var accountContactAttrDict = new SerializableAttributesDictionary();
                if (Contract.CheckNotNull(associatedSite))
                {
                    if (Contract.CheckNotEmpty(associatedSite?.ShipMethods)
                        && associatedSite!.ShipMethods.Any(x => !string.IsNullOrWhiteSpace(x.DefaultShipMethod)))
                    {
                        sad["defaultShipMethod"] = new SerializableAttributeObject
                        {
                            Key = "defaultShipMethod",
                            Value = associatedSite!.ShipMethods.Where(x => !string.IsNullOrWhiteSpace(x.DefaultShipMethod)).First().DefaultShipMethod!,
                        };
                    }
                    if (Contract.CheckValidID(associatedUserID))
                    {
                        sad["userId"] = new SerializableAttributeObject
                        {
                            Key = "userId",
                            Value = associatedUserID.ToString(),
                        };

                    }
                }
                sad["billToSiteUseId"] = new SerializableAttributeObject
                {
                    Key = "billToSiteUseId",
                    Value = sites
                        .Where(x => x.Type != "N")
                        .FirstOrDefault()?.SiteUses
                        ?.Where(y => y?.SiteUseCode == "BILL_TO")
                        .Select(z => z.SiteUseId)
                        .SingleOrDefault() ?? string.Empty,
                };
                sad["addressId"] = new SerializableAttributeObject
                {
                    Key = "addressId",
                    Value = addr.AddressId.ToString(),
                };
                accountContact.Slave!.JsonAttributes = sad.SerializeAttributesDictionary();
                if (addr.AddressType!.Contains("BILL_TO"))
                {
                    accountContactAttrDict["isBilling"] = new SerializableAttributeObject
                    {
                        Key = "isBilling",
                        Value = "true",
                    };
                }
                accountContact.JsonAttributes = accountContactAttrDict.SerializeAttributesDictionary();
                if (existing)
                {
                    try
                    {
                        context.AccountContacts.Add(accountContact);
                        context.Entry(accountContact).State = EntityState.Modified;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                else
                {
                    context.AccountContacts.Add(accountContact);
                }
            }
            try
            {
                var res = await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                return CEFAR.PassingCEFAR();
            }
            catch (Exception)
            {
                return CEFAR.FailingCEFAR("Could not save the contact information.");
            }
        }

        public static IProductModel AddOrUpdatePrimaryUnitOfMeasure(IProductModel product, string uomCode)
        {
            SerializableAttributeObject availableUOMs;
            SerializableAttributeObject uomAttr;
            product!.SerializableAttributes.TryRemove("AvailableUOMS", out _);
            if (product!.SerializableAttributes.TryGetValue("AvailableUOMs", out var available))
            {
                available.Value = !available.Value.Contains(uomCode)
                    ? available.Value += $",{uomCode}"
                    : available.Value;
                product.SerializableAttributes["AvailableUOMs"] = available;
            }
            else
            {
                availableUOMs = new SerializableAttributeObject
                {
                    Key = "AvailableUOMs",
                    Value = uomCode!,
                };
                product.SerializableAttributes.TryAdd(availableUOMs.Key, availableUOMs);
            }
            uomAttr = new SerializableAttributeObject();
            uomAttr.Key = uomCode!;
            uomAttr.Value = "1";
            uomAttr.UofM = product.UnitOfMeasure ?? string.Empty;
            product.SerializableAttributes[uomAttr.Key] = uomAttr;
            product!.UnitOfMeasure = uomCode;
            return product;
        }

        public static IProductModel AddOrUpdateAvailableUnitsOfMeasure(IProductModel product, string uomCode, string conversion, string? name)
        {
            SerializableAttributeObject availableUOMs;
            SerializableAttributeObject uomAttr;
            if (product!.SerializableAttributes.TryGetValue("AvailableUOMs", out var available))
            {
                available.Value = !available.Value.Contains(uomCode)
                    ? available.Value += $",{uomCode}"
                    : available.Value;
                product.SerializableAttributes["AvailableUOMs"] = available;
            }
            else
            {
                availableUOMs = new SerializableAttributeObject
                {
                    Key = "AvailableUOMs",
                    Value = uomCode,
                };
                product.SerializableAttributes.TryAdd(availableUOMs.Key, availableUOMs);
            }
            if (product.SerializableAttributes.TryGetValue(uomCode, out var code))
            {
                uomAttr = code;
                uomAttr.Value = conversion;
                uomAttr.UofM = name ?? string.Empty;
                product.SerializableAttributes[uomAttr.Key] = uomAttr;
            }
            else
            {
                uomAttr = new SerializableAttributeObject
                {
                    Key = uomCode,
                    Value = conversion,
                    UofM = name ?? string.Empty,
                };
                product.SerializableAttributes[uomAttr.Key] = uomAttr;
            }
            return product;
        }

        public static async Task<int?> CheckAccountExistsAndReturnIDAsync(string accountKey, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Accounts
                .AsNoTracking()
                .FilterByCustomKey(accountKey)
                .Select(x => x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public static async Task<CEFActionResponse> EnsureAccountUserRoleAsync(int accountID, string priceListName, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var existing = await context.Roles.SingleOrDefaultAsync(x => x.Name == priceListName).ConfigureAwait(false);
            if (Contract.CheckNull(existing))
            {
                context.Roles.Add(
                    new UserRole
                    {
                        Name = priceListName,
                    });
                try
                {
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                }
                catch
                {
                    return CEFAR.FailingCEFAR();
                }
            }
            var role = await context.Roles.SingleOrDefaultAsync(x => x.Name == priceListName).ConfigureAwait(false);
            var acctRole = await context.AccountUserRoles.FirstOrDefaultAsync(x => x.SlaveID == role.Id).ConfigureAwait(false);
            if (Contract.CheckNull(acctRole))
            {
                var acctUserRole = new AccountUserRole
                {
                    CustomKey = $"{accountID}|{role.Id}",
                    Active = true,
                    CreatedDate = DateExtensions.GenDateTime,
                    MasterID = accountID,
                    SlaveID = role.Id,
                    StartDate = DateExtensions.GenDateTime,
                };
                context.AccountUserRoles.Add(acctUserRole);
                try
                {
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                }
                catch
                {
                    return CEFAR.FailingCEFAR();
                }
            }
            return CEFAR.PassingCEFAR();
        }

        public static async Task<CEFActionResponse> AssociateProductsToAccountUserRoleAsync(string priceListName, List<string> priceListItems, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var productsDigest = await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .Select(x => new DigestModel
                {
                    Hash = default,
                    ID = x.ID,
                    Key = x.CustomKey,
                })
                .ToListAsync()
                .ConfigureAwait(false);
            foreach (var p in priceListItems.Distinct())
            {
                if (!productsDigest.Exists(x => x.Key == p))
                {
                    continue;
                }
                var product = await context.Products.FilterByCustomKey(p).FirstOrDefaultAsync().ConfigureAwait(false);
                if (!Contract.CheckNotNull(product))
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(product!.RequiresRoles))
                {
                    product.RequiresRoles = priceListName;
                }
                else if (!product!.RequiresRoles!.Contains(priceListName))
                {
                    product.RequiresRoles += $",{priceListName}";
                }
                //await Workflows.Products.UpdateAsync(product, context.ContextProfileName).ConfigureAwait(false);
                context.Products.Add(product);
                context.Entry(product).State = EntityState.Modified;
                try
                {
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                }
                catch
                {
                    // Do nothing.
                }
            }
            return CEFAR.PassingCEFAR();
        }

        public static async Task<ISalesInvoiceModel?> MapToCEFInvoiceAsync(FusionInvoiceHeader invoice, InvoiceLinesResponse invoiceLines)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            ISalesInvoiceModel cefInvoice = new SalesInvoiceModel();
            var account = await context.Accounts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByCustomKey(invoice.BillToPartyId.ToString(), true)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var billingContacts = await context.AccountContacts.FilterAccountContactsByIsBilling(true).Select(x => x.Slave).ToListAsync().ConfigureAwait(false);
            //var billingContacts = Contract.CheckValidID(billingID)
            //    ? await context.Contacts
            //        .AsNoTracking()
            //        .FilterByActive(true)
            //        .FilterContactsByAddressID(billingID, false)
            //        .ToListAsync()
            //        .ConfigureAwait(false)
            //    : null;
            var shippingID = await context.Addresses.FilterByCustomKey(invoice.ShipToSite).Select(x => x.ID).FirstOrDefaultAsync();
            var shippingContacts = Contract.CheckValidID(shippingID)
                ? await context.Contacts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterContactsByAddressID(shippingID, false)
                    .ToListAsync()
                    .ConfigureAwait(false)
                : null;
            Contact? shipping = shippingContacts.FirstOrDefault();
            Contact? billing = billingContacts?.Count() == 1 ? billingContacts.First() : shipping;
            if (!Contract.CheckAllValidIDs(account?.ID))
            {
                return null;
            }
            cefInvoice.AccountID = account!.ID;
            cefInvoice.Active = true;
            cefInvoice.CustomKey = invoice.TransactionNumber;
            cefInvoice.DueDate = invoice.DueDate;
            cefInvoice.Notes = !string.IsNullOrWhiteSpace(invoice.SpecialInstructions) ? new List<INoteModel>() { new NoteModel() { Note1 = invoice.SpecialInstructions }, } : null;
            cefInvoice.BalanceDue = invoice.InvoiceBalanceAmount;
            cefInvoice.BillingContactID = billing?.ID;
            cefInvoice.ShippingContactID = shipping?.ID;
            cefInvoice.StateID = invoice.InvoiceBalanceAmount.HasValue && invoice.InvoiceBalanceAmount.Value > 0m ? 1 : 2;
            cefInvoice.TypeID = 1;
            if (invoice.InvoiceBalanceAmount == 0m)
            {
                cefInvoice.StatusID = 3;
            }
            if (invoice.InvoiceBalanceAmount > 0m && invoice.InvoiceBalanceAmount < invoice.EnteredAmount)
            {
                cefInvoice.StatusID = 2;
            }
            else
            {
                cefInvoice.StatusID = 1;
            }
            cefInvoice.Totals.SubTotal = invoice.EnteredAmount!.Value;
            var linesAndOrderNumber = MapInvoiceLinesToCEFInvoice(invoiceLines, cefInvoice, invoice);
            cefInvoice.SalesItems = linesAndOrderNumber.SaleItems;
            var salesOrderID = await context.SalesOrders
                .AsNoTracking()
                .FilterByCustomKey(linesAndOrderNumber.SalesOrderKey, true)
                .Select(x => x.SalesGroupAsMasterID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            cefInvoice.SalesGroupID = salesOrderID;
            return cefInvoice;
        }

        public static (List<ISalesItemBaseModel<IAppliedSalesInvoiceItemDiscountModel>> SaleItems, string? SalesOrderKey) MapInvoiceLinesToCEFInvoice(
            InvoiceLinesResponse invoiceLines,
            ISalesInvoiceModel cefInvoice,
            FusionInvoiceHeader header)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            var orderNumber = invoiceLines!.Items.Select(x => x.SalesOrder).FirstOrDefault();
            return (invoiceLines!.Items.Select(x => new SalesItemBaseModel<IAppliedSalesInvoiceItemDiscountModel, AppliedSalesInvoiceItemDiscountModel>
            {
                Active = true,
                CustomKey = $"{header.CustomerTransactionId}|{x.LineNumber}",
                Name = x.Description,
                MasterKey = cefInvoice.CustomKey,
                Quantity = x.Quantity!.Value,
                Sku = x.ItemNumber,
                UnitCorePrice = x.UnitSellingPrice!.Value,
                UnitSoldPrice = x.UnitSellingPrice!.Value,
            }
            as ISalesItemBaseModel<IAppliedSalesInvoiceItemDiscountModel>).ToList(), orderNumber);
        }

        public static async Task<CEFActionResponse<SalesOrder?>?> CompareAndUpdateOrder(FusionSalesOrder fusionOrder, string orderKey, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cefOrder = await context.SalesOrders
                .FilterByCustomKey(orderKey, true)
                .Include("SalesItems")
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckNull(cefOrder))
            {
                return null;
            }
            try
            {
                cefOrder = await AddOrUpdateOrderLines(fusionOrder, cefOrder, contextProfileName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR<SalesOrder?>(ex.Message, ex?.StackTrace ?? "No StackTrace.");
            }
            cefOrder.UpdatedDate = DateExtensions.GenDateTime;
            cefOrder.SubtotalDiscounts = 0m;
            cefOrder.SubtotalFees = 0m;
            cefOrder.SubtotalHandling = 0m;
            cefOrder.SubtotalShipping = (decimal)(fusionOrder.Totals?.Items?.SingleOrDefault(x => x.TotalCode == "QP_TOTAL_SHIP_CHARGE")?.TotalAmount ?? 0.00);
            cefOrder.SubtotalTaxes = (decimal)(fusionOrder.Totals?.Items?.SingleOrDefault(x => x.TotalCode == "QP_TOTAL_TAX")?.TotalAmount ?? 0.00);
            cefOrder.SubtotalItems = (decimal)(fusionOrder.Totals?.Items?.SingleOrDefault(x => x.TotalCode == "QP_TOTAL_NET_PRICE")?.TotalAmount ?? 0.00);
            cefOrder.Total = (decimal)(fusionOrder.Totals?.Items?.SingleOrDefault(x => x.TotalCode == "QP_TOTAL_PAY_NOW")?.TotalAmount ?? 0.00);
            cefOrder.BalanceDue = cefOrder.Total;
            try
            {
                context.SalesOrders.AddOrUpdate(cefOrder);
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                return CEFAR.PassingCEFAR(cefOrder);
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR<SalesOrder?>(ex.Message, ex?.StackTrace ?? "No StackTrace.");
            }
        }

        private static async Task<SalesOrder> AddOrUpdateOrderLines(FusionSalesOrder fusionOrder, SalesOrder cefOrder, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var orderLines = await context.SalesOrderItems.FilterSalesOrderItemsBySalesOrderID(cefOrder.ID).ToListAsync().ConfigureAwait(false);
            var lineMappings = JsonConvert.DeserializeObject<Dictionary<string, string>>(JBMConfig.OrderLineStatusMappings!);
            foreach (var oldLine in orderLines)
            {
                context.SalesOrderItems.Remove(oldLine);
                context.Entry(oldLine).State = EntityState.Deleted;
            }
            foreach (var line in fusionOrder.Lines!)
            {
                var saleItem = new SalesOrderItem()
                {
                    Active = true,
                    CustomKey = $"{cefOrder.CustomKey}|{line.ProductNumber}",
                    CreatedDate = DateExtensions.GenDateTime,
                    Description = line.ProductNumber,
                    Sku = line.ProductNumber,
                    UnitCorePrice = line.UnitListPrice ?? 0m,
                    UnitSoldPrice = line.UnitSellingPrice!.Value,
                    UnitOfMeasure = line.OrderedUOMCode,
                    UserID = cefOrder.UserID,
                    MasterID = cefOrder.ID,
                    Quantity = line.OrderedQuantity!.Value,
                    Status = lineMappings!.TryGetValue(line.Status!, out var status) ? status : null,
                };
                var productID = await context.Products
                        .AsNoTracking()
                        .FilterByCustomKey(line.ProductNumber, true)
                        .Select(x => x.ID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                saleItem.ProductID = Contract.CheckValidID(productID) ? productID : null;
                context.SalesOrderItems.Add(saleItem);
            }
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            return await context.SalesOrders
                .FilterByCustomKey(cefOrder.CustomKey)
                .Include("SalesItems")
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}