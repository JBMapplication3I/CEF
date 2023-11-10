// <copyright file="201912030141238_ForceUserPWResetAndProductDropShipOnly.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201912030141238 force user password reset and product drop ship only class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ForceUserPWResetAndProductDropShipOnly : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.Product", "DropShipOnly", c => c.Boolean(nullable: false));
            AddColumn("Contacts.User", "RequirePasswordChangeOnNextLogin", c => c.Boolean(nullable: false));
            Sql("UPDATE [Contacts].[User] SET [IsApproved] = 1 WHERE [IsApproved] IS NULL");
            AlterColumn("Contacts.User", "IsApproved", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("Contacts.User", "IsApproved", c => c.Boolean());
            DropColumn("Contacts.User", "RequirePasswordChangeOnNextLogin");
            DropColumn("Products.Product", "DropShipOnly");
        }
    }
}
