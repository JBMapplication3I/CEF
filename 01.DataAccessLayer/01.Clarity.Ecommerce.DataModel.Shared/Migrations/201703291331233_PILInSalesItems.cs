// <copyright file="201703291331233_PILInSalesItems.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201703291331233 pil in sales items class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PILInSalesItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("Shopping.CartItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID", c => c.Int());
            CreateIndex("Shopping.CartItem", "ProductInventoryLocationSectionID");
            CreateIndex("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID");
            CreateIndex("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID");
            CreateIndex("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID");
            CreateIndex("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID");
            CreateIndex("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID");
            AddForeignKey("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Shopping.CartItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Shopping.CartItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Shopping.CartItem", new[] { "ProductInventoryLocationSectionID" });
            DropColumn("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID");
            DropColumn("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID");
            DropColumn("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID");
            DropColumn("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID");
            DropColumn("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID");
            DropColumn("Shopping.CartItem", "ProductInventoryLocationSectionID");
        }
    }
}
