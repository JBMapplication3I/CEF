// <copyright file="DependsOnAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the depends on attribute class</summary>
namespace Clarity.Ecommerce.JSConfigs
{
    using System;

    /// <summary>Attribute for depends on. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DependsOnAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="DependsOnAttribute"/> class.</summary>
        /// <param name="properties">The property(ies) this setting depends on.</param>
        public DependsOnAttribute(params string[] properties)
        {
            Properties = properties;
        }

        /// <summary>Gets the properties.</summary>
        /// <value>The properties.</value>
        public string[] Properties { get; }
    }
}
