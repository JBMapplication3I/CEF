// <copyright file="202010200015369_BrandCurrencyAndISOs.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202010200015369 brand currency and is operating system class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class BrandCurrencyAndISOs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Brands.BrandCurrency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        CustomName = c.String(maxLength: 128, unicode: false),
                        CustomTranslationKey = c.String(maxLength: 128, unicode: false),
                        OverrideUnicodeSymbolValue = c.Decimal(nullable: false, precision: 18, scale: 4),
                        OverrideHtmlCharacterCode = c.String(maxLength: 12, unicode: false),
                        OverrideRawCharacter = c.String(maxLength: 5),
                        OverrideDecimalPlaceAccuracy = c.Int(),
                        OverrideUseSeparator = c.Boolean(),
                        OverrideRawDecimalCharacter = c.String(maxLength: 5),
                        OverrideHtmlDecimalCharacterCode = c.String(maxLength: 12, unicode: false),
                        OverrideRawSeparatorCharacter = c.String(maxLength: 5),
                        OverrideHtmlSeparatorCharacterCode = c.String(maxLength: 12, unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Currencies.Currency", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("Geography.Country", "ISO3166Alpha2", c => c.String(maxLength: 2, unicode: false));
            AddColumn("Geography.Country", "ISO3166Alpha3", c => c.String(maxLength: 3, unicode: false));
            AddColumn("Geography.Country", "ISO3166Numeric", c => c.Int());
            AddColumn("Currencies.Currency", "ISO4217Alpha", c => c.String(maxLength: 3, unicode: false));
            AddColumn("Currencies.Currency", "ISO4217Numeric", c => c.Int());
            AddColumn("Currencies.Currency", "DecimalPlaceAccuracy", c => c.Int());
            AddColumn("Currencies.Currency", "UseSeparator", c => c.Boolean());
            AddColumn("Currencies.Currency", "RawDecimalCharacter", c => c.String(maxLength: 5));
            AddColumn("Currencies.Currency", "HtmlDecimalCharacterCode", c => c.String(maxLength: 12, unicode: false));
            AddColumn("Currencies.Currency", "RawSeparatorCharacter", c => c.String(maxLength: 5));
            AddColumn("Currencies.Currency", "HtmlSeparatorCharacterCode", c => c.String(maxLength: 12, unicode: false));
            AddColumn("Globalization.Language", "ISO639_1_2002", c => c.String(maxLength: 2, unicode: false));
            AddColumn("Globalization.Language", "ISO639_2_1998", c => c.String(maxLength: 3, unicode: false));
            AddColumn("Globalization.Language", "ISO639_3_2007", c => c.String(maxLength: 3, unicode: false));
            AddColumn("Globalization.Language", "ISO639_5_2008", c => c.String(maxLength: 3, unicode: false));
            AddColumn("Products.Product", "IndexSynonyms", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Accounts.AccountCurrency", "IsPrimary", c => c.Boolean(nullable: false));
            AddColumn("Accounts.AccountCurrency", "CustomTranslationKey", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Accounts.AccountCurrency", "OverrideUnicodeSymbolValue", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("Accounts.AccountCurrency", "OverrideHtmlCharacterCode", c => c.String(maxLength: 12, unicode: false));
            AddColumn("Accounts.AccountCurrency", "OverrideRawCharacter", c => c.String(maxLength: 5));
            AddColumn("Accounts.AccountCurrency", "OverrideDecimalPlaceAccuracy", c => c.Int());
            AddColumn("Accounts.AccountCurrency", "OverrideUseSeparator", c => c.Boolean());
            AddColumn("Accounts.AccountCurrency", "OverrideRawDecimalCharacter", c => c.String(maxLength: 5));
            AddColumn("Accounts.AccountCurrency", "OverrideHtmlDecimalCharacterCode", c => c.String(maxLength: 12, unicode: false));
            AddColumn("Accounts.AccountCurrency", "OverrideRawSeparatorCharacter", c => c.String(maxLength: 5));
            AddColumn("Accounts.AccountCurrency", "OverrideHtmlSeparatorCharacterCode", c => c.String(maxLength: 12, unicode: false));
        }

        public override void Down()
        {
            DropForeignKey("Brands.BrandCurrency", "SlaveID", "Currencies.Currency");
            DropForeignKey("Brands.BrandCurrency", "MasterID", "Brands.Brand");
            DropIndex("Brands.BrandCurrency", new[] { "Hash" });
            DropIndex("Brands.BrandCurrency", new[] { "Active" });
            DropIndex("Brands.BrandCurrency", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandCurrency", new[] { "CreatedDate" });
            DropIndex("Brands.BrandCurrency", new[] { "CustomKey" });
            DropIndex("Brands.BrandCurrency", new[] { "SlaveID" });
            DropIndex("Brands.BrandCurrency", new[] { "MasterID" });
            DropIndex("Brands.BrandCurrency", new[] { "ID" });
            DropColumn("Accounts.AccountCurrency", "OverrideHtmlSeparatorCharacterCode");
            DropColumn("Accounts.AccountCurrency", "OverrideRawSeparatorCharacter");
            DropColumn("Accounts.AccountCurrency", "OverrideHtmlDecimalCharacterCode");
            DropColumn("Accounts.AccountCurrency", "OverrideRawDecimalCharacter");
            DropColumn("Accounts.AccountCurrency", "OverrideUseSeparator");
            DropColumn("Accounts.AccountCurrency", "OverrideDecimalPlaceAccuracy");
            DropColumn("Accounts.AccountCurrency", "OverrideRawCharacter");
            DropColumn("Accounts.AccountCurrency", "OverrideHtmlCharacterCode");
            DropColumn("Accounts.AccountCurrency", "OverrideUnicodeSymbolValue");
            DropColumn("Accounts.AccountCurrency", "CustomTranslationKey");
            DropColumn("Accounts.AccountCurrency", "IsPrimary");
            DropColumn("Products.Product", "IndexSynonyms");
            DropColumn("Globalization.Language", "ISO639_5_2008");
            DropColumn("Globalization.Language", "ISO639_3_2007");
            DropColumn("Globalization.Language", "ISO639_2_1998");
            DropColumn("Globalization.Language", "ISO639_1_2002");
            DropColumn("Currencies.Currency", "HtmlSeparatorCharacterCode");
            DropColumn("Currencies.Currency", "RawSeparatorCharacter");
            DropColumn("Currencies.Currency", "HtmlDecimalCharacterCode");
            DropColumn("Currencies.Currency", "RawDecimalCharacter");
            DropColumn("Currencies.Currency", "UseSeparator");
            DropColumn("Currencies.Currency", "DecimalPlaceAccuracy");
            DropColumn("Currencies.Currency", "ISO4217Numeric");
            DropColumn("Currencies.Currency", "ISO4217Alpha");
            DropColumn("Geography.Country", "ISO3166Numeric");
            DropColumn("Geography.Country", "ISO3166Alpha3");
            DropColumn("Geography.Country", "ISO3166Alpha2");
            DropTable("Brands.BrandCurrency");
        }
    }
}
