// <copyright file="OrbitalPaymentechConstants.cs" company="clarity-ventures.com">
// Copyright (c) -2019 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the OrbitalPaymentechConstants.cs class/interface</summary>
// ReSharper disable CommentTypo
namespace Clarity.Ecommerce.Providers.Payments.OrbitalPaymentech
{
    /// <summary>An orbital paymentech constants.</summary>
    internal static class OrbitalPaymentechConstants
    {
        /// <summary>The orbital paymentech API version.</summary>
        internal const string ApiVersion = "3.9.3";

        /// <summary>Type of the orbital paymentech industry.</summary>
        internal const string IndustryType = "EC";

        /// <summary>The orbital paymentech bin.</summary>
        internal const string Bin = "000001";

        /// <summary>Identifier for the orbital paymentech terminal.</summary>
        internal const string TerminalId = "001";

        /// <summary>A credit card presence indicators (Visa or Discover Credit/Debit Cards only).
        /// * If you are trying to collect a Card Verification Number (ccCardVerifyNum) for a Visa or Discover
        ///   transaction, pass one of these values:
        ///   - 1 Value is Present
        ///   - 2 Value on card but illegible
        ///   - 9 Cardholder states data not available
        /// * If the transaction is not a Visa or Discover transaction:
        ///   - null-fill this attribute OR
        ///   - Do not submit the attribute at all.
        /// </summary>
        internal static class CreditCardPresenceIndicators
        {
            /// <summary>The value is present.</summary>
            internal const string ValueIsPresent = "1";

            /// <summary>The value is on card but illegible.</summary>
            internal const string ValueIsOnCardButIllegible = "2";

            /// <summary>The cardholder states data not available.</summary>
            internal const string CardholderStatesDataNotAvailable = "9";

            /// <summary>The non-visa/non-discover option.</summary>
            internal const string? NonVisaNonDiscover = null;
        }

        /// <summary>A transaction types.</summary>
        internal static class TransactionTypes
        {
            /// <summary>The authorize type.</summary>
            internal const string Authorize = "A";

            /// <summary>The authorize and capture type.</summary>
            internal const string AuthorizeAndCapture = "AC";

            /// <summary>The force capture type.</summary>
            internal const string ForceCapture = "FC";

            /// <summary>The refund type.</summary>
            internal const string Refund = "R";
        }

        /// <summary>CIT/MIT Message Code<br/>
        /// Indicates the message type to be used for the message type records<br/>
        /// Customer Initiated: CSTO/CGEN/CINS/CUSE/CREC<br/>
        /// Merchant Initiated: MUSE/MINS/MRAU/MREC/MRSB.</summary>
        internal static class InitiatedTransactionMessageCodes
        {
            /// <summary>A by customer.</summary>
            internal static class ByCustomer
            {
                /// <summary>The stored credential.</summary>
                internal const string StoredCredential = "CSTO";

                /// <summary>The general.</summary>
                internal const string General = "CGEN";

                /// <summary>The installment.</summary>
                internal const string Installment = "CINS";

                /// <summary>The unscheduled credential on file.</summary>
                internal const string UnscheduledCredentialOnFile = "CUSE";

                /// <summary>The recurring.</summary>
                internal const string Recurring = "CREC";
            }

            /// <summary>A by merchant.</summary>
            internal static class ByMerchant
            {
                /// <summary>The unscheduled credential on file.</summary>
                internal const string UnscheduledCredentialOnFile = "MUSE";

                /// <summary>The installment.</summary>
                internal const string Installment = "MINS";

                /// <summary>The reauthorization.</summary>
                internal const string Reauthorization = "MRAU";

                /// <summary>The recurring.</summary>
                internal const string Recurring = "MREC";

                /// <summary>The resubmission.</summary>
                internal const string Resubmission = "MRSB";

                /// <summary>Customer Request.</summary>
                internal const string CustomerRequest = "CREC";

                /// <summary>Customer Generated.</summary>
                internal const string CustomerGenerated = "CGEN";
            }
        }

        /// <summary>A stored credential indicators.</summary>
        internal static class StoredCredentialIndicators
        {
            /// <summary>The on file.</summary>
            internal const string OnFile = "Y";

            /// <summary>The not on file.</summary>
            internal const string NotOnFile = "N";

            /// <summary>The blank.</summary>
            internal const string Blank = " ";
        }

        /// <summary>Card Type/Brand for the Transaction<br/>
        /// Returns the Card Type/Brand as processed on the host platform<br/>
        /// * For Refunds and Force transactions, if no cardBrand, such as Visa or MasterCard, was sent in the request
        ///   (when optional), the specific Card Brand mnemonic is returned.
        /// * For PINless Debit transactions, the Card Brand is DP (which is a generic PINless mnemonic).</summary>
        internal static class CardBrands
        {
            /// <summary>The bill me later.</summary>
            internal const string BillMeLater = "BL";

            /// <summary>The electronic check.</summary>
            internal const string ElectronicCheck = "EC";

            /// <summary>The european direct debit.</summary>
            internal const string EuropeanDirectDebit = "ED";

            /// <summary>The international maestro.</summary>
            internal const string InternationalMaestro = "IM";

            /// <summary>The chase net credit card.</summary>
            internal const string ChaseNetCreditCard = "CZ";

            /// <summary>The chase net signature debit.</summary>
            internal const string ChaseNetSignatureDebit = "CR";

            /// <summary>The PIN-less debit.</summary>
            internal const string PINLessDebit = "DP";
        }

        /// <summary>Deposit Account Type<br/>
        /// Conditionally required for Electronic Check processing:
        /// * C Consumer Checking (US or Canadian)
        /// * S Consumer Savings (US or Canadian)
        /// * X Commercial Checking (US or Canadian)
        /// * NOTE: If this tag is missing, the host will default the value to 'C'.</summary>
        internal static class ECheckAccountTypes
        {
            /// <summary>The consumer checking.</summary>
            internal const string ConsumerChecking = "C";

            /// <summary>The consumer savings.</summary>
            internal const string ConsumerSavings = "S";

            /// <summary>The commercial checking.</summary>
            internal const string CommercialChecking = "X";
        }

        /// <summary>ECP Payment Delivery Method<br/>
        /// * Conditionally required for Electronic Check processing.
        /// * This field indicates the preferred manner to deposit the transaction.</summary>
        internal static class ECPPaymentDeliveryMethod
        {
            /// <summary>Best Possible Method (US Only)<br/>
            /// Chase Paymentech utilizes the method that best fits the situation. If the RDFI is not an ACH participant, a
            /// facsimile draft is created. This should be the default value for this field.</summary>
            internal const string BestPossibleMethod = "B";

            /// <summary>ACH (US or Canadian)<br/>
            /// Deposit the transaction by ACH only. If the RDFI is not an ACH participant, the transaction is rejected.</summary>
            internal const string ACH = "A";
        }

        /// <summary>A procedure response statuses.</summary>
        internal static class ProcedureResponseStatuses
        {
            /// <summary>Code 0. Success!</summary>
            internal const string Success = "0";

            /// <summary>Code 20400. Invalid Request. Fix the request.</summary>
            internal const string InvalidRequest = "20400";

            /// <summary>Code 20403. Forbidden: SSL Connection Required. Fix.</summary>
            internal const string Status20403 = "20403";

            /// <summary>Code 20408. Request Timed Out. Resend.</summary>
            internal const string Status20408 = "20408";

            /// <summary>Code 20412. Precondition Failed: Security Information is missing. Call.</summary>
            internal const string Status20412 = "20412";

            /// <summary>Code 20500. Internal Server Error. Resend.</summary>
            internal const string Status20500 = "20500";

            /// <summary>Code 20502. Connection Error. Resend.</summary>
            internal const string Status20502 = "20502";

            /// <summary>Code 20503. Server Unavailable: Please Try Again Later. Resend.</summary>
            internal const string Status20503 = "20503";

            /// <summary>Code 19789. FX: Merchant is Not Eligible. Resend.</summary>
            internal const string Status19789 = "19789";

            /// <summary>Code 19790. FX: Optout Indicator And Rate Handling Indicator Both Must Be Present. Resend.</summary>
            internal const string Status19790 = "19790";

            /// <summary>Code 19791. FX: Invalid Value in, Field[%s], Value[%s], Expected[%s]. Resend.</summary>
            internal const string Status19791 = "19791";

            /// <summary>Code 19792. FX: Missing Data [%s]. Resend.</summary>
            internal const string Status19792 = "19792";
        }

        /// <summary>An approval statuses.</summary>
        internal static class ApprovalStatuses
        {
            /// <summary>The declined.</summary>
            internal const string Declined = "0";

            /// <summary>The approved.</summary>
            internal const string Approved = "1";

            /// <summary>The error.</summary>
            internal const string Error = "2";
        }

        /// <summary>A profile override indicators.</summary>
        /// <remarks>The Orbital Gateway has configuration options for the Profile setup to determine how the Customer
        /// Reference Number is leveraged to populate other data sets using the profileOrderOverideInd value.</remarks>
        internal static class ProfileOverrideIndicators
        {
            /// <summary>No mapping to order data.</summary>
            internal const string NoMapping = "NO";

            /// <summary>Pre-populate orderID with the Customer Reference Number.</summary>
            internal const string OrderID = "OI";

            /// <summary>Pre-populate the comments field (this field is called Order Description in the Virtual Terminal)
            /// with the Customer Reference Number. The relevance of this feature is on the PNS platform (BIN 000002), where
            /// the comments field populates the Customer-Definable Data. This data can then be made
            /// available on certain Resource Online Reports. Any questions about your reports should be directed to your
            /// Relationship Manager.</summary>
            internal const string Comments = "OD";

            /// <summary>Pre-populate the orderID and comments fields with the Customer Reference Number.</summary>
            internal const string OrderIDAndComments = "OA";
        }

        /// <summary>Method to use to Generate the Customer Profile Number
        /// * When creating a profile during the Order request, defines how Customer Profile Number will be generated.</summary>
        internal static class AddProfileFromOrderValues
        {
            /// <summary>A Auto-Generate the customerRefNum.</summary>
            internal const string AutoGenerate = "A";

            /// <summary>S Use customerRefNum field.</summary>
            internal const string UseCustomerRefNumField = "S";

            /// <summary>O Use orderID as the customerRefNum.</summary>
            internal const string UseOrderID = "O";

            /// <summary>D Use comments as the CustomerRefNum.</summary>
            internal const string UseComments = "D";
        }

        /// <summary>A recurring indicators.</summary>
        internal static class RecurringIndicators
        {
            /// <summary>The first.</summary>
            internal const string First = "RF";

            /// <summary>The subsequent.</summary>
            internal const string Subsequent = "RS";
        }

        /// <summary>A recurring has no end date.</summary>
        internal static class RecurringHasNoEndDate
        {
            /// <summary>The has no end date.</summary>
            internal const string HasNoEndDate = "Y";

            /// <summary>The has end date.</summary>
            internal const string HasEndDate = "N";
        }

        /// <summary>A managed billing type.</summary>
        internal static class ManagedBillingType
        {
            /// <summary>The recurring.</summary>
            internal const string Recurring = "R";

            /// <summary>The deferred.</summary>
            internal const string Deferred = "D";
        }

        /// <summary>A managed billing order identifier generate methods.</summary>
        internal static class ManagedBillingOrderIDGenMethods
        {
            /// <summary>The use customer reference number.</summary>
            internal const string UseCustomerRefNum = "IO";

            /// <summary>The dynamically generate.</summary>
            internal const string DynamicallyGenerate = "DI";
        }

        /// <summary>A managed billing recurring frequency.</summary>
        internal static class ManagedBillingRecurringFrequency
        {
            /*
             * mbRecurringFrequency
             * Managed Billing Recurring Frequency Pattern
             * This pattern is a subset of a standard CRON expression, comprising 3 fields separated by white space:
             * Field            Allowed Values      Allowed Special Characters
             * Day-of-month     1–31                ,-/*?LW
             * Month            1–12 or JAN–DEC     ,-/*
             * Day-of-week      1–7 or SUN–SAT      ,-/*?L#
             * SEE ALSO: For a full discussion of these three fields, the usage of the special characters, and multiple
             * example values, see 3.4.3 Profiles and Managed Billing.
             * https://secure.paymentech.com/developercenter/files/file?fid=gTKAbxPnAPY%3D
             * See Page 99 for a list of examples
             */
        }
    }
}
