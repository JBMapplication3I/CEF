// <copyright file="Configuration.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the configuration class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ClarityEcommerceEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        // ReSharper disable once RedundantOverriddenMember
        protected override void Seed(ClarityEcommerceEntities context)
        {
            // new DataSets.DefaultData(context).Populate();
            // new DataSets.IdentityData(context).Populate();
            base.Seed(context);
        }
    }
}
