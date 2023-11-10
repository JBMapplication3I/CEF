// <copyright file="201701061432005_SplitShipping.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201701061432005 split shipping class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SplitShipping : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "Shipping.SalesOrderItemShipment", newSchema: "Ordering");
            CreateTable(
                "Quoting.SalesQuoteItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SalesQuoteItemID = c.Int(nullable: false),
                        ShipmentID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Quoting.SalesQuoteItem", t => t.SalesQuoteItemID, cascadeDelete: true)
                .ForeignKey("Shipping.Shipment", t => t.ShipmentID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesQuoteItemID)
                .Index(t => t.ShipmentID);

            CreateTable(
                "Sampling.SampleRequestItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SampleRequestItemID = c.Int(nullable: false),
                        ShipmentID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Sampling.SampleRequestItem", t => t.SampleRequestItemID, cascadeDelete: true)
                .ForeignKey("Shipping.Shipment", t => t.ShipmentID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SampleRequestItemID)
                .Index(t => t.ShipmentID);

            CreateTable(
                "Shopping.CartItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        CartItemID = c.Int(nullable: false),
                        ShipmentID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shopping.CartItem", t => t.CartItemID, cascadeDelete: true)
                .ForeignKey("Shipping.Shipment", t => t.ShipmentID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.CartItemID)
                .Index(t => t.ShipmentID);

            CreateTable(
                "Invoicing.SalesInvoiceItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SalesInvoiceItemID = c.Int(nullable: false),
                        ShipmentID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Invoicing.SalesInvoiceItem", t => t.SalesInvoiceItemID, cascadeDelete: true)
                .ForeignKey("Shipping.Shipment", t => t.ShipmentID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesInvoiceItemID)
                .Index(t => t.ShipmentID);

            CreateTable(
                "Purchasing.PurchaseOrderItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        PurchaseOrderItemID = c.Int(nullable: false),
                        ShipmentID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Purchasing.PurchaseOrderItem", t => t.PurchaseOrderItemID, cascadeDelete: true)
                .ForeignKey("Shipping.Shipment", t => t.ShipmentID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.PurchaseOrderItemID)
                .Index(t => t.ShipmentID);

            AddColumn("Ordering.SalesOrderItemShipment", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("Ordering.SalesOrderItemShipment", "JsonAttributes", c => c.String());
        }

        public override void Down()
        {
            DropForeignKey("Purchasing.PurchaseOrderItemShipment", "ShipmentID", "Shipping.Shipment");
            DropForeignKey("Purchasing.PurchaseOrderItemShipment", "PurchaseOrderItemID", "Purchasing.PurchaseOrderItem");
            DropForeignKey("Invoicing.SalesInvoiceItemShipment", "ShipmentID", "Shipping.Shipment");
            DropForeignKey("Invoicing.SalesInvoiceItemShipment", "SalesInvoiceItemID", "Invoicing.SalesInvoiceItem");
            DropForeignKey("Shopping.CartItemShipment", "ShipmentID", "Shipping.Shipment");
            DropForeignKey("Shopping.CartItemShipment", "CartItemID", "Shopping.CartItem");
            DropForeignKey("Sampling.SampleRequestItemShipment", "ShipmentID", "Shipping.Shipment");
            DropForeignKey("Sampling.SampleRequestItemShipment", "SampleRequestItemID", "Sampling.SampleRequestItem");
            DropForeignKey("Quoting.SalesQuoteItemShipment", "ShipmentID", "Shipping.Shipment");
            DropForeignKey("Quoting.SalesQuoteItemShipment", "SalesQuoteItemID", "Quoting.SalesQuoteItem");
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "ShipmentID" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "PurchaseOrderItemID" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "ShipmentID" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "SalesInvoiceItemID" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "ID" });
            DropIndex("Shopping.CartItemShipment", new[] { "ShipmentID" });
            DropIndex("Shopping.CartItemShipment", new[] { "CartItemID" });
            DropIndex("Shopping.CartItemShipment", new[] { "Active" });
            DropIndex("Shopping.CartItemShipment", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartItemShipment", new[] { "CustomKey" });
            DropIndex("Shopping.CartItemShipment", new[] { "ID" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "ShipmentID" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "SampleRequestItemID" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "Active" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "ShipmentID" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "SalesQuoteItemID" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "ID" });
            DropColumn("Ordering.SalesOrderItemShipment", "JsonAttributes");
            DropColumn("Ordering.SalesOrderItemShipment", "Quantity");
            DropTable("Purchasing.PurchaseOrderItemShipment");
            DropTable("Invoicing.SalesInvoiceItemShipment");
            DropTable("Shopping.CartItemShipment");
            DropTable("Sampling.SampleRequestItemShipment");
            DropTable("Quoting.SalesQuoteItemShipment");
            MoveTable(name: "Ordering.SalesOrderItemShipment", newSchema: "Shipping");
        }
    }
}
