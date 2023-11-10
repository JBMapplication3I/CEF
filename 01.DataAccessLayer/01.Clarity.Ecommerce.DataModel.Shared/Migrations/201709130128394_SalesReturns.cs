// <copyright file="201709130128394_SalesReturns.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201709130128394 sales returns class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SalesReturns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Returning.SalesReturn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        DueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SubtotalItems = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalShipping = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalTaxes = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalDiscounts = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalFees = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SubtotalHandling = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 4),
                        ShippingSameAsBilling = c.Boolean(),
                        BillingContactID = c.Int(nullable: false),
                        ShippingContactID = c.Int(),
                        StatusID = c.Int(),
                        StateID = c.Int(),
                        TypeID = c.Int(),
                        UserID = c.Int(),
                        AccountID = c.Int(),
                        JsonAttributes = c.String(),
                        ParentID = c.Int(),
                        StoreID = c.Int(),
                        PurchaseOrderNumber = c.String(maxLength: 100),
                        BalanceDue = c.Decimal(precision: 18, scale: 4),
                        OrderStateName = c.String(),
                        TrackingNumber = c.String(),
                        RefundTransactionID = c.String(maxLength: 256),
                        TaxTransactionID = c.String(maxLength: 256),
                        ReturnApprovedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ReturnCommitmentDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RequiredShipDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RequestedShipDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ActualShipDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RefundAmount = c.Decimal(precision: 18, scale: 4),
                        CustomerPriorityID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.Contact", t => t.BillingContactID)
                .ForeignKey("Returning.SalesReturn", t => t.ParentID)
                .ForeignKey("Contacts.CustomerPriority", t => t.CustomerPriorityID)
                .ForeignKey("Contacts.Contact", t => t.ShippingContactID)
                .ForeignKey("Returning.SalesReturnState", t => t.StateID)
                .ForeignKey("Returning.SalesReturnStatus", t => t.StatusID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Returning.SalesReturnType", t => t.TypeID)
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
                .Index(t => t.ParentID)
                .Index(t => t.StoreID)
                .Index(t => t.CustomerPriorityID);

            CreateTable(
                "Returning.SalesReturnSalesOrder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        SalesOrderID = c.Int(nullable: false),
                        SalesReturnID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturn", t => t.SalesReturnID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesOrderID)
                .Index(t => t.SalesReturnID);

            CreateTable(
                "Returning.SalesReturnAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.GeneralAttribute", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturn", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Returning.SalesReturnContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        SalesReturnID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        SalesReturn_ID = c.Int(),
                        SalesReturn_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturn", t => t.SalesReturnID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturn", t => t.SalesReturn_ID)
                .ForeignKey("Returning.SalesReturn", t => t.SalesReturn_ID1)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesReturnID)
                .Index(t => t.ContactID)
                .Index(t => t.SalesReturn_ID)
                .Index(t => t.SalesReturn_ID1);

            CreateTable(
                "Discounts.SalesReturnDiscounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturn", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

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
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        SalesReturnID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturn", t => t.SalesReturnID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.SalesReturnID)
                .Index(t => t.FileID);

            CreateTable(
                "Returning.SalesReturnItem",
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
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        QuantityBackOrdered = c.Decimal(precision: 18, scale: 4),
                        Sku = c.String(maxLength: 1000, unicode: false),
                        UnitCorePrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        UnitSoldPrice = c.Decimal(precision: 18, scale: 4),
                        UnitCorePriceInSellingCurrency = c.Decimal(precision: 18, scale: 4),
                        UnitSoldPriceInSellingCurrency = c.Decimal(precision: 18, scale: 4),
                        UnitOfMeasure = c.String(maxLength: 1000, unicode: false),
                        ProductID = c.Int(),
                        ProductInventoryLocationSectionID = c.Int(),
                        UserID = c.Int(),
                        MasterID = c.Int(nullable: false),
                        StatusID = c.Int(),
                        VendorProductID = c.Int(),
                        StoreProductID = c.Int(),
                        OriginalCurrencyID = c.Int(),
                        SellingCurrencyID = c.Int(),
                        JsonAttributes = c.String(),
                        ShippingCarrierName = c.String(maxLength: 255, unicode: false),
                        ShippingCarrierMethodName = c.String(maxLength: 255, unicode: false),
                        TrackingNumber = c.String(maxLength: 100, unicode: false),
                        SalesReturnReasonID = c.Int(),
                        RestockingFeeAmount = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Returning.SalesReturn", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Currencies.Currency", t => t.OriginalCurrencyID)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Products.ProductInventoryLocationSection", t => t.ProductInventoryLocationSectionID)
                .ForeignKey("Returning.SalesReturnReason", t => t.SalesReturnReasonID)
                .ForeignKey("Currencies.Currency", t => t.SellingCurrencyID)
                .ForeignKey("Returning.SalesReturnItemStatus", t => t.StatusID)
                .ForeignKey("Stores.StoreProduct", t => t.StoreProductID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .ForeignKey("Vendors.VendorProduct", t => t.VendorProductID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.ProductID)
                .Index(t => t.ProductInventoryLocationSectionID)
                .Index(t => t.UserID)
                .Index(t => t.MasterID)
                .Index(t => t.StatusID)
                .Index(t => t.VendorProductID)
                .Index(t => t.StoreProductID)
                .Index(t => t.OriginalCurrencyID)
                .Index(t => t.SellingCurrencyID)
                .Index(t => t.SalesReturnReasonID);

            CreateTable(
                "Returning.SalesReturnItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        SortOrder = c.Int(),
                        Value = c.String(unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.GeneralAttribute", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturnItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.SalesReturnItemDiscounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturnItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Returning.SalesReturnItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        SalesReturnItemID = c.Int(nullable: false),
                        ShipmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Returning.SalesReturnItem", t => t.SalesReturnItemID, cascadeDelete: true)
                .ForeignKey("Shipping.Shipment", t => t.ShipmentID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesReturnItemID)
                .Index(t => t.ShipmentID);

            CreateTable(
                "Returning.SalesReturnReason",
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
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        IsRestockingFeeApplicable = c.Boolean(nullable: false),
                        RestockingFeePercent = c.Decimal(precision: 18, scale: 5),
                        RestockingFeeAmount = c.Decimal(precision: 18, scale: 4),
                        RestockingFeeAmountCurrencyID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Currencies.Currency", t => t.RestockingFeeAmountCurrencyID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.RestockingFeeAmountCurrencyID);

            CreateTable(
                "Returning.SalesReturnItemStatus",
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
                "Payments.SalesReturnPayment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        SalesReturnID = c.Int(nullable: false),
                        PaymentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Payments.Payment", t => t.PaymentID, cascadeDelete: true)
                .ForeignKey("Returning.SalesReturn", t => t.SalesReturnID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesReturnID)
                .Index(t => t.PaymentID);

            CreateTable(
                "Returning.SalesReturnState",
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
                "Returning.SalesReturnStatus",
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
                "Returning.SalesReturnFileNew",
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
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Returning.SalesReturn", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Returning.SalesReturnType",
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

            AddColumn("Categories.Category", "RestockingFeePercent", c => c.Decimal(precision: 18, scale: 5));
            AddColumn("Categories.Category", "RestockingFeeAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Categories.Category", "RestockingFeeAmountCurrencyID", c => c.Int());
            AddColumn("Products.Product", "RestockingFeePercent", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Products.Product", "RestockingFeeAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Products.Product", "IsEligibleForReturn", c => c.Boolean());
            AddColumn("Products.Product", "RestockingFeeAmountCurrencyID", c => c.Int());
            AddColumn("System.Note", "SalesReturnID", c => c.Int());
            AddColumn("System.Note", "SalesReturnItemID", c => c.Int());
            CreateIndex("Categories.Category", "RestockingFeeAmountCurrencyID");
            CreateIndex("Products.Product", "RestockingFeeAmountCurrencyID");
            CreateIndex("System.Note", "SalesReturnID");
            CreateIndex("System.Note", "SalesReturnItemID");
            AddForeignKey("System.Note", "SalesReturnID", "Returning.SalesReturn", "ID");
            AddForeignKey("System.Note", "SalesReturnItemID", "Returning.SalesReturnItem", "ID");
            AddForeignKey("Products.Product", "RestockingFeeAmountCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Categories.Category", "RestockingFeeAmountCurrencyID", "Currencies.Currency", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Categories.Category", "RestockingFeeAmountCurrencyID", "Currencies.Currency");
            DropForeignKey("Products.Product", "RestockingFeeAmountCurrencyID", "Currencies.Currency");
            DropForeignKey("System.Note", "SalesReturnItemID", "Returning.SalesReturnItem");
            DropForeignKey("System.Note", "SalesReturnID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturn", "UserID", "Contacts.User");
            DropForeignKey("Returning.SalesReturn", "TypeID", "Returning.SalesReturnType");
            DropForeignKey("Returning.SalesReturnFileNew", "SlaveID", "Media.StoredFile");
            DropForeignKey("Returning.SalesReturnFileNew", "MasterID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturn", "StoreID", "Stores.Store");
            DropForeignKey("Returning.SalesReturn", "StatusID", "Returning.SalesReturnStatus");
            DropForeignKey("Returning.SalesReturn", "StateID", "Returning.SalesReturnState");
            DropForeignKey("Returning.SalesReturn", "ShippingContactID", "Contacts.Contact");
            DropForeignKey("Payments.SalesReturnPayment", "SalesReturnID", "Returning.SalesReturn");
            DropForeignKey("Payments.SalesReturnPayment", "PaymentID", "Payments.Payment");
            DropForeignKey("Returning.SalesReturnContact", "SalesReturn_ID1", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Returning.SalesReturnItem", "UserID", "Contacts.User");
            DropForeignKey("Returning.SalesReturnItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Returning.SalesReturnItem", "StatusID", "Returning.SalesReturnItemStatus");
            DropForeignKey("Returning.SalesReturnItem", "SellingCurrencyID", "Currencies.Currency");
            DropForeignKey("Returning.SalesReturnItem", "SalesReturnReasonID", "Returning.SalesReturnReason");
            DropForeignKey("Returning.SalesReturnReason", "RestockingFeeAmountCurrencyID", "Currencies.Currency");
            DropForeignKey("Returning.SalesReturnItemShipment", "ShipmentID", "Shipping.Shipment");
            DropForeignKey("Returning.SalesReturnItemShipment", "SalesReturnItemID", "Returning.SalesReturnItem");
            DropForeignKey("Returning.SalesReturnItem", "ProductInventoryLocationSectionID", "Products.ProductInventoryLocationSection");
            DropForeignKey("Returning.SalesReturnItem", "ProductID", "Products.Product");
            DropForeignKey("Returning.SalesReturnItem", "OriginalCurrencyID", "Currencies.Currency");
            DropForeignKey("Returning.SalesReturnItem", "MasterID", "Returning.SalesReturn");
            DropForeignKey("Discounts.SalesReturnItemDiscounts", "MasterID", "Returning.SalesReturnItem");
            DropForeignKey("Discounts.SalesReturnItemDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Returning.SalesReturnItemAttribute", "MasterID", "Returning.SalesReturnItem");
            DropForeignKey("Returning.SalesReturnItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Returning.SalesReturnFile", "SalesReturnID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnFile", "FileID", "Media.File");
            DropForeignKey("Discounts.SalesReturnDiscounts", "MasterID", "Returning.SalesReturn");
            DropForeignKey("Discounts.SalesReturnDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Returning.SalesReturn", "CustomerPriorityID", "Contacts.CustomerPriority");
            DropForeignKey("Returning.SalesReturnContact", "SalesReturn_ID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnContact", "SalesReturnID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnContact", "ContactID", "Contacts.Contact");
            DropForeignKey("Returning.SalesReturn", "ParentID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturn", "BillingContactID", "Contacts.Contact");
            DropForeignKey("Returning.SalesReturnAttribute", "MasterID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Returning.SalesReturnSalesOrder", "SalesReturnID", "Returning.SalesReturn");
            DropForeignKey("Returning.SalesReturnSalesOrder", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Returning.SalesReturn", "AccountID", "Accounts.Account");
            DropIndex("Returning.SalesReturnType", new[] { "SortOrder" });
            DropIndex("Returning.SalesReturnType", new[] { "DisplayName" });
            DropIndex("Returning.SalesReturnType", new[] { "Name" });
            DropIndex("Returning.SalesReturnType", new[] { "Active" });
            DropIndex("Returning.SalesReturnType", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnType", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnType", new[] { "ID" });
            DropIndex("Returning.SalesReturnFileNew", new[] { "SlaveID" });
            DropIndex("Returning.SalesReturnFileNew", new[] { "MasterID" });
            DropIndex("Returning.SalesReturnFileNew", new[] { "Name" });
            DropIndex("Returning.SalesReturnFileNew", new[] { "Hash" });
            DropIndex("Returning.SalesReturnFileNew", new[] { "Active" });
            DropIndex("Returning.SalesReturnFileNew", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnFileNew", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnFileNew", new[] { "ID" });
            DropIndex("Returning.SalesReturnStatus", new[] { "SortOrder" });
            DropIndex("Returning.SalesReturnStatus", new[] { "DisplayName" });
            DropIndex("Returning.SalesReturnStatus", new[] { "Name" });
            DropIndex("Returning.SalesReturnStatus", new[] { "Active" });
            DropIndex("Returning.SalesReturnStatus", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnStatus", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnStatus", new[] { "ID" });
            DropIndex("Returning.SalesReturnState", new[] { "SortOrder" });
            DropIndex("Returning.SalesReturnState", new[] { "DisplayName" });
            DropIndex("Returning.SalesReturnState", new[] { "Name" });
            DropIndex("Returning.SalesReturnState", new[] { "Active" });
            DropIndex("Returning.SalesReturnState", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnState", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnState", new[] { "ID" });
            DropIndex("Payments.SalesReturnPayment", new[] { "PaymentID" });
            DropIndex("Payments.SalesReturnPayment", new[] { "SalesReturnID" });
            DropIndex("Payments.SalesReturnPayment", new[] { "Active" });
            DropIndex("Payments.SalesReturnPayment", new[] { "UpdatedDate" });
            DropIndex("Payments.SalesReturnPayment", new[] { "CustomKey" });
            DropIndex("Payments.SalesReturnPayment", new[] { "ID" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "SortOrder" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "DisplayName" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "Name" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "Active" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnItemStatus", new[] { "ID" });
            DropIndex("Returning.SalesReturnReason", new[] { "RestockingFeeAmountCurrencyID" });
            DropIndex("Returning.SalesReturnReason", new[] { "SortOrder" });
            DropIndex("Returning.SalesReturnReason", new[] { "DisplayName" });
            DropIndex("Returning.SalesReturnReason", new[] { "Name" });
            DropIndex("Returning.SalesReturnReason", new[] { "Active" });
            DropIndex("Returning.SalesReturnReason", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnReason", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnReason", new[] { "ID" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "ShipmentID" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "SalesReturnItemID" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "Active" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "ID" });
            DropIndex("Discounts.SalesReturnItemDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.SalesReturnItemDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.SalesReturnItemDiscounts", new[] { "Active" });
            DropIndex("Discounts.SalesReturnItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.SalesReturnItemDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.SalesReturnItemDiscounts", new[] { "ID" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "MasterID" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "AttributeID" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "Active" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnItemAttribute", new[] { "ID" });
            DropIndex("Returning.SalesReturnItem", new[] { "SalesReturnReasonID" });
            DropIndex("Returning.SalesReturnItem", new[] { "SellingCurrencyID" });
            DropIndex("Returning.SalesReturnItem", new[] { "OriginalCurrencyID" });
            DropIndex("Returning.SalesReturnItem", new[] { "StoreProductID" });
            DropIndex("Returning.SalesReturnItem", new[] { "VendorProductID" });
            DropIndex("Returning.SalesReturnItem", new[] { "StatusID" });
            DropIndex("Returning.SalesReturnItem", new[] { "MasterID" });
            DropIndex("Returning.SalesReturnItem", new[] { "UserID" });
            DropIndex("Returning.SalesReturnItem", new[] { "ProductInventoryLocationSectionID" });
            DropIndex("Returning.SalesReturnItem", new[] { "ProductID" });
            DropIndex("Returning.SalesReturnItem", new[] { "Name" });
            DropIndex("Returning.SalesReturnItem", new[] { "Active" });
            DropIndex("Returning.SalesReturnItem", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnItem", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnItem", new[] { "ID" });
            DropIndex("Returning.SalesReturnFile", new[] { "FileID" });
            DropIndex("Returning.SalesReturnFile", new[] { "SalesReturnID" });
            DropIndex("Returning.SalesReturnFile", new[] { "Name" });
            DropIndex("Returning.SalesReturnFile", new[] { "Hash" });
            DropIndex("Returning.SalesReturnFile", new[] { "Active" });
            DropIndex("Returning.SalesReturnFile", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnFile", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnFile", new[] { "ID" });
            DropIndex("Discounts.SalesReturnDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.SalesReturnDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.SalesReturnDiscounts", new[] { "Active" });
            DropIndex("Discounts.SalesReturnDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.SalesReturnDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.SalesReturnDiscounts", new[] { "ID" });
            DropIndex("Returning.SalesReturnContact", new[] { "SalesReturn_ID1" });
            DropIndex("Returning.SalesReturnContact", new[] { "SalesReturn_ID" });
            DropIndex("Returning.SalesReturnContact", new[] { "ContactID" });
            DropIndex("Returning.SalesReturnContact", new[] { "SalesReturnID" });
            DropIndex("Returning.SalesReturnContact", new[] { "Active" });
            DropIndex("Returning.SalesReturnContact", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnContact", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnContact", new[] { "ID" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "MasterID" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "AttributeID" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "Active" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnAttribute", new[] { "ID" });
            DropIndex("Returning.SalesReturnSalesOrder", new[] { "SalesReturnID" });
            DropIndex("Returning.SalesReturnSalesOrder", new[] { "SalesOrderID" });
            DropIndex("Returning.SalesReturnSalesOrder", new[] { "Active" });
            DropIndex("Returning.SalesReturnSalesOrder", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnSalesOrder", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnSalesOrder", new[] { "ID" });
            DropIndex("Returning.SalesReturn", new[] { "CustomerPriorityID" });
            DropIndex("Returning.SalesReturn", new[] { "StoreID" });
            DropIndex("Returning.SalesReturn", new[] { "ParentID" });
            DropIndex("Returning.SalesReturn", new[] { "AccountID" });
            DropIndex("Returning.SalesReturn", new[] { "UserID" });
            DropIndex("Returning.SalesReturn", new[] { "TypeID" });
            DropIndex("Returning.SalesReturn", new[] { "StateID" });
            DropIndex("Returning.SalesReturn", new[] { "StatusID" });
            DropIndex("Returning.SalesReturn", new[] { "ShippingContactID" });
            DropIndex("Returning.SalesReturn", new[] { "BillingContactID" });
            DropIndex("Returning.SalesReturn", new[] { "Active" });
            DropIndex("Returning.SalesReturn", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturn", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturn", new[] { "ID" });
            DropIndex("System.Note", new[] { "SalesReturnItemID" });
            DropIndex("System.Note", new[] { "SalesReturnID" });
            DropIndex("Products.Product", new[] { "RestockingFeeAmountCurrencyID" });
            DropIndex("Categories.Category", new[] { "RestockingFeeAmountCurrencyID" });
            DropColumn("System.Note", "SalesReturnItemID");
            DropColumn("System.Note", "SalesReturnID");
            DropColumn("Products.Product", "RestockingFeeAmountCurrencyID");
            DropColumn("Products.Product", "IsEligibleForReturn");
            DropColumn("Products.Product", "RestockingFeeAmount");
            DropColumn("Products.Product", "RestockingFeePercent");
            DropColumn("Categories.Category", "RestockingFeeAmountCurrencyID");
            DropColumn("Categories.Category", "RestockingFeeAmount");
            DropColumn("Categories.Category", "RestockingFeePercent");
            DropTable("Returning.SalesReturnType");
            DropTable("Returning.SalesReturnFileNew");
            DropTable("Returning.SalesReturnStatus");
            DropTable("Returning.SalesReturnState");
            DropTable("Payments.SalesReturnPayment");
            DropTable("Returning.SalesReturnItemStatus");
            DropTable("Returning.SalesReturnReason");
            DropTable("Returning.SalesReturnItemShipment");
            DropTable("Discounts.SalesReturnItemDiscounts");
            DropTable("Returning.SalesReturnItemAttribute");
            DropTable("Returning.SalesReturnItem");
            DropTable("Returning.SalesReturnFile");
            DropTable("Discounts.SalesReturnDiscounts");
            DropTable("Returning.SalesReturnContact");
            DropTable("Returning.SalesReturnAttribute");
            DropTable("Returning.SalesReturnSalesOrder");
            DropTable("Returning.SalesReturn");
        }
    }
}
