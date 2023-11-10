// <copyright file="201803201704234_AddRateQuotes.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201803201704234 add rate quotes class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddRateQuotes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Shipping.RateQuote",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        JsonAttributes = c.String(),
                        EstimatedDeliveryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Rate = c.Decimal(precision: 18, scale: 4),
                        CartHash = c.Long(),
                        RateTimestamp = c.DateTime(precision: 7, storeType: "datetime2"),
                        Selected = c.Boolean(nullable: false),
                        ShipCarrierMethodID = c.Int(nullable: false),
                        CartID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shopping.Cart", t => t.CartID)
                .ForeignKey("Shipping.ShipCarrierMethod", t => t.ShipCarrierMethodID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.ShipCarrierMethodID)
                .Index(t => t.CartID);

            AddColumn("Invoicing.SalesInvoice", "RateQuoteID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "RateQuoteID", c => c.Int());
            AddColumn("Sampling.SampleRequest", "RateQuoteID", c => c.Int());
            CreateIndex("Invoicing.SalesInvoice", "RateQuoteID");
            CreateIndex("Quoting.SalesQuote", "RateQuoteID");
            CreateIndex("Sampling.SampleRequest", "RateQuoteID");
            AddForeignKey("Quoting.SalesQuote", "RateQuoteID", "Shipping.RateQuote", "ID");
            AddForeignKey("Sampling.SampleRequest", "RateQuoteID", "Shipping.RateQuote", "ID");
            AddForeignKey("Invoicing.SalesInvoice", "RateQuoteID", "Shipping.RateQuote", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Invoicing.SalesInvoice", "RateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Sampling.SampleRequest", "RateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Quoting.SalesQuote", "RateQuoteID", "Shipping.RateQuote");
            DropForeignKey("Shipping.RateQuote", "ShipCarrierMethodID", "Shipping.ShipCarrierMethod");
            DropForeignKey("Shipping.RateQuote", "CartID", "Shopping.Cart");
            DropIndex("Sampling.SampleRequest", new[] { "RateQuoteID" });
            DropIndex("Shipping.RateQuote", new[] { "CartID" });
            DropIndex("Shipping.RateQuote", new[] { "ShipCarrierMethodID" });
            DropIndex("Shipping.RateQuote", new[] { "Name" });
            DropIndex("Shipping.RateQuote", new[] { "Hash" });
            DropIndex("Shipping.RateQuote", new[] { "Active" });
            DropIndex("Shipping.RateQuote", new[] { "UpdatedDate" });
            DropIndex("Shipping.RateQuote", new[] { "CustomKey" });
            DropIndex("Shipping.RateQuote", new[] { "ID" });
            DropIndex("Quoting.SalesQuote", new[] { "RateQuoteID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "RateQuoteID" });
            DropColumn("Sampling.SampleRequest", "RateQuoteID");
            DropColumn("Quoting.SalesQuote", "RateQuoteID");
            DropColumn("Invoicing.SalesInvoice", "RateQuoteID");
            DropTable("Shipping.RateQuote");
        }
    }
}
