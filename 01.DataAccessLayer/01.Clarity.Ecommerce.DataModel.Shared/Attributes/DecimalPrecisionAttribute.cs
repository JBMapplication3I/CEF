// <copyright file="DecimalPrecisionAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the decimal precision attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for decimal precision. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DecimalPrecisionAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="DecimalPrecisionAttribute"/> class.</summary>
        /// <param name="precision">The precision.</param>
        /// <param name="scale">    The scale.</param>
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }

        /// <summary>Gets the precision.</summary>
        /// <value>The precision.</value>
        public byte Precision { get; }

        /// <summary>Gets the scale.</summary>
        /// <value>The scale.</value>
        public byte Scale { get; }
    }
}
