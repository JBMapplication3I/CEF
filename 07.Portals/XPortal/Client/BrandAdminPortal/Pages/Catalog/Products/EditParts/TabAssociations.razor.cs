// <copyright file="TabProducts.razor.cs" company="clarity-ventures.com">
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
    public partial class TabAssociations
        : TabAssociationBase<
            ProductModel,
            ProductModel,
            ProductPagedResults,
            GetProducts,
            ProductAssociationModel,
            ProductAssociationPagedResults,
            GetProductAssociations,
            CreateProductAssociation,
            DeactivateProductAssociationByID,
            ModalEditProductAssociation>
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

        private ModalCreateProductAssociation? modalCreate;

        /// <inheritdoc />
        protected override string Title => "Associations";

        /// <inheritdoc />
        protected override Func<GetProducts, Task<IHttpPromiseCallbackArg<ProductPagedResults>>> OtherSearchEndpointFn
            => x => cvApi.GetProducts(x);

        /// <inheritdoc />
        protected override Func<GetProductAssociations, Task<IHttpPromiseCallbackArg<ProductAssociationPagedResults>>> AssocSearchEndpointFn
            => x => cvApi.GetProductAssociations(x);

        /// <inheritdoc />
        protected override Func<ProductAssociationModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocCreateEndpointFn
            => x => cvApi.CreateProductAssociation(Mapper.Map<CreateProductAssociation>(x));

        /// <inheritdoc />
        protected override Func<ProductAssociationModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocUpdateEndpointFn
            => x => cvApi.UpdateProductAssociation(Mapper.Map<UpdateProductAssociation>(x));

        /// <inheritdoc />
        protected override Func<DeactivateProductAssociationByID, Task<IHttpPromiseCallbackArg<CEFActionResponse>>> AssocDeactivateEndpointFn
            => x => cvApi.DeactivateProductAssociationByID(x);

        /// <inheritdoc />
        protected override Action ClearCollectionPropertyFn
            => () => Record.ProductAssociations = null;

        /// <inheritdoc />
        protected override Func<ProductAssociationModel, string?>? GetAssocRecordDisplayString
            => x => x.MasterName;

        /// <inheritdoc />
        protected override Func<ProductAssociationModel, string?>? GetAssocRecordTooltipString
            => x => x.MasterID + " " + x.MasterKey + " " + x.MasterName;

        /// <inheritdoc />
        protected override Action<GetProductAssociations> AssignAdditionalSearchPropertiesFn
            => x => x.SlaveID = Record.ID;

        /// <inheritdoc />
        protected override Action<GetProductAssociations, int> AssignAdditionalDupeCheckPropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };

        /// <inheritdoc />
        protected override Action<CreateProductAssociation, int> AssignAdditionalCreatePropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };
    }
}
