// <copyright file="202108082143506_Auctions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202108082143506 auctions class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Auctions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Auctions.Auction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        ContactID = c.Int(),
                        OpensAt = c.DateTime(),
                        ClosesAt = c.DateTime(),
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
                .ForeignKey("Contacts.Contact", t => t.ContactID)
                .ForeignKey("Auctions.AuctionStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Auctions.AuctionType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.ContactID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Auctions.Lot",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusID = c.Int(nullable: false),
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
                .ForeignKey("Auctions.LotStatus", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StatusID)
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
                "Auctions.Bid",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        WonTheLot = c.Boolean(nullable: false),
                        MaxBid = c.Decimal(precision: 18, scale: 4),
                        CurrentBid = c.Decimal(precision: 18, scale: 4),
                        LotID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Auctions.Lot", t => t.LotID, cascadeDelete: true)
                .ForeignKey("Auctions.BidStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StatusID)
                .Index(t => t.UserID)
                .Index(t => t.LotID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Auctions.BidStatus",
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
                "Auctions.LotStatus",
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
                "Auctions.AuctionStatus",
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
                "Auctions.AuctionType",
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

            AlterColumn("Quoting.SalesQuote", "RequestedShipDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("Products.Product", "BiddingReserve");
            DropColumn("Accounts.AccountProduct", "MaxBid");
            DropColumn("Accounts.AccountProduct", "CurrentBid");
            DropColumn("Accounts.AccountProduct", "NoShow");
        }

        public override void Down()
        {
            AddColumn("Accounts.AccountProduct", "NoShow", c => c.Boolean(nullable: false));
            AddColumn("Accounts.AccountProduct", "CurrentBid", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Accounts.AccountProduct", "MaxBid", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "BiddingReserve", c => c.Decimal(precision: 18, scale: 4));
            DropForeignKey("Auctions.Auction", "TypeID", "Auctions.AuctionType");
            DropForeignKey("Auctions.Auction", "StatusID", "Auctions.AuctionStatus");
            DropForeignKey("Auctions.Lot", "StatusID", "Auctions.LotStatus");
            DropForeignKey("Auctions.Lot", "ProductID", "Products.Product");
            DropForeignKey("Auctions.Lot", "PickupLocationID", "Contacts.Contact");
            DropForeignKey("Auctions.Bid", "UserID", "Contacts.User");
            DropForeignKey("Auctions.Bid", "StatusID", "Auctions.BidStatus");
            DropForeignKey("Auctions.Bid", "LotID", "Auctions.Lot");
            DropForeignKey("Auctions.Lot", "AuctionID", "Auctions.Auction");
            DropForeignKey("Auctions.Auction", "ContactID", "Contacts.Contact");
            DropIndex("Auctions.AuctionType", new[] { "Hash" });
            DropIndex("Auctions.AuctionType", new[] { "Active" });
            DropIndex("Auctions.AuctionType", new[] { "UpdatedDate" });
            DropIndex("Auctions.AuctionType", new[] { "CreatedDate" });
            DropIndex("Auctions.AuctionType", new[] { "CustomKey" });
            DropIndex("Auctions.AuctionType", new[] { "Name" });
            DropIndex("Auctions.AuctionType", new[] { "SortOrder" });
            DropIndex("Auctions.AuctionType", new[] { "DisplayName" });
            DropIndex("Auctions.AuctionType", new[] { "ID" });
            DropIndex("Auctions.AuctionStatus", new[] { "Hash" });
            DropIndex("Auctions.AuctionStatus", new[] { "Active" });
            DropIndex("Auctions.AuctionStatus", new[] { "UpdatedDate" });
            DropIndex("Auctions.AuctionStatus", new[] { "CreatedDate" });
            DropIndex("Auctions.AuctionStatus", new[] { "CustomKey" });
            DropIndex("Auctions.AuctionStatus", new[] { "Name" });
            DropIndex("Auctions.AuctionStatus", new[] { "SortOrder" });
            DropIndex("Auctions.AuctionStatus", new[] { "DisplayName" });
            DropIndex("Auctions.AuctionStatus", new[] { "ID" });
            DropIndex("Auctions.LotStatus", new[] { "Hash" });
            DropIndex("Auctions.LotStatus", new[] { "Active" });
            DropIndex("Auctions.LotStatus", new[] { "UpdatedDate" });
            DropIndex("Auctions.LotStatus", new[] { "CreatedDate" });
            DropIndex("Auctions.LotStatus", new[] { "CustomKey" });
            DropIndex("Auctions.LotStatus", new[] { "Name" });
            DropIndex("Auctions.LotStatus", new[] { "SortOrder" });
            DropIndex("Auctions.LotStatus", new[] { "DisplayName" });
            DropIndex("Auctions.LotStatus", new[] { "ID" });
            DropIndex("Auctions.BidStatus", new[] { "Hash" });
            DropIndex("Auctions.BidStatus", new[] { "Active" });
            DropIndex("Auctions.BidStatus", new[] { "UpdatedDate" });
            DropIndex("Auctions.BidStatus", new[] { "CreatedDate" });
            DropIndex("Auctions.BidStatus", new[] { "CustomKey" });
            DropIndex("Auctions.BidStatus", new[] { "Name" });
            DropIndex("Auctions.BidStatus", new[] { "SortOrder" });
            DropIndex("Auctions.BidStatus", new[] { "DisplayName" });
            DropIndex("Auctions.BidStatus", new[] { "ID" });
            DropIndex("Auctions.Bid", new[] { "Hash" });
            DropIndex("Auctions.Bid", new[] { "Active" });
            DropIndex("Auctions.Bid", new[] { "UpdatedDate" });
            DropIndex("Auctions.Bid", new[] { "CreatedDate" });
            DropIndex("Auctions.Bid", new[] { "CustomKey" });
            DropIndex("Auctions.Bid", new[] { "LotID" });
            DropIndex("Auctions.Bid", new[] { "UserID" });
            DropIndex("Auctions.Bid", new[] { "StatusID" });
            DropIndex("Auctions.Bid", new[] { "ID" });
            DropIndex("Auctions.Lot", new[] { "Hash" });
            DropIndex("Auctions.Lot", new[] { "Active" });
            DropIndex("Auctions.Lot", new[] { "UpdatedDate" });
            DropIndex("Auctions.Lot", new[] { "CreatedDate" });
            DropIndex("Auctions.Lot", new[] { "CustomKey" });
            DropIndex("Auctions.Lot", new[] { "Name" });
            DropIndex("Auctions.Lot", new[] { "PickupLocationID" });
            DropIndex("Auctions.Lot", new[] { "AuctionID" });
            DropIndex("Auctions.Lot", new[] { "ProductID" });
            DropIndex("Auctions.Lot", new[] { "StatusID" });
            DropIndex("Auctions.Lot", new[] { "ID" });
            DropIndex("Auctions.Auction", new[] { "Hash" });
            DropIndex("Auctions.Auction", new[] { "Active" });
            DropIndex("Auctions.Auction", new[] { "UpdatedDate" });
            DropIndex("Auctions.Auction", new[] { "CreatedDate" });
            DropIndex("Auctions.Auction", new[] { "CustomKey" });
            DropIndex("Auctions.Auction", new[] { "Name" });
            DropIndex("Auctions.Auction", new[] { "ContactID" });
            DropIndex("Auctions.Auction", new[] { "StatusID" });
            DropIndex("Auctions.Auction", new[] { "TypeID" });
            DropIndex("Auctions.Auction", new[] { "ID" });
            AlterColumn("Quoting.SalesQuote", "RequestedShipDate", c => c.DateTime());
            DropTable("Auctions.AuctionType");
            DropTable("Auctions.AuctionStatus");
            DropTable("Auctions.LotStatus");
            DropTable("Auctions.BidStatus");
            DropTable("Auctions.Bid");
            DropTable("Auctions.Lot");
            DropTable("Auctions.Auction");
        }
    }
}
