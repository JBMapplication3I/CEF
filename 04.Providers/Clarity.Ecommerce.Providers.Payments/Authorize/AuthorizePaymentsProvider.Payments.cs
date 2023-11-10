// <copyright file="AuthorizePaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authorize payments provider class</summary>
#if !NET5_0_OR_GREATER // Authorize.NET doesn't have .net 5.0+ builds
#pragma warning disable CS0618
namespace Clarity.Ecommerce.Providers.Payments.Authorize
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using AuthorizeNet;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Utilities;
    using AuthV1 = AuthorizeNet.Api.Contracts.V1;

    /// <content>An authorize payments provider.</content>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class AuthorizePaymentsProvider : PaymentsProviderBase
    {
        /// <summary>The login.</summary>
        private string? login;

        /// <summary>The transaction key.</summary>
        private string? transactionKey;

        /// <summary>The gateway.</summary>
        private Gateway? gateway;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => AuthorizePaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <summary>Gets a value indicating whether the test mode.</summary>
        /// <value>True if test mode, false if not.</value>
        private static bool TestMode => ProviderMode != Enums.PaymentProviderMode.Production;

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            transactionKey = AuthorizePaymentsProviderConfig.TransactionKey;
            login = AuthorizePaymentsProviderConfig.Login;
            gateway = new(login, transactionKey, TestMode);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            if (!payment.Amount.HasValue)
            {
                throw new ArgumentNullException(nameof(payment.Amount));
            }
            if (!string.IsNullOrEmpty(payment.Token))
            {
                return Task.FromResult(CustomerProfileAuthorization(payment));
            }
            var isCreditCard = string.IsNullOrEmpty(payment.AccountNumber)
                && string.IsNullOrEmpty(payment.RoutingNumber)
                && !string.IsNullOrEmpty(payment.CardNumber);
            if (isCreditCard)
            {
                var request = new CardPresentAuthorizationRequest(
                    payment.Amount.Value,
                    payment.CardNumber,
                    payment.ExpirationMonth.ToString(),
                    payment.ExpirationYear.ToString());
                MapAddresses(request, billing, shipping);
                var response = gateway!.Send(request);
                return Task.FromResult(response.ToPaymentResponse());
            }
            // eCheck
            var eCheckAuthRequest = new EcheckAuthorizationRequest(
                type: EcheckType.WEB,
                amount: payment.Amount.Value,
                bankABACode: payment.RoutingNumber,
                bankAccountNumber: payment.AccountNumber,
                acctType: payment.CardType == "Checking" ? BankAccountType.Checking : BankAccountType.Savings,
                bankName: payment.BankName,
                acctName: billing?.FullName,
                bankCheckNumber: null);
            var authResponse = gateway!.Send(eCheckAuthRequest);
            return Task.FromResult(authResponse.ToPaymentResponse());
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(payment.Amount);
            ////var request = new CardNotPresentCaptureOnly(
            ////    paymentAuthorizationToken,
            ////    payment.CardNumber,
            ////    payment.ExpirationMonth + payment.ExpirationYear.ToString(),
            ////    payment.Amount.Value);
            ////var response = gateway.Send(request);
            ////return response.ToPaymentResponse();
            // ReSharper disable PossibleInvalidOperationException
            // ReSharper disable once UnusedVariable
            _ = new CardPresentPriorAuthCapture(paymentAuthorizationToken, payment.Amount ?? 0m);
            var response = Run(payment.Amount!.Value, paymentAuthorizationToken, payment.InvoiceNumber ?? string.Empty);
            return Task.FromResult(response.ToPaymentResponse(payment.Amount.Value));
            // ReSharper restore PossibleInvalidOperationException
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false)
        {
            if (!payment.Amount.HasValue)
            {
                throw new ArgumentNullException(nameof(payment.Amount));
            }
            if (!string.IsNullOrEmpty(payment.Token))
            {
                return Task.FromResult(CustomerProfilePayment(payment));
            }
            var isCreditCard = string.IsNullOrEmpty(payment.AccountNumber)
                && string.IsNullOrEmpty(payment.RoutingNumber)
                && !string.IsNullOrEmpty(payment.CardNumber);
            if (isCreditCard)
            {
                var exp = CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value);
                var request = new CardPresentAuthorizeAndCaptureRequest(
                    payment.Amount.Value,
                    payment.CardNumber,
                    exp[..2],
                    exp.Substring(2, 2))
                {
                    ExpDate = $"{payment.ExpirationYear}-{payment.ExpirationMonth}",
                };
                MapAddresses(request, billing, shipping);
                var response = gateway!.Send(request);
                return Task.FromResult(response.ToPaymentResponse());
            }
            // eCheck
            var eCheckAuthRequest = new EcheckAuthorizationRequest(
                type: EcheckType.WEB,
                amount: payment.Amount.Value,
                bankABACode: payment.RoutingNumber,
                bankAccountNumber: payment.AccountNumber,
                acctType: payment.CardType == "Checking" ? BankAccountType.Checking : BankAccountType.Savings,
                bankName: payment.BankName,
                acctName: billing?.FullName ?? string.Empty,
                bankCheckNumber: null);
            var authResponse = gateway!.Send(eCheckAuthRequest);
            if (!authResponse.Approved)
            {
                return Task.FromResult(authResponse.ToPaymentResponse());
            }
            var eCheckCaptureRequest = new EcheckCaptureRequest(
                authCode: authResponse.AuthorizationCode,
                type: EcheckType.WEB,
                amount: payment.Amount.Value,
                bankABACode: payment.RoutingNumber,
                bankAccountNumber: payment.AccountNumber,
                acctType: payment.CardType == "Checking" ? BankAccountType.Checking : BankAccountType.Savings,
                bankName: payment.BankName,
                acctName: billing?.FullName ?? string.Empty,
                bankCheckNumber: null);
            var eCheckCaptureResponse = gateway.Send(eCheckCaptureRequest);
            return Task.FromResult(eCheckCaptureResponse.ToPaymentResponse());
        }

        /// <summary>Runs the transaction.</summary>
        /// <param name="amount">       The transaction amount.</param>
        /// <param name="transactionID">Identifier for the transaction.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <returns>An AuthV1.ANetApiResponse.</returns>
        private static AuthV1.createTransactionResponse? Run(decimal amount, string transactionID, string invoiceNumber)
        {
            Debug.WriteLine("Capture Previously Authorized Amount");
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.RunEnvironment = TestMode
                ? AuthorizeNet.Environment.SANDBOX
                : AuthorizeNet.Environment.PRODUCTION;
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.MerchantAuthentication
                = new()
                {
                    name = AuthorizePaymentsProviderConfig.Login,
                    ItemElementName = AuthV1.ItemChoiceType.transactionKey,
                    Item = AuthorizePaymentsProviderConfig.TransactionKey,
                };
            var transactionRequest = new AuthV1.transactionRequestType
            {
                transactionType = nameof(AuthV1.transactionTypeEnum.priorAuthCaptureTransaction), // capture prior only
                amount = amount,
                refTransId = transactionID,
                order = new() { invoiceNumber = invoiceNumber },
            };
            var request = new AuthV1.createTransactionRequest { transactionRequest = transactionRequest };
            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();
            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
            // validate
            if (response == null)
            {
                Debug.WriteLine("Null Response.");
                return null;
            }
            if (response.messages.resultCode == AuthV1.messageTypeEnum.Ok)
            {
                if (response.transactionResponse.messages != null)
                {
                    Debug.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                    Debug.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                    Debug.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                    Debug.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                    Debug.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                }
                else
                {
                    Debug.WriteLine("Failed Transaction.");
                    if (response.transactionResponse.errors == null)
                    {
                        return response;
                    }
                    Debug.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                    Debug.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                }
                return response;
            }
            Debug.WriteLine("Failed Transaction.");
            if (response.transactionResponse?.errors != null)
            {
                Debug.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                Debug.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
            }
            else
            {
                Debug.WriteLine("Error Code: " + response.messages.message[0].code);
                Debug.WriteLine("Error message: " + response.messages.message[0].text);
            }
            return response;
        }

        /// <summary>Map addresses.</summary>
        /// <param name="request"> The request.</param>
        /// <param name="billing"> The billing.</param>
        /// <param name="shipping">The shipping.</param>
        private static void MapAddresses(IGatewayRequest request, IContactModel? billing, IContactModel? shipping)
        {
            if (billing is not null)
            {
                request.FirstName = billing.FirstName;
                request.LastName = billing.LastName;
                request.Company = billing.FullName;
                request.Phone = billing.Phone1;
                request.Email = billing.Email1;
                if (billing.Address is not null)
                {
                    request.Company = billing.Address.Company;
                    request.Address = $"{billing.Address.Street1} {billing.Address.Street2}";
                    request.City = billing.Address.City;
                    request.State = billing.Address.RegionCode ?? billing.Address.Region?.Code;
                    request.Zip = billing.Address.PostalCode;
                    request.Country = billing.Address.CountryName ?? billing.Address.Country?.Name;
                }
            }
            if (shipping == null)
            {
                return;
            }
            request.ShipToFirstName = shipping.FirstName;
            request.ShipToLastName = shipping.LastName;
            request.ShipToCompany = shipping.FullName;
            if (shipping.Address == null)
            {
                return;
            }
            request.ShipToAddress = $"{shipping.Address.Street1} {shipping.Address.Street2}";
            request.ShipToCity = shipping.Address.City;
            request.ShipToState = shipping.Address.RegionCode ?? shipping.Address.Region?.Code;
            request.ShipToZip = shipping.Address.PostalCode;
            request.ShipToCountry = shipping.Address.CountryName ?? shipping.Address.Country?.Name;
        }
    }
}
#endif
