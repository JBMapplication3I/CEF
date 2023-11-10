// <copyright file="PayPalPayFlowResponseCodes.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal pay flow response codes class</summary>
// ReSharper disable CommentTypo, StyleCop.SA1602, UnusedMember.Global
#pragma warning disable 1591
namespace Clarity.Ecommerce.Providers.Payments.PayPalPayflowPro
{
    using System.ComponentModel;

    /// <summary>Values that represent PayPal pay flow response codes.</summary>
    public enum PayPalPayFlowResponseCodes
    {
        [Description("Unable to round and truncate the currency value simultaneously")]
        UnableToRoundAndTruncateTheCurrencyValueSimultaneously = -113,

        [Description("The following error occurred while initializing from message file: <Details of the error message>")]
        TheFollowingErrorOccurredWhileInitializingFromMessageFile = -111,

        [Description("Unable to do logging")]
        UnableToDoLogging = -109,

        [Description("The server certificate chain did not validate")]
        TheServerCertificateChainDidNotValidate = -108,

        [Description("This XMLPay version is not supported")]
        ThisXMLPayVersionIsNotSupported = -107,

        [Description("Invalid response format")]
        InvalidResponseFormat = -106,

        [Description("Invalid name value pair request")]
        InvalidNameValuePairRequest = -105,

        [Description("Unexpected transaction state")]
        UnexpectedTransactionState = -104,

        [Description("Context initialization failed")]
        ContextInitializationFailed = -103,

        [Description("Parameter list cannot be empty")]
        ParameterListCannotBeEmpty = -100,

        [Description("Out of memory")]
        OutOfMemory = -99,

        [Description("Required Request ID not found in request")]
        RequiredRequestIDNotFoundInRequest = -41,

        [Description("Unexpected Request ID found in request")]
        UnexpectedRequestIDFoundInRequest = -40,

        [Description("The certificate chain did not validate, common name did not match URL")]
        TheCertificateChainDidNotValidateCommonNameDidNotMatchURL = -32,

        [Description("The certificate chain did not validate, no local certificate found")]
        TheCertificateChainDidNotValidateNoLocalCertificateFound = -31,

        [Description("Invalid timeout value")]
        InvalidTimeoutValue = -30,

        [Description("Failed to initialize SSL connection")]
        FailedToInitializeSSLConnection = -29,

        [Description("Parameter list format error: name")]
        ParameterListFormatErrorName = -28,

        [Description("Parameter list format error: invalid [] name length clause")]
        ParameterListFormatErrorInvalidArrayNameLengthClause1 = -27,

        [Description("Failed to initialize socket layer")]
        FailedToInitializeSocketLayer = -26,

        [Description("Failed to create a socket")]
        FailedToCreateASocket = -25,

        [Description("Invalid transaction type")]
        InvalidTransactionType1 = -24,

        [Description("Host address not specified")]
        HostAddressNotSpecified = -23,

        [Description("Failed to initialize SSL certificate")]
        FailedToInitializeSSLCertificate = -22,

        [Description("Proxy write failed")]
        ProxyWriteFailed = -21,

        [Description("Proxy read failed")]
        ProxyReadFailed = -20,

        [Description("Failed to set socket options")]
        FailedToSetSocketOptions = -15,

        [Description("Too many connections")]
        TooManyConnections = -14,

        [Description("Select failure")]
        SelectFailure = -13,

        [Description("Timeout waiting for response")]
        TimeoutWaitingForResponse = -12,

        [Description("Proxy authorization failed")]
        ProxyAuthorizationFailed = -11,

        [Description("SSL write failed")]
        SSLWriteFailed = -10,

        [Description("SSL read failed")]
        SSLReadFailed = -9,

        [Description("SSL failed to connect to host")]
        SSLFailedToConnectToHost = -8,

        [Description("Parameter list format error: invalid [] name length clause")]
        ParameterListFormatErrorInvalidArrayNameLengthClause2 = -7,

        [Description("Parameter list format error: & in name")]
        ParameterListFormatErrorAmpersandInName = -6,

        [Description("Failed to initialize SSL context")]
        FailedToInitializeSSLContext = -5,

        [Description("Failed to resolve hostname")]
        FailedToResolveHostname = -2,

        [Description("Failed to connect to host")]
        FailedToConnectToHost = -1,

        [Description("Approved: Note: PayPal processor: Warning information may be returned that may be useful to the request application. See the API error codes on the PayPal developer website for information on corrective actions.")]
        Approved = 0,

        [Description("User authentication failed. Error is caused by one or more of the following:\r\n* Login information is incorrect. Verify that USER, VENDOR, PARTNER, and PASSWORD have been entered correctly. VENDOR is your merchant ID and USER is the same as VENDOR unless you created a Payflow Pro user. All fields are case sensitive.\r\n* Invalid Processor information entered. Contact merchant bank to verify.\r\n* \"Allowed IP Address\" security feature implemented. The transaction is coming from an unknown IP address. See PayPal Manager online help for details on how to update the allowed IP addresses.\r\n* You are using a test (not active) account to submit a transaction to the live PayPal servers. Change the host address from the test server URL to the live server URL")]
        UserAuthenticationFailed = 1,

        [Description("Invalid tender type. Your merchant bank account does not support the following credit card type that was submitted.")]
        InvalidTenderType = 2,

        [Description("Invalid transaction type. Transaction type is not appropriate for this transaction. For example, you cannot credit an authorization-only transaction")]
        InvalidTransactionType2 = 3,

        [Description("Invalid amount format. Use the format: \"### ##.##\" Do not include currency symbols or commas.")]
        InvalidAmountFormat = 4,

        [Description("Invalid merchant information. Processor does not recognize your merchant account information. Contact your bank account acquirer to resolve this problem.")]
        InvalidMerchantInformation = 5,

        [Description("Invalid or unsupported currency code")]
        InvalidOrUnsupportedCurrencyCode = 6,

        [Description("Field format error. Invalid information entered. See RESPMSG.")]
        FieldFormatError = 7,

        [Description("Not a transaction server")]
        NotATransactionServer = 8,

        [Description("Too many parameters or invalid stream")]
        TooManyParametersOrInvalidStream = 9,

        [Description("Too many line items")]
        TooManyLineItems = 10,

        [Description("Client time-out waiting for response")]
        ClientTimeOutWaitingForResponse = 11,

        [Description("Declined. Check the credit card number, expiration date, and transaction information to make sure they were entered correctly. If this does not resolve the problem, have the customer call their card issuing bank to resolve.")]
        Declined = 12,

        [Description("Referral. Transaction cannot be approved electronically but can be approved with a verbal authorization. Contact your merchant bank to obtain an authorization and submit a manual Voice Authorization transaction.")]
        Referral = 13,

        [Description("Original transaction ID not found. The transaction ID you entered for this transaction is not valid. See RESPMSG.")]
        OriginalTransactionIDNotFound = 19,

        [Description("Cannot find the customer reference number")]
        CannotFindTheCustomerReferenceNumber = 20,

        [Description("Invalid ABA number")]
        InvalidABANumber = 22,

        [Description("Invalid account number. Check credit card number and bank account number and re-submit.")]
        InvalidAccountNumber = 23,

        [Description("Invalid expiration date. Check card expiration date and re-submit.")]
        InvalidExpirationDate = 24,

        [Description("Invalid Host Mapping. Error is caused by one or more of the following:\r\n* You are trying to process a tender type such as Discover Card, but you are not set up with your merchant bank to accept this card type.\r\n* You are trying to process an Express Checkout transaction when your account is not set up to do so. Contact your account holder to have Express Checkout added to your account.")]
        InvalidHostMapping = 25,

        [Description("Invalid vendor account. Login information is incorrect. Verify that USER, VENDOR, PARTNER, and PASSWORD have been entered correctly. VENDOR is your merchant ID and USER is the same as VENDOR unless you created a Payflow Pro user. All fields are case sensitive.")]
        InvalidVendorAccount = 26,

        [Description("Insufficient partner permissions")]
        InsufficientPartnerPermissions = 27,

        [Description("Insufficient user permissions")]
        InsufficientUserPermissions = 28,

        [Description("Invalid XML document. This could be caused by an unrecognized XML tag or a bad XML format that cannot be parsed by the system.")]
        InvalidXMLDocument = 29,

        [Description("Duplicate transaction")]
        DuplicateTransaction = 30,

        [Description("Error in adding the recurring profile")]
        ErrorInAddingTheRecurringProfile = 31,

        [Description("Error in modifying the recurring profile")]
        ErrorInModifyingTheRecurringProfile = 32,

        [Description("Error in canceling the recurring profile")]
        ErrorInCancelingTheRecurringProfile = 33,

        [Description("Error in forcing the recurring profile")]
        ErrorInForcingTheRecurringProfile = 34,

        [Description("Error in reactivating the recurring profile")]
        ErrorInReactivatingTheRecurringProfile = 35,

        [Description("OLTP Transaction failed")]
        OLTPTransactionFailed = 36,

        [Description("Invalid recurring profile ID")]
        InvalidRecurringProfileID = 37,

        [Description("Insufficient funds available in account")]
        InsufficientFundsAvailableInAccount = 50,

        [Description("Exceeds per transaction limit")]
        ExceedsPerTransactionLimit = 51,

        [Description("Permission issue. Attempting to perform a transaction type, such as Sale or Authorization, that is not allowed for this account.")]
        PermissionIssue = 52,

        [Description("General error. See RESPMSG.")]
        GeneralError = 99,

        [Description("Transaction type not supported by host")]
        TransactionTypeNotSupportedByHost = 100,

        [Description("Time-out value too small. PayPal Australia: Invalid transaction returned from host (can be treated as an invalid transaction or a decline)")]
        TimeOutValueTooSmall = 101,

        [Description("Processor not available. The financial host was unable to communicate with the external processor. These transactions do not make it out of the Payflow network and will not settle or appear on processor reports.")]
        ProcessorNotAvailable = 102,

        [Description("Error reading response from host. The financial host could not interpret the response from the processor. This error can result in an uncaptured authorization if the transaction is an authorization or a sale, except on the following processors:\r\n* PayPal Australia: Time-out reversal logic should reverse the transaction. According to PayPal Australia, this is a best effort and is not guaranteed.\r\n* Global Payments Central, Citi Bank Singapore: Time-out reversal logic should reverse the transaction.\r\n* PayPal: The transaction might settle. There is no time-out reversal process on PayPal.")]
        ErrorReadingResponseFromHost = 103,

        [Description("Timeout waiting for processor response. Try your transaction again. The Payflow gateway sent the transaction to the processor, but the processor did not respond in the allotted time. This error can result in an uncaptured authorization if the transaction is an authorization or a sale, except on the following processors:\r\n* PayPal Australia: Time-out reversal logic should reverse the transaction. According to PayPal Australia, this is a best effort and is not guaranteed.\r\n* Global Payments Central, Citi Bank Singapore: Time-out reversal logic should reverse the transaction.\r\n* PayPal: The transaction might settle. There is no time-out reversal process on PayPal.")]
        TimeoutWaitingForProcessorResponse = 104,

        [Description("Credit error. Make sure you have not already credited this transaction, or that this transaction ID is for a creditable transaction. (For example, you cannot credit an authorization.)")]
        CreditError = 105,

        [Description("Host not available. The Payflow transaction server was unable to communicate with the financial host. This error is an internal failure, and the transaction will not make it to the processor.")]
        HostNotAvailable = 106,

        [Description("Duplicate suppression time-out")]
        DuplicateSuppressionTimeOut = 107,

        [Description("Void error. RESPMSG. Make sure the transaction ID entered has not already been voided. If not, then look at the Transaction Detail screen for this transaction to see if it has settled. (The Batch field is set to a number greater than zero if the transaction has been settled). If the transaction has already settled, your only recourse is a reversal (credit a payment or submit a payment for a credit)")]
        VoidError = 108,

        [Description("Time-out waiting for host response. The Payflow transaction server times out waiting for a financial host to respond. This error can result in uncaptured authorizations on all platforms, or settled sales on PayPal Australia, Global Payments Central, and PayPal.")]
        TimeOutWaitingForHostResponse = 109,

        [Description("Referenced auth (against order) Error")]
        ReferencedAuthAgainstOrderError = 110,

        [Description("Capture error. The attempt to capture failed. This may be that the transaction is not an authorization, the attempt to capture an authorization transaction has already been completed, or the amount of the capture is above the allowable limit of the authorization transaction.")]
        CaptureError = 111,

        [Description("Failed AVS check. Address and zip code do not match. An authorization may still exist on the cardholder's account.")]
        FailedAVSCheck = 112,

        [Description("Merchant sale total will exceed the sales cap with current transaction. ACH transactions only.")]
        MerchantSaleTotalWillExceedTheSalesCapWithCurrentTransaction = 113,

        [Description("Card Security Code (CSC) Mismatch. An authorization may still exist on the cardholder's account.")]
        CardSecurityCodeCSCMismatch = 114,

        [Description("System busy, try again later")]
        SystemBusy = 115,

        [Description("Failed to lock terminal number. Please try again later. For Moneris Solutions, another transaction or settlement is in process. Rerun the transaction later.")]
        FailedToLockTerminalNumber = 116,

        [Description("Failed merchant rule check. One or more of the following three failures occurred:\r\n* An attempt was made to submit a transaction that failed to meet the security settings specified on the PayPal Manager Security Settings page. If the transaction exceeded the Maximum Amount security setting, then no values are returned for AVS or CSC.\r\n* AVS validation failed. The AVS return value should appear in the RESPMSG.\r\n* CSC validation failed. The CSC return value should appear in the RESPMSG.")]
        FailedMerchantRuleCheck = 117,

        [Description("Invalid keywords found in string fields")]
        InvalidKeywordsFoundInStringFields = 118,

        [Description("Attempt to reference a failed transaction")]
        AttemptToReferenceAFailedTransaction = 120,

        [Description("Not enabled for feature")]
        NotEnabledForFeature = 121,

        [Description("Merchant sale total will exceed the credit cap with current transaction. ACH transactions only.")]
        MerchantSaleTotalWillExceedTheCreditCapWithCurrentTransaction = 122,

        [Description("Fraud Protection Services Filter - Declined by filters")]
        FraudProtectionServicesFilterDeclinedByFilters = 125,

        [Description("Fraud Protection Services Filter - Flagged for review by filters Note: RESULT value 126 indicates that a transaction triggered a fraud filter. This is not an error, but a notice that the transaction is in a review status. The transaction has been authorized but requires you to review and to manually accept the transaction before it will be allowed to settle. RESULT value 126 is intended to give you an idea of the kind of transaction that is considered suspicious to enable you to evaluate whether you can benefit from using the Fraud Protection Services. To eliminate RESULT 126, turn the filters off. For more information, see the fraud documentation for your payments solution.")]
        FraudProtectionServicesFilterFlaggedForReviewByFilters = 126,

        [Description("Fraud Protection Services Filter - Not processed by filters")]
        FraudProtectionServicesFilterNotProcessedByFilters = 127,

        [Description("Fraud Protection Services Filter - Declined by merchant after being flagged for review by filters")]
        FraudProtectionServicesFilterDeclinedByMerchantAfterBeingFlaggedForReviewByFilters = 128,

        [Description("Card has not been submitted for update")]
        CardHasNotBeenSubmittedForUpdate = 132,

        [Description("Data mismatch in HTTP retry request")]
        DataMismatchInHTTPRetryRequest = 133,

        [Description("Issuing bank timed out. PayPal Australia reported a timeout between their system and the bank. This error will be reversed by time-out reversal. According to PayPal Australia, this is a best effort and is not guaranteed.")]
        IssuingBankTimedOut = 150,

        [Description("Issuing bank unavailable")]
        IssuingBankUnavailable = 151,

        [Description("Secure Token already been used. Indicates that the secure token has expired due to either a successful transaction or the token has been used three times while trying to successfully process a transaction. You must generate a new secure token.")]
        SecureTokenAlreadyBeenUsed = 160,

        [Description("Transaction using secure token is already in progress. This could occur if a customer hits the submit button two or more times before the transaction completed.")]
        TransactionUsingSecureTokenIsAlreadyInProgress = 161,

        [Description("Secure Token Expired. The time limit of 20 minutes has expired and the token can no longer be used.")]
        SecureTokenExpired = 162,

        [Description("Carding Error. Carding or fraudulent activity was found on the account and the account is temporarily suspended until issue is resolved.")]
        CardingError = 0170,

        [Description("Reauth error")]
        ReauthError = 200,

        [Description("Order error")]
        OrderError = 201,

        [Description("Cybercash Batch Error")]
        CybercashBatchError = 600,

        [Description("Cybercash Query Error")]
        CybercashQueryError = 601,

        [Description("Generic host error. This is a generic message returned by your credit card processor. The RESPMSG will contain more information describing the error.")]
        GenericHostError = 1000,

        [Description("Buyer Authentication Service unavailable")]
        BuyerAuthenticationServiceUnavailable1 = 1001,

        [Description("Buyer Authentication Service - Transaction timeout")]
        BuyerAuthenticationServiceTransactionTimeout = 1002,

        [Description("Buyer Authentication Service - Invalid client version")]
        BuyerAuthenticationServiceInvalidClientVersion = 1003,

        [Description("Buyer Authentication Service - Invalid timeout value")]
        BuyerAuthenticationServiceInvalidTimeoutValue = 1004,

        [Description("Buyer Authentication Service unavailable")]
        BuyerAuthenticationServiceUnavailable2 = 1011,

        [Description("Buyer Authentication Service unavailable")]
        BuyerAuthenticationServiceUnavailable3 = 1012,

        [Description("Buyer Authentication Service unavailable")]
        BuyerAuthenticationServiceUnavailable4 = 1013,

        [Description("Buyer Authentication Service - Merchant is not enrolled for Buyer Authentication Service (3-D Secure)")]
        BuyerAuthenticationServiceMerchantIsNotEnrolledForBuyerAuthenticationService = 1014,

        [Description("Buyer Authentication Service - 3-D Secure error response received. Instead of receiving a PARes response to a Validate Authentication transaction, an error response was received.")]
        BuyerAuthenticationService3DSecureErrorResponseReceived = 1016,

        [Description("Buyer Authentication Service - 3-D Secure error response is invalid. An error response is received and the response is not well formed for a Validate Authentication transaction.")]
        BuyerAuthenticationService3DSecureErrorResponseIsInvalid = 1017,

        [Description("Buyer Authentication Service - Invalid card type")]
        BuyerAuthenticationServiceInvalidCardType = 1021,

        [Description("Buyer Authentication Service - Invalid or missing currency code")]
        BuyerAuthenticationServiceInvalidOrMissingCurrencyCode = 1022,

        [Description("Buyer Authentication Service - merchant status for 3D secure is invalid")]
        BuyerAuthenticationServiceMerchantStatusFor3DSecureIsInvalid = 1023,

        [Description("Buyer Authentication Service - Validate Authentication failed: missing or invalid PARES")]
        BuyerAuthenticationServiceValidateAuthenticationFailedMissingOrInvalidPARES = 1041,

        [Description("Buyer Authentication Service - Validate Authentication failed: PARES format is invalid")]
        BuyerAuthenticationServiceValidateAuthenticationFailedPARESFormatIsInvalid = 1042,

        [Description("Buyer Authentication Service - Validate Authentication failed: Cannot find successful Verify Enrollment")]
        BuyerAuthenticationServiceValidateAuthenticationFailedCannotFindSuccessfulVerifyEnrollment = 1043,

        [Description("Buyer Authentication Service - Validate Authentication failed: Signature validation failed for PARES")]
        BuyerAuthenticationServiceValidateAuthenticationFailedSignatureValidationFailedForPARES = 1044,

        [Description("Buyer Authentication Service - Validate Authentication failed: Mismatched or invalid amount in PARES")]
        BuyerAuthenticationServiceValidateAuthenticationFailedMismatchedOrInvalidAmountInPARES = 1045,

        [Description("Buyer Authentication Service - Validate Authentication failed: Mismatched or invalid acquirer in PARES")]
        BuyerAuthenticationServiceValidateAuthenticationFailedMismatchedOrInvalidAcquirerInPARES = 1046,

        [Description("Buyer Authentication Service - Validate Authentication failed: Mismatched or invalid Merchant ID in PARES")]
        BuyerAuthenticationServiceValidateAuthenticationFailedMismatchedOrInvalidMerchantIDinPARES = 1047,

        [Description("Buyer Authentication Service - Validate Authentication failed: Mismatched or invalid card number in PARES")]
        BuyerAuthenticationServiceValidateAuthenticationFailedMismatchedOrInvalidCardNumberInPARES = 1048,

        /// <summary>An enum constant representing the buyer authentication service validate authentication failed
        /// mismatched or invalid currency code in pares option.</summary>
        [Description("Buyer Authentication Service - Validate Authentication failed: Mismatched or invalid currency code in PARES")]
        BuyerAuthenticationServiceValidateAuthenticationFailedMismatchedOrInvalidCurrencyCodeInPARES = 1049,

        /// <summary>An enum constant representing the buyer authentication service validate authentication failed
        /// mismatched or invalid xi din pares option.</summary>
        [Description("Buyer Authentication Service - Validate Authentication failed: Mismatched or invalid XID in PARES")]
        BuyerAuthenticationServiceValidateAuthenticationFailedMismatchedOrInvalidXIDinPARES = 1050,

        /// <summary>An enum constant representing the buyer authentication service validate authentication failed
        /// mismatched or invalid order date in pares option.</summary>
        [Description("Buyer Authentication Service - Validate Authentication failed: Mismatched or invalid order date in PARES")]
        BuyerAuthenticationServiceValidateAuthenticationFailedMismatchedOrInvalidOrderDateInPARES = 1051,

        /// <summary>An enum constant representing the buyer authentication service validate authentication failed option.</summary>
        [Description("Buyer Authentication Service - Validate Authentication failed: This PARES was already validated for a previous Validate Authentication transaction")]
        BuyerAuthenticationServiceValidateAuthenticationFailed = 1052,
    }
}
