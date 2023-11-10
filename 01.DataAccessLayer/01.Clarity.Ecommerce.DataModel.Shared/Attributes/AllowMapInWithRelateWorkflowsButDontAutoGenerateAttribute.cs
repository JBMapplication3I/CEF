// <copyright file="AllowMapInWithRelateWorkflowsButDontAutoGenerateAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the allow map in with relate workflows but dont automatic generate attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for allow map in with relate workflows but dont automatic generate. This class cannot be
    /// inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AllowMapInWithRelateWorkflowsButDontAutoGenerateAttribute : Attribute
    {
    }
}
