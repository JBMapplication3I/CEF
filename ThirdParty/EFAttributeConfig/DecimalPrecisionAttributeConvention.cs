// <copyright file="DecimalPrecisionAttributeConvention.cs" company="Richard Lawley">
// Copyright (c) 2016-2022 Richard Lawley. All rights reserved.
// </copyright>
// <summary>Implements the decimal precision attribute convention class</summary>

namespace RichardLawley.EF.AttributeConfig
{
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Data.Entity.ModelConfiguration.Conventions;

    /// <summary>A decimal precision attribute convention.</summary>
    /// <seealso cref="PrimitivePropertyAttributeConfigurationConvention{TAttribute}" />
    /// <seealso cref="PrimitivePropertyAttributeConfigurationConvention{TAttribute}" />
    public class
        DecimalPrecisionAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<
            DecimalPrecisionAttribute>
    {
        /// <inheritdoc/>
        public override void Apply(
            ConventionPrimitivePropertyConfiguration configuration,
            DecimalPrecisionAttribute attribute)
        {
            configuration.HasPrecision(attribute.Precision, attribute.Scale);
        }
    }
}