// <copyright file="202202041920550_FixLotGroupAssignment.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202202041920550 fix lot group assignment class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixLotGroupAssignment : DbMigration
    {
        public override void Up()
        {
            AddColumn("Auctions.Lot", "LotGroupID", c => c.Int());
            CreateIndex("Auctions.Lot", "LotGroupID");
            AddForeignKey("Auctions.Lot", "LotGroupID", "Auctions.LotGroup", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Auctions.Lot", "LotGroupID", "Auctions.LotGroup");
            DropIndex("Auctions.Lot", new[] { "LotGroupID" });
            DropColumn("Auctions.Lot", "LotGroupID");
        }
    }
}
