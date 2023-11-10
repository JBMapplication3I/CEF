// <copyright file="ProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the provider base class</summary>
namespace Clarity.Ecommerce.Providers
{
    using Interfaces.Providers;
    using Interfaces.Workflow;

    /// <summary>A provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public abstract class ProviderBase : IProviderBase
    {
        /// <summary>The logger.</summary>
        protected static readonly ILogger Logger
            = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>The workflows.</summary>
        protected static readonly IWorkflowsController Workflows
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <inheritdoc/>
        public virtual string Name => GetType().Name;

        /// <inheritdoc/>
        public abstract Enums.ProviderType ProviderType { get; }

        /// <inheritdoc/>
        public abstract bool HasValidConfiguration { get; }

        /// <inheritdoc/>
        public abstract bool HasDefaultProvider { get; }

        /// <inheritdoc/>
        public abstract bool IsDefaultProvider { get; }

        /// <inheritdoc/>
        public abstract bool IsDefaultProviderActivated { get; set; }
    }
}
