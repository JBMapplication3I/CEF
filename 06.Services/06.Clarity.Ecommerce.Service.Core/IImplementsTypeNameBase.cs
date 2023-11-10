// <copyright file="IImplementsTypeNameBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImplementsTypeNameBase interface</summary>
namespace Clarity.Ecommerce.Service
{
    /// <summary>Interface for implements type name base.</summary>
    public interface IImplementsTypeNameBase
    {
        /// <summary>Gets or sets the name of the type.</summary>
        /// <value>The name of the type.</value>
        string? TypeName { get; set; }

        /// <summary>Gets or sets the no cache.</summary>
        /// <value>The no cache.</value>
        // ReSharper disable once InconsistentNaming, StyleCop.SA1300
#pragma warning disable SA1300, IDE1006
        long? noCache { get; set; }
#pragma warning restore SA1300, IDE1006
    }
}