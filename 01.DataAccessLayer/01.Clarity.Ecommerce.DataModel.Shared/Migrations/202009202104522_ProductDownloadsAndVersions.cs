// <copyright file="202009202104522_ProductDownloadsAndVersions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202009202104522 product downloads and versions class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ProductDownloadsAndVersions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Products.ProductShipCarrierMethod",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        StoreID = c.Int(),
                        BrandID = c.Int(),
                        MinQuantity = c.Decimal(precision: 18, scale: 4),
                        MaxQuantity = c.Decimal(precision: 18, scale: 4),
                        From = c.DateTime(precision: 7, storeType: "datetime2"),
                        To = c.DateTime(precision: 7, storeType: "datetime2"),
                        UnitOfMeasure = c.String(maxLength: 10),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 4),
                        CurrencyID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.BrandID)
                .ForeignKey("Currencies.Currency", t => t.CurrencyID)
                .ForeignKey("Products.Product", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Shipping.ShipCarrierMethod", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.StoreID)
                .Index(t => t.BrandID)
                .Index(t => t.CurrencyID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Products.ProductDownload",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeID = c.Int(nullable: false),
                        IsAbsoluteUrl = c.Boolean(nullable: false),
                        AbsoluteUrl = c.String(maxLength: 512, unicode: false),
                        RelativeUrl = c.String(maxLength: 512, unicode: false),
                        ProductID = c.Int(nullable: false),
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
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("Products.ProductDownloadType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.ProductID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Products.ProductDownloadType",
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
                "System.RecordVersion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StoreID = c.Int(),
                        BrandID = c.Int(),
                        TypeID = c.Int(nullable: false),
                        RecordID = c.Int(),
                        OriginalPublishDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        MostRecentPublishDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsDraft = c.Boolean(nullable: false),
                        SerializedRecord = c.String(),
                        PublishedByUserID = c.Int(),
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
                .ForeignKey("Brands.Brand", t => t.BrandID)
                .ForeignKey("Contacts.User", t => t.PublishedByUserID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("System.RecordVersionType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StoreID)
                .Index(t => t.BrandID)
                .Index(t => t.TypeID)
                .Index(t => t.PublishedByUserID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "System.RecordVersionType",
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
        }

        public override void Down()
        {
            DropForeignKey("System.RecordVersion", "TypeID", "System.RecordVersionType");
            DropForeignKey("System.RecordVersion", "StoreID", "Stores.Store");
            DropForeignKey("System.RecordVersion", "PublishedByUserID", "Contacts.User");
            DropForeignKey("System.RecordVersion", "BrandID", "Brands.Brand");
            DropForeignKey("Products.ProductDownload", "TypeID", "Products.ProductDownloadType");
            DropForeignKey("Products.ProductDownload", "ProductID", "Products.Product");
            DropForeignKey("Products.ProductShipCarrierMethod", "StoreID", "Stores.Store");
            DropForeignKey("Products.ProductShipCarrierMethod", "SlaveID", "Shipping.ShipCarrierMethod");
            DropForeignKey("Products.ProductShipCarrierMethod", "MasterID", "Products.Product");
            DropForeignKey("Products.ProductShipCarrierMethod", "CurrencyID", "Currencies.Currency");
            DropForeignKey("Products.ProductShipCarrierMethod", "BrandID", "Brands.Brand");
            DropIndex("System.RecordVersionType", new[] { "Hash" });
            DropIndex("System.RecordVersionType", new[] { "Active" });
            DropIndex("System.RecordVersionType", new[] { "UpdatedDate" });
            DropIndex("System.RecordVersionType", new[] { "CreatedDate" });
            DropIndex("System.RecordVersionType", new[] { "CustomKey" });
            DropIndex("System.RecordVersionType", new[] { "Name" });
            DropIndex("System.RecordVersionType", new[] { "SortOrder" });
            DropIndex("System.RecordVersionType", new[] { "DisplayName" });
            DropIndex("System.RecordVersionType", new[] { "ID" });
            DropIndex("System.RecordVersion", new[] { "Hash" });
            DropIndex("System.RecordVersion", new[] { "Active" });
            DropIndex("System.RecordVersion", new[] { "UpdatedDate" });
            DropIndex("System.RecordVersion", new[] { "CreatedDate" });
            DropIndex("System.RecordVersion", new[] { "CustomKey" });
            DropIndex("System.RecordVersion", new[] { "Name" });
            DropIndex("System.RecordVersion", new[] { "PublishedByUserID" });
            DropIndex("System.RecordVersion", new[] { "TypeID" });
            DropIndex("System.RecordVersion", new[] { "BrandID" });
            DropIndex("System.RecordVersion", new[] { "StoreID" });
            DropIndex("System.RecordVersion", new[] { "ID" });
            DropIndex("Products.ProductDownloadType", new[] { "Hash" });
            DropIndex("Products.ProductDownloadType", new[] { "Active" });
            DropIndex("Products.ProductDownloadType", new[] { "UpdatedDate" });
            DropIndex("Products.ProductDownloadType", new[] { "CreatedDate" });
            DropIndex("Products.ProductDownloadType", new[] { "CustomKey" });
            DropIndex("Products.ProductDownloadType", new[] { "Name" });
            DropIndex("Products.ProductDownloadType", new[] { "SortOrder" });
            DropIndex("Products.ProductDownloadType", new[] { "DisplayName" });
            DropIndex("Products.ProductDownloadType", new[] { "ID" });
            DropIndex("Products.ProductDownload", new[] { "Hash" });
            DropIndex("Products.ProductDownload", new[] { "Active" });
            DropIndex("Products.ProductDownload", new[] { "UpdatedDate" });
            DropIndex("Products.ProductDownload", new[] { "CreatedDate" });
            DropIndex("Products.ProductDownload", new[] { "CustomKey" });
            DropIndex("Products.ProductDownload", new[] { "Name" });
            DropIndex("Products.ProductDownload", new[] { "ProductID" });
            DropIndex("Products.ProductDownload", new[] { "TypeID" });
            DropIndex("Products.ProductDownload", new[] { "ID" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "Hash" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "Active" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "UpdatedDate" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "CreatedDate" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "CustomKey" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "CurrencyID" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "BrandID" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "StoreID" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "SlaveID" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "MasterID" });
            DropIndex("Products.ProductShipCarrierMethod", new[] { "ID" });
            DropTable("System.RecordVersionType");
            DropTable("System.RecordVersion");
            DropTable("Products.ProductDownloadType");
            DropTable("Products.ProductDownload");
            DropTable("Products.ProductShipCarrierMethod");
        }
    }
}
