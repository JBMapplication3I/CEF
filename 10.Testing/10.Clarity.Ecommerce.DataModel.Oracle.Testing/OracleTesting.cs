// <copyright file="OracleTesting.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the oracle testing class</summary>
namespace Clarity.Ecommerce.DataModel.Oracle.Testing
{
    using System.Data.Entity;
    using System.Linq;
    using JSConfigs;
    using Xunit;
    using Configuration = Migrations.Configuration;

    [Trait("Category", "Oracle")]
    public class OracleTesting
    {
        [Fact]
        public void VerifyMostBasicCall()
        {
            CEFConfigDictionary.Load();
            // Update to the latest migration on first initialization
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OracleClarityEcommerceEntities, Configuration>());
            var configuration = new Configuration();
            var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();
            }
            using var context = new OracleClarityEcommerceEntities();
            var list = context.UserTypes.Select(x => x.CustomKey).ToList();
            Assert.NotNull(list);
        }

        [Fact]
        public void VerifyInsert()
        {
            using var context = new OracleClarityEcommerceEntities();
            var userType = new UserType
            {
                CustomKey = "TestCustomKey",
            };
            context.UserTypes.Add(userType);
            context.SaveChanges();
            Assert.True(userType.ID != 0);
        }
    }
}
