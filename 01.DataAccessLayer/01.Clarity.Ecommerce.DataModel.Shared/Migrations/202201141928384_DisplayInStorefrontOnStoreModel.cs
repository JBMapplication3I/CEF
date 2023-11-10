// <copyright file="202201141928384_DisplayInStorefrontOnStoreModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the display in storefront on store model class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class DisplayInStorefrontOnStoreModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("Stores.Store", "DisplayInStorefront", c => c.Boolean());
        }

        public override void Down()
        {
            DropColumn("Stores.Store", "DisplayInStorefront");
        }
    }
}
