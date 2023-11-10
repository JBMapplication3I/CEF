// <copyright file="202110132215060_IncludeAccountInCartCluster.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202110132215060 include account in cart cluster class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class IncludeAccountInCartCluster : DbMigration
    {
        public override void Up()
        {
            DropIndex("Shopping.Cart", "Unq_BySessionTypeUserActive");
            CreateIndex("Shopping.Cart", new[] { "Active", "SessionID", "TypeID", "UserID", "AccountID", "BrandID", "FranchiseID", "StoreID" }, unique: true, name: "Unq_ByCartClusterRequirements");
            CreateIndex("Shopping.Cart", "SessionID");
        }

        public override void Down()
        {
            DropIndex("Shopping.Cart", new[] { "SessionID" });
            DropIndex("Shopping.Cart", "Unq_ByCartClusterRequirements");
            CreateIndex("Shopping.Cart", new[] { "SessionID", "TypeID", "UserID", "Active" }, unique: true, name: "Unq_BySessionTypeUserActive");
        }
    }
}
