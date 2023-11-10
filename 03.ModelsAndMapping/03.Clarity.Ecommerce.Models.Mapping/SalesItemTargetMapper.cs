// <copyright file="SalesItemTargetMapper.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item target mapper class</summary>
namespace Clarity.Ecommerce.Mapper
{
    using Interfaces.DataModel;
    using Interfaces.Models;

    public static partial class BaseModelMapper
    {
        /// <summary>An ISalesItemTargetBase extension method that creates sales item target base model from entity.</summary>
        /// <param name="entity">            The entity to act on.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new sales item target base from entity.</returns>
        public static ISalesItemTargetBaseModel CreateSalesItemTargetBaseModelFromEntity(
            this ISalesItemTargetBase entity,
            string? contextProfileName)
        {
            var model = RegistryLoaderWrapper.GetInstance<ISalesItemTargetBaseModel>(contextProfileName);
            // Base Properties
            model.ID = entity.ID;
            model.CustomKey = entity.CustomKey;
            model.Active = entity.Active;
            model.CreatedDate = entity.CreatedDate;
            model.UpdatedDate = entity.UpdatedDate;
            model.Hash = entity.Hash;
            model.SerializableAttributes = entity.SerializableAttributes;
            // SalesItemTarget Properties
            model.Quantity = entity.Quantity;
            model.NothingToShip = entity.NothingToShip;
            // Related Objects
            model.MasterID = entity.MasterID;
            model.TypeID = entity.TypeID;
            if (entity.Type != null)
            {
                model.TypeKey = entity.Type.CustomKey;
                model.TypeName = entity.Type.Name;
                model.Type = entity.Type.CreateSalesItemTargetTypeModelFromEntityLite(contextProfileName);
            }
            model.DestinationContactID = entity.DestinationContactID;
            if (entity.DestinationContact != null)
            {
                model.DestinationContactKey = entity.DestinationContact.CustomKey;
                model.DestinationContact = entity.DestinationContact.CreateContactModelFromEntityLite(contextProfileName);
            }
            ////if (JSConfigs.CEFConfigDictionary.InventoryAdvancedEnabled)
            ////{
            ////    model.OriginProductInventoryLocationSectionID = entity.OriginProductInventoryLocationSectionID;
            ////    if (entity.OriginProductInventoryLocationSection != null)
            ////    {
            ////        model.OriginProductInventoryLocationSectionKey = entity.OriginProductInventoryLocationSection.CustomKey;
            ////        model.OriginProductInventoryLocationSection = entity.OriginProductInventoryLocationSection
            ////            .CreateProductInventoryLocationSectionModelFromEntityLite(contextProfileName);
            ////    }
            ////}
            ////if (JSConfigs.CEFConfigDictionary.StoresEnabled)
            ////{
            ////    model.OriginStoreProductID = entity.OriginStoreProductID;
            ////    if (entity.OriginStoreProduct != null)
            ////    {
            ////        model.OriginStoreProductKey = entity.OriginStoreProduct.CustomKey;
            ////        model.OriginStoreProduct = entity.OriginStoreProduct.CreateStoreProductModelFromEntityLite(contextProfileName);
            ////    }
            ////}
            ////if (JSConfigs.CEFConfigDictionary.VendorsEnabled)
            ////{
            ////    model.OriginVendorProductID = entity.OriginVendorProductID;
            ////    if (entity.OriginVendorProduct != null)
            ////    {
            ////        model.OriginVendorProductKey = entity.OriginVendorProduct.CustomKey;
            ////        model.OriginVendorProduct = entity.OriginVendorProduct.CreateVendorProductModelFromEntityLite(contextProfileName);
            ////    }
            ////}
            model.SelectedRateQuoteID = entity.SelectedRateQuoteID;
            // ReSharper disable once InvertIf
            if (entity.SelectedRateQuote != null)
            {
                model.SelectedRateQuoteKey = entity.SelectedRateQuote.CustomKey;
                model.SelectedRateQuote = entity.SelectedRateQuote.CreateRateQuoteModelFromEntityLite(contextProfileName);
            }
            return model;
        }
    }
}
