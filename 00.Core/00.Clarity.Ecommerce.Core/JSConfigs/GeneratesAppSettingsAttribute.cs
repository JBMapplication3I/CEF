// <copyright file="GeneratesAppSettingsAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the generates application settings attribute class</summary>
namespace Clarity.Ecommerce.JSConfigs
{
    using System;

    /// <summary>Attribute for generates application settings. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GeneratesAppSettingsAttribute : Attribute
    {
    }
}
