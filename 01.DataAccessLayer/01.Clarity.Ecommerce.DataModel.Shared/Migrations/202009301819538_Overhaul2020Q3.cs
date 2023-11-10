// <copyright file="202009301819538_Overhaul2020Q3.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202009301819538 overhaul 2020 q 3 class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Overhaul2020Q3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Geography.DistrictCurrency", "MasterID", "Geography.District");
            DropForeignKey("Geography.DistrictCurrency", "SlaveID", "Currencies.Currency");
            DropForeignKey("Geography.DistrictImage", "MasterID", "Geography.District");
            DropForeignKey("Geography.DistrictImage", "TypeID", "Geography.DistrictImageType");
            DropForeignKey("Geography.DistrictLanguage", "MasterID", "Geography.District");
            DropForeignKey("Geography.DistrictLanguage", "SlaveID", "Globalization.Language");
            DropForeignKey("Geography.District", "RegionID", "Geography.Region");
            DropForeignKey("Tax.TaxDistrict", "DistrictID", "Geography.District");
            DropForeignKey("Geography.InterRegion", "KeyRegionID", "Geography.Region");
            DropForeignKey("Geography.InterRegion", "RelationRegionID", "Geography.Region");
            DropForeignKey("Geography.Address", "DistrictID", "Geography.District");
            DropForeignKey("Stores.StoreCategory", "MasterID", "Stores.Store");
            DropForeignKey("Vendors.Vendor", "ContactMethodID", "Contacts.ContactMethod");
            DropForeignKey("Shopping.Cart", "ParentID", "Shopping.Cart");
            DropForeignKey("Quoting.SalesQuote", "ParentID", "Quoting.SalesQuote");
            DropForeignKey("Returning.SalesReturn", "ParentID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnItem", "StatusID", "Returning.SalesReturnItemStatus");
            DropForeignKey("Shipping.CarrierInvoice", "ShipCarrierID", "Shipping.ShipCarrier");
            DropForeignKey("Shipping.CarrierOrigin", "ShipCarrierID", "Shipping.ShipCarrier");
            DropForeignKey("Quoting.SalesQuoteItem", "StatusID", "Quoting.SalesQuoteItemStatus");
            DropForeignKey("Sampling.SampleRequest", "ParentID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequestItem", "StatusID", "Sampling.SampleRequestItemStatus");
            DropForeignKey("Purchasing.PurchaseOrderItem", "StatusID", "Purchasing.PurchaseOrderItemStatus");
            DropForeignKey("Invoicing.SalesInvoiceItem", "StatusID", "Invoicing.SalesInvoiceItemStatus");
            DropForeignKey("Ordering.SalesOrderItem", "StatusID", "Ordering.SalesOrderItemStatus");
            DropForeignKey("Brands.BrandUserType", "MasterID", "Brands.Brand");
            DropForeignKey("Brands.BrandUserType", "SlaveID", "Contacts.UserType");
            DropForeignKey("Stores.StoreUserType", "MasterID", "Stores.Store");
            DropForeignKey("Stores.StoreUserType", "SlaveID", "Contacts.UserType");
            DropForeignKey("Products.ProductType", "BrandID", "Brands.Brand");
            DropForeignKey("Products.ProductType", "StoreID", "Stores.Store");
            DropForeignKey("Shopping.CartItem", "StatusID", "Shopping.CartItemStatus");
            DropForeignKey("Products.ProductRestriction", "RestrictionsApplyToDistrictID", "Geography.District");
            DropForeignKey("Brands.BrandCategoryType", "MasterID", "Brands.Brand");
            DropForeignKey("Brands.BrandCategoryType", "SlaveID", "Categories.CategoryType");
            DropForeignKey("Categories.CategoryType", "ParentID", "Categories.CategoryType");
            DropForeignKey("Stores.StoreCategoryType", "MasterID", "Stores.Store");
            DropForeignKey("Stores.StoreCategoryType", "SlaveID", "Categories.CategoryType");
            DropForeignKey("Stores.StoreCategory", "SlaveID", "Categories.Category");
            DropForeignKey("Stores.StoreSiteDomain", "MasterID", "Stores.Store");
            DropForeignKey("Stores.StoreSiteDomain", "SlaveID", "Stores.SiteDomain");
            DropForeignKey("Invoicing.SalesInvoice", "ParentID", "Invoicing.SalesInvoice");
            DropForeignKey("Ordering.SalesOrder", "ParentID", "Ordering.SalesOrder");
            DropForeignKey("Purchasing.PurchaseOrder", "ParentID", "Purchasing.PurchaseOrder");
            DropForeignKey("Accounts.Account", "ParentID", "Accounts.Account");
            DropForeignKey("Geography.PhonePrefixLookup", "DistrictID", "Geography.District");
            DropIndex("Accounts.Account", new[] { "ParentID" });
            DropIndex("Accounts.Account", new[] { "Phone" });
            DropIndex("Accounts.Account", new[] { "Fax" });
            DropIndex("Accounts.Account", new[] { "Email" });
            DropIndex("Geography.Address", new[] { "DistrictID" });
            DropIndex("Geography.District", new[] { "ID" });
            DropIndex("Geography.District", new[] { "RegionID" });
            DropIndex("Geography.District", new[] { "Name" });
            DropIndex("Geography.District", new[] { "CustomKey" });
            DropIndex("Geography.District", new[] { "CreatedDate" });
            DropIndex("Geography.District", new[] { "UpdatedDate" });
            DropIndex("Geography.District", new[] { "Active" });
            DropIndex("Geography.District", new[] { "Hash" });
            DropIndex("Geography.DistrictCurrency", new[] { "ID" });
            DropIndex("Geography.DistrictCurrency", new[] { "MasterID" });
            DropIndex("Geography.DistrictCurrency", new[] { "SlaveID" });
            DropIndex("Geography.DistrictCurrency", new[] { "CustomKey" });
            DropIndex("Geography.DistrictCurrency", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictCurrency", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictCurrency", new[] { "Active" });
            DropIndex("Geography.DistrictCurrency", new[] { "Hash" });
            DropIndex("Geography.DistrictImage", new[] { "ID" });
            DropIndex("Geography.DistrictImage", new[] { "MasterID" });
            DropIndex("Geography.DistrictImage", new[] { "TypeID" });
            DropIndex("Geography.DistrictImage", new[] { "Name" });
            DropIndex("Geography.DistrictImage", new[] { "CustomKey" });
            DropIndex("Geography.DistrictImage", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictImage", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictImage", new[] { "Active" });
            DropIndex("Geography.DistrictImage", new[] { "Hash" });
            DropIndex("Geography.DistrictImageType", new[] { "ID" });
            DropIndex("Geography.DistrictImageType", new[] { "DisplayName" });
            DropIndex("Geography.DistrictImageType", new[] { "SortOrder" });
            DropIndex("Geography.DistrictImageType", new[] { "Name" });
            DropIndex("Geography.DistrictImageType", new[] { "CustomKey" });
            DropIndex("Geography.DistrictImageType", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictImageType", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictImageType", new[] { "Active" });
            DropIndex("Geography.DistrictImageType", new[] { "Hash" });
            DropIndex("Geography.DistrictLanguage", new[] { "ID" });
            DropIndex("Geography.DistrictLanguage", new[] { "MasterID" });
            DropIndex("Geography.DistrictLanguage", new[] { "SlaveID" });
            DropIndex("Geography.DistrictLanguage", new[] { "CustomKey" });
            DropIndex("Geography.DistrictLanguage", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictLanguage", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictLanguage", new[] { "Active" });
            DropIndex("Geography.DistrictLanguage", new[] { "Hash" });
            DropIndex("Tax.TaxDistrict", new[] { "ID" });
            DropIndex("Tax.TaxDistrict", new[] { "DistrictID" });
            DropIndex("Tax.TaxDistrict", new[] { "Name" });
            DropIndex("Tax.TaxDistrict", new[] { "CustomKey" });
            DropIndex("Tax.TaxDistrict", new[] { "CreatedDate" });
            DropIndex("Tax.TaxDistrict", new[] { "UpdatedDate" });
            DropIndex("Tax.TaxDistrict", new[] { "Active" });
            DropIndex("Tax.TaxDistrict", new[] { "Hash" });
            DropIndex("Geography.InterRegion", new[] { "ID" });
            DropIndex("Geography.InterRegion", new[] { "KeyRegionID" });
            DropIndex("Geography.InterRegion", new[] { "RelationRegionID" });
            DropIndex("Geography.InterRegion", new[] { "CustomKey" });
            DropIndex("Geography.InterRegion", new[] { "CreatedDate" });
            DropIndex("Geography.InterRegion", new[] { "UpdatedDate" });
            DropIndex("Geography.InterRegion", new[] { "Active" });
            DropIndex("Geography.InterRegion", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "ParentID" });
            DropIndex("Ordering.SalesOrder", new[] { "ParentID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "ParentID" });
            DropIndex("Stores.StoreCategory", new[] { "ID" });
            DropIndex("Stores.StoreCategory", new[] { "MasterID" });
            DropIndex("Stores.StoreCategory", new[] { "SlaveID" });
            DropIndex("Stores.StoreCategory", new[] { "CustomKey" });
            DropIndex("Stores.StoreCategory", new[] { "CreatedDate" });
            DropIndex("Stores.StoreCategory", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreCategory", new[] { "Active" });
            DropIndex("Stores.StoreCategory", new[] { "Hash" });
            DropIndex("Shopping.CartItem", new[] { "StatusID" });
            DropIndex("Vendors.Vendor", new[] { "ContactMethodID" });
            DropIndex("Contacts.ContactMethod", new[] { "ID" });
            DropIndex("Contacts.ContactMethod", new[] { "DisplayName" });
            DropIndex("Contacts.ContactMethod", new[] { "SortOrder" });
            DropIndex("Contacts.ContactMethod", new[] { "Name" });
            DropIndex("Contacts.ContactMethod", new[] { "CustomKey" });
            DropIndex("Contacts.ContactMethod", new[] { "CreatedDate" });
            DropIndex("Contacts.ContactMethod", new[] { "UpdatedDate" });
            DropIndex("Contacts.ContactMethod", new[] { "Active" });
            DropIndex("Contacts.ContactMethod", new[] { "Hash" });
            DropIndex("Shopping.Cart", new[] { "ParentID" });
            DropIndex("Quoting.SalesQuote", new[] { "ParentID" });
            DropIndex("Returning.SalesReturn", new[] { "ParentID" });
            DropIndex("Returning.SalesReturnItem", new[] { "StatusID" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "ID" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "DisplayName" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "SortOrder" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "Name" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "CreatedDate" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "Active" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "Hash" });
            DropIndex("Shipping.CarrierInvoice", new[] { "ID" });
            DropIndex("Shipping.CarrierInvoice", new[] { "ShipCarrierID" });
            DropIndex("Shipping.CarrierInvoice", new[] { "CustomKey" });
            DropIndex("Shipping.CarrierInvoice", new[] { "CreatedDate" });
            DropIndex("Shipping.CarrierInvoice", new[] { "UpdatedDate" });
            DropIndex("Shipping.CarrierInvoice", new[] { "Active" });
            DropIndex("Shipping.CarrierInvoice", new[] { "Hash" });
            DropIndex("Shipping.CarrierOrigin", new[] { "ID" });
            DropIndex("Shipping.CarrierOrigin", new[] { "ShipCarrierID" });
            DropIndex("Shipping.CarrierOrigin", new[] { "CustomKey" });
            DropIndex("Shipping.CarrierOrigin", new[] { "CreatedDate" });
            DropIndex("Shipping.CarrierOrigin", new[] { "UpdatedDate" });
            DropIndex("Shipping.CarrierOrigin", new[] { "Active" });
            DropIndex("Shipping.CarrierOrigin", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "StatusID" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "DisplayName" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "SortOrder" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "CreatedDate" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "Hash" });
            DropIndex("Sampling.SampleRequest", new[] { "ParentID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "StatusID" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "ID" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "DisplayName" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "SortOrder" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "Name" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "CreatedDate" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "Active" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "StatusID" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "DisplayName" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "SortOrder" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "StatusID" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "DisplayName" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "SortOrder" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "CreatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderItem", new[] { "StatusID" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "ID" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "DisplayName" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "SortOrder" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "Name" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "CreatedDate" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "Active" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "Hash" });
            DropIndex("Brands.BrandUserType", new[] { "ID" });
            DropIndex("Brands.BrandUserType", new[] { "MasterID" });
            DropIndex("Brands.BrandUserType", new[] { "SlaveID" });
            DropIndex("Brands.BrandUserType", new[] { "CustomKey" });
            DropIndex("Brands.BrandUserType", new[] { "CreatedDate" });
            DropIndex("Brands.BrandUserType", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandUserType", new[] { "Active" });
            DropIndex("Brands.BrandUserType", new[] { "Hash" });
            DropIndex("Stores.StoreUserType", new[] { "ID" });
            DropIndex("Stores.StoreUserType", new[] { "MasterID" });
            DropIndex("Stores.StoreUserType", new[] { "SlaveID" });
            DropIndex("Stores.StoreUserType", new[] { "CustomKey" });
            DropIndex("Stores.StoreUserType", new[] { "CreatedDate" });
            DropIndex("Stores.StoreUserType", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreUserType", new[] { "Active" });
            DropIndex("Stores.StoreUserType", new[] { "Hash" });
            DropIndex("Products.ProductType", new[] { "StoreID" });
            DropIndex("Products.ProductType", new[] { "BrandID" });
            DropIndex("Shopping.CartItemStatus", new[] { "ID" });
            DropIndex("Shopping.CartItemStatus", new[] { "DisplayName" });
            DropIndex("Shopping.CartItemStatus", new[] { "SortOrder" });
            DropIndex("Shopping.CartItemStatus", new[] { "Name" });
            DropIndex("Shopping.CartItemStatus", new[] { "CustomKey" });
            DropIndex("Shopping.CartItemStatus", new[] { "CreatedDate" });
            DropIndex("Shopping.CartItemStatus", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartItemStatus", new[] { "Active" });
            DropIndex("Shopping.CartItemStatus", new[] { "Hash" });
            DropIndex("Products.ProductRestriction", new[] { "RestrictionsApplyToDistrictID" });
            DropIndex("Categories.CategoryType", new[] { "ParentID" });
            DropIndex("Brands.BrandCategoryType", new[] { "ID" });
            DropIndex("Brands.BrandCategoryType", new[] { "MasterID" });
            DropIndex("Brands.BrandCategoryType", new[] { "SlaveID" });
            DropIndex("Brands.BrandCategoryType", new[] { "CustomKey" });
            DropIndex("Brands.BrandCategoryType", new[] { "CreatedDate" });
            DropIndex("Brands.BrandCategoryType", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandCategoryType", new[] { "Active" });
            DropIndex("Brands.BrandCategoryType", new[] { "Hash" });
            DropIndex("Stores.StoreCategoryType", new[] { "ID" });
            DropIndex("Stores.StoreCategoryType", new[] { "MasterID" });
            DropIndex("Stores.StoreCategoryType", new[] { "SlaveID" });
            DropIndex("Stores.StoreCategoryType", new[] { "CustomKey" });
            DropIndex("Stores.StoreCategoryType", new[] { "CreatedDate" });
            DropIndex("Stores.StoreCategoryType", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreCategoryType", new[] { "Active" });
            DropIndex("Stores.StoreCategoryType", new[] { "Hash" });
            DropIndex("Stores.StoreSiteDomain", new[] { "ID" });
            DropIndex("Stores.StoreSiteDomain", new[] { "MasterID" });
            DropIndex("Stores.StoreSiteDomain", new[] { "SlaveID" });
            DropIndex("Stores.StoreSiteDomain", new[] { "CustomKey" });
            DropIndex("Stores.StoreSiteDomain", new[] { "CreatedDate" });
            DropIndex("Stores.StoreSiteDomain", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreSiteDomain", new[] { "Active" });
            DropIndex("Stores.StoreSiteDomain", new[] { "Hash" });
            DropIndex("Geography.PhonePrefixLookup", new[] { "DistrictID" });
            AddColumn("Currencies.Currency", "HtmlCharacterCode", c => c.String(maxLength: 12, unicode: false));
            AddColumn("Currencies.Currency", "RawCharacter", c => c.String(maxLength: 5));
            DropColumn("Accounts.Account", "ParentID");
            DropColumn("Accounts.Account", "SeoUrl");
            DropColumn("Accounts.Account", "SeoKeywords");
            DropColumn("Accounts.Account", "SeoPageTitle");
            DropColumn("Accounts.Account", "SeoDescription");
            DropColumn("Accounts.Account", "SeoMetaData");
            DropColumn("Accounts.Account", "Phone");
            DropColumn("Accounts.Account", "Fax");
            DropColumn("Accounts.Account", "Email");
            DropColumn("Contacts.Contact", "Email2");
            DropColumn("Contacts.Contact", "Email3");
            DropColumn("Contacts.Contact", "Website2");
            DropColumn("Contacts.Contact", "Website3");
            DropColumn("Geography.Address", "DistrictCustom");
            DropColumn("Geography.Address", "DistrictID");
            DropColumn("Purchasing.PurchaseOrder", "ParentID");
            DropColumn("Ordering.SalesOrder", "ParentID");
            DropColumn("Ordering.SalesOrder", "OrderStateName");
            DropColumn("Invoicing.SalesInvoice", "ParentID");
            DropColumn("Manufacturers.Manufacturer", "SeoUrl");
            DropColumn("Manufacturers.Manufacturer", "SeoKeywords");
            DropColumn("Manufacturers.Manufacturer", "SeoPageTitle");
            DropColumn("Manufacturers.Manufacturer", "SeoDescription");
            DropColumn("Manufacturers.Manufacturer", "SeoMetaData");
            DropColumn("Products.Product", "IsSale");
            DropColumn("Products.Product", "IsQuotable");
            DropColumn("Shopping.CartItem", "StatusID");
            DropColumn("Contacts.User", "TaxNumber");
            DropColumn("Vendors.Vendor", "SeoUrl");
            DropColumn("Vendors.Vendor", "SeoKeywords");
            DropColumn("Vendors.Vendor", "SeoPageTitle");
            DropColumn("Vendors.Vendor", "SeoDescription");
            DropColumn("Vendors.Vendor", "SeoMetaData");
            DropColumn("Vendors.Vendor", "ContactMethodID");
            DropColumn("Shopping.Cart", "ParentID");
            DropColumn("Quoting.SalesQuote", "ParentID");
            DropColumn("Returning.SalesReturn", "ParentID");
            DropColumn("Returning.SalesReturn", "OrderStateName");
            DropColumn("Returning.SalesReturnItem", "StatusID");
            DropColumn("Quoting.SalesQuoteItem", "StatusID");
            DropColumn("Sampling.SampleRequest", "ParentID");
            DropColumn("Sampling.SampleRequestItem", "StatusID");
            DropColumn("Purchasing.PurchaseOrderItem", "StatusID");
            DropColumn("Invoicing.SalesInvoiceItem", "StatusID");
            DropColumn("Ordering.SalesOrderItem", "StatusID");
            DropColumn("Products.ProductType", "StoreID");
            DropColumn("Products.ProductType", "BrandID");
            DropColumn("Products.ProductRestriction", "RestrictionsApplyToDistrictID");
            DropColumn("Categories.CategoryType", "ParentID");
            DropColumn("Tax.HistoricalTaxRate", "DistrictLevelRate");
            DropColumn("Geography.PhonePrefixLookup", "DistrictID");
            DropTable("Geography.District");
            DropTable("Geography.DistrictCurrency");
            DropTable("Geography.DistrictImage");
            DropTable("Geography.DistrictImageType");
            DropTable("Geography.DistrictLanguage");
            DropTable("Tax.TaxDistrict");
            DropTable("Geography.InterRegion");
            DropTable("Stores.StoreCategory");
            DropTable("Contacts.ContactMethod");
            DropTable("Returning.SalesReturnItemStatus");
            DropTable("Shipping.CarrierInvoice");
            DropTable("Shipping.CarrierOrigin");
            DropTable("Quoting.SalesQuoteItemStatus");
            DropTable("Sampling.SampleRequestItemStatus");
            DropTable("Purchasing.PurchaseOrderItemStatus");
            DropTable("Invoicing.SalesInvoiceItemStatus");
            DropTable("Ordering.SalesOrderItemStatus");
            DropTable("Brands.BrandUserType");
            DropTable("Stores.StoreUserType");
            DropTable("Shopping.CartItemStatus");
            DropTable("Brands.BrandCategoryType");
            DropTable("Stores.StoreCategoryType");
            DropTable("Stores.StoreSiteDomain");
        }

        public override void Down()
        {
            CreateTable(
                "Stores.StoreSiteDomain",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Stores.StoreCategoryType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Brands.BrandCategoryType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Shopping.CartItemStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Stores.StoreUserType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Brands.BrandUserType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Ordering.SalesOrderItemStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Invoicing.SalesInvoiceItemStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Purchasing.PurchaseOrderItemStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Sampling.SampleRequestItemStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Quoting.SalesQuoteItemStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Shipping.CarrierOrigin",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        PackageReferenceNumber = c.String(maxLength: 100, unicode: false),
                        ShipmentReferenceNumber = c.String(maxLength: 100, unicode: false),
                        ReferenceNumber = c.String(maxLength: 100, unicode: false),
                        ShipperNumber = c.String(maxLength: 100, unicode: false),
                        SubscriptionEventName = c.String(maxLength: 100, unicode: false),
                        SubscriptionEventNumber = c.String(maxLength: 100, unicode: false),
                        SubscriptionFileName = c.String(maxLength: 100, unicode: false),
                        TrackingNumber = c.String(maxLength: 100, unicode: false),
                        TrackingStatus = c.String(maxLength: 100, unicode: false),
                        TrackingShippingMethod = c.String(maxLength: 100, unicode: false),
                        TrackingShippingDate = c.String(maxLength: 50, unicode: false),
                        TrackingLastScan = c.String(maxLength: 100, unicode: false),
                        TrackingDestination = c.String(maxLength: 100, unicode: false),
                        TrackingEstDeliveryDate = c.String(maxLength: 50, unicode: false),
                        TrackingLastUpdate = c.DateTime(precision: 7, storeType: "datetime2"),
                        TrackingEventName = c.String(maxLength: 50, unicode: false),
                        TrackingOriginalEstimatedDeliveryDate = c.String(maxLength: 50, unicode: false),
                        TrackingManualDelivered = c.Boolean(nullable: false),
                        ShipCarrierID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Shipping.CarrierInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(maxLength: 50, unicode: false),
                        InvoiceNumber = c.String(maxLength: 50, unicode: false),
                        InvoiceDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AmountDue = c.Decimal(precision: 18, scale: 2),
                        TrackingNumber = c.String(maxLength: 50, unicode: false),
                        PickupRecord = c.String(maxLength: 50, unicode: false),
                        ReferenceNo1 = c.String(maxLength: 50, unicode: false),
                        ReferenceNo2 = c.String(maxLength: 50, unicode: false),
                        ReferenceNo3 = c.String(maxLength: 50, unicode: false),
                        Weight = c.String(maxLength: 50, unicode: false),
                        Zone = c.String(maxLength: 50, unicode: false),
                        ServiceLevel = c.String(maxLength: 50, unicode: false),
                        PickupDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SenderName = c.String(maxLength: 50, unicode: false),
                        SenderCompanyName = c.String(maxLength: 50, unicode: false),
                        SenderStreet = c.String(maxLength: 50, unicode: false),
                        SenderCity = c.String(maxLength: 50, unicode: false),
                        SenderState = c.String(maxLength: 50, unicode: false),
                        SenderZipCode = c.String(maxLength: 50, unicode: false),
                        ReceiverName = c.String(maxLength: 50, unicode: false),
                        ReceiverCompanyName = c.String(maxLength: 50, unicode: false),
                        ReceiverStreet = c.String(maxLength: 50, unicode: false),
                        ReceiverCity = c.String(maxLength: 50, unicode: false),
                        ReceiverState = c.String(maxLength: 50, unicode: false),
                        ReceiverZipCode = c.String(maxLength: 50, unicode: false),
                        ReceiverCountry = c.String(maxLength: 50, unicode: false),
                        ThirdParty = c.String(maxLength: 50, unicode: false),
                        BilledCharge = c.Decimal(precision: 18, scale: 2),
                        IncentiveCredit = c.Decimal(precision: 18, scale: 2),
                        InvoiceSection = c.String(maxLength: 50, unicode: false),
                        InvoiceType = c.String(maxLength: 50, unicode: false),
                        InvoiceDueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Status = c.String(maxLength: 200, unicode: false),
                        ShipCarrierID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Returning.SalesReturnItemStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Contacts.ContactMethod",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Stores.StoreCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Geography.InterRegion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Notes = c.String(maxLength: 100),
                        KeyRegionID = c.Int(nullable: false),
                        RelationRegionID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Tax.TaxDistrict",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 7, scale: 6, storeType: "numeric"),
                        DistrictID = c.Int(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Geography.DistrictLanguage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Geography.DistrictImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Geography.DistrictImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 8),
                        Longitude = c.Decimal(precision: 18, scale: 8),
                        IsPrimary = c.Boolean(nullable: false),
                        OriginalWidth = c.Int(),
                        OriginalHeight = c.Int(),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Boolean(nullable: false),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Int(),
                        ThumbnailHeight = c.Int(),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Boolean(nullable: false),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Int(),
                        TypeID = c.Int(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Geography.DistrictCurrency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Geography.District",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        RegionID = c.Int(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            AddColumn("Geography.PhonePrefixLookup", "DistrictID", c => c.Int());
            AddColumn("Tax.HistoricalTaxRate", "DistrictLevelRate", c => c.Decimal(precision: 7, scale: 6));
            AddColumn("Categories.CategoryType", "ParentID", c => c.Int());
            AddColumn("Products.ProductRestriction", "RestrictionsApplyToDistrictID", c => c.Int());
            AddColumn("Products.ProductType", "BrandID", c => c.Int());
            AddColumn("Products.ProductType", "StoreID", c => c.Int());
            AddColumn("Ordering.SalesOrderItem", "StatusID", c => c.Int(nullable: false));
            AddColumn("Invoicing.SalesInvoiceItem", "StatusID", c => c.Int(nullable: false));
            AddColumn("Purchasing.PurchaseOrderItem", "StatusID", c => c.Int(nullable: false));
            AddColumn("Sampling.SampleRequestItem", "StatusID", c => c.Int(nullable: false));
            AddColumn("Sampling.SampleRequest", "ParentID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItem", "StatusID", c => c.Int(nullable: false));
            AddColumn("Returning.SalesReturnItem", "StatusID", c => c.Int(nullable: false));
            AddColumn("Returning.SalesReturn", "OrderStateName", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Returning.SalesReturn", "ParentID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "ParentID", c => c.Int());
            AddColumn("Shopping.Cart", "ParentID", c => c.Int());
            AddColumn("Vendors.Vendor", "ContactMethodID", c => c.Int());
            AddColumn("Vendors.Vendor", "SeoMetaData", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Vendors.Vendor", "SeoDescription", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Vendors.Vendor", "SeoPageTitle", c => c.String(maxLength: 75, unicode: false));
            AddColumn("Vendors.Vendor", "SeoKeywords", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Vendors.Vendor", "SeoUrl", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Contacts.User", "TaxNumber", c => c.String(maxLength: 50, unicode: false));
            AddColumn("Shopping.CartItem", "StatusID", c => c.Int(nullable: false));
            AddColumn("Products.Product", "IsQuotable", c => c.Boolean(nullable: false));
            AddColumn("Products.Product", "IsSale", c => c.Boolean(nullable: false));
            AddColumn("Manufacturers.Manufacturer", "SeoMetaData", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "SeoDescription", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "SeoPageTitle", c => c.String(maxLength: 75, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "SeoKeywords", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "SeoUrl", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Invoicing.SalesInvoice", "ParentID", c => c.Int());
            AddColumn("Ordering.SalesOrder", "OrderStateName", c => c.String());
            AddColumn("Ordering.SalesOrder", "ParentID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrder", "ParentID", c => c.Int());
            AddColumn("Geography.Address", "DistrictID", c => c.Int());
            AddColumn("Geography.Address", "DistrictCustom", c => c.String(maxLength: 100));
            AddColumn("Contacts.Contact", "Website3", c => c.String(maxLength: 1000, unicode: false));
            AddColumn("Contacts.Contact", "Website2", c => c.String(maxLength: 1000, unicode: false));
            AddColumn("Contacts.Contact", "Email3", c => c.String(maxLength: 1000, unicode: false));
            AddColumn("Contacts.Contact", "Email2", c => c.String(maxLength: 1000, unicode: false));
            AddColumn("Accounts.Account", "Email", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Accounts.Account", "Fax", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Accounts.Account", "Phone", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Accounts.Account", "SeoMetaData", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Accounts.Account", "SeoDescription", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Accounts.Account", "SeoPageTitle", c => c.String(maxLength: 75, unicode: false));
            AddColumn("Accounts.Account", "SeoKeywords", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Accounts.Account", "SeoUrl", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Accounts.Account", "ParentID", c => c.Int());
            DropColumn("Currencies.Currency", "RawCharacter");
            DropColumn("Currencies.Currency", "HtmlCharacterCode");
            CreateIndex("Geography.PhonePrefixLookup", "DistrictID");
            CreateIndex("Stores.StoreSiteDomain", "Hash");
            CreateIndex("Stores.StoreSiteDomain", "Active");
            CreateIndex("Stores.StoreSiteDomain", "UpdatedDate");
            CreateIndex("Stores.StoreSiteDomain", "CreatedDate");
            CreateIndex("Stores.StoreSiteDomain", "CustomKey");
            CreateIndex("Stores.StoreSiteDomain", "SlaveID");
            CreateIndex("Stores.StoreSiteDomain", "MasterID");
            CreateIndex("Stores.StoreSiteDomain", "ID");
            CreateIndex("Stores.StoreCategoryType", "Hash");
            CreateIndex("Stores.StoreCategoryType", "Active");
            CreateIndex("Stores.StoreCategoryType", "UpdatedDate");
            CreateIndex("Stores.StoreCategoryType", "CreatedDate");
            CreateIndex("Stores.StoreCategoryType", "CustomKey");
            CreateIndex("Stores.StoreCategoryType", "SlaveID");
            CreateIndex("Stores.StoreCategoryType", "MasterID");
            CreateIndex("Stores.StoreCategoryType", "ID");
            CreateIndex("Brands.BrandCategoryType", "Hash");
            CreateIndex("Brands.BrandCategoryType", "Active");
            CreateIndex("Brands.BrandCategoryType", "UpdatedDate");
            CreateIndex("Brands.BrandCategoryType", "CreatedDate");
            CreateIndex("Brands.BrandCategoryType", "CustomKey");
            CreateIndex("Brands.BrandCategoryType", "SlaveID");
            CreateIndex("Brands.BrandCategoryType", "MasterID");
            CreateIndex("Brands.BrandCategoryType", "ID");
            CreateIndex("Categories.CategoryType", "ParentID");
            CreateIndex("Products.ProductRestriction", "RestrictionsApplyToDistrictID");
            CreateIndex("Shopping.CartItemStatus", "Hash");
            CreateIndex("Shopping.CartItemStatus", "Active");
            CreateIndex("Shopping.CartItemStatus", "UpdatedDate");
            CreateIndex("Shopping.CartItemStatus", "CreatedDate");
            CreateIndex("Shopping.CartItemStatus", "CustomKey");
            CreateIndex("Shopping.CartItemStatus", "Name");
            CreateIndex("Shopping.CartItemStatus", "SortOrder");
            CreateIndex("Shopping.CartItemStatus", "DisplayName");
            CreateIndex("Shopping.CartItemStatus", "ID");
            CreateIndex("Products.ProductType", "BrandID");
            CreateIndex("Products.ProductType", "StoreID");
            CreateIndex("Stores.StoreUserType", "Hash");
            CreateIndex("Stores.StoreUserType", "Active");
            CreateIndex("Stores.StoreUserType", "UpdatedDate");
            CreateIndex("Stores.StoreUserType", "CreatedDate");
            CreateIndex("Stores.StoreUserType", "CustomKey");
            CreateIndex("Stores.StoreUserType", "SlaveID");
            CreateIndex("Stores.StoreUserType", "MasterID");
            CreateIndex("Stores.StoreUserType", "ID");
            CreateIndex("Brands.BrandUserType", "Hash");
            CreateIndex("Brands.BrandUserType", "Active");
            CreateIndex("Brands.BrandUserType", "UpdatedDate");
            CreateIndex("Brands.BrandUserType", "CreatedDate");
            CreateIndex("Brands.BrandUserType", "CustomKey");
            CreateIndex("Brands.BrandUserType", "SlaveID");
            CreateIndex("Brands.BrandUserType", "MasterID");
            CreateIndex("Brands.BrandUserType", "ID");
            CreateIndex("Ordering.SalesOrderItemStatus", "Hash");
            CreateIndex("Ordering.SalesOrderItemStatus", "Active");
            CreateIndex("Ordering.SalesOrderItemStatus", "UpdatedDate");
            CreateIndex("Ordering.SalesOrderItemStatus", "CreatedDate");
            CreateIndex("Ordering.SalesOrderItemStatus", "CustomKey");
            CreateIndex("Ordering.SalesOrderItemStatus", "Name");
            CreateIndex("Ordering.SalesOrderItemStatus", "SortOrder");
            CreateIndex("Ordering.SalesOrderItemStatus", "DisplayName");
            CreateIndex("Ordering.SalesOrderItemStatus", "ID");
            CreateIndex("Ordering.SalesOrderItem", "StatusID");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "Hash");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "Active");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "UpdatedDate");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "CreatedDate");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "CustomKey");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "Name");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "SortOrder");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "DisplayName");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "ID");
            CreateIndex("Invoicing.SalesInvoiceItem", "StatusID");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "Hash");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "Active");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "UpdatedDate");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "CreatedDate");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "CustomKey");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "Name");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "SortOrder");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "DisplayName");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "ID");
            CreateIndex("Purchasing.PurchaseOrderItem", "StatusID");
            CreateIndex("Sampling.SampleRequestItemStatus", "Hash");
            CreateIndex("Sampling.SampleRequestItemStatus", "Active");
            CreateIndex("Sampling.SampleRequestItemStatus", "UpdatedDate");
            CreateIndex("Sampling.SampleRequestItemStatus", "CreatedDate");
            CreateIndex("Sampling.SampleRequestItemStatus", "CustomKey");
            CreateIndex("Sampling.SampleRequestItemStatus", "Name");
            CreateIndex("Sampling.SampleRequestItemStatus", "SortOrder");
            CreateIndex("Sampling.SampleRequestItemStatus", "DisplayName");
            CreateIndex("Sampling.SampleRequestItemStatus", "ID");
            CreateIndex("Sampling.SampleRequestItem", "StatusID");
            CreateIndex("Sampling.SampleRequest", "ParentID");
            CreateIndex("Quoting.SalesQuoteItemStatus", "Hash");
            CreateIndex("Quoting.SalesQuoteItemStatus", "Active");
            CreateIndex("Quoting.SalesQuoteItemStatus", "UpdatedDate");
            CreateIndex("Quoting.SalesQuoteItemStatus", "CreatedDate");
            CreateIndex("Quoting.SalesQuoteItemStatus", "CustomKey");
            CreateIndex("Quoting.SalesQuoteItemStatus", "Name");
            CreateIndex("Quoting.SalesQuoteItemStatus", "SortOrder");
            CreateIndex("Quoting.SalesQuoteItemStatus", "DisplayName");
            CreateIndex("Quoting.SalesQuoteItemStatus", "ID");
            CreateIndex("Quoting.SalesQuoteItem", "StatusID");
            CreateIndex("Shipping.CarrierOrigin", "Hash");
            CreateIndex("Shipping.CarrierOrigin", "Active");
            CreateIndex("Shipping.CarrierOrigin", "UpdatedDate");
            CreateIndex("Shipping.CarrierOrigin", "CreatedDate");
            CreateIndex("Shipping.CarrierOrigin", "CustomKey");
            CreateIndex("Shipping.CarrierOrigin", "ShipCarrierID");
            CreateIndex("Shipping.CarrierOrigin", "ID");
            CreateIndex("Shipping.CarrierInvoice", "Hash");
            CreateIndex("Shipping.CarrierInvoice", "Active");
            CreateIndex("Shipping.CarrierInvoice", "UpdatedDate");
            CreateIndex("Shipping.CarrierInvoice", "CreatedDate");
            CreateIndex("Shipping.CarrierInvoice", "CustomKey");
            CreateIndex("Shipping.CarrierInvoice", "ShipCarrierID");
            CreateIndex("Shipping.CarrierInvoice", "ID");
            CreateIndex("Returning.SalesReturnItemStatus", "Hash");
            CreateIndex("Returning.SalesReturnItemStatus", "Active");
            CreateIndex("Returning.SalesReturnItemStatus", "UpdatedDate");
            CreateIndex("Returning.SalesReturnItemStatus", "CreatedDate");
            CreateIndex("Returning.SalesReturnItemStatus", "CustomKey");
            CreateIndex("Returning.SalesReturnItemStatus", "Name");
            CreateIndex("Returning.SalesReturnItemStatus", "SortOrder");
            CreateIndex("Returning.SalesReturnItemStatus", "DisplayName");
            CreateIndex("Returning.SalesReturnItemStatus", "ID");
            CreateIndex("Returning.SalesReturnItem", "StatusID");
            CreateIndex("Returning.SalesReturn", "ParentID");
            CreateIndex("Quoting.SalesQuote", "ParentID");
            CreateIndex("Shopping.Cart", "ParentID");
            CreateIndex("Contacts.ContactMethod", "Hash");
            CreateIndex("Contacts.ContactMethod", "Active");
            CreateIndex("Contacts.ContactMethod", "UpdatedDate");
            CreateIndex("Contacts.ContactMethod", "CreatedDate");
            CreateIndex("Contacts.ContactMethod", "CustomKey");
            CreateIndex("Contacts.ContactMethod", "Name");
            CreateIndex("Contacts.ContactMethod", "SortOrder");
            CreateIndex("Contacts.ContactMethod", "DisplayName");
            CreateIndex("Contacts.ContactMethod", "ID");
            CreateIndex("Vendors.Vendor", "ContactMethodID");
            CreateIndex("Shopping.CartItem", "StatusID");
            CreateIndex("Stores.StoreCategory", "Hash");
            CreateIndex("Stores.StoreCategory", "Active");
            CreateIndex("Stores.StoreCategory", "UpdatedDate");
            CreateIndex("Stores.StoreCategory", "CreatedDate");
            CreateIndex("Stores.StoreCategory", "CustomKey");
            CreateIndex("Stores.StoreCategory", "SlaveID");
            CreateIndex("Stores.StoreCategory", "MasterID");
            CreateIndex("Stores.StoreCategory", "ID");
            CreateIndex("Invoicing.SalesInvoice", "ParentID");
            CreateIndex("Ordering.SalesOrder", "ParentID");
            CreateIndex("Purchasing.PurchaseOrder", "ParentID");
            CreateIndex("Geography.InterRegion", "Hash");
            CreateIndex("Geography.InterRegion", "Active");
            CreateIndex("Geography.InterRegion", "UpdatedDate");
            CreateIndex("Geography.InterRegion", "CreatedDate");
            CreateIndex("Geography.InterRegion", "CustomKey");
            CreateIndex("Geography.InterRegion", "RelationRegionID");
            CreateIndex("Geography.InterRegion", "KeyRegionID");
            CreateIndex("Geography.InterRegion", "ID");
            CreateIndex("Tax.TaxDistrict", "Hash");
            CreateIndex("Tax.TaxDistrict", "Active");
            CreateIndex("Tax.TaxDistrict", "UpdatedDate");
            CreateIndex("Tax.TaxDistrict", "CreatedDate");
            CreateIndex("Tax.TaxDistrict", "CustomKey");
            CreateIndex("Tax.TaxDistrict", "Name");
            CreateIndex("Tax.TaxDistrict", "DistrictID");
            CreateIndex("Tax.TaxDistrict", "ID");
            CreateIndex("Geography.DistrictLanguage", "Hash");
            CreateIndex("Geography.DistrictLanguage", "Active");
            CreateIndex("Geography.DistrictLanguage", "UpdatedDate");
            CreateIndex("Geography.DistrictLanguage", "CreatedDate");
            CreateIndex("Geography.DistrictLanguage", "CustomKey");
            CreateIndex("Geography.DistrictLanguage", "SlaveID");
            CreateIndex("Geography.DistrictLanguage", "MasterID");
            CreateIndex("Geography.DistrictLanguage", "ID");
            CreateIndex("Geography.DistrictImageType", "Hash");
            CreateIndex("Geography.DistrictImageType", "Active");
            CreateIndex("Geography.DistrictImageType", "UpdatedDate");
            CreateIndex("Geography.DistrictImageType", "CreatedDate");
            CreateIndex("Geography.DistrictImageType", "CustomKey");
            CreateIndex("Geography.DistrictImageType", "Name");
            CreateIndex("Geography.DistrictImageType", "SortOrder");
            CreateIndex("Geography.DistrictImageType", "DisplayName");
            CreateIndex("Geography.DistrictImageType", "ID");
            CreateIndex("Geography.DistrictImage", "Hash");
            CreateIndex("Geography.DistrictImage", "Active");
            CreateIndex("Geography.DistrictImage", "UpdatedDate");
            CreateIndex("Geography.DistrictImage", "CreatedDate");
            CreateIndex("Geography.DistrictImage", "CustomKey");
            CreateIndex("Geography.DistrictImage", "Name");
            CreateIndex("Geography.DistrictImage", "TypeID");
            CreateIndex("Geography.DistrictImage", "MasterID");
            CreateIndex("Geography.DistrictImage", "ID");
            CreateIndex("Geography.DistrictCurrency", "Hash");
            CreateIndex("Geography.DistrictCurrency", "Active");
            CreateIndex("Geography.DistrictCurrency", "UpdatedDate");
            CreateIndex("Geography.DistrictCurrency", "CreatedDate");
            CreateIndex("Geography.DistrictCurrency", "CustomKey");
            CreateIndex("Geography.DistrictCurrency", "SlaveID");
            CreateIndex("Geography.DistrictCurrency", "MasterID");
            CreateIndex("Geography.DistrictCurrency", "ID");
            CreateIndex("Geography.District", "Hash");
            CreateIndex("Geography.District", "Active");
            CreateIndex("Geography.District", "UpdatedDate");
            CreateIndex("Geography.District", "CreatedDate");
            CreateIndex("Geography.District", "CustomKey");
            CreateIndex("Geography.District", "Name");
            CreateIndex("Geography.District", "RegionID");
            CreateIndex("Geography.District", "ID");
            CreateIndex("Geography.Address", "DistrictID");
            CreateIndex("Accounts.Account", "Email");
            CreateIndex("Accounts.Account", "Fax");
            CreateIndex("Accounts.Account", "Phone");
            CreateIndex("Accounts.Account", "ParentID");
            AddForeignKey("Geography.PhonePrefixLookup", "DistrictID", "Geography.District", "ID");
            AddForeignKey("Accounts.Account", "ParentID", "Accounts.Account", "ID");
            AddForeignKey("Purchasing.PurchaseOrder", "ParentID", "Purchasing.PurchaseOrder", "ID");
            AddForeignKey("Ordering.SalesOrder", "ParentID", "Ordering.SalesOrder", "ID");
            AddForeignKey("Invoicing.SalesInvoice", "ParentID", "Invoicing.SalesInvoice", "ID");
            AddForeignKey("Stores.StoreSiteDomain", "SlaveID", "Stores.SiteDomain", "ID", cascadeDelete: true);
            AddForeignKey("Stores.StoreSiteDomain", "MasterID", "Stores.Store", "ID", cascadeDelete: true);
            AddForeignKey("Stores.StoreCategory", "SlaveID", "Categories.Category", "ID", cascadeDelete: true);
            AddForeignKey("Stores.StoreCategoryType", "SlaveID", "Categories.CategoryType", "ID", cascadeDelete: true);
            AddForeignKey("Stores.StoreCategoryType", "MasterID", "Stores.Store", "ID", cascadeDelete: true);
            AddForeignKey("Categories.CategoryType", "ParentID", "Categories.CategoryType", "ID");
            AddForeignKey("Brands.BrandCategoryType", "SlaveID", "Categories.CategoryType", "ID", cascadeDelete: true);
            AddForeignKey("Brands.BrandCategoryType", "MasterID", "Brands.Brand", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductRestriction", "RestrictionsApplyToDistrictID", "Geography.District", "ID");
            AddForeignKey("Shopping.CartItem", "StatusID", "Shopping.CartItemStatus", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductType", "StoreID", "Stores.Store", "ID");
            AddForeignKey("Products.ProductType", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Stores.StoreUserType", "SlaveID", "Contacts.UserType", "ID", cascadeDelete: true);
            AddForeignKey("Stores.StoreUserType", "MasterID", "Stores.Store", "ID", cascadeDelete: true);
            AddForeignKey("Brands.BrandUserType", "SlaveID", "Contacts.UserType", "ID", cascadeDelete: true);
            AddForeignKey("Brands.BrandUserType", "MasterID", "Brands.Brand", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderItem", "StatusID", "Ordering.SalesOrderItemStatus", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoiceItem", "StatusID", "Invoicing.SalesInvoiceItemStatus", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderItem", "StatusID", "Purchasing.PurchaseOrderItemStatus", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequestItem", "StatusID", "Sampling.SampleRequestItemStatus", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequest", "ParentID", "Sampling.SampleRequest", "ID");
            AddForeignKey("Quoting.SalesQuoteItem", "StatusID", "Quoting.SalesQuoteItemStatus", "ID", cascadeDelete: true);
            AddForeignKey("Shipping.CarrierOrigin", "ShipCarrierID", "Shipping.ShipCarrier", "ID");
            AddForeignKey("Shipping.CarrierInvoice", "ShipCarrierID", "Shipping.ShipCarrier", "ID");
            AddForeignKey("Returning.SalesReturnItem", "StatusID", "Returning.SalesReturnItemStatus", "ID", cascadeDelete: true);
            AddForeignKey("Returning.SalesReturn", "ParentID", "Returning.SalesReturn", "ID");
            AddForeignKey("Quoting.SalesQuote", "ParentID", "Quoting.SalesQuote", "ID");
            AddForeignKey("Shopping.Cart", "ParentID", "Shopping.Cart", "ID");
            AddForeignKey("Vendors.Vendor", "ContactMethodID", "Contacts.ContactMethod", "ID");
            AddForeignKey("Stores.StoreCategory", "MasterID", "Stores.Store", "ID", cascadeDelete: true);
            AddForeignKey("Geography.Address", "DistrictID", "Geography.District", "ID");
            AddForeignKey("Geography.InterRegion", "RelationRegionID", "Geography.Region", "ID");
            AddForeignKey("Geography.InterRegion", "KeyRegionID", "Geography.Region", "ID", cascadeDelete: true);
            AddForeignKey("Tax.TaxDistrict", "DistrictID", "Geography.District", "ID", cascadeDelete: true);
            AddForeignKey("Geography.District", "RegionID", "Geography.Region", "ID", cascadeDelete: true);
            AddForeignKey("Geography.DistrictLanguage", "SlaveID", "Globalization.Language", "ID", cascadeDelete: true);
            AddForeignKey("Geography.DistrictLanguage", "MasterID", "Geography.District", "ID", cascadeDelete: true);
            AddForeignKey("Geography.DistrictImage", "TypeID", "Geography.DistrictImageType", "ID", cascadeDelete: true);
            AddForeignKey("Geography.DistrictImage", "MasterID", "Geography.District", "ID");
            AddForeignKey("Geography.DistrictCurrency", "SlaveID", "Currencies.Currency", "ID", cascadeDelete: true);
            AddForeignKey("Geography.DistrictCurrency", "MasterID", "Geography.District", "ID", cascadeDelete: true);
        }
    }
}
