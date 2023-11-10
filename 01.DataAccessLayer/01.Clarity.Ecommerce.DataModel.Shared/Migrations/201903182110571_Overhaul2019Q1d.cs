// <copyright file="201903182110571_Overhaul2019Q1d.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201903182110571 overhaul 2019 q 1d class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Overhaul2019Q1d : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "Stores.BrandStore", newSchema: "Brands");
            MoveTable(name: "Stores.Brand", newSchema: "Brands");
            MoveTable(name: "Stores.BrandSiteDomain", newSchema: "Brands");
            MoveTable(name: "Stores.BrandImage", newSchema: "Brands");
            MoveTable(name: "Stores.BrandImageType", newSchema: "Brands");
            MoveTable(name: "Stores.Badge", newSchema: "Badges");
            MoveTable(name: "Stores.BadgeImage", newSchema: "Badges");
            MoveTable(name: "Stores.BadgeImageType", newSchema: "Badges");
            MoveTable(name: "Stores.BadgeType", newSchema: "Badges");
        }

        public override void Down()
        {
            MoveTable(name: "Badges.BadgeType", newSchema: "Stores");
            MoveTable(name: "Badges.BadgeImageType", newSchema: "Stores");
            MoveTable(name: "Badges.BadgeImage", newSchema: "Stores");
            MoveTable(name: "Badges.Badge", newSchema: "Stores");
            MoveTable(name: "Brands.BrandImageType", newSchema: "Stores");
            MoveTable(name: "Brands.BrandImage", newSchema: "Stores");
            MoveTable(name: "Brands.BrandSiteDomain", newSchema: "Stores");
            MoveTable(name: "Brands.Brand", newSchema: "Stores");
            MoveTable(name: "Brands.BrandStore", newSchema: "Stores");
        }
    }
}
