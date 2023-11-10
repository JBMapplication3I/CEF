#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK
{
    public class SLM
    {
        public SLM()
        {
        }

        public class HeartBeat
        {
            public readonly static string SequenceNumber;

            public readonly static string CurrentGMTTime;

            static HeartBeat()
            {
                SLM.HeartBeat.SequenceNumber = "SequenceNumber";
                SLM.HeartBeat.CurrentGMTTime = "CurrentGMTTime";
            }

            public HeartBeat()
            {
            }
        }

        public class NewTransaction
        {
            public readonly static string MerchantOrderNumber;

            public readonly static string MethodOfPayment;

            public readonly static string AccountNumber;

            public readonly static string ExpirationDate;

            public readonly static string DivisionNumber;

            public readonly static string Amount;

            public readonly static string CurrencyCode;

            public readonly static string TransactionType;

            public readonly static string EncryptionFlag;

            public readonly static string PaymentIndicator;

            public readonly static string ActionCode;

            static NewTransaction()
            {
                SLM.NewTransaction.MerchantOrderNumber = "MerchantOrderNumber";
                SLM.NewTransaction.MethodOfPayment = "MethodOfPayment";
                SLM.NewTransaction.AccountNumber = "AccountNumber";
                SLM.NewTransaction.ExpirationDate = "ExpirationDate";
                SLM.NewTransaction.DivisionNumber = "DivisionNumber";
                SLM.NewTransaction.Amount = "Amount";
                SLM.NewTransaction.CurrencyCode = "CurrencyCode";
                SLM.NewTransaction.TransactionType = "TransactionType";
                SLM.NewTransaction.EncryptionFlag = "EncryptionFlag";
                SLM.NewTransaction.PaymentIndicator = "PaymentIndicator";
                SLM.NewTransaction.ActionCode = "ActionCode";
            }

            public NewTransaction()
            {
            }

            public class AdditionalAuthenticationData1
            {
                public readonly static string AuthenticationType;

                static AdditionalAuthenticationData1()
                {
                    SLM.NewTransaction.AdditionalAuthenticationData1.AuthenticationType = "AdditionalAuthenticationData1.AuthenticationType";
                }

                public AdditionalAuthenticationData1()
                {
                }
            }

            public class AgreementDescription
            {
                public readonly static string AgreementDescription_;

                static AgreementDescription()
                {
                    SLM.NewTransaction.AgreementDescription.AgreementDescription_ = "AgreementDescription.AgreementDescription_";
                }

                public AgreementDescription()
                {
                }
            }

            public class AmericanExpressAuthentication
            {
                public readonly static string AmericanExpressAuthenticationVerificationValue;

                public readonly static string TransactionID;

                static AmericanExpressAuthentication()
                {
                    SLM.NewTransaction.AmericanExpressAuthentication.AmericanExpressAuthenticationVerificationValue = "AmericanExpressAuthentication.AmericanExpressAuthenticationVerificationValue";
                    SLM.NewTransaction.AmericanExpressAuthentication.TransactionID = "AmericanExpressAuthentication.TransactionID";
                }

                public AmericanExpressAuthentication()
                {
                }
            }

            public class BillMeLater
            {
                public readonly static string ShippingCost;

                public readonly static string TCVersion;

                public readonly static string CustomerRegistrationDate;

                public readonly static string CustomerTypeFlag;

                public readonly static string ItemCategory;

                public readonly static string PreApprovalInvitationNumber;

                public readonly static string MerchantPromotionalCode;

                public readonly static string CustomerPasswordChange;

                public readonly static string CustomerBillingAddressChange;

                public readonly static string CustomerEmailChange;

                public readonly static string CustomerHomePhoneChange;

                static BillMeLater()
                {
                    SLM.NewTransaction.BillMeLater.ShippingCost = "BillMeLater.ShippingCost";
                    SLM.NewTransaction.BillMeLater.TCVersion = "BillMeLater.TCVersion";
                    SLM.NewTransaction.BillMeLater.CustomerRegistrationDate = "BillMeLater.CustomerRegistrationDate";
                    SLM.NewTransaction.BillMeLater.CustomerTypeFlag = "BillMeLater.CustomerTypeFlag";
                    SLM.NewTransaction.BillMeLater.ItemCategory = "BillMeLater.ItemCategory";
                    SLM.NewTransaction.BillMeLater.PreApprovalInvitationNumber = "BillMeLater.PreApprovalInvitationNumber";
                    SLM.NewTransaction.BillMeLater.MerchantPromotionalCode = "BillMeLater.MerchantPromotionalCode";
                    SLM.NewTransaction.BillMeLater.CustomerPasswordChange = "BillMeLater.CustomerPasswordChange";
                    SLM.NewTransaction.BillMeLater.CustomerBillingAddressChange = "BillMeLater.CustomerBillingAddressChange";
                    SLM.NewTransaction.BillMeLater.CustomerEmailChange = "BillMeLater.CustomerEmailChange";
                    SLM.NewTransaction.BillMeLater.CustomerHomePhoneChange = "BillMeLater.CustomerHomePhoneChange";
                }

                public BillMeLater()
                {
                }
            }

            public class BillMeLaterInformation
            {
                public readonly static string ShippingCost;

                public readonly static string TC;

                public readonly static string CustomerRegistrationDate;

                public readonly static string CustomerTypeFlag;

                public readonly static string ItemCategory;

                public readonly static string PreApprovalInviteNumber;

                public readonly static string MerchantPromoCode;

                public readonly static string CustomerPasswordChange;

                public readonly static string CustomerBillAddressChange;

                public readonly static string CustomerEmailChange;

                public readonly static string CustomerHomePhoneChange;

                public readonly static string ProductType;

                public readonly static string SecretQuestionCode;

                public readonly static string SecretAnswer;

                public readonly static string IATANumber;

                public readonly static string CustomerAuthByMerchant;

                public readonly static string BackOfficeProcessing;

                static BillMeLaterInformation()
                {
                    SLM.NewTransaction.BillMeLaterInformation.ShippingCost = "BillMeLaterInformation.ShippingCost";
                    SLM.NewTransaction.BillMeLaterInformation.TC = "BillMeLaterInformation.TC";
                    SLM.NewTransaction.BillMeLaterInformation.CustomerRegistrationDate = "BillMeLaterInformation.CustomerRegistrationDate";
                    SLM.NewTransaction.BillMeLaterInformation.CustomerTypeFlag = "BillMeLaterInformation.CustomerTypeFlag";
                    SLM.NewTransaction.BillMeLaterInformation.ItemCategory = "BillMeLaterInformation.ItemCategory";
                    SLM.NewTransaction.BillMeLaterInformation.PreApprovalInviteNumber = "BillMeLaterInformation.PreApprovalInviteNumber";
                    SLM.NewTransaction.BillMeLaterInformation.MerchantPromoCode = "BillMeLaterInformation.MerchantPromoCode";
                    SLM.NewTransaction.BillMeLaterInformation.CustomerPasswordChange = "BillMeLaterInformation.CustomerPasswordChange";
                    SLM.NewTransaction.BillMeLaterInformation.CustomerBillAddressChange = "BillMeLaterInformation.CustomerBillAddressChange";
                    SLM.NewTransaction.BillMeLaterInformation.CustomerEmailChange = "BillMeLaterInformation.CustomerEmailChange";
                    SLM.NewTransaction.BillMeLaterInformation.CustomerHomePhoneChange = "BillMeLaterInformation.CustomerHomePhoneChange";
                    SLM.NewTransaction.BillMeLaterInformation.ProductType = "BillMeLaterInformation.ProductType";
                    SLM.NewTransaction.BillMeLaterInformation.SecretQuestionCode = "BillMeLaterInformation.SecretQuestionCode";
                    SLM.NewTransaction.BillMeLaterInformation.SecretAnswer = "BillMeLaterInformation.SecretAnswer";
                    SLM.NewTransaction.BillMeLaterInformation.IATANumber = "BillMeLaterInformation.IATANumber";
                    SLM.NewTransaction.BillMeLaterInformation.CustomerAuthByMerchant = "BillMeLaterInformation.CustomerAuthByMerchant";
                    SLM.NewTransaction.BillMeLaterInformation.BackOfficeProcessing = "BillMeLaterInformation.BackOfficeProcessing";
                }

                public BillMeLaterInformation()
                {
                }
            }

            public class BillMeLaterPrivateLabel
            {
                public readonly static string ShippingCost;

                public readonly static string TCVersion;

                public readonly static string CustomerRegistrationDate;

                public readonly static string CustomerTypeFlag;

                public readonly static string ItemCategory;

                public readonly static string PreApprovalInvitationNumber;

                public readonly static string MerchantPromotionCode;

                public readonly static string ProductType;

                public readonly static string SecretQuestionCode;

                public readonly static string SecretAnswer;

                static BillMeLaterPrivateLabel()
                {
                    SLM.NewTransaction.BillMeLaterPrivateLabel.ShippingCost = "BillMeLaterPrivateLabel.ShippingCost";
                    SLM.NewTransaction.BillMeLaterPrivateLabel.TCVersion = "BillMeLaterPrivateLabel.TCVersion";
                    SLM.NewTransaction.BillMeLaterPrivateLabel.CustomerRegistrationDate = "BillMeLaterPrivateLabel.CustomerRegistrationDate";
                    SLM.NewTransaction.BillMeLaterPrivateLabel.CustomerTypeFlag = "BillMeLaterPrivateLabel.CustomerTypeFlag";
                    SLM.NewTransaction.BillMeLaterPrivateLabel.ItemCategory = "BillMeLaterPrivateLabel.ItemCategory";
                    SLM.NewTransaction.BillMeLaterPrivateLabel.PreApprovalInvitationNumber = "BillMeLaterPrivateLabel.PreApprovalInvitationNumber";
                    SLM.NewTransaction.BillMeLaterPrivateLabel.MerchantPromotionCode = "BillMeLaterPrivateLabel.MerchantPromotionCode";
                    SLM.NewTransaction.BillMeLaterPrivateLabel.ProductType = "BillMeLaterPrivateLabel.ProductType";
                    SLM.NewTransaction.BillMeLaterPrivateLabel.SecretQuestionCode = "BillMeLaterPrivateLabel.SecretQuestionCode";
                    SLM.NewTransaction.BillMeLaterPrivateLabel.SecretAnswer = "BillMeLaterPrivateLabel.SecretAnswer";
                }

                public BillMeLaterPrivateLabel()
                {
                }
            }

            public class BillToAddress
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

                static BillToAddress()
                {
                    SLM.NewTransaction.BillToAddress.TelephoneType = "BillToAddress.TelephoneType";
                    SLM.NewTransaction.BillToAddress.TelephoneNumber = "BillToAddress.TelephoneNumber";
                    SLM.NewTransaction.BillToAddress.NameText = "BillToAddress.NameText";
                    SLM.NewTransaction.BillToAddress.AddressLine1 = "BillToAddress.AddressLine1";
                    SLM.NewTransaction.BillToAddress.AddressLine2 = "BillToAddress.AddressLine2";
                    SLM.NewTransaction.BillToAddress.CountryCode = "BillToAddress.CountryCode";
                    SLM.NewTransaction.BillToAddress.City = "BillToAddress.City";
                    SLM.NewTransaction.BillToAddress.State = "BillToAddress.State";
                    SLM.NewTransaction.BillToAddress.PostalCode = "BillToAddress.PostalCode";
                }

                public BillToAddress()
                {
                }
            }

            public class Business
            {
                public readonly static string DBAName;

                public readonly static string LegalName;

                public readonly static string BusinessTelNumber;

                public readonly static string DBNumber;

                public readonly static string TaxIdNumber;

                public readonly static string CategoryCode;

                public readonly static string BusinessType;

                public readonly static string YearsInBusiness;

                public readonly static string NumberOfEmployees;

                public readonly static string PIN;

                public readonly static string UserID;

                static Business()
                {
                    SLM.NewTransaction.Business.DBAName = "Business.DBAName";
                    SLM.NewTransaction.Business.LegalName = "Business.LegalName";
                    SLM.NewTransaction.Business.BusinessTelNumber = "Business.BusinessTelNumber";
                    SLM.NewTransaction.Business.DBNumber = "Business.DBNumber";
                    SLM.NewTransaction.Business.TaxIdNumber = "Business.TaxIdNumber";
                    SLM.NewTransaction.Business.CategoryCode = "Business.CategoryCode";
                    SLM.NewTransaction.Business.BusinessType = "Business.BusinessType";
                    SLM.NewTransaction.Business.YearsInBusiness = "Business.YearsInBusiness";
                    SLM.NewTransaction.Business.NumberOfEmployees = "Business.NumberOfEmployees";
                    SLM.NewTransaction.Business.PIN = "Business.PIN";
                    SLM.NewTransaction.Business.UserID = "Business.UserID";
                }

                public Business()
                {
                }
            }

            public class BusinessAdministrator
            {
                public readonly static string Title;

                public readonly static string BusinessTelephoneNumber;

                public readonly static string BusinessFaxNumber;

                static BusinessAdministrator()
                {
                    SLM.NewTransaction.BusinessAdministrator.Title = "BusinessAdministrator.Title";
                    SLM.NewTransaction.BusinessAdministrator.BusinessTelephoneNumber = "BusinessAdministrator.BusinessTelephoneNumber";
                    SLM.NewTransaction.BusinessAdministrator.BusinessFaxNumber = "BusinessAdministrator.BusinessFaxNumber";
                }

                public BusinessAdministrator()
                {
                }
            }

            public class BusinessLoan
            {
                public readonly static string PurchaseOrderNumber;

                public readonly static string ApplicationID;

                public readonly static string LoanType;

                public readonly static string LeaseMasterAgreementID;

                public readonly static string LeasePurchaseOption;

                public readonly static string RequestedTerm;

                public readonly static string RequestedAmountOfPayment;

                public readonly static string RequestedFrequency;

                public readonly static string LeasePercentQualified;

                public readonly static string SuppliesPercentQualified;

                public readonly static string ServicesPercentQualified;

                public readonly static string OtherPercentQualified;

                static BusinessLoan()
                {
                    SLM.NewTransaction.BusinessLoan.PurchaseOrderNumber = "BusinessLoan.PurchaseOrderNumber";
                    SLM.NewTransaction.BusinessLoan.ApplicationID = "BusinessLoan.ApplicationID";
                    SLM.NewTransaction.BusinessLoan.LoanType = "BusinessLoan.LoanType";
                    SLM.NewTransaction.BusinessLoan.LeaseMasterAgreementID = "BusinessLoan.LeaseMasterAgreementID";
                    SLM.NewTransaction.BusinessLoan.LeasePurchaseOption = "BusinessLoan.LeasePurchaseOption";
                    SLM.NewTransaction.BusinessLoan.RequestedTerm = "BusinessLoan.RequestedTerm";
                    SLM.NewTransaction.BusinessLoan.RequestedAmountOfPayment = "BusinessLoan.RequestedAmountOfPayment";
                    SLM.NewTransaction.BusinessLoan.RequestedFrequency = "BusinessLoan.RequestedFrequency";
                    SLM.NewTransaction.BusinessLoan.LeasePercentQualified = "BusinessLoan.LeasePercentQualified";
                    SLM.NewTransaction.BusinessLoan.SuppliesPercentQualified = "BusinessLoan.SuppliesPercentQualified";
                    SLM.NewTransaction.BusinessLoan.ServicesPercentQualified = "BusinessLoan.ServicesPercentQualified";
                    SLM.NewTransaction.BusinessLoan.OtherPercentQualified = "BusinessLoan.OtherPercentQualified";
                }

                public BusinessLoan()
                {
                }
            }

            public class CardTypeIndicator
            {
                public readonly static string FormatVersion;

                static CardTypeIndicator()
                {
                    SLM.NewTransaction.CardTypeIndicator.FormatVersion = "CardTypeIndicator.FormatVersion";
                }

                public CardTypeIndicator()
                {
                }
            }

            public class CashBack
            {
                public readonly static string CashBackAmount;

                static CashBack()
                {
                    SLM.NewTransaction.CashBack.CashBackAmount = "CashBack.CashBackAmount";
                }

                public CashBack()
                {
                }
            }

            public class ChipEMVData
            {
                public readonly static string ChipData;

                static ChipEMVData()
                {
                    SLM.NewTransaction.ChipEMVData.ChipData = "ChipEMVData.ChipData";
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

                static CrossCurrency()
                {
                    SLM.NewTransaction.CrossCurrency.OptOutIndicator = "CrossCurrency.OptOutIndicator";
                    SLM.NewTransaction.CrossCurrency.RateHandlingIndicator = "CrossCurrency.RateHandlingIndicator";
                    SLM.NewTransaction.CrossCurrency.RateIdentifier = "CrossCurrency.RateIdentifier";
                    SLM.NewTransaction.CrossCurrency.ExchangeRate = "CrossCurrency.ExchangeRate";
                    SLM.NewTransaction.CrossCurrency.PresentmentCurrency = "CrossCurrency.PresentmentCurrency";
                    SLM.NewTransaction.CrossCurrency.SettlementCurrency = "CrossCurrency.SettlementCurrency";
                }

                public CrossCurrency()
                {
                }
            }

            public class CustomerANI
            {
                public readonly static string CustomerANI_;

                public readonly static string CustomerIIDigits;

                static CustomerANI()
                {
                    SLM.NewTransaction.CustomerANI.CustomerANI_ = "CustomerANI.CustomerANI_";
                    SLM.NewTransaction.CustomerANI.CustomerIIDigits = "CustomerANI.CustomerIIDigits";
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
                    SLM.NewTransaction.CustomerBrowserName.CustomerBrowserName_ = "CustomerBrowserName.CustomerBrowserName_";
                }

                public CustomerBrowserName()
                {
                }
            }

            public class CustomerEmailAddress
            {
                public readonly static string AddressSubtype;

                public readonly static string EmailAddress;

                static CustomerEmailAddress()
                {
                    SLM.NewTransaction.CustomerEmailAddress.AddressSubtype = "CustomerEmailAddress.AddressSubtype";
                    SLM.NewTransaction.CustomerEmailAddress.EmailAddress = "CustomerEmailAddress.EmailAddress";
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
                    SLM.NewTransaction.CustomerHostName.CustomerHostName_ = "CustomerHostName.CustomerHostName_";
                }

                public CustomerHostName()
                {
                }
            }

            public class DebitRouting
            {
                public readonly static string DebitRoutingData;

                static DebitRouting()
                {
                    SLM.NewTransaction.DebitRouting.DebitRoutingData = "DebitRouting.DebitRoutingData";
                }

                public DebitRouting()
                {
                }
            }

            public class DigitalPAN
            {
                public readonly static string TokenAssuranceLevel;

                public readonly static string AccountStatus;

                public readonly static string TokenRequestorID;

                static DigitalPAN()
                {
                    SLM.NewTransaction.DigitalPAN.TokenAssuranceLevel = "DigitalPAN.TokenAssuranceLevel";
                    SLM.NewTransaction.DigitalPAN.AccountStatus = "DigitalPAN.AccountStatus";
                    SLM.NewTransaction.DigitalPAN.TokenRequestorID = "DigitalPAN.TokenRequestorID";
                }

                public DigitalPAN()
                {
                }
            }

            public class DigitalWallet
            {
                public readonly static string DigitalWalletIndicator;

                public readonly static string StagedDigitalWalletIndicator;

                public readonly static string MasterPassDigitalWalletID;

                static DigitalWallet()
                {
                    SLM.NewTransaction.DigitalWallet.DigitalWalletIndicator = "DigitalWallet.DigitalWalletIndicator";
                    SLM.NewTransaction.DigitalWallet.StagedDigitalWalletIndicator = "DigitalWallet.StagedDigitalWalletIndicator";
                    SLM.NewTransaction.DigitalWallet.MasterPassDigitalWalletID = "DigitalWallet.MasterPassDigitalWalletID";
                }

                public DigitalWallet()
                {
                }
            }

            public class DirectPay
            {
                public readonly static string BusinessApplicationIdentifier;

                public readonly static string ServiceFee;

                public readonly static string ForeignExchangeMarkupFee;

                public readonly static string SenderReferenceNumber;

                public readonly static string SourceOfFunds;

                public readonly static string RecipientName;

                public readonly static string SenderName;

                public readonly static string SenderAddress;

                public readonly static string SenderCity;

                public readonly static string SenderState;

                public readonly static string SenderZipCode;

                public readonly static string SenderCountryCode;

                static DirectPay()
                {
                    SLM.NewTransaction.DirectPay.BusinessApplicationIdentifier = "DirectPay.BusinessApplicationIdentifier";
                    SLM.NewTransaction.DirectPay.ServiceFee = "DirectPay.ServiceFee";
                    SLM.NewTransaction.DirectPay.ForeignExchangeMarkupFee = "DirectPay.ForeignExchangeMarkupFee";
                    SLM.NewTransaction.DirectPay.SenderReferenceNumber = "DirectPay.SenderReferenceNumber";
                    SLM.NewTransaction.DirectPay.SourceOfFunds = "DirectPay.SourceOfFunds";
                    SLM.NewTransaction.DirectPay.RecipientName = "DirectPay.RecipientName";
                    SLM.NewTransaction.DirectPay.SenderName = "DirectPay.SenderName";
                    SLM.NewTransaction.DirectPay.SenderAddress = "DirectPay.SenderAddress";
                    SLM.NewTransaction.DirectPay.SenderCity = "DirectPay.SenderCity";
                    SLM.NewTransaction.DirectPay.SenderState = "DirectPay.SenderState";
                    SLM.NewTransaction.DirectPay.SenderZipCode = "DirectPay.SenderZipCode";
                    SLM.NewTransaction.DirectPay.SenderCountryCode = "DirectPay.SenderCountryCode";
                }

                public DirectPay()
                {
                }
            }

            public class DiscoverAuthentication
            {
                public readonly static string CAVV;

                static DiscoverAuthentication()
                {
                    SLM.NewTransaction.DiscoverAuthentication.CAVV = "DiscoverAuthentication.CAVV";
                }

                public DiscoverAuthentication()
                {
                }
            }

            public class ECPAdvancedVerification1
            {
                public readonly static string RecordType;

                public readonly static string FirstName;

                public readonly static string LastName;

                public readonly static string MiddleNameOrInitial;

                public readonly static string BusinessName;

                static ECPAdvancedVerification1()
                {
                    SLM.NewTransaction.ECPAdvancedVerification1.RecordType = "ECPAdvancedVerification1.RecordType";
                    SLM.NewTransaction.ECPAdvancedVerification1.FirstName = "ECPAdvancedVerification1.FirstName";
                    SLM.NewTransaction.ECPAdvancedVerification1.LastName = "ECPAdvancedVerification1.LastName";
                    SLM.NewTransaction.ECPAdvancedVerification1.MiddleNameOrInitial = "ECPAdvancedVerification1.MiddleNameOrInitial";
                    SLM.NewTransaction.ECPAdvancedVerification1.BusinessName = "ECPAdvancedVerification1.BusinessName";
                }

                public ECPAdvancedVerification1()
                {
                }
            }

            public class ECPAdvancedVerification2
            {
                public readonly static string RecordType;

                public readonly static string AddressLine1;

                public readonly static string AddressLine2;

                public readonly static string City;

                public readonly static string State;

                public readonly static string Zip;

                public readonly static string PhoneType;

                public readonly static string Phone;

                static ECPAdvancedVerification2()
                {
                    SLM.NewTransaction.ECPAdvancedVerification2.RecordType = "ECPAdvancedVerification2.RecordType";
                    SLM.NewTransaction.ECPAdvancedVerification2.AddressLine1 = "ECPAdvancedVerification2.AddressLine1";
                    SLM.NewTransaction.ECPAdvancedVerification2.AddressLine2 = "ECPAdvancedVerification2.AddressLine2";
                    SLM.NewTransaction.ECPAdvancedVerification2.City = "ECPAdvancedVerification2.City";
                    SLM.NewTransaction.ECPAdvancedVerification2.State = "ECPAdvancedVerification2.State";
                    SLM.NewTransaction.ECPAdvancedVerification2.Zip = "ECPAdvancedVerification2.Zip";
                    SLM.NewTransaction.ECPAdvancedVerification2.PhoneType = "ECPAdvancedVerification2.PhoneType";
                    SLM.NewTransaction.ECPAdvancedVerification2.Phone = "ECPAdvancedVerification2.Phone";
                }

                public ECPAdvancedVerification2()
                {
                }
            }

            public class ECPAdvancedVerification3
            {
                public readonly static string RecordType;

                public readonly static string CheckSerialNumber;

                static ECPAdvancedVerification3()
                {
                    SLM.NewTransaction.ECPAdvancedVerification3.RecordType = "ECPAdvancedVerification3.RecordType";
                    SLM.NewTransaction.ECPAdvancedVerification3.CheckSerialNumber = "ECPAdvancedVerification3.CheckSerialNumber";
                }

                public ECPAdvancedVerification3()
                {
                }
            }

            public class ECPAdvancedVerification4
            {
                public readonly static string RecordType;

                public readonly static string TaxIdentificationNumber;

                public readonly static string DOB;

                public readonly static string IDType;

                public readonly static string IDNumber;

                public readonly static string IDState;

                static ECPAdvancedVerification4()
                {
                    SLM.NewTransaction.ECPAdvancedVerification4.RecordType = "ECPAdvancedVerification4.RecordType";
                    SLM.NewTransaction.ECPAdvancedVerification4.TaxIdentificationNumber = "ECPAdvancedVerification4.TaxIdentificationNumber";
                    SLM.NewTransaction.ECPAdvancedVerification4.DOB = "ECPAdvancedVerification4.DOB";
                    SLM.NewTransaction.ECPAdvancedVerification4.IDType = "ECPAdvancedVerification4.IDType";
                    SLM.NewTransaction.ECPAdvancedVerification4.IDNumber = "ECPAdvancedVerification4.IDNumber";
                    SLM.NewTransaction.ECPAdvancedVerification4.IDState = "ECPAdvancedVerification4.IDState";
                }

                public ECPAdvancedVerification4()
                {
                }
            }

            public class ElectronicBenefitsTransfer
            {
                public readonly static string AccountType;

                public readonly static string VoucherNumber;

                public readonly static string FoodConsumerNumber;

                static ElectronicBenefitsTransfer()
                {
                    SLM.NewTransaction.ElectronicBenefitsTransfer.AccountType = "ElectronicBenefitsTransfer.AccountType";
                    SLM.NewTransaction.ElectronicBenefitsTransfer.VoucherNumber = "ElectronicBenefitsTransfer.VoucherNumber";
                    SLM.NewTransaction.ElectronicBenefitsTransfer.FoodConsumerNumber = "ElectronicBenefitsTransfer.FoodConsumerNumber";
                }

                public ElectronicBenefitsTransfer()
                {
                }
            }

            public class ElectronicCheck
            {
                public readonly static string RDFIBankID;

                public readonly static string AuthorizationMethod;

                static ElectronicCheck()
                {
                    SLM.NewTransaction.ElectronicCheck.RDFIBankID = "ElectronicCheck.RDFIBankID";
                    SLM.NewTransaction.ElectronicCheck.AuthorizationMethod = "ElectronicCheck.AuthorizationMethod";
                }

                public ElectronicCheck()
                {
                }
            }

            public class EmployerAddress
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

                static EmployerAddress()
                {
                    SLM.NewTransaction.EmployerAddress.TelephoneType = "EmployerAddress.TelephoneType";
                    SLM.NewTransaction.EmployerAddress.TelephoneNumber = "EmployerAddress.TelephoneNumber";
                    SLM.NewTransaction.EmployerAddress.NameText = "EmployerAddress.NameText";
                    SLM.NewTransaction.EmployerAddress.AddressLine1 = "EmployerAddress.AddressLine1";
                    SLM.NewTransaction.EmployerAddress.AddressLine2 = "EmployerAddress.AddressLine2";
                    SLM.NewTransaction.EmployerAddress.CountryCode = "EmployerAddress.CountryCode";
                    SLM.NewTransaction.EmployerAddress.City = "EmployerAddress.City";
                    SLM.NewTransaction.EmployerAddress.State = "EmployerAddress.State";
                    SLM.NewTransaction.EmployerAddress.PostalCode = "EmployerAddress.PostalCode";
                }

                public EmployerAddress()
                {
                }
            }

            public class EuropeanDirectDebit
            {
                public readonly static string CountryCode;

                public readonly static string BankSortCode;

                public readonly static string RIBCode;

                static EuropeanDirectDebit()
                {
                    SLM.NewTransaction.EuropeanDirectDebit.CountryCode = "EuropeanDirectDebit.CountryCode";
                    SLM.NewTransaction.EuropeanDirectDebit.BankSortCode = "EuropeanDirectDebit.BankSortCode";
                    SLM.NewTransaction.EuropeanDirectDebit.RIBCode = "EuropeanDirectDebit.RIBCode";
                }

                public EuropeanDirectDebit()
                {
                }
            }

            public class EuropeanDirectDebit2
            {
                public readonly static string CountryCode;

                public readonly static string BankSortCode;

                public readonly static string RIBCode;

                public readonly static string BankBranchCode;

                public readonly static string IBAN;

                public readonly static string BIC;

                static EuropeanDirectDebit2()
                {
                    SLM.NewTransaction.EuropeanDirectDebit2.CountryCode = "EuropeanDirectDebit2.CountryCode";
                    SLM.NewTransaction.EuropeanDirectDebit2.BankSortCode = "EuropeanDirectDebit2.BankSortCode";
                    SLM.NewTransaction.EuropeanDirectDebit2.RIBCode = "EuropeanDirectDebit2.RIBCode";
                    SLM.NewTransaction.EuropeanDirectDebit2.BankBranchCode = "EuropeanDirectDebit2.BankBranchCode";
                    SLM.NewTransaction.EuropeanDirectDebit2.IBAN = "EuropeanDirectDebit2.IBAN";
                    SLM.NewTransaction.EuropeanDirectDebit2.BIC = "EuropeanDirectDebit2.BIC";
                }

                public EuropeanDirectDebit2()
                {
                }
            }

            public class FormattedBillToName
            {
                public readonly static string FirstName;

                public readonly static string LastName;

                static FormattedBillToName()
                {
                    SLM.NewTransaction.FormattedBillToName.FirstName = "FormattedBillToName.FirstName";
                    SLM.NewTransaction.FormattedBillToName.LastName = "FormattedBillToName.LastName";
                }

                public FormattedBillToName()
                {
                }
            }

            public class FormattedShipToName
            {
                public readonly static string FirstName;

                public readonly static string LastName;

                static FormattedShipToName()
                {
                    SLM.NewTransaction.FormattedShipToName.FirstName = "FormattedShipToName.FirstName";
                    SLM.NewTransaction.FormattedShipToName.LastName = "FormattedShipToName.LastName";
                }

                public FormattedShipToName()
                {
                }
            }

            public class Fraud
            {
                public readonly static string CardSecurityPresence;

                public readonly static string CardSecurityValue;

                static Fraud()
                {
                    SLM.NewTransaction.Fraud.CardSecurityPresence = "Fraud.CardSecurityPresence";
                    SLM.NewTransaction.Fraud.CardSecurityValue = "Fraud.CardSecurityValue";
                }

                public Fraud()
                {
                }
            }

            public class FraudScoring1
            {
                public readonly static string ReturnRulesTriggered;

                public readonly static string SafetechMerchantID;

                public readonly static string KaptchaSessionID;

                public readonly static string WebSiteShortName;

                static FraudScoring1()
                {
                    SLM.NewTransaction.FraudScoring1.ReturnRulesTriggered = "FraudScoring1.ReturnRulesTriggered";
                    SLM.NewTransaction.FraudScoring1.SafetechMerchantID = "FraudScoring1.SafetechMerchantID";
                    SLM.NewTransaction.FraudScoring1.KaptchaSessionID = "FraudScoring1.KaptchaSessionID";
                    SLM.NewTransaction.FraudScoring1.WebSiteShortName = "FraudScoring1.WebSiteShortName";
                }

                public FraudScoring1()
                {
                }
            }

            public class FraudScoring2
            {
                public readonly static string ReturnRulesTriggered;

                public readonly static string SafetechMerchantID;

                public readonly static string KaptchaSessionID;

                public readonly static string WebSiteShortName;

                public readonly static string CashValueOfFencibleItems;

                public readonly static string CustomerDateOfBirth;

                public readonly static string CustomerGender;

                public readonly static string CustomerDriversLicenseNumber;

                public readonly static string CustomerID;

                public readonly static string CustomerIDCreationTime;

                static FraudScoring2()
                {
                    SLM.NewTransaction.FraudScoring2.ReturnRulesTriggered = "FraudScoring2.ReturnRulesTriggered";
                    SLM.NewTransaction.FraudScoring2.SafetechMerchantID = "FraudScoring2.SafetechMerchantID";
                    SLM.NewTransaction.FraudScoring2.KaptchaSessionID = "FraudScoring2.KaptchaSessionID";
                    SLM.NewTransaction.FraudScoring2.WebSiteShortName = "FraudScoring2.WebSiteShortName";
                    SLM.NewTransaction.FraudScoring2.CashValueOfFencibleItems = "FraudScoring2.CashValueOfFencibleItems";
                    SLM.NewTransaction.FraudScoring2.CustomerDateOfBirth = "FraudScoring2.CustomerDateOfBirth";
                    SLM.NewTransaction.FraudScoring2.CustomerGender = "FraudScoring2.CustomerGender";
                    SLM.NewTransaction.FraudScoring2.CustomerDriversLicenseNumber = "FraudScoring2.CustomerDriversLicenseNumber";
                    SLM.NewTransaction.FraudScoring2.CustomerID = "FraudScoring2.CustomerID";
                    SLM.NewTransaction.FraudScoring2.CustomerIDCreationTime = "FraudScoring2.CustomerIDCreationTime";
                }

                public FraudScoring2()
                {
                }
            }

            public class GiftCard
            {
                public readonly static string PartialRedemptionIndicatorFlag;

                public readonly static string NumberOfCardsForBlockActivation;

                static GiftCard()
                {
                    SLM.NewTransaction.GiftCard.PartialRedemptionIndicatorFlag = "GiftCard.PartialRedemptionIndicatorFlag";
                    SLM.NewTransaction.GiftCard.NumberOfCardsForBlockActivation = "GiftCard.NumberOfCardsForBlockActivation";
                }

                public GiftCard()
                {
                }
            }

            public class GifteeInformation
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

                static GifteeInformation()
                {
                    SLM.NewTransaction.GifteeInformation.TelephoneType = "GifteeInformation.TelephoneType";
                    SLM.NewTransaction.GifteeInformation.TelephoneNumber = "GifteeInformation.TelephoneNumber";
                    SLM.NewTransaction.GifteeInformation.NameText = "GifteeInformation.NameText";
                    SLM.NewTransaction.GifteeInformation.AddressLine1 = "GifteeInformation.AddressLine1";
                    SLM.NewTransaction.GifteeInformation.AddressLine2 = "GifteeInformation.AddressLine2";
                    SLM.NewTransaction.GifteeInformation.CountryCode = "GifteeInformation.CountryCode";
                    SLM.NewTransaction.GifteeInformation.City = "GifteeInformation.City";
                    SLM.NewTransaction.GifteeInformation.State = "GifteeInformation.State";
                    SLM.NewTransaction.GifteeInformation.PostalCode = "GifteeInformation.PostalCode";
                }

                public GifteeInformation()
                {
                }
            }

            public class GoodsSold
            {
                public readonly static string GoodsSold_;

                static GoodsSold()
                {
                    SLM.NewTransaction.GoodsSold.GoodsSold_ = "GoodsSold.GoodsSold_";
                }

                public GoodsSold()
                {
                }
            }

            public class HealthCareIIAS
            {
                public readonly static string QHPAmount;

                public readonly static string RXAmount;

                public readonly static string VisionAmount;

                public readonly static string ClinicOtherAmount;

                public readonly static string DentalAmount;

                public readonly static string IIASFlag;

                static HealthCareIIAS()
                {
                    SLM.NewTransaction.HealthCareIIAS.QHPAmount = "HealthCareIIAS.QHPAmount";
                    SLM.NewTransaction.HealthCareIIAS.RXAmount = "HealthCareIIAS.RXAmount";
                    SLM.NewTransaction.HealthCareIIAS.VisionAmount = "HealthCareIIAS.VisionAmount";
                    SLM.NewTransaction.HealthCareIIAS.ClinicOtherAmount = "HealthCareIIAS.ClinicOtherAmount";
                    SLM.NewTransaction.HealthCareIIAS.DentalAmount = "HealthCareIIAS.DentalAmount";
                    SLM.NewTransaction.HealthCareIIAS.IIASFlag = "HealthCareIIAS.IIASFlag";
                }

                public HealthCareIIAS()
                {
                }
            }

            public class IPAddress
            {
                public readonly static string AddressSubType;

                public readonly static string CustomerIPAddress;

                static IPAddress()
                {
                    SLM.NewTransaction.IPAddress.AddressSubType = "IPAddress.AddressSubType";
                    SLM.NewTransaction.IPAddress.CustomerIPAddress = "IPAddress.CustomerIPAddress";
                }

                public IPAddress()
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
                    SLM.NewTransaction.JCBAuthentication.RecordType = "JCBAuthentication.RecordType";
                    SLM.NewTransaction.JCBAuthentication.AEVV = "JCBAuthentication.AEVV";
                    SLM.NewTransaction.JCBAuthentication.TransactionID = "JCBAuthentication.TransactionID";
                }

                public JCBAuthentication()
                {
                }
            }

            public class Lodging
            {
                public readonly static string Duration;

                static Lodging()
                {
                    SLM.NewTransaction.Lodging.Duration = "Lodging.Duration";
                }

                public Lodging()
                {
                }
            }

            public class MasterCardAuthentication
            {
                public readonly static string AccountholderAuthenticationValue;

                static MasterCardAuthentication()
                {
                    SLM.NewTransaction.MasterCardAuthentication.AccountholderAuthenticationValue = "MasterCardAuthentication.AccountholderAuthenticationValue";
                }

                public MasterCardAuthentication()
                {
                }
            }

            public class MerchantDescriptor
            {
                public readonly static string SoftMerchantNameItemDescription;

                public readonly static string SoftMerchantCityCustomerServicePhoneNumber;

                static MerchantDescriptor()
                {
                    SLM.NewTransaction.MerchantDescriptor.SoftMerchantNameItemDescription = "MerchantDescriptor.SoftMerchantNameItemDescription";
                    SLM.NewTransaction.MerchantDescriptor.SoftMerchantCityCustomerServicePhoneNumber = "MerchantDescriptor.SoftMerchantCityCustomerServicePhoneNumber";
                }

                public MerchantDescriptor()
                {
                }
            }

            public class MerchantEcho
            {
                public readonly static string MerchantEcho_;

                static MerchantEcho()
                {
                    SLM.NewTransaction.MerchantEcho.MerchantEcho_ = "MerchantEcho.MerchantEcho_";
                }

                public MerchantEcho()
                {
                }
            }

            public class MessageType
            {
                public readonly static string MessageType_;

                public readonly static string StoredCredentialFlag;

                public readonly static string SubmittedTransactionID;

                public readonly static string ResponseTransactionID;

                public readonly static string OriginalTransactionAmount;

                static MessageType()
                {
                    SLM.NewTransaction.MessageType.MessageType_ = "MessageType.MessageType_";
                    SLM.NewTransaction.MessageType.StoredCredentialFlag = "MessageType.StoredCredentialFlag";
                    SLM.NewTransaction.MessageType.SubmittedTransactionID = "MessageType.SubmittedTransactionID";
                    SLM.NewTransaction.MessageType.ResponseTransactionID = "MessageType.ResponseTransactionID";
                    SLM.NewTransaction.MessageType.OriginalTransactionAmount = "MessageType.OriginalTransactionAmount";
                }

                public MessageType()
                {
                }
            }

            public class Miscellaneous1
            {
                public readonly static string DealerNumber;

                static Miscellaneous1()
                {
                    SLM.NewTransaction.Miscellaneous1.DealerNumber = "Miscellaneous1.DealerNumber";
                }

                public Miscellaneous1()
                {
                }
            }

            public class MobilePOSDeviceInformation
            {
                public readonly static string DeviceType;

                static MobilePOSDeviceInformation()
                {
                    SLM.NewTransaction.MobilePOSDeviceInformation.DeviceType = "MobilePOSDeviceInformation.DeviceType";
                }

                public MobilePOSDeviceInformation()
                {
                }
            }

            public class MoneyPak
            {
                public readonly static string OriginalTransactionID;

                public readonly static string ConfirmationID;

                public readonly static string MoneyPakAccountNumber;

                static MoneyPak()
                {
                    SLM.NewTransaction.MoneyPak.OriginalTransactionID = "MoneyPak.OriginalTransactionID";
                    SLM.NewTransaction.MoneyPak.ConfirmationID = "MoneyPak.ConfirmationID";
                    SLM.NewTransaction.MoneyPak.MoneyPakAccountNumber = "MoneyPak.MoneyPakAccountNumber";
                }

                public MoneyPak()
                {
                }
            }

            public class OrderInformation1
            {
                public readonly static string ProductDeliveryTypeIndicator;

                public readonly static string ShippingCarrier;

                public readonly static string ShippingMethod;

                public readonly static string OrderDate;

                public readonly static string OrderTime;

                static OrderInformation1()
                {
                    SLM.NewTransaction.OrderInformation1.ProductDeliveryTypeIndicator = "OrderInformation1.ProductDeliveryTypeIndicator";
                    SLM.NewTransaction.OrderInformation1.ShippingCarrier = "OrderInformation1.ShippingCarrier";
                    SLM.NewTransaction.OrderInformation1.ShippingMethod = "OrderInformation1.ShippingMethod";
                    SLM.NewTransaction.OrderInformation1.OrderDate = "OrderInformation1.OrderDate";
                    SLM.NewTransaction.OrderInformation1.OrderTime = "OrderInformation1.OrderTime";
                }

                public OrderInformation1()
                {
                }
            }

            public class OrderInformation2
            {
                public readonly static string BillerReferenceNumber;

                static OrderInformation2()
                {
                    SLM.NewTransaction.OrderInformation2.BillerReferenceNumber = "OrderInformation2.BillerReferenceNumber";
                }

                public OrderInformation2()
                {
                }
            }

            public class OrderInformation3
            {
                public readonly static string SKU;

                static OrderInformation3()
                {
                    SLM.NewTransaction.OrderInformation3.SKU = "OrderInformation3.SKU";
                }

                public OrderInformation3()
                {
                }
            }

            public class OrderInformation4
            {
                public readonly static string EmployeeID;

                static OrderInformation4()
                {
                    SLM.NewTransaction.OrderInformation4.EmployeeID = "OrderInformation4.EmployeeID";
                }

                public OrderInformation4()
                {
                }
            }

            public class OrderInformation5
            {
                public readonly static string TCIDNumber;

                public readonly static string TCVersionNumber;

                public readonly static string TCDate;

                public readonly static string TCTime;

                static OrderInformation5()
                {
                    SLM.NewTransaction.OrderInformation5.TCIDNumber = "OrderInformation5.TCIDNumber";
                    SLM.NewTransaction.OrderInformation5.TCVersionNumber = "OrderInformation5.TCVersionNumber";
                    SLM.NewTransaction.OrderInformation5.TCDate = "OrderInformation5.TCDate";
                    SLM.NewTransaction.OrderInformation5.TCTime = "OrderInformation5.TCTime";
                }

                public OrderInformation5()
                {
                }
            }

            public class OrderInformation6
            {
                public readonly static string ProductCode;

                public readonly static string RewardsTransactionAmount;

                public readonly static string RewardsCurrency;

                public readonly static string ConversionRate;

                public readonly static string ShopwithPointsTraceNumber;

                static OrderInformation6()
                {
                    SLM.NewTransaction.OrderInformation6.ProductCode = "OrderInformation6.ProductCode";
                    SLM.NewTransaction.OrderInformation6.RewardsTransactionAmount = "OrderInformation6.RewardsTransactionAmount";
                    SLM.NewTransaction.OrderInformation6.RewardsCurrency = "OrderInformation6.RewardsCurrency";
                    SLM.NewTransaction.OrderInformation6.ConversionRate = "OrderInformation6.ConversionRate";
                    SLM.NewTransaction.OrderInformation6.ShopwithPointsTraceNumber = "OrderInformation6.ShopwithPointsTraceNumber";
                }

                public OrderInformation6()
                {
                }
            }

            public class OrderInformation7
            {
                public readonly static string SurchargeAmount;

                static OrderInformation7()
                {
                    SLM.NewTransaction.OrderInformation7.SurchargeAmount = "OrderInformation7.SurchargeAmount";
                }

                public OrderInformation7()
                {
                }
            }

            public class PageSetUp
            {
                public readonly static string LocaleCode;

                public readonly static string PageStyle;

                public readonly static string BorderColorHeader;

                public readonly static string BackgroundColorHeader;

                public readonly static string BackgroundColorPayPage;

                public readonly static string HeaderImage;

                static PageSetUp()
                {
                    SLM.NewTransaction.PageSetUp.LocaleCode = "PageSetUp.LocaleCode";
                    SLM.NewTransaction.PageSetUp.PageStyle = "PageSetUp.PageStyle";
                    SLM.NewTransaction.PageSetUp.BorderColorHeader = "PageSetUp.BorderColorHeader";
                    SLM.NewTransaction.PageSetUp.BackgroundColorHeader = "PageSetUp.BackgroundColorHeader";
                    SLM.NewTransaction.PageSetUp.BackgroundColorPayPage = "PageSetUp.BackgroundColorPayPage";
                    SLM.NewTransaction.PageSetUp.HeaderImage = "PageSetUp.HeaderImage";
                }

                public PageSetUp()
                {
                }
            }

            public class PartialAuthorization
            {
                public readonly static string PartialRedemptionIndicatorFlag;

                static PartialAuthorization()
                {
                    SLM.NewTransaction.PartialAuthorization.PartialRedemptionIndicatorFlag = "PartialAuthorization.PartialRedemptionIndicatorFlag";
                }

                public PartialAuthorization()
                {
                }
            }

            public class Payment
            {
                public readonly static string PaymentType;

                static Payment()
                {
                    SLM.NewTransaction.Payment.PaymentType = "Payment.PaymentType";
                }

                public Payment()
                {
                }
            }

            public class PaymentAction
            {
                public readonly static string SubTypeFlag;

                static PaymentAction()
                {
                    SLM.NewTransaction.PaymentAction.SubTypeFlag = "PaymentAction.SubTypeFlag";
                }

                public PaymentAction()
                {
                }
            }

            public class PaymentDevice
            {
                public readonly static string RecordType;

                public readonly static string PaymentDevice_;

                static PaymentDevice()
                {
                    SLM.NewTransaction.PaymentDevice.RecordType = "PaymentDevice.RecordType";
                    SLM.NewTransaction.PaymentDevice.PaymentDevice_ = "PaymentDevice.PaymentDevice_";
                }

                public PaymentDevice()
                {
                }
            }

            public class PersonalGuarantorAddress
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

                static PersonalGuarantorAddress()
                {
                    SLM.NewTransaction.PersonalGuarantorAddress.TelephoneType = "PersonalGuarantorAddress.TelephoneType";
                    SLM.NewTransaction.PersonalGuarantorAddress.TelephoneNumber = "PersonalGuarantorAddress.TelephoneNumber";
                    SLM.NewTransaction.PersonalGuarantorAddress.NameText = "PersonalGuarantorAddress.NameText";
                    SLM.NewTransaction.PersonalGuarantorAddress.AddressLine1 = "PersonalGuarantorAddress.AddressLine1";
                    SLM.NewTransaction.PersonalGuarantorAddress.AddressLine2 = "PersonalGuarantorAddress.AddressLine2";
                    SLM.NewTransaction.PersonalGuarantorAddress.CountryCode = "PersonalGuarantorAddress.CountryCode";
                    SLM.NewTransaction.PersonalGuarantorAddress.City = "PersonalGuarantorAddress.City";
                    SLM.NewTransaction.PersonalGuarantorAddress.State = "PersonalGuarantorAddress.State";
                    SLM.NewTransaction.PersonalGuarantorAddress.PostalCode = "PersonalGuarantorAddress.PostalCode";
                }

                public PersonalGuarantorAddress()
                {
                }
            }

            public class PersonalInformation
            {
                public readonly static string CustomerDateOfBirth;

                public readonly static string CustomerSSN;

                public readonly static string CurrencyType;

                public readonly static string GrossIncome;

                public readonly static string CustomerResidenceStatus;

                public readonly static string CustomerYearsAtResidence;

                public readonly static string CustomerYearsAtEmployer;

                public readonly static string CustomerCheckingAccount;

                public readonly static string CustomerSavingAccount;

                static PersonalInformation()
                {
                    SLM.NewTransaction.PersonalInformation.CustomerDateOfBirth = "PersonalInformation.CustomerDateOfBirth";
                    SLM.NewTransaction.PersonalInformation.CustomerSSN = "PersonalInformation.CustomerSSN";
                    SLM.NewTransaction.PersonalInformation.CurrencyType = "PersonalInformation.CurrencyType";
                    SLM.NewTransaction.PersonalInformation.GrossIncome = "PersonalInformation.GrossIncome";
                    SLM.NewTransaction.PersonalInformation.CustomerResidenceStatus = "PersonalInformation.CustomerResidenceStatus";
                    SLM.NewTransaction.PersonalInformation.CustomerYearsAtResidence = "PersonalInformation.CustomerYearsAtResidence";
                    SLM.NewTransaction.PersonalInformation.CustomerYearsAtEmployer = "PersonalInformation.CustomerYearsAtEmployer";
                    SLM.NewTransaction.PersonalInformation.CustomerCheckingAccount = "PersonalInformation.CustomerCheckingAccount";
                    SLM.NewTransaction.PersonalInformation.CustomerSavingAccount = "PersonalInformation.CustomerSavingAccount";
                }

                public PersonalInformation()
                {
                }
            }

            public class PersonalInformation2
            {
                public readonly static string PayerID;

                static PersonalInformation2()
                {
                    SLM.NewTransaction.PersonalInformation2.PayerID = "PersonalInformation2.PayerID";
                }

                public PersonalInformation2()
                {
                }
            }

            public class PersonalInformation3
            {
                public readonly static string PositionTitle;

                static PersonalInformation3()
                {
                    SLM.NewTransaction.PersonalInformation3.PositionTitle = "PersonalInformation3.PositionTitle";
                }

                public PersonalInformation3()
                {
                }
            }

            public class PINBasedDebitOnly
            {
                public readonly static string EncryptedPinNumber;

                public readonly static string PinKeySequenceNumber;

                public readonly static string AccountType;

                public readonly static string CashBackAmount;

                static PINBasedDebitOnly()
                {
                    SLM.NewTransaction.PINBasedDebitOnly.EncryptedPinNumber = "PINBasedDebitOnly.EncryptedPinNumber";
                    SLM.NewTransaction.PINBasedDebitOnly.PinKeySequenceNumber = "PINBasedDebitOnly.PinKeySequenceNumber";
                    SLM.NewTransaction.PINBasedDebitOnly.AccountType = "PINBasedDebitOnly.AccountType";
                    SLM.NewTransaction.PINBasedDebitOnly.CashBackAmount = "PINBasedDebitOnly.CashBackAmount";
                }

                public PINBasedDebitOnly()
                {
                }
            }

            public class PostalCodeOnlyAddress
            {
                public readonly static string PostalCode;

                public readonly static string CountryCode;

                static PostalCodeOnlyAddress()
                {
                    SLM.NewTransaction.PostalCodeOnlyAddress.PostalCode = "PostalCodeOnlyAddress.PostalCode";
                    SLM.NewTransaction.PostalCodeOnlyAddress.CountryCode = "PostalCodeOnlyAddress.CountryCode";
                }

                public PostalCodeOnlyAddress()
                {
                }
            }

            public class PriorAuthorization
            {
                public readonly static string ResponseDate;

                public readonly static string AuthorizationCode;

                public readonly static string DebitTraceNumber;

                static PriorAuthorization()
                {
                    SLM.NewTransaction.PriorAuthorization.ResponseDate = "PriorAuthorization.ResponseDate";
                    SLM.NewTransaction.PriorAuthorization.AuthorizationCode = "PriorAuthorization.AuthorizationCode";
                    SLM.NewTransaction.PriorAuthorization.DebitTraceNumber = "PriorAuthorization.DebitTraceNumber";
                }

                public PriorAuthorization()
                {
                }
            }

            public class RealTimeAccountUpdater
            {
                public readonly static string BypassAccountUpdateRequest;

                static RealTimeAccountUpdater()
                {
                    SLM.NewTransaction.RealTimeAccountUpdater.BypassAccountUpdateRequest = "RealTimeAccountUpdater.BypassAccountUpdateRequest";
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
                    SLM.NewTransaction.RecipientData.DateofBirthofPrimaryRecipient = "RecipientData.DateofBirthofPrimaryRecipient";
                    SLM.NewTransaction.RecipientData.MaskedAccountNumber = "RecipientData.MaskedAccountNumber";
                    SLM.NewTransaction.RecipientData.PartialPostalCode = "RecipientData.PartialPostalCode";
                    SLM.NewTransaction.RecipientData.LastNameofPrimaryRecipient = "RecipientData.LastNameofPrimaryRecipient";
                }

                public RecipientData()
                {
                }
            }

            public class RequestInformation
            {
                public readonly static string EnhancedVerificationFlag;

                static RequestInformation()
                {
                    SLM.NewTransaction.RequestInformation.EnhancedVerificationFlag = "RequestInformation.EnhancedVerificationFlag";
                }

                public RequestInformation()
                {
                }
            }

            public class Retail
            {
                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string TrackIndicator;

                public readonly static string SwipeData;

                static Retail()
                {
                    SLM.NewTransaction.Retail.POSCapabilityCode = "Retail.POSCapabilityCode";
                    SLM.NewTransaction.Retail.POSEntryMode = "Retail.POSEntryMode";
                    SLM.NewTransaction.Retail.TrackIndicator = "Retail.TrackIndicator";
                    SLM.NewTransaction.Retail.SwipeData = "Retail.SwipeData";
                }

                public Retail()
                {
                }
            }

            public class Retail2
            {
                public readonly static string MerchantCategoryCode;

                public readonly static string CATType;

                public readonly static string TerminalID;

                static Retail2()
                {
                    SLM.NewTransaction.Retail2.MerchantCategoryCode = "Retail2.MerchantCategoryCode";
                    SLM.NewTransaction.Retail2.CATType = "Retail2.CATType";
                    SLM.NewTransaction.Retail2.TerminalID = "Retail2.TerminalID";
                }

                public Retail2()
                {
                }
            }

            public class Retail3
            {
                public readonly static string POSCapabilityCode;

                public readonly static string POSEntryMode;

                public readonly static string TrackIndicator;

                public readonly static string SwipeData;

                static Retail3()
                {
                    SLM.NewTransaction.Retail3.POSCapabilityCode = "Retail3.POSCapabilityCode";
                    SLM.NewTransaction.Retail3.POSEntryMode = "Retail3.POSEntryMode";
                    SLM.NewTransaction.Retail3.TrackIndicator = "Retail3.TrackIndicator";
                    SLM.NewTransaction.Retail3.SwipeData = "Retail3.SwipeData";
                }

                public Retail3()
                {
                }
            }

            public class Reversal
            {
                public readonly static string SystemAssignedReferenceNumber;

                static Reversal()
                {
                    SLM.NewTransaction.Reversal.SystemAssignedReferenceNumber = "Reversal.SystemAssignedReferenceNumber";
                }

                public Reversal()
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
                    SLM.NewTransaction.ReversalReason.ResponseDate = "ReversalReason.ResponseDate";
                    SLM.NewTransaction.ReversalReason.AuthorizationCode = "ReversalReason.AuthorizationCode";
                    SLM.NewTransaction.ReversalReason.DebitTraceNumber = "ReversalReason.DebitTraceNumber";
                    SLM.NewTransaction.ReversalReason.ReversalReasonCode = "ReversalReason.ReversalReasonCode";
                }

                public ReversalReason()
                {
                }
            }

            public class SafetechPageEncryption
            {
                public readonly static string SubscriberID;

                public readonly static string FormatID;

                public readonly static string IntegrityCheck;

                public readonly static string KeyID;

                public readonly static string PhaseID;

                static SafetechPageEncryption()
                {
                    SLM.NewTransaction.SafetechPageEncryption.SubscriberID = "SafetechPageEncryption.SubscriberID";
                    SLM.NewTransaction.SafetechPageEncryption.FormatID = "SafetechPageEncryption.FormatID";
                    SLM.NewTransaction.SafetechPageEncryption.IntegrityCheck = "SafetechPageEncryption.IntegrityCheck";
                    SLM.NewTransaction.SafetechPageEncryption.KeyID = "SafetechPageEncryption.KeyID";
                    SLM.NewTransaction.SafetechPageEncryption.PhaseID = "SafetechPageEncryption.PhaseID";
                }

                public SafetechPageEncryption()
                {
                }
            }

            public class ShipToAddress
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

                static ShipToAddress()
                {
                    SLM.NewTransaction.ShipToAddress.TelephoneType = "ShipToAddress.TelephoneType";
                    SLM.NewTransaction.ShipToAddress.TelephoneNumber = "ShipToAddress.TelephoneNumber";
                    SLM.NewTransaction.ShipToAddress.NameText = "ShipToAddress.NameText";
                    SLM.NewTransaction.ShipToAddress.AddressLine1 = "ShipToAddress.AddressLine1";
                    SLM.NewTransaction.ShipToAddress.AddressLine2 = "ShipToAddress.AddressLine2";
                    SLM.NewTransaction.ShipToAddress.CountryCode = "ShipToAddress.CountryCode";
                    SLM.NewTransaction.ShipToAddress.City = "ShipToAddress.City";
                    SLM.NewTransaction.ShipToAddress.State = "ShipToAddress.State";
                    SLM.NewTransaction.ShipToAddress.PostalCode = "ShipToAddress.PostalCode";
                }

                public ShipToAddress()
                {
                }
            }

            public class SoftMerchantInformation
            {
                public readonly static string DBA;

                public readonly static string Street;

                public readonly static string City;

                public readonly static string Region;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                public readonly static string MerchantID;

                public readonly static string MerchantCategoryCode;

                static SoftMerchantInformation()
                {
                    SLM.NewTransaction.SoftMerchantInformation.DBA = "SoftMerchantInformation.DBA";
                    SLM.NewTransaction.SoftMerchantInformation.Street = "SoftMerchantInformation.Street";
                    SLM.NewTransaction.SoftMerchantInformation.City = "SoftMerchantInformation.City";
                    SLM.NewTransaction.SoftMerchantInformation.Region = "SoftMerchantInformation.Region";
                    SLM.NewTransaction.SoftMerchantInformation.PostalCode = "SoftMerchantInformation.PostalCode";
                    SLM.NewTransaction.SoftMerchantInformation.CountryCode = "SoftMerchantInformation.CountryCode";
                    SLM.NewTransaction.SoftMerchantInformation.MerchantID = "SoftMerchantInformation.MerchantID";
                    SLM.NewTransaction.SoftMerchantInformation.MerchantCategoryCode = "SoftMerchantInformation.MerchantCategoryCode";
                }

                public SoftMerchantInformation()
                {
                }
            }

            public class SoftMerchantInformation2
            {
                public readonly static string DBA;

                public readonly static string Street;

                public readonly static string City;

                public readonly static string Region;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                public readonly static string MerchantSellerID;

                public readonly static string MerchantCategoryCode;

                static SoftMerchantInformation2()
                {
                    SLM.NewTransaction.SoftMerchantInformation2.DBA = "SoftMerchantInformation2.DBA";
                    SLM.NewTransaction.SoftMerchantInformation2.Street = "SoftMerchantInformation2.Street";
                    SLM.NewTransaction.SoftMerchantInformation2.City = "SoftMerchantInformation2.City";
                    SLM.NewTransaction.SoftMerchantInformation2.Region = "SoftMerchantInformation2.Region";
                    SLM.NewTransaction.SoftMerchantInformation2.PostalCode = "SoftMerchantInformation2.PostalCode";
                    SLM.NewTransaction.SoftMerchantInformation2.CountryCode = "SoftMerchantInformation2.CountryCode";
                    SLM.NewTransaction.SoftMerchantInformation2.MerchantSellerID = "SoftMerchantInformation2.MerchantSellerID";
                    SLM.NewTransaction.SoftMerchantInformation2.MerchantCategoryCode = "SoftMerchantInformation2.MerchantCategoryCode";
                }

                public SoftMerchantInformation2()
                {
                }
            }

            public class SoftMerchantInformation3
            {
                public readonly static string DBA;

                public readonly static string Street;

                public readonly static string City;

                public readonly static string Region;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                public readonly static string MerchantSellerID;

                public readonly static string MerchantCategoryCode;

                public readonly static string EmailAddress;

                public readonly static string TelephoneNumber;

                static SoftMerchantInformation3()
                {
                    SLM.NewTransaction.SoftMerchantInformation3.DBA = "SoftMerchantInformation3.DBA";
                    SLM.NewTransaction.SoftMerchantInformation3.Street = "SoftMerchantInformation3.Street";
                    SLM.NewTransaction.SoftMerchantInformation3.City = "SoftMerchantInformation3.City";
                    SLM.NewTransaction.SoftMerchantInformation3.Region = "SoftMerchantInformation3.Region";
                    SLM.NewTransaction.SoftMerchantInformation3.PostalCode = "SoftMerchantInformation3.PostalCode";
                    SLM.NewTransaction.SoftMerchantInformation3.CountryCode = "SoftMerchantInformation3.CountryCode";
                    SLM.NewTransaction.SoftMerchantInformation3.MerchantSellerID = "SoftMerchantInformation3.MerchantSellerID";
                    SLM.NewTransaction.SoftMerchantInformation3.MerchantCategoryCode = "SoftMerchantInformation3.MerchantCategoryCode";
                    SLM.NewTransaction.SoftMerchantInformation3.EmailAddress = "SoftMerchantInformation3.EmailAddress";
                    SLM.NewTransaction.SoftMerchantInformation3.TelephoneNumber = "SoftMerchantInformation3.TelephoneNumber";
                }

                public SoftMerchantInformation3()
                {
                }
            }

            public class SoftMerchantInformation4
            {
                public readonly static string DBA;

                public readonly static string Street;

                public readonly static string City;

                public readonly static string Region;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                public readonly static string MerchantSellerID;

                public readonly static string MerchantCategoryCode;

                public readonly static string EmailAddress;

                public readonly static string TelephoneNumber;

                static SoftMerchantInformation4()
                {
                    SLM.NewTransaction.SoftMerchantInformation4.DBA = "SoftMerchantInformation4.DBA";
                    SLM.NewTransaction.SoftMerchantInformation4.Street = "SoftMerchantInformation4.Street";
                    SLM.NewTransaction.SoftMerchantInformation4.City = "SoftMerchantInformation4.City";
                    SLM.NewTransaction.SoftMerchantInformation4.Region = "SoftMerchantInformation4.Region";
                    SLM.NewTransaction.SoftMerchantInformation4.PostalCode = "SoftMerchantInformation4.PostalCode";
                    SLM.NewTransaction.SoftMerchantInformation4.CountryCode = "SoftMerchantInformation4.CountryCode";
                    SLM.NewTransaction.SoftMerchantInformation4.MerchantSellerID = "SoftMerchantInformation4.MerchantSellerID";
                    SLM.NewTransaction.SoftMerchantInformation4.MerchantCategoryCode = "SoftMerchantInformation4.MerchantCategoryCode";
                    SLM.NewTransaction.SoftMerchantInformation4.EmailAddress = "SoftMerchantInformation4.EmailAddress";
                    SLM.NewTransaction.SoftMerchantInformation4.TelephoneNumber = "SoftMerchantInformation4.TelephoneNumber";
                }

                public SoftMerchantInformation4()
                {
                }
            }

            public class SplitTender
            {
                public readonly static string SplitTenderIndicator;

                static SplitTender()
                {
                    SLM.NewTransaction.SplitTender.SplitTenderIndicator = "SplitTender.SplitTenderIndicator";
                }

                public SplitTender()
                {
                }
            }

            public class TokenID
            {
                public readonly static string TokenID_;

                static TokenID()
                {
                    SLM.NewTransaction.TokenID.TokenID_ = "TokenID.TokenID_";
                }

                public TokenID()
                {
                }
            }

            public class UKDomesticMaestro
            {
                public readonly static string CardStartDate;

                public readonly static string CardIssueNumber;

                static UKDomesticMaestro()
                {
                    SLM.NewTransaction.UKDomesticMaestro.CardStartDate = "UKDomesticMaestro.CardStartDate";
                    SLM.NewTransaction.UKDomesticMaestro.CardIssueNumber = "UKDomesticMaestro.CardIssueNumber";
                }

                public UKDomesticMaestro()
                {
                }
            }

            public class UKDomesticMaestroAuthentication
            {
                public readonly static string AccountholderAuthenticationValue;

                static UKDomesticMaestroAuthentication()
                {
                    SLM.NewTransaction.UKDomesticMaestroAuthentication.AccountholderAuthenticationValue = "UKDomesticMaestroAuthentication.AccountholderAuthenticationValue";
                }

                public UKDomesticMaestroAuthentication()
                {
                }
            }

            public class URLAndAddressFlag
            {
                public readonly static string ReturnURL;

                public readonly static string CancelURL;

                public readonly static string RequestConfirmShipAddress;

                public readonly static string ShipAddressDisplay;

                static URLAndAddressFlag()
                {
                    SLM.NewTransaction.URLAndAddressFlag.ReturnURL = "URLAndAddressFlag.ReturnURL";
                    SLM.NewTransaction.URLAndAddressFlag.CancelURL = "URLAndAddressFlag.CancelURL";
                    SLM.NewTransaction.URLAndAddressFlag.RequestConfirmShipAddress = "URLAndAddressFlag.RequestConfirmShipAddress";
                    SLM.NewTransaction.URLAndAddressFlag.ShipAddressDisplay = "URLAndAddressFlag.ShipAddressDisplay";
                }

                public URLAndAddressFlag()
                {
                }
            }

            public class UserDefinedAndShoppingCart
            {
                public readonly static string DataString;

                static UserDefinedAndShoppingCart()
                {
                    SLM.NewTransaction.UserDefinedAndShoppingCart.DataString = "UserDefinedAndShoppingCart.DataString";
                }

                public UserDefinedAndShoppingCart()
                {
                }
            }

            public class VariousText
            {
                public readonly static string TextMessage;

                static VariousText()
                {
                    SLM.NewTransaction.VariousText.TextMessage = "VariousText.TextMessage";
                }

                public VariousText()
                {
                }
            }

            public class Version
            {
                public readonly static string VersionInfo;

                static Version()
                {
                    SLM.NewTransaction.Version.VersionInfo = "Version.VersionInfo";
                }

                public Version()
                {
                }
            }

            public class VisaAuthentication
            {
                public readonly static string TransactionID;

                public readonly static string CardHolderAuthenticationVerificationValue;

                static VisaAuthentication()
                {
                    SLM.NewTransaction.VisaAuthentication.TransactionID = "VisaAuthentication.TransactionID";
                    SLM.NewTransaction.VisaAuthentication.CardHolderAuthenticationVerificationValue = "VisaAuthentication.CardHolderAuthenticationVerificationValue";
                }

                public VisaAuthentication()
                {
                }
            }
        }

        public class SubmissionBatchTotal
        {
            public readonly static string BatchRecordCount;

            public readonly static string BatchOrderCount;

            public readonly static string BatchAmountTotal;

            public readonly static string BatchAmountSales;

            public readonly static string BatchAmountRefunds;

            static SubmissionBatchTotal()
            {
                SLM.SubmissionBatchTotal.BatchRecordCount = "BatchRecordCount";
                SLM.SubmissionBatchTotal.BatchOrderCount = "BatchOrderCount";
                SLM.SubmissionBatchTotal.BatchAmountTotal = "BatchAmountTotal";
                SLM.SubmissionBatchTotal.BatchAmountSales = "BatchAmountSales";
                SLM.SubmissionBatchTotal.BatchAmountRefunds = "BatchAmountRefunds";
            }

            public SubmissionBatchTotal()
            {
            }
        }

        public class SubmissionHeader
        {
            public readonly static string PID;

            public readonly static string PIDPassword;

            public readonly static string SID;

            public readonly static string SIDPassword;

            public readonly static string AuthorizationLogIndicator;

            public readonly static string MerchantSpace;

            static SubmissionHeader()
            {
                SLM.SubmissionHeader.PID = "PID";
                SLM.SubmissionHeader.PIDPassword = "PIDPassword";
                SLM.SubmissionHeader.SID = "SID";
                SLM.SubmissionHeader.SIDPassword = "SIDPassword";
                SLM.SubmissionHeader.AuthorizationLogIndicator = "AuthorizationLogIndicator";
                SLM.SubmissionHeader.MerchantSpace = "MerchantSpace";
            }

            public SubmissionHeader()
            {
            }
        }

        public class SubmissionOrder
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

            public readonly static string PaymentIndicator;

            public readonly static string EncryptionFlag;

            public readonly static string SplitTenderIndicator;

            public readonly static string ReversalReasonCode;

            public readonly static string MerchantSpace;

            static SubmissionOrder()
            {
                SLM.SubmissionOrder.DivisionNumber = "SubmissionOrder.DivisionNumber";
                SLM.SubmissionOrder.MerchantOrderNumber = "SubmissionOrder.MerchantOrderNumber";
                SLM.SubmissionOrder.ActionCode = "SubmissionOrder.ActionCode";
                SLM.SubmissionOrder.MethodOfPayment = "SubmissionOrder.MethodOfPayment";
                SLM.SubmissionOrder.AccountNumber = "SubmissionOrder.AccountNumber";
                SLM.SubmissionOrder.ExpirationDate = "SubmissionOrder.ExpirationDate";
                SLM.SubmissionOrder.Amount = "SubmissionOrder.Amount";
                SLM.SubmissionOrder.CurrencyCode = "SubmissionOrder.CurrencyCode";
                SLM.SubmissionOrder.ResponseReasonCode = "SubmissionOrder.ResponseReasonCode";
                SLM.SubmissionOrder.TransactionType = "SubmissionOrder.TransactionType";
                SLM.SubmissionOrder.CardSecurityValueResponse = "SubmissionOrder.CardSecurityValueResponse";
                SLM.SubmissionOrder.ResponseDate = "SubmissionOrder.ResponseDate";
                SLM.SubmissionOrder.AuthVerCode = "SubmissionOrder.AuthVerCode";
                SLM.SubmissionOrder.AVSAAVRespCode = "SubmissionOrder.AVSAAVRespCode";
                SLM.SubmissionOrder.PaymentIndicator = "SubmissionOrder.PaymentIndicator";
                SLM.SubmissionOrder.EncryptionFlag = "SubmissionOrder.EncryptionFlag";
                SLM.SubmissionOrder.SplitTenderIndicator = "SubmissionOrder.SplitTenderIndicator";
                SLM.SubmissionOrder.ReversalReasonCode = "SubmissionOrder.ReversalReasonCode";
                SLM.SubmissionOrder.MerchantSpace = "SubmissionOrder.MerchantSpace";
            }

            public SubmissionOrder()
            {
            }

            public class AddressRecordConsumerIPAddress
            {
                public readonly static string AddressSubType;

                public readonly static string CustomerIPAddress;

                static AddressRecordConsumerIPAddress()
                {
                    SLM.SubmissionOrder.AddressRecordConsumerIPAddress.AddressSubType = "AddressRecordConsumerIPAddress.AddressSubType";
                    SLM.SubmissionOrder.AddressRecordConsumerIPAddress.CustomerIPAddress = "AddressRecordConsumerIPAddress.CustomerIPAddress";
                }

                public AddressRecordConsumerIPAddress()
                {
                }
            }

            public class AddressRecordConsumerName
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static AddressRecordConsumerName()
                {
                    SLM.SubmissionOrder.AddressRecordConsumerName.AddressLine = "AddressRecordConsumerName.AddressLine";
                    SLM.SubmissionOrder.AddressRecordConsumerName.TelephoneType = "AddressRecordConsumerName.TelephoneType";
                    SLM.SubmissionOrder.AddressRecordConsumerName.TelephoneNumber = "AddressRecordConsumerName.TelephoneNumber";
                    SLM.SubmissionOrder.AddressRecordConsumerName.CountryCode = "AddressRecordConsumerName.CountryCode";
                }

                public AddressRecordConsumerName()
                {
                }
            }

            public class AddressRecordCustomerANI
            {
                public readonly static string CustomerANI;

                public readonly static string CustomerInfoID;

                static AddressRecordCustomerANI()
                {
                    SLM.SubmissionOrder.AddressRecordCustomerANI.CustomerANI = "AddressRecordCustomerANI.CustomerANI";
                    SLM.SubmissionOrder.AddressRecordCustomerANI.CustomerInfoID = "AddressRecordCustomerANI.CustomerInfoID";
                }

                public AddressRecordCustomerANI()
                {
                }
            }

            public class AddressRecordCustomerBrowserName
            {
                public readonly static string CustomerBrowserName;

                public readonly static string Default;

                static AddressRecordCustomerBrowserName()
                {
                    SLM.SubmissionOrder.AddressRecordCustomerBrowserName.CustomerBrowserName = "AddressRecordCustomerBrowserName.CustomerBrowserName";
                    SLM.SubmissionOrder.AddressRecordCustomerBrowserName.Default = "AddressRecordCustomerBrowserName.Default";
                }

                public AddressRecordCustomerBrowserName()
                {
                }
            }

            public class AddressRecordCustomerEmailAddress
            {
                public readonly static string AddressSubType;

                public readonly static string CustomerEmailAddress;

                static AddressRecordCustomerEmailAddress()
                {
                    SLM.SubmissionOrder.AddressRecordCustomerEmailAddress.AddressSubType = "AddressRecordCustomerEmailAddress.AddressSubType";
                    SLM.SubmissionOrder.AddressRecordCustomerEmailAddress.CustomerEmailAddress = "AddressRecordCustomerEmailAddress.CustomerEmailAddress";
                }

                public AddressRecordCustomerEmailAddress()
                {
                }
            }

            public class AddressRecordCustomerHostName
            {
                public readonly static string CustomerHostName;

                static AddressRecordCustomerHostName()
                {
                    SLM.SubmissionOrder.AddressRecordCustomerHostName.CustomerHostName = "AddressRecordCustomerHostName.CustomerHostName";
                }

                public AddressRecordCustomerHostName()
                {
                }
            }

            public class AddressRecordEmployerInfo
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static AddressRecordEmployerInfo()
                {
                    SLM.SubmissionOrder.AddressRecordEmployerInfo.AddressLine = "AddressRecordEmployerInfo.AddressLine";
                    SLM.SubmissionOrder.AddressRecordEmployerInfo.TelephoneType = "AddressRecordEmployerInfo.TelephoneType";
                    SLM.SubmissionOrder.AddressRecordEmployerInfo.TelephoneNumber = "AddressRecordEmployerInfo.TelephoneNumber";
                    SLM.SubmissionOrder.AddressRecordEmployerInfo.CountryCode = "AddressRecordEmployerInfo.CountryCode";
                }

                public AddressRecordEmployerInfo()
                {
                }
            }

            public class AddressRecordGiftRecipientInfo
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static AddressRecordGiftRecipientInfo()
                {
                    SLM.SubmissionOrder.AddressRecordGiftRecipientInfo.AddressLine = "AddressRecordGiftRecipientInfo.AddressLine";
                    SLM.SubmissionOrder.AddressRecordGiftRecipientInfo.TelephoneType = "AddressRecordGiftRecipientInfo.TelephoneType";
                    SLM.SubmissionOrder.AddressRecordGiftRecipientInfo.TelephoneNumber = "AddressRecordGiftRecipientInfo.TelephoneNumber";
                    SLM.SubmissionOrder.AddressRecordGiftRecipientInfo.CountryCode = "AddressRecordGiftRecipientInfo.CountryCode";
                }

                public AddressRecordGiftRecipientInfo()
                {
                }
            }

            public class AddressRecordPostalCode
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static AddressRecordPostalCode()
                {
                    SLM.SubmissionOrder.AddressRecordPostalCode.AddressLine = "AddressRecordPostalCode.AddressLine";
                    SLM.SubmissionOrder.AddressRecordPostalCode.TelephoneType = "AddressRecordPostalCode.TelephoneType";
                    SLM.SubmissionOrder.AddressRecordPostalCode.TelephoneNumber = "AddressRecordPostalCode.TelephoneNumber";
                    SLM.SubmissionOrder.AddressRecordPostalCode.CountryCode = "AddressRecordPostalCode.CountryCode";
                }

                public AddressRecordPostalCode()
                {
                }
            }

            public class AddressRecordShipToAddress
            {
                public readonly static string AddressLine;

                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                public readonly static string CountryCode;

                static AddressRecordShipToAddress()
                {
                    SLM.SubmissionOrder.AddressRecordShipToAddress.AddressLine = "AddressRecordShipToAddress.AddressLine";
                    SLM.SubmissionOrder.AddressRecordShipToAddress.TelephoneType = "AddressRecordShipToAddress.TelephoneType";
                    SLM.SubmissionOrder.AddressRecordShipToAddress.TelephoneNumber = "AddressRecordShipToAddress.TelephoneNumber";
                    SLM.SubmissionOrder.AddressRecordShipToAddress.CountryCode = "AddressRecordShipToAddress.CountryCode";
                }

                public AddressRecordShipToAddress()
                {
                }
            }

            public class ExtensionRecordAmericanExpressAuthentication
            {
                public readonly static string AmericanExpressAuthenticationVerificationValue;

                public readonly static string TransactionID;

                public readonly static string AuthenticationType;

                public readonly static string AEVVResponseCode;

                static ExtensionRecordAmericanExpressAuthentication()
                {
                    SLM.SubmissionOrder.ExtensionRecordAmericanExpressAuthentication.AmericanExpressAuthenticationVerificationValue = "ExtensionRecordAmericanExpressAuthentication.AmericanExpressAuthenticationVerificationValue";
                    SLM.SubmissionOrder.ExtensionRecordAmericanExpressAuthentication.TransactionID = "ExtensionRecordAmericanExpressAuthentication.TransactionID";
                    SLM.SubmissionOrder.ExtensionRecordAmericanExpressAuthentication.AuthenticationType = "ExtensionRecordAmericanExpressAuthentication.AuthenticationType";
                    SLM.SubmissionOrder.ExtensionRecordAmericanExpressAuthentication.AEVVResponseCode = "ExtensionRecordAmericanExpressAuthentication.AEVVResponseCode";
                }

                public ExtensionRecordAmericanExpressAuthentication()
                {
                }
            }

            public class ExtensionRecordAMEX001
            {
                public readonly static string TAA1;

                public readonly static string TAA2;

                static ExtensionRecordAMEX001()
                {
                    SLM.SubmissionOrder.ExtensionRecordAMEX001.TAA1 = "ExtensionRecordAMEX001.TAA1";
                    SLM.SubmissionOrder.ExtensionRecordAMEX001.TAA2 = "ExtensionRecordAMEX001.TAA2";
                }

                public ExtensionRecordAMEX001()
                {
                }
            }

            public class ExtensionRecordAMEX002
            {
                public readonly static string TAA3;

                public readonly static string TAA4;

                static ExtensionRecordAMEX002()
                {
                    SLM.SubmissionOrder.ExtensionRecordAMEX002.TAA3 = "ExtensionRecordAMEX002.TAA3";
                    SLM.SubmissionOrder.ExtensionRecordAMEX002.TAA4 = "ExtensionRecordAMEX002.TAA4";
                }

                public ExtensionRecordAMEX002()
                {
                }
            }

            public class ExtensionRecordAMEX003
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

                static ExtensionRecordAMEX003()
                {
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.TransactionID = "ExtensionRecordAMEX003.TransactionID";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.POSCapabilityCode = "ExtensionRecordAMEX003.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.CardHolderAuthenticationCap = "ExtensionRecordAMEX003.CardHolderAuthenticationCap";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.CardCaptureCapability = "ExtensionRecordAMEX003.CardCaptureCapability";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.OperatingEnvironment = "ExtensionRecordAMEX003.OperatingEnvironment";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.CardHolderPresent = "ExtensionRecordAMEX003.CardHolderPresent";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.CardPresent = "ExtensionRecordAMEX003.CardPresent";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.POSEntryMode = "ExtensionRecordAMEX003.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.POSCardIDMethod = "ExtensionRecordAMEX003.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.CardHolderAuthenticationEntity = "ExtensionRecordAMEX003.CardHolderAuthenticationEntity";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.CardDataOutputCapability = "ExtensionRecordAMEX003.CardDataOutputCapability";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.TerminalOutputCapability = "ExtensionRecordAMEX003.TerminalOutputCapability";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.PINCaptureCapability = "ExtensionRecordAMEX003.PINCaptureCapability";
                    SLM.SubmissionOrder.ExtensionRecordAMEX003.AuthorizedAmount = "ExtensionRecordAMEX003.AuthorizedAmount";
                }

                public ExtensionRecordAMEX003()
                {
                }
            }

            public class ExtensionRecordBillMeLater001
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

                static ExtensionRecordBillMeLater001()
                {
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.ShippingCost = "ExtensionRecordBillMeLater001.ShippingCost";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.TCVersion = "ExtensionRecordBillMeLater001.TCVersion";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.CustomerRegDate = "ExtensionRecordBillMeLater001.CustomerRegDate";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.CustomerTypeFlag = "ExtensionRecordBillMeLater001.CustomerTypeFlag";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.ItemCategory = "ExtensionRecordBillMeLater001.ItemCategory";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.PreApprovalInvitationNumber = "ExtensionRecordBillMeLater001.PreApprovalInvitationNumber";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.MerchantPromoCode = "ExtensionRecordBillMeLater001.MerchantPromoCode";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.CustomerPasswordChange = "ExtensionRecordBillMeLater001.CustomerPasswordChange";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.CustomerBillingAddressChange = "ExtensionRecordBillMeLater001.CustomerBillingAddressChange";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.CustomerEmailChange = "ExtensionRecordBillMeLater001.CustomerEmailChange";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLater001.CustomerHomePhoneChange = "ExtensionRecordBillMeLater001.CustomerHomePhoneChange";
                }

                public ExtensionRecordBillMeLater001()
                {
                }
            }

            public class ExtensionRecordBillMeLaterPrivateLabel001
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

                static ExtensionRecordBillMeLaterPrivateLabel001()
                {
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.ShippingCost = "ExtensionRecordBillMeLaterPrivateLabel001.ShippingCost";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.TCVersion = "ExtensionRecordBillMeLaterPrivateLabel001.TCVersion";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.CustomerRegDate = "ExtensionRecordBillMeLaterPrivateLabel001.CustomerRegDate";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.CustomerTypeFlag = "ExtensionRecordBillMeLaterPrivateLabel001.CustomerTypeFlag";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.ItemCategory = "ExtensionRecordBillMeLaterPrivateLabel001.ItemCategory";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.PreApprovalInvitationNumber = "ExtensionRecordBillMeLaterPrivateLabel001.PreApprovalInvitationNumber";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.MerchantPromoCode = "ExtensionRecordBillMeLaterPrivateLabel001.MerchantPromoCode";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.ProductType = "ExtensionRecordBillMeLaterPrivateLabel001.ProductType";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.SecretQuestionCode = "ExtensionRecordBillMeLaterPrivateLabel001.SecretQuestionCode";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.SecretAnswer = "ExtensionRecordBillMeLaterPrivateLabel001.SecretAnswer";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.ExpirationDate = "ExtensionRecordBillMeLaterPrivateLabel001.ExpirationDate";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.CreditLine = "ExtensionRecordBillMeLaterPrivateLabel001.CreditLine";
                    SLM.SubmissionOrder.ExtensionRecordBillMeLaterPrivateLabel001.PromoOffer = "ExtensionRecordBillMeLaterPrivateLabel001.PromoOffer";
                }

                public ExtensionRecordBillMeLaterPrivateLabel001()
                {
                }
            }

            public class ExtensionRecordChaseCardServices001
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

                static ExtensionRecordChaseCardServices001()
                {
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.MerchantDirectRewardID = "ExtensionRecordChaseCardServices001.MerchantDirectRewardID";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.MerchantDirectPromoID = "ExtensionRecordChaseCardServices001.MerchantDirectPromoID";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.StoreNumber = "ExtensionRecordChaseCardServices001.StoreNumber";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.TransactionDate = "ExtensionRecordChaseCardServices001.TransactionDate";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.TransactionTime = "ExtensionRecordChaseCardServices001.TransactionTime";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.TicketNumber = "ExtensionRecordChaseCardServices001.TicketNumber";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.TerminalID = "ExtensionRecordChaseCardServices001.TerminalID";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.SalesTaxAmount = "ExtensionRecordChaseCardServices001.SalesTaxAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.TotalTicketAmount = "ExtensionRecordChaseCardServices001.TotalTicketAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.AlternatePayType1 = "ExtensionRecordChaseCardServices001.AlternatePayType1";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.AlternatePayType2 = "ExtensionRecordChaseCardServices001.AlternatePayType2";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.AlternatePayType3 = "ExtensionRecordChaseCardServices001.AlternatePayType3";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.POSCapabilityCode = "ExtensionRecordChaseCardServices001.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.POSEntryMode = "ExtensionRecordChaseCardServices001.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.POSAuthorizationSource = "ExtensionRecordChaseCardServices001.POSAuthorizationSource";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.POSCardIDMethod = "ExtensionRecordChaseCardServices001.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.POSConditionCode = "ExtensionRecordChaseCardServices001.POSConditionCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.AuthorizedAmount = "ExtensionRecordChaseCardServices001.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.MerchantCategoryCode = "ExtensionRecordChaseCardServices001.MerchantCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.MerchantPromoAmount = "ExtensionRecordChaseCardServices001.MerchantPromoAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices001.MerchantPromoCredDebInd = "ExtensionRecordChaseCardServices001.MerchantPromoCredDebInd";
                }

                public ExtensionRecordChaseCardServices001()
                {
                }
            }

            public class ExtensionRecordChaseCardServices002
            {
                public readonly static string ExcludedAmount;

                static ExtensionRecordChaseCardServices002()
                {
                    SLM.SubmissionOrder.ExtensionRecordChaseCardServices002.ExcludedAmount = "ExtensionRecordChaseCardServices002.ExcludedAmount";
                }

                public ExtensionRecordChaseCardServices002()
                {
                }
            }

            public class ExtensionRecordChaseNetAuthCredit
            {
                public readonly static string TransactionID;

                public readonly static string CAVV;

                public readonly static string CAVVResponseCode;

                static ExtensionRecordChaseNetAuthCredit()
                {
                    SLM.SubmissionOrder.ExtensionRecordChaseNetAuthCredit.TransactionID = "ExtensionRecordChaseNetAuthCredit.TransactionID";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetAuthCredit.CAVV = "ExtensionRecordChaseNetAuthCredit.CAVV";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetAuthCredit.CAVVResponseCode = "ExtensionRecordChaseNetAuthCredit.CAVVResponseCode";
                }

                public ExtensionRecordChaseNetAuthCredit()
                {
                }
            }

            public class ExtensionRecordChaseNetAuthSignaturePrepaidDebit
            {
                public readonly static string TransactionID;

                public readonly static string CAVV;

                public readonly static string CAVVResponseCode;

                static ExtensionRecordChaseNetAuthSignaturePrepaidDebit()
                {
                    SLM.SubmissionOrder.ExtensionRecordChaseNetAuthSignaturePrepaidDebit.TransactionID = "ExtensionRecordChaseNetAuthSignaturePrepaidDebit.TransactionID";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetAuthSignaturePrepaidDebit.CAVV = "ExtensionRecordChaseNetAuthSignaturePrepaidDebit.CAVV";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetAuthSignaturePrepaidDebit.CAVVResponseCode = "ExtensionRecordChaseNetAuthSignaturePrepaidDebit.CAVVResponseCode";
                }

                public ExtensionRecordChaseNetAuthSignaturePrepaidDebit()
                {
                }
            }

            public class ExtensionRecordChaseNetCredit
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

                static ExtensionRecordChaseNetCredit()
                {
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.POSCapabilityCode = "ExtensionRecordChaseNetCredit.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.POSEntryMode = "ExtensionRecordChaseNetCredit.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.POSAuthSource = "ExtensionRecordChaseNetCredit.POSAuthSource";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.POSCardIDMethod = "ExtensionRecordChaseNetCredit.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.AuthCharacteristicIndicator = "ExtensionRecordChaseNetCredit.AuthCharacteristicIndicator";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.TransactionID = "ExtensionRecordChaseNetCredit.TransactionID";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.ValidationCode = "ExtensionRecordChaseNetCredit.ValidationCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.AuthorizedAmount = "ExtensionRecordChaseNetCredit.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.MerchCategoryCode = "ExtensionRecordChaseNetCredit.MerchCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.TotalAuthAmount = "ExtensionRecordChaseNetCredit.TotalAuthAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.MarketSpecificDataIndicator = "ExtensionRecordChaseNetCredit.MarketSpecificDataIndicator";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.CATType = "ExtensionRecordChaseNetCredit.CATType";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.CardLevelResult = "ExtensionRecordChaseNetCredit.CardLevelResult";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.PartialAuthIndicator = "ExtensionRecordChaseNetCredit.PartialAuthIndicator";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.AuthorizationResponseCode = "ExtensionRecordChaseNetCredit.AuthorizationResponseCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCredit.SpendQualifiedIndicator = "ExtensionRecordChaseNetCredit.SpendQualifiedIndicator";
                }

                public ExtensionRecordChaseNetCredit()
                {
                }
            }

            public class ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary
            {
                public readonly static string ClearingSequenceNumber;

                public readonly static string ClearingCount;

                public readonly static string TotalClearingAmount;

                public readonly static string ComputerizedReservationSystem;

                static ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary()
                {
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary.ClearingSequenceNumber = "ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary.ClearingSequenceNumber";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary.ClearingCount = "ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary.ClearingCount";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary.TotalClearingAmount = "ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary.TotalClearingAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary.ComputerizedReservationSystem = "ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary.ComputerizedReservationSystem";
                }

                public ExtensionRecordChaseNetCreditPassengerTransportSupplementalItinerary()
                {
                }
            }

            public class ExtensionRecordChaseNetSignaturePrepaidDebit
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

                static ExtensionRecordChaseNetSignaturePrepaidDebit()
                {
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.POSCapabilityCode = "ExtensionRecordChaseNetSignaturePrepaidDebit.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.POSEntryMode = "ExtensionRecordChaseNetSignaturePrepaidDebit.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.POSAuthSource = "ExtensionRecordChaseNetSignaturePrepaidDebit.POSAuthSource";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.POSCardIDMethod = "ExtensionRecordChaseNetSignaturePrepaidDebit.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.AuthCharacteristicIndicator = "ExtensionRecordChaseNetSignaturePrepaidDebit.AuthCharacteristicIndicator";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.TransactionID = "ExtensionRecordChaseNetSignaturePrepaidDebit.TransactionID";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.ValidationCode = "ExtensionRecordChaseNetSignaturePrepaidDebit.ValidationCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.AuthorizedAmount = "ExtensionRecordChaseNetSignaturePrepaidDebit.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.MerchCategoryCode = "ExtensionRecordChaseNetSignaturePrepaidDebit.MerchCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.TotalAuthAmount = "ExtensionRecordChaseNetSignaturePrepaidDebit.TotalAuthAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.MarketSpecificDataIndicator = "ExtensionRecordChaseNetSignaturePrepaidDebit.MarketSpecificDataIndicator";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.CATType = "ExtensionRecordChaseNetSignaturePrepaidDebit.CATType";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.CardLevelResult = "ExtensionRecordChaseNetSignaturePrepaidDebit.CardLevelResult";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.PartialAuthIndicator = "ExtensionRecordChaseNetSignaturePrepaidDebit.PartialAuthIndicator";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.AuthorizationResponseCode = "ExtensionRecordChaseNetSignaturePrepaidDebit.AuthorizationResponseCode";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebit.SpendQualifiedIndicator = "ExtensionRecordChaseNetSignaturePrepaidDebit.SpendQualifiedIndicator";
                }

                public ExtensionRecordChaseNetSignaturePrepaidDebit()
                {
                }
            }

            public class ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary
            {
                public readonly static string ClearingSequenceNumber;

                public readonly static string ClearingCount;

                public readonly static string TotalClearingAmount;

                public readonly static string ComputerizedReservationSystem;

                static ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary()
                {
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary.ClearingSequenceNumber = "ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary.ClearingSequenceNumber";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary.ClearingCount = "ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary.ClearingCount";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary.TotalClearingAmount = "ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary.TotalClearingAmount";
                    SLM.SubmissionOrder.ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary.ComputerizedReservationSystem = "ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary.ComputerizedReservationSystem";
                }

                public ExtensionRecordChaseNetSignaturePrepaidDebitPassengerTransportSupplementalItinerary()
                {
                }
            }

            public class ExtensionRecordDiscover001
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

                static ExtensionRecordDiscover001()
                {
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.SystemTraceAuditNumber = "ExtensionRecordDiscover001.SystemTraceAuditNumber";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.ProcessingCodeType = "ExtensionRecordDiscover001.ProcessingCodeType";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.FromAccount = "ExtensionRecordDiscover001.FromAccount";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.ToAccount = "ExtensionRecordDiscover001.ToAccount";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.TransactionDataConditionCode = "ExtensionRecordDiscover001.TransactionDataConditionCode";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.PANEntryMode = "ExtensionRecordDiscover001.PANEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.PINEntryMode = "ExtensionRecordDiscover001.PINEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.POSDeviceAttendanceIndicator = "ExtensionRecordDiscover001.POSDeviceAttendanceIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.PartialApprovalIndicator = "ExtensionRecordDiscover001.PartialApprovalIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.POSDeviceLocationIndicator = "ExtensionRecordDiscover001.POSDeviceLocationIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.POSCardholderPresenceIndicator = "ExtensionRecordDiscover001.POSCardholderPresenceIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.POSCardPresenceIndicator = "ExtensionRecordDiscover001.POSCardPresenceIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.POSCardCaptureCapabilityIndicator = "ExtensionRecordDiscover001.POSCardCaptureCapabilityIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.POSTransactionStatusIndicator = "ExtensionRecordDiscover001.POSTransactionStatusIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.POSTransactionSecurityIndicator = "ExtensionRecordDiscover001.POSTransactionSecurityIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.POSTypeOfTerminalDevice = "ExtensionRecordDiscover001.POSTypeOfTerminalDevice";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.POSDeviceCardDataInputCapabilityIndicator = "ExtensionRecordDiscover001.POSDeviceCardDataInputCapabilityIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.CashOverAmount = "ExtensionRecordDiscover001.CashOverAmount";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.LocalTransactionDate = "ExtensionRecordDiscover001.LocalTransactionDate";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.LocalTransactionTime = "ExtensionRecordDiscover001.LocalTransactionTime";
                    SLM.SubmissionOrder.ExtensionRecordDiscover001.NetworkReferenceID = "ExtensionRecordDiscover001.NetworkReferenceID";
                }

                public ExtensionRecordDiscover001()
                {
                }
            }

            public class ExtensionRecordDiscoverAuthentication
            {
                public readonly static string CAVV;

                public readonly static string CAVVResponseCode;

                public readonly static string AuthenticationType;

                static ExtensionRecordDiscoverAuthentication()
                {
                    SLM.SubmissionOrder.ExtensionRecordDiscoverAuthentication.CAVV = "ExtensionRecordDiscoverAuthentication.CAVV";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverAuthentication.CAVVResponseCode = "ExtensionRecordDiscoverAuthentication.CAVVResponseCode";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverAuthentication.AuthenticationType = "ExtensionRecordDiscoverAuthentication.AuthenticationType";
                }

                public ExtensionRecordDiscoverAuthentication()
                {
                }
            }

            public class ExtensionRecordDiscoverDiners001
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

                static ExtensionRecordDiscoverDiners001()
                {
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.SystemTraceAuditNumber = "ExtensionRecordDiscoverDiners001.SystemTraceAuditNumber";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.ProcessingCodeType = "ExtensionRecordDiscoverDiners001.ProcessingCodeType";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.FromAccount = "ExtensionRecordDiscoverDiners001.FromAccount";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.ToAccount = "ExtensionRecordDiscoverDiners001.ToAccount";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.Track1DataConditionCode = "ExtensionRecordDiscoverDiners001.Track1DataConditionCode";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.Track2DataConditionCode = "ExtensionRecordDiscoverDiners001.Track2DataConditionCode";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.PANEntryMode = "ExtensionRecordDiscoverDiners001.PANEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.PINEntryMode = "ExtensionRecordDiscoverDiners001.PINEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.POSDeviceAttendanceIndicator = "ExtensionRecordDiscoverDiners001.POSDeviceAttendanceIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.PartialApprovalIndicator = "ExtensionRecordDiscoverDiners001.PartialApprovalIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.POSDeviceLocationIndicator = "ExtensionRecordDiscoverDiners001.POSDeviceLocationIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.POSCardholderPresenceIndicator = "ExtensionRecordDiscoverDiners001.POSCardholderPresenceIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.POSCardPresenceIndicator = "ExtensionRecordDiscoverDiners001.POSCardPresenceIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.POSCardCaptureCapabilityIndicator = "ExtensionRecordDiscoverDiners001.POSCardCaptureCapabilityIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.POSTransactionStatusIndicator = "ExtensionRecordDiscoverDiners001.POSTransactionStatusIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.POSTransactionSecurityIndicator = "ExtensionRecordDiscoverDiners001.POSTransactionSecurityIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.POSTypeOfTerminalDevice = "ExtensionRecordDiscoverDiners001.POSTypeOfTerminalDevice";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.POSDeviceCardDataInputCapabilityIndicator = "ExtensionRecordDiscoverDiners001.POSDeviceCardDataInputCapabilityIndicator";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.CashOverAmount = "ExtensionRecordDiscoverDiners001.CashOverAmount";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.LocalTransactionDate = "ExtensionRecordDiscoverDiners001.LocalTransactionDate";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.LocalTransactionTime = "ExtensionRecordDiscoverDiners001.LocalTransactionTime";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners001.NetworkReferenceID = "ExtensionRecordDiscoverDiners001.NetworkReferenceID";
                }

                public ExtensionRecordDiscoverDiners001()
                {
                }
            }

            public class ExtensionRecordDiscoverDiners002
            {
                public readonly static string CAVV;

                public readonly static string Reserved1;

                public readonly static string CAVVResponseCode;

                public readonly static string AuthenticationType;

                public readonly static string Reserved2;

                static ExtensionRecordDiscoverDiners002()
                {
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners002.CAVV = "ExtensionRecordDiscoverDiners002.CAVV";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners002.Reserved1 = "ExtensionRecordDiscoverDiners002.Reserved1";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners002.CAVVResponseCode = "ExtensionRecordDiscoverDiners002.CAVVResponseCode";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners002.AuthenticationType = "ExtensionRecordDiscoverDiners002.AuthenticationType";
                    SLM.SubmissionOrder.ExtensionRecordDiscoverDiners002.Reserved2 = "ExtensionRecordDiscoverDiners002.Reserved2";
                }

                public ExtensionRecordDiscoverDiners002()
                {
                }
            }

            public class ExtensionRecordECP001
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

                static ExtensionRecordECP001()
                {
                    SLM.SubmissionOrder.ExtensionRecordECP001.RDFIBankID = "ExtensionRecordECP001.RDFIBankID";
                    SLM.SubmissionOrder.ExtensionRecordECP001.AccountType = "ExtensionRecordECP001.AccountType";
                    SLM.SubmissionOrder.ExtensionRecordECP001.PrefDeliveryMethod = "ExtensionRecordECP001.PrefDeliveryMethod";
                    SLM.SubmissionOrder.ExtensionRecordECP001.ECPAuthMethod = "ExtensionRecordECP001.ECPAuthMethod";
                    SLM.SubmissionOrder.ExtensionRecordECP001.CheckSerialNumber = "ExtensionRecordECP001.CheckSerialNumber";
                    SLM.SubmissionOrder.ExtensionRecordECP001.TerminalCity = "ExtensionRecordECP001.TerminalCity";
                    SLM.SubmissionOrder.ExtensionRecordECP001.TerminalState = "ExtensionRecordECP001.TerminalState";
                    SLM.SubmissionOrder.ExtensionRecordECP001.ImageRefNumber = "ExtensionRecordECP001.ImageRefNumber";
                    SLM.SubmissionOrder.ExtensionRecordECP001.ReDepositFrequency = "ExtensionRecordECP001.ReDepositFrequency";
                    SLM.SubmissionOrder.ExtensionRecordECP001.ReDepositOption = "ExtensionRecordECP001.ReDepositOption";
                    SLM.SubmissionOrder.ExtensionRecordECP001.ECPSameDayIndicator = "ExtensionRecordECP001.ECPSameDayIndicator";
                    SLM.SubmissionOrder.ExtensionRecordECP001.ECPSameDayACHResponseCode = "ExtensionRecordECP001.ECPSameDayACHResponseCode";
                }

                public ExtensionRecordECP001()
                {
                }
            }

            public class ExtensionRecordEUDD001
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

                static ExtensionRecordEUDD001()
                {
                    SLM.SubmissionOrder.ExtensionRecordEUDD001.CountryCode = "ExtensionRecordEUDD001.CountryCode";
                    SLM.SubmissionOrder.ExtensionRecordEUDD001.BankSortCode = "ExtensionRecordEUDD001.BankSortCode";
                    SLM.SubmissionOrder.ExtensionRecordEUDD001.RIBCode = "ExtensionRecordEUDD001.RIBCode";
                    SLM.SubmissionOrder.ExtensionRecordEUDD001.BankBranchCode = "ExtensionRecordEUDD001.BankBranchCode";
                    SLM.SubmissionOrder.ExtensionRecordEUDD001.IBAN = "ExtensionRecordEUDD001.IBAN";
                    SLM.SubmissionOrder.ExtensionRecordEUDD001.BIC = "ExtensionRecordEUDD001.BIC";
                    SLM.SubmissionOrder.ExtensionRecordEUDD001.MandateType = "ExtensionRecordEUDD001.MandateType";
                    SLM.SubmissionOrder.ExtensionRecordEUDD001.MandateID = "ExtensionRecordEUDD001.MandateID";
                    SLM.SubmissionOrder.ExtensionRecordEUDD001.SignatureDate = "ExtensionRecordEUDD001.SignatureDate";
                }

                public ExtensionRecordEUDD001()
                {
                }
            }

            public class ExtensionRecordGenericPrivateLabel
            {
                public readonly static string POSEntryMode;

                public readonly static string MerchantCategoryCode;

                static ExtensionRecordGenericPrivateLabel()
                {
                    SLM.SubmissionOrder.ExtensionRecordGenericPrivateLabel.POSEntryMode = "ExtensionRecordGenericPrivateLabel.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordGenericPrivateLabel.MerchantCategoryCode = "ExtensionRecordGenericPrivateLabel.MerchantCategoryCode";
                }

                public ExtensionRecordGenericPrivateLabel()
                {
                }
            }

            public class ExtensionRecordIntlMaestro001
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

                static ExtensionRecordIntlMaestro001()
                {
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.POSCapabilityCode = "ExtensionRecordIntlMaestro001.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.POSEntryMode = "ExtensionRecordIntlMaestro001.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.POSAuthorizationSource = "ExtensionRecordIntlMaestro001.POSAuthorizationSource";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.POSCardIDMethod = "ExtensionRecordIntlMaestro001.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.BanknetReferenceNumber = "ExtensionRecordIntlMaestro001.BanknetReferenceNumber";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.AuthorizedAmount = "ExtensionRecordIntlMaestro001.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.BanknetDate = "ExtensionRecordIntlMaestro001.BanknetDate";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.MerchantCategoryCode = "ExtensionRecordIntlMaestro001.MerchantCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.CATType = "ExtensionRecordIntlMaestro001.CATType";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.TransactionCategoryIndicator = "ExtensionRecordIntlMaestro001.TransactionCategoryIndicator";
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro001.AuthType = "ExtensionRecordIntlMaestro001.AuthType";
                }

                public ExtensionRecordIntlMaestro001()
                {
                }
            }

            public class ExtensionRecordIntlMaestro002
            {
                public readonly static string AccountholderAuthenticationValue;

                static ExtensionRecordIntlMaestro002()
                {
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro002.AccountholderAuthenticationValue = "ExtensionRecordIntlMaestro002.AccountholderAuthenticationValue";
                }

                public ExtensionRecordIntlMaestro002()
                {
                }
            }

            public class ExtensionRecordIntlMaestro003
            {
                public readonly static string SubtypeFlag;

                static ExtensionRecordIntlMaestro003()
                {
                    SLM.SubmissionOrder.ExtensionRecordIntlMaestro003.SubtypeFlag = "ExtensionRecordIntlMaestro003.SubtypeFlag";
                }

                public ExtensionRecordIntlMaestro003()
                {
                }
            }

            public class ExtensionRecordJapanCreditBureau001
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

                static ExtensionRecordJapanCreditBureau001()
                {
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.SystemTraceAuditNumber = "ExtensionRecordJapanCreditBureau001.SystemTraceAuditNumber";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.ProcessingCodeType = "ExtensionRecordJapanCreditBureau001.ProcessingCodeType";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.FromAccount = "ExtensionRecordJapanCreditBureau001.FromAccount";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.ToAccount = "ExtensionRecordJapanCreditBureau001.ToAccount";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.Track1DataConditionCode = "ExtensionRecordJapanCreditBureau001.Track1DataConditionCode";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.Track2DataConditionCode = "ExtensionRecordJapanCreditBureau001.Track2DataConditionCode";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.PANEntryMode = "ExtensionRecordJapanCreditBureau001.PANEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.PINEntryMode = "ExtensionRecordJapanCreditBureau001.PINEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.POSDeviceAttendanceIndicator = "ExtensionRecordJapanCreditBureau001.POSDeviceAttendanceIndicator";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.POSDeviceLocationIndicator = "ExtensionRecordJapanCreditBureau001.POSDeviceLocationIndicator";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.POSCardholderPresenceIndicator = "ExtensionRecordJapanCreditBureau001.POSCardholderPresenceIndicator";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.POSCardPresenceIndicator = "ExtensionRecordJapanCreditBureau001.POSCardPresenceIndicator";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.POSCardCaptureCapabilityIndicator = "ExtensionRecordJapanCreditBureau001.POSCardCaptureCapabilityIndicator";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.POSTransactionStatusIndicator = "ExtensionRecordJapanCreditBureau001.POSTransactionStatusIndicator";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.POSTransactionSecurityIndicator = "ExtensionRecordJapanCreditBureau001.POSTransactionSecurityIndicator";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.POSTypeOfTerminalDevice = "ExtensionRecordJapanCreditBureau001.POSTypeOfTerminalDevice";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.POSDeviceCardDataInputCapabilityIndicator = "ExtensionRecordJapanCreditBureau001.POSDeviceCardDataInputCapabilityIndicator";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.LocalTransactionDate = "ExtensionRecordJapanCreditBureau001.LocalTransactionDate";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.LocalTransactionTime = "ExtensionRecordJapanCreditBureau001.LocalTransactionTime";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau001.NetworkReferenceID = "ExtensionRecordJapanCreditBureau001.NetworkReferenceID";
                }

                public ExtensionRecordJapanCreditBureau001()
                {
                }
            }

            public class ExtensionRecordJapanCreditBureau002
            {
                public readonly static string RecordType;

                public readonly static string TAA1;

                public readonly static string TAA2;

                static ExtensionRecordJapanCreditBureau002()
                {
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau002.RecordType = "ExtensionRecordJapanCreditBureau002.RecordType";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau002.TAA1 = "ExtensionRecordJapanCreditBureau002.TAA1";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau002.TAA2 = "ExtensionRecordJapanCreditBureau002.TAA2";
                }

                public ExtensionRecordJapanCreditBureau002()
                {
                }
            }

            public class ExtensionRecordJapanCreditBureau003
            {
                public readonly static string RecordType;

                public readonly static string TAA3;

                public readonly static string TAA4;

                static ExtensionRecordJapanCreditBureau003()
                {
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau003.RecordType = "ExtensionRecordJapanCreditBureau003.RecordType";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau003.TAA3 = "ExtensionRecordJapanCreditBureau003.TAA3";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau003.TAA4 = "ExtensionRecordJapanCreditBureau003.TAA4";
                }

                public ExtensionRecordJapanCreditBureau003()
                {
                }
            }

            public class ExtensionRecordJapanCreditBureau004
            {
                public readonly static string RecordType;

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

                static ExtensionRecordJapanCreditBureau004()
                {
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.RecordType = "ExtensionRecordJapanCreditBureau004.RecordType";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.TransactionIdentifier = "ExtensionRecordJapanCreditBureau004.TransactionIdentifier";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.POSCapabilityCode = "ExtensionRecordJapanCreditBureau004.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.AccountholderAuthenticationCapability = "ExtensionRecordJapanCreditBureau004.AccountholderAuthenticationCapability";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.CardCaptureCapability = "ExtensionRecordJapanCreditBureau004.CardCaptureCapability";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.OperatingEnvironment = "ExtensionRecordJapanCreditBureau004.OperatingEnvironment";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.AccountholderPresent = "ExtensionRecordJapanCreditBureau004.AccountholderPresent";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.CardPresent = "ExtensionRecordJapanCreditBureau004.CardPresent";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.POSEntryMode = "ExtensionRecordJapanCreditBureau004.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.POSCardIDMethod = "ExtensionRecordJapanCreditBureau004.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.AccountholderAuthenticationEntity = "ExtensionRecordJapanCreditBureau004.AccountholderAuthenticationEntity";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.CardDataOutputCapability = "ExtensionRecordJapanCreditBureau004.CardDataOutputCapability";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.TerminalOutputCapability = "ExtensionRecordJapanCreditBureau004.TerminalOutputCapability";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.PINCaptureCapability = "ExtensionRecordJapanCreditBureau004.PINCaptureCapability";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau004.AuthorizedAmount = "ExtensionRecordJapanCreditBureau004.AuthorizedAmount";
                }

                public ExtensionRecordJapanCreditBureau004()
                {
                }
            }

            public class ExtensionRecordJapanCreditBureau005
            {
                public readonly static string RecordType;

                public readonly static string AEVV;

                public readonly static string TransactionID;

                static ExtensionRecordJapanCreditBureau005()
                {
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau005.RecordType = "ExtensionRecordJapanCreditBureau005.RecordType";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau005.AEVV = "ExtensionRecordJapanCreditBureau005.AEVV";
                    SLM.SubmissionOrder.ExtensionRecordJapanCreditBureau005.TransactionID = "ExtensionRecordJapanCreditBureau005.TransactionID";
                }

                public ExtensionRecordJapanCreditBureau005()
                {
                }
            }

            public class ExtensionRecordMasterCard0001
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

                static ExtensionRecordMasterCard0001()
                {
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.POSCapabilityCode = "ExtensionRecordMasterCard0001.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.POSEntryMode = "ExtensionRecordMasterCard0001.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.POSAuthSource = "ExtensionRecordMasterCard0001.POSAuthSource";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.POSCardIDMethod = "ExtensionRecordMasterCard0001.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.FinancialNetworkCode = "ExtensionRecordMasterCard0001.FinancialNetworkCode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.BankNetReferenceNumber = "ExtensionRecordMasterCard0001.BankNetReferenceNumber";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.AuthorizedAmount = "ExtensionRecordMasterCard0001.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.BankNetDate = "ExtensionRecordMasterCard0001.BankNetDate";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.MerchantCategoryCode = "ExtensionRecordMasterCard0001.MerchantCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.CATType = "ExtensionRecordMasterCard0001.CATType";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.TransactionCategoryIndicator = "ExtensionRecordMasterCard0001.TransactionCategoryIndicator";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.AutomatedFuelDispenserCompletionAdviceFlag = "ExtensionRecordMasterCard0001.AutomatedFuelDispenserCompletionAdviceFlag";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.UCAFCollectionIndicator = "ExtensionRecordMasterCard0001.UCAFCollectionIndicator";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.CardholderPresent = "ExtensionRecordMasterCard0001.CardholderPresent";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.CardPresent = "ExtensionRecordMasterCard0001.CardPresent";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard0001.AuthType = "ExtensionRecordMasterCard0001.AuthType";
                }

                public ExtensionRecordMasterCard0001()
                {
                }
            }

            public class ExtensionRecordMasterCard001
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

                public readonly static string TransactionCategoryIndicator;

                public readonly static string AutomatedFuelDispenserCompletionAdviceFlag;

                public readonly static string UCAFCollectionIndicator;

                public readonly static string CardholderPresent;

                public readonly static string CardPresent;

                public readonly static string AuthType;

                public readonly static string TransactionIntegrityClassification;

                static ExtensionRecordMasterCard001()
                {
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.POSCapabilityCode = "ExtensionRecordMasterCard001.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.POSEntryMode = "ExtensionRecordMasterCard001.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.POSAuthSource = "ExtensionRecordMasterCard001.POSAuthSource";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.POSCardIDMethod = "ExtensionRecordMasterCard001.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.BankNetRefNumber = "ExtensionRecordMasterCard001.BankNetRefNumber";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.AuthorizedAmount = "ExtensionRecordMasterCard001.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.BankNetDate = "ExtensionRecordMasterCard001.BankNetDate";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.MerchantCategoryCode = "ExtensionRecordMasterCard001.MerchantCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.CATType = "ExtensionRecordMasterCard001.CATType";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.TransactionCategoryIndicator = "ExtensionRecordMasterCard001.TransactionCategoryIndicator";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.AutomatedFuelDispenserCompletionAdviceFlag = "ExtensionRecordMasterCard001.AutomatedFuelDispenserCompletionAdviceFlag";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.UCAFCollectionIndicator = "ExtensionRecordMasterCard001.UCAFCollectionIndicator";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.CardholderPresent = "ExtensionRecordMasterCard001.CardholderPresent";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.CardPresent = "ExtensionRecordMasterCard001.CardPresent";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.AuthType = "ExtensionRecordMasterCard001.AuthType";
                    SLM.SubmissionOrder.ExtensionRecordMasterCard001.TransactionIntegrityClassification = "ExtensionRecordMasterCard001.TransactionIntegrityClassification";
                }

                public ExtensionRecordMasterCard001()
                {
                }
            }

            public class ExtensionRecordMasterCard002
            {
                public readonly static string AcctAuthValue;

                static ExtensionRecordMasterCard002()
                {
                    SLM.SubmissionOrder.ExtensionRecordMasterCard002.AcctAuthValue = "ExtensionRecordMasterCard002.AcctAuthValue";
                }

                public ExtensionRecordMasterCard002()
                {
                }
            }

            public class ExtensionRecordMasterCard003
            {
                public readonly static string SubtypeFlag;

                static ExtensionRecordMasterCard003()
                {
                    SLM.SubmissionOrder.ExtensionRecordMasterCard003.SubtypeFlag = "ExtensionRecordMasterCard003.SubtypeFlag";
                }

                public ExtensionRecordMasterCard003()
                {
                }
            }

            public class ExtensionRecordMasterCardDiners001
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

                static ExtensionRecordMasterCardDiners001()
                {
                    SLM.SubmissionOrder.ExtensionRecordMasterCardDiners001.POSCapabilityCode = "ExtensionRecordMasterCardDiners001.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCardDiners001.POSEntryMode = "ExtensionRecordMasterCardDiners001.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCardDiners001.POSAuthSource = "ExtensionRecordMasterCardDiners001.POSAuthSource";
                    SLM.SubmissionOrder.ExtensionRecordMasterCardDiners001.POSCardIDMethod = "ExtensionRecordMasterCardDiners001.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordMasterCardDiners001.BankNetRefNumber = "ExtensionRecordMasterCardDiners001.BankNetRefNumber";
                    SLM.SubmissionOrder.ExtensionRecordMasterCardDiners001.AuthorizedAmount = "ExtensionRecordMasterCardDiners001.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordMasterCardDiners001.BankNetDate = "ExtensionRecordMasterCardDiners001.BankNetDate";
                    SLM.SubmissionOrder.ExtensionRecordMasterCardDiners001.MerchantCategoryCode = "ExtensionRecordMasterCardDiners001.MerchantCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordMasterCardDiners001.CATType = "ExtensionRecordMasterCardDiners001.CATType";
                }

                public ExtensionRecordMasterCardDiners001()
                {
                }
            }

            public class ExtensionRecordMoneyPak001
            {
                public readonly static string OriginalTransactionID;

                public readonly static string ConfirmationID;

                public readonly static string MoneyPakAccountNumber;

                static ExtensionRecordMoneyPak001()
                {
                    SLM.SubmissionOrder.ExtensionRecordMoneyPak001.OriginalTransactionID = "ExtensionRecordMoneyPak001.OriginalTransactionID";
                    SLM.SubmissionOrder.ExtensionRecordMoneyPak001.ConfirmationID = "ExtensionRecordMoneyPak001.ConfirmationID";
                    SLM.SubmissionOrder.ExtensionRecordMoneyPak001.MoneyPakAccountNumber = "ExtensionRecordMoneyPak001.MoneyPakAccountNumber";
                }

                public ExtensionRecordMoneyPak001()
                {
                }
            }

            public class ExtensionRecordPayPal001
            {
                public readonly static string SubTypeFlag;

                public readonly static string ContractID;

                public readonly static string TransactionID;

                public readonly static string PayerID;

                static ExtensionRecordPayPal001()
                {
                    SLM.SubmissionOrder.ExtensionRecordPayPal001.SubTypeFlag = "ExtensionRecordPayPal001.SubTypeFlag";
                    SLM.SubmissionOrder.ExtensionRecordPayPal001.ContractID = "ExtensionRecordPayPal001.ContractID";
                    SLM.SubmissionOrder.ExtensionRecordPayPal001.TransactionID = "ExtensionRecordPayPal001.TransactionID";
                    SLM.SubmissionOrder.ExtensionRecordPayPal001.PayerID = "ExtensionRecordPayPal001.PayerID";
                }

                public ExtensionRecordPayPal001()
                {
                }
            }

            public class ExtensionRecordPayPal002
            {
                public readonly static string ReceiverType;

                static ExtensionRecordPayPal002()
                {
                    SLM.SubmissionOrder.ExtensionRecordPayPal002.ReceiverType = "ExtensionRecordPayPal002.ReceiverType";
                }

                public ExtensionRecordPayPal002()
                {
                }
            }

            public class ExtensionRecordPayPal003
            {
                public readonly static string ReceiverID;

                public readonly static string Amount;

                public readonly static string UniqueID;

                public readonly static string Note;

                static ExtensionRecordPayPal003()
                {
                    SLM.SubmissionOrder.ExtensionRecordPayPal003.ReceiverID = "ExtensionRecordPayPal003.ReceiverID";
                    SLM.SubmissionOrder.ExtensionRecordPayPal003.Amount = "ExtensionRecordPayPal003.Amount";
                    SLM.SubmissionOrder.ExtensionRecordPayPal003.UniqueID = "ExtensionRecordPayPal003.UniqueID";
                    SLM.SubmissionOrder.ExtensionRecordPayPal003.Note = "ExtensionRecordPayPal003.Note";
                }

                public ExtensionRecordPayPal003()
                {
                }
            }

            public class ExtensionRecordPrivateLabelBeneficial001
            {
                public readonly static string CreditPlan;

                public readonly static string DepartmentCode;

                public readonly static string SKUNumber;

                public readonly static string ItemDescription;

                public readonly static string StoreNumber;

                static ExtensionRecordPrivateLabelBeneficial001()
                {
                    SLM.SubmissionOrder.ExtensionRecordPrivateLabelBeneficial001.CreditPlan = "ExtensionRecordPrivateLabelBeneficial001.CreditPlan";
                    SLM.SubmissionOrder.ExtensionRecordPrivateLabelBeneficial001.DepartmentCode = "ExtensionRecordPrivateLabelBeneficial001.DepartmentCode";
                    SLM.SubmissionOrder.ExtensionRecordPrivateLabelBeneficial001.SKUNumber = "ExtensionRecordPrivateLabelBeneficial001.SKUNumber";
                    SLM.SubmissionOrder.ExtensionRecordPrivateLabelBeneficial001.ItemDescription = "ExtensionRecordPrivateLabelBeneficial001.ItemDescription";
                    SLM.SubmissionOrder.ExtensionRecordPrivateLabelBeneficial001.StoreNumber = "ExtensionRecordPrivateLabelBeneficial001.StoreNumber";
                }

                public ExtensionRecordPrivateLabelBeneficial001()
                {
                }
            }

            public class ExtensionRecordRevolutionCard001
            {
                public readonly static string OneTimeTokenID;

                public readonly static string TraceNumber;

                public readonly static string TransactionID;

                static ExtensionRecordRevolutionCard001()
                {
                    SLM.SubmissionOrder.ExtensionRecordRevolutionCard001.OneTimeTokenID = "ExtensionRecordRevolutionCard001.OneTimeTokenID";
                    SLM.SubmissionOrder.ExtensionRecordRevolutionCard001.TraceNumber = "ExtensionRecordRevolutionCard001.TraceNumber";
                    SLM.SubmissionOrder.ExtensionRecordRevolutionCard001.TransactionID = "ExtensionRecordRevolutionCard001.TransactionID";
                }

                public ExtensionRecordRevolutionCard001()
                {
                }
            }

            public class ExtensionRecordStoredValue
            {
                public readonly static string POSEntryMode;

                public readonly static string MerchantCategoryCode;

                static ExtensionRecordStoredValue()
                {
                    SLM.SubmissionOrder.ExtensionRecordStoredValue.POSEntryMode = "ExtensionRecordStoredValue.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordStoredValue.MerchantCategoryCode = "ExtensionRecordStoredValue.MerchantCategoryCode";
                }

                public ExtensionRecordStoredValue()
                {
                }
            }

            public class ExtensionRecordUKDomesticMaestro001
            {
                public readonly static string CardStartDate;

                public readonly static string CardIssueNumber;

                public readonly static string AccountHolderAuthValue;

                static ExtensionRecordUKDomesticMaestro001()
                {
                    SLM.SubmissionOrder.ExtensionRecordUKDomesticMaestro001.CardStartDate = "ExtensionRecordUKDomesticMaestro001.CardStartDate";
                    SLM.SubmissionOrder.ExtensionRecordUKDomesticMaestro001.CardIssueNumber = "ExtensionRecordUKDomesticMaestro001.CardIssueNumber";
                    SLM.SubmissionOrder.ExtensionRecordUKDomesticMaestro001.AccountHolderAuthValue = "ExtensionRecordUKDomesticMaestro001.AccountHolderAuthValue";
                }

                public ExtensionRecordUKDomesticMaestro001()
                {
                }
            }

            public class ExtensionRecordVisa001
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

                static ExtensionRecordVisa001()
                {
                    SLM.SubmissionOrder.ExtensionRecordVisa001.POSCapabilityCode = "ExtensionRecordVisa001.POSCapabilityCode";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.POSEntryMode = "ExtensionRecordVisa001.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.POSAuthSource = "ExtensionRecordVisa001.POSAuthSource";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.POSCardIDMethod = "ExtensionRecordVisa001.POSCardIDMethod";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.AuthCharacteristicIndicator = "ExtensionRecordVisa001.AuthCharacteristicIndicator";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.TransactionID = "ExtensionRecordVisa001.TransactionID";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.ValidationCode = "ExtensionRecordVisa001.ValidationCode";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.AuthorizedAmount = "ExtensionRecordVisa001.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.MerchCategoryCode = "ExtensionRecordVisa001.MerchCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.TotalAuthAmount = "ExtensionRecordVisa001.TotalAuthAmount";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.MarketSpecificDataIndicator = "ExtensionRecordVisa001.MarketSpecificDataIndicator";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.CATType = "ExtensionRecordVisa001.CATType";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.CardLevelResult = "ExtensionRecordVisa001.CardLevelResult";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.PartialAuthIndicator = "ExtensionRecordVisa001.PartialAuthIndicator";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.AuthorizationResponseCode = "ExtensionRecordVisa001.AuthorizationResponseCode";
                    SLM.SubmissionOrder.ExtensionRecordVisa001.SpendQualifiedIndicator = "ExtensionRecordVisa001.SpendQualifiedIndicator";
                }

                public ExtensionRecordVisa001()
                {
                }
            }

            public class ExtensionRecordVisa002
            {
                public readonly static string TransactionID;

                public readonly static string CAVV;

                public readonly static string CAVVResponseCode;

                static ExtensionRecordVisa002()
                {
                    SLM.SubmissionOrder.ExtensionRecordVisa002.TransactionID = "ExtensionRecordVisa002.TransactionID";
                    SLM.SubmissionOrder.ExtensionRecordVisa002.CAVV = "ExtensionRecordVisa002.CAVV";
                    SLM.SubmissionOrder.ExtensionRecordVisa002.CAVVResponseCode = "ExtensionRecordVisa002.CAVVResponseCode";
                }

                public ExtensionRecordVisa002()
                {
                }
            }

            public class ExtensionRecordVoyager
            {
                public readonly static string AuthorizedAmount;

                public readonly static string AuthorizationResponse;

                public readonly static string MerchantCategoryCode;

                public readonly static string POSEntryMode;

                public readonly static string OriginalInvoiceNumber;

                public readonly static string PurchaseDate;

                public readonly static string PartialAuthorizationIndicator;

                static ExtensionRecordVoyager()
                {
                    SLM.SubmissionOrder.ExtensionRecordVoyager.AuthorizedAmount = "ExtensionRecordVoyager.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordVoyager.AuthorizationResponse = "ExtensionRecordVoyager.AuthorizationResponse";
                    SLM.SubmissionOrder.ExtensionRecordVoyager.MerchantCategoryCode = "ExtensionRecordVoyager.MerchantCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordVoyager.POSEntryMode = "ExtensionRecordVoyager.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordVoyager.OriginalInvoiceNumber = "ExtensionRecordVoyager.OriginalInvoiceNumber";
                    SLM.SubmissionOrder.ExtensionRecordVoyager.PurchaseDate = "ExtensionRecordVoyager.PurchaseDate";
                    SLM.SubmissionOrder.ExtensionRecordVoyager.PartialAuthorizationIndicator = "ExtensionRecordVoyager.PartialAuthorizationIndicator";
                }

                public ExtensionRecordVoyager()
                {
                }
            }

            public class ExtensionRecordWrightExpress
            {
                public readonly static string AuthorizedAmount;

                public readonly static string AuthorizationResponse;

                public readonly static string MerchantCategoryCode;

                public readonly static string POSEntryMode;

                public readonly static string TicketNumber;

                public readonly static string PurchaseDate;

                static ExtensionRecordWrightExpress()
                {
                    SLM.SubmissionOrder.ExtensionRecordWrightExpress.AuthorizedAmount = "ExtensionRecordWrightExpress.AuthorizedAmount";
                    SLM.SubmissionOrder.ExtensionRecordWrightExpress.AuthorizationResponse = "ExtensionRecordWrightExpress.AuthorizationResponse";
                    SLM.SubmissionOrder.ExtensionRecordWrightExpress.MerchantCategoryCode = "ExtensionRecordWrightExpress.MerchantCategoryCode";
                    SLM.SubmissionOrder.ExtensionRecordWrightExpress.POSEntryMode = "ExtensionRecordWrightExpress.POSEntryMode";
                    SLM.SubmissionOrder.ExtensionRecordWrightExpress.TicketNumber = "ExtensionRecordWrightExpress.TicketNumber";
                    SLM.SubmissionOrder.ExtensionRecordWrightExpress.PurchaseDate = "ExtensionRecordWrightExpress.PurchaseDate";
                }

                public ExtensionRecordWrightExpress()
                {
                }
            }

            public class FormattedAddressRecordBillToAddress
            {
                public readonly static string NameText;

                public readonly static string AddrLine1;

                public readonly static string AddrLine2;

                public readonly static string City;

                public readonly static string State;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                static FormattedAddressRecordBillToAddress()
                {
                    SLM.SubmissionOrder.FormattedAddressRecordBillToAddress.NameText = "FormattedAddressRecordBillToAddress.NameText";
                    SLM.SubmissionOrder.FormattedAddressRecordBillToAddress.AddrLine1 = "FormattedAddressRecordBillToAddress.AddrLine1";
                    SLM.SubmissionOrder.FormattedAddressRecordBillToAddress.AddrLine2 = "FormattedAddressRecordBillToAddress.AddrLine2";
                    SLM.SubmissionOrder.FormattedAddressRecordBillToAddress.City = "FormattedAddressRecordBillToAddress.City";
                    SLM.SubmissionOrder.FormattedAddressRecordBillToAddress.State = "FormattedAddressRecordBillToAddress.State";
                    SLM.SubmissionOrder.FormattedAddressRecordBillToAddress.PostalCode = "FormattedAddressRecordBillToAddress.PostalCode";
                    SLM.SubmissionOrder.FormattedAddressRecordBillToAddress.CountryCode = "FormattedAddressRecordBillToAddress.CountryCode";
                }

                public FormattedAddressRecordBillToAddress()
                {
                }
            }

            public class FormattedAddressRecordBillToName
            {
                public readonly static string FirstName;

                public readonly static string LastName;

                static FormattedAddressRecordBillToName()
                {
                    SLM.SubmissionOrder.FormattedAddressRecordBillToName.FirstName = "FormattedAddressRecordBillToName.FirstName";
                    SLM.SubmissionOrder.FormattedAddressRecordBillToName.LastName = "FormattedAddressRecordBillToName.LastName";
                }

                public FormattedAddressRecordBillToName()
                {
                }
            }

            public class FormattedAddressRecordBillToTelephone
            {
                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                static FormattedAddressRecordBillToTelephone()
                {
                    SLM.SubmissionOrder.FormattedAddressRecordBillToTelephone.TelephoneType = "FormattedAddressRecordBillToTelephone.TelephoneType";
                    SLM.SubmissionOrder.FormattedAddressRecordBillToTelephone.TelephoneNumber = "FormattedAddressRecordBillToTelephone.TelephoneNumber";
                }

                public FormattedAddressRecordBillToTelephone()
                {
                }
            }

            public class FormattedAddressRecordECPandEUDD
            {
                public readonly static string NameText;

                public readonly static string AddrLine1;

                public readonly static string AddrLine2;

                public readonly static string City;

                public readonly static string State;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                static FormattedAddressRecordECPandEUDD()
                {
                    SLM.SubmissionOrder.FormattedAddressRecordECPandEUDD.NameText = "FormattedAddressRecordECPandEUDD.NameText";
                    SLM.SubmissionOrder.FormattedAddressRecordECPandEUDD.AddrLine1 = "FormattedAddressRecordECPandEUDD.AddrLine1";
                    SLM.SubmissionOrder.FormattedAddressRecordECPandEUDD.AddrLine2 = "FormattedAddressRecordECPandEUDD.AddrLine2";
                    SLM.SubmissionOrder.FormattedAddressRecordECPandEUDD.City = "FormattedAddressRecordECPandEUDD.City";
                    SLM.SubmissionOrder.FormattedAddressRecordECPandEUDD.State = "FormattedAddressRecordECPandEUDD.State";
                    SLM.SubmissionOrder.FormattedAddressRecordECPandEUDD.PostalCode = "FormattedAddressRecordECPandEUDD.PostalCode";
                    SLM.SubmissionOrder.FormattedAddressRecordECPandEUDD.CountryCode = "FormattedAddressRecordECPandEUDD.CountryCode";
                }

                public FormattedAddressRecordECPandEUDD()
                {
                }
            }

            public class FormattedAddressRecordECPandEUDDTelelphone
            {
                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                static FormattedAddressRecordECPandEUDDTelelphone()
                {
                    SLM.SubmissionOrder.FormattedAddressRecordECPandEUDDTelelphone.TelephoneType = "FormattedAddressRecordECPandEUDDTelelphone.TelephoneType";
                    SLM.SubmissionOrder.FormattedAddressRecordECPandEUDDTelelphone.TelephoneNumber = "FormattedAddressRecordECPandEUDDTelelphone.TelephoneNumber";
                }

                public FormattedAddressRecordECPandEUDDTelelphone()
                {
                }
            }

            public class FormattedAddressRecordShipToAddress
            {
                public readonly static string NameText;

                public readonly static string AddrLine1;

                public readonly static string AddrLine2;

                public readonly static string City;

                public readonly static string State;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                static FormattedAddressRecordShipToAddress()
                {
                    SLM.SubmissionOrder.FormattedAddressRecordShipToAddress.NameText = "FormattedAddressRecordShipToAddress.NameText";
                    SLM.SubmissionOrder.FormattedAddressRecordShipToAddress.AddrLine1 = "FormattedAddressRecordShipToAddress.AddrLine1";
                    SLM.SubmissionOrder.FormattedAddressRecordShipToAddress.AddrLine2 = "FormattedAddressRecordShipToAddress.AddrLine2";
                    SLM.SubmissionOrder.FormattedAddressRecordShipToAddress.City = "FormattedAddressRecordShipToAddress.City";
                    SLM.SubmissionOrder.FormattedAddressRecordShipToAddress.State = "FormattedAddressRecordShipToAddress.State";
                    SLM.SubmissionOrder.FormattedAddressRecordShipToAddress.PostalCode = "FormattedAddressRecordShipToAddress.PostalCode";
                    SLM.SubmissionOrder.FormattedAddressRecordShipToAddress.CountryCode = "FormattedAddressRecordShipToAddress.CountryCode";
                }

                public FormattedAddressRecordShipToAddress()
                {
                }
            }

            public class FormattedAddressRecordShipToName
            {
                public readonly static string FirstName;

                public readonly static string LastName;

                static FormattedAddressRecordShipToName()
                {
                    SLM.SubmissionOrder.FormattedAddressRecordShipToName.FirstName = "FormattedAddressRecordShipToName.FirstName";
                    SLM.SubmissionOrder.FormattedAddressRecordShipToName.LastName = "FormattedAddressRecordShipToName.LastName";
                }

                public FormattedAddressRecordShipToName()
                {
                }
            }

            public class FormattedAddressRecordShipToTelephone
            {
                public readonly static string TelephoneType;

                public readonly static string TelephoneNumber;

                static FormattedAddressRecordShipToTelephone()
                {
                    SLM.SubmissionOrder.FormattedAddressRecordShipToTelephone.TelephoneType = "FormattedAddressRecordShipToTelephone.TelephoneType";
                    SLM.SubmissionOrder.FormattedAddressRecordShipToTelephone.TelephoneNumber = "FormattedAddressRecordShipToTelephone.TelephoneNumber";
                }

                public FormattedAddressRecordShipToTelephone()
                {
                }
            }

            public class InformationRecordGoodsSold001
            {
                public readonly static string GoodsSold;

                static InformationRecordGoodsSold001()
                {
                    SLM.SubmissionOrder.InformationRecordGoodsSold001.GoodsSold = "InformationRecordGoodsSold001.GoodsSold";
                }

                public InformationRecordGoodsSold001()
                {
                }
            }

            public class InformationRecordOrderInformation001
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

                static InformationRecordOrderInformation001()
                {
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.ProductDeliveryTypeIndicator = "InformationRecordOrderInformation001.ProductDeliveryTypeIndicator";
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.ShippingCarrier = "InformationRecordOrderInformation001.ShippingCarrier";
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.ShippingMethod = "InformationRecordOrderInformation001.ShippingMethod";
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.OrderDate = "InformationRecordOrderInformation001.OrderDate";
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.OrderTime = "InformationRecordOrderInformation001.OrderTime";
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.TrackingNumber = "InformationRecordOrderInformation001.TrackingNumber";
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.MCC = "InformationRecordOrderInformation001.MCC";
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.SKU = "InformationRecordOrderInformation001.SKU";
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.NumberOfShipments = "InformationRecordOrderInformation001.NumberOfShipments";
                    SLM.SubmissionOrder.InformationRecordOrderInformation001.ShipperCode = "InformationRecordOrderInformation001.ShipperCode";
                }

                public InformationRecordOrderInformation001()
                {
                }
            }

            public class InformationRecordPersonalInformation001
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

                static InformationRecordPersonalInformation001()
                {
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerDOB = "InformationRecordPersonalInformation001.CustomerDOB";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerSSN = "InformationRecordPersonalInformation001.CustomerSSN";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CurrencyTypeAnnualIncome = "InformationRecordPersonalInformation001.CurrencyTypeAnnualIncome";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.HouseholdAnnualIncome = "InformationRecordPersonalInformation001.HouseholdAnnualIncome";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerResStatus = "InformationRecordPersonalInformation001.CustomerResStatus";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerYrsAtRes = "InformationRecordPersonalInformation001.CustomerYrsAtRes";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerYrsAtEmployer = "InformationRecordPersonalInformation001.CustomerYrsAtEmployer";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerCheckAcct = "InformationRecordPersonalInformation001.CustomerCheckAcct";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerSavingAcct = "InformationRecordPersonalInformation001.CustomerSavingAcct";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerDrivLicNumber = "InformationRecordPersonalInformation001.CustomerDrivLicNumber";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerDrivLicState = "InformationRecordPersonalInformation001.CustomerDrivLicState";
                    SLM.SubmissionOrder.InformationRecordPersonalInformation001.CustomerDrivLicCountry = "InformationRecordPersonalInformation001.CustomerDrivLicCountry";
                }

                public InformationRecordPersonalInformation001()
                {
                }
            }

            public class InformationRecordTransactionInformation001
            {
                public readonly static string SurchargeAmount;

                static InformationRecordTransactionInformation001()
                {
                    SLM.SubmissionOrder.InformationRecordTransactionInformation001.SurchargeAmount = "InformationRecordTransactionInformation001.SurchargeAmount";
                }

                public InformationRecordTransactionInformation001()
                {
                }
            }

            public class MerchantDescriptorRecordCreditCard
            {
                public readonly static string MerchantNameItemDescription;

                public readonly static string MerchantCityCSRPhone;

                public readonly static string MerchantState;

                static MerchantDescriptorRecordCreditCard()
                {
                    SLM.SubmissionOrder.MerchantDescriptorRecordCreditCard.MerchantNameItemDescription = "MerchantDescriptorRecordCreditCard.MerchantNameItemDescription";
                    SLM.SubmissionOrder.MerchantDescriptorRecordCreditCard.MerchantCityCSRPhone = "MerchantDescriptorRecordCreditCard.MerchantCityCSRPhone";
                    SLM.SubmissionOrder.MerchantDescriptorRecordCreditCard.MerchantState = "MerchantDescriptorRecordCreditCard.MerchantState";
                }

                public MerchantDescriptorRecordCreditCard()
                {
                }
            }

            public class MerchantDescriptorRecordElectronicCheck
            {
                public readonly static string MerchantName;

                public readonly static string EntryDescription;

                static MerchantDescriptorRecordElectronicCheck()
                {
                    SLM.SubmissionOrder.MerchantDescriptorRecordElectronicCheck.MerchantName = "MerchantDescriptorRecordElectronicCheck.MerchantName";
                    SLM.SubmissionOrder.MerchantDescriptorRecordElectronicCheck.EntryDescription = "MerchantDescriptorRecordElectronicCheck.EntryDescription";
                }

                public MerchantDescriptorRecordElectronicCheck()
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
                    SLM.SubmissionOrder.PassengerTransExtRecord3.ClearingSequenceNumber = "PassengerTransExtRecord3.ClearingSequenceNumber";
                    SLM.SubmissionOrder.PassengerTransExtRecord3.ClearingCount = "PassengerTransExtRecord3.ClearingCount";
                    SLM.SubmissionOrder.PassengerTransExtRecord3.TotalClearingAmount = "PassengerTransExtRecord3.TotalClearingAmount";
                    SLM.SubmissionOrder.PassengerTransExtRecord3.ComputerizedReservationSystem = "PassengerTransExtRecord3.ComputerizedReservationSystem";
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
                    SLM.SubmissionOrder.PassengerTransTicInfo1.TicketNumber = "PassengerTransTicInfo1.TicketNumber";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.PassengerName = "PassengerTransTicInfo1.PassengerName";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.CustomerCode = "PassengerTransTicInfo1.CustomerCode";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.IssueDate = "PassengerTransTicInfo1.IssueDate";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.IssuingCarrier = "PassengerTransTicInfo1.IssuingCarrier";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.ArrivalDate = "PassengerTransTicInfo1.ArrivalDate";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.NumberInParty = "PassengerTransTicInfo1.NumberInParty";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.ConjunctionTicketInd = "PassengerTransTicInfo1.ConjunctionTicketInd";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.ElectronicTicketInd = "PassengerTransTicInfo1.ElectronicTicketInd";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.RestrictedTicketInd = "PassengerTransTicInfo1.RestrictedTicketInd";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.IATAClientCode = "PassengerTransTicInfo1.IATAClientCode";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.CreditReasonIndicator = "PassengerTransTicInfo1.CreditReasonIndicator";
                    SLM.SubmissionOrder.PassengerTransTicInfo1.TicketChangeIndicator = "PassengerTransTicInfo1.TicketChangeIndicator";
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
                    SLM.SubmissionOrder.PassengerTransTicInfo2.TotalFare = "PassengerTransTicInfo2.TotalFare";
                    SLM.SubmissionOrder.PassengerTransTicInfo2.TotalFees = "PassengerTransTicInfo2.TotalFees";
                    SLM.SubmissionOrder.PassengerTransTicInfo2.TotalTaxes = "PassengerTransTicInfo2.TotalTaxes";
                    SLM.SubmissionOrder.PassengerTransTicInfo2.ExchangeTicketAmount = "PassengerTransTicInfo2.ExchangeTicketAmount";
                    SLM.SubmissionOrder.PassengerTransTicInfo2.ExchangeFeeAmount = "PassengerTransTicInfo2.ExchangeFeeAmount";
                    SLM.SubmissionOrder.PassengerTransTicInfo2.InvoiceNumber = "PassengerTransTicInfo2.InvoiceNumber";
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
                    SLM.SubmissionOrder.PassengerTransTicInfo3.TravelAgencyCode = "PassengerTransTicInfo3.TravelAgencyCode";
                    SLM.SubmissionOrder.PassengerTransTicInfo3.TravelAgencyName = "PassengerTransTicInfo3.TravelAgencyName";
                    SLM.SubmissionOrder.PassengerTransTicInfo3.TravelAuthorizationCode = "PassengerTransTicInfo3.TravelAuthorizationCode";
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
                    SLM.SubmissionOrder.PassengerTransTicInfo4.AncillaryServiceCat1 = "PassengerTransTicInfo4.AncillaryServiceCat1";
                    SLM.SubmissionOrder.PassengerTransTicInfo4.AncillaryServiceSubCat1 = "PassengerTransTicInfo4.AncillaryServiceSubCat1";
                    SLM.SubmissionOrder.PassengerTransTicInfo4.AncillaryServiceCat2 = "PassengerTransTicInfo4.AncillaryServiceCat2";
                    SLM.SubmissionOrder.PassengerTransTicInfo4.AncillaryServiceSubCat2 = "PassengerTransTicInfo4.AncillaryServiceSubCat2";
                    SLM.SubmissionOrder.PassengerTransTicInfo4.AncillaryServiceCat3 = "PassengerTransTicInfo4.AncillaryServiceCat3";
                    SLM.SubmissionOrder.PassengerTransTicInfo4.AncillaryServiceSubCat3 = "PassengerTransTicInfo4.AncillaryServiceSubCat3";
                    SLM.SubmissionOrder.PassengerTransTicInfo4.AncillaryServiceCat4 = "PassengerTransTicInfo4.AncillaryServiceCat4";
                    SLM.SubmissionOrder.PassengerTransTicInfo4.AncillaryServiceSubCat4 = "PassengerTransTicInfo4.AncillaryServiceSubCat4";
                    SLM.SubmissionOrder.PassengerTransTicInfo4.Description = "PassengerTransTicInfo4.Description";
                    SLM.SubmissionOrder.PassengerTransTicInfo4.AssociatedTicketNumber = "PassengerTransTicInfo4.AssociatedTicketNumber";
                }

                public PassengerTransTicInfo4()
                {
                }
            }

            public class PassengerTransTrip1
            {
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
                    SLM.SubmissionOrder.PassengerTransTrip1.ConjunctionTicketNumber = "PassengerTransTrip1.ConjunctionTicketNumber";
                    SLM.SubmissionOrder.PassengerTransTrip1.ExchangeTicketNumber = "PassengerTransTrip1.ExchangeTicketNumber";
                    SLM.SubmissionOrder.PassengerTransTrip1.CouponNumber = "PassengerTransTrip1.CouponNumber";
                    SLM.SubmissionOrder.PassengerTransTrip1.ServiceClass = "PassengerTransTrip1.ServiceClass";
                    SLM.SubmissionOrder.PassengerTransTrip1.CarrierCode = "PassengerTransTrip1.CarrierCode";
                    SLM.SubmissionOrder.PassengerTransTrip1.StopOverCode = "PassengerTransTrip1.StopOverCode";
                    SLM.SubmissionOrder.PassengerTransTrip1.CityOfOriginAirportCode = "PassengerTransTrip1.CityOfOriginAirportCode";
                    SLM.SubmissionOrder.PassengerTransTrip1.CityOfDestinationAirportCode = "PassengerTransTrip1.CityOfDestinationAirportCode";
                    SLM.SubmissionOrder.PassengerTransTrip1.FlightNumber = "PassengerTransTrip1.FlightNumber";
                    SLM.SubmissionOrder.PassengerTransTrip1.DepartureDate = "PassengerTransTrip1.DepartureDate";
                    SLM.SubmissionOrder.PassengerTransTrip1.DepartureTime = "PassengerTransTrip1.DepartureTime";
                    SLM.SubmissionOrder.PassengerTransTrip1.ArrivalTime = "PassengerTransTrip1.ArrivalTime";
                    SLM.SubmissionOrder.PassengerTransTrip1.FareBasisCode = "PassengerTransTrip1.FareBasisCode";
                }

                public PassengerTransTrip1()
                {
                }
            }

            public class PassengerTransTrip2
            {
                public readonly static string TripLegFare;

                public readonly static string TripLegTaxes;

                public readonly static string TripLegFee;

                public readonly static string EndorsementsRestrictions;

                static PassengerTransTrip2()
                {
                    SLM.SubmissionOrder.PassengerTransTrip2.TripLegFare = "PassengerTransTrip2.TripLegFare";
                    SLM.SubmissionOrder.PassengerTransTrip2.TripLegTaxes = "PassengerTransTrip2.TripLegTaxes";
                    SLM.SubmissionOrder.PassengerTransTrip2.TripLegFee = "PassengerTransTrip2.TripLegFee";
                    SLM.SubmissionOrder.PassengerTransTrip2.EndorsementsRestrictions = "PassengerTransTrip2.EndorsementsRestrictions";
                }

                public PassengerTransTrip2()
                {
                }
            }

            public class ProductRecordAmericanExpress001
            {
                public readonly static string DepartmentName;

                public readonly static string ItemDescription1;

                public readonly static string ItemQuantity1;

                public readonly static string ItemAmount1;

                public readonly static string ItemDescription2;

                public readonly static string ItemQuantity2;

                public readonly static string ItemAmount2;

                static ProductRecordAmericanExpress001()
                {
                    SLM.SubmissionOrder.ProductRecordAmericanExpress001.DepartmentName = "ProductRecordAmericanExpress001.DepartmentName";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress001.ItemDescription1 = "ProductRecordAmericanExpress001.ItemDescription1";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress001.ItemQuantity1 = "ProductRecordAmericanExpress001.ItemQuantity1";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress001.ItemAmount1 = "ProductRecordAmericanExpress001.ItemAmount1";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress001.ItemDescription2 = "ProductRecordAmericanExpress001.ItemDescription2";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress001.ItemQuantity2 = "ProductRecordAmericanExpress001.ItemQuantity2";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress001.ItemAmount2 = "ProductRecordAmericanExpress001.ItemAmount2";
                }

                public ProductRecordAmericanExpress001()
                {
                }
            }

            public class ProductRecordAmericanExpress002
            {
                public readonly static string ItemDescription3;

                public readonly static string ItemQuantity3;

                public readonly static string ItemAmount3;

                public readonly static string ItemDescription4;

                public readonly static string ItemQuantity4;

                public readonly static string ItemAmount4;

                static ProductRecordAmericanExpress002()
                {
                    SLM.SubmissionOrder.ProductRecordAmericanExpress002.ItemDescription3 = "ProductRecordAmericanExpress002.ItemDescription3";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress002.ItemQuantity3 = "ProductRecordAmericanExpress002.ItemQuantity3";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress002.ItemAmount3 = "ProductRecordAmericanExpress002.ItemAmount3";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress002.ItemDescription4 = "ProductRecordAmericanExpress002.ItemDescription4";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress002.ItemQuantity4 = "ProductRecordAmericanExpress002.ItemQuantity4";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress002.ItemAmount4 = "ProductRecordAmericanExpress002.ItemAmount4";
                }

                public ProductRecordAmericanExpress002()
                {
                }
            }

            public class ProductRecordAmericanExpress003
            {
                public readonly static string ItemDescription5;

                public readonly static string ItemQuantity5;

                public readonly static string ItemAmount5;

                public readonly static string ItemDescription6;

                public readonly static string ItemQuantity6;

                public readonly static string ItemAmount6;

                static ProductRecordAmericanExpress003()
                {
                    SLM.SubmissionOrder.ProductRecordAmericanExpress003.ItemDescription5 = "ProductRecordAmericanExpress003.ItemDescription5";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress003.ItemQuantity5 = "ProductRecordAmericanExpress003.ItemQuantity5";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress003.ItemAmount5 = "ProductRecordAmericanExpress003.ItemAmount5";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress003.ItemDescription6 = "ProductRecordAmericanExpress003.ItemDescription6";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress003.ItemQuantity6 = "ProductRecordAmericanExpress003.ItemQuantity6";
                    SLM.SubmissionOrder.ProductRecordAmericanExpress003.ItemAmount6 = "ProductRecordAmericanExpress003.ItemAmount6";
                }

                public ProductRecordAmericanExpress003()
                {
                }
            }

            public class ProductRecordAmericanExpressCPSLevel2
            {
                public readonly static string Description;

                public readonly static string Quantity;

                public readonly static string UnitCost;

                public readonly static string Description1;

                public readonly static string Quantity1;

                public readonly static string UnitCost1;

                static ProductRecordAmericanExpressCPSLevel2()
                {
                    SLM.SubmissionOrder.ProductRecordAmericanExpressCPSLevel2.Description = "ProductRecordAmericanExpressCPSLevel2.Description";
                    SLM.SubmissionOrder.ProductRecordAmericanExpressCPSLevel2.Quantity = "ProductRecordAmericanExpressCPSLevel2.Quantity";
                    SLM.SubmissionOrder.ProductRecordAmericanExpressCPSLevel2.UnitCost = "ProductRecordAmericanExpressCPSLevel2.UnitCost";
                    SLM.SubmissionOrder.ProductRecordAmericanExpressCPSLevel2.Description1 = "ProductRecordAmericanExpressCPSLevel2.Description1";
                    SLM.SubmissionOrder.ProductRecordAmericanExpressCPSLevel2.Quantity1 = "ProductRecordAmericanExpressCPSLevel2.Quantity1";
                    SLM.SubmissionOrder.ProductRecordAmericanExpressCPSLevel2.UnitCost1 = "ProductRecordAmericanExpressCPSLevel2.UnitCost1";
                }

                public ProductRecordAmericanExpressCPSLevel2()
                {
                }
            }

            public class ProductRecordBalanceInquiry001
            {
                public readonly static string CurrentBalance;

                public readonly static string CurrentBalanceSign;

                public readonly static string CurrentBalanceCurrencyCode;

                static ProductRecordBalanceInquiry001()
                {
                    SLM.SubmissionOrder.ProductRecordBalanceInquiry001.CurrentBalance = "ProductRecordBalanceInquiry001.CurrentBalance";
                    SLM.SubmissionOrder.ProductRecordBalanceInquiry001.CurrentBalanceSign = "ProductRecordBalanceInquiry001.CurrentBalanceSign";
                    SLM.SubmissionOrder.ProductRecordBalanceInquiry001.CurrentBalanceCurrencyCode = "ProductRecordBalanceInquiry001.CurrentBalanceCurrencyCode";
                }

                public ProductRecordBalanceInquiry001()
                {
                }
            }

            public class ProductRecordCardIssuingCountryStatus001
            {
                public readonly static string CountryStatus;

                public readonly static string CountryCode;

                static ProductRecordCardIssuingCountryStatus001()
                {
                    SLM.SubmissionOrder.ProductRecordCardIssuingCountryStatus001.CountryStatus = "ProductRecordCardIssuingCountryStatus001.CountryStatus";
                    SLM.SubmissionOrder.ProductRecordCardIssuingCountryStatus001.CountryCode = "ProductRecordCardIssuingCountryStatus001.CountryCode";
                }

                public ProductRecordCardIssuingCountryStatus001()
                {
                }
            }

            public class ProductRecordCardTypeIndicator
            {
                public readonly static string ProductRecordVersionNumber;

                static ProductRecordCardTypeIndicator()
                {
                    SLM.SubmissionOrder.ProductRecordCardTypeIndicator.ProductRecordVersionNumber = "ProductRecordCardTypeIndicator.ProductRecordVersionNumber";
                }

                public ProductRecordCardTypeIndicator()
                {
                }
            }

            public class ProductRecordCashBack001
            {
                public readonly static string CashBackAmountRequested;

                public readonly static string CashBackAmountApproved;

                static ProductRecordCashBack001()
                {
                    SLM.SubmissionOrder.ProductRecordCashBack001.CashBackAmountRequested = "ProductRecordCashBack001.CashBackAmountRequested";
                    SLM.SubmissionOrder.ProductRecordCashBack001.CashBackAmountApproved = "ProductRecordCashBack001.CashBackAmountApproved";
                }

                public ProductRecordCashBack001()
                {
                }
            }

            public class ProductRecordChaseCardServicesLineItemData
            {
                public readonly static string Description;

                public readonly static string SKUNumber;

                public readonly static string Quantity;

                public readonly static string Price;

                public readonly static string ProductType;

                static ProductRecordChaseCardServicesLineItemData()
                {
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesLineItemData.Description = "ProductRecordChaseCardServicesLineItemData.Description";
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesLineItemData.SKUNumber = "ProductRecordChaseCardServicesLineItemData.SKUNumber";
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesLineItemData.Quantity = "ProductRecordChaseCardServicesLineItemData.Quantity";
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesLineItemData.Price = "ProductRecordChaseCardServicesLineItemData.Price";
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesLineItemData.ProductType = "ProductRecordChaseCardServicesLineItemData.ProductType";
                }

                public ProductRecordChaseCardServicesLineItemData()
                {
                }
            }

            public class ProductRecordChaseCardServicesPayment001
            {
                public readonly static string PaymentType;

                public readonly static string ReasonCode;

                public readonly static string RDFIID;

                public readonly static string CheckAccountNumber;

                public readonly static string CheckSerialNumber;

                public readonly static string PaymentAdjustmentType;

                static ProductRecordChaseCardServicesPayment001()
                {
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesPayment001.PaymentType = "ProductRecordChaseCardServicesPayment001.PaymentType";
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesPayment001.ReasonCode = "ProductRecordChaseCardServicesPayment001.ReasonCode";
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesPayment001.RDFIID = "ProductRecordChaseCardServicesPayment001.RDFIID";
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesPayment001.CheckAccountNumber = "ProductRecordChaseCardServicesPayment001.CheckAccountNumber";
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesPayment001.CheckSerialNumber = "ProductRecordChaseCardServicesPayment001.CheckSerialNumber";
                    SLM.SubmissionOrder.ProductRecordChaseCardServicesPayment001.PaymentAdjustmentType = "ProductRecordChaseCardServicesPayment001.PaymentAdjustmentType";
                }

                public ProductRecordChaseCardServicesPayment001()
                {
                }
            }

            public class ProductRecordChipEMV
            {
                public readonly static string ChipEMVDataString;

                static ProductRecordChipEMV()
                {
                    SLM.SubmissionOrder.ProductRecordChipEMV.ChipEMVDataString = "ProductRecordChipEMV.ChipEMVDataString";
                }

                public ProductRecordChipEMV()
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
                    SLM.SubmissionOrder.ProductRecordCrossCurrency.RecordType = "ProductRecordCrossCurrency.RecordType";
                    SLM.SubmissionOrder.ProductRecordCrossCurrency.OptOutIndicator = "ProductRecordCrossCurrency.OptOutIndicator";
                    SLM.SubmissionOrder.ProductRecordCrossCurrency.RateHandlingIndicator = "ProductRecordCrossCurrency.RateHandlingIndicator";
                    SLM.SubmissionOrder.ProductRecordCrossCurrency.RateIdentifier = "ProductRecordCrossCurrency.RateIdentifier";
                    SLM.SubmissionOrder.ProductRecordCrossCurrency.ExchangeRate = "ProductRecordCrossCurrency.ExchangeRate";
                    SLM.SubmissionOrder.ProductRecordCrossCurrency.PresentmentCurrency = "ProductRecordCrossCurrency.PresentmentCurrency";
                    SLM.SubmissionOrder.ProductRecordCrossCurrency.SettlementCurrency = "ProductRecordCrossCurrency.SettlementCurrency";
                    SLM.SubmissionOrder.ProductRecordCrossCurrency.DefaultRateIndicator = "ProductRecordCrossCurrency.DefaultRateIndicator";
                    SLM.SubmissionOrder.ProductRecordCrossCurrency.RateStatus = "ProductRecordCrossCurrency.RateStatus";
                }

                public ProductRecordCrossCurrency()
                {
                }
            }

            public class ProductRecordDebit001
            {
                public readonly static string TraceNumber;

                public readonly static string BillerReference;

                public readonly static string AccountType;

                public readonly static string VoucherNumber;

                public readonly static string FoodConsumerNumber;

                public readonly static string DebitRoutingData;

                static ProductRecordDebit001()
                {
                    SLM.SubmissionOrder.ProductRecordDebit001.TraceNumber = "ProductRecordDebit001.TraceNumber";
                    SLM.SubmissionOrder.ProductRecordDebit001.BillerReference = "ProductRecordDebit001.BillerReference";
                    SLM.SubmissionOrder.ProductRecordDebit001.AccountType = "ProductRecordDebit001.AccountType";
                    SLM.SubmissionOrder.ProductRecordDebit001.VoucherNumber = "ProductRecordDebit001.VoucherNumber";
                    SLM.SubmissionOrder.ProductRecordDebit001.FoodConsumerNumber = "ProductRecordDebit001.FoodConsumerNumber";
                    SLM.SubmissionOrder.ProductRecordDebit001.DebitRoutingData = "ProductRecordDebit001.DebitRoutingData";
                }

                public ProductRecordDebit001()
                {
                }
            }

            public class ProductRecordDigitalPAN001
            {
                public readonly static string TokenAssuranceLevel;

                public readonly static string AccountStatus;

                public readonly static string TokenRequestorID;

                static ProductRecordDigitalPAN001()
                {
                    SLM.SubmissionOrder.ProductRecordDigitalPAN001.TokenAssuranceLevel = "ProductRecordDigitalPAN001.TokenAssuranceLevel";
                    SLM.SubmissionOrder.ProductRecordDigitalPAN001.AccountStatus = "ProductRecordDigitalPAN001.AccountStatus";
                    SLM.SubmissionOrder.ProductRecordDigitalPAN001.TokenRequestorID = "ProductRecordDigitalPAN001.TokenRequestorID";
                }

                public ProductRecordDigitalPAN001()
                {
                }
            }

            public class ProductRecordECPAdvancedVerification1
            {
                public readonly static string RecordType;

                public readonly static string FirstName;

                public readonly static string LastName;

                static ProductRecordECPAdvancedVerification1()
                {
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification1.RecordType = "ProductRecordECPAdvancedVerification1.RecordType";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification1.FirstName = "ProductRecordECPAdvancedVerification1.FirstName";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification1.LastName = "ProductRecordECPAdvancedVerification1.LastName";
                }

                public ProductRecordECPAdvancedVerification1()
                {
                }
            }

            public class ProductRecordECPAdvancedVerification2
            {
                public readonly static string RecordType;

                public readonly static string BusinessName;

                static ProductRecordECPAdvancedVerification2()
                {
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification2.RecordType = "ProductRecordECPAdvancedVerification2.RecordType";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification2.BusinessName = "ProductRecordECPAdvancedVerification2.BusinessName";
                }

                public ProductRecordECPAdvancedVerification2()
                {
                }
            }

            public class ProductRecordECPAdvancedVerification3
            {
                public readonly static string RecordType;

                public readonly static string AddressLine1;

                public readonly static string AddressLine2;

                static ProductRecordECPAdvancedVerification3()
                {
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification3.RecordType = "ProductRecordECPAdvancedVerification3.RecordType";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification3.AddressLine1 = "ProductRecordECPAdvancedVerification3.AddressLine1";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification3.AddressLine2 = "ProductRecordECPAdvancedVerification3.AddressLine2";
                }

                public ProductRecordECPAdvancedVerification3()
                {
                }
            }

            public class ProductRecordECPAdvancedVerification4
            {
                public readonly static string RecordType;

                public readonly static string City;

                public readonly static string State;

                public readonly static string ZIP;

                public readonly static string PhoneType;

                public readonly static string Phone;

                public readonly static string MiddleNameOrInitial;

                static ProductRecordECPAdvancedVerification4()
                {
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification4.RecordType = "ProductRecordECPAdvancedVerification4.RecordType";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification4.City = "ProductRecordECPAdvancedVerification4.City";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification4.State = "ProductRecordECPAdvancedVerification4.State";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification4.ZIP = "ProductRecordECPAdvancedVerification4.ZIP";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification4.PhoneType = "ProductRecordECPAdvancedVerification4.PhoneType";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification4.Phone = "ProductRecordECPAdvancedVerification4.Phone";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification4.MiddleNameOrInitial = "ProductRecordECPAdvancedVerification4.MiddleNameOrInitial";
                }

                public ProductRecordECPAdvancedVerification4()
                {
                }
            }

            public class ProductRecordECPAdvancedVerification5
            {
                public readonly static string RecordType;

                public readonly static string TaxIdentificationNumber;

                public readonly static string DOB;

                public readonly static string IDType;

                public readonly static string IDNumber;

                public readonly static string IDState;

                static ProductRecordECPAdvancedVerification5()
                {
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification5.RecordType = "ProductRecordECPAdvancedVerification5.RecordType";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification5.TaxIdentificationNumber = "ProductRecordECPAdvancedVerification5.TaxIdentificationNumber";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification5.DOB = "ProductRecordECPAdvancedVerification5.DOB";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification5.IDType = "ProductRecordECPAdvancedVerification5.IDType";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification5.IDNumber = "ProductRecordECPAdvancedVerification5.IDNumber";
                    SLM.SubmissionOrder.ProductRecordECPAdvancedVerification5.IDState = "ProductRecordECPAdvancedVerification5.IDState";
                }

                public ProductRecordECPAdvancedVerification5()
                {
                }
            }

            public class ProductRecordEMV001
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

                static ProductRecordEMV001()
                {
                    SLM.SubmissionOrder.ProductRecordEMV001.CardSequenceNumber = "ProductRecordEMV001.CardSequenceNumber";
                    SLM.SubmissionOrder.ProductRecordEMV001.Cryptogram = "ProductRecordEMV001.Cryptogram";
                    SLM.SubmissionOrder.ProductRecordEMV001.CryptogramAmount = "ProductRecordEMV001.CryptogramAmount";
                    SLM.SubmissionOrder.ProductRecordEMV001.OtherAmount = "ProductRecordEMV001.OtherAmount";
                    SLM.SubmissionOrder.ProductRecordEMV001.CryptogramInformationData = "ProductRecordEMV001.CryptogramInformationData";
                    SLM.SubmissionOrder.ProductRecordEMV001.ApplicationTransactionCounter = "ProductRecordEMV001.ApplicationTransactionCounter";
                    SLM.SubmissionOrder.ProductRecordEMV001.TransactionType = "ProductRecordEMV001.TransactionType";
                    SLM.SubmissionOrder.ProductRecordEMV001.TerminalCountryCode = "ProductRecordEMV001.TerminalCountryCode";
                    SLM.SubmissionOrder.ProductRecordEMV001.TerminalCurrencyCode = "ProductRecordEMV001.TerminalCurrencyCode";
                    SLM.SubmissionOrder.ProductRecordEMV001.TerminalTransactionDate = "ProductRecordEMV001.TerminalTransactionDate";
                    SLM.SubmissionOrder.ProductRecordEMV001.TerminalTransactionTime = "ProductRecordEMV001.TerminalTransactionTime";
                    SLM.SubmissionOrder.ProductRecordEMV001.TerminalCapabilityProfile = "ProductRecordEMV001.TerminalCapabilityProfile";
                    SLM.SubmissionOrder.ProductRecordEMV001.TerminalType = "ProductRecordEMV001.TerminalType";
                    SLM.SubmissionOrder.ProductRecordEMV001.TerminalVerificationResults = "ProductRecordEMV001.TerminalVerificationResults";
                    SLM.SubmissionOrder.ProductRecordEMV001.UnpredictableNumber = "ProductRecordEMV001.UnpredictableNumber";
                    SLM.SubmissionOrder.ProductRecordEMV001.FormFactorID = "ProductRecordEMV001.FormFactorID";
                    SLM.SubmissionOrder.ProductRecordEMV001.IssuerScriptID = "ProductRecordEMV001.IssuerScriptID";
                    SLM.SubmissionOrder.ProductRecordEMV001.ApplicationInterchangeProfile = "ProductRecordEMV001.ApplicationInterchangeProfile";
                }

                public ProductRecordEMV001()
                {
                }
            }

            public class ProductRecordEMV002
            {
                public readonly static string AuthorizationResponseCode;

                public readonly static string IssuerApplicationData;

                public readonly static string IssuerScriptResults;

                static ProductRecordEMV002()
                {
                    SLM.SubmissionOrder.ProductRecordEMV002.AuthorizationResponseCode = "ProductRecordEMV002.AuthorizationResponseCode";
                    SLM.SubmissionOrder.ProductRecordEMV002.IssuerApplicationData = "ProductRecordEMV002.IssuerApplicationData";
                    SLM.SubmissionOrder.ProductRecordEMV002.IssuerScriptResults = "ProductRecordEMV002.IssuerScriptResults";
                }

                public ProductRecordEMV002()
                {
                }
            }

            public class ProductRecordEMV003
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

                static ProductRecordEMV003()
                {
                    SLM.SubmissionOrder.ProductRecordEMV003.InterfaceDeviceSerialNumber = "ProductRecordEMV003.InterfaceDeviceSerialNumber";
                    SLM.SubmissionOrder.ProductRecordEMV003.CardAuthenticationResultsCode = "ProductRecordEMV003.CardAuthenticationResultsCode";
                    SLM.SubmissionOrder.ProductRecordEMV003.CVVICVVCamResultsCode = "ProductRecordEMV003.CVVICVVCamResultsCode";
                    SLM.SubmissionOrder.ProductRecordEMV003.CVMResults = "ProductRecordEMV003.CVMResults";
                    SLM.SubmissionOrder.ProductRecordEMV003.IssuerScriptCommandData = "ProductRecordEMV003.IssuerScriptCommandData";
                    SLM.SubmissionOrder.ProductRecordEMV003.ApplicationIdentifier = "ProductRecordEMV003.ApplicationIdentifier";
                    SLM.SubmissionOrder.ProductRecordEMV003.TerminalTransactionQualifier = "ProductRecordEMV003.TerminalTransactionQualifier";
                    SLM.SubmissionOrder.ProductRecordEMV003.IssuerScriptTemplate1Indicator = "ProductRecordEMV003.IssuerScriptTemplate1Indicator";
                    SLM.SubmissionOrder.ProductRecordEMV003.IssuerScriptTemplate2Indicator = "ProductRecordEMV003.IssuerScriptTemplate2Indicator";
                    SLM.SubmissionOrder.ProductRecordEMV003.ChipConditionCode = "ProductRecordEMV003.ChipConditionCode";
                }

                public ProductRecordEMV003()
                {
                }
            }

            public class ProductRecordEMV004
            {
                public readonly static string TransactionSequenceCounter;

                public readonly static string CustomerExclusiveData;

                public readonly static string IssuerAuthenticationData;

                static ProductRecordEMV004()
                {
                    SLM.SubmissionOrder.ProductRecordEMV004.TransactionSequenceCounter = "ProductRecordEMV004.TransactionSequenceCounter";
                    SLM.SubmissionOrder.ProductRecordEMV004.CustomerExclusiveData = "ProductRecordEMV004.CustomerExclusiveData";
                    SLM.SubmissionOrder.ProductRecordEMV004.IssuerAuthenticationData = "ProductRecordEMV004.IssuerAuthenticationData";
                }

                public ProductRecordEMV004()
                {
                }
            }

            public class ProductRecordEntertainmentTicketing001
            {
                public readonly static string EventName;

                public readonly static string EventDate;

                public readonly static string IndividualTicketPrice;

                public readonly static string Quantity;

                static ProductRecordEntertainmentTicketing001()
                {
                    SLM.SubmissionOrder.ProductRecordEntertainmentTicketing001.EventName = "ProductRecordEntertainmentTicketing001.EventName";
                    SLM.SubmissionOrder.ProductRecordEntertainmentTicketing001.EventDate = "ProductRecordEntertainmentTicketing001.EventDate";
                    SLM.SubmissionOrder.ProductRecordEntertainmentTicketing001.IndividualTicketPrice = "ProductRecordEntertainmentTicketing001.IndividualTicketPrice";
                    SLM.SubmissionOrder.ProductRecordEntertainmentTicketing001.Quantity = "ProductRecordEntertainmentTicketing001.Quantity";
                }

                public ProductRecordEntertainmentTicketing001()
                {
                }
            }

            public class ProductRecordEntertainmentTicketing002
            {
                public readonly static string Location;

                public readonly static string Region;

                public readonly static string Country;

                static ProductRecordEntertainmentTicketing002()
                {
                    SLM.SubmissionOrder.ProductRecordEntertainmentTicketing002.Location = "ProductRecordEntertainmentTicketing002.Location";
                    SLM.SubmissionOrder.ProductRecordEntertainmentTicketing002.Region = "ProductRecordEntertainmentTicketing002.Region";
                    SLM.SubmissionOrder.ProductRecordEntertainmentTicketing002.Country = "ProductRecordEntertainmentTicketing002.Country";
                }

                public ProductRecordEntertainmentTicketing002()
                {
                }
            }

            public class ProductRecordFleet001
            {
                public readonly static string FleetProdTypeCode;

                public readonly static string Odometer;

                public readonly static string IDNumber;

                public readonly static string VehicleNumber;

                public readonly static string DriverIDNumber;

                static ProductRecordFleet001()
                {
                    SLM.SubmissionOrder.ProductRecordFleet001.FleetProdTypeCode = "ProductRecordFleet001.FleetProdTypeCode";
                    SLM.SubmissionOrder.ProductRecordFleet001.Odometer = "ProductRecordFleet001.Odometer";
                    SLM.SubmissionOrder.ProductRecordFleet001.IDNumber = "ProductRecordFleet001.IDNumber";
                    SLM.SubmissionOrder.ProductRecordFleet001.VehicleNumber = "ProductRecordFleet001.VehicleNumber";
                    SLM.SubmissionOrder.ProductRecordFleet001.DriverIDNumber = "ProductRecordFleet001.DriverIDNumber";
                }

                public ProductRecordFleet001()
                {
                }
            }

            public class ProductRecordFraud001
            {
                public readonly static string CardSecurityValue;

                public readonly static string CardSecurityPresence;

                static ProductRecordFraud001()
                {
                    SLM.SubmissionOrder.ProductRecordFraud001.CardSecurityValue = "ProductRecordFraud001.CardSecurityValue";
                    SLM.SubmissionOrder.ProductRecordFraud001.CardSecurityPresence = "ProductRecordFraud001.CardSecurityPresence";
                }

                public ProductRecordFraud001()
                {
                }
            }

            public class ProductRecordFraudScoringInputData1
            {
                public readonly static string ReturnRulesTriggered;

                public readonly static string SafetechMerchantID;

                public readonly static string KaptchaSessionID;

                public readonly static string WebSiteShortName;

                static ProductRecordFraudScoringInputData1()
                {
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData1.ReturnRulesTriggered = "ProductRecordFraudScoringInputData1.ReturnRulesTriggered";
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData1.SafetechMerchantID = "ProductRecordFraudScoringInputData1.SafetechMerchantID";
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData1.KaptchaSessionID = "ProductRecordFraudScoringInputData1.KaptchaSessionID";
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData1.WebSiteShortName = "ProductRecordFraudScoringInputData1.WebSiteShortName";
                }

                public ProductRecordFraudScoringInputData1()
                {
                }
            }

            public class ProductRecordFraudScoringInputData2
            {
                public readonly static string CashValueOfFencibleItems;

                public readonly static string CustomerDateOfBirth;

                public readonly static string CustomerGender;

                public readonly static string CustomerDriversLicenseNumber;

                public readonly static string CustomerID;

                public readonly static string CustomerIDCreationTime;

                static ProductRecordFraudScoringInputData2()
                {
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData2.CashValueOfFencibleItems = "ProductRecordFraudScoringInputData2.CashValueOfFencibleItems";
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData2.CustomerDateOfBirth = "ProductRecordFraudScoringInputData2.CustomerDateOfBirth";
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData2.CustomerGender = "ProductRecordFraudScoringInputData2.CustomerGender";
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData2.CustomerDriversLicenseNumber = "ProductRecordFraudScoringInputData2.CustomerDriversLicenseNumber";
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData2.CustomerID = "ProductRecordFraudScoringInputData2.CustomerID";
                    SLM.SubmissionOrder.ProductRecordFraudScoringInputData2.CustomerIDCreationTime = "ProductRecordFraudScoringInputData2.CustomerIDCreationTime";
                }

                public ProductRecordFraudScoringInputData2()
                {
                }
            }

            public class ProductRecordFuel001
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

                public readonly static string Reserved;

                static ProductRecordFuel001()
                {
                    SLM.SubmissionOrder.ProductRecordFuel001.PurchaseTime = "ProductRecordFuel001.PurchaseTime";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelPurchaseType = "ProductRecordFuel001.FuelPurchaseType";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelServiceTypeCode = "ProductRecordFuel001.FuelServiceTypeCode";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelProductCode = "ProductRecordFuel001.FuelProductCode";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelUnitPrice = "ProductRecordFuel001.FuelUnitPrice";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelUnitOfMeasure = "ProductRecordFuel001.FuelUnitOfMeasure";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelQuantity = "ProductRecordFuel001.FuelQuantity";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelSaleAmount = "ProductRecordFuel001.FuelSaleAmount";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelTaxAmount = "ProductRecordFuel001.FuelTaxAmount";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelDiscountAmount = "ProductRecordFuel001.FuelDiscountAmount";
                    SLM.SubmissionOrder.ProductRecordFuel001.MiscellaneousFuelTax = "ProductRecordFuel001.MiscellaneousFuelTax";
                    SLM.SubmissionOrder.ProductRecordFuel001.MiscellaneousFuelTaxExemptionIndicator = "ProductRecordFuel001.MiscellaneousFuelTaxExemptionIndicator";
                    SLM.SubmissionOrder.ProductRecordFuel001.FuelVATTaxRate = "ProductRecordFuel001.FuelVATTaxRate";
                    SLM.SubmissionOrder.ProductRecordFuel001.Reserved = "ProductRecordFuel001.Reserved";
                }

                public ProductRecordFuel001()
                {
                }
            }

            public class ProductRecordFuel002
            {
                public readonly static string NonFuelGrossAmount;

                public readonly static string NonFuelTaxAmount;

                public readonly static string NonFuelDiscountAmount;

                public readonly static string NonFuelMiscellaneousTaxAmount;

                public readonly static string MiscellaneousNonFuelTaxIndicator;

                public readonly static string ProductCode;

                public readonly static string Reserved;

                static ProductRecordFuel002()
                {
                    SLM.SubmissionOrder.ProductRecordFuel002.NonFuelGrossAmount = "ProductRecordFuel002.NonFuelGrossAmount";
                    SLM.SubmissionOrder.ProductRecordFuel002.NonFuelTaxAmount = "ProductRecordFuel002.NonFuelTaxAmount";
                    SLM.SubmissionOrder.ProductRecordFuel002.NonFuelDiscountAmount = "ProductRecordFuel002.NonFuelDiscountAmount";
                    SLM.SubmissionOrder.ProductRecordFuel002.NonFuelMiscellaneousTaxAmount = "ProductRecordFuel002.NonFuelMiscellaneousTaxAmount";
                    SLM.SubmissionOrder.ProductRecordFuel002.MiscellaneousNonFuelTaxIndicator = "ProductRecordFuel002.MiscellaneousNonFuelTaxIndicator";
                    SLM.SubmissionOrder.ProductRecordFuel002.ProductCode = "ProductRecordFuel002.ProductCode";
                    SLM.SubmissionOrder.ProductRecordFuel002.Reserved = "ProductRecordFuel002.Reserved";
                }

                public ProductRecordFuel002()
                {
                }
            }

            public class ProductRecordGiftCard001
            {
                public readonly static string CurrentBalance;

                public readonly static string PreviousBalance;

                static ProductRecordGiftCard001()
                {
                    SLM.SubmissionOrder.ProductRecordGiftCard001.CurrentBalance = "ProductRecordGiftCard001.CurrentBalance";
                    SLM.SubmissionOrder.ProductRecordGiftCard001.PreviousBalance = "ProductRecordGiftCard001.PreviousBalance";
                }

                public ProductRecordGiftCard001()
                {
                }
            }

            public class ProductRecordHealthCareIIAS001
            {
                public readonly static string QHPAmount;

                public readonly static string RXAmount;

                public readonly static string VisionAmount;

                public readonly static string ClinicOtherAmount;

                public readonly static string DentalAmount;

                public readonly static string IIASFlag;

                static ProductRecordHealthCareIIAS001()
                {
                    SLM.SubmissionOrder.ProductRecordHealthCareIIAS001.QHPAmount = "ProductRecordHealthCareIIAS001.QHPAmount";
                    SLM.SubmissionOrder.ProductRecordHealthCareIIAS001.RXAmount = "ProductRecordHealthCareIIAS001.RXAmount";
                    SLM.SubmissionOrder.ProductRecordHealthCareIIAS001.VisionAmount = "ProductRecordHealthCareIIAS001.VisionAmount";
                    SLM.SubmissionOrder.ProductRecordHealthCareIIAS001.ClinicOtherAmount = "ProductRecordHealthCareIIAS001.ClinicOtherAmount";
                    SLM.SubmissionOrder.ProductRecordHealthCareIIAS001.DentalAmount = "ProductRecordHealthCareIIAS001.DentalAmount";
                    SLM.SubmissionOrder.ProductRecordHealthCareIIAS001.IIASFlag = "ProductRecordHealthCareIIAS001.IIASFlag";
                }

                public ProductRecordHealthCareIIAS001()
                {
                }
            }

            public class ProductRecordInsurance001
            {
                public readonly static string PolicyNumber;

                public readonly static string CoverageStartDate;

                public readonly static string CoverageEndDate;

                public readonly static string PremiumFrequency;

                public readonly static string PolicyType;

                static ProductRecordInsurance001()
                {
                    SLM.SubmissionOrder.ProductRecordInsurance001.PolicyNumber = "ProductRecordInsurance001.PolicyNumber";
                    SLM.SubmissionOrder.ProductRecordInsurance001.CoverageStartDate = "ProductRecordInsurance001.CoverageStartDate";
                    SLM.SubmissionOrder.ProductRecordInsurance001.CoverageEndDate = "ProductRecordInsurance001.CoverageEndDate";
                    SLM.SubmissionOrder.ProductRecordInsurance001.PremiumFrequency = "ProductRecordInsurance001.PremiumFrequency";
                    SLM.SubmissionOrder.ProductRecordInsurance001.PolicyType = "ProductRecordInsurance001.PolicyType";
                }

                public ProductRecordInsurance001()
                {
                }
            }

            public class ProductRecordInsurance002
            {
                public readonly static string NameOfInsured;

                public readonly static string AdditionalPolicyNumber;

                static ProductRecordInsurance002()
                {
                    SLM.SubmissionOrder.ProductRecordInsurance002.NameOfInsured = "ProductRecordInsurance002.NameOfInsured";
                    SLM.SubmissionOrder.ProductRecordInsurance002.AdditionalPolicyNumber = "ProductRecordInsurance002.AdditionalPolicyNumber";
                }

                public ProductRecordInsurance002()
                {
                }
            }

            public class ProductRecordLevel2InternationalLineItemData
            {
                public readonly static string CommodityCode;

                public readonly static string VATID;

                public readonly static string PSTRegistrationTaxNumber;

                public readonly static string LocalTaxDetailTaxAmount2;

                public readonly static string NationalTaxRateDetailTaxRate1;

                public readonly static string Reserved;

                static ProductRecordLevel2InternationalLineItemData()
                {
                    SLM.SubmissionOrder.ProductRecordLevel2InternationalLineItemData.CommodityCode = "ProductRecordLevel2InternationalLineItemData.CommodityCode";
                    SLM.SubmissionOrder.ProductRecordLevel2InternationalLineItemData.VATID = "ProductRecordLevel2InternationalLineItemData.VATID";
                    SLM.SubmissionOrder.ProductRecordLevel2InternationalLineItemData.PSTRegistrationTaxNumber = "ProductRecordLevel2InternationalLineItemData.PSTRegistrationTaxNumber";
                    SLM.SubmissionOrder.ProductRecordLevel2InternationalLineItemData.LocalTaxDetailTaxAmount2 = "ProductRecordLevel2InternationalLineItemData.LocalTaxDetailTaxAmount2";
                    SLM.SubmissionOrder.ProductRecordLevel2InternationalLineItemData.NationalTaxRateDetailTaxRate1 = "ProductRecordLevel2InternationalLineItemData.NationalTaxRateDetailTaxRate1";
                    SLM.SubmissionOrder.ProductRecordLevel2InternationalLineItemData.Reserved = "ProductRecordLevel2InternationalLineItemData.Reserved";
                }

                public ProductRecordLevel2InternationalLineItemData()
                {
                }
            }

            public class ProductRecordLevel2LineItemData
            {
                public readonly static string CustomerReferenceNumber;

                public readonly static string SalesTaxAmount;

                public readonly static string RequestorName;

                public readonly static string DestinationPostalCode;

                public readonly static string ExemptIndicator;

                public readonly static string CustomerVATRegistrationNumber;

                public readonly static string NationalTax;

                public readonly static string LocalTaxRate;

                public readonly static string Reserved;

                static ProductRecordLevel2LineItemData()
                {
                    SLM.SubmissionOrder.ProductRecordLevel2LineItemData.CustomerReferenceNumber = "ProductRecordLevel2LineItemData.CustomerReferenceNumber";
                    SLM.SubmissionOrder.ProductRecordLevel2LineItemData.SalesTaxAmount = "ProductRecordLevel2LineItemData.SalesTaxAmount";
                    SLM.SubmissionOrder.ProductRecordLevel2LineItemData.RequestorName = "ProductRecordLevel2LineItemData.RequestorName";
                    SLM.SubmissionOrder.ProductRecordLevel2LineItemData.DestinationPostalCode = "ProductRecordLevel2LineItemData.DestinationPostalCode";
                    SLM.SubmissionOrder.ProductRecordLevel2LineItemData.ExemptIndicator = "ProductRecordLevel2LineItemData.ExemptIndicator";
                    SLM.SubmissionOrder.ProductRecordLevel2LineItemData.CustomerVATRegistrationNumber = "ProductRecordLevel2LineItemData.CustomerVATRegistrationNumber";
                    SLM.SubmissionOrder.ProductRecordLevel2LineItemData.NationalTax = "ProductRecordLevel2LineItemData.NationalTax";
                    SLM.SubmissionOrder.ProductRecordLevel2LineItemData.LocalTaxRate = "ProductRecordLevel2LineItemData.LocalTaxRate";
                    SLM.SubmissionOrder.ProductRecordLevel2LineItemData.Reserved = "ProductRecordLevel2LineItemData.Reserved";
                }

                public ProductRecordLevel2LineItemData()
                {
                }
            }

            public class ProductRecordLineItemLevelDataDiscoverRecord001
            {
                public readonly static string TaxAmount;

                public readonly static string ItemCommodityCode;

                public readonly static string ProductCode;

                public readonly static string Description;

                public readonly static string DiscountRate;

                static ProductRecordLineItemLevelDataDiscoverRecord001()
                {
                    SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.TaxAmount = "ProductRecordLineItemLevelDataDiscoverRecord001.TaxAmount";
                    SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.ItemCommodityCode = "ProductRecordLineItemLevelDataDiscoverRecord001.ItemCommodityCode";
                    SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.ProductCode = "ProductRecordLineItemLevelDataDiscoverRecord001.ProductCode";
                    SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.Description = "ProductRecordLineItemLevelDataDiscoverRecord001.Description";
                    SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.DiscountRate = "ProductRecordLineItemLevelDataDiscoverRecord001.DiscountRate";
                }

                public ProductRecordLineItemLevelDataDiscoverRecord001()
                {
                }

                public class ProductRecordLineItemLevelDataDiscoverRecord002
                {
                    public readonly static string UnitOfMeasure;

                    public readonly static string UnitCost;

                    public readonly static string Quantity;

                    public readonly static string TaxRate;

                    public readonly static string LineItemTotal;

                    static ProductRecordLineItemLevelDataDiscoverRecord002()
                    {
                        SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.ProductRecordLineItemLevelDataDiscoverRecord002.UnitOfMeasure = "ProductRecordLineItemLevelDataDiscoverRecord002.UnitOfMeasure";
                        SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.ProductRecordLineItemLevelDataDiscoverRecord002.UnitCost = "ProductRecordLineItemLevelDataDiscoverRecord002.UnitCost";
                        SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.ProductRecordLineItemLevelDataDiscoverRecord002.Quantity = "ProductRecordLineItemLevelDataDiscoverRecord002.Quantity";
                        SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.ProductRecordLineItemLevelDataDiscoverRecord002.TaxRate = "ProductRecordLineItemLevelDataDiscoverRecord002.TaxRate";
                        SLM.SubmissionOrder.ProductRecordLineItemLevelDataDiscoverRecord001.ProductRecordLineItemLevelDataDiscoverRecord002.LineItemTotal = "ProductRecordLineItemLevelDataDiscoverRecord002.LineItemTotal";
                    }

                    public ProductRecordLineItemLevelDataDiscoverRecord002()
                    {
                    }
                }
            }

            public class ProductRecordLodging001
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

                static ProductRecordLodging001()
                {
                    SLM.SubmissionOrder.ProductRecordLodging001.Duration = "ProductRecordLodging001.Duration";
                    SLM.SubmissionOrder.ProductRecordLodging001.NoShowIndicator = "ProductRecordLodging001.NoShowIndicator";
                    SLM.SubmissionOrder.ProductRecordLodging001.ArrivalDate = "ProductRecordLodging001.ArrivalDate";
                    SLM.SubmissionOrder.ProductRecordLodging001.DepartureDate = "ProductRecordLodging001.DepartureDate";
                    SLM.SubmissionOrder.ProductRecordLodging001.ExtraCharges = "ProductRecordLodging001.ExtraCharges";
                    SLM.SubmissionOrder.ProductRecordLodging001.ExtraChargesAmount = "ProductRecordLodging001.ExtraChargesAmount";
                    SLM.SubmissionOrder.ProductRecordLodging001.FolioNumber = "ProductRecordLodging001.FolioNumber";
                    SLM.SubmissionOrder.ProductRecordLodging001.PropertyPhoneNumber = "ProductRecordLodging001.PropertyPhoneNumber";
                    SLM.SubmissionOrder.ProductRecordLodging001.CSRPhoneNumber = "ProductRecordLodging001.CSRPhoneNumber";
                    SLM.SubmissionOrder.ProductRecordLodging001.RoomRate = "ProductRecordLodging001.RoomRate";
                    SLM.SubmissionOrder.ProductRecordLodging001.RoomTax = "ProductRecordLodging001.RoomTax";
                    SLM.SubmissionOrder.ProductRecordLodging001.FireSafetyIndicator = "ProductRecordLodging001.FireSafetyIndicator";
                    SLM.SubmissionOrder.ProductRecordLodging001.AmericanExpressFolioNumber = "ProductRecordLodging001.AmericanExpressFolioNumber";
                    SLM.SubmissionOrder.ProductRecordLodging001.NumberOfNightsAtRoomRate1 = "ProductRecordLodging001.NumberOfNightsAtRoomRate1";
                    SLM.SubmissionOrder.ProductRecordLodging001.AdvancedDepositIndicator = "ProductRecordLodging001.AdvancedDepositIndicator";
                }

                public ProductRecordLodging001()
                {
                }
            }

            public class ProductRecordLodging002
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

                static ProductRecordLodging002()
                {
                    SLM.SubmissionOrder.ProductRecordLodging002.RenterName = "ProductRecordLodging002.RenterName";
                    SLM.SubmissionOrder.ProductRecordLodging002.RoomRate2 = "ProductRecordLodging002.RoomRate2";
                    SLM.SubmissionOrder.ProductRecordLodging002.NumberNightsRoomRate2 = "ProductRecordLodging002.NumberNightsRoomRate2";
                    SLM.SubmissionOrder.ProductRecordLodging002.RoomRate3 = "ProductRecordLodging002.RoomRate3";
                    SLM.SubmissionOrder.ProductRecordLodging002.NumberNightsRoomRate3 = "ProductRecordLodging002.NumberNightsRoomRate3";
                    SLM.SubmissionOrder.ProductRecordLodging002.ProgramCode = "ProductRecordLodging002.ProgramCode";
                    SLM.SubmissionOrder.ProductRecordLodging002.PrepaidExpenseAmount = "ProductRecordLodging002.PrepaidExpenseAmount";
                    SLM.SubmissionOrder.ProductRecordLodging002.CashAdvanceAmount = "ProductRecordLodging002.CashAdvanceAmount";
                    SLM.SubmissionOrder.ProductRecordLodging002.TotalNonRoomChargesAmount = "ProductRecordLodging002.TotalNonRoomChargesAmount";
                }

                public ProductRecordLodging002()
                {
                }
            }

            public class ProductRecordLodging003
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

                static ProductRecordLodging003()
                {
                    SLM.SubmissionOrder.ProductRecordLodging003.PhoneChargesAmount = "ProductRecordLodging003.PhoneChargesAmount";
                    SLM.SubmissionOrder.ProductRecordLodging003.RestaurantChargesAmount = "ProductRecordLodging003.RestaurantChargesAmount";
                    SLM.SubmissionOrder.ProductRecordLodging003.MinibarChargesAmount = "ProductRecordLodging003.MinibarChargesAmount";
                    SLM.SubmissionOrder.ProductRecordLodging003.GiftShopChargesAmount = "ProductRecordLodging003.GiftShopChargesAmount";
                    SLM.SubmissionOrder.ProductRecordLodging003.LaundryChargesAmount = "ProductRecordLodging003.LaundryChargesAmount";
                    SLM.SubmissionOrder.ProductRecordLodging003.ParkingChargesAmount = "ProductRecordLodging003.ParkingChargesAmount";
                    SLM.SubmissionOrder.ProductRecordLodging003.MovieChargesAmount = "ProductRecordLodging003.MovieChargesAmount";
                    SLM.SubmissionOrder.ProductRecordLodging003.BusinessCenterChargesAmount = "ProductRecordLodging003.BusinessCenterChargesAmount";
                    SLM.SubmissionOrder.ProductRecordLodging003.HealthClubChargesAmount = "ProductRecordLodging003.HealthClubChargesAmount";
                }

                public ProductRecordLodging003()
                {
                }
            }

            public class ProductRecordLodging004
            {
                public readonly static string OtherServices;

                static ProductRecordLodging004()
                {
                    SLM.SubmissionOrder.ProductRecordLodging004.OtherServices = "ProductRecordLodging004.OtherServices";
                }

                public ProductRecordLodging004()
                {
                }
            }

            public class ProductRecordMasterCardDigitalWallet
            {
                public readonly static string DigitalWalletIndicator;

                public readonly static string WalletIndicator;

                public readonly static string MasterPassDigitalWalletID;

                static ProductRecordMasterCardDigitalWallet()
                {
                    SLM.SubmissionOrder.ProductRecordMasterCardDigitalWallet.DigitalWalletIndicator = "ProductRecordMasterCardDigitalWallet.DigitalWalletIndicator";
                    SLM.SubmissionOrder.ProductRecordMasterCardDigitalWallet.WalletIndicator = "ProductRecordMasterCardDigitalWallet.WalletIndicator";
                    SLM.SubmissionOrder.ProductRecordMasterCardDigitalWallet.MasterPassDigitalWalletID = "ProductRecordMasterCardDigitalWallet.MasterPassDigitalWalletID";
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
                    SLM.SubmissionOrder.ProductRecordMessageType.MessageType = "ProductRecordMessageType.MessageType";
                    SLM.SubmissionOrder.ProductRecordMessageType.StoredCredentialFlag = "ProductRecordMessageType.StoredCredentialFlag";
                    SLM.SubmissionOrder.ProductRecordMessageType.SubmittedTransactionID = "ProductRecordMessageType.SubmittedTransactionID";
                    SLM.SubmissionOrder.ProductRecordMessageType.ResponseTransactionID = "ProductRecordMessageType.ResponseTransactionID";
                    SLM.SubmissionOrder.ProductRecordMessageType.OriginalTransactionAmount = "ProductRecordMessageType.OriginalTransactionAmount";
                }

                public ProductRecordMessageType()
                {
                }
            }

            public class ProductRecordMobilePOSDeviceInformation
            {
                public readonly static string DeviceType;

                public readonly static string PaymentDevice;

                public readonly static string Reserved;

                static ProductRecordMobilePOSDeviceInformation()
                {
                    SLM.SubmissionOrder.ProductRecordMobilePOSDeviceInformation.DeviceType = "ProductRecordMobilePOSDeviceInformation.DeviceType";
                    SLM.SubmissionOrder.ProductRecordMobilePOSDeviceInformation.PaymentDevice = "ProductRecordMobilePOSDeviceInformation.PaymentDevice";
                    SLM.SubmissionOrder.ProductRecordMobilePOSDeviceInformation.Reserved = "ProductRecordMobilePOSDeviceInformation.Reserved";
                }

                public ProductRecordMobilePOSDeviceInformation()
                {
                }
            }

            public class ProductRecordPartialAuthorizationBalanceInquiry001
            {
                public readonly static string PartialRedemptionIndicatorFlag;

                public readonly static string CurrentBalance;

                public readonly static string RedemptionAmount;

                static ProductRecordPartialAuthorizationBalanceInquiry001()
                {
                    SLM.SubmissionOrder.ProductRecordPartialAuthorizationBalanceInquiry001.PartialRedemptionIndicatorFlag = "ProductRecordPartialAuthorizationBalanceInquiry001.PartialRedemptionIndicatorFlag";
                    SLM.SubmissionOrder.ProductRecordPartialAuthorizationBalanceInquiry001.CurrentBalance = "ProductRecordPartialAuthorizationBalanceInquiry001.CurrentBalance";
                    SLM.SubmissionOrder.ProductRecordPartialAuthorizationBalanceInquiry001.RedemptionAmount = "ProductRecordPartialAuthorizationBalanceInquiry001.RedemptionAmount";
                }

                public ProductRecordPartialAuthorizationBalanceInquiry001()
                {
                }
            }

            public class ProductRecordProcurement3MasterCardOrderLevelRecord
            {
                public readonly static string FreightAmount;

                public readonly static string DutyAmount;

                public readonly static string DestinationPostalCode;

                public readonly static string DestinationCountryCode;

                public readonly static string ShipFromPostalCode;

                public readonly static string AlternateTaxID;

                public readonly static string AlternateTaxAmt;

                public readonly static string VATTaxAmount;

                static ProductRecordProcurement3MasterCardOrderLevelRecord()
                {
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardOrderLevelRecord.FreightAmount = "ProductRecordProcurement3MasterCardOrderLevelRecord.FreightAmount";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardOrderLevelRecord.DutyAmount = "ProductRecordProcurement3MasterCardOrderLevelRecord.DutyAmount";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardOrderLevelRecord.DestinationPostalCode = "ProductRecordProcurement3MasterCardOrderLevelRecord.DestinationPostalCode";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardOrderLevelRecord.DestinationCountryCode = "ProductRecordProcurement3MasterCardOrderLevelRecord.DestinationCountryCode";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardOrderLevelRecord.ShipFromPostalCode = "ProductRecordProcurement3MasterCardOrderLevelRecord.ShipFromPostalCode";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardOrderLevelRecord.AlternateTaxID = "ProductRecordProcurement3MasterCardOrderLevelRecord.AlternateTaxID";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardOrderLevelRecord.AlternateTaxAmt = "ProductRecordProcurement3MasterCardOrderLevelRecord.AlternateTaxAmt";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardOrderLevelRecord.VATTaxAmount = "ProductRecordProcurement3MasterCardOrderLevelRecord.VATTaxAmount";
                }

                public ProductRecordProcurement3MasterCardOrderLevelRecord()
                {
                }
            }

            public class ProductRecordProcurement3MasterCardRecord1LineItemData
            {
                public readonly static string Description;

                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string UnitOfMeasure;

                public readonly static string TaxAmount1;

                public readonly static string TaxRate1;

                static ProductRecordProcurement3MasterCardRecord1LineItemData()
                {
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.Description = "ProductRecordProcurement3MasterCardRecord1LineItemData.Description";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductCode = "ProductRecordProcurement3MasterCardRecord1LineItemData.ProductCode";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.Quantity = "ProductRecordProcurement3MasterCardRecord1LineItemData.Quantity";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.UnitOfMeasure = "ProductRecordProcurement3MasterCardRecord1LineItemData.UnitOfMeasure";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.TaxAmount1 = "ProductRecordProcurement3MasterCardRecord1LineItemData.TaxAmount1";
                    SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.TaxRate1 = "ProductRecordProcurement3MasterCardRecord1LineItemData.TaxRate1";
                }

                public ProductRecordProcurement3MasterCardRecord1LineItemData()
                {
                }

                public class ProductRecordProcurement3MasterCardRecord2LineItemData
                {
                    public readonly static string LineItemTotal;

                    public readonly static string DiscountAmt;

                    public readonly static string GrossNetIndicator;

                    public readonly static string TaxTypeApplied1;

                    public readonly static string DiscountIndicator;

                    public readonly static string ItemCommodityCode;

                    public readonly static string UnitCost;

                    public readonly static string TaxAmount2;

                    public readonly static string TaxRate2;

                    public readonly static string TaxTypeApplied2;

                    static ProductRecordProcurement3MasterCardRecord2LineItemData()
                    {
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.LineItemTotal = "ProductRecordProcurement3MasterCardRecord2LineItemData.LineItemTotal";
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.DiscountAmt = "ProductRecordProcurement3MasterCardRecord2LineItemData.DiscountAmt";
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.GrossNetIndicator = "ProductRecordProcurement3MasterCardRecord2LineItemData.GrossNetIndicator";
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.TaxTypeApplied1 = "ProductRecordProcurement3MasterCardRecord2LineItemData.TaxTypeApplied1";
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.DiscountIndicator = "ProductRecordProcurement3MasterCardRecord2LineItemData.DiscountIndicator";
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.ItemCommodityCode = "ProductRecordProcurement3MasterCardRecord2LineItemData.ItemCommodityCode";
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.UnitCost = "ProductRecordProcurement3MasterCardRecord2LineItemData.UnitCost";
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.TaxAmount2 = "ProductRecordProcurement3MasterCardRecord2LineItemData.TaxAmount2";
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.TaxRate2 = "ProductRecordProcurement3MasterCardRecord2LineItemData.TaxRate2";
                        SLM.SubmissionOrder.ProductRecordProcurement3MasterCardRecord1LineItemData.ProductRecordProcurement3MasterCardRecord2LineItemData.TaxTypeApplied2 = "ProductRecordProcurement3MasterCardRecord2LineItemData.TaxTypeApplied2";
                    }

                    public ProductRecordProcurement3MasterCardRecord2LineItemData()
                    {
                    }
                }
            }

            public class ProductRecordProcurement3VisaOrderLevelRecord
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

                public readonly static string Reserved;

                static ProductRecordProcurement3VisaOrderLevelRecord()
                {
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.FreightAmount = "ProductRecordProcurement3VisaOrderLevelRecord.FreightAmount";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.DutyAmount = "ProductRecordProcurement3VisaOrderLevelRecord.DutyAmount";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.DestinationPostalCode = "ProductRecordProcurement3VisaOrderLevelRecord.DestinationPostalCode";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.DestinationCountryCode = "ProductRecordProcurement3VisaOrderLevelRecord.DestinationCountryCode";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.ShipFromPostalCode = "ProductRecordProcurement3VisaOrderLevelRecord.ShipFromPostalCode";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.DiscountApplied = "ProductRecordProcurement3VisaOrderLevelRecord.DiscountApplied";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.TaxAmount = "ProductRecordProcurement3VisaOrderLevelRecord.TaxAmount";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.TaxRate = "ProductRecordProcurement3VisaOrderLevelRecord.TaxRate";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.ShippingTaxRate = "ProductRecordProcurement3VisaOrderLevelRecord.ShippingTaxRate";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.InvoiceDiscountTreatment = "ProductRecordProcurement3VisaOrderLevelRecord.InvoiceDiscountTreatment";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.TaxTreatment = "ProductRecordProcurement3VisaOrderLevelRecord.TaxTreatment";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.DiscountAmountSign = "ProductRecordProcurement3VisaOrderLevelRecord.DiscountAmountSign";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.FreightAmountSign = "ProductRecordProcurement3VisaOrderLevelRecord.FreightAmountSign";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.DutyAmountSign = "ProductRecordProcurement3VisaOrderLevelRecord.DutyAmountSign";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.VATTaxAmountSign = "ProductRecordProcurement3VisaOrderLevelRecord.VATTaxAmountSign";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.UniqueVATInvoiceReferenceNumber = "ProductRecordProcurement3VisaOrderLevelRecord.UniqueVATInvoiceReferenceNumber";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.VATTaxAmount = "ProductRecordProcurement3VisaOrderLevelRecord.VATTaxAmount";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaOrderLevelRecord.Reserved = "ProductRecordProcurement3VisaOrderLevelRecord.Reserved";
                }

                public ProductRecordProcurement3VisaOrderLevelRecord()
                {
                }
            }

            public class ProductRecordProcurement3VisaRecord1LineItemData
            {
                public readonly static string Description;

                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string UnitOfMeasure;

                public readonly static string TaxAmount;

                public readonly static string TaxRate;

                static ProductRecordProcurement3VisaRecord1LineItemData()
                {
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.Description = "ProductRecordProcurement3VisaRecord1LineItemData.Description";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.ProductCode = "ProductRecordProcurement3VisaRecord1LineItemData.ProductCode";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.Quantity = "ProductRecordProcurement3VisaRecord1LineItemData.Quantity";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.UnitOfMeasure = "ProductRecordProcurement3VisaRecord1LineItemData.UnitOfMeasure";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.TaxAmount = "ProductRecordProcurement3VisaRecord1LineItemData.TaxAmount";
                    SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.TaxRate = "ProductRecordProcurement3VisaRecord1LineItemData.TaxRate";
                }

                public ProductRecordProcurement3VisaRecord1LineItemData()
                {
                }

                public class ProductRecordProcurement3VisaRecord2LineItemData
                {
                    public readonly static string LineItemTotal;

                    public readonly static string DiscountAmount;

                    public readonly static string ItemCommodityCode;

                    public readonly static string UnitCost;

                    public readonly static string LineItemLevelDiscountTreatmentCode;

                    public readonly static string LineItemDetailIndicator;

                    static ProductRecordProcurement3VisaRecord2LineItemData()
                    {
                        SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.ProductRecordProcurement3VisaRecord2LineItemData.LineItemTotal = "ProductRecordProcurement3VisaRecord2LineItemData.LineItemTotal";
                        SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.ProductRecordProcurement3VisaRecord2LineItemData.DiscountAmount = "ProductRecordProcurement3VisaRecord2LineItemData.DiscountAmount";
                        SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.ProductRecordProcurement3VisaRecord2LineItemData.ItemCommodityCode = "ProductRecordProcurement3VisaRecord2LineItemData.ItemCommodityCode";
                        SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.ProductRecordProcurement3VisaRecord2LineItemData.UnitCost = "ProductRecordProcurement3VisaRecord2LineItemData.UnitCost";
                        SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.ProductRecordProcurement3VisaRecord2LineItemData.LineItemLevelDiscountTreatmentCode = "ProductRecordProcurement3VisaRecord2LineItemData.LineItemLevelDiscountTreatmentCode";
                        SLM.SubmissionOrder.ProductRecordProcurement3VisaRecord1LineItemData.ProductRecordProcurement3VisaRecord2LineItemData.LineItemDetailIndicator = "ProductRecordProcurement3VisaRecord2LineItemData.LineItemDetailIndicator";
                    }

                    public ProductRecordProcurement3VisaRecord2LineItemData()
                    {
                    }
                }
            }

            public class ProductRecordProcurementL2ElectronicPayAccount003
            {
                public readonly static string CustomIdentifier;

                public readonly static string InvoiceNumber;

                static ProductRecordProcurementL2ElectronicPayAccount003()
                {
                    SLM.SubmissionOrder.ProductRecordProcurementL2ElectronicPayAccount003.CustomIdentifier = "ProductRecordProcurementL2ElectronicPayAccount003.CustomIdentifier";
                    SLM.SubmissionOrder.ProductRecordProcurementL2ElectronicPayAccount003.InvoiceNumber = "ProductRecordProcurementL2ElectronicPayAccount003.InvoiceNumber";
                }

                public ProductRecordProcurementL2ElectronicPayAccount003()
                {
                }
            }

            public class ProductRecordRecipientData
            {
                public readonly static string DateofBirthofPrimaryRecipient;

                public readonly static string MaskedAccountNumber;

                public readonly static string PartialPostalCode;

                public readonly static string LastNameofPrimaryRecipient;

                static ProductRecordRecipientData()
                {
                    SLM.SubmissionOrder.ProductRecordRecipientData.DateofBirthofPrimaryRecipient = "ProductRecordRecipientData.DateofBirthofPrimaryRecipient";
                    SLM.SubmissionOrder.ProductRecordRecipientData.MaskedAccountNumber = "ProductRecordRecipientData.MaskedAccountNumber";
                    SLM.SubmissionOrder.ProductRecordRecipientData.PartialPostalCode = "ProductRecordRecipientData.PartialPostalCode";
                    SLM.SubmissionOrder.ProductRecordRecipientData.LastNameofPrimaryRecipient = "ProductRecordRecipientData.LastNameofPrimaryRecipient";
                }

                public ProductRecordRecipientData()
                {
                }
            }

            public class ProductRecordResponseInformation
            {
                public readonly static string EnhancedVerificationFlag;

                static ProductRecordResponseInformation()
                {
                    SLM.SubmissionOrder.ProductRecordResponseInformation.EnhancedVerificationFlag = "ProductRecordResponseInformation.EnhancedVerificationFlag";
                }

                public ProductRecordResponseInformation()
                {
                }
            }

            public class ProductRecordRetail001
            {
                public readonly static string TerminalID;

                public readonly static string BatchID;

                public readonly static string BatchDate;

                public readonly static string CommunicationMethod;

                public readonly static string ReferenceNumber;

                public readonly static string CustomerData;

                public readonly static string CaptureType;

                public readonly static string GuaranteedID;

                static ProductRecordRetail001()
                {
                    SLM.SubmissionOrder.ProductRecordRetail001.TerminalID = "ProductRecordRetail001.TerminalID";
                    SLM.SubmissionOrder.ProductRecordRetail001.BatchID = "ProductRecordRetail001.BatchID";
                    SLM.SubmissionOrder.ProductRecordRetail001.BatchDate = "ProductRecordRetail001.BatchDate";
                    SLM.SubmissionOrder.ProductRecordRetail001.CommunicationMethod = "ProductRecordRetail001.CommunicationMethod";
                    SLM.SubmissionOrder.ProductRecordRetail001.ReferenceNumber = "ProductRecordRetail001.ReferenceNumber";
                    SLM.SubmissionOrder.ProductRecordRetail001.CustomerData = "ProductRecordRetail001.CustomerData";
                    SLM.SubmissionOrder.ProductRecordRetail001.CaptureType = "ProductRecordRetail001.CaptureType";
                    SLM.SubmissionOrder.ProductRecordRetail001.GuaranteedID = "ProductRecordRetail001.GuaranteedID";
                }

                public ProductRecordRetail001()
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
                    SLM.SubmissionOrder.ProductRecordSafetechPageEncryption.SubscriberID = "ProductRecordSafetechPageEncryption.SubscriberID";
                    SLM.SubmissionOrder.ProductRecordSafetechPageEncryption.FormatID = "ProductRecordSafetechPageEncryption.FormatID";
                    SLM.SubmissionOrder.ProductRecordSafetechPageEncryption.IntegrityCheck = "ProductRecordSafetechPageEncryption.IntegrityCheck";
                    SLM.SubmissionOrder.ProductRecordSafetechPageEncryption.KeyID = "ProductRecordSafetechPageEncryption.KeyID";
                    SLM.SubmissionOrder.ProductRecordSafetechPageEncryption.PhaseID = "ProductRecordSafetechPageEncryption.PhaseID";
                }

                public ProductRecordSafetechPageEncryption()
                {
                }
            }

            public class ProductRecordSearsLicensedBusiness001
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

                static ProductRecordSearsLicensedBusiness001()
                {
                    SLM.SubmissionOrder.ProductRecordSearsLicensedBusiness001.ProductCode1 = "ProductRecordSearsLicensedBusiness001.ProductCode1";
                    SLM.SubmissionOrder.ProductRecordSearsLicensedBusiness001.ProductAmount1 = "ProductRecordSearsLicensedBusiness001.ProductAmount1";
                    SLM.SubmissionOrder.ProductRecordSearsLicensedBusiness001.ProductCode2 = "ProductRecordSearsLicensedBusiness001.ProductCode2";
                    SLM.SubmissionOrder.ProductRecordSearsLicensedBusiness001.ProductAmount2 = "ProductRecordSearsLicensedBusiness001.ProductAmount2";
                    SLM.SubmissionOrder.ProductRecordSearsLicensedBusiness001.ProductCode3 = "ProductRecordSearsLicensedBusiness001.ProductCode3";
                    SLM.SubmissionOrder.ProductRecordSearsLicensedBusiness001.ProductAmount3 = "ProductRecordSearsLicensedBusiness001.ProductAmount3";
                    SLM.SubmissionOrder.ProductRecordSearsLicensedBusiness001.ProductCode4 = "ProductRecordSearsLicensedBusiness001.ProductCode4";
                    SLM.SubmissionOrder.ProductRecordSearsLicensedBusiness001.ProductAmount4 = "ProductRecordSearsLicensedBusiness001.ProductAmount4";
                    SLM.SubmissionOrder.ProductRecordSearsLicensedBusiness001.TaxAmount = "ProductRecordSearsLicensedBusiness001.TaxAmount";
                }

                public ProductRecordSearsLicensedBusiness001()
                {
                }
            }

            public class ProductRecordShopWithPoints
            {
                public readonly static string RewardsTransactionAmount;

                public readonly static string RewardsCurrency;

                public readonly static string ConversionRate;

                public readonly static string ProductCode;

                static ProductRecordShopWithPoints()
                {
                    SLM.SubmissionOrder.ProductRecordShopWithPoints.RewardsTransactionAmount = "ProductRecordShopWithPoints.RewardsTransactionAmount";
                    SLM.SubmissionOrder.ProductRecordShopWithPoints.RewardsCurrency = "ProductRecordShopWithPoints.RewardsCurrency";
                    SLM.SubmissionOrder.ProductRecordShopWithPoints.ConversionRate = "ProductRecordShopWithPoints.ConversionRate";
                    SLM.SubmissionOrder.ProductRecordShopWithPoints.ProductCode = "ProductRecordShopWithPoints.ProductCode";
                }

                public ProductRecordShopWithPoints()
                {
                }
            }

            public class ProductRecordSoftMerchantInformation001
            {
                public readonly static string DBA;

                public readonly static string MerchantID;

                public readonly static string MerchantContactInfo;

                public readonly static string EmailAddress;

                static ProductRecordSoftMerchantInformation001()
                {
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation001.DBA = "ProductRecordSoftMerchantInformation001.DBA";
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation001.MerchantID = "ProductRecordSoftMerchantInformation001.MerchantID";
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation001.MerchantContactInfo = "ProductRecordSoftMerchantInformation001.MerchantContactInfo";
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation001.EmailAddress = "ProductRecordSoftMerchantInformation001.EmailAddress";
                }

                public ProductRecordSoftMerchantInformation001()
                {
                }
            }

            public class ProductRecordSoftMerchantInformation002
            {
                public readonly static string Street;

                public readonly static string City;

                public readonly static string Region;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                public readonly static string MerchantSellerID;

                public readonly static string MCC;

                static ProductRecordSoftMerchantInformation002()
                {
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation002.Street = "ProductRecordSoftMerchantInformation002.Street";
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation002.City = "ProductRecordSoftMerchantInformation002.City";
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation002.Region = "ProductRecordSoftMerchantInformation002.Region";
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation002.PostalCode = "ProductRecordSoftMerchantInformation002.PostalCode";
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation002.CountryCode = "ProductRecordSoftMerchantInformation002.CountryCode";
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation002.MerchantSellerID = "ProductRecordSoftMerchantInformation002.MerchantSellerID";
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation002.MCC = "ProductRecordSoftMerchantInformation002.MCC";
                }

                public ProductRecordSoftMerchantInformation002()
                {
                }
            }

            public class ProductRecordSoftMerchantInformation003
            {
                public readonly static string EmailAddress;

                static ProductRecordSoftMerchantInformation003()
                {
                    SLM.SubmissionOrder.ProductRecordSoftMerchantInformation003.EmailAddress = "ProductRecordSoftMerchantInformation003.EmailAddress";
                }

                public ProductRecordSoftMerchantInformation003()
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
                    SLM.SubmissionOrder.ProductRecordSplitShipment.SplitShipmentSequencNumber = "ProductRecordSplitShipment.SplitShipmentSequencNumber";
                    SLM.SubmissionOrder.ProductRecordSplitShipment.SplitShipmentCount = "ProductRecordSplitShipment.SplitShipmentCount";
                    SLM.SubmissionOrder.ProductRecordSplitShipment.PartialReversalAmount = "ProductRecordSplitShipment.PartialReversalAmount";
                    SLM.SubmissionOrder.ProductRecordSplitShipment.ResponseReasonCode = "ProductRecordSplitShipment.ResponseReasonCode";
                }

                public ProductRecordSplitShipment()
                {
                }
            }

            public class ProductRecordUserDefinedAndShoppingCart
            {
                public readonly static string DataString;

                static ProductRecordUserDefinedAndShoppingCart()
                {
                    SLM.SubmissionOrder.ProductRecordUserDefinedAndShoppingCart.DataString = "ProductRecordUserDefinedAndShoppingCart.DataString";
                }

                public ProductRecordUserDefinedAndShoppingCart()
                {
                }
            }

            public class ProductRecordVariousText001
            {
                public readonly static string TextMessage;

                static ProductRecordVariousText001()
                {
                    SLM.SubmissionOrder.ProductRecordVariousText001.TextMessage = "ProductRecordVariousText001.TextMessage";
                }

                public ProductRecordVariousText001()
                {
                }
            }

            public class ProductRecordVehicleRental1
            {
                public readonly static string RenterName;

                public readonly static string RentalAgreementNumber;

                public readonly static string RentalDate;

                public readonly static string RentalTime;

                public readonly static string RentalCity;

                public readonly static string RentalStateRegion;

                public readonly static string RentalCountry;

                public readonly static string DaysRented;

                static ProductRecordVehicleRental1()
                {
                    SLM.SubmissionOrder.ProductRecordVehicleRental1.RenterName = "ProductRecordVehicleRental1.RenterName";
                    SLM.SubmissionOrder.ProductRecordVehicleRental1.RentalAgreementNumber = "ProductRecordVehicleRental1.RentalAgreementNumber";
                    SLM.SubmissionOrder.ProductRecordVehicleRental1.RentalDate = "ProductRecordVehicleRental1.RentalDate";
                    SLM.SubmissionOrder.ProductRecordVehicleRental1.RentalTime = "ProductRecordVehicleRental1.RentalTime";
                    SLM.SubmissionOrder.ProductRecordVehicleRental1.RentalCity = "ProductRecordVehicleRental1.RentalCity";
                    SLM.SubmissionOrder.ProductRecordVehicleRental1.RentalStateRegion = "ProductRecordVehicleRental1.RentalStateRegion";
                    SLM.SubmissionOrder.ProductRecordVehicleRental1.RentalCountry = "ProductRecordVehicleRental1.RentalCountry";
                    SLM.SubmissionOrder.ProductRecordVehicleRental1.DaysRented = "ProductRecordVehicleRental1.DaysRented";
                }

                public ProductRecordVehicleRental1()
                {
                }
            }

            public class ProductRecordVehicleRental2
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

                static ProductRecordVehicleRental2()
                {
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.ReturnCity = "ProductRecordVehicleRental2.ReturnCity";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.ReturnStateRegion = "ProductRecordVehicleRental2.ReturnStateRegion";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.ReturnCountry = "ProductRecordVehicleRental2.ReturnCountry";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.ReturnLocationID = "ProductRecordVehicleRental2.ReturnLocationID";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.ReturnDate = "ProductRecordVehicleRental2.ReturnDate";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.ReturnTime = "ProductRecordVehicleRental2.ReturnTime";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.TollFreeNumber = "ProductRecordVehicleRental2.TollFreeNumber";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.RentalType = "ProductRecordVehicleRental2.RentalType";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.RentalRateperRentalType = "ProductRecordVehicleRental2.RentalRateperRentalType";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.RentalClassID = "ProductRecordVehicleRental2.RentalClassID";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.NoShowIndicator = "ProductRecordVehicleRental2.NoShowIndicator";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.TaxExemptIndicator = "ProductRecordVehicleRental2.TaxExemptIndicator";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.OneWayCharge = "ProductRecordVehicleRental2.OneWayCharge";
                    SLM.SubmissionOrder.ProductRecordVehicleRental2.ExtraCharges = "ProductRecordVehicleRental2.ExtraCharges";
                }

                public ProductRecordVehicleRental2()
                {
                }
            }

            public class ProductRecordVisaDirectPayment1
            {
                public readonly static string BusinessApplicationIdentifier;

                public readonly static string ServiceFee;

                public readonly static string ForeignExchangeMarkupFee;

                public readonly static string SenderReferenceNumber;

                public readonly static string SourceOfFunds;

                public readonly static string RecipientName;

                static ProductRecordVisaDirectPayment1()
                {
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment1.BusinessApplicationIdentifier = "ProductRecordVisaDirectPayment1.BusinessApplicationIdentifier";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment1.ServiceFee = "ProductRecordVisaDirectPayment1.ServiceFee";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment1.ForeignExchangeMarkupFee = "ProductRecordVisaDirectPayment1.ForeignExchangeMarkupFee";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment1.SenderReferenceNumber = "ProductRecordVisaDirectPayment1.SenderReferenceNumber";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment1.SourceOfFunds = "ProductRecordVisaDirectPayment1.SourceOfFunds";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment1.RecipientName = "ProductRecordVisaDirectPayment1.RecipientName";
                }

                public ProductRecordVisaDirectPayment1()
                {
                }
            }

            public class ProductRecordVisaDirectPayment2
            {
                public readonly static string SenderName;

                public readonly static string SenderAddress;

                public readonly static string SenderCity;

                public readonly static string SenderState;

                public readonly static string SenderZipCode;

                public readonly static string SenderCountryCode;

                static ProductRecordVisaDirectPayment2()
                {
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment2.SenderName = "ProductRecordVisaDirectPayment2.SenderName";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment2.SenderAddress = "ProductRecordVisaDirectPayment2.SenderAddress";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment2.SenderCity = "ProductRecordVisaDirectPayment2.SenderCity";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment2.SenderState = "ProductRecordVisaDirectPayment2.SenderState";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment2.SenderZipCode = "ProductRecordVisaDirectPayment2.SenderZipCode";
                    SLM.SubmissionOrder.ProductRecordVisaDirectPayment2.SenderCountryCode = "ProductRecordVisaDirectPayment2.SenderCountryCode";
                }

                public ProductRecordVisaDirectPayment2()
                {
                }
            }

            public class ProductRecordVoyagerLineItemLevelData
            {
                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string LineItemTotal;

                public readonly static string TaxAmount;

                public readonly static string DiscountAmount;

                static ProductRecordVoyagerLineItemLevelData()
                {
                    SLM.SubmissionOrder.ProductRecordVoyagerLineItemLevelData.ProductCode = "ProductRecordVoyagerLineItemLevelData.ProductCode";
                    SLM.SubmissionOrder.ProductRecordVoyagerLineItemLevelData.Quantity = "ProductRecordVoyagerLineItemLevelData.Quantity";
                    SLM.SubmissionOrder.ProductRecordVoyagerLineItemLevelData.LineItemTotal = "ProductRecordVoyagerLineItemLevelData.LineItemTotal";
                    SLM.SubmissionOrder.ProductRecordVoyagerLineItemLevelData.TaxAmount = "ProductRecordVoyagerLineItemLevelData.TaxAmount";
                    SLM.SubmissionOrder.ProductRecordVoyagerLineItemLevelData.DiscountAmount = "ProductRecordVoyagerLineItemLevelData.DiscountAmount";
                }

                public ProductRecordVoyagerLineItemLevelData()
                {
                }
            }

            public class ProductRecordWrightExpressLineItemLevelData
            {
                public readonly static string ProductCode;

                public readonly static string Quantity;

                public readonly static string LineItemTotal;

                public readonly static string UnitPrice;

                static ProductRecordWrightExpressLineItemLevelData()
                {
                    SLM.SubmissionOrder.ProductRecordWrightExpressLineItemLevelData.ProductCode = "ProductRecordWrightExpressLineItemLevelData.ProductCode";
                    SLM.SubmissionOrder.ProductRecordWrightExpressLineItemLevelData.Quantity = "ProductRecordWrightExpressLineItemLevelData.Quantity";
                    SLM.SubmissionOrder.ProductRecordWrightExpressLineItemLevelData.LineItemTotal = "ProductRecordWrightExpressLineItemLevelData.LineItemTotal";
                    SLM.SubmissionOrder.ProductRecordWrightExpressLineItemLevelData.UnitPrice = "ProductRecordWrightExpressLineItemLevelData.UnitPrice";
                }

                public ProductRecordWrightExpressLineItemLevelData()
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
                    SLM.SubmissionOrder.RealTimeAccountUpdaterType.UpdateResponse = "RealTimeAccountUpdaterType.UpdateResponse";
                    SLM.SubmissionOrder.RealTimeAccountUpdaterType.BypassAccountUpdateRequest = "RealTimeAccountUpdaterType.BypassAccountUpdateRequest";
                    SLM.SubmissionOrder.RealTimeAccountUpdaterType.NewAccountNumber = "RealTimeAccountUpdaterType.NewAccountNumber";
                    SLM.SubmissionOrder.RealTimeAccountUpdaterType.NewExpirationDate = "RealTimeAccountUpdaterType.NewExpirationDate";
                    SLM.SubmissionOrder.RealTimeAccountUpdaterType.NewMethodOfPayment = "RealTimeAccountUpdaterType.NewMethodOfPayment";
                }

                public RealTimeAccountUpdaterType()
                {
                }
            }
        }

        public class SubmissionResponse
        {
            public readonly static string PID;

            public readonly static string PIDPassword;

            public readonly static string SID;

            public readonly static string SIDPassword;

            static SubmissionResponse()
            {
                SLM.SubmissionResponse.PID = "PID";
                SLM.SubmissionResponse.PIDPassword = "PIDPassword";
                SLM.SubmissionResponse.SID = "SID";
                SLM.SubmissionResponse.SIDPassword = "SIDPassword";
            }

            public SubmissionResponse()
            {
            }
        }

        public class SubmissionStatus
        {
            public readonly static string PID;

            public readonly static string PIDPassword;

            public readonly static string SID;

            public readonly static string SIDPassword;

            static SubmissionStatus()
            {
                SLM.SubmissionStatus.PID = "PID";
                SLM.SubmissionStatus.PIDPassword = "PIDPassword";
                SLM.SubmissionStatus.SID = "SID";
                SLM.SubmissionStatus.SIDPassword = "SIDPassword";
            }

            public SubmissionStatus()
            {
            }
        }

        public class SubmissionTotal
        {
            public readonly static string FileRecordCount;

            public readonly static string FileOrderCount;

            public readonly static string FileAmountTotal;

            public readonly static string FileAmountSales;

            public readonly static string FileAmountRefunds;

            static SubmissionTotal()
            {
                SLM.SubmissionTotal.FileRecordCount = "FileRecordCount";
                SLM.SubmissionTotal.FileOrderCount = "FileOrderCount";
                SLM.SubmissionTotal.FileAmountTotal = "FileAmountTotal";
                SLM.SubmissionTotal.FileAmountSales = "FileAmountSales";
                SLM.SubmissionTotal.FileAmountRefunds = "FileAmountRefunds";
            }

            public SubmissionTotal()
            {
            }
        }

        public class SubmissionTrailer
        {
            public readonly static string PID;

            public readonly static string PIDPassword;

            public readonly static string SID;

            public readonly static string SIDPassword;

            static SubmissionTrailer()
            {
                SLM.SubmissionTrailer.PID = "PID";
                SLM.SubmissionTrailer.PIDPassword = "PIDPassword";
                SLM.SubmissionTrailer.SID = "SID";
                SLM.SubmissionTrailer.SIDPassword = "SIDPassword";
            }

            public SubmissionTrailer()
            {
            }
        }
    }
}