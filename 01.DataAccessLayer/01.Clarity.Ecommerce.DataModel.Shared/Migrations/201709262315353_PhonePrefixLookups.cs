// <copyright file="201709262315353_PhonePrefixLookups.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201709262315353 phone prefix lookups class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PhonePrefixLookups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Geography.PhonePrefixLookup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Prefix = c.String(maxLength: 20),
                        TimeZone = c.String(maxLength: 255),
                        CityName = c.String(maxLength: 255),
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
                .Index(t => t.Hash)
                .Index(t => t.Prefix)
                .Index(t => t.CountryID)
                .Index(t => t.RegionID)
                .Index(t => t.DistrictID);
        }

        public override void Down()
        {
            DropForeignKey("Geography.PhonePrefixLookup", "RegionID", "Geography.Region");
            DropForeignKey("Geography.PhonePrefixLookup", "DistrictID", "Geography.District");
            DropForeignKey("Geography.PhonePrefixLookup", "CountryID", "Geography.Country");
            DropIndex("Geography.PhonePrefixLookup", new[] { "DistrictID" });
            DropIndex("Geography.PhonePrefixLookup", new[] { "RegionID" });
            DropIndex("Geography.PhonePrefixLookup", new[] { "CountryID" });
            DropIndex("Geography.PhonePrefixLookup", new[] { "Prefix" });
            DropIndex("Geography.PhonePrefixLookup", new[] { "Hash" });
            DropIndex("Geography.PhonePrefixLookup", new[] { "Active" });
            DropIndex("Geography.PhonePrefixLookup", new[] { "UpdatedDate" });
            DropIndex("Geography.PhonePrefixLookup", new[] { "CustomKey" });
            DropIndex("Geography.PhonePrefixLookup", new[] { "ID" });
            DropTable("Geography.PhonePrefixLookup");
        }
    }
}
