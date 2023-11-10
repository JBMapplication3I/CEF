// <copyright file="202007092155244_OracleRoleUserOptionalGroup.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202007092155244 oracle role user optional group class</summary>
namespace Clarity.Ecommerce.DataModel.Oracle.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OracleRoleUserOptionalGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("CLARITY.RoleUsers", "GroupID", "CLARITY.Groups");
            DropIndex("CLARITY.RoleUsers", new[] { "GroupID" });
            DropPrimaryKey("CLARITY.RoleUsers");
            AlterColumn("CLARITY.RoleUsers", "GroupID", c => c.Decimal(precision: 10, scale: 0));
            AddPrimaryKey("CLARITY.RoleUsers", new[] { "RoleId", "UserId" });
            CreateIndex("CLARITY.RoleUsers", "GroupID");
            AddForeignKey("CLARITY.RoleUsers", "GroupID", "CLARITY.Groups", "ID");
        }

        public override void Down()
        {
            DropForeignKey("CLARITY.RoleUsers", "GroupID", "CLARITY.Groups");
            DropIndex("CLARITY.RoleUsers", new[] { "GroupID" });
            DropPrimaryKey("CLARITY.RoleUsers");
            AlterColumn("CLARITY.RoleUsers", "GroupID", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddPrimaryKey("CLARITY.RoleUsers", new[] { "RoleId", "UserId", "GroupID" });
            CreateIndex("CLARITY.RoleUsers", "GroupID");
            AddForeignKey("CLARITY.RoleUsers", "GroupID", "CLARITY.Groups", "ID", cascadeDelete: true);
        }
    }
}
