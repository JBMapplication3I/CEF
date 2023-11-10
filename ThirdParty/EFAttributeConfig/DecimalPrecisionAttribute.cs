// <copyright file="DecimalPrecisionAttribute.cs" company="Richard Lawley">
// Copyright (c) 2016-2022 Richard Lawley. All rights reserved.
// </copyright>
// <summary>Implements the decimal precision attribute class</summary>

namespace RichardLawley.EF.AttributeConfig
{
    using System;

    /// <summary>Attribute for decimal precision. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DecimalPrecisionAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="DecimalPrecisionAttribute" /> class.</summary>
        /// <param name="precision">The precision.</param>
        /// <param name="scale">    The scale.</param>
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            this.Precision = precision;
            this.Scale = scale;
        }

        /// <summary>Gets or sets the precision.</summary>
        /// <value>The precision.</value>
        public byte Precision { get; set; }

        /// <summary>Gets or sets the scale.</summary>
        /// <value>The scale.</value>
        public byte Scale { get; set; }
    }
}