// <copyright file="202111292031386_AutoPay.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202111292031386 automatic pay class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AutoPay : DbMigration
    {
        public override void Up()
        {
            AddColumn("Contacts.User", "UseAutoPay", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("Contacts.User", "UseAutoPay");
        }
    }
}
