// <copyright file="201708211733285_QuoteCategories.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201708211733285 quote categories class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class QuoteCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Quoting.SalesQuoteCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        SalesQuoteID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Categories.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("Quoting.SalesQuote", t => t.SalesQuoteID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.SalesQuoteID)
                .Index(t => t.CategoryID);
        }

        public override void Down()
        {
            DropForeignKey("Quoting.SalesQuoteCategory", "SalesQuoteID", "Quoting.SalesQuote");
            DropForeignKey("Quoting.SalesQuoteCategory", "CategoryID", "Categories.Category");
            DropIndex("Quoting.SalesQuoteCategory", new[] { "CategoryID" });
            DropIndex("Quoting.SalesQuoteCategory", new[] { "SalesQuoteID" });
            DropIndex("Quoting.SalesQuoteCategory", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteCategory", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteCategory", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteCategory", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteCategory", new[] { "ID" });
            DropTable("Quoting.SalesQuoteCategory");
        }
    }
}
