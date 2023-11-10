// <copyright file="201902010024488_ScheduledImports.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201902010024488 scheduled imports class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ScheduledImports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Products.FutureImport",
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
                        StoreID = c.Int(),
                        FileName = c.String(nullable: false, maxLength: 512, unicode: false),
                        RunImportAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Attempts = c.Int(nullable: false),
                        HasError = c.Boolean(nullable: false),
                        StatusID = c.Int(nullable: false),
                        VendorID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.FutureImportStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Vendors.Vendor", t => t.VendorID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.StoreID)
                .Index(t => t.FileName)
                .Index(t => t.RunImportAt)
                .Index(t => t.StatusID)
                .Index(t => t.VendorID);

            CreateTable(
                "Products.FutureImportStatus",
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
        }

        public override void Down()
        {
            DropForeignKey("Products.FutureImport", "VendorID", "Vendors.Vendor");
            DropForeignKey("Products.FutureImport", "StoreID", "Stores.Store");
            DropForeignKey("Products.FutureImport", "StatusID", "Products.FutureImportStatus");
            DropIndex("Products.FutureImportStatus", new[] { "SortOrder" });
            DropIndex("Products.FutureImportStatus", new[] { "DisplayName" });
            DropIndex("Products.FutureImportStatus", new[] { "Name" });
            DropIndex("Products.FutureImportStatus", new[] { "Hash" });
            DropIndex("Products.FutureImportStatus", new[] { "Active" });
            DropIndex("Products.FutureImportStatus", new[] { "UpdatedDate" });
            DropIndex("Products.FutureImportStatus", new[] { "CreatedDate" });
            DropIndex("Products.FutureImportStatus", new[] { "CustomKey" });
            DropIndex("Products.FutureImportStatus", new[] { "ID" });
            DropIndex("Products.FutureImport", new[] { "VendorID" });
            DropIndex("Products.FutureImport", new[] { "StatusID" });
            DropIndex("Products.FutureImport", new[] { "RunImportAt" });
            DropIndex("Products.FutureImport", new[] { "FileName" });
            DropIndex("Products.FutureImport", new[] { "StoreID" });
            DropIndex("Products.FutureImport", new[] { "Name" });
            DropIndex("Products.FutureImport", new[] { "Hash" });
            DropIndex("Products.FutureImport", new[] { "Active" });
            DropIndex("Products.FutureImport", new[] { "UpdatedDate" });
            DropIndex("Products.FutureImport", new[] { "CreatedDate" });
            DropIndex("Products.FutureImport", new[] { "CustomKey" });
            DropIndex("Products.FutureImport", new[] { "ID" });
            DropTable("Products.FutureImportStatus");
            DropTable("Products.FutureImport");
        }
    }
}
