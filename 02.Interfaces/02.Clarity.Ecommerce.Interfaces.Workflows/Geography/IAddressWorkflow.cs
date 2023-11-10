// <copyright file="IAddressWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAddressWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for address workflow.</summary>
    public partial interface IAddressWorkflow
    {
        /// <summary>Resolve address.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IAddressModel.</returns>
        Task<IAddressModel> ResolveAddressAsync(
            IAddressModel model,
            string? contextProfileName);

        /// <summary>Resolve address.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>An IAddressModel.</returns>
        Task<IAddressModel> ResolveAddressAsync(
            IAddressModel model,
            IClarityEcommerceEntities context);

        /// <summary>Gets the latitude and longitude for an address if possible.</summary>
        /// <param name="model">The address model to find coordinates for.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>The latitude and longitude coordinates of the address (or a close approximation), or an error.</returns>
        Task<CEFActionResponse<(decimal lat, decimal lon)>> GetLatLongForAddress(
            IAddressModel model,
            string? contextProfileName);
    }
}
