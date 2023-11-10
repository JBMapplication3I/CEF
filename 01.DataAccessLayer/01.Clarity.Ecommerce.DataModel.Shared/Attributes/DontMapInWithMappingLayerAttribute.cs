// <copyright file="DontMapInWithMappingLayerAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the dont map in with mapping layer attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for dont map in with mapping layer. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DontMapInWithMappingLayerAttribute : Attribute
    {
    }
}
