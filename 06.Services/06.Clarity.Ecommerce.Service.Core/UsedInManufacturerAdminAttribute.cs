// <copyright file="UsedInManufacturerAdminAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the used in manufacturer admin attribute class</summary>
namespace Clarity.Ecommerce.Service
{
    using System;

    /// <summary>Attribute for used in manufacturer admin. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UsedInManufacturerAdminAttribute : Attribute
    {
    }
}
