// <copyright file="202109242150261_Franchises.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202109242150261 franchises class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Franchises : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Geography.District",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        RegionID = c.Int(),
                        CountryID = c.Int(nullable: false),
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
                .ForeignKey("Geography.Country", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("Geography.Region", t => t.RegionID)
                .Index(t => t.ID)
                .Index(t => t.RegionID)
                .Index(t => t.CountryID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.District", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Currencies.Currency", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Geography.DistrictImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(),
                        TypeID = c.Int(nullable: false),
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
                .ForeignKey("Geography.District", t => t.MasterID)
                .ForeignKey("Geography.DistrictImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.District", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Globalization.Language", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Tax.TaxDistrict",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.District", t => t.DistrictID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.DistrictID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseStore",
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.Franchise",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MinimumOrderDollarAmount = c.Decimal(precision: 18, scale: 4),
                        MinimumOrderDollarAmountAfter = c.Decimal(precision: 18, scale: 4),
                        MinimumOrderDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountOverrideFee = c.Decimal(precision: 18, scale: 4),
                        MinimumOrderDollarAmountOverrideFeeIsPercent = c.Boolean(nullable: false),
                        MinimumOrderDollarAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmount = c.Decimal(precision: 18, scale: 4),
                        MinimumOrderQuantityAmountAfter = c.Decimal(precision: 18, scale: 4),
                        MinimumOrderQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFee = c.Decimal(precision: 18, scale: 4),
                        MinimumOrderQuantityAmountOverrideFeeIsPercent = c.Boolean(nullable: false),
                        MinimumOrderQuantityAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountBufferProductID = c.Int(),
                        MinimumOrderQuantityAmountBufferProductID = c.Int(),
                        MinimumOrderDollarAmountBufferCategoryID = c.Int(),
                        MinimumOrderQuantityAmountBufferCategoryID = c.Int(),
                        MinimumForFreeShippingDollarAmount = c.Decimal(precision: 18, scale: 4),
                        MinimumForFreeShippingDollarAmountAfter = c.Decimal(precision: 18, scale: 4),
                        MinimumForFreeShippingDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmount = c.Decimal(precision: 18, scale: 4),
                        MinimumForFreeShippingQuantityAmountAfter = c.Decimal(precision: 18, scale: 4),
                        MinimumForFreeShippingQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountBufferProductID = c.Int(),
                        MinimumForFreeShippingQuantityAmountBufferProductID = c.Int(),
                        MinimumForFreeShippingDollarAmountBufferCategoryID = c.Int(),
                        MinimumForFreeShippingQuantityAmountBufferCategoryID = c.Int(),
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
                .ForeignKey("Categories.Category", t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .ForeignKey("Products.Product", t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .ForeignKey("Categories.Category", t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .ForeignKey("Products.Product", t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .ForeignKey("Categories.Category", t => t.MinimumOrderDollarAmountBufferCategoryID)
                .ForeignKey("Products.Product", t => t.MinimumOrderDollarAmountBufferProductID)
                .ForeignKey("Categories.Category", t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .ForeignKey("Products.Product", t => t.MinimumOrderQuantityAmountBufferProductID)
                .Index(t => t.ID)
                .Index(t => t.MinimumOrderDollarAmountBufferProductID)
                .Index(t => t.MinimumOrderQuantityAmountBufferProductID)
                .Index(t => t.MinimumOrderDollarAmountBufferCategoryID)
                .Index(t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        HasAccessToFranchise = c.Boolean(nullable: false),
                        PricePointID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
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
                "Brands.BrandFranchise",
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
                .ForeignKey("Franchises.Franchise", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseCategory",
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
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
                "Franchises.FranchiseInventoryLocation",
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Inventory.InventoryLocation", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("Franchises.FranchiseInventoryLocationType", t => t.TypeID, cascadeDelete: true)
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
                "Franchises.FranchiseInventoryLocationType",
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
                "Franchises.FranchiseUser",
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
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
                "Advertising.AdFranchise",
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
                .ForeignKey("Franchises.Franchise", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Discounts.DiscountFranchises",
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
                .ForeignKey("Franchises.Franchise", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseProduct",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleInFranchise = c.Boolean(nullable: false),
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
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
                "Franchises.FranchiseCountry",
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Geography.Country", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseCurrency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        CustomName = c.String(maxLength: 128, unicode: false),
                        CustomTranslationKey = c.String(maxLength: 128, unicode: false),
                        OverrideUnicodeSymbolValue = c.Decimal(nullable: false, precision: 18, scale: 4),
                        OverrideHtmlCharacterCode = c.String(maxLength: 12, unicode: false),
                        OverrideRawCharacter = c.String(maxLength: 5),
                        OverrideDecimalPlaceAccuracy = c.Int(),
                        OverrideUseSeparator = c.Boolean(),
                        OverrideRawDecimalCharacter = c.String(maxLength: 5),
                        OverrideHtmlDecimalCharacterCode = c.String(maxLength: 12, unicode: false),
                        OverrideRawSeparatorCharacter = c.String(maxLength: 5),
                        OverrideHtmlSeparatorCharacterCode = c.String(maxLength: 12, unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Currencies.Currency", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseDistrict",
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Geography.District", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseLanguage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        OverrideLocale = c.String(maxLength: 128),
                        OverrideUnicodeName = c.String(maxLength: 1024),
                        OverrideISO639_1_2002 = c.String(maxLength: 2, unicode: false),
                        OverrideISO639_2_1998 = c.String(maxLength: 3, unicode: false),
                        OverrideISO639_3_2007 = c.String(maxLength: 3, unicode: false),
                        OverrideISO639_5_2008 = c.String(maxLength: 3, unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Globalization.Language", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.OverrideLocale)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseRegion",
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Geography.Region", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseSiteDomain",
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Stores.SiteDomain", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(),
                        TypeID = c.Int(nullable: false),
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
                .ForeignKey("Franchises.Franchise", t => t.MasterID)
                .ForeignKey("Franchises.FranchiseImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseImageType",
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
                "Stores.StoreCountry",
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
                .ForeignKey("Stores.Store", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Geography.Country", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Stores.StoreDistrict",
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
                .ForeignKey("Stores.Store", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Geography.District", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("Geography.Region", "ISO31661", c => c.String(maxLength: 10));
            AddColumn("Geography.Region", "ISO31662", c => c.String(maxLength: 10));
            AddColumn("Geography.Region", "ISO3166Alpha2", c => c.String(maxLength: 10));
            AddColumn("Purchasing.PurchaseOrder", "FranchiseID", c => c.Int());
            AddColumn("Ordering.SalesOrder", "FranchiseID", c => c.Int());
            AddColumn("Invoicing.SalesInvoice", "FranchiseID", c => c.Int());
            AddColumn("Stores.Store", "StartDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Stores.Store", "EndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("System.Note", "FranchiseID", c => c.Int());
            AddColumn("Shopping.Cart", "FranchiseID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "FranchiseID", c => c.Int());
            AddColumn("Returning.SalesReturn", "FranchiseID", c => c.Int());
            AddColumn("Sampling.SampleRequest", "FranchiseID", c => c.Int());
            AddColumn("Products.ProductPricePoint", "FranchiseID", c => c.Int());
            RenameColumn("Tax.HistoricalTaxRate", "CountyLevelRate", "DistrictLevelRate");
            AlterColumn("Auctions.Auction", "OpensAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("Auctions.Auction", "ClosesAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("Purchasing.PurchaseOrder", "FranchiseID");
            CreateIndex("Ordering.SalesOrder", "FranchiseID");
            CreateIndex("Invoicing.SalesInvoice", "FranchiseID");
            CreateIndex("System.Note", "FranchiseID");
            CreateIndex("Shopping.Cart", "FranchiseID");
            CreateIndex("Quoting.SalesQuote", "FranchiseID");
            CreateIndex("Returning.SalesReturn", "FranchiseID");
            CreateIndex("Sampling.SampleRequest", "FranchiseID");
            CreateIndex("Products.ProductPricePoint", "FranchiseID");
            AddForeignKey("Shopping.Cart", "FranchiseID", "Franchises.Franchise", "ID");
            AddForeignKey("Quoting.SalesQuote", "FranchiseID", "Franchises.Franchise", "ID");
            AddForeignKey("Returning.SalesReturn", "FranchiseID", "Franchises.Franchise", "ID");
            AddForeignKey("Sampling.SampleRequest", "FranchiseID", "Franchises.Franchise", "ID");
            AddForeignKey("System.Note", "FranchiseID", "Franchises.Franchise", "ID");
            AddForeignKey("Products.ProductPricePoint", "FranchiseID", "Franchises.Franchise", "ID");
            AddForeignKey("Invoicing.SalesInvoice", "FranchiseID", "Franchises.Franchise", "ID");
            AddForeignKey("Ordering.SalesOrder", "FranchiseID", "Franchises.Franchise", "ID");
            AddForeignKey("Purchasing.PurchaseOrder", "FranchiseID", "Franchises.Franchise", "ID");
        }

        public override void Down()
        {
            RenameColumn("Tax.HistoricalTaxRate", "DistrictLevelRate", "CountyLevelRate");
            DropForeignKey("Purchasing.PurchaseOrder", "FranchiseID", "Franchises.Franchise");
            DropForeignKey("Ordering.SalesOrder", "FranchiseID", "Franchises.Franchise");
            DropForeignKey("Invoicing.SalesInvoice", "FranchiseID", "Franchises.Franchise");
            DropForeignKey("Stores.StoreDistrict", "SlaveID", "Geography.District");
            DropForeignKey("Stores.StoreDistrict", "MasterID", "Stores.Store");
            DropForeignKey("Stores.StoreCountry", "SlaveID", "Geography.Country");
            DropForeignKey("Stores.StoreCountry", "MasterID", "Stores.Store");
            DropForeignKey("Franchises.FranchiseStore", "SlaveID", "Stores.Store");
            DropForeignKey("Franchises.FranchiseStore", "MasterID", "Franchises.Franchise");
            DropForeignKey("Franchises.Franchise", "MinimumOrderQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Franchises.Franchise", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Franchises.Franchise", "MinimumOrderDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Franchises.Franchise", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Franchises.Franchise", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Franchises.Franchise", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Franchises.Franchise", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Franchises.Franchise", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Franchises.FranchiseImage", "TypeID", "Franchises.FranchiseImageType");
            DropForeignKey("Franchises.FranchiseImage", "MasterID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseSiteDomain", "SlaveID", "Stores.SiteDomain");
            DropForeignKey("Franchises.FranchiseSiteDomain", "MasterID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseRegion", "SlaveID", "Geography.Region");
            DropForeignKey("Franchises.FranchiseRegion", "MasterID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseLanguage", "SlaveID", "Globalization.Language");
            DropForeignKey("Franchises.FranchiseLanguage", "MasterID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseDistrict", "SlaveID", "Geography.District");
            DropForeignKey("Franchises.FranchiseDistrict", "MasterID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseCurrency", "SlaveID", "Currencies.Currency");
            DropForeignKey("Franchises.FranchiseCurrency", "MasterID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseCountry", "SlaveID", "Geography.Country");
            DropForeignKey("Franchises.FranchiseCountry", "MasterID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseCategory", "SlaveID", "Categories.Category");
            DropForeignKey("Products.ProductPricePoint", "FranchiseID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseProduct", "SlaveID", "Products.Product");
            DropForeignKey("Franchises.FranchiseProduct", "MasterID", "Franchises.Franchise");
            DropForeignKey("Discounts.DiscountFranchises", "SlaveID", "Franchises.Franchise");
            DropForeignKey("Discounts.DiscountFranchises", "MasterID", "Discounts.Discount");
            DropForeignKey("Advertising.AdFranchise", "SlaveID", "Franchises.Franchise");
            DropForeignKey("Advertising.AdFranchise", "MasterID", "Advertising.Ad");
            DropForeignKey("Franchises.FranchiseUser", "SlaveID", "Contacts.User");
            DropForeignKey("Franchises.FranchiseUser", "MasterID", "Franchises.Franchise");
            DropForeignKey("System.Note", "FranchiseID", "Franchises.Franchise");
            DropForeignKey("Sampling.SampleRequest", "FranchiseID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseInventoryLocation", "TypeID", "Franchises.FranchiseInventoryLocationType");
            DropForeignKey("Franchises.FranchiseInventoryLocation", "SlaveID", "Inventory.InventoryLocation");
            DropForeignKey("Franchises.FranchiseInventoryLocation", "MasterID", "Franchises.Franchise");
            DropForeignKey("Returning.SalesReturn", "FranchiseID", "Franchises.Franchise");
            DropForeignKey("Quoting.SalesQuote", "FranchiseID", "Franchises.Franchise");
            DropForeignKey("Shopping.Cart", "FranchiseID", "Franchises.Franchise");
            DropForeignKey("Franchises.FranchiseCategory", "MasterID", "Franchises.Franchise");
            DropForeignKey("Brands.BrandFranchise", "SlaveID", "Franchises.Franchise");
            DropForeignKey("Brands.BrandFranchise", "MasterID", "Brands.Brand");
            DropForeignKey("Franchises.FranchiseAccount", "SlaveID", "Accounts.Account");
            DropForeignKey("Franchises.FranchiseAccount", "PricePointID", "Pricing.PricePoint");
            DropForeignKey("Franchises.FranchiseAccount", "MasterID", "Franchises.Franchise");
            DropForeignKey("Tax.TaxDistrict", "DistrictID", "Geography.District");
            DropForeignKey("Geography.District", "RegionID", "Geography.Region");
            DropForeignKey("Geography.DistrictLanguage", "SlaveID", "Globalization.Language");
            DropForeignKey("Geography.DistrictLanguage", "MasterID", "Geography.District");
            DropForeignKey("Geography.DistrictImage", "TypeID", "Geography.DistrictImageType");
            DropForeignKey("Geography.DistrictImage", "MasterID", "Geography.District");
            DropForeignKey("Geography.DistrictCurrency", "SlaveID", "Currencies.Currency");
            DropForeignKey("Geography.DistrictCurrency", "MasterID", "Geography.District");
            DropForeignKey("Geography.District", "CountryID", "Geography.Country");
            DropIndex("Stores.StoreDistrict", new[] { "Hash" });
            DropIndex("Stores.StoreDistrict", new[] { "Active" });
            DropIndex("Stores.StoreDistrict", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreDistrict", new[] { "CreatedDate" });
            DropIndex("Stores.StoreDistrict", new[] { "CustomKey" });
            DropIndex("Stores.StoreDistrict", new[] { "SlaveID" });
            DropIndex("Stores.StoreDistrict", new[] { "MasterID" });
            DropIndex("Stores.StoreDistrict", new[] { "ID" });
            DropIndex("Stores.StoreCountry", new[] { "Hash" });
            DropIndex("Stores.StoreCountry", new[] { "Active" });
            DropIndex("Stores.StoreCountry", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreCountry", new[] { "CreatedDate" });
            DropIndex("Stores.StoreCountry", new[] { "CustomKey" });
            DropIndex("Stores.StoreCountry", new[] { "SlaveID" });
            DropIndex("Stores.StoreCountry", new[] { "MasterID" });
            DropIndex("Stores.StoreCountry", new[] { "ID" });
            DropIndex("Franchises.FranchiseImageType", new[] { "Hash" });
            DropIndex("Franchises.FranchiseImageType", new[] { "Active" });
            DropIndex("Franchises.FranchiseImageType", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseImageType", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseImageType", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseImageType", new[] { "Name" });
            DropIndex("Franchises.FranchiseImageType", new[] { "SortOrder" });
            DropIndex("Franchises.FranchiseImageType", new[] { "DisplayName" });
            DropIndex("Franchises.FranchiseImageType", new[] { "ID" });
            DropIndex("Franchises.FranchiseImage", new[] { "Hash" });
            DropIndex("Franchises.FranchiseImage", new[] { "Active" });
            DropIndex("Franchises.FranchiseImage", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseImage", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseImage", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseImage", new[] { "Name" });
            DropIndex("Franchises.FranchiseImage", new[] { "TypeID" });
            DropIndex("Franchises.FranchiseImage", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseImage", new[] { "ID" });
            DropIndex("Franchises.FranchiseSiteDomain", new[] { "Hash" });
            DropIndex("Franchises.FranchiseSiteDomain", new[] { "Active" });
            DropIndex("Franchises.FranchiseSiteDomain", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseSiteDomain", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseSiteDomain", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseSiteDomain", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseSiteDomain", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseSiteDomain", new[] { "ID" });
            DropIndex("Franchises.FranchiseRegion", new[] { "Hash" });
            DropIndex("Franchises.FranchiseRegion", new[] { "Active" });
            DropIndex("Franchises.FranchiseRegion", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseRegion", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseRegion", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseRegion", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseRegion", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseRegion", new[] { "ID" });
            DropIndex("Franchises.FranchiseLanguage", new[] { "Hash" });
            DropIndex("Franchises.FranchiseLanguage", new[] { "Active" });
            DropIndex("Franchises.FranchiseLanguage", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseLanguage", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseLanguage", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseLanguage", new[] { "OverrideLocale" });
            DropIndex("Franchises.FranchiseLanguage", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseLanguage", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseLanguage", new[] { "ID" });
            DropIndex("Franchises.FranchiseDistrict", new[] { "Hash" });
            DropIndex("Franchises.FranchiseDistrict", new[] { "Active" });
            DropIndex("Franchises.FranchiseDistrict", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseDistrict", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseDistrict", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseDistrict", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseDistrict", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseDistrict", new[] { "ID" });
            DropIndex("Franchises.FranchiseCurrency", new[] { "Hash" });
            DropIndex("Franchises.FranchiseCurrency", new[] { "Active" });
            DropIndex("Franchises.FranchiseCurrency", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseCurrency", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseCurrency", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseCurrency", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseCurrency", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseCurrency", new[] { "ID" });
            DropIndex("Franchises.FranchiseCountry", new[] { "Hash" });
            DropIndex("Franchises.FranchiseCountry", new[] { "Active" });
            DropIndex("Franchises.FranchiseCountry", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseCountry", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseCountry", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseCountry", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseCountry", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseCountry", new[] { "ID" });
            DropIndex("Products.ProductPricePoint", new[] { "FranchiseID" });
            DropIndex("Franchises.FranchiseProduct", new[] { "Hash" });
            DropIndex("Franchises.FranchiseProduct", new[] { "Active" });
            DropIndex("Franchises.FranchiseProduct", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseProduct", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseProduct", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseProduct", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseProduct", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseProduct", new[] { "ID" });
            DropIndex("Discounts.DiscountFranchises", new[] { "Hash" });
            DropIndex("Discounts.DiscountFranchises", new[] { "Active" });
            DropIndex("Discounts.DiscountFranchises", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountFranchises", new[] { "CreatedDate" });
            DropIndex("Discounts.DiscountFranchises", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountFranchises", new[] { "SlaveID" });
            DropIndex("Discounts.DiscountFranchises", new[] { "MasterID" });
            DropIndex("Discounts.DiscountFranchises", new[] { "ID" });
            DropIndex("Advertising.AdFranchise", new[] { "Hash" });
            DropIndex("Advertising.AdFranchise", new[] { "Active" });
            DropIndex("Advertising.AdFranchise", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdFranchise", new[] { "CreatedDate" });
            DropIndex("Advertising.AdFranchise", new[] { "CustomKey" });
            DropIndex("Advertising.AdFranchise", new[] { "SlaveID" });
            DropIndex("Advertising.AdFranchise", new[] { "MasterID" });
            DropIndex("Advertising.AdFranchise", new[] { "ID" });
            DropIndex("Franchises.FranchiseUser", new[] { "Hash" });
            DropIndex("Franchises.FranchiseUser", new[] { "Active" });
            DropIndex("Franchises.FranchiseUser", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseUser", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseUser", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseUser", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseUser", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseUser", new[] { "ID" });
            DropIndex("Sampling.SampleRequest", new[] { "FranchiseID" });
            DropIndex("Franchises.FranchiseInventoryLocationType", new[] { "Hash" });
            DropIndex("Franchises.FranchiseInventoryLocationType", new[] { "Active" });
            DropIndex("Franchises.FranchiseInventoryLocationType", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseInventoryLocationType", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseInventoryLocationType", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseInventoryLocationType", new[] { "Name" });
            DropIndex("Franchises.FranchiseInventoryLocationType", new[] { "SortOrder" });
            DropIndex("Franchises.FranchiseInventoryLocationType", new[] { "DisplayName" });
            DropIndex("Franchises.FranchiseInventoryLocationType", new[] { "ID" });
            DropIndex("Franchises.FranchiseInventoryLocation", new[] { "Hash" });
            DropIndex("Franchises.FranchiseInventoryLocation", new[] { "Active" });
            DropIndex("Franchises.FranchiseInventoryLocation", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseInventoryLocation", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseInventoryLocation", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseInventoryLocation", new[] { "TypeID" });
            DropIndex("Franchises.FranchiseInventoryLocation", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseInventoryLocation", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseInventoryLocation", new[] { "ID" });
            DropIndex("Returning.SalesReturn", new[] { "FranchiseID" });
            DropIndex("Quoting.SalesQuote", new[] { "FranchiseID" });
            DropIndex("Shopping.Cart", new[] { "FranchiseID" });
            DropIndex("System.Note", new[] { "FranchiseID" });
            DropIndex("Franchises.FranchiseCategory", new[] { "Hash" });
            DropIndex("Franchises.FranchiseCategory", new[] { "Active" });
            DropIndex("Franchises.FranchiseCategory", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseCategory", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseCategory", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseCategory", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseCategory", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseCategory", new[] { "ID" });
            DropIndex("Brands.BrandFranchise", new[] { "Hash" });
            DropIndex("Brands.BrandFranchise", new[] { "Active" });
            DropIndex("Brands.BrandFranchise", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandFranchise", new[] { "CreatedDate" });
            DropIndex("Brands.BrandFranchise", new[] { "CustomKey" });
            DropIndex("Brands.BrandFranchise", new[] { "SlaveID" });
            DropIndex("Brands.BrandFranchise", new[] { "MasterID" });
            DropIndex("Brands.BrandFranchise", new[] { "ID" });
            DropIndex("Franchises.FranchiseAccount", new[] { "Hash" });
            DropIndex("Franchises.FranchiseAccount", new[] { "Active" });
            DropIndex("Franchises.FranchiseAccount", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseAccount", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseAccount", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseAccount", new[] { "PricePointID" });
            DropIndex("Franchises.FranchiseAccount", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseAccount", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseAccount", new[] { "ID" });
            DropIndex("Franchises.Franchise", new[] { "Hash" });
            DropIndex("Franchises.Franchise", new[] { "Active" });
            DropIndex("Franchises.Franchise", new[] { "UpdatedDate" });
            DropIndex("Franchises.Franchise", new[] { "CreatedDate" });
            DropIndex("Franchises.Franchise", new[] { "CustomKey" });
            DropIndex("Franchises.Franchise", new[] { "Name" });
            DropIndex("Franchises.Franchise", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("Franchises.Franchise", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("Franchises.Franchise", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("Franchises.Franchise", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("Franchises.Franchise", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("Franchises.Franchise", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("Franchises.Franchise", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("Franchises.Franchise", new[] { "MinimumOrderDollarAmountBufferProductID" });
            DropIndex("Franchises.Franchise", new[] { "ID" });
            DropIndex("Franchises.FranchiseStore", new[] { "Hash" });
            DropIndex("Franchises.FranchiseStore", new[] { "Active" });
            DropIndex("Franchises.FranchiseStore", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseStore", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseStore", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseStore", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseStore", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseStore", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "FranchiseID" });
            DropIndex("Ordering.SalesOrder", new[] { "FranchiseID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "FranchiseID" });
            DropIndex("Tax.TaxDistrict", new[] { "Hash" });
            DropIndex("Tax.TaxDistrict", new[] { "Active" });
            DropIndex("Tax.TaxDistrict", new[] { "UpdatedDate" });
            DropIndex("Tax.TaxDistrict", new[] { "CreatedDate" });
            DropIndex("Tax.TaxDistrict", new[] { "CustomKey" });
            DropIndex("Tax.TaxDistrict", new[] { "Name" });
            DropIndex("Tax.TaxDistrict", new[] { "DistrictID" });
            DropIndex("Tax.TaxDistrict", new[] { "ID" });
            DropIndex("Geography.DistrictLanguage", new[] { "Hash" });
            DropIndex("Geography.DistrictLanguage", new[] { "Active" });
            DropIndex("Geography.DistrictLanguage", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictLanguage", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictLanguage", new[] { "CustomKey" });
            DropIndex("Geography.DistrictLanguage", new[] { "SlaveID" });
            DropIndex("Geography.DistrictLanguage", new[] { "MasterID" });
            DropIndex("Geography.DistrictLanguage", new[] { "ID" });
            DropIndex("Geography.DistrictImageType", new[] { "Hash" });
            DropIndex("Geography.DistrictImageType", new[] { "Active" });
            DropIndex("Geography.DistrictImageType", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictImageType", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictImageType", new[] { "CustomKey" });
            DropIndex("Geography.DistrictImageType", new[] { "Name" });
            DropIndex("Geography.DistrictImageType", new[] { "SortOrder" });
            DropIndex("Geography.DistrictImageType", new[] { "DisplayName" });
            DropIndex("Geography.DistrictImageType", new[] { "ID" });
            DropIndex("Geography.DistrictImage", new[] { "Hash" });
            DropIndex("Geography.DistrictImage", new[] { "Active" });
            DropIndex("Geography.DistrictImage", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictImage", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictImage", new[] { "CustomKey" });
            DropIndex("Geography.DistrictImage", new[] { "Name" });
            DropIndex("Geography.DistrictImage", new[] { "TypeID" });
            DropIndex("Geography.DistrictImage", new[] { "MasterID" });
            DropIndex("Geography.DistrictImage", new[] { "ID" });
            DropIndex("Geography.DistrictCurrency", new[] { "Hash" });
            DropIndex("Geography.DistrictCurrency", new[] { "Active" });
            DropIndex("Geography.DistrictCurrency", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictCurrency", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictCurrency", new[] { "CustomKey" });
            DropIndex("Geography.DistrictCurrency", new[] { "SlaveID" });
            DropIndex("Geography.DistrictCurrency", new[] { "MasterID" });
            DropIndex("Geography.DistrictCurrency", new[] { "ID" });
            DropIndex("Geography.District", new[] { "Hash" });
            DropIndex("Geography.District", new[] { "Active" });
            DropIndex("Geography.District", new[] { "UpdatedDate" });
            DropIndex("Geography.District", new[] { "CreatedDate" });
            DropIndex("Geography.District", new[] { "CustomKey" });
            DropIndex("Geography.District", new[] { "Name" });
            DropIndex("Geography.District", new[] { "CountryID" });
            DropIndex("Geography.District", new[] { "RegionID" });
            DropIndex("Geography.District", new[] { "ID" });
            AlterColumn("Auctions.Auction", "ClosesAt", c => c.DateTime());
            AlterColumn("Auctions.Auction", "OpensAt", c => c.DateTime());
            DropColumn("Products.ProductPricePoint", "FranchiseID");
            DropColumn("Sampling.SampleRequest", "FranchiseID");
            DropColumn("Returning.SalesReturn", "FranchiseID");
            DropColumn("Quoting.SalesQuote", "FranchiseID");
            DropColumn("Shopping.Cart", "FranchiseID");
            DropColumn("System.Note", "FranchiseID");
            DropColumn("Stores.Store", "EndDate");
            DropColumn("Stores.Store", "StartDate");
            DropColumn("Invoicing.SalesInvoice", "FranchiseID");
            DropColumn("Ordering.SalesOrder", "FranchiseID");
            DropColumn("Purchasing.PurchaseOrder", "FranchiseID");
            DropColumn("Geography.Region", "ISO3166Alpha2");
            DropColumn("Geography.Region", "ISO31662");
            DropColumn("Geography.Region", "ISO31661");
            DropTable("Stores.StoreDistrict");
            DropTable("Stores.StoreCountry");
            DropTable("Franchises.FranchiseImageType");
            DropTable("Franchises.FranchiseImage");
            DropTable("Franchises.FranchiseSiteDomain");
            DropTable("Franchises.FranchiseRegion");
            DropTable("Franchises.FranchiseLanguage");
            DropTable("Franchises.FranchiseDistrict");
            DropTable("Franchises.FranchiseCurrency");
            DropTable("Franchises.FranchiseCountry");
            DropTable("Franchises.FranchiseProduct");
            DropTable("Discounts.DiscountFranchises");
            DropTable("Advertising.AdFranchise");
            DropTable("Franchises.FranchiseUser");
            DropTable("Franchises.FranchiseInventoryLocationType");
            DropTable("Franchises.FranchiseInventoryLocation");
            DropTable("Franchises.FranchiseCategory");
            DropTable("Brands.BrandFranchise");
            DropTable("Franchises.FranchiseAccount");
            DropTable("Franchises.Franchise");
            DropTable("Franchises.FranchiseStore");
            DropTable("Tax.TaxDistrict");
            DropTable("Geography.DistrictLanguage");
            DropTable("Geography.DistrictImageType");
            DropTable("Geography.DistrictImage");
            DropTable("Geography.DistrictCurrency");
            DropTable("Geography.District");
        }
    }
}
