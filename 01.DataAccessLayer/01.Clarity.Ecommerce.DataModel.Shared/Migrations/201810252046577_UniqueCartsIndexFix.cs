// <copyright file="201810252046577_UniqueCartsIndexFix.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201810252046577 unique carts index fix class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UniqueCartsIndexFix : DbMigration
    {
        public override void Up()
        {
            DropIndex("Shopping.Cart", "Unq_BySessionTypeUserActive");
            CreateIndex("Shopping.Cart", new[] { "SessionID", "TypeID", "UserID", "Active" }, unique: true, name: "Unq_BySessionTypeUserActive");
        }

        public override void Down()
        {
            DropIndex("Shopping.Cart", "Unq_BySessionTypeUserActive");
            CreateIndex("Shopping.Cart", new[] { "SessionID", "TypeID", "UserID" }, unique: true, name: "Unq_BySessionTypeUserActive");
        }
    }
}
