// <copyright file="201810101538291_StoredFilesOnAccountsAndUsers.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201810101538291 stored files on accounts and users class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class StoredFilesOnAccountsAndUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Contacts.UserFile",
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
                    SeoKeywords = c.String(maxLength: 512, unicode: false),
                    SeoUrl = c.String(maxLength: 512, unicode: false),
                    SeoPageTitle = c.String(maxLength: 75, unicode: false),
                    SeoDescription = c.String(maxLength: 256, unicode: false),
                    SeoMetaData = c.String(maxLength: 512, unicode: false),
                    MasterID = c.Int(nullable: false),
                    SlaveID = c.Int(nullable: false),
                    FileAccessTypeID = c.Int(nullable: false),
                    SortOrder = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Accounts.AccountFile",
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
                    SeoKeywords = c.String(maxLength: 512, unicode: false),
                    SeoUrl = c.String(maxLength: 512, unicode: false),
                    SeoPageTitle = c.String(maxLength: 75, unicode: false),
                    SeoDescription = c.String(maxLength: 256, unicode: false),
                    SeoMetaData = c.String(maxLength: 512, unicode: false),
                    MasterID = c.Int(nullable: false),
                    SlaveID = c.Int(nullable: false),
                    FileAccessTypeID = c.Int(nullable: false),
                    SortOrder = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Media.StoredFile", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);
        }

        public override void Down()
        {
            DropForeignKey("Accounts.AccountFile", "SlaveID", "Media.StoredFile");
            DropForeignKey("Accounts.AccountFile", "MasterID", "Accounts.Account");
            DropForeignKey("Contacts.UserFile", "SlaveID", "Media.StoredFile");
            DropForeignKey("Contacts.UserFile", "MasterID", "Contacts.User");
            DropIndex("Accounts.AccountFile", new[] { "SlaveID" });
            DropIndex("Accounts.AccountFile", new[] { "MasterID" });
            DropIndex("Accounts.AccountFile", new[] { "Name" });
            DropIndex("Accounts.AccountFile", new[] { "Hash" });
            DropIndex("Accounts.AccountFile", new[] { "Active" });
            DropIndex("Accounts.AccountFile", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountFile", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountFile", new[] { "CustomKey" });
            DropIndex("Accounts.AccountFile", new[] { "ID" });
            DropIndex("Contacts.UserFile", new[] { "SlaveID" });
            DropIndex("Contacts.UserFile", new[] { "MasterID" });
            DropIndex("Contacts.UserFile", new[] { "Name" });
            DropIndex("Contacts.UserFile", new[] { "Hash" });
            DropIndex("Contacts.UserFile", new[] { "Active" });
            DropIndex("Contacts.UserFile", new[] { "UpdatedDate" });
            DropIndex("Contacts.UserFile", new[] { "CreatedDate" });
            DropIndex("Contacts.UserFile", new[] { "CustomKey" });
            DropIndex("Contacts.UserFile", new[] { "ID" });
            DropTable("Accounts.AccountFile");
            DropTable("Contacts.UserFile");
        }
    }
}
