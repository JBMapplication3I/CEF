// <copyright file="201810201539534_TargetsByVendor.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201810201539534 targets by vendor class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TargetsByVendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("Returning.SalesReturnItemTarget", "OriginVendorProductID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItemTarget", "OriginVendorProductID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItemTarget", "OriginVendorProductID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItemTarget", "OriginVendorProductID", c => c.Int());
            AddColumn("Ordering.SalesOrderItemTarget", "OriginVendorProductID", c => c.Int());
            AddColumn("Sampling.SampleRequestItemTarget", "OriginVendorProductID", c => c.Int());
            AddColumn("Shopping.CartItemTarget", "OriginVendorProductID", c => c.Int());
            CreateIndex("Returning.SalesReturnItemTarget", "OriginVendorProductID");
            CreateIndex("Quoting.SalesQuoteItemTarget", "OriginVendorProductID");
            CreateIndex("Purchasing.PurchaseOrderItemTarget", "OriginVendorProductID");
            CreateIndex("Invoicing.SalesInvoiceItemTarget", "OriginVendorProductID");
            CreateIndex("Ordering.SalesOrderItemTarget", "OriginVendorProductID");
            CreateIndex("Sampling.SampleRequestItemTarget", "OriginVendorProductID");
            CreateIndex("Shopping.CartItemTarget", "OriginVendorProductID");
            AddForeignKey("Returning.SalesReturnItemTarget", "OriginVendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Quoting.SalesQuoteItemTarget", "OriginVendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItemTarget", "OriginVendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItemTarget", "OriginVendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Ordering.SalesOrderItemTarget", "OriginVendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Sampling.SampleRequestItemTarget", "OriginVendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Shopping.CartItemTarget", "OriginVendorProductID", "Vendors.VendorProduct", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Shopping.CartItemTarget", "OriginVendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Sampling.SampleRequestItemTarget", "OriginVendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Ordering.SalesOrderItemTarget", "OriginVendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "OriginVendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "OriginVendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "OriginVendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Returning.SalesReturnItemTarget", "OriginVendorProductID", "Vendors.VendorProduct");
            DropIndex("Shopping.CartItemTarget", new[] { "OriginVendorProductID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "OriginVendorProductID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "OriginVendorProductID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "OriginVendorProductID" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "OriginVendorProductID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "OriginVendorProductID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "OriginVendorProductID" });
            DropColumn("Shopping.CartItemTarget", "OriginVendorProductID");
            DropColumn("Sampling.SampleRequestItemTarget", "OriginVendorProductID");
            DropColumn("Ordering.SalesOrderItemTarget", "OriginVendorProductID");
            DropColumn("Invoicing.SalesInvoiceItemTarget", "OriginVendorProductID");
            DropColumn("Purchasing.PurchaseOrderItemTarget", "OriginVendorProductID");
            DropColumn("Quoting.SalesQuoteItemTarget", "OriginVendorProductID");
            DropColumn("Returning.SalesReturnItemTarget", "OriginVendorProductID");
        }
    }
}
