// <copyright file="AppBuilderProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the App Builder Provider class</summary>
namespace Clarity.Ecommerce.Scheduler
{
    using System;
    using Owin;

    /// <summary>An application builder provider.</summary>
    /// <seealso cref="IDisposable"/>
    public class AppBuilderProvider : IDisposable
    {
        /// <summary>The application.</summary>
        private readonly IAppBuilder app;

        /// <summary>Initializes a new instance of the <see cref="AppBuilderProvider"/> class.</summary>
        /// <param name="app">The application.</param>
        public AppBuilderProvider(IAppBuilder app)
        {
            this.app = app;
        }

        /// <summary>Gets the get.</summary>
        /// <returns>An IAppBuilder.</returns>
        public IAppBuilder Get() => app;

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}
