// <copyright file="SeedDatabase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SeedDatabase class</summary>
namespace Clarity.Ecommerce.SeedDatabase
{
    ////using Clarity.Ecommerce.DataModel.Oracle;
    using DataModel;
    using DataModel.DataSets;
    using Xunit;

    [Trait("Category", "Seed Database")]
    public class SeedDatabase
    {
        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void SeedDefaultData()
        {
            using var context = new ClarityEcommerceEntities();
            new DefaultData(context).Populate();
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void SeedIdentityData()
        {
            using var context = new ClarityEcommerceEntities();
            new IdentityData(context).Populate();
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void SeedSampleData()
        {
            using var context = new ClarityEcommerceEntities();
            new SampleData(context).Populate();
        }

        [Fact] // Intentionally not skipped and wrapped in a try/catch to ignore errors for CI/CD
        public void RunAllSeedDataInOrder()
        {
            try
            {
                using var context = new ClarityEcommerceEntities();
                new DefaultData(context).Populate();
                new IdentityData(context).Populate();
                new SampleData(context).Populate();
            }
            catch
            {
                // Do Nothing
            }
        }

        /*
        [Fact(Skip = "Comment out the Skip when you need to seed the Oracle database")]
        public void SeedOracleDefaultData()
        {
            using var context = new OracleClarityEcommerceEntities();
            new DataModel.Oracle.DataSets.DefaultData(context).Populate();
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the Oracle database")]
        public void SeedOracleIdentityData()
        {
            using var context = new OracleClarityEcommerceEntities();
            new DataModel.Oracle.DataSets.IdentityData(context).Populate();
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the Oracle database")]
        public void SeedOracleSampleData()
        {
            using var context = new OracleClarityEcommerceEntities();
            new DataModel.Oracle.DataSets.SampleData(context).Populate();
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the Oracle database")]
        public void RunAllOracleSeedDataInOrder()
        {
            try
            {
                using var context = new OracleClarityEcommerceEntities();
                new DataModel.Oracle.DataSets.DefaultData(context).Populate();
                new DataModel.Oracle.DataSets.IdentityData(context).Populate();
                new DataModel.Oracle.DataSets.SampleData(context).Populate();
            }
            catch
            {
                // Do Nothing
            }
        }
        */
    }
}
