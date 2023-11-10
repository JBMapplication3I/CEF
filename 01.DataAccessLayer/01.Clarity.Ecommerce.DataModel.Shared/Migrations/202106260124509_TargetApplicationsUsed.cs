// <copyright file="202106260124509_TargetApplicationsUsed.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202106260124509 target applications used class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TargetApplicationsUsed : DbMigration
    {
        public override void Up()
        {
            AddColumn("Discounts.CartItemDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.CartDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesQuoteDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesReturnDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesReturnItemDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SampleRequestDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SampleRequestItemDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesQuoteItemDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.PurchaseOrderItemDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesInvoiceItemDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesOrderItemDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesInvoiceDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesOrderDiscounts", "TargetApplicationsUsed", c => c.Int());
            AddColumn("Discounts.PurchaseOrderDiscounts", "TargetApplicationsUsed", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("Discounts.PurchaseOrderDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SalesOrderDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SalesInvoiceDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SalesOrderItemDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SalesInvoiceItemDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.PurchaseOrderItemDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SalesQuoteItemDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SampleRequestItemDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SampleRequestDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SalesReturnItemDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SalesReturnDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.SalesQuoteDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.CartDiscounts", "TargetApplicationsUsed");
            DropColumn("Discounts.CartItemDiscounts", "TargetApplicationsUsed");
        }
    }
}
