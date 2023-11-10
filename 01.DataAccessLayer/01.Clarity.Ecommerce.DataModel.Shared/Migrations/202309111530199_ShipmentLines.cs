namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ShipmentLines : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Shipping.ShipmentLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sku = c.String(maxLength: 100, unicode: false),
                        ProductID = c.Int(),
                        Quantity = c.Decimal(precision: 18, scale: 4),
                        Description = c.String(maxLength: 100, unicode: false),
                        ShipmentID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Shipping.Shipment", t => t.ShipmentID)
                .Index(t => t.ID)
                .Index(t => t.ProductID)
                .Index(t => t.ShipmentID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);
        }
        public override void Down()
        {
            DropForeignKey("Shipping.ShipmentLines", "ShipmentID", "Shipping.Shipment");
            DropForeignKey("Shipping.ShipmentLines", "ProductID", "Products.Product");
            DropIndex("Shipping.ShipmentLines", new[] { "Hash" });
            DropIndex("Shipping.ShipmentLines", new[] { "Active" });
            DropIndex("Shipping.ShipmentLines", new[] { "UpdatedDate" });
            DropIndex("Shipping.ShipmentLines", new[] { "CreatedDate" });
            DropIndex("Shipping.ShipmentLines", new[] { "CustomKey" });
            DropIndex("Shipping.ShipmentLines", new[] { "ShipmentID" });
            DropIndex("Shipping.ShipmentLines", new[] { "ProductID" });
            DropIndex("Shipping.ShipmentLines", new[] { "ID" });
            DropTable("Shipping.ShipmentLines");
        }
    }
}
