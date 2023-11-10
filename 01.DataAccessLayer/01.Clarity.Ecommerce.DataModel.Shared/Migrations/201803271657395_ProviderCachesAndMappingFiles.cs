// <copyright file="201803271657395_ProviderCachesAndMappingFiles.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201803271657395 provider caches and mapping files class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ProviderCachesAndMappingFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Geography.HistoricalAddressValidation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        AddressHash = c.Long(),
                        OnDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsValid = c.Boolean(nullable: false),
                        Provider = c.String(maxLength: 128, unicode: false),
                        SerializedRequest = c.String(),
                        SerializedResponse = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Tax.HistoricalTaxRate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Provider = c.String(maxLength: 128, unicode: false),
                        CartHash = c.Long(),
                        OnDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CountryLevelRate = c.Decimal(precision: 7, scale: 6),
                        RegionLevelRate = c.Decimal(precision: 7, scale: 6),
                        DistrictLevelRate = c.Decimal(precision: 7, scale: 6),
                        CountyLevelRate = c.Decimal(precision: 7, scale: 6),
                        TotalAmount = c.Decimal(precision: 7, scale: 6),
                        TotalTaxable = c.Decimal(precision: 7, scale: 6),
                        TotalTax = c.Decimal(precision: 7, scale: 6),
                        TotalTaxCalculated = c.Decimal(precision: 7, scale: 6),
                        Rate = c.Decimal(nullable: false, precision: 7, scale: 6),
                        SerializedRequest = c.String(),
                        SerializedResponse = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "System.ImportExportMapping",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        MappingJson = c.String(nullable: false),
                        MappingJsonHash = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name);

            AddColumn("Attributes.GeneralAttribute", "IsTab", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropIndex("System.ImportExportMapping", new[] { "Name" });
            DropIndex("System.ImportExportMapping", new[] { "Hash" });
            DropIndex("System.ImportExportMapping", new[] { "Active" });
            DropIndex("System.ImportExportMapping", new[] { "UpdatedDate" });
            DropIndex("System.ImportExportMapping", new[] { "CustomKey" });
            DropIndex("System.ImportExportMapping", new[] { "ID" });
            DropIndex("Tax.HistoricalTaxRate", new[] { "Hash" });
            DropIndex("Tax.HistoricalTaxRate", new[] { "Active" });
            DropIndex("Tax.HistoricalTaxRate", new[] { "UpdatedDate" });
            DropIndex("Tax.HistoricalTaxRate", new[] { "CustomKey" });
            DropIndex("Tax.HistoricalTaxRate", new[] { "ID" });
            DropIndex("Geography.HistoricalAddressValidation", new[] { "Hash" });
            DropIndex("Geography.HistoricalAddressValidation", new[] { "Active" });
            DropIndex("Geography.HistoricalAddressValidation", new[] { "UpdatedDate" });
            DropIndex("Geography.HistoricalAddressValidation", new[] { "CustomKey" });
            DropIndex("Geography.HistoricalAddressValidation", new[] { "ID" });
            DropColumn("Attributes.GeneralAttribute", "IsTab");
            DropTable("System.ImportExportMapping");
            DropTable("Tax.HistoricalTaxRate");
            DropTable("Geography.HistoricalAddressValidation");
        }
    }
}
