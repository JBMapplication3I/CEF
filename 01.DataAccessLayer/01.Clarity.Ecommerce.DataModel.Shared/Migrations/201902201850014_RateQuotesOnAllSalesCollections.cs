// <copyright file="201902201850014_RateQuotesOnAllSalesCollections.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201902201850014 rate quotes on all sales collections class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RateQuotesOnAllSalesCollections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Quoting.SalesQuote", "RateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Sampling.SampleRequest", "RateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Invoicing.SalesInvoice", "RateQuoteID", "Shipping.RateQuote");
            DropIndex("Purchasing.PurchaseOrder", new[] { "TypeID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "UserID" });
            DropIndex("Ordering.SalesOrder", new[] { "TypeID" });
            DropIndex("Ordering.SalesOrder", new[] { "UserID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "TypeID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "UserID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "RateQuoteID" });
            DropIndex("Shopping.Cart", "Unq_BySessionTypeUserActive");
            DropIndex("Quoting.SalesQuote", new[] { "TypeID" });
            DropIndex("Quoting.SalesQuote", new[] { "UserID" });
            DropIndex("Quoting.SalesQuote", new[] { "RateQuoteID" });
            DropIndex("Returning.SalesReturn", new[] { "TypeID" });
            DropIndex("Returning.SalesReturn", new[] { "UserID" });
            DropIndex("Sampling.SampleRequest", new[] { "TypeID" });
            DropIndex("Sampling.SampleRequest", new[] { "UserID" });
            DropIndex("Sampling.SampleRequest", new[] { "RateQuoteID" });
            AddColumn("Products.Product", "ShippingLeadTimeIsCalendarDays", c => c.Boolean(nullable: false));
            AddColumn("Products.Product", "ShippingLeadTimeDays", c => c.Int());
            AddColumn("Shipping.RateQuote", "SampleRequestID", c => c.Int());
            AddColumn("Shipping.RateQuote", "SalesQuoteID", c => c.Int());
            AddColumn("Shipping.RateQuote", "SalesOrderID", c => c.Int());
            AddColumn("Shipping.RateQuote", "PurchaseOrderID", c => c.Int());
            AddColumn("Shipping.RateQuote", "SalesInvoiceID", c => c.Int());
            AddColumn("Shipping.RateQuote", "SalesReturnID", c => c.Int());
            CreateIndex("Purchasing.PurchaseOrder", "TypeID");
            CreateIndex("Purchasing.PurchaseOrder", "UserID");
            CreateIndex("Ordering.SalesOrder", "TypeID");
            CreateIndex("Ordering.SalesOrder", "UserID");
            CreateIndex("Invoicing.SalesInvoice", "TypeID");
            CreateIndex("Invoicing.SalesInvoice", "UserID");
            CreateIndex("Shopping.Cart", new[] { "SessionID", "TypeID", "UserID" }, unique: true, name: "Unq_BySessionTypeUserActive");
            CreateIndex("Quoting.SalesQuote", "TypeID");
            CreateIndex("Quoting.SalesQuote", "UserID");
            CreateIndex("Shipping.RateQuote", "SampleRequestID");
            CreateIndex("Shipping.RateQuote", "SalesQuoteID");
            CreateIndex("Shipping.RateQuote", "SalesOrderID");
            CreateIndex("Shipping.RateQuote", "PurchaseOrderID");
            CreateIndex("Shipping.RateQuote", "SalesInvoiceID");
            CreateIndex("Shipping.RateQuote", "SalesReturnID");
            CreateIndex("Returning.SalesReturn", "TypeID");
            CreateIndex("Returning.SalesReturn", "UserID");
            CreateIndex("Sampling.SampleRequest", "TypeID");
            CreateIndex("Sampling.SampleRequest", "UserID");
            AddForeignKey("Shipping.RateQuote", "PurchaseOrderID", "Purchasing.PurchaseOrder", "ID");
            AddForeignKey("Shipping.RateQuote", "SalesInvoiceID", "Invoicing.SalesInvoice", "ID");
            AddForeignKey("Shipping.RateQuote", "SalesOrderID", "Ordering.SalesOrder", "ID");
            AddForeignKey("Shipping.RateQuote", "SalesQuoteID", "Quoting.SalesQuote", "ID");
            AddForeignKey("Shipping.RateQuote", "SalesReturnID", "Returning.SalesReturn", "ID");
            AddForeignKey("Shipping.RateQuote", "SampleRequestID", "Sampling.SampleRequest", "ID");
            DropColumn("Invoicing.SalesInvoice", "RateQuoteID");
            DropColumn("Quoting.SalesQuote", "RateQuoteID");
            DropColumn("Sampling.SampleRequest", "RateQuoteID");
        }

        public override void Down()
        {
            AddColumn("Sampling.SampleRequest", "RateQuoteID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "RateQuoteID", c => c.Int());
            AddColumn("Invoicing.SalesInvoice", "RateQuoteID", c => c.Int());
            DropForeignKey("Shipping.RateQuote", "SampleRequestID", "Sampling.SampleRequest");
            DropForeignKey("Shipping.RateQuote", "SalesReturnID", "Returning.SalesReturn");
            DropForeignKey("Shipping.RateQuote", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("Shipping.RateQuote", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Shipping.RateQuote", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Shipping.RateQuote", "PurchaseOrderID", "Purchasing.PurchaseOrder");
            DropIndex("Sampling.SampleRequest", new[] { "UserID" });
            DropIndex("Sampling.SampleRequest", new[] { "TypeID" });
            DropIndex("Returning.SalesReturn", new[] { "UserID" });
            DropIndex("Returning.SalesReturn", new[] { "TypeID" });
            DropIndex("Shipping.RateQuote", new[] { "SalesReturnID" });
            DropIndex("Shipping.RateQuote", new[] { "SalesInvoiceID" });
            DropIndex("Shipping.RateQuote", new[] { "PurchaseOrderID" });
            DropIndex("Shipping.RateQuote", new[] { "SalesOrderID" });
            DropIndex("Shipping.RateQuote", new[] { "SalesQuoteID" });
            DropIndex("Shipping.RateQuote", new[] { "SampleRequestID" });
            DropIndex("Quoting.SalesQuote", new[] { "UserID" });
            DropIndex("Quoting.SalesQuote", new[] { "TypeID" });
            DropIndex("Shopping.Cart", "Unq_BySessionTypeUserActive");
            DropIndex("Invoicing.SalesInvoice", new[] { "UserID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "TypeID" });
            DropIndex("Ordering.SalesOrder", new[] { "UserID" });
            DropIndex("Ordering.SalesOrder", new[] { "TypeID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "UserID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "TypeID" });
            DropColumn("Shipping.RateQuote", "SalesReturnID");
            DropColumn("Shipping.RateQuote", "SalesInvoiceID");
            DropColumn("Shipping.RateQuote", "PurchaseOrderID");
            DropColumn("Shipping.RateQuote", "SalesOrderID");
            DropColumn("Shipping.RateQuote", "SalesQuoteID");
            DropColumn("Shipping.RateQuote", "SampleRequestID");
            DropColumn("Products.Product", "ShippingLeadTimeDays");
            DropColumn("Products.Product", "ShippingLeadTimeIsCalendarDays");
            CreateIndex("Sampling.SampleRequest", "RateQuoteID");
            CreateIndex("Sampling.SampleRequest", "UserID");
            CreateIndex("Sampling.SampleRequest", "TypeID");
            CreateIndex("Returning.SalesReturn", "UserID");
            CreateIndex("Returning.SalesReturn", "TypeID");
            CreateIndex("Quoting.SalesQuote", "RateQuoteID");
            CreateIndex("Quoting.SalesQuote", "UserID");
            CreateIndex("Quoting.SalesQuote", "TypeID");
            CreateIndex("Shopping.Cart", new[] { "SessionID", "TypeID", "UserID", "Active" }, unique: true, name: "Unq_BySessionTypeUserActive");
            CreateIndex("Invoicing.SalesInvoice", "RateQuoteID");
            CreateIndex("Invoicing.SalesInvoice", "UserID");
            CreateIndex("Invoicing.SalesInvoice", "TypeID");
            CreateIndex("Ordering.SalesOrder", "UserID");
            CreateIndex("Ordering.SalesOrder", "TypeID");
            CreateIndex("Purchasing.PurchaseOrder", "UserID");
            CreateIndex("Purchasing.PurchaseOrder", "TypeID");
            AddForeignKey("Invoicing.SalesInvoice", "RateQuoteID", "Shipping.RateQuote", "ID");
            AddForeignKey("Sampling.SampleRequest", "RateQuoteID", "Shipping.RateQuote", "ID");
            AddForeignKey("Quoting.SalesQuote", "RateQuoteID", "Shipping.RateQuote", "ID");
        }
    }
}
