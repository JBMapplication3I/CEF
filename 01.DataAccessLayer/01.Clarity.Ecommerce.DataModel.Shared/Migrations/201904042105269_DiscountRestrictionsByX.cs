// <copyright file="201904042105269_DiscountRestrictionsByX.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201904042105269 discount restrictions by x coordinate class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DiscountRestrictionsByX : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Discounts.DiscountAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Accounts.Account", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Discounts.DiscountAccountType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Accounts.AccountType", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Discounts.DiscountCountry",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Geography.Country", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Discounts.DiscountManufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Manufacturers.Manufacturer", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Discounts.DiscountUserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        RoleName = c.String(maxLength: 128, unicode: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.DiscountUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Discounts.DiscountVendor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Vendors.Vendor", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            AddColumn("Discounts.Discount", "UsageLimitByCart", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropForeignKey("Discounts.DiscountVendor", "SlaveID", "Vendors.Vendor");
            DropForeignKey("Discounts.DiscountVendor", "MasterID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountUser", "SlaveID", "Contacts.User");
            DropForeignKey("Discounts.DiscountUser", "MasterID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountUserRole", "MasterID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountManufacturer", "SlaveID", "Manufacturers.Manufacturer");
            DropForeignKey("Discounts.DiscountManufacturer", "MasterID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountCountry", "SlaveID", "Geography.Country");
            DropForeignKey("Discounts.DiscountCountry", "MasterID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountAccountType", "SlaveID", "Accounts.AccountType");
            DropForeignKey("Discounts.DiscountAccountType", "MasterID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountAccount", "SlaveID", "Accounts.Account");
            DropForeignKey("Discounts.DiscountAccount", "MasterID", "Discounts.Discount");
            DropIndex("Discounts.DiscountVendor", new[] { "SlaveID" });
            DropIndex("Discounts.DiscountVendor", new[] { "MasterID" });
            DropIndex("Discounts.DiscountVendor", new[] { "Hash" });
            DropIndex("Discounts.DiscountVendor", new[] { "Active" });
            DropIndex("Discounts.DiscountVendor", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountVendor", new[] { "CreatedDate" });
            DropIndex("Discounts.DiscountVendor", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountVendor", new[] { "ID" });
            DropIndex("Discounts.DiscountUser", new[] { "SlaveID" });
            DropIndex("Discounts.DiscountUser", new[] { "MasterID" });
            DropIndex("Discounts.DiscountUser", new[] { "Hash" });
            DropIndex("Discounts.DiscountUser", new[] { "Active" });
            DropIndex("Discounts.DiscountUser", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountUser", new[] { "CreatedDate" });
            DropIndex("Discounts.DiscountUser", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountUser", new[] { "ID" });
            DropIndex("Discounts.DiscountUserRole", new[] { "MasterID" });
            DropIndex("Discounts.DiscountUserRole", new[] { "Hash" });
            DropIndex("Discounts.DiscountUserRole", new[] { "Active" });
            DropIndex("Discounts.DiscountUserRole", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountUserRole", new[] { "CreatedDate" });
            DropIndex("Discounts.DiscountUserRole", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountUserRole", new[] { "ID" });
            DropIndex("Discounts.DiscountManufacturer", new[] { "SlaveID" });
            DropIndex("Discounts.DiscountManufacturer", new[] { "MasterID" });
            DropIndex("Discounts.DiscountManufacturer", new[] { "Hash" });
            DropIndex("Discounts.DiscountManufacturer", new[] { "Active" });
            DropIndex("Discounts.DiscountManufacturer", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountManufacturer", new[] { "CreatedDate" });
            DropIndex("Discounts.DiscountManufacturer", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountManufacturer", new[] { "ID" });
            DropIndex("Discounts.DiscountCountry", new[] { "SlaveID" });
            DropIndex("Discounts.DiscountCountry", new[] { "MasterID" });
            DropIndex("Discounts.DiscountCountry", new[] { "Hash" });
            DropIndex("Discounts.DiscountCountry", new[] { "Active" });
            DropIndex("Discounts.DiscountCountry", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountCountry", new[] { "CreatedDate" });
            DropIndex("Discounts.DiscountCountry", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountCountry", new[] { "ID" });
            DropIndex("Discounts.DiscountAccountType", new[] { "SlaveID" });
            DropIndex("Discounts.DiscountAccountType", new[] { "MasterID" });
            DropIndex("Discounts.DiscountAccountType", new[] { "Hash" });
            DropIndex("Discounts.DiscountAccountType", new[] { "Active" });
            DropIndex("Discounts.DiscountAccountType", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountAccountType", new[] { "CreatedDate" });
            DropIndex("Discounts.DiscountAccountType", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountAccountType", new[] { "ID" });
            DropIndex("Discounts.DiscountAccount", new[] { "SlaveID" });
            DropIndex("Discounts.DiscountAccount", new[] { "MasterID" });
            DropIndex("Discounts.DiscountAccount", new[] { "Hash" });
            DropIndex("Discounts.DiscountAccount", new[] { "Active" });
            DropIndex("Discounts.DiscountAccount", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountAccount", new[] { "CreatedDate" });
            DropIndex("Discounts.DiscountAccount", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountAccount", new[] { "ID" });
            DropColumn("Discounts.Discount", "UsageLimitByCart");
            DropTable("Discounts.DiscountVendor");
            DropTable("Discounts.DiscountUser");
            DropTable("Discounts.DiscountUserRole");
            DropTable("Discounts.DiscountManufacturer");
            DropTable("Discounts.DiscountCountry");
            DropTable("Discounts.DiscountAccountType");
            DropTable("Discounts.DiscountAccount");
        }
    }
}
