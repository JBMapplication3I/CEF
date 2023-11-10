// <copyright file="DontMapInWithRelateWorkflowsAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the dont map in with relate workflows attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for dont map in with relate workflows. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DontMapInWithRelateWorkflowsAttribute : Attribute
    {
    }
}
