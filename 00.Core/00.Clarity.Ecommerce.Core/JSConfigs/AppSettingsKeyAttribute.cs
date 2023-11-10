// <copyright file="AppSettingsKeyAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application settings key attribute class</summary>
namespace Clarity.Ecommerce.JSConfigs
{
    using System;

    /// <summary>Attribute for application settings key. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AppSettingsKeyAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="AppSettingsKeyAttribute"/> class.</summary>
        /// <param name="key">The key.</param>
        public AppSettingsKeyAttribute(string key)
        {
            Key = key;
        }

        /// <summary>Gets the key.</summary>
        /// <value>The key.</value>
        public string Key { get; }
    }
}
