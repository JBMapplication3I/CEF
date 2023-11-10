// <copyright file="201701111447011_SavedSellers.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201701111447011 saved sellers class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SavedSellers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Currencies.HistoricalCurrencyRate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 24, scale: 20),
                        OnDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StartingCurrencyID = c.Int(nullable: false),
                        EndingCurrencyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Currencies.Currency", t => t.EndingCurrencyID)
                .ForeignKey("Currencies.Currency", t => t.StartingCurrencyID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.StartingCurrencyID)
                .Index(t => t.EndingCurrencyID);

            CreateTable(
                "Favorites.FavoriteCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        FavoriteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.FavoriteID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.UserID)
                .Index(t => t.FavoriteID);

            CreateTable(
                "Favorites.FavoriteManufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        FavoriteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Manufacturers.Manufacturer", t => t.FavoriteID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.UserID)
                .Index(t => t.FavoriteID);

            CreateTable(
                "Favorites.FavoriteStore",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        FavoriteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.FavoriteID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.UserID)
                .Index(t => t.FavoriteID);

            CreateTable(
                "Favorites.FavoriteVendor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        FavoriteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Vendors.Vendor", t => t.FavoriteID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.UserID)
                .Index(t => t.FavoriteID);

            CreateTable(
                "Favorites.FavoriteShipCarrier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        FavoriteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Shipping.ShipCarrier", t => t.FavoriteID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.UserID)
                .Index(t => t.FavoriteID);

            const string Body = @"
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

-- Insert statements for procedure here
IF NOT EXISTS(SELECT * FROM [Globalization].[UIKey] WHERE [Active] = 1 AND [CustomKey] = @key)
BEGIN
    INSERT INTO [Globalization].[UIKey] ([CreatedDate],[Active],[CustomKey]) VALUES (CURRENT_TIMESTAMP, 1, @key)
END

DECLARE @id_of_key INT = (SELECT TOP 1 [ID] FROM [Globalization].[UIKey] WHERE [Active] = 1 AND [CustomKey] = @key);

IF @id_of_key IS NULL
BEGIN
    print 'No id for key ' + @key
END

IF EXISTS(SELECT * FROM [Globalization].[UITranslation] WHERE [Active] = 1 AND [UiKeyID] = @id_of_key AND Locale = @locale)
BEGIN
    UPDATE [Globalization].[UITranslation] SET [Value] = @value WHERE [Active] = 1 AND [UiKeyID] = @id_of_key AND Locale = @locale
END
ELSE
BEGIN
    INSERT INTO [Globalization].[UITranslation]
           ([CreatedDate],     [Active],[Locale],[UiKeyID], [CustomKey],           [Value])
    VALUES (CURRENT_TIMESTAMP, 1,       @locale, @id_of_key, @key + '|' + @locale, @value)
END";
            CreateStoredProcedure(
                "Globalization.UpsertUiKeysAndTranslations",
                p => new
                {
                    locale = p.String(maxLength: 20),
                    key = p.String(maxLength: 512),
                    value = p.String(),
                },
                body: Body);
        }

        public override void Down()
        {
            DropStoredProcedure("Globalization.UpsertUiKeysAndTranslations");
            DropForeignKey("Favorites.FavoriteShipCarrier", "UserID", "Contacts.User");
            DropForeignKey("Favorites.FavoriteShipCarrier", "FavoriteID", "Shipping.ShipCarrier");
            DropForeignKey("Favorites.FavoriteVendor", "UserID", "Contacts.User");
            DropForeignKey("Favorites.FavoriteVendor", "FavoriteID", "Vendors.Vendor");
            DropForeignKey("Favorites.FavoriteStore", "UserID", "Contacts.User");
            DropForeignKey("Favorites.FavoriteStore", "FavoriteID", "Stores.Store");
            DropForeignKey("Favorites.FavoriteManufacturer", "UserID", "Contacts.User");
            DropForeignKey("Favorites.FavoriteManufacturer", "FavoriteID", "Manufacturers.Manufacturer");
            DropForeignKey("Favorites.FavoriteCategory", "UserID", "Contacts.User");
            DropForeignKey("Favorites.FavoriteCategory", "FavoriteID", "Categories.Category");
            DropForeignKey("Currencies.HistoricalCurrencyRate", "StartingCurrencyID", "Currencies.Currency");
            DropForeignKey("Currencies.HistoricalCurrencyRate", "EndingCurrencyID", "Currencies.Currency");
            DropIndex("Favorites.FavoriteShipCarrier", new[] { "FavoriteID" });
            DropIndex("Favorites.FavoriteShipCarrier", new[] { "UserID" });
            DropIndex("Favorites.FavoriteShipCarrier", new[] { "Active" });
            DropIndex("Favorites.FavoriteShipCarrier", new[] { "UpdatedDate" });
            DropIndex("Favorites.FavoriteShipCarrier", new[] { "CustomKey" });
            DropIndex("Favorites.FavoriteShipCarrier", new[] { "ID" });
            DropIndex("Favorites.FavoriteVendor", new[] { "FavoriteID" });
            DropIndex("Favorites.FavoriteVendor", new[] { "UserID" });
            DropIndex("Favorites.FavoriteVendor", new[] { "Active" });
            DropIndex("Favorites.FavoriteVendor", new[] { "UpdatedDate" });
            DropIndex("Favorites.FavoriteVendor", new[] { "CustomKey" });
            DropIndex("Favorites.FavoriteVendor", new[] { "ID" });
            DropIndex("Favorites.FavoriteStore", new[] { "FavoriteID" });
            DropIndex("Favorites.FavoriteStore", new[] { "UserID" });
            DropIndex("Favorites.FavoriteStore", new[] { "Active" });
            DropIndex("Favorites.FavoriteStore", new[] { "UpdatedDate" });
            DropIndex("Favorites.FavoriteStore", new[] { "CustomKey" });
            DropIndex("Favorites.FavoriteStore", new[] { "ID" });
            DropIndex("Favorites.FavoriteManufacturer", new[] { "FavoriteID" });
            DropIndex("Favorites.FavoriteManufacturer", new[] { "UserID" });
            DropIndex("Favorites.FavoriteManufacturer", new[] { "Active" });
            DropIndex("Favorites.FavoriteManufacturer", new[] { "UpdatedDate" });
            DropIndex("Favorites.FavoriteManufacturer", new[] { "CustomKey" });
            DropIndex("Favorites.FavoriteManufacturer", new[] { "ID" });
            DropIndex("Favorites.FavoriteCategory", new[] { "FavoriteID" });
            DropIndex("Favorites.FavoriteCategory", new[] { "UserID" });
            DropIndex("Favorites.FavoriteCategory", new[] { "Active" });
            DropIndex("Favorites.FavoriteCategory", new[] { "UpdatedDate" });
            DropIndex("Favorites.FavoriteCategory", new[] { "CustomKey" });
            DropIndex("Favorites.FavoriteCategory", new[] { "ID" });
            DropIndex("Currencies.HistoricalCurrencyRate", new[] { "EndingCurrencyID" });
            DropIndex("Currencies.HistoricalCurrencyRate", new[] { "StartingCurrencyID" });
            DropIndex("Currencies.HistoricalCurrencyRate", new[] { "Active" });
            DropIndex("Currencies.HistoricalCurrencyRate", new[] { "UpdatedDate" });
            DropIndex("Currencies.HistoricalCurrencyRate", new[] { "CustomKey" });
            DropIndex("Currencies.HistoricalCurrencyRate", new[] { "ID" });
            DropTable("Favorites.FavoriteShipCarrier");
            DropTable("Favorites.FavoriteVendor");
            DropTable("Favorites.FavoriteStore");
            DropTable("Favorites.FavoriteManufacturer");
            DropTable("Favorites.FavoriteCategory");
            DropTable("Currencies.HistoricalCurrencyRate");
        }
    }
}
