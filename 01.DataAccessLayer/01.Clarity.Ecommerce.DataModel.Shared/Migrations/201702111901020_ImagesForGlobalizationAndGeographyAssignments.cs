// <copyright file="201702111901020_ImagesForGlobalizationAndGeographyAssignments.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201702111901020 images for globalization and geography assignments class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ImagesForGlobalizationAndGeographyAssignments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Geography.CountryCurrency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        CurrencyID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Currencies.Currency", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("Geography.Country", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.CurrencyID)
                .Index(t => t.MasterID);

            CreateTable(
                "Geography.CountryLanguage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        LanguageID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Globalization.Language", t => t.LanguageID, cascadeDelete: true)
                .ForeignKey("Geography.Country", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.LanguageID)
                .Index(t => t.MasterID);

            CreateTable(
                "Geography.RegionCurrency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        CurrencyID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Currencies.Currency", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("Geography.Region", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.CurrencyID)
                .Index(t => t.MasterID);

            CreateTable(
                "Geography.DistrictCurrency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        CurrencyID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Currencies.Currency", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("Geography.District", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.CurrencyID)
                .Index(t => t.MasterID);

            CreateTable(
                "Geography.DistrictLanguage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        LanguageID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Globalization.Language", t => t.LanguageID, cascadeDelete: true)
                .ForeignKey("Geography.District", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.LanguageID)
                .Index(t => t.MasterID);

            CreateTable(
                "Geography.RegionLanguage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        LanguageID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Globalization.Language", t => t.LanguageID, cascadeDelete: true)
                .ForeignKey("Geography.Region", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.LanguageID)
                .Index(t => t.MasterID);

            AddColumn("Geography.Country", "LibraryID", c => c.Int());
            AddColumn("Geography.Region", "LibraryID", c => c.Int());
            AddColumn("Geography.District", "LibraryID", c => c.Int());
            AddColumn("Currencies.Currency", "LibraryID", c => c.Int());
            AddColumn("Globalization.Language", "LibraryID", c => c.Int());
            CreateIndex("Geography.Country", "LibraryID");
            CreateIndex("Currencies.Currency", "LibraryID");
            CreateIndex("Globalization.Language", "LibraryID");
            CreateIndex("Geography.Region", "LibraryID");
            CreateIndex("Geography.District", "LibraryID");
            AddForeignKey("Currencies.Currency", "LibraryID", "Media.Library", "ID");
            AddForeignKey("Globalization.Language", "LibraryID", "Media.Library", "ID");
            AddForeignKey("Geography.Country", "LibraryID", "Media.Library", "ID");
            AddForeignKey("Geography.District", "LibraryID", "Media.Library", "ID");
            AddForeignKey("Geography.Region", "LibraryID", "Media.Library", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Geography.Region", "LibraryID", "Media.Library");
            DropForeignKey("Geography.RegionLanguage", "MasterID", "Geography.Region");
            DropForeignKey("Geography.RegionLanguage", "LanguageID", "Globalization.Language");
            DropForeignKey("Geography.District", "LibraryID", "Media.Library");
            DropForeignKey("Geography.DistrictLanguage", "MasterID", "Geography.District");
            DropForeignKey("Geography.DistrictLanguage", "LanguageID", "Globalization.Language");
            DropForeignKey("Geography.DistrictCurrency", "MasterID", "Geography.District");
            DropForeignKey("Geography.DistrictCurrency", "CurrencyID", "Currencies.Currency");
            DropForeignKey("Geography.RegionCurrency", "MasterID", "Geography.Region");
            DropForeignKey("Geography.RegionCurrency", "CurrencyID", "Currencies.Currency");
            DropForeignKey("Geography.Country", "LibraryID", "Media.Library");
            DropForeignKey("Geography.CountryLanguage", "MasterID", "Geography.Country");
            DropForeignKey("Geography.CountryLanguage", "LanguageID", "Globalization.Language");
            DropForeignKey("Globalization.Language", "LibraryID", "Media.Library");
            DropForeignKey("Geography.CountryCurrency", "MasterID", "Geography.Country");
            DropForeignKey("Geography.CountryCurrency", "CurrencyID", "Currencies.Currency");
            DropForeignKey("Currencies.Currency", "LibraryID", "Media.Library");
            DropIndex("Geography.RegionLanguage", new[] { "MasterID" });
            DropIndex("Geography.RegionLanguage", new[] { "LanguageID" });
            DropIndex("Geography.RegionLanguage", new[] { "Active" });
            DropIndex("Geography.RegionLanguage", new[] { "UpdatedDate" });
            DropIndex("Geography.RegionLanguage", new[] { "CustomKey" });
            DropIndex("Geography.RegionLanguage", new[] { "ID" });
            DropIndex("Geography.DistrictLanguage", new[] { "MasterID" });
            DropIndex("Geography.DistrictLanguage", new[] { "LanguageID" });
            DropIndex("Geography.DistrictLanguage", new[] { "Active" });
            DropIndex("Geography.DistrictLanguage", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictLanguage", new[] { "CustomKey" });
            DropIndex("Geography.DistrictLanguage", new[] { "ID" });
            DropIndex("Geography.DistrictCurrency", new[] { "MasterID" });
            DropIndex("Geography.DistrictCurrency", new[] { "CurrencyID" });
            DropIndex("Geography.DistrictCurrency", new[] { "Active" });
            DropIndex("Geography.DistrictCurrency", new[] { "UpdatedDate" });
            DropIndex("Geography.DistrictCurrency", new[] { "CustomKey" });
            DropIndex("Geography.DistrictCurrency", new[] { "ID" });
            DropIndex("Geography.District", new[] { "LibraryID" });
            DropIndex("Geography.RegionCurrency", new[] { "MasterID" });
            DropIndex("Geography.RegionCurrency", new[] { "CurrencyID" });
            DropIndex("Geography.RegionCurrency", new[] { "Active" });
            DropIndex("Geography.RegionCurrency", new[] { "UpdatedDate" });
            DropIndex("Geography.RegionCurrency", new[] { "CustomKey" });
            DropIndex("Geography.RegionCurrency", new[] { "ID" });
            DropIndex("Geography.Region", new[] { "LibraryID" });
            DropIndex("Globalization.Language", new[] { "LibraryID" });
            DropIndex("Geography.CountryLanguage", new[] { "MasterID" });
            DropIndex("Geography.CountryLanguage", new[] { "LanguageID" });
            DropIndex("Geography.CountryLanguage", new[] { "Active" });
            DropIndex("Geography.CountryLanguage", new[] { "UpdatedDate" });
            DropIndex("Geography.CountryLanguage", new[] { "CustomKey" });
            DropIndex("Geography.CountryLanguage", new[] { "ID" });
            DropIndex("Currencies.Currency", new[] { "LibraryID" });
            DropIndex("Geography.CountryCurrency", new[] { "MasterID" });
            DropIndex("Geography.CountryCurrency", new[] { "CurrencyID" });
            DropIndex("Geography.CountryCurrency", new[] { "Active" });
            DropIndex("Geography.CountryCurrency", new[] { "UpdatedDate" });
            DropIndex("Geography.CountryCurrency", new[] { "CustomKey" });
            DropIndex("Geography.CountryCurrency", new[] { "ID" });
            DropIndex("Geography.Country", new[] { "LibraryID" });
            DropColumn("Globalization.Language", "LibraryID");
            DropColumn("Currencies.Currency", "LibraryID");
            DropColumn("Geography.District", "LibraryID");
            DropColumn("Geography.Region", "LibraryID");
            DropColumn("Geography.Country", "LibraryID");
            DropTable("Geography.RegionLanguage");
            DropTable("Geography.DistrictLanguage");
            DropTable("Geography.DistrictCurrency");
            DropTable("Geography.RegionCurrency");
            DropTable("Geography.CountryLanguage");
            DropTable("Geography.CountryCurrency");
        }
    }
}
