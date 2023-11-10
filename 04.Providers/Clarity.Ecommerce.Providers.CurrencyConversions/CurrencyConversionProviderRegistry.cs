// <copyright file="CurrencyConversionProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Currency Conversion Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.CurrencyConversions
{
    using FixerIO;
    using Interfaces.Providers.CurrencyConversions;
    using Lamar;

    /// <summary>The Currency Conversion provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class CurrencyConversionProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="CurrencyConversionProviderRegistry"/> class.</summary>
        public CurrencyConversionProviderRegistry()
        {
            if (FixerIOCurrencyConversionsProviderConfig.IsValid(false))
            {
                Use<FixerIOCurrencyConversionsProvider>().Singleton().For<ICurrencyConversionsProviderBase>();
            }
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.CurrencyConversions
{
    using FixerIO;
    using Interfaces.Providers.CurrencyConversions;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The Currency Conversion provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class CurrencyConversionProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="CurrencyConversionProviderRegistry"/> class.</summary>
        public CurrencyConversionProviderRegistry()
        {
            if (FixerIOCurrencyConversionsProviderConfig.IsValid(false))
            {
                For<ICurrencyConversionsProviderBase>(new SingletonLifecycle()).Add<FixerIOCurrencyConversionsProvider>();
            }
        }
    }
}
#endif
