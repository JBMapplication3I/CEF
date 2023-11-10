#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK
{
    public class SLMResp
    {
        public SLMResp()
        {
        }

        public class Batch
        {
            public Batch()
            {
            }

            public class ACCESSFXRATE
            {
                public ACCESSFXRATE()
                {
                }

                public class AccessFxDetail
                {
                    public readonly static string RecordType;

                    public readonly static string RateID;

                    public readonly static string Rate;

                    public readonly static string ExpirationDate;

                    public readonly static string ExpirationTime;

                    public readonly static string RefundExpirationDate;

                    public readonly static string RefundExpirationTime;

                    public readonly static string SourceCurrency;

                    public readonly static string DestinationCurrency;

                    public readonly static string ActionType;

                    public readonly static string CIBBaseRate;

                    public readonly static string ClientBaseRate;

                    public readonly static string DefaultRateIndicator;

                    public readonly static string CIBMarkup;

                    public readonly static string ClientMarkup;

                    public readonly static string DoddFrankRateMID;

                    static AccessFxDetail()
                    {
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.RecordType = "AccessFxDetail.RecordType";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.RateID = "AccessFxDetail.RateID";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.Rate = "AccessFxDetail.Rate";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.ExpirationDate = "AccessFxDetail.ExpirationDate";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.ExpirationTime = "AccessFxDetail.ExpirationTime";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.RefundExpirationDate = "AccessFxDetail.RefundExpirationDate";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.RefundExpirationTime = "AccessFxDetail.RefundExpirationTime";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.SourceCurrency = "AccessFxDetail.SourceCurrency";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.DestinationCurrency = "AccessFxDetail.DestinationCurrency";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.ActionType = "AccessFxDetail.ActionType";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.CIBBaseRate = "AccessFxDetail.CIBBaseRate";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.ClientBaseRate = "AccessFxDetail.ClientBaseRate";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.DefaultRateIndicator = "AccessFxDetail.DefaultRateIndicator";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.CIBMarkup = "AccessFxDetail.CIBMarkup";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.ClientMarkup = "AccessFxDetail.ClientMarkup";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxDetail.DoddFrankRateMID = "AccessFxDetail.DoddFrankRateMID";
                    }

                    public AccessFxDetail()
                    {
                    }
                }

                public class AccessFxHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string Version;

                    public readonly static string RateSheetStartDate;

                    public readonly static string RateSheetPublicationID;

                    public readonly static string ChannelID;

                    static AccessFxHeader()
                    {
                        SLMResp.Batch.ACCESSFXRATE.AccessFxHeader.RecordType = "AccessFxHeader.RecordType";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxHeader.FileName = "AccessFxHeader.FileName";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxHeader.Version = "AccessFxHeader.Version";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxHeader.RateSheetStartDate = "AccessFxHeader.RateSheetStartDate";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxHeader.RateSheetPublicationID = "AccessFxHeader.RateSheetPublicationID";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxHeader.ChannelID = "AccessFxHeader.ChannelID";
                    }

                    public AccessFxHeader()
                    {
                    }
                }

                public class AccessFxTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDateAndTime;

                    public readonly static string NumberOfJPMorganAccessFxRateIDs;

                    static AccessFxTrailer()
                    {
                        SLMResp.Batch.ACCESSFXRATE.AccessFxTrailer.RecordType = "AccessFxTrailer.RecordType";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxTrailer.CreationDateAndTime = "AccessFxTrailer.CreationDateAndTime";
                        SLMResp.Batch.ACCESSFXRATE.AccessFxTrailer.NumberOfJPMorganAccessFxRateIDs = "AccessFxTrailer.NumberOfJPMorganAccessFxRateIDs";
                    }

                    public AccessFxTrailer()
                    {
                    }
                }
            }

            public class AddressExtension
            {
                public readonly static string AddressLine;

                static AddressExtension()
                {
                    SLMResp.Batch.AddressExtension.AddressLine = "AddressExtension.AddressLine";
                }

                public AddressExtension()
                {
                }
            }

            public class AmericanExpress1
            {
                public readonly static string DepartmentName;

                public readonly static string ItemDescription1;

                public readonly static string ItemQuantity1;

                public readonly static string ItemAmount1;

                public readonly static string ItemDescription2;

                public readonly static string ItemQuantity2;

                public readonly static string ItemAmount2;

                static AmericanExpress1()
                {
                    SLMResp.Batch.AmericanExpress1.DepartmentName = "AmericanExpress1.DepartmentName";
                    SLMResp.Batch.AmericanExpress1.ItemDescription1 = "AmericanExpress1.ItemDescription1";
                    SLMResp.Batch.AmericanExpress1.ItemQuantity1 = "AmericanExpress1.ItemQuantity1";
                    SLMResp.Batch.AmericanExpress1.ItemAmount1 = "AmericanExpress1.ItemAmount1";
                    SLMResp.Batch.AmericanExpress1.ItemDescription2 = "AmericanExpress1.ItemDescription2";
                    SLMResp.Batch.AmericanExpress1.ItemQuantity2 = "AmericanExpress1.ItemQuantity2";
                    SLMResp.Batch.AmericanExpress1.ItemAmount2 = "AmericanExpress1.ItemAmount2";
                }

                public AmericanExpress1()
                {
                }
            }

            public class AmericanExpress2
            {
                public readonly static string ItemDescription3;

                public readonly static string ItemQuantity3;

                public readonly static string ItemAmount3;

                public readonly static string ItemDescription4;

                public readonly static string ItemQuantity4;

                public readonly static string ItemAmount4;

                static AmericanExpress2()
                {
                    SLMResp.Batch.AmericanExpress2.ItemDescription3 = "AmericanExpress2.ItemDescription3";
                    SLMResp.Batch.AmericanExpress2.ItemQuantity3 = "AmericanExpress2.ItemQuantity3";
                    SLMResp.Batch.AmericanExpress2.ItemAmount3 = "AmericanExpress2.ItemAmount3";
                    SLMResp.Batch.AmericanExpress2.ItemDescription4 = "AmericanExpress2.ItemDescription4";
                    SLMResp.Batch.AmericanExpress2.ItemQuantity4 = "AmericanExpress2.ItemQuantity4";
                    SLMResp.Batch.AmericanExpress2.ItemAmount4 = "AmericanExpress2.ItemAmount4";
                }

                public AmericanExpress2()
                {
                }
            }

            public class AmericanExpress3
            {
                public readonly static string ItemDescription5;

                public readonly static string ItemQuantity5;

                public readonly static string ItemAmount5;

                public readonly static string ItemDescription6;

                public readonly static string ItemQuantity6;

                public readonly static string ItemAmount6;

                static AmericanExpress3()
                {
                    SLMResp.Batch.AmericanExpress3.ItemDescription5 = "AmericanExpress3.ItemDescription5";
                    SLMResp.Batch.AmericanExpress3.ItemQuantity5 = "AmericanExpress3.ItemQuantity5";
                    SLMResp.Batch.AmericanExpress3.ItemAmount5 = "AmericanExpress3.ItemAmount5";
                    SLMResp.Batch.AmericanExpress3.ItemDescription6 = "AmericanExpress3.ItemDescription6";
                    SLMResp.Batch.AmericanExpress3.ItemQuantity6 = "AmericanExpress3.ItemQuantity6";
                    SLMResp.Batch.AmericanExpress3.ItemAmount6 = "AmericanExpress3.ItemAmount6";
                }

                public AmericanExpress3()
                {
                }
            }

            public class AmericanExpressAuthentication
            {
                public readonly static string AmericanExpressAuthenticationVerificationValue;

                public readonly static string TransactionID;

                public readonly static string AuthenticationType;

                public readonly static string AEVVResponseCode;

                static AmericanExpressAuthentication()
                {
                    SLMResp.Batch.AmericanExpressAuthentication.AmericanExpressAuthenticationVerificationValue = "AmericanExpressAuthentication.AmericanExpressAuthenticationVerificationValue";
                    SLMResp.Batch.AmericanExpressAuthentication.TransactionID = "AmericanExpressAuthentication.TransactionID";
                    SLMResp.Batch.AmericanExpressAuthentication.AuthenticationType = "AmericanExpressAuthentication.AuthenticationType";
                    SLMResp.Batch.AmericanExpressAuthentication.AEVVResponseCode = "AmericanExpressAuthentication.AEVVResponseCode";
                }

                public AmericanExpressAuthentication()
                {
                }
            }

            public class AmericanExpressCPSLevel2
            {
                public readonly static string SequenceNumber;

                public readonly static string Description;

                public readonly static string Quantity;

                public readonly static string UnitCost;

                public readonly static string Description1;

                public readonly static string Quantity1;

                public readonly static string UnitCost1;

                static AmericanExpressCPSLevel2()
                {
                    SLMResp.Batch.AmericanExpressCPSLevel2.SequenceNumber = "AmericanExpressCPSLevel2.SequenceNumber";
                    SLMResp.Batch.AmericanExpressCPSLevel2.Description = "AmericanExpressCPSLevel2.Description";
                    SLMResp.Batch.AmericanExpressCPSLevel2.Quantity = "AmericanExpressCPSLevel2.Quantity";
                    SLMResp.Batch.AmericanExpressCPSLevel2.UnitCost = "AmericanExpressCPSLevel2.UnitCost";
                    SLMResp.Batch.AmericanExpressCPSLevel2.Description1 = "AmericanExpressCPSLevel2.Description1";
                    SLMResp.Batch.AmericanExpressCPSLevel2.Quantity1 = "AmericanExpressCPSLevel2.Quantity1";
                    SLMResp.Batch.AmericanExpressCPSLevel2.UnitCost1 = "AmericanExpressCPSLevel2.UnitCost1";
                }

                public AmericanExpressCPSLevel2()
                {
                }
            }

            public class AMEXExtRecord1
            {
                public readonly static string TAA1;

                public readonly static string TAA2;

                static AMEXExtRecord1()
                {
                    SLMResp.Batch.AMEXExtRecord1.TAA1 = "AMEXExtRecord1.TAA1";
                    SLMResp.Batch.AMEXExtRecord1.TAA2 = "AMEXExtRecord1.TAA2";
                }

                public AMEXExtRecord1()
                {
                }
            }

            public class AMEXExtRecord2
            {
                public readonly static string TAA3;

                public readonly static string TAA4;

                static AMEXExtRecord2()
                {
                    SLMResp.Batch.AMEXExtRecord2.TAA3 = "AMEXExtRecord2.TAA3";
                    SLMResp.Batch.AMEXExtRecord2.TAA4 = "AMEXExtRecord2.TAA4";
                }

                public AMEXExtRecord2()
                {
                }
            }

            public class AMEXExtRecord3
            {
                public readonly static string TransactionID;

                public readonly static string POSCapabilityCode;

                public readonly static string CardHolderAuthenticationCap;

                public readonly static string CardCaptureCapability;

                public readonly static string OperatingEnvironment;

                public readonly static string CardHolderPresent;

                public readonly static string CardPresent;

                public readonly static string POSEntryMode;

                public readonly static string POSCardIDMethod;

                public readonly static string CardHolderAuthenticationEntity;

                public readonly static string CardDataOutputCapability;

                public readonly static string TerminalOutputCapability;

                public readonly static string PINCaptureCapability;

                public readonly static string AuthorizedAmount;

                static AMEXExtRecord3()
                {
                    SLMResp.Batch.AMEXExtRecord3.TransactionID = "AMEXExtRecord3.TransactionID";
                    SLMResp.Batch.AMEXExtRecord3.POSCapabilityCode = "AMEXExtRecord3.POSCapabilityCode";
                    SLMResp.Batch.AMEXExtRecord3.CardHolderAuthenticationCap = "AMEXExtRecord3.CardHolderAuthenticationCap";
                    SLMResp.Batch.AMEXExtRecord3.CardCaptureCapability = "AMEXExtRecord3.CardCaptureCapability";
                    SLMResp.Batch.AMEXExtRecord3.OperatingEnvironment = "AMEXExtRecord3.OperatingEnvironment";
                    SLMResp.Batch.AMEXExtRecord3.CardHolderPresent = "AMEXExtRecord3.CardHolderPresent";
                    SLMResp.Batch.AMEXExtRecord3.CardPresent = "AMEXExtRecord3.CardPresent";
                    SLMResp.Batch.AMEXExtRecord3.POSEntryMode = "AMEXExtRecord3.POSEntryMode";
                    SLMResp.Batch.AMEXExtRecord3.POSCardIDMethod = "AMEXExtRecord3.POSCardIDMethod";
                    SLMResp.Batch.AMEXExtRecord3.CardHolderAuthenticationEntity = "AMEXExtRecord3.CardHolderAuthenticationEntity";
                    SLMResp.Batch.AMEXExtRecord3.CardDataOutputCapability = "AMEXExtRecord3.CardDataOutputCapability";
                    SLMResp.Batch.AMEXExtRecord3.TerminalOutputCapability = "AMEXExtRecord3.TerminalOutputCapability";
                    SLMResp.Batch.AMEXExtRecord3.PINCaptureCapability = "AMEXExtRecord3.PINCaptureCapability";
                    SLMResp.Batch.AMEXExtRecord3.AuthorizedAmount = "AMEXExtRecord3.AuthorizedAmount";
                }

                public AMEXExtRecord3()
                {
                }
            }

            public class BalanceInquiry1
            {
                public readonly static string CurrentBalance;

                public readonly static string CurrentBalanceSign;

                public readonly static string CurrentBalanceCurrencyCode;

                static BalanceInquiry1()
                {
                    SLMResp.Batch.BalanceInquiry1.CurrentBalance = "BalanceInquiry1.CurrentBalance";
                    SLMResp.Batch.BalanceInquiry1.CurrentBalanceSign = "BalanceInquiry1.CurrentBalanceSign";
                    SLMResp.Batch.BalanceInquiry1.CurrentBalanceCurrencyCode = "BalanceInquiry1.CurrentBalanceCurrencyCode";
                }

                public BalanceInquiry1()
                {
                }
            }

            public class BatchTotal
            {
                public readonly static string BatchRecordCount;

                public readonly static string BatchOrderCount;

                public readonly static string BatchAmountTotal;

                public readonly static string BatchAmountSales;

                public readonly static string BatchAmountRefunds;

                static BatchTotal()
                {
                    SLMResp.Batch.BatchTotal.BatchRecordCount = "BatchTotal.BatchRecordCount";
                    SLMResp.Batch.BatchTotal.BatchOrderCount = "BatchTotal.BatchOrderCount";
                    SLMResp.Batch.BatchTotal.BatchAmountTotal = "BatchTotal.BatchAmountTotal";
                    SLMResp.Batch.BatchTotal.BatchAmountSales = "BatchTotal.BatchAmountSales";
                    SLMResp.Batch.BatchTotal.BatchAmountRefunds = "BatchTotal.BatchAmountRefunds";
                }

                public BatchTotal()
                {
                }
            }

            public class BeneficialExtRecord1
            {
                public readonly static string CreditPlan;

                public readonly static string DepartmentCode;

                public readonly static string SKUNumber;

                public readonly static string ItemDescription;

                public readonly static string StoreNumber;

                static BeneficialExtRecord1()
                {
                    SLMResp.Batch.BeneficialExtRecord1.CreditPlan = "BeneficialExtRecord1.CreditPlan";
                    SLMResp.Batch.BeneficialExtRecord1.DepartmentCode = "BeneficialExtRecord1.DepartmentCode";
                    SLMResp.Batch.BeneficialExtRecord1.SKUNumber = "BeneficialExtRecord1.SKUNumber";
                    SLMResp.Batch.BeneficialExtRecord1.ItemDescription = "BeneficialExtRecord1.ItemDescription";
                    SLMResp.Batch.BeneficialExtRecord1.StoreNumber = "BeneficialExtRecord1.StoreNumber";
                }

                public BeneficialExtRecord1()
                {
                }
            }

            public class BMLExtRecord1
            {
                public readonly static string ShippingCost;

                public readonly static string TCVersion;

                public readonly static string CustomerRegDate;

                public readonly static string CustomerTypeFlag;

                public readonly static string ItemCategory;

                public readonly static string PreApprovalInvitationNumber;

                public readonly static string MerchantPromoCode;

                public readonly static string CustomerPasswordChange;

                public readonly static string CustomerBillingAddressChange;

                public readonly static string CustomerEmailChange;

                public readonly static string CustomerHomePhoneChange;

                static BMLExtRecord1()
                {
                    SLMResp.Batch.BMLExtRecord1.ShippingCost = "BMLExtRecord1.ShippingCost";
                    SLMResp.Batch.BMLExtRecord1.TCVersion = "BMLExtRecord1.TCVersion";
                    SLMResp.Batch.BMLExtRecord1.CustomerRegDate = "BMLExtRecord1.CustomerRegDate";
                    SLMResp.Batch.BMLExtRecord1.CustomerTypeFlag = "BMLExtRecord1.CustomerTypeFlag";
                    SLMResp.Batch.BMLExtRecord1.ItemCategory = "BMLExtRecord1.ItemCategory";
                    SLMResp.Batch.BMLExtRecord1.PreApprovalInvitationNumber = "BMLExtRecord1.PreApprovalInvitationNumber";
                    SLMResp.Batch.BMLExtRecord1.MerchantPromoCode = "BMLExtRecord1.MerchantPromoCode";
                    SLMResp.Batch.BMLExtRecord1.CustomerPasswordChange = "BMLExtRecord1.CustomerPasswordChange";
                    SLMResp.Batch.BMLExtRecord1.CustomerBillingAddressChange = "BMLExtRecord1.CustomerBillingAddressChange";
                    SLMResp.Batch.BMLExtRecord1.CustomerEmailChange = "BMLExtRecord1.CustomerEmailChange";
                    SLMResp.Batch.BMLExtRecord1.CustomerHomePhoneChange = "BMLExtRecord1.CustomerHomePhoneChange";
                }

                public BMLExtRecord1()
                {
                }
            }

            public class BMLPrivateLabelExtRecord1
            {
                public readonly static string ShippingCost;

                public readonly static string TCVersion;

                public readonly static string CustomerRegDate;

                public readonly static string CustomerTypeFlag;

                public readonly static string ItemCategory;

                public readonly static string PreApprovalInvitationNumber;

                public readonly static string MerchantPromoCode;

                public readonly static string ProductType;

                public readonly static string SecretQuestionCode;

                public readonly static string SecretAnswer;

                public readonly static string ExpirationDate;

                public readonly static string CreditLine;

                public readonly static string PromoOffer;

                static BMLPrivateLabelExtRecord1()
                {
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.ShippingCost = "BMLPrivateLabelExtRecord1.ShippingCost";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.TCVersion = "BMLPrivateLabelExtRecord1.TCVersion";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.CustomerRegDate = "BMLPrivateLabelExtRecord1.CustomerRegDate";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.CustomerTypeFlag = "BMLPrivateLabelExtRecord1.CustomerTypeFlag";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.ItemCategory = "BMLPrivateLabelExtRecord1.ItemCategory";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.PreApprovalInvitationNumber = "BMLPrivateLabelExtRecord1.PreApprovalInvitationNumber";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.MerchantPromoCode = "BMLPrivateLabelExtRecord1.MerchantPromoCode";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.ProductType = "BMLPrivateLabelExtRecord1.ProductType";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.SecretQuestionCode = "BMLPrivateLabelExtRecord1.SecretQuestionCode";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.SecretAnswer = "BMLPrivateLabelExtRecord1.SecretAnswer";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.ExpirationDate = "BMLPrivateLabelExtRecord1.ExpirationDate";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.CreditLine = "BMLPrivateLabelExtRecord1.CreditLine";
                    SLMResp.Batch.BMLPrivateLabelExtRecord1.PromoOffer = "BMLPrivateLabelExtRecord1.PromoOffer";
                }

                public BMLPrivateLabelExtRecord1()
                {
                }
            }

            public class CardIssuingStatus1
            {
                public readonly static string CountryStatus;

                public readonly static string CountryCode;

                static CardIssuingStatus1()
                {
                    SLMResp.Batch.CardIssuingStatus1.CountryStatus = "CardIssuingStatus1.CountryStatus";
                    SLMResp.Batch.CardIssuingStatus1.CountryCode = "CardIssuingStatus1.CountryCode";
                }

                public CardIssuingStatus1()
                {
                }
            }

            public class CardTypeIndicatorRequestResponse
            {
                public readonly static string RecordType;

                public readonly static string ProductRecordVersionNumber;

                static CardTypeIndicatorRequestResponse()
                {
                    SLMResp.Batch.CardTypeIndicatorRequestResponse.RecordType = "CardTypeIndicatorRequestResponse.RecordType";
                    SLMResp.Batch.CardTypeIndicatorRequestResponse.ProductRecordVersionNumber = "CardTypeIndicatorRequestResponse.ProductRecordVersionNumber";
                }

                public CardTypeIndicatorRequestResponse()
                {
                }
            }

            public class CardTypeIndicatorResponse
            {
                public readonly static string ProductRecordVersionNumber;

                public readonly static string CountryCodeCountryOfIssuance;

                public readonly static string DurbinRegulated;

                public readonly static string CommercialCard;

                public readonly static string PrepaidCard;

                public readonly static string PayrollCard;

                public readonly static string HealthcareCard;

                public readonly static string AffluentCategory;

                public readonly static string SignatureDebit;

                public readonly static string PINlessDebit;

                public readonly static string Level3Eligible;

                static CardTypeIndicatorResponse()
                {
                    SLMResp.Batch.CardTypeIndicatorResponse.ProductRecordVersionNumber = "CardTypeIndicatorResponse.ProductRecordVersionNumber";
                    SLMResp.Batch.CardTypeIndicatorResponse.CountryCodeCountryOfIssuance = "CardTypeIndicatorResponse.CountryCodeCountryOfIssuance";
                    SLMResp.Batch.CardTypeIndicatorResponse.DurbinRegulated = "CardTypeIndicatorResponse.DurbinRegulated";
                    SLMResp.Batch.CardTypeIndicatorResponse.CommercialCard = "CardTypeIndicatorResponse.CommercialCard";
                    SLMResp.Batch.CardTypeIndicatorResponse.PrepaidCard = "CardTypeIndicatorResponse.PrepaidCard";
                    SLMResp.Batch.CardTypeIndicatorResponse.PayrollCard = "CardTypeIndicatorResponse.PayrollCard";
                    SLMResp.Batch.CardTypeIndicatorResponse.HealthcareCard = "CardTypeIndicatorResponse.HealthcareCard";
                    SLMResp.Batch.CardTypeIndicatorResponse.AffluentCategory = "CardTypeIndicatorResponse.AffluentCategory";
                    SLMResp.Batch.CardTypeIndicatorResponse.SignatureDebit = "CardTypeIndicatorResponse.SignatureDebit";
                    SLMResp.Batch.CardTypeIndicatorResponse.PINlessDebit = "CardTypeIndicatorResponse.PINlessDebit";
                    SLMResp.Batch.CardTypeIndicatorResponse.Level3Eligible = "CardTypeIndicatorResponse.Level3Eligible";
                }

                public CardTypeIndicatorResponse()
                {
                }
            }

            public class CashBack1
            {
                public readonly static string CashBackAmountRequested;

                public readonly static string CashBackAmountApproved;

                static CashBack1()
                {
                    SLMResp.Batch.CashBack1.CashBackAmountRequested = "CashBack1.CashBackAmountRequested";
                    SLMResp.Batch.CashBack1.CashBackAmountApproved = "CashBack1.CashBackAmountApproved";
                }

                public CashBack1()
                {
                }
            }

            public class ChaseCardService1
            {
                public readonly static string MerchantDirectRewardID;

                public readonly static string MerchantDirectPromoID;

                public readonly static string StoreNumber;

                public readonly static string TransactionDate;

                public readonly static string TransactionTime;

                public readonly static string TicketNumber;

                public readonly static string TerminalID;

                public readonly static string SalesTaxAmount;

                public readonly static string TotalTicketAmount;

                public readonly static string AlternatePayType1;

                public readonly static string AlternatePayType2;

                public readonly static string AlternatePayType3;

                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string POSAuthorizationSource;

                public readonly static string POSCardIDMethod;

                public readonly static string POSConditionCode;

                public readonly static string AuthorizedAmount;

                public readonly static string MerchantCategoryCode;

                public readonly static string MerchantPromoAmount;

                public readonly static string MerchantPromoCredDebInd;

                static ChaseCardService1()
                {
                    SLMResp.Batch.ChaseCardService1.MerchantDirectRewardID = "ChaseCardService1.MerchantDirectRewardID";
                    SLMResp.Batch.ChaseCardService1.MerchantDirectPromoID = "ChaseCardService1.MerchantDirectPromoID";
                    SLMResp.Batch.ChaseCardService1.StoreNumber = "ChaseCardService1.StoreNumber";
                    SLMResp.Batch.ChaseCardService1.TransactionDate = "ChaseCardService1.TransactionDate";
                    SLMResp.Batch.ChaseCardService1.TransactionTime = "ChaseCardService1.TransactionTime";
                    SLMResp.Batch.ChaseCardService1.TicketNumber = "ChaseCardService1.TicketNumber";
                    SLMResp.Batch.ChaseCardService1.TerminalID = "ChaseCardService1.TerminalID";
                    SLMResp.Batch.ChaseCardService1.SalesTaxAmount = "ChaseCardService1.SalesTaxAmount";
                    SLMResp.Batch.ChaseCardService1.TotalTicketAmount = "ChaseCardService1.TotalTicketAmount";
                    SLMResp.Batch.ChaseCardService1.AlternatePayType1 = "ChaseCardService1.AlternatePayType1";
                    SLMResp.Batch.ChaseCardService1.AlternatePayType2 = "ChaseCardService1.AlternatePayType2";
                    SLMResp.Batch.ChaseCardService1.AlternatePayType3 = "ChaseCardService1.AlternatePayType3";
                    SLMResp.Batch.ChaseCardService1.POSCapabilityCode = "ChaseCardService1.POSCapabilityCode";
                    SLMResp.Batch.ChaseCardService1.POSEntryMode = "ChaseCardService1.POSEntryMode";
                    SLMResp.Batch.ChaseCardService1.POSAuthorizationSource = "ChaseCardService1.POSAuthorizationSource";
                    SLMResp.Batch.ChaseCardService1.POSCardIDMethod = "ChaseCardService1.POSCardIDMethod";
                    SLMResp.Batch.ChaseCardService1.POSConditionCode = "ChaseCardService1.POSConditionCode";
                    SLMResp.Batch.ChaseCardService1.AuthorizedAmount = "ChaseCardService1.AuthorizedAmount";
                    SLMResp.Batch.ChaseCardService1.MerchantCategoryCode = "ChaseCardService1.MerchantCategoryCode";
                    SLMResp.Batch.ChaseCardService1.MerchantPromoAmount = "ChaseCardService1.MerchantPromoAmount";
                    SLMResp.Batch.ChaseCardService1.MerchantPromoCredDebInd = "ChaseCardService1.MerchantPromoCredDebInd";
                }

                public ChaseCardService1()
                {
                }
            }

            public class ChaseCardService2
            {
                public readonly static string ExcludedAmount;

                static ChaseCardService2()
                {
                    SLMResp.Batch.ChaseCardService2.ExcludedAmount = "ChaseCardService2.ExcludedAmount";
                }

                public ChaseCardService2()
                {
                }
            }

            public class ChaseCardServicesLineItemDataRecord1
            {
                public readonly static string SequenceNumber;

                public readonly static string Description;

                public readonly static string SKUNumber;

                public readonly static string Quantity;

                public readonly static string Price;

                public readonly static string ProductType;

                static ChaseCardServicesLineItemDataRecord1()
                {
                    SLMResp.Batch.ChaseCardServicesLineItemDataRecord1.SequenceNumber = "ChaseCardServicesLineItemDataRecord1.SequenceNumber";
                    SLMResp.Batch.ChaseCardServicesLineItemDataRecord1.Description = "ChaseCardServicesLineItemDataRecord1.Description";
                    SLMResp.Batch.ChaseCardServicesLineItemDataRecord1.SKUNumber = "ChaseCardServicesLineItemDataRecord1.SKUNumber";
                    SLMResp.Batch.ChaseCardServicesLineItemDataRecord1.Quantity = "ChaseCardServicesLineItemDataRecord1.Quantity";
                    SLMResp.Batch.ChaseCardServicesLineItemDataRecord1.Price = "ChaseCardServicesLineItemDataRecord1.Price";
                    SLMResp.Batch.ChaseCardServicesLineItemDataRecord1.ProductType = "ChaseCardServicesLineItemDataRecord1.ProductType";
                }

                public ChaseCardServicesLineItemDataRecord1()
                {
                }
            }

            public class ChaseCardServicesPaymentDataRecord1
            {
                public readonly static string PaymentType;

                public readonly static string ReasonCode;

                public readonly static string RDFIID;

                public readonly static string CheckAccountNumber;

                public readonly static string CheckSerialNumber;

                public readonly static string PaymentAdjustmentType;

                static ChaseCardServicesPaymentDataRecord1()
                {
                    SLMResp.Batch.ChaseCardServicesPaymentDataRecord1.PaymentType = "ChaseCardServicesPaymentDataRecord1.PaymentType";
                    SLMResp.Batch.ChaseCardServicesPaymentDataRecord1.ReasonCode = "ChaseCardServicesPaymentDataRecord1.ReasonCode";
                    SLMResp.Batch.ChaseCardServicesPaymentDataRecord1.RDFIID = "ChaseCardServicesPaymentDataRecord1.RDFIID";
                    SLMResp.Batch.ChaseCardServicesPaymentDataRecord1.CheckAccountNumber = "ChaseCardServicesPaymentDataRecord1.CheckAccountNumber";
                    SLMResp.Batch.ChaseCardServicesPaymentDataRecord1.CheckSerialNumber = "ChaseCardServicesPaymentDataRecord1.CheckSerialNumber";
                    SLMResp.Batch.ChaseCardServicesPaymentDataRecord1.PaymentAdjustmentType = "ChaseCardServicesPaymentDataRecord1.PaymentAdjustmentType";
                }

                public ChaseCardServicesPaymentDataRecord1()
                {
                }
            }

            public class ChaseNetAuthCreditExtRecord2
            {
                public readonly static string TransactionID;

                public readonly static string CAVV;

                public readonly static string CAVVResponseCode;

                static ChaseNetAuthCreditExtRecord2()
                {
                    SLMResp.Batch.ChaseNetAuthCreditExtRecord2.TransactionID = "ChaseNetAuthCreditExtRecord2.TransactionID";
                    SLMResp.Batch.ChaseNetAuthCreditExtRecord2.CAVV = "ChaseNetAuthCreditExtRecord2.CAVV";
                    SLMResp.Batch.ChaseNetAuthCreditExtRecord2.CAVVResponseCode = "ChaseNetAuthCreditExtRecord2.CAVVResponseCode";
                }

                public ChaseNetAuthCreditExtRecord2()
                {
                }
            }

            public class ChaseNetAuthSigDebitExtRecord2
            {
                public readonly static string TransactionID;

                public readonly static string CAVV;

                public readonly static string CAVVResponseCode;

                static ChaseNetAuthSigDebitExtRecord2()
                {
                    SLMResp.Batch.ChaseNetAuthSigDebitExtRecord2.TransactionID = "ChaseNetAuthSigDebitExtRecord2.TransactionID";
                    SLMResp.Batch.ChaseNetAuthSigDebitExtRecord2.CAVV = "ChaseNetAuthSigDebitExtRecord2.CAVV";
                    SLMResp.Batch.ChaseNetAuthSigDebitExtRecord2.CAVVResponseCode = "ChaseNetAuthSigDebitExtRecord2.CAVVResponseCode";
                }

                public ChaseNetAuthSigDebitExtRecord2()
                {
                }
            }

            public class ChaseNetCreditExtRecord1
            {
                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string POSAuthSource;

                public readonly static string POSCardIDMethod;

                public readonly static string AuthCharacteristicIndicator;

                public readonly static string TransactionID;

                public readonly static string ValidationCode;

                public readonly static string AuthorizedAmount;

                public readonly static string MerchCategoryCode;

                public readonly static string TotalAuthAmount;

                public readonly static string MarketSpecificDataIndicator;

                public readonly static string CATType;

                public readonly static string CardLevelResult;

                public readonly static string PartialAuthIndicator;

                public readonly static string AuthorizationResponseCode;

                public readonly static string SpendQualifiedIndicator;

                static ChaseNetCreditExtRecord1()
                {
                    SLMResp.Batch.ChaseNetCreditExtRecord1.POSCapabilityCode = "ChaseNetCreditExtRecord1.POSCapabilityCode";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.POSEntryMode = "ChaseNetCreditExtRecord1.POSEntryMode";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.POSAuthSource = "ChaseNetCreditExtRecord1.POSAuthSource";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.POSCardIDMethod = "ChaseNetCreditExtRecord1.POSCardIDMethod";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.AuthCharacteristicIndicator = "ChaseNetCreditExtRecord1.AuthCharacteristicIndicator";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.TransactionID = "ChaseNetCreditExtRecord1.TransactionID";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.ValidationCode = "ChaseNetCreditExtRecord1.ValidationCode";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.AuthorizedAmount = "ChaseNetCreditExtRecord1.AuthorizedAmount";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.MerchCategoryCode = "ChaseNetCreditExtRecord1.MerchCategoryCode";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.TotalAuthAmount = "ChaseNetCreditExtRecord1.TotalAuthAmount";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.MarketSpecificDataIndicator = "ChaseNetCreditExtRecord1.MarketSpecificDataIndicator";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.CATType = "ChaseNetCreditExtRecord1.CATType";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.CardLevelResult = "ChaseNetCreditExtRecord1.CardLevelResult";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.PartialAuthIndicator = "ChaseNetCreditExtRecord1.PartialAuthIndicator";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.AuthorizationResponseCode = "ChaseNetCreditExtRecord1.AuthorizationResponseCode";
                    SLMResp.Batch.ChaseNetCreditExtRecord1.SpendQualifiedIndicator = "ChaseNetCreditExtRecord1.SpendQualifiedIndicator";
                }

                public ChaseNetCreditExtRecord1()
                {
                }
            }

            public class ChaseNetCreditPassengerTransSuppItinerary
            {
                public readonly static string ClearingSequenceNumber;

                public readonly static string ClearingCount;

                public readonly static string TotalClearingAmount;

                public readonly static string ComputerizedReservationSystem;

                static ChaseNetCreditPassengerTransSuppItinerary()
                {
                    SLMResp.Batch.ChaseNetCreditPassengerTransSuppItinerary.ClearingSequenceNumber = "ChaseNetCreditPassengerTransSuppItinerary.ClearingSequenceNumber";
                    SLMResp.Batch.ChaseNetCreditPassengerTransSuppItinerary.ClearingCount = "ChaseNetCreditPassengerTransSuppItinerary.ClearingCount";
                    SLMResp.Batch.ChaseNetCreditPassengerTransSuppItinerary.TotalClearingAmount = "ChaseNetCreditPassengerTransSuppItinerary.TotalClearingAmount";
                    SLMResp.Batch.ChaseNetCreditPassengerTransSuppItinerary.ComputerizedReservationSystem = "ChaseNetCreditPassengerTransSuppItinerary.ComputerizedReservationSystem";
                }

                public ChaseNetCreditPassengerTransSuppItinerary()
                {
                }
            }

            public class ChaseNetSigDebitExtRecord1
            {
                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string POSAuthSource;

                public readonly static string POSCardIDMethod;

                public readonly static string AuthCharacteristicIndicator;

                public readonly static string TransactionID;

                public readonly static string ValidationCode;

                public readonly static string AuthorizedAmount;

                public readonly static string MerchCategoryCode;

                public readonly static string TotalAuthAmount;

                public readonly static string MarketSpecificDataIndicator;

                public readonly static string CATType;

                public readonly static string CardLevelResult;

                public readonly static string PartialAuthIndicator;

                public readonly static string AuthorizationResponseCode;

                public readonly static string SpendQualifiedIndicator;

                static ChaseNetSigDebitExtRecord1()
                {
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.POSCapabilityCode = "ChaseNetSigDebitExtRecord1.POSCapabilityCode";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.POSEntryMode = "ChaseNetSigDebitExtRecord1.POSEntryMode";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.POSAuthSource = "ChaseNetSigDebitExtRecord1.POSAuthSource";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.POSCardIDMethod = "ChaseNetSigDebitExtRecord1.POSCardIDMethod";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.AuthCharacteristicIndicator = "ChaseNetSigDebitExtRecord1.AuthCharacteristicIndicator";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.TransactionID = "ChaseNetSigDebitExtRecord1.TransactionID";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.ValidationCode = "ChaseNetSigDebitExtRecord1.ValidationCode";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.AuthorizedAmount = "ChaseNetSigDebitExtRecord1.AuthorizedAmount";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.MerchCategoryCode = "ChaseNetSigDebitExtRecord1.MerchCategoryCode";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.TotalAuthAmount = "ChaseNetSigDebitExtRecord1.TotalAuthAmount";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.MarketSpecificDataIndicator = "ChaseNetSigDebitExtRecord1.MarketSpecificDataIndicator";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.CATType = "ChaseNetSigDebitExtRecord1.CATType";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.CardLevelResult = "ChaseNetSigDebitExtRecord1.CardLevelResult";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.PartialAuthIndicator = "ChaseNetSigDebitExtRecord1.PartialAuthIndicator";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.AuthorizationResponseCode = "ChaseNetSigDebitExtRecord1.AuthorizationResponseCode";
                    SLMResp.Batch.ChaseNetSigDebitExtRecord1.SpendQualifiedIndicator = "ChaseNetSigDebitExtRecord1.SpendQualifiedIndicator";
                }

                public ChaseNetSigDebitExtRecord1()
                {
                }
            }

            public class ChaseNetSigDebitPassengerTransSuppItinerary
            {
                public readonly static string ClearingSequenceNumber;

                public readonly static string ClearingCount;

                public readonly static string TotalClearingAmount;

                public readonly static string ComputerizedReservationSystem;

                static ChaseNetSigDebitPassengerTransSuppItinerary()
                {
                    SLMResp.Batch.ChaseNetSigDebitPassengerTransSuppItinerary.ClearingSequenceNumber = "ChaseNetSigDebitPassengerTransSuppItinerary.ClearingSequenceNumber";
                    SLMResp.Batch.ChaseNetSigDebitPassengerTransSuppItinerary.ClearingCount = "ChaseNetSigDebitPassengerTransSuppItinerary.ClearingCount";
                    SLMResp.Batch.ChaseNetSigDebitPassengerTransSuppItinerary.TotalClearingAmount = "ChaseNetSigDebitPassengerTransSuppItinerary.TotalClearingAmount";
                    SLMResp.Batch.ChaseNetSigDebitPassengerTransSuppItinerary.ComputerizedReservationSystem = "ChaseNetSigDebitPassengerTransSuppItinerary.ComputerizedReservationSystem";
                }

                public ChaseNetSigDebitPassengerTransSuppItinerary()
                {
                }
            }

            public class CHNBIN
            {
                public CHNBIN()
                {
                }

                public class CHNBINDetail
                {
                    public readonly static string RecordType;

                    public readonly static string BINLow;

                    public readonly static string BINHigh;

                    public readonly static string MaximumPANLength;

                    public readonly static string MethodOfPayment;

                    public readonly static string CardProductIndicator;

                    public readonly static string PINCapability;

                    public readonly static string BINUpdateDate;

                    public readonly static string ConsumerDigitalPaymentechTokenIndicator;

                    static CHNBINDetail()
                    {
                        SLMResp.Batch.CHNBIN.CHNBINDetail.RecordType = "CHNBINDetail.RecordType";
                        SLMResp.Batch.CHNBIN.CHNBINDetail.BINLow = "CHNBINDetail.BINLow";
                        SLMResp.Batch.CHNBIN.CHNBINDetail.BINHigh = "CHNBINDetail.BINHigh";
                        SLMResp.Batch.CHNBIN.CHNBINDetail.MaximumPANLength = "CHNBINDetail.MaximumPANLength";
                        SLMResp.Batch.CHNBIN.CHNBINDetail.MethodOfPayment = "CHNBINDetail.MethodOfPayment";
                        SLMResp.Batch.CHNBIN.CHNBINDetail.CardProductIndicator = "CHNBINDetail.CardProductIndicator";
                        SLMResp.Batch.CHNBIN.CHNBINDetail.PINCapability = "CHNBINDetail.PINCapability";
                        SLMResp.Batch.CHNBIN.CHNBINDetail.BINUpdateDate = "CHNBINDetail.BINUpdateDate";
                        SLMResp.Batch.CHNBIN.CHNBINDetail.ConsumerDigitalPaymentechTokenIndicator = "CHNBINDetail.ConsumerDigitalPaymentechTokenIndicator";
                    }

                    public CHNBINDetail()
                    {
                    }
                }

                public class CHNBINHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string Version;

                    public readonly static string CreationDate;

                    static CHNBINHeader()
                    {
                        SLMResp.Batch.CHNBIN.CHNBINHeader.RecordType = "CHNBINHeader.RecordType";
                        SLMResp.Batch.CHNBIN.CHNBINHeader.FileName = "CHNBINHeader.FileName";
                        SLMResp.Batch.CHNBIN.CHNBINHeader.Version = "CHNBINHeader.Version";
                        SLMResp.Batch.CHNBIN.CHNBINHeader.CreationDate = "CHNBINHeader.CreationDate";
                    }

                    public CHNBINHeader()
                    {
                    }
                }

                public class CHNBINTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDate;

                    public readonly static string NumberOfBinRecords;

                    public readonly static string NumberOfBINRecords;

                    static CHNBINTrailer()
                    {
                        SLMResp.Batch.CHNBIN.CHNBINTrailer.RecordType = "CHNBINTrailer.RecordType";
                        SLMResp.Batch.CHNBIN.CHNBINTrailer.CreationDate = "CHNBINTrailer.CreationDate";
                        SLMResp.Batch.CHNBIN.CHNBINTrailer.NumberOfBinRecords = "CHNBINTrailer.NumberOfBinRecords";
                        SLMResp.Batch.CHNBIN.CHNBINTrailer.NumberOfBINRecords = "CHNBINTrailer.NumberOfBINRecords";
                    }

                    public CHNBINTrailer()
                    {
                    }
                }
            }

            public class CNandVisaLevel3LineItemRecord1
            {
                public readonly static string SeqNumber;

                public readonly static string Description;

                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string UnitOfMeasure;

                public readonly static string TaxAmount;

                public readonly static string TaxRate;

                static CNandVisaLevel3LineItemRecord1()
                {
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord1.SeqNumber = "CNandVisaLevel3LineItemRecord1.SeqNumber";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord1.Description = "CNandVisaLevel3LineItemRecord1.Description";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord1.ProductCode = "CNandVisaLevel3LineItemRecord1.ProductCode";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord1.Quantity = "CNandVisaLevel3LineItemRecord1.Quantity";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord1.UnitOfMeasure = "CNandVisaLevel3LineItemRecord1.UnitOfMeasure";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord1.TaxAmount = "CNandVisaLevel3LineItemRecord1.TaxAmount";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord1.TaxRate = "CNandVisaLevel3LineItemRecord1.TaxRate";
                }

                public CNandVisaLevel3LineItemRecord1()
                {
                }
            }

            public class CNandVisaLevel3LineItemRecord2
            {
                public readonly static string SeqNumber;

                public readonly static string LineItemTotal;

                public readonly static string DiscountAmount;

                public readonly static string ItemCommodityCode;

                public readonly static string UnitCost;

                public readonly static string LineItemLevelDiscountTreatmentCode;

                public readonly static string LineItemDetailIndicator;

                static CNandVisaLevel3LineItemRecord2()
                {
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord2.SeqNumber = "CNandVisaLevel3LineItemRecord2.SeqNumber";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord2.LineItemTotal = "CNandVisaLevel3LineItemRecord2.LineItemTotal";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord2.DiscountAmount = "CNandVisaLevel3LineItemRecord2.DiscountAmount";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord2.ItemCommodityCode = "CNandVisaLevel3LineItemRecord2.ItemCommodityCode";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord2.UnitCost = "CNandVisaLevel3LineItemRecord2.UnitCost";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord2.LineItemLevelDiscountTreatmentCode = "CNandVisaLevel3LineItemRecord2.LineItemLevelDiscountTreatmentCode";
                    SLMResp.Batch.CNandVisaLevel3LineItemRecord2.LineItemDetailIndicator = "CNandVisaLevel3LineItemRecord2.LineItemDetailIndicator";
                }

                public CNandVisaLevel3LineItemRecord2()
                {
                }
            }

            public class CNandVisaLevel3OrderRecord
            {
                public readonly static string FreightAmount;

                public readonly static string DutyAmount;

                public readonly static string DestinationPostalCode;

                public readonly static string DestinationCountryCode;

                public readonly static string ShipFromPostalCode;

                public readonly static string DiscountApplied;

                public readonly static string TaxAmount;

                public readonly static string TaxRate;

                public readonly static string ShippingTaxRate;

                public readonly static string InvoiceDiscountTreatment;

                public readonly static string TaxTreatment;

                public readonly static string DiscountAmountSign;

                public readonly static string FreightAmountSign;

                public readonly static string DutyAmountSign;

                public readonly static string VATTaxAmountSign;

                public readonly static string UniqueVATInvoiceReferenceNumber;

                public readonly static string VATTaxAmount;

                static CNandVisaLevel3OrderRecord()
                {
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.FreightAmount = "CNandVisaLevel3OrderRecord.FreightAmount";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.DutyAmount = "CNandVisaLevel3OrderRecord.DutyAmount";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.DestinationPostalCode = "CNandVisaLevel3OrderRecord.DestinationPostalCode";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.DestinationCountryCode = "CNandVisaLevel3OrderRecord.DestinationCountryCode";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.ShipFromPostalCode = "CNandVisaLevel3OrderRecord.ShipFromPostalCode";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.DiscountApplied = "CNandVisaLevel3OrderRecord.DiscountApplied";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.TaxAmount = "CNandVisaLevel3OrderRecord.TaxAmount";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.TaxRate = "CNandVisaLevel3OrderRecord.TaxRate";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.ShippingTaxRate = "CNandVisaLevel3OrderRecord.ShippingTaxRate";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.InvoiceDiscountTreatment = "CNandVisaLevel3OrderRecord.InvoiceDiscountTreatment";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.TaxTreatment = "CNandVisaLevel3OrderRecord.TaxTreatment";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.DiscountAmountSign = "CNandVisaLevel3OrderRecord.DiscountAmountSign";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.FreightAmountSign = "CNandVisaLevel3OrderRecord.FreightAmountSign";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.DutyAmountSign = "CNandVisaLevel3OrderRecord.DutyAmountSign";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.VATTaxAmountSign = "CNandVisaLevel3OrderRecord.VATTaxAmountSign";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.UniqueVATInvoiceReferenceNumber = "CNandVisaLevel3OrderRecord.UniqueVATInvoiceReferenceNumber";
                    SLMResp.Batch.CNandVisaLevel3OrderRecord.VATTaxAmount = "CNandVisaLevel3OrderRecord.VATTaxAmount";
                }

                public CNandVisaLevel3OrderRecord()
                {
                }
            }

            public class CommercialBIN
            {
                public CommercialBIN()
                {
                }

                public class CommBinDetail
                {
                    public readonly static string RecordType;

                    public readonly static string LowRange;

                    public readonly static string HighRange;

                    public readonly static string BusinessCardIndicator;

                    public readonly static string PCIndicator;

                    public readonly static string CorpTECardIndicator;

                    public readonly static string FleetCardIndicator;

                    public readonly static string EPaymentIndicator;

                    public readonly static string CountryCode;

                    static CommBinDetail()
                    {
                        SLMResp.Batch.CommercialBIN.CommBinDetail.RecordType = "CommBinDetail.RecordType";
                        SLMResp.Batch.CommercialBIN.CommBinDetail.LowRange = "CommBinDetail.LowRange";
                        SLMResp.Batch.CommercialBIN.CommBinDetail.HighRange = "CommBinDetail.HighRange";
                        SLMResp.Batch.CommercialBIN.CommBinDetail.BusinessCardIndicator = "CommBinDetail.BusinessCardIndicator";
                        SLMResp.Batch.CommercialBIN.CommBinDetail.PCIndicator = "CommBinDetail.PCIndicator";
                        SLMResp.Batch.CommercialBIN.CommBinDetail.CorpTECardIndicator = "CommBinDetail.CorpTECardIndicator";
                        SLMResp.Batch.CommercialBIN.CommBinDetail.FleetCardIndicator = "CommBinDetail.FleetCardIndicator";
                        SLMResp.Batch.CommercialBIN.CommBinDetail.EPaymentIndicator = "CommBinDetail.EPaymentIndicator";
                        SLMResp.Batch.CommercialBIN.CommBinDetail.CountryCode = "CommBinDetail.CountryCode";
                    }

                    public CommBinDetail()
                    {
                    }
                }

                public class CommBinHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string CreationDate;

                    public readonly static string VersionNumber;

                    static CommBinHeader()
                    {
                        SLMResp.Batch.CommercialBIN.CommBinHeader.RecordType = "CommBinHeader.RecordType";
                        SLMResp.Batch.CommercialBIN.CommBinHeader.FileName = "CommBinHeader.FileName";
                        SLMResp.Batch.CommercialBIN.CommBinHeader.CreationDate = "CommBinHeader.CreationDate";
                        SLMResp.Batch.CommercialBIN.CommBinHeader.VersionNumber = "CommBinHeader.VersionNumber";
                    }

                    public CommBinHeader()
                    {
                    }
                }

                public class CommBinTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDate;

                    public readonly static string NumberOfBINrecords;

                    public readonly static string NumberOfBINRecords;

                    static CommBinTrailer()
                    {
                        SLMResp.Batch.CommercialBIN.CommBinTrailer.RecordType = "CommBinTrailer.RecordType";
                        SLMResp.Batch.CommercialBIN.CommBinTrailer.CreationDate = "CommBinTrailer.CreationDate";
                        SLMResp.Batch.CommercialBIN.CommBinTrailer.NumberOfBINrecords = "CommBinTrailer.NumberOfBINrecords";
                        SLMResp.Batch.CommercialBIN.CommBinTrailer.NumberOfBINRecords = "CommBinTrailer.NumberOfBINRecords";
                    }

                    public CommBinTrailer()
                    {
                    }
                }
            }

            public class ConsumerIPAddress
            {
                public readonly static string AddressSubType;

                public readonly static string CustomerIPAddress;

                static ConsumerIPAddress()
                {
                    SLMResp.Batch.ConsumerIPAddress.AddressSubType = "ConsumerIPAddress.AddressSubType";
                    SLMResp.Batch.ConsumerIPAddress.CustomerIPAddress = "ConsumerIPAddress.CustomerIPAddress";
                }

                public ConsumerIPAddress()
                {
                }
            }

            public class ConsumerName
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static ConsumerName()
                {
                    SLMResp.Batch.ConsumerName.AddressLine = "ConsumerName.AddressLine";
                    SLMResp.Batch.ConsumerName.TelephoneType = "ConsumerName.TelephoneType";
                    SLMResp.Batch.ConsumerName.TelephoneNumber = "ConsumerName.TelephoneNumber";
                    SLMResp.Batch.ConsumerName.CountryCode = "ConsumerName.CountryCode";
                }

                public ConsumerName()
                {
                }
            }

            public class CustomerANI
            {
                public readonly static string CustomerANI_;

                public readonly static string CustomerInfoID;

                static CustomerANI()
                {
                    SLMResp.Batch.CustomerANI.CustomerANI_ = "CustomerANI.CustomerANI_";
                    SLMResp.Batch.CustomerANI.CustomerInfoID = "CustomerANI.CustomerInfoID";
                }

                public CustomerANI()
                {
                }
            }

            public class CustomerBrowserName
            {
                public readonly static string CustomerBrowserName_;

                static CustomerBrowserName()
                {
                    SLMResp.Batch.CustomerBrowserName.CustomerBrowserName_ = "CustomerBrowserName.CustomerBrowserName_";
                }

                public CustomerBrowserName()
                {
                }
            }

            public class CustomerEmailAddress
            {
                public readonly static string AddressSubType;

                public readonly static string CustomerEmailAddress_;

                static CustomerEmailAddress()
                {
                    SLMResp.Batch.CustomerEmailAddress.AddressSubType = "CustomerEmailAddress.AddressSubType";
                    SLMResp.Batch.CustomerEmailAddress.CustomerEmailAddress_ = "CustomerEmailAddress.CustomerEmailAddress_";
                }

                public CustomerEmailAddress()
                {
                }
            }

            public class CustomerHostName
            {
                public readonly static string CustomerHostName_;

                static CustomerHostName()
                {
                    SLMResp.Batch.CustomerHostName.CustomerHostName_ = "CustomerHostName.CustomerHostName_";
                }

                public CustomerHostName()
                {
                }
            }

            public class DebitRecord1
            {
                public readonly static string TraceNumber;

                public readonly static string BillerReference;

                public readonly static string AccountType;

                public readonly static string VoucherNumber;

                public readonly static string FoodConsumerNumber;

                public readonly static string DebitRoutingData;

                static DebitRecord1()
                {
                    SLMResp.Batch.DebitRecord1.TraceNumber = "DebitRecord1.TraceNumber";
                    SLMResp.Batch.DebitRecord1.BillerReference = "DebitRecord1.BillerReference";
                    SLMResp.Batch.DebitRecord1.AccountType = "DebitRecord1.AccountType";
                    SLMResp.Batch.DebitRecord1.VoucherNumber = "DebitRecord1.VoucherNumber";
                    SLMResp.Batch.DebitRecord1.FoodConsumerNumber = "DebitRecord1.FoodConsumerNumber";
                    SLMResp.Batch.DebitRecord1.DebitRoutingData = "DebitRecord1.DebitRoutingData";
                }

                public DebitRecord1()
                {
                }
            }

            public class DFR
            {
                public readonly static string ReportType;

                static DFR()
                {
                    SLMResp.Batch.DFR.ReportType = "ReportType";
                }

                public DFR()
                {
                }

                public class AuthorizationDetail
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string Currency;

                    public readonly static string SubmissionDate;

                    public readonly static string PID;

                    public readonly static string PIDShortName;

                    public readonly static string SubmissionNumber;

                    public readonly static string RecordNumber;

                    public readonly static string TransactionDivisionNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string RDFINumber;

                    public readonly static string AccountNumber;

                    public readonly static string ExpirationDate;

                    public readonly static string Amount;

                    public readonly static string CurrencyAuthorized;

                    public readonly static string MethodOfPayment;

                    public readonly static string ActionCode;

                    public readonly static string TransactionType;

                    public readonly static string AuthPerformedDateTime;

                    public readonly static string AuthCode;

                    public readonly static string AuthResponseCode;

                    public readonly static string VendorAuthResponse;

                    public readonly static string AuthInitiator;

                    public readonly static string AuthSource;

                    public readonly static string POSAuthSource;

                    public readonly static string VoiceAuthIndicator;

                    public readonly static string VendorLine;

                    public readonly static string PTIAVSResponse;

                    public readonly static string VendorAVSResponse;

                    public readonly static string CardSecurityValueResponse;

                    public readonly static string ConsumerBankCountryCode;

                    public readonly static string TraceNumber;

                    public readonly static string MCC;

                    public readonly static string InvoiceNumber;

                    public readonly static string DealerNumber;

                    public readonly static string EnhancedAVSResponse;

                    public readonly static string Encrypt;

                    public readonly static string FraudScoreRequest;

                    public readonly static string FraudScoreResponse;

                    public readonly static string CountryofIssuance;

                    public readonly static string DurbinRegulated;

                    public readonly static string CommercialCard;

                    public readonly static string PrepaidCard;

                    public readonly static string PayrollCard;

                    public readonly static string HealthcareCard;

                    public readonly static string AffluentCard;

                    public readonly static string SignatureDebit;

                    public readonly static string PINlessDebit;

                    public readonly static string LevelIIICapable;

                    public readonly static string PreFinalAuthorization;

                    public readonly static string TokenIndicator;

                    public readonly static string BankSortCode;

                    public readonly static string IBAN;

                    public readonly static string BIC;

                    public readonly static string CreditorID;

                    public readonly static string MandateID;

                    public readonly static string MandateType;

                    public readonly static string SignatureDate;

                    static AuthorizationDetail()
                    {
                        SLMResp.Batch.DFR.AuthorizationDetail.ReportType = "AuthorizationDetail.ReportType";
                        SLMResp.Batch.DFR.AuthorizationDetail.EntityType = "AuthorizationDetail.EntityType";
                        SLMResp.Batch.DFR.AuthorizationDetail.EntityNumber = "AuthorizationDetail.EntityNumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.Currency = "AuthorizationDetail.Currency";
                        SLMResp.Batch.DFR.AuthorizationDetail.SubmissionDate = "AuthorizationDetail.SubmissionDate";
                        SLMResp.Batch.DFR.AuthorizationDetail.PID = "AuthorizationDetail.PID";
                        SLMResp.Batch.DFR.AuthorizationDetail.PIDShortName = "AuthorizationDetail.PIDShortName";
                        SLMResp.Batch.DFR.AuthorizationDetail.SubmissionNumber = "AuthorizationDetail.SubmissionNumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.RecordNumber = "AuthorizationDetail.RecordNumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.TransactionDivisionNumber = "AuthorizationDetail.TransactionDivisionNumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.MerchantOrderNumber = "AuthorizationDetail.MerchantOrderNumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.RDFINumber = "AuthorizationDetail.RDFINumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.AccountNumber = "AuthorizationDetail.AccountNumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.ExpirationDate = "AuthorizationDetail.ExpirationDate";
                        SLMResp.Batch.DFR.AuthorizationDetail.Amount = "AuthorizationDetail.Amount";
                        SLMResp.Batch.DFR.AuthorizationDetail.CurrencyAuthorized = "AuthorizationDetail.CurrencyAuthorized";
                        SLMResp.Batch.DFR.AuthorizationDetail.MethodOfPayment = "AuthorizationDetail.MethodOfPayment";
                        SLMResp.Batch.DFR.AuthorizationDetail.ActionCode = "AuthorizationDetail.ActionCode";
                        SLMResp.Batch.DFR.AuthorizationDetail.TransactionType = "AuthorizationDetail.TransactionType";
                        SLMResp.Batch.DFR.AuthorizationDetail.AuthPerformedDateTime = "AuthorizationDetail.AuthPerformedDateTime";
                        SLMResp.Batch.DFR.AuthorizationDetail.AuthCode = "AuthorizationDetail.AuthCode";
                        SLMResp.Batch.DFR.AuthorizationDetail.AuthResponseCode = "AuthorizationDetail.AuthResponseCode";
                        SLMResp.Batch.DFR.AuthorizationDetail.VendorAuthResponse = "AuthorizationDetail.VendorAuthResponse";
                        SLMResp.Batch.DFR.AuthorizationDetail.AuthInitiator = "AuthorizationDetail.AuthInitiator";
                        SLMResp.Batch.DFR.AuthorizationDetail.AuthSource = "AuthorizationDetail.AuthSource";
                        SLMResp.Batch.DFR.AuthorizationDetail.POSAuthSource = "AuthorizationDetail.POSAuthSource";
                        SLMResp.Batch.DFR.AuthorizationDetail.VoiceAuthIndicator = "AuthorizationDetail.VoiceAuthIndicator";
                        SLMResp.Batch.DFR.AuthorizationDetail.VendorLine = "AuthorizationDetail.VendorLine";
                        SLMResp.Batch.DFR.AuthorizationDetail.PTIAVSResponse = "AuthorizationDetail.PTIAVSResponse";
                        SLMResp.Batch.DFR.AuthorizationDetail.VendorAVSResponse = "AuthorizationDetail.VendorAVSResponse";
                        SLMResp.Batch.DFR.AuthorizationDetail.CardSecurityValueResponse = "AuthorizationDetail.CardSecurityValueResponse";
                        SLMResp.Batch.DFR.AuthorizationDetail.ConsumerBankCountryCode = "AuthorizationDetail.ConsumerBankCountryCode";
                        SLMResp.Batch.DFR.AuthorizationDetail.TraceNumber = "AuthorizationDetail.TraceNumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.MCC = "AuthorizationDetail.MCC";
                        SLMResp.Batch.DFR.AuthorizationDetail.InvoiceNumber = "AuthorizationDetail.InvoiceNumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.DealerNumber = "AuthorizationDetail.DealerNumber";
                        SLMResp.Batch.DFR.AuthorizationDetail.EnhancedAVSResponse = "AuthorizationDetail.EnhancedAVSResponse";
                        SLMResp.Batch.DFR.AuthorizationDetail.Encrypt = "AuthorizationDetail.Encrypt";
                        SLMResp.Batch.DFR.AuthorizationDetail.FraudScoreRequest = "AuthorizationDetail.FraudScoreRequest";
                        SLMResp.Batch.DFR.AuthorizationDetail.FraudScoreResponse = "AuthorizationDetail.FraudScoreResponse";
                        SLMResp.Batch.DFR.AuthorizationDetail.CountryofIssuance = "AuthorizationDetail.CountryofIssuance";
                        SLMResp.Batch.DFR.AuthorizationDetail.DurbinRegulated = "AuthorizationDetail.DurbinRegulated";
                        SLMResp.Batch.DFR.AuthorizationDetail.CommercialCard = "AuthorizationDetail.CommercialCard";
                        SLMResp.Batch.DFR.AuthorizationDetail.PrepaidCard = "AuthorizationDetail.PrepaidCard";
                        SLMResp.Batch.DFR.AuthorizationDetail.PayrollCard = "AuthorizationDetail.PayrollCard";
                        SLMResp.Batch.DFR.AuthorizationDetail.HealthcareCard = "AuthorizationDetail.HealthcareCard";
                        SLMResp.Batch.DFR.AuthorizationDetail.AffluentCard = "AuthorizationDetail.AffluentCard";
                        SLMResp.Batch.DFR.AuthorizationDetail.SignatureDebit = "AuthorizationDetail.SignatureDebit";
                        SLMResp.Batch.DFR.AuthorizationDetail.PINlessDebit = "AuthorizationDetail.PINlessDebit";
                        SLMResp.Batch.DFR.AuthorizationDetail.LevelIIICapable = "AuthorizationDetail.LevelIIICapable";
                        SLMResp.Batch.DFR.AuthorizationDetail.PreFinalAuthorization = "AuthorizationDetail.PreFinalAuthorization";
                        SLMResp.Batch.DFR.AuthorizationDetail.TokenIndicator = "AuthorizationDetail.TokenIndicator";
                        SLMResp.Batch.DFR.AuthorizationDetail.BankSortCode = "AuthorizationDetail.BankSortCode";
                        SLMResp.Batch.DFR.AuthorizationDetail.IBAN = "AuthorizationDetail.IBAN";
                        SLMResp.Batch.DFR.AuthorizationDetail.BIC = "AuthorizationDetail.BIC";
                        SLMResp.Batch.DFR.AuthorizationDetail.CreditorID = "AuthorizationDetail.CreditorID";
                        SLMResp.Batch.DFR.AuthorizationDetail.MandateID = "AuthorizationDetail.MandateID";
                        SLMResp.Batch.DFR.AuthorizationDetail.MandateType = "AuthorizationDetail.MandateType";
                        SLMResp.Batch.DFR.AuthorizationDetail.SignatureDate = "AuthorizationDetail.SignatureDate";
                    }

                    public AuthorizationDetail()
                    {
                    }
                }

                public class AuthorizationDetailHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static AuthorizationDetailHeader()
                    {
                        SLMResp.Batch.DFR.AuthorizationDetailHeader.ReportType = "AuthorizationDetailHeader.ReportType";
                        SLMResp.Batch.DFR.AuthorizationDetailHeader.CompanyIDNumber = "AuthorizationDetailHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.AuthorizationDetailHeader.ReportDateFrom = "AuthorizationDetailHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.AuthorizationDetailHeader.ReportDateTo = "AuthorizationDetailHeader.ReportDateTo";
                        SLMResp.Batch.DFR.AuthorizationDetailHeader.ReportGenerationDate = "AuthorizationDetailHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.AuthorizationDetailHeader.ReportGenerationTime = "AuthorizationDetailHeader.ReportGenerationTime";
                    }

                    public AuthorizationDetailHeader()
                    {
                    }
                }

                public class ChargebackActivityDetail
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string IssuerChargeBackPresentmentCurrencyAmount;

                    public readonly static string PreviousPartialRepresentment;

                    public readonly static string PresentmentCurrency;

                    public readonly static string ChargebackCategory;

                    public readonly static string StatusFlag;

                    public readonly static string SequenceNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string CardAccountNumber;

                    public readonly static string ReasonCode;

                    public readonly static string TransactionDate;

                    public readonly static string ChargebackDate;

                    public readonly static string ActivityDate;

                    public readonly static string CurrentActionPresentmentCurrencyChargebackAmount;

                    public readonly static string FeeAmount;

                    public readonly static string UsageCode;

                    public readonly static string SettlementCurrency;

                    public readonly static string IssuerCBSettlementCurrencyAmount;

                    public readonly static string CurrentActionSettlementCurrencyAmount;

                    public readonly static string MOPCode;

                    public readonly static string AuthorizationDate;

                    public readonly static string ChargeBackDueDate;

                    public readonly static string TicketNumber;

                    public readonly static string PotentialBundledChargebacks;

                    public readonly static string TokenIndicator;

                    static ChargebackActivityDetail()
                    {
                        SLMResp.Batch.DFR.ChargebackActivityDetail.ReportType = "ChargebackActivityDetail.ReportType";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.EntityType = "ChargebackActivityDetail.EntityType";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.EntityNumber = "ChargebackActivityDetail.EntityNumber";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.IssuerChargeBackPresentmentCurrencyAmount = "ChargebackActivityDetail.IssuerChargeBackPresentmentCurrencyAmount";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.PreviousPartialRepresentment = "ChargebackActivityDetail.PreviousPartialRepresentment";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.PresentmentCurrency = "ChargebackActivityDetail.PresentmentCurrency";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.ChargebackCategory = "ChargebackActivityDetail.ChargebackCategory";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.StatusFlag = "ChargebackActivityDetail.StatusFlag";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.SequenceNumber = "ChargebackActivityDetail.SequenceNumber";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.MerchantOrderNumber = "ChargebackActivityDetail.MerchantOrderNumber";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.CardAccountNumber = "ChargebackActivityDetail.CardAccountNumber";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.ReasonCode = "ChargebackActivityDetail.ReasonCode";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.TransactionDate = "ChargebackActivityDetail.TransactionDate";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.ChargebackDate = "ChargebackActivityDetail.ChargebackDate";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.ActivityDate = "ChargebackActivityDetail.ActivityDate";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.CurrentActionPresentmentCurrencyChargebackAmount = "ChargebackActivityDetail.CurrentActionPresentmentCurrencyChargebackAmount";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.FeeAmount = "ChargebackActivityDetail.FeeAmount";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.UsageCode = "ChargebackActivityDetail.UsageCode";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.SettlementCurrency = "ChargebackActivityDetail.SettlementCurrency";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.IssuerCBSettlementCurrencyAmount = "ChargebackActivityDetail.IssuerCBSettlementCurrencyAmount";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.CurrentActionSettlementCurrencyAmount = "ChargebackActivityDetail.CurrentActionSettlementCurrencyAmount";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.MOPCode = "ChargebackActivityDetail.MOPCode";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.AuthorizationDate = "ChargebackActivityDetail.AuthorizationDate";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.ChargeBackDueDate = "ChargebackActivityDetail.ChargeBackDueDate";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.TicketNumber = "ChargebackActivityDetail.TicketNumber";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.PotentialBundledChargebacks = "ChargebackActivityDetail.PotentialBundledChargebacks";
                        SLMResp.Batch.DFR.ChargebackActivityDetail.TokenIndicator = "ChargebackActivityDetail.TokenIndicator";
                    }

                    public ChargebackActivityDetail()
                    {
                    }
                }

                public class ChargebackActivityHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static ChargebackActivityHeader()
                    {
                        SLMResp.Batch.DFR.ChargebackActivityHeader.ReportType = "ChargebackActivityHeader.ReportType";
                        SLMResp.Batch.DFR.ChargebackActivityHeader.CompanyIDNumber = "ChargebackActivityHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.ChargebackActivityHeader.ReportDateFrom = "ChargebackActivityHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.ChargebackActivityHeader.ReportDateTo = "ChargebackActivityHeader.ReportDateTo";
                        SLMResp.Batch.DFR.ChargebackActivityHeader.ReportGenerationDate = "ChargebackActivityHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.ChargebackActivityHeader.ReportGenerationTime = "ChargebackActivityHeader.ReportGenerationTime";
                    }

                    public ChargebackActivityHeader()
                    {
                    }
                }

                public class ChargebackActivitySummary
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string MethodOfPaymentType;

                    public readonly static string Category;

                    public readonly static string FinancialNonFinancial;

                    public readonly static string Count;

                    public readonly static string PresentmentCurrencyAmount;

                    public readonly static string SettlementCurrency;

                    public readonly static string SettlementCurrencyAmount;

                    static ChargebackActivitySummary()
                    {
                        SLMResp.Batch.DFR.ChargebackActivitySummary.ReportType = "ChargebackActivitySummary.ReportType";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.EntityType = "ChargebackActivitySummary.EntityType";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.EntityNumber = "ChargebackActivitySummary.EntityNumber";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.PresentmentCurrency = "ChargebackActivitySummary.PresentmentCurrency";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.MethodOfPaymentType = "ChargebackActivitySummary.MethodOfPaymentType";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.Category = "ChargebackActivitySummary.Category";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.FinancialNonFinancial = "ChargebackActivitySummary.FinancialNonFinancial";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.Count = "ChargebackActivitySummary.Count";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.PresentmentCurrencyAmount = "ChargebackActivitySummary.PresentmentCurrencyAmount";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.SettlementCurrency = "ChargebackActivitySummary.SettlementCurrency";
                        SLMResp.Batch.DFR.ChargebackActivitySummary.SettlementCurrencyAmount = "ChargebackActivitySummary.SettlementCurrencyAmount";
                    }

                    public ChargebackActivitySummary()
                    {
                    }
                }

                public class ChargebacksReceived
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string Category;

                    public readonly static string StatusFlag;

                    public readonly static string SequenceNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string CardAccountNumber;

                    public readonly static string ReasonCode;

                    public readonly static string TransactionDate;

                    public readonly static string ChargebackDate;

                    public readonly static string ActivityDate;

                    public readonly static string PresentmentCurrencyChargebackAmount;

                    public readonly static string UsageCode;

                    public readonly static string SettlementCurrency;

                    public readonly static string SettlementCurrencyAmount;

                    public readonly static string TicketNumber;

                    public readonly static string PotentialBundledChargebacks;

                    public readonly static string TokenIndicator;

                    static ChargebacksReceived()
                    {
                        SLMResp.Batch.DFR.ChargebacksReceived.ReportType = "ChargebacksReceived.ReportType";
                        SLMResp.Batch.DFR.ChargebacksReceived.EntityType = "ChargebacksReceived.EntityType";
                        SLMResp.Batch.DFR.ChargebacksReceived.EntityNumber = "ChargebacksReceived.EntityNumber";
                        SLMResp.Batch.DFR.ChargebacksReceived.PresentmentCurrency = "ChargebacksReceived.PresentmentCurrency";
                        SLMResp.Batch.DFR.ChargebacksReceived.Category = "ChargebacksReceived.Category";
                        SLMResp.Batch.DFR.ChargebacksReceived.StatusFlag = "ChargebacksReceived.StatusFlag";
                        SLMResp.Batch.DFR.ChargebacksReceived.SequenceNumber = "ChargebacksReceived.SequenceNumber";
                        SLMResp.Batch.DFR.ChargebacksReceived.MerchantOrderNumber = "ChargebacksReceived.MerchantOrderNumber";
                        SLMResp.Batch.DFR.ChargebacksReceived.CardAccountNumber = "ChargebacksReceived.CardAccountNumber";
                        SLMResp.Batch.DFR.ChargebacksReceived.ReasonCode = "ChargebacksReceived.ReasonCode";
                        SLMResp.Batch.DFR.ChargebacksReceived.TransactionDate = "ChargebacksReceived.TransactionDate";
                        SLMResp.Batch.DFR.ChargebacksReceived.ChargebackDate = "ChargebacksReceived.ChargebackDate";
                        SLMResp.Batch.DFR.ChargebacksReceived.ActivityDate = "ChargebacksReceived.ActivityDate";
                        SLMResp.Batch.DFR.ChargebacksReceived.PresentmentCurrencyChargebackAmount = "ChargebacksReceived.PresentmentCurrencyChargebackAmount";
                        SLMResp.Batch.DFR.ChargebacksReceived.UsageCode = "ChargebacksReceived.UsageCode";
                        SLMResp.Batch.DFR.ChargebacksReceived.SettlementCurrency = "ChargebacksReceived.SettlementCurrency";
                        SLMResp.Batch.DFR.ChargebacksReceived.SettlementCurrencyAmount = "ChargebacksReceived.SettlementCurrencyAmount";
                        SLMResp.Batch.DFR.ChargebacksReceived.TicketNumber = "ChargebacksReceived.TicketNumber";
                        SLMResp.Batch.DFR.ChargebacksReceived.PotentialBundledChargebacks = "ChargebacksReceived.PotentialBundledChargebacks";
                        SLMResp.Batch.DFR.ChargebacksReceived.TokenIndicator = "ChargebacksReceived.TokenIndicator";
                    }

                    public ChargebacksReceived()
                    {
                    }
                }

                public class ChargebacksReceivedHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static ChargebacksReceivedHeader()
                    {
                        SLMResp.Batch.DFR.ChargebacksReceivedHeader.ReportType = "ChargebacksReceivedHeader.ReportType";
                        SLMResp.Batch.DFR.ChargebacksReceivedHeader.CompanyIDNumber = "ChargebacksReceivedHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.ChargebacksReceivedHeader.ReportDateFrom = "ChargebacksReceivedHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.ChargebacksReceivedHeader.ReportDateTo = "ChargebacksReceivedHeader.ReportDateTo";
                        SLMResp.Batch.DFR.ChargebacksReceivedHeader.ReportGenerationDate = "ChargebacksReceivedHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.ChargebacksReceivedHeader.ReportGenerationTime = "ChargebacksReceivedHeader.ReportGenerationTime";
                    }

                    public ChargebacksReceivedHeader()
                    {
                    }
                }

                public class DebitAdjustments
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string Currency;

                    public readonly static string Category;

                    public readonly static string StatusFlag;

                    public readonly static string SequenceNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string AccountNumber;

                    public readonly static string ReasonCode;

                    public readonly static string TransactionDate;

                    public readonly static string DebitAdjustmentDate;

                    public readonly static string DebitAdjustmentAmount;

                    public readonly static string MOP;

                    public readonly static string ActionCode;

                    public readonly static string DebitOrCreditCardholder;

                    public readonly static string AuthDate;

                    public readonly static string AdjustmentNumber;

                    static DebitAdjustments()
                    {
                        SLMResp.Batch.DFR.DebitAdjustments.ReportType = "DebitAdjustments.ReportType";
                        SLMResp.Batch.DFR.DebitAdjustments.EntityType = "DebitAdjustments.EntityType";
                        SLMResp.Batch.DFR.DebitAdjustments.EntityNumber = "DebitAdjustments.EntityNumber";
                        SLMResp.Batch.DFR.DebitAdjustments.Currency = "DebitAdjustments.Currency";
                        SLMResp.Batch.DFR.DebitAdjustments.Category = "DebitAdjustments.Category";
                        SLMResp.Batch.DFR.DebitAdjustments.StatusFlag = "DebitAdjustments.StatusFlag";
                        SLMResp.Batch.DFR.DebitAdjustments.SequenceNumber = "DebitAdjustments.SequenceNumber";
                        SLMResp.Batch.DFR.DebitAdjustments.MerchantOrderNumber = "DebitAdjustments.MerchantOrderNumber";
                        SLMResp.Batch.DFR.DebitAdjustments.AccountNumber = "DebitAdjustments.AccountNumber";
                        SLMResp.Batch.DFR.DebitAdjustments.ReasonCode = "DebitAdjustments.ReasonCode";
                        SLMResp.Batch.DFR.DebitAdjustments.TransactionDate = "DebitAdjustments.TransactionDate";
                        SLMResp.Batch.DFR.DebitAdjustments.DebitAdjustmentDate = "DebitAdjustments.DebitAdjustmentDate";
                        SLMResp.Batch.DFR.DebitAdjustments.DebitAdjustmentAmount = "DebitAdjustments.DebitAdjustmentAmount";
                        SLMResp.Batch.DFR.DebitAdjustments.MOP = "DebitAdjustments.MOP";
                        SLMResp.Batch.DFR.DebitAdjustments.ActionCode = "DebitAdjustments.ActionCode";
                        SLMResp.Batch.DFR.DebitAdjustments.DebitOrCreditCardholder = "DebitAdjustments.DebitOrCreditCardholder";
                        SLMResp.Batch.DFR.DebitAdjustments.AuthDate = "DebitAdjustments.AuthDate";
                        SLMResp.Batch.DFR.DebitAdjustments.AdjustmentNumber = "DebitAdjustments.AdjustmentNumber";
                    }

                    public DebitAdjustments()
                    {
                    }
                }

                public class DebitAdjustmentsHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static DebitAdjustmentsHeader()
                    {
                        SLMResp.Batch.DFR.DebitAdjustmentsHeader.ReportType = "DebitAdjustmentsHeader.ReportType";
                        SLMResp.Batch.DFR.DebitAdjustmentsHeader.CompanyIDNumber = "DebitAdjustmentsHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.DebitAdjustmentsHeader.ReportDateFrom = "DebitAdjustmentsHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.DebitAdjustmentsHeader.ReportDateTo = "DebitAdjustmentsHeader.ReportDateTo";
                        SLMResp.Batch.DFR.DebitAdjustmentsHeader.ReportGenerationDate = "DebitAdjustmentsHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.DebitAdjustmentsHeader.ReportGenerationTime = "DebitAdjustmentsHeader.ReportGenerationTime";
                    }

                    public DebitAdjustmentsHeader()
                    {
                    }
                }

                public class DebitAuthAging
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string Currency;

                    public readonly static string MethodOfPayment;

                    public readonly static string NumberDaysOutstanding;

                    public readonly static string AuthorizationCode;

                    public readonly static string AuthorizationDate;

                    public readonly static string ActionCode;

                    public readonly static string Amount;

                    public readonly static string AccountNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string TraceNumber;

                    static DebitAuthAging()
                    {
                        SLMResp.Batch.DFR.DebitAuthAging.ReportType = "DebitAuthAging.ReportType";
                        SLMResp.Batch.DFR.DebitAuthAging.EntityType = "DebitAuthAging.EntityType";
                        SLMResp.Batch.DFR.DebitAuthAging.EntityNumber = "DebitAuthAging.EntityNumber";
                        SLMResp.Batch.DFR.DebitAuthAging.Currency = "DebitAuthAging.Currency";
                        SLMResp.Batch.DFR.DebitAuthAging.MethodOfPayment = "DebitAuthAging.MethodOfPayment";
                        SLMResp.Batch.DFR.DebitAuthAging.NumberDaysOutstanding = "DebitAuthAging.NumberDaysOutstanding";
                        SLMResp.Batch.DFR.DebitAuthAging.AuthorizationCode = "DebitAuthAging.AuthorizationCode";
                        SLMResp.Batch.DFR.DebitAuthAging.AuthorizationDate = "DebitAuthAging.AuthorizationDate";
                        SLMResp.Batch.DFR.DebitAuthAging.ActionCode = "DebitAuthAging.ActionCode";
                        SLMResp.Batch.DFR.DebitAuthAging.Amount = "DebitAuthAging.Amount";
                        SLMResp.Batch.DFR.DebitAuthAging.AccountNumber = "DebitAuthAging.AccountNumber";
                        SLMResp.Batch.DFR.DebitAuthAging.MerchantOrderNumber = "DebitAuthAging.MerchantOrderNumber";
                        SLMResp.Batch.DFR.DebitAuthAging.TraceNumber = "DebitAuthAging.TraceNumber";
                    }

                    public DebitAuthAging()
                    {
                    }
                }

                public class DebitAuthAgingHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static DebitAuthAgingHeader()
                    {
                        SLMResp.Batch.DFR.DebitAuthAgingHeader.ReportType = "DebitAuthAgingHeader.ReportType";
                        SLMResp.Batch.DFR.DebitAuthAgingHeader.CompanyIDNumber = "DebitAuthAgingHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.DebitAuthAgingHeader.ReportDateFrom = "DebitAuthAgingHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.DebitAuthAgingHeader.ReportDateTo = "DebitAuthAgingHeader.ReportDateTo";
                        SLMResp.Batch.DFR.DebitAuthAgingHeader.ReportGenerationDate = "DebitAuthAgingHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.DebitAuthAgingHeader.ReportGenerationTime = "DebitAuthAgingHeader.ReportGenerationTime";
                    }

                    public DebitAuthAgingHeader()
                    {
                    }
                }

                public class DepositActivity
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string FundsTransferInstructionNumber;

                    public readonly static string SecureBANumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string MethodOfPayment;

                    public readonly static string Category;

                    public readonly static string SettledConveyed;

                    public readonly static string Count;

                    public readonly static string Amount;

                    public readonly static string SettlementCurrency;

                    public readonly static string ForeignExchangeRate;

                    static DepositActivity()
                    {
                        SLMResp.Batch.DFR.DepositActivity.ReportType = "DepositActivity.ReportType";
                        SLMResp.Batch.DFR.DepositActivity.EntityType = "DepositActivity.EntityType";
                        SLMResp.Batch.DFR.DepositActivity.EntityNumber = "DepositActivity.EntityNumber";
                        SLMResp.Batch.DFR.DepositActivity.FundsTransferInstructionNumber = "DepositActivity.FundsTransferInstructionNumber";
                        SLMResp.Batch.DFR.DepositActivity.SecureBANumber = "DepositActivity.SecureBANumber";
                        SLMResp.Batch.DFR.DepositActivity.PresentmentCurrency = "DepositActivity.PresentmentCurrency";
                        SLMResp.Batch.DFR.DepositActivity.MethodOfPayment = "DepositActivity.MethodOfPayment";
                        SLMResp.Batch.DFR.DepositActivity.Category = "DepositActivity.Category";
                        SLMResp.Batch.DFR.DepositActivity.SettledConveyed = "DepositActivity.SettledConveyed";
                        SLMResp.Batch.DFR.DepositActivity.Count = "DepositActivity.Count";
                        SLMResp.Batch.DFR.DepositActivity.Amount = "DepositActivity.Amount";
                        SLMResp.Batch.DFR.DepositActivity.SettlementCurrency = "DepositActivity.SettlementCurrency";
                        SLMResp.Batch.DFR.DepositActivity.ForeignExchangeRate = "DepositActivity.ForeignExchangeRate";
                    }

                    public DepositActivity()
                    {
                    }
                }

                public class DepositActivityAttributes
                {
                    public readonly static string ReportType;

                    public readonly static string SubmissionDate;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string AccountNumber;

                    public readonly static string MethodOfPayment;

                    public readonly static string ActionCode;

                    public readonly static string AuthDate;

                    public readonly static string AuthCode;

                    public readonly static string PresentmentCurrency;

                    public readonly static string Amount;

                    public readonly static string CardUsageType;

                    public readonly static string CardProductType;

                    public readonly static string ARN;

                    public readonly static string IssuerCountry;

                    public readonly static string IssuerRegion;

                    public readonly static string IssuerName;

                    public readonly static string SettlementCurrency;

                    static DepositActivityAttributes()
                    {
                        SLMResp.Batch.DFR.DepositActivityAttributes.ReportType = "DepositActivityAttributes.ReportType";
                        SLMResp.Batch.DFR.DepositActivityAttributes.SubmissionDate = "DepositActivityAttributes.SubmissionDate";
                        SLMResp.Batch.DFR.DepositActivityAttributes.EntityType = "DepositActivityAttributes.EntityType";
                        SLMResp.Batch.DFR.DepositActivityAttributes.EntityNumber = "DepositActivityAttributes.EntityNumber";
                        SLMResp.Batch.DFR.DepositActivityAttributes.MerchantOrderNumber = "DepositActivityAttributes.MerchantOrderNumber";
                        SLMResp.Batch.DFR.DepositActivityAttributes.AccountNumber = "DepositActivityAttributes.AccountNumber";
                        SLMResp.Batch.DFR.DepositActivityAttributes.MethodOfPayment = "DepositActivityAttributes.MethodOfPayment";
                        SLMResp.Batch.DFR.DepositActivityAttributes.ActionCode = "DepositActivityAttributes.ActionCode";
                        SLMResp.Batch.DFR.DepositActivityAttributes.AuthDate = "DepositActivityAttributes.AuthDate";
                        SLMResp.Batch.DFR.DepositActivityAttributes.AuthCode = "DepositActivityAttributes.AuthCode";
                        SLMResp.Batch.DFR.DepositActivityAttributes.PresentmentCurrency = "DepositActivityAttributes.PresentmentCurrency";
                        SLMResp.Batch.DFR.DepositActivityAttributes.Amount = "DepositActivityAttributes.Amount";
                        SLMResp.Batch.DFR.DepositActivityAttributes.CardUsageType = "DepositActivityAttributes.CardUsageType";
                        SLMResp.Batch.DFR.DepositActivityAttributes.CardProductType = "DepositActivityAttributes.CardProductType";
                        SLMResp.Batch.DFR.DepositActivityAttributes.ARN = "DepositActivityAttributes.ARN";
                        SLMResp.Batch.DFR.DepositActivityAttributes.IssuerCountry = "DepositActivityAttributes.IssuerCountry";
                        SLMResp.Batch.DFR.DepositActivityAttributes.IssuerRegion = "DepositActivityAttributes.IssuerRegion";
                        SLMResp.Batch.DFR.DepositActivityAttributes.IssuerName = "DepositActivityAttributes.IssuerName";
                        SLMResp.Batch.DFR.DepositActivityAttributes.SettlementCurrency = "DepositActivityAttributes.SettlementCurrency";
                    }

                    public DepositActivityAttributes()
                    {
                    }
                }

                public class DepositActivityAttributesHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static DepositActivityAttributesHeader()
                    {
                        SLMResp.Batch.DFR.DepositActivityAttributesHeader.ReportType = "DepositActivityAttributesHeader.ReportType";
                        SLMResp.Batch.DFR.DepositActivityAttributesHeader.CompanyIDNumber = "DepositActivityAttributesHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.DepositActivityAttributesHeader.ReportDateFrom = "DepositActivityAttributesHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.DepositActivityAttributesHeader.ReportDateTo = "DepositActivityAttributesHeader.ReportDateTo";
                        SLMResp.Batch.DFR.DepositActivityAttributesHeader.ReportGenerationDate = "DepositActivityAttributesHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.DepositActivityAttributesHeader.ReportGenerationTime = "DepositActivityAttributesHeader.ReportGenerationTime";
                    }

                    public DepositActivityAttributesHeader()
                    {
                    }
                }

                public class DepositActivityHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static DepositActivityHeader()
                    {
                        SLMResp.Batch.DFR.DepositActivityHeader.ReportType = "DepositActivityHeader.ReportType";
                        SLMResp.Batch.DFR.DepositActivityHeader.CompanyIDNumber = "DepositActivityHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.DepositActivityHeader.ReportDateFrom = "DepositActivityHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.DepositActivityHeader.ReportDateTo = "DepositActivityHeader.ReportDateTo";
                        SLMResp.Batch.DFR.DepositActivityHeader.ReportGenerationDate = "DepositActivityHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.DepositActivityHeader.ReportGenerationTime = "DepositActivityHeader.ReportGenerationTime";
                    }

                    public DepositActivityHeader()
                    {
                    }
                }

                public class DepositActivityTransfer
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string FundsTransferInstructionNumber;

                    public readonly static string SecureBANumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string FundsTransferNumber;

                    public readonly static string Category;

                    public readonly static string EffectiveDate;

                    public readonly static string Count;

                    public readonly static string Amount;

                    public readonly static string SettlementCurrency;

                    static DepositActivityTransfer()
                    {
                        SLMResp.Batch.DFR.DepositActivityTransfer.ReportType = "DepositActivityTransfer.ReportType";
                        SLMResp.Batch.DFR.DepositActivityTransfer.EntityType = "DepositActivityTransfer.EntityType";
                        SLMResp.Batch.DFR.DepositActivityTransfer.EntityNumber = "DepositActivityTransfer.EntityNumber";
                        SLMResp.Batch.DFR.DepositActivityTransfer.FundsTransferInstructionNumber = "DepositActivityTransfer.FundsTransferInstructionNumber";
                        SLMResp.Batch.DFR.DepositActivityTransfer.SecureBANumber = "DepositActivityTransfer.SecureBANumber";
                        SLMResp.Batch.DFR.DepositActivityTransfer.PresentmentCurrency = "DepositActivityTransfer.PresentmentCurrency";
                        SLMResp.Batch.DFR.DepositActivityTransfer.FundsTransferNumber = "DepositActivityTransfer.FundsTransferNumber";
                        SLMResp.Batch.DFR.DepositActivityTransfer.Category = "DepositActivityTransfer.Category";
                        SLMResp.Batch.DFR.DepositActivityTransfer.EffectiveDate = "DepositActivityTransfer.EffectiveDate";
                        SLMResp.Batch.DFR.DepositActivityTransfer.Count = "DepositActivityTransfer.Count";
                        SLMResp.Batch.DFR.DepositActivityTransfer.Amount = "DepositActivityTransfer.Amount";
                        SLMResp.Batch.DFR.DepositActivityTransfer.SettlementCurrency = "DepositActivityTransfer.SettlementCurrency";
                    }

                    public DepositActivityTransfer()
                    {
                    }
                }

                public class DepositActivityTransferHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static DepositActivityTransferHeader()
                    {
                        SLMResp.Batch.DFR.DepositActivityTransferHeader.ReportType = "DepositActivityTransferHeader.ReportType";
                        SLMResp.Batch.DFR.DepositActivityTransferHeader.CompanyIDNumber = "DepositActivityTransferHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.DepositActivityTransferHeader.ReportDateFrom = "DepositActivityTransferHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.DepositActivityTransferHeader.ReportDateTo = "DepositActivityTransferHeader.ReportDateTo";
                        SLMResp.Batch.DFR.DepositActivityTransferHeader.ReportGenerationDate = "DepositActivityTransferHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.DepositActivityTransferHeader.ReportGenerationTime = "DepositActivityTransferHeader.ReportGenerationTime";
                    }

                    public DepositActivityTransferHeader()
                    {
                    }
                }

                public class DepositDetail
                {
                    public readonly static string ReportType;

                    public readonly static string SubmissionDate;

                    public readonly static string PIDNumber;

                    public readonly static string PIDShortName;

                    public readonly static string SubmissionNumber;

                    public readonly static string RecordNumber;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string RDFINumber;

                    public readonly static string AccountNumber;

                    public readonly static string ExpirationDate;

                    public readonly static string Amount;

                    public readonly static string MethodOfPayment;

                    public readonly static string ActionCode;

                    public readonly static string AuthDate;

                    public readonly static string AuthCode;

                    public readonly static string AuthResponseCode;

                    public readonly static string TraceNumber;

                    public readonly static string ConsumerCountryCode;

                    public readonly static string MCC;

                    public readonly static string TokenIndicator;

                    public readonly static string CashBackAmount;

                    public readonly static string SurchargeAmount;

                    public readonly static string VoucherNumber;

                    public readonly static string EBTAccountType;

                    public readonly static string InterchangeQualificationCode;

                    public readonly static string DebitAssetClass;

                    public readonly static string DurbinRegulated;

                    public readonly static string InterchangeUnitFee;

                    public readonly static string InterchangeFaceValuePercentageFee;

                    public readonly static string TotalInterchangeAmount;

                    public readonly static string TotalAssessmentAmount;

                    public readonly static string OtherDebitPassthroughFees;

                    public readonly static string SettlementCurrency;

                    public readonly static string MerchantInformation;

                    public readonly static string TranSurchargeAmount;

                    public readonly static string BankSortCode;

                    public readonly static string IBAN;

                    public readonly static string BIC;

                    public readonly static string CreditorID;

                    public readonly static string MandateID;

                    public readonly static string MandateType;

                    public readonly static string SignatureDate;

                    static DepositDetail()
                    {
                        SLMResp.Batch.DFR.DepositDetail.ReportType = "DepositDetail.ReportType";
                        SLMResp.Batch.DFR.DepositDetail.SubmissionDate = "DepositDetail.SubmissionDate";
                        SLMResp.Batch.DFR.DepositDetail.PIDNumber = "DepositDetail.PIDNumber";
                        SLMResp.Batch.DFR.DepositDetail.PIDShortName = "DepositDetail.PIDShortName";
                        SLMResp.Batch.DFR.DepositDetail.SubmissionNumber = "DepositDetail.SubmissionNumber";
                        SLMResp.Batch.DFR.DepositDetail.RecordNumber = "DepositDetail.RecordNumber";
                        SLMResp.Batch.DFR.DepositDetail.EntityType = "DepositDetail.EntityType";
                        SLMResp.Batch.DFR.DepositDetail.EntityNumber = "DepositDetail.EntityNumber";
                        SLMResp.Batch.DFR.DepositDetail.PresentmentCurrency = "DepositDetail.PresentmentCurrency";
                        SLMResp.Batch.DFR.DepositDetail.MerchantOrderNumber = "DepositDetail.MerchantOrderNumber";
                        SLMResp.Batch.DFR.DepositDetail.RDFINumber = "DepositDetail.RDFINumber";
                        SLMResp.Batch.DFR.DepositDetail.AccountNumber = "DepositDetail.AccountNumber";
                        SLMResp.Batch.DFR.DepositDetail.ExpirationDate = "DepositDetail.ExpirationDate";
                        SLMResp.Batch.DFR.DepositDetail.Amount = "DepositDetail.Amount";
                        SLMResp.Batch.DFR.DepositDetail.MethodOfPayment = "DepositDetail.MethodOfPayment";
                        SLMResp.Batch.DFR.DepositDetail.ActionCode = "DepositDetail.ActionCode";
                        SLMResp.Batch.DFR.DepositDetail.AuthDate = "DepositDetail.AuthDate";
                        SLMResp.Batch.DFR.DepositDetail.AuthCode = "DepositDetail.AuthCode";
                        SLMResp.Batch.DFR.DepositDetail.AuthResponseCode = "DepositDetail.AuthResponseCode";
                        SLMResp.Batch.DFR.DepositDetail.TraceNumber = "DepositDetail.TraceNumber";
                        SLMResp.Batch.DFR.DepositDetail.ConsumerCountryCode = "DepositDetail.ConsumerCountryCode";
                        SLMResp.Batch.DFR.DepositDetail.MCC = "DepositDetail.MCC";
                        SLMResp.Batch.DFR.DepositDetail.TokenIndicator = "DepositDetail.TokenIndicator";
                        SLMResp.Batch.DFR.DepositDetail.CashBackAmount = "DepositDetail.CashBackAmount";
                        SLMResp.Batch.DFR.DepositDetail.SurchargeAmount = "DepositDetail.SurchargeAmount";
                        SLMResp.Batch.DFR.DepositDetail.VoucherNumber = "DepositDetail.VoucherNumber";
                        SLMResp.Batch.DFR.DepositDetail.EBTAccountType = "DepositDetail.EBTAccountType";
                        SLMResp.Batch.DFR.DepositDetail.InterchangeQualificationCode = "DepositDetail.InterchangeQualificationCode";
                        SLMResp.Batch.DFR.DepositDetail.DebitAssetClass = "DepositDetail.DebitAssetClass";
                        SLMResp.Batch.DFR.DepositDetail.DurbinRegulated = "DepositDetail.DurbinRegulated";
                        SLMResp.Batch.DFR.DepositDetail.InterchangeUnitFee = "DepositDetail.InterchangeUnitFee";
                        SLMResp.Batch.DFR.DepositDetail.InterchangeFaceValuePercentageFee = "DepositDetail.InterchangeFaceValuePercentageFee";
                        SLMResp.Batch.DFR.DepositDetail.TotalInterchangeAmount = "DepositDetail.TotalInterchangeAmount";
                        SLMResp.Batch.DFR.DepositDetail.TotalAssessmentAmount = "DepositDetail.TotalAssessmentAmount";
                        SLMResp.Batch.DFR.DepositDetail.OtherDebitPassthroughFees = "DepositDetail.OtherDebitPassthroughFees";
                        SLMResp.Batch.DFR.DepositDetail.SettlementCurrency = "DepositDetail.SettlementCurrency";
                        SLMResp.Batch.DFR.DepositDetail.MerchantInformation = "DepositDetail.MerchantInformation";
                        SLMResp.Batch.DFR.DepositDetail.TranSurchargeAmount = "DepositDetail.TranSurchargeAmount";
                        SLMResp.Batch.DFR.DepositDetail.BankSortCode = "DepositDetail.BankSortCode";
                        SLMResp.Batch.DFR.DepositDetail.IBAN = "DepositDetail.IBAN";
                        SLMResp.Batch.DFR.DepositDetail.BIC = "DepositDetail.BIC";
                        SLMResp.Batch.DFR.DepositDetail.CreditorID = "DepositDetail.CreditorID";
                        SLMResp.Batch.DFR.DepositDetail.MandateID = "DepositDetail.MandateID";
                        SLMResp.Batch.DFR.DepositDetail.MandateType = "DepositDetail.MandateType";
                        SLMResp.Batch.DFR.DepositDetail.SignatureDate = "DepositDetail.SignatureDate";
                    }

                    public DepositDetail()
                    {
                    }
                }

                public class DepositDetailHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static DepositDetailHeader()
                    {
                        SLMResp.Batch.DFR.DepositDetailHeader.ReportType = "DepositDetailHeader.ReportType";
                        SLMResp.Batch.DFR.DepositDetailHeader.CompanyIDNumber = "DepositDetailHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.DepositDetailHeader.ReportDateFrom = "DepositDetailHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.DepositDetailHeader.ReportDateTo = "DepositDetailHeader.ReportDateTo";
                        SLMResp.Batch.DFR.DepositDetailHeader.ReportGenerationDate = "DepositDetailHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.DepositDetailHeader.ReportGenerationTime = "DepositDetailHeader.ReportGenerationTime";
                    }

                    public DepositDetailHeader()
                    {
                    }
                }

                public class DFRHeader
                {
                    public readonly static string ReportType;

                    public readonly static string PID;

                    public readonly static string Frequency;

                    public readonly static string EntityLevel;

                    static DFRHeader()
                    {
                        SLMResp.Batch.DFR.DFRHeader.ReportType = "DFRHeader.ReportType";
                        SLMResp.Batch.DFR.DFRHeader.PID = "DFRHeader.PID";
                        SLMResp.Batch.DFR.DFRHeader.Frequency = "DFRHeader.Frequency";
                        SLMResp.Batch.DFR.DFRHeader.EntityLevel = "DFRHeader.EntityLevel";
                    }

                    public DFRHeader()
                    {
                    }
                }

                public class DFRTrailer
                {
                    public readonly static string ReportType;

                    public readonly static string PID;

                    public readonly static string Frequency;

                    public readonly static string EntityLevel;

                    static DFRTrailer()
                    {
                        SLMResp.Batch.DFR.DFRTrailer.ReportType = "DFRTrailer.ReportType";
                        SLMResp.Batch.DFR.DFRTrailer.PID = "DFRTrailer.PID";
                        SLMResp.Batch.DFR.DFRTrailer.Frequency = "DFRTrailer.Frequency";
                        SLMResp.Batch.DFR.DFRTrailer.EntityLevel = "DFRTrailer.EntityLevel";
                    }

                    public DFRTrailer()
                    {
                    }
                }

                public class DiscoverChargebackSummary
                {
                    public readonly static string ReportType;

                    public readonly static string SENumber;

                    public readonly static string EntityTypeCompany;

                    public readonly static string EntityNumberCompany;

                    public readonly static string EntityTypeBU;

                    public readonly static string EntityNumberBU;

                    public readonly static string EntityTypeTD;

                    public readonly static string EntityNumberTD;

                    public readonly static string EntityName;

                    public readonly static string Currency;

                    public readonly static string SalesCount;

                    public readonly static string SalesAmount;

                    public readonly static string RefundCount;

                    public readonly static string RefundAmount;

                    public readonly static string NetTransaction;

                    public readonly static string ChargebackCount;

                    public readonly static string ChargebackAmount;

                    public readonly static string RepresentmentCount;

                    public readonly static string RepresentmentAmount;

                    public readonly static string DisputeArbitrationCount;

                    public readonly static string DisputeArbitrationAmount;

                    public readonly static string ReversalCount;

                    public readonly static string ReversalAmount;

                    public readonly static string NetSalesAmount;

                    public readonly static string ChargebackRatio;

                    static DiscoverChargebackSummary()
                    {
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.ReportType = "DiscoverChargebackSummary.ReportType";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.SENumber = "DiscoverChargebackSummary.SENumber";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.EntityTypeCompany = "DiscoverChargebackSummary.EntityTypeCompany";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.EntityNumberCompany = "DiscoverChargebackSummary.EntityNumberCompany";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.EntityTypeBU = "DiscoverChargebackSummary.EntityTypeBU";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.EntityNumberBU = "DiscoverChargebackSummary.EntityNumberBU";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.EntityTypeTD = "DiscoverChargebackSummary.EntityTypeTD";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.EntityNumberTD = "DiscoverChargebackSummary.EntityNumberTD";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.EntityName = "DiscoverChargebackSummary.EntityName";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.Currency = "DiscoverChargebackSummary.Currency";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.SalesCount = "DiscoverChargebackSummary.SalesCount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.SalesAmount = "DiscoverChargebackSummary.SalesAmount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.RefundCount = "DiscoverChargebackSummary.RefundCount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.RefundAmount = "DiscoverChargebackSummary.RefundAmount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.NetTransaction = "DiscoverChargebackSummary.NetTransaction";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.ChargebackCount = "DiscoverChargebackSummary.ChargebackCount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.ChargebackAmount = "DiscoverChargebackSummary.ChargebackAmount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.RepresentmentCount = "DiscoverChargebackSummary.RepresentmentCount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.RepresentmentAmount = "DiscoverChargebackSummary.RepresentmentAmount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.DisputeArbitrationCount = "DiscoverChargebackSummary.DisputeArbitrationCount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.DisputeArbitrationAmount = "DiscoverChargebackSummary.DisputeArbitrationAmount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.ReversalCount = "DiscoverChargebackSummary.ReversalCount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.ReversalAmount = "DiscoverChargebackSummary.ReversalAmount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.NetSalesAmount = "DiscoverChargebackSummary.NetSalesAmount";
                        SLMResp.Batch.DFR.DiscoverChargebackSummary.ChargebackRatio = "DiscoverChargebackSummary.ChargebackRatio";
                    }

                    public DiscoverChargebackSummary()
                    {
                    }
                }

                public class DiscoverChargebackSummaryHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static DiscoverChargebackSummaryHeader()
                    {
                        SLMResp.Batch.DFR.DiscoverChargebackSummaryHeader.ReportType = "DiscoverChargebackSummaryHeader.ReportType";
                        SLMResp.Batch.DFR.DiscoverChargebackSummaryHeader.CompanyIDNumber = "DiscoverChargebackSummaryHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.DiscoverChargebackSummaryHeader.ReportDateFrom = "DiscoverChargebackSummaryHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.DiscoverChargebackSummaryHeader.ReportDateTo = "DiscoverChargebackSummaryHeader.ReportDateTo";
                        SLMResp.Batch.DFR.DiscoverChargebackSummaryHeader.ReportGenerationDate = "DiscoverChargebackSummaryHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.DiscoverChargebackSummaryHeader.ReportGenerationTime = "DiscoverChargebackSummaryHeader.ReportGenerationTime";
                    }

                    public DiscoverChargebackSummaryHeader()
                    {
                    }
                }

                public class DynamicDebitRoutingComparison
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string TransactionDivisionNumber;

                    public readonly static string AccountNumber;

                    public readonly static string TraceNumber;

                    public readonly static string SaleAmount;

                    public readonly static string TransactionType;

                    public readonly static string TransactionDate;

                    public readonly static string ExemptRegulatedStatus;

                    public readonly static string RoutingMethod;

                    public readonly static string Network;

                    public readonly static string RoutedNetworkCalculatedCost;

                    public readonly static string OtherEligibleNetwork1;

                    public readonly static string NetworkProductQualification1;

                    public readonly static string OtherNetworkCalculatedCost1;

                    public readonly static string OtherEligibleNetwork2;

                    public readonly static string NetworkProductQualification2;

                    public readonly static string OtherNetworkCalculatedCost2;

                    public readonly static string OtherEligibleNetwork3;

                    public readonly static string NetworkProductQualification3;

                    public readonly static string OtherNetworkCalculatedCost3;

                    public readonly static string OtherEligibleNetwork4;

                    public readonly static string NetworkProductQualification4;

                    public readonly static string OtherNetworkCalculatedCost4;

                    public readonly static string OtherEligibleNetwork5;

                    public readonly static string NetworkProductQualification5;

                    public readonly static string OtherNetworkCalculatedCost5;

                    public readonly static string OtherEligibleNetwork6;

                    public readonly static string NetworkProductQualification6;

                    public readonly static string OtherNetworkCalculatedCost6;

                    public readonly static string OtherEligibleNetwork7;

                    public readonly static string NetworkProductQualification7;

                    public readonly static string OtherNetworkCalculatedCost7;

                    public readonly static string OtherEligibleNetwork8;

                    public readonly static string NetworkProductQualification8;

                    public readonly static string OtherNetworkCalculatedCost8;

                    public readonly static string OtherEligibleNetwork9;

                    public readonly static string NetworkProductQualification9;

                    public readonly static string OtherNetworkCalculatedCost9;

                    public readonly static string OtherEligibleNetwork10;

                    public readonly static string NetworkProductQualification10;

                    public readonly static string OtherNetworkCalculatedCost10;

                    public readonly static string OtherEligibleNetwork11;

                    public readonly static string NetworkProductQualification11;

                    public readonly static string OtherNetworkCalculatedCost11;

                    public readonly static string OtherEligibleNetwork12;

                    public readonly static string NetworkProductQualification12;

                    public readonly static string OtherNetworkCalculatedCost12;

                    public readonly static string OtherEligibleNetwork13;

                    public readonly static string NetworkProductQualification13;

                    public readonly static string OtherNetworkCalculatedCost13;

                    public readonly static string OtherEligibleNetwork14;

                    public readonly static string NetworkProductQualification14;

                    public readonly static string OtherNetworkCalculatedCost14;

                    public readonly static string OtherEligibleNetwork15;

                    public readonly static string NetworkProductQualification15;

                    public readonly static string OtherNetworkCalculatedCost15;

                    public readonly static string OtherEligibleNetwork16;

                    public readonly static string NetworkProductQualification16;

                    public readonly static string OtherNetworkCalculatedCost16;

                    public readonly static string OtherEligibleNetwork17;

                    public readonly static string NetworkProductQualification17;

                    public readonly static string OtherNetworkCalculatedCost17;

                    public readonly static string OtherEligibleNetwork18;

                    public readonly static string NetworkProductQualification18;

                    public readonly static string OtherNetworkCalculatedCost18;

                    public readonly static string OtherEligibleNetwork19;

                    public readonly static string NetworkProductQualification19;

                    public readonly static string OtherNetworkCalculatedCost19;

                    public readonly static string OtherEligibleNetwork20;

                    public readonly static string NetworkProductQualification20;

                    public readonly static string OtherNetworkCalculatedCost20;

                    static DynamicDebitRoutingComparison()
                    {
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.ReportType = "DynamicDebitRoutingComparison.ReportType";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.EntityType = "DynamicDebitRoutingComparison.EntityType";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.TransactionDivisionNumber = "DynamicDebitRoutingComparison.TransactionDivisionNumber";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.AccountNumber = "DynamicDebitRoutingComparison.AccountNumber";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.TraceNumber = "DynamicDebitRoutingComparison.TraceNumber";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.SaleAmount = "DynamicDebitRoutingComparison.SaleAmount";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.TransactionType = "DynamicDebitRoutingComparison.TransactionType";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.TransactionDate = "DynamicDebitRoutingComparison.TransactionDate";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.ExemptRegulatedStatus = "DynamicDebitRoutingComparison.ExemptRegulatedStatus";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.RoutingMethod = "DynamicDebitRoutingComparison.RoutingMethod";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.Network = "DynamicDebitRoutingComparison.Network";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.RoutedNetworkCalculatedCost = "DynamicDebitRoutingComparison.RoutedNetworkCalculatedCost";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork1 = "DynamicDebitRoutingComparison.OtherEligibleNetwork1";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification1 = "DynamicDebitRoutingComparison.NetworkProductQualification1";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost1 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost1";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork2 = "DynamicDebitRoutingComparison.OtherEligibleNetwork2";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification2 = "DynamicDebitRoutingComparison.NetworkProductQualification2";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost2 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost2";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork3 = "DynamicDebitRoutingComparison.OtherEligibleNetwork3";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification3 = "DynamicDebitRoutingComparison.NetworkProductQualification3";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost3 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost3";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork4 = "DynamicDebitRoutingComparison.OtherEligibleNetwork4";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification4 = "DynamicDebitRoutingComparison.NetworkProductQualification4";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost4 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost4";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork5 = "DynamicDebitRoutingComparison.OtherEligibleNetwork5";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification5 = "DynamicDebitRoutingComparison.NetworkProductQualification5";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost5 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost5";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork6 = "DynamicDebitRoutingComparison.OtherEligibleNetwork6";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification6 = "DynamicDebitRoutingComparison.NetworkProductQualification6";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost6 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost6";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork7 = "DynamicDebitRoutingComparison.OtherEligibleNetwork7";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification7 = "DynamicDebitRoutingComparison.NetworkProductQualification7";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost7 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost7";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork8 = "DynamicDebitRoutingComparison.OtherEligibleNetwork8";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification8 = "DynamicDebitRoutingComparison.NetworkProductQualification8";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost8 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost8";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork9 = "DynamicDebitRoutingComparison.OtherEligibleNetwork9";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification9 = "DynamicDebitRoutingComparison.NetworkProductQualification9";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost9 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost9";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork10 = "DynamicDebitRoutingComparison.OtherEligibleNetwork10";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification10 = "DynamicDebitRoutingComparison.NetworkProductQualification10";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost10 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost10";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork11 = "DynamicDebitRoutingComparison.OtherEligibleNetwork11";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification11 = "DynamicDebitRoutingComparison.NetworkProductQualification11";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost11 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost11";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork12 = "DynamicDebitRoutingComparison.OtherEligibleNetwork12";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification12 = "DynamicDebitRoutingComparison.NetworkProductQualification12";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost12 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost12";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork13 = "DynamicDebitRoutingComparison.OtherEligibleNetwork13";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification13 = "DynamicDebitRoutingComparison.NetworkProductQualification13";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost13 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost13";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork14 = "DynamicDebitRoutingComparison.OtherEligibleNetwork14";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification14 = "DynamicDebitRoutingComparison.NetworkProductQualification14";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost14 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost14";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork15 = "DynamicDebitRoutingComparison.OtherEligibleNetwork15";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification15 = "DynamicDebitRoutingComparison.NetworkProductQualification15";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost15 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost15";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork16 = "DynamicDebitRoutingComparison.OtherEligibleNetwork16";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification16 = "DynamicDebitRoutingComparison.NetworkProductQualification16";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost16 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost16";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork17 = "DynamicDebitRoutingComparison.OtherEligibleNetwork17";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification17 = "DynamicDebitRoutingComparison.NetworkProductQualification17";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost17 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost17";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork18 = "DynamicDebitRoutingComparison.OtherEligibleNetwork18";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification18 = "DynamicDebitRoutingComparison.NetworkProductQualification18";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost18 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost18";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork19 = "DynamicDebitRoutingComparison.OtherEligibleNetwork19";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification19 = "DynamicDebitRoutingComparison.NetworkProductQualification19";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost19 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost19";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherEligibleNetwork20 = "DynamicDebitRoutingComparison.OtherEligibleNetwork20";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.NetworkProductQualification20 = "DynamicDebitRoutingComparison.NetworkProductQualification20";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparison.OtherNetworkCalculatedCost20 = "DynamicDebitRoutingComparison.OtherNetworkCalculatedCost20";
                    }

                    public DynamicDebitRoutingComparison()
                    {
                    }
                }

                public class DynamicDebitRoutingComparisonHeader
                {
                    public readonly static string ReportType;

                    public readonly static string EntityID;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static DynamicDebitRoutingComparisonHeader()
                    {
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparisonHeader.ReportType = "DynamicDebitRoutingComparisonHeader.ReportType";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparisonHeader.EntityID = "DynamicDebitRoutingComparisonHeader.EntityID";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparisonHeader.ReportDateFrom = "DynamicDebitRoutingComparisonHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparisonHeader.ReportDateTo = "DynamicDebitRoutingComparisonHeader.ReportDateTo";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparisonHeader.ReportGenerationDate = "DynamicDebitRoutingComparisonHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.DynamicDebitRoutingComparisonHeader.ReportGenerationTime = "DynamicDebitRoutingComparisonHeader.ReportGenerationTime";
                    }

                    public DynamicDebitRoutingComparisonHeader()
                    {
                    }
                }

                public class ECPReturnsDetail
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string ConsumerBankCountryCode;

                    public readonly static string MethodOfPayment;

                    public readonly static string Currency;

                    public readonly static string Category;

                    public readonly static string StatusFlag;

                    public readonly static string SequenceNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string AccountNumber;

                    public readonly static string ReasonCode;

                    public readonly static string TransactionDate;

                    public readonly static string ECPReturnDate;

                    public readonly static string ActivityDate;

                    public readonly static string ECPReturnAmount;

                    public readonly static string UsageCode;

                    public readonly static string ConsumerName;

                    public readonly static string RDFI;

                    public readonly static string ReasonCodeClassification;

                    public readonly static string AccountholderAuthorizationMethod;

                    public readonly static string IBAN;

                    static ECPReturnsDetail()
                    {
                        SLMResp.Batch.DFR.ECPReturnsDetail.ReportType = "ECPReturnsDetail.ReportType";
                        SLMResp.Batch.DFR.ECPReturnsDetail.EntityType = "ECPReturnsDetail.EntityType";
                        SLMResp.Batch.DFR.ECPReturnsDetail.EntityNumber = "ECPReturnsDetail.EntityNumber";
                        SLMResp.Batch.DFR.ECPReturnsDetail.ConsumerBankCountryCode = "ECPReturnsDetail.ConsumerBankCountryCode";
                        SLMResp.Batch.DFR.ECPReturnsDetail.MethodOfPayment = "ECPReturnsDetail.MethodOfPayment";
                        SLMResp.Batch.DFR.ECPReturnsDetail.Currency = "ECPReturnsDetail.Currency";
                        SLMResp.Batch.DFR.ECPReturnsDetail.Category = "ECPReturnsDetail.Category";
                        SLMResp.Batch.DFR.ECPReturnsDetail.StatusFlag = "ECPReturnsDetail.StatusFlag";
                        SLMResp.Batch.DFR.ECPReturnsDetail.SequenceNumber = "ECPReturnsDetail.SequenceNumber";
                        SLMResp.Batch.DFR.ECPReturnsDetail.MerchantOrderNumber = "ECPReturnsDetail.MerchantOrderNumber";
                        SLMResp.Batch.DFR.ECPReturnsDetail.AccountNumber = "ECPReturnsDetail.AccountNumber";
                        SLMResp.Batch.DFR.ECPReturnsDetail.ReasonCode = "ECPReturnsDetail.ReasonCode";
                        SLMResp.Batch.DFR.ECPReturnsDetail.TransactionDate = "ECPReturnsDetail.TransactionDate";
                        SLMResp.Batch.DFR.ECPReturnsDetail.ECPReturnDate = "ECPReturnsDetail.ECPReturnDate";
                        SLMResp.Batch.DFR.ECPReturnsDetail.ActivityDate = "ECPReturnsDetail.ActivityDate";
                        SLMResp.Batch.DFR.ECPReturnsDetail.ECPReturnAmount = "ECPReturnsDetail.ECPReturnAmount";
                        SLMResp.Batch.DFR.ECPReturnsDetail.UsageCode = "ECPReturnsDetail.UsageCode";
                        SLMResp.Batch.DFR.ECPReturnsDetail.ConsumerName = "ECPReturnsDetail.ConsumerName";
                        SLMResp.Batch.DFR.ECPReturnsDetail.RDFI = "ECPReturnsDetail.RDFI";
                        SLMResp.Batch.DFR.ECPReturnsDetail.ReasonCodeClassification = "ECPReturnsDetail.ReasonCodeClassification";
                        SLMResp.Batch.DFR.ECPReturnsDetail.AccountholderAuthorizationMethod = "ECPReturnsDetail.AccountholderAuthorizationMethod";
                        SLMResp.Batch.DFR.ECPReturnsDetail.IBAN = "ECPReturnsDetail.IBAN";
                    }

                    public ECPReturnsDetail()
                    {
                    }
                }

                public class ECPReturnsHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static ECPReturnsHeader()
                    {
                        SLMResp.Batch.DFR.ECPReturnsHeader.ReportType = "ECPReturnsHeader.ReportType";
                        SLMResp.Batch.DFR.ECPReturnsHeader.CompanyIDNumber = "ECPReturnsHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.ECPReturnsHeader.ReportDateFrom = "ECPReturnsHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.ECPReturnsHeader.ReportDateTo = "ECPReturnsHeader.ReportDateTo";
                        SLMResp.Batch.DFR.ECPReturnsHeader.ReportGenerationDate = "ECPReturnsHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.ECPReturnsHeader.ReportGenerationTime = "ECPReturnsHeader.ReportGenerationTime";
                    }

                    public ECPReturnsHeader()
                    {
                    }
                }

                public class ECPReturnsReceived
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string ConsumerBankCountryCode;

                    public readonly static string MethodOfPayment;

                    public readonly static string Currency;

                    public readonly static string Category;

                    public readonly static string StatusFlag;

                    public readonly static string SequenceNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string AccountNumber;

                    public readonly static string ReasonCode;

                    public readonly static string TransactionDate;

                    public readonly static string ECPReturnDate;

                    public readonly static string ActivityDate;

                    public readonly static string ECPReturnAmount;

                    public readonly static string UsageCode;

                    public readonly static string ConsumerName;

                    public readonly static string RDFI;

                    public readonly static string ReasonCodeClassification;

                    public readonly static string AccountholderAuthorizationMethod;

                    public readonly static string IBAN;

                    static ECPReturnsReceived()
                    {
                        SLMResp.Batch.DFR.ECPReturnsReceived.ReportType = "ECPReturnsReceived.ReportType";
                        SLMResp.Batch.DFR.ECPReturnsReceived.EntityType = "ECPReturnsReceived.EntityType";
                        SLMResp.Batch.DFR.ECPReturnsReceived.EntityNumber = "ECPReturnsReceived.EntityNumber";
                        SLMResp.Batch.DFR.ECPReturnsReceived.ConsumerBankCountryCode = "ECPReturnsReceived.ConsumerBankCountryCode";
                        SLMResp.Batch.DFR.ECPReturnsReceived.MethodOfPayment = "ECPReturnsReceived.MethodOfPayment";
                        SLMResp.Batch.DFR.ECPReturnsReceived.Currency = "ECPReturnsReceived.Currency";
                        SLMResp.Batch.DFR.ECPReturnsReceived.Category = "ECPReturnsReceived.Category";
                        SLMResp.Batch.DFR.ECPReturnsReceived.StatusFlag = "ECPReturnsReceived.StatusFlag";
                        SLMResp.Batch.DFR.ECPReturnsReceived.SequenceNumber = "ECPReturnsReceived.SequenceNumber";
                        SLMResp.Batch.DFR.ECPReturnsReceived.MerchantOrderNumber = "ECPReturnsReceived.MerchantOrderNumber";
                        SLMResp.Batch.DFR.ECPReturnsReceived.AccountNumber = "ECPReturnsReceived.AccountNumber";
                        SLMResp.Batch.DFR.ECPReturnsReceived.ReasonCode = "ECPReturnsReceived.ReasonCode";
                        SLMResp.Batch.DFR.ECPReturnsReceived.TransactionDate = "ECPReturnsReceived.TransactionDate";
                        SLMResp.Batch.DFR.ECPReturnsReceived.ECPReturnDate = "ECPReturnsReceived.ECPReturnDate";
                        SLMResp.Batch.DFR.ECPReturnsReceived.ActivityDate = "ECPReturnsReceived.ActivityDate";
                        SLMResp.Batch.DFR.ECPReturnsReceived.ECPReturnAmount = "ECPReturnsReceived.ECPReturnAmount";
                        SLMResp.Batch.DFR.ECPReturnsReceived.UsageCode = "ECPReturnsReceived.UsageCode";
                        SLMResp.Batch.DFR.ECPReturnsReceived.ConsumerName = "ECPReturnsReceived.ConsumerName";
                        SLMResp.Batch.DFR.ECPReturnsReceived.RDFI = "ECPReturnsReceived.RDFI";
                        SLMResp.Batch.DFR.ECPReturnsReceived.ReasonCodeClassification = "ECPReturnsReceived.ReasonCodeClassification";
                        SLMResp.Batch.DFR.ECPReturnsReceived.AccountholderAuthorizationMethod = "ECPReturnsReceived.AccountholderAuthorizationMethod";
                        SLMResp.Batch.DFR.ECPReturnsReceived.IBAN = "ECPReturnsReceived.IBAN";
                    }

                    public ECPReturnsReceived()
                    {
                    }
                }

                public class ECPReturnsReceivedHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static ECPReturnsReceivedHeader()
                    {
                        SLMResp.Batch.DFR.ECPReturnsReceivedHeader.ReportType = "ECPReturnsReceivedHeader.ReportType";
                        SLMResp.Batch.DFR.ECPReturnsReceivedHeader.CompanyIDNumber = "ECPReturnsReceivedHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.ECPReturnsReceivedHeader.ReportDateFrom = "ECPReturnsReceivedHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.ECPReturnsReceivedHeader.ReportDateTo = "ECPReturnsReceivedHeader.ReportDateTo";
                        SLMResp.Batch.DFR.ECPReturnsReceivedHeader.ReportGenerationDate = "ECPReturnsReceivedHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.ECPReturnsReceivedHeader.ReportGenerationTime = "ECPReturnsReceivedHeader.ReportGenerationTime";
                    }

                    public ECPReturnsReceivedHeader()
                    {
                    }
                }

                public class ECPReturnsSummary
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string ConsumerBankCountryCode;

                    public readonly static string Currency;

                    public readonly static string MethodOfPayment;

                    public readonly static string Category;

                    public readonly static string FinancialNonFinancial;

                    public readonly static string Count;

                    public readonly static string Amount;

                    static ECPReturnsSummary()
                    {
                        SLMResp.Batch.DFR.ECPReturnsSummary.ReportType = "ECPReturnsSummary.ReportType";
                        SLMResp.Batch.DFR.ECPReturnsSummary.EntityType = "ECPReturnsSummary.EntityType";
                        SLMResp.Batch.DFR.ECPReturnsSummary.EntityNumber = "ECPReturnsSummary.EntityNumber";
                        SLMResp.Batch.DFR.ECPReturnsSummary.ConsumerBankCountryCode = "ECPReturnsSummary.ConsumerBankCountryCode";
                        SLMResp.Batch.DFR.ECPReturnsSummary.Currency = "ECPReturnsSummary.Currency";
                        SLMResp.Batch.DFR.ECPReturnsSummary.MethodOfPayment = "ECPReturnsSummary.MethodOfPayment";
                        SLMResp.Batch.DFR.ECPReturnsSummary.Category = "ECPReturnsSummary.Category";
                        SLMResp.Batch.DFR.ECPReturnsSummary.FinancialNonFinancial = "ECPReturnsSummary.FinancialNonFinancial";
                        SLMResp.Batch.DFR.ECPReturnsSummary.Count = "ECPReturnsSummary.Count";
                        SLMResp.Batch.DFR.ECPReturnsSummary.Amount = "ECPReturnsSummary.Amount";
                    }

                    public ECPReturnsSummary()
                    {
                    }
                }

                public class EFileImageUploadException
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string SubmissionNumber;

                    public readonly static string SequenceNumber;

                    public readonly static string MethodofPayment;

                    public readonly static string AccountNumber;

                    public readonly static string CBRRType;

                    public readonly static string ImageName;

                    public readonly static string ExceptionDescription;

                    static EFileImageUploadException()
                    {
                        SLMResp.Batch.DFR.EFileImageUploadException.ReportType = "EFileImageUploadException.ReportType";
                        SLMResp.Batch.DFR.EFileImageUploadException.EntityType = "EFileImageUploadException.EntityType";
                        SLMResp.Batch.DFR.EFileImageUploadException.EntityNumber = "EFileImageUploadException.EntityNumber";
                        SLMResp.Batch.DFR.EFileImageUploadException.SubmissionNumber = "EFileImageUploadException.SubmissionNumber";
                        SLMResp.Batch.DFR.EFileImageUploadException.SequenceNumber = "EFileImageUploadException.SequenceNumber";
                        SLMResp.Batch.DFR.EFileImageUploadException.MethodofPayment = "EFileImageUploadException.MethodofPayment";
                        SLMResp.Batch.DFR.EFileImageUploadException.AccountNumber = "EFileImageUploadException.AccountNumber";
                        SLMResp.Batch.DFR.EFileImageUploadException.CBRRType = "EFileImageUploadException.CBRRType";
                        SLMResp.Batch.DFR.EFileImageUploadException.ImageName = "EFileImageUploadException.ImageName";
                        SLMResp.Batch.DFR.EFileImageUploadException.ExceptionDescription = "EFileImageUploadException.ExceptionDescription";
                    }

                    public EFileImageUploadException()
                    {
                    }
                }

                public class EFileImageUploadExceptionHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static EFileImageUploadExceptionHeader()
                    {
                        SLMResp.Batch.DFR.EFileImageUploadExceptionHeader.ReportType = "EFileImageUploadExceptionHeader.ReportType";
                        SLMResp.Batch.DFR.EFileImageUploadExceptionHeader.CompanyIDNumber = "EFileImageUploadExceptionHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.EFileImageUploadExceptionHeader.ReportDateFrom = "EFileImageUploadExceptionHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.EFileImageUploadExceptionHeader.ReportDateTo = "EFileImageUploadExceptionHeader.ReportDateTo";
                        SLMResp.Batch.DFR.EFileImageUploadExceptionHeader.ReportGenerationDate = "EFileImageUploadExceptionHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.EFileImageUploadExceptionHeader.ReportGenerationTime = "EFileImageUploadExceptionHeader.ReportGenerationTime";
                    }

                    public EFileImageUploadExceptionHeader()
                    {
                    }
                }

                public class ExceptionDetail
                {
                    public readonly static string ReportType;

                    public readonly static string SubmissionDate;

                    public readonly static string PID;

                    public readonly static string PIDShortName;

                    public readonly static string SubmissionNumber;

                    public readonly static string RecordNumber;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string RDFINumber;

                    public readonly static string AccountNumber;

                    public readonly static string ExpirationDate;

                    public readonly static string Amount;

                    public readonly static string MethodOfPayment;

                    public readonly static string ActionCode;

                    public readonly static string AuthDate;

                    public readonly static string AuthCode;

                    public readonly static string AuthResponseCode;

                    public readonly static string TraceNumber;

                    public readonly static string ConsumerCountryCode;

                    public readonly static string Category;

                    public readonly static string MCC;

                    public readonly static string RejectCode;

                    public readonly static string SubmissionStatus;

                    public readonly static string SettlementCurrency;

                    public readonly static string BankSortCode;

                    public readonly static string IBAN;

                    public readonly static string BIC;

                    public readonly static string CreditorID;

                    public readonly static string MandateID;

                    public readonly static string MandateType;

                    public readonly static string SignatureDate;

                    static ExceptionDetail()
                    {
                        SLMResp.Batch.DFR.ExceptionDetail.ReportType = "ExceptionDetail.ReportType";
                        SLMResp.Batch.DFR.ExceptionDetail.SubmissionDate = "ExceptionDetail.SubmissionDate";
                        SLMResp.Batch.DFR.ExceptionDetail.PID = "ExceptionDetail.PID";
                        SLMResp.Batch.DFR.ExceptionDetail.PIDShortName = "ExceptionDetail.PIDShortName";
                        SLMResp.Batch.DFR.ExceptionDetail.SubmissionNumber = "ExceptionDetail.SubmissionNumber";
                        SLMResp.Batch.DFR.ExceptionDetail.RecordNumber = "ExceptionDetail.RecordNumber";
                        SLMResp.Batch.DFR.ExceptionDetail.EntityType = "ExceptionDetail.EntityType";
                        SLMResp.Batch.DFR.ExceptionDetail.EntityNumber = "ExceptionDetail.EntityNumber";
                        SLMResp.Batch.DFR.ExceptionDetail.PresentmentCurrency = "ExceptionDetail.PresentmentCurrency";
                        SLMResp.Batch.DFR.ExceptionDetail.MerchantOrderNumber = "ExceptionDetail.MerchantOrderNumber";
                        SLMResp.Batch.DFR.ExceptionDetail.RDFINumber = "ExceptionDetail.RDFINumber";
                        SLMResp.Batch.DFR.ExceptionDetail.AccountNumber = "ExceptionDetail.AccountNumber";
                        SLMResp.Batch.DFR.ExceptionDetail.ExpirationDate = "ExceptionDetail.ExpirationDate";
                        SLMResp.Batch.DFR.ExceptionDetail.Amount = "ExceptionDetail.Amount";
                        SLMResp.Batch.DFR.ExceptionDetail.MethodOfPayment = "ExceptionDetail.MethodOfPayment";
                        SLMResp.Batch.DFR.ExceptionDetail.ActionCode = "ExceptionDetail.ActionCode";
                        SLMResp.Batch.DFR.ExceptionDetail.AuthDate = "ExceptionDetail.AuthDate";
                        SLMResp.Batch.DFR.ExceptionDetail.AuthCode = "ExceptionDetail.AuthCode";
                        SLMResp.Batch.DFR.ExceptionDetail.AuthResponseCode = "ExceptionDetail.AuthResponseCode";
                        SLMResp.Batch.DFR.ExceptionDetail.TraceNumber = "ExceptionDetail.TraceNumber";
                        SLMResp.Batch.DFR.ExceptionDetail.ConsumerCountryCode = "ExceptionDetail.ConsumerCountryCode";
                        SLMResp.Batch.DFR.ExceptionDetail.Category = "ExceptionDetail.Category";
                        SLMResp.Batch.DFR.ExceptionDetail.MCC = "ExceptionDetail.MCC";
                        SLMResp.Batch.DFR.ExceptionDetail.RejectCode = "ExceptionDetail.RejectCode";
                        SLMResp.Batch.DFR.ExceptionDetail.SubmissionStatus = "ExceptionDetail.SubmissionStatus";
                        SLMResp.Batch.DFR.ExceptionDetail.SettlementCurrency = "ExceptionDetail.SettlementCurrency";
                        SLMResp.Batch.DFR.ExceptionDetail.BankSortCode = "ExceptionDetail.BankSortCode";
                        SLMResp.Batch.DFR.ExceptionDetail.IBAN = "ExceptionDetail.IBAN";
                        SLMResp.Batch.DFR.ExceptionDetail.BIC = "ExceptionDetail.BIC";
                        SLMResp.Batch.DFR.ExceptionDetail.CreditorID = "ExceptionDetail.CreditorID";
                        SLMResp.Batch.DFR.ExceptionDetail.MandateID = "ExceptionDetail.MandateID";
                        SLMResp.Batch.DFR.ExceptionDetail.MandateType = "ExceptionDetail.MandateType";
                        SLMResp.Batch.DFR.ExceptionDetail.SignatureDate = "ExceptionDetail.SignatureDate";
                    }

                    public ExceptionDetail()
                    {
                    }
                }

                public class ExceptionDetailHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static ExceptionDetailHeader()
                    {
                        SLMResp.Batch.DFR.ExceptionDetailHeader.ReportType = "ExceptionDetailHeader.ReportType";
                        SLMResp.Batch.DFR.ExceptionDetailHeader.CompanyIDNumber = "ExceptionDetailHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.ExceptionDetailHeader.ReportDateFrom = "ExceptionDetailHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.ExceptionDetailHeader.ReportDateTo = "ExceptionDetailHeader.ReportDateTo";
                        SLMResp.Batch.DFR.ExceptionDetailHeader.ReportGenerationDate = "ExceptionDetailHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.ExceptionDetailHeader.ReportGenerationTime = "ExceptionDetailHeader.ReportGenerationTime";
                    }

                    public ExceptionDetailHeader()
                    {
                    }
                }

                public class IBANConversionDetail
                {
                    public readonly static string ReportType;

                    public readonly static string SubmissionDate;

                    public readonly static string SubmissionNumber;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string MandateID;

                    public readonly static string MandateType;

                    public readonly static string SignatureDate;

                    public readonly static string CreditorID;

                    public readonly static string BankSortCode;

                    public readonly static string AccountNumber;

                    public readonly static string IBAN;

                    public readonly static string BIC;

                    public readonly static string CountryCode;

                    public readonly static string ActionCode;

                    public readonly static string Amount;

                    public readonly static string ResponseReasonCode;

                    static IBANConversionDetail()
                    {
                        SLMResp.Batch.DFR.IBANConversionDetail.ReportType = "IBANConversionDetail.ReportType";
                        SLMResp.Batch.DFR.IBANConversionDetail.SubmissionDate = "IBANConversionDetail.SubmissionDate";
                        SLMResp.Batch.DFR.IBANConversionDetail.SubmissionNumber = "IBANConversionDetail.SubmissionNumber";
                        SLMResp.Batch.DFR.IBANConversionDetail.EntityType = "IBANConversionDetail.EntityType";
                        SLMResp.Batch.DFR.IBANConversionDetail.EntityNumber = "IBANConversionDetail.EntityNumber";
                        SLMResp.Batch.DFR.IBANConversionDetail.MerchantOrderNumber = "IBANConversionDetail.MerchantOrderNumber";
                        SLMResp.Batch.DFR.IBANConversionDetail.MandateID = "IBANConversionDetail.MandateID";
                        SLMResp.Batch.DFR.IBANConversionDetail.MandateType = "IBANConversionDetail.MandateType";
                        SLMResp.Batch.DFR.IBANConversionDetail.SignatureDate = "IBANConversionDetail.SignatureDate";
                        SLMResp.Batch.DFR.IBANConversionDetail.CreditorID = "IBANConversionDetail.CreditorID";
                        SLMResp.Batch.DFR.IBANConversionDetail.BankSortCode = "IBANConversionDetail.BankSortCode";
                        SLMResp.Batch.DFR.IBANConversionDetail.AccountNumber = "IBANConversionDetail.AccountNumber";
                        SLMResp.Batch.DFR.IBANConversionDetail.IBAN = "IBANConversionDetail.IBAN";
                        SLMResp.Batch.DFR.IBANConversionDetail.BIC = "IBANConversionDetail.BIC";
                        SLMResp.Batch.DFR.IBANConversionDetail.CountryCode = "IBANConversionDetail.CountryCode";
                        SLMResp.Batch.DFR.IBANConversionDetail.ActionCode = "IBANConversionDetail.ActionCode";
                        SLMResp.Batch.DFR.IBANConversionDetail.Amount = "IBANConversionDetail.Amount";
                        SLMResp.Batch.DFR.IBANConversionDetail.ResponseReasonCode = "IBANConversionDetail.ResponseReasonCode";
                    }

                    public IBANConversionDetail()
                    {
                    }
                }

                public class IBANConversionDetailHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static IBANConversionDetailHeader()
                    {
                        SLMResp.Batch.DFR.IBANConversionDetailHeader.ReportType = "IBANConversionDetailHeader.ReportType";
                        SLMResp.Batch.DFR.IBANConversionDetailHeader.CompanyIDNumber = "IBANConversionDetailHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.IBANConversionDetailHeader.ReportDateFrom = "IBANConversionDetailHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.IBANConversionDetailHeader.ReportDateTo = "IBANConversionDetailHeader.ReportDateTo";
                        SLMResp.Batch.DFR.IBANConversionDetailHeader.ReportGenerationDate = "IBANConversionDetailHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.IBANConversionDetailHeader.ReportGenerationTime = "IBANConversionDetailHeader.ReportGenerationTime";
                    }

                    public IBANConversionDetailHeader()
                    {
                    }
                }

                public class InterchangeDowngradeSummary
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string Currency;

                    public readonly static string MethodOfPayment;

                    public readonly static string InterchangeQualification;

                    public readonly static string Count;

                    public readonly static string FrontEndDowngradeReasonCode;

                    public readonly static string DowngradeReasonCodeDescription;

                    public readonly static string SalesCount;

                    public readonly static string CountPercent;

                    public readonly static string SaleAmount;

                    public readonly static string AmountPercent;

                    static InterchangeDowngradeSummary()
                    {
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.ReportType = "InterchangeDowngradeSummary.ReportType";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.EntityType = "InterchangeDowngradeSummary.EntityType";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.EntityNumber = "InterchangeDowngradeSummary.EntityNumber";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.Currency = "InterchangeDowngradeSummary.Currency";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.MethodOfPayment = "InterchangeDowngradeSummary.MethodOfPayment";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.InterchangeQualification = "InterchangeDowngradeSummary.InterchangeQualification";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.Count = "InterchangeDowngradeSummary.Count";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.FrontEndDowngradeReasonCode = "InterchangeDowngradeSummary.FrontEndDowngradeReasonCode";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.DowngradeReasonCodeDescription = "InterchangeDowngradeSummary.DowngradeReasonCodeDescription";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.SalesCount = "InterchangeDowngradeSummary.SalesCount";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.CountPercent = "InterchangeDowngradeSummary.CountPercent";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.SaleAmount = "InterchangeDowngradeSummary.SaleAmount";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummary.AmountPercent = "InterchangeDowngradeSummary.AmountPercent";
                    }

                    public InterchangeDowngradeSummary()
                    {
                    }
                }

                public class InterchangeDowngradeSummaryHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static InterchangeDowngradeSummaryHeader()
                    {
                        SLMResp.Batch.DFR.InterchangeDowngradeSummaryHeader.ReportType = "InterchangeDowngradeSummaryHeader.ReportType";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummaryHeader.CompanyIDNumber = "InterchangeDowngradeSummaryHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummaryHeader.ReportDateFrom = "InterchangeDowngradeSummaryHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummaryHeader.ReportDateTo = "InterchangeDowngradeSummaryHeader.ReportDateTo";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummaryHeader.ReportGenerationDate = "InterchangeDowngradeSummaryHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.InterchangeDowngradeSummaryHeader.ReportGenerationTime = "InterchangeDowngradeSummaryHeader.ReportGenerationTime";
                    }

                    public InterchangeDowngradeSummaryHeader()
                    {
                    }
                }

                public class InterchangeQualificationDetail
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string PIDNumber;

                    public readonly static string Currency;

                    public readonly static string MethodOfPayment;

                    public readonly static string InterchangeQualification;

                    public readonly static string DowngradeReasonCode;

                    public readonly static string DowngradeReasonCodeDescription;

                    public readonly static string AccountNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string DepositDate;

                    public readonly static string Amount;

                    static InterchangeQualificationDetail()
                    {
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.ReportType = "InterchangeQualificationDetail.ReportType";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.EntityType = "InterchangeQualificationDetail.EntityType";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.EntityNumber = "InterchangeQualificationDetail.EntityNumber";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.PIDNumber = "InterchangeQualificationDetail.PIDNumber";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.Currency = "InterchangeQualificationDetail.Currency";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.MethodOfPayment = "InterchangeQualificationDetail.MethodOfPayment";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.InterchangeQualification = "InterchangeQualificationDetail.InterchangeQualification";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.DowngradeReasonCode = "InterchangeQualificationDetail.DowngradeReasonCode";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.DowngradeReasonCodeDescription = "InterchangeQualificationDetail.DowngradeReasonCodeDescription";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.AccountNumber = "InterchangeQualificationDetail.AccountNumber";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.MerchantOrderNumber = "InterchangeQualificationDetail.MerchantOrderNumber";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.DepositDate = "InterchangeQualificationDetail.DepositDate";
                        SLMResp.Batch.DFR.InterchangeQualificationDetail.Amount = "InterchangeQualificationDetail.Amount";
                    }

                    public InterchangeQualificationDetail()
                    {
                    }
                }

                public class InterchangeQualificationDetailHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static InterchangeQualificationDetailHeader()
                    {
                        SLMResp.Batch.DFR.InterchangeQualificationDetailHeader.ReportType = "InterchangeQualificationDetailHeader.ReportType";
                        SLMResp.Batch.DFR.InterchangeQualificationDetailHeader.CompanyIDNumber = "InterchangeQualificationDetailHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.InterchangeQualificationDetailHeader.ReportDateFrom = "InterchangeQualificationDetailHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.InterchangeQualificationDetailHeader.ReportDateTo = "InterchangeQualificationDetailHeader.ReportDateTo";
                        SLMResp.Batch.DFR.InterchangeQualificationDetailHeader.ReportGenerationDate = "InterchangeQualificationDetailHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.InterchangeQualificationDetailHeader.ReportGenerationTime = "InterchangeQualificationDetailHeader.ReportGenerationTime";
                    }

                    public InterchangeQualificationDetailHeader()
                    {
                    }
                }

                public class InterchangeQualificationSummary
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string Currency;

                    public readonly static string MethodOfPayment;

                    public readonly static string InterchangeQualification;

                    public readonly static string InterchangeQualificationDescription;

                    public readonly static string PercentageRate;

                    public readonly static string UnitFee;

                    public readonly static string SalesCount;

                    public readonly static string CountPercentage;

                    public readonly static string SalesAmount;

                    public readonly static string AmountPercentage;

                    static InterchangeQualificationSummary()
                    {
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.ReportType = "InterchangeQualificationSummary.ReportType";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.EntityType = "InterchangeQualificationSummary.EntityType";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.EntityNumber = "InterchangeQualificationSummary.EntityNumber";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.Currency = "InterchangeQualificationSummary.Currency";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.MethodOfPayment = "InterchangeQualificationSummary.MethodOfPayment";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.InterchangeQualification = "InterchangeQualificationSummary.InterchangeQualification";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.InterchangeQualificationDescription = "InterchangeQualificationSummary.InterchangeQualificationDescription";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.PercentageRate = "InterchangeQualificationSummary.PercentageRate";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.UnitFee = "InterchangeQualificationSummary.UnitFee";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.SalesCount = "InterchangeQualificationSummary.SalesCount";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.CountPercentage = "InterchangeQualificationSummary.CountPercentage";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.SalesAmount = "InterchangeQualificationSummary.SalesAmount";
                        SLMResp.Batch.DFR.InterchangeQualificationSummary.AmountPercentage = "InterchangeQualificationSummary.AmountPercentage";
                    }

                    public InterchangeQualificationSummary()
                    {
                    }
                }

                public class InterchangeQualificationSummaryHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static InterchangeQualificationSummaryHeader()
                    {
                        SLMResp.Batch.DFR.InterchangeQualificationSummaryHeader.ReportType = "InterchangeQualificationSummaryHeader.ReportType";
                        SLMResp.Batch.DFR.InterchangeQualificationSummaryHeader.CompanyIDNumber = "InterchangeQualificationSummaryHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.InterchangeQualificationSummaryHeader.ReportDateFrom = "InterchangeQualificationSummaryHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.InterchangeQualificationSummaryHeader.ReportDateTo = "InterchangeQualificationSummaryHeader.ReportDateTo";
                        SLMResp.Batch.DFR.InterchangeQualificationSummaryHeader.ReportGenerationDate = "InterchangeQualificationSummaryHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.InterchangeQualificationSummaryHeader.ReportGenerationTime = "InterchangeQualificationSummaryHeader.ReportGenerationTime";
                    }

                    public InterchangeQualificationSummaryHeader()
                    {
                    }
                }

                public class MasterCardExcessiveChargeback
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string Currency;

                    public readonly static string PTIReversalFlag;

                    public readonly static string SequenceNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string AccountNumber;

                    public readonly static string MCC;

                    public readonly static string ReasonCode;

                    public readonly static string MethodOfPayment;

                    public readonly static string OriginalTransactionDate;

                    public readonly static string InitiatedDate;

                    public readonly static string ActivityDate;

                    public readonly static string Amount;

                    public readonly static string Descriptor;

                    public readonly static string City;

                    public readonly static string State;

                    public readonly static string CNPRetailIndicator;

                    static MasterCardExcessiveChargeback()
                    {
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.ReportType = "MasterCardExcessiveChargeback.ReportType";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.EntityType = "MasterCardExcessiveChargeback.EntityType";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.EntityNumber = "MasterCardExcessiveChargeback.EntityNumber";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.Currency = "MasterCardExcessiveChargeback.Currency";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.PTIReversalFlag = "MasterCardExcessiveChargeback.PTIReversalFlag";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.SequenceNumber = "MasterCardExcessiveChargeback.SequenceNumber";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.MerchantOrderNumber = "MasterCardExcessiveChargeback.MerchantOrderNumber";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.AccountNumber = "MasterCardExcessiveChargeback.AccountNumber";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.MCC = "MasterCardExcessiveChargeback.MCC";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.ReasonCode = "MasterCardExcessiveChargeback.ReasonCode";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.MethodOfPayment = "MasterCardExcessiveChargeback.MethodOfPayment";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.OriginalTransactionDate = "MasterCardExcessiveChargeback.OriginalTransactionDate";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.InitiatedDate = "MasterCardExcessiveChargeback.InitiatedDate";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.ActivityDate = "MasterCardExcessiveChargeback.ActivityDate";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.Amount = "MasterCardExcessiveChargeback.Amount";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.Descriptor = "MasterCardExcessiveChargeback.Descriptor";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.City = "MasterCardExcessiveChargeback.City";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.State = "MasterCardExcessiveChargeback.State";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargeback.CNPRetailIndicator = "MasterCardExcessiveChargeback.CNPRetailIndicator";
                    }

                    public MasterCardExcessiveChargeback()
                    {
                    }
                }

                public class MasterCardExcessiveChargebackHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static MasterCardExcessiveChargebackHeader()
                    {
                        SLMResp.Batch.DFR.MasterCardExcessiveChargebackHeader.ReportType = "MasterCardExcessiveChargebackHeader.ReportType";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargebackHeader.CompanyIDNumber = "MasterCardExcessiveChargebackHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargebackHeader.ReportDateFrom = "MasterCardExcessiveChargebackHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargebackHeader.ReportDateTo = "MasterCardExcessiveChargebackHeader.ReportDateTo";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargebackHeader.ReportGenerationDate = "MasterCardExcessiveChargebackHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.MasterCardExcessiveChargebackHeader.ReportGenerationTime = "MasterCardExcessiveChargebackHeader.ReportGenerationTime";
                    }

                    public MasterCardExcessiveChargebackHeader()
                    {
                    }
                }

                public class MastercardFraudAdvice
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityID;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string IssuerCountryCode;

                    public readonly static string IssuerCountryName;

                    public readonly static string AcquirerICAName;

                    public readonly static string AccountNumber;

                    public readonly static string ProcessDate;

                    public readonly static string TransactionDate;

                    public readonly static string TransactionTime;

                    public readonly static string TransactionAmount;

                    public readonly static string CurrencyCode;

                    public readonly static string CurrencyName;

                    public readonly static string USDAmount;

                    public readonly static string AggregateMerchantID;

                    public readonly static string AggregateMerchantName;

                    public readonly static string KeyAggregateMerchantID;

                    public readonly static string KeyAggregateMerchantName;

                    public readonly static string ParentAggregateMerchantID;

                    public readonly static string ParentAggregateMerchantName;

                    public readonly static string MerchantLocationID;

                    public readonly static string MerchantID;

                    public readonly static string MerchantName;

                    public readonly static string MerchantCity;

                    public readonly static string MerchantState;

                    public readonly static string MerchantPostalCode;

                    public readonly static string CardProductType;

                    public readonly static string FraudTypeCode;

                    public readonly static string BanknetReferenceNumber;

                    public readonly static string MastercardAssignedID;

                    public readonly static string SequenceNumber;

                    public readonly static string RegionCode;

                    public readonly static string RegionName;

                    public readonly static string MCC;

                    public readonly static string MerchantCategoryName;

                    public readonly static string SecureCode;

                    public readonly static string AcquirerReferenceNumber;

                    public readonly static string POSEntryMode;

                    public readonly static string CardholderPresenceCode;

                    public readonly static string CVCCode;

                    public readonly static string FraudSubTypeCode;

                    public readonly static string CardNotPresentIndicator;

                    public readonly static string ATMCode;

                    public readonly static string ReasonCode;

                    public readonly static string ReasonName;

                    static MastercardFraudAdvice()
                    {
                        SLMResp.Batch.DFR.MastercardFraudAdvice.ReportType = "MastercardFraudAdvice.ReportType";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.EntityType = "MastercardFraudAdvice.EntityType";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.EntityID = "MastercardFraudAdvice.EntityID";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MerchantOrderNumber = "MastercardFraudAdvice.MerchantOrderNumber";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.IssuerCountryCode = "MastercardFraudAdvice.IssuerCountryCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.IssuerCountryName = "MastercardFraudAdvice.IssuerCountryName";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.AcquirerICAName = "MastercardFraudAdvice.AcquirerICAName";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.AccountNumber = "MastercardFraudAdvice.AccountNumber";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.ProcessDate = "MastercardFraudAdvice.ProcessDate";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.TransactionDate = "MastercardFraudAdvice.TransactionDate";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.TransactionTime = "MastercardFraudAdvice.TransactionTime";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.TransactionAmount = "MastercardFraudAdvice.TransactionAmount";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.CurrencyCode = "MastercardFraudAdvice.CurrencyCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.CurrencyName = "MastercardFraudAdvice.CurrencyName";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.USDAmount = "MastercardFraudAdvice.USDAmount";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.AggregateMerchantID = "MastercardFraudAdvice.AggregateMerchantID";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.AggregateMerchantName = "MastercardFraudAdvice.AggregateMerchantName";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.KeyAggregateMerchantID = "MastercardFraudAdvice.KeyAggregateMerchantID";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.KeyAggregateMerchantName = "MastercardFraudAdvice.KeyAggregateMerchantName";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.ParentAggregateMerchantID = "MastercardFraudAdvice.ParentAggregateMerchantID";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.ParentAggregateMerchantName = "MastercardFraudAdvice.ParentAggregateMerchantName";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MerchantLocationID = "MastercardFraudAdvice.MerchantLocationID";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MerchantID = "MastercardFraudAdvice.MerchantID";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MerchantName = "MastercardFraudAdvice.MerchantName";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MerchantCity = "MastercardFraudAdvice.MerchantCity";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MerchantState = "MastercardFraudAdvice.MerchantState";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MerchantPostalCode = "MastercardFraudAdvice.MerchantPostalCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.CardProductType = "MastercardFraudAdvice.CardProductType";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.FraudTypeCode = "MastercardFraudAdvice.FraudTypeCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.BanknetReferenceNumber = "MastercardFraudAdvice.BanknetReferenceNumber";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MastercardAssignedID = "MastercardFraudAdvice.MastercardAssignedID";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.SequenceNumber = "MastercardFraudAdvice.SequenceNumber";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.RegionCode = "MastercardFraudAdvice.RegionCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.RegionName = "MastercardFraudAdvice.RegionName";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MCC = "MastercardFraudAdvice.MCC";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.MerchantCategoryName = "MastercardFraudAdvice.MerchantCategoryName";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.SecureCode = "MastercardFraudAdvice.SecureCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.AcquirerReferenceNumber = "MastercardFraudAdvice.AcquirerReferenceNumber";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.POSEntryMode = "MastercardFraudAdvice.POSEntryMode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.CardholderPresenceCode = "MastercardFraudAdvice.CardholderPresenceCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.CVCCode = "MastercardFraudAdvice.CVCCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.FraudSubTypeCode = "MastercardFraudAdvice.FraudSubTypeCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.CardNotPresentIndicator = "MastercardFraudAdvice.CardNotPresentIndicator";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.ATMCode = "MastercardFraudAdvice.ATMCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.ReasonCode = "MastercardFraudAdvice.ReasonCode";
                        SLMResp.Batch.DFR.MastercardFraudAdvice.ReasonName = "MastercardFraudAdvice.ReasonName";
                    }

                    public MastercardFraudAdvice()
                    {
                    }
                }

                public class MastercardFraudAdviceHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static MastercardFraudAdviceHeader()
                    {
                        SLMResp.Batch.DFR.MastercardFraudAdviceHeader.ReportType = "MastercardFraudAdviceHeader.ReportType";
                        SLMResp.Batch.DFR.MastercardFraudAdviceHeader.CompanyIDNumber = "MastercardFraudAdviceHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.MastercardFraudAdviceHeader.ReportDateFrom = "MastercardFraudAdviceHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.MastercardFraudAdviceHeader.ReportDateTo = "MastercardFraudAdviceHeader.ReportDateTo";
                        SLMResp.Batch.DFR.MastercardFraudAdviceHeader.ReportGenerationDate = "MastercardFraudAdviceHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.MastercardFraudAdviceHeader.ReportGenerationTime = "MastercardFraudAdviceHeader.ReportGenerationTime";
                    }

                    public MastercardFraudAdviceHeader()
                    {
                    }
                }

                public class MerchantFXRate
                {
                    public readonly static string ReportType;

                    public readonly static string PresentmentCurrencyAlphaCode;

                    public readonly static string PresentmentCurrencyISONumericCode;

                    public readonly static string SettlementCurrencyAlphaCode;

                    public readonly static string SettlementCurrencyISONumericCode;

                    public readonly static string DailyFXRate;

                    static MerchantFXRate()
                    {
                        SLMResp.Batch.DFR.MerchantFXRate.ReportType = "MerchantFXRate.ReportType";
                        SLMResp.Batch.DFR.MerchantFXRate.PresentmentCurrencyAlphaCode = "MerchantFXRate.PresentmentCurrencyAlphaCode";
                        SLMResp.Batch.DFR.MerchantFXRate.PresentmentCurrencyISONumericCode = "MerchantFXRate.PresentmentCurrencyISONumericCode";
                        SLMResp.Batch.DFR.MerchantFXRate.SettlementCurrencyAlphaCode = "MerchantFXRate.SettlementCurrencyAlphaCode";
                        SLMResp.Batch.DFR.MerchantFXRate.SettlementCurrencyISONumericCode = "MerchantFXRate.SettlementCurrencyISONumericCode";
                        SLMResp.Batch.DFR.MerchantFXRate.DailyFXRate = "MerchantFXRate.DailyFXRate";
                    }

                    public MerchantFXRate()
                    {
                    }
                }

                public class MerchantFXRateHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string BeginningChasePaymentechActivityDate;

                    public readonly static string EndingChasePaymentechActivityDate;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static MerchantFXRateHeader()
                    {
                        SLMResp.Batch.DFR.MerchantFXRateHeader.ReportType = "MerchantFXRateHeader.ReportType";
                        SLMResp.Batch.DFR.MerchantFXRateHeader.CompanyIDNumber = "MerchantFXRateHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.MerchantFXRateHeader.BeginningChasePaymentechActivityDate = "MerchantFXRateHeader.BeginningChasePaymentechActivityDate";
                        SLMResp.Batch.DFR.MerchantFXRateHeader.EndingChasePaymentechActivityDate = "MerchantFXRateHeader.EndingChasePaymentechActivityDate";
                        SLMResp.Batch.DFR.MerchantFXRateHeader.ReportGenerationDate = "MerchantFXRateHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.MerchantFXRateHeader.ReportGenerationTime = "MerchantFXRateHeader.ReportGenerationTime";
                    }

                    public MerchantFXRateHeader()
                    {
                    }
                }

                public class MerchantSettlementAging
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string MethodOfPayment;

                    public readonly static string NumberDaysOutstanding;

                    public readonly static string SubmissionDate;

                    public readonly static string ActionCode;

                    public readonly static string PresentmentCurrencyAmount;

                    public readonly static string AccountNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string ContractIDNumber;

                    public readonly static string SettlementCurrency;

                    static MerchantSettlementAging()
                    {
                        SLMResp.Batch.DFR.MerchantSettlementAging.ReportType = "MerchantSettlementAging.ReportType";
                        SLMResp.Batch.DFR.MerchantSettlementAging.EntityType = "MerchantSettlementAging.EntityType";
                        SLMResp.Batch.DFR.MerchantSettlementAging.EntityNumber = "MerchantSettlementAging.EntityNumber";
                        SLMResp.Batch.DFR.MerchantSettlementAging.PresentmentCurrency = "MerchantSettlementAging.PresentmentCurrency";
                        SLMResp.Batch.DFR.MerchantSettlementAging.MethodOfPayment = "MerchantSettlementAging.MethodOfPayment";
                        SLMResp.Batch.DFR.MerchantSettlementAging.NumberDaysOutstanding = "MerchantSettlementAging.NumberDaysOutstanding";
                        SLMResp.Batch.DFR.MerchantSettlementAging.SubmissionDate = "MerchantSettlementAging.SubmissionDate";
                        SLMResp.Batch.DFR.MerchantSettlementAging.ActionCode = "MerchantSettlementAging.ActionCode";
                        SLMResp.Batch.DFR.MerchantSettlementAging.PresentmentCurrencyAmount = "MerchantSettlementAging.PresentmentCurrencyAmount";
                        SLMResp.Batch.DFR.MerchantSettlementAging.AccountNumber = "MerchantSettlementAging.AccountNumber";
                        SLMResp.Batch.DFR.MerchantSettlementAging.MerchantOrderNumber = "MerchantSettlementAging.MerchantOrderNumber";
                        SLMResp.Batch.DFR.MerchantSettlementAging.ContractIDNumber = "MerchantSettlementAging.ContractIDNumber";
                        SLMResp.Batch.DFR.MerchantSettlementAging.SettlementCurrency = "MerchantSettlementAging.SettlementCurrency";
                    }

                    public MerchantSettlementAging()
                    {
                    }
                }

                public class MerchantSettlementAgingHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static MerchantSettlementAgingHeader()
                    {
                        SLMResp.Batch.DFR.MerchantSettlementAgingHeader.ReportType = "MerchantSettlementAgingHeader.ReportType";
                        SLMResp.Batch.DFR.MerchantSettlementAgingHeader.CompanyIDNumber = "MerchantSettlementAgingHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.MerchantSettlementAgingHeader.ReportDateFrom = "MerchantSettlementAgingHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.MerchantSettlementAgingHeader.ReportDateTo = "MerchantSettlementAgingHeader.ReportDateTo";
                        SLMResp.Batch.DFR.MerchantSettlementAgingHeader.ReportGenerationDate = "MerchantSettlementAgingHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.MerchantSettlementAgingHeader.ReportGenerationTime = "MerchantSettlementAgingHeader.ReportGenerationTime";
                    }

                    public MerchantSettlementAgingHeader()
                    {
                    }
                }

                public class NotificationOfChange
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string Category;

                    public readonly static string AccountType;

                    public readonly static string RDFINumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string AccountNumber;

                    public readonly static string ConsumerName;

                    public readonly static string DepositDate;

                    public readonly static string NOCCode;

                    public readonly static string CorrectedRDFIRoutingNumber;

                    public readonly static string CorrectedRDFIAccountNumber;

                    public readonly static string CorrectedConsumerName;

                    public readonly static string CorrectedAccountType;

                    public readonly static string Source;

                    static NotificationOfChange()
                    {
                        SLMResp.Batch.DFR.NotificationOfChange.ReportType = "NotificationOfChange.ReportType";
                        SLMResp.Batch.DFR.NotificationOfChange.EntityType = "NotificationOfChange.EntityType";
                        SLMResp.Batch.DFR.NotificationOfChange.EntityNumber = "NotificationOfChange.EntityNumber";
                        SLMResp.Batch.DFR.NotificationOfChange.PresentmentCurrency = "NotificationOfChange.PresentmentCurrency";
                        SLMResp.Batch.DFR.NotificationOfChange.Category = "NotificationOfChange.Category";
                        SLMResp.Batch.DFR.NotificationOfChange.AccountType = "NotificationOfChange.AccountType";
                        SLMResp.Batch.DFR.NotificationOfChange.RDFINumber = "NotificationOfChange.RDFINumber";
                        SLMResp.Batch.DFR.NotificationOfChange.MerchantOrderNumber = "NotificationOfChange.MerchantOrderNumber";
                        SLMResp.Batch.DFR.NotificationOfChange.AccountNumber = "NotificationOfChange.AccountNumber";
                        SLMResp.Batch.DFR.NotificationOfChange.ConsumerName = "NotificationOfChange.ConsumerName";
                        SLMResp.Batch.DFR.NotificationOfChange.DepositDate = "NotificationOfChange.DepositDate";
                        SLMResp.Batch.DFR.NotificationOfChange.NOCCode = "NotificationOfChange.NOCCode";
                        SLMResp.Batch.DFR.NotificationOfChange.CorrectedRDFIRoutingNumber = "NotificationOfChange.CorrectedRDFIRoutingNumber";
                        SLMResp.Batch.DFR.NotificationOfChange.CorrectedRDFIAccountNumber = "NotificationOfChange.CorrectedRDFIAccountNumber";
                        SLMResp.Batch.DFR.NotificationOfChange.CorrectedConsumerName = "NotificationOfChange.CorrectedConsumerName";
                        SLMResp.Batch.DFR.NotificationOfChange.CorrectedAccountType = "NotificationOfChange.CorrectedAccountType";
                        SLMResp.Batch.DFR.NotificationOfChange.Source = "NotificationOfChange.Source";
                    }

                    public NotificationOfChange()
                    {
                    }
                }

                public class NotificationOfChangeHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static NotificationOfChangeHeader()
                    {
                        SLMResp.Batch.DFR.NotificationOfChangeHeader.ReportType = "NotificationOfChangeHeader.ReportType";
                        SLMResp.Batch.DFR.NotificationOfChangeHeader.CompanyIDNumber = "NotificationOfChangeHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.NotificationOfChangeHeader.ReportDateFrom = "NotificationOfChangeHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.NotificationOfChangeHeader.ReportDateTo = "NotificationOfChangeHeader.ReportDateTo";
                        SLMResp.Batch.DFR.NotificationOfChangeHeader.ReportGenerationDate = "NotificationOfChangeHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.NotificationOfChangeHeader.ReportGenerationTime = "NotificationOfChangeHeader.ReportGenerationTime";
                    }

                    public NotificationOfChangeHeader()
                    {
                    }
                }

                public class RejectSummary
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string FundsTransferInstructionNumber;

                    public readonly static string SecureBANumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string MethodOfPayment;

                    public readonly static string ActionCode;

                    public readonly static string SettledConveyed;

                    public readonly static string Count;

                    public readonly static string Amount;

                    public readonly static string SettlementCurrency;

                    static RejectSummary()
                    {
                        SLMResp.Batch.DFR.RejectSummary.ReportType = "RejectSummary.ReportType";
                        SLMResp.Batch.DFR.RejectSummary.EntityType = "RejectSummary.EntityType";
                        SLMResp.Batch.DFR.RejectSummary.EntityNumber = "RejectSummary.EntityNumber";
                        SLMResp.Batch.DFR.RejectSummary.FundsTransferInstructionNumber = "RejectSummary.FundsTransferInstructionNumber";
                        SLMResp.Batch.DFR.RejectSummary.SecureBANumber = "RejectSummary.SecureBANumber";
                        SLMResp.Batch.DFR.RejectSummary.PresentmentCurrency = "RejectSummary.PresentmentCurrency";
                        SLMResp.Batch.DFR.RejectSummary.MethodOfPayment = "RejectSummary.MethodOfPayment";
                        SLMResp.Batch.DFR.RejectSummary.ActionCode = "RejectSummary.ActionCode";
                        SLMResp.Batch.DFR.RejectSummary.SettledConveyed = "RejectSummary.SettledConveyed";
                        SLMResp.Batch.DFR.RejectSummary.Count = "RejectSummary.Count";
                        SLMResp.Batch.DFR.RejectSummary.Amount = "RejectSummary.Amount";
                        SLMResp.Batch.DFR.RejectSummary.SettlementCurrency = "RejectSummary.SettlementCurrency";
                    }

                    public RejectSummary()
                    {
                    }
                }

                public class RejectSummaryHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static RejectSummaryHeader()
                    {
                        SLMResp.Batch.DFR.RejectSummaryHeader.ReportType = "RejectSummaryHeader.ReportType";
                        SLMResp.Batch.DFR.RejectSummaryHeader.CompanyIDNumber = "RejectSummaryHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.RejectSummaryHeader.ReportDateFrom = "RejectSummaryHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.RejectSummaryHeader.ReportDateTo = "RejectSummaryHeader.ReportDateTo";
                        SLMResp.Batch.DFR.RejectSummaryHeader.ReportGenerationDate = "RejectSummaryHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.RejectSummaryHeader.ReportGenerationTime = "RejectSummaryHeader.ReportGenerationTime";
                    }

                    public RejectSummaryHeader()
                    {
                    }
                }

                public class RetrievalDetail
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string Currency;

                    public readonly static string Category;

                    public readonly static string SequenceNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string AccountNumber;

                    public readonly static string AuthorizationCode;

                    public readonly static string RetrievalReasonCode;

                    public readonly static string TransactionDate;

                    public readonly static string RetrievalReceivedDate;

                    public readonly static string ResponseDueDate;

                    public readonly static string RetrievalAmount;

                    public readonly static string TerminalNumber;

                    public readonly static string BatchNumber;

                    public readonly static string FulfillmentAttribute;

                    public readonly static string AuthorizationDate;

                    public readonly static string ARN;

                    static RetrievalDetail()
                    {
                        SLMResp.Batch.DFR.RetrievalDetail.ReportType = "RetrievalDetail.ReportType";
                        SLMResp.Batch.DFR.RetrievalDetail.EntityType = "RetrievalDetail.EntityType";
                        SLMResp.Batch.DFR.RetrievalDetail.EntityNumber = "RetrievalDetail.EntityNumber";
                        SLMResp.Batch.DFR.RetrievalDetail.Currency = "RetrievalDetail.Currency";
                        SLMResp.Batch.DFR.RetrievalDetail.Category = "RetrievalDetail.Category";
                        SLMResp.Batch.DFR.RetrievalDetail.SequenceNumber = "RetrievalDetail.SequenceNumber";
                        SLMResp.Batch.DFR.RetrievalDetail.MerchantOrderNumber = "RetrievalDetail.MerchantOrderNumber";
                        SLMResp.Batch.DFR.RetrievalDetail.AccountNumber = "RetrievalDetail.AccountNumber";
                        SLMResp.Batch.DFR.RetrievalDetail.AuthorizationCode = "RetrievalDetail.AuthorizationCode";
                        SLMResp.Batch.DFR.RetrievalDetail.RetrievalReasonCode = "RetrievalDetail.RetrievalReasonCode";
                        SLMResp.Batch.DFR.RetrievalDetail.TransactionDate = "RetrievalDetail.TransactionDate";
                        SLMResp.Batch.DFR.RetrievalDetail.RetrievalReceivedDate = "RetrievalDetail.RetrievalReceivedDate";
                        SLMResp.Batch.DFR.RetrievalDetail.ResponseDueDate = "RetrievalDetail.ResponseDueDate";
                        SLMResp.Batch.DFR.RetrievalDetail.RetrievalAmount = "RetrievalDetail.RetrievalAmount";
                        SLMResp.Batch.DFR.RetrievalDetail.TerminalNumber = "RetrievalDetail.TerminalNumber";
                        SLMResp.Batch.DFR.RetrievalDetail.BatchNumber = "RetrievalDetail.BatchNumber";
                        SLMResp.Batch.DFR.RetrievalDetail.FulfillmentAttribute = "RetrievalDetail.FulfillmentAttribute";
                        SLMResp.Batch.DFR.RetrievalDetail.AuthorizationDate = "RetrievalDetail.AuthorizationDate";
                        SLMResp.Batch.DFR.RetrievalDetail.ARN = "RetrievalDetail.ARN";
                    }

                    public RetrievalDetail()
                    {
                    }
                }

                public class RetrievalDetailHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static RetrievalDetailHeader()
                    {
                        SLMResp.Batch.DFR.RetrievalDetailHeader.ReportType = "RetrievalDetailHeader.ReportType";
                        SLMResp.Batch.DFR.RetrievalDetailHeader.CompanyIDNumber = "RetrievalDetailHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.RetrievalDetailHeader.ReportDateFrom = "RetrievalDetailHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.RetrievalDetailHeader.ReportDateTo = "RetrievalDetailHeader.ReportDateTo";
                        SLMResp.Batch.DFR.RetrievalDetailHeader.ReportGenerationDate = "RetrievalDetailHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.RetrievalDetailHeader.ReportGenerationTime = "RetrievalDetailHeader.ReportGenerationTime";
                    }

                    public RetrievalDetailHeader()
                    {
                    }
                }

                public class ServiceCharge
                {
                    public readonly static string ReportType;

                    public readonly static string Category;

                    public readonly static string SubCategory;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string FundsTransferInstructionNumber;

                    public readonly static string SecureBANumber;

                    public readonly static string SettlementCurrency;

                    public readonly static string FeeSchedule;

                    public readonly static string MethodOfPayment;

                    public readonly static string InterchangeQualification;

                    public readonly static string FeeTypeDescription;

                    public readonly static string ActionType;

                    public readonly static string UnitQuantity;

                    public readonly static string UnitFee;

                    public readonly static string Amount;

                    public readonly static string PercentageRate;

                    public readonly static string TotalCharge;

                    public readonly static string PresentmentCurrency;

                    static ServiceCharge()
                    {
                        SLMResp.Batch.DFR.ServiceCharge.ReportType = "ServiceCharge.ReportType";
                        SLMResp.Batch.DFR.ServiceCharge.Category = "ServiceCharge.Category";
                        SLMResp.Batch.DFR.ServiceCharge.SubCategory = "ServiceCharge.SubCategory";
                        SLMResp.Batch.DFR.ServiceCharge.EntityType = "ServiceCharge.EntityType";
                        SLMResp.Batch.DFR.ServiceCharge.EntityNumber = "ServiceCharge.EntityNumber";
                        SLMResp.Batch.DFR.ServiceCharge.FundsTransferInstructionNumber = "ServiceCharge.FundsTransferInstructionNumber";
                        SLMResp.Batch.DFR.ServiceCharge.SecureBANumber = "ServiceCharge.SecureBANumber";
                        SLMResp.Batch.DFR.ServiceCharge.SettlementCurrency = "ServiceCharge.SettlementCurrency";
                        SLMResp.Batch.DFR.ServiceCharge.FeeSchedule = "ServiceCharge.FeeSchedule";
                        SLMResp.Batch.DFR.ServiceCharge.MethodOfPayment = "ServiceCharge.MethodOfPayment";
                        SLMResp.Batch.DFR.ServiceCharge.InterchangeQualification = "ServiceCharge.InterchangeQualification";
                        SLMResp.Batch.DFR.ServiceCharge.FeeTypeDescription = "ServiceCharge.FeeTypeDescription";
                        SLMResp.Batch.DFR.ServiceCharge.ActionType = "ServiceCharge.ActionType";
                        SLMResp.Batch.DFR.ServiceCharge.UnitQuantity = "ServiceCharge.UnitQuantity";
                        SLMResp.Batch.DFR.ServiceCharge.UnitFee = "ServiceCharge.UnitFee";
                        SLMResp.Batch.DFR.ServiceCharge.Amount = "ServiceCharge.Amount";
                        SLMResp.Batch.DFR.ServiceCharge.PercentageRate = "ServiceCharge.PercentageRate";
                        SLMResp.Batch.DFR.ServiceCharge.TotalCharge = "ServiceCharge.TotalCharge";
                        SLMResp.Batch.DFR.ServiceCharge.PresentmentCurrency = "ServiceCharge.PresentmentCurrency";
                    }

                    public ServiceCharge()
                    {
                    }
                }

                public class ServiceChargeHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static ServiceChargeHeader()
                    {
                        SLMResp.Batch.DFR.ServiceChargeHeader.ReportType = "ServiceChargeHeader.ReportType";
                        SLMResp.Batch.DFR.ServiceChargeHeader.CompanyIDNumber = "ServiceChargeHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.ServiceChargeHeader.ReportDateFrom = "ServiceChargeHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.ServiceChargeHeader.ReportDateTo = "ServiceChargeHeader.ReportDateTo";
                        SLMResp.Batch.DFR.ServiceChargeHeader.ReportGenerationDate = "ServiceChargeHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.ServiceChargeHeader.ReportGenerationTime = "ServiceChargeHeader.ReportGenerationTime";
                    }

                    public ServiceChargeHeader()
                    {
                    }
                }

                public class SubmissionListing
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string SubmissionNumber;

                    public readonly static string PIDNumber;

                    public readonly static string PIDShortName;

                    public readonly static string SubmissionDate;

                    public readonly static string SubmissionTime;

                    public readonly static string TransactionCount;

                    public readonly static string AuthorizationCount;

                    public readonly static string NonFinancialTransactionCount;

                    public readonly static string DeclinedDepositCount;

                    public readonly static string RejectedTransactionCount;

                    public readonly static string CancelledOnHoldDepositCount;

                    public readonly static string CancelledOnHoldNetDepositCount;

                    public readonly static string SuccessfulDepositCount;

                    public readonly static string SuccessfulNetDepositCount;

                    public readonly static string Status;

                    public readonly static string SettlementCurrency;

                    static SubmissionListing()
                    {
                        SLMResp.Batch.DFR.SubmissionListing.ReportType = "SubmissionListing.ReportType";
                        SLMResp.Batch.DFR.SubmissionListing.EntityType = "SubmissionListing.EntityType";
                        SLMResp.Batch.DFR.SubmissionListing.EntityNumber = "SubmissionListing.EntityNumber";
                        SLMResp.Batch.DFR.SubmissionListing.PresentmentCurrency = "SubmissionListing.PresentmentCurrency";
                        SLMResp.Batch.DFR.SubmissionListing.SubmissionNumber = "SubmissionListing.SubmissionNumber";
                        SLMResp.Batch.DFR.SubmissionListing.PIDNumber = "SubmissionListing.PIDNumber";
                        SLMResp.Batch.DFR.SubmissionListing.PIDShortName = "SubmissionListing.PIDShortName";
                        SLMResp.Batch.DFR.SubmissionListing.SubmissionDate = "SubmissionListing.SubmissionDate";
                        SLMResp.Batch.DFR.SubmissionListing.SubmissionTime = "SubmissionListing.SubmissionTime";
                        SLMResp.Batch.DFR.SubmissionListing.TransactionCount = "SubmissionListing.TransactionCount";
                        SLMResp.Batch.DFR.SubmissionListing.AuthorizationCount = "SubmissionListing.AuthorizationCount";
                        SLMResp.Batch.DFR.SubmissionListing.NonFinancialTransactionCount = "SubmissionListing.NonFinancialTransactionCount";
                        SLMResp.Batch.DFR.SubmissionListing.DeclinedDepositCount = "SubmissionListing.DeclinedDepositCount";
                        SLMResp.Batch.DFR.SubmissionListing.RejectedTransactionCount = "SubmissionListing.RejectedTransactionCount";
                        SLMResp.Batch.DFR.SubmissionListing.CancelledOnHoldDepositCount = "SubmissionListing.CancelledOnHoldDepositCount";
                        SLMResp.Batch.DFR.SubmissionListing.CancelledOnHoldNetDepositCount = "SubmissionListing.CancelledOnHoldNetDepositCount";
                        SLMResp.Batch.DFR.SubmissionListing.SuccessfulDepositCount = "SubmissionListing.SuccessfulDepositCount";
                        SLMResp.Batch.DFR.SubmissionListing.SuccessfulNetDepositCount = "SubmissionListing.SuccessfulNetDepositCount";
                        SLMResp.Batch.DFR.SubmissionListing.Status = "SubmissionListing.Status";
                        SLMResp.Batch.DFR.SubmissionListing.SettlementCurrency = "SubmissionListing.SettlementCurrency";
                    }

                    public SubmissionListing()
                    {
                    }
                }

                public class SubmissionListingHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static SubmissionListingHeader()
                    {
                        SLMResp.Batch.DFR.SubmissionListingHeader.ReportType = "SubmissionListingHeader.ReportType";
                        SLMResp.Batch.DFR.SubmissionListingHeader.CompanyIDNumber = "SubmissionListingHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.SubmissionListingHeader.ReportDateFrom = "SubmissionListingHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.SubmissionListingHeader.ReportDateTo = "SubmissionListingHeader.ReportDateTo";
                        SLMResp.Batch.DFR.SubmissionListingHeader.ReportGenerationDate = "SubmissionListingHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.SubmissionListingHeader.ReportGenerationTime = "SubmissionListingHeader.ReportGenerationTime";
                    }

                    public SubmissionListingHeader()
                    {
                    }
                }

                public class TerminalBatchDetail
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string TerminalNumber;

                    public readonly static string BatchNumber;

                    public readonly static string AccountNumber;

                    public readonly static string Encrypt;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string TransactionDate;

                    public readonly static string ActionType;

                    public readonly static string AuthCode;

                    public readonly static string Amount;

                    public readonly static string Total;

                    static TerminalBatchDetail()
                    {
                        SLMResp.Batch.DFR.TerminalBatchDetail.ReportType = "TerminalBatchDetail.ReportType";
                        SLMResp.Batch.DFR.TerminalBatchDetail.EntityType = "TerminalBatchDetail.EntityType";
                        SLMResp.Batch.DFR.TerminalBatchDetail.EntityNumber = "TerminalBatchDetail.EntityNumber";
                        SLMResp.Batch.DFR.TerminalBatchDetail.PresentmentCurrency = "TerminalBatchDetail.PresentmentCurrency";
                        SLMResp.Batch.DFR.TerminalBatchDetail.TerminalNumber = "TerminalBatchDetail.TerminalNumber";
                        SLMResp.Batch.DFR.TerminalBatchDetail.BatchNumber = "TerminalBatchDetail.BatchNumber";
                        SLMResp.Batch.DFR.TerminalBatchDetail.AccountNumber = "TerminalBatchDetail.AccountNumber";
                        SLMResp.Batch.DFR.TerminalBatchDetail.Encrypt = "TerminalBatchDetail.Encrypt";
                        SLMResp.Batch.DFR.TerminalBatchDetail.MerchantOrderNumber = "TerminalBatchDetail.MerchantOrderNumber";
                        SLMResp.Batch.DFR.TerminalBatchDetail.TransactionDate = "TerminalBatchDetail.TransactionDate";
                        SLMResp.Batch.DFR.TerminalBatchDetail.ActionType = "TerminalBatchDetail.ActionType";
                        SLMResp.Batch.DFR.TerminalBatchDetail.AuthCode = "TerminalBatchDetail.AuthCode";
                        SLMResp.Batch.DFR.TerminalBatchDetail.Amount = "TerminalBatchDetail.Amount";
                        SLMResp.Batch.DFR.TerminalBatchDetail.Total = "TerminalBatchDetail.Total";
                    }

                    public TerminalBatchDetail()
                    {
                    }
                }

                public class TerminalBatchDetailHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static TerminalBatchDetailHeader()
                    {
                        SLMResp.Batch.DFR.TerminalBatchDetailHeader.ReportType = "TerminalBatchDetailHeader.ReportType";
                        SLMResp.Batch.DFR.TerminalBatchDetailHeader.CompanyIDNumber = "TerminalBatchDetailHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.TerminalBatchDetailHeader.ReportDateFrom = "TerminalBatchDetailHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.TerminalBatchDetailHeader.ReportDateTo = "TerminalBatchDetailHeader.ReportDateTo";
                        SLMResp.Batch.DFR.TerminalBatchDetailHeader.ReportGenerationDate = "TerminalBatchDetailHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.TerminalBatchDetailHeader.ReportGenerationTime = "TerminalBatchDetailHeader.ReportGenerationTime";
                    }

                    public TerminalBatchDetailHeader()
                    {
                    }
                }

                public class TransactionSummary
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityNumber;

                    public readonly static string FundsTransferInstructionNumber;

                    public readonly static string SecureBANumber;

                    public readonly static string PresentmentCurrency;

                    public readonly static string MethodOfPayment;

                    public readonly static string Category;

                    public readonly static string SettledConveyed;

                    public readonly static string Count;

                    public readonly static string Amount;

                    public readonly static string SettlementCurrency;

                    static TransactionSummary()
                    {
                        SLMResp.Batch.DFR.TransactionSummary.ReportType = "TransactionSummary.ReportType";
                        SLMResp.Batch.DFR.TransactionSummary.EntityType = "TransactionSummary.EntityType";
                        SLMResp.Batch.DFR.TransactionSummary.EntityNumber = "TransactionSummary.EntityNumber";
                        SLMResp.Batch.DFR.TransactionSummary.FundsTransferInstructionNumber = "TransactionSummary.FundsTransferInstructionNumber";
                        SLMResp.Batch.DFR.TransactionSummary.SecureBANumber = "TransactionSummary.SecureBANumber";
                        SLMResp.Batch.DFR.TransactionSummary.PresentmentCurrency = "TransactionSummary.PresentmentCurrency";
                        SLMResp.Batch.DFR.TransactionSummary.MethodOfPayment = "TransactionSummary.MethodOfPayment";
                        SLMResp.Batch.DFR.TransactionSummary.Category = "TransactionSummary.Category";
                        SLMResp.Batch.DFR.TransactionSummary.SettledConveyed = "TransactionSummary.SettledConveyed";
                        SLMResp.Batch.DFR.TransactionSummary.Count = "TransactionSummary.Count";
                        SLMResp.Batch.DFR.TransactionSummary.Amount = "TransactionSummary.Amount";
                        SLMResp.Batch.DFR.TransactionSummary.SettlementCurrency = "TransactionSummary.SettlementCurrency";
                    }

                    public TransactionSummary()
                    {
                    }
                }

                public class TransactionSummaryHeader
                {
                    public readonly static string ReportType;

                    public readonly static string CompanyIDNumber;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static TransactionSummaryHeader()
                    {
                        SLMResp.Batch.DFR.TransactionSummaryHeader.ReportType = "TransactionSummaryHeader.ReportType";
                        SLMResp.Batch.DFR.TransactionSummaryHeader.CompanyIDNumber = "TransactionSummaryHeader.CompanyIDNumber";
                        SLMResp.Batch.DFR.TransactionSummaryHeader.ReportDateFrom = "TransactionSummaryHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.TransactionSummaryHeader.ReportDateTo = "TransactionSummaryHeader.ReportDateTo";
                        SLMResp.Batch.DFR.TransactionSummaryHeader.ReportGenerationDate = "TransactionSummaryHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.TransactionSummaryHeader.ReportGenerationTime = "TransactionSummaryHeader.ReportGenerationTime";
                    }

                    public TransactionSummaryHeader()
                    {
                    }
                }

                public class VisaFraudAdvice
                {
                    public readonly static string ReportType;

                    public readonly static string EntityType;

                    public readonly static string EntityID;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string AccountSequenceNumber;

                    public readonly static string ReferenceID;

                    public readonly static string AquirerReferenceNumber;

                    public readonly static string AuthorizationCode;

                    public readonly static string CardAcceptorID;

                    public readonly static string AccountNumber;

                    public readonly static string CardCapabilityCode;

                    public readonly static string ExpirationDate;

                    public readonly static string CardholderIdentificationMethod;

                    public readonly static string FraudAmount;

                    public readonly static string FraudCurrencyCode;

                    public readonly static string FraudType;

                    public readonly static string MCC;

                    public readonly static string MerchantCity;

                    public readonly static string MerchantCountry;

                    public readonly static string MerchantName;

                    public readonly static string MerchantPostalCode;

                    public readonly static string MerchantStateOrProvince;

                    public readonly static string MOTOECI;

                    public readonly static string MultiClearingSequenceNumber;

                    public readonly static string NetworkID;

                    public readonly static string NotificationCode;

                    public readonly static string POSEntryMode;

                    public readonly static string POSTerminalCapability;

                    public readonly static string PurchaseDate;

                    static VisaFraudAdvice()
                    {
                        SLMResp.Batch.DFR.VisaFraudAdvice.ReportType = "VisaFraudAdvice.ReportType";
                        SLMResp.Batch.DFR.VisaFraudAdvice.EntityType = "VisaFraudAdvice.EntityType";
                        SLMResp.Batch.DFR.VisaFraudAdvice.EntityID = "VisaFraudAdvice.EntityID";
                        SLMResp.Batch.DFR.VisaFraudAdvice.MerchantOrderNumber = "VisaFraudAdvice.MerchantOrderNumber";
                        SLMResp.Batch.DFR.VisaFraudAdvice.AccountSequenceNumber = "VisaFraudAdvice.AccountSequenceNumber";
                        SLMResp.Batch.DFR.VisaFraudAdvice.ReferenceID = "VisaFraudAdvice.ReferenceID";
                        SLMResp.Batch.DFR.VisaFraudAdvice.AquirerReferenceNumber = "VisaFraudAdvice.AquirerReferenceNumber";
                        SLMResp.Batch.DFR.VisaFraudAdvice.AuthorizationCode = "VisaFraudAdvice.AuthorizationCode";
                        SLMResp.Batch.DFR.VisaFraudAdvice.CardAcceptorID = "VisaFraudAdvice.CardAcceptorID";
                        SLMResp.Batch.DFR.VisaFraudAdvice.AccountNumber = "VisaFraudAdvice.AccountNumber";
                        SLMResp.Batch.DFR.VisaFraudAdvice.CardCapabilityCode = "VisaFraudAdvice.CardCapabilityCode";
                        SLMResp.Batch.DFR.VisaFraudAdvice.ExpirationDate = "VisaFraudAdvice.ExpirationDate";
                        SLMResp.Batch.DFR.VisaFraudAdvice.CardholderIdentificationMethod = "VisaFraudAdvice.CardholderIdentificationMethod";
                        SLMResp.Batch.DFR.VisaFraudAdvice.FraudAmount = "VisaFraudAdvice.FraudAmount";
                        SLMResp.Batch.DFR.VisaFraudAdvice.FraudCurrencyCode = "VisaFraudAdvice.FraudCurrencyCode";
                        SLMResp.Batch.DFR.VisaFraudAdvice.FraudType = "VisaFraudAdvice.FraudType";
                        SLMResp.Batch.DFR.VisaFraudAdvice.MCC = "VisaFraudAdvice.MCC";
                        SLMResp.Batch.DFR.VisaFraudAdvice.MerchantCity = "VisaFraudAdvice.MerchantCity";
                        SLMResp.Batch.DFR.VisaFraudAdvice.MerchantCountry = "VisaFraudAdvice.MerchantCountry";
                        SLMResp.Batch.DFR.VisaFraudAdvice.MerchantName = "VisaFraudAdvice.MerchantName";
                        SLMResp.Batch.DFR.VisaFraudAdvice.MerchantPostalCode = "VisaFraudAdvice.MerchantPostalCode";
                        SLMResp.Batch.DFR.VisaFraudAdvice.MerchantStateOrProvince = "VisaFraudAdvice.MerchantStateOrProvince";
                        SLMResp.Batch.DFR.VisaFraudAdvice.MOTOECI = "VisaFraudAdvice.MOTOECI";
                        SLMResp.Batch.DFR.VisaFraudAdvice.MultiClearingSequenceNumber = "VisaFraudAdvice.MultiClearingSequenceNumber";
                        SLMResp.Batch.DFR.VisaFraudAdvice.NetworkID = "VisaFraudAdvice.NetworkID";
                        SLMResp.Batch.DFR.VisaFraudAdvice.NotificationCode = "VisaFraudAdvice.NotificationCode";
                        SLMResp.Batch.DFR.VisaFraudAdvice.POSEntryMode = "VisaFraudAdvice.POSEntryMode";
                        SLMResp.Batch.DFR.VisaFraudAdvice.POSTerminalCapability = "VisaFraudAdvice.POSTerminalCapability";
                        SLMResp.Batch.DFR.VisaFraudAdvice.PurchaseDate = "VisaFraudAdvice.PurchaseDate";
                    }

                    public VisaFraudAdvice()
                    {
                    }
                }

                public class VisaFraudAdviceHeader
                {
                    public readonly static string ReportType;

                    public readonly static string EntityID;

                    public readonly static string ReportDateFrom;

                    public readonly static string ReportDateTo;

                    public readonly static string ReportGenerationDate;

                    public readonly static string ReportGenerationTime;

                    static VisaFraudAdviceHeader()
                    {
                        SLMResp.Batch.DFR.VisaFraudAdviceHeader.ReportType = "VisaFraudAdviceHeader.ReportType";
                        SLMResp.Batch.DFR.VisaFraudAdviceHeader.EntityID = "VisaFraudAdviceHeader.EntityID";
                        SLMResp.Batch.DFR.VisaFraudAdviceHeader.ReportDateFrom = "VisaFraudAdviceHeader.ReportDateFrom";
                        SLMResp.Batch.DFR.VisaFraudAdviceHeader.ReportDateTo = "VisaFraudAdviceHeader.ReportDateTo";
                        SLMResp.Batch.DFR.VisaFraudAdviceHeader.ReportGenerationDate = "VisaFraudAdviceHeader.ReportGenerationDate";
                        SLMResp.Batch.DFR.VisaFraudAdviceHeader.ReportGenerationTime = "VisaFraudAdviceHeader.ReportGenerationTime";
                    }

                    public VisaFraudAdviceHeader()
                    {
                    }
                }
            }

            public class DigitalPAN1
            {
                public readonly static string TokenAssuranceLevel;

                public readonly static string AccountStatus;

                public readonly static string TokenRequestorID;

                static DigitalPAN1()
                {
                    SLMResp.Batch.DigitalPAN1.TokenAssuranceLevel = "DigitalPAN1.TokenAssuranceLevel";
                    SLMResp.Batch.DigitalPAN1.AccountStatus = "DigitalPAN1.AccountStatus";
                    SLMResp.Batch.DigitalPAN1.TokenRequestorID = "DigitalPAN1.TokenRequestorID";
                }

                public DigitalPAN1()
                {
                }
            }

            public class DinersExtRecord1
            {
                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string POSAuthSource;

                public readonly static string POSCardIDMethod;

                public readonly static string BankNetRefNumber;

                public readonly static string AuthorizedAmount;

                public readonly static string BankNetDate;

                public readonly static string MerchantCategoryCode;

                public readonly static string CATType;

                static DinersExtRecord1()
                {
                    SLMResp.Batch.DinersExtRecord1.POSCapabilityCode = "DinersExtRecord1.POSCapabilityCode";
                    SLMResp.Batch.DinersExtRecord1.POSEntryMode = "DinersExtRecord1.POSEntryMode";
                    SLMResp.Batch.DinersExtRecord1.POSAuthSource = "DinersExtRecord1.POSAuthSource";
                    SLMResp.Batch.DinersExtRecord1.POSCardIDMethod = "DinersExtRecord1.POSCardIDMethod";
                    SLMResp.Batch.DinersExtRecord1.BankNetRefNumber = "DinersExtRecord1.BankNetRefNumber";
                    SLMResp.Batch.DinersExtRecord1.AuthorizedAmount = "DinersExtRecord1.AuthorizedAmount";
                    SLMResp.Batch.DinersExtRecord1.BankNetDate = "DinersExtRecord1.BankNetDate";
                    SLMResp.Batch.DinersExtRecord1.MerchantCategoryCode = "DinersExtRecord1.MerchantCategoryCode";
                    SLMResp.Batch.DinersExtRecord1.CATType = "DinersExtRecord1.CATType";
                }

                public DinersExtRecord1()
                {
                }
            }

            public class DirectPay1
            {
                public readonly static string BusinessApplicationIdentifier;

                public readonly static string ServiceFee;

                public readonly static string ForeignExchangeMarkupFee;

                public readonly static string SenderReferenceNumber;

                public readonly static string SourceOfFunds;

                public readonly static string RecipientName;

                static DirectPay1()
                {
                    SLMResp.Batch.DirectPay1.BusinessApplicationIdentifier = "DirectPay1.BusinessApplicationIdentifier";
                    SLMResp.Batch.DirectPay1.ServiceFee = "DirectPay1.ServiceFee";
                    SLMResp.Batch.DirectPay1.ForeignExchangeMarkupFee = "DirectPay1.ForeignExchangeMarkupFee";
                    SLMResp.Batch.DirectPay1.SenderReferenceNumber = "DirectPay1.SenderReferenceNumber";
                    SLMResp.Batch.DirectPay1.SourceOfFunds = "DirectPay1.SourceOfFunds";
                    SLMResp.Batch.DirectPay1.RecipientName = "DirectPay1.RecipientName";
                }

                public DirectPay1()
                {
                }
            }

            public class DirectPay2
            {
                public readonly static string SenderName;

                public readonly static string SenderAddress;

                public readonly static string SenderCity;

                public readonly static string SenderState;

                public readonly static string SenderZipCode;

                public readonly static string SenderCountryCode;

                static DirectPay2()
                {
                    SLMResp.Batch.DirectPay2.SenderName = "DirectPay2.SenderName";
                    SLMResp.Batch.DirectPay2.SenderAddress = "DirectPay2.SenderAddress";
                    SLMResp.Batch.DirectPay2.SenderCity = "DirectPay2.SenderCity";
                    SLMResp.Batch.DirectPay2.SenderState = "DirectPay2.SenderState";
                    SLMResp.Batch.DirectPay2.SenderZipCode = "DirectPay2.SenderZipCode";
                    SLMResp.Batch.DirectPay2.SenderCountryCode = "DirectPay2.SenderCountryCode";
                }

                public DirectPay2()
                {
                }
            }

            public class DiscoverBIN
            {
                public DiscoverBIN()
                {
                }

                public class DiscoverBINDetail
                {
                    public readonly static string RecordType;

                    public readonly static string BINLow;

                    public readonly static string BINHigh;

                    public readonly static string MaximumPANLength;

                    public readonly static string CountryCode;

                    public readonly static string CardProduct;

                    public readonly static string CardIndicator;

                    public readonly static string DefaultCardType;

                    public readonly static string BINUpdateDate;

                    static DiscoverBINDetail()
                    {
                        SLMResp.Batch.DiscoverBIN.DiscoverBINDetail.RecordType = "DiscoverBINDetail.RecordType";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINDetail.BINLow = "DiscoverBINDetail.BINLow";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINDetail.BINHigh = "DiscoverBINDetail.BINHigh";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINDetail.MaximumPANLength = "DiscoverBINDetail.MaximumPANLength";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINDetail.CountryCode = "DiscoverBINDetail.CountryCode";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINDetail.CardProduct = "DiscoverBINDetail.CardProduct";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINDetail.CardIndicator = "DiscoverBINDetail.CardIndicator";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINDetail.DefaultCardType = "DiscoverBINDetail.DefaultCardType";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINDetail.BINUpdateDate = "DiscoverBINDetail.BINUpdateDate";
                    }

                    public DiscoverBINDetail()
                    {
                    }
                }

                public class DiscoverBINHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string Version;

                    public readonly static string CreationDate;

                    static DiscoverBINHeader()
                    {
                        SLMResp.Batch.DiscoverBIN.DiscoverBINHeader.RecordType = "DiscoverBINHeader.RecordType";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINHeader.FileName = "DiscoverBINHeader.FileName";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINHeader.Version = "DiscoverBINHeader.Version";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINHeader.CreationDate = "DiscoverBINHeader.CreationDate";
                    }

                    public DiscoverBINHeader()
                    {
                    }
                }

                public class DiscoverBINTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDate;

                    public readonly static string NumberOfBinRecords;

                    static DiscoverBINTrailer()
                    {
                        SLMResp.Batch.DiscoverBIN.DiscoverBINTrailer.RecordType = "DiscoverBINTrailer.RecordType";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINTrailer.CreationDate = "DiscoverBINTrailer.CreationDate";
                        SLMResp.Batch.DiscoverBIN.DiscoverBINTrailer.NumberOfBinRecords = "DiscoverBINTrailer.NumberOfBinRecords";
                    }

                    public DiscoverBINTrailer()
                    {
                    }
                }
            }

            public class DiscoverDinersExtRecord1
            {
                public readonly static string SystemTraceAuditNumber;

                public readonly static string ProcessingCodeType;

                public readonly static string FromAccount;

                public readonly static string ToAccount;

                public readonly static string Track1DataConditionCode;

                public readonly static string Track2DataConditionCode;

                public readonly static string PANEntryMode;

                public readonly static string PINEntryMode;

                public readonly static string POSDeviceAttendanceIndicator;

                public readonly static string PartialApprovalIndicator;

                public readonly static string POSDeviceLocationIndicator;

                public readonly static string POSCardholderPresenceIndicator;

                public readonly static string POSCardPresenceIndicator;

                public readonly static string POSCardCaptureCapabilityIndicator;

                public readonly static string POSTransactionStatusIndicator;

                public readonly static string POSTransactionSecurityIndicator;

                public readonly static string POSTypeOfTerminalDevice;

                public readonly static string POSDeviceCardDataInputCapabilityIndicator;

                public readonly static string CashOverAmount;

                public readonly static string LocalTransactionDate;

                public readonly static string LocalTransactionTime;

                public readonly static string NetworkReferenceID;

                static DiscoverDinersExtRecord1()
                {
                    SLMResp.Batch.DiscoverDinersExtRecord1.SystemTraceAuditNumber = "DiscoverDinersExtRecord1.SystemTraceAuditNumber";
                    SLMResp.Batch.DiscoverDinersExtRecord1.ProcessingCodeType = "DiscoverDinersExtRecord1.ProcessingCodeType";
                    SLMResp.Batch.DiscoverDinersExtRecord1.FromAccount = "DiscoverDinersExtRecord1.FromAccount";
                    SLMResp.Batch.DiscoverDinersExtRecord1.ToAccount = "DiscoverDinersExtRecord1.ToAccount";
                    SLMResp.Batch.DiscoverDinersExtRecord1.Track1DataConditionCode = "DiscoverDinersExtRecord1.Track1DataConditionCode";
                    SLMResp.Batch.DiscoverDinersExtRecord1.Track2DataConditionCode = "DiscoverDinersExtRecord1.Track2DataConditionCode";
                    SLMResp.Batch.DiscoverDinersExtRecord1.PANEntryMode = "DiscoverDinersExtRecord1.PANEntryMode";
                    SLMResp.Batch.DiscoverDinersExtRecord1.PINEntryMode = "DiscoverDinersExtRecord1.PINEntryMode";
                    SLMResp.Batch.DiscoverDinersExtRecord1.POSDeviceAttendanceIndicator = "DiscoverDinersExtRecord1.POSDeviceAttendanceIndicator";
                    SLMResp.Batch.DiscoverDinersExtRecord1.PartialApprovalIndicator = "DiscoverDinersExtRecord1.PartialApprovalIndicator";
                    SLMResp.Batch.DiscoverDinersExtRecord1.POSDeviceLocationIndicator = "DiscoverDinersExtRecord1.POSDeviceLocationIndicator";
                    SLMResp.Batch.DiscoverDinersExtRecord1.POSCardholderPresenceIndicator = "DiscoverDinersExtRecord1.POSCardholderPresenceIndicator";
                    SLMResp.Batch.DiscoverDinersExtRecord1.POSCardPresenceIndicator = "DiscoverDinersExtRecord1.POSCardPresenceIndicator";
                    SLMResp.Batch.DiscoverDinersExtRecord1.POSCardCaptureCapabilityIndicator = "DiscoverDinersExtRecord1.POSCardCaptureCapabilityIndicator";
                    SLMResp.Batch.DiscoverDinersExtRecord1.POSTransactionStatusIndicator = "DiscoverDinersExtRecord1.POSTransactionStatusIndicator";
                    SLMResp.Batch.DiscoverDinersExtRecord1.POSTransactionSecurityIndicator = "DiscoverDinersExtRecord1.POSTransactionSecurityIndicator";
                    SLMResp.Batch.DiscoverDinersExtRecord1.POSTypeOfTerminalDevice = "DiscoverDinersExtRecord1.POSTypeOfTerminalDevice";
                    SLMResp.Batch.DiscoverDinersExtRecord1.POSDeviceCardDataInputCapabilityIndicator = "DiscoverDinersExtRecord1.POSDeviceCardDataInputCapabilityIndicator";
                    SLMResp.Batch.DiscoverDinersExtRecord1.CashOverAmount = "DiscoverDinersExtRecord1.CashOverAmount";
                    SLMResp.Batch.DiscoverDinersExtRecord1.LocalTransactionDate = "DiscoverDinersExtRecord1.LocalTransactionDate";
                    SLMResp.Batch.DiscoverDinersExtRecord1.LocalTransactionTime = "DiscoverDinersExtRecord1.LocalTransactionTime";
                    SLMResp.Batch.DiscoverDinersExtRecord1.NetworkReferenceID = "DiscoverDinersExtRecord1.NetworkReferenceID";
                }

                public DiscoverDinersExtRecord1()
                {
                }
            }

            public class DiscoverExtRecord1
            {
                public readonly static string SystemTraceAuditNumber;

                public readonly static string ProcessingCodeType;

                public readonly static string FromAccount;

                public readonly static string ToAccount;

                public readonly static string TransactionDataConditionCode;

                public readonly static string PANEntryMode;

                public readonly static string PINEntryMode;

                public readonly static string POSDeviceAttendanceIndicator;

                public readonly static string PartialApprovalIndicator;

                public readonly static string POSDeviceLocationIndicator;

                public readonly static string POSCardholderPresenceIndicator;

                public readonly static string POSCardPresenceIndicator;

                public readonly static string POSCardCaptureCapabilityIndicator;

                public readonly static string POSTransactionStatusIndicator;

                public readonly static string POSTransactionSecurityIndicator;

                public readonly static string POSTypeOfTerminalDevice;

                public readonly static string POSDeviceCardDataInputCapabilityIndicator;

                public readonly static string CashOverAmount;

                public readonly static string LocalTransactionDate;

                public readonly static string LocalTransactionTime;

                public readonly static string NetworkReferenceID;

                static DiscoverExtRecord1()
                {
                    SLMResp.Batch.DiscoverExtRecord1.SystemTraceAuditNumber = "DiscoverExtRecord1.SystemTraceAuditNumber";
                    SLMResp.Batch.DiscoverExtRecord1.ProcessingCodeType = "DiscoverExtRecord1.ProcessingCodeType";
                    SLMResp.Batch.DiscoverExtRecord1.FromAccount = "DiscoverExtRecord1.FromAccount";
                    SLMResp.Batch.DiscoverExtRecord1.ToAccount = "DiscoverExtRecord1.ToAccount";
                    SLMResp.Batch.DiscoverExtRecord1.TransactionDataConditionCode = "DiscoverExtRecord1.TransactionDataConditionCode";
                    SLMResp.Batch.DiscoverExtRecord1.PANEntryMode = "DiscoverExtRecord1.PANEntryMode";
                    SLMResp.Batch.DiscoverExtRecord1.PINEntryMode = "DiscoverExtRecord1.PINEntryMode";
                    SLMResp.Batch.DiscoverExtRecord1.POSDeviceAttendanceIndicator = "DiscoverExtRecord1.POSDeviceAttendanceIndicator";
                    SLMResp.Batch.DiscoverExtRecord1.PartialApprovalIndicator = "DiscoverExtRecord1.PartialApprovalIndicator";
                    SLMResp.Batch.DiscoverExtRecord1.POSDeviceLocationIndicator = "DiscoverExtRecord1.POSDeviceLocationIndicator";
                    SLMResp.Batch.DiscoverExtRecord1.POSCardholderPresenceIndicator = "DiscoverExtRecord1.POSCardholderPresenceIndicator";
                    SLMResp.Batch.DiscoverExtRecord1.POSCardPresenceIndicator = "DiscoverExtRecord1.POSCardPresenceIndicator";
                    SLMResp.Batch.DiscoverExtRecord1.POSCardCaptureCapabilityIndicator = "DiscoverExtRecord1.POSCardCaptureCapabilityIndicator";
                    SLMResp.Batch.DiscoverExtRecord1.POSTransactionStatusIndicator = "DiscoverExtRecord1.POSTransactionStatusIndicator";
                    SLMResp.Batch.DiscoverExtRecord1.POSTransactionSecurityIndicator = "DiscoverExtRecord1.POSTransactionSecurityIndicator";
                    SLMResp.Batch.DiscoverExtRecord1.POSTypeOfTerminalDevice = "DiscoverExtRecord1.POSTypeOfTerminalDevice";
                    SLMResp.Batch.DiscoverExtRecord1.POSDeviceCardDataInputCapabilityIndicator = "DiscoverExtRecord1.POSDeviceCardDataInputCapabilityIndicator";
                    SLMResp.Batch.DiscoverExtRecord1.CashOverAmount = "DiscoverExtRecord1.CashOverAmount";
                    SLMResp.Batch.DiscoverExtRecord1.LocalTransactionDate = "DiscoverExtRecord1.LocalTransactionDate";
                    SLMResp.Batch.DiscoverExtRecord1.LocalTransactionTime = "DiscoverExtRecord1.LocalTransactionTime";
                    SLMResp.Batch.DiscoverExtRecord1.NetworkReferenceID = "DiscoverExtRecord1.NetworkReferenceID";
                }

                public DiscoverExtRecord1()
                {
                }
            }

            public class DiscoverLineItemRecord1
            {
                public readonly static string SequenceNumber;

                public readonly static string TaxAmount;

                public readonly static string ItemCommodityCode;

                public readonly static string ProductCode;

                public readonly static string Description;

                public readonly static string DiscountRate;

                static DiscoverLineItemRecord1()
                {
                    SLMResp.Batch.DiscoverLineItemRecord1.SequenceNumber = "DiscoverLineItemRecord1.SequenceNumber";
                    SLMResp.Batch.DiscoverLineItemRecord1.TaxAmount = "DiscoverLineItemRecord1.TaxAmount";
                    SLMResp.Batch.DiscoverLineItemRecord1.ItemCommodityCode = "DiscoverLineItemRecord1.ItemCommodityCode";
                    SLMResp.Batch.DiscoverLineItemRecord1.ProductCode = "DiscoverLineItemRecord1.ProductCode";
                    SLMResp.Batch.DiscoverLineItemRecord1.Description = "DiscoverLineItemRecord1.Description";
                    SLMResp.Batch.DiscoverLineItemRecord1.DiscountRate = "DiscoverLineItemRecord1.DiscountRate";
                }

                public DiscoverLineItemRecord1()
                {
                }
            }

            public class DiscoverLineItemRecord2
            {
                public readonly static string SequenceNumber;

                public readonly static string UnitOfMeasure;

                public readonly static string UnitCost;

                public readonly static string Quantity;

                public readonly static string TaxRate;

                public readonly static string LineItemTotal;

                static DiscoverLineItemRecord2()
                {
                    SLMResp.Batch.DiscoverLineItemRecord2.SequenceNumber = "DiscoverLineItemRecord2.SequenceNumber";
                    SLMResp.Batch.DiscoverLineItemRecord2.UnitOfMeasure = "DiscoverLineItemRecord2.UnitOfMeasure";
                    SLMResp.Batch.DiscoverLineItemRecord2.UnitCost = "DiscoverLineItemRecord2.UnitCost";
                    SLMResp.Batch.DiscoverLineItemRecord2.Quantity = "DiscoverLineItemRecord2.Quantity";
                    SLMResp.Batch.DiscoverLineItemRecord2.TaxRate = "DiscoverLineItemRecord2.TaxRate";
                    SLMResp.Batch.DiscoverLineItemRecord2.LineItemTotal = "DiscoverLineItemRecord2.LineItemTotal";
                }

                public DiscoverLineItemRecord2()
                {
                }
            }

            public class ECPAdvancedVerification1
            {
                public readonly static string RecordType;

                public readonly static string FirstName;

                public readonly static string LastName;

                static ECPAdvancedVerification1()
                {
                    SLMResp.Batch.ECPAdvancedVerification1.RecordType = "ECPAdvancedVerification1.RecordType";
                    SLMResp.Batch.ECPAdvancedVerification1.FirstName = "ECPAdvancedVerification1.FirstName";
                    SLMResp.Batch.ECPAdvancedVerification1.LastName = "ECPAdvancedVerification1.LastName";
                }

                public ECPAdvancedVerification1()
                {
                }
            }

            public class ECPAdvancedVerification2
            {
                public readonly static string RecordType;

                public readonly static string BusinessName;

                static ECPAdvancedVerification2()
                {
                    SLMResp.Batch.ECPAdvancedVerification2.RecordType = "ECPAdvancedVerification2.RecordType";
                    SLMResp.Batch.ECPAdvancedVerification2.BusinessName = "ECPAdvancedVerification2.BusinessName";
                }

                public ECPAdvancedVerification2()
                {
                }
            }

            public class ECPAdvancedVerification3
            {
                public readonly static string RecordType;

                public readonly static string AddressLine1;

                public readonly static string AddressLine2;

                static ECPAdvancedVerification3()
                {
                    SLMResp.Batch.ECPAdvancedVerification3.RecordType = "ECPAdvancedVerification3.RecordType";
                    SLMResp.Batch.ECPAdvancedVerification3.AddressLine1 = "ECPAdvancedVerification3.AddressLine1";
                    SLMResp.Batch.ECPAdvancedVerification3.AddressLine2 = "ECPAdvancedVerification3.AddressLine2";
                }

                public ECPAdvancedVerification3()
                {
                }
            }

            public class ECPAdvancedVerification4
            {
                public readonly static string RecordType;

                public readonly static string City;

                public readonly static string State;

                public readonly static string ZIP;

                public readonly static string PhoneType;

                public readonly static string Phone;

                public readonly static string MiddleNameOrInitial;

                static ECPAdvancedVerification4()
                {
                    SLMResp.Batch.ECPAdvancedVerification4.RecordType = "ECPAdvancedVerification4.RecordType";
                    SLMResp.Batch.ECPAdvancedVerification4.City = "ECPAdvancedVerification4.City";
                    SLMResp.Batch.ECPAdvancedVerification4.State = "ECPAdvancedVerification4.State";
                    SLMResp.Batch.ECPAdvancedVerification4.ZIP = "ECPAdvancedVerification4.ZIP";
                    SLMResp.Batch.ECPAdvancedVerification4.PhoneType = "ECPAdvancedVerification4.PhoneType";
                    SLMResp.Batch.ECPAdvancedVerification4.Phone = "ECPAdvancedVerification4.Phone";
                    SLMResp.Batch.ECPAdvancedVerification4.MiddleNameOrInitial = "ECPAdvancedVerification4.MiddleNameOrInitial";
                }

                public ECPAdvancedVerification4()
                {
                }
            }

            public class ECPAdvancedVerification5
            {
                public readonly static string RecordType;

                public readonly static string TaxIdentificationNumber;

                public readonly static string DOB;

                public readonly static string IDType;

                public readonly static string IDNumber;

                public readonly static string IDState;

                static ECPAdvancedVerification5()
                {
                    SLMResp.Batch.ECPAdvancedVerification5.RecordType = "ECPAdvancedVerification5.RecordType";
                    SLMResp.Batch.ECPAdvancedVerification5.TaxIdentificationNumber = "ECPAdvancedVerification5.TaxIdentificationNumber";
                    SLMResp.Batch.ECPAdvancedVerification5.DOB = "ECPAdvancedVerification5.DOB";
                    SLMResp.Batch.ECPAdvancedVerification5.IDType = "ECPAdvancedVerification5.IDType";
                    SLMResp.Batch.ECPAdvancedVerification5.IDNumber = "ECPAdvancedVerification5.IDNumber";
                    SLMResp.Batch.ECPAdvancedVerification5.IDState = "ECPAdvancedVerification5.IDState";
                }

                public ECPAdvancedVerification5()
                {
                }
            }

            public class ECPAdvancedVerificationResponse
            {
                public readonly static string RecordType;

                public readonly static string AccountStatusCode;

                public readonly static string AOAConditionCode;

                public readonly static string FullNameMatch;

                public readonly static string FirstNameMatch;

                public readonly static string LastNameMatch;

                public readonly static string MiddleNameOrInitialMatch;

                public readonly static string BusinessNameMatch;

                public readonly static string Address1Address2Match;

                public readonly static string CityMatch;

                public readonly static string StateMatch;

                public readonly static string ZipCodeMatch;

                public readonly static string PhoneMatch;

                public readonly static string TaxIdentificationNumberMatch;

                public readonly static string DOBMatch;

                public readonly static string IDTypeMatch;

                public readonly static string IDNumberMatch;

                public readonly static string IDStateMatch;

                static ECPAdvancedVerificationResponse()
                {
                    SLMResp.Batch.ECPAdvancedVerificationResponse.RecordType = "ECPAdvancedVerificationResponse.RecordType";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.AccountStatusCode = "ECPAdvancedVerificationResponse.AccountStatusCode";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.AOAConditionCode = "ECPAdvancedVerificationResponse.AOAConditionCode";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.FullNameMatch = "ECPAdvancedVerificationResponse.FullNameMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.FirstNameMatch = "ECPAdvancedVerificationResponse.FirstNameMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.LastNameMatch = "ECPAdvancedVerificationResponse.LastNameMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.MiddleNameOrInitialMatch = "ECPAdvancedVerificationResponse.MiddleNameOrInitialMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.BusinessNameMatch = "ECPAdvancedVerificationResponse.BusinessNameMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.Address1Address2Match = "ECPAdvancedVerificationResponse.Address1Address2Match";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.CityMatch = "ECPAdvancedVerificationResponse.CityMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.StateMatch = "ECPAdvancedVerificationResponse.StateMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.ZipCodeMatch = "ECPAdvancedVerificationResponse.ZipCodeMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.PhoneMatch = "ECPAdvancedVerificationResponse.PhoneMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.TaxIdentificationNumberMatch = "ECPAdvancedVerificationResponse.TaxIdentificationNumberMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.DOBMatch = "ECPAdvancedVerificationResponse.DOBMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.IDTypeMatch = "ECPAdvancedVerificationResponse.IDTypeMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.IDNumberMatch = "ECPAdvancedVerificationResponse.IDNumberMatch";
                    SLMResp.Batch.ECPAdvancedVerificationResponse.IDStateMatch = "ECPAdvancedVerificationResponse.IDStateMatch";
                }

                public ECPAdvancedVerificationResponse()
                {
                }
            }

            public class ECPExtRecord1
            {
                public readonly static string RDFIBankID;

                public readonly static string AccountType;

                public readonly static string PrefDeliveryMethod;

                public readonly static string ECPAuthMethod;

                public readonly static string CheckSerialNumber;

                public readonly static string TerminalCity;

                public readonly static string TerminalState;

                public readonly static string ImageRefNumber;

                public readonly static string ReDepositFrequency;

                public readonly static string ReDepositOption;

                public readonly static string ECPSameDayIndicator;

                public readonly static string ECPSameDayACHResponseCode;

                static ECPExtRecord1()
                {
                    SLMResp.Batch.ECPExtRecord1.RDFIBankID = "ECPExtRecord1.RDFIBankID";
                    SLMResp.Batch.ECPExtRecord1.AccountType = "ECPExtRecord1.AccountType";
                    SLMResp.Batch.ECPExtRecord1.PrefDeliveryMethod = "ECPExtRecord1.PrefDeliveryMethod";
                    SLMResp.Batch.ECPExtRecord1.ECPAuthMethod = "ECPExtRecord1.ECPAuthMethod";
                    SLMResp.Batch.ECPExtRecord1.CheckSerialNumber = "ECPExtRecord1.CheckSerialNumber";
                    SLMResp.Batch.ECPExtRecord1.TerminalCity = "ECPExtRecord1.TerminalCity";
                    SLMResp.Batch.ECPExtRecord1.TerminalState = "ECPExtRecord1.TerminalState";
                    SLMResp.Batch.ECPExtRecord1.ImageRefNumber = "ECPExtRecord1.ImageRefNumber";
                    SLMResp.Batch.ECPExtRecord1.ReDepositFrequency = "ECPExtRecord1.ReDepositFrequency";
                    SLMResp.Batch.ECPExtRecord1.ReDepositOption = "ECPExtRecord1.ReDepositOption";
                    SLMResp.Batch.ECPExtRecord1.ECPSameDayIndicator = "ECPExtRecord1.ECPSameDayIndicator";
                    SLMResp.Batch.ECPExtRecord1.ECPSameDayACHResponseCode = "ECPExtRecord1.ECPSameDayACHResponseCode";
                }

                public ECPExtRecord1()
                {
                }
            }

            public class ElectronicPayAccountLevel2
            {
                public readonly static string CustomIdentifier;

                public readonly static string InvoiceNumber;

                static ElectronicPayAccountLevel2()
                {
                    SLMResp.Batch.ElectronicPayAccountLevel2.CustomIdentifier = "ElectronicPayAccountLevel2.CustomIdentifier";
                    SLMResp.Batch.ElectronicPayAccountLevel2.InvoiceNumber = "ElectronicPayAccountLevel2.InvoiceNumber";
                }

                public ElectronicPayAccountLevel2()
                {
                }
            }

            public class EmployerAddress
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static EmployerAddress()
                {
                    SLMResp.Batch.EmployerAddress.AddressLine = "EmployerAddress.AddressLine";
                    SLMResp.Batch.EmployerAddress.TelephoneType = "EmployerAddress.TelephoneType";
                    SLMResp.Batch.EmployerAddress.TelephoneNumber = "EmployerAddress.TelephoneNumber";
                    SLMResp.Batch.EmployerAddress.CountryCode = "EmployerAddress.CountryCode";
                }

                public EmployerAddress()
                {
                }
            }

            public class EMVRecord1
            {
                public readonly static string CardSequenceNumber;

                public readonly static string Cryptogram;

                public readonly static string CryptogramAmount;

                public readonly static string OtherAmount;

                public readonly static string CryptogramInformationData;

                public readonly static string ApplicationTransactionCounter;

                public readonly static string TransactionType;

                public readonly static string TerminalCountryCode;

                public readonly static string TerminalCurrencyCode;

                public readonly static string TerminalTransactionDate;

                public readonly static string TerminalTransactionTime;

                public readonly static string TerminalCapabilityProfile;

                public readonly static string TerminalType;

                public readonly static string TerminalVerificationResults;

                public readonly static string UnpredictableNumber;

                public readonly static string FormFactorID;

                public readonly static string IssuerScriptID;

                public readonly static string ApplicationInterchangeProfile;

                static EMVRecord1()
                {
                    SLMResp.Batch.EMVRecord1.CardSequenceNumber = "EMVRecord1.CardSequenceNumber";
                    SLMResp.Batch.EMVRecord1.Cryptogram = "EMVRecord1.Cryptogram";
                    SLMResp.Batch.EMVRecord1.CryptogramAmount = "EMVRecord1.CryptogramAmount";
                    SLMResp.Batch.EMVRecord1.OtherAmount = "EMVRecord1.OtherAmount";
                    SLMResp.Batch.EMVRecord1.CryptogramInformationData = "EMVRecord1.CryptogramInformationData";
                    SLMResp.Batch.EMVRecord1.ApplicationTransactionCounter = "EMVRecord1.ApplicationTransactionCounter";
                    SLMResp.Batch.EMVRecord1.TransactionType = "EMVRecord1.TransactionType";
                    SLMResp.Batch.EMVRecord1.TerminalCountryCode = "EMVRecord1.TerminalCountryCode";
                    SLMResp.Batch.EMVRecord1.TerminalCurrencyCode = "EMVRecord1.TerminalCurrencyCode";
                    SLMResp.Batch.EMVRecord1.TerminalTransactionDate = "EMVRecord1.TerminalTransactionDate";
                    SLMResp.Batch.EMVRecord1.TerminalTransactionTime = "EMVRecord1.TerminalTransactionTime";
                    SLMResp.Batch.EMVRecord1.TerminalCapabilityProfile = "EMVRecord1.TerminalCapabilityProfile";
                    SLMResp.Batch.EMVRecord1.TerminalType = "EMVRecord1.TerminalType";
                    SLMResp.Batch.EMVRecord1.TerminalVerificationResults = "EMVRecord1.TerminalVerificationResults";
                    SLMResp.Batch.EMVRecord1.UnpredictableNumber = "EMVRecord1.UnpredictableNumber";
                    SLMResp.Batch.EMVRecord1.FormFactorID = "EMVRecord1.FormFactorID";
                    SLMResp.Batch.EMVRecord1.IssuerScriptID = "EMVRecord1.IssuerScriptID";
                    SLMResp.Batch.EMVRecord1.ApplicationInterchangeProfile = "EMVRecord1.ApplicationInterchangeProfile";
                }

                public EMVRecord1()
                {
                }
            }

            public class EMVRecord2
            {
                public readonly static string AuthorizationResponseCode;

                public readonly static string IssuerApplicationData;

                public readonly static string IssuerScriptResults;

                static EMVRecord2()
                {
                    SLMResp.Batch.EMVRecord2.AuthorizationResponseCode = "EMVRecord2.AuthorizationResponseCode";
                    SLMResp.Batch.EMVRecord2.IssuerApplicationData = "EMVRecord2.IssuerApplicationData";
                    SLMResp.Batch.EMVRecord2.IssuerScriptResults = "EMVRecord2.IssuerScriptResults";
                }

                public EMVRecord2()
                {
                }
            }

            public class EMVRecord3
            {
                public readonly static string InterfaceDeviceSerialNumber;

                public readonly static string CardAuthenticationResultsCode;

                public readonly static string CVVICVVCamResultsCode;

                public readonly static string CVMResults;

                public readonly static string IssuerScriptCommandData;

                public readonly static string ApplicationIdentifier;

                public readonly static string TerminalTransactionQualifier;

                public readonly static string IssuerScriptTemplate1Indicator;

                public readonly static string IssuerScriptTemplate2Indicator;

                public readonly static string ChipConditionCode;

                static EMVRecord3()
                {
                    SLMResp.Batch.EMVRecord3.InterfaceDeviceSerialNumber = "EMVRecord3.InterfaceDeviceSerialNumber";
                    SLMResp.Batch.EMVRecord3.CardAuthenticationResultsCode = "EMVRecord3.CardAuthenticationResultsCode";
                    SLMResp.Batch.EMVRecord3.CVVICVVCamResultsCode = "EMVRecord3.CVVICVVCamResultsCode";
                    SLMResp.Batch.EMVRecord3.CVMResults = "EMVRecord3.CVMResults";
                    SLMResp.Batch.EMVRecord3.IssuerScriptCommandData = "EMVRecord3.IssuerScriptCommandData";
                    SLMResp.Batch.EMVRecord3.ApplicationIdentifier = "EMVRecord3.ApplicationIdentifier";
                    SLMResp.Batch.EMVRecord3.TerminalTransactionQualifier = "EMVRecord3.TerminalTransactionQualifier";
                    SLMResp.Batch.EMVRecord3.IssuerScriptTemplate1Indicator = "EMVRecord3.IssuerScriptTemplate1Indicator";
                    SLMResp.Batch.EMVRecord3.IssuerScriptTemplate2Indicator = "EMVRecord3.IssuerScriptTemplate2Indicator";
                    SLMResp.Batch.EMVRecord3.ChipConditionCode = "EMVRecord3.ChipConditionCode";
                }

                public EMVRecord3()
                {
                }
            }

            public class EMVRecord4
            {
                public readonly static string TransactionSequenceCounter;

                public readonly static string CustomerExclusiveData;

                public readonly static string IssuerAuthenticationData;

                static EMVRecord4()
                {
                    SLMResp.Batch.EMVRecord4.TransactionSequenceCounter = "EMVRecord4.TransactionSequenceCounter";
                    SLMResp.Batch.EMVRecord4.CustomerExclusiveData = "EMVRecord4.CustomerExclusiveData";
                    SLMResp.Batch.EMVRecord4.IssuerAuthenticationData = "EMVRecord4.IssuerAuthenticationData";
                }

                public EMVRecord4()
                {
                }
            }

            public class EntertainmentTicketing1
            {
                public readonly static string EventName;

                public readonly static string EventDate;

                public readonly static string IndividualTicketPrice;

                public readonly static string Quantity;

                static EntertainmentTicketing1()
                {
                    SLMResp.Batch.EntertainmentTicketing1.EventName = "EntertainmentTicketing1.EventName";
                    SLMResp.Batch.EntertainmentTicketing1.EventDate = "EntertainmentTicketing1.EventDate";
                    SLMResp.Batch.EntertainmentTicketing1.IndividualTicketPrice = "EntertainmentTicketing1.IndividualTicketPrice";
                    SLMResp.Batch.EntertainmentTicketing1.Quantity = "EntertainmentTicketing1.Quantity";
                }

                public EntertainmentTicketing1()
                {
                }
            }

            public class EntertainmentTicketing2
            {
                public readonly static string Location;

                public readonly static string Region;

                public readonly static string Country;

                static EntertainmentTicketing2()
                {
                    SLMResp.Batch.EntertainmentTicketing2.Location = "EntertainmentTicketing2.Location";
                    SLMResp.Batch.EntertainmentTicketing2.Region = "EntertainmentTicketing2.Region";
                    SLMResp.Batch.EntertainmentTicketing2.Country = "EntertainmentTicketing2.Country";
                }

                public EntertainmentTicketing2()
                {
                }
            }

            public class EUDDExtRecord1
            {
                public readonly static string CountryCode;

                public readonly static string BankSortCode;

                public readonly static string RIBCode;

                public readonly static string BankBranchCode;

                public readonly static string IBAN;

                public readonly static string BIC;

                public readonly static string MandateType;

                public readonly static string MandateID;

                public readonly static string SignatureDate;

                static EUDDExtRecord1()
                {
                    SLMResp.Batch.EUDDExtRecord1.CountryCode = "EUDDExtRecord1.CountryCode";
                    SLMResp.Batch.EUDDExtRecord1.BankSortCode = "EUDDExtRecord1.BankSortCode";
                    SLMResp.Batch.EUDDExtRecord1.RIBCode = "EUDDExtRecord1.RIBCode";
                    SLMResp.Batch.EUDDExtRecord1.BankBranchCode = "EUDDExtRecord1.BankBranchCode";
                    SLMResp.Batch.EUDDExtRecord1.IBAN = "EUDDExtRecord1.IBAN";
                    SLMResp.Batch.EUDDExtRecord1.BIC = "EUDDExtRecord1.BIC";
                    SLMResp.Batch.EUDDExtRecord1.MandateType = "EUDDExtRecord1.MandateType";
                    SLMResp.Batch.EUDDExtRecord1.MandateID = "EUDDExtRecord1.MandateID";
                    SLMResp.Batch.EUDDExtRecord1.SignatureDate = "EUDDExtRecord1.SignatureDate";
                }

                public EUDDExtRecord1()
                {
                }
            }

            public class EUMCAccountUpdater
            {
                public EUMCAccountUpdater()
                {
                }

                public class AccountUpdaterDetail
                {
                    public readonly static string RecordType;

                    public readonly static string TransactionDivisionNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string OldAccountNumber;

                    public readonly static string OldExpirationDate;

                    public readonly static string NewAccountNumber;

                    public readonly static string NewExpirationDate;

                    public readonly static string UpdateResponse;

                    public readonly static string PreviouslySentFlag;

                    public readonly static string TokenIndicator;

                    static AccountUpdaterDetail()
                    {
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.RecordType = "AccountUpdaterDetail.RecordType";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.TransactionDivisionNumber = "AccountUpdaterDetail.TransactionDivisionNumber";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.MerchantOrderNumber = "AccountUpdaterDetail.MerchantOrderNumber";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.OldAccountNumber = "AccountUpdaterDetail.OldAccountNumber";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.OldExpirationDate = "AccountUpdaterDetail.OldExpirationDate";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.NewAccountNumber = "AccountUpdaterDetail.NewAccountNumber";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.NewExpirationDate = "AccountUpdaterDetail.NewExpirationDate";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.UpdateResponse = "AccountUpdaterDetail.UpdateResponse";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.PreviouslySentFlag = "AccountUpdaterDetail.PreviouslySentFlag";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterDetail.TokenIndicator = "AccountUpdaterDetail.TokenIndicator";
                    }

                    public AccountUpdaterDetail()
                    {
                    }
                }

                public class AccountUpdaterHeader
                {
                    public readonly static string RecordType;

                    public readonly static string EntityLevel;

                    public readonly static string FileDate;

                    public readonly static string FileVersion;

                    static AccountUpdaterHeader()
                    {
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterHeader.RecordType = "AccountUpdaterHeader.RecordType";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterHeader.EntityLevel = "AccountUpdaterHeader.EntityLevel";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterHeader.FileDate = "AccountUpdaterHeader.FileDate";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterHeader.FileVersion = "AccountUpdaterHeader.FileVersion";
                    }

                    public AccountUpdaterHeader()
                    {
                    }
                }

                public class AccountUpdaterTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string FileRecordCount;

                    static AccountUpdaterTrailer()
                    {
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterTrailer.RecordType = "AccountUpdaterTrailer.RecordType";
                        SLMResp.Batch.EUMCAccountUpdater.AccountUpdaterTrailer.FileRecordCount = "AccountUpdaterTrailer.FileRecordCount";
                    }

                    public AccountUpdaterTrailer()
                    {
                    }
                }
            }

            public class EUVIAccountUpdater
            {
                public EUVIAccountUpdater()
                {
                }

                public class AccountUpdaterDetail
                {
                    public readonly static string RecordType;

                    public readonly static string TransactionDivisionNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string OldAccountNumber;

                    public readonly static string OldExpirationDate;

                    public readonly static string NewAccountNumber;

                    public readonly static string NewExpirationDate;

                    public readonly static string UpdateResponse;

                    public readonly static string PreviouslySentFlag;

                    public readonly static string TokenIndicator;

                    static AccountUpdaterDetail()
                    {
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.RecordType = "AccountUpdaterDetail.RecordType";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.TransactionDivisionNumber = "AccountUpdaterDetail.TransactionDivisionNumber";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.MerchantOrderNumber = "AccountUpdaterDetail.MerchantOrderNumber";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.OldAccountNumber = "AccountUpdaterDetail.OldAccountNumber";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.OldExpirationDate = "AccountUpdaterDetail.OldExpirationDate";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.NewAccountNumber = "AccountUpdaterDetail.NewAccountNumber";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.NewExpirationDate = "AccountUpdaterDetail.NewExpirationDate";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.UpdateResponse = "AccountUpdaterDetail.UpdateResponse";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.PreviouslySentFlag = "AccountUpdaterDetail.PreviouslySentFlag";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterDetail.TokenIndicator = "AccountUpdaterDetail.TokenIndicator";
                    }

                    public AccountUpdaterDetail()
                    {
                    }
                }

                public class AccountUpdaterHeader
                {
                    public readonly static string RecordType;

                    public readonly static string EntityLevel;

                    public readonly static string FileDate;

                    public readonly static string FileVersion;

                    static AccountUpdaterHeader()
                    {
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterHeader.RecordType = "AccountUpdaterHeader.RecordType";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterHeader.EntityLevel = "AccountUpdaterHeader.EntityLevel";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterHeader.FileDate = "AccountUpdaterHeader.FileDate";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterHeader.FileVersion = "AccountUpdaterHeader.FileVersion";
                    }

                    public AccountUpdaterHeader()
                    {
                    }
                }

                public class AccountUpdaterTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string FileRecordCount;

                    static AccountUpdaterTrailer()
                    {
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterTrailer.RecordType = "AccountUpdaterTrailer.RecordType";
                        SLMResp.Batch.EUVIAccountUpdater.AccountUpdaterTrailer.FileRecordCount = "AccountUpdaterTrailer.FileRecordCount";
                    }

                    public AccountUpdaterTrailer()
                    {
                    }
                }
            }

            public class ExtensionRecordDiscoverAuthentication
            {
                public readonly static string CAVV;

                public readonly static string CAVVResponseCode;

                public readonly static string AuthenticationType;

                static ExtensionRecordDiscoverAuthentication()
                {
                    SLMResp.Batch.ExtensionRecordDiscoverAuthentication.CAVV = "ExtensionRecordDiscoverAuthentication.CAVV";
                    SLMResp.Batch.ExtensionRecordDiscoverAuthentication.CAVVResponseCode = "ExtensionRecordDiscoverAuthentication.CAVVResponseCode";
                    SLMResp.Batch.ExtensionRecordDiscoverAuthentication.AuthenticationType = "ExtensionRecordDiscoverAuthentication.AuthenticationType";
                }

                public ExtensionRecordDiscoverAuthentication()
                {
                }
            }

            public class ExtensionRecordDiscoverDiners002
            {
                public readonly static string CAVV;

                public readonly static string CAVVResponseCode;

                public readonly static string AuthenticationType;

                static ExtensionRecordDiscoverDiners002()
                {
                    SLMResp.Batch.ExtensionRecordDiscoverDiners002.CAVV = "ExtensionRecordDiscoverDiners002.CAVV";
                    SLMResp.Batch.ExtensionRecordDiscoverDiners002.CAVVResponseCode = "ExtensionRecordDiscoverDiners002.CAVVResponseCode";
                    SLMResp.Batch.ExtensionRecordDiscoverDiners002.AuthenticationType = "ExtensionRecordDiscoverDiners002.AuthenticationType";
                }

                public ExtensionRecordDiscoverDiners002()
                {
                }
            }

            public class ExtensionRecordGenericPrivateLabel
            {
                public readonly static string POSEntryMode;

                public readonly static string MerchantCategoryCode;

                static ExtensionRecordGenericPrivateLabel()
                {
                    SLMResp.Batch.ExtensionRecordGenericPrivateLabel.POSEntryMode = "ExtensionRecordGenericPrivateLabel.POSEntryMode";
                    SLMResp.Batch.ExtensionRecordGenericPrivateLabel.MerchantCategoryCode = "ExtensionRecordGenericPrivateLabel.MerchantCategoryCode";
                }

                public ExtensionRecordGenericPrivateLabel()
                {
                }
            }

            public class ExtensionRecordStoredValue
            {
                public readonly static string POSEntryMode;

                public readonly static string MerchantCategoryCode;

                static ExtensionRecordStoredValue()
                {
                    SLMResp.Batch.ExtensionRecordStoredValue.POSEntryMode = "ExtensionRecordStoredValue.POSEntryMode";
                    SLMResp.Batch.ExtensionRecordStoredValue.MerchantCategoryCode = "ExtensionRecordStoredValue.MerchantCategoryCode";
                }

                public ExtensionRecordStoredValue()
                {
                }
            }

            public class FleetRecord1
            {
                public readonly static string FleetProdTypeCode;

                public readonly static string Odometer;

                public readonly static string IDNumber;

                public readonly static string VehicleNumber;

                public readonly static string DriverIDNumber;

                static FleetRecord1()
                {
                    SLMResp.Batch.FleetRecord1.FleetProdTypeCode = "FleetRecord1.FleetProdTypeCode";
                    SLMResp.Batch.FleetRecord1.Odometer = "FleetRecord1.Odometer";
                    SLMResp.Batch.FleetRecord1.IDNumber = "FleetRecord1.IDNumber";
                    SLMResp.Batch.FleetRecord1.VehicleNumber = "FleetRecord1.VehicleNumber";
                    SLMResp.Batch.FleetRecord1.DriverIDNumber = "FleetRecord1.DriverIDNumber";
                }

                public FleetRecord1()
                {
                }
            }

            public class FormattedBillToAddress
            {
                public readonly static string NameText;

                public readonly static string AddrLine1;

                public readonly static string AddrLine2;

                public readonly static string City;

                public readonly static string State;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                static FormattedBillToAddress()
                {
                    SLMResp.Batch.FormattedBillToAddress.NameText = "FormattedBillToAddress.NameText";
                    SLMResp.Batch.FormattedBillToAddress.AddrLine1 = "FormattedBillToAddress.AddrLine1";
                    SLMResp.Batch.FormattedBillToAddress.AddrLine2 = "FormattedBillToAddress.AddrLine2";
                    SLMResp.Batch.FormattedBillToAddress.City = "FormattedBillToAddress.City";
                    SLMResp.Batch.FormattedBillToAddress.State = "FormattedBillToAddress.State";
                    SLMResp.Batch.FormattedBillToAddress.PostalCode = "FormattedBillToAddress.PostalCode";
                    SLMResp.Batch.FormattedBillToAddress.CountryCode = "FormattedBillToAddress.CountryCode";
                }

                public FormattedBillToAddress()
                {
                }
            }

            public class FormattedBillToName
            {
                public readonly static string FirstName;

                public readonly static string LastName;

                static FormattedBillToName()
                {
                    SLMResp.Batch.FormattedBillToName.FirstName = "FormattedBillToName.FirstName";
                    SLMResp.Batch.FormattedBillToName.LastName = "FormattedBillToName.LastName";
                }

                public FormattedBillToName()
                {
                }
            }

            public class FormattedBillToTelephone
            {
                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                static FormattedBillToTelephone()
                {
                    SLMResp.Batch.FormattedBillToTelephone.TelephoneType = "FormattedBillToTelephone.TelephoneType";
                    SLMResp.Batch.FormattedBillToTelephone.TelephoneNumber = "FormattedBillToTelephone.TelephoneNumber";
                }

                public FormattedBillToTelephone()
                {
                }
            }

            public class FormattedECPEUDDAddress
            {
                public readonly static string NameText;

                public readonly static string AddrLine1;

                public readonly static string AddrLine2;

                public readonly static string City;

                public readonly static string State;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                static FormattedECPEUDDAddress()
                {
                    SLMResp.Batch.FormattedECPEUDDAddress.NameText = "FormattedECPEUDDAddress.NameText";
                    SLMResp.Batch.FormattedECPEUDDAddress.AddrLine1 = "FormattedECPEUDDAddress.AddrLine1";
                    SLMResp.Batch.FormattedECPEUDDAddress.AddrLine2 = "FormattedECPEUDDAddress.AddrLine2";
                    SLMResp.Batch.FormattedECPEUDDAddress.City = "FormattedECPEUDDAddress.City";
                    SLMResp.Batch.FormattedECPEUDDAddress.State = "FormattedECPEUDDAddress.State";
                    SLMResp.Batch.FormattedECPEUDDAddress.PostalCode = "FormattedECPEUDDAddress.PostalCode";
                    SLMResp.Batch.FormattedECPEUDDAddress.CountryCode = "FormattedECPEUDDAddress.CountryCode";
                }

                public FormattedECPEUDDAddress()
                {
                }
            }

            public class FormattedECPEUDDTelephone
            {
                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                static FormattedECPEUDDTelephone()
                {
                    SLMResp.Batch.FormattedECPEUDDTelephone.TelephoneType = "FormattedECPEUDDTelephone.TelephoneType";
                    SLMResp.Batch.FormattedECPEUDDTelephone.TelephoneNumber = "FormattedECPEUDDTelephone.TelephoneNumber";
                }

                public FormattedECPEUDDTelephone()
                {
                }
            }

            public class FormattedShipToAddress
            {
                public readonly static string NameText;

                public readonly static string AddrLine1;

                public readonly static string AddrLine2;

                public readonly static string City;

                public readonly static string State;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                static FormattedShipToAddress()
                {
                    SLMResp.Batch.FormattedShipToAddress.NameText = "FormattedShipToAddress.NameText";
                    SLMResp.Batch.FormattedShipToAddress.AddrLine1 = "FormattedShipToAddress.AddrLine1";
                    SLMResp.Batch.FormattedShipToAddress.AddrLine2 = "FormattedShipToAddress.AddrLine2";
                    SLMResp.Batch.FormattedShipToAddress.City = "FormattedShipToAddress.City";
                    SLMResp.Batch.FormattedShipToAddress.State = "FormattedShipToAddress.State";
                    SLMResp.Batch.FormattedShipToAddress.PostalCode = "FormattedShipToAddress.PostalCode";
                    SLMResp.Batch.FormattedShipToAddress.CountryCode = "FormattedShipToAddress.CountryCode";
                }

                public FormattedShipToAddress()
                {
                }
            }

            public class FormattedShipToName
            {
                public readonly static string FirstName;

                public readonly static string LastName;

                static FormattedShipToName()
                {
                    SLMResp.Batch.FormattedShipToName.FirstName = "FormattedShipToName.FirstName";
                    SLMResp.Batch.FormattedShipToName.LastName = "FormattedShipToName.LastName";
                }

                public FormattedShipToName()
                {
                }
            }

            public class FormattedShipToTelephone
            {
                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                static FormattedShipToTelephone()
                {
                    SLMResp.Batch.FormattedShipToTelephone.TelephoneType = "FormattedShipToTelephone.TelephoneType";
                    SLMResp.Batch.FormattedShipToTelephone.TelephoneNumber = "FormattedShipToTelephone.TelephoneNumber";
                }

                public FormattedShipToTelephone()
                {
                }
            }

            public class FraudRecord1
            {
                public readonly static string CardSecurityValue;

                public readonly static string CardSecurityPresence;

                static FraudRecord1()
                {
                    SLMResp.Batch.FraudRecord1.CardSecurityValue = "FraudRecord1.CardSecurityValue";
                    SLMResp.Batch.FraudRecord1.CardSecurityPresence = "FraudRecord1.CardSecurityPresence";
                }

                public FraudRecord1()
                {
                }
            }

            public class FraudScoringInputData1
            {
                public readonly static string ReturnRulesTriggered;

                public readonly static string SafetechMerchantID;

                public readonly static string KaptchaSessionID;

                public readonly static string WebSiteShortName;

                static FraudScoringInputData1()
                {
                    SLMResp.Batch.FraudScoringInputData1.ReturnRulesTriggered = "FraudScoringInputData1.ReturnRulesTriggered";
                    SLMResp.Batch.FraudScoringInputData1.SafetechMerchantID = "FraudScoringInputData1.SafetechMerchantID";
                    SLMResp.Batch.FraudScoringInputData1.KaptchaSessionID = "FraudScoringInputData1.KaptchaSessionID";
                    SLMResp.Batch.FraudScoringInputData1.WebSiteShortName = "FraudScoringInputData1.WebSiteShortName";
                }

                public FraudScoringInputData1()
                {
                }
            }

            public class FraudScoringInputData2
            {
                public readonly static string CashValueOfFencibleItems;

                public readonly static string CustomerDateOfBirth;

                public readonly static string CustomerGender;

                public readonly static string CustomerDriversLicenseNumber;

                public readonly static string CustomerID;

                public readonly static string CustomerIDCreationTime;

                static FraudScoringInputData2()
                {
                    SLMResp.Batch.FraudScoringInputData2.CashValueOfFencibleItems = "FraudScoringInputData2.CashValueOfFencibleItems";
                    SLMResp.Batch.FraudScoringInputData2.CustomerDateOfBirth = "FraudScoringInputData2.CustomerDateOfBirth";
                    SLMResp.Batch.FraudScoringInputData2.CustomerGender = "FraudScoringInputData2.CustomerGender";
                    SLMResp.Batch.FraudScoringInputData2.CustomerDriversLicenseNumber = "FraudScoringInputData2.CustomerDriversLicenseNumber";
                    SLMResp.Batch.FraudScoringInputData2.CustomerID = "FraudScoringInputData2.CustomerID";
                    SLMResp.Batch.FraudScoringInputData2.CustomerIDCreationTime = "FraudScoringInputData2.CustomerIDCreationTime";
                }

                public FraudScoringInputData2()
                {
                }
            }

            public class FraudScoringOutputData1
            {
                public readonly static string FraudStatusCode;

                public readonly static string RiskInquiryTransactionID;

                public readonly static string AutoDecisionResponse;

                public readonly static string RiskScore;

                public readonly static string KaptchaMatchFlag;

                public readonly static string WorstCountry;

                public readonly static string CountryStateProvince;

                public readonly static string PaymentBrand;

                public readonly static string Velocity14Days;

                public readonly static string Velocity6Hours;

                public readonly static string CustomerNetwork;

                public readonly static string NumberOfDevicesWithTransaction;

                public readonly static string NumberOfCardsWithTransaction;

                public readonly static string NumberOfEmailsWithTransaction;

                static FraudScoringOutputData1()
                {
                    SLMResp.Batch.FraudScoringOutputData1.FraudStatusCode = "FraudScoringOutputData1.FraudStatusCode";
                    SLMResp.Batch.FraudScoringOutputData1.RiskInquiryTransactionID = "FraudScoringOutputData1.RiskInquiryTransactionID";
                    SLMResp.Batch.FraudScoringOutputData1.AutoDecisionResponse = "FraudScoringOutputData1.AutoDecisionResponse";
                    SLMResp.Batch.FraudScoringOutputData1.RiskScore = "FraudScoringOutputData1.RiskScore";
                    SLMResp.Batch.FraudScoringOutputData1.KaptchaMatchFlag = "FraudScoringOutputData1.KaptchaMatchFlag";
                    SLMResp.Batch.FraudScoringOutputData1.WorstCountry = "FraudScoringOutputData1.WorstCountry";
                    SLMResp.Batch.FraudScoringOutputData1.CountryStateProvince = "FraudScoringOutputData1.CountryStateProvince";
                    SLMResp.Batch.FraudScoringOutputData1.PaymentBrand = "FraudScoringOutputData1.PaymentBrand";
                    SLMResp.Batch.FraudScoringOutputData1.Velocity14Days = "FraudScoringOutputData1.Velocity14Days";
                    SLMResp.Batch.FraudScoringOutputData1.Velocity6Hours = "FraudScoringOutputData1.Velocity6Hours";
                    SLMResp.Batch.FraudScoringOutputData1.CustomerNetwork = "FraudScoringOutputData1.CustomerNetwork";
                    SLMResp.Batch.FraudScoringOutputData1.NumberOfDevicesWithTransaction = "FraudScoringOutputData1.NumberOfDevicesWithTransaction";
                    SLMResp.Batch.FraudScoringOutputData1.NumberOfCardsWithTransaction = "FraudScoringOutputData1.NumberOfCardsWithTransaction";
                    SLMResp.Batch.FraudScoringOutputData1.NumberOfEmailsWithTransaction = "FraudScoringOutputData1.NumberOfEmailsWithTransaction";
                }

                public FraudScoringOutputData1()
                {
                }
            }

            public class FraudScoringOutputData2
            {
                public readonly static string DeviceLayers;

                public readonly static string DeviceFingerPrint;

                static FraudScoringOutputData2()
                {
                    SLMResp.Batch.FraudScoringOutputData2.DeviceLayers = "FraudScoringOutputData2.DeviceLayers";
                    SLMResp.Batch.FraudScoringOutputData2.DeviceFingerPrint = "FraudScoringOutputData2.DeviceFingerPrint";
                }

                public FraudScoringOutputData2()
                {
                }
            }

            public class FraudScoringOutputData3
            {
                public readonly static string CustomerTimeZone;

                public readonly static string CustomerLocalDateTime;

                public readonly static string DeviceRegion;

                public readonly static string DeviceCountry;

                public readonly static string ProxyStatus;

                public readonly static string JavaScriptStatus;

                public readonly static string FlashStatus;

                public readonly static string CookiesStatus;

                public readonly static string BrowserCountry;

                public readonly static string BrowserLanguage;

                public readonly static string MobileDeviceIndicator;

                public readonly static string MobileDeviceType;

                public readonly static string MobileWirelessIndicator;

                public readonly static string VoiceDevice;

                public readonly static string PCRemoteIndicator;

                static FraudScoringOutputData3()
                {
                    SLMResp.Batch.FraudScoringOutputData3.CustomerTimeZone = "FraudScoringOutputData3.CustomerTimeZone";
                    SLMResp.Batch.FraudScoringOutputData3.CustomerLocalDateTime = "FraudScoringOutputData3.CustomerLocalDateTime";
                    SLMResp.Batch.FraudScoringOutputData3.DeviceRegion = "FraudScoringOutputData3.DeviceRegion";
                    SLMResp.Batch.FraudScoringOutputData3.DeviceCountry = "FraudScoringOutputData3.DeviceCountry";
                    SLMResp.Batch.FraudScoringOutputData3.ProxyStatus = "FraudScoringOutputData3.ProxyStatus";
                    SLMResp.Batch.FraudScoringOutputData3.JavaScriptStatus = "FraudScoringOutputData3.JavaScriptStatus";
                    SLMResp.Batch.FraudScoringOutputData3.FlashStatus = "FraudScoringOutputData3.FlashStatus";
                    SLMResp.Batch.FraudScoringOutputData3.CookiesStatus = "FraudScoringOutputData3.CookiesStatus";
                    SLMResp.Batch.FraudScoringOutputData3.BrowserCountry = "FraudScoringOutputData3.BrowserCountry";
                    SLMResp.Batch.FraudScoringOutputData3.BrowserLanguage = "FraudScoringOutputData3.BrowserLanguage";
                    SLMResp.Batch.FraudScoringOutputData3.MobileDeviceIndicator = "FraudScoringOutputData3.MobileDeviceIndicator";
                    SLMResp.Batch.FraudScoringOutputData3.MobileDeviceType = "FraudScoringOutputData3.MobileDeviceType";
                    SLMResp.Batch.FraudScoringOutputData3.MobileWirelessIndicator = "FraudScoringOutputData3.MobileWirelessIndicator";
                    SLMResp.Batch.FraudScoringOutputData3.VoiceDevice = "FraudScoringOutputData3.VoiceDevice";
                    SLMResp.Batch.FraudScoringOutputData3.PCRemoteIndicator = "FraudScoringOutputData3.PCRemoteIndicator";
                }

                public FraudScoringOutputData3()
                {
                }
            }

            public class FuelRecord1
            {
                public readonly static string PurchaseTime;

                public readonly static string FuelPurchaseType;

                public readonly static string FuelServiceTypeCode;

                public readonly static string FuelProductCode;

                public readonly static string FuelUnitPrice;

                public readonly static string FuelUnitOfMeasure;

                public readonly static string FuelQuantity;

                public readonly static string FuelSaleAmount;

                public readonly static string FuelTaxAmount;

                public readonly static string FuelDiscountAmount;

                public readonly static string MiscellaneousFuelTax;

                public readonly static string MiscellaneousFuelTaxExemptionIndicator;

                public readonly static string FuelVATTaxRate;

                static FuelRecord1()
                {
                    SLMResp.Batch.FuelRecord1.PurchaseTime = "FuelRecord1.PurchaseTime";
                    SLMResp.Batch.FuelRecord1.FuelPurchaseType = "FuelRecord1.FuelPurchaseType";
                    SLMResp.Batch.FuelRecord1.FuelServiceTypeCode = "FuelRecord1.FuelServiceTypeCode";
                    SLMResp.Batch.FuelRecord1.FuelProductCode = "FuelRecord1.FuelProductCode";
                    SLMResp.Batch.FuelRecord1.FuelUnitPrice = "FuelRecord1.FuelUnitPrice";
                    SLMResp.Batch.FuelRecord1.FuelUnitOfMeasure = "FuelRecord1.FuelUnitOfMeasure";
                    SLMResp.Batch.FuelRecord1.FuelQuantity = "FuelRecord1.FuelQuantity";
                    SLMResp.Batch.FuelRecord1.FuelSaleAmount = "FuelRecord1.FuelSaleAmount";
                    SLMResp.Batch.FuelRecord1.FuelTaxAmount = "FuelRecord1.FuelTaxAmount";
                    SLMResp.Batch.FuelRecord1.FuelDiscountAmount = "FuelRecord1.FuelDiscountAmount";
                    SLMResp.Batch.FuelRecord1.MiscellaneousFuelTax = "FuelRecord1.MiscellaneousFuelTax";
                    SLMResp.Batch.FuelRecord1.MiscellaneousFuelTaxExemptionIndicator = "FuelRecord1.MiscellaneousFuelTaxExemptionIndicator";
                    SLMResp.Batch.FuelRecord1.FuelVATTaxRate = "FuelRecord1.FuelVATTaxRate";
                }

                public FuelRecord1()
                {
                }
            }

            public class FuelRecord2
            {
                public readonly static string NonFuelGrossAmount;

                public readonly static string NonFuelTaxAmount;

                public readonly static string NonFuelDiscountAmount;

                public readonly static string NonFuelMiscellaneousTaxAmount;

                public readonly static string MiscellaneousNonFuelTaxIndicator;

                public readonly static string ProductCode;

                static FuelRecord2()
                {
                    SLMResp.Batch.FuelRecord2.NonFuelGrossAmount = "FuelRecord2.NonFuelGrossAmount";
                    SLMResp.Batch.FuelRecord2.NonFuelTaxAmount = "FuelRecord2.NonFuelTaxAmount";
                    SLMResp.Batch.FuelRecord2.NonFuelDiscountAmount = "FuelRecord2.NonFuelDiscountAmount";
                    SLMResp.Batch.FuelRecord2.NonFuelMiscellaneousTaxAmount = "FuelRecord2.NonFuelMiscellaneousTaxAmount";
                    SLMResp.Batch.FuelRecord2.MiscellaneousNonFuelTaxIndicator = "FuelRecord2.MiscellaneousNonFuelTaxIndicator";
                    SLMResp.Batch.FuelRecord2.ProductCode = "FuelRecord2.ProductCode";
                }

                public FuelRecord2()
                {
                }
            }

            public class GiftCardRecord1
            {
                public readonly static string CurrentBalance;

                public readonly static string PreviousBalance;

                static GiftCardRecord1()
                {
                    SLMResp.Batch.GiftCardRecord1.CurrentBalance = "GiftCardRecord1.CurrentBalance";
                    SLMResp.Batch.GiftCardRecord1.PreviousBalance = "GiftCardRecord1.PreviousBalance";
                }

                public GiftCardRecord1()
                {
                }
            }

            public class GiftRecipientInfo
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static GiftRecipientInfo()
                {
                    SLMResp.Batch.GiftRecipientInfo.AddressLine = "GiftRecipientInfo.AddressLine";
                    SLMResp.Batch.GiftRecipientInfo.TelephoneType = "GiftRecipientInfo.TelephoneType";
                    SLMResp.Batch.GiftRecipientInfo.TelephoneNumber = "GiftRecipientInfo.TelephoneNumber";
                    SLMResp.Batch.GiftRecipientInfo.CountryCode = "GiftRecipientInfo.CountryCode";
                }

                public GiftRecipientInfo()
                {
                }
            }

            public class GLOBALBIN
            {
                public GLOBALBIN()
                {
                }

                public class GLOBALDetail
                {
                    public readonly static string RecordType;

                    public readonly static string BINLow;

                    public readonly static string BINHigh;

                    public readonly static string MaximumPANLength;

                    public readonly static string CountryCode;

                    public readonly static string CardProduct;

                    public readonly static string CardIndicator;

                    public readonly static string DefaultCardType;

                    public readonly static string BINUpdateYear;

                    public readonly static string BINUpdateMonth;

                    public readonly static string BINUpdateDay;

                    public readonly static string NetworkIndicator;

                    public readonly static string EBTState;

                    static GLOBALDetail()
                    {
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.RecordType = "GLOBALDetail.RecordType";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.BINLow = "GLOBALDetail.BINLow";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.BINHigh = "GLOBALDetail.BINHigh";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.MaximumPANLength = "GLOBALDetail.MaximumPANLength";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.CountryCode = "GLOBALDetail.CountryCode";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.CardProduct = "GLOBALDetail.CardProduct";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.CardIndicator = "GLOBALDetail.CardIndicator";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.DefaultCardType = "GLOBALDetail.DefaultCardType";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.BINUpdateYear = "GLOBALDetail.BINUpdateYear";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.BINUpdateMonth = "GLOBALDetail.BINUpdateMonth";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.BINUpdateDay = "GLOBALDetail.BINUpdateDay";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.NetworkIndicator = "GLOBALDetail.NetworkIndicator";
                        SLMResp.Batch.GLOBALBIN.GLOBALDetail.EBTState = "GLOBALDetail.EBTState";
                    }

                    public GLOBALDetail()
                    {
                    }
                }

                public class GLOBALHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string Version;

                    public readonly static string CreationDate;

                    static GLOBALHeader()
                    {
                        SLMResp.Batch.GLOBALBIN.GLOBALHeader.RecordType = "GLOBALHeader.RecordType";
                        SLMResp.Batch.GLOBALBIN.GLOBALHeader.FileName = "GLOBALHeader.FileName";
                        SLMResp.Batch.GLOBALBIN.GLOBALHeader.Version = "GLOBALHeader.Version";
                        SLMResp.Batch.GLOBALBIN.GLOBALHeader.CreationDate = "GLOBALHeader.CreationDate";
                    }

                    public GLOBALHeader()
                    {
                    }
                }

                public class GLOBALTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDate;

                    public readonly static string NumberOfBinRecords;

                    static GLOBALTrailer()
                    {
                        SLMResp.Batch.GLOBALBIN.GLOBALTrailer.RecordType = "GLOBALTrailer.RecordType";
                        SLMResp.Batch.GLOBALBIN.GLOBALTrailer.CreationDate = "GLOBALTrailer.CreationDate";
                        SLMResp.Batch.GLOBALBIN.GLOBALTrailer.NumberOfBinRecords = "GLOBALTrailer.NumberOfBinRecords";
                    }

                    public GLOBALTrailer()
                    {
                    }
                }
            }

            public class GoodsSold1
            {
                public readonly static string GoodsSold;

                static GoodsSold1()
                {
                    SLMResp.Batch.GoodsSold1.GoodsSold = "GoodsSold1.GoodsSold";
                }

                public GoodsSold1()
                {
                }
            }

            public class HBCBIN
            {
                public HBCBIN()
                {
                }

                public class HBCBINDetail
                {
                    public readonly static string RecordType;

                    public readonly static string BINLow;

                    public readonly static string BINHigh;

                    public readonly static string MaximumPANLength;

                    public readonly static string CardProduct;

                    public readonly static string CardIndicator;

                    public readonly static string DefaultCardType;

                    public readonly static string BINUpdateDate;

                    public readonly static string ConsumerDigitalPaymentechTokenIndicator;

                    static HBCBINDetail()
                    {
                        SLMResp.Batch.HBCBIN.HBCBINDetail.RecordType = "HBCBINDetail.RecordType";
                        SLMResp.Batch.HBCBIN.HBCBINDetail.BINLow = "HBCBINDetail.BINLow";
                        SLMResp.Batch.HBCBIN.HBCBINDetail.BINHigh = "HBCBINDetail.BINHigh";
                        SLMResp.Batch.HBCBIN.HBCBINDetail.MaximumPANLength = "HBCBINDetail.MaximumPANLength";
                        SLMResp.Batch.HBCBIN.HBCBINDetail.CardProduct = "HBCBINDetail.CardProduct";
                        SLMResp.Batch.HBCBIN.HBCBINDetail.CardIndicator = "HBCBINDetail.CardIndicator";
                        SLMResp.Batch.HBCBIN.HBCBINDetail.DefaultCardType = "HBCBINDetail.DefaultCardType";
                        SLMResp.Batch.HBCBIN.HBCBINDetail.BINUpdateDate = "HBCBINDetail.BINUpdateDate";
                        SLMResp.Batch.HBCBIN.HBCBINDetail.ConsumerDigitalPaymentechTokenIndicator = "HBCBINDetail.ConsumerDigitalPaymentechTokenIndicator";
                    }

                    public HBCBINDetail()
                    {
                    }
                }

                public class HBCBINHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string Version;

                    public readonly static string CreationDate;

                    static HBCBINHeader()
                    {
                        SLMResp.Batch.HBCBIN.HBCBINHeader.RecordType = "HBCBINHeader.RecordType";
                        SLMResp.Batch.HBCBIN.HBCBINHeader.FileName = "HBCBINHeader.FileName";
                        SLMResp.Batch.HBCBIN.HBCBINHeader.Version = "HBCBINHeader.Version";
                        SLMResp.Batch.HBCBIN.HBCBINHeader.CreationDate = "HBCBINHeader.CreationDate";
                    }

                    public HBCBINHeader()
                    {
                    }
                }

                public class HBCBINTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDate;

                    public readonly static string NumberOfBinRecords;

                    public readonly static string NumberOfBINRecords;

                    static HBCBINTrailer()
                    {
                        SLMResp.Batch.HBCBIN.HBCBINTrailer.RecordType = "HBCBINTrailer.RecordType";
                        SLMResp.Batch.HBCBIN.HBCBINTrailer.CreationDate = "HBCBINTrailer.CreationDate";
                        SLMResp.Batch.HBCBIN.HBCBINTrailer.NumberOfBinRecords = "HBCBINTrailer.NumberOfBinRecords";
                        SLMResp.Batch.HBCBIN.HBCBINTrailer.NumberOfBINRecords = "HBCBINTrailer.NumberOfBINRecords";
                    }

                    public HBCBINTrailer()
                    {
                    }
                }
            }

            public class Header
            {
                public readonly static string PID;

                public readonly static string PIDPassword;

                public readonly static string SID;

                public readonly static string SIDPassword;

                public readonly static string CreationDate;

                public readonly static string SubmissionNumber;

                public readonly static string FileName;

                public readonly static string MerchantSpace;

                static Header()
                {
                    SLMResp.Batch.Header.PID = "Header.PID";
                    SLMResp.Batch.Header.PIDPassword = "Header.PIDPassword";
                    SLMResp.Batch.Header.SID = "Header.SID";
                    SLMResp.Batch.Header.SIDPassword = "Header.SIDPassword";
                    SLMResp.Batch.Header.CreationDate = "Header.CreationDate";
                    SLMResp.Batch.Header.SubmissionNumber = "Header.SubmissionNumber";
                    SLMResp.Batch.Header.FileName = "Header.FileName";
                    SLMResp.Batch.Header.MerchantSpace = "Header.MerchantSpace";
                }

                public Header()
                {
                }
            }

            public class HealthcareIIASRecord1
            {
                public readonly static string QHPAmount;

                public readonly static string RXAmount;

                public readonly static string VisionAmount;

                public readonly static string ClinicOtherAmount;

                public readonly static string DentalAmount;

                public readonly static string IIASFlag;

                static HealthcareIIASRecord1()
                {
                    SLMResp.Batch.HealthcareIIASRecord1.QHPAmount = "HealthcareIIASRecord1.QHPAmount";
                    SLMResp.Batch.HealthcareIIASRecord1.RXAmount = "HealthcareIIASRecord1.RXAmount";
                    SLMResp.Batch.HealthcareIIASRecord1.VisionAmount = "HealthcareIIASRecord1.VisionAmount";
                    SLMResp.Batch.HealthcareIIASRecord1.ClinicOtherAmount = "HealthcareIIASRecord1.ClinicOtherAmount";
                    SLMResp.Batch.HealthcareIIASRecord1.DentalAmount = "HealthcareIIASRecord1.DentalAmount";
                    SLMResp.Batch.HealthcareIIASRecord1.IIASFlag = "HealthcareIIASRecord1.IIASFlag";
                }

                public HealthcareIIASRecord1()
                {
                }
            }

            public class Insurance1
            {
                public readonly static string PolicyNumber;

                public readonly static string CoverageStartDate;

                public readonly static string CoverageEndDate;

                public readonly static string PremiumFrequency;

                public readonly static string PolicyType;

                static Insurance1()
                {
                    SLMResp.Batch.Insurance1.PolicyNumber = "Insurance1.PolicyNumber";
                    SLMResp.Batch.Insurance1.CoverageStartDate = "Insurance1.CoverageStartDate";
                    SLMResp.Batch.Insurance1.CoverageEndDate = "Insurance1.CoverageEndDate";
                    SLMResp.Batch.Insurance1.PremiumFrequency = "Insurance1.PremiumFrequency";
                    SLMResp.Batch.Insurance1.PolicyType = "Insurance1.PolicyType";
                }

                public Insurance1()
                {
                }
            }

            public class Insurance2
            {
                public readonly static string NameOfInsured;

                public readonly static string AdditionalPolicyNumber;

                static Insurance2()
                {
                    SLMResp.Batch.Insurance2.NameOfInsured = "Insurance2.NameOfInsured";
                    SLMResp.Batch.Insurance2.AdditionalPolicyNumber = "Insurance2.AdditionalPolicyNumber";
                }

                public Insurance2()
                {
                }
            }

            public class IntlMaestroExtRecord1
            {
                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string POSAuthorizationSource;

                public readonly static string POSCardIDMethod;

                public readonly static string BanknetReferenceNumber;

                public readonly static string AuthorizedAmount;

                public readonly static string BanknetDate;

                public readonly static string MerchantCategoryCode;

                public readonly static string CATType;

                public readonly static string TransactionCategoryIndicator;

                public readonly static string AuthType;

                static IntlMaestroExtRecord1()
                {
                    SLMResp.Batch.IntlMaestroExtRecord1.POSCapabilityCode = "IntlMaestroExtRecord1.POSCapabilityCode";
                    SLMResp.Batch.IntlMaestroExtRecord1.POSEntryMode = "IntlMaestroExtRecord1.POSEntryMode";
                    SLMResp.Batch.IntlMaestroExtRecord1.POSAuthorizationSource = "IntlMaestroExtRecord1.POSAuthorizationSource";
                    SLMResp.Batch.IntlMaestroExtRecord1.POSCardIDMethod = "IntlMaestroExtRecord1.POSCardIDMethod";
                    SLMResp.Batch.IntlMaestroExtRecord1.BanknetReferenceNumber = "IntlMaestroExtRecord1.BanknetReferenceNumber";
                    SLMResp.Batch.IntlMaestroExtRecord1.AuthorizedAmount = "IntlMaestroExtRecord1.AuthorizedAmount";
                    SLMResp.Batch.IntlMaestroExtRecord1.BanknetDate = "IntlMaestroExtRecord1.BanknetDate";
                    SLMResp.Batch.IntlMaestroExtRecord1.MerchantCategoryCode = "IntlMaestroExtRecord1.MerchantCategoryCode";
                    SLMResp.Batch.IntlMaestroExtRecord1.CATType = "IntlMaestroExtRecord1.CATType";
                    SLMResp.Batch.IntlMaestroExtRecord1.TransactionCategoryIndicator = "IntlMaestroExtRecord1.TransactionCategoryIndicator";
                    SLMResp.Batch.IntlMaestroExtRecord1.AuthType = "IntlMaestroExtRecord1.AuthType";
                }

                public IntlMaestroExtRecord1()
                {
                }
            }

            public class IntlMaestroExtRecord2
            {
                public readonly static string AccountholderAuthenticationValue;

                static IntlMaestroExtRecord2()
                {
                    SLMResp.Batch.IntlMaestroExtRecord2.AccountholderAuthenticationValue = "IntlMaestroExtRecord2.AccountholderAuthenticationValue";
                }

                public IntlMaestroExtRecord2()
                {
                }
            }

            public class IntlMaestroExtRecord3
            {
                public readonly static string SubtypeFlag;

                static IntlMaestroExtRecord3()
                {
                    SLMResp.Batch.IntlMaestroExtRecord3.SubtypeFlag = "IntlMaestroExtRecord3.SubtypeFlag";
                }

                public IntlMaestroExtRecord3()
                {
                }
            }

            public class JapanCreditBureauAuthenticationExtRec
            {
                public readonly static string AEVV;

                public readonly static string TransactionID;

                static JapanCreditBureauAuthenticationExtRec()
                {
                    SLMResp.Batch.JapanCreditBureauAuthenticationExtRec.AEVV = "JapanCreditBureauAuthenticationExtRec.AEVV";
                    SLMResp.Batch.JapanCreditBureauAuthenticationExtRec.TransactionID = "JapanCreditBureauAuthenticationExtRec.TransactionID";
                }

                public JapanCreditBureauAuthenticationExtRec()
                {
                }
            }

            public class JapanCreditBureauExtRecord1
            {
                public readonly static string SystemTraceAuditNumber;

                public readonly static string ProcessingCodeType;

                public readonly static string FromAccount;

                public readonly static string ToAccount;

                public readonly static string Track1DataConditionCode;

                public readonly static string Track2DataConditionCode;

                public readonly static string PANEntryMode;

                public readonly static string PINEntryMode;

                public readonly static string POSDeviceAttendanceIndicator;

                public readonly static string POSDeviceLocationIndicator;

                public readonly static string POSCardholderPresenceIndicator;

                public readonly static string POSCardPresenceIndicator;

                public readonly static string POSCardCaptureCapabilityIndicator;

                public readonly static string POSTransactionStatusIndicator;

                public readonly static string POSTransactionSecurityIndicator;

                public readonly static string POSTypeOfTerminalDevice;

                public readonly static string POSDeviceCardDataInputCapabilityIndicator;

                public readonly static string LocalTransactionDate;

                public readonly static string LocalTransactionTime;

                public readonly static string NetworkReferenceID;

                static JapanCreditBureauExtRecord1()
                {
                    SLMResp.Batch.JapanCreditBureauExtRecord1.SystemTraceAuditNumber = "JapanCreditBureauExtRecord1.SystemTraceAuditNumber";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.ProcessingCodeType = "JapanCreditBureauExtRecord1.ProcessingCodeType";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.FromAccount = "JapanCreditBureauExtRecord1.FromAccount";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.ToAccount = "JapanCreditBureauExtRecord1.ToAccount";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.Track1DataConditionCode = "JapanCreditBureauExtRecord1.Track1DataConditionCode";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.Track2DataConditionCode = "JapanCreditBureauExtRecord1.Track2DataConditionCode";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.PANEntryMode = "JapanCreditBureauExtRecord1.PANEntryMode";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.PINEntryMode = "JapanCreditBureauExtRecord1.PINEntryMode";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.POSDeviceAttendanceIndicator = "JapanCreditBureauExtRecord1.POSDeviceAttendanceIndicator";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.POSDeviceLocationIndicator = "JapanCreditBureauExtRecord1.POSDeviceLocationIndicator";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.POSCardholderPresenceIndicator = "JapanCreditBureauExtRecord1.POSCardholderPresenceIndicator";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.POSCardPresenceIndicator = "JapanCreditBureauExtRecord1.POSCardPresenceIndicator";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.POSCardCaptureCapabilityIndicator = "JapanCreditBureauExtRecord1.POSCardCaptureCapabilityIndicator";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.POSTransactionStatusIndicator = "JapanCreditBureauExtRecord1.POSTransactionStatusIndicator";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.POSTransactionSecurityIndicator = "JapanCreditBureauExtRecord1.POSTransactionSecurityIndicator";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.POSTypeOfTerminalDevice = "JapanCreditBureauExtRecord1.POSTypeOfTerminalDevice";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.POSDeviceCardDataInputCapabilityIndicator = "JapanCreditBureauExtRecord1.POSDeviceCardDataInputCapabilityIndicator";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.LocalTransactionDate = "JapanCreditBureauExtRecord1.LocalTransactionDate";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.LocalTransactionTime = "JapanCreditBureauExtRecord1.LocalTransactionTime";
                    SLMResp.Batch.JapanCreditBureauExtRecord1.NetworkReferenceID = "JapanCreditBureauExtRecord1.NetworkReferenceID";
                }

                public JapanCreditBureauExtRecord1()
                {
                }
            }

            public class JapanCreditBureauExtRecord2
            {
                public readonly static string TAA1;

                public readonly static string TAA2;

                static JapanCreditBureauExtRecord2()
                {
                    SLMResp.Batch.JapanCreditBureauExtRecord2.TAA1 = "JapanCreditBureauExtRecord2.TAA1";
                    SLMResp.Batch.JapanCreditBureauExtRecord2.TAA2 = "JapanCreditBureauExtRecord2.TAA2";
                }

                public JapanCreditBureauExtRecord2()
                {
                }
            }

            public class JapanCreditBureauExtRecord3
            {
                public readonly static string TAA3;

                public readonly static string TAA4;

                static JapanCreditBureauExtRecord3()
                {
                    SLMResp.Batch.JapanCreditBureauExtRecord3.TAA3 = "JapanCreditBureauExtRecord3.TAA3";
                    SLMResp.Batch.JapanCreditBureauExtRecord3.TAA4 = "JapanCreditBureauExtRecord3.TAA4";
                }

                public JapanCreditBureauExtRecord3()
                {
                }
            }

            public class JapanCreditBureauExtRecord4
            {
                public readonly static string TransactionIdentifier;

                public readonly static string POSCapabilityCode;

                public readonly static string AccountholderAuthenticationCapability;

                public readonly static string CardCaptureCapability;

                public readonly static string OperatingEnvironment;

                public readonly static string AccountholderPresent;

                public readonly static string CardPresent;

                public readonly static string POSEntryMode;

                public readonly static string POSCardIDMethod;

                public readonly static string AccountholderAuthenticationEntity;

                public readonly static string CardDataOutputCapability;

                public readonly static string TerminalOutputCapability;

                public readonly static string PINCaptureCapability;

                public readonly static string AuthorizedAmount;

                static JapanCreditBureauExtRecord4()
                {
                    SLMResp.Batch.JapanCreditBureauExtRecord4.TransactionIdentifier = "JapanCreditBureauExtRecord4.TransactionIdentifier";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.POSCapabilityCode = "JapanCreditBureauExtRecord4.POSCapabilityCode";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.AccountholderAuthenticationCapability = "JapanCreditBureauExtRecord4.AccountholderAuthenticationCapability";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.CardCaptureCapability = "JapanCreditBureauExtRecord4.CardCaptureCapability";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.OperatingEnvironment = "JapanCreditBureauExtRecord4.OperatingEnvironment";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.AccountholderPresent = "JapanCreditBureauExtRecord4.AccountholderPresent";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.CardPresent = "JapanCreditBureauExtRecord4.CardPresent";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.POSEntryMode = "JapanCreditBureauExtRecord4.POSEntryMode";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.POSCardIDMethod = "JapanCreditBureauExtRecord4.POSCardIDMethod";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.AccountholderAuthenticationEntity = "JapanCreditBureauExtRecord4.AccountholderAuthenticationEntity";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.CardDataOutputCapability = "JapanCreditBureauExtRecord4.CardDataOutputCapability";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.TerminalOutputCapability = "JapanCreditBureauExtRecord4.TerminalOutputCapability";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.PINCaptureCapability = "JapanCreditBureauExtRecord4.PINCaptureCapability";
                    SLMResp.Batch.JapanCreditBureauExtRecord4.AuthorizedAmount = "JapanCreditBureauExtRecord4.AuthorizedAmount";
                }

                public JapanCreditBureauExtRecord4()
                {
                }
            }

            public class Level2InternationalLineItemRecord
            {
                public readonly static string CommodityCode;

                public readonly static string VATID;

                public readonly static string PSTRegistrationTaxNumber;

                public readonly static string LocalTaxDetailTaxAmount2;

                public readonly static string NationalTaxRateDetailTaxRate1;

                static Level2InternationalLineItemRecord()
                {
                    SLMResp.Batch.Level2InternationalLineItemRecord.CommodityCode = "Level2InternationalLineItemRecord.CommodityCode";
                    SLMResp.Batch.Level2InternationalLineItemRecord.VATID = "Level2InternationalLineItemRecord.VATID";
                    SLMResp.Batch.Level2InternationalLineItemRecord.PSTRegistrationTaxNumber = "Level2InternationalLineItemRecord.PSTRegistrationTaxNumber";
                    SLMResp.Batch.Level2InternationalLineItemRecord.LocalTaxDetailTaxAmount2 = "Level2InternationalLineItemRecord.LocalTaxDetailTaxAmount2";
                    SLMResp.Batch.Level2InternationalLineItemRecord.NationalTaxRateDetailTaxRate1 = "Level2InternationalLineItemRecord.NationalTaxRateDetailTaxRate1";
                }

                public Level2InternationalLineItemRecord()
                {
                }
            }

            public class Level2LineItemRecord
            {
                public readonly static string CustomerReferenceNumber;

                public readonly static string SalesTaxAmount;

                public readonly static string RequestorName;

                public readonly static string DestinationPostalCode;

                public readonly static string ExemptIndicator;

                public readonly static string CustomerVATRegistrationNumber;

                public readonly static string NationalTax;

                public readonly static string LocalTaxRate;

                static Level2LineItemRecord()
                {
                    SLMResp.Batch.Level2LineItemRecord.CustomerReferenceNumber = "Level2LineItemRecord.CustomerReferenceNumber";
                    SLMResp.Batch.Level2LineItemRecord.SalesTaxAmount = "Level2LineItemRecord.SalesTaxAmount";
                    SLMResp.Batch.Level2LineItemRecord.RequestorName = "Level2LineItemRecord.RequestorName";
                    SLMResp.Batch.Level2LineItemRecord.DestinationPostalCode = "Level2LineItemRecord.DestinationPostalCode";
                    SLMResp.Batch.Level2LineItemRecord.ExemptIndicator = "Level2LineItemRecord.ExemptIndicator";
                    SLMResp.Batch.Level2LineItemRecord.CustomerVATRegistrationNumber = "Level2LineItemRecord.CustomerVATRegistrationNumber";
                    SLMResp.Batch.Level2LineItemRecord.NationalTax = "Level2LineItemRecord.NationalTax";
                    SLMResp.Batch.Level2LineItemRecord.LocalTaxRate = "Level2LineItemRecord.LocalTaxRate";
                }

                public Level2LineItemRecord()
                {
                }
            }

            public class LodgingRecord1
            {
                public readonly static string Duration;

                public readonly static string NoShowIndicator;

                public readonly static string ArrivalDate;

                public readonly static string DepartureDate;

                public readonly static string ExtraCharges;

                public readonly static string ExtraChargesAmount;

                public readonly static string FolioNumber;

                public readonly static string PropertyPhoneNumber;

                public readonly static string CSRPhoneNumber;

                public readonly static string RoomRate;

                public readonly static string RoomTax;

                public readonly static string FireSafetyIndicator;

                public readonly static string AmericanExpressFolioNumber;

                public readonly static string NumberOfNightsAtRoomRate1;

                public readonly static string AdvancedDepositIndicator;

                static LodgingRecord1()
                {
                    SLMResp.Batch.LodgingRecord1.Duration = "LodgingRecord1.Duration";
                    SLMResp.Batch.LodgingRecord1.NoShowIndicator = "LodgingRecord1.NoShowIndicator";
                    SLMResp.Batch.LodgingRecord1.ArrivalDate = "LodgingRecord1.ArrivalDate";
                    SLMResp.Batch.LodgingRecord1.DepartureDate = "LodgingRecord1.DepartureDate";
                    SLMResp.Batch.LodgingRecord1.ExtraCharges = "LodgingRecord1.ExtraCharges";
                    SLMResp.Batch.LodgingRecord1.ExtraChargesAmount = "LodgingRecord1.ExtraChargesAmount";
                    SLMResp.Batch.LodgingRecord1.FolioNumber = "LodgingRecord1.FolioNumber";
                    SLMResp.Batch.LodgingRecord1.PropertyPhoneNumber = "LodgingRecord1.PropertyPhoneNumber";
                    SLMResp.Batch.LodgingRecord1.CSRPhoneNumber = "LodgingRecord1.CSRPhoneNumber";
                    SLMResp.Batch.LodgingRecord1.RoomRate = "LodgingRecord1.RoomRate";
                    SLMResp.Batch.LodgingRecord1.RoomTax = "LodgingRecord1.RoomTax";
                    SLMResp.Batch.LodgingRecord1.FireSafetyIndicator = "LodgingRecord1.FireSafetyIndicator";
                    SLMResp.Batch.LodgingRecord1.AmericanExpressFolioNumber = "LodgingRecord1.AmericanExpressFolioNumber";
                    SLMResp.Batch.LodgingRecord1.NumberOfNightsAtRoomRate1 = "LodgingRecord1.NumberOfNightsAtRoomRate1";
                    SLMResp.Batch.LodgingRecord1.AdvancedDepositIndicator = "LodgingRecord1.AdvancedDepositIndicator";
                }

                public LodgingRecord1()
                {
                }
            }

            public class LodgingRecord2
            {
                public readonly static string RenterName;

                public readonly static string RoomRate2;

                public readonly static string NumberNightsRoomRate2;

                public readonly static string RoomRate3;

                public readonly static string NumberNightsRoomRate3;

                public readonly static string ProgramCode;

                public readonly static string PrepaidExpenseAmount;

                public readonly static string CashAdvanceAmount;

                public readonly static string TotalNonRoomChargesAmount;

                static LodgingRecord2()
                {
                    SLMResp.Batch.LodgingRecord2.RenterName = "LodgingRecord2.RenterName";
                    SLMResp.Batch.LodgingRecord2.RoomRate2 = "LodgingRecord2.RoomRate2";
                    SLMResp.Batch.LodgingRecord2.NumberNightsRoomRate2 = "LodgingRecord2.NumberNightsRoomRate2";
                    SLMResp.Batch.LodgingRecord2.RoomRate3 = "LodgingRecord2.RoomRate3";
                    SLMResp.Batch.LodgingRecord2.NumberNightsRoomRate3 = "LodgingRecord2.NumberNightsRoomRate3";
                    SLMResp.Batch.LodgingRecord2.ProgramCode = "LodgingRecord2.ProgramCode";
                    SLMResp.Batch.LodgingRecord2.PrepaidExpenseAmount = "LodgingRecord2.PrepaidExpenseAmount";
                    SLMResp.Batch.LodgingRecord2.CashAdvanceAmount = "LodgingRecord2.CashAdvanceAmount";
                    SLMResp.Batch.LodgingRecord2.TotalNonRoomChargesAmount = "LodgingRecord2.TotalNonRoomChargesAmount";
                }

                public LodgingRecord2()
                {
                }
            }

            public class LodgingRecord3
            {
                public readonly static string PhoneChargesAmount;

                public readonly static string RestaurantChargesAmount;

                public readonly static string MinibarChargesAmount;

                public readonly static string GiftShopChargesAmount;

                public readonly static string LaundryChargesAmount;

                public readonly static string ParkingChargesAmount;

                public readonly static string MovieChargesAmount;

                public readonly static string BusinessCenterChargesAmount;

                public readonly static string HealthClubChargesAmount;

                static LodgingRecord3()
                {
                    SLMResp.Batch.LodgingRecord3.PhoneChargesAmount = "LodgingRecord3.PhoneChargesAmount";
                    SLMResp.Batch.LodgingRecord3.RestaurantChargesAmount = "LodgingRecord3.RestaurantChargesAmount";
                    SLMResp.Batch.LodgingRecord3.MinibarChargesAmount = "LodgingRecord3.MinibarChargesAmount";
                    SLMResp.Batch.LodgingRecord3.GiftShopChargesAmount = "LodgingRecord3.GiftShopChargesAmount";
                    SLMResp.Batch.LodgingRecord3.LaundryChargesAmount = "LodgingRecord3.LaundryChargesAmount";
                    SLMResp.Batch.LodgingRecord3.ParkingChargesAmount = "LodgingRecord3.ParkingChargesAmount";
                    SLMResp.Batch.LodgingRecord3.MovieChargesAmount = "LodgingRecord3.MovieChargesAmount";
                    SLMResp.Batch.LodgingRecord3.BusinessCenterChargesAmount = "LodgingRecord3.BusinessCenterChargesAmount";
                    SLMResp.Batch.LodgingRecord3.HealthClubChargesAmount = "LodgingRecord3.HealthClubChargesAmount";
                }

                public LodgingRecord3()
                {
                }
            }

            public class LodgingRecord4
            {
                public readonly static string OtherServices;

                static LodgingRecord4()
                {
                    SLMResp.Batch.LodgingRecord4.OtherServices = "LodgingRecord4.OtherServices";
                }

                public LodgingRecord4()
                {
                }
            }

            public class MasterCardBIN
            {
                public MasterCardBIN()
                {
                }

                public class MasterCardBINDetail
                {
                    public readonly static string RecordType;

                    public readonly static string LowRange;

                    public readonly static string HighRange;

                    public readonly static string ConsumerDigitalPaymentTokenIndicator;

                    static MasterCardBINDetail()
                    {
                        SLMResp.Batch.MasterCardBIN.MasterCardBINDetail.RecordType = "MasterCardBINDetail.RecordType";
                        SLMResp.Batch.MasterCardBIN.MasterCardBINDetail.LowRange = "MasterCardBINDetail.LowRange";
                        SLMResp.Batch.MasterCardBIN.MasterCardBINDetail.HighRange = "MasterCardBINDetail.HighRange";
                        SLMResp.Batch.MasterCardBIN.MasterCardBINDetail.ConsumerDigitalPaymentTokenIndicator = "MasterCardBINDetail.ConsumerDigitalPaymentTokenIndicator";
                    }

                    public MasterCardBINDetail()
                    {
                    }
                }

                public class MasterCardBINHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string VersionNumber;

                    public readonly static string CreationDate;

                    static MasterCardBINHeader()
                    {
                        SLMResp.Batch.MasterCardBIN.MasterCardBINHeader.RecordType = "MasterCardBINHeader.RecordType";
                        SLMResp.Batch.MasterCardBIN.MasterCardBINHeader.FileName = "MasterCardBINHeader.FileName";
                        SLMResp.Batch.MasterCardBIN.MasterCardBINHeader.VersionNumber = "MasterCardBINHeader.VersionNumber";
                        SLMResp.Batch.MasterCardBIN.MasterCardBINHeader.CreationDate = "MasterCardBINHeader.CreationDate";
                    }

                    public MasterCardBINHeader()
                    {
                    }
                }

                public class MasterCardBINTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDate;

                    public readonly static string NumberOfBinRecords;

                    static MasterCardBINTrailer()
                    {
                        SLMResp.Batch.MasterCardBIN.MasterCardBINTrailer.RecordType = "MasterCardBINTrailer.RecordType";
                        SLMResp.Batch.MasterCardBIN.MasterCardBINTrailer.CreationDate = "MasterCardBINTrailer.CreationDate";
                        SLMResp.Batch.MasterCardBIN.MasterCardBINTrailer.NumberOfBinRecords = "MasterCardBINTrailer.NumberOfBinRecords";
                    }

                    public MasterCardBINTrailer()
                    {
                    }
                }
            }

            public class MasterCardDigitalWallet
            {
                public readonly static string RecordType;

                public readonly static string DigitalWalletIndicator;

                public readonly static string WalletIndicator;

                public readonly static string MasterPassDigitalWalletID;

                static MasterCardDigitalWallet()
                {
                    SLMResp.Batch.MasterCardDigitalWallet.RecordType = "MasterCardDigitalWallet.RecordType";
                    SLMResp.Batch.MasterCardDigitalWallet.DigitalWalletIndicator = "MasterCardDigitalWallet.DigitalWalletIndicator";
                    SLMResp.Batch.MasterCardDigitalWallet.WalletIndicator = "MasterCardDigitalWallet.WalletIndicator";
                    SLMResp.Batch.MasterCardDigitalWallet.MasterPassDigitalWalletID = "MasterCardDigitalWallet.MasterPassDigitalWalletID";
                }

                public MasterCardDigitalWallet()
                {
                }
            }

            public class MasterCardExtRecord1
            {
                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string POSAuthSource;

                public readonly static string POSCardIDMethod;

                public readonly static string FinancialNetworkCode;

                public readonly static string BankNetReferenceNumber;

                public readonly static string AuthorizedAmount;

                public readonly static string BankNetDate;

                public readonly static string MerchantCategoryCode;

                public readonly static string CATType;

                public readonly static string TransactionCategoryIndicator;

                public readonly static string AutomatedFuelDispenserCompletionAdviceFlag;

                public readonly static string UCAFCollectionIndicator;

                public readonly static string CardholderPresent;

                public readonly static string CardPresent;

                public readonly static string AuthType;

                public readonly static string TransactionIntegrityClassification;

                public readonly static string BankNetRefNumber;

                static MasterCardExtRecord1()
                {
                    SLMResp.Batch.MasterCardExtRecord1.POSCapabilityCode = "MasterCardExtRecord1.POSCapabilityCode";
                    SLMResp.Batch.MasterCardExtRecord1.POSEntryMode = "MasterCardExtRecord1.POSEntryMode";
                    SLMResp.Batch.MasterCardExtRecord1.POSAuthSource = "MasterCardExtRecord1.POSAuthSource";
                    SLMResp.Batch.MasterCardExtRecord1.POSCardIDMethod = "MasterCardExtRecord1.POSCardIDMethod";
                    SLMResp.Batch.MasterCardExtRecord1.FinancialNetworkCode = "MasterCardExtRecord1.FinancialNetworkCode";
                    SLMResp.Batch.MasterCardExtRecord1.BankNetReferenceNumber = "MasterCardExtRecord1.BankNetReferenceNumber";
                    SLMResp.Batch.MasterCardExtRecord1.AuthorizedAmount = "MasterCardExtRecord1.AuthorizedAmount";
                    SLMResp.Batch.MasterCardExtRecord1.BankNetDate = "MasterCardExtRecord1.BankNetDate";
                    SLMResp.Batch.MasterCardExtRecord1.MerchantCategoryCode = "MasterCardExtRecord1.MerchantCategoryCode";
                    SLMResp.Batch.MasterCardExtRecord1.CATType = "MasterCardExtRecord1.CATType";
                    SLMResp.Batch.MasterCardExtRecord1.TransactionCategoryIndicator = "MasterCardExtRecord1.TransactionCategoryIndicator";
                    SLMResp.Batch.MasterCardExtRecord1.AutomatedFuelDispenserCompletionAdviceFlag = "MasterCardExtRecord1.AutomatedFuelDispenserCompletionAdviceFlag";
                    SLMResp.Batch.MasterCardExtRecord1.UCAFCollectionIndicator = "MasterCardExtRecord1.UCAFCollectionIndicator";
                    SLMResp.Batch.MasterCardExtRecord1.CardholderPresent = "MasterCardExtRecord1.CardholderPresent";
                    SLMResp.Batch.MasterCardExtRecord1.CardPresent = "MasterCardExtRecord1.CardPresent";
                    SLMResp.Batch.MasterCardExtRecord1.AuthType = "MasterCardExtRecord1.AuthType";
                    SLMResp.Batch.MasterCardExtRecord1.TransactionIntegrityClassification = "MasterCardExtRecord1.TransactionIntegrityClassification";
                    SLMResp.Batch.MasterCardExtRecord1.BankNetRefNumber = "MasterCardExtRecord1.BankNetRefNumber";
                }

                public MasterCardExtRecord1()
                {
                }
            }

            public class MasterCardExtRecord2
            {
                public readonly static string AcctAuthValue;

                static MasterCardExtRecord2()
                {
                    SLMResp.Batch.MasterCardExtRecord2.AcctAuthValue = "MasterCardExtRecord2.AcctAuthValue";
                }

                public MasterCardExtRecord2()
                {
                }
            }

            public class MasterCardExtRecord3
            {
                public readonly static string SubtypeFlag;

                static MasterCardExtRecord3()
                {
                    SLMResp.Batch.MasterCardExtRecord3.SubtypeFlag = "MasterCardExtRecord3.SubtypeFlag";
                }

                public MasterCardExtRecord3()
                {
                }
            }

            public class MasterCardLevel3LineItemRecord1
            {
                public readonly static string SeqNumber;

                public readonly static string Description;

                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string UnitOfMeasure;

                public readonly static string TaxAmount;

                public readonly static string TaxAmount1;

                public readonly static string TaxRate;

                public readonly static string TaxRate1;

                static MasterCardLevel3LineItemRecord1()
                {
                    SLMResp.Batch.MasterCardLevel3LineItemRecord1.SeqNumber = "MasterCardLevel3LineItemRecord1.SeqNumber";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord1.Description = "MasterCardLevel3LineItemRecord1.Description";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord1.ProductCode = "MasterCardLevel3LineItemRecord1.ProductCode";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord1.Quantity = "MasterCardLevel3LineItemRecord1.Quantity";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord1.UnitOfMeasure = "MasterCardLevel3LineItemRecord1.UnitOfMeasure";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord1.TaxAmount = "MasterCardLevel3LineItemRecord1.TaxAmount";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord1.TaxAmount1 = "MasterCardLevel3LineItemRecord1.TaxAmount1";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord1.TaxRate = "MasterCardLevel3LineItemRecord1.TaxRate";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord1.TaxRate1 = "MasterCardLevel3LineItemRecord1.TaxRate1";
                }

                public MasterCardLevel3LineItemRecord1()
                {
                }
            }

            public class MasterCardLevel3LineItemRecord2
            {
                public readonly static string SeqNumber;

                public readonly static string LineItemTotal;

                public readonly static string DiscountAmt;

                public readonly static string GrossNetIndicator;

                public readonly static string TaxType;

                public readonly static string TaxTypeApplied1;

                public readonly static string DiscountIndicator;

                public readonly static string ItemCommodityCode;

                public readonly static string UnitCost;

                public readonly static string TaxAmount2;

                public readonly static string TaxRate2;

                public readonly static string TaxTypeApplied2;

                static MasterCardLevel3LineItemRecord2()
                {
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.SeqNumber = "MasterCardLevel3LineItemRecord2.SeqNumber";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.LineItemTotal = "MasterCardLevel3LineItemRecord2.LineItemTotal";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.DiscountAmt = "MasterCardLevel3LineItemRecord2.DiscountAmt";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.GrossNetIndicator = "MasterCardLevel3LineItemRecord2.GrossNetIndicator";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.TaxType = "MasterCardLevel3LineItemRecord2.TaxType";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.TaxTypeApplied1 = "MasterCardLevel3LineItemRecord2.TaxTypeApplied1";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.DiscountIndicator = "MasterCardLevel3LineItemRecord2.DiscountIndicator";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.ItemCommodityCode = "MasterCardLevel3LineItemRecord2.ItemCommodityCode";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.UnitCost = "MasterCardLevel3LineItemRecord2.UnitCost";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.TaxAmount2 = "MasterCardLevel3LineItemRecord2.TaxAmount2";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.TaxRate2 = "MasterCardLevel3LineItemRecord2.TaxRate2";
                    SLMResp.Batch.MasterCardLevel3LineItemRecord2.TaxTypeApplied2 = "MasterCardLevel3LineItemRecord2.TaxTypeApplied2";
                }

                public MasterCardLevel3LineItemRecord2()
                {
                }
            }

            public class MasterCardLevel3OrderRecord
            {
                public readonly static string FreightAmount;

                public readonly static string DutyAmount;

                public readonly static string DestinationPostalCode;

                public readonly static string DestinationCountryCode;

                public readonly static string ShipFromPostalCode;

                public readonly static string AlternateTaxID;

                public readonly static string AlternateTaxAmt;

                public readonly static string VATTaxAmount;

                static MasterCardLevel3OrderRecord()
                {
                    SLMResp.Batch.MasterCardLevel3OrderRecord.FreightAmount = "MasterCardLevel3OrderRecord.FreightAmount";
                    SLMResp.Batch.MasterCardLevel3OrderRecord.DutyAmount = "MasterCardLevel3OrderRecord.DutyAmount";
                    SLMResp.Batch.MasterCardLevel3OrderRecord.DestinationPostalCode = "MasterCardLevel3OrderRecord.DestinationPostalCode";
                    SLMResp.Batch.MasterCardLevel3OrderRecord.DestinationCountryCode = "MasterCardLevel3OrderRecord.DestinationCountryCode";
                    SLMResp.Batch.MasterCardLevel3OrderRecord.ShipFromPostalCode = "MasterCardLevel3OrderRecord.ShipFromPostalCode";
                    SLMResp.Batch.MasterCardLevel3OrderRecord.AlternateTaxID = "MasterCardLevel3OrderRecord.AlternateTaxID";
                    SLMResp.Batch.MasterCardLevel3OrderRecord.AlternateTaxAmt = "MasterCardLevel3OrderRecord.AlternateTaxAmt";
                    SLMResp.Batch.MasterCardLevel3OrderRecord.VATTaxAmount = "MasterCardLevel3OrderRecord.VATTaxAmount";
                }

                public MasterCardLevel3OrderRecord()
                {
                }
            }

            public class MCMCDLevel3LineItemRecord1
            {
                public readonly static string SeqNumber;

                public readonly static string Description;

                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string UnitOfMeasure;

                public readonly static string TaxAmount;

                public readonly static string TaxAmount1;

                public readonly static string TaxRate;

                public readonly static string TaxRate1;

                static MCMCDLevel3LineItemRecord1()
                {
                    SLMResp.Batch.MCMCDLevel3LineItemRecord1.SeqNumber = "MCMCDLevel3LineItemRecord1.SeqNumber";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord1.Description = "MCMCDLevel3LineItemRecord1.Description";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord1.ProductCode = "MCMCDLevel3LineItemRecord1.ProductCode";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord1.Quantity = "MCMCDLevel3LineItemRecord1.Quantity";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord1.UnitOfMeasure = "MCMCDLevel3LineItemRecord1.UnitOfMeasure";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord1.TaxAmount = "MCMCDLevel3LineItemRecord1.TaxAmount";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord1.TaxAmount1 = "MCMCDLevel3LineItemRecord1.TaxAmount1";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord1.TaxRate = "MCMCDLevel3LineItemRecord1.TaxRate";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord1.TaxRate1 = "MCMCDLevel3LineItemRecord1.TaxRate1";
                }

                public MCMCDLevel3LineItemRecord1()
                {
                }
            }

            public class MCMCDLevel3LineItemRecord2
            {
                public readonly static string SeqNumber;

                public readonly static string LineItemTotal;

                public readonly static string DiscountAmt;

                public readonly static string GrossNetIndicator;

                public readonly static string TaxType;

                public readonly static string TaxTypeApplied1;

                public readonly static string DiscountIndicator;

                public readonly static string ItemCommodityCode;

                public readonly static string UnitCost;

                public readonly static string TaxAmount2;

                public readonly static string TaxRate2;

                public readonly static string TaxTypeApplied2;

                static MCMCDLevel3LineItemRecord2()
                {
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.SeqNumber = "MCMCDLevel3LineItemRecord2.SeqNumber";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.LineItemTotal = "MCMCDLevel3LineItemRecord2.LineItemTotal";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.DiscountAmt = "MCMCDLevel3LineItemRecord2.DiscountAmt";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.GrossNetIndicator = "MCMCDLevel3LineItemRecord2.GrossNetIndicator";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.TaxType = "MCMCDLevel3LineItemRecord2.TaxType";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.TaxTypeApplied1 = "MCMCDLevel3LineItemRecord2.TaxTypeApplied1";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.DiscountIndicator = "MCMCDLevel3LineItemRecord2.DiscountIndicator";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.ItemCommodityCode = "MCMCDLevel3LineItemRecord2.ItemCommodityCode";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.UnitCost = "MCMCDLevel3LineItemRecord2.UnitCost";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.TaxAmount2 = "MCMCDLevel3LineItemRecord2.TaxAmount2";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.TaxRate2 = "MCMCDLevel3LineItemRecord2.TaxRate2";
                    SLMResp.Batch.MCMCDLevel3LineItemRecord2.TaxTypeApplied2 = "MCMCDLevel3LineItemRecord2.TaxTypeApplied2";
                }

                public MCMCDLevel3LineItemRecord2()
                {
                }
            }

            public class MCMCDLevel3OrderRecord
            {
                public readonly static string FreightAmount;

                public readonly static string DutyAmount;

                public readonly static string DestinationPostalCode;

                public readonly static string DestinationCountryCode;

                public readonly static string ShipFromPostalCode;

                public readonly static string AlternateTaxID;

                public readonly static string AlternateTaxAmt;

                public readonly static string VATTaxAmount;

                static MCMCDLevel3OrderRecord()
                {
                    SLMResp.Batch.MCMCDLevel3OrderRecord.FreightAmount = "MCMCDLevel3OrderRecord.FreightAmount";
                    SLMResp.Batch.MCMCDLevel3OrderRecord.DutyAmount = "MCMCDLevel3OrderRecord.DutyAmount";
                    SLMResp.Batch.MCMCDLevel3OrderRecord.DestinationPostalCode = "MCMCDLevel3OrderRecord.DestinationPostalCode";
                    SLMResp.Batch.MCMCDLevel3OrderRecord.DestinationCountryCode = "MCMCDLevel3OrderRecord.DestinationCountryCode";
                    SLMResp.Batch.MCMCDLevel3OrderRecord.ShipFromPostalCode = "MCMCDLevel3OrderRecord.ShipFromPostalCode";
                    SLMResp.Batch.MCMCDLevel3OrderRecord.AlternateTaxID = "MCMCDLevel3OrderRecord.AlternateTaxID";
                    SLMResp.Batch.MCMCDLevel3OrderRecord.AlternateTaxAmt = "MCMCDLevel3OrderRecord.AlternateTaxAmt";
                    SLMResp.Batch.MCMCDLevel3OrderRecord.VATTaxAmount = "MCMCDLevel3OrderRecord.VATTaxAmount";
                }

                public MCMCDLevel3OrderRecord()
                {
                }
            }

            public class MerchantDescriptorCC
            {
                public readonly static string MerchantNameItemDescription;

                public readonly static string MerchantCityCSRPhone;

                public readonly static string MerchantState;

                static MerchantDescriptorCC()
                {
                    SLMResp.Batch.MerchantDescriptorCC.MerchantNameItemDescription = "MerchantDescriptorCC.MerchantNameItemDescription";
                    SLMResp.Batch.MerchantDescriptorCC.MerchantCityCSRPhone = "MerchantDescriptorCC.MerchantCityCSRPhone";
                    SLMResp.Batch.MerchantDescriptorCC.MerchantState = "MerchantDescriptorCC.MerchantState";
                }

                public MerchantDescriptorCC()
                {
                }
            }

            public class MerchantDescriptorECP
            {
                public readonly static string MerchantName;

                public readonly static string EntryDescription;

                static MerchantDescriptorECP()
                {
                    SLMResp.Batch.MerchantDescriptorECP.MerchantName = "MerchantDescriptorECP.MerchantName";
                    SLMResp.Batch.MerchantDescriptorECP.EntryDescription = "MerchantDescriptorECP.EntryDescription";
                }

                public MerchantDescriptorECP()
                {
                }
            }

            public class MoneyPakRecord1
            {
                public readonly static string OriginalTransactionID;

                public readonly static string ConfirmationID;

                public readonly static string MoneyPakAccountNumber;

                static MoneyPakRecord1()
                {
                    SLMResp.Batch.MoneyPakRecord1.OriginalTransactionID = "MoneyPakRecord1.OriginalTransactionID";
                    SLMResp.Batch.MoneyPakRecord1.ConfirmationID = "MoneyPakRecord1.ConfirmationID";
                    SLMResp.Batch.MoneyPakRecord1.MoneyPakAccountNumber = "MoneyPakRecord1.MoneyPakAccountNumber";
                }

                public MoneyPakRecord1()
                {
                }
            }

            public class NoData
            {
                public NoData()
                {
                }
            }

            public class Order
            {
                public readonly static string DivisionNumber;

                public readonly static string MerchantOrderNumber;

                public readonly static string ActionCode;

                public readonly static string MethodOfPayment;

                public readonly static string AccountNumber;

                public readonly static string ExpirationDate;

                public readonly static string Amount;

                public readonly static string CurrencyCode;

                public readonly static string ResponseReasonCode;

                public readonly static string TransactionType;

                public readonly static string CardSecurityValueResponse;

                public readonly static string ResponseDate;

                public readonly static string AuthVerCode;

                public readonly static string AVSAAVRespCode;

                public readonly static string DepositFlag;

                public readonly static string BillPayIndicator;

                public readonly static string EncryptionFlag;

                public readonly static string RecurringPaymentAdviceCode;

                public readonly static string PaymentAdviceCode;

                public readonly static string PartialAuthFlag;

                public readonly static string SplitTenderIndicator;

                public readonly static string ReversalReasonCode;

                public readonly static string MerchantSpace;

                static Order()
                {
                    SLMResp.Batch.Order.DivisionNumber = "DivisionNumber";
                    SLMResp.Batch.Order.MerchantOrderNumber = "MerchantOrderNumber";
                    SLMResp.Batch.Order.ActionCode = "ActionCode";
                    SLMResp.Batch.Order.MethodOfPayment = "MethodOfPayment";
                    SLMResp.Batch.Order.AccountNumber = "AccountNumber";
                    SLMResp.Batch.Order.ExpirationDate = "ExpirationDate";
                    SLMResp.Batch.Order.Amount = "Amount";
                    SLMResp.Batch.Order.CurrencyCode = "CurrencyCode";
                    SLMResp.Batch.Order.ResponseReasonCode = "ResponseReasonCode";
                    SLMResp.Batch.Order.TransactionType = "TransactionType";
                    SLMResp.Batch.Order.CardSecurityValueResponse = "CardSecurityValueResponse";
                    SLMResp.Batch.Order.ResponseDate = "ResponseDate";
                    SLMResp.Batch.Order.AuthVerCode = "AuthVerCode";
                    SLMResp.Batch.Order.AVSAAVRespCode = "AVSAAVRespCode";
                    SLMResp.Batch.Order.DepositFlag = "DepositFlag";
                    SLMResp.Batch.Order.BillPayIndicator = "BillPayIndicator";
                    SLMResp.Batch.Order.EncryptionFlag = "EncryptionFlag";
                    SLMResp.Batch.Order.RecurringPaymentAdviceCode = "RecurringPaymentAdviceCode";
                    SLMResp.Batch.Order.PaymentAdviceCode = "PaymentAdviceCode";
                    SLMResp.Batch.Order.PartialAuthFlag = "PartialAuthFlag";
                    SLMResp.Batch.Order.SplitTenderIndicator = "SplitTenderIndicator";
                    SLMResp.Batch.Order.ReversalReasonCode = "ReversalReasonCode";
                    SLMResp.Batch.Order.MerchantSpace = "MerchantSpace";
                }

                public Order()
                {
                }
            }

            public class OrderInfoRecord1
            {
                public readonly static string ProductDeliveryTypeIndicator;

                public readonly static string ShippingCarrier;

                public readonly static string ShippingMethod;

                public readonly static string OrderDate;

                public readonly static string OrderTime;

                public readonly static string TrackingNumber;

                public readonly static string MCC;

                public readonly static string SKU;

                public readonly static string NumberOfShipments;

                public readonly static string ShipperCode;

                static OrderInfoRecord1()
                {
                    SLMResp.Batch.OrderInfoRecord1.ProductDeliveryTypeIndicator = "OrderInfoRecord1.ProductDeliveryTypeIndicator";
                    SLMResp.Batch.OrderInfoRecord1.ShippingCarrier = "OrderInfoRecord1.ShippingCarrier";
                    SLMResp.Batch.OrderInfoRecord1.ShippingMethod = "OrderInfoRecord1.ShippingMethod";
                    SLMResp.Batch.OrderInfoRecord1.OrderDate = "OrderInfoRecord1.OrderDate";
                    SLMResp.Batch.OrderInfoRecord1.OrderTime = "OrderInfoRecord1.OrderTime";
                    SLMResp.Batch.OrderInfoRecord1.TrackingNumber = "OrderInfoRecord1.TrackingNumber";
                    SLMResp.Batch.OrderInfoRecord1.MCC = "OrderInfoRecord1.MCC";
                    SLMResp.Batch.OrderInfoRecord1.SKU = "OrderInfoRecord1.SKU";
                    SLMResp.Batch.OrderInfoRecord1.NumberOfShipments = "OrderInfoRecord1.NumberOfShipments";
                    SLMResp.Batch.OrderInfoRecord1.ShipperCode = "OrderInfoRecord1.ShipperCode";
                }

                public OrderInfoRecord1()
                {
                }
            }

            public class PartialAuthRecord1
            {
                public readonly static string PartialRedemptionIndicatorFlag;

                public readonly static string CurrentBalance;

                public readonly static string RedemptionAmount;

                static PartialAuthRecord1()
                {
                    SLMResp.Batch.PartialAuthRecord1.PartialRedemptionIndicatorFlag = "PartialAuthRecord1.PartialRedemptionIndicatorFlag";
                    SLMResp.Batch.PartialAuthRecord1.CurrentBalance = "PartialAuthRecord1.CurrentBalance";
                    SLMResp.Batch.PartialAuthRecord1.RedemptionAmount = "PartialAuthRecord1.RedemptionAmount";
                }

                public PartialAuthRecord1()
                {
                }
            }

            public class PassengerTransExtRecord3
            {
                public readonly static string ClearingSequenceNumber;

                public readonly static string ClearingCount;

                public readonly static string TotalClearingAmount;

                public readonly static string ComputerizedReservationSystem;

                static PassengerTransExtRecord3()
                {
                    SLMResp.Batch.PassengerTransExtRecord3.ClearingSequenceNumber = "PassengerTransExtRecord3.ClearingSequenceNumber";
                    SLMResp.Batch.PassengerTransExtRecord3.ClearingCount = "PassengerTransExtRecord3.ClearingCount";
                    SLMResp.Batch.PassengerTransExtRecord3.TotalClearingAmount = "PassengerTransExtRecord3.TotalClearingAmount";
                    SLMResp.Batch.PassengerTransExtRecord3.ComputerizedReservationSystem = "PassengerTransExtRecord3.ComputerizedReservationSystem";
                }

                public PassengerTransExtRecord3()
                {
                }
            }

            public class PassengerTransTicInfo1
            {
                public readonly static string TicketNumber;

                public readonly static string PassengerName;

                public readonly static string CustomerCode;

                public readonly static string IssueDate;

                public readonly static string IssuingCarrier;

                public readonly static string ArrivalDate;

                public readonly static string NumberInParty;

                public readonly static string ConjunctionTicketInd;

                public readonly static string ElectronicTicketInd;

                public readonly static string RestrictedTicketInd;

                public readonly static string IATAClientCode;

                public readonly static string CreditReasonIndicator;

                public readonly static string TicketChangeIndicator;

                static PassengerTransTicInfo1()
                {
                    SLMResp.Batch.PassengerTransTicInfo1.TicketNumber = "PassengerTransTicInfo1.TicketNumber";
                    SLMResp.Batch.PassengerTransTicInfo1.PassengerName = "PassengerTransTicInfo1.PassengerName";
                    SLMResp.Batch.PassengerTransTicInfo1.CustomerCode = "PassengerTransTicInfo1.CustomerCode";
                    SLMResp.Batch.PassengerTransTicInfo1.IssueDate = "PassengerTransTicInfo1.IssueDate";
                    SLMResp.Batch.PassengerTransTicInfo1.IssuingCarrier = "PassengerTransTicInfo1.IssuingCarrier";
                    SLMResp.Batch.PassengerTransTicInfo1.ArrivalDate = "PassengerTransTicInfo1.ArrivalDate";
                    SLMResp.Batch.PassengerTransTicInfo1.NumberInParty = "PassengerTransTicInfo1.NumberInParty";
                    SLMResp.Batch.PassengerTransTicInfo1.ConjunctionTicketInd = "PassengerTransTicInfo1.ConjunctionTicketInd";
                    SLMResp.Batch.PassengerTransTicInfo1.ElectronicTicketInd = "PassengerTransTicInfo1.ElectronicTicketInd";
                    SLMResp.Batch.PassengerTransTicInfo1.RestrictedTicketInd = "PassengerTransTicInfo1.RestrictedTicketInd";
                    SLMResp.Batch.PassengerTransTicInfo1.IATAClientCode = "PassengerTransTicInfo1.IATAClientCode";
                    SLMResp.Batch.PassengerTransTicInfo1.CreditReasonIndicator = "PassengerTransTicInfo1.CreditReasonIndicator";
                    SLMResp.Batch.PassengerTransTicInfo1.TicketChangeIndicator = "PassengerTransTicInfo1.TicketChangeIndicator";
                }

                public PassengerTransTicInfo1()
                {
                }
            }

            public class PassengerTransTicInfo2
            {
                public readonly static string TotalFare;

                public readonly static string TotalFees;

                public readonly static string TotalTaxes;

                public readonly static string ExchangeTicketAmount;

                public readonly static string ExchangeFeeAmount;

                public readonly static string InvoiceNumber;

                static PassengerTransTicInfo2()
                {
                    SLMResp.Batch.PassengerTransTicInfo2.TotalFare = "PassengerTransTicInfo2.TotalFare";
                    SLMResp.Batch.PassengerTransTicInfo2.TotalFees = "PassengerTransTicInfo2.TotalFees";
                    SLMResp.Batch.PassengerTransTicInfo2.TotalTaxes = "PassengerTransTicInfo2.TotalTaxes";
                    SLMResp.Batch.PassengerTransTicInfo2.ExchangeTicketAmount = "PassengerTransTicInfo2.ExchangeTicketAmount";
                    SLMResp.Batch.PassengerTransTicInfo2.ExchangeFeeAmount = "PassengerTransTicInfo2.ExchangeFeeAmount";
                    SLMResp.Batch.PassengerTransTicInfo2.InvoiceNumber = "PassengerTransTicInfo2.InvoiceNumber";
                }

                public PassengerTransTicInfo2()
                {
                }
            }

            public class PassengerTransTicInfo3
            {
                public readonly static string TravelAgencyCode;

                public readonly static string TravelAgencyName;

                public readonly static string TravelAuthorizationCode;

                static PassengerTransTicInfo3()
                {
                    SLMResp.Batch.PassengerTransTicInfo3.TravelAgencyCode = "PassengerTransTicInfo3.TravelAgencyCode";
                    SLMResp.Batch.PassengerTransTicInfo3.TravelAgencyName = "PassengerTransTicInfo3.TravelAgencyName";
                    SLMResp.Batch.PassengerTransTicInfo3.TravelAuthorizationCode = "PassengerTransTicInfo3.TravelAuthorizationCode";
                }

                public PassengerTransTicInfo3()
                {
                }
            }

            public class PassengerTransTicInfo4
            {
                public readonly static string AncillaryServiceCat1;

                public readonly static string AncillaryServiceSubCat1;

                public readonly static string AncillaryServiceCat2;

                public readonly static string AncillaryServiceSubCat2;

                public readonly static string AncillaryServiceCat3;

                public readonly static string AncillaryServiceSubCat3;

                public readonly static string AncillaryServiceCat4;

                public readonly static string AncillaryServiceSubCat4;

                public readonly static string Description;

                public readonly static string AssociatedTicketNumber;

                static PassengerTransTicInfo4()
                {
                    SLMResp.Batch.PassengerTransTicInfo4.AncillaryServiceCat1 = "PassengerTransTicInfo4.AncillaryServiceCat1";
                    SLMResp.Batch.PassengerTransTicInfo4.AncillaryServiceSubCat1 = "PassengerTransTicInfo4.AncillaryServiceSubCat1";
                    SLMResp.Batch.PassengerTransTicInfo4.AncillaryServiceCat2 = "PassengerTransTicInfo4.AncillaryServiceCat2";
                    SLMResp.Batch.PassengerTransTicInfo4.AncillaryServiceSubCat2 = "PassengerTransTicInfo4.AncillaryServiceSubCat2";
                    SLMResp.Batch.PassengerTransTicInfo4.AncillaryServiceCat3 = "PassengerTransTicInfo4.AncillaryServiceCat3";
                    SLMResp.Batch.PassengerTransTicInfo4.AncillaryServiceSubCat3 = "PassengerTransTicInfo4.AncillaryServiceSubCat3";
                    SLMResp.Batch.PassengerTransTicInfo4.AncillaryServiceCat4 = "PassengerTransTicInfo4.AncillaryServiceCat4";
                    SLMResp.Batch.PassengerTransTicInfo4.AncillaryServiceSubCat4 = "PassengerTransTicInfo4.AncillaryServiceSubCat4";
                    SLMResp.Batch.PassengerTransTicInfo4.Description = "PassengerTransTicInfo4.Description";
                    SLMResp.Batch.PassengerTransTicInfo4.AssociatedTicketNumber = "PassengerTransTicInfo4.AssociatedTicketNumber";
                }

                public PassengerTransTicInfo4()
                {
                }
            }

            public class PassengerTransTrip1
            {
                public readonly static string SequenceNumber;

                public readonly static string ConjunctionTicketNumber;

                public readonly static string ExchangeTicketNumber;

                public readonly static string CouponNumber;

                public readonly static string ServiceClass;

                public readonly static string CarrierCode;

                public readonly static string StopOverCode;

                public readonly static string CityOfOriginAirportCode;

                public readonly static string CityOfDestinationAirportCode;

                public readonly static string FlightNumber;

                public readonly static string DepartureDate;

                public readonly static string DepartureTime;

                public readonly static string ArrivalTime;

                public readonly static string FareBasisCode;

                static PassengerTransTrip1()
                {
                    SLMResp.Batch.PassengerTransTrip1.SequenceNumber = "PassengerTransTrip1.SequenceNumber";
                    SLMResp.Batch.PassengerTransTrip1.ConjunctionTicketNumber = "PassengerTransTrip1.ConjunctionTicketNumber";
                    SLMResp.Batch.PassengerTransTrip1.ExchangeTicketNumber = "PassengerTransTrip1.ExchangeTicketNumber";
                    SLMResp.Batch.PassengerTransTrip1.CouponNumber = "PassengerTransTrip1.CouponNumber";
                    SLMResp.Batch.PassengerTransTrip1.ServiceClass = "PassengerTransTrip1.ServiceClass";
                    SLMResp.Batch.PassengerTransTrip1.CarrierCode = "PassengerTransTrip1.CarrierCode";
                    SLMResp.Batch.PassengerTransTrip1.StopOverCode = "PassengerTransTrip1.StopOverCode";
                    SLMResp.Batch.PassengerTransTrip1.CityOfOriginAirportCode = "PassengerTransTrip1.CityOfOriginAirportCode";
                    SLMResp.Batch.PassengerTransTrip1.CityOfDestinationAirportCode = "PassengerTransTrip1.CityOfDestinationAirportCode";
                    SLMResp.Batch.PassengerTransTrip1.FlightNumber = "PassengerTransTrip1.FlightNumber";
                    SLMResp.Batch.PassengerTransTrip1.DepartureDate = "PassengerTransTrip1.DepartureDate";
                    SLMResp.Batch.PassengerTransTrip1.DepartureTime = "PassengerTransTrip1.DepartureTime";
                    SLMResp.Batch.PassengerTransTrip1.ArrivalTime = "PassengerTransTrip1.ArrivalTime";
                    SLMResp.Batch.PassengerTransTrip1.FareBasisCode = "PassengerTransTrip1.FareBasisCode";
                }

                public PassengerTransTrip1()
                {
                }
            }

            public class PassengerTransTrip2
            {
                public readonly static string SequenceNumber;

                public readonly static string TripLegFare;

                public readonly static string TripLegTaxes;

                public readonly static string TripLegFee;

                public readonly static string EndorsementsRestrictions;

                static PassengerTransTrip2()
                {
                    SLMResp.Batch.PassengerTransTrip2.SequenceNumber = "PassengerTransTrip2.SequenceNumber";
                    SLMResp.Batch.PassengerTransTrip2.TripLegFare = "PassengerTransTrip2.TripLegFare";
                    SLMResp.Batch.PassengerTransTrip2.TripLegTaxes = "PassengerTransTrip2.TripLegTaxes";
                    SLMResp.Batch.PassengerTransTrip2.TripLegFee = "PassengerTransTrip2.TripLegFee";
                    SLMResp.Batch.PassengerTransTrip2.EndorsementsRestrictions = "PassengerTransTrip2.EndorsementsRestrictions";
                }

                public PassengerTransTrip2()
                {
                }
            }

            public class PayPalExtRecord1
            {
                public readonly static string SubTypeFlag;

                public readonly static string ContractID;

                public readonly static string TransactionID;

                public readonly static string PayerID;

                static PayPalExtRecord1()
                {
                    SLMResp.Batch.PayPalExtRecord1.SubTypeFlag = "PayPalExtRecord1.SubTypeFlag";
                    SLMResp.Batch.PayPalExtRecord1.ContractID = "PayPalExtRecord1.ContractID";
                    SLMResp.Batch.PayPalExtRecord1.TransactionID = "PayPalExtRecord1.TransactionID";
                    SLMResp.Batch.PayPalExtRecord1.PayerID = "PayPalExtRecord1.PayerID";
                }

                public PayPalExtRecord1()
                {
                }
            }

            public class PayPalExtRecord2
            {
                public readonly static string ReceiverType;

                public readonly static string PaymentStatus;

                public readonly static string ReceiptID;

                static PayPalExtRecord2()
                {
                    SLMResp.Batch.PayPalExtRecord2.ReceiverType = "PayPalExtRecord2.ReceiverType";
                    SLMResp.Batch.PayPalExtRecord2.PaymentStatus = "PayPalExtRecord2.PaymentStatus";
                    SLMResp.Batch.PayPalExtRecord2.ReceiptID = "PayPalExtRecord2.ReceiptID";
                }

                public PayPalExtRecord2()
                {
                }
            }

            public class PayPalExtRecord3
            {
                public readonly static string SequenceNumber;

                public readonly static string ReceiverID;

                public readonly static string Amount;

                public readonly static string UniqueID;

                public readonly static string Note;

                static PayPalExtRecord3()
                {
                    SLMResp.Batch.PayPalExtRecord3.SequenceNumber = "PayPalExtRecord3.SequenceNumber";
                    SLMResp.Batch.PayPalExtRecord3.ReceiverID = "PayPalExtRecord3.ReceiverID";
                    SLMResp.Batch.PayPalExtRecord3.Amount = "PayPalExtRecord3.Amount";
                    SLMResp.Batch.PayPalExtRecord3.UniqueID = "PayPalExtRecord3.UniqueID";
                    SLMResp.Batch.PayPalExtRecord3.Note = "PayPalExtRecord3.Note";
                }

                public PayPalExtRecord3()
                {
                }
            }

            public class PersonalInfoRecord1
            {
                public readonly static string CustomerDOB;

                public readonly static string CustomerSSN;

                public readonly static string CurrencyTypeAnnualIncome;

                public readonly static string HouseholdAnnualIncome;

                public readonly static string CustomerResStatus;

                public readonly static string CustomerYrsAtRes;

                public readonly static string CustomerYrsAtEmployer;

                public readonly static string CustomerCheckAcct;

                public readonly static string CustomerSavingAcct;

                public readonly static string CustomerDrivLicNumber;

                public readonly static string CustomerDrivLicState;

                public readonly static string CustomerDrivLicCountry;

                static PersonalInfoRecord1()
                {
                    SLMResp.Batch.PersonalInfoRecord1.CustomerDOB = "PersonalInfoRecord1.CustomerDOB";
                    SLMResp.Batch.PersonalInfoRecord1.CustomerSSN = "PersonalInfoRecord1.CustomerSSN";
                    SLMResp.Batch.PersonalInfoRecord1.CurrencyTypeAnnualIncome = "PersonalInfoRecord1.CurrencyTypeAnnualIncome";
                    SLMResp.Batch.PersonalInfoRecord1.HouseholdAnnualIncome = "PersonalInfoRecord1.HouseholdAnnualIncome";
                    SLMResp.Batch.PersonalInfoRecord1.CustomerResStatus = "PersonalInfoRecord1.CustomerResStatus";
                    SLMResp.Batch.PersonalInfoRecord1.CustomerYrsAtRes = "PersonalInfoRecord1.CustomerYrsAtRes";
                    SLMResp.Batch.PersonalInfoRecord1.CustomerYrsAtEmployer = "PersonalInfoRecord1.CustomerYrsAtEmployer";
                    SLMResp.Batch.PersonalInfoRecord1.CustomerCheckAcct = "PersonalInfoRecord1.CustomerCheckAcct";
                    SLMResp.Batch.PersonalInfoRecord1.CustomerSavingAcct = "PersonalInfoRecord1.CustomerSavingAcct";
                    SLMResp.Batch.PersonalInfoRecord1.CustomerDrivLicNumber = "PersonalInfoRecord1.CustomerDrivLicNumber";
                    SLMResp.Batch.PersonalInfoRecord1.CustomerDrivLicState = "PersonalInfoRecord1.CustomerDrivLicState";
                    SLMResp.Batch.PersonalInfoRecord1.CustomerDrivLicCountry = "PersonalInfoRecord1.CustomerDrivLicCountry";
                }

                public PersonalInfoRecord1()
                {
                }
            }

            public class PinDebitBIN
            {
                public PinDebitBIN()
                {
                }

                public class DebitBINDetail
                {
                    public readonly static string RecordType;

                    public readonly static string BINLength;

                    public readonly static string BIN;

                    public readonly static string MinimumPANLength;

                    public readonly static string MaximumPANLength;

                    public readonly static string ConDigPayTokInd;

                    static DebitBINDetail()
                    {
                        SLMResp.Batch.PinDebitBIN.DebitBINDetail.RecordType = "DebitBINDetail.RecordType";
                        SLMResp.Batch.PinDebitBIN.DebitBINDetail.BINLength = "DebitBINDetail.BINLength";
                        SLMResp.Batch.PinDebitBIN.DebitBINDetail.BIN = "DebitBINDetail.BIN";
                        SLMResp.Batch.PinDebitBIN.DebitBINDetail.MinimumPANLength = "DebitBINDetail.MinimumPANLength";
                        SLMResp.Batch.PinDebitBIN.DebitBINDetail.MaximumPANLength = "DebitBINDetail.MaximumPANLength";
                        SLMResp.Batch.PinDebitBIN.DebitBINDetail.ConDigPayTokInd = "DebitBINDetail.ConDigPayTokInd";
                    }

                    public DebitBINDetail()
                    {
                    }
                }

                public class DebitBinHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string CreationDate;

                    static DebitBinHeader()
                    {
                        SLMResp.Batch.PinDebitBIN.DebitBinHeader.RecordType = "DebitBinHeader.RecordType";
                        SLMResp.Batch.PinDebitBIN.DebitBinHeader.FileName = "DebitBinHeader.FileName";
                        SLMResp.Batch.PinDebitBIN.DebitBinHeader.CreationDate = "DebitBinHeader.CreationDate";
                    }

                    public DebitBinHeader()
                    {
                    }
                }

                public class DebitBINTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDate;

                    public readonly static string NumberOfBINRecords;

                    static DebitBINTrailer()
                    {
                        SLMResp.Batch.PinDebitBIN.DebitBINTrailer.RecordType = "DebitBINTrailer.RecordType";
                        SLMResp.Batch.PinDebitBIN.DebitBINTrailer.CreationDate = "DebitBINTrailer.CreationDate";
                        SLMResp.Batch.PinDebitBIN.DebitBINTrailer.NumberOfBINRecords = "DebitBINTrailer.NumberOfBINRecords";
                    }

                    public DebitBINTrailer()
                    {
                    }
                }
            }

            public class PinlessDebitBIN
            {
                public PinlessDebitBIN()
                {
                }

                public class PNLSDBITDetail
                {
                    public readonly static string RecordType;

                    public readonly static string BINLength;

                    public readonly static string BIN;

                    public readonly static string MinimumPANLength;

                    public readonly static string MaximumPANLength;

                    public readonly static string PINlessNetworkIndicator;

                    static PNLSDBITDetail()
                    {
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITDetail.RecordType = "PNLSDBITDetail.RecordType";
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITDetail.BINLength = "PNLSDBITDetail.BINLength";
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITDetail.BIN = "PNLSDBITDetail.BIN";
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITDetail.MinimumPANLength = "PNLSDBITDetail.MinimumPANLength";
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITDetail.MaximumPANLength = "PNLSDBITDetail.MaximumPANLength";
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITDetail.PINlessNetworkIndicator = "PNLSDBITDetail.PINlessNetworkIndicator";
                    }

                    public PNLSDBITDetail()
                    {
                    }
                }

                public class PNLSDBITHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string CreationDate;

                    static PNLSDBITHeader()
                    {
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITHeader.RecordType = "PNLSDBITHeader.RecordType";
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITHeader.FileName = "PNLSDBITHeader.FileName";
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITHeader.CreationDate = "PNLSDBITHeader.CreationDate";
                    }

                    public PNLSDBITHeader()
                    {
                    }
                }

                public class PNLSDBITTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDate;

                    public readonly static string NumberOfBINRecords;

                    static PNLSDBITTrailer()
                    {
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITTrailer.RecordType = "PNLSDBITTrailer.RecordType";
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITTrailer.CreationDate = "PNLSDBITTrailer.CreationDate";
                        SLMResp.Batch.PinlessDebitBIN.PNLSDBITTrailer.NumberOfBINRecords = "PNLSDBITTrailer.NumberOfBINRecords";
                    }

                    public PNLSDBITTrailer()
                    {
                    }
                }
            }

            public class PostalCodeAddress
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static PostalCodeAddress()
                {
                    SLMResp.Batch.PostalCodeAddress.AddressLine = "PostalCodeAddress.AddressLine";
                    SLMResp.Batch.PostalCodeAddress.TelephoneType = "PostalCodeAddress.TelephoneType";
                    SLMResp.Batch.PostalCodeAddress.TelephoneNumber = "PostalCodeAddress.TelephoneNumber";
                    SLMResp.Batch.PostalCodeAddress.CountryCode = "PostalCodeAddress.CountryCode";
                }

                public PostalCodeAddress()
                {
                }
            }

            public class ProductRecordCrossCurrency
            {
                public readonly static string RecordType;

                public readonly static string OptOutIndicator;

                public readonly static string RateHandlingIndicator;

                public readonly static string RateIdentifier;

                public readonly static string ExchangeRate;

                public readonly static string PresentmentCurrency;

                public readonly static string SettlementCurrency;

                public readonly static string DefaultRateIndicator;

                public readonly static string RateStatus;

                static ProductRecordCrossCurrency()
                {
                    SLMResp.Batch.ProductRecordCrossCurrency.RecordType = "ProductRecordCrossCurrency.RecordType";
                    SLMResp.Batch.ProductRecordCrossCurrency.OptOutIndicator = "ProductRecordCrossCurrency.OptOutIndicator";
                    SLMResp.Batch.ProductRecordCrossCurrency.RateHandlingIndicator = "ProductRecordCrossCurrency.RateHandlingIndicator";
                    SLMResp.Batch.ProductRecordCrossCurrency.RateIdentifier = "ProductRecordCrossCurrency.RateIdentifier";
                    SLMResp.Batch.ProductRecordCrossCurrency.ExchangeRate = "ProductRecordCrossCurrency.ExchangeRate";
                    SLMResp.Batch.ProductRecordCrossCurrency.PresentmentCurrency = "ProductRecordCrossCurrency.PresentmentCurrency";
                    SLMResp.Batch.ProductRecordCrossCurrency.SettlementCurrency = "ProductRecordCrossCurrency.SettlementCurrency";
                    SLMResp.Batch.ProductRecordCrossCurrency.DefaultRateIndicator = "ProductRecordCrossCurrency.DefaultRateIndicator";
                    SLMResp.Batch.ProductRecordCrossCurrency.RateStatus = "ProductRecordCrossCurrency.RateStatus";
                }

                public ProductRecordCrossCurrency()
                {
                }
            }

            public class ProductRecordMasterCardDigitalWallet
            {
                public readonly static string RecordType;

                public readonly static string DigitalWalletIndicator;

                public readonly static string WalletIndicator;

                public readonly static string MasterPassDigitalWalletID;

                static ProductRecordMasterCardDigitalWallet()
                {
                    SLMResp.Batch.ProductRecordMasterCardDigitalWallet.RecordType = "ProductRecordMasterCardDigitalWallet.RecordType";
                    SLMResp.Batch.ProductRecordMasterCardDigitalWallet.DigitalWalletIndicator = "ProductRecordMasterCardDigitalWallet.DigitalWalletIndicator";
                    SLMResp.Batch.ProductRecordMasterCardDigitalWallet.WalletIndicator = "ProductRecordMasterCardDigitalWallet.WalletIndicator";
                    SLMResp.Batch.ProductRecordMasterCardDigitalWallet.MasterPassDigitalWalletID = "ProductRecordMasterCardDigitalWallet.MasterPassDigitalWalletID";
                }

                public ProductRecordMasterCardDigitalWallet()
                {
                }
            }

            public class ProductRecordMessageType
            {
                public readonly static string MessageType;

                public readonly static string StoredCredentialFlag;

                public readonly static string SubmittedTransactionID;

                public readonly static string ResponseTransactionID;

                public readonly static string OriginalTransactionAmount;

                static ProductRecordMessageType()
                {
                    SLMResp.Batch.ProductRecordMessageType.MessageType = "ProductRecordMessageType.MessageType";
                    SLMResp.Batch.ProductRecordMessageType.StoredCredentialFlag = "ProductRecordMessageType.StoredCredentialFlag";
                    SLMResp.Batch.ProductRecordMessageType.SubmittedTransactionID = "ProductRecordMessageType.SubmittedTransactionID";
                    SLMResp.Batch.ProductRecordMessageType.ResponseTransactionID = "ProductRecordMessageType.ResponseTransactionID";
                    SLMResp.Batch.ProductRecordMessageType.OriginalTransactionAmount = "ProductRecordMessageType.OriginalTransactionAmount";
                }

                public ProductRecordMessageType()
                {
                }
            }

            public class ProductRecordMobilePOSDeviceInformation
            {
                public readonly static string RecordType;

                public readonly static string DeviceType;

                public readonly static string PaymentDevice;

                static ProductRecordMobilePOSDeviceInformation()
                {
                    SLMResp.Batch.ProductRecordMobilePOSDeviceInformation.RecordType = "ProductRecordMobilePOSDeviceInformation.RecordType";
                    SLMResp.Batch.ProductRecordMobilePOSDeviceInformation.DeviceType = "ProductRecordMobilePOSDeviceInformation.DeviceType";
                    SLMResp.Batch.ProductRecordMobilePOSDeviceInformation.PaymentDevice = "ProductRecordMobilePOSDeviceInformation.PaymentDevice";
                }

                public ProductRecordMobilePOSDeviceInformation()
                {
                }
            }

            public class ProductRecordRecipientData
            {
                public readonly static string RecordType;

                public readonly static string DateofBirthofPrimaryRecipient;

                public readonly static string MaskedAccountNumber;

                public readonly static string PartialPostalCode;

                public readonly static string LastNameofPrimaryRecipient;

                static ProductRecordRecipientData()
                {
                    SLMResp.Batch.ProductRecordRecipientData.RecordType = "ProductRecordRecipientData.RecordType";
                    SLMResp.Batch.ProductRecordRecipientData.DateofBirthofPrimaryRecipient = "ProductRecordRecipientData.DateofBirthofPrimaryRecipient";
                    SLMResp.Batch.ProductRecordRecipientData.MaskedAccountNumber = "ProductRecordRecipientData.MaskedAccountNumber";
                    SLMResp.Batch.ProductRecordRecipientData.PartialPostalCode = "ProductRecordRecipientData.PartialPostalCode";
                    SLMResp.Batch.ProductRecordRecipientData.LastNameofPrimaryRecipient = "ProductRecordRecipientData.LastNameofPrimaryRecipient";
                }

                public ProductRecordRecipientData()
                {
                }
            }

            public class ProductRecordSafetechPageEncryption
            {
                public readonly static string SubscriberID;

                public readonly static string FormatID;

                public readonly static string IntegrityCheck;

                public readonly static string KeyID;

                public readonly static string PhaseID;

                static ProductRecordSafetechPageEncryption()
                {
                    SLMResp.Batch.ProductRecordSafetechPageEncryption.SubscriberID = "ProductRecordSafetechPageEncryption.SubscriberID";
                    SLMResp.Batch.ProductRecordSafetechPageEncryption.FormatID = "ProductRecordSafetechPageEncryption.FormatID";
                    SLMResp.Batch.ProductRecordSafetechPageEncryption.IntegrityCheck = "ProductRecordSafetechPageEncryption.IntegrityCheck";
                    SLMResp.Batch.ProductRecordSafetechPageEncryption.KeyID = "ProductRecordSafetechPageEncryption.KeyID";
                    SLMResp.Batch.ProductRecordSafetechPageEncryption.PhaseID = "ProductRecordSafetechPageEncryption.PhaseID";
                }

                public ProductRecordSafetechPageEncryption()
                {
                }
            }

            public class ProductRecordSplitShipment
            {
                public readonly static string SplitShipmentSequencNumber;

                public readonly static string SplitShipmentCount;

                public readonly static string PartialReversalAmount;

                public readonly static string ResponseReasonCode;

                static ProductRecordSplitShipment()
                {
                    SLMResp.Batch.ProductRecordSplitShipment.SplitShipmentSequencNumber = "ProductRecordSplitShipment.SplitShipmentSequencNumber";
                    SLMResp.Batch.ProductRecordSplitShipment.SplitShipmentCount = "ProductRecordSplitShipment.SplitShipmentCount";
                    SLMResp.Batch.ProductRecordSplitShipment.PartialReversalAmount = "ProductRecordSplitShipment.PartialReversalAmount";
                    SLMResp.Batch.ProductRecordSplitShipment.ResponseReasonCode = "ProductRecordSplitShipment.ResponseReasonCode";
                }

                public ProductRecordSplitShipment()
                {
                }
            }

            public class ProductRecordStagedDigitalWallet
            {
                public readonly static string RecordType;

                public readonly static string DigitalWalletIndicator;

                public readonly static string WalletIndicator;

                public readonly static string MasterPassDigitalWalletID;

                static ProductRecordStagedDigitalWallet()
                {
                    SLMResp.Batch.ProductRecordStagedDigitalWallet.RecordType = "ProductRecordStagedDigitalWallet.RecordType";
                    SLMResp.Batch.ProductRecordStagedDigitalWallet.DigitalWalletIndicator = "ProductRecordStagedDigitalWallet.DigitalWalletIndicator";
                    SLMResp.Batch.ProductRecordStagedDigitalWallet.WalletIndicator = "ProductRecordStagedDigitalWallet.WalletIndicator";
                    SLMResp.Batch.ProductRecordStagedDigitalWallet.MasterPassDigitalWalletID = "ProductRecordStagedDigitalWallet.MasterPassDigitalWalletID";
                }

                public ProductRecordStagedDigitalWallet()
                {
                }
            }

            public class ProductRecordTokenID
            {
                public readonly static string TokenID;

                static ProductRecordTokenID()
                {
                    SLMResp.Batch.ProductRecordTokenID.TokenID = "ProductRecordTokenID.TokenID";
                }

                public ProductRecordTokenID()
                {
                }
            }

            public class RealTimeAccountUpdaterType
            {
                public readonly static string UpdateResponse;

                public readonly static string BypassAccountUpdateRequest;

                public readonly static string NewAccountNumber;

                public readonly static string NewExpirationDate;

                public readonly static string NewMethodOfPayment;

                static RealTimeAccountUpdaterType()
                {
                    SLMResp.Batch.RealTimeAccountUpdaterType.UpdateResponse = "RealTimeAccountUpdaterType.UpdateResponse";
                    SLMResp.Batch.RealTimeAccountUpdaterType.BypassAccountUpdateRequest = "RealTimeAccountUpdaterType.BypassAccountUpdateRequest";
                    SLMResp.Batch.RealTimeAccountUpdaterType.NewAccountNumber = "RealTimeAccountUpdaterType.NewAccountNumber";
                    SLMResp.Batch.RealTimeAccountUpdaterType.NewExpirationDate = "RealTimeAccountUpdaterType.NewExpirationDate";
                    SLMResp.Batch.RealTimeAccountUpdaterType.NewMethodOfPayment = "RealTimeAccountUpdaterType.NewMethodOfPayment";
                }

                public RealTimeAccountUpdaterType()
                {
                }
            }

            public class ResponseInformation1
            {
                public readonly static string EnhancedVerificationFlag;

                public readonly static string AVSResponseCode;

                public readonly static string PostalCodeVerificationResponse;

                public readonly static string StreetVerificationResponse;

                public readonly static string NameVerificationResponse;

                public readonly static string PhoneVerificationResponse;

                public readonly static string EmailVerificationResponse;

                static ResponseInformation1()
                {
                    SLMResp.Batch.ResponseInformation1.EnhancedVerificationFlag = "ResponseInformation1.EnhancedVerificationFlag";
                    SLMResp.Batch.ResponseInformation1.AVSResponseCode = "ResponseInformation1.AVSResponseCode";
                    SLMResp.Batch.ResponseInformation1.PostalCodeVerificationResponse = "ResponseInformation1.PostalCodeVerificationResponse";
                    SLMResp.Batch.ResponseInformation1.StreetVerificationResponse = "ResponseInformation1.StreetVerificationResponse";
                    SLMResp.Batch.ResponseInformation1.NameVerificationResponse = "ResponseInformation1.NameVerificationResponse";
                    SLMResp.Batch.ResponseInformation1.PhoneVerificationResponse = "ResponseInformation1.PhoneVerificationResponse";
                    SLMResp.Batch.ResponseInformation1.EmailVerificationResponse = "ResponseInformation1.EmailVerificationResponse";
                }

                public ResponseInformation1()
                {
                }
            }

            public class ResponseMessageRecord
            {
                public readonly static string MessageText;

                static ResponseMessageRecord()
                {
                    SLMResp.Batch.ResponseMessageRecord.MessageText = "ResponseMessageRecord.MessageText";
                }

                public ResponseMessageRecord()
                {
                }
            }

            public class RetailRecord1
            {
                public readonly static string TerminalID;

                public readonly static string BatchID;

                public readonly static string BatchDate;

                public readonly static string CommunicationMethod;

                public readonly static string ReferenceNumber;

                public readonly static string CustomerData;

                public readonly static string CaptureType;

                public readonly static string GuaranteedID;

                static RetailRecord1()
                {
                    SLMResp.Batch.RetailRecord1.TerminalID = "RetailRecord1.TerminalID";
                    SLMResp.Batch.RetailRecord1.BatchID = "RetailRecord1.BatchID";
                    SLMResp.Batch.RetailRecord1.BatchDate = "RetailRecord1.BatchDate";
                    SLMResp.Batch.RetailRecord1.CommunicationMethod = "RetailRecord1.CommunicationMethod";
                    SLMResp.Batch.RetailRecord1.ReferenceNumber = "RetailRecord1.ReferenceNumber";
                    SLMResp.Batch.RetailRecord1.CustomerData = "RetailRecord1.CustomerData";
                    SLMResp.Batch.RetailRecord1.CaptureType = "RetailRecord1.CaptureType";
                    SLMResp.Batch.RetailRecord1.GuaranteedID = "RetailRecord1.GuaranteedID";
                }

                public RetailRecord1()
                {
                }
            }

            public class RevolutionCardRecord1
            {
                public readonly static string OneTimeTokenID;

                public readonly static string TraceNumber;

                public readonly static string TransactionID;

                static RevolutionCardRecord1()
                {
                    SLMResp.Batch.RevolutionCardRecord1.OneTimeTokenID = "RevolutionCardRecord1.OneTimeTokenID";
                    SLMResp.Batch.RevolutionCardRecord1.TraceNumber = "RevolutionCardRecord1.TraceNumber";
                    SLMResp.Batch.RevolutionCardRecord1.TransactionID = "RevolutionCardRecord1.TransactionID";
                }

                public RevolutionCardRecord1()
                {
                }
            }

            public class RFR
            {
                public readonly static string PID;

                public readonly static string PIDPassword;

                public readonly static string SID;

                public readonly static string SIDPassword;

                public readonly static string CreationDate;

                static RFR()
                {
                    SLMResp.Batch.RFR.PID = "RFR.PID";
                    SLMResp.Batch.RFR.PIDPassword = "RFR.PIDPassword";
                    SLMResp.Batch.RFR.SID = "RFR.SID";
                    SLMResp.Batch.RFR.SIDPassword = "RFR.SIDPassword";
                    SLMResp.Batch.RFR.CreationDate = "RFR.CreationDate";
                }

                public RFR()
                {
                }
            }

            public class RFSReport
            {
                public RFSReport()
                {
                }

                public class RFSDetail
                {
                    public readonly static string ReportType;

                    public readonly static string RecordSequenceNumber;

                    public readonly static string SubmissionID;

                    public readonly static string SubmitterSubmissionID;

                    public readonly static string SubmissionStatus;

                    public readonly static string SubmissionRecordCount;

                    public readonly static string SubmissionReceivedDate;

                    public readonly static string SubmissionReceivedTime;

                    public readonly static string ResponseFileReturned;

                    static RFSDetail()
                    {
                        SLMResp.Batch.RFSReport.RFSDetail.ReportType = "RFSDetail.ReportType";
                        SLMResp.Batch.RFSReport.RFSDetail.RecordSequenceNumber = "RFSDetail.RecordSequenceNumber";
                        SLMResp.Batch.RFSReport.RFSDetail.SubmissionID = "RFSDetail.SubmissionID";
                        SLMResp.Batch.RFSReport.RFSDetail.SubmitterSubmissionID = "RFSDetail.SubmitterSubmissionID";
                        SLMResp.Batch.RFSReport.RFSDetail.SubmissionStatus = "RFSDetail.SubmissionStatus";
                        SLMResp.Batch.RFSReport.RFSDetail.SubmissionRecordCount = "RFSDetail.SubmissionRecordCount";
                        SLMResp.Batch.RFSReport.RFSDetail.SubmissionReceivedDate = "RFSDetail.SubmissionReceivedDate";
                        SLMResp.Batch.RFSReport.RFSDetail.SubmissionReceivedTime = "RFSDetail.SubmissionReceivedTime";
                        SLMResp.Batch.RFSReport.RFSDetail.ResponseFileReturned = "RFSDetail.ResponseFileReturned";
                    }

                    public RFSDetail()
                    {
                    }
                }

                public class RFSHeader
                {
                    public readonly static string ReportType;

                    public readonly static string FileName;

                    public readonly static string CreationDate;

                    public readonly static string CreationTime;

                    public readonly static string Version;

                    public readonly static string PID;

                    public readonly static string StartDate;

                    public readonly static string Enddate;

                    static RFSHeader()
                    {
                        SLMResp.Batch.RFSReport.RFSHeader.ReportType = "RFSHeader.ReportType";
                        SLMResp.Batch.RFSReport.RFSHeader.FileName = "RFSHeader.FileName";
                        SLMResp.Batch.RFSReport.RFSHeader.CreationDate = "RFSHeader.CreationDate";
                        SLMResp.Batch.RFSReport.RFSHeader.CreationTime = "RFSHeader.CreationTime";
                        SLMResp.Batch.RFSReport.RFSHeader.Version = "RFSHeader.Version";
                        SLMResp.Batch.RFSReport.RFSHeader.PID = "RFSHeader.PID";
                        SLMResp.Batch.RFSReport.RFSHeader.StartDate = "RFSHeader.StartDate";
                        SLMResp.Batch.RFSReport.RFSHeader.Enddate = "RFSHeader.Enddate";
                    }

                    public RFSHeader()
                    {
                    }
                }

                public class RFSTrailer
                {
                    public readonly static string ReportType;

                    public readonly static string EndFileName;

                    public readonly static string RecordCount;

                    static RFSTrailer()
                    {
                        SLMResp.Batch.RFSReport.RFSTrailer.ReportType = "RFSTrailer.ReportType";
                        SLMResp.Batch.RFSReport.RFSTrailer.EndFileName = "RFSTrailer.EndFileName";
                        SLMResp.Batch.RFSReport.RFSTrailer.RecordCount = "RFSTrailer.RecordCount";
                    }

                    public RFSTrailer()
                    {
                    }
                }
            }

            public class RulesTriggeredData
            {
                public readonly static string ProductRecordSequenceNumber;

                public readonly static string RulesTriggered;

                static RulesTriggeredData()
                {
                    SLMResp.Batch.RulesTriggeredData.ProductRecordSequenceNumber = "RulesTriggeredData.ProductRecordSequenceNumber";
                    SLMResp.Batch.RulesTriggeredData.RulesTriggered = "RulesTriggeredData.RulesTriggered";
                }

                public RulesTriggeredData()
                {
                }
            }

            public class SearsBusinessRecord1
            {
                public readonly static string ProductCode1;

                public readonly static string ProductAmount1;

                public readonly static string ProductCode2;

                public readonly static string ProductAmount2;

                public readonly static string ProductCode3;

                public readonly static string ProductAmount3;

                public readonly static string ProductCode4;

                public readonly static string ProductAmount4;

                public readonly static string TaxAmount;

                static SearsBusinessRecord1()
                {
                    SLMResp.Batch.SearsBusinessRecord1.ProductCode1 = "SearsBusinessRecord1.ProductCode1";
                    SLMResp.Batch.SearsBusinessRecord1.ProductAmount1 = "SearsBusinessRecord1.ProductAmount1";
                    SLMResp.Batch.SearsBusinessRecord1.ProductCode2 = "SearsBusinessRecord1.ProductCode2";
                    SLMResp.Batch.SearsBusinessRecord1.ProductAmount2 = "SearsBusinessRecord1.ProductAmount2";
                    SLMResp.Batch.SearsBusinessRecord1.ProductCode3 = "SearsBusinessRecord1.ProductCode3";
                    SLMResp.Batch.SearsBusinessRecord1.ProductAmount3 = "SearsBusinessRecord1.ProductAmount3";
                    SLMResp.Batch.SearsBusinessRecord1.ProductCode4 = "SearsBusinessRecord1.ProductCode4";
                    SLMResp.Batch.SearsBusinessRecord1.ProductAmount4 = "SearsBusinessRecord1.ProductAmount4";
                    SLMResp.Batch.SearsBusinessRecord1.TaxAmount = "SearsBusinessRecord1.TaxAmount";
                }

                public SearsBusinessRecord1()
                {
                }
            }

            public class ShipToAddress
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static ShipToAddress()
                {
                    SLMResp.Batch.ShipToAddress.AddressLine = "ShipToAddress.AddressLine";
                    SLMResp.Batch.ShipToAddress.TelephoneType = "ShipToAddress.TelephoneType";
                    SLMResp.Batch.ShipToAddress.TelephoneNumber = "ShipToAddress.TelephoneNumber";
                    SLMResp.Batch.ShipToAddress.CountryCode = "ShipToAddress.CountryCode";
                }

                public ShipToAddress()
                {
                }
            }

            public class ShopWithPointsRecord1
            {
                public readonly static string RewardsTransactionAmount;

                public readonly static string RewardsCurrency;

                public readonly static string ConversionRate;

                public readonly static string ProductCode;

                static ShopWithPointsRecord1()
                {
                    SLMResp.Batch.ShopWithPointsRecord1.RewardsTransactionAmount = "ShopWithPointsRecord1.RewardsTransactionAmount";
                    SLMResp.Batch.ShopWithPointsRecord1.RewardsCurrency = "ShopWithPointsRecord1.RewardsCurrency";
                    SLMResp.Batch.ShopWithPointsRecord1.ConversionRate = "ShopWithPointsRecord1.ConversionRate";
                    SLMResp.Batch.ShopWithPointsRecord1.ProductCode = "ShopWithPointsRecord1.ProductCode";
                }

                public ShopWithPointsRecord1()
                {
                }
            }

            public class SoftMerchantInformation3
            {
                public readonly static string EmailAddress;

                static SoftMerchantInformation3()
                {
                    SLMResp.Batch.SoftMerchantInformation3.EmailAddress = "SoftMerchantInformation3.EmailAddress";
                }

                public SoftMerchantInformation3()
                {
                }
            }

            public class SoftMerchantRecord1
            {
                public readonly static string DBA;

                public readonly static string MerchantID;

                public readonly static string MerchantContactInfo;

                public readonly static string EmailAddress;

                static SoftMerchantRecord1()
                {
                    SLMResp.Batch.SoftMerchantRecord1.DBA = "SoftMerchantRecord1.DBA";
                    SLMResp.Batch.SoftMerchantRecord1.MerchantID = "SoftMerchantRecord1.MerchantID";
                    SLMResp.Batch.SoftMerchantRecord1.MerchantContactInfo = "SoftMerchantRecord1.MerchantContactInfo";
                    SLMResp.Batch.SoftMerchantRecord1.EmailAddress = "SoftMerchantRecord1.EmailAddress";
                }

                public SoftMerchantRecord1()
                {
                }
            }

            public class SoftMerchantRecord2
            {
                public readonly static string Street;

                public readonly static string City;

                public readonly static string Region;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                public readonly static string MerchantSellerID;

                public readonly static string MCC;

                static SoftMerchantRecord2()
                {
                    SLMResp.Batch.SoftMerchantRecord2.Street = "SoftMerchantRecord2.Street";
                    SLMResp.Batch.SoftMerchantRecord2.City = "SoftMerchantRecord2.City";
                    SLMResp.Batch.SoftMerchantRecord2.Region = "SoftMerchantRecord2.Region";
                    SLMResp.Batch.SoftMerchantRecord2.PostalCode = "SoftMerchantRecord2.PostalCode";
                    SLMResp.Batch.SoftMerchantRecord2.CountryCode = "SoftMerchantRecord2.CountryCode";
                    SLMResp.Batch.SoftMerchantRecord2.MerchantSellerID = "SoftMerchantRecord2.MerchantSellerID";
                    SLMResp.Batch.SoftMerchantRecord2.MCC = "SoftMerchantRecord2.MCC";
                }

                public SoftMerchantRecord2()
                {
                }
            }

            public class SwitchSoloExtRecord1
            {
                public readonly static string CardStartDate;

                public readonly static string CardIssueNumber;

                public readonly static string AccountHolderAuthValue;

                static SwitchSoloExtRecord1()
                {
                    SLMResp.Batch.SwitchSoloExtRecord1.CardStartDate = "SwitchSoloExtRecord1.CardStartDate";
                    SLMResp.Batch.SwitchSoloExtRecord1.CardIssueNumber = "SwitchSoloExtRecord1.CardIssueNumber";
                    SLMResp.Batch.SwitchSoloExtRecord1.AccountHolderAuthValue = "SwitchSoloExtRecord1.AccountHolderAuthValue";
                }

                public SwitchSoloExtRecord1()
                {
                }
            }

            public class TCPPGP
            {
                public TCPPGP()
                {
                }
            }

            public class Totals
            {
                public readonly static string FileRecordCount;

                public readonly static string FileOrderCount;

                public readonly static string FileAmountTotal;

                public readonly static string FileAmountSales;

                public readonly static string FileAmountRefunds;

                static Totals()
                {
                    SLMResp.Batch.Totals.FileRecordCount = "Totals.FileRecordCount";
                    SLMResp.Batch.Totals.FileOrderCount = "Totals.FileOrderCount";
                    SLMResp.Batch.Totals.FileAmountTotal = "Totals.FileAmountTotal";
                    SLMResp.Batch.Totals.FileAmountSales = "Totals.FileAmountSales";
                    SLMResp.Batch.Totals.FileAmountRefunds = "Totals.FileAmountRefunds";
                }

                public Totals()
                {
                }
            }

            public class Trailer
            {
                public readonly static string PID;

                public readonly static string PIDPassword;

                public readonly static string SID;

                public readonly static string SIDPassword;

                public readonly static string CreationDate;

                static Trailer()
                {
                    SLMResp.Batch.Trailer.PID = "Trailer.PID";
                    SLMResp.Batch.Trailer.PIDPassword = "Trailer.PIDPassword";
                    SLMResp.Batch.Trailer.SID = "Trailer.SID";
                    SLMResp.Batch.Trailer.SIDPassword = "Trailer.SIDPassword";
                    SLMResp.Batch.Trailer.CreationDate = "Trailer.CreationDate";
                }

                public Trailer()
                {
                }
            }

            public class TransactionInfoRecord1
            {
                public readonly static string SurchargeAmount;

                static TransactionInfoRecord1()
                {
                    SLMResp.Batch.TransactionInfoRecord1.SurchargeAmount = "TransactionInfoRecord1.SurchargeAmount";
                }

                public TransactionInfoRecord1()
                {
                }
            }

            public class UKDomesticMaestroExtRecord1
            {
                public readonly static string CardStartDate;

                public readonly static string CardIssueNumber;

                public readonly static string AccountHolderAuthValue;

                static UKDomesticMaestroExtRecord1()
                {
                    SLMResp.Batch.UKDomesticMaestroExtRecord1.CardStartDate = "UKDomesticMaestroExtRecord1.CardStartDate";
                    SLMResp.Batch.UKDomesticMaestroExtRecord1.CardIssueNumber = "UKDomesticMaestroExtRecord1.CardIssueNumber";
                    SLMResp.Batch.UKDomesticMaestroExtRecord1.AccountHolderAuthValue = "UKDomesticMaestroExtRecord1.AccountHolderAuthValue";
                }

                public UKDomesticMaestroExtRecord1()
                {
                }
            }

            public class USDIAccountUpdater
            {
                public USDIAccountUpdater()
                {
                }

                public class AccountUpdaterDetail
                {
                    public readonly static string RecordType;

                    public readonly static string TransactionDivisionNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string OldAccountNumber;

                    public readonly static string OldExpirationDate;

                    public readonly static string NewAccountNumber;

                    public readonly static string NewExpirationDate;

                    public readonly static string UpdateResponse;

                    public readonly static string PreviouslySentFlag;

                    public readonly static string TokenIndicator;

                    static AccountUpdaterDetail()
                    {
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.RecordType = "AccountUpdaterDetail.RecordType";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.TransactionDivisionNumber = "AccountUpdaterDetail.TransactionDivisionNumber";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.MerchantOrderNumber = "AccountUpdaterDetail.MerchantOrderNumber";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.OldAccountNumber = "AccountUpdaterDetail.OldAccountNumber";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.OldExpirationDate = "AccountUpdaterDetail.OldExpirationDate";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.NewAccountNumber = "AccountUpdaterDetail.NewAccountNumber";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.NewExpirationDate = "AccountUpdaterDetail.NewExpirationDate";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.UpdateResponse = "AccountUpdaterDetail.UpdateResponse";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.PreviouslySentFlag = "AccountUpdaterDetail.PreviouslySentFlag";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterDetail.TokenIndicator = "AccountUpdaterDetail.TokenIndicator";
                    }

                    public AccountUpdaterDetail()
                    {
                    }
                }

                public class AccountUpdaterHeader
                {
                    public readonly static string RecordType;

                    public readonly static string EntityLevel;

                    public readonly static string FileDate;

                    public readonly static string FileVersion;

                    static AccountUpdaterHeader()
                    {
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterHeader.RecordType = "AccountUpdaterHeader.RecordType";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterHeader.EntityLevel = "AccountUpdaterHeader.EntityLevel";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterHeader.FileDate = "AccountUpdaterHeader.FileDate";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterHeader.FileVersion = "AccountUpdaterHeader.FileVersion";
                    }

                    public AccountUpdaterHeader()
                    {
                    }
                }

                public class AccountUpdaterTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string FileRecordCount;

                    static AccountUpdaterTrailer()
                    {
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterTrailer.RecordType = "AccountUpdaterTrailer.RecordType";
                        SLMResp.Batch.USDIAccountUpdater.AccountUpdaterTrailer.FileRecordCount = "AccountUpdaterTrailer.FileRecordCount";
                    }

                    public AccountUpdaterTrailer()
                    {
                    }
                }
            }

            public class UserDefinedAndShoppingCart
            {
                public readonly static string SequenceNumber;

                public readonly static string DataString;

                static UserDefinedAndShoppingCart()
                {
                    SLMResp.Batch.UserDefinedAndShoppingCart.SequenceNumber = "UserDefinedAndShoppingCart.SequenceNumber";
                    SLMResp.Batch.UserDefinedAndShoppingCart.DataString = "UserDefinedAndShoppingCart.DataString";
                }

                public UserDefinedAndShoppingCart()
                {
                }
            }

            public class USMCAccountUpdater
            {
                public USMCAccountUpdater()
                {
                }

                public class AccountUpdaterDetail
                {
                    public readonly static string RecordType;

                    public readonly static string TransactionDivisionNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string OldAccountNumber;

                    public readonly static string OldExpirationDate;

                    public readonly static string NewAccountNumber;

                    public readonly static string NewExpirationDate;

                    public readonly static string UpdateResponse;

                    public readonly static string PreviouslySentFlag;

                    public readonly static string TokenIndicator;

                    static AccountUpdaterDetail()
                    {
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.RecordType = "AccountUpdaterDetail.RecordType";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.TransactionDivisionNumber = "AccountUpdaterDetail.TransactionDivisionNumber";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.MerchantOrderNumber = "AccountUpdaterDetail.MerchantOrderNumber";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.OldAccountNumber = "AccountUpdaterDetail.OldAccountNumber";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.OldExpirationDate = "AccountUpdaterDetail.OldExpirationDate";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.NewAccountNumber = "AccountUpdaterDetail.NewAccountNumber";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.NewExpirationDate = "AccountUpdaterDetail.NewExpirationDate";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.UpdateResponse = "AccountUpdaterDetail.UpdateResponse";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.PreviouslySentFlag = "AccountUpdaterDetail.PreviouslySentFlag";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterDetail.TokenIndicator = "AccountUpdaterDetail.TokenIndicator";
                    }

                    public AccountUpdaterDetail()
                    {
                    }
                }

                public class AccountUpdaterHeader
                {
                    public readonly static string RecordType;

                    public readonly static string EntityLevel;

                    public readonly static string FileDate;

                    public readonly static string FileVersion;

                    static AccountUpdaterHeader()
                    {
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterHeader.RecordType = "AccountUpdaterHeader.RecordType";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterHeader.EntityLevel = "AccountUpdaterHeader.EntityLevel";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterHeader.FileDate = "AccountUpdaterHeader.FileDate";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterHeader.FileVersion = "AccountUpdaterHeader.FileVersion";
                    }

                    public AccountUpdaterHeader()
                    {
                    }
                }

                public class AccountUpdaterTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string FileRecordCount;

                    static AccountUpdaterTrailer()
                    {
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterTrailer.RecordType = "AccountUpdaterTrailer.RecordType";
                        SLMResp.Batch.USMCAccountUpdater.AccountUpdaterTrailer.FileRecordCount = "AccountUpdaterTrailer.FileRecordCount";
                    }

                    public AccountUpdaterTrailer()
                    {
                    }
                }
            }

            public class USVIAccountUpdater
            {
                public USVIAccountUpdater()
                {
                }

                public class AccountUpdaterDetail
                {
                    public readonly static string RecordType;

                    public readonly static string TransactionDivisionNumber;

                    public readonly static string MerchantOrderNumber;

                    public readonly static string OldAccountNumber;

                    public readonly static string OldExpirationDate;

                    public readonly static string NewAccountNumber;

                    public readonly static string NewExpirationDate;

                    public readonly static string UpdateResponse;

                    public readonly static string PreviouslySentFlag;

                    public readonly static string TokenIndicator;

                    static AccountUpdaterDetail()
                    {
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.RecordType = "AccountUpdaterDetail.RecordType";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.TransactionDivisionNumber = "AccountUpdaterDetail.TransactionDivisionNumber";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.MerchantOrderNumber = "AccountUpdaterDetail.MerchantOrderNumber";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.OldAccountNumber = "AccountUpdaterDetail.OldAccountNumber";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.OldExpirationDate = "AccountUpdaterDetail.OldExpirationDate";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.NewAccountNumber = "AccountUpdaterDetail.NewAccountNumber";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.NewExpirationDate = "AccountUpdaterDetail.NewExpirationDate";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.UpdateResponse = "AccountUpdaterDetail.UpdateResponse";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.PreviouslySentFlag = "AccountUpdaterDetail.PreviouslySentFlag";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterDetail.TokenIndicator = "AccountUpdaterDetail.TokenIndicator";
                    }

                    public AccountUpdaterDetail()
                    {
                    }
                }

                public class AccountUpdaterHeader
                {
                    public readonly static string RecordType;

                    public readonly static string EntityLevel;

                    public readonly static string FileDate;

                    public readonly static string FileVersion;

                    static AccountUpdaterHeader()
                    {
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterHeader.RecordType = "AccountUpdaterHeader.RecordType";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterHeader.EntityLevel = "AccountUpdaterHeader.EntityLevel";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterHeader.FileDate = "AccountUpdaterHeader.FileDate";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterHeader.FileVersion = "AccountUpdaterHeader.FileVersion";
                    }

                    public AccountUpdaterHeader()
                    {
                    }
                }

                public class AccountUpdaterTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string FileRecordCount;

                    static AccountUpdaterTrailer()
                    {
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterTrailer.RecordType = "AccountUpdaterTrailer.RecordType";
                        SLMResp.Batch.USVIAccountUpdater.AccountUpdaterTrailer.FileRecordCount = "AccountUpdaterTrailer.FileRecordCount";
                    }

                    public AccountUpdaterTrailer()
                    {
                    }
                }
            }

            public class VariousTextRecord1
            {
                public readonly static string TextMessage;

                static VariousTextRecord1()
                {
                    SLMResp.Batch.VariousTextRecord1.TextMessage = "VariousTextRecord1.TextMessage";
                }

                public VariousTextRecord1()
                {
                }
            }

            public class VehicleRentalRecord1
            {
                public readonly static string RenterName;

                public readonly static string RentalAgreementNumber;

                public readonly static string RentalDate;

                public readonly static string RentalTime;

                public readonly static string RentalCity;

                public readonly static string RentalStateRegion;

                public readonly static string RentalCountry;

                public readonly static string DaysRented;

                static VehicleRentalRecord1()
                {
                    SLMResp.Batch.VehicleRentalRecord1.RenterName = "VehicleRentalRecord1.RenterName";
                    SLMResp.Batch.VehicleRentalRecord1.RentalAgreementNumber = "VehicleRentalRecord1.RentalAgreementNumber";
                    SLMResp.Batch.VehicleRentalRecord1.RentalDate = "VehicleRentalRecord1.RentalDate";
                    SLMResp.Batch.VehicleRentalRecord1.RentalTime = "VehicleRentalRecord1.RentalTime";
                    SLMResp.Batch.VehicleRentalRecord1.RentalCity = "VehicleRentalRecord1.RentalCity";
                    SLMResp.Batch.VehicleRentalRecord1.RentalStateRegion = "VehicleRentalRecord1.RentalStateRegion";
                    SLMResp.Batch.VehicleRentalRecord1.RentalCountry = "VehicleRentalRecord1.RentalCountry";
                    SLMResp.Batch.VehicleRentalRecord1.DaysRented = "VehicleRentalRecord1.DaysRented";
                }

                public VehicleRentalRecord1()
                {
                }
            }

            public class VehicleRentalRecord2
            {
                public readonly static string ReturnCity;

                public readonly static string ReturnStateRegion;

                public readonly static string ReturnCountry;

                public readonly static string ReturnLocationID;

                public readonly static string ReturnDate;

                public readonly static string ReturnTime;

                public readonly static string TollFreeNumber;

                public readonly static string RentalType;

                public readonly static string RentalRateperRentalType;

                public readonly static string RentalClassID;

                public readonly static string NoShowIndicator;

                public readonly static string TaxExemptIndicator;

                public readonly static string OneWayCharge;

                public readonly static string ExtraCharges;

                static VehicleRentalRecord2()
                {
                    SLMResp.Batch.VehicleRentalRecord2.ReturnCity = "VehicleRentalRecord2.ReturnCity";
                    SLMResp.Batch.VehicleRentalRecord2.ReturnStateRegion = "VehicleRentalRecord2.ReturnStateRegion";
                    SLMResp.Batch.VehicleRentalRecord2.ReturnCountry = "VehicleRentalRecord2.ReturnCountry";
                    SLMResp.Batch.VehicleRentalRecord2.ReturnLocationID = "VehicleRentalRecord2.ReturnLocationID";
                    SLMResp.Batch.VehicleRentalRecord2.ReturnDate = "VehicleRentalRecord2.ReturnDate";
                    SLMResp.Batch.VehicleRentalRecord2.ReturnTime = "VehicleRentalRecord2.ReturnTime";
                    SLMResp.Batch.VehicleRentalRecord2.TollFreeNumber = "VehicleRentalRecord2.TollFreeNumber";
                    SLMResp.Batch.VehicleRentalRecord2.RentalType = "VehicleRentalRecord2.RentalType";
                    SLMResp.Batch.VehicleRentalRecord2.RentalRateperRentalType = "VehicleRentalRecord2.RentalRateperRentalType";
                    SLMResp.Batch.VehicleRentalRecord2.RentalClassID = "VehicleRentalRecord2.RentalClassID";
                    SLMResp.Batch.VehicleRentalRecord2.NoShowIndicator = "VehicleRentalRecord2.NoShowIndicator";
                    SLMResp.Batch.VehicleRentalRecord2.TaxExemptIndicator = "VehicleRentalRecord2.TaxExemptIndicator";
                    SLMResp.Batch.VehicleRentalRecord2.OneWayCharge = "VehicleRentalRecord2.OneWayCharge";
                    SLMResp.Batch.VehicleRentalRecord2.ExtraCharges = "VehicleRentalRecord2.ExtraCharges";
                }

                public VehicleRentalRecord2()
                {
                }
            }

            public class VisaBIN
            {
                public VisaBIN()
                {
                }

                public class VisaBINDetail
                {
                    public readonly static string RecordType;

                    public readonly static string LowRange;

                    public readonly static string HighRange;

                    public readonly static string ConsumerDigitalPaymentTokenIndicator;

                    static VisaBINDetail()
                    {
                        SLMResp.Batch.VisaBIN.VisaBINDetail.RecordType = "VisaBINDetail.RecordType";
                        SLMResp.Batch.VisaBIN.VisaBINDetail.LowRange = "VisaBINDetail.LowRange";
                        SLMResp.Batch.VisaBIN.VisaBINDetail.HighRange = "VisaBINDetail.HighRange";
                        SLMResp.Batch.VisaBIN.VisaBINDetail.ConsumerDigitalPaymentTokenIndicator = "VisaBINDetail.ConsumerDigitalPaymentTokenIndicator";
                    }

                    public VisaBINDetail()
                    {
                    }
                }

                public class VisaBINHeader
                {
                    public readonly static string RecordType;

                    public readonly static string FileName;

                    public readonly static string VersionNumber;

                    public readonly static string CreationDate;

                    static VisaBINHeader()
                    {
                        SLMResp.Batch.VisaBIN.VisaBINHeader.RecordType = "VisaBINHeader.RecordType";
                        SLMResp.Batch.VisaBIN.VisaBINHeader.FileName = "VisaBINHeader.FileName";
                        SLMResp.Batch.VisaBIN.VisaBINHeader.VersionNumber = "VisaBINHeader.VersionNumber";
                        SLMResp.Batch.VisaBIN.VisaBINHeader.CreationDate = "VisaBINHeader.CreationDate";
                    }

                    public VisaBINHeader()
                    {
                    }
                }

                public class VisaBINTrailer
                {
                    public readonly static string RecordType;

                    public readonly static string CreationDate;

                    public readonly static string NumberOfBinRecords;

                    static VisaBINTrailer()
                    {
                        SLMResp.Batch.VisaBIN.VisaBINTrailer.RecordType = "VisaBINTrailer.RecordType";
                        SLMResp.Batch.VisaBIN.VisaBINTrailer.CreationDate = "VisaBINTrailer.CreationDate";
                        SLMResp.Batch.VisaBIN.VisaBINTrailer.NumberOfBinRecords = "VisaBINTrailer.NumberOfBinRecords";
                    }

                    public VisaBINTrailer()
                    {
                    }
                }
            }

            public class VisaExtRecord1
            {
                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string POSAuthSource;

                public readonly static string POSCardIDMethod;

                public readonly static string AuthCharacteristicIndicator;

                public readonly static string TransactionID;

                public readonly static string ValidationCode;

                public readonly static string AuthorizedAmount;

                public readonly static string MerchCategoryCode;

                public readonly static string TotalAuthAmount;

                public readonly static string MarketSpecificDataIndicator;

                public readonly static string CATType;

                public readonly static string CardLevelResult;

                public readonly static string PartialAuthIndicator;

                public readonly static string AuthorizationResponseCode;

                public readonly static string SpendQualifiedIndicator;

                static VisaExtRecord1()
                {
                    SLMResp.Batch.VisaExtRecord1.POSCapabilityCode = "VisaExtRecord1.POSCapabilityCode";
                    SLMResp.Batch.VisaExtRecord1.POSEntryMode = "VisaExtRecord1.POSEntryMode";
                    SLMResp.Batch.VisaExtRecord1.POSAuthSource = "VisaExtRecord1.POSAuthSource";
                    SLMResp.Batch.VisaExtRecord1.POSCardIDMethod = "VisaExtRecord1.POSCardIDMethod";
                    SLMResp.Batch.VisaExtRecord1.AuthCharacteristicIndicator = "VisaExtRecord1.AuthCharacteristicIndicator";
                    SLMResp.Batch.VisaExtRecord1.TransactionID = "VisaExtRecord1.TransactionID";
                    SLMResp.Batch.VisaExtRecord1.ValidationCode = "VisaExtRecord1.ValidationCode";
                    SLMResp.Batch.VisaExtRecord1.AuthorizedAmount = "VisaExtRecord1.AuthorizedAmount";
                    SLMResp.Batch.VisaExtRecord1.MerchCategoryCode = "VisaExtRecord1.MerchCategoryCode";
                    SLMResp.Batch.VisaExtRecord1.TotalAuthAmount = "VisaExtRecord1.TotalAuthAmount";
                    SLMResp.Batch.VisaExtRecord1.MarketSpecificDataIndicator = "VisaExtRecord1.MarketSpecificDataIndicator";
                    SLMResp.Batch.VisaExtRecord1.CATType = "VisaExtRecord1.CATType";
                    SLMResp.Batch.VisaExtRecord1.CardLevelResult = "VisaExtRecord1.CardLevelResult";
                    SLMResp.Batch.VisaExtRecord1.PartialAuthIndicator = "VisaExtRecord1.PartialAuthIndicator";
                    SLMResp.Batch.VisaExtRecord1.AuthorizationResponseCode = "VisaExtRecord1.AuthorizationResponseCode";
                    SLMResp.Batch.VisaExtRecord1.SpendQualifiedIndicator = "VisaExtRecord1.SpendQualifiedIndicator";
                }

                public VisaExtRecord1()
                {
                }
            }

            public class VisaExtRecord1A
            {
                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string POSAuthSource;

                public readonly static string POSCardIDMethod;

                public readonly static string AuthCharacteristicIndicator;

                public readonly static string TransactionID;

                public readonly static string ValidationCode;

                public readonly static string AuthorizedAmount;

                public readonly static string MerchCategoryCode;

                public readonly static string TotalAuthAmount;

                public readonly static string MarketSpecificDataIndicator;

                public readonly static string CATType;

                public readonly static string CardLevelResult;

                static VisaExtRecord1A()
                {
                    SLMResp.Batch.VisaExtRecord1A.POSCapabilityCode = "VisaExtRecord1A.POSCapabilityCode";
                    SLMResp.Batch.VisaExtRecord1A.POSEntryMode = "VisaExtRecord1A.POSEntryMode";
                    SLMResp.Batch.VisaExtRecord1A.POSAuthSource = "VisaExtRecord1A.POSAuthSource";
                    SLMResp.Batch.VisaExtRecord1A.POSCardIDMethod = "VisaExtRecord1A.POSCardIDMethod";
                    SLMResp.Batch.VisaExtRecord1A.AuthCharacteristicIndicator = "VisaExtRecord1A.AuthCharacteristicIndicator";
                    SLMResp.Batch.VisaExtRecord1A.TransactionID = "VisaExtRecord1A.TransactionID";
                    SLMResp.Batch.VisaExtRecord1A.ValidationCode = "VisaExtRecord1A.ValidationCode";
                    SLMResp.Batch.VisaExtRecord1A.AuthorizedAmount = "VisaExtRecord1A.AuthorizedAmount";
                    SLMResp.Batch.VisaExtRecord1A.MerchCategoryCode = "VisaExtRecord1A.MerchCategoryCode";
                    SLMResp.Batch.VisaExtRecord1A.TotalAuthAmount = "VisaExtRecord1A.TotalAuthAmount";
                    SLMResp.Batch.VisaExtRecord1A.MarketSpecificDataIndicator = "VisaExtRecord1A.MarketSpecificDataIndicator";
                    SLMResp.Batch.VisaExtRecord1A.CATType = "VisaExtRecord1A.CATType";
                    SLMResp.Batch.VisaExtRecord1A.CardLevelResult = "VisaExtRecord1A.CardLevelResult";
                }

                public VisaExtRecord1A()
                {
                }
            }

            public class VisaExtRecord2
            {
                public readonly static string TransactionID;

                public readonly static string CAVV;

                public readonly static string CAVVResponseCode;

                static VisaExtRecord2()
                {
                    SLMResp.Batch.VisaExtRecord2.TransactionID = "VisaExtRecord2.TransactionID";
                    SLMResp.Batch.VisaExtRecord2.CAVV = "VisaExtRecord2.CAVV";
                    SLMResp.Batch.VisaExtRecord2.CAVVResponseCode = "VisaExtRecord2.CAVVResponseCode";
                }

                public VisaExtRecord2()
                {
                }
            }

            public class VisaLevel3LineItemRecord1
            {
                public readonly static string SeqNumber;

                public readonly static string Description;

                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string UnitOfMeasure;

                public readonly static string TaxAmount;

                public readonly static string TaxRate;

                static VisaLevel3LineItemRecord1()
                {
                    SLMResp.Batch.VisaLevel3LineItemRecord1.SeqNumber = "VisaLevel3LineItemRecord1.SeqNumber";
                    SLMResp.Batch.VisaLevel3LineItemRecord1.Description = "VisaLevel3LineItemRecord1.Description";
                    SLMResp.Batch.VisaLevel3LineItemRecord1.ProductCode = "VisaLevel3LineItemRecord1.ProductCode";
                    SLMResp.Batch.VisaLevel3LineItemRecord1.Quantity = "VisaLevel3LineItemRecord1.Quantity";
                    SLMResp.Batch.VisaLevel3LineItemRecord1.UnitOfMeasure = "VisaLevel3LineItemRecord1.UnitOfMeasure";
                    SLMResp.Batch.VisaLevel3LineItemRecord1.TaxAmount = "VisaLevel3LineItemRecord1.TaxAmount";
                    SLMResp.Batch.VisaLevel3LineItemRecord1.TaxRate = "VisaLevel3LineItemRecord1.TaxRate";
                }

                public VisaLevel3LineItemRecord1()
                {
                }
            }

            public class VisaLevel3LineItemRecord2
            {
                public readonly static string SeqNumber;

                public readonly static string LineItemTotal;

                public readonly static string DiscountAmount;

                public readonly static string ItemCommodityCode;

                public readonly static string UnitCost;

                public readonly static string LineItemLevelDiscountTreatmentCode;

                public readonly static string LineItemDetailIndicator;

                static VisaLevel3LineItemRecord2()
                {
                    SLMResp.Batch.VisaLevel3LineItemRecord2.SeqNumber = "VisaLevel3LineItemRecord2.SeqNumber";
                    SLMResp.Batch.VisaLevel3LineItemRecord2.LineItemTotal = "VisaLevel3LineItemRecord2.LineItemTotal";
                    SLMResp.Batch.VisaLevel3LineItemRecord2.DiscountAmount = "VisaLevel3LineItemRecord2.DiscountAmount";
                    SLMResp.Batch.VisaLevel3LineItemRecord2.ItemCommodityCode = "VisaLevel3LineItemRecord2.ItemCommodityCode";
                    SLMResp.Batch.VisaLevel3LineItemRecord2.UnitCost = "VisaLevel3LineItemRecord2.UnitCost";
                    SLMResp.Batch.VisaLevel3LineItemRecord2.LineItemLevelDiscountTreatmentCode = "VisaLevel3LineItemRecord2.LineItemLevelDiscountTreatmentCode";
                    SLMResp.Batch.VisaLevel3LineItemRecord2.LineItemDetailIndicator = "VisaLevel3LineItemRecord2.LineItemDetailIndicator";
                }

                public VisaLevel3LineItemRecord2()
                {
                }
            }

            public class VisaLevel3OrderRecord
            {
                public readonly static string FreightAmount;

                public readonly static string DutyAmount;

                public readonly static string DestinationPostalCode;

                public readonly static string DestinationCountryCode;

                public readonly static string ShipFromPostalCode;

                public readonly static string DiscountApplied;

                public readonly static string TaxAmount;

                public readonly static string TaxRate;

                public readonly static string ShippingTaxRate;

                public readonly static string InvoiceDiscountTreatment;

                public readonly static string TaxTreatment;

                public readonly static string DiscountAmountSign;

                public readonly static string FreightAmountSign;

                public readonly static string DutyAmountSign;

                public readonly static string VATTaxAmountSign;

                public readonly static string UniqueVATInvoiceReferenceNumber;

                public readonly static string VATTaxAmount;

                static VisaLevel3OrderRecord()
                {
                    SLMResp.Batch.VisaLevel3OrderRecord.FreightAmount = "VisaLevel3OrderRecord.FreightAmount";
                    SLMResp.Batch.VisaLevel3OrderRecord.DutyAmount = "VisaLevel3OrderRecord.DutyAmount";
                    SLMResp.Batch.VisaLevel3OrderRecord.DestinationPostalCode = "VisaLevel3OrderRecord.DestinationPostalCode";
                    SLMResp.Batch.VisaLevel3OrderRecord.DestinationCountryCode = "VisaLevel3OrderRecord.DestinationCountryCode";
                    SLMResp.Batch.VisaLevel3OrderRecord.ShipFromPostalCode = "VisaLevel3OrderRecord.ShipFromPostalCode";
                    SLMResp.Batch.VisaLevel3OrderRecord.DiscountApplied = "VisaLevel3OrderRecord.DiscountApplied";
                    SLMResp.Batch.VisaLevel3OrderRecord.TaxAmount = "VisaLevel3OrderRecord.TaxAmount";
                    SLMResp.Batch.VisaLevel3OrderRecord.TaxRate = "VisaLevel3OrderRecord.TaxRate";
                    SLMResp.Batch.VisaLevel3OrderRecord.ShippingTaxRate = "VisaLevel3OrderRecord.ShippingTaxRate";
                    SLMResp.Batch.VisaLevel3OrderRecord.InvoiceDiscountTreatment = "VisaLevel3OrderRecord.InvoiceDiscountTreatment";
                    SLMResp.Batch.VisaLevel3OrderRecord.TaxTreatment = "VisaLevel3OrderRecord.TaxTreatment";
                    SLMResp.Batch.VisaLevel3OrderRecord.DiscountAmountSign = "VisaLevel3OrderRecord.DiscountAmountSign";
                    SLMResp.Batch.VisaLevel3OrderRecord.FreightAmountSign = "VisaLevel3OrderRecord.FreightAmountSign";
                    SLMResp.Batch.VisaLevel3OrderRecord.DutyAmountSign = "VisaLevel3OrderRecord.DutyAmountSign";
                    SLMResp.Batch.VisaLevel3OrderRecord.VATTaxAmountSign = "VisaLevel3OrderRecord.VATTaxAmountSign";
                    SLMResp.Batch.VisaLevel3OrderRecord.UniqueVATInvoiceReferenceNumber = "VisaLevel3OrderRecord.UniqueVATInvoiceReferenceNumber";
                    SLMResp.Batch.VisaLevel3OrderRecord.VATTaxAmount = "VisaLevel3OrderRecord.VATTaxAmount";
                }

                public VisaLevel3OrderRecord()
                {
                }
            }

            public class VoyagerLineItemLevelDataRecord1
            {
                public readonly static string SequenceNumber;

                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string LineItemTotal;

                public readonly static string TaxAmount;

                public readonly static string DiscountAmount;

                static VoyagerLineItemLevelDataRecord1()
                {
                    SLMResp.Batch.VoyagerLineItemLevelDataRecord1.SequenceNumber = "VoyagerLineItemLevelDataRecord1.SequenceNumber";
                    SLMResp.Batch.VoyagerLineItemLevelDataRecord1.ProductCode = "VoyagerLineItemLevelDataRecord1.ProductCode";
                    SLMResp.Batch.VoyagerLineItemLevelDataRecord1.Quantity = "VoyagerLineItemLevelDataRecord1.Quantity";
                    SLMResp.Batch.VoyagerLineItemLevelDataRecord1.LineItemTotal = "VoyagerLineItemLevelDataRecord1.LineItemTotal";
                    SLMResp.Batch.VoyagerLineItemLevelDataRecord1.TaxAmount = "VoyagerLineItemLevelDataRecord1.TaxAmount";
                    SLMResp.Batch.VoyagerLineItemLevelDataRecord1.DiscountAmount = "VoyagerLineItemLevelDataRecord1.DiscountAmount";
                }

                public VoyagerLineItemLevelDataRecord1()
                {
                }
            }

            public class VoyagerRecord1
            {
                public readonly static string AuthorizedAmount;

                public readonly static string AuthorizationResponse;

                public readonly static string MerchantCategoryCode;

                public readonly static string POSEntryMode;

                public readonly static string OriginalInvoiceNumber;

                public readonly static string PurchaseDate;

                public readonly static string PartialAuthorizationIndicator;

                static VoyagerRecord1()
                {
                    SLMResp.Batch.VoyagerRecord1.AuthorizedAmount = "VoyagerRecord1.AuthorizedAmount";
                    SLMResp.Batch.VoyagerRecord1.AuthorizationResponse = "VoyagerRecord1.AuthorizationResponse";
                    SLMResp.Batch.VoyagerRecord1.MerchantCategoryCode = "VoyagerRecord1.MerchantCategoryCode";
                    SLMResp.Batch.VoyagerRecord1.POSEntryMode = "VoyagerRecord1.POSEntryMode";
                    SLMResp.Batch.VoyagerRecord1.OriginalInvoiceNumber = "VoyagerRecord1.OriginalInvoiceNumber";
                    SLMResp.Batch.VoyagerRecord1.PurchaseDate = "VoyagerRecord1.PurchaseDate";
                    SLMResp.Batch.VoyagerRecord1.PartialAuthorizationIndicator = "VoyagerRecord1.PartialAuthorizationIndicator";
                }

                public VoyagerRecord1()
                {
                }
            }

            public class WrightExpressLineItemLevelDataRecord1
            {
                public readonly static string SequenceNumber;

                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string LineItemTotal;

                public readonly static string UnitPrice;

                static WrightExpressLineItemLevelDataRecord1()
                {
                    SLMResp.Batch.WrightExpressLineItemLevelDataRecord1.SequenceNumber = "WrightExpressLineItemLevelDataRecord1.SequenceNumber";
                    SLMResp.Batch.WrightExpressLineItemLevelDataRecord1.ProductCode = "WrightExpressLineItemLevelDataRecord1.ProductCode";
                    SLMResp.Batch.WrightExpressLineItemLevelDataRecord1.Quantity = "WrightExpressLineItemLevelDataRecord1.Quantity";
                    SLMResp.Batch.WrightExpressLineItemLevelDataRecord1.LineItemTotal = "WrightExpressLineItemLevelDataRecord1.LineItemTotal";
                    SLMResp.Batch.WrightExpressLineItemLevelDataRecord1.UnitPrice = "WrightExpressLineItemLevelDataRecord1.UnitPrice";
                }

                public WrightExpressLineItemLevelDataRecord1()
                {
                }
            }

            public class WrightExpressRecord1
            {
                public readonly static string AuthorizedAmount;

                public readonly static string AuthorizationResponse;

                public readonly static string MerchantCategoryCode;

                public readonly static string POSEntryMode;

                public readonly static string TicketNumber;

                public readonly static string PurchaseDate;

                static WrightExpressRecord1()
                {
                    SLMResp.Batch.WrightExpressRecord1.AuthorizedAmount = "WrightExpressRecord1.AuthorizedAmount";
                    SLMResp.Batch.WrightExpressRecord1.AuthorizationResponse = "WrightExpressRecord1.AuthorizationResponse";
                    SLMResp.Batch.WrightExpressRecord1.MerchantCategoryCode = "WrightExpressRecord1.MerchantCategoryCode";
                    SLMResp.Batch.WrightExpressRecord1.POSEntryMode = "WrightExpressRecord1.POSEntryMode";
                    SLMResp.Batch.WrightExpressRecord1.TicketNumber = "WrightExpressRecord1.TicketNumber";
                    SLMResp.Batch.WrightExpressRecord1.PurchaseDate = "WrightExpressRecord1.PurchaseDate";
                }

                public WrightExpressRecord1()
                {
                }
            }
        }

        public class Online
        {
            public Online()
            {
            }

            public class AccountLookupResponse
            {
                public readonly static string NumberAccountsOnFile;

                public readonly static string POSStatusDescription;

                public readonly static string NumberAccountsReturned;

                static AccountLookupResponse()
                {
                    SLMResp.Online.AccountLookupResponse.NumberAccountsOnFile = "AccountLookupResponse.NumberAccountsOnFile";
                    SLMResp.Online.AccountLookupResponse.POSStatusDescription = "AccountLookupResponse.POSStatusDescription";
                    SLMResp.Online.AccountLookupResponse.NumberAccountsReturned = "AccountLookupResponse.NumberAccountsReturned";
                }

                public AccountLookupResponse()
                {
                }

                public class Account
                {
                    public readonly static string AccountNumber;

                    public readonly static string CustomerName;

                    public readonly static string ProductName;

                    public readonly static string InStorePaymentAllowed;

                    static Account()
                    {
                        SLMResp.Online.AccountLookupResponse.Account.AccountNumber = "Account.AccountNumber";
                        SLMResp.Online.AccountLookupResponse.Account.CustomerName = "Account.CustomerName";
                        SLMResp.Online.AccountLookupResponse.Account.ProductName = "Account.ProductName";
                        SLMResp.Online.AccountLookupResponse.Account.InStorePaymentAllowed = "Account.InStorePaymentAllowed";
                    }

                    public Account()
                    {
                    }
                }
            }

            public class AdditionalAuthenticationData1Response
            {
                public readonly static string AuthenticationType;

                static AdditionalAuthenticationData1Response()
                {
                    SLMResp.Online.AdditionalAuthenticationData1Response.AuthenticationType = "AdditionalAuthenticationData1Response.AuthenticationType";
                }

                public AdditionalAuthenticationData1Response()
                {
                }
            }

            public class AmericanExpressAuthenticationResponse
            {
                public readonly static string AmericanExpressAuthenticationVerificationValue;

                public readonly static string TransactionID;

                static AmericanExpressAuthenticationResponse()
                {
                    SLMResp.Online.AmericanExpressAuthenticationResponse.AmericanExpressAuthenticationVerificationValue = "AmericanExpressAuthenticationResponse.AmericanExpressAuthenticationVerificationValue";
                    SLMResp.Online.AmericanExpressAuthenticationResponse.TransactionID = "AmericanExpressAuthenticationResponse.TransactionID";
                }

                public AmericanExpressAuthenticationResponse()
                {
                }
            }

            public class AmexResponse
            {
                public readonly static string TransactionIdentifier;

                public readonly static string POSCapabilityCode;

                public readonly static string CardholderAuthenticationCapability;

                public readonly static string CardCaptureCapability;

                public readonly static string OperatingEnvironment;

                public readonly static string CardholderPresent;

                public readonly static string CardPresent;

                public readonly static string POSEntryMode;

                public readonly static string POSCardIDMethod;

                public readonly static string CardholderAuthenticationEntity;

                public readonly static string CardDataOutputCapability;

                public readonly static string TerminalOutputCapability;

                public readonly static string PINCaptureCapability;

                static AmexResponse()
                {
                    SLMResp.Online.AmexResponse.TransactionIdentifier = "AmexResponse.TransactionIdentifier";
                    SLMResp.Online.AmexResponse.POSCapabilityCode = "AmexResponse.POSCapabilityCode";
                    SLMResp.Online.AmexResponse.CardholderAuthenticationCapability = "AmexResponse.CardholderAuthenticationCapability";
                    SLMResp.Online.AmexResponse.CardCaptureCapability = "AmexResponse.CardCaptureCapability";
                    SLMResp.Online.AmexResponse.OperatingEnvironment = "AmexResponse.OperatingEnvironment";
                    SLMResp.Online.AmexResponse.CardholderPresent = "AmexResponse.CardholderPresent";
                    SLMResp.Online.AmexResponse.CardPresent = "AmexResponse.CardPresent";
                    SLMResp.Online.AmexResponse.POSEntryMode = "AmexResponse.POSEntryMode";
                    SLMResp.Online.AmexResponse.POSCardIDMethod = "AmexResponse.POSCardIDMethod";
                    SLMResp.Online.AmexResponse.CardholderAuthenticationEntity = "AmexResponse.CardholderAuthenticationEntity";
                    SLMResp.Online.AmexResponse.CardDataOutputCapability = "AmexResponse.CardDataOutputCapability";
                    SLMResp.Online.AmexResponse.TerminalOutputCapability = "AmexResponse.TerminalOutputCapability";
                    SLMResp.Online.AmexResponse.PINCaptureCapability = "AmexResponse.PINCaptureCapability";
                }

                public AmexResponse()
                {
                }
            }

            public class BalanceInquiry2Response
            {
                public readonly static string CurrentBalance;

                public readonly static string CurrentBalanceSign;

                public readonly static string CurrentBalanceCurrencyCode;

                static BalanceInquiry2Response()
                {
                    SLMResp.Online.BalanceInquiry2Response.CurrentBalance = "BalanceInquiry2Response.CurrentBalance";
                    SLMResp.Online.BalanceInquiry2Response.CurrentBalanceSign = "BalanceInquiry2Response.CurrentBalanceSign";
                    SLMResp.Online.BalanceInquiry2Response.CurrentBalanceCurrencyCode = "BalanceInquiry2Response.CurrentBalanceCurrencyCode";
                }

                public BalanceInquiry2Response()
                {
                }
            }

            public class BalanceInquiryResponse
            {
                public readonly static string CurrentBalance;

                static BalanceInquiryResponse()
                {
                    SLMResp.Online.BalanceInquiryResponse.CurrentBalance = "BalanceInquiryResponse.CurrentBalance";
                }

                public BalanceInquiryResponse()
                {
                }
            }

            public class BillMeLaterResponse
            {
                public readonly static string ExpirationDate;

                public readonly static string CreditLine;

                public readonly static string PromoOffer;

                public readonly static string ApprovedAmount;

                public readonly static string ApprovedTermCode;

                public readonly static string ShippingAddressIndicator;

                public readonly static string LTVEstimator;

                public readonly static string RiskEstimator;

                public readonly static string RiskQueue;

                public readonly static string LeaseMasterAgreementID;

                public readonly static string LeaseDocRequirement;

                static BillMeLaterResponse()
                {
                    SLMResp.Online.BillMeLaterResponse.ExpirationDate = "BillMeLaterResponse.ExpirationDate";
                    SLMResp.Online.BillMeLaterResponse.CreditLine = "BillMeLaterResponse.CreditLine";
                    SLMResp.Online.BillMeLaterResponse.PromoOffer = "BillMeLaterResponse.PromoOffer";
                    SLMResp.Online.BillMeLaterResponse.ApprovedAmount = "BillMeLaterResponse.ApprovedAmount";
                    SLMResp.Online.BillMeLaterResponse.ApprovedTermCode = "BillMeLaterResponse.ApprovedTermCode";
                    SLMResp.Online.BillMeLaterResponse.ShippingAddressIndicator = "BillMeLaterResponse.ShippingAddressIndicator";
                    SLMResp.Online.BillMeLaterResponse.LTVEstimator = "BillMeLaterResponse.LTVEstimator";
                    SLMResp.Online.BillMeLaterResponse.RiskEstimator = "BillMeLaterResponse.RiskEstimator";
                    SLMResp.Online.BillMeLaterResponse.RiskQueue = "BillMeLaterResponse.RiskQueue";
                    SLMResp.Online.BillMeLaterResponse.LeaseMasterAgreementID = "BillMeLaterResponse.LeaseMasterAgreementID";
                    SLMResp.Online.BillMeLaterResponse.LeaseDocRequirement = "BillMeLaterResponse.LeaseDocRequirement";
                }

                public BillMeLaterResponse()
                {
                }
            }

            public class BillToAddressResponse
            {
                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string NameText;

                public readonly static string AddressLine1;

                public readonly static string AddressLine2;

                public readonly static string CountryCode;

                public readonly static string City;

                public readonly static string State;

                public readonly static string PostalCode;

                static BillToAddressResponse()
                {
                    SLMResp.Online.BillToAddressResponse.TelephoneType = "BillToAddressResponse.TelephoneType";
                    SLMResp.Online.BillToAddressResponse.TelephoneNumber = "BillToAddressResponse.TelephoneNumber";
                    SLMResp.Online.BillToAddressResponse.NameText = "BillToAddressResponse.NameText";
                    SLMResp.Online.BillToAddressResponse.AddressLine1 = "BillToAddressResponse.AddressLine1";
                    SLMResp.Online.BillToAddressResponse.AddressLine2 = "BillToAddressResponse.AddressLine2";
                    SLMResp.Online.BillToAddressResponse.CountryCode = "BillToAddressResponse.CountryCode";
                    SLMResp.Online.BillToAddressResponse.City = "BillToAddressResponse.City";
                    SLMResp.Online.BillToAddressResponse.State = "BillToAddressResponse.State";
                    SLMResp.Online.BillToAddressResponse.PostalCode = "BillToAddressResponse.PostalCode";
                }

                public BillToAddressResponse()
                {
                }
            }

            public class BMLPrivateLabelResponse
            {
                public readonly static string ExpirationDate;

                public readonly static string CreditLine;

                public readonly static string PromoOffer;

                static BMLPrivateLabelResponse()
                {
                    SLMResp.Online.BMLPrivateLabelResponse.ExpirationDate = "BMLPrivateLabelResponse.ExpirationDate";
                    SLMResp.Online.BMLPrivateLabelResponse.CreditLine = "BMLPrivateLabelResponse.CreditLine";
                    SLMResp.Online.BMLPrivateLabelResponse.PromoOffer = "BMLPrivateLabelResponse.PromoOffer";
                }

                public BMLPrivateLabelResponse()
                {
                }
            }

            public class CardIssuingCountryStatusResponse
            {
                public readonly static string CountryStatus;

                public readonly static string CountryCode;

                static CardIssuingCountryStatusResponse()
                {
                    SLMResp.Online.CardIssuingCountryStatusResponse.CountryStatus = "CardIssuingCountryStatusResponse.CountryStatus";
                    SLMResp.Online.CardIssuingCountryStatusResponse.CountryCode = "CardIssuingCountryStatusResponse.CountryCode";
                }

                public CardIssuingCountryStatusResponse()
                {
                }
            }

            public class CardTypeIndicatorResponse1
            {
                public readonly static string FormatVersion;

                public readonly static string CountryCodeCountryOfIssuance;

                public readonly static string DurbinRegulated;

                public readonly static string CommercialCard;

                public readonly static string PrepaidCard;

                public readonly static string PayrollCard;

                public readonly static string HealthcareCard;

                public readonly static string AffluentCategory;

                public readonly static string SignatureDebit;

                public readonly static string PINlessDebit;

                static CardTypeIndicatorResponse1()
                {
                    SLMResp.Online.CardTypeIndicatorResponse1.FormatVersion = "CardTypeIndicatorResponse1.FormatVersion";
                    SLMResp.Online.CardTypeIndicatorResponse1.CountryCodeCountryOfIssuance = "CardTypeIndicatorResponse1.CountryCodeCountryOfIssuance";
                    SLMResp.Online.CardTypeIndicatorResponse1.DurbinRegulated = "CardTypeIndicatorResponse1.DurbinRegulated";
                    SLMResp.Online.CardTypeIndicatorResponse1.CommercialCard = "CardTypeIndicatorResponse1.CommercialCard";
                    SLMResp.Online.CardTypeIndicatorResponse1.PrepaidCard = "CardTypeIndicatorResponse1.PrepaidCard";
                    SLMResp.Online.CardTypeIndicatorResponse1.PayrollCard = "CardTypeIndicatorResponse1.PayrollCard";
                    SLMResp.Online.CardTypeIndicatorResponse1.HealthcareCard = "CardTypeIndicatorResponse1.HealthcareCard";
                    SLMResp.Online.CardTypeIndicatorResponse1.AffluentCategory = "CardTypeIndicatorResponse1.AffluentCategory";
                    SLMResp.Online.CardTypeIndicatorResponse1.SignatureDebit = "CardTypeIndicatorResponse1.SignatureDebit";
                    SLMResp.Online.CardTypeIndicatorResponse1.PINlessDebit = "CardTypeIndicatorResponse1.PINlessDebit";
                }

                public CardTypeIndicatorResponse1()
                {
                }
            }

            public class CardTypeIndicatorResponse2
            {
                public readonly static string FormatVersion;

                public readonly static string CountryCodeCountryOfIssuance;

                public readonly static string DurbinRegulated;

                public readonly static string CommercialCard;

                public readonly static string PrepaidCard;

                public readonly static string PayrollCard;

                public readonly static string HealthcareCard;

                public readonly static string AffluentCategory;

                public readonly static string SignatureDebit;

                public readonly static string PINlessDebit;

                public readonly static string Level3Eligible;

                static CardTypeIndicatorResponse2()
                {
                    SLMResp.Online.CardTypeIndicatorResponse2.FormatVersion = "CardTypeIndicatorResponse2.FormatVersion";
                    SLMResp.Online.CardTypeIndicatorResponse2.CountryCodeCountryOfIssuance = "CardTypeIndicatorResponse2.CountryCodeCountryOfIssuance";
                    SLMResp.Online.CardTypeIndicatorResponse2.DurbinRegulated = "CardTypeIndicatorResponse2.DurbinRegulated";
                    SLMResp.Online.CardTypeIndicatorResponse2.CommercialCard = "CardTypeIndicatorResponse2.CommercialCard";
                    SLMResp.Online.CardTypeIndicatorResponse2.PrepaidCard = "CardTypeIndicatorResponse2.PrepaidCard";
                    SLMResp.Online.CardTypeIndicatorResponse2.PayrollCard = "CardTypeIndicatorResponse2.PayrollCard";
                    SLMResp.Online.CardTypeIndicatorResponse2.HealthcareCard = "CardTypeIndicatorResponse2.HealthcareCard";
                    SLMResp.Online.CardTypeIndicatorResponse2.AffluentCategory = "CardTypeIndicatorResponse2.AffluentCategory";
                    SLMResp.Online.CardTypeIndicatorResponse2.SignatureDebit = "CardTypeIndicatorResponse2.SignatureDebit";
                    SLMResp.Online.CardTypeIndicatorResponse2.PINlessDebit = "CardTypeIndicatorResponse2.PINlessDebit";
                    SLMResp.Online.CardTypeIndicatorResponse2.Level3Eligible = "CardTypeIndicatorResponse2.Level3Eligible";
                }

                public CardTypeIndicatorResponse2()
                {
                }
            }

            public class CashBackResponse
            {
                public readonly static string CashBackAmountRequested;

                public readonly static string CashBackAmountApproved;

                static CashBackResponse()
                {
                    SLMResp.Online.CashBackResponse.CashBackAmountRequested = "CashBackResponse.CashBackAmountRequested";
                    SLMResp.Online.CashBackResponse.CashBackAmountApproved = "CashBackResponse.CashBackAmountApproved";
                }

                public CashBackResponse()
                {
                }
            }

            public class ChipEMVData
            {
                public readonly static string ChipData;

                static ChipEMVData()
                {
                    SLMResp.Online.ChipEMVData.ChipData = "ChipEMVData.ChipData";
                }

                public ChipEMVData()
                {
                }
            }

            public class CrossCurrency
            {
                public readonly static string OptOutIndicator;

                public readonly static string RateHandlingIndicator;

                public readonly static string RateIdentifier;

                public readonly static string ExchangeRate;

                public readonly static string PresentmentCurrency;

                public readonly static string SettlementCurrency;

                public readonly static string DefaultRateIndicator;

                public readonly static string RateStatus;

                static CrossCurrency()
                {
                    SLMResp.Online.CrossCurrency.OptOutIndicator = "CrossCurrency.OptOutIndicator";
                    SLMResp.Online.CrossCurrency.RateHandlingIndicator = "CrossCurrency.RateHandlingIndicator";
                    SLMResp.Online.CrossCurrency.RateIdentifier = "CrossCurrency.RateIdentifier";
                    SLMResp.Online.CrossCurrency.ExchangeRate = "CrossCurrency.ExchangeRate";
                    SLMResp.Online.CrossCurrency.PresentmentCurrency = "CrossCurrency.PresentmentCurrency";
                    SLMResp.Online.CrossCurrency.SettlementCurrency = "CrossCurrency.SettlementCurrency";
                    SLMResp.Online.CrossCurrency.DefaultRateIndicator = "CrossCurrency.DefaultRateIndicator";
                    SLMResp.Online.CrossCurrency.RateStatus = "CrossCurrency.RateStatus";
                }

                public CrossCurrency()
                {
                }
            }

            public class CurrentBalanceResponse
            {
                public readonly static string PaymentPurchaseIndicator;

                public readonly static string CSRTelephoneNumber;

                public readonly static string POSStatusDesc;

                public readonly static string CurrentAccountBalance;

                public readonly static string LastStatementBalance;

                public readonly static string OpenToBuy;

                public readonly static string MinPaymentDue;

                public readonly static string PaymentDueDate;

                static CurrentBalanceResponse()
                {
                    SLMResp.Online.CurrentBalanceResponse.PaymentPurchaseIndicator = "CurrentBalanceResponse.PaymentPurchaseIndicator";
                    SLMResp.Online.CurrentBalanceResponse.CSRTelephoneNumber = "CurrentBalanceResponse.CSRTelephoneNumber";
                    SLMResp.Online.CurrentBalanceResponse.POSStatusDesc = "CurrentBalanceResponse.POSStatusDesc";
                    SLMResp.Online.CurrentBalanceResponse.CurrentAccountBalance = "CurrentBalanceResponse.CurrentAccountBalance";
                    SLMResp.Online.CurrentBalanceResponse.LastStatementBalance = "CurrentBalanceResponse.LastStatementBalance";
                    SLMResp.Online.CurrentBalanceResponse.OpenToBuy = "CurrentBalanceResponse.OpenToBuy";
                    SLMResp.Online.CurrentBalanceResponse.MinPaymentDue = "CurrentBalanceResponse.MinPaymentDue";
                    SLMResp.Online.CurrentBalanceResponse.PaymentDueDate = "CurrentBalanceResponse.PaymentDueDate";
                }

                public CurrentBalanceResponse()
                {
                }
            }

            public class DebitResponse
            {
                public readonly static string TotalAmount;

                public readonly static string SurchargeAmount;

                public readonly static string DebitTraceNumber;

                static DebitResponse()
                {
                    SLMResp.Online.DebitResponse.TotalAmount = "DebitResponse.TotalAmount";
                    SLMResp.Online.DebitResponse.SurchargeAmount = "DebitResponse.SurchargeAmount";
                    SLMResp.Online.DebitResponse.DebitTraceNumber = "DebitResponse.DebitTraceNumber";
                }

                public DebitResponse()
                {
                }
            }

            public class DebitRoutingResponse
            {
                public readonly static string DebitRoutingData;

                static DebitRoutingResponse()
                {
                    SLMResp.Online.DebitRoutingResponse.DebitRoutingData = "DebitRoutingResponse.DebitRoutingData";
                }

                public DebitRoutingResponse()
                {
                }
            }

            public class DigitalPANResponse
            {
                public readonly static string TokenAssuranceLevel;

                public readonly static string AccountStatus;

                public readonly static string TokenRequestorID;

                static DigitalPANResponse()
                {
                    SLMResp.Online.DigitalPANResponse.TokenAssuranceLevel = "DigitalPANResponse.TokenAssuranceLevel";
                    SLMResp.Online.DigitalPANResponse.AccountStatus = "DigitalPANResponse.AccountStatus";
                    SLMResp.Online.DigitalPANResponse.TokenRequestorID = "DigitalPANResponse.TokenRequestorID";
                }

                public DigitalPANResponse()
                {
                }
            }

            public class DiscoverAuthentication
            {
                public readonly static string CAVV;

                static DiscoverAuthentication()
                {
                    SLMResp.Online.DiscoverAuthentication.CAVV = "DiscoverAuthentication.CAVV";
                }

                public DiscoverAuthentication()
                {
                }
            }

            public class DiscoverExtendedAuthDataResponse
            {
                public readonly static string MerchantCategoryCode;

                public readonly static string PANEntryMode;

                public readonly static string SystemTraceAudit;

                public readonly static string AuthResponseCode;

                public readonly static string NetworkReferenceID;

                public readonly static string AVSResponseCode;

                static DiscoverExtendedAuthDataResponse()
                {
                    SLMResp.Online.DiscoverExtendedAuthDataResponse.MerchantCategoryCode = "DiscoverExtendedAuthDataResponse.MerchantCategoryCode";
                    SLMResp.Online.DiscoverExtendedAuthDataResponse.PANEntryMode = "DiscoverExtendedAuthDataResponse.PANEntryMode";
                    SLMResp.Online.DiscoverExtendedAuthDataResponse.SystemTraceAudit = "DiscoverExtendedAuthDataResponse.SystemTraceAudit";
                    SLMResp.Online.DiscoverExtendedAuthDataResponse.AuthResponseCode = "DiscoverExtendedAuthDataResponse.AuthResponseCode";
                    SLMResp.Online.DiscoverExtendedAuthDataResponse.NetworkReferenceID = "DiscoverExtendedAuthDataResponse.NetworkReferenceID";
                    SLMResp.Online.DiscoverExtendedAuthDataResponse.AVSResponseCode = "DiscoverExtendedAuthDataResponse.AVSResponseCode";
                }

                public DiscoverExtendedAuthDataResponse()
                {
                }
            }

            public class EchoResponse
            {
                public readonly static string MerchantEcho;

                static EchoResponse()
                {
                    SLMResp.Online.EchoResponse.MerchantEcho = "EchoResponse.MerchantEcho";
                }

                public EchoResponse()
                {
                }
            }

            public class ECPAdvancedVerificationInformation
            {
                public readonly static string DataLength;

                public readonly static string ConsumerAccountAddDelFlag;

                public readonly static string ConsumerAccountAddDelDate;

                public readonly static string AccountStatusDate;

                static ECPAdvancedVerificationInformation()
                {
                    SLMResp.Online.ECPAdvancedVerificationInformation.DataLength = "ECPAdvancedVerificationInformation.DataLength";
                    SLMResp.Online.ECPAdvancedVerificationInformation.ConsumerAccountAddDelFlag = "ECPAdvancedVerificationInformation.ConsumerAccountAddDelFlag";
                    SLMResp.Online.ECPAdvancedVerificationInformation.ConsumerAccountAddDelDate = "ECPAdvancedVerificationInformation.ConsumerAccountAddDelDate";
                    SLMResp.Online.ECPAdvancedVerificationInformation.AccountStatusDate = "ECPAdvancedVerificationInformation.AccountStatusDate";
                }

                public ECPAdvancedVerificationInformation()
                {
                }
            }

            public class ECPAdvancedVerificationResponse
            {
                public readonly static string RecordType;

                public readonly static string AccountStatusCode;

                public readonly static string AOAConditionCode;

                public readonly static string FullNameMatch;

                public readonly static string FirstNameMatch;

                public readonly static string LastNameMatch;

                public readonly static string MiddleNameOrInitialMatch;

                public readonly static string BusinessNameMatch;

                public readonly static string Address1Address2Match;

                public readonly static string CityMatch;

                public readonly static string StateMatch;

                public readonly static string ZipCodeMatch;

                public readonly static string PhoneMatch;

                public readonly static string TaxIdentificationNumberMatch;

                public readonly static string DOBMatch;

                public readonly static string IDTypeMatch;

                public readonly static string IDNumberMatch;

                public readonly static string IDStateMatch;

                static ECPAdvancedVerificationResponse()
                {
                    SLMResp.Online.ECPAdvancedVerificationResponse.RecordType = "ECPAdvancedVerificationResponse.RecordType";
                    SLMResp.Online.ECPAdvancedVerificationResponse.AccountStatusCode = "ECPAdvancedVerificationResponse.AccountStatusCode";
                    SLMResp.Online.ECPAdvancedVerificationResponse.AOAConditionCode = "ECPAdvancedVerificationResponse.AOAConditionCode";
                    SLMResp.Online.ECPAdvancedVerificationResponse.FullNameMatch = "ECPAdvancedVerificationResponse.FullNameMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.FirstNameMatch = "ECPAdvancedVerificationResponse.FirstNameMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.LastNameMatch = "ECPAdvancedVerificationResponse.LastNameMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.MiddleNameOrInitialMatch = "ECPAdvancedVerificationResponse.MiddleNameOrInitialMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.BusinessNameMatch = "ECPAdvancedVerificationResponse.BusinessNameMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.Address1Address2Match = "ECPAdvancedVerificationResponse.Address1Address2Match";
                    SLMResp.Online.ECPAdvancedVerificationResponse.CityMatch = "ECPAdvancedVerificationResponse.CityMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.StateMatch = "ECPAdvancedVerificationResponse.StateMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.ZipCodeMatch = "ECPAdvancedVerificationResponse.ZipCodeMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.PhoneMatch = "ECPAdvancedVerificationResponse.PhoneMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.TaxIdentificationNumberMatch = "ECPAdvancedVerificationResponse.TaxIdentificationNumberMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.DOBMatch = "ECPAdvancedVerificationResponse.DOBMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.IDTypeMatch = "ECPAdvancedVerificationResponse.IDTypeMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.IDNumberMatch = "ECPAdvancedVerificationResponse.IDNumberMatch";
                    SLMResp.Online.ECPAdvancedVerificationResponse.IDStateMatch = "ECPAdvancedVerificationResponse.IDStateMatch";
                }

                public ECPAdvancedVerificationResponse()
                {
                }
            }

            public class ElectronicBenefitsTransferResponse
            {
                public readonly static string Count;

                static ElectronicBenefitsTransferResponse()
                {
                    SLMResp.Online.ElectronicBenefitsTransferResponse.Count = "ElectronicBenefitsTransferResponse.Count";
                }

                public ElectronicBenefitsTransferResponse()
                {
                }

                public class Balance
                {
                    public readonly static string BalanceTypeAmount;

                    static Balance()
                    {
                        SLMResp.Online.ElectronicBenefitsTransferResponse.Balance.BalanceTypeAmount = "Balance.BalanceTypeAmount";
                    }

                    public Balance()
                    {
                    }
                }
            }

            public class EmailAddressResponse
            {
                public readonly static string AddressSubType;

                public readonly static string EmailAddress;

                static EmailAddressResponse()
                {
                    SLMResp.Online.EmailAddressResponse.AddressSubType = "EmailAddressResponse.AddressSubType";
                    SLMResp.Online.EmailAddressResponse.EmailAddress = "EmailAddressResponse.EmailAddress";
                }

                public EmailAddressResponse()
                {
                }
            }

            public class EuropeanDirectDebit2Response
            {
                public readonly static string IBAN;

                public readonly static string BIC;

                static EuropeanDirectDebit2Response()
                {
                    SLMResp.Online.EuropeanDirectDebit2Response.IBAN = "EuropeanDirectDebit2Response.IBAN";
                    SLMResp.Online.EuropeanDirectDebit2Response.BIC = "EuropeanDirectDebit2Response.BIC";
                }

                public EuropeanDirectDebit2Response()
                {
                }
            }

            public class FraudScoring1Response
            {
                public readonly static string FraudStatusCode;

                public readonly static string RiskInquiryTransactionID;

                public readonly static string AutoDecisionResponse;

                public readonly static string RiskScore;

                public readonly static string KaptchaMatchFlag;

                static FraudScoring1Response()
                {
                    SLMResp.Online.FraudScoring1Response.FraudStatusCode = "FraudScoring1Response.FraudStatusCode";
                    SLMResp.Online.FraudScoring1Response.RiskInquiryTransactionID = "FraudScoring1Response.RiskInquiryTransactionID";
                    SLMResp.Online.FraudScoring1Response.AutoDecisionResponse = "FraudScoring1Response.AutoDecisionResponse";
                    SLMResp.Online.FraudScoring1Response.RiskScore = "FraudScoring1Response.RiskScore";
                    SLMResp.Online.FraudScoring1Response.KaptchaMatchFlag = "FraudScoring1Response.KaptchaMatchFlag";
                }

                public FraudScoring1Response()
                {
                }
            }

            public class FraudScoring2Response
            {
                public readonly static string FraudStatusCode;

                public readonly static string RiskInquiryTransactionID;

                public readonly static string AutoDecisionResponse;

                public readonly static string RiskScore;

                public readonly static string WorstCountry;

                public readonly static string CustomerRegion;

                public readonly static string PaymentBrand;

                public readonly static string Velocity14Days;

                public readonly static string Velocity6Hours;

                public readonly static string CustomerNetwork;

                public readonly static string NumberTransactionsWithDevice;

                public readonly static string NumberDevicesWithTransaction;

                public readonly static string NumberTransactionsWithCard;

                public readonly static string NumberCardsWithTransaction;

                public readonly static string NumberTransactionsWithEmail;

                public readonly static string NumberEmailsWithTransaction;

                public readonly static string KaptchMatchFlag;

                public readonly static string KaptchaMatchFlag;

                public readonly static string DeviceLayers;

                public readonly static string DeviceFingerprint;

                public readonly static string CustomerTimeZone;

                public readonly static string CustomerLocalDateTime;

                public readonly static string DeviceRegion;

                public readonly static string DeviceCountry;

                public readonly static string ProxyStatus;

                public readonly static string JavascriptStatus;

                public readonly static string FlashStatus;

                public readonly static string CookieStatus;

                public readonly static string BrowserCountry;

                public readonly static string BrowserLanguage;

                public readonly static string MobileDeviceIndicator;

                public readonly static string MobileDeviceType;

                public readonly static string MobileWirelessIndicator;

                public readonly static string VoiceDevice;

                public readonly static string PCRemoteIndicator;

                static FraudScoring2Response()
                {
                    SLMResp.Online.FraudScoring2Response.FraudStatusCode = "FraudScoring2Response.FraudStatusCode";
                    SLMResp.Online.FraudScoring2Response.RiskInquiryTransactionID = "FraudScoring2Response.RiskInquiryTransactionID";
                    SLMResp.Online.FraudScoring2Response.AutoDecisionResponse = "FraudScoring2Response.AutoDecisionResponse";
                    SLMResp.Online.FraudScoring2Response.RiskScore = "FraudScoring2Response.RiskScore";
                    SLMResp.Online.FraudScoring2Response.WorstCountry = "FraudScoring2Response.WorstCountry";
                    SLMResp.Online.FraudScoring2Response.CustomerRegion = "FraudScoring2Response.CustomerRegion";
                    SLMResp.Online.FraudScoring2Response.PaymentBrand = "FraudScoring2Response.PaymentBrand";
                    SLMResp.Online.FraudScoring2Response.Velocity14Days = "FraudScoring2Response.Velocity14Days";
                    SLMResp.Online.FraudScoring2Response.Velocity6Hours = "FraudScoring2Response.Velocity6Hours";
                    SLMResp.Online.FraudScoring2Response.CustomerNetwork = "FraudScoring2Response.CustomerNetwork";
                    SLMResp.Online.FraudScoring2Response.NumberTransactionsWithDevice = "FraudScoring2Response.NumberTransactionsWithDevice";
                    SLMResp.Online.FraudScoring2Response.NumberDevicesWithTransaction = "FraudScoring2Response.NumberDevicesWithTransaction";
                    SLMResp.Online.FraudScoring2Response.NumberTransactionsWithCard = "FraudScoring2Response.NumberTransactionsWithCard";
                    SLMResp.Online.FraudScoring2Response.NumberCardsWithTransaction = "FraudScoring2Response.NumberCardsWithTransaction";
                    SLMResp.Online.FraudScoring2Response.NumberTransactionsWithEmail = "FraudScoring2Response.NumberTransactionsWithEmail";
                    SLMResp.Online.FraudScoring2Response.NumberEmailsWithTransaction = "FraudScoring2Response.NumberEmailsWithTransaction";
                    SLMResp.Online.FraudScoring2Response.KaptchMatchFlag = "FraudScoring2Response.KaptchMatchFlag";
                    SLMResp.Online.FraudScoring2Response.KaptchaMatchFlag = "FraudScoring2Response.KaptchaMatchFlag";
                    SLMResp.Online.FraudScoring2Response.DeviceLayers = "FraudScoring2Response.DeviceLayers";
                    SLMResp.Online.FraudScoring2Response.DeviceFingerprint = "FraudScoring2Response.DeviceFingerprint";
                    SLMResp.Online.FraudScoring2Response.CustomerTimeZone = "FraudScoring2Response.CustomerTimeZone";
                    SLMResp.Online.FraudScoring2Response.CustomerLocalDateTime = "FraudScoring2Response.CustomerLocalDateTime";
                    SLMResp.Online.FraudScoring2Response.DeviceRegion = "FraudScoring2Response.DeviceRegion";
                    SLMResp.Online.FraudScoring2Response.DeviceCountry = "FraudScoring2Response.DeviceCountry";
                    SLMResp.Online.FraudScoring2Response.ProxyStatus = "FraudScoring2Response.ProxyStatus";
                    SLMResp.Online.FraudScoring2Response.JavascriptStatus = "FraudScoring2Response.JavascriptStatus";
                    SLMResp.Online.FraudScoring2Response.FlashStatus = "FraudScoring2Response.FlashStatus";
                    SLMResp.Online.FraudScoring2Response.CookieStatus = "FraudScoring2Response.CookieStatus";
                    SLMResp.Online.FraudScoring2Response.BrowserCountry = "FraudScoring2Response.BrowserCountry";
                    SLMResp.Online.FraudScoring2Response.BrowserLanguage = "FraudScoring2Response.BrowserLanguage";
                    SLMResp.Online.FraudScoring2Response.MobileDeviceIndicator = "FraudScoring2Response.MobileDeviceIndicator";
                    SLMResp.Online.FraudScoring2Response.MobileDeviceType = "FraudScoring2Response.MobileDeviceType";
                    SLMResp.Online.FraudScoring2Response.MobileWirelessIndicator = "FraudScoring2Response.MobileWirelessIndicator";
                    SLMResp.Online.FraudScoring2Response.VoiceDevice = "FraudScoring2Response.VoiceDevice";
                    SLMResp.Online.FraudScoring2Response.PCRemoteIndicator = "FraudScoring2Response.PCRemoteIndicator";
                }

                public FraudScoring2Response()
                {
                }
            }

            public class GiftBlockResponse
            {
                public readonly static string OriginalRefNumber;

                public readonly static string FailedAccountNumber;

                static GiftBlockResponse()
                {
                    SLMResp.Online.GiftBlockResponse.OriginalRefNumber = "GiftBlockResponse.OriginalRefNumber";
                    SLMResp.Online.GiftBlockResponse.FailedAccountNumber = "GiftBlockResponse.FailedAccountNumber";
                }

                public GiftBlockResponse()
                {
                }
            }

            public class GiftResponse
            {
                public readonly static string CurrentBalance;

                public readonly static string PreviousBalance;

                public readonly static string ExpirationDate;

                public readonly static string RedemptionAmount;

                public readonly static string OriginalRefNumber;

                static GiftResponse()
                {
                    SLMResp.Online.GiftResponse.CurrentBalance = "GiftResponse.CurrentBalance";
                    SLMResp.Online.GiftResponse.PreviousBalance = "GiftResponse.PreviousBalance";
                    SLMResp.Online.GiftResponse.ExpirationDate = "GiftResponse.ExpirationDate";
                    SLMResp.Online.GiftResponse.RedemptionAmount = "GiftResponse.RedemptionAmount";
                    SLMResp.Online.GiftResponse.OriginalRefNumber = "GiftResponse.OriginalRefNumber";
                }

                public GiftResponse()
                {
                }
            }

            public class GoodsSold
            {
                public readonly static string GoodsSold_;

                static GoodsSold()
                {
                    SLMResp.Online.GoodsSold.GoodsSold_ = "GoodsSold.GoodsSold_";
                }

                public GoodsSold()
                {
                }
            }

            public class JCBAuthentication
            {
                public readonly static string RecordType;

                public readonly static string AEVV;

                public readonly static string TransactionID;

                static JCBAuthentication()
                {
                    SLMResp.Online.JCBAuthentication.RecordType = "JCBAuthentication.RecordType";
                    SLMResp.Online.JCBAuthentication.AEVV = "JCBAuthentication.AEVV";
                    SLMResp.Online.JCBAuthentication.TransactionID = "JCBAuthentication.TransactionID";
                }

                public JCBAuthentication()
                {
                }
            }

            public class MasterCardDigitalWallet
            {
                public readonly static string DigitalWalletIndicator;

                public readonly static string StagedDigitalWalletIndicator;

                public readonly static string MasterPassDigitalWalletID;

                static MasterCardDigitalWallet()
                {
                    SLMResp.Online.MasterCardDigitalWallet.DigitalWalletIndicator = "MasterCardDigitalWallet.DigitalWalletIndicator";
                    SLMResp.Online.MasterCardDigitalWallet.StagedDigitalWalletIndicator = "MasterCardDigitalWallet.StagedDigitalWalletIndicator";
                    SLMResp.Online.MasterCardDigitalWallet.MasterPassDigitalWalletID = "MasterCardDigitalWallet.MasterPassDigitalWalletID";
                }

                public MasterCardDigitalWallet()
                {
                }
            }

            public class MasterCardExtendedAuthDataResponse
            {
                public readonly static string AuthorizedAmount;

                public readonly static string MerchantCategoryCode;

                public readonly static string POSEntryMode;

                public readonly static string AuthResponseCode;

                public readonly static string FinancialNetworkCode;

                public readonly static string BanknetRefNumber;

                public readonly static string BanknetDate;

                public readonly static string AVSResponseCode;

                static MasterCardExtendedAuthDataResponse()
                {
                    SLMResp.Online.MasterCardExtendedAuthDataResponse.AuthorizedAmount = "MasterCardExtendedAuthDataResponse.AuthorizedAmount";
                    SLMResp.Online.MasterCardExtendedAuthDataResponse.MerchantCategoryCode = "MasterCardExtendedAuthDataResponse.MerchantCategoryCode";
                    SLMResp.Online.MasterCardExtendedAuthDataResponse.POSEntryMode = "MasterCardExtendedAuthDataResponse.POSEntryMode";
                    SLMResp.Online.MasterCardExtendedAuthDataResponse.AuthResponseCode = "MasterCardExtendedAuthDataResponse.AuthResponseCode";
                    SLMResp.Online.MasterCardExtendedAuthDataResponse.FinancialNetworkCode = "MasterCardExtendedAuthDataResponse.FinancialNetworkCode";
                    SLMResp.Online.MasterCardExtendedAuthDataResponse.BanknetRefNumber = "MasterCardExtendedAuthDataResponse.BanknetRefNumber";
                    SLMResp.Online.MasterCardExtendedAuthDataResponse.BanknetDate = "MasterCardExtendedAuthDataResponse.BanknetDate";
                    SLMResp.Online.MasterCardExtendedAuthDataResponse.AVSResponseCode = "MasterCardExtendedAuthDataResponse.AVSResponseCode";
                }

                public MasterCardExtendedAuthDataResponse()
                {
                }
            }

            public class MemberToMember
            {
                public readonly static string MemberToMemberIdentifier;

                public readonly static string MemberToMemberData;

                static MemberToMember()
                {
                    SLMResp.Online.MemberToMember.MemberToMemberIdentifier = "MemberToMember.MemberToMemberIdentifier";
                    SLMResp.Online.MemberToMember.MemberToMemberData = "MemberToMember.MemberToMemberData";
                }

                public MemberToMember()
                {
                }
            }

            public class MerchantHeartBeatResponse
            {
                public readonly static string SequenceNumber;

                public readonly static string GMTTime;

                static MerchantHeartBeatResponse()
                {
                    SLMResp.Online.MerchantHeartBeatResponse.SequenceNumber = "MerchantHeartBeatResponse.SequenceNumber";
                    SLMResp.Online.MerchantHeartBeatResponse.GMTTime = "MerchantHeartBeatResponse.GMTTime";
                }

                public MerchantHeartBeatResponse()
                {
                }
            }

            public class MessageType
            {
                public readonly static string RecordType;

                public readonly static string MessageType_;

                public readonly static string StoredCredentialFlag;

                public readonly static string SubmittedTransactionID;

                public readonly static string ResponseTransactionID;

                public readonly static string OriginalTransactionAmount;

                static MessageType()
                {
                    SLMResp.Online.MessageType.RecordType = "MessageType.RecordType";
                    SLMResp.Online.MessageType.MessageType_ = "MessageType.MessageType_";
                    SLMResp.Online.MessageType.StoredCredentialFlag = "MessageType.StoredCredentialFlag";
                    SLMResp.Online.MessageType.SubmittedTransactionID = "MessageType.SubmittedTransactionID";
                    SLMResp.Online.MessageType.ResponseTransactionID = "MessageType.ResponseTransactionID";
                    SLMResp.Online.MessageType.OriginalTransactionAmount = "MessageType.OriginalTransactionAmount";
                }

                public MessageType()
                {
                }
            }

            public class MobilePOSDeviceInformation
            {
                public readonly static string DeviceType;

                static MobilePOSDeviceInformation()
                {
                    SLMResp.Online.MobilePOSDeviceInformation.DeviceType = "MobilePOSDeviceInformation.DeviceType";
                }

                public MobilePOSDeviceInformation()
                {
                }
            }

            public class MoneyPakResponse
            {
                public readonly static string OriginalTransactionID;

                public readonly static string ConfirmationID;

                public readonly static string MoneyPakAccountNumber;

                static MoneyPakResponse()
                {
                    SLMResp.Online.MoneyPakResponse.OriginalTransactionID = "MoneyPakResponse.OriginalTransactionID";
                    SLMResp.Online.MoneyPakResponse.ConfirmationID = "MoneyPakResponse.ConfirmationID";
                    SLMResp.Online.MoneyPakResponse.MoneyPakAccountNumber = "MoneyPakResponse.MoneyPakAccountNumber";
                }

                public MoneyPakResponse()
                {
                }
            }

            public class OpenToBuyResponse
            {
                public readonly static string OpenToBuy;

                static OpenToBuyResponse()
                {
                    SLMResp.Online.OpenToBuyResponse.OpenToBuy = "OpenToBuyResponse.OpenToBuy";
                }

                public OpenToBuyResponse()
                {
                }
            }

            public class PartialAuthResponse
            {
                public readonly static string CurrentBalance;

                public readonly static string RedemptionAmount;

                static PartialAuthResponse()
                {
                    SLMResp.Online.PartialAuthResponse.CurrentBalance = "PartialAuthResponse.CurrentBalance";
                    SLMResp.Online.PartialAuthResponse.RedemptionAmount = "PartialAuthResponse.RedemptionAmount";
                }

                public PartialAuthResponse()
                {
                }
            }

            public class PaymentDevice
            {
                public readonly static string PaymentDevice_;

                static PaymentDevice()
                {
                    SLMResp.Online.PaymentDevice.PaymentDevice_ = "PaymentDevice.PaymentDevice_";
                }

                public PaymentDevice()
                {
                }
            }

            public class PersonalInformation2Response
            {
                public readonly static string PayerID;

                public readonly static string EmailAddressStatus;

                public readonly static string AddressStatus;

                static PersonalInformation2Response()
                {
                    SLMResp.Online.PersonalInformation2Response.PayerID = "PersonalInformation2Response.PayerID";
                    SLMResp.Online.PersonalInformation2Response.EmailAddressStatus = "PersonalInformation2Response.EmailAddressStatus";
                    SLMResp.Online.PersonalInformation2Response.AddressStatus = "PersonalInformation2Response.AddressStatus";
                }

                public PersonalInformation2Response()
                {
                }
            }

            public class RealTimeAccountUpdater
            {
                public readonly static string UpdateResponse;

                public readonly static string NewAccountNumber;

                public readonly static string NewExpirationDate;

                public readonly static string NewMethodOfPayment;

                static RealTimeAccountUpdater()
                {
                    SLMResp.Online.RealTimeAccountUpdater.UpdateResponse = "RealTimeAccountUpdater.UpdateResponse";
                    SLMResp.Online.RealTimeAccountUpdater.NewAccountNumber = "RealTimeAccountUpdater.NewAccountNumber";
                    SLMResp.Online.RealTimeAccountUpdater.NewExpirationDate = "RealTimeAccountUpdater.NewExpirationDate";
                    SLMResp.Online.RealTimeAccountUpdater.NewMethodOfPayment = "RealTimeAccountUpdater.NewMethodOfPayment";
                }

                public RealTimeAccountUpdater()
                {
                }
            }

            public class RecipientData
            {
                public readonly static string DateofBirthofPrimaryRecipient;

                public readonly static string MaskedAccountNumber;

                public readonly static string PartialPostalCode;

                public readonly static string LastNameofPrimaryRecipient;

                static RecipientData()
                {
                    SLMResp.Online.RecipientData.DateofBirthofPrimaryRecipient = "RecipientData.DateofBirthofPrimaryRecipient";
                    SLMResp.Online.RecipientData.MaskedAccountNumber = "RecipientData.MaskedAccountNumber";
                    SLMResp.Online.RecipientData.PartialPostalCode = "RecipientData.PartialPostalCode";
                    SLMResp.Online.RecipientData.LastNameofPrimaryRecipient = "RecipientData.LastNameofPrimaryRecipient";
                }

                public RecipientData()
                {
                }
            }

            public class Response
            {
                public readonly static string MerchantOrderNumber;

                public readonly static string ResponseReasonCode;

                public readonly static string ResponseDate;

                public readonly static string AuthVerificationCode;

                public readonly static string AVSAAV;

                public readonly static string CardSecurityValue;

                public readonly static string AccountNumber;

                public readonly static string ExpirationDate;

                public readonly static string MOP;

                public readonly static string MethodOfPayment;

                public readonly static string RecurringPaymentAdviceCode;

                public readonly static string PaymentAdviceCode;

                public readonly static string CAVV;

                public readonly static string Amount;

                static Response()
                {
                    SLMResp.Online.Response.MerchantOrderNumber = "MerchantOrderNumber";
                    SLMResp.Online.Response.ResponseReasonCode = "ResponseReasonCode";
                    SLMResp.Online.Response.ResponseDate = "ResponseDate";
                    SLMResp.Online.Response.AuthVerificationCode = "AuthVerificationCode";
                    SLMResp.Online.Response.AVSAAV = "AVSAAV";
                    SLMResp.Online.Response.CardSecurityValue = "CardSecurityValue";
                    SLMResp.Online.Response.AccountNumber = "AccountNumber";
                    SLMResp.Online.Response.ExpirationDate = "ExpirationDate";
                    SLMResp.Online.Response.MOP = "MOP";
                    SLMResp.Online.Response.MethodOfPayment = "MethodOfPayment";
                    SLMResp.Online.Response.RecurringPaymentAdviceCode = "RecurringPaymentAdviceCode";
                    SLMResp.Online.Response.PaymentAdviceCode = "PaymentAdviceCode";
                    SLMResp.Online.Response.CAVV = "CAVV";
                    SLMResp.Online.Response.Amount = "Amount";
                }

                public Response()
                {
                }
            }

            public class ResponseInformationResponse
            {
                public readonly static string AVSResponseCode;

                public readonly static string PostalCodeVerificationResponse;

                public readonly static string StreetVerificationResponse;

                public readonly static string NameVerificationResponse;

                public readonly static string PhoneVerificationResponse;

                public readonly static string EmailVerificationResponse;

                static ResponseInformationResponse()
                {
                    SLMResp.Online.ResponseInformationResponse.AVSResponseCode = "ResponseInformationResponse.AVSResponseCode";
                    SLMResp.Online.ResponseInformationResponse.PostalCodeVerificationResponse = "ResponseInformationResponse.PostalCodeVerificationResponse";
                    SLMResp.Online.ResponseInformationResponse.StreetVerificationResponse = "ResponseInformationResponse.StreetVerificationResponse";
                    SLMResp.Online.ResponseInformationResponse.NameVerificationResponse = "ResponseInformationResponse.NameVerificationResponse";
                    SLMResp.Online.ResponseInformationResponse.PhoneVerificationResponse = "ResponseInformationResponse.PhoneVerificationResponse";
                    SLMResp.Online.ResponseInformationResponse.EmailVerificationResponse = "ResponseInformationResponse.EmailVerificationResponse";
                }

                public ResponseInformationResponse()
                {
                }
            }

            public class ResponseMessageResponse
            {
                public readonly static string MessageText;

                static ResponseMessageResponse()
                {
                    SLMResp.Online.ResponseMessageResponse.MessageText = "ResponseMessageResponse.MessageText";
                }

                public ResponseMessageResponse()
                {
                }
            }

            public class ReversalReason
            {
                public readonly static string ResponseDate;

                public readonly static string AuthorizationCode;

                public readonly static string DebitTraceNumber;

                public readonly static string ReversalReasonCode;

                static ReversalReason()
                {
                    SLMResp.Online.ReversalReason.ResponseDate = "ReversalReason.ResponseDate";
                    SLMResp.Online.ReversalReason.AuthorizationCode = "ReversalReason.AuthorizationCode";
                    SLMResp.Online.ReversalReason.DebitTraceNumber = "ReversalReason.DebitTraceNumber";
                    SLMResp.Online.ReversalReason.ReversalReasonCode = "ReversalReason.ReversalReasonCode";
                }

                public ReversalReason()
                {
                }
            }

            public class RulesTriggeredResponse
            {
                public readonly static string RulesTriggered;

                static RulesTriggeredResponse()
                {
                    SLMResp.Online.RulesTriggeredResponse.RulesTriggered = "RulesTriggeredResponse.RulesTriggered";
                }

                public RulesTriggeredResponse()
                {
                }
            }

            public class ShipToAddressResponse
            {
                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string NameText;

                public readonly static string AddressLine1;

                public readonly static string AddressLine2;

                public readonly static string CountryCode;

                public readonly static string City;

                public readonly static string State;

                public readonly static string PostalCode;

                static ShipToAddressResponse()
                {
                    SLMResp.Online.ShipToAddressResponse.TelephoneType = "ShipToAddressResponse.TelephoneType";
                    SLMResp.Online.ShipToAddressResponse.TelephoneNumber = "ShipToAddressResponse.TelephoneNumber";
                    SLMResp.Online.ShipToAddressResponse.NameText = "ShipToAddressResponse.NameText";
                    SLMResp.Online.ShipToAddressResponse.AddressLine1 = "ShipToAddressResponse.AddressLine1";
                    SLMResp.Online.ShipToAddressResponse.AddressLine2 = "ShipToAddressResponse.AddressLine2";
                    SLMResp.Online.ShipToAddressResponse.CountryCode = "ShipToAddressResponse.CountryCode";
                    SLMResp.Online.ShipToAddressResponse.City = "ShipToAddressResponse.City";
                    SLMResp.Online.ShipToAddressResponse.State = "ShipToAddressResponse.State";
                    SLMResp.Online.ShipToAddressResponse.PostalCode = "ShipToAddressResponse.PostalCode";
                }

                public ShipToAddressResponse()
                {
                }
            }

            public class ShopwithPointsInformationResponse
            {
                public readonly static string RewardsTransactionAmount;

                public readonly static string RewardsCurrency;

                public readonly static string ShopwithPointsTraceNumber;

                static ShopwithPointsInformationResponse()
                {
                    SLMResp.Online.ShopwithPointsInformationResponse.RewardsTransactionAmount = "ShopwithPointsInformationResponse.RewardsTransactionAmount";
                    SLMResp.Online.ShopwithPointsInformationResponse.RewardsCurrency = "ShopwithPointsInformationResponse.RewardsCurrency";
                    SLMResp.Online.ShopwithPointsInformationResponse.ShopwithPointsTraceNumber = "ShopwithPointsInformationResponse.ShopwithPointsTraceNumber";
                }

                public ShopwithPointsInformationResponse()
                {
                }
            }

            public class ShopwithPointsResponse
            {
                public readonly static string RewardsBalance;

                public readonly static string ConvertedRewardsBalance;

                public readonly static string RewardsCurrency;

                public readonly static string ConversionRate;

                public readonly static string MinimumSpend;

                public readonly static string MaximumSpend;

                public readonly static string ProductCode;

                public readonly static string ContentID;

                static ShopwithPointsResponse()
                {
                    SLMResp.Online.ShopwithPointsResponse.RewardsBalance = "ShopwithPointsResponse.RewardsBalance";
                    SLMResp.Online.ShopwithPointsResponse.ConvertedRewardsBalance = "ShopwithPointsResponse.ConvertedRewardsBalance";
                    SLMResp.Online.ShopwithPointsResponse.RewardsCurrency = "ShopwithPointsResponse.RewardsCurrency";
                    SLMResp.Online.ShopwithPointsResponse.ConversionRate = "ShopwithPointsResponse.ConversionRate";
                    SLMResp.Online.ShopwithPointsResponse.MinimumSpend = "ShopwithPointsResponse.MinimumSpend";
                    SLMResp.Online.ShopwithPointsResponse.MaximumSpend = "ShopwithPointsResponse.MaximumSpend";
                    SLMResp.Online.ShopwithPointsResponse.ProductCode = "ShopwithPointsResponse.ProductCode";
                    SLMResp.Online.ShopwithPointsResponse.ContentID = "ShopwithPointsResponse.ContentID";
                }

                public ShopwithPointsResponse()
                {
                }
            }

            public class StagedDigitalWallet
            {
                public readonly static string DigitalWalletIndicator;

                public readonly static string StagedDigitalWalletIndicator;

                public readonly static string MasterPassDigitalWalletID;

                static StagedDigitalWallet()
                {
                    SLMResp.Online.StagedDigitalWallet.DigitalWalletIndicator = "StagedDigitalWallet.DigitalWalletIndicator";
                    SLMResp.Online.StagedDigitalWallet.StagedDigitalWalletIndicator = "StagedDigitalWallet.StagedDigitalWalletIndicator";
                    SLMResp.Online.StagedDigitalWallet.MasterPassDigitalWalletID = "StagedDigitalWallet.MasterPassDigitalWalletID";
                }

                public StagedDigitalWallet()
                {
                }
            }

            public class TokenIDResponse
            {
                public readonly static string TokenID;

                static TokenIDResponse()
                {
                    SLMResp.Online.TokenIDResponse.TokenID = "TokenIDResponse.TokenID";
                }

                public TokenIDResponse()
                {
                }
            }

            public class TransactionIDResponse
            {
                public readonly static string TokenID;

                public readonly static string TransactionID;

                static TransactionIDResponse()
                {
                    SLMResp.Online.TransactionIDResponse.TokenID = "TransactionIDResponse.TokenID";
                    SLMResp.Online.TransactionIDResponse.TransactionID = "TransactionIDResponse.TransactionID";
                }

                public TransactionIDResponse()
                {
                }
            }

            public class ValueLinkResponse
            {
                public readonly static string CurrentBalance;

                public readonly static string PreviousBalance;

                static ValueLinkResponse()
                {
                    SLMResp.Online.ValueLinkResponse.CurrentBalance = "ValueLinkResponse.CurrentBalance";
                    SLMResp.Online.ValueLinkResponse.PreviousBalance = "ValueLinkResponse.PreviousBalance";
                }

                public ValueLinkResponse()
                {
                }
            }

            public class Version
            {
                public readonly static string VersionInfo;

                static Version()
                {
                    SLMResp.Online.Version.VersionInfo = "Version.VersionInfo";
                }

                public Version()
                {
                }
            }

            public class VisaExtendedAuthDataResponse
            {
                public readonly static string AuthorizedAmount;

                public readonly static string MerchantCategoryCode;

                public readonly static string POSEntryMode;

                public readonly static string AuthResponseCode;

                public readonly static string AuthCharacteristicIndicator;

                public readonly static string TransactionIdentifier;

                public readonly static string ValidationCode;

                public readonly static string MarketSpecificDataIndicator;

                public readonly static string CardLevelResults;

                public readonly static string AVSResponseCode;

                static VisaExtendedAuthDataResponse()
                {
                    SLMResp.Online.VisaExtendedAuthDataResponse.AuthorizedAmount = "VisaExtendedAuthDataResponse.AuthorizedAmount";
                    SLMResp.Online.VisaExtendedAuthDataResponse.MerchantCategoryCode = "VisaExtendedAuthDataResponse.MerchantCategoryCode";
                    SLMResp.Online.VisaExtendedAuthDataResponse.POSEntryMode = "VisaExtendedAuthDataResponse.POSEntryMode";
                    SLMResp.Online.VisaExtendedAuthDataResponse.AuthResponseCode = "VisaExtendedAuthDataResponse.AuthResponseCode";
                    SLMResp.Online.VisaExtendedAuthDataResponse.AuthCharacteristicIndicator = "VisaExtendedAuthDataResponse.AuthCharacteristicIndicator";
                    SLMResp.Online.VisaExtendedAuthDataResponse.TransactionIdentifier = "VisaExtendedAuthDataResponse.TransactionIdentifier";
                    SLMResp.Online.VisaExtendedAuthDataResponse.ValidationCode = "VisaExtendedAuthDataResponse.ValidationCode";
                    SLMResp.Online.VisaExtendedAuthDataResponse.MarketSpecificDataIndicator = "VisaExtendedAuthDataResponse.MarketSpecificDataIndicator";
                    SLMResp.Online.VisaExtendedAuthDataResponse.CardLevelResults = "VisaExtendedAuthDataResponse.CardLevelResults";
                    SLMResp.Online.VisaExtendedAuthDataResponse.AVSResponseCode = "VisaExtendedAuthDataResponse.AVSResponseCode";
                }

                public VisaExtendedAuthDataResponse()
                {
                }
            }
        }
    }
}