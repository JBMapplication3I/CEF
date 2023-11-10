// <copyright file="PayPalCheckoutProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal checkout provider class</summary>
#if PAYPAL && NET5_0_OR_GREATER // PayPal package used doesn't have net5+ versions
namespace Clarity.Ecommerce.Providers.Checkouts.PayPalInt
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Emails;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Models;
    using PayPal.Exception;
    using PayPal.PayPalAPIInterfaceService;
    using PayPal.PayPalAPIInterfaceService.Model;

    /// <summary>A PayPal checkout provider.</summary>
    public class PayPalCheckoutProvider : CheckoutProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => PayPalCheckoutProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            CartByIDLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        // ReSharper disable once FunctionComplexityOverflow
        public override async Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            string? contextProfileName)
        {
            var (cartResponse, _) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!cartResponse.ActionSucceeded)
            {
                var retVal = new CheckoutResult();
                retVal.ErrorMessages.Add("ERROR! Cart was null");
                return retVal;
            }
            return CheckoutInner(checkout, cartResponse.Result!);
        }

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            string? contextProfileName)
        {
            var cart = await Workflows.Carts.AdminGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (cart is null)
            {
                var retVal = new CheckoutResult();
                retVal.ErrorMessages.Add("ERROR! Cart was null");
                return retVal;
            }
            return CheckoutInner(checkout, cart);
        }

        /// <summary>Executes the PayPal checkout return action operation.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="pricingFactoryContext">The account price point key.</param>
        /// <param name="checkoutWithProvider"> The checkout with provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICheckoutResult.</returns>
        public async Task<ICheckoutResult> CheckoutReturnAsync(
            ICheckoutModel checkout,
            ICartModel cart,
            IPricingFactoryContextModel pricingFactoryContext,
            Func<Task<ICheckoutResult>> checkoutWithProvider,
            string? contextProfileName)
        {
            var result = new CheckoutResult();
            var token = checkout.PayByPayPal!.PayPalToken; // Request.Params["token"];
            var payerID = checkout.PayByPayPal.PayerID; // checkout Request.Params["PayerID"];
            if (string.IsNullOrEmpty(token))
            {
                result.Succeeded = false;
                result.ErrorMessage = "Error! Token missing.";
                return result;
            }
            // If the payer ID is missing, it's not a big deal -- we'll get it in the GetExpressCheckoutDetails call.
            var getExpressCheckout = new GetExpressCheckoutDetailsReq
            {
                GetExpressCheckoutDetailsRequest = new()
                {
                    Token = token,
                    Version = ConfigurationManager.AppSettings["Clarity.Payment.PayPal.Version"],
                },
            };
            // PayPal configuration -- set mode to "live" to use with the live site
            var config = new Dictionary<string, string>();
            var mode = CEFConfigDictionary.PaymentsProviderMode;
            var testMode = mode == Enums.PaymentProviderMode.Testing;
            config["mode"] = testMode ? "sandbox" : "live";
            var api = new PayPalAPIInterfaceServiceService(config);
            // Set our API credentials
            var credentials = PayPalCheckoutProviderConfig.GetPayPalSignatureCredential();
            try
            {
                var response = api.GetExpressCheckoutDetails(getExpressCheckout, credentials);
                if (response.Ack == AckCodeType.SUCCESS || response.Ack == AckCodeType.SUCCESSWITHWARNING)
                {
                    // You can fetch the buyer's details (shipping address, etc.) out of the response object now.
                    payerID ??= response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID;
                    ////var name = checkout.Shipping.FirstName = response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].ShipToAddress.Name;
                    ////var street1 = checkout.Shipping.Address.Street1 = response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].ShipToAddress.Street1;
                    ////var street2 = checkout.Shipping.Address.Street2 = response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].ShipToAddress.Street2;
                    ////var city = checkout.Shipping.Address.City = response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].ShipToAddress.CityName;
                    ////var zip = checkout.Shipping.Address.PostalCode = response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].ShipToAddress.PostalCode;
                    ////var stateCode = checkout.Shipping.Address.response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].ShipToAddress.StateOrProvince;
                    ////var region = EntityContext.Regions.FirstOrDefault(gr => gr.Code == stateCode);
                    //// ReSharper disable once PossibleNullReferenceException
                    ////checkout.Shipping.Address.RegionID = region?.ID;
                    ////const string country = "United States of America";
                    ////checkout.Shipping.Address.CountryID = 1;
                    // Print out the shipping address on the page
                    result.Succeeded = true;
                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorMessage = "ERROR! Sorry, an error occurred: " + response.Errors[0].ErrorCode + ": " + response.Errors[0].ShortMessage + ": " + response.Errors[0].LongMessage;
                    result.Token = string.Empty;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.ErrorMessage = "ERROR! Sorry, an error occurred: " + ex.Message + ex.StackTrace;
                return result;
            }
            // Construct the DoExpressCheckoutPayment request.
            var doExpressCheckout = new DoExpressCheckoutPaymentReq();
            var doExpressCheckoutReq = new DoExpressCheckoutPaymentRequestType { Version = ConfigurationManager.AppSettings["Clarity.Payment.PayPal.Version"] };
            var doExpressCheckoutDetails = new DoExpressCheckoutPaymentRequestDetailsType { Token = token, PayerID = payerID };
            ////var cartItems = cart.SalesItems;
            ////var cuts = GetCartCutsForPayback(cartItems);
            // ReSharper disable once UnusedVariable
            var payDetails = GetPayPalPayDetails(cart, /*cuts,*/ out var tooMany);
            // Put the payment details into the request
            doExpressCheckoutDetails.PaymentDetails = payDetails;
            // Specify the payment action.  Required.
            doExpressCheckoutDetails.PaymentAction = PaymentActionCodeType.SALE;
            // Put the request details into the request.
            doExpressCheckoutReq.DoExpressCheckoutPaymentRequestDetails = doExpressCheckoutDetails;
            // And finally, put the request into the req object.
            doExpressCheckout.DoExpressCheckoutPaymentRequest = doExpressCheckoutReq;
            // The api and credentials variables are still good from last time, so we'll just reuse them.
            try
            {
                var response = api.DoExpressCheckoutPayment(doExpressCheckout, credentials);
                if (response.Ack == AckCodeType.SUCCESS || response.Ack == AckCodeType.SUCCESSWITHWARNING)
                {
                    // Transaction was successful.  This is a basic example; there are some other elements that you need to pay attention to here, such as:
                    //  - PaymentStatus (part of response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[x] -- should be "Completed" for Sale transactions, or "Pending" for Authorization/Order transactions.
                    //  - PendingReason (also part of response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[x] -- should be "none" for Sale transactions, "authorization" for Authorization transactions,
                    //    or "order" for order transactions.  Any other value indicates the transaction is on hold.
                    // For this example, we'll just grab the transaction ID and show it to the user.
                    var transactionId = response.DoExpressCheckoutPaymentResponseDetails?.PaymentInfo[0]?.TransactionID;
                    var user = await Workflows.Users.GetAsync(checkout.WithUserInfo!.ExternalUserID!, contextProfileName).ConfigureAwait(false);
                    if (user != null)
                    {
                        pricingFactoryContext.UserID = user.ID;
                        if (user.AccountID is > 0)
                        {
                            pricingFactoryContext.AccountID = user.AccountID;
                        }
                    }
                    var checkoutResult = await checkoutWithProvider().ConfigureAwait(false);
                    result.OrderID = checkoutResult.OrderID;
                    result.PaymentTransactionID = transactionId;
                    result.Token += $"<p><b>Transaction ID:</b> {transactionId}<br/><b>Order ID:</b> {result.OrderID:0000000000}</p>";
                    result.Succeeded = true;
                    // ReSharper disable once PossibleInvalidOperationException
                    try
                    {
                        await ProcessEmailsForCheckoutAsync(
                                checkoutResult: result,
                                orderID: result.OrderID!.Value,
                                purchaseOrderNumber: checkout.PayByBillMeLater?.CustomerPurchaseOrderNumber,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                    }
                    catch
                    {
                        result.WarningMessages.Add("There were errors processing the emails");
                    }
                    return result;
                }
                result.ErrorMessage = response.Errors.Select(x => x.LongMessage).DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + "\r\n" + n);
                result.Succeeded = false;
                return result;
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.ErrorMessage += "<br/>ERROR! Sorry, an error occurred: " + ex.Message + ex.StackTrace;
                result.Token = string.Empty;
                return result;
            }
        }

        /// <summary>Gets PayPal pay details.</summary>
        /// <param name="cart">   The cart.</param>
        /// <param name="tooMany">The too many.</param>
        /// <returns>The PayPal payment details.</returns>
        private static List<PaymentDetailsType> GetPayPalPayDetails(ICartModel cart, out bool tooMany)
        {
            var payDetails = new List<PaymentDetailsType>();
            // We can only have up to 10 sellers in a single transaction, the tooMany flag tells us we hit that point
            tooMany = false;
            var cartItems = cart.SalesItems;
            var receiverEmail = ConfigurationManager.AppSettings["Clarity.Payment.PayPal.ReceiverPayPalEmail"];
            // Specify seller details, This is the seller's paypal email, set up when they listed the item
            // A Unique Identifier for this transaction only
            var paymentDetails = new PaymentDetailsType
            {
                SellerDetails = new() { PayPalAccountID = receiverEmail },
                PaymentRequestID = "CEFPayRequest_" + payDetails.Count + "_" + Guid.NewGuid(),
            };
            var lineItems = new List<PaymentDetailsItemType>();
            foreach (var cartItem in cartItems.Where(x => x.Active))
            {
                if (!cartItem.ProductID.HasValue)
                {
                    continue;
                }
                var lineItem = new PaymentDetailsItemType();
                // Cost of the item, with discounts but not taxes, round to next penny up
                // ReSharper disable once PossibleInvalidOperationException
                decimal price = 0;
                if (cartItem.UnitSoldPrice > 0)
                {
                    price = cartItem.UnitSoldPrice.Value;
                }
                else if (cartItem.UnitCorePrice > 0)
                {
                    price = cartItem.UnitCorePrice;
                }
                var itemPrice = Math.Ceiling(price * 100m) / 100m;
                // Taxes on the itemPrice amount, round to next penny up
                var itemTaxes = 0.00m; // Math.Ceiling(itemPrice * TaxRate * 100m) / 100m;
                // Item Details
                lineItem.Number = cartItem.ProductID.Value.ToString(CultureInfo.InvariantCulture);
                lineItem.Name = cartItem.ProductName;
                lineItem.Quantity = (int)cartItem.Quantity;
                lineItem.Amount = new(CurrencyCodeType.USD, itemPrice.ToString("0.00", CultureInfo.InvariantCulture));
                lineItem.Tax = new(CurrencyCodeType.USD, itemTaxes.ToString("0.00", CultureInfo.InvariantCulture));
                // Add the Item
                lineItems.Add(lineItem);
            }
            // Add the line item details to the payment details
            paymentDetails.PaymentDetailsItem = lineItems;
            // Set the line item subtotal. This MUST be the sum of all line item amounts multiplied by their respective quantities.
            paymentDetails.ItemTotal = new(CurrencyCodeType.USD, cart.Totals!.SubTotal.ToString("0.00", CultureInfo.InvariantCulture));
            // If you include tax amounts in your line items, then TaxTotal MUST be the sum of all line item tax amounts multiplied by their respective quantities.
            // Otherwise, it can be an arbitrary amount.
            paymentDetails.TaxTotal = new(CurrencyCodeType.USD, cart.Totals.Tax.ToString("0.00", CultureInfo.InvariantCulture));
            paymentDetails.ShippingTotal = new(CurrencyCodeType.USD, cart.Totals.Shipping.ToString("0.00", CultureInfo.InvariantCulture));
            // Specify order total
            paymentDetails.OrderTotal = new(CurrencyCodeType.USD, cart.Totals.Total.ToString("0.00", CultureInfo.InvariantCulture));
            payDetails.Add(paymentDetails);
            return payDetails;
        }

        // ReSharper disable once FunctionComplexityOverflow
        private ICheckoutResult CheckoutInner(ICheckoutModel checkout, ICartModel cart)
        {
            var result = new CheckoutResult();
            // Start constructing the SetExpressCheckout request
            var secr = new SetExpressCheckoutReq();
            var setExpressCheckoutReq = new SetExpressCheckoutRequestType
            {
                Version = ConfigurationManager.AppSettings["Clarity.Payment.PayPal.Version"],
            };
            var setExpressCheckoutDetails = new SetExpressCheckoutRequestDetailsType
            {
                ReturnURL = string.IsNullOrWhiteSpace(checkout.PayByPayPal!.ReturnUrl)
                    ? CEFConfigDictionary.SiteRouteHostUrl + ConfigurationManager.AppSettings["Clarity.Payment.PayPal.ReturnURL"]
                    : checkout.PayByPayPal.ReturnUrl,
                CancelURL = string.IsNullOrWhiteSpace(checkout.PayByPayPal.ReturnUrl)
                    ? CEFConfigDictionary.SiteRouteHostUrl + ConfigurationManager.AppSettings["Clarity.Payment.PayPal.CancelURL"]
                    : checkout.PayByPayPal.CancelUrl,
            };
            // Set the size of this array to the number of sellers that will be paid in this transaction.  Maximum of 10.
            // Put the payment details into the request
            setExpressCheckoutDetails.PaymentDetails = GetPayPalPayDetails(cart, out _);
            setExpressCheckoutDetails.PaymentAction = CEFConfigDictionary.PaymentsProcess == Enums.PaymentProcessMode.AuthorizeOnly
                ? PaymentActionCodeType.AUTHORIZATION
                : PaymentActionCodeType.SALE;
            // Put the request details into the request.
            setExpressCheckoutReq.SetExpressCheckoutRequestDetails = setExpressCheckoutDetails;
            // And finally, put the request into the req object.
            secr.SetExpressCheckoutRequest = setExpressCheckoutReq;
            // PayPal configuration -- set mode to "live" to use with the live site
            var payPalConfig = new Dictionary<string, string>();
            var mode = CEFConfigDictionary.PaymentsProviderMode;
            var testMode = mode == Enums.PaymentProviderMode.Testing;
            payPalConfig["mode"] = testMode ? "sandbox" : "live";
            // Create an API instance.
            var paypalAPI = new PayPalAPIInterfaceServiceService(payPalConfig);
            // Set our API credentials.
            var credentials = PayPalCheckoutProviderConfig.GetPayPalSignatureCredential();
            // Now we're ready to run the call.
            try
            {
                var response = paypalAPI.SetExpressCheckout(secr, credentials);
                if (response.Ack == AckCodeType.SUCCESS || response.Ack == AckCodeType.SUCCESSWITHWARNING)
                {
                    var token = response.Token;
                    result.Succeeded = true;
                    // Use https://www.paypal.com/cgi-bin/webscr?cmd=_express-checkout&useraction=commit&token= for the live site
                    // "useraction=commit" tells PayPal that you intend to complete the purchase as soon as the buyer returns from PayPal.
                    // It changes some UI elements on PayPal to make it obvious to the buyer that they're completing the purchase as
                    // soon as they're done on PayPal.
                    var sandbox = testMode ? "sandbox." : string.Empty;
                    result.Token += $"https://www.{sandbox}paypal.com/cgi-bin/webscr?cmd=_express-checkout&useraction=commit&token={token}";
                    return result;
                }
                result.Token = $"Sorry, an error occurred: {response.Errors[0].ErrorCode}: {response.Errors[0].ShortMessage}: {response.Errors[0].LongMessage}";
                return result;
            }
            catch (PayPalException ex)
            {
                result.Token = ex.InnerException is ConnectionException exception ? exception.Response : ex.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Token = $"Sorry, an error occurred: {ex.Message}";
                return result;
            }
        }

        private async Task ProcessEmailsForCheckoutAsync(
            ICheckoutResult checkoutResult,
            int orderID,
            string? purchaseOrderNumber,
            string? contextProfileName)
        {
            var order = await Workflows.SalesOrders.GetAsync(orderID, contextProfileName).ConfigureAwait(false);
            order!.PaymentTransactionID = checkoutResult.PaymentTransactionID;
            order.PurchaseOrderNumber = purchaseOrderNumber;
            try
            {
                await new SalesOrdersSubmittedNormalToCustomerEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesOrder"] = order, })
                    .ConfigureAwait(false);
            }
            catch
            {
                checkoutResult.ErrorMessage ??= string.Empty;
                checkoutResult.ErrorMessage += "There was an error sending the customer order confirmation.\r\n";
            }
            try
            {
                await new SalesOrdersSubmittedNormalToBackOfficeEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesOrder"] = order, })
                    .ConfigureAwait(false);
            }
            catch
            {
                // checkoutResult.ErrorMessage ??= string.Empty;
                // checkoutResult.ErrorMessage += "There was an error sending the back-office order confirmation.\r\n";
            }
            try
            {
                await new SalesOrdersSubmittedNormalToBackOfficeStoreEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesOrder"] = order, })
                    .ConfigureAwait(false);
            }
            catch
            {
                // checkoutResult.ErrorMessage ??= string.Empty;
                // checkoutResult.ErrorMessage += "There was an error sending the store back-office order confirmation.\r\n";
            }
        }
    }
}
#endif
