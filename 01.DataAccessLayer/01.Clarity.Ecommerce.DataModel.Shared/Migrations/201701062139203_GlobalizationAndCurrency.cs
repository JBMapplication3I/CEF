// <copyright file="201701062139203_GlobalizationAndCurrency.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201701062139203 globalization and currency class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class GlobalizationAndCurrency : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Currencies.Currency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        UnicodeSymbolValue = c.Decimal(nullable: false, precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name);

            CreateTable(
                "Globalization.Language",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Locale = c.String(maxLength: 128),
                        UnicodeName = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Locale);

            CreateTable(
                "Globalization.UIKey",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active);

            CreateTable(
                "Globalization.UITranslation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Locale = c.String(maxLength: 1024),
                        Value = c.String(maxLength: 1024),
                        UiKeyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Globalization.UIKey", t => t.UiKeyID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.UiKeyID);

            AddColumn("Shopping.CartItem", "UnitCorePriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Shopping.CartItem", "UnitSoldPriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Shopping.CartItem", "OriginalCurrencyID", c => c.Int());
            AddColumn("Shopping.CartItem", "SellingCurrencyID", c => c.Int());
            AddColumn("Contacts.User", "CurrencyID", c => c.Int());
            AddColumn("Contacts.User", "LanguageID", c => c.Int());
            AddColumn("Stores.Store", "LanguageID", c => c.Int());
            AddColumn("Ordering.SalesOrderItem", "UnitCorePriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Ordering.SalesOrderItem", "UnitSoldPriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Ordering.SalesOrderItem", "OriginalCurrencyID", c => c.Int());
            AddColumn("Ordering.SalesOrderItem", "SellingCurrencyID", c => c.Int());
            AddColumn("Payments.Payment", "CurrencyID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItem", "UnitCorePriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Quoting.SalesQuoteItem", "UnitSoldPriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Quoting.SalesQuoteItem", "OriginalCurrencyID", c => c.Int());
            AddColumn("Quoting.SalesQuoteItem", "SellingCurrencyID", c => c.Int());
            AddColumn("Sampling.SampleRequestItem", "UnitCorePriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Sampling.SampleRequestItem", "UnitSoldPriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Sampling.SampleRequestItem", "OriginalCurrencyID", c => c.Int());
            AddColumn("Sampling.SampleRequestItem", "SellingCurrencyID", c => c.Int());
            AddColumn("Products.ProductPricePoint", "CurrencyID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItem", "UnitCorePriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Invoicing.SalesInvoiceItem", "UnitSoldPriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Invoicing.SalesInvoiceItem", "OriginalCurrencyID", c => c.Int());
            AddColumn("Invoicing.SalesInvoiceItem", "SellingCurrencyID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItem", "UnitCorePriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Purchasing.PurchaseOrderItem", "UnitSoldPriceInSellingCurrency", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Purchasing.PurchaseOrderItem", "OriginalCurrencyID", c => c.Int());
            AddColumn("Purchasing.PurchaseOrderItem", "SellingCurrencyID", c => c.Int());
            CreateIndex("Shopping.CartItem", "OriginalCurrencyID");
            CreateIndex("Shopping.CartItem", "SellingCurrencyID");
            CreateIndex("Contacts.User", "CurrencyID");
            CreateIndex("Contacts.User", "LanguageID");
            CreateIndex("Stores.Store", "LanguageID");
            CreateIndex("Ordering.SalesOrderItem", "OriginalCurrencyID");
            CreateIndex("Ordering.SalesOrderItem", "SellingCurrencyID");
            CreateIndex("Payments.Payment", "CurrencyID");
            CreateIndex("Quoting.SalesQuoteItem", "OriginalCurrencyID");
            CreateIndex("Quoting.SalesQuoteItem", "SellingCurrencyID");
            CreateIndex("Sampling.SampleRequestItem", "OriginalCurrencyID");
            CreateIndex("Sampling.SampleRequestItem", "SellingCurrencyID");
            CreateIndex("Products.ProductPricePoint", "CurrencyID");
            CreateIndex("Invoicing.SalesInvoiceItem", "OriginalCurrencyID");
            CreateIndex("Invoicing.SalesInvoiceItem", "SellingCurrencyID");
            CreateIndex("Purchasing.PurchaseOrderItem", "OriginalCurrencyID");
            CreateIndex("Purchasing.PurchaseOrderItem", "SellingCurrencyID");
            AddForeignKey("Contacts.User", "CurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Contacts.User", "LanguageID", "Globalization.Language", "ID");
            AddForeignKey("Stores.Store", "LanguageID", "Globalization.Language", "ID");
            AddForeignKey("Ordering.SalesOrderItem", "OriginalCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Ordering.SalesOrderItem", "SellingCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Payments.Payment", "CurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Quoting.SalesQuoteItem", "OriginalCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Quoting.SalesQuoteItem", "SellingCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Sampling.SampleRequestItem", "OriginalCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Sampling.SampleRequestItem", "SellingCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Shopping.CartItem", "OriginalCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Shopping.CartItem", "SellingCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Products.ProductPricePoint", "CurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItem", "OriginalCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Invoicing.SalesInvoiceItem", "SellingCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItem", "OriginalCurrencyID", "Currencies.Currency", "ID");
            AddForeignKey("Purchasing.PurchaseOrderItem", "SellingCurrencyID", "Currencies.Currency", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Globalization.UITranslation", "UiKeyID", "Globalization.UIKey");
            DropForeignKey("Purchasing.PurchaseOrderItem", "SellingCurrencyID", "Currencies.Currency");
            DropForeignKey("Purchasing.PurchaseOrderItem", "OriginalCurrencyID", "Currencies.Currency");
            DropForeignKey("Invoicing.SalesInvoiceItem", "SellingCurrencyID", "Currencies.Currency");
            DropForeignKey("Invoicing.SalesInvoiceItem", "OriginalCurrencyID", "Currencies.Currency");
            DropForeignKey("Products.ProductPricePoint", "CurrencyID", "Currencies.Currency");
            DropForeignKey("Shopping.CartItem", "SellingCurrencyID", "Currencies.Currency");
            DropForeignKey("Shopping.CartItem", "OriginalCurrencyID", "Currencies.Currency");
            DropForeignKey("Sampling.SampleRequestItem", "SellingCurrencyID", "Currencies.Currency");
            DropForeignKey("Sampling.SampleRequestItem", "OriginalCurrencyID", "Currencies.Currency");
            DropForeignKey("Quoting.SalesQuoteItem", "SellingCurrencyID", "Currencies.Currency");
            DropForeignKey("Quoting.SalesQuoteItem", "OriginalCurrencyID", "Currencies.Currency");
            DropForeignKey("Payments.Payment", "CurrencyID", "Currencies.Currency");
            DropForeignKey("Ordering.SalesOrderItem", "SellingCurrencyID", "Currencies.Currency");
            DropForeignKey("Ordering.SalesOrderItem", "OriginalCurrencyID", "Currencies.Currency");
            DropForeignKey("Stores.Store", "LanguageID", "Globalization.Language");
            DropForeignKey("Contacts.User", "LanguageID", "Globalization.Language");
            DropForeignKey("Contacts.User", "CurrencyID", "Currencies.Currency");
            DropIndex("Globalization.UITranslation", new[] { "UiKeyID" });
            DropIndex("Globalization.UITranslation", new[] { "Active" });
            DropIndex("Globalization.UITranslation", new[] { "UpdatedDate" });
            DropIndex("Globalization.UITranslation", new[] { "CustomKey" });
            DropIndex("Globalization.UITranslation", new[] { "ID" });
            DropIndex("Globalization.UIKey", new[] { "Active" });
            DropIndex("Globalization.UIKey", new[] { "UpdatedDate" });
            DropIndex("Globalization.UIKey", new[] { "CustomKey" });
            DropIndex("Globalization.UIKey", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "SellingCurrencyID" });
            DropIndex("Purchasing.PurchaseOrderItem", new[] { "OriginalCurrencyID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "SellingCurrencyID" });
            DropIndex("Invoicing.SalesInvoiceItem", new[] { "OriginalCurrencyID" });
            DropIndex("Products.ProductPricePoint", new[] { "CurrencyID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "SellingCurrencyID" });
            DropIndex("Sampling.SampleRequestItem", new[] { "OriginalCurrencyID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "SellingCurrencyID" });
            DropIndex("Quoting.SalesQuoteItem", new[] { "OriginalCurrencyID" });
            DropIndex("Payments.Payment", new[] { "CurrencyID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "SellingCurrencyID" });
            DropIndex("Ordering.SalesOrderItem", new[] { "OriginalCurrencyID" });
            DropIndex("Stores.Store", new[] { "LanguageID" });
            DropIndex("Globalization.Language", new[] { "Locale" });
            DropIndex("Globalization.Language", new[] { "Active" });
            DropIndex("Globalization.Language", new[] { "UpdatedDate" });
            DropIndex("Globalization.Language", new[] { "CustomKey" });
            DropIndex("Globalization.Language", new[] { "ID" });
            DropIndex("Currencies.Currency", new[] { "Name" });
            DropIndex("Currencies.Currency", new[] { "Active" });
            DropIndex("Currencies.Currency", new[] { "UpdatedDate" });
            DropIndex("Currencies.Currency", new[] { "CustomKey" });
            DropIndex("Currencies.Currency", new[] { "ID" });
            DropIndex("Contacts.User", new[] { "LanguageID" });
            DropIndex("Contacts.User", new[] { "CurrencyID" });
            DropIndex("Shopping.CartItem", new[] { "SellingCurrencyID" });
            DropIndex("Shopping.CartItem", new[] { "OriginalCurrencyID" });
            DropColumn("Purchasing.PurchaseOrderItem", "SellingCurrencyID");
            DropColumn("Purchasing.PurchaseOrderItem", "OriginalCurrencyID");
            DropColumn("Purchasing.PurchaseOrderItem", "UnitSoldPriceInSellingCurrency");
            DropColumn("Purchasing.PurchaseOrderItem", "UnitCorePriceInSellingCurrency");
            DropColumn("Invoicing.SalesInvoiceItem", "SellingCurrencyID");
            DropColumn("Invoicing.SalesInvoiceItem", "OriginalCurrencyID");
            DropColumn("Invoicing.SalesInvoiceItem", "UnitSoldPriceInSellingCurrency");
            DropColumn("Invoicing.SalesInvoiceItem", "UnitCorePriceInSellingCurrency");
            DropColumn("Products.ProductPricePoint", "CurrencyID");
            DropColumn("Sampling.SampleRequestItem", "SellingCurrencyID");
            DropColumn("Sampling.SampleRequestItem", "OriginalCurrencyID");
            DropColumn("Sampling.SampleRequestItem", "UnitSoldPriceInSellingCurrency");
            DropColumn("Sampling.SampleRequestItem", "UnitCorePriceInSellingCurrency");
            DropColumn("Quoting.SalesQuoteItem", "SellingCurrencyID");
            DropColumn("Quoting.SalesQuoteItem", "OriginalCurrencyID");
            DropColumn("Quoting.SalesQuoteItem", "UnitSoldPriceInSellingCurrency");
            DropColumn("Quoting.SalesQuoteItem", "UnitCorePriceInSellingCurrency");
            DropColumn("Payments.Payment", "CurrencyID");
            DropColumn("Ordering.SalesOrderItem", "SellingCurrencyID");
            DropColumn("Ordering.SalesOrderItem", "OriginalCurrencyID");
            DropColumn("Ordering.SalesOrderItem", "UnitSoldPriceInSellingCurrency");
            DropColumn("Ordering.SalesOrderItem", "UnitCorePriceInSellingCurrency");
            DropColumn("Stores.Store", "LanguageID");
            DropColumn("Contacts.User", "LanguageID");
            DropColumn("Contacts.User", "CurrencyID");
            DropColumn("Shopping.CartItem", "SellingCurrencyID");
            DropColumn("Shopping.CartItem", "OriginalCurrencyID");
            DropColumn("Shopping.CartItem", "UnitSoldPriceInSellingCurrency");
            DropColumn("Shopping.CartItem", "UnitCorePriceInSellingCurrency");
            DropTable("Globalization.UITranslation");
            DropTable("Globalization.UIKey");
            DropTable("Globalization.Language");
            DropTable("Currencies.Currency");
        }
    }
}
