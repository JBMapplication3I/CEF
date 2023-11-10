// <copyright file="CheckoutService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout service class</summary>
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Checkouts;
    using Interfaces.Workflow;
    using Models;
#if PAYPAL
    using PayPalInt;
#endif
    using Service;
    using ServiceStack;

    /// <summary>A checkout service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [JetBrains.Annotations.PublicAPI]
    public partial class CheckoutService : ClarityEcommerceServiceBase
    {
        /// <summary>Gets or sets the PayPal checkout provider.</summary>
        /// <value>The PayPal checkout provider.</value>
        protected static ICheckoutProviderBase PayPalCheckoutProvider { get; set; } = null!;

        /// <summary>Gets or sets the identifier of the billing type.</summary>
        /// <value>The identifier of the billing type.</value>
        protected static int BillingTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the shipping type.</summary>
        /// <value>The identifier of the shipping type.</value>
        protected static int ShippingTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the order status pending.</summary>
        /// <value>The identifier of the order status pending.</value>
        protected static int OrderStatusPendingID { get; set; }

        /// <summary>Gets or sets the identifier of the order status paid.</summary>
        /// <value>The identifier of the order status paid.</value>
        protected static int OrderStatusPaidID { get; set; }

        /// <summary>Gets or sets the identifier of the order status on hold.</summary>
        /// <value>The identifier of the order status on hold.</value>
        protected static int OrderStatusOnHoldID { get; set; }

        /// <summary>Gets or sets the identifier of the order state.</summary>
        /// <value>The identifier of the order state.</value>
        protected static int OrderStateID { get; set; }

        /// <summary>Gets or sets the identifier of the order type.</summary>
        /// <value>The identifier of the order type.</value>
        protected static int OrderTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the customer note type.</summary>
        /// <value>The identifier of the customer note type.</value>
        protected static int CustomerNoteTypeID { get; set; }

        /// <summary>Gets or sets the default currency identifier.</summary>
        /// <value>The default currency identifier.</value>
        protected static int DefaultCurrencyID { get; set; }

        /// <summary>Validates the and complete checkout.</summary>
        /// <param name="failCondition">              True if it failed, false if it succeeded.</param>
        /// <param name="result">                     The result.</param>
        /// <param name="checkoutByInventoryLocation">True to checkout by inventory location.</param>
        /// <returns>A CheckoutResult.</returns>
        protected async Task<ICheckoutResult> ValidateAndCompleteCheckoutAsync(
            bool failCondition,
            ICheckoutResult result,
            bool checkoutByInventoryLocation = false)
        {
            if (failCondition)
            {
                // There was an error and the error message as put into ErrorMessage
                result.Succeeded = false;
                return result;
            }
            if (checkoutByInventoryLocation)
            {
                return result;
            }
            // Ensuring Session's SessionCartGuid gets reset
            await ClearSessionShoppingCartGuidAsync().ConfigureAwait(false);
            return result;
        }

        /////// <summary>Gets or sets the preferred payment method attribute.</summary>
        /////// <value>The preferred payment method attribute.</value>
        ////protected static IBaseModel PreferredPaymentMethodAttr { get; set; }

#if PAYPAL
        /// <summary>Ensures that PayPal checkout provider exists and is initialized.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task EnsurePayPalCheckoutProviderAsync(string? contextProfileName)
        {
            if (OrderTypeID == 0)
            {
                await SetupIDsAsync(contextProfileName).ConfigureAwait(false);
            }
            PayPalCheckoutProvider ??= new PayPalCheckoutProvider();
            if (PayPalCheckoutProvider.IsInitialized)
            {
                return;
            }
            await PayPalCheckoutProvider.InitAsync(
                    orderStatusPendingID: OrderStatusPendingID,
                    orderStatusPaidID: OrderStatusPaidID,
                    orderStatusOnHoldID: OrderStatusOnHoldID,
                    orderTypeID: OrderTypeID,
                    orderStateID: OrderStateID,
                    billingTypeID: BillingTypeID,
                    shippingTypeID: ShippingTypeID,
                    customerNoteTypeID: CustomerNoteTypeID,
                    defaultCurrencyID: DefaultCurrencyID,
                    ////preferredPaymentMethodAttr: PreferredPaymentMethodAttr,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }
#endif

        /// <summary>Sets up the IDs.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task SetupIDsAsync(string? contextProfileName)
        {
            var workflows = RegistryLoaderWrapper.GetInstance<IWorkflowsController>(contextProfileName);
            BillingTypeID = await workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Billing",
                    byName: "Billing",
                    byDisplayName: "Billing",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(continueOnCapturedContext: false);
            ShippingTypeID = await workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Shipping",
                    byName: "Shipping",
                    byDisplayName: "Shipping",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(continueOnCapturedContext: false);
            OrderStatusPendingID = await workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Pending",
                    byName: "Pending",
                    byDisplayName: "Pending",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(continueOnCapturedContext: false);
            OrderStatusPaidID = await workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Full Payment Received",
                    byName: "Full Payment Received",
                    byDisplayName: "Full Payment Received",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(continueOnCapturedContext: false);
            OrderStatusOnHoldID = await workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "On Hold",
                    byName: "On Hold",
                    byDisplayName: "On Hold",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(continueOnCapturedContext: false);
            OrderTypeID = await workflows.SalesOrderTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Web",
                    byName: "Web",
                    byDisplayName: "Web",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(continueOnCapturedContext: false);
            OrderStateID = await workflows.SalesOrderStates.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "WORK",
                    byName: "Work",
                    byDisplayName: "Work",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(continueOnCapturedContext: false);
            CustomerNoteTypeID = await workflows.NoteTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Customer",
                    byName: "Customer",
                    byDisplayName: "Customer",
                    model: new NoteTypeModel
                    {
                        Active = true,
                        CustomKey = "Customer",
                        Name = "Customer",
                        DisplayName = "Customer",
                        IsCustomer = true,
                        IsPublic = true,
                    },
                    contextProfileName: contextProfileName)
                .ConfigureAwait(continueOnCapturedContext: false);
            DefaultCurrencyID = await workflows.Currencies.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "USD",
                    byName: "US Dollar",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(continueOnCapturedContext: false);
            ////PreferredPaymentMethodAttr = await workflows.GeneralAttributes.ResolveWithAutoGenerateAsync(
            ////        null,
            ////        "Preferred Payment Method",
            ////        "Preferred Payment Method",
            ////        "Preferred Payment Method",
            ////        new GeneralAttributeModel
            ////        {
            ////            Active = true,
            ////            CustomKey = "Preferred Payment Method",
            ////            Name = "Preferred Payment Method",
            ////            DisplayName = "Preferred Payment Method",
            ////            TypeName = "General"
            ////        },
            ////        true,
            ////        contextProfileName)
            ////    .ConfigureAwait(false);
        }

        /// <summary>Process for selected store identifier described by request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task.</returns>
        private Task ProcessForSelectedStoreIDAsync(ICheckoutModel request)
        {
            var storeIDString = Request.GetItemOrCookie("cefSelectedStoreId");
            if (int.TryParse(storeIDString, out var storeID))
            {
                request.ReferringStoreID = storeID;
            }
            return Task.CompletedTask;
        }
    }

    /// <summary>A checkout feature.</summary>
    /// <seealso cref="IPlugin"/>
    [JetBrains.Annotations.PublicAPI]
    public class CheckoutFeature : IPlugin
    {
        /// <summary>Registers this CheckoutFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
