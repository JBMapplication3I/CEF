// <copyright file="OnlyMapOutFlattenedValuesInsteadOfObjectWithLiteAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the only map out flattened values with lite attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for only map out flattened values instead of the object as well with lite. This class
    /// cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class OnlyMapOutFlattenedValuesInsteadOfObjectWithLiteAttribute : Attribute
    {
    }
}
