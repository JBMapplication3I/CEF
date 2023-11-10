﻿// <copyright file="UsedInStoreAdminAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the used in store admin attribute class</summary>
namespace Clarity.Ecommerce.Service
{
    using System;

    /// <summary>Attribute for used in store admin. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UsedInStoreAdminAttribute : Attribute
    {
    }
}
