// <copyright file="201705082235329_SiteDomainAlternateUrls.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201705082235329 site domain alternate urls class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SiteDomainAlternateUrls : DbMigration
    {
        public override void Up()
        {
            AddColumn("Stores.SiteDomain", "AlternateUrl1", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Stores.SiteDomain", "AlternateUrl2", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Stores.SiteDomain", "AlternateUrl3", c => c.String(maxLength: 512, unicode: false));
        }

        public override void Down()
        {
            DropColumn("Stores.SiteDomain", "AlternateUrl3");
            DropColumn("Stores.SiteDomain", "AlternateUrl2");
            DropColumn("Stores.SiteDomain", "AlternateUrl1");
        }
    }
}
