// <copyright file="DiscountsRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discounts registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Discounts
{
    using Lamar;

    /// <summary>The discounts registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    public class DiscountsRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="DiscountsRegistry"/> class.</summary>
        public DiscountsRegistry()
        {
            Use<DiscountManager>().Singleton().For<IDiscountManager>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Discounts
{
    using StructureMap;

    /// <summary>The discounts registry.</summary>
    /// <seealso cref="Registry"/>
    public class DiscountsRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="DiscountsRegistry"/> class.</summary>
        public DiscountsRegistry()
        {
            ////For<IDiscountController>().Use<DiscountController>();
            For<IDiscountManager>().Use<DiscountManager>();
        }
    }
}
#endif
