// <copyright file="201612230147293_DropFavoritesCustomersAndIndividuals.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201612230147293 drop favorites customers and individuals class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DropFavoritesCustomersAndIndividuals : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Contacts.Favorite", "ProductID", "Products.Product");
            DropForeignKey("Contacts.Favorite", "UserID", "Contacts.User");
            DropForeignKey("Contacts.Customer", "ID", "Contacts.Contact");
            DropForeignKey("Contacts.Individual", "ID", "Contacts.Contact");
            DropIndex("Contacts.Favorite", new[] { "ID" });
            DropIndex("Contacts.Favorite", new[] { "CustomKey" });
            DropIndex("Contacts.Favorite", new[] { "UpdatedDate" });
            DropIndex("Contacts.Favorite", new[] { "Active" });
            DropIndex("Contacts.Favorite", new[] { "ProductID" });
            DropIndex("Contacts.Favorite", new[] { "UserID" });
            DropIndex("Contacts.Customer", new[] { "ID" });
            DropIndex("Contacts.Customer", new[] { "CustomKey" });
            DropIndex("Contacts.Customer", new[] { "UpdatedDate" });
            DropIndex("Contacts.Customer", new[] { "Active" });
            DropIndex("Contacts.Individual", new[] { "ID" });
            DropIndex("Contacts.Individual", new[] { "CustomKey" });
            DropIndex("Contacts.Individual", new[] { "UpdatedDate" });
            DropIndex("Contacts.Individual", new[] { "Active" });
            DropTable("Contacts.Favorite");
            DropTable("Contacts.Customer");
            DropTable("Contacts.Individual");
        }

        public override void Down()
        {
            CreateTable(
                "Contacts.Individual",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Contacts.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Contacts.Favorite",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SessionID = c.Guid(),
                        ProductID = c.Int(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateIndex("Contacts.Individual", "Active");
            CreateIndex("Contacts.Individual", "UpdatedDate");
            CreateIndex("Contacts.Individual", "CustomKey");
            CreateIndex("Contacts.Individual", "ID");
            CreateIndex("Contacts.Customer", "Active");
            CreateIndex("Contacts.Customer", "UpdatedDate");
            CreateIndex("Contacts.Customer", "CustomKey");
            CreateIndex("Contacts.Customer", "ID");
            CreateIndex("Contacts.Favorite", "UserID");
            CreateIndex("Contacts.Favorite", "ProductID");
            CreateIndex("Contacts.Favorite", "Active");
            CreateIndex("Contacts.Favorite", "UpdatedDate");
            CreateIndex("Contacts.Favorite", "CustomKey");
            CreateIndex("Contacts.Favorite", "ID");
            AddForeignKey("Contacts.Individual", "ID", "Contacts.Contact", "ID");
            AddForeignKey("Contacts.Customer", "ID", "Contacts.Contact", "ID");
            AddForeignKey("Contacts.Favorite", "UserID", "Contacts.User", "ID");
            AddForeignKey("Contacts.Favorite", "ProductID", "Products.Product", "ID");
        }
    }
}
