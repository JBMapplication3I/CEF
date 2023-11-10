// <copyright file="201801151906449_PriceRuleNullableDates.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201801151906449 price rule nullable dates class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PriceRuleNullableDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Pricing.PriceRule", "StartDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("Pricing.PriceRule", "EndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }

        public override void Down()
        {
            AlterColumn("Pricing.PriceRule", "EndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("Pricing.PriceRule", "StartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
