#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK
{
    public class PNS
    {
        public PNS()
        {
        }

        public class Bit11
        {
            public readonly static string SystemTraceAuditNumber;

            static Bit11()
            {
                PNS.Bit11.SystemTraceAuditNumber = "Bit11.SystemTraceAuditNumber";
            }

            public Bit11()
            {
            }
        }

        public class Bit12
        {
            public readonly static string TimeLocalTrans;

            static Bit12()
            {
                PNS.Bit12.TimeLocalTrans = "Bit12.TimeLocalTrans";
            }

            public Bit12()
            {
            }
        }

        public class Bit13
        {
            public readonly static string DateLocalTrans;

            static Bit13()
            {
                PNS.Bit13.DateLocalTrans = "Bit13.DateLocalTrans";
            }

            public Bit13()
            {
            }
        }

        public class Bit14
        {
            public readonly static string DateExpire;

            static Bit14()
            {
                PNS.Bit14.DateExpire = "Bit14.DateExpire";
            }

            public Bit14()
            {
            }
        }

        public class Bit18
        {
            public readonly static string MerchantCategoryCode;

            public readonly static string MerchantType;

            static Bit18()
            {
                PNS.Bit18.MerchantCategoryCode = "Bit18.MerchantCategoryCode";
                PNS.Bit18.MerchantType = "Bit18.MerchantType";
            }

            public Bit18()
            {
            }
        }

        public class Bit2
        {
            public readonly static string PrimaryAccountNumber;

            static Bit2()
            {
                PNS.Bit2.PrimaryAccountNumber = "Bit2.PrimaryAccountNumber";
            }

            public Bit2()
            {
            }
        }

        public class Bit22
        {
            public readonly static string POSEntryMode;

            static Bit22()
            {
                PNS.Bit22.POSEntryMode = "Bit22.POSEntryMode";
            }

            public Bit22()
            {
            }
        }

        public class Bit23
        {
            public readonly static string EMVContactlessPANSeqNumber;

            static Bit23()
            {
                PNS.Bit23.EMVContactlessPANSeqNumber = "Bit23.EMVContactlessPANSeqNumber";
            }

            public Bit23()
            {
            }
        }

        public class Bit25
        {
            public readonly static string POSConditionCode;

            static Bit25()
            {
                PNS.Bit25.POSConditionCode = "Bit25.POSConditionCode";
            }

            public Bit25()
            {
            }
        }

        public class Bit3
        {
            public readonly static string TransactionType;

            public readonly static string AccountTypeFrom;

            public readonly static string AccountTypeTo;

            static Bit3()
            {
                PNS.Bit3.TransactionType = "Bit3.TransactionType";
                PNS.Bit3.AccountTypeFrom = "Bit3.AccountTypeFrom";
                PNS.Bit3.AccountTypeTo = "Bit3.AccountTypeTo";
            }

            public Bit3()
            {
            }
        }

        public class Bit35
        {
            public readonly static string Track2Data;

            static Bit35()
            {
                PNS.Bit35.Track2Data = "Bit35.Track2Data";
            }

            public Bit35()
            {
            }
        }

        public class Bit37
        {
            public readonly static string RetrievalReferenceNumber;

            static Bit37()
            {
                PNS.Bit37.RetrievalReferenceNumber = "Bit37.RetrievalReferenceNumber";
            }

            public Bit37()
            {
            }
        }

        public class Bit38
        {
            public readonly static string AuthorizationIDResponse;

            static Bit38()
            {
                PNS.Bit38.AuthorizationIDResponse = "Bit38.AuthorizationIDResponse";
            }

            public Bit38()
            {
            }
        }

        public class Bit4
        {
            public readonly static string TransactionAmount;

            static Bit4()
            {
                PNS.Bit4.TransactionAmount = "Bit4.TransactionAmount";
            }

            public Bit4()
            {
            }
        }

        public class Bit41
        {
            public readonly static string CardAcquirerTerminalId;

            static Bit41()
            {
                PNS.Bit41.CardAcquirerTerminalId = "Bit41.CardAcquirerTerminalId";
            }

            public Bit41()
            {
            }
        }

        public class Bit42
        {
            public readonly static string CardAcquirerId;

            static Bit42()
            {
                PNS.Bit42.CardAcquirerId = "Bit42.CardAcquirerId";
            }

            public Bit42()
            {
            }
        }

        public class Bit45
        {
            public readonly static string Track1Data;

            static Bit45()
            {
                PNS.Bit45.Track1Data = "Bit45.Track1Data";
            }

            public Bit45()
            {
            }
        }

        public class Bit48
        {
            public Bit48()
            {
            }

            public class A1
            {
                public readonly static string CardholderZipCode;

                public readonly static string CardholderAddress;

                static A1()
                {
                    PNS.Bit48.A1.CardholderZipCode = "Bit48.A1.CardholderZipCode";
                    PNS.Bit48.A1.CardholderAddress = "Bit48.A1.CardholderAddress";
                }

                public A1()
                {
                }
            }

            public class A3
            {
                public readonly static string TransactionDataTypeIndicator;

                static A3()
                {
                    PNS.Bit48.A3.TransactionDataTypeIndicator = "Bit48.A3.TransactionDataTypeIndicator";
                }

                public A3()
                {
                }
            }

            public class A4
            {
                public readonly static string AccountType;

                static A4()
                {
                    PNS.Bit48.A4.AccountType = "Bit48.A4.AccountType";
                }

                public A4()
                {
                }
            }

            public class A5
            {
                public readonly static string TransactionIdentifier;

                static A5()
                {
                    PNS.Bit48.A5.TransactionIdentifier = "Bit48.A5.TransactionIdentifier";
                }

                public A5()
                {
                }
            }

            public class A6
            {
                public readonly static string POSDataCodeResponse;

                static A6()
                {
                    PNS.Bit48.A6.POSDataCodeResponse = "Bit48.A6.POSDataCodeResponse";
                }

                public A6()
                {
                }
            }

            public class A7
            {
                public readonly static string EMVContactlessAID;

                static A7()
                {
                    PNS.Bit48.A7.EMVContactlessAID = "Bit48.A7.EMVContactlessAID";
                }

                public A7()
                {
                }
            }

            public class AD
            {
                public readonly static string AEVVBlockA;

                public readonly static string XIDBlockB;

                static AD()
                {
                    PNS.Bit48.AD.AEVVBlockA = "Bit48.AD.AEVVBlockA";
                    PNS.Bit48.AD.XIDBlockB = "Bit48.AD.XIDBlockB";
                }

                public AD()
                {
                }
            }

            public class BA
            {
                public readonly static string BusinessApplicationIdentifier;

                static BA()
                {
                    PNS.Bit48.BA.BusinessApplicationIdentifier = "Bit48.BA.BusinessApplicationIdentifier";
                }

                public BA()
                {
                }
            }

            public class BT
            {
                public readonly static string InquiryType;

                public readonly static string CardTypeRequested;

                static BT()
                {
                    PNS.Bit48.BT.InquiryType = "Bit48.BT.InquiryType";
                    PNS.Bit48.BT.CardTypeRequested = "Bit48.BT.CardTypeRequested";
                }

                public BT()
                {
                }
            }

            public class C1
            {
                public readonly static string RequestType;

                public readonly static string CardholderVerificationData;

                static C1()
                {
                    PNS.Bit48.C1.RequestType = "Bit48.C1.RequestType";
                    PNS.Bit48.C1.CardholderVerificationData = "Bit48.C1.CardholderVerificationData";
                }

                public C1()
                {
                }
            }

            public class C2
            {
                public readonly static string CVV2CIDPresenceIndicator;

                static C2()
                {
                    PNS.Bit48.C2.CVV2CIDPresenceIndicator = "Bit48.C2.CVV2CIDPresenceIndicator";
                }

                public C2()
                {
                }
            }

            public class C7
            {
                public readonly static string EMVPOSParameterDownloadRequestStatus;

                static C7()
                {
                    PNS.Bit48.C7.EMVPOSParameterDownloadRequestStatus = "Bit48.C7.EMVPOSParameterDownloadRequestStatus";
                }

                public C7()
                {
                }
            }

            public class C8
            {
                public readonly static string CreditDebitPrepaidSupportIndicator;

                static C8()
                {
                    PNS.Bit48.C8.CreditDebitPrepaidSupportIndicator = "Bit48.C8.CreditDebitPrepaidSupportIndicator";
                }

                public C8()
                {
                }
            }

            public class CA
            {
                public readonly static string CheckAuthorizationType;

                static CA()
                {
                    PNS.Bit48.CA.CheckAuthorizationType = "Bit48.CA.CheckAuthorizationType";
                }

                public CA()
                {
                }
            }

            public class CC
            {
                public readonly static string RequestType;

                public readonly static string MICROrDriversLicense;

                static CC()
                {
                    PNS.Bit48.CC.RequestType = "Bit48.CC.RequestType";
                    PNS.Bit48.CC.MICROrDriversLicense = "Bit48.CC.MICROrDriversLicense";
                }

                public CC()
                {
                }
            }

            public class CF
            {
                public readonly static string POSEntryModeCredentialOnFile;

                static CF()
                {
                    PNS.Bit48.CF.POSEntryModeCredentialOnFile = "Bit48.CF.POSEntryModeCredentialOnFile";
                }

                public CF()
                {
                }
            }

            public class CG
            {
                public readonly static string ChasePayCryptogram;

                static CG()
                {
                    PNS.Bit48.CG.ChasePayCryptogram = "Bit48.CG.ChasePayCryptogram";
                }

                public CG()
                {
                }
            }

            public class D1
            {
                public readonly static string DataEntrySource;

                static D1()
                {
                    PNS.Bit48.D1.DataEntrySource = "Bit48.D1.DataEntrySource";
                }

                public D1()
                {
                }
            }

            public class D2
            {
                public readonly static string TraceNumber;

                static D2()
                {
                    PNS.Bit48.D2.TraceNumber = "Bit48.D2.TraceNumber";
                }

                public D2()
                {
                }
            }

            public class D4
            {
                public readonly static string BillingAccountNumber;

                static D4()
                {
                    PNS.Bit48.D4.BillingAccountNumber = "Bit48.D4.BillingAccountNumber";
                }

                public D4()
                {
                }
            }

            public class D5Host
            {
                public readonly static string DCCRequestHost;

                static D5Host()
                {
                    PNS.Bit48.D5Host.DCCRequestHost = "Bit48.D5Host.DCCRequestHost";
                }

                public D5Host()
                {
                }
            }

            public class D5POS
            {
                public readonly static string DCCRequestHost;

                public readonly static string DCCAmount;

                public readonly static string DCCMinorUnit;

                public readonly static string DCCExchangeRate;

                public readonly static string DCCStrCurrencyCode;

                public readonly static string DCCIntCurrencyCode;

                static D5POS()
                {
                    PNS.Bit48.D5POS.DCCRequestHost = "Bit48.D5POS.DCCRequestHost";
                    PNS.Bit48.D5POS.DCCAmount = "Bit48.D5POS.DCCAmount";
                    PNS.Bit48.D5POS.DCCMinorUnit = "Bit48.D5POS.DCCMinorUnit";
                    PNS.Bit48.D5POS.DCCExchangeRate = "Bit48.D5POS.DCCExchangeRate";
                    PNS.Bit48.D5POS.DCCStrCurrencyCode = "Bit48.D5POS.DCCStrCurrencyCode";
                    PNS.Bit48.D5POS.DCCIntCurrencyCode = "Bit48.D5POS.DCCIntCurrencyCode";
                }

                public D5POS()
                {
                }
            }

            public class D6
            {
                public readonly static string DuplicateTransactionCheckingIndicator;

                static D6()
                {
                    PNS.Bit48.D6.DuplicateTransactionCheckingIndicator = "Bit48.D6.DuplicateTransactionCheckingIndicator";
                }

                public D6()
                {
                }
            }

            public class D7
            {
                public readonly static string RequestType;

                public readonly static string SoftDescriptor;

                static D7()
                {
                    PNS.Bit48.D7.RequestType = "Bit48.D7.RequestType";
                    PNS.Bit48.D7.SoftDescriptor = "Bit48.D7.SoftDescriptor";
                }

                public D7()
                {
                }
            }

            public class DA
            {
                public readonly static string ProcessingCode;

                public readonly static string SystemTraceAuditNumber;

                public readonly static string POSEntryMode;

                public readonly static string PINCapabilityCode;

                public readonly static string TrackDataConditionCode;

                public readonly static string AVSResponseCode;

                public readonly static string CIDResponseCode;

                public readonly static string AuthAmount;

                static DA()
                {
                    PNS.Bit48.DA.ProcessingCode = "Bit48.DA.ProcessingCode";
                    PNS.Bit48.DA.SystemTraceAuditNumber = "Bit48.DA.SystemTraceAuditNumber";
                    PNS.Bit48.DA.POSEntryMode = "Bit48.DA.POSEntryMode";
                    PNS.Bit48.DA.PINCapabilityCode = "Bit48.DA.PINCapabilityCode";
                    PNS.Bit48.DA.TrackDataConditionCode = "Bit48.DA.TrackDataConditionCode";
                    PNS.Bit48.DA.AVSResponseCode = "Bit48.DA.AVSResponseCode";
                    PNS.Bit48.DA.CIDResponseCode = "Bit48.DA.CIDResponseCode";
                    PNS.Bit48.DA.AuthAmount = "Bit48.DA.AuthAmount";
                }

                public DA()
                {
                }
            }

            public class DW
            {
                public readonly static string SDWOIndicator;

                static DW()
                {
                    PNS.Bit48.DW.SDWOIndicator = "Bit48.DW.SDWOIndicator";
                }

                public DW()
                {
                }
            }

            public class E1
            {
                public readonly static string RequestType;

                public readonly static string CustomerReferenceNumber;

                static E1()
                {
                    PNS.Bit48.E1.RequestType = "Bit48.E1.RequestType";
                    PNS.Bit48.E1.CustomerReferenceNumber = "Bit48.E1.CustomerReferenceNumber";
                }

                public E1()
                {
                }
            }

            public class E2
            {
                public readonly static string LocalTaxFlag;

                public readonly static string LocalTaxAmount;

                static E2()
                {
                    PNS.Bit48.E2.LocalTaxFlag = "Bit48.E2.LocalTaxFlag";
                    PNS.Bit48.E2.LocalTaxAmount = "Bit48.E2.LocalTaxAmount";
                }

                public E2()
                {
                }
            }

            public class E3
            {
                public readonly static string DestinationZipCode;

                static E3()
                {
                    PNS.Bit48.E3.DestinationZipCode = "Bit48.E3.DestinationZipCode";
                }

                public E3()
                {
                }
            }

            public class E4
            {
                public readonly static string TypeCode;

                public readonly static string VoucherNumber;

                static E4()
                {
                    PNS.Bit48.E4.TypeCode = "Bit48.E4.TypeCode";
                    PNS.Bit48.E4.VoucherNumber = "Bit48.E4.VoucherNumber";
                }

                public E4()
                {
                }
            }

            public class E6
            {
                public readonly static string EncryptedKeyIndex;

                static E6()
                {
                    PNS.Bit48.E6.EncryptedKeyIndex = "Bit48.E6.EncryptedKeyIndex";
                }

                public E6()
                {
                }
            }

            public class E8
            {
                public readonly static string FreightAmountIndicator;

                public readonly static string FreightAmount;

                public readonly static string DutyAmountIndicator;

                public readonly static string DutyAmount;

                public readonly static string DestinationCountryCode;

                public readonly static string ShipFromZip;

                static E8()
                {
                    PNS.Bit48.E8.FreightAmountIndicator = "Bit48.E8.FreightAmountIndicator";
                    PNS.Bit48.E8.FreightAmount = "Bit48.E8.FreightAmount";
                    PNS.Bit48.E8.DutyAmountIndicator = "Bit48.E8.DutyAmountIndicator";
                    PNS.Bit48.E8.DutyAmount = "Bit48.E8.DutyAmount";
                    PNS.Bit48.E8.DestinationCountryCode = "Bit48.E8.DestinationCountryCode";
                    PNS.Bit48.E8.ShipFromZip = "Bit48.E8.ShipFromZip";
                }

                public E8()
                {
                }
            }

            public class E9
            {
                public readonly static string ECPAuthorizationMethod;

                public readonly static string PreferredDeliveryMethod;

                static E9()
                {
                    PNS.Bit48.E9.ECPAuthorizationMethod = "Bit48.E9.ECPAuthorizationMethod";
                    PNS.Bit48.E9.PreferredDeliveryMethod = "Bit48.E9.PreferredDeliveryMethod";
                }

                public E9()
                {
                }
            }

            public class EA
            {
                public readonly static string ECPActionCode;

                static EA()
                {
                    PNS.Bit48.EA.ECPActionCode = "Bit48.EA.ECPActionCode";
                }

                public EA()
                {
                }
            }

            public class EI
            {
                public readonly static string ECommerceIndicator;

                static EI()
                {
                    PNS.Bit48.EI.ECommerceIndicator = "Bit48.EI.ECommerceIndicator";
                }

                public EI()
                {
                }
            }

            public class F1
            {
                public readonly static string FleetDataIndicator;

                public readonly static string VehicleNumber;

                public readonly static string Odometer;

                public readonly static string DriverNumberJobNumber;

                public readonly static string PINBlockFleetCorDriverID;

                static F1()
                {
                    PNS.Bit48.F1.FleetDataIndicator = "Bit48.F1.FleetDataIndicator";
                    PNS.Bit48.F1.VehicleNumber = "Bit48.F1.VehicleNumber";
                    PNS.Bit48.F1.Odometer = "Bit48.F1.Odometer";
                    PNS.Bit48.F1.DriverNumberJobNumber = "Bit48.F1.DriverNumberJobNumber";
                    PNS.Bit48.F1.PINBlockFleetCorDriverID = "Bit48.F1.PINBlockFleetCorDriverID";
                }

                public F1()
                {
                }
            }

            public class F2
            {
                public readonly static string FleetDataIndicator;

                public readonly static string NumberOfPromptCodesPresent;

                public readonly static string PurchaseDeviceSequenceNumber;

                public readonly static string TicketNumber;

                public readonly static string StorenForwardIndicator;

                static F2()
                {
                    PNS.Bit48.F2.FleetDataIndicator = "Bit48.F2.FleetDataIndicator";
                    PNS.Bit48.F2.NumberOfPromptCodesPresent = "Bit48.F2.NumberOfPromptCodesPresent";
                    PNS.Bit48.F2.PurchaseDeviceSequenceNumber = "Bit48.F2.PurchaseDeviceSequenceNumber";
                    PNS.Bit48.F2.TicketNumber = "Bit48.F2.TicketNumber";
                    PNS.Bit48.F2.StorenForwardIndicator = "Bit48.F2.StorenForwardIndicator";
                }

                public F2()
                {
                }
            }

            public class FE
            {
                public readonly static string AccountFundingTransactionForeignExchangeMarkUpFee;

                static FE()
                {
                    PNS.Bit48.FE.AccountFundingTransactionForeignExchangeMarkUpFee = "Bit48.FE.AccountFundingTransactionForeignExchangeMarkUpFee";
                }

                public FE()
                {
                }
            }

            public class G1
            {
                public readonly static string MethodOfPayment;

                static G1()
                {
                    PNS.Bit48.G1.MethodOfPayment = "Bit48.G1.MethodOfPayment";
                }

                public G1()
                {
                }
            }

            public class GS
            {
                public readonly static string GoodsSoldCode;

                static GS()
                {
                    PNS.Bit48.GS.GoodsSoldCode = "Bit48.GS.GoodsSoldCode";
                }

                public GS()
                {
                }
            }

            public class H1
            {
                public H1()
                {
                }

                public class PromptData
                {
                    public readonly static string AmountType;

                    public readonly static string Amount;

                    static PromptData()
                    {
                        PNS.Bit48.H1.PromptData.AmountType = "Bit48.H1.PromptData.AmountType";
                        PNS.Bit48.H1.PromptData.Amount = "Bit48.H1.PromptData.Amount";
                    }

                    public PromptData()
                    {
                    }
                }
            }

            public class I1
            {
                public readonly static string ImageReferenceNumber;

                static I1()
                {
                    PNS.Bit48.I1.ImageReferenceNumber = "Bit48.I1.ImageReferenceNumber";
                }

                public I1()
                {
                }
            }

            public class I2
            {
                public readonly static string InStorePaymentFlag;

                static I2()
                {
                    PNS.Bit48.I2.InStorePaymentFlag = "Bit48.I2.InStorePaymentFlag";
                }

                public I2()
                {
                }
            }

            public class L2
            {
                public readonly static string RequestorName;

                public readonly static string CustomerVATRegistrationNumber;

                static L2()
                {
                    PNS.Bit48.L2.RequestorName = "Bit48.L2.RequestorName";
                    PNS.Bit48.L2.CustomerVATRegistrationNumber = "Bit48.L2.CustomerVATRegistrationNumber";
                }

                public L2()
                {
                }
            }

            public class M1
            {
                public readonly static string MasterCardInterchangeComplianceIndicator;

                public readonly static string BankNetReferenceNumber;

                public readonly static string BankNetDate;

                public readonly static string CVCErrorIndicator;

                public readonly static string POSValidationCode;

                public readonly static string MagStripeQualityIndicator;

                static M1()
                {
                    PNS.Bit48.M1.MasterCardInterchangeComplianceIndicator = "Bit48.M1.MasterCardInterchangeComplianceIndicator";
                    PNS.Bit48.M1.BankNetReferenceNumber = "Bit48.M1.BankNetReferenceNumber";
                    PNS.Bit48.M1.BankNetDate = "Bit48.M1.BankNetDate";
                    PNS.Bit48.M1.CVCErrorIndicator = "Bit48.M1.CVCErrorIndicator";
                    PNS.Bit48.M1.POSValidationCode = "Bit48.M1.POSValidationCode";
                    PNS.Bit48.M1.MagStripeQualityIndicator = "Bit48.M1.MagStripeQualityIndicator";
                }

                public M1()
                {
                }
            }

            public class M3
            {
                public readonly static string MACValue;

                static M3()
                {
                    PNS.Bit48.M3.MACValue = "Bit48.M3.MACValue";
                }

                public M3()
                {
                }
            }

            public class MT
            {
                public readonly static string SubmittedTransactionIdentifier;

                public readonly static string MessageReasonCode;

                public readonly static string TotalCumulativeAmount;

                public readonly static string AmountSign;

                public readonly static string POSEnvironment;

                public readonly static string EstimateAuthorizationIndicator;

                public readonly static string TransactionIdentifierRequest;

                static MT()
                {
                    PNS.Bit48.MT.SubmittedTransactionIdentifier = "Bit48.MT.SubmittedTransactionIdentifier";
                    PNS.Bit48.MT.MessageReasonCode = "Bit48.MT.MessageReasonCode";
                    PNS.Bit48.MT.TotalCumulativeAmount = "Bit48.MT.TotalCumulativeAmount";
                    PNS.Bit48.MT.AmountSign = "Bit48.MT.AmountSign";
                    PNS.Bit48.MT.POSEnvironment = "Bit48.MT.POSEnvironment";
                    PNS.Bit48.MT.EstimateAuthorizationIndicator = "Bit48.MT.EstimateAuthorizationIndicator";
                    PNS.Bit48.MT.TransactionIdentifierRequest = "Bit48.MT.TransactionIdentifierRequest";
                }

                public MT()
                {
                }
            }

            public class MW
            {
                public readonly static string MasterPassWalletIdentifier;

                public readonly static string MobileWalletIdentifier;

                public readonly static string MasterPassIncentive;

                public readonly static string AdditionalWalletData;

                static MW()
                {
                    PNS.Bit48.MW.MasterPassWalletIdentifier = "Bit48.MW.MasterPassWalletIdentifier";
                    PNS.Bit48.MW.MobileWalletIdentifier = "Bit48.MW.MobileWalletIdentifier";
                    PNS.Bit48.MW.MasterPassIncentive = "Bit48.MW.MasterPassIncentive";
                    PNS.Bit48.MW.AdditionalWalletData = "Bit48.MW.AdditionalWalletData";
                }

                public MW()
                {
                }
            }

            public class N1
            {
                public readonly static string RequestType;

                public readonly static string Name;

                static N1()
                {
                    PNS.Bit48.N1.RequestType = "Bit48.N1.RequestType";
                    PNS.Bit48.N1.Name = "Bit48.N1.Name";
                }

                public N1()
                {
                }
            }

            public class OP
            {
                public readonly static string OperatorID;

                static OP()
                {
                    PNS.Bit48.OP.OperatorID = "Bit48.OP.OperatorID";
                }

                public OP()
                {
                }
            }

            public class OS
            {
                public readonly static string OperatorIDSupervisor;

                static OS()
                {
                    PNS.Bit48.OS.OperatorIDSupervisor = "Bit48.OS.OperatorIDSupervisor";
                }

                public OS()
                {
                }
            }

            public class P1
            {
                public readonly static string PartialAuthorizationRequested;

                static P1()
                {
                    PNS.Bit48.P1.PartialAuthorizationRequested = "Bit48.P1.PartialAuthorizationRequested";
                }

                public P1()
                {
                }
            }

            public class P2
            {
                public readonly static string DevicePumpLaneIdentifier;

                static P2()
                {
                    PNS.Bit48.P2.DevicePumpLaneIdentifier = "Bit48.P2.DevicePumpLaneIdentifier";
                }

                public P2()
                {
                }
            }

            public class P3
            {
                public readonly static string RequestType;

                public readonly static string PINPadSerialNumber;

                static P3()
                {
                    PNS.Bit48.P3.RequestType = "Bit48.P3.RequestType";
                    PNS.Bit48.P3.PINPadSerialNumber = "Bit48.P3.PINPadSerialNumber";
                }

                public P3()
                {
                }
            }

            public class P4
            {
                public P4()
                {
                }

                public class PromptData
                {
                    public readonly static string PromptIdentifier;

                    public readonly static string PromptData_;

                    static PromptData()
                    {
                        PNS.Bit48.P4.PromptData.PromptIdentifier = "Bit48.P4.PromptData.PromptIdentifier";
                        PNS.Bit48.P4.PromptData.PromptData_ = "Bit48.P4.PromptData.PromptData_";
                    }

                    public PromptData()
                    {
                    }
                }
            }

            public class P7
            {
                public readonly static string PartiallyApprovedTransactionAmount;

                static P7()
                {
                    PNS.Bit48.P7.PartiallyApprovedTransactionAmount = "Bit48.P7.PartiallyApprovedTransactionAmount";
                }

                public P7()
                {
                }
            }

            public class P8
            {
                public readonly static string EnhancedAuthorizationRequestIndicator;

                static P8()
                {
                    PNS.Bit48.P8.EnhancedAuthorizationRequestIndicator = "Bit48.P8.EnhancedAuthorizationRequestIndicator";
                }

                public P8()
                {
                }
            }

            public class P9
            {
                public readonly static string PreAuthIndicator;

                static P9()
                {
                    PNS.Bit48.P9.PreAuthIndicator = "Bit48.P9.PreAuthIndicator";
                }

                public P9()
                {
                }
            }

            public class PI
            {
                public readonly static string PaymentIndicator;

                public readonly static string DateOfBirth;

                public readonly static string MaskedAccountNumber;

                public readonly static string PartialPostalCode;

                public readonly static string LastName;

                static PI()
                {
                    PNS.Bit48.PI.PaymentIndicator = "Bit48.PI.PaymentIndicator";
                    PNS.Bit48.PI.DateOfBirth = "Bit48.PI.DateOfBirth";
                    PNS.Bit48.PI.MaskedAccountNumber = "Bit48.PI.MaskedAccountNumber";
                    PNS.Bit48.PI.PartialPostalCode = "Bit48.PI.PartialPostalCode";
                    PNS.Bit48.PI.LastName = "Bit48.PI.LastName";
                }

                public PI()
                {
                }
            }

            public class QS
            {
                public readonly static string RequestedPaymentService;

                static QS()
                {
                    PNS.Bit48.QS.RequestedPaymentService = "Bit48.QS.RequestedPaymentService";
                }

                public QS()
                {
                }
            }

            public class R1
            {
                public readonly static string RequestType;

                public readonly static string CustomerDefinedData;

                static R1()
                {
                    PNS.Bit48.R1.RequestType = "Bit48.R1.RequestType";
                    PNS.Bit48.R1.CustomerDefinedData = "Bit48.R1.CustomerDefinedData";
                }

                public R1()
                {
                }
            }

            public class R2
            {
                public readonly static string RecurringIndicator;

                public readonly static string TransactionTypeIndicator;

                public readonly static string SubmissionType;

                static R2()
                {
                    PNS.Bit48.R2.RecurringIndicator = "Bit48.R2.RecurringIndicator";
                    PNS.Bit48.R2.TransactionTypeIndicator = "Bit48.R2.TransactionTypeIndicator";
                    PNS.Bit48.R2.SubmissionType = "Bit48.R2.SubmissionType";
                }

                public R2()
                {
                }
            }

            public class R3
            {
                public readonly static string ReversalReasonCode;

                static R3()
                {
                    PNS.Bit48.R3.ReversalReasonCode = "Bit48.R3.ReversalReasonCode";
                }

                public R3()
                {
                }
            }

            public class R4
            {
                public readonly static string RequestType;

                public readonly static string MerchantSpecificInformation;

                static R4()
                {
                    PNS.Bit48.R4.RequestType = "Bit48.R4.RequestType";
                    PNS.Bit48.R4.MerchantSpecificInformation = "Bit48.R4.MerchantSpecificInformation";
                }

                public R4()
                {
                }
            }

            public class R5
            {
                public readonly static string RequestStatus;

                static R5()
                {
                    PNS.Bit48.R5.RequestStatus = "Bit48.R5.RequestStatus";
                }

                public R5()
                {
                }
            }

            public class R6
            {
                public readonly static string MoreDataFlag;

                static R6()
                {
                    PNS.Bit48.R6.MoreDataFlag = "Bit48.R6.MoreDataFlag";
                }

                public R6()
                {
                }
            }

            public class RP
            {
                public readonly static string RemotePaymentType;

                static RP()
                {
                    PNS.Bit48.RP.RemotePaymentType = "Bit48.RP.RemotePaymentType";
                }

                public RP()
                {
                }
            }

            public class S1
            {
                public readonly static string CardType;

                static S1()
                {
                    PNS.Bit48.S1.CardType = "Bit48.S1.CardType";
                }

                public S1()
                {
                }
            }

            public class S2
            {
                public readonly static string ActualResponseCode;

                public readonly static string AuthorizerResponseValues;

                static S2()
                {
                    PNS.Bit48.S2.ActualResponseCode = "Bit48.S2.ActualResponseCode";
                    PNS.Bit48.S2.AuthorizerResponseValues = "Bit48.S2.AuthorizerResponseValues";
                }

                public S2()
                {
                }
            }

            public class S3
            {
                public readonly static string PartialRedemptionRequested;

                static S3()
                {
                    PNS.Bit48.S3.PartialRedemptionRequested = "Bit48.S3.PartialRedemptionRequested";
                }

                public S3()
                {
                }
            }

            public class S4
            {
                public readonly static string ValueOfUCAFCollectionIndicator;

                public readonly static string SecureCodeResponseData;

                static S4()
                {
                    PNS.Bit48.S4.ValueOfUCAFCollectionIndicator = "Bit48.S4.ValueOfUCAFCollectionIndicator";
                    PNS.Bit48.S4.SecureCodeResponseData = "Bit48.S4.SecureCodeResponseData";
                }

                public S4()
                {
                }
            }

            public class S5
            {
                public readonly static string SAFIndicator;

                public readonly static string DateTimeOfOriginalTransaction;

                static S5()
                {
                    PNS.Bit48.S5.SAFIndicator = "Bit48.S5.SAFIndicator";
                    PNS.Bit48.S5.DateTimeOfOriginalTransaction = "Bit48.S5.DateTimeOfOriginalTransaction";
                }

                public S5()
                {
                }
            }

            public class S6
            {
                public readonly static string OriginalRetrievalReferenceNumber;

                public readonly static string OriginalTransmissionDateTime;

                static S6()
                {
                    PNS.Bit48.S6.OriginalRetrievalReferenceNumber = "Bit48.S6.OriginalRetrievalReferenceNumber";
                    PNS.Bit48.S6.OriginalTransmissionDateTime = "Bit48.S6.OriginalTransmissionDateTime";
                }

                public S6()
                {
                }
            }

            public class S8
            {
                public readonly static string StoredValueCardTypeIndicator;

                public readonly static string GiftCardTypeIndicator;

                public readonly static string ThirdPartySVTransactionType;

                public readonly static string ThirdPartyGiftCardTransactionInformation;

                static S8()
                {
                    PNS.Bit48.S8.StoredValueCardTypeIndicator = "Bit48.S8.StoredValueCardTypeIndicator";
                    PNS.Bit48.S8.GiftCardTypeIndicator = "Bit48.S8.GiftCardTypeIndicator";
                    PNS.Bit48.S8.ThirdPartySVTransactionType = "Bit48.S8.ThirdPartySVTransactionType";
                    PNS.Bit48.S8.ThirdPartyGiftCardTransactionInformation = "Bit48.S8.ThirdPartyGiftCardTransactionInformation";
                }

                public S8()
                {
                }
            }

            public class SF
            {
                public readonly static string ServiceFee;

                static SF()
                {
                    PNS.Bit48.SF.ServiceFee = "Bit48.SF.ServiceFee";
                }

                public SF()
                {
                }
            }

            public class T1
            {
                public readonly static string TransactionStatus;

                static T1()
                {
                    PNS.Bit48.T1.TransactionStatus = "Bit48.T1.TransactionStatus";
                }

                public T1()
                {
                }
            }

            public class T2
            {
                public readonly static string EMVContactlessTransactionSequenceCounter;

                static T2()
                {
                    PNS.Bit48.T2.EMVContactlessTransactionSequenceCounter = "Bit48.T2.EMVContactlessTransactionSequenceCounter";
                }

                public T2()
                {
                }
            }

            public class T3
            {
                public readonly static string IncomingAcctType;

                static T3()
                {
                    PNS.Bit48.T3.IncomingAcctType = "Bit48.T3.IncomingAcctType";
                }

                public T3()
                {
                }
            }

            public class TK
            {
                public readonly static string AccountStatus;

                public readonly static string TokenAssuranceLevel;

                public readonly static string ConsumerDigitalPaymentTokenFlag;

                public readonly static string SubsequentFlag;

                public readonly static string TokenRequestorID;

                static TK()
                {
                    PNS.Bit48.TK.AccountStatus = "Bit48.TK.AccountStatus";
                    PNS.Bit48.TK.TokenAssuranceLevel = "Bit48.TK.TokenAssuranceLevel";
                    PNS.Bit48.TK.ConsumerDigitalPaymentTokenFlag = "Bit48.TK.ConsumerDigitalPaymentTokenFlag";
                    PNS.Bit48.TK.SubsequentFlag = "Bit48.TK.SubsequentFlag";
                    PNS.Bit48.TK.TokenRequestorID = "Bit48.TK.TokenRequestorID";
                }

                public TK()
                {
                }
            }

            public class TR
            {
                public readonly static string SecurityRelatedControlInfo;

                static TR()
                {
                    PNS.Bit48.TR.SecurityRelatedControlInfo = "Bit48.TR.SecurityRelatedControlInfo";
                }

                public TR()
                {
                }
            }

            public class U1
            {
                public readonly static string MerchantURL;

                static U1()
                {
                    PNS.Bit48.U1.MerchantURL = "Bit48.U1.MerchantURL";
                }

                public U1()
                {
                }
            }

            public class U2
            {
                public readonly static string TotalShipmentNumber;

                public readonly static string SequenceNumber;

                static U2()
                {
                    PNS.Bit48.U2.TotalShipmentNumber = "Bit48.U2.TotalShipmentNumber";
                    PNS.Bit48.U2.SequenceNumber = "Bit48.U2.SequenceNumber";
                }

                public U2()
                {
                }
            }

            public class V4
            {
                public readonly static string MarketSpecificDataIndicator;

                public readonly static string Duration;

                static V4()
                {
                    PNS.Bit48.V4.MarketSpecificDataIndicator = "Bit48.V4.MarketSpecificDataIndicator";
                    PNS.Bit48.V4.Duration = "Bit48.V4.Duration";
                }

                public V4()
                {
                }
            }

            public class V6
            {
                public readonly static string OriginalAuthorizationAmount;

                static V6()
                {
                    PNS.Bit48.V6.OriginalAuthorizationAmount = "Bit48.V6.OriginalAuthorizationAmount";
                }

                public V6()
                {
                }
            }

            public class V7
            {
                public readonly static string CreditAuthorizationType;

                public readonly static string AuthorizationType;

                static V7()
                {
                    PNS.Bit48.V7.CreditAuthorizationType = "Bit48.V7.CreditAuthorizationType";
                    PNS.Bit48.V7.AuthorizationType = "Bit48.V7.AuthorizationType";
                }

                public V7()
                {
                }
            }

            public class VA
            {
                public readonly static string VerifiedByVisaAuthenticationResponseData;

                static VA()
                {
                    PNS.Bit48.VA.VerifiedByVisaAuthenticationResponseData = "Bit48.VA.VerifiedByVisaAuthenticationResponseData";
                }

                public VA()
                {
                }
            }

            public class VB
            {
                public readonly static string VBVResponseCode;

                static VB()
                {
                    PNS.Bit48.VB.VBVResponseCode = "Bit48.VB.VBVResponseCode";
                }

                public VB()
                {
                }
            }

            public class VC
            {
                public readonly static string EMVContactlessCardAuthResultsCode;

                static VC()
                {
                    PNS.Bit48.VC.EMVContactlessCardAuthResultsCode = "Bit48.VC.EMVContactlessCardAuthResultsCode";
                }

                public VC()
                {
                }
            }

            public class VD
            {
                public readonly static string CNandVisaCardLevelResultsCode;

                static VD()
                {
                    PNS.Bit48.VD.CNandVisaCardLevelResultsCode = "Bit48.VD.CNandVisaCardLevelResultsCode";
                }

                public VD()
                {
                }
            }

            public class VE
            {
                public readonly static string CNandVisaCVViCVVCAMResultsCode;

                static VE()
                {
                    PNS.Bit48.VE.CNandVisaCVViCVVCAMResultsCode = "Bit48.VE.CNandVisaCVViCVVCAMResultsCode";
                }

                public VE()
                {
                }
            }

            public class VF
            {
                public readonly static string VisaTokenAuthenticationVerificationValue;

                static VF()
                {
                    PNS.Bit48.VF.VisaTokenAuthenticationVerificationValue = "Bit48.VF.VisaTokenAuthenticationVerificationValue";
                }

                public VF()
                {
                }
            }

            public class VS
            {
                public readonly static string SpendQualifiedResultCode;

                static VS()
                {
                    PNS.Bit48.VS.SpendQualifiedResultCode = "Bit48.VS.SpendQualifiedResultCode";
                }

                public VS()
                {
                }
            }

            public class Z1
            {
                public readonly static string KeyType;

                static Z1()
                {
                    PNS.Bit48.Z1.KeyType = "Bit48.Z1.KeyType";
                }

                public Z1()
                {
                }
            }

            public class Z2
            {
                public readonly static string KeyType;

                public readonly static string StorageIndex;

                public readonly static string WorkingKeyEncryptedUnderZoneMasterKey;

                public readonly static string FieldSeparator;

                public readonly static string CheckValue;

                static Z2()
                {
                    PNS.Bit48.Z2.KeyType = "Bit48.Z2.KeyType";
                    PNS.Bit48.Z2.StorageIndex = "Bit48.Z2.StorageIndex";
                    PNS.Bit48.Z2.WorkingKeyEncryptedUnderZoneMasterKey = "Bit48.Z2.WorkingKeyEncryptedUnderZoneMasterKey";
                    PNS.Bit48.Z2.FieldSeparator = "Bit48.Z2.FieldSeparator";
                    PNS.Bit48.Z2.CheckValue = "Bit48.Z2.CheckValue";
                }

                public Z2()
                {
                }
            }

            public class Z3
            {
                public readonly static string SecurityFormatCode;

                public readonly static string PINBlockFormatCode;

                public readonly static string StorageIndex;

                public readonly static string SecurityMessageCounter;

                static Z3()
                {
                    PNS.Bit48.Z3.SecurityFormatCode = "Bit48.Z3.SecurityFormatCode";
                    PNS.Bit48.Z3.PINBlockFormatCode = "Bit48.Z3.PINBlockFormatCode";
                    PNS.Bit48.Z3.StorageIndex = "Bit48.Z3.StorageIndex";
                    PNS.Bit48.Z3.SecurityMessageCounter = "Bit48.Z3.SecurityMessageCounter";
                }

                public Z3()
                {
                }
            }

            public class Z4
            {
                public readonly static string SecurityFormatCode;

                public readonly static string StorageIndex;

                public readonly static string SecurityMessageCounter;

                static Z4()
                {
                    PNS.Bit48.Z4.SecurityFormatCode = "Bit48.Z4.SecurityFormatCode";
                    PNS.Bit48.Z4.StorageIndex = "Bit48.Z4.StorageIndex";
                    PNS.Bit48.Z4.SecurityMessageCounter = "Bit48.Z4.SecurityMessageCounter";
                }

                public Z4()
                {
                }
            }

            public class Z5
            {
                public readonly static string SecurityFormatCode;

                public readonly static string StorageIndex;

                public readonly static string SecurityMessageCounter;

                static Z5()
                {
                    PNS.Bit48.Z5.SecurityFormatCode = "Bit48.Z5.SecurityFormatCode";
                    PNS.Bit48.Z5.StorageIndex = "Bit48.Z5.StorageIndex";
                    PNS.Bit48.Z5.SecurityMessageCounter = "Bit48.Z5.SecurityMessageCounter";
                }

                public Z5()
                {
                }
            }
        }

        public class Bit49
        {
            public readonly static string CurrencyCode;

            static Bit49()
            {
                PNS.Bit49.CurrencyCode = "Bit49.CurrencyCode";
            }

            public Bit49()
            {
            }
        }

        public class Bit52
        {
            public readonly static string PersonalIdentificationNumber;

            static Bit52()
            {
                PNS.Bit52.PersonalIdentificationNumber = "Bit52.PersonalIdentificationNumber";
            }

            public Bit52()
            {
            }
        }

        public class Bit53
        {
            public readonly static string SecurityRelatedControlInformation;

            static Bit53()
            {
                PNS.Bit53.SecurityRelatedControlInformation = "Bit53.SecurityRelatedControlInformation";
            }

            public Bit53()
            {
            }
        }

        public class Bit54
        {
            public readonly static string AdditionalAmount;

            static Bit54()
            {
                PNS.Bit54.AdditionalAmount = "Bit54.AdditionalAmount";
            }

            public Bit54()
            {
            }
        }

        public class Bit55
        {
            public readonly static string ChipCardData;

            static Bit55()
            {
                PNS.Bit55.ChipCardData = "Bit55.ChipCardData";
            }

            public Bit55()
            {
            }
        }

        public class Bit60
        {
            public Bit60()
            {
            }

            public class A1
            {
                public readonly static string AttendedTerminalData;

                public readonly static string TerminalLocation;

                public readonly static string CardholderAttendance;

                public readonly static string CardPresentIndicator;

                public readonly static string CardholderActivatedTerminalInformation;

                public readonly static string TerminalEntryCapability;

                public readonly static string CardholderAuthenticationMethod;

                static A1()
                {
                    PNS.Bit60.A1.AttendedTerminalData = "Bit60.A1.AttendedTerminalData";
                    PNS.Bit60.A1.TerminalLocation = "Bit60.A1.TerminalLocation";
                    PNS.Bit60.A1.CardholderAttendance = "Bit60.A1.CardholderAttendance";
                    PNS.Bit60.A1.CardPresentIndicator = "Bit60.A1.CardPresentIndicator";
                    PNS.Bit60.A1.CardholderActivatedTerminalInformation = "Bit60.A1.CardholderActivatedTerminalInformation";
                    PNS.Bit60.A1.TerminalEntryCapability = "Bit60.A1.TerminalEntryCapability";
                    PNS.Bit60.A1.CardholderAuthenticationMethod = "Bit60.A1.CardholderAuthenticationMethod";
                }

                public A1()
                {
                }
            }
        }

        public class Bit62
        {
            public Bit62()
            {
            }

            public class AG
            {
                public readonly static string DBAName;

                public readonly static string SubMerchantID;

                public readonly static string Street;

                public readonly static string City;

                public readonly static string StateProvince;

                public readonly static string PostalCode;

                public readonly static string CountryCode;

                public readonly static string SubEntitlementNumber;

                public readonly static string ContactInfo;

                public readonly static string Email;

                public readonly static string SellerID;

                static AG()
                {
                    PNS.Bit62.AG.DBAName = "Bit62.AG.DBAName";
                    PNS.Bit62.AG.SubMerchantID = "Bit62.AG.SubMerchantID";
                    PNS.Bit62.AG.Street = "Bit62.AG.Street";
                    PNS.Bit62.AG.City = "Bit62.AG.City";
                    PNS.Bit62.AG.StateProvince = "Bit62.AG.StateProvince";
                    PNS.Bit62.AG.PostalCode = "Bit62.AG.PostalCode";
                    PNS.Bit62.AG.CountryCode = "Bit62.AG.CountryCode";
                    PNS.Bit62.AG.SubEntitlementNumber = "Bit62.AG.SubEntitlementNumber";
                    PNS.Bit62.AG.ContactInfo = "Bit62.AG.ContactInfo";
                    PNS.Bit62.AG.Email = "Bit62.AG.Email";
                    PNS.Bit62.AG.SellerID = "Bit62.AG.SellerID";
                }

                public AG()
                {
                }
            }

            public class B2
            {
                public readonly static string BatchUploadType;

                public readonly static string BatchNumber;

                public readonly static string SalesTransactionCount;

                public readonly static string TotalSalesAmount;

                public readonly static string ReturnTransactionCount;

                public readonly static string TotalReturnsAmount;

                static B2()
                {
                    PNS.Bit62.B2.BatchUploadType = "Bit62.B2.BatchUploadType";
                    PNS.Bit62.B2.BatchNumber = "Bit62.B2.BatchNumber";
                    PNS.Bit62.B2.SalesTransactionCount = "Bit62.B2.SalesTransactionCount";
                    PNS.Bit62.B2.TotalSalesAmount = "Bit62.B2.TotalSalesAmount";
                    PNS.Bit62.B2.ReturnTransactionCount = "Bit62.B2.ReturnTransactionCount";
                    PNS.Bit62.B2.TotalReturnsAmount = "Bit62.B2.TotalReturnsAmount";
                }

                public B2()
                {
                }
            }

            public class DV
            {
                public readonly static string CAVV;

                static DV()
                {
                    PNS.Bit62.DV.CAVV = "Bit62.DV.CAVV";
                }

                public DV()
                {
                }
            }

            public class H1
            {
                public readonly static string JulianDay;

                public readonly static string BatchNumber;

                public readonly static string TransactionClass;

                public readonly static string SequenceNumber;

                static H1()
                {
                    PNS.Bit62.H1.JulianDay = "Bit62.H1.JulianDay";
                    PNS.Bit62.H1.BatchNumber = "Bit62.H1.BatchNumber";
                    PNS.Bit62.H1.TransactionClass = "Bit62.H1.TransactionClass";
                    PNS.Bit62.H1.SequenceNumber = "Bit62.H1.SequenceNumber";
                }

                public H1()
                {
                }
            }

            public class O1
            {
                public readonly static string ProviderName;

                static O1()
                {
                    PNS.Bit62.O1.ProviderName = "Bit62.O1.ProviderName";
                }

                public O1()
                {
                }
            }

            public class OD
            {
                public readonly static string SenderReferenceNumber;

                public readonly static string SenderName;

                public readonly static string RecipientName;

                public readonly static string SenderStreetAddress;

                public readonly static string SenderCity;

                public readonly static string SenderStateProvince;

                public readonly static string SenderZip;

                public readonly static string SenderCountry;

                public readonly static string SourceOfFunds;

                static OD()
                {
                    PNS.Bit62.OD.SenderReferenceNumber = "Bit62.OD.SenderReferenceNumber";
                    PNS.Bit62.OD.SenderName = "Bit62.OD.SenderName";
                    PNS.Bit62.OD.RecipientName = "Bit62.OD.RecipientName";
                    PNS.Bit62.OD.SenderStreetAddress = "Bit62.OD.SenderStreetAddress";
                    PNS.Bit62.OD.SenderCity = "Bit62.OD.SenderCity";
                    PNS.Bit62.OD.SenderStateProvince = "Bit62.OD.SenderStateProvince";
                    PNS.Bit62.OD.SenderZip = "Bit62.OD.SenderZip";
                    PNS.Bit62.OD.SenderCountry = "Bit62.OD.SenderCountry";
                    PNS.Bit62.OD.SourceOfFunds = "Bit62.OD.SourceOfFunds";
                }

                public OD()
                {
                }
            }

            public class P1
            {
                public readonly static string HardwareVendorIdentifier;

                public readonly static string SoftwareIdentifier;

                public readonly static string HardwareSerialNumber;

                static P1()
                {
                    PNS.Bit62.P1.HardwareVendorIdentifier = "Bit62.P1.HardwareVendorIdentifier";
                    PNS.Bit62.P1.SoftwareIdentifier = "Bit62.P1.SoftwareIdentifier";
                    PNS.Bit62.P1.HardwareSerialNumber = "Bit62.P1.HardwareSerialNumber";
                }

                public P1()
                {
                }
            }

            public class P2
            {
                public P2()
                {
                }

                public class Bit10
                {
                    public readonly static string P10IndustryInformation2;

                    static Bit10()
                    {
                        PNS.Bit62.P2.Bit10.P10IndustryInformation2 = "Bit62.P2.Bit10.P10IndustryInformation2";
                    }

                    public Bit10()
                    {
                    }
                }

                public class Bit11
                {
                    public readonly static string P11ClassComplianceCertification;

                    static Bit11()
                    {
                        PNS.Bit62.P2.Bit11.P11ClassComplianceCertification = "Bit62.P2.Bit11.P11ClassComplianceCertification";
                    }

                    public Bit11()
                    {
                    }
                }

                public class Bit12
                {
                    public readonly static string P12DeviceSecurityFeatures;

                    static Bit12()
                    {
                        PNS.Bit62.P2.Bit12.P12DeviceSecurityFeatures = "Bit62.P2.Bit12.P12DeviceSecurityFeatures";
                    }

                    public Bit12()
                    {
                    }
                }

                public class Bit13
                {
                    public readonly static string P13FutureCapabilities2;

                    static Bit13()
                    {
                        PNS.Bit62.P2.Bit13.P13FutureCapabilities2 = "Bit62.P2.Bit13.P13FutureCapabilities2";
                    }

                    public Bit13()
                    {
                    }
                }

                public class Bit2
                {
                    public readonly static string P2HostProcessingPlatform;

                    static Bit2()
                    {
                        PNS.Bit62.P2.Bit2.P2HostProcessingPlatform = "Bit62.P2.Bit2.P2HostProcessingPlatform";
                    }

                    public Bit2()
                    {
                    }
                }

                public class Bit3
                {
                    public readonly static string P3MessageFormatSupport1;

                    static Bit3()
                    {
                        PNS.Bit62.P2.Bit3.P3MessageFormatSupport1 = "Bit62.P2.Bit3.P3MessageFormatSupport1";
                    }

                    public Bit3()
                    {
                    }
                }

                public class Bit4
                {
                    public readonly static string P4EMVSupport;

                    static Bit4()
                    {
                        PNS.Bit62.P2.Bit4.P4EMVSupport = "Bit62.P2.Bit4.P4EMVSupport";
                    }

                    public Bit4()
                    {
                    }
                }

                public class Bit5
                {
                    public readonly static string P5PeripheralSupport1;

                    static Bit5()
                    {
                        PNS.Bit62.P2.Bit5.P5PeripheralSupport1 = "Bit62.P2.Bit5.P5PeripheralSupport1";
                    }

                    public Bit5()
                    {
                    }
                }

                public class Bit6
                {
                    public readonly static string P6PeripheralSupport2;

                    static Bit6()
                    {
                        PNS.Bit62.P2.Bit6.P6PeripheralSupport2 = "Bit62.P2.Bit6.P6PeripheralSupport2";
                    }

                    public Bit6()
                    {
                    }
                }

                public class Bit7
                {
                    public readonly static string P7CommunicationInformation1;

                    static Bit7()
                    {
                        PNS.Bit62.P2.Bit7.P7CommunicationInformation1 = "Bit62.P2.Bit7.P7CommunicationInformation1";
                    }

                    public Bit7()
                    {
                    }
                }

                public class Bit8
                {
                    public readonly static string P8CommunicationInformation2;

                    static Bit8()
                    {
                        PNS.Bit62.P2.Bit8.P8CommunicationInformation2 = "Bit62.P2.Bit8.P8CommunicationInformation2";
                    }

                    public Bit8()
                    {
                    }
                }

                public class Bit9
                {
                    public readonly static string P9IndustryInformation1;

                    static Bit9()
                    {
                        PNS.Bit62.P2.Bit9.P9IndustryInformation1 = "Bit62.P2.Bit9.P9IndustryInformation1";
                    }

                    public Bit9()
                    {
                    }
                }
            }

            public class P3
            {
                public readonly static string PrivateLabelCardProgram;

                public readonly static string PrivateLabelAdditionalData;

                static P3()
                {
                    PNS.Bit62.P3.PrivateLabelCardProgram = "Bit62.P3.PrivateLabelCardProgram";
                    PNS.Bit62.P3.PrivateLabelAdditionalData = "Bit62.P3.PrivateLabelAdditionalData";
                }

                public P3()
                {
                }
            }

            public class P3TD
            {
                public readonly static string PrivateLabelCardProgram;

                public readonly static string CreditPlan;

                public readonly static string InvoiceNumber;

                static P3TD()
                {
                    PNS.Bit62.P3TD.PrivateLabelCardProgram = "Bit62.P3TD.PrivateLabelCardProgram";
                    PNS.Bit62.P3TD.CreditPlan = "Bit62.P3TD.CreditPlan";
                    PNS.Bit62.P3TD.InvoiceNumber = "Bit62.P3TD.InvoiceNumber";
                }

                public P3TD()
                {
                }
            }

            public class P4
            {
                public readonly static string PANMethodology;

                public readonly static string EncryptionPayload;

                static P4()
                {
                    PNS.Bit62.P4.PANMethodology = "Bit62.P4.PANMethodology";
                    PNS.Bit62.P4.EncryptionPayload = "Bit62.P4.EncryptionPayload";
                }

                public P4()
                {
                }
            }

            public class P4Ingenico
            {
                public readonly static string PANMethodology;

                public readonly static string MethodologyID;

                public readonly static string KeySerialNumber;

                public readonly static string KSNExtension;

                public readonly static string EncryptedCardData;

                static P4Ingenico()
                {
                    PNS.Bit62.P4Ingenico.PANMethodology = "Bit62.P4Ingenico.PANMethodology";
                    PNS.Bit62.P4Ingenico.MethodologyID = "Bit62.P4Ingenico.MethodologyID";
                    PNS.Bit62.P4Ingenico.KeySerialNumber = "Bit62.P4Ingenico.KeySerialNumber";
                    PNS.Bit62.P4Ingenico.KSNExtension = "Bit62.P4Ingenico.KSNExtension";
                    PNS.Bit62.P4Ingenico.EncryptedCardData = "Bit62.P4Ingenico.EncryptedCardData";
                }

                public P4Ingenico()
                {
                }
            }

            public class P4Magtek
            {
                public readonly static string PANMethodology;

                public readonly static string MethodologyID;

                public readonly static string EncryptedFlag;

                public readonly static string KeySerialNumber;

                public readonly static string EncryptedBlock;

                static P4Magtek()
                {
                    PNS.Bit62.P4Magtek.PANMethodology = "Bit62.P4Magtek.PANMethodology";
                    PNS.Bit62.P4Magtek.MethodologyID = "Bit62.P4Magtek.MethodologyID";
                    PNS.Bit62.P4Magtek.EncryptedFlag = "Bit62.P4Magtek.EncryptedFlag";
                    PNS.Bit62.P4Magtek.KeySerialNumber = "Bit62.P4Magtek.KeySerialNumber";
                    PNS.Bit62.P4Magtek.EncryptedBlock = "Bit62.P4Magtek.EncryptedBlock";
                }

                public P4Magtek()
                {
                }
            }

            public class P4Semtek
            {
                public readonly static string PANMethodology;

                public readonly static string SemtekEParms;

                static P4Semtek()
                {
                    PNS.Bit62.P4Semtek.PANMethodology = "Bit62.P4Semtek.PANMethodology";
                    PNS.Bit62.P4Semtek.SemtekEParms = "Bit62.P4Semtek.SemtekEParms";
                }

                public P4Semtek()
                {
                }
            }

            public class T1
            {
                public readonly static string ApplicationName;

                public readonly static string ReleaseDate;

                public readonly static string EPROMVersionNumber;

                static T1()
                {
                    PNS.Bit62.T1.ApplicationName = "Bit62.T1.ApplicationName";
                    PNS.Bit62.T1.ReleaseDate = "Bit62.T1.ReleaseDate";
                    PNS.Bit62.T1.EPROMVersionNumber = "Bit62.T1.EPROMVersionNumber";
                }

                public T1()
                {
                }
            }

            public class ZD
            {
                public ZD()
                {
                }

                public class Bit1
                {
                    public readonly static string P2HostToHostEncryption;

                    static Bit1()
                    {
                        PNS.Bit62.ZD.Bit1.P2HostToHostEncryption = "Bit62.ZD.Bit1.P2HostToHostEncryption";
                    }

                    public Bit1()
                    {
                    }
                }
            }
        }

        public class Bit63
        {
            public Bit63()
            {
            }

            public class A1
            {
                public readonly static string ExtraChargesAmount;

                public readonly static string RentalAgreementNumber;

                public readonly static string RentalCity;

                public readonly static string RentalStateCode;

                public readonly static string RentalDate;

                public readonly static string RentalTime;

                public readonly static string ReturnCity;

                public readonly static string ReturnStateCode;

                public readonly static string ReturnDate;

                public readonly static string ReturnTime;

                public readonly static string RenterName;

                public readonly static string ExtraChargesReasons;

                static A1()
                {
                    PNS.Bit63.A1.ExtraChargesAmount = "Bit63.A1.ExtraChargesAmount";
                    PNS.Bit63.A1.RentalAgreementNumber = "Bit63.A1.RentalAgreementNumber";
                    PNS.Bit63.A1.RentalCity = "Bit63.A1.RentalCity";
                    PNS.Bit63.A1.RentalStateCode = "Bit63.A1.RentalStateCode";
                    PNS.Bit63.A1.RentalDate = "Bit63.A1.RentalDate";
                    PNS.Bit63.A1.RentalTime = "Bit63.A1.RentalTime";
                    PNS.Bit63.A1.ReturnCity = "Bit63.A1.ReturnCity";
                    PNS.Bit63.A1.ReturnStateCode = "Bit63.A1.ReturnStateCode";
                    PNS.Bit63.A1.ReturnDate = "Bit63.A1.ReturnDate";
                    PNS.Bit63.A1.ReturnTime = "Bit63.A1.ReturnTime";
                    PNS.Bit63.A1.RenterName = "Bit63.A1.RenterName";
                    PNS.Bit63.A1.ExtraChargesReasons = "Bit63.A1.ExtraChargesReasons";
                }

                public A1()
                {
                }
            }

            public class A2
            {
                public readonly static string RentalRate;

                public readonly static string RentalCountry;

                public readonly static string RentalClassID;

                public readonly static string NoShowIndicator;

                public readonly static string ReturnCountry;

                public readonly static string ReturnLocationID;

                public readonly static string OneWayDropOffCharges;

                static A2()
                {
                    PNS.Bit63.A2.RentalRate = "Bit63.A2.RentalRate";
                    PNS.Bit63.A2.RentalCountry = "Bit63.A2.RentalCountry";
                    PNS.Bit63.A2.RentalClassID = "Bit63.A2.RentalClassID";
                    PNS.Bit63.A2.NoShowIndicator = "Bit63.A2.NoShowIndicator";
                    PNS.Bit63.A2.ReturnCountry = "Bit63.A2.ReturnCountry";
                    PNS.Bit63.A2.ReturnLocationID = "Bit63.A2.ReturnLocationID";
                    PNS.Bit63.A2.OneWayDropOffCharges = "Bit63.A2.OneWayDropOffCharges";
                }

                public A2()
                {
                }
            }

            public class D1
            {
                public readonly static string ItemProductCode;

                public readonly static string ItemDescription;

                public readonly static string ItemQuantity;

                public readonly static string ItemBulkUnitOfMeasure;

                public readonly static string ItemDebitCreditIndicator;

                public readonly static string ItemNetGrossIndicator;

                public readonly static string ItemExtendedAmount;

                static D1()
                {
                    PNS.Bit63.D1.ItemProductCode = "Bit63.D1.ItemProductCode";
                    PNS.Bit63.D1.ItemDescription = "Bit63.D1.ItemDescription";
                    PNS.Bit63.D1.ItemQuantity = "Bit63.D1.ItemQuantity";
                    PNS.Bit63.D1.ItemBulkUnitOfMeasure = "Bit63.D1.ItemBulkUnitOfMeasure";
                    PNS.Bit63.D1.ItemDebitCreditIndicator = "Bit63.D1.ItemDebitCreditIndicator";
                    PNS.Bit63.D1.ItemNetGrossIndicator = "Bit63.D1.ItemNetGrossIndicator";
                    PNS.Bit63.D1.ItemExtendedAmount = "Bit63.D1.ItemExtendedAmount";
                }

                public D1()
                {
                }
            }

            public class D2
            {
                public readonly static string ItemUnitCost;

                static D2()
                {
                    PNS.Bit63.D2.ItemUnitCost = "Bit63.D2.ItemUnitCost";
                }

                public D2()
                {
                }
            }

            public class D3
            {
                public readonly static string ItemDiscountAmount;

                static D3()
                {
                    PNS.Bit63.D3.ItemDiscountAmount = "Bit63.D3.ItemDiscountAmount";
                }

                public D3()
                {
                }
            }

            public class D4
            {
                public readonly static string ItemLocalTaxAmount;

                static D4()
                {
                    PNS.Bit63.D4.ItemLocalTaxAmount = "Bit63.D4.ItemLocalTaxAmount";
                }

                public D4()
                {
                }
            }

            public class D5
            {
                public readonly static string ItemCommodityCode;

                static D5()
                {
                    PNS.Bit63.D5.ItemCommodityCode = "Bit63.D5.ItemCommodityCode";
                }

                public D5()
                {
                }
            }

            public class D6
            {
                public readonly static string TotalTaxAmount;

                static D6()
                {
                    PNS.Bit63.D6.TotalTaxAmount = "Bit63.D6.TotalTaxAmount";
                }

                public D6()
                {
                }

                public class ItemTaxData
                {
                    public readonly static string ItemizedTaxTypeIdentifier;

                    public readonly static string ItemizedTaxAmount;

                    static ItemTaxData()
                    {
                        PNS.Bit63.D6.ItemTaxData.ItemizedTaxTypeIdentifier = "Bit63.D6.ItemTaxData.ItemizedTaxTypeIdentifier";
                        PNS.Bit63.D6.ItemTaxData.ItemizedTaxAmount = "Bit63.D6.ItemTaxData.ItemizedTaxAmount";
                    }

                    public ItemTaxData()
                    {
                    }
                }
            }

            public class D7
            {
                public readonly static string InvoiceDiscountTreatment;

                public readonly static string TaxTreatment;

                public readonly static string DiscountAmountSign;

                public readonly static string FreightAmountSign;

                public readonly static string DutyAmountSign;

                public readonly static string VATTaxAmountSignOrderLevel;

                public readonly static string LineItemLevelDiscountTreatmentCode;

                public readonly static string LineItemDetailIndicator;

                static D7()
                {
                    PNS.Bit63.D7.InvoiceDiscountTreatment = "Bit63.D7.InvoiceDiscountTreatment";
                    PNS.Bit63.D7.TaxTreatment = "Bit63.D7.TaxTreatment";
                    PNS.Bit63.D7.DiscountAmountSign = "Bit63.D7.DiscountAmountSign";
                    PNS.Bit63.D7.FreightAmountSign = "Bit63.D7.FreightAmountSign";
                    PNS.Bit63.D7.DutyAmountSign = "Bit63.D7.DutyAmountSign";
                    PNS.Bit63.D7.VATTaxAmountSignOrderLevel = "Bit63.D7.VATTaxAmountSignOrderLevel";
                    PNS.Bit63.D7.LineItemLevelDiscountTreatmentCode = "Bit63.D7.LineItemLevelDiscountTreatmentCode";
                    PNS.Bit63.D7.LineItemDetailIndicator = "Bit63.D7.LineItemDetailIndicator";
                }

                public D7()
                {
                }
            }

            public class D8
            {
                public readonly static string UniqueVATInvoiceReferenceNumber;

                public readonly static string VATTaxAmountForFreightAndShipping;

                public readonly static string VATTaxRateForFreightAndShipping;

                public readonly static string VATTaxAmountForLineItems;

                public readonly static string VATTaxRateForLineItems;

                static D8()
                {
                    PNS.Bit63.D8.UniqueVATInvoiceReferenceNumber = "Bit63.D8.UniqueVATInvoiceReferenceNumber";
                    PNS.Bit63.D8.VATTaxAmountForFreightAndShipping = "Bit63.D8.VATTaxAmountForFreightAndShipping";
                    PNS.Bit63.D8.VATTaxRateForFreightAndShipping = "Bit63.D8.VATTaxRateForFreightAndShipping";
                    PNS.Bit63.D8.VATTaxAmountForLineItems = "Bit63.D8.VATTaxAmountForLineItems";
                    PNS.Bit63.D8.VATTaxRateForLineItems = "Bit63.D8.VATTaxRateForLineItems";
                }

                public D8()
                {
                }
            }

            public class E1
            {
                public readonly static string OrderNumber;

                public readonly static string SecurityIndicator;

                public readonly static string ECommerceIndicator;

                public readonly static string ElectronicCommerceGoodsIndicator;

                static E1()
                {
                    PNS.Bit63.E1.OrderNumber = "Bit63.E1.OrderNumber";
                    PNS.Bit63.E1.SecurityIndicator = "Bit63.E1.SecurityIndicator";
                    PNS.Bit63.E1.ECommerceIndicator = "Bit63.E1.ECommerceIndicator";
                    PNS.Bit63.E1.ElectronicCommerceGoodsIndicator = "Bit63.E1.ElectronicCommerceGoodsIndicator";
                }

                public E1()
                {
                }
            }

            public class L1
            {
                public readonly static string FolioNumber;

                public readonly static string ChargeDescription;

                public readonly static string ArrivalDate;

                public readonly static string DepartureDate;

                public readonly static string SaleCode;

                public readonly static string ExtraChargesReasons;

                public readonly static string ExtraChargesAmount;

                static L1()
                {
                    PNS.Bit63.L1.FolioNumber = "Bit63.L1.FolioNumber";
                    PNS.Bit63.L1.ChargeDescription = "Bit63.L1.ChargeDescription";
                    PNS.Bit63.L1.ArrivalDate = "Bit63.L1.ArrivalDate";
                    PNS.Bit63.L1.DepartureDate = "Bit63.L1.DepartureDate";
                    PNS.Bit63.L1.SaleCode = "Bit63.L1.SaleCode";
                    PNS.Bit63.L1.ExtraChargesReasons = "Bit63.L1.ExtraChargesReasons";
                    PNS.Bit63.L1.ExtraChargesAmount = "Bit63.L1.ExtraChargesAmount";
                }

                public L1()
                {
                }
            }

            public class L2
            {
                public readonly static string RenterName;

                public readonly static string LodgingPlan;

                public readonly static string RoomRate;

                public readonly static string NoOfNightsRoomRate1;

                public readonly static string RoomRate2;

                public readonly static string NoOfNightsRoomRate2;

                public readonly static string RoomRate3;

                public readonly static string NoOfNightsRoomRate3;

                public readonly static string RoomTax;

                public readonly static string TaxAmount;

                public readonly static string PrepaidExpense;

                public readonly static string FolioCashAdvances;

                public readonly static string PostCheckOutChargesAmt;

                public readonly static string RestaurantRoomServiceChargesAmt;

                public readonly static string GiftShopChargesAmt;

                public readonly static string MiniBarChargesAmt;

                public readonly static string PhoneChargesAmt;

                public readonly static string LaundryChargesAmt;

                public readonly static string HealthClubChargesAmt;

                public readonly static string LoungeBarChargesAmt;

                public readonly static string MovieChargesAmt;

                public readonly static string ValetParkingChargesAmt;

                public readonly static string BusinessCenterChargesAmt;

                public readonly static string NonRoomChargesAmt;

                public readonly static string OtherServicesCode1;

                public readonly static string OtherServicesAmt1;

                public readonly static string OtherServicesCode2;

                public readonly static string OtherServicesAmt2;

                public readonly static string OtherServicesCode3;

                public readonly static string OtherServicesAmt3;

                public readonly static string OtherServicesCode4;

                public readonly static string OtherServicesAmt4;

                public readonly static string OtherServicesCode5;

                public readonly static string OtherServicesAmt5;

                public readonly static string OtherServicesCode6;

                public readonly static string OtherServicesAmt6;

                static L2()
                {
                    PNS.Bit63.L2.RenterName = "Bit63.L2.RenterName";
                    PNS.Bit63.L2.LodgingPlan = "Bit63.L2.LodgingPlan";
                    PNS.Bit63.L2.RoomRate = "Bit63.L2.RoomRate";
                    PNS.Bit63.L2.NoOfNightsRoomRate1 = "Bit63.L2.NoOfNightsRoomRate1";
                    PNS.Bit63.L2.RoomRate2 = "Bit63.L2.RoomRate2";
                    PNS.Bit63.L2.NoOfNightsRoomRate2 = "Bit63.L2.NoOfNightsRoomRate2";
                    PNS.Bit63.L2.RoomRate3 = "Bit63.L2.RoomRate3";
                    PNS.Bit63.L2.NoOfNightsRoomRate3 = "Bit63.L2.NoOfNightsRoomRate3";
                    PNS.Bit63.L2.RoomTax = "Bit63.L2.RoomTax";
                    PNS.Bit63.L2.TaxAmount = "Bit63.L2.TaxAmount";
                    PNS.Bit63.L2.PrepaidExpense = "Bit63.L2.PrepaidExpense";
                    PNS.Bit63.L2.FolioCashAdvances = "Bit63.L2.FolioCashAdvances";
                    PNS.Bit63.L2.PostCheckOutChargesAmt = "Bit63.L2.PostCheckOutChargesAmt";
                    PNS.Bit63.L2.RestaurantRoomServiceChargesAmt = "Bit63.L2.RestaurantRoomServiceChargesAmt";
                    PNS.Bit63.L2.GiftShopChargesAmt = "Bit63.L2.GiftShopChargesAmt";
                    PNS.Bit63.L2.MiniBarChargesAmt = "Bit63.L2.MiniBarChargesAmt";
                    PNS.Bit63.L2.PhoneChargesAmt = "Bit63.L2.PhoneChargesAmt";
                    PNS.Bit63.L2.LaundryChargesAmt = "Bit63.L2.LaundryChargesAmt";
                    PNS.Bit63.L2.HealthClubChargesAmt = "Bit63.L2.HealthClubChargesAmt";
                    PNS.Bit63.L2.LoungeBarChargesAmt = "Bit63.L2.LoungeBarChargesAmt";
                    PNS.Bit63.L2.MovieChargesAmt = "Bit63.L2.MovieChargesAmt";
                    PNS.Bit63.L2.ValetParkingChargesAmt = "Bit63.L2.ValetParkingChargesAmt";
                    PNS.Bit63.L2.BusinessCenterChargesAmt = "Bit63.L2.BusinessCenterChargesAmt";
                    PNS.Bit63.L2.NonRoomChargesAmt = "Bit63.L2.NonRoomChargesAmt";
                    PNS.Bit63.L2.OtherServicesCode1 = "Bit63.L2.OtherServicesCode1";
                    PNS.Bit63.L2.OtherServicesAmt1 = "Bit63.L2.OtherServicesAmt1";
                    PNS.Bit63.L2.OtherServicesCode2 = "Bit63.L2.OtherServicesCode2";
                    PNS.Bit63.L2.OtherServicesAmt2 = "Bit63.L2.OtherServicesAmt2";
                    PNS.Bit63.L2.OtherServicesCode3 = "Bit63.L2.OtherServicesCode3";
                    PNS.Bit63.L2.OtherServicesAmt3 = "Bit63.L2.OtherServicesAmt3";
                    PNS.Bit63.L2.OtherServicesCode4 = "Bit63.L2.OtherServicesCode4";
                    PNS.Bit63.L2.OtherServicesAmt4 = "Bit63.L2.OtherServicesAmt4";
                    PNS.Bit63.L2.OtherServicesCode5 = "Bit63.L2.OtherServicesCode5";
                    PNS.Bit63.L2.OtherServicesAmt5 = "Bit63.L2.OtherServicesAmt5";
                    PNS.Bit63.L2.OtherServicesCode6 = "Bit63.L2.OtherServicesCode6";
                    PNS.Bit63.L2.OtherServicesAmt6 = "Bit63.L2.OtherServicesAmt6";
                }

                public L2()
                {
                }
            }

            public class L3
            {
                public readonly static string RequestorName;

                public readonly static string CustomerVATRegistrationNumber;

                public readonly static string MerchantVATRegistrationNumber;

                public readonly static string PSTTaxRegistrationNumber;

                public readonly static string LocalTaxRate;

                public readonly static string NationalTax;

                static L3()
                {
                    PNS.Bit63.L3.RequestorName = "Bit63.L3.RequestorName";
                    PNS.Bit63.L3.CustomerVATRegistrationNumber = "Bit63.L3.CustomerVATRegistrationNumber";
                    PNS.Bit63.L3.MerchantVATRegistrationNumber = "Bit63.L3.MerchantVATRegistrationNumber";
                    PNS.Bit63.L3.PSTTaxRegistrationNumber = "Bit63.L3.PSTTaxRegistrationNumber";
                    PNS.Bit63.L3.LocalTaxRate = "Bit63.L3.LocalTaxRate";
                    PNS.Bit63.L3.NationalTax = "Bit63.L3.NationalTax";
                }

                public L3()
                {
                }
            }

            public class M1
            {
                public readonly static string OrderNumber;

                public readonly static string TypeIndicator;

                static M1()
                {
                    PNS.Bit63.M1.OrderNumber = "Bit63.M1.OrderNumber";
                    PNS.Bit63.M1.TypeIndicator = "Bit63.M1.TypeIndicator";
                }

                public M1()
                {
                }
            }

            public class P1
            {
                public readonly static string ProductCode1;

                public readonly static string ProductCode2;

                public readonly static string ProductCode3;

                public readonly static string DollarAmount1;

                public readonly static string DollarAmount2;

                public readonly static string DollarAmount3;

                public readonly static string FuelUnitPrice;

                public readonly static string FuelGallons;

                public readonly static string FuelFullServiceIndicator;

                static P1()
                {
                    PNS.Bit63.P1.ProductCode1 = "Bit63.P1.ProductCode1";
                    PNS.Bit63.P1.ProductCode2 = "Bit63.P1.ProductCode2";
                    PNS.Bit63.P1.ProductCode3 = "Bit63.P1.ProductCode3";
                    PNS.Bit63.P1.DollarAmount1 = "Bit63.P1.DollarAmount1";
                    PNS.Bit63.P1.DollarAmount2 = "Bit63.P1.DollarAmount2";
                    PNS.Bit63.P1.DollarAmount3 = "Bit63.P1.DollarAmount3";
                    PNS.Bit63.P1.FuelUnitPrice = "Bit63.P1.FuelUnitPrice";
                    PNS.Bit63.P1.FuelGallons = "Bit63.P1.FuelGallons";
                    PNS.Bit63.P1.FuelFullServiceIndicator = "Bit63.P1.FuelFullServiceIndicator";
                }

                public P1()
                {
                }
            }

            public class P2
            {
                public readonly static string NumberOfProducts;

                static P2()
                {
                    PNS.Bit63.P2.NumberOfProducts = "Bit63.P2.NumberOfProducts";
                }

                public P2()
                {
                }

                public class ProductInformation
                {
                    public readonly static string ProductCode;

                    public readonly static string ProductAmount;

                    public readonly static string UnitPrice;

                    public readonly static string Quantity;

                    public readonly static string UnitOfMeasure;

                    static ProductInformation()
                    {
                        PNS.Bit63.P2.ProductInformation.ProductCode = "Bit63.P2.ProductInformation.ProductCode";
                        PNS.Bit63.P2.ProductInformation.ProductAmount = "Bit63.P2.ProductInformation.ProductAmount";
                        PNS.Bit63.P2.ProductInformation.UnitPrice = "Bit63.P2.ProductInformation.UnitPrice";
                        PNS.Bit63.P2.ProductInformation.Quantity = "Bit63.P2.ProductInformation.Quantity";
                        PNS.Bit63.P2.ProductInformation.UnitOfMeasure = "Bit63.P2.ProductInformation.UnitOfMeasure";
                    }

                    public ProductInformation()
                    {
                    }
                }
            }

            public class P3
            {
                public readonly static string FuelServiceType;

                public readonly static string NumberOfProducts;

                static P3()
                {
                    PNS.Bit63.P3.FuelServiceType = "Bit63.P3.FuelServiceType";
                    PNS.Bit63.P3.NumberOfProducts = "Bit63.P3.NumberOfProducts";
                }

                public P3()
                {
                }

                public class ProductInformation
                {
                    public readonly static string ProductCode;

                    public readonly static string TaxAmount;

                    public readonly static string DiscountAmount;

                    public readonly static string MiscellaneousTaxAmount;

                    public readonly static string MiscellaneousTaxExemptionFlag;

                    public readonly static string VATTaxRate;

                    static ProductInformation()
                    {
                        PNS.Bit63.P3.ProductInformation.ProductCode = "Bit63.P3.ProductInformation.ProductCode";
                        PNS.Bit63.P3.ProductInformation.TaxAmount = "Bit63.P3.ProductInformation.TaxAmount";
                        PNS.Bit63.P3.ProductInformation.DiscountAmount = "Bit63.P3.ProductInformation.DiscountAmount";
                        PNS.Bit63.P3.ProductInformation.MiscellaneousTaxAmount = "Bit63.P3.ProductInformation.MiscellaneousTaxAmount";
                        PNS.Bit63.P3.ProductInformation.MiscellaneousTaxExemptionFlag = "Bit63.P3.ProductInformation.MiscellaneousTaxExemptionFlag";
                        PNS.Bit63.P3.ProductInformation.VATTaxRate = "Bit63.P3.ProductInformation.VATTaxRate";
                    }

                    public ProductInformation()
                    {
                    }
                }
            }

            public class R1
            {
                public readonly static string ReferenceCode;

                public readonly static string TipAmount;

                public readonly static string ServerNumber;

                public readonly static string TaxAmount;

                static R1()
                {
                    PNS.Bit63.R1.ReferenceCode = "Bit63.R1.ReferenceCode";
                    PNS.Bit63.R1.TipAmount = "Bit63.R1.TipAmount";
                    PNS.Bit63.R1.ServerNumber = "Bit63.R1.ServerNumber";
                    PNS.Bit63.R1.TaxAmount = "Bit63.R1.TaxAmount";
                }

                public R1()
                {
                }
            }

            public class R2
            {
                public readonly static string InvoiceNumber;

                public readonly static string TranInformation;

                static R2()
                {
                    PNS.Bit63.R2.InvoiceNumber = "Bit63.R2.InvoiceNumber";
                    PNS.Bit63.R2.TranInformation = "Bit63.R2.TranInformation";
                }

                public R2()
                {
                }
            }

            public class SV
            {
                public readonly static string ExternalTransactionIdentifier;

                public readonly static string EmployeeNumber;

                public readonly static string CashOutIndicator;

                public readonly static string SequenceNumberOfCardBeingUsed;

                public readonly static string TotalNumberOfCardsBeingIssued;

                static SV()
                {
                    PNS.Bit63.SV.ExternalTransactionIdentifier = "Bit63.SV.ExternalTransactionIdentifier";
                    PNS.Bit63.SV.EmployeeNumber = "Bit63.SV.EmployeeNumber";
                    PNS.Bit63.SV.CashOutIndicator = "Bit63.SV.CashOutIndicator";
                    PNS.Bit63.SV.SequenceNumberOfCardBeingUsed = "Bit63.SV.SequenceNumberOfCardBeingUsed";
                    PNS.Bit63.SV.TotalNumberOfCardsBeingIssued = "Bit63.SV.TotalNumberOfCardsBeingIssued";
                }

                public SV()
                {
                }
            }

            public class SV1
            {
                public readonly static string ExternalTransactionIdentifier;

                public readonly static string EmployeeNumber;

                public readonly static string CashOutIndicator;

                public readonly static string TotalCardsActivatedInBlock;

                static SV1()
                {
                    PNS.Bit63.SV1.ExternalTransactionIdentifier = "Bit63.SV1.ExternalTransactionIdentifier";
                    PNS.Bit63.SV1.EmployeeNumber = "Bit63.SV1.EmployeeNumber";
                    PNS.Bit63.SV1.CashOutIndicator = "Bit63.SV1.CashOutIndicator";
                    PNS.Bit63.SV1.TotalCardsActivatedInBlock = "Bit63.SV1.TotalCardsActivatedInBlock";
                }

                public SV1()
                {
                }
            }
        }

        public class Bit70
        {
            public readonly static string NetworkManagementInformationCode;

            static Bit70()
            {
                PNS.Bit70.NetworkManagementInformationCode = "Bit70.NetworkManagementInformationCode";
            }

            public Bit70()
            {
            }
        }

        public class Bit71
        {
            public readonly static string MessageNumberSecurityMessageCounter;

            static Bit71()
            {
                PNS.Bit71.MessageNumberSecurityMessageCounter = "Bit71.MessageNumberSecurityMessageCounter";
            }

            public Bit71()
            {
            }
        }

        public class Bit8
        {
            public readonly static string AmtCardholderBillingFee;

            static Bit8()
            {
                PNS.Bit8.AmtCardholderBillingFee = "Bit8.AmtCardholderBillingFee";
            }

            public Bit8()
            {
            }
        }

        public class Bit90
        {
            public readonly static string OriginalMessageType;

            public readonly static string OriginalTraceNumber;

            public readonly static string OriginalTransmissionDateAndTime;

            static Bit90()
            {
                PNS.Bit90.OriginalMessageType = "Bit90.OriginalMessageType";
                PNS.Bit90.OriginalTraceNumber = "Bit90.OriginalTraceNumber";
                PNS.Bit90.OriginalTransmissionDateAndTime = "Bit90.OriginalTransmissionDateAndTime";
            }

            public Bit90()
            {
            }
        }

        public class MessageHeader
        {
            public readonly static string RoutingInformation;

            public readonly static string MultitranIndicator;

            public readonly static string MultiBatchFlag;

            public readonly static string MessageType;

            static MessageHeader()
            {
                PNS.MessageHeader.RoutingInformation = "MessageHeader.RoutingInformation";
                PNS.MessageHeader.MultitranIndicator = "MessageHeader.MultitranIndicator";
                PNS.MessageHeader.MultiBatchFlag = "MessageHeader.MultiBatchFlag";
                PNS.MessageHeader.MessageType = "MessageHeader.MessageType";
            }

            public MessageHeader()
            {
            }
        }
    }
}