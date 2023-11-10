// <copyright file="EmailBatchRunner.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email batch runner class</summary>
namespace Clarity.Ecommerce.Tasks.EmailBatches.Testing
{
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ScheduledTasks.EmailBatches")]
    public class EmailBatchRunner
    {
        [Fact(Skip = "Only run when you need it")]
        public Task RunEmailBatch()
        {
            return new ProcessEmailBatchTask().ProcessAsync(null);
        }
    }
}
