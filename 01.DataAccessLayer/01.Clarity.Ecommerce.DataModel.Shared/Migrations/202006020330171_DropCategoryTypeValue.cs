// <copyright file="202006020330171_DropCategoryTypeValue.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the drop category type value class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DropCategoryTypeValue : DbMigration
    {
        public override void Up()
        {
            DropColumn("Categories.CategoryType", "Value");
        }

        public override void Down()
        {
            AddColumn("Categories.CategoryType", "Value", c => c.String(maxLength: 2500));
        }
    }
}
