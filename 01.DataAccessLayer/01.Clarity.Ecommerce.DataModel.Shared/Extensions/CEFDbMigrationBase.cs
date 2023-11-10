// <copyright file="CEFDbMigrationBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF Db migration class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>A CEF database migration.</summary>
    /// <seealso cref="DbMigration"/>
    public abstract class CEFDbMigrationBase : DbMigration
    {
        public void RunSql(string sql, bool suppressTransaction = false, object? anonymousArguments = null)
        {
            Sql(sql, suppressTransaction, anonymousArguments);
        }
    }
}
