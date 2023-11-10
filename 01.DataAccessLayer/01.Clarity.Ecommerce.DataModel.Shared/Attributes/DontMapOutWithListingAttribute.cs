// <copyright file="DontMapOutWithListingAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the dont map listing attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for dont map listing. This class cannot be inherited.</summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DontMapOutWithListingAttribute : Attribute
    {
    }
}
