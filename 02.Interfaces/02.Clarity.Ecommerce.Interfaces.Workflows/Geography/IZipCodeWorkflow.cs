// <copyright file="IZipCodeWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IZipCodeWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using DataModel;
    using Models;

    /// <summary>Interface for zip code workflow.</summary>
    public partial interface IZipCodeWorkflow
    {
        /// <summary>Gets by zip code value.</summary>
        /// <param name="zipCode">           The zip code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by zip code value.</returns>
        Task<IZipCodeModel?> GetByZipCodeValueAsync(string? zipCode, string? contextProfileName);

        /// <summary>Updates the latitude longitude based on zip code.</summary>
        /// <param name="addressEntity">     The address entity.</param>
        /// <param name="addressModel">      The address model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task UpdateLatitudeLongitudeBasedOnZipCodeAsync(
            IAddress addressEntity,
            IAddressModel addressModel,
            string? contextProfileName);
    }
}
