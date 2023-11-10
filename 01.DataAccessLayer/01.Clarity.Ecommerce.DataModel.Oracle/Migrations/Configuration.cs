// <copyright file="Configuration.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the configuration class</summary>
namespace Clarity.Ecommerce.DataModel.Oracle.Migrations
{
    using System.Configuration;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<OracleClarityEcommerceEntities>
    {
        private static readonly string OracleUser = ConfigurationManager.AppSettings["OracleUser"];

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            var historyContextFactory = GetHistoryContextFactory("Oracle.ManagedDataAccess.Client");
            SetHistoryContextFactory(
                "Oracle.ManagedDataAccess.Client",
                (dbc, schema) => historyContextFactory.Invoke(dbc, OracleUser));
        }

        // ReSharper disable once RedundantOverriddenMember
        protected override void Seed(OracleClarityEcommerceEntities context)
        {
            // new DataSets.DefaultData(context).Populate();
            // new DataSets.IdentityData(context).Populate();
            base.Seed(context);
        }
    }
}
