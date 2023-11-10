// <copyright file="OnlineStatusChecksTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the online status checks tests class</summary>
namespace Clarity.Ecommerce.Tasks.OnlineStatusChecks.Testing
{
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ScheduledTasks.OnlineStatusChecks")]
    public class OnlineStatusChecksTests
    {
        [Fact(Skip = "Don't run automatically")]
        public Task Verify_Process_RunsWithoutError()
        {
            return new OnlineStatusChecksTask().ProcessAsync(null);
        }
    }
}
