// <copyright file="201704140207336_CategoryHashes.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201704140207336 category hashes class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CategoryHashes : DbMigration
    {
        public override void Up()
        {
            AddColumn("Categories.Category", "CategoryHash", c => c.Long());
            CreateIndex("Categories.Category", "CategoryHash");
        }

        public override void Down()
        {
            DropIndex("Categories.Category", new[] { "CategoryHash" });
            DropColumn("Categories.Category", "CategoryHash");
        }
    }
}
