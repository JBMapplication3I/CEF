// <copyright file="201903020249227_PriceRuleCountries.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201903020249227 price rule countries class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PriceRuleCountries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Pricing.PriceRuleCountry",
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
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Country", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.PriceRuleID)
                .Index(t => t.CountryID);
        }

        public override void Down()
        {
            DropForeignKey("Pricing.PriceRuleCountry", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleCountry", "CountryID", "Geography.Country");
            DropIndex("Pricing.PriceRuleCountry", new[] { "CountryID" });
            DropIndex("Pricing.PriceRuleCountry", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleCountry", new[] { "Hash" });
            DropIndex("Pricing.PriceRuleCountry", new[] { "Active" });
            DropIndex("Pricing.PriceRuleCountry", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleCountry", new[] { "CreatedDate" });
            DropIndex("Pricing.PriceRuleCountry", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleCountry", new[] { "ID" });
            DropTable("Pricing.PriceRuleCountry");
        }
    }
}
