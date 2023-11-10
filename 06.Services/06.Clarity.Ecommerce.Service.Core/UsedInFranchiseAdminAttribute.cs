// <copyright file="UsedInFranchiseAdminAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the used in franchise admin attribute class</summary>
namespace Clarity.Ecommerce.Service
{
    using System;

    /// <summary>Attribute for used in franchise admin. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UsedInFranchiseAdminAttribute : Attribute
    {
    }
}
