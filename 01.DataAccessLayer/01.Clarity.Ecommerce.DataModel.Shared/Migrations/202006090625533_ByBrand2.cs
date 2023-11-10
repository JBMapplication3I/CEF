// <copyright file="202006090625533_ByBrand2.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202006090625533 by brand 2 class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ByBrand2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Purchasing.PurchaseOrder", "BrandID", c => c.Int());
            AddColumn("Ordering.SalesOrder", "BrandID", c => c.Int());
            AddColumn("Invoicing.SalesInvoice", "BrandID", c => c.Int());
            AddColumn("Shopping.Cart", "BrandID", c => c.Int());
            AddColumn("Messaging.Conversation", "BrandID", c => c.Int());
            AddColumn("Messaging.Message", "BrandID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "BrandID", c => c.Int());
            AddColumn("Returning.SalesReturn", "BrandID", c => c.Int());
            AddColumn("Returning.SalesReturnItemTarget", "BrandProductID", c => c.Int());
            AddColumn("Payments.Payment", "BrandID", c => c.Int());
            AddColumn("Sampling.SampleRequest", "BrandID", c => c.Int());
            AddColumn("Sampling.SampleRequestItemTarget", "BrandProductID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItemTarget", "BrandProductID", c => c.Int());
            AddColumn("Tracking.CampaignType", "BrandID", c => c.Int());
            AddColumn("Products.ProductType", "BrandID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItemTarget", "BrandProductID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItemTarget", "BrandProductID", c => c.Int());
            AddColumn("Ordering.SalesOrderItemTarget", "BrandProductID", c => c.Int());
            AddColumn("Shopping.CartType", "BrandID", c => c.Int());
            AddColumn("Shopping.CartItemTarget", "BrandProductID", c => c.Int());
            AddColumn("Products.ProductAssociation", "BrandID", c => c.Int());
            AddColumn("Products.ProductPricePoint", "BrandID", c => c.Int());
            AddColumn("System.SystemLog", "BrandID", c => c.Int());
            AddColumn("System.Setting", "BrandID", c => c.Int());
            CreateIndex("Purchasing.PurchaseOrder", "BrandID");
            CreateIndex("Ordering.SalesOrder", "BrandID");
            CreateIndex("Invoicing.SalesInvoice", "BrandID");
            CreateIndex("Messaging.Conversation", "BrandID");
            CreateIndex("Messaging.Message", "BrandID");
            CreateIndex("Shopping.Cart", "BrandID");
            CreateIndex("Quoting.SalesQuote", "BrandID");
            CreateIndex("Returning.SalesReturn", "BrandID");
            CreateIndex("Returning.SalesReturnItemTarget", "BrandProductID");
            CreateIndex("Payments.Payment", "BrandID");
            CreateIndex("Quoting.SalesQuoteItemTarget", "BrandProductID");
            CreateIndex("Sampling.SampleRequest", "BrandID");
            CreateIndex("Sampling.SampleRequestItemTarget", "BrandProductID");
            CreateIndex("Shopping.CartType", "BrandID");
            CreateIndex("Purchasing.PurchaseOrderItemTarget", "BrandProductID");
            CreateIndex("Invoicing.SalesInvoiceItemTarget", "BrandProductID");
            CreateIndex("Ordering.SalesOrderItemTarget", "BrandProductID");
            CreateIndex("Tracking.CampaignType", "BrandID");
            CreateIndex("Products.ProductType", "BrandID");
            CreateIndex("Shopping.CartItemTarget", "BrandProductID");
            CreateIndex("Products.ProductAssociation", "BrandID");
            CreateIndex("Products.ProductPricePoint", "BrandID");
            CreateIndex("System.SystemLog", "BrandID");
            CreateIndex("System.Setting", "BrandID");
            AddForeignKey("Messaging.Conversation", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Messaging.Message", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Shopping.Cart", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Quoting.SalesQuote", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Returning.SalesReturn", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Returning.SalesReturnItemTarget", "BrandProductID", "Brands.BrandProduct", "ID");
            AddForeignKey("Payments.Payment", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Quoting.SalesQuoteItemTarget", "BrandProductID", "Brands.BrandProduct", "ID");
            AddForeignKey("Sampling.SampleRequest", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Sampling.SampleRequestItemTarget", "BrandProductID", "Brands.BrandProduct", "ID");
            AddForeignKey("Shopping.CartType", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItemTarget", "BrandProductID", "Brands.BrandProduct", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItemTarget", "BrandProductID", "Brands.BrandProduct", "ID");
            AddForeignKey("Ordering.SalesOrderItemTarget", "BrandProductID", "Brands.BrandProduct", "ID");
            AddForeignKey("Tracking.CampaignType", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Products.ProductType", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Shopping.CartItemTarget", "BrandProductID", "Brands.BrandProduct", "ID");
            AddForeignKey("Products.ProductAssociation", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Products.ProductPricePoint", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Invoicing.SalesInvoice", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Ordering.SalesOrder", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Purchasing.PurchaseOrder", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("System.SystemLog", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("System.Setting", "BrandID", "Brands.Brand", "ID");
        }

        public override void Down()
        {
            DropForeignKey("System.Setting", "BrandID", "Brands.Brand");
            DropForeignKey("System.SystemLog", "BrandID", "Brands.Brand");
            DropForeignKey("Purchasing.PurchaseOrder", "BrandID", "Brands.Brand");
            DropForeignKey("Ordering.SalesOrder", "BrandID", "Brands.Brand");
            DropForeignKey("Invoicing.SalesInvoice", "BrandID", "Brands.Brand");
            DropForeignKey("Products.ProductPricePoint", "BrandID", "Brands.Brand");
            DropForeignKey("Products.ProductAssociation", "BrandID", "Brands.Brand");
            DropForeignKey("Shopping.CartItemTarget", "BrandProductID", "Brands.BrandProduct");
            DropForeignKey("Products.ProductType", "BrandID", "Brands.Brand");
            DropForeignKey("Tracking.CampaignType", "BrandID", "Brands.Brand");
            DropForeignKey("Ordering.SalesOrderItemTarget", "BrandProductID", "Brands.BrandProduct");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "BrandProductID", "Brands.BrandProduct");
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "BrandProductID", "Brands.BrandProduct");
            DropForeignKey("Shopping.CartType", "BrandID", "Brands.Brand");
            DropForeignKey("Sampling.SampleRequestItemTarget", "BrandProductID", "Brands.BrandProduct");
            DropForeignKey("Sampling.SampleRequest", "BrandID", "Brands.Brand");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "BrandProductID", "Brands.BrandProduct");
            DropForeignKey("Payments.Payment", "BrandID", "Brands.Brand");
            DropForeignKey("Returning.SalesReturnItemTarget", "BrandProductID", "Brands.BrandProduct");
            DropForeignKey("Returning.SalesReturn", "BrandID", "Brands.Brand");
            DropForeignKey("Quoting.SalesQuote", "BrandID", "Brands.Brand");
            DropForeignKey("Shopping.Cart", "BrandID", "Brands.Brand");
            DropForeignKey("Messaging.Message", "BrandID", "Brands.Brand");
            DropForeignKey("Messaging.Conversation", "BrandID", "Brands.Brand");
            DropIndex("System.Setting", new[] { "BrandID" });
            DropIndex("System.SystemLog", new[] { "BrandID" });
            DropIndex("Products.ProductPricePoint", new[] { "BrandID" });
            DropIndex("Products.ProductAssociation", new[] { "BrandID" });
            DropIndex("Shopping.CartItemTarget", new[] { "BrandProductID" });
            DropIndex("Products.ProductType", new[] { "BrandID" });
            DropIndex("Tracking.CampaignType", new[] { "BrandID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "BrandProductID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "BrandProductID" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "BrandProductID" });
            DropIndex("Shopping.CartType", new[] { "BrandID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "BrandProductID" });
            DropIndex("Sampling.SampleRequest", new[] { "BrandID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "BrandProductID" });
            DropIndex("Payments.Payment", new[] { "BrandID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "BrandProductID" });
            DropIndex("Returning.SalesReturn", new[] { "BrandID" });
            DropIndex("Quoting.SalesQuote", new[] { "BrandID" });
            DropIndex("Shopping.Cart", new[] { "BrandID" });
            DropIndex("Messaging.Message", new[] { "BrandID" });
            DropIndex("Messaging.Conversation", new[] { "BrandID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "BrandID" });
            DropIndex("Ordering.SalesOrder", new[] { "BrandID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "BrandID" });
            DropColumn("System.Setting", "BrandID");
            DropColumn("System.SystemLog", "BrandID");
            DropColumn("Products.ProductPricePoint", "BrandID");
            DropColumn("Products.ProductAssociation", "BrandID");
            DropColumn("Shopping.CartItemTarget", "BrandProductID");
            DropColumn("Shopping.CartType", "BrandID");
            DropColumn("Ordering.SalesOrderItemTarget", "BrandProductID");
            DropColumn("Invoicing.SalesInvoiceItemTarget", "BrandProductID");
            DropColumn("Purchasing.PurchaseOrderItemTarget", "BrandProductID");
            DropColumn("Products.ProductType", "BrandID");
            DropColumn("Tracking.CampaignType", "BrandID");
            DropColumn("Quoting.SalesQuoteItemTarget", "BrandProductID");
            DropColumn("Sampling.SampleRequestItemTarget", "BrandProductID");
            DropColumn("Sampling.SampleRequest", "BrandID");
            DropColumn("Payments.Payment", "BrandID");
            DropColumn("Returning.SalesReturnItemTarget", "BrandProductID");
            DropColumn("Returning.SalesReturn", "BrandID");
            DropColumn("Quoting.SalesQuote", "BrandID");
            DropColumn("Messaging.Message", "BrandID");
            DropColumn("Messaging.Conversation", "BrandID");
            DropColumn("Shopping.Cart", "BrandID");
            DropColumn("Invoicing.SalesInvoice", "BrandID");
            DropColumn("Ordering.SalesOrder", "BrandID");
            DropColumn("Purchasing.PurchaseOrder", "BrandID");
        }
    }
}
