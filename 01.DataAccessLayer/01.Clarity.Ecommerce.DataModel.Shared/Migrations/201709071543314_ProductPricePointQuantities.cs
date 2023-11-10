// <copyright file="201709071543314_ProductPricePointQuantities.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201709071543314 product price point quantities class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ProductPricePointQuantities : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Products.ProductPricePoint", "MinQuantity", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("Products.ProductPricePoint", "MaxQuantity", c => c.Decimal(precision: 18, scale: 4));
        }

        public override void Down()
        {
            AlterColumn("Products.ProductPricePoint", "MaxQuantity", c => c.Int());
            AlterColumn("Products.ProductPricePoint", "MinQuantity", c => c.Int());
        }
    }
}
