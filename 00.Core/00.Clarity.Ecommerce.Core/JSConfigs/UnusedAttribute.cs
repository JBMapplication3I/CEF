// <copyright file="UnusedAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the unused attribute class</summary>
namespace Clarity.Ecommerce.JSConfigs
{
    using System;

    /// <summary>Attribute for unused. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class UnusedAttribute : Attribute
    {
    }
}
