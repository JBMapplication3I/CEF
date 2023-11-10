// <copyright file="IHaveAStateBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAStateBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for have the state base.</summary>
    /// <typeparam name="TState">Type of the state.</typeparam>
    /// <seealso cref="IHaveAStateBase"/>
    public interface IHaveAStateBase<TState> : IHaveAStateBase
        where TState : IStateableBase
    {
        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        TState? State { get; set; }
    }

    /// <summary>Interface for have the state base.</summary>
    public interface IHaveAStateBase
    {
        /// <summary>Gets or sets the identifier of the state.</summary>
        /// <value>The identifier of the state.</value>
        int StateID { get; set; }
    }
}
