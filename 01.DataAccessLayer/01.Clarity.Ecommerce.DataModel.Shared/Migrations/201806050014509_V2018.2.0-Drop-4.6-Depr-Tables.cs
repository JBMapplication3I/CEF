// <copyright file="201806050014509_V2018.2.0-Drop-4.6-Depr-Tables.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201806050014509 v 2018.2.0 drop 4.6 depr tables class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class V201820Drop46DeprTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Attributes.AttributeValue", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Attributes.AttributeValue", "ParentID", "Attributes.AttributeValue");
            DropForeignKey("Accounts.AccountAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Invoicing.SalesInvoiceAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Invoicing.SalesInvoiceAttribute", "MasterID", "Invoicing.SalesInvoice");
            DropForeignKey("Categories.CategoryAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Categories.CategoryAttribute", "MasterID", "Categories.Category");
            DropForeignKey("Products.ProductCategoryAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Products.ProductCategoryAttribute", "MasterID", "Products.ProductCategory");
            DropForeignKey("Products.ProductAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Products.ProductAttribute", "MasterID", "Products.Product");
            DropForeignKey("Shopping.CartItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Shopping.CartItemAttribute", "MasterID", "Shopping.CartItem");
            DropForeignKey("Shopping.CartAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Shopping.CartAttribute", "MasterID", "Shopping.Cart");
            DropForeignKey("Contacts.UserAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Contacts.UserAttribute", "MasterID", "Contacts.User");
            DropForeignKey("Quoting.SalesQuoteAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Quoting.SalesQuoteAttribute", "MasterID", "Quoting.SalesQuote");
            DropForeignKey("Returning.SalesReturnAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Returning.SalesReturnAttribute", "MasterID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Returning.SalesReturnItemAttribute", "MasterID", "Returning.SalesReturnItem");
            DropForeignKey("Quoting.SalesQuoteItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Quoting.SalesQuoteItemAttribute", "MasterID", "Quoting.SalesQuoteItem");
            DropForeignKey("Purchasing.PurchaseOrderItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Purchasing.PurchaseOrderItemAttribute", "MasterID", "Purchasing.PurchaseOrderItem");
            DropForeignKey("Invoicing.SalesInvoiceItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Invoicing.SalesInvoiceItemAttribute", "MasterID", "Invoicing.SalesInvoiceItem");
            DropForeignKey("Ordering.SalesOrderItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Ordering.SalesOrderItemAttribute", "MasterID", "Ordering.SalesOrderItem");
            DropForeignKey("Sampling.SampleRequestAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Sampling.SampleRequestAttribute", "MasterID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequestItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Sampling.SampleRequestItemAttribute", "MasterID", "Sampling.SampleRequestItem");
            DropForeignKey("Products.ProductAssociationAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Products.ProductAssociationAttribute", "MasterID", "Products.ProductAssociation");
            DropForeignKey("Ordering.SalesOrderAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Ordering.SalesOrderAttribute", "MasterID", "Ordering.SalesOrder");
            DropForeignKey("Purchasing.PurchaseOrderAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Purchasing.PurchaseOrderAttribute", "MasterID", "Purchasing.PurchaseOrder");
            DropForeignKey("Accounts.AccountAttribute", "MasterID", "Accounts.Account");
            DropIndex("Accounts.AccountAttribute", new[] { "ID" });
            DropIndex("Accounts.AccountAttribute", new[] { "CustomKey" });
            DropIndex("Accounts.AccountAttribute", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountAttribute", new[] { "Active" });
            DropIndex("Accounts.AccountAttribute", new[] { "Hash" });
            DropIndex("Accounts.AccountAttribute", new[] { "AttributeValueID" });
            DropIndex("Accounts.AccountAttribute", new[] { "MasterID" });
            DropIndex("Attributes.AttributeValue", new[] { "ID" });
            DropIndex("Attributes.AttributeValue", new[] { "CustomKey" });
            DropIndex("Attributes.AttributeValue", new[] { "UpdatedDate" });
            DropIndex("Attributes.AttributeValue", new[] { "Active" });
            DropIndex("Attributes.AttributeValue", new[] { "Hash" });
            DropIndex("Attributes.AttributeValue", new[] { "ParentID" });
            DropIndex("Attributes.AttributeValue", new[] { "AttributeID" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "AttributeID" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "MasterID" });
            DropIndex("Categories.CategoryAttribute", new[] { "ID" });
            DropIndex("Categories.CategoryAttribute", new[] { "CustomKey" });
            DropIndex("Categories.CategoryAttribute", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryAttribute", new[] { "Active" });
            DropIndex("Categories.CategoryAttribute", new[] { "Hash" });
            DropIndex("Categories.CategoryAttribute", new[] { "AttributeValueID" });
            DropIndex("Categories.CategoryAttribute", new[] { "MasterID" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "ID" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "CustomKey" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "UpdatedDate" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "Active" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "Hash" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "AttributeValueID" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "MasterID" });
            DropIndex("Products.ProductAttribute", new[] { "ID" });
            DropIndex("Products.ProductAttribute", new[] { "CustomKey" });
            DropIndex("Products.ProductAttribute", new[] { "UpdatedDate" });
            DropIndex("Products.ProductAttribute", new[] { "Active" });
            DropIndex("Products.ProductAttribute", new[] { "Hash" });
            DropIndex("Products.ProductAttribute", new[] { "AttributeValueID" });
            DropIndex("Products.ProductAttribute", new[] { "MasterID" });
            DropIndex("Shopping.CartItemAttribute", new[] { "ID" });
            DropIndex("Shopping.CartItemAttribute", new[] { "CustomKey" });
            DropIndex("Shopping.CartItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartItemAttribute", new[] { "Active" });
            DropIndex("Shopping.CartItemAttribute", new[] { "Hash" });
            DropIndex("Shopping.CartItemAttribute", new[] { "AttributeID" });
            DropIndex("Shopping.CartItemAttribute", new[] { "MasterID" });
            DropIndex("Shopping.CartAttribute", new[] { "ID" });
            DropIndex("Shopping.CartAttribute", new[] { "CustomKey" });
            DropIndex("Shopping.CartAttribute", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartAttribute", new[] { "Active" });
            DropIndex("Shopping.CartAttribute", new[] { "Hash" });
            DropIndex("Shopping.CartAttribute", new[] { "AttributeID" });
            DropIndex("Shopping.CartAttribute", new[] { "MasterID" });
            DropIndex("Contacts.UserAttribute", new[] { "ID" });
            DropIndex("Contacts.UserAttribute", new[] { "CustomKey" });
            DropIndex("Contacts.UserAttribute", new[] { "UpdatedDate" });
            DropIndex("Contacts.UserAttribute", new[] { "Active" });
            DropIndex("Contacts.UserAttribute", new[] { "Hash" });
            DropIndex("Contacts.UserAttribute", new[] { "AttributeValueID" });
            DropIndex("Contacts.UserAttribute", new[] { "MasterID" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "AttributeID" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "MasterID" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "ID" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "Active" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "AttributeID" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "MasterID" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "ID" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "Active" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "AttributeID" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "MasterID" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "AttributeID" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "MasterID" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "AttributeID" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "MasterID" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "AttributeID" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "MasterID" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "ID" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "Active" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "AttributeID" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "MasterID" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "ID" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "Active" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "AttributeID" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "MasterID" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "ID" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "Active" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "AttributeID" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "MasterID" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "ID" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "CustomKey" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "UpdatedDate" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "Active" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "Hash" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "AttributeValueID" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "MasterID" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "ID" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "Active" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "AttributeID" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "MasterID" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "AttributeID" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "MasterID" });
            DropTable("Accounts.AccountAttribute");
            DropTable("Attributes.AttributeValue");
            DropTable("Invoicing.SalesInvoiceAttribute");
            DropTable("Categories.CategoryAttribute");
            DropTable("Products.ProductCategoryAttribute");
            DropTable("Products.ProductAttribute");
            DropTable("Shopping.CartItemAttribute");
            DropTable("Shopping.CartAttribute");
            DropTable("Contacts.UserAttribute");
            DropTable("Quoting.SalesQuoteAttribute");
            DropTable("Returning.SalesReturnAttribute");
            DropTable("Returning.SalesReturnItemAttribute");
            DropTable("Quoting.SalesQuoteItemAttribute");
            DropTable("Purchasing.PurchaseOrderItemAttribute");
            DropTable("Invoicing.SalesInvoiceItemAttribute");
            DropTable("Ordering.SalesOrderItemAttribute");
            DropTable("Sampling.SampleRequestAttribute");
            DropTable("Sampling.SampleRequestItemAttribute");
            DropTable("Products.ProductAssociationAttribute");
            DropTable("Ordering.SalesOrderAttribute");
            DropTable("Purchasing.PurchaseOrderAttribute");
        }

        public override void Down()
        {
            CreateTable(
                "Purchasing.PurchaseOrderAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Ordering.SalesOrderAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Products.ProductAssociationAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Sampling.SampleRequestItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Sampling.SampleRequestAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Ordering.SalesOrderItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Invoicing.SalesInvoiceItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Purchasing.PurchaseOrderItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Quoting.SalesQuoteItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Returning.SalesReturnItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Returning.SalesReturnAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Quoting.SalesQuoteAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Contacts.UserAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Shopping.CartAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(maxLength: 2500, unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Shopping.CartItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(maxLength: 2500, unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Products.ProductAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Products.ProductCategoryAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Categories.CategoryAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Invoicing.SalesInvoiceAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Attributes.AttributeValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        ParentID = c.Int(),
                        JsonAttributes = c.String(),
                        Value = c.String(nullable: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Accounts.AccountAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateIndex("Purchasing.PurchaseOrderAttribute", "MasterID");
            CreateIndex("Purchasing.PurchaseOrderAttribute", "AttributeID");
            CreateIndex("Purchasing.PurchaseOrderAttribute", "Hash");
            CreateIndex("Purchasing.PurchaseOrderAttribute", "Active");
            CreateIndex("Purchasing.PurchaseOrderAttribute", "UpdatedDate");
            CreateIndex("Purchasing.PurchaseOrderAttribute", "CustomKey");
            CreateIndex("Purchasing.PurchaseOrderAttribute", "ID");
            CreateIndex("Ordering.SalesOrderAttribute", "MasterID");
            CreateIndex("Ordering.SalesOrderAttribute", "AttributeID");
            CreateIndex("Ordering.SalesOrderAttribute", "Hash");
            CreateIndex("Ordering.SalesOrderAttribute", "Active");
            CreateIndex("Ordering.SalesOrderAttribute", "UpdatedDate");
            CreateIndex("Ordering.SalesOrderAttribute", "CustomKey");
            CreateIndex("Ordering.SalesOrderAttribute", "ID");
            CreateIndex("Products.ProductAssociationAttribute", "MasterID");
            CreateIndex("Products.ProductAssociationAttribute", "AttributeValueID");
            CreateIndex("Products.ProductAssociationAttribute", "Hash");
            CreateIndex("Products.ProductAssociationAttribute", "Active");
            CreateIndex("Products.ProductAssociationAttribute", "UpdatedDate");
            CreateIndex("Products.ProductAssociationAttribute", "CustomKey");
            CreateIndex("Products.ProductAssociationAttribute", "ID");
            CreateIndex("Sampling.SampleRequestItemAttribute", "MasterID");
            CreateIndex("Sampling.SampleRequestItemAttribute", "AttributeID");
            CreateIndex("Sampling.SampleRequestItemAttribute", "Hash");
            CreateIndex("Sampling.SampleRequestItemAttribute", "Active");
            CreateIndex("Sampling.SampleRequestItemAttribute", "UpdatedDate");
            CreateIndex("Sampling.SampleRequestItemAttribute", "CustomKey");
            CreateIndex("Sampling.SampleRequestItemAttribute", "ID");
            CreateIndex("Sampling.SampleRequestAttribute", "MasterID");
            CreateIndex("Sampling.SampleRequestAttribute", "AttributeID");
            CreateIndex("Sampling.SampleRequestAttribute", "Hash");
            CreateIndex("Sampling.SampleRequestAttribute", "Active");
            CreateIndex("Sampling.SampleRequestAttribute", "UpdatedDate");
            CreateIndex("Sampling.SampleRequestAttribute", "CustomKey");
            CreateIndex("Sampling.SampleRequestAttribute", "ID");
            CreateIndex("Ordering.SalesOrderItemAttribute", "MasterID");
            CreateIndex("Ordering.SalesOrderItemAttribute", "AttributeID");
            CreateIndex("Ordering.SalesOrderItemAttribute", "Hash");
            CreateIndex("Ordering.SalesOrderItemAttribute", "Active");
            CreateIndex("Ordering.SalesOrderItemAttribute", "UpdatedDate");
            CreateIndex("Ordering.SalesOrderItemAttribute", "CustomKey");
            CreateIndex("Ordering.SalesOrderItemAttribute", "ID");
            CreateIndex("Invoicing.SalesInvoiceItemAttribute", "MasterID");
            CreateIndex("Invoicing.SalesInvoiceItemAttribute", "AttributeID");
            CreateIndex("Invoicing.SalesInvoiceItemAttribute", "Hash");
            CreateIndex("Invoicing.SalesInvoiceItemAttribute", "Active");
            CreateIndex("Invoicing.SalesInvoiceItemAttribute", "UpdatedDate");
            CreateIndex("Invoicing.SalesInvoiceItemAttribute", "CustomKey");
            CreateIndex("Invoicing.SalesInvoiceItemAttribute", "ID");
            CreateIndex("Purchasing.PurchaseOrderItemAttribute", "MasterID");
            CreateIndex("Purchasing.PurchaseOrderItemAttribute", "AttributeID");
            CreateIndex("Purchasing.PurchaseOrderItemAttribute", "Hash");
            CreateIndex("Purchasing.PurchaseOrderItemAttribute", "Active");
            CreateIndex("Purchasing.PurchaseOrderItemAttribute", "UpdatedDate");
            CreateIndex("Purchasing.PurchaseOrderItemAttribute", "CustomKey");
            CreateIndex("Purchasing.PurchaseOrderItemAttribute", "ID");
            CreateIndex("Quoting.SalesQuoteItemAttribute", "MasterID");
            CreateIndex("Quoting.SalesQuoteItemAttribute", "AttributeID");
            CreateIndex("Quoting.SalesQuoteItemAttribute", "Hash");
            CreateIndex("Quoting.SalesQuoteItemAttribute", "Active");
            CreateIndex("Quoting.SalesQuoteItemAttribute", "UpdatedDate");
            CreateIndex("Quoting.SalesQuoteItemAttribute", "CustomKey");
            CreateIndex("Quoting.SalesQuoteItemAttribute", "ID");
            CreateIndex("Returning.SalesReturnItemAttribute", "MasterID");
            CreateIndex("Returning.SalesReturnItemAttribute", "AttributeID");
            CreateIndex("Returning.SalesReturnItemAttribute", "Active");
            CreateIndex("Returning.SalesReturnItemAttribute", "UpdatedDate");
            CreateIndex("Returning.SalesReturnItemAttribute", "CustomKey");
            CreateIndex("Returning.SalesReturnItemAttribute", "ID");
            CreateIndex("Returning.SalesReturnAttribute", "MasterID");
            CreateIndex("Returning.SalesReturnAttribute", "AttributeID");
            CreateIndex("Returning.SalesReturnAttribute", "Active");
            CreateIndex("Returning.SalesReturnAttribute", "UpdatedDate");
            CreateIndex("Returning.SalesReturnAttribute", "CustomKey");
            CreateIndex("Returning.SalesReturnAttribute", "ID");
            CreateIndex("Quoting.SalesQuoteAttribute", "MasterID");
            CreateIndex("Quoting.SalesQuoteAttribute", "AttributeID");
            CreateIndex("Quoting.SalesQuoteAttribute", "Hash");
            CreateIndex("Quoting.SalesQuoteAttribute", "Active");
            CreateIndex("Quoting.SalesQuoteAttribute", "UpdatedDate");
            CreateIndex("Quoting.SalesQuoteAttribute", "CustomKey");
            CreateIndex("Quoting.SalesQuoteAttribute", "ID");
            CreateIndex("Contacts.UserAttribute", "MasterID");
            CreateIndex("Contacts.UserAttribute", "AttributeValueID");
            CreateIndex("Contacts.UserAttribute", "Hash");
            CreateIndex("Contacts.UserAttribute", "Active");
            CreateIndex("Contacts.UserAttribute", "UpdatedDate");
            CreateIndex("Contacts.UserAttribute", "CustomKey");
            CreateIndex("Contacts.UserAttribute", "ID");
            CreateIndex("Shopping.CartAttribute", "MasterID");
            CreateIndex("Shopping.CartAttribute", "AttributeID");
            CreateIndex("Shopping.CartAttribute", "Hash");
            CreateIndex("Shopping.CartAttribute", "Active");
            CreateIndex("Shopping.CartAttribute", "UpdatedDate");
            CreateIndex("Shopping.CartAttribute", "CustomKey");
            CreateIndex("Shopping.CartAttribute", "ID");
            CreateIndex("Shopping.CartItemAttribute", "MasterID");
            CreateIndex("Shopping.CartItemAttribute", "AttributeID");
            CreateIndex("Shopping.CartItemAttribute", "Hash");
            CreateIndex("Shopping.CartItemAttribute", "Active");
            CreateIndex("Shopping.CartItemAttribute", "UpdatedDate");
            CreateIndex("Shopping.CartItemAttribute", "CustomKey");
            CreateIndex("Shopping.CartItemAttribute", "ID");
            CreateIndex("Products.ProductAttribute", "MasterID");
            CreateIndex("Products.ProductAttribute", "AttributeValueID");
            CreateIndex("Products.ProductAttribute", "Hash");
            CreateIndex("Products.ProductAttribute", "Active");
            CreateIndex("Products.ProductAttribute", "UpdatedDate");
            CreateIndex("Products.ProductAttribute", "CustomKey");
            CreateIndex("Products.ProductAttribute", "ID");
            CreateIndex("Products.ProductCategoryAttribute", "MasterID");
            CreateIndex("Products.ProductCategoryAttribute", "AttributeValueID");
            CreateIndex("Products.ProductCategoryAttribute", "Hash");
            CreateIndex("Products.ProductCategoryAttribute", "Active");
            CreateIndex("Products.ProductCategoryAttribute", "UpdatedDate");
            CreateIndex("Products.ProductCategoryAttribute", "CustomKey");
            CreateIndex("Products.ProductCategoryAttribute", "ID");
            CreateIndex("Categories.CategoryAttribute", "MasterID");
            CreateIndex("Categories.CategoryAttribute", "AttributeValueID");
            CreateIndex("Categories.CategoryAttribute", "Hash");
            CreateIndex("Categories.CategoryAttribute", "Active");
            CreateIndex("Categories.CategoryAttribute", "UpdatedDate");
            CreateIndex("Categories.CategoryAttribute", "CustomKey");
            CreateIndex("Categories.CategoryAttribute", "ID");
            CreateIndex("Invoicing.SalesInvoiceAttribute", "MasterID");
            CreateIndex("Invoicing.SalesInvoiceAttribute", "AttributeID");
            CreateIndex("Invoicing.SalesInvoiceAttribute", "Hash");
            CreateIndex("Invoicing.SalesInvoiceAttribute", "Active");
            CreateIndex("Invoicing.SalesInvoiceAttribute", "UpdatedDate");
            CreateIndex("Invoicing.SalesInvoiceAttribute", "CustomKey");
            CreateIndex("Invoicing.SalesInvoiceAttribute", "ID");
            CreateIndex("Attributes.AttributeValue", "AttributeID");
            CreateIndex("Attributes.AttributeValue", "ParentID");
            CreateIndex("Attributes.AttributeValue", "Hash");
            CreateIndex("Attributes.AttributeValue", "Active");
            CreateIndex("Attributes.AttributeValue", "UpdatedDate");
            CreateIndex("Attributes.AttributeValue", "CustomKey");
            CreateIndex("Attributes.AttributeValue", "ID");
            CreateIndex("Accounts.AccountAttribute", "MasterID");
            CreateIndex("Accounts.AccountAttribute", "AttributeValueID");
            CreateIndex("Accounts.AccountAttribute", "Hash");
            CreateIndex("Accounts.AccountAttribute", "Active");
            CreateIndex("Accounts.AccountAttribute", "UpdatedDate");
            CreateIndex("Accounts.AccountAttribute", "CustomKey");
            CreateIndex("Accounts.AccountAttribute", "ID");
            AddForeignKey("Accounts.AccountAttribute", "MasterID", "Accounts.Account", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderAttribute", "MasterID", "Purchasing.PurchaseOrder", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderAttribute", "MasterID", "Ordering.SalesOrder", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductAssociationAttribute", "MasterID", "Products.ProductAssociation", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductAssociationAttribute", "AttributeValueID", "Attributes.AttributeValue", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequestItemAttribute", "MasterID", "Sampling.SampleRequestItem", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequestItemAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequestAttribute", "MasterID", "Sampling.SampleRequest", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequestAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderItemAttribute", "MasterID", "Ordering.SalesOrderItem", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderItemAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoiceItemAttribute", "MasterID", "Invoicing.SalesInvoiceItem", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoiceItemAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderItemAttribute", "MasterID", "Purchasing.PurchaseOrderItem", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderItemAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteItemAttribute", "MasterID", "Quoting.SalesQuoteItem", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteItemAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Returning.SalesReturnItemAttribute", "MasterID", "Returning.SalesReturnItem", "ID", cascadeDelete: true);
            AddForeignKey("Returning.SalesReturnItemAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Returning.SalesReturnAttribute", "MasterID", "Returning.SalesReturn", "ID", cascadeDelete: true);
            AddForeignKey("Returning.SalesReturnAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteAttribute", "MasterID", "Quoting.SalesQuote", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Contacts.UserAttribute", "MasterID", "Contacts.User", "ID", cascadeDelete: true);
            AddForeignKey("Contacts.UserAttribute", "AttributeValueID", "Attributes.AttributeValue", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartAttribute", "MasterID", "Shopping.Cart", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartItemAttribute", "MasterID", "Shopping.CartItem", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartItemAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductAttribute", "MasterID", "Products.Product", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductAttribute", "AttributeValueID", "Attributes.AttributeValue", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductCategoryAttribute", "MasterID", "Products.ProductCategory", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductCategoryAttribute", "AttributeValueID", "Attributes.AttributeValue", "ID", cascadeDelete: true);
            AddForeignKey("Categories.CategoryAttribute", "MasterID", "Categories.Category", "ID", cascadeDelete: true);
            AddForeignKey("Categories.CategoryAttribute", "AttributeValueID", "Attributes.AttributeValue", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoiceAttribute", "MasterID", "Invoicing.SalesInvoice", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoiceAttribute", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
            AddForeignKey("Accounts.AccountAttribute", "AttributeValueID", "Attributes.AttributeValue", "ID", cascadeDelete: true);
            AddForeignKey("Attributes.AttributeValue", "ParentID", "Attributes.AttributeValue", "ID");
            AddForeignKey("Attributes.AttributeValue", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
        }
    }
}
