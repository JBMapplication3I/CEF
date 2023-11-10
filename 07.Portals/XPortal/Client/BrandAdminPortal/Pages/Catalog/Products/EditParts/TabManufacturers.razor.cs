// <copyright file="TabManufacturers.razor.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tab manufacturers.razor class</summary>
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary.Pages.Catalog.Products.EditParts
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Components;
    using MVC.Api.Callers;
    using MVC.Api.Endpoints;
    using MVC.Api.Models;
    using MVC.Core;

    /// <content>A tab manufacturers.</content>
    public partial class TabManufacturers
        : TabAssociationBase<
            ProductModel,
            ManufacturerModel,
            ManufacturerPagedResults,
            GetManufacturers,
            ManufacturerProductModel,
            ManufacturerProductPagedResults,
            GetManufacturerProducts,
            CreateManufacturerProduct,
            DeactivateManufacturerProductByID,
            ModalEditManufacturerProduct>
    {
        #region Injected
#pragma warning disable CS8618 // Ignored: Set by Parameter
        /// <summary>Gets or sets the mapper.</summary>
        /// <value>The mapper.</value>
        [Inject]
        protected IMapper Mapper { get; set; }

        /// <summary>Gets or sets the CEF API.</summary>
        /// <value>The CEF API.</value>
        [Inject]
        protected CEFAPI cvApi { get; set; }
#pragma warning restore CS8618 // Ignored: Set by Parameter
        #endregion

        /// <inheritdoc />
        protected override string Title => "Manufacturers";

        /// <inheritdoc />
        protected override Func<GetManufacturers, Task<IHttpPromiseCallbackArg<ManufacturerPagedResults>>> OtherSearchEndpointFn
            => x => cvApi.GetManufacturers(x);

        /// <inheritdoc />
        protected override Func<GetManufacturerProducts, Task<IHttpPromiseCallbackArg<ManufacturerProductPagedResults>>> AssocSearchEndpointFn
            => x => cvApi.GetManufacturerProducts(x);

        /// <inheritdoc />
        protected override Func<ManufacturerProductModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocCreateEndpointFn
            => x => cvApi.CreateManufacturerProduct(Mapper.Map<CreateManufacturerProduct>(x));

        /// <inheritdoc />
        protected override Func<ManufacturerProductModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocUpdateEndpointFn
            => x => cvApi.UpdateManufacturerProduct(Mapper.Map<UpdateManufacturerProduct>(x));

        /// <inheritdoc />
        protected override Func<DeactivateManufacturerProductByID, Task<IHttpPromiseCallbackArg<CEFActionResponse>>> AssocDeactivateEndpointFn
            => x => cvApi.DeactivateManufacturerProductByID(x);

        /// <inheritdoc />
        protected override Action ClearCollectionPropertyFn
            => () => Record.Manufacturers = null;

        /// <inheritdoc />
        protected override Func<ManufacturerProductModel, string?>? GetAssocRecordDisplayString
            => x => x.MasterName;

        /// <inheritdoc />
        protected override Func<ManufacturerProductModel, string?>? GetAssocRecordTooltipString
            => x => x.MasterID + " " + x.MasterKey + " " + x.MasterName;

        /// <inheritdoc />
        protected override Action<GetManufacturerProducts> AssignAdditionalSearchPropertiesFn
            => x => x.SlaveID = Record.ID;

        /// <inheritdoc />
        protected override Action<GetManufacturerProducts, int> AssignAdditionalDupeCheckPropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };

        /// <inheritdoc />
        protected override Action<CreateManufacturerProduct, int> AssignAdditionalCreatePropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };
    }
}
