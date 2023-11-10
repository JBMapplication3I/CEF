// <copyright file="ModelDBInitializer.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the model database initializer class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity;
    using DataSets;

    public class ModelDBInitializer : CreateDatabaseIfNotExists<ClarityEcommerceEntities>
    {
        protected override void Seed(ClarityEcommerceEntities context)
        {
            new DefaultData(context).Populate();
            new IdentityData(context).Populate();
            base.Seed(context);
        }
    }
}
