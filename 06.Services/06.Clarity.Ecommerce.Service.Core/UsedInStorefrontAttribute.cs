// <copyright file="UsedInStorefrontAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the used in storefront attribute class</summary>
namespace Clarity.Ecommerce.Service
{
    using System;

    /// <summary>Attribute for used in user interface. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UsedInStorefrontAttribute : Attribute
    {
    }
}
