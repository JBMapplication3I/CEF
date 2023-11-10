// <copyright file="201702121904460_ProductSalesAnalytics.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201702121904460 product sales analytics class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ProductSalesAnalytics : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.Product", "QuantityMasterPackPerLayer", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "QuantityMasterPackLayersPerPallet", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "QuantityPerLayer", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "QuantityLayersPerPallet", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "TotalPurchasedAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "TotalPurchasedAmountCurrencyID", c => c.Int());
            AddColumn("Products.Product", "TotalPurchasedQuantity", c => c.Decimal(precision: 18, scale: 4));
            CreateIndex("Products.Product", "TotalPurchasedAmountCurrencyID");
            AddForeignKey("Products.Product", "TotalPurchasedAmountCurrencyID", "Currencies.Currency", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Products.Product", "TotalPurchasedAmountCurrencyID", "Currencies.Currency");
            DropIndex("Products.Product", new[] { "TotalPurchasedAmountCurrencyID" });
            DropColumn("Products.Product", "TotalPurchasedQuantity");
            DropColumn("Products.Product", "TotalPurchasedAmountCurrencyID");
            DropColumn("Products.Product", "TotalPurchasedAmount");
            DropColumn("Products.Product", "QuantityLayersPerPallet");
            DropColumn("Products.Product", "QuantityPerLayer");
            DropColumn("Products.Product", "QuantityMasterPackLayersPerPallet");
            DropColumn("Products.Product", "QuantityMasterPackPerLayer");
        }
    }
}
