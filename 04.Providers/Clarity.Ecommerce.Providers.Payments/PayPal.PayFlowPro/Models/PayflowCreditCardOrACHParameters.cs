// <copyright file="PayflowCreditCardOrACHParameters.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payflow credit card parameters class</summary>
// ReSharper disable CommentTypo, StringLiteralTypo, StyleCop.SA1623
namespace Clarity.Ecommerce.Providers.Payments.PayPalPayflowPro
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>(Serializable) A payflow credit card or ACH parameters.</summary>
    /// <remarks>Core Credit Card/ACH Parameters. All credit card processors accept the basic parameters described in
    /// the following table* with one exception: the PayPal processor does not support SWIPE*. Note: See PayPal
    /// Credit Card Transaction Request Parameters for the request parameters specific to the PayPal Processor.</remarks>
    /// <seealso cref="PayflowRequestBase"/>
    [PublicAPI, Serializable, DataContract]
    public class PayflowCreditCardOrACHParameters : PayflowRequestBase
    {
        /// <summary>TENDER (Required)<br/>
        /// The method of payment. Values are:<br/>
        /// - A = Automated clearinghouse (ACH)<br/>
        /// - C = Credit card<br/>
        /// - D = Pinless debit<br/>
        /// - K = Telecheck<br/>
        /// - P = PayPal<br/>
        /// See the Payflow ACH Payment Service Guide for details on the ACH tender type.</summary>
        /// <value>The tender.</value>
        [JsonProperty("TENDER"), DataMember(Name = "TENDER"), ApiMember(Name = "TENDER",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "TENDER: (Required) The method of payment. Values are:"
                 + "A = Automated clearinghouse (ACH), C = Credit card, D = Pinless debit, K = Telecheck, P = PayPal. "
                 + "See the Payflow ACH Payment Service Guide for details on the ACH tender type.")]
        public string Tender { get; set; } = "C";

        /// <summary>TRXTYPE (Required)<br/>
        /// Indicates the type of transaction to perform. Values are:<br/>
        /// - A = Authorization<br/>
        /// - B = Balance Inquiry<br/>
        /// - C = Credit (Refund)<br/>
        /// - D = Delayed Capture<br/>
        /// - F = Voice Authorization<br/>
        /// - I = Inquiry<br/>
        /// - K = Rate Lookup<br/>
        /// - L = Data Upload<br/>
        /// - S = Sale<br/>
        /// - V = Void<br/>
        /// - N = Duplicate Transaction<br/>
        /// Note: A type N transaction represents a duplicate transaction (version 4 SDK or HTTPS interface only)
        /// with a PNREF that is the same as the original. It appears only in the PayPal Manager user interface and
        /// never settles.</summary>
        /// <value>The type of the transaction.</value>
        [JsonProperty("TRXTYPE"), DataMember(Name = "TRXTYPE"), ApiMember(Name = "TRXTYPE",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "TRXTYPE (Required): Indicates the type of transaction to perform. Values are: "
                 + "A = Authorization, B = Balance Inquiry, C = Credit (Refund), D = Delayed Capture, "
                 + "F = Voice Authorization, I = Inquiry, K = Rate Lookup, L = Data Upload, S = Sale, V = Void, "
                 + "N = Duplicate Transaction Note: A type N transaction represents a duplicate transaction "
                 + "(version 4 SDK or HTTPS interface only) with a PNREF that is the same as the original. "
                 + "It appears only in the PayPal Manager user interface and never settles.")]
        public string? TransactionType { get; set; }

        /// <summary>ACCTTYPE (Required for echecks)<br/>
        /// The type of account for echecks (checking or savings).<br/>
        /// Limitations: This value may only contain the character 'C' or 'S'</summary>
        /// <value>The account.</value>
        [JsonProperty("ACCTTYPE"), DataMember(Name = "ACCTTYPE"), ApiMember(Name = "ACCTTYPE",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "ACCTTYPE (Required for echecks): The type of account for echecks (checking or savings). "
                + "Limitations: This value may only contain the character 'C' or 'S'")]
        public string? AccountType { get; set; }

        /// <summary>ACCT (Required for credit cards and echecks)<br/>
        /// Credit card, purchase card or echeck account number. For example, ACCT=5555555555554444. For the pinless
        /// debit TENDER type, ACCT can be the bank account number.<br/>
        /// Limitations: This value may not contain spaces, non-numeric characters, or dashes.</summary>
        /// <value>The account.</value>
        [JsonProperty("ACCT"), DataMember(Name = "ACCT"), ApiMember(Name = "ACCT",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "ACCT (Required for credit cards and echecks): Credit card, purchase card oe echeck number."
                 + " For example, ACCT=5555555555554444. For the pinless debit TENDER type, ACCT can be the bank "
                 + "account number. Limitations: This value may not contain spaces, non-numeric characters, or dashes")]
        public string? Account { get; set; }

        /// <summary>ABA (Required for echecks)<br/>
        /// Bank Account Routing number. For example, ABA=123456789.<br/>
        /// Limitations: This value may not contain spaces, non-numeric characters, or dashes.</summary>
        /// <value>The account.</value>
        [JsonProperty("ABA"), DataMember(Name = "ABA"), ApiMember(Name = "ACCT",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "ABA (Required for echecks): Bank Account Routing number. For example, ACCT=123456789."
                + "Limitations: This value may not contain spaces, non-numeric characters, or dashes")]
        public string? ABA { get; set; }

        /// <summary>FIRSTNAME (Required for echecks)<br/>
        /// The account holder name. For example, FIRSTNAME=Susan Smith.</summary>
        /// <value>The account.</value>
        [JsonProperty("FIRSTNAME"), DataMember(Name = "FIRSTNAME"), ApiMember(Name = "FIRSTNAME",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "FIRSTNAME (Required for echecks): The account holder name. "
                + "For example, FIRSTNAME=Susan Smith.")]
        public string? AccountHolderName { get; set; }

        /// <summary>EXPDATE (Required)<br/>
        /// Expiration date of the credit card. For example, 1215 represents December 2015.<br/>
        /// Limitations: MMYY format.</summary>
        /// <value>The expiration date.</value>
        [JsonProperty("EXPDATE"), DataMember(Name = "EXPDATE"), ApiMember(Name = "EXPDATE",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "EXPDATE (Required): Expiration date of the credit card. For example, 1215 represents"
                 + "December 2015. Limitations: MMYY format.")]
        public string? ExpirationDate { get; set; }

        /// <summary>AMT (Required)<br/>
        /// Amount (Default: U.S. based currency).<br/>
        /// Character length and limitations: numeric characters and a decimal only. The maximum length varies depending
        /// on your processor. Specify the exact amount to the cent using a decimal point (use 34.00 not 34). Do not
        /// include comma separators (use 1199.95 not 1,199.95). Your processor or Internet Merchant Account provider
        /// may stipulate a maximum amount.<br/>
        /// Note: If your processor accepts non-decimal currencies, such as, Japanese Yen, include a decimal in the
        /// amount you pass to Payflow (use 100.00 not 100). Payflow removes the decimal portion before sending the
        /// value to the processor.</summary>
        /// <value>The amount.</value>
        [JsonProperty("AMT"), DataMember(Name = "AMT"), ApiMember(Name = "AMT",
             DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "AMT (Required): Amount (Default: U.S. based currency). Character length and limitations: "
                 + "numeric characters and a decimal only. The maximum length varies depending on your processor. "
                 + "Specify the exact amount to the cent using a decimal point (use 34.00 not 34). Do not include "
                 + "comma separators (use 1199.95 not 1,199.95). Your processor or Internet Merchant Account "
                 + "provider may stipulate a maximum amount. Note: If your processor accepts non-decimal currencies, "
                 + "such as, Japanese Yen, include a decimal in the amount you pass to Payflow (use 100.00 not 100)."
                 + " Payflow removes the decimal portion before sending the value to the processor.")]
        public string? Amount { get; set; }

        /// <summary>COMMENT1 (Optional)<br/>
        /// Merchant-defined value for reporting and auditing purposes.<br/>
        /// Limitations: 128 alphanumeric characters.</summary>
        /// <value>The comment 1.</value>
        [JsonProperty("COMMENT1"), DataMember(Name = "COMMENT1"), ApiMember(Name = "COMMENT1",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "COMMENT1 (Optional): Merchant-defined value for reporting and auditing purposes. "
                 + "Limitations: 128 alphanumeric characters")]
        public string? Comment1 { get; set; }

        /// <summary>COMMENT2 (Optional)<br/>
        /// Merchant-defined value for reporting and auditing purposes.<br/>
        /// Limitations: 128 alphanumeric characters.</summary>
        /// <value>The comment 1.</value>
        [JsonProperty("COMMENT2"), DataMember(Name = "COMMENT2"), ApiMember(Name = "COMMENT2",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "COMMENT2 (Optional): Merchant-defined value for reporting and auditing purposes. "
                 + "Limitations: 128 alphanumeric characters")]
        public string? Comment2 { get; set; }

        /// <summary>CVV2 (Optional)<br/>
        /// A code printed (not imprinted) on the back of a credit card. Used as partial assurance that the card is in
        /// the buyer's possession.<br/>
        /// Limitations: 3 or 4 digits.</summary>
        /// <value>The CVV2.</value>
        [JsonProperty("CVV2"), DataMember(Name = "CVV2"), ApiMember(Name = "CVV2",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "CVV2 (Optional): A code printed (not imprinted) on the back of a credit card. Used as "
                 + "partial assurance that the card is in the buyer's possession."
                 + "Limitations: 3 or 4 digits.")]
        public string? CVV2 { get; set; }

        /// <summary>RECURRING (Optional)<br/>
        /// Identifies the transaction as recurring. Value is:<br/>
        /// - Y - Identifies the transaction as recurring.<br/>
        /// - N - Does not identify the transaction as recurring (default).<br/>
        /// This value does not activate the Payflow Recurring Billing Service API. If the RECURRING parameter value is
        /// Y in the original transaction, this value is ignored when forming credit, void, and force transactions. If
        /// you subscribe to the Payflow Fraud Protection Services:<br/>
        /// - To avoid charging you to filter recurring transactions that you know are reliable, the fraud filters
        ///   do not screen recurring transactions.<br/>
        /// - To screen a prospective recurring customer, submit the transaction data on the PayPal Manager Manual
        ///   Transactions page. The filters screen the transaction in the normal manner. If the transaction triggers a
        ///   filter, follow the normal process to review the filter results.<br/>
        /// Note: If your transaction is declined and the PAYMENTADVICECODE response parameter is supported by your
        /// processor, a PAYMENTADVICECODE value is returned representing the reason that the transaction was declined.
        /// Obtain the meaning of PAYMENTADVICECODE values from your acquiring bank.<br/>
        /// Character length and limitations: 1 alpha character.</summary>
        /// <value>The recurring.</value>
        [JsonProperty("RECURRING"), DataMember(Name = "RECURRING"), ApiMember(Name = "RECURRING",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "RECURRING (Optional): Identifies the transaction as recurring. Value is: "
                 + "Y: Identifies the transaction as recurring, N: Does not identify the transaction as recurring (default). "
                 + "This value does not activate the Payflow Recurring Billing Service API. If the RECURRING "
                 + "parameter value is Y in the original transaction, this value is ignored when forming credit, "
                 + "void, and force transactions. If you subscribe to the Payflow Fraud Protection Services: - To "
                 + "avoid charging you to filter recurring transactions that you know are reliable, the fraud "
                 + "filters do not screen recurring transactions. - To screen a prospective recurring customer, "
                 + "submit the transaction data on the PayPal Manager Manual Transactions page. The filters screen "
                 + "the transaction in the normal manner. If the transaction triggers a filter, follow the normal "
                 + "process to review the filter results. Note: If your transaction is declined and the "
                 + "PAYMENTADVICECODE response parameter is supported by your processor, a PAYMENTADVICECODE value "
                 + "is returned representing the reason that the transaction was declined. Obtain the meaning of "
                 + "PAYMENTADVICECODE values from your acquiring bank. "
                 + "Character length and limitations: 1 alpha character.")]
        public string Recurring { get; set; } = "N";

        /// <summary>SWIPE (Required for card-present transactions only)<br/>
        /// Used to pass the Track 1 or Track 2 data (card's magnetic stripe information) for card-present transactions.
        /// Include either Track 1 or Track 2 data, not both. If Track 1 is physically damaged, the point-of-sale (POS)
        /// application can send Track 2 data instead.<br/>
        /// The track data includes the disallowed = (equal sign) character. To enable you to use the data, the SWIPE
        /// parameter must include a length tag specifying the number of characters in the track data. For this reason,
        /// in addition to passing the track data, the POS application must count the characters in the track data and
        /// pass that number. Length tags are described in Using Special Characters In Values.<br/>
        /// Note: swipe(card-present transactions) are not supported by the PayPal processor.<br/>
        /// Limitations: Alphanumeric and special characters.</summary>
        /// <value>The swipe.</value>
        [JsonProperty("SWIPE"), DataMember(Name = "SWIPE"), ApiMember(Name = "SWIPE",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "SWIPE (Required for card-present transactions only)")]
        public string? Swipe { get; set; }

        /// <summary>ORDERID (Optional)<br/>
        /// Checks for a duplicate order. If you pass ORDERID in a request and pass it again in the future, the response
        /// returns DUPLICATE=2 along with the ORDERID.<br/>
        /// Note: Do not use ORDERID to catch duplicate orders processed within seconds of each other. Use ORDERID with
        /// Request ID to prevent duplicates as a result of processing or communication errors.</summary>
        /// <value>The orderid.</value>
        [JsonProperty("ORDERID"), DataMember(Name = "ORDERID"), ApiMember(Name = "ORDERID",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "ORDERID (Optional)")]
        public string? OrderID { get; set; }

        /// <summary>BILLTOFIRSTNAME (Optional)<br/>
        /// Cardholder's first name.<br/>
        /// Limitations: 30 alphanumeric characters.</summary>
        /// <value>The name of the bill to first.</value>
        [JsonProperty("BILLTOFIRSTNAME"), DataMember(Name = "BILLTOFIRSTNAME"), ApiMember(Name = "BILLTOFIRSTNAME",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "BILLTOFIRSTNAME (Optional): Cardholder's first name. Limitations: 30 alphanumeric characters.")]
        public string? BillToFirstName { get; set; }

        /// <summary>BILLTOLASTNAME (Optional but recommended)<br/>
        /// Cardholder's last name.<br/>
        /// Limitations: 30 alphanumeric characters.</summary>
        /// <value>The name of the bill to last.</value>
        [JsonProperty("BILLTOLASTNAME"), DataMember(Name = "BILLTOLASTNAME"), ApiMember(Name = "BILLTOLASTNAME",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "BILLTOLASTNAME (Optional but recommended): Cardholder's last name. Limitations: 30 alphanumeric characters.")]
        public string? BillToLastName { get; set; }

        /// <summary>BILLTOSTREET (Optional)<br/>
        /// The cardholder's street address (number and street name).<br/>
        /// The address verification service verifies the STREET address.<br/>
        /// Limitations: 30 alphanumeric characters.</summary>
        /// <value>The bill to street.</value>
        [JsonProperty("BILLTOSTREET"), DataMember(Name = "BILLTOSTREET"), ApiMember(Name = "BILLTOSTREET",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "BILLTOSTREET (Optional): The cardholder's street address (number and street name). The "
                 + "address verification service verifies the STREET address. Limitations: 30 alphanumeric "
                 + "characters")]
        public string? BillToStreet { get; set; }

        /// <summary>BILLTOSTREET2 (Optional)<br/>
        /// The second line of the cardholder's street address.<br/>
        /// The address verification service verifies the STREET address.<br/>
        /// Limitations: 30 alphanumeric characters.</summary>
        /// <value>The bill to street 2.</value>
        [JsonProperty("BILLTOSTREET2"), DataMember(Name = "BILLTOSTREET2"), ApiMember(Name = "BILLTOSTREET2",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "BILLTOSTREET2 (Optional): The second line of the cardholder's street address. The "
                 + "address verification service verifies the STREET address.")]
        public string? BillToStreet2 { get; set; }

        /// <summary>BILLTOCITY (Optional)<br/>
        /// Bill-to city.<br/>
        /// Limitations: 20-character string.</summary>
        /// <value>The bill to city.</value>
        [JsonProperty("BILLTOCITY"), DataMember(Name = "BILLTOCITY"), ApiMember(Name = "BILLTOCITY",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "BILLTOCITY (Optional): Bill-to city. Limitations: 20-character string.")]
        public string? BillToCity { get; set; }

        /// <summary>BILLTOSTATE (Optional)<br/>
        /// Bill-to state.<br/>
        /// Limitations: 2-character string.</summary>
        /// <value>The bill to state.</value>
        [JsonProperty("BILLTOSTATE"), DataMember(Name = "BILLTOSTATE"), ApiMember(Name = "BILLTOSTATE",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "BILLTOSTATE (Optional): ")]
        public string? BillToState { get; set; }

        /// <summary>BILLTOZIP (Optional)<br/>
        /// Cardholder's 5- to 9-digit zip (postal) code.<br/>
        /// Limitations: 9 characters maximum. Do not use spaces, dashes, or non-numeric characters.</summary>
        /// <value>The bill to zip.</value>
        [JsonProperty("BILLTOZIP"), DataMember(Name = "BILLTOZIP"), ApiMember(Name = "BILLTOZIP",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "BILLTOZIP (Optional): ")]
        public string? BillToZip { get; set; }

        /// <summary>BILLTOCOUNTRY (Optional)<br/>
        /// Bill-to country. The Payflow API accepts 3-digit numeric country codes. Refer to the ISO 3166-1 numeric
        /// country codes.<br/>
        /// Limitations: 3-character country code.</summary>
        /// <value>The bill to country.</value>
        [JsonProperty("BILLTOCOUNTRY"), DataMember(Name = "BILLTOCOUNTRY"), ApiMember(Name = "BILLTOCOUNTRY",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "BILLTOCOUNTRY (Optional): ")]
        public string? BillToCountry { get; set; }

        /// <summary>SHIPTOFIRSTNAME (Optional)<br/>
        /// Ship-to first name.<br/>
        /// Limitations: 30-character string.</summary>
        /// <value>The name of the ship to first.</value>
        [JsonProperty("SHIPTOFIRSTNAME"), DataMember(Name = "SHIPTOFIRSTNAME"), ApiMember(Name = "SHIPTOFIRSTNAME",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "SHIPTOFIRSTNAME (Optional): ")]
        public string? ShipToFirstName { get; set; }

        /// <summary>SHIPTOLASTNAME (Optional)<br/>
        /// Ship-to last name.<br/>
        /// Limitations: 30-character string.</summary>
        /// <value>The name of the ship to last.</value>
        [JsonProperty("SHIPTOLASTNAME"), DataMember(Name = "SHIPTOLASTNAME"), ApiMember(Name = "SHIPTOLASTNAME",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "SHIPTOLASTNAME (Optional): ")]
        public string? ShipToLastName { get; set; }

        /// <summary>SHIPTOSTREET (Optional)<br/>
        /// Ship-to street address.<br/>
        /// Limitations: 30-character string.</summary>
        /// <value>The ship to street.</value>
        [JsonProperty("SHIPTOSTREET"), DataMember(Name = "SHIPTOSTREET"), ApiMember(Name = "SHIPTOSTREET",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "SHIPTOSTREET (Optional): ")]
        public string? ShipToStreet { get; set; }

        /// <summary>SHIPTOCITY (Optional)<br/>
        /// Ship-to city.<br/>
        /// Limitations: 20-character string.</summary>
        /// <value>The ship to city.</value>
        [JsonProperty("SHIPTOCITY"), DataMember(Name = "SHIPTOCITY"), ApiMember(Name = "SHIPTOCITY",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "SHIPTOCITY (Optional): ")]
        public string? ShipToCity { get; set; }

        /// <summary>SHIPTOSTATE (Optional)<br/>
        /// Ship-to state.<br/>
        /// Limitations: 2-character string.</summary>
        /// <value>The ship to state.</value>
        [JsonProperty("SHIPTOSTATE"), DataMember(Name = "SHIPTOSTATE"), ApiMember(Name = "SHIPTOSTATE",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "SHIPTOSTATE (Optional): ")]
        public string? ShipToState { get; set; }

        /// <summary>SHIPTOZIP (Optional)<br/>
        /// Ship-to postal code.<br/>
        /// Limitations: 9-character string.</summary>
        /// <value>The ship to zip.</value>
        [JsonProperty("SHIPTOZIP"), DataMember(Name = "SHIPTOZIP"), ApiMember(Name = "SHIPTOZIP",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "SHIPTOZIP (Optional): ")]
        public string? ShipToZip { get; set; }

        /// <summary>SHIPTOCOUNTRY (Optional)<br/>
        /// Ship-to country. The Payflow API accepts 3-digit numeric country codes. Refer to the ISO 3166-1 numeric
        /// country codes.<br/>
        /// Limitations: 3-character country code.</summary>
        /// <value>The ship to country.</value>
        [JsonProperty("SHIPTOCOUNTRY"), DataMember(Name = "SHIPTOCOUNTRY"), ApiMember(Name = "SHIPTOCOUNTRY",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "SHIPTOCOUNTRY (Optional): ")]
        public string? ShipToCountry { get; set; }

        /// <summary>ORIGID (Required by some transaction types)<br/>
        /// ID of the original transaction referenced, or the value returned in the PNREF parameter in the original
        /// transaction; it appears as the Transaction ID in PayPal Manager reports.<br/>
        /// Limitations: 12 case-sensitive alphanumeric characters.</summary>
        /// <value>The identifier of the original transaction in PayPal.</value>
        [JsonProperty("ORIGID"), DataMember(Name = "ORIGID"), ApiMember(Name = "ORIGID",
             DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "(Required by some transaction types) ID of the original transaction referenced, or the value returned in the PNREF parameter in the original transaction; it appears as the Transaction ID in PayPal Manager reports. Limitations: 12 case-sensitive alphanumeric characters.")]
        public string? OriginalID { get; set; }
    }
}
