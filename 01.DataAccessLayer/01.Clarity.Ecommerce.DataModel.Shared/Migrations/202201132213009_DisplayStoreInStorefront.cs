// <copyright file="202201132213009_DisplayStoreInStorefront.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202201132213009 display store in storefront class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DisplayStoreInStorefront : DbMigration
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
