// <copyright file="202103170753458_SalesEvents.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202103170753458 sales events class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SalesEvents : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "Ordering.SalesOrderEvent", name: "SalesOrderID", newName: "MasterID");
            RenameIndex(table: "Ordering.SalesOrderEvent", name: "IX_SalesOrderID", newName: "IX_MasterID");
            CreateTable(
                "Quoting.SalesQuoteEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OldStateID = c.Int(),
                        NewStateID = c.Int(),
                        OldStatusID = c.Int(),
                        NewStatusID = c.Int(),
                        OldTypeID = c.Int(),
                        NewTypeID = c.Int(),
                        OldHash = c.Long(),
                        NewHash = c.Long(),
                        OldRecordSerialized = c.String(),
                        NewRecordSerialized = c.String(),
                        TypeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Quoting.SalesQuote", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Quoting.SalesQuoteEventType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.MasterID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Quoting.SalesQuoteEventType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Returning.SalesReturnEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OldStateID = c.Int(),
                        NewStateID = c.Int(),
                        OldStatusID = c.Int(),
                        NewStatusID = c.Int(),
                        OldTypeID = c.Int(),
                        NewTypeID = c.Int(),
                        OldHash = c.Long(),
                        NewHash = c.Long(),
                        OldRecordSerialized = c.String(),
                        NewRecordSerialized = c.String(),
                        TypeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Returning.SalesReturn", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturnEventType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.MasterID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Returning.SalesReturnEventType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Sampling.SampleRequestEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OldStateID = c.Int(),
                        NewStateID = c.Int(),
                        OldStatusID = c.Int(),
                        NewStatusID = c.Int(),
                        OldTypeID = c.Int(),
                        NewTypeID = c.Int(),
                        OldHash = c.Long(),
                        NewHash = c.Long(),
                        OldRecordSerialized = c.String(),
                        NewRecordSerialized = c.String(),
                        TypeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Sampling.SampleRequest", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Sampling.SampleRequestEventType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.MasterID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Sampling.SampleRequestEventType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Shopping.CartEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OldStateID = c.Int(),
                        NewStateID = c.Int(),
                        OldStatusID = c.Int(),
                        NewStatusID = c.Int(),
                        OldTypeID = c.Int(),
                        NewTypeID = c.Int(),
                        OldHash = c.Long(),
                        NewHash = c.Long(),
                        OldRecordSerialized = c.String(),
                        NewRecordSerialized = c.String(),
                        TypeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shopping.Cart", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Shopping.CartEventType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.MasterID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Shopping.CartEventType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Invoicing.SalesInvoiceEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OldBalanceDue = c.Decimal(precision: 18, scale: 4),
                        NewBalanceDue = c.Decimal(precision: 18, scale: 4),
                        OldStateID = c.Int(),
                        NewStateID = c.Int(),
                        OldStatusID = c.Int(),
                        NewStatusID = c.Int(),
                        OldTypeID = c.Int(),
                        NewTypeID = c.Int(),
                        OldHash = c.Long(),
                        NewHash = c.Long(),
                        OldRecordSerialized = c.String(),
                        NewRecordSerialized = c.String(),
                        TypeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Invoicing.SalesInvoice", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Invoicing.SalesInvoiceEventType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.MasterID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Invoicing.SalesInvoiceEventType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Ordering.SalesOrderEventType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Purchasing.PurchaseOrderEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OldStateID = c.Int(),
                        NewStateID = c.Int(),
                        OldStatusID = c.Int(),
                        NewStatusID = c.Int(),
                        OldTypeID = c.Int(),
                        NewTypeID = c.Int(),
                        OldHash = c.Long(),
                        NewHash = c.Long(),
                        OldRecordSerialized = c.String(),
                        NewRecordSerialized = c.String(),
                        TypeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Purchasing.PurchaseOrder", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Purchasing.PurchaseOrderEventType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.MasterID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Purchasing.PurchaseOrderEventType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("Ordering.SalesOrderEvent", "TypeID", c => c.Int(nullable: false));
            CreateIndex("Ordering.SalesOrderEvent", "TypeID");
            AddForeignKey("Ordering.SalesOrderEvent", "TypeID", "Ordering.SalesOrderEventType", "ID", cascadeDelete: true);
            DropColumn("Quoting.SalesQuote", "BalanceDue");
        }

        public override void Down()
        {
            AddColumn("Quoting.SalesQuote", "BalanceDue", c => c.Decimal(precision: 18, scale: 4));
            DropForeignKey("Purchasing.PurchaseOrderEvent", "TypeID", "Purchasing.PurchaseOrderEventType");
            DropForeignKey("Purchasing.PurchaseOrderEvent", "MasterID", "Purchasing.PurchaseOrder");
            DropForeignKey("Ordering.SalesOrderEvent", "TypeID", "Ordering.SalesOrderEventType");
            DropForeignKey("Invoicing.SalesInvoiceEvent", "TypeID", "Invoicing.SalesInvoiceEventType");
            DropForeignKey("Invoicing.SalesInvoiceEvent", "MasterID", "Invoicing.SalesInvoice");
            DropForeignKey("Shopping.CartEvent", "TypeID", "Shopping.CartEventType");
            DropForeignKey("Shopping.CartEvent", "MasterID", "Shopping.Cart");
            DropForeignKey("Sampling.SampleRequestEvent", "TypeID", "Sampling.SampleRequestEventType");
            DropForeignKey("Sampling.SampleRequestEvent", "MasterID", "Sampling.SampleRequest");
            DropForeignKey("Returning.SalesReturnEvent", "TypeID", "Returning.SalesReturnEventType");
            DropForeignKey("Returning.SalesReturnEvent", "MasterID", "Returning.SalesReturn");
            DropForeignKey("Quoting.SalesQuoteEvent", "TypeID", "Quoting.SalesQuoteEventType");
            DropForeignKey("Quoting.SalesQuoteEvent", "MasterID", "Quoting.SalesQuote");
            DropIndex("Purchasing.PurchaseOrderEventType", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderEventType", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderEventType", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderEventType", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrderEventType", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderEventType", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderEventType", new[] { "SortOrder" });
            DropIndex("Purchasing.PurchaseOrderEventType", new[] { "DisplayName" });
            DropIndex("Purchasing.PurchaseOrderEventType", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderEvent", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderEvent", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderEvent", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderEvent", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrderEvent", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderEvent", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderEvent", new[] { "MasterID" });
            DropIndex("Purchasing.PurchaseOrderEvent", new[] { "TypeID" });
            DropIndex("Purchasing.PurchaseOrderEvent", new[] { "ID" });
            DropIndex("Ordering.SalesOrderEventType", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderEventType", new[] { "Active" });
            DropIndex("Ordering.SalesOrderEventType", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderEventType", new[] { "CreatedDate" });
            DropIndex("Ordering.SalesOrderEventType", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderEventType", new[] { "Name" });
            DropIndex("Ordering.SalesOrderEventType", new[] { "SortOrder" });
            DropIndex("Ordering.SalesOrderEventType", new[] { "DisplayName" });
            DropIndex("Ordering.SalesOrderEventType", new[] { "ID" });
            DropIndex("Ordering.SalesOrderEvent", new[] { "TypeID" });
            DropIndex("Invoicing.SalesInvoiceEventType", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceEventType", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceEventType", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceEventType", new[] { "CreatedDate" });
            DropIndex("Invoicing.SalesInvoiceEventType", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceEventType", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceEventType", new[] { "SortOrder" });
            DropIndex("Invoicing.SalesInvoiceEventType", new[] { "DisplayName" });
            DropIndex("Invoicing.SalesInvoiceEventType", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceEvent", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceEvent", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceEvent", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceEvent", new[] { "CreatedDate" });
            DropIndex("Invoicing.SalesInvoiceEvent", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceEvent", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceEvent", new[] { "MasterID" });
            DropIndex("Invoicing.SalesInvoiceEvent", new[] { "TypeID" });
            DropIndex("Invoicing.SalesInvoiceEvent", new[] { "ID" });
            DropIndex("Shopping.CartEventType", new[] { "Hash" });
            DropIndex("Shopping.CartEventType", new[] { "Active" });
            DropIndex("Shopping.CartEventType", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartEventType", new[] { "CreatedDate" });
            DropIndex("Shopping.CartEventType", new[] { "CustomKey" });
            DropIndex("Shopping.CartEventType", new[] { "Name" });
            DropIndex("Shopping.CartEventType", new[] { "SortOrder" });
            DropIndex("Shopping.CartEventType", new[] { "DisplayName" });
            DropIndex("Shopping.CartEventType", new[] { "ID" });
            DropIndex("Shopping.CartEvent", new[] { "Hash" });
            DropIndex("Shopping.CartEvent", new[] { "Active" });
            DropIndex("Shopping.CartEvent", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartEvent", new[] { "CreatedDate" });
            DropIndex("Shopping.CartEvent", new[] { "CustomKey" });
            DropIndex("Shopping.CartEvent", new[] { "Name" });
            DropIndex("Shopping.CartEvent", new[] { "MasterID" });
            DropIndex("Shopping.CartEvent", new[] { "TypeID" });
            DropIndex("Shopping.CartEvent", new[] { "ID" });
            DropIndex("Sampling.SampleRequestEventType", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestEventType", new[] { "Active" });
            DropIndex("Sampling.SampleRequestEventType", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestEventType", new[] { "CreatedDate" });
            DropIndex("Sampling.SampleRequestEventType", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestEventType", new[] { "Name" });
            DropIndex("Sampling.SampleRequestEventType", new[] { "SortOrder" });
            DropIndex("Sampling.SampleRequestEventType", new[] { "DisplayName" });
            DropIndex("Sampling.SampleRequestEventType", new[] { "ID" });
            DropIndex("Sampling.SampleRequestEvent", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestEvent", new[] { "Active" });
            DropIndex("Sampling.SampleRequestEvent", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestEvent", new[] { "CreatedDate" });
            DropIndex("Sampling.SampleRequestEvent", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestEvent", new[] { "Name" });
            DropIndex("Sampling.SampleRequestEvent", new[] { "MasterID" });
            DropIndex("Sampling.SampleRequestEvent", new[] { "TypeID" });
            DropIndex("Sampling.SampleRequestEvent", new[] { "ID" });
            DropIndex("Returning.SalesReturnEventType", new[] { "Hash" });
            DropIndex("Returning.SalesReturnEventType", new[] { "Active" });
            DropIndex("Returning.SalesReturnEventType", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnEventType", new[] { "CreatedDate" });
            DropIndex("Returning.SalesReturnEventType", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnEventType", new[] { "Name" });
            DropIndex("Returning.SalesReturnEventType", new[] { "SortOrder" });
            DropIndex("Returning.SalesReturnEventType", new[] { "DisplayName" });
            DropIndex("Returning.SalesReturnEventType", new[] { "ID" });
            DropIndex("Returning.SalesReturnEvent", new[] { "Hash" });
            DropIndex("Returning.SalesReturnEvent", new[] { "Active" });
            DropIndex("Returning.SalesReturnEvent", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnEvent", new[] { "CreatedDate" });
            DropIndex("Returning.SalesReturnEvent", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnEvent", new[] { "Name" });
            DropIndex("Returning.SalesReturnEvent", new[] { "MasterID" });
            DropIndex("Returning.SalesReturnEvent", new[] { "TypeID" });
            DropIndex("Returning.SalesReturnEvent", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteEventType", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteEventType", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteEventType", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteEventType", new[] { "CreatedDate" });
            DropIndex("Quoting.SalesQuoteEventType", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteEventType", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteEventType", new[] { "SortOrder" });
            DropIndex("Quoting.SalesQuoteEventType", new[] { "DisplayName" });
            DropIndex("Quoting.SalesQuoteEventType", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteEvent", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteEvent", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteEvent", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteEvent", new[] { "CreatedDate" });
            DropIndex("Quoting.SalesQuoteEvent", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteEvent", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteEvent", new[] { "MasterID" });
            DropIndex("Quoting.SalesQuoteEvent", new[] { "TypeID" });
            DropIndex("Quoting.SalesQuoteEvent", new[] { "ID" });
            DropColumn("Ordering.SalesOrderEvent", "TypeID");
            DropTable("Purchasing.PurchaseOrderEventType");
            DropTable("Purchasing.PurchaseOrderEvent");
            DropTable("Ordering.SalesOrderEventType");
            DropTable("Invoicing.SalesInvoiceEventType");
            DropTable("Invoicing.SalesInvoiceEvent");
            DropTable("Shopping.CartEventType");
            DropTable("Shopping.CartEvent");
            DropTable("Sampling.SampleRequestEventType");
            DropTable("Sampling.SampleRequestEvent");
            DropTable("Returning.SalesReturnEventType");
            DropTable("Returning.SalesReturnEvent");
            DropTable("Quoting.SalesQuoteEventType");
            DropTable("Quoting.SalesQuoteEvent");
            RenameIndex(table: "Ordering.SalesOrderEvent", name: "IX_MasterID", newName: "IX_SalesOrderID");
            RenameColumn(table: "Ordering.SalesOrderEvent", name: "MasterID", newName: "SalesOrderID");
        }
    }
}
