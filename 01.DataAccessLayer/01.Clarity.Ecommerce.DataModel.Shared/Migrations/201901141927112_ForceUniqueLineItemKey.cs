// <copyright file="201901141927112_ForceUniqueLineItemKey.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201901141927112 force unique line item key class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ForceUniqueLineItemKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("Shopping.CartItem", "ForceUniqueLineItemKey", c => c.String(maxLength: 1024, unicode: false));
            AddColumn("Returning.SalesReturnItem", "ForceUniqueLineItemKey", c => c.String(maxLength: 1024, unicode: false));
            AddColumn("Quoting.SalesQuoteItem", "ForceUniqueLineItemKey", c => c.String(maxLength: 1024, unicode: false));
            AddColumn("Purchasing.PurchaseOrderItem", "ForceUniqueLineItemKey", c => c.String(maxLength: 1024, unicode: false));
            AddColumn("Invoicing.SalesInvoiceItem", "ForceUniqueLineItemKey", c => c.String(maxLength: 1024, unicode: false));
            AddColumn("Ordering.SalesOrderItem", "ForceUniqueLineItemKey", c => c.String(maxLength: 1024, unicode: false));
            AddColumn("Sampling.SampleRequestItem", "ForceUniqueLineItemKey", c => c.String(maxLength: 1024, unicode: false));
        }

        public override void Down()
        {
            DropColumn("Sampling.SampleRequestItem", "ForceUniqueLineItemKey");
            DropColumn("Ordering.SalesOrderItem", "ForceUniqueLineItemKey");
            DropColumn("Invoicing.SalesInvoiceItem", "ForceUniqueLineItemKey");
            DropColumn("Purchasing.PurchaseOrderItem", "ForceUniqueLineItemKey");
            DropColumn("Quoting.SalesQuoteItem", "ForceUniqueLineItemKey");
            DropColumn("Returning.SalesReturnItem", "ForceUniqueLineItemKey");
            DropColumn("Shopping.CartItem", "ForceUniqueLineItemKey");
        }
    }
}
