#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK
{
    /// <summary>
    /// ORBResp
    /// </summary>
    public class ORBResp
    {
        public ORBResp()
        {
        }

        public class AccountUpdaterResp
        {
            public static string customerBin;

            public static string customerMerchantID;

            public static string CustomerRefNum;

            public static string CustomerProfileAction;

            public static string Status;

            public static string ScheduledDate;

            public static string ProfileProcStatus;

            public static string CustomerProfileMessage;

            public static string RespTime;

            static AccountUpdaterResp()
            {
                ORBResp.AccountUpdaterResp.customerBin = "customerBin";
                ORBResp.AccountUpdaterResp.customerMerchantID = "customerMerchantID";
                ORBResp.AccountUpdaterResp.CustomerRefNum = "CustomerRefNum";
                ORBResp.AccountUpdaterResp.CustomerProfileAction = "CustomerProfileAction";
                ORBResp.AccountUpdaterResp.Status = "Status";
                ORBResp.AccountUpdaterResp.ScheduledDate = "ScheduledDate";
                ORBResp.AccountUpdaterResp.ProfileProcStatus = "ProfileProcStatus";
                ORBResp.AccountUpdaterResp.CustomerProfileMessage = "CustomerProfileMessage";
                ORBResp.AccountUpdaterResp.RespTime = "RespTime";
            }

            public AccountUpdaterResp()
            {
            }
        }

        public class EndOfDayResp
        {
            public static string MerchantID;

            public static string TerminalID;

            public static string BatchSeqNum;

            public static string ProcStatus;

            public static string StatusMsg;

            public static string RespTime;

            static EndOfDayResp()
            {
                ORBResp.EndOfDayResp.MerchantID = "MerchantID";
                ORBResp.EndOfDayResp.TerminalID = "TerminalID";
                ORBResp.EndOfDayResp.BatchSeqNum = "BatchSeqNum";
                ORBResp.EndOfDayResp.ProcStatus = "ProcStatus";
                ORBResp.EndOfDayResp.StatusMsg = "StatusMsg";
                ORBResp.EndOfDayResp.RespTime = "RespTime";
            }

            public EndOfDayResp()
            {
            }
        }

        public class FlexCacheResp
        {
            public static string MerchantID;

            public static string TerminalID;

            public static string OrderID;

            public static string BatchFailedAcctNum;

            public static string FlexRequestedAmount;

            public static string FlexRedeemedAmt;

            public static string FlexHostTrace;

            public static string FlexAction;

            public static string FlexAcctBalance;

            public static string FlexAcctPriorBalance;

            public static string FlexAcctExpireDate;

            public static string CardBrand;

            public static string TxRefNum;

            public static string TxRefIdx;

            public static string ProcStatus;

            public static string StatusMsg;

            public static string ApprovalStatus;

            public static string AuthCode;

            public static string RespCode;

            public static string CVV2RespCode;

            public static string AutoAuthTxRefIdx;

            public static string AutoAuthTxRefNum;

            public static string AutoAuthProcStatus;

            public static string AutoAuthStatusMsg;

            public static string AutoAuthApprovalStatus;

            public static string AutoAuthFlexRedeemedAmt;

            public static string AutoAuthAuthCode;

            public static string AutoAuthRespCode;

            public static string AutoAuthFlexHostTrace;

            public static string AutoAuthFlexAction;

            public static string AutoAuthRespTime;

            public static string RespTime;

            public static string FraudScoreProcStatus;

            public static string FraudScoreProcMsg;

            public static string FraudAnalysisResponse;

            static FlexCacheResp()
            {
                ORBResp.FlexCacheResp.MerchantID = "MerchantID";
                ORBResp.FlexCacheResp.TerminalID = "TerminalID";
                ORBResp.FlexCacheResp.OrderID = "OrderID";
                ORBResp.FlexCacheResp.BatchFailedAcctNum = "BatchFailedAcctNum";
                ORBResp.FlexCacheResp.FlexRequestedAmount = "FlexRequestedAmount";
                ORBResp.FlexCacheResp.FlexRedeemedAmt = "FlexRedeemedAmt";
                ORBResp.FlexCacheResp.FlexHostTrace = "FlexHostTrace";
                ORBResp.FlexCacheResp.FlexAction = "FlexAction";
                ORBResp.FlexCacheResp.FlexAcctBalance = "FlexAcctBalance";
                ORBResp.FlexCacheResp.FlexAcctPriorBalance = "FlexAcctPriorBalance";
                ORBResp.FlexCacheResp.FlexAcctExpireDate = "FlexAcctExpireDate";
                ORBResp.FlexCacheResp.CardBrand = "CardBrand";
                ORBResp.FlexCacheResp.TxRefNum = "TxRefNum";
                ORBResp.FlexCacheResp.TxRefIdx = "TxRefIdx";
                ORBResp.FlexCacheResp.ProcStatus = "ProcStatus";
                ORBResp.FlexCacheResp.StatusMsg = "StatusMsg";
                ORBResp.FlexCacheResp.ApprovalStatus = "ApprovalStatus";
                ORBResp.FlexCacheResp.AuthCode = "AuthCode";
                ORBResp.FlexCacheResp.RespCode = "RespCode";
                ORBResp.FlexCacheResp.CVV2RespCode = "CVV2RespCode";
                ORBResp.FlexCacheResp.AutoAuthTxRefIdx = "AutoAuthTxRefIdx";
                ORBResp.FlexCacheResp.AutoAuthTxRefNum = "AutoAuthTxRefNum";
                ORBResp.FlexCacheResp.AutoAuthProcStatus = "AutoAuthProcStatus";
                ORBResp.FlexCacheResp.AutoAuthStatusMsg = "AutoAuthStatusMsg";
                ORBResp.FlexCacheResp.AutoAuthApprovalStatus = "AutoAuthApprovalStatus";
                ORBResp.FlexCacheResp.AutoAuthFlexRedeemedAmt = "AutoAuthFlexRedeemedAmt";
                ORBResp.FlexCacheResp.AutoAuthAuthCode = "AutoAuthAuthCode";
                ORBResp.FlexCacheResp.AutoAuthRespCode = "AutoAuthRespCode";
                ORBResp.FlexCacheResp.AutoAuthFlexHostTrace = "AutoAuthFlexHostTrace";
                ORBResp.FlexCacheResp.AutoAuthFlexAction = "AutoAuthFlexAction";
                ORBResp.FlexCacheResp.AutoAuthRespTime = "AutoAuthRespTime";
                ORBResp.FlexCacheResp.RespTime = "RespTime";
                ORBResp.FlexCacheResp.FraudScoreProcStatus = "FraudScoreProcStatus";
                ORBResp.FlexCacheResp.FraudScoreProcMsg = "FraudScoreProcMsg";
                ORBResp.FlexCacheResp.FraudAnalysisResponse = "FraudAnalysisResponse";
            }

            public FlexCacheResp()
            {
            }
        }

        public class InquiryResp
        {
            public static string IndustryType;

            public static string MessageType;

            public static string MerchantID;

            public static string TerminalID;

            public static string CardBrand;

            public static string AccountNum;

            public static string OrderID;

            public static string TxRefNum;

            public static string TxRefIdx;

            public static string ProcStatus;

            public static string ApprovalStatus;

            public static string RespCode;

            public static string AVSRespCode;

            public static string CVV2RespCode;

            public static string AuthCode;

            public static string RecurringAdviceCd;

            public static string CAVVRespCode;

            public static string StatusMsg;

            public static string RespMsg;

            public static string HostRespCode;

            public static string HostAVSRespCode;

            public static string HostCVV2RespCode;

            public static string CustomerRefNum;

            public static string CustomerName;

            public static string ProfileProcStatus;

            public static string CustomerProfileMessage;

            public static string BillerReferenceNumber;

            public static string MBStatus;

            public static string MBMicroPaymentDaysLeft;

            public static string MBMicroPaymentDollarsLeft;

            public static string RespTime;

            public static string PartialAuthOccurred;

            public static string RequestedAmount;

            public static string RedeemedAmount;

            public static string RemainingBalance;

            public static string CountryFraudFilterStatus;

            public static string IsoCountryCode;

            public static string FraudScoreProcStatus;

            public static string FraudScoreProcMsg;

            public static string FraudAnalysisResponse;

            public static string CTIAffluentCard;

            public static string CTICommercialCard;

            public static string CTIDurbinExemption;

            public static string CTIHealthcareCard;

            public static string CTILevel3Eligible;

            public static string CTIPayrollCard;

            public static string CTIPrepaidCard;

            public static string CTIPINlessDebitCard;

            public static string CTISignatureDebitCard;

            public static string CTIIssuingCountry;

            public static string EUDDCountryCode;

            public static string EUDDBankSortCode;

            public static string EUDDRibCode;

            public static string EUDDBankBranchCode;

            public static string EUDDIBAN;

            public static string EUDDBIC;

            public static string EUDDMandateSignatureDate;

            public static string EUDDMandateID;

            public static string EUDDMandateType;

            public static string TokenAssuranceLevel;

            public static string DPANAccountStatus;

            static InquiryResp()
            {
                ORBResp.InquiryResp.IndustryType = "IndustryType";
                ORBResp.InquiryResp.MessageType = "MessageType";
                ORBResp.InquiryResp.MerchantID = "MerchantID";
                ORBResp.InquiryResp.TerminalID = "TerminalID";
                ORBResp.InquiryResp.CardBrand = "CardBrand";
                ORBResp.InquiryResp.AccountNum = "AccountNum";
                ORBResp.InquiryResp.OrderID = "OrderID";
                ORBResp.InquiryResp.TxRefNum = "TxRefNum";
                ORBResp.InquiryResp.TxRefIdx = "TxRefIdx";
                ORBResp.InquiryResp.ProcStatus = "ProcStatus";
                ORBResp.InquiryResp.ApprovalStatus = "ApprovalStatus";
                ORBResp.InquiryResp.RespCode = "RespCode";
                ORBResp.InquiryResp.AVSRespCode = "AVSRespCode";
                ORBResp.InquiryResp.CVV2RespCode = "CVV2RespCode";
                ORBResp.InquiryResp.AuthCode = "AuthCode";
                ORBResp.InquiryResp.RecurringAdviceCd = "RecurringAdviceCd";
                ORBResp.InquiryResp.CAVVRespCode = "CAVVRespCode";
                ORBResp.InquiryResp.StatusMsg = "StatusMsg";
                ORBResp.InquiryResp.RespMsg = "RespMsg";
                ORBResp.InquiryResp.HostRespCode = "HostRespCode";
                ORBResp.InquiryResp.HostAVSRespCode = "HostAVSRespCode";
                ORBResp.InquiryResp.HostCVV2RespCode = "HostCVV2RespCode";
                ORBResp.InquiryResp.CustomerRefNum = "CustomerRefNum";
                ORBResp.InquiryResp.CustomerName = "CustomerName";
                ORBResp.InquiryResp.ProfileProcStatus = "ProfileProcStatus";
                ORBResp.InquiryResp.CustomerProfileMessage = "CustomerProfileMessage";
                ORBResp.InquiryResp.BillerReferenceNumber = "BillerReferenceNumber";
                ORBResp.InquiryResp.MBStatus = "MBStatus";
                ORBResp.InquiryResp.MBMicroPaymentDaysLeft = "MBMicroPaymentDaysLeft";
                ORBResp.InquiryResp.MBMicroPaymentDollarsLeft = "MBMicroPaymentDollarsLeft";
                ORBResp.InquiryResp.RespTime = "RespTime";
                ORBResp.InquiryResp.PartialAuthOccurred = "PartialAuthOccurred";
                ORBResp.InquiryResp.RequestedAmount = "RequestedAmount";
                ORBResp.InquiryResp.RedeemedAmount = "RedeemedAmount";
                ORBResp.InquiryResp.RemainingBalance = "RemainingBalance";
                ORBResp.InquiryResp.CountryFraudFilterStatus = "CountryFraudFilterStatus";
                ORBResp.InquiryResp.IsoCountryCode = "IsoCountryCode";
                ORBResp.InquiryResp.FraudScoreProcStatus = "FraudScoreProcStatus";
                ORBResp.InquiryResp.FraudScoreProcMsg = "FraudScoreProcMsg";
                ORBResp.InquiryResp.FraudAnalysisResponse = "FraudAnalysisResponse";
                ORBResp.InquiryResp.CTIAffluentCard = "CTIAffluentCard";
                ORBResp.InquiryResp.CTICommercialCard = "CTICommercialCard";
                ORBResp.InquiryResp.CTIDurbinExemption = "CTIDurbinExemption";
                ORBResp.InquiryResp.CTIHealthcareCard = "CTIHealthcareCard";
                ORBResp.InquiryResp.CTILevel3Eligible = "CTILevel3Eligible";
                ORBResp.InquiryResp.CTIPayrollCard = "CTIPayrollCard";
                ORBResp.InquiryResp.CTIPrepaidCard = "CTIPrepaidCard";
                ORBResp.InquiryResp.CTIPINlessDebitCard = "CTIPINlessDebitCard";
                ORBResp.InquiryResp.CTISignatureDebitCard = "CTISignatureDebitCard";
                ORBResp.InquiryResp.CTIIssuingCountry = "CTIIssuingCountry";
                ORBResp.InquiryResp.EUDDCountryCode = "EUDDCountryCode";
                ORBResp.InquiryResp.EUDDBankSortCode = "EUDDBankSortCode";
                ORBResp.InquiryResp.EUDDRibCode = "EUDDRibCode";
                ORBResp.InquiryResp.EUDDBankBranchCode = "EUDDBankBranchCode";
                ORBResp.InquiryResp.EUDDIBAN = "EUDDIBAN";
                ORBResp.InquiryResp.EUDDBIC = "EUDDBIC";
                ORBResp.InquiryResp.EUDDMandateSignatureDate = "EUDDMandateSignatureDate";
                ORBResp.InquiryResp.EUDDMandateID = "EUDDMandateID";
                ORBResp.InquiryResp.EUDDMandateType = "EUDDMandateType";
                ORBResp.InquiryResp.TokenAssuranceLevel = "TokenAssuranceLevel";
                ORBResp.InquiryResp.DPANAccountStatus = "DPANAccountStatus";
            }

            public InquiryResp()
            {
            }
        }

        public class MarkForCaptureResp
        {
            public static string MerchantID;

            public static string TerminalID;

            public static string OrderID;

            public static string TxRefNum;

            public static string TxRefIdx;

            public static string Amount;

            public static string ProcStatus;

            public static string StatusMsg;

            public static string RespTime;

            public static string ApprovalStatus;

            public static string RespCode;

            public static string AVSRespCode;

            public static string AuthCode;

            public static string RespMsg;

            public static string HostRespCode;

            public static string HostAVSRespCode;

            public static string TxnSurchargeAmt;

            public static string TokenAssuranceLevel;

            public static string DPANAccountStatus;

            public static string MITReceivedTransactionID;

            static MarkForCaptureResp()
            {
                ORBResp.MarkForCaptureResp.MerchantID = "MerchantID";
                ORBResp.MarkForCaptureResp.TerminalID = "TerminalID";
                ORBResp.MarkForCaptureResp.OrderID = "OrderID";
                ORBResp.MarkForCaptureResp.TxRefNum = "TxRefNum";
                ORBResp.MarkForCaptureResp.TxRefIdx = "TxRefIdx";
                ORBResp.MarkForCaptureResp.Amount = "Amount";
                ORBResp.MarkForCaptureResp.ProcStatus = "ProcStatus";
                ORBResp.MarkForCaptureResp.StatusMsg = "StatusMsg";
                ORBResp.MarkForCaptureResp.RespTime = "RespTime";
                ORBResp.MarkForCaptureResp.ApprovalStatus = "ApprovalStatus";
                ORBResp.MarkForCaptureResp.RespCode = "RespCode";
                ORBResp.MarkForCaptureResp.AVSRespCode = "AVSRespCode";
                ORBResp.MarkForCaptureResp.AuthCode = "AuthCode";
                ORBResp.MarkForCaptureResp.RespMsg = "RespMsg";
                ORBResp.MarkForCaptureResp.HostRespCode = "HostRespCode";
                ORBResp.MarkForCaptureResp.HostAVSRespCode = "HostAVSRespCode";
                ORBResp.MarkForCaptureResp.TxnSurchargeAmt = "TxnSurchargeAmt";
                ORBResp.MarkForCaptureResp.TokenAssuranceLevel = "TokenAssuranceLevel";
                ORBResp.MarkForCaptureResp.DPANAccountStatus = "DPANAccountStatus";
                ORBResp.MarkForCaptureResp.MITReceivedTransactionID = "MITReceivedTransactionID";
            }

            public MarkForCaptureResp()
            {
            }
        }

        public class NewOrderResp
        {
            public static string IndustryType;

            public static string MessageType;

            public static string MerchantID;

            public static string TerminalID;

            public static string CardBrand;

            public static string AccountNum;

            public static string OrderID;

            public static string TxRefNum;

            public static string TxRefIdx;

            public static string ProcStatus;

            public static string ApprovalStatus;

            public static string RespCode;

            public static string AVSRespCode;

            public static string CVV2RespCode;

            public static string AuthCode;

            public static string RecurringAdviceCd;

            public static string CAVVRespCode;

            public static string StatusMsg;

            public static string RespMsg;

            public static string HostRespCode;

            public static string HostAVSRespCode;

            public static string HostCVV2RespCode;

            public static string CustomerRefNum;

            public static string CustomerName;

            public static string ProfileProcStatus;

            public static string CustomerProfileMessage;

            public static string BillerReferenceNumber;

            public static string MBStatus;

            public static string MBMicroPaymentDaysLeft;

            public static string MBMicroPaymentDollarsLeft;

            public static string RespTime;

            public static string PartialAuthOccurred;

            public static string RequestedAmount;

            public static string RedeemedAmount;

            public static string RemainingBalance;

            public static string CountryFraudFilterStatus;

            public static string IsoCountryCode;

            public static string FraudScoreProcStatus;

            public static string FraudScoreProcMsg;

            public static string FraudAnalysisResponse;

            public static string CTIAffluentCard;

            public static string CTICommercialCard;

            public static string CTIDurbinExemption;

            public static string CTIHealthcareCard;

            public static string CTILevel3Eligible;

            public static string CTIPayrollCard;

            public static string CTIPrepaidCard;

            public static string CTIPINlessDebitCard;

            public static string CTISignatureDebitCard;

            public static string CTIIssuingCountry;

            public static string EUDDCountryCode;

            public static string EUDDBankSortCode;

            public static string EUDDRibCode;

            public static string EUDDBankBranchCode;

            public static string EUDDIBAN;

            public static string EUDDBIC;

            public static string EUDDMandateSignatureDate;

            public static string EUDDMandateID;

            public static string EUDDMandateType;

            public static string TokenAssuranceLevel;

            public static string DPANAccountStatus;

            public static string EWSAccountStatusCode;

            public static string EWSAOAConditionCode;

            public static string EWSNameMatch;

            public static string EWSFirstNameMatch;

            public static string EWSMiddleNameMatch;

            public static string EWSLastNameMatch;

            public static string EWSBusinessNameMatch;

            public static string EWSAddressMatch;

            public static string EWSCityMatch;

            public static string EWSStateMatch;

            public static string EWSZipMatch;

            public static string EWSPhoneMatch;

            public static string EWSSSNTINMatch;

            public static string EWSDOBMatch;

            public static string EWSIDTypeMatch;

            public static string EWSIDNumberMatch;

            public static string EWSIDStateMatch;

            public static string FXOptOutInd;

            public static string FXRateHandlingInd;

            public static string FXRateID;

            public static string FXExchangeRate;

            public static string FXPresentmentCurrency;

            public static string FXSettlementCurrency;

            public static string FXDefaultRateInd;

            public static string FXRateStatus;

            public static string MITReceivedTransactionID;

            public static string RtauUpdatedCardNum;

            public static string RtauUpdatedCardExp;

            public static string RtauResponseCode;

            public static string AVConsumerAccountDate;

            static NewOrderResp()
            {
                ORBResp.NewOrderResp.IndustryType = "IndustryType";
                ORBResp.NewOrderResp.MessageType = "MessageType";
                ORBResp.NewOrderResp.MerchantID = "MerchantID";
                ORBResp.NewOrderResp.TerminalID = "TerminalID";
                ORBResp.NewOrderResp.CardBrand = "CardBrand";
                ORBResp.NewOrderResp.AccountNum = "AccountNum";
                ORBResp.NewOrderResp.OrderID = "OrderID";
                ORBResp.NewOrderResp.TxRefNum = "TxRefNum";
                ORBResp.NewOrderResp.TxRefIdx = "TxRefIdx";
                ORBResp.NewOrderResp.ProcStatus = "ProcStatus";
                ORBResp.NewOrderResp.ApprovalStatus = "ApprovalStatus";
                ORBResp.NewOrderResp.RespCode = "RespCode";
                ORBResp.NewOrderResp.AVSRespCode = "AVSRespCode";
                ORBResp.NewOrderResp.CVV2RespCode = "CVV2RespCode";
                ORBResp.NewOrderResp.AuthCode = "AuthCode";
                ORBResp.NewOrderResp.RecurringAdviceCd = "RecurringAdviceCd";
                ORBResp.NewOrderResp.CAVVRespCode = "CAVVRespCode";
                ORBResp.NewOrderResp.StatusMsg = "StatusMsg";
                ORBResp.NewOrderResp.RespMsg = "RespMsg";
                ORBResp.NewOrderResp.HostRespCode = "HostRespCode";
                ORBResp.NewOrderResp.HostAVSRespCode = "HostAVSRespCode";
                ORBResp.NewOrderResp.HostCVV2RespCode = "HostCVV2RespCode";
                ORBResp.NewOrderResp.CustomerRefNum = "CustomerRefNum";
                ORBResp.NewOrderResp.CustomerName = "CustomerName";
                ORBResp.NewOrderResp.ProfileProcStatus = "ProfileProcStatus";
                ORBResp.NewOrderResp.CustomerProfileMessage = "CustomerProfileMessage";
                ORBResp.NewOrderResp.BillerReferenceNumber = "BillerReferenceNumber";
                ORBResp.NewOrderResp.MBStatus = "MBStatus";
                ORBResp.NewOrderResp.MBMicroPaymentDaysLeft = "MBMicroPaymentDaysLeft";
                ORBResp.NewOrderResp.MBMicroPaymentDollarsLeft = "MBMicroPaymentDollarsLeft";
                ORBResp.NewOrderResp.RespTime = "RespTime";
                ORBResp.NewOrderResp.PartialAuthOccurred = "PartialAuthOccurred";
                ORBResp.NewOrderResp.RequestedAmount = "RequestedAmount";
                ORBResp.NewOrderResp.RedeemedAmount = "RedeemedAmount";
                ORBResp.NewOrderResp.RemainingBalance = "RemainingBalance";
                ORBResp.NewOrderResp.CountryFraudFilterStatus = "CountryFraudFilterStatus";
                ORBResp.NewOrderResp.IsoCountryCode = "IsoCountryCode";
                ORBResp.NewOrderResp.FraudScoreProcStatus = "FraudScoreProcStatus";
                ORBResp.NewOrderResp.FraudScoreProcMsg = "FraudScoreProcMsg";
                ORBResp.NewOrderResp.FraudAnalysisResponse = "FraudAnalysisResponse";
                ORBResp.NewOrderResp.CTIAffluentCard = "CTIAffluentCard";
                ORBResp.NewOrderResp.CTICommercialCard = "CTICommercialCard";
                ORBResp.NewOrderResp.CTIDurbinExemption = "CTIDurbinExemption";
                ORBResp.NewOrderResp.CTIHealthcareCard = "CTIHealthcareCard";
                ORBResp.NewOrderResp.CTILevel3Eligible = "CTILevel3Eligible";
                ORBResp.NewOrderResp.CTIPayrollCard = "CTIPayrollCard";
                ORBResp.NewOrderResp.CTIPrepaidCard = "CTIPrepaidCard";
                ORBResp.NewOrderResp.CTIPINlessDebitCard = "CTIPINlessDebitCard";
                ORBResp.NewOrderResp.CTISignatureDebitCard = "CTISignatureDebitCard";
                ORBResp.NewOrderResp.CTIIssuingCountry = "CTIIssuingCountry";
                ORBResp.NewOrderResp.EUDDCountryCode = "EUDDCountryCode";
                ORBResp.NewOrderResp.EUDDBankSortCode = "EUDDBankSortCode";
                ORBResp.NewOrderResp.EUDDRibCode = "EUDDRibCode";
                ORBResp.NewOrderResp.EUDDBankBranchCode = "EUDDBankBranchCode";
                ORBResp.NewOrderResp.EUDDIBAN = "EUDDIBAN";
                ORBResp.NewOrderResp.EUDDBIC = "EUDDBIC";
                ORBResp.NewOrderResp.EUDDMandateSignatureDate = "EUDDMandateSignatureDate";
                ORBResp.NewOrderResp.EUDDMandateID = "EUDDMandateID";
                ORBResp.NewOrderResp.EUDDMandateType = "EUDDMandateType";
                ORBResp.NewOrderResp.TokenAssuranceLevel = "TokenAssuranceLevel";
                ORBResp.NewOrderResp.DPANAccountStatus = "DPANAccountStatus";
                ORBResp.NewOrderResp.EWSAccountStatusCode = "EWSAccountStatusCode";
                ORBResp.NewOrderResp.EWSAOAConditionCode = "EWSAOAConditionCode";
                ORBResp.NewOrderResp.EWSNameMatch = "EWSNameMatch";
                ORBResp.NewOrderResp.EWSFirstNameMatch = "EWSFirstNameMatch";
                ORBResp.NewOrderResp.EWSMiddleNameMatch = "EWSMiddleNameMatch";
                ORBResp.NewOrderResp.EWSLastNameMatch = "EWSLastNameMatch";
                ORBResp.NewOrderResp.EWSBusinessNameMatch = "EWSBusinessNameMatch";
                ORBResp.NewOrderResp.EWSAddressMatch = "EWSAddressMatch";
                ORBResp.NewOrderResp.EWSCityMatch = "EWSCityMatch";
                ORBResp.NewOrderResp.EWSStateMatch = "EWSStateMatch";
                ORBResp.NewOrderResp.EWSZipMatch = "EWSZipMatch";
                ORBResp.NewOrderResp.EWSPhoneMatch = "EWSPhoneMatch";
                ORBResp.NewOrderResp.EWSSSNTINMatch = "EWSSSNTINMatch";
                ORBResp.NewOrderResp.EWSDOBMatch = "EWSDOBMatch";
                ORBResp.NewOrderResp.EWSIDTypeMatch = "EWSIDTypeMatch";
                ORBResp.NewOrderResp.EWSIDNumberMatch = "EWSIDNumberMatch";
                ORBResp.NewOrderResp.EWSIDStateMatch = "EWSIDStateMatch";
                ORBResp.NewOrderResp.FXOptOutInd = "FXOptOutInd";
                ORBResp.NewOrderResp.FXRateHandlingInd = "FXRateHandlingInd";
                ORBResp.NewOrderResp.FXRateID = "FXRateID";
                ORBResp.NewOrderResp.FXExchangeRate = "FXExchangeRate";
                ORBResp.NewOrderResp.FXPresentmentCurrency = "FXPresentmentCurrency";
                ORBResp.NewOrderResp.FXSettlementCurrency = "FXSettlementCurrency";
                ORBResp.NewOrderResp.FXDefaultRateInd = "FXDefaultRateInd";
                ORBResp.NewOrderResp.FXRateStatus = "FXRateStatus";
                ORBResp.NewOrderResp.MITReceivedTransactionID = "MITReceivedTransactionID";
                ORBResp.NewOrderResp.RtauUpdatedCardNum = "RtauUpdatedCardNum";
                ORBResp.NewOrderResp.RtauUpdatedCardExp = "RtauUpdatedCardExp";
                ORBResp.NewOrderResp.RtauResponseCode = "RtauResponseCode";
                ORBResp.NewOrderResp.AVConsumerAccountDate = "AVConsumerAccountDate";
            }

            public NewOrderResp()
            {
            }
        }

        public class ProfileResp
        {
            public static string CustomerBin;

            public static string CustomerMerchantID;

            public static string CustomerName;

            public static string CustomerRefNum;

            public static string CustomerProfileAction;

            public static string ProfileProcStatus;

            public static string CustomerProfileMessage;

            public static string CustomerAddress1;

            public static string CustomerAddress2;

            public static string CustomerCity;

            public static string CustomerState;

            public static string CustomerZIP;

            public static string CustomerEmail;

            public static string CustomerPhone;

            public static string CustomerCountryCode;

            public static string CustomerProfileOrderOverrideInd;

            public static string OrderDefaultDescription;

            public static string OrderDefaultAmount;

            public static string CustomerAccountType;

            public static string Status;

            public static string CardBrand;

            public static string CCAccountNum;

            public static string CCExpireDate;

            public static string ECPAccountDDA;

            public static string ECPAccountType;

            public static string ECPAccountRT;

            public static string ECPBankPmtDlv;

            public static string SwitchSoloStartDate;

            public static string SwitchSoloIssueNum;

            public static string CustomerMBStatus;

            public static string MBType;

            public static string MBOrderIdGenerationMethod;

            public static string MBRecurringStartDate;

            public static string MBRecurringEndDate;

            public static string MBRecurringNoEndDateFlag;

            public static string MBRecurringMaxBillings;

            public static string MBRecurringFrequency;

            public static string MBDeferredBillDate;

            public static string MBMicroPaymentMaxDollarValue;

            public static string MBMicroPaymentMaxBillingDays;

            public static string MBMicroPaymentMaxTransactions;

            public static string MBCustomerStatus;

            public static string MBMicroPaymentDaysLeft;

            public static string MBMicroPaymentDollarsLeft;

            public static string EUDDCountryCode;

            public static string EUDDBankSortCode;

            public static string EUDDRibCode;

            public static string SDMerchantName;

            public static string SDProductDescription;

            public static string SDMerchantCity;

            public static string SDMerchantPhone;

            public static string SDMerchantURL;

            public static string SDMerchantEmail;

            public static string BillerReferenceNumber;

            public static string RespTime;

            public static string AccountUpdaterEligibility;

            public static string EUDDBankBranchCode;

            public static string EUDDIBAN;

            public static string EUDDBIC;

            public static string EUDDMandateSignatureDate;

            public static string EUDDMandateID;

            public static string EUDDMandateType;

            public static string DPANInd;

            public static string TokenRequestorID;

            public static string MITMsgType;

            public static string MITSubmittedTransactionID;

            public static string PinlessDebitTxnType;

            public static string PinlessDebitMerchantUrl;

            static ProfileResp()
            {
                ORBResp.ProfileResp.CustomerBin = "CustomerBin";
                ORBResp.ProfileResp.CustomerMerchantID = "CustomerMerchantID";
                ORBResp.ProfileResp.CustomerName = "CustomerName";
                ORBResp.ProfileResp.CustomerRefNum = "CustomerRefNum";
                ORBResp.ProfileResp.CustomerProfileAction = "CustomerProfileAction";
                ORBResp.ProfileResp.ProfileProcStatus = "ProfileProcStatus";
                ORBResp.ProfileResp.CustomerProfileMessage = "CustomerProfileMessage";
                ORBResp.ProfileResp.CustomerAddress1 = "CustomerAddress1";
                ORBResp.ProfileResp.CustomerAddress2 = "CustomerAddress2";
                ORBResp.ProfileResp.CustomerCity = "CustomerCity";
                ORBResp.ProfileResp.CustomerState = "CustomerState";
                ORBResp.ProfileResp.CustomerZIP = "CustomerZIP";
                ORBResp.ProfileResp.CustomerEmail = "CustomerEmail";
                ORBResp.ProfileResp.CustomerPhone = "CustomerPhone";
                ORBResp.ProfileResp.CustomerCountryCode = "CustomerCountryCode";
                ORBResp.ProfileResp.CustomerProfileOrderOverrideInd = "CustomerProfileOrderOverrideInd";
                ORBResp.ProfileResp.OrderDefaultDescription = "OrderDefaultDescription";
                ORBResp.ProfileResp.OrderDefaultAmount = "OrderDefaultAmount";
                ORBResp.ProfileResp.CustomerAccountType = "CustomerAccountType";
                ORBResp.ProfileResp.Status = "Status";
                ORBResp.ProfileResp.CardBrand = "CardBrand";
                ORBResp.ProfileResp.CCAccountNum = "CCAccountNum";
                ORBResp.ProfileResp.CCExpireDate = "CCExpireDate";
                ORBResp.ProfileResp.ECPAccountDDA = "ECPAccountDDA";
                ORBResp.ProfileResp.ECPAccountType = "ECPAccountType";
                ORBResp.ProfileResp.ECPAccountRT = "ECPAccountRT";
                ORBResp.ProfileResp.ECPBankPmtDlv = "ECPBankPmtDlv";
                ORBResp.ProfileResp.SwitchSoloStartDate = "SwitchSoloStartDate";
                ORBResp.ProfileResp.SwitchSoloIssueNum = "SwitchSoloIssueNum";
                ORBResp.ProfileResp.CustomerMBStatus = "CustomerMBStatus";
                ORBResp.ProfileResp.MBType = "MBType";
                ORBResp.ProfileResp.MBOrderIdGenerationMethod = "MBOrderIdGenerationMethod";
                ORBResp.ProfileResp.MBRecurringStartDate = "MBRecurringStartDate";
                ORBResp.ProfileResp.MBRecurringEndDate = "MBRecurringEndDate";
                ORBResp.ProfileResp.MBRecurringNoEndDateFlag = "MBRecurringNoEndDateFlag";
                ORBResp.ProfileResp.MBRecurringMaxBillings = "MBRecurringMaxBillings";
                ORBResp.ProfileResp.MBRecurringFrequency = "MBRecurringFrequency";
                ORBResp.ProfileResp.MBDeferredBillDate = "MBDeferredBillDate";
                ORBResp.ProfileResp.MBMicroPaymentMaxDollarValue = "MBMicroPaymentMaxDollarValue";
                ORBResp.ProfileResp.MBMicroPaymentMaxBillingDays = "MBMicroPaymentMaxBillingDays";
                ORBResp.ProfileResp.MBMicroPaymentMaxTransactions = "MBMicroPaymentMaxTransactions";
                ORBResp.ProfileResp.MBCustomerStatus = "MBCustomerStatus";
                ORBResp.ProfileResp.MBMicroPaymentDaysLeft = "MBMicroPaymentDaysLeft";
                ORBResp.ProfileResp.MBMicroPaymentDollarsLeft = "MBMicroPaymentDollarsLeft";
                ORBResp.ProfileResp.EUDDCountryCode = "EUDDCountryCode";
                ORBResp.ProfileResp.EUDDBankSortCode = "EUDDBankSortCode";
                ORBResp.ProfileResp.EUDDRibCode = "EUDDRibCode";
                ORBResp.ProfileResp.SDMerchantName = "SDMerchantName";
                ORBResp.ProfileResp.SDProductDescription = "SDProductDescription";
                ORBResp.ProfileResp.SDMerchantCity = "SDMerchantCity";
                ORBResp.ProfileResp.SDMerchantPhone = "SDMerchantPhone";
                ORBResp.ProfileResp.SDMerchantURL = "SDMerchantURL";
                ORBResp.ProfileResp.SDMerchantEmail = "SDMerchantEmail";
                ORBResp.ProfileResp.BillerReferenceNumber = "BillerReferenceNumber";
                ORBResp.ProfileResp.RespTime = "RespTime";
                ORBResp.ProfileResp.AccountUpdaterEligibility = "AccountUpdaterEligibility";
                ORBResp.ProfileResp.EUDDBankBranchCode = "EUDDBankBranchCode";
                ORBResp.ProfileResp.EUDDIBAN = "EUDDIBAN";
                ORBResp.ProfileResp.EUDDBIC = "EUDDBIC";
                ORBResp.ProfileResp.EUDDMandateSignatureDate = "EUDDMandateSignatureDate";
                ORBResp.ProfileResp.EUDDMandateID = "EUDDMandateID";
                ORBResp.ProfileResp.EUDDMandateType = "EUDDMandateType";
                ORBResp.ProfileResp.DPANInd = "DPANInd";
                ORBResp.ProfileResp.TokenRequestorID = "TokenRequestorID";
                ORBResp.ProfileResp.MITMsgType = "MITMsgType";
                ORBResp.ProfileResp.MITSubmittedTransactionID = "MITSubmittedTransactionID";
                ORBResp.ProfileResp.PinlessDebitTxnType = "PinlessDebitTxnType";
                ORBResp.ProfileResp.PinlessDebitMerchantUrl = "PinlessDebitMerchantUrl";
            }

            public ProfileResp()
            {
            }
        }

        public class QuickResp
        {
            public static string MerchantID;

            public static string TerminalID;

            public static string OrderID;

            public static string AccountNum;

            public static string StartAccountNum;

            public static string TxRefNum;

            public static string TxRefIdx;

            public static string ProcStatus;

            public static string StatusMsg;

            public static string ApprovalStatus;

            public static string CustomerBin;

            public static string CustomerMerchantID;

            public static string CustomerName;

            public static string CustomerRefNum;

            public static string CustomerProfileAction;

            public static string ProfileProcStatus;

            public static string CustomerProfileMessage;

            public static string CustomerAddress1;

            public static string CustomerAddress2;

            public static string CustomerCity;

            public static string CustomerState;

            public static string CustomerZIP;

            public static string CustomerEmail;

            public static string CustomerPhone;

            public static string CustomerProfileOrderOverrideInd;

            public static string OrderDefaultDescription;

            public static string OrderDefaultAmount;

            public static string CustomerAccountType;

            public static string CCAccountNum;

            public static string CCExpireDate;

            public static string ECPAccountDDA;

            public static string ECPAccountType;

            public static string ECPAccountRT;

            public static string ECPBankPmtDlv;

            public static string SwitchSoloStartDate;

            public static string SwitchSoloIssueNum;

            public static string RespTime;

            static QuickResp()
            {
                ORBResp.QuickResp.MerchantID = "MerchantID";
                ORBResp.QuickResp.TerminalID = "TerminalID";
                ORBResp.QuickResp.OrderID = "OrderID";
                ORBResp.QuickResp.AccountNum = "AccountNum";
                ORBResp.QuickResp.StartAccountNum = "StartAccountNum";
                ORBResp.QuickResp.TxRefNum = "TxRefNum";
                ORBResp.QuickResp.TxRefIdx = "TxRefIdx";
                ORBResp.QuickResp.ProcStatus = "ProcStatus";
                ORBResp.QuickResp.StatusMsg = "StatusMsg";
                ORBResp.QuickResp.ApprovalStatus = "ApprovalStatus";
                ORBResp.QuickResp.CustomerBin = "CustomerBin";
                ORBResp.QuickResp.CustomerMerchantID = "CustomerMerchantID";
                ORBResp.QuickResp.CustomerName = "CustomerName";
                ORBResp.QuickResp.CustomerRefNum = "CustomerRefNum";
                ORBResp.QuickResp.CustomerProfileAction = "CustomerProfileAction";
                ORBResp.QuickResp.ProfileProcStatus = "ProfileProcStatus";
                ORBResp.QuickResp.CustomerProfileMessage = "CustomerProfileMessage";
                ORBResp.QuickResp.CustomerAddress1 = "CustomerAddress1";
                ORBResp.QuickResp.CustomerAddress2 = "CustomerAddress2";
                ORBResp.QuickResp.CustomerCity = "CustomerCity";
                ORBResp.QuickResp.CustomerState = "CustomerState";
                ORBResp.QuickResp.CustomerZIP = "CustomerZIP";
                ORBResp.QuickResp.CustomerEmail = "CustomerEmail";
                ORBResp.QuickResp.CustomerPhone = "CustomerPhone";
                ORBResp.QuickResp.CustomerProfileOrderOverrideInd = "CustomerProfileOrderOverrideInd";
                ORBResp.QuickResp.OrderDefaultDescription = "OrderDefaultDescription";
                ORBResp.QuickResp.OrderDefaultAmount = "OrderDefaultAmount";
                ORBResp.QuickResp.CustomerAccountType = "CustomerAccountType";
                ORBResp.QuickResp.CCAccountNum = "CCAccountNum";
                ORBResp.QuickResp.CCExpireDate = "CCExpireDate";
                ORBResp.QuickResp.ECPAccountDDA = "ECPAccountDDA";
                ORBResp.QuickResp.ECPAccountType = "ECPAccountType";
                ORBResp.QuickResp.ECPAccountRT = "ECPAccountRT";
                ORBResp.QuickResp.ECPBankPmtDlv = "ECPBankPmtDlv";
                ORBResp.QuickResp.SwitchSoloStartDate = "SwitchSoloStartDate";
                ORBResp.QuickResp.SwitchSoloIssueNum = "SwitchSoloIssueNum";
                ORBResp.QuickResp.RespTime = "RespTime";
            }

            public QuickResp()
            {
            }
        }

        public class QuickResponse
        {
            public static string MerchantID;

            public static string TerminalID;

            public static string ProcStatus;

            public static string ApprovalStatus;

            public static string TxRefIdx;

            public static string TxRefNum;

            public static string OrderNumber;

            public static string AccountNum;

            public static string POSEntryMode;

            public static string CardType1;

            public static string RespTime;

            public static string CustomerProfileResponse;

            static QuickResponse()
            {
                ORBResp.QuickResponse.MerchantID = "MerchantID";
                ORBResp.QuickResponse.TerminalID = "TerminalID";
                ORBResp.QuickResponse.ProcStatus = "ProcStatus";
                ORBResp.QuickResponse.ApprovalStatus = "ApprovalStatus";
                ORBResp.QuickResponse.TxRefIdx = "TxRefIdx";
                ORBResp.QuickResponse.TxRefNum = "TxRefNum";
                ORBResp.QuickResponse.OrderNumber = "OrderNumber";
                ORBResp.QuickResponse.AccountNum = "AccountNum";
                ORBResp.QuickResponse.POSEntryMode = "POSEntryMode";
                ORBResp.QuickResponse.CardType1 = "CardType1";
                ORBResp.QuickResponse.RespTime = "RespTime";
                ORBResp.QuickResponse.CustomerProfileResponse = "CustomerProfileResponse";
            }

            public QuickResponse()
            {
            }
        }

        public class ReversalResp
        {
            public static string MerchantID;

            public static string TerminalID;

            public static string OrderID;

            public static string TxRefNum;

            public static string TxRefIdx;

            public static string OutstandingAmt;

            public static string ProcStatus;

            public static string StatusMsg;

            public static string RespTime;

            public static string MITReceivedTransactionID;

            static ReversalResp()
            {
                ORBResp.ReversalResp.MerchantID = "MerchantID";
                ORBResp.ReversalResp.TerminalID = "TerminalID";
                ORBResp.ReversalResp.OrderID = "OrderID";
                ORBResp.ReversalResp.TxRefNum = "TxRefNum";
                ORBResp.ReversalResp.TxRefIdx = "TxRefIdx";
                ORBResp.ReversalResp.OutstandingAmt = "OutstandingAmt";
                ORBResp.ReversalResp.ProcStatus = "ProcStatus";
                ORBResp.ReversalResp.StatusMsg = "StatusMsg";
                ORBResp.ReversalResp.RespTime = "RespTime";
                ORBResp.ReversalResp.MITReceivedTransactionID = "MITReceivedTransactionID";
            }

            public ReversalResp()
            {
            }
        }

        public class SafetechFraudAnalysisResp
        {
            public static string IndustryType;

            public static string MerchantID;

            public static string TerminalID;

            public static string CardBrand;

            public static string AccountNum;

            public static string OrderID;

            public static string TxRefNum;

            public static string RespTime;

            public static string ProcStatus;

            public static string ApprovalStatus;

            public static string RespCode;

            public static string StatusMsg;

            public static string RespMsg;

            public static string HostRespCode;

            public static string CustomerRefNum;

            public static string CustomerName;

            public static string ProfileProcStatus;

            public static string CustomerProfileMessage;

            public static string FraudAnalysisResponse;

            public static string EUDDCountryCode;

            public static string EUDDBankSortCode;

            public static string EUDDRibCode;

            public static string EUDDBankBranchCode;

            public static string EUDDIBAN;

            public static string EUDDBIC;

            static SafetechFraudAnalysisResp()
            {
                ORBResp.SafetechFraudAnalysisResp.IndustryType = "IndustryType";
                ORBResp.SafetechFraudAnalysisResp.MerchantID = "MerchantID";
                ORBResp.SafetechFraudAnalysisResp.TerminalID = "TerminalID";
                ORBResp.SafetechFraudAnalysisResp.CardBrand = "CardBrand";
                ORBResp.SafetechFraudAnalysisResp.AccountNum = "AccountNum";
                ORBResp.SafetechFraudAnalysisResp.OrderID = "OrderID";
                ORBResp.SafetechFraudAnalysisResp.TxRefNum = "TxRefNum";
                ORBResp.SafetechFraudAnalysisResp.RespTime = "RespTime";
                ORBResp.SafetechFraudAnalysisResp.ProcStatus = "ProcStatus";
                ORBResp.SafetechFraudAnalysisResp.ApprovalStatus = "ApprovalStatus";
                ORBResp.SafetechFraudAnalysisResp.RespCode = "RespCode";
                ORBResp.SafetechFraudAnalysisResp.StatusMsg = "StatusMsg";
                ORBResp.SafetechFraudAnalysisResp.RespMsg = "RespMsg";
                ORBResp.SafetechFraudAnalysisResp.HostRespCode = "HostRespCode";
                ORBResp.SafetechFraudAnalysisResp.CustomerRefNum = "CustomerRefNum";
                ORBResp.SafetechFraudAnalysisResp.CustomerName = "CustomerName";
                ORBResp.SafetechFraudAnalysisResp.ProfileProcStatus = "ProfileProcStatus";
                ORBResp.SafetechFraudAnalysisResp.CustomerProfileMessage = "CustomerProfileMessage";
                ORBResp.SafetechFraudAnalysisResp.FraudAnalysisResponse = "FraudAnalysisResponse";
                ORBResp.SafetechFraudAnalysisResp.EUDDCountryCode = "EUDDCountryCode";
                ORBResp.SafetechFraudAnalysisResp.EUDDBankSortCode = "EUDDBankSortCode";
                ORBResp.SafetechFraudAnalysisResp.EUDDRibCode = "EUDDRibCode";
                ORBResp.SafetechFraudAnalysisResp.EUDDBankBranchCode = "EUDDBankBranchCode";
                ORBResp.SafetechFraudAnalysisResp.EUDDIBAN = "EUDDIBAN";
                ORBResp.SafetechFraudAnalysisResp.EUDDBIC = "EUDDBIC";
            }

            public SafetechFraudAnalysisResp()
            {
            }
        }
    }
}