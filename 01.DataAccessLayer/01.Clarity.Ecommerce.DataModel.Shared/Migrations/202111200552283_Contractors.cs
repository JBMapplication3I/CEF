// <copyright file="202111200552283_Contractors.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202111200552283 contractors class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Contractors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("System.Note", "Appointment_ID", "Scheduling.Appointment");
            DropIndex("System.Note", new[] { "Appointment_ID" });
            CreateTable(
                "Accounts.Contractor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(),
                        UserID = c.Int(),
                        StoreID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID)
                .ForeignKey("Stores.Store", t => t.StoreID)
                .ForeignKey("Contacts.User", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.AccountID)
                .Index(t => t.UserID)
                .Index(t => t.StoreID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Accounts.ServiceArea",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Radius = c.Decimal(precision: 18, scale: 2),
                        ContractorID = c.Int(nullable: false),
                        AddressID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Geography.Address", t => t.AddressID, cascadeDelete: true)
                .ForeignKey("Accounts.Contractor", t => t.ContractorID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.ContractorID)
                .Index(t => t.AddressID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            DropColumn("System.Note", "Appointment_ID");
        }

        public override void Down()
        {
            AddColumn("System.Note", "Appointment_ID", c => c.Int());
            DropForeignKey("Accounts.Contractor", "UserID", "Contacts.User");
            DropForeignKey("Accounts.Contractor", "StoreID", "Stores.Store");
            DropForeignKey("Accounts.ServiceArea", "ContractorID", "Accounts.Contractor");
            DropForeignKey("Accounts.ServiceArea", "AddressID", "Geography.Address");
            DropForeignKey("Accounts.Contractor", "AccountID", "Accounts.Account");
            DropIndex("Accounts.ServiceArea", new[] { "Hash" });
            DropIndex("Accounts.ServiceArea", new[] { "Active" });
            DropIndex("Accounts.ServiceArea", new[] { "UpdatedDate" });
            DropIndex("Accounts.ServiceArea", new[] { "CreatedDate" });
            DropIndex("Accounts.ServiceArea", new[] { "CustomKey" });
            DropIndex("Accounts.ServiceArea", new[] { "AddressID" });
            DropIndex("Accounts.ServiceArea", new[] { "ContractorID" });
            DropIndex("Accounts.ServiceArea", new[] { "ID" });
            DropIndex("Accounts.Contractor", new[] { "Hash" });
            DropIndex("Accounts.Contractor", new[] { "Active" });
            DropIndex("Accounts.Contractor", new[] { "UpdatedDate" });
            DropIndex("Accounts.Contractor", new[] { "CreatedDate" });
            DropIndex("Accounts.Contractor", new[] { "CustomKey" });
            DropIndex("Accounts.Contractor", new[] { "StoreID" });
            DropIndex("Accounts.Contractor", new[] { "UserID" });
            DropIndex("Accounts.Contractor", new[] { "AccountID" });
            DropIndex("Accounts.Contractor", new[] { "ID" });
            DropTable("Accounts.ServiceArea");
            DropTable("Accounts.Contractor");
            CreateIndex("System.Note", "Appointment_ID");
            AddForeignKey("System.Note", "Appointment_ID", "Scheduling.Appointment", "ID");
        }
    }
}
