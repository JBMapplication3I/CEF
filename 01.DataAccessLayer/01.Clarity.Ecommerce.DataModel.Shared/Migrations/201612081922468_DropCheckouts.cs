// <copyright file="201612081922468_DropCheckouts.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201612081922468 drop checkouts class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DropCheckouts : DbMigration
    {
        public override void Up()
        {
            DropIndex("Shopping.Checkout", new[] { "ID" });
            DropIndex("Shopping.Checkout", new[] { "CustomKey" });
            DropIndex("Shopping.Checkout", new[] { "UpdatedDate" });
            DropIndex("Shopping.Checkout", new[] { "Active" });
            DropTable("Shopping.Checkout");
        }

        public override void Down()
        {
            CreateTable(
                "Shopping.Checkout",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateIndex("Shopping.Checkout", "Active");
            CreateIndex("Shopping.Checkout", "UpdatedDate");
            CreateIndex("Shopping.Checkout", "CustomKey");
            CreateIndex("Shopping.Checkout", "ID");
        }
    }
}
