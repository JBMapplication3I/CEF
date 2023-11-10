// <copyright file="201806130212123_ChangeStoreToStoreProductForTargets.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201806130212123 change store to store product for targets class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeStoreToStoreProductForTargets : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Returning.SalesReturnItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Ordering.SalesOrderItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Sampling.SampleRequestItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Shopping.CartItemTarget", "OriginStoreID", "Stores.Store");
            DropIndex("Returning.SalesReturnItemTarget", new[] { "OriginStoreID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "OriginStoreID" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "OriginStoreID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "OriginStoreID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "OriginStoreID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "OriginStoreID" });
            DropIndex("Shopping.CartItemTarget", new[] { "OriginStoreID" });
            AddColumn("Returning.SalesReturnItemTarget", "OriginStoreProductID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItemTarget", "OriginStoreProductID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItemTarget", "OriginStoreProductID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItemTarget", "OriginStoreProductID", c => c.Int());
            AddColumn("Ordering.SalesOrderItemTarget", "OriginStoreProductID", c => c.Int());
            AddColumn("Sampling.SampleRequestItemTarget", "OriginStoreProductID", c => c.Int());
            AddColumn("Shopping.CartItemTarget", "OriginStoreProductID", c => c.Int());
            CreateIndex("Returning.SalesReturnItemTarget", "OriginStoreProductID");
            CreateIndex("Quoting.SalesQuoteItemTarget", "OriginStoreProductID");
            CreateIndex("Purchasing.PurchaseOrderItemTarget", "OriginStoreProductID");
            CreateIndex("Invoicing.SalesInvoiceItemTarget", "OriginStoreProductID");
            CreateIndex("Ordering.SalesOrderItemTarget", "OriginStoreProductID");
            CreateIndex("Sampling.SampleRequestItemTarget", "OriginStoreProductID");
            CreateIndex("Shopping.CartItemTarget", "OriginStoreProductID");
            AddForeignKey("Returning.SalesReturnItemTarget", "OriginStoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Quoting.SalesQuoteItemTarget", "OriginStoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItemTarget", "OriginStoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItemTarget", "OriginStoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Ordering.SalesOrderItemTarget", "OriginStoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Sampling.SampleRequestItemTarget", "OriginStoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Shopping.CartItemTarget", "OriginStoreProductID", "Stores.StoreProduct", "ID");
            DropColumn("Returning.SalesReturnItemTarget", "OriginStoreID");
            DropColumn("Quoting.SalesQuoteItemTarget", "OriginStoreID");
            DropColumn("Purchasing.PurchaseOrderItemTarget", "OriginStoreID");
            DropColumn("Invoicing.SalesInvoiceItemTarget", "OriginStoreID");
            DropColumn("Ordering.SalesOrderItemTarget", "OriginStoreID");
            DropColumn("Sampling.SampleRequestItemTarget", "OriginStoreID");
            DropColumn("Shopping.CartItemTarget", "OriginStoreID");
        }

        public override void Down()
        {
            AddColumn("Shopping.CartItemTarget", "OriginStoreID", c => c.Int());
            AddColumn("Sampling.SampleRequestItemTarget", "OriginStoreID", c => c.Int());
            AddColumn("Ordering.SalesOrderItemTarget", "OriginStoreID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItemTarget", "OriginStoreID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItemTarget", "OriginStoreID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItemTarget", "OriginStoreID", c => c.Int());
            AddColumn("Returning.SalesReturnItemTarget", "OriginStoreID", c => c.Int());
            DropForeignKey("Shopping.CartItemTarget", "OriginStoreProductID", "Stores.StoreProduct");
            DropForeignKey("Sampling.SampleRequestItemTarget", "OriginStoreProductID", "Stores.StoreProduct");
            DropForeignKey("Ordering.SalesOrderItemTarget", "OriginStoreProductID", "Stores.StoreProduct");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "OriginStoreProductID", "Stores.StoreProduct");
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "OriginStoreProductID", "Stores.StoreProduct");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "OriginStoreProductID", "Stores.StoreProduct");
            DropForeignKey("Returning.SalesReturnItemTarget", "OriginStoreProductID", "Stores.StoreProduct");
            DropIndex("Shopping.CartItemTarget", new[] { "OriginStoreProductID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "OriginStoreProductID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "OriginStoreProductID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "OriginStoreProductID" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "OriginStoreProductID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "OriginStoreProductID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "OriginStoreProductID" });
            DropColumn("Shopping.CartItemTarget", "OriginStoreProductID");
            DropColumn("Sampling.SampleRequestItemTarget", "OriginStoreProductID");
            DropColumn("Ordering.SalesOrderItemTarget", "OriginStoreProductID");
            DropColumn("Invoicing.SalesInvoiceItemTarget", "OriginStoreProductID");
            DropColumn("Purchasing.PurchaseOrderItemTarget", "OriginStoreProductID");
            DropColumn("Quoting.SalesQuoteItemTarget", "OriginStoreProductID");
            DropColumn("Returning.SalesReturnItemTarget", "OriginStoreProductID");
            CreateIndex("Shopping.CartItemTarget", "OriginStoreID");
            CreateIndex("Sampling.SampleRequestItemTarget", "OriginStoreID");
            CreateIndex("Ordering.SalesOrderItemTarget", "OriginStoreID");
            CreateIndex("Invoicing.SalesInvoiceItemTarget", "OriginStoreID");
            CreateIndex("Purchasing.PurchaseOrderItemTarget", "OriginStoreID");
            CreateIndex("Quoting.SalesQuoteItemTarget", "OriginStoreID");
            CreateIndex("Returning.SalesReturnItemTarget", "OriginStoreID");
            AddForeignKey("Shopping.CartItemTarget", "OriginStoreID", "Stores.Store", "ID");
            AddForeignKey("Sampling.SampleRequestItemTarget", "OriginStoreID", "Stores.Store", "ID");
            AddForeignKey("Ordering.SalesOrderItemTarget", "OriginStoreID", "Stores.Store", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItemTarget", "OriginStoreID", "Stores.Store", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItemTarget", "OriginStoreID", "Stores.Store", "ID");
            AddForeignKey("Quoting.SalesQuoteItemTarget", "OriginStoreID", "Stores.Store", "ID");
            AddForeignKey("Returning.SalesReturnItemTarget", "OriginStoreID", "Stores.Store", "ID");
        }
    }
}
