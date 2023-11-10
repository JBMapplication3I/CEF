// <copyright file="AllowLookupAssignInWithAssociateWorkflowsButDontAffectAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the allow lookup assign in with associate workflows but don't affect attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>An allow lookup assign in with associate workflows but don't affect attribute. This class cannot be
    /// inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AllowLookupAssignInWithAssociateWorkflowsButDontAffectAttribute : Attribute
    {
    }
}
