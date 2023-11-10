// <copyright file="201902042245342_PriceRulesOverhaul2019.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201902042245342 price rules overhaul 2019 class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PriceRulesOverhaul2019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Pricing.PriceRuleManufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        PriceRuleID = c.Int(nullable: false),
                        ManufacturerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Manufacturers.Manufacturer", t => t.ManufacturerID, cascadeDelete: true)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.PriceRuleID)
                .Index(t => t.ManufacturerID);

            CreateTable(
                "Pricing.PriceRuleStore",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        PriceRuleID = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.PriceRuleID)
                .Index(t => t.StoreID);

            CreateTable(
                "Pricing.PriceRuleUserRole",
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
                        PriceRuleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.PriceRuleID);

            RenameColumn(table: "Pricing.PriceRule", name: "IsHigherThan", newName: "IsMarkup");
            RenameColumn(table: "Pricing.PriceRule", name: "Currency", newName: "CustomCurrency");
            RenameColumn(table: "Pricing.PriceRuleProduct", name: "OverrideCost", newName: "OverrideBasePrice");
            AddColumn("Pricing.PriceRule", "IsOnlyForAnonymousUsers", c => c.Boolean(nullable: false));
            AddColumn("Pricing.PriceRule", "CurrencyID", c => c.Int());
            CreateIndex("Pricing.PriceRule", "CurrencyID");
            AddForeignKey("Pricing.PriceRule", "CurrencyID", "Currencies.Currency", "ID");
        }

        public override void Down()
        {
            RenameColumn(table: "Pricing.PriceRule", name: "IsMarkup", newName: "IsHigherThan");
            RenameColumn(table: "Pricing.PriceRule", name: "CustomCurrency", newName: "Currency");
            RenameColumn(table: "Pricing.PriceRuleProduct", name: "OverrideBasePrice", newName: "OverrideCost");
            DropForeignKey("Pricing.PriceRuleUserRole", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleStore", "StoreID", "Stores.Store");
            DropForeignKey("Pricing.PriceRuleStore", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleManufacturer", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleManufacturer", "ManufacturerID", "Manufacturers.Manufacturer");
            DropForeignKey("Pricing.PriceRule", "CurrencyID", "Currencies.Currency");
            DropIndex("Pricing.PriceRuleUserRole", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleUserRole", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleUserRole", new[] { "Active" });
            DropIndex("Pricing.PriceRuleUserRole", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleUserRole", new[] { "CreatedDate" });
            DropIndex("Pricing.PriceRuleUserRole", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleUserRole", new[] { "ID" });
            DropIndex("Pricing.PriceRuleStore", new[] { "StoreID" });
            DropIndex("Pricing.PriceRuleStore", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleStore", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleStore", new[] { "Active" });
            DropIndex("Pricing.PriceRuleStore", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleStore", new[] { "CreatedDate" });
            DropIndex("Pricing.PriceRuleStore", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleStore", new[] { "ID" });
            DropIndex("Pricing.PriceRuleManufacturer", new[] { "ManufacturerID" });
            DropIndex("Pricing.PriceRuleManufacturer", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleManufacturer", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleManufacturer", new[] { "Active" });
            DropIndex("Pricing.PriceRuleManufacturer", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleManufacturer", new[] { "CreatedDate" });
            DropIndex("Pricing.PriceRuleManufacturer", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleManufacturer", new[] { "ID" });
            DropIndex("Pricing.PriceRule", new[] { "CurrencyID" });
            DropColumn("Pricing.PriceRule", "CurrencyID");
            DropColumn("Pricing.PriceRule", "IsOnlyForAnonymousUsers");
            DropTable("Pricing.PriceRuleUserRole");
            DropTable("Pricing.PriceRuleStore");
            DropTable("Pricing.PriceRuleManufacturer");
        }
    }
}
