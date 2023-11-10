// <copyright file="201812051727294_TargetsWithNothingToShip.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201812051727294 targets with nothing to ship class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TargetsWithNothingToShip : DbMigration
    {
        public override void Up()
        {
            AddColumn("Returning.SalesReturnItemTarget", "NothingToShip", c => c.Boolean(nullable: false));
            AddColumn("Quoting.SalesQuoteItemTarget", "NothingToShip", c => c.Boolean(nullable: false));
            AddColumn("Purchasing.PurchaseOrderItemTarget", "NothingToShip", c => c.Boolean(nullable: false));
            AddColumn("Invoicing.SalesInvoiceItemTarget", "NothingToShip", c => c.Boolean(nullable: false));
            AddColumn("Ordering.SalesOrderItemTarget", "NothingToShip", c => c.Boolean(nullable: false));
            AddColumn("Sampling.SampleRequestItemTarget", "NothingToShip", c => c.Boolean(nullable: false));
            AddColumn("Shopping.CartItemTarget", "NothingToShip", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("Shopping.CartItemTarget", "NothingToShip");
            DropColumn("Sampling.SampleRequestItemTarget", "NothingToShip");
            DropColumn("Ordering.SalesOrderItemTarget", "NothingToShip");
            DropColumn("Invoicing.SalesInvoiceItemTarget", "NothingToShip");
            DropColumn("Purchasing.PurchaseOrderItemTarget", "NothingToShip");
            DropColumn("Quoting.SalesQuoteItemTarget", "NothingToShip");
            DropColumn("Returning.SalesReturnItemTarget", "NothingToShip");
        }
    }
}
