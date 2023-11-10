// <copyright file="201910230352558_OrderMinimums.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201910230352558 order minimums class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OrderMinimums : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Vendors.VendorType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);
            Sql("INSERT INTO Vendors.VendorType (CreatedDate,Active,CustomKey,[Name],DisplayName,SortOrder,JsonAttributes) VALUES (CURRENT_TIMESTAMP,1,'General','General','General',0,'{}')");

            CreateTable(
                "Manufacturers.ManufacturerType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);
            Sql("INSERT INTO Manufacturers.ManufacturerType (CreatedDate,Active,CustomKey,[Name],DisplayName,SortOrder,JsonAttributes) VALUES (CURRENT_TIMESTAMP,1,'General','General','General',0,'{}')");

            AddColumn("Stores.Store", "MinimumOrderDollarAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Stores.Store", "MinimumOrderDollarAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Stores.Store", "MinimumOrderDollarAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Stores.Store", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Stores.Store", "MinimumOrderQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Stores.Store", "MinimumOrderQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Stores.Store", "MinimumOrderQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Stores.Store", "MinimumOrderQuantityAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Stores.Store", "MinimumOrderQuantityAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Stores.Store", "MinimumOrderQuantityAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Stores.Store", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Stores.Store", "MinimumForFreeShippingDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Stores.Store", "MinimumForFreeShippingDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Stores.Store", "MinimumForFreeShippingDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Stores.Store", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Stores.Store", "MinimumForFreeShippingQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Categories.Category", "MinimumOrderDollarAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumOrderDollarAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Categories.Category", "MinimumOrderDollarAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Categories.Category", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Categories.Category", "MinimumOrderQuantityAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumOrderQuantityAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Categories.Category", "MinimumOrderQuantityAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Categories.Category", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Categories.Category", "MinimumForFreeShippingDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumForFreeShippingDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumForFreeShippingDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Categories.Category", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Categories.Category", "MinimumForFreeShippingQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Products.Product", "DocumentRequiredForPurchase", c => c.String(maxLength: 128));
            AddColumn("Products.Product", "DocumentRequiredForPurchaseMissingWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Products.Product", "DocumentRequiredForPurchaseExpiredWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Products.Product", "DocumentRequiredForPurchaseOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "DocumentRequiredForPurchaseOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Products.Product", "DocumentRequiredForPurchaseOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Products.Product", "DocumentRequiredForPurchaseOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Products.Product", "MustPurchaseInMultiplesOfAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "MustPurchaseInMultiplesOfAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Products.Product", "MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Products.Product", "MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("System.Note", "StoreID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "SeoUrl", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "SeoKeywords", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "SeoPageTitle", c => c.String(maxLength: 75, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "SeoDescription", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "SeoMetaData", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "TypeID", c => c.Int(nullable: false));
            Sql("UPDATE Manufacturers.Manufacturer SET TypeID = 1");
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Vendors.Vendor", "SeoUrl", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Vendors.Vendor", "SeoKeywords", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Vendors.Vendor", "SeoPageTitle", c => c.String(maxLength: 75, unicode: false));
            AddColumn("Vendors.Vendor", "SeoDescription", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Vendors.Vendor", "SeoMetaData", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Vendors.Vendor", "TypeID", c => c.Int(nullable: false));
            Sql("UPDATE Vendors.Vendor SET TypeID = 1");
            AddColumn("Vendors.Vendor", "MinimumOrderDollarAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Vendors.Vendor", "MinimumOrderDollarAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Vendors.Vendor", "MinimumOrderDollarAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Vendors.Vendor", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Vendors.Vendor", "MinimumOrderQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Vendors.Vendor", "MinimumOrderQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Vendors.Vendor", "MinimumOrderQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Vendors.Vendor", "MinimumOrderQuantityAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Vendors.Vendor", "MinimumOrderQuantityAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Vendors.Vendor", "MinimumOrderQuantityAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Vendors.Vendor", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AlterColumn("Stores.Store", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            AlterColumn("Categories.Category", "RequiresRoles", c => c.String(maxLength: 512));
            AlterColumn("Categories.Category", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            AlterColumn("Categories.Category", "MinimumOrderQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AlterColumn("Products.Product", "RequiresRoles", c => c.String(maxLength: 512));
            AlterColumn("Vendors.Vendor", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            CreateIndex("Manufacturers.Manufacturer", "TypeID");
            CreateIndex("System.Note", "StoreID");
            CreateIndex("Vendors.Vendor", "TypeID");
            AddForeignKey("Vendors.Vendor", "TypeID", "Vendors.VendorType", "ID", cascadeDelete: true);
            AddForeignKey("System.Note", "StoreID", "Stores.Store", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "TypeID", "Manufacturers.ManufacturerType", "ID", cascadeDelete: true);
            DropColumn("Pricing.PriceRule", "CustomCurrency");
        }

        public override void Down()
        {
            AddColumn("Pricing.PriceRule", "CustomCurrency", c => c.String(unicode: false));
            DropForeignKey("Manufacturers.Manufacturer", "TypeID", "Manufacturers.ManufacturerType");
            DropForeignKey("System.Note", "StoreID", "Stores.Store");
            DropForeignKey("Vendors.Vendor", "TypeID", "Vendors.VendorType");
            DropIndex("Manufacturers.ManufacturerType", new[] { "SortOrder" });
            DropIndex("Manufacturers.ManufacturerType", new[] { "DisplayName" });
            DropIndex("Manufacturers.ManufacturerType", new[] { "Name" });
            DropIndex("Manufacturers.ManufacturerType", new[] { "Hash" });
            DropIndex("Manufacturers.ManufacturerType", new[] { "Active" });
            DropIndex("Manufacturers.ManufacturerType", new[] { "UpdatedDate" });
            DropIndex("Manufacturers.ManufacturerType", new[] { "CreatedDate" });
            DropIndex("Manufacturers.ManufacturerType", new[] { "CustomKey" });
            DropIndex("Manufacturers.ManufacturerType", new[] { "ID" });
            DropIndex("Vendors.VendorType", new[] { "SortOrder" });
            DropIndex("Vendors.VendorType", new[] { "DisplayName" });
            DropIndex("Vendors.VendorType", new[] { "Name" });
            DropIndex("Vendors.VendorType", new[] { "Hash" });
            DropIndex("Vendors.VendorType", new[] { "Active" });
            DropIndex("Vendors.VendorType", new[] { "UpdatedDate" });
            DropIndex("Vendors.VendorType", new[] { "CreatedDate" });
            DropIndex("Vendors.VendorType", new[] { "CustomKey" });
            DropIndex("Vendors.VendorType", new[] { "ID" });
            DropIndex("Vendors.Vendor", new[] { "TypeID" });
            DropIndex("System.Note", new[] { "StoreID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "TypeID" });
            AlterColumn("Vendors.Vendor", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 512));
            AlterColumn("Products.Product", "RequiresRoles", c => c.String());
            AlterColumn("Categories.Category", "MinimumOrderQuantityAmountWarningMessage", c => c.String(maxLength: 512));
            AlterColumn("Categories.Category", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 512));
            AlterColumn("Categories.Category", "RequiresRoles", c => c.String());
            AlterColumn("Stores.Store", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 512));
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountWarningMessage");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountAfter");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmount");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountWarningMessage");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountAfter");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmount");
            DropColumn("Vendors.Vendor", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage");
            DropColumn("Vendors.Vendor", "MinimumOrderQuantityAmountOverrideFeeWarningMessage");
            DropColumn("Vendors.Vendor", "MinimumOrderQuantityAmountOverrideFeeIsPercent");
            DropColumn("Vendors.Vendor", "MinimumOrderQuantityAmountOverrideFee");
            DropColumn("Vendors.Vendor", "MinimumOrderQuantityAmountWarningMessage");
            DropColumn("Vendors.Vendor", "MinimumOrderQuantityAmountAfter");
            DropColumn("Vendors.Vendor", "MinimumOrderQuantityAmount");
            DropColumn("Vendors.Vendor", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage");
            DropColumn("Vendors.Vendor", "MinimumOrderDollarAmountOverrideFeeWarningMessage");
            DropColumn("Vendors.Vendor", "MinimumOrderDollarAmountOverrideFeeIsPercent");
            DropColumn("Vendors.Vendor", "MinimumOrderDollarAmountOverrideFee");
            DropColumn("Vendors.Vendor", "TypeID");
            DropColumn("Vendors.Vendor", "SeoMetaData");
            DropColumn("Vendors.Vendor", "SeoDescription");
            DropColumn("Vendors.Vendor", "SeoPageTitle");
            DropColumn("Vendors.Vendor", "SeoKeywords");
            DropColumn("Vendors.Vendor", "SeoUrl");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountWarningMessage");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountAfter");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmount");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountWarningMessage");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountAfter");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmount");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountOverrideFeeWarningMessage");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountOverrideFeeIsPercent");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountOverrideFee");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountWarningMessage");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountAfter");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmount");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountOverrideFeeWarningMessage");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountOverrideFeeIsPercent");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountOverrideFee");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountAfter");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmount");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountWarningMessage");
            DropColumn("Manufacturers.Manufacturer", "TypeID");
            DropColumn("Manufacturers.Manufacturer", "SeoMetaData");
            DropColumn("Manufacturers.Manufacturer", "SeoDescription");
            DropColumn("Manufacturers.Manufacturer", "SeoPageTitle");
            DropColumn("Manufacturers.Manufacturer", "SeoKeywords");
            DropColumn("Manufacturers.Manufacturer", "SeoUrl");
            DropColumn("System.Note", "StoreID");
            DropColumn("Products.Product", "MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage");
            DropColumn("Products.Product", "MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage");
            DropColumn("Products.Product", "MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent");
            DropColumn("Products.Product", "MustPurchaseInMultiplesOfAmountOverrideFee");
            DropColumn("Products.Product", "MustPurchaseInMultiplesOfAmount");
            DropColumn("Products.Product", "DocumentRequiredForPurchaseOverrideFeeAcceptedMessage");
            DropColumn("Products.Product", "DocumentRequiredForPurchaseOverrideFeeWarningMessage");
            DropColumn("Products.Product", "DocumentRequiredForPurchaseOverrideFeeIsPercent");
            DropColumn("Products.Product", "DocumentRequiredForPurchaseOverrideFee");
            DropColumn("Products.Product", "DocumentRequiredForPurchaseExpiredWarningMessage");
            DropColumn("Products.Product", "DocumentRequiredForPurchaseMissingWarningMessage");
            DropColumn("Products.Product", "DocumentRequiredForPurchase");
            DropColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage");
            DropColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountWarningMessage");
            DropColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountAfter");
            DropColumn("Categories.Category", "MinimumForFreeShippingQuantityAmount");
            DropColumn("Categories.Category", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage");
            DropColumn("Categories.Category", "MinimumForFreeShippingDollarAmountWarningMessage");
            DropColumn("Categories.Category", "MinimumForFreeShippingDollarAmountAfter");
            DropColumn("Categories.Category", "MinimumForFreeShippingDollarAmount");
            DropColumn("Categories.Category", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage");
            DropColumn("Categories.Category", "MinimumOrderQuantityAmountOverrideFeeWarningMessage");
            DropColumn("Categories.Category", "MinimumOrderQuantityAmountOverrideFeeIsPercent");
            DropColumn("Categories.Category", "MinimumOrderQuantityAmountOverrideFee");
            DropColumn("Categories.Category", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage");
            DropColumn("Categories.Category", "MinimumOrderDollarAmountOverrideFeeWarningMessage");
            DropColumn("Categories.Category", "MinimumOrderDollarAmountOverrideFeeIsPercent");
            DropColumn("Categories.Category", "MinimumOrderDollarAmountOverrideFee");
            DropColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage");
            DropColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountWarningMessage");
            DropColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountAfter");
            DropColumn("Stores.Store", "MinimumForFreeShippingQuantityAmount");
            DropColumn("Stores.Store", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage");
            DropColumn("Stores.Store", "MinimumForFreeShippingDollarAmountWarningMessage");
            DropColumn("Stores.Store", "MinimumForFreeShippingDollarAmountAfter");
            DropColumn("Stores.Store", "MinimumForFreeShippingDollarAmount");
            DropColumn("Stores.Store", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage");
            DropColumn("Stores.Store", "MinimumOrderQuantityAmountOverrideFeeWarningMessage");
            DropColumn("Stores.Store", "MinimumOrderQuantityAmountOverrideFeeIsPercent");
            DropColumn("Stores.Store", "MinimumOrderQuantityAmountOverrideFee");
            DropColumn("Stores.Store", "MinimumOrderQuantityAmountWarningMessage");
            DropColumn("Stores.Store", "MinimumOrderQuantityAmountAfter");
            DropColumn("Stores.Store", "MinimumOrderQuantityAmount");
            DropColumn("Stores.Store", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage");
            DropColumn("Stores.Store", "MinimumOrderDollarAmountOverrideFeeWarningMessage");
            DropColumn("Stores.Store", "MinimumOrderDollarAmountOverrideFeeIsPercent");
            DropColumn("Stores.Store", "MinimumOrderDollarAmountOverrideFee");
            DropTable("Manufacturers.ManufacturerType");
            DropTable("Vendors.VendorType");
        }
    }
}
