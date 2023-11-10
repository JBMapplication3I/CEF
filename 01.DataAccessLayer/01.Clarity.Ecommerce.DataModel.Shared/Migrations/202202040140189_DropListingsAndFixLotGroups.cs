// <copyright file="202202040140189_DropListingsAndFixLotGroups.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202201132213009 drop listings and fix lot groups class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DropListingsAndFixLotGroups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Auctions.Listing", "AuctionID", "Auctions.Auction");
            DropForeignKey("Auctions.Bid", "ListingID", "Auctions.Listing");
            DropForeignKey("Auctions.ListingCategory", "MasterID", "Auctions.Listing");
            DropForeignKey("Auctions.ListingCategory", "SlaveID", "Categories.Category");
            DropForeignKey("Auctions.Listing", "PickupLocationID", "Contacts.Contact");
            DropForeignKey("Auctions.Listing", "ProductID", "Products.Product");
            DropForeignKey("Auctions.Listing", "StatusID", "Auctions.ListingStatus");
            DropForeignKey("Auctions.Listing", "TypeID", "Auctions.ListingType");
            DropIndex("Auctions.Listing", new[] { "ID" });
            DropIndex("Auctions.Listing", new[] { "StatusID" });
            DropIndex("Auctions.Listing", new[] { "TypeID" });
            DropIndex("Auctions.Listing", new[] { "ProductID" });
            DropIndex("Auctions.Listing", new[] { "AuctionID" });
            DropIndex("Auctions.Listing", new[] { "PickupLocationID" });
            DropIndex("Auctions.Listing", new[] { "Name" });
            DropIndex("Auctions.Listing", new[] { "CustomKey" });
            DropIndex("Auctions.Listing", new[] { "CreatedDate" });
            DropIndex("Auctions.Listing", new[] { "UpdatedDate" });
            DropIndex("Auctions.Listing", new[] { "Active" });
            DropIndex("Auctions.Listing", new[] { "Hash" });
            DropIndex("Auctions.Bid", new[] { "ListingID" });
            DropIndex("Auctions.ListingCategory", new[] { "ID" });
            DropIndex("Auctions.ListingCategory", new[] { "MasterID" });
            DropIndex("Auctions.ListingCategory", new[] { "SlaveID" });
            DropIndex("Auctions.ListingCategory", new[] { "CustomKey" });
            DropIndex("Auctions.ListingCategory", new[] { "CreatedDate" });
            DropIndex("Auctions.ListingCategory", new[] { "UpdatedDate" });
            DropIndex("Auctions.ListingCategory", new[] { "Active" });
            DropIndex("Auctions.ListingCategory", new[] { "Hash" });
            DropIndex("Auctions.ListingStatus", new[] { "ID" });
            DropIndex("Auctions.ListingStatus", new[] { "DisplayName" });
            DropIndex("Auctions.ListingStatus", new[] { "SortOrder" });
            DropIndex("Auctions.ListingStatus", new[] { "Name" });
            DropIndex("Auctions.ListingStatus", new[] { "CustomKey" });
            DropIndex("Auctions.ListingStatus", new[] { "CreatedDate" });
            DropIndex("Auctions.ListingStatus", new[] { "UpdatedDate" });
            DropIndex("Auctions.ListingStatus", new[] { "Active" });
            DropIndex("Auctions.ListingStatus", new[] { "Hash" });
            DropIndex("Auctions.ListingType", new[] { "ID" });
            DropIndex("Auctions.ListingType", new[] { "DisplayName" });
            DropIndex("Auctions.ListingType", new[] { "SortOrder" });
            DropIndex("Auctions.ListingType", new[] { "Name" });
            DropIndex("Auctions.ListingType", new[] { "CustomKey" });
            DropIndex("Auctions.ListingType", new[] { "CreatedDate" });
            DropIndex("Auctions.ListingType", new[] { "UpdatedDate" });
            DropIndex("Auctions.ListingType", new[] { "Active" });
            DropIndex("Auctions.ListingType", new[] { "Hash" });
            DropColumn("Auctions.Bid", "ListingID");
            DropTable("Auctions.Listing");
            DropTable("Auctions.ListingCategory");
            DropTable("Auctions.ListingStatus");
            DropTable("Auctions.ListingType");
        }

        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);

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
                .PrimaryKey(t => t.ID);

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
                .PrimaryKey(t => t.ID);

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
                .PrimaryKey(t => t.ID);

            AddColumn("Auctions.Bid", "ListingID", c => c.Int());
            CreateIndex("Auctions.ListingType", "Hash");
            CreateIndex("Auctions.ListingType", "Active");
            CreateIndex("Auctions.ListingType", "UpdatedDate");
            CreateIndex("Auctions.ListingType", "CreatedDate");
            CreateIndex("Auctions.ListingType", "CustomKey");
            CreateIndex("Auctions.ListingType", "Name");
            CreateIndex("Auctions.ListingType", "SortOrder");
            CreateIndex("Auctions.ListingType", "DisplayName");
            CreateIndex("Auctions.ListingType", "ID");
            CreateIndex("Auctions.ListingStatus", "Hash");
            CreateIndex("Auctions.ListingStatus", "Active");
            CreateIndex("Auctions.ListingStatus", "UpdatedDate");
            CreateIndex("Auctions.ListingStatus", "CreatedDate");
            CreateIndex("Auctions.ListingStatus", "CustomKey");
            CreateIndex("Auctions.ListingStatus", "Name");
            CreateIndex("Auctions.ListingStatus", "SortOrder");
            CreateIndex("Auctions.ListingStatus", "DisplayName");
            CreateIndex("Auctions.ListingStatus", "ID");
            CreateIndex("Auctions.ListingCategory", "Hash");
            CreateIndex("Auctions.ListingCategory", "Active");
            CreateIndex("Auctions.ListingCategory", "UpdatedDate");
            CreateIndex("Auctions.ListingCategory", "CreatedDate");
            CreateIndex("Auctions.ListingCategory", "CustomKey");
            CreateIndex("Auctions.ListingCategory", "SlaveID");
            CreateIndex("Auctions.ListingCategory", "MasterID");
            CreateIndex("Auctions.ListingCategory", "ID");
            CreateIndex("Auctions.Bid", "ListingID");
            CreateIndex("Auctions.Listing", "Hash");
            CreateIndex("Auctions.Listing", "Active");
            CreateIndex("Auctions.Listing", "UpdatedDate");
            CreateIndex("Auctions.Listing", "CreatedDate");
            CreateIndex("Auctions.Listing", "CustomKey");
            CreateIndex("Auctions.Listing", "Name");
            CreateIndex("Auctions.Listing", "PickupLocationID");
            CreateIndex("Auctions.Listing", "AuctionID");
            CreateIndex("Auctions.Listing", "ProductID");
            CreateIndex("Auctions.Listing", "TypeID");
            CreateIndex("Auctions.Listing", "StatusID");
            CreateIndex("Auctions.Listing", "ID");
            AddForeignKey("Auctions.Listing", "TypeID", "Auctions.ListingType", "ID", cascadeDelete: true);
            AddForeignKey("Auctions.Listing", "StatusID", "Auctions.ListingStatus", "ID", cascadeDelete: true);
            AddForeignKey("Auctions.Listing", "ProductID", "Products.Product", "ID", cascadeDelete: true);
            AddForeignKey("Auctions.Listing", "PickupLocationID", "Contacts.Contact", "ID");
            AddForeignKey("Auctions.ListingCategory", "SlaveID", "Categories.Category", "ID", cascadeDelete: true);
            AddForeignKey("Auctions.ListingCategory", "MasterID", "Auctions.Listing", "ID", cascadeDelete: true);
            AddForeignKey("Auctions.Bid", "ListingID", "Auctions.Listing", "ID");
            AddForeignKey("Auctions.Listing", "AuctionID", "Auctions.Auction", "ID", cascadeDelete: true);
        }
    }
}
