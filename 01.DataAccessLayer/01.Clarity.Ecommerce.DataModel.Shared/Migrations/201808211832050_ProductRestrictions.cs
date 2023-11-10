// <copyright file="201808211832050_ProductRestrictions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201808211832050 product restrictions class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ProductRestrictions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Products.ProductRestriction",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    CustomKey = c.String(maxLength: 128, unicode: false),
                    CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    Active = c.Boolean(nullable: false),
                    Hash = c.Long(),
                    JsonAttributes = c.String(),
                    CanPurchaseInternationally = c.Boolean(nullable: false),
                    CanPurchaseDomestically = c.Boolean(nullable: false),
                    CanPurchaseIntraRegion = c.Boolean(nullable: false),
                    CanShipInternationally = c.Boolean(nullable: false),
                    CanShipDomestically = c.Boolean(nullable: false),
                    CanShipIntraRegion = c.Boolean(nullable: false),
                    ProductID = c.Int(nullable: false),
                    RestrictionsApplyToCity = c.String(maxLength: 128, unicode: false),
                    RestrictionsApplyToPostalCode = c.String(maxLength: 24, unicode: false),
                    RestrictionsApplyToCountryID = c.Int(),
                    RestrictionsApplyToRegionID = c.Int(),
                    RestrictionsApplyToDistrictID = c.Int(),
                    OverrideWithRoles = c.String(maxLength: 1024, unicode: false),
                    OverrideWithAccountTypeID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.AccountType", t => t.OverrideWithAccountTypeID)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("Geography.Country", t => t.RestrictionsApplyToCountryID)
                .ForeignKey("Geography.District", t => t.RestrictionsApplyToDistrictID)
                .ForeignKey("Geography.Region", t => t.RestrictionsApplyToRegionID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.ProductID)
                .Index(t => t.RestrictionsApplyToCountryID)
                .Index(t => t.RestrictionsApplyToRegionID)
                .Index(t => t.RestrictionsApplyToDistrictID)
                .Index(t => t.OverrideWithAccountTypeID);
        }

        public override void Down()
        {
            DropForeignKey("Products.ProductRestriction", "RestrictionsApplyToRegionID", "Geography.Region");
            DropForeignKey("Products.ProductRestriction", "RestrictionsApplyToDistrictID", "Geography.District");
            DropForeignKey("Products.ProductRestriction", "RestrictionsApplyToCountryID", "Geography.Country");
            DropForeignKey("Products.ProductRestriction", "ProductID", "Products.Product");
            DropForeignKey("Products.ProductRestriction", "OverrideWithAccountTypeID", "Accounts.AccountType");
            DropIndex("Products.ProductRestriction", new[] { "OverrideWithAccountTypeID" });
            DropIndex("Products.ProductRestriction", new[] { "RestrictionsApplyToDistrictID" });
            DropIndex("Products.ProductRestriction", new[] { "RestrictionsApplyToRegionID" });
            DropIndex("Products.ProductRestriction", new[] { "RestrictionsApplyToCountryID" });
            DropIndex("Products.ProductRestriction", new[] { "ProductID" });
            DropIndex("Products.ProductRestriction", new[] { "Hash" });
            DropIndex("Products.ProductRestriction", new[] { "Active" });
            DropIndex("Products.ProductRestriction", new[] { "UpdatedDate" });
            DropIndex("Products.ProductRestriction", new[] { "CreatedDate" });
            DropIndex("Products.ProductRestriction", new[] { "CustomKey" });
            DropIndex("Products.ProductRestriction", new[] { "ID" });
            DropTable("Products.ProductRestriction");
        }
    }
}
