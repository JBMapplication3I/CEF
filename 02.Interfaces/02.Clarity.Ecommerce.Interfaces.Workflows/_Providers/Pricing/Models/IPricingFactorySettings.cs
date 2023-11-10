// <copyright file="IPricingFactorySettings.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPricingFactorySettings interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Pricing
{
    /// <summary>Interface for pricing factory settings.</summary>
    public interface IPricingFactorySettings
    {
        /// <summary>Gets the default currency key.</summary>
        /// <value>The default currency key.</value>
        string DefaultCurrencyKey { get; }

        /// <summary>Gets the default markup rate.</summary>
        /// <value>The default markup rate.</value>
        decimal DefaultMarkupRate { get; }

        /// <summary>Gets the default price point key.</summary>
        /// <value>The default price point key.</value>
        string DefaultPricePointKey { get; }

        /// <summary>Gets the default unit of measure.</summary>
        /// <value>The default unit of measure.</value>
        string DefaultUnitOfMeasure { get; }
    }
}
