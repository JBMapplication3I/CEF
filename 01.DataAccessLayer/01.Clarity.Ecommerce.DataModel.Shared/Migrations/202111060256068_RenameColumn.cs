// <copyright file="202111060256068_RenameColumn.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the rename column class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenameColumn : DbMigration
    {
        public override void Up()
        {
            RenameColumn("Brands.BrandAccount", "HasAccessToBrand", "IsVisibleIn");
        }

        public override void Down()
        {
            RenameColumn("Brands.BrandAccount", "IsVisibleIn", "HasAccessToBrand");
        }
    }
}
