// <copyright file="IImplementsIDBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImplementsIDBase interface.</summary>
namespace Clarity.Ecommerce.Service
{
    /// <summary>Interface for implements identifier.</summary>
    public interface IImplementsIDBase
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        int ID { get; set; }

        /// <summary>Gets or sets the no cache.</summary>
        /// <value>The no cache.</value>
        // ReSharper disable once InconsistentNaming, StyleCop.SA1300
#pragma warning disable SA1300, IDE1006 // Naming Styles
        long? noCache { get; set; }
#pragma warning restore SA1300, IDE1006
    }
}
