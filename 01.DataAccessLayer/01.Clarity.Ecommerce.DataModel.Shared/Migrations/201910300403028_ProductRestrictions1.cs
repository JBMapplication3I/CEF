// <copyright file="201910300403028_ProductRestrictions1.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201910300403028 product restrictions 1 class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ProductRestrictions1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.Product", "RequiresRolesAlt", c => c.String(maxLength: 512));
            AddColumn("Products.Product", "MustPurchaseInMultiplesOfAmountWarningMessage", c => c.String(maxLength: 1024));
            AddColumn("Categories.Category", "RequiresRolesAlt", c => c.String(maxLength: 512));
        }

        public override void Down()
        {
            DropColumn("Categories.Category", "RequiresRolesAlt");
            DropColumn("Products.Product", "MustPurchaseInMultiplesOfAmountWarningMessage");
            DropColumn("Products.Product", "RequiresRolesAlt");
        }
    }
}
