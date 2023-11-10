// <copyright file="201912310626422_BufferSkusEverywhere.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201912310626422 buffer skus everywhere class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class BufferSkusEverywhere : DbMigration
    {
        public override void Up()
        {
            AddColumn("Stores.Store", "MinimumOrderDollarAmountBufferProductID", c => c.Int());
            AddColumn("Stores.Store", "MinimumOrderQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Stores.Store", "MinimumOrderDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Stores.Store", "MinimumOrderQuantityAmountBufferCategoryID", c => c.Int());
            AddColumn("Stores.Store", "MinimumForFreeShippingDollarAmountBufferProductID", c => c.Int());
            AddColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Stores.Store", "MinimumForFreeShippingDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferCategoryID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferProductID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferCategoryID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferProductID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferCategoryID", c => c.Int());
            AddColumn("Vendors.Vendor", "MinimumOrderDollarAmountBufferProductID", c => c.Int());
            AddColumn("Vendors.Vendor", "MinimumOrderQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Vendors.Vendor", "MinimumOrderDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Vendors.Vendor", "MinimumOrderQuantityAmountBufferCategoryID", c => c.Int());
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferProductID", c => c.Int());
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferCategoryID", c => c.Int());
            AddColumn("Categories.Category", "MinimumForFreeShippingDollarAmountBufferProductID", c => c.Int());
            AddColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferProductID", c => c.Int());
            AddColumn("Categories.Category", "MinimumForFreeShippingDollarAmountBufferCategoryID", c => c.Int());
            AddColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferCategoryID", c => c.Int());
            CreateIndex("Stores.Store", "MinimumOrderDollarAmountBufferProductID");
            CreateIndex("Stores.Store", "MinimumOrderQuantityAmountBufferProductID");
            CreateIndex("Stores.Store", "MinimumOrderDollarAmountBufferCategoryID");
            CreateIndex("Stores.Store", "MinimumOrderQuantityAmountBufferCategoryID");
            CreateIndex("Stores.Store", "MinimumForFreeShippingDollarAmountBufferProductID");
            CreateIndex("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferProductID");
            CreateIndex("Stores.Store", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            CreateIndex("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            CreateIndex("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferProductID");
            CreateIndex("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferProductID");
            CreateIndex("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferCategoryID");
            CreateIndex("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferCategoryID");
            CreateIndex("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferProductID");
            CreateIndex("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferProductID");
            CreateIndex("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            CreateIndex("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            CreateIndex("Categories.Category", "MinimumForFreeShippingDollarAmountBufferProductID");
            CreateIndex("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferProductID");
            CreateIndex("Categories.Category", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            CreateIndex("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            CreateIndex("Vendors.Vendor", "MinimumOrderDollarAmountBufferProductID");
            CreateIndex("Vendors.Vendor", "MinimumOrderQuantityAmountBufferProductID");
            CreateIndex("Vendors.Vendor", "MinimumOrderDollarAmountBufferCategoryID");
            CreateIndex("Vendors.Vendor", "MinimumOrderQuantityAmountBufferCategoryID");
            CreateIndex("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferProductID");
            CreateIndex("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferProductID");
            CreateIndex("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            CreateIndex("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            AddForeignKey("Categories.Category", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Vendors.Vendor", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Vendors.Vendor", "MinimumOrderDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Vendors.Vendor", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Vendors.Vendor", "MinimumOrderQuantityAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Categories.Category", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Stores.Store", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Stores.Store", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Stores.Store", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Stores.Store", "MinimumOrderDollarAmountBufferProductID", "Products.Product", "ID");
            AddForeignKey("Stores.Store", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category", "ID");
            AddForeignKey("Stores.Store", "MinimumOrderQuantityAmountBufferProductID", "Products.Product", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Stores.Store", "MinimumOrderQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Stores.Store", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Stores.Store", "MinimumOrderDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Stores.Store", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Stores.Store", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Stores.Store", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Categories.Category", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Vendors.Vendor", "MinimumOrderQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Vendors.Vendor", "MinimumOrderQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Vendors.Vendor", "MinimumOrderDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Vendors.Vendor", "MinimumOrderDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferProductID", "Products.Product");
            DropForeignKey("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferProductID", "Products.Product");
            DropForeignKey("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category");
            DropForeignKey("Categories.Category", "MinimumForFreeShippingDollarAmountBufferCategoryID", "Categories.Category");
            DropIndex("Vendors.Vendor", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("Vendors.Vendor", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("Vendors.Vendor", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("Vendors.Vendor", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("Vendors.Vendor", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("Vendors.Vendor", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("Vendors.Vendor", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("Vendors.Vendor", new[] { "MinimumOrderDollarAmountBufferProductID" });
            DropIndex("Categories.Category", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("Categories.Category", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("Categories.Category", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("Categories.Category", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "MinimumOrderDollarAmountBufferProductID" });
            DropIndex("Stores.Store", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("Stores.Store", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("Stores.Store", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("Stores.Store", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("Stores.Store", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("Stores.Store", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("Stores.Store", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("Stores.Store", new[] { "MinimumOrderDollarAmountBufferProductID" });
            DropColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            DropColumn("Categories.Category", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            DropColumn("Categories.Category", "MinimumForFreeShippingQuantityAmountBufferProductID");
            DropColumn("Categories.Category", "MinimumForFreeShippingDollarAmountBufferProductID");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingQuantityAmountBufferProductID");
            DropColumn("Vendors.Vendor", "MinimumForFreeShippingDollarAmountBufferProductID");
            DropColumn("Vendors.Vendor", "MinimumOrderQuantityAmountBufferCategoryID");
            DropColumn("Vendors.Vendor", "MinimumOrderDollarAmountBufferCategoryID");
            DropColumn("Vendors.Vendor", "MinimumOrderQuantityAmountBufferProductID");
            DropColumn("Vendors.Vendor", "MinimumOrderDollarAmountBufferProductID");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingQuantityAmountBufferProductID");
            DropColumn("Manufacturers.Manufacturer", "MinimumForFreeShippingDollarAmountBufferProductID");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferCategoryID");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferCategoryID");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderQuantityAmountBufferProductID");
            DropColumn("Manufacturers.Manufacturer", "MinimumOrderDollarAmountBufferProductID");
            DropColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferCategoryID");
            DropColumn("Stores.Store", "MinimumForFreeShippingDollarAmountBufferCategoryID");
            DropColumn("Stores.Store", "MinimumForFreeShippingQuantityAmountBufferProductID");
            DropColumn("Stores.Store", "MinimumForFreeShippingDollarAmountBufferProductID");
            DropColumn("Stores.Store", "MinimumOrderQuantityAmountBufferCategoryID");
            DropColumn("Stores.Store", "MinimumOrderDollarAmountBufferCategoryID");
            DropColumn("Stores.Store", "MinimumOrderQuantityAmountBufferProductID");
            DropColumn("Stores.Store", "MinimumOrderDollarAmountBufferProductID");
        }
    }
}
