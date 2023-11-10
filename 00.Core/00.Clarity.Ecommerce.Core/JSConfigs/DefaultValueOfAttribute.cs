// <copyright file="DefaultValueOfAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the default value of attribute class</summary>
#nullable enable
namespace Clarity.Ecommerce.JSConfigs
{
    using System;

    /// <summary>Attribute for default value of. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DefaultValueOfAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="DefaultValueOfAttribute"/> class.</summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="otherType">   Type of the other.</param>
        public DefaultValueOfAttribute(string propertyName, Type? otherType = null)
        {
            PropertyName = propertyName;
            OtherType = otherType;
        }

        /// <summary>Gets the name of the property.</summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; }

        /// <summary>Gets the type of the other.</summary>
        /// <value>The type of the other.</value>
        public Type? OtherType { get; }
    }
}
