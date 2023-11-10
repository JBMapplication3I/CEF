// <copyright file="TabFranchises.razor.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tab franchises.razor class</summary>
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

    /// <content>A tab franchises.</content>
    public partial class TabFranchises
        : TabAssociationBase<
            ProductModel,
            FranchiseModel,
            FranchisePagedResults,
            GetFranchises,
            FranchiseProductModel,
            FranchiseProductPagedResults,
            GetFranchiseProducts,
            CreateFranchiseProduct,
            DeactivateFranchiseProductByID,
            ModalEditFranchiseProduct>
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
        protected override string Title => "Franchises";

        /// <inheritdoc />
        protected override Func<GetFranchises, Task<IHttpPromiseCallbackArg<FranchisePagedResults>>> OtherSearchEndpointFn
            => x => cvApi.GetFranchises(x);

        /// <inheritdoc />
        protected override Func<GetFranchiseProducts, Task<IHttpPromiseCallbackArg<FranchiseProductPagedResults>>> AssocSearchEndpointFn
            => x => cvApi.GetFranchiseProducts(x);

        /// <inheritdoc />
        protected override Func<FranchiseProductModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocCreateEndpointFn
            => x => cvApi.CreateFranchiseProduct(Mapper.Map<CreateFranchiseProduct>(x));

        /// <inheritdoc />
        protected override Func<FranchiseProductModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocUpdateEndpointFn
            => x => cvApi.UpdateFranchiseProduct(Mapper.Map<UpdateFranchiseProduct>(x));

        /// <inheritdoc />
        protected override Func<DeactivateFranchiseProductByID, Task<IHttpPromiseCallbackArg<CEFActionResponse>>> AssocDeactivateEndpointFn
            => x => cvApi.DeactivateFranchiseProductByID(x);

        /// <inheritdoc />
        protected override Action ClearCollectionPropertyFn
            => () => Record.Franchises = null;

        /// <inheritdoc />
        protected override Func<FranchiseProductModel, string?>? GetAssocRecordDisplayString
            => x => x.MasterName;

        /// <inheritdoc />
        protected override Func<FranchiseProductModel, string?>? GetAssocRecordTooltipString
            => x => x.MasterID + " " + x.MasterKey + " " + x.MasterName;

        /// <inheritdoc />
        protected override Action<GetFranchiseProducts> AssignAdditionalSearchPropertiesFn
            => x => x.SlaveID = Record.ID;

        /// <inheritdoc />
        protected override Action<GetFranchiseProducts, int> AssignAdditionalDupeCheckPropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };

        /// <inheritdoc />
        protected override Action<CreateFranchiseProduct, int> AssignAdditionalCreatePropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };
    }
}
