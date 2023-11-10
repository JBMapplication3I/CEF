// <copyright file="201902181819454_WalletWithEchecks.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201902181819454 wallet with echecks class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class WalletWithEchecks : DbMigration
    {
        public override void Up()
        {
            AddColumn("Payments.Wallet", "AccountNumber", c => c.String(maxLength: 100, unicode: false));
            AddColumn("Payments.Wallet", "RoutingNumber", c => c.String(maxLength: 100, unicode: false));
            AddColumn("Payments.Wallet", "BankName", c => c.String(maxLength: 128, unicode: false));
        }

        public override void Down()
        {
            DropColumn("Payments.Wallet", "BankName");
            DropColumn("Payments.Wallet", "RoutingNumber");
            DropColumn("Payments.Wallet", "AccountNumber");
        }
    }
}
