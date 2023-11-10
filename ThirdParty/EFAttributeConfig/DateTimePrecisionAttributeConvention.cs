// <copyright file="DateTimePrecisionAttributeConvention.cs" company="Richard Lawley">
// Copyright (c) 2016-2022 Richard Lawley. All rights reserved.
// </copyright>
// <summary>Implements the date time precision attribute convention class</summary>

namespace RichardLawley.EF.AttributeConfig
{
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Data.Entity.ModelConfiguration.Conventions;

    /// <summary>A date time precision attribute convention.</summary>
    /// <seealso cref="PrimitivePropertyAttributeConfigurationConvention{TAttribute}" />
    /// <seealso cref="PrimitivePropertyAttributeConfigurationConvention{TAttribute}" />
    public class
        DateTimePrecisionAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<
            DateTimePrecisionAttribute>
    {
        /// <inheritdoc/>
        public override void Apply(
            ConventionPrimitivePropertyConfiguration configuration,
            DateTimePrecisionAttribute attribute)
        {
            configuration.HasPrecision(attribute.Value);
        }
    }
}