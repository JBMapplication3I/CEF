// <copyright file="202202040157516_AddPriceRuleFranchise.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202202040157516 add price rule franchise class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPriceRuleFranchise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Auctions.LotGroup",
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
                "Pricing.PriceRuleFranchise",
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
                .ForeignKey("Pricing.PriceRule", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Franchises.Franchise", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);
        }

        public override void Down()
        {
            DropForeignKey("Pricing.PriceRuleFranchise", "SlaveID", "Franchises.Franchise");
            DropForeignKey("Pricing.PriceRuleFranchise", "MasterID", "Pricing.PriceRule");
            DropIndex("Pricing.PriceRuleFranchise", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleFranchise", new[] { "Active" });
            DropIndex("Pricing.PriceRuleFranchise", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleFranchise", new[] { "CreatedDate" });
            DropIndex("Pricing.PriceRuleFranchise", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleFranchise", new[] { "SlaveID" });
            DropIndex("Pricing.PriceRuleFranchise", new[] { "MasterID" });
            DropIndex("Pricing.PriceRuleFranchise", new[] { "ID" });
            DropIndex("Auctions.LotGroup", new[] { "Hash" });
            DropIndex("Auctions.LotGroup", new[] { "Active" });
            DropIndex("Auctions.LotGroup", new[] { "UpdatedDate" });
            DropIndex("Auctions.LotGroup", new[] { "CreatedDate" });
            DropIndex("Auctions.LotGroup", new[] { "CustomKey" });
            DropIndex("Auctions.LotGroup", new[] { "Name" });
            DropIndex("Auctions.LotGroup", new[] { "SortOrder" });
            DropIndex("Auctions.LotGroup", new[] { "DisplayName" });
            DropIndex("Auctions.LotGroup", new[] { "ID" });
            DropTable("Pricing.PriceRuleFranchise");
            DropTable("Auctions.LotGroup");
        }
    }
}
