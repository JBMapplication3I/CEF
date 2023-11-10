// <copyright file="ISurchargeProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISurchargeProviderBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Providers.Surcharges
{
    using System.Threading.Tasks;

    /// <summary>A provider which allows adding a surcharge on top of a payment (currently for invoices; could be
    /// extended).</summary>
    public interface ISurchargeProviderBase : IProviderBase
    {
        /// <summary>Ensure the descriptor has a valid Key on it.</summary>
        /// <remarks>Typically, this would be implemented by looking at how many payments the user has already made or by
        /// using the applicable invoice IDs.</remarks>
        /// <param name="descriptor">        A SurchargeDescriptor which provides enough information about the surcharge
        ///                                  to find its key.</param>
        /// <param name="contextProfileName">DI context.</param>
        /// <returns>A surcharge context with a valid key.</returns>
        Task<SurchargeDescriptor> ResolveKeyAsync(SurchargeDescriptor descriptor, string? contextProfileName);

        /// <summary>Attempt to ensure the descriptor has a valid ProviderKey on it.</summary>
        /// <param name="descriptor">        A SurchargeDescriptor which provides enough information about the surcharge
        ///                                  to find its key.</param>
        /// <param name="contextProfileName">DI context.</param>
        /// <returns>The descriptor with a filled out ProviderKey, if possible.</returns>
        Task<SurchargeDescriptor> TryResolveProviderKeyAsync(SurchargeDescriptor descriptor, string? contextProfileName);

        /// <summary>Calculate a surcharge for a transaction or potential transaction.</summary>
        /// <remarks>If all the conditions in the descriptor are equivalent, the provider may choose to re-use a
        /// previously calculated surcharge.</remarks>
        /// <param name="descriptor">        A SurchargeDescriptor which provides enough information about the surcharge
        ///                                  to find its key.</param>
        /// <param name="contextProfileName">DI context.</param>
        /// <returns>The surcharge, including 0.0m if unable to calculate.</returns>
        Task<(SurchargeDescriptor descriptor, decimal amount)> CalculateSurchargeAsync(
            SurchargeDescriptor descriptor,
            string? contextProfileName);

        /// <summary>Ensure the surcharge associated with this descriptor's Key or ProviderKey is marked as complete.</summary>
        /// <remarks>As surcharging should be a non-critical path, this method may never throw unless mayThrow is true.
        /// If an error occurs while marking a transaction as complete, the implementation is to mark the affected
        /// invoices/orders with a serializable attribute whose key is formatted with
        /// <see cref="SurchargeProviderHelpers.FormatMissingCompletion(string)"/>. The RetryCompletingSurcharges
        /// scheduled task will find all unique provider keys and attempt to re-complete them.</remarks>
        /// <param name="descriptor">        A SurchargeDescriptor which provides enough information about the surcharge
        ///                                  to find its key.</param>
        /// <param name="mayThrow">          Signal whether the method is allowed to throw on errors. See remarks.</param>
        /// <param name="contextProfileName">DI context.</param>
        /// <returns>A more complete descriptor.</returns>
        Task<SurchargeDescriptor> MarkCompleteAsync(
            SurchargeDescriptor descriptor,
            bool mayThrow,
            string? contextProfileName);

        /// <summary>Ensure the surcharge associated with this context is cancelled.</summary>
        /// <param name="descriptor">        A SurchargeDescriptor which provides enough information about the surcharge
        ///                                  to find its key.</param>
        /// <param name="contextProfileName">DI context.</param>
        /// <returns>A more complete descriptor, if possible. If the descriptor doesn't refer to a real surcharge
        /// transaction, the implementation may choose to not change anything.</returns>
        Task<SurchargeDescriptor> CancelAsync(SurchargeDescriptor descriptor, string? contextProfileName);
    }
}
