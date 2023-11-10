// <copyright file="NotFound.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the not found class</summary>
namespace Microsoft.Owin.Builder
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Simple object used by AppBuilder as seed OWIN callable if the
    /// builder.Properties["builder.DefaultApp"] is not set.</summary>
    internal class NotFound
    {
        /// <summary>The completed.</summary>
        private static readonly Task Completed;

        /// <summary>Initializes static members of the Microsoft.Owin.Builder.NotFound class.</summary>
        static NotFound()
        {
            Completed = CreateCompletedTask();
        }

        /// <summary>Executes the given operation on a different thread, and waits for the result.</summary>
        /// <param name="env">The environment.</param>
        /// <returns>A Task.</returns>
        public Task Invoke(IDictionary<string, object> env)
        {
            env["owin.ResponseStatusCode"] = 404;
            return Completed;
        }

        /// <summary>Creates completed task.</summary>
        /// <returns>The new completed task.</returns>
        private static Task CreateCompletedTask()
        {
            var taskCompletionSource = new TaskCompletionSource<object>();
            taskCompletionSource.SetResult(null);
            return taskCompletionSource.Task;
        }
    }
}
