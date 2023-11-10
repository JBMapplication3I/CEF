// <copyright file="CartValidationProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Cart Validation Provider StructureMap 4 Registry to associate the interfaces with
// their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using Interfaces.Models;
    using Interfaces.Providers.CartValidation;
    using Lamar;
    using Models;

    /// <summary>The address validation provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class CartValidationProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="CartValidationProviderRegistry"/> class.</summary>
        public CartValidationProviderRegistry()
        {
            For<ICartValidatorItemModificationResult>().Use<CartValidatorItemModificationResult>();
            Use<CartValidatorConfig>().Singleton().For<ICartValidatorConfig>();
            Use<CartValidator>().Singleton().For<ICartValidator>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using Interfaces.Models;
    using Interfaces.Providers.CartValidation;
    using Models;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The address validation provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class CartValidationProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="CartValidationProviderRegistry"/> class.</summary>
        public CartValidationProviderRegistry()
        {
            For<ICartValidatorItemModificationResult>().Use<CartValidatorItemModificationResult>();
            For<ICartValidatorConfig>(new SingletonLifecycle()).Add<CartValidatorConfig>();
            For<ICartValidator>(new SingletonLifecycle()).Use<CartValidator>();
        }
    }
}
#endif
