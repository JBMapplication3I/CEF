// <copyright file="TabVendors.razor.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tab vendors.razor class</summary>
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

    /// <content>A tab vendors.</content>
    public partial class TabVendors
        : TabAssociationBase<
            ProductModel,
            VendorModel,
            VendorPagedResults,
            GetVendors,
            VendorProductModel,
            VendorProductPagedResults,
            GetVendorProducts,
            CreateVendorProduct,
            DeactivateVendorProductByID,
            ModalEditVendorProduct>
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
        protected override string Title => "Vendors";

        /// <inheritdoc />
        protected override Func<GetVendors, Task<IHttpPromiseCallbackArg<VendorPagedResults>>> OtherSearchEndpointFn
            => x => cvApi.GetVendors(x);

        /// <inheritdoc />
        protected override Func<GetVendorProducts, Task<IHttpPromiseCallbackArg<VendorProductPagedResults>>> AssocSearchEndpointFn
            => x => cvApi.GetVendorProducts(x);

        /// <inheritdoc />
        protected override Func<VendorProductModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocCreateEndpointFn
            => x => cvApi.CreateVendorProduct(Mapper.Map<CreateVendorProduct>(x));

        /// <inheritdoc />
        protected override Func<VendorProductModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocUpdateEndpointFn
            => x => cvApi.UpdateVendorProduct(Mapper.Map<UpdateVendorProduct>(x));

        /// <inheritdoc />
        protected override Func<DeactivateVendorProductByID, Task<IHttpPromiseCallbackArg<CEFActionResponse>>> AssocDeactivateEndpointFn
            => x => cvApi.DeactivateVendorProductByID(x);

        /// <inheritdoc />
        protected override Action ClearCollectionPropertyFn
            => () => Record.Vendors = null;

        /// <inheritdoc />
        protected override Func<VendorProductModel, string?>? GetAssocRecordDisplayString
            => x => x.MasterName;

        /// <inheritdoc />
        protected override Func<VendorProductModel, string?>? GetAssocRecordTooltipString
            => x => x.MasterID + " " + x.MasterKey + " " + x.MasterName;

        /// <inheritdoc />
        protected override Action<GetVendorProducts> AssignAdditionalSearchPropertiesFn
            => x => x.SlaveID = Record.ID;

        /// <inheritdoc />
        protected override Action<GetVendorProducts, int> AssignAdditionalDupeCheckPropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };

        /// <inheritdoc />
        protected override Action<CreateVendorProduct, int> AssignAdditionalCreatePropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };
    }
}
