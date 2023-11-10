// <copyright file="201709220043077_AllSalesContactsOptional.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201709220043077 all sales contacts optional class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AllSalesContactsOptional : DbMigration
    {
        public override void Up()
        {
            DropIndex("Purchasing.PurchaseOrder", new[] { "BillingContactID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "ShippingContactID" });
            DropIndex("Ordering.SalesOrder", new[] { "BillingContactID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "BillingContactID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "ShippingContactID" });
            DropIndex("Quoting.SalesQuote", new[] { "BillingContactID" });
            DropIndex("Quoting.SalesQuote", new[] { "ShippingContactID" });
            DropIndex("Returning.SalesReturn", new[] { "BillingContactID" });
            AlterColumn("Purchasing.PurchaseOrder", "BillingContactID", c => c.Int());
            AlterColumn("Purchasing.PurchaseOrder", "ShippingContactID", c => c.Int());
            AlterColumn("Ordering.SalesOrder", "BillingContactID", c => c.Int());
            AlterColumn("Invoicing.SalesInvoice", "BillingContactID", c => c.Int());
            AlterColumn("Invoicing.SalesInvoice", "ShippingContactID", c => c.Int());
            AlterColumn("Quoting.SalesQuote", "BillingContactID", c => c.Int());
            AlterColumn("Quoting.SalesQuote", "ShippingContactID", c => c.Int());
            AlterColumn("Returning.SalesReturn", "BillingContactID", c => c.Int());
            CreateIndex("Purchasing.PurchaseOrder", "BillingContactID");
            CreateIndex("Purchasing.PurchaseOrder", "ShippingContactID");
            CreateIndex("Ordering.SalesOrder", "BillingContactID");
            CreateIndex("Invoicing.SalesInvoice", "BillingContactID");
            CreateIndex("Invoicing.SalesInvoice", "ShippingContactID");
            CreateIndex("Quoting.SalesQuote", "BillingContactID");
            CreateIndex("Quoting.SalesQuote", "ShippingContactID");
            CreateIndex("Returning.SalesReturn", "BillingContactID");
        }

        public override void Down()
        {
            DropIndex("Returning.SalesReturn", new[] { "BillingContactID" });
            DropIndex("Quoting.SalesQuote", new[] { "ShippingContactID" });
            DropIndex("Quoting.SalesQuote", new[] { "BillingContactID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "ShippingContactID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "BillingContactID" });
            DropIndex("Ordering.SalesOrder", new[] { "BillingContactID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "ShippingContactID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "BillingContactID" });
            AlterColumn("Returning.SalesReturn", "BillingContactID", c => c.Int(nullable: false));
            AlterColumn("Quoting.SalesQuote", "ShippingContactID", c => c.Int(nullable: false));
            AlterColumn("Quoting.SalesQuote", "BillingContactID", c => c.Int(nullable: false));
            AlterColumn("Invoicing.SalesInvoice", "ShippingContactID", c => c.Int(nullable: false));
            AlterColumn("Invoicing.SalesInvoice", "BillingContactID", c => c.Int(nullable: false));
            AlterColumn("Ordering.SalesOrder", "BillingContactID", c => c.Int(nullable: false));
            AlterColumn("Purchasing.PurchaseOrder", "ShippingContactID", c => c.Int(nullable: false));
            AlterColumn("Purchasing.PurchaseOrder", "BillingContactID", c => c.Int(nullable: false));
            CreateIndex("Returning.SalesReturn", "BillingContactID");
            CreateIndex("Quoting.SalesQuote", "ShippingContactID");
            CreateIndex("Quoting.SalesQuote", "BillingContactID");
            CreateIndex("Invoicing.SalesInvoice", "ShippingContactID");
            CreateIndex("Invoicing.SalesInvoice", "BillingContactID");
            CreateIndex("Ordering.SalesOrder", "BillingContactID");
            CreateIndex("Purchasing.PurchaseOrder", "ShippingContactID");
            CreateIndex("Purchasing.PurchaseOrder", "BillingContactID");
        }
    }
}
