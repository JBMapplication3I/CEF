// <copyright file="202103120204105_WalletContacts.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202103120204105 wallet contacts class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class WalletContacts : DbMigration
    {
        public override void Up()
        {
            AddColumn("Payments.Wallet", "IsDefault", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("Payments.Wallet", "AccountContactID", c => c.Int());
            CreateIndex("Payments.Wallet", "AccountContactID");
            AddForeignKey("Payments.Wallet", "AccountContactID", "Accounts.AccountContact", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Payments.Wallet", "AccountContactID", "Accounts.AccountContact");
            DropIndex("Payments.Wallet", new[] { "AccountContactID" });
            DropColumn("Payments.Wallet", "AccountContactID");
            DropColumn("Payments.Wallet", "IsDefault");
        }
    }
}
