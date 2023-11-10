// <copyright file="RecommendByCategoryTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the recommend by category tests class</summary>
namespace Clarity.Ecommerce.Tasks.Personalization.RecommendByCategory.Testing
{
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ScheduledTasks.Personalization.RecommendByCategory.Testing")]
    public class RecommendByCategoryTests
    {
        [Fact(Skip = "Don't run automatically")]
        public Task Verify_Personalization_RecommendByCategory_QueuesEmails()
        {
            return new RecommendByCategoryPersonalizedTask().ProcessAsync(null);
        }
    }
}
