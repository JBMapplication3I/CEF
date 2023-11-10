// <copyright file="AuthorizePaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authorize payments provider class</summary>
#if !NET5_0_OR_GREATER // Authorize.NET doesn't have .net 5.0+ builds
namespace Clarity.Ecommerce.Providers.Payments.Authorize
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.APICore;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using AuthV1 = AuthorizeNet.Api.Contracts.V1;

    /// <summary>An authorize payments provider.</summary>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class AuthorizePaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            Debug.WriteLine("CreateCustomerProfile Sample");
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.RunEnvironment = TestMode
                ? AuthorizeNet.Environment.SANDBOX
                : AuthorizeNet.Environment.PRODUCTION;
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.MerchantAuthentication = new()
            {
                name = login,
                ItemElementName = AuthV1.ItemChoiceType.transactionKey,
                Item = transactionKey,
            };
            var creditCard = new AuthV1.creditCardType
            {
                cardNumber = payment.CardNumber,
                expirationDate = $"{payment.ExpirationMonth ?? 0:00}{payment.ExpirationYear.ToString()[^2..]}",
            };
            // standard api call to retrieve response
            var cc = new AuthV1.paymentType { Item = creditCard };
            var paymentProfileList = new List<AuthV1.customerPaymentProfileType>();
            var paymentProfile = new AuthV1.customerPaymentProfileType { payment = cc };
            paymentProfileList.Add(paymentProfile);
            var addressInfoList = new List<AuthV1.customerAddressType>();
            var homeAddress = new AuthV1.customerAddressType
            {
                address = billing.Address?.Street1,
                city = billing.Address?.City,
                zip = billing.Address?.PostalCode,
                country = billing.Address?.CountryName,
                firstName = billing.FirstName,
                lastName = billing.LastName,
            };
            addressInfoList.Add(homeAddress);
            var customerProfile = new AuthV1.customerProfileType
            {
                merchantCustomerId = $"{billing.FirstName}.{billing.LastName}",
                paymentProfiles = paymentProfileList.ToArray(),
                shipToList = addressInfoList.ToArray(),
                email = billing.Email1,
            };
            var request = new AuthV1.createCustomerProfileRequest { profile = customerProfile, validationMode = AuthV1.validationModeEnum.none };
            // validate
            try
            {
                var controller = new createCustomerProfileController(request); // instantiate the controller that will call the service
                controller.Execute();
                var response = controller.GetApiResponse(); // get the response from the service (errors contained if any)
                if (response?.messages.resultCode == AuthV1.messageTypeEnum.Ok)
                {
                    if (response.messages.message != null)
                    {
                        Debug.WriteLine("Success, CustomerProfileID : " + response.customerProfileId);
                        Debug.WriteLine("Success, CustomerPaymentProfileID : " + response.customerPaymentProfileIdList[0]);
                        Debug.WriteLine("Success, CustomerShippingProfileID : " + response.customerShippingAddressIdList[0]);
                        return Task.FromResult(AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(true, $"{response.customerProfileId}/{response.customerPaymentProfileIdList.FirstOrDefault()}", string.Empty));
                        ////return AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(true, $"{response.customerPaymentProfileIdList.FirstOrDefault()}", "");
                    }
                }
                else if (response?.messages?.message[0].code == "E00039")
                {
                    // Customer profile already exists retrieve it and add payment profile
                    var profile = GetCustomerProfile(billing);
                    return Task.FromResult(CreatePaymentProfile(profile.customerProfileId, paymentProfile));
                }
                else if (response != null)
                {
                    Debug.WriteLine("Error: " + response.messages!.message[0].code + "  " + response.messages.message[0].text);
                    return Task.FromResult(AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(false, null, response.messages.message[0].code + "  " + response.messages.message[0].text));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
            return Task.FromResult(AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(false, null, "Failed creating profile"));
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            // Nothing to update - we only update card nickname in cef
            return Task.FromResult(AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(false, string.Empty, string.Empty));
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.RunEnvironment = TestMode
                ? AuthorizeNet.Environment.SANDBOX
                : AuthorizeNet.Environment.PRODUCTION;
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.MerchantAuthentication = new()
            {
                name = login,
                ItemElementName = AuthV1.ItemChoiceType.transactionKey,
                Item = transactionKey,
            };
            ////if (payment.Token.IndexOf("/") > -1)
            ////{
            var tokens = payment.Token!.Split('/');
            ////    payment.Token = tokens[1];
            ////}
            // Please update the subscriptionId according to your sandbox credentials
            var request = new AuthV1.deleteCustomerPaymentProfileRequest
            {
                customerProfileId = tokens[0],
                customerPaymentProfileId = tokens[1],
            };
            // Prepare Request
            try
            {
                var controller = new deleteCustomerPaymentProfileController(request);
                controller.Execute();
                // Send Request to EndPoint
                var response = controller.GetApiResponse();
                if (response?.messages.resultCode == AuthV1.messageTypeEnum.Ok)
                {
                    if (response.messages?.message != null)
                    {
                        Debug.WriteLine("Success, ResultCode : " + response.messages.resultCode);
                        return Task.FromResult(AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(true, string.Empty, string.Empty));
                    }
                }
                else if (response?.messages?.message != null)
                {
                    Debug.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
            return Task.FromResult(AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(false, string.Empty, string.Empty));
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Deletes the customer profiles described by customerProfileId.</summary>
        /// <param name="customerProfileId"> Identifier for the customer profile.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
#pragma warning disable IDE0060
        public void DeleteCustomerProfiles(string customerProfileId, string? contextProfileName)
#pragma warning restore IDE0060
        {
            Debug.WriteLine("DeleteCustomerProfile Sample");
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.MerchantAuthentication = new()
            {
                name = "7CzD39cE",
                ItemElementName = AuthV1.ItemChoiceType.transactionKey,
                Item = "4tH54w984sY5jyKc",
            };
            // Please update the subscriptionId according to your sandbox credentials
            var request = new AuthV1.deleteCustomerProfileRequest
            {
                customerProfileId = customerProfileId,
            };
            // Prepare Request
            var controller = new deleteCustomerProfileController(request);
            controller.Execute();
            // Send Request to EndPoint
            var response = controller.GetApiResponse();
            if (response != null && response.messages.resultCode == AuthV1.messageTypeEnum.Ok)
            {
                if (response.messages.message != null)
                {
                    Debug.WriteLine("Success, ResultCode : " + response.messages.resultCode);
                }
            }
            else if (response?.messages.message != null)
            {
                Debug.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
            }
        }

        /// <summary>List customer profiles.</summary>
        public void ListCustomerProfiles()
        {
            Debug.WriteLine("Get Customer Profile Id sample");
            ////<add key="SystemValues.Payment.AuthorizeNet.Login" value="7CzD39cE" />
            ////< add key = "SystemValues.Payment.AuthorizeNet.TransactionKey" value = "4tH54w984sY5jyKc" />
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.MerchantAuthentication = new()
            {
                name = "7CzD39cE",
                ItemElementName = AuthV1.ItemChoiceType.transactionKey,
                Item = "4tH54w984sY5jyKc",
            };
            var request = new AuthV1.getCustomerProfileIdsRequest();
            // instantiate the controller that will call the service
            var controller = new getCustomerProfileIdsController(request);
            controller.Execute();
            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
            if (response != null && response.messages.resultCode == AuthV1.messageTypeEnum.Ok)
            {
                Debug.WriteLine(response.messages.message[0].text);
                Debug.WriteLine("Customer Profile Ids: ");
                foreach (var id in response.ids)
                {
                    Debug.WriteLine(id);
                }
            }
            else if (response != null)
            {
                Debug.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
            }
        }

        /// <inheritdoc/>
        public Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Creates payment profile.</summary>
        /// <param name="customerProfileId">Identifier for the customer profile.</param>
        /// <param name="paymentProfile"> The Cc payment profile.</param>
        /// <returns>The new payment profile.</returns>
        private static IPaymentWalletResponse CreatePaymentProfile(
            string customerProfileId,
            AuthV1.customerPaymentProfileType paymentProfile)
        {
            var request = new AuthV1.createCustomerPaymentProfileRequest
            {
                customerProfileId = customerProfileId,
                paymentProfile = paymentProfile,
                validationMode = AuthV1.validationModeEnum.none,
            };
            var controller = new createCustomerPaymentProfileController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            if (response?.messages.resultCode != AuthV1.messageTypeEnum.Ok)
            {
                return AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(
                    approved: false,
                    customerProfileId: null,
                    responseCode: "Failed creating profile");
                ////return new AuthorizeWalletResponse(true, $"{response.customerPaymentProfileId}", "");
            }
            if (response.messages.message != null)
            {
                return AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(
                    approved: false,
                    customerProfileId: null,
                    responseCode: response.messages.message[0].code + "  " + response.messages.message[0].text);
            }
            Debug.WriteLine("Success, CustomerProfileID : " + response.customerProfileId);
            return AuthorizePaymentsProviderExtensions.ToPaymentWalletResponse(
                approved: true,
                customerProfileId: $"{response.customerProfileId}/{response.customerPaymentProfileId}",
                responseCode: string.Empty);
        }

        /// <summary>Gets customer profile.</summary>
        /// <param name="billing">The billing.</param>
        /// <returns>The customer profile.</returns>
        private static AuthV1.customerProfileMaskedType GetCustomerProfile(IContactModel billing)
        {
            var getRequest = new AuthV1.getCustomerProfileRequest
            {
                email = billing.Email1,
                merchantCustomerId = $"{billing.FirstName}.{billing.LastName}",
            };
            // instantiate the controller that will call the service
            var getController = new getCustomerProfileController(getRequest);
            getController.Execute();
            var getResponse = getController.GetApiResponse();
            return getResponse.profile;
        }

        /// <summary>Customer profile payment.</summary>
        /// <param name="payment">The payment.</param>
        /// <returns>An IPaymentResponse.</returns>
        private IPaymentResponse CustomerProfilePayment(IProviderPayment payment)
        {
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.RunEnvironment = TestMode
                ? AuthorizeNet.Environment.SANDBOX
                : AuthorizeNet.Environment.PRODUCTION;
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.MerchantAuthentication = new()
            {
                name = login,
                ItemElementName = AuthV1.ItemChoiceType.transactionKey,
                Item = transactionKey,
            };
            // create a customer payment profile
            var token = payment.Token!.Split('/').First();
            var paymentID = payment.Token.Split('/').Last();
            var profileToCharge = new AuthV1.customerProfilePaymentType
            {
                customerProfileId = token,
                paymentProfile = new() { paymentProfileId = paymentID },
            };
            var transactionRequest = new AuthV1.transactionRequestType
            {
                transactionType = nameof(transactionTypeEnum.authCaptureTransaction), // refund type
                amount = payment.Amount ?? 0m,
                profile = profileToCharge,
            };
            var request = new AuthV1.createTransactionRequest { transactionRequest = transactionRequest };
            // instantiate the collector that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();
            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
            var ret = new PaymentResponse();
            // validate
            if (response == null)
            {
                Debug.WriteLine("Null Response.");
                return ret;
            }
            if (response.messages.resultCode == AuthV1.messageTypeEnum.Ok)
            {
                if (response.transactionResponse.messages != null)
                {
                    ret.Approved = true;
                    ret.TransactionID = response.transactionResponse.transId;
                    ret.ResponseCode = response.transactionResponse.responseCode;
                    ret.AuthorizationCode = response.transactionResponse.authCode;
                    return ret;
                }
                Debug.WriteLine("Failed Transaction.");
                ret.Approved = false;
                if (response.transactionResponse.errors != null)
                {
                    ret.ResponseCode = response.transactionResponse.errors[0].errorCode;
                    ////Debug.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                }
                return ret;
            }
            Debug.WriteLine("Failed Transaction.");
            ret.Approved = false;
            ret.ResponseCode = response.transactionResponse?.errors != null ? response.transactionResponse.errors[0].errorCode : response.messages.message[0].code;
            return ret;
        }

        /// <summary>Customer profile authorization.</summary>
        /// <param name="payment">The payment.</param>
        /// <returns>An IPaymentResponse.</returns>
        private IPaymentResponse CustomerProfileAuthorization(IProviderPayment payment)
        {
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.RunEnvironment = TestMode
                ? AuthorizeNet.Environment.SANDBOX
                : AuthorizeNet.Environment.PRODUCTION;
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<AuthV1.ANetApiRequest, AuthV1.ANetApiResponse>.MerchantAuthentication = new()
            {
                name = login,
                ItemElementName = AuthV1.ItemChoiceType.transactionKey,
                Item = transactionKey,
            };
            // create a customer payment profile
            var token = payment.Token!.Split('/').First();
            var paymentID = payment.Token.Split('/').Last();
            var profileToCharge = new AuthV1.customerProfilePaymentType
            {
                customerProfileId = token,
                paymentProfile = new() { paymentProfileId = paymentID },
            };
            var transactionRequest = new AuthV1.transactionRequestType
            {
                transactionType = nameof(transactionTypeEnum.authOnlyTransaction),
                amount = payment.Amount ?? 0m,
                profile = profileToCharge,
            };
            var request = new AuthV1.createTransactionRequest { transactionRequest = transactionRequest };
            // instantiate the collector that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();
            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
            var ret = new PaymentResponse();
            // validate
            if (response == null)
            {
                Debug.WriteLine("Null Response.");
                return ret;
            }
            if (response.messages.resultCode == AuthV1.messageTypeEnum.Ok)
            {
                if (response.transactionResponse.messages != null)
                {
                    ret.Approved = true;
                    ret.TransactionID = response.transactionResponse.transId;
                    ret.ResponseCode = response.transactionResponse.responseCode;
                    ret.AuthorizationCode = response.transactionResponse.authCode;
                    return ret;
                }
                Debug.WriteLine("Failed Transaction.");
                ret.Approved = false;
                if (response.transactionResponse.errors != null)
                {
                    ret.ResponseCode = response.transactionResponse.errors[0].errorCode;
                    ////Debug.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                }
                return ret;
            }
            Debug.WriteLine("Failed Transaction.");
            ret.Approved = false;
            ret.ResponseCode = response.transactionResponse?.errors != null ? response.transactionResponse.errors[0].errorCode : response.messages.message[0].code;
            return ret;
        }
    }
}
#endif
