// <copyright file="SplitOnAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the split on attribute class</summary>
namespace Clarity.Ecommerce
{
    using System;

    /// <summary>Attribute for split on. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SplitOnAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="SplitOnAttribute"/> class.</summary>
        /// <param name="splitOn">The split on.</param>
        public SplitOnAttribute(char[] splitOn)
        {
            SplitOn = splitOn;
        }

        /// <summary>Gets the split on.</summary>
        /// <value>The split on.</value>
        public char[] SplitOn { get; }
    }
}
