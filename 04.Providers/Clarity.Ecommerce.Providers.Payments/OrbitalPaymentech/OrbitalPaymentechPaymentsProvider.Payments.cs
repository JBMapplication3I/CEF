// <copyright file="OrbitalPaymentechPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the orbital paymentech payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.OrbitalPaymentech
{
    using System;
    using System.Net;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using PaymentechService;
    using Utilities;

    /// <summary>An Orbital Paymentech payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class OrbitalPaymentechPaymentsProvider : PaymentsProviderBase, IDisposable
    {
        /// <summary>The payment gateway client.</summary>
        private PaymentechGatewayPortTypeClient paymentGatewayClient = null!;

        /// <summary>True to disposed value.</summary>
        private bool disposedValue;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => OrbitalPaymentechPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <summary>Gets a value indicating whether the test mode.</summary>
        /// <value>true if test mode, false if not.</value>
        private static bool TestMode => ProviderMode != Enums.PaymentProviderMode.Production;

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override async Task InitConfigurationAsync(string? contextProfileName)
        {
            ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
            // 1.2+ is the only thing that should be allowed
            try
            {
                paymentGatewayClient = new(
                    "PaymentechGateway",
                    // ReSharper disable once StyleCop.SA1118
                    TestMode
                        ? OrbitalPaymentechPaymentsProviderConfig.TestUrl
                        : OrbitalPaymentechPaymentsProviderConfig.ProdUrl);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(InitConfigurationAsync)}.{nameof(Exception)}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: null)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(billing);
            try
            {
                return ToPaymentResponse(
                    PaymentechGatewayNewOrderRequest(
                        PaymentNewOrderRequestElement(
                            transType: OrbitalPaymentechConstants.TransactionTypes.Authorize,
                            payment: payment,
                            billing: billing!),
                        contextProfileName),
                    payment.Amount ?? 0);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AuthorizeAsync)}.{nameof(Exception)}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentResponse
                {
                    Approved = false,
                    ResponseCode = $"EXCEPTION: {ex.Message}",
                };
            }
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken, IProviderPayment payment, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false)
        {
            Contract.RequiresNotNull(billing);
            try
            {
                return ToPaymentResponse(
                    PaymentechGatewayNewOrderRequest(
                        PaymentNewOrderRequestElement(
                            transType: OrbitalPaymentechConstants.TransactionTypes.AuthorizeAndCapture,
                            payment: payment,
                            billing: billing!),
                        contextProfileName),
                    payment.Amount ?? 0);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AuthorizeAndACaptureAsync)}.{nameof(Exception)}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentResponse
                {
                    Approved = false,
                    ResponseCode = $"EXCEPTION: {ex.Message}",
                };
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Makes new order request to Payment Gateway.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">The name of the context profile.</param>
        /// <returns>A NewOrderResponseElement.</returns>
        internal NewOrderResponseElement PaymentechGatewayNewOrderRequest(
            NewOrderRequestElement request,
            string? contextProfileName)
        {
            try
            {
                return paymentGatewayClient.NewOrder(request);
            }
            catch (TimeoutException timeProblem)
            {
                Logger.LogError(
                    $"{nameof(PaymentechGatewayNewOrderRequest)}.{nameof(TimeoutException)}",
                    timeProblem.Message,
                    timeProblem,
                    contextProfileName);
                return new()
                {
                    procStatus = "TIMEOUT",
                    procStatusMessage = $"The service operation timed out. {timeProblem.Message}",
                };
            }
            catch (FaultException fault)
            {
                Logger.LogError(
                    $"{nameof(PaymentechGatewayNewOrderRequest)}.{nameof(FaultException)}",
                    fault.Message,
                    fault,
                    contextProfileName);
                return new()
                {
                    procStatus = "FAULT",
                    procStatusMessage = $"A fault exception was received. {fault.Message}",
                };
            }
            catch (CommunicationException comException)
            {
                Logger.LogError(
                    $"{nameof(PaymentechGatewayNewOrderRequest)}.{nameof(CommunicationException)}",
                    comException.Message,
                    comException,
                    contextProfileName);
                return new()
                {
                    procStatus = "COMEXCEPTION",
                    procStatusMessage = $"There was a communication problem. {comException.Message}{comException.StackTrace}",
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(
                    $"{nameof(PaymentechGatewayNewOrderRequest)}.{nameof(Exception)}",
                    ex.Message,
                    ex,
                    contextProfileName);
                return new()
                {
                    procStatus = "EXCEPTION",
                    procStatusMessage = ex.Message,
                };
            }
        }

        /// <summary>Releases the unmanaged resources used by the <seealso cref="OrbitalPaymentechPaymentsProvider"/>
        /// and optionally releases the managed resources.</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only
        ///                         unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    paymentGatewayClient?.Close();
                    paymentGatewayClient = null!;
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        private static NewOrderRequestElement BaseNewOrderRequestElement()
        {
            return new()
            {
                version = OrbitalPaymentechConstants.ApiVersion,
                merchantID = OrbitalPaymentechPaymentsProviderConfig.MerchantID,
                orbitalConnectionUsername = OrbitalPaymentechPaymentsProviderConfig.Username,
                orbitalConnectionPassword = OrbitalPaymentechPaymentsProviderConfig.Password,
                industryType = OrbitalPaymentechConstants.IndustryType,
                bin = OrbitalPaymentechConstants.Bin,
                terminalID = OrbitalPaymentechConstants.TerminalId,
            };
        }

        private static NewOrderRequestElement PaymentNewOrderRequestElementForAuthorizeZeroAmount(
            IProviderPayment payment,
            IContactModel billing)
        {
            return PaymentNewOrderRequestElement(
                OrbitalPaymentechConstants.TransactionTypes.Authorize,
                payment,
                0,
                billing);
        }

        private static NewOrderRequestElement PaymentNewOrderRequestElement(
            string transType,
            IProviderPayment payment,
            IContactModel billing)
        {
            return PaymentNewOrderRequestElement(transType, payment, payment.Amount ?? 0, billing);
        }

        private static NewOrderRequestElement PaymentNewOrderRequestElement(
            string transType,
            IProviderPayment payment,
            decimal amount,
            IContactModel billing)
        {
            Contract.RequiresNotNull(billing);
            var element = BaseNewOrderRequestElement();
            element.addProfileFromOrder = "A";
            element.profileOrderOverideInd = "NO";
            element.useCustomerRefNum = string.Empty;
            element.transType = transType;
            element.orderID = ShortGuid(); // Unique Order ID since nothing comes from the IProviderPayment
            element.amount = $"{amount:F}".Replace(".", string.Empty); // Remove decimal
            element.customerName = payment.CardHolderName;
            element.avsCountryCode = "US";
            element.avsZip = payment.Zip;
            element.pCardDestAddress = billing.Address!.Street1;
            element.pCardDestAddress2 = billing.Address.Street2;
            element.pCardDestCity = billing.Address.City;
            element.pCardDestStateCd = billing.Address.RegionCode;
            element.pCardDestZip = billing.Address.PostalCode;
            element.avsAddress1 = billing.Address.Street1;
            element.avsAddress2 = billing.Address.Street2;
            element.avsCity = billing.Address.City;
            element.avsState = billing.Address.RegionCode;
            element.avsZip = billing.Address.PostalCode;
            element.avsDestAddress1 = billing.Address.Street1;
            element.avsDestAddress2 = billing.Address.Street2;
            element.avsDestCity = billing.Address.City;
            element.avsDestState = billing.Address.RegionCode;
            element.avsDestZip = billing.Address.PostalCode;
            element.ewsAddressLine1 = billing.Address.Street1;
            element.ewsAddressLine2 = billing.Address.Street2;
            element.ewsCity = billing.Address.City;
            element.ewsState = billing.Address.RegionCode;
            element.ewsZip = billing.Address.PostalCode;
            if (!Contract.CheckValidKey(payment.RoutingNumber) && Contract.CheckValidKey(payment.CVV))
            {
                // TODO: Determine card type and don't send if not Visa or Discover
                element.ccCardVerifyPresenceInd = OrbitalPaymentechConstants.CreditCardPresenceIndicators.ValueIsPresent;
                element.ccCardVerifyNum = payment.CVV;
            }
            // Use Token if it exists
            if (Contract.CheckValidKey(payment.Token))
            {
                if (TryParseToken(payment.Token, out var customerRefNum, out var mitReceivedTransactionID))
                {
                    element.mitMsgType = OrbitalPaymentechConstants.InitiatedTransactionMessageCodes.ByMerchant.UnscheduledCredentialOnFile;
                    element.mitStoredCredentialInd = OrbitalPaymentechConstants.StoredCredentialIndicators.OnFile;
                    element.customerRefNum = customerRefNum;
                    element.mitSubmittedTransactionID = mitReceivedTransactionID;
                }
                else
                {
                    element.mitMsgType = OrbitalPaymentechConstants.InitiatedTransactionMessageCodes.ByMerchant.CustomerGenerated;
                    element.mitStoredCredentialInd = OrbitalPaymentechConstants.StoredCredentialIndicators.OnFile;
                    element.mitSubmittedTransactionID = string.Empty;
                }
                return element;
            }
            element.mitMsgType = OrbitalPaymentechConstants.InitiatedTransactionMessageCodes.ByMerchant.CustomerGenerated;
            element.mitStoredCredentialInd = OrbitalPaymentechConstants.StoredCredentialIndicators.OnFile;
            element.mitSubmittedTransactionID = string.Empty;
            if (!Contract.CheckValidKey(payment.RoutingNumber))
            {
                element.ccAccountNum = payment.CardNumber;
                element.ccExp = ExtractCcExpirationDate(payment);
                // Check for AMEX and remove MIT values if present (doesn't support MIT data at this time)
                if (payment.CardNumber!.Length == 15)
                {
                    ////element.cardBrand = OrbitalPaymentechConstants.CardBrands.ChaseNetCreditCard;
                    element.mitMsgType = null;
                    element.mitStoredCredentialInd = null;
                    element.mitSubmittedTransactionID = null;
                }
            }
            else
            {
                element.cardBrand = OrbitalPaymentechConstants.CardBrands.ElectronicCheck;
                element.ecpCheckRT = payment.RoutingNumber;
                element.ecpCheckDDA = payment.AccountNumber;
                element.ecpBankAcctType = payment.CardType == "Checking"
                    ? OrbitalPaymentechConstants.ECheckAccountTypes.ConsumerChecking
                    : OrbitalPaymentechConstants.ECheckAccountTypes.ConsumerSavings;
                element.ecpDelvMethod = OrbitalPaymentechConstants.ECPPaymentDeliveryMethod.BestPossibleMethod;
            }
            return element;
        }

        private static string ShortGuid()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())[..22];
        }

        private static string ExtractCcExpirationDate(IProviderPayment payment)
        {
            Contract.RequiresNotNull(payment.ExpirationYear, "Expiration Month cannot be null.");
            Contract.RequiresNotNull(payment.ExpirationMonth, "Expiration Year cannot be null.");
            Contract.Requires<ArgumentOutOfRangeException>(
                payment.ExpirationMonth is >= 1 and <= 12,
                "Expiration Month must be a number from 1 to 12.");
            var parsed = CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value, true);
            return $"{parsed.Substring(2, 4)}{parsed[..2]}";
        }

        private static IPaymentResponse ToPaymentResponse(NewOrderResponseElement response, decimal amount)
        {
            if (!NewOrderResponseIsSuccess(response))
            {
                return new PaymentResponse
                {
                    Approved = false,
                    ResponseCode = $"{response.procStatus}: {response.procStatusMessage}",
                };
            }
            if (NewOrderResponseIsApproved(response))
            {
                return new PaymentResponse
                {
                    Approved = true,
                    Amount = decimal.TryParse(response.requestAmount, out var requestAmount) ? requestAmount : amount,
                    AuthorizationCode = response.authorizationCode,
                    ResponseCode = $"{response.respCode}: {response.procStatusMessage}",
                    TransactionID = $"{response.txRefNum}|{response.txRefIdx}|{response.mitReceivedTransactionID}",
                };
            }
            if (NewOrderResponseIsDeclined(response) || NewOrderResponseIsError(response))
            {
                return new PaymentResponse
                {
                    Approved = false,
                    ResponseCode = $"{response.respCode}: {response.procStatusMessage}",
                };
            }
            return new PaymentResponse
            {
                Approved = false,
                ResponseCode = $"{response.procStatus}: {response.procStatusMessage}",
            };
        }

        private static bool NewOrderResponseIsSuccess(NewOrderResponseElement response)
        {
            return response?.procStatus == OrbitalPaymentechConstants.ProcedureResponseStatuses.Success;
        }

        private static bool NewOrderResponseIsDeclined(NewOrderResponseElement response)
        {
            return response?.approvalStatus == OrbitalPaymentechConstants.ApprovalStatuses.Declined;
        }

        private static bool NewOrderResponseIsApproved(NewOrderResponseElement response)
        {
            return response?.approvalStatus == OrbitalPaymentechConstants.ApprovalStatuses.Approved;
        }

        private static bool NewOrderResponseIsError(NewOrderResponseElement response)
        {
            return response?.approvalStatus == OrbitalPaymentechConstants.ApprovalStatuses.Error;
        }

        /// <summary>Makes new order request to Payment Gateway.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A NewOrderResponseElement.</returns>
        private NewOrderResponseElement PaymentechGatewayNewOrderRequest(NewOrderRequestElement request)
        {
            try
            {
                return paymentGatewayClient.NewOrder(request);
            }
            catch (TimeoutException timeProblem)
            {
                return new()
                {
                    procStatus = "TIMEOUT",
                    procStatusMessage = $"The service operation timed out. {timeProblem.Message}",
                };
            }
            catch (FaultException fault)
            {
                return new()
                {
                    procStatus = "FAULT",
                    procStatusMessage = $"A fault exception was received. {fault.Message}",
                };
            }
            catch (CommunicationException comException)
            {
                return new()
                {
                    procStatus = "COMEXCEPTION",
                    procStatusMessage = $"There was a communication problem. {comException.Message}{comException.StackTrace}",
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    procStatus = "EXCEPTION",
                    procStatusMessage = ex.Message,
                };
            }
        }
    }
}
