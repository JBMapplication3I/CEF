// <copyright file="OrbitalPaymentechPaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the orbital paymentech payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.OrbitalPaymentech
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using PaymentechService;
    using Utilities;

    /// <summary>An Orbital Paymentech payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class OrbitalPaymentechPaymentsProvider : IWalletProviderBase
    {
        /// <summary>Values that represent profile request types.</summary>
        private enum ProfileRequestType
        {
            /// <summary>An enum constant representing the add option.</summary>
            Add,

            /// <summary>An enum constant representing the change option.</summary>
            Change,

            /// <summary>An enum constant representing the delete option.</summary>
            Delete,

            /// <summary>An enum constant representing the fetch option.</summary>
            Fetch,
        }

        /// <summary>Attempts to parse token from the given data.</summary>
        /// <param name="token">                   The token.</param>
        /// <param name="customerRefNum">          The customer reference number.</param>
        /// <param name="mitReceivedTransactionID">Identifier for the mit received transaction.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool TryParseToken(
            string? token,
            out string? customerRefNum,
            out string? mitReceivedTransactionID)
        {
            customerRefNum = null;
            mitReceivedTransactionID = null;
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            var delimiterIndex = token!.IndexOf('_');
            if (delimiterIndex <= 0 || delimiterIndex >= token.Length - 1)
            {
                return false;
            }
            customerRefNum = token[..delimiterIndex];
            mitReceivedTransactionID = token[(delimiterIndex + 1)..];
            return Contract.CheckAllValidKeys(customerRefNum, mitReceivedTransactionID);
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel contact,
            string? contextProfileName)
        {
            try
            {
                var customerRefNum = GetCustomerRefNumFromProviderPayment(payment, true);
                if (!Contract.CheckValidKey(customerRefNum))
                {
                    await Logger.LogErrorAsync(
                            name: $"{nameof(CreateCustomerProfileAsync)}.ERROR",
                            message: "EXCEPTION: CustomerRefNum is required to create a profile.",
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    return new PaymentWalletResponse
                    {
                        Approved = false,
                        ResponseCode = "EXCEPTION: CustomerRefNum is required to create a profile.",
                    };
                }
                var element = PaymentNewOrderRequestElementForProfile(payment, customerRefNum, contact);
                element.addProfileFromOrder = OrbitalPaymentechConstants.AddProfileFromOrderValues.UseCustomerRefNumField;
                var response = ToPaymentWalletResponse(PaymentechGatewayNewOrderRequest(element, contextProfileName));
                response.CardType = payment.CardType; // Add back to response since Paymentech does not return.
                return response;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(CreateCustomerProfileAsync)}.{nameof(Exception)}",
                        message: $"EXCEPTION: {ex.Message}",
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentWalletResponse
                {
                    Approved = false,
                    ResponseCode = $"EXCEPTION: {ex.Message}",
                };
            }
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel contact,
            string? contextProfileName)
        {
            try
            {
                var customerRefNum = GetCustomerRefNumFromProviderPayment(payment);
                if (string.IsNullOrWhiteSpace(customerRefNum))
                {
                    return new PaymentWalletResponse
                    {
                        Approved = false,
                        ResponseCode = "EXCEPTION: CustomerRefNum is required to update a profile.",
                    };
                }
                // Currently only the Wallet Name can be changed and the Wallet Name does not get sent to Paymentech
                // so there is nothing to update.
                return new PaymentWalletResponse
                {
                    Approved = true,
                    ResponseCode = "Update Not Required.",
                    Token = payment.Token,
                };
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(UpdateCustomerProfileAsync)}.{nameof(Exception)}",
                        $"EXCEPTION: {ex.Message}",
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentWalletResponse
                {
                    Approved = false,
                    ResponseCode = $"EXCEPTION: {ex.Message}",
                };
            }
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            try
            {
                var customerRefNum = GetCustomerRefNumFromProviderPayment(payment);
                if (string.IsNullOrWhiteSpace(customerRefNum))
                {
                    return new PaymentWalletResponse
                    {
                        Approved = false,
                        ResponseCode = "EXCEPTION: CustomerRefNum is required to delete a profile.",
                    };
                }
                var response = ToPaymentWalletResponse(PaymentechGatewayProfileDeleteRequest(
                    new()
                    {
                        version = OrbitalPaymentechConstants.ApiVersion,
                        merchantID = OrbitalPaymentechPaymentsProviderConfig.MerchantID,
                        orbitalConnectionUsername = OrbitalPaymentechPaymentsProviderConfig.Username,
                        orbitalConnectionPassword = OrbitalPaymentechPaymentsProviderConfig.Password,
                        bin = OrbitalPaymentechConstants.Bin,
                        customerRefNum = customerRefNum,
                    },
                    contextProfileName));
                response.CardType = payment.CardType; // Add back to response since Paymentech does not return.
                return response;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(DeleteCustomerProfileAsync)}.{nameof(Exception)}",
                        $"EXCEPTION: {ex.Message}",
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentWalletResponse
                {
                    Approved = false,
                    ResponseCode = $"EXCEPTION: {ex.Message}",
                };
            }
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            try
            {
                var customerRefNum = GetCustomerRefNumFromProviderPayment(payment);
                if (string.IsNullOrWhiteSpace(customerRefNum))
                {
                    return new PaymentWalletResponse
                    {
                        Approved = false,
                        ResponseCode = "EXCEPTION: CustomerRefNum is required to get a profile.",
                    };
                }
                var response = ToPaymentWalletResponse(PaymentechGatewayProfileFetchRequest(
                    new()
                    {
                        version = OrbitalPaymentechConstants.ApiVersion,
                        merchantID = OrbitalPaymentechPaymentsProviderConfig.MerchantID,
                        orbitalConnectionUsername = OrbitalPaymentechPaymentsProviderConfig.Username,
                        orbitalConnectionPassword = OrbitalPaymentechPaymentsProviderConfig.Password,
                        bin = OrbitalPaymentechConstants.Bin,
                        customerRefNum = customerRefNum,
                    },
                    contextProfileName));
                response.CardType = payment.CardType; // Add back to response since Paymentech does not return.
                return response;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(GetCustomerProfileAsync)}.{nameof(Exception)}",
                        $"EXCEPTION: {ex.Message}",
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentWalletResponse
                {
                    Approved = false,
                    ResponseCode = $"EXCEPTION: {ex.Message}",
                };
            }
        }

        /// <inheritdoc/>
        public Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets customer reference number from provider payment.</summary>
        /// <param name="payment">The payment.</param>
        /// <param name="create"> True to create.</param>
        /// <returns>The customer reference number from provider payment.</returns>
        private static string? GetCustomerRefNumFromProviderPayment(IProviderPayment payment, bool create = false)
        {
            return TryParseToken(payment.Token, out var customerRefNum, out _)
                ? customerRefNum
                : create
                    ? ShortGuid()
                    : null;
        }

        /// <summary>Payment new order request element for profile.</summary>
        /// <param name="payment">       The payment.</param>
        /// <param name="customerRefNum">The customer reference number.</param>
        /// <param name="contact">       The contact.</param>
        /// <returns>An OrderRequestElement.</returns>
        private static NewOrderRequestElement PaymentNewOrderRequestElementForProfile(
            IProviderPayment payment,
            string? customerRefNum,
            IContactModel contact)
        {
            var element = PaymentNewOrderRequestElementForAuthorizeZeroAmount(payment, contact);
            element.mitMsgType = OrbitalPaymentechConstants.InitiatedTransactionMessageCodes.ByCustomer.General;
            element.mitStoredCredentialInd = OrbitalPaymentechConstants.StoredCredentialIndicators.OnFile;
            element.profileOrderOverideInd = OrbitalPaymentechConstants.ProfileOverrideIndicators.NoMapping;
            element.customerRefNum = customerRefNum;
            return element;
        }

        /// <summary>Converts a response to a payment wallet response.</summary>
        /// <param name="response">The response.</param>
        /// <returns>Response as an IPaymentWalletResponse.</returns>
        private static IPaymentWalletResponse ToPaymentWalletResponse(NewOrderResponseElement response)
        {
            if (NewOrderResponseIsApproved(response)
                && response.profileProcStatus == OrbitalPaymentechConstants.ProcedureResponseStatuses.Success)
            {
                return new PaymentWalletResponse
                {
                    Approved = true,
                    ResponseCode = $"{response.profileProcStatus}: {response.profileProcStatusMsg}",
                    Token = $"{response.customerRefNum}_{response.mitReceivedTransactionID}",
                };
            }
            if (NewOrderResponseIsDeclined(response) || NewOrderResponseIsError(response))
            {
                return new PaymentWalletResponse
                {
                    Approved = false,
                    ResponseCode = $"Code '{response.respCode}', Message '{response.procStatusMessage}'",
                };
            }
            if (response.profileProcStatus != OrbitalPaymentechConstants.ProcedureResponseStatuses.Success)
            {
                return new PaymentWalletResponse
                {
                    Approved = false,
                    ResponseCode = $"Status '{response.profileProcStatus}', Message '{response.profileProcStatusMsg}'",
                };
            }
            return new PaymentWalletResponse
            {
                Approved = false,
                ResponseCode = $"Status '{response.procStatus}', Message '{response.procStatusMessage}'",
            };
        }

        /// <summary>Converts a response to a payment wallet response.</summary>
        /// <param name="response">The response.</param>
        /// <returns>Response as an IPaymentWalletResponse.</returns>
        private static IPaymentWalletResponse ToPaymentWalletResponse(ProfileResponseElement response)
        {
            return response.procStatus == OrbitalPaymentechConstants.ProcedureResponseStatuses.Success
                ? new()
                {
                    Approved = true,
                    ResponseCode = $"{response.procStatus}: {response.procStatusMessage}",
                    Token = $"{response.customerRefNum}_{response.mitSubmittedTransactionID}",
                }
                : new PaymentWalletResponse
                {
                    Approved = false,
                    ResponseCode = $"{response.procStatus}: {response.procStatusMessage}",
                };
        }

        /// <summary>Paymentech gateway profile add request.</summary>
        /// <param name="addElement">        The add element.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A ProfileResponseElement.</returns>
        // ReSharper disable once UnusedMember.Local
        private ProfileResponseElement PaymentechGatewayProfileAddRequest(
            ProfileAddElement addElement,
            string? contextProfileName)
        {
            return PaymentechGatewayProfileRequest(
                requestType: ProfileRequestType.Add,
                contextProfileName: contextProfileName,
                addElement: addElement);
        }

        /// <summary>Paymentech gateway profile change request.</summary>
        /// <param name="changeElement">     The change element.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A ProfileResponseElement.</returns>
        // ReSharper disable once UnusedMember.Local
        private ProfileResponseElement PaymentechGatewayProfileChangeRequest(
            ProfileChangeElement changeElement,
            string? contextProfileName)
        {
            return PaymentechGatewayProfileRequest(
                requestType: ProfileRequestType.Change,
                contextProfileName: contextProfileName,
                addElement: null,
                changeElement: changeElement);
        }

        /// <summary>Paymentech gateway profile delete request.</summary>
        /// <param name="deleteElement">     The delete element.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A ProfileResponseElement.</returns>
        private ProfileResponseElement PaymentechGatewayProfileDeleteRequest(
            ProfileDeleteElement deleteElement,
            string? contextProfileName)
        {
            return PaymentechGatewayProfileRequest(
                requestType: ProfileRequestType.Delete,
                contextProfileName: contextProfileName,
                addElement: null,
                changeElement: null,
                deleteElement: deleteElement);
        }

        /// <summary>Paymentech gateway profile fetch request.</summary>
        /// <param name="fetchElement">      The fetch element.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A ProfileResponseElement.</returns>
        private ProfileResponseElement PaymentechGatewayProfileFetchRequest(
            ProfileFetchElement fetchElement,
            string? contextProfileName)
        {
            return PaymentechGatewayProfileRequest(
                requestType: ProfileRequestType.Fetch,
                contextProfileName: contextProfileName,
                addElement: null,
                changeElement: null,
                deleteElement: null,
                fetchElement: fetchElement);
        }

        /// <summary>Add Profile to Payment Gateway.</summary>
        /// <param name="requestType">       Type of the request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="addElement">        The add element.</param>
        /// <param name="changeElement">     The change element.</param>
        /// <param name="deleteElement">     The delete element.</param>
        /// <param name="fetchElement">      The fetch element.</param>
        /// <returns>A ProfileResponseElement.</returns>
        private ProfileResponseElement PaymentechGatewayProfileRequest(
            ProfileRequestType requestType,
            string? contextProfileName,
            ProfileAddElement? addElement = null,
            ProfileChangeElement? changeElement = null,
            ProfileDeleteElement? deleteElement = null,
            ProfileFetchElement? fetchElement = null)
        {
            try
            {
                switch (requestType)
                {
                    case ProfileRequestType.Add:
                    {
                        return paymentGatewayClient!.ProfileAdd(addElement);
                    }
                    case ProfileRequestType.Change:
                    {
                        return paymentGatewayClient!.ProfileChange(changeElement);
                    }
                    case ProfileRequestType.Delete:
                    {
                        return paymentGatewayClient!.ProfileDelete(deleteElement);
                    }
                    case ProfileRequestType.Fetch:
                    {
                        return paymentGatewayClient!.ProfileFetch(fetchElement);
                    }
                }
                return new()
                {
                    procStatus = "EXCEPTION",
                    procStatusMessage = "INVALID ProfileRequestType",
                };
            }
            catch (TimeoutException timeProblem)
            {
                Logger.LogError(
                    name: $"{nameof(PaymentechGatewayProfileRequest)}.{nameof(TimeoutException)}",
                    message: $"The service operation timed out. {timeProblem.Message}",
                    ex: timeProblem,
                    contextProfileName: contextProfileName);
                return new()
                {
                    procStatus = "TIMEOUT",
                    procStatusMessage = $"The service operation timed out. {timeProblem.Message}",
                };
            }
            catch (FaultException fault)
            {
                Logger.LogError(
                    name: $"{nameof(PaymentechGatewayProfileRequest)}.{nameof(FaultException)}",
                    message: $"A fault exception was received. {fault.Message}",
                    ex: fault,
                    contextProfileName: contextProfileName);
                return new()
                {
                    procStatus = "FAULT",
                    procStatusMessage = $"A fault exception was received. {fault.Message}",
                };
            }
            catch (CommunicationException comException)
            {
                Logger.LogError(
                    name: $"{nameof(PaymentechGatewayProfileRequest)}.{nameof(CommunicationException)}",
                    message: $"There was a communication problem. {comException.Message}{comException.StackTrace}",
                    ex: comException,
                    contextProfileName: contextProfileName);
                return new()
                {
                    procStatus = "COMEXCEPTION",
                    procStatusMessage = $"There was a communication problem. {comException.Message}{comException.StackTrace}",
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(
                    name: $"{nameof(PaymentechGatewayProfileRequest)}.{nameof(Exception)}",
                    message: ex.Message,
                    ex: ex,
                    contextProfileName: contextProfileName);
                return new()
                {
                    procStatus = "EXCEPTION",
                    procStatusMessage = ex.Message,
                };
            }
        }
    }
}
