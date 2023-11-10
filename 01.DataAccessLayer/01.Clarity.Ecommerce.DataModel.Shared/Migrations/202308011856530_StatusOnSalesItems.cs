namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class StatusOnSalesItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("Shopping.CartItem", "Status", c => c.String(maxLength: 500, unicode: false));
            AddColumn("Returning.SalesReturnItem", "Status", c => c.String(maxLength: 500, unicode: false));
            AddColumn("Sampling.SampleRequestItem", "Status", c => c.String(maxLength: 500, unicode: false));
            AddColumn("Quoting.SalesQuoteItem", "Status", c => c.String(maxLength: 500, unicode: false));
            AddColumn("Purchasing.PurchaseOrderItem", "Status", c => c.String(maxLength: 500, unicode: false));
            AddColumn("Invoicing.SalesInvoiceItem", "Status", c => c.String(maxLength: 500, unicode: false));
            AddColumn("Ordering.SalesOrderItem", "Status", c => c.String(maxLength: 500, unicode: false));
        }

        public override void Down()
        {
            DropColumn("Ordering.SalesOrderItem", "Status");
            DropColumn("Invoicing.SalesInvoiceItem", "Status");
            DropColumn("Purchasing.PurchaseOrderItem", "Status");
            DropColumn("Quoting.SalesQuoteItem", "Status");
            DropColumn("Sampling.SampleRequestItem", "Status");
            DropColumn("Returning.SalesReturnItem", "Status");
            DropColumn("Shopping.CartItem", "Status");
        }
    }
}
