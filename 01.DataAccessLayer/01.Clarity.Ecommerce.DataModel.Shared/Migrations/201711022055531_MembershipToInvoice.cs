// <copyright file="201711022055531_MembershipToInvoice.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201711022055531 membership to invoice class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MembershipToInvoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Payments.Subscription", "PaymentID", "Payments.Payment");
            DropIndex("Payments.Subscription", new[] { "PaymentID" });
            AddColumn("Payments.Subscription", "SalesInvoiceID", c => c.Int());
            CreateIndex("Payments.Subscription", "SalesInvoiceID");
            AddForeignKey("Payments.Subscription", "SalesInvoiceID", "Invoicing.SalesInvoice", "ID");
            DropColumn("Payments.Subscription", "PaymentID");
        }

        public override void Down()
        {
            AddColumn("Payments.Subscription", "PaymentID", c => c.Int());
            DropForeignKey("Payments.Subscription", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropIndex("Payments.Subscription", new[] { "SalesInvoiceID" });
            DropColumn("Payments.Subscription", "SalesInvoiceID");
            CreateIndex("Payments.Subscription", "PaymentID");
            AddForeignKey("Payments.Subscription", "PaymentID", "Payments.Payment", "ID");
        }
    }
}
