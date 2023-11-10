// <copyright file="ProviderType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ProviderType class</summary>
// <remarks>This enum has been reviewed and is not tied to a database id
// (where-in it would have needed to be reviewed to convert to a table).</remarks>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent file entity types.</summary>
    public enum ProviderType
    {
        /// <summary>An enum constant representing the address validation option.</summary>
        AddressValidation,

        /// <summary>An enum constant representing the api option.</summary>
        Api,

        /// <summary>An enum constant representing the files option.</summary>
        Files,

        /// <summary>An enum constant representing the inventory option.</summary>
        Inventory,

        /// <summary>An enum constant representing the sales invoice actions handler option.</summary>
        SalesInvoiceActionsHandler,

        /// <summary>An enum constant representing the sales invoice queries handler option.</summary>
        SalesInvoiceQueriesHandler,

        /// <summary>An enum constant representing the sales quote checkout option.</summary>
        SalesQuoteCheckout,

        /// <summary>An enum constant representing the sales quote actions handler option.</summary>
        SalesQuoteActionsHandler,

        /// <summary>An enum constant representing the sales quote queries handler option.</summary>
        SalesQuoteQueriesHandler,

        /// <summary>An enum constant representing the sales return actions handler option.</summary>
        SalesReturnActionsHandler,

        /// <summary>An enum constant representing the sales return queries handler option.</summary>
        SalesReturnQueriesHandler,

        /// <summary>An enum constant representing the sample request checkout option.</summary>
        SampleRequestCheckout,

        /// <summary>An enum constant representing the sample request actions handler option.</summary>
        SampleRequestActionsHandler,

        /// <summary>An enum constant representing the sample request queries handler option.</summary>
        SampleRequestQueriesHandler,

        /// <summary>An enum constant representing the memberships option.</summary>
        Memberships,

        /// <summary>An enum constant representing the packaging option.</summary>
        Packaging,

        /// <summary>An enum constant representing the payments option.</summary>
        Payments,

        /// <summary>An enum constant representing the pricing option.</summary>
        Pricing,

        /// <summary>An enum constant representing the shipping option.</summary>
        Shipping,

        /// <summary> Adds charges on top of payments being made. </summary>
        Surcharges,

        /// <summary>An enum constant representing the taxes option.</summary>
        Taxes,

        /// <summary>An enum constant representing the importer option.</summary>
        Importer,

        /// <summary>An enum constant representing the currency conversions option.</summary>
        CurrencyConversions,

        /// <summary>An enum constant representing the searching option.</summary>
        Searching,

        /// <summary>An enum constant representing the personalization option.</summary>
        Personalization,

        /// <summary>An enum constant representing the checkouts option.</summary>
        Checkouts,

        /// <summary>An enum constant representing the chatting option.</summary>
        Chatting,

        /// <summary>An enum constant representing the survey option.</summary>
        Survey,

        /// <summary>An enum constant representing the sales quote importer option.</summary>
        SalesQuoteImporter,

        /// <summary>An enum constant representing the proxy option.</summary>
        Proxy,

        /// <summary>An enum constant representing the caching option.</summary>
        Caching,

        /// <summary>An enum constant representing the vin lookup option.</summary>
        VINLookup,
    }
}
