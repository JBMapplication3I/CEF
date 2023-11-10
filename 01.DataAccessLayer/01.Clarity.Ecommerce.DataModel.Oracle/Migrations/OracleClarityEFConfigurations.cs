// <copyright file="OracleClarityEFConfigurations.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the clarity EF configurations class</summary>
namespace Clarity.Ecommerce.DataModel.Oracle.Migrations
{
    using System;
    using System.Data.Entity;

    public class OracleClarityEFConfigurations : DbConfiguration
    {
        public OracleClarityEFConfigurations()
        {
            SetExecutionStrategy(
                "Oracle.ManagedDataAccess.Client",
                () => new ClarityCustomExecutionStrategy(10, TimeSpan.FromSeconds(30)));
        }
    }
}
