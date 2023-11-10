// <copyright file="AllowMapInWithAssociateWorkflowsButDontAutoGenerateAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the allow map in with associate workflows but dont automatic generate attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for allow map in with associate workflows but don't automatic generate. This class cannot
    /// be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AllowMapInWithAssociateWorkflowsButDontAutoGenerateAttribute : Attribute
    {
    }
}
