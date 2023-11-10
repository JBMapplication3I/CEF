// <copyright file="202111052049577_ElasticIndexingRequirements.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic indexing requirements class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ElasticIndexingRequirements : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Auctions.Auction", "BrandID", "Brands.Brand");
            DropForeignKey("Auctions.Auction", "StoreID", "Stores.Store");
            DropForeignKey("Auctions.Bid", "LotID", "Auctions.Lot");
            DropIndex("Auctions.Auction", new[] { "BrandID" });
            DropIndex("Auctions.Auction", new[] { "StoreID" });
            DropIndex("Auctions.Bid", new[] { "LotID" });
            CreateTable(
                "Stores.StoreCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleIn = c.Boolean(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.MasterID, cascadeDelete: true)
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
                "Brands.BrandManufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleIn = c.Boolean(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Manufacturers.Manufacturer", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseManufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleIn = c.Boolean(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Manufacturers.Manufacturer", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Brands.BrandVendor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleIn = c.Boolean(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Vendors.Vendor", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseVendor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleIn = c.Boolean(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Vendors.Vendor", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Franchises.FranchiseType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
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
                "Auctions.AuctionCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Auctions.Auction", t => t.MasterID, cascadeDelete: true)
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
                "Auctions.BrandAuction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleIn = c.Boolean(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Auctions.Auction", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Auctions.FranchiseAuction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleIn = c.Boolean(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Franchises.Franchise", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Auctions.Auction", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Auctions.Listing",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        BuyNowAvailable = c.Boolean(nullable: false),
                        BiddingReserve = c.Decimal(precision: 18, scale: 4),
                        NoShow = c.Boolean(nullable: false),
                        PickupTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        AuctionID = c.Int(nullable: false),
                        PickupLocationID = c.Int(),
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
                .ForeignKey("Auctions.Auction", t => t.AuctionID, cascadeDelete: true)
                .ForeignKey("Contacts.Contact", t => t.PickupLocationID)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("Auctions.ListingStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Auctions.ListingType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StatusID)
                .Index(t => t.TypeID)
                .Index(t => t.ProductID)
                .Index(t => t.AuctionID)
                .Index(t => t.PickupLocationID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Auctions.LotCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Auctions.Lot", t => t.MasterID, cascadeDelete: true)
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
                "Auctions.LotType",
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
                "Auctions.ListingCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Auctions.Listing", t => t.MasterID, cascadeDelete: true)
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
                "Auctions.ListingStatus",
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
                "Auctions.ListingType",
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
                "Auctions.StoreAuction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsVisibleIn = c.Boolean(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Auctions.Auction", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("Stores.Store", "SortOrder", c => c.Int());
            AddColumn("Brands.BrandStore", "IsVisibleIn", c => c.Boolean(nullable: false));
            AddColumn("Franchises.FranchiseStore", "IsVisibleIn", c => c.Boolean(nullable: false));
            AddColumn("Franchises.Franchise", "TypeID", c => c.Int(nullable: false));
            AddColumn("Franchises.FranchiseCategory", "IsVisibleIn", c => c.Boolean(nullable: false));
            AddColumn("Brands.BrandCategory", "IsVisibleIn", c => c.Boolean(nullable: false));
            AddColumn("Brands.BrandProduct", "IsVisibleIn", c => c.Boolean(nullable: false));
            AddColumn("Stores.StoreProduct", "IsVisibleIn", c => c.Boolean(nullable: false));
            AddColumn("Franchises.FranchiseProduct", "IsVisibleIn", c => c.Boolean(nullable: false));
            AddColumn("Auctions.Lot", "TypeID", c => c.Int(nullable: false));
            AddColumn("Auctions.Lot", "PreventBuyMultiple", c => c.Boolean(nullable: false));
            AddColumn("Auctions.Lot", "QuantityAvailable", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Auctions.Lot", "QuantitySold", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Auctions.Bid", "Won", c => c.Boolean(nullable: false));
            AddColumn("Auctions.Bid", "ListingID", c => c.Int());
            AlterColumn("Auctions.Bid", "LotID", c => c.Int());
            CreateIndex("Franchises.Franchise", "TypeID");
            CreateIndex("Auctions.Bid", "ListingID");
            CreateIndex("Auctions.Bid", "LotID");
            CreateIndex("Auctions.Lot", "TypeID");
            AddForeignKey("Franchises.Franchise", "TypeID", "Franchises.FranchiseType", "ID", cascadeDelete: true);
            AddForeignKey("Auctions.Bid", "ListingID", "Auctions.Listing", "ID");
            Sql("INSERT INTO Auctions.LotType (CreatedDate, Active, Name, CustomKey) VALUES(current_TimeStamp, 1, 'General', 'General')");
            Sql("UPDATE Auctions.Lot SET TypeID = 1 WHERE TypeID IS NULL OR TypeID = 0");
            AddForeignKey("Auctions.Lot", "TypeID", "Auctions.LotType", "ID", cascadeDelete: true);
            AddForeignKey("Auctions.Bid", "LotID", "Auctions.Lot", "ID");
            DropColumn("Brands.BrandProduct", "IsVisibleInBrand");
            DropColumn("Stores.StoreProduct", "IsVisibleInStore");
            DropColumn("Franchises.FranchiseProduct", "IsVisibleInFranchise");
            DropColumn("Auctions.Auction", "BrandID");
            DropColumn("Auctions.Auction", "StoreID");
            DropColumn("Auctions.Bid", "WonTheLot");
        }

        public override void Down()
        {
            AddColumn("Auctions.Bid", "WonTheLot", c => c.Boolean(nullable: false));
            AddColumn("Auctions.Auction", "StoreID", c => c.Int());
            AddColumn("Auctions.Auction", "BrandID", c => c.Int());
            AddColumn("Franchises.FranchiseProduct", "IsVisibleInFranchise", c => c.Boolean(nullable: false));
            AddColumn("Stores.StoreProduct", "IsVisibleInStore", c => c.Boolean(nullable: false));
            AddColumn("Brands.BrandProduct", "IsVisibleInBrand", c => c.Boolean(nullable: false));
            DropForeignKey("Auctions.Bid", "LotID", "Auctions.Lot");
            DropForeignKey("Auctions.AuctionCategory", "SlaveID", "Categories.Category");
            DropForeignKey("Auctions.AuctionCategory", "MasterID", "Auctions.Auction");
            DropForeignKey("Auctions.StoreAuction", "SlaveID", "Auctions.Auction");
            DropForeignKey("Auctions.StoreAuction", "MasterID", "Stores.Store");
            DropForeignKey("Auctions.Listing", "TypeID", "Auctions.ListingType");
            DropForeignKey("Auctions.Listing", "StatusID", "Auctions.ListingStatus");
            DropForeignKey("Auctions.Listing", "ProductID", "Products.Product");
            DropForeignKey("Auctions.Listing", "PickupLocationID", "Contacts.Contact");
            DropForeignKey("Auctions.ListingCategory", "SlaveID", "Categories.Category");
            DropForeignKey("Auctions.ListingCategory", "MasterID", "Auctions.Listing");
            DropForeignKey("Auctions.Lot", "TypeID", "Auctions.LotType");
            DropForeignKey("Auctions.LotCategory", "SlaveID", "Categories.Category");
            DropForeignKey("Auctions.LotCategory", "MasterID", "Auctions.Lot");
            DropForeignKey("Auctions.Bid", "ListingID", "Auctions.Listing");
            DropForeignKey("Auctions.Listing", "AuctionID", "Auctions.Auction");
            DropForeignKey("Auctions.FranchiseAuction", "SlaveID", "Auctions.Auction");
            DropForeignKey("Auctions.FranchiseAuction", "MasterID", "Franchises.Franchise");
            DropForeignKey("Auctions.BrandAuction", "SlaveID", "Auctions.Auction");
            DropForeignKey("Auctions.BrandAuction", "MasterID", "Brands.Brand");
            DropForeignKey("Stores.StoreCategory", "SlaveID", "Categories.Category");
            DropForeignKey("Franchises.Franchise", "TypeID", "Franchises.FranchiseType");
            DropForeignKey("Franchises.FranchiseVendor", "SlaveID", "Vendors.Vendor");
            DropForeignKey("Franchises.FranchiseVendor", "MasterID", "Franchises.Franchise");
            DropForeignKey("Brands.BrandVendor", "SlaveID", "Vendors.Vendor");
            DropForeignKey("Brands.BrandVendor", "MasterID", "Brands.Brand");
            DropForeignKey("Franchises.FranchiseManufacturer", "SlaveID", "Manufacturers.Manufacturer");
            DropForeignKey("Franchises.FranchiseManufacturer", "MasterID", "Franchises.Franchise");
            DropForeignKey("Brands.BrandManufacturer", "SlaveID", "Manufacturers.Manufacturer");
            DropForeignKey("Brands.BrandManufacturer", "MasterID", "Brands.Brand");
            DropForeignKey("Stores.StoreCategory", "MasterID", "Stores.Store");
            DropIndex("Auctions.StoreAuction", new[] { "Hash" });
            DropIndex("Auctions.StoreAuction", new[] { "Active" });
            DropIndex("Auctions.StoreAuction", new[] { "UpdatedDate" });
            DropIndex("Auctions.StoreAuction", new[] { "CreatedDate" });
            DropIndex("Auctions.StoreAuction", new[] { "CustomKey" });
            DropIndex("Auctions.StoreAuction", new[] { "SlaveID" });
            DropIndex("Auctions.StoreAuction", new[] { "MasterID" });
            DropIndex("Auctions.StoreAuction", new[] { "ID" });
            DropIndex("Auctions.ListingType", new[] { "Hash" });
            DropIndex("Auctions.ListingType", new[] { "Active" });
            DropIndex("Auctions.ListingType", new[] { "UpdatedDate" });
            DropIndex("Auctions.ListingType", new[] { "CreatedDate" });
            DropIndex("Auctions.ListingType", new[] { "CustomKey" });
            DropIndex("Auctions.ListingType", new[] { "Name" });
            DropIndex("Auctions.ListingType", new[] { "SortOrder" });
            DropIndex("Auctions.ListingType", new[] { "DisplayName" });
            DropIndex("Auctions.ListingType", new[] { "ID" });
            DropIndex("Auctions.ListingStatus", new[] { "Hash" });
            DropIndex("Auctions.ListingStatus", new[] { "Active" });
            DropIndex("Auctions.ListingStatus", new[] { "UpdatedDate" });
            DropIndex("Auctions.ListingStatus", new[] { "CreatedDate" });
            DropIndex("Auctions.ListingStatus", new[] { "CustomKey" });
            DropIndex("Auctions.ListingStatus", new[] { "Name" });
            DropIndex("Auctions.ListingStatus", new[] { "SortOrder" });
            DropIndex("Auctions.ListingStatus", new[] { "DisplayName" });
            DropIndex("Auctions.ListingStatus", new[] { "ID" });
            DropIndex("Auctions.ListingCategory", new[] { "Hash" });
            DropIndex("Auctions.ListingCategory", new[] { "Active" });
            DropIndex("Auctions.ListingCategory", new[] { "UpdatedDate" });
            DropIndex("Auctions.ListingCategory", new[] { "CreatedDate" });
            DropIndex("Auctions.ListingCategory", new[] { "CustomKey" });
            DropIndex("Auctions.ListingCategory", new[] { "SlaveID" });
            DropIndex("Auctions.ListingCategory", new[] { "MasterID" });
            DropIndex("Auctions.ListingCategory", new[] { "ID" });
            DropIndex("Auctions.LotType", new[] { "Hash" });
            DropIndex("Auctions.LotType", new[] { "Active" });
            DropIndex("Auctions.LotType", new[] { "UpdatedDate" });
            DropIndex("Auctions.LotType", new[] { "CreatedDate" });
            DropIndex("Auctions.LotType", new[] { "CustomKey" });
            DropIndex("Auctions.LotType", new[] { "Name" });
            DropIndex("Auctions.LotType", new[] { "SortOrder" });
            DropIndex("Auctions.LotType", new[] { "DisplayName" });
            DropIndex("Auctions.LotType", new[] { "ID" });
            DropIndex("Auctions.LotCategory", new[] { "Hash" });
            DropIndex("Auctions.LotCategory", new[] { "Active" });
            DropIndex("Auctions.LotCategory", new[] { "UpdatedDate" });
            DropIndex("Auctions.LotCategory", new[] { "CreatedDate" });
            DropIndex("Auctions.LotCategory", new[] { "CustomKey" });
            DropIndex("Auctions.LotCategory", new[] { "SlaveID" });
            DropIndex("Auctions.LotCategory", new[] { "MasterID" });
            DropIndex("Auctions.LotCategory", new[] { "ID" });
            DropIndex("Auctions.Lot", new[] { "TypeID" });
            DropIndex("Auctions.Bid", new[] { "LotID" });
            DropIndex("Auctions.Bid", new[] { "ListingID" });
            DropIndex("Auctions.Listing", new[] { "Hash" });
            DropIndex("Auctions.Listing", new[] { "Active" });
            DropIndex("Auctions.Listing", new[] { "UpdatedDate" });
            DropIndex("Auctions.Listing", new[] { "CreatedDate" });
            DropIndex("Auctions.Listing", new[] { "CustomKey" });
            DropIndex("Auctions.Listing", new[] { "Name" });
            DropIndex("Auctions.Listing", new[] { "PickupLocationID" });
            DropIndex("Auctions.Listing", new[] { "AuctionID" });
            DropIndex("Auctions.Listing", new[] { "ProductID" });
            DropIndex("Auctions.Listing", new[] { "TypeID" });
            DropIndex("Auctions.Listing", new[] { "StatusID" });
            DropIndex("Auctions.Listing", new[] { "ID" });
            DropIndex("Auctions.FranchiseAuction", new[] { "Hash" });
            DropIndex("Auctions.FranchiseAuction", new[] { "Active" });
            DropIndex("Auctions.FranchiseAuction", new[] { "UpdatedDate" });
            DropIndex("Auctions.FranchiseAuction", new[] { "CreatedDate" });
            DropIndex("Auctions.FranchiseAuction", new[] { "CustomKey" });
            DropIndex("Auctions.FranchiseAuction", new[] { "SlaveID" });
            DropIndex("Auctions.FranchiseAuction", new[] { "MasterID" });
            DropIndex("Auctions.FranchiseAuction", new[] { "ID" });
            DropIndex("Auctions.BrandAuction", new[] { "Hash" });
            DropIndex("Auctions.BrandAuction", new[] { "Active" });
            DropIndex("Auctions.BrandAuction", new[] { "UpdatedDate" });
            DropIndex("Auctions.BrandAuction", new[] { "CreatedDate" });
            DropIndex("Auctions.BrandAuction", new[] { "CustomKey" });
            DropIndex("Auctions.BrandAuction", new[] { "SlaveID" });
            DropIndex("Auctions.BrandAuction", new[] { "MasterID" });
            DropIndex("Auctions.BrandAuction", new[] { "ID" });
            DropIndex("Auctions.AuctionCategory", new[] { "Hash" });
            DropIndex("Auctions.AuctionCategory", new[] { "Active" });
            DropIndex("Auctions.AuctionCategory", new[] { "UpdatedDate" });
            DropIndex("Auctions.AuctionCategory", new[] { "CreatedDate" });
            DropIndex("Auctions.AuctionCategory", new[] { "CustomKey" });
            DropIndex("Auctions.AuctionCategory", new[] { "SlaveID" });
            DropIndex("Auctions.AuctionCategory", new[] { "MasterID" });
            DropIndex("Auctions.AuctionCategory", new[] { "ID" });
            DropIndex("Franchises.FranchiseType", new[] { "Hash" });
            DropIndex("Franchises.FranchiseType", new[] { "Active" });
            DropIndex("Franchises.FranchiseType", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseType", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseType", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseType", new[] { "Name" });
            DropIndex("Franchises.FranchiseType", new[] { "SortOrder" });
            DropIndex("Franchises.FranchiseType", new[] { "DisplayName" });
            DropIndex("Franchises.FranchiseType", new[] { "ID" });
            DropIndex("Franchises.FranchiseVendor", new[] { "Hash" });
            DropIndex("Franchises.FranchiseVendor", new[] { "Active" });
            DropIndex("Franchises.FranchiseVendor", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseVendor", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseVendor", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseVendor", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseVendor", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseVendor", new[] { "ID" });
            DropIndex("Brands.BrandVendor", new[] { "Hash" });
            DropIndex("Brands.BrandVendor", new[] { "Active" });
            DropIndex("Brands.BrandVendor", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandVendor", new[] { "CreatedDate" });
            DropIndex("Brands.BrandVendor", new[] { "CustomKey" });
            DropIndex("Brands.BrandVendor", new[] { "SlaveID" });
            DropIndex("Brands.BrandVendor", new[] { "MasterID" });
            DropIndex("Brands.BrandVendor", new[] { "ID" });
            DropIndex("Franchises.FranchiseManufacturer", new[] { "Hash" });
            DropIndex("Franchises.FranchiseManufacturer", new[] { "Active" });
            DropIndex("Franchises.FranchiseManufacturer", new[] { "UpdatedDate" });
            DropIndex("Franchises.FranchiseManufacturer", new[] { "CreatedDate" });
            DropIndex("Franchises.FranchiseManufacturer", new[] { "CustomKey" });
            DropIndex("Franchises.FranchiseManufacturer", new[] { "SlaveID" });
            DropIndex("Franchises.FranchiseManufacturer", new[] { "MasterID" });
            DropIndex("Franchises.FranchiseManufacturer", new[] { "ID" });
            DropIndex("Brands.BrandManufacturer", new[] { "Hash" });
            DropIndex("Brands.BrandManufacturer", new[] { "Active" });
            DropIndex("Brands.BrandManufacturer", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandManufacturer", new[] { "CreatedDate" });
            DropIndex("Brands.BrandManufacturer", new[] { "CustomKey" });
            DropIndex("Brands.BrandManufacturer", new[] { "SlaveID" });
            DropIndex("Brands.BrandManufacturer", new[] { "MasterID" });
            DropIndex("Brands.BrandManufacturer", new[] { "ID" });
            DropIndex("Franchises.Franchise", new[] { "TypeID" });
            DropIndex("Stores.StoreCategory", new[] { "Hash" });
            DropIndex("Stores.StoreCategory", new[] { "Active" });
            DropIndex("Stores.StoreCategory", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreCategory", new[] { "CreatedDate" });
            DropIndex("Stores.StoreCategory", new[] { "CustomKey" });
            DropIndex("Stores.StoreCategory", new[] { "SlaveID" });
            DropIndex("Stores.StoreCategory", new[] { "MasterID" });
            DropIndex("Stores.StoreCategory", new[] { "ID" });
            AlterColumn("Auctions.Bid", "LotID", c => c.Int(nullable: false));
            DropColumn("Auctions.Bid", "ListingID");
            DropColumn("Auctions.Bid", "Won");
            DropColumn("Auctions.Lot", "QuantitySold");
            DropColumn("Auctions.Lot", "QuantityAvailable");
            DropColumn("Auctions.Lot", "PreventBuyMultiple");
            DropColumn("Auctions.Lot", "TypeID");
            DropColumn("Franchises.FranchiseProduct", "IsVisibleIn");
            DropColumn("Stores.StoreProduct", "IsVisibleIn");
            DropColumn("Brands.BrandProduct", "IsVisibleIn");
            DropColumn("Brands.BrandCategory", "IsVisibleIn");
            DropColumn("Franchises.FranchiseCategory", "IsVisibleIn");
            DropColumn("Franchises.Franchise", "TypeID");
            DropColumn("Franchises.FranchiseStore", "IsVisibleIn");
            DropColumn("Brands.BrandStore", "IsVisibleIn");
            DropColumn("Stores.Store", "SortOrder");
            DropTable("Auctions.StoreAuction");
            DropTable("Auctions.ListingType");
            DropTable("Auctions.ListingStatus");
            DropTable("Auctions.ListingCategory");
            DropTable("Auctions.LotType");
            DropTable("Auctions.LotCategory");
            DropTable("Auctions.Listing");
            DropTable("Auctions.FranchiseAuction");
            DropTable("Auctions.BrandAuction");
            DropTable("Auctions.AuctionCategory");
            DropTable("Franchises.FranchiseType");
            DropTable("Franchises.FranchiseVendor");
            DropTable("Brands.BrandVendor");
            DropTable("Franchises.FranchiseManufacturer");
            DropTable("Brands.BrandManufacturer");
            DropTable("Stores.StoreCategory");
            CreateIndex("Auctions.Bid", "LotID");
            CreateIndex("Auctions.Auction", "StoreID");
            CreateIndex("Auctions.Auction", "BrandID");
            AddForeignKey("Auctions.Bid", "LotID", "Auctions.Lot", "ID", cascadeDelete: true);
            AddForeignKey("Auctions.Auction", "StoreID", "Stores.Store", "ID");
            AddForeignKey("Auctions.Auction", "BrandID", "Brands.Brand", "ID");
        }
    }
}
