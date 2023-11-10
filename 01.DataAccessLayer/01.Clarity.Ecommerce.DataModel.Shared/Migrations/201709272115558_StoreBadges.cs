// <copyright file="201709272115558_StoreBadges.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201709272115558 store badges class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class StoreBadges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Stores.StoreBadge",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        StoreID = c.Int(nullable: false),
                        BadgeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Badge", t => t.BadgeID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.StoreID)
                .Index(t => t.BadgeID);

            CreateTable(
                "Stores.Badge",
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
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.BadgeType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.TypeID);

            CreateTable(
                "Stores.BadgeImage",
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
                .ForeignKey("Stores.Badge", t => t.MasterID)
                .ForeignKey("Stores.BadgeImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "Stores.BadgeImageType",
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
                "Stores.BadgeType",
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
        }

        public override void Down()
        {
            DropForeignKey("Stores.StoreBadge", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreBadge", "BadgeID", "Stores.Badge");
            DropForeignKey("Stores.Badge", "TypeID", "Stores.BadgeType");
            DropForeignKey("Stores.BadgeImage", "TypeID", "Stores.BadgeImageType");
            DropForeignKey("Stores.BadgeImage", "MasterID", "Stores.Badge");
            DropIndex("Stores.BadgeType", new[] { "SortOrder" });
            DropIndex("Stores.BadgeType", new[] { "DisplayName" });
            DropIndex("Stores.BadgeType", new[] { "Name" });
            DropIndex("Stores.BadgeType", new[] { "Hash" });
            DropIndex("Stores.BadgeType", new[] { "Active" });
            DropIndex("Stores.BadgeType", new[] { "UpdatedDate" });
            DropIndex("Stores.BadgeType", new[] { "CustomKey" });
            DropIndex("Stores.BadgeType", new[] { "ID" });
            DropIndex("Stores.BadgeImageType", new[] { "SortOrder" });
            DropIndex("Stores.BadgeImageType", new[] { "DisplayName" });
            DropIndex("Stores.BadgeImageType", new[] { "Name" });
            DropIndex("Stores.BadgeImageType", new[] { "Hash" });
            DropIndex("Stores.BadgeImageType", new[] { "Active" });
            DropIndex("Stores.BadgeImageType", new[] { "UpdatedDate" });
            DropIndex("Stores.BadgeImageType", new[] { "CustomKey" });
            DropIndex("Stores.BadgeImageType", new[] { "ID" });
            DropIndex("Stores.BadgeImage", new[] { "TypeID" });
            DropIndex("Stores.BadgeImage", new[] { "MasterID" });
            DropIndex("Stores.BadgeImage", new[] { "Name" });
            DropIndex("Stores.BadgeImage", new[] { "Hash" });
            DropIndex("Stores.BadgeImage", new[] { "Active" });
            DropIndex("Stores.BadgeImage", new[] { "UpdatedDate" });
            DropIndex("Stores.BadgeImage", new[] { "CustomKey" });
            DropIndex("Stores.BadgeImage", new[] { "ID" });
            DropIndex("Stores.Badge", new[] { "TypeID" });
            DropIndex("Stores.Badge", new[] { "Name" });
            DropIndex("Stores.Badge", new[] { "Hash" });
            DropIndex("Stores.Badge", new[] { "Active" });
            DropIndex("Stores.Badge", new[] { "UpdatedDate" });
            DropIndex("Stores.Badge", new[] { "CustomKey" });
            DropIndex("Stores.Badge", new[] { "ID" });
            DropIndex("Stores.StoreBadge", new[] { "BadgeID" });
            DropIndex("Stores.StoreBadge", new[] { "StoreID" });
            DropIndex("Stores.StoreBadge", new[] { "Hash" });
            DropIndex("Stores.StoreBadge", new[] { "Active" });
            DropIndex("Stores.StoreBadge", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreBadge", new[] { "CustomKey" });
            DropIndex("Stores.StoreBadge", new[] { "ID" });
            DropTable("Stores.BadgeType");
            DropTable("Stores.BadgeImageType");
            DropTable("Stores.BadgeImage");
            DropTable("Stores.Badge");
            DropTable("Stores.StoreBadge");
        }
    }
}
