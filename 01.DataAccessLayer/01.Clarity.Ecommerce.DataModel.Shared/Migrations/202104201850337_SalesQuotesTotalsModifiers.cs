// <copyright file="202104201850337_SalesQuotesTotalsModifiers.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202104201850337 sales quotes totals modifiers class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SalesQuotesTotalsModifiers : DbMigration
    {
        public override void Up()
        {
            AddColumn("Quoting.SalesQuote", "SubtotalShippingModifier", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Quoting.SalesQuote", "SubtotalShippingModifierMode", c => c.Int());
            AddColumn("Quoting.SalesQuote", "SubtotalTaxesModifier", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Quoting.SalesQuote", "SubtotalTaxesModifierMode", c => c.Int());
            AddColumn("Quoting.SalesQuote", "SubtotalFeesModifier", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Quoting.SalesQuote", "SubtotalFeesModifierMode", c => c.Int());
            AddColumn("Quoting.SalesQuote", "SubtotalHandlingModifier", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Quoting.SalesQuote", "SubtotalHandlingModifierMode", c => c.Int());
            AddColumn("Quoting.SalesQuote", "SubtotalDiscountsModifier", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Quoting.SalesQuote", "SubtotalDiscountsModifierMode", c => c.Int());
            AddColumn("Quoting.SalesQuote", "RequestedShipDate", c => c.DateTime());
            AddColumn("Quoting.SalesQuoteItem", "UnitSoldPriceModifier", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Quoting.SalesQuoteItem", "UnitSoldPriceModifierMode", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("Quoting.SalesQuoteItem", "UnitSoldPriceModifierMode");
            DropColumn("Quoting.SalesQuoteItem", "UnitSoldPriceModifier");
            DropColumn("Quoting.SalesQuote", "RequestedShipDate");
            DropColumn("Quoting.SalesQuote", "SubtotalDiscountsModifierMode");
            DropColumn("Quoting.SalesQuote", "SubtotalDiscountsModifier");
            DropColumn("Quoting.SalesQuote", "SubtotalHandlingModifierMode");
            DropColumn("Quoting.SalesQuote", "SubtotalHandlingModifier");
            DropColumn("Quoting.SalesQuote", "SubtotalFeesModifierMode");
            DropColumn("Quoting.SalesQuote", "SubtotalFeesModifier");
            DropColumn("Quoting.SalesQuote", "SubtotalTaxesModifierMode");
            DropColumn("Quoting.SalesQuote", "SubtotalTaxesModifier");
            DropColumn("Quoting.SalesQuote", "SubtotalShippingModifierMode");
            DropColumn("Quoting.SalesQuote", "SubtotalShippingModifier");
        }
    }
}
