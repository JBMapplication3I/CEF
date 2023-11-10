// <copyright file="201612070517527_SalesQuotesAndSampleRequestsSchema.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201612070517527 sales quotes and sample requests schema class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SalesQuotesAndSampleRequestsSchema : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Discounts.QuoteItemDiscounts", newName: "SalesQuoteItemDiscounts");
            CreateTable(
                "Sampling.SampleRequest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        DueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SubtotalItems = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalShipping = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalTaxes = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalFees = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalHandling = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalDiscounts = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 4),
                        ShippingSameAsBilling = c.Boolean(),
                        BillingContactID = c.Int(),
                        ShippingContactID = c.Int(),
                        StatusID = c.Int(),
                        StateID = c.Int(),
                        TypeID = c.Int(),
                        UserID = c.Int(),
                        AccountID = c.Int(),
                        JsonAttributes = c.String(),
                        StoreID = c.Int(),
                        ShipOptionID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.Contact", t => t.BillingContactID)
                .ForeignKey("Shipping.ShipOption", t => t.ShipOptionID)
                .ForeignKey("Contacts.Contact", t => t.ShippingContactID)
                .ForeignKey("Sampling.SampleRequestState", t => t.StateID)
                .ForeignKey("Sampling.SampleRequestStatus", t => t.StatusID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Sampling.SampleRequestType", t => t.TypeID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.BillingContactID)
                .Index(t => t.ShippingContactID)
                .Index(t => t.StatusID)
                .Index(t => t.StateID)
                .Index(t => t.TypeID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.StoreID)
                .Index(t => t.ShipOptionID);

            CreateTable(
                "Sampling.SampleRequestAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.GeneralAttribute", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("Sampling.SampleRequest", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.SampleRequestDiscounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("Sampling.SampleRequest", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Sampling.SampleRequestItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        QuantityBackOrdered = c.Decimal(precision: 18, scale: 4),
                        Sku = c.String(maxLength: 1000, unicode: false),
                        UnitCorePrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        UnitSoldPrice = c.Decimal(precision: 18, scale: 4),
                        UnitOfMeasure = c.String(maxLength: 100, unicode: false),
                        ProductID = c.Int(),
                        UserID = c.Int(),
                        MasterID = c.Int(nullable: false),
                        StatusID = c.Int(),
                        VendorProductID = c.Int(),
                        StoreProductID = c.Int(),
                        JsonAttributes = c.String(),
                        TrackingNumber = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Sampling.SampleRequest", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Sampling.SampleRequestItemStatus", t => t.StatusID)
                .ForeignKey("Stores.StoreProduct", t => t.StoreProductID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .ForeignKey("Vendors.VendorProduct", t => t.VendorProductID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.ProductID)
                .Index(t => t.UserID)
                .Index(t => t.MasterID)
                .Index(t => t.StatusID)
                .Index(t => t.VendorProductID)
                .Index(t => t.StoreProductID);

            CreateTable(
                "Sampling.SampleRequestItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.GeneralAttribute", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("Sampling.SampleRequestItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.SampleRequestItemDiscounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("Sampling.SampleRequestItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Sampling.SampleRequestItemStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Sampling.SampleRequestState",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Sampling.SampleRequestStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Sampling.SampleRequestType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            AddColumn("System.Note", "SampleRequestID", c => c.Int());
            CreateIndex("System.Note", "SampleRequestID");
            AddForeignKey("System.Note", "SampleRequestID", "Sampling.SampleRequest", "ID");
        }

        public override void Down()
        {
            DropForeignKey("System.Note", "SampleRequestID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequest", "UserID", "Contacts.User");
            DropForeignKey("Sampling.SampleRequest", "TypeID", "Sampling.SampleRequestType");
            DropForeignKey("Sampling.SampleRequest", "StoreID", "Stores.Store");
            DropForeignKey("Sampling.SampleRequest", "StatusID", "Sampling.SampleRequestStatus");
            DropForeignKey("Sampling.SampleRequest", "StateID", "Sampling.SampleRequestState");
            DropForeignKey("Sampling.SampleRequest", "ShippingContactID", "Contacts.Contact");
            DropForeignKey("Sampling.SampleRequest", "ShipOptionID", "Shipping.ShipOption");
            DropForeignKey("Sampling.SampleRequestItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Sampling.SampleRequestItem", "UserID", "Contacts.User");
            DropForeignKey("Sampling.SampleRequestItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Sampling.SampleRequestItem", "StatusID", "Sampling.SampleRequestItemStatus");
            DropForeignKey("Sampling.SampleRequestItem", "ProductID", "Products.Product");
            DropForeignKey("Sampling.SampleRequestItem", "MasterID", "Sampling.SampleRequest");
            DropForeignKey("Discounts.SampleRequestItemDiscounts", "MasterID", "Sampling.SampleRequestItem");
            DropForeignKey("Discounts.SampleRequestItemDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Sampling.SampleRequestItemAttribute", "MasterID", "Sampling.SampleRequestItem");
            DropForeignKey("Sampling.SampleRequestItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Discounts.SampleRequestDiscounts", "MasterID", "Sampling.SampleRequest");
            DropForeignKey("Discounts.SampleRequestDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Sampling.SampleRequest", "BillingContactID", "Contacts.Contact");
            DropForeignKey("Sampling.SampleRequestAttribute", "MasterID", "Sampling.SampleRequest");
            DropForeignKey("Sampling.SampleRequestAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Sampling.SampleRequest", "AccountID", "Accounts.Account");
            DropIndex("Sampling.SampleRequestType", new[] { "SortOrder" });
            DropIndex("Sampling.SampleRequestType", new[] { "DisplayName" });
            DropIndex("Sampling.SampleRequestType", new[] { "Name" });
            DropIndex("Sampling.SampleRequestType", new[] { "Active" });
            DropIndex("Sampling.SampleRequestType", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestType", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestType", new[] { "ID" });
            DropIndex("Sampling.SampleRequestStatus", new[] { "SortOrder" });
            DropIndex("Sampling.SampleRequestStatus", new[] { "DisplayName" });
            DropIndex("Sampling.SampleRequestStatus", new[] { "Name" });
            DropIndex("Sampling.SampleRequestStatus", new[] { "Active" });
            DropIndex("Sampling.SampleRequestStatus", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestStatus", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestStatus", new[] { "ID" });
            DropIndex("Sampling.SampleRequestState", new[] { "SortOrder" });
            DropIndex("Sampling.SampleRequestState", new[] { "DisplayName" });
            DropIndex("Sampling.SampleRequestState", new[] { "Name" });
            DropIndex("Sampling.SampleRequestState", new[] { "Active" });
            DropIndex("Sampling.SampleRequestState", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestState", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestState", new[] { "ID" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "SortOrder" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "DisplayName" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "Name" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "Active" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestItemStatus", new[] { "ID" });
            DropIndex("Discounts.SampleRequestItemDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.SampleRequestItemDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.SampleRequestItemDiscounts", new[] { "Active" });
            DropIndex("Discounts.SampleRequestItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.SampleRequestItemDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.SampleRequestItemDiscounts", new[] { "ID" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "MasterID" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "AttributeID" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "Active" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestItemAttribute", new[] { "ID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "StoreProductID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "VendorProductID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "StatusID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "MasterID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "UserID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "ProductID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "Name" });
            DropIndex("Sampling.SampleRequestItem", new[] { "Active" });
            DropIndex("Sampling.SampleRequestItem", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestItem", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestItem", new[] { "ID" });
            DropIndex("Discounts.SampleRequestDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.SampleRequestDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.SampleRequestDiscounts", new[] { "Active" });
            DropIndex("Discounts.SampleRequestDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.SampleRequestDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.SampleRequestDiscounts", new[] { "ID" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "MasterID" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "AttributeID" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "Active" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestAttribute", new[] { "ID" });
            DropIndex("Sampling.SampleRequest", new[] { "ShipOptionID" });
            DropIndex("Sampling.SampleRequest", new[] { "StoreID" });
            DropIndex("Sampling.SampleRequest", new[] { "AccountID" });
            DropIndex("Sampling.SampleRequest", new[] { "UserID" });
            DropIndex("Sampling.SampleRequest", new[] { "TypeID" });
            DropIndex("Sampling.SampleRequest", new[] { "StateID" });
            DropIndex("Sampling.SampleRequest", new[] { "StatusID" });
            DropIndex("Sampling.SampleRequest", new[] { "ShippingContactID" });
            DropIndex("Sampling.SampleRequest", new[] { "BillingContactID" });
            DropIndex("Sampling.SampleRequest", new[] { "Active" });
            DropIndex("Sampling.SampleRequest", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequest", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequest", new[] { "ID" });
            DropIndex("System.Note", new[] { "SampleRequestID" });
            DropColumn("System.Note", "SampleRequestID");
            DropTable("Sampling.SampleRequestType");
            DropTable("Sampling.SampleRequestStatus");
            DropTable("Sampling.SampleRequestState");
            DropTable("Sampling.SampleRequestItemStatus");
            DropTable("Discounts.SampleRequestItemDiscounts");
            DropTable("Sampling.SampleRequestItemAttribute");
            DropTable("Sampling.SampleRequestItem");
            DropTable("Discounts.SampleRequestDiscounts");
            DropTable("Sampling.SampleRequestAttribute");
            DropTable("Sampling.SampleRequest");
            RenameTable(name: "Discounts.SalesQuoteItemDiscounts", newName: "QuoteItemDiscounts");
        }
    }
}
