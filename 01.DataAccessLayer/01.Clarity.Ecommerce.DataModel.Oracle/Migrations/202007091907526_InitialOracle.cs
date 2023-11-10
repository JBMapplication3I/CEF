// <copyright file="202007091907526_InitialOracle.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202007091907526 initial oracle class</summary>
namespace Clarity.Ecommerce.DataModel.Oracle.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialOracle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CLARITY.AccountAssociations",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.MasterID)
                .ForeignKey("CLARITY.Accounts", t => t.SlaveID)
                .ForeignKey("CLARITY.AccountAssociationTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.TypeID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Accounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IsTaxable = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TaxExemptionNo = c.String(maxLength: 128),
                        TaxEntityUseCode = c.String(maxLength: 128),
                        IsOnHold = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Credit = c.Decimal(precision: 18, scale: 2),
                        Token = c.String(maxLength: 128),
                        SageID = c.String(maxLength: 128),
                        CreditCurrencyID = c.Decimal(precision: 10, scale: 0),
                        Phone = c.String(maxLength: 64),
                        Fax = c.String(maxLength: 64),
                        Email = c.String(maxLength: 256),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Currencies", t => t.CreditCurrencyID)
                .ForeignKey("CLARITY.Accounts", t => t.ParentID)
                .ForeignKey("CLARITY.AccountStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.AccountTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.ParentID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.CreditCurrencyID)
                .Index(t => t.Phone)
                .Index(t => t.Fax)
                .Index(t => t.Email)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountContacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsBilling = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TransmittedToERP = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Contacts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Contacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FirstName = c.String(maxLength: 100),
                        MiddleName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        FullName = c.String(maxLength: 300),
                        Phone1 = c.String(maxLength: 50),
                        Phone2 = c.String(maxLength: 50),
                        Phone3 = c.String(maxLength: 50),
                        Fax1 = c.String(maxLength: 50),
                        Email1 = c.String(maxLength: 1000),
                        Email2 = c.String(maxLength: 1000),
                        Email3 = c.String(maxLength: 1000),
                        Website1 = c.String(maxLength: 1000),
                        Website2 = c.String(maxLength: 1000),
                        Website3 = c.String(maxLength: 1000),
                        AddressID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Addresses", t => t.AddressID)
                .ForeignKey("CLARITY.ContactTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.AddressID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Addresses",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 255),
                        Street1 = c.String(maxLength: 255),
                        Street2 = c.String(maxLength: 255),
                        Street3 = c.String(maxLength: 255),
                        City = c.String(maxLength: 100),
                        DistrictCustom = c.String(maxLength: 100),
                        RegionCustom = c.String(maxLength: 100),
                        CountryCustom = c.String(maxLength: 100),
                        PostalCode = c.String(maxLength: 50),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        CountryID = c.Decimal(precision: 10, scale: 0),
                        RegionID = c.Decimal(precision: 10, scale: 0),
                        DistrictID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Countries", t => t.CountryID)
                .ForeignKey("CLARITY.Districts", t => t.DistrictID)
                .ForeignKey("CLARITY.Regions", t => t.RegionID)
                .Index(t => t.ID)
                .Index(t => t.CountryID)
                .Index(t => t.RegionID)
                .Index(t => t.DistrictID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Countries",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        PhoneRegEx = c.String(maxLength: 512),
                        PhonePrefix = c.String(maxLength: 10),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CountryCurrencies",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Countries", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Currencies",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UnicodeSymbolValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CurrencyConversions",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        StartingCurrencyID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        EndingCurrencyID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Currencies", t => t.EndingCurrencyID)
                .ForeignKey("CLARITY.Currencies", t => t.StartingCurrencyID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StartingCurrencyID)
                .Index(t => t.EndingCurrencyID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.HistoricalCurrencyRates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OnDate = c.DateTime(nullable: false),
                        StartingCurrencyID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        EndingCurrencyID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Currencies", t => t.EndingCurrencyID)
                .ForeignKey("CLARITY.Currencies", t => t.StartingCurrencyID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StartingCurrencyID)
                .Index(t => t.EndingCurrencyID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CurrencyImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Currencies", t => t.MasterID)
                .ForeignKey("CLARITY.CurrencyImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CurrencyImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CountryImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Countries", t => t.MasterID)
                .ForeignKey("CLARITY.CountryImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CountryImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CountryLanguages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Countries", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Languages", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Languages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Locale = c.String(maxLength: 128),
                        UnicodeName = c.String(maxLength: 1024),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Locale)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.LanguageImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Languages", t => t.MasterID)
                .ForeignKey("CLARITY.LanguageImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.LanguageImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Regions",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        CountryID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Countries", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CountryID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.RegionCurrencies",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Regions", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Districts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        RegionID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Regions", t => t.RegionID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.RegionID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DistrictCurrencies",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Districts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DistrictImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Districts", t => t.MasterID)
                .ForeignKey("CLARITY.DistrictImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DistrictImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DistrictLanguages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Districts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Languages", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.TaxDistricts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 7, scale: 6),
                        DistrictID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Districts", t => t.DistrictID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.DistrictID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.RegionImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Regions", t => t.MasterID)
                .ForeignKey("CLARITY.RegionImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.RegionImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.InterRegions",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Notes = c.String(maxLength: 100),
                        KeyRegionID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RelationRegionID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Regions", t => t.KeyRegionID, cascadeDelete: true)
                .ForeignKey("CLARITY.Regions", t => t.RelationRegionID)
                .Index(t => t.ID)
                .Index(t => t.KeyRegionID)
                .Index(t => t.RelationRegionID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.RegionLanguages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Regions", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Languages", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.TaxRegions",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 7, scale: 6),
                        RegionID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Regions", t => t.RegionID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.RegionID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.TaxCountries",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 7, scale: 6),
                        CountryID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Countries", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CountryID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PurchaseOrders",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DueDate = c.DateTime(),
                        SubtotalItems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalTaxes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalHandling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalDiscounts = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingSameAsBilling = c.Decimal(precision: 1, scale: 0),
                        BillingContactID = c.Decimal(precision: 10, scale: 0),
                        ShippingContactID = c.Decimal(precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StateID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        TrackingNumber = c.String(maxLength: 50),
                        ReleaseDate = c.DateTime(),
                        EstimatedReceiptDate = c.DateTime(),
                        ActualReceiptDate = c.DateTime(),
                        InventoryLocationID = c.Decimal(precision: 10, scale: 0),
                        ShipCarrierID = c.Decimal(precision: 10, scale: 0),
                        VendorID = c.Decimal(precision: 10, scale: 0),
                        SalesGroupID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.Contacts", t => t.BillingContactID)
                .ForeignKey("CLARITY.PurchaseOrders", t => t.ParentID)
                .ForeignKey("CLARITY.InventoryLocations", t => t.InventoryLocationID)
                .ForeignKey("CLARITY.SalesGroups", t => t.SalesGroupID)
                .ForeignKey("CLARITY.ShipCarriers", t => t.ShipCarrierID)
                .ForeignKey("CLARITY.Contacts", t => t.ShippingContactID)
                .ForeignKey("CLARITY.PurchaseOrderStates", t => t.StateID, cascadeDelete: true)
                .ForeignKey("CLARITY.PurchaseOrderStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.PurchaseOrderTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .ForeignKey("CLARITY.Vendors", t => t.VendorID)
                .Index(t => t.ID)
                .Index(t => t.BillingContactID)
                .Index(t => t.ShippingContactID)
                .Index(t => t.StatusID)
                .Index(t => t.StateID)
                .Index(t => t.TypeID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.StoreID)
                .Index(t => t.ParentID)
                .Index(t => t.InventoryLocationID)
                .Index(t => t.ShipCarrierID)
                .Index(t => t.VendorID)
                .Index(t => t.SalesGroupID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderPurchaseOrders",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.PurchaseOrders", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrders",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DueDate = c.DateTime(),
                        SubtotalItems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalTaxes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalHandling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalDiscounts = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingSameAsBilling = c.Decimal(precision: 1, scale: 0),
                        BillingContactID = c.Decimal(precision: 10, scale: 0),
                        ShippingContactID = c.Decimal(precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StateID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        PurchaseOrderNumber = c.String(maxLength: 100),
                        BalanceDue = c.Decimal(precision: 18, scale: 2),
                        OrderStateName = c.String(),
                        TrackingNumber = c.String(),
                        PaymentTransactionID = c.String(maxLength: 256),
                        TaxTransactionID = c.String(maxLength: 256),
                        OrderApprovedDate = c.DateTime(),
                        OrderCommitmentDate = c.DateTime(),
                        RequiredShipDate = c.DateTime(),
                        RequestedShipDate = c.DateTime(),
                        ActualShipDate = c.DateTime(),
                        SalesGroupAsMasterID = c.Decimal(precision: 10, scale: 0),
                        SalesGroupAsSubID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.SalesGroups", t => t.SalesGroupAsMasterID)
                .ForeignKey("CLARITY.SalesGroups", t => t.SalesGroupAsSubID)
                .ForeignKey("CLARITY.Contacts", t => t.BillingContactID)
                .ForeignKey("CLARITY.SalesOrders", t => t.ParentID)
                .ForeignKey("CLARITY.Contacts", t => t.ShippingContactID)
                .ForeignKey("CLARITY.SalesOrderStates", t => t.StateID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesOrderStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.SalesOrderTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.BillingContactID)
                .Index(t => t.ShippingContactID)
                .Index(t => t.StatusID)
                .Index(t => t.StateID)
                .Index(t => t.TypeID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.StoreID)
                .Index(t => t.ParentID)
                .Index(t => t.SalesGroupAsMasterID)
                .Index(t => t.SalesGroupAsSubID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderSalesInvoices",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesInvoices", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoices",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DueDate = c.DateTime(),
                        SubtotalItems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalTaxes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalHandling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalDiscounts = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingSameAsBilling = c.Decimal(precision: 1, scale: 0),
                        BillingContactID = c.Decimal(precision: 10, scale: 0),
                        ShippingContactID = c.Decimal(precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StateID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        BalanceDue = c.Decimal(precision: 18, scale: 2),
                        SalesGroupID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.Contacts", t => t.BillingContactID)
                .ForeignKey("CLARITY.SalesInvoices", t => t.ParentID)
                .ForeignKey("CLARITY.SalesGroups", t => t.SalesGroupID)
                .ForeignKey("CLARITY.Contacts", t => t.ShippingContactID)
                .ForeignKey("CLARITY.SalesInvoiceStates", t => t.StateID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesInvoiceStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.SalesInvoiceTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.BillingContactID)
                .Index(t => t.ShippingContactID)
                .Index(t => t.StatusID)
                .Index(t => t.StateID)
                .Index(t => t.TypeID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.StoreID)
                .Index(t => t.ParentID)
                .Index(t => t.SalesGroupID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoiceContacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesInvoices", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Contacts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSalesInvoiceDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesInvoices", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Discounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UsageLimitByUser = c.Decimal(nullable: false, precision: 1, scale: 0),
                        UsageLimitByCart = c.Decimal(nullable: false, precision: 1, scale: 0),
                        CanCombine = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsAutoApplied = c.Decimal(nullable: false, precision: 1, scale: 0),
                        RoundingOperation = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UsageLimit = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PurchaseMinimum = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PurchaseLimit = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountCompareOperator = c.Decimal(precision: 10, scale: 0),
                        DiscountTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ValueType = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RoundingType = c.Decimal(precision: 10, scale: 0),
                        Priority = c.Decimal(nullable: false, precision: 3, scale: 0),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ThresholdAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyXValue = c.Decimal(precision: 18, scale: 2),
                        GetYValue = c.Decimal(precision: 18, scale: 2),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountAccounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Accounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountAccountTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.AccountTypes", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.StoreID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Stores",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MinimumOrderDollarAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountOverrideFee = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MinimumOrderDollarAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFee = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MinimumOrderQuantityAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderQuantityAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderDollarAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderQuantityAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingDollarAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingDollarAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingQuantityAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingQuantityAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingDollarAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingQuantityAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        Slogan = c.String(maxLength: 1024),
                        MissionStatement = c.String(maxLength: 1024),
                        About = c.String(),
                        Overview = c.String(),
                        ExternalUrl = c.String(maxLength: 512),
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
                        LanguageID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID)
                .ForeignKey("CLARITY.Languages", t => t.LanguageID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumOrderDollarAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumOrderDollarAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumOrderQuantityAmountBufferProductID)
                .ForeignKey("CLARITY.StoreTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.ContactID)
                .Index(t => t.MinimumOrderDollarAmountBufferProductID)
                .Index(t => t.MinimumOrderQuantityAmountBufferProductID)
                .Index(t => t.MinimumOrderDollarAmountBufferCategoryID)
                .Index(t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .Index(t => t.LanguageID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreAccounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        HasAccessToStore = c.Decimal(nullable: false, precision: 1, scale: 0),
                        PricePointID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.PricePoints", t => t.PricePointID)
                .ForeignKey("CLARITY.Accounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.PricePointID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PricePoints",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.BrandStores",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Brands", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Brands",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.BrandSiteDomains",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Brands", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.SiteDomains", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SiteDomains",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        HeaderContent = c.String(),
                        FooterContent = c.String(),
                        SideBarContent = c.String(),
                        CatalogContent = c.String(),
                        Url = c.String(nullable: false, maxLength: 512),
                        AlternateUrl1 = c.String(maxLength: 512),
                        AlternateUrl2 = c.String(maxLength: 512),
                        AlternateUrl3 = c.String(maxLength: 512),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SiteDomainSocialProviders",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Script = c.String(),
                        UrlValues = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SiteDomains", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.SocialProviders", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SocialProviders",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Url = c.String(maxLength: 1024),
                        UrlFormat = c.String(maxLength: 1024),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreSiteDomains",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.SiteDomains", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.BrandImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Brands", t => t.MasterID)
                .ForeignKey("CLARITY.BrandImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.BrandImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID)
                .ForeignKey("CLARITY.StoreImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreManufacturers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Manufacturers", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Manufacturers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ContactID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountOverrideFee = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MinimumOrderDollarAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFee = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MinimumOrderQuantityAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderQuantityAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderDollarAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderQuantityAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingDollarAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingDollarAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingQuantityAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingQuantityAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingDollarAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingQuantityAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumOrderDollarAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumOrderDollarAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumOrderQuantityAmountBufferProductID)
                .ForeignKey("CLARITY.ManufacturerTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.ContactID)
                .Index(t => t.MinimumOrderDollarAmountBufferProductID)
                .Index(t => t.MinimumOrderQuantityAmountBufferProductID)
                .Index(t => t.MinimumOrderDollarAmountBufferCategoryID)
                .Index(t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ManufacturerImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Manufacturers", t => t.MasterID)
                .ForeignKey("CLARITY.ManufacturerImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ManufacturerImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Categories",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoUrl = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MinimumOrderDollarAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountOverrideFee = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MinimumOrderDollarAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFee = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MinimumOrderQuantityAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderQuantityAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderDollarAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderQuantityAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingDollarAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingDollarAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingQuantityAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingQuantityAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingDollarAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingQuantityAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RequiresRoles = c.String(maxLength: 512),
                        RequiresRolesAlt = c.String(maxLength: 512),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        IsVisible = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IncludeInMenu = c.Decimal(nullable: false, precision: 1, scale: 0),
                        HeaderContent = c.String(),
                        SidebarContent = c.String(),
                        FooterContent = c.String(),
                        HandlingCharge = c.Decimal(precision: 18, scale: 2),
                        RestockingFeePercent = c.Decimal(precision: 18, scale: 2),
                        RestockingFeeAmount = c.Decimal(precision: 18, scale: 2),
                        RestockingFeeAmountCurrencyID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumOrderDollarAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumOrderDollarAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumOrderQuantityAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.ParentID)
                .ForeignKey("CLARITY.Currencies", t => t.RestockingFeeAmountCurrencyID)
                .ForeignKey("CLARITY.CategoryTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.ParentID)
                .Index(t => t.MinimumOrderDollarAmountBufferProductID)
                .Index(t => t.MinimumOrderQuantityAmountBufferProductID)
                .Index(t => t.MinimumOrderDollarAmountBufferCategoryID)
                .Index(t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .Index(t => t.TypeID)
                .Index(t => t.DisplayName)
                .Index(t => t.RestockingFeeAmountCurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CategoryImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Categories", t => t.MasterID)
                .ForeignKey("CLARITY.CategoryImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CategoryImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Products",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        RequiresRoles = c.String(maxLength: 512),
                        RequiresRolesAlt = c.String(maxLength: 512),
                        Weight = c.Decimal(precision: 18, scale: 2),
                        WeightUnitOfMeasure = c.String(maxLength: 64),
                        Width = c.Decimal(precision: 18, scale: 2),
                        WidthUnitOfMeasure = c.String(maxLength: 64),
                        Depth = c.Decimal(precision: 18, scale: 2),
                        DepthUnitOfMeasure = c.String(maxLength: 64),
                        Height = c.Decimal(precision: 18, scale: 2),
                        HeightUnitOfMeasure = c.String(maxLength: 64),
                        IsVisible = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsDiscontinued = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsEligibleForReturn = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsTaxable = c.Decimal(nullable: false, precision: 1, scale: 0),
                        AllowBackOrder = c.Decimal(nullable: false, precision: 1, scale: 0),
                        AllowPreSale = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsUnlimitedStock = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsSale = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsQuotable = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsFreeShipping = c.Decimal(nullable: false, precision: 1, scale: 0),
                        NothingToShip = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DropShipOnly = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ShippingLeadTimeIsCalendarDays = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ShortDescription = c.String(maxLength: 255),
                        ManufacturerPartNumber = c.String(maxLength: 64),
                        BrandName = c.String(maxLength: 128),
                        TaxCode = c.String(maxLength: 64),
                        UnitOfMeasure = c.String(maxLength: 64),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        PriceBase = c.Decimal(precision: 18, scale: 2),
                        PriceMsrp = c.Decimal(precision: 18, scale: 2),
                        PriceReduction = c.Decimal(precision: 18, scale: 2),
                        PriceSale = c.Decimal(precision: 18, scale: 2),
                        HandlingCharge = c.Decimal(precision: 18, scale: 2),
                        FlatShippingCharge = c.Decimal(precision: 18, scale: 2),
                        RestockingFeePercent = c.Decimal(precision: 18, scale: 2),
                        RestockingFeeAmount = c.Decimal(precision: 18, scale: 2),
                        AvailableStartDate = c.DateTime(),
                        AvailableEndDate = c.DateTime(),
                        PreSellEndDate = c.DateTime(),
                        StockQuantity = c.Decimal(precision: 18, scale: 2),
                        StockQuantityAllocated = c.Decimal(precision: 18, scale: 2),
                        StockQuantityPreSold = c.Decimal(precision: 18, scale: 2),
                        QuantityPerMasterPack = c.Decimal(precision: 18, scale: 2),
                        QuantityMasterPackPerLayer = c.Decimal(precision: 18, scale: 2),
                        QuantityMasterPackLayersPerPallet = c.Decimal(precision: 18, scale: 2),
                        QuantityMasterPackPerPallet = c.Decimal(precision: 18, scale: 2),
                        QuantityPerLayer = c.Decimal(precision: 18, scale: 2),
                        QuantityLayersPerPallet = c.Decimal(precision: 18, scale: 2),
                        QuantityPerPallet = c.Decimal(precision: 18, scale: 2),
                        KitBaseQuantityPriceMultiplier = c.Decimal(precision: 18, scale: 2),
                        ShippingLeadTimeDays = c.Decimal(precision: 10, scale: 0),
                        MinimumPurchaseQuantity = c.Decimal(precision: 18, scale: 2),
                        MinimumPurchaseQuantityIfPastPurchased = c.Decimal(precision: 18, scale: 2),
                        MaximumPurchaseQuantity = c.Decimal(precision: 18, scale: 2),
                        MaximumPurchaseQuantityIfPastPurchased = c.Decimal(precision: 18, scale: 2),
                        MaximumBackOrderPurchaseQuantity = c.Decimal(precision: 18, scale: 2),
                        MaximumBackOrderPurchaseQuantityIfPastPurchased = c.Decimal(precision: 18, scale: 2),
                        MaximumBackOrderPurchaseQuantityGlobal = c.Decimal(precision: 18, scale: 2),
                        MaximumPrePurchaseQuantity = c.Decimal(precision: 18, scale: 2),
                        MaximumPrePurchaseQuantityIfPastPurchased = c.Decimal(precision: 18, scale: 2),
                        MaximumPrePurchaseQuantityGlobal = c.Decimal(precision: 18, scale: 2),
                        DocumentRequiredForPurchase = c.String(maxLength: 128),
                        DocumentRequiredForPurchaseMissingWarningMessage = c.String(maxLength: 1024),
                        DocumentRequiredForPurchaseExpiredWarningMessage = c.String(maxLength: 1024),
                        DocumentRequiredForPurchaseOverrideFee = c.Decimal(precision: 18, scale: 2),
                        DocumentRequiredForPurchaseOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DocumentRequiredForPurchaseOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        DocumentRequiredForPurchaseOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MustPurchaseInMultiplesOfAmount = c.Decimal(precision: 18, scale: 2),
                        MustPurchaseInMultiplesOfAmountWarningMessage = c.String(maxLength: 1024),
                        MustPurchaseInMultiplesOfAmountOverrideFee = c.Decimal(precision: 18, scale: 2),
                        MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        TotalPurchasedAmount = c.Decimal(precision: 18, scale: 2),
                        TotalPurchasedAmountCurrencyID = c.Decimal(precision: 10, scale: 0),
                        TotalPurchasedQuantity = c.Decimal(precision: 18, scale: 2),
                        PackageID = c.Decimal(precision: 10, scale: 0),
                        MasterPackID = c.Decimal(precision: 10, scale: 0),
                        PalletID = c.Decimal(precision: 10, scale: 0),
                        RestockingFeeAmountCurrencyID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Packages", t => t.MasterPackID)
                .ForeignKey("CLARITY.Packages", t => t.PackageID)
                .ForeignKey("CLARITY.Packages", t => t.PalletID)
                .ForeignKey("CLARITY.Currencies", t => t.RestockingFeeAmountCurrencyID)
                .ForeignKey("CLARITY.ProductStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.TotalPurchasedAmountCurrencyID)
                .ForeignKey("CLARITY.ProductTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StatusID)
                .Index(t => t.TypeID)
                .Index(t => t.TotalPurchasedAmountCurrencyID)
                .Index(t => t.PackageID)
                .Index(t => t.MasterPackID)
                .Index(t => t.PalletID)
                .Index(t => t.RestockingFeeAmountCurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountProducts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Products", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("CLARITY.AccountProductTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.TypeID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountProductTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CartItems",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Sku = c.String(maxLength: 1000),
                        UnitOfMeasure = c.String(maxLength: 100),
                        ForceUniqueLineItemKey = c.String(maxLength: 1024),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityBackOrdered = c.Decimal(precision: 18, scale: 2),
                        QuantityPreSold = c.Decimal(precision: 18, scale: 2),
                        UnitCorePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitSoldPrice = c.Decimal(precision: 18, scale: 2),
                        UnitCorePriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        UnitSoldPriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        OriginalCurrencyID = c.Decimal(precision: 10, scale: 0),
                        SellingCurrencyID = c.Decimal(precision: 10, scale: 0),
                        UnitSoldPriceModifier = c.Decimal(precision: 18, scale: 2),
                        UnitSoldPriceModifierMode = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Carts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.OriginalCurrencyID)
                .ForeignKey("CLARITY.Products", t => t.ProductID)
                .ForeignKey("CLARITY.Currencies", t => t.SellingCurrencyID)
                .ForeignKey("CLARITY.CartItemStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.StatusID)
                .Index(t => t.ProductID)
                .Index(t => t.UserID)
                .Index(t => t.OriginalCurrencyID)
                .Index(t => t.SellingCurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedCartItemDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.CartItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Carts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DueDate = c.DateTime(),
                        SubtotalItems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalTaxes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalHandling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalDiscounts = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingSameAsBilling = c.Decimal(precision: 1, scale: 0),
                        BillingContactID = c.Decimal(precision: 10, scale: 0),
                        ShippingContactID = c.Decimal(precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StateID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        SessionID = c.Guid(),
                        SubtotalShippingModifier = c.Decimal(precision: 18, scale: 2),
                        SubtotalShippingModifierMode = c.Decimal(precision: 10, scale: 0),
                        SubtotalTaxesModifier = c.Decimal(precision: 18, scale: 2),
                        SubtotalTaxesModifierMode = c.Decimal(precision: 10, scale: 0),
                        SubtotalFeesModifier = c.Decimal(precision: 18, scale: 2),
                        SubtotalFeesModifierMode = c.Decimal(precision: 10, scale: 0),
                        SubtotalHandlingModifier = c.Decimal(precision: 18, scale: 2),
                        SubtotalHandlingModifierMode = c.Decimal(precision: 10, scale: 0),
                        SubtotalDiscountsModifier = c.Decimal(precision: 18, scale: 2),
                        SubtotalDiscountsModifierMode = c.Decimal(precision: 10, scale: 0),
                        RequestedShipDate = c.DateTime(),
                        ShipmentID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.Contacts", t => t.BillingContactID)
                .ForeignKey("CLARITY.Carts", t => t.ParentID)
                .ForeignKey("CLARITY.Shipments", t => t.ShipmentID)
                .ForeignKey("CLARITY.Contacts", t => t.ShippingContactID)
                .ForeignKey("CLARITY.CartStates", t => t.StateID, cascadeDelete: true)
                .ForeignKey("CLARITY.CartStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.CartTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.Active)
                .Index(t => new { t.SessionID, t.TypeID, t.UserID, t.Active }, unique: true, name: "Unq_BySessionTypeUserActive")
                .Index(t => t.BillingContactID)
                .Index(t => t.ShippingContactID)
                .Index(t => t.StatusID)
                .Index(t => t.StateID)
                .Index(t => t.TypeID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.StoreID)
                .Index(t => t.ParentID)
                .Index(t => t.ShipmentID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CartContacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Carts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Contacts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedCartDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Carts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Notes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Note = c.String(nullable: false),
                        CreatedByUserID = c.Decimal(precision: 10, scale: 0),
                        UpdatedByUserID = c.Decimal(precision: 10, scale: 0),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        VendorID = c.Decimal(precision: 10, scale: 0),
                        ManufacturerID = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        SalesGroupID = c.Decimal(precision: 10, scale: 0),
                        PurchaseOrderID = c.Decimal(precision: 10, scale: 0),
                        SalesOrderID = c.Decimal(precision: 10, scale: 0),
                        SalesInvoiceID = c.Decimal(precision: 10, scale: 0),
                        SalesQuoteID = c.Decimal(precision: 10, scale: 0),
                        SampleRequestID = c.Decimal(precision: 10, scale: 0),
                        SalesReturnID = c.Decimal(precision: 10, scale: 0),
                        CartID = c.Decimal(precision: 10, scale: 0),
                        PurchaseOrderItemID = c.Decimal(precision: 10, scale: 0),
                        SalesOrderItemID = c.Decimal(precision: 10, scale: 0),
                        SalesInvoiceItemID = c.Decimal(precision: 10, scale: 0),
                        SalesQuoteItemID = c.Decimal(precision: 10, scale: 0),
                        SampleRequestItemID = c.Decimal(precision: 10, scale: 0),
                        SalesReturnItemID = c.Decimal(precision: 10, scale: 0),
                        CartItemID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.Carts", t => t.CartID)
                .ForeignKey("CLARITY.CartItems", t => t.CartItemID)
                .ForeignKey("CLARITY.Users", t => t.CreatedByUserID)
                .ForeignKey("CLARITY.Manufacturers", t => t.ManufacturerID)
                .ForeignKey("CLARITY.PurchaseOrders", t => t.PurchaseOrderID)
                .ForeignKey("CLARITY.PurchaseOrderItems", t => t.PurchaseOrderItemID)
                .ForeignKey("CLARITY.SalesGroups", t => t.SalesGroupID)
                .ForeignKey("CLARITY.SalesInvoices", t => t.SalesInvoiceID)
                .ForeignKey("CLARITY.SalesInvoiceItems", t => t.SalesInvoiceItemID)
                .ForeignKey("CLARITY.SalesOrders", t => t.SalesOrderID)
                .ForeignKey("CLARITY.SalesOrderItems", t => t.SalesOrderItemID)
                .ForeignKey("CLARITY.SalesQuotes", t => t.SalesQuoteID)
                .ForeignKey("CLARITY.SalesQuoteItems", t => t.SalesQuoteItemID)
                .ForeignKey("CLARITY.SalesReturns", t => t.SalesReturnID)
                .ForeignKey("CLARITY.SalesReturnItems", t => t.SalesReturnItemID)
                .ForeignKey("CLARITY.SampleRequests", t => t.SampleRequestID)
                .ForeignKey("CLARITY.SampleRequestItems", t => t.SampleRequestItemID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.NoteTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UpdatedByUserID)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .ForeignKey("CLARITY.Vendors", t => t.VendorID)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.UpdatedByUserID)
                .Index(t => t.AccountID)
                .Index(t => t.UserID)
                .Index(t => t.VendorID)
                .Index(t => t.ManufacturerID)
                .Index(t => t.StoreID)
                .Index(t => t.SalesGroupID)
                .Index(t => t.PurchaseOrderID)
                .Index(t => t.SalesOrderID)
                .Index(t => t.SalesInvoiceID)
                .Index(t => t.SalesQuoteID)
                .Index(t => t.SampleRequestID)
                .Index(t => t.SalesReturnID)
                .Index(t => t.CartID)
                .Index(t => t.PurchaseOrderItemID)
                .Index(t => t.SalesOrderItemID)
                .Index(t => t.SalesInvoiceItemID)
                .Index(t => t.SalesQuoteItemID)
                .Index(t => t.SampleRequestItemID)
                .Index(t => t.SalesReturnItemID)
                .Index(t => t.CartItemID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Users",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserName = c.String(maxLength: 256),
                        Email = c.String(maxLength: 256),
                        PasswordHash = c.String(maxLength: 100),
                        SecurityStamp = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 25),
                        LockoutEndDateUtc = c.DateTime(),
                        IsApproved = c.Decimal(nullable: false, precision: 1, scale: 0),
                        RequirePasswordChangeOnNextLogin = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TaxNumber = c.String(maxLength: 50),
                        DisplayName = c.String(maxLength: 128),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        PreferredStoreID = c.Decimal(precision: 10, scale: 0),
                        CurrencyID = c.Decimal(precision: 10, scale: 0),
                        LanguageID = c.Decimal(precision: 10, scale: 0),
                        UserOnlineStatusID = c.Decimal(precision: 10, scale: 0),
                        EmailConfirmed = c.Decimal(nullable: false, precision: 1, scale: 0),
                        PhoneNumberConfirmed = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TwoFactorEnabled = c.Decimal(nullable: false, precision: 1, scale: 0),
                        LockoutEnabled = c.Decimal(nullable: false, precision: 1, scale: 0),
                        AccessFailedCount = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.CurrencyID)
                .ForeignKey("CLARITY.Languages", t => t.LanguageID)
                .ForeignKey("CLARITY.Stores", t => t.PreferredStoreID)
                .ForeignKey("CLARITY.UserStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.UserTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.UserOnlineStatus", t => t.UserOnlineStatusID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.ContactID)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.AccountID)
                .Index(t => t.PreferredStoreID)
                .Index(t => t.CurrencyID)
                .Index(t => t.LanguageID)
                .Index(t => t.UserOnlineStatusID);

            CreateTable(
                "CLARITY.UserClaims",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("CLARITY.Users", t => t.UserId)
                .Index(t => t.Id)
                .Index(t => t.UserId);

            CreateTable(
                "CLARITY.ConversationUsers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        LastHeartbeat = c.DateTime(),
                        IsTyping = c.Decimal(precision: 1, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Conversations", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Conversations",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        HasEnded = c.Decimal(precision: 1, scale: 0),
                        CopyUserWhenEnded = c.Decimal(precision: 1, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.StoreID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Messages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        Subject = c.String(maxLength: 255),
                        Context = c.String(maxLength: 256),
                        Body = c.String(nullable: false),
                        IsReplyAllAllowed = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ConversationID = c.Decimal(precision: 10, scale: 0),
                        SentByUserID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Conversations", t => t.ConversationID)
                .ForeignKey("CLARITY.Users", t => t.SentByUserID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.StoreID)
                .Index(t => t.ConversationID)
                .Index(t => t.SentByUserID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.MessageAttachments",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CreatedByUserID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UpdatedByUserID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.CreatedByUserID)
                .ForeignKey("CLARITY.Messages", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UpdatedByUserID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.UpdatedByUserID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoredFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 128),
                        SeoTitle = c.String(maxLength: 75),
                        Author = c.String(maxLength: 512),
                        Copyright = c.String(maxLength: 512),
                        FileFormat = c.String(maxLength: 512),
                        FileName = c.String(maxLength: 512),
                        IsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Bytes = c.Binary(),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.MessageRecipients",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IsRead = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ReadAt = c.DateTime(),
                        IsArchived = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ArchivedAt = c.DateTime(),
                        HasSentAnEmail = c.Decimal(nullable: false, precision: 1, scale: 0),
                        EmailSentAt = c.DateTime(),
                        GroupID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Groups", t => t.GroupID)
                .ForeignKey("CLARITY.Messages", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.GroupID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.EmailQueues",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        AddressesTo = c.String(maxLength: 1024),
                        AddressesCc = c.String(maxLength: 1024),
                        AddressesBcc = c.String(maxLength: 1024),
                        AddressFrom = c.String(nullable: false, maxLength: 1024),
                        Subject = c.String(nullable: false, maxLength: 255),
                        Body = c.String(nullable: false),
                        Attempts = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IsHtml = c.Decimal(nullable: false, precision: 1, scale: 0),
                        HasError = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        EmailTemplateID = c.Decimal(precision: 10, scale: 0),
                        MessageRecipientID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.EmailTemplates", t => t.EmailTemplateID)
                .ForeignKey("CLARITY.MessageRecipients", t => t.MessageRecipientID)
                .ForeignKey("CLARITY.EmailStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.EmailTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.EmailTemplateID)
                .Index(t => t.MessageRecipientID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.EmailQueueAttachments",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        CreatedByUserID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UpdatedByUserID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.CreatedByUserID, cascadeDelete: true)
                .ForeignKey("CLARITY.EmailQueues", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UpdatedByUserID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.UpdatedByUserID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.EmailTemplates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Subject = c.String(nullable: false, maxLength: 255),
                        Body = c.String(nullable: false),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.EmailStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.EmailTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Groups",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        GroupOwnerID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.GroupOwnerID)
                .ForeignKey("CLARITY.Groups", t => t.ParentID)
                .ForeignKey("CLARITY.GroupStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.GroupTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.ParentID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.GroupOwnerID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.GroupStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.GroupTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.GroupUsers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Groups", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountCodes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Code = c.String(nullable: false, maxLength: 20),
                        DiscountID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.DiscountID)
                .Index(t => t.UserID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.FavoriteCategories",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Categories", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.FavoriteManufacturers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Manufacturers", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.FavoriteStores",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.FavoriteVendors",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Vendors", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Vendors",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ContactID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderDollarAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountOverrideFee = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderDollarAmountOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MinimumOrderDollarAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFee = c.Decimal(precision: 18, scale: 2),
                        MinimumOrderQuantityAmountOverrideFeeIsPercent = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MinimumOrderQuantityAmountOverrideFeeWarningMessage = c.String(maxLength: 1024),
                        MinimumOrderQuantityAmountOverrideFeeAcceptedMessage = c.String(maxLength: 1024),
                        MinimumOrderDollarAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderQuantityAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderDollarAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumOrderQuantityAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingDollarAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingDollarAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingDollarAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmount = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingQuantityAmountAfter = c.Decimal(precision: 18, scale: 2),
                        MinimumForFreeShippingQuantityAmountWarningMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage = c.String(maxLength: 1024),
                        MinimumForFreeShippingDollarAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingQuantityAmountBufferProductID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingDollarAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        MinimumForFreeShippingQuantityAmountBufferCategoryID = c.Decimal(precision: 10, scale: 0),
                        Notes = c.String(),
                        AccountNumber = c.String(maxLength: 100),
                        Terms = c.String(maxLength: 100),
                        TermNotes = c.String(),
                        SendMethod = c.String(maxLength: 100),
                        EmailSubject = c.String(maxLength: 300),
                        ShipTo = c.String(maxLength: 100),
                        ShipViaNotes = c.String(),
                        SignBy = c.String(maxLength: 100),
                        AllowDropShip = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DefaultDiscount = c.Decimal(precision: 18, scale: 2),
                        RecommendedPurchaseOrderDollarAmount = c.Decimal(precision: 18, scale: 2),
                        UserName = c.String(maxLength: 128),
                        PasswordHash = c.String(maxLength: 128),
                        SecurityToken = c.String(maxLength: 128),
                        MustResetPassword = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ContactMethodID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID)
                .ForeignKey("CLARITY.ContactMethods", t => t.ContactMethodID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumOrderDollarAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumOrderDollarAmountBufferProductID)
                .ForeignKey("CLARITY.Categories", t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .ForeignKey("CLARITY.Products", t => t.MinimumOrderQuantityAmountBufferProductID)
                .ForeignKey("CLARITY.VendorTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.ContactID)
                .Index(t => t.MinimumOrderDollarAmountBufferProductID)
                .Index(t => t.MinimumOrderQuantityAmountBufferProductID)
                .Index(t => t.MinimumOrderDollarAmountBufferCategoryID)
                .Index(t => t.MinimumOrderQuantityAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferProductID)
                .Index(t => t.MinimumForFreeShippingDollarAmountBufferCategoryID)
                .Index(t => t.MinimumForFreeShippingQuantityAmountBufferCategoryID)
                .Index(t => t.UserName)
                .Index(t => t.PasswordHash)
                .Index(t => t.SecurityToken)
                .Index(t => t.MustResetPassword)
                .Index(t => t.ContactMethodID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.VendorAccounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Vendors", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Accounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ContactMethods",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.VendorImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Vendors", t => t.MasterID)
                .ForeignKey("CLARITY.VendorImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.VendorImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.VendorManufacturers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Vendors", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Manufacturers", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.VendorProducts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Bin = c.String(maxLength: 1000),
                        MinimumInventory = c.Decimal(precision: 10, scale: 0),
                        MaximumInventory = c.Decimal(precision: 10, scale: 0),
                        InventoryCount = c.Decimal(precision: 10, scale: 0),
                        CostMultiplier = c.Decimal(precision: 18, scale: 2),
                        ListedPrice = c.Decimal(precision: 18, scale: 2),
                        ActualCost = c.Decimal(precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Vendors", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Products", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Reviews",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                        Approved = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ApprovedDate = c.DateTime(),
                        Title = c.String(maxLength: 255),
                        Location = c.String(maxLength: 255),
                        SubmittedByUserID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ApprovedByUserID = c.Decimal(precision: 10, scale: 0),
                        CategoryID = c.Decimal(precision: 10, scale: 0),
                        ManufacturerID = c.Decimal(precision: 10, scale: 0),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        VendorID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.ApprovedByUserID)
                .ForeignKey("CLARITY.Categories", t => t.CategoryID)
                .ForeignKey("CLARITY.Manufacturers", t => t.ManufacturerID)
                .ForeignKey("CLARITY.Products", t => t.ProductID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.Users", t => t.SubmittedByUserID, cascadeDelete: true)
                .ForeignKey("CLARITY.ReviewTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .ForeignKey("CLARITY.Vendors", t => t.VendorID)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.SubmittedByUserID)
                .Index(t => t.ApprovedByUserID)
                .Index(t => t.CategoryID)
                .Index(t => t.ManufacturerID)
                .Index(t => t.ProductID)
                .Index(t => t.StoreID)
                .Index(t => t.UserID)
                .Index(t => t.VendorID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ReviewTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Shipments",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Reference1 = c.String(maxLength: 100),
                        Reference2 = c.String(maxLength: 100),
                        Reference3 = c.String(maxLength: 100),
                        TrackingNumber = c.String(maxLength: 100),
                        Destination = c.String(maxLength: 255),
                        TargetShippingDate = c.DateTime(),
                        EstimatedDeliveryDate = c.DateTime(),
                        ShipDate = c.DateTime(),
                        DateDelivered = c.DateTime(),
                        NegotiatedRate = c.Decimal(precision: 18, scale: 2),
                        PublishedRate = c.Decimal(precision: 18, scale: 2),
                        OriginContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DestinationContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        InventoryLocationSectionID = c.Decimal(precision: 10, scale: 0),
                        ShipCarrierID = c.Decimal(precision: 10, scale: 0),
                        ShipCarrierMethodID = c.Decimal(precision: 10, scale: 0),
                        VendorID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.DestinationContactID)
                .ForeignKey("CLARITY.InventoryLocationSections", t => t.InventoryLocationSectionID)
                .ForeignKey("CLARITY.Contacts", t => t.OriginContactID)
                .ForeignKey("CLARITY.ShipCarriers", t => t.ShipCarrierID)
                .ForeignKey("CLARITY.ShipCarrierMethods", t => t.ShipCarrierMethodID)
                .ForeignKey("CLARITY.ShipmentStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.ShipmentTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Vendors", t => t.VendorID)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.OriginContactID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.InventoryLocationSectionID)
                .Index(t => t.ShipCarrierID)
                .Index(t => t.ShipCarrierMethodID)
                .Index(t => t.VendorID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.InventoryLocationSections",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        InventoryLocationID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.InventoryLocations", t => t.InventoryLocationID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.InventoryLocationID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.InventoryLocations",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ContactID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID)
                .Index(t => t.ID)
                .Index(t => t.ContactID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreInventoryLocations",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.InventoryLocations", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoreInventoryLocationTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.TypeID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreInventoryLocationTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductInventoryLocationSections",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Quantity = c.Decimal(precision: 18, scale: 2),
                        QuantityAllocated = c.Decimal(precision: 18, scale: 2),
                        QuantityPreSold = c.Decimal(precision: 18, scale: 2),
                        QuantityBroken = c.Decimal(precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Products", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.InventoryLocationSections", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ShipCarriers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ContactID = c.Decimal(precision: 10, scale: 0),
                        PointOfContact = c.String(maxLength: 1000),
                        IsInbound = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsOutbound = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Username = c.String(maxLength: 75),
                        EncryptedPassword = c.String(maxLength: 1024),
                        Authentication = c.String(maxLength: 128),
                        AccountNumber = c.String(maxLength: 128),
                        SalesRep = c.String(maxLength: 128),
                        PickupTime = c.DateTime(),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID)
                .Index(t => t.ID)
                .Index(t => t.ContactID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CarrierInvoices",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        AccountNumber = c.String(maxLength: 50),
                        InvoiceNumber = c.String(maxLength: 50),
                        InvoiceDate = c.DateTime(),
                        AmountDue = c.Decimal(precision: 18, scale: 2),
                        TrackingNumber = c.String(maxLength: 50),
                        PickupRecord = c.String(maxLength: 50),
                        ReferenceNo1 = c.String(maxLength: 50),
                        ReferenceNo2 = c.String(maxLength: 50),
                        ReferenceNo3 = c.String(maxLength: 50),
                        Weight = c.String(maxLength: 50),
                        Zone = c.String(maxLength: 50),
                        ServiceLevel = c.String(maxLength: 50),
                        PickupDate = c.DateTime(),
                        SenderName = c.String(maxLength: 50),
                        SenderCompanyName = c.String(maxLength: 50),
                        SenderStreet = c.String(maxLength: 50),
                        SenderCity = c.String(maxLength: 50),
                        SenderState = c.String(maxLength: 50),
                        SenderZipCode = c.String(maxLength: 50),
                        ReceiverName = c.String(maxLength: 50),
                        ReceiverCompanyName = c.String(maxLength: 50),
                        ReceiverStreet = c.String(maxLength: 50),
                        ReceiverCity = c.String(maxLength: 50),
                        ReceiverState = c.String(maxLength: 50),
                        ReceiverZipCode = c.String(maxLength: 50),
                        ReceiverCountry = c.String(maxLength: 50),
                        ThirdParty = c.String(maxLength: 50),
                        BilledCharge = c.Decimal(precision: 18, scale: 2),
                        IncentiveCredit = c.Decimal(precision: 18, scale: 2),
                        InvoiceSection = c.String(maxLength: 50),
                        InvoiceType = c.String(maxLength: 50),
                        InvoiceDueDate = c.DateTime(),
                        Status = c.String(maxLength: 200),
                        ShipCarrierID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.ShipCarriers", t => t.ShipCarrierID)
                .Index(t => t.ID)
                .Index(t => t.ShipCarrierID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CarrierOrigins",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DateTime = c.DateTime(),
                        PackageReferenceNumber = c.String(maxLength: 100),
                        ShipmentReferenceNumber = c.String(maxLength: 100),
                        ReferenceNumber = c.String(maxLength: 100),
                        ShipperNumber = c.String(maxLength: 100),
                        SubscriptionEventName = c.String(maxLength: 100),
                        SubscriptionEventNumber = c.String(maxLength: 100),
                        SubscriptionFileName = c.String(maxLength: 100),
                        TrackingNumber = c.String(maxLength: 100),
                        TrackingStatus = c.String(maxLength: 100),
                        TrackingShippingMethod = c.String(maxLength: 100),
                        TrackingShippingDate = c.String(maxLength: 50),
                        TrackingLastScan = c.String(maxLength: 100),
                        TrackingDestination = c.String(maxLength: 100),
                        TrackingEstDeliveryDate = c.String(maxLength: 50),
                        TrackingLastUpdate = c.DateTime(),
                        TrackingEventName = c.String(maxLength: 50),
                        TrackingOriginalEstimatedDeliveryDate = c.String(maxLength: 50),
                        TrackingManualDelivered = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ShipCarrierID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.ShipCarriers", t => t.ShipCarrierID)
                .Index(t => t.ID)
                .Index(t => t.ShipCarrierID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ShipCarrierMethods",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ShipCarrierID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.ShipCarriers", t => t.ShipCarrierID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.ShipCarrierID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ShipmentEvents",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Note = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        AddressID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ShipmentID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Addresses", t => t.AddressID, cascadeDelete: true)
                .ForeignKey("CLARITY.Shipments", t => t.ShipmentID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.AddressID)
                .Index(t => t.ShipmentID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ShipmentStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ShipmentTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreVendors",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Vendors", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.VendorTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.MasterID)
                .ForeignKey("CLARITY.UserImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserLogins",
                c => new
                    {
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("CLARITY.Users", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "CLARITY.ReferralCodes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Code = c.String(nullable: false, maxLength: 128),
                        UserID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.ReferralCodeStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.ReferralCodeTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.UserID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ReferralCodeStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ReferralCodeTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.RoleUsers",
                c => new
                    {
                        RoleId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        GroupID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        JsonAttributes = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId, t.GroupID })
                .ForeignKey("CLARITY.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("CLARITY.UserRoles", t => t.RoleId)
                .ForeignKey("CLARITY.Users", t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.GroupID);

            CreateTable(
                "CLARITY.UserRoles",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "CLARITY.RolePermissions",
                c => new
                    {
                        RoleId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PermissionId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PermissionId })
                .ForeignKey("CLARITY.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("CLARITY.UserRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);

            CreateTable(
                "CLARITY.Permissions",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "CLARITY.SalesQuotes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DueDate = c.DateTime(),
                        SubtotalItems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalTaxes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalHandling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalDiscounts = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingSameAsBilling = c.Decimal(precision: 1, scale: 0),
                        BillingContactID = c.Decimal(precision: 10, scale: 0),
                        ShippingContactID = c.Decimal(precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StateID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        BalanceDue = c.Decimal(precision: 18, scale: 2),
                        ResponseAsVendorID = c.Decimal(precision: 10, scale: 0),
                        ResponseAsStoreID = c.Decimal(precision: 10, scale: 0),
                        SalesGroupAsMasterID = c.Decimal(precision: 10, scale: 0),
                        SalesGroupAsResponseID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.Contacts", t => t.BillingContactID)
                .ForeignKey("CLARITY.SalesQuotes", t => t.ParentID)
                .ForeignKey("CLARITY.SalesGroups", t => t.SalesGroupAsMasterID)
                .ForeignKey("CLARITY.SalesGroups", t => t.SalesGroupAsResponseID)
                .ForeignKey("CLARITY.Stores", t => t.ResponseAsStoreID)
                .ForeignKey("CLARITY.Vendors", t => t.ResponseAsVendorID)
                .ForeignKey("CLARITY.Contacts", t => t.ShippingContactID)
                .ForeignKey("CLARITY.SalesQuoteStates", t => t.StateID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesQuoteStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.SalesQuoteTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.BillingContactID)
                .Index(t => t.ShippingContactID)
                .Index(t => t.StatusID)
                .Index(t => t.StateID)
                .Index(t => t.TypeID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.StoreID)
                .Index(t => t.ParentID)
                .Index(t => t.ResponseAsVendorID)
                .Index(t => t.ResponseAsStoreID)
                .Index(t => t.SalesGroupAsMasterID)
                .Index(t => t.SalesGroupAsResponseID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteSalesOrders",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesQuotes", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesOrders", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteContacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesQuotes", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Contacts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSalesQuoteDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesQuotes", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.RateQuotes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        EstimatedDeliveryDate = c.DateTime(),
                        TargetShippingDate = c.DateTime(),
                        Rate = c.Decimal(precision: 18, scale: 2),
                        CartHash = c.Decimal(precision: 19, scale: 0),
                        RateTimestamp = c.DateTime(),
                        Selected = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ShipCarrierMethodID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CartID = c.Decimal(precision: 10, scale: 0),
                        SampleRequestID = c.Decimal(precision: 10, scale: 0),
                        SalesQuoteID = c.Decimal(precision: 10, scale: 0),
                        SalesOrderID = c.Decimal(precision: 10, scale: 0),
                        PurchaseOrderID = c.Decimal(precision: 10, scale: 0),
                        SalesInvoiceID = c.Decimal(precision: 10, scale: 0),
                        SalesReturnID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Carts", t => t.CartID)
                .ForeignKey("CLARITY.PurchaseOrders", t => t.PurchaseOrderID)
                .ForeignKey("CLARITY.SalesInvoices", t => t.SalesInvoiceID)
                .ForeignKey("CLARITY.SalesOrders", t => t.SalesOrderID)
                .ForeignKey("CLARITY.SalesQuotes", t => t.SalesQuoteID)
                .ForeignKey("CLARITY.SalesReturns", t => t.SalesReturnID)
                .ForeignKey("CLARITY.SampleRequests", t => t.SampleRequestID)
                .ForeignKey("CLARITY.ShipCarrierMethods", t => t.ShipCarrierMethodID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.ShipCarrierMethodID)
                .Index(t => t.CartID)
                .Index(t => t.SampleRequestID)
                .Index(t => t.SalesQuoteID)
                .Index(t => t.SalesOrderID)
                .Index(t => t.PurchaseOrderID)
                .Index(t => t.SalesInvoiceID)
                .Index(t => t.SalesReturnID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturns",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DueDate = c.DateTime(),
                        SubtotalItems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalTaxes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalHandling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalDiscounts = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingSameAsBilling = c.Decimal(precision: 1, scale: 0),
                        BillingContactID = c.Decimal(precision: 10, scale: 0),
                        ShippingContactID = c.Decimal(precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StateID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        PurchaseOrderNumber = c.String(maxLength: 100),
                        OrderStateName = c.String(maxLength: 128),
                        TrackingNumber = c.String(maxLength: 256),
                        RefundTransactionID = c.String(maxLength: 256),
                        TaxTransactionID = c.String(maxLength: 256),
                        BalanceDue = c.Decimal(precision: 18, scale: 2),
                        RefundAmount = c.Decimal(precision: 18, scale: 2),
                        ReturnApprovedDate = c.DateTime(),
                        ReturnCommitmentDate = c.DateTime(),
                        RequiredShipDate = c.DateTime(),
                        RequestedShipDate = c.DateTime(),
                        ActualShipDate = c.DateTime(),
                        SalesGroupID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.Contacts", t => t.BillingContactID)
                .ForeignKey("CLARITY.SalesReturns", t => t.ParentID)
                .ForeignKey("CLARITY.SalesGroups", t => t.SalesGroupID)
                .ForeignKey("CLARITY.Contacts", t => t.ShippingContactID)
                .ForeignKey("CLARITY.SalesReturnStates", t => t.StateID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesReturnStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.SalesReturnTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.BillingContactID)
                .Index(t => t.ShippingContactID)
                .Index(t => t.StatusID)
                .Index(t => t.StateID)
                .Index(t => t.TypeID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.StoreID)
                .Index(t => t.ParentID)
                .Index(t => t.SalesGroupID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnSalesOrders",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesReturns", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesOrders", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnContacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesReturns", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Contacts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSalesReturnDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesReturns", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesGroups",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        BillingContactID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.Contacts", t => t.BillingContactID)
                .Index(t => t.ID)
                .Index(t => t.AccountID)
                .Index(t => t.BillingContactID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnItems",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Sku = c.String(maxLength: 1000),
                        UnitOfMeasure = c.String(maxLength: 100),
                        ForceUniqueLineItemKey = c.String(maxLength: 1024),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityBackOrdered = c.Decimal(precision: 18, scale: 2),
                        QuantityPreSold = c.Decimal(precision: 18, scale: 2),
                        UnitCorePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitSoldPrice = c.Decimal(precision: 18, scale: 2),
                        UnitCorePriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        UnitSoldPriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        OriginalCurrencyID = c.Decimal(precision: 10, scale: 0),
                        SellingCurrencyID = c.Decimal(precision: 10, scale: 0),
                        RestockingFeeAmount = c.Decimal(precision: 18, scale: 2),
                        SalesReturnReasonID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesReturns", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.OriginalCurrencyID)
                .ForeignKey("CLARITY.Products", t => t.ProductID)
                .ForeignKey("CLARITY.SalesReturnReasons", t => t.SalesReturnReasonID)
                .ForeignKey("CLARITY.Currencies", t => t.SellingCurrencyID)
                .ForeignKey("CLARITY.SalesReturnItemStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.StatusID)
                .Index(t => t.ProductID)
                .Index(t => t.UserID)
                .Index(t => t.OriginalCurrencyID)
                .Index(t => t.SellingCurrencyID)
                .Index(t => t.SalesReturnReasonID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSalesReturnItemDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesReturnItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnReasons",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        IsRestockingFeeApplicable = c.Decimal(nullable: false, precision: 1, scale: 0),
                        RestockingFeePercent = c.Decimal(precision: 18, scale: 2),
                        RestockingFeeAmount = c.Decimal(precision: 18, scale: 2),
                        RestockingFeeAmountCurrencyID = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Currencies", t => t.RestockingFeeAmountCurrencyID)
                .Index(t => t.ID)
                .Index(t => t.RestockingFeeAmountCurrencyID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnItemStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnItemTargets",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NothingToShip = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DestinationContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OriginProductInventoryLocationSectionID = c.Decimal(precision: 10, scale: 0),
                        OriginStoreProductID = c.Decimal(precision: 10, scale: 0),
                        OriginVendorProductID = c.Decimal(precision: 10, scale: 0),
                        SelectedRateQuoteID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesReturnItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductInventoryLocationSections", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("CLARITY.StoreProducts", t => t.OriginStoreProductID)
                .ForeignKey("CLARITY.VendorProducts", t => t.OriginVendorProductID)
                .ForeignKey("CLARITY.RateQuotes", t => t.SelectedRateQuoteID)
                .ForeignKey("CLARITY.SalesItemTargetTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreProductID)
                .Index(t => t.OriginVendorProductID)
                .Index(t => t.SelectedRateQuoteID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreProducts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IsVisibleInStore = c.Decimal(nullable: false, precision: 1, scale: 0),
                        PriceBase = c.Decimal(precision: 18, scale: 2),
                        PriceMsrp = c.Decimal(precision: 18, scale: 2),
                        PriceReduction = c.Decimal(precision: 18, scale: 2),
                        PriceSale = c.Decimal(precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Products", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesItemTargetTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnPayments",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesReturns", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Payments", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Payments",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        AuthCode = c.String(maxLength: 100),
                        ReferenceNo = c.String(maxLength: 100),
                        TransactionNumber = c.String(maxLength: 100),
                        StatusDate = c.DateTime(),
                        Authorized = c.Decimal(precision: 1, scale: 0),
                        AuthDate = c.DateTime(),
                        Received = c.Decimal(precision: 1, scale: 0),
                        ReceivedDate = c.DateTime(),
                        Response = c.String(),
                        ExternalCustomerID = c.String(maxLength: 100),
                        ExternalPaymentID = c.String(maxLength: 100),
                        PaymentData = c.String(),
                        CardTypeID = c.Decimal(precision: 10, scale: 0),
                        CardMask = c.String(maxLength: 50),
                        CVV = c.String(maxLength: 50),
                        Last4CardDigits = c.String(maxLength: 4),
                        ExpirationMonth = c.Decimal(precision: 10, scale: 0),
                        ExpirationYear = c.Decimal(precision: 10, scale: 0),
                        CheckNumber = c.String(maxLength: 8),
                        RoutingNumberLast4 = c.String(maxLength: 4),
                        AccountNumberLast4 = c.String(maxLength: 4),
                        BankName = c.String(maxLength: 100),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        BillingContactID = c.Decimal(precision: 10, scale: 0),
                        PaymentMethodID = c.Decimal(precision: 10, scale: 0),
                        CurrencyID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.BillingContactID)
                .ForeignKey("CLARITY.Currencies", t => t.CurrencyID)
                .ForeignKey("CLARITY.PaymentMethods", t => t.PaymentMethodID)
                .ForeignKey("CLARITY.PaymentStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.PaymentTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StoreID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.BillingContactID)
                .Index(t => t.PaymentMethodID)
                .Index(t => t.CurrencyID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PaymentMethods",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PaymentStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PaymentTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnStates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesReturns", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesReturnTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SampleRequests",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DueDate = c.DateTime(),
                        SubtotalItems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalTaxes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalHandling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalDiscounts = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingSameAsBilling = c.Decimal(precision: 1, scale: 0),
                        BillingContactID = c.Decimal(precision: 10, scale: 0),
                        ShippingContactID = c.Decimal(precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StateID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.Contacts", t => t.BillingContactID)
                .ForeignKey("CLARITY.SampleRequests", t => t.ParentID)
                .ForeignKey("CLARITY.Contacts", t => t.ShippingContactID)
                .ForeignKey("CLARITY.SampleRequestStates", t => t.StateID, cascadeDelete: true)
                .ForeignKey("CLARITY.SampleRequestStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.SampleRequestTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.BillingContactID)
                .Index(t => t.ShippingContactID)
                .Index(t => t.StatusID)
                .Index(t => t.StateID)
                .Index(t => t.TypeID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.StoreID)
                .Index(t => t.ParentID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SampleRequestContacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SampleRequests", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Contacts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSampleRequestDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SampleRequests", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SampleRequestItems",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Sku = c.String(maxLength: 1000),
                        UnitOfMeasure = c.String(maxLength: 100),
                        ForceUniqueLineItemKey = c.String(maxLength: 1024),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityBackOrdered = c.Decimal(precision: 18, scale: 2),
                        QuantityPreSold = c.Decimal(precision: 18, scale: 2),
                        UnitCorePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitSoldPrice = c.Decimal(precision: 18, scale: 2),
                        UnitCorePriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        UnitSoldPriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        OriginalCurrencyID = c.Decimal(precision: 10, scale: 0),
                        SellingCurrencyID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SampleRequests", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.OriginalCurrencyID)
                .ForeignKey("CLARITY.Products", t => t.ProductID)
                .ForeignKey("CLARITY.Currencies", t => t.SellingCurrencyID)
                .ForeignKey("CLARITY.SampleRequestItemStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.StatusID)
                .Index(t => t.ProductID)
                .Index(t => t.UserID)
                .Index(t => t.OriginalCurrencyID)
                .Index(t => t.SellingCurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSampleRequestItemDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SampleRequestItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SampleRequestItemStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SampleRequestItemTargets",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NothingToShip = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DestinationContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OriginProductInventoryLocationSectionID = c.Decimal(precision: 10, scale: 0),
                        OriginStoreProductID = c.Decimal(precision: 10, scale: 0),
                        OriginVendorProductID = c.Decimal(precision: 10, scale: 0),
                        SelectedRateQuoteID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("CLARITY.SampleRequestItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductInventoryLocationSections", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("CLARITY.StoreProducts", t => t.OriginStoreProductID)
                .ForeignKey("CLARITY.VendorProducts", t => t.OriginVendorProductID)
                .ForeignKey("CLARITY.RateQuotes", t => t.SelectedRateQuoteID)
                .ForeignKey("CLARITY.SalesItemTargetTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreProductID)
                .Index(t => t.OriginVendorProductID)
                .Index(t => t.SelectedRateQuoteID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SampleRequestStates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SampleRequestStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SampleRequestFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SampleRequests", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SampleRequestTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteItems",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Sku = c.String(maxLength: 1000),
                        UnitOfMeasure = c.String(maxLength: 100),
                        ForceUniqueLineItemKey = c.String(maxLength: 1024),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityBackOrdered = c.Decimal(precision: 18, scale: 2),
                        QuantityPreSold = c.Decimal(precision: 18, scale: 2),
                        UnitCorePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitSoldPrice = c.Decimal(precision: 18, scale: 2),
                        UnitCorePriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        UnitSoldPriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        OriginalCurrencyID = c.Decimal(precision: 10, scale: 0),
                        SellingCurrencyID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesQuotes", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.OriginalCurrencyID)
                .ForeignKey("CLARITY.Products", t => t.ProductID)
                .ForeignKey("CLARITY.Currencies", t => t.SellingCurrencyID)
                .ForeignKey("CLARITY.SalesQuoteItemStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.StatusID)
                .Index(t => t.ProductID)
                .Index(t => t.UserID)
                .Index(t => t.OriginalCurrencyID)
                .Index(t => t.SellingCurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSalesQuoteItemDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesQuoteItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteItemStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteItemTargets",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NothingToShip = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DestinationContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OriginProductInventoryLocationSectionID = c.Decimal(precision: 10, scale: 0),
                        OriginStoreProductID = c.Decimal(precision: 10, scale: 0),
                        OriginVendorProductID = c.Decimal(precision: 10, scale: 0),
                        SelectedRateQuoteID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesQuoteItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductInventoryLocationSections", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("CLARITY.StoreProducts", t => t.OriginStoreProductID)
                .ForeignKey("CLARITY.VendorProducts", t => t.OriginVendorProductID)
                .ForeignKey("CLARITY.RateQuotes", t => t.SelectedRateQuoteID)
                .ForeignKey("CLARITY.SalesItemTargetTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreProductID)
                .Index(t => t.OriginVendorProductID)
                .Index(t => t.SelectedRateQuoteID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteCategories",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesQuotes", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Categories", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteStates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesQuotes", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesQuoteTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreUsers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Subscriptions",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MemberSince = c.DateTime(nullable: false),
                        StartsOn = c.DateTime(nullable: false),
                        EndsOn = c.DateTime(),
                        LastPaidDate = c.DateTime(),
                        BillingPeriodsTotal = c.Decimal(nullable: false, precision: 10, scale: 0),
                        BillingPeriodsPaid = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditUponUpgrade = c.Decimal(precision: 18, scale: 2),
                        AutoRenew = c.Decimal(nullable: false, precision: 1, scale: 0),
                        CanUpgrade = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Memo = c.String(maxLength: 128),
                        ProductMembershipLevelID = c.Decimal(precision: 10, scale: 0),
                        RepeatTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SalesInvoiceID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        AccountID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.AccountID)
                .ForeignKey("CLARITY.ProductMembershipLevels", t => t.ProductMembershipLevelID)
                .ForeignKey("CLARITY.RepeatTypes", t => t.RepeatTypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesInvoices", t => t.SalesInvoiceID)
                .ForeignKey("CLARITY.SubscriptionStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.SubscriptionTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.ProductMembershipLevelID)
                .Index(t => t.RepeatTypeID)
                .Index(t => t.SalesInvoiceID)
                .Index(t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductMembershipLevels",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MembershipRepeatTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Products", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.MembershipRepeatTypes", t => t.MembershipRepeatTypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.MembershipLevels", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.MembershipRepeatTypeID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.MembershipRepeatTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Memberships", t => t.MasterID)
                .ForeignKey("CLARITY.RepeatTypes", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Memberships",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        IsContractual = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.MembershipLevels",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        RolesApplied = c.String(maxLength: 512),
                        MembershipID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Memberships", t => t.MembershipID)
                .Index(t => t.ID)
                .Index(t => t.MembershipID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.MembershipAdZoneAccessByLevels",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SubscriberCountThreshold = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UniqueAdLimit = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.MembershipAdZoneAccesses", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.MembershipLevels", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.MembershipAdZoneAccesses",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Memberships", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.AdZoneAccesses", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AdZoneAccesses",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ImpressionCounterID = c.Decimal(precision: 10, scale: 0),
                        ClickCounterID = c.Decimal(precision: 10, scale: 0),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        UniqueAdLimit = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ImpressionLimit = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ClickLimit = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ZoneID = c.Decimal(precision: 10, scale: 0),
                        SubscriptionID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Counters", t => t.ClickCounterID)
                .ForeignKey("CLARITY.Counters", t => t.ImpressionCounterID)
                .ForeignKey("CLARITY.Subscriptions", t => t.SubscriptionID)
                .ForeignKey("CLARITY.Zones", t => t.ZoneID)
                .Index(t => t.ID)
                .Index(t => t.ImpressionCounterID)
                .Index(t => t.ClickCounterID)
                .Index(t => t.ZoneID)
                .Index(t => t.SubscriptionID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AdZones",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ImpressionCounterID = c.Decimal(precision: 10, scale: 0),
                        ClickCounterID = c.Decimal(precision: 10, scale: 0),
                        AdZoneAccessID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.AdZoneAccesses", t => t.AdZoneAccessID)
                .ForeignKey("CLARITY.Counters", t => t.ClickCounterID)
                .ForeignKey("CLARITY.Counters", t => t.ImpressionCounterID)
                .ForeignKey("CLARITY.Ads", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Zones", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.ImpressionCounterID)
                .Index(t => t.ClickCounterID)
                .Index(t => t.AdZoneAccessID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Counters",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Value = c.Decimal(precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.CounterTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CounterLogs",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Value = c.Decimal(precision: 18, scale: 2),
                        CounterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Counters", t => t.CounterID, cascadeDelete: true)
                .ForeignKey("CLARITY.CounterLogTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.CounterID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CounterLogTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CounterTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Ads",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TargetURL = c.String(nullable: false, maxLength: 512),
                        Caption = c.String(maxLength: 256),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImpressionCounterID = c.Decimal(precision: 10, scale: 0),
                        ClickCounterID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Counters", t => t.ClickCounterID)
                .ForeignKey("CLARITY.Counters", t => t.ImpressionCounterID)
                .ForeignKey("CLARITY.AdStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.AdTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.ImpressionCounterID)
                .Index(t => t.ClickCounterID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AdAccounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Ads", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Accounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CampaignAds",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Campaigns", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Ads", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Campaigns",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ProposedStart = c.DateTime(),
                        ProposedEnd = c.DateTime(),
                        ActualStart = c.DateTime(),
                        ActualEnd = c.DateTime(),
                        BudgetedCost = c.Decimal(precision: 18, scale: 2),
                        OtherCost = c.Decimal(precision: 18, scale: 2),
                        ExpectedRevenue = c.Decimal(precision: 18, scale: 2),
                        TotalActualCost = c.Decimal(precision: 18, scale: 2),
                        TotalCampaignActivityActualCost = c.Decimal(precision: 18, scale: 2),
                        ExchangeRate = c.Decimal(precision: 18, scale: 2),
                        CodeName = c.String(maxLength: 32),
                        PromotionCodeName = c.String(maxLength: 128),
                        Message = c.String(maxLength: 256),
                        Objective = c.String(),
                        ExpectedResponse = c.Decimal(precision: 10, scale: 0),
                        UTCConversionTimeZoneCode = c.Decimal(precision: 10, scale: 0),
                        IsTemplate = c.Decimal(precision: 1, scale: 0),
                        CreatedByUserID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.CreatedByUserID)
                .ForeignKey("CLARITY.CampaignStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.CampaignTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CampaignStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CampaignTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.StoreID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AdImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Ads", t => t.MasterID)
                .ForeignKey("CLARITY.AdImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AdImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AdStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AdStores",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Ads", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AdTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Zones",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Width = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Height = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.ZoneStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.ZoneTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ZoneStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ZoneTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.RepeatTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        RepeatableBillingPeriods = c.Decimal(precision: 10, scale: 0),
                        InitialBonusBillingPeriods = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SubscriptionStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreSubscriptions",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Subscriptions", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SubscriptionHistories",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentSuccess = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Memo = c.String(nullable: false, maxLength: 128),
                        BillingPeriodsPaid = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Subscriptions", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Payments", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SubscriptionTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductSubscriptionTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Products", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.SubscriptionTypes", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreUserTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.UserTypes", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserEventAttendances",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        HasAttended = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Date = c.DateTime(nullable: false),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.CalendarEvents", t => t.MasterID)
                .ForeignKey("CLARITY.Users", t => t.SlaveID)
                .ForeignKey("CLARITY.UserEventAttendanceTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Date)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CalendarEvents",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ShortDescription = c.String(maxLength: 256),
                        EventDurationUnitOfMeasure = c.String(maxLength: 128),
                        RecurrenceString = c.String(maxLength: 1024),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EventDuration = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MaxAttendees = c.Decimal(nullable: false, precision: 10, scale: 0),
                        GroupID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("CLARITY.Groups", t => t.GroupID)
                .ForeignKey("CLARITY.CalendarEventStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.CalendarEventTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.ContactID)
                .Index(t => t.GroupID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CalendarEventDetails",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Day = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        Location = c.String(maxLength: 256),
                        CalendarEventID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.CalendarEvents", t => t.CalendarEventID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CalendarEventID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CalendarEventImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.CalendarEvents", t => t.MasterID)
                .ForeignKey("CLARITY.CalendarEventImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CalendarEventImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CalendarEventProducts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.CalendarEvents", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Products", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CalendarEventStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CalendarEventFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.CalendarEvents", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CalendarEventTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserEventAttendanceTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserOnlineStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UserProductTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductTypes", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.StoreID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Wallets",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        CreditCardNumber = c.String(maxLength: 100),
                        AccountNumber = c.String(maxLength: 100),
                        RoutingNumber = c.String(maxLength: 100),
                        BankName = c.String(maxLength: 128),
                        ExpirationMonth = c.Decimal(precision: 10, scale: 0),
                        ExpirationYear = c.Decimal(precision: 10, scale: 0),
                        Token = c.String(maxLength: 100),
                        CardType = c.String(maxLength: 100),
                        CardHolderName = c.String(maxLength: 256),
                        UserID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CurrencyID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Currencies", t => t.CurrencyID)
                .ForeignKey("CLARITY.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.UserID)
                .Index(t => t.CurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PurchaseOrderItems",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Sku = c.String(maxLength: 1000),
                        UnitOfMeasure = c.String(maxLength: 100),
                        ForceUniqueLineItemKey = c.String(maxLength: 1024),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityBackOrdered = c.Decimal(precision: 18, scale: 2),
                        QuantityPreSold = c.Decimal(precision: 18, scale: 2),
                        UnitCorePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitSoldPrice = c.Decimal(precision: 18, scale: 2),
                        UnitCorePriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        UnitSoldPriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        OriginalCurrencyID = c.Decimal(precision: 10, scale: 0),
                        SellingCurrencyID = c.Decimal(precision: 10, scale: 0),
                        DateReceived = c.DateTime(),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PurchaseOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.OriginalCurrencyID)
                .ForeignKey("CLARITY.Products", t => t.ProductID)
                .ForeignKey("CLARITY.Currencies", t => t.SellingCurrencyID)
                .ForeignKey("CLARITY.PurchaseOrderItemStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.StatusID)
                .Index(t => t.ProductID)
                .Index(t => t.UserID)
                .Index(t => t.OriginalCurrencyID)
                .Index(t => t.SellingCurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedPurchaseOrderItemDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PurchaseOrderItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PurchaseOrderItemStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PurchaseOrderItemTargets",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NothingToShip = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DestinationContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OriginProductInventoryLocationSectionID = c.Decimal(precision: 10, scale: 0),
                        OriginStoreProductID = c.Decimal(precision: 10, scale: 0),
                        OriginVendorProductID = c.Decimal(precision: 10, scale: 0),
                        SelectedRateQuoteID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("CLARITY.PurchaseOrderItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductInventoryLocationSections", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("CLARITY.StoreProducts", t => t.OriginStoreProductID)
                .ForeignKey("CLARITY.VendorProducts", t => t.OriginVendorProductID)
                .ForeignKey("CLARITY.RateQuotes", t => t.SelectedRateQuoteID)
                .ForeignKey("CLARITY.SalesItemTargetTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreProductID)
                .Index(t => t.OriginVendorProductID)
                .Index(t => t.SelectedRateQuoteID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoiceItems",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Sku = c.String(maxLength: 1000),
                        UnitOfMeasure = c.String(maxLength: 100),
                        ForceUniqueLineItemKey = c.String(maxLength: 1024),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityBackOrdered = c.Decimal(precision: 18, scale: 2),
                        QuantityPreSold = c.Decimal(precision: 18, scale: 2),
                        UnitCorePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitSoldPrice = c.Decimal(precision: 18, scale: 2),
                        UnitCorePriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        UnitSoldPriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        OriginalCurrencyID = c.Decimal(precision: 10, scale: 0),
                        SellingCurrencyID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesInvoices", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.OriginalCurrencyID)
                .ForeignKey("CLARITY.Products", t => t.ProductID)
                .ForeignKey("CLARITY.Currencies", t => t.SellingCurrencyID)
                .ForeignKey("CLARITY.SalesInvoiceItemStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.StatusID)
                .Index(t => t.ProductID)
                .Index(t => t.UserID)
                .Index(t => t.OriginalCurrencyID)
                .Index(t => t.SellingCurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSalesInvoiceItemDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesInvoiceItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoiceItemStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoiceItemTargets",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NothingToShip = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DestinationContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OriginProductInventoryLocationSectionID = c.Decimal(precision: 10, scale: 0),
                        OriginStoreProductID = c.Decimal(precision: 10, scale: 0),
                        OriginVendorProductID = c.Decimal(precision: 10, scale: 0),
                        SelectedRateQuoteID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesInvoiceItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductInventoryLocationSections", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("CLARITY.StoreProducts", t => t.OriginStoreProductID)
                .ForeignKey("CLARITY.VendorProducts", t => t.OriginVendorProductID)
                .ForeignKey("CLARITY.RateQuotes", t => t.SelectedRateQuoteID)
                .ForeignKey("CLARITY.SalesItemTargetTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreProductID)
                .Index(t => t.OriginVendorProductID)
                .Index(t => t.SelectedRateQuoteID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderItems",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Sku = c.String(maxLength: 1000),
                        UnitOfMeasure = c.String(maxLength: 100),
                        ForceUniqueLineItemKey = c.String(maxLength: 1024),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityBackOrdered = c.Decimal(precision: 18, scale: 2),
                        QuantityPreSold = c.Decimal(precision: 18, scale: 2),
                        UnitCorePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitSoldPrice = c.Decimal(precision: 18, scale: 2),
                        UnitCorePriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        UnitSoldPriceInSellingCurrency = c.Decimal(precision: 18, scale: 2),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        OriginalCurrencyID = c.Decimal(precision: 10, scale: 0),
                        SellingCurrencyID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.OriginalCurrencyID)
                .ForeignKey("CLARITY.Products", t => t.ProductID)
                .ForeignKey("CLARITY.Currencies", t => t.SellingCurrencyID)
                .ForeignKey("CLARITY.SalesOrderItemStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.StatusID)
                .Index(t => t.ProductID)
                .Index(t => t.UserID)
                .Index(t => t.OriginalCurrencyID)
                .Index(t => t.SellingCurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSalesOrderItemDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesOrderItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderItemStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderItemTargets",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NothingToShip = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DestinationContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OriginProductInventoryLocationSectionID = c.Decimal(precision: 10, scale: 0),
                        OriginStoreProductID = c.Decimal(precision: 10, scale: 0),
                        OriginVendorProductID = c.Decimal(precision: 10, scale: 0),
                        SelectedRateQuoteID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("CLARITY.SalesOrderItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductInventoryLocationSections", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("CLARITY.StoreProducts", t => t.OriginStoreProductID)
                .ForeignKey("CLARITY.VendorProducts", t => t.OriginVendorProductID)
                .ForeignKey("CLARITY.RateQuotes", t => t.SelectedRateQuoteID)
                .ForeignKey("CLARITY.SalesItemTargetTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreProductID)
                .Index(t => t.OriginVendorProductID)
                .Index(t => t.SelectedRateQuoteID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.NoteTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        IsPublic = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsCustomer = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CartStates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CartStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CartFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Carts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CartTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        CreatedByUserID = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.CreatedByUserID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.StoreID)
                .Index(t => t.CreatedByUserID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CartItemStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CartItemTargets",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NothingToShip = c.Decimal(nullable: false, precision: 1, scale: 0),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DestinationContactID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OriginProductInventoryLocationSectionID = c.Decimal(precision: 10, scale: 0),
                        OriginStoreProductID = c.Decimal(precision: 10, scale: 0),
                        OriginVendorProductID = c.Decimal(precision: 10, scale: 0),
                        SelectedRateQuoteID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.DestinationContactID, cascadeDelete: true)
                .ForeignKey("CLARITY.CartItems", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductInventoryLocationSections", t => t.OriginProductInventoryLocationSectionID)
                .ForeignKey("CLARITY.StoreProducts", t => t.OriginStoreProductID)
                .ForeignKey("CLARITY.VendorProducts", t => t.OriginVendorProductID)
                .ForeignKey("CLARITY.RateQuotes", t => t.SelectedRateQuoteID)
                .ForeignKey("CLARITY.SalesItemTargetTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.DestinationContactID)
                .Index(t => t.OriginProductInventoryLocationSectionID)
                .Index(t => t.OriginStoreProductID)
                .Index(t => t.OriginVendorProductID)
                .Index(t => t.SelectedRateQuoteID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountProducts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Products", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Products", t => t.MasterID)
                .ForeignKey("CLARITY.ProductImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ManufacturerProducts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Manufacturers", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Products", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Packages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Width = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WidthUnitOfMeasure = c.String(maxLength: 64),
                        Depth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DepthUnitOfMeasure = c.String(maxLength: 64),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeightUnitOfMeasure = c.String(maxLength: 64),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeightUnitOfMeasure = c.String(maxLength: 64),
                        DimensionalWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DimensionalWeightUnitOfMeasure = c.String(maxLength: 64),
                        IsCustom = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PackageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PackageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductAssociations",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Quantity = c.Decimal(precision: 18, scale: 2),
                        UnitOfMeasure = c.String(maxLength: 128),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Products", t => t.MasterID)
                .ForeignKey("CLARITY.Products", t => t.SlaveID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.ProductAssociationTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.StoreID)
                .Index(t => t.TypeID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductAssociationTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductCategories",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Products", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Categories", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductNotifications",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ProductID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.ProductID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductPricePoints",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        Price = c.Decimal(precision: 18, scale: 2),
                        PercentDiscount = c.Decimal(precision: 18, scale: 2),
                        MinQuantity = c.Decimal(precision: 18, scale: 2),
                        MaxQuantity = c.Decimal(precision: 18, scale: 2),
                        UnitOfMeasure = c.String(maxLength: 10),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        PriceRoundingID = c.Decimal(precision: 10, scale: 0),
                        CurrencyID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Currencies", t => t.CurrencyID)
                .ForeignKey("CLARITY.Products", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.PriceRoundings", t => t.PriceRoundingID)
                .ForeignKey("CLARITY.PricePoints", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.StoreID)
                .Index(t => t.PriceRoundingID)
                .Index(t => t.CurrencyID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRoundings",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PricePointKey = c.String(maxLength: 100),
                        ProductKey = c.String(maxLength: 100),
                        CurrencyKey = c.String(maxLength: 100),
                        UnitOfMeasure = c.String(maxLength: 100),
                        RoundHow = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RoundTo = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RoundingAmount = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductRestrictions",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ProductID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CanPurchaseInternationally = c.Decimal(nullable: false, precision: 1, scale: 0),
                        CanPurchaseDomestically = c.Decimal(nullable: false, precision: 1, scale: 0),
                        CanPurchaseIntraRegion = c.Decimal(nullable: false, precision: 1, scale: 0),
                        CanShipInternationally = c.Decimal(nullable: false, precision: 1, scale: 0),
                        CanShipDomestically = c.Decimal(nullable: false, precision: 1, scale: 0),
                        CanShipIntraRegion = c.Decimal(nullable: false, precision: 1, scale: 0),
                        RestrictionsApplyToCity = c.String(maxLength: 128),
                        RestrictionsApplyToPostalCode = c.String(maxLength: 24),
                        OverrideWithRoles = c.String(maxLength: 1024),
                        OverrideWithAccountTypeID = c.Decimal(precision: 10, scale: 0),
                        RestrictionsApplyToCountryID = c.Decimal(precision: 10, scale: 0),
                        RestrictionsApplyToRegionID = c.Decimal(precision: 10, scale: 0),
                        RestrictionsApplyToDistrictID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.AccountTypes", t => t.OverrideWithAccountTypeID)
                .ForeignKey("CLARITY.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("CLARITY.Countries", t => t.RestrictionsApplyToCountryID)
                .ForeignKey("CLARITY.Districts", t => t.RestrictionsApplyToDistrictID)
                .ForeignKey("CLARITY.Regions", t => t.RestrictionsApplyToRegionID)
                .Index(t => t.ID)
                .Index(t => t.ProductID)
                .Index(t => t.OverrideWithAccountTypeID)
                .Index(t => t.RestrictionsApplyToCountryID)
                .Index(t => t.RestrictionsApplyToRegionID)
                .Index(t => t.RestrictionsApplyToDistrictID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProductFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Products", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CategoryFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Categories", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreCategories",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Categories", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.CategoryTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ParentID = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.CategoryTypes", t => t.ParentID)
                .Index(t => t.ID)
                .Index(t => t.ParentID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreCategoryTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.CategoryTypes", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ManufacturerTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreBadges",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Badges", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Badges",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.BadgeTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.BadgeImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Badges", t => t.MasterID)
                .ForeignKey("CLARITY.BadgeImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.BadgeImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.BadgeTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreContacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Contacts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.StoreTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountCategories",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Categories", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountCountries",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Countries", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountManufacturers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Manufacturers", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountProductTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductTypes", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountShipCarrierMethods",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ShipCarrierMethods", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountStores",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountUserRoles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        RoleName = c.String(maxLength: 128),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountUsers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.DiscountVendors",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Discounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Vendors", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoicePayments",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesInvoices", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Payments", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoiceStates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoiceStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoiceFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesInvoices", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesInvoiceTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderContacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Contacts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedSalesOrderDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderEvents",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        OldStateID = c.Decimal(precision: 10, scale: 0),
                        NewStateID = c.Decimal(precision: 10, scale: 0),
                        OldStatusID = c.Decimal(precision: 10, scale: 0),
                        NewStatusID = c.Decimal(precision: 10, scale: 0),
                        OldTypeID = c.Decimal(precision: 10, scale: 0),
                        NewTypeID = c.Decimal(precision: 10, scale: 0),
                        OldBalanceDue = c.Decimal(precision: 18, scale: 2),
                        NewBalanceDue = c.Decimal(precision: 18, scale: 2),
                        OldHash = c.Decimal(precision: 19, scale: 0),
                        NewHash = c.Decimal(precision: 19, scale: 0),
                        OldRecordSerialized = c.String(),
                        NewRecordSerialized = c.String(),
                        SalesOrderID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesOrders", t => t.SalesOrderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.SalesOrderID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderPayments",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Payments", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderStates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SalesOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SalesOrderTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PurchaseOrderContacts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PurchaseOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Contacts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AppliedPurchaseOrderDiscounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PurchaseOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Discounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PurchaseOrderStates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PurchaseOrderStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PurchaseOrderFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PurchaseOrders", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PurchaseOrderTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ContactImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Contacts", t => t.MasterID)
                .ForeignKey("CLARITY.ContactImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ContactImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ContactTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountCurrencies",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomName = c.String(maxLength: 128),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Currencies", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountPricePoints",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.PricePoints", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountUserRoles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.UserRoles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountImages",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        IsPrimary = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalWidth = c.Decimal(precision: 10, scale: 0),
                        OriginalHeight = c.Decimal(precision: 10, scale: 0),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Decimal(precision: 10, scale: 0),
                        ThumbnailHeight = c.Decimal(precision: 10, scale: 0),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Decimal(precision: 10, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.MasterID)
                .ForeignKey("CLARITY.AccountImageTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountImageTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountFiles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SeoUrl = c.String(maxLength: 512),
                        SeoKeywords = c.String(maxLength: 512),
                        SeoPageTitle = c.String(maxLength: 75),
                        SeoDescription = c.String(maxLength: 256),
                        SeoMetaData = c.String(maxLength: 512),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FileAccessTypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.StoredFiles", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountAssociationTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AccountUsageBalances",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Quantity = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Accounts", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Products", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AttributeGroups",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AttributeTabs",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.AttributeTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.GeneralAttributes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        CustomKey = c.String(nullable: false, maxLength: 128),
                        IsFilter = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsComparable = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsPredefined = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsMarkup = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsTab = c.Decimal(nullable: false, precision: 1, scale: 0),
                        HideFromStorefront = c.Decimal(nullable: false, precision: 1, scale: 0),
                        HideFromSuppliers = c.Decimal(nullable: false, precision: 1, scale: 0),
                        HideFromProductDetailView = c.Decimal(nullable: false, precision: 1, scale: 0),
                        HideFromCatalogViews = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        AttributeTabID = c.Decimal(precision: 10, scale: 0),
                        AttributeGroupID = c.Decimal(precision: 10, scale: 0),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.AttributeGroups", t => t.AttributeGroupID)
                .ForeignKey("CLARITY.AttributeTabs", t => t.AttributeTabID)
                .ForeignKey("CLARITY.AttributeTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey, unique: true)
                .Index(t => t.TypeID)
                .Index(t => t.AttributeTabID)
                .Index(t => t.AttributeGroupID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.GeneralAttributePredefinedOptions",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Value = c.String(nullable: false),
                        UofM = c.String(maxLength: 64),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        AttributeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.GeneralAttributes", t => t.AttributeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.AttributeID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.EventLogs",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        DataID = c.Decimal(precision: 10, scale: 0),
                        LogLevel = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .Index(t => t.ID)
                .Index(t => t.StoreID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Events",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IPAddress = c.String(maxLength: 20),
                        Score = c.Decimal(precision: 10, scale: 0),
                        AddressID = c.Decimal(precision: 10, scale: 0),
                        IPOrganizationID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        DidBounce = c.Decimal(precision: 1, scale: 0),
                        OperatingSystem = c.String(maxLength: 20),
                        Browser = c.String(maxLength: 10),
                        Language = c.String(maxLength: 50),
                        ContainsSocialProfile = c.Decimal(precision: 1, scale: 0),
                        Delta = c.Decimal(precision: 10, scale: 0),
                        Duration = c.Decimal(precision: 10, scale: 0),
                        StartedOn = c.DateTime(),
                        EndedOn = c.DateTime(),
                        Time = c.String(maxLength: 100),
                        EntryPage = c.String(maxLength: 2000),
                        ExitPage = c.String(maxLength: 2000),
                        IsFirstTrigger = c.Decimal(precision: 1, scale: 0),
                        Flash = c.String(maxLength: 10),
                        Keywords = c.String(maxLength: 100),
                        PartitionKey = c.String(maxLength: 100),
                        Referrer = c.String(maxLength: 2000),
                        ReferringHost = c.String(maxLength: 300),
                        RowKey = c.String(maxLength: 50),
                        Source = c.Decimal(precision: 10, scale: 0),
                        TotalTriggers = c.Decimal(precision: 10, scale: 0),
                        CampaignID = c.Decimal(precision: 10, scale: 0),
                        ContactID = c.Decimal(precision: 10, scale: 0),
                        SiteDomainID = c.Decimal(precision: 10, scale: 0),
                        VisitorID = c.Decimal(precision: 10, scale: 0),
                        VisitID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Addresses", t => t.AddressID)
                .ForeignKey("CLARITY.Campaigns", t => t.CampaignID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID)
                .ForeignKey("CLARITY.IPOrganizations", t => t.IPOrganizationID)
                .ForeignKey("CLARITY.SiteDomains", t => t.SiteDomainID)
                .ForeignKey("CLARITY.EventStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.EventTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .ForeignKey("CLARITY.Visits", t => t.VisitID)
                .ForeignKey("CLARITY.Visitors", t => t.VisitorID)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.AddressID)
                .Index(t => t.IPOrganizationID)
                .Index(t => t.UserID)
                .Index(t => t.CampaignID)
                .Index(t => t.ContactID)
                .Index(t => t.SiteDomainID)
                .Index(t => t.VisitorID)
                .Index(t => t.VisitID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.IPOrganizations",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IPAddress = c.String(maxLength: 20),
                        Score = c.Decimal(precision: 10, scale: 0),
                        VisitorKey = c.String(maxLength: 50),
                        AddressID = c.Decimal(precision: 10, scale: 0),
                        PrimaryUserID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Addresses", t => t.AddressID)
                .ForeignKey("CLARITY.Users", t => t.PrimaryUserID)
                .ForeignKey("CLARITY.IPOrganizationStatus", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StatusID)
                .Index(t => t.AddressID)
                .Index(t => t.PrimaryUserID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.IPOrganizationStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PageViewEvents",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PageViews", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Events", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PageViews",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IPAddress = c.String(maxLength: 20),
                        Score = c.Decimal(precision: 10, scale: 0),
                        AddressID = c.Decimal(precision: 10, scale: 0),
                        IPOrganizationID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        DidBounce = c.Decimal(precision: 1, scale: 0),
                        OperatingSystem = c.String(maxLength: 20),
                        Browser = c.String(maxLength: 10),
                        Language = c.String(maxLength: 50),
                        ContainsSocialProfile = c.Decimal(precision: 1, scale: 0),
                        Delta = c.Decimal(precision: 10, scale: 0),
                        Duration = c.Decimal(precision: 10, scale: 0),
                        StartedOn = c.DateTime(),
                        EndedOn = c.DateTime(),
                        Time = c.String(maxLength: 100),
                        EntryPage = c.String(maxLength: 2000),
                        ExitPage = c.String(maxLength: 2000),
                        IsFirstTrigger = c.Decimal(precision: 1, scale: 0),
                        Flash = c.String(maxLength: 10),
                        Keywords = c.String(maxLength: 100),
                        PartitionKey = c.String(maxLength: 100),
                        Referrer = c.String(maxLength: 2000),
                        ReferringHost = c.String(maxLength: 300),
                        RowKey = c.String(maxLength: 50),
                        Source = c.Decimal(precision: 10, scale: 0),
                        TotalTriggers = c.Decimal(precision: 10, scale: 0),
                        CampaignID = c.Decimal(precision: 10, scale: 0),
                        ContactID = c.Decimal(precision: 10, scale: 0),
                        SiteDomainID = c.Decimal(precision: 10, scale: 0),
                        VisitorID = c.Decimal(precision: 10, scale: 0),
                        Title = c.String(maxLength: 500),
                        URI = c.String(maxLength: 2000),
                        ViewedOn = c.DateTime(),
                        VisitKey = c.String(maxLength: 50),
                        CategoryID = c.Decimal(precision: 10, scale: 0),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Addresses", t => t.AddressID)
                .ForeignKey("CLARITY.Campaigns", t => t.CampaignID)
                .ForeignKey("CLARITY.Categories", t => t.CategoryID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID)
                .ForeignKey("CLARITY.IPOrganizations", t => t.IPOrganizationID)
                .ForeignKey("CLARITY.Products", t => t.ProductID)
                .ForeignKey("CLARITY.SiteDomains", t => t.SiteDomainID)
                .ForeignKey("CLARITY.PageViewStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.PageViewTypes", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .ForeignKey("CLARITY.Visitors", t => t.VisitorID)
                .Index(t => t.ID)
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
                .Index(t => t.ProductID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PageViewStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PageViewTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Visitors",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        IPAddress = c.String(maxLength: 20),
                        Score = c.Decimal(precision: 10, scale: 0),
                        AddressID = c.Decimal(precision: 10, scale: 0),
                        IPOrganizationID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Addresses", t => t.AddressID)
                .ForeignKey("CLARITY.IPOrganizations", t => t.IPOrganizationID)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.AddressID)
                .Index(t => t.IPOrganizationID)
                .Index(t => t.UserID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Visits",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IPAddress = c.String(maxLength: 20),
                        Score = c.Decimal(precision: 10, scale: 0),
                        AddressID = c.Decimal(precision: 10, scale: 0),
                        IPOrganizationID = c.Decimal(precision: 10, scale: 0),
                        UserID = c.Decimal(precision: 10, scale: 0),
                        DidBounce = c.Decimal(precision: 1, scale: 0),
                        OperatingSystem = c.String(maxLength: 20),
                        Browser = c.String(maxLength: 10),
                        Language = c.String(maxLength: 50),
                        ContainsSocialProfile = c.Decimal(precision: 1, scale: 0),
                        Delta = c.Decimal(precision: 10, scale: 0),
                        Duration = c.Decimal(precision: 10, scale: 0),
                        StartedOn = c.DateTime(),
                        EndedOn = c.DateTime(),
                        Time = c.String(maxLength: 100),
                        EntryPage = c.String(maxLength: 2000),
                        ExitPage = c.String(maxLength: 2000),
                        IsFirstTrigger = c.Decimal(precision: 1, scale: 0),
                        Flash = c.String(maxLength: 10),
                        Keywords = c.String(maxLength: 100),
                        PartitionKey = c.String(maxLength: 100),
                        Referrer = c.String(maxLength: 2000),
                        ReferringHost = c.String(maxLength: 300),
                        RowKey = c.String(maxLength: 50),
                        Source = c.Decimal(precision: 10, scale: 0),
                        TotalTriggers = c.Decimal(precision: 10, scale: 0),
                        CampaignID = c.Decimal(precision: 10, scale: 0),
                        ContactID = c.Decimal(precision: 10, scale: 0),
                        SiteDomainID = c.Decimal(precision: 10, scale: 0),
                        VisitorID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Addresses", t => t.AddressID)
                .ForeignKey("CLARITY.Campaigns", t => t.CampaignID)
                .ForeignKey("CLARITY.Contacts", t => t.ContactID)
                .ForeignKey("CLARITY.IPOrganizations", t => t.IPOrganizationID)
                .ForeignKey("CLARITY.SiteDomains", t => t.SiteDomainID)
                .ForeignKey("CLARITY.VisitStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Users", t => t.UserID)
                .ForeignKey("CLARITY.Visitors", t => t.VisitorID)
                .Index(t => t.ID)
                .Index(t => t.StatusID)
                .Index(t => t.AddressID)
                .Index(t => t.IPOrganizationID)
                .Index(t => t.UserID)
                .Index(t => t.CampaignID)
                .Index(t => t.ContactID)
                .Index(t => t.SiteDomainID)
                .Index(t => t.VisitorID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.VisitStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.EventStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.EventTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.FavoriteShipCarriers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ShipCarriers", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.FutureImports",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StatusID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        FileName = c.String(nullable: false, maxLength: 512),
                        RunImportAt = c.DateTime(nullable: false),
                        Attempts = c.Decimal(nullable: false, precision: 10, scale: 0),
                        HasError = c.Decimal(nullable: false, precision: 1, scale: 0),
                        VendorID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.FutureImportStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.Vendors", t => t.VendorID)
                .Index(t => t.ID)
                .Index(t => t.StatusID)
                .Index(t => t.StoreID)
                .Index(t => t.FileName)
                .Index(t => t.RunImportAt)
                .Index(t => t.VendorID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.FutureImportStatus",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.HangfireAggregatedCounters",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.Decimal(nullable: false, precision: 19, scale: 0),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Key)
                .Index(t => t.ExpireAt, name: "[IX_HangFire_AggregatedCounter_ExpireAt]");

            CreateTable(
                "CLARITY.HangfireCounters",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Key);

            CreateTable(
                "CLARITY.HangfireHashes",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 100),
                        Field = c.String(nullable: false, maxLength: 100),
                        Value = c.String(),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Key, t.Field })
                .Index(t => t.Key, name: "IX_HangFire_Hash_Key")
                .Index(t => new { t.Key, t.Field }, unique: true, name: "UX_HangFire_Hash_Key_Field")
                .Index(t => t.ExpireAt, name: "IX_HangFire_Hash_ExpireAt");

            CreateTable(
                "CLARITY.HangfireJobParameters",
                c => new
                    {
                        JobId = c.Decimal(nullable: false, precision: 19, scale: 0),
                        Name = c.String(nullable: false, maxLength: 40),
                        Value = c.String(),
                    })
                .PrimaryKey(t => new { t.JobId, t.Name })
                .ForeignKey("CLARITY.HangfireJobs", t => t.JobId, cascadeDelete: true)
                .Index(t => new { t.JobId, t.Name }, name: "IX_HangFire_JobParameter_JobIdAndName");

            CreateTable(
                "CLARITY.HangfireJobs",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 19, scale: 0, identity: true),
                        StateId = c.Decimal(precision: 19, scale: 0),
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
                "CLARITY.HangfireStates",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 19, scale: 0, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Reason = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        Data = c.String(),
                        JobId = c.Decimal(nullable: false, precision: 19, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("CLARITY.HangfireJobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);

            CreateTable(
                "CLARITY.HangfireJobQueues",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Queue = c.String(nullable: false, maxLength: 50),
                        FetchedAt = c.DateTime(),
                        JobId = c.Decimal(nullable: false, precision: 19, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("CLARITY.HangfireJobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => new { t.Queue, t.Id }, name: "IX_HangFire_JobQueue_QueueAndId")
                .Index(t => t.JobId);

            CreateTable(
                "CLARITY.HangfireLists",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 19, scale: 0, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.String(),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ExpireAt);

            CreateTable(
                "CLARITY.HangfireSchemas",
                c => new
                    {
                        Version = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Version);

            CreateTable(
                "CLARITY.HangfireServers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        Data = c.String(),
                        LastHeartbeat = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.LastHeartbeat, name: "IX_HangFire_Server_LastHeartbeat");

            CreateTable(
                "CLARITY.HangfireSets",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 100),
                        Score = c.Decimal(nullable: false, precision: 38, scale: 0),
                        Value = c.String(nullable: false, maxLength: 256),
                        ExpireAt = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Key, t.Value })
                .Index(t => t.Key)
                .Index(t => new { t.Key, t.Score }, name: "IX_HangFire_Set_Score")
                .Index(t => new { t.Key, t.Value }, name: "UX_HangFire_Set_KeyAndValue")
                .Index(t => t.ExpireAt);

            CreateTable(
                "CLARITY.HistoricalAddressValidations",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        AddressHash = c.Decimal(precision: 19, scale: 0),
                        OnDate = c.DateTime(nullable: false),
                        IsValid = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Provider = c.String(maxLength: 128),
                        SerializedRequest = c.String(),
                        SerializedResponse = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.HistoricalTaxRates",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Provider = c.String(maxLength: 128),
                        CartHash = c.Decimal(precision: 19, scale: 0),
                        OnDate = c.DateTime(nullable: false),
                        CountryLevelRate = c.Decimal(precision: 18, scale: 2),
                        RegionLevelRate = c.Decimal(precision: 18, scale: 2),
                        DistrictLevelRate = c.Decimal(precision: 18, scale: 2),
                        CountyLevelRate = c.Decimal(precision: 18, scale: 2),
                        TotalAmount = c.Decimal(precision: 18, scale: 2),
                        TotalTaxable = c.Decimal(precision: 18, scale: 2),
                        TotalTax = c.Decimal(precision: 18, scale: 2),
                        TotalTaxCalculated = c.Decimal(precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SerializedRequest = c.String(),
                        SerializedResponse = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ImportExportMappings",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MappingJson = c.String(nullable: false),
                        MappingJsonHash = c.Decimal(precision: 19, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PhonePrefixLookups",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Prefix = c.String(maxLength: 20),
                        TimeZone = c.String(maxLength: 255),
                        CityName = c.String(maxLength: 255),
                        CountryID = c.Decimal(precision: 10, scale: 0),
                        RegionID = c.Decimal(precision: 10, scale: 0),
                        DistrictID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Countries", t => t.CountryID)
                .ForeignKey("CLARITY.Districts", t => t.DistrictID)
                .ForeignKey("CLARITY.Regions", t => t.RegionID)
                .Index(t => t.ID)
                .Index(t => t.Prefix)
                .Index(t => t.CountryID)
                .Index(t => t.RegionID)
                .Index(t => t.DistrictID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleAccounts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Accounts", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRules",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        UnitOfMeasure = c.String(maxLength: 128),
                        PriceAdjustment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinQuantity = c.Decimal(precision: 18, scale: 2),
                        MaxQuantity = c.Decimal(precision: 18, scale: 2),
                        IsPercentage = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsMarkup = c.Decimal(nullable: false, precision: 1, scale: 0),
                        UsePriceBase = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsExclusive = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsOnlyForAnonymousUsers = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Priority = c.Decimal(precision: 10, scale: 0),
                        CurrencyID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Currencies", t => t.CurrencyID)
                .Index(t => t.ID)
                .Index(t => t.CurrencyID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleManufacturers",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Manufacturers", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleAccountTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.AccountTypes", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleCategories",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Categories", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleCountries",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Countries", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleProductTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.ProductTypes", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleUserRoles",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        RoleName = c.String(maxLength: 128),
                        PriceRuleID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.PriceRuleID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.PriceRuleID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleProducts",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OverridePrice = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OverrideBasePrice = c.Decimal(precision: 18, scale: 2),
                        OverrideSalePrice = c.Decimal(precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Products", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleStores",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Stores", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.PriceRuleVendors",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.PriceRules", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Vendors", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ProfanityFilters",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Reports",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ResultsData = c.String(),
                        RunByUserID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.Users", t => t.RunByUserID, cascadeDelete: true)
                .ForeignKey("CLARITY.ReportTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.RunByUserID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ReportTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Template = c.Binary(),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ScheduledJobConfigurations",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NotificationTemplateID = c.Decimal(precision: 10, scale: 0),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.EmailTemplates", t => t.NotificationTemplateID)
                .Index(t => t.ID)
                .Index(t => t.NotificationTemplateID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ScheduledJobConfigurationSettings",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MasterID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SlaveID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.ScheduledJobConfigurations", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("CLARITY.Settings", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.Settings",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TypeID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        StoreID = c.Decimal(precision: 10, scale: 0),
                        Value = c.String(nullable: false),
                        SettingGroupID = c.Decimal(precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.SettingGroups", t => t.SettingGroupID)
                .ForeignKey("CLARITY.Stores", t => t.StoreID)
                .ForeignKey("CLARITY.SettingTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StoreID)
                .Index(t => t.SettingGroupID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SettingGroups",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.SettingTypes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        SortOrder = c.Decimal(precision: 10, scale: 0),
                        TranslationKey = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        Description = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UiKeys",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Type = c.String(),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.UiTranslations",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Locale = c.String(maxLength: 1024),
                        Value = c.String(maxLength: 1024),
                        UiKeyID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CLARITY.UiKeys", t => t.UiKeyID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.UiKeyID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "CLARITY.ZipCodes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ZipCode = c.String(maxLength: 20),
                        ZipType = c.String(maxLength: 255),
                        CityName = c.String(maxLength: 255),
                        CityType = c.String(maxLength: 255),
                        CountyName = c.String(maxLength: 255),
                        CountyFIPS = c.Decimal(precision: 19, scale: 0),
                        StateName = c.String(maxLength: 255),
                        StateAbbreviation = c.String(maxLength: 255),
                        StateFIPS = c.Decimal(precision: 19, scale: 0),
                        MSACode = c.Decimal(precision: 19, scale: 0),
                        AreaCode = c.String(maxLength: 255),
                        TimeZone = c.String(maxLength: 255),
                        UTC = c.Decimal(precision: 19, scale: 0),
                        DST = c.String(maxLength: 255),
                        Latitude = c.Decimal(precision: 18, scale: 2),
                        Longitude = c.Decimal(precision: 18, scale: 2),
                        CustomKey = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        Hash = c.Decimal(precision: 19, scale: 0),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.ZipCode)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);
        }

        public override void Down()
        {
            DropForeignKey("CLARITY.UiTranslations", "UiKeyID", "CLARITY.UiKeys");
            DropForeignKey("CLARITY.ScheduledJobConfigurationSettings", "SlaveID", "CLARITY.Settings");
            DropForeignKey("CLARITY.Settings", "TypeID", "CLARITY.SettingTypes");
            DropForeignKey("CLARITY.Settings", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Settings", "SettingGroupID", "CLARITY.SettingGroups");
            DropForeignKey("CLARITY.ScheduledJobConfigurationSettings", "MasterID", "CLARITY.ScheduledJobConfigurations");
            DropForeignKey("CLARITY.ScheduledJobConfigurations", "NotificationTemplateID", "CLARITY.EmailTemplates");
            DropForeignKey("CLARITY.Reports", "TypeID", "CLARITY.ReportTypes");
            DropForeignKey("CLARITY.Reports", "RunByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.PriceRuleAccounts", "SlaveID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.PriceRuleAccounts", "MasterID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRuleVendors", "SlaveID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.PriceRuleVendors", "MasterID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRuleStores", "SlaveID", "CLARITY.Stores");
            DropForeignKey("CLARITY.PriceRuleStores", "MasterID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRuleProducts", "SlaveID", "CLARITY.Products");
            DropForeignKey("CLARITY.PriceRuleProducts", "MasterID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRuleUserRoles", "PriceRuleID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRuleProductTypes", "SlaveID", "CLARITY.ProductTypes");
            DropForeignKey("CLARITY.PriceRuleProductTypes", "MasterID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRuleCountries", "SlaveID", "CLARITY.Countries");
            DropForeignKey("CLARITY.PriceRuleCountries", "MasterID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRuleCategories", "SlaveID", "CLARITY.Categories");
            DropForeignKey("CLARITY.PriceRuleCategories", "MasterID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRuleAccountTypes", "SlaveID", "CLARITY.AccountTypes");
            DropForeignKey("CLARITY.PriceRuleAccountTypes", "MasterID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRuleManufacturers", "SlaveID", "CLARITY.Manufacturers");
            DropForeignKey("CLARITY.PriceRuleManufacturers", "MasterID", "CLARITY.PriceRules");
            DropForeignKey("CLARITY.PriceRules", "CurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.PhonePrefixLookups", "RegionID", "CLARITY.Regions");
            DropForeignKey("CLARITY.PhonePrefixLookups", "DistrictID", "CLARITY.Districts");
            DropForeignKey("CLARITY.PhonePrefixLookups", "CountryID", "CLARITY.Countries");
            DropForeignKey("CLARITY.HangfireJobQueues", "JobId", "CLARITY.HangfireJobs");
            DropForeignKey("CLARITY.HangfireJobParameters", "JobId", "CLARITY.HangfireJobs");
            DropForeignKey("CLARITY.HangfireStates", "JobId", "CLARITY.HangfireJobs");
            DropForeignKey("CLARITY.FutureImports", "VendorID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.FutureImports", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.FutureImports", "StatusID", "CLARITY.FutureImportStatus");
            DropForeignKey("CLARITY.FavoriteShipCarriers", "SlaveID", "CLARITY.ShipCarriers");
            DropForeignKey("CLARITY.FavoriteShipCarriers", "MasterID", "CLARITY.Users");
            DropForeignKey("CLARITY.Events", "VisitorID", "CLARITY.Visitors");
            DropForeignKey("CLARITY.Events", "VisitID", "CLARITY.Visits");
            DropForeignKey("CLARITY.Events", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Events", "TypeID", "CLARITY.EventTypes");
            DropForeignKey("CLARITY.Events", "StatusID", "CLARITY.EventStatus");
            DropForeignKey("CLARITY.Events", "SiteDomainID", "CLARITY.SiteDomains");
            DropForeignKey("CLARITY.PageViewEvents", "SlaveID", "CLARITY.Events");
            DropForeignKey("CLARITY.PageViewEvents", "MasterID", "CLARITY.PageViews");
            DropForeignKey("CLARITY.PageViews", "VisitorID", "CLARITY.Visitors");
            DropForeignKey("CLARITY.Visits", "VisitorID", "CLARITY.Visitors");
            DropForeignKey("CLARITY.Visits", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Visits", "StatusID", "CLARITY.VisitStatus");
            DropForeignKey("CLARITY.Visits", "SiteDomainID", "CLARITY.SiteDomains");
            DropForeignKey("CLARITY.Visits", "IPOrganizationID", "CLARITY.IPOrganizations");
            DropForeignKey("CLARITY.Visits", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.Visits", "CampaignID", "CLARITY.Campaigns");
            DropForeignKey("CLARITY.Visits", "AddressID", "CLARITY.Addresses");
            DropForeignKey("CLARITY.Visitors", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Visitors", "IPOrganizationID", "CLARITY.IPOrganizations");
            DropForeignKey("CLARITY.Visitors", "AddressID", "CLARITY.Addresses");
            DropForeignKey("CLARITY.PageViews", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.PageViews", "TypeID", "CLARITY.PageViewTypes");
            DropForeignKey("CLARITY.PageViews", "StatusID", "CLARITY.PageViewStatus");
            DropForeignKey("CLARITY.PageViews", "SiteDomainID", "CLARITY.SiteDomains");
            DropForeignKey("CLARITY.PageViews", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.PageViews", "IPOrganizationID", "CLARITY.IPOrganizations");
            DropForeignKey("CLARITY.PageViews", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.PageViews", "CategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.PageViews", "CampaignID", "CLARITY.Campaigns");
            DropForeignKey("CLARITY.PageViews", "AddressID", "CLARITY.Addresses");
            DropForeignKey("CLARITY.Events", "IPOrganizationID", "CLARITY.IPOrganizations");
            DropForeignKey("CLARITY.IPOrganizations", "StatusID", "CLARITY.IPOrganizationStatus");
            DropForeignKey("CLARITY.IPOrganizations", "PrimaryUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.IPOrganizations", "AddressID", "CLARITY.Addresses");
            DropForeignKey("CLARITY.Events", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.Events", "CampaignID", "CLARITY.Campaigns");
            DropForeignKey("CLARITY.Events", "AddressID", "CLARITY.Addresses");
            DropForeignKey("CLARITY.EventLogs", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.GeneralAttributes", "TypeID", "CLARITY.AttributeTypes");
            DropForeignKey("CLARITY.GeneralAttributePredefinedOptions", "AttributeID", "CLARITY.GeneralAttributes");
            DropForeignKey("CLARITY.GeneralAttributes", "AttributeTabID", "CLARITY.AttributeTabs");
            DropForeignKey("CLARITY.GeneralAttributes", "AttributeGroupID", "CLARITY.AttributeGroups");
            DropForeignKey("CLARITY.AccountUsageBalances", "SlaveID", "CLARITY.Products");
            DropForeignKey("CLARITY.AccountUsageBalances", "MasterID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AccountAssociations", "TypeID", "CLARITY.AccountAssociationTypes");
            DropForeignKey("CLARITY.AccountAssociations", "SlaveID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AccountAssociations", "MasterID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.Accounts", "TypeID", "CLARITY.AccountTypes");
            DropForeignKey("CLARITY.AccountFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.AccountFiles", "MasterID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.Accounts", "StatusID", "CLARITY.AccountStatus");
            DropForeignKey("CLARITY.Accounts", "ParentID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AccountImages", "TypeID", "CLARITY.AccountImageTypes");
            DropForeignKey("CLARITY.AccountImages", "MasterID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.Accounts", "CreditCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.AccountUserRoles", "SlaveID", "CLARITY.UserRoles");
            DropForeignKey("CLARITY.AccountUserRoles", "MasterID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AccountPricePoints", "SlaveID", "CLARITY.PricePoints");
            DropForeignKey("CLARITY.AccountPricePoints", "MasterID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AccountCurrencies", "SlaveID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.AccountCurrencies", "MasterID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AccountContacts", "SlaveID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.Contacts", "TypeID", "CLARITY.ContactTypes");
            DropForeignKey("CLARITY.ContactImages", "TypeID", "CLARITY.ContactImageTypes");
            DropForeignKey("CLARITY.ContactImages", "MasterID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.PurchaseOrders", "VendorID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.PurchaseOrders", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.PurchaseOrders", "TypeID", "CLARITY.PurchaseOrderTypes");
            DropForeignKey("CLARITY.PurchaseOrderFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.PurchaseOrderFiles", "MasterID", "CLARITY.PurchaseOrders");
            DropForeignKey("CLARITY.PurchaseOrders", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.PurchaseOrders", "StatusID", "CLARITY.PurchaseOrderStatus");
            DropForeignKey("CLARITY.PurchaseOrders", "StateID", "CLARITY.PurchaseOrderStates");
            DropForeignKey("CLARITY.PurchaseOrders", "ShippingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.PurchaseOrders", "ShipCarrierID", "CLARITY.ShipCarriers");
            DropForeignKey("CLARITY.PurchaseOrders", "SalesGroupID", "CLARITY.SalesGroups");
            DropForeignKey("CLARITY.PurchaseOrders", "InventoryLocationID", "CLARITY.InventoryLocations");
            DropForeignKey("CLARITY.AppliedPurchaseOrderDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedPurchaseOrderDiscounts", "MasterID", "CLARITY.PurchaseOrders");
            DropForeignKey("CLARITY.PurchaseOrderContacts", "SlaveID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.PurchaseOrderContacts", "MasterID", "CLARITY.PurchaseOrders");
            DropForeignKey("CLARITY.PurchaseOrders", "ParentID", "CLARITY.PurchaseOrders");
            DropForeignKey("CLARITY.PurchaseOrders", "BillingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesOrderPurchaseOrders", "SlaveID", "CLARITY.PurchaseOrders");
            DropForeignKey("CLARITY.SalesOrderPurchaseOrders", "MasterID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.SalesOrders", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SalesOrders", "TypeID", "CLARITY.SalesOrderTypes");
            DropForeignKey("CLARITY.SalesOrderFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.SalesOrderFiles", "MasterID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.SalesOrders", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.SalesOrders", "StatusID", "CLARITY.SalesOrderStatus");
            DropForeignKey("CLARITY.SalesOrders", "StateID", "CLARITY.SalesOrderStates");
            DropForeignKey("CLARITY.SalesOrders", "ShippingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesOrderPayments", "SlaveID", "CLARITY.Payments");
            DropForeignKey("CLARITY.SalesOrderPayments", "MasterID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.SalesOrderEvents", "SalesOrderID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.AppliedSalesOrderDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSalesOrderDiscounts", "MasterID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.SalesOrderContacts", "SlaveID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesOrderContacts", "MasterID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.SalesOrders", "ParentID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.SalesOrders", "BillingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesOrderSalesInvoices", "SlaveID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.SalesInvoices", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SalesInvoices", "TypeID", "CLARITY.SalesInvoiceTypes");
            DropForeignKey("CLARITY.SalesInvoiceFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.SalesInvoiceFiles", "MasterID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.SalesInvoices", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.SalesInvoices", "StatusID", "CLARITY.SalesInvoiceStatus");
            DropForeignKey("CLARITY.SalesInvoices", "StateID", "CLARITY.SalesInvoiceStates");
            DropForeignKey("CLARITY.SalesInvoices", "ShippingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesInvoicePayments", "SlaveID", "CLARITY.Payments");
            DropForeignKey("CLARITY.SalesInvoicePayments", "MasterID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.SalesInvoices", "SalesGroupID", "CLARITY.SalesGroups");
            DropForeignKey("CLARITY.AppliedSalesInvoiceDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountVendors", "SlaveID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.DiscountVendors", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountUsers", "SlaveID", "CLARITY.Users");
            DropForeignKey("CLARITY.DiscountUsers", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountUserRoles", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountStores", "SlaveID", "CLARITY.Stores");
            DropForeignKey("CLARITY.DiscountStores", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountShipCarrierMethods", "SlaveID", "CLARITY.ShipCarrierMethods");
            DropForeignKey("CLARITY.DiscountShipCarrierMethods", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountProductTypes", "SlaveID", "CLARITY.ProductTypes");
            DropForeignKey("CLARITY.DiscountProductTypes", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountManufacturers", "SlaveID", "CLARITY.Manufacturers");
            DropForeignKey("CLARITY.DiscountManufacturers", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountCountries", "SlaveID", "CLARITY.Countries");
            DropForeignKey("CLARITY.DiscountCountries", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountCategories", "SlaveID", "CLARITY.Categories");
            DropForeignKey("CLARITY.DiscountCategories", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountAccountTypes", "SlaveID", "CLARITY.AccountTypes");
            DropForeignKey("CLARITY.AccountTypes", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Stores", "TypeID", "CLARITY.StoreTypes");
            DropForeignKey("CLARITY.StoreContacts", "SlaveID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.StoreContacts", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.StoreBadges", "SlaveID", "CLARITY.Badges");
            DropForeignKey("CLARITY.Badges", "TypeID", "CLARITY.BadgeTypes");
            DropForeignKey("CLARITY.BadgeImages", "TypeID", "CLARITY.BadgeImageTypes");
            DropForeignKey("CLARITY.BadgeImages", "MasterID", "CLARITY.Badges");
            DropForeignKey("CLARITY.StoreBadges", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Stores", "MinimumOrderQuantityAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Stores", "MinimumOrderQuantityAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Stores", "MinimumOrderDollarAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Stores", "MinimumOrderDollarAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Stores", "MinimumForFreeShippingQuantityAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Stores", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Stores", "MinimumForFreeShippingDollarAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Stores", "MinimumForFreeShippingDollarAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.StoreManufacturers", "SlaveID", "CLARITY.Manufacturers");
            DropForeignKey("CLARITY.Manufacturers", "TypeID", "CLARITY.ManufacturerTypes");
            DropForeignKey("CLARITY.Manufacturers", "MinimumOrderQuantityAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Manufacturers", "MinimumOrderQuantityAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Manufacturers", "MinimumOrderDollarAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Manufacturers", "MinimumOrderDollarAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Manufacturers", "MinimumForFreeShippingQuantityAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Manufacturers", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Manufacturers", "MinimumForFreeShippingDollarAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Manufacturers", "MinimumForFreeShippingDollarAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Categories", "TypeID", "CLARITY.CategoryTypes");
            DropForeignKey("CLARITY.StoreCategoryTypes", "SlaveID", "CLARITY.CategoryTypes");
            DropForeignKey("CLARITY.StoreCategoryTypes", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.CategoryTypes", "ParentID", "CLARITY.CategoryTypes");
            DropForeignKey("CLARITY.StoreCategories", "SlaveID", "CLARITY.Categories");
            DropForeignKey("CLARITY.StoreCategories", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.CategoryFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.CategoryFiles", "MasterID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Categories", "RestockingFeeAmountCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.Categories", "ParentID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Categories", "MinimumOrderQuantityAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Categories", "MinimumOrderQuantityAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Categories", "MinimumOrderDollarAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Categories", "MinimumOrderDollarAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Categories", "MinimumForFreeShippingQuantityAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Categories", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Categories", "MinimumForFreeShippingDollarAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Products", "TypeID", "CLARITY.ProductTypes");
            DropForeignKey("CLARITY.Products", "TotalPurchasedAmountCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.ProductFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.ProductFiles", "MasterID", "CLARITY.Products");
            DropForeignKey("CLARITY.Products", "StatusID", "CLARITY.ProductStatus");
            DropForeignKey("CLARITY.Products", "RestockingFeeAmountCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.ProductRestrictions", "RestrictionsApplyToRegionID", "CLARITY.Regions");
            DropForeignKey("CLARITY.ProductRestrictions", "RestrictionsApplyToDistrictID", "CLARITY.Districts");
            DropForeignKey("CLARITY.ProductRestrictions", "RestrictionsApplyToCountryID", "CLARITY.Countries");
            DropForeignKey("CLARITY.ProductRestrictions", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.ProductRestrictions", "OverrideWithAccountTypeID", "CLARITY.AccountTypes");
            DropForeignKey("CLARITY.ProductPricePoints", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.ProductPricePoints", "SlaveID", "CLARITY.PricePoints");
            DropForeignKey("CLARITY.ProductPricePoints", "PriceRoundingID", "CLARITY.PriceRoundings");
            DropForeignKey("CLARITY.ProductPricePoints", "MasterID", "CLARITY.Products");
            DropForeignKey("CLARITY.ProductPricePoints", "CurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.ProductNotifications", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.ProductCategories", "SlaveID", "CLARITY.Categories");
            DropForeignKey("CLARITY.ProductCategories", "MasterID", "CLARITY.Products");
            DropForeignKey("CLARITY.ProductAssociations", "TypeID", "CLARITY.ProductAssociationTypes");
            DropForeignKey("CLARITY.ProductAssociations", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.ProductAssociations", "SlaveID", "CLARITY.Products");
            DropForeignKey("CLARITY.ProductAssociations", "MasterID", "CLARITY.Products");
            DropForeignKey("CLARITY.Products", "PalletID", "CLARITY.Packages");
            DropForeignKey("CLARITY.Products", "PackageID", "CLARITY.Packages");
            DropForeignKey("CLARITY.Products", "MasterPackID", "CLARITY.Packages");
            DropForeignKey("CLARITY.Packages", "TypeID", "CLARITY.PackageTypes");
            DropForeignKey("CLARITY.ManufacturerProducts", "SlaveID", "CLARITY.Products");
            DropForeignKey("CLARITY.ManufacturerProducts", "MasterID", "CLARITY.Manufacturers");
            DropForeignKey("CLARITY.ProductImages", "TypeID", "CLARITY.ProductImageTypes");
            DropForeignKey("CLARITY.ProductImages", "MasterID", "CLARITY.Products");
            DropForeignKey("CLARITY.DiscountProducts", "SlaveID", "CLARITY.Products");
            DropForeignKey("CLARITY.DiscountProducts", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.CartItems", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.CartItemTargets", "TypeID", "CLARITY.SalesItemTargetTypes");
            DropForeignKey("CLARITY.CartItemTargets", "SelectedRateQuoteID", "CLARITY.RateQuotes");
            DropForeignKey("CLARITY.CartItemTargets", "OriginVendorProductID", "CLARITY.VendorProducts");
            DropForeignKey("CLARITY.CartItemTargets", "OriginStoreProductID", "CLARITY.StoreProducts");
            DropForeignKey("CLARITY.CartItemTargets", "OriginProductInventoryLocationSectionID", "CLARITY.ProductInventoryLocationSections");
            DropForeignKey("CLARITY.CartItemTargets", "MasterID", "CLARITY.CartItems");
            DropForeignKey("CLARITY.CartItemTargets", "DestinationContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.CartItems", "StatusID", "CLARITY.CartItemStatus");
            DropForeignKey("CLARITY.CartItems", "SellingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.CartItems", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.CartItems", "OriginalCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.CartItems", "MasterID", "CLARITY.Carts");
            DropForeignKey("CLARITY.Carts", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Carts", "TypeID", "CLARITY.CartTypes");
            DropForeignKey("CLARITY.CartTypes", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.CartTypes", "CreatedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.CartFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.CartFiles", "MasterID", "CLARITY.Carts");
            DropForeignKey("CLARITY.Carts", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Carts", "StatusID", "CLARITY.CartStatus");
            DropForeignKey("CLARITY.Carts", "StateID", "CLARITY.CartStates");
            DropForeignKey("CLARITY.Carts", "ShippingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.Carts", "ShipmentID", "CLARITY.Shipments");
            DropForeignKey("CLARITY.Notes", "VendorID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.Notes", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Notes", "UpdatedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Notes", "TypeID", "CLARITY.NoteTypes");
            DropForeignKey("CLARITY.Notes", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Notes", "SampleRequestItemID", "CLARITY.SampleRequestItems");
            DropForeignKey("CLARITY.Notes", "SampleRequestID", "CLARITY.SampleRequests");
            DropForeignKey("CLARITY.Notes", "SalesReturnItemID", "CLARITY.SalesReturnItems");
            DropForeignKey("CLARITY.Notes", "SalesReturnID", "CLARITY.SalesReturns");
            DropForeignKey("CLARITY.Notes", "SalesQuoteItemID", "CLARITY.SalesQuoteItems");
            DropForeignKey("CLARITY.Notes", "SalesQuoteID", "CLARITY.SalesQuotes");
            DropForeignKey("CLARITY.Notes", "SalesOrderItemID", "CLARITY.SalesOrderItems");
            DropForeignKey("CLARITY.SalesOrderItems", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SalesOrderItemTargets", "TypeID", "CLARITY.SalesItemTargetTypes");
            DropForeignKey("CLARITY.SalesOrderItemTargets", "SelectedRateQuoteID", "CLARITY.RateQuotes");
            DropForeignKey("CLARITY.SalesOrderItemTargets", "OriginVendorProductID", "CLARITY.VendorProducts");
            DropForeignKey("CLARITY.SalesOrderItemTargets", "OriginStoreProductID", "CLARITY.StoreProducts");
            DropForeignKey("CLARITY.SalesOrderItemTargets", "OriginProductInventoryLocationSectionID", "CLARITY.ProductInventoryLocationSections");
            DropForeignKey("CLARITY.SalesOrderItemTargets", "MasterID", "CLARITY.SalesOrderItems");
            DropForeignKey("CLARITY.SalesOrderItemTargets", "DestinationContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesOrderItems", "StatusID", "CLARITY.SalesOrderItemStatus");
            DropForeignKey("CLARITY.SalesOrderItems", "SellingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SalesOrderItems", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.SalesOrderItems", "OriginalCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SalesOrderItems", "MasterID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.AppliedSalesOrderItemDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSalesOrderItemDiscounts", "MasterID", "CLARITY.SalesOrderItems");
            DropForeignKey("CLARITY.Notes", "SalesOrderID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.Notes", "SalesInvoiceItemID", "CLARITY.SalesInvoiceItems");
            DropForeignKey("CLARITY.SalesInvoiceItems", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SalesInvoiceItemTargets", "TypeID", "CLARITY.SalesItemTargetTypes");
            DropForeignKey("CLARITY.SalesInvoiceItemTargets", "SelectedRateQuoteID", "CLARITY.RateQuotes");
            DropForeignKey("CLARITY.SalesInvoiceItemTargets", "OriginVendorProductID", "CLARITY.VendorProducts");
            DropForeignKey("CLARITY.SalesInvoiceItemTargets", "OriginStoreProductID", "CLARITY.StoreProducts");
            DropForeignKey("CLARITY.SalesInvoiceItemTargets", "OriginProductInventoryLocationSectionID", "CLARITY.ProductInventoryLocationSections");
            DropForeignKey("CLARITY.SalesInvoiceItemTargets", "MasterID", "CLARITY.SalesInvoiceItems");
            DropForeignKey("CLARITY.SalesInvoiceItemTargets", "DestinationContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesInvoiceItems", "StatusID", "CLARITY.SalesInvoiceItemStatus");
            DropForeignKey("CLARITY.SalesInvoiceItems", "SellingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SalesInvoiceItems", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.SalesInvoiceItems", "OriginalCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SalesInvoiceItems", "MasterID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.AppliedSalesInvoiceItemDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSalesInvoiceItemDiscounts", "MasterID", "CLARITY.SalesInvoiceItems");
            DropForeignKey("CLARITY.Notes", "SalesInvoiceID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.Notes", "SalesGroupID", "CLARITY.SalesGroups");
            DropForeignKey("CLARITY.Notes", "PurchaseOrderItemID", "CLARITY.PurchaseOrderItems");
            DropForeignKey("CLARITY.PurchaseOrderItems", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.PurchaseOrderItemTargets", "TypeID", "CLARITY.SalesItemTargetTypes");
            DropForeignKey("CLARITY.PurchaseOrderItemTargets", "SelectedRateQuoteID", "CLARITY.RateQuotes");
            DropForeignKey("CLARITY.PurchaseOrderItemTargets", "OriginVendorProductID", "CLARITY.VendorProducts");
            DropForeignKey("CLARITY.PurchaseOrderItemTargets", "OriginStoreProductID", "CLARITY.StoreProducts");
            DropForeignKey("CLARITY.PurchaseOrderItemTargets", "OriginProductInventoryLocationSectionID", "CLARITY.ProductInventoryLocationSections");
            DropForeignKey("CLARITY.PurchaseOrderItemTargets", "MasterID", "CLARITY.PurchaseOrderItems");
            DropForeignKey("CLARITY.PurchaseOrderItemTargets", "DestinationContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.PurchaseOrderItems", "StatusID", "CLARITY.PurchaseOrderItemStatus");
            DropForeignKey("CLARITY.PurchaseOrderItems", "SellingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.PurchaseOrderItems", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.PurchaseOrderItems", "OriginalCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.PurchaseOrderItems", "MasterID", "CLARITY.PurchaseOrders");
            DropForeignKey("CLARITY.AppliedPurchaseOrderItemDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedPurchaseOrderItemDiscounts", "MasterID", "CLARITY.PurchaseOrderItems");
            DropForeignKey("CLARITY.Notes", "PurchaseOrderID", "CLARITY.PurchaseOrders");
            DropForeignKey("CLARITY.Notes", "ManufacturerID", "CLARITY.Manufacturers");
            DropForeignKey("CLARITY.Notes", "CreatedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Wallets", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Wallets", "CurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.UserProductTypes", "SlaveID", "CLARITY.ProductTypes");
            DropForeignKey("CLARITY.ProductTypes", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.UserProductTypes", "MasterID", "CLARITY.Users");
            DropForeignKey("CLARITY.Users", "UserOnlineStatusID", "CLARITY.UserOnlineStatus");
            DropForeignKey("CLARITY.UserEventAttendances", "TypeID", "CLARITY.UserEventAttendanceTypes");
            DropForeignKey("CLARITY.UserEventAttendances", "SlaveID", "CLARITY.Users");
            DropForeignKey("CLARITY.UserEventAttendances", "MasterID", "CLARITY.CalendarEvents");
            DropForeignKey("CLARITY.CalendarEvents", "TypeID", "CLARITY.CalendarEventTypes");
            DropForeignKey("CLARITY.CalendarEventFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.CalendarEventFiles", "MasterID", "CLARITY.CalendarEvents");
            DropForeignKey("CLARITY.CalendarEvents", "StatusID", "CLARITY.CalendarEventStatus");
            DropForeignKey("CLARITY.CalendarEventProducts", "SlaveID", "CLARITY.Products");
            DropForeignKey("CLARITY.CalendarEventProducts", "MasterID", "CLARITY.CalendarEvents");
            DropForeignKey("CLARITY.CalendarEventImages", "TypeID", "CLARITY.CalendarEventImageTypes");
            DropForeignKey("CLARITY.CalendarEventImages", "MasterID", "CLARITY.CalendarEvents");
            DropForeignKey("CLARITY.CalendarEvents", "GroupID", "CLARITY.Groups");
            DropForeignKey("CLARITY.CalendarEvents", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.CalendarEventDetails", "CalendarEventID", "CLARITY.CalendarEvents");
            DropForeignKey("CLARITY.Users", "TypeID", "CLARITY.UserTypes");
            DropForeignKey("CLARITY.StoreUserTypes", "SlaveID", "CLARITY.UserTypes");
            DropForeignKey("CLARITY.StoreUserTypes", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Subscriptions", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Subscriptions", "TypeID", "CLARITY.SubscriptionTypes");
            DropForeignKey("CLARITY.ProductSubscriptionTypes", "SlaveID", "CLARITY.SubscriptionTypes");
            DropForeignKey("CLARITY.ProductSubscriptionTypes", "MasterID", "CLARITY.Products");
            DropForeignKey("CLARITY.SubscriptionHistories", "SlaveID", "CLARITY.Payments");
            DropForeignKey("CLARITY.SubscriptionHistories", "MasterID", "CLARITY.Subscriptions");
            DropForeignKey("CLARITY.StoreSubscriptions", "SlaveID", "CLARITY.Subscriptions");
            DropForeignKey("CLARITY.StoreSubscriptions", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Subscriptions", "StatusID", "CLARITY.SubscriptionStatus");
            DropForeignKey("CLARITY.Subscriptions", "SalesInvoiceID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.Subscriptions", "RepeatTypeID", "CLARITY.RepeatTypes");
            DropForeignKey("CLARITY.Subscriptions", "ProductMembershipLevelID", "CLARITY.ProductMembershipLevels");
            DropForeignKey("CLARITY.ProductMembershipLevels", "SlaveID", "CLARITY.MembershipLevels");
            DropForeignKey("CLARITY.ProductMembershipLevels", "MembershipRepeatTypeID", "CLARITY.MembershipRepeatTypes");
            DropForeignKey("CLARITY.MembershipRepeatTypes", "SlaveID", "CLARITY.RepeatTypes");
            DropForeignKey("CLARITY.MembershipRepeatTypes", "MasterID", "CLARITY.Memberships");
            DropForeignKey("CLARITY.MembershipAdZoneAccessByLevels", "SlaveID", "CLARITY.MembershipLevels");
            DropForeignKey("CLARITY.MembershipAdZoneAccessByLevels", "MasterID", "CLARITY.MembershipAdZoneAccesses");
            DropForeignKey("CLARITY.MembershipAdZoneAccesses", "SlaveID", "CLARITY.AdZoneAccesses");
            DropForeignKey("CLARITY.AdZoneAccesses", "ZoneID", "CLARITY.Zones");
            DropForeignKey("CLARITY.AdZoneAccesses", "SubscriptionID", "CLARITY.Subscriptions");
            DropForeignKey("CLARITY.AdZoneAccesses", "ImpressionCounterID", "CLARITY.Counters");
            DropForeignKey("CLARITY.AdZoneAccesses", "ClickCounterID", "CLARITY.Counters");
            DropForeignKey("CLARITY.AdZones", "SlaveID", "CLARITY.Zones");
            DropForeignKey("CLARITY.Zones", "TypeID", "CLARITY.ZoneTypes");
            DropForeignKey("CLARITY.Zones", "StatusID", "CLARITY.ZoneStatus");
            DropForeignKey("CLARITY.AdZones", "MasterID", "CLARITY.Ads");
            DropForeignKey("CLARITY.Ads", "TypeID", "CLARITY.AdTypes");
            DropForeignKey("CLARITY.AdStores", "SlaveID", "CLARITY.Stores");
            DropForeignKey("CLARITY.AdStores", "MasterID", "CLARITY.Ads");
            DropForeignKey("CLARITY.Ads", "StatusID", "CLARITY.AdStatus");
            DropForeignKey("CLARITY.Ads", "ImpressionCounterID", "CLARITY.Counters");
            DropForeignKey("CLARITY.AdImages", "TypeID", "CLARITY.AdImageTypes");
            DropForeignKey("CLARITY.AdImages", "MasterID", "CLARITY.Ads");
            DropForeignKey("CLARITY.Ads", "ClickCounterID", "CLARITY.Counters");
            DropForeignKey("CLARITY.CampaignAds", "SlaveID", "CLARITY.Ads");
            DropForeignKey("CLARITY.CampaignAds", "MasterID", "CLARITY.Campaigns");
            DropForeignKey("CLARITY.Campaigns", "TypeID", "CLARITY.CampaignTypes");
            DropForeignKey("CLARITY.CampaignTypes", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Campaigns", "StatusID", "CLARITY.CampaignStatus");
            DropForeignKey("CLARITY.Campaigns", "CreatedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.AdAccounts", "SlaveID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AdAccounts", "MasterID", "CLARITY.Ads");
            DropForeignKey("CLARITY.AdZones", "ImpressionCounterID", "CLARITY.Counters");
            DropForeignKey("CLARITY.AdZones", "ClickCounterID", "CLARITY.Counters");
            DropForeignKey("CLARITY.Counters", "TypeID", "CLARITY.CounterTypes");
            DropForeignKey("CLARITY.CounterLogs", "TypeID", "CLARITY.CounterLogTypes");
            DropForeignKey("CLARITY.CounterLogs", "CounterID", "CLARITY.Counters");
            DropForeignKey("CLARITY.AdZones", "AdZoneAccessID", "CLARITY.AdZoneAccesses");
            DropForeignKey("CLARITY.MembershipAdZoneAccesses", "MasterID", "CLARITY.Memberships");
            DropForeignKey("CLARITY.MembershipLevels", "MembershipID", "CLARITY.Memberships");
            DropForeignKey("CLARITY.ProductMembershipLevels", "MasterID", "CLARITY.Products");
            DropForeignKey("CLARITY.Subscriptions", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.StoreUsers", "SlaveID", "CLARITY.Users");
            DropForeignKey("CLARITY.StoreUsers", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.UserFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.UserFiles", "MasterID", "CLARITY.Users");
            DropForeignKey("CLARITY.Users", "StatusID", "CLARITY.UserStatus");
            DropForeignKey("CLARITY.SalesQuotes", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SalesQuotes", "TypeID", "CLARITY.SalesQuoteTypes");
            DropForeignKey("CLARITY.SalesQuoteFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.SalesQuoteFiles", "MasterID", "CLARITY.SalesQuotes");
            DropForeignKey("CLARITY.SalesQuotes", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.SalesQuotes", "StatusID", "CLARITY.SalesQuoteStatus");
            DropForeignKey("CLARITY.SalesQuotes", "StateID", "CLARITY.SalesQuoteStates");
            DropForeignKey("CLARITY.SalesQuotes", "ShippingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesQuoteCategories", "SlaveID", "CLARITY.Categories");
            DropForeignKey("CLARITY.SalesQuoteCategories", "MasterID", "CLARITY.SalesQuotes");
            DropForeignKey("CLARITY.SalesQuoteItems", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SalesQuoteItemTargets", "TypeID", "CLARITY.SalesItemTargetTypes");
            DropForeignKey("CLARITY.SalesQuoteItemTargets", "SelectedRateQuoteID", "CLARITY.RateQuotes");
            DropForeignKey("CLARITY.SalesQuoteItemTargets", "OriginVendorProductID", "CLARITY.VendorProducts");
            DropForeignKey("CLARITY.SalesQuoteItemTargets", "OriginStoreProductID", "CLARITY.StoreProducts");
            DropForeignKey("CLARITY.SalesQuoteItemTargets", "OriginProductInventoryLocationSectionID", "CLARITY.ProductInventoryLocationSections");
            DropForeignKey("CLARITY.SalesQuoteItemTargets", "MasterID", "CLARITY.SalesQuoteItems");
            DropForeignKey("CLARITY.SalesQuoteItemTargets", "DestinationContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesQuoteItems", "StatusID", "CLARITY.SalesQuoteItemStatus");
            DropForeignKey("CLARITY.SalesQuoteItems", "SellingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SalesQuoteItems", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.SalesQuoteItems", "OriginalCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SalesQuoteItems", "MasterID", "CLARITY.SalesQuotes");
            DropForeignKey("CLARITY.AppliedSalesQuoteItemDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSalesQuoteItemDiscounts", "MasterID", "CLARITY.SalesQuoteItems");
            DropForeignKey("CLARITY.SalesQuotes", "ResponseAsVendorID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.SalesQuotes", "ResponseAsStoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.RateQuotes", "ShipCarrierMethodID", "CLARITY.ShipCarrierMethods");
            DropForeignKey("CLARITY.RateQuotes", "SampleRequestID", "CLARITY.SampleRequests");
            DropForeignKey("CLARITY.SampleRequests", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SampleRequests", "TypeID", "CLARITY.SampleRequestTypes");
            DropForeignKey("CLARITY.SampleRequestFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.SampleRequestFiles", "MasterID", "CLARITY.SampleRequests");
            DropForeignKey("CLARITY.SampleRequests", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.SampleRequests", "StatusID", "CLARITY.SampleRequestStatus");
            DropForeignKey("CLARITY.SampleRequests", "StateID", "CLARITY.SampleRequestStates");
            DropForeignKey("CLARITY.SampleRequests", "ShippingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SampleRequestItems", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SampleRequestItemTargets", "TypeID", "CLARITY.SalesItemTargetTypes");
            DropForeignKey("CLARITY.SampleRequestItemTargets", "SelectedRateQuoteID", "CLARITY.RateQuotes");
            DropForeignKey("CLARITY.SampleRequestItemTargets", "OriginVendorProductID", "CLARITY.VendorProducts");
            DropForeignKey("CLARITY.SampleRequestItemTargets", "OriginStoreProductID", "CLARITY.StoreProducts");
            DropForeignKey("CLARITY.SampleRequestItemTargets", "OriginProductInventoryLocationSectionID", "CLARITY.ProductInventoryLocationSections");
            DropForeignKey("CLARITY.SampleRequestItemTargets", "MasterID", "CLARITY.SampleRequestItems");
            DropForeignKey("CLARITY.SampleRequestItemTargets", "DestinationContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SampleRequestItems", "StatusID", "CLARITY.SampleRequestItemStatus");
            DropForeignKey("CLARITY.SampleRequestItems", "SellingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SampleRequestItems", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.SampleRequestItems", "OriginalCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SampleRequestItems", "MasterID", "CLARITY.SampleRequests");
            DropForeignKey("CLARITY.AppliedSampleRequestItemDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSampleRequestItemDiscounts", "MasterID", "CLARITY.SampleRequestItems");
            DropForeignKey("CLARITY.AppliedSampleRequestDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSampleRequestDiscounts", "MasterID", "CLARITY.SampleRequests");
            DropForeignKey("CLARITY.SampleRequestContacts", "SlaveID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SampleRequestContacts", "MasterID", "CLARITY.SampleRequests");
            DropForeignKey("CLARITY.SampleRequests", "ParentID", "CLARITY.SampleRequests");
            DropForeignKey("CLARITY.SampleRequests", "BillingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SampleRequests", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.RateQuotes", "SalesReturnID", "CLARITY.SalesReturns");
            DropForeignKey("CLARITY.SalesReturns", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SalesReturns", "TypeID", "CLARITY.SalesReturnTypes");
            DropForeignKey("CLARITY.SalesReturnFiles", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.SalesReturnFiles", "MasterID", "CLARITY.SalesReturns");
            DropForeignKey("CLARITY.SalesReturns", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.SalesReturns", "StatusID", "CLARITY.SalesReturnStatus");
            DropForeignKey("CLARITY.SalesReturns", "StateID", "CLARITY.SalesReturnStates");
            DropForeignKey("CLARITY.SalesReturns", "ShippingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesReturnPayments", "SlaveID", "CLARITY.Payments");
            DropForeignKey("CLARITY.Payments", "TypeID", "CLARITY.PaymentTypes");
            DropForeignKey("CLARITY.Payments", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Payments", "StatusID", "CLARITY.PaymentStatus");
            DropForeignKey("CLARITY.Payments", "PaymentMethodID", "CLARITY.PaymentMethods");
            DropForeignKey("CLARITY.Payments", "CurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.Payments", "BillingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesReturnPayments", "MasterID", "CLARITY.SalesReturns");
            DropForeignKey("CLARITY.SalesReturnItems", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.SalesReturnItemTargets", "TypeID", "CLARITY.SalesItemTargetTypes");
            DropForeignKey("CLARITY.SalesReturnItemTargets", "SelectedRateQuoteID", "CLARITY.RateQuotes");
            DropForeignKey("CLARITY.SalesReturnItemTargets", "OriginVendorProductID", "CLARITY.VendorProducts");
            DropForeignKey("CLARITY.SalesReturnItemTargets", "OriginStoreProductID", "CLARITY.StoreProducts");
            DropForeignKey("CLARITY.StoreProducts", "SlaveID", "CLARITY.Products");
            DropForeignKey("CLARITY.StoreProducts", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.SalesReturnItemTargets", "OriginProductInventoryLocationSectionID", "CLARITY.ProductInventoryLocationSections");
            DropForeignKey("CLARITY.SalesReturnItemTargets", "MasterID", "CLARITY.SalesReturnItems");
            DropForeignKey("CLARITY.SalesReturnItemTargets", "DestinationContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesReturnItems", "StatusID", "CLARITY.SalesReturnItemStatus");
            DropForeignKey("CLARITY.SalesReturnItems", "SellingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SalesReturnItems", "SalesReturnReasonID", "CLARITY.SalesReturnReasons");
            DropForeignKey("CLARITY.SalesReturnReasons", "RestockingFeeAmountCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SalesReturnItems", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.SalesReturnItems", "OriginalCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.SalesReturnItems", "MasterID", "CLARITY.SalesReturns");
            DropForeignKey("CLARITY.AppliedSalesReturnItemDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSalesReturnItemDiscounts", "MasterID", "CLARITY.SalesReturnItems");
            DropForeignKey("CLARITY.SalesReturns", "SalesGroupID", "CLARITY.SalesGroups");
            DropForeignKey("CLARITY.SalesOrders", "SalesGroupAsSubID", "CLARITY.SalesGroups");
            DropForeignKey("CLARITY.SalesQuotes", "SalesGroupAsResponseID", "CLARITY.SalesGroups");
            DropForeignKey("CLARITY.SalesQuotes", "SalesGroupAsMasterID", "CLARITY.SalesGroups");
            DropForeignKey("CLARITY.SalesOrders", "SalesGroupAsMasterID", "CLARITY.SalesGroups");
            DropForeignKey("CLARITY.SalesGroups", "BillingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesGroups", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AppliedSalesReturnDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSalesReturnDiscounts", "MasterID", "CLARITY.SalesReturns");
            DropForeignKey("CLARITY.SalesReturnContacts", "SlaveID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesReturnContacts", "MasterID", "CLARITY.SalesReturns");
            DropForeignKey("CLARITY.SalesReturns", "ParentID", "CLARITY.SalesReturns");
            DropForeignKey("CLARITY.SalesReturns", "BillingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesReturnSalesOrders", "SlaveID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.SalesReturnSalesOrders", "MasterID", "CLARITY.SalesReturns");
            DropForeignKey("CLARITY.SalesReturns", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.RateQuotes", "SalesQuoteID", "CLARITY.SalesQuotes");
            DropForeignKey("CLARITY.RateQuotes", "SalesOrderID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.RateQuotes", "SalesInvoiceID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.RateQuotes", "PurchaseOrderID", "CLARITY.PurchaseOrders");
            DropForeignKey("CLARITY.RateQuotes", "CartID", "CLARITY.Carts");
            DropForeignKey("CLARITY.AppliedSalesQuoteDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSalesQuoteDiscounts", "MasterID", "CLARITY.SalesQuotes");
            DropForeignKey("CLARITY.SalesQuoteContacts", "SlaveID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesQuoteContacts", "MasterID", "CLARITY.SalesQuotes");
            DropForeignKey("CLARITY.SalesQuotes", "ParentID", "CLARITY.SalesQuotes");
            DropForeignKey("CLARITY.SalesQuotes", "BillingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesQuoteSalesOrders", "SlaveID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.SalesQuoteSalesOrders", "MasterID", "CLARITY.SalesQuotes");
            DropForeignKey("CLARITY.SalesQuotes", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.RoleUsers", "UserId", "CLARITY.Users");
            DropForeignKey("CLARITY.RoleUsers", "RoleId", "CLARITY.UserRoles");
            DropForeignKey("CLARITY.RolePermissions", "RoleId", "CLARITY.UserRoles");
            DropForeignKey("CLARITY.RolePermissions", "PermissionId", "CLARITY.Permissions");
            DropForeignKey("CLARITY.RoleUsers", "GroupID", "CLARITY.Groups");
            DropForeignKey("CLARITY.ReferralCodes", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.ReferralCodes", "TypeID", "CLARITY.ReferralCodeTypes");
            DropForeignKey("CLARITY.ReferralCodes", "StatusID", "CLARITY.ReferralCodeStatus");
            DropForeignKey("CLARITY.Users", "PreferredStoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.UserLogins", "UserId", "CLARITY.Users");
            DropForeignKey("CLARITY.Users", "LanguageID", "CLARITY.Languages");
            DropForeignKey("CLARITY.UserImages", "TypeID", "CLARITY.UserImageTypes");
            DropForeignKey("CLARITY.UserImages", "MasterID", "CLARITY.Users");
            DropForeignKey("CLARITY.FavoriteVendors", "SlaveID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.Vendors", "TypeID", "CLARITY.VendorTypes");
            DropForeignKey("CLARITY.StoreVendors", "SlaveID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.StoreVendors", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Shipments", "VendorID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.Shipments", "TypeID", "CLARITY.ShipmentTypes");
            DropForeignKey("CLARITY.Shipments", "StatusID", "CLARITY.ShipmentStatus");
            DropForeignKey("CLARITY.ShipmentEvents", "ShipmentID", "CLARITY.Shipments");
            DropForeignKey("CLARITY.ShipmentEvents", "AddressID", "CLARITY.Addresses");
            DropForeignKey("CLARITY.Shipments", "ShipCarrierMethodID", "CLARITY.ShipCarrierMethods");
            DropForeignKey("CLARITY.Shipments", "ShipCarrierID", "CLARITY.ShipCarriers");
            DropForeignKey("CLARITY.ShipCarrierMethods", "ShipCarrierID", "CLARITY.ShipCarriers");
            DropForeignKey("CLARITY.ShipCarriers", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.CarrierOrigins", "ShipCarrierID", "CLARITY.ShipCarriers");
            DropForeignKey("CLARITY.CarrierInvoices", "ShipCarrierID", "CLARITY.ShipCarriers");
            DropForeignKey("CLARITY.Shipments", "OriginContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.Shipments", "InventoryLocationSectionID", "CLARITY.InventoryLocationSections");
            DropForeignKey("CLARITY.ProductInventoryLocationSections", "SlaveID", "CLARITY.InventoryLocationSections");
            DropForeignKey("CLARITY.ProductInventoryLocationSections", "MasterID", "CLARITY.Products");
            DropForeignKey("CLARITY.InventoryLocationSections", "InventoryLocationID", "CLARITY.InventoryLocations");
            DropForeignKey("CLARITY.StoreInventoryLocations", "TypeID", "CLARITY.StoreInventoryLocationTypes");
            DropForeignKey("CLARITY.StoreInventoryLocations", "SlaveID", "CLARITY.InventoryLocations");
            DropForeignKey("CLARITY.StoreInventoryLocations", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.InventoryLocations", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.Shipments", "DestinationContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.Reviews", "VendorID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.Reviews", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Reviews", "TypeID", "CLARITY.ReviewTypes");
            DropForeignKey("CLARITY.Reviews", "SubmittedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Reviews", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Reviews", "ProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Reviews", "ManufacturerID", "CLARITY.Manufacturers");
            DropForeignKey("CLARITY.Reviews", "CategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Reviews", "ApprovedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.VendorProducts", "SlaveID", "CLARITY.Products");
            DropForeignKey("CLARITY.VendorProducts", "MasterID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.Vendors", "MinimumOrderQuantityAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Vendors", "MinimumOrderQuantityAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Vendors", "MinimumOrderDollarAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Vendors", "MinimumOrderDollarAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Vendors", "MinimumForFreeShippingQuantityAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Vendors", "MinimumForFreeShippingQuantityAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.Vendors", "MinimumForFreeShippingDollarAmountBufferProductID", "CLARITY.Products");
            DropForeignKey("CLARITY.Vendors", "MinimumForFreeShippingDollarAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.VendorManufacturers", "SlaveID", "CLARITY.Manufacturers");
            DropForeignKey("CLARITY.VendorManufacturers", "MasterID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.VendorImages", "TypeID", "CLARITY.VendorImageTypes");
            DropForeignKey("CLARITY.VendorImages", "MasterID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.Vendors", "ContactMethodID", "CLARITY.ContactMethods");
            DropForeignKey("CLARITY.Vendors", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.VendorAccounts", "SlaveID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.VendorAccounts", "MasterID", "CLARITY.Vendors");
            DropForeignKey("CLARITY.FavoriteVendors", "MasterID", "CLARITY.Users");
            DropForeignKey("CLARITY.FavoriteStores", "SlaveID", "CLARITY.Stores");
            DropForeignKey("CLARITY.FavoriteStores", "MasterID", "CLARITY.Users");
            DropForeignKey("CLARITY.FavoriteManufacturers", "SlaveID", "CLARITY.Manufacturers");
            DropForeignKey("CLARITY.FavoriteManufacturers", "MasterID", "CLARITY.Users");
            DropForeignKey("CLARITY.FavoriteCategories", "SlaveID", "CLARITY.Categories");
            DropForeignKey("CLARITY.FavoriteCategories", "MasterID", "CLARITY.Users");
            DropForeignKey("CLARITY.DiscountCodes", "UserID", "CLARITY.Users");
            DropForeignKey("CLARITY.DiscountCodes", "DiscountID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.Users", "CurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.ConversationUsers", "SlaveID", "CLARITY.Users");
            DropForeignKey("CLARITY.ConversationUsers", "MasterID", "CLARITY.Conversations");
            DropForeignKey("CLARITY.Conversations", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Messages", "StoreID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Messages", "SentByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.MessageRecipients", "SlaveID", "CLARITY.Users");
            DropForeignKey("CLARITY.MessageRecipients", "MasterID", "CLARITY.Messages");
            DropForeignKey("CLARITY.MessageRecipients", "GroupID", "CLARITY.Groups");
            DropForeignKey("CLARITY.GroupUsers", "SlaveID", "CLARITY.Users");
            DropForeignKey("CLARITY.GroupUsers", "MasterID", "CLARITY.Groups");
            DropForeignKey("CLARITY.Groups", "TypeID", "CLARITY.GroupTypes");
            DropForeignKey("CLARITY.Groups", "StatusID", "CLARITY.GroupStatus");
            DropForeignKey("CLARITY.Groups", "ParentID", "CLARITY.Groups");
            DropForeignKey("CLARITY.Groups", "GroupOwnerID", "CLARITY.Users");
            DropForeignKey("CLARITY.EmailQueues", "TypeID", "CLARITY.EmailTypes");
            DropForeignKey("CLARITY.EmailQueues", "StatusID", "CLARITY.EmailStatus");
            DropForeignKey("CLARITY.EmailQueues", "MessageRecipientID", "CLARITY.MessageRecipients");
            DropForeignKey("CLARITY.EmailQueues", "EmailTemplateID", "CLARITY.EmailTemplates");
            DropForeignKey("CLARITY.EmailQueueAttachments", "UpdatedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.EmailQueueAttachments", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.EmailQueueAttachments", "MasterID", "CLARITY.EmailQueues");
            DropForeignKey("CLARITY.EmailQueueAttachments", "CreatedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.MessageAttachments", "UpdatedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.MessageAttachments", "SlaveID", "CLARITY.StoredFiles");
            DropForeignKey("CLARITY.MessageAttachments", "MasterID", "CLARITY.Messages");
            DropForeignKey("CLARITY.MessageAttachments", "CreatedByUserID", "CLARITY.Users");
            DropForeignKey("CLARITY.Messages", "ConversationID", "CLARITY.Conversations");
            DropForeignKey("CLARITY.Users", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.UserClaims", "UserId", "CLARITY.Users");
            DropForeignKey("CLARITY.Users", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.Notes", "CartItemID", "CLARITY.CartItems");
            DropForeignKey("CLARITY.Notes", "CartID", "CLARITY.Carts");
            DropForeignKey("CLARITY.Notes", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AppliedCartDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedCartDiscounts", "MasterID", "CLARITY.Carts");
            DropForeignKey("CLARITY.CartContacts", "SlaveID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.CartContacts", "MasterID", "CLARITY.Carts");
            DropForeignKey("CLARITY.Carts", "ParentID", "CLARITY.Carts");
            DropForeignKey("CLARITY.Carts", "BillingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.Carts", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.AppliedCartItemDiscounts", "SlaveID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedCartItemDiscounts", "MasterID", "CLARITY.CartItems");
            DropForeignKey("CLARITY.AccountProducts", "TypeID", "CLARITY.AccountProductTypes");
            DropForeignKey("CLARITY.AccountProducts", "SlaveID", "CLARITY.Products");
            DropForeignKey("CLARITY.AccountProducts", "MasterID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.Categories", "MinimumForFreeShippingDollarAmountBufferCategoryID", "CLARITY.Categories");
            DropForeignKey("CLARITY.CategoryImages", "TypeID", "CLARITY.CategoryImageTypes");
            DropForeignKey("CLARITY.CategoryImages", "MasterID", "CLARITY.Categories");
            DropForeignKey("CLARITY.ManufacturerImages", "TypeID", "CLARITY.ManufacturerImageTypes");
            DropForeignKey("CLARITY.ManufacturerImages", "MasterID", "CLARITY.Manufacturers");
            DropForeignKey("CLARITY.Manufacturers", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.StoreManufacturers", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Stores", "LanguageID", "CLARITY.Languages");
            DropForeignKey("CLARITY.StoreImages", "TypeID", "CLARITY.StoreImageTypes");
            DropForeignKey("CLARITY.StoreImages", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.Stores", "ContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.BrandStores", "SlaveID", "CLARITY.Stores");
            DropForeignKey("CLARITY.BrandStores", "MasterID", "CLARITY.Brands");
            DropForeignKey("CLARITY.BrandImages", "TypeID", "CLARITY.BrandImageTypes");
            DropForeignKey("CLARITY.BrandImages", "MasterID", "CLARITY.Brands");
            DropForeignKey("CLARITY.BrandSiteDomains", "SlaveID", "CLARITY.SiteDomains");
            DropForeignKey("CLARITY.StoreSiteDomains", "SlaveID", "CLARITY.SiteDomains");
            DropForeignKey("CLARITY.StoreSiteDomains", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.SiteDomainSocialProviders", "SlaveID", "CLARITY.SocialProviders");
            DropForeignKey("CLARITY.SiteDomainSocialProviders", "MasterID", "CLARITY.SiteDomains");
            DropForeignKey("CLARITY.BrandSiteDomains", "MasterID", "CLARITY.Brands");
            DropForeignKey("CLARITY.StoreAccounts", "SlaveID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.StoreAccounts", "PricePointID", "CLARITY.PricePoints");
            DropForeignKey("CLARITY.StoreAccounts", "MasterID", "CLARITY.Stores");
            DropForeignKey("CLARITY.DiscountAccountTypes", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.DiscountAccounts", "SlaveID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.DiscountAccounts", "MasterID", "CLARITY.Discounts");
            DropForeignKey("CLARITY.AppliedSalesInvoiceDiscounts", "MasterID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.SalesInvoiceContacts", "SlaveID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesInvoiceContacts", "MasterID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.SalesInvoices", "ParentID", "CLARITY.SalesInvoices");
            DropForeignKey("CLARITY.SalesInvoices", "BillingContactID", "CLARITY.Contacts");
            DropForeignKey("CLARITY.SalesInvoices", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.SalesOrderSalesInvoices", "MasterID", "CLARITY.SalesOrders");
            DropForeignKey("CLARITY.SalesOrders", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.PurchaseOrders", "AccountID", "CLARITY.Accounts");
            DropForeignKey("CLARITY.Contacts", "AddressID", "CLARITY.Addresses");
            DropForeignKey("CLARITY.Addresses", "RegionID", "CLARITY.Regions");
            DropForeignKey("CLARITY.Addresses", "DistrictID", "CLARITY.Districts");
            DropForeignKey("CLARITY.Addresses", "CountryID", "CLARITY.Countries");
            DropForeignKey("CLARITY.TaxCountries", "CountryID", "CLARITY.Countries");
            DropForeignKey("CLARITY.TaxRegions", "RegionID", "CLARITY.Regions");
            DropForeignKey("CLARITY.RegionLanguages", "SlaveID", "CLARITY.Languages");
            DropForeignKey("CLARITY.RegionLanguages", "MasterID", "CLARITY.Regions");
            DropForeignKey("CLARITY.InterRegions", "RelationRegionID", "CLARITY.Regions");
            DropForeignKey("CLARITY.InterRegions", "KeyRegionID", "CLARITY.Regions");
            DropForeignKey("CLARITY.RegionImages", "TypeID", "CLARITY.RegionImageTypes");
            DropForeignKey("CLARITY.RegionImages", "MasterID", "CLARITY.Regions");
            DropForeignKey("CLARITY.TaxDistricts", "DistrictID", "CLARITY.Districts");
            DropForeignKey("CLARITY.Districts", "RegionID", "CLARITY.Regions");
            DropForeignKey("CLARITY.DistrictLanguages", "SlaveID", "CLARITY.Languages");
            DropForeignKey("CLARITY.DistrictLanguages", "MasterID", "CLARITY.Districts");
            DropForeignKey("CLARITY.DistrictImages", "TypeID", "CLARITY.DistrictImageTypes");
            DropForeignKey("CLARITY.DistrictImages", "MasterID", "CLARITY.Districts");
            DropForeignKey("CLARITY.DistrictCurrencies", "SlaveID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.DistrictCurrencies", "MasterID", "CLARITY.Districts");
            DropForeignKey("CLARITY.RegionCurrencies", "SlaveID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.RegionCurrencies", "MasterID", "CLARITY.Regions");
            DropForeignKey("CLARITY.Regions", "CountryID", "CLARITY.Countries");
            DropForeignKey("CLARITY.CountryLanguages", "SlaveID", "CLARITY.Languages");
            DropForeignKey("CLARITY.LanguageImages", "TypeID", "CLARITY.LanguageImageTypes");
            DropForeignKey("CLARITY.LanguageImages", "MasterID", "CLARITY.Languages");
            DropForeignKey("CLARITY.CountryLanguages", "MasterID", "CLARITY.Countries");
            DropForeignKey("CLARITY.CountryImages", "TypeID", "CLARITY.CountryImageTypes");
            DropForeignKey("CLARITY.CountryImages", "MasterID", "CLARITY.Countries");
            DropForeignKey("CLARITY.CountryCurrencies", "SlaveID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.CurrencyImages", "TypeID", "CLARITY.CurrencyImageTypes");
            DropForeignKey("CLARITY.CurrencyImages", "MasterID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.HistoricalCurrencyRates", "StartingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.HistoricalCurrencyRates", "EndingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.CurrencyConversions", "StartingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.CurrencyConversions", "EndingCurrencyID", "CLARITY.Currencies");
            DropForeignKey("CLARITY.CountryCurrencies", "MasterID", "CLARITY.Countries");
            DropForeignKey("CLARITY.AccountContacts", "MasterID", "CLARITY.Accounts");
            DropIndex("CLARITY.ZipCodes", new[] { "Hash" });
            DropIndex("CLARITY.ZipCodes", new[] { "Active" });
            DropIndex("CLARITY.ZipCodes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ZipCodes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ZipCodes", new[] { "CustomKey" });
            DropIndex("CLARITY.ZipCodes", new[] { "ZipCode" });
            DropIndex("CLARITY.ZipCodes", new[] { "ID" });
            DropIndex("CLARITY.UiTranslations", new[] { "Hash" });
            DropIndex("CLARITY.UiTranslations", new[] { "Active" });
            DropIndex("CLARITY.UiTranslations", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UiTranslations", new[] { "CreatedDate" });
            DropIndex("CLARITY.UiTranslations", new[] { "CustomKey" });
            DropIndex("CLARITY.UiTranslations", new[] { "UiKeyID" });
            DropIndex("CLARITY.UiTranslations", new[] { "ID" });
            DropIndex("CLARITY.UiKeys", new[] { "Hash" });
            DropIndex("CLARITY.UiKeys", new[] { "Active" });
            DropIndex("CLARITY.UiKeys", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UiKeys", new[] { "CreatedDate" });
            DropIndex("CLARITY.UiKeys", new[] { "CustomKey" });
            DropIndex("CLARITY.UiKeys", new[] { "ID" });
            DropIndex("CLARITY.SettingTypes", new[] { "Hash" });
            DropIndex("CLARITY.SettingTypes", new[] { "Active" });
            DropIndex("CLARITY.SettingTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SettingTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.SettingTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.SettingTypes", new[] { "Name" });
            DropIndex("CLARITY.SettingTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.SettingTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.SettingTypes", new[] { "ID" });
            DropIndex("CLARITY.SettingGroups", new[] { "Hash" });
            DropIndex("CLARITY.SettingGroups", new[] { "Active" });
            DropIndex("CLARITY.SettingGroups", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SettingGroups", new[] { "CreatedDate" });
            DropIndex("CLARITY.SettingGroups", new[] { "CustomKey" });
            DropIndex("CLARITY.SettingGroups", new[] { "Name" });
            DropIndex("CLARITY.SettingGroups", new[] { "SortOrder" });
            DropIndex("CLARITY.SettingGroups", new[] { "DisplayName" });
            DropIndex("CLARITY.SettingGroups", new[] { "ID" });
            DropIndex("CLARITY.Settings", new[] { "Hash" });
            DropIndex("CLARITY.Settings", new[] { "Active" });
            DropIndex("CLARITY.Settings", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Settings", new[] { "CreatedDate" });
            DropIndex("CLARITY.Settings", new[] { "CustomKey" });
            DropIndex("CLARITY.Settings", new[] { "SettingGroupID" });
            DropIndex("CLARITY.Settings", new[] { "StoreID" });
            DropIndex("CLARITY.Settings", new[] { "TypeID" });
            DropIndex("CLARITY.Settings", new[] { "ID" });
            DropIndex("CLARITY.ScheduledJobConfigurationSettings", new[] { "Hash" });
            DropIndex("CLARITY.ScheduledJobConfigurationSettings", new[] { "Active" });
            DropIndex("CLARITY.ScheduledJobConfigurationSettings", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ScheduledJobConfigurationSettings", new[] { "CreatedDate" });
            DropIndex("CLARITY.ScheduledJobConfigurationSettings", new[] { "CustomKey" });
            DropIndex("CLARITY.ScheduledJobConfigurationSettings", new[] { "SlaveID" });
            DropIndex("CLARITY.ScheduledJobConfigurationSettings", new[] { "MasterID" });
            DropIndex("CLARITY.ScheduledJobConfigurationSettings", new[] { "ID" });
            DropIndex("CLARITY.ScheduledJobConfigurations", new[] { "Hash" });
            DropIndex("CLARITY.ScheduledJobConfigurations", new[] { "Active" });
            DropIndex("CLARITY.ScheduledJobConfigurations", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ScheduledJobConfigurations", new[] { "CreatedDate" });
            DropIndex("CLARITY.ScheduledJobConfigurations", new[] { "CustomKey" });
            DropIndex("CLARITY.ScheduledJobConfigurations", new[] { "Name" });
            DropIndex("CLARITY.ScheduledJobConfigurations", new[] { "NotificationTemplateID" });
            DropIndex("CLARITY.ScheduledJobConfigurations", new[] { "ID" });
            DropIndex("CLARITY.ReportTypes", new[] { "Hash" });
            DropIndex("CLARITY.ReportTypes", new[] { "Active" });
            DropIndex("CLARITY.ReportTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ReportTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ReportTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ReportTypes", new[] { "Name" });
            DropIndex("CLARITY.ReportTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ReportTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ReportTypes", new[] { "ID" });
            DropIndex("CLARITY.Reports", new[] { "Hash" });
            DropIndex("CLARITY.Reports", new[] { "Active" });
            DropIndex("CLARITY.Reports", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Reports", new[] { "CreatedDate" });
            DropIndex("CLARITY.Reports", new[] { "CustomKey" });
            DropIndex("CLARITY.Reports", new[] { "Name" });
            DropIndex("CLARITY.Reports", new[] { "RunByUserID" });
            DropIndex("CLARITY.Reports", new[] { "TypeID" });
            DropIndex("CLARITY.Reports", new[] { "ID" });
            DropIndex("CLARITY.ProfanityFilters", new[] { "Hash" });
            DropIndex("CLARITY.ProfanityFilters", new[] { "Active" });
            DropIndex("CLARITY.ProfanityFilters", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProfanityFilters", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProfanityFilters", new[] { "CustomKey" });
            DropIndex("CLARITY.ProfanityFilters", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleVendors", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleVendors", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleVendors", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleVendors", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleVendors", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleVendors", new[] { "SlaveID" });
            DropIndex("CLARITY.PriceRuleVendors", new[] { "MasterID" });
            DropIndex("CLARITY.PriceRuleVendors", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleStores", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleStores", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleStores", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleStores", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleStores", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleStores", new[] { "SlaveID" });
            DropIndex("CLARITY.PriceRuleStores", new[] { "MasterID" });
            DropIndex("CLARITY.PriceRuleStores", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleProducts", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleProducts", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleProducts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleProducts", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleProducts", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleProducts", new[] { "SlaveID" });
            DropIndex("CLARITY.PriceRuleProducts", new[] { "MasterID" });
            DropIndex("CLARITY.PriceRuleProducts", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleUserRoles", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleUserRoles", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleUserRoles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleUserRoles", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleUserRoles", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleUserRoles", new[] { "PriceRuleID" });
            DropIndex("CLARITY.PriceRuleUserRoles", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleProductTypes", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleProductTypes", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleProductTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleProductTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleProductTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleProductTypes", new[] { "SlaveID" });
            DropIndex("CLARITY.PriceRuleProductTypes", new[] { "MasterID" });
            DropIndex("CLARITY.PriceRuleProductTypes", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleCountries", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleCountries", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleCountries", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleCountries", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleCountries", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleCountries", new[] { "SlaveID" });
            DropIndex("CLARITY.PriceRuleCountries", new[] { "MasterID" });
            DropIndex("CLARITY.PriceRuleCountries", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleCategories", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleCategories", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleCategories", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleCategories", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleCategories", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleCategories", new[] { "SlaveID" });
            DropIndex("CLARITY.PriceRuleCategories", new[] { "MasterID" });
            DropIndex("CLARITY.PriceRuleCategories", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleAccountTypes", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleAccountTypes", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleAccountTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleAccountTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleAccountTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleAccountTypes", new[] { "SlaveID" });
            DropIndex("CLARITY.PriceRuleAccountTypes", new[] { "MasterID" });
            DropIndex("CLARITY.PriceRuleAccountTypes", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleManufacturers", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleManufacturers", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleManufacturers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleManufacturers", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleManufacturers", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleManufacturers", new[] { "SlaveID" });
            DropIndex("CLARITY.PriceRuleManufacturers", new[] { "MasterID" });
            DropIndex("CLARITY.PriceRuleManufacturers", new[] { "ID" });
            DropIndex("CLARITY.PriceRules", new[] { "Hash" });
            DropIndex("CLARITY.PriceRules", new[] { "Active" });
            DropIndex("CLARITY.PriceRules", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRules", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRules", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRules", new[] { "Name" });
            DropIndex("CLARITY.PriceRules", new[] { "CurrencyID" });
            DropIndex("CLARITY.PriceRules", new[] { "ID" });
            DropIndex("CLARITY.PriceRuleAccounts", new[] { "Hash" });
            DropIndex("CLARITY.PriceRuleAccounts", new[] { "Active" });
            DropIndex("CLARITY.PriceRuleAccounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRuleAccounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRuleAccounts", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRuleAccounts", new[] { "SlaveID" });
            DropIndex("CLARITY.PriceRuleAccounts", new[] { "MasterID" });
            DropIndex("CLARITY.PriceRuleAccounts", new[] { "ID" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "Hash" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "Active" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "CreatedDate" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "CustomKey" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "DistrictID" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "RegionID" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "CountryID" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "Prefix" });
            DropIndex("CLARITY.PhonePrefixLookups", new[] { "ID" });
            DropIndex("CLARITY.ImportExportMappings", new[] { "Hash" });
            DropIndex("CLARITY.ImportExportMappings", new[] { "Active" });
            DropIndex("CLARITY.ImportExportMappings", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ImportExportMappings", new[] { "CreatedDate" });
            DropIndex("CLARITY.ImportExportMappings", new[] { "CustomKey" });
            DropIndex("CLARITY.ImportExportMappings", new[] { "Name" });
            DropIndex("CLARITY.ImportExportMappings", new[] { "ID" });
            DropIndex("CLARITY.HistoricalTaxRates", new[] { "Hash" });
            DropIndex("CLARITY.HistoricalTaxRates", new[] { "Active" });
            DropIndex("CLARITY.HistoricalTaxRates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.HistoricalTaxRates", new[] { "CreatedDate" });
            DropIndex("CLARITY.HistoricalTaxRates", new[] { "CustomKey" });
            DropIndex("CLARITY.HistoricalTaxRates", new[] { "ID" });
            DropIndex("CLARITY.HistoricalAddressValidations", new[] { "Hash" });
            DropIndex("CLARITY.HistoricalAddressValidations", new[] { "Active" });
            DropIndex("CLARITY.HistoricalAddressValidations", new[] { "UpdatedDate" });
            DropIndex("CLARITY.HistoricalAddressValidations", new[] { "CreatedDate" });
            DropIndex("CLARITY.HistoricalAddressValidations", new[] { "CustomKey" });
            DropIndex("CLARITY.HistoricalAddressValidations", new[] { "ID" });
            DropIndex("CLARITY.HangfireSets", new[] { "ExpireAt" });
            DropIndex("CLARITY.HangfireSets", "UX_HangFire_Set_KeyAndValue");
            DropIndex("CLARITY.HangfireSets", "IX_HangFire_Set_Score");
            DropIndex("CLARITY.HangfireSets", new[] { "Key" });
            DropIndex("CLARITY.HangfireServers", "IX_HangFire_Server_LastHeartbeat");
            DropIndex("CLARITY.HangfireLists", new[] { "ExpireAt" });
            DropIndex("CLARITY.HangfireJobQueues", new[] { "JobId" });
            DropIndex("CLARITY.HangfireJobQueues", "IX_HangFire_JobQueue_QueueAndId");
            DropIndex("CLARITY.HangfireJobQueues", new[] { "Id" });
            DropIndex("CLARITY.HangfireStates", new[] { "JobId" });
            DropIndex("CLARITY.HangfireJobs", new[] { "ExpireAt" });
            DropIndex("CLARITY.HangfireJobs", new[] { "StateName" });
            DropIndex("CLARITY.HangfireJobs", new[] { "Id" });
            DropIndex("CLARITY.HangfireJobParameters", "IX_HangFire_JobParameter_JobIdAndName");
            DropIndex("CLARITY.HangfireHashes", "IX_HangFire_Hash_ExpireAt");
            DropIndex("CLARITY.HangfireHashes", "UX_HangFire_Hash_Key_Field");
            DropIndex("CLARITY.HangfireHashes", "IX_HangFire_Hash_Key");
            DropIndex("CLARITY.HangfireAggregatedCounters", "[IX_HangFire_AggregatedCounter_ExpireAt]");
            DropIndex("CLARITY.FutureImportStatus", new[] { "Hash" });
            DropIndex("CLARITY.FutureImportStatus", new[] { "Active" });
            DropIndex("CLARITY.FutureImportStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.FutureImportStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.FutureImportStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.FutureImportStatus", new[] { "Name" });
            DropIndex("CLARITY.FutureImportStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.FutureImportStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.FutureImportStatus", new[] { "ID" });
            DropIndex("CLARITY.FutureImports", new[] { "Hash" });
            DropIndex("CLARITY.FutureImports", new[] { "Active" });
            DropIndex("CLARITY.FutureImports", new[] { "UpdatedDate" });
            DropIndex("CLARITY.FutureImports", new[] { "CreatedDate" });
            DropIndex("CLARITY.FutureImports", new[] { "CustomKey" });
            DropIndex("CLARITY.FutureImports", new[] { "Name" });
            DropIndex("CLARITY.FutureImports", new[] { "VendorID" });
            DropIndex("CLARITY.FutureImports", new[] { "RunImportAt" });
            DropIndex("CLARITY.FutureImports", new[] { "FileName" });
            DropIndex("CLARITY.FutureImports", new[] { "StoreID" });
            DropIndex("CLARITY.FutureImports", new[] { "StatusID" });
            DropIndex("CLARITY.FutureImports", new[] { "ID" });
            DropIndex("CLARITY.FavoriteShipCarriers", new[] { "Hash" });
            DropIndex("CLARITY.FavoriteShipCarriers", new[] { "Active" });
            DropIndex("CLARITY.FavoriteShipCarriers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.FavoriteShipCarriers", new[] { "CreatedDate" });
            DropIndex("CLARITY.FavoriteShipCarriers", new[] { "CustomKey" });
            DropIndex("CLARITY.FavoriteShipCarriers", new[] { "SlaveID" });
            DropIndex("CLARITY.FavoriteShipCarriers", new[] { "MasterID" });
            DropIndex("CLARITY.FavoriteShipCarriers", new[] { "ID" });
            DropIndex("CLARITY.EventTypes", new[] { "Hash" });
            DropIndex("CLARITY.EventTypes", new[] { "Active" });
            DropIndex("CLARITY.EventTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.EventTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.EventTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.EventTypes", new[] { "Name" });
            DropIndex("CLARITY.EventTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.EventTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.EventTypes", new[] { "ID" });
            DropIndex("CLARITY.EventStatus", new[] { "Hash" });
            DropIndex("CLARITY.EventStatus", new[] { "Active" });
            DropIndex("CLARITY.EventStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.EventStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.EventStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.EventStatus", new[] { "Name" });
            DropIndex("CLARITY.EventStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.EventStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.EventStatus", new[] { "ID" });
            DropIndex("CLARITY.VisitStatus", new[] { "Hash" });
            DropIndex("CLARITY.VisitStatus", new[] { "Active" });
            DropIndex("CLARITY.VisitStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.VisitStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.VisitStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.VisitStatus", new[] { "Name" });
            DropIndex("CLARITY.VisitStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.VisitStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.VisitStatus", new[] { "ID" });
            DropIndex("CLARITY.Visits", new[] { "Hash" });
            DropIndex("CLARITY.Visits", new[] { "Active" });
            DropIndex("CLARITY.Visits", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Visits", new[] { "CreatedDate" });
            DropIndex("CLARITY.Visits", new[] { "CustomKey" });
            DropIndex("CLARITY.Visits", new[] { "Name" });
            DropIndex("CLARITY.Visits", new[] { "VisitorID" });
            DropIndex("CLARITY.Visits", new[] { "SiteDomainID" });
            DropIndex("CLARITY.Visits", new[] { "ContactID" });
            DropIndex("CLARITY.Visits", new[] { "CampaignID" });
            DropIndex("CLARITY.Visits", new[] { "UserID" });
            DropIndex("CLARITY.Visits", new[] { "IPOrganizationID" });
            DropIndex("CLARITY.Visits", new[] { "AddressID" });
            DropIndex("CLARITY.Visits", new[] { "StatusID" });
            DropIndex("CLARITY.Visits", new[] { "ID" });
            DropIndex("CLARITY.Visitors", new[] { "Hash" });
            DropIndex("CLARITY.Visitors", new[] { "Active" });
            DropIndex("CLARITY.Visitors", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Visitors", new[] { "CreatedDate" });
            DropIndex("CLARITY.Visitors", new[] { "CustomKey" });
            DropIndex("CLARITY.Visitors", new[] { "Name" });
            DropIndex("CLARITY.Visitors", new[] { "UserID" });
            DropIndex("CLARITY.Visitors", new[] { "IPOrganizationID" });
            DropIndex("CLARITY.Visitors", new[] { "AddressID" });
            DropIndex("CLARITY.Visitors", new[] { "ID" });
            DropIndex("CLARITY.PageViewTypes", new[] { "Hash" });
            DropIndex("CLARITY.PageViewTypes", new[] { "Active" });
            DropIndex("CLARITY.PageViewTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PageViewTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.PageViewTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.PageViewTypes", new[] { "Name" });
            DropIndex("CLARITY.PageViewTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.PageViewTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.PageViewTypes", new[] { "ID" });
            DropIndex("CLARITY.PageViewStatus", new[] { "Hash" });
            DropIndex("CLARITY.PageViewStatus", new[] { "Active" });
            DropIndex("CLARITY.PageViewStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PageViewStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.PageViewStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.PageViewStatus", new[] { "Name" });
            DropIndex("CLARITY.PageViewStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.PageViewStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.PageViewStatus", new[] { "ID" });
            DropIndex("CLARITY.PageViews", new[] { "Hash" });
            DropIndex("CLARITY.PageViews", new[] { "Active" });
            DropIndex("CLARITY.PageViews", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PageViews", new[] { "CreatedDate" });
            DropIndex("CLARITY.PageViews", new[] { "CustomKey" });
            DropIndex("CLARITY.PageViews", new[] { "Name" });
            DropIndex("CLARITY.PageViews", new[] { "ProductID" });
            DropIndex("CLARITY.PageViews", new[] { "CategoryID" });
            DropIndex("CLARITY.PageViews", new[] { "VisitorID" });
            DropIndex("CLARITY.PageViews", new[] { "SiteDomainID" });
            DropIndex("CLARITY.PageViews", new[] { "ContactID" });
            DropIndex("CLARITY.PageViews", new[] { "CampaignID" });
            DropIndex("CLARITY.PageViews", new[] { "UserID" });
            DropIndex("CLARITY.PageViews", new[] { "IPOrganizationID" });
            DropIndex("CLARITY.PageViews", new[] { "AddressID" });
            DropIndex("CLARITY.PageViews", new[] { "StatusID" });
            DropIndex("CLARITY.PageViews", new[] { "TypeID" });
            DropIndex("CLARITY.PageViews", new[] { "ID" });
            DropIndex("CLARITY.PageViewEvents", new[] { "Hash" });
            DropIndex("CLARITY.PageViewEvents", new[] { "Active" });
            DropIndex("CLARITY.PageViewEvents", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PageViewEvents", new[] { "CreatedDate" });
            DropIndex("CLARITY.PageViewEvents", new[] { "CustomKey" });
            DropIndex("CLARITY.PageViewEvents", new[] { "SlaveID" });
            DropIndex("CLARITY.PageViewEvents", new[] { "MasterID" });
            DropIndex("CLARITY.PageViewEvents", new[] { "ID" });
            DropIndex("CLARITY.IPOrganizationStatus", new[] { "Hash" });
            DropIndex("CLARITY.IPOrganizationStatus", new[] { "Active" });
            DropIndex("CLARITY.IPOrganizationStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.IPOrganizationStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.IPOrganizationStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.IPOrganizationStatus", new[] { "Name" });
            DropIndex("CLARITY.IPOrganizationStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.IPOrganizationStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.IPOrganizationStatus", new[] { "ID" });
            DropIndex("CLARITY.IPOrganizations", new[] { "Hash" });
            DropIndex("CLARITY.IPOrganizations", new[] { "Active" });
            DropIndex("CLARITY.IPOrganizations", new[] { "UpdatedDate" });
            DropIndex("CLARITY.IPOrganizations", new[] { "CreatedDate" });
            DropIndex("CLARITY.IPOrganizations", new[] { "CustomKey" });
            DropIndex("CLARITY.IPOrganizations", new[] { "Name" });
            DropIndex("CLARITY.IPOrganizations", new[] { "PrimaryUserID" });
            DropIndex("CLARITY.IPOrganizations", new[] { "AddressID" });
            DropIndex("CLARITY.IPOrganizations", new[] { "StatusID" });
            DropIndex("CLARITY.IPOrganizations", new[] { "ID" });
            DropIndex("CLARITY.Events", new[] { "Hash" });
            DropIndex("CLARITY.Events", new[] { "Active" });
            DropIndex("CLARITY.Events", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Events", new[] { "CreatedDate" });
            DropIndex("CLARITY.Events", new[] { "CustomKey" });
            DropIndex("CLARITY.Events", new[] { "Name" });
            DropIndex("CLARITY.Events", new[] { "VisitID" });
            DropIndex("CLARITY.Events", new[] { "VisitorID" });
            DropIndex("CLARITY.Events", new[] { "SiteDomainID" });
            DropIndex("CLARITY.Events", new[] { "ContactID" });
            DropIndex("CLARITY.Events", new[] { "CampaignID" });
            DropIndex("CLARITY.Events", new[] { "UserID" });
            DropIndex("CLARITY.Events", new[] { "IPOrganizationID" });
            DropIndex("CLARITY.Events", new[] { "AddressID" });
            DropIndex("CLARITY.Events", new[] { "StatusID" });
            DropIndex("CLARITY.Events", new[] { "TypeID" });
            DropIndex("CLARITY.Events", new[] { "ID" });
            DropIndex("CLARITY.EventLogs", new[] { "Hash" });
            DropIndex("CLARITY.EventLogs", new[] { "Active" });
            DropIndex("CLARITY.EventLogs", new[] { "UpdatedDate" });
            DropIndex("CLARITY.EventLogs", new[] { "CreatedDate" });
            DropIndex("CLARITY.EventLogs", new[] { "CustomKey" });
            DropIndex("CLARITY.EventLogs", new[] { "Name" });
            DropIndex("CLARITY.EventLogs", new[] { "StoreID" });
            DropIndex("CLARITY.EventLogs", new[] { "ID" });
            DropIndex("CLARITY.GeneralAttributePredefinedOptions", new[] { "Hash" });
            DropIndex("CLARITY.GeneralAttributePredefinedOptions", new[] { "Active" });
            DropIndex("CLARITY.GeneralAttributePredefinedOptions", new[] { "UpdatedDate" });
            DropIndex("CLARITY.GeneralAttributePredefinedOptions", new[] { "CreatedDate" });
            DropIndex("CLARITY.GeneralAttributePredefinedOptions", new[] { "CustomKey" });
            DropIndex("CLARITY.GeneralAttributePredefinedOptions", new[] { "AttributeID" });
            DropIndex("CLARITY.GeneralAttributePredefinedOptions", new[] { "ID" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "Hash" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "Active" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "CreatedDate" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "Name" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "SortOrder" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "DisplayName" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "AttributeGroupID" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "AttributeTabID" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "TypeID" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "CustomKey" });
            DropIndex("CLARITY.GeneralAttributes", new[] { "ID" });
            DropIndex("CLARITY.AttributeTypes", new[] { "Hash" });
            DropIndex("CLARITY.AttributeTypes", new[] { "Active" });
            DropIndex("CLARITY.AttributeTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AttributeTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.AttributeTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.AttributeTypes", new[] { "Name" });
            DropIndex("CLARITY.AttributeTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.AttributeTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.AttributeTypes", new[] { "ID" });
            DropIndex("CLARITY.AttributeTabs", new[] { "Hash" });
            DropIndex("CLARITY.AttributeTabs", new[] { "Active" });
            DropIndex("CLARITY.AttributeTabs", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AttributeTabs", new[] { "CreatedDate" });
            DropIndex("CLARITY.AttributeTabs", new[] { "CustomKey" });
            DropIndex("CLARITY.AttributeTabs", new[] { "Name" });
            DropIndex("CLARITY.AttributeTabs", new[] { "SortOrder" });
            DropIndex("CLARITY.AttributeTabs", new[] { "DisplayName" });
            DropIndex("CLARITY.AttributeTabs", new[] { "ID" });
            DropIndex("CLARITY.AttributeGroups", new[] { "Hash" });
            DropIndex("CLARITY.AttributeGroups", new[] { "Active" });
            DropIndex("CLARITY.AttributeGroups", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AttributeGroups", new[] { "CreatedDate" });
            DropIndex("CLARITY.AttributeGroups", new[] { "CustomKey" });
            DropIndex("CLARITY.AttributeGroups", new[] { "Name" });
            DropIndex("CLARITY.AttributeGroups", new[] { "SortOrder" });
            DropIndex("CLARITY.AttributeGroups", new[] { "DisplayName" });
            DropIndex("CLARITY.AttributeGroups", new[] { "ID" });
            DropIndex("CLARITY.AccountUsageBalances", new[] { "Hash" });
            DropIndex("CLARITY.AccountUsageBalances", new[] { "Active" });
            DropIndex("CLARITY.AccountUsageBalances", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountUsageBalances", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountUsageBalances", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountUsageBalances", new[] { "SlaveID" });
            DropIndex("CLARITY.AccountUsageBalances", new[] { "MasterID" });
            DropIndex("CLARITY.AccountUsageBalances", new[] { "ID" });
            DropIndex("CLARITY.AccountAssociationTypes", new[] { "Hash" });
            DropIndex("CLARITY.AccountAssociationTypes", new[] { "Active" });
            DropIndex("CLARITY.AccountAssociationTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountAssociationTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountAssociationTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountAssociationTypes", new[] { "Name" });
            DropIndex("CLARITY.AccountAssociationTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.AccountAssociationTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.AccountAssociationTypes", new[] { "ID" });
            DropIndex("CLARITY.AccountFiles", new[] { "Hash" });
            DropIndex("CLARITY.AccountFiles", new[] { "Active" });
            DropIndex("CLARITY.AccountFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountFiles", new[] { "Name" });
            DropIndex("CLARITY.AccountFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.AccountFiles", new[] { "MasterID" });
            DropIndex("CLARITY.AccountFiles", new[] { "ID" });
            DropIndex("CLARITY.AccountStatus", new[] { "Hash" });
            DropIndex("CLARITY.AccountStatus", new[] { "Active" });
            DropIndex("CLARITY.AccountStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountStatus", new[] { "Name" });
            DropIndex("CLARITY.AccountStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.AccountStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.AccountStatus", new[] { "ID" });
            DropIndex("CLARITY.AccountImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.AccountImageTypes", new[] { "Active" });
            DropIndex("CLARITY.AccountImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountImageTypes", new[] { "Name" });
            DropIndex("CLARITY.AccountImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.AccountImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.AccountImageTypes", new[] { "ID" });
            DropIndex("CLARITY.AccountImages", new[] { "Hash" });
            DropIndex("CLARITY.AccountImages", new[] { "Active" });
            DropIndex("CLARITY.AccountImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountImages", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountImages", new[] { "Name" });
            DropIndex("CLARITY.AccountImages", new[] { "TypeID" });
            DropIndex("CLARITY.AccountImages", new[] { "MasterID" });
            DropIndex("CLARITY.AccountImages", new[] { "ID" });
            DropIndex("CLARITY.AccountUserRoles", new[] { "Hash" });
            DropIndex("CLARITY.AccountUserRoles", new[] { "Active" });
            DropIndex("CLARITY.AccountUserRoles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountUserRoles", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountUserRoles", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountUserRoles", new[] { "SlaveID" });
            DropIndex("CLARITY.AccountUserRoles", new[] { "MasterID" });
            DropIndex("CLARITY.AccountUserRoles", new[] { "ID" });
            DropIndex("CLARITY.AccountPricePoints", new[] { "Hash" });
            DropIndex("CLARITY.AccountPricePoints", new[] { "Active" });
            DropIndex("CLARITY.AccountPricePoints", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountPricePoints", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountPricePoints", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountPricePoints", new[] { "SlaveID" });
            DropIndex("CLARITY.AccountPricePoints", new[] { "MasterID" });
            DropIndex("CLARITY.AccountPricePoints", new[] { "ID" });
            DropIndex("CLARITY.AccountCurrencies", new[] { "Hash" });
            DropIndex("CLARITY.AccountCurrencies", new[] { "Active" });
            DropIndex("CLARITY.AccountCurrencies", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountCurrencies", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountCurrencies", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountCurrencies", new[] { "SlaveID" });
            DropIndex("CLARITY.AccountCurrencies", new[] { "MasterID" });
            DropIndex("CLARITY.AccountCurrencies", new[] { "ID" });
            DropIndex("CLARITY.ContactTypes", new[] { "Hash" });
            DropIndex("CLARITY.ContactTypes", new[] { "Active" });
            DropIndex("CLARITY.ContactTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ContactTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ContactTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ContactTypes", new[] { "Name" });
            DropIndex("CLARITY.ContactTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ContactTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ContactTypes", new[] { "ID" });
            DropIndex("CLARITY.ContactImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.ContactImageTypes", new[] { "Active" });
            DropIndex("CLARITY.ContactImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ContactImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ContactImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ContactImageTypes", new[] { "Name" });
            DropIndex("CLARITY.ContactImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ContactImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ContactImageTypes", new[] { "ID" });
            DropIndex("CLARITY.ContactImages", new[] { "Hash" });
            DropIndex("CLARITY.ContactImages", new[] { "Active" });
            DropIndex("CLARITY.ContactImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ContactImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.ContactImages", new[] { "CustomKey" });
            DropIndex("CLARITY.ContactImages", new[] { "Name" });
            DropIndex("CLARITY.ContactImages", new[] { "TypeID" });
            DropIndex("CLARITY.ContactImages", new[] { "MasterID" });
            DropIndex("CLARITY.ContactImages", new[] { "ID" });
            DropIndex("CLARITY.PurchaseOrderTypes", new[] { "Hash" });
            DropIndex("CLARITY.PurchaseOrderTypes", new[] { "Active" });
            DropIndex("CLARITY.PurchaseOrderTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PurchaseOrderTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.PurchaseOrderTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.PurchaseOrderTypes", new[] { "Name" });
            DropIndex("CLARITY.PurchaseOrderTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.PurchaseOrderTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.PurchaseOrderTypes", new[] { "ID" });
            DropIndex("CLARITY.PurchaseOrderFiles", new[] { "Hash" });
            DropIndex("CLARITY.PurchaseOrderFiles", new[] { "Active" });
            DropIndex("CLARITY.PurchaseOrderFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PurchaseOrderFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.PurchaseOrderFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.PurchaseOrderFiles", new[] { "Name" });
            DropIndex("CLARITY.PurchaseOrderFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.PurchaseOrderFiles", new[] { "MasterID" });
            DropIndex("CLARITY.PurchaseOrderFiles", new[] { "ID" });
            DropIndex("CLARITY.PurchaseOrderStatus", new[] { "Hash" });
            DropIndex("CLARITY.PurchaseOrderStatus", new[] { "Active" });
            DropIndex("CLARITY.PurchaseOrderStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PurchaseOrderStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.PurchaseOrderStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.PurchaseOrderStatus", new[] { "Name" });
            DropIndex("CLARITY.PurchaseOrderStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.PurchaseOrderStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.PurchaseOrderStatus", new[] { "ID" });
            DropIndex("CLARITY.PurchaseOrderStates", new[] { "Hash" });
            DropIndex("CLARITY.PurchaseOrderStates", new[] { "Active" });
            DropIndex("CLARITY.PurchaseOrderStates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PurchaseOrderStates", new[] { "CreatedDate" });
            DropIndex("CLARITY.PurchaseOrderStates", new[] { "CustomKey" });
            DropIndex("CLARITY.PurchaseOrderStates", new[] { "Name" });
            DropIndex("CLARITY.PurchaseOrderStates", new[] { "SortOrder" });
            DropIndex("CLARITY.PurchaseOrderStates", new[] { "DisplayName" });
            DropIndex("CLARITY.PurchaseOrderStates", new[] { "ID" });
            DropIndex("CLARITY.AppliedPurchaseOrderDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedPurchaseOrderDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedPurchaseOrderDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedPurchaseOrderDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedPurchaseOrderDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedPurchaseOrderDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedPurchaseOrderDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedPurchaseOrderDiscounts", new[] { "ID" });
            DropIndex("CLARITY.PurchaseOrderContacts", new[] { "Hash" });
            DropIndex("CLARITY.PurchaseOrderContacts", new[] { "Active" });
            DropIndex("CLARITY.PurchaseOrderContacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PurchaseOrderContacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.PurchaseOrderContacts", new[] { "CustomKey" });
            DropIndex("CLARITY.PurchaseOrderContacts", new[] { "SlaveID" });
            DropIndex("CLARITY.PurchaseOrderContacts", new[] { "MasterID" });
            DropIndex("CLARITY.PurchaseOrderContacts", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderTypes", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderTypes", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderTypes", new[] { "Name" });
            DropIndex("CLARITY.SalesOrderTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesOrderTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesOrderTypes", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderFiles", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderFiles", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderFiles", new[] { "Name" });
            DropIndex("CLARITY.SalesOrderFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesOrderFiles", new[] { "MasterID" });
            DropIndex("CLARITY.SalesOrderFiles", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderStatus", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderStatus", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderStatus", new[] { "Name" });
            DropIndex("CLARITY.SalesOrderStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesOrderStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesOrderStatus", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderStates", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderStates", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderStates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderStates", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderStates", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderStates", new[] { "Name" });
            DropIndex("CLARITY.SalesOrderStates", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesOrderStates", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesOrderStates", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderPayments", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderPayments", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderPayments", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderPayments", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderPayments", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderPayments", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesOrderPayments", new[] { "MasterID" });
            DropIndex("CLARITY.SalesOrderPayments", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderEvents", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderEvents", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderEvents", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderEvents", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderEvents", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderEvents", new[] { "Name" });
            DropIndex("CLARITY.SalesOrderEvents", new[] { "SalesOrderID" });
            DropIndex("CLARITY.SalesOrderEvents", new[] { "ID" });
            DropIndex("CLARITY.AppliedSalesOrderDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSalesOrderDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSalesOrderDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSalesOrderDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSalesOrderDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSalesOrderDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSalesOrderDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSalesOrderDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderContacts", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderContacts", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderContacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderContacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderContacts", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderContacts", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesOrderContacts", new[] { "MasterID" });
            DropIndex("CLARITY.SalesOrderContacts", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoiceTypes", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoiceTypes", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoiceTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoiceTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoiceTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoiceTypes", new[] { "Name" });
            DropIndex("CLARITY.SalesInvoiceTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesInvoiceTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesInvoiceTypes", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoiceFiles", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoiceFiles", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoiceFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoiceFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoiceFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoiceFiles", new[] { "Name" });
            DropIndex("CLARITY.SalesInvoiceFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesInvoiceFiles", new[] { "MasterID" });
            DropIndex("CLARITY.SalesInvoiceFiles", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoiceStatus", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoiceStatus", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoiceStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoiceStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoiceStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoiceStatus", new[] { "Name" });
            DropIndex("CLARITY.SalesInvoiceStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesInvoiceStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesInvoiceStatus", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoiceStates", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoiceStates", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoiceStates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoiceStates", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoiceStates", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoiceStates", new[] { "Name" });
            DropIndex("CLARITY.SalesInvoiceStates", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesInvoiceStates", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesInvoiceStates", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoicePayments", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoicePayments", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoicePayments", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoicePayments", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoicePayments", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoicePayments", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesInvoicePayments", new[] { "MasterID" });
            DropIndex("CLARITY.SalesInvoicePayments", new[] { "ID" });
            DropIndex("CLARITY.DiscountVendors", new[] { "Hash" });
            DropIndex("CLARITY.DiscountVendors", new[] { "Active" });
            DropIndex("CLARITY.DiscountVendors", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountVendors", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountVendors", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountVendors", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountVendors", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountVendors", new[] { "ID" });
            DropIndex("CLARITY.DiscountUsers", new[] { "Hash" });
            DropIndex("CLARITY.DiscountUsers", new[] { "Active" });
            DropIndex("CLARITY.DiscountUsers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountUsers", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountUsers", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountUsers", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountUsers", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountUsers", new[] { "ID" });
            DropIndex("CLARITY.DiscountUserRoles", new[] { "Hash" });
            DropIndex("CLARITY.DiscountUserRoles", new[] { "Active" });
            DropIndex("CLARITY.DiscountUserRoles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountUserRoles", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountUserRoles", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountUserRoles", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountUserRoles", new[] { "ID" });
            DropIndex("CLARITY.DiscountStores", new[] { "Hash" });
            DropIndex("CLARITY.DiscountStores", new[] { "Active" });
            DropIndex("CLARITY.DiscountStores", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountStores", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountStores", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountStores", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountStores", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountStores", new[] { "ID" });
            DropIndex("CLARITY.DiscountShipCarrierMethods", new[] { "Hash" });
            DropIndex("CLARITY.DiscountShipCarrierMethods", new[] { "Active" });
            DropIndex("CLARITY.DiscountShipCarrierMethods", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountShipCarrierMethods", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountShipCarrierMethods", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountShipCarrierMethods", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountShipCarrierMethods", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountShipCarrierMethods", new[] { "ID" });
            DropIndex("CLARITY.DiscountProductTypes", new[] { "Hash" });
            DropIndex("CLARITY.DiscountProductTypes", new[] { "Active" });
            DropIndex("CLARITY.DiscountProductTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountProductTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountProductTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountProductTypes", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountProductTypes", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountProductTypes", new[] { "ID" });
            DropIndex("CLARITY.DiscountManufacturers", new[] { "Hash" });
            DropIndex("CLARITY.DiscountManufacturers", new[] { "Active" });
            DropIndex("CLARITY.DiscountManufacturers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountManufacturers", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountManufacturers", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountManufacturers", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountManufacturers", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountManufacturers", new[] { "ID" });
            DropIndex("CLARITY.DiscountCountries", new[] { "Hash" });
            DropIndex("CLARITY.DiscountCountries", new[] { "Active" });
            DropIndex("CLARITY.DiscountCountries", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountCountries", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountCountries", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountCountries", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountCountries", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountCountries", new[] { "ID" });
            DropIndex("CLARITY.DiscountCategories", new[] { "Hash" });
            DropIndex("CLARITY.DiscountCategories", new[] { "Active" });
            DropIndex("CLARITY.DiscountCategories", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountCategories", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountCategories", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountCategories", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountCategories", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountCategories", new[] { "ID" });
            DropIndex("CLARITY.StoreTypes", new[] { "Hash" });
            DropIndex("CLARITY.StoreTypes", new[] { "Active" });
            DropIndex("CLARITY.StoreTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreTypes", new[] { "Name" });
            DropIndex("CLARITY.StoreTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.StoreTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.StoreTypes", new[] { "ID" });
            DropIndex("CLARITY.StoreContacts", new[] { "Hash" });
            DropIndex("CLARITY.StoreContacts", new[] { "Active" });
            DropIndex("CLARITY.StoreContacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreContacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreContacts", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreContacts", new[] { "Name" });
            DropIndex("CLARITY.StoreContacts", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreContacts", new[] { "MasterID" });
            DropIndex("CLARITY.StoreContacts", new[] { "ID" });
            DropIndex("CLARITY.BadgeTypes", new[] { "Hash" });
            DropIndex("CLARITY.BadgeTypes", new[] { "Active" });
            DropIndex("CLARITY.BadgeTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.BadgeTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.BadgeTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.BadgeTypes", new[] { "Name" });
            DropIndex("CLARITY.BadgeTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.BadgeTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.BadgeTypes", new[] { "ID" });
            DropIndex("CLARITY.BadgeImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.BadgeImageTypes", new[] { "Active" });
            DropIndex("CLARITY.BadgeImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.BadgeImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.BadgeImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.BadgeImageTypes", new[] { "Name" });
            DropIndex("CLARITY.BadgeImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.BadgeImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.BadgeImageTypes", new[] { "ID" });
            DropIndex("CLARITY.BadgeImages", new[] { "Hash" });
            DropIndex("CLARITY.BadgeImages", new[] { "Active" });
            DropIndex("CLARITY.BadgeImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.BadgeImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.BadgeImages", new[] { "CustomKey" });
            DropIndex("CLARITY.BadgeImages", new[] { "Name" });
            DropIndex("CLARITY.BadgeImages", new[] { "TypeID" });
            DropIndex("CLARITY.BadgeImages", new[] { "MasterID" });
            DropIndex("CLARITY.BadgeImages", new[] { "ID" });
            DropIndex("CLARITY.Badges", new[] { "Hash" });
            DropIndex("CLARITY.Badges", new[] { "Active" });
            DropIndex("CLARITY.Badges", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Badges", new[] { "CreatedDate" });
            DropIndex("CLARITY.Badges", new[] { "CustomKey" });
            DropIndex("CLARITY.Badges", new[] { "Name" });
            DropIndex("CLARITY.Badges", new[] { "TypeID" });
            DropIndex("CLARITY.Badges", new[] { "ID" });
            DropIndex("CLARITY.StoreBadges", new[] { "Hash" });
            DropIndex("CLARITY.StoreBadges", new[] { "Active" });
            DropIndex("CLARITY.StoreBadges", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreBadges", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreBadges", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreBadges", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreBadges", new[] { "MasterID" });
            DropIndex("CLARITY.StoreBadges", new[] { "ID" });
            DropIndex("CLARITY.ManufacturerTypes", new[] { "Hash" });
            DropIndex("CLARITY.ManufacturerTypes", new[] { "Active" });
            DropIndex("CLARITY.ManufacturerTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ManufacturerTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ManufacturerTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ManufacturerTypes", new[] { "Name" });
            DropIndex("CLARITY.ManufacturerTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ManufacturerTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ManufacturerTypes", new[] { "ID" });
            DropIndex("CLARITY.StoreCategoryTypes", new[] { "Hash" });
            DropIndex("CLARITY.StoreCategoryTypes", new[] { "Active" });
            DropIndex("CLARITY.StoreCategoryTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreCategoryTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreCategoryTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreCategoryTypes", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreCategoryTypes", new[] { "MasterID" });
            DropIndex("CLARITY.StoreCategoryTypes", new[] { "ID" });
            DropIndex("CLARITY.CategoryTypes", new[] { "Hash" });
            DropIndex("CLARITY.CategoryTypes", new[] { "Active" });
            DropIndex("CLARITY.CategoryTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CategoryTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CategoryTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CategoryTypes", new[] { "Name" });
            DropIndex("CLARITY.CategoryTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CategoryTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CategoryTypes", new[] { "ParentID" });
            DropIndex("CLARITY.CategoryTypes", new[] { "ID" });
            DropIndex("CLARITY.StoreCategories", new[] { "Hash" });
            DropIndex("CLARITY.StoreCategories", new[] { "Active" });
            DropIndex("CLARITY.StoreCategories", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreCategories", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreCategories", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreCategories", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreCategories", new[] { "MasterID" });
            DropIndex("CLARITY.StoreCategories", new[] { "ID" });
            DropIndex("CLARITY.CategoryFiles", new[] { "Hash" });
            DropIndex("CLARITY.CategoryFiles", new[] { "Active" });
            DropIndex("CLARITY.CategoryFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CategoryFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.CategoryFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.CategoryFiles", new[] { "Name" });
            DropIndex("CLARITY.CategoryFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.CategoryFiles", new[] { "MasterID" });
            DropIndex("CLARITY.CategoryFiles", new[] { "ID" });
            DropIndex("CLARITY.ProductFiles", new[] { "Hash" });
            DropIndex("CLARITY.ProductFiles", new[] { "Active" });
            DropIndex("CLARITY.ProductFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductFiles", new[] { "Name" });
            DropIndex("CLARITY.ProductFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.ProductFiles", new[] { "MasterID" });
            DropIndex("CLARITY.ProductFiles", new[] { "ID" });
            DropIndex("CLARITY.ProductStatus", new[] { "Hash" });
            DropIndex("CLARITY.ProductStatus", new[] { "Active" });
            DropIndex("CLARITY.ProductStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductStatus", new[] { "Name" });
            DropIndex("CLARITY.ProductStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.ProductStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.ProductStatus", new[] { "ID" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "Hash" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "Active" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "RestrictionsApplyToDistrictID" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "RestrictionsApplyToRegionID" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "RestrictionsApplyToCountryID" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "OverrideWithAccountTypeID" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "ProductID" });
            DropIndex("CLARITY.ProductRestrictions", new[] { "ID" });
            DropIndex("CLARITY.PriceRoundings", new[] { "Hash" });
            DropIndex("CLARITY.PriceRoundings", new[] { "Active" });
            DropIndex("CLARITY.PriceRoundings", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PriceRoundings", new[] { "CreatedDate" });
            DropIndex("CLARITY.PriceRoundings", new[] { "CustomKey" });
            DropIndex("CLARITY.PriceRoundings", new[] { "ID" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "Hash" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "Active" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "CurrencyID" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "PriceRoundingID" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "StoreID" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "SlaveID" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "MasterID" });
            DropIndex("CLARITY.ProductPricePoints", new[] { "ID" });
            DropIndex("CLARITY.ProductNotifications", new[] { "Hash" });
            DropIndex("CLARITY.ProductNotifications", new[] { "Active" });
            DropIndex("CLARITY.ProductNotifications", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductNotifications", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductNotifications", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductNotifications", new[] { "Name" });
            DropIndex("CLARITY.ProductNotifications", new[] { "ProductID" });
            DropIndex("CLARITY.ProductNotifications", new[] { "ID" });
            DropIndex("CLARITY.ProductCategories", new[] { "Hash" });
            DropIndex("CLARITY.ProductCategories", new[] { "Active" });
            DropIndex("CLARITY.ProductCategories", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductCategories", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductCategories", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductCategories", new[] { "SlaveID" });
            DropIndex("CLARITY.ProductCategories", new[] { "MasterID" });
            DropIndex("CLARITY.ProductCategories", new[] { "ID" });
            DropIndex("CLARITY.ProductAssociationTypes", new[] { "Hash" });
            DropIndex("CLARITY.ProductAssociationTypes", new[] { "Active" });
            DropIndex("CLARITY.ProductAssociationTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductAssociationTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductAssociationTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductAssociationTypes", new[] { "Name" });
            DropIndex("CLARITY.ProductAssociationTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ProductAssociationTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ProductAssociationTypes", new[] { "ID" });
            DropIndex("CLARITY.ProductAssociations", new[] { "Hash" });
            DropIndex("CLARITY.ProductAssociations", new[] { "Active" });
            DropIndex("CLARITY.ProductAssociations", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductAssociations", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductAssociations", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductAssociations", new[] { "TypeID" });
            DropIndex("CLARITY.ProductAssociations", new[] { "StoreID" });
            DropIndex("CLARITY.ProductAssociations", new[] { "SlaveID" });
            DropIndex("CLARITY.ProductAssociations", new[] { "MasterID" });
            DropIndex("CLARITY.ProductAssociations", new[] { "ID" });
            DropIndex("CLARITY.PackageTypes", new[] { "Hash" });
            DropIndex("CLARITY.PackageTypes", new[] { "Active" });
            DropIndex("CLARITY.PackageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PackageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.PackageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.PackageTypes", new[] { "Name" });
            DropIndex("CLARITY.PackageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.PackageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.PackageTypes", new[] { "ID" });
            DropIndex("CLARITY.Packages", new[] { "Hash" });
            DropIndex("CLARITY.Packages", new[] { "Active" });
            DropIndex("CLARITY.Packages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Packages", new[] { "CreatedDate" });
            DropIndex("CLARITY.Packages", new[] { "CustomKey" });
            DropIndex("CLARITY.Packages", new[] { "Name" });
            DropIndex("CLARITY.Packages", new[] { "TypeID" });
            DropIndex("CLARITY.Packages", new[] { "ID" });
            DropIndex("CLARITY.ManufacturerProducts", new[] { "Hash" });
            DropIndex("CLARITY.ManufacturerProducts", new[] { "Active" });
            DropIndex("CLARITY.ManufacturerProducts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ManufacturerProducts", new[] { "CreatedDate" });
            DropIndex("CLARITY.ManufacturerProducts", new[] { "CustomKey" });
            DropIndex("CLARITY.ManufacturerProducts", new[] { "SlaveID" });
            DropIndex("CLARITY.ManufacturerProducts", new[] { "MasterID" });
            DropIndex("CLARITY.ManufacturerProducts", new[] { "ID" });
            DropIndex("CLARITY.ProductImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.ProductImageTypes", new[] { "Active" });
            DropIndex("CLARITY.ProductImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductImageTypes", new[] { "Name" });
            DropIndex("CLARITY.ProductImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ProductImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ProductImageTypes", new[] { "ID" });
            DropIndex("CLARITY.ProductImages", new[] { "Hash" });
            DropIndex("CLARITY.ProductImages", new[] { "Active" });
            DropIndex("CLARITY.ProductImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductImages", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductImages", new[] { "Name" });
            DropIndex("CLARITY.ProductImages", new[] { "TypeID" });
            DropIndex("CLARITY.ProductImages", new[] { "MasterID" });
            DropIndex("CLARITY.ProductImages", new[] { "ID" });
            DropIndex("CLARITY.DiscountProducts", new[] { "Hash" });
            DropIndex("CLARITY.DiscountProducts", new[] { "Active" });
            DropIndex("CLARITY.DiscountProducts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountProducts", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountProducts", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountProducts", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountProducts", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountProducts", new[] { "ID" });
            DropIndex("CLARITY.CartItemTargets", new[] { "Hash" });
            DropIndex("CLARITY.CartItemTargets", new[] { "Active" });
            DropIndex("CLARITY.CartItemTargets", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CartItemTargets", new[] { "CreatedDate" });
            DropIndex("CLARITY.CartItemTargets", new[] { "CustomKey" });
            DropIndex("CLARITY.CartItemTargets", new[] { "SelectedRateQuoteID" });
            DropIndex("CLARITY.CartItemTargets", new[] { "OriginVendorProductID" });
            DropIndex("CLARITY.CartItemTargets", new[] { "OriginStoreProductID" });
            DropIndex("CLARITY.CartItemTargets", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("CLARITY.CartItemTargets", new[] { "DestinationContactID" });
            DropIndex("CLARITY.CartItemTargets", new[] { "TypeID" });
            DropIndex("CLARITY.CartItemTargets", new[] { "MasterID" });
            DropIndex("CLARITY.CartItemTargets", new[] { "ID" });
            DropIndex("CLARITY.CartItemStatus", new[] { "Hash" });
            DropIndex("CLARITY.CartItemStatus", new[] { "Active" });
            DropIndex("CLARITY.CartItemStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CartItemStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.CartItemStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.CartItemStatus", new[] { "Name" });
            DropIndex("CLARITY.CartItemStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.CartItemStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.CartItemStatus", new[] { "ID" });
            DropIndex("CLARITY.CartTypes", new[] { "Hash" });
            DropIndex("CLARITY.CartTypes", new[] { "Active" });
            DropIndex("CLARITY.CartTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CartTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CartTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CartTypes", new[] { "Name" });
            DropIndex("CLARITY.CartTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CartTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CartTypes", new[] { "CreatedByUserID" });
            DropIndex("CLARITY.CartTypes", new[] { "StoreID" });
            DropIndex("CLARITY.CartTypes", new[] { "ID" });
            DropIndex("CLARITY.CartFiles", new[] { "Hash" });
            DropIndex("CLARITY.CartFiles", new[] { "Active" });
            DropIndex("CLARITY.CartFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CartFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.CartFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.CartFiles", new[] { "Name" });
            DropIndex("CLARITY.CartFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.CartFiles", new[] { "MasterID" });
            DropIndex("CLARITY.CartFiles", new[] { "ID" });
            DropIndex("CLARITY.CartStatus", new[] { "Hash" });
            DropIndex("CLARITY.CartStatus", new[] { "Active" });
            DropIndex("CLARITY.CartStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CartStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.CartStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.CartStatus", new[] { "Name" });
            DropIndex("CLARITY.CartStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.CartStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.CartStatus", new[] { "ID" });
            DropIndex("CLARITY.CartStates", new[] { "Hash" });
            DropIndex("CLARITY.CartStates", new[] { "Active" });
            DropIndex("CLARITY.CartStates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CartStates", new[] { "CreatedDate" });
            DropIndex("CLARITY.CartStates", new[] { "CustomKey" });
            DropIndex("CLARITY.CartStates", new[] { "Name" });
            DropIndex("CLARITY.CartStates", new[] { "SortOrder" });
            DropIndex("CLARITY.CartStates", new[] { "DisplayName" });
            DropIndex("CLARITY.CartStates", new[] { "ID" });
            DropIndex("CLARITY.NoteTypes", new[] { "Hash" });
            DropIndex("CLARITY.NoteTypes", new[] { "Active" });
            DropIndex("CLARITY.NoteTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.NoteTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.NoteTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.NoteTypes", new[] { "Name" });
            DropIndex("CLARITY.NoteTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.NoteTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.NoteTypes", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "SelectedRateQuoteID" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "OriginVendorProductID" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "OriginStoreProductID" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "DestinationContactID" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "TypeID" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "MasterID" });
            DropIndex("CLARITY.SalesOrderItemTargets", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderItemStatus", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderItemStatus", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderItemStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderItemStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderItemStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderItemStatus", new[] { "Name" });
            DropIndex("CLARITY.SalesOrderItemStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesOrderItemStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesOrderItemStatus", new[] { "ID" });
            DropIndex("CLARITY.AppliedSalesOrderItemDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSalesOrderItemDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSalesOrderItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSalesOrderItemDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSalesOrderItemDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSalesOrderItemDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSalesOrderItemDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSalesOrderItemDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "Name" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "SellingCurrencyID" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "OriginalCurrencyID" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "UserID" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "ProductID" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "StatusID" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "MasterID" });
            DropIndex("CLARITY.SalesOrderItems", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "SelectedRateQuoteID" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "OriginVendorProductID" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "OriginStoreProductID" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "DestinationContactID" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "TypeID" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "MasterID" });
            DropIndex("CLARITY.SalesInvoiceItemTargets", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoiceItemStatus", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoiceItemStatus", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoiceItemStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoiceItemStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoiceItemStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoiceItemStatus", new[] { "Name" });
            DropIndex("CLARITY.SalesInvoiceItemStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesInvoiceItemStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesInvoiceItemStatus", new[] { "ID" });
            DropIndex("CLARITY.AppliedSalesInvoiceItemDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSalesInvoiceItemDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSalesInvoiceItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSalesInvoiceItemDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSalesInvoiceItemDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSalesInvoiceItemDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSalesInvoiceItemDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSalesInvoiceItemDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "Name" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "SellingCurrencyID" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "OriginalCurrencyID" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "UserID" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "ProductID" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "StatusID" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "MasterID" });
            DropIndex("CLARITY.SalesInvoiceItems", new[] { "ID" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "Hash" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "Active" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "CreatedDate" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "CustomKey" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "SelectedRateQuoteID" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "OriginVendorProductID" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "OriginStoreProductID" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "DestinationContactID" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "TypeID" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "MasterID" });
            DropIndex("CLARITY.PurchaseOrderItemTargets", new[] { "ID" });
            DropIndex("CLARITY.PurchaseOrderItemStatus", new[] { "Hash" });
            DropIndex("CLARITY.PurchaseOrderItemStatus", new[] { "Active" });
            DropIndex("CLARITY.PurchaseOrderItemStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PurchaseOrderItemStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.PurchaseOrderItemStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.PurchaseOrderItemStatus", new[] { "Name" });
            DropIndex("CLARITY.PurchaseOrderItemStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.PurchaseOrderItemStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.PurchaseOrderItemStatus", new[] { "ID" });
            DropIndex("CLARITY.AppliedPurchaseOrderItemDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedPurchaseOrderItemDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedPurchaseOrderItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedPurchaseOrderItemDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedPurchaseOrderItemDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedPurchaseOrderItemDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedPurchaseOrderItemDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedPurchaseOrderItemDiscounts", new[] { "ID" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "Hash" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "Active" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "CreatedDate" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "CustomKey" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "Name" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "SellingCurrencyID" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "OriginalCurrencyID" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "UserID" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "ProductID" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "StatusID" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "MasterID" });
            DropIndex("CLARITY.PurchaseOrderItems", new[] { "ID" });
            DropIndex("CLARITY.Wallets", new[] { "Hash" });
            DropIndex("CLARITY.Wallets", new[] { "Active" });
            DropIndex("CLARITY.Wallets", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Wallets", new[] { "CreatedDate" });
            DropIndex("CLARITY.Wallets", new[] { "CustomKey" });
            DropIndex("CLARITY.Wallets", new[] { "Name" });
            DropIndex("CLARITY.Wallets", new[] { "CurrencyID" });
            DropIndex("CLARITY.Wallets", new[] { "UserID" });
            DropIndex("CLARITY.Wallets", new[] { "ID" });
            DropIndex("CLARITY.ProductTypes", new[] { "Hash" });
            DropIndex("CLARITY.ProductTypes", new[] { "Active" });
            DropIndex("CLARITY.ProductTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductTypes", new[] { "Name" });
            DropIndex("CLARITY.ProductTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ProductTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ProductTypes", new[] { "StoreID" });
            DropIndex("CLARITY.ProductTypes", new[] { "ID" });
            DropIndex("CLARITY.UserProductTypes", new[] { "Hash" });
            DropIndex("CLARITY.UserProductTypes", new[] { "Active" });
            DropIndex("CLARITY.UserProductTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UserProductTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.UserProductTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.UserProductTypes", new[] { "SlaveID" });
            DropIndex("CLARITY.UserProductTypes", new[] { "MasterID" });
            DropIndex("CLARITY.UserProductTypes", new[] { "ID" });
            DropIndex("CLARITY.UserOnlineStatus", new[] { "Hash" });
            DropIndex("CLARITY.UserOnlineStatus", new[] { "Active" });
            DropIndex("CLARITY.UserOnlineStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UserOnlineStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.UserOnlineStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.UserOnlineStatus", new[] { "Name" });
            DropIndex("CLARITY.UserOnlineStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.UserOnlineStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.UserOnlineStatus", new[] { "ID" });
            DropIndex("CLARITY.UserEventAttendanceTypes", new[] { "Hash" });
            DropIndex("CLARITY.UserEventAttendanceTypes", new[] { "Active" });
            DropIndex("CLARITY.UserEventAttendanceTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UserEventAttendanceTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.UserEventAttendanceTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.UserEventAttendanceTypes", new[] { "Name" });
            DropIndex("CLARITY.UserEventAttendanceTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.UserEventAttendanceTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.UserEventAttendanceTypes", new[] { "ID" });
            DropIndex("CLARITY.CalendarEventTypes", new[] { "Hash" });
            DropIndex("CLARITY.CalendarEventTypes", new[] { "Active" });
            DropIndex("CLARITY.CalendarEventTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CalendarEventTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CalendarEventTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CalendarEventTypes", new[] { "Name" });
            DropIndex("CLARITY.CalendarEventTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CalendarEventTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CalendarEventTypes", new[] { "ID" });
            DropIndex("CLARITY.CalendarEventFiles", new[] { "Hash" });
            DropIndex("CLARITY.CalendarEventFiles", new[] { "Active" });
            DropIndex("CLARITY.CalendarEventFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CalendarEventFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.CalendarEventFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.CalendarEventFiles", new[] { "Name" });
            DropIndex("CLARITY.CalendarEventFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.CalendarEventFiles", new[] { "MasterID" });
            DropIndex("CLARITY.CalendarEventFiles", new[] { "ID" });
            DropIndex("CLARITY.CalendarEventStatus", new[] { "Hash" });
            DropIndex("CLARITY.CalendarEventStatus", new[] { "Active" });
            DropIndex("CLARITY.CalendarEventStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CalendarEventStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.CalendarEventStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.CalendarEventStatus", new[] { "Name" });
            DropIndex("CLARITY.CalendarEventStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.CalendarEventStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.CalendarEventStatus", new[] { "ID" });
            DropIndex("CLARITY.CalendarEventProducts", new[] { "Hash" });
            DropIndex("CLARITY.CalendarEventProducts", new[] { "Active" });
            DropIndex("CLARITY.CalendarEventProducts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CalendarEventProducts", new[] { "CreatedDate" });
            DropIndex("CLARITY.CalendarEventProducts", new[] { "CustomKey" });
            DropIndex("CLARITY.CalendarEventProducts", new[] { "SlaveID" });
            DropIndex("CLARITY.CalendarEventProducts", new[] { "MasterID" });
            DropIndex("CLARITY.CalendarEventProducts", new[] { "ID" });
            DropIndex("CLARITY.CalendarEventImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.CalendarEventImageTypes", new[] { "Active" });
            DropIndex("CLARITY.CalendarEventImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CalendarEventImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CalendarEventImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CalendarEventImageTypes", new[] { "Name" });
            DropIndex("CLARITY.CalendarEventImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CalendarEventImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CalendarEventImageTypes", new[] { "ID" });
            DropIndex("CLARITY.CalendarEventImages", new[] { "Hash" });
            DropIndex("CLARITY.CalendarEventImages", new[] { "Active" });
            DropIndex("CLARITY.CalendarEventImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CalendarEventImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.CalendarEventImages", new[] { "CustomKey" });
            DropIndex("CLARITY.CalendarEventImages", new[] { "Name" });
            DropIndex("CLARITY.CalendarEventImages", new[] { "TypeID" });
            DropIndex("CLARITY.CalendarEventImages", new[] { "MasterID" });
            DropIndex("CLARITY.CalendarEventImages", new[] { "ID" });
            DropIndex("CLARITY.CalendarEventDetails", new[] { "Hash" });
            DropIndex("CLARITY.CalendarEventDetails", new[] { "Active" });
            DropIndex("CLARITY.CalendarEventDetails", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CalendarEventDetails", new[] { "CreatedDate" });
            DropIndex("CLARITY.CalendarEventDetails", new[] { "CustomKey" });
            DropIndex("CLARITY.CalendarEventDetails", new[] { "Name" });
            DropIndex("CLARITY.CalendarEventDetails", new[] { "CalendarEventID" });
            DropIndex("CLARITY.CalendarEventDetails", new[] { "ID" });
            DropIndex("CLARITY.CalendarEvents", new[] { "Hash" });
            DropIndex("CLARITY.CalendarEvents", new[] { "Active" });
            DropIndex("CLARITY.CalendarEvents", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CalendarEvents", new[] { "CreatedDate" });
            DropIndex("CLARITY.CalendarEvents", new[] { "CustomKey" });
            DropIndex("CLARITY.CalendarEvents", new[] { "Name" });
            DropIndex("CLARITY.CalendarEvents", new[] { "GroupID" });
            DropIndex("CLARITY.CalendarEvents", new[] { "ContactID" });
            DropIndex("CLARITY.CalendarEvents", new[] { "StatusID" });
            DropIndex("CLARITY.CalendarEvents", new[] { "TypeID" });
            DropIndex("CLARITY.CalendarEvents", new[] { "ID" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "Hash" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "Active" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "CreatedDate" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "CustomKey" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "Date" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "SlaveID" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "MasterID" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "TypeID" });
            DropIndex("CLARITY.UserEventAttendances", new[] { "ID" });
            DropIndex("CLARITY.StoreUserTypes", new[] { "Hash" });
            DropIndex("CLARITY.StoreUserTypes", new[] { "Active" });
            DropIndex("CLARITY.StoreUserTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreUserTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreUserTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreUserTypes", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreUserTypes", new[] { "MasterID" });
            DropIndex("CLARITY.StoreUserTypes", new[] { "ID" });
            DropIndex("CLARITY.UserTypes", new[] { "Hash" });
            DropIndex("CLARITY.UserTypes", new[] { "Active" });
            DropIndex("CLARITY.UserTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UserTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.UserTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.UserTypes", new[] { "Name" });
            DropIndex("CLARITY.UserTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.UserTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.UserTypes", new[] { "ID" });
            DropIndex("CLARITY.ProductSubscriptionTypes", new[] { "Hash" });
            DropIndex("CLARITY.ProductSubscriptionTypes", new[] { "Active" });
            DropIndex("CLARITY.ProductSubscriptionTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductSubscriptionTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductSubscriptionTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductSubscriptionTypes", new[] { "SlaveID" });
            DropIndex("CLARITY.ProductSubscriptionTypes", new[] { "MasterID" });
            DropIndex("CLARITY.ProductSubscriptionTypes", new[] { "ID" });
            DropIndex("CLARITY.SubscriptionTypes", new[] { "Hash" });
            DropIndex("CLARITY.SubscriptionTypes", new[] { "Active" });
            DropIndex("CLARITY.SubscriptionTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SubscriptionTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.SubscriptionTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.SubscriptionTypes", new[] { "Name" });
            DropIndex("CLARITY.SubscriptionTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.SubscriptionTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.SubscriptionTypes", new[] { "ID" });
            DropIndex("CLARITY.SubscriptionHistories", new[] { "Hash" });
            DropIndex("CLARITY.SubscriptionHistories", new[] { "Active" });
            DropIndex("CLARITY.SubscriptionHistories", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SubscriptionHistories", new[] { "CreatedDate" });
            DropIndex("CLARITY.SubscriptionHistories", new[] { "CustomKey" });
            DropIndex("CLARITY.SubscriptionHistories", new[] { "SlaveID" });
            DropIndex("CLARITY.SubscriptionHistories", new[] { "MasterID" });
            DropIndex("CLARITY.SubscriptionHistories", new[] { "ID" });
            DropIndex("CLARITY.StoreSubscriptions", new[] { "Hash" });
            DropIndex("CLARITY.StoreSubscriptions", new[] { "Active" });
            DropIndex("CLARITY.StoreSubscriptions", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreSubscriptions", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreSubscriptions", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreSubscriptions", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreSubscriptions", new[] { "MasterID" });
            DropIndex("CLARITY.StoreSubscriptions", new[] { "ID" });
            DropIndex("CLARITY.SubscriptionStatus", new[] { "Hash" });
            DropIndex("CLARITY.SubscriptionStatus", new[] { "Active" });
            DropIndex("CLARITY.SubscriptionStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SubscriptionStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SubscriptionStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SubscriptionStatus", new[] { "Name" });
            DropIndex("CLARITY.SubscriptionStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SubscriptionStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SubscriptionStatus", new[] { "ID" });
            DropIndex("CLARITY.RepeatTypes", new[] { "Hash" });
            DropIndex("CLARITY.RepeatTypes", new[] { "Active" });
            DropIndex("CLARITY.RepeatTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.RepeatTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.RepeatTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.RepeatTypes", new[] { "Name" });
            DropIndex("CLARITY.RepeatTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.RepeatTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.RepeatTypes", new[] { "ID" });
            DropIndex("CLARITY.ZoneTypes", new[] { "Hash" });
            DropIndex("CLARITY.ZoneTypes", new[] { "Active" });
            DropIndex("CLARITY.ZoneTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ZoneTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ZoneTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ZoneTypes", new[] { "Name" });
            DropIndex("CLARITY.ZoneTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ZoneTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ZoneTypes", new[] { "ID" });
            DropIndex("CLARITY.ZoneStatus", new[] { "Hash" });
            DropIndex("CLARITY.ZoneStatus", new[] { "Active" });
            DropIndex("CLARITY.ZoneStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ZoneStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.ZoneStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.ZoneStatus", new[] { "Name" });
            DropIndex("CLARITY.ZoneStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.ZoneStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.ZoneStatus", new[] { "ID" });
            DropIndex("CLARITY.Zones", new[] { "Hash" });
            DropIndex("CLARITY.Zones", new[] { "Active" });
            DropIndex("CLARITY.Zones", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Zones", new[] { "CreatedDate" });
            DropIndex("CLARITY.Zones", new[] { "CustomKey" });
            DropIndex("CLARITY.Zones", new[] { "Name" });
            DropIndex("CLARITY.Zones", new[] { "StatusID" });
            DropIndex("CLARITY.Zones", new[] { "TypeID" });
            DropIndex("CLARITY.Zones", new[] { "ID" });
            DropIndex("CLARITY.AdTypes", new[] { "Hash" });
            DropIndex("CLARITY.AdTypes", new[] { "Active" });
            DropIndex("CLARITY.AdTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AdTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.AdTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.AdTypes", new[] { "Name" });
            DropIndex("CLARITY.AdTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.AdTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.AdTypes", new[] { "ID" });
            DropIndex("CLARITY.AdStores", new[] { "Hash" });
            DropIndex("CLARITY.AdStores", new[] { "Active" });
            DropIndex("CLARITY.AdStores", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AdStores", new[] { "CreatedDate" });
            DropIndex("CLARITY.AdStores", new[] { "CustomKey" });
            DropIndex("CLARITY.AdStores", new[] { "SlaveID" });
            DropIndex("CLARITY.AdStores", new[] { "MasterID" });
            DropIndex("CLARITY.AdStores", new[] { "ID" });
            DropIndex("CLARITY.AdStatus", new[] { "Hash" });
            DropIndex("CLARITY.AdStatus", new[] { "Active" });
            DropIndex("CLARITY.AdStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AdStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.AdStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.AdStatus", new[] { "Name" });
            DropIndex("CLARITY.AdStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.AdStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.AdStatus", new[] { "ID" });
            DropIndex("CLARITY.AdImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.AdImageTypes", new[] { "Active" });
            DropIndex("CLARITY.AdImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AdImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.AdImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.AdImageTypes", new[] { "Name" });
            DropIndex("CLARITY.AdImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.AdImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.AdImageTypes", new[] { "ID" });
            DropIndex("CLARITY.AdImages", new[] { "Hash" });
            DropIndex("CLARITY.AdImages", new[] { "Active" });
            DropIndex("CLARITY.AdImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AdImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.AdImages", new[] { "CustomKey" });
            DropIndex("CLARITY.AdImages", new[] { "Name" });
            DropIndex("CLARITY.AdImages", new[] { "TypeID" });
            DropIndex("CLARITY.AdImages", new[] { "MasterID" });
            DropIndex("CLARITY.AdImages", new[] { "ID" });
            DropIndex("CLARITY.CampaignTypes", new[] { "Hash" });
            DropIndex("CLARITY.CampaignTypes", new[] { "Active" });
            DropIndex("CLARITY.CampaignTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CampaignTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CampaignTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CampaignTypes", new[] { "Name" });
            DropIndex("CLARITY.CampaignTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CampaignTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CampaignTypes", new[] { "StoreID" });
            DropIndex("CLARITY.CampaignTypes", new[] { "ID" });
            DropIndex("CLARITY.CampaignStatus", new[] { "Hash" });
            DropIndex("CLARITY.CampaignStatus", new[] { "Active" });
            DropIndex("CLARITY.CampaignStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CampaignStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.CampaignStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.CampaignStatus", new[] { "Name" });
            DropIndex("CLARITY.CampaignStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.CampaignStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.CampaignStatus", new[] { "ID" });
            DropIndex("CLARITY.Campaigns", new[] { "Hash" });
            DropIndex("CLARITY.Campaigns", new[] { "Active" });
            DropIndex("CLARITY.Campaigns", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Campaigns", new[] { "CreatedDate" });
            DropIndex("CLARITY.Campaigns", new[] { "CustomKey" });
            DropIndex("CLARITY.Campaigns", new[] { "Name" });
            DropIndex("CLARITY.Campaigns", new[] { "CreatedByUserID" });
            DropIndex("CLARITY.Campaigns", new[] { "StatusID" });
            DropIndex("CLARITY.Campaigns", new[] { "TypeID" });
            DropIndex("CLARITY.Campaigns", new[] { "ID" });
            DropIndex("CLARITY.CampaignAds", new[] { "Hash" });
            DropIndex("CLARITY.CampaignAds", new[] { "Active" });
            DropIndex("CLARITY.CampaignAds", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CampaignAds", new[] { "CreatedDate" });
            DropIndex("CLARITY.CampaignAds", new[] { "CustomKey" });
            DropIndex("CLARITY.CampaignAds", new[] { "SlaveID" });
            DropIndex("CLARITY.CampaignAds", new[] { "MasterID" });
            DropIndex("CLARITY.CampaignAds", new[] { "ID" });
            DropIndex("CLARITY.AdAccounts", new[] { "Hash" });
            DropIndex("CLARITY.AdAccounts", new[] { "Active" });
            DropIndex("CLARITY.AdAccounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AdAccounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AdAccounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AdAccounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AdAccounts", new[] { "MasterID" });
            DropIndex("CLARITY.AdAccounts", new[] { "ID" });
            DropIndex("CLARITY.Ads", new[] { "Hash" });
            DropIndex("CLARITY.Ads", new[] { "Active" });
            DropIndex("CLARITY.Ads", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Ads", new[] { "CreatedDate" });
            DropIndex("CLARITY.Ads", new[] { "CustomKey" });
            DropIndex("CLARITY.Ads", new[] { "Name" });
            DropIndex("CLARITY.Ads", new[] { "ClickCounterID" });
            DropIndex("CLARITY.Ads", new[] { "ImpressionCounterID" });
            DropIndex("CLARITY.Ads", new[] { "StatusID" });
            DropIndex("CLARITY.Ads", new[] { "TypeID" });
            DropIndex("CLARITY.Ads", new[] { "ID" });
            DropIndex("CLARITY.CounterTypes", new[] { "Hash" });
            DropIndex("CLARITY.CounterTypes", new[] { "Active" });
            DropIndex("CLARITY.CounterTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CounterTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CounterTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CounterTypes", new[] { "Name" });
            DropIndex("CLARITY.CounterTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CounterTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CounterTypes", new[] { "ID" });
            DropIndex("CLARITY.CounterLogTypes", new[] { "Hash" });
            DropIndex("CLARITY.CounterLogTypes", new[] { "Active" });
            DropIndex("CLARITY.CounterLogTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CounterLogTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CounterLogTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CounterLogTypes", new[] { "Name" });
            DropIndex("CLARITY.CounterLogTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CounterLogTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CounterLogTypes", new[] { "ID" });
            DropIndex("CLARITY.CounterLogs", new[] { "Hash" });
            DropIndex("CLARITY.CounterLogs", new[] { "Active" });
            DropIndex("CLARITY.CounterLogs", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CounterLogs", new[] { "CreatedDate" });
            DropIndex("CLARITY.CounterLogs", new[] { "CustomKey" });
            DropIndex("CLARITY.CounterLogs", new[] { "CounterID" });
            DropIndex("CLARITY.CounterLogs", new[] { "TypeID" });
            DropIndex("CLARITY.CounterLogs", new[] { "ID" });
            DropIndex("CLARITY.Counters", new[] { "Hash" });
            DropIndex("CLARITY.Counters", new[] { "Active" });
            DropIndex("CLARITY.Counters", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Counters", new[] { "CreatedDate" });
            DropIndex("CLARITY.Counters", new[] { "CustomKey" });
            DropIndex("CLARITY.Counters", new[] { "TypeID" });
            DropIndex("CLARITY.Counters", new[] { "ID" });
            DropIndex("CLARITY.AdZones", new[] { "Hash" });
            DropIndex("CLARITY.AdZones", new[] { "Active" });
            DropIndex("CLARITY.AdZones", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AdZones", new[] { "CreatedDate" });
            DropIndex("CLARITY.AdZones", new[] { "CustomKey" });
            DropIndex("CLARITY.AdZones", new[] { "AdZoneAccessID" });
            DropIndex("CLARITY.AdZones", new[] { "ClickCounterID" });
            DropIndex("CLARITY.AdZones", new[] { "ImpressionCounterID" });
            DropIndex("CLARITY.AdZones", new[] { "SlaveID" });
            DropIndex("CLARITY.AdZones", new[] { "MasterID" });
            DropIndex("CLARITY.AdZones", new[] { "ID" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "Hash" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "Active" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "CreatedDate" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "CustomKey" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "Name" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "SubscriptionID" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "ZoneID" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "ClickCounterID" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "ImpressionCounterID" });
            DropIndex("CLARITY.AdZoneAccesses", new[] { "ID" });
            DropIndex("CLARITY.MembershipAdZoneAccesses", new[] { "Hash" });
            DropIndex("CLARITY.MembershipAdZoneAccesses", new[] { "Active" });
            DropIndex("CLARITY.MembershipAdZoneAccesses", new[] { "UpdatedDate" });
            DropIndex("CLARITY.MembershipAdZoneAccesses", new[] { "CreatedDate" });
            DropIndex("CLARITY.MembershipAdZoneAccesses", new[] { "CustomKey" });
            DropIndex("CLARITY.MembershipAdZoneAccesses", new[] { "SlaveID" });
            DropIndex("CLARITY.MembershipAdZoneAccesses", new[] { "MasterID" });
            DropIndex("CLARITY.MembershipAdZoneAccesses", new[] { "ID" });
            DropIndex("CLARITY.MembershipAdZoneAccessByLevels", new[] { "Hash" });
            DropIndex("CLARITY.MembershipAdZoneAccessByLevels", new[] { "Active" });
            DropIndex("CLARITY.MembershipAdZoneAccessByLevels", new[] { "UpdatedDate" });
            DropIndex("CLARITY.MembershipAdZoneAccessByLevels", new[] { "CreatedDate" });
            DropIndex("CLARITY.MembershipAdZoneAccessByLevels", new[] { "CustomKey" });
            DropIndex("CLARITY.MembershipAdZoneAccessByLevels", new[] { "SlaveID" });
            DropIndex("CLARITY.MembershipAdZoneAccessByLevels", new[] { "MasterID" });
            DropIndex("CLARITY.MembershipAdZoneAccessByLevels", new[] { "ID" });
            DropIndex("CLARITY.MembershipLevels", new[] { "Hash" });
            DropIndex("CLARITY.MembershipLevels", new[] { "Active" });
            DropIndex("CLARITY.MembershipLevels", new[] { "UpdatedDate" });
            DropIndex("CLARITY.MembershipLevels", new[] { "CreatedDate" });
            DropIndex("CLARITY.MembershipLevels", new[] { "CustomKey" });
            DropIndex("CLARITY.MembershipLevels", new[] { "Name" });
            DropIndex("CLARITY.MembershipLevels", new[] { "SortOrder" });
            DropIndex("CLARITY.MembershipLevels", new[] { "DisplayName" });
            DropIndex("CLARITY.MembershipLevels", new[] { "MembershipID" });
            DropIndex("CLARITY.MembershipLevels", new[] { "ID" });
            DropIndex("CLARITY.Memberships", new[] { "Hash" });
            DropIndex("CLARITY.Memberships", new[] { "Active" });
            DropIndex("CLARITY.Memberships", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Memberships", new[] { "CreatedDate" });
            DropIndex("CLARITY.Memberships", new[] { "CustomKey" });
            DropIndex("CLARITY.Memberships", new[] { "Name" });
            DropIndex("CLARITY.Memberships", new[] { "SortOrder" });
            DropIndex("CLARITY.Memberships", new[] { "DisplayName" });
            DropIndex("CLARITY.Memberships", new[] { "ID" });
            DropIndex("CLARITY.MembershipRepeatTypes", new[] { "Hash" });
            DropIndex("CLARITY.MembershipRepeatTypes", new[] { "Active" });
            DropIndex("CLARITY.MembershipRepeatTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.MembershipRepeatTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.MembershipRepeatTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.MembershipRepeatTypes", new[] { "SlaveID" });
            DropIndex("CLARITY.MembershipRepeatTypes", new[] { "MasterID" });
            DropIndex("CLARITY.MembershipRepeatTypes", new[] { "ID" });
            DropIndex("CLARITY.ProductMembershipLevels", new[] { "Hash" });
            DropIndex("CLARITY.ProductMembershipLevels", new[] { "Active" });
            DropIndex("CLARITY.ProductMembershipLevels", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductMembershipLevels", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductMembershipLevels", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductMembershipLevels", new[] { "MembershipRepeatTypeID" });
            DropIndex("CLARITY.ProductMembershipLevels", new[] { "SlaveID" });
            DropIndex("CLARITY.ProductMembershipLevels", new[] { "MasterID" });
            DropIndex("CLARITY.ProductMembershipLevels", new[] { "ID" });
            DropIndex("CLARITY.Subscriptions", new[] { "Hash" });
            DropIndex("CLARITY.Subscriptions", new[] { "Active" });
            DropIndex("CLARITY.Subscriptions", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Subscriptions", new[] { "CreatedDate" });
            DropIndex("CLARITY.Subscriptions", new[] { "CustomKey" });
            DropIndex("CLARITY.Subscriptions", new[] { "Name" });
            DropIndex("CLARITY.Subscriptions", new[] { "AccountID" });
            DropIndex("CLARITY.Subscriptions", new[] { "UserID" });
            DropIndex("CLARITY.Subscriptions", new[] { "SalesInvoiceID" });
            DropIndex("CLARITY.Subscriptions", new[] { "RepeatTypeID" });
            DropIndex("CLARITY.Subscriptions", new[] { "ProductMembershipLevelID" });
            DropIndex("CLARITY.Subscriptions", new[] { "StatusID" });
            DropIndex("CLARITY.Subscriptions", new[] { "TypeID" });
            DropIndex("CLARITY.Subscriptions", new[] { "ID" });
            DropIndex("CLARITY.StoreUsers", new[] { "Hash" });
            DropIndex("CLARITY.StoreUsers", new[] { "Active" });
            DropIndex("CLARITY.StoreUsers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreUsers", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreUsers", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreUsers", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreUsers", new[] { "MasterID" });
            DropIndex("CLARITY.StoreUsers", new[] { "ID" });
            DropIndex("CLARITY.UserFiles", new[] { "Hash" });
            DropIndex("CLARITY.UserFiles", new[] { "Active" });
            DropIndex("CLARITY.UserFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UserFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.UserFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.UserFiles", new[] { "Name" });
            DropIndex("CLARITY.UserFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.UserFiles", new[] { "MasterID" });
            DropIndex("CLARITY.UserFiles", new[] { "ID" });
            DropIndex("CLARITY.UserStatus", new[] { "Hash" });
            DropIndex("CLARITY.UserStatus", new[] { "Active" });
            DropIndex("CLARITY.UserStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UserStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.UserStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.UserStatus", new[] { "Name" });
            DropIndex("CLARITY.UserStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.UserStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.UserStatus", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteTypes", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteTypes", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteTypes", new[] { "Name" });
            DropIndex("CLARITY.SalesQuoteTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesQuoteTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesQuoteTypes", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteFiles", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteFiles", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteFiles", new[] { "Name" });
            DropIndex("CLARITY.SalesQuoteFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesQuoteFiles", new[] { "MasterID" });
            DropIndex("CLARITY.SalesQuoteFiles", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteStatus", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteStatus", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteStatus", new[] { "Name" });
            DropIndex("CLARITY.SalesQuoteStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesQuoteStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesQuoteStatus", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteStates", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteStates", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteStates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteStates", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteStates", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteStates", new[] { "Name" });
            DropIndex("CLARITY.SalesQuoteStates", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesQuoteStates", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesQuoteStates", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteCategories", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteCategories", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteCategories", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteCategories", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteCategories", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteCategories", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesQuoteCategories", new[] { "MasterID" });
            DropIndex("CLARITY.SalesQuoteCategories", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "SelectedRateQuoteID" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "OriginVendorProductID" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "OriginStoreProductID" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "DestinationContactID" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "TypeID" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "MasterID" });
            DropIndex("CLARITY.SalesQuoteItemTargets", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteItemStatus", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteItemStatus", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteItemStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteItemStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteItemStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteItemStatus", new[] { "Name" });
            DropIndex("CLARITY.SalesQuoteItemStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesQuoteItemStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesQuoteItemStatus", new[] { "ID" });
            DropIndex("CLARITY.AppliedSalesQuoteItemDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSalesQuoteItemDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSalesQuoteItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSalesQuoteItemDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSalesQuoteItemDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSalesQuoteItemDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSalesQuoteItemDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSalesQuoteItemDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "Name" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "SellingCurrencyID" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "OriginalCurrencyID" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "UserID" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "ProductID" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "StatusID" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "MasterID" });
            DropIndex("CLARITY.SalesQuoteItems", new[] { "ID" });
            DropIndex("CLARITY.SampleRequestTypes", new[] { "Hash" });
            DropIndex("CLARITY.SampleRequestTypes", new[] { "Active" });
            DropIndex("CLARITY.SampleRequestTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SampleRequestTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.SampleRequestTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.SampleRequestTypes", new[] { "Name" });
            DropIndex("CLARITY.SampleRequestTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.SampleRequestTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.SampleRequestTypes", new[] { "ID" });
            DropIndex("CLARITY.SampleRequestFiles", new[] { "Hash" });
            DropIndex("CLARITY.SampleRequestFiles", new[] { "Active" });
            DropIndex("CLARITY.SampleRequestFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SampleRequestFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.SampleRequestFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.SampleRequestFiles", new[] { "Name" });
            DropIndex("CLARITY.SampleRequestFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.SampleRequestFiles", new[] { "MasterID" });
            DropIndex("CLARITY.SampleRequestFiles", new[] { "ID" });
            DropIndex("CLARITY.SampleRequestStatus", new[] { "Hash" });
            DropIndex("CLARITY.SampleRequestStatus", new[] { "Active" });
            DropIndex("CLARITY.SampleRequestStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SampleRequestStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SampleRequestStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SampleRequestStatus", new[] { "Name" });
            DropIndex("CLARITY.SampleRequestStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SampleRequestStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SampleRequestStatus", new[] { "ID" });
            DropIndex("CLARITY.SampleRequestStates", new[] { "Hash" });
            DropIndex("CLARITY.SampleRequestStates", new[] { "Active" });
            DropIndex("CLARITY.SampleRequestStates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SampleRequestStates", new[] { "CreatedDate" });
            DropIndex("CLARITY.SampleRequestStates", new[] { "CustomKey" });
            DropIndex("CLARITY.SampleRequestStates", new[] { "Name" });
            DropIndex("CLARITY.SampleRequestStates", new[] { "SortOrder" });
            DropIndex("CLARITY.SampleRequestStates", new[] { "DisplayName" });
            DropIndex("CLARITY.SampleRequestStates", new[] { "ID" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "Hash" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "Active" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "CreatedDate" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "CustomKey" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "SelectedRateQuoteID" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "OriginVendorProductID" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "OriginStoreProductID" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "DestinationContactID" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "TypeID" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "MasterID" });
            DropIndex("CLARITY.SampleRequestItemTargets", new[] { "ID" });
            DropIndex("CLARITY.SampleRequestItemStatus", new[] { "Hash" });
            DropIndex("CLARITY.SampleRequestItemStatus", new[] { "Active" });
            DropIndex("CLARITY.SampleRequestItemStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SampleRequestItemStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SampleRequestItemStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SampleRequestItemStatus", new[] { "Name" });
            DropIndex("CLARITY.SampleRequestItemStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SampleRequestItemStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SampleRequestItemStatus", new[] { "ID" });
            DropIndex("CLARITY.AppliedSampleRequestItemDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSampleRequestItemDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSampleRequestItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSampleRequestItemDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSampleRequestItemDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSampleRequestItemDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSampleRequestItemDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSampleRequestItemDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "Hash" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "Active" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "CreatedDate" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "CustomKey" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "Name" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "SellingCurrencyID" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "OriginalCurrencyID" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "UserID" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "ProductID" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "StatusID" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "MasterID" });
            DropIndex("CLARITY.SampleRequestItems", new[] { "ID" });
            DropIndex("CLARITY.AppliedSampleRequestDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSampleRequestDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSampleRequestDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSampleRequestDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSampleRequestDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSampleRequestDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSampleRequestDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSampleRequestDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SampleRequestContacts", new[] { "Hash" });
            DropIndex("CLARITY.SampleRequestContacts", new[] { "Active" });
            DropIndex("CLARITY.SampleRequestContacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SampleRequestContacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.SampleRequestContacts", new[] { "CustomKey" });
            DropIndex("CLARITY.SampleRequestContacts", new[] { "SlaveID" });
            DropIndex("CLARITY.SampleRequestContacts", new[] { "MasterID" });
            DropIndex("CLARITY.SampleRequestContacts", new[] { "ID" });
            DropIndex("CLARITY.SampleRequests", new[] { "Hash" });
            DropIndex("CLARITY.SampleRequests", new[] { "Active" });
            DropIndex("CLARITY.SampleRequests", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SampleRequests", new[] { "CreatedDate" });
            DropIndex("CLARITY.SampleRequests", new[] { "CustomKey" });
            DropIndex("CLARITY.SampleRequests", new[] { "ParentID" });
            DropIndex("CLARITY.SampleRequests", new[] { "StoreID" });
            DropIndex("CLARITY.SampleRequests", new[] { "AccountID" });
            DropIndex("CLARITY.SampleRequests", new[] { "UserID" });
            DropIndex("CLARITY.SampleRequests", new[] { "TypeID" });
            DropIndex("CLARITY.SampleRequests", new[] { "StateID" });
            DropIndex("CLARITY.SampleRequests", new[] { "StatusID" });
            DropIndex("CLARITY.SampleRequests", new[] { "ShippingContactID" });
            DropIndex("CLARITY.SampleRequests", new[] { "BillingContactID" });
            DropIndex("CLARITY.SampleRequests", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnTypes", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnTypes", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnTypes", new[] { "Name" });
            DropIndex("CLARITY.SalesReturnTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesReturnTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesReturnTypes", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnFiles", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnFiles", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnFiles", new[] { "Name" });
            DropIndex("CLARITY.SalesReturnFiles", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesReturnFiles", new[] { "MasterID" });
            DropIndex("CLARITY.SalesReturnFiles", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnStatus", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnStatus", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnStatus", new[] { "Name" });
            DropIndex("CLARITY.SalesReturnStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesReturnStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesReturnStatus", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnStates", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnStates", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnStates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnStates", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnStates", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnStates", new[] { "Name" });
            DropIndex("CLARITY.SalesReturnStates", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesReturnStates", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesReturnStates", new[] { "ID" });
            DropIndex("CLARITY.PaymentTypes", new[] { "Hash" });
            DropIndex("CLARITY.PaymentTypes", new[] { "Active" });
            DropIndex("CLARITY.PaymentTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PaymentTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.PaymentTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.PaymentTypes", new[] { "Name" });
            DropIndex("CLARITY.PaymentTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.PaymentTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.PaymentTypes", new[] { "ID" });
            DropIndex("CLARITY.PaymentStatus", new[] { "Hash" });
            DropIndex("CLARITY.PaymentStatus", new[] { "Active" });
            DropIndex("CLARITY.PaymentStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PaymentStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.PaymentStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.PaymentStatus", new[] { "Name" });
            DropIndex("CLARITY.PaymentStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.PaymentStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.PaymentStatus", new[] { "ID" });
            DropIndex("CLARITY.PaymentMethods", new[] { "Hash" });
            DropIndex("CLARITY.PaymentMethods", new[] { "Active" });
            DropIndex("CLARITY.PaymentMethods", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PaymentMethods", new[] { "CreatedDate" });
            DropIndex("CLARITY.PaymentMethods", new[] { "CustomKey" });
            DropIndex("CLARITY.PaymentMethods", new[] { "Name" });
            DropIndex("CLARITY.PaymentMethods", new[] { "ID" });
            DropIndex("CLARITY.Payments", new[] { "Hash" });
            DropIndex("CLARITY.Payments", new[] { "Active" });
            DropIndex("CLARITY.Payments", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Payments", new[] { "CreatedDate" });
            DropIndex("CLARITY.Payments", new[] { "CustomKey" });
            DropIndex("CLARITY.Payments", new[] { "CurrencyID" });
            DropIndex("CLARITY.Payments", new[] { "PaymentMethodID" });
            DropIndex("CLARITY.Payments", new[] { "BillingContactID" });
            DropIndex("CLARITY.Payments", new[] { "StatusID" });
            DropIndex("CLARITY.Payments", new[] { "TypeID" });
            DropIndex("CLARITY.Payments", new[] { "StoreID" });
            DropIndex("CLARITY.Payments", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnPayments", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnPayments", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnPayments", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnPayments", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnPayments", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnPayments", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesReturnPayments", new[] { "MasterID" });
            DropIndex("CLARITY.SalesReturnPayments", new[] { "ID" });
            DropIndex("CLARITY.SalesItemTargetTypes", new[] { "Hash" });
            DropIndex("CLARITY.SalesItemTargetTypes", new[] { "Active" });
            DropIndex("CLARITY.SalesItemTargetTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesItemTargetTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesItemTargetTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesItemTargetTypes", new[] { "Name" });
            DropIndex("CLARITY.SalesItemTargetTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesItemTargetTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesItemTargetTypes", new[] { "ID" });
            DropIndex("CLARITY.StoreProducts", new[] { "Hash" });
            DropIndex("CLARITY.StoreProducts", new[] { "Active" });
            DropIndex("CLARITY.StoreProducts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreProducts", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreProducts", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreProducts", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreProducts", new[] { "MasterID" });
            DropIndex("CLARITY.StoreProducts", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "SelectedRateQuoteID" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "OriginVendorProductID" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "OriginStoreProductID" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "OriginProductInventoryLocationSectionID" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "DestinationContactID" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "TypeID" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "MasterID" });
            DropIndex("CLARITY.SalesReturnItemTargets", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnItemStatus", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnItemStatus", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnItemStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnItemStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnItemStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnItemStatus", new[] { "Name" });
            DropIndex("CLARITY.SalesReturnItemStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesReturnItemStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesReturnItemStatus", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "Name" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "SortOrder" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "DisplayName" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "RestockingFeeAmountCurrencyID" });
            DropIndex("CLARITY.SalesReturnReasons", new[] { "ID" });
            DropIndex("CLARITY.AppliedSalesReturnItemDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSalesReturnItemDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSalesReturnItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSalesReturnItemDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSalesReturnItemDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSalesReturnItemDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSalesReturnItemDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSalesReturnItemDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "Name" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "SalesReturnReasonID" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "SellingCurrencyID" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "OriginalCurrencyID" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "UserID" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "ProductID" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "StatusID" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "MasterID" });
            DropIndex("CLARITY.SalesReturnItems", new[] { "ID" });
            DropIndex("CLARITY.SalesGroups", new[] { "Hash" });
            DropIndex("CLARITY.SalesGroups", new[] { "Active" });
            DropIndex("CLARITY.SalesGroups", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesGroups", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesGroups", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesGroups", new[] { "BillingContactID" });
            DropIndex("CLARITY.SalesGroups", new[] { "AccountID" });
            DropIndex("CLARITY.SalesGroups", new[] { "ID" });
            DropIndex("CLARITY.AppliedSalesReturnDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSalesReturnDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSalesReturnDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSalesReturnDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSalesReturnDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSalesReturnDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSalesReturnDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSalesReturnDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnContacts", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnContacts", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnContacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnContacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnContacts", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnContacts", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesReturnContacts", new[] { "MasterID" });
            DropIndex("CLARITY.SalesReturnContacts", new[] { "ID" });
            DropIndex("CLARITY.SalesReturnSalesOrders", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturnSalesOrders", new[] { "Active" });
            DropIndex("CLARITY.SalesReturnSalesOrders", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturnSalesOrders", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturnSalesOrders", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturnSalesOrders", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesReturnSalesOrders", new[] { "MasterID" });
            DropIndex("CLARITY.SalesReturnSalesOrders", new[] { "ID" });
            DropIndex("CLARITY.SalesReturns", new[] { "Hash" });
            DropIndex("CLARITY.SalesReturns", new[] { "Active" });
            DropIndex("CLARITY.SalesReturns", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesReturns", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesReturns", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesReturns", new[] { "SalesGroupID" });
            DropIndex("CLARITY.SalesReturns", new[] { "ParentID" });
            DropIndex("CLARITY.SalesReturns", new[] { "StoreID" });
            DropIndex("CLARITY.SalesReturns", new[] { "AccountID" });
            DropIndex("CLARITY.SalesReturns", new[] { "UserID" });
            DropIndex("CLARITY.SalesReturns", new[] { "TypeID" });
            DropIndex("CLARITY.SalesReturns", new[] { "StateID" });
            DropIndex("CLARITY.SalesReturns", new[] { "StatusID" });
            DropIndex("CLARITY.SalesReturns", new[] { "ShippingContactID" });
            DropIndex("CLARITY.SalesReturns", new[] { "BillingContactID" });
            DropIndex("CLARITY.SalesReturns", new[] { "ID" });
            DropIndex("CLARITY.RateQuotes", new[] { "Hash" });
            DropIndex("CLARITY.RateQuotes", new[] { "Active" });
            DropIndex("CLARITY.RateQuotes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.RateQuotes", new[] { "CreatedDate" });
            DropIndex("CLARITY.RateQuotes", new[] { "CustomKey" });
            DropIndex("CLARITY.RateQuotes", new[] { "Name" });
            DropIndex("CLARITY.RateQuotes", new[] { "SalesReturnID" });
            DropIndex("CLARITY.RateQuotes", new[] { "SalesInvoiceID" });
            DropIndex("CLARITY.RateQuotes", new[] { "PurchaseOrderID" });
            DropIndex("CLARITY.RateQuotes", new[] { "SalesOrderID" });
            DropIndex("CLARITY.RateQuotes", new[] { "SalesQuoteID" });
            DropIndex("CLARITY.RateQuotes", new[] { "SampleRequestID" });
            DropIndex("CLARITY.RateQuotes", new[] { "CartID" });
            DropIndex("CLARITY.RateQuotes", new[] { "ShipCarrierMethodID" });
            DropIndex("CLARITY.RateQuotes", new[] { "ID" });
            DropIndex("CLARITY.AppliedSalesQuoteDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSalesQuoteDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSalesQuoteDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSalesQuoteDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSalesQuoteDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSalesQuoteDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSalesQuoteDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSalesQuoteDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteContacts", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteContacts", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteContacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteContacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteContacts", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteContacts", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesQuoteContacts", new[] { "MasterID" });
            DropIndex("CLARITY.SalesQuoteContacts", new[] { "ID" });
            DropIndex("CLARITY.SalesQuoteSalesOrders", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuoteSalesOrders", new[] { "Active" });
            DropIndex("CLARITY.SalesQuoteSalesOrders", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuoteSalesOrders", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuoteSalesOrders", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuoteSalesOrders", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesQuoteSalesOrders", new[] { "MasterID" });
            DropIndex("CLARITY.SalesQuoteSalesOrders", new[] { "ID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "Hash" });
            DropIndex("CLARITY.SalesQuotes", new[] { "Active" });
            DropIndex("CLARITY.SalesQuotes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesQuotes", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesQuotes", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesQuotes", new[] { "SalesGroupAsResponseID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "SalesGroupAsMasterID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "ResponseAsStoreID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "ResponseAsVendorID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "ParentID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "StoreID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "AccountID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "UserID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "TypeID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "StateID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "StatusID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "ShippingContactID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "BillingContactID" });
            DropIndex("CLARITY.SalesQuotes", new[] { "ID" });
            DropIndex("CLARITY.Permissions", new[] { "Id" });
            DropIndex("CLARITY.RolePermissions", new[] { "PermissionId" });
            DropIndex("CLARITY.RolePermissions", new[] { "RoleId" });
            DropIndex("CLARITY.RoleUsers", new[] { "GroupID" });
            DropIndex("CLARITY.RoleUsers", new[] { "UserId" });
            DropIndex("CLARITY.RoleUsers", new[] { "RoleId" });
            DropIndex("CLARITY.ReferralCodeTypes", new[] { "Hash" });
            DropIndex("CLARITY.ReferralCodeTypes", new[] { "Active" });
            DropIndex("CLARITY.ReferralCodeTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ReferralCodeTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ReferralCodeTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ReferralCodeTypes", new[] { "Name" });
            DropIndex("CLARITY.ReferralCodeTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ReferralCodeTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ReferralCodeTypes", new[] { "ID" });
            DropIndex("CLARITY.ReferralCodeStatus", new[] { "Hash" });
            DropIndex("CLARITY.ReferralCodeStatus", new[] { "Active" });
            DropIndex("CLARITY.ReferralCodeStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ReferralCodeStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.ReferralCodeStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.ReferralCodeStatus", new[] { "Name" });
            DropIndex("CLARITY.ReferralCodeStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.ReferralCodeStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.ReferralCodeStatus", new[] { "ID" });
            DropIndex("CLARITY.ReferralCodes", new[] { "Hash" });
            DropIndex("CLARITY.ReferralCodes", new[] { "Active" });
            DropIndex("CLARITY.ReferralCodes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ReferralCodes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ReferralCodes", new[] { "CustomKey" });
            DropIndex("CLARITY.ReferralCodes", new[] { "Name" });
            DropIndex("CLARITY.ReferralCodes", new[] { "UserID" });
            DropIndex("CLARITY.ReferralCodes", new[] { "StatusID" });
            DropIndex("CLARITY.ReferralCodes", new[] { "TypeID" });
            DropIndex("CLARITY.ReferralCodes", new[] { "ID" });
            DropIndex("CLARITY.UserLogins", new[] { "UserId" });
            DropIndex("CLARITY.UserImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.UserImageTypes", new[] { "Active" });
            DropIndex("CLARITY.UserImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UserImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.UserImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.UserImageTypes", new[] { "Name" });
            DropIndex("CLARITY.UserImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.UserImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.UserImageTypes", new[] { "ID" });
            DropIndex("CLARITY.UserImages", new[] { "Hash" });
            DropIndex("CLARITY.UserImages", new[] { "Active" });
            DropIndex("CLARITY.UserImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.UserImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.UserImages", new[] { "CustomKey" });
            DropIndex("CLARITY.UserImages", new[] { "Name" });
            DropIndex("CLARITY.UserImages", new[] { "TypeID" });
            DropIndex("CLARITY.UserImages", new[] { "MasterID" });
            DropIndex("CLARITY.UserImages", new[] { "ID" });
            DropIndex("CLARITY.VendorTypes", new[] { "Hash" });
            DropIndex("CLARITY.VendorTypes", new[] { "Active" });
            DropIndex("CLARITY.VendorTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.VendorTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.VendorTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.VendorTypes", new[] { "Name" });
            DropIndex("CLARITY.VendorTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.VendorTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.VendorTypes", new[] { "ID" });
            DropIndex("CLARITY.StoreVendors", new[] { "Hash" });
            DropIndex("CLARITY.StoreVendors", new[] { "Active" });
            DropIndex("CLARITY.StoreVendors", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreVendors", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreVendors", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreVendors", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreVendors", new[] { "MasterID" });
            DropIndex("CLARITY.StoreVendors", new[] { "ID" });
            DropIndex("CLARITY.ShipmentTypes", new[] { "Hash" });
            DropIndex("CLARITY.ShipmentTypes", new[] { "Active" });
            DropIndex("CLARITY.ShipmentTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ShipmentTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ShipmentTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ShipmentTypes", new[] { "Name" });
            DropIndex("CLARITY.ShipmentTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ShipmentTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ShipmentTypes", new[] { "ID" });
            DropIndex("CLARITY.ShipmentStatus", new[] { "Hash" });
            DropIndex("CLARITY.ShipmentStatus", new[] { "Active" });
            DropIndex("CLARITY.ShipmentStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ShipmentStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.ShipmentStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.ShipmentStatus", new[] { "Name" });
            DropIndex("CLARITY.ShipmentStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.ShipmentStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.ShipmentStatus", new[] { "ID" });
            DropIndex("CLARITY.ShipmentEvents", new[] { "Hash" });
            DropIndex("CLARITY.ShipmentEvents", new[] { "Active" });
            DropIndex("CLARITY.ShipmentEvents", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ShipmentEvents", new[] { "CreatedDate" });
            DropIndex("CLARITY.ShipmentEvents", new[] { "CustomKey" });
            DropIndex("CLARITY.ShipmentEvents", new[] { "ShipmentID" });
            DropIndex("CLARITY.ShipmentEvents", new[] { "AddressID" });
            DropIndex("CLARITY.ShipmentEvents", new[] { "ID" });
            DropIndex("CLARITY.ShipCarrierMethods", new[] { "Hash" });
            DropIndex("CLARITY.ShipCarrierMethods", new[] { "Active" });
            DropIndex("CLARITY.ShipCarrierMethods", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ShipCarrierMethods", new[] { "CreatedDate" });
            DropIndex("CLARITY.ShipCarrierMethods", new[] { "CustomKey" });
            DropIndex("CLARITY.ShipCarrierMethods", new[] { "Name" });
            DropIndex("CLARITY.ShipCarrierMethods", new[] { "ShipCarrierID" });
            DropIndex("CLARITY.ShipCarrierMethods", new[] { "ID" });
            DropIndex("CLARITY.CarrierOrigins", new[] { "Hash" });
            DropIndex("CLARITY.CarrierOrigins", new[] { "Active" });
            DropIndex("CLARITY.CarrierOrigins", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CarrierOrigins", new[] { "CreatedDate" });
            DropIndex("CLARITY.CarrierOrigins", new[] { "CustomKey" });
            DropIndex("CLARITY.CarrierOrigins", new[] { "ShipCarrierID" });
            DropIndex("CLARITY.CarrierOrigins", new[] { "ID" });
            DropIndex("CLARITY.CarrierInvoices", new[] { "Hash" });
            DropIndex("CLARITY.CarrierInvoices", new[] { "Active" });
            DropIndex("CLARITY.CarrierInvoices", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CarrierInvoices", new[] { "CreatedDate" });
            DropIndex("CLARITY.CarrierInvoices", new[] { "CustomKey" });
            DropIndex("CLARITY.CarrierInvoices", new[] { "ShipCarrierID" });
            DropIndex("CLARITY.CarrierInvoices", new[] { "ID" });
            DropIndex("CLARITY.ShipCarriers", new[] { "Hash" });
            DropIndex("CLARITY.ShipCarriers", new[] { "Active" });
            DropIndex("CLARITY.ShipCarriers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ShipCarriers", new[] { "CreatedDate" });
            DropIndex("CLARITY.ShipCarriers", new[] { "CustomKey" });
            DropIndex("CLARITY.ShipCarriers", new[] { "Name" });
            DropIndex("CLARITY.ShipCarriers", new[] { "ContactID" });
            DropIndex("CLARITY.ShipCarriers", new[] { "ID" });
            DropIndex("CLARITY.ProductInventoryLocationSections", new[] { "Hash" });
            DropIndex("CLARITY.ProductInventoryLocationSections", new[] { "Active" });
            DropIndex("CLARITY.ProductInventoryLocationSections", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ProductInventoryLocationSections", new[] { "CreatedDate" });
            DropIndex("CLARITY.ProductInventoryLocationSections", new[] { "CustomKey" });
            DropIndex("CLARITY.ProductInventoryLocationSections", new[] { "SlaveID" });
            DropIndex("CLARITY.ProductInventoryLocationSections", new[] { "MasterID" });
            DropIndex("CLARITY.ProductInventoryLocationSections", new[] { "ID" });
            DropIndex("CLARITY.StoreInventoryLocationTypes", new[] { "Hash" });
            DropIndex("CLARITY.StoreInventoryLocationTypes", new[] { "Active" });
            DropIndex("CLARITY.StoreInventoryLocationTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreInventoryLocationTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreInventoryLocationTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreInventoryLocationTypes", new[] { "Name" });
            DropIndex("CLARITY.StoreInventoryLocationTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.StoreInventoryLocationTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.StoreInventoryLocationTypes", new[] { "ID" });
            DropIndex("CLARITY.StoreInventoryLocations", new[] { "Hash" });
            DropIndex("CLARITY.StoreInventoryLocations", new[] { "Active" });
            DropIndex("CLARITY.StoreInventoryLocations", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreInventoryLocations", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreInventoryLocations", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreInventoryLocations", new[] { "TypeID" });
            DropIndex("CLARITY.StoreInventoryLocations", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreInventoryLocations", new[] { "MasterID" });
            DropIndex("CLARITY.StoreInventoryLocations", new[] { "ID" });
            DropIndex("CLARITY.InventoryLocations", new[] { "Hash" });
            DropIndex("CLARITY.InventoryLocations", new[] { "Active" });
            DropIndex("CLARITY.InventoryLocations", new[] { "UpdatedDate" });
            DropIndex("CLARITY.InventoryLocations", new[] { "CreatedDate" });
            DropIndex("CLARITY.InventoryLocations", new[] { "CustomKey" });
            DropIndex("CLARITY.InventoryLocations", new[] { "Name" });
            DropIndex("CLARITY.InventoryLocations", new[] { "ContactID" });
            DropIndex("CLARITY.InventoryLocations", new[] { "ID" });
            DropIndex("CLARITY.InventoryLocationSections", new[] { "Hash" });
            DropIndex("CLARITY.InventoryLocationSections", new[] { "Active" });
            DropIndex("CLARITY.InventoryLocationSections", new[] { "UpdatedDate" });
            DropIndex("CLARITY.InventoryLocationSections", new[] { "CreatedDate" });
            DropIndex("CLARITY.InventoryLocationSections", new[] { "CustomKey" });
            DropIndex("CLARITY.InventoryLocationSections", new[] { "Name" });
            DropIndex("CLARITY.InventoryLocationSections", new[] { "InventoryLocationID" });
            DropIndex("CLARITY.InventoryLocationSections", new[] { "ID" });
            DropIndex("CLARITY.Shipments", new[] { "Hash" });
            DropIndex("CLARITY.Shipments", new[] { "Active" });
            DropIndex("CLARITY.Shipments", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Shipments", new[] { "CreatedDate" });
            DropIndex("CLARITY.Shipments", new[] { "CustomKey" });
            DropIndex("CLARITY.Shipments", new[] { "VendorID" });
            DropIndex("CLARITY.Shipments", new[] { "ShipCarrierMethodID" });
            DropIndex("CLARITY.Shipments", new[] { "ShipCarrierID" });
            DropIndex("CLARITY.Shipments", new[] { "InventoryLocationSectionID" });
            DropIndex("CLARITY.Shipments", new[] { "DestinationContactID" });
            DropIndex("CLARITY.Shipments", new[] { "OriginContactID" });
            DropIndex("CLARITY.Shipments", new[] { "StatusID" });
            DropIndex("CLARITY.Shipments", new[] { "TypeID" });
            DropIndex("CLARITY.Shipments", new[] { "ID" });
            DropIndex("CLARITY.ReviewTypes", new[] { "Hash" });
            DropIndex("CLARITY.ReviewTypes", new[] { "Active" });
            DropIndex("CLARITY.ReviewTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ReviewTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ReviewTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ReviewTypes", new[] { "Name" });
            DropIndex("CLARITY.ReviewTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ReviewTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ReviewTypes", new[] { "ID" });
            DropIndex("CLARITY.Reviews", new[] { "Hash" });
            DropIndex("CLARITY.Reviews", new[] { "Active" });
            DropIndex("CLARITY.Reviews", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Reviews", new[] { "CreatedDate" });
            DropIndex("CLARITY.Reviews", new[] { "CustomKey" });
            DropIndex("CLARITY.Reviews", new[] { "Name" });
            DropIndex("CLARITY.Reviews", new[] { "VendorID" });
            DropIndex("CLARITY.Reviews", new[] { "UserID" });
            DropIndex("CLARITY.Reviews", new[] { "StoreID" });
            DropIndex("CLARITY.Reviews", new[] { "ProductID" });
            DropIndex("CLARITY.Reviews", new[] { "ManufacturerID" });
            DropIndex("CLARITY.Reviews", new[] { "CategoryID" });
            DropIndex("CLARITY.Reviews", new[] { "ApprovedByUserID" });
            DropIndex("CLARITY.Reviews", new[] { "SubmittedByUserID" });
            DropIndex("CLARITY.Reviews", new[] { "TypeID" });
            DropIndex("CLARITY.Reviews", new[] { "ID" });
            DropIndex("CLARITY.VendorProducts", new[] { "Hash" });
            DropIndex("CLARITY.VendorProducts", new[] { "Active" });
            DropIndex("CLARITY.VendorProducts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.VendorProducts", new[] { "CreatedDate" });
            DropIndex("CLARITY.VendorProducts", new[] { "CustomKey" });
            DropIndex("CLARITY.VendorProducts", new[] { "SlaveID" });
            DropIndex("CLARITY.VendorProducts", new[] { "MasterID" });
            DropIndex("CLARITY.VendorProducts", new[] { "ID" });
            DropIndex("CLARITY.VendorManufacturers", new[] { "Hash" });
            DropIndex("CLARITY.VendorManufacturers", new[] { "Active" });
            DropIndex("CLARITY.VendorManufacturers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.VendorManufacturers", new[] { "CreatedDate" });
            DropIndex("CLARITY.VendorManufacturers", new[] { "CustomKey" });
            DropIndex("CLARITY.VendorManufacturers", new[] { "SlaveID" });
            DropIndex("CLARITY.VendorManufacturers", new[] { "MasterID" });
            DropIndex("CLARITY.VendorManufacturers", new[] { "ID" });
            DropIndex("CLARITY.VendorImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.VendorImageTypes", new[] { "Active" });
            DropIndex("CLARITY.VendorImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.VendorImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.VendorImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.VendorImageTypes", new[] { "Name" });
            DropIndex("CLARITY.VendorImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.VendorImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.VendorImageTypes", new[] { "ID" });
            DropIndex("CLARITY.VendorImages", new[] { "Hash" });
            DropIndex("CLARITY.VendorImages", new[] { "Active" });
            DropIndex("CLARITY.VendorImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.VendorImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.VendorImages", new[] { "CustomKey" });
            DropIndex("CLARITY.VendorImages", new[] { "Name" });
            DropIndex("CLARITY.VendorImages", new[] { "TypeID" });
            DropIndex("CLARITY.VendorImages", new[] { "MasterID" });
            DropIndex("CLARITY.VendorImages", new[] { "ID" });
            DropIndex("CLARITY.ContactMethods", new[] { "Hash" });
            DropIndex("CLARITY.ContactMethods", new[] { "Active" });
            DropIndex("CLARITY.ContactMethods", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ContactMethods", new[] { "CreatedDate" });
            DropIndex("CLARITY.ContactMethods", new[] { "CustomKey" });
            DropIndex("CLARITY.ContactMethods", new[] { "Name" });
            DropIndex("CLARITY.ContactMethods", new[] { "SortOrder" });
            DropIndex("CLARITY.ContactMethods", new[] { "DisplayName" });
            DropIndex("CLARITY.ContactMethods", new[] { "ID" });
            DropIndex("CLARITY.VendorAccounts", new[] { "Hash" });
            DropIndex("CLARITY.VendorAccounts", new[] { "Active" });
            DropIndex("CLARITY.VendorAccounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.VendorAccounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.VendorAccounts", new[] { "CustomKey" });
            DropIndex("CLARITY.VendorAccounts", new[] { "SlaveID" });
            DropIndex("CLARITY.VendorAccounts", new[] { "MasterID" });
            DropIndex("CLARITY.VendorAccounts", new[] { "ID" });
            DropIndex("CLARITY.Vendors", new[] { "Hash" });
            DropIndex("CLARITY.Vendors", new[] { "Active" });
            DropIndex("CLARITY.Vendors", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Vendors", new[] { "CreatedDate" });
            DropIndex("CLARITY.Vendors", new[] { "CustomKey" });
            DropIndex("CLARITY.Vendors", new[] { "Name" });
            DropIndex("CLARITY.Vendors", new[] { "ContactMethodID" });
            DropIndex("CLARITY.Vendors", new[] { "MustResetPassword" });
            DropIndex("CLARITY.Vendors", new[] { "SecurityToken" });
            DropIndex("CLARITY.Vendors", new[] { "PasswordHash" });
            DropIndex("CLARITY.Vendors", new[] { "UserName" });
            DropIndex("CLARITY.Vendors", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("CLARITY.Vendors", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("CLARITY.Vendors", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("CLARITY.Vendors", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("CLARITY.Vendors", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("CLARITY.Vendors", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("CLARITY.Vendors", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("CLARITY.Vendors", new[] { "MinimumOrderDollarAmountBufferProductID" });
            DropIndex("CLARITY.Vendors", new[] { "ContactID" });
            DropIndex("CLARITY.Vendors", new[] { "TypeID" });
            DropIndex("CLARITY.Vendors", new[] { "ID" });
            DropIndex("CLARITY.FavoriteVendors", new[] { "Hash" });
            DropIndex("CLARITY.FavoriteVendors", new[] { "Active" });
            DropIndex("CLARITY.FavoriteVendors", new[] { "UpdatedDate" });
            DropIndex("CLARITY.FavoriteVendors", new[] { "CreatedDate" });
            DropIndex("CLARITY.FavoriteVendors", new[] { "CustomKey" });
            DropIndex("CLARITY.FavoriteVendors", new[] { "SlaveID" });
            DropIndex("CLARITY.FavoriteVendors", new[] { "MasterID" });
            DropIndex("CLARITY.FavoriteVendors", new[] { "ID" });
            DropIndex("CLARITY.FavoriteStores", new[] { "Hash" });
            DropIndex("CLARITY.FavoriteStores", new[] { "Active" });
            DropIndex("CLARITY.FavoriteStores", new[] { "UpdatedDate" });
            DropIndex("CLARITY.FavoriteStores", new[] { "CreatedDate" });
            DropIndex("CLARITY.FavoriteStores", new[] { "CustomKey" });
            DropIndex("CLARITY.FavoriteStores", new[] { "SlaveID" });
            DropIndex("CLARITY.FavoriteStores", new[] { "MasterID" });
            DropIndex("CLARITY.FavoriteStores", new[] { "ID" });
            DropIndex("CLARITY.FavoriteManufacturers", new[] { "Hash" });
            DropIndex("CLARITY.FavoriteManufacturers", new[] { "Active" });
            DropIndex("CLARITY.FavoriteManufacturers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.FavoriteManufacturers", new[] { "CreatedDate" });
            DropIndex("CLARITY.FavoriteManufacturers", new[] { "CustomKey" });
            DropIndex("CLARITY.FavoriteManufacturers", new[] { "SlaveID" });
            DropIndex("CLARITY.FavoriteManufacturers", new[] { "MasterID" });
            DropIndex("CLARITY.FavoriteManufacturers", new[] { "ID" });
            DropIndex("CLARITY.FavoriteCategories", new[] { "Hash" });
            DropIndex("CLARITY.FavoriteCategories", new[] { "Active" });
            DropIndex("CLARITY.FavoriteCategories", new[] { "UpdatedDate" });
            DropIndex("CLARITY.FavoriteCategories", new[] { "CreatedDate" });
            DropIndex("CLARITY.FavoriteCategories", new[] { "CustomKey" });
            DropIndex("CLARITY.FavoriteCategories", new[] { "SlaveID" });
            DropIndex("CLARITY.FavoriteCategories", new[] { "MasterID" });
            DropIndex("CLARITY.FavoriteCategories", new[] { "ID" });
            DropIndex("CLARITY.DiscountCodes", new[] { "Hash" });
            DropIndex("CLARITY.DiscountCodes", new[] { "Active" });
            DropIndex("CLARITY.DiscountCodes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountCodes", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountCodes", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountCodes", new[] { "UserID" });
            DropIndex("CLARITY.DiscountCodes", new[] { "DiscountID" });
            DropIndex("CLARITY.DiscountCodes", new[] { "ID" });
            DropIndex("CLARITY.GroupUsers", new[] { "Hash" });
            DropIndex("CLARITY.GroupUsers", new[] { "Active" });
            DropIndex("CLARITY.GroupUsers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.GroupUsers", new[] { "CreatedDate" });
            DropIndex("CLARITY.GroupUsers", new[] { "CustomKey" });
            DropIndex("CLARITY.GroupUsers", new[] { "SlaveID" });
            DropIndex("CLARITY.GroupUsers", new[] { "MasterID" });
            DropIndex("CLARITY.GroupUsers", new[] { "ID" });
            DropIndex("CLARITY.GroupTypes", new[] { "Hash" });
            DropIndex("CLARITY.GroupTypes", new[] { "Active" });
            DropIndex("CLARITY.GroupTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.GroupTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.GroupTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.GroupTypes", new[] { "Name" });
            DropIndex("CLARITY.GroupTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.GroupTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.GroupTypes", new[] { "ID" });
            DropIndex("CLARITY.GroupStatus", new[] { "Hash" });
            DropIndex("CLARITY.GroupStatus", new[] { "Active" });
            DropIndex("CLARITY.GroupStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.GroupStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.GroupStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.GroupStatus", new[] { "Name" });
            DropIndex("CLARITY.GroupStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.GroupStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.GroupStatus", new[] { "ID" });
            DropIndex("CLARITY.Groups", new[] { "Hash" });
            DropIndex("CLARITY.Groups", new[] { "Active" });
            DropIndex("CLARITY.Groups", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Groups", new[] { "CreatedDate" });
            DropIndex("CLARITY.Groups", new[] { "CustomKey" });
            DropIndex("CLARITY.Groups", new[] { "Name" });
            DropIndex("CLARITY.Groups", new[] { "GroupOwnerID" });
            DropIndex("CLARITY.Groups", new[] { "StatusID" });
            DropIndex("CLARITY.Groups", new[] { "TypeID" });
            DropIndex("CLARITY.Groups", new[] { "ParentID" });
            DropIndex("CLARITY.Groups", new[] { "ID" });
            DropIndex("CLARITY.EmailTypes", new[] { "Hash" });
            DropIndex("CLARITY.EmailTypes", new[] { "Active" });
            DropIndex("CLARITY.EmailTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.EmailTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.EmailTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.EmailTypes", new[] { "Name" });
            DropIndex("CLARITY.EmailTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.EmailTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.EmailTypes", new[] { "ID" });
            DropIndex("CLARITY.EmailStatus", new[] { "Hash" });
            DropIndex("CLARITY.EmailStatus", new[] { "Active" });
            DropIndex("CLARITY.EmailStatus", new[] { "UpdatedDate" });
            DropIndex("CLARITY.EmailStatus", new[] { "CreatedDate" });
            DropIndex("CLARITY.EmailStatus", new[] { "CustomKey" });
            DropIndex("CLARITY.EmailStatus", new[] { "Name" });
            DropIndex("CLARITY.EmailStatus", new[] { "SortOrder" });
            DropIndex("CLARITY.EmailStatus", new[] { "DisplayName" });
            DropIndex("CLARITY.EmailStatus", new[] { "ID" });
            DropIndex("CLARITY.EmailTemplates", new[] { "Hash" });
            DropIndex("CLARITY.EmailTemplates", new[] { "Active" });
            DropIndex("CLARITY.EmailTemplates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.EmailTemplates", new[] { "CreatedDate" });
            DropIndex("CLARITY.EmailTemplates", new[] { "CustomKey" });
            DropIndex("CLARITY.EmailTemplates", new[] { "Name" });
            DropIndex("CLARITY.EmailTemplates", new[] { "ID" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "Hash" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "Active" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "UpdatedDate" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "CreatedDate" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "CustomKey" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "Name" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "UpdatedByUserID" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "CreatedByUserID" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "SlaveID" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "MasterID" });
            DropIndex("CLARITY.EmailQueueAttachments", new[] { "ID" });
            DropIndex("CLARITY.EmailQueues", new[] { "Hash" });
            DropIndex("CLARITY.EmailQueues", new[] { "Active" });
            DropIndex("CLARITY.EmailQueues", new[] { "UpdatedDate" });
            DropIndex("CLARITY.EmailQueues", new[] { "CreatedDate" });
            DropIndex("CLARITY.EmailQueues", new[] { "CustomKey" });
            DropIndex("CLARITY.EmailQueues", new[] { "MessageRecipientID" });
            DropIndex("CLARITY.EmailQueues", new[] { "EmailTemplateID" });
            DropIndex("CLARITY.EmailQueues", new[] { "StatusID" });
            DropIndex("CLARITY.EmailQueues", new[] { "TypeID" });
            DropIndex("CLARITY.EmailQueues", new[] { "ID" });
            DropIndex("CLARITY.MessageRecipients", new[] { "Hash" });
            DropIndex("CLARITY.MessageRecipients", new[] { "Active" });
            DropIndex("CLARITY.MessageRecipients", new[] { "UpdatedDate" });
            DropIndex("CLARITY.MessageRecipients", new[] { "CreatedDate" });
            DropIndex("CLARITY.MessageRecipients", new[] { "CustomKey" });
            DropIndex("CLARITY.MessageRecipients", new[] { "GroupID" });
            DropIndex("CLARITY.MessageRecipients", new[] { "SlaveID" });
            DropIndex("CLARITY.MessageRecipients", new[] { "MasterID" });
            DropIndex("CLARITY.MessageRecipients", new[] { "ID" });
            DropIndex("CLARITY.StoredFiles", new[] { "Hash" });
            DropIndex("CLARITY.StoredFiles", new[] { "Active" });
            DropIndex("CLARITY.StoredFiles", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoredFiles", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoredFiles", new[] { "CustomKey" });
            DropIndex("CLARITY.StoredFiles", new[] { "Name" });
            DropIndex("CLARITY.StoredFiles", new[] { "ID" });
            DropIndex("CLARITY.MessageAttachments", new[] { "Hash" });
            DropIndex("CLARITY.MessageAttachments", new[] { "Active" });
            DropIndex("CLARITY.MessageAttachments", new[] { "UpdatedDate" });
            DropIndex("CLARITY.MessageAttachments", new[] { "CreatedDate" });
            DropIndex("CLARITY.MessageAttachments", new[] { "CustomKey" });
            DropIndex("CLARITY.MessageAttachments", new[] { "Name" });
            DropIndex("CLARITY.MessageAttachments", new[] { "UpdatedByUserID" });
            DropIndex("CLARITY.MessageAttachments", new[] { "CreatedByUserID" });
            DropIndex("CLARITY.MessageAttachments", new[] { "SlaveID" });
            DropIndex("CLARITY.MessageAttachments", new[] { "MasterID" });
            DropIndex("CLARITY.MessageAttachments", new[] { "ID" });
            DropIndex("CLARITY.Messages", new[] { "Hash" });
            DropIndex("CLARITY.Messages", new[] { "Active" });
            DropIndex("CLARITY.Messages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Messages", new[] { "CreatedDate" });
            DropIndex("CLARITY.Messages", new[] { "CustomKey" });
            DropIndex("CLARITY.Messages", new[] { "SentByUserID" });
            DropIndex("CLARITY.Messages", new[] { "ConversationID" });
            DropIndex("CLARITY.Messages", new[] { "StoreID" });
            DropIndex("CLARITY.Messages", new[] { "ID" });
            DropIndex("CLARITY.Conversations", new[] { "Hash" });
            DropIndex("CLARITY.Conversations", new[] { "Active" });
            DropIndex("CLARITY.Conversations", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Conversations", new[] { "CreatedDate" });
            DropIndex("CLARITY.Conversations", new[] { "CustomKey" });
            DropIndex("CLARITY.Conversations", new[] { "StoreID" });
            DropIndex("CLARITY.Conversations", new[] { "ID" });
            DropIndex("CLARITY.ConversationUsers", new[] { "Hash" });
            DropIndex("CLARITY.ConversationUsers", new[] { "Active" });
            DropIndex("CLARITY.ConversationUsers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ConversationUsers", new[] { "CreatedDate" });
            DropIndex("CLARITY.ConversationUsers", new[] { "CustomKey" });
            DropIndex("CLARITY.ConversationUsers", new[] { "SlaveID" });
            DropIndex("CLARITY.ConversationUsers", new[] { "MasterID" });
            DropIndex("CLARITY.ConversationUsers", new[] { "ID" });
            DropIndex("CLARITY.UserClaims", new[] { "UserId" });
            DropIndex("CLARITY.UserClaims", new[] { "Id" });
            DropIndex("CLARITY.Users", new[] { "UserOnlineStatusID" });
            DropIndex("CLARITY.Users", new[] { "LanguageID" });
            DropIndex("CLARITY.Users", new[] { "CurrencyID" });
            DropIndex("CLARITY.Users", new[] { "PreferredStoreID" });
            DropIndex("CLARITY.Users", new[] { "AccountID" });
            DropIndex("CLARITY.Users", new[] { "UserName" });
            DropIndex("CLARITY.Users", new[] { "ContactID" });
            DropIndex("CLARITY.Users", new[] { "StatusID" });
            DropIndex("CLARITY.Users", new[] { "TypeID" });
            DropIndex("CLARITY.Users", new[] { "Hash" });
            DropIndex("CLARITY.Users", new[] { "Active" });
            DropIndex("CLARITY.Users", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Users", new[] { "CreatedDate" });
            DropIndex("CLARITY.Users", new[] { "CustomKey" });
            DropIndex("CLARITY.Users", new[] { "ID" });
            DropIndex("CLARITY.Notes", new[] { "Hash" });
            DropIndex("CLARITY.Notes", new[] { "Active" });
            DropIndex("CLARITY.Notes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Notes", new[] { "CreatedDate" });
            DropIndex("CLARITY.Notes", new[] { "CustomKey" });
            DropIndex("CLARITY.Notes", new[] { "CartItemID" });
            DropIndex("CLARITY.Notes", new[] { "SalesReturnItemID" });
            DropIndex("CLARITY.Notes", new[] { "SampleRequestItemID" });
            DropIndex("CLARITY.Notes", new[] { "SalesQuoteItemID" });
            DropIndex("CLARITY.Notes", new[] { "SalesInvoiceItemID" });
            DropIndex("CLARITY.Notes", new[] { "SalesOrderItemID" });
            DropIndex("CLARITY.Notes", new[] { "PurchaseOrderItemID" });
            DropIndex("CLARITY.Notes", new[] { "CartID" });
            DropIndex("CLARITY.Notes", new[] { "SalesReturnID" });
            DropIndex("CLARITY.Notes", new[] { "SampleRequestID" });
            DropIndex("CLARITY.Notes", new[] { "SalesQuoteID" });
            DropIndex("CLARITY.Notes", new[] { "SalesInvoiceID" });
            DropIndex("CLARITY.Notes", new[] { "SalesOrderID" });
            DropIndex("CLARITY.Notes", new[] { "PurchaseOrderID" });
            DropIndex("CLARITY.Notes", new[] { "SalesGroupID" });
            DropIndex("CLARITY.Notes", new[] { "StoreID" });
            DropIndex("CLARITY.Notes", new[] { "ManufacturerID" });
            DropIndex("CLARITY.Notes", new[] { "VendorID" });
            DropIndex("CLARITY.Notes", new[] { "UserID" });
            DropIndex("CLARITY.Notes", new[] { "AccountID" });
            DropIndex("CLARITY.Notes", new[] { "UpdatedByUserID" });
            DropIndex("CLARITY.Notes", new[] { "CreatedByUserID" });
            DropIndex("CLARITY.Notes", new[] { "TypeID" });
            DropIndex("CLARITY.Notes", new[] { "ID" });
            DropIndex("CLARITY.AppliedCartDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedCartDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedCartDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedCartDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedCartDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedCartDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedCartDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedCartDiscounts", new[] { "ID" });
            DropIndex("CLARITY.CartContacts", new[] { "Hash" });
            DropIndex("CLARITY.CartContacts", new[] { "Active" });
            DropIndex("CLARITY.CartContacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CartContacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.CartContacts", new[] { "CustomKey" });
            DropIndex("CLARITY.CartContacts", new[] { "SlaveID" });
            DropIndex("CLARITY.CartContacts", new[] { "MasterID" });
            DropIndex("CLARITY.CartContacts", new[] { "ID" });
            DropIndex("CLARITY.Carts", new[] { "Hash" });
            DropIndex("CLARITY.Carts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Carts", new[] { "CreatedDate" });
            DropIndex("CLARITY.Carts", new[] { "CustomKey" });
            DropIndex("CLARITY.Carts", new[] { "ShipmentID" });
            DropIndex("CLARITY.Carts", new[] { "ParentID" });
            DropIndex("CLARITY.Carts", new[] { "StoreID" });
            DropIndex("CLARITY.Carts", new[] { "AccountID" });
            DropIndex("CLARITY.Carts", new[] { "UserID" });
            DropIndex("CLARITY.Carts", new[] { "TypeID" });
            DropIndex("CLARITY.Carts", new[] { "StateID" });
            DropIndex("CLARITY.Carts", new[] { "StatusID" });
            DropIndex("CLARITY.Carts", new[] { "ShippingContactID" });
            DropIndex("CLARITY.Carts", new[] { "BillingContactID" });
            DropIndex("CLARITY.Carts", "Unq_BySessionTypeUserActive");
            DropIndex("CLARITY.Carts", new[] { "Active" });
            DropIndex("CLARITY.Carts", new[] { "ID" });
            DropIndex("CLARITY.AppliedCartItemDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedCartItemDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedCartItemDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedCartItemDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedCartItemDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedCartItemDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedCartItemDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedCartItemDiscounts", new[] { "ID" });
            DropIndex("CLARITY.CartItems", new[] { "Hash" });
            DropIndex("CLARITY.CartItems", new[] { "Active" });
            DropIndex("CLARITY.CartItems", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CartItems", new[] { "CreatedDate" });
            DropIndex("CLARITY.CartItems", new[] { "CustomKey" });
            DropIndex("CLARITY.CartItems", new[] { "Name" });
            DropIndex("CLARITY.CartItems", new[] { "SellingCurrencyID" });
            DropIndex("CLARITY.CartItems", new[] { "OriginalCurrencyID" });
            DropIndex("CLARITY.CartItems", new[] { "UserID" });
            DropIndex("CLARITY.CartItems", new[] { "ProductID" });
            DropIndex("CLARITY.CartItems", new[] { "StatusID" });
            DropIndex("CLARITY.CartItems", new[] { "MasterID" });
            DropIndex("CLARITY.CartItems", new[] { "ID" });
            DropIndex("CLARITY.AccountProductTypes", new[] { "Hash" });
            DropIndex("CLARITY.AccountProductTypes", new[] { "Active" });
            DropIndex("CLARITY.AccountProductTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountProductTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountProductTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountProductTypes", new[] { "Name" });
            DropIndex("CLARITY.AccountProductTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.AccountProductTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.AccountProductTypes", new[] { "ID" });
            DropIndex("CLARITY.AccountProducts", new[] { "Hash" });
            DropIndex("CLARITY.AccountProducts", new[] { "Active" });
            DropIndex("CLARITY.AccountProducts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountProducts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountProducts", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountProducts", new[] { "TypeID" });
            DropIndex("CLARITY.AccountProducts", new[] { "SlaveID" });
            DropIndex("CLARITY.AccountProducts", new[] { "MasterID" });
            DropIndex("CLARITY.AccountProducts", new[] { "ID" });
            DropIndex("CLARITY.Products", new[] { "Hash" });
            DropIndex("CLARITY.Products", new[] { "Active" });
            DropIndex("CLARITY.Products", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Products", new[] { "CreatedDate" });
            DropIndex("CLARITY.Products", new[] { "CustomKey" });
            DropIndex("CLARITY.Products", new[] { "Name" });
            DropIndex("CLARITY.Products", new[] { "RestockingFeeAmountCurrencyID" });
            DropIndex("CLARITY.Products", new[] { "PalletID" });
            DropIndex("CLARITY.Products", new[] { "MasterPackID" });
            DropIndex("CLARITY.Products", new[] { "PackageID" });
            DropIndex("CLARITY.Products", new[] { "TotalPurchasedAmountCurrencyID" });
            DropIndex("CLARITY.Products", new[] { "TypeID" });
            DropIndex("CLARITY.Products", new[] { "StatusID" });
            DropIndex("CLARITY.Products", new[] { "ID" });
            DropIndex("CLARITY.CategoryImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.CategoryImageTypes", new[] { "Active" });
            DropIndex("CLARITY.CategoryImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CategoryImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CategoryImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CategoryImageTypes", new[] { "Name" });
            DropIndex("CLARITY.CategoryImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CategoryImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CategoryImageTypes", new[] { "ID" });
            DropIndex("CLARITY.CategoryImages", new[] { "Hash" });
            DropIndex("CLARITY.CategoryImages", new[] { "Active" });
            DropIndex("CLARITY.CategoryImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CategoryImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.CategoryImages", new[] { "CustomKey" });
            DropIndex("CLARITY.CategoryImages", new[] { "Name" });
            DropIndex("CLARITY.CategoryImages", new[] { "TypeID" });
            DropIndex("CLARITY.CategoryImages", new[] { "MasterID" });
            DropIndex("CLARITY.CategoryImages", new[] { "ID" });
            DropIndex("CLARITY.Categories", new[] { "Hash" });
            DropIndex("CLARITY.Categories", new[] { "Active" });
            DropIndex("CLARITY.Categories", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Categories", new[] { "CreatedDate" });
            DropIndex("CLARITY.Categories", new[] { "CustomKey" });
            DropIndex("CLARITY.Categories", new[] { "Name" });
            DropIndex("CLARITY.Categories", new[] { "RestockingFeeAmountCurrencyID" });
            DropIndex("CLARITY.Categories", new[] { "DisplayName" });
            DropIndex("CLARITY.Categories", new[] { "TypeID" });
            DropIndex("CLARITY.Categories", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("CLARITY.Categories", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("CLARITY.Categories", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("CLARITY.Categories", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("CLARITY.Categories", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("CLARITY.Categories", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("CLARITY.Categories", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("CLARITY.Categories", new[] { "MinimumOrderDollarAmountBufferProductID" });
            DropIndex("CLARITY.Categories", new[] { "ParentID" });
            DropIndex("CLARITY.Categories", new[] { "ID" });
            DropIndex("CLARITY.ManufacturerImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.ManufacturerImageTypes", new[] { "Active" });
            DropIndex("CLARITY.ManufacturerImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ManufacturerImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.ManufacturerImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.ManufacturerImageTypes", new[] { "Name" });
            DropIndex("CLARITY.ManufacturerImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.ManufacturerImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.ManufacturerImageTypes", new[] { "ID" });
            DropIndex("CLARITY.ManufacturerImages", new[] { "Hash" });
            DropIndex("CLARITY.ManufacturerImages", new[] { "Active" });
            DropIndex("CLARITY.ManufacturerImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.ManufacturerImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.ManufacturerImages", new[] { "CustomKey" });
            DropIndex("CLARITY.ManufacturerImages", new[] { "Name" });
            DropIndex("CLARITY.ManufacturerImages", new[] { "TypeID" });
            DropIndex("CLARITY.ManufacturerImages", new[] { "MasterID" });
            DropIndex("CLARITY.ManufacturerImages", new[] { "ID" });
            DropIndex("CLARITY.Manufacturers", new[] { "Hash" });
            DropIndex("CLARITY.Manufacturers", new[] { "Active" });
            DropIndex("CLARITY.Manufacturers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Manufacturers", new[] { "CreatedDate" });
            DropIndex("CLARITY.Manufacturers", new[] { "CustomKey" });
            DropIndex("CLARITY.Manufacturers", new[] { "Name" });
            DropIndex("CLARITY.Manufacturers", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("CLARITY.Manufacturers", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("CLARITY.Manufacturers", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("CLARITY.Manufacturers", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("CLARITY.Manufacturers", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("CLARITY.Manufacturers", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("CLARITY.Manufacturers", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("CLARITY.Manufacturers", new[] { "MinimumOrderDollarAmountBufferProductID" });
            DropIndex("CLARITY.Manufacturers", new[] { "ContactID" });
            DropIndex("CLARITY.Manufacturers", new[] { "TypeID" });
            DropIndex("CLARITY.Manufacturers", new[] { "ID" });
            DropIndex("CLARITY.StoreManufacturers", new[] { "Hash" });
            DropIndex("CLARITY.StoreManufacturers", new[] { "Active" });
            DropIndex("CLARITY.StoreManufacturers", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreManufacturers", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreManufacturers", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreManufacturers", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreManufacturers", new[] { "MasterID" });
            DropIndex("CLARITY.StoreManufacturers", new[] { "ID" });
            DropIndex("CLARITY.StoreImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.StoreImageTypes", new[] { "Active" });
            DropIndex("CLARITY.StoreImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreImageTypes", new[] { "Name" });
            DropIndex("CLARITY.StoreImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.StoreImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.StoreImageTypes", new[] { "ID" });
            DropIndex("CLARITY.StoreImages", new[] { "Hash" });
            DropIndex("CLARITY.StoreImages", new[] { "Active" });
            DropIndex("CLARITY.StoreImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreImages", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreImages", new[] { "Name" });
            DropIndex("CLARITY.StoreImages", new[] { "TypeID" });
            DropIndex("CLARITY.StoreImages", new[] { "MasterID" });
            DropIndex("CLARITY.StoreImages", new[] { "ID" });
            DropIndex("CLARITY.BrandImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.BrandImageTypes", new[] { "Active" });
            DropIndex("CLARITY.BrandImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.BrandImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.BrandImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.BrandImageTypes", new[] { "Name" });
            DropIndex("CLARITY.BrandImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.BrandImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.BrandImageTypes", new[] { "ID" });
            DropIndex("CLARITY.BrandImages", new[] { "Hash" });
            DropIndex("CLARITY.BrandImages", new[] { "Active" });
            DropIndex("CLARITY.BrandImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.BrandImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.BrandImages", new[] { "CustomKey" });
            DropIndex("CLARITY.BrandImages", new[] { "Name" });
            DropIndex("CLARITY.BrandImages", new[] { "TypeID" });
            DropIndex("CLARITY.BrandImages", new[] { "MasterID" });
            DropIndex("CLARITY.BrandImages", new[] { "ID" });
            DropIndex("CLARITY.StoreSiteDomains", new[] { "Hash" });
            DropIndex("CLARITY.StoreSiteDomains", new[] { "Active" });
            DropIndex("CLARITY.StoreSiteDomains", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreSiteDomains", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreSiteDomains", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreSiteDomains", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreSiteDomains", new[] { "MasterID" });
            DropIndex("CLARITY.StoreSiteDomains", new[] { "ID" });
            DropIndex("CLARITY.SocialProviders", new[] { "Hash" });
            DropIndex("CLARITY.SocialProviders", new[] { "Active" });
            DropIndex("CLARITY.SocialProviders", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SocialProviders", new[] { "CreatedDate" });
            DropIndex("CLARITY.SocialProviders", new[] { "CustomKey" });
            DropIndex("CLARITY.SocialProviders", new[] { "Name" });
            DropIndex("CLARITY.SocialProviders", new[] { "ID" });
            DropIndex("CLARITY.SiteDomainSocialProviders", new[] { "Hash" });
            DropIndex("CLARITY.SiteDomainSocialProviders", new[] { "Active" });
            DropIndex("CLARITY.SiteDomainSocialProviders", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SiteDomainSocialProviders", new[] { "CreatedDate" });
            DropIndex("CLARITY.SiteDomainSocialProviders", new[] { "CustomKey" });
            DropIndex("CLARITY.SiteDomainSocialProviders", new[] { "SlaveID" });
            DropIndex("CLARITY.SiteDomainSocialProviders", new[] { "MasterID" });
            DropIndex("CLARITY.SiteDomainSocialProviders", new[] { "ID" });
            DropIndex("CLARITY.SiteDomains", new[] { "Hash" });
            DropIndex("CLARITY.SiteDomains", new[] { "Active" });
            DropIndex("CLARITY.SiteDomains", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SiteDomains", new[] { "CreatedDate" });
            DropIndex("CLARITY.SiteDomains", new[] { "CustomKey" });
            DropIndex("CLARITY.SiteDomains", new[] { "Name" });
            DropIndex("CLARITY.SiteDomains", new[] { "ID" });
            DropIndex("CLARITY.BrandSiteDomains", new[] { "Hash" });
            DropIndex("CLARITY.BrandSiteDomains", new[] { "Active" });
            DropIndex("CLARITY.BrandSiteDomains", new[] { "UpdatedDate" });
            DropIndex("CLARITY.BrandSiteDomains", new[] { "CreatedDate" });
            DropIndex("CLARITY.BrandSiteDomains", new[] { "CustomKey" });
            DropIndex("CLARITY.BrandSiteDomains", new[] { "SlaveID" });
            DropIndex("CLARITY.BrandSiteDomains", new[] { "MasterID" });
            DropIndex("CLARITY.BrandSiteDomains", new[] { "ID" });
            DropIndex("CLARITY.Brands", new[] { "Hash" });
            DropIndex("CLARITY.Brands", new[] { "Active" });
            DropIndex("CLARITY.Brands", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Brands", new[] { "CreatedDate" });
            DropIndex("CLARITY.Brands", new[] { "CustomKey" });
            DropIndex("CLARITY.Brands", new[] { "Name" });
            DropIndex("CLARITY.Brands", new[] { "ID" });
            DropIndex("CLARITY.BrandStores", new[] { "Hash" });
            DropIndex("CLARITY.BrandStores", new[] { "Active" });
            DropIndex("CLARITY.BrandStores", new[] { "UpdatedDate" });
            DropIndex("CLARITY.BrandStores", new[] { "CreatedDate" });
            DropIndex("CLARITY.BrandStores", new[] { "CustomKey" });
            DropIndex("CLARITY.BrandStores", new[] { "SlaveID" });
            DropIndex("CLARITY.BrandStores", new[] { "MasterID" });
            DropIndex("CLARITY.BrandStores", new[] { "ID" });
            DropIndex("CLARITY.PricePoints", new[] { "Hash" });
            DropIndex("CLARITY.PricePoints", new[] { "Active" });
            DropIndex("CLARITY.PricePoints", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PricePoints", new[] { "CreatedDate" });
            DropIndex("CLARITY.PricePoints", new[] { "CustomKey" });
            DropIndex("CLARITY.PricePoints", new[] { "Name" });
            DropIndex("CLARITY.PricePoints", new[] { "SortOrder" });
            DropIndex("CLARITY.PricePoints", new[] { "DisplayName" });
            DropIndex("CLARITY.PricePoints", new[] { "ID" });
            DropIndex("CLARITY.StoreAccounts", new[] { "Hash" });
            DropIndex("CLARITY.StoreAccounts", new[] { "Active" });
            DropIndex("CLARITY.StoreAccounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.StoreAccounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.StoreAccounts", new[] { "CustomKey" });
            DropIndex("CLARITY.StoreAccounts", new[] { "PricePointID" });
            DropIndex("CLARITY.StoreAccounts", new[] { "SlaveID" });
            DropIndex("CLARITY.StoreAccounts", new[] { "MasterID" });
            DropIndex("CLARITY.StoreAccounts", new[] { "ID" });
            DropIndex("CLARITY.Stores", new[] { "Hash" });
            DropIndex("CLARITY.Stores", new[] { "Active" });
            DropIndex("CLARITY.Stores", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Stores", new[] { "CreatedDate" });
            DropIndex("CLARITY.Stores", new[] { "CustomKey" });
            DropIndex("CLARITY.Stores", new[] { "Name" });
            DropIndex("CLARITY.Stores", new[] { "LanguageID" });
            DropIndex("CLARITY.Stores", new[] { "MinimumForFreeShippingQuantityAmountBufferCategoryID" });
            DropIndex("CLARITY.Stores", new[] { "MinimumForFreeShippingDollarAmountBufferCategoryID" });
            DropIndex("CLARITY.Stores", new[] { "MinimumForFreeShippingQuantityAmountBufferProductID" });
            DropIndex("CLARITY.Stores", new[] { "MinimumForFreeShippingDollarAmountBufferProductID" });
            DropIndex("CLARITY.Stores", new[] { "MinimumOrderQuantityAmountBufferCategoryID" });
            DropIndex("CLARITY.Stores", new[] { "MinimumOrderDollarAmountBufferCategoryID" });
            DropIndex("CLARITY.Stores", new[] { "MinimumOrderQuantityAmountBufferProductID" });
            DropIndex("CLARITY.Stores", new[] { "MinimumOrderDollarAmountBufferProductID" });
            DropIndex("CLARITY.Stores", new[] { "ContactID" });
            DropIndex("CLARITY.Stores", new[] { "TypeID" });
            DropIndex("CLARITY.Stores", new[] { "ID" });
            DropIndex("CLARITY.AccountTypes", new[] { "Hash" });
            DropIndex("CLARITY.AccountTypes", new[] { "Active" });
            DropIndex("CLARITY.AccountTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountTypes", new[] { "Name" });
            DropIndex("CLARITY.AccountTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.AccountTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.AccountTypes", new[] { "StoreID" });
            DropIndex("CLARITY.AccountTypes", new[] { "ID" });
            DropIndex("CLARITY.DiscountAccountTypes", new[] { "Hash" });
            DropIndex("CLARITY.DiscountAccountTypes", new[] { "Active" });
            DropIndex("CLARITY.DiscountAccountTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountAccountTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountAccountTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountAccountTypes", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountAccountTypes", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountAccountTypes", new[] { "ID" });
            DropIndex("CLARITY.DiscountAccounts", new[] { "Hash" });
            DropIndex("CLARITY.DiscountAccounts", new[] { "Active" });
            DropIndex("CLARITY.DiscountAccounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DiscountAccounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.DiscountAccounts", new[] { "CustomKey" });
            DropIndex("CLARITY.DiscountAccounts", new[] { "SlaveID" });
            DropIndex("CLARITY.DiscountAccounts", new[] { "MasterID" });
            DropIndex("CLARITY.DiscountAccounts", new[] { "ID" });
            DropIndex("CLARITY.Discounts", new[] { "Hash" });
            DropIndex("CLARITY.Discounts", new[] { "Active" });
            DropIndex("CLARITY.Discounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Discounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.Discounts", new[] { "CustomKey" });
            DropIndex("CLARITY.Discounts", new[] { "Name" });
            DropIndex("CLARITY.Discounts", new[] { "ID" });
            DropIndex("CLARITY.AppliedSalesInvoiceDiscounts", new[] { "Hash" });
            DropIndex("CLARITY.AppliedSalesInvoiceDiscounts", new[] { "Active" });
            DropIndex("CLARITY.AppliedSalesInvoiceDiscounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AppliedSalesInvoiceDiscounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AppliedSalesInvoiceDiscounts", new[] { "CustomKey" });
            DropIndex("CLARITY.AppliedSalesInvoiceDiscounts", new[] { "SlaveID" });
            DropIndex("CLARITY.AppliedSalesInvoiceDiscounts", new[] { "MasterID" });
            DropIndex("CLARITY.AppliedSalesInvoiceDiscounts", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoiceContacts", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoiceContacts", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoiceContacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoiceContacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoiceContacts", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoiceContacts", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesInvoiceContacts", new[] { "MasterID" });
            DropIndex("CLARITY.SalesInvoiceContacts", new[] { "ID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "Hash" });
            DropIndex("CLARITY.SalesInvoices", new[] { "Active" });
            DropIndex("CLARITY.SalesInvoices", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesInvoices", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesInvoices", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesInvoices", new[] { "SalesGroupID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "ParentID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "StoreID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "AccountID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "UserID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "TypeID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "StateID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "StatusID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "ShippingContactID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "BillingContactID" });
            DropIndex("CLARITY.SalesInvoices", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderSalesInvoices", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderSalesInvoices", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderSalesInvoices", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderSalesInvoices", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderSalesInvoices", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderSalesInvoices", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesOrderSalesInvoices", new[] { "MasterID" });
            DropIndex("CLARITY.SalesOrderSalesInvoices", new[] { "ID" });
            DropIndex("CLARITY.SalesOrders", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrders", new[] { "Active" });
            DropIndex("CLARITY.SalesOrders", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrders", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrders", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrders", new[] { "SalesGroupAsSubID" });
            DropIndex("CLARITY.SalesOrders", new[] { "SalesGroupAsMasterID" });
            DropIndex("CLARITY.SalesOrders", new[] { "ParentID" });
            DropIndex("CLARITY.SalesOrders", new[] { "StoreID" });
            DropIndex("CLARITY.SalesOrders", new[] { "AccountID" });
            DropIndex("CLARITY.SalesOrders", new[] { "UserID" });
            DropIndex("CLARITY.SalesOrders", new[] { "TypeID" });
            DropIndex("CLARITY.SalesOrders", new[] { "StateID" });
            DropIndex("CLARITY.SalesOrders", new[] { "StatusID" });
            DropIndex("CLARITY.SalesOrders", new[] { "ShippingContactID" });
            DropIndex("CLARITY.SalesOrders", new[] { "BillingContactID" });
            DropIndex("CLARITY.SalesOrders", new[] { "ID" });
            DropIndex("CLARITY.SalesOrderPurchaseOrders", new[] { "Hash" });
            DropIndex("CLARITY.SalesOrderPurchaseOrders", new[] { "Active" });
            DropIndex("CLARITY.SalesOrderPurchaseOrders", new[] { "UpdatedDate" });
            DropIndex("CLARITY.SalesOrderPurchaseOrders", new[] { "CreatedDate" });
            DropIndex("CLARITY.SalesOrderPurchaseOrders", new[] { "CustomKey" });
            DropIndex("CLARITY.SalesOrderPurchaseOrders", new[] { "SlaveID" });
            DropIndex("CLARITY.SalesOrderPurchaseOrders", new[] { "MasterID" });
            DropIndex("CLARITY.SalesOrderPurchaseOrders", new[] { "ID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "Hash" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "Active" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "UpdatedDate" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "CreatedDate" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "CustomKey" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "SalesGroupID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "VendorID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "ShipCarrierID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "InventoryLocationID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "ParentID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "StoreID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "AccountID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "UserID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "TypeID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "StateID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "StatusID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "ShippingContactID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "BillingContactID" });
            DropIndex("CLARITY.PurchaseOrders", new[] { "ID" });
            DropIndex("CLARITY.TaxCountries", new[] { "Hash" });
            DropIndex("CLARITY.TaxCountries", new[] { "Active" });
            DropIndex("CLARITY.TaxCountries", new[] { "UpdatedDate" });
            DropIndex("CLARITY.TaxCountries", new[] { "CreatedDate" });
            DropIndex("CLARITY.TaxCountries", new[] { "CustomKey" });
            DropIndex("CLARITY.TaxCountries", new[] { "Name" });
            DropIndex("CLARITY.TaxCountries", new[] { "CountryID" });
            DropIndex("CLARITY.TaxCountries", new[] { "ID" });
            DropIndex("CLARITY.TaxRegions", new[] { "Hash" });
            DropIndex("CLARITY.TaxRegions", new[] { "Active" });
            DropIndex("CLARITY.TaxRegions", new[] { "UpdatedDate" });
            DropIndex("CLARITY.TaxRegions", new[] { "CreatedDate" });
            DropIndex("CLARITY.TaxRegions", new[] { "CustomKey" });
            DropIndex("CLARITY.TaxRegions", new[] { "Name" });
            DropIndex("CLARITY.TaxRegions", new[] { "RegionID" });
            DropIndex("CLARITY.TaxRegions", new[] { "ID" });
            DropIndex("CLARITY.RegionLanguages", new[] { "Hash" });
            DropIndex("CLARITY.RegionLanguages", new[] { "Active" });
            DropIndex("CLARITY.RegionLanguages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.RegionLanguages", new[] { "CreatedDate" });
            DropIndex("CLARITY.RegionLanguages", new[] { "CustomKey" });
            DropIndex("CLARITY.RegionLanguages", new[] { "SlaveID" });
            DropIndex("CLARITY.RegionLanguages", new[] { "MasterID" });
            DropIndex("CLARITY.RegionLanguages", new[] { "ID" });
            DropIndex("CLARITY.InterRegions", new[] { "Hash" });
            DropIndex("CLARITY.InterRegions", new[] { "Active" });
            DropIndex("CLARITY.InterRegions", new[] { "UpdatedDate" });
            DropIndex("CLARITY.InterRegions", new[] { "CreatedDate" });
            DropIndex("CLARITY.InterRegions", new[] { "CustomKey" });
            DropIndex("CLARITY.InterRegions", new[] { "RelationRegionID" });
            DropIndex("CLARITY.InterRegions", new[] { "KeyRegionID" });
            DropIndex("CLARITY.InterRegions", new[] { "ID" });
            DropIndex("CLARITY.RegionImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.RegionImageTypes", new[] { "Active" });
            DropIndex("CLARITY.RegionImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.RegionImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.RegionImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.RegionImageTypes", new[] { "Name" });
            DropIndex("CLARITY.RegionImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.RegionImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.RegionImageTypes", new[] { "ID" });
            DropIndex("CLARITY.RegionImages", new[] { "Hash" });
            DropIndex("CLARITY.RegionImages", new[] { "Active" });
            DropIndex("CLARITY.RegionImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.RegionImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.RegionImages", new[] { "CustomKey" });
            DropIndex("CLARITY.RegionImages", new[] { "Name" });
            DropIndex("CLARITY.RegionImages", new[] { "TypeID" });
            DropIndex("CLARITY.RegionImages", new[] { "MasterID" });
            DropIndex("CLARITY.RegionImages", new[] { "ID" });
            DropIndex("CLARITY.TaxDistricts", new[] { "Hash" });
            DropIndex("CLARITY.TaxDistricts", new[] { "Active" });
            DropIndex("CLARITY.TaxDistricts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.TaxDistricts", new[] { "CreatedDate" });
            DropIndex("CLARITY.TaxDistricts", new[] { "CustomKey" });
            DropIndex("CLARITY.TaxDistricts", new[] { "Name" });
            DropIndex("CLARITY.TaxDistricts", new[] { "DistrictID" });
            DropIndex("CLARITY.TaxDistricts", new[] { "ID" });
            DropIndex("CLARITY.DistrictLanguages", new[] { "Hash" });
            DropIndex("CLARITY.DistrictLanguages", new[] { "Active" });
            DropIndex("CLARITY.DistrictLanguages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DistrictLanguages", new[] { "CreatedDate" });
            DropIndex("CLARITY.DistrictLanguages", new[] { "CustomKey" });
            DropIndex("CLARITY.DistrictLanguages", new[] { "SlaveID" });
            DropIndex("CLARITY.DistrictLanguages", new[] { "MasterID" });
            DropIndex("CLARITY.DistrictLanguages", new[] { "ID" });
            DropIndex("CLARITY.DistrictImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.DistrictImageTypes", new[] { "Active" });
            DropIndex("CLARITY.DistrictImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DistrictImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.DistrictImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.DistrictImageTypes", new[] { "Name" });
            DropIndex("CLARITY.DistrictImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.DistrictImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.DistrictImageTypes", new[] { "ID" });
            DropIndex("CLARITY.DistrictImages", new[] { "Hash" });
            DropIndex("CLARITY.DistrictImages", new[] { "Active" });
            DropIndex("CLARITY.DistrictImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DistrictImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.DistrictImages", new[] { "CustomKey" });
            DropIndex("CLARITY.DistrictImages", new[] { "Name" });
            DropIndex("CLARITY.DistrictImages", new[] { "TypeID" });
            DropIndex("CLARITY.DistrictImages", new[] { "MasterID" });
            DropIndex("CLARITY.DistrictImages", new[] { "ID" });
            DropIndex("CLARITY.DistrictCurrencies", new[] { "Hash" });
            DropIndex("CLARITY.DistrictCurrencies", new[] { "Active" });
            DropIndex("CLARITY.DistrictCurrencies", new[] { "UpdatedDate" });
            DropIndex("CLARITY.DistrictCurrencies", new[] { "CreatedDate" });
            DropIndex("CLARITY.DistrictCurrencies", new[] { "CustomKey" });
            DropIndex("CLARITY.DistrictCurrencies", new[] { "SlaveID" });
            DropIndex("CLARITY.DistrictCurrencies", new[] { "MasterID" });
            DropIndex("CLARITY.DistrictCurrencies", new[] { "ID" });
            DropIndex("CLARITY.Districts", new[] { "Hash" });
            DropIndex("CLARITY.Districts", new[] { "Active" });
            DropIndex("CLARITY.Districts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Districts", new[] { "CreatedDate" });
            DropIndex("CLARITY.Districts", new[] { "CustomKey" });
            DropIndex("CLARITY.Districts", new[] { "Name" });
            DropIndex("CLARITY.Districts", new[] { "RegionID" });
            DropIndex("CLARITY.Districts", new[] { "ID" });
            DropIndex("CLARITY.RegionCurrencies", new[] { "Hash" });
            DropIndex("CLARITY.RegionCurrencies", new[] { "Active" });
            DropIndex("CLARITY.RegionCurrencies", new[] { "UpdatedDate" });
            DropIndex("CLARITY.RegionCurrencies", new[] { "CreatedDate" });
            DropIndex("CLARITY.RegionCurrencies", new[] { "CustomKey" });
            DropIndex("CLARITY.RegionCurrencies", new[] { "SlaveID" });
            DropIndex("CLARITY.RegionCurrencies", new[] { "MasterID" });
            DropIndex("CLARITY.RegionCurrencies", new[] { "ID" });
            DropIndex("CLARITY.Regions", new[] { "Hash" });
            DropIndex("CLARITY.Regions", new[] { "Active" });
            DropIndex("CLARITY.Regions", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Regions", new[] { "CreatedDate" });
            DropIndex("CLARITY.Regions", new[] { "CustomKey" });
            DropIndex("CLARITY.Regions", new[] { "Name" });
            DropIndex("CLARITY.Regions", new[] { "CountryID" });
            DropIndex("CLARITY.Regions", new[] { "ID" });
            DropIndex("CLARITY.LanguageImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.LanguageImageTypes", new[] { "Active" });
            DropIndex("CLARITY.LanguageImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.LanguageImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.LanguageImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.LanguageImageTypes", new[] { "Name" });
            DropIndex("CLARITY.LanguageImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.LanguageImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.LanguageImageTypes", new[] { "ID" });
            DropIndex("CLARITY.LanguageImages", new[] { "Hash" });
            DropIndex("CLARITY.LanguageImages", new[] { "Active" });
            DropIndex("CLARITY.LanguageImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.LanguageImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.LanguageImages", new[] { "CustomKey" });
            DropIndex("CLARITY.LanguageImages", new[] { "Name" });
            DropIndex("CLARITY.LanguageImages", new[] { "TypeID" });
            DropIndex("CLARITY.LanguageImages", new[] { "MasterID" });
            DropIndex("CLARITY.LanguageImages", new[] { "ID" });
            DropIndex("CLARITY.Languages", new[] { "Hash" });
            DropIndex("CLARITY.Languages", new[] { "Active" });
            DropIndex("CLARITY.Languages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Languages", new[] { "CreatedDate" });
            DropIndex("CLARITY.Languages", new[] { "CustomKey" });
            DropIndex("CLARITY.Languages", new[] { "Locale" });
            DropIndex("CLARITY.Languages", new[] { "ID" });
            DropIndex("CLARITY.CountryLanguages", new[] { "Hash" });
            DropIndex("CLARITY.CountryLanguages", new[] { "Active" });
            DropIndex("CLARITY.CountryLanguages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CountryLanguages", new[] { "CreatedDate" });
            DropIndex("CLARITY.CountryLanguages", new[] { "CustomKey" });
            DropIndex("CLARITY.CountryLanguages", new[] { "SlaveID" });
            DropIndex("CLARITY.CountryLanguages", new[] { "MasterID" });
            DropIndex("CLARITY.CountryLanguages", new[] { "ID" });
            DropIndex("CLARITY.CountryImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.CountryImageTypes", new[] { "Active" });
            DropIndex("CLARITY.CountryImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CountryImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CountryImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CountryImageTypes", new[] { "Name" });
            DropIndex("CLARITY.CountryImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CountryImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CountryImageTypes", new[] { "ID" });
            DropIndex("CLARITY.CountryImages", new[] { "Hash" });
            DropIndex("CLARITY.CountryImages", new[] { "Active" });
            DropIndex("CLARITY.CountryImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CountryImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.CountryImages", new[] { "CustomKey" });
            DropIndex("CLARITY.CountryImages", new[] { "Name" });
            DropIndex("CLARITY.CountryImages", new[] { "TypeID" });
            DropIndex("CLARITY.CountryImages", new[] { "MasterID" });
            DropIndex("CLARITY.CountryImages", new[] { "ID" });
            DropIndex("CLARITY.CurrencyImageTypes", new[] { "Hash" });
            DropIndex("CLARITY.CurrencyImageTypes", new[] { "Active" });
            DropIndex("CLARITY.CurrencyImageTypes", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CurrencyImageTypes", new[] { "CreatedDate" });
            DropIndex("CLARITY.CurrencyImageTypes", new[] { "CustomKey" });
            DropIndex("CLARITY.CurrencyImageTypes", new[] { "Name" });
            DropIndex("CLARITY.CurrencyImageTypes", new[] { "SortOrder" });
            DropIndex("CLARITY.CurrencyImageTypes", new[] { "DisplayName" });
            DropIndex("CLARITY.CurrencyImageTypes", new[] { "ID" });
            DropIndex("CLARITY.CurrencyImages", new[] { "Hash" });
            DropIndex("CLARITY.CurrencyImages", new[] { "Active" });
            DropIndex("CLARITY.CurrencyImages", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CurrencyImages", new[] { "CreatedDate" });
            DropIndex("CLARITY.CurrencyImages", new[] { "CustomKey" });
            DropIndex("CLARITY.CurrencyImages", new[] { "Name" });
            DropIndex("CLARITY.CurrencyImages", new[] { "TypeID" });
            DropIndex("CLARITY.CurrencyImages", new[] { "MasterID" });
            DropIndex("CLARITY.CurrencyImages", new[] { "ID" });
            DropIndex("CLARITY.HistoricalCurrencyRates", new[] { "Hash" });
            DropIndex("CLARITY.HistoricalCurrencyRates", new[] { "Active" });
            DropIndex("CLARITY.HistoricalCurrencyRates", new[] { "UpdatedDate" });
            DropIndex("CLARITY.HistoricalCurrencyRates", new[] { "CreatedDate" });
            DropIndex("CLARITY.HistoricalCurrencyRates", new[] { "CustomKey" });
            DropIndex("CLARITY.HistoricalCurrencyRates", new[] { "EndingCurrencyID" });
            DropIndex("CLARITY.HistoricalCurrencyRates", new[] { "StartingCurrencyID" });
            DropIndex("CLARITY.HistoricalCurrencyRates", new[] { "ID" });
            DropIndex("CLARITY.CurrencyConversions", new[] { "Hash" });
            DropIndex("CLARITY.CurrencyConversions", new[] { "Active" });
            DropIndex("CLARITY.CurrencyConversions", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CurrencyConversions", new[] { "CreatedDate" });
            DropIndex("CLARITY.CurrencyConversions", new[] { "CustomKey" });
            DropIndex("CLARITY.CurrencyConversions", new[] { "EndingCurrencyID" });
            DropIndex("CLARITY.CurrencyConversions", new[] { "StartingCurrencyID" });
            DropIndex("CLARITY.CurrencyConversions", new[] { "ID" });
            DropIndex("CLARITY.Currencies", new[] { "Hash" });
            DropIndex("CLARITY.Currencies", new[] { "Active" });
            DropIndex("CLARITY.Currencies", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Currencies", new[] { "CreatedDate" });
            DropIndex("CLARITY.Currencies", new[] { "CustomKey" });
            DropIndex("CLARITY.Currencies", new[] { "Name" });
            DropIndex("CLARITY.Currencies", new[] { "ID" });
            DropIndex("CLARITY.CountryCurrencies", new[] { "Hash" });
            DropIndex("CLARITY.CountryCurrencies", new[] { "Active" });
            DropIndex("CLARITY.CountryCurrencies", new[] { "UpdatedDate" });
            DropIndex("CLARITY.CountryCurrencies", new[] { "CreatedDate" });
            DropIndex("CLARITY.CountryCurrencies", new[] { "CustomKey" });
            DropIndex("CLARITY.CountryCurrencies", new[] { "SlaveID" });
            DropIndex("CLARITY.CountryCurrencies", new[] { "MasterID" });
            DropIndex("CLARITY.CountryCurrencies", new[] { "ID" });
            DropIndex("CLARITY.Countries", new[] { "Hash" });
            DropIndex("CLARITY.Countries", new[] { "Active" });
            DropIndex("CLARITY.Countries", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Countries", new[] { "CreatedDate" });
            DropIndex("CLARITY.Countries", new[] { "CustomKey" });
            DropIndex("CLARITY.Countries", new[] { "Name" });
            DropIndex("CLARITY.Countries", new[] { "ID" });
            DropIndex("CLARITY.Addresses", new[] { "Hash" });
            DropIndex("CLARITY.Addresses", new[] { "Active" });
            DropIndex("CLARITY.Addresses", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Addresses", new[] { "CreatedDate" });
            DropIndex("CLARITY.Addresses", new[] { "CustomKey" });
            DropIndex("CLARITY.Addresses", new[] { "DistrictID" });
            DropIndex("CLARITY.Addresses", new[] { "RegionID" });
            DropIndex("CLARITY.Addresses", new[] { "CountryID" });
            DropIndex("CLARITY.Addresses", new[] { "ID" });
            DropIndex("CLARITY.Contacts", new[] { "Hash" });
            DropIndex("CLARITY.Contacts", new[] { "Active" });
            DropIndex("CLARITY.Contacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Contacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.Contacts", new[] { "CustomKey" });
            DropIndex("CLARITY.Contacts", new[] { "AddressID" });
            DropIndex("CLARITY.Contacts", new[] { "TypeID" });
            DropIndex("CLARITY.Contacts", new[] { "ID" });
            DropIndex("CLARITY.AccountContacts", new[] { "Hash" });
            DropIndex("CLARITY.AccountContacts", new[] { "Active" });
            DropIndex("CLARITY.AccountContacts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountContacts", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountContacts", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountContacts", new[] { "Name" });
            DropIndex("CLARITY.AccountContacts", new[] { "SlaveID" });
            DropIndex("CLARITY.AccountContacts", new[] { "MasterID" });
            DropIndex("CLARITY.AccountContacts", new[] { "ID" });
            DropIndex("CLARITY.Accounts", new[] { "Hash" });
            DropIndex("CLARITY.Accounts", new[] { "Active" });
            DropIndex("CLARITY.Accounts", new[] { "UpdatedDate" });
            DropIndex("CLARITY.Accounts", new[] { "CreatedDate" });
            DropIndex("CLARITY.Accounts", new[] { "CustomKey" });
            DropIndex("CLARITY.Accounts", new[] { "Name" });
            DropIndex("CLARITY.Accounts", new[] { "Email" });
            DropIndex("CLARITY.Accounts", new[] { "Fax" });
            DropIndex("CLARITY.Accounts", new[] { "Phone" });
            DropIndex("CLARITY.Accounts", new[] { "CreditCurrencyID" });
            DropIndex("CLARITY.Accounts", new[] { "StatusID" });
            DropIndex("CLARITY.Accounts", new[] { "TypeID" });
            DropIndex("CLARITY.Accounts", new[] { "ParentID" });
            DropIndex("CLARITY.Accounts", new[] { "ID" });
            DropIndex("CLARITY.AccountAssociations", new[] { "Hash" });
            DropIndex("CLARITY.AccountAssociations", new[] { "Active" });
            DropIndex("CLARITY.AccountAssociations", new[] { "UpdatedDate" });
            DropIndex("CLARITY.AccountAssociations", new[] { "CreatedDate" });
            DropIndex("CLARITY.AccountAssociations", new[] { "CustomKey" });
            DropIndex("CLARITY.AccountAssociations", new[] { "TypeID" });
            DropIndex("CLARITY.AccountAssociations", new[] { "SlaveID" });
            DropIndex("CLARITY.AccountAssociations", new[] { "MasterID" });
            DropIndex("CLARITY.AccountAssociations", new[] { "ID" });
            DropTable("CLARITY.ZipCodes");
            DropTable("CLARITY.UiTranslations");
            DropTable("CLARITY.UiKeys");
            DropTable("CLARITY.SettingTypes");
            DropTable("CLARITY.SettingGroups");
            DropTable("CLARITY.Settings");
            DropTable("CLARITY.ScheduledJobConfigurationSettings");
            DropTable("CLARITY.ScheduledJobConfigurations");
            DropTable("CLARITY.ReportTypes");
            DropTable("CLARITY.Reports");
            DropTable("CLARITY.ProfanityFilters");
            DropTable("CLARITY.PriceRuleVendors");
            DropTable("CLARITY.PriceRuleStores");
            DropTable("CLARITY.PriceRuleProducts");
            DropTable("CLARITY.PriceRuleUserRoles");
            DropTable("CLARITY.PriceRuleProductTypes");
            DropTable("CLARITY.PriceRuleCountries");
            DropTable("CLARITY.PriceRuleCategories");
            DropTable("CLARITY.PriceRuleAccountTypes");
            DropTable("CLARITY.PriceRuleManufacturers");
            DropTable("CLARITY.PriceRules");
            DropTable("CLARITY.PriceRuleAccounts");
            DropTable("CLARITY.PhonePrefixLookups");
            DropTable("CLARITY.ImportExportMappings");
            DropTable("CLARITY.HistoricalTaxRates");
            DropTable("CLARITY.HistoricalAddressValidations");
            DropTable("CLARITY.HangfireSets");
            DropTable("CLARITY.HangfireServers");
            DropTable("CLARITY.HangfireSchemas");
            DropTable("CLARITY.HangfireLists");
            DropTable("CLARITY.HangfireJobQueues");
            DropTable("CLARITY.HangfireStates");
            DropTable("CLARITY.HangfireJobs");
            DropTable("CLARITY.HangfireJobParameters");
            DropTable("CLARITY.HangfireHashes");
            DropTable("CLARITY.HangfireCounters");
            DropTable("CLARITY.HangfireAggregatedCounters");
            DropTable("CLARITY.FutureImportStatus");
            DropTable("CLARITY.FutureImports");
            DropTable("CLARITY.FavoriteShipCarriers");
            DropTable("CLARITY.EventTypes");
            DropTable("CLARITY.EventStatus");
            DropTable("CLARITY.VisitStatus");
            DropTable("CLARITY.Visits");
            DropTable("CLARITY.Visitors");
            DropTable("CLARITY.PageViewTypes");
            DropTable("CLARITY.PageViewStatus");
            DropTable("CLARITY.PageViews");
            DropTable("CLARITY.PageViewEvents");
            DropTable("CLARITY.IPOrganizationStatus");
            DropTable("CLARITY.IPOrganizations");
            DropTable("CLARITY.Events");
            DropTable("CLARITY.EventLogs");
            DropTable("CLARITY.GeneralAttributePredefinedOptions");
            DropTable("CLARITY.GeneralAttributes");
            DropTable("CLARITY.AttributeTypes");
            DropTable("CLARITY.AttributeTabs");
            DropTable("CLARITY.AttributeGroups");
            DropTable("CLARITY.AccountUsageBalances");
            DropTable("CLARITY.AccountAssociationTypes");
            DropTable("CLARITY.AccountFiles");
            DropTable("CLARITY.AccountStatus");
            DropTable("CLARITY.AccountImageTypes");
            DropTable("CLARITY.AccountImages");
            DropTable("CLARITY.AccountUserRoles");
            DropTable("CLARITY.AccountPricePoints");
            DropTable("CLARITY.AccountCurrencies");
            DropTable("CLARITY.ContactTypes");
            DropTable("CLARITY.ContactImageTypes");
            DropTable("CLARITY.ContactImages");
            DropTable("CLARITY.PurchaseOrderTypes");
            DropTable("CLARITY.PurchaseOrderFiles");
            DropTable("CLARITY.PurchaseOrderStatus");
            DropTable("CLARITY.PurchaseOrderStates");
            DropTable("CLARITY.AppliedPurchaseOrderDiscounts");
            DropTable("CLARITY.PurchaseOrderContacts");
            DropTable("CLARITY.SalesOrderTypes");
            DropTable("CLARITY.SalesOrderFiles");
            DropTable("CLARITY.SalesOrderStatus");
            DropTable("CLARITY.SalesOrderStates");
            DropTable("CLARITY.SalesOrderPayments");
            DropTable("CLARITY.SalesOrderEvents");
            DropTable("CLARITY.AppliedSalesOrderDiscounts");
            DropTable("CLARITY.SalesOrderContacts");
            DropTable("CLARITY.SalesInvoiceTypes");
            DropTable("CLARITY.SalesInvoiceFiles");
            DropTable("CLARITY.SalesInvoiceStatus");
            DropTable("CLARITY.SalesInvoiceStates");
            DropTable("CLARITY.SalesInvoicePayments");
            DropTable("CLARITY.DiscountVendors");
            DropTable("CLARITY.DiscountUsers");
            DropTable("CLARITY.DiscountUserRoles");
            DropTable("CLARITY.DiscountStores");
            DropTable("CLARITY.DiscountShipCarrierMethods");
            DropTable("CLARITY.DiscountProductTypes");
            DropTable("CLARITY.DiscountManufacturers");
            DropTable("CLARITY.DiscountCountries");
            DropTable("CLARITY.DiscountCategories");
            DropTable("CLARITY.StoreTypes");
            DropTable("CLARITY.StoreContacts");
            DropTable("CLARITY.BadgeTypes");
            DropTable("CLARITY.BadgeImageTypes");
            DropTable("CLARITY.BadgeImages");
            DropTable("CLARITY.Badges");
            DropTable("CLARITY.StoreBadges");
            DropTable("CLARITY.ManufacturerTypes");
            DropTable("CLARITY.StoreCategoryTypes");
            DropTable("CLARITY.CategoryTypes");
            DropTable("CLARITY.StoreCategories");
            DropTable("CLARITY.CategoryFiles");
            DropTable("CLARITY.ProductFiles");
            DropTable("CLARITY.ProductStatus");
            DropTable("CLARITY.ProductRestrictions");
            DropTable("CLARITY.PriceRoundings");
            DropTable("CLARITY.ProductPricePoints");
            DropTable("CLARITY.ProductNotifications");
            DropTable("CLARITY.ProductCategories");
            DropTable("CLARITY.ProductAssociationTypes");
            DropTable("CLARITY.ProductAssociations");
            DropTable("CLARITY.PackageTypes");
            DropTable("CLARITY.Packages");
            DropTable("CLARITY.ManufacturerProducts");
            DropTable("CLARITY.ProductImageTypes");
            DropTable("CLARITY.ProductImages");
            DropTable("CLARITY.DiscountProducts");
            DropTable("CLARITY.CartItemTargets");
            DropTable("CLARITY.CartItemStatus");
            DropTable("CLARITY.CartTypes");
            DropTable("CLARITY.CartFiles");
            DropTable("CLARITY.CartStatus");
            DropTable("CLARITY.CartStates");
            DropTable("CLARITY.NoteTypes");
            DropTable("CLARITY.SalesOrderItemTargets");
            DropTable("CLARITY.SalesOrderItemStatus");
            DropTable("CLARITY.AppliedSalesOrderItemDiscounts");
            DropTable("CLARITY.SalesOrderItems");
            DropTable("CLARITY.SalesInvoiceItemTargets");
            DropTable("CLARITY.SalesInvoiceItemStatus");
            DropTable("CLARITY.AppliedSalesInvoiceItemDiscounts");
            DropTable("CLARITY.SalesInvoiceItems");
            DropTable("CLARITY.PurchaseOrderItemTargets");
            DropTable("CLARITY.PurchaseOrderItemStatus");
            DropTable("CLARITY.AppliedPurchaseOrderItemDiscounts");
            DropTable("CLARITY.PurchaseOrderItems");
            DropTable("CLARITY.Wallets");
            DropTable("CLARITY.ProductTypes");
            DropTable("CLARITY.UserProductTypes");
            DropTable("CLARITY.UserOnlineStatus");
            DropTable("CLARITY.UserEventAttendanceTypes");
            DropTable("CLARITY.CalendarEventTypes");
            DropTable("CLARITY.CalendarEventFiles");
            DropTable("CLARITY.CalendarEventStatus");
            DropTable("CLARITY.CalendarEventProducts");
            DropTable("CLARITY.CalendarEventImageTypes");
            DropTable("CLARITY.CalendarEventImages");
            DropTable("CLARITY.CalendarEventDetails");
            DropTable("CLARITY.CalendarEvents");
            DropTable("CLARITY.UserEventAttendances");
            DropTable("CLARITY.StoreUserTypes");
            DropTable("CLARITY.UserTypes");
            DropTable("CLARITY.ProductSubscriptionTypes");
            DropTable("CLARITY.SubscriptionTypes");
            DropTable("CLARITY.SubscriptionHistories");
            DropTable("CLARITY.StoreSubscriptions");
            DropTable("CLARITY.SubscriptionStatus");
            DropTable("CLARITY.RepeatTypes");
            DropTable("CLARITY.ZoneTypes");
            DropTable("CLARITY.ZoneStatus");
            DropTable("CLARITY.Zones");
            DropTable("CLARITY.AdTypes");
            DropTable("CLARITY.AdStores");
            DropTable("CLARITY.AdStatus");
            DropTable("CLARITY.AdImageTypes");
            DropTable("CLARITY.AdImages");
            DropTable("CLARITY.CampaignTypes");
            DropTable("CLARITY.CampaignStatus");
            DropTable("CLARITY.Campaigns");
            DropTable("CLARITY.CampaignAds");
            DropTable("CLARITY.AdAccounts");
            DropTable("CLARITY.Ads");
            DropTable("CLARITY.CounterTypes");
            DropTable("CLARITY.CounterLogTypes");
            DropTable("CLARITY.CounterLogs");
            DropTable("CLARITY.Counters");
            DropTable("CLARITY.AdZones");
            DropTable("CLARITY.AdZoneAccesses");
            DropTable("CLARITY.MembershipAdZoneAccesses");
            DropTable("CLARITY.MembershipAdZoneAccessByLevels");
            DropTable("CLARITY.MembershipLevels");
            DropTable("CLARITY.Memberships");
            DropTable("CLARITY.MembershipRepeatTypes");
            DropTable("CLARITY.ProductMembershipLevels");
            DropTable("CLARITY.Subscriptions");
            DropTable("CLARITY.StoreUsers");
            DropTable("CLARITY.UserFiles");
            DropTable("CLARITY.UserStatus");
            DropTable("CLARITY.SalesQuoteTypes");
            DropTable("CLARITY.SalesQuoteFiles");
            DropTable("CLARITY.SalesQuoteStatus");
            DropTable("CLARITY.SalesQuoteStates");
            DropTable("CLARITY.SalesQuoteCategories");
            DropTable("CLARITY.SalesQuoteItemTargets");
            DropTable("CLARITY.SalesQuoteItemStatus");
            DropTable("CLARITY.AppliedSalesQuoteItemDiscounts");
            DropTable("CLARITY.SalesQuoteItems");
            DropTable("CLARITY.SampleRequestTypes");
            DropTable("CLARITY.SampleRequestFiles");
            DropTable("CLARITY.SampleRequestStatus");
            DropTable("CLARITY.SampleRequestStates");
            DropTable("CLARITY.SampleRequestItemTargets");
            DropTable("CLARITY.SampleRequestItemStatus");
            DropTable("CLARITY.AppliedSampleRequestItemDiscounts");
            DropTable("CLARITY.SampleRequestItems");
            DropTable("CLARITY.AppliedSampleRequestDiscounts");
            DropTable("CLARITY.SampleRequestContacts");
            DropTable("CLARITY.SampleRequests");
            DropTable("CLARITY.SalesReturnTypes");
            DropTable("CLARITY.SalesReturnFiles");
            DropTable("CLARITY.SalesReturnStatus");
            DropTable("CLARITY.SalesReturnStates");
            DropTable("CLARITY.PaymentTypes");
            DropTable("CLARITY.PaymentStatus");
            DropTable("CLARITY.PaymentMethods");
            DropTable("CLARITY.Payments");
            DropTable("CLARITY.SalesReturnPayments");
            DropTable("CLARITY.SalesItemTargetTypes");
            DropTable("CLARITY.StoreProducts");
            DropTable("CLARITY.SalesReturnItemTargets");
            DropTable("CLARITY.SalesReturnItemStatus");
            DropTable("CLARITY.SalesReturnReasons");
            DropTable("CLARITY.AppliedSalesReturnItemDiscounts");
            DropTable("CLARITY.SalesReturnItems");
            DropTable("CLARITY.SalesGroups");
            DropTable("CLARITY.AppliedSalesReturnDiscounts");
            DropTable("CLARITY.SalesReturnContacts");
            DropTable("CLARITY.SalesReturnSalesOrders");
            DropTable("CLARITY.SalesReturns");
            DropTable("CLARITY.RateQuotes");
            DropTable("CLARITY.AppliedSalesQuoteDiscounts");
            DropTable("CLARITY.SalesQuoteContacts");
            DropTable("CLARITY.SalesQuoteSalesOrders");
            DropTable("CLARITY.SalesQuotes");
            DropTable("CLARITY.Permissions");
            DropTable("CLARITY.RolePermissions");
            DropTable("CLARITY.UserRoles");
            DropTable("CLARITY.RoleUsers");
            DropTable("CLARITY.ReferralCodeTypes");
            DropTable("CLARITY.ReferralCodeStatus");
            DropTable("CLARITY.ReferralCodes");
            DropTable("CLARITY.UserLogins");
            DropTable("CLARITY.UserImageTypes");
            DropTable("CLARITY.UserImages");
            DropTable("CLARITY.VendorTypes");
            DropTable("CLARITY.StoreVendors");
            DropTable("CLARITY.ShipmentTypes");
            DropTable("CLARITY.ShipmentStatus");
            DropTable("CLARITY.ShipmentEvents");
            DropTable("CLARITY.ShipCarrierMethods");
            DropTable("CLARITY.CarrierOrigins");
            DropTable("CLARITY.CarrierInvoices");
            DropTable("CLARITY.ShipCarriers");
            DropTable("CLARITY.ProductInventoryLocationSections");
            DropTable("CLARITY.StoreInventoryLocationTypes");
            DropTable("CLARITY.StoreInventoryLocations");
            DropTable("CLARITY.InventoryLocations");
            DropTable("CLARITY.InventoryLocationSections");
            DropTable("CLARITY.Shipments");
            DropTable("CLARITY.ReviewTypes");
            DropTable("CLARITY.Reviews");
            DropTable("CLARITY.VendorProducts");
            DropTable("CLARITY.VendorManufacturers");
            DropTable("CLARITY.VendorImageTypes");
            DropTable("CLARITY.VendorImages");
            DropTable("CLARITY.ContactMethods");
            DropTable("CLARITY.VendorAccounts");
            DropTable("CLARITY.Vendors");
            DropTable("CLARITY.FavoriteVendors");
            DropTable("CLARITY.FavoriteStores");
            DropTable("CLARITY.FavoriteManufacturers");
            DropTable("CLARITY.FavoriteCategories");
            DropTable("CLARITY.DiscountCodes");
            DropTable("CLARITY.GroupUsers");
            DropTable("CLARITY.GroupTypes");
            DropTable("CLARITY.GroupStatus");
            DropTable("CLARITY.Groups");
            DropTable("CLARITY.EmailTypes");
            DropTable("CLARITY.EmailStatus");
            DropTable("CLARITY.EmailTemplates");
            DropTable("CLARITY.EmailQueueAttachments");
            DropTable("CLARITY.EmailQueues");
            DropTable("CLARITY.MessageRecipients");
            DropTable("CLARITY.StoredFiles");
            DropTable("CLARITY.MessageAttachments");
            DropTable("CLARITY.Messages");
            DropTable("CLARITY.Conversations");
            DropTable("CLARITY.ConversationUsers");
            DropTable("CLARITY.UserClaims");
            DropTable("CLARITY.Users");
            DropTable("CLARITY.Notes");
            DropTable("CLARITY.AppliedCartDiscounts");
            DropTable("CLARITY.CartContacts");
            DropTable("CLARITY.Carts");
            DropTable("CLARITY.AppliedCartItemDiscounts");
            DropTable("CLARITY.CartItems");
            DropTable("CLARITY.AccountProductTypes");
            DropTable("CLARITY.AccountProducts");
            DropTable("CLARITY.Products");
            DropTable("CLARITY.CategoryImageTypes");
            DropTable("CLARITY.CategoryImages");
            DropTable("CLARITY.Categories");
            DropTable("CLARITY.ManufacturerImageTypes");
            DropTable("CLARITY.ManufacturerImages");
            DropTable("CLARITY.Manufacturers");
            DropTable("CLARITY.StoreManufacturers");
            DropTable("CLARITY.StoreImageTypes");
            DropTable("CLARITY.StoreImages");
            DropTable("CLARITY.BrandImageTypes");
            DropTable("CLARITY.BrandImages");
            DropTable("CLARITY.StoreSiteDomains");
            DropTable("CLARITY.SocialProviders");
            DropTable("CLARITY.SiteDomainSocialProviders");
            DropTable("CLARITY.SiteDomains");
            DropTable("CLARITY.BrandSiteDomains");
            DropTable("CLARITY.Brands");
            DropTable("CLARITY.BrandStores");
            DropTable("CLARITY.PricePoints");
            DropTable("CLARITY.StoreAccounts");
            DropTable("CLARITY.Stores");
            DropTable("CLARITY.AccountTypes");
            DropTable("CLARITY.DiscountAccountTypes");
            DropTable("CLARITY.DiscountAccounts");
            DropTable("CLARITY.Discounts");
            DropTable("CLARITY.AppliedSalesInvoiceDiscounts");
            DropTable("CLARITY.SalesInvoiceContacts");
            DropTable("CLARITY.SalesInvoices");
            DropTable("CLARITY.SalesOrderSalesInvoices");
            DropTable("CLARITY.SalesOrders");
            DropTable("CLARITY.SalesOrderPurchaseOrders");
            DropTable("CLARITY.PurchaseOrders");
            DropTable("CLARITY.TaxCountries");
            DropTable("CLARITY.TaxRegions");
            DropTable("CLARITY.RegionLanguages");
            DropTable("CLARITY.InterRegions");
            DropTable("CLARITY.RegionImageTypes");
            DropTable("CLARITY.RegionImages");
            DropTable("CLARITY.TaxDistricts");
            DropTable("CLARITY.DistrictLanguages");
            DropTable("CLARITY.DistrictImageTypes");
            DropTable("CLARITY.DistrictImages");
            DropTable("CLARITY.DistrictCurrencies");
            DropTable("CLARITY.Districts");
            DropTable("CLARITY.RegionCurrencies");
            DropTable("CLARITY.Regions");
            DropTable("CLARITY.LanguageImageTypes");
            DropTable("CLARITY.LanguageImages");
            DropTable("CLARITY.Languages");
            DropTable("CLARITY.CountryLanguages");
            DropTable("CLARITY.CountryImageTypes");
            DropTable("CLARITY.CountryImages");
            DropTable("CLARITY.CurrencyImageTypes");
            DropTable("CLARITY.CurrencyImages");
            DropTable("CLARITY.HistoricalCurrencyRates");
            DropTable("CLARITY.CurrencyConversions");
            DropTable("CLARITY.Currencies");
            DropTable("CLARITY.CountryCurrencies");
            DropTable("CLARITY.Countries");
            DropTable("CLARITY.Addresses");
            DropTable("CLARITY.Contacts");
            DropTable("CLARITY.AccountContacts");
            DropTable("CLARITY.Accounts");
            DropTable("CLARITY.AccountAssociations");
        }
    }
}
