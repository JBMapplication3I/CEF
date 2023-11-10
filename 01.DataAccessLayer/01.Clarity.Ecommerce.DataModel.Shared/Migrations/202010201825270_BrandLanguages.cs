// <copyright file="202010201825270_BrandLanguages.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202010201825270 brand languages class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class BrandLanguages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Brands.BrandLanguage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        OverrideLocale = c.String(maxLength: 128),
                        OverrideUnicodeName = c.String(maxLength: 1024),
                        OverrideISO639_1_2002 = c.String(maxLength: 2, unicode: false),
                        OverrideISO639_2_1998 = c.String(maxLength: 3, unicode: false),
                        OverrideISO639_3_2007 = c.String(maxLength: 3, unicode: false),
                        OverrideISO639_5_2008 = c.String(maxLength: 3, unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Globalization.Language", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.OverrideLocale)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);
        }

        public override void Down()
        {
            DropForeignKey("Brands.BrandLanguage", "SlaveID", "Globalization.Language");
            DropForeignKey("Brands.BrandLanguage", "MasterID", "Brands.Brand");
            DropIndex("Brands.BrandLanguage", new[] { "Hash" });
            DropIndex("Brands.BrandLanguage", new[] { "Active" });
            DropIndex("Brands.BrandLanguage", new[] { "UpdatedDate" });
            DropIndex("Brands.BrandLanguage", new[] { "CreatedDate" });
            DropIndex("Brands.BrandLanguage", new[] { "CustomKey" });
            DropIndex("Brands.BrandLanguage", new[] { "OverrideLocale" });
            DropIndex("Brands.BrandLanguage", new[] { "SlaveID" });
            DropIndex("Brands.BrandLanguage", new[] { "MasterID" });
            DropIndex("Brands.BrandLanguage", new[] { "ID" });
            DropTable("Brands.BrandLanguage");
        }
    }
}
