// <copyright file="201704172328247_StoreProductHash.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201704172328247 store product hash class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class StoreProductHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("Stores.StoreProduct", "PriceBase", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Stores.StoreProduct", "PriceMsrp", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Stores.StoreProduct", "PriceReduction", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Stores.StoreProduct", "PriceSale", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Stores.StoreProduct", "StoreProductHash", c => c.Long());
        }

        public override void Down()
        {
            DropColumn("Stores.StoreProduct", "StoreProductHash");
            DropColumn("Stores.StoreProduct", "PriceSale");
            DropColumn("Stores.StoreProduct", "PriceReduction");
            DropColumn("Stores.StoreProduct", "PriceMsrp");
            DropColumn("Stores.StoreProduct", "PriceBase");
        }
    }
}
