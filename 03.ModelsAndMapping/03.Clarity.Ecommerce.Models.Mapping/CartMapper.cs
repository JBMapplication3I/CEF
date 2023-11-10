// <copyright file="CartMapper.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart mapper class</summary>
namespace Clarity.Ecommerce.Mapper
{
    using System.Linq;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;

    /// <summary>A cart mapper.</summary>
    public static class CartMapper
    {
        /// <summary>An ICart extension method that session map.</summary>
        /// <param name="x">                 The cart to act on.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An ICartModel.</returns>
        public static ICartModel? SessionMap(this ICart? x, string? contextProfileName)
        {
            if (x is null)
            {
                return null;
            }
            var model = RegistryLoaderWrapper.GetInstance<ICartModel>(contextProfileName);
            PullCommonProperties(x, model, false, contextProfileName);
            model.SessionID = x.SessionID;
            model.Totals = new CartTotals
            {
                SubTotal = model.SalesItems!.Sum(si => si.ExtendedPrice),
                Tax = x.SubtotalTaxes,
                Fees = x.SubtotalFees,
                Handling = x.SubtotalHandling,
                Shipping = x.Shipment is not null
                    ? x.Shipment.PublishedRate ?? x.SubtotalShipping
                    : x.RateQuotes!.Any(y => y.Active && y.Selected)
                        ? x.RateQuotes!
                            .Where(y => y.Active)
                            .OrderByDescending(y => y.CreatedDate)
                            .First(y => y.Selected)
                            .Rate ?? x.SubtotalShipping
                        : x.SubtotalShipping,
                Discounts = x.Discounts!.Where(y => y.Active).Sum(y => y.DiscountTotal)
                    + x.SalesItems!
                       .Where(y => y.Discounts!.Any(z => z.Active))
                       .Sum(y => y.Discounts!.Where(z => z.Active).Sum(z => z.DiscountTotal)),
            };
            model.SubtotalDiscountsModifier = x.SubtotalDiscountsModifier;
            model.SubtotalDiscountsModifierMode = x.SubtotalDiscountsModifierMode;
            model.SubtotalTaxesModifier = x.SubtotalTaxesModifier;
            model.SubtotalTaxesModifierMode = x.SubtotalTaxesModifierMode;
            model.SubtotalHandlingModifier = x.SubtotalHandlingModifier;
            model.SubtotalHandlingModifierMode = x.SubtotalHandlingModifierMode;
            model.SubtotalFeesModifier = x.SubtotalFeesModifier;
            model.SubtotalFeesModifierMode = x.SubtotalFeesModifierMode;
            model.SubtotalShippingModifier = x.SubtotalShippingModifier;
            model.SubtotalShippingModifierMode = x.SubtotalShippingModifierMode;
            model.RequestedShipDate = x.RequestedShipDate;
            // Addresses/Contacts
            model.ShippingContactID = x.ShippingContactID;
            model.ShippingContact = x.ShippingContact.CreateContactModelFromEntityFull(contextProfileName);
            model.BillingContactID = x.BillingContactID;
            model.BillingContact = x.BillingContact.CreateContactModelFromEntityFull(contextProfileName);
            model.ShippingDetail = null;
            model.ShipmentID = x.ShipmentID;
            if (x.Shipment is not null)
            {
                model.Shipment = x.Shipment.CreateShipmentModelFromEntityLite(contextProfileName);
                model.ShipmentKey = x.Shipment.CustomKey;
            }
            // Associated Objects
            model.Discounts = x.Discounts!.Where(y => y.Active).Select(y => y.CreateAppliedCartDiscountModelFromEntityList(contextProfileName)).ToList()!;
            model.Notes = x.Notes!.Where(y => y.Active).Select(y => y.CreateNoteModelFromEntityList(contextProfileName)).ToList()!;
            model.Contacts = x.Contacts!.Where(y => y.Active).Select(y => y.CreateCartContactModelFromEntityLite(contextProfileName)).ToList()!;
            model.RateQuotes = x.RateQuotes!
                .AsQueryable()
                .FilterByActive(true)
                .FilterByModifiedSince(DateExtensions.GenDateTime.Date.AddDays(-1))
                .OrderBy(y => y.Rate ?? 0m)
                .Select(y => ModelMapperForRateQuote.MapRateQuoteModelFromEntityLite(y, contextProfileName))
                .ToList()!;
            return model;
        }

        /// <summary>An ICart extension method that static map.</summary>
        /// <param name="x">                 The cart to act on.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An ICartModel.</returns>
        public static ICartModel? StaticMap(this ICart? x, string? contextProfileName)
        {
            if (x is null)
            {
                return null;
            }
            var model = RegistryLoaderWrapper.GetInstance<ICartModel>(contextProfileName);
            PullCommonProperties(x, model, true, contextProfileName);
            return model;
        }

        // ReSharper disable once FunctionComplexityOverflow
        private static void PullCommonProperties(
            ICart x,
            ICartModel model,
            bool isStatic,
            string? contextProfileName)
        {
            // Base
            model.ID = x.ID;
            model.CustomKey = x.SessionID?.ToString();
            model.Active = x.Active;
            model.CreatedDate = x.CreatedDate;
            model.UpdatedDate = x.UpdatedDate;
            model.SerializableAttributes = x.SerializableAttributes;
            // Related Objects
            model.UserID = x.UserID;
            model.AccountID = x.AccountID;
            model.StoreID = x.StoreID;
            model.TypeID = x.TypeID;
            model.StatusID = x.StatusID;
            model.StateID = x.StateID;
            model.ShippingSameAsBilling = x.ShippingSameAsBilling ?? false;
            model.ShippingContactID = x.ShippingContactID;
            if (x.User is not null)
            {
                model.UserKey = x.User.CustomKey;
                model.UserUserName = x.User.UserName;
                if (x.User.Contact is not null)
                {
                    model.UserContactEmail = x.User.Contact.Email1;
                    model.UserContactFirstName = x.User.Contact.FirstName;
                    model.UserContactLastName = x.User.Contact.LastName;
                }
            }
            if (x.Account is not null)
            {
                model.AccountKey = x.Account.CustomKey;
                model.AccountName = x.Account.Name;
            }
            if (x.Store is not null)
            {
                model.Store = x.Store.CreateStoreModelFromEntityLite(contextProfileName);
                model.StoreKey = x.Store.CustomKey;
                model.StoreName = x.Store.Name;
            }
            if (x.Type is not null)
            {
                model.Type = x.Type.CreateCartTypeModelFromEntityLite(contextProfileName);
                model.TypeKey = x.Type.CustomKey;
                model.TypeName = x.Type.Name;
            }
            if (x.Status is not null)
            {
                model.Status = x.Status.CreateCartStatusModelFromEntityLite(contextProfileName);
                model.StatusKey = x.Status.CustomKey;
                model.StatusName = x.Status.Name;
            }
            if (x.State is not null)
            {
                model.State = x.State.CreateCartStateModelFromEntityLite(contextProfileName);
                model.StateKey = x.State.CustomKey;
                model.StateName = x.State.Name;
            }
            // Line Items
            model.SalesItems = x.SalesItems!
                .Where(y => y.Active)
                .Select(ci => isStatic
                    ? PullStaticCartItem(ci, contextProfileName)!
                    : PullSessionCartItem(ci, model.TypeName!, contextProfileName)!)
                .ToList();
            model.ItemQuantity = x.SalesItems!
                .Where(y => y.Active)
                .Select(y => y.Quantity + (y.QuantityBackOrdered ?? 0m) + (y.QuantityPreSold ?? 0m))
                .DefaultIfEmpty(0m)
                .Sum();
        }

        private static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? PullSessionCartItem(
            this ICartItem? x,
            string cartType,
            string? contextProfileName)
        {
            if (x is null)
            {
                return null;
            }
            var model = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(contextProfileName);
            PullCommonItemProperties(x, model);
            model.Quantity = x.Quantity;
            model.QuantityBackOrdered = x.QuantityBackOrdered ?? 0m;
            model.QuantityPreSold = x.QuantityPreSold ?? 0m;
            if (cartType == "Quote Cart" || model.ProductTypeKey == "Custom Quote Item")
            {
                model.UnitSoldPrice = x.UnitSoldPrice;
            }
            model.UnitSoldPriceModifier = x.UnitSoldPriceModifier ?? 0m;
            model.UnitSoldPriceModifierMode = x.UnitSoldPriceModifierMode ?? (int)Enums.TotalsModifierModes.Add;
            model.Discounts = x.Discounts!
                .Where(y => y.Active)
                .Select(y => y.CreateAppliedCartItemDiscountModelFromEntityList(contextProfileName))
                .ToList()!;
            model.Targets = x.Targets!
                .Where(y => y.Active)
                .Select(y => y.CreateSalesItemTargetBaseModelFromEntity(contextProfileName))
                .ToList();
            return model;
        }

        private static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? PullStaticCartItem(
            this ICartItem? x,
            string? contextProfileName)
        {
            if (x is null)
            {
                return null;
            }
            var model = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(contextProfileName);
            PullCommonItemProperties(x, model);
            model.Quantity = x.Quantity;
            model.QuantityBackOrdered = 0m;
            model.QuantityPreSold = 0m;
            return model;
        }

        // ReSharper disable once FunctionComplexityOverflow
        private static void PullCommonItemProperties(ICartItem x, ISalesItemBaseModel model)
        {
            // Base
            model.ID = x.ID;
            model.CustomKey = x.CustomKey;
            model.Active = x.Active;
            model.CreatedDate = x.CreatedDate;
            model.UpdatedDate = x.UpdatedDate;
            model.SerializableAttributes = x.SerializableAttributes;
            // Overridden data
            model.Name = x.Name;
            model.Description = x.Description;
            model.Sku = x.Sku;
            // Cart Items
            model.ForceUniqueLineItemKey = x.ForceUniqueLineItemKey;
            // Related Items
            model.MasterID = x.MasterID;
            model.UserID = x.UserID;
            model.ProductID = x.ProductID;
            if (x.User is not null)
            {
                model.UserKey = x.User.CustomKey;
                model.UserUserName = x.User.UserName;
            }
            if (x.Product is not null)
            {
                model.ProductKey = x.Product.CustomKey;
                model.ProductName = x.Product.Name;
                model.ProductDescription = x.Product.Description;
                model.ProductShortDescription = x.Product.ShortDescription;
                model.ProductSeoUrl = x.Product.SeoUrl;
                model.ProductUnitOfMeasure = x.Product.UnitOfMeasure;
                model.UnitOfMeasure = x.UnitOfMeasure ?? x.Product.UnitOfMeasure;
                model.ProductRequiresRoles = x.Product.RequiresRoles;
                model.ProductRequiresRolesAlt = x.Product.RequiresRolesAlt;
                model.ProductTypeID = x.Product.TypeID;
                model.ProductTypeKey = x.Product.Type?.CustomKey;
                model.ProductNothingToShip = x.Product.NothingToShip;
                model.ProductDropShipOnly = x.Product.DropShipOnly;
                model.ProductMaximumPurchaseQuantity = x.Product.MaximumPurchaseQuantity;
                model.ProductMaximumPurchaseQuantityIfPastPurchased = x.Product.MaximumPurchaseQuantityIfPastPurchased;
                model.ProductMinimumPurchaseQuantity = x.Product.MinimumPurchaseQuantity;
                model.ProductMinimumPurchaseQuantityIfPastPurchased = x.Product.MinimumPurchaseQuantityIfPastPurchased;
                model.ProductMaximumBackOrderPurchaseQuantity = x.Product.MaximumBackOrderPurchaseQuantity;
                model.ProductMaximumBackOrderPurchaseQuantityIfPastPurchased = x.Product.MaximumBackOrderPurchaseQuantityIfPastPurchased;
                model.ProductMaximumBackOrderPurchaseQuantityGlobal = x.Product.MaximumBackOrderPurchaseQuantityGlobal;
                model.ProductMaximumPrePurchaseQuantity = x.Product.MaximumPrePurchaseQuantity;
                model.ProductMaximumPrePurchaseQuantityIfPastPurchased = x.Product.MaximumPrePurchaseQuantityIfPastPurchased;
                model.ProductMaximumPrePurchaseQuantityGlobal = x.Product.MaximumPrePurchaseQuantityGlobal;
                model.ProductIsDiscontinued = x.Product.IsDiscontinued;
                model.ProductIsUnlimitedStock = x.Product.IsUnlimitedStock;
                model.ProductAllowBackOrder = x.Product.AllowBackOrder;
                model.ProductAllowPreSale = x.Product.AllowPreSale;
                model.ProductIsEligibleForReturn = x.Product.IsEligibleForReturn;
                model.ProductRestockingFeePercent = x.Product.RestockingFeePercent;
                model.ProductRestockingFeeAmount = x.Product.RestockingFeeAmount;
                model.ProductIsTaxable = x.Product.IsTaxable;
                model.ProductTaxCode = x.Product.TaxCode;
                model.ProductSerializableAttributes = x.Product.JsonAttributes.DeserializeAttributesDictionary();
                model.ProductPrimaryImage = x.Product.Images!
                    .Where(y => y.Active)
                    .OrderByDescending(y => y.IsPrimary)
                    .ThenBy(y => y.OriginalWidth)
                    .ThenBy(y => y.OriginalHeight)
                    .Take(1)
                    .Select(y => y.ThumbnailFileName ?? y.OriginalFileName)
                    .FirstOrDefault();
            }
            else
            {
                model.UnitOfMeasure = x.UnitOfMeasure;
            }
        }
    }
}
