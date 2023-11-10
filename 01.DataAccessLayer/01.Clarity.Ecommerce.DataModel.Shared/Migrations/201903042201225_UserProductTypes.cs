// <copyright file="201903042201225_UserProductTypes.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201903042201225 user product types class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserProductTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Contacts.UserProductType",
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.ProductType", t => t.SlaveID, cascadeDelete: true)
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
            DropForeignKey("Contacts.UserProductType", "SlaveID", "Products.ProductType");
            DropForeignKey("Contacts.UserProductType", "MasterID", "Contacts.User");
            DropIndex("Contacts.UserProductType", new[] { "SlaveID" });
            DropIndex("Contacts.UserProductType", new[] { "MasterID" });
            DropIndex("Contacts.UserProductType", new[] { "Hash" });
            DropIndex("Contacts.UserProductType", new[] { "Active" });
            DropIndex("Contacts.UserProductType", new[] { "UpdatedDate" });
            DropIndex("Contacts.UserProductType", new[] { "CreatedDate" });
            DropIndex("Contacts.UserProductType", new[] { "CustomKey" });
            DropIndex("Contacts.UserProductType", new[] { "ID" });
            DropTable("Contacts.UserProductType");
        }
    }
}
