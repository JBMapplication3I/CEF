// <copyright file="HeartlandPaymentsProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payments provider registry class</summary>
namespace Clarity.Ecommerce.Providers.Payments
{
    using Heartland;
    using Interfaces.Providers.Payments;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The payments provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class HeartlandPaymentsProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="HeartlandPaymentsProviderRegistry"/> class.</summary>
        public HeartlandPaymentsProviderRegistry()
        {
            if (HeartlandPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<HeartlandPaymentsProvider>();
            }
        }
    }
}
