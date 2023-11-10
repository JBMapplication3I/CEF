// <copyright file="CheckoutProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout provider base class</summary>
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Checkouts;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using MoreLinq;
    using Payments.Payoneer;
    using Utilities;

    /// <summary>A checkout provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ICheckoutProviderBase"/>
    public abstract class CheckoutProviderBase : ProviderBase, ICheckoutProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Checkouts;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public bool IsInitialized { get; protected set; }

        /// <summary>Gets a value indicating whether this CheckoutProviderBase use preferred payment method for later
        /// payments.</summary>
        /// <value>True if use preferred payment method for later payments, false if not.</value>
        protected static bool UsePreferredPaymentMethodForLaterPayments { get; }
            = ProviderConfig.GetBooleanSetting("Clarity.Checkout.UsePreferredPaymentMethodForLaterPayments");

        /// <summary>Gets the identifier of the order status pending.</summary>
        /// <value>The identifier of the order status pending.</value>
        protected int OrderStatusPendingID { get; private set; }

        /// <summary>Gets the identifier of the order status paid.</summary>
        /// <value>The identifier of the order status paid.</value>
        protected int OrderStatusPaidID { get; private set; }

        /// <summary>Gets the identifier of the order status on hold.</summary>
        /// <value>The identifier of the order status on hold.</value>
        protected int OrderStatusOnHoldID { get; private set; }

        /// <summary>Gets the identifier of the order type.</summary>
        /// <value>The identifier of the order type.</value>
        protected int OrderTypeID { get; private set; }

        /// <summary>Gets the identifier of the order state.</summary>
        /// <value>The identifier of the order state.</value>
        protected int OrderStateID { get; private set; }

        /// <summary>Gets the identifier of the billing type.</summary>
        /// <value>The identifier of the billing type.</value>
        protected int BillingTypeID { get; private set; }

        /// <summary>Gets the identifier of the shipping type.</summary>
        /// <value>The identifier of the shipping type.</value>
        protected int ShippingTypeID { get; private set; }

        /// <summary>Gets or sets the default currency identifier.</summary>
        /// <value>The default currency identifier.</value>
        protected int DefaultCurrencyID { get; set; }

        /////// <summary>Gets the preferred payment method attribute.</summary>
        /////// <value>The preferred payment method attribute.</value>
        ////protected IBaseModel PreferredPaymentMethodAttr { get; private set; }

        /// <summary>Gets or sets the identifier of the customer note type.</summary>
        /// <value>The identifier of the customer note type.</value>
        private int CustomerNoteTypeID { get; set; }

        /// <inheritdoc/>
        public virtual Task InitAsync(
            int orderStatusPendingID,
            int orderStatusPaidID,
            int orderStatusOnHoldID,
            int orderTypeID,
            int orderStateID,
            int billingTypeID,
            int shippingTypeID,
            int customerNoteTypeID,
            int defaultCurrencyID,
            ////IBaseModel preferredPaymentMethodAttr,
            string? contextProfileName)
        {
            OrderStatusPendingID = orderStatusPendingID;
            OrderStatusPaidID = orderStatusPaidID;
            OrderStatusOnHoldID = orderStatusOnHoldID;
            OrderTypeID = orderTypeID;
            OrderStateID = orderStateID;
            BillingTypeID = billingTypeID;
            ShippingTypeID = shippingTypeID;
            CustomerNoteTypeID = customerNoteTypeID;
            DefaultCurrencyID = defaultCurrencyID;
            ////PreferredPaymentMethodAttr = preferredPaymentMethodAttr;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            CartByIDLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            int? selectedAccountID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            int? selectedAccountID,
            string? contextProfileName);

        /// <summary>Builds shipment from selected rate quote.</summary>
        /// <param name="cart">           The cart.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="billingContact"> The billing contact.</param>
        /// <param name="shippingContact">The shipping contact.</param>
        /// <returns>A Shipment.</returns>
        protected static CEFActionResponse<Shipment> BuildShipmentFromSelectedRateQuote(
            ICartModel cart,
            DateTime timestamp,
            IContact billingContact,
            IContact shippingContact)
        {
            if (cart.RateQuotes?.Any(x => x.Selected) != true)
            {
                return CEFAR.FailingCEFAR<Shipment>(
                    "WARNING! Could not build a shipment because there wasn't a selected Rate Quote");
            }
            var selectedRateQuote = cart.RateQuotes.First(x => x.Selected);
            var shipment = new Shipment
            {
                Active = true,
                CreatedDate = timestamp,
                CustomKey = $"{selectedRateQuote.CustomKey}|{selectedRateQuote.Name}",
                DestinationContact = (Contact)shippingContact,
                OriginContact = (Contact)billingContact,
                PublishedRate = selectedRateQuote.Rate,
                ShipCarrierMethodID = selectedRateQuote.ShipCarrierMethodID,
                ShipCarrierID = selectedRateQuote.ShipCarrierMethod!.ShipCarrierID,
                EstimatedDeliveryDate = selectedRateQuote.EstimatedDeliveryDate,
                TargetShippingDate = selectedRateQuote.TargetShippingDate,
            };
            return shipment.WrapInPassingCEFAR()!;
        }

        /// <summary>Gets modified value.</summary>
        /// <param name="baseAmount">The base amount.</param>
        /// <param name="modifier">  The modifier.</param>
        /// <param name="mode">      The mode.</param>
        /// <returns>The modified value.</returns>
        protected static decimal GetModifiedValue(decimal? baseAmount, decimal? modifier, int? mode)
        {
            mode ??= (int)Enums.TotalsModifierModes.Add;
            modifier ??= 0;
            baseAmount ??= 0;
            return (Enums.TotalsModifierModes)mode.Value switch
            {
                Enums.TotalsModifierModes.Unknown => baseAmount.Value, // No Change
                Enums.TotalsModifierModes.Override => modifier.Value,
                Enums.TotalsModifierModes.Add => baseAmount.Value + modifier.Value,
                Enums.TotalsModifierModes.PercentMarkup => baseAmount.Value * ((modifier.Value + 100) / 100),
                _ => throw new ArgumentException($"Invalid modifier mode: {mode.Value}"),
            };
        }

        /// <summary>Executes the cart is null check operation.</summary>
        /// <param name="cart">The cart.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        protected static CEFActionResponse DoCartIsNullCheck(ICartModel cart)
        {
            return cart.WrapInPassingCEFARIfNotNull("ERROR! Could not look up your cart.");
        }

        /// <summary>Executes the cart empty check operation.</summary>
        /// <param name="cart">The cart.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        protected static CEFActionResponse DoCartEmptyCheck(ICartModel cart)
        {
            return cart.SalesItems.WrapInPassingCEFARIfNotNullOrEmpty<List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>, ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(
                "ERROR! Your cart was empty.");
        }

        /// <summary>Enforce user in pricing factory context.</summary>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="user">                 The user.</param>
        protected static void EnforceUserInPricingFactoryContextIfSet(
            IPricingFactoryContextModel pricingFactoryContext,
            IUserModel? user)
        {
            if (user == null)
            {
                return;
            }
            pricingFactoryContext.UserID = user.ID;
            if (Contract.CheckValidID(user.AccountID))
            {
                pricingFactoryContext.AccountID = user.AccountID;
            }
        }

        /// <summary>Gets order level discounts.</summary>
        /// <param name="cart">     The cart.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A List{AppliedSalesOrderDiscount}.</returns>
        protected static List<AppliedSalesOrderDiscount> GetOrderLevelDiscounts(
            ICartModel cart,
            DateTime timestamp)
        {
            return cart.Discounts!
                .Select(x => new AppliedSalesOrderDiscount
                {
                    Active = true,
                    CreatedDate = timestamp,
                    CustomKey = x.CustomKey,
                    SlaveID = x.DiscountID,
                    DiscountTotal = x.DiscountTotal,
                    ApplicationsUsed = x.ApplicationsUsed,
                })
                .ToList();
        }

        /// <summary>Gets the selected rate quote from the cart.</summary>
        /// <param name="cart">     The cart.</param>
        /// <returns>The selected rate quote(s).</returns>
        protected static List<RateQuote> GetSelectedRateQuote(ICartModel cart)
        {
            return cart.RateQuotes!
                .Where(x => x.Selected)
                .Select(x => new RateQuote
                {
                    // Base Properties
                    CustomKey = x.CustomKey,
                    Active = x.Active,
                    CreatedDate = x.CreatedDate,
                    JsonAttributes = x.SerializableAttributes.SerializeAttributesDictionary(),
                    // NameableBase Properties
                    Name = x.Name,
                    Description = x.Description,
                    // RateQuote Properties
                    CartHash = x.CartHash,
                    Rate = x.Rate,
                    RateTimestamp = x.RateTimestamp,
                    Selected = true,
                    TargetShippingDate = x.TargetShippingDate,
                    EstimatedDeliveryDate = x.EstimatedDeliveryDate,
                    // Related objects
                    ShipCarrierMethodID = x.ShipCarrierMethodID,
                })
                .ToList();
        }

        /// <summary>Wipe IDs from main contacts.</summary>
        /// <param name="cart">The cart.</param>
        protected static void WipeIDsFromMainContacts(ISalesCollectionBaseModel cart)
        {
            // Wipe the IDs from the contacts so they are forcefully regenerated
            WipeIDsFromContact(cart.BillingContact);
            WipeIDsFromContact(cart.ShippingContact);
        }

        /// <summary>Wipe IDs from contact.</summary>
        /// <param name="contact">The contact.</param>
        /// <returns>An IContactModel.</returns>
        protected static IContactModel WipeIDsFromContact(IContactModel? contact)
        {
            if (!Contract.CheckValidID(contact?.ID))
            {
                return contact!;
            }
            contact!.ID = 0;
            if (!Contract.CheckValidID(contact.Address?.ID))
            {
                return contact;
            }
            contact.AddressID = contact.Address!.ID = 0;
            return contact;
        }

        /// <summary>Transfer contacts list.</summary>
        /// <param name="cart">  The cart.</param>
        /// <param name="entity">The entity.</param>
        protected static void TransferContactsList(ICartModel cart, ISalesOrder entity)
        {
            if (Contract.CheckEmpty(cart.Contacts))
            {
                return;
            }
            foreach (var contact in cart.Contacts!)
            {
                (entity.Contacts ??= new HashSet<SalesOrderContact>()).Add(new()
                {
                    Active = contact.Active,
                    CreatedDate = contact.CreatedDate,
                    SlaveID = contact.ContactID,
                });
            }
        }

        /// <summary>Gets payment gateway.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The payment gateway.</returns>
        protected static Task<IPaymentsProviderBase?> GetPaymentGatewayAsync(string? contextProfileName)
        {
            if (CommonCheckoutProviderConfig.IntentionallyNoPaymentProvider)
            {
                return Task.FromResult<IPaymentsProviderBase?>(null);
            }
            return Task.FromResult<IPaymentsProviderBase?>(
                RegistryLoaderWrapper.GetPaymentProvider(contextProfileName)
                ?? throw new InvalidOperationException(
                    "ERROR! Could not load a Payment Provider. Are your provider settings in the web config correct?"));
        }

        /// <summary>Relate Optional Status.</summary>
        /// <param name="entity">            The entity that has a Optional Status.</param>
        /// <param name="model">             The model that has a Optional Status.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task RelateRequiredStatusAsync(
            ISalesOrder entity,
            ISalesOrderModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Must have the core objects on both sides
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            // Look up the Object Model
            var resolved = await Workflows.SalesOrderStatuses.ResolveWithAutoGenerateAsync(
                    byID: model.StatusID,
                    byKey: model.StatusKey,
                    byName: model.StatusName,
                    byDisplayName: null,
                    model: model.Status,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var entityIDIsNull = entity.StatusID is <= 0 or int.MaxValue;
            if (entityIDIsNull && entity.StatusID > 0)
            {
                // Scenario 1: Bad data in the database, must be fixed by a dev
                throw new ArgumentException("Somehow, the database has an invalid StatusID on this entity, please correct the data manually");
            }
            var modelIDIsNull = resolved.Result?.ID == null || resolved.Result.ID is <= 0 or int.MaxValue;
            var entityObjectIsNull = entity.Status == null;
            var modelObjectIsNull = resolved == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // Scenario 2: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = entity.StatusID == resolved!.Result?.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Status!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // Scenario 3: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull)
                {
                    entity.Status!.UpdateSalesOrderStatusFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // Scenario 4: We have IDs but they don't match, assign the model's type ID to the entity's type ID
                entity.StatusID = resolved.Result!.ID;
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && resolved.Result!.ID == 0;
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // Scenario 5: We have IDs but they don't match, assign the model's type ID to the entity's type ID (from the model object)
                    entity.StatusID = resolved.Result!.ID;
                    return;
                }
                // Scenario 6: We have IDs but they don't match and the type model has been deactivated, remove the type entity from it's master
                entity.StatusID = 0;
                entity.Status = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.StatusID = 0;
                entity.Status = (SalesOrderStatus)resolved.Result!.CreateSalesOrderStatusEntity(timestamp, contextProfileName);
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 8: We don't have an entity id, and we have a new model, assign the new model
                entity.Status = (SalesOrderStatus)resolved.Result!.CreateSalesOrderStatusEntity(timestamp, contextProfileName);
                return;
            }
            // ReSharper disable once InvertIf
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                entity.StatusID = 0;
                entity.Status = null;
                return;
            }
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the type)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.Status.UpdateSalesOrderStatusFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // Scenario 10: Could not figure out what to do
            throw new InvalidOperationException("Couldn't figure out how to relate the given Status to the SalesOrder entity");
        }

        /// <summary>Relate Optional State.</summary>
        /// <param name="entity">            The entity that has a Optional State.</param>
        /// <param name="model">             The model that has a Optional State.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task RelateRequiredStateAsync(
            ISalesOrder entity,
            ISalesOrderModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Must have the core objects on both sides
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            // Look up the Object Model
            var resolved = await Workflows.SalesOrderStates.ResolveWithAutoGenerateAsync(
                    model.StateID,
                    byKey: model.StateKey,
                    byName: model.StateName,
                    byDisplayName: null,
                    model: model.State,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var entityIDIsNull = entity.StateID is <= 0 or int.MaxValue;
            if (entityIDIsNull && entity.StateID > 0)
            {
                // Scenario 1: Bad data in the database, must be fixed by a dev
                throw new ArgumentException("Somehow, the database has an invalid StateID on this entity, please correct the data manually");
            }
            var modelIDIsNull = resolved.Result?.ID == null || resolved.Result.ID is <= 0 or int.MaxValue;
            var entityObjectIsNull = entity.State == null;
            var modelObjectIsNull = resolved.Result == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // Scenario 2: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = entity.StateID == resolved.Result?.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.State!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // Scenario 3: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull)
                {
                    entity.State!.UpdateSalesOrderStateFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // Scenario 4: We have IDs but they don't match, assign the model's type ID to the entity's type ID
                entity.StateID = resolved.Result!.ID;
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && resolved.Result!.ID == 0;
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // Scenario 5: We have IDs but they don't match, assign the model's type ID to the entity's type ID (from the model object)
                    entity.StateID = resolved.Result!.ID;
                    return;
                }
                // Scenario 6: We have IDs but they don't match and the type model has been deactivated, remove the type entity from it's master
                entity.StateID = 0;
                entity.State = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.StateID = 0;
                entity.State = (SalesOrderState)resolved.Result!.CreateSalesOrderStateEntity(timestamp, contextProfileName);
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 8: We don't have an entity id, and we have a new model, assign the new model
                entity.State = (SalesOrderState)resolved.Result!.CreateSalesOrderStateEntity(timestamp, contextProfileName);
                return;
            }
            // ReSharper disable once InvertIf
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                entity.StateID = 0;
                entity.State = null;
                return;
            }
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the type)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.State.UpdateSalesOrderStateFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // Scenario 10: Could not figure out what to do
            throw new InvalidOperationException("Couldn't figure out how to relate the given State to the SalesOrder entity");
        }

        /// <summary>Relate Optional Type.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model that has a Optional Type.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task RelateRequiredTypeAsync(
            ISalesOrder entity,
            ISalesOrderModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Must have the core objects on both sides
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            // Look up the Object Model
            var resolved = await Workflows.SalesOrderTypes.ResolveWithAutoGenerateAsync(
                    byID: model.TypeID,
                    byKey: model.TypeKey,
                    byName: model.TypeName,
                    byDisplayName: null,
                    model: model.Type,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var entityIDIsNull = entity.TypeID is <= 0 or int.MaxValue;
            if (entityIDIsNull && entity.TypeID > 0)
            {
                // Scenario 1: Bad data in the database, must be fixed by a dev
                throw new ArgumentException("Somehow, the database has an invalid TypeID on this entity, please correct the data manually");
            }
            var modelIDIsNull = resolved.Result?.ID == null || resolved.Result.ID is <= 0 or int.MaxValue;
            var entityObjectIsNull = entity.Type == null;
            var modelObjectIsNull = resolved.Result == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // Scenario 2: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = entity.TypeID == resolved.Result?.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Type!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // Scenario 3: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull)
                {
                    entity.Type!.UpdateSalesOrderTypeFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // Scenario 4: We have IDs but they don't match, assign the model's type ID to the entity's type ID
                entity.TypeID = resolved.Result!.ID;
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && resolved.Result!.ID == 0;
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // Scenario 5: We have IDs but they don't match, assign the model's type ID to the entity's type ID (from the model object)
                    entity.TypeID = resolved.Result!.ID;
                    return;
                }
                // Scenario 6: We have IDs but they don't match and the type model has been deactivated, remove the type entity from it's master
                entity.TypeID = 0;
                entity.Type = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.TypeID = 0;
                entity.Type = (SalesOrderType)resolved.Result!.CreateSalesOrderTypeEntity(timestamp, contextProfileName);
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 8: We don't have an entity id, and we have a new model, assign the new model
                entity.Type = (SalesOrderType)resolved.Result!.CreateSalesOrderTypeEntity(timestamp, contextProfileName);
                return;
            }
            // ReSharper disable once InvertIf
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                entity.TypeID = 0;
                entity.Type = null;
                return;
            }
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the type)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.Type.UpdateSalesOrderTypeFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // Scenario 10: Could not figure out what to do
            throw new InvalidOperationException("Couldn't figure out how to relate the given Type to the SalesOrder entity");
        }

        /// <summary>Validates the payoneer provider is ready for order.</summary>
        /// <param name="storeID">                   The cart.</param>
        /// <param name="buyer">                     The buyer.</param>
        /// <param name="paymentStyle">              The payment style.</param>
        /// <param name="overridePayoneerAccountID"> Overriding identifier buyer's payoneer account.</param>
        /// <param name="overridePayoneerCustomerID">Overriding identifier buyer's payoneer customer.</param>
        /// <param name="contextProfileName">        Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected static async Task<bool> ValidatePayoneerReadyForOrderAsync(
            int? storeID,
            IUserModel buyer,
            string paymentStyle,
            long? overridePayoneerAccountID,
            long? overridePayoneerCustomerID,
            string? contextProfileName)
        {
            if (!CommonCheckoutProviderConfig.PayoneerPaymentProviderAvailable)
            {
                return true;
            }
            if (paymentStyle != "Payoneer")
            {
                return true;
            }
            if (Contract.CheckInvalidID(storeID))
            {
                return true;
            }
            return PayoneerPaymentsProvider.ValidateReady(
                store: await Workflows.Stores.GetAsync(storeID!.Value, contextProfileName).ConfigureAwait(false),
                seller: (await Workflows.Stores.GetStoreAdministratorUserAsync(storeID.Value, contextProfileName).ConfigureAwait(false)).Result!,
                buyer: buyer,
                payoneerAccountID: overridePayoneerAccountID,
                payoneerCustomerID: overridePayoneerCustomerID);
        }

        /// <summary>Process the payoneer for checkout.</summary>
        /// <param name="storeID">                   The store identifier.</param>
        /// <param name="buyer">                     The buyer.</param>
        /// <param name="order">                     The order.</param>
        /// <param name="cart">                      The cart.</param>
        /// <param name="amount">                    The amount.</param>
        /// <param name="paymentStyle">              The payment style.</param>
        /// <param name="overridePayoneerAccountID"> Overriding identifier buyer's payoneer account.</param>
        /// <param name="overridePayoneerCustomerID">Overriding identifier buyer's payoneer customer.</param>
        /// <param name="contextProfileName">        Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        protected static async Task<CEFActionResponse> ProcessPayoneerForOrderAsync(
            int? storeID,
            IUserModel? buyer,
            ISalesOrder order,
            ISalesCollectionBaseModel cart,
            decimal amount,
            string? paymentStyle,
            long? overridePayoneerAccountID,
            long? overridePayoneerCustomerID,
            string? contextProfileName)
        {
            if (!CommonCheckoutProviderConfig.PayoneerPaymentProviderAvailable)
            {
                return CEFAR.PassingCEFAR("NOTE! Payoneer is not enabled, skipping Payoneer processing.");
            }
            if (paymentStyle != Enums.PaymentMethodsStrings.Payoneer)
            {
                return CEFAR.PassingCEFAR("NOTE! The Payment Style was not Payoneer, skipping Payoneer processing.");
            }
            if (Contract.CheckNullID(storeID))
            {
                return CEFAR.FailingCEFAR("ERROR! The Payoneer Payment Style cannot be used without a Store ID");
            }
            var payoneerEscrowCreateOrderResult = new PayoneerPaymentsProvider().CreateAnEscrowOrderToFacilitateATransactionForGoods(
                store: Contract.CheckValidID(storeID) ? (await Workflows.Stores.GetAsync(storeID!.Value, contextProfileName).ConfigureAwait(false))! : null,
                seller: (await Workflows.Stores.GetStoreAdministratorUserAsync(storeID!.Value, contextProfileName).ConfigureAwait(false)).Result!,
                buyer: buyer!,
                order: order,
                cart: cart,
                amount: amount,
                payoneerAccountID: overridePayoneerAccountID,
                payoneerCustomerID: overridePayoneerCustomerID);
            if (payoneerEscrowCreateOrderResult.ActionSucceeded)
            {
                // Update Attributes
                await Workflows.Users.UpdateAsync(buyer!, contextProfileName).ConfigureAwait(false);
            }
            // Return the result
            return payoneerEscrowCreateOrderResult;
        }

        /// <summary>Try resolve cart.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="validate">             A flag indicating to validate the cart or not.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICartModel.</returns>
        protected static async Task<CEFActionResponse<ICartModel>> TryResolveCartAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            bool validate,
            string? contextProfileName)
        {
            var lookup = new CartByIDLookupKey();
            if (Contract.CheckNotNull(checkout.WithCartInfo?.CartID))
            {
                lookup = lookupKey.ToIDLookupKey(checkout.WithCartInfo!.CartID!.Value);
                lookup.AltAccountID = lookupKey.AltAccountID;
            }
            var cart = Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                ? await Workflows.Carts.AdminGetAsync(
                        lookup,
                        pricingFactoryContext,
                        taxesProvider,
                        contextProfileName)
                    .ConfigureAwait(false)
                : Contract.CheckNotNull(checkout.WithCartInfo?.CartSessionID)
                    ? (await Workflows.Carts.SessionGetAsync(
                            lookupKey,
                            pricingFactoryContext,
                            taxesProvider,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .cartResponse.Result
                    : null;
            if (cart is null)
            {
                return CEFAR.FailingCEFAR<ICartModel>("ERROR! Cart was null");
            }
            var validateResponse = validate
                ? await Workflows.CartValidator.ValidateReadyForCheckoutAsync(
                        cart: cart,
                        currentAccount: Contract.CheckValidID(pricingFactoryContext.AccountID)
                            ? await Workflows.Accounts.GetForCartValidatorAsync(pricingFactoryContext.AccountID!.Value, contextProfileName).ConfigureAwait(false)
                            : null,
                        taxesProvider: taxesProvider,
                        pricingFactoryContext: pricingFactoryContext,
                        currentUserID: lookupKey.UserID ?? pricingFactoryContext.UserID,
                        currentAccountID: lookupKey.AccountID ?? pricingFactoryContext.AccountID,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false)
                : CEFAR.PassingCEFAR();
            var response = CEFAR.FailingCEFAR<ICartModel>();
            if (!validateResponse.ActionSucceeded)
            {
                // Cart has been modified, get the cart again with changes
                cart = Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                    ? await Workflows.Carts.AdminGetAsync(
                            lookupKey.ToIDLookupKey(checkout.WithCartInfo!.CartID!.Value),
                            pricingFactoryContext,
                            taxesProvider,
                            contextProfileName)
                        .ConfigureAwait(false)
                    : Contract.CheckNotNull(checkout.WithCartInfo?.CartSessionID)
                        ? (await Workflows.Carts.SessionGetAsync(
                                lookupKey,
                                pricingFactoryContext,
                                taxesProvider,
                                contextProfileName)
                            .ConfigureAwait(false))
                        .cartResponse.Result
                        : null;
            }
            response.ActionSucceeded = cart != null;
            response.Result = cart;
            response.Messages.AddRange(validateResponse.Messages);
            if (!response.ActionSucceeded && Contract.CheckEmpty(response.Messages))
            {
                response.Messages.Add("ERROR! Cart was null, no other messages provided.");
            }
            return response;
        }

        /// <summary>Try resolve user.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{IUserModel}.</returns>
        protected static async Task<CEFActionResponse<IUserModel?>> TryResolveUserAsync(
            ICheckoutModel checkout,
            string? contextProfileName)
        {
            // No username means Guest Checkout
            if (!Contract.CheckValidID(checkout.WithUserInfo?.UserID)
                && !Contract.CheckValidKey(checkout.WithUserInfo?.UserName)
                && !Contract.CheckValidKey(checkout.WithUserInfo?.ExternalUserID))
            {
                return CEFAR.FailingCEFAR<IUserModel?>(
                    "WARNING! No user data to resolve a user with, assuming Guest Checkout.");
            }
            // Get User, Create if doesn't exist
            var currentUserName = checkout.WithUserInfo!.ExternalUserID ?? checkout.WithUserInfo.UserName;
            var user = Contract.CheckValidID(checkout.WithUserInfo.UserID)
                ? await Workflows.Users.GetAsync(checkout.WithUserInfo.UserID!.Value, contextProfileName).ConfigureAwait(false)
                : await Workflows.Users.GetAsync(currentUserName!, contextProfileName).ConfigureAwait(false);
            if (user == null)
            {
                user = await CreateUserFromCheckoutAsync(
                        checkout: checkout,
                        createAccountForUser: ProviderConfig.GetBooleanSetting("Clarity.Checkout.CreateAccountWithUser", true),
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            else
            {
                if (user.Account is { Active: false })
                {
                    return CEFAR.FailingCEFAR<IUserModel?>(
                        "ERROR! Your Account has been deactivated. Please contact support for assistance.");
                }
                BackFillContactInfoFromUserIfMissing(checkout.Billing, checkout.Shipping, user);
            }
            return user.WrapInPassingCEFAR();
        }

        /// <summary>Try to use wallet if set.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="userID">            The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An ICheckoutResult.</returns>
        protected static async Task<CEFActionResponse> TryToUseWalletIfSetAsync(
            ICheckoutModel checkout,
            int? userID,
            string? contextProfileName)
        {
            // When the user uses a card in the wallet and we store credit card access info in CEF
            // If this works, it will apply wallet data to the checkout model
            return Contract.CheckValidID(checkout.PayByWalletEntry?.WalletID)
                ? await UseInternalWalletAsync(checkout, userID, contextProfileName).ConfigureAwait(false)
                : CEFAR.PassingCEFAR("INFO: No Wallet info was provided, cannot use internal Wallet");
        }

        /// <summary>Maps to the CustomKeys of the PaymentMethods.</summary>
        /// <param name="style">The style.</param>
        /// <returns>The CustomKey of the appropriate Method.</returns>
        protected static string MapToPaymentMethodKey(string? style)
        {
            return style switch
            {
                Enums.PaymentMethodsStrings.ACH => "ACH",
                Enums.PaymentMethodsStrings.CreditCard => "Credit Card",
                Enums.PaymentMethodsStrings.Echeck => "eCheck",
                Enums.PaymentMethodsStrings.Free => "Free",
                Enums.PaymentMethodsStrings.Invoice => "Invoice",
                Enums.PaymentMethodsStrings.Payoneer => "Payoneer",
                Enums.PaymentMethodsStrings.PayPal => "PayPal",
                Enums.PaymentMethodsStrings.QuoteMe => "Quote Me",
                Enums.PaymentMethodsStrings.StoreCredit => "Store Credit / Gift Certificate",
                Enums.PaymentMethodsStrings.WireTransfer => "Wire Transfer",
                _ => "General",
            };
        }

        /// <summary>Relate state status and type using dummy.</summary>
        /// <param name="attributes">        The attributes.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="statusKey">         The key of the status.</param>
        /// <param name="typeKey">           The key of the type.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An ISalesOrderModel.</returns>
        protected static async Task<ISalesOrderModel> RelateStateStatusTypeAndAttributesUsingDummyAsync(
            SerializableAttributesDictionary attributes,
            ISalesOrder entity,
            DateTime timestamp,
            string statusKey,
            string typeKey,
            string? contextProfileName)
        {
            var dummy = RegistryLoaderWrapper.GetInstance<ISalesOrderModel>(contextProfileName);
            var state = RegistryLoaderWrapper.GetInstance<IStateModel>(contextProfileName);
            state.Name = state.DisplayName = "Work";
            state.CustomKey = "WORK";
            var status = RegistryLoaderWrapper.GetInstance<IStatusModel>(contextProfileName);
            status.CustomKey = status.Name = status.DisplayName = statusKey;
            var type = RegistryLoaderWrapper.GetInstance<ITypeModel>(contextProfileName);
            type.CustomKey = type.Name = type.DisplayName = typeKey;
            dummy.State = state;
            dummy.Status = status;
            dummy.Type = type;
            await RelateRequiredStateAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredStatusAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredTypeAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            dummy.SerializableAttributes = attributes;
            return dummy;
        }

        /// <summary>Transfer cart items to order items.</summary>
        /// <param name="context">           The context.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="cartItem">          The cart item.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task TransferCartItemsToOrderItemsAsync(
            IClarityEcommerceEntities context,
            DateTime timestamp,
            IBase entity,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> cartItem,
            string? contextProfileName)
        {
            var item = new SalesOrderItem
            {
                // Base Properties
                Active = true,
                CustomKey = cartItem.CustomKey,
                CreatedDate = timestamp,
                UpdatedDate = null,
                // NameableBaseProperties
                Name = cartItem.Name ?? cartItem.ProductName,
                Description = cartItem.Description ?? cartItem.ProductShortDescription,
                // SalesItemBase Properties
                Sku = cartItem.Sku ?? cartItem.ProductKey,
                ForceUniqueLineItemKey = cartItem.ForceUniqueLineItemKey,
                UnitOfMeasure = cartItem.UnitOfMeasure ?? cartItem.ProductUnitOfMeasure,
                Quantity = cartItem.Quantity,
                QuantityBackOrdered = cartItem.QuantityBackOrdered ?? 0m,
                QuantityPreSold = cartItem.QuantityPreSold ?? 0m,
                UnitCorePrice = cartItem.UnitCorePrice,
                UnitSoldPrice = GetModifiedValue(
                    cartItem.UnitSoldPrice ?? cartItem.UnitCorePrice,
                    cartItem.UnitSoldPriceModifier,
                    cartItem.UnitSoldPriceModifierMode),
                UnitCorePriceInSellingCurrency = cartItem.UnitCorePriceInSellingCurrency,
                UnitSoldPriceInSellingCurrency = cartItem.UnitSoldPriceInSellingCurrency,
                OriginalCurrencyID = cartItem.OriginalCurrencyID,
                SellingCurrencyID = cartItem.SellingCurrencyID,
                // Related Objects
                MasterID = entity.ID,
                ProductID = cartItem.ProductID,
                UserID = cartItem.UserID,
            };
            // Transfer Attributes
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(item, cartItem, contextProfileName).ConfigureAwait(false);
            // Transfer Discounts
            if (cartItem.Discounts?.Any() == true)
            {
                var discounts = item.Discounts ?? new List<AppliedSalesOrderItemDiscount>();
                cartItem.Discounts
                    .Select(x => new AppliedSalesOrderItemDiscount
                    {
                        ID = x.ID,
                        CustomKey = x.CustomKey,
                        Active = x.Active,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        SlaveID = x.DiscountID,
                        DiscountTotal = x.DiscountTotal,
                        ApplicationsUsed = x.ApplicationsUsed,
                    })
                    .ForEach(x => discounts.Add(x));
                item.Discounts = discounts;
            }
            context.SalesOrderItems.Add(item);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            // Don't deactivate yet until the warehouse shipping email has been sent
            if (!CEFConfigDictionary.GetClosestWarehouseWithStock)
            {
                await Workflows.CartItems.DeactivateAsync(cartItem.ID, contextProfileName).ConfigureAwait(false);
            }
            var salesOrder = (await Workflows.SalesOrders.GetAsync(entity.ID, context).ConfigureAwait(false))!;
            var regionID = salesOrder.ShippingSameAsBilling ?? false
                ? salesOrder.BillingContact?.Address?.RegionID
                : salesOrder.ShippingContact?.Address?.RegionID;
            var region = Contract.CheckValidID(regionID)
                ? await Workflows.Regions.GetAsync(regionID!.Value, context).ConfigureAwait(false)
                : null;
            await UpdateStockInfoForCartItemAsync(cartItem, region?.Code, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Updates the stock information for cart item.</summary>
        /// <param name="cartItem">          The cart item.</param>
        /// <param name="shippingRegionCode">The shipping region code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task UpdateStockInfoForCartItemAsync(
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> cartItem,
            string? shippingRegionCode,
            string? contextProfileName)
        {
            // Decrement this product's inventory by increasing the Allocated amount
            if (!Contract.CheckValidIDOrKey(cartItem.ProductID, cartItem.ProductKey))
            {
                // Do Nothing
                return;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var useID = Contract.CheckValidID(cartItem.ProductID);
            var isUnlimited = useID
                ? await context.Products.AsNoTracking().FilterByActive(true).FilterByID(cartItem.ProductID).Select(x => x.IsUnlimitedStock).SingleAsync().ConfigureAwait(false)
                : await context.Products.AsNoTracking().FilterByActive(true).FilterByCustomKey(cartItem.ProductKey).Select(x => x.IsUnlimitedStock).SingleAsync().ConfigureAwait(false);
            if (isUnlimited)
            {
                // Do Nothing as we don't count stock for this product
                return;
            }
            if (!CEFConfigDictionary.InventoryAdvancedEnabled)
            {
                // Use Flat stock setup
                var flatProduct = useID
                    ? await context.Products.FilterByActive(true).FilterByID(cartItem.ProductID).SingleAsync().ConfigureAwait(false)
                    : await context.Products.FilterByActive(true).FilterByCustomKey(cartItem.ProductKey).SingleAsync().ConfigureAwait(false);
                flatProduct.StockQuantityAllocated ??= 0m;
                flatProduct.StockQuantityPreSold ??= 0m;
                flatProduct.StockQuantityAllocated += cartItem.Quantity + (cartItem.QuantityBackOrdered ?? 0m);
                flatProduct.StockQuantityPreSold += cartItem.QuantityPreSold ?? 0m;
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                return;
            }
            // Use PILS setup
            var pilsProduct = useID
                ? await context.Products.FilterByActive(true).FilterByID(cartItem.ProductID).SingleAsync().ConfigureAwait(false)
                : await context.Products.FilterByActive(true).FilterByCustomKey(cartItem.ProductKey).SingleAsync().ConfigureAwait(false);
#if FALSE
                var storeProduct = pilsProduct.Stores.FirstOrDefault(x => x.ID == cartItem.StoreProductID);
                if (cartItem.ProductInventoryLocationSectionID.HasValue
                    && cartItem.ProductInventoryLocationSectionID > 0
                    && cartItem.ProductInventoryLocationSectionID != int.MaxValue)
                {
                    // We specified ProductInventoryLocationSectionID, do any PILS
                    var section = pilsProduct.ProductInventoryLocationSections
                        .SingleOrDefault(x => x.ID == cartItem.ProductInventoryLocationSectionID); // Get PILS by ID
                    if (section != null)
                    {
                        if (!section.QuantityAllocated.HasValue) { section.QuantityAllocated = 0m; }
                        if (!section.QuantityPreSold.HasValue) { section.QuantityPreSold = 0m; }
                        section.QuantityAllocated += cartItem.Quantity + (cartItem.QuantityBackOrdered ?? 0m);
                        section.QuantityPreSold += cartItem.QuantityPreSold ?? 0m;
                        await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                    }
                }
                else if (storeProduct != null)
                {
                    // Do PILS for Store
                    var section = pilsProduct.ProductInventoryLocationSections
                        .FirstOrDefault(x => x.InventoryLocationSection.Active
                            && x.InventoryLocationSection.InventoryLocation.Active
                            && x.InventoryLocationSection.InventoryLocation.Stores
                                .Any(y => y.Active && y.StoreID == storeProduct.StoreID));
                    // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                    if (section == null)
                    {
                        // Do any PILS
                        section = pilsProduct.ProductInventoryLocationSections
                            .FirstOrDefault(x => x.InventoryLocationSection.Active
                                && x.InventoryLocationSection.InventoryLocation.Active);
                    }
                    if (section != null)
                    {
                        if (!section.QuantityAllocated.HasValue) { section.QuantityAllocated = 0m; }
                        if (!section.QuantityPreSold.HasValue) { section.QuantityPreSold = 0m; }
                        section.QuantityAllocated += cartItem.Quantity + (cartItem.QuantityBackOrdered ?? 0m);
                        section.QuantityPreSold += cartItem.QuantityPreSold ?? 0m;
                        await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                    }
                }
                else
#endif
            {
                // Do any PILS
                var sections = pilsProduct.ProductInventoryLocationSections!
                    .Where(x => x.Active
                        && x.Slave!.Active
                        && x.Slave.InventoryLocation!.Active);
                if (CEFConfigDictionary.GetClosestWarehouseWithStock)
                {
                    var closestSection = Workflows.ProductInventoryLocationSections.GetClosestWarehouseByRegionCode(
                        shippingRegionCode!,
                        pilsProduct.ID,
                        contextProfileName);
                    if (closestSection != null)
                    {
                        sections = pilsProduct.ProductInventoryLocationSections!
                            .Where(x => x.Active
                                && x.Slave!.Active
                                && x.Slave.InventoryLocation!.Active
                                && x.Slave.InventoryLocation.Name == closestSection.Slave!.InventoryLocation!.Name)
                            .ToList();
                    }
                }
                var totalQty = cartItem.Quantity + (cartItem.QuantityBackOrdered ?? 0m);
                var qtyNeeded = totalQty;
                foreach (var section in sections)
                {
                    section.Quantity ??= 0m;
                    section.QuantityAllocated ??= 0m;
                    section.QuantityPreSold ??= 0m;
                    section.QuantityPreSold += cartItem.QuantityPreSold ?? 0m;
                    if (section.Quantity - section.QuantityAllocated.Value <= qtyNeeded && qtyNeeded != 0)
                    {
                        qtyNeeded -= section.Quantity.Value - section.QuantityAllocated.Value;
                        section.QuantityAllocated = section.Quantity;
                    }
                    else
                    {
                        section.QuantityAllocated += qtyNeeded;
                        break;
                    }
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
        }

        /// <summary>Handles the wallet.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="gateway">           The gateway.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task HandleWalletAsync(
            IUserModel? user,
            ICheckoutModel checkout,
            IPaymentsProviderBase? gateway,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.PaymentsWalletEnabled)
            {
                // Not Enabled
                return;
            }
            if (gateway is not IWalletProviderBase walletProvider)
            {
                // Wallet not supported
                return;
            }
            if (!Contract.CheckValidID(user?.ID))
            {
                // Not logged in
                return;
            }
            if (!Contract.CheckAnyValidKey(
                    checkout.PayByCreditCard?.CardReferenceName,
                    checkout.PayByECheck?.AccountReferenceName))
            {
                // Didn't provide a nickname
                return;
            }
            var entry = RegistryLoaderWrapper.GetInstance<IWalletModel>(contextProfileName);
            entry.Active = true;
            entry.UserID = user!.ID;
            if (Contract.CheckValidKey(checkout.PayByCreditCard?.CardReferenceName))
            {
                entry.Name = checkout.PayByCreditCard!.CardReferenceName;
                entry.CardHolderName = checkout.PayByCreditCard.CardHolderName;
                entry.CreditCardNumber = checkout.PayByCreditCard.CardNumber;
                entry.ExpirationMonth = checkout.PayByCreditCard.ExpirationMonth;
                entry.ExpirationYear = checkout.PayByCreditCard.ExpirationYear;
                entry.CardType = checkout.PayByCreditCard.CardType;
                ////entry.CVV = checkout.PayByCreditCard.CVV;
            }
            else
            {
                entry.Name = checkout.PayByECheck!.AccountReferenceName;
                entry.AccountNumber = checkout.PayByECheck.AccountNumber;
                entry.RoutingNumber = checkout.PayByECheck.RoutingNumber;
                entry.BankName = checkout.PayByECheck.BankName;
            }
            await Workflows.Wallets.CreateWalletEntryAsync(
                    entry,
                    walletProvider,
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Process the transaction for approval.</summary>
        /// <param name="checkout">               The checkout.</param>
        /// <param name="gateway">                The gateway.</param>
        /// <param name="cart">                   The cart.</param>
        /// <param name="originalCurrencyKey">    The original currency key.</param>
        /// <param name="sellingCurrencyKey">     The selling currency key.</param>
        /// <param name="paymentAlreadyConverted">True if payment already converted.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>A CEFActionResponse{(string token,string transactionID)}.</returns>
        protected virtual async Task<CEFActionResponse<(string? token, string? transactionID)>> ProcessTransactionForApprovalAsync(
            ICheckoutModel checkout,
            IPaymentsProviderBase? gateway,
            ICartModel cart,
            string originalCurrencyKey,
            string sellingCurrencyKey,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            if (gateway is null)
            {
                // Intentionally no payment provider, just pass it
                return new((null, null), true);
            }
            // Integrate with payment processor
            var amount = checkout.IsPartialPayment ? checkout.Amount ?? 0m : cart.Totals!.Total;
            var payment = checkout.CreatePaymentModelFromCheckoutModel();
            payment.CurrencyKey = sellingCurrencyKey;
            payment.Amount = amount;
            /* TODO: Replace with the intended markup feature
            if (CEFConfigDictionary.EnablePaymentMarkup)
            {
                payment.Amount *= 1.0m + (CEFConfigDictionary.PaymentMarkupPercent / 100m);
            }
            */
            var transaction = CommonCheckoutProviderConfig.PaymentProcess switch
            {
                Enums.PaymentProcessMode.AuthorizeOnly => await gateway.AuthorizeAsync(
                        payment: payment,
                        billing: checkout.Billing,
                        shipping: checkout.Shipping,
                        paymentAlreadyConverted: paymentAlreadyConverted,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false),
                _ => await gateway.AuthorizeAndACaptureAsync(
                        payment: payment,
                        billing: checkout.Billing,
                        shipping: checkout.Shipping,
                        paymentAlreadyConverted: paymentAlreadyConverted,
                        contextProfileName: contextProfileName,
                        cart: cart)
                    .ConfigureAwait(false),
            };
            return new((transaction.AuthorizationCode, transaction.TransactionID), transaction.Approved);
        }

        /// <summary>Process the payment.</summary>
        /// <param name="checkout">               The checkout.</param>
        /// <param name="cart">                   The cart.</param>
        /// <param name="gateway">                The gateway.</param>
        /// <param name="originalCurrencyKey">    The original currency key.</param>
        /// <param name="sellingCurrencyKey">     The selling currency key.</param>
        /// <param name="paymentAlreadyConverted">True if payment already converted.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>A CEFActionResponse{(string token,string transactionID,bool makeInvoice,decimal balanceDue)}.</returns>
        protected async Task<CEFActionResponse<IProcessPaymentResponse>> ProcessPaymentAsync(
            ICheckoutModel checkout,
            ICartModel cart,
            IPaymentsProviderBase? gateway,
            string originalCurrencyKey,
            string sellingCurrencyKey,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            switch (checkout.PaymentStyle)
            {
                case Enums.PaymentMethodsStrings.Free when cart.Totals!.Total <= 0m:
                {
                    return CEFAR.PassingCEFAR<IProcessPaymentResponse>(
                        new ProcessPaymentResponse(
                            $"{nameof(Enums.PaymentMethodsStrings.Free)}:{Guid.NewGuid()}",
                            $"{nameof(Enums.PaymentMethodsStrings.Free)}:{Guid.NewGuid()}",
                            false,
                            0m),
                        "SUCCESS! Transaction Automatically Approved as the order total was zero.")!;
                }
                case Enums.PaymentMethodsStrings.Free:
                {
                    return CEFAR.FailingCEFAR<IProcessPaymentResponse>(
                        "ERROR! Transaction Rejected as the order total is non-zero but Free was selected as the " +
                        "payment type.");
                }
                case Enums.PaymentMethodsStrings.Invoice:
                {
                    ////if (Contract.CheckValidKey(checkout.PurchaseOrder))
                    ////{
                    return CEFAR.PassingCEFAR<IProcessPaymentResponse>(
                        new ProcessPaymentResponse(
                            $"{nameof(Enums.PaymentMethodsStrings.Invoice)}:{Guid.NewGuid()}",
                            $"{nameof(Enums.PaymentMethodsStrings.Invoice)}:{Guid.NewGuid()}",
                            true,
                            cart.Totals!.Total),
                        "SUCCESS! Transaction Automatically Approved as Bill Me Later (Purchase Order now, Invoice later).")!;
                    ////}
                }
                case Enums.PaymentMethodsStrings.Payoneer when CommonCheckoutProviderConfig.PayoneerPaymentProviderAvailable:
                {
                    return CEFAR.PassingCEFAR<IProcessPaymentResponse>(
                        new ProcessPaymentResponse(
                            $"{nameof(Enums.PaymentMethodsStrings.Payoneer)}:{Guid.NewGuid()}",
                            $"{nameof(Enums.PaymentMethodsStrings.Payoneer)}:{Guid.NewGuid()}",
                            true,
                            cart.Totals!.Total),
                        "SUCCESS! Transaction Automatically Approved as Payoneer will handle at a later point.")!;
                }
                case Enums.PaymentMethodsStrings.Payoneer:
                {
                    return CEFAR.FailingCEFAR<IProcessPaymentResponse>(
                        "ERROR! Transaction Rejected as Payoneer was selected but is not available.");
                }
                case Enums.PaymentMethodsStrings.ACH:
                {
                    throw new NotImplementedException(
                        "ERROR! ACH hasn't been set up for this method yet");
                }
                case Enums.PaymentMethodsStrings.PayPal:
                {
                    throw new NotImplementedException(
                        "ERROR! PayPal hasn't been set up for this method yet, should full trigger express checkout "
                        + "first.");
                    /*
                    var transactionResult = await ProcessTransactionForApprovalAsync(
                            checkout,
                            gateway,
                            cart,
                            originalCurrencyKey,
                            sellingCurrencyKey,
                            paymentAlreadyConverted,
                            contextProfileName)
                        .ConfigureAwait(false);
                    return transactionResult.ActionSucceeded
                        ? CEFAR.PassingCEFAR(
                            (transactionResult.Result.token,
                                transactionResult.Result.transactionID,
                                checkout.IsPartialPayment,
                                checkout.IsPartialPayment ? checkout.Amount ?? 0m : 0m))!
                        : CEFAR.FailingCEFAR<(string token, string transactionID, bool makeInvoice, decimal balanceDue)>(
                            "ERROR! Your transaction was not approved by the payment provider!");
                    */
                }
                case Enums.PaymentMethodsStrings.QuoteMe:
                {
                    throw new InvalidOperationException(
                        "ERROR! QuoteMe should have triggered quote creation instead of an order");
                }
                case Enums.PaymentMethodsStrings.StoreCredit:
                {
                    // TODO: Read Account Credit data and rules around going passed limits, etc.
                    return CEFAR.PassingCEFAR<IProcessPaymentResponse>(
                        new ProcessPaymentResponse(
                            $"{nameof(Enums.PaymentMethodsStrings.StoreCredit)}:{Guid.NewGuid()}",
                            $"{nameof(Enums.PaymentMethodsStrings.StoreCredit)}:{Guid.NewGuid()}",
                            true,
                            cart.Totals.Total),
                        "SUCCESS! Transaction Automatically Approved as Store Credit / Pay On Account.")!;
                }
                case Enums.PaymentMethodsStrings.WireTransfer:
                {
                    return CEFAR.PassingCEFAR<IProcessPaymentResponse>(
                        new ProcessPaymentResponse(
                            $"{nameof(Enums.PaymentMethodsStrings.WireTransfer)}:{Guid.NewGuid()}",
                            $"{nameof(Enums.PaymentMethodsStrings.WireTransfer)}:{Guid.NewGuid()}",
                            true,
                            cart.Totals.Total),
                        "SUCCESS! Transaction Automatically Approved as Wire Transfer Instructions should have been "
                        + "provided to the customer and the CSR will handle data entry at a later point.")!;
                }
                case Enums.PaymentMethodsStrings.CreditCard when CommonCheckoutProviderConfig.IntentionallyNoPaymentProvider:
                case Enums.PaymentMethodsStrings.Echeck when CommonCheckoutProviderConfig.IntentionallyNoPaymentProvider:
                {
                    return CEFAR.PassingCEFAR<IProcessPaymentResponse>(
                        new ProcessPaymentResponse(
                            $"{nameof(CommonCheckoutProviderConfig.IntentionallyNoPaymentProvider)}:{Guid.NewGuid()}",
                            $"{nameof(CommonCheckoutProviderConfig.IntentionallyNoPaymentProvider)}:{Guid.NewGuid()}",
                            checkout.IsPartialPayment,
                            checkout.IsPartialPayment ? checkout.Amount ?? 0m : 0m),
                        "SUCCESS! Transaction Automatically Approved as there is intentionally no Payment Provider."
                        + " Payments will be handled outside the website order process.")!;
                }
                case Enums.PaymentMethodsStrings.CreditCard:
                case Enums.PaymentMethodsStrings.Echeck:
                {
                    var transactionResult = await ProcessTransactionForApprovalAsync(
                            checkout,
                            gateway,
                            cart,
                            originalCurrencyKey,
                            sellingCurrencyKey,
                            paymentAlreadyConverted,
                            contextProfileName)
                        .ConfigureAwait(false);
                    return transactionResult.ActionSucceeded
                        ? CEFAR.PassingCEFAR<IProcessPaymentResponse>(
                            new ProcessPaymentResponse(
                                transactionResult.Result.token,
                                transactionResult.Result.transactionID,
                                checkout.IsPartialPayment,
                                checkout.IsPartialPayment ? checkout.Amount ?? 0m : 0m))!
                        : CEFAR.FailingCEFAR<IProcessPaymentResponse>(
                            "ERROR! Your transaction was not approved by the payment provider!");
                }
                default:
                {
                    return CEFAR.FailingCEFAR<IProcessPaymentResponse>("ERROR! Unknown Payment Method");
                }
            }
        }

        /// <summary>Copies the notes.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="entity">               The entity.</param>
        /// <param name="dummy">                The dummy.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected async Task CopyNotesAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            ISalesOrder entity,
            ISalesOrderModel dummy,
            DateTime timestamp,
            string? contextProfileName)
        {
            await Workflows.SalesOrderWithNotesAssociation.AssociateObjectsAsync(
                    entity,
                    dummy,
                    timestamp,
                    contextProfileName)
                .ConfigureAwait(false);
            if (Contract.CheckEmpty(entity.Notes))
            {
                entity.Notes = new HashSet<Note>();
                var (cartID, _) = Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                    ? (checkout.WithCartInfo!.CartID!.Value, null)
                    : await Workflows.Carts.SessionGetAsIDAsync(
                            lookupKey: new(
                                sessionID: checkout.WithCartInfo!.CartSessionID!.Value,
                                typeKey: checkout.WithCartInfo.CartTypeName!,
                                userID: pricingFactoryContext.UserID,
                                accountID: pricingFactoryContext.AccountID),
                            pricingFactoryContext: pricingFactoryContext,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                if (Contract.CheckValidID(cartID))
                {
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    foreach (var note in context.Notes.AsNoTracking().Where(x => x.Active && x.CartID == cartID))
                    {
                        entity.Notes.Add(new()
                        {
                            Active = true,
                            CreatedDate = timestamp,
                            TypeID = note.TypeID,
                            CreatedByUserID = note.CreatedByUserID,
                            Note1 = note.Note1,
                        });
                    }
                }
            }
            if (!Contract.CheckValidKey(checkout.SpecialInstructions))
            {
                return;
            }
            entity.Notes!.Add(new()
            {
                Active = true,
                CreatedDate = timestamp,
                TypeID = CustomerNoteTypeID,
                CreatedByUserID = pricingFactoryContext.UserID,
                Note1 = checkout.SpecialInstructions,
            });
        }

        /// <summary>Adds the addresses to book to 'user'.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="user">              The user.</param>
        /// <param name="nothingToShip">     A flag indicating there was nothing to ship.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected async Task HandleAddressBookAsync(
            ICheckoutModel checkout,
            IUserModel? user,
            bool nothingToShip,
            string? contextProfileName)
        {
            if (!CommonCheckoutProviderConfig.AddAddressesToBook)
            {
                return;
            }
            if (!Contract.CheckValidID(user?.AccountID))
            {
                return;
            }
            // ReSharper disable once PossibleInvalidOperationException
            if (!Contract.CheckValidID(await Workflows.Accounts.CheckExistsAsync(
                        user!.AccountID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false)))
            {
                return;
            }
            var addressBook = await Workflows.AddressBooks.GetAddressBookAsync(
                    user.AccountID.Value,
                    contextProfileName)
                .ConfigureAwait(false);
            if (checkout.Billing != null && addressBook.All(x => x.ContactKey != checkout.Billing.CustomKey))
            {
                await AddAddressToBookAsync(
                        checkout.Billing,
                        user.AccountID.Value,
                        true,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (!checkout.IsSameAsBilling
                && !nothingToShip
                && checkout.Shipping != null // Targets checkout will have this be null
                && addressBook.All(x => x.ContactKey != checkout.Shipping.CustomKey))
            {
                await AddAddressToBookAsync(
                        checkout.Shipping,
                        user.AccountID.Value,
                        false,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>Process the memberships for order.</summary>
        /// <param name="salesOrder">           The sales order.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="invoiceID">            Identifier for the invoice.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        protected async Task<CEFActionResponse> ProcessMembershipsForOrderAsync(
            ISalesOrderModel? salesOrder,
            IPricingFactoryContextModel pricingFactoryContext,
            int? invoiceID,
            DateTime? timestamp,
            string? contextProfileName)
        {
            try
            {
                if (salesOrder is null)
                {
                    return CEFAR.FailingCEFAR("ERROR! Sales order is null");
                }
                var provider = RegistryLoaderWrapper.GetMembershipProvider(contextProfileName);
                if (provider?.HasValidConfiguration != true)
                {
                    return CEFAR.FailingCEFAR("No Membership Provider Configured");
                }
                return await CEFAR.AggregateAsync(
                    salesOrder.SalesItems!,
                    item => provider.ImplementProductMembershipFromOrderItemAsync(
                            salesOrderUserID: salesOrder.UserID,
                            salesOrderAccountID: salesOrder.AccountID,
                            salesOrderItem: item,
                            pricingFactoryContext: pricingFactoryContext,
                            invoiceID: invoiceID,
                            timestamp: timestamp ?? DateExtensions.GenDateTime,
                            contextProfileName: contextProfileName))
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{GetType().Name}.{nameof(ProcessMembershipsForOrderAsync)}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR("There was an exception");
            }
        }

        /// <summary>Process the subscriptions for order.</summary>
        /// <param name="salesOrder">The sales order for the product</param>
        /// <param name="pricingFactoryContext">The pricing factory context</param>
        /// <param name="salesGroupID">The sales group ID for the sales group</param>
        /// <param name="invoiceID">The invoice ID for the product</param>
        /// <param name="timestamp">The date of the subscription purchase</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        protected async Task<CEFActionResponse> ProcessSubscriptionsForOrderAsync(
            ISalesOrderModel salesOrder,
            IPricingFactoryContextModel pricingFactoryContext,
            int? salesGroupID,
            int? invoiceID,
            DateTime timestamp,
            string? contextProfileName)
        {
            try
            {
                var provider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
                if (provider is not { HasValidConfiguration: true })
                {
                    return CEFAR.PassingCEFAR("No Payment Provider Configured");
                }
                if (provider is not ISubscriptionProviderBase subscriptionProvider)
                {
                    return CEFAR.PassingCEFAR("The Payment Provider Configured is not a Subscription Provider");
                }
                salesOrder.SalesItems ??= (await Workflows.SalesOrderItems.SearchAsync(
                            new SalesItemBaseSearchModel { MasterID = salesOrder.ID },
                            false,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .results;
                return await CEFAR.AggregateAsync(
                    salesOrder.SalesItems,
                    item => subscriptionProvider.ImplementProductSubscriptionFromOrderItemAsync(
                        salesOrder.UserID,
                        salesOrder.AccountID,
                        salesGroupID,
                        item,
                        pricingFactoryContext,
                        invoiceID,
                        salesOrder.ID,
                        timestamp,
                        contextProfileName))
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{GetType().Name}.{nameof(ProcessSubscriptionsForOrderAsync)}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR();
            }
        }

        /// <summary>Process the calendar events for order.</summary>
        /// <param name="result">            The result.</param>
        /// <param name="contextUserID">     Identifier for the context user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        protected async Task<CEFActionResponse> ProcessCalendarEventsForOrderAsync(
            ICheckoutResult result,
            int? contextUserID,
            string? contextProfileName)
        {
            var failResponse = CEFAR.FailingCEFAR();
            try
            {
                using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
                {
                    var orderIDs = (result.OrderIDs ?? Array.Empty<int>()).Union(new[] { result.OrderID ?? 0 })
                        .Where(x => Contract.CheckValidID(x));
                    foreach (var orderItem in context.SalesOrders
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByIDs(orderIDs)
                        .SelectMany(x => x.SalesItems!
                            .Where(y => y.Active && y.ProductID > 0)
                            .Select(y => new
                            {
                                SalesOrderUserID = x.UserID,
                                SalesOrderItemUserID = y.UserID,
                                ProductID = y.ProductID!.Value,
                            })))
                    {
                        var userID = orderItem.SalesOrderItemUserID
                            ?? orderItem.SalesOrderUserID
                            ?? contextUserID;
                        if (Contract.CheckInvalidID(userID))
                        {
                            continue;
                        }
                        try
                        {
                            await (await context.CalendarEventProducts
                                    .AsNoTracking()
                                    .FilterByActive(true)
                                    .FilterCalendarEventProductsByProductID(orderItem.ProductID)
                                    .Select(x => x.MasterID)
                                    .Distinct()
                                    .ToListAsync()
                                    .ConfigureAwait(false))
                                .ForEachAsync(
                                    1,
                                    eventID => Workflows.UserEventAttendances.CreateAsync(
                                        new UserEventAttendanceModel
                                        {
                                            Active = true,
                                            MasterID = eventID,
                                            SlaveID = userID!.Value,
                                            TypeKey = "GENERAL",
                                        },
                                        contextProfileName))
                                .ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            var message = $"ERROR! Unable to register the user for purchased event(s) tied to product ID {orderItem.ProductID}.";
                            await Logger.LogErrorAsync($"{GetType().Name}.{nameof(ProcessCalendarEventsForOrderAsync)}", message, ex, contextProfileName).ConfigureAwait(false);
                            failResponse.Messages.Add(message);
                        }
                    }
                }
                return failResponse.Messages.Count > 0 ? failResponse : CEFAR.PassingCEFAR();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync($"{GetType().Name}.{nameof(ProcessCalendarEventsForOrderAsync)}", ex.Message, ex, contextProfileName).ConfigureAwait(false);
                failResponse.Messages.Add(ex.Message);
                return failResponse;
            }
        }

        /// <summary>Gets cart.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="cartType">             Type of the cart.</param>
        /// <param name="currentUserID">        Identifier for the current user.</param>
        /// <param name="currentAccountID">     Identifier for the current account.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The cart.</returns>
        protected virtual async Task<ICartModel?> GetCartAsync(
            ICheckoutModel checkout,
            string cartType,
            int? currentUserID,
            int? currentAccountID,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            return Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                ? await Workflows.Carts.AdminGetAsync(
                        lookupKey: new CartByIDLookupKey(
                            cartID: checkout.WithCartInfo!.CartID!.Value,
                            userID: currentUserID,
                            accountID: currentAccountID),
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false)
                : (checkout.WithCartInfo?.CartSessionID).HasValue
                    ? (await Workflows.Carts.SessionGetAsync(
                                lookupKey: new SessionCartBySessionAndTypeLookupKey(
                                    sessionID: checkout.WithCartInfo!.CartSessionID!.Value,
                                    typeKey: cartType,
                                    userID: currentUserID),
                                pricingFactoryContext: pricingFactoryContext,
                                taxesProvider: taxesProvider,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        .cartResponse.Result
                    : null;
        }

        /// <summary>Back fill contact information from user if missing.</summary>
        /// <param name="billingContact"> The billing contact.</param>
        /// <param name="shippingContact">The shipping contact.</param>
        /// <param name="user">           The user.</param>
        private static void BackFillContactInfoFromUserIfMissing(
            IContactModel? billingContact,
            IContactModel? shippingContact,
            IHaveAContactBaseModel user)
        {
            if (user?.Contact == null)
            {
                return;
            }
            if (billingContact != null)
            {
                if (string.IsNullOrWhiteSpace(billingContact.FirstName)
                    || string.IsNullOrWhiteSpace(billingContact.LastName))
                {
                    billingContact.FirstName = user.Contact.FirstName;
                    billingContact.LastName = user.Contact.LastName;
                }
                if (string.IsNullOrWhiteSpace(billingContact.Address?.Street1))
                {
                    billingContact.Address = user.Contact.Address;
                }
            }
            // ReSharper disable once InvertIf
            if (shippingContact != null)
            {
                if (string.IsNullOrWhiteSpace(shippingContact.FirstName)
                    || string.IsNullOrWhiteSpace(shippingContact.LastName))
                {
                    shippingContact.FirstName = user.Contact.FirstName;
                    shippingContact.LastName = user.Contact.LastName;
                }
                if (string.IsNullOrWhiteSpace(shippingContact.Address?.Street1))
                {
                    shippingContact.Address = billingContact?.Address;
                }
            }
        }

        /// <summary>Creates user from checkout.</summary>
        /// <param name="checkout">            The checkout.</param>
        /// <param name="createAccountForUser">True to create account for user.</param>
        /// <param name="contextProfileName">  Name of the context profile.</param>
        /// <returns>The new user from checkout.</returns>
        private static async Task<IUserModel> CreateUserFromCheckoutAsync(
            ICheckoutModel checkout,
            bool createAccountForUser,
            string? contextProfileName)
        {
            var user = RegistryLoaderWrapper.GetInstance<IUserModel>(contextProfileName);
            user.Active = true;
            user.CreatedDate = DateExtensions.GenDateTime;
            if (createAccountForUser)
            {
                var account = RegistryLoaderWrapper.GetInstance<IAccountModel>(contextProfileName);
                account.Active = true;
                account.CreatedDate = DateExtensions.GenDateTime;
                account.Name = checkout.Billing?.FirstName + checkout.Billing?.LastName;
                account.StatusID = 1;
                account.TypeID = 1;
                account.IsTaxable = true;
                var createAccountResponse = await Workflows.Accounts.CreateAsync(account, contextProfileName).ConfigureAwait(false);
                user.AccountID = createAccountResponse.Result;
            }
            user.UserName = checkout.WithUserInfo?.ExternalUserID ?? checkout.WithUserInfo?.UserName;
            // ReSharper disable PossibleInvalidOperationException
            var status = RegistryLoaderWrapper.GetInstance<IStatusModel>(contextProfileName);
            status.CustomKey = status.Name = status.DisplayName = "Registered";
            user.StatusID = await Workflows.UserStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: null,
                    byName: null,
                    byDisplayName: null,
                    model: status,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var type = RegistryLoaderWrapper.GetInstance<ITypeModel>(contextProfileName);
            type.CustomKey = type.Name = type.DisplayName = "Customer";
            user.TypeID = await Workflows.UserTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: null,
                    byName: null,
                    byDisplayName: null,
                    model: type,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            // ReSharper restore PossibleInvalidOperationException
            user.Contact = checkout.Billing;
            user.BillingAddress = checkout.Billing?.Address;
            user.OverridePassword = checkout.WithUserInfo?.Password;
            var createResponse = await Workflows.Users.CreateAsync(user, contextProfileName).ConfigureAwait(false);
            return (await Workflows.Users.GetAsync(createResponse.Result, contextProfileName).ConfigureAwait(false))!;
        }

        /// <summary>Use internal wallet.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private static async Task<CEFActionResponse> UseInternalWalletAsync(
            ICheckoutModel checkout,
            int? userID,
            string? contextProfileName)
        {
            if (Contract.CheckInvalidID(userID))
            {
                return CEFAR.FailingCEFAR(
                    "ERROR! A wallet entry was set to be used but the current user ID could not be determined.");
            }
            // Apply the card info from the wallet to the checkout model
            // ReSharper disable once PossibleInvalidOperationException
            var wallet = await Workflows.Wallets.GetDecryptedWalletAsync(
                    userID!.Value,
                    checkout.PayByWalletEntry!.WalletID!.Value,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!wallet.ActionSucceeded)
            {
                return wallet;
            }
            if (Contract.CheckAllValidKeys(
                    wallet.Result!.AccountNumber,
                    wallet.Result.RoutingNumber,
                    wallet.Result.BankName))
            {
                checkout.PayByECheck ??= new CheckoutPayByECheck();
                checkout.PayByECheck.AccountNumber = wallet.Result.AccountNumber;
                checkout.PayByECheck.RoutingNumber = wallet.Result.RoutingNumber;
                checkout.PayByECheck.BankName = wallet.Result.BankName;
            }
            if (Contract.CheckAllValidKeys(
                    wallet.Result.CreditCardNumber,
                    wallet.Result.CardType,
                    wallet.Result.CardHolderName))
            {
                checkout.PayByCreditCard ??= new CheckoutPayByCreditCard();
                checkout.PayByCreditCard.CardHolderName = wallet.Result.CardHolderName;
                checkout.PayByCreditCard.CardNumber = wallet.Result.CreditCardNumber;
                checkout.PayByCreditCard.ExpirationMonth = wallet.Result.ExpirationMonth;
                checkout.PayByCreditCard.ExpirationYear = wallet.Result.ExpirationYear;
                checkout.PayByCreditCard.CardType = wallet.Result.CardType;
                checkout.PayByCreditCard.CVV = checkout.PayByWalletEntry.WalletCVV;
            }
            checkout.PayByWalletEntry.WalletToken = wallet.Result.Token;
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Adds the address to book.</summary>
        /// <param name="contact">           The contact.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="isBilling">         True if this CheckoutProviderBase is billing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AddAddressToBookAsync(
            IBaseModel contact,
            int accountID,
            bool isBilling,
            string? contextProfileName)
        {
            try
            {
                var model = RegistryLoaderWrapper.GetInstance<IAccountContactModel>(contextProfileName);
                model.Active = true;
                model.CustomKey = contact.CustomKey;
                // TODO: Make these set by a value chosen by the user instead of always doing it
                model.IsBilling = isBilling;
                model.IsPrimary = !isBilling;
                model.TransmittedToERP = false;
                model.MasterID = model.MasterID = accountID;
                var slave = (IContactModel)contact.DeepCopy();
                WipeIDsFromContact(slave);
                model.Slave = slave;
                await Workflows.AddressBooks.CreateAddressInBookAsync(model, null, contextProfileName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(CheckoutProviderBase)}.{nameof(AddAddressToBookAsync)}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }
    }
}
