// <copyright file="201804022337061_QuoteResponseAs.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201804022337061 quote response as class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class QuoteResponseAs : DbMigration
    {
        public override void Up()
        {
            AddColumn("Quoting.SalesQuote", "ResponseAsVendorID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "ResponseAsStoreID", c => c.Int());
            CreateIndex("Quoting.SalesQuote", "ResponseAsVendorID");
            CreateIndex("Quoting.SalesQuote", "ResponseAsStoreID");
            AddForeignKey("Quoting.SalesQuote", "ResponseAsStoreID", "Stores.Store", "ID");
            AddForeignKey("Quoting.SalesQuote", "ResponseAsVendorID", "Vendors.Vendor", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Quoting.SalesQuote", "ResponseAsVendorID", "Vendors.Vendor");
            DropForeignKey("Quoting.SalesQuote", "ResponseAsStoreID", "Stores.Store");
            DropIndex("Quoting.SalesQuote", new[] { "ResponseAsStoreID" });
            DropIndex("Quoting.SalesQuote", new[] { "ResponseAsVendorID" });
            DropColumn("Quoting.SalesQuote", "ResponseAsStoreID");
            DropColumn("Quoting.SalesQuote", "ResponseAsVendorID");
        }
    }
}
