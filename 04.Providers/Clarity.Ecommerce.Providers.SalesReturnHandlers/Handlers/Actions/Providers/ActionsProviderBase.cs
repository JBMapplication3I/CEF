// <copyright file="ActionsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the actions provider base class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SalesReturnHandlers.Actions;
    using Interfaces.Providers.Taxes;
    using Models;

    /// <summary>The sales return actions provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISalesReturnActionsProviderBase"/>
    public abstract class SalesReturnActionsProviderBase : ProviderBase, ISalesReturnActionsProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SalesReturnActionsHandler;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> AddPaymentToReturnAsync(
            int id,
            IPaymentModel payment,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<ISalesReturnModel?> CreateStoreFrontSalesReturnAsync(
            ISalesReturnModel salesReturnModel,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> ManuallyRefundReturnAsync(
            ISalesReturnModel salesReturnModel,
            int userID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SendRmaCreationEmailNotificationsAsync(
            ISalesReturnModel salesReturn,
            int salesOrderID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsCancelledAsync(
            int id,
            IUserModel currentUser,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsCompletedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsConfirmedAsync(
            int id,
            int userID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsRefundedAsync(
            int id,
            int userID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsRejectedAsync(
            int id,
            int userID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsShippedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsVoidAsync(
            int id,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);
    }
}
