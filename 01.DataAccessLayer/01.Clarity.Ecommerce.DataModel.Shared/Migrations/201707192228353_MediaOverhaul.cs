// <copyright file="201707192228353_MediaOverhaul.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201707192228353 media overhaul class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MediaOverhaul : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrderID", "Purchasing.PurchaseOrder");
            DropForeignKey("Ordering.SalesOrderContact", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Categories.CategoryImage", "CategoryID", "Categories.Category");
            DropForeignKey("Shopping.CartContact", "CartID", "Shopping.Cart");
            DropForeignKey("Quoting.SalesQuoteContact", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("Sampling.SampleRequestContact", "SampleRequestID", "Sampling.SampleRequest");
            DropIndex("Categories.Category", new[] { "CategoryHash" });
            DropIndex("Products.Product", new[] { "ProductHash" });
            CreateTable(
                "Currencies.CurrencyImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Currencies.Currency", t => t.MasterID)
                .ForeignKey("Currencies.CurrencyImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Currencies.CurrencyImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Geography.CountryImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Country", t => t.MasterID)
                .ForeignKey("Geography.CountryImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Geography.CountryImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Globalization.LanguageImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Globalization.Language", t => t.MasterID)
                .ForeignKey("Globalization.LanguageImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Globalization.LanguageImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Geography.DistrictImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.District", t => t.MasterID)
                .ForeignKey("Geography.DistrictImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Geography.DistrictImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Geography.RegionImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Region", t => t.MasterID)
                .ForeignKey("Geography.RegionImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Geography.RegionImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Categories.CategoryImageNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.CategoryImageType", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("Categories.Category", t => t.MasterID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Categories.CategoryImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Manufacturers.ManufacturerImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Manufacturers.Manufacturer", t => t.MasterID)
                .ForeignKey("Manufacturers.ManufacturerImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Manufacturers.ManufacturerImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Stores.BrandImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Brand", t => t.MasterID)
                .ForeignKey("Stores.BrandImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Stores.BrandImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Stores.StoreImageNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.MasterID)
                .ForeignKey("Stores.StoreImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Stores.StoreImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Vendors.VendorImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Vendors.Vendor", t => t.MasterID)
                .ForeignKey("Vendors.VendorImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Vendors.VendorImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Contacts.UserImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.MasterID)
                .ForeignKey("Contacts.UserImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Contacts.UserImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Media.StoredFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SortOrder = c.Int(),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        Copyright = c.String(),
                        FileFormat = c.String(),
                        FileName = c.String(),
                        IsStoredInDB = c.Boolean(nullable: false),
                        Bytes = c.Binary(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name);

            CreateTable(
                "Quoting.SalesQuoteFileNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Quoting.SalesQuote", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "CalendarEvents.CalendarEventFileNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CalendarEvents.CalendarEvent", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Sampling.SampleRequestFileNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Sampling.SampleRequest", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Shopping.CartFileNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shopping.Cart", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Products.ProductImageNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.MasterID)
                .ForeignKey("Products.ProductImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Products.ProductFileNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Categories.CategoryFileNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Invoicing.SalesInvoiceFileNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Invoicing.SalesInvoice", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Ordering.SalesOrderFileNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Ordering.SalesOrder", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Purchasing.PurchaseOrderFileNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Purchasing.PurchaseOrder", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Contacts.ContactImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.MasterID)
                .ForeignKey("Contacts.ContactImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Contacts.ContactImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Accounts.AccountImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.MasterID)
                .ForeignKey("Accounts.AccountImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Accounts.AccountImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Advertising.AdImageNew",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Advertising.Ad", t => t.MasterID)
                .ForeignKey("Advertising.AdImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Advertising.AdImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            AddColumn("Accounts.AccountAttribute", "Hash", c => c.Long());
            AddColumn("Attributes.AttributeValue", "Hash", c => c.Long());
            AddColumn("Attributes.GeneralAttribute", "Hash", c => c.Long());
            AddColumn("Attributes.AttributeType", "Hash", c => c.Long());
            AddColumn("Accounts.AccountContact", "Hash", c => c.Long());
            AddColumn("Contacts.Contact", "Hash", c => c.Long());
            AddColumn("Geography.Address", "Hash", c => c.Long());
            AddColumn("Geography.CountryCurrency", "Hash", c => c.Long());
            AddColumn("Currencies.Currency", "Hash", c => c.Long());
            AddColumn("Currencies.HistoricalCurrencyRate", "Hash", c => c.Long());
            AddColumn("Media.Library", "Hash", c => c.Long());
            AddColumn("Media.Audio", "Hash", c => c.Long());
            AddColumn("Media.File", "Hash", c => c.Long());
            AddColumn("Media.FileData", "Hash", c => c.Long());
            AddColumn("Media.Image", "Hash", c => c.Long());
            AddColumn("Media.Document", "Hash", c => c.Long());
            AddColumn("Media.LibraryType", "Hash", c => c.Long());
            AddColumn("Media.Video", "Hash", c => c.Long());
            AddColumn("Geography.CountryLanguage", "Hash", c => c.Long());
            AddColumn("Globalization.Language", "Hash", c => c.Long());
            AddColumn("Geography.RegionCurrency", "Hash", c => c.Long());
            AddColumn("Geography.District", "Hash", c => c.Long());
            AddColumn("Geography.DistrictCurrency", "Hash", c => c.Long());
            AddColumn("Geography.DistrictLanguage", "Hash", c => c.Long());
            AddColumn("Tax.TaxDistrict", "Hash", c => c.Long());
            AddColumn("Geography.InterRegion", "Hash", c => c.Long());
            AddColumn("Geography.RegionLanguage", "Hash", c => c.Long());
            AddColumn("Tax.TaxRegion", "Hash", c => c.Long());
            AddColumn("Tax.TaxCountry", "Hash", c => c.Long());
            AddColumn("Purchasing.SalesOrderPurchaseOrder", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesOrderSalesInvoice", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.SalesInvoiceDiscounts", "Hash", c => c.Long());
            AddColumn("Discounts.Discount", "Hash", c => c.Long());
            AddColumn("Discounts.DiscountCategories", "Hash", c => c.Long());
            AddColumn("Categories.Category", "Hash", c => c.Long());
            AddColumn("Categories.CategoryAttribute", "Hash", c => c.Long());
            AddColumn("Categories.CategoryFile", "Hash", c => c.Long());
            AddColumn("Categories.CategoryFile", "SeoKeywords", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Categories.CategoryFile", "SeoUrl", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Categories.CategoryFile", "SeoPageTitle", c => c.String(maxLength: 75, unicode: false));
            AddColumn("Categories.CategoryFile", "SeoDescription", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Categories.CategoryFile", "SeoMetaData", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Categories.CategoryImage", "Hash", c => c.Long());
            AddColumn("Products.ProductCategory", "Hash", c => c.Long());
            AddColumn("Products.ProductCategoryAttribute", "Hash", c => c.Long());
            AddColumn("Products.Product", "Hash", c => c.Long());
            AddColumn("Products.ProductAttribute", "Hash", c => c.Long());
            AddColumn("Shopping.CartItem", "Hash", c => c.Long());
            AddColumn("Shopping.CartItemAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.CartItemDiscounts", "Hash", c => c.Long());
            AddColumn("Shopping.Cart", "Hash", c => c.Long());
            AddColumn("Shopping.CartAttribute", "Hash", c => c.Long());
            AddColumn("Shopping.CartContact", "Hash", c => c.Long());
            AddColumn("Shopping.CartContact", "Cart_ID", c => c.Int());
            AddColumn("Shopping.CartContact", "Cart_ID1", c => c.Int());
            AddColumn("Discounts.CartDiscounts", "Hash", c => c.Long());
            AddColumn("Shopping.CartFile", "Hash", c => c.Long());
            AddColumn("System.Note", "Hash", c => c.Long());
            AddColumn("Contacts.UserAttribute", "Hash", c => c.Long());
            AddColumn("Favorites.FavoriteCategory", "Hash", c => c.Long());
            AddColumn("Favorites.FavoriteManufacturer", "Hash", c => c.Long());
            AddColumn("Manufacturers.ManufacturerProduct", "Hash", c => c.Long());
            AddColumn("Reviews.Review", "Hash", c => c.Long());
            AddColumn("Stores.BrandStore", "Hash", c => c.Long());
            AddColumn("Stores.Brand", "Hash", c => c.Long());
            AddColumn("Stores.BrandSiteDomain", "Hash", c => c.Long());
            AddColumn("Stores.SiteDomain", "Hash", c => c.Long());
            AddColumn("Stores.SiteDomainSocialProvider", "Hash", c => c.Long());
            AddColumn("Stores.SocialProvider", "Hash", c => c.Long());
            AddColumn("Stores.StoreSiteDomain", "Hash", c => c.Long());
            AddColumn("Stores.StoreAccount", "Hash", c => c.Long());
            AddColumn("Pricing.PricePoint", "Hash", c => c.Long());
            AddColumn("Stores.StoreCategory", "Hash", c => c.Long());
            AddColumn("Stores.StoreCategoryType", "Hash", c => c.Long());
            AddColumn("Categories.CategoryType", "Hash", c => c.Long());
            AddColumn("Stores.StoreContact", "Hash", c => c.Long());
            AddColumn("Stores.StoreImage", "Hash", c => c.Long());
            AddColumn("Stores.StoreInventoryLocation", "Hash", c => c.Long());
            AddColumn("Inventory.InventoryLocationSection", "Hash", c => c.Long());
            AddColumn("Products.ProductInventoryLocationSection", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderItemShipment", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderItem", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderItemAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.SalesOrderItemDiscounts", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderItemStatus", "Hash", c => c.Long());
            AddColumn("Stores.StoreProduct", "Hash", c => c.Long());
            AddColumn("Vendors.VendorProduct", "Hash", c => c.Long());
            AddColumn("Vendors.Vendor", "Hash", c => c.Long());
            AddColumn("Contacts.ContactMethod", "Hash", c => c.Long());
            AddColumn("Vendors.ShipVia", "Hash", c => c.Long());
            AddColumn("Shipping.ShipCarrier", "Hash", c => c.Long());
            AddColumn("Shipping.CarrierInvoice", "Hash", c => c.Long());
            AddColumn("Shipping.CarrierOrigin", "Hash", c => c.Long());
            AddColumn("Shipping.ShipCarrierMethod", "Hash", c => c.Long());
            AddColumn("Stores.StoreVendor", "Hash", c => c.Long());
            AddColumn("Vendors.Term", "Hash", c => c.Long());
            AddColumn("Vendors.VendorTerm", "Hash", c => c.Long());
            AddColumn("Vendors.VendorManufacturer", "Hash", c => c.Long());
            AddColumn("Shipping.ShipmentEvent", "Hash", c => c.Long());
            AddColumn("Shipping.ShipmentStatus", "Hash", c => c.Long());
            AddColumn("Shipping.ShipmentType", "Hash", c => c.Long());
            AddColumn("Stores.StoreManufacturer", "Hash", c => c.Long());
            AddColumn("Stores.StoreSubscription", "Hash", c => c.Long());
            AddColumn("Payments.PaymentMethod", "Hash", c => c.Long());
            AddColumn("Payments.PaymentStatus", "Hash", c => c.Long());
            AddColumn("Payments.SubscriptionHistory", "Hash", c => c.Long());
            AddColumn("Payments.PaymentType", "Hash", c => c.Long());
            AddColumn("Payments.RepeatType", "Hash", c => c.Long());
            AddColumn("Payments.SubscriptionStatus", "Hash", c => c.Long());
            AddColumn("Payments.SubscriptionType", "Hash", c => c.Long());
            AddColumn("Products.ProductSubscriptionType", "Hash", c => c.Long());
            AddColumn("Stores.StoreUser", "Hash", c => c.Long());
            AddColumn("Stores.StoreUserType", "Hash", c => c.Long());
            AddColumn("Contacts.UserType", "Hash", c => c.Long());
            AddColumn("Stores.StoreType", "Hash", c => c.Long());
            AddColumn("Reviews.ReviewType", "Hash", c => c.Long());
            AddColumn("Favorites.FavoriteStore", "Hash", c => c.Long());
            AddColumn("Favorites.FavoriteVendor", "Hash", c => c.Long());
            AddColumn("Messaging.MessageAttachment", "Hash", c => c.Long());
            AddColumn("Messaging.MessageAttachment", "StoredFileID", c => c.Int(nullable: false));
            AddColumn("Messaging.Message", "Hash", c => c.Long());
            AddColumn("Messaging.Conversation", "Hash", c => c.Long());
            AddColumn("Messaging.MessageRecipient", "Hash", c => c.Long());
            AddColumn("Messaging.EmailQueue", "Hash", c => c.Long());
            AddColumn("Messaging.EmailTemplate", "Hash", c => c.Long());
            AddColumn("Messaging.EmailStatus", "Hash", c => c.Long());
            AddColumn("Messaging.EmailType", "Hash", c => c.Long());
            AddColumn("Groups.Group", "Hash", c => c.Long());
            AddColumn("Groups.GroupUser", "Hash", c => c.Long());
            AddColumn("Groups.GroupStatus", "Hash", c => c.Long());
            AddColumn("Groups.GroupType", "Hash", c => c.Long());
            AddColumn("Notifications.NotificationMessage", "Hash", c => c.Long());
            AddColumn("Notifications.Notification", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteSalesOrder", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.SalesQuoteDiscounts", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteFile", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteItem", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteItemAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.SalesQuoteItemDiscounts", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteItemShipment", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteItemStatus", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteContact", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteContact", "SalesQuote_ID", c => c.Int());
            AddColumn("Quoting.SalesQuoteContact", "SalesQuote_ID1", c => c.Int());
            AddColumn("Shipping.ShipOption", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteState", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteStatus", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuoteType", "Hash", c => c.Long());
            AddColumn("Contacts.UserStatus", "Hash", c => c.Long());
            AddColumn("CalendarEvents.UserEventAttendance", "Hash", c => c.Long());
            AddColumn("CalendarEvents.CalendarEvent", "Hash", c => c.Long());
            AddColumn("CalendarEvents.CalendarEventDetail", "Hash", c => c.Long());
            AddColumn("CalendarEvents.CalendarEventFile", "Hash", c => c.Long());
            AddColumn("CalendarEvents.CalendarEventProducts", "Hash", c => c.Long());
            AddColumn("CalendarEvents.CalendarEventStatus", "Hash", c => c.Long());
            AddColumn("CalendarEvents.CalendarEventType", "Hash", c => c.Long());
            AddColumn("CalendarEvents.UserEventAttendanceType", "Hash", c => c.Long());
            AddColumn("Payments.Wallet", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.SampleRequestDiscounts", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestFile", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestItem", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestItemAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.SampleRequestItemDiscounts", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestItemShipment", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestItemStatus", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestContact", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestContact", "SampleRequest_ID", c => c.Int());
            AddColumn("Sampling.SampleRequestContact", "SampleRequest_ID1", c => c.Int());
            AddColumn("Sampling.SampleRequestState", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestStatus", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequestType", "Hash", c => c.Long());
            AddColumn("System.NoteType", "Hash", c => c.Long());
            AddColumn("Shopping.CartState", "Hash", c => c.Long());
            AddColumn("Shopping.CartStatus", "Hash", c => c.Long());
            AddColumn("Shopping.CartType", "Hash", c => c.Long());
            AddColumn("Shopping.CartItemShipment", "Hash", c => c.Long());
            AddColumn("Shopping.CartItemStatus", "Hash", c => c.Long());
            AddColumn("Discounts.DiscountProducts", "Hash", c => c.Long());
            AddColumn("Shipping.Package", "Hash", c => c.Long());
            AddColumn("Shipping.PackageType", "Hash", c => c.Long());
            AddColumn("Products.ProductAssociation", "Hash", c => c.Long());
            AddColumn("Products.ProductAssociationAttribute", "Hash", c => c.Long());
            AddColumn("Products.ProductAssociationType", "Hash", c => c.Long());
            AddColumn("Products.ProductFile", "Hash", c => c.Long());
            AddColumn("Products.ProductImage", "Hash", c => c.Long());
            AddColumn("Products.ProductImageType", "Hash", c => c.Long());
            AddColumn("Pricing.PriceRounding", "Hash", c => c.Long());
            AddColumn("Products.ProductType", "Hash", c => c.Long());
            AddColumn("Discounts.DiscountCode", "Hash", c => c.Long());
            AddColumn("Discounts.DiscountProductType", "Hash", c => c.Long());
            AddColumn("Discounts.DiscountStores", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceFile", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceContact", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceContact", "SalesInvoice_ID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1", c => c.Int());
            AddColumn("Payments.SalesInvoicePayment", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceItem", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceItemAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.SalesInvoiceItemDiscounts", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceItemShipment", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceItemStatus", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceState", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceStatus", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoiceType", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderAttribute", "Hash", c => c.Long());
            AddColumn("Contacts.CustomerPriority", "Hash", c => c.Long());
            AddColumn("Discounts.SalesOrderDiscounts", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderFile", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderContact", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderContact", "SalesOrder_ID", c => c.Int());
            AddColumn("Ordering.SalesOrderContact", "SalesOrder_ID1", c => c.Int());
            AddColumn("Payments.SalesOrderPayment", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderState", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderStatus", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrderType", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.PurchaseOrderDiscounts", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderFile", "Hash", c => c.Long());
            AddColumn("Purchasing.FreeOnBoard", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderContact", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItem", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderItemAttribute", "Hash", c => c.Long());
            AddColumn("Discounts.PurchaseOrderItemDiscounts", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderItemShipment", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderItemStatus", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderState", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderStatus", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrderType", "Hash", c => c.Long());
            AddColumn("Contacts.ContactType", "Hash", c => c.Long());
            AddColumn("Accounts.AccountPricePoint", "Hash", c => c.Long());
            AddColumn("Accounts.AccountTerm", "Hash", c => c.Long());
            AddColumn("Contacts.Opportunities", "Hash", c => c.Long());
            AddColumn("Accounts.AccountStatus", "Hash", c => c.Long());
            AddColumn("Accounts.AccountType", "Hash", c => c.Long());
            AddColumn("Advertising.AdAccount", "Hash", c => c.Long());
            AddColumn("Advertising.Ad", "Hash", c => c.Long());
            AddColumn("Advertising.AdImage", "Hash", c => c.Long());
            AddColumn("Advertising.AdStore", "Hash", c => c.Long());
            AddColumn("Advertising.AdZone", "Hash", c => c.Long());
            AddColumn("Advertising.AdZoneAccess", "Hash", c => c.Long());
            AddColumn("Counters.Counter", "Hash", c => c.Long());
            AddColumn("Counters.CounterLog", "Hash", c => c.Long());
            AddColumn("Counters.CounterLogType", "Hash", c => c.Long());
            AddColumn("Counters.CounterType", "Hash", c => c.Long());
            AddColumn("Advertising.Zone", "Hash", c => c.Long());
            AddColumn("Advertising.ZoneStatus", "Hash", c => c.Long());
            AddColumn("Advertising.ZoneType", "Hash", c => c.Long());
            AddColumn("Tracking.CampaignAd", "Hash", c => c.Long());
            AddColumn("Tracking.Campaign", "Hash", c => c.Long());
            AddColumn("Tracking.CampaignStatus", "Hash", c => c.Long());
            AddColumn("Tracking.CampaignType", "Hash", c => c.Long());
            AddColumn("Advertising.AdStatus", "Hash", c => c.Long());
            AddColumn("Advertising.AdType", "Hash", c => c.Long());
            AddColumn("System.SystemLog", "Hash", c => c.Long());
            AddColumn("Tracking.Event", "Hash", c => c.Long());
            AddColumn("Tracking.IPOrganization", "Hash", c => c.Long());
            AddColumn("Tracking.IPOrganizationStatus", "Hash", c => c.Long());
            AddColumn("Tracking.PageViewEvent", "Hash", c => c.Long());
            AddColumn("Tracking.PageView", "Hash", c => c.Long());
            AddColumn("Tracking.PageViewStatus", "Hash", c => c.Long());
            AddColumn("Tracking.PageViewType", "Hash", c => c.Long());
            AddColumn("Tracking.Visitor", "Hash", c => c.Long());
            AddColumn("Tracking.Visit", "Hash", c => c.Long());
            AddColumn("Tracking.VisitStatus", "Hash", c => c.Long());
            AddColumn("Tracking.EventStatus", "Hash", c => c.Long());
            AddColumn("Tracking.EventType", "Hash", c => c.Long());
            AddColumn("Favorites.FavoriteShipCarrier", "Hash", c => c.Long());
            AddColumn("Notifications.Action", "Hash", c => c.Long());
            AddColumn("Pricing.PriceRuleAccount", "Hash", c => c.Long());
            AddColumn("Pricing.PriceRule", "Hash", c => c.Long());
            AddColumn("Pricing.PriceRuleAccountType", "Hash", c => c.Long());
            AddColumn("Pricing.PriceRuleCategory", "Hash", c => c.Long());
            AddColumn("Pricing.PriceRuleProduct", "Hash", c => c.Long());
            AddColumn("Pricing.PriceRuleProductType", "Hash", c => c.Long());
            AddColumn("Pricing.PriceRuleVendor", "Hash", c => c.Long());
            AddColumn("Contacts.ProfanityFilter", "Hash", c => c.Long());
            AddColumn("Reporting.Reports", "Hash", c => c.Long());
            AddColumn("Reporting.ReportTypes", "Hash", c => c.Long());
            AddColumn("Hangfire.ScheduledJobConfiguration", "Hash", c => c.Long());
            AddColumn("Hangfire.ScheduledJobConfigurationSetting", "Hash", c => c.Long());
            AddColumn("System.Setting", "Hash", c => c.Long());
            AddColumn("System.SettingGroup", "Hash", c => c.Long());
            AddColumn("System.SettingType", "Hash", c => c.Long());
            AddColumn("Globalization.UIKey", "Hash", c => c.Long());
            AddColumn("Globalization.UITranslation", "Hash", c => c.Long());
            AddColumn("Shipping.UPSEndOfDay", "Hash", c => c.Long());
            AddColumn("Geography.ZipCode", "Hash", c => c.Long());
            CreateIndex("Accounts.AccountAttribute", "Hash");
            CreateIndex("Attributes.AttributeValue", "Hash");
            CreateIndex("Attributes.GeneralAttribute", "Hash");
            CreateIndex("Attributes.AttributeType", "Hash");
            CreateIndex("Accounts.Account", "Hash");
            CreateIndex("Accounts.AccountContact", "Hash");
            CreateIndex("Contacts.Contact", "Hash");
            CreateIndex("Geography.Address", "Hash");
            CreateIndex("Geography.Country", "Hash");
            CreateIndex("Geography.CountryCurrency", "Hash");
            CreateIndex("Currencies.Currency", "Hash");
            CreateIndex("Currencies.HistoricalCurrencyRate", "Hash");
            CreateIndex("Media.Library", "Hash");
            CreateIndex("Media.Audio", "Hash");
            CreateIndex("Media.File", "Hash");
            CreateIndex("Media.LibraryType", "Hash");
            CreateIndex("Geography.CountryLanguage", "Hash");
            CreateIndex("Globalization.Language", "Hash");
            CreateIndex("Geography.Region", "Hash");
            CreateIndex("Geography.RegionCurrency", "Hash");
            CreateIndex("Geography.District", "Hash");
            CreateIndex("Geography.DistrictCurrency", "Hash");
            CreateIndex("Geography.DistrictLanguage", "Hash");
            CreateIndex("Tax.TaxDistrict", "Hash");
            CreateIndex("Geography.InterRegion", "Hash");
            CreateIndex("Geography.RegionLanguage", "Hash");
            CreateIndex("Tax.TaxRegion", "Hash");
            CreateIndex("Tax.TaxCountry", "Hash");
            CreateIndex("Purchasing.PurchaseOrder", "Hash");
            CreateIndex("Purchasing.SalesOrderPurchaseOrder", "Hash");
            CreateIndex("Ordering.SalesOrder", "Hash");
            CreateIndex("Invoicing.SalesOrderSalesInvoice", "Hash");
            CreateIndex("Invoicing.SalesInvoice", "Hash");
            CreateIndex("Invoicing.SalesInvoiceAttribute", "Hash");
            CreateIndex("Invoicing.SalesInvoiceContact", "Hash");
            CreateIndex("Invoicing.SalesInvoiceContact", "SalesInvoice_ID");
            CreateIndex("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1");
            CreateIndex("Discounts.SalesInvoiceDiscounts", "Hash");
            CreateIndex("Discounts.Discount", "Hash");
            CreateIndex("Discounts.DiscountCategories", "Hash");
            CreateIndex("Categories.Category", "Hash");
            CreateIndex("Categories.CategoryAttribute", "Hash");
            CreateIndex("Categories.CategoryImage", "Hash");
            CreateIndex("Categories.CategoryFile", "Hash");
            CreateIndex("Products.ProductCategory", "Hash");
            CreateIndex("Products.ProductCategoryAttribute", "Hash");
            CreateIndex("Products.Product", "Hash");
            CreateIndex("Products.ProductAttribute", "Hash");
            CreateIndex("Shopping.CartItem", "Hash");
            CreateIndex("Shopping.CartItemAttribute", "Hash");
            CreateIndex("Discounts.CartItemDiscounts", "Hash");
            CreateIndex("Shopping.Cart", "Hash");
            CreateIndex("Shopping.CartAttribute", "Hash");
            CreateIndex("Shopping.CartContact", "Hash");
            CreateIndex("Shopping.CartContact", "Cart_ID");
            CreateIndex("Shopping.CartContact", "Cart_ID1");
            CreateIndex("Discounts.CartDiscounts", "Hash");
            CreateIndex("Shopping.CartFile", "Hash");
            CreateIndex("System.Note", "Hash");
            CreateIndex("Contacts.User", "Hash");
            CreateIndex("Contacts.UserAttribute", "Hash");
            CreateIndex("Favorites.FavoriteCategory", "Hash");
            CreateIndex("Favorites.FavoriteManufacturer", "Hash");
            CreateIndex("Manufacturers.Manufacturer", "Hash");
            CreateIndex("Manufacturers.ManufacturerProduct", "Hash");
            CreateIndex("Reviews.Review", "Hash");
            CreateIndex("Stores.BrandStore", "Hash");
            CreateIndex("Stores.Brand", "Hash");
            CreateIndex("Stores.BrandSiteDomain", "Hash");
            CreateIndex("Stores.SiteDomain", "Hash");
            CreateIndex("Stores.SiteDomainSocialProvider", "Hash");
            CreateIndex("Stores.SocialProvider", "Hash");
            CreateIndex("Stores.StoreSiteDomain", "Hash");
            CreateIndex("Stores.StoreAccount", "Hash");
            CreateIndex("Pricing.PricePoint", "Hash");
            CreateIndex("Stores.StoreCategory", "Hash");
            CreateIndex("Stores.StoreCategoryType", "Hash");
            CreateIndex("Categories.CategoryType", "Hash");
            CreateIndex("Stores.StoreContact", "Hash");
            CreateIndex("Stores.StoreImage", "Hash");
            CreateIndex("Stores.StoreInventoryLocation", "Hash");
            CreateIndex("Inventory.InventoryLocation", "Hash");
            CreateIndex("Inventory.InventoryLocationSection", "Hash");
            CreateIndex("Products.ProductInventoryLocationSection", "Hash");
            CreateIndex("Shipping.Shipment", "Hash");
            CreateIndex("Ordering.SalesOrderItemShipment", "Hash");
            CreateIndex("Ordering.SalesOrderItem", "Hash");
            CreateIndex("Ordering.SalesOrderItemAttribute", "Hash");
            CreateIndex("Discounts.SalesOrderItemDiscounts", "Hash");
            CreateIndex("Ordering.SalesOrderItemStatus", "Hash");
            CreateIndex("Stores.StoreProduct", "Hash");
            CreateIndex("Vendors.VendorProduct", "Hash");
            CreateIndex("Vendors.Vendor", "Hash");
            CreateIndex("Contacts.ContactMethod", "Hash");
            CreateIndex("Vendors.ShipVia", "Hash");
            CreateIndex("Shipping.ShipCarrier", "Hash");
            CreateIndex("Shipping.CarrierInvoice", "Hash");
            CreateIndex("Shipping.CarrierOrigin", "Hash");
            CreateIndex("Shipping.ShipCarrierMethod", "Hash");
            CreateIndex("Stores.StoreVendor", "Hash");
            CreateIndex("Vendors.Term", "Hash");
            CreateIndex("Vendors.VendorTerm", "Hash");
            CreateIndex("Vendors.VendorManufacturer", "Hash");
            CreateIndex("Shipping.ShipmentEvent", "Hash");
            CreateIndex("Shipping.ShipmentStatus", "Hash");
            CreateIndex("Shipping.ShipmentType", "Hash");
            CreateIndex("Stores.StoreManufacturer", "Hash");
            CreateIndex("Stores.StoreSubscription", "Hash");
            CreateIndex("Payments.Subscription", "Hash");
            CreateIndex("Payments.Payment", "Hash");
            CreateIndex("Payments.PaymentMethod", "Hash");
            CreateIndex("Payments.PaymentStatus", "Hash");
            CreateIndex("Payments.SubscriptionHistory", "Hash");
            CreateIndex("Payments.PaymentType", "Hash");
            CreateIndex("Payments.RepeatType", "Hash");
            CreateIndex("Payments.SubscriptionStatus", "Hash");
            CreateIndex("Payments.SubscriptionType", "Hash");
            CreateIndex("Products.ProductSubscriptionType", "Hash");
            CreateIndex("Stores.StoreUser", "Hash");
            CreateIndex("Stores.StoreUserType", "Hash");
            CreateIndex("Contacts.UserType", "Hash");
            CreateIndex("Stores.StoreType", "Hash");
            CreateIndex("Reviews.ReviewType", "Hash");
            CreateIndex("Favorites.FavoriteStore", "Hash");
            CreateIndex("Favorites.FavoriteVendor", "Hash");
            CreateIndex("Messaging.MessageAttachment", "Hash");
            CreateIndex("Messaging.MessageAttachment", "StoredFileID");
            CreateIndex("Messaging.Message", "Hash");
            CreateIndex("Messaging.Conversation", "Hash");
            CreateIndex("Messaging.MessageRecipient", "Hash");
            CreateIndex("Messaging.EmailQueue", "Hash");
            CreateIndex("Messaging.EmailTemplate", "Hash");
            CreateIndex("Messaging.EmailStatus", "Hash");
            CreateIndex("Messaging.EmailType", "Hash");
            CreateIndex("Groups.Group", "Hash");
            CreateIndex("Groups.GroupUser", "Hash");
            CreateIndex("Groups.GroupStatus", "Hash");
            CreateIndex("Groups.GroupType", "Hash");
            CreateIndex("Notifications.NotificationMessage", "Hash");
            CreateIndex("Notifications.Notification", "Hash");
            CreateIndex("Quoting.SalesQuote", "Hash");
            CreateIndex("Quoting.SalesQuoteSalesOrder", "Hash");
            CreateIndex("Quoting.SalesQuoteAttribute", "Hash");
            CreateIndex("Quoting.SalesQuoteContact", "Hash");
            CreateIndex("Quoting.SalesQuoteContact", "SalesQuote_ID");
            CreateIndex("Quoting.SalesQuoteContact", "SalesQuote_ID1");
            CreateIndex("Discounts.SalesQuoteDiscounts", "Hash");
            CreateIndex("Quoting.SalesQuoteFile", "Hash");
            CreateIndex("Quoting.SalesQuoteItem", "Hash");
            CreateIndex("Quoting.SalesQuoteItemAttribute", "Hash");
            CreateIndex("Discounts.SalesQuoteItemDiscounts", "Hash");
            CreateIndex("Quoting.SalesQuoteItemShipment", "Hash");
            CreateIndex("Quoting.SalesQuoteItemStatus", "Hash");
            CreateIndex("Shipping.ShipOption", "Hash");
            CreateIndex("Quoting.SalesQuoteState", "Hash");
            CreateIndex("Quoting.SalesQuoteStatus", "Hash");
            CreateIndex("Quoting.SalesQuoteType", "Hash");
            CreateIndex("Contacts.UserStatus", "Hash");
            CreateIndex("CalendarEvents.UserEventAttendance", "Hash");
            CreateIndex("CalendarEvents.CalendarEvent", "Hash");
            CreateIndex("CalendarEvents.CalendarEventDetail", "Hash");
            CreateIndex("CalendarEvents.CalendarEventFile", "Hash");
            CreateIndex("CalendarEvents.CalendarEventProducts", "Hash");
            CreateIndex("CalendarEvents.CalendarEventStatus", "Hash");
            CreateIndex("CalendarEvents.CalendarEventType", "Hash");
            CreateIndex("CalendarEvents.UserEventAttendanceType", "Hash");
            CreateIndex("Payments.Wallet", "Hash");
            CreateIndex("Sampling.SampleRequest", "Hash");
            CreateIndex("Sampling.SampleRequestAttribute", "Hash");
            CreateIndex("Sampling.SampleRequestContact", "Hash");
            CreateIndex("Sampling.SampleRequestContact", "SampleRequest_ID");
            CreateIndex("Sampling.SampleRequestContact", "SampleRequest_ID1");
            CreateIndex("Discounts.SampleRequestDiscounts", "Hash");
            CreateIndex("Sampling.SampleRequestFile", "Hash");
            CreateIndex("Sampling.SampleRequestItem", "Hash");
            CreateIndex("Sampling.SampleRequestItemAttribute", "Hash");
            CreateIndex("Discounts.SampleRequestItemDiscounts", "Hash");
            CreateIndex("Sampling.SampleRequestItemShipment", "Hash");
            CreateIndex("Sampling.SampleRequestItemStatus", "Hash");
            CreateIndex("Sampling.SampleRequestState", "Hash");
            CreateIndex("Sampling.SampleRequestStatus", "Hash");
            CreateIndex("Sampling.SampleRequestType", "Hash");
            CreateIndex("System.NoteType", "Hash");
            CreateIndex("Shopping.CartState", "Hash");
            CreateIndex("Shopping.CartStatus", "Hash");
            CreateIndex("Shopping.CartType", "Hash");
            CreateIndex("Shopping.CartItemShipment", "Hash");
            CreateIndex("Shopping.CartItemStatus", "Hash");
            CreateIndex("Discounts.DiscountProducts", "Hash");
            CreateIndex("Products.ProductImageType", "Hash");
            CreateIndex("Shipping.Package", "Hash");
            CreateIndex("Shipping.PackageType", "Hash");
            CreateIndex("Products.ProductAssociation", "Hash");
            CreateIndex("Products.ProductAssociationAttribute", "Hash");
            CreateIndex("Products.ProductAssociationType", "Hash");
            CreateIndex("Products.ProductFile", "Hash");
            CreateIndex("Products.ProductImage", "Hash");
            CreateIndex("Products.ProductPricePoint", "Hash");
            CreateIndex("Pricing.PriceRounding", "Hash");
            CreateIndex("Products.ProductType", "Hash");
            CreateIndex("Discounts.DiscountCode", "Hash");
            CreateIndex("Discounts.DiscountProductType", "Hash");
            CreateIndex("Discounts.DiscountStores", "Hash");
            CreateIndex("Invoicing.SalesInvoiceFile", "Hash");
            CreateIndex("Payments.SalesInvoicePayment", "Hash");
            CreateIndex("Invoicing.SalesInvoiceItem", "Hash");
            CreateIndex("Invoicing.SalesInvoiceItemAttribute", "Hash");
            CreateIndex("Discounts.SalesInvoiceItemDiscounts", "Hash");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "Hash");
            CreateIndex("Invoicing.SalesInvoiceItemStatus", "Hash");
            CreateIndex("Invoicing.SalesInvoiceState", "Hash");
            CreateIndex("Invoicing.SalesInvoiceStatus", "Hash");
            CreateIndex("Invoicing.SalesInvoiceType", "Hash");
            CreateIndex("Ordering.SalesOrderAttribute", "Hash");
            CreateIndex("Ordering.SalesOrderContact", "Hash");
            CreateIndex("Ordering.SalesOrderContact", "SalesOrder_ID");
            CreateIndex("Ordering.SalesOrderContact", "SalesOrder_ID1");
            CreateIndex("Contacts.CustomerPriority", "Hash");
            CreateIndex("Discounts.SalesOrderDiscounts", "Hash");
            CreateIndex("Ordering.SalesOrderFile", "Hash");
            CreateIndex("Payments.SalesOrderPayment", "Hash");
            CreateIndex("Ordering.SalesOrderState", "Hash");
            CreateIndex("Ordering.SalesOrderStatus", "Hash");
            CreateIndex("Ordering.SalesOrderType", "Hash");
            CreateIndex("Purchasing.PurchaseOrderAttribute", "Hash");
            CreateIndex("Purchasing.PurchaseOrderContact", "Hash");
            CreateIndex("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID");
            CreateIndex("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1");
            CreateIndex("Discounts.PurchaseOrderDiscounts", "Hash");
            CreateIndex("Purchasing.PurchaseOrderFile", "Hash");
            CreateIndex("Purchasing.FreeOnBoard", "Hash");
            CreateIndex("Purchasing.PurchaseOrderItem", "Hash");
            CreateIndex("Purchasing.PurchaseOrderItemAttribute", "Hash");
            CreateIndex("Discounts.PurchaseOrderItemDiscounts", "Hash");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "Hash");
            CreateIndex("Purchasing.PurchaseOrderItemStatus", "Hash");
            CreateIndex("Purchasing.PurchaseOrderState", "Hash");
            CreateIndex("Purchasing.PurchaseOrderStatus", "Hash");
            CreateIndex("Purchasing.PurchaseOrderType", "Hash");
            CreateIndex("Contacts.ContactType", "Hash");
            CreateIndex("Accounts.AccountPricePoint", "Hash");
            CreateIndex("Accounts.AccountTerm", "Hash");
            CreateIndex("Contacts.Opportunities", "Hash");
            CreateIndex("Accounts.AccountStatus", "Hash");
            CreateIndex("Accounts.AccountType", "Hash");
            CreateIndex("Advertising.AdAccount", "Hash");
            CreateIndex("Advertising.Ad", "Hash");
            CreateIndex("Advertising.AdImage", "Hash");
            CreateIndex("Advertising.AdStore", "Hash");
            CreateIndex("Advertising.AdZone", "Hash");
            CreateIndex("Advertising.AdZoneAccess", "Hash");
            CreateIndex("Counters.Counter", "Hash");
            CreateIndex("Counters.CounterLog", "Hash");
            CreateIndex("Counters.CounterLogType", "Hash");
            CreateIndex("Counters.CounterType", "Hash");
            CreateIndex("Advertising.Zone", "Hash");
            CreateIndex("Advertising.ZoneStatus", "Hash");
            CreateIndex("Advertising.ZoneType", "Hash");
            CreateIndex("Tracking.CampaignAd", "Hash");
            CreateIndex("Tracking.Campaign", "Hash");
            CreateIndex("Tracking.CampaignStatus", "Hash");
            CreateIndex("Tracking.CampaignType", "Hash");
            CreateIndex("Advertising.AdStatus", "Hash");
            CreateIndex("Advertising.AdType", "Hash");
            CreateIndex("System.SystemLog", "Hash");
            CreateIndex("Tracking.Event", "Hash");
            CreateIndex("Tracking.IPOrganization", "Hash");
            CreateIndex("Tracking.IPOrganizationStatus", "Hash");
            CreateIndex("Tracking.PageViewEvent", "Hash");
            CreateIndex("Tracking.PageView", "Hash");
            CreateIndex("Tracking.PageViewStatus", "Hash");
            CreateIndex("Tracking.PageViewType", "Hash");
            CreateIndex("Tracking.Visitor", "Hash");
            CreateIndex("Tracking.Visit", "Hash");
            CreateIndex("Tracking.VisitStatus", "Hash");
            CreateIndex("Tracking.EventStatus", "Hash");
            CreateIndex("Tracking.EventType", "Hash");
            CreateIndex("Favorites.FavoriteShipCarrier", "Hash");
            CreateIndex("Notifications.Action", "Hash");
            CreateIndex("Pricing.PriceRuleAccount", "Hash");
            CreateIndex("Pricing.PriceRule", "Hash");
            CreateIndex("Pricing.PriceRuleAccountType", "Hash");
            CreateIndex("Pricing.PriceRuleCategory", "Hash");
            CreateIndex("Pricing.PriceRuleProduct", "Hash");
            CreateIndex("Pricing.PriceRuleProductType", "Hash");
            CreateIndex("Pricing.PriceRuleVendor", "Hash");
            CreateIndex("Contacts.ProfanityFilter", "Hash");
            CreateIndex("Reporting.Reports", "Hash");
            CreateIndex("Reporting.ReportTypes", "Hash");
            CreateIndex("Hangfire.ScheduledJobConfiguration", "Hash");
            CreateIndex("Hangfire.ScheduledJobConfigurationSetting", "Hash");
            CreateIndex("System.Setting", "Hash");
            CreateIndex("System.SettingGroup", "Hash");
            CreateIndex("System.SettingType", "Hash");
            CreateIndex("Globalization.UIKey", "Hash");
            CreateIndex("Globalization.UITranslation", "Hash");
            CreateIndex("Shipping.UPSEndOfDay", "Hash");
            CreateIndex("Geography.ZipCode", "Hash");
            AddForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoice_ID", "Invoicing.SalesInvoice", "ID");
            AddForeignKey("Shopping.CartContact", "Cart_ID1", "Shopping.Cart", "ID");
            AddForeignKey("Messaging.MessageAttachment", "StoredFileID", "Media.StoredFile", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteContact", "SalesQuote_ID", "Quoting.SalesQuote", "ID");
            AddForeignKey("Sampling.SampleRequestContact", "SampleRequest_ID", "Sampling.SampleRequest", "ID");
            AddForeignKey("Ordering.SalesOrderContact", "SalesOrder_ID", "Ordering.SalesOrder", "ID");
            AddForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID", "Purchasing.PurchaseOrder", "ID");
            AddForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1", "Purchasing.PurchaseOrder", "ID");
            AddForeignKey("Ordering.SalesOrderContact", "SalesOrder_ID1", "Ordering.SalesOrder", "ID");
            AddForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1", "Invoicing.SalesInvoice", "ID");
            AddForeignKey("Shopping.CartContact", "Cart_ID", "Shopping.Cart", "ID");
            AddForeignKey("Quoting.SalesQuoteContact", "SalesQuote_ID1", "Quoting.SalesQuote", "ID");
            AddForeignKey("Sampling.SampleRequestContact", "SampleRequest_ID1", "Sampling.SampleRequest", "ID");
            DropColumn("Categories.Category", "CategoryHash");
            DropColumn("Products.Product", "ProductHash");
        }

        public override void Down()
        {
            AddColumn("Products.Product", "ProductHash", c => c.Long());
            AddColumn("Categories.Category", "CategoryHash", c => c.Long());
            DropForeignKey("Sampling.SampleRequestContact", "SampleRequest_ID1", "Sampling.SampleRequest");
            DropForeignKey("Quoting.SalesQuoteContact", "SalesQuote_ID1", "Quoting.SalesQuote");
            DropForeignKey("Shopping.CartContact", "Cart_ID", "Shopping.Cart");
            DropForeignKey("Categories.CategoryImageNew", "MasterID", "Categories.Category");
            DropForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1", "Invoicing.SalesInvoice");
            DropForeignKey("Ordering.SalesOrderContact", "SalesOrder_ID1", "Ordering.SalesOrder");
            DropForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1", "Purchasing.PurchaseOrder");
            DropForeignKey("Advertising.AdImageNew", "TypeID", "Advertising.AdImageType");
            DropForeignKey("Advertising.AdImageNew", "MasterID", "Advertising.Ad");
            DropForeignKey("Accounts.AccountImage", "TypeID", "Accounts.AccountImageType");
            DropForeignKey("Accounts.AccountImage", "MasterID", "Accounts.Account");
            DropForeignKey("Contacts.ContactImage", "TypeID", "Contacts.ContactImageType");
            DropForeignKey("Contacts.ContactImage", "MasterID", "Contacts.Contact");
            DropForeignKey("Purchasing.PurchaseOrderFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("Purchasing.PurchaseOrderFileNew", "MasterID", "Purchasing.PurchaseOrder");
            DropForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID", "Purchasing.PurchaseOrder");
            DropForeignKey("Ordering.SalesOrderFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("Ordering.SalesOrderFileNew", "MasterID", "Ordering.SalesOrder");
            DropForeignKey("Ordering.SalesOrderContact", "SalesOrder_ID", "Ordering.SalesOrder");
            DropForeignKey("Invoicing.SalesInvoiceFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("Invoicing.SalesInvoiceFileNew", "MasterID", "Invoicing.SalesInvoice");
            DropForeignKey("Categories.CategoryFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("Categories.CategoryFileNew", "MasterID", "Categories.Category");
            DropForeignKey("Products.ProductFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("Products.ProductFileNew", "MasterID", "Products.Product");
            DropForeignKey("Products.ProductImageNew", "TypeID", "Products.ProductImageType");
            DropForeignKey("Products.ProductImageNew", "MasterID", "Products.Product");
            DropForeignKey("Shopping.CartFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("Shopping.CartFileNew", "MasterID", "Shopping.Cart");
            DropForeignKey("Sampling.SampleRequestFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("Sampling.SampleRequestFileNew", "MasterID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequestContact", "SampleRequest_ID", "Sampling.SampleRequest");
            DropForeignKey("CalendarEvents.CalendarEventFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("CalendarEvents.CalendarEventFileNew", "MasterID", "CalendarEvents.CalendarEvent");
            DropForeignKey("Quoting.SalesQuoteFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("Quoting.SalesQuoteFileNew", "MasterID", "Quoting.SalesQuote");
            DropForeignKey("Quoting.SalesQuoteContact", "SalesQuote_ID", "Quoting.SalesQuote");
            DropForeignKey("Messaging.MessageAttachment", "StoredFileID", "Media.StoredFile");
            DropForeignKey("Contacts.UserImage", "TypeID", "Contacts.UserImageType");
            DropForeignKey("Contacts.UserImage", "MasterID", "Contacts.User");
            DropForeignKey("Vendors.VendorImage", "TypeID", "Vendors.VendorImageType");
            DropForeignKey("Vendors.VendorImage", "MasterID", "Vendors.Vendor");
            DropForeignKey("Stores.StoreImageNew", "TypeID", "Stores.StoreImageType");
            DropForeignKey("Stores.StoreImageNew", "MasterID", "Stores.Store");
            DropForeignKey("Stores.BrandImage", "TypeID", "Stores.BrandImageType");
            DropForeignKey("Stores.BrandImage", "MasterID", "Stores.Brand");
            DropForeignKey("Manufacturers.ManufacturerImage", "TypeID", "Manufacturers.ManufacturerImageType");
            DropForeignKey("Manufacturers.ManufacturerImage", "MasterID", "Manufacturers.Manufacturer");
            DropForeignKey("Shopping.CartContact", "Cart_ID1", "Shopping.Cart");
            DropForeignKey("Categories.CategoryImageNew", "TypeID", "Categories.CategoryImageType");
            DropForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoice_ID", "Invoicing.SalesInvoice");
            DropForeignKey("Geography.RegionImage", "TypeID", "Geography.RegionImageType");
            DropForeignKey("Geography.RegionImage", "MasterID", "Geography.Region");
            DropForeignKey("Geography.DistrictImage", "TypeID", "Geography.DistrictImageType");
            DropForeignKey("Geography.DistrictImage", "MasterID", "Geography.District");
            DropForeignKey("Globalization.LanguageImage", "TypeID", "Globalization.LanguageImageType");
            DropForeignKey("Globalization.LanguageImage", "MasterID", "Globalization.Language");
            DropForeignKey("Geography.CountryImage", "TypeID", "Geography.CountryImageType");
            DropForeignKey("Geography.CountryImage", "MasterID", "Geography.Country");
            DropForeignKey("Currencies.CurrencyImage", "TypeID", "Currencies.CurrencyImageType");
            DropForeignKey("Currencies.CurrencyImage", "MasterID", "Currencies.Currency");
            DropIndex("Geography.ZipCode", new[] { "Hash" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "Hash" });
            DropIndex("Globalization.UITranslation", new[] { "Hash" });
            DropIndex("Globalization.UIKey", new[] { "Hash" });
            DropIndex("System.SettingType", new[] { "Hash" });
            DropIndex("System.SettingGroup", new[] { "Hash" });
            DropIndex("System.Setting", new[] { "Hash" });
            DropIndex("Hangfire.ScheduledJobConfigurationSetting", new[] { "Hash" });
            DropIndex("Hangfire.ScheduledJobConfiguration", new[] { "Hash" });
            DropIndex("Reporting.ReportTypes", new[] { "Hash" });
            DropIndex("Reporting.Reports", new[] { "Hash" });
            DropIndex("Contacts.ProfanityFilter", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleVendor", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleProductType", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleProduct", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleCategory", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleAccountType", new[] { "Hash" });
            DropIndex("Pricing.PriceRule", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleAccount", new[] { "Hash" });
            DropIndex("Notifications.Action", new[] { "Hash" });
            DropIndex("Favorites.FavoriteShipCarrier", new[] { "Hash" });
            DropIndex("Tracking.EventType", new[] { "Hash" });
            DropIndex("Tracking.EventStatus", new[] { "Hash" });
            DropIndex("Tracking.VisitStatus", new[] { "Hash" });
            DropIndex("Tracking.Visit", new[] { "Hash" });
            DropIndex("Tracking.Visitor", new[] { "Hash" });
            DropIndex("Tracking.PageViewType", new[] { "Hash" });
            DropIndex("Tracking.PageViewStatus", new[] { "Hash" });
            DropIndex("Tracking.PageView", new[] { "Hash" });
            DropIndex("Tracking.PageViewEvent", new[] { "Hash" });
            DropIndex("Tracking.IPOrganizationStatus", new[] { "Hash" });
            DropIndex("Tracking.IPOrganization", new[] { "Hash" });
            DropIndex("Tracking.Event", new[] { "Hash" });
            DropIndex("System.SystemLog", new[] { "Hash" });
            DropIndex("Advertising.AdType", new[] { "Hash" });
            DropIndex("Advertising.AdStatus", new[] { "Hash" });
            DropIndex("Advertising.AdImageType", new[] { "SortOrder" });
            DropIndex("Advertising.AdImageType", new[] { "DisplayName" });
            DropIndex("Advertising.AdImageType", new[] { "Name" });
            DropIndex("Advertising.AdImageType", new[] { "Hash" });
            DropIndex("Advertising.AdImageType", new[] { "Active" });
            DropIndex("Advertising.AdImageType", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdImageType", new[] { "CustomKey" });
            DropIndex("Advertising.AdImageType", new[] { "ID" });
            DropIndex("Advertising.AdImageNew", new[] { "TypeID" });
            DropIndex("Advertising.AdImageNew", new[] { "MasterID" });
            DropIndex("Advertising.AdImageNew", new[] { "Name" });
            DropIndex("Advertising.AdImageNew", new[] { "Hash" });
            DropIndex("Advertising.AdImageNew", new[] { "Active" });
            DropIndex("Advertising.AdImageNew", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdImageNew", new[] { "CustomKey" });
            DropIndex("Advertising.AdImageNew", new[] { "ID" });
            DropIndex("Tracking.CampaignType", new[] { "Hash" });
            DropIndex("Tracking.CampaignStatus", new[] { "Hash" });
            DropIndex("Tracking.Campaign", new[] { "Hash" });
            DropIndex("Tracking.CampaignAd", new[] { "Hash" });
            DropIndex("Advertising.ZoneType", new[] { "Hash" });
            DropIndex("Advertising.ZoneStatus", new[] { "Hash" });
            DropIndex("Advertising.Zone", new[] { "Hash" });
            DropIndex("Counters.CounterType", new[] { "Hash" });
            DropIndex("Counters.CounterLogType", new[] { "Hash" });
            DropIndex("Counters.CounterLog", new[] { "Hash" });
            DropIndex("Counters.Counter", new[] { "Hash" });
            DropIndex("Advertising.AdZoneAccess", new[] { "Hash" });
            DropIndex("Advertising.AdZone", new[] { "Hash" });
            DropIndex("Advertising.AdStore", new[] { "Hash" });
            DropIndex("Advertising.AdImage", new[] { "Hash" });
            DropIndex("Advertising.Ad", new[] { "Hash" });
            DropIndex("Advertising.AdAccount", new[] { "Hash" });
            DropIndex("Accounts.AccountType", new[] { "Hash" });
            DropIndex("Accounts.AccountStatus", new[] { "Hash" });
            DropIndex("Contacts.Opportunities", new[] { "Hash" });
            DropIndex("Accounts.AccountImageType", new[] { "SortOrder" });
            DropIndex("Accounts.AccountImageType", new[] { "DisplayName" });
            DropIndex("Accounts.AccountImageType", new[] { "Name" });
            DropIndex("Accounts.AccountImageType", new[] { "Hash" });
            DropIndex("Accounts.AccountImageType", new[] { "Active" });
            DropIndex("Accounts.AccountImageType", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountImageType", new[] { "CustomKey" });
            DropIndex("Accounts.AccountImageType", new[] { "ID" });
            DropIndex("Accounts.AccountImage", new[] { "TypeID" });
            DropIndex("Accounts.AccountImage", new[] { "MasterID" });
            DropIndex("Accounts.AccountImage", new[] { "Name" });
            DropIndex("Accounts.AccountImage", new[] { "Hash" });
            DropIndex("Accounts.AccountImage", new[] { "Active" });
            DropIndex("Accounts.AccountImage", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountImage", new[] { "CustomKey" });
            DropIndex("Accounts.AccountImage", new[] { "ID" });
            DropIndex("Accounts.AccountTerm", new[] { "Hash" });
            DropIndex("Accounts.AccountPricePoint", new[] { "Hash" });
            DropIndex("Contacts.ContactType", new[] { "Hash" });
            DropIndex("Contacts.ContactImageType", new[] { "SortOrder" });
            DropIndex("Contacts.ContactImageType", new[] { "DisplayName" });
            DropIndex("Contacts.ContactImageType", new[] { "Name" });
            DropIndex("Contacts.ContactImageType", new[] { "Hash" });
            DropIndex("Contacts.ContactImageType", new[] { "Active" });
            DropIndex("Contacts.ContactImageType", new[] { "UpdatedDate" });
            DropIndex("Contacts.ContactImageType", new[] { "CustomKey" });
            DropIndex("Contacts.ContactImageType", new[] { "ID" });
            DropIndex("Contacts.ContactImage", new[] { "TypeID" });
            DropIndex("Contacts.ContactImage", new[] { "MasterID" });
            DropIndex("Contacts.ContactImage", new[] { "Name" });
            DropIndex("Contacts.ContactImage", new[] { "Hash" });
            DropIndex("Contacts.ContactImage", new[] { "Active" });
            DropIndex("Contacts.ContactImage", new[] { "UpdatedDate" });
            DropIndex("Contacts.ContactImage", new[] { "CustomKey" });
            DropIndex("Contacts.ContactImage", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderType", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderFileNew", new[] { "SlaveID" });
            DropIndex("Purchasing.PurchaseOrderFileNew", new[] { "MasterID" });
            DropIndex("Purchasing.PurchaseOrderFileNew", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderFileNew", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderFileNew", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderFileNew", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderFileNew", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderFileNew", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderStatus", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderState", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "Hash" });
            DropIndex("Discounts.PurchaseOrderItemDiscounts", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "Hash" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "Hash" });
            DropIndex("Discounts.PurchaseOrderDiscounts", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "PurchaseOrder_ID1" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "PurchaseOrder_ID" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderType", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderFileNew", new[] { "SlaveID" });
            DropIndex("Ordering.SalesOrderFileNew", new[] { "MasterID" });
            DropIndex("Ordering.SalesOrderFileNew", new[] { "Name" });
            DropIndex("Ordering.SalesOrderFileNew", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderFileNew", new[] { "Active" });
            DropIndex("Ordering.SalesOrderFileNew", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderFileNew", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderFileNew", new[] { "ID" });
            DropIndex("Ordering.SalesOrderStatus", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderState", new[] { "Hash" });
            DropIndex("Payments.SalesOrderPayment", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderFile", new[] { "Hash" });
            DropIndex("Discounts.SalesOrderDiscounts", new[] { "Hash" });
            DropIndex("Contacts.CustomerPriority", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderContact", new[] { "SalesOrder_ID1" });
            DropIndex("Ordering.SalesOrderContact", new[] { "SalesOrder_ID" });
            DropIndex("Ordering.SalesOrderContact", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceType", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceFileNew", new[] { "SlaveID" });
            DropIndex("Invoicing.SalesInvoiceFileNew", new[] { "MasterID" });
            DropIndex("Invoicing.SalesInvoiceFileNew", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceFileNew", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceFileNew", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceFileNew", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceFileNew", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceFileNew", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceStatus", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceState", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "Hash" });
            DropIndex("Discounts.SalesInvoiceItemDiscounts", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "Hash" });
            DropIndex("Payments.SalesInvoicePayment", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "Hash" });
            DropIndex("Discounts.DiscountStores", new[] { "Hash" });
            DropIndex("Discounts.DiscountProductType", new[] { "Hash" });
            DropIndex("Discounts.DiscountCode", new[] { "Hash" });
            DropIndex("Categories.CategoryFileNew", new[] { "SlaveID" });
            DropIndex("Categories.CategoryFileNew", new[] { "MasterID" });
            DropIndex("Categories.CategoryFileNew", new[] { "Name" });
            DropIndex("Categories.CategoryFileNew", new[] { "Hash" });
            DropIndex("Categories.CategoryFileNew", new[] { "Active" });
            DropIndex("Categories.CategoryFileNew", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryFileNew", new[] { "CustomKey" });
            DropIndex("Categories.CategoryFileNew", new[] { "ID" });
            DropIndex("Products.ProductType", new[] { "Hash" });
            DropIndex("Products.ProductFileNew", new[] { "SlaveID" });
            DropIndex("Products.ProductFileNew", new[] { "MasterID" });
            DropIndex("Products.ProductFileNew", new[] { "Name" });
            DropIndex("Products.ProductFileNew", new[] { "Hash" });
            DropIndex("Products.ProductFileNew", new[] { "Active" });
            DropIndex("Products.ProductFileNew", new[] { "UpdatedDate" });
            DropIndex("Products.ProductFileNew", new[] { "CustomKey" });
            DropIndex("Products.ProductFileNew", new[] { "ID" });
            DropIndex("Pricing.PriceRounding", new[] { "Hash" });
            DropIndex("Products.ProductPricePoint", new[] { "Hash" });
            DropIndex("Products.ProductImage", new[] { "Hash" });
            DropIndex("Products.ProductFile", new[] { "Hash" });
            DropIndex("Products.ProductAssociationType", new[] { "Hash" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "Hash" });
            DropIndex("Products.ProductAssociation", new[] { "Hash" });
            DropIndex("Shipping.PackageType", new[] { "Hash" });
            DropIndex("Shipping.Package", new[] { "Hash" });
            DropIndex("Products.ProductImageType", new[] { "Hash" });
            DropIndex("Products.ProductImageNew", new[] { "TypeID" });
            DropIndex("Products.ProductImageNew", new[] { "MasterID" });
            DropIndex("Products.ProductImageNew", new[] { "Name" });
            DropIndex("Products.ProductImageNew", new[] { "Hash" });
            DropIndex("Products.ProductImageNew", new[] { "Active" });
            DropIndex("Products.ProductImageNew", new[] { "UpdatedDate" });
            DropIndex("Products.ProductImageNew", new[] { "CustomKey" });
            DropIndex("Products.ProductImageNew", new[] { "ID" });
            DropIndex("Discounts.DiscountProducts", new[] { "Hash" });
            DropIndex("Shopping.CartItemStatus", new[] { "Hash" });
            DropIndex("Shopping.CartItemShipment", new[] { "Hash" });
            DropIndex("Shopping.CartType", new[] { "Hash" });
            DropIndex("Shopping.CartFileNew", new[] { "SlaveID" });
            DropIndex("Shopping.CartFileNew", new[] { "MasterID" });
            DropIndex("Shopping.CartFileNew", new[] { "Name" });
            DropIndex("Shopping.CartFileNew", new[] { "Hash" });
            DropIndex("Shopping.CartFileNew", new[] { "Active" });
            DropIndex("Shopping.CartFileNew", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartFileNew", new[] { "CustomKey" });
            DropIndex("Shopping.CartFileNew", new[] { "ID" });
            DropIndex("Shopping.CartStatus", new[] { "Hash" });
            DropIndex("Shopping.CartState", new[] { "Hash" });
            DropIndex("System.NoteType", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestType", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestFileNew", new[] { "SlaveID" });
            DropIndex("Sampling.SampleRequestFileNew", new[] { "MasterID" });
            DropIndex("Sampling.SampleRequestFileNew", new[] { "Name" });
            DropIndex("Sampling.SampleRequestFileNew", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestFileNew", new[] { "Active" });
            DropIndex("Sampling.SampleRequestFileNew", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestFileNew", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestFileNew", new[] { "ID" });
            DropIndex("Sampling.SampleRequestStatus", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestState", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "Hash" });
            DropIndex("Discounts.SampleRequestItemDiscounts", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestItem", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestFile", new[] { "Hash" });
            DropIndex("Discounts.SampleRequestDiscounts", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestContact", new[] { "SampleRequest_ID1" });
            DropIndex("Sampling.SampleRequestContact", new[] { "SampleRequest_ID" });
            DropIndex("Sampling.SampleRequestContact", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "Hash" });
            DropIndex("Sampling.SampleRequest", new[] { "Hash" });
            DropIndex("Payments.Wallet", new[] { "Hash" });
            DropIndex("CalendarEvents.UserEventAttendanceType", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEventType", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEventFileNew", new[] { "SlaveID" });
            DropIndex("CalendarEvents.CalendarEventFileNew", new[] { "MasterID" });
            DropIndex("CalendarEvents.CalendarEventFileNew", new[] { "Name" });
            DropIndex("CalendarEvents.CalendarEventFileNew", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEventFileNew", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEventFileNew", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEventFileNew", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEventFileNew", new[] { "ID" });
            DropIndex("CalendarEvents.CalendarEventStatus", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEventProducts", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEventDetail", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "Hash" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "Hash" });
            DropIndex("Contacts.UserStatus", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteType", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteFileNew", new[] { "SlaveID" });
            DropIndex("Quoting.SalesQuoteFileNew", new[] { "MasterID" });
            DropIndex("Quoting.SalesQuoteFileNew", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteFileNew", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteFileNew", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteFileNew", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteFileNew", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteFileNew", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteStatus", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteState", new[] { "Hash" });
            DropIndex("Shipping.ShipOption", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "Hash" });
            DropIndex("Discounts.SalesQuoteItemDiscounts", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "Hash" });
            DropIndex("Discounts.SalesQuoteDiscounts", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "SalesQuote_ID1" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "SalesQuote_ID" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteSalesOrder", new[] { "Hash" });
            DropIndex("Quoting.SalesQuote", new[] { "Hash" });
            DropIndex("Notifications.Notification", new[] { "Hash" });
            DropIndex("Notifications.NotificationMessage", new[] { "Hash" });
            DropIndex("Media.StoredFile", new[] { "Name" });
            DropIndex("Media.StoredFile", new[] { "Hash" });
            DropIndex("Media.StoredFile", new[] { "Active" });
            DropIndex("Media.StoredFile", new[] { "UpdatedDate" });
            DropIndex("Media.StoredFile", new[] { "CustomKey" });
            DropIndex("Media.StoredFile", new[] { "ID" });
            DropIndex("Groups.GroupType", new[] { "Hash" });
            DropIndex("Groups.GroupStatus", new[] { "Hash" });
            DropIndex("Groups.GroupUser", new[] { "Hash" });
            DropIndex("Groups.Group", new[] { "Hash" });
            DropIndex("Messaging.EmailType", new[] { "Hash" });
            DropIndex("Messaging.EmailStatus", new[] { "Hash" });
            DropIndex("Messaging.EmailTemplate", new[] { "Hash" });
            DropIndex("Messaging.EmailQueue", new[] { "Hash" });
            DropIndex("Messaging.MessageRecipient", new[] { "Hash" });
            DropIndex("Messaging.Conversation", new[] { "Hash" });
            DropIndex("Messaging.Message", new[] { "Hash" });
            DropIndex("Messaging.MessageAttachment", new[] { "StoredFileID" });
            DropIndex("Messaging.MessageAttachment", new[] { "Hash" });
            DropIndex("Contacts.UserImageType", new[] { "SortOrder" });
            DropIndex("Contacts.UserImageType", new[] { "DisplayName" });
            DropIndex("Contacts.UserImageType", new[] { "Name" });
            DropIndex("Contacts.UserImageType", new[] { "Hash" });
            DropIndex("Contacts.UserImageType", new[] { "Active" });
            DropIndex("Contacts.UserImageType", new[] { "UpdatedDate" });
            DropIndex("Contacts.UserImageType", new[] { "CustomKey" });
            DropIndex("Contacts.UserImageType", new[] { "ID" });
            DropIndex("Contacts.UserImage", new[] { "TypeID" });
            DropIndex("Contacts.UserImage", new[] { "MasterID" });
            DropIndex("Contacts.UserImage", new[] { "Name" });
            DropIndex("Contacts.UserImage", new[] { "Hash" });
            DropIndex("Contacts.UserImage", new[] { "Active" });
            DropIndex("Contacts.UserImage", new[] { "UpdatedDate" });
            DropIndex("Contacts.UserImage", new[] { "CustomKey" });
            DropIndex("Contacts.UserImage", new[] { "ID" });
            DropIndex("Favorites.FavoriteVendor", new[] { "Hash" });
            DropIndex("Favorites.FavoriteStore", new[] { "Hash" });
            DropIndex("Reviews.ReviewType", new[] { "Hash" });
            DropIndex("Stores.StoreType", new[] { "Hash" });
            DropIndex("Contacts.UserType", new[] { "Hash" });
            DropIndex("Stores.StoreUserType", new[] { "Hash" });
            DropIndex("Stores.StoreUser", new[] { "Hash" });
            DropIndex("Products.ProductSubscriptionType", new[] { "Hash" });
            DropIndex("Payments.SubscriptionType", new[] { "Hash" });
            DropIndex("Payments.SubscriptionStatus", new[] { "Hash" });
            DropIndex("Payments.RepeatType", new[] { "Hash" });
            DropIndex("Payments.PaymentType", new[] { "Hash" });
            DropIndex("Payments.SubscriptionHistory", new[] { "Hash" });
            DropIndex("Payments.PaymentStatus", new[] { "Hash" });
            DropIndex("Payments.PaymentMethod", new[] { "Hash" });
            DropIndex("Payments.Payment", new[] { "Hash" });
            DropIndex("Payments.Subscription", new[] { "Hash" });
            DropIndex("Stores.StoreSubscription", new[] { "Hash" });
            DropIndex("Stores.StoreManufacturer", new[] { "Hash" });
            DropIndex("Shipping.ShipmentType", new[] { "Hash" });
            DropIndex("Shipping.ShipmentStatus", new[] { "Hash" });
            DropIndex("Shipping.ShipmentEvent", new[] { "Hash" });
            DropIndex("Vendors.VendorManufacturer", new[] { "Hash" });
            DropIndex("Vendors.VendorTerm", new[] { "Hash" });
            DropIndex("Vendors.Term", new[] { "Hash" });
            DropIndex("Stores.StoreVendor", new[] { "Hash" });
            DropIndex("Shipping.ShipCarrierMethod", new[] { "Hash" });
            DropIndex("Shipping.CarrierOrigin", new[] { "Hash" });
            DropIndex("Shipping.CarrierInvoice", new[] { "Hash" });
            DropIndex("Shipping.ShipCarrier", new[] { "Hash" });
            DropIndex("Vendors.ShipVia", new[] { "Hash" });
            DropIndex("Vendors.VendorImageType", new[] { "SortOrder" });
            DropIndex("Vendors.VendorImageType", new[] { "DisplayName" });
            DropIndex("Vendors.VendorImageType", new[] { "Name" });
            DropIndex("Vendors.VendorImageType", new[] { "Hash" });
            DropIndex("Vendors.VendorImageType", new[] { "Active" });
            DropIndex("Vendors.VendorImageType", new[] { "UpdatedDate" });
            DropIndex("Vendors.VendorImageType", new[] { "CustomKey" });
            DropIndex("Vendors.VendorImageType", new[] { "ID" });
            DropIndex("Vendors.VendorImage", new[] { "TypeID" });
            DropIndex("Vendors.VendorImage", new[] { "MasterID" });
            DropIndex("Vendors.VendorImage", new[] { "Name" });
            DropIndex("Vendors.VendorImage", new[] { "Hash" });
            DropIndex("Vendors.VendorImage", new[] { "Active" });
            DropIndex("Vendors.VendorImage", new[] { "UpdatedDate" });
            DropIndex("Vendors.VendorImage", new[] { "CustomKey" });
            DropIndex("Vendors.VendorImage", new[] { "ID" });
            DropIndex("Contacts.ContactMethod", new[] { "Hash" });
            DropIndex("Vendors.Vendor", new[] { "Hash" });
            DropIndex("Vendors.VendorProduct", new[] { "Hash" });
            DropIndex("Stores.StoreProduct", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "Hash" });
            DropIndex("Discounts.SalesOrderItemDiscounts", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderItem", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "Hash" });
            DropIndex("Shipping.Shipment", new[] { "Hash" });
            DropIndex("Products.ProductInventoryLocationSection", new[] { "Hash" });
            DropIndex("Inventory.InventoryLocationSection", new[] { "Hash" });
            DropIndex("Inventory.InventoryLocation", new[] { "Hash" });
            DropIndex("Stores.StoreInventoryLocation", new[] { "Hash" });
            DropIndex("Stores.StoreImage", new[] { "Hash" });
            DropIndex("Stores.StoreContact", new[] { "Hash" });
            DropIndex("Categories.CategoryType", new[] { "Hash" });
            DropIndex("Stores.StoreCategoryType", new[] { "Hash" });
            DropIndex("Stores.StoreCategory", new[] { "Hash" });
            DropIndex("Pricing.PricePoint", new[] { "Hash" });
            DropIndex("Stores.StoreAccount", new[] { "Hash" });
            DropIndex("Stores.StoreImageType", new[] { "SortOrder" });
            DropIndex("Stores.StoreImageType", new[] { "DisplayName" });
            DropIndex("Stores.StoreImageType", new[] { "Name" });
            DropIndex("Stores.StoreImageType", new[] { "Hash" });
            DropIndex("Stores.StoreImageType", new[] { "Active" });
            DropIndex("Stores.StoreImageType", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreImageType", new[] { "CustomKey" });
            DropIndex("Stores.StoreImageType", new[] { "ID" });
            DropIndex("Stores.StoreImageNew", new[] { "TypeID" });
            DropIndex("Stores.StoreImageNew", new[] { "MasterID" });
            DropIndex("Stores.StoreImageNew", new[] { "Name" });
            DropIndex("Stores.StoreImageNew", new[] { "Hash" });
            DropIndex("Stores.StoreImageNew", new[] { "Active" });
            DropIndex("Stores.StoreImageNew", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreImageNew", new[] { "CustomKey" });
            DropIndex("Stores.StoreImageNew", new[] { "ID" });
            DropIndex("Stores.BrandImageType", new[] { "SortOrder" });
            DropIndex("Stores.BrandImageType", new[] { "DisplayName" });
            DropIndex("Stores.BrandImageType", new[] { "Name" });
            DropIndex("Stores.BrandImageType", new[] { "Hash" });
            DropIndex("Stores.BrandImageType", new[] { "Active" });
            DropIndex("Stores.BrandImageType", new[] { "UpdatedDate" });
            DropIndex("Stores.BrandImageType", new[] { "CustomKey" });
            DropIndex("Stores.BrandImageType", new[] { "ID" });
            DropIndex("Stores.BrandImage", new[] { "TypeID" });
            DropIndex("Stores.BrandImage", new[] { "MasterID" });
            DropIndex("Stores.BrandImage", new[] { "Name" });
            DropIndex("Stores.BrandImage", new[] { "Hash" });
            DropIndex("Stores.BrandImage", new[] { "Active" });
            DropIndex("Stores.BrandImage", new[] { "UpdatedDate" });
            DropIndex("Stores.BrandImage", new[] { "CustomKey" });
            DropIndex("Stores.BrandImage", new[] { "ID" });
            DropIndex("Stores.StoreSiteDomain", new[] { "Hash" });
            DropIndex("Stores.SocialProvider", new[] { "Hash" });
            DropIndex("Stores.SiteDomainSocialProvider", new[] { "Hash" });
            DropIndex("Stores.SiteDomain", new[] { "Hash" });
            DropIndex("Stores.BrandSiteDomain", new[] { "Hash" });
            DropIndex("Stores.Brand", new[] { "Hash" });
            DropIndex("Stores.BrandStore", new[] { "Hash" });
            DropIndex("Reviews.Review", new[] { "Hash" });
            DropIndex("Manufacturers.ManufacturerProduct", new[] { "Hash" });
            DropIndex("Manufacturers.ManufacturerImageType", new[] { "SortOrder" });
            DropIndex("Manufacturers.ManufacturerImageType", new[] { "DisplayName" });
            DropIndex("Manufacturers.ManufacturerImageType", new[] { "Name" });
            DropIndex("Manufacturers.ManufacturerImageType", new[] { "Hash" });
            DropIndex("Manufacturers.ManufacturerImageType", new[] { "Active" });
            DropIndex("Manufacturers.ManufacturerImageType", new[] { "UpdatedDate" });
            DropIndex("Manufacturers.ManufacturerImageType", new[] { "CustomKey" });
            DropIndex("Manufacturers.ManufacturerImageType", new[] { "ID" });
            DropIndex("Manufacturers.ManufacturerImage", new[] { "TypeID" });
            DropIndex("Manufacturers.ManufacturerImage", new[] { "MasterID" });
            DropIndex("Manufacturers.ManufacturerImage", new[] { "Name" });
            DropIndex("Manufacturers.ManufacturerImage", new[] { "Hash" });
            DropIndex("Manufacturers.ManufacturerImage", new[] { "Active" });
            DropIndex("Manufacturers.ManufacturerImage", new[] { "UpdatedDate" });
            DropIndex("Manufacturers.ManufacturerImage", new[] { "CustomKey" });
            DropIndex("Manufacturers.ManufacturerImage", new[] { "ID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "Hash" });
            DropIndex("Favorites.FavoriteManufacturer", new[] { "Hash" });
            DropIndex("Favorites.FavoriteCategory", new[] { "Hash" });
            DropIndex("Contacts.UserAttribute", new[] { "Hash" });
            DropIndex("Contacts.User", new[] { "Hash" });
            DropIndex("System.Note", new[] { "Hash" });
            DropIndex("Shopping.CartFile", new[] { "Hash" });
            DropIndex("Discounts.CartDiscounts", new[] { "Hash" });
            DropIndex("Shopping.CartContact", new[] { "Cart_ID1" });
            DropIndex("Shopping.CartContact", new[] { "Cart_ID" });
            DropIndex("Shopping.CartContact", new[] { "Hash" });
            DropIndex("Shopping.CartAttribute", new[] { "Hash" });
            DropIndex("Shopping.Cart", new[] { "Hash" });
            DropIndex("Discounts.CartItemDiscounts", new[] { "Hash" });
            DropIndex("Shopping.CartItemAttribute", new[] { "Hash" });
            DropIndex("Shopping.CartItem", new[] { "Hash" });
            DropIndex("Products.ProductAttribute", new[] { "Hash" });
            DropIndex("Products.Product", new[] { "Hash" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "Hash" });
            DropIndex("Products.ProductCategory", new[] { "Hash" });
            DropIndex("Categories.CategoryImageType", new[] { "SortOrder" });
            DropIndex("Categories.CategoryImageType", new[] { "DisplayName" });
            DropIndex("Categories.CategoryImageType", new[] { "Name" });
            DropIndex("Categories.CategoryImageType", new[] { "Hash" });
            DropIndex("Categories.CategoryImageType", new[] { "Active" });
            DropIndex("Categories.CategoryImageType", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryImageType", new[] { "CustomKey" });
            DropIndex("Categories.CategoryImageType", new[] { "ID" });
            DropIndex("Categories.CategoryImageNew", new[] { "TypeID" });
            DropIndex("Categories.CategoryImageNew", new[] { "MasterID" });
            DropIndex("Categories.CategoryImageNew", new[] { "Name" });
            DropIndex("Categories.CategoryImageNew", new[] { "Hash" });
            DropIndex("Categories.CategoryImageNew", new[] { "Active" });
            DropIndex("Categories.CategoryImageNew", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryImageNew", new[] { "CustomKey" });
            DropIndex("Categories.CategoryImageNew", new[] { "ID" });
            DropIndex("Categories.CategoryFile", new[] { "Hash" });
            DropIndex("Categories.CategoryImage", new[] { "Hash" });
            DropIndex("Categories.CategoryAttribute", new[] { "Hash" });
            DropIndex("Categories.Category", new[] { "Hash" });
            DropIndex("Discounts.DiscountCategories", new[] { "Hash" });
            DropIndex("Discounts.Discount", new[] { "Hash" });
            DropIndex("Discounts.SalesInvoiceDiscounts", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "SalesInvoice_ID1" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "SalesInvoice_ID" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoice", new[] { "Hash" });
            DropIndex("Invoicing.SalesOrderSalesInvoice", new[] { "Hash" });
            DropIndex("Ordering.SalesOrder", new[] { "Hash" });
            DropIndex("Purchasing.SalesOrderPurchaseOrder", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "Hash" });
            DropIndex("Tax.TaxCountry", new[] { "Hash" });
            DropIndex("Tax.TaxRegion", new[] { "Hash" });
            DropIndex("Geography.RegionLanguage", new[] { "Hash" });
            DropIndex("Geography.InterRegion", new[] { "Hash" });
            DropIndex("Geography.RegionImageType", new[] { "SortOrder" });
            DropIndex("Geography.RegionImageType", new[] { "DisplayName" });
            DropIndex("Geography.RegionImageType", new[] { "Name" });
            DropIndex("Geography.RegionImageType", new[] { "Hash" });
            DropIndex("Geography.RegionImageType", new[] { "Active" });
            DropIndex("Geography.RegionImageType", new[] { "UpdatedDate" });
            DropIndex("Geography.RegionImageType", new[] { "CustomKey" });
            DropIndex("Geography.RegionImageType", new[] { "ID" });
            DropIndex("Geography.RegionImage", new[] { "TypeID" });
            DropIndex("Geography.RegionImage", new[] { "MasterID" });
            DropIndex("Geography.RegionImage", new[] { "Name" });
            DropIndex("Geography.RegionImage", new[] { "Hash" });
            DropIndex("Geography.RegionImage", new[] { "Active" });
            DropIndex("Geography.RegionImage", new[] { "UpdatedDate" });
            DropIndex("Geography.RegionImage", new[] { "CustomKey" });
            DropIndex("Geography.RegionImage", new[] { "ID" });
            DropIndex("Tax.TaxDistrict", new[] { "Hash" });
            DropIndex("Geography.DistrictLanguage", new[] { "Hash" });
            DropIndex("Geography.DistrictImageType", new[] { "SortOrder" });
            DropIndex("Geography.DistrictImageType", new[] { "DisplayName" });
            DropIndex("Geography.DistrictImageType", new[] { "Name" });
            DropIndex("Geography.DistrictImageType", new[] { "Hash" });
            DropIndex("Geography.DistrictImageType", new[] { "Active" });
            DropIndex("Geography.DistrictImageType", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictImageType", new[] { "CustomKey" });
            DropIndex("Geography.DistrictImageType", new[] { "ID" });
            DropIndex("Geography.DistrictImage", new[] { "TypeID" });
            DropIndex("Geography.DistrictImage", new[] { "MasterID" });
            DropIndex("Geography.DistrictImage", new[] { "Name" });
            DropIndex("Geography.DistrictImage", new[] { "Hash" });
            DropIndex("Geography.DistrictImage", new[] { "Active" });
            DropIndex("Geography.DistrictImage", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictImage", new[] { "CustomKey" });
            DropIndex("Geography.DistrictImage", new[] { "ID" });
            DropIndex("Geography.DistrictCurrency", new[] { "Hash" });
            DropIndex("Geography.District", new[] { "Hash" });
            DropIndex("Geography.RegionCurrency", new[] { "Hash" });
            DropIndex("Geography.Region", new[] { "Hash" });
            DropIndex("Globalization.LanguageImageType", new[] { "SortOrder" });
            DropIndex("Globalization.LanguageImageType", new[] { "DisplayName" });
            DropIndex("Globalization.LanguageImageType", new[] { "Name" });
            DropIndex("Globalization.LanguageImageType", new[] { "Hash" });
            DropIndex("Globalization.LanguageImageType", new[] { "Active" });
            DropIndex("Globalization.LanguageImageType", new[] { "UpdatedDate" });
            DropIndex("Globalization.LanguageImageType", new[] { "CustomKey" });
            DropIndex("Globalization.LanguageImageType", new[] { "ID" });
            DropIndex("Globalization.LanguageImage", new[] { "TypeID" });
            DropIndex("Globalization.LanguageImage", new[] { "MasterID" });
            DropIndex("Globalization.LanguageImage", new[] { "Name" });
            DropIndex("Globalization.LanguageImage", new[] { "Hash" });
            DropIndex("Globalization.LanguageImage", new[] { "Active" });
            DropIndex("Globalization.LanguageImage", new[] { "UpdatedDate" });
            DropIndex("Globalization.LanguageImage", new[] { "CustomKey" });
            DropIndex("Globalization.LanguageImage", new[] { "ID" });
            DropIndex("Globalization.Language", new[] { "Hash" });
            DropIndex("Geography.CountryLanguage", new[] { "Hash" });
            DropIndex("Geography.CountryImageType", new[] { "SortOrder" });
            DropIndex("Geography.CountryImageType", new[] { "DisplayName" });
            DropIndex("Geography.CountryImageType", new[] { "Name" });
            DropIndex("Geography.CountryImageType", new[] { "Hash" });
            DropIndex("Geography.CountryImageType", new[] { "Active" });
            DropIndex("Geography.CountryImageType", new[] { "UpdatedDate" });
            DropIndex("Geography.CountryImageType", new[] { "CustomKey" });
            DropIndex("Geography.CountryImageType", new[] { "ID" });
            DropIndex("Geography.CountryImage", new[] { "TypeID" });
            DropIndex("Geography.CountryImage", new[] { "MasterID" });
            DropIndex("Geography.CountryImage", new[] { "Name" });
            DropIndex("Geography.CountryImage", new[] { "Hash" });
            DropIndex("Geography.CountryImage", new[] { "Active" });
            DropIndex("Geography.CountryImage", new[] { "UpdatedDate" });
            DropIndex("Geography.CountryImage", new[] { "CustomKey" });
            DropIndex("Geography.CountryImage", new[] { "ID" });
            DropIndex("Media.LibraryType", new[] { "Hash" });
            DropIndex("Media.File", new[] { "Hash" });
            DropIndex("Media.Audio", new[] { "Hash" });
            DropIndex("Media.Library", new[] { "Hash" });
            DropIndex("Currencies.CurrencyImageType", new[] { "SortOrder" });
            DropIndex("Currencies.CurrencyImageType", new[] { "DisplayName" });
            DropIndex("Currencies.CurrencyImageType", new[] { "Name" });
            DropIndex("Currencies.CurrencyImageType", new[] { "Hash" });
            DropIndex("Currencies.CurrencyImageType", new[] { "Active" });
            DropIndex("Currencies.CurrencyImageType", new[] { "UpdatedDate" });
            DropIndex("Currencies.CurrencyImageType", new[] { "CustomKey" });
            DropIndex("Currencies.CurrencyImageType", new[] { "ID" });
            DropIndex("Currencies.CurrencyImage", new[] { "TypeID" });
            DropIndex("Currencies.CurrencyImage", new[] { "MasterID" });
            DropIndex("Currencies.CurrencyImage", new[] { "Name" });
            DropIndex("Currencies.CurrencyImage", new[] { "Hash" });
            DropIndex("Currencies.CurrencyImage", new[] { "Active" });
            DropIndex("Currencies.CurrencyImage", new[] { "UpdatedDate" });
            DropIndex("Currencies.CurrencyImage", new[] { "CustomKey" });
            DropIndex("Currencies.CurrencyImage", new[] { "ID" });
            DropIndex("Currencies.HistoricalCurrencyRate", new[] { "Hash" });
            DropIndex("Currencies.Currency", new[] { "Hash" });
            DropIndex("Geography.CountryCurrency", new[] { "Hash" });
            DropIndex("Geography.Country", new[] { "Hash" });
            DropIndex("Geography.Address", new[] { "Hash" });
            DropIndex("Contacts.Contact", new[] { "Hash" });
            DropIndex("Accounts.AccountContact", new[] { "Hash" });
            DropIndex("Accounts.Account", new[] { "Hash" });
            DropIndex("Attributes.AttributeType", new[] { "Hash" });
            DropIndex("Attributes.GeneralAttribute", new[] { "Hash" });
            DropIndex("Attributes.AttributeValue", new[] { "Hash" });
            DropIndex("Accounts.AccountAttribute", new[] { "Hash" });
            DropColumn("Geography.ZipCode", "Hash");
            DropColumn("Shipping.UPSEndOfDay", "Hash");
            DropColumn("Globalization.UITranslation", "Hash");
            DropColumn("Globalization.UIKey", "Hash");
            DropColumn("System.SettingType", "Hash");
            DropColumn("System.SettingGroup", "Hash");
            DropColumn("System.Setting", "Hash");
            DropColumn("Hangfire.ScheduledJobConfigurationSetting", "Hash");
            DropColumn("Hangfire.ScheduledJobConfiguration", "Hash");
            DropColumn("Reporting.ReportTypes", "Hash");
            DropColumn("Reporting.Reports", "Hash");
            DropColumn("Contacts.ProfanityFilter", "Hash");
            DropColumn("Pricing.PriceRuleVendor", "Hash");
            DropColumn("Pricing.PriceRuleProductType", "Hash");
            DropColumn("Pricing.PriceRuleProduct", "Hash");
            DropColumn("Pricing.PriceRuleCategory", "Hash");
            DropColumn("Pricing.PriceRuleAccountType", "Hash");
            DropColumn("Pricing.PriceRule", "Hash");
            DropColumn("Pricing.PriceRuleAccount", "Hash");
            DropColumn("Notifications.Action", "Hash");
            DropColumn("Favorites.FavoriteShipCarrier", "Hash");
            DropColumn("Tracking.EventType", "Hash");
            DropColumn("Tracking.EventStatus", "Hash");
            DropColumn("Tracking.VisitStatus", "Hash");
            DropColumn("Tracking.Visit", "Hash");
            DropColumn("Tracking.Visitor", "Hash");
            DropColumn("Tracking.PageViewType", "Hash");
            DropColumn("Tracking.PageViewStatus", "Hash");
            DropColumn("Tracking.PageView", "Hash");
            DropColumn("Tracking.PageViewEvent", "Hash");
            DropColumn("Tracking.IPOrganizationStatus", "Hash");
            DropColumn("Tracking.IPOrganization", "Hash");
            DropColumn("Tracking.Event", "Hash");
            DropColumn("System.SystemLog", "Hash");
            DropColumn("Advertising.AdType", "Hash");
            DropColumn("Advertising.AdStatus", "Hash");
            DropColumn("Tracking.CampaignType", "Hash");
            DropColumn("Tracking.CampaignStatus", "Hash");
            DropColumn("Tracking.Campaign", "Hash");
            DropColumn("Tracking.CampaignAd", "Hash");
            DropColumn("Advertising.ZoneType", "Hash");
            DropColumn("Advertising.ZoneStatus", "Hash");
            DropColumn("Advertising.Zone", "Hash");
            DropColumn("Counters.CounterType", "Hash");
            DropColumn("Counters.CounterLogType", "Hash");
            DropColumn("Counters.CounterLog", "Hash");
            DropColumn("Counters.Counter", "Hash");
            DropColumn("Advertising.AdZoneAccess", "Hash");
            DropColumn("Advertising.AdZone", "Hash");
            DropColumn("Advertising.AdStore", "Hash");
            DropColumn("Advertising.AdImage", "Hash");
            DropColumn("Advertising.Ad", "Hash");
            DropColumn("Advertising.AdAccount", "Hash");
            DropColumn("Accounts.AccountType", "Hash");
            DropColumn("Accounts.AccountStatus", "Hash");
            DropColumn("Contacts.Opportunities", "Hash");
            DropColumn("Accounts.AccountTerm", "Hash");
            DropColumn("Accounts.AccountPricePoint", "Hash");
            DropColumn("Contacts.ContactType", "Hash");
            DropColumn("Purchasing.PurchaseOrderType", "Hash");
            DropColumn("Purchasing.PurchaseOrderStatus", "Hash");
            DropColumn("Purchasing.PurchaseOrderState", "Hash");
            DropColumn("Purchasing.PurchaseOrderItemStatus", "Hash");
            DropColumn("Purchasing.PurchaseOrderItemShipment", "Hash");
            DropColumn("Discounts.PurchaseOrderItemDiscounts", "Hash");
            DropColumn("Purchasing.PurchaseOrderItemAttribute", "Hash");
            DropColumn("Purchasing.PurchaseOrderItem", "Hash");
            DropColumn("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1");
            DropColumn("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID");
            DropColumn("Purchasing.PurchaseOrderContact", "Hash");
            DropColumn("Purchasing.FreeOnBoard", "Hash");
            DropColumn("Purchasing.PurchaseOrderFile", "Hash");
            DropColumn("Discounts.PurchaseOrderDiscounts", "Hash");
            DropColumn("Purchasing.PurchaseOrderAttribute", "Hash");
            DropColumn("Ordering.SalesOrderType", "Hash");
            DropColumn("Ordering.SalesOrderStatus", "Hash");
            DropColumn("Ordering.SalesOrderState", "Hash");
            DropColumn("Payments.SalesOrderPayment", "Hash");
            DropColumn("Ordering.SalesOrderContact", "SalesOrder_ID1");
            DropColumn("Ordering.SalesOrderContact", "SalesOrder_ID");
            DropColumn("Ordering.SalesOrderContact", "Hash");
            DropColumn("Ordering.SalesOrderFile", "Hash");
            DropColumn("Discounts.SalesOrderDiscounts", "Hash");
            DropColumn("Contacts.CustomerPriority", "Hash");
            DropColumn("Ordering.SalesOrderAttribute", "Hash");
            DropColumn("Invoicing.SalesInvoiceType", "Hash");
            DropColumn("Invoicing.SalesInvoiceStatus", "Hash");
            DropColumn("Invoicing.SalesInvoiceState", "Hash");
            DropColumn("Invoicing.SalesInvoiceItemStatus", "Hash");
            DropColumn("Invoicing.SalesInvoiceItemShipment", "Hash");
            DropColumn("Discounts.SalesInvoiceItemDiscounts", "Hash");
            DropColumn("Invoicing.SalesInvoiceItemAttribute", "Hash");
            DropColumn("Invoicing.SalesInvoiceItem", "Hash");
            DropColumn("Payments.SalesInvoicePayment", "Hash");
            DropColumn("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1");
            DropColumn("Invoicing.SalesInvoiceContact", "SalesInvoice_ID");
            DropColumn("Invoicing.SalesInvoiceContact", "Hash");
            DropColumn("Invoicing.SalesInvoiceFile", "Hash");
            DropColumn("Discounts.DiscountStores", "Hash");
            DropColumn("Discounts.DiscountProductType", "Hash");
            DropColumn("Discounts.DiscountCode", "Hash");
            DropColumn("Products.ProductType", "Hash");
            DropColumn("Pricing.PriceRounding", "Hash");
            DropColumn("Products.ProductImageType", "Hash");
            DropColumn("Products.ProductImage", "Hash");
            DropColumn("Products.ProductFile", "Hash");
            DropColumn("Products.ProductAssociationType", "Hash");
            DropColumn("Products.ProductAssociationAttribute", "Hash");
            DropColumn("Products.ProductAssociation", "Hash");
            DropColumn("Shipping.PackageType", "Hash");
            DropColumn("Shipping.Package", "Hash");
            DropColumn("Discounts.DiscountProducts", "Hash");
            DropColumn("Shopping.CartItemStatus", "Hash");
            DropColumn("Shopping.CartItemShipment", "Hash");
            DropColumn("Shopping.CartType", "Hash");
            DropColumn("Shopping.CartStatus", "Hash");
            DropColumn("Shopping.CartState", "Hash");
            DropColumn("System.NoteType", "Hash");
            DropColumn("Sampling.SampleRequestType", "Hash");
            DropColumn("Sampling.SampleRequestStatus", "Hash");
            DropColumn("Sampling.SampleRequestState", "Hash");
            DropColumn("Sampling.SampleRequestContact", "SampleRequest_ID1");
            DropColumn("Sampling.SampleRequestContact", "SampleRequest_ID");
            DropColumn("Sampling.SampleRequestContact", "Hash");
            DropColumn("Sampling.SampleRequestItemStatus", "Hash");
            DropColumn("Sampling.SampleRequestItemShipment", "Hash");
            DropColumn("Discounts.SampleRequestItemDiscounts", "Hash");
            DropColumn("Sampling.SampleRequestItemAttribute", "Hash");
            DropColumn("Sampling.SampleRequestItem", "Hash");
            DropColumn("Sampling.SampleRequestFile", "Hash");
            DropColumn("Discounts.SampleRequestDiscounts", "Hash");
            DropColumn("Sampling.SampleRequestAttribute", "Hash");
            DropColumn("Payments.Wallet", "Hash");
            DropColumn("CalendarEvents.UserEventAttendanceType", "Hash");
            DropColumn("CalendarEvents.CalendarEventType", "Hash");
            DropColumn("CalendarEvents.CalendarEventStatus", "Hash");
            DropColumn("CalendarEvents.CalendarEventProducts", "Hash");
            DropColumn("CalendarEvents.CalendarEventFile", "Hash");
            DropColumn("CalendarEvents.CalendarEventDetail", "Hash");
            DropColumn("CalendarEvents.CalendarEvent", "Hash");
            DropColumn("CalendarEvents.UserEventAttendance", "Hash");
            DropColumn("Contacts.UserStatus", "Hash");
            DropColumn("Quoting.SalesQuoteType", "Hash");
            DropColumn("Quoting.SalesQuoteStatus", "Hash");
            DropColumn("Quoting.SalesQuoteState", "Hash");
            DropColumn("Shipping.ShipOption", "Hash");
            DropColumn("Quoting.SalesQuoteContact", "SalesQuote_ID1");
            DropColumn("Quoting.SalesQuoteContact", "SalesQuote_ID");
            DropColumn("Quoting.SalesQuoteContact", "Hash");
            DropColumn("Quoting.SalesQuoteItemStatus", "Hash");
            DropColumn("Quoting.SalesQuoteItemShipment", "Hash");
            DropColumn("Discounts.SalesQuoteItemDiscounts", "Hash");
            DropColumn("Quoting.SalesQuoteItemAttribute", "Hash");
            DropColumn("Quoting.SalesQuoteItem", "Hash");
            DropColumn("Quoting.SalesQuoteFile", "Hash");
            DropColumn("Discounts.SalesQuoteDiscounts", "Hash");
            DropColumn("Quoting.SalesQuoteAttribute", "Hash");
            DropColumn("Quoting.SalesQuoteSalesOrder", "Hash");
            DropColumn("Notifications.Notification", "Hash");
            DropColumn("Notifications.NotificationMessage", "Hash");
            DropColumn("Groups.GroupType", "Hash");
            DropColumn("Groups.GroupStatus", "Hash");
            DropColumn("Groups.GroupUser", "Hash");
            DropColumn("Groups.Group", "Hash");
            DropColumn("Messaging.EmailType", "Hash");
            DropColumn("Messaging.EmailStatus", "Hash");
            DropColumn("Messaging.EmailTemplate", "Hash");
            DropColumn("Messaging.EmailQueue", "Hash");
            DropColumn("Messaging.MessageRecipient", "Hash");
            DropColumn("Messaging.Conversation", "Hash");
            DropColumn("Messaging.Message", "Hash");
            DropColumn("Messaging.MessageAttachment", "StoredFileID");
            DropColumn("Messaging.MessageAttachment", "Hash");
            DropColumn("Favorites.FavoriteVendor", "Hash");
            DropColumn("Favorites.FavoriteStore", "Hash");
            DropColumn("Reviews.ReviewType", "Hash");
            DropColumn("Stores.StoreType", "Hash");
            DropColumn("Contacts.UserType", "Hash");
            DropColumn("Stores.StoreUserType", "Hash");
            DropColumn("Stores.StoreUser", "Hash");
            DropColumn("Products.ProductSubscriptionType", "Hash");
            DropColumn("Payments.SubscriptionType", "Hash");
            DropColumn("Payments.SubscriptionStatus", "Hash");
            DropColumn("Payments.RepeatType", "Hash");
            DropColumn("Payments.PaymentType", "Hash");
            DropColumn("Payments.SubscriptionHistory", "Hash");
            DropColumn("Payments.PaymentStatus", "Hash");
            DropColumn("Payments.PaymentMethod", "Hash");
            DropColumn("Stores.StoreSubscription", "Hash");
            DropColumn("Stores.StoreManufacturer", "Hash");
            DropColumn("Shipping.ShipmentType", "Hash");
            DropColumn("Shipping.ShipmentStatus", "Hash");
            DropColumn("Shipping.ShipmentEvent", "Hash");
            DropColumn("Vendors.VendorManufacturer", "Hash");
            DropColumn("Vendors.VendorTerm", "Hash");
            DropColumn("Vendors.Term", "Hash");
            DropColumn("Stores.StoreVendor", "Hash");
            DropColumn("Shipping.ShipCarrierMethod", "Hash");
            DropColumn("Shipping.CarrierOrigin", "Hash");
            DropColumn("Shipping.CarrierInvoice", "Hash");
            DropColumn("Shipping.ShipCarrier", "Hash");
            DropColumn("Vendors.ShipVia", "Hash");
            DropColumn("Contacts.ContactMethod", "Hash");
            DropColumn("Vendors.Vendor", "Hash");
            DropColumn("Vendors.VendorProduct", "Hash");
            DropColumn("Stores.StoreProduct", "Hash");
            DropColumn("Ordering.SalesOrderItemStatus", "Hash");
            DropColumn("Discounts.SalesOrderItemDiscounts", "Hash");
            DropColumn("Ordering.SalesOrderItemAttribute", "Hash");
            DropColumn("Ordering.SalesOrderItem", "Hash");
            DropColumn("Ordering.SalesOrderItemShipment", "Hash");
            DropColumn("Products.ProductInventoryLocationSection", "Hash");
            DropColumn("Inventory.InventoryLocationSection", "Hash");
            DropColumn("Stores.StoreInventoryLocation", "Hash");
            DropColumn("Stores.StoreImage", "Hash");
            DropColumn("Stores.StoreContact", "Hash");
            DropColumn("Categories.CategoryType", "Hash");
            DropColumn("Stores.StoreCategoryType", "Hash");
            DropColumn("Stores.StoreCategory", "Hash");
            DropColumn("Pricing.PricePoint", "Hash");
            DropColumn("Stores.StoreAccount", "Hash");
            DropColumn("Stores.StoreSiteDomain", "Hash");
            DropColumn("Stores.SocialProvider", "Hash");
            DropColumn("Stores.SiteDomainSocialProvider", "Hash");
            DropColumn("Stores.SiteDomain", "Hash");
            DropColumn("Stores.BrandSiteDomain", "Hash");
            DropColumn("Stores.Brand", "Hash");
            DropColumn("Stores.BrandStore", "Hash");
            DropColumn("Reviews.Review", "Hash");
            DropColumn("Manufacturers.ManufacturerProduct", "Hash");
            DropColumn("Favorites.FavoriteManufacturer", "Hash");
            DropColumn("Favorites.FavoriteCategory", "Hash");
            DropColumn("Contacts.UserAttribute", "Hash");
            DropColumn("System.Note", "Hash");
            DropColumn("Shopping.CartFile", "Hash");
            DropColumn("Discounts.CartDiscounts", "Hash");
            DropColumn("Shopping.CartContact", "Cart_ID1");
            DropColumn("Shopping.CartContact", "Cart_ID");
            DropColumn("Shopping.CartContact", "Hash");
            DropColumn("Shopping.CartAttribute", "Hash");
            DropColumn("Shopping.Cart", "Hash");
            DropColumn("Discounts.CartItemDiscounts", "Hash");
            DropColumn("Shopping.CartItemAttribute", "Hash");
            DropColumn("Shopping.CartItem", "Hash");
            DropColumn("Products.ProductAttribute", "Hash");
            DropColumn("Products.Product", "Hash");
            DropColumn("Products.ProductCategoryAttribute", "Hash");
            DropColumn("Products.ProductCategory", "Hash");
            DropColumn("Categories.CategoryImage", "Hash");
            DropColumn("Categories.CategoryFile", "SeoMetaData");
            DropColumn("Categories.CategoryFile", "SeoDescription");
            DropColumn("Categories.CategoryFile", "SeoPageTitle");
            DropColumn("Categories.CategoryFile", "SeoUrl");
            DropColumn("Categories.CategoryFile", "SeoKeywords");
            DropColumn("Categories.CategoryFile", "Hash");
            DropColumn("Categories.CategoryAttribute", "Hash");
            DropColumn("Categories.Category", "Hash");
            DropColumn("Discounts.DiscountCategories", "Hash");
            DropColumn("Discounts.Discount", "Hash");
            DropColumn("Discounts.SalesInvoiceDiscounts", "Hash");
            DropColumn("Invoicing.SalesInvoiceAttribute", "Hash");
            DropColumn("Invoicing.SalesOrderSalesInvoice", "Hash");
            DropColumn("Purchasing.SalesOrderPurchaseOrder", "Hash");
            DropColumn("Tax.TaxCountry", "Hash");
            DropColumn("Tax.TaxRegion", "Hash");
            DropColumn("Geography.RegionLanguage", "Hash");
            DropColumn("Geography.InterRegion", "Hash");
            DropColumn("Tax.TaxDistrict", "Hash");
            DropColumn("Geography.DistrictLanguage", "Hash");
            DropColumn("Geography.DistrictCurrency", "Hash");
            DropColumn("Geography.District", "Hash");
            DropColumn("Geography.RegionCurrency", "Hash");
            DropColumn("Globalization.Language", "Hash");
            DropColumn("Geography.CountryLanguage", "Hash");
            DropColumn("Media.Video", "Hash");
            DropColumn("Media.LibraryType", "Hash");
            DropColumn("Media.Document", "Hash");
            DropColumn("Media.Image", "Hash");
            DropColumn("Media.FileData", "Hash");
            DropColumn("Media.File", "Hash");
            DropColumn("Media.Audio", "Hash");
            DropColumn("Media.Library", "Hash");
            DropColumn("Currencies.HistoricalCurrencyRate", "Hash");
            DropColumn("Currencies.Currency", "Hash");
            DropColumn("Geography.CountryCurrency", "Hash");
            DropColumn("Geography.Address", "Hash");
            DropColumn("Contacts.Contact", "Hash");
            DropColumn("Accounts.AccountContact", "Hash");
            DropColumn("Attributes.AttributeType", "Hash");
            DropColumn("Attributes.GeneralAttribute", "Hash");
            DropColumn("Attributes.AttributeValue", "Hash");
            DropColumn("Accounts.AccountAttribute", "Hash");
            DropTable("Advertising.AdImageType");
            DropTable("Advertising.AdImageNew");
            DropTable("Accounts.AccountImageType");
            DropTable("Accounts.AccountImage");
            DropTable("Contacts.ContactImageType");
            DropTable("Contacts.ContactImage");
            DropTable("Purchasing.PurchaseOrderFileNew");
            DropTable("Ordering.SalesOrderFileNew");
            DropTable("Invoicing.SalesInvoiceFileNew");
            DropTable("Categories.CategoryFileNew");
            DropTable("Products.ProductFileNew");
            DropTable("Products.ProductImageNew");
            DropTable("Shopping.CartFileNew");
            DropTable("Sampling.SampleRequestFileNew");
            DropTable("CalendarEvents.CalendarEventFileNew");
            DropTable("Quoting.SalesQuoteFileNew");
            DropTable("Media.StoredFile");
            DropTable("Contacts.UserImageType");
            DropTable("Contacts.UserImage");
            DropTable("Vendors.VendorImageType");
            DropTable("Vendors.VendorImage");
            DropTable("Stores.StoreImageType");
            DropTable("Stores.StoreImageNew");
            DropTable("Stores.BrandImageType");
            DropTable("Stores.BrandImage");
            DropTable("Manufacturers.ManufacturerImageType");
            DropTable("Manufacturers.ManufacturerImage");
            DropTable("Categories.CategoryImageType");
            DropTable("Categories.CategoryImageNew");
            DropTable("Geography.RegionImageType");
            DropTable("Geography.RegionImage");
            DropTable("Geography.DistrictImageType");
            DropTable("Geography.DistrictImage");
            DropTable("Globalization.LanguageImageType");
            DropTable("Globalization.LanguageImage");
            DropTable("Geography.CountryImageType");
            DropTable("Geography.CountryImage");
            DropTable("Currencies.CurrencyImageType");
            DropTable("Currencies.CurrencyImage");
            CreateIndex("Products.Product", "ProductHash");
            CreateIndex("Categories.Category", "CategoryHash");
            AddForeignKey("Sampling.SampleRequestContact", "SampleRequestID", "Sampling.SampleRequest", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteContact", "SalesQuoteID", "Quoting.SalesQuote", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartContact", "CartID", "Shopping.Cart", "ID", cascadeDelete: true);
            AddForeignKey("Categories.CategoryImage", "CategoryID", "Categories.Category", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoiceID", "Invoicing.SalesInvoice", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderContact", "SalesOrderID", "Ordering.SalesOrder", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrderID", "Purchasing.PurchaseOrder", "ID", cascadeDelete: true);
        }
    }
}
