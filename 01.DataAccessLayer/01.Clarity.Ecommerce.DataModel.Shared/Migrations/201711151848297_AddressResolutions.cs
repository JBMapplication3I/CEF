// <copyright file="201711151848297_AddressResolutions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201711151848297 address resolutions class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddressResolutions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Vendors.Vendor", "AddressID", "Geography.Address");
            DropForeignKey("Shopping.Cart", "ShippingAddressID", "Geography.Address");
            DropForeignKey("Contacts.Contact", "ShippingAddressID", "Geography.Address");
            DropIndex("Contacts.Contact", new[] { "ShippingAddressID" });
            DropIndex("Shopping.Cart", new[] { "ShippingAddressID" });
            DropIndex("Vendors.Vendor", new[] { "AddressID" });
            AddColumn("Manufacturers.Manufacturer", "ContactID", c => c.Int());
            AddColumn("Inventory.InventoryLocation", "ContactID", c => c.Int());
            AddColumn("Shipping.ShipCarrier", "ContactID", c => c.Int());
            CreateIndex("Manufacturers.Manufacturer", "ContactID");
            CreateIndex("Inventory.InventoryLocation", "ContactID");
            CreateIndex("Shipping.ShipCarrier", "ContactID");
            AddForeignKey("Manufacturers.Manufacturer", "ContactID", "Contacts.Contact", "ID");
            AddForeignKey("Inventory.InventoryLocation", "ContactID", "Contacts.Contact", "ID");
            AddForeignKey("Shipping.ShipCarrier", "ContactID", "Contacts.Contact", "ID");
            DropColumn("Contacts.Contact", "ShippingAddressID");
            DropColumn("Shopping.Cart", "ShippingAddressID");
            DropColumn("Vendors.Vendor", "AddressID");
        }

        public override void Down()
        {
            AddColumn("Vendors.Vendor", "AddressID", c => c.Int());
            AddColumn("Shopping.Cart", "ShippingAddressID", c => c.Int());
            AddColumn("Contacts.Contact", "ShippingAddressID", c => c.Int());
            DropForeignKey("Shipping.ShipCarrier", "ContactID", "Contacts.Contact");
            DropForeignKey("Inventory.InventoryLocation", "ContactID", "Contacts.Contact");
            DropForeignKey("Manufacturers.Manufacturer", "ContactID", "Contacts.Contact");
            DropIndex("Shipping.ShipCarrier", new[] { "ContactID" });
            DropIndex("Inventory.InventoryLocation", new[] { "ContactID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "ContactID" });
            DropColumn("Shipping.ShipCarrier", "ContactID");
            DropColumn("Inventory.InventoryLocation", "ContactID");
            DropColumn("Manufacturers.Manufacturer", "ContactID");
            CreateIndex("Vendors.Vendor", "AddressID");
            CreateIndex("Shopping.Cart", "ShippingAddressID");
            CreateIndex("Contacts.Contact", "ShippingAddressID");
            AddForeignKey("Contacts.Contact", "ShippingAddressID", "Geography.Address", "ID");
            AddForeignKey("Shopping.Cart", "ShippingAddressID", "Geography.Address", "ID");
            AddForeignKey("Vendors.Vendor", "AddressID", "Geography.Address", "ID");
        }
    }
}
