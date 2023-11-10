// <copyright file="201708281909147_CartShipment.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201708281909147 cart shipment class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CartShipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("Shopping.Cart", "ShipmentID", c => c.Int());
            CreateIndex("Shopping.Cart", "ShipmentID");
            AddForeignKey("Shopping.Cart", "ShipmentID", "Shipping.Shipment", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Shopping.Cart", "ShipmentID", "Shipping.Shipment");
            DropIndex("Shopping.Cart", new[] { "ShipmentID" });
            DropColumn("Shopping.Cart", "ShipmentID");
        }
    }
}
