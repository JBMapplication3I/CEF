// <copyright file="201704032222033_InventoryHash.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201704032222033 inventory hash class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InventoryHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.ProductInventoryLocationSection", "InventoryHash", c => c.Long());
        }

        public override void Down()
        {
            DropColumn("Products.ProductInventoryLocationSection", "InventoryHash");
        }
    }
}
