using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Currency
    {
        public Currency()
        {
            AccountCurrencies = new HashSet<AccountCurrency>();
            Accounts = new HashSet<Account>();
            BrandCurrencies = new HashSet<BrandCurrency>();
            CartItemOriginalCurrencies = new HashSet<CartItem>();
            CartItemSellingCurrencies = new HashSet<CartItem>();
            Categories = new HashSet<Category>();
            CountryCurrencies = new HashSet<CountryCurrency>();
            CurrencyConversionEndingCurrencies = new HashSet<CurrencyConversion>();
            CurrencyConversionStartingCurrencies = new HashSet<CurrencyConversion>();
            CurrencyImages = new HashSet<CurrencyImage>();
            DistrictCurrencies = new HashSet<DistrictCurrency>();
            FranchiseCurrencies = new HashSet<FranchiseCurrency>();
            HistoricalCurrencyRateEndingCurrencies = new HashSet<HistoricalCurrencyRate>();
            HistoricalCurrencyRateStartingCurrencies = new HashSet<HistoricalCurrencyRate>();
            Payments = new HashSet<Payment>();
            PriceRules = new HashSet<PriceRule>();
            ProductPricePoints = new HashSet<ProductPricePoint>();
            ProductRestockingFeeAmountCurrencies = new HashSet<Product>();
            ProductShipCarrierMethods = new HashSet<ProductShipCarrierMethod>();
            ProductTotalPurchasedAmountCurrencies = new HashSet<Product>();
            PurchaseOrderItemOriginalCurrencies = new HashSet<PurchaseOrderItem>();
            PurchaseOrderItemSellingCurrencies = new HashSet<PurchaseOrderItem>();
            RegionCurrencies = new HashSet<RegionCurrency>();
            SalesInvoiceItemOriginalCurrencies = new HashSet<SalesInvoiceItem>();
            SalesInvoiceItemSellingCurrencies = new HashSet<SalesInvoiceItem>();
            SalesOrderItemOriginalCurrencies = new HashSet<SalesOrderItem>();
            SalesOrderItemSellingCurrencies = new HashSet<SalesOrderItem>();
            SalesQuoteItemOriginalCurrencies = new HashSet<SalesQuoteItem>();
            SalesQuoteItemSellingCurrencies = new HashSet<SalesQuoteItem>();
            SalesReturnItemOriginalCurrencies = new HashSet<SalesReturnItem>();
            SalesReturnItemSellingCurrencies = new HashSet<SalesReturnItem>();
            SalesReturnReasons = new HashSet<SalesReturnReason>();
            SampleRequestItemOriginalCurrencies = new HashSet<SampleRequestItem>();
            SampleRequestItemSellingCurrencies = new HashSet<SampleRequestItem>();
            Users = new HashSet<User>();
            Wallets = new HashSet<Wallet>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal UnicodeSymbolValue { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? HtmlCharacterCode { get; set; }
        public string? RawCharacter { get; set; }
        public string? Iso4217alpha { get; set; }
        public int? Iso4217numeric { get; set; }
        public int? DecimalPlaceAccuracy { get; set; }
        public bool? UseSeparator { get; set; }
        public string? RawDecimalCharacter { get; set; }
        public string? HtmlDecimalCharacterCode { get; set; }
        public string? RawSeparatorCharacter { get; set; }
        public string? HtmlSeparatorCharacterCode { get; set; }

        public virtual ICollection<AccountCurrency> AccountCurrencies { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<BrandCurrency> BrandCurrencies { get; set; }
        public virtual ICollection<CartItem> CartItemOriginalCurrencies { get; set; }
        public virtual ICollection<CartItem> CartItemSellingCurrencies { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<CountryCurrency> CountryCurrencies { get; set; }
        public virtual ICollection<CurrencyConversion> CurrencyConversionEndingCurrencies { get; set; }
        public virtual ICollection<CurrencyConversion> CurrencyConversionStartingCurrencies { get; set; }
        public virtual ICollection<CurrencyImage> CurrencyImages { get; set; }
        public virtual ICollection<DistrictCurrency> DistrictCurrencies { get; set; }
        public virtual ICollection<FranchiseCurrency> FranchiseCurrencies { get; set; }
        public virtual ICollection<HistoricalCurrencyRate> HistoricalCurrencyRateEndingCurrencies { get; set; }
        public virtual ICollection<HistoricalCurrencyRate> HistoricalCurrencyRateStartingCurrencies { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PriceRule> PriceRules { get; set; }
        public virtual ICollection<ProductPricePoint> ProductPricePoints { get; set; }
        public virtual ICollection<Product> ProductRestockingFeeAmountCurrencies { get; set; }
        public virtual ICollection<ProductShipCarrierMethod> ProductShipCarrierMethods { get; set; }
        public virtual ICollection<Product> ProductTotalPurchasedAmountCurrencies { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItemOriginalCurrencies { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItemSellingCurrencies { get; set; }
        public virtual ICollection<RegionCurrency> RegionCurrencies { get; set; }
        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItemOriginalCurrencies { get; set; }
        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItemSellingCurrencies { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItemOriginalCurrencies { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItemSellingCurrencies { get; set; }
        public virtual ICollection<SalesQuoteItem> SalesQuoteItemOriginalCurrencies { get; set; }
        public virtual ICollection<SalesQuoteItem> SalesQuoteItemSellingCurrencies { get; set; }
        public virtual ICollection<SalesReturnItem> SalesReturnItemOriginalCurrencies { get; set; }
        public virtual ICollection<SalesReturnItem> SalesReturnItemSellingCurrencies { get; set; }
        public virtual ICollection<SalesReturnReason> SalesReturnReasons { get; set; }
        public virtual ICollection<SampleRequestItem> SampleRequestItemOriginalCurrencies { get; set; }
        public virtual ICollection<SampleRequestItem> SampleRequestItemSellingCurrencies { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
