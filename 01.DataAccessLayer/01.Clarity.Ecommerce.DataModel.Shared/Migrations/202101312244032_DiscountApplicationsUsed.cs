// <copyright file="202101312244032_DiscountApplicationsUsed.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202101312244032 discount applications used class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DiscountApplicationsUsed : DbMigration
    {
        public override void Up()
        {
            AddColumn("Discounts.CartItemDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.CartDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesQuoteDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesReturnDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesReturnItemDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesQuoteItemDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SampleRequestDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SampleRequestItemDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.PurchaseOrderItemDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesInvoiceItemDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesOrderItemDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesInvoiceDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.SalesOrderDiscounts", "ApplicationsUsed", c => c.Int());
            AddColumn("Discounts.PurchaseOrderDiscounts", "ApplicationsUsed", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("Discounts.PurchaseOrderDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SalesOrderDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SalesInvoiceDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SalesOrderItemDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SalesInvoiceItemDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.PurchaseOrderItemDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SampleRequestItemDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SampleRequestDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SalesQuoteItemDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SalesReturnItemDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SalesReturnDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.SalesQuoteDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.CartDiscounts", "ApplicationsUsed");
            DropColumn("Discounts.CartItemDiscounts", "ApplicationsUsed");
        }
    }
}
