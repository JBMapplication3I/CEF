// <copyright file="DateTimePrecisionAttribute.cs" company="Richard Lawley">
// Copyright (c) 2016-2022 Richard Lawley. All rights reserved.
// </copyright>
// <summary>Implements the date time precision attribute class</summary>

namespace RichardLawley.EF.AttributeConfig
{
    using System;

    /// <summary>Attribute for date time precision. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DateTimePrecisionAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="DateTimePrecisionAttribute" /> class.</summary>
        /// <param name="value">The value.</param>
        public DateTimePrecisionAttribute(byte value)
        {
            this.Value = value;
        }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public byte Value { get; set; }
    }
}