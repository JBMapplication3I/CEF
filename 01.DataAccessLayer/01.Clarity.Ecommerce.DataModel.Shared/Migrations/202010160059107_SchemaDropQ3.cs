// <copyright file="202010160059107_SchemaDropQ3.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202010160059107 schema drop q 3 class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SchemaDropQ3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Discounts.Discount", "Priority", c => c.Int());
            DropColumn("Discounts.Discount", "UsageLimitByCart");
            DropColumn("Discounts.Discount", "PurchaseMinimum");
            DropColumn("Discounts.Discount", "PurchaseLimit");
        }

        public override void Down()
        {
            AddColumn("Discounts.Discount", "PurchaseLimit", c => c.Int(nullable: false));
            AddColumn("Discounts.Discount", "PurchaseMinimum", c => c.Int(nullable: false));
            AddColumn("Discounts.Discount", "UsageLimitByCart", c => c.Boolean(nullable: false));
            AlterColumn("Discounts.Discount", "Priority", c => c.Byte(nullable: false));
        }
    }
}
