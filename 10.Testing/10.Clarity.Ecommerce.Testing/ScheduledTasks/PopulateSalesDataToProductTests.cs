// <copyright file="PopulateSalesDataToProductTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the populate sales data to product tests class</summary>
namespace Clarity.Ecommerce.Tasks.PopulateSalesDataToProducts.Testing
{
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ScheduledTasks.PopulateSalesDataToProduct")]
    public class PopulateSalesDataToProductTests
    {
        [Fact(Skip = "Only run to test this data in a database")]
        public Task Verify_PopulateData_AggregatesSalesDataIntoTheProduct()
        {
            return new ProcessPopulateSalesDataToProductsTask().ProcessAsync(null);
        }
    }
}
