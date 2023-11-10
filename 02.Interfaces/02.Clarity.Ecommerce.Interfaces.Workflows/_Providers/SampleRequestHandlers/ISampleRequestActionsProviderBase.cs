// <copyright file="ISampleRequestActionsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISampleRequestActionsProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.SampleRequestHandlers.Actions
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for sample request actions provider.</summary>
    public interface ISampleRequestActionsProviderBase : IProviderBase
    {
        /// <summary>Creates via checkout process.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="cart">              The cart.</param>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new via checkout process.</returns>
        Task<ISampleRequestModel> CreateViaCheckoutProcessAsync(
            ICheckoutModel checkout,
            ICartModel cart,
            IUserModel user,
            string? contextProfileName);

        /// <summary>Sets record as confirmed.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsConfirmedAsync(int id, string? contextProfileName);

        /// <summary>Sets record as backordered.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsBackorderedAsync(int id, string? contextProfileName);

        /// <summary>Creates invoice for.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new invoice for.</returns>
        Task<CEFActionResponse<ISalesInvoiceModel>> CreateInvoiceForAsync(int id, string? contextProfileName);

        /// <summary>Adds a payment.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{ISalesInvoiceModel}}.</returns>
        Task<CEFActionResponse<ISalesInvoiceModel>> AddPaymentAsync(
            int id,
            IPaymentModel payment,
            string? contextProfileName);

        /// <summary>Sets record as drop shipped.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsDropShippedAsync(int id, string? contextProfileName);

        /// <summary>Creates pick ticket for.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new pick ticket for.</returns>
        Task<CEFActionResponse> CreatePickTicketForAsync(int id, string? contextProfileName);

        /// <summary>Sets record as shipped.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsShippedAsync(int id, string? contextProfileName);

        /// <summary>Sets record as completed.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsCompletedAsync(int id, string? contextProfileName);

        /// <summary>Sets record as voided.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsVoidedAsync(int id, string? contextProfileName);
    }
}
