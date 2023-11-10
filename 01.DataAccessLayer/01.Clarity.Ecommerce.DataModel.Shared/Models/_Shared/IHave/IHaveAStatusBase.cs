// <copyright file="IHaveAStatusBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAStatusBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for have the status base.</summary>
    /// <typeparam name="TStatus">Type of the status.</typeparam>
    /// <seealso cref="IHaveAStatusBase"/>
    public interface IHaveAStatusBase<TStatus> : IHaveAStatusBase
        where TStatus : IStatusableBase
    {
        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        TStatus? Status { get; set; }
    }

    /// <summary>Interface for have the status base.</summary>
    public interface IHaveAStatusBase
    {
        /// <summary>Gets or sets the identifier of the status.</summary>
        /// <value>The identifier of the status.</value>
        int StatusID { get; set; }
    }
}
