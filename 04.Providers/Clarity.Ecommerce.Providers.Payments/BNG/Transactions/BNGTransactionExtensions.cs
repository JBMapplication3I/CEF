// <copyright file="BNGTransactionExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bng transaction class</summary>
// ReSharper disable CommentTypo, InconsistentNaming, MemberCanBeProtected.Global, UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global, StringLiteralTypo
namespace Clarity.Ecommerce.Providers.Payments.BNG
{
    using System;
    using Utilities;

    /// <summary>A BNG transaction extensions.</summary>
    public static class BNGTransactionExtensions
    {
        /// <summary>A BNGTransaction extension method that converts a transaction to a request string.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="transaction">The transaction to act on.</param>
        /// <returns>transaction as a string.</returns>
        public static string ToRequestString(this BNGTransaction transaction)
        {
            Contract.RequiresNotNull(transaction);
            var request = string.Empty;
            request = AddCoreTransactionInfoToRequest(transaction, request);
            request = AddDescriptorInfoToRequest(transaction, request);
            if (!string.IsNullOrWhiteSpace(transaction.BillingMethod))
            {
                request += "&billing_method=" + transaction.BillingMethod;
            }
            if (!string.IsNullOrWhiteSpace(transaction.OrderDescription))
            {
                request += "&order_description=" + transaction.OrderDescription;
            }
            if (!string.IsNullOrWhiteSpace(transaction.OrderID))
            {
                request += "&orderid=" + transaction.OrderID;
            }
            if (!string.IsNullOrWhiteSpace(transaction.IPAddress))
            {
                request += "&ipaddress=" + transaction.IPAddress;
            }
            request = AddOrderAddressInfoToRequest(transaction, request);
            if (!string.IsNullOrWhiteSpace(transaction.SocialSecurityNumber))
            {
                request += "&social_security_number=" + transaction.SocialSecurityNumber;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DriversLicenseNumber))
            {
                request += "&drivers_license_number=" + transaction.DriversLicenseNumber;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DriversLicenseDOB))
            {
                request += "&drivers_license_dob=" + transaction.DriversLicenseDOB;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DriversLicenseState))
            {
                request += "&drivers_license_state=" + transaction.DriversLicenseState;
            }
            request = AddShippingInfoToRequest(transaction, request);
            if (!string.IsNullOrWhiteSpace(transaction.MerchantDefinedField))
            {
                request += "&merchant_defined_field_#=" + transaction.MerchantDefinedField;
            }
            if (!string.IsNullOrWhiteSpace(transaction.CustomerReceipt))
            {
                request += "&customer_receipt=" + transaction.CustomerReceipt;
            }
            request = AddSubscriptionInfoToRequest(transaction, request);
            if (!string.IsNullOrWhiteSpace(transaction.CustomerVault))
            {
                request += "&customer_vault=" + transaction.CustomerVault;
            }
            if (!string.IsNullOrWhiteSpace(transaction.CustomerVaultID))
            {
                request += "&customer_vault_id=" + transaction.CustomerVaultID;
            }
            if (!string.IsNullOrWhiteSpace(transaction.PONumber))
            {
                request += "&ponumber=" + transaction.PONumber;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingPostal))
            {
                request += "&shipping_postal=" + transaction.ShippingPostal;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShipFromPostal))
            {
                request += "&ship_from_postal=" + transaction.ShipFromPostal;
            }
            if (!string.IsNullOrWhiteSpace(transaction.SummaryCommodityCode))
            {
                request += "&summary_commodity_code=" + transaction.SummaryCommodityCode;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DutyAmount))
            {
                request += "&duty_amount=" + transaction.DutyAmount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DiscountAmount))
            {
                request += "&discount_amount=" + transaction.DiscountAmount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.NationalTaxAmount))
            {
                request += "&national_tax_amount=" + transaction.NationalTaxAmount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.AlternateTaxAmount))
            {
                request += "&alternate_tax_amount=" + transaction.AlternateTaxAmount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.AlternateTaxID))
            {
                request += "&alternate_tax_id=" + transaction.AlternateTaxID;
            }
            if (!string.IsNullOrWhiteSpace(transaction.VATTaxAmount))
            {
                request += "&vat_tax_amount=" + transaction.VATTaxAmount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.VATTaxRate))
            {
                request += "&vat_tax_rate=" + transaction.VATTaxRate;
            }
            if (!string.IsNullOrWhiteSpace(transaction.VATInvoiceReferenceNumber))
            {
                request += "&vat_invoice_reference_number=" + transaction.VATInvoiceReferenceNumber;
            }
            if (!string.IsNullOrWhiteSpace(transaction.CustomerVATRegistration))
            {
                request += "&customer_vat_registration=" + transaction.CustomerVATRegistration;
            }
            if (!string.IsNullOrWhiteSpace(transaction.MerchantVATRegistration))
            {
                request += "&merchant_vat_registration=" + transaction.MerchantVATRegistration;
            }
            if (!string.IsNullOrWhiteSpace(transaction.OrderDate))
            {
                request += "&order_date=" + transaction.OrderDate;
            }
            request = AddItemInfoToRequest(transaction, request);
            return request;
        }

        private static string AddSubscriptionInfoToRequest(BNGTransaction transaction, string request)
        {
            if (!string.IsNullOrWhiteSpace(transaction.Recurring))
            {
                request += "&recurring=" + transaction.Recurring;
            }
            if (!string.IsNullOrWhiteSpace(transaction.PlanID))
            {
                request += "&plan_id=" + transaction.PlanID;
            }
            if (!string.IsNullOrWhiteSpace(transaction.PlanPayments))
            {
                request += "&plan_payments=" + transaction.PlanPayments;
            }
            if (!string.IsNullOrWhiteSpace(transaction.PlanAmount))
            {
                request += "&plan_amount=" + transaction.PlanAmount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DayFrequency))
            {
                request += "&day_frequency=" + transaction.DayFrequency;
            }
            if (!string.IsNullOrWhiteSpace(transaction.MonthFrequency))
            {
                request += "&month_frequency=" + transaction.MonthFrequency;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DayOfMonth))
            {
                request += "&day_of_month=" + transaction.DayOfMonth;
            }
            if (!string.IsNullOrWhiteSpace(transaction.StartDate))
            {
                request += "&start_date=" + transaction.StartDate;
            }
            return request;
        }

        private static string AddOrderAddressInfoToRequest(BNGTransaction transaction, string request)
        {
            if (!string.IsNullOrWhiteSpace(transaction.Tax))
            {
                request += "&tax=" + transaction.Tax;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Shipping))
            {
                request += "&shipping=" + transaction.Shipping;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Ponumber))
            {
                request += "&ponumber=" + transaction.Ponumber;
            }
            if (!string.IsNullOrWhiteSpace(transaction.FirstName))
            {
                request += "&first_name=" + transaction.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(transaction.LastName))
            {
                request += "&last_name=" + transaction.LastName;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Company))
            {
                request += "&company=" + transaction.Company;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Address1))
            {
                request += "&address1=" + transaction.Address1;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Address2))
            {
                request += "&address2=" + transaction.Address2;
            }
            if (!string.IsNullOrWhiteSpace(transaction.City))
            {
                request += "&city=" + transaction.City;
            }
            if (!string.IsNullOrWhiteSpace(transaction.State))
            {
                request += "&state=" + transaction.State;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Zip))
            {
                request += "&zip=" + transaction.Zip;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Country))
            {
                request += "&country=" + transaction.Country;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Phone))
            {
                request += "&phone=" + transaction.Phone;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Fax))
            {
                request += "&fax=" + transaction.Fax;
            }
            return request;
        }

        private static string AddItemInfoToRequest(BNGTransaction transaction, string request)
        {
            if (!string.IsNullOrWhiteSpace(transaction.ItemProductCode))
            {
                request += "&item_product_code_#=" + transaction.ItemProductCode;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemDescription))
            {
                request += "&item_description_#=" + transaction.ItemDescription;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemUnitOfMeasure))
            {
                request += "&tem_unit_of_measure_#=" + transaction.ItemUnitOfMeasure;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemUnitCost))
            {
                request += "&item_unit_cost_#=" + transaction.ItemUnitCost;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemQuantity))
            {
                request += "&item_quantity_#=" + transaction.ItemQuantity;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemTotalAmount))
            {
                request += "&item_total_amount_#=" + transaction.ItemTotalAmount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemTaxAmount))
            {
                request += "&item_tax_amount_#=" + transaction.ItemTaxAmount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemTaxRate))
            {
                request += "&item_tax_rate_#=" + transaction.ItemTaxRate;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemDiscountAmount))
            {
                request += "&item_discount_amount_#=" + transaction.ItemDiscountAmount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemDiscountRate))
            {
                request += "&item_discount_rate_#=" + transaction.ItemDiscountRate;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemTaxType))
            {
                request += "&item_tax_type_#=" + transaction.ItemTaxType;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ItemAlternateTaxID))
            {
                request += "&item_alternate_tax_id_#=" + transaction.ItemAlternateTaxID;
            }
            return request;
        }

        private static string AddShippingInfoToRequest(BNGTransaction transaction, string request)
        {
            if (!string.IsNullOrWhiteSpace(transaction.ShippingFirstname))
            {
                request += "&shipping_firstname=" + transaction.ShippingFirstname;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingLastname))
            {
                request += "&shipping_lastname=" + transaction.ShippingLastname;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingCompany))
            {
                request += "&shipping_company=" + transaction.ShippingCompany;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingAddress1))
            {
                request += "&shipping_address1=" + transaction.ShippingAddress1;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingAddress2))
            {
                request += "&shipping_address2=" + transaction.ShippingAddress2;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingCity))
            {
                request += "&shipping_city=" + transaction.ShippingCity;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingState))
            {
                request += "&shipping_state=" + transaction.ShippingState;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingZip))
            {
                request += "&shipping_zip=" + transaction.ShippingZip;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingCountry))
            {
                request += "&shipping_country=" + transaction.ShippingCountry;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ShippingEmail))
            {
                request += "&shipping_email=" + transaction.ShippingEmail;
            }
            return request;
        }

        private static string AddDescriptorInfoToRequest(BNGTransaction transaction, string request)
        {
            if (!string.IsNullOrWhiteSpace(transaction.Descriptor))
            {
                request += "&descriptor=" + transaction.Descriptor;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DescriptorPhone))
            {
                request += "&descriptor_phone=" + transaction.DescriptorPhone;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DescriptorAddress))
            {
                request += "&descriptor_address=" + transaction.DescriptorAddress;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DescriptorCity))
            {
                request += "&descriptor_city=" + transaction.DescriptorCity;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DescriptorState))
            {
                request += "&descriptor_state=" + transaction.DescriptorState;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DescriptorPostal))
            {
                request += "&descriptor_postal=" + transaction.DescriptorPostal;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DescriptorCountry))
            {
                request += "&descriptor_country=" + transaction.DescriptorCountry;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DescriptorMCC))
            {
                request += "&descriptor_mcc=" + transaction.DescriptorMCC;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DescriptorMerchantID))
            {
                request += "&descriptor_merchant_id=" + transaction.DescriptorMerchantID;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DescriptorURL))
            {
                request += "&descriptor_url=" + transaction.DescriptorURL;
            }
            return request;
        }

        private static string AddCoreTransactionInfoToRequest(BNGTransaction transaction, string request)
        {
            if (!string.IsNullOrWhiteSpace(transaction.Username))
            {
                request += "username=" + transaction.Username;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Password))
            {
                request += "&password=" + transaction.Password;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Type))
            {
                request += "&type=" + transaction.Type;
            }
            if (!string.IsNullOrWhiteSpace(transaction.CCNumber))
            {
                request += "&ccnumber=" + transaction.CCNumber;
            }
            if (!string.IsNullOrWhiteSpace(transaction.TransactionID))
            {
                request += "&transactionID=" + transaction.TransactionID;
            }
            if (!string.IsNullOrWhiteSpace(transaction.CCExp))
            {
                request += "&ccexp=" + transaction.CCExp;
            }
            if (!string.IsNullOrWhiteSpace(transaction.CVV))
            {
                request += "&cvv=" + transaction.CVV;
            }
            if (!string.IsNullOrWhiteSpace(transaction.CheckName))
            {
                request += "&checkname=" + transaction.CheckName;
            }
            if (!string.IsNullOrWhiteSpace(transaction.CheckABA))
            {
                request += "&checkaba=" + transaction.CheckABA;
            }
            if (!string.IsNullOrWhiteSpace(transaction.CheckAccount))
            {
                request += "&checkaccount=" + transaction.CheckAccount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.AccountHolderType))
            {
                request += "&account_holder_type=" + transaction.AccountHolderType;
            }
            if (!string.IsNullOrWhiteSpace(transaction.AccountType))
            {
                request += "&account_type=" + transaction.AccountType;
            }
            if (!string.IsNullOrWhiteSpace(transaction.SECCode))
            {
                request += "&sec_code=" + transaction.SECCode;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Amount))
            {
                request += "&amount=" + transaction.Amount;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Currency))
            {
                request += "&currency=" + transaction.Currency;
            }
            if (!string.IsNullOrWhiteSpace(transaction.Payment))
            {
                request += "&payment=" + transaction.Payment;
            }
            if (!string.IsNullOrWhiteSpace(transaction.ProcessorID))
            {
                request += "&processor_id=" + transaction.ProcessorID;
            }
            if (!string.IsNullOrWhiteSpace(transaction.AuthorizationCode))
            {
                request += "&authorization_code=" + transaction.AuthorizationCode;
            }
            if (!string.IsNullOrWhiteSpace(transaction.DUPSeconds))
            {
                request += "&dup_seconds=" + transaction.DUPSeconds;
            }
            return request;
        }
    }
}
