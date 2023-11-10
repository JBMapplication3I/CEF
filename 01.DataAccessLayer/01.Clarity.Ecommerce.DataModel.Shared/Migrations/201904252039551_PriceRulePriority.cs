// <copyright file="201904252039551_PriceRulePriority.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201904252039551 price rule priority class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PriceRulePriority : DbMigration
    {
        public override void Up()
        {
            AddColumn("Pricing.PriceRule", "Priority", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("Pricing.PriceRule", "Priority");
        }
    }
}
