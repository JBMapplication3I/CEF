// <copyright file="AddressValidationProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Address Validation Provider StructureMap 4 Registry to associate the interfaces with
// their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.AddressValidation
{
    using Avalara;
    using Basic;
    using Interfaces.Providers.AddressValidation;
    using Lamar;
    using MelissaData;

    /// <summary>The address validation provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class AddressValidationProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="AddressValidationProviderRegistry"/> class.</summary>
        public AddressValidationProviderRegistry()
        {
            if (AvalaraAddressValidationProviderConfig.IsValid(false))
            {
                Use<AvalaraAddressValidationProvider>().Singleton().For<IAddressValidationProviderBase>();
                return;
            }
            if (MelissaDataAddressValidationProviderConfig.IsValid(false))
            {
                Use<MelissaDataAddressValidationProvider>().Singleton().For<IAddressValidationProviderBase>();
                return;
            }
            // Assign default
            Use<BasicAddressValidationProvider>().Singleton().For<IAddressValidationProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.AddressValidation
{
    using Avalara;
    using Basic;
    using Interfaces.Providers.AddressValidation;
    using MelissaData;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The address validation provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class AddressValidationProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="AddressValidationProviderRegistry"/> class.</summary>
        public AddressValidationProviderRegistry()
        {
            var found = false;
            if (AvalaraAddressValidationProviderConfig.IsValid(false))
            {
                For<IAddressValidationProviderBase>(new SingletonLifecycle()).Add<AvalaraAddressValidationProvider>();
                found = true;
            }
            if (MelissaDataAddressValidationProviderConfig.IsValid(false))
            {
                For<IAddressValidationProviderBase>(new SingletonLifecycle()).Add<MelissaDataAddressValidationProvider>();
                found = true;
            }
            if (found)
            {
                return;
            }
            // Assign default
            For<IAddressValidationProviderBase>(new SingletonLifecycle()).Add<BasicAddressValidationProvider>();
        }
    }
}
#endif
