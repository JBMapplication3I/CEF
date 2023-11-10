// <copyright file="ISalesReturnActionsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesReturnActionsProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.SalesReturnHandlers.Actions
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;
    using Taxes;

    /// <summary>Interface for sales return actions provider.</summary>
    public interface ISalesReturnActionsProviderBase : IProviderBase
    {
        /// <summary>Confirm return.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsConfirmedAsync(int id, int userID, string? contextProfileName);

        /// <summary>Adds a payment to return.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> AddPaymentToReturnAsync(int id, IPaymentModel payment, string? contextProfileName);

        /// <summary>Ship return.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsShippedAsync(int id, string? contextProfileName);

        /// <summary>Complete return.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsCompletedAsync(int id, string? contextProfileName);

        /// <summary>Void return.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="taxesProvider">     The taxes provider.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsVoidAsync(int id, ITaxesProviderBase? taxesProvider, string? contextProfileName);

        /// <summary>Reject return.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsRejectedAsync(int id, int userID, string? contextProfileName);

        /// <summary>Refund return.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsRefundedAsync(int id, int userID, string? contextProfileName);

        /// <summary>Manual refund return.</summary>
        /// <param name="salesReturnModel">  The sales return model.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ManuallyRefundReturnAsync(
            ISalesReturnModel salesReturnModel,
            int userID,
            string? contextProfileName);

        /// <summary>Cancel return.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="currentUser">       The current user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsCancelledAsync(int id, IUserModel currentUser, string? contextProfileName);

        /// <summary>Sends RMA creation email notifications.</summary>
        /// <param name="salesReturn">       The sales return.</param>
        /// <param name="salesOrderID">      Identifier for the sales order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SendRmaCreationEmailNotificationsAsync(
            ISalesReturnModel salesReturn,
            int salesOrderID,
            string? contextProfileName);

        /// <summary>Creates store front sales return.</summary>
        /// <param name="salesReturnModel">     The sales return model.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The new store front sales return.</returns>
        Task<ISalesReturnModel?> CreateStoreFrontSalesReturnAsync(
            ISalesReturnModel salesReturnModel,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);
    }
}
