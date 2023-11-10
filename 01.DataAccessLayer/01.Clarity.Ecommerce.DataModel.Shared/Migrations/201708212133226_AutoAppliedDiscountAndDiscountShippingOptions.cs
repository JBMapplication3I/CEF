// <copyright file="201708212133226_AutoAppliedDiscountAndDiscountShippingOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201708212133226 automatic applied discount and discount shipping options class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AutoAppliedDiscountAndDiscountShippingOptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Discounts.DiscountShipCarrierMethods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        DiscountID = c.Int(nullable: false),
                        ShipCarrierMethodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("Shipping.ShipCarrierMethod", t => t.ShipCarrierMethodID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.DiscountID)
                .Index(t => t.ShipCarrierMethodID);

            AddColumn("Discounts.Discount", "IsAutoApplied", c => c.Boolean(nullable: false));
            AddColumn("Discounts.Discount", "DiscountCompareOperator", c => c.Int());
            AddColumn("Discounts.Discount", "ThresholdAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }

        public override void Down()
        {
            DropForeignKey("Discounts.DiscountShipCarrierMethods", "ShipCarrierMethodID", "Shipping.ShipCarrierMethod");
            DropForeignKey("Discounts.DiscountShipCarrierMethods", "DiscountID", "Discounts.Discount");
            DropIndex("Discounts.DiscountShipCarrierMethods", new[] { "ShipCarrierMethodID" });
            DropIndex("Discounts.DiscountShipCarrierMethods", new[] { "DiscountID" });
            DropIndex("Discounts.DiscountShipCarrierMethods", new[] { "Hash" });
            DropIndex("Discounts.DiscountShipCarrierMethods", new[] { "Active" });
            DropIndex("Discounts.DiscountShipCarrierMethods", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountShipCarrierMethods", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountShipCarrierMethods", new[] { "ID" });
            DropColumn("Discounts.Discount", "ThresholdAmount");
            DropColumn("Discounts.Discount", "DiscountCompareOperator");
            DropColumn("Discounts.Discount", "IsAutoApplied");
            DropTable("Discounts.DiscountShipCarrierMethods");
        }
    }
}
