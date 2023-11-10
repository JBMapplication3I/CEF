// <copyright file="BNGTransaction.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bng transaction class</summary>
// ReSharper disable CommentTypo, InconsistentNaming, MemberCanBeProtected.Global, UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global, StringLiteralTypo
namespace Clarity.Ecommerce.Providers.Payments.BNG
{
    /// <summary>A bng transaction.</summary>
    public class BNGTransaction
    {
        #region Required Fields
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        public string? Type { get; set; } // The type of transaction to be processed. Values: 'sale', 'auth', 'credit', 'validate', or 'offline'

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        public string? Username { get; set; } // Username assigned to merchant account.

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string? Password { get; set; } // Password for the specified username.

        /// <summary>Gets or sets the Cc number.</summary>
        /// <value>The Cc number.</value>
        public string? CCNumber { get; set; } // Credit card number.

        /// <summary>Gets or sets the identifier of the transaction.</summary>
        /// <value>The identifier of the transaction.</value>
        public string? TransactionID { get; set; } // Transaction ID.

        /// <summary>Gets or sets the Cc exponent.</summary>
        /// <value>The Cc exponent.</value>
        public string? CCExp { get; set; } // Credit card expiration date. Format: MMYY

        /// <summary>Gets or sets the cvv.</summary>
        /// <value>The cvv.</value>
        public string? CVV { get; set; } // The card security code. While this is not required, it is strongly recommended.

        /// <summary>Gets or sets the name of the check.</summary>
        /// <value>The name of the check.</value>
        public string? CheckName { get; set; } // The name on the customer's ACH account.

        /// <summary>Gets or sets the check a ba.</summary>
        /// <value>The check a ba.</value>
        public string? CheckABA { get; set; } // The customer's bank routing number.

        /// <summary>Gets or sets the check account.</summary>
        /// <value>The check account.</value>
        public string? CheckAccount { get; set; } // The customer's bank account number.

        /// <summary>Gets or sets the type of the account holder.</summary>
        /// <value>The type of the account holder.</value>
        public string? AccountHolderType { get; set; } // The type of ACH account the customer has. Values: 'business' or 'personal'

        /// <summary>Gets or sets the type of the account.</summary>
        /// <value>The type of the account.</value>
        public string? AccountType { get; set; } // The ACH account entity of the customer. Values: 'checking' or 'savings'

        /// <summary>Gets or sets the security code.</summary>
        /// <value>The security code.</value>
        public string? SECCode { get; set; } // The Standard Entry Class code of the ACH transaction. Values: 'PPD', 'WEB', 'TEL', or 'CCD'

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public string? Amount { get; set; } // Total amount to be charged. For validate, the amount must be omitted or set to 0.00. Format: x.xx

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        public string? Currency { get; set; } // The transaction currency.Format: ISO 4217

        /// <summary>Gets or sets the payment.</summary>
        /// <value>The payment.</value>
        public string? Payment { get; set; } // The type of payment. Default: 'creditcard' Values: 'creditcard' or 'check'

        /// <summary>Gets or sets the identifier of the processor.</summary>
        /// <value>The identifier of the processor.</value>
        public string? ProcessorID { get; set; } // If using Multiple MIDs, route to this processor (processor_id is obtained under Settings->Load Balancing in the Control Panel).

        /// <summary>Gets or sets the authorization code.</summary>
        /// <value>The authorization code.</value>
        public string? AuthorizationCode { get; set; } // Specify authorization code. For use with "offline" action only.

        /// <summary>Gets or sets the duplicate seconds.</summary>
        /// <value>The duplicate seconds.</value>
        public string? DUPSeconds { get; set; } // Sets the time in seconds for duplicate transaction checking on supported processors. Set to 0 to disable duplicate checking.

        /// <summary>Gets or sets the descriptor.</summary>
        /// <value>The descriptor.</value>
        public string? Descriptor { get; set; } // Set payment descriptor on supported processors.

        /// <summary>Gets or sets the descriptor phone.</summary>
        /// <value>The descriptor phone.</value>
        public string? DescriptorPhone { get; set; } // Set payment descriptor phone on supported processors.

        /// <summary>Gets or sets the descriptor address.</summary>
        /// <value>The descriptor address.</value>
        public string? DescriptorAddress { get; set; } // Set payment descriptor address on supported processors.

        /// <summary>Gets or sets the descriptor city.</summary>
        /// <value>The descriptor city.</value>
        public string? DescriptorCity { get; set; } // Set payment descriptor city on supported processors.

        /// <summary>Gets or sets the state of the descriptor.</summary>
        /// <value>The descriptor state.</value>
        public string? DescriptorState { get; set; } // Set payment descriptor state on supported processors.

        /// <summary>Gets or sets the descriptor postal.</summary>
        /// <value>The descriptor postal.</value>
        public string? DescriptorPostal { get; set; } // Set payment descriptor postal code on supported processors.

        /// <summary>Gets or sets the descriptor country.</summary>
        /// <value>The descriptor country.</value>
        public string? DescriptorCountry { get; set; } // Set payment descriptor country on supported processors.

        /// <summary>Gets or sets the descriptor mcc.</summary>
        /// <value>The descriptor mcc.</value>
        public string? DescriptorMCC { get; set; } // Set payment descriptor mcc on supported processors.

        /// <summary>Gets or sets the identifier of the descriptor merchant.</summary>
        /// <value>The identifier of the descriptor merchant.</value>
        public string? DescriptorMerchantID { get; set; } // Set payment descriptor merchant id on supported processors.

        /// <summary>Gets or sets URL of the descriptor.</summary>
        /// <value>The descriptor URL.</value>
        public string? DescriptorURL { get; set; } // Set payment descriptor url on supported processors.

        /// <summary>Gets or sets the billing method.</summary>
        /// <value>The billing method.</value>
        public string? BillingMethod { get; set; } // Should be set to 'recurring' to mark payment as a recurring transaction. Values:'recurring'

        /// <summary>Gets or sets information describing the order.</summary>
        /// <value>Information describing the order.</value>
        public string? OrderDescription { get; set; } // Order description. Legacy variable includes: orderdescription

        /// <summary>Gets or sets the identifier of the order.</summary>
        /// <value>The identifier of the order.</value>
        public string? OrderID { get; set; } // Order Id

        /// <summary>Gets or sets the IP address.</summary>
        /// <value>The IP address.</value>
        public string? IPAddress { get; set; } // IP address of cardholder, this field is recommended. Format: xxx.xxx.xxx.xxx

        /// <summary>Gets or sets the tax.</summary>
        /// <value>The tax.</value>
        public string? Tax { get; set; } // Total tax amount.

        /// <summary>Gets or sets the shipping.</summary>
        /// <value>The shipping.</value>
        public string? Shipping { get; set; } // Total shipping amount.

        /// <summary>Gets or sets the ponumber.</summary>
        /// <value>The ponumber.</value>
        public string? Ponumber { get; set; } // Original purchase order.

        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        public string? FirstName { get; set; } // Cardholder's first name. Legacy variable includes: firstname

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        public string? LastName { get; set; } // Cardholder's last name. Legacy variable includes: lastname

        /// <summary>Gets or sets the company.</summary>
        /// <value>The company.</value>
        public string? Company { get; set; } // Cardholder's company.

        /// <summary>Gets or sets the address 1.</summary>
        /// <value>The address 1.</value>
        public string? Address1 { get; set; } // Card billing address.

        /// <summary>Gets or sets the address 2.</summary>
        /// <value>The address 2.</value>
        public string? Address2 { get; set; } // Card billing address, line 2

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        public string? City { get; set; } // Card billing city.

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        public string? State { get; set; } // Card billing state. Format: CC

        /// <summary>Gets or sets the zip.</summary>
        /// <value>The zip.</value>
        public string? Zip { get; set; } // Card billing zip code.

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        public string? Country { get; set; } // Card billing country. Country codes are as shown in ISO 3166.Format: CC

        /// <summary>Gets or sets the phone.</summary>
        /// <value>The phone.</value>
        public string? Phone { get; set; } // Billing phone number.

        /// <summary>Gets or sets the fax.</summary>
        /// <value>The fax.</value>
        public string? Fax { get; set; } // Billing fax number.

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        public string? Email { get; set; } // Billing email address.

        /// <summary>Gets or sets the social security number.</summary>
        /// <value>The social security number.</value>
        public string? SocialSecurityNumber { get; set; } // Customer's social security number, checked against bad check writers database if check verification is enabled.

        /// <summary>Gets or sets the drivers license number.</summary>
        /// <value>The drivers license number.</value>
        public string? DriversLicenseNumber { get; set; } // Driver's license number.

        /// <summary>Gets or sets the drivers license dob.</summary>
        /// <value>The drivers license dob.</value>
        public string? DriversLicenseDOB { get; set; } // Driver's license date of birth.

        /// <summary>Gets or sets the state of the drivers license.</summary>
        /// <value>The drivers license state.</value>
        public string? DriversLicenseState { get; set; } // The state that issued the customer's driver's license.

        /// <summary>Gets or sets the shipping firstname.</summary>
        /// <value>The shipping firstname.</value>
        public string? ShippingFirstname { get; set; } // Shipping first name.

        /// <summary>Gets or sets the shipping lastname.</summary>
        /// <value>The shipping lastname.</value>
        public string? ShippingLastname { get; set; } // Shipping last name.

        /// <summary>Gets or sets the shipping company.</summary>
        /// <value>The shipping company.</value>
        public string? ShippingCompany { get; set; } // Shipping company.

        /// <summary>Gets or sets the shipping address 1.</summary>
        /// <value>The shipping address 1.</value>
        public string? ShippingAddress1 { get; set; } // Shipping address.

        /// <summary>Gets or sets the shipping address 2.</summary>
        /// <value>The shipping address 2.</value>
        public string? ShippingAddress2 { get; set; } // Shipping address, line 2.

        /// <summary>Gets or sets the shipping city.</summary>
        /// <value>The shipping city.</value>
        public string? ShippingCity { get; set; } // Shipping city.

        /// <summary>Gets or sets the state of the shipping.</summary>
        /// <value>The shipping state.</value>
        public string? ShippingState { get; set; } // Shipping state. Format: CC

        /// <summary>Gets or sets the shipping zip.</summary>
        /// <value>The shipping zip.</value>
        public string? ShippingZip { get; set; } // Shipping zip code.

        /// <summary>Gets or sets the shipping country.</summary>
        /// <value>The shipping country.</value>
        public string? ShippingCountry { get; set; } // Shipping country. Country codes are as shown in ISO 3166.Format: CC

        /// <summary>Gets or sets the shipping email.</summary>
        /// <value>The shipping email.</value>
        public string? ShippingEmail { get; set; } // Shipping email address

        /// <summary>Gets or sets the merchant defined field.</summary>
        /// <value>The merchant defined field.</value>
        public string? MerchantDefinedField { get; set; } // You can pass custom information in up to 20 fields. Format:

        /// <summary>Gets or sets the merchant defined field 1.</summary>
        /// <value>The merchant defined field 1.</value>
        public string? MerchantDefinedField1 { get; set; }

        /// <summary>Gets or sets the customer receipt.</summary>
        /// <value>The customer receipt.</value>
        public string? CustomerReceipt { get; set; } // If set to true, when the customer is charged, they will be sent a transaction receipt. Values: 'true' or 'false'
        #endregion

        #region Recurring
        /// <summary>Gets or sets the recurring.</summary>
        /// <value>The recurring.</value>
        public string? Recurring { get; set; } // Recurring action to be processed. Values: add_subscription

        /// <summary>Gets or sets the identifier of the plan.</summary>
        /// <value>The identifier of the plan.</value>
        public string? PlanID { get; set; } // Create a subscription tied to a Plan ID if the sale / auth transaction is successful.

        /// <summary>Gets or sets the plan payments.</summary>
        /// <value>The plan payments.</value>
        public string? PlanPayments { get; set; } // The number of payments before the recurring plan is complete. Note: Use '0' for 'until canceled'

        /// <summary>Gets or sets the plan amount.</summary>
        /// <value>The plan amount.</value>
        public string? PlanAmount { get; set; } // The plan amount to be charged each billing cycle. Format: x.xx

        /// <summary>Gets or sets the day frequency.</summary>
        /// <value>The day frequency.</value>
        public string? DayFrequency { get; set; } // How often, in days, to charge the customer.Cannot be set with 'month_frequency' or 'day_of_month'.

        /// <summary>Gets or sets the month frequency.</summary>
        /// <value>The month frequency.</value>
        public string? MonthFrequency { get; set; } // How often, in months, to charge the customer.Cannot be set with 'day_frequency'.Must be set with 'day_of_month'. Values: 1 through 24

        /// <summary>Gets or sets the day of month.</summary>
        /// <value>The day of month.</value>
        public string? DayOfMonth { get; set; } // The day that the customer will be charged.Cannot be set with 'day_frequency'.Must be set with 'month_frequency'. Values: 1 through 31 - for months without 29, 30, or 31 days, the charge will be on the last day

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        public string? StartDate { get; set; } // The first day that the customer will be charged. Format: YYYYMMDD
        #endregion

        #region Customer Vault specific fields
        /// <summary>Gets or sets the customer vault.</summary>
        /// <value>The customer vault.</value>
        public string? CustomerVault { get; set; } // Associate payment information with a Customer Vault record if the transaction is successful. Values: 'add_customer' or 'update_customer'

        /// <summary>Gets or sets the identifier of the customer vault.</summary>
        /// <value>The identifier of the customer vault.</value>
        public string? CustomerVaultID { get; set; } // Specifies a customer vault id. If not set, the payment gateway will randomly generate a customer vault id.
        #endregion

        #region Level III specific order fields
        /// <summary>Gets or sets the po number.</summary>
        /// <value>The po number.</value>
        public string? PONumber { get; set; }

        /// <summary>Gets or sets the shipping postal.</summary>
        /// <value>The shipping postal.</value>
        public string? ShippingPostal { get; set; } // Postal / ZIP code of the address where purchased goods will be delivered. This field can be identical to the 'ship_from_postal' if the customer is present and takes immediate possession of the goods.

        /// <summary>Gets or sets the ship from postal.</summary>
        /// <value>The ship from postal.</value>
        public string? ShipFromPostal { get; set; } // Postal / ZIP code of the address from where purchased goods are being shipped, defaults to merchant profile postal code.

        /// <summary>Gets or sets the summary commodity code.</summary>
        /// <value>The summary commodity code.</value>
        public string? SummaryCommodityCode { get; set; } // 4 character international description code of the overall goods or services being supplied. The acquirer or processor will provide a list of current codes.

        /// <summary>Gets or sets the duty amount.</summary>
        /// <value>The duty amount.</value>
        public string? DutyAmount { get; set; } // Amount included in the transaction amount associated with the import of purchased goods. Default: '0.00' Format: x.xx

        /// <summary>Gets or sets the discount amount.</summary>
        /// <value>The discount amount.</value>
        public string? DiscountAmount { get; set; } // Amount included in the transaction amount of any discount applied to complete order by the merchant. Default: '0.00' Format: x.xx

        /// <summary>Gets or sets the national tax amount.</summary>
        /// <value>The national tax amount.</value>
        public string? NationalTaxAmount { get; set; } // The national tax amount included in the transaction amount.

        /// <summary>Gets or sets the alternate tax amount.</summary>
        /// <value>The alternate tax amount.</value>
        public string? AlternateTaxAmount { get; set; } // Second tax amount included in the transaction amount in countries where more than one type of tax can be applied to the purchases. Default: '0.00' Format: x.xx

        /// <summary>Gets or sets the identifier of the alternate tax.</summary>
        /// <value>The identifier of the alternate tax.</value>
        public string? AlternateTaxID { get; set; } // Tax identification number of the merchant that reported the alternate tax amount.

        /// <summary>Gets or sets the vat tax amount.</summary>
        /// <value>The vat tax amount.</value>
        public string? VATTaxAmount { get; set; } // Contains the amount of any value added taxes which can be associated with the purchased item. Default: '0.00' Format: x.xx

        /// <summary>Gets or sets the vat tax rate.</summary>
        /// <value>The vat tax rate.</value>
        public string? VATTaxRate { get; set; } // Contains the tax rate used to calculate the sales tax amount appearing. Can contain up to 2 decimal places, e.g. 1 % = 1.00. Default: '0.00' Format: x.xx

        /// <summary>Gets or sets the vat invoice reference number.</summary>
        /// <value>The vat invoice reference number.</value>
        public string? VATInvoiceReferenceNumber { get; set; } // Invoice number that is associated with the VAT invoice.

        /// <summary>Gets or sets the customer vat registration.</summary>
        /// <value>The customer vat registration.</value>
        public string? CustomerVATRegistration { get; set; } // Value added tax registration number supplied by the cardholder.

        /// <summary>Gets or sets the merchant vat registration.</summary>
        /// <value>The merchant vat registration.</value>
        public string? MerchantVATRegistration { get; set; } // Government assigned tax identification number of the merchant for whom the goods or services were purchased from.

        /// <summary>Gets or sets the order date.</summary>
        /// <value>The order date.</value>
        public string? OrderDate { get; set; } // Purchase order date, defaults to the date of the transaction. Format: YYMMDD
        #endregion

        #region Level III specific line item detail fields
        /// <summary>Gets or sets the item product code.</summary>
        /// <value>The item product code.</value>
        public string? ItemProductCode { get; set; } // Merchant defined description code of the item being purchased.

        /// <summary>Gets or sets information describing the item.</summary>
        /// <value>Information describing the item.</value>
        public string? ItemDescription { get; set; } // Description of the item(s) being supplied. International description code of the individual good or service being supplied. The acquirer or processor will provide a list of current codes.

        /// <summary>Gets or sets the item unit of measure.</summary>
        /// <value>The item unit of measure.</value>
        public string? ItemUnitOfMeasure { get; set; } // Code for units of measurement as used in international trade. Default: 'EACH'

        /// <summary>Gets or sets the item unit cost.</summary>
        /// <value>The item unit cost.</value>
        public string? ItemUnitCost { get; set; } // Unit cost of item purchased, may contain up to 4 decimal places.

        /// <summary>Gets or sets the item quantity.</summary>
        /// <value>The item quantity.</value>
        public string? ItemQuantity { get; set; } // Quantity of the item(s) being purchased. Default: '1'

        /// <summary>Gets or sets the item total amount.</summary>
        /// <value>The item total amount.</value>
        public string? ItemTotalAmount { get; set; } // Purchase amount associated with the item. Defaults to: 'item_unit_cost_#' x 'item_quantity_#' rounded to the nearest penny.

        /// <summary>Gets or sets the item tax amount.</summary>
        /// <value>The item tax amount.</value>
        public string? ItemTaxAmount { get; set; } // Amount of tax on specific item, amount should not be included in 'total_amount_#'. Default: '0.00'

        /// <summary>Gets or sets the item tax rate.</summary>
        /// <value>The item tax rate.</value>
        public string? ItemTaxRate { get; set; } // Percentage representing the value-added tax applied. Default: '0.00'

        /// <summary>Gets or sets the item discount amount.</summary>
        /// <value>The item discount amount.</value>
        public string? ItemDiscountAmount { get; set; } // Discount amount which can have been applied by the merchant on the sale of the specific item. Amount should not be included in 'total_amount_#'.

        /// <summary>Gets or sets the item discount rate.</summary>
        /// <value>The item discount rate.</value>
        public string? ItemDiscountRate { get; set; } // Discount rate for the line item. 1% = 1.00. Default: '0.00'

        /// <summary>Gets or sets the type of the item tax.</summary>
        /// <value>The type of the item tax.</value>
        public string? ItemTaxType { get; set; } // Type of value-added taxes that are being used.

        /// <summary>Gets or sets the identifier of the item alternate tax.</summary>
        /// <value>The identifier of the item alternate tax.</value>
        public string? ItemAlternateTaxID { get; set; } // Tax identification number of the merchant that reported the alternate tax amount.
        #endregion
    }
}
