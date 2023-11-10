// <copyright file="TabFranchises.razor.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tab franchises.razor class</summary>
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

    /// <content>A tab franchises.</content>
    public partial class TabFranchises
        : Catalog.Products.EditParts.TabAssociationBase<
            UserModel,
            FranchiseModel,
            FranchisePagedResults,
            GetFranchises,
            FranchiseUserModel,
            FranchiseUserPagedResults,
            GetFranchiseUsers,
            CreateFranchiseUser,
            DeactivateFranchiseUserByID,
            ModalEditFranchiseUser>
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
        protected override Func<GetFranchiseUsers, Task<IHttpPromiseCallbackArg<FranchiseUserPagedResults>>> AssocSearchEndpointFn
            => x => cvApi.GetFranchiseUsers(x);

        /// <inheritdoc />
        protected override Func<FranchiseUserModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocCreateEndpointFn
            => x => cvApi.CreateFranchiseUser(Mapper.Map<CreateFranchiseUser>(x));

        /// <inheritdoc />
        protected override Func<FranchiseUserModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocUpdateEndpointFn
            => x => cvApi.UpdateFranchiseUser(Mapper.Map<UpdateFranchiseUser>(x));

        /// <inheritdoc />
        protected override Func<DeactivateFranchiseUserByID, Task<IHttpPromiseCallbackArg<CEFActionResponse>>> AssocDeactivateEndpointFn
            => x => cvApi.DeactivateFranchiseUserByID(x);

        /// <inheritdoc />
        protected override Action ClearCollectionPropertyFn
            => () => Record.Franchises = null;

        /// <inheritdoc />
        protected override Func<FranchiseUserModel, string?>? GetAssocRecordDisplayString
            => x => x.MasterName;

        /// <inheritdoc />
        protected override Func<FranchiseUserModel, string?>? GetAssocRecordTooltipString
            => x => x.MasterID + " " + x.MasterKey + " " + x.MasterName;

        /// <inheritdoc />
        protected override Action<GetFranchiseUsers> AssignAdditionalSearchPropertiesFn
            => x => x.SlaveID = Record.ID;

        /// <inheritdoc />
        protected override Action<GetFranchiseUsers, int> AssignAdditionalDupeCheckPropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };

        /// <inheritdoc />
        protected override Action<CreateFranchiseUser, int> AssignAdditionalCreatePropertiesFn
            => (x, idToAssociate) =>
            {
                x.MasterID = idToAssociate;
                x.SlaveID = Record.ID;
            };
    }
}
