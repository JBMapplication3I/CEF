#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK
{
    public class PNSResp
    {
        public PNSResp()
        {
        }

        public class Bit11
        {
            public readonly static string SystemTraceAuditNumber;

            static Bit11()
            {
                PNSResp.Bit11.SystemTraceAuditNumber = "Bit11.SystemTraceAuditNumber";
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
                PNSResp.Bit12.TimeLocalTrans = "Bit12.TimeLocalTrans";
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
                PNSResp.Bit13.DateLocalTrans = "Bit13.DateLocalTrans";
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
                PNSResp.Bit14.DateExpire = "Bit14.DateExpire";
            }

            public Bit14()
            {
            }
        }

        public class Bit15
        {
            public readonly static string DateSettlement;

            static Bit15()
            {
                PNSResp.Bit15.DateSettlement = "Bit15.DateSettlement";
            }

            public Bit15()
            {
            }
        }

        public class Bit18
        {
            public readonly static string MerchantCategoryCode;

            public readonly static string MerchantType;

            static Bit18()
            {
                PNSResp.Bit18.MerchantCategoryCode = "Bit18.MerchantCategoryCode";
                PNSResp.Bit18.MerchantType = "Bit18.MerchantType";
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
                PNSResp.Bit2.PrimaryAccountNumber = "Bit2.PrimaryAccountNumber";
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
                PNSResp.Bit22.POSEntryMode = "Bit22.POSEntryMode";
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
                PNSResp.Bit23.EMVContactlessPANSeqNumber = "Bit23.EMVContactlessPANSeqNumber";
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
                PNSResp.Bit25.POSConditionCode = "Bit25.POSConditionCode";
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
                PNSResp.Bit3.TransactionType = "Bit3.TransactionType";
                PNSResp.Bit3.AccountTypeFrom = "Bit3.AccountTypeFrom";
                PNSResp.Bit3.AccountTypeTo = "Bit3.AccountTypeTo";
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
                PNSResp.Bit35.Track2Data = "Bit35.Track2Data";
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
                PNSResp.Bit37.RetrievalReferenceNumber = "Bit37.RetrievalReferenceNumber";
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
                PNSResp.Bit38.AuthorizationIDResponse = "Bit38.AuthorizationIDResponse";
            }

            public Bit38()
            {
            }
        }

        public class Bit39
        {
            public readonly static string ResponseCode;

            static Bit39()
            {
                PNSResp.Bit39.ResponseCode = "Bit39.ResponseCode";
            }

            public Bit39()
            {
            }
        }

        public class Bit4
        {
            public readonly static string TransactionAmount;

            static Bit4()
            {
                PNSResp.Bit4.TransactionAmount = "Bit4.TransactionAmount";
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
                PNSResp.Bit41.CardAcquirerTerminalId = "Bit41.CardAcquirerTerminalId";
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
                PNSResp.Bit42.CardAcquirerId = "Bit42.CardAcquirerId";
            }

            public Bit42()
            {
            }
        }

        public class Bit44
        {
            public Bit44()
            {
            }

            public class Default
            {
                public readonly static string Data;

                static Default()
                {
                    PNSResp.Bit44.Default.Data = "Bit44.Default.Data";
                }

                public Default()
                {
                }
            }

            public class P1
            {
                public readonly static string ApplicationIdentifier;

                public readonly static string KeyIndex;

                public readonly static string KeyModulus;

                public readonly static string KeyExponent;

                public readonly static string KeyChecksum;

                static P1()
                {
                    PNSResp.Bit44.P1.ApplicationIdentifier = "Bit44.P1.ApplicationIdentifier";
                    PNSResp.Bit44.P1.KeyIndex = "Bit44.P1.KeyIndex";
                    PNSResp.Bit44.P1.KeyModulus = "Bit44.P1.KeyModulus";
                    PNSResp.Bit44.P1.KeyExponent = "Bit44.P1.KeyExponent";
                    PNSResp.Bit44.P1.KeyChecksum = "Bit44.P1.KeyChecksum";
                }

                public P1()
                {
                }
            }
        }

        public class Bit45
        {
            public readonly static string Track1Data;

            static Bit45()
            {
                PNSResp.Bit45.Track1Data = "Bit45.Track1Data";
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
                    PNSResp.Bit48.A1.CardholderZipCode = "Bit48.A1.CardholderZipCode";
                    PNSResp.Bit48.A1.CardholderAddress = "Bit48.A1.CardholderAddress";
                }

                public A1()
                {
                }
            }

            public class A2
            {
                public readonly static string AVSResponse;

                static A2()
                {
                    PNSResp.Bit48.A2.AVSResponse = "Bit48.A2.AVSResponse";
                }

                public A2()
                {
                }
            }

            public class A3
            {
                public readonly static string TransactionDataTypeIndicator;

                static A3()
                {
                    PNSResp.Bit48.A3.TransactionDataTypeIndicator = "Bit48.A3.TransactionDataTypeIndicator";
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
                    PNSResp.Bit48.A4.AccountType = "Bit48.A4.AccountType";
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
                    PNSResp.Bit48.A5.TransactionIdentifier = "Bit48.A5.TransactionIdentifier";
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
                    PNSResp.Bit48.A6.POSDataCodeResponse = "Bit48.A6.POSDataCodeResponse";
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
                    PNSResp.Bit48.A7.EMVContactlessAID = "Bit48.A7.EMVContactlessAID";
                }

                public A7()
                {
                }
            }

            public class AD
            {
                public readonly static string AEVVBlockA;

                public readonly static string AmexConsumerDigitalPaymentTokenCryptogramBlockA;

                public readonly static string XIDBlockB;

                public readonly static string AmexConsumerDigitalPaymentTokenCryptogramBlockB;

                static AD()
                {
                    PNSResp.Bit48.AD.AEVVBlockA = "Bit48.AD.AEVVBlockA";
                    PNSResp.Bit48.AD.AmexConsumerDigitalPaymentTokenCryptogramBlockA = "Bit48.AD.AmexConsumerDigitalPaymentTokenCryptogramBlockA";
                    PNSResp.Bit48.AD.XIDBlockB = "Bit48.AD.XIDBlockB";
                    PNSResp.Bit48.AD.AmexConsumerDigitalPaymentTokenCryptogramBlockB = "Bit48.AD.AmexConsumerDigitalPaymentTokenCryptogramBlockB";
                }

                public AD()
                {
                }
            }

            public class B1
            {
                public readonly static string NumberOfBalanceAmounts;

                static B1()
                {
                    PNSResp.Bit48.B1.NumberOfBalanceAmounts = "Bit48.B1.NumberOfBalanceAmounts";
                }

                public B1()
                {
                }

                public class BalanceInformation
                {
                    public readonly static string StoredValueAccountBalanceIndicator;

                    public readonly static string BalanceAmount;

                    static BalanceInformation()
                    {
                        PNSResp.Bit48.B1.BalanceInformation.StoredValueAccountBalanceIndicator = "Bit48.B1.BalanceInformation.StoredValueAccountBalanceIndicator";
                        PNSResp.Bit48.B1.BalanceInformation.BalanceAmount = "Bit48.B1.BalanceInformation.BalanceAmount";
                    }

                    public BalanceInformation()
                    {
                    }
                }
            }

            public class B3
            {
                public readonly static string NumberOfBalanceAmounts;

                static B3()
                {
                    PNSResp.Bit48.B3.NumberOfBalanceAmounts = "Bit48.B3.NumberOfBalanceAmounts";
                }

                public B3()
                {
                }

                public class BalanceInformation
                {
                    public readonly static string CreditAccountBalanceIndicator;

                    public readonly static string CurrencyCode;

                    public readonly static string BalanceAmount;

                    static BalanceInformation()
                    {
                        PNSResp.Bit48.B3.BalanceInformation.CreditAccountBalanceIndicator = "Bit48.B3.BalanceInformation.CreditAccountBalanceIndicator";
                        PNSResp.Bit48.B3.BalanceInformation.CurrencyCode = "Bit48.B3.BalanceInformation.CurrencyCode";
                        PNSResp.Bit48.B3.BalanceInformation.BalanceAmount = "Bit48.B3.BalanceInformation.BalanceAmount";
                    }

                    public BalanceInformation()
                    {
                    }
                }
            }

            public class BA
            {
                public readonly static string BusinessApplicationIdentifier;

                static BA()
                {
                    PNSResp.Bit48.BA.BusinessApplicationIdentifier = "Bit48.BA.BusinessApplicationIdentifier";
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
                    PNSResp.Bit48.BT.InquiryType = "Bit48.BT.InquiryType";
                    PNSResp.Bit48.BT.CardTypeRequested = "Bit48.BT.CardTypeRequested";
                }

                public BT()
                {
                }
            }

            public class C1
            {
                public readonly static string CardholderVerificationData;

                static C1()
                {
                    PNSResp.Bit48.C1.CardholderVerificationData = "Bit48.C1.CardholderVerificationData";
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
                    PNSResp.Bit48.C2.CVV2CIDPresenceIndicator = "Bit48.C2.CVV2CIDPresenceIndicator";
                }

                public C2()
                {
                }
            }

            public class C3
            {
                public readonly static string VerificationResponse;

                static C3()
                {
                    PNSResp.Bit48.C3.VerificationResponse = "Bit48.C3.VerificationResponse";
                }

                public C3()
                {
                }
            }

            public class C6
            {
                public readonly static string TPK;

                public readonly static string TAK;

                public readonly static string ForcedCurrentKeyRequest;

                static C6()
                {
                    PNSResp.Bit48.C6.TPK = "Bit48.C6.TPK";
                    PNSResp.Bit48.C6.TAK = "Bit48.C6.TAK";
                    PNSResp.Bit48.C6.ForcedCurrentKeyRequest = "Bit48.C6.ForcedCurrentKeyRequest";
                }

                public C6()
                {
                }
            }

            public class C7
            {
                public readonly static string EMVPOSParameterDownloadRequestStatus;

                static C7()
                {
                    PNSResp.Bit48.C7.EMVPOSParameterDownloadRequestStatus = "Bit48.C7.EMVPOSParameterDownloadRequestStatus";
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
                    PNSResp.Bit48.C8.CreditDebitPrepaidSupportIndicator = "Bit48.C8.CreditDebitPrepaidSupportIndicator";
                }

                public C8()
                {
                }
            }

            public class C9
            {
                public readonly static string VerificationResponse;

                static C9()
                {
                    PNSResp.Bit48.C9.VerificationResponse = "Bit48.C9.VerificationResponse";
                }

                public C9()
                {
                }
            }

            public class CA
            {
                public readonly static string CheckAuthorizationType;

                static CA()
                {
                    PNSResp.Bit48.CA.CheckAuthorizationType = "Bit48.CA.CheckAuthorizationType";
                }

                public CA()
                {
                }
            }

            public class CC
            {
                public readonly static string MICROrDriversLicense;

                static CC()
                {
                    PNSResp.Bit48.CC.MICROrDriversLicense = "Bit48.CC.MICROrDriversLicense";
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
                    PNSResp.Bit48.CF.POSEntryModeCredentialOnFile = "Bit48.CF.POSEntryModeCredentialOnFile";
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
                    PNSResp.Bit48.CG.ChasePayCryptogram = "Bit48.CG.ChasePayCryptogram";
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
                    PNSResp.Bit48.D1.DataEntrySource = "Bit48.D1.DataEntrySource";
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
                    PNSResp.Bit48.D2.TraceNumber = "Bit48.D2.TraceNumber";
                }

                public D2()
                {
                }
            }

            public class D3
            {
                public readonly static string DebitNetworkID;

                static D3()
                {
                    PNSResp.Bit48.D3.DebitNetworkID = "Bit48.D3.DebitNetworkID";
                }

                public D3()
                {
                }
            }

            public class D4
            {
                public readonly static string BillingAccountNumber;

                static D4()
                {
                    PNSResp.Bit48.D4.BillingAccountNumber = "Bit48.D4.BillingAccountNumber";
                }

                public D4()
                {
                }
            }

            public class D5
            {
                public readonly static string DCCResponseCode;

                public readonly static string DCCCheckValue;

                public readonly static string DCCRequest;

                public readonly static string DCCAmount;

                public readonly static string DCCMinorUnit;

                public readonly static string DCCExchangeRate;

                public readonly static string DCCStrCurrencyCode;

                public readonly static string DCCIntCurrencyCode;

                static D5()
                {
                    PNSResp.Bit48.D5.DCCResponseCode = "Bit48.D5.DCCResponseCode";
                    PNSResp.Bit48.D5.DCCCheckValue = "Bit48.D5.DCCCheckValue";
                    PNSResp.Bit48.D5.DCCRequest = "Bit48.D5.DCCRequest";
                    PNSResp.Bit48.D5.DCCAmount = "Bit48.D5.DCCAmount";
                    PNSResp.Bit48.D5.DCCMinorUnit = "Bit48.D5.DCCMinorUnit";
                    PNSResp.Bit48.D5.DCCExchangeRate = "Bit48.D5.DCCExchangeRate";
                    PNSResp.Bit48.D5.DCCStrCurrencyCode = "Bit48.D5.DCCStrCurrencyCode";
                    PNSResp.Bit48.D5.DCCIntCurrencyCode = "Bit48.D5.DCCIntCurrencyCode";
                }

                public D5()
                {
                }
            }

            public class D6
            {
                public readonly static string DuplicateTransactionCheckingIndicator;

                static D6()
                {
                    PNSResp.Bit48.D6.DuplicateTransactionCheckingIndicator = "Bit48.D6.DuplicateTransactionCheckingIndicator";
                }

                public D6()
                {
                }
            }

            public class D7
            {
                public readonly static string SoftDescriptor;

                static D7()
                {
                    PNSResp.Bit48.D7.SoftDescriptor = "Bit48.D7.SoftDescriptor";
                }

                public D7()
                {
                }
            }

            public class D8
            {
                public readonly static string EMVParameterDownloadFlag;

                static D8()
                {
                    PNSResp.Bit48.D8.EMVParameterDownloadFlag = "Bit48.D8.EMVParameterDownloadFlag";
                }

                public D8()
                {
                }
            }

            public class D9
            {
                public readonly static string EMVParameterDownloadResponseStatusIndicator;

                static D9()
                {
                    PNSResp.Bit48.D9.EMVParameterDownloadResponseStatusIndicator = "Bit48.D9.EMVParameterDownloadResponseStatusIndicator";
                }

                public D9()
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
                    PNSResp.Bit48.DA.ProcessingCode = "Bit48.DA.ProcessingCode";
                    PNSResp.Bit48.DA.SystemTraceAuditNumber = "Bit48.DA.SystemTraceAuditNumber";
                    PNSResp.Bit48.DA.POSEntryMode = "Bit48.DA.POSEntryMode";
                    PNSResp.Bit48.DA.PINCapabilityCode = "Bit48.DA.PINCapabilityCode";
                    PNSResp.Bit48.DA.TrackDataConditionCode = "Bit48.DA.TrackDataConditionCode";
                    PNSResp.Bit48.DA.AVSResponseCode = "Bit48.DA.AVSResponseCode";
                    PNSResp.Bit48.DA.CIDResponseCode = "Bit48.DA.CIDResponseCode";
                    PNSResp.Bit48.DA.AuthAmount = "Bit48.DA.AuthAmount";
                }

                public DA()
                {
                }
            }

            public class Default
            {
                public readonly static string Data;

                static Default()
                {
                    PNSResp.Bit48.Default.Data = "Bit48.Default.Data";
                }

                public Default()
                {
                }
            }

            public class DW
            {
                public readonly static string SDWOIndicator;

                static DW()
                {
                    PNSResp.Bit48.DW.SDWOIndicator = "Bit48.DW.SDWOIndicator";
                }

                public DW()
                {
                }
            }

            public class E1
            {
                public readonly static string CustomerReferenceNumber;

                static E1()
                {
                    PNSResp.Bit48.E1.CustomerReferenceNumber = "Bit48.E1.CustomerReferenceNumber";
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
                    PNSResp.Bit48.E2.LocalTaxFlag = "Bit48.E2.LocalTaxFlag";
                    PNSResp.Bit48.E2.LocalTaxAmount = "Bit48.E2.LocalTaxAmount";
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
                    PNSResp.Bit48.E3.DestinationZipCode = "Bit48.E3.DestinationZipCode";
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
                    PNSResp.Bit48.E4.TypeCode = "Bit48.E4.TypeCode";
                    PNSResp.Bit48.E4.VoucherNumber = "Bit48.E4.VoucherNumber";
                }

                public E4()
                {
                }
            }

            public class E5
            {
                public readonly static string NumberOfBalanceAmounts;

                static E5()
                {
                    PNSResp.Bit48.E5.NumberOfBalanceAmounts = "Bit48.E5.NumberOfBalanceAmounts";
                }

                public E5()
                {
                }

                public class BalanceInformationElement
                {
                    public readonly static string EBTAccountBalanceIndicator;

                    public readonly static string BalanceAmount;

                    static BalanceInformationElement()
                    {
                        PNSResp.Bit48.E5.BalanceInformationElement.EBTAccountBalanceIndicator = "Bit48.E5.BalanceInformationElement.EBTAccountBalanceIndicator";
                        PNSResp.Bit48.E5.BalanceInformationElement.BalanceAmount = "Bit48.E5.BalanceInformationElement.BalanceAmount";
                    }

                    public BalanceInformationElement()
                    {
                    }
                }
            }

            public class E6
            {
                public readonly static string EncryptedKeyIndex;

                static E6()
                {
                    PNSResp.Bit48.E6.EncryptedKeyIndex = "Bit48.E6.EncryptedKeyIndex";
                }

                public E6()
                {
                }
            }

            public class E7
            {
                public readonly static string DebitIssuerData;

                static E7()
                {
                    PNSResp.Bit48.E7.DebitIssuerData = "Bit48.E7.DebitIssuerData";
                }

                public E7()
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
                    PNSResp.Bit48.E8.FreightAmountIndicator = "Bit48.E8.FreightAmountIndicator";
                    PNSResp.Bit48.E8.FreightAmount = "Bit48.E8.FreightAmount";
                    PNSResp.Bit48.E8.DutyAmountIndicator = "Bit48.E8.DutyAmountIndicator";
                    PNSResp.Bit48.E8.DutyAmount = "Bit48.E8.DutyAmount";
                    PNSResp.Bit48.E8.DestinationCountryCode = "Bit48.E8.DestinationCountryCode";
                    PNSResp.Bit48.E8.ShipFromZip = "Bit48.E8.ShipFromZip";
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
                    PNSResp.Bit48.E9.ECPAuthorizationMethod = "Bit48.E9.ECPAuthorizationMethod";
                    PNSResp.Bit48.E9.PreferredDeliveryMethod = "Bit48.E9.PreferredDeliveryMethod";
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
                    PNSResp.Bit48.EA.ECPActionCode = "Bit48.EA.ECPActionCode";
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
                    PNSResp.Bit48.EI.ECommerceIndicator = "Bit48.EI.ECommerceIndicator";
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
                    PNSResp.Bit48.F1.FleetDataIndicator = "Bit48.F1.FleetDataIndicator";
                    PNSResp.Bit48.F1.VehicleNumber = "Bit48.F1.VehicleNumber";
                    PNSResp.Bit48.F1.Odometer = "Bit48.F1.Odometer";
                    PNSResp.Bit48.F1.DriverNumberJobNumber = "Bit48.F1.DriverNumberJobNumber";
                    PNSResp.Bit48.F1.PINBlockFleetCorDriverID = "Bit48.F1.PINBlockFleetCorDriverID";
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
                    PNSResp.Bit48.F2.FleetDataIndicator = "Bit48.F2.FleetDataIndicator";
                    PNSResp.Bit48.F2.NumberOfPromptCodesPresent = "Bit48.F2.NumberOfPromptCodesPresent";
                    PNSResp.Bit48.F2.PurchaseDeviceSequenceNumber = "Bit48.F2.PurchaseDeviceSequenceNumber";
                    PNSResp.Bit48.F2.TicketNumber = "Bit48.F2.TicketNumber";
                    PNSResp.Bit48.F2.StorenForwardIndicator = "Bit48.F2.StorenForwardIndicator";
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
                    PNSResp.Bit48.FE.AccountFundingTransactionForeignExchangeMarkUpFee = "Bit48.FE.AccountFundingTransactionForeignExchangeMarkUpFee";
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
                    PNSResp.Bit48.G1.MethodOfPayment = "Bit48.G1.MethodOfPayment";
                }

                public G1()
                {
                }
            }

            public class I1
            {
                public readonly static string ImageReferenceNumber;

                static I1()
                {
                    PNSResp.Bit48.I1.ImageReferenceNumber = "Bit48.I1.ImageReferenceNumber";
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
                    PNSResp.Bit48.I2.InStorePaymentFlag = "Bit48.I2.InStorePaymentFlag";
                }

                public I2()
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
                    PNSResp.Bit48.M1.MasterCardInterchangeComplianceIndicator = "Bit48.M1.MasterCardInterchangeComplianceIndicator";
                    PNSResp.Bit48.M1.BankNetReferenceNumber = "Bit48.M1.BankNetReferenceNumber";
                    PNSResp.Bit48.M1.BankNetDate = "Bit48.M1.BankNetDate";
                    PNSResp.Bit48.M1.CVCErrorIndicator = "Bit48.M1.CVCErrorIndicator";
                    PNSResp.Bit48.M1.POSValidationCode = "Bit48.M1.POSValidationCode";
                    PNSResp.Bit48.M1.MagStripeQualityIndicator = "Bit48.M1.MagStripeQualityIndicator";
                }

                public M1()
                {
                }
            }

            public class M2
            {
                public readonly static string MCPurchasingCardFlag;

                static M2()
                {
                    PNSResp.Bit48.M2.MCPurchasingCardFlag = "Bit48.M2.MCPurchasingCardFlag";
                }

                public M2()
                {
                }
            }

            public class M3
            {
                public readonly static string MACValue;

                static M3()
                {
                    PNSResp.Bit48.M3.MACValue = "Bit48.M3.MACValue";
                }

                public M3()
                {
                }
            }

            public class M5
            {
                public readonly static string ClassificationValue;

                static M5()
                {
                    PNSResp.Bit48.M5.ClassificationValue = "Bit48.M5.ClassificationValue";
                }

                public M5()
                {
                }
            }

            public class MM
            {
                public readonly static string MemberToMemberIdentifier;

                public readonly static string MemberToMemberData;

                static MM()
                {
                    PNSResp.Bit48.MM.MemberToMemberIdentifier = "Bit48.MM.MemberToMemberIdentifier";
                    PNSResp.Bit48.MM.MemberToMemberData = "Bit48.MM.MemberToMemberData";
                }

                public MM()
                {
                }
            }

            public class MU
            {
                public readonly static string SubmittedTransactionIdentifier;

                static MU()
                {
                    PNSResp.Bit48.MU.SubmittedTransactionIdentifier = "Bit48.MU.SubmittedTransactionIdentifier";
                }

                public MU()
                {
                }
            }

            public class MW
            {
                public readonly static string MobileWalletIdentifier;

                public readonly static string AdditionalWalletData;

                static MW()
                {
                    PNSResp.Bit48.MW.MobileWalletIdentifier = "Bit48.MW.MobileWalletIdentifier";
                    PNSResp.Bit48.MW.AdditionalWalletData = "Bit48.MW.AdditionalWalletData";
                }

                public MW()
                {
                }
            }

            public class N1
            {
                public readonly static string Name;

                static N1()
                {
                    PNSResp.Bit48.N1.Name = "Bit48.N1.Name";
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
                    PNSResp.Bit48.OP.OperatorID = "Bit48.OP.OperatorID";
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
                    PNSResp.Bit48.OS.OperatorIDSupervisor = "Bit48.OS.OperatorIDSupervisor";
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
                    PNSResp.Bit48.P1.PartialAuthorizationRequested = "Bit48.P1.PartialAuthorizationRequested";
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
                    PNSResp.Bit48.P2.DevicePumpLaneIdentifier = "Bit48.P2.DevicePumpLaneIdentifier";
                }

                public P2()
                {
                }
            }

            public class P3
            {
                public readonly static string PINPadSerialNumber;

                static P3()
                {
                    PNSResp.Bit48.P3.PINPadSerialNumber = "Bit48.P3.PINPadSerialNumber";
                }

                public P3()
                {
                }
            }

            public class P5
            {
                public readonly static string ApplicationIdentifier;

                public readonly static string FallBackIndicator;

                static P5()
                {
                    PNSResp.Bit48.P5.ApplicationIdentifier = "Bit48.P5.ApplicationIdentifier";
                    PNSResp.Bit48.P5.FallBackIndicator = "Bit48.P5.FallBackIndicator";
                }

                public P5()
                {
                }
            }

            public class P6
            {
                public readonly static string ApplicationIdentifier;

                public readonly static string BiasedRandomSelectionLimit;

                public readonly static string OfflineFloorLimit;

                static P6()
                {
                    PNSResp.Bit48.P6.ApplicationIdentifier = "Bit48.P6.ApplicationIdentifier";
                    PNSResp.Bit48.P6.BiasedRandomSelectionLimit = "Bit48.P6.BiasedRandomSelectionLimit";
                    PNSResp.Bit48.P6.OfflineFloorLimit = "Bit48.P6.OfflineFloorLimit";
                }

                public P6()
                {
                }
            }

            public class P7
            {
                public readonly static string PartiallyApprovedTransactionAmount;

                static P7()
                {
                    PNSResp.Bit48.P7.PartiallyApprovedTransactionAmount = "Bit48.P7.PartiallyApprovedTransactionAmount";
                }

                public P7()
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
                    PNSResp.Bit48.PI.PaymentIndicator = "Bit48.PI.PaymentIndicator";
                    PNSResp.Bit48.PI.DateOfBirth = "Bit48.PI.DateOfBirth";
                    PNSResp.Bit48.PI.MaskedAccountNumber = "Bit48.PI.MaskedAccountNumber";
                    PNSResp.Bit48.PI.PartialPostalCode = "Bit48.PI.PartialPostalCode";
                    PNSResp.Bit48.PI.LastName = "Bit48.PI.LastName";
                }

                public PI()
                {
                }
            }

            public class Q8
            {
                public readonly static string EnhancedAuthorizationResponseIndicator;

                public readonly static string CardType;

                static Q8()
                {
                    PNSResp.Bit48.Q8.EnhancedAuthorizationResponseIndicator = "Bit48.Q8.EnhancedAuthorizationResponseIndicator";
                    PNSResp.Bit48.Q8.CardType = "Bit48.Q8.CardType";
                }

                public Q8()
                {
                }
            }

            public class QS
            {
                public readonly static string RecordType;

                public readonly static string RequestedPaymentService;

                static QS()
                {
                    PNSResp.Bit48.QS.RecordType = "Bit48.QS.RecordType";
                    PNSResp.Bit48.QS.RequestedPaymentService = "Bit48.QS.RequestedPaymentService";
                }

                public QS()
                {
                }
            }

            public class R1
            {
                public readonly static string CustomerDefinedData;

                static R1()
                {
                    PNSResp.Bit48.R1.CustomerDefinedData = "Bit48.R1.CustomerDefinedData";
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
                    PNSResp.Bit48.R2.RecurringIndicator = "Bit48.R2.RecurringIndicator";
                    PNSResp.Bit48.R2.TransactionTypeIndicator = "Bit48.R2.TransactionTypeIndicator";
                    PNSResp.Bit48.R2.SubmissionType = "Bit48.R2.SubmissionType";
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
                    PNSResp.Bit48.R3.ReversalReasonCode = "Bit48.R3.ReversalReasonCode";
                }

                public R3()
                {
                }
            }

            public class R4
            {
                public readonly static string MerchantSpecificInformation;

                static R4()
                {
                    PNSResp.Bit48.R4.MerchantSpecificInformation = "Bit48.R4.MerchantSpecificInformation";
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
                    PNSResp.Bit48.R5.RequestStatus = "Bit48.R5.RequestStatus";
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
                    PNSResp.Bit48.R6.MoreDataFlag = "Bit48.R6.MoreDataFlag";
                }

                public R6()
                {
                }
            }

            public class R8
            {
                public readonly static string ReversalAmountPartial;

                static R8()
                {
                    PNSResp.Bit48.R8.ReversalAmountPartial = "Bit48.R8.ReversalAmountPartial";
                }

                public R8()
                {
                }
            }

            public class RP
            {
                public readonly static string RemotePaymentType;

                static RP()
                {
                    PNSResp.Bit48.RP.RemotePaymentType = "Bit48.RP.RemotePaymentType";
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
                    PNSResp.Bit48.S1.CardType = "Bit48.S1.CardType";
                }

                public S1()
                {
                }
            }

            public class S2
            {
                public readonly static string ActualResponseCode;

                public readonly static string AuthorizerResponseValues;

                public readonly static string AuthorizationNetworkID;

                static S2()
                {
                    PNSResp.Bit48.S2.ActualResponseCode = "Bit48.S2.ActualResponseCode";
                    PNSResp.Bit48.S2.AuthorizerResponseValues = "Bit48.S2.AuthorizerResponseValues";
                    PNSResp.Bit48.S2.AuthorizationNetworkID = "Bit48.S2.AuthorizationNetworkID";
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
                    PNSResp.Bit48.S3.PartialRedemptionRequested = "Bit48.S3.PartialRedemptionRequested";
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
                    PNSResp.Bit48.S4.ValueOfUCAFCollectionIndicator = "Bit48.S4.ValueOfUCAFCollectionIndicator";
                    PNSResp.Bit48.S4.SecureCodeResponseData = "Bit48.S4.SecureCodeResponseData";
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
                    PNSResp.Bit48.S5.SAFIndicator = "Bit48.S5.SAFIndicator";
                    PNSResp.Bit48.S5.DateTimeOfOriginalTransaction = "Bit48.S5.DateTimeOfOriginalTransaction";
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
                    PNSResp.Bit48.S6.OriginalRetrievalReferenceNumber = "Bit48.S6.OriginalRetrievalReferenceNumber";
                    PNSResp.Bit48.S6.OriginalTransmissionDateTime = "Bit48.S6.OriginalTransmissionDateTime";
                }

                public S6()
                {
                }
            }

            public class S7
            {
                public readonly static string ResubmissionIndicator;

                static S7()
                {
                    PNSResp.Bit48.S7.ResubmissionIndicator = "Bit48.S7.ResubmissionIndicator";
                }

                public S7()
                {
                }
            }

            public class S8
            {
                public readonly static string GiftCardTypeIndicator;

                public readonly static string ThirdPartyGiftCardTransactionInformation;

                static S8()
                {
                    PNSResp.Bit48.S8.GiftCardTypeIndicator = "Bit48.S8.GiftCardTypeIndicator";
                    PNSResp.Bit48.S8.ThirdPartyGiftCardTransactionInformation = "Bit48.S8.ThirdPartyGiftCardTransactionInformation";
                }

                public S8()
                {
                }
            }

            public class S9
            {
                public readonly static string SemtekCDSResultCode;

                public readonly static string SemtekDetailedResponseDescription;

                static S9()
                {
                    PNSResp.Bit48.S9.SemtekCDSResultCode = "Bit48.S9.SemtekCDSResultCode";
                    PNSResp.Bit48.S9.SemtekDetailedResponseDescription = "Bit48.S9.SemtekDetailedResponseDescription";
                }

                public S9()
                {
                }
            }

            public class SF
            {
                public readonly static string ServiceFee;

                static SF()
                {
                    PNSResp.Bit48.SF.ServiceFee = "Bit48.SF.ServiceFee";
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
                    PNSResp.Bit48.T1.TransactionStatus = "Bit48.T1.TransactionStatus";
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
                    PNSResp.Bit48.T2.EMVContactlessTransactionSequenceCounter = "Bit48.T2.EMVContactlessTransactionSequenceCounter";
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
                    PNSResp.Bit48.T3.IncomingAcctType = "Bit48.T3.IncomingAcctType";
                }

                public T3()
                {
                }
            }

            public class T4
            {
                public readonly static string TokenID;

                public readonly static string TokenResponseCode;

                public readonly static string TokenResponseDescription;

                static T4()
                {
                    PNSResp.Bit48.T4.TokenID = "Bit48.T4.TokenID";
                    PNSResp.Bit48.T4.TokenResponseCode = "Bit48.T4.TokenResponseCode";
                    PNSResp.Bit48.T4.TokenResponseDescription = "Bit48.T4.TokenResponseDescription";
                }

                public T4()
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
                    PNSResp.Bit48.TK.AccountStatus = "Bit48.TK.AccountStatus";
                    PNSResp.Bit48.TK.TokenAssuranceLevel = "Bit48.TK.TokenAssuranceLevel";
                    PNSResp.Bit48.TK.ConsumerDigitalPaymentTokenFlag = "Bit48.TK.ConsumerDigitalPaymentTokenFlag";
                    PNSResp.Bit48.TK.SubsequentFlag = "Bit48.TK.SubsequentFlag";
                    PNSResp.Bit48.TK.TokenRequestorID = "Bit48.TK.TokenRequestorID";
                }

                public TK()
                {
                }
            }

            public class U1
            {
                public readonly static string MerchantURL;

                static U1()
                {
                    PNSResp.Bit48.U1.MerchantURL = "Bit48.U1.MerchantURL";
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
                    PNSResp.Bit48.U2.TotalShipmentNumber = "Bit48.U2.TotalShipmentNumber";
                    PNSResp.Bit48.U2.SequenceNumber = "Bit48.U2.SequenceNumber";
                }

                public U2()
                {
                }
            }

            public class V1
            {
                public readonly static string AuthorizationCharacteristicsIndicator;

                public readonly static string TransactionID;

                static V1()
                {
                    PNSResp.Bit48.V1.AuthorizationCharacteristicsIndicator = "Bit48.V1.AuthorizationCharacteristicsIndicator";
                    PNSResp.Bit48.V1.TransactionID = "Bit48.V1.TransactionID";
                }

                public V1()
                {
                }
            }

            public class V2
            {
                public readonly static string ValidationCode;

                static V2()
                {
                    PNSResp.Bit48.V2.ValidationCode = "Bit48.V2.ValidationCode";
                }

                public V2()
                {
                }
            }

            public class V3
            {
                public readonly static string TotalAuthorizationAmount;

                static V3()
                {
                    PNSResp.Bit48.V3.TotalAuthorizationAmount = "Bit48.V3.TotalAuthorizationAmount";
                }

                public V3()
                {
                }
            }

            public class V4
            {
                public readonly static string MarketSpecificDataIndicator;

                public readonly static string Duration;

                static V4()
                {
                    PNSResp.Bit48.V4.MarketSpecificDataIndicator = "Bit48.V4.MarketSpecificDataIndicator";
                    PNSResp.Bit48.V4.Duration = "Bit48.V4.Duration";
                }

                public V4()
                {
                }
            }

            public class V5
            {
                public readonly static string DowngradeReason;

                static V5()
                {
                    PNSResp.Bit48.V5.DowngradeReason = "Bit48.V5.DowngradeReason";
                }

                public V5()
                {
                }
            }

            public class V6
            {
                public readonly static string OriginalAuthorizationAmount;

                static V6()
                {
                    PNSResp.Bit48.V6.OriginalAuthorizationAmount = "Bit48.V6.OriginalAuthorizationAmount";
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
                    PNSResp.Bit48.V7.CreditAuthorizationType = "Bit48.V7.CreditAuthorizationType";
                    PNSResp.Bit48.V7.AuthorizationType = "Bit48.V7.AuthorizationType";
                }

                public V7()
                {
                }
            }

            public class V8
            {
                public readonly static string CNandVisaCommercialCardIndicator;

                public readonly static string VisaCommercialCardIndicator;

                static V8()
                {
                    PNSResp.Bit48.V8.CNandVisaCommercialCardIndicator = "Bit48.V8.CNandVisaCommercialCardIndicator";
                    PNSResp.Bit48.V8.VisaCommercialCardIndicator = "Bit48.V8.VisaCommercialCardIndicator";
                }

                public V8()
                {
                }
            }

            public class V9
            {
                public readonly static string AuthorizationSource;

                static V9()
                {
                    PNSResp.Bit48.V9.AuthorizationSource = "Bit48.V9.AuthorizationSource";
                }

                public V9()
                {
                }
            }

            public class VA
            {
                public readonly static string VerifiedByVisaAuthenticationResponseData;

                static VA()
                {
                    PNSResp.Bit48.VA.VerifiedByVisaAuthenticationResponseData = "Bit48.VA.VerifiedByVisaAuthenticationResponseData";
                }

                public VA()
                {
                }
            }

            public class VB
            {
                public readonly static string VbVResponseCode;

                static VB()
                {
                    PNSResp.Bit48.VB.VbVResponseCode = "Bit48.VB.VbVResponseCode";
                }

                public VB()
                {
                }
            }

            public class VC
            {
                public readonly static string EMVContactlessCardAuthenticationResultsCode;

                static VC()
                {
                    PNSResp.Bit48.VC.EMVContactlessCardAuthenticationResultsCode = "Bit48.VC.EMVContactlessCardAuthenticationResultsCode";
                }

                public VC()
                {
                }
            }

            public class VD
            {
                public readonly static string CNandVisaCardLevelResultsCode;

                public readonly static string VisaCardLevelResultsCode;

                static VD()
                {
                    PNSResp.Bit48.VD.CNandVisaCardLevelResultsCode = "Bit48.VD.CNandVisaCardLevelResultsCode";
                    PNSResp.Bit48.VD.VisaCardLevelResultsCode = "Bit48.VD.VisaCardLevelResultsCode";
                }

                public VD()
                {
                }
            }

            public class VE
            {
                public readonly static string CNandVisaCVViCVVCAMResultsCode;

                public readonly static string VisaCVViCVVCAMResultsCode;

                static VE()
                {
                    PNSResp.Bit48.VE.CNandVisaCVViCVVCAMResultsCode = "Bit48.VE.CNandVisaCVViCVVCAMResultsCode";
                    PNSResp.Bit48.VE.VisaCVViCVVCAMResultsCode = "Bit48.VE.VisaCVViCVVCAMResultsCode";
                }

                public VE()
                {
                }
            }

            public class VS
            {
                public readonly static string SpendQualifiedResultCode;

                static VS()
                {
                    PNSResp.Bit48.VS.SpendQualifiedResultCode = "Bit48.VS.SpendQualifiedResultCode";
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
                    PNSResp.Bit48.Z1.KeyType = "Bit48.Z1.KeyType";
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

                public readonly static string CheckValue;

                static Z2()
                {
                    PNSResp.Bit48.Z2.KeyType = "Bit48.Z2.KeyType";
                    PNSResp.Bit48.Z2.StorageIndex = "Bit48.Z2.StorageIndex";
                    PNSResp.Bit48.Z2.WorkingKeyEncryptedUnderZoneMasterKey = "Bit48.Z2.WorkingKeyEncryptedUnderZoneMasterKey";
                    PNSResp.Bit48.Z2.CheckValue = "Bit48.Z2.CheckValue";
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
                    PNSResp.Bit48.Z3.SecurityFormatCode = "Bit48.Z3.SecurityFormatCode";
                    PNSResp.Bit48.Z3.PINBlockFormatCode = "Bit48.Z3.PINBlockFormatCode";
                    PNSResp.Bit48.Z3.StorageIndex = "Bit48.Z3.StorageIndex";
                    PNSResp.Bit48.Z3.SecurityMessageCounter = "Bit48.Z3.SecurityMessageCounter";
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
                    PNSResp.Bit48.Z4.SecurityFormatCode = "Bit48.Z4.SecurityFormatCode";
                    PNSResp.Bit48.Z4.StorageIndex = "Bit48.Z4.StorageIndex";
                    PNSResp.Bit48.Z4.SecurityMessageCounter = "Bit48.Z4.SecurityMessageCounter";
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
                    PNSResp.Bit48.Z5.SecurityFormatCode = "Bit48.Z5.SecurityFormatCode";
                    PNSResp.Bit48.Z5.StorageIndex = "Bit48.Z5.StorageIndex";
                    PNSResp.Bit48.Z5.SecurityMessageCounter = "Bit48.Z5.SecurityMessageCounter";
                }

                public Z5()
                {
                }
            }

            public class ZZ
            {
                public readonly static string ErrorBitNumber;

                public readonly static string ErrorValue;

                static ZZ()
                {
                    PNSResp.Bit48.ZZ.ErrorBitNumber = "Bit48.ZZ.ErrorBitNumber";
                    PNSResp.Bit48.ZZ.ErrorValue = "Bit48.ZZ.ErrorValue";
                }

                public ZZ()
                {
                }
            }
        }

        public class Bit49
        {
            public readonly static string CurrencyCode;

            static Bit49()
            {
                PNSResp.Bit49.CurrencyCode = "Bit49.CurrencyCode";
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
                PNSResp.Bit52.PersonalIdentificationNumber = "Bit52.PersonalIdentificationNumber";
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
                PNSResp.Bit53.SecurityRelatedControlInformation = "Bit53.SecurityRelatedControlInformation";
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
                PNSResp.Bit54.AdditionalAmount = "Bit54.AdditionalAmount";
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
                PNSResp.Bit55.ChipCardData = "Bit55.ChipCardData";
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
                    PNSResp.Bit60.A1.AttendedTerminalData = "Bit60.A1.AttendedTerminalData";
                    PNSResp.Bit60.A1.TerminalLocation = "Bit60.A1.TerminalLocation";
                    PNSResp.Bit60.A1.CardholderAttendance = "Bit60.A1.CardholderAttendance";
                    PNSResp.Bit60.A1.CardPresentIndicator = "Bit60.A1.CardPresentIndicator";
                    PNSResp.Bit60.A1.CardholderActivatedTerminalInformation = "Bit60.A1.CardholderActivatedTerminalInformation";
                    PNSResp.Bit60.A1.TerminalEntryCapability = "Bit60.A1.TerminalEntryCapability";
                    PNSResp.Bit60.A1.CardholderAuthenticationMethod = "Bit60.A1.CardholderAuthenticationMethod";
                }

                public A1()
                {
                }
            }

            public class Default
            {
                public readonly static string Data;

                static Default()
                {
                    PNSResp.Bit60.Default.Data = "Bit60.Default.Data";
                }

                public Default()
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
                    PNSResp.Bit62.AG.DBAName = "Bit62.AG.DBAName";
                    PNSResp.Bit62.AG.SubMerchantID = "Bit62.AG.SubMerchantID";
                    PNSResp.Bit62.AG.Street = "Bit62.AG.Street";
                    PNSResp.Bit62.AG.City = "Bit62.AG.City";
                    PNSResp.Bit62.AG.StateProvince = "Bit62.AG.StateProvince";
                    PNSResp.Bit62.AG.PostalCode = "Bit62.AG.PostalCode";
                    PNSResp.Bit62.AG.CountryCode = "Bit62.AG.CountryCode";
                    PNSResp.Bit62.AG.SubEntitlementNumber = "Bit62.AG.SubEntitlementNumber";
                    PNSResp.Bit62.AG.ContactInfo = "Bit62.AG.ContactInfo";
                    PNSResp.Bit62.AG.Email = "Bit62.AG.Email";
                    PNSResp.Bit62.AG.SellerID = "Bit62.AG.SellerID";
                }

                public AG()
                {
                }
            }

            public class B1
            {
                public readonly static string BatchOpenDate;

                public readonly static string BatchOpenTime;

                public readonly static string BatchReleaseDate;

                public readonly static string BatchReleaseTime;

                public readonly static string BatchTransactionCount;

                public readonly static string BatchNetAmount;

                public readonly static string NumberOfPaymentTypes;

                static B1()
                {
                    PNSResp.Bit62.B1.BatchOpenDate = "Bit62.B1.BatchOpenDate";
                    PNSResp.Bit62.B1.BatchOpenTime = "Bit62.B1.BatchOpenTime";
                    PNSResp.Bit62.B1.BatchReleaseDate = "Bit62.B1.BatchReleaseDate";
                    PNSResp.Bit62.B1.BatchReleaseTime = "Bit62.B1.BatchReleaseTime";
                    PNSResp.Bit62.B1.BatchTransactionCount = "Bit62.B1.BatchTransactionCount";
                    PNSResp.Bit62.B1.BatchNetAmount = "Bit62.B1.BatchNetAmount";
                    PNSResp.Bit62.B1.NumberOfPaymentTypes = "Bit62.B1.NumberOfPaymentTypes";
                }

                public B1()
                {
                }

                public class Account
                {
                    public readonly static string PaymentType;

                    public readonly static string TransactionCount;

                    public readonly static string NetAmount;

                    static Account()
                    {
                        PNSResp.Bit62.B1.Account.PaymentType = "Bit62.B1.Account.PaymentType";
                        PNSResp.Bit62.B1.Account.TransactionCount = "Bit62.B1.Account.TransactionCount";
                        PNSResp.Bit62.B1.Account.NetAmount = "Bit62.B1.Account.NetAmount";
                    }

                    public Account()
                    {
                    }
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
                    PNSResp.Bit62.B2.BatchUploadType = "Bit62.B2.BatchUploadType";
                    PNSResp.Bit62.B2.BatchNumber = "Bit62.B2.BatchNumber";
                    PNSResp.Bit62.B2.SalesTransactionCount = "Bit62.B2.SalesTransactionCount";
                    PNSResp.Bit62.B2.TotalSalesAmount = "Bit62.B2.TotalSalesAmount";
                    PNSResp.Bit62.B2.ReturnTransactionCount = "Bit62.B2.ReturnTransactionCount";
                    PNSResp.Bit62.B2.TotalReturnsAmount = "Bit62.B2.TotalReturnsAmount";
                }

                public B2()
                {
                }
            }

            public class B3
            {
                public readonly static string NextBatchNumber;

                static B3()
                {
                    PNSResp.Bit62.B3.NextBatchNumber = "Bit62.B3.NextBatchNumber";
                }

                public B3()
                {
                }
            }

            public class B4
            {
                public readonly static string NumberOfTotals;

                static B4()
                {
                    PNSResp.Bit62.B4.NumberOfTotals = "Bit62.B4.NumberOfTotals";
                }

                public B4()
                {
                }

                public class Account
                {
                    public readonly static string CardType;

                    public readonly static string TransactionCount;

                    public readonly static string NetAmount;

                    static Account()
                    {
                        PNSResp.Bit62.B4.Account.CardType = "Bit62.B4.Account.CardType";
                        PNSResp.Bit62.B4.Account.TransactionCount = "Bit62.B4.Account.TransactionCount";
                        PNSResp.Bit62.B4.Account.NetAmount = "Bit62.B4.Account.NetAmount";
                    }

                    public Account()
                    {
                    }
                }
            }

            public class D1
            {
                public readonly static string NumberOfDetails;

                static D1()
                {
                    PNSResp.Bit62.D1.NumberOfDetails = "Bit62.D1.NumberOfDetails";
                }

                public D1()
                {
                }

                public class Account
                {
                    public readonly static string TransactionSequenceNumber;

                    public readonly static string CardType;

                    public readonly static string TransactionType;

                    public readonly static string AuthorizationCode;

                    public readonly static string Amount;

                    static Account()
                    {
                        PNSResp.Bit62.D1.Account.TransactionSequenceNumber = "Bit62.D1.Account.TransactionSequenceNumber";
                        PNSResp.Bit62.D1.Account.CardType = "Bit62.D1.Account.CardType";
                        PNSResp.Bit62.D1.Account.TransactionType = "Bit62.D1.Account.TransactionType";
                        PNSResp.Bit62.D1.Account.AuthorizationCode = "Bit62.D1.Account.AuthorizationCode";
                        PNSResp.Bit62.D1.Account.Amount = "Bit62.D1.Account.Amount";
                    }

                    public Account()
                    {
                    }
                }
            }

            public class Default
            {
                public readonly static string Data;

                static Default()
                {
                    PNSResp.Bit62.Default.Data = "Bit62.Default.Data";
                }

                public Default()
                {
                }
            }

            public class DV
            {
                public readonly static string CAVV;

                static DV()
                {
                    PNSResp.Bit62.DV.CAVV = "Bit62.DV.CAVV";
                }

                public DV()
                {
                }
            }

            public class G1
            {
                public readonly static string AuthorizedGallonsDollars;

                public readonly static string OilLimit;

                public readonly static string PartsServiceLimited;

                public readonly static string MiscellaneousLimit;

                public readonly static string FuelPrintFlag;

                static G1()
                {
                    PNSResp.Bit62.G1.AuthorizedGallonsDollars = "Bit62.G1.AuthorizedGallonsDollars";
                    PNSResp.Bit62.G1.OilLimit = "Bit62.G1.OilLimit";
                    PNSResp.Bit62.G1.PartsServiceLimited = "Bit62.G1.PartsServiceLimited";
                    PNSResp.Bit62.G1.MiscellaneousLimit = "Bit62.G1.MiscellaneousLimit";
                    PNSResp.Bit62.G1.FuelPrintFlag = "Bit62.G1.FuelPrintFlag";
                }

                public G1()
                {
                }
            }

            public class G2
            {
                public readonly static string PrintText;

                static G2()
                {
                    PNSResp.Bit62.G2.PrintText = "Bit62.G2.PrintText";
                }

                public G2()
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
                    PNSResp.Bit62.H1.JulianDay = "Bit62.H1.JulianDay";
                    PNSResp.Bit62.H1.BatchNumber = "Bit62.H1.BatchNumber";
                    PNSResp.Bit62.H1.TransactionClass = "Bit62.H1.TransactionClass";
                    PNSResp.Bit62.H1.SequenceNumber = "Bit62.H1.SequenceNumber";
                }

                public H1()
                {
                }
            }

            public class H2
            {
                public readonly static string HostResponseText;

                static H2()
                {
                    PNSResp.Bit62.H2.HostResponseText = "Bit62.H2.HostResponseText";
                }

                public H2()
                {
                }
            }

            public class H3
            {
                public readonly static string HostResponseErrorNumber;

                static H3()
                {
                    PNSResp.Bit62.H3.HostResponseErrorNumber = "Bit62.H3.HostResponseErrorNumber";
                }

                public H3()
                {
                }
            }

            public class O1
            {
                public readonly static string ProviderName;

                static O1()
                {
                    PNSResp.Bit62.O1.ProviderName = "Bit62.O1.ProviderName";
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
                    PNSResp.Bit62.OD.SenderReferenceNumber = "Bit62.OD.SenderReferenceNumber";
                    PNSResp.Bit62.OD.SenderName = "Bit62.OD.SenderName";
                    PNSResp.Bit62.OD.RecipientName = "Bit62.OD.RecipientName";
                    PNSResp.Bit62.OD.SenderStreetAddress = "Bit62.OD.SenderStreetAddress";
                    PNSResp.Bit62.OD.SenderCity = "Bit62.OD.SenderCity";
                    PNSResp.Bit62.OD.SenderStateProvince = "Bit62.OD.SenderStateProvince";
                    PNSResp.Bit62.OD.SenderZip = "Bit62.OD.SenderZip";
                    PNSResp.Bit62.OD.SenderCountry = "Bit62.OD.SenderCountry";
                    PNSResp.Bit62.OD.SourceOfFunds = "Bit62.OD.SourceOfFunds";
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
                    PNSResp.Bit62.P1.HardwareVendorIdentifier = "Bit62.P1.HardwareVendorIdentifier";
                    PNSResp.Bit62.P1.SoftwareIdentifier = "Bit62.P1.SoftwareIdentifier";
                    PNSResp.Bit62.P1.HardwareSerialNumber = "Bit62.P1.HardwareSerialNumber";
                }

                public P1()
                {
                }
            }

            public class P3
            {
                public readonly static string PrivateLabelCardProgram;

                public readonly static string PrivateLabelAdditionalData;

                static P3()
                {
                    PNSResp.Bit62.P3.PrivateLabelCardProgram = "Bit62.P3.PrivateLabelCardProgram";
                    PNSResp.Bit62.P3.PrivateLabelAdditionalData = "Bit62.P3.PrivateLabelAdditionalData";
                }

                public P3()
                {
                }
            }

            public class P4
            {
                public readonly static string PANMethodology;

                public readonly static string EncryptionPayload;

                static P4()
                {
                    PNSResp.Bit62.P4.PANMethodology = "Bit62.P4.PANMethodology";
                    PNSResp.Bit62.P4.EncryptionPayload = "Bit62.P4.EncryptionPayload";
                }

                public P4()
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
                    PNSResp.Bit62.T1.ApplicationName = "Bit62.T1.ApplicationName";
                    PNSResp.Bit62.T1.ReleaseDate = "Bit62.T1.ReleaseDate";
                    PNSResp.Bit62.T1.EPROMVersionNumber = "Bit62.T1.EPROMVersionNumber";
                }

                public T1()
                {
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
                    PNSResp.Bit63.A1.ExtraChargesAmount = "Bit63.A1.ExtraChargesAmount";
                    PNSResp.Bit63.A1.RentalAgreementNumber = "Bit63.A1.RentalAgreementNumber";
                    PNSResp.Bit63.A1.RentalCity = "Bit63.A1.RentalCity";
                    PNSResp.Bit63.A1.RentalStateCode = "Bit63.A1.RentalStateCode";
                    PNSResp.Bit63.A1.RentalDate = "Bit63.A1.RentalDate";
                    PNSResp.Bit63.A1.RentalTime = "Bit63.A1.RentalTime";
                    PNSResp.Bit63.A1.ReturnCity = "Bit63.A1.ReturnCity";
                    PNSResp.Bit63.A1.ReturnStateCode = "Bit63.A1.ReturnStateCode";
                    PNSResp.Bit63.A1.ReturnDate = "Bit63.A1.ReturnDate";
                    PNSResp.Bit63.A1.ReturnTime = "Bit63.A1.ReturnTime";
                    PNSResp.Bit63.A1.RenterName = "Bit63.A1.RenterName";
                    PNSResp.Bit63.A1.ExtraChargesReasons = "Bit63.A1.ExtraChargesReasons";
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
                    PNSResp.Bit63.A2.RentalRate = "Bit63.A2.RentalRate";
                    PNSResp.Bit63.A2.RentalCountry = "Bit63.A2.RentalCountry";
                    PNSResp.Bit63.A2.RentalClassID = "Bit63.A2.RentalClassID";
                    PNSResp.Bit63.A2.NoShowIndicator = "Bit63.A2.NoShowIndicator";
                    PNSResp.Bit63.A2.ReturnCountry = "Bit63.A2.ReturnCountry";
                    PNSResp.Bit63.A2.ReturnLocationID = "Bit63.A2.ReturnLocationID";
                    PNSResp.Bit63.A2.OneWayDropOffCharges = "Bit63.A2.OneWayDropOffCharges";
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
                    PNSResp.Bit63.D1.ItemProductCode = "Bit63.D1.ItemProductCode";
                    PNSResp.Bit63.D1.ItemDescription = "Bit63.D1.ItemDescription";
                    PNSResp.Bit63.D1.ItemQuantity = "Bit63.D1.ItemQuantity";
                    PNSResp.Bit63.D1.ItemBulkUnitOfMeasure = "Bit63.D1.ItemBulkUnitOfMeasure";
                    PNSResp.Bit63.D1.ItemDebitCreditIndicator = "Bit63.D1.ItemDebitCreditIndicator";
                    PNSResp.Bit63.D1.ItemNetGrossIndicator = "Bit63.D1.ItemNetGrossIndicator";
                    PNSResp.Bit63.D1.ItemExtendedAmount = "Bit63.D1.ItemExtendedAmount";
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
                    PNSResp.Bit63.D2.ItemUnitCost = "Bit63.D2.ItemUnitCost";
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
                    PNSResp.Bit63.D3.ItemDiscountAmount = "Bit63.D3.ItemDiscountAmount";
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
                    PNSResp.Bit63.D4.ItemLocalTaxAmount = "Bit63.D4.ItemLocalTaxAmount";
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
                    PNSResp.Bit63.D5.ItemCommodityCode = "Bit63.D5.ItemCommodityCode";
                }

                public D5()
                {
                }
            }

            public class D6
            {
                public readonly static string TotalTaxAmount;

                public readonly static string NumberOfTaxTypeIdentifiers;

                static D6()
                {
                    PNSResp.Bit63.D6.TotalTaxAmount = "Bit63.D6.TotalTaxAmount";
                    PNSResp.Bit63.D6.NumberOfTaxTypeIdentifiers = "Bit63.D6.NumberOfTaxTypeIdentifiers";
                }

                public D6()
                {
                }

                public class TaxTypes
                {
                    public readonly static string ItemizedTaxTypeIdentifier;

                    public readonly static string ItemizedTaxAmount;

                    static TaxTypes()
                    {
                        PNSResp.Bit63.D6.TaxTypes.ItemizedTaxTypeIdentifier = "Bit63.D6.TaxTypes.ItemizedTaxTypeIdentifier";
                        PNSResp.Bit63.D6.TaxTypes.ItemizedTaxAmount = "Bit63.D6.TaxTypes.ItemizedTaxAmount";
                    }

                    public TaxTypes()
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
                    PNSResp.Bit63.D7.InvoiceDiscountTreatment = "Bit63.D7.InvoiceDiscountTreatment";
                    PNSResp.Bit63.D7.TaxTreatment = "Bit63.D7.TaxTreatment";
                    PNSResp.Bit63.D7.DiscountAmountSign = "Bit63.D7.DiscountAmountSign";
                    PNSResp.Bit63.D7.FreightAmountSign = "Bit63.D7.FreightAmountSign";
                    PNSResp.Bit63.D7.DutyAmountSign = "Bit63.D7.DutyAmountSign";
                    PNSResp.Bit63.D7.VATTaxAmountSignOrderLevel = "Bit63.D7.VATTaxAmountSignOrderLevel";
                    PNSResp.Bit63.D7.LineItemLevelDiscountTreatmentCode = "Bit63.D7.LineItemLevelDiscountTreatmentCode";
                    PNSResp.Bit63.D7.LineItemDetailIndicator = "Bit63.D7.LineItemDetailIndicator";
                }

                public D7()
                {
                }
            }

            public class Default
            {
                public readonly static string Data;

                static Default()
                {
                    PNSResp.Bit63.Default.Data = "Bit63.Default.Data";
                }

                public Default()
                {
                }
            }

            public class E1
            {
                public readonly static string OrderNumber;

                public readonly static string ECommerceIndicator;

                public readonly static string SecurityIndicator;

                public readonly static string ElectronicCommerceGoodsIndicator;

                static E1()
                {
                    PNSResp.Bit63.E1.OrderNumber = "Bit63.E1.OrderNumber";
                    PNSResp.Bit63.E1.ECommerceIndicator = "Bit63.E1.ECommerceIndicator";
                    PNSResp.Bit63.E1.SecurityIndicator = "Bit63.E1.SecurityIndicator";
                    PNSResp.Bit63.E1.ElectronicCommerceGoodsIndicator = "Bit63.E1.ElectronicCommerceGoodsIndicator";
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
                    PNSResp.Bit63.L1.FolioNumber = "Bit63.L1.FolioNumber";
                    PNSResp.Bit63.L1.ChargeDescription = "Bit63.L1.ChargeDescription";
                    PNSResp.Bit63.L1.ArrivalDate = "Bit63.L1.ArrivalDate";
                    PNSResp.Bit63.L1.DepartureDate = "Bit63.L1.DepartureDate";
                    PNSResp.Bit63.L1.SaleCode = "Bit63.L1.SaleCode";
                    PNSResp.Bit63.L1.ExtraChargesReasons = "Bit63.L1.ExtraChargesReasons";
                    PNSResp.Bit63.L1.ExtraChargesAmount = "Bit63.L1.ExtraChargesAmount";
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
                    PNSResp.Bit63.L2.RenterName = "Bit63.L2.RenterName";
                    PNSResp.Bit63.L2.LodgingPlan = "Bit63.L2.LodgingPlan";
                    PNSResp.Bit63.L2.RoomRate = "Bit63.L2.RoomRate";
                    PNSResp.Bit63.L2.NoOfNightsRoomRate1 = "Bit63.L2.NoOfNightsRoomRate1";
                    PNSResp.Bit63.L2.RoomRate2 = "Bit63.L2.RoomRate2";
                    PNSResp.Bit63.L2.NoOfNightsRoomRate2 = "Bit63.L2.NoOfNightsRoomRate2";
                    PNSResp.Bit63.L2.RoomRate3 = "Bit63.L2.RoomRate3";
                    PNSResp.Bit63.L2.NoOfNightsRoomRate3 = "Bit63.L2.NoOfNightsRoomRate3";
                    PNSResp.Bit63.L2.RoomTax = "Bit63.L2.RoomTax";
                    PNSResp.Bit63.L2.TaxAmount = "Bit63.L2.TaxAmount";
                    PNSResp.Bit63.L2.PrepaidExpense = "Bit63.L2.PrepaidExpense";
                    PNSResp.Bit63.L2.FolioCashAdvances = "Bit63.L2.FolioCashAdvances";
                    PNSResp.Bit63.L2.PostCheckOutChargesAmt = "Bit63.L2.PostCheckOutChargesAmt";
                    PNSResp.Bit63.L2.RestaurantRoomServiceChargesAmt = "Bit63.L2.RestaurantRoomServiceChargesAmt";
                    PNSResp.Bit63.L2.GiftShopChargesAmt = "Bit63.L2.GiftShopChargesAmt";
                    PNSResp.Bit63.L2.MiniBarChargesAmt = "Bit63.L2.MiniBarChargesAmt";
                    PNSResp.Bit63.L2.PhoneChargesAmt = "Bit63.L2.PhoneChargesAmt";
                    PNSResp.Bit63.L2.LaundryChargesAmt = "Bit63.L2.LaundryChargesAmt";
                    PNSResp.Bit63.L2.HealthClubChargesAmt = "Bit63.L2.HealthClubChargesAmt";
                    PNSResp.Bit63.L2.LoungeBarChargesAmt = "Bit63.L2.LoungeBarChargesAmt";
                    PNSResp.Bit63.L2.MovieChargesAmt = "Bit63.L2.MovieChargesAmt";
                    PNSResp.Bit63.L2.ValetParkingChargesAmt = "Bit63.L2.ValetParkingChargesAmt";
                    PNSResp.Bit63.L2.BusinessCenterChargesAmt = "Bit63.L2.BusinessCenterChargesAmt";
                    PNSResp.Bit63.L2.NonRoomChargesAmt = "Bit63.L2.NonRoomChargesAmt";
                    PNSResp.Bit63.L2.OtherServicesCode1 = "Bit63.L2.OtherServicesCode1";
                    PNSResp.Bit63.L2.OtherServicesAmt1 = "Bit63.L2.OtherServicesAmt1";
                    PNSResp.Bit63.L2.OtherServicesCode2 = "Bit63.L2.OtherServicesCode2";
                    PNSResp.Bit63.L2.OtherServicesAmt2 = "Bit63.L2.OtherServicesAmt2";
                    PNSResp.Bit63.L2.OtherServicesCode3 = "Bit63.L2.OtherServicesCode3";
                    PNSResp.Bit63.L2.OtherServicesAmt3 = "Bit63.L2.OtherServicesAmt3";
                    PNSResp.Bit63.L2.OtherServicesCode4 = "Bit63.L2.OtherServicesCode4";
                    PNSResp.Bit63.L2.OtherServicesAmt4 = "Bit63.L2.OtherServicesAmt4";
                    PNSResp.Bit63.L2.OtherServicesCode5 = "Bit63.L2.OtherServicesCode5";
                    PNSResp.Bit63.L2.OtherServicesAmt5 = "Bit63.L2.OtherServicesAmt5";
                    PNSResp.Bit63.L2.OtherServicesCode6 = "Bit63.L2.OtherServicesCode6";
                    PNSResp.Bit63.L2.OtherServicesAmt6 = "Bit63.L2.OtherServicesAmt6";
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
                    PNSResp.Bit63.L3.RequestorName = "Bit63.L3.RequestorName";
                    PNSResp.Bit63.L3.CustomerVATRegistrationNumber = "Bit63.L3.CustomerVATRegistrationNumber";
                    PNSResp.Bit63.L3.MerchantVATRegistrationNumber = "Bit63.L3.MerchantVATRegistrationNumber";
                    PNSResp.Bit63.L3.PSTTaxRegistrationNumber = "Bit63.L3.PSTTaxRegistrationNumber";
                    PNSResp.Bit63.L3.LocalTaxRate = "Bit63.L3.LocalTaxRate";
                    PNSResp.Bit63.L3.NationalTax = "Bit63.L3.NationalTax";
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
                    PNSResp.Bit63.M1.OrderNumber = "Bit63.M1.OrderNumber";
                    PNSResp.Bit63.M1.TypeIndicator = "Bit63.M1.TypeIndicator";
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
                    PNSResp.Bit63.P1.ProductCode1 = "Bit63.P1.ProductCode1";
                    PNSResp.Bit63.P1.ProductCode2 = "Bit63.P1.ProductCode2";
                    PNSResp.Bit63.P1.ProductCode3 = "Bit63.P1.ProductCode3";
                    PNSResp.Bit63.P1.DollarAmount1 = "Bit63.P1.DollarAmount1";
                    PNSResp.Bit63.P1.DollarAmount2 = "Bit63.P1.DollarAmount2";
                    PNSResp.Bit63.P1.DollarAmount3 = "Bit63.P1.DollarAmount3";
                    PNSResp.Bit63.P1.FuelUnitPrice = "Bit63.P1.FuelUnitPrice";
                    PNSResp.Bit63.P1.FuelGallons = "Bit63.P1.FuelGallons";
                    PNSResp.Bit63.P1.FuelFullServiceIndicator = "Bit63.P1.FuelFullServiceIndicator";
                }

                public P1()
                {
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
                    PNSResp.Bit63.R1.ReferenceCode = "Bit63.R1.ReferenceCode";
                    PNSResp.Bit63.R1.TipAmount = "Bit63.R1.TipAmount";
                    PNSResp.Bit63.R1.ServerNumber = "Bit63.R1.ServerNumber";
                    PNSResp.Bit63.R1.TaxAmount = "Bit63.R1.TaxAmount";
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
                    PNSResp.Bit63.R2.InvoiceNumber = "Bit63.R2.InvoiceNumber";
                    PNSResp.Bit63.R2.TranInformation = "Bit63.R2.TranInformation";
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

                public readonly static string TotalCardsActivatedInBlock;

                static SV()
                {
                    PNSResp.Bit63.SV.ExternalTransactionIdentifier = "Bit63.SV.ExternalTransactionIdentifier";
                    PNSResp.Bit63.SV.EmployeeNumber = "Bit63.SV.EmployeeNumber";
                    PNSResp.Bit63.SV.CashOutIndicator = "Bit63.SV.CashOutIndicator";
                    PNSResp.Bit63.SV.SequenceNumberOfCardBeingUsed = "Bit63.SV.SequenceNumberOfCardBeingUsed";
                    PNSResp.Bit63.SV.TotalNumberOfCardsBeingIssued = "Bit63.SV.TotalNumberOfCardsBeingIssued";
                    PNSResp.Bit63.SV.TotalCardsActivatedInBlock = "Bit63.SV.TotalCardsActivatedInBlock";
                }

                public SV()
                {
                }
            }
        }

        public class Bit7
        {
            public readonly static string TransactionDateTime;

            static Bit7()
            {
                PNSResp.Bit7.TransactionDateTime = "Bit7.TransactionDateTime";
            }

            public Bit7()
            {
            }
        }

        public class Bit70
        {
            public readonly static string NetworkManagementInformationCode;

            static Bit70()
            {
                PNSResp.Bit70.NetworkManagementInformationCode = "Bit70.NetworkManagementInformationCode";
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
                PNSResp.Bit71.MessageNumberSecurityMessageCounter = "Bit71.MessageNumberSecurityMessageCounter";
            }

            public Bit71()
            {
            }
        }

        public class Bit72
        {
            public readonly static string MessageNumberLastSecurityMessageCounter;

            static Bit72()
            {
                PNSResp.Bit72.MessageNumberLastSecurityMessageCounter = "Bit72.MessageNumberLastSecurityMessageCounter";
            }

            public Bit72()
            {
            }
        }

        public class Bit8
        {
            public readonly static string AmtCardholderBillingFee;

            static Bit8()
            {
                PNSResp.Bit8.AmtCardholderBillingFee = "Bit8.AmtCardholderBillingFee";
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
                PNSResp.Bit90.OriginalMessageType = "Bit90.OriginalMessageType";
                PNSResp.Bit90.OriginalTraceNumber = "Bit90.OriginalTraceNumber";
                PNSResp.Bit90.OriginalTransmissionDateAndTime = "Bit90.OriginalTransmissionDateAndTime";
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
                PNSResp.MessageHeader.RoutingInformation = "MessageHeader.RoutingInformation";
                PNSResp.MessageHeader.MultitranIndicator = "MessageHeader.MultitranIndicator";
                PNSResp.MessageHeader.MultiBatchFlag = "MessageHeader.MultiBatchFlag";
                PNSResp.MessageHeader.MessageType = "MessageHeader.MessageType";
            }

            public MessageHeader()
            {
            }
        }
    }
}