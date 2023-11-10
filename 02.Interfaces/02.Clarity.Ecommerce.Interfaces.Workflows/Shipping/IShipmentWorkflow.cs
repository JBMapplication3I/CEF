// <copyright file="IShipmentWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IShipmentWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for shipment workflow.</summary>
    public partial interface IShipmentWorkflow
    {
        /// <summary>Gets by tracking number.</summary>
        /// <param name="packageTrackingNumber">The package tracking number.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The by tracking number.</returns>
        Task<IShipmentModel?> GetByTrackingNumberAsync(string packageTrackingNumber, string? contextProfileName);

        /// <summary>Deactivate by tracking number.</summary>
        /// <param name="trackingNumber">    The tracking number.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeactivateByTrackingNumberAsync(string trackingNumber, string? contextProfileName);
    }
}
