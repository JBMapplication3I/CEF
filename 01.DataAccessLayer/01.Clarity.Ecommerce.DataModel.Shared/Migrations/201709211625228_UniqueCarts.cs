// <copyright file="201709211625228_UniqueCarts.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201709211625228 unique carts class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UniqueCarts : DbMigration
    {
        public override void Up()
        {
            DropIndex("Shopping.Cart", new[] { "TypeID" });
            DropIndex("Shopping.Cart", new[] { "UserID" });
            CreateIndex("Shopping.Cart", new[] { "SessionID", "TypeID", "UserID", "Active" }, unique: true, name: "Unq_BySessionTypeUserActive");
            CreateIndex("Shopping.Cart", "TypeID");
            CreateIndex("Shopping.Cart", "UserID");
        }

        public override void Down()
        {
            DropIndex("Shopping.Cart", new[] { "UserID" });
            DropIndex("Shopping.Cart", new[] { "TypeID" });
            DropIndex("Shopping.Cart", "Unq_BySessionTypeUserActive");
            CreateIndex("Shopping.Cart", "UserID");
            CreateIndex("Shopping.Cart", "TypeID");
        }
    }
}
