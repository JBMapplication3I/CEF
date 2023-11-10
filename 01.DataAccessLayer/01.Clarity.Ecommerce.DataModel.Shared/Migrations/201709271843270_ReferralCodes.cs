// <copyright file="201709271843270_ReferralCodes.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201709271843270 referral codes class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ReferralCodes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Contacts.ReferralCode",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        JsonAttributes = c.String(),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 128, unicode: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.ReferralCodeStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Contacts.ReferralCodeType", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.UserID);

            CreateTable(
                "Contacts.ReferralCodeStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Contacts.ReferralCodeType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);
        }

        public override void Down()
        {
            DropForeignKey("Contacts.ReferralCode", "UserID", "Contacts.User");
            DropForeignKey("Contacts.ReferralCode", "TypeID", "Contacts.ReferralCodeType");
            DropForeignKey("Contacts.ReferralCode", "StatusID", "Contacts.ReferralCodeStatus");
            DropIndex("Contacts.ReferralCodeType", new[] { "SortOrder" });
            DropIndex("Contacts.ReferralCodeType", new[] { "DisplayName" });
            DropIndex("Contacts.ReferralCodeType", new[] { "Name" });
            DropIndex("Contacts.ReferralCodeType", new[] { "Hash" });
            DropIndex("Contacts.ReferralCodeType", new[] { "Active" });
            DropIndex("Contacts.ReferralCodeType", new[] { "UpdatedDate" });
            DropIndex("Contacts.ReferralCodeType", new[] { "CustomKey" });
            DropIndex("Contacts.ReferralCodeType", new[] { "ID" });
            DropIndex("Contacts.ReferralCodeStatus", new[] { "SortOrder" });
            DropIndex("Contacts.ReferralCodeStatus", new[] { "DisplayName" });
            DropIndex("Contacts.ReferralCodeStatus", new[] { "Name" });
            DropIndex("Contacts.ReferralCodeStatus", new[] { "Hash" });
            DropIndex("Contacts.ReferralCodeStatus", new[] { "Active" });
            DropIndex("Contacts.ReferralCodeStatus", new[] { "UpdatedDate" });
            DropIndex("Contacts.ReferralCodeStatus", new[] { "CustomKey" });
            DropIndex("Contacts.ReferralCodeStatus", new[] { "ID" });
            DropIndex("Contacts.ReferralCode", new[] { "UserID" });
            DropIndex("Contacts.ReferralCode", new[] { "StatusID" });
            DropIndex("Contacts.ReferralCode", new[] { "TypeID" });
            DropIndex("Contacts.ReferralCode", new[] { "Name" });
            DropIndex("Contacts.ReferralCode", new[] { "Hash" });
            DropIndex("Contacts.ReferralCode", new[] { "Active" });
            DropIndex("Contacts.ReferralCode", new[] { "UpdatedDate" });
            DropIndex("Contacts.ReferralCode", new[] { "CustomKey" });
            DropIndex("Contacts.ReferralCode", new[] { "ID" });
            DropTable("Contacts.ReferralCodeType");
            DropTable("Contacts.ReferralCodeStatus");
            DropTable("Contacts.ReferralCode");
        }
    }
}
