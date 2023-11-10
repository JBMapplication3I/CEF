// <copyright file="TaxesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the taxes provider base class</summary>
namespace Clarity.Ecommerce.Providers.Taxes
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using Models;

    /// <summary>The taxes provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ITaxesProviderBase"/>
    public abstract class TaxesProviderBase : ProviderBase, ITaxesProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Taxes;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public bool IsInitialized { get; protected set; }

        /// <inheritdoc/>
        public abstract Task<bool> InitAsync(string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<decimal> CalculateAsync(
            Enums.TaxEntityType taxEntityType, ICartModel cart, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<TaxesResult> CalculateWithLineItemsAsync(
            Enums.TaxEntityType taxEntityType, ICartModel cart, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<string>> CommitAsync(
            Enums.TaxEntityType taxEntityType, ICartModel cartModel, string purchaseOrderNumber, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<string>> VoidAsync(string taxTransactionID, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> TestServiceAsync();

        /// <inheritdoc/>
        public abstract Task<TaxesResult> CalculateCartAsync(
            ICartModel cart,
            int? userId,
            string? contextProfileName,
            TargetGroupingKey? key = null,
            string? vatId = null);

        /// <inheritdoc/>
        public abstract Task<TaxesResult> CalculateCartAsync(
            ICartModel cart,
            int? userId,
            int? currentAccountId,
            string? contextProfileName,
            TargetGroupingKey? key = null,
            string? vatId = null);

        /// <inheritdoc/>
        public abstract Task<TaxesResult> CommitCartAsync(
            ICartModel cart,
            int? userId,
            string? contextProfileName,
            string? purchaseOrderNumber = null,
            string? vatId = null);

        /// <inheritdoc/>
        public abstract Task CommitReturnAsync(
            ISalesReturnModel salesReturn, string description, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task VoidOrderAsync(ISalesOrderModel salesOrder, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task VoidReturnAsync(ISalesReturnModel salesReturn, string? contextProfileName);
    }
}
