// <copyright file="OracleDataModelRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the DataModel StructureMap 4 Registry to associate the interfaces with their concretes</summary>
// ReSharper disable MissingXmlDoc, UnusedMember.Global
#pragma warning disable 618
namespace Clarity.Ecommerce.DataModel.Oracle
{
    using System.Data.Entity;
    using Interfaces.DataModel;
    using StructureMap;

    /// <summary>An oracle data model registry.</summary>
    /// <seealso cref="Registry"/>
    public class OracleDataModelRegistry : Registry
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="OracleDataModelRegistry"/> class.</summary>
        public OracleDataModelRegistry()
        {
            var usingOracleDB = System.Configuration.ConfigurationManager.AppSettings["UsingOracle"];
            if (usingOracleDB != "true")
            {
                return;
            }
            For<IClarityEcommerceEntities>().Use<OracleClarityEcommerceEntities>();
            For<IDbContext>().Use<OracleClarityEcommerceEntities>();
            For<DbContext>().Use<OracleClarityEcommerceEntities>();
        }
    }
}
