// <copyright file="DontMapOutEverAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the dont map ever attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for dont map ever. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DontMapOutEverAttribute : Attribute
    {
    }
}
