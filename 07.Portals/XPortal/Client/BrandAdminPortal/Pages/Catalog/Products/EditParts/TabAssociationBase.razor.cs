// ReSharper disable UnassignedGetOnlyAutoProperty
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary.Pages.Catalog.Products.EditParts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using MVC.Api.Models;
    using MVC.Core;
    using MVC.Utilities;
    using Shared.Modals;
    using Shared.Paged;

    /// <content>A tab association base.</content>
    public abstract partial class TabAssociationBase<
            TRecordModel,
            TOtherModel,
            TOtherPagedResults,
            TOtherSearchEndpoint,
            TAssocModel,
            TAssocPagedResults,
            TAssocSearchEndpoint,
            TAssocCreateEndpoint,
            TAssocDeactivateEndpoint,
            TModalEditAssoc>
        : TemplatedControllerBase
        where TRecordModel : BaseModel
        where TOtherModel : NameableBaseModel
        where TOtherPagedResults : PagedResultsBase<TOtherModel>
        where TOtherSearchEndpoint : NameableBaseSearchModel, new()
        where TAssocModel : AmARelationshipTableBaseModel
        where TAssocPagedResults : PagedResultsBase<TAssocModel>
        where TAssocSearchEndpoint : AmARelationshipTableBaseSearchModel, new()
        where TAssocCreateEndpoint : TAssocModel, new()
        where TAssocDeactivateEndpoint : ImplementsIDBase, new()
        where TModalEditAssoc : ModalEditAssociationBase<TAssocModel>
    {
        #region Parameters
#pragma warning disable CS8618 // Ignored: Set by Parameter
        /// <summary>Gets or sets the record.</summary>
        /// <value>The record.</value>
        [Parameter, EditorRequired]
        public TRecordModel Record { get; set; }

        /// <summary>Gets the title.</summary>
        /// <value>The title.</value>
        protected abstract string Title { get; }

        /// <summary>Gets the other search endpoint function.</summary>
        /// <value>The other search endpoint function.</value>
        protected abstract Func<TOtherSearchEndpoint, Task<IHttpPromiseCallbackArg<TOtherPagedResults>>> OtherSearchEndpointFn { get; }

        /// <summary>Gets the associated search endpoint function.</summary>
        /// <value>The associated search endpoint function.</value>
        protected abstract Func<TAssocSearchEndpoint, Task<IHttpPromiseCallbackArg<TAssocPagedResults>>> AssocSearchEndpointFn { get; }

        /// <summary>Gets the associated create endpoint function.</summary>
        /// <value>The associated create endpoint function.</value>
        protected abstract Func<TAssocModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocCreateEndpointFn { get; }

        /// <summary>Gets the associated update endpoint function.</summary>
        /// <value>The associated update endpoint function.</value>
        protected abstract Func<TAssocModel, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocUpdateEndpointFn { get; }

        /// <summary>Gets the associated deactivate endpoint function.</summary>
        /// <value>The associated deactivate endpoint function.</value>
        protected abstract Func<TAssocDeactivateEndpoint, Task<IHttpPromiseCallbackArg<CEFActionResponse>>> AssocDeactivateEndpointFn { get; }

        /// <summary>Gets the clear collection property function.</summary>
        /// <value>The clear collection property function.</value>
        protected abstract Action ClearCollectionPropertyFn { get; }

        /// <summary>Gets the get associated record display string.</summary>
        /// <value>The get associated record display string.</value>
        protected abstract Func<TAssocModel, string?>? GetAssocRecordDisplayString { get; }

        /// <summary>Gets the get associated record tooltip string.</summary>
        /// <value>The get associated record tooltip string.</value>
        protected abstract Func<TAssocModel, string?>? GetAssocRecordTooltipString { get; }

        /// <summary>Gets the assign additional search properties function.</summary>
        /// <value>The assign additional search properties function.</value>
        protected abstract Action<TAssocSearchEndpoint>? AssignAdditionalSearchPropertiesFn { get; }

        /// <summary>Gets the assign additional dupe check properties function.</summary>
        /// <value>The assign additional dupe check properties function.</value>
        protected abstract Action<TAssocSearchEndpoint, int>? AssignAdditionalDupeCheckPropertiesFn { get; }

        /// <summary>Gets the assign additional create properties function.</summary>
        /// <value>The assign additional create properties function.</value>
        protected abstract Action<TAssocCreateEndpoint, int>? AssignAdditionalCreatePropertiesFn { get; }
#pragma warning restore CS8618 // Ignored: Set by Parameter
        #endregion

        /// <summary>Gets or sets the modal edit.</summary>
        /// <value>The modal edit.</value>
        protected TModalEditAssoc? modalEdit { get; set; }

        private string? quickFilter;
        private ServerSidePaging<TOtherModel, TOtherPagedResults, TOtherSearchEndpoint>? available;
        private ServerSidePaging<TAssocModel, TAssocPagedResults, TAssocSearchEndpoint>? assigned;
        private MessageModalFactory? messageModalFactory;
        private ConfirmModalFactory? confirmModalFactory;

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            DebugBeginMethod();
            await base.OnInitializedAsync().ConfigureAwait(false);
            available = new(
                searchCall: OtherSearchEndpointFn,
                size: 8,
                name: $"product-{Record.ID}.to.{Title}.available",
                searchParameterName: null,
                searchParamsToMerge: () => new TOtherSearchEndpoint
                {
                    Active = true,
                    AsListing = true,
                    Sorts = new[]
                    {
                        new Sort { Field = "Name", Order = 0, Dir = "asc" },
                        new Sort { Field = "CustomKey", Order = 1, Dir = "asc" },
                        new Sort { Field = "ID", Order = 2, Dir = "asc" }
                    },
                },
                waitCondition: () => !Contract.CheckValidID(Record.ID),
                callback: StateHasChanged);
            assigned = new(
                searchCall: AssocSearchEndpointFn,
                size: 8,
                name: $"product-{Record.ID}.to.{Title}.assigned",
                searchParameterName: null,
                searchParamsToMerge: () =>
                {
                    var retVal = new TAssocSearchEndpoint
                    {
                        Active = true,
                        AsListing = true,
                        Sorts = new[]
                        {
                            new Sort { Field = "CustomKey", Order = 0, Dir = "asc" },
                            new Sort { Field = "ID", Order = 1, Dir = "asc" }
                        },
                    };
                    AssignAdditionalSearchPropertiesFn?.Invoke(retVal);
                    return retVal;
                },
                waitCondition: () => !Contract.CheckValidID(Record.ID),
                callback: StateHasChanged);
            await FinishRunningAsync().ConfigureAwait(false);
            ViewState.loading = false;
            DebugEndMethod();
        }

        /// <summary>Extended record data enforcement on save.</summary>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A Task.</returns>
        public Task ExtendedRecordDataEnforcementOnSaveAsync(DateTime timestamp)
        {
            DebugBeginMethod();
            DebugEndMethod();
            return Task.CompletedTask;
        }

        /// <summary>Extended record data calls on after save.</summary>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A Task.</returns>
        public Task ExtendedRecordDataCallsOnAfterSaveAsync(DateTime timestamp)
        {
            DebugBeginMethod();
            DebugEndMethod();
            return Task.CompletedTask;
        }

        private async Task AddAssociationAsync(int idToAssociate)
        {
            DebugBeginMethod();
            if (!Contract.CheckValidID(idToAssociate))
            {
                DebugEndMethod();
                return;
            }
            /* This check isn't necessary
            if (!record || !record.ID)
            {
                cvMessageModalFactory.show("You must save the record first");
                DebugEndMethod();
                return;
            }
            */
            // Ensure the data is loaded
            var model = available!.DataUnpaged.SingleOrDefault(x => x.ID == idToAssociate);
            if (model is null)
            {
                DebugEndMethod();
                return;
            }
            // Ensure it's not already in the collection
            if (assigned!.DataUnpaged.Any(x => x.Active && x.MasterID == model.ID))
            {
                // cvMessageModalFactory.show("This is already associated");
                DebugEndMethod();
                return;
            }
            await SetRunningAsync().ConfigureAwait(false);
            TAssocSearchEndpoint dupeCheckDto = new()
            {
                Active = true,
                AsListing = true,
            };
            AssignAdditionalDupeCheckPropertiesFn?.Invoke(dupeCheckDto, idToAssociate);
            var r = await AssocSearchEndpointFn(dupeCheckDto).ConfigureAwait(false);
            if (r.data?.Results is null)
            {
                await messageModalFactory!.ShowAsync(
                        message: "Could not retrieve data from duplicate check",
                        callback: () => FinishRunningAsync(true, "Could not retrieve data from duplicate check"))
                    .ConfigureAwait(false);
                DebugEndMethod();
                return;
            }
            if (Contract.CheckNotEmpty(r.data.Results))
            {
                await messageModalFactory!.ShowAsync(
                        message: "This is already in the collection.",
                        callback: () => FinishRunningAsync(true, "This is already in the collection."))
                    .ConfigureAwait(false);
                DebugEndMethod();
                return;
            }
            // Add it
            var createDto = new TAssocCreateEndpoint
            {
                Active = true,
                CreatedDate = DateTime.Now,
            };
            AssignAdditionalCreatePropertiesFn?.Invoke(createDto, model.ID);
            var rc = await AssocCreateEndpointFn(createDto).ConfigureAwait(false);
            if (rc.data is not { ActionSucceeded: true, Result: > 0 })
            {
                await FinishRunningAsync(true, "ERROR! Failed to create the association in the server").ConfigureAwait(false);
                DebugEndMethod();
                return;
            }
            // Ensure we are only setting the new way
            ClearCollectionPropertyFn();
            assigned.ResetAll(); // Pull updated data
            assigned.Search(); // Pull updated data
            ViewState.MakeDirty();
            await FinishRunningAsync().ConfigureAwait(false);
            StateHasChanged();
            DebugEndMethod();
        }

        private Task EditAssociationAsync(TAssocModel toEdit)
        {
            DebugBeginMethod();
            DebugEndMethod();
            return modalEdit!.ShowAsync(
                association: toEdit,
                callback: async (accept, toSend) =>
                {
                    DebugBeginMethod();
                    if (accept)
                    {
                        var r = await AssocUpdateEndpointFn(toSend).ConfigureAwait(false);
                        if (r.data?.ActionSucceeded != true)
                        {
                            await FinishRunningAsync(true, errorMessages: r.data?.Messages.ToArray()).ConfigureAwait(false);
                            StateHasChanged();
                            DebugEndMethod();
                            return;
                        }
                        // Ensure we are only setting the new way
                        ClearCollectionPropertyFn();
                        assigned!.ResetAll(); // Pull updated data
                        assigned.Search(); // Pull updated data
                        ViewState.MakeDirty();
                    }
                    await FinishRunningAsync().ConfigureAwait(false);
                    StateHasChanged();
                    DebugEndMethod();
                });
        }

        private Task RemoveAssociationAsync(TAssocModel toRemove)
        {
            DebugBeginMethod();
            DebugEndMethod();
            return confirmModalFactory!.ShowAsync(
                message: "Are you sure you want to remove this association?",
                callback: async result =>
                {
                    DebugBeginMethod();
                    if (!result)
                    {
                        DebugEndMethod();
                        return;
                    }
                    var r = await AssocDeactivateEndpointFn(new TAssocDeactivateEndpoint { ID = toRemove.ID }).ConfigureAwait(false);
                    if (r.data is not { ActionSucceeded: true })
                    {
                        await FinishRunningAsync(
                                true,
                                "ERROR! Failed to disassociate the record in the server.",
                                r.data?.Messages.ToArray())
                            .ConfigureAwait(false);
                        DebugEndMethod();
                        return;
                    }
                    // Ensure we are only setting the new way
                    ClearCollectionPropertyFn();
                    assigned!.ResetAll(); // Pull updated data
                    assigned.Search(); // Pull updated data
                    await messageModalFactory!.ShowAsync(
                            message: "The record has been disassociated on the server",
                            callback: () => FinishRunningAsync())
                        .ConfigureAwait(false);
                    StateHasChanged();
                    DebugEndMethod();
                });
        }
    }
}
