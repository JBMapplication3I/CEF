// <copyright file="TaxesProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Taxes Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Taxes
{
    using AvalaraInt;
    using Basic;
    using Interfaces.Providers.Taxes;
    using Lamar;
    using OneSource;
    using Oracle;

    /// <summary>The Taxes provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class TaxesProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="TaxesProviderRegistry"/> class.</summary>
        public TaxesProviderRegistry()
        {
            if (AvalaraTaxesProviderConfig.IsValid(false))
            {
                Use<AvalaraTaxesProvider>().Singleton().For<ITaxesProviderBase>();
                return;
            }
            if (OracleTaxesProviderConfig.IsValid(false))
            {
                Use<OracleTaxesProvider>().Singleton().For<ITaxesProviderBase>();
                return;
            }
            if (OneSourceTaxesProviderConfig.IsValid(false))
            {
                Use<OneSourceTaxesProvider>().Singleton().For<ITaxesProviderBase>();
                return;
            }
            if (BasicTaxesProviderConfig.IsValid(false))
            {
                Use<BasicTaxesProvider>().Singleton().For<ITaxesProviderBase>();
                return;
            }
            // Assign default
            Use<BasicTaxesProvider>().Singleton().For<ITaxesProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Taxes
{
    using AvalaraInt;
    using Basic;
    using Clarity.Ecommerce.Providers.Taxes.OneSource;
    using Interfaces.Providers.Taxes;
    using Oracle;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The Taxes provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class TaxesProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="TaxesProviderRegistry"/> class.</summary>
        public TaxesProviderRegistry()
        {
            var found = false;
            if (AvalaraTaxesProviderConfig.IsValid(false))
            {
                For<ITaxesProviderBase>(new SingletonLifecycle()).Add<AvalaraTaxesProvider>();
                found = true;
            }
            if (OracleTaxesProviderConfig.IsValid(false))
            {
                For<ITaxesProviderBase>(new SingletonLifecycle()).Add<OracleTaxesProvider>();
                found = true;
            }
            if (OneSourceTaxesProviderConfig.IsValid(false))
            {
                For<ITaxesProviderBase>(new SingletonLifecycle()).Add<OneSourceTaxesProvider>();
                found = true;
            }
            if (BasicTaxesProviderConfig.IsValid(false))
            {
                For<ITaxesProviderBase>(new SingletonLifecycle()).Add<BasicTaxesProvider>();
                found = true;
            }
            if (found)
            {
                return;
            }
            // Assign default
            For<ITaxesProviderBase>(new SingletonLifecycle()).Add<BasicTaxesProvider>();
        }
    }
}
#endif
