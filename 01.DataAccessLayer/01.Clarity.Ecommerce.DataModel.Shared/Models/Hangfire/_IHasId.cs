// <copyright file="_IHasId.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the _IHasId interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for has identifier.</summary>
    public interface IHasId
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        int Id { get; set; }
    }

    /// <summary>Interface for has identifier and key.</summary>
    /// <seealso cref="IHasId"/>
    public interface IHasIdAndKey : IHasId
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        string Key { get; set; }
    }
}
