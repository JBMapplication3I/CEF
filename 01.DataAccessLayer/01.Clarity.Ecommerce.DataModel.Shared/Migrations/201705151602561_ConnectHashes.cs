// <copyright file="201705151602561_ConnectHashes.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201705151602561 connect hashes class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ConnectHashes : DbMigration
    {
        public override void Up()
        {
            AddColumn("Attributes.GeneralAttribute", "DisplayName", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Attributes.GeneralAttribute", "SortOrder", c => c.Int());
            AddColumn("Attributes.GeneralAttribute", "Group", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Accounts.Account", "Hash", c => c.Long());
            AddColumn("Geography.Country", "Hash", c => c.Long());
            AddColumn("Geography.Region", "Hash", c => c.Long());
            AddColumn("Purchasing.PurchaseOrder", "Hash", c => c.Long());
            AddColumn("Ordering.SalesOrder", "Hash", c => c.Long());
            AddColumn("Invoicing.SalesInvoice", "Hash", c => c.Long());
            AddColumn("Contacts.User", "Hash", c => c.Long());
            AddColumn("Manufacturers.Manufacturer", "Hash", c => c.Long());
            AddColumn("Stores.Store", "Hash", c => c.Long());
            AddColumn("Inventory.InventoryLocation", "Hash", c => c.Long());
            AddColumn("Shipping.Shipment", "Hash", c => c.Long());
            AddColumn("Payments.Subscription", "Hash", c => c.Long());
            AddColumn("Payments.Payment", "Hash", c => c.Long());
            AddColumn("Quoting.SalesQuote", "Hash", c => c.Long());
            AddColumn("Sampling.SampleRequest", "Hash", c => c.Long());
            AddColumn("Products.ProductPricePoint", "Hash", c => c.Long());
            CreateIndex("Attributes.GeneralAttribute", "DisplayName");
            CreateIndex("Attributes.GeneralAttribute", "SortOrder");
        }

        public override void Down()
        {
            DropIndex("Attributes.GeneralAttribute", new[] { "SortOrder" });
            DropIndex("Attributes.GeneralAttribute", new[] { "DisplayName" });
            DropColumn("Products.ProductPricePoint", "Hash");
            DropColumn("Sampling.SampleRequest", "Hash");
            DropColumn("Quoting.SalesQuote", "Hash");
            DropColumn("Payments.Payment", "Hash");
            DropColumn("Payments.Subscription", "Hash");
            DropColumn("Shipping.Shipment", "Hash");
            DropColumn("Inventory.InventoryLocation", "Hash");
            DropColumn("Stores.Store", "Hash");
            DropColumn("Manufacturers.Manufacturer", "Hash");
            DropColumn("Contacts.User", "Hash");
            DropColumn("Invoicing.SalesInvoice", "Hash");
            DropColumn("Ordering.SalesOrder", "Hash");
            DropColumn("Purchasing.PurchaseOrder", "Hash");
            DropColumn("Geography.Region", "Hash");
            DropColumn("Geography.Country", "Hash");
            DropColumn("Accounts.Account", "Hash");
            DropColumn("Attributes.GeneralAttribute", "Group");
            DropColumn("Attributes.GeneralAttribute", "SortOrder");
            DropColumn("Attributes.GeneralAttribute", "DisplayName");
        }
    }
}
