// <copyright file="201803231823227_SalesGroups.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201803231823227 sales groups class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SalesGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Sales.SalesGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        AccountID = c.Int(),
                        BillingContactID = c.Int(),
                        MasterSalesQuoteID = c.Int(),
                        MasterSalesOrderID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.Contact", t => t.BillingContactID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.AccountID)
                .Index(t => t.BillingContactID);

            AddColumn("Purchasing.PurchaseOrder", "SalesGroupID", c => c.Int());
            AddColumn("Ordering.SalesOrder", "SalesGroupAsMasterID", c => c.Int());
            AddColumn("Ordering.SalesOrder", "SalesGroupAsSubID", c => c.Int());
            AddColumn("Ordering.SalesOrder", "SalesGroupAsMaster", c => c.Int());
            AddColumn("Invoicing.SalesInvoice", "SalesGroupID", c => c.Int());
            AddColumn("System.Note", "SalesGroupID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "SalesGroupAsMasterID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "SalesGroupAsResponseID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "SalesGroupAsMaster", c => c.Int());
            AddColumn("Returning.SalesReturn", "SalesGroupID", c => c.Int());
            CreateIndex("Purchasing.PurchaseOrder", "SalesGroupID");
            CreateIndex("Ordering.SalesOrder", "SalesGroupAsSubID");
            CreateIndex("Ordering.SalesOrder", "SalesGroupAsMaster");
            CreateIndex("Invoicing.SalesInvoice", "SalesGroupID");
            CreateIndex("System.Note", "SalesGroupID");
            CreateIndex("Quoting.SalesQuote", "SalesGroupAsResponseID");
            CreateIndex("Quoting.SalesQuote", "SalesGroupAsMaster");
            CreateIndex("Returning.SalesReturn", "SalesGroupID");
            AddForeignKey("Ordering.SalesOrder", "SalesGroupAsMaster", "Sales.SalesGroup", "ID");
            AddForeignKey("Quoting.SalesQuote", "SalesGroupAsMaster", "Sales.SalesGroup", "ID");
            AddForeignKey("Quoting.SalesQuote", "SalesGroupAsResponseID", "Sales.SalesGroup", "ID");
            AddForeignKey("Returning.SalesReturn", "SalesGroupID", "Sales.SalesGroup", "ID");
            AddForeignKey("Ordering.SalesOrder", "SalesGroupAsSubID", "Sales.SalesGroup", "ID");
            AddForeignKey("System.Note", "SalesGroupID", "Sales.SalesGroup", "ID");
            AddForeignKey("Invoicing.SalesInvoice", "SalesGroupID", "Sales.SalesGroup", "ID");
            AddForeignKey("Purchasing.PurchaseOrder", "SalesGroupID", "Sales.SalesGroup", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Purchasing.PurchaseOrder", "SalesGroupID", "Sales.SalesGroup");
            DropForeignKey("Invoicing.SalesInvoice", "SalesGroupID", "Sales.SalesGroup");
            DropForeignKey("System.Note", "SalesGroupID", "Sales.SalesGroup");
            DropForeignKey("Ordering.SalesOrder", "SalesGroupAsSubID", "Sales.SalesGroup");
            DropForeignKey("Returning.SalesReturn", "SalesGroupID", "Sales.SalesGroup");
            DropForeignKey("Quoting.SalesQuote", "SalesGroupAsResponseID", "Sales.SalesGroup");
            DropForeignKey("Quoting.SalesQuote", "SalesGroupAsMaster", "Sales.SalesGroup");
            DropForeignKey("Ordering.SalesOrder", "SalesGroupAsMaster", "Sales.SalesGroup");
            DropForeignKey("Sales.SalesGroup", "BillingContactID", "Contacts.Contact");
            DropForeignKey("Sales.SalesGroup", "AccountID", "Accounts.Account");
            DropIndex("Returning.SalesReturn", new[] { "SalesGroupID" });
            DropIndex("Sales.SalesGroup", new[] { "BillingContactID" });
            DropIndex("Sales.SalesGroup", new[] { "AccountID" });
            DropIndex("Sales.SalesGroup", new[] { "Hash" });
            DropIndex("Sales.SalesGroup", new[] { "Active" });
            DropIndex("Sales.SalesGroup", new[] { "UpdatedDate" });
            DropIndex("Sales.SalesGroup", new[] { "CustomKey" });
            DropIndex("Sales.SalesGroup", new[] { "ID" });
            DropIndex("Quoting.SalesQuote", new[] { "SalesGroupAsMaster" });
            DropIndex("Quoting.SalesQuote", new[] { "SalesGroupAsResponseID" });
            DropIndex("System.Note", new[] { "SalesGroupID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "SalesGroupID" });
            DropIndex("Ordering.SalesOrder", new[] { "SalesGroupAsMaster" });
            DropIndex("Ordering.SalesOrder", new[] { "SalesGroupAsSubID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "SalesGroupID" });
            DropColumn("Returning.SalesReturn", "SalesGroupID");
            DropColumn("Quoting.SalesQuote", "SalesGroupAsMaster");
            DropColumn("Quoting.SalesQuote", "SalesGroupAsResponseID");
            DropColumn("Quoting.SalesQuote", "SalesGroupAsMasterID");
            DropColumn("System.Note", "SalesGroupID");
            DropColumn("Invoicing.SalesInvoice", "SalesGroupID");
            DropColumn("Ordering.SalesOrder", "SalesGroupAsMaster");
            DropColumn("Ordering.SalesOrder", "SalesGroupAsSubID");
            DropColumn("Ordering.SalesOrder", "SalesGroupAsMasterID");
            DropColumn("Purchasing.PurchaseOrder", "SalesGroupID");
            DropTable("Sales.SalesGroup");
        }
    }
}
