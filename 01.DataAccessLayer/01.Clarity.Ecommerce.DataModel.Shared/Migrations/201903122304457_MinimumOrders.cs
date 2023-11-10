// <copyright file="201903122304457_MinimumOrders.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201903122304457 minimum orders class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MinimumOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("Geography.Country", "PhoneRegEx", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Geography.Country", "PhonePrefix", c => c.String(maxLength: 10, unicode: false));
            AddColumn("Categories.Category", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 512));
            AddColumn("Categories.Category", "MinimumOrderQuantityAmountWarningMessage", c => c.String(maxLength: 512));
            AddColumn("Categories.Category", "MinimumOrderDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumOrderQuantityAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumOrderDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumOrderQuantityAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "MinimumOrderDollarAmountBufferProductID", c => c.Int());
            AddColumn("Categories.Category", "MinimumOrderQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Categories.Category", "MinimumOrderDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Categories.Category", "MinimumOrderQuantityAmountBufferCategoryID", c => c.Int());
            AddColumn("Vendors.Vendor", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 512));
            AddColumn("Vendors.Vendor", "MinimumOrderDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Vendors.Vendor", "MinimumOrderDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("Vendors.Vendor", "AccountNumber", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("Vendors.Vendor", "Terms", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("Vendors.Vendor", "SendMethod", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("Vendors.Vendor", "EmailSubject", c => c.String(maxLength: 300, unicode: false));
            AlterColumn("Vendors.Vendor", "ShipTo", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("Vendors.Vendor", "SignBy", c => c.String(maxLength: 100, unicode: false));
            CreateIndex("Categories.Category", "MinimumOrderDollarAmountBufferProductID");
            CreateIndex("Categories.Category", "MinimumOrderQuantityAmountBufferProductID");
            CreateIndex("Categories.Category", "MinimumOrderDollarAmountBufferCategoryID");
            CreateIndex("Categories.Category", "MinimumOrderQuantityAmountBufferCategoryID");
            AddForeignKey("Categories.Category", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Categories.Category", "MinimumOrderDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Categories.Category", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Categories.Category", "MinimumOrderQuantityAmountBufferProductID", "Products.Product", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Categories.Category", "MinimumOrderQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Categories.Category", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Categories.Category", "MinimumOrderDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Categories.Category", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category");
            DropIndex("Categories.Category", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("Categories.Category", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("Categories.Category", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("Categories.Category", new[] { "MinimumOrderDollarAmountBufferProductID" });
            AlterColumn("Vendors.Vendor", "SignBy", c => c.String(maxLength: 100));
            AlterColumn("Vendors.Vendor", "ShipTo", c => c.String(maxLength: 100));
            AlterColumn("Vendors.Vendor", "EmailSubject", c => c.String(maxLength: 300));
            AlterColumn("Vendors.Vendor", "SendMethod", c => c.String(maxLength: 100));
            AlterColumn("Vendors.Vendor", "Terms", c => c.String(maxLength: 100));
            AlterColumn("Vendors.Vendor", "AccountNumber", c => c.String(maxLength: 100));
            DropColumn("Vendors.Vendor", "MinimumOrderDollarAmountAfter");
            DropColumn("Vendors.Vendor", "MinimumOrderDollarAmount");
            DropColumn("Vendors.Vendor", "MinimumOrderDollarAmountWarningMessage");
            DropColumn("Categories.Category", "MinimumOrderQuantityAmountBufferCategoryID");
            DropColumn("Categories.Category", "MinimumOrderDollarAmountBufferCategoryID");
            DropColumn("Categories.Category", "MinimumOrderQuantityAmountBufferProductID");
            DropColumn("Categories.Category", "MinimumOrderDollarAmountBufferProductID");
            DropColumn("Categories.Category", "MinimumOrderQuantityAmountAfter");
            DropColumn("Categories.Category", "MinimumOrderDollarAmountAfter");
            DropColumn("Categories.Category", "MinimumOrderQuantityAmount");
            DropColumn("Categories.Category", "MinimumOrderDollarAmount");
            DropColumn("Categories.Category", "MinimumOrderQuantityAmountWarningMessage");
            DropColumn("Categories.Category", "MinimumOrderDollarAmountWarningMessage");
            DropColumn("Geography.Country", "PhonePrefix");
            DropColumn("Geography.Country", "PhoneRegEx");
        }
    }
}
