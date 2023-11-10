// <copyright file="201911042200421_PreSales.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201911042200421 pre sales class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PreSales : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.Product", "AllowPreSale", c => c.Boolean(nullable: false));
            AddColumn("Products.Product", "PreSellEndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Products.Product", "StockQuantityPreSold", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "MaximumBackOrderPurchaseQuantity", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "MaximumBackOrderPurchaseQuantityIfPastPurchased", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "MaximumBackOrderPurchaseQuantityGlobal", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "MaximumPrePurchaseQuantity", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "MaximumPrePurchaseQuantityIfPastPurchased", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "MaximumPrePurchaseQuantityGlobal", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Shopping.CartItem", "QuantityPreSold", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.ProductInventoryLocationSection", "QuantityPreSold", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Ordering.SalesOrderItem", "QuantityPreSold", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Returning.SalesReturnItem", "QuantityPreSold", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Quoting.SalesQuoteItem", "QuantityPreSold", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Sampling.SampleRequestItem", "QuantityPreSold", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Purchasing.PurchaseOrderItem", "QuantityPreSold", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Invoicing.SalesInvoiceItem", "QuantityPreSold", c => c.Decimal(precision: 18, scale: 4));
        }

        public override void Down()
        {
            DropColumn("Invoicing.SalesInvoiceItem", "QuantityPreSold");
            DropColumn("Purchasing.PurchaseOrderItem", "QuantityPreSold");
            DropColumn("Sampling.SampleRequestItem", "QuantityPreSold");
            DropColumn("Quoting.SalesQuoteItem", "QuantityPreSold");
            DropColumn("Returning.SalesReturnItem", "QuantityPreSold");
            DropColumn("Ordering.SalesOrderItem", "QuantityPreSold");
            DropColumn("Products.ProductInventoryLocationSection", "QuantityPreSold");
            DropColumn("Shopping.CartItem", "QuantityPreSold");
            DropColumn("Products.Product", "MaximumPrePurchaseQuantityGlobal");
            DropColumn("Products.Product", "MaximumPrePurchaseQuantityIfPastPurchased");
            DropColumn("Products.Product", "MaximumPrePurchaseQuantity");
            DropColumn("Products.Product", "MaximumBackOrderPurchaseQuantityGlobal");
            DropColumn("Products.Product", "MaximumBackOrderPurchaseQuantityIfPastPurchased");
            DropColumn("Products.Product", "MaximumBackOrderPurchaseQuantity");
            DropColumn("Products.Product", "StockQuantityPreSold");
            DropColumn("Products.Product", "PreSellEndDate");
            DropColumn("Products.Product", "AllowPreSale");
        }
    }
}
