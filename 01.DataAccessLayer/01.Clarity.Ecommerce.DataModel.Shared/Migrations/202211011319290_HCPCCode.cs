// <copyright file="202211011319290_HCPCCode.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202211011319290 hcpc code class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class HCPCCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.Product", "HCPCCode", c => c.String(maxLength: 255, unicode: false));
        }

        public override void Down()
        {
            DropColumn("Products.Product", "HCPCCode");
        }
    }
}
