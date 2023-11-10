// <copyright file="SurchargesRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the surcharges registry class.</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Surcharges
{
    using Fixed;
    using Interfaces.Providers.Surcharges;
    using InterPayments;
    using Lamar;

    /// <summary>Configures which SurchargeProvider to use.</summary>
    /// <seealso cref="ServiceRegistry"/>
    public class SurchargeProviderRegistry : ServiceRegistry
    {
        /// <summary>Registers surcharge providers.</summary>
        public SurchargeProviderRegistry()
        {
            var found = false;
            if (!found && InterPaymentsSurchargeProviderConfig.IsValid(false))
            {
                Use<InterpaymentsSurchargeProvider>().Singleton().For<ISurchargeProviderBase>();
                found = true;
            }
            if (!found)
            {
                Use<FixedSurchargeProvider>().Singleton().For<ISurchargeProviderBase>();
            }
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Surcharges
{
    using Fixed;
    using Interfaces.Providers.Surcharges;
    using InterPayments;
    using StructureMap;

    /// <summary>Configures which SurchargeProvider to use.</summary>
    /// <seealso cref="Registry"/>
    public class SurchargeProviderRegistry : Registry
    {
        /// <summary>Registers surcharge providers.</summary>
        public SurchargeProviderRegistry()
        {
            var found = false;
            if (!found && InterPaymentsSurchargeProviderConfig.IsValid(false))
            {
                For<ISurchargeProviderBase>().Use<InterpaymentsSurchargeProvider>();
                found = true;
            }
            if (!found)
            {
                For<ISurchargeProviderBase>().Use<FixedSurchargeProvider>();
            }
        }
    }
}
#endif
