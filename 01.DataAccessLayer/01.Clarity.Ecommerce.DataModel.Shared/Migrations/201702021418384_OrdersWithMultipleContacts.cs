// <copyright file="201702021418384_OrdersWithMultipleContacts.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201702021418384 orders with multiple contacts class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OrdersWithMultipleContacts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Shopping.CartContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        CartID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        SampleRequest_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shopping.Cart", t => t.CartID, cascadeDelete: true)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Sampling.SampleRequest", t => t.SampleRequest_ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.CartID)
                .Index(t => t.ContactID)
                .Index(t => t.SampleRequest_ID);

            CreateTable(
                "Quoting.SalesQuoteContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SalesQuoteID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Quoting.SalesQuote", t => t.SalesQuoteID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesQuoteID)
                .Index(t => t.ContactID);

            CreateTable(
                "Invoicing.SalesInvoiceContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SalesInvoiceID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Invoicing.SalesInvoice", t => t.SalesInvoiceID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesInvoiceID)
                .Index(t => t.ContactID);

            CreateTable(
                "Ordering.SalesOrderContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SalesOrderID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesOrderID)
                .Index(t => t.ContactID);

            CreateTable(
                "Purchasing.PurchaseOrderContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        PurchaseOrderID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Purchasing.PurchaseOrder", t => t.PurchaseOrderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.PurchaseOrderID)
                .Index(t => t.ContactID);

            CreateTable(
                "Sampling.SampleRequestContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SampleRequestID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Sampling.SampleRequest", t => t.SampleRequestID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SampleRequestID)
                .Index(t => t.ContactID);
        }

        public override void Down()
        {
            DropForeignKey("Sampling.SampleRequestContact", "SampleRequestID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequestContact", "ContactID", "Contacts.Contact");
            DropForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrderID", "Purchasing.PurchaseOrder");
            DropForeignKey("Purchasing.PurchaseOrderContact", "ContactID", "Contacts.Contact");
            DropForeignKey("Ordering.SalesOrderContact", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Ordering.SalesOrderContact", "ContactID", "Contacts.Contact");
            DropForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Invoicing.SalesInvoiceContact", "ContactID", "Contacts.Contact");
            DropForeignKey("Shopping.CartContact", "SampleRequest_ID", "Sampling.SampleRequest");
            DropForeignKey("Quoting.SalesQuoteContact", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("Quoting.SalesQuoteContact", "ContactID", "Contacts.Contact");
            DropForeignKey("Shopping.CartContact", "ContactID", "Contacts.Contact");
            DropForeignKey("Shopping.CartContact", "CartID", "Shopping.Cart");
            DropIndex("Sampling.SampleRequestContact", new[] { "ContactID" });
            DropIndex("Sampling.SampleRequestContact", new[] { "SampleRequestID" });
            DropIndex("Sampling.SampleRequestContact", new[] { "Active" });
            DropIndex("Sampling.SampleRequestContact", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestContact", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestContact", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "ContactID" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "PurchaseOrderID" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "ID" });
            DropIndex("Ordering.SalesOrderContact", new[] { "ContactID" });
            DropIndex("Ordering.SalesOrderContact", new[] { "SalesOrderID" });
            DropIndex("Ordering.SalesOrderContact", new[] { "Active" });
            DropIndex("Ordering.SalesOrderContact", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderContact", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderContact", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "ContactID" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "SalesInvoiceID" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "ContactID" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "SalesQuoteID" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "ID" });
            DropIndex("Shopping.CartContact", new[] { "SampleRequest_ID" });
            DropIndex("Shopping.CartContact", new[] { "ContactID" });
            DropIndex("Shopping.CartContact", new[] { "CartID" });
            DropIndex("Shopping.CartContact", new[] { "Active" });
            DropIndex("Shopping.CartContact", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartContact", new[] { "CustomKey" });
            DropIndex("Shopping.CartContact", new[] { "ID" });
            DropTable("Sampling.SampleRequestContact");
            DropTable("Purchasing.PurchaseOrderContact");
            DropTable("Ordering.SalesOrderContact");
            DropTable("Invoicing.SalesInvoiceContact");
            DropTable("Quoting.SalesQuoteContact");
            DropTable("Shopping.CartContact");
        }
    }
}
