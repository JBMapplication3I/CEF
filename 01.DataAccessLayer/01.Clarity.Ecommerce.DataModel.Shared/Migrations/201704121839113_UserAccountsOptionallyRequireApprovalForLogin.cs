// <copyright file="201704121839113_UserAccountsOptionallyRequireApprovalForLogin.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201704121839113 user accounts optionally require approval for login class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserAccountsOptionallyRequireApprovalForLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("Contacts.User", "IsApproved", c => c.Boolean());
        }

        public override void Down()
        {
            DropColumn("Contacts.User", "IsApproved");
        }
    }
}
