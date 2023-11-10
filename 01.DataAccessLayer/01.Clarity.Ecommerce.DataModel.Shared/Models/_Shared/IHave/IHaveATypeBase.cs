// <copyright file="IHaveATypeBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveATypeBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for have a type base.</summary>
    /// <typeparam name="TType">Type of the type.</typeparam>
    /// <seealso cref="IHaveATypeBase"/>
    public interface IHaveATypeBase<TType> : IHaveATypeBase
        where TType : ITypableBase
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        TType? Type { get; set; }
    }

    /// <summary>Interface for have a type base.</summary>
    public interface IHaveATypeBase : IBase
    {
        /// <summary>Gets or sets the identifier of the type.</summary>
        /// <value>The identifier of the type.</value>
        int TypeID { get; set; }
    }
}
