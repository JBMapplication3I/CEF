namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddSalesEntitiesToShipments : DbMigration
    {
        public override void Up()
        {
            AddColumn("Shipping.Shipment", "SalesGroupID", c => c.Int());
            AddColumn("Shipping.Shipment", "SalesOrderID", c => c.Int());
            AddColumn("Shipping.Shipment", "SalesInvoiceID", c => c.Int());
            CreateIndex("Shipping.Shipment", "SalesGroupID");
            CreateIndex("Shipping.Shipment", "SalesOrderID");
            CreateIndex("Shipping.Shipment", "SalesInvoiceID");
            AddForeignKey("Shipping.Shipment", "SalesGroupID", "Sales.SalesGroup", "ID");
            AddForeignKey("Shipping.Shipment", "SalesInvoiceID", "Invoicing.SalesInvoice", "ID");
            AddForeignKey("Shipping.Shipment", "SalesOrderID", "Ordering.SalesOrder", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Shipping.Shipment", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Shipping.Shipment", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Shipping.Shipment", "SalesGroupID", "Sales.SalesGroup");
            DropIndex("Shipping.Shipment", new[] { "SalesInvoiceID" });
            DropIndex("Shipping.Shipment", new[] { "SalesOrderID" });
            DropIndex("Shipping.Shipment", new[] { "SalesGroupID" });
            DropColumn("Shipping.Shipment", "SalesInvoiceID");
            DropColumn("Shipping.Shipment", "SalesOrderID");
            DropColumn("Shipping.Shipment", "SalesGroupID");
        }
    }
}
