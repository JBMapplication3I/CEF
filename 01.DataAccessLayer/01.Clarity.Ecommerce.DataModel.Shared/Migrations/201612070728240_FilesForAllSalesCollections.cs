// <copyright file="201612070728240_FilesForAllSalesCollections.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201612070728240 files for all sales collections class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FilesForAllSalesCollections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Shopping.CartFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        CartID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shopping.Cart", t => t.CartID, cascadeDelete: true)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.CartID)
                .Index(t => t.FileID);

            CreateTable(
                "Quoting.SalesQuoteFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SalesQuoteID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .ForeignKey("Quoting.SalesQuote", t => t.SalesQuoteID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.SalesQuoteID)
                .Index(t => t.FileID);

            CreateTable(
                "Sampling.SampleRequestFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SampleRequestID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .ForeignKey("Sampling.SampleRequest", t => t.SampleRequestID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.SampleRequestID)
                .Index(t => t.FileID);

            CreateTable(
                "Invoicing.SalesInvoiceFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SalesInvoiceID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .ForeignKey("Invoicing.SalesInvoice", t => t.SalesInvoiceID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.SalesInvoiceID)
                .Index(t => t.FileID);

            CreateTable(
                "Ordering.SalesOrderFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SalesOrderID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.SalesOrderID)
                .Index(t => t.FileID);

            CreateTable(
                "Purchasing.PurchaseOrderFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        PurchaseOrderID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .ForeignKey("Purchasing.PurchaseOrder", t => t.PurchaseOrderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.PurchaseOrderID)
                .Index(t => t.FileID);
        }

        public override void Down()
        {
            DropForeignKey("Purchasing.PurchaseOrderFile", "PurchaseOrderID", "Purchasing.PurchaseOrder");
            DropForeignKey("Purchasing.PurchaseOrderFile", "FileID", "Media.File");
            DropForeignKey("Ordering.SalesOrderFile", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Ordering.SalesOrderFile", "FileID", "Media.File");
            DropForeignKey("Invoicing.SalesInvoiceFile", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Invoicing.SalesInvoiceFile", "FileID", "Media.File");
            DropForeignKey("Sampling.SampleRequestFile", "SampleRequestID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequestFile", "FileID", "Media.File");
            DropForeignKey("Quoting.SalesQuoteFile", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("Quoting.SalesQuoteFile", "FileID", "Media.File");
            DropForeignKey("Shopping.CartFile", "FileID", "Media.File");
            DropForeignKey("Shopping.CartFile", "CartID", "Shopping.Cart");
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "FileID" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "PurchaseOrderID" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "ID" });
            DropIndex("Ordering.SalesOrderFile", new[] { "FileID" });
            DropIndex("Ordering.SalesOrderFile", new[] { "SalesOrderID" });
            DropIndex("Ordering.SalesOrderFile", new[] { "Name" });
            DropIndex("Ordering.SalesOrderFile", new[] { "Active" });
            DropIndex("Ordering.SalesOrderFile", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderFile", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderFile", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "FileID" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "SalesInvoiceID" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "ID" });
            DropIndex("Sampling.SampleRequestFile", new[] { "FileID" });
            DropIndex("Sampling.SampleRequestFile", new[] { "SampleRequestID" });
            DropIndex("Sampling.SampleRequestFile", new[] { "Name" });
            DropIndex("Sampling.SampleRequestFile", new[] { "Active" });
            DropIndex("Sampling.SampleRequestFile", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestFile", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestFile", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "FileID" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "SalesQuoteID" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "ID" });
            DropIndex("Shopping.CartFile", new[] { "FileID" });
            DropIndex("Shopping.CartFile", new[] { "CartID" });
            DropIndex("Shopping.CartFile", new[] { "Name" });
            DropIndex("Shopping.CartFile", new[] { "Active" });
            DropIndex("Shopping.CartFile", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartFile", new[] { "CustomKey" });
            DropIndex("Shopping.CartFile", new[] { "ID" });
            DropTable("Purchasing.PurchaseOrderFile");
            DropTable("Ordering.SalesOrderFile");
            DropTable("Invoicing.SalesInvoiceFile");
            DropTable("Sampling.SampleRequestFile");
            DropTable("Quoting.SalesQuoteFile");
            DropTable("Shopping.CartFile");
        }
    }
}
