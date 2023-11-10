// <copyright file="202107301623530_Bids.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202107301623530 bids class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Bids : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.Product", "BiddingReserve", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Accounts.AccountProduct", "MaxBid", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Accounts.AccountProduct", "CurrentBid", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Accounts.AccountProduct", "NoShow", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
            DropColumn("Accounts.AccountProduct", "NoShow");
            DropColumn("Accounts.AccountProduct", "CurrentBid");
            DropColumn("Accounts.AccountProduct", "MaxBid");
            DropColumn("Products.Product", "BiddingReserve");
        }
    }
}
