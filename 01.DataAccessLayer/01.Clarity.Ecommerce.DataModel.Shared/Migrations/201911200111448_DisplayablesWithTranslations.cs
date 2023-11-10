// <copyright file="201911200111448_DisplayablesWithTranslations.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201911200111448 displayables with translations class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DisplayablesWithTranslations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Accounts.AccountCurrency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomName = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Currencies.Currency", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            AddColumn("Currencies.CurrencyImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Geography.CountryImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Globalization.LanguageImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Geography.DistrictImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Geography.RegionImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Accounts.AccountType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Pricing.PricePoint", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Brands.BrandImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Stores.StoreImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Manufacturers.ManufacturerImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Contacts.ContactMethod", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Vendors.VendorImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Accounts.AccountProductType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Shopping.CartItemStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Stores.StoreInventoryLocationType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Shipping.ShipmentStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Shipping.ShipmentType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Sales.SalesItemTargetType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Messaging.EmailStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Messaging.EmailType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Groups.GroupStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Groups.GroupType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Categories.CategoryImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Reviews.ReviewType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Categories.CategoryType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Contacts.UserImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Contacts.ReferralCodeStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Contacts.ReferralCodeType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Contacts.UserStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Payments.Membership", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Payments.MembershipLevel", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Counters.CounterLogType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Counters.CounterType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Tracking.CampaignStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Tracking.CampaignType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Advertising.AdImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Advertising.AdStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Advertising.AdType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Advertising.ZoneStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Advertising.ZoneType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Payments.RepeatType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Payments.SubscriptionStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Payments.PaymentStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Payments.PaymentType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Payments.SubscriptionType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Contacts.UserType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("CalendarEvents.CalendarEventImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("CalendarEvents.CalendarEventStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("CalendarEvents.CalendarEventType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("CalendarEvents.UserEventAttendanceType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Contacts.UserOnlineStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Products.ProductType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Products.ProductImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Shipping.PackageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Products.ProductAssociationType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Ordering.SalesOrderItemStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Returning.SalesReturnState", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Returning.SalesReturnStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Returning.SalesReturnType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Returning.SalesReturnReason", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Returning.SalesReturnItemStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Products.ProductStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Vendors.VendorType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Quoting.SalesQuoteItemStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Quoting.SalesQuoteState", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Quoting.SalesQuoteStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Quoting.SalesQuoteType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Sampling.SampleRequestItemStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Sampling.SampleRequestState", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Sampling.SampleRequestStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Sampling.SampleRequestType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Shopping.CartState", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Shopping.CartStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Shopping.CartType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Purchasing.PurchaseOrderItemStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Invoicing.SalesInvoiceItemStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("System.NoteType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Manufacturers.ManufacturerType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Badges.BadgeImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Badges.BadgeType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Stores.StoreType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Invoicing.SalesInvoiceState", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Invoicing.SalesInvoiceStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Invoicing.SalesInvoiceType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Ordering.SalesOrderState", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Ordering.SalesOrderStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Ordering.SalesOrderType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Purchasing.PurchaseOrderState", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Purchasing.PurchaseOrderStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Purchasing.PurchaseOrderType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Contacts.ContactImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Contacts.ContactType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Accounts.AccountImageType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Accounts.AccountStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Accounts.AccountAssociationType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Attributes.AttributeGroup", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Attributes.AttributeTab", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Attributes.AttributeType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Attributes.GeneralAttribute", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Tracking.IPOrganizationStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Tracking.PageViewStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Tracking.PageViewType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Tracking.VisitStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Tracking.EventStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Tracking.EventType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Products.FutureImportStatus", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Reporting.ReportTypes", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("System.SettingGroup", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("System.SettingType", "TranslationKey", c => c.String(maxLength: 128, unicode: false));
        }

        public override void Down()
        {
            DropForeignKey("Accounts.AccountCurrency", "SlaveID", "Currencies.Currency");
            DropForeignKey("Accounts.AccountCurrency", "MasterID", "Accounts.Account");
            DropIndex("Accounts.AccountCurrency", new[] { "SlaveID" });
            DropIndex("Accounts.AccountCurrency", new[] { "MasterID" });
            DropIndex("Accounts.AccountCurrency", new[] { "Hash" });
            DropIndex("Accounts.AccountCurrency", new[] { "Active" });
            DropIndex("Accounts.AccountCurrency", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountCurrency", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountCurrency", new[] { "CustomKey" });
            DropIndex("Accounts.AccountCurrency", new[] { "ID" });
            DropColumn("System.SettingType", "TranslationKey");
            DropColumn("System.SettingGroup", "TranslationKey");
            DropColumn("Reporting.ReportTypes", "TranslationKey");
            DropColumn("Products.FutureImportStatus", "TranslationKey");
            DropColumn("Tracking.EventType", "TranslationKey");
            DropColumn("Tracking.EventStatus", "TranslationKey");
            DropColumn("Tracking.VisitStatus", "TranslationKey");
            DropColumn("Tracking.PageViewType", "TranslationKey");
            DropColumn("Tracking.PageViewStatus", "TranslationKey");
            DropColumn("Tracking.IPOrganizationStatus", "TranslationKey");
            DropColumn("Attributes.GeneralAttribute", "TranslationKey");
            DropColumn("Attributes.AttributeType", "TranslationKey");
            DropColumn("Attributes.AttributeTab", "TranslationKey");
            DropColumn("Attributes.AttributeGroup", "TranslationKey");
            DropColumn("Accounts.AccountAssociationType", "TranslationKey");
            DropColumn("Accounts.AccountStatus", "TranslationKey");
            DropColumn("Accounts.AccountImageType", "TranslationKey");
            DropColumn("Contacts.ContactType", "TranslationKey");
            DropColumn("Contacts.ContactImageType", "TranslationKey");
            DropColumn("Purchasing.PurchaseOrderType", "TranslationKey");
            DropColumn("Purchasing.PurchaseOrderStatus", "TranslationKey");
            DropColumn("Purchasing.PurchaseOrderState", "TranslationKey");
            DropColumn("Ordering.SalesOrderType", "TranslationKey");
            DropColumn("Ordering.SalesOrderStatus", "TranslationKey");
            DropColumn("Ordering.SalesOrderState", "TranslationKey");
            DropColumn("Invoicing.SalesInvoiceType", "TranslationKey");
            DropColumn("Invoicing.SalesInvoiceStatus", "TranslationKey");
            DropColumn("Invoicing.SalesInvoiceState", "TranslationKey");
            DropColumn("Stores.StoreType", "TranslationKey");
            DropColumn("Badges.BadgeType", "TranslationKey");
            DropColumn("Badges.BadgeImageType", "TranslationKey");
            DropColumn("Manufacturers.ManufacturerType", "TranslationKey");
            DropColumn("System.NoteType", "TranslationKey");
            DropColumn("Invoicing.SalesInvoiceItemStatus", "TranslationKey");
            DropColumn("Purchasing.PurchaseOrderItemStatus", "TranslationKey");
            DropColumn("Shopping.CartType", "TranslationKey");
            DropColumn("Shopping.CartStatus", "TranslationKey");
            DropColumn("Shopping.CartState", "TranslationKey");
            DropColumn("Sampling.SampleRequestType", "TranslationKey");
            DropColumn("Sampling.SampleRequestStatus", "TranslationKey");
            DropColumn("Sampling.SampleRequestState", "TranslationKey");
            DropColumn("Sampling.SampleRequestItemStatus", "TranslationKey");
            DropColumn("Quoting.SalesQuoteType", "TranslationKey");
            DropColumn("Quoting.SalesQuoteStatus", "TranslationKey");
            DropColumn("Quoting.SalesQuoteState", "TranslationKey");
            DropColumn("Quoting.SalesQuoteItemStatus", "TranslationKey");
            DropColumn("Vendors.VendorType", "TranslationKey");
            DropColumn("Products.ProductStatus", "TranslationKey");
            DropColumn("Returning.SalesReturnItemStatus", "TranslationKey");
            DropColumn("Returning.SalesReturnReason", "TranslationKey");
            DropColumn("Returning.SalesReturnType", "TranslationKey");
            DropColumn("Returning.SalesReturnStatus", "TranslationKey");
            DropColumn("Returning.SalesReturnState", "TranslationKey");
            DropColumn("Ordering.SalesOrderItemStatus", "TranslationKey");
            DropColumn("Products.ProductAssociationType", "TranslationKey");
            DropColumn("Shipping.PackageType", "TranslationKey");
            DropColumn("Products.ProductImageType", "TranslationKey");
            DropColumn("Products.ProductType", "TranslationKey");
            DropColumn("Contacts.UserOnlineStatus", "TranslationKey");
            DropColumn("CalendarEvents.UserEventAttendanceType", "TranslationKey");
            DropColumn("CalendarEvents.CalendarEventType", "TranslationKey");
            DropColumn("CalendarEvents.CalendarEventStatus", "TranslationKey");
            DropColumn("CalendarEvents.CalendarEventImageType", "TranslationKey");
            DropColumn("Contacts.UserType", "TranslationKey");
            DropColumn("Payments.SubscriptionType", "TranslationKey");
            DropColumn("Payments.PaymentType", "TranslationKey");
            DropColumn("Payments.PaymentStatus", "TranslationKey");
            DropColumn("Payments.SubscriptionStatus", "TranslationKey");
            DropColumn("Payments.RepeatType", "TranslationKey");
            DropColumn("Advertising.ZoneType", "TranslationKey");
            DropColumn("Advertising.ZoneStatus", "TranslationKey");
            DropColumn("Advertising.AdType", "TranslationKey");
            DropColumn("Advertising.AdStatus", "TranslationKey");
            DropColumn("Advertising.AdImageType", "TranslationKey");
            DropColumn("Tracking.CampaignType", "TranslationKey");
            DropColumn("Tracking.CampaignStatus", "TranslationKey");
            DropColumn("Counters.CounterType", "TranslationKey");
            DropColumn("Counters.CounterLogType", "TranslationKey");
            DropColumn("Payments.MembershipLevel", "TranslationKey");
            DropColumn("Payments.Membership", "TranslationKey");
            DropColumn("Contacts.UserStatus", "TranslationKey");
            DropColumn("Contacts.ReferralCodeType", "TranslationKey");
            DropColumn("Contacts.ReferralCodeStatus", "TranslationKey");
            DropColumn("Contacts.UserImageType", "TranslationKey");
            DropColumn("Categories.CategoryType", "TranslationKey");
            DropColumn("Reviews.ReviewType", "TranslationKey");
            DropColumn("Categories.CategoryImageType", "TranslationKey");
            DropColumn("Groups.GroupType", "TranslationKey");
            DropColumn("Groups.GroupStatus", "TranslationKey");
            DropColumn("Messaging.EmailType", "TranslationKey");
            DropColumn("Messaging.EmailStatus", "TranslationKey");
            DropColumn("Sales.SalesItemTargetType", "TranslationKey");
            DropColumn("Shipping.ShipmentType", "TranslationKey");
            DropColumn("Shipping.ShipmentStatus", "TranslationKey");
            DropColumn("Stores.StoreInventoryLocationType", "TranslationKey");
            DropColumn("Shopping.CartItemStatus", "TranslationKey");
            DropColumn("Accounts.AccountProductType", "TranslationKey");
            DropColumn("Vendors.VendorImageType", "TranslationKey");
            DropColumn("Contacts.ContactMethod", "TranslationKey");
            DropColumn("Manufacturers.ManufacturerImageType", "TranslationKey");
            DropColumn("Stores.StoreImageType", "TranslationKey");
            DropColumn("Brands.BrandImageType", "TranslationKey");
            DropColumn("Pricing.PricePoint", "TranslationKey");
            DropColumn("Accounts.AccountType", "TranslationKey");
            DropColumn("Geography.RegionImageType", "TranslationKey");
            DropColumn("Geography.DistrictImageType", "TranslationKey");
            DropColumn("Globalization.LanguageImageType", "TranslationKey");
            DropColumn("Geography.CountryImageType", "TranslationKey");
            DropColumn("Currencies.CurrencyImageType", "TranslationKey");
            DropTable("Accounts.AccountCurrency");
        }
    }
}
