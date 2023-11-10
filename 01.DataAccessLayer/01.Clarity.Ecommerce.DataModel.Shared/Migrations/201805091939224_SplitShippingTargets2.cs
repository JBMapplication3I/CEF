// <copyright file="201805091939224_SplitShippingTargets2.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201805091939224 split shipping targets 2 class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SplitShippingTargets2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "Ordering.SalesOrderItemShipment", name: "SalesOrderItemID", newName: "MasterID");
            RenameColumn(table: "Ordering.SalesOrderItemShipment", name: "ShipmentID", newName: "SlaveID");
            RenameColumn(table: "Returning.SalesReturnItemShipment", name: "SalesReturnItemID", newName: "MasterID");
            RenameColumn(table: "Returning.SalesReturnItemShipment", name: "ShipmentID", newName: "SlaveID");
            RenameColumn(table: "Quoting.SalesQuoteItemShipment", name: "SalesQuoteItemID", newName: "MasterID");
            RenameColumn(table: "Quoting.SalesQuoteItemShipment", name: "ShipmentID", newName: "SlaveID");
            RenameColumn(table: "Sampling.SampleRequestItemShipment", name: "SampleRequestItemID", newName: "MasterID");
            RenameColumn(table: "Sampling.SampleRequestItemShipment", name: "ShipmentID", newName: "SlaveID");
            RenameColumn(table: "Shopping.CartItemShipment", name: "CartItemID", newName: "MasterID");
            RenameColumn(table: "Shopping.CartItemShipment", name: "ShipmentID", newName: "SlaveID");
            RenameColumn(table: "Invoicing.SalesInvoiceItemShipment", name: "SalesInvoiceItemID", newName: "MasterID");
            RenameColumn(table: "Invoicing.SalesInvoiceItemShipment", name: "ShipmentID", newName: "SlaveID");
            RenameColumn(table: "Purchasing.PurchaseOrderItemShipment", name: "PurchaseOrderItemID", newName: "MasterID");
            RenameColumn(table: "Purchasing.PurchaseOrderItemShipment", name: "ShipmentID", newName: "SlaveID");
            RenameIndex(table: "Returning.SalesReturnItemShipment", name: "IX_SalesReturnItemID", newName: "IX_MasterID");
            RenameIndex(table: "Returning.SalesReturnItemShipment", name: "IX_ShipmentID", newName: "IX_SlaveID");
            RenameIndex(table: "Quoting.SalesQuoteItemShipment", name: "IX_SalesQuoteItemID", newName: "IX_MasterID");
            RenameIndex(table: "Quoting.SalesQuoteItemShipment", name: "IX_ShipmentID", newName: "IX_SlaveID");
            RenameIndex(table: "Purchasing.PurchaseOrderItemShipment", name: "IX_PurchaseOrderItemID", newName: "IX_MasterID");
            RenameIndex(table: "Purchasing.PurchaseOrderItemShipment", name: "IX_ShipmentID", newName: "IX_SlaveID");
            RenameIndex(table: "Invoicing.SalesInvoiceItemShipment", name: "IX_SalesInvoiceItemID", newName: "IX_MasterID");
            RenameIndex(table: "Invoicing.SalesInvoiceItemShipment", name: "IX_ShipmentID", newName: "IX_SlaveID");
            RenameIndex(table: "Ordering.SalesOrderItemShipment", name: "IX_SalesOrderItemID", newName: "IX_MasterID");
            RenameIndex(table: "Ordering.SalesOrderItemShipment", name: "IX_ShipmentID", newName: "IX_SlaveID");
            RenameIndex(table: "Sampling.SampleRequestItemShipment", name: "IX_SampleRequestItemID", newName: "IX_MasterID");
            RenameIndex(table: "Sampling.SampleRequestItemShipment", name: "IX_ShipmentID", newName: "IX_SlaveID");
            RenameIndex(table: "Shopping.CartItemShipment", name: "IX_CartItemID", newName: "IX_MasterID");
            RenameIndex(table: "Shopping.CartItemShipment", name: "IX_ShipmentID", newName: "IX_SlaveID");
            CreateTable(
                "Sales.SalesItemTargetType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            AddColumn("System.Note", "PurchaseOrderItemID", c => c.Int());
            AddColumn("System.Note", "SalesOrderItemID", c => c.Int());
            AddColumn("System.Note", "SalesInvoiceItemID", c => c.Int());
            AddColumn("System.Note", "SalesQuoteItemID", c => c.Int());
            AddColumn("System.Note", "SampleRequestItemID", c => c.Int());
            AddColumn("System.Note", "CartItemID", c => c.Int());
            AddColumn("Ordering.SalesOrderItemTarget", "TypeID", c => c.Int(nullable: false));
            AddColumn("Returning.SalesReturnItemTarget", "TypeID", c => c.Int(nullable: false));
            AddColumn("Quoting.SalesQuoteItemTarget", "TypeID", c => c.Int(nullable: false));
            AddColumn("Sampling.SampleRequestItemTarget", "TypeID", c => c.Int(nullable: false));
            AddColumn("Shopping.CartItemTarget", "TypeID", c => c.Int(nullable: false));
            AddColumn("Invoicing.SalesInvoiceItemTarget", "TypeID", c => c.Int(nullable: false));
            AddColumn("Purchasing.PurchaseOrderItemTarget", "TypeID", c => c.Int(nullable: false));
            AlterColumn("Shopping.CartItem", "UnitOfMeasure", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("Ordering.SalesOrderItem", "UnitOfMeasure", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("Returning.SalesReturn", "PurchaseOrderNumber", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("Returning.SalesReturn", "OrderStateName", c => c.String(maxLength: 128, unicode: false));
            AlterColumn("Returning.SalesReturn", "TrackingNumber", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("Returning.SalesReturn", "RefundTransactionID", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("Returning.SalesReturn", "TaxTransactionID", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("Returning.SalesReturnItem", "UnitOfMeasure", c => c.String(maxLength: 100, unicode: false));
            CreateIndex("Shopping.CartItem", "CreatedDate");
            CreateIndex("System.Note", "CreatedDate");
            CreateIndex("System.Note", "PurchaseOrderItemID");
            CreateIndex("System.Note", "SalesOrderItemID");
            CreateIndex("System.Note", "SalesInvoiceItemID");
            CreateIndex("System.Note", "SalesQuoteItemID");
            CreateIndex("System.Note", "SampleRequestItemID");
            CreateIndex("System.Note", "CartItemID");
            CreateIndex("Returning.SalesReturn", "CreatedDate");
            CreateIndex("Returning.SalesReturn", "Hash");
            CreateIndex("Returning.SalesReturnItem", "Hash");
            CreateIndex("Returning.SalesReturnItemShipment", "CreatedDate");
            CreateIndex("Returning.SalesReturnItemShipment", "Hash");
            CreateIndex("Returning.SalesReturnItemTarget", "TypeID");
            CreateIndex("Quoting.SalesQuoteItemShipment", "CreatedDate");
            CreateIndex("Quoting.SalesQuoteItemTarget", "TypeID");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "CreatedDate");
            CreateIndex("Purchasing.PurchaseOrderItemTarget", "TypeID");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "CreatedDate");
            CreateIndex("Invoicing.SalesInvoiceItemTarget", "TypeID");
            CreateIndex("Ordering.SalesOrderItemShipment", "CreatedDate");
            CreateIndex("Ordering.SalesOrderItemTarget", "TypeID");
            CreateIndex("Sampling.SampleRequestItemShipment", "CreatedDate");
            CreateIndex("Sampling.SampleRequestItemTarget", "TypeID");
            CreateIndex("Shopping.CartItemShipment", "CreatedDate");
            CreateIndex("Shopping.CartItemTarget", "TypeID");
            AddForeignKey("System.Note", "CartItemID", "Shopping.CartItem", "ID");
            AddForeignKey("Returning.SalesReturnItemTarget", "TypeID", "Sales.SalesItemTargetType", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteItemTarget", "TypeID", "Sales.SalesItemTargetType", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderItemTarget", "TypeID", "Sales.SalesItemTargetType", "ID", cascadeDelete: true);
            AddForeignKey("System.Note", "PurchaseOrderItemID", "Purchasing.PurchaseOrderItem", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItemTarget", "TypeID", "Sales.SalesItemTargetType", "ID", cascadeDelete: true);
            AddForeignKey("System.Note", "SalesInvoiceItemID", "Invoicing.SalesInvoiceItem", "ID");
            AddForeignKey("Ordering.SalesOrderItemTarget", "TypeID", "Sales.SalesItemTargetType", "ID", cascadeDelete: true);
            AddForeignKey("System.Note", "SalesOrderItemID", "Ordering.SalesOrderItem", "ID");
            AddForeignKey("System.Note", "SalesQuoteItemID", "Quoting.SalesQuoteItem", "ID");
            AddForeignKey("Sampling.SampleRequestItemTarget", "TypeID", "Sales.SalesItemTargetType", "ID", cascadeDelete: true);
            AddForeignKey("System.Note", "SampleRequestItemID", "Sampling.SampleRequestItem", "ID");
            AddForeignKey("Shopping.CartItemTarget", "TypeID", "Sales.SalesItemTargetType", "ID", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("Shopping.CartItemTarget", "TypeID", "Sales.SalesItemTargetType");
            DropForeignKey("System.Note", "SampleRequestItemID", "Sampling.SampleRequestItem");
            DropForeignKey("Sampling.SampleRequestItemTarget", "TypeID", "Sales.SalesItemTargetType");
            DropForeignKey("System.Note", "SalesQuoteItemID", "Quoting.SalesQuoteItem");
            DropForeignKey("System.Note", "SalesOrderItemID", "Ordering.SalesOrderItem");
            DropForeignKey("Ordering.SalesOrderItemTarget", "TypeID", "Sales.SalesItemTargetType");
            DropForeignKey("System.Note", "SalesInvoiceItemID", "Invoicing.SalesInvoiceItem");
            DropForeignKey("Invoicing.SalesInvoiceItemTarget", "TypeID", "Sales.SalesItemTargetType");
            DropForeignKey("System.Note", "PurchaseOrderItemID", "Purchasing.PurchaseOrderItem");
            DropForeignKey("Purchasing.PurchaseOrderItemTarget", "TypeID", "Sales.SalesItemTargetType");
            DropForeignKey("Quoting.SalesQuoteItemTarget", "TypeID", "Sales.SalesItemTargetType");
            DropForeignKey("Returning.SalesReturnItemTarget", "TypeID", "Sales.SalesItemTargetType");
            DropForeignKey("System.Note", "CartItemID", "Shopping.CartItem");
            DropIndex("Shopping.CartItemTarget", new[] { "TypeID" });
            DropIndex("Shopping.CartItemShipment", new[] { "CreatedDate" });
            DropIndex("Sampling.SampleRequestItemTarget", new[] { "TypeID" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "CreatedDate" });
            DropIndex("Ordering.SalesOrderItemTarget", new[] { "TypeID" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "CreatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemTarget", new[] { "TypeID" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemTarget", new[] { "TypeID" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "CreatedDate" });
            DropIndex("Quoting.SalesQuoteItemTarget", new[] { "TypeID" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "CreatedDate" });
            DropIndex("Sales.SalesItemTargetType", new[] { "SortOrder" });
            DropIndex("Sales.SalesItemTargetType", new[] { "DisplayName" });
            DropIndex("Sales.SalesItemTargetType", new[] { "Name" });
            DropIndex("Sales.SalesItemTargetType", new[] { "Hash" });
            DropIndex("Sales.SalesItemTargetType", new[] { "Active" });
            DropIndex("Sales.SalesItemTargetType", new[] { "UpdatedDate" });
            DropIndex("Sales.SalesItemTargetType", new[] { "CreatedDate" });
            DropIndex("Sales.SalesItemTargetType", new[] { "CustomKey" });
            DropIndex("Sales.SalesItemTargetType", new[] { "ID" });
            DropIndex("Returning.SalesReturnItemTarget", new[] { "TypeID" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "Hash" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "CreatedDate" });
            DropIndex("Returning.SalesReturnItem", new[] { "Hash" });
            DropIndex("Returning.SalesReturn", new[] { "Hash" });
            DropIndex("Returning.SalesReturn", new[] { "CreatedDate" });
            DropIndex("System.Note", new[] { "CartItemID" });
            DropIndex("System.Note", new[] { "SampleRequestItemID" });
            DropIndex("System.Note", new[] { "SalesQuoteItemID" });
            DropIndex("System.Note", new[] { "SalesInvoiceItemID" });
            DropIndex("System.Note", new[] { "SalesOrderItemID" });
            DropIndex("System.Note", new[] { "PurchaseOrderItemID" });
            DropIndex("System.Note", new[] { "CreatedDate" });
            DropIndex("Shopping.CartItem", new[] { "CreatedDate" });
            AlterColumn("Returning.SalesReturnItem", "UnitOfMeasure", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("Returning.SalesReturn", "TaxTransactionID", c => c.String(maxLength: 256));
            AlterColumn("Returning.SalesReturn", "RefundTransactionID", c => c.String(maxLength: 256));
            AlterColumn("Returning.SalesReturn", "TrackingNumber", c => c.String());
            AlterColumn("Returning.SalesReturn", "OrderStateName", c => c.String());
            AlterColumn("Returning.SalesReturn", "PurchaseOrderNumber", c => c.String(maxLength: 100));
            AlterColumn("Ordering.SalesOrderItem", "UnitOfMeasure", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("Shopping.CartItem", "UnitOfMeasure", c => c.String(maxLength: 1000, unicode: false));
            DropColumn("Purchasing.PurchaseOrderItemTarget", "TypeID");
            DropColumn("Invoicing.SalesInvoiceItemTarget", "TypeID");
            DropColumn("Shopping.CartItemTarget", "TypeID");
            DropColumn("Sampling.SampleRequestItemTarget", "TypeID");
            DropColumn("Quoting.SalesQuoteItemTarget", "TypeID");
            DropColumn("Returning.SalesReturnItemTarget", "TypeID");
            DropColumn("Ordering.SalesOrderItemTarget", "TypeID");
            DropColumn("System.Note", "CartItemID");
            DropColumn("System.Note", "SampleRequestItemID");
            DropColumn("System.Note", "SalesQuoteItemID");
            DropColumn("System.Note", "SalesInvoiceItemID");
            DropColumn("System.Note", "SalesOrderItemID");
            DropColumn("System.Note", "PurchaseOrderItemID");
            DropTable("Sales.SalesItemTargetType");
            RenameIndex(table: "Shopping.CartItemShipment", name: "IX_SlaveID", newName: "IX_ShipmentID");
            RenameIndex(table: "Shopping.CartItemShipment", name: "IX_MasterID", newName: "IX_CartItemID");
            RenameIndex(table: "Sampling.SampleRequestItemShipment", name: "IX_SlaveID", newName: "IX_ShipmentID");
            RenameIndex(table: "Sampling.SampleRequestItemShipment", name: "IX_MasterID", newName: "IX_SampleRequestItemID");
            RenameIndex(table: "Ordering.SalesOrderItemShipment", name: "IX_SlaveID", newName: "IX_ShipmentID");
            RenameIndex(table: "Ordering.SalesOrderItemShipment", name: "IX_MasterID", newName: "IX_SalesOrderItemID");
            RenameIndex(table: "Invoicing.SalesInvoiceItemShipment", name: "IX_SlaveID", newName: "IX_ShipmentID");
            RenameIndex(table: "Invoicing.SalesInvoiceItemShipment", name: "IX_MasterID", newName: "IX_SalesInvoiceItemID");
            RenameIndex(table: "Purchasing.PurchaseOrderItemShipment", name: "IX_SlaveID", newName: "IX_ShipmentID");
            RenameIndex(table: "Purchasing.PurchaseOrderItemShipment", name: "IX_MasterID", newName: "IX_PurchaseOrderItemID");
            RenameIndex(table: "Quoting.SalesQuoteItemShipment", name: "IX_SlaveID", newName: "IX_ShipmentID");
            RenameIndex(table: "Quoting.SalesQuoteItemShipment", name: "IX_MasterID", newName: "IX_SalesQuoteItemID");
            RenameIndex(table: "Returning.SalesReturnItemShipment", name: "IX_SlaveID", newName: "IX_ShipmentID");
            RenameIndex(table: "Returning.SalesReturnItemShipment", name: "IX_MasterID", newName: "IX_SalesReturnItemID");
            RenameColumn(table: "Purchasing.PurchaseOrderItemShipment", name: "SlaveID", newName: "ShipmentID");
            RenameColumn(table: "Purchasing.PurchaseOrderItemShipment", name: "MasterID", newName: "PurchaseOrderItemID");
            RenameColumn(table: "Invoicing.SalesInvoiceItemShipment", name: "SlaveID", newName: "ShipmentID");
            RenameColumn(table: "Invoicing.SalesInvoiceItemShipment", name: "MasterID", newName: "SalesInvoiceItemID");
            RenameColumn(table: "Shopping.CartItemShipment", name: "SlaveID", newName: "ShipmentID");
            RenameColumn(table: "Shopping.CartItemShipment", name: "MasterID", newName: "CartItemID");
            RenameColumn(table: "Sampling.SampleRequestItemShipment", name: "SlaveID", newName: "ShipmentID");
            RenameColumn(table: "Sampling.SampleRequestItemShipment", name: "MasterID", newName: "SampleRequestItemID");
            RenameColumn(table: "Quoting.SalesQuoteItemShipment", name: "SlaveID", newName: "ShipmentID");
            RenameColumn(table: "Quoting.SalesQuoteItemShipment", name: "MasterID", newName: "SalesQuoteItemID");
            RenameColumn(table: "Returning.SalesReturnItemShipment", name: "SlaveID", newName: "ShipmentID");
            RenameColumn(table: "Returning.SalesReturnItemShipment", name: "MasterID", newName: "SalesReturnItemID");
            RenameColumn(table: "Ordering.SalesOrderItemShipment", name: "SlaveID", newName: "ShipmentID");
            RenameColumn(table: "Ordering.SalesOrderItemShipment", name: "MasterID", newName: "SalesOrderItemID");
        }
    }
}
