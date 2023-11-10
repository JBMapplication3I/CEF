// <copyright file="202101080220571_DiscountLimits.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202101080220571 discount limits class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DiscountLimits : DbMigration
    {
        public override void Up()
        {
            AddColumn("Discounts.Discount", "UsageLimitPerAccount", c => c.Int());
            AddColumn("Discounts.Discount", "UsageLimitPerUser", c => c.Int());
            AddColumn("Discounts.Discount", "UsageLimitPerCart", c => c.Int());
            AlterColumn("Discounts.Discount", "UsageLimit", c => c.Int(nullable: true));
            RenameColumn("Discounts.Discount", "UsageLimit", "UsageLimitGlobally");
            DropColumn("Discounts.Discount", "UsageLimitByUser");
        }

        public override void Down()
        {
            AddColumn("Discounts.Discount", "UsageLimitByUser", c => c.Boolean(nullable: false, defaultValue: false));
            Sql("UPDATE [Discounts].[Discount] SET [UsageLimitGlobally] = 0 WHERE [UsageLimitGlobally] IS NULL");
            RenameColumn("Discounts.Discount", "UsageLimitGlobally", "UsageLimit");
            AlterColumn("Discounts.Discount", "UsageLimitGlobally", c => c.Int(nullable: false));
            DropColumn("Discounts.Discount", "UsageLimitGlobally");
            DropColumn("Discounts.Discount", "UsageLimitPerCart");
            DropColumn("Discounts.Discount", "UsageLimitPerUser");
            DropColumn("Discounts.Discount", "UsageLimitPerAccount");
        }
    }
}
