// <copyright file="201801082055564_RFQParent.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201801082055564 rfq parent class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RFQParent : DbMigration
    {
        public override void Up()
        {
            AddColumn("Purchasing.PurchaseOrder", "ParentID", c => c.Int());
            AddColumn("Invoicing.SalesInvoice", "ParentID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "ParentID", c => c.Int());
            AddColumn("Sampling.SampleRequest", "ParentID", c => c.Int());
            CreateIndex("Purchasing.PurchaseOrder", "ParentID");
            CreateIndex("Invoicing.SalesInvoice", "ParentID");
            CreateIndex("Quoting.SalesQuote", "ParentID");
            CreateIndex("Sampling.SampleRequest", "ParentID");
            AddForeignKey("Invoicing.SalesInvoice", "ParentID", "Invoicing.SalesInvoice", "ID");
            AddForeignKey("Quoting.SalesQuote", "ParentID", "Quoting.SalesQuote", "ID");
            AddForeignKey("Sampling.SampleRequest", "ParentID", "Sampling.SampleRequest", "ID");
            AddForeignKey("Purchasing.PurchaseOrder", "ParentID", "Purchasing.PurchaseOrder", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Purchasing.PurchaseOrder", "ParentID", "Purchasing.PurchaseOrder");
            DropForeignKey("Sampling.SampleRequest", "ParentID", "Sampling.SampleRequest");
            DropForeignKey("Quoting.SalesQuote", "ParentID", "Quoting.SalesQuote");
            DropForeignKey("Invoicing.SalesInvoice", "ParentID", "Invoicing.SalesInvoice");
            DropIndex("Sampling.SampleRequest", new[] { "ParentID" });
            DropIndex("Quoting.SalesQuote", new[] { "ParentID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "ParentID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "ParentID" });
            DropColumn("Sampling.SampleRequest", "ParentID");
            DropColumn("Quoting.SalesQuote", "ParentID");
            DropColumn("Invoicing.SalesInvoice", "ParentID");
            DropColumn("Purchasing.PurchaseOrder", "ParentID");
        }
    }
}
