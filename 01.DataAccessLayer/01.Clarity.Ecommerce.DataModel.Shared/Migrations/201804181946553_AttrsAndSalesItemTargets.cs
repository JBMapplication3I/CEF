// <copyright file="201804181946553_AttrsAndSalesItemTargets.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201804181946553 attributes and sales item targets class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AttrsAndSalesItemTargets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Attributes.GeneralAttributePredefinedOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Value = c.String(nullable: false),
                        UofM = c.String(maxLength: 64, unicode: false),
                        SortOrder = c.Int(),
                        AttributeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.GeneralAttribute", t => t.AttributeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.AttributeID);

            CreateTable(
                "Ordering.SalesOrderItemTarget",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        DestinationContactID = c.Int(nullable: false),
                        OriginProductInventoryLocationSectionID = c.Int(),
                        OriginStoreID = c.Int(),
                        SelectedRateQuoteID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("Ordering.SalesOrderItem", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.ProductInventoryLocationSection", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("Stores.Store", t => t.OriginStoreID)
                .ForeignKey("Shipping.RateQuote", t => t.SelectedRateQuoteID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreID)
                .Index(t => t.SelectedRateQuoteID);

            CreateTable(
                "Returning.SalesReturnItemTarget",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        DestinationContactID = c.Int(nullable: false),
                        OriginProductInventoryLocationSectionID = c.Int(),
                        OriginStoreID = c.Int(),
                        SelectedRateQuoteID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturnItem", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.ProductInventoryLocationSection", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("Stores.Store", t => t.OriginStoreID)
                .ForeignKey("Shipping.RateQuote", t => t.SelectedRateQuoteID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreID)
                .Index(t => t.SelectedRateQuoteID);

            CreateTable(
                "Quoting.SalesQuoteItemTarget",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        DestinationContactID = c.Int(nullable: false),
                        OriginProductInventoryLocationSectionID = c.Int(),
                        OriginStoreID = c.Int(),
                        SelectedRateQuoteID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("Quoting.SalesQuoteItem", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.ProductInventoryLocationSection", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("Stores.Store", t => t.OriginStoreID)
                .ForeignKey("Shipping.RateQuote", t => t.SelectedRateQuoteID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreID)
                .Index(t => t.SelectedRateQuoteID);

            CreateTable(
                "Sampling.SampleRequestItemTarget",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        DestinationContactID = c.Int(nullable: false),
                        OriginProductInventoryLocationSectionID = c.Int(),
                        OriginStoreID = c.Int(),
                        SelectedRateQuoteID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("Sampling.SampleRequestItem", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.ProductInventoryLocationSection", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("Stores.Store", t => t.OriginStoreID)
                .ForeignKey("Shipping.RateQuote", t => t.SelectedRateQuoteID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreID)
                .Index(t => t.SelectedRateQuoteID);

            CreateTable(
                "Shopping.CartItemTarget",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        DestinationContactID = c.Int(nullable: false),
                        OriginProductInventoryLocationSectionID = c.Int(),
                        OriginStoreID = c.Int(),
                        SelectedRateQuoteID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("Shopping.CartItem", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.ProductInventoryLocationSection", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("Stores.Store", t => t.OriginStoreID)
                .ForeignKey("Shipping.RateQuote", t => t.SelectedRateQuoteID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreID)
                .Index(t => t.SelectedRateQuoteID);

            CreateTable(
                "Invoicing.SalesInvoiceItemTarget",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        DestinationContactID = c.Int(nullable: false),
                        OriginProductInventoryLocationSectionID = c.Int(),
                        OriginStoreID = c.Int(),
                        SelectedRateQuoteID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("Invoicing.SalesInvoiceItem", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.ProductInventoryLocationSection", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("Stores.Store", t => t.OriginStoreID)
                .ForeignKey("Shipping.RateQuote", t => t.SelectedRateQuoteID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreID)
                .Index(t => t.SelectedRateQuoteID);

            CreateTable(
                "Purchasing.PurchaseOrderItemTarget",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        DestinationContactID = c.Int(nullable: false),
                        OriginProductInventoryLocationSectionID = c.Int(),
                        OriginStoreID = c.Int(),
                        SelectedRateQuoteID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("Purchasing.PurchaseOrderItem", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.ProductInventoryLocationSection", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("Stores.Store", t => t.OriginStoreID)
                .ForeignKey("Shipping.RateQuote", t => t.SelectedRateQuoteID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreID)
                .Index(t => t.SelectedRateQuoteID);

            Sql("UPDATE [Products].[Product] SET [IsQuotable] = 1 WHERE [IsQuotable] IS NULL");
            Sql("UPDATE [Products].[Product] SET [IsTaxable] = 1 WHERE [IsTaxable] IS NULL");
            Sql("UPDATE [Products].[Product] SET [IsSale] = 0 WHERE [IsSale] IS NULL");
            Sql("UPDATE [Products].[Product] SET [IsFreeShipping] = 0 WHERE [IsFreeShipping] IS NULL");
            Sql("UPDATE [Products].[Product] SET [AllowBackOrder] = 1 WHERE [AllowBackOrder] IS NULL");
            Sql("UPDATE [Products].[Product] SET [IsDiscontinued] = 0 WHERE [IsDiscontinued] IS NULL");
            Sql("UPDATE [Products].[Product] SET [IsEligibleForReturn] = 1 WHERE [IsEligibleForReturn] IS NULL");

            AddColumn("Attributes.GeneralAttribute", "HideFromSuppliers", c => c.Boolean(nullable: false));
            AddColumn("Attributes.GeneralAttribute", "HideFromProductDetailView", c => c.Boolean(nullable: false));
            AddColumn("Attributes.GeneralAttribute", "HideFromCatalogViews", c => c.Boolean(nullable: false));
            AlterColumn("Products.Product", "PriceBase", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("Products.Product", "RestockingFeePercent", c => c.Decimal(precision: 18, scale: 5));
            AlterColumn("Products.Product", "RestockingFeeAmount", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("Products.Product", "IsQuotable", c => c.Boolean(nullable: false));
            AlterColumn("Products.Product", "IsTaxable", c => c.Boolean(nullable: false));
            AlterColumn("Products.Product", "IsSale", c => c.Boolean(nullable: false));
            AlterColumn("Products.Product", "IsFreeShipping", c => c.Boolean(nullable: false));
            AlterColumn("Products.Product", "AllowBackOrder", c => c.Boolean(nullable: false));
            AlterColumn("Products.Product", "IsDiscontinued", c => c.Boolean(nullable: false));
            AlterColumn("Products.Product", "IsEligibleForReturn", c => c.Boolean(nullable: false));

            Sql("UPDATE [Products].[Product] SET [ManufacturerPartNumber] = LEFT([ManufacturerPartNumber], 255) WHERE LEN([ManufacturerPartNumber]) > 64");
            Sql("UPDATE [Products].[Product] SET [ShortDescription] = LEFT([ShortDescription], 255) WHERE LEN([ShortDescription]) > 255");
            Sql("UPDATE [Products].[Product] SET [BrandName] = LEFT([BrandName], 255) WHERE LEN([BrandName]) > 128");
            Sql("UPDATE [Products].[Product] SET [TaxCode] = LEFT([TaxCode], 255) WHERE LEN([TaxCode]) > 64");
            Sql("UPDATE [Products].[Product] SET [UnitOfMeasure] = LEFT([UnitOfMeasure], 255) WHERE LEN([UnitOfMeasure]) > 64");

            AlterColumn("Products.Product", "ManufacturerPartNumber", c => c.String(maxLength: 64, unicode: false));
            AlterColumn("Products.Product", "ShortDescription", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("Products.Product", "BrandName", c => c.String(maxLength: 128, unicode: false));
            AlterColumn("Products.Product", "TaxCode", c => c.String(maxLength: 64, unicode: false));
            AlterColumn("Products.Product", "UnitOfMeasure", c => c.String(maxLength: 64, unicode: false));
        }

        public override void Down()
        {
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "SelectedRateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "OriginProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "MasterID", "Purchasing.PurchaseOrderItem");
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "DestinationContactID", "Contacts.Contact");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "SelectedRateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "OriginProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "MasterID", "Invoicing.SalesInvoiceItem");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "DestinationContactID", "Contacts.Contact");
            DropForeignKey("Shopping.CartItemTarget", "SelectedRateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Shopping.CartItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Shopping.CartItemTarget", "OriginProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Shopping.CartItemTarget", "MasterID", "Shopping.CartItem");
            DropForeignKey("Shopping.CartItemTarget", "DestinationContactID", "Contacts.Contact");
            DropForeignKey("Sampling.SampleRequestItemTarget", "SelectedRateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Sampling.SampleRequestItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Sampling.SampleRequestItemTarget", "OriginProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Sampling.SampleRequestItemTarget", "MasterID", "Sampling.SampleRequestItem");
            DropForeignKey("Sampling.SampleRequestItemTarget", "DestinationContactID", "Contacts.Contact");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "SelectedRateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "OriginProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "MasterID", "Quoting.SalesQuoteItem");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "DestinationContactID", "Contacts.Contact");
            DropForeignKey("Returning.SalesReturnItemTarget", "SelectedRateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Returning.SalesReturnItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Returning.SalesReturnItemTarget", "OriginProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Returning.SalesReturnItemTarget", "MasterID", "Returning.SalesReturnItem");
            DropForeignKey("Returning.SalesReturnItemTarget", "DestinationContactID", "Contacts.Contact");
            DropForeignKey("Ordering.SalesOrderItemTarget", "SelectedRateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Ordering.SalesOrderItemTarget", "OriginStoreID", "Stores.Store");
            DropForeignKey("Ordering.SalesOrderItemTarget", "OriginProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Ordering.SalesOrderItemTarget", "MasterID", "Ordering.SalesOrderItem");
            DropForeignKey("Ordering.SalesOrderItemTarget", "DestinationContactID", "Contacts.Contact");
            DropForeignKey("Attributes.GeneralAttributePredefinedOption", "AttributeID", "Attributes.GeneralAttribute");
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "SelectedRateQuoteID" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "OriginStoreID" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "DestinationContactID" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "MasterID" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "SelectedRateQuoteID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "OriginStoreID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "DestinationContactID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "MasterID" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "CreatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "ID" });
            DropIndex("Shopping.CartItemTarget", new[] { "SelectedRateQuoteID" });
            DropIndex("Shopping.CartItemTarget", new[] { "OriginStoreID" });
            DropIndex("Shopping.CartItemTarget", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("Shopping.CartItemTarget", new[] { "DestinationContactID" });
            DropIndex("Shopping.CartItemTarget", new[] { "MasterID" });
            DropIndex("Shopping.CartItemTarget", new[] { "Hash" });
            DropIndex("Shopping.CartItemTarget", new[] { "Active" });
            DropIndex("Shopping.CartItemTarget", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartItemTarget", new[] { "CreatedDate" });
            DropIndex("Shopping.CartItemTarget", new[] { "CustomKey" });
            DropIndex("Shopping.CartItemTarget", new[] { "ID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "SelectedRateQuoteID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "OriginStoreID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "DestinationContactID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "MasterID" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "Active" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "CreatedDate" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "SelectedRateQuoteID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "OriginStoreID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "DestinationContactID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "MasterID" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "CreatedDate" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "ID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "SelectedRateQuoteID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "OriginStoreID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "DestinationContactID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "MasterID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "Hash" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "Active" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "CreatedDate" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "ID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "SelectedRateQuoteID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "OriginStoreID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "DestinationContactID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "MasterID" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "Active" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "CreatedDate" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "ID" });
            DropIndex("Attributes.GeneralAttributePredefinedOption", new[] { "AttributeID" });
            DropIndex("Attributes.GeneralAttributePredefinedOption", new[] { "Hash" });
            DropIndex("Attributes.GeneralAttributePredefinedOption", new[] { "Active" });
            DropIndex("Attributes.GeneralAttributePredefinedOption", new[] { "UpdatedDate" });
            DropIndex("Attributes.GeneralAttributePredefinedOption", new[] { "CustomKey" });
            DropIndex("Attributes.GeneralAttributePredefinedOption", new[] { "ID" });
            AlterColumn("Products.Product", "IsEligibleForReturn", c => c.Boolean());
            AlterColumn("Products.Product", "UnitOfMeasure", c => c.String());
            AlterColumn("Products.Product", "IsDiscontinued", c => c.Boolean());
            AlterColumn("Products.Product", "AllowBackOrder", c => c.Boolean());
            AlterColumn("Products.Product", "IsFreeShipping", c => c.Boolean());
            AlterColumn("Products.Product", "IsSale", c => c.Boolean());
            AlterColumn("Products.Product", "TaxCode", c => c.String());
            AlterColumn("Products.Product", "IsTaxable", c => c.Boolean());
            AlterColumn("Products.Product", "IsQuotable", c => c.Boolean());
            AlterColumn("Products.Product", "RestockingFeeAmount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("Products.Product", "RestockingFeePercent", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("Products.Product", "PriceBase", c => c.Decimal(precision: 18, scale: 4, storeType: "numeric"));
            AlterColumn("Products.Product", "BrandName", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("Products.Product", "ShortDescription", c => c.String(unicode: false));
            AlterColumn("Products.Product", "ManufacturerPartNumber", c => c.String(maxLength: 255));
            DropColumn("Attributes.GeneralAttribute", "HideFromCatalogViews");
            DropColumn("Attributes.GeneralAttribute", "HideFromProductDetailView");
            DropColumn("Attributes.GeneralAttribute", "HideFromSuppliers");
            DropTable("Purchasing.PurchaseOrderItemTarget");
            DropTable("Invoicing.SalesInvoiceItemTarget");
            DropTable("Shopping.CartItemTarget");
            DropTable("Sampling.SampleRequestItemTarget");
            DropTable("Quoting.SalesQuoteItemTarget");
            DropTable("Returning.SalesReturnItemTarget");
            DropTable("Ordering.SalesOrderItemTarget");
            DropTable("Attributes.GeneralAttributePredefinedOption");
        }
    }
}
