// <copyright file="202006060940352_ByBrand.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202006060940352 by brand class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ByBrand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Brands.BrandAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        HasAccessToBrand = c.Boolean(nullable: false),
                        PricePointID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Pricing.PricePoint", t => t.PricePointID)
                .ForeignKey("Accounts.Account", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.PricePointID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Categories.CategoryType", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Brands.BrandInventoryLocation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Inventory.InventoryLocation", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("Brands.BrandInventoryLocationType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.TypeID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Brands.BrandProduct",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleInBrand = c.Boolean(nullable: false),
                        PriceBase = c.Decimal(precision: 18, scale: 4),
                        PriceMsrp = c.Decimal(precision: 18, scale: 4),
                        PriceReduction = c.Decimal(precision: 18, scale: 4),
                        PriceSale = c.Decimal(precision: 18, scale: 4),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Brands.BrandUser",
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Brands.BrandCategory",
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Categories.Category", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Advertising.AdBrand",
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Advertising.Ad", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Brands.Brand", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Contacts.UserType", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Brands.BrandInventoryLocationType",
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
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Discounts.DiscountBrands",
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Brands.Brand", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Pricing.PriceRuleBrand",
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Pricing.PriceRule", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Brands.Brand", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Scouting.ScoutCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Scouting.Scout", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Categories.Category", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("Scouting.ScoutCategoryType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.TypeID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Scouting.Scout",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        YearMin = c.DateTime(precision: 7, storeType: "datetime2"),
                        YearMax = c.DateTime(precision: 7, storeType: "datetime2"),
                        PriceMin = c.Decimal(precision: 18, scale: 4),
                        PriceMax = c.Decimal(precision: 18, scale: 4),
                        HoursUsedMin = c.Decimal(precision: 18, scale: 4),
                        HoursUsedMax = c.Decimal(precision: 18, scale: 4),
                        DistanceUsedMin = c.Decimal(precision: 18, scale: 4),
                        DistanceUsedMax = c.Decimal(precision: 18, scale: 4),
                        DistanceUnitOfMeasure = c.String(maxLength: 128, unicode: false),
                        CreatedByUserID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.CreatedByUserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Scouting.ScoutCategoryType",
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
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("Brands.Brand", "MinimumOrderDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumOrderDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumOrderDollarAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumOrderDollarAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Brands.Brand", "MinimumOrderDollarAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumOrderQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumOrderQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumOrderQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumOrderQuantityAmountOverrideFee", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumOrderQuantityAmountOverrideFeeIsPercent", c => c.Boolean(nullable: false));
            AddColumn("Brands.Brand", "MinimumOrderQuantityAmountOverrideFeeWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumOrderDollarAmountBufferProductID", c => c.Int());
            AddColumn("Brands.Brand", "MinimumOrderQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Brands.Brand", "MinimumOrderDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Brands.Brand", "MinimumOrderQuantityAmountBufferCategoryID", c => c.Int());
            AddColumn("Brands.Brand", "MinimumForFreeShippingDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage", c => c.String(maxLength: 1024));
            AddColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferProductID", c => c.Int());
            AddColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferCategoryID", c => c.Int());
            AddColumn("System.Note", "BrandID", c => c.Int());
            CreateIndex("Brands.Brand", "MinimumOrderDollarAmountBufferProductID");
            CreateIndex("Brands.Brand", "MinimumOrderQuantityAmountBufferProductID");
            CreateIndex("Brands.Brand", "MinimumOrderDollarAmountBufferCategoryID");
            CreateIndex("Brands.Brand", "MinimumOrderQuantityAmountBufferCategoryID");
            CreateIndex("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferProductID");
            CreateIndex("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferProductID");
            CreateIndex("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            CreateIndex("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            CreateIndex("System.Note", "BrandID");
            AddForeignKey("System.Note", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Brands.Brand", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Brands.Brand", "MinimumOrderDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Brands.Brand", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Brands.Brand", "MinimumOrderQuantityAmountBufferProductID", "Products.Product", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Scouting.ScoutCategory", "TypeID", "Scouting.ScoutCategoryType");
            DropForeignKey("Scouting.ScoutCategory", "SlaveID", "Categories.Category");
            DropForeignKey("Scouting.ScoutCategory", "MasterID", "Scouting.Scout");
            DropForeignKey("Scouting.Scout", "CreatedByUserID", "Contacts.User");
            DropForeignKey("Pricing.PriceRuleBrand", "SlaveID", "Brands.Brand");
            DropForeignKey("Pricing.PriceRuleBrand", "MasterID", "Pricing.PriceRule");
            DropForeignKey("Discounts.DiscountBrands", "SlaveID", "Brands.Brand");
            DropForeignKey("Discounts.DiscountBrands", "MasterID", "Discounts.Discount");
            DropForeignKey("Brands.Brand", "MinimumOrderQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Brands.Brand", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Brands.Brand", "MinimumOrderDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Brands.Brand", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Brands.BrandInventoryLocation", "TypeID", "Brands.BrandInventoryLocationType");
            DropForeignKey("Brands.BrandInventoryLocation", "SlaveID", "Inventory.InventoryLocation");
            DropForeignKey("Brands.BrandUserType", "SlaveID", "Contacts.UserType");
            DropForeignKey("Brands.BrandUserType", "MasterID", "Brands.Brand");
            DropForeignKey("Advertising.AdBrand", "SlaveID", "Brands.Brand");
            DropForeignKey("Advertising.AdBrand", "MasterID", "Advertising.Ad");
            DropForeignKey("Brands.BrandCategory", "SlaveID", "Categories.Category");
            DropForeignKey("Brands.BrandCategory", "MasterID", "Brands.Brand");
            DropForeignKey("Brands.BrandUser", "SlaveID", "Contacts.User");
            DropForeignKey("Brands.BrandUser", "MasterID", "Brands.Brand");
            DropForeignKey("System.Note", "BrandID", "Brands.Brand");
            DropForeignKey("Brands.BrandProduct", "SlaveID", "Products.Product");
            DropForeignKey("Brands.BrandProduct", "MasterID", "Brands.Brand");
            DropForeignKey("Brands.BrandInventoryLocation", "MasterID", "Brands.Brand");
            DropForeignKey("Brands.BrandCategoryType", "SlaveID", "Categories.CategoryType");
            DropForeignKey("Brands.BrandCategoryType", "MasterID", "Brands.Brand");
            DropForeignKey("Brands.BrandAccount", "SlaveID", "Accounts.Account");
            DropForeignKey("Brands.BrandAccount", "PricePointID", "Pricing.PricePoint");
            DropForeignKey("Brands.BrandAccount", "MasterID", "Brands.Brand");
            DropIndex("Scouting.ScoutCategoryType", new[] { "Hash" });
            DropIndex("Scouting.ScoutCategoryType", new[] { "Active" });
            DropIndex("Scouting.ScoutCategoryType", new[] { "UpdatedDate" });
            DropIndex("Scouting.ScoutCategoryType", new[] { "CreatedDate" });
            DropIndex("Scouting.ScoutCategoryType", new[] { "CustomKey" });
            DropIndex("Scouting.ScoutCategoryType", new[] { "Name" });
            DropIndex("Scouting.ScoutCategoryType", new[] { "SortOrder" });
            DropIndex("Scouting.ScoutCategoryType", new[] { "DisplayName" });
            DropIndex("Scouting.ScoutCategoryType", new[] { "ID" });
            DropIndex("Scouting.Scout", new[] { "Hash" });
            DropIndex("Scouting.Scout", new[] { "Active" });
            DropIndex("Scouting.Scout", new[] { "UpdatedDate" });
            DropIndex("Scouting.Scout", new[] { "CreatedDate" });
            DropIndex("Scouting.Scout", new[] { "CustomKey" });
            DropIndex("Scouting.Scout", new[] { "CreatedByUserID" });
            DropIndex("Scouting.Scout", new[] { "ID" });
            DropIndex("Scouting.ScoutCategory", new[] { "Hash" });
            DropIndex("Scouting.ScoutCategory", new[] { "Active" });
            DropIndex("Scouting.ScoutCategory", new[] { "UpdatedDate" });
            DropIndex("Scouting.ScoutCategory", new[] { "CreatedDate" });
            DropIndex("Scouting.ScoutCategory", new[] { "CustomKey" });
            DropIndex("Scouting.ScoutCategory", new[] { "TypeID" });
            DropIndex("Scouting.ScoutCategory", new[] { "SlaveID" });
            DropIndex("Scouting.ScoutCategory", new[] { "MasterID" });
            DropIndex("Scouting.ScoutCategory", new[] { "ID" });
            DropIndex("Pricing.PriceRuleBrand", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleBrand", new[] { "Active" });
            DropIndex("Pricing.PriceRuleBrand", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleBrand", new[] { "CreatedDate" });
            DropIndex("Pricing.PriceRuleBrand", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleBrand", new[] { "SlaveID" });
            DropIndex("Pricing.PriceRuleBrand", new[] { "MasterID" });
            DropIndex("Pricing.PriceRuleBrand", new[] { "ID" });
            DropIndex("Discounts.DiscountBrands", new[] { "Hash" });
            DropIndex("Discounts.DiscountBrands", new[] { "Active" });
            DropIndex("Discounts.DiscountBrands", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountBrands", new[] { "CreatedDate" });
            DropIndex("Discounts.DiscountBrands", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountBrands", new[] { "SlaveID" });
            DropIndex("Discounts.DiscountBrands", new[] { "MasterID" });
            DropIndex("Discounts.DiscountBrands", new[] { "ID" });
            DropIndex("Brands.BrandInventoryLocationType", new[] { "Hash" });
            DropIndex("Brands.BrandInventoryLocationType", new[] { "Active" });
            DropIndex("Brands.BrandInventoryLocationType", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandInventoryLocationType", new[] { "CreatedDate" });
            DropIndex("Brands.BrandInventoryLocationType", new[] { "CustomKey" });
            DropIndex("Brands.BrandInventoryLocationType", new[] { "Name" });
            DropIndex("Brands.BrandInventoryLocationType", new[] { "SortOrder" });
            DropIndex("Brands.BrandInventoryLocationType", new[] { "DisplayName" });
            DropIndex("Brands.BrandInventoryLocationType", new[] { "ID" });
            DropIndex("Brands.BrandUserType", new[] { "Hash" });
            DropIndex("Brands.BrandUserType", new[] { "Active" });
            DropIndex("Brands.BrandUserType", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandUserType", new[] { "CreatedDate" });
            DropIndex("Brands.BrandUserType", new[] { "CustomKey" });
            DropIndex("Brands.BrandUserType", new[] { "SlaveID" });
            DropIndex("Brands.BrandUserType", new[] { "MasterID" });
            DropIndex("Brands.BrandUserType", new[] { "ID" });
            DropIndex("Advertising.AdBrand", new[] { "Hash" });
            DropIndex("Advertising.AdBrand", new[] { "Active" });
            DropIndex("Advertising.AdBrand", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdBrand", new[] { "CreatedDate" });
            DropIndex("Advertising.AdBrand", new[] { "CustomKey" });
            DropIndex("Advertising.AdBrand", new[] { "SlaveID" });
            DropIndex("Advertising.AdBrand", new[] { "MasterID" });
            DropIndex("Advertising.AdBrand", new[] { "ID" });
            DropIndex("Brands.BrandCategory", new[] { "Hash" });
            DropIndex("Brands.BrandCategory", new[] { "Active" });
            DropIndex("Brands.BrandCategory", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandCategory", new[] { "CreatedDate" });
            DropIndex("Brands.BrandCategory", new[] { "CustomKey" });
            DropIndex("Brands.BrandCategory", new[] { "SlaveID" });
            DropIndex("Brands.BrandCategory", new[] { "MasterID" });
            DropIndex("Brands.BrandCategory", new[] { "ID" });
            DropIndex("Brands.BrandUser", new[] { "Hash" });
            DropIndex("Brands.BrandUser", new[] { "Active" });
            DropIndex("Brands.BrandUser", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandUser", new[] { "CreatedDate" });
            DropIndex("Brands.BrandUser", new[] { "CustomKey" });
            DropIndex("Brands.BrandUser", new[] { "SlaveID" });
            DropIndex("Brands.BrandUser", new[] { "MasterID" });
            DropIndex("Brands.BrandUser", new[] { "ID" });
            DropIndex("System.Note", new[] { "BrandID" });
            DropIndex("Brands.BrandProduct", new[] { "Hash" });
            DropIndex("Brands.BrandProduct", new[] { "Active" });
            DropIndex("Brands.BrandProduct", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandProduct", new[] { "CreatedDate" });
            DropIndex("Brands.BrandProduct", new[] { "CustomKey" });
            DropIndex("Brands.BrandProduct", new[] { "SlaveID" });
            DropIndex("Brands.BrandProduct", new[] { "MasterID" });
            DropIndex("Brands.BrandProduct", new[] { "ID" });
            DropIndex("Brands.BrandInventoryLocation", new[] { "Hash" });
            DropIndex("Brands.BrandInventoryLocation", new[] { "Active" });
            DropIndex("Brands.BrandInventoryLocation", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandInventoryLocation", new[] { "CreatedDate" });
            DropIndex("Brands.BrandInventoryLocation", new[] { "CustomKey" });
            DropIndex("Brands.BrandInventoryLocation", new[] { "TypeID" });
            DropIndex("Brands.BrandInventoryLocation", new[] { "SlaveID" });
            DropIndex("Brands.BrandInventoryLocation", new[] { "MasterID" });
            DropIndex("Brands.BrandInventoryLocation", new[] { "ID" });
            DropIndex("Brands.BrandCategoryType", new[] { "Hash" });
            DropIndex("Brands.BrandCategoryType", new[] { "Active" });
            DropIndex("Brands.BrandCategoryType", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandCategoryType", new[] { "CreatedDate" });
            DropIndex("Brands.BrandCategoryType", new[] { "CustomKey" });
            DropIndex("Brands.BrandCategoryType", new[] { "SlaveID" });
            DropIndex("Brands.BrandCategoryType", new[] { "MasterID" });
            DropIndex("Brands.BrandCategoryType", new[] { "ID" });
            DropIndex("Brands.BrandAccount", new[] { "Hash" });
            DropIndex("Brands.BrandAccount", new[] { "Active" });
            DropIndex("Brands.BrandAccount", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandAccount", new[] { "CreatedDate" });
            DropIndex("Brands.BrandAccount", new[] { "CustomKey" });
            DropIndex("Brands.BrandAccount", new[] { "PricePointID" });
            DropIndex("Brands.BrandAccount", new[] { "SlaveID" });
            DropIndex("Brands.BrandAccount", new[] { "MasterID" });
            DropIndex("Brands.BrandAccount", new[] { "ID" });
            DropIndex("Brands.Brand", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("Brands.Brand", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("Brands.Brand", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("Brands.Brand", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("Brands.Brand", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("Brands.Brand", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("Brands.Brand", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("Brands.Brand", new[] { "MinimumOrderDollarAmountBufferProductID" });
            DropColumn("System.Note", "BrandID");
            DropColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            DropColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            DropColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountBufferProductID");
            DropColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountBufferProductID");
            DropColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage");
            DropColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountWarningMessage");
            DropColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmountAfter");
            DropColumn("Brands.Brand", "MinimumForFreeShippingQuantityAmount");
            DropColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage");
            DropColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountWarningMessage");
            DropColumn("Brands.Brand", "MinimumForFreeShippingDollarAmountAfter");
            DropColumn("Brands.Brand", "MinimumForFreeShippingDollarAmount");
            DropColumn("Brands.Brand", "MinimumOrderQuantityAmountBufferCategoryID");
            DropColumn("Brands.Brand", "MinimumOrderDollarAmountBufferCategoryID");
            DropColumn("Brands.Brand", "MinimumOrderQuantityAmountBufferProductID");
            DropColumn("Brands.Brand", "MinimumOrderDollarAmountBufferProductID");
            DropColumn("Brands.Brand", "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage");
            DropColumn("Brands.Brand", "MinimumOrderQuantityAmountOverrideFeeWarningMessage");
            DropColumn("Brands.Brand", "MinimumOrderQuantityAmountOverrideFeeIsPercent");
            DropColumn("Brands.Brand", "MinimumOrderQuantityAmountOverrideFee");
            DropColumn("Brands.Brand", "MinimumOrderQuantityAmountWarningMessage");
            DropColumn("Brands.Brand", "MinimumOrderQuantityAmountAfter");
            DropColumn("Brands.Brand", "MinimumOrderQuantityAmount");
            DropColumn("Brands.Brand", "MinimumOrderDollarAmountOverrideFeeAcceptedMessage");
            DropColumn("Brands.Brand", "MinimumOrderDollarAmountOverrideFeeWarningMessage");
            DropColumn("Brands.Brand", "MinimumOrderDollarAmountOverrideFeeIsPercent");
            DropColumn("Brands.Brand", "MinimumOrderDollarAmountOverrideFee");
            DropColumn("Brands.Brand", "MinimumOrderDollarAmountWarningMessage");
            DropColumn("Brands.Brand", "MinimumOrderDollarAmountAfter");
            DropColumn("Brands.Brand", "MinimumOrderDollarAmount");
            DropTable("Scouting.ScoutCategoryType");
            DropTable("Scouting.Scout");
            DropTable("Scouting.ScoutCategory");
            DropTable("Pricing.PriceRuleBrand");
            DropTable("Discounts.DiscountBrands");
            DropTable("Brands.BrandInventoryLocationType");
            DropTable("Brands.BrandUserType");
            DropTable("Advertising.AdBrand");
            DropTable("Brands.BrandCategory");
            DropTable("Brands.BrandUser");
            DropTable("Brands.BrandProduct");
            DropTable("Brands.BrandInventoryLocation");
            DropTable("Brands.BrandCategoryType");
            DropTable("Brands.BrandAccount");
        }
    }
}
