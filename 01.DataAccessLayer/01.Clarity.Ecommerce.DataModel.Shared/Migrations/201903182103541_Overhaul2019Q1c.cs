// <copyright file="201903182103541_Overhaul2019Q1c.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201903182103541 overhaul 2019 q 1c class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    // ReSharper disable once InconsistentNaming
    public partial class Overhaul2019Q1c : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Advertising.AdImageNew", newName: "AdImage");
            RenameTable(name: "CalendarEvents.CalendarEventFileNew", newName: "CalendarEventFile");
            RenameTable(name: "Categories.CategoryFileNew", newName: "CategoryFile");
            RenameTable(name: "Categories.CategoryImageNew", newName: "CategoryImage");
            RenameTable(name: "Invoicing.SalesInvoiceFileNew", newName: "SalesInvoiceFile");
            RenameTable(name: "Ordering.SalesOrderFileNew", newName: "SalesOrderFile");
            RenameTable(name: "Products.ProductFileNew", newName: "ProductFile");
            RenameTable(name: "Products.ProductImageNew", newName: "ProductImage");
            RenameTable(name: "Purchasing.PurchaseOrderFileNew", newName: "PurchaseOrderFile");
            RenameTable(name: "Quoting.SalesQuoteFileNew", newName: "SalesQuoteFile");
            RenameTable(name: "Returning.SalesReturnFileNew", newName: "SalesReturnFile");
            RenameTable(name: "Sampling.SampleRequestFileNew", newName: "SampleRequestFile");
            RenameTable(name: "Shopping.CartFileNew", newName: "CartFile");
            RenameTable(name: "Stores.StoreImageNew", newName: "StoreImage");
            DropIndex("Vendors.VendorTerm", new[] { "ID" });
            DropIndex("Vendors.VendorTerm", new[] { "CustomKey" });
            DropIndex("Vendors.VendorTerm", new[] { "CreatedDate" });
            DropIndex("Vendors.VendorTerm", new[] { "UpdatedDate" });
            DropIndex("Vendors.VendorTerm", new[] { "Active" });
            DropIndex("Vendors.VendorTerm", new[] { "Hash" });
            DropIndex("Vendors.VendorTerm", new[] { "Name" });
            DropTable("Vendors.VendorTerm");
        }

        public override void Down()
        {
            CreateTable(
                "Vendors.VendorTerm",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateIndex("Vendors.VendorTerm", "Name");
            CreateIndex("Vendors.VendorTerm", "Hash");
            CreateIndex("Vendors.VendorTerm", "Active");
            CreateIndex("Vendors.VendorTerm", "UpdatedDate");
            CreateIndex("Vendors.VendorTerm", "CreatedDate");
            CreateIndex("Vendors.VendorTerm", "CustomKey");
            CreateIndex("Vendors.VendorTerm", "ID");
            RenameTable(name: "Stores.StoreImage", newName: "StoreImageNew");
            RenameTable(name: "Shopping.CartFile", newName: "CartFileNew");
            RenameTable(name: "Sampling.SampleRequestFile", newName: "SampleRequestFileNew");
            RenameTable(name: "Returning.SalesReturnFile", newName: "SalesReturnFileNew");
            RenameTable(name: "Quoting.SalesQuoteFile", newName: "SalesQuoteFileNew");
            RenameTable(name: "Purchasing.PurchaseOrderFile", newName: "PurchaseOrderFileNew");
            RenameTable(name: "Products.ProductImage", newName: "ProductImageNew");
            RenameTable(name: "Products.ProductFile", newName: "ProductFileNew");
            RenameTable(name: "Ordering.SalesOrderFile", newName: "SalesOrderFileNew");
            RenameTable(name: "Invoicing.SalesInvoiceFile", newName: "SalesInvoiceFileNew");
            RenameTable(name: "Categories.CategoryImage", newName: "CategoryImageNew");
            RenameTable(name: "Categories.CategoryFile", newName: "CategoryFileNew");
            RenameTable(name: "CalendarEvents.CalendarEventFile", newName: "CalendarEventFileNew");
            RenameTable(name: "Advertising.AdImage", newName: "AdImageNew");
        }
    }
}
