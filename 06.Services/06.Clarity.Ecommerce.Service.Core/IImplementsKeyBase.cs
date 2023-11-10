// <copyright file="IImplementsKeyBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements key base class</summary>
namespace Clarity.Ecommerce.Service
{
    /// <summary>Interface for implements key.</summary>
    public interface IImplementsKeyBase
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        string Key { get; set; }

        /// <summary>Gets or sets the no cache.</summary>
        /// <value>The no cache.</value>
        // ReSharper disable once InconsistentNaming, StyleCop.SA1300
#pragma warning disable SA1300, IDE1006
        long? noCache { get; set; }
#pragma warning restore SA1300, IDE1006
    }
}
