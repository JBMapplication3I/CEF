// <copyright file="201811282338482_NothingToShipProducts.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201811282338482 nothing to ship products class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class NothingToShipProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.Product", "NothingToShip", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("Products.Product", "NothingToShip");
        }
    }
}
