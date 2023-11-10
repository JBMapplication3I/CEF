// <copyright file="201612021738169_InitialCreate_4pt7.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201612021738169 initial create 4pt 7 class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate_4pt7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Accounts.AccountAddress",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        IsPrimary = c.Boolean(nullable: false),
                        IsBilling = c.Boolean(nullable: false),
                        TransmittedToERP = c.Boolean(nullable: false),
                        AccountID = c.Int(nullable: false),
                        AddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("Geography.Address", t => t.AddressID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.AccountID)
                .Index(t => t.AddressID);

            CreateTable(
                "Accounts.Account",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Phone = c.String(maxLength: 64, unicode: false),
                        Fax = c.String(maxLength: 64, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        ParentID = c.Int(),
                        JsonAttributes = c.String(),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        Website = c.String(maxLength: 1000, unicode: false),
                        IsTaxable = c.Boolean(),
                        TaxExemptionNo = c.String(),
                        TaxEntityUseCode = c.String(),
                        IsOnHold = c.Boolean(nullable: false),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        AccountTermID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.ParentID)
                .ForeignKey("Accounts.AccountStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Accounts.AccountType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.Phone)
                .Index(t => t.Fax)
                .Index(t => t.Email)
                .Index(t => t.ParentID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID);

            CreateTable(
                "Accounts.AccountContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        IsPrimary = c.Boolean(nullable: false),
                        IsBilling = c.Boolean(nullable: false),
                        TransmittedToERP = c.Boolean(nullable: false),
                        AccountID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.AccountID)
                .Index(t => t.ContactID);

            CreateTable(
                "Contacts.Contact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        TypeID = c.Int(),
                        FirstName = c.String(maxLength: 100),
                        MiddleName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        FullName = c.String(maxLength: 300),
                        Phone1 = c.String(maxLength: 50),
                        Phone2 = c.String(maxLength: 50),
                        Phone3 = c.String(maxLength: 50),
                        Fax1 = c.String(maxLength: 50),
                        Fax2 = c.String(maxLength: 50),
                        Fax3 = c.String(maxLength: 50),
                        Email1 = c.String(maxLength: 1000),
                        Email2 = c.String(maxLength: 1000),
                        Email3 = c.String(maxLength: 1000),
                        Website1 = c.String(maxLength: 1000),
                        Website2 = c.String(maxLength: 1000),
                        Website3 = c.String(maxLength: 1000),
                        NotificationPhone = c.String(maxLength: 50),
                        NotificationEmail = c.String(maxLength: 1000),
                        NotificationViaEmail = c.Boolean(nullable: false),
                        NotificationViaSMSPhone = c.Boolean(nullable: false),
                        DateOfBirth = c.DateTime(precision: 7, storeType: "datetime2"),
                        Gender = c.Boolean(nullable: false),
                        AddressID = c.Int(),
                        ShippingAddressID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .ForeignKey("Geography.Address", t => t.ShippingAddressID)
                .ForeignKey("Contacts.ContactType", t => t.TypeID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.TypeID)
                .Index(t => t.AddressID)
                .Index(t => t.ShippingAddressID);

            CreateTable(
                "Geography.Address",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Phone = c.String(maxLength: 64, unicode: false),
                        Fax = c.String(maxLength: 64, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        Phone2 = c.String(maxLength: 32),
                        Phone3 = c.String(maxLength: 32),
                        Street1 = c.String(maxLength: 255),
                        Street2 = c.String(maxLength: 255),
                        Street3 = c.String(maxLength: 255),
                        City = c.String(maxLength: 100),
                        DistrictCustom = c.String(maxLength: 100),
                        RegionCustom = c.String(maxLength: 100),
                        CountryCustom = c.String(maxLength: 100),
                        PostalCode = c.String(maxLength: 50),
                        Latitude = c.Decimal(precision: 18, scale: 8),
                        Longitude = c.Decimal(precision: 18, scale: 8),
                        CountryID = c.Int(),
                        RegionID = c.Int(),
                        DistrictID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Country", t => t.CountryID)
                .ForeignKey("Geography.District", t => t.DistrictID)
                .ForeignKey("Geography.Region", t => t.RegionID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.Phone)
                .Index(t => t.Fax)
                .Index(t => t.Email)
                .Index(t => t.CountryID)
                .Index(t => t.RegionID)
                .Index(t => t.DistrictID);

            CreateTable(
                "Geography.Country",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Geography.Region",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Country", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.CountryID);

            CreateTable(
                "Geography.District",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        RegionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Region", t => t.RegionID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.RegionID);

            CreateTable(
                "Tax.TaxDistrict",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Rate = c.Decimal(nullable: false, precision: 7, scale: 6, storeType: "numeric"),
                        DistrictID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.District", t => t.DistrictID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DistrictID);

            CreateTable(
                "Geography.InterRegion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Notes = c.String(maxLength: 100),
                        KeyRegionID = c.Int(nullable: false),
                        RelationRegionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Region", t => t.KeyRegionID, cascadeDelete: true)
                .ForeignKey("Geography.Region", t => t.RelationRegionID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.KeyRegionID)
                .Index(t => t.RelationRegionID);

            CreateTable(
                "Tax.TaxRegion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Rate = c.Decimal(nullable: false, precision: 7, scale: 6, storeType: "numeric"),
                        RegionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Region", t => t.RegionID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.RegionID);

            CreateTable(
                "Tax.TaxCountry",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Rate = c.Decimal(nullable: false, precision: 7, scale: 6, storeType: "numeric"),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Country", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.CountryID);

            CreateTable(
                "Purchasing.PurchaseOrder",
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
                        BillingContactID = c.Int(nullable: false),
                        ShippingContactID = c.Int(nullable: false),
                        StatusID = c.Int(),
                        StateID = c.Int(),
                        TypeID = c.Int(),
                        UserID = c.Int(),
                        AccountID = c.Int(),
                        JsonAttributes = c.String(),
                        StoreID = c.Int(),
                        ReleaseDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EstimatedReceiptDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ActualReceiptDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        TrackingNumber = c.String(maxLength: 50, unicode: false),
                        InventoryLocationID = c.Int(),
                        FreeOnBoardID = c.Int(),
                        ShipCarrierID = c.Int(),
                        VendorTermID = c.Int(),
                        VendorID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.Contact", t => t.BillingContactID)
                .ForeignKey("Purchasing.FreeOnBoard", t => t.FreeOnBoardID)
                .ForeignKey("Inventory.InventoryLocation", t => t.InventoryLocationID)
                .ForeignKey("Shipping.ShipCarrier", t => t.ShipCarrierID)
                .ForeignKey("Contacts.Contact", t => t.ShippingContactID)
                .ForeignKey("Purchasing.PurchaseOrderState", t => t.StateID)
                .ForeignKey("Purchasing.PurchaseOrderStatus", t => t.StatusID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Purchasing.PurchaseOrderType", t => t.TypeID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .ForeignKey("Vendors.Vendor", t => t.VendorID)
                .ForeignKey("Vendors.VendorTerm", t => t.VendorTermID)
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
                .Index(t => t.InventoryLocationID)
                .Index(t => t.FreeOnBoardID)
                .Index(t => t.ShipCarrierID)
                .Index(t => t.VendorTermID)
                .Index(t => t.VendorID);

            CreateTable(
                "Purchasing.SalesOrderPurchaseOrder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SalesOrderID = c.Int(nullable: false),
                        PurchaseOrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Purchasing.PurchaseOrder", t => t.PurchaseOrderID, cascadeDelete: true)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesOrderID)
                .Index(t => t.PurchaseOrderID);

            CreateTable(
                "Ordering.SalesOrder",
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
                        PaymentTransactionID = c.String(maxLength: 256),
                        TaxTransactionID = c.String(maxLength: 256),
                        OrderApprovedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        OrderCommitmentDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RequiredShipDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RequestedShipDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ActualShipDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CustomerPriorityID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.Contact", t => t.BillingContactID)
                .ForeignKey("Ordering.SalesOrder", t => t.ParentID)
                .ForeignKey("Contacts.CustomerPriority", t => t.CustomerPriorityID)
                .ForeignKey("Contacts.Contact", t => t.ShippingContactID)
                .ForeignKey("Ordering.SalesOrderState", t => t.StateID)
                .ForeignKey("Ordering.SalesOrderStatus", t => t.StatusID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Ordering.SalesOrderType", t => t.TypeID)
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
                "Invoicing.SalesOrderSalesInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SalesOrderID = c.Int(nullable: false),
                        SalesInvoiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Invoicing.SalesInvoice", t => t.SalesInvoiceID, cascadeDelete: true)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesOrderID)
                .Index(t => t.SalesInvoiceID);

            CreateTable(
                "Invoicing.SalesInvoice",
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
                        AccountKey = c.String(),
                        ShippingSameAsBilling = c.Boolean(),
                        BillingContactID = c.Int(nullable: false),
                        ShippingContactID = c.Int(nullable: false),
                        StatusID = c.Int(),
                        StateID = c.Int(),
                        TypeID = c.Int(),
                        UserID = c.Int(),
                        AccountID = c.Int(),
                        JsonAttributes = c.String(),
                        StoreID = c.Int(),
                        BalanceDue = c.Decimal(precision: 18, scale: 4),
                        ShipOptionID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.Contact", t => t.BillingContactID)
                .ForeignKey("Shipping.ShipOption", t => t.ShipOptionID)
                .ForeignKey("Contacts.Contact", t => t.ShippingContactID)
                .ForeignKey("Invoicing.SalesInvoiceState", t => t.StateID)
                .ForeignKey("Invoicing.SalesInvoiceStatus", t => t.StatusID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Invoicing.SalesInvoiceType", t => t.TypeID)
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
                "Invoicing.SalesInvoiceAttribute",
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
                .ForeignKey("Invoicing.SalesInvoice", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Attributes.GeneralAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        IsFilter = c.Boolean(nullable: false),
                        IsPredefined = c.Boolean(nullable: false),
                        IsMarkup = c.Boolean(),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.AttributeType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID);

            CreateTable(
                "Attributes.AttributeValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        ParentID = c.Int(),
                        Value = c.String(nullable: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.GeneralAttribute", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("Attributes.AttributeValue", t => t.ParentID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ParentID)
                .Index(t => t.AttributeID);

            CreateTable(
                "Attributes.AttributeType",
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
                "Discounts.SalesInvoiceDiscounts",
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
                .ForeignKey("Invoicing.SalesInvoice", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.Discount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 4),
                        RoundingOperation = c.Int(nullable: false),
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsageLimit = c.Int(nullable: false),
                        UsageLimitByUser = c.Boolean(nullable: false),
                        Priority = c.Byte(nullable: false),
                        CanCombine = c.Boolean(nullable: false),
                        PurchaseMinimum = c.Int(nullable: false),
                        PurchaseLimit = c.Int(nullable: false),
                        Enable = c.Boolean(nullable: false),
                        DiscountTypeID = c.Int(nullable: false),
                        ValueType = c.Int(nullable: false),
                        RoundingType = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Discounts.DiscountCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        DiscountID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.CategoryID);

            CreateTable(
                "Categories.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        ParentID = c.Int(),
                        JsonAttributes = c.String(),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        TypeID = c.Int(nullable: false),
                        RequiresRoles = c.String(),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        IsVisible = c.Boolean(nullable: false),
                        IncludeInMenu = c.Boolean(nullable: false),
                        HeaderContent = c.String(),
                        SidebarContent = c.String(),
                        FooterContent = c.String(),
                        HandlingCharge = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.ParentID)
                .ForeignKey("Categories.CategoryType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.ParentID)
                .Index(t => t.TypeID)
                .Index(t => t.DisplayName);

            CreateTable(
                "Categories.CategoryAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.AttributeValue", t => t.AttributeValueID, cascadeDelete: true)
                .ForeignKey("Categories.Category", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeValueID)
                .Index(t => t.MasterID);

            CreateTable(
                "Categories.CategoryFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        CategoryID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.CategoryID)
                .Index(t => t.FileID);

            CreateTable(
                "Media.File",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        FileName = c.String(nullable: false),
                        IsStoredInDB = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active);

            CreateTable(
                "Media.Audio",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        FormatID = c.Int(nullable: false),
                        FullFileID = c.Int(nullable: false),
                        ClipFileID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.ClipFileID)
                .ForeignKey("Media.File", t => t.FullFileID, cascadeDelete: true)
                .ForeignKey("Media.Library", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.FullFileID)
                .Index(t => t.ClipFileID);

            CreateTable(
                "Media.Library",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoName = c.String(),
                        Caption = c.String(),
                        SortOrder = c.Int(),
                        Author = c.String(maxLength: 100),
                        MediaDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Copyright = c.String(maxLength: 100),
                        Location = c.String(maxLength: 100),
                        TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.LibraryType", t => t.TypeID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID);

            CreateTable(
                "Media.Document",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        FormatID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .ForeignKey("Media.Library", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.FileID);

            CreateTable(
                "Media.Image",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        FormatID = c.Int(nullable: false),
                        FullHeight = c.Int(),
                        FullWidth = c.Int(),
                        ThumbHeight = c.Int(),
                        ThumbWidth = c.Int(),
                        FullFileID = c.Int(nullable: false),
                        ThumbFileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FullFileID, cascadeDelete: true)
                .ForeignKey("Media.Library", t => t.ID)
                .ForeignKey("Media.File", t => t.ThumbFileID)
                .Index(t => t.ID)
                .Index(t => t.FullFileID)
                .Index(t => t.ThumbFileID);

            CreateTable(
                "Media.LibraryType",
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
                "Media.Video",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        FormatID = c.Int(nullable: false),
                        FullHeight = c.Int(),
                        FullWidth = c.Int(),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .ForeignKey("Media.Library", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.FileID);

            CreateTable(
                "Media.FileData",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Bytes = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.ID)
                .Index(t => t.ID);

            CreateTable(
                "Categories.CategoryImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        LibraryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("Media.Library", t => t.LibraryID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.CategoryID)
                .Index(t => t.LibraryID);

            CreateTable(
                "Products.ProductCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        JsonAttributes = c.String(),
                        SortOrder = c.Int(),
                        CategoryID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.CategoryID)
                .Index(t => t.ProductID);

            CreateTable(
                "Products.ProductCategoryAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.AttributeValue", t => t.AttributeValueID, cascadeDelete: true)
                .ForeignKey("Products.ProductCategory", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeValueID)
                .Index(t => t.MasterID);

            CreateTable(
                "Products.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        JsonAttributes = c.String(),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        RequiresRoles = c.String(),
                        Weight = c.Decimal(precision: 18, scale: 4),
                        WeightUnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        Width = c.Decimal(precision: 18, scale: 4),
                        WidthUnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        Depth = c.Decimal(precision: 18, scale: 4),
                        DepthUnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        Height = c.Decimal(precision: 18, scale: 4),
                        HeightUnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        ManufacturerPartNumber = c.String(maxLength: 255),
                        ShortDescription = c.String(unicode: false),
                        BrandName = c.String(maxLength: 255, unicode: false),
                        PriceBase = c.Decimal(precision: 18, scale: 4, storeType: "numeric"),
                        PriceMsrp = c.Decimal(precision: 18, scale: 4),
                        PriceReduction = c.Decimal(precision: 18, scale: 4),
                        PriceSale = c.Decimal(precision: 18, scale: 4),
                        HandlingCharge = c.Decimal(precision: 18, scale: 4),
                        FlatShippingCharge = c.Decimal(precision: 18, scale: 4),
                        IsVisible = c.Boolean(nullable: false),
                        IsQuotable = c.Boolean(),
                        IsTaxable = c.Boolean(),
                        TaxCode = c.String(),
                        IsSale = c.Boolean(),
                        IsFreeShipping = c.Boolean(),
                        AvailableStartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AvailableEndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        StockQuantity = c.Decimal(precision: 18, scale: 4),
                        StockQuantityAllocated = c.Decimal(precision: 18, scale: 4),
                        IsUnlimitedStock = c.Boolean(nullable: false),
                        AllowBackOrder = c.Boolean(),
                        IsDiscontinued = c.Boolean(),
                        UnitOfMeasure = c.String(),
                        MinimumPurchaseQuantity = c.Decimal(precision: 18, scale: 4),
                        MinimumPurchaseQuantityIfPastPurchased = c.Decimal(precision: 18, scale: 4),
                        MaximumPurchaseQuantity = c.Decimal(precision: 18, scale: 4),
                        MaximumPurchaseQuantityIfPastPurchased = c.Decimal(precision: 18, scale: 4),
                        QuantityPerMasterPack = c.Decimal(precision: 18, scale: 4),
                        QuantityMasterPackPerPallet = c.Decimal(precision: 18, scale: 4),
                        QuantityPerPallet = c.Decimal(precision: 18, scale: 4),
                        KitParentKeyForQuantity = c.String(),
                        KitQuantityOfParent = c.Decimal(precision: 18, scale: 4),
                        KitCapacity = c.String(),
                        KitBaseQuantityPriceMultiplier = c.Decimal(precision: 18, scale: 5),
                        SortOrder = c.Int(),
                        ProductHash = c.Long(),
                        PackageID = c.Int(),
                        MasterPackID = c.Int(),
                        PalletID = c.Int(),
                        TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shipping.Package", t => t.MasterPackID)
                .ForeignKey("Shipping.Package", t => t.PackageID)
                .ForeignKey("Shipping.Package", t => t.PalletID)
                .ForeignKey("Products.ProductType", t => t.TypeID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.ProductHash)
                .Index(t => t.PackageID)
                .Index(t => t.MasterPackID)
                .Index(t => t.PalletID)
                .Index(t => t.TypeID);

            CreateTable(
                "Products.ProductAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.AttributeValue", t => t.AttributeValueID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeValueID)
                .Index(t => t.MasterID);

            CreateTable(
                "Shopping.CartItem",
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
                        UnitOfMeasure = c.String(maxLength: 1000, unicode: false),
                        ProductID = c.Int(),
                        UserID = c.Int(),
                        MasterID = c.Int(nullable: false),
                        StatusID = c.Int(),
                        VendorProductID = c.Int(),
                        StoreProductID = c.Int(),
                        JsonAttributes = c.String(),
                        UnitSoldPriceModifier = c.Decimal(precision: 18, scale: 4),
                        UnitSoldPriceModifierMode = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shopping.Cart", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Shopping.CartItemStatus", t => t.StatusID)
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
                "Shopping.CartItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        Value = c.String(maxLength: 2500, unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.GeneralAttribute", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("Shopping.CartItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.CartItemDiscounts",
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
                .ForeignKey("Shopping.CartItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Shopping.Cart",
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
                        SessionID = c.Guid(),
                        RequestedShipDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SubtotalShippingModifier = c.Decimal(precision: 18, scale: 4),
                        SubtotalShippingModifierMode = c.Int(),
                        SubtotalTaxesModifier = c.Decimal(precision: 18, scale: 4),
                        SubtotalTaxesModifierMode = c.Int(),
                        SubtotalFeesModifier = c.Decimal(precision: 18, scale: 4),
                        SubtotalFeesModifierMode = c.Int(),
                        SubtotalHandlingModifier = c.Decimal(precision: 18, scale: 4),
                        SubtotalHandlingModifierMode = c.Int(),
                        SubtotalDiscountsModifier = c.Decimal(precision: 18, scale: 4),
                        SubtotalDiscountsModifierMode = c.Int(),
                        ShippingAddressID = c.Int(),
                        ShipOptionID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.Contact", t => t.BillingContactID)
                .ForeignKey("Shipping.ShipOption", t => t.ShipOptionID)
                .ForeignKey("Geography.Address", t => t.ShippingAddressID)
                .ForeignKey("Contacts.Contact", t => t.ShippingContactID)
                .ForeignKey("Shopping.CartState", t => t.StateID)
                .ForeignKey("Shopping.CartStatus", t => t.StatusID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Shopping.CartType", t => t.TypeID)
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
                .Index(t => t.ShippingAddressID)
                .Index(t => t.ShipOptionID);

            CreateTable(
                "Shopping.CartAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        Value = c.String(maxLength: 2500, unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.GeneralAttribute", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("Shopping.Cart", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.CartDiscounts",
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
                .ForeignKey("Shopping.Cart", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "System.Note",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Note = c.String(nullable: false, maxLength: 8000, unicode: false),
                        TypeID = c.Int(nullable: false),
                        CreatedByUserID = c.Int(),
                        UpdatedByUserID = c.Int(),
                        PurchaseOrderID = c.Int(),
                        SalesOrderID = c.Int(),
                        AccountID = c.Int(),
                        UserID = c.Int(),
                        SalesInvoiceID = c.Int(),
                        SalesQuoteID = c.Int(),
                        CartID = c.Int(),
                        VendorID = c.Int(),
                        ManufacturerID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Shopping.Cart", t => t.CartID)
                .ForeignKey("Contacts.User", t => t.CreatedByUserID)
                .ForeignKey("Manufacturers.Manufacturer", t => t.ManufacturerID)
                .ForeignKey("Purchasing.PurchaseOrder", t => t.PurchaseOrderID)
                .ForeignKey("Invoicing.SalesInvoice", t => t.SalesInvoiceID)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID)
                .ForeignKey("Quoting.SalesQuote", t => t.SalesQuoteID)
                .ForeignKey("System.NoteType", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UpdatedByUserID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .ForeignKey("Vendors.Vendor", t => t.VendorID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.TypeID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.UpdatedByUserID)
                .Index(t => t.PurchaseOrderID)
                .Index(t => t.SalesOrderID)
                .Index(t => t.AccountID)
                .Index(t => t.UserID)
                .Index(t => t.SalesInvoiceID)
                .Index(t => t.SalesQuoteID)
                .Index(t => t.CartID)
                .Index(t => t.VendorID)
                .Index(t => t.ManufacturerID);

            CreateTable(
                "Contacts.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        JsonAttributes = c.String(),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        PasswordHash = c.String(maxLength: 100),
                        SecurityStamp = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 25, unicode: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        TaxNumber = c.String(maxLength: 50, unicode: false),
                        DisplayName = c.String(maxLength: 128),
                        IsEmailSubscriber = c.Boolean(nullable: false),
                        IsCatalogSubscriber = c.Boolean(nullable: false),
                        PercentDiscount = c.Int(),
                        IsSuperAdmin = c.Boolean(nullable: false),
                        SalesRepContactsUserID = c.Int(),
                        AccountID = c.Int(),
                        PreferredStoreID = c.Int(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.PreferredStoreID)
                .ForeignKey("Contacts.UserStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Contacts.UserType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.ContactID)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.AccountID)
                .Index(t => t.PreferredStoreID);

            CreateTable(
                "Contacts.UserAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.AttributeValue", t => t.AttributeValueID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeValueID)
                .Index(t => t.MasterID);

            CreateTable(
                "Contacts.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Contacts.User", t => t.UserId)
                .Index(t => t.Id)
                .Index(t => t.UserId);

            CreateTable(
                "Contacts.Favorite",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SessionID = c.Guid(),
                        ProductID = c.Int(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ProductID)
                .Index(t => t.UserID);

            CreateTable(
                "Contacts.UserLogin",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128, unicode: false),
                        ProviderKey = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("Contacts.User", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "Messaging.MessageAttachment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        MessageID = c.Int(nullable: false),
                        LibraryID = c.Int(nullable: false),
                        CreatedByUserID = c.Int(nullable: false),
                        UpdatedByUserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.CreatedByUserID)
                .ForeignKey("Media.Library", t => t.LibraryID, cascadeDelete: true)
                .ForeignKey("Messaging.Message", t => t.MessageID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UpdatedByUserID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.MessageID)
                .Index(t => t.LibraryID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.UpdatedByUserID);

            CreateTable(
                "Messaging.Message",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(),
                        Subject = c.String(maxLength: 512),
                        Body = c.String(),
                        Context = c.String(maxLength: 256),
                        IsReplyAllAllowed = c.Boolean(nullable: false),
                        ConversationID = c.Int(),
                        SentByUserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Messaging.Conversation", t => t.ConversationID)
                .ForeignKey("Contacts.User", t => t.SentByUserID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.ConversationID)
                .Index(t => t.SentByUserID);

            CreateTable(
                "Messaging.Conversation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID);

            CreateTable(
                "Stores.Store",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        TypeID = c.Int(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        JsonAttributes = c.String(),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        Slogan = c.String(maxLength: 1024),
                        MissionStatement = c.String(maxLength: 1024),
                        About = c.String(),
                        Overview = c.String(),
                        ExternalUrl = c.String(maxLength: 512, unicode: false),
                        OperatingHoursTimeZoneId = c.String(maxLength: 55),
                        OperatingHoursMondayStart = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursMondayEnd = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursTuesdayStart = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursTuesdayEnd = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursWednesdayStart = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursWednesdayEnd = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursThursdayStart = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursThursdayEnd = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursFridayStart = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursFridayEnd = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursSaturdayStart = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursSaturdayEnd = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursSundayStart = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursSundayEnd = c.Decimal(precision: 18, scale: 2),
                        OperatingHoursClosedStatement = c.String(maxLength: 256),
                        ContactID = c.Int(nullable: false),
                        LogoImageLibraryID = c.Int(),
                        SellerImageLibraryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ContactID)
                .ForeignKey("Media.Library", t => t.LogoImageLibraryID)
                .ForeignKey("Media.Library", t => t.SellerImageLibraryID)
                .ForeignKey("Stores.StoreType", t => t.TypeID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.ContactID)
                .Index(t => t.LogoImageLibraryID)
                .Index(t => t.SellerImageLibraryID);

            CreateTable(
                "Stores.BrandStore",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        BrandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Brand", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.BrandID);

            CreateTable(
                "Stores.Brand",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Stores.BrandSiteDomain",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        BrandID = c.Int(nullable: false),
                        SiteDomainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Brand", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("Stores.SiteDomain", t => t.SiteDomainID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.BrandID)
                .Index(t => t.SiteDomainID);

            CreateTable(
                "Stores.SiteDomain",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        HeaderContent = c.String(unicode: false),
                        FooterContent = c.String(unicode: false),
                        SideBarContent = c.String(unicode: false),
                        CatalogContent = c.String(unicode: false),
                        Url = c.String(maxLength: 512, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Stores.SiteDomainSocialProvider",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Script = c.String(),
                        UrlValues = c.String(),
                        SiteDomainID = c.Int(nullable: false),
                        SocialProviderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.SiteDomain", t => t.SiteDomainID, cascadeDelete: true)
                .ForeignKey("Stores.SocialProvider", t => t.SocialProviderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SiteDomainID)
                .Index(t => t.SocialProviderID);

            CreateTable(
                "Stores.SocialProvider",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Url = c.String(),
                        UrlFormat = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Stores.StoreSiteDomain",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        SiteDomainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.SiteDomain", t => t.SiteDomainID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.SiteDomainID);

            CreateTable(
                "Reviews.Review",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        TypeID = c.Int(),
                        SortOrder = c.Int(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                        Approved = c.Boolean(nullable: false),
                        ApprovedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Title = c.String(maxLength: 255, unicode: false),
                        Location = c.String(maxLength: 255, unicode: false),
                        SubmittedByUserID = c.Int(nullable: false),
                        ApprovedByUserID = c.Int(),
                        CategoryID = c.Int(),
                        ManufacturerID = c.Int(),
                        ProductID = c.Int(),
                        StoreID = c.Int(),
                        UserID = c.Int(),
                        VendorID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.ApprovedByUserID)
                .ForeignKey("Categories.Category", t => t.CategoryID)
                .ForeignKey("Manufacturers.Manufacturer", t => t.ManufacturerID)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Contacts.User", t => t.SubmittedByUserID, cascadeDelete: true)
                .ForeignKey("Reviews.ReviewType", t => t.TypeID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .ForeignKey("Vendors.Vendor", t => t.VendorID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID)
                .Index(t => t.SubmittedByUserID)
                .Index(t => t.ApprovedByUserID)
                .Index(t => t.CategoryID)
                .Index(t => t.ManufacturerID)
                .Index(t => t.ProductID)
                .Index(t => t.StoreID)
                .Index(t => t.UserID)
                .Index(t => t.VendorID);

            CreateTable(
                "Manufacturers.Manufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Phone = c.String(maxLength: 64, unicode: false),
                        Fax = c.String(maxLength: 64, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        AddressID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.Phone)
                .Index(t => t.Fax)
                .Index(t => t.Email)
                .Index(t => t.AddressID);

            CreateTable(
                "Manufacturers.ManufacturerProduct",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        ProductID = c.Int(nullable: false),
                        ManufacturerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Manufacturers.Manufacturer", t => t.ManufacturerID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ProductID)
                .Index(t => t.ManufacturerID);

            CreateTable(
                "Stores.StoreManufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        ManufacturerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Manufacturers.Manufacturer", t => t.ManufacturerID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.ManufacturerID);

            CreateTable(
                "Vendors.VendorManufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        ManufacturerID = c.Int(nullable: false),
                        VendorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Manufacturers.Manufacturer", t => t.ManufacturerID, cascadeDelete: true)
                .ForeignKey("Vendors.Vendor", t => t.VendorID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ManufacturerID)
                .Index(t => t.VendorID);

            CreateTable(
                "Vendors.Vendor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Phone = c.String(maxLength: 64, unicode: false),
                        Fax = c.String(maxLength: 64, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        ContactID = c.Int(),
                        Notes = c.String(unicode: false),
                        AccountNumber = c.String(maxLength: 100),
                        Terms = c.String(maxLength: 100),
                        TermNotes = c.String(),
                        SendMethod = c.String(maxLength: 100),
                        EmailSubject = c.String(maxLength: 300),
                        ShipTo = c.String(maxLength: 100),
                        ShipViaNotes = c.String(),
                        SignBy = c.String(maxLength: 100),
                        DefaultDiscount = c.Decimal(precision: 18, scale: 4),
                        AllowDropShip = c.Boolean(nullable: false),
                        RecommendedPurchaseOrderDollarAmount = c.Decimal(precision: 18, scale: 2),
                        VendorHash = c.Long(),
                        AddressID = c.Int(),
                        TermID = c.Int(),
                        ContactMethodID = c.Int(),
                        ShipViaID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .ForeignKey("Contacts.Contact", t => t.ContactID)
                .ForeignKey("Contacts.ContactMethod", t => t.ContactMethodID)
                .ForeignKey("Vendors.ShipVia", t => t.ShipViaID)
                .ForeignKey("Vendors.Term", t => t.TermID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.Phone)
                .Index(t => t.Fax)
                .Index(t => t.Email)
                .Index(t => t.ContactID)
                .Index(t => t.VendorHash)
                .Index(t => t.AddressID)
                .Index(t => t.TermID)
                .Index(t => t.ContactMethodID)
                .Index(t => t.ShipViaID);

            CreateTable(
                "Contacts.ContactMethod",
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
                "Shipping.Shipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Reference1 = c.String(maxLength: 100, unicode: false),
                        Reference2 = c.String(maxLength: 100, unicode: false),
                        Reference3 = c.String(maxLength: 100, unicode: false),
                        TrackingNumber = c.String(maxLength: 100, unicode: false),
                        Destination = c.String(maxLength: 255, unicode: false),
                        ShipDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EstimatedDeliveryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateDelivered = c.DateTime(precision: 7, storeType: "datetime2"),
                        NegotiatedRate = c.Decimal(precision: 18, scale: 4),
                        PublishedRate = c.Decimal(precision: 18, scale: 4),
                        OriginContactID = c.Int(nullable: false),
                        DestinationContactID = c.Int(nullable: false),
                        InventoryLocationSectionID = c.Int(),
                        ShipCarrierID = c.Int(),
                        ShipCarrierMethodID = c.Int(),
                        StatusID = c.Int(),
                        TypeID = c.Int(),
                        VendorID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.DestinationContactID)
                .ForeignKey("Inventory.InventoryLocationSection", t => t.InventoryLocationSectionID)
                .ForeignKey("Contacts.Contact", t => t.OriginContactID)
                .ForeignKey("Shipping.ShipCarrier", t => t.ShipCarrierID)
                .ForeignKey("Shipping.ShipCarrierMethod", t => t.ShipCarrierMethodID)
                .ForeignKey("Shipping.ShipmentStatus", t => t.StatusID)
                .ForeignKey("Shipping.ShipmentType", t => t.TypeID)
                .ForeignKey("Vendors.Vendor", t => t.VendorID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.OriginContactID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.InventoryLocationSectionID)
                .Index(t => t.ShipCarrierID)
                .Index(t => t.ShipCarrierMethodID)
                .Index(t => t.StatusID)
                .Index(t => t.TypeID)
                .Index(t => t.VendorID);

            CreateTable(
                "Inventory.InventoryLocationSection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        InventoryLocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Inventory.InventoryLocation", t => t.InventoryLocationID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.InventoryLocationID);

            CreateTable(
                "Inventory.InventoryLocation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Phone = c.String(maxLength: 64, unicode: false),
                        Fax = c.String(maxLength: 64, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        AddressID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.Phone)
                .Index(t => t.Fax)
                .Index(t => t.Email)
                .Index(t => t.AddressID);

            CreateTable(
                "Stores.StoreInventoryLocation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        InventoryLocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Inventory.InventoryLocation", t => t.InventoryLocationID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.InventoryLocationID);

            CreateTable(
                "Products.ProductInventoryLocationSection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Quantity = c.Decimal(precision: 18, scale: 4),
                        QuantityBroken = c.Decimal(precision: 18, scale: 4),
                        QuantityAllocated = c.Decimal(precision: 18, scale: 4),
                        InventoryLocationSectionID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Inventory.InventoryLocationSection", t => t.InventoryLocationSectionID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.InventoryLocationSectionID)
                .Index(t => t.ProductID);

            CreateTable(
                "Shipping.SalesOrderItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SalesOrderItemID = c.Int(nullable: false),
                        ShipmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Ordering.SalesOrderItem", t => t.SalesOrderItemID, cascadeDelete: true)
                .ForeignKey("Shipping.Shipment", t => t.ShipmentID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesOrderItemID)
                .Index(t => t.ShipmentID);

            CreateTable(
                "Ordering.SalesOrderItem",
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
                        UnitOfMeasure = c.String(maxLength: 1000, unicode: false),
                        ProductID = c.Int(),
                        UserID = c.Int(),
                        MasterID = c.Int(nullable: false),
                        StatusID = c.Int(),
                        VendorProductID = c.Int(),
                        StoreProductID = c.Int(),
                        JsonAttributes = c.String(),
                        ShippingCarrierName = c.String(maxLength: 255, unicode: false),
                        ShippingCarrierMethodName = c.String(maxLength: 255, unicode: false),
                        TrackingNumber = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Ordering.SalesOrder", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Ordering.SalesOrderItemStatus", t => t.StatusID)
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
                "Ordering.SalesOrderItemAttribute",
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
                .ForeignKey("Ordering.SalesOrderItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.SalesOrderItemDiscounts",
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
                .ForeignKey("Ordering.SalesOrderItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Ordering.SalesOrderItemStatus",
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
                "Stores.StoreProduct",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        IsVisibleInStore = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.ProductID);

            CreateTable(
                "Vendors.VendorProduct",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Bin = c.String(maxLength: 1000, unicode: false),
                        MinimumInventory = c.Int(),
                        MaximumInventory = c.Int(),
                        CostMultiplier = c.Decimal(precision: 18, scale: 0),
                        ListedPrice = c.Decimal(precision: 18, scale: 4),
                        ActualCost = c.Decimal(precision: 18, scale: 4),
                        InventoryCount = c.Int(),
                        ProductID = c.Int(nullable: false),
                        VendorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("Vendors.Vendor", t => t.VendorID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ProductID)
                .Index(t => t.VendorID);

            CreateTable(
                "Shipping.ShipCarrier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Phone = c.String(maxLength: 64, unicode: false),
                        Fax = c.String(maxLength: 64, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        PointOfContact = c.String(maxLength: 1000, unicode: false),
                        IsInbound = c.Boolean(nullable: false),
                        IsOutbound = c.Boolean(nullable: false),
                        Username = c.String(maxLength: 75),
                        EncryptedPassword = c.String(maxLength: 1024),
                        Authentication = c.String(maxLength: 128),
                        AccountNumber = c.String(maxLength: 128),
                        SalesRep = c.String(maxLength: 128),
                        PickupTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        AddressID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.Phone)
                .Index(t => t.Fax)
                .Index(t => t.Email)
                .Index(t => t.AddressID);

            CreateTable(
                "Shipping.CarrierInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        AccountNumber = c.String(maxLength: 50, unicode: false),
                        InvoiceNumber = c.String(maxLength: 50, unicode: false),
                        InvoiceDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AmountDue = c.Decimal(precision: 18, scale: 2),
                        TrackingNumber = c.String(maxLength: 50, unicode: false),
                        PickupRecord = c.String(maxLength: 50, unicode: false),
                        ReferenceNo1 = c.String(maxLength: 50, unicode: false),
                        ReferenceNo2 = c.String(maxLength: 50, unicode: false),
                        ReferenceNo3 = c.String(maxLength: 50, unicode: false),
                        Weight = c.String(maxLength: 50, unicode: false),
                        Zone = c.String(maxLength: 50, unicode: false),
                        ServiceLevel = c.String(maxLength: 50, unicode: false),
                        PickupDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SenderName = c.String(maxLength: 50, unicode: false),
                        SenderCompanyName = c.String(maxLength: 50, unicode: false),
                        SenderStreet = c.String(maxLength: 50, unicode: false),
                        SenderCity = c.String(maxLength: 50, unicode: false),
                        SenderState = c.String(maxLength: 50, unicode: false),
                        SenderZipCode = c.String(maxLength: 50, unicode: false),
                        ReceiverName = c.String(maxLength: 50, unicode: false),
                        ReceiverCompanyName = c.String(maxLength: 50, unicode: false),
                        ReceiverStreet = c.String(maxLength: 50, unicode: false),
                        ReceiverCity = c.String(maxLength: 50, unicode: false),
                        ReceiverState = c.String(maxLength: 50, unicode: false),
                        ReceiverZipCode = c.String(maxLength: 50, unicode: false),
                        ReceiverCountry = c.String(maxLength: 50, unicode: false),
                        ThirdParty = c.String(maxLength: 50, unicode: false),
                        BilledCharge = c.Decimal(precision: 18, scale: 2),
                        IncentiveCredit = c.Decimal(precision: 18, scale: 2),
                        InvoiceSection = c.String(maxLength: 50, unicode: false),
                        InvoiceType = c.String(maxLength: 50, unicode: false),
                        InvoiceDueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Status = c.String(maxLength: 200, unicode: false),
                        ShipCarrierID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shipping.ShipCarrier", t => t.ShipCarrierID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ShipCarrierID);

            CreateTable(
                "Shipping.CarrierOrigin",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        DateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        PackageReferenceNumber = c.String(maxLength: 100, unicode: false),
                        ShipmentReferenceNumber = c.String(maxLength: 100, unicode: false),
                        ReferenceNumber = c.String(maxLength: 100, unicode: false),
                        ShipperNumber = c.String(maxLength: 100, unicode: false),
                        SubscriptionEventName = c.String(maxLength: 100, unicode: false),
                        SubscriptionEventNumber = c.String(maxLength: 100, unicode: false),
                        SubscriptionFileName = c.String(maxLength: 100, unicode: false),
                        TrackingNumber = c.String(maxLength: 100, unicode: false),
                        TrackingStatus = c.String(maxLength: 100, unicode: false),
                        TrackingShippingMethod = c.String(maxLength: 100, unicode: false),
                        TrackingShippingDate = c.String(maxLength: 50, unicode: false),
                        TrackingLastScan = c.String(maxLength: 100, unicode: false),
                        TrackingDestination = c.String(maxLength: 100, unicode: false),
                        TrackingEstDeliveryDate = c.String(maxLength: 50, unicode: false),
                        TrackingLastUpdate = c.DateTime(precision: 7, storeType: "datetime2"),
                        TrackingEventName = c.String(maxLength: 50, unicode: false),
                        TrackingOriginalEstimatedDeliveryDate = c.String(maxLength: 50, unicode: false),
                        TrackingManualDelivered = c.Boolean(nullable: false),
                        ShipCarrierID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shipping.ShipCarrier", t => t.ShipCarrierID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ShipCarrierID);

            CreateTable(
                "Shipping.ShipCarrierMethod",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        ShipCarrierID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shipping.ShipCarrier", t => t.ShipCarrierID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.ShipCarrierID);

            CreateTable(
                "Vendors.ShipVia",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        AccountNumber = c.String(maxLength: 200, unicode: false),
                        Notes = c.String(maxLength: 500, unicode: false),
                        ShipCarrierID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shipping.ShipCarrier", t => t.ShipCarrierID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ShipCarrierID);

            CreateTable(
                "Shipping.ShipmentEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Note = c.String(),
                        EventDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AddressID = c.Int(nullable: false),
                        ShipmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID, cascadeDelete: true)
                .ForeignKey("Shipping.Shipment", t => t.ShipmentID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AddressID)
                .Index(t => t.ShipmentID);

            CreateTable(
                "Shipping.ShipmentStatus",
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
                "Shipping.ShipmentType",
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
                "Stores.StoreVendor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        VendorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .ForeignKey("Vendors.Vendor", t => t.VendorID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.VendorID);

            CreateTable(
                "Vendors.Term",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        AccountNumber = c.String(maxLength: 200, unicode: false),
                        Notes = c.String(maxLength: 500, unicode: false),
                        VendorTermID = c.Int(nullable: false),
                        CreatedByUserID = c.Int(nullable: false),
                        UpdatedByUserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.CreatedByUserID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UpdatedByUserID)
                .ForeignKey("Vendors.VendorTerm", t => t.VendorTermID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.VendorTermID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.UpdatedByUserID);

            CreateTable(
                "Vendors.VendorTerm",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Reviews.ReviewType",
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
                "Stores.StoreAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        HasAccessToStore = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        PricePointID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("Pricing.PricePoint", t => t.PricePointID)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.AccountID)
                .Index(t => t.PricePointID);

            CreateTable(
                "Pricing.PricePoint",
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
                "Stores.StoreCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.CategoryID);

            CreateTable(
                "Stores.StoreCategoryType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        CategoryTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.CategoryType", t => t.CategoryTypeID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.CategoryTypeID);

            CreateTable(
                "Categories.CategoryType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        ParentID = c.Int(),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        Value = c.String(maxLength: 2500),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.CategoryType", t => t.ParentID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.ParentID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Stores.StoreContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        StoreID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.StoreID)
                .Index(t => t.ContactID);

            CreateTable(
                "Stores.StoreImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        LibraryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.Library", t => t.LibraryID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.LibraryID);

            CreateTable(
                "Stores.StoreSubscription",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        SubscriptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .ForeignKey("Payments.Subscription", t => t.SubscriptionID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.SubscriptionID);

            CreateTable(
                "Payments.Subscription",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        MemberSince = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StartsOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndsOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        BillingPeriodsTotal = c.Int(nullable: false),
                        BillingPeriodsPaid = c.Int(nullable: false),
                        LastPaidDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Memo = c.String(),
                        AccountKey = c.String(),
                        UserKey = c.String(),
                        AutoRenew = c.Boolean(nullable: false),
                        CanUpgrade = c.Boolean(nullable: false),
                        CreditUponUpgrade = c.Decimal(precision: 18, scale: 2),
                        FilterKey1 = c.String(),
                        FilterKey2 = c.String(),
                        FilterKey3 = c.String(),
                        RepeatTypeID = c.Int(nullable: false),
                        PaymentID = c.Int(),
                        SubscriptionStatusID = c.Int(),
                        SubscriptionTypeID = c.Int(),
                        UserID = c.Int(),
                        AccountID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Payments.Payment", t => t.PaymentID)
                .ForeignKey("Payments.RepeatType", t => t.RepeatTypeID, cascadeDelete: true)
                .ForeignKey("Payments.SubscriptionStatus", t => t.SubscriptionStatusID)
                .ForeignKey("Payments.SubscriptionType", t => t.SubscriptionTypeID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.RepeatTypeID)
                .Index(t => t.PaymentID)
                .Index(t => t.SubscriptionStatusID)
                .Index(t => t.SubscriptionTypeID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID);

            CreateTable(
                "Payments.Payment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        PaymentData = c.String(),
                        StatusDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Authorized = c.Boolean(),
                        AuthCode = c.String(maxLength: 100),
                        AuthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Received = c.Boolean(),
                        ReceivedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ReferenceNo = c.String(maxLength: 100),
                        Response = c.String(),
                        ExternalCustomerID = c.String(maxLength: 100),
                        ExternalPaymentID = c.String(maxLength: 100),
                        CardTypeID = c.Int(),
                        CardMask = c.String(maxLength: 50),
                        CVV = c.String(maxLength: 50),
                        AVS = c.String(maxLength: 100),
                        Last4CardDigits = c.String(maxLength: 100),
                        ExpirationMonth = c.Int(),
                        ExpirationYear = c.Int(),
                        TransactionNumber = c.String(maxLength: 100),
                        BillingContactID = c.Int(),
                        PaymentMethodID = c.Int(),
                        StatusID = c.Int(),
                        TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.BillingContactID)
                .ForeignKey("Payments.PaymentMethod", t => t.PaymentMethodID)
                .ForeignKey("Payments.PaymentStatus", t => t.StatusID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Payments.PaymentType", t => t.TypeID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.BillingContactID)
                .Index(t => t.PaymentMethodID)
                .Index(t => t.StatusID)
                .Index(t => t.TypeID);

            CreateTable(
                "Payments.PaymentMethod",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Payments.PaymentStatus",
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
                "Payments.SubscriptionHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        PaymentDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PaymentSuccess = c.Boolean(nullable: false),
                        Memo = c.String(nullable: false),
                        BillingPeriodsPaid = c.Int(nullable: false),
                        PaymentID = c.Int(nullable: false),
                        SubscriptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Payments.Payment", t => t.PaymentID, cascadeDelete: true)
                .ForeignKey("Payments.Subscription", t => t.SubscriptionID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.PaymentID)
                .Index(t => t.SubscriptionID);

            CreateTable(
                "Payments.PaymentType",
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
                "Payments.RepeatType",
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
                "Payments.SubscriptionStatus",
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
                "Payments.SubscriptionType",
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
                        JsonAttributes = c.String(),
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
                "Products.ProductSubscriptionType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        SubscriptionTypeID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("Payments.SubscriptionType", t => t.SubscriptionTypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SubscriptionTypeID)
                .Index(t => t.ProductID);

            CreateTable(
                "Stores.StoreUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.UserID);

            CreateTable(
                "Stores.StoreUserType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        UserTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .ForeignKey("Contacts.UserType", t => t.UserTypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.UserTypeID);

            CreateTable(
                "Contacts.UserType",
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
                        IsAdministrator = c.Boolean(nullable: false),
                        IsCustomer = c.Boolean(nullable: false),
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
                "Stores.StoreType",
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
                "Messaging.MessageRecipient",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        HasSentAnEmail = c.Boolean(nullable: false),
                        EmailSentAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        MessageID = c.Int(nullable: false),
                        ToUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Messaging.Message", t => t.MessageID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.ToUserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.MessageID)
                .Index(t => t.ToUserID);

            CreateTable(
                "Messaging.EmailQueue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        AddressesTo = c.String(),
                        AddressesCc = c.String(),
                        AddressesBcc = c.String(),
                        AddressFrom = c.String(nullable: false, maxLength: 1000),
                        Subject = c.String(nullable: false, maxLength: 255),
                        Body = c.String(nullable: false),
                        IsHtml = c.Boolean(nullable: false),
                        Attempts = c.Int(nullable: false),
                        HasError = c.Boolean(nullable: false),
                        StatusID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                        EmailTemplateID = c.Int(),
                        MessageRecipientID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Messaging.EmailTemplate", t => t.EmailTemplateID)
                .ForeignKey("Messaging.MessageRecipient", t => t.MessageRecipientID)
                .ForeignKey("Messaging.EmailStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Messaging.EmailType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StatusID)
                .Index(t => t.TypeID)
                .Index(t => t.EmailTemplateID)
                .Index(t => t.MessageRecipientID);

            CreateTable(
                "Messaging.EmailTemplate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Subject = c.String(nullable: false, maxLength: 255),
                        Body = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Messaging.EmailStatus",
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
                "Messaging.EmailType",
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
                "Notifications.NotificationMessage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Message = c.String(unicode: false),
                        WorkflowInstance = c.String(unicode: false),
                        ActionID = c.Int(),
                        EntityID = c.Int(),
                        AccountID = c.Int(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AccountID)
                .Index(t => t.UserID);

            CreateTable(
                "Notifications.Notification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        MonitorInstance = c.String(unicode: false),
                        MessageTemplate = c.String(unicode: false),
                        LastMessage = c.Int(),
                        ScheduledDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        WorkflowInstance = c.String(unicode: false),
                        ActionCodeMask = c.Int(),
                        Filter = c.String(unicode: false),
                        DistributionList = c.String(unicode: false),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.UserID);

            CreateTable(
                "Contacts.RoleUser",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("Contacts.UserRole", t => t.RoleId)
                .ForeignKey("Contacts.User", t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);

            CreateTable(
                "Contacts.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "Contacts.RolePermission",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PermissionId })
                .ForeignKey("Contacts.Permission", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("Contacts.UserRole", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);

            CreateTable(
                "Contacts.Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "Quoting.SalesQuote",
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
                        BillingContactID = c.Int(nullable: false),
                        ShippingContactID = c.Int(nullable: false),
                        StatusID = c.Int(),
                        StateID = c.Int(),
                        TypeID = c.Int(),
                        UserID = c.Int(),
                        AccountID = c.Int(),
                        JsonAttributes = c.String(),
                        StoreID = c.Int(),
                        BalanceDue = c.Decimal(precision: 18, scale: 4),
                        ShipOptionID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Contacts.Contact", t => t.BillingContactID)
                .ForeignKey("Shipping.ShipOption", t => t.ShipOptionID)
                .ForeignKey("Contacts.Contact", t => t.ShippingContactID)
                .ForeignKey("Quoting.SalesQuoteState", t => t.StateID)
                .ForeignKey("Quoting.SalesQuoteStatus", t => t.StatusID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Quoting.SalesQuoteType", t => t.TypeID)
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
                "Quoting.SalesQuoteSalesOrder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SalesQuoteID = c.Int(nullable: false),
                        SalesOrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID, cascadeDelete: true)
                .ForeignKey("Quoting.SalesQuote", t => t.SalesQuoteID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesQuoteID)
                .Index(t => t.SalesOrderID);

            CreateTable(
                "Quoting.SalesQuoteAttribute",
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
                .ForeignKey("Quoting.SalesQuote", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.SalesQuoteDiscounts",
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
                .ForeignKey("Quoting.SalesQuote", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Quoting.SalesQuoteItem",
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
                .ForeignKey("Quoting.SalesQuote", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Quoting.SalesQuoteItemStatus", t => t.StatusID)
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
                "Quoting.SalesQuoteItemAttribute",
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
                .ForeignKey("Quoting.SalesQuoteItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.QuoteItemDiscounts",
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
                .ForeignKey("Quoting.SalesQuoteItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Quoting.SalesQuoteItemStatus",
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
                "Shipping.ShipOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        EstimatedDeliveryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Rate = c.Decimal(precision: 18, scale: 4),
                        ShipCarrierMethodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shipping.ShipCarrierMethod", t => t.ShipCarrierMethodID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.ShipCarrierMethodID);

            CreateTable(
                "Quoting.SalesQuoteState",
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
                "Quoting.SalesQuoteStatus",
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
                "Quoting.SalesQuoteType",
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
                "Contacts.UserStatus",
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
                "Payments.Wallet",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CreditCardNumber = c.String(maxLength: 100, unicode: false),
                        ExpirationMonth = c.Int(),
                        ExpirationYear = c.Int(),
                        Token = c.String(maxLength: 100, unicode: false),
                        CardType = c.String(maxLength: 100, unicode: false),
                        CardHolderName = c.String(maxLength: 256, unicode: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.UserID);

            CreateTable(
                "System.NoteType",
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
                        IsPublic = c.Boolean(nullable: false),
                        IsCustomer = c.Boolean(nullable: false),
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
                "Shopping.CartState",
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
                "Shopping.CartStatus",
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
                "Shopping.CartType",
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
                        StoreID = c.Int(),
                        CreatedByUserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.CreatedByUserID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.StoreID)
                .Index(t => t.CreatedByUserID);

            CreateTable(
                "Shopping.CartItemStatus",
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
                "Discounts.DiscountProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        DiscountID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.ProductID);

            CreateTable(
                "Shipping.Package",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Width = c.Decimal(nullable: false, precision: 18, scale: 4),
                        WidthUnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        Depth = c.Decimal(nullable: false, precision: 18, scale: 4),
                        DepthUnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 4),
                        HeightUnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 4),
                        WeightUnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        DimensionalWeight = c.Decimal(nullable: false, precision: 18, scale: 4),
                        DimensionalWeightUnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        IsCustom = c.Boolean(nullable: false),
                        TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shipping.PackageType", t => t.TypeID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID);

            CreateTable(
                "Shipping.PackageType",
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
                "Products.ProductAssociation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(precision: 18, scale: 4),
                        UnitOfMeasure = c.String(),
                        SortOrder = c.Int(),
                        TypeID = c.Int(),
                        PrimaryProductID = c.Int(nullable: false),
                        AssociatedProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.AssociatedProductID)
                .ForeignKey("Products.Product", t => t.PrimaryProductID)
                .ForeignKey("Products.ProductAssociationType", t => t.TypeID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.TypeID)
                .Index(t => t.PrimaryProductID)
                .Index(t => t.AssociatedProductID);

            CreateTable(
                "Products.ProductAssociationAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.AttributeValue", t => t.AttributeValueID, cascadeDelete: true)
                .ForeignKey("Products.ProductAssociation", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeValueID)
                .Index(t => t.MasterID);

            CreateTable(
                "Products.ProductAssociationType",
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
                "Products.ProductAttributeFilterValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        Value = c.String(nullable: false, maxLength: 1000, unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.GeneralAttribute", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Products.ProductFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        ProductID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.ProductID)
                .Index(t => t.FileID);

            CreateTable(
                "Products.ProductImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        ProductID = c.Int(nullable: false),
                        LibraryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Media.Library", t => t.LibraryID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ProductID)
                .Index(t => t.LibraryID);

            CreateTable(
                "Products.ProductPricePoint",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 4),
                        PercentDiscount = c.Decimal(precision: 18, scale: 4),
                        MinQuantity = c.Int(),
                        MaxQuantity = c.Int(),
                        UnitOfMeasure = c.String(maxLength: 10),
                        PricePointID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        PriceRoundingID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Pricing.PricePoint", t => t.PricePointID, cascadeDelete: true)
                .ForeignKey("Pricing.PriceRounding", t => t.PriceRoundingID)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.PricePointID)
                .Index(t => t.ProductID)
                .Index(t => t.PriceRoundingID);

            CreateTable(
                "Pricing.PriceRounding",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        PricePointKey = c.String(maxLength: 100, unicode: false),
                        ProductKey = c.String(maxLength: 100, unicode: false),
                        CurrencyKey = c.String(maxLength: 100, unicode: false),
                        UnitOfMeasure = c.String(maxLength: 100, unicode: false),
                        RoundHow = c.Int(nullable: false),
                        RoundTo = c.Int(nullable: false),
                        RoundingAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active);

            CreateTable(
                "Products.ProductType",
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
                        StoreID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.StoreID);

            CreateTable(
                "Discounts.DiscountCode",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Code = c.String(nullable: false, maxLength: 20),
                        DiscountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID);

            CreateTable(
                "Discounts.DiscountProductType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        DiscountID = c.Int(nullable: false),
                        ProductTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("Products.ProductType", t => t.ProductTypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.ProductTypeID);

            CreateTable(
                "Discounts.DiscountStores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        DiscountID = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Discounts.Discount", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.StoreID);

            CreateTable(
                "Payments.SalesInvoicePayment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SalesInvoiceID = c.Int(nullable: false),
                        PaymentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Payments.Payment", t => t.PaymentID, cascadeDelete: true)
                .ForeignKey("Invoicing.SalesInvoice", t => t.SalesInvoiceID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesInvoiceID)
                .Index(t => t.PaymentID);

            CreateTable(
                "Invoicing.SalesInvoiceItem",
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Invoicing.SalesInvoice", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Invoicing.SalesInvoiceItemStatus", t => t.StatusID)
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
                "Invoicing.SalesInvoiceItemAttribute",
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
                .ForeignKey("Invoicing.SalesInvoiceItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.SalesInvoiceItemDiscounts",
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
                .ForeignKey("Invoicing.SalesInvoiceItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Invoicing.SalesInvoiceItemStatus",
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
                "Invoicing.SalesInvoiceState",
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
                "Invoicing.SalesInvoiceStatus",
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
                "Invoicing.SalesInvoiceType",
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
                "Ordering.SalesOrderAttribute",
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
                .ForeignKey("Ordering.SalesOrder", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Contacts.CustomerPriority",
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
                "Discounts.SalesOrderDiscounts",
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
                .ForeignKey("Ordering.SalesOrder", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Payments.SalesOrderPayment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SalesOrderID = c.Int(nullable: false),
                        PaymentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Payments.Payment", t => t.PaymentID, cascadeDelete: true)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.SalesOrderID)
                .Index(t => t.PaymentID);

            CreateTable(
                "Ordering.SalesOrderState",
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
                "Ordering.SalesOrderStatus",
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
                "Ordering.SalesOrderType",
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
                "Purchasing.PurchaseOrderAttribute",
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
                .ForeignKey("Purchasing.PurchaseOrder", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.PurchaseOrderDiscounts",
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
                .ForeignKey("Purchasing.PurchaseOrder", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Purchasing.FreeOnBoard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Purchasing.PurchaseOrderItem",
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
                        StoreProductID = c.Int(),
                        JsonAttributes = c.String(),
                        EstimatedArrival = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateReceived = c.DateTime(precision: 7, storeType: "datetime2"),
                        VendorProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Purchasing.PurchaseOrder", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Purchasing.PurchaseOrderItemStatus", t => t.StatusID)
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
                .Index(t => t.StoreProductID)
                .Index(t => t.VendorProductID);

            CreateTable(
                "Purchasing.PurchaseOrderItemAttribute",
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
                .ForeignKey("Purchasing.PurchaseOrderItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeID)
                .Index(t => t.MasterID);

            CreateTable(
                "Discounts.PurchaseOrderItemDiscounts",
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
                .ForeignKey("Purchasing.PurchaseOrderItem", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.DiscountID)
                .Index(t => t.MasterID);

            CreateTable(
                "Purchasing.PurchaseOrderItemStatus",
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
                "Purchasing.PurchaseOrderState",
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
                "Purchasing.PurchaseOrderStatus",
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
                "Purchasing.PurchaseOrderType",
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
                "Contacts.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active);

            CreateTable(
                "Contacts.Individual",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active);

            CreateTable(
                "Contacts.ContactType",
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
                "Accounts.AccountPricePoint",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        AccountID = c.Int(nullable: false),
                        PricePointID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("Pricing.PricePoint", t => t.PricePointID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AccountID)
                .Index(t => t.PricePointID);

            CreateTable(
                "Accounts.AccountTerm",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Uid = c.Guid(nullable: false),
                        DaysDiscountAvailable = c.Int(),
                        DaysUntilDue = c.Int(),
                        DiscountCalculation = c.String(),
                        DiscountDateBasedOn = c.String(),
                        DueDateBasedOn = c.String(),
                        IsDiscountCalculatedOnFreight = c.Boolean(),
                        IsDiscountCalculatedOnMiscellaneous = c.Boolean(),
                        IsDiscountCalculatedOnSalePurchase = c.Boolean(),
                        IsDiscountCalculatedOnTax = c.Boolean(),
                        IsDiscountCalculatedOnTradeDiscount = c.Boolean(),
                        ModifiedBy = c.String(),
                        UseGracePeriods = c.Boolean(),
                        UseValueAddedTax = c.Boolean(),
                        AccountKey = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Accounts.AccountAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        AttributeValueID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Attributes.AttributeValue", t => t.AttributeValueID, cascadeDelete: true)
                .ForeignKey("Accounts.Account", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AttributeValueID)
                .Index(t => t.MasterID);

            CreateTable(
                "Contacts.Opportunities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StageName = c.String(maxLength: 100),
                        Type = c.String(maxLength: 100),
                        DeliveryInstructions = c.String(maxLength: 300),
                        PurchaseOrder = c.String(maxLength: 50),
                        CloseDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProbabilityOfClose = c.Decimal(precision: 18, scale: 2),
                        ProbabilityOfWin = c.Decimal(precision: 18, scale: 2),
                        AccountID = c.Int(nullable: false),
                        PrimaryContactID = c.Int(),
                        ClientProjectContactID = c.Int(),
                        OFTDeliveryContactID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("Contacts.Contact", t => t.ClientProjectContactID)
                .ForeignKey("Contacts.Contact", t => t.OFTDeliveryContactID)
                .ForeignKey("Contacts.Contact", t => t.PrimaryContactID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AccountID)
                .Index(t => t.PrimaryContactID)
                .Index(t => t.ClientProjectContactID)
                .Index(t => t.OFTDeliveryContactID);

            CreateTable(
                "Accounts.AccountStatus",
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
                "Accounts.AccountType",
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
                        StoreID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.StoreID);

            CreateTable(
                "Advertising.AdAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        AdID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("Advertising.Ad", t => t.AdID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AdID)
                .Index(t => t.AccountID);

            CreateTable(
                "Advertising.Ad",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        TargetURL = c.String(maxLength: 512, unicode: false),
                        Caption = c.String(maxLength: 256, unicode: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ExpirationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 4),
                        ImpressionCounterID = c.Int(),
                        ClickCounterID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Counters.Counter", t => t.ClickCounterID)
                .ForeignKey("Counters.Counter", t => t.ImpressionCounterID)
                .ForeignKey("Advertising.AdStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Advertising.AdType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.ImpressionCounterID)
                .Index(t => t.ClickCounterID);

            CreateTable(
                "Advertising.AdImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        AdID = c.Int(nullable: false),
                        LibraryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Advertising.Ad", t => t.AdID, cascadeDelete: true)
                .ForeignKey("Media.Library", t => t.LibraryID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AdID)
                .Index(t => t.LibraryID);

            CreateTable(
                "Advertising.AdStore",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        AdID = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Advertising.Ad", t => t.AdID, cascadeDelete: true)
                .ForeignKey("Stores.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AdID)
                .Index(t => t.StoreID);

            CreateTable(
                "Advertising.AdZone",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        AdID = c.Int(nullable: false),
                        ZoneID = c.Int(nullable: false),
                        AdZoneAccessID = c.Int(),
                        ImpressionCounterID = c.Int(),
                        ClickCounterID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Advertising.Ad", t => t.AdID, cascadeDelete: true)
                .ForeignKey("Advertising.AdZoneAccess", t => t.AdZoneAccessID)
                .ForeignKey("Counters.Counter", t => t.ClickCounterID)
                .ForeignKey("Counters.Counter", t => t.ImpressionCounterID)
                .ForeignKey("Advertising.Zone", t => t.ZoneID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.AdID)
                .Index(t => t.ZoneID)
                .Index(t => t.AdZoneAccessID)
                .Index(t => t.ImpressionCounterID)
                .Index(t => t.ClickCounterID);

            CreateTable(
                "Advertising.AdZoneAccess",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UniqueAdLimit = c.Int(nullable: false),
                        ImpressionLimit = c.Int(nullable: false),
                        ClickLimit = c.Int(nullable: false),
                        ZoneID = c.Int(),
                        SubscriptionID = c.Int(),
                        ImpressionCounterID = c.Int(),
                        ClickCounterID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Counters.Counter", t => t.ClickCounterID)
                .ForeignKey("Counters.Counter", t => t.ImpressionCounterID)
                .ForeignKey("Payments.Subscription", t => t.SubscriptionID)
                .ForeignKey("Advertising.Zone", t => t.ZoneID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.ZoneID)
                .Index(t => t.SubscriptionID)
                .Index(t => t.ImpressionCounterID)
                .Index(t => t.ClickCounterID);

            CreateTable(
                "Counters.Counter",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        TypeID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        Value = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Counters.CounterType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.TypeID);

            CreateTable(
                "Counters.CounterLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        TypeID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        Value = c.Decimal(precision: 18, scale: 2),
                        CounterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Counters.Counter", t => t.CounterID, cascadeDelete: true)
                .ForeignKey("Counters.CounterLogType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.TypeID)
                .Index(t => t.CounterID);

            CreateTable(
                "Counters.CounterLogType",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Counters.CounterType",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Advertising.Zone",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Advertising.ZoneStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Advertising.ZoneType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID);

            CreateTable(
                "Advertising.ZoneStatus",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Advertising.ZoneType",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Tracking.CampaignAd",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        CampaignID = c.Int(nullable: false),
                        AdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Advertising.Ad", t => t.AdID, cascadeDelete: true)
                .ForeignKey("Tracking.Campaign", t => t.CampaignID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.CampaignID)
                .Index(t => t.AdID);

            CreateTable(
                "Tracking.Campaign",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        ProposedStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProposedEnd = c.DateTime(precision: 7, storeType: "datetime2"),
                        ActualStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        ActualEnd = c.DateTime(precision: 7, storeType: "datetime2"),
                        BudgetedCost = c.Decimal(precision: 18, scale: 4),
                        OtherCost = c.Decimal(precision: 18, scale: 4),
                        ExpectedRevenue = c.Decimal(precision: 18, scale: 4),
                        TotalActualCost = c.Decimal(precision: 18, scale: 4),
                        TotalCampaignActivityActualCost = c.Decimal(precision: 18, scale: 4),
                        ExchangeRate = c.Decimal(precision: 18, scale: 8),
                        CodeName = c.String(maxLength: 32),
                        PromotionCodeName = c.String(maxLength: 128),
                        Message = c.String(maxLength: 256),
                        Objective = c.String(),
                        ExpectedResponse = c.Int(),
                        UTCConversionTimeZoneCode = c.Int(),
                        IsTemplate = c.Boolean(),
                        CreatedByUserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.CreatedByUserID)
                .ForeignKey("Tracking.CampaignStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Tracking.CampaignType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.CreatedByUserID);

            CreateTable(
                "Tracking.CampaignStatus",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Tracking.CampaignType",
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
                        StoreID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.StoreID);

            CreateTable(
                "Advertising.AdStatus",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Advertising.AdType",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Shopping.Checkout",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active);

            CreateTable(
                "System.SystemLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        StoreID = c.Int(),
                        DataID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.StoreID);

            CreateTable(
                "Tracking.Event",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        IPAddress = c.String(maxLength: 20, unicode: false),
                        Score = c.Int(),
                        AddressID = c.Int(),
                        IPOrganizationID = c.Int(),
                        UserID = c.Int(),
                        DidBounce = c.Boolean(),
                        OperatingSystem = c.String(maxLength: 20, unicode: false),
                        Browser = c.String(maxLength: 10, unicode: false),
                        Language = c.String(maxLength: 50, unicode: false),
                        ContainsSocialProfile = c.Boolean(),
                        Delta = c.Int(),
                        Duration = c.Int(),
                        StartedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Time = c.String(maxLength: 100, unicode: false),
                        EntryPage = c.String(maxLength: 2000, unicode: false),
                        ExitPage = c.String(maxLength: 2000, unicode: false),
                        IsFirstTrigger = c.Boolean(),
                        Flash = c.String(maxLength: 10, unicode: false),
                        Keywords = c.String(maxLength: 100),
                        PartitionKey = c.String(maxLength: 100, unicode: false),
                        Referrer = c.String(maxLength: 2000, unicode: false),
                        ReferringHost = c.String(maxLength: 300, unicode: false),
                        RowKey = c.String(maxLength: 50),
                        Source = c.Int(),
                        TotalTrigggers = c.Int(),
                        CampaignID = c.Int(),
                        ContactID = c.Int(),
                        SiteDomainID = c.Int(),
                        VisitorID = c.Int(),
                        VisitID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .ForeignKey("Tracking.Campaign", t => t.CampaignID)
                .ForeignKey("Contacts.Contact", t => t.ContactID)
                .ForeignKey("Tracking.IPOrganization", t => t.IPOrganizationID)
                .ForeignKey("Stores.SiteDomain", t => t.SiteDomainID)
                .ForeignKey("Tracking.EventStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Tracking.EventType", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID)
                .ForeignKey("Tracking.Visit", t => t.VisitID)
                .ForeignKey("Tracking.Visitor", t => t.VisitorID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.AddressID)
                .Index(t => t.IPOrganizationID)
                .Index(t => t.UserID)
                .Index(t => t.CampaignID)
                .Index(t => t.ContactID)
                .Index(t => t.SiteDomainID)
                .Index(t => t.VisitorID)
                .Index(t => t.VisitID);

            CreateTable(
                "Tracking.IPOrganization",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        StatusID = c.Int(nullable: false),
                        IPAddress = c.String(maxLength: 20, unicode: false),
                        Score = c.Int(),
                        VisitorKey = c.String(maxLength: 50),
                        AddressID = c.Int(),
                        PrimaryUserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .ForeignKey("Contacts.User", t => t.PrimaryUserID)
                .ForeignKey("Tracking.IPOrganizationStatus", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.StatusID)
                .Index(t => t.AddressID)
                .Index(t => t.PrimaryUserID);

            CreateTable(
                "Tracking.IPOrganizationStatus",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Tracking.PageViewEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        EventID = c.Int(nullable: false),
                        PageViewID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Tracking.Event", t => t.EventID, cascadeDelete: true)
                .ForeignKey("Tracking.PageView", t => t.PageViewID, cascadeDelete: true)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.EventID)
                .Index(t => t.PageViewID);

            CreateTable(
                "Tracking.PageView",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        IPAddress = c.String(maxLength: 20, unicode: false),
                        Score = c.Int(),
                        AddressID = c.Int(),
                        IPOrganizationID = c.Int(),
                        UserID = c.Int(),
                        DidBounce = c.Boolean(),
                        OperatingSystem = c.String(maxLength: 20, unicode: false),
                        Browser = c.String(maxLength: 10, unicode: false),
                        Language = c.String(maxLength: 50, unicode: false),
                        ContainsSocialProfile = c.Boolean(),
                        Delta = c.Int(),
                        Duration = c.Int(),
                        StartedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Time = c.String(maxLength: 100, unicode: false),
                        EntryPage = c.String(maxLength: 2000, unicode: false),
                        ExitPage = c.String(maxLength: 2000, unicode: false),
                        IsFirstTrigger = c.Boolean(),
                        Flash = c.String(maxLength: 10, unicode: false),
                        Keywords = c.String(maxLength: 100),
                        PartitionKey = c.String(maxLength: 100, unicode: false),
                        Referrer = c.String(maxLength: 2000, unicode: false),
                        ReferringHost = c.String(maxLength: 300, unicode: false),
                        RowKey = c.String(maxLength: 50),
                        Source = c.Int(),
                        TotalTrigggers = c.Int(),
                        CampaignID = c.Int(),
                        ContactID = c.Int(),
                        SiteDomainID = c.Int(),
                        VisitorID = c.Int(),
                        Title = c.String(maxLength: 500, unicode: false),
                        URI = c.String(maxLength: 2000, unicode: false),
                        ViewedOn = c.DateTime(),
                        VisitKey = c.String(maxLength: 50),
                        CategoryID = c.Int(),
                        ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .ForeignKey("Tracking.Campaign", t => t.CampaignID)
                .ForeignKey("Categories.Category", t => t.CategoryID)
                .ForeignKey("Contacts.Contact", t => t.ContactID)
                .ForeignKey("Tracking.IPOrganization", t => t.IPOrganizationID)
                .ForeignKey("Products.Product", t => t.ProductID)
                .ForeignKey("Stores.SiteDomain", t => t.SiteDomainID)
                .ForeignKey("Tracking.PageViewStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Tracking.PageViewType", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID)
                .ForeignKey("Tracking.Visitor", t => t.VisitorID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.AddressID)
                .Index(t => t.IPOrganizationID)
                .Index(t => t.UserID)
                .Index(t => t.CampaignID)
                .Index(t => t.ContactID)
                .Index(t => t.SiteDomainID)
                .Index(t => t.VisitorID)
                .Index(t => t.CategoryID)
                .Index(t => t.ProductID);

            CreateTable(
                "Tracking.PageViewStatus",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Tracking.PageViewType",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Tracking.Visitor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        JsonAttributes = c.String(),
                        IPAddress = c.String(maxLength: 20, unicode: false),
                        Score = c.Int(),
                        AddressID = c.Int(),
                        IPOrganizationID = c.Int(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .ForeignKey("Tracking.IPOrganization", t => t.IPOrganizationID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.AddressID)
                .Index(t => t.IPOrganizationID)
                .Index(t => t.UserID);

            CreateTable(
                "Tracking.EventStatus",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Tracking.EventType",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Tracking.Visit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        StatusID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        IPAddress = c.String(maxLength: 20, unicode: false),
                        Score = c.Int(),
                        AddressID = c.Int(),
                        IPOrganizationID = c.Int(),
                        UserID = c.Int(),
                        DidBounce = c.Boolean(),
                        OperatingSystem = c.String(maxLength: 20, unicode: false),
                        Browser = c.String(maxLength: 10, unicode: false),
                        Language = c.String(maxLength: 50, unicode: false),
                        ContainsSocialProfile = c.Boolean(),
                        Delta = c.Int(),
                        Duration = c.Int(),
                        StartedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Time = c.String(maxLength: 100, unicode: false),
                        EntryPage = c.String(maxLength: 2000, unicode: false),
                        ExitPage = c.String(maxLength: 2000, unicode: false),
                        IsFirstTrigger = c.Boolean(),
                        Flash = c.String(maxLength: 10, unicode: false),
                        Keywords = c.String(maxLength: 100),
                        PartitionKey = c.String(maxLength: 100, unicode: false),
                        Referrer = c.String(maxLength: 2000, unicode: false),
                        ReferringHost = c.String(maxLength: 300, unicode: false),
                        RowKey = c.String(maxLength: 50),
                        Source = c.Int(),
                        TotalTrigggers = c.Int(),
                        CampaignID = c.Int(),
                        ContactID = c.Int(),
                        SiteDomainID = c.Int(),
                        VisitorID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID)
                .ForeignKey("Tracking.Campaign", t => t.CampaignID)
                .ForeignKey("Contacts.Contact", t => t.ContactID)
                .ForeignKey("Tracking.IPOrganization", t => t.IPOrganizationID)
                .ForeignKey("Stores.SiteDomain", t => t.SiteDomainID)
                .ForeignKey("Tracking.VisitStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID)
                .ForeignKey("Tracking.Visitor", t => t.VisitorID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.StatusID)
                .Index(t => t.AddressID)
                .Index(t => t.IPOrganizationID)
                .Index(t => t.UserID)
                .Index(t => t.CampaignID)
                .Index(t => t.ContactID)
                .Index(t => t.SiteDomainID)
                .Index(t => t.VisitorID);

            CreateTable(
                "Tracking.VisitStatus",
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
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Hangfire.AggregatedCounter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.Long(nullable: false),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "Hangfire.Counter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.Short(nullable: false),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "Hangfire.Hash",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Field = c.String(nullable: false, maxLength: 100),
                        Value = c.String(),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Key, name: "IX_HangFire_Hash_Key")
                .Index(t => new { t.Key, t.Field }, unique: true, name: "UX_HangFire_Hash_Key_Field")
                .Index(t => t.ExpireAt, name: "IX_HangFire_Hash_ExpireAt");

            CreateTable(
                "Hangfire.JobParameter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Value = c.String(),
                        JobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hangfire.Job", t => t.JobId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => new { t.Id, t.Name }, name: "IX_HangFire_JobParameter_JobIdAndName")
                .Index(t => t.JobId);

            CreateTable(
                "Hangfire.Job",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateId = c.Int(),
                        StateName = c.String(maxLength: 20),
                        InvocationData = c.String(nullable: false),
                        Arguments = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.StateName)
                .Index(t => t.ExpireAt);

            CreateTable(
                "Hangfire.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Reason = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        Data = c.String(),
                        JobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hangfire.Job", t => t.JobId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.JobId);

            CreateTable(
                "Hangfire.JobQueue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Queue = c.String(nullable: false, maxLength: 50),
                        FetchedAt = c.DateTime(),
                        JobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hangfire.Job", t => t.JobId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => new { t.Queue, t.FetchedAt }, name: "IX_HangFire_JobQueue_QueueAndFetchedAt")
                .Index(t => t.JobId);

            CreateTable(
                "Hangfire.List",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.String(),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Key)
                .Index(t => t.ExpireAt);

            CreateTable(
                "Hangfire.Schema",
                c => new
                    {
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Version);

            CreateTable(
                "Hangfire.Server",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        Data = c.String(),
                        LastHeartbeat = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "Hangfire.Set",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Score = c.Double(nullable: false),
                        Value = c.String(nullable: false, maxLength: 256),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Key)
                .Index(t => new { t.Key, t.Value }, name: "UX_HangFire_Set_KeyAndValue")
                .Index(t => t.ExpireAt);

            CreateTable(
                "Notifications.Action",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SystemMessage = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        ActionType = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active);

            CreateTable(
                "Pricing.PriceRuleAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        PriceRuleID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.PriceRuleID)
                .Index(t => t.AccountID);

            CreateTable(
                "Pricing.PriceRule",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PriceAdjustment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPercentage = c.Boolean(nullable: false),
                        IsHigherThan = c.Boolean(nullable: false),
                        UsePriceBase = c.Boolean(nullable: false),
                        Currency = c.String(),
                        IsExclusive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Pricing.PriceRuleAccountType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        PriceRuleID = c.Int(nullable: false),
                        AccountTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.AccountType", t => t.AccountTypeID, cascadeDelete: true)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.PriceRuleID)
                .Index(t => t.AccountTypeID);

            CreateTable(
                "Pricing.PriceRuleCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        PriceRuleID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.PriceRuleID)
                .Index(t => t.CategoryID);

            CreateTable(
                "Pricing.PriceRuleProduct",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        PriceRuleID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        OverridePrice = c.Boolean(nullable: false),
                        OverrideCost = c.Decimal(precision: 18, scale: 4),
                        OverrideSalePrice = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.PriceRuleID)
                .Index(t => t.ProductID);

            CreateTable(
                "Pricing.PriceRuleProductType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        PriceRuleID = c.Int(nullable: false),
                        ProductTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .ForeignKey("Products.ProductType", t => t.ProductTypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.PriceRuleID)
                .Index(t => t.ProductTypeID);

            CreateTable(
                "Pricing.PriceRuleVendor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        PriceRuleID = c.Int(nullable: false),
                        VendorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Pricing.PriceRule", t => t.PriceRuleID, cascadeDelete: true)
                .ForeignKey("Vendors.Vendor", t => t.VendorID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.PriceRuleID)
                .Index(t => t.VendorID);

            CreateTable(
                "Contacts.ProfanityFilter",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active);

            CreateTable(
                "Reporting.Reports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        ResultsData = c.String(),
                        RunByUserID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.RunByUserID, cascadeDelete: true)
                .ForeignKey("Reporting.ReportTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.RunByUserID)
                .Index(t => t.TypeID);

            CreateTable(
                "Reporting.ReportTypes",
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
                        Template = c.Binary(),
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
                "Hangfire.ScheduledJobConfiguration",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        NotificationTemplateID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Messaging.EmailTemplate", t => t.NotificationTemplateID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.NotificationTemplateID);

            CreateTable(
                "Hangfire.ScheduledJobConfigurationSetting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        ScheduledJobConfigurationID = c.Int(nullable: false),
                        SettingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Hangfire.ScheduledJobConfiguration", t => t.ScheduledJobConfigurationID, cascadeDelete: true)
                .ForeignKey("System.Setting", t => t.SettingID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ScheduledJobConfigurationID)
                .Index(t => t.SettingID);

            CreateTable(
                "System.Setting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        StoreID = c.Int(),
                        Value = c.String(nullable: false),
                        TypeID = c.Int(nullable: false),
                        SettingGroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("System.SettingGroup", t => t.SettingGroupID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("System.SettingType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StoreID)
                .Index(t => t.TypeID)
                .Index(t => t.SettingGroupID);

            CreateTable(
                "System.SettingGroup",
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
                "System.SettingType",
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
                "Shipping.UPSEndOfDay",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        ShipToEmailAddress = c.String(),
                        ThirdPartyReceiverPostalCode = c.String(),
                        ShipToCustomerID = c.String(),
                        ShipToCompanyorName = c.String(),
                        ShipToAttention = c.String(),
                        ShipToAddress1 = c.String(),
                        ShipToAddress2 = c.String(),
                        ShipToAddress3 = c.String(),
                        ShipToCountryTerritory = c.String(),
                        ShipToPostalCode = c.String(),
                        ShipToCityorTown = c.String(),
                        ShipToStateProvinceCounty = c.String(),
                        ShipToTelephone = c.String(),
                        ShipToFaxNumber = c.String(),
                        ShipToReceiverUPSAccountNumber = c.String(),
                        ShipToResidentialIndicator = c.String(),
                        ShipFromCompanyorName = c.String(),
                        ShipFromAddress1 = c.String(),
                        ShipFromAttention = c.String(),
                        ShipFromAddress2 = c.String(),
                        ShipFromAddress3 = c.String(),
                        ShipFromCountryTerritory = c.String(),
                        ShipFromPostalCode = c.String(),
                        ShipFromCityorTown = c.String(),
                        ShipFromStateProvinceCounty = c.String(),
                        ShipFromTelephone = c.String(),
                        ShipFromFaxNumber = c.String(),
                        ShipFromEmailAddress = c.String(),
                        ShipFromUPSAccountNumber = c.String(),
                        ThirdPartyCustomerID = c.String(),
                        ThirdPartyCompanyorName = c.String(),
                        ThirdPartyAttention = c.String(),
                        ThirdPartyAddress1 = c.String(),
                        ThirdPartyAddress2 = c.String(),
                        ThirdPartyAddress3 = c.String(),
                        ThirdPartyCountryTerritory = c.String(),
                        ThirdPartyCityorTown = c.String(),
                        ThirdPartyStateProvinceCounty = c.String(),
                        ThirdPartyTelephone = c.String(),
                        ThirdPartyFaxNumber = c.String(),
                        ThirdPartyUPSAccountNumber = c.String(),
                        InternationalDocumentationInvoiceImporterSameAsShipTo = c.String(),
                        InternationalDocumentationInvoiceTermsOfSale = c.String(),
                        InternationalDocumentationInvoiceReasonForExport = c.String(),
                        InternationalDocumentationInvoiceDeclarationStatement = c.String(),
                        InternationalDocumentationInvoiceAdditionalComments = c.String(),
                        InternationalDocumentationInvoiceLineTotal = c.String(),
                        InternationalDocumentationTotalInvoiceAmount = c.String(),
                        InternationalDocumentationCustomsValueTotal = c.String(),
                        InternationalDocumentationDutiescost = c.String(),
                        InternationalDocumentationTaxesandFeescost = c.String(),
                        InternationalDocumentationTotalDutiesandTaxes = c.String(),
                        GoodsPartNumber = c.String(),
                        GoodsDescriptionOfGood = c.String(),
                        GoodsInvNAFTACN22TariffCode = c.String(),
                        GoodsDDTCQuantity = c.String(),
                        GoodsDDTCUnitofMeasure = c.String(),
                        ImporterCustomerID = c.String(),
                        ImporterCompanyorName = c.String(),
                        ImporterAttention = c.String(),
                        ImporterAddress1 = c.String(),
                        ImporterAddress2 = c.String(),
                        ImporterAddress3 = c.String(),
                        ImporterCountryTerritory = c.String(),
                        ImporterCityorTown = c.String(),
                        ImporterStateProvinceCounty = c.String(),
                        ImporterPostalCode = c.String(),
                        ImporterTelephone = c.String(),
                        ImporterUPSAccountNumber = c.String(),
                        UltimateConsigneeCustomerID = c.String(),
                        UltimateConsigneeCompanyorName = c.String(),
                        UltimateConsigneeAttention = c.String(),
                        UltimateConsigneeAddress1 = c.String(),
                        UltimateConsigneeAddress2 = c.String(),
                        UltimateConsigneeAddress3 = c.String(),
                        UltimateConsigneeCountryTerritory = c.String(),
                        UltimateConsigneeCityorTown = c.String(),
                        UltimateConsigneeStateProvinceCounty = c.String(),
                        UltimateConsigneePostalCode = c.String(),
                        UltimateConsigneeTelephone = c.String(),
                        UltimateConsigneeTaxIDNumber = c.String(),
                        ProducerCustomerID = c.String(),
                        ProducerCompanyorName = c.String(),
                        ProducerAttention = c.String(),
                        ProducerAddress1 = c.String(),
                        ProducerAddress2 = c.String(),
                        ProducerAddress3 = c.String(),
                        ProducerCountryTerritory = c.String(),
                        ProducerCityorTown = c.String(),
                        ProducerStateProvinceCounty = c.String(),
                        ProducerPostalCode = c.String(),
                        ProducerTelephone = c.String(),
                        ProducerTaxIDNumber = c.String(),
                        GoodsInvNAFTACOCN22CountryTerritoryOfOrigin = c.String(),
                        GoodsInvoiceCN22Units = c.String(),
                        GoodsInvoiceCN22UnitOfMeasure = c.String(),
                        GoodsInvoiceSEDCN22UnitPrice = c.String(),
                        GoodsCurrencyCode = c.String(),
                        GoodsInvoiceNetWeight = c.String(),
                        GoodsSEDCOGrossWeight = c.String(),
                        GoodsSEDCOLBSorKgs = c.String(),
                        GoodsSEDPrintGoodOnSED = c.String(),
                        GoodsInvoiceSEDCOMarksAndNumbers = c.String(),
                        GoodsSEDScheduleBNumber = c.String(),
                        GoodsSEDScheduleBUnits1 = c.String(),
                        GoodsSEDScheduleBUnits2 = c.String(),
                        GoodsSEDUnitofMeasure1 = c.String(),
                        GoodsSEDUnitofMeasure2 = c.String(),
                        GoodsSEDECCN = c.String(),
                        GoodsDFM = c.String(),
                        GoodsSEDException = c.String(),
                        GoodsSEDLicenseNumber = c.String(),
                        GoodsSEDLicenseExpirationDate = c.String(),
                        GoodsSEDExportCode = c.String(),
                        GoodsDDTCRegistrationNumber = c.String(),
                        GoodsDDTCSignificantMilitaryEquipmentIndicator = c.String(),
                        GoodsDDTCEligiblePartyCertificationIndicator = c.String(),
                        GoodsDDTCUSMLCategoryCode = c.String(),
                        GoodsDDTCLicenseLineNumber = c.String(),
                        GoodsNAFTAPrintGoodOnNAFTA = c.String(),
                        GoodsNAFTAPreferenceCriterion = c.String(),
                        GoodsNAFTAProducer = c.String(),
                        GoodsNAFTAMultipleCountriesTerritoriesofOrigin = c.String(),
                        GoodsNAFTANetCostMethod = c.String(),
                        GoodsNAFTANetCostPeriodStartDate = c.String(),
                        GoodsNAFTANetCostPeriodEndDate = c.String(),
                        GoodsCOPrintGoodOnCO = c.String(),
                        GoodsCONumberOfPackagesContainingGood = c.String(),
                        GoodsSLIPrintGoodOnSLI = c.String(),
                        InternationalDocumentationInvoiceCN22CurrencyCode = c.String(),
                        InternationalDocumentationInvoiceDiscount = c.String(),
                        InternationalDocumentationInvoiceChargesFreight = c.String(),
                        InternationalDocumentationInvoiceChargesInsurance = c.String(),
                        InternationalDocumentationInvoiceChargesOther = c.String(),
                        InternationalDocumentationCustomsValueCurrencyCode = c.String(),
                        InternationalDocumentationCustomsValueOrigin = c.String(),
                        InternationalDocumentationSEDCode = c.String(),
                        InternationalDocumentationSEDPartiesToTransaction = c.String(),
                        InternationalDocumentationSEDUltimateConsigneeSameAsShipTo = c.String(),
                        InternationalDocumentationSEDCountryTerritoryOfUltimateDestination = c.String(),
                        InternationalDocumentationSEDAESTransactionNumber = c.String(),
                        InternationalDocumentationSEDAESTransactionNumberType = c.String(),
                        InternationalDocumentationSEDRoutedTransaction = c.String(),
                        InternationalDocumentationSEDCurrencyConversion = c.String(),
                        InternationalDocumentationCOUPSPrepareCO = c.String(),
                        InternationalDocumentationCOOwnerOrAgent = c.String(),
                        InternationalDocumentationNAFTAProducer = c.String(),
                        InternationalDocumentationNAFTABlanketPeriodStart = c.String(),
                        InternationalDocumentationNAFTABlanketPeriodEnd = c.String(),
                        InternationalDocumentationMovementReferenceNumberMRN = c.String(),
                        InternationalDocumentationCN22GoodsType = c.String(),
                        InternationalDocumentationCN22GoodsTypeOtherDescription = c.String(),
                        InternationalDocumentationRequestDutiesandTaxes = c.String(),
                        InternationalDocumentationVATcost = c.String(),
                        ThirdPartyReceiverCustomerID = c.String(),
                        ThirdPartyReceiverCompanyorName = c.String(),
                        ThirdPartyReceiverAttention = c.String(),
                        ThirdPartyReceiverAddress1 = c.String(),
                        ThirdPartyReceiverAddress2 = c.String(),
                        ThirdPartyReceiverAddress3 = c.String(),
                        ThirdPartyReceiverCountryTerritory = c.String(),
                        PackageUSPSWeight = c.String(),
                        ThirdPartyReceiverCityorTown = c.String(),
                        ThirdPartyReceiverStateProvinceCounty = c.String(),
                        ThirdPartyReceiverTelephone = c.String(),
                        ThirdPartyReceiverFaxNumber = c.String(),
                        ThirdPartyReceiverUPSAccountNumber = c.String(),
                        PackageVoidIndicator = c.String(),
                        PackagePackageReferenceCharge = c.String(),
                        PackagePackagePublishedCharge = c.String(),
                        PackagePackageType = c.String(),
                        PackageWeight = c.String(),
                        PackageDeliveryConfirmationNegotiatedRatesCharge = c.String(),
                        PackageNonmachinableNegotiatedRatesCharge = c.String(),
                        PackageNonDDUNegotiatedRatesCharge = c.String(),
                        PackageTrackingNumber = c.String(),
                        PackageLargePackageIndicator = c.String(),
                        PackageReference1 = c.String(),
                        PackageReference2 = c.String(),
                        PackageAdditionalHandlingOption = c.String(),
                        PackagePackageNegotiatedRatesCharge = c.Decimal(precision: 18, scale: 2),
                        PackageAdditionalHandlingNegotiatedRatesCharge = c.String(),
                        PackageDeliveryConfirmationSignatureRequired = c.String(),
                        PackageDeclaredValueOption = c.String(),
                        PackageDeclaredValueShipperPaid = c.String(),
                        PackageDeclaredValueAmount = c.String(),
                        PackageDeliveryConfirmationOption = c.String(),
                        PackageDeliveryConfirmationReferenceCharge = c.String(),
                        PackageMerchandiseDescriptionforPackage = c.String(),
                        PackageLength = c.String(),
                        PackageWidth = c.String(),
                        PackageHeight = c.String(),
                        ShipmentInformationServiceType = c.String(),
                        ShipmentInformationPageNumber = c.String(),
                        ShipmentInformationShipmentID = c.String(),
                        ShipmentInformationActualWeight = c.String(),
                        ShipmentInformationDimensionalWeight = c.String(),
                        ShipmentInformationBillableWeight = c.String(),
                        ShipmentInformationBillTransportationTo = c.String(),
                        ShipmentInformationBillDutyandTaxTo = c.String(),
                        ShipmentInformationTransitTime = c.String(),
                        ShipmentInformationSaturdayDeliveryNegotiatedRatesCharge = c.String(),
                        ShipmentInformationNumberofPackages = c.String(),
                        ShipmentInformationSaturdayPickupOption = c.String(),
                        ShipmentInformationCollectiondate = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active);

            CreateTable(
                "Geography.ZipCode",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        ZipCode = c.String(maxLength: 20),
                        ZipType = c.String(maxLength: 255),
                        CityName = c.String(maxLength: 255),
                        CityType = c.String(maxLength: 255),
                        CountyName = c.String(maxLength: 255),
                        CountyFIPS = c.Long(),
                        StateName = c.String(maxLength: 255),
                        StateAbbreviation = c.String(maxLength: 255),
                        StateFIPS = c.Long(),
                        MSACode = c.Long(),
                        AreaCode = c.String(maxLength: 255),
                        TimeZone = c.String(maxLength: 255),
                        UTC = c.Long(),
                        DST = c.String(maxLength: 255),
                        Latitude = c.Decimal(precision: 18, scale: 8),
                        Longitude = c.Decimal(precision: 18, scale: 8),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.ZipCode);
        }

        public override void Down()
        {
            DropForeignKey("Hangfire.ScheduledJobConfigurationSetting", "SettingID", "System.Setting");
            DropForeignKey("System.Setting", "TypeID", "System.SettingType");
            DropForeignKey("System.Setting", "StoreID", "Stores.Store");
            DropForeignKey("System.Setting", "SettingGroupID", "System.SettingGroup");
            DropForeignKey("Hangfire.ScheduledJobConfigurationSetting", "ScheduledJobConfigurationID", "Hangfire.ScheduledJobConfiguration");
            DropForeignKey("Hangfire.ScheduledJobConfiguration", "NotificationTemplateID", "Messaging.EmailTemplate");
            DropForeignKey("Reporting.Reports", "TypeID", "Reporting.ReportTypes");
            DropForeignKey("Reporting.Reports", "RunByUserID", "Contacts.User");
            DropForeignKey("Pricing.PriceRuleAccount", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleVendor", "VendorID", "Vendors.Vendor");
            DropForeignKey("Pricing.PriceRuleVendor", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleProductType", "ProductTypeID", "Products.ProductType");
            DropForeignKey("Pricing.PriceRuleProductType", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleProduct", "ProductID", "Products.Product");
            DropForeignKey("Pricing.PriceRuleProduct", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleCategory", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleCategory", "CategoryID", "Categories.Category");
            DropForeignKey("Pricing.PriceRuleAccountType", "PriceRuleID", "Pricing.PriceRule");
            DropForeignKey("Pricing.PriceRuleAccountType", "AccountTypeID", "Accounts.AccountType");
            DropForeignKey("Pricing.PriceRuleAccount", "AccountID", "Accounts.Account");
            DropForeignKey("Hangfire.JobQueue", "JobId", "Hangfire.Job");
            DropForeignKey("Hangfire.JobParameter", "JobId", "Hangfire.Job");
            DropForeignKey("Hangfire.State", "JobId", "Hangfire.Job");
            DropForeignKey("Tracking.Event", "VisitorID", "Tracking.Visitor");
            DropForeignKey("Tracking.Event", "VisitID", "Tracking.Visit");
            DropForeignKey("Tracking.Visit", "VisitorID", "Tracking.Visitor");
            DropForeignKey("Tracking.Visit", "UserID", "Contacts.User");
            DropForeignKey("Tracking.Visit", "StatusID", "Tracking.VisitStatus");
            DropForeignKey("Tracking.Visit", "SiteDomainID", "Stores.SiteDomain");
            DropForeignKey("Tracking.Visit", "IPOrganizationID", "Tracking.IPOrganization");
            DropForeignKey("Tracking.Visit", "ContactID", "Contacts.Contact");
            DropForeignKey("Tracking.Visit", "CampaignID", "Tracking.Campaign");
            DropForeignKey("Tracking.Visit", "AddressID", "Geography.Address");
            DropForeignKey("Tracking.Event", "UserID", "Contacts.User");
            DropForeignKey("Tracking.Event", "TypeID", "Tracking.EventType");
            DropForeignKey("Tracking.Event", "StatusID", "Tracking.EventStatus");
            DropForeignKey("Tracking.Event", "SiteDomainID", "Stores.SiteDomain");
            DropForeignKey("Tracking.PageViewEvent", "PageViewID", "Tracking.PageView");
            DropForeignKey("Tracking.PageView", "VisitorID", "Tracking.Visitor");
            DropForeignKey("Tracking.Visitor", "UserID", "Contacts.User");
            DropForeignKey("Tracking.Visitor", "IPOrganizationID", "Tracking.IPOrganization");
            DropForeignKey("Tracking.Visitor", "AddressID", "Geography.Address");
            DropForeignKey("Tracking.PageView", "UserID", "Contacts.User");
            DropForeignKey("Tracking.PageView", "TypeID", "Tracking.PageViewType");
            DropForeignKey("Tracking.PageView", "StatusID", "Tracking.PageViewStatus");
            DropForeignKey("Tracking.PageView", "SiteDomainID", "Stores.SiteDomain");
            DropForeignKey("Tracking.PageView", "ProductID", "Products.Product");
            DropForeignKey("Tracking.PageView", "IPOrganizationID", "Tracking.IPOrganization");
            DropForeignKey("Tracking.PageView", "ContactID", "Contacts.Contact");
            DropForeignKey("Tracking.PageView", "CategoryID", "Categories.Category");
            DropForeignKey("Tracking.PageView", "CampaignID", "Tracking.Campaign");
            DropForeignKey("Tracking.PageView", "AddressID", "Geography.Address");
            DropForeignKey("Tracking.PageViewEvent", "EventID", "Tracking.Event");
            DropForeignKey("Tracking.Event", "IPOrganizationID", "Tracking.IPOrganization");
            DropForeignKey("Tracking.IPOrganization", "StatusID", "Tracking.IPOrganizationStatus");
            DropForeignKey("Tracking.IPOrganization", "PrimaryUserID", "Contacts.User");
            DropForeignKey("Tracking.IPOrganization", "AddressID", "Geography.Address");
            DropForeignKey("Tracking.Event", "ContactID", "Contacts.Contact");
            DropForeignKey("Tracking.Event", "CampaignID", "Tracking.Campaign");
            DropForeignKey("Tracking.Event", "AddressID", "Geography.Address");
            DropForeignKey("System.SystemLog", "StoreID", "Stores.Store");
            DropForeignKey("Advertising.Ad", "TypeID", "Advertising.AdType");
            DropForeignKey("Advertising.Ad", "StatusID", "Advertising.AdStatus");
            DropForeignKey("Advertising.Ad", "ImpressionCounterID", "Counters.Counter");
            DropForeignKey("Advertising.Ad", "ClickCounterID", "Counters.Counter");
            DropForeignKey("Tracking.CampaignAd", "CampaignID", "Tracking.Campaign");
            DropForeignKey("Tracking.Campaign", "TypeID", "Tracking.CampaignType");
            DropForeignKey("Tracking.CampaignType", "StoreID", "Stores.Store");
            DropForeignKey("Tracking.Campaign", "StatusID", "Tracking.CampaignStatus");
            DropForeignKey("Tracking.Campaign", "CreatedByUserID", "Contacts.User");
            DropForeignKey("Tracking.CampaignAd", "AdID", "Advertising.Ad");
            DropForeignKey("Advertising.AdZone", "ZoneID", "Advertising.Zone");
            DropForeignKey("Advertising.AdZone", "ImpressionCounterID", "Counters.Counter");
            DropForeignKey("Advertising.AdZone", "ClickCounterID", "Counters.Counter");
            DropForeignKey("Advertising.AdZone", "AdZoneAccessID", "Advertising.AdZoneAccess");
            DropForeignKey("Advertising.AdZoneAccess", "ZoneID", "Advertising.Zone");
            DropForeignKey("Advertising.Zone", "TypeID", "Advertising.ZoneType");
            DropForeignKey("Advertising.Zone", "StatusID", "Advertising.ZoneStatus");
            DropForeignKey("Advertising.AdZoneAccess", "SubscriptionID", "Payments.Subscription");
            DropForeignKey("Advertising.AdZoneAccess", "ImpressionCounterID", "Counters.Counter");
            DropForeignKey("Advertising.AdZoneAccess", "ClickCounterID", "Counters.Counter");
            DropForeignKey("Counters.Counter", "TypeID", "Counters.CounterType");
            DropForeignKey("Counters.CounterLog", "TypeID", "Counters.CounterLogType");
            DropForeignKey("Counters.CounterLog", "CounterID", "Counters.Counter");
            DropForeignKey("Advertising.AdZone", "AdID", "Advertising.Ad");
            DropForeignKey("Advertising.AdStore", "StoreID", "Stores.Store");
            DropForeignKey("Advertising.AdStore", "AdID", "Advertising.Ad");
            DropForeignKey("Advertising.AdImage", "LibraryID", "Media.Library");
            DropForeignKey("Advertising.AdImage", "AdID", "Advertising.Ad");
            DropForeignKey("Advertising.AdAccount", "AdID", "Advertising.Ad");
            DropForeignKey("Advertising.AdAccount", "AccountID", "Accounts.Account");
            DropForeignKey("Accounts.AccountAddress", "AddressID", "Geography.Address");
            DropForeignKey("Accounts.AccountAddress", "AccountID", "Accounts.Account");
            DropForeignKey("Accounts.Account", "TypeID", "Accounts.AccountType");
            DropForeignKey("Accounts.AccountType", "StoreID", "Stores.Store");
            DropForeignKey("Accounts.Account", "StatusID", "Accounts.AccountStatus");
            DropForeignKey("Accounts.Account", "ParentID", "Accounts.Account");
            DropForeignKey("Contacts.Opportunities", "PrimaryContactID", "Contacts.Contact");
            DropForeignKey("Contacts.Opportunities", "OFTDeliveryContactID", "Contacts.Contact");
            DropForeignKey("Contacts.Opportunities", "ClientProjectContactID", "Contacts.Contact");
            DropForeignKey("Contacts.Opportunities", "AccountID", "Accounts.Account");
            DropForeignKey("Accounts.AccountAttribute", "MasterID", "Accounts.Account");
            DropForeignKey("Accounts.AccountAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Accounts.AccountTerm", "ID", "Accounts.Account");
            DropForeignKey("Accounts.AccountPricePoint", "PricePointID", "Pricing.PricePoint");
            DropForeignKey("Accounts.AccountPricePoint", "AccountID", "Accounts.Account");
            DropForeignKey("Accounts.AccountContact", "ContactID", "Contacts.Contact");
            DropForeignKey("Contacts.Contact", "TypeID", "Contacts.ContactType");
            DropForeignKey("Contacts.Contact", "ShippingAddressID", "Geography.Address");
            DropForeignKey("Contacts.Individual", "ID", "Contacts.Contact");
            DropForeignKey("Contacts.Customer", "ID", "Contacts.Contact");
            DropForeignKey("Purchasing.PurchaseOrder", "VendorTermID", "Vendors.VendorTerm");
            DropForeignKey("Purchasing.PurchaseOrder", "VendorID", "Vendors.Vendor");
            DropForeignKey("Purchasing.PurchaseOrder", "UserID", "Contacts.User");
            DropForeignKey("Purchasing.PurchaseOrder", "TypeID", "Purchasing.PurchaseOrderType");
            DropForeignKey("Purchasing.PurchaseOrder", "StoreID", "Stores.Store");
            DropForeignKey("Purchasing.PurchaseOrder", "StatusID", "Purchasing.PurchaseOrderStatus");
            DropForeignKey("Purchasing.PurchaseOrder", "StateID", "Purchasing.PurchaseOrderState");
            DropForeignKey("Purchasing.PurchaseOrder", "ShippingContactID", "Contacts.Contact");
            DropForeignKey("Purchasing.PurchaseOrder", "ShipCarrierID", "Shipping.ShipCarrier");
            DropForeignKey("Purchasing.PurchaseOrderItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Purchasing.PurchaseOrderItem", "UserID", "Contacts.User");
            DropForeignKey("Purchasing.PurchaseOrderItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Purchasing.PurchaseOrderItem", "StatusID", "Purchasing.PurchaseOrderItemStatus");
            DropForeignKey("Purchasing.PurchaseOrderItem", "ProductID", "Products.Product");
            DropForeignKey("Purchasing.PurchaseOrderItem", "MasterID", "Purchasing.PurchaseOrder");
            DropForeignKey("Discounts.PurchaseOrderItemDiscounts", "MasterID", "Purchasing.PurchaseOrderItem");
            DropForeignKey("Discounts.PurchaseOrderItemDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Purchasing.PurchaseOrderItemAttribute", "MasterID", "Purchasing.PurchaseOrderItem");
            DropForeignKey("Purchasing.PurchaseOrderItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Purchasing.PurchaseOrder", "InventoryLocationID", "Inventory.InventoryLocation");
            DropForeignKey("Purchasing.PurchaseOrder", "FreeOnBoardID", "Purchasing.FreeOnBoard");
            DropForeignKey("Discounts.PurchaseOrderDiscounts", "MasterID", "Purchasing.PurchaseOrder");
            DropForeignKey("Discounts.PurchaseOrderDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Purchasing.PurchaseOrder", "BillingContactID", "Contacts.Contact");
            DropForeignKey("Purchasing.PurchaseOrderAttribute", "MasterID", "Purchasing.PurchaseOrder");
            DropForeignKey("Purchasing.PurchaseOrderAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Purchasing.SalesOrderPurchaseOrder", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Ordering.SalesOrder", "UserID", "Contacts.User");
            DropForeignKey("Ordering.SalesOrder", "TypeID", "Ordering.SalesOrderType");
            DropForeignKey("Ordering.SalesOrder", "StoreID", "Stores.Store");
            DropForeignKey("Ordering.SalesOrder", "StatusID", "Ordering.SalesOrderStatus");
            DropForeignKey("Ordering.SalesOrder", "StateID", "Ordering.SalesOrderState");
            DropForeignKey("Ordering.SalesOrder", "ShippingContactID", "Contacts.Contact");
            DropForeignKey("Payments.SalesOrderPayment", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Payments.SalesOrderPayment", "PaymentID", "Payments.Payment");
            DropForeignKey("Discounts.SalesOrderDiscounts", "MasterID", "Ordering.SalesOrder");
            DropForeignKey("Discounts.SalesOrderDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Ordering.SalesOrder", "CustomerPriorityID", "Contacts.CustomerPriority");
            DropForeignKey("Ordering.SalesOrder", "ParentID", "Ordering.SalesOrder");
            DropForeignKey("Ordering.SalesOrder", "BillingContactID", "Contacts.Contact");
            DropForeignKey("Ordering.SalesOrderAttribute", "MasterID", "Ordering.SalesOrder");
            DropForeignKey("Ordering.SalesOrderAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Invoicing.SalesOrderSalesInvoice", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Invoicing.SalesOrderSalesInvoice", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Invoicing.SalesInvoice", "UserID", "Contacts.User");
            DropForeignKey("Invoicing.SalesInvoice", "TypeID", "Invoicing.SalesInvoiceType");
            DropForeignKey("Invoicing.SalesInvoice", "StoreID", "Stores.Store");
            DropForeignKey("Invoicing.SalesInvoice", "StatusID", "Invoicing.SalesInvoiceStatus");
            DropForeignKey("Invoicing.SalesInvoice", "StateID", "Invoicing.SalesInvoiceState");
            DropForeignKey("Invoicing.SalesInvoice", "ShippingContactID", "Contacts.Contact");
            DropForeignKey("Invoicing.SalesInvoice", "ShipOptionID", "Shipping.ShipOption");
            DropForeignKey("Invoicing.SalesInvoiceItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Invoicing.SalesInvoiceItem", "UserID", "Contacts.User");
            DropForeignKey("Invoicing.SalesInvoiceItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Invoicing.SalesInvoiceItem", "StatusID", "Invoicing.SalesInvoiceItemStatus");
            DropForeignKey("Invoicing.SalesInvoiceItem", "ProductID", "Products.Product");
            DropForeignKey("Invoicing.SalesInvoiceItem", "MasterID", "Invoicing.SalesInvoice");
            DropForeignKey("Discounts.SalesInvoiceItemDiscounts", "MasterID", "Invoicing.SalesInvoiceItem");
            DropForeignKey("Discounts.SalesInvoiceItemDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Invoicing.SalesInvoiceItemAttribute", "MasterID", "Invoicing.SalesInvoiceItem");
            DropForeignKey("Invoicing.SalesInvoiceItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Payments.SalesInvoicePayment", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("Payments.SalesInvoicePayment", "PaymentID", "Payments.Payment");
            DropForeignKey("Discounts.SalesInvoiceDiscounts", "MasterID", "Invoicing.SalesInvoice");
            DropForeignKey("Discounts.SalesInvoiceDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountStores", "StoreID", "Stores.Store");
            DropForeignKey("Discounts.DiscountStores", "DiscountID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountProductType", "ProductTypeID", "Products.ProductType");
            DropForeignKey("Discounts.DiscountProductType", "DiscountID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountCode", "DiscountID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountCategories", "DiscountID", "Discounts.Discount");
            DropForeignKey("Discounts.DiscountCategories", "CategoryID", "Categories.Category");
            DropForeignKey("Categories.Category", "TypeID", "Categories.CategoryType");
            DropForeignKey("Products.ProductCategory", "ProductID", "Products.Product");
            DropForeignKey("Products.Product", "TypeID", "Products.ProductType");
            DropForeignKey("Products.ProductType", "StoreID", "Stores.Store");
            DropForeignKey("Products.ProductPricePoint", "StoreID", "Stores.Store");
            DropForeignKey("Products.ProductPricePoint", "ProductID", "Products.Product");
            DropForeignKey("Products.ProductPricePoint", "PriceRoundingID", "Pricing.PriceRounding");
            DropForeignKey("Products.ProductPricePoint", "PricePointID", "Pricing.PricePoint");
            DropForeignKey("Products.ProductImage", "ProductID", "Products.Product");
            DropForeignKey("Products.ProductImage", "LibraryID", "Media.Library");
            DropForeignKey("Products.ProductFile", "ProductID", "Products.Product");
            DropForeignKey("Products.ProductFile", "FileID", "Media.File");
            DropForeignKey("Products.ProductAttributeFilterValue", "MasterID", "Products.Product");
            DropForeignKey("Products.ProductAttributeFilterValue", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Products.ProductAssociation", "TypeID", "Products.ProductAssociationType");
            DropForeignKey("Products.ProductAssociation", "PrimaryProductID", "Products.Product");
            DropForeignKey("Products.ProductAssociationAttribute", "MasterID", "Products.ProductAssociation");
            DropForeignKey("Products.ProductAssociationAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Products.ProductAssociation", "AssociatedProductID", "Products.Product");
            DropForeignKey("Products.Product", "PalletID", "Shipping.Package");
            DropForeignKey("Products.Product", "PackageID", "Shipping.Package");
            DropForeignKey("Products.Product", "MasterPackID", "Shipping.Package");
            DropForeignKey("Shipping.Package", "TypeID", "Shipping.PackageType");
            DropForeignKey("Discounts.DiscountProducts", "ProductID", "Products.Product");
            DropForeignKey("Discounts.DiscountProducts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Shopping.CartItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Shopping.CartItem", "UserID", "Contacts.User");
            DropForeignKey("Shopping.CartItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Shopping.CartItem", "StatusID", "Shopping.CartItemStatus");
            DropForeignKey("Shopping.CartItem", "ProductID", "Products.Product");
            DropForeignKey("Shopping.CartItem", "MasterID", "Shopping.Cart");
            DropForeignKey("Shopping.Cart", "UserID", "Contacts.User");
            DropForeignKey("Shopping.Cart", "TypeID", "Shopping.CartType");
            DropForeignKey("Shopping.CartType", "StoreID", "Stores.Store");
            DropForeignKey("Shopping.CartType", "CreatedByUserID", "Contacts.User");
            DropForeignKey("Shopping.Cart", "StoreID", "Stores.Store");
            DropForeignKey("Shopping.Cart", "StatusID", "Shopping.CartStatus");
            DropForeignKey("Shopping.Cart", "StateID", "Shopping.CartState");
            DropForeignKey("Shopping.Cart", "ShippingContactID", "Contacts.Contact");
            DropForeignKey("Shopping.Cart", "ShippingAddressID", "Geography.Address");
            DropForeignKey("Shopping.Cart", "ShipOptionID", "Shipping.ShipOption");
            DropForeignKey("System.Note", "VendorID", "Vendors.Vendor");
            DropForeignKey("System.Note", "UserID", "Contacts.User");
            DropForeignKey("System.Note", "UpdatedByUserID", "Contacts.User");
            DropForeignKey("System.Note", "TypeID", "System.NoteType");
            DropForeignKey("System.Note", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("System.Note", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("System.Note", "SalesInvoiceID", "Invoicing.SalesInvoice");
            DropForeignKey("System.Note", "PurchaseOrderID", "Purchasing.PurchaseOrder");
            DropForeignKey("System.Note", "ManufacturerID", "Manufacturers.Manufacturer");
            DropForeignKey("System.Note", "CreatedByUserID", "Contacts.User");
            DropForeignKey("Payments.Wallet", "UserID", "Contacts.User");
            DropForeignKey("Contacts.User", "TypeID", "Contacts.UserType");
            DropForeignKey("Contacts.User", "StatusID", "Contacts.UserStatus");
            DropForeignKey("Quoting.SalesQuote", "UserID", "Contacts.User");
            DropForeignKey("Quoting.SalesQuote", "TypeID", "Quoting.SalesQuoteType");
            DropForeignKey("Quoting.SalesQuote", "StoreID", "Stores.Store");
            DropForeignKey("Quoting.SalesQuote", "StatusID", "Quoting.SalesQuoteStatus");
            DropForeignKey("Quoting.SalesQuote", "StateID", "Quoting.SalesQuoteState");
            DropForeignKey("Quoting.SalesQuote", "ShippingContactID", "Contacts.Contact");
            DropForeignKey("Quoting.SalesQuote", "ShipOptionID", "Shipping.ShipOption");
            DropForeignKey("Shipping.ShipOption", "ShipCarrierMethodID", "Shipping.ShipCarrierMethod");
            DropForeignKey("Quoting.SalesQuoteItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Quoting.SalesQuoteItem", "UserID", "Contacts.User");
            DropForeignKey("Quoting.SalesQuoteItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Quoting.SalesQuoteItem", "StatusID", "Quoting.SalesQuoteItemStatus");
            DropForeignKey("Quoting.SalesQuoteItem", "ProductID", "Products.Product");
            DropForeignKey("Quoting.SalesQuoteItem", "MasterID", "Quoting.SalesQuote");
            DropForeignKey("Discounts.QuoteItemDiscounts", "MasterID", "Quoting.SalesQuoteItem");
            DropForeignKey("Discounts.QuoteItemDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Quoting.SalesQuoteItemAttribute", "MasterID", "Quoting.SalesQuoteItem");
            DropForeignKey("Quoting.SalesQuoteItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Discounts.SalesQuoteDiscounts", "MasterID", "Quoting.SalesQuote");
            DropForeignKey("Discounts.SalesQuoteDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Quoting.SalesQuote", "BillingContactID", "Contacts.Contact");
            DropForeignKey("Quoting.SalesQuoteAttribute", "MasterID", "Quoting.SalesQuote");
            DropForeignKey("Quoting.SalesQuoteAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Quoting.SalesQuoteSalesOrder", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("Quoting.SalesQuoteSalesOrder", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Quoting.SalesQuote", "AccountID", "Accounts.Account");
            DropForeignKey("Contacts.RoleUser", "UserId", "Contacts.User");
            DropForeignKey("Contacts.RoleUser", "RoleId", "Contacts.UserRole");
            DropForeignKey("Contacts.RolePermission", "RoleId", "Contacts.UserRole");
            DropForeignKey("Contacts.RolePermission", "PermissionId", "Contacts.Permission");
            DropForeignKey("Contacts.User", "PreferredStoreID", "Stores.Store");
            DropForeignKey("Notifications.Notification", "UserID", "Contacts.User");
            DropForeignKey("Notifications.NotificationMessage", "UserID", "Contacts.User");
            DropForeignKey("Notifications.NotificationMessage", "AccountID", "Accounts.Account");
            DropForeignKey("Messaging.MessageAttachment", "UpdatedByUserID", "Contacts.User");
            DropForeignKey("Messaging.MessageAttachment", "MessageID", "Messaging.Message");
            DropForeignKey("Messaging.Message", "StoreID", "Stores.Store");
            DropForeignKey("Messaging.Message", "SentByUserID", "Contacts.User");
            DropForeignKey("Messaging.MessageRecipient", "ToUserID", "Contacts.User");
            DropForeignKey("Messaging.MessageRecipient", "MessageID", "Messaging.Message");
            DropForeignKey("Messaging.EmailQueue", "TypeID", "Messaging.EmailType");
            DropForeignKey("Messaging.EmailQueue", "StatusID", "Messaging.EmailStatus");
            DropForeignKey("Messaging.EmailQueue", "MessageRecipientID", "Messaging.MessageRecipient");
            DropForeignKey("Messaging.EmailQueue", "EmailTemplateID", "Messaging.EmailTemplate");
            DropForeignKey("Messaging.Message", "ConversationID", "Messaging.Conversation");
            DropForeignKey("Messaging.Conversation", "StoreID", "Stores.Store");
            DropForeignKey("Stores.Store", "TypeID", "Stores.StoreType");
            DropForeignKey("Stores.StoreUserType", "UserTypeID", "Contacts.UserType");
            DropForeignKey("Stores.StoreUserType", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreUser", "UserID", "Contacts.User");
            DropForeignKey("Stores.StoreUser", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreSubscription", "SubscriptionID", "Payments.Subscription");
            DropForeignKey("Payments.Subscription", "UserID", "Contacts.User");
            DropForeignKey("Payments.Subscription", "SubscriptionTypeID", "Payments.SubscriptionType");
            DropForeignKey("Products.ProductSubscriptionType", "SubscriptionTypeID", "Payments.SubscriptionType");
            DropForeignKey("Products.ProductSubscriptionType", "ProductID", "Products.Product");
            DropForeignKey("Payments.Subscription", "SubscriptionStatusID", "Payments.SubscriptionStatus");
            DropForeignKey("Payments.Subscription", "RepeatTypeID", "Payments.RepeatType");
            DropForeignKey("Payments.Subscription", "PaymentID", "Payments.Payment");
            DropForeignKey("Payments.Payment", "TypeID", "Payments.PaymentType");
            DropForeignKey("Payments.SubscriptionHistory", "SubscriptionID", "Payments.Subscription");
            DropForeignKey("Payments.SubscriptionHistory", "PaymentID", "Payments.Payment");
            DropForeignKey("Payments.Payment", "StoreID", "Stores.Store");
            DropForeignKey("Payments.Payment", "StatusID", "Payments.PaymentStatus");
            DropForeignKey("Payments.Payment", "PaymentMethodID", "Payments.PaymentMethod");
            DropForeignKey("Payments.Payment", "BillingContactID", "Contacts.Contact");
            DropForeignKey("Payments.Subscription", "AccountID", "Accounts.Account");
            DropForeignKey("Stores.StoreSubscription", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreImage", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreImage", "LibraryID", "Media.Library");
            DropForeignKey("Stores.StoreContact", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreContact", "ContactID", "Contacts.Contact");
            DropForeignKey("Stores.StoreCategoryType", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreCategoryType", "CategoryTypeID", "Categories.CategoryType");
            DropForeignKey("Categories.CategoryType", "ParentID", "Categories.CategoryType");
            DropForeignKey("Stores.StoreCategory", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreCategory", "CategoryID", "Categories.Category");
            DropForeignKey("Stores.StoreAccount", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreAccount", "PricePointID", "Pricing.PricePoint");
            DropForeignKey("Stores.StoreAccount", "AccountID", "Accounts.Account");
            DropForeignKey("Stores.Store", "SellerImageLibraryID", "Media.Library");
            DropForeignKey("Reviews.Review", "VendorID", "Vendors.Vendor");
            DropForeignKey("Reviews.Review", "UserID", "Contacts.User");
            DropForeignKey("Reviews.Review", "TypeID", "Reviews.ReviewType");
            DropForeignKey("Reviews.Review", "SubmittedByUserID", "Contacts.User");
            DropForeignKey("Reviews.Review", "StoreID", "Stores.Store");
            DropForeignKey("Reviews.Review", "ProductID", "Products.Product");
            DropForeignKey("Reviews.Review", "ManufacturerID", "Manufacturers.Manufacturer");
            DropForeignKey("Vendors.VendorManufacturer", "VendorID", "Vendors.Vendor");
            DropForeignKey("Vendors.Vendor", "TermID", "Vendors.Term");
            DropForeignKey("Vendors.Term", "VendorTermID", "Vendors.VendorTerm");
            DropForeignKey("Vendors.Term", "UpdatedByUserID", "Contacts.User");
            DropForeignKey("Vendors.Term", "CreatedByUserID", "Contacts.User");
            DropForeignKey("Stores.StoreVendor", "VendorID", "Vendors.Vendor");
            DropForeignKey("Stores.StoreVendor", "StoreID", "Stores.Store");
            DropForeignKey("Vendors.Vendor", "ShipViaID", "Vendors.ShipVia");
            DropForeignKey("Shipping.Shipment", "VendorID", "Vendors.Vendor");
            DropForeignKey("Shipping.Shipment", "TypeID", "Shipping.ShipmentType");
            DropForeignKey("Shipping.Shipment", "StatusID", "Shipping.ShipmentStatus");
            DropForeignKey("Shipping.ShipmentEvent", "ShipmentID", "Shipping.Shipment");
            DropForeignKey("Shipping.ShipmentEvent", "AddressID", "Geography.Address");
            DropForeignKey("Shipping.Shipment", "ShipCarrierMethodID", "Shipping.ShipCarrierMethod");
            DropForeignKey("Shipping.Shipment", "ShipCarrierID", "Shipping.ShipCarrier");
            DropForeignKey("Vendors.ShipVia", "ShipCarrierID", "Shipping.ShipCarrier");
            DropForeignKey("Shipping.ShipCarrierMethod", "ShipCarrierID", "Shipping.ShipCarrier");
            DropForeignKey("Shipping.CarrierOrigin", "ShipCarrierID", "Shipping.ShipCarrier");
            DropForeignKey("Shipping.CarrierInvoice", "ShipCarrierID", "Shipping.ShipCarrier");
            DropForeignKey("Shipping.ShipCarrier", "AddressID", "Geography.Address");
            DropForeignKey("Shipping.SalesOrderItemShipment", "ShipmentID", "Shipping.Shipment");
            DropForeignKey("Shipping.SalesOrderItemShipment", "SalesOrderItemID", "Ordering.SalesOrderItem");
            DropForeignKey("Ordering.SalesOrderItem", "VendorProductID", "Vendors.VendorProduct");
            DropForeignKey("Vendors.VendorProduct", "VendorID", "Vendors.Vendor");
            DropForeignKey("Vendors.VendorProduct", "ProductID", "Products.Product");
            DropForeignKey("Ordering.SalesOrderItem", "UserID", "Contacts.User");
            DropForeignKey("Ordering.SalesOrderItem", "StoreProductID", "Stores.StoreProduct");
            DropForeignKey("Stores.StoreProduct", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreProduct", "ProductID", "Products.Product");
            DropForeignKey("Ordering.SalesOrderItem", "StatusID", "Ordering.SalesOrderItemStatus");
            DropForeignKey("Ordering.SalesOrderItem", "ProductID", "Products.Product");
            DropForeignKey("Ordering.SalesOrderItem", "MasterID", "Ordering.SalesOrder");
            DropForeignKey("Discounts.SalesOrderItemDiscounts", "MasterID", "Ordering.SalesOrderItem");
            DropForeignKey("Discounts.SalesOrderItemDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Ordering.SalesOrderItemAttribute", "MasterID", "Ordering.SalesOrderItem");
            DropForeignKey("Ordering.SalesOrderItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Shipping.Shipment", "OriginContactID", "Contacts.Contact");
            DropForeignKey("Shipping.Shipment", "InventoryLocationSectionID", "Inventory.InventoryLocationSection");
            DropForeignKey("Products.ProductInventoryLocationSection", "ProductID", "Products.Product");
            DropForeignKey("Products.ProductInventoryLocationSection", "InventoryLocationSectionID", "Inventory.InventoryLocationSection");
            DropForeignKey("Inventory.InventoryLocationSection", "InventoryLocationID", "Inventory.InventoryLocation");
            DropForeignKey("Stores.StoreInventoryLocation", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreInventoryLocation", "InventoryLocationID", "Inventory.InventoryLocation");
            DropForeignKey("Inventory.InventoryLocation", "AddressID", "Geography.Address");
            DropForeignKey("Shipping.Shipment", "DestinationContactID", "Contacts.Contact");
            DropForeignKey("Vendors.Vendor", "ContactMethodID", "Contacts.ContactMethod");
            DropForeignKey("Vendors.Vendor", "ContactID", "Contacts.Contact");
            DropForeignKey("Vendors.Vendor", "AddressID", "Geography.Address");
            DropForeignKey("Vendors.VendorManufacturer", "ManufacturerID", "Manufacturers.Manufacturer");
            DropForeignKey("Stores.StoreManufacturer", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreManufacturer", "ManufacturerID", "Manufacturers.Manufacturer");
            DropForeignKey("Manufacturers.ManufacturerProduct", "ProductID", "Products.Product");
            DropForeignKey("Manufacturers.ManufacturerProduct", "ManufacturerID", "Manufacturers.Manufacturer");
            DropForeignKey("Manufacturers.Manufacturer", "AddressID", "Geography.Address");
            DropForeignKey("Reviews.Review", "CategoryID", "Categories.Category");
            DropForeignKey("Reviews.Review", "ApprovedByUserID", "Contacts.User");
            DropForeignKey("Stores.Store", "LogoImageLibraryID", "Media.Library");
            DropForeignKey("Stores.Store", "ContactID", "Contacts.Contact");
            DropForeignKey("Stores.BrandStore", "StoreID", "Stores.Store");
            DropForeignKey("Stores.BrandStore", "BrandID", "Stores.Brand");
            DropForeignKey("Stores.BrandSiteDomain", "SiteDomainID", "Stores.SiteDomain");
            DropForeignKey("Stores.StoreSiteDomain", "StoreID", "Stores.Store");
            DropForeignKey("Stores.StoreSiteDomain", "SiteDomainID", "Stores.SiteDomain");
            DropForeignKey("Stores.SiteDomainSocialProvider", "SocialProviderID", "Stores.SocialProvider");
            DropForeignKey("Stores.SiteDomainSocialProvider", "SiteDomainID", "Stores.SiteDomain");
            DropForeignKey("Stores.BrandSiteDomain", "BrandID", "Stores.Brand");
            DropForeignKey("Messaging.MessageAttachment", "LibraryID", "Media.Library");
            DropForeignKey("Messaging.MessageAttachment", "CreatedByUserID", "Contacts.User");
            DropForeignKey("Contacts.UserLogin", "UserId", "Contacts.User");
            DropForeignKey("Contacts.Favorite", "UserID", "Contacts.User");
            DropForeignKey("Contacts.Favorite", "ProductID", "Products.Product");
            DropForeignKey("Contacts.User", "ContactID", "Contacts.Contact");
            DropForeignKey("Contacts.UserClaim", "UserId", "Contacts.User");
            DropForeignKey("Contacts.UserAttribute", "MasterID", "Contacts.User");
            DropForeignKey("Contacts.UserAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Contacts.User", "AccountID", "Accounts.Account");
            DropForeignKey("System.Note", "CartID", "Shopping.Cart");
            DropForeignKey("System.Note", "AccountID", "Accounts.Account");
            DropForeignKey("Discounts.CartDiscounts", "MasterID", "Shopping.Cart");
            DropForeignKey("Discounts.CartDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Shopping.Cart", "BillingContactID", "Contacts.Contact");
            DropForeignKey("Shopping.CartAttribute", "MasterID", "Shopping.Cart");
            DropForeignKey("Shopping.CartAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Shopping.Cart", "AccountID", "Accounts.Account");
            DropForeignKey("Discounts.CartItemDiscounts", "MasterID", "Shopping.CartItem");
            DropForeignKey("Discounts.CartItemDiscounts", "DiscountID", "Discounts.Discount");
            DropForeignKey("Shopping.CartItemAttribute", "MasterID", "Shopping.CartItem");
            DropForeignKey("Shopping.CartItemAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Products.ProductAttribute", "MasterID", "Products.Product");
            DropForeignKey("Products.ProductAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Products.ProductCategory", "CategoryID", "Categories.Category");
            DropForeignKey("Products.ProductCategoryAttribute", "MasterID", "Products.ProductCategory");
            DropForeignKey("Products.ProductCategoryAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Categories.Category", "ParentID", "Categories.Category");
            DropForeignKey("Categories.CategoryImage", "LibraryID", "Media.Library");
            DropForeignKey("Categories.CategoryImage", "CategoryID", "Categories.Category");
            DropForeignKey("Categories.CategoryFile", "FileID", "Media.File");
            DropForeignKey("Media.FileData", "ID", "Media.File");
            DropForeignKey("Media.Audio", "ID", "Media.Library");
            DropForeignKey("Media.Video", "ID", "Media.Library");
            DropForeignKey("Media.Video", "FileID", "Media.File");
            DropForeignKey("Media.Library", "TypeID", "Media.LibraryType");
            DropForeignKey("Media.Image", "ThumbFileID", "Media.File");
            DropForeignKey("Media.Image", "ID", "Media.Library");
            DropForeignKey("Media.Image", "FullFileID", "Media.File");
            DropForeignKey("Media.Document", "ID", "Media.Library");
            DropForeignKey("Media.Document", "FileID", "Media.File");
            DropForeignKey("Media.Audio", "FullFileID", "Media.File");
            DropForeignKey("Media.Audio", "ClipFileID", "Media.File");
            DropForeignKey("Categories.CategoryFile", "CategoryID", "Categories.Category");
            DropForeignKey("Categories.CategoryAttribute", "MasterID", "Categories.Category");
            DropForeignKey("Categories.CategoryAttribute", "AttributeValueID", "Attributes.AttributeValue");
            DropForeignKey("Invoicing.SalesInvoice", "BillingContactID", "Contacts.Contact");
            DropForeignKey("Invoicing.SalesInvoiceAttribute", "MasterID", "Invoicing.SalesInvoice");
            DropForeignKey("Invoicing.SalesInvoiceAttribute", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Attributes.GeneralAttribute", "TypeID", "Attributes.AttributeType");
            DropForeignKey("Attributes.AttributeValue", "ParentID", "Attributes.AttributeValue");
            DropForeignKey("Attributes.AttributeValue", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Invoicing.SalesInvoice", "AccountID", "Accounts.Account");
            DropForeignKey("Ordering.SalesOrder", "AccountID", "Accounts.Account");
            DropForeignKey("Purchasing.SalesOrderPurchaseOrder", "PurchaseOrderID", "Purchasing.PurchaseOrder");
            DropForeignKey("Purchasing.PurchaseOrder", "AccountID", "Accounts.Account");
            DropForeignKey("Contacts.Contact", "AddressID", "Geography.Address");
            DropForeignKey("Geography.Address", "RegionID", "Geography.Region");
            DropForeignKey("Geography.Address", "DistrictID", "Geography.District");
            DropForeignKey("Geography.Address", "CountryID", "Geography.Country");
            DropForeignKey("Tax.TaxCountry", "CountryID", "Geography.Country");
            DropForeignKey("Tax.TaxRegion", "RegionID", "Geography.Region");
            DropForeignKey("Geography.InterRegion", "RelationRegionID", "Geography.Region");
            DropForeignKey("Geography.InterRegion", "KeyRegionID", "Geography.Region");
            DropForeignKey("Tax.TaxDistrict", "DistrictID", "Geography.District");
            DropForeignKey("Geography.District", "RegionID", "Geography.Region");
            DropForeignKey("Geography.Region", "CountryID", "Geography.Country");
            DropForeignKey("Accounts.AccountContact", "AccountID", "Accounts.Account");
            DropIndex("Geography.ZipCode", new[] { "ZipCode" });
            DropIndex("Geography.ZipCode", new[] { "Active" });
            DropIndex("Geography.ZipCode", new[] { "UpdatedDate" });
            DropIndex("Geography.ZipCode", new[] { "CustomKey" });
            DropIndex("Geography.ZipCode", new[] { "ID" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "Active" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "UpdatedDate" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "CustomKey" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "ID" });
            DropIndex("System.SettingType", new[] { "SortOrder" });
            DropIndex("System.SettingType", new[] { "DisplayName" });
            DropIndex("System.SettingType", new[] { "Name" });
            DropIndex("System.SettingType", new[] { "Active" });
            DropIndex("System.SettingType", new[] { "UpdatedDate" });
            DropIndex("System.SettingType", new[] { "CustomKey" });
            DropIndex("System.SettingType", new[] { "ID" });
            DropIndex("System.SettingGroup", new[] { "SortOrder" });
            DropIndex("System.SettingGroup", new[] { "DisplayName" });
            DropIndex("System.SettingGroup", new[] { "Name" });
            DropIndex("System.SettingGroup", new[] { "Active" });
            DropIndex("System.SettingGroup", new[] { "UpdatedDate" });
            DropIndex("System.SettingGroup", new[] { "CustomKey" });
            DropIndex("System.SettingGroup", new[] { "ID" });
            DropIndex("System.Setting", new[] { "SettingGroupID" });
            DropIndex("System.Setting", new[] { "TypeID" });
            DropIndex("System.Setting", new[] { "StoreID" });
            DropIndex("System.Setting", new[] { "Active" });
            DropIndex("System.Setting", new[] { "UpdatedDate" });
            DropIndex("System.Setting", new[] { "CustomKey" });
            DropIndex("System.Setting", new[] { "ID" });
            DropIndex("Hangfire.ScheduledJobConfigurationSetting", new[] { "SettingID" });
            DropIndex("Hangfire.ScheduledJobConfigurationSetting", new[] { "ScheduledJobConfigurationID" });
            DropIndex("Hangfire.ScheduledJobConfigurationSetting", new[] { "Active" });
            DropIndex("Hangfire.ScheduledJobConfigurationSetting", new[] { "UpdatedDate" });
            DropIndex("Hangfire.ScheduledJobConfigurationSetting", new[] { "CustomKey" });
            DropIndex("Hangfire.ScheduledJobConfigurationSetting", new[] { "ID" });
            DropIndex("Hangfire.ScheduledJobConfiguration", new[] { "NotificationTemplateID" });
            DropIndex("Hangfire.ScheduledJobConfiguration", new[] { "Name" });
            DropIndex("Hangfire.ScheduledJobConfiguration", new[] { "Active" });
            DropIndex("Hangfire.ScheduledJobConfiguration", new[] { "UpdatedDate" });
            DropIndex("Hangfire.ScheduledJobConfiguration", new[] { "CustomKey" });
            DropIndex("Hangfire.ScheduledJobConfiguration", new[] { "ID" });
            DropIndex("Reporting.ReportTypes", new[] { "SortOrder" });
            DropIndex("Reporting.ReportTypes", new[] { "DisplayName" });
            DropIndex("Reporting.ReportTypes", new[] { "Name" });
            DropIndex("Reporting.ReportTypes", new[] { "Active" });
            DropIndex("Reporting.ReportTypes", new[] { "UpdatedDate" });
            DropIndex("Reporting.ReportTypes", new[] { "CustomKey" });
            DropIndex("Reporting.ReportTypes", new[] { "ID" });
            DropIndex("Reporting.Reports", new[] { "TypeID" });
            DropIndex("Reporting.Reports", new[] { "RunByUserID" });
            DropIndex("Reporting.Reports", new[] { "Name" });
            DropIndex("Reporting.Reports", new[] { "Active" });
            DropIndex("Reporting.Reports", new[] { "UpdatedDate" });
            DropIndex("Reporting.Reports", new[] { "CustomKey" });
            DropIndex("Reporting.Reports", new[] { "ID" });
            DropIndex("Contacts.ProfanityFilter", new[] { "Active" });
            DropIndex("Contacts.ProfanityFilter", new[] { "UpdatedDate" });
            DropIndex("Contacts.ProfanityFilter", new[] { "CustomKey" });
            DropIndex("Contacts.ProfanityFilter", new[] { "ID" });
            DropIndex("Pricing.PriceRuleVendor", new[] { "VendorID" });
            DropIndex("Pricing.PriceRuleVendor", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleVendor", new[] { "Active" });
            DropIndex("Pricing.PriceRuleVendor", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleVendor", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleVendor", new[] { "ID" });
            DropIndex("Pricing.PriceRuleProductType", new[] { "ProductTypeID" });
            DropIndex("Pricing.PriceRuleProductType", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleProductType", new[] { "Active" });
            DropIndex("Pricing.PriceRuleProductType", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleProductType", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleProductType", new[] { "ID" });
            DropIndex("Pricing.PriceRuleProduct", new[] { "ProductID" });
            DropIndex("Pricing.PriceRuleProduct", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleProduct", new[] { "Active" });
            DropIndex("Pricing.PriceRuleProduct", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleProduct", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleProduct", new[] { "ID" });
            DropIndex("Pricing.PriceRuleCategory", new[] { "CategoryID" });
            DropIndex("Pricing.PriceRuleCategory", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleCategory", new[] { "Active" });
            DropIndex("Pricing.PriceRuleCategory", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleCategory", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleCategory", new[] { "ID" });
            DropIndex("Pricing.PriceRuleAccountType", new[] { "AccountTypeID" });
            DropIndex("Pricing.PriceRuleAccountType", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleAccountType", new[] { "Active" });
            DropIndex("Pricing.PriceRuleAccountType", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleAccountType", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleAccountType", new[] { "ID" });
            DropIndex("Pricing.PriceRule", new[] { "Name" });
            DropIndex("Pricing.PriceRule", new[] { "Active" });
            DropIndex("Pricing.PriceRule", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRule", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRule", new[] { "ID" });
            DropIndex("Pricing.PriceRuleAccount", new[] { "AccountID" });
            DropIndex("Pricing.PriceRuleAccount", new[] { "PriceRuleID" });
            DropIndex("Pricing.PriceRuleAccount", new[] { "Active" });
            DropIndex("Pricing.PriceRuleAccount", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRuleAccount", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRuleAccount", new[] { "ID" });
            DropIndex("Notifications.Action", new[] { "Active" });
            DropIndex("Notifications.Action", new[] { "UpdatedDate" });
            DropIndex("Notifications.Action", new[] { "CustomKey" });
            DropIndex("Notifications.Action", new[] { "ID" });
            DropIndex("Hangfire.Set", new[] { "ExpireAt" });
            DropIndex("Hangfire.Set", "UX_HangFire_Set_KeyAndValue");
            DropIndex("Hangfire.Set", new[] { "Key" });
            DropIndex("Hangfire.Set", new[] { "Id" });
            DropIndex("Hangfire.Server", new[] { "Id" });
            DropIndex("Hangfire.List", new[] { "ExpireAt" });
            DropIndex("Hangfire.List", new[] { "Key" });
            DropIndex("Hangfire.List", new[] { "Id" });
            DropIndex("Hangfire.JobQueue", new[] { "JobId" });
            DropIndex("Hangfire.JobQueue", "IX_HangFire_JobQueue_QueueAndFetchedAt");
            DropIndex("Hangfire.JobQueue", new[] { "Id" });
            DropIndex("Hangfire.State", new[] { "JobId" });
            DropIndex("Hangfire.State", new[] { "Id" });
            DropIndex("Hangfire.Job", new[] { "ExpireAt" });
            DropIndex("Hangfire.Job", new[] { "StateName" });
            DropIndex("Hangfire.Job", new[] { "Id" });
            DropIndex("Hangfire.JobParameter", new[] { "JobId" });
            DropIndex("Hangfire.JobParameter", "IX_HangFire_JobParameter_JobIdAndName");
            DropIndex("Hangfire.JobParameter", new[] { "Id" });
            DropIndex("Hangfire.Hash", "IX_HangFire_Hash_ExpireAt");
            DropIndex("Hangfire.Hash", "UX_HangFire_Hash_Key_Field");
            DropIndex("Hangfire.Hash", "IX_HangFire_Hash_Key");
            DropIndex("Hangfire.Hash", new[] { "Id" });
            DropIndex("Hangfire.Counter", new[] { "Id" });
            DropIndex("Hangfire.AggregatedCounter", new[] { "Id" });
            DropIndex("Tracking.VisitStatus", new[] { "SortOrder" });
            DropIndex("Tracking.VisitStatus", new[] { "DisplayName" });
            DropIndex("Tracking.VisitStatus", new[] { "Name" });
            DropIndex("Tracking.VisitStatus", new[] { "Active" });
            DropIndex("Tracking.VisitStatus", new[] { "UpdatedDate" });
            DropIndex("Tracking.VisitStatus", new[] { "CustomKey" });
            DropIndex("Tracking.Visit", new[] { "VisitorID" });
            DropIndex("Tracking.Visit", new[] { "SiteDomainID" });
            DropIndex("Tracking.Visit", new[] { "ContactID" });
            DropIndex("Tracking.Visit", new[] { "CampaignID" });
            DropIndex("Tracking.Visit", new[] { "UserID" });
            DropIndex("Tracking.Visit", new[] { "IPOrganizationID" });
            DropIndex("Tracking.Visit", new[] { "AddressID" });
            DropIndex("Tracking.Visit", new[] { "StatusID" });
            DropIndex("Tracking.Visit", new[] { "Name" });
            DropIndex("Tracking.Visit", new[] { "Active" });
            DropIndex("Tracking.Visit", new[] { "UpdatedDate" });
            DropIndex("Tracking.Visit", new[] { "CustomKey" });
            DropIndex("Tracking.EventType", new[] { "SortOrder" });
            DropIndex("Tracking.EventType", new[] { "DisplayName" });
            DropIndex("Tracking.EventType", new[] { "Name" });
            DropIndex("Tracking.EventType", new[] { "Active" });
            DropIndex("Tracking.EventType", new[] { "UpdatedDate" });
            DropIndex("Tracking.EventType", new[] { "CustomKey" });
            DropIndex("Tracking.EventStatus", new[] { "SortOrder" });
            DropIndex("Tracking.EventStatus", new[] { "DisplayName" });
            DropIndex("Tracking.EventStatus", new[] { "Name" });
            DropIndex("Tracking.EventStatus", new[] { "Active" });
            DropIndex("Tracking.EventStatus", new[] { "UpdatedDate" });
            DropIndex("Tracking.EventStatus", new[] { "CustomKey" });
            DropIndex("Tracking.Visitor", new[] { "UserID" });
            DropIndex("Tracking.Visitor", new[] { "IPOrganizationID" });
            DropIndex("Tracking.Visitor", new[] { "AddressID" });
            DropIndex("Tracking.Visitor", new[] { "Name" });
            DropIndex("Tracking.Visitor", new[] { "Active" });
            DropIndex("Tracking.Visitor", new[] { "UpdatedDate" });
            DropIndex("Tracking.Visitor", new[] { "CustomKey" });
            DropIndex("Tracking.PageViewType", new[] { "SortOrder" });
            DropIndex("Tracking.PageViewType", new[] { "DisplayName" });
            DropIndex("Tracking.PageViewType", new[] { "Name" });
            DropIndex("Tracking.PageViewType", new[] { "Active" });
            DropIndex("Tracking.PageViewType", new[] { "UpdatedDate" });
            DropIndex("Tracking.PageViewType", new[] { "CustomKey" });
            DropIndex("Tracking.PageViewStatus", new[] { "SortOrder" });
            DropIndex("Tracking.PageViewStatus", new[] { "DisplayName" });
            DropIndex("Tracking.PageViewStatus", new[] { "Name" });
            DropIndex("Tracking.PageViewStatus", new[] { "Active" });
            DropIndex("Tracking.PageViewStatus", new[] { "UpdatedDate" });
            DropIndex("Tracking.PageViewStatus", new[] { "CustomKey" });
            DropIndex("Tracking.PageView", new[] { "ProductID" });
            DropIndex("Tracking.PageView", new[] { "CategoryID" });
            DropIndex("Tracking.PageView", new[] { "VisitorID" });
            DropIndex("Tracking.PageView", new[] { "SiteDomainID" });
            DropIndex("Tracking.PageView", new[] { "ContactID" });
            DropIndex("Tracking.PageView", new[] { "CampaignID" });
            DropIndex("Tracking.PageView", new[] { "UserID" });
            DropIndex("Tracking.PageView", new[] { "IPOrganizationID" });
            DropIndex("Tracking.PageView", new[] { "AddressID" });
            DropIndex("Tracking.PageView", new[] { "StatusID" });
            DropIndex("Tracking.PageView", new[] { "TypeID" });
            DropIndex("Tracking.PageView", new[] { "Name" });
            DropIndex("Tracking.PageView", new[] { "Active" });
            DropIndex("Tracking.PageView", new[] { "UpdatedDate" });
            DropIndex("Tracking.PageView", new[] { "CustomKey" });
            DropIndex("Tracking.PageViewEvent", new[] { "PageViewID" });
            DropIndex("Tracking.PageViewEvent", new[] { "EventID" });
            DropIndex("Tracking.PageViewEvent", new[] { "Active" });
            DropIndex("Tracking.PageViewEvent", new[] { "UpdatedDate" });
            DropIndex("Tracking.PageViewEvent", new[] { "CustomKey" });
            DropIndex("Tracking.IPOrganizationStatus", new[] { "SortOrder" });
            DropIndex("Tracking.IPOrganizationStatus", new[] { "DisplayName" });
            DropIndex("Tracking.IPOrganizationStatus", new[] { "Name" });
            DropIndex("Tracking.IPOrganizationStatus", new[] { "Active" });
            DropIndex("Tracking.IPOrganizationStatus", new[] { "UpdatedDate" });
            DropIndex("Tracking.IPOrganizationStatus", new[] { "CustomKey" });
            DropIndex("Tracking.IPOrganization", new[] { "PrimaryUserID" });
            DropIndex("Tracking.IPOrganization", new[] { "AddressID" });
            DropIndex("Tracking.IPOrganization", new[] { "StatusID" });
            DropIndex("Tracking.IPOrganization", new[] { "Name" });
            DropIndex("Tracking.IPOrganization", new[] { "Active" });
            DropIndex("Tracking.IPOrganization", new[] { "UpdatedDate" });
            DropIndex("Tracking.IPOrganization", new[] { "CustomKey" });
            DropIndex("Tracking.Event", new[] { "VisitID" });
            DropIndex("Tracking.Event", new[] { "VisitorID" });
            DropIndex("Tracking.Event", new[] { "SiteDomainID" });
            DropIndex("Tracking.Event", new[] { "ContactID" });
            DropIndex("Tracking.Event", new[] { "CampaignID" });
            DropIndex("Tracking.Event", new[] { "UserID" });
            DropIndex("Tracking.Event", new[] { "IPOrganizationID" });
            DropIndex("Tracking.Event", new[] { "AddressID" });
            DropIndex("Tracking.Event", new[] { "StatusID" });
            DropIndex("Tracking.Event", new[] { "TypeID" });
            DropIndex("Tracking.Event", new[] { "Name" });
            DropIndex("Tracking.Event", new[] { "Active" });
            DropIndex("Tracking.Event", new[] { "UpdatedDate" });
            DropIndex("Tracking.Event", new[] { "CustomKey" });
            DropIndex("System.SystemLog", new[] { "StoreID" });
            DropIndex("System.SystemLog", new[] { "Name" });
            DropIndex("System.SystemLog", new[] { "Active" });
            DropIndex("System.SystemLog", new[] { "UpdatedDate" });
            DropIndex("System.SystemLog", new[] { "CustomKey" });
            DropIndex("System.SystemLog", new[] { "ID" });
            DropIndex("Shopping.Checkout", new[] { "Active" });
            DropIndex("Shopping.Checkout", new[] { "UpdatedDate" });
            DropIndex("Shopping.Checkout", new[] { "CustomKey" });
            DropIndex("Shopping.Checkout", new[] { "ID" });
            DropIndex("Advertising.AdType", new[] { "SortOrder" });
            DropIndex("Advertising.AdType", new[] { "DisplayName" });
            DropIndex("Advertising.AdType", new[] { "Name" });
            DropIndex("Advertising.AdType", new[] { "Active" });
            DropIndex("Advertising.AdType", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdType", new[] { "CustomKey" });
            DropIndex("Advertising.AdStatus", new[] { "SortOrder" });
            DropIndex("Advertising.AdStatus", new[] { "DisplayName" });
            DropIndex("Advertising.AdStatus", new[] { "Name" });
            DropIndex("Advertising.AdStatus", new[] { "Active" });
            DropIndex("Advertising.AdStatus", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdStatus", new[] { "CustomKey" });
            DropIndex("Tracking.CampaignType", new[] { "StoreID" });
            DropIndex("Tracking.CampaignType", new[] { "SortOrder" });
            DropIndex("Tracking.CampaignType", new[] { "DisplayName" });
            DropIndex("Tracking.CampaignType", new[] { "Name" });
            DropIndex("Tracking.CampaignType", new[] { "Active" });
            DropIndex("Tracking.CampaignType", new[] { "UpdatedDate" });
            DropIndex("Tracking.CampaignType", new[] { "CustomKey" });
            DropIndex("Tracking.CampaignStatus", new[] { "SortOrder" });
            DropIndex("Tracking.CampaignStatus", new[] { "DisplayName" });
            DropIndex("Tracking.CampaignStatus", new[] { "Name" });
            DropIndex("Tracking.CampaignStatus", new[] { "Active" });
            DropIndex("Tracking.CampaignStatus", new[] { "UpdatedDate" });
            DropIndex("Tracking.CampaignStatus", new[] { "CustomKey" });
            DropIndex("Tracking.Campaign", new[] { "CreatedByUserID" });
            DropIndex("Tracking.Campaign", new[] { "StatusID" });
            DropIndex("Tracking.Campaign", new[] { "TypeID" });
            DropIndex("Tracking.Campaign", new[] { "Name" });
            DropIndex("Tracking.Campaign", new[] { "Active" });
            DropIndex("Tracking.Campaign", new[] { "UpdatedDate" });
            DropIndex("Tracking.Campaign", new[] { "CustomKey" });
            DropIndex("Tracking.CampaignAd", new[] { "AdID" });
            DropIndex("Tracking.CampaignAd", new[] { "CampaignID" });
            DropIndex("Tracking.CampaignAd", new[] { "Active" });
            DropIndex("Tracking.CampaignAd", new[] { "UpdatedDate" });
            DropIndex("Tracking.CampaignAd", new[] { "CustomKey" });
            DropIndex("Advertising.ZoneType", new[] { "SortOrder" });
            DropIndex("Advertising.ZoneType", new[] { "DisplayName" });
            DropIndex("Advertising.ZoneType", new[] { "Name" });
            DropIndex("Advertising.ZoneType", new[] { "Active" });
            DropIndex("Advertising.ZoneType", new[] { "UpdatedDate" });
            DropIndex("Advertising.ZoneType", new[] { "CustomKey" });
            DropIndex("Advertising.ZoneStatus", new[] { "SortOrder" });
            DropIndex("Advertising.ZoneStatus", new[] { "DisplayName" });
            DropIndex("Advertising.ZoneStatus", new[] { "Name" });
            DropIndex("Advertising.ZoneStatus", new[] { "Active" });
            DropIndex("Advertising.ZoneStatus", new[] { "UpdatedDate" });
            DropIndex("Advertising.ZoneStatus", new[] { "CustomKey" });
            DropIndex("Advertising.Zone", new[] { "StatusID" });
            DropIndex("Advertising.Zone", new[] { "TypeID" });
            DropIndex("Advertising.Zone", new[] { "Name" });
            DropIndex("Advertising.Zone", new[] { "Active" });
            DropIndex("Advertising.Zone", new[] { "UpdatedDate" });
            DropIndex("Advertising.Zone", new[] { "CustomKey" });
            DropIndex("Counters.CounterType", new[] { "SortOrder" });
            DropIndex("Counters.CounterType", new[] { "DisplayName" });
            DropIndex("Counters.CounterType", new[] { "Name" });
            DropIndex("Counters.CounterType", new[] { "Active" });
            DropIndex("Counters.CounterType", new[] { "UpdatedDate" });
            DropIndex("Counters.CounterType", new[] { "CustomKey" });
            DropIndex("Counters.CounterLogType", new[] { "SortOrder" });
            DropIndex("Counters.CounterLogType", new[] { "DisplayName" });
            DropIndex("Counters.CounterLogType", new[] { "Name" });
            DropIndex("Counters.CounterLogType", new[] { "Active" });
            DropIndex("Counters.CounterLogType", new[] { "UpdatedDate" });
            DropIndex("Counters.CounterLogType", new[] { "CustomKey" });
            DropIndex("Counters.CounterLog", new[] { "CounterID" });
            DropIndex("Counters.CounterLog", new[] { "TypeID" });
            DropIndex("Counters.CounterLog", new[] { "Active" });
            DropIndex("Counters.CounterLog", new[] { "UpdatedDate" });
            DropIndex("Counters.CounterLog", new[] { "CustomKey" });
            DropIndex("Counters.Counter", new[] { "TypeID" });
            DropIndex("Counters.Counter", new[] { "Active" });
            DropIndex("Counters.Counter", new[] { "UpdatedDate" });
            DropIndex("Counters.Counter", new[] { "CustomKey" });
            DropIndex("Advertising.AdZoneAccess", new[] { "ClickCounterID" });
            DropIndex("Advertising.AdZoneAccess", new[] { "ImpressionCounterID" });
            DropIndex("Advertising.AdZoneAccess", new[] { "SubscriptionID" });
            DropIndex("Advertising.AdZoneAccess", new[] { "ZoneID" });
            DropIndex("Advertising.AdZoneAccess", new[] { "Name" });
            DropIndex("Advertising.AdZoneAccess", new[] { "Active" });
            DropIndex("Advertising.AdZoneAccess", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdZoneAccess", new[] { "CustomKey" });
            DropIndex("Advertising.AdZone", new[] { "ClickCounterID" });
            DropIndex("Advertising.AdZone", new[] { "ImpressionCounterID" });
            DropIndex("Advertising.AdZone", new[] { "AdZoneAccessID" });
            DropIndex("Advertising.AdZone", new[] { "ZoneID" });
            DropIndex("Advertising.AdZone", new[] { "AdID" });
            DropIndex("Advertising.AdZone", new[] { "Active" });
            DropIndex("Advertising.AdZone", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdZone", new[] { "CustomKey" });
            DropIndex("Advertising.AdStore", new[] { "StoreID" });
            DropIndex("Advertising.AdStore", new[] { "AdID" });
            DropIndex("Advertising.AdStore", new[] { "Active" });
            DropIndex("Advertising.AdStore", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdStore", new[] { "CustomKey" });
            DropIndex("Advertising.AdImage", new[] { "LibraryID" });
            DropIndex("Advertising.AdImage", new[] { "AdID" });
            DropIndex("Advertising.AdImage", new[] { "Active" });
            DropIndex("Advertising.AdImage", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdImage", new[] { "CustomKey" });
            DropIndex("Advertising.Ad", new[] { "ClickCounterID" });
            DropIndex("Advertising.Ad", new[] { "ImpressionCounterID" });
            DropIndex("Advertising.Ad", new[] { "StatusID" });
            DropIndex("Advertising.Ad", new[] { "TypeID" });
            DropIndex("Advertising.Ad", new[] { "Name" });
            DropIndex("Advertising.Ad", new[] { "Active" });
            DropIndex("Advertising.Ad", new[] { "UpdatedDate" });
            DropIndex("Advertising.Ad", new[] { "CustomKey" });
            DropIndex("Advertising.AdAccount", new[] { "AccountID" });
            DropIndex("Advertising.AdAccount", new[] { "AdID" });
            DropIndex("Advertising.AdAccount", new[] { "Active" });
            DropIndex("Advertising.AdAccount", new[] { "UpdatedDate" });
            DropIndex("Advertising.AdAccount", new[] { "CustomKey" });
            DropIndex("Accounts.AccountType", new[] { "StoreID" });
            DropIndex("Accounts.AccountType", new[] { "SortOrder" });
            DropIndex("Accounts.AccountType", new[] { "DisplayName" });
            DropIndex("Accounts.AccountType", new[] { "Name" });
            DropIndex("Accounts.AccountType", new[] { "Active" });
            DropIndex("Accounts.AccountType", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountType", new[] { "CustomKey" });
            DropIndex("Accounts.AccountType", new[] { "ID" });
            DropIndex("Accounts.AccountStatus", new[] { "SortOrder" });
            DropIndex("Accounts.AccountStatus", new[] { "DisplayName" });
            DropIndex("Accounts.AccountStatus", new[] { "Name" });
            DropIndex("Accounts.AccountStatus", new[] { "Active" });
            DropIndex("Accounts.AccountStatus", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountStatus", new[] { "CustomKey" });
            DropIndex("Accounts.AccountStatus", new[] { "ID" });
            DropIndex("Contacts.Opportunities", new[] { "OFTDeliveryContactID" });
            DropIndex("Contacts.Opportunities", new[] { "ClientProjectContactID" });
            DropIndex("Contacts.Opportunities", new[] { "PrimaryContactID" });
            DropIndex("Contacts.Opportunities", new[] { "AccountID" });
            DropIndex("Contacts.Opportunities", new[] { "Active" });
            DropIndex("Contacts.Opportunities", new[] { "UpdatedDate" });
            DropIndex("Contacts.Opportunities", new[] { "CustomKey" });
            DropIndex("Contacts.Opportunities", new[] { "ID" });
            DropIndex("Accounts.AccountAttribute", new[] { "MasterID" });
            DropIndex("Accounts.AccountAttribute", new[] { "AttributeValueID" });
            DropIndex("Accounts.AccountAttribute", new[] { "Active" });
            DropIndex("Accounts.AccountAttribute", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountAttribute", new[] { "CustomKey" });
            DropIndex("Accounts.AccountAttribute", new[] { "ID" });
            DropIndex("Accounts.AccountTerm", new[] { "Name" });
            DropIndex("Accounts.AccountTerm", new[] { "Active" });
            DropIndex("Accounts.AccountTerm", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountTerm", new[] { "CustomKey" });
            DropIndex("Accounts.AccountTerm", new[] { "ID" });
            DropIndex("Accounts.AccountPricePoint", new[] { "PricePointID" });
            DropIndex("Accounts.AccountPricePoint", new[] { "AccountID" });
            DropIndex("Accounts.AccountPricePoint", new[] { "Active" });
            DropIndex("Accounts.AccountPricePoint", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountPricePoint", new[] { "CustomKey" });
            DropIndex("Accounts.AccountPricePoint", new[] { "ID" });
            DropIndex("Contacts.ContactType", new[] { "SortOrder" });
            DropIndex("Contacts.ContactType", new[] { "DisplayName" });
            DropIndex("Contacts.ContactType", new[] { "Name" });
            DropIndex("Contacts.ContactType", new[] { "Active" });
            DropIndex("Contacts.ContactType", new[] { "UpdatedDate" });
            DropIndex("Contacts.ContactType", new[] { "CustomKey" });
            DropIndex("Contacts.ContactType", new[] { "ID" });
            DropIndex("Contacts.Individual", new[] { "Active" });
            DropIndex("Contacts.Individual", new[] { "UpdatedDate" });
            DropIndex("Contacts.Individual", new[] { "CustomKey" });
            DropIndex("Contacts.Individual", new[] { "ID" });
            DropIndex("Contacts.Customer", new[] { "Active" });
            DropIndex("Contacts.Customer", new[] { "UpdatedDate" });
            DropIndex("Contacts.Customer", new[] { "CustomKey" });
            DropIndex("Contacts.Customer", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderType", new[] { "SortOrder" });
            DropIndex("Purchasing.PurchaseOrderType", new[] { "DisplayName" });
            DropIndex("Purchasing.PurchaseOrderType", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderType", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderType", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderType", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderType", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderStatus", new[] { "SortOrder" });
            DropIndex("Purchasing.PurchaseOrderStatus", new[] { "DisplayName" });
            DropIndex("Purchasing.PurchaseOrderStatus", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderStatus", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderStatus", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderStatus", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderStatus", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderState", new[] { "SortOrder" });
            DropIndex("Purchasing.PurchaseOrderState", new[] { "DisplayName" });
            DropIndex("Purchasing.PurchaseOrderState", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderState", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderState", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderState", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderState", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "SortOrder" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "DisplayName" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderItemStatus", new[] { "ID" });
            DropIndex("Discounts.PurchaseOrderItemDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.PurchaseOrderItemDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.PurchaseOrderItemDiscounts", new[] { "Active" });
            DropIndex("Discounts.PurchaseOrderItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.PurchaseOrderItemDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.PurchaseOrderItemDiscounts", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "MasterID" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "AttributeID" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderItemAttribute", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "VendorProductID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "StoreProductID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "StatusID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "MasterID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "UserID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "ProductID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "Name" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "ID" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "Name" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "Active" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "UpdatedDate" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "CustomKey" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "ID" });
            DropIndex("Discounts.PurchaseOrderDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.PurchaseOrderDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.PurchaseOrderDiscounts", new[] { "Active" });
            DropIndex("Discounts.PurchaseOrderDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.PurchaseOrderDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.PurchaseOrderDiscounts", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "MasterID" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "AttributeID" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderAttribute", new[] { "ID" });
            DropIndex("Ordering.SalesOrderType", new[] { "SortOrder" });
            DropIndex("Ordering.SalesOrderType", new[] { "DisplayName" });
            DropIndex("Ordering.SalesOrderType", new[] { "Name" });
            DropIndex("Ordering.SalesOrderType", new[] { "Active" });
            DropIndex("Ordering.SalesOrderType", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderType", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderType", new[] { "ID" });
            DropIndex("Ordering.SalesOrderStatus", new[] { "SortOrder" });
            DropIndex("Ordering.SalesOrderStatus", new[] { "DisplayName" });
            DropIndex("Ordering.SalesOrderStatus", new[] { "Name" });
            DropIndex("Ordering.SalesOrderStatus", new[] { "Active" });
            DropIndex("Ordering.SalesOrderStatus", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderStatus", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderStatus", new[] { "ID" });
            DropIndex("Ordering.SalesOrderState", new[] { "SortOrder" });
            DropIndex("Ordering.SalesOrderState", new[] { "DisplayName" });
            DropIndex("Ordering.SalesOrderState", new[] { "Name" });
            DropIndex("Ordering.SalesOrderState", new[] { "Active" });
            DropIndex("Ordering.SalesOrderState", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderState", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderState", new[] { "ID" });
            DropIndex("Payments.SalesOrderPayment", new[] { "PaymentID" });
            DropIndex("Payments.SalesOrderPayment", new[] { "SalesOrderID" });
            DropIndex("Payments.SalesOrderPayment", new[] { "Active" });
            DropIndex("Payments.SalesOrderPayment", new[] { "UpdatedDate" });
            DropIndex("Payments.SalesOrderPayment", new[] { "CustomKey" });
            DropIndex("Payments.SalesOrderPayment", new[] { "ID" });
            DropIndex("Discounts.SalesOrderDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.SalesOrderDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.SalesOrderDiscounts", new[] { "Active" });
            DropIndex("Discounts.SalesOrderDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.SalesOrderDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.SalesOrderDiscounts", new[] { "ID" });
            DropIndex("Contacts.CustomerPriority", new[] { "SortOrder" });
            DropIndex("Contacts.CustomerPriority", new[] { "DisplayName" });
            DropIndex("Contacts.CustomerPriority", new[] { "Name" });
            DropIndex("Contacts.CustomerPriority", new[] { "Active" });
            DropIndex("Contacts.CustomerPriority", new[] { "UpdatedDate" });
            DropIndex("Contacts.CustomerPriority", new[] { "CustomKey" });
            DropIndex("Contacts.CustomerPriority", new[] { "ID" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "MasterID" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "AttributeID" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "Active" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderAttribute", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceType", new[] { "SortOrder" });
            DropIndex("Invoicing.SalesInvoiceType", new[] { "DisplayName" });
            DropIndex("Invoicing.SalesInvoiceType", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceType", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceType", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceType", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceType", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceStatus", new[] { "SortOrder" });
            DropIndex("Invoicing.SalesInvoiceStatus", new[] { "DisplayName" });
            DropIndex("Invoicing.SalesInvoiceStatus", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceStatus", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceStatus", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceStatus", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceStatus", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceState", new[] { "SortOrder" });
            DropIndex("Invoicing.SalesInvoiceState", new[] { "DisplayName" });
            DropIndex("Invoicing.SalesInvoiceState", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceState", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceState", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceState", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceState", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "SortOrder" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "DisplayName" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceItemStatus", new[] { "ID" });
            DropIndex("Discounts.SalesInvoiceItemDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.SalesInvoiceItemDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.SalesInvoiceItemDiscounts", new[] { "Active" });
            DropIndex("Discounts.SalesInvoiceItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.SalesInvoiceItemDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.SalesInvoiceItemDiscounts", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "MasterID" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "AttributeID" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceItemAttribute", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "StoreProductID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "VendorProductID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "StatusID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "MasterID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "UserID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "ProductID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "Name" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "ID" });
            DropIndex("Payments.SalesInvoicePayment", new[] { "PaymentID" });
            DropIndex("Payments.SalesInvoicePayment", new[] { "SalesInvoiceID" });
            DropIndex("Payments.SalesInvoicePayment", new[] { "Active" });
            DropIndex("Payments.SalesInvoicePayment", new[] { "UpdatedDate" });
            DropIndex("Payments.SalesInvoicePayment", new[] { "CustomKey" });
            DropIndex("Payments.SalesInvoicePayment", new[] { "ID" });
            DropIndex("Discounts.DiscountStores", new[] { "StoreID" });
            DropIndex("Discounts.DiscountStores", new[] { "DiscountID" });
            DropIndex("Discounts.DiscountStores", new[] { "Active" });
            DropIndex("Discounts.DiscountStores", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountStores", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountStores", new[] { "ID" });
            DropIndex("Discounts.DiscountProductType", new[] { "ProductTypeID" });
            DropIndex("Discounts.DiscountProductType", new[] { "DiscountID" });
            DropIndex("Discounts.DiscountProductType", new[] { "Active" });
            DropIndex("Discounts.DiscountProductType", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountProductType", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountProductType", new[] { "ID" });
            DropIndex("Discounts.DiscountCode", new[] { "DiscountID" });
            DropIndex("Discounts.DiscountCode", new[] { "Active" });
            DropIndex("Discounts.DiscountCode", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountCode", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountCode", new[] { "ID" });
            DropIndex("Products.ProductType", new[] { "StoreID" });
            DropIndex("Products.ProductType", new[] { "SortOrder" });
            DropIndex("Products.ProductType", new[] { "DisplayName" });
            DropIndex("Products.ProductType", new[] { "Name" });
            DropIndex("Products.ProductType", new[] { "Active" });
            DropIndex("Products.ProductType", new[] { "UpdatedDate" });
            DropIndex("Products.ProductType", new[] { "CustomKey" });
            DropIndex("Products.ProductType", new[] { "ID" });
            DropIndex("Pricing.PriceRounding", new[] { "Active" });
            DropIndex("Pricing.PriceRounding", new[] { "UpdatedDate" });
            DropIndex("Pricing.PriceRounding", new[] { "CustomKey" });
            DropIndex("Pricing.PriceRounding", new[] { "ID" });
            DropIndex("Products.ProductPricePoint", new[] { "PriceRoundingID" });
            DropIndex("Products.ProductPricePoint", new[] { "ProductID" });
            DropIndex("Products.ProductPricePoint", new[] { "PricePointID" });
            DropIndex("Products.ProductPricePoint", new[] { "StoreID" });
            DropIndex("Products.ProductPricePoint", new[] { "Active" });
            DropIndex("Products.ProductPricePoint", new[] { "UpdatedDate" });
            DropIndex("Products.ProductPricePoint", new[] { "CustomKey" });
            DropIndex("Products.ProductPricePoint", new[] { "ID" });
            DropIndex("Products.ProductImage", new[] { "LibraryID" });
            DropIndex("Products.ProductImage", new[] { "ProductID" });
            DropIndex("Products.ProductImage", new[] { "Active" });
            DropIndex("Products.ProductImage", new[] { "UpdatedDate" });
            DropIndex("Products.ProductImage", new[] { "CustomKey" });
            DropIndex("Products.ProductImage", new[] { "ID" });
            DropIndex("Products.ProductFile", new[] { "FileID" });
            DropIndex("Products.ProductFile", new[] { "ProductID" });
            DropIndex("Products.ProductFile", new[] { "Name" });
            DropIndex("Products.ProductFile", new[] { "Active" });
            DropIndex("Products.ProductFile", new[] { "UpdatedDate" });
            DropIndex("Products.ProductFile", new[] { "CustomKey" });
            DropIndex("Products.ProductFile", new[] { "ID" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "MasterID" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "AttributeID" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "Active" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "UpdatedDate" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "CustomKey" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "ID" });
            DropIndex("Products.ProductAssociationType", new[] { "SortOrder" });
            DropIndex("Products.ProductAssociationType", new[] { "DisplayName" });
            DropIndex("Products.ProductAssociationType", new[] { "Name" });
            DropIndex("Products.ProductAssociationType", new[] { "Active" });
            DropIndex("Products.ProductAssociationType", new[] { "UpdatedDate" });
            DropIndex("Products.ProductAssociationType", new[] { "CustomKey" });
            DropIndex("Products.ProductAssociationType", new[] { "ID" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "MasterID" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "AttributeValueID" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "Active" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "UpdatedDate" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "CustomKey" });
            DropIndex("Products.ProductAssociationAttribute", new[] { "ID" });
            DropIndex("Products.ProductAssociation", new[] { "AssociatedProductID" });
            DropIndex("Products.ProductAssociation", new[] { "PrimaryProductID" });
            DropIndex("Products.ProductAssociation", new[] { "TypeID" });
            DropIndex("Products.ProductAssociation", new[] { "Active" });
            DropIndex("Products.ProductAssociation", new[] { "UpdatedDate" });
            DropIndex("Products.ProductAssociation", new[] { "CustomKey" });
            DropIndex("Products.ProductAssociation", new[] { "ID" });
            DropIndex("Shipping.PackageType", new[] { "SortOrder" });
            DropIndex("Shipping.PackageType", new[] { "DisplayName" });
            DropIndex("Shipping.PackageType", new[] { "Name" });
            DropIndex("Shipping.PackageType", new[] { "Active" });
            DropIndex("Shipping.PackageType", new[] { "UpdatedDate" });
            DropIndex("Shipping.PackageType", new[] { "CustomKey" });
            DropIndex("Shipping.PackageType", new[] { "ID" });
            DropIndex("Shipping.Package", new[] { "TypeID" });
            DropIndex("Shipping.Package", new[] { "Name" });
            DropIndex("Shipping.Package", new[] { "Active" });
            DropIndex("Shipping.Package", new[] { "UpdatedDate" });
            DropIndex("Shipping.Package", new[] { "CustomKey" });
            DropIndex("Shipping.Package", new[] { "ID" });
            DropIndex("Discounts.DiscountProducts", new[] { "ProductID" });
            DropIndex("Discounts.DiscountProducts", new[] { "DiscountID" });
            DropIndex("Discounts.DiscountProducts", new[] { "Active" });
            DropIndex("Discounts.DiscountProducts", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountProducts", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountProducts", new[] { "ID" });
            DropIndex("Shopping.CartItemStatus", new[] { "SortOrder" });
            DropIndex("Shopping.CartItemStatus", new[] { "DisplayName" });
            DropIndex("Shopping.CartItemStatus", new[] { "Name" });
            DropIndex("Shopping.CartItemStatus", new[] { "Active" });
            DropIndex("Shopping.CartItemStatus", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartItemStatus", new[] { "CustomKey" });
            DropIndex("Shopping.CartItemStatus", new[] { "ID" });
            DropIndex("Shopping.CartType", new[] { "CreatedByUserID" });
            DropIndex("Shopping.CartType", new[] { "StoreID" });
            DropIndex("Shopping.CartType", new[] { "SortOrder" });
            DropIndex("Shopping.CartType", new[] { "DisplayName" });
            DropIndex("Shopping.CartType", new[] { "Name" });
            DropIndex("Shopping.CartType", new[] { "Active" });
            DropIndex("Shopping.CartType", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartType", new[] { "CustomKey" });
            DropIndex("Shopping.CartType", new[] { "ID" });
            DropIndex("Shopping.CartStatus", new[] { "SortOrder" });
            DropIndex("Shopping.CartStatus", new[] { "DisplayName" });
            DropIndex("Shopping.CartStatus", new[] { "Name" });
            DropIndex("Shopping.CartStatus", new[] { "Active" });
            DropIndex("Shopping.CartStatus", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartStatus", new[] { "CustomKey" });
            DropIndex("Shopping.CartStatus", new[] { "ID" });
            DropIndex("Shopping.CartState", new[] { "SortOrder" });
            DropIndex("Shopping.CartState", new[] { "DisplayName" });
            DropIndex("Shopping.CartState", new[] { "Name" });
            DropIndex("Shopping.CartState", new[] { "Active" });
            DropIndex("Shopping.CartState", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartState", new[] { "CustomKey" });
            DropIndex("Shopping.CartState", new[] { "ID" });
            DropIndex("System.NoteType", new[] { "SortOrder" });
            DropIndex("System.NoteType", new[] { "DisplayName" });
            DropIndex("System.NoteType", new[] { "Name" });
            DropIndex("System.NoteType", new[] { "Active" });
            DropIndex("System.NoteType", new[] { "UpdatedDate" });
            DropIndex("System.NoteType", new[] { "CustomKey" });
            DropIndex("System.NoteType", new[] { "ID" });
            DropIndex("Payments.Wallet", new[] { "UserID" });
            DropIndex("Payments.Wallet", new[] { "Name" });
            DropIndex("Payments.Wallet", new[] { "Active" });
            DropIndex("Payments.Wallet", new[] { "UpdatedDate" });
            DropIndex("Payments.Wallet", new[] { "CustomKey" });
            DropIndex("Payments.Wallet", new[] { "ID" });
            DropIndex("Contacts.UserStatus", new[] { "SortOrder" });
            DropIndex("Contacts.UserStatus", new[] { "DisplayName" });
            DropIndex("Contacts.UserStatus", new[] { "Name" });
            DropIndex("Contacts.UserStatus", new[] { "Active" });
            DropIndex("Contacts.UserStatus", new[] { "UpdatedDate" });
            DropIndex("Contacts.UserStatus", new[] { "CustomKey" });
            DropIndex("Contacts.UserStatus", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteType", new[] { "SortOrder" });
            DropIndex("Quoting.SalesQuoteType", new[] { "DisplayName" });
            DropIndex("Quoting.SalesQuoteType", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteType", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteType", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteType", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteType", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteStatus", new[] { "SortOrder" });
            DropIndex("Quoting.SalesQuoteStatus", new[] { "DisplayName" });
            DropIndex("Quoting.SalesQuoteStatus", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteStatus", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteStatus", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteStatus", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteStatus", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteState", new[] { "SortOrder" });
            DropIndex("Quoting.SalesQuoteState", new[] { "DisplayName" });
            DropIndex("Quoting.SalesQuoteState", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteState", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteState", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteState", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteState", new[] { "ID" });
            DropIndex("Shipping.ShipOption", new[] { "ShipCarrierMethodID" });
            DropIndex("Shipping.ShipOption", new[] { "Name" });
            DropIndex("Shipping.ShipOption", new[] { "Active" });
            DropIndex("Shipping.ShipOption", new[] { "UpdatedDate" });
            DropIndex("Shipping.ShipOption", new[] { "CustomKey" });
            DropIndex("Shipping.ShipOption", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "SortOrder" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "DisplayName" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteItemStatus", new[] { "ID" });
            DropIndex("Discounts.QuoteItemDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.QuoteItemDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.QuoteItemDiscounts", new[] { "Active" });
            DropIndex("Discounts.QuoteItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.QuoteItemDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.QuoteItemDiscounts", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "MasterID" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "AttributeID" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteItemAttribute", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "StoreProductID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "VendorProductID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "StatusID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "MasterID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "UserID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "ProductID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "Name" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "ID" });
            DropIndex("Discounts.SalesQuoteDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.SalesQuoteDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.SalesQuoteDiscounts", new[] { "Active" });
            DropIndex("Discounts.SalesQuoteDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.SalesQuoteDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.SalesQuoteDiscounts", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "MasterID" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "AttributeID" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteAttribute", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteSalesOrder", new[] { "SalesOrderID" });
            DropIndex("Quoting.SalesQuoteSalesOrder", new[] { "SalesQuoteID" });
            DropIndex("Quoting.SalesQuoteSalesOrder", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteSalesOrder", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteSalesOrder", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteSalesOrder", new[] { "ID" });
            DropIndex("Quoting.SalesQuote", new[] { "ShipOptionID" });
            DropIndex("Quoting.SalesQuote", new[] { "StoreID" });
            DropIndex("Quoting.SalesQuote", new[] { "AccountID" });
            DropIndex("Quoting.SalesQuote", new[] { "UserID" });
            DropIndex("Quoting.SalesQuote", new[] { "TypeID" });
            DropIndex("Quoting.SalesQuote", new[] { "StateID" });
            DropIndex("Quoting.SalesQuote", new[] { "StatusID" });
            DropIndex("Quoting.SalesQuote", new[] { "ShippingContactID" });
            DropIndex("Quoting.SalesQuote", new[] { "BillingContactID" });
            DropIndex("Quoting.SalesQuote", new[] { "Active" });
            DropIndex("Quoting.SalesQuote", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuote", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuote", new[] { "ID" });
            DropIndex("Contacts.Permission", new[] { "Id" });
            DropIndex("Contacts.RolePermission", new[] { "PermissionId" });
            DropIndex("Contacts.RolePermission", new[] { "RoleId" });
            DropIndex("Contacts.UserRole", "RoleNameIndex");
            DropIndex("Contacts.RoleUser", new[] { "UserId" });
            DropIndex("Contacts.RoleUser", new[] { "RoleId" });
            DropIndex("Notifications.Notification", new[] { "UserID" });
            DropIndex("Notifications.Notification", new[] { "Active" });
            DropIndex("Notifications.Notification", new[] { "UpdatedDate" });
            DropIndex("Notifications.Notification", new[] { "CustomKey" });
            DropIndex("Notifications.Notification", new[] { "ID" });
            DropIndex("Notifications.NotificationMessage", new[] { "UserID" });
            DropIndex("Notifications.NotificationMessage", new[] { "AccountID" });
            DropIndex("Notifications.NotificationMessage", new[] { "Active" });
            DropIndex("Notifications.NotificationMessage", new[] { "UpdatedDate" });
            DropIndex("Notifications.NotificationMessage", new[] { "CustomKey" });
            DropIndex("Notifications.NotificationMessage", new[] { "ID" });
            DropIndex("Messaging.EmailType", new[] { "SortOrder" });
            DropIndex("Messaging.EmailType", new[] { "DisplayName" });
            DropIndex("Messaging.EmailType", new[] { "Name" });
            DropIndex("Messaging.EmailType", new[] { "Active" });
            DropIndex("Messaging.EmailType", new[] { "UpdatedDate" });
            DropIndex("Messaging.EmailType", new[] { "CustomKey" });
            DropIndex("Messaging.EmailType", new[] { "ID" });
            DropIndex("Messaging.EmailStatus", new[] { "SortOrder" });
            DropIndex("Messaging.EmailStatus", new[] { "DisplayName" });
            DropIndex("Messaging.EmailStatus", new[] { "Name" });
            DropIndex("Messaging.EmailStatus", new[] { "Active" });
            DropIndex("Messaging.EmailStatus", new[] { "UpdatedDate" });
            DropIndex("Messaging.EmailStatus", new[] { "CustomKey" });
            DropIndex("Messaging.EmailStatus", new[] { "ID" });
            DropIndex("Messaging.EmailTemplate", new[] { "Name" });
            DropIndex("Messaging.EmailTemplate", new[] { "Active" });
            DropIndex("Messaging.EmailTemplate", new[] { "UpdatedDate" });
            DropIndex("Messaging.EmailTemplate", new[] { "CustomKey" });
            DropIndex("Messaging.EmailTemplate", new[] { "ID" });
            DropIndex("Messaging.EmailQueue", new[] { "MessageRecipientID" });
            DropIndex("Messaging.EmailQueue", new[] { "EmailTemplateID" });
            DropIndex("Messaging.EmailQueue", new[] { "TypeID" });
            DropIndex("Messaging.EmailQueue", new[] { "StatusID" });
            DropIndex("Messaging.EmailQueue", new[] { "Active" });
            DropIndex("Messaging.EmailQueue", new[] { "UpdatedDate" });
            DropIndex("Messaging.EmailQueue", new[] { "CustomKey" });
            DropIndex("Messaging.EmailQueue", new[] { "ID" });
            DropIndex("Messaging.MessageRecipient", new[] { "ToUserID" });
            DropIndex("Messaging.MessageRecipient", new[] { "MessageID" });
            DropIndex("Messaging.MessageRecipient", new[] { "Active" });
            DropIndex("Messaging.MessageRecipient", new[] { "UpdatedDate" });
            DropIndex("Messaging.MessageRecipient", new[] { "CustomKey" });
            DropIndex("Messaging.MessageRecipient", new[] { "ID" });
            DropIndex("Stores.StoreType", new[] { "SortOrder" });
            DropIndex("Stores.StoreType", new[] { "DisplayName" });
            DropIndex("Stores.StoreType", new[] { "Name" });
            DropIndex("Stores.StoreType", new[] { "Active" });
            DropIndex("Stores.StoreType", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreType", new[] { "CustomKey" });
            DropIndex("Stores.StoreType", new[] { "ID" });
            DropIndex("Contacts.UserType", new[] { "SortOrder" });
            DropIndex("Contacts.UserType", new[] { "DisplayName" });
            DropIndex("Contacts.UserType", new[] { "Name" });
            DropIndex("Contacts.UserType", new[] { "Active" });
            DropIndex("Contacts.UserType", new[] { "UpdatedDate" });
            DropIndex("Contacts.UserType", new[] { "CustomKey" });
            DropIndex("Contacts.UserType", new[] { "ID" });
            DropIndex("Stores.StoreUserType", new[] { "UserTypeID" });
            DropIndex("Stores.StoreUserType", new[] { "StoreID" });
            DropIndex("Stores.StoreUserType", new[] { "Active" });
            DropIndex("Stores.StoreUserType", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreUserType", new[] { "CustomKey" });
            DropIndex("Stores.StoreUserType", new[] { "ID" });
            DropIndex("Stores.StoreUser", new[] { "UserID" });
            DropIndex("Stores.StoreUser", new[] { "StoreID" });
            DropIndex("Stores.StoreUser", new[] { "Active" });
            DropIndex("Stores.StoreUser", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreUser", new[] { "CustomKey" });
            DropIndex("Stores.StoreUser", new[] { "ID" });
            DropIndex("Products.ProductSubscriptionType", new[] { "ProductID" });
            DropIndex("Products.ProductSubscriptionType", new[] { "SubscriptionTypeID" });
            DropIndex("Products.ProductSubscriptionType", new[] { "Active" });
            DropIndex("Products.ProductSubscriptionType", new[] { "UpdatedDate" });
            DropIndex("Products.ProductSubscriptionType", new[] { "CustomKey" });
            DropIndex("Products.ProductSubscriptionType", new[] { "ID" });
            DropIndex("Payments.SubscriptionType", new[] { "SortOrder" });
            DropIndex("Payments.SubscriptionType", new[] { "DisplayName" });
            DropIndex("Payments.SubscriptionType", new[] { "Name" });
            DropIndex("Payments.SubscriptionType", new[] { "Active" });
            DropIndex("Payments.SubscriptionType", new[] { "UpdatedDate" });
            DropIndex("Payments.SubscriptionType", new[] { "CustomKey" });
            DropIndex("Payments.SubscriptionType", new[] { "ID" });
            DropIndex("Payments.SubscriptionStatus", new[] { "SortOrder" });
            DropIndex("Payments.SubscriptionStatus", new[] { "DisplayName" });
            DropIndex("Payments.SubscriptionStatus", new[] { "Name" });
            DropIndex("Payments.SubscriptionStatus", new[] { "Active" });
            DropIndex("Payments.SubscriptionStatus", new[] { "UpdatedDate" });
            DropIndex("Payments.SubscriptionStatus", new[] { "CustomKey" });
            DropIndex("Payments.SubscriptionStatus", new[] { "ID" });
            DropIndex("Payments.RepeatType", new[] { "SortOrder" });
            DropIndex("Payments.RepeatType", new[] { "DisplayName" });
            DropIndex("Payments.RepeatType", new[] { "Name" });
            DropIndex("Payments.RepeatType", new[] { "Active" });
            DropIndex("Payments.RepeatType", new[] { "UpdatedDate" });
            DropIndex("Payments.RepeatType", new[] { "CustomKey" });
            DropIndex("Payments.RepeatType", new[] { "ID" });
            DropIndex("Payments.PaymentType", new[] { "SortOrder" });
            DropIndex("Payments.PaymentType", new[] { "DisplayName" });
            DropIndex("Payments.PaymentType", new[] { "Name" });
            DropIndex("Payments.PaymentType", new[] { "Active" });
            DropIndex("Payments.PaymentType", new[] { "UpdatedDate" });
            DropIndex("Payments.PaymentType", new[] { "CustomKey" });
            DropIndex("Payments.PaymentType", new[] { "ID" });
            DropIndex("Payments.SubscriptionHistory", new[] { "SubscriptionID" });
            DropIndex("Payments.SubscriptionHistory", new[] { "PaymentID" });
            DropIndex("Payments.SubscriptionHistory", new[] { "Active" });
            DropIndex("Payments.SubscriptionHistory", new[] { "UpdatedDate" });
            DropIndex("Payments.SubscriptionHistory", new[] { "CustomKey" });
            DropIndex("Payments.SubscriptionHistory", new[] { "ID" });
            DropIndex("Payments.PaymentStatus", new[] { "SortOrder" });
            DropIndex("Payments.PaymentStatus", new[] { "DisplayName" });
            DropIndex("Payments.PaymentStatus", new[] { "Name" });
            DropIndex("Payments.PaymentStatus", new[] { "Active" });
            DropIndex("Payments.PaymentStatus", new[] { "UpdatedDate" });
            DropIndex("Payments.PaymentStatus", new[] { "CustomKey" });
            DropIndex("Payments.PaymentStatus", new[] { "ID" });
            DropIndex("Payments.PaymentMethod", new[] { "Name" });
            DropIndex("Payments.PaymentMethod", new[] { "Active" });
            DropIndex("Payments.PaymentMethod", new[] { "UpdatedDate" });
            DropIndex("Payments.PaymentMethod", new[] { "CustomKey" });
            DropIndex("Payments.PaymentMethod", new[] { "ID" });
            DropIndex("Payments.Payment", new[] { "TypeID" });
            DropIndex("Payments.Payment", new[] { "StatusID" });
            DropIndex("Payments.Payment", new[] { "PaymentMethodID" });
            DropIndex("Payments.Payment", new[] { "BillingContactID" });
            DropIndex("Payments.Payment", new[] { "StoreID" });
            DropIndex("Payments.Payment", new[] { "Active" });
            DropIndex("Payments.Payment", new[] { "UpdatedDate" });
            DropIndex("Payments.Payment", new[] { "CustomKey" });
            DropIndex("Payments.Payment", new[] { "ID" });
            DropIndex("Payments.Subscription", new[] { "AccountID" });
            DropIndex("Payments.Subscription", new[] { "UserID" });
            DropIndex("Payments.Subscription", new[] { "SubscriptionTypeID" });
            DropIndex("Payments.Subscription", new[] { "SubscriptionStatusID" });
            DropIndex("Payments.Subscription", new[] { "PaymentID" });
            DropIndex("Payments.Subscription", new[] { "RepeatTypeID" });
            DropIndex("Payments.Subscription", new[] { "Name" });
            DropIndex("Payments.Subscription", new[] { "Active" });
            DropIndex("Payments.Subscription", new[] { "UpdatedDate" });
            DropIndex("Payments.Subscription", new[] { "CustomKey" });
            DropIndex("Payments.Subscription", new[] { "ID" });
            DropIndex("Stores.StoreSubscription", new[] { "SubscriptionID" });
            DropIndex("Stores.StoreSubscription", new[] { "StoreID" });
            DropIndex("Stores.StoreSubscription", new[] { "Active" });
            DropIndex("Stores.StoreSubscription", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreSubscription", new[] { "CustomKey" });
            DropIndex("Stores.StoreSubscription", new[] { "ID" });
            DropIndex("Stores.StoreImage", new[] { "LibraryID" });
            DropIndex("Stores.StoreImage", new[] { "StoreID" });
            DropIndex("Stores.StoreImage", new[] { "Active" });
            DropIndex("Stores.StoreImage", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreImage", new[] { "CustomKey" });
            DropIndex("Stores.StoreImage", new[] { "ID" });
            DropIndex("Stores.StoreContact", new[] { "ContactID" });
            DropIndex("Stores.StoreContact", new[] { "StoreID" });
            DropIndex("Stores.StoreContact", new[] { "Name" });
            DropIndex("Stores.StoreContact", new[] { "Active" });
            DropIndex("Stores.StoreContact", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreContact", new[] { "CustomKey" });
            DropIndex("Stores.StoreContact", new[] { "ID" });
            DropIndex("Categories.CategoryType", new[] { "SortOrder" });
            DropIndex("Categories.CategoryType", new[] { "DisplayName" });
            DropIndex("Categories.CategoryType", new[] { "ParentID" });
            DropIndex("Categories.CategoryType", new[] { "Name" });
            DropIndex("Categories.CategoryType", new[] { "Active" });
            DropIndex("Categories.CategoryType", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryType", new[] { "CustomKey" });
            DropIndex("Categories.CategoryType", new[] { "ID" });
            DropIndex("Stores.StoreCategoryType", new[] { "CategoryTypeID" });
            DropIndex("Stores.StoreCategoryType", new[] { "StoreID" });
            DropIndex("Stores.StoreCategoryType", new[] { "Active" });
            DropIndex("Stores.StoreCategoryType", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreCategoryType", new[] { "CustomKey" });
            DropIndex("Stores.StoreCategoryType", new[] { "ID" });
            DropIndex("Stores.StoreCategory", new[] { "CategoryID" });
            DropIndex("Stores.StoreCategory", new[] { "StoreID" });
            DropIndex("Stores.StoreCategory", new[] { "Active" });
            DropIndex("Stores.StoreCategory", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreCategory", new[] { "CustomKey" });
            DropIndex("Stores.StoreCategory", new[] { "ID" });
            DropIndex("Pricing.PricePoint", new[] { "SortOrder" });
            DropIndex("Pricing.PricePoint", new[] { "DisplayName" });
            DropIndex("Pricing.PricePoint", new[] { "Name" });
            DropIndex("Pricing.PricePoint", new[] { "Active" });
            DropIndex("Pricing.PricePoint", new[] { "UpdatedDate" });
            DropIndex("Pricing.PricePoint", new[] { "CustomKey" });
            DropIndex("Pricing.PricePoint", new[] { "ID" });
            DropIndex("Stores.StoreAccount", new[] { "PricePointID" });
            DropIndex("Stores.StoreAccount", new[] { "AccountID" });
            DropIndex("Stores.StoreAccount", new[] { "StoreID" });
            DropIndex("Stores.StoreAccount", new[] { "Active" });
            DropIndex("Stores.StoreAccount", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreAccount", new[] { "CustomKey" });
            DropIndex("Stores.StoreAccount", new[] { "ID" });
            DropIndex("Reviews.ReviewType", new[] { "SortOrder" });
            DropIndex("Reviews.ReviewType", new[] { "DisplayName" });
            DropIndex("Reviews.ReviewType", new[] { "Name" });
            DropIndex("Reviews.ReviewType", new[] { "Active" });
            DropIndex("Reviews.ReviewType", new[] { "UpdatedDate" });
            DropIndex("Reviews.ReviewType", new[] { "CustomKey" });
            DropIndex("Reviews.ReviewType", new[] { "ID" });
            DropIndex("Vendors.VendorTerm", new[] { "Name" });
            DropIndex("Vendors.VendorTerm", new[] { "Active" });
            DropIndex("Vendors.VendorTerm", new[] { "UpdatedDate" });
            DropIndex("Vendors.VendorTerm", new[] { "CustomKey" });
            DropIndex("Vendors.VendorTerm", new[] { "ID" });
            DropIndex("Vendors.Term", new[] { "UpdatedByUserID" });
            DropIndex("Vendors.Term", new[] { "CreatedByUserID" });
            DropIndex("Vendors.Term", new[] { "VendorTermID" });
            DropIndex("Vendors.Term", new[] { "Active" });
            DropIndex("Vendors.Term", new[] { "UpdatedDate" });
            DropIndex("Vendors.Term", new[] { "CustomKey" });
            DropIndex("Vendors.Term", new[] { "ID" });
            DropIndex("Stores.StoreVendor", new[] { "VendorID" });
            DropIndex("Stores.StoreVendor", new[] { "StoreID" });
            DropIndex("Stores.StoreVendor", new[] { "Active" });
            DropIndex("Stores.StoreVendor", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreVendor", new[] { "CustomKey" });
            DropIndex("Stores.StoreVendor", new[] { "ID" });
            DropIndex("Shipping.ShipmentType", new[] { "SortOrder" });
            DropIndex("Shipping.ShipmentType", new[] { "DisplayName" });
            DropIndex("Shipping.ShipmentType", new[] { "Name" });
            DropIndex("Shipping.ShipmentType", new[] { "Active" });
            DropIndex("Shipping.ShipmentType", new[] { "UpdatedDate" });
            DropIndex("Shipping.ShipmentType", new[] { "CustomKey" });
            DropIndex("Shipping.ShipmentType", new[] { "ID" });
            DropIndex("Shipping.ShipmentStatus", new[] { "SortOrder" });
            DropIndex("Shipping.ShipmentStatus", new[] { "DisplayName" });
            DropIndex("Shipping.ShipmentStatus", new[] { "Name" });
            DropIndex("Shipping.ShipmentStatus", new[] { "Active" });
            DropIndex("Shipping.ShipmentStatus", new[] { "UpdatedDate" });
            DropIndex("Shipping.ShipmentStatus", new[] { "CustomKey" });
            DropIndex("Shipping.ShipmentStatus", new[] { "ID" });
            DropIndex("Shipping.ShipmentEvent", new[] { "ShipmentID" });
            DropIndex("Shipping.ShipmentEvent", new[] { "AddressID" });
            DropIndex("Shipping.ShipmentEvent", new[] { "Active" });
            DropIndex("Shipping.ShipmentEvent", new[] { "UpdatedDate" });
            DropIndex("Shipping.ShipmentEvent", new[] { "CustomKey" });
            DropIndex("Shipping.ShipmentEvent", new[] { "ID" });
            DropIndex("Vendors.ShipVia", new[] { "ShipCarrierID" });
            DropIndex("Vendors.ShipVia", new[] { "Active" });
            DropIndex("Vendors.ShipVia", new[] { "UpdatedDate" });
            DropIndex("Vendors.ShipVia", new[] { "CustomKey" });
            DropIndex("Vendors.ShipVia", new[] { "ID" });
            DropIndex("Shipping.ShipCarrierMethod", new[] { "ShipCarrierID" });
            DropIndex("Shipping.ShipCarrierMethod", new[] { "Name" });
            DropIndex("Shipping.ShipCarrierMethod", new[] { "Active" });
            DropIndex("Shipping.ShipCarrierMethod", new[] { "UpdatedDate" });
            DropIndex("Shipping.ShipCarrierMethod", new[] { "CustomKey" });
            DropIndex("Shipping.ShipCarrierMethod", new[] { "ID" });
            DropIndex("Shipping.CarrierOrigin", new[] { "ShipCarrierID" });
            DropIndex("Shipping.CarrierOrigin", new[] { "Active" });
            DropIndex("Shipping.CarrierOrigin", new[] { "UpdatedDate" });
            DropIndex("Shipping.CarrierOrigin", new[] { "CustomKey" });
            DropIndex("Shipping.CarrierOrigin", new[] { "ID" });
            DropIndex("Shipping.CarrierInvoice", new[] { "ShipCarrierID" });
            DropIndex("Shipping.CarrierInvoice", new[] { "Active" });
            DropIndex("Shipping.CarrierInvoice", new[] { "UpdatedDate" });
            DropIndex("Shipping.CarrierInvoice", new[] { "CustomKey" });
            DropIndex("Shipping.CarrierInvoice", new[] { "ID" });
            DropIndex("Shipping.ShipCarrier", new[] { "AddressID" });
            DropIndex("Shipping.ShipCarrier", new[] { "Email" });
            DropIndex("Shipping.ShipCarrier", new[] { "Fax" });
            DropIndex("Shipping.ShipCarrier", new[] { "Phone" });
            DropIndex("Shipping.ShipCarrier", new[] { "Name" });
            DropIndex("Shipping.ShipCarrier", new[] { "Active" });
            DropIndex("Shipping.ShipCarrier", new[] { "UpdatedDate" });
            DropIndex("Shipping.ShipCarrier", new[] { "CustomKey" });
            DropIndex("Shipping.ShipCarrier", new[] { "ID" });
            DropIndex("Vendors.VendorProduct", new[] { "VendorID" });
            DropIndex("Vendors.VendorProduct", new[] { "ProductID" });
            DropIndex("Vendors.VendorProduct", new[] { "Active" });
            DropIndex("Vendors.VendorProduct", new[] { "UpdatedDate" });
            DropIndex("Vendors.VendorProduct", new[] { "CustomKey" });
            DropIndex("Vendors.VendorProduct", new[] { "ID" });
            DropIndex("Stores.StoreProduct", new[] { "ProductID" });
            DropIndex("Stores.StoreProduct", new[] { "StoreID" });
            DropIndex("Stores.StoreProduct", new[] { "Active" });
            DropIndex("Stores.StoreProduct", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreProduct", new[] { "CustomKey" });
            DropIndex("Stores.StoreProduct", new[] { "ID" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "SortOrder" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "DisplayName" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "Name" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "Active" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderItemStatus", new[] { "ID" });
            DropIndex("Discounts.SalesOrderItemDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.SalesOrderItemDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.SalesOrderItemDiscounts", new[] { "Active" });
            DropIndex("Discounts.SalesOrderItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.SalesOrderItemDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.SalesOrderItemDiscounts", new[] { "ID" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "MasterID" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "AttributeID" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "Active" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderItemAttribute", new[] { "ID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "StoreProductID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "VendorProductID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "StatusID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "MasterID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "UserID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "ProductID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "Name" });
            DropIndex("Ordering.SalesOrderItem", new[] { "Active" });
            DropIndex("Ordering.SalesOrderItem", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderItem", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderItem", new[] { "ID" });
            DropIndex("Shipping.SalesOrderItemShipment", new[] { "ShipmentID" });
            DropIndex("Shipping.SalesOrderItemShipment", new[] { "SalesOrderItemID" });
            DropIndex("Shipping.SalesOrderItemShipment", new[] { "Active" });
            DropIndex("Shipping.SalesOrderItemShipment", new[] { "UpdatedDate" });
            DropIndex("Shipping.SalesOrderItemShipment", new[] { "CustomKey" });
            DropIndex("Shipping.SalesOrderItemShipment", new[] { "ID" });
            DropIndex("Products.ProductInventoryLocationSection", new[] { "ProductID" });
            DropIndex("Products.ProductInventoryLocationSection", new[] { "InventoryLocationSectionID" });
            DropIndex("Products.ProductInventoryLocationSection", new[] { "Active" });
            DropIndex("Products.ProductInventoryLocationSection", new[] { "UpdatedDate" });
            DropIndex("Products.ProductInventoryLocationSection", new[] { "CustomKey" });
            DropIndex("Products.ProductInventoryLocationSection", new[] { "ID" });
            DropIndex("Stores.StoreInventoryLocation", new[] { "InventoryLocationID" });
            DropIndex("Stores.StoreInventoryLocation", new[] { "StoreID" });
            DropIndex("Stores.StoreInventoryLocation", new[] { "Active" });
            DropIndex("Stores.StoreInventoryLocation", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreInventoryLocation", new[] { "CustomKey" });
            DropIndex("Stores.StoreInventoryLocation", new[] { "ID" });
            DropIndex("Inventory.InventoryLocation", new[] { "AddressID" });
            DropIndex("Inventory.InventoryLocation", new[] { "Email" });
            DropIndex("Inventory.InventoryLocation", new[] { "Fax" });
            DropIndex("Inventory.InventoryLocation", new[] { "Phone" });
            DropIndex("Inventory.InventoryLocation", new[] { "Name" });
            DropIndex("Inventory.InventoryLocation", new[] { "Active" });
            DropIndex("Inventory.InventoryLocation", new[] { "UpdatedDate" });
            DropIndex("Inventory.InventoryLocation", new[] { "CustomKey" });
            DropIndex("Inventory.InventoryLocation", new[] { "ID" });
            DropIndex("Inventory.InventoryLocationSection", new[] { "InventoryLocationID" });
            DropIndex("Inventory.InventoryLocationSection", new[] { "Name" });
            DropIndex("Inventory.InventoryLocationSection", new[] { "Active" });
            DropIndex("Inventory.InventoryLocationSection", new[] { "UpdatedDate" });
            DropIndex("Inventory.InventoryLocationSection", new[] { "CustomKey" });
            DropIndex("Inventory.InventoryLocationSection", new[] { "ID" });
            DropIndex("Shipping.Shipment", new[] { "VendorID" });
            DropIndex("Shipping.Shipment", new[] { "TypeID" });
            DropIndex("Shipping.Shipment", new[] { "StatusID" });
            DropIndex("Shipping.Shipment", new[] { "ShipCarrierMethodID" });
            DropIndex("Shipping.Shipment", new[] { "ShipCarrierID" });
            DropIndex("Shipping.Shipment", new[] { "InventoryLocationSectionID" });
            DropIndex("Shipping.Shipment", new[] { "DestinationContactID" });
            DropIndex("Shipping.Shipment", new[] { "OriginContactID" });
            DropIndex("Shipping.Shipment", new[] { "Active" });
            DropIndex("Shipping.Shipment", new[] { "UpdatedDate" });
            DropIndex("Shipping.Shipment", new[] { "CustomKey" });
            DropIndex("Shipping.Shipment", new[] { "ID" });
            DropIndex("Contacts.ContactMethod", new[] { "SortOrder" });
            DropIndex("Contacts.ContactMethod", new[] { "DisplayName" });
            DropIndex("Contacts.ContactMethod", new[] { "Name" });
            DropIndex("Contacts.ContactMethod", new[] { "Active" });
            DropIndex("Contacts.ContactMethod", new[] { "UpdatedDate" });
            DropIndex("Contacts.ContactMethod", new[] { "CustomKey" });
            DropIndex("Contacts.ContactMethod", new[] { "ID" });
            DropIndex("Vendors.Vendor", new[] { "ShipViaID" });
            DropIndex("Vendors.Vendor", new[] { "ContactMethodID" });
            DropIndex("Vendors.Vendor", new[] { "TermID" });
            DropIndex("Vendors.Vendor", new[] { "AddressID" });
            DropIndex("Vendors.Vendor", new[] { "VendorHash" });
            DropIndex("Vendors.Vendor", new[] { "ContactID" });
            DropIndex("Vendors.Vendor", new[] { "Email" });
            DropIndex("Vendors.Vendor", new[] { "Fax" });
            DropIndex("Vendors.Vendor", new[] { "Phone" });
            DropIndex("Vendors.Vendor", new[] { "Name" });
            DropIndex("Vendors.Vendor", new[] { "Active" });
            DropIndex("Vendors.Vendor", new[] { "UpdatedDate" });
            DropIndex("Vendors.Vendor", new[] { "CustomKey" });
            DropIndex("Vendors.Vendor", new[] { "ID" });
            DropIndex("Vendors.VendorManufacturer", new[] { "VendorID" });
            DropIndex("Vendors.VendorManufacturer", new[] { "ManufacturerID" });
            DropIndex("Vendors.VendorManufacturer", new[] { "Active" });
            DropIndex("Vendors.VendorManufacturer", new[] { "UpdatedDate" });
            DropIndex("Vendors.VendorManufacturer", new[] { "CustomKey" });
            DropIndex("Vendors.VendorManufacturer", new[] { "ID" });
            DropIndex("Stores.StoreManufacturer", new[] { "ManufacturerID" });
            DropIndex("Stores.StoreManufacturer", new[] { "StoreID" });
            DropIndex("Stores.StoreManufacturer", new[] { "Active" });
            DropIndex("Stores.StoreManufacturer", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreManufacturer", new[] { "CustomKey" });
            DropIndex("Stores.StoreManufacturer", new[] { "ID" });
            DropIndex("Manufacturers.ManufacturerProduct", new[] { "ManufacturerID" });
            DropIndex("Manufacturers.ManufacturerProduct", new[] { "ProductID" });
            DropIndex("Manufacturers.ManufacturerProduct", new[] { "Active" });
            DropIndex("Manufacturers.ManufacturerProduct", new[] { "UpdatedDate" });
            DropIndex("Manufacturers.ManufacturerProduct", new[] { "CustomKey" });
            DropIndex("Manufacturers.ManufacturerProduct", new[] { "ID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "AddressID" });
            DropIndex("Manufacturers.Manufacturer", new[] { "Email" });
            DropIndex("Manufacturers.Manufacturer", new[] { "Fax" });
            DropIndex("Manufacturers.Manufacturer", new[] { "Phone" });
            DropIndex("Manufacturers.Manufacturer", new[] { "Name" });
            DropIndex("Manufacturers.Manufacturer", new[] { "Active" });
            DropIndex("Manufacturers.Manufacturer", new[] { "UpdatedDate" });
            DropIndex("Manufacturers.Manufacturer", new[] { "CustomKey" });
            DropIndex("Manufacturers.Manufacturer", new[] { "ID" });
            DropIndex("Reviews.Review", new[] { "VendorID" });
            DropIndex("Reviews.Review", new[] { "UserID" });
            DropIndex("Reviews.Review", new[] { "StoreID" });
            DropIndex("Reviews.Review", new[] { "ProductID" });
            DropIndex("Reviews.Review", new[] { "ManufacturerID" });
            DropIndex("Reviews.Review", new[] { "CategoryID" });
            DropIndex("Reviews.Review", new[] { "ApprovedByUserID" });
            DropIndex("Reviews.Review", new[] { "SubmittedByUserID" });
            DropIndex("Reviews.Review", new[] { "TypeID" });
            DropIndex("Reviews.Review", new[] { "Name" });
            DropIndex("Reviews.Review", new[] { "Active" });
            DropIndex("Reviews.Review", new[] { "UpdatedDate" });
            DropIndex("Reviews.Review", new[] { "CustomKey" });
            DropIndex("Reviews.Review", new[] { "ID" });
            DropIndex("Stores.StoreSiteDomain", new[] { "SiteDomainID" });
            DropIndex("Stores.StoreSiteDomain", new[] { "StoreID" });
            DropIndex("Stores.StoreSiteDomain", new[] { "Active" });
            DropIndex("Stores.StoreSiteDomain", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreSiteDomain", new[] { "CustomKey" });
            DropIndex("Stores.StoreSiteDomain", new[] { "ID" });
            DropIndex("Stores.SocialProvider", new[] { "Name" });
            DropIndex("Stores.SocialProvider", new[] { "Active" });
            DropIndex("Stores.SocialProvider", new[] { "UpdatedDate" });
            DropIndex("Stores.SocialProvider", new[] { "CustomKey" });
            DropIndex("Stores.SocialProvider", new[] { "ID" });
            DropIndex("Stores.SiteDomainSocialProvider", new[] { "SocialProviderID" });
            DropIndex("Stores.SiteDomainSocialProvider", new[] { "SiteDomainID" });
            DropIndex("Stores.SiteDomainSocialProvider", new[] { "Active" });
            DropIndex("Stores.SiteDomainSocialProvider", new[] { "UpdatedDate" });
            DropIndex("Stores.SiteDomainSocialProvider", new[] { "CustomKey" });
            DropIndex("Stores.SiteDomainSocialProvider", new[] { "ID" });
            DropIndex("Stores.SiteDomain", new[] { "Name" });
            DropIndex("Stores.SiteDomain", new[] { "Active" });
            DropIndex("Stores.SiteDomain", new[] { "UpdatedDate" });
            DropIndex("Stores.SiteDomain", new[] { "CustomKey" });
            DropIndex("Stores.SiteDomain", new[] { "ID" });
            DropIndex("Stores.BrandSiteDomain", new[] { "SiteDomainID" });
            DropIndex("Stores.BrandSiteDomain", new[] { "BrandID" });
            DropIndex("Stores.BrandSiteDomain", new[] { "Active" });
            DropIndex("Stores.BrandSiteDomain", new[] { "UpdatedDate" });
            DropIndex("Stores.BrandSiteDomain", new[] { "CustomKey" });
            DropIndex("Stores.BrandSiteDomain", new[] { "ID" });
            DropIndex("Stores.Brand", new[] { "Name" });
            DropIndex("Stores.Brand", new[] { "Active" });
            DropIndex("Stores.Brand", new[] { "UpdatedDate" });
            DropIndex("Stores.Brand", new[] { "CustomKey" });
            DropIndex("Stores.Brand", new[] { "ID" });
            DropIndex("Stores.BrandStore", new[] { "BrandID" });
            DropIndex("Stores.BrandStore", new[] { "StoreID" });
            DropIndex("Stores.BrandStore", new[] { "Active" });
            DropIndex("Stores.BrandStore", new[] { "UpdatedDate" });
            DropIndex("Stores.BrandStore", new[] { "CustomKey" });
            DropIndex("Stores.BrandStore", new[] { "ID" });
            DropIndex("Stores.Store", new[] { "SellerImageLibraryID" });
            DropIndex("Stores.Store", new[] { "LogoImageLibraryID" });
            DropIndex("Stores.Store", new[] { "ContactID" });
            DropIndex("Stores.Store", new[] { "Name" });
            DropIndex("Stores.Store", new[] { "TypeID" });
            DropIndex("Stores.Store", new[] { "Active" });
            DropIndex("Stores.Store", new[] { "UpdatedDate" });
            DropIndex("Stores.Store", new[] { "CustomKey" });
            DropIndex("Stores.Store", new[] { "ID" });
            DropIndex("Messaging.Conversation", new[] { "StoreID" });
            DropIndex("Messaging.Conversation", new[] { "Active" });
            DropIndex("Messaging.Conversation", new[] { "UpdatedDate" });
            DropIndex("Messaging.Conversation", new[] { "CustomKey" });
            DropIndex("Messaging.Conversation", new[] { "ID" });
            DropIndex("Messaging.Message", new[] { "SentByUserID" });
            DropIndex("Messaging.Message", new[] { "ConversationID" });
            DropIndex("Messaging.Message", new[] { "StoreID" });
            DropIndex("Messaging.Message", new[] { "Active" });
            DropIndex("Messaging.Message", new[] { "UpdatedDate" });
            DropIndex("Messaging.Message", new[] { "CustomKey" });
            DropIndex("Messaging.Message", new[] { "ID" });
            DropIndex("Messaging.MessageAttachment", new[] { "UpdatedByUserID" });
            DropIndex("Messaging.MessageAttachment", new[] { "CreatedByUserID" });
            DropIndex("Messaging.MessageAttachment", new[] { "LibraryID" });
            DropIndex("Messaging.MessageAttachment", new[] { "MessageID" });
            DropIndex("Messaging.MessageAttachment", new[] { "Name" });
            DropIndex("Messaging.MessageAttachment", new[] { "Active" });
            DropIndex("Messaging.MessageAttachment", new[] { "UpdatedDate" });
            DropIndex("Messaging.MessageAttachment", new[] { "CustomKey" });
            DropIndex("Messaging.MessageAttachment", new[] { "ID" });
            DropIndex("Contacts.UserLogin", new[] { "UserId" });
            DropIndex("Contacts.Favorite", new[] { "UserID" });
            DropIndex("Contacts.Favorite", new[] { "ProductID" });
            DropIndex("Contacts.Favorite", new[] { "Active" });
            DropIndex("Contacts.Favorite", new[] { "UpdatedDate" });
            DropIndex("Contacts.Favorite", new[] { "CustomKey" });
            DropIndex("Contacts.Favorite", new[] { "ID" });
            DropIndex("Contacts.UserClaim", new[] { "UserId" });
            DropIndex("Contacts.UserClaim", new[] { "Id" });
            DropIndex("Contacts.UserAttribute", new[] { "MasterID" });
            DropIndex("Contacts.UserAttribute", new[] { "AttributeValueID" });
            DropIndex("Contacts.UserAttribute", new[] { "Active" });
            DropIndex("Contacts.UserAttribute", new[] { "UpdatedDate" });
            DropIndex("Contacts.UserAttribute", new[] { "CustomKey" });
            DropIndex("Contacts.UserAttribute", new[] { "ID" });
            DropIndex("Contacts.User", new[] { "PreferredStoreID" });
            DropIndex("Contacts.User", new[] { "AccountID" });
            DropIndex("Contacts.User", "UserNameIndex");
            DropIndex("Contacts.User", new[] { "UserName" });
            DropIndex("Contacts.User", new[] { "ContactID" });
            DropIndex("Contacts.User", new[] { "StatusID" });
            DropIndex("Contacts.User", new[] { "TypeID" });
            DropIndex("Contacts.User", new[] { "Active" });
            DropIndex("Contacts.User", new[] { "UpdatedDate" });
            DropIndex("Contacts.User", new[] { "CustomKey" });
            DropIndex("Contacts.User", new[] { "ID" });
            DropIndex("System.Note", new[] { "ManufacturerID" });
            DropIndex("System.Note", new[] { "VendorID" });
            DropIndex("System.Note", new[] { "CartID" });
            DropIndex("System.Note", new[] { "SalesQuoteID" });
            DropIndex("System.Note", new[] { "SalesInvoiceID" });
            DropIndex("System.Note", new[] { "UserID" });
            DropIndex("System.Note", new[] { "AccountID" });
            DropIndex("System.Note", new[] { "SalesOrderID" });
            DropIndex("System.Note", new[] { "PurchaseOrderID" });
            DropIndex("System.Note", new[] { "UpdatedByUserID" });
            DropIndex("System.Note", new[] { "CreatedByUserID" });
            DropIndex("System.Note", new[] { "TypeID" });
            DropIndex("System.Note", new[] { "Active" });
            DropIndex("System.Note", new[] { "UpdatedDate" });
            DropIndex("System.Note", new[] { "CustomKey" });
            DropIndex("System.Note", new[] { "ID" });
            DropIndex("Discounts.CartDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.CartDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.CartDiscounts", new[] { "Active" });
            DropIndex("Discounts.CartDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.CartDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.CartDiscounts", new[] { "ID" });
            DropIndex("Shopping.CartAttribute", new[] { "MasterID" });
            DropIndex("Shopping.CartAttribute", new[] { "AttributeID" });
            DropIndex("Shopping.CartAttribute", new[] { "Active" });
            DropIndex("Shopping.CartAttribute", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartAttribute", new[] { "CustomKey" });
            DropIndex("Shopping.CartAttribute", new[] { "ID" });
            DropIndex("Shopping.Cart", new[] { "ShipOptionID" });
            DropIndex("Shopping.Cart", new[] { "ShippingAddressID" });
            DropIndex("Shopping.Cart", new[] { "StoreID" });
            DropIndex("Shopping.Cart", new[] { "AccountID" });
            DropIndex("Shopping.Cart", new[] { "UserID" });
            DropIndex("Shopping.Cart", new[] { "TypeID" });
            DropIndex("Shopping.Cart", new[] { "StateID" });
            DropIndex("Shopping.Cart", new[] { "StatusID" });
            DropIndex("Shopping.Cart", new[] { "ShippingContactID" });
            DropIndex("Shopping.Cart", new[] { "BillingContactID" });
            DropIndex("Shopping.Cart", new[] { "Active" });
            DropIndex("Shopping.Cart", new[] { "UpdatedDate" });
            DropIndex("Shopping.Cart", new[] { "CustomKey" });
            DropIndex("Shopping.Cart", new[] { "ID" });
            DropIndex("Discounts.CartItemDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.CartItemDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.CartItemDiscounts", new[] { "Active" });
            DropIndex("Discounts.CartItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.CartItemDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.CartItemDiscounts", new[] { "ID" });
            DropIndex("Shopping.CartItemAttribute", new[] { "MasterID" });
            DropIndex("Shopping.CartItemAttribute", new[] { "AttributeID" });
            DropIndex("Shopping.CartItemAttribute", new[] { "Active" });
            DropIndex("Shopping.CartItemAttribute", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartItemAttribute", new[] { "CustomKey" });
            DropIndex("Shopping.CartItemAttribute", new[] { "ID" });
            DropIndex("Shopping.CartItem", new[] { "StoreProductID" });
            DropIndex("Shopping.CartItem", new[] { "VendorProductID" });
            DropIndex("Shopping.CartItem", new[] { "StatusID" });
            DropIndex("Shopping.CartItem", new[] { "MasterID" });
            DropIndex("Shopping.CartItem", new[] { "UserID" });
            DropIndex("Shopping.CartItem", new[] { "ProductID" });
            DropIndex("Shopping.CartItem", new[] { "Name" });
            DropIndex("Shopping.CartItem", new[] { "Active" });
            DropIndex("Shopping.CartItem", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartItem", new[] { "CustomKey" });
            DropIndex("Shopping.CartItem", new[] { "ID" });
            DropIndex("Products.ProductAttribute", new[] { "MasterID" });
            DropIndex("Products.ProductAttribute", new[] { "AttributeValueID" });
            DropIndex("Products.ProductAttribute", new[] { "Active" });
            DropIndex("Products.ProductAttribute", new[] { "UpdatedDate" });
            DropIndex("Products.ProductAttribute", new[] { "CustomKey" });
            DropIndex("Products.ProductAttribute", new[] { "ID" });
            DropIndex("Products.Product", new[] { "TypeID" });
            DropIndex("Products.Product", new[] { "PalletID" });
            DropIndex("Products.Product", new[] { "MasterPackID" });
            DropIndex("Products.Product", new[] { "PackageID" });
            DropIndex("Products.Product", new[] { "ProductHash" });
            DropIndex("Products.Product", new[] { "Name" });
            DropIndex("Products.Product", new[] { "Active" });
            DropIndex("Products.Product", new[] { "UpdatedDate" });
            DropIndex("Products.Product", new[] { "CustomKey" });
            DropIndex("Products.Product", new[] { "ID" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "MasterID" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "AttributeValueID" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "Active" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "UpdatedDate" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "CustomKey" });
            DropIndex("Products.ProductCategoryAttribute", new[] { "ID" });
            DropIndex("Products.ProductCategory", new[] { "ProductID" });
            DropIndex("Products.ProductCategory", new[] { "CategoryID" });
            DropIndex("Products.ProductCategory", new[] { "Active" });
            DropIndex("Products.ProductCategory", new[] { "UpdatedDate" });
            DropIndex("Products.ProductCategory", new[] { "CustomKey" });
            DropIndex("Products.ProductCategory", new[] { "ID" });
            DropIndex("Categories.CategoryImage", new[] { "LibraryID" });
            DropIndex("Categories.CategoryImage", new[] { "CategoryID" });
            DropIndex("Categories.CategoryImage", new[] { "Active" });
            DropIndex("Categories.CategoryImage", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryImage", new[] { "CustomKey" });
            DropIndex("Categories.CategoryImage", new[] { "ID" });
            DropIndex("Media.FileData", new[] { "ID" });
            DropIndex("Media.Video", new[] { "FileID" });
            DropIndex("Media.Video", new[] { "ID" });
            DropIndex("Media.LibraryType", new[] { "SortOrder" });
            DropIndex("Media.LibraryType", new[] { "DisplayName" });
            DropIndex("Media.LibraryType", new[] { "Name" });
            DropIndex("Media.LibraryType", new[] { "Active" });
            DropIndex("Media.LibraryType", new[] { "UpdatedDate" });
            DropIndex("Media.LibraryType", new[] { "CustomKey" });
            DropIndex("Media.LibraryType", new[] { "ID" });
            DropIndex("Media.Image", new[] { "ThumbFileID" });
            DropIndex("Media.Image", new[] { "FullFileID" });
            DropIndex("Media.Image", new[] { "ID" });
            DropIndex("Media.Document", new[] { "FileID" });
            DropIndex("Media.Document", new[] { "ID" });
            DropIndex("Media.Library", new[] { "TypeID" });
            DropIndex("Media.Library", new[] { "Name" });
            DropIndex("Media.Library", new[] { "Active" });
            DropIndex("Media.Library", new[] { "UpdatedDate" });
            DropIndex("Media.Library", new[] { "CustomKey" });
            DropIndex("Media.Library", new[] { "ID" });
            DropIndex("Media.Audio", new[] { "ClipFileID" });
            DropIndex("Media.Audio", new[] { "FullFileID" });
            DropIndex("Media.Audio", new[] { "ID" });
            DropIndex("Media.File", new[] { "Active" });
            DropIndex("Media.File", new[] { "UpdatedDate" });
            DropIndex("Media.File", new[] { "CustomKey" });
            DropIndex("Media.File", new[] { "ID" });
            DropIndex("Categories.CategoryFile", new[] { "FileID" });
            DropIndex("Categories.CategoryFile", new[] { "CategoryID" });
            DropIndex("Categories.CategoryFile", new[] { "Name" });
            DropIndex("Categories.CategoryFile", new[] { "Active" });
            DropIndex("Categories.CategoryFile", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryFile", new[] { "CustomKey" });
            DropIndex("Categories.CategoryFile", new[] { "ID" });
            DropIndex("Categories.CategoryAttribute", new[] { "MasterID" });
            DropIndex("Categories.CategoryAttribute", new[] { "AttributeValueID" });
            DropIndex("Categories.CategoryAttribute", new[] { "Active" });
            DropIndex("Categories.CategoryAttribute", new[] { "UpdatedDate" });
            DropIndex("Categories.CategoryAttribute", new[] { "CustomKey" });
            DropIndex("Categories.CategoryAttribute", new[] { "ID" });
            DropIndex("Categories.Category", new[] { "DisplayName" });
            DropIndex("Categories.Category", new[] { "TypeID" });
            DropIndex("Categories.Category", new[] { "ParentID" });
            DropIndex("Categories.Category", new[] { "Name" });
            DropIndex("Categories.Category", new[] { "Active" });
            DropIndex("Categories.Category", new[] { "UpdatedDate" });
            DropIndex("Categories.Category", new[] { "CustomKey" });
            DropIndex("Categories.Category", new[] { "ID" });
            DropIndex("Discounts.DiscountCategories", new[] { "CategoryID" });
            DropIndex("Discounts.DiscountCategories", new[] { "DiscountID" });
            DropIndex("Discounts.DiscountCategories", new[] { "Active" });
            DropIndex("Discounts.DiscountCategories", new[] { "UpdatedDate" });
            DropIndex("Discounts.DiscountCategories", new[] { "CustomKey" });
            DropIndex("Discounts.DiscountCategories", new[] { "ID" });
            DropIndex("Discounts.Discount", new[] { "Name" });
            DropIndex("Discounts.Discount", new[] { "Active" });
            DropIndex("Discounts.Discount", new[] { "UpdatedDate" });
            DropIndex("Discounts.Discount", new[] { "CustomKey" });
            DropIndex("Discounts.Discount", new[] { "ID" });
            DropIndex("Discounts.SalesInvoiceDiscounts", new[] { "MasterID" });
            DropIndex("Discounts.SalesInvoiceDiscounts", new[] { "DiscountID" });
            DropIndex("Discounts.SalesInvoiceDiscounts", new[] { "Active" });
            DropIndex("Discounts.SalesInvoiceDiscounts", new[] { "UpdatedDate" });
            DropIndex("Discounts.SalesInvoiceDiscounts", new[] { "CustomKey" });
            DropIndex("Discounts.SalesInvoiceDiscounts", new[] { "ID" });
            DropIndex("Attributes.AttributeType", new[] { "SortOrder" });
            DropIndex("Attributes.AttributeType", new[] { "DisplayName" });
            DropIndex("Attributes.AttributeType", new[] { "Name" });
            DropIndex("Attributes.AttributeType", new[] { "Active" });
            DropIndex("Attributes.AttributeType", new[] { "UpdatedDate" });
            DropIndex("Attributes.AttributeType", new[] { "CustomKey" });
            DropIndex("Attributes.AttributeType", new[] { "ID" });
            DropIndex("Attributes.AttributeValue", new[] { "AttributeID" });
            DropIndex("Attributes.AttributeValue", new[] { "ParentID" });
            DropIndex("Attributes.AttributeValue", new[] { "Active" });
            DropIndex("Attributes.AttributeValue", new[] { "UpdatedDate" });
            DropIndex("Attributes.AttributeValue", new[] { "CustomKey" });
            DropIndex("Attributes.AttributeValue", new[] { "ID" });
            DropIndex("Attributes.GeneralAttribute", new[] { "TypeID" });
            DropIndex("Attributes.GeneralAttribute", new[] { "Name" });
            DropIndex("Attributes.GeneralAttribute", new[] { "Active" });
            DropIndex("Attributes.GeneralAttribute", new[] { "UpdatedDate" });
            DropIndex("Attributes.GeneralAttribute", new[] { "CustomKey" });
            DropIndex("Attributes.GeneralAttribute", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "MasterID" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "AttributeID" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceAttribute", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "ShipOptionID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "StoreID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "AccountID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "UserID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "TypeID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "StateID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "StatusID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "ShippingContactID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "BillingContactID" });
            DropIndex("Invoicing.SalesInvoice", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoice", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoice", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoice", new[] { "ID" });
            DropIndex("Invoicing.SalesOrderSalesInvoice", new[] { "SalesInvoiceID" });
            DropIndex("Invoicing.SalesOrderSalesInvoice", new[] { "SalesOrderID" });
            DropIndex("Invoicing.SalesOrderSalesInvoice", new[] { "Active" });
            DropIndex("Invoicing.SalesOrderSalesInvoice", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesOrderSalesInvoice", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesOrderSalesInvoice", new[] { "ID" });
            DropIndex("Ordering.SalesOrder", new[] { "CustomerPriorityID" });
            DropIndex("Ordering.SalesOrder", new[] { "StoreID" });
            DropIndex("Ordering.SalesOrder", new[] { "ParentID" });
            DropIndex("Ordering.SalesOrder", new[] { "AccountID" });
            DropIndex("Ordering.SalesOrder", new[] { "UserID" });
            DropIndex("Ordering.SalesOrder", new[] { "TypeID" });
            DropIndex("Ordering.SalesOrder", new[] { "StateID" });
            DropIndex("Ordering.SalesOrder", new[] { "StatusID" });
            DropIndex("Ordering.SalesOrder", new[] { "ShippingContactID" });
            DropIndex("Ordering.SalesOrder", new[] { "BillingContactID" });
            DropIndex("Ordering.SalesOrder", new[] { "Active" });
            DropIndex("Ordering.SalesOrder", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrder", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrder", new[] { "ID" });
            DropIndex("Purchasing.SalesOrderPurchaseOrder", new[] { "PurchaseOrderID" });
            DropIndex("Purchasing.SalesOrderPurchaseOrder", new[] { "SalesOrderID" });
            DropIndex("Purchasing.SalesOrderPurchaseOrder", new[] { "Active" });
            DropIndex("Purchasing.SalesOrderPurchaseOrder", new[] { "UpdatedDate" });
            DropIndex("Purchasing.SalesOrderPurchaseOrder", new[] { "CustomKey" });
            DropIndex("Purchasing.SalesOrderPurchaseOrder", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "VendorID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "VendorTermID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "ShipCarrierID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "FreeOnBoardID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "InventoryLocationID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "StoreID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "AccountID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "UserID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "TypeID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "StateID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "StatusID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "ShippingContactID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "BillingContactID" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrder", new[] { "ID" });
            DropIndex("Tax.TaxCountry", new[] { "CountryID" });
            DropIndex("Tax.TaxCountry", new[] { "Name" });
            DropIndex("Tax.TaxCountry", new[] { "Active" });
            DropIndex("Tax.TaxCountry", new[] { "UpdatedDate" });
            DropIndex("Tax.TaxCountry", new[] { "CustomKey" });
            DropIndex("Tax.TaxCountry", new[] { "ID" });
            DropIndex("Tax.TaxRegion", new[] { "RegionID" });
            DropIndex("Tax.TaxRegion", new[] { "Name" });
            DropIndex("Tax.TaxRegion", new[] { "Active" });
            DropIndex("Tax.TaxRegion", new[] { "UpdatedDate" });
            DropIndex("Tax.TaxRegion", new[] { "CustomKey" });
            DropIndex("Tax.TaxRegion", new[] { "ID" });
            DropIndex("Geography.InterRegion", new[] { "RelationRegionID" });
            DropIndex("Geography.InterRegion", new[] { "KeyRegionID" });
            DropIndex("Geography.InterRegion", new[] { "Active" });
            DropIndex("Geography.InterRegion", new[] { "UpdatedDate" });
            DropIndex("Geography.InterRegion", new[] { "CustomKey" });
            DropIndex("Geography.InterRegion", new[] { "ID" });
            DropIndex("Tax.TaxDistrict", new[] { "DistrictID" });
            DropIndex("Tax.TaxDistrict", new[] { "Name" });
            DropIndex("Tax.TaxDistrict", new[] { "Active" });
            DropIndex("Tax.TaxDistrict", new[] { "UpdatedDate" });
            DropIndex("Tax.TaxDistrict", new[] { "CustomKey" });
            DropIndex("Tax.TaxDistrict", new[] { "ID" });
            DropIndex("Geography.District", new[] { "RegionID" });
            DropIndex("Geography.District", new[] { "Name" });
            DropIndex("Geography.District", new[] { "Active" });
            DropIndex("Geography.District", new[] { "UpdatedDate" });
            DropIndex("Geography.District", new[] { "CustomKey" });
            DropIndex("Geography.District", new[] { "ID" });
            DropIndex("Geography.Region", new[] { "CountryID" });
            DropIndex("Geography.Region", new[] { "Name" });
            DropIndex("Geography.Region", new[] { "Active" });
            DropIndex("Geography.Region", new[] { "UpdatedDate" });
            DropIndex("Geography.Region", new[] { "CustomKey" });
            DropIndex("Geography.Region", new[] { "ID" });
            DropIndex("Geography.Country", new[] { "Name" });
            DropIndex("Geography.Country", new[] { "Active" });
            DropIndex("Geography.Country", new[] { "UpdatedDate" });
            DropIndex("Geography.Country", new[] { "CustomKey" });
            DropIndex("Geography.Country", new[] { "ID" });
            DropIndex("Geography.Address", new[] { "DistrictID" });
            DropIndex("Geography.Address", new[] { "RegionID" });
            DropIndex("Geography.Address", new[] { "CountryID" });
            DropIndex("Geography.Address", new[] { "Email" });
            DropIndex("Geography.Address", new[] { "Fax" });
            DropIndex("Geography.Address", new[] { "Phone" });
            DropIndex("Geography.Address", new[] { "Name" });
            DropIndex("Geography.Address", new[] { "Active" });
            DropIndex("Geography.Address", new[] { "UpdatedDate" });
            DropIndex("Geography.Address", new[] { "CustomKey" });
            DropIndex("Geography.Address", new[] { "ID" });
            DropIndex("Contacts.Contact", new[] { "ShippingAddressID" });
            DropIndex("Contacts.Contact", new[] { "AddressID" });
            DropIndex("Contacts.Contact", new[] { "TypeID" });
            DropIndex("Contacts.Contact", new[] { "Active" });
            DropIndex("Contacts.Contact", new[] { "UpdatedDate" });
            DropIndex("Contacts.Contact", new[] { "CustomKey" });
            DropIndex("Contacts.Contact", new[] { "ID" });
            DropIndex("Accounts.AccountContact", new[] { "ContactID" });
            DropIndex("Accounts.AccountContact", new[] { "AccountID" });
            DropIndex("Accounts.AccountContact", new[] { "Name" });
            DropIndex("Accounts.AccountContact", new[] { "Active" });
            DropIndex("Accounts.AccountContact", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountContact", new[] { "CustomKey" });
            DropIndex("Accounts.AccountContact", new[] { "ID" });
            DropIndex("Accounts.Account", new[] { "StatusID" });
            DropIndex("Accounts.Account", new[] { "TypeID" });
            DropIndex("Accounts.Account", new[] { "ParentID" });
            DropIndex("Accounts.Account", new[] { "Email" });
            DropIndex("Accounts.Account", new[] { "Fax" });
            DropIndex("Accounts.Account", new[] { "Phone" });
            DropIndex("Accounts.Account", new[] { "Name" });
            DropIndex("Accounts.Account", new[] { "Active" });
            DropIndex("Accounts.Account", new[] { "UpdatedDate" });
            DropIndex("Accounts.Account", new[] { "CustomKey" });
            DropIndex("Accounts.Account", new[] { "ID" });
            DropIndex("Accounts.AccountAddress", new[] { "AddressID" });
            DropIndex("Accounts.AccountAddress", new[] { "AccountID" });
            DropIndex("Accounts.AccountAddress", new[] { "Name" });
            DropIndex("Accounts.AccountAddress", new[] { "Active" });
            DropIndex("Accounts.AccountAddress", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountAddress", new[] { "CustomKey" });
            DropIndex("Accounts.AccountAddress", new[] { "ID" });
            DropTable("Geography.ZipCode");
            DropTable("Shipping.UPSEndOfDay");
            DropTable("System.SettingType");
            DropTable("System.SettingGroup");
            DropTable("System.Setting");
            DropTable("Hangfire.ScheduledJobConfigurationSetting");
            DropTable("Hangfire.ScheduledJobConfiguration");
            DropTable("Reporting.ReportTypes");
            DropTable("Reporting.Reports");
            DropTable("Contacts.ProfanityFilter");
            DropTable("Pricing.PriceRuleVendor");
            DropTable("Pricing.PriceRuleProductType");
            DropTable("Pricing.PriceRuleProduct");
            DropTable("Pricing.PriceRuleCategory");
            DropTable("Pricing.PriceRuleAccountType");
            DropTable("Pricing.PriceRule");
            DropTable("Pricing.PriceRuleAccount");
            DropTable("Notifications.Action");
            DropTable("Hangfire.Set");
            DropTable("Hangfire.Server");
            DropTable("Hangfire.Schema");
            DropTable("Hangfire.List");
            DropTable("Hangfire.JobQueue");
            DropTable("Hangfire.State");
            DropTable("Hangfire.Job");
            DropTable("Hangfire.JobParameter");
            DropTable("Hangfire.Hash");
            DropTable("Hangfire.Counter");
            DropTable("Hangfire.AggregatedCounter");
            DropTable("Tracking.VisitStatus");
            DropTable("Tracking.Visit");
            DropTable("Tracking.EventType");
            DropTable("Tracking.EventStatus");
            DropTable("Tracking.Visitor");
            DropTable("Tracking.PageViewType");
            DropTable("Tracking.PageViewStatus");
            DropTable("Tracking.PageView");
            DropTable("Tracking.PageViewEvent");
            DropTable("Tracking.IPOrganizationStatus");
            DropTable("Tracking.IPOrganization");
            DropTable("Tracking.Event");
            DropTable("System.SystemLog");
            DropTable("Shopping.Checkout");
            DropTable("Advertising.AdType");
            DropTable("Advertising.AdStatus");
            DropTable("Tracking.CampaignType");
            DropTable("Tracking.CampaignStatus");
            DropTable("Tracking.Campaign");
            DropTable("Tracking.CampaignAd");
            DropTable("Advertising.ZoneType");
            DropTable("Advertising.ZoneStatus");
            DropTable("Advertising.Zone");
            DropTable("Counters.CounterType");
            DropTable("Counters.CounterLogType");
            DropTable("Counters.CounterLog");
            DropTable("Counters.Counter");
            DropTable("Advertising.AdZoneAccess");
            DropTable("Advertising.AdZone");
            DropTable("Advertising.AdStore");
            DropTable("Advertising.AdImage");
            DropTable("Advertising.Ad");
            DropTable("Advertising.AdAccount");
            DropTable("Accounts.AccountType");
            DropTable("Accounts.AccountStatus");
            DropTable("Contacts.Opportunities");
            DropTable("Accounts.AccountAttribute");
            DropTable("Accounts.AccountTerm");
            DropTable("Accounts.AccountPricePoint");
            DropTable("Contacts.ContactType");
            DropTable("Contacts.Individual");
            DropTable("Contacts.Customer");
            DropTable("Purchasing.PurchaseOrderType");
            DropTable("Purchasing.PurchaseOrderStatus");
            DropTable("Purchasing.PurchaseOrderState");
            DropTable("Purchasing.PurchaseOrderItemStatus");
            DropTable("Discounts.PurchaseOrderItemDiscounts");
            DropTable("Purchasing.PurchaseOrderItemAttribute");
            DropTable("Purchasing.PurchaseOrderItem");
            DropTable("Purchasing.FreeOnBoard");
            DropTable("Discounts.PurchaseOrderDiscounts");
            DropTable("Purchasing.PurchaseOrderAttribute");
            DropTable("Ordering.SalesOrderType");
            DropTable("Ordering.SalesOrderStatus");
            DropTable("Ordering.SalesOrderState");
            DropTable("Payments.SalesOrderPayment");
            DropTable("Discounts.SalesOrderDiscounts");
            DropTable("Contacts.CustomerPriority");
            DropTable("Ordering.SalesOrderAttribute");
            DropTable("Invoicing.SalesInvoiceType");
            DropTable("Invoicing.SalesInvoiceStatus");
            DropTable("Invoicing.SalesInvoiceState");
            DropTable("Invoicing.SalesInvoiceItemStatus");
            DropTable("Discounts.SalesInvoiceItemDiscounts");
            DropTable("Invoicing.SalesInvoiceItemAttribute");
            DropTable("Invoicing.SalesInvoiceItem");
            DropTable("Payments.SalesInvoicePayment");
            DropTable("Discounts.DiscountStores");
            DropTable("Discounts.DiscountProductType");
            DropTable("Discounts.DiscountCode");
            DropTable("Products.ProductType");
            DropTable("Pricing.PriceRounding");
            DropTable("Products.ProductPricePoint");
            DropTable("Products.ProductImage");
            DropTable("Products.ProductFile");
            DropTable("Products.ProductAttributeFilterValue");
            DropTable("Products.ProductAssociationType");
            DropTable("Products.ProductAssociationAttribute");
            DropTable("Products.ProductAssociation");
            DropTable("Shipping.PackageType");
            DropTable("Shipping.Package");
            DropTable("Discounts.DiscountProducts");
            DropTable("Shopping.CartItemStatus");
            DropTable("Shopping.CartType");
            DropTable("Shopping.CartStatus");
            DropTable("Shopping.CartState");
            DropTable("System.NoteType");
            DropTable("Payments.Wallet");
            DropTable("Contacts.UserStatus");
            DropTable("Quoting.SalesQuoteType");
            DropTable("Quoting.SalesQuoteStatus");
            DropTable("Quoting.SalesQuoteState");
            DropTable("Shipping.ShipOption");
            DropTable("Quoting.SalesQuoteItemStatus");
            DropTable("Discounts.QuoteItemDiscounts");
            DropTable("Quoting.SalesQuoteItemAttribute");
            DropTable("Quoting.SalesQuoteItem");
            DropTable("Discounts.SalesQuoteDiscounts");
            DropTable("Quoting.SalesQuoteAttribute");
            DropTable("Quoting.SalesQuoteSalesOrder");
            DropTable("Quoting.SalesQuote");
            DropTable("Contacts.Permission");
            DropTable("Contacts.RolePermission");
            DropTable("Contacts.UserRole");
            DropTable("Contacts.RoleUser");
            DropTable("Notifications.Notification");
            DropTable("Notifications.NotificationMessage");
            DropTable("Messaging.EmailType");
            DropTable("Messaging.EmailStatus");
            DropTable("Messaging.EmailTemplate");
            DropTable("Messaging.EmailQueue");
            DropTable("Messaging.MessageRecipient");
            DropTable("Stores.StoreType");
            DropTable("Contacts.UserType");
            DropTable("Stores.StoreUserType");
            DropTable("Stores.StoreUser");
            DropTable("Products.ProductSubscriptionType");
            DropTable("Payments.SubscriptionType");
            DropTable("Payments.SubscriptionStatus");
            DropTable("Payments.RepeatType");
            DropTable("Payments.PaymentType");
            DropTable("Payments.SubscriptionHistory");
            DropTable("Payments.PaymentStatus");
            DropTable("Payments.PaymentMethod");
            DropTable("Payments.Payment");
            DropTable("Payments.Subscription");
            DropTable("Stores.StoreSubscription");
            DropTable("Stores.StoreImage");
            DropTable("Stores.StoreContact");
            DropTable("Categories.CategoryType");
            DropTable("Stores.StoreCategoryType");
            DropTable("Stores.StoreCategory");
            DropTable("Pricing.PricePoint");
            DropTable("Stores.StoreAccount");
            DropTable("Reviews.ReviewType");
            DropTable("Vendors.VendorTerm");
            DropTable("Vendors.Term");
            DropTable("Stores.StoreVendor");
            DropTable("Shipping.ShipmentType");
            DropTable("Shipping.ShipmentStatus");
            DropTable("Shipping.ShipmentEvent");
            DropTable("Vendors.ShipVia");
            DropTable("Shipping.ShipCarrierMethod");
            DropTable("Shipping.CarrierOrigin");
            DropTable("Shipping.CarrierInvoice");
            DropTable("Shipping.ShipCarrier");
            DropTable("Vendors.VendorProduct");
            DropTable("Stores.StoreProduct");
            DropTable("Ordering.SalesOrderItemStatus");
            DropTable("Discounts.SalesOrderItemDiscounts");
            DropTable("Ordering.SalesOrderItemAttribute");
            DropTable("Ordering.SalesOrderItem");
            DropTable("Shipping.SalesOrderItemShipment");
            DropTable("Products.ProductInventoryLocationSection");
            DropTable("Stores.StoreInventoryLocation");
            DropTable("Inventory.InventoryLocation");
            DropTable("Inventory.InventoryLocationSection");
            DropTable("Shipping.Shipment");
            DropTable("Contacts.ContactMethod");
            DropTable("Vendors.Vendor");
            DropTable("Vendors.VendorManufacturer");
            DropTable("Stores.StoreManufacturer");
            DropTable("Manufacturers.ManufacturerProduct");
            DropTable("Manufacturers.Manufacturer");
            DropTable("Reviews.Review");
            DropTable("Stores.StoreSiteDomain");
            DropTable("Stores.SocialProvider");
            DropTable("Stores.SiteDomainSocialProvider");
            DropTable("Stores.SiteDomain");
            DropTable("Stores.BrandSiteDomain");
            DropTable("Stores.Brand");
            DropTable("Stores.BrandStore");
            DropTable("Stores.Store");
            DropTable("Messaging.Conversation");
            DropTable("Messaging.Message");
            DropTable("Messaging.MessageAttachment");
            DropTable("Contacts.UserLogin");
            DropTable("Contacts.Favorite");
            DropTable("Contacts.UserClaim");
            DropTable("Contacts.UserAttribute");
            DropTable("Contacts.User");
            DropTable("System.Note");
            DropTable("Discounts.CartDiscounts");
            DropTable("Shopping.CartAttribute");
            DropTable("Shopping.Cart");
            DropTable("Discounts.CartItemDiscounts");
            DropTable("Shopping.CartItemAttribute");
            DropTable("Shopping.CartItem");
            DropTable("Products.ProductAttribute");
            DropTable("Products.Product");
            DropTable("Products.ProductCategoryAttribute");
            DropTable("Products.ProductCategory");
            DropTable("Categories.CategoryImage");
            DropTable("Media.FileData");
            DropTable("Media.Video");
            DropTable("Media.LibraryType");
            DropTable("Media.Image");
            DropTable("Media.Document");
            DropTable("Media.Library");
            DropTable("Media.Audio");
            DropTable("Media.File");
            DropTable("Categories.CategoryFile");
            DropTable("Categories.CategoryAttribute");
            DropTable("Categories.Category");
            DropTable("Discounts.DiscountCategories");
            DropTable("Discounts.Discount");
            DropTable("Discounts.SalesInvoiceDiscounts");
            DropTable("Attributes.AttributeType");
            DropTable("Attributes.AttributeValue");
            DropTable("Attributes.GeneralAttribute");
            DropTable("Invoicing.SalesInvoiceAttribute");
            DropTable("Invoicing.SalesInvoice");
            DropTable("Invoicing.SalesOrderSalesInvoice");
            DropTable("Ordering.SalesOrder");
            DropTable("Purchasing.SalesOrderPurchaseOrder");
            DropTable("Purchasing.PurchaseOrder");
            DropTable("Tax.TaxCountry");
            DropTable("Tax.TaxRegion");
            DropTable("Geography.InterRegion");
            DropTable("Tax.TaxDistrict");
            DropTable("Geography.District");
            DropTable("Geography.Region");
            DropTable("Geography.Country");
            DropTable("Geography.Address");
            DropTable("Contacts.Contact");
            DropTable("Accounts.AccountContact");
            DropTable("Accounts.Account");
            DropTable("Accounts.AccountAddress");
        }
    }
}
