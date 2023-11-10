// <copyright file="TypeDeterminer.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the type determiner class</summary>
// ReSharper disable InconsistentNaming, MemberCanBePrivate.Global, MissingXmlDoc, UnusedAutoPropertyAccessor.Global
// ReSharper disable CyclomaticComplexity, FunctionComplexityOverflow, UnusedMember.Global
namespace CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Clarity.Ecommerce.DataModel;
    using Clarity.Ecommerce.Interfaces.DataModel;
    using Clarity.Ecommerce.Interfaces.Models;
    using Clarity.Ecommerce.Models;

    /// <summary>A type determiner.</summary>
    public class TypeDeterminer
    {
        /// <summary>Initializes a new instance of the <see cref="TypeDeterminer" /> class.</summary>
        /// <param name="theType">Type of the.</param>
        public TypeDeterminer(Type theType)
        {
            IsExcludedFromT4 = theType.GetInterfaces().Any(x => x.Name == nameof(IAmExcludedFromT4Generation));
            if (IsExcludedFromT4)
            {
                return;
            }
            var obsolete = theType.GetCustomAttribute<ObsoleteAttribute>();
            if (obsolete == null)
            {
                var attrs = theType.GetCustomAttributes().Where(x => x.GetType().Name.Contains("Obsolete")).ToList();
                if (attrs.Count > 0)
                {
                    obsolete = attrs[0] as ObsoleteAttribute;
                }
            }
            if (obsolete != null)
            {
                IsDeprecated = true;
                DeprecatedMessage = obsolete.Message;
            }

            IsIBase = theType.GetInterfaces().Any(x => x.Name is nameof(IBase) or nameof(IBaseModel));
            IsINameableBase = theType.GetInterfaces().Any(x => x.Name is nameof(INameableBase) or nameof(INameableBaseModel));

            IsIDisplayableBase = theType.GetInterfaces().Any(x => x.Name is nameof(IDisplayableBase) or nameof(IDisplayableBaseModel));

            IsIStatusableBase = theType.GetInterfaces().Any(x => x.Name is nameof(IStatusableBase) or nameof(IStatusableBaseModel));
            IsIHaveAStatusBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveAStatusBase) or nameof(IHaveAStatusBaseModel));
            IsIHaveAStatusBaseSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IHaveAStatusBaseSearchModel));
            if (IsIHaveAStatusBase)
            {
                RelatedStatusType = theType.GetInterfaces().First(x => x.Name == typeof(IHaveAStatusBase<>).Name || x.Name == typeof(IHaveAStatusBaseModel<>).Name).GetGenericArguments().First();
            }

            IsITypableBase = theType.GetInterfaces().Any(x => x.Name is nameof(ITypableBase) or nameof(ITypableBaseModel));
            IsIHaveATypeBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveATypeBase) or nameof(IHaveATypeBaseModel));
            if (IsIHaveATypeBase)
            {
                RelatedTypeType = theType.GetInterfaces().First(x => x.Name == typeof(IHaveATypeBase<>).Name || x.Name == typeof(IHaveATypeBaseModel<>).Name).GetGenericArguments().First();
            }
            IsIHaveATypeBaseSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IHaveATypeBaseSearchModel));

            IsIStateableBase = theType.GetInterfaces().Any(x => x.Name is nameof(IStateableBase) or nameof(IStateableBaseModel));
            IsIHaveAStateBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveAStateBase) or nameof(IHaveAStateBaseModel));
            if (IsIHaveAStateBase)
            {
                RelatedStateType = theType.GetInterfaces().First(x => x.Name == typeof(IHaveAStateBase<>).Name || x.Name == typeof(IHaveAStateBaseModel<>).Name).GetGenericArguments().First();
            }
            IsIHaveAStateBaseSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IHaveAStateBaseSearchModel));

            IsIHaveCurrenciesBase = theType.GetInterfaces().Any(x => x.Name == typeof(IHaveCurrenciesBase<>).Name || x.Name == typeof(IHaveCurrenciesBaseModel<>).Name);
            if (IsIHaveCurrenciesBase)
            {
                IHaveCurrenciesTType = theType.GetInterfaces().First(x => x.Name == typeof(IHaveCurrenciesBase<>).Name || x.Name == typeof(IHaveCurrenciesBaseModel<>).Name).GetGenericArguments().First();
            }

            IsIHaveLanguagesBase = theType.GetInterfaces().Any(x => x.Name == typeof(IHaveLanguagesBase<>).Name || x.Name == typeof(IHaveLanguagesBaseModel<>).Name);
            if (IsIHaveLanguagesBase)
            {
                IHaveLanguagesTType = theType.GetInterfaces().First(x => x.Name == typeof(IHaveLanguagesBase<>).Name || x.Name == typeof(IHaveLanguagesBaseModel<>).Name).GetGenericArguments().First();
            }

            IsIHaveAppliedDiscountsBase = theType.GetInterfaces().Any(x => x.Name == typeof(IHaveAppliedDiscountsBase<,>).Name || x.Name == typeof(IHaveAppliedDiscountsBaseModel<>).Name);
            if (IsIHaveAppliedDiscountsBase)
            {
                IHaveAppliedDiscountsTType = theType.GetInterfaces().FirstOrDefault(x => x.Name == typeof(IHaveAppliedDiscountsBase<,>).Name)?.GetGenericArguments().Last()
                    ?? theType.GetInterfaces().First(x => x.Name == typeof(IHaveAppliedDiscountsBaseModel<>).Name).GetGenericArguments().First();
            }

            IsIHaveContactsBase = theType.GetInterfaces().Any(x => x.Name == typeof(IHaveContactsBase<,>).Name || x.Name == typeof(IHaveContactsBaseModel<>).Name);
            if (IsIHaveContactsBase)
            {
                IHaveContactsTType = theType.GetInterfaces().FirstOrDefault(x => x.Name == typeof(IHaveContactsBase<,>).Name)?.GetGenericArguments().Last()
                    ?? theType.GetInterfaces().First(x => x.Name == typeof(IHaveContactsBaseModel<>).Name).GetGenericArguments().First();
            }

            IsIHaveAContactBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveAContactBase) or nameof(IHaveAContactBaseModel));
            IsIHaveANullableContactBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveANullableContactBase) or nameof(IHaveANullableContactBaseModel));
            IsIHaveAContactBaseSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IHaveAContactBaseSearchModel));

            IsIHaveNotesBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveNotesBase) or nameof(IHaveNotesBaseModel));
            IsIObjectNoteBase = theType.GetInterfaces().Any(x => x.Name == typeof(IObjectNoteBase<>).Name /*|| x.Name == typeof(IObjectNoteBaseModel<>).Name*/);

            IsIAppliedDiscountBase = theType.GetInterfaces().Any(x => x.Name == typeof(IAppliedDiscountBase<,>).Name || x.Name == nameof(IAppliedDiscountBaseModel));
            IsIAmADiscountFilterRelationshipTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmADiscountFilterRelationshipTable<>).Name);

            IsIHaveAParentBase = theType.GetInterfaces().Any(x => x.Name == typeof(IHaveAParentBase<>).Name || x.Name == typeof(IHaveAParentBaseModel<>).Name);

            IsIHaveJsonAttributesBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveJsonAttributesBase) or nameof(IHaveJsonAttributesBaseModel));

            IsIHaveAdCounters = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveAdCounters) or nameof(IHaveAdCountersModel));

            IsIHaveRequiresRolesBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveRequiresRolesBase) or nameof(IHaveRequiresRolesBaseModel));

            IsISalesEventBase = theType.GetInterfaces().Any(x => x.Name == typeof(ISalesEventBase<,>).Name || x.Name == nameof(ISalesEventBaseModel));

            IsISalesCollectionBaseT = theType.GetInterfaces().Any(x => x.Name == typeof(ISalesCollectionBase<,,,,,,,,,>).Name || x.Name is nameof(ISalesCollectionBase) or nameof(ISalesCollectionBaseModel) or nameof(ISalesCollectionBaseModel));
            IsISalesCollectionBase = theType.GetInterfaces().Any(x => x.Name is nameof(ISalesCollectionBase) or nameof(ISalesCollectionBaseModel));

            IsISalesItemBaseTT = theType.GetInterfaces().Any(x => x.Name == typeof(ISalesItemBase<,,>).Name || x.Name == typeof(ISalesItemBaseModel<>).Name);
            IsISalesItemBaseT = theType.GetInterfaces().Any(x => x.Name == typeof(ISalesItemBase<,,>).Name || x.Name == typeof(ISalesItemBaseModel<>).Name);
            IsISalesItemBase = theType.GetInterfaces().Any(x => x.Name == typeof(ISalesItemBase<,,>).Name || x.Name == typeof(ISalesItemBaseModel<>).Name);
            if (IsISalesItemBase)
            {
                SalesItemBaseDiscountType = theType.GetInterfaces().First(x => x.Name == typeof(ISalesItemBase<,,>).Name || x.Name == typeof(ISalesItemBaseModel<>).Name).GetGenericArguments().First();
            }

            IsISalesItemTargetBase = theType.GetInterfaces().Any(x => x.Name is nameof(ISalesItemTargetBase) or nameof(ISalesItemTargetBaseModel));

            IsIAmFilterableByAccount = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByAccount) or nameof(IAmFilterableByAccountModel));
            IsIAmFilterableByAccountSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IAmFilterableByAccountSearchModel));
            IsIAmFilterableByNullableAccount = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByNullableAccount) or nameof(IAmFilterableByNullableAccountModel));
            IsIAmFilterableByAccountT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmFilterableByAccount<>).Name || x.Name == typeof(IAmFilterableByAccountModel<>).Name);
            IAmFilterableByAccountTType = IsIAmFilterableByAccountT ? theType.GetInterfaces().First(x => x.Name == typeof(IAmFilterableByAccount<>).Name || x.Name == typeof(IAmFilterableByAccountModel<>).Name).GetGenericArguments()[0] : null;

            IsIAmFilterableByBrand = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByBrand) or nameof(IAmFilterableByBrandModel));
            IsIAmFilterableByBrandSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IAmFilterableByBrandSearchModel));
            IsIAmFilterableByNullableBrand = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByNullableBrand) or nameof(IAmFilterableByNullableBrandModel));
            IsIAmFilterableByBrandT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmFilterableByBrand<>).Name || x.Name == typeof(IAmFilterableByBrandModel<>).Name);
            IAmFilterableByBrandTType = IsIAmFilterableByBrandT ? theType.GetInterfaces().First(x => x.Name == typeof(IAmFilterableByBrand<>).Name || x.Name == typeof(IAmFilterableByBrandModel<>).Name).GetGenericArguments()[0] : null;

            IsIAmFilterableByCategory = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByCategory) or nameof(IAmFilterableByCategoryModel));
            IsIAmFilterableByCategorySearch = theType.GetInterfaces().Any(x => x.Name == nameof(IAmFilterableByCategorySearchModel));
            IsIAmFilterableByNullableCategory = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByNullableCategory) or nameof(IAmFilterableByNullableCategoryModel));
            IsIAmFilterableByCategoryT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmFilterableByCategory<>).Name || x.Name == typeof(IAmFilterableByCategoryModel<>).Name);
            IAmFilterableByCategoryTType = IsIAmFilterableByCategoryT ? theType.GetInterfaces().First(x => x.Name == typeof(IAmFilterableByCategory<>).Name || x.Name == typeof(IAmFilterableByCategoryModel<>).Name).GetGenericArguments()[0] : null;

            IsIAmFilterableByFranchise = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByFranchise) or nameof(IAmFilterableByFranchiseModel));
            IsIAmFilterableByFranchiseSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IAmFilterableByFranchiseSearchModel));
            IsIAmFilterableByNullableFranchise = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByNullableFranchise) or nameof(IAmFilterableByNullableFranchiseModel));
            IsIAmFilterableByFranchiseT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmFilterableByFranchise<>).Name || x.Name == typeof(IAmFilterableByFranchiseModel<>).Name);
            IAmFilterableByFranchiseTType = IsIAmFilterableByFranchiseT ? theType.GetInterfaces().First(x => x.Name == typeof(IAmFilterableByFranchise<>).Name || x.Name == typeof(IAmFilterableByFranchiseModel<>).Name).GetGenericArguments()[0] : null;

            IsIAmFilterableByManufacturer = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByManufacturer) or nameof(IAmFilterableByManufacturerModel));
            IsIAmFilterableByManufacturerSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IAmFilterableByManufacturerSearchModel));
            IsIAmFilterableByNullableManufacturer = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByNullableManufacturer) or nameof(IAmFilterableByNullableManufacturerModel));
            IsIAmFilterableByManufacturerT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmFilterableByManufacturer<>).Name || x.Name == typeof(IAmFilterableByManufacturerModel<>).Name);
            IAmFilterableByManufacturerTType = IsIAmFilterableByManufacturerT ? theType.GetInterfaces().First(x => x.Name == typeof(IAmFilterableByManufacturer<>).Name || x.Name == typeof(IAmFilterableByManufacturerModel<>).Name).GetGenericArguments()[0] : null;

            IsIAmFilterableByProduct = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByProduct) or nameof(IAmFilterableByProductModel));
            IsIAmFilterableByProductSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IAmFilterableByProductSearchModel));
            IsIAmFilterableByNullableProduct = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByNullableProduct) or nameof(IAmFilterableByNullableProductModel));
            IsIAmFilterableByProductT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmFilterableByProduct<>).Name || x.Name == typeof(IAmFilterableByProductModel<>).Name);
            IAmFilterableByProductTType = IsIAmFilterableByProductT ? theType.GetInterfaces().First(x => x.Name == typeof(IAmFilterableByProduct<>).Name || x.Name == typeof(IAmFilterableByProductModel<>).Name).GetGenericArguments()[0] : null;

            IsIAmFilterableByStore = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByStore) or nameof(IAmFilterableByStoreModel));
            IsIAmFilterableByStoreSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IAmFilterableByStoreSearchModel));
            IsIAmFilterableByNullableStore = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByNullableStore) or nameof(IAmFilterableByNullableStoreModel));
            IsIAmFilterableByStoreT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmFilterableByStore<>).Name || x.Name == typeof(IAmFilterableByStoreModel<>).Name);
            IAmFilterableByStoreTType = IsIAmFilterableByStoreT ? theType.GetInterfaces().First(x => x.Name == typeof(IAmFilterableByStore<>).Name || x.Name == typeof(IAmFilterableByStoreModel<>).Name).GetGenericArguments()[0] : null;

            IsIAmFilterableByUser = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByUser) or nameof(IAmFilterableByUserModel));
            IsIAmFilterableByUserSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IAmFilterableByUserSearchModel));
            IsIAmFilterableByNullableUser = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByNullableUser) or nameof(IAmFilterableByNullableUserModel));
            IsIAmFilterableByUserT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmFilterableByUser<>).Name || x.Name == typeof(IAmFilterableByUserModel<>).Name);
            IAmFilterableByUserTType = IsIAmFilterableByUserT ? theType.GetInterfaces().First(x => x.Name == typeof(IAmFilterableByUser<>).Name || x.Name == typeof(IAmFilterableByUserModel<>).Name).GetGenericArguments()[0] : null;

            IsIAmFilterableByVendor = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByVendor) or nameof(IAmFilterableByVendorModel));
            IsIAmFilterableByVendorSearch = theType.GetInterfaces().Any(x => x.Name == nameof(IAmFilterableByVendorSearchModel));
            IsIAmFilterableByNullableVendor = theType.GetInterfaces().Any(x => x.Name is nameof(IAmFilterableByNullableVendor) or nameof(IAmFilterableByNullableVendorModel));
            IsIAmFilterableByVendorT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmFilterableByVendor<>).Name || x.Name == typeof(IAmFilterableByVendorModel<>).Name);
            IAmFilterableByVendorTType = IsIAmFilterableByVendorT ? theType.GetInterfaces().First(x => x.Name == typeof(IAmFilterableByVendor<>).Name || x.Name == typeof(IAmFilterableByVendorModel<>).Name).GetGenericArguments()[0] : null;

            IsIHaveImagesBase = theType.GetInterfaces().Any(x => x.Name == typeof(IHaveImagesBase<,,>).Name || x.Name == typeof(IHaveImagesBaseModel<,>).Name);
            if (IsIHaveImagesBase)
            {
                var dbType = Array.Find(theType.GetInterfaces(), x => x.Name == typeof(IHaveImagesBase<,,>).Name);
                var modelType = Array.Find(theType.GetInterfaces(), x => x.Name == typeof(IHaveImagesBaseModel<,>).Name);
                if (dbType != null)
                {
                    IsIHaveImagesBaseImageType = dbType.GetGenericArguments()[1];
                    IsIHaveImagesBaseImageTypeType = dbType.GetGenericArguments()[2];
                    IsIHaveImagesBaseImageTypeTD = new(IsIHaveImagesBaseImageType);
                    IsIHaveImagesBaseImageTypeTypeTD = new(IsIHaveImagesBaseImageTypeType);
                }
                else if (modelType != null)
                {
                    IsIHaveImagesBaseImageType = modelType.GetGenericArguments()[0];
                    IsIHaveImagesBaseImageTypeType = modelType.GetGenericArguments()[1];
                    IsIHaveImagesBaseImageTypeTD = new(IsIHaveImagesBaseImageType);
                    IsIHaveImagesBaseImageTypeTypeTD = new(IsIHaveImagesBaseImageTypeType);
                }
            }
            IsIImageBase = theType.GetInterfaces().Any(x => x.Name == typeof(IImageBase<,>).Name || x.Name == typeof(IImageBaseModel<>).Name);

            IsIAmARelationshipTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmARelationshipTable<,>).Name || x.Name == typeof(IAmARelationshipTableBaseModel<>).Name);
            if (IsIAmARelationshipTable)
            {
                var dbType = Array.Find(theType.GetInterfaces(), x => x.Name == typeof(IAmARelationshipTable<,>).Name);
                var modelType = Array.Find(theType.GetInterfaces(), x => x.Name == typeof(IAmARelationshipTableBaseModel<>).Name);
                if (dbType != null)
                {
                    RelationshipPrimaryType = dbType.GetGenericArguments()[0];
                    RelationshipSecondaryType = dbType.GetGenericArguments()[1];
                    RelationshipPrimaryTypeTD = new(RelationshipPrimaryType);
                    RelationshipSecondaryTypeTD = new(RelationshipSecondaryType);
                }
                else if (modelType != null)
                {
                    RelationshipPrimaryType = null;
                    RelationshipSecondaryType = modelType.GetGenericArguments()[0];
                    RelationshipPrimaryTypeTD = null;
                    RelationshipSecondaryTypeTD = new(RelationshipSecondaryType);
                }
                IsIAmAContactRelationshipTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAContactRelationshipTable<,>).Name || x.Name == nameof(IAmAContactRelationshipTableModel));
                IsIHaveAContactBaseIsTheRelationship = IsIHaveAContactBase && (RelationshipPrimaryType == typeof(Contact) || RelationshipSecondaryType == typeof(Contact) || RelationshipSecondaryType == typeof(IContactModel) || RelationshipSecondaryType == typeof(ContactModel));
                IsIHaveAContactBaseSearchIsTheRelationship = IsIHaveAContactBaseSearch && (RelationshipPrimaryType == typeof(Contact) || RelationshipSecondaryType == typeof(Contact) || RelationshipSecondaryType == typeof(IContactModel) || RelationshipSecondaryType == typeof(ContactModel));

                IsIAmAnAccountRelationshipMasterTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAnAccountRelationshipTableWhereAccountIsTheMaster<>).Name || x.Name == typeof(IAmAnAccountRelationshipTableWhereAccountIsTheMasterModel<>).Name);
                IsIAmAnAccountRelationshipSlaveTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAnAccountRelationshipTableWhereAccountIsTheSlave<>).Name || x.Name == nameof(IAmAnAccountRelationshipTableWhereAccountIsTheSlaveModel));
                IsIAmFilterableByAccountIsTheRelationship = IsIAmAnAccountRelationshipMasterTable || IsIAmAnAccountRelationshipSlaveTable;
                IsIAmFilterableByAccountSearchIsTheRelationship = IsIAmFilterableByAccountSearch && (IsIAmAnAccountRelationshipMasterTable || IsIAmAnAccountRelationshipSlaveTable);

                IsIAmABrandRelationshipMasterTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmABrandRelationshipTableWhereBrandIsTheMaster<>).Name || x.Name == typeof(IAmABrandRelationshipTableWhereBrandIsTheMasterModel<>).Name);
                IsIAmABrandRelationshipSlaveTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmABrandRelationshipTableWhereBrandIsTheSlave<>).Name || x.Name == nameof(IAmABrandRelationshipTableWhereBrandIsTheSlaveModel));
                IsIAmFilterableByBrandIsTheRelationship = IsIAmABrandRelationshipMasterTable || IsIAmABrandRelationshipSlaveTable;
                IsIAmFilterableByBrandSearchIsTheRelationship = IsIAmFilterableByBrandSearch && (IsIAmABrandRelationshipMasterTable || IsIAmABrandRelationshipSlaveTable);

                IsIAmACategoryRelationshipMasterTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmACategoryRelationshipTableWhereCategoryIsTheMaster<>).Name || x.Name == typeof(IAmACategoryRelationshipTableWhereCategoryIsTheMasterModel<>).Name);
                IsIAmACategoryRelationshipSlaveTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmACategoryRelationshipTableWhereCategoryIsTheSlave<>).Name || x.Name == nameof(IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel));
                IsIAmFilterableByCategoryIsTheRelationship = IsIAmACategoryRelationshipMasterTable || IsIAmACategoryRelationshipSlaveTable;
                IsIAmFilterableByCategorySearchIsTheRelationship = IsIAmFilterableByCategorySearch && (IsIAmACategoryRelationshipMasterTable || IsIAmACategoryRelationshipSlaveTable);

                IsIAmAFranchiseRelationshipMasterTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<>).Name || x.Name == typeof(IAmAFranchiseRelationshipTableWhereFranchiseIsTheMasterModel<>).Name);
                IsIAmAFranchiseRelationshipSlaveTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlave<>).Name || x.Name == nameof(IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlaveModel));
                IsIAmFilterableByFranchiseIsTheRelationship = IsIAmAFranchiseRelationshipMasterTable || IsIAmAFranchiseRelationshipSlaveTable;
                IsIAmFilterableByFranchiseSearchIsTheRelationship = IsIAmFilterableByFranchiseSearch && (IsIAmAFranchiseRelationshipMasterTable || IsIAmAFranchiseRelationshipSlaveTable);

                IsIAmAManufacturerRelationshipMasterTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAManufacturerRelationshipTableWhereManufacturerIsTheMaster<>).Name || x.Name == typeof(IAmAManufacturerRelationshipTableWhereManufacturerIsTheMasterModel<>).Name);
                IsIAmAManufacturerRelationshipSlaveTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<>).Name || x.Name == nameof(IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlaveModel));
                IsIAmFilterableByManufacturerIsTheRelationship = IsIAmAManufacturerRelationshipMasterTable || IsIAmAManufacturerRelationshipSlaveTable;
                IsIAmFilterableByManufacturerSearchIsTheRelationship = IsIAmFilterableByManufacturerSearch && (IsIAmAManufacturerRelationshipMasterTable || IsIAmAManufacturerRelationshipSlaveTable);

                IsIAmAProductRelationshipMasterTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAProductRelationshipTableWhereProductIsTheMaster<>).Name || x.Name == typeof(IAmAProductRelationshipTableWhereProductIsTheMasterModel<>).Name);
                IsIAmAProductRelationshipSlaveTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAProductRelationshipTableWhereProductIsTheSlave<>).Name || x.Name == nameof(IAmAProductRelationshipTableWhereProductIsTheSlaveModel));
                IsIAmFilterableByProductIsTheRelationship = IsIAmAProductRelationshipMasterTable || IsIAmAProductRelationshipSlaveTable;
                IsIAmFilterableByProductSearchIsTheRelationship = IsIAmFilterableByProductSearch && (IsIAmAProductRelationshipMasterTable || IsIAmAProductRelationshipSlaveTable);

                IsIAmAStoreRelationshipMasterTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAStoreRelationshipTableWhereStoreIsTheMaster<>).Name || x.Name == typeof(IAmAStoreRelationshipTableWhereStoreIsTheMasterModel<>).Name);
                IsIAmAStoreRelationshipSlaveTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAStoreRelationshipTableWhereStoreIsTheSlave<>).Name || x.Name == nameof(IAmAStoreRelationshipTableWhereStoreIsTheSlaveModel));
                IsIAmFilterableByStoreIsTheRelationship = IsIAmAStoreRelationshipMasterTable || IsIAmAStoreRelationshipSlaveTable;
                IsIAmFilterableByStoreSearchIsTheRelationship = IsIAmFilterableByStoreSearch && (IsIAmAStoreRelationshipMasterTable || IsIAmAStoreRelationshipSlaveTable);

                IsIAmAUserRelationshipMasterTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAUserRelationshipTableWhereUserIsTheMaster<>).Name || x.Name == typeof(IAmAUserRelationshipTableWhereUserIsTheMasterModel<>).Name);
                IsIAmAUserRelationshipSlaveTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAUserRelationshipTableWhereUserIsTheSlave<>).Name || x.Name == nameof(IAmAUserRelationshipTableWhereUserIsTheSlaveModel));
                IsIAmFilterableByUserIsTheRelationship = IsIAmAUserRelationshipMasterTable || IsIAmAUserRelationshipSlaveTable;
                IsIAmFilterableByUserSearchIsTheRelationship = IsIAmFilterableByUserSearch && (IsIAmAUserRelationshipMasterTable || IsIAmAUserRelationshipSlaveTable);

                IsIAmAVendorRelationshipMasterTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAVendorRelationshipTableWhereVendorIsTheMaster<>).Name || x.Name == typeof(IAmAVendorRelationshipTableWhereVendorIsTheMasterModel<>).Name);
                IsIAmAVendorRelationshipSlaveTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAVendorRelationshipTableWhereVendorIsTheSlave<>).Name || x.Name == nameof(IAmAVendorRelationshipTableWhereVendorIsTheSlaveModel));
                IsIAmFilterableByVendorIsTheRelationship = IsIAmAVendorRelationshipMasterTable || IsIAmAVendorRelationshipSlaveTable;
                IsIAmFilterableByVendorSearchIsTheRelationship = IsIAmFilterableByVendorSearch && (IsIAmAVendorRelationshipMasterTable || IsIAmAVendorRelationshipSlaveTable);

                IsIAmAStoredFileRelationshipTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAStoredFileRelationshipTable<>).Name || x.Name == nameof(IAmAStoredFileRelationshipTableModel));
                IsIAmAFavoriteRelationshipTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmAFavoriteRelationshipTable<>).Name /* || x.Name == typeof(IAmAFavoriteRelationshipTableModel<>).Name*/);
                IsIAmACurrencyRelationshipTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmACurrencyRelationshipTableBase<>).Name || x.Name == nameof(IAmACurrencyRelationshipTableBaseModel));
                IsIAmALanguageRelationshipTable = theType.GetInterfaces().Any(x => x.Name == typeof(IAmALanguageRelationshipTableBase<>).Name || x.Name == nameof(IAmALanguageRelationshipTableBaseModel));
            }
            IsIHaveReviewsBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveReviewsBase) or nameof(IHaveReviewsBaseModel));
            IsIHaveSeoBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveSeoBase) or nameof(IHaveSeoBaseModel));
            IsIHaveOrderMinimumsBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveOrderMinimumsBase) or nameof(IHaveOrderMinimumsBaseModel));
            IsIHaveFreeShippingMinimumsBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveFreeShippingMinimumsBase) or nameof(IHaveFreeShippingMinimumsBaseModel));
            IsIHaveDimensions = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveDimensions) or nameof(IHaveDimensionsModel));
            IsIHaveNullableDimensions = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveDimensions) or nameof(IHaveNullableDimensionsModel));
            IsIAmALitelyTrackedEventBase = theType.GetInterfaces().Any(x => x.Name is nameof(IAmALitelyTrackedEventBase) or nameof(IAmALitelyTrackedEventBaseModel));
            IsIAmATrackedEventBase = theType.GetInterfaces().Any(x => x.Name is nameof(IAmATrackedEventBase) or nameof(IAmATrackedEventBaseModel));
            IsIAmATrackedEventBaseT = theType.GetInterfaces().Any(x => x.Name == typeof(IAmATrackedEventBase<>).Name || x.Name == typeof(IAmATrackedEventBaseModel<>).Name); // the T is a Status which is caught by the other interface
            IsIHaveStoredFilesBase = theType.GetInterfaces().Any(x => x.Name == typeof(IHaveStoredFilesBase<,>).Name || x.Name == typeof(IHaveStoredFilesBaseModel<>).Name);
            if (IsIHaveStoredFilesBase)
            {
                IsIHaveStoredFilesBaseFileType = theType.GetInterfaces().First(x => x.Name == typeof(IHaveStoredFilesBase<,>).Name || x.Name == typeof(IHaveStoredFilesBaseModel<>).Name).GetGenericArguments()[0];
            }
            IsIHaveAStoredFileBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveAStoredFileBase) or nameof(IHaveAStoredFileBaseModel));
            IsIHaveANullableStoredFileBase = theType.GetInterfaces().Any(x => x.Name is nameof(IHaveANullableStoredFileBase) or nameof(IHaveANullableStoredFileBaseModel));
            // Finally, initialize the results dictionary
            ForceOrDontResults = new();
            IsIProductPricePoint = theType.GetInterfaces().Any(x => x.Name == nameof(IProductPricePoint));
        }

        /// <summary>Gets a message describing the deprecated.</summary>
        /// <value>A message describing the deprecated.</value>
        public string? DeprecatedMessage { get; }

        /// <summary>Gets the force or dont results.</summary>
        /// <value>The force or dont results.</value>
        public Dictionary<string, ForceOrDontMapResults>? ForceOrDontResults { get; }

        /// <summary>Gets the type of the am filterable by account.</summary>
        /// <value>The type of the am filterable by account.</value>
        public Type? IAmFilterableByAccountTType { get; }

        /// <summary>Gets the type of the am filterable by brand t.</summary>
        /// <value>The type of the am filterable by brand t.</value>
        public Type? IAmFilterableByBrandTType { get; }

        /// <summary>Gets the type of the am filterable by franchise t.</summary>
        /// <value>The type of the am filterable by franchise t.</value>
        public Type? IAmFilterableByFranchiseTType { get; }

        /// <summary>Gets the type of the am filterable by category t.</summary>
        /// <value>The type of the am filterable by category t.</value>
        public Type? IAmFilterableByCategoryTType { get; }

        /// <summary>Gets the type of the am filterable by manufacturer t.</summary>
        /// <value>The type of the am filterable by manufacturer t.</value>
        public Type? IAmFilterableByManufacturerTType { get; }

        /// <summary>Gets the type of the am filterable by product.</summary>
        /// <value>The type of the am filterable by product.</value>
        public Type? IAmFilterableByProductTType { get; }

        /// <summary>Gets the type of the am filterable by store t.</summary>
        /// <value>The type of the am filterable by store t.</value>
        public Type? IAmFilterableByStoreTType { get; }

        /// <summary>Gets the type of the am filterable by user t.</summary>
        /// <value>The type of the am filterable by user t.</value>
        public Type? IAmFilterableByUserTType { get; }

        /// <summary>Gets the type of the am filterable by vendor t.</summary>
        /// <value>The type of the am filterable by vendor t.</value>
        public Type? IAmFilterableByVendorTType { get; }

        /// <summary>Gets the type of the have applied discounts t.</summary>
        /// <value>The type of the have applied discounts t.</value>
        public Type? IHaveAppliedDiscountsTType { get; }

        /// <summary>Gets the type of the have contacts t.</summary>
        /// <value>The type of the have contacts t.</value>
        public Type? IHaveContactsTType { get; }

        /// <summary>Gets the type of the have currencies t.</summary>
        /// <value>The type of the have currencies t.</value>
        public Type? IHaveCurrenciesTType { get; }

        /// <summary>Gets the type of the have languages t.</summary>
        /// <value>The type of the have languages t.</value>
        public Type? IHaveLanguagesTType { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is deprecated.</summary>
        /// <value>True if this TypeDeterminer is deprecated, false if not.</value>
        public bool IsDeprecated { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is excluded from t 4.</summary>
        /// <value>True if this TypeDeterminer is excluded from t 4, false if not.</value>
        public bool IsExcludedFromT4 { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a brand relationship master table.</summary>
        /// <value>True if this TypeDeterminer is i am a brand relationship master table, false if not.</value>
        public bool IsIAmABrandRelationshipMasterTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a brand relationship slave table.</summary>
        /// <value>True if this TypeDeterminer is i am a brand relationship slave table, false if not.</value>
        public bool IsIAmABrandRelationshipSlaveTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a category relationship master table.</summary>
        /// <value>True if this TypeDeterminer is i am a category relationship master table, false if not.</value>
        public bool IsIAmACategoryRelationshipMasterTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a category relationship slave table.</summary>
        /// <value>True if this TypeDeterminer is i am a category relationship slave table, false if not.</value>
        public bool IsIAmACategoryRelationshipSlaveTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a franchise relationship master table.</summary>
        /// <value>True if this TypeDeterminer is i am a franchise relationship master table, false if not.</value>
        public bool IsIAmAFranchiseRelationshipMasterTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a franchise relationship slave table.</summary>
        /// <value>True if this TypeDeterminer is i am a franchise relationship slave table, false if not.</value>
        public bool IsIAmAFranchiseRelationshipSlaveTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a contact relationship table.</summary>
        /// <value>True if this TypeDeterminer is i am a contact relationship table, false if not.</value>
        public bool IsIAmAContactRelationshipTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a currency relationship table.</summary>
        /// <value>True if this TypeDeterminer is i am a currency relationship table, false if not.</value>
        public bool IsIAmACurrencyRelationshipTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a discount filter relationship table.</summary>
        /// <value>True if this TypeDeterminer is i am a discount filter relationship table, false if not.</value>
        public bool IsIAmADiscountFilterRelationshipTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a favorite relationship table.</summary>
        /// <value>True if this TypeDeterminer is i am a favorite relationship table, false if not.</value>
        public bool IsIAmAFavoriteRelationshipTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a language relationship table.</summary>
        /// <value>True if this TypeDeterminer is i am a language relationship table, false if not.</value>
        public bool IsIAmALanguageRelationshipTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a litely tracked event base.</summary>
        /// <value>True if this TypeDeterminer is i am a litely tracked event base, false if not.</value>
        public bool IsIAmALitelyTrackedEventBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a manufacturer relationship master table.</summary>
        /// <value>True if this TypeDeterminer is i am a manufacturer relationship master table, false if not.</value>
        public bool IsIAmAManufacturerRelationshipMasterTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a manufacturer relationship slave table.</summary>
        /// <value>True if this TypeDeterminer is i am a manufacturer relationship slave table, false if not.</value>
        public bool IsIAmAManufacturerRelationshipSlaveTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am an account relationship master table.</summary>
        /// <value>True if this TypeDeterminer is i am an account relationship master table, false if not.</value>
        public bool IsIAmAnAccountRelationshipMasterTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am an account relationship slave table.</summary>
        /// <value>True if this TypeDeterminer is i am an account relationship slave table, false if not.</value>
        public bool IsIAmAnAccountRelationshipSlaveTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am an image table.</summary>
        /// <value>True if this TypeDeterminer is i am an image table, false if not.</value>
        public bool IsIImageBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a product relationship master table.</summary>
        /// <value>True if this TypeDeterminer is i am a product relationship master table, false if not.</value>
        public bool IsIAmAProductRelationshipMasterTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a product relationship slave table.</summary>
        /// <value>True if this TypeDeterminer is i am a product relationship slave table, false if not.</value>
        public bool IsIAmAProductRelationshipSlaveTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a relationship table.</summary>
        /// <value>True if this TypeDeterminer is i am a relationship table, false if not.</value>
        public bool IsIAmARelationshipTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a stored file relationship table.</summary>
        /// <value>True if this TypeDeterminer is i am a stored file relationship table, false if not.</value>
        public bool IsIAmAStoredFileRelationshipTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a store relationship master table.</summary>
        /// <value>True if this TypeDeterminer is i am a store relationship master table, false if not.</value>
        public bool IsIAmAStoreRelationshipMasterTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a store relationship slave table.</summary>
        /// <value>True if this TypeDeterminer is i am a store relationship slave table, false if not.</value>
        public bool IsIAmAStoreRelationshipSlaveTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a tracked event base.</summary>
        /// <value>True if this TypeDeterminer is i am a tracked event base, false if not.</value>
        public bool IsIAmATrackedEventBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a tracked event base t.</summary>
        /// <value>True if this TypeDeterminer is i am a tracked event base t, false if not.</value>
        public bool IsIAmATrackedEventBaseT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a user relationship master table.</summary>
        /// <value>True if this TypeDeterminer is i am a user relationship master table, false if not.</value>
        public bool IsIAmAUserRelationshipMasterTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a user relationship slave table.</summary>
        /// <value>True if this TypeDeterminer is i am a user relationship slave table, false if not.</value>
        public bool IsIAmAUserRelationshipSlaveTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a vendor relationship master table.</summary>
        /// <value>True if this TypeDeterminer is i am a vendor relationship master table, false if not.</value>
        public bool IsIAmAVendorRelationshipMasterTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am a vendor relationship slave table.</summary>
        /// <value>True if this TypeDeterminer is i am a vendor relationship slave table, false if not.</value>
        public bool IsIAmAVendorRelationshipSlaveTable { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by account.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by account, false if not.</value>
        public bool IsIAmFilterableByAccount { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by account is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by account is the relationship, false if not.</value>
        public bool IsIAmFilterableByAccountIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by account search.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by account search, false if not.</value>
        public bool IsIAmFilterableByAccountSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by account search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by account search is the relationship, false if not.</value>
        public bool IsIAmFilterableByAccountSearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by account.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by account t, false if not.</value>
        public bool IsIAmFilterableByAccountT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by brand.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by brand, false if not.</value>
        public bool IsIAmFilterableByBrand { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by brand is the relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by brand is the relationship, false if not.</value>
        public bool IsIAmFilterableByBrandIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by brand search.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by brand search, false if not.</value>
        public bool IsIAmFilterableByBrandSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by brand search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by brand search is the relationship, false if not.</value>
        public bool IsIAmFilterableByBrandSearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by brand t.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by brand t, false if not.</value>
        public bool IsIAmFilterableByBrandT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by category.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by category, false if not.</value>
        public bool IsIAmFilterableByCategory { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by category is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by category is the relationship, false if not.</value>
        public bool IsIAmFilterableByCategoryIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by category search.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by category search, false if not.</value>
        public bool IsIAmFilterableByCategorySearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by category search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by category search is the relationship, false if not.</value>
        public bool IsIAmFilterableByCategorySearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by category t.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by category t, false if not.</value>
        public bool IsIAmFilterableByCategoryT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by franchise.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by franchise, false if not.</value>
        public bool IsIAmFilterableByFranchise { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by franchise is the relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by franchise is the relationship, false if not.</value>
        public bool IsIAmFilterableByFranchiseIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by franchise search.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by franchise search, false if not.</value>
        public bool IsIAmFilterableByFranchiseSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by franchise search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by franchise search is the relationship, false if not.</value>
        public bool IsIAmFilterableByFranchiseSearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by franchise t.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by franchise t, false if not.</value>
        public bool IsIAmFilterableByFranchiseT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by manufacturer.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by manufacturer, false if not.</value>
        public bool IsIAmFilterableByManufacturer { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by manufacturer is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by manufacturer is the relationship, false if not.</value>
        public bool IsIAmFilterableByManufacturerIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by manufacturer search.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by manufacturer search, false if not.</value>
        public bool IsIAmFilterableByManufacturerSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by manufacturer search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by manufacturer search is the relationship, false if
        /// not.</value>
        public bool IsIAmFilterableByManufacturerSearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by manufacturer t.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by manufacturer t, false if not.</value>
        public bool IsIAmFilterableByManufacturerT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by nullable account.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by nullable account, false if not.</value>
        public bool IsIAmFilterableByNullableAccount { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by nullable brand.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by nullable brand, false if not.</value>
        public bool IsIAmFilterableByNullableBrand { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by nullable franchise.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by nullable franchise, false if not.</value>
        public bool IsIAmFilterableByNullableFranchise { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by nullable category.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by nullable category, false if not.</value>
        public bool IsIAmFilterableByNullableCategory { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by nullable manufacturer.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by nullable manufacturer, false if not.</value>
        public bool IsIAmFilterableByNullableManufacturer { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by nullable product.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by nullable product, false if not.</value>
        public bool IsIAmFilterableByNullableProduct { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by nullable store.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by nullable store, false if not.</value>
        public bool IsIAmFilterableByNullableStore { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by nullable user.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by nullable user, false if not.</value>
        public bool IsIAmFilterableByNullableUser { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by nullable vendor.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by nullable vendor, false if not.</value>
        public bool IsIAmFilterableByNullableVendor { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by product.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by product, false if not.</value>
        public bool IsIAmFilterableByProduct { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by product is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by product is the relationship, false if not.</value>
        public bool IsIAmFilterableByProductIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by product search.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by product search, false if not.</value>
        public bool IsIAmFilterableByProductSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by product search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by product search is the relationship, false if not.</value>
        public bool IsIAmFilterableByProductSearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by product.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by product t, false if not.</value>
        public bool IsIAmFilterableByProductT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by store.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by store, false if not.</value>
        public bool IsIAmFilterableByStore { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by store is the relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by store is the relationship, false if not.</value>
        public bool IsIAmFilterableByStoreIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by store search.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by store search, false if not.</value>
        public bool IsIAmFilterableByStoreSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by store search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by store search is the relationship, false if not.</value>
        public bool IsIAmFilterableByStoreSearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by store t.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by store t, false if not.</value>
        public bool IsIAmFilterableByStoreT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by user.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by user, false if not.</value>
        public bool IsIAmFilterableByUser { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by user is the relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by user is the relationship, false if not.</value>
        public bool IsIAmFilterableByUserIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by user search.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by user search, false if not.</value>
        public bool IsIAmFilterableByUserSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by user search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by user search is the relationship, false if not.</value>
        public bool IsIAmFilterableByUserSearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by user t.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by user t, false if not.</value>
        public bool IsIAmFilterableByUserT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by vendor.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by vendor, false if not.</value>
        public bool IsIAmFilterableByVendor { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by vendor is the relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by vendor is the relationship, false if not.</value>
        public bool IsIAmFilterableByVendorIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by vendor search.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by vendor search, false if not.</value>
        public bool IsIAmFilterableByVendorSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by vendor search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by vendor search is the relationship, false if not.</value>
        public bool IsIAmFilterableByVendorSearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i am filterable by vendor t.</summary>
        /// <value>True if this TypeDeterminer is i am filterable by vendor t, false if not.</value>
        public bool IsIAmFilterableByVendorT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i applied discount base.</summary>
        /// <value>True if this TypeDeterminer is i applied discount base, false if not.</value>
        public bool IsIAppliedDiscountBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i base.</summary>
        /// <value>True if this TypeDeterminer is i base, false if not.</value>
        public bool IsIBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i displayable base.</summary>
        /// <value>True if this TypeDeterminer is i displayable base, false if not.</value>
        public bool IsIDisplayableBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a contact base.</summary>
        /// <value>True if this TypeDeterminer is i have a contact base, false if not.</value>
        public bool IsIHaveAContactBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a contact base is the relationship.</summary>
        /// <value>True if this TypeDeterminer is i have a contact base is the relationship, false if not.</value>
        public bool IsIHaveAContactBaseIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a contact base search.</summary>
        /// <value>True if this TypeDeterminer is i have a contact base search, false if not.</value>
        public bool IsIHaveAContactBaseSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a contact base search is the
        /// relationship.</summary>
        /// <value>True if this TypeDeterminer is i have a contact base search is the relationship, false if not.</value>
        public bool IsIHaveAContactBaseSearchIsTheRelationship { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have ad counters.</summary>
        /// <value>True if this TypeDeterminer is i have ad counters, false if not.</value>
        public bool IsIHaveAdCounters { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a nullable contact base.</summary>
        /// <value>True if this TypeDeterminer is i have a nullable contact base, false if not.</value>
        public bool IsIHaveANullableContactBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a nullable stored file base.</summary>
        /// <value>True if this TypeDeterminer is i have a nullable stored file base, false if not.</value>
        public bool IsIHaveANullableStoredFileBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a parent base.</summary>
        /// <value>True if this TypeDeterminer is i have a parent base, false if not.</value>
        public bool IsIHaveAParentBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have applied discounts base.</summary>
        /// <value>True if this TypeDeterminer is i have applied discounts base, false if not.</value>
        public bool IsIHaveAppliedDiscountsBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a state base.</summary>
        /// <value>True if this TypeDeterminer is i have a state base, false if not.</value>
        public bool IsIHaveAStateBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a state base search.</summary>
        /// <value>True if this TypeDeterminer is i have a state base search, false if not.</value>
        public bool IsIHaveAStateBaseSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have the status base.</summary>
        /// <value>True if this TypeDeterminer is i have the status base, false if not.</value>
        public bool IsIHaveAStatusBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have the status base search.</summary>
        /// <value>True if this TypeDeterminer is i have the status base search, false if not.</value>
        public bool IsIHaveAStatusBaseSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a stored file base.</summary>
        /// <value>True if this TypeDeterminer is i have a stored file base, false if not.</value>
        public bool IsIHaveAStoredFileBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a type base.</summary>
        /// <value>True if this TypeDeterminer is i have a type base, false if not.</value>
        public bool IsIHaveATypeBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have a type base search.</summary>
        /// <value>True if this TypeDeterminer is i have a type base search, false if not.</value>
        public bool IsIHaveATypeBaseSearch { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have contacts base.</summary>
        /// <value>True if this TypeDeterminer is i have contacts base, false if not.</value>
        public bool IsIHaveContactsBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have currencies base.</summary>
        /// <value>True if this TypeDeterminer is i have currencies base, false if not.</value>
        public bool IsIHaveCurrenciesBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have dimensions.</summary>
        /// <value>True if this TypeDeterminer is i have dimensions, false if not.</value>
        public bool IsIHaveDimensions { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have free shipping minimums base.</summary>
        /// <value>True if this TypeDeterminer is i have free shipping minimums base, false if not.</value>
        public bool IsIHaveFreeShippingMinimumsBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have images base.</summary>
        /// <value>True if this TypeDeterminer is i have images base, false if not.</value>
        public bool IsIHaveImagesBase { get; }

        /// <summary>Gets the type of the is i have images base image.</summary>
        /// <value>The type of the is i have images base image.</value>
        public Type? IsIHaveImagesBaseImageType { get; }

        /// <summary>Gets the is i have images base image type td.</summary>
        /// <value>The is i have images base image type td.</value>
        public TypeDeterminer? IsIHaveImagesBaseImageTypeTD { get; }

        /// <summary>Gets the type of the is i have images base image.</summary>
        /// <value>The type of the is i have images base image.</value>
        public Type? IsIHaveImagesBaseImageTypeType { get; }

        /// <summary>Gets the is i have images base image type td.</summary>
        /// <value>The is i have images base image type td.</value>
        public TypeDeterminer? IsIHaveImagesBaseImageTypeTypeTD { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have JSON attributes base.</summary>
        /// <value>True if this TypeDeterminer is i have JSON attributes base, false if not.</value>
        public bool IsIHaveJsonAttributesBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have languages base.</summary>
        /// <value>True if this TypeDeterminer is i have languages base, false if not.</value>
        public bool IsIHaveLanguagesBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have notes base.</summary>
        /// <value>True if this TypeDeterminer is i have notes base, false if not.</value>
        public bool IsIHaveNotesBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have nullable dimensions.</summary>
        /// <value>True if this TypeDeterminer is i have nullable dimensions, false if not.</value>
        public bool IsIHaveNullableDimensions { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have order minimums base.</summary>
        /// <value>True if this TypeDeterminer is i have order minimums base, false if not.</value>
        public bool IsIHaveOrderMinimumsBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have requires roles base.</summary>
        /// <value>True if this TypeDeterminer is i have requires roles base, false if not.</value>
        public bool IsIHaveRequiresRolesBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have reviews base.</summary>
        /// <value>True if this TypeDeterminer is i have reviews base, false if not.</value>
        public bool IsIHaveReviewsBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have seo base.</summary>
        /// <value>True if this TypeDeterminer is i have seo base, false if not.</value>
        public bool IsIHaveSeoBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i have stored files base.</summary>
        /// <value>True if this TypeDeterminer is i have stored files base, false if not.</value>
        public bool IsIHaveStoredFilesBase { get; }

        /// <summary>Gets the type of the is i have stored files base file.</summary>
        /// <value>The type of the is i have stored files base file.</value>
        public Type? IsIHaveStoredFilesBaseFileType { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i nameable base.</summary>
        /// <value>True if this TypeDeterminer is i nameable base, false if not.</value>
        public bool IsINameableBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i object note base.</summary>
        /// <value>True if this TypeDeterminer is i object note base, false if not.</value>
        public bool IsIObjectNoteBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i product price point.</summary>
        /// <value>True if this TypeDeterminer is i product price point, false if not.</value>
        public bool IsIProductPricePoint { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i sales collection base.</summary>
        /// <value>True if this TypeDeterminer is i sales collection base, false if not.</value>
        public bool IsISalesCollectionBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i sales collection base t.</summary>
        /// <value>True if this TypeDeterminer is i sales collection base t, false if not.</value>
        public bool IsISalesCollectionBaseT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i sales event base.</summary>
        /// <value>True if this TypeDeterminer is i sales event base, false if not.</value>
        public bool IsISalesEventBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i sales item base.</summary>
        /// <value>True if this TypeDeterminer is i sales item base, false if not.</value>
        public bool IsISalesItemBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i sales item base t.</summary>
        /// <value>True if this TypeDeterminer is i sales item base t, false if not.</value>
        public bool IsISalesItemBaseT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i sales item base tt.</summary>
        /// <value>True if this TypeDeterminer is i sales item base tt, false if not.</value>
        public bool IsISalesItemBaseTT { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i sales item target base.</summary>
        /// <value>True if this TypeDeterminer is i sales item target base, false if not.</value>
        public bool IsISalesItemTargetBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i stateable base.</summary>
        /// <value>True if this TypeDeterminer is i stateable base, false if not.</value>
        public bool IsIStateableBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i statusable base.</summary>
        /// <value>True if this TypeDeterminer is i statusable base, false if not.</value>
        public bool IsIStatusableBase { get; }

        /// <summary>Gets a value indicating whether this TypeDeterminer is i typable base.</summary>
        /// <value>True if this TypeDeterminer is i typable base, false if not.</value>
        public bool IsITypableBase { get; }

        /// <summary>Gets the type of the related state.</summary>
        /// <value>The type of the related state.</value>
        public Type? RelatedStateType { get; }

        /// <summary>Gets the type of the related status.</summary>
        /// <value>The type of the related status.</value>
        public Type? RelatedStatusType { get; }

        /// <summary>Gets the type of the related.</summary>
        /// <value>The type of the related.</value>
        public Type? RelatedTypeType { get; }

        /// <summary>Gets the type of the relationship primary.</summary>
        /// <value>The type of the relationship primary.</value>
        public Type? RelationshipPrimaryType { get; }

        /// <summary>Gets the relationship primary type td.</summary>
        /// <value>The relationship primary type td.</value>
        public TypeDeterminer? RelationshipPrimaryTypeTD { get; }

        /// <summary>Gets the type of the relationship secondary.</summary>
        /// <value>The type of the relationship secondary.</value>
        public Type? RelationshipSecondaryType { get; }

        /// <summary>Gets the relationship secondary type td.</summary>
        /// <value>The relationship secondary type td.</value>
        public TypeDeterminer? RelationshipSecondaryTypeTD { get; }

        /// <summary>Gets the type of the sales item base discount.</summary>
        /// <value>The type of the sales item base discount.</value>
        public Type? SalesItemBaseDiscountType { get; }
    }
}
