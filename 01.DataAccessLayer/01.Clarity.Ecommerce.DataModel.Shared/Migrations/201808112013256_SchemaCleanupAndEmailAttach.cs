// <copyright file="201808112013256_SchemaCleanupAndEmailAttach.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201808112013256 schema cleanup and email attach class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SchemaCleanupAndEmailAttach : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Media.LibraryType", newName: "StoreInventoryLocationType");
            MoveTable(name: "Media.StoreInventoryLocationType", newSchema: "Stores");
            DropForeignKey("Media.FileData", "ID", "Media.File");
            DropForeignKey("Media.Image", "FullFileID", "Media.File");
            DropForeignKey("Media.Image", "ID", "Media.Library");
            DropForeignKey("Media.Image", "ThumbFileID", "Media.File");
            DropForeignKey("Media.Audio", "ClipFileID", "Media.File");
            DropForeignKey("Media.Audio", "FullFileID", "Media.File");
            DropForeignKey("Media.Audio", "ID", "Media.Library");
            DropForeignKey("Media.Document", "FileID", "Media.File");
            DropForeignKey("Media.Document", "ID", "Media.Library");
            DropForeignKey("Media.Library", "TypeID", "Media.LibraryType");
            DropForeignKey("Media.Video", "FileID", "Media.File");
            DropForeignKey("Media.Video", "ID", "Media.Library");
            DropForeignKey("Currencies.Currency", "LibraryID", "Media.Library");
            DropForeignKey("Globalization.Language", "LibraryID", "Media.Library");
            DropForeignKey("Geography.Country", "LibraryID", "Media.Library");
            DropForeignKey("Geography.District", "LibraryID", "Media.Library");
            DropForeignKey("Geography.Region", "LibraryID", "Media.Library");
            DropForeignKey("Categories.CategoryImage", "CategoryID", "Categories.Category");
            DropForeignKey("Categories.CategoryImage", "LibraryID", "Media.Library");
            DropForeignKey("Categories.CategoryFile", "CategoryID", "Categories.Category");
            DropForeignKey("Categories.CategoryFile", "FileID", "Media.File");
            DropForeignKey("Shopping.CartFile", "CartID", "Shopping.Cart");
            DropForeignKey("Shopping.CartFile", "FileID", "Media.File");
            DropForeignKey("Messaging.MessageAttachment", "LibraryID", "Media.Library");
            DropForeignKey("Manufacturers.Manufacturer", "AddressID", "Geography.Address");
            DropForeignKey("Inventory.InventoryLocation", "AddressID", "Geography.Address");
            DropForeignKey("Shipping.ShipCarrier", "AddressID", "Geography.Address");
            DropForeignKey("Quoting.SalesQuoteFile", "FileID", "Media.File");
            DropForeignKey("Quoting.SalesQuoteFile", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("Returning.SalesReturnFile", "FileID", "Media.File");
            DropForeignKey("Returning.SalesReturnFile", "SalesReturnID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Returning.SalesReturnItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Returning.SalesReturnItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Quoting.SalesQuoteItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Quoting.SalesQuoteItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Shipping.ShipOption", "ShipCarrierMethodID", "Shipping.ShipCarrierMethod");
            DropForeignKey("Quoting.SalesQuote", "ShipOptionID", "Shipping.ShipOption");
            DropForeignKey("CalendarEvents.CalendarEventFile", "CalendarEventID", "CalendarEvents.CalendarEvent");
            DropForeignKey("CalendarEvents.CalendarEventFile", "FileID", "Media.File");
            DropForeignKey("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Purchasing.PurchaseOrderItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Purchasing.PurchaseOrderItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Invoicing.SalesInvoiceItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Invoicing.SalesInvoiceItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Ordering.SalesOrderItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Ordering.SalesOrderItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Sampling.SampleRequestFile", "FileID", "Media.File");
            DropForeignKey("Sampling.SampleRequestFile", "SampleRequestID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Sampling.SampleRequestItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Sampling.SampleRequestItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Sampling.SampleRequest", "ShipOptionID", "Shipping.ShipOption");
            DropForeignKey("Shopping.Cart", "ShipOptionID", "Shipping.ShipOption");
            DropForeignKey("Shopping.CartItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Shopping.CartItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Shopping.CartItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Products.ProductFile", "FileID", "Media.File");
            DropForeignKey("Products.ProductFile", "ProductID", "Products.Product");
            DropForeignKey("Products.ProductImage", "LibraryID", "Media.Library");
            DropForeignKey("Products.ProductImage", "ProductID", "Products.Product");
            DropForeignKey("Products.ProductImage", "TypeID", "Products.ProductImageType");
            DropForeignKey("Invoicing.SalesInvoiceFile", "FileID", "Media.File");
            DropForeignKey("Invoicing.SalesInvoiceFile", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Invoicing.SalesInvoice", "ShipOptionID", "Shipping.ShipOption");
            DropForeignKey("Ordering.SalesOrderFile", "FileID", "Media.File");
            DropForeignKey("Ordering.SalesOrderFile", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Purchasing.PurchaseOrderFile", "FileID", "Media.File");
            DropForeignKey("Purchasing.PurchaseOrderFile", "PurchaseOrderID", "Purchasing.PurchaseOrder");
            DropForeignKey("Stores.Store", "LogoImageLibraryID", "Media.Library");
            DropForeignKey("Stores.Store", "SellerImageLibraryID", "Media.Library");
            DropForeignKey("Stores.StoreImage", "LibraryID", "Media.Library");
            DropForeignKey("Stores.StoreImage", "StoreID", "Stores.Store");
            DropForeignKey("Advertising.AdImage", "AdID", "Advertising.Ad");
            DropForeignKey("Advertising.AdImage", "LibraryID", "Media.Library");
            DropIndex("Stores.Store", new[] { "LogoImageLibraryID" });
            DropIndex("Stores.Store", new[] { "SellerImageLibraryID" });
            DropIndex("Geography.Country", new[] { "LibraryID" });
            DropIndex("Currencies.Currency", new[] { "LibraryID" });
            DropIndex("Media.Library", new[] { "ID" });
            DropIndex("Media.Library", new[] { "CustomKey" });
            DropIndex("Media.Library", new[] { "UpdatedDate" });
            DropIndex("Media.Library", new[] { "Active" });
            DropIndex("Media.Library", new[] { "Hash" });
            DropIndex("Media.Library", new[] { "Name" });
            DropIndex("Media.Library", new[] { "TypeID" });
            DropIndex("Media.Audio", new[] { "ID" });
            DropIndex("Media.Audio", new[] { "Hash" });
            DropIndex("Media.Audio", new[] { "FullFileID" });
            DropIndex("Media.Audio", new[] { "ClipFileID" });
            DropIndex("Media.File", new[] { "ID" });
            DropIndex("Media.File", new[] { "CustomKey" });
            DropIndex("Media.File", new[] { "UpdatedDate" });
            DropIndex("Media.File", new[] { "Active" });
            DropIndex("Media.File", new[] { "Hash" });
            DropIndex("Media.FileData", new[] { "ID" });
            DropIndex("Media.Image", new[] { "ID" });
            DropIndex("Media.Image", new[] { "FullFileID" });
            DropIndex("Media.Image", new[] { "ThumbFileID" });
            DropIndex("Media.Document", new[] { "ID" });
            DropIndex("Media.Document", new[] { "FileID" });
            DropIndex("Media.Video", new[] { "ID" });
            DropIndex("Media.Video", new[] { "FileID" });
            DropIndex("Globalization.Language", new[] { "LibraryID" });
            DropIndex("Geography.Region", new[] { "LibraryID" });
            DropIndex("Geography.District", new[] { "LibraryID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "ShipOptionID" });
            DropIndex("Categories.CategoryImage", new[] { "ID" });
            DropIndex("Categories.CategoryImage", new[] { "CustomKey" });
            DropIndex("Categories.CategoryImage", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryImage", new[] { "Active" });
            DropIndex("Categories.CategoryImage", new[] { "Hash" });
            DropIndex("Categories.CategoryImage", new[] { "LibraryID" });
            DropIndex("Categories.CategoryImage", new[] { "CategoryID" });
            DropIndex("Categories.CategoryFile", new[] { "ID" });
            DropIndex("Categories.CategoryFile", new[] { "CustomKey" });
            DropIndex("Categories.CategoryFile", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryFile", new[] { "Active" });
            DropIndex("Categories.CategoryFile", new[] { "Hash" });
            DropIndex("Categories.CategoryFile", new[] { "Name" });
            DropIndex("Categories.CategoryFile", new[] { "CategoryID" });
            DropIndex("Categories.CategoryFile", new[] { "FileID" });
            DropIndex("Shopping.CartItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Shopping.CartItem", new[] { "VendorProductID" });
            DropIndex("Shopping.CartItem", new[] { "StoreProductID" });
            DropIndex("Shopping.Cart", "Unq_BySessionTypeUserActive");
            DropIndex("Shopping.Cart", new[] { "ShipOptionID" });
            DropIndex("Shopping.CartFile", new[] { "ID" });
            DropIndex("Shopping.CartFile", new[] { "CustomKey" });
            DropIndex("Shopping.CartFile", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartFile", new[] { "Active" });
            DropIndex("Shopping.CartFile", new[] { "Hash" });
            DropIndex("Shopping.CartFile", new[] { "Name" });
            DropIndex("Shopping.CartFile", new[] { "CartID" });
            DropIndex("Shopping.CartFile", new[] { "FileID" });
            DropIndex("Messaging.MessageAttachment", new[] { "LibraryID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "Phone" });
            DropIndex("Manufacturers.Manufacturer", new[] { "Fax" });
            DropIndex("Manufacturers.Manufacturer", new[] { "Email" });
            DropIndex("Manufacturers.Manufacturer", new[] { "AddressID" });
            DropIndex("Vendors.Vendor", new[] { "Phone" });
            DropIndex("Vendors.Vendor", new[] { "Fax" });
            DropIndex("Vendors.Vendor", new[] { "Email" });
            DropIndex("Inventory.InventoryLocation", new[] { "Phone" });
            DropIndex("Inventory.InventoryLocation", new[] { "Fax" });
            DropIndex("Inventory.InventoryLocation", new[] { "Email" });
            DropIndex("Inventory.InventoryLocation", new[] { "AddressID" });
            DropIndex("Shipping.ShipCarrier", new[] { "Phone" });
            DropIndex("Shipping.ShipCarrier", new[] { "Fax" });
            DropIndex("Shipping.ShipCarrier", new[] { "Email" });
            DropIndex("Shipping.ShipCarrier", new[] { "AddressID" });
            DropIndex("Quoting.SalesQuote", new[] { "ShipOptionID" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "SalesQuoteID" });
            DropIndex("Quoting.SalesQuoteFile", new[] { "FileID" });
            DropIndex("Returning.SalesReturnFile", new[] { "ID" });
            DropIndex("Returning.SalesReturnFile", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnFile", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnFile", new[] { "Active" });
            DropIndex("Returning.SalesReturnFile", new[] { "Hash" });
            DropIndex("Returning.SalesReturnFile", new[] { "Name" });
            DropIndex("Returning.SalesReturnFile", new[] { "SalesReturnID" });
            DropIndex("Returning.SalesReturnFile", new[] { "FileID" });
            DropIndex("Returning.SalesReturnItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Returning.SalesReturnItem", new[] { "VendorProductID" });
            DropIndex("Returning.SalesReturnItem", new[] { "StoreProductID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "VendorProductID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "StoreProductID" });
            DropIndex("Shipping.ShipOption", new[] { "ID" });
            DropIndex("Shipping.ShipOption", new[] { "CustomKey" });
            DropIndex("Shipping.ShipOption", new[] { "UpdatedDate" });
            DropIndex("Shipping.ShipOption", new[] { "Active" });
            DropIndex("Shipping.ShipOption", new[] { "Hash" });
            DropIndex("Shipping.ShipOption", new[] { "Name" });
            DropIndex("Shipping.ShipOption", new[] { "ShipCarrierMethodID" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "ID" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "Name" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "CalendarEventID" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "FileID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "VendorProductID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "StoreProductID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "VendorProductID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "StoreProductID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "VendorProductID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "StoreProductID" });
            DropIndex("Sampling.SampleRequest", new[] { "ShipOptionID" });
            DropIndex("Sampling.SampleRequestFile", new[] { "ID" });
            DropIndex("Sampling.SampleRequestFile", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestFile", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestFile", new[] { "Active" });
            DropIndex("Sampling.SampleRequestFile", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestFile", new[] { "Name" });
            DropIndex("Sampling.SampleRequestFile", new[] { "SampleRequestID" });
            DropIndex("Sampling.SampleRequestFile", new[] { "FileID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "VendorProductID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "StoreProductID" });
            DropIndex("Products.ProductFile", new[] { "ID" });
            DropIndex("Products.ProductFile", new[] { "CustomKey" });
            DropIndex("Products.ProductFile", new[] { "UpdatedDate" });
            DropIndex("Products.ProductFile", new[] { "Active" });
            DropIndex("Products.ProductFile", new[] { "Hash" });
            DropIndex("Products.ProductFile", new[] { "Name" });
            DropIndex("Products.ProductFile", new[] { "ProductID" });
            DropIndex("Products.ProductFile", new[] { "FileID" });
            DropIndex("Products.ProductImage", new[] { "ID" });
            DropIndex("Products.ProductImage", new[] { "CustomKey" });
            DropIndex("Products.ProductImage", new[] { "UpdatedDate" });
            DropIndex("Products.ProductImage", new[] { "Active" });
            DropIndex("Products.ProductImage", new[] { "Hash" });
            DropIndex("Products.ProductImage", new[] { "LibraryID" });
            DropIndex("Products.ProductImage", new[] { "ProductID" });
            DropIndex("Products.ProductImage", new[] { "TypeID" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "SalesInvoiceID" });
            DropIndex("Invoicing.SalesInvoiceFile", new[] { "FileID" });
            DropIndex("Ordering.SalesOrderFile", new[] { "ID" });
            DropIndex("Ordering.SalesOrderFile", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderFile", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderFile", new[] { "Active" });
            DropIndex("Ordering.SalesOrderFile", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderFile", new[] { "Name" });
            DropIndex("Ordering.SalesOrderFile", new[] { "SalesOrderID" });
            DropIndex("Ordering.SalesOrderFile", new[] { "FileID" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "PurchaseOrderID" });
            DropIndex("Purchasing.PurchaseOrderFile", new[] { "FileID" });
            DropIndex("Stores.StoreImage", new[] { "ID" });
            DropIndex("Stores.StoreImage", new[] { "CustomKey" });
            DropIndex("Stores.StoreImage", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreImage", new[] { "Active" });
            DropIndex("Stores.StoreImage", new[] { "Hash" });
            DropIndex("Stores.StoreImage", new[] { "LibraryID" });
            DropIndex("Stores.StoreImage", new[] { "StoreID" });
            DropIndex("Advertising.AdImage", new[] { "CustomKey" });
            DropIndex("Advertising.AdImage", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdImage", new[] { "Active" });
            DropIndex("Advertising.AdImage", new[] { "Hash" });
            DropIndex("Advertising.AdImage", new[] { "LibraryID" });
            DropIndex("Advertising.AdImage", new[] { "AdID" });
            DropForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoice_ID", "Invoicing.SalesInvoice");
            DropForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1", "Invoicing.SalesInvoice");
            DropForeignKey("Ordering.SalesOrderContact", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Ordering.SalesOrderContact", "SalesOrder_ID", "Ordering.SalesOrder");
            DropForeignKey("Ordering.SalesOrderContact", "SalesOrder_ID1", "Ordering.SalesOrder");
            DropForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrderID", "Purchasing.PurchaseOrder");
            DropForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID", "Purchasing.PurchaseOrder");
            DropForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1", "Purchasing.PurchaseOrder");
            DropForeignKey("Quoting.SalesQuoteContact", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("Quoting.SalesQuoteContact", "SalesQuote_ID", "Quoting.SalesQuote");
            DropForeignKey("Quoting.SalesQuoteContact", "SalesQuote_ID1", "Quoting.SalesQuote");
            DropForeignKey("Returning.SalesReturnContact", "SalesReturnID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnContact", "SalesReturn_ID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnContact", "SalesReturn_ID1", "Returning.SalesReturn");
            DropForeignKey("Sampling.SampleRequestContact", "SampleRequestID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequestContact", "SampleRequest_ID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequestContact", "SampleRequest_ID1", "Sampling.SampleRequest");
            DropForeignKey("Shopping.CartContact", "CartID", "Shopping.Cart");
            DropForeignKey("Shopping.CartContact", "Cart_ID", "Shopping.Cart");
            DropForeignKey("Shopping.CartContact", "Cart_ID1", "Shopping.Cart");
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "SalesInvoice_ID" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "SalesInvoice_ID1" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "SalesInvoiceID" });
            DropIndex("Ordering.SalesOrderContact", new[] { "SalesOrder_ID" });
            DropIndex("Ordering.SalesOrderContact", new[] { "SalesOrder_ID1" });
            DropIndex("Ordering.SalesOrderContact", new[] { "SalesOrderID" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "PurchaseOrder_ID" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "PurchaseOrder_ID1" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "PurchaseOrderID" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "SalesQuote_ID" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "SalesQuote_ID1" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "SalesQuoteID" });
            DropIndex("Returning.SalesReturnContact", new[] { "SalesReturn_ID" });
            DropIndex("Returning.SalesReturnContact", new[] { "SalesReturn_ID1" });
            DropIndex("Returning.SalesReturnContact", new[] { "SalesReturnID" });
            DropIndex("Sampling.SampleRequestContact", new[] { "SampleRequest_ID" });
            DropIndex("Sampling.SampleRequestContact", new[] { "SampleRequest_ID1" });
            DropIndex("Sampling.SampleRequestContact", new[] { "SampleRequestID" });
            DropIndex("Shopping.CartContact", new[] { "Cart_ID" });
            DropIndex("Shopping.CartContact", new[] { "Cart_ID1" });
            DropIndex("Shopping.CartContact", new[] { "CartID" });
            DropColumn("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1");
            DropColumn("Invoicing.SalesInvoiceContact", "SalesInvoice_ID");
            DropColumn("Ordering.SalesOrderContact", "SalesOrder_ID1");
            DropColumn("Ordering.SalesOrderContact", "SalesOrder_ID");
            DropColumn("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1");
            DropColumn("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID");
            DropColumn("Quoting.SalesQuoteContact", "SalesQuote_ID1");
            DropColumn("Quoting.SalesQuoteContact", "SalesQuote_ID");
            DropColumn("Returning.SalesReturnContact", "SalesReturn_ID1");
            DropColumn("Returning.SalesReturnContact", "SalesReturn_ID");
            DropColumn("Sampling.SampleRequestContact", "SampleRequest_ID1");
            DropColumn("Sampling.SampleRequestContact", "SampleRequest_ID");
            DropColumn("Shopping.CartContact", "Cart_ID1");
            DropColumn("Shopping.CartContact", "Cart_ID");
            CreateIndex("Returning.SalesReturnContact", "CreatedDate");
            CreateIndex("Returning.SalesReturnContact", "Hash");
            AddForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoiceID", "Invoicing.SalesInvoice", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderContact", "SalesOrderID", "Ordering.SalesOrder", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrderID", "Purchasing.PurchaseOrder", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteContact", "SalesQuoteID", "Quoting.SalesQuote", "ID", cascadeDelete: true);
            AddForeignKey("Returning.SalesReturnContact", "SalesReturnID", "Returning.SalesReturn", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequestContact", "SampleRequestID", "Sampling.SampleRequest", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartContact", "CartID", "Shopping.Cart", "ID", cascadeDelete: true);
            CreateIndex("Invoicing.SalesInvoiceContact", "SalesInvoiceID");
            CreateIndex("Ordering.SalesOrderContact", "SalesOrderID");
            CreateIndex("Purchasing.PurchaseOrderContact", "PurchaseOrderID");
            CreateIndex("Quoting.SalesQuoteContact", "SalesQuoteID");
            CreateIndex("Returning.SalesReturnContact", "SalesReturnID");
            CreateIndex("Sampling.SampleRequestContact", "SampleRequestID");
            CreateIndex("Shopping.CartContact", "CartID");
            CreateTable(
                "Messaging.EmailQueueAttachment",
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
                    StoredFileID = c.Int(nullable: false),
                    EmailQueueID = c.Int(nullable: false),
                    CreatedByUserID = c.Int(nullable: false),
                    UpdatedByUserID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.CreatedByUserID, cascadeDelete: true)
                .ForeignKey("Messaging.EmailQueue", t => t.EmailQueueID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.StoredFileID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UpdatedByUserID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.StoredFileID)
                .Index(t => t.EmailQueueID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.UpdatedByUserID);
            AddColumn("Stores.StoreInventoryLocation", "TypeID", c => c.Int());
            AlterColumn("Contacts.Contact", "Phone1", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("Contacts.Contact", "Phone2", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("Contacts.Contact", "Phone3", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("Contacts.Contact", "Fax1", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("Contacts.Contact", "Fax2", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("Contacts.Contact", "Fax3", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("Contacts.Contact", "Email1", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("Contacts.Contact", "Email2", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("Contacts.Contact", "Email3", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("Contacts.Contact", "Website1", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("Contacts.Contact", "Website2", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("Contacts.Contact", "Website3", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("Contacts.Contact", "NotificationPhone", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("Contacts.Contact", "NotificationEmail", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("Messaging.Message", "Subject", c => c.String(maxLength: 255));
            AlterColumn("Messaging.Message", "Body", c => c.String(nullable: false));
            AlterColumn("Messaging.EmailQueue", "AddressesTo", c => c.String(maxLength: 1024, unicode: false));
            AlterColumn("Messaging.EmailQueue", "AddressesCc", c => c.String(maxLength: 1024, unicode: false));
            AlterColumn("Messaging.EmailQueue", "AddressesBcc", c => c.String(maxLength: 1024, unicode: false));
            AlterColumn("Messaging.EmailQueue", "AddressFrom", c => c.String(nullable: false, maxLength: 1024, unicode: false));
            AlterColumn("Contacts.Opportunities", "ProbabilityOfClose", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("Contacts.Opportunities", "ProbabilityOfWin", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("Counters.Counter", "Value", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("Counters.CounterLog", "Value", c => c.Decimal(precision: 18, scale: 4));
            CreateIndex("Accounts.AccountContact", "CreatedDate");
            CreateIndex("Accounts.Account", "CreatedDate");
            CreateIndex("Accounts.AccountPricePoint", "CreatedDate");
            CreateIndex("Stores.Store", "CreatedDate");
            CreateIndex("Stores.Store", "Hash");
            CreateIndex("Stores.Brand", "CreatedDate");
            CreateIndex("Stores.BrandImage", "CreatedDate");
            CreateIndex("Stores.BrandImageType", "CreatedDate");
            CreateIndex("Contacts.Contact", "CreatedDate");
            CreateIndex("Geography.Country", "CreatedDate");
            CreateIndex("Currencies.Currency", "CreatedDate");
            CreateIndex("Currencies.HistoricalCurrencyRate", "CreatedDate");
            CreateIndex("Currencies.CurrencyImage", "CreatedDate");
            CreateIndex("Currencies.CurrencyImageType", "CreatedDate");
            CreateIndex("Geography.CountryImage", "CreatedDate");
            CreateIndex("Geography.CountryImageType", "CreatedDate");
            CreateIndex("Globalization.LanguageImage", "CreatedDate");
            CreateIndex("Globalization.LanguageImageType", "CreatedDate");
            CreateIndex("Geography.Region", "CreatedDate");
            CreateIndex("Geography.District", "CreatedDate");
            CreateIndex("Geography.DistrictImage", "CreatedDate");
            CreateIndex("Geography.DistrictImageType", "CreatedDate");
            CreateIndex("Geography.RegionImage", "CreatedDate");
            CreateIndex("Geography.RegionImageType", "CreatedDate");
            CreateIndex("Purchasing.PurchaseOrder", "CreatedDate");
            CreateIndex("Ordering.SalesOrder", "CreatedDate");
            CreateIndex("Categories.Category", "CreatedDate");
            CreateIndex("Categories.CategoryImageNew", "CreatedDate");
            CreateIndex("Categories.CategoryImageType", "CreatedDate");
            CreateIndex("Products.Product", "CreatedDate");
            CreateIndex("Shopping.Cart", "CreatedDate");
            CreateIndex("Shopping.Cart", new[] { "SessionID", "TypeID", "UserID" }, unique: true, name: "Unq_BySessionTypeUserActive");
            CreateIndex("Contacts.User", "CreatedDate");
            CreateIndex("Messaging.Message", "CreatedDate");
            CreateIndex("Messaging.MessageAttachment", "CreatedDate");
            CreateIndex("Media.StoredFile", "CreatedDate");
            CreateIndex("Messaging.EmailQueue", "CreatedDate");
            CreateIndex("Manufacturers.Manufacturer", "CreatedDate");
            CreateIndex("Manufacturers.ManufacturerImage", "CreatedDate");
            CreateIndex("Manufacturers.ManufacturerImageType", "CreatedDate");
            CreateIndex("Vendors.Vendor", "CreatedDate");
            CreateIndex("Contacts.ContactMethod", "CreatedDate");
            CreateIndex("Vendors.VendorImage", "CreatedDate");
            CreateIndex("Vendors.VendorImageType", "CreatedDate");
            CreateIndex("Inventory.InventoryLocation", "CreatedDate");
            CreateIndex("Stores.StoreInventoryLocation", "CreatedDate");
            CreateIndex("Stores.StoreInventoryLocation", "TypeID");
            CreateIndex("Stores.StoreInventoryLocationType", "CreatedDate");
            CreateIndex("Products.ProductInventoryLocationSection", "CreatedDate");
            CreateIndex("Shipping.ShipCarrier", "CreatedDate");
            CreateIndex("Shipping.CarrierOrigin", "CreatedDate");
            CreateIndex("Contacts.UserImage", "CreatedDate");
            CreateIndex("Contacts.UserImageType", "CreatedDate");
            CreateIndex("Contacts.ReferralCode", "CreatedDate");
            CreateIndex("Contacts.ReferralCodeStatus", "CreatedDate");
            CreateIndex("Contacts.ReferralCodeType", "CreatedDate");
            CreateIndex("Quoting.SalesQuote", "CreatedDate");
            CreateIndex("Contacts.CustomerPriority", "CreatedDate");
            CreateIndex("Returning.SalesReturnItem", "CreatedDate");
            CreateIndex("Returning.SalesReturnFileNew", "CreatedDate");
            CreateIndex("Quoting.SalesQuoteItem", "CreatedDate");
            CreateIndex("Quoting.SalesQuoteFileNew", "CreatedDate");
            CreateIndex("Contacts.UserStatus", "CreatedDate");
            CreateIndex("Contacts.UserType", "CreatedDate");
            CreateIndex("CalendarEvents.UserEventAttendance", "CreatedDate");
            CreateIndex("CalendarEvents.CalendarEvent", "CreatedDate");
            CreateIndex("CalendarEvents.CalendarEventDetail", "CreatedDate");
            CreateIndex("CalendarEvents.CalendarEventProducts", "CreatedDate");
            CreateIndex("CalendarEvents.CalendarEventImage", "CreatedDate");
            CreateIndex("CalendarEvents.CalendarEventImageType", "CreatedDate");
            CreateIndex("CalendarEvents.CalendarEventStatus", "CreatedDate");
            CreateIndex("CalendarEvents.CalendarEventFileNew", "CreatedDate");
            CreateIndex("CalendarEvents.CalendarEventType", "CreatedDate");
            CreateIndex("CalendarEvents.UserEventAttendanceType", "CreatedDate");
            CreateIndex("Contacts.UserOnlineStatus", "CreatedDate");
            CreateIndex("Purchasing.PurchaseOrderItem", "CreatedDate");
            CreateIndex("Ordering.SalesOrderItem", "CreatedDate");
            CreateIndex("Sampling.SampleRequest", "CreatedDate");
            CreateIndex("Sampling.SampleRequestItem", "CreatedDate");
            CreateIndex("Sampling.SampleRequestFileNew", "CreatedDate");
            CreateIndex("Shopping.CartFileNew", "CreatedDate");
            CreateIndex("Products.ProductImageNew", "CreatedDate");
            CreateIndex("Products.ProductImageType", "CreatedDate");
            CreateIndex("Products.ProductFileNew", "CreatedDate");
            CreateIndex("Categories.CategoryFileNew", "CreatedDate");
            CreateIndex("Categories.CategoryType", "CreatedDate");
            CreateIndex("Invoicing.SalesInvoiceFileNew", "CreatedDate");
            CreateIndex("Ordering.SalesOrderFileNew", "CreatedDate");
            CreateIndex("Purchasing.PurchaseOrderFileNew", "CreatedDate");
            CreateIndex("Contacts.ContactImage", "CreatedDate");
            CreateIndex("Contacts.ContactImageType", "CreatedDate");
            CreateIndex("Contacts.ContactType", "CreatedDate");
            CreateIndex("Stores.StoreImageNew", "CreatedDate");
            CreateIndex("Stores.StoreImageType", "CreatedDate");
            CreateIndex("Stores.Badge", "CreatedDate");
            CreateIndex("Stores.BadgeImage", "CreatedDate");
            CreateIndex("Stores.BadgeImageType", "CreatedDate");
            CreateIndex("Accounts.AccountTerm", "CreatedDate");
            CreateIndex("Accounts.AccountImage", "CreatedDate");
            CreateIndex("Accounts.AccountImageType", "CreatedDate");
            CreateIndex("Contacts.Opportunities", "CreatedDate");
            CreateIndex("Accounts.AccountStatus", "CreatedDate");
            CreateIndex("Accounts.AccountType", "CreatedDate");
            CreateIndex("Advertising.AdAccount", "ID");
            CreateIndex("Advertising.AdAccount", "CreatedDate");
            CreateIndex("Advertising.Ad", "ID");
            CreateIndex("Advertising.Ad", "CreatedDate");
            CreateIndex("Advertising.AdStore", "ID");
            CreateIndex("Advertising.AdStore", "CreatedDate");
            CreateIndex("Advertising.AdZone", "ID");
            CreateIndex("Advertising.AdZone", "CreatedDate");
            CreateIndex("Advertising.AdZoneAccess", "ID");
            CreateIndex("Advertising.AdZoneAccess", "CreatedDate");
            CreateIndex("Counters.Counter", "ID");
            CreateIndex("Counters.Counter", "CreatedDate");
            CreateIndex("Counters.CounterLog", "ID");
            CreateIndex("Counters.CounterLog", "CreatedDate");
            CreateIndex("Counters.CounterLogType", "ID");
            CreateIndex("Counters.CounterLogType", "CreatedDate");
            CreateIndex("Counters.CounterType", "ID");
            CreateIndex("Counters.CounterType", "CreatedDate");
            CreateIndex("Advertising.Zone", "ID");
            CreateIndex("Advertising.Zone", "CreatedDate");
            CreateIndex("Advertising.ZoneStatus", "ID");
            CreateIndex("Advertising.ZoneStatus", "CreatedDate");
            CreateIndex("Advertising.ZoneType", "ID");
            CreateIndex("Advertising.ZoneType", "CreatedDate");
            CreateIndex("Advertising.AdImageNew", "CreatedDate");
            CreateIndex("Advertising.AdImageType", "CreatedDate");
            CreateIndex("Advertising.AdStatus", "ID");
            CreateIndex("Advertising.AdStatus", "CreatedDate");
            CreateIndex("Advertising.AdType", "ID");
            CreateIndex("Advertising.AdType", "CreatedDate");
            CreateIndex("Attributes.AttributeGroup", "CreatedDate");
            CreateIndex("Attributes.AttributeTab", "CreatedDate");
            CreateIndex("Attributes.AttributeType", "CreatedDate");
            CreateIndex("Attributes.GeneralAttributePredefinedOption", "CreatedDate");
            CreateIndex("Attributes.GeneralAttribute", "CreatedDate");
            CreateIndex("Contacts.ProfanityFilter", "CreatedDate");
            AddForeignKey("Stores.StoreInventoryLocation", "TypeID", "Stores.StoreInventoryLocationType", "ID");
            DropColumn("Stores.Store", "LogoImageLibraryID");
            DropColumn("Stores.Store", "SellerImageLibraryID");
            DropColumn("Geography.Country", "LibraryID");
            DropColumn("Currencies.Currency", "LibraryID");
            DropColumn("Globalization.Language", "LibraryID");
            DropColumn("Geography.Region", "LibraryID");
            DropColumn("Geography.District", "LibraryID");
            DropColumn("Invoicing.SalesInvoice", "AccountKey");
            DropColumn("Invoicing.SalesInvoice", "ShipOptionID");
            DropColumn("Products.Product", "KitParentKeyForQuantity");
            DropColumn("Products.Product", "KitQuantityOfParent");
            DropColumn("Products.Product", "KitCapacity");
            DropColumn("Shopping.CartItem", "ProductInventoryLocationSectionID");
            DropColumn("Shopping.CartItem", "VendorProductID");
            DropColumn("Shopping.CartItem", "StoreProductID");
            DropColumn("Shopping.Cart", "ShipOptionID");
            DropColumn("Messaging.MessageAttachment", "LibraryID");
            DropColumn("Manufacturers.Manufacturer", "Phone");
            DropColumn("Manufacturers.Manufacturer", "Fax");
            DropColumn("Manufacturers.Manufacturer", "Email");
            DropColumn("Manufacturers.Manufacturer", "AddressID");
            DropColumn("Vendors.Vendor", "Phone");
            DropColumn("Vendors.Vendor", "Fax");
            DropColumn("Vendors.Vendor", "Email");
            DropColumn("Inventory.InventoryLocation", "Phone");
            DropColumn("Inventory.InventoryLocation", "Fax");
            DropColumn("Inventory.InventoryLocation", "Email");
            DropColumn("Inventory.InventoryLocation", "AddressID");
            DropColumn("Products.ProductInventoryLocationSection", "InventoryHash");
            DropColumn("Shipping.ShipCarrier", "Phone");
            DropColumn("Shipping.ShipCarrier", "Fax");
            DropColumn("Shipping.ShipCarrier", "Email");
            DropColumn("Shipping.ShipCarrier", "AddressID");
            DropColumn("Quoting.SalesQuote", "ShipOptionID");
            DropColumn("Returning.SalesReturnItem", "ShippingCarrierName");
            DropColumn("Returning.SalesReturnItem", "ShippingCarrierMethodName");
            DropColumn("Returning.SalesReturnItem", "TrackingNumber");
            DropColumn("Returning.SalesReturnItem", "ProductInventoryLocationSectionID");
            DropColumn("Returning.SalesReturnItem", "VendorProductID");
            DropColumn("Returning.SalesReturnItem", "StoreProductID");
            DropColumn("Stores.StoreProduct", "StoreProductHash");
            DropColumn("Quoting.SalesQuoteItem", "TrackingNumber");
            DropColumn("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID");
            DropColumn("Quoting.SalesQuoteItem", "VendorProductID");
            DropColumn("Quoting.SalesQuoteItem", "StoreProductID");
            DropColumn("Purchasing.PurchaseOrderItem", "EstimatedArrival");
            DropColumn("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID");
            DropColumn("Purchasing.PurchaseOrderItem", "VendorProductID");
            DropColumn("Purchasing.PurchaseOrderItem", "StoreProductID");
            DropColumn("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID");
            DropColumn("Invoicing.SalesInvoiceItem", "VendorProductID");
            DropColumn("Invoicing.SalesInvoiceItem", "StoreProductID");
            DropColumn("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID");
            DropColumn("Ordering.SalesOrderItem", "VendorProductID");
            DropColumn("Ordering.SalesOrderItem", "StoreProductID");
            DropColumn("Ordering.SalesOrderItem", "ShippingCarrierName");
            DropColumn("Ordering.SalesOrderItem", "ShippingCarrierMethodName");
            DropColumn("Ordering.SalesOrderItem", "TrackingNumber");
            DropColumn("Sampling.SampleRequest", "ShipOptionID");
            DropColumn("Sampling.SampleRequestItem", "TrackingNumber");
            DropColumn("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID");
            DropColumn("Sampling.SampleRequestItem", "VendorProductID");
            DropColumn("Sampling.SampleRequestItem", "StoreProductID");
            DropTable("Media.Library");
            DropTable("Media.Audio");
            DropTable("Media.File");
            DropTable("Media.FileData");
            DropTable("Media.Image");
            DropTable("Media.Document");
            DropTable("Media.Video");
            DropTable("Categories.CategoryImage");
            DropTable("Categories.CategoryFile");
            DropTable("Shopping.CartFile");
            DropTable("Quoting.SalesQuoteFile");
            DropTable("Returning.SalesReturnFile");
            DropTable("Shipping.ShipOption");
            DropTable("CalendarEvents.CalendarEventFile");
            DropTable("Sampling.SampleRequestFile");
            DropTable("Products.ProductFile");
            DropTable("Products.ProductImage");
            DropTable("Invoicing.SalesInvoiceFile");
            DropTable("Ordering.SalesOrderFile");
            DropTable("Purchasing.PurchaseOrderFile");
            DropTable("Stores.StoreImage");
            DropTable("Advertising.AdImage");
        }

        public override void Down()
        {
            CreateTable(
                "Advertising.AdImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        LibraryID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        AdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Stores.StoreImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        LibraryID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Purchasing.PurchaseOrderFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        PurchaseOrderID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Ordering.SalesOrderFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SalesOrderID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Invoicing.SalesInvoiceFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SalesInvoiceID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Products.ProductImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        LibraryID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        ProductID = c.Int(nullable: false),
                        TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Products.ProductFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        ProductID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Sampling.SampleRequestFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SampleRequestID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "CalendarEvents.CalendarEventFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        CalendarEventID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Shipping.ShipOption",
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
                        ShipCarrierMethodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Returning.SalesReturnFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SalesReturnID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Quoting.SalesQuoteFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SalesQuoteID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Shopping.CartFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        CartID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Categories.CategoryFile",
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
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        JsonAttributes = c.String(),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        CategoryID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Categories.CategoryImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        LibraryID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Media.Video",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        FormatID = c.Int(nullable: false),
                        FullHeight = c.Int(),
                        FullWidth = c.Int(),
                        FileID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Media.Document",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        FormatID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Media.Image",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        FormatID = c.Int(nullable: false),
                        FullHeight = c.Int(),
                        FullWidth = c.Int(),
                        ThumbHeight = c.Int(),
                        ThumbWidth = c.Int(),
                        FullFileID = c.Int(nullable: false),
                        ThumbFileID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Media.FileData",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Bytes = c.Binary(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Media.File",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        FileName = c.String(nullable: false),
                        IsStoredInDB = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Media.Audio",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        FormatID = c.Int(nullable: false),
                        FullFileID = c.Int(nullable: false),
                        ClipFileID = c.Int(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Media.Library",
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
                        SeoName = c.String(),
                        Caption = c.String(),
                        SortOrder = c.Int(),
                        Author = c.String(maxLength: 100),
                        MediaDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Copyright = c.String(maxLength: 100),
                        Location = c.String(maxLength: 100),
                        TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            AddColumn("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1", c => c.Int());
            AddColumn("Ordering.SalesOrderContact", "SalesOrder_ID1", c => c.Int());
            AddColumn("Sampling.SampleRequestItem", "StoreProductID", c => c.Int());
            AddColumn("Sampling.SampleRequestItem", "VendorProductID", c => c.Int());
            AddColumn("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Sampling.SampleRequestItem", "TrackingNumber", c => c.String(maxLength: 100, unicode: false));
            AddColumn("Sampling.SampleRequestContact", "SampleRequest_ID1", c => c.Int());
            AddColumn("Sampling.SampleRequest", "ShipOptionID", c => c.Int());
            AddColumn("Ordering.SalesOrderItem", "TrackingNumber", c => c.String(maxLength: 100, unicode: false));
            AddColumn("Ordering.SalesOrderItem", "ShippingCarrierMethodName", c => c.String(maxLength: 255, unicode: false));
            AddColumn("Ordering.SalesOrderItem", "ShippingCarrierName", c => c.String(maxLength: 255, unicode: false));
            AddColumn("Ordering.SalesOrderItem", "StoreProductID", c => c.Int());
            AddColumn("Ordering.SalesOrderItem", "VendorProductID", c => c.Int());
            AddColumn("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItem", "StoreProductID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItem", "VendorProductID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItem", "StoreProductID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItem", "VendorProductID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItem", "EstimatedArrival", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Quoting.SalesQuoteItem", "StoreProductID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItem", "VendorProductID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItem", "TrackingNumber", c => c.String(maxLength: 100, unicode: false));
            AddColumn("Stores.StoreProduct", "StoreProductHash", c => c.Long());
            AddColumn("Returning.SalesReturnItem", "StoreProductID", c => c.Int());
            AddColumn("Returning.SalesReturnItem", "VendorProductID", c => c.Int());
            AddColumn("Returning.SalesReturnItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Returning.SalesReturnItem", "TrackingNumber", c => c.String(maxLength: 100, unicode: false));
            AddColumn("Returning.SalesReturnItem", "ShippingCarrierMethodName", c => c.String(maxLength: 255, unicode: false));
            AddColumn("Returning.SalesReturnItem", "ShippingCarrierName", c => c.String(maxLength: 255, unicode: false));
            AddColumn("Returning.SalesReturnContact", "SalesReturn_ID1", c => c.Int());
            AddColumn("Quoting.SalesQuoteContact", "SalesQuote_ID1", c => c.Int());
            AddColumn("Quoting.SalesQuote", "ShipOptionID", c => c.Int());
            AddColumn("Shipping.ShipCarrier", "AddressID", c => c.Int());
            AddColumn("Shipping.ShipCarrier", "Email", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Shipping.ShipCarrier", "Fax", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Shipping.ShipCarrier", "Phone", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Products.ProductInventoryLocationSection", "InventoryHash", c => c.Long());
            AddColumn("Inventory.InventoryLocation", "AddressID", c => c.Int());
            AddColumn("Inventory.InventoryLocation", "Email", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Inventory.InventoryLocation", "Fax", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Inventory.InventoryLocation", "Phone", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Vendors.Vendor", "Email", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Vendors.Vendor", "Fax", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Vendors.Vendor", "Phone", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "AddressID", c => c.Int());
            AddColumn("Manufacturers.Manufacturer", "Email", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "Fax", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Manufacturers.Manufacturer", "Phone", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Messaging.MessageAttachment", "LibraryID", c => c.Int(nullable: false));
            AddColumn("Shopping.CartContact", "Cart_ID", c => c.Int());
            AddColumn("Shopping.Cart", "ShipOptionID", c => c.Int());
            AddColumn("Shopping.CartItem", "StoreProductID", c => c.Int());
            AddColumn("Shopping.CartItem", "VendorProductID", c => c.Int());
            AddColumn("Shopping.CartItem", "ProductInventoryLocationSectionID", c => c.Int());
            AddColumn("Products.Product", "KitCapacity", c => c.String());
            AddColumn("Products.Product", "KitQuantityOfParent", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "KitParentKeyForQuantity", c => c.String());
            AddColumn("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1", c => c.Int());
            AddColumn("Invoicing.SalesInvoice", "ShipOptionID", c => c.Int());
            AddColumn("Invoicing.SalesInvoice", "AccountKey", c => c.String());
            AddColumn("Geography.District", "LibraryID", c => c.Int());
            AddColumn("Geography.Region", "LibraryID", c => c.Int());
            AddColumn("Globalization.Language", "LibraryID", c => c.Int());
            AddColumn("Currencies.Currency", "LibraryID", c => c.Int());
            AddColumn("Geography.Country", "LibraryID", c => c.Int());
            AddColumn("Stores.Store", "SellerImageLibraryID", c => c.Int());
            AddColumn("Stores.Store", "LogoImageLibraryID", c => c.Int());
            DropForeignKey("Sampling.SampleRequestContact", "SampleRequestID", "Sampling.SampleRequest");
            DropForeignKey("Returning.SalesReturnContact", "SalesReturnID", "Returning.SalesReturn");
            DropForeignKey("Quoting.SalesQuoteContact", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("Shopping.CartContact", "CartID", "Shopping.Cart");
            DropForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Ordering.SalesOrderContact", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrderID", "Purchasing.PurchaseOrder");
            DropForeignKey("Stores.StoreInventoryLocation", "TypeID", "Stores.StoreInventoryLocationType");
            DropForeignKey("Messaging.EmailQueueAttachment", "UpdatedByUserID", "Contacts.User");
            DropForeignKey("Messaging.EmailQueueAttachment", "StoredFileID", "Media.StoredFile");
            DropForeignKey("Messaging.EmailQueueAttachment", "EmailQueueID", "Messaging.EmailQueue");
            DropForeignKey("Messaging.EmailQueueAttachment", "CreatedByUserID", "Contacts.User");
            DropIndex("Contacts.ProfanityFilter", new[] { "CreatedDate" });
            DropIndex("Attributes.GeneralAttribute", new[] { "CreatedDate" });
            DropIndex("Attributes.GeneralAttributePredefinedOption", new[] { "CreatedDate" });
            DropIndex("Attributes.AttributeType", new[] { "CreatedDate" });
            DropIndex("Attributes.AttributeTab", new[] { "CreatedDate" });
            DropIndex("Attributes.AttributeGroup", new[] { "CreatedDate" });
            DropIndex("Advertising.AdType", new[] { "CreatedDate" });
            DropIndex("Advertising.AdType", new[] { "ID" });
            DropIndex("Advertising.AdStatus", new[] { "CreatedDate" });
            DropIndex("Advertising.AdStatus", new[] { "ID" });
            DropIndex("Advertising.AdImageType", new[] { "CreatedDate" });
            DropIndex("Advertising.AdImageNew", new[] { "CreatedDate" });
            DropIndex("Advertising.ZoneType", new[] { "CreatedDate" });
            DropIndex("Advertising.ZoneType", new[] { "ID" });
            DropIndex("Advertising.ZoneStatus", new[] { "CreatedDate" });
            DropIndex("Advertising.ZoneStatus", new[] { "ID" });
            DropIndex("Advertising.Zone", new[] { "CreatedDate" });
            DropIndex("Advertising.Zone", new[] { "ID" });
            DropIndex("Counters.CounterType", new[] { "CreatedDate" });
            DropIndex("Counters.CounterType", new[] { "ID" });
            DropIndex("Counters.CounterLogType", new[] { "CreatedDate" });
            DropIndex("Counters.CounterLogType", new[] { "ID" });
            DropIndex("Counters.CounterLog", new[] { "CreatedDate" });
            DropIndex("Counters.CounterLog", new[] { "ID" });
            DropIndex("Counters.Counter", new[] { "CreatedDate" });
            DropIndex("Counters.Counter", new[] { "ID" });
            DropIndex("Advertising.AdZoneAccess", new[] { "CreatedDate" });
            DropIndex("Advertising.AdZoneAccess", new[] { "ID" });
            DropIndex("Advertising.AdZone", new[] { "CreatedDate" });
            DropIndex("Advertising.AdZone", new[] { "ID" });
            DropIndex("Advertising.AdStore", new[] { "CreatedDate" });
            DropIndex("Advertising.AdStore", new[] { "ID" });
            DropIndex("Advertising.Ad", new[] { "CreatedDate" });
            DropIndex("Advertising.Ad", new[] { "ID" });
            DropIndex("Advertising.AdAccount", new[] { "CreatedDate" });
            DropIndex("Advertising.AdAccount", new[] { "ID" });
            DropIndex("Accounts.AccountType", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountStatus", new[] { "CreatedDate" });
            DropIndex("Contacts.Opportunities", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountImageType", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountImage", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountTerm", new[] { "CreatedDate" });
            DropIndex("Stores.BadgeImageType", new[] { "CreatedDate" });
            DropIndex("Stores.BadgeImage", new[] { "CreatedDate" });
            DropIndex("Stores.Badge", new[] { "CreatedDate" });
            DropIndex("Stores.StoreImageType", new[] { "CreatedDate" });
            DropIndex("Stores.StoreImageNew", new[] { "CreatedDate" });
            DropIndex("Contacts.ContactType", new[] { "CreatedDate" });
            DropIndex("Contacts.ContactImageType", new[] { "CreatedDate" });
            DropIndex("Contacts.ContactImage", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrderFileNew", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrderContact", new[] { "PurchaseOrderID" });
            DropIndex("Ordering.SalesOrderFileNew", new[] { "CreatedDate" });
            DropIndex("Ordering.SalesOrderContact", new[] { "SalesOrderID" });
            DropIndex("Invoicing.SalesInvoiceFileNew", new[] { "CreatedDate" });
            DropIndex("Categories.CategoryType", new[] { "CreatedDate" });
            DropIndex("Categories.CategoryFileNew", new[] { "CreatedDate" });
            DropIndex("Products.ProductFileNew", new[] { "CreatedDate" });
            DropIndex("Products.ProductImageType", new[] { "CreatedDate" });
            DropIndex("Products.ProductImageNew", new[] { "CreatedDate" });
            DropIndex("Shopping.CartFileNew", new[] { "CreatedDate" });
            DropIndex("Sampling.SampleRequestFileNew", new[] { "CreatedDate" });
            DropIndex("Sampling.SampleRequestItem", new[] { "CreatedDate" });
            DropIndex("Sampling.SampleRequestContact", new[] { "SampleRequestID" });
            DropIndex("Sampling.SampleRequest", new[] { "CreatedDate" });
            DropIndex("Ordering.SalesOrderItem", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "CreatedDate" });
            DropIndex("Contacts.UserOnlineStatus", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.UserEventAttendanceType", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.CalendarEventType", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.CalendarEventFileNew", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.CalendarEventStatus", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.CalendarEventImageType", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.CalendarEventImage", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.CalendarEventProducts", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.CalendarEventDetail", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "CreatedDate" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "CreatedDate" });
            DropIndex("Contacts.UserType", new[] { "CreatedDate" });
            DropIndex("Contacts.UserStatus", new[] { "CreatedDate" });
            DropIndex("Quoting.SalesQuoteFileNew", new[] { "CreatedDate" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "CreatedDate" });
            DropIndex("Returning.SalesReturnFileNew", new[] { "CreatedDate" });
            DropIndex("Returning.SalesReturnItem", new[] { "CreatedDate" });
            DropIndex("Contacts.CustomerPriority", new[] { "CreatedDate" });
            DropIndex("Returning.SalesReturnContact", new[] { "SalesReturnID" });
            DropIndex("Returning.SalesReturnContact", new[] { "Hash" });
            DropIndex("Returning.SalesReturnContact", new[] { "CreatedDate" });
            DropIndex("Quoting.SalesQuoteContact", new[] { "SalesQuoteID" });
            DropIndex("Quoting.SalesQuote", new[] { "CreatedDate" });
            DropIndex("Contacts.ReferralCodeType", new[] { "CreatedDate" });
            DropIndex("Contacts.ReferralCodeStatus", new[] { "CreatedDate" });
            DropIndex("Contacts.ReferralCode", new[] { "CreatedDate" });
            DropIndex("Contacts.UserImageType", new[] { "CreatedDate" });
            DropIndex("Contacts.UserImage", new[] { "CreatedDate" });
            DropIndex("Shipping.CarrierOrigin", new[] { "CreatedDate" });
            DropIndex("Shipping.ShipCarrier", new[] { "CreatedDate" });
            DropIndex("Products.ProductInventoryLocationSection", new[] { "CreatedDate" });
            DropIndex("Stores.StoreInventoryLocationType", new[] { "CreatedDate" });
            DropIndex("Stores.StoreInventoryLocation", new[] { "TypeID" });
            DropIndex("Stores.StoreInventoryLocation", new[] { "CreatedDate" });
            DropIndex("Inventory.InventoryLocation", new[] { "CreatedDate" });
            DropIndex("Vendors.VendorImageType", new[] { "CreatedDate" });
            DropIndex("Vendors.VendorImage", new[] { "CreatedDate" });
            DropIndex("Contacts.ContactMethod", new[] { "CreatedDate" });
            DropIndex("Vendors.Vendor", new[] { "CreatedDate" });
            DropIndex("Manufacturers.ManufacturerImageType", new[] { "CreatedDate" });
            DropIndex("Manufacturers.ManufacturerImage", new[] { "CreatedDate" });
            DropIndex("Manufacturers.Manufacturer", new[] { "CreatedDate" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "UpdatedByUserID" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "CreatedByUserID" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "EmailQueueID" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "StoredFileID" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "Name" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "Hash" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "Active" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "UpdatedDate" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "CreatedDate" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "CustomKey" });
            DropIndex("Messaging.EmailQueueAttachment", new[] { "ID" });
            DropIndex("Messaging.EmailQueue", new[] { "CreatedDate" });
            DropIndex("Media.StoredFile", new[] { "CreatedDate" });
            DropIndex("Messaging.MessageAttachment", new[] { "CreatedDate" });
            DropIndex("Messaging.Message", new[] { "CreatedDate" });
            DropIndex("Contacts.User", new[] { "CreatedDate" });
            DropIndex("Shopping.CartContact", new[] { "CartID" });
            DropIndex("Shopping.Cart", "Unq_BySessionTypeUserActive");
            DropIndex("Shopping.Cart", new[] { "CreatedDate" });
            DropIndex("Products.Product", new[] { "CreatedDate" });
            DropIndex("Categories.CategoryImageType", new[] { "CreatedDate" });
            DropIndex("Categories.CategoryImageNew", new[] { "CreatedDate" });
            DropIndex("Categories.Category", new[] { "CreatedDate" });
            DropIndex("Invoicing.SalesInvoiceContact", new[] { "SalesInvoiceID" });
            DropIndex("Ordering.SalesOrder", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "CreatedDate" });
            DropIndex("Geography.RegionImageType", new[] { "CreatedDate" });
            DropIndex("Geography.RegionImage", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictImageType", new[] { "CreatedDate" });
            DropIndex("Geography.DistrictImage", new[] { "CreatedDate" });
            DropIndex("Geography.District", new[] { "CreatedDate" });
            DropIndex("Geography.Region", new[] { "CreatedDate" });
            DropIndex("Globalization.LanguageImageType", new[] { "CreatedDate" });
            DropIndex("Globalization.LanguageImage", new[] { "CreatedDate" });
            DropIndex("Geography.CountryImageType", new[] { "CreatedDate" });
            DropIndex("Geography.CountryImage", new[] { "CreatedDate" });
            DropIndex("Currencies.CurrencyImageType", new[] { "CreatedDate" });
            DropIndex("Currencies.CurrencyImage", new[] { "CreatedDate" });
            DropIndex("Currencies.HistoricalCurrencyRate", new[] { "CreatedDate" });
            DropIndex("Currencies.Currency", new[] { "CreatedDate" });
            DropIndex("Geography.Country", new[] { "CreatedDate" });
            DropIndex("Contacts.Contact", new[] { "CreatedDate" });
            DropIndex("Stores.BrandImageType", new[] { "CreatedDate" });
            DropIndex("Stores.BrandImage", new[] { "CreatedDate" });
            DropIndex("Stores.Brand", new[] { "CreatedDate" });
            DropIndex("Stores.Store", new[] { "Hash" });
            DropIndex("Stores.Store", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountPricePoint", new[] { "CreatedDate" });
            DropIndex("Accounts.Account", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountContact", new[] { "CreatedDate" });
            AlterColumn("Counters.CounterLog", "Value", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("Counters.Counter", "Value", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("Contacts.Opportunities", "ProbabilityOfWin", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("Contacts.Opportunities", "ProbabilityOfClose", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("Purchasing.PurchaseOrderContact", "PurchaseOrderID", c => c.Int());
            AlterColumn("Ordering.SalesOrderContact", "SalesOrderID", c => c.Int());
            AlterColumn("Sampling.SampleRequestContact", "SampleRequestID", c => c.Int());
            AlterColumn("Returning.SalesReturnContact", "SalesReturnID", c => c.Int());
            AlterColumn("Quoting.SalesQuoteContact", "SalesQuoteID", c => c.Int());
            AlterColumn("Messaging.EmailQueue", "AddressFrom", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("Messaging.EmailQueue", "AddressesBcc", c => c.String());
            AlterColumn("Messaging.EmailQueue", "AddressesCc", c => c.String());
            AlterColumn("Messaging.EmailQueue", "AddressesTo", c => c.String());
            AlterColumn("Messaging.Message", "Body", c => c.String());
            AlterColumn("Messaging.Message", "Subject", c => c.String(maxLength: 512));
            AlterColumn("Shopping.CartContact", "CartID", c => c.Int());
            AlterColumn("Invoicing.SalesInvoiceContact", "SalesInvoiceID", c => c.Int());
            AlterColumn("Contacts.Contact", "NotificationEmail", c => c.String(maxLength: 1000));
            AlterColumn("Contacts.Contact", "NotificationPhone", c => c.String(maxLength: 50));
            AlterColumn("Contacts.Contact", "Website3", c => c.String(maxLength: 1000));
            AlterColumn("Contacts.Contact", "Website2", c => c.String(maxLength: 1000));
            AlterColumn("Contacts.Contact", "Website1", c => c.String(maxLength: 1000));
            AlterColumn("Contacts.Contact", "Email3", c => c.String(maxLength: 1000));
            AlterColumn("Contacts.Contact", "Email2", c => c.String(maxLength: 1000));
            AlterColumn("Contacts.Contact", "Email1", c => c.String(maxLength: 1000));
            AlterColumn("Contacts.Contact", "Fax3", c => c.String(maxLength: 50));
            AlterColumn("Contacts.Contact", "Fax2", c => c.String(maxLength: 50));
            AlterColumn("Contacts.Contact", "Fax1", c => c.String(maxLength: 50));
            AlterColumn("Contacts.Contact", "Phone3", c => c.String(maxLength: 50));
            AlterColumn("Contacts.Contact", "Phone2", c => c.String(maxLength: 50));
            AlterColumn("Contacts.Contact", "Phone1", c => c.String(maxLength: 50));
            DropColumn("Stores.StoreInventoryLocation", "TypeID");
            DropTable("Messaging.EmailQueueAttachment");
            RenameColumn(table: "Sampling.SampleRequestContact", name: "SampleRequestID", newName: "SampleRequest_ID");
            RenameColumn(table: "Returning.SalesReturnContact", name: "SalesReturnID", newName: "SalesReturn_ID");
            RenameColumn(table: "Quoting.SalesQuoteContact", name: "SalesQuoteID", newName: "SalesQuote_ID");
            RenameColumn(table: "Shopping.CartContact", name: "CartID", newName: "Cart_ID1");
            RenameColumn(table: "Invoicing.SalesInvoiceContact", name: "SalesInvoiceID", newName: "SalesInvoice_ID");
            RenameColumn(table: "Ordering.SalesOrderContact", name: "SalesOrderID", newName: "SalesOrder_ID");
            RenameColumn(table: "Purchasing.PurchaseOrderContact", name: "PurchaseOrderID", newName: "PurchaseOrder_ID");
            AddColumn("Purchasing.PurchaseOrderContact", "PurchaseOrderID", c => c.Int(nullable: false));
            AddColumn("Ordering.SalesOrderContact", "SalesOrderID", c => c.Int(nullable: false));
            AddColumn("Sampling.SampleRequestContact", "SampleRequestID", c => c.Int(nullable: false));
            AddColumn("Returning.SalesReturnContact", "SalesReturnID", c => c.Int(nullable: false));
            AddColumn("Quoting.SalesQuoteContact", "SalesQuoteID", c => c.Int(nullable: false));
            AddColumn("Shopping.CartContact", "CartID", c => c.Int(nullable: false));
            AddColumn("Invoicing.SalesInvoiceContact", "SalesInvoiceID", c => c.Int(nullable: false));
            CreateIndex("Advertising.AdImage", "AdID");
            CreateIndex("Advertising.AdImage", "LibraryID");
            CreateIndex("Advertising.AdImage", "Hash");
            CreateIndex("Advertising.AdImage", "Active");
            CreateIndex("Advertising.AdImage", "UpdatedDate");
            CreateIndex("Advertising.AdImage", "CustomKey");
            CreateIndex("Stores.StoreImage", "StoreID");
            CreateIndex("Stores.StoreImage", "LibraryID");
            CreateIndex("Stores.StoreImage", "Hash");
            CreateIndex("Stores.StoreImage", "Active");
            CreateIndex("Stores.StoreImage", "UpdatedDate");
            CreateIndex("Stores.StoreImage", "CustomKey");
            CreateIndex("Stores.StoreImage", "ID");
            CreateIndex("Purchasing.PurchaseOrderFile", "FileID");
            CreateIndex("Purchasing.PurchaseOrderFile", "PurchaseOrderID");
            CreateIndex("Purchasing.PurchaseOrderFile", "Name");
            CreateIndex("Purchasing.PurchaseOrderFile", "Hash");
            CreateIndex("Purchasing.PurchaseOrderFile", "Active");
            CreateIndex("Purchasing.PurchaseOrderFile", "UpdatedDate");
            CreateIndex("Purchasing.PurchaseOrderFile", "CustomKey");
            CreateIndex("Purchasing.PurchaseOrderFile", "ID");
            CreateIndex("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1");
            CreateIndex("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID");
            CreateIndex("Purchasing.PurchaseOrderContact", "PurchaseOrderID");
            CreateIndex("Ordering.SalesOrderFile", "FileID");
            CreateIndex("Ordering.SalesOrderFile", "SalesOrderID");
            CreateIndex("Ordering.SalesOrderFile", "Name");
            CreateIndex("Ordering.SalesOrderFile", "Hash");
            CreateIndex("Ordering.SalesOrderFile", "Active");
            CreateIndex("Ordering.SalesOrderFile", "UpdatedDate");
            CreateIndex("Ordering.SalesOrderFile", "CustomKey");
            CreateIndex("Ordering.SalesOrderFile", "ID");
            CreateIndex("Ordering.SalesOrderContact", "SalesOrder_ID1");
            CreateIndex("Ordering.SalesOrderContact", "SalesOrder_ID");
            CreateIndex("Ordering.SalesOrderContact", "SalesOrderID");
            CreateIndex("Invoicing.SalesInvoiceFile", "FileID");
            CreateIndex("Invoicing.SalesInvoiceFile", "SalesInvoiceID");
            CreateIndex("Invoicing.SalesInvoiceFile", "Name");
            CreateIndex("Invoicing.SalesInvoiceFile", "Hash");
            CreateIndex("Invoicing.SalesInvoiceFile", "Active");
            CreateIndex("Invoicing.SalesInvoiceFile", "UpdatedDate");
            CreateIndex("Invoicing.SalesInvoiceFile", "CustomKey");
            CreateIndex("Invoicing.SalesInvoiceFile", "ID");
            CreateIndex("Products.ProductImage", "TypeID");
            CreateIndex("Products.ProductImage", "ProductID");
            CreateIndex("Products.ProductImage", "LibraryID");
            CreateIndex("Products.ProductImage", "Hash");
            CreateIndex("Products.ProductImage", "Active");
            CreateIndex("Products.ProductImage", "UpdatedDate");
            CreateIndex("Products.ProductImage", "CustomKey");
            CreateIndex("Products.ProductImage", "ID");
            CreateIndex("Products.ProductFile", "FileID");
            CreateIndex("Products.ProductFile", "ProductID");
            CreateIndex("Products.ProductFile", "Name");
            CreateIndex("Products.ProductFile", "Hash");
            CreateIndex("Products.ProductFile", "Active");
            CreateIndex("Products.ProductFile", "UpdatedDate");
            CreateIndex("Products.ProductFile", "CustomKey");
            CreateIndex("Products.ProductFile", "ID");
            CreateIndex("Sampling.SampleRequestItem", "StoreProductID");
            CreateIndex("Sampling.SampleRequestItem", "VendorProductID");
            CreateIndex("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID");
            CreateIndex("Sampling.SampleRequestFile", "FileID");
            CreateIndex("Sampling.SampleRequestFile", "SampleRequestID");
            CreateIndex("Sampling.SampleRequestFile", "Name");
            CreateIndex("Sampling.SampleRequestFile", "Hash");
            CreateIndex("Sampling.SampleRequestFile", "Active");
            CreateIndex("Sampling.SampleRequestFile", "UpdatedDate");
            CreateIndex("Sampling.SampleRequestFile", "CustomKey");
            CreateIndex("Sampling.SampleRequestFile", "ID");
            CreateIndex("Sampling.SampleRequestContact", "SampleRequest_ID1");
            CreateIndex("Sampling.SampleRequestContact", "SampleRequest_ID");
            CreateIndex("Sampling.SampleRequestContact", "SampleRequestID");
            CreateIndex("Sampling.SampleRequest", "ShipOptionID");
            CreateIndex("Ordering.SalesOrderItem", "StoreProductID");
            CreateIndex("Ordering.SalesOrderItem", "VendorProductID");
            CreateIndex("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID");
            CreateIndex("Invoicing.SalesInvoiceItem", "StoreProductID");
            CreateIndex("Invoicing.SalesInvoiceItem", "VendorProductID");
            CreateIndex("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID");
            CreateIndex("Purchasing.PurchaseOrderItem", "StoreProductID");
            CreateIndex("Purchasing.PurchaseOrderItem", "VendorProductID");
            CreateIndex("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID");
            CreateIndex("CalendarEvents.CalendarEventFile", "FileID");
            CreateIndex("CalendarEvents.CalendarEventFile", "CalendarEventID");
            CreateIndex("CalendarEvents.CalendarEventFile", "Name");
            CreateIndex("CalendarEvents.CalendarEventFile", "Hash");
            CreateIndex("CalendarEvents.CalendarEventFile", "Active");
            CreateIndex("CalendarEvents.CalendarEventFile", "UpdatedDate");
            CreateIndex("CalendarEvents.CalendarEventFile", "CustomKey");
            CreateIndex("CalendarEvents.CalendarEventFile", "ID");
            CreateIndex("Shipping.ShipOption", "ShipCarrierMethodID");
            CreateIndex("Shipping.ShipOption", "Name");
            CreateIndex("Shipping.ShipOption", "Hash");
            CreateIndex("Shipping.ShipOption", "Active");
            CreateIndex("Shipping.ShipOption", "UpdatedDate");
            CreateIndex("Shipping.ShipOption", "CustomKey");
            CreateIndex("Shipping.ShipOption", "ID");
            CreateIndex("Quoting.SalesQuoteItem", "StoreProductID");
            CreateIndex("Quoting.SalesQuoteItem", "VendorProductID");
            CreateIndex("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID");
            CreateIndex("Returning.SalesReturnItem", "StoreProductID");
            CreateIndex("Returning.SalesReturnItem", "VendorProductID");
            CreateIndex("Returning.SalesReturnItem", "ProductInventoryLocationSectionID");
            CreateIndex("Returning.SalesReturnFile", "FileID");
            CreateIndex("Returning.SalesReturnFile", "SalesReturnID");
            CreateIndex("Returning.SalesReturnFile", "Name");
            CreateIndex("Returning.SalesReturnFile", "Hash");
            CreateIndex("Returning.SalesReturnFile", "Active");
            CreateIndex("Returning.SalesReturnFile", "UpdatedDate");
            CreateIndex("Returning.SalesReturnFile", "CustomKey");
            CreateIndex("Returning.SalesReturnFile", "ID");
            CreateIndex("Returning.SalesReturnContact", "SalesReturn_ID1");
            CreateIndex("Returning.SalesReturnContact", "SalesReturn_ID");
            CreateIndex("Returning.SalesReturnContact", "SalesReturnID");
            CreateIndex("Quoting.SalesQuoteFile", "FileID");
            CreateIndex("Quoting.SalesQuoteFile", "SalesQuoteID");
            CreateIndex("Quoting.SalesQuoteFile", "Name");
            CreateIndex("Quoting.SalesQuoteFile", "Hash");
            CreateIndex("Quoting.SalesQuoteFile", "Active");
            CreateIndex("Quoting.SalesQuoteFile", "UpdatedDate");
            CreateIndex("Quoting.SalesQuoteFile", "CustomKey");
            CreateIndex("Quoting.SalesQuoteFile", "ID");
            CreateIndex("Quoting.SalesQuoteContact", "SalesQuote_ID1");
            CreateIndex("Quoting.SalesQuoteContact", "SalesQuote_ID");
            CreateIndex("Quoting.SalesQuoteContact", "SalesQuoteID");
            CreateIndex("Quoting.SalesQuote", "ShipOptionID");
            CreateIndex("Shipping.ShipCarrier", "AddressID");
            CreateIndex("Shipping.ShipCarrier", "Email");
            CreateIndex("Shipping.ShipCarrier", "Fax");
            CreateIndex("Shipping.ShipCarrier", "Phone");
            CreateIndex("Inventory.InventoryLocation", "AddressID");
            CreateIndex("Inventory.InventoryLocation", "Email");
            CreateIndex("Inventory.InventoryLocation", "Fax");
            CreateIndex("Inventory.InventoryLocation", "Phone");
            CreateIndex("Vendors.Vendor", "Email");
            CreateIndex("Vendors.Vendor", "Fax");
            CreateIndex("Vendors.Vendor", "Phone");
            CreateIndex("Manufacturers.Manufacturer", "AddressID");
            CreateIndex("Manufacturers.Manufacturer", "Email");
            CreateIndex("Manufacturers.Manufacturer", "Fax");
            CreateIndex("Manufacturers.Manufacturer", "Phone");
            CreateIndex("Messaging.MessageAttachment", "LibraryID");
            CreateIndex("Shopping.CartFile", "FileID");
            CreateIndex("Shopping.CartFile", "CartID");
            CreateIndex("Shopping.CartFile", "Name");
            CreateIndex("Shopping.CartFile", "Hash");
            CreateIndex("Shopping.CartFile", "Active");
            CreateIndex("Shopping.CartFile", "UpdatedDate");
            CreateIndex("Shopping.CartFile", "CustomKey");
            CreateIndex("Shopping.CartFile", "ID");
            CreateIndex("Shopping.CartContact", "Cart_ID1");
            CreateIndex("Shopping.CartContact", "Cart_ID");
            CreateIndex("Shopping.CartContact", "CartID");
            CreateIndex("Shopping.Cart", "ShipOptionID");
            CreateIndex("Shopping.Cart", new[] { "SessionID", "TypeID", "UserID", "Active" }, unique: true, name: "Unq_BySessionTypeUserActive");
            CreateIndex("Shopping.CartItem", "StoreProductID");
            CreateIndex("Shopping.CartItem", "VendorProductID");
            CreateIndex("Shopping.CartItem", "ProductInventoryLocationSectionID");
            CreateIndex("Categories.CategoryFile", "FileID");
            CreateIndex("Categories.CategoryFile", "CategoryID");
            CreateIndex("Categories.CategoryFile", "Name");
            CreateIndex("Categories.CategoryFile", "Hash");
            CreateIndex("Categories.CategoryFile", "Active");
            CreateIndex("Categories.CategoryFile", "UpdatedDate");
            CreateIndex("Categories.CategoryFile", "CustomKey");
            CreateIndex("Categories.CategoryFile", "ID");
            CreateIndex("Categories.CategoryImage", "CategoryID");
            CreateIndex("Categories.CategoryImage", "LibraryID");
            CreateIndex("Categories.CategoryImage", "Hash");
            CreateIndex("Categories.CategoryImage", "Active");
            CreateIndex("Categories.CategoryImage", "UpdatedDate");
            CreateIndex("Categories.CategoryImage", "CustomKey");
            CreateIndex("Categories.CategoryImage", "ID");
            CreateIndex("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1");
            CreateIndex("Invoicing.SalesInvoiceContact", "SalesInvoice_ID");
            CreateIndex("Invoicing.SalesInvoiceContact", "SalesInvoiceID");
            CreateIndex("Invoicing.SalesInvoice", "ShipOptionID");
            CreateIndex("Geography.District", "LibraryID");
            CreateIndex("Geography.Region", "LibraryID");
            CreateIndex("Globalization.Language", "LibraryID");
            CreateIndex("Media.Video", "FileID");
            CreateIndex("Media.Video", "ID");
            CreateIndex("Media.Document", "FileID");
            CreateIndex("Media.Document", "ID");
            CreateIndex("Media.Image", "ThumbFileID");
            CreateIndex("Media.Image", "FullFileID");
            CreateIndex("Media.Image", "ID");
            CreateIndex("Media.FileData", "ID");
            CreateIndex("Media.File", "Hash");
            CreateIndex("Media.File", "Active");
            CreateIndex("Media.File", "UpdatedDate");
            CreateIndex("Media.File", "CustomKey");
            CreateIndex("Media.File", "ID");
            CreateIndex("Media.Audio", "ClipFileID");
            CreateIndex("Media.Audio", "FullFileID");
            CreateIndex("Media.Audio", "Hash");
            CreateIndex("Media.Audio", "ID");
            CreateIndex("Media.Library", "TypeID");
            CreateIndex("Media.Library", "Name");
            CreateIndex("Media.Library", "Hash");
            CreateIndex("Media.Library", "Active");
            CreateIndex("Media.Library", "UpdatedDate");
            CreateIndex("Media.Library", "CustomKey");
            CreateIndex("Media.Library", "ID");
            CreateIndex("Currencies.Currency", "LibraryID");
            CreateIndex("Geography.Country", "LibraryID");
            CreateIndex("Stores.Store", "SellerImageLibraryID");
            CreateIndex("Stores.Store", "LogoImageLibraryID");
            AddForeignKey("Sampling.SampleRequestContact", "SampleRequest_ID", "Sampling.SampleRequest", "ID");
            AddForeignKey("Returning.SalesReturnContact", "SalesReturn_ID", "Returning.SalesReturn", "ID");
            AddForeignKey("Quoting.SalesQuoteContact", "SalesQuote_ID", "Quoting.SalesQuote", "ID");
            AddForeignKey("Shopping.CartContact", "Cart_ID1", "Shopping.Cart", "ID");
            AddForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoice_ID", "Invoicing.SalesInvoice", "ID");
            AddForeignKey("Ordering.SalesOrderContact", "SalesOrder_ID", "Ordering.SalesOrder", "ID");
            AddForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID", "Purchasing.PurchaseOrder", "ID");
            AddForeignKey("Advertising.AdImage", "LibraryID", "Media.Library", "ID", cascadeDelete: true);
            AddForeignKey("Advertising.AdImage", "AdID", "Advertising.Ad", "ID", cascadeDelete: true);
            AddForeignKey("Stores.StoreImage", "StoreID", "Stores.Store", "ID", cascadeDelete: true);
            AddForeignKey("Stores.StoreImage", "LibraryID", "Media.Library", "ID", cascadeDelete: true);
            AddForeignKey("Stores.Store", "SellerImageLibraryID", "Media.Library", "ID");
            AddForeignKey("Stores.Store", "LogoImageLibraryID", "Media.Library", "ID");
            AddForeignKey("Purchasing.PurchaseOrderContact", "PurchaseOrder_ID1", "Purchasing.PurchaseOrder", "ID");
            AddForeignKey("Purchasing.PurchaseOrderFile", "PurchaseOrderID", "Purchasing.PurchaseOrder", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderContact", "SalesOrder_ID1", "Ordering.SalesOrder", "ID");
            AddForeignKey("Ordering.SalesOrderFile", "SalesOrderID", "Ordering.SalesOrder", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoice", "ShipOptionID", "Shipping.ShipOption", "ID");
            AddForeignKey("Invoicing.SalesInvoiceContact", "SalesInvoice_ID1", "Invoicing.SalesInvoice", "ID");
            AddForeignKey("Invoicing.SalesInvoiceFile", "SalesInvoiceID", "Invoicing.SalesInvoice", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoiceFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductImage", "TypeID", "Products.ProductImageType", "ID");
            AddForeignKey("Products.ProductImage", "ProductID", "Products.Product", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductImage", "LibraryID", "Media.Library", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductFile", "ProductID", "Products.Product", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartItem", "VendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Shopping.CartItem", "StoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Shopping.CartItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Shopping.Cart", "ShipOptionID", "Shipping.ShipOption", "ID");
            AddForeignKey("Sampling.SampleRequest", "ShipOptionID", "Shipping.ShipOption", "ID");
            AddForeignKey("Sampling.SampleRequestContact", "SampleRequest_ID1", "Sampling.SampleRequest", "ID");
            AddForeignKey("Sampling.SampleRequestItem", "VendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Sampling.SampleRequestItem", "StoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Sampling.SampleRequestItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Sampling.SampleRequestFile", "SampleRequestID", "Sampling.SampleRequest", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequestFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderItem", "VendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Ordering.SalesOrderItem", "StoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Ordering.SalesOrderItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItem", "VendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItem", "StoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItem", "VendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItem", "StoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("CalendarEvents.CalendarEventFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("CalendarEvents.CalendarEventFile", "CalendarEventID", "CalendarEvents.CalendarEvent", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuote", "ShipOptionID", "Shipping.ShipOption", "ID");
            AddForeignKey("Shipping.ShipOption", "ShipCarrierMethodID", "Shipping.ShipCarrierMethod", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteContact", "SalesQuote_ID1", "Quoting.SalesQuote", "ID");
            AddForeignKey("Quoting.SalesQuoteItem", "VendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Quoting.SalesQuoteItem", "StoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Quoting.SalesQuoteItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Returning.SalesReturnContact", "SalesReturn_ID1", "Returning.SalesReturn", "ID");
            AddForeignKey("Returning.SalesReturnItem", "VendorProductID", "Vendors.VendorProduct", "ID");
            AddForeignKey("Returning.SalesReturnItem", "StoreProductID", "Stores.StoreProduct", "ID");
            AddForeignKey("Returning.SalesReturnItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection", "ID");
            AddForeignKey("Returning.SalesReturnFile", "SalesReturnID", "Returning.SalesReturn", "ID", cascadeDelete: true);
            AddForeignKey("Returning.SalesReturnFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteFile", "SalesQuoteID", "Quoting.SalesQuote", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Shipping.ShipCarrier", "AddressID", "Geography.Address", "ID");
            AddForeignKey("Inventory.InventoryLocation", "AddressID", "Geography.Address", "ID");
            AddForeignKey("Manufacturers.Manufacturer", "AddressID", "Geography.Address", "ID");
            AddForeignKey("Messaging.MessageAttachment", "LibraryID", "Media.Library", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartFile", "CartID", "Shopping.Cart", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartContact", "Cart_ID", "Shopping.Cart", "ID");
            AddForeignKey("Categories.CategoryFile", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Categories.CategoryFile", "CategoryID", "Categories.Category", "ID", cascadeDelete: true);
            AddForeignKey("Categories.CategoryImage", "LibraryID", "Media.Library", "ID", cascadeDelete: true);
            AddForeignKey("Categories.CategoryImage", "CategoryID", "Categories.Category", "ID", cascadeDelete: true);
            AddForeignKey("Geography.Region", "LibraryID", "Media.Library", "ID");
            AddForeignKey("Geography.District", "LibraryID", "Media.Library", "ID");
            AddForeignKey("Geography.Country", "LibraryID", "Media.Library", "ID");
            AddForeignKey("Globalization.Language", "LibraryID", "Media.Library", "ID");
            AddForeignKey("Currencies.Currency", "LibraryID", "Media.Library", "ID");
            AddForeignKey("Media.Video", "ID", "Media.Library", "ID");
            AddForeignKey("Media.Video", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Media.Library", "TypeID", "Media.LibraryType", "ID");
            AddForeignKey("Media.Document", "ID", "Media.Library", "ID");
            AddForeignKey("Media.Document", "FileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Media.Audio", "ID", "Media.Library", "ID");
            AddForeignKey("Media.Audio", "FullFileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Media.Audio", "ClipFileID", "Media.File", "ID");
            AddForeignKey("Media.Image", "ThumbFileID", "Media.File", "ID");
            AddForeignKey("Media.Image", "ID", "Media.Library", "ID");
            AddForeignKey("Media.Image", "FullFileID", "Media.File", "ID", cascadeDelete: true);
            AddForeignKey("Media.FileData", "ID", "Media.File", "ID");
            MoveTable(name: "Stores.StoreInventoryLocationType", newSchema: "Media");
            RenameTable(name: "Media.StoreInventoryLocationType", newName: "LibraryType");
        }
    }
}
