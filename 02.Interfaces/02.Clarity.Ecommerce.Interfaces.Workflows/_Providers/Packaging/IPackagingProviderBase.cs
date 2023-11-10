// <copyright file="IPackagingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPackagingProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Packaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;

    /// <summary>Interface for packaging provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IPackagingProviderBase : IProviderBase
    {
        /// <summary>Gets item packages.</summary>
        /// <param name="cartID">            The cart database identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The item packages wrapped in a <see cref="CEFActionResponse{TResult}"/>.</returns>
        Task<CEFActionResponse<List<IProviderShipment>>> GetItemPackagesAsync(
            int cartID,
            string? contextProfileName);
    }
}
