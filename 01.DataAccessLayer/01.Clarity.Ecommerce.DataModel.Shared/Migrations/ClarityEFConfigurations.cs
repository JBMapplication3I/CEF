// <copyright file="ClarityEFConfigurations.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the clarity ef configurations class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity;

    public class ClarityEFConfigurations : DbConfiguration
    {
        public ClarityEFConfigurations()
        {
            SetExecutionStrategy(
                "System.Data.SqlClient",
                () => new ClarityCustomExecutionStrategy(10, TimeSpan.FromSeconds(30)));
        }
    }
}
