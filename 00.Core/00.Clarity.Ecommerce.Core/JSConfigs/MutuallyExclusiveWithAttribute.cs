// <copyright file="MutuallyExclusiveWithAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mutually exclusive with attribute class</summary>
namespace Clarity.Ecommerce.JSConfigs
{
    using System;

    /// <summary>Attribute for mutually exclusive with. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MutuallyExclusiveWithAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="MutuallyExclusiveWithAttribute"/> class.</summary>
        /// <param name="properties">The property(ies) this setting mutually exclusive with.</param>
        public MutuallyExclusiveWithAttribute(params string[] properties)
        {
            Properties = properties;
        }

        /// <summary>Gets the properties.</summary>
        /// <value>The properties.</value>
        public string[] Properties { get; }
    }
}
