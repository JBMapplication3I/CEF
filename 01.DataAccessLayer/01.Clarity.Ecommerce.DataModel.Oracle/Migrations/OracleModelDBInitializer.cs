// <copyright file="OracleModelDBInitializer.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the model database initializer class</summary>
namespace Clarity.Ecommerce.DataModel.Oracle.Migrations
{
    using System.Data.Entity;

    public class OracleModelDBInitializer : CreateDatabaseIfNotExists<OracleClarityEcommerceEntities>
    {
        // ReSharper disable once RedundantOverriddenMember
        protected override void Seed(OracleClarityEcommerceEntities context)
        {
            // new DataSets.DefaultData(context).Populate();
            // new DataSets.IdentityData(context).Populate();
            base.Seed(context);
        }
    }
}
