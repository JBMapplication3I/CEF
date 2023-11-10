// <copyright file="TabStores.razor.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tab stores.razor class</summary>
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary.Pages.Customers.Users.EditParts
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Components;
    using MVC.Api.Callers;
    using MVC.Api.Endpoints;
    using MVC.Api.Models;
    using MVC.Core;

    /// <content>A tab stores.</content>
    public partial class TabStores
        : Catalog.Products.EditParts.TabAssociationBase<
            UserModel,
            StoreModel,
            StorePagedResults,
            GetStores,
            StoreUserModel,
            StoreUserPagedResults,
            GetStoreUsers,
            CreateStoreUser,
            DeactivateStoreUserByID,
            ModalEditStoreUser>
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
        protected override string Title => "Stores";

        /// <inheritdoc />
        protected override Func<GetStores, Task<IHttpPromiseCallbackArg<StorePagedResults>>> OtherSearchEndpointFn
            => x => cvApi.GetStores(x);

        /// <inheritdoc />
        protected override Func<GetStoreUsers, Task<IHttpPromiseCallbackArg<StoreUserPagedResults>>> AssocSearchEndpointFn
            => x => cvApi.GetStoreUsers(x);

        /// <inheritdoc />
        protected override Func<StoreUserModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocCreateEndpointFn
            => x => cvApi.CreateStoreUser(Mapper.Map<CreateStoreUser>(x));

        /// <inheritdoc />
        protected override Func<StoreUserModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocUpdateEndpointFn
            => x => cvApi.UpdateStoreUser(Mapper.Map<UpdateStoreUser>(x));

        /// <inheritdoc />
        protected override Func<DeactivateStoreUserByID, Task<IHttpPromiseCallbackArg<CEFActionResponse>>> AssocDeactivateEndpointFn
            => x => cvApi.DeactivateStoreUserByID(x);

        /// <inheritdoc />
        protected override Action ClearCollectionPropertyFn
            => () => Record.Stores = null;

        /// <inheritdoc />
        protected override Func<StoreUserModel, string?>? GetAssocRecordDisplayString
            => x => x.MasterName;

        /// <inheritdoc />
        protected override Func<StoreUserModel, string?>? GetAssocRecordTooltipString
            => x => x.MasterID + " " + x.MasterKey + " " + x.MasterName;

        /// <inheritdoc />
        protected override Action<GetStoreUsers> AssignAdditionalSearchPropertiesFn
            => x => x.SlaveID = Record.ID;

        /// <inheritdoc />
        protected override Action<GetStoreUsers, int> AssignAdditionalDupeCheckPropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };

        /// <inheritdoc />
        protected override Action<CreateStoreUser, int> AssignAdditionalCreatePropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };
    }
}
