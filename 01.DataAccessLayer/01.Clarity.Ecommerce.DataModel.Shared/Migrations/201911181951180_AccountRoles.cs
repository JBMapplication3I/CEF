// <copyright file="201911181951180_AccountRoles.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201911181951180 account roles class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AccountRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Accounts.AccountUserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Contacts.UserRole", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);
        }

        public override void Down()
        {
            DropForeignKey("Accounts.AccountUserRole", "SlaveID", "Contacts.UserRole");
            DropForeignKey("Accounts.AccountUserRole", "MasterID", "Accounts.Account");
            DropIndex("Accounts.AccountUserRole", new[] { "SlaveID" });
            DropIndex("Accounts.AccountUserRole", new[] { "MasterID" });
            DropIndex("Accounts.AccountUserRole", new[] { "Hash" });
            DropIndex("Accounts.AccountUserRole", new[] { "Active" });
            DropIndex("Accounts.AccountUserRole", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountUserRole", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountUserRole", new[] { "CustomKey" });
            DropIndex("Accounts.AccountUserRole", new[] { "ID" });
            DropTable("Accounts.AccountUserRole");
        }
    }
}
