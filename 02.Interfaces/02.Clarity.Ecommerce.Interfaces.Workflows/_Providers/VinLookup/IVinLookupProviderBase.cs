// <copyright file="IVinLookupProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IVinLookupProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.VinLookup
{
    using System.Threading.Tasks;
    using Ecommerce.Models;

    /// <inheritdoc/>
    public interface IVinLookupProviderBase : IProviderBase
    {
        /// <summary>validates the vin number.</summary>
        /// <param name="vinNumber">The vin number.</param>
        /// <param name="contextProfileName">The contextProfileName.</param>
        /// <returns>True if valid, false if not.</returns>
        Task<CEFActionResponse<bool>> ValidateVinAsync(string vinNumber, string? contextProfileName);
    }
}
