// <copyright file="ActionsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the actions provider base class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SampleRequestHandlers.Actions;
    using Models;

    /// <summary>The sample request actions provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISampleRequestActionsProviderBase"/>
    public abstract class SampleRequestActionsProviderBase : ProviderBase, ISampleRequestActionsProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SampleRequestActionsHandler;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<ISampleRequestModel> CreateViaCheckoutProcessAsync(
            ICheckoutModel checkout,
            ICartModel cart,
            IUserModel user,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<ISalesInvoiceModel>> AddPaymentAsync(
            int id,
            IPaymentModel payment,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<ISalesInvoiceModel>> CreateInvoiceForAsync(
            int id,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> CreatePickTicketForAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsBackorderedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsCompletedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsConfirmedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsDropShippedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsShippedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsVoidedAsync(int id, string? contextProfileName);
    }
}
