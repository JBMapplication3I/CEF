// <copyright file="201806152035017_CartParents.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201806152035017 cart parents class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CartParents : DbMigration
    {
        public override void Up()
        {
            AddColumn("Shopping.Cart", "ParentID", c => c.Int());
            CreateIndex("Shopping.Cart", "ParentID");
            AddForeignKey("Shopping.Cart", "ParentID", "Shopping.Cart", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Shopping.Cart", "ParentID", "Shopping.Cart");
            DropIndex("Shopping.Cart", new[] { "ParentID" });
            DropColumn("Shopping.Cart", "ParentID");
        }
    }
}
