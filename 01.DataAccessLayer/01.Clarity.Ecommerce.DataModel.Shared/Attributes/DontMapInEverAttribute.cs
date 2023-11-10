// <copyright file="DontMapInEverAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the dont assign attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for dont assign. This class cannot be inherited.</summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DontMapInEverAttribute : Attribute
    {
    }
}
