// <copyright file="201701102149344_FixTriggerName.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201701102149344 fix trigger name class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixTriggerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tracking.Event", "TotalTriggers", c => c.Int());
            AddColumn("Tracking.PageView", "TotalTriggers", c => c.Int());
            AddColumn("Tracking.Visit", "TotalTriggers", c => c.Int());
            DropColumn("Tracking.Event", "TotalTrigggers");
            DropColumn("Tracking.PageView", "TotalTrigggers");
            DropColumn("Tracking.Visit", "TotalTrigggers");
        }

        public override void Down()
        {
            AddColumn("Tracking.Visit", "TotalTrigggers", c => c.Int());
            AddColumn("Tracking.PageView", "TotalTrigggers", c => c.Int());
            AddColumn("Tracking.Event", "TotalTrigggers", c => c.Int());
            DropColumn("Tracking.Visit", "TotalTriggers");
            DropColumn("Tracking.PageView", "TotalTriggers");
            DropColumn("Tracking.Event", "TotalTriggers");
        }
    }
}
