// <copyright file="201808020000321_PricePointsAndRulesProperties.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201808020000321 price points and rules properties class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PricePointsAndRulesProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.ProductPricePoint", "From", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Products.ProductPricePoint", "To", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Pricing.PriceRule", "MinQuantity", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Pricing.PriceRule", "MaxQuantity", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("Pricing.PriceRule", "PriceAdjustment", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("Pricing.PriceRule", "Currency", c => c.String(unicode: false));
            CreateIndex("Products.ProductPricePoint", "CreatedDate");
            CreateIndex("Pricing.PriceRule", "CreatedDate");
        }

        public override void Down()
        {
            DropIndex("Pricing.PriceRule", new[] { "CreatedDate" });
            DropIndex("Products.ProductPricePoint", new[] { "CreatedDate" });
            AlterColumn("Pricing.PriceRule", "Currency", c => c.String());
            AlterColumn("Pricing.PriceRule", "PriceAdjustment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("Pricing.PriceRule", "MaxQuantity");
            DropColumn("Pricing.PriceRule", "MinQuantity");
            DropColumn("Products.ProductPricePoint", "To");
            DropColumn("Products.ProductPricePoint", "From");
        }
    }
}
