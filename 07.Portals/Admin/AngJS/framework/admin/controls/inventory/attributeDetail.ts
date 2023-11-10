/**
 * @file framework/admin/controls/inventory.attributeDetail.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Attribute detail class
 */
module cef.admin.controls.inventory {
    class AttributeDetailController extends DetailBaseController<api.GeneralAttributeModel> {
        // Forced overrides
        detailName = "Attribute";
        // Collections
        types: api.TypeModel[] = [];
        tabs: api.AttributeTabModel[] = [];
        groups: api.AttributeGroupModel[] = [];
        predefinedOptions: api.GeneralAttributePredefinedOptionModel[] = [];
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.$q.all([
                    this.cvApi.attributes.GetAttributeTypes({ Active: true, AsListing: true }),
                    this.cvApi.attributes.GetAttributeTabs({ Active: true, AsListing: true }),
                    this.cvApi.attributes.GetAttributeGroups({ Active: true, AsListing: true }),
                    this.record && this.record.ID > 0
                        ? this.cvApi.attributes.GetGeneralAttributePredefinedOptions({
                            Active: true,
                            AsListing: true,
                            AttributeID: this.record.ID
                        })
                        : this.$q.resolve({ data: { Results: [] } })
                ]).then((rarr: ng.IHttpPromiseCallbackArg<api.PagedResultsBase<api.BaseModel>>[]) => {
                    this.types  = rarr[0].data.Results;
                    this.tabs   = rarr[1].data.Results;
                    this.groups = rarr[2].data.Results;
                    this.predefinedOptions = rarr[3].data.Results as any;
                    resolve();
                }).catch(reject);
            });
        }
        loadNewRecord(): ng.IPromise<api.GeneralAttributeModel> {
            this.record = <api.GeneralAttributeModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // TypableBase Properties
                DisplayName: null,
                SortOrder: null,
                // HaveATypeBase Properties
                TypeID: 0,
                TypeKey: null,
                TypeName: null,
                Type: null,
                // GeneralAttribute Properties
                HideFromSuppliers: false,
                HideFromStorefront: false,
                HideFromCatalogViews: false,
                HideFromProductDetailView: false,
                IsFilter: false,
                IsComparable: false,
                IsTab: false,
                IsPredefined: false,
                IsMarkup: false,
                Group: null,
                // Related Objects
                AttributeTabID: 0,
                AttributeTabKey: null,
                AttributeTabName: null,
                AttributeTabDisplayName: null,
                AttributeTab: null,
                AttributeGroupID: 0,
                AttributeGroupKey: null,
                AttributeGroupName: null,
                AttributeGroupDisplayName: null,
                AttributeGroup: null,
                // Associated Objects
                GeneralAttributePredefinedOptions: []
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> {
            this.detailName = "Attribute";
            return this.$q.resolve();
        }
        loadRecordCall(id: number): ng.IHttpPromise<api.GeneralAttributeModel> {
            return this.cvApi.attributes.GetGeneralAttributeByID(id);
        }
        createRecordCall(routeParams: api.GeneralAttributeModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.attributes.CreateGeneralAttribute(routeParams);
        }
        updateRecordCall(routeParams: api.GeneralAttributeModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.attributes.UpdateGeneralAttribute(routeParams);
        }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.attributes.DeactivateGeneralAttributeByID(id);
        }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.attributes.ReactivateGeneralAttributeByID(id);
        }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.attributes.DeleteGeneralAttributeByID(id);
        }
        loadRecordActionAfterSuccess(result: api.GeneralAttributeModel): ng.IPromise<api.GeneralAttributeModel> {
            this.updateToggles();
            return this.$q.resolve(result);
        }
        // Supportive Functions
        updateToggles(): void {
            if (this.record.HideFromSuppliers) {
                this.record.HideFromStorefront = true;
                this.record.HideFromProductDetailView = true;
                this.record.HideFromCatalogViews = true;
                this.record.IsFilter = false;
                this.record.IsComparable = false;
                this.record.IsTab = false;
            }
            if (this.record.HideFromStorefront) {
                this.record.HideFromProductDetailView = true;
                this.record.HideFromCatalogViews = true;
                this.record.IsFilter = false;
                this.record.IsComparable = false;
                this.record.IsTab = false;
            }
            if (this.record.HideFromProductDetailView) {
                this.record.IsTab = false;
            }
            if (this.record.HideFromCatalogViews) {
                this.record.IsFilter = false;
            }
            if (this.record.IsPredefined) {
                this.record.IsMarkup = false;
            }
            if (this.record.IsMarkup) {
                this.record.IsFilter = false;
            }
        }
        // Predefined Options Management Events
        openAddOptionModal(): void {
            if (!this.record || !this.record.ID) {
                alert("You must save the record before adding predefined options");
                return;
            }
            this.$uibModal.open({
                size: this.cvServiceStrings.modalSizes.sm,
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/inventory/modals/addOptionModal.html", "ui"),
                controller: modals.AddOptionModalController,
                controllerAs: "addOptionModalCtrl"
            }).result.then((result: { value: string, uofm?: string, sortOrder?: number }) => {
                if (!result || !result.value) {
                    return;
                }
                const exists = _.some(
                    this.predefinedOptions,
                    x => x.Value === result.value);
                if (exists) {
                    return;
                }
                if (!this.predefinedOptions) {
                    this.predefinedOptions = [];
                }
                this.setRunning();
                this.cvApi.attributes.CreateGeneralAttributePredefinedOption({
                    // Base Properties
                    ID: 0,
                    CustomKey: null,
                    Active: true,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    // Properties
                    Value: result.value,
                    UofM: result.uofm,
                    SortOrder: result.sortOrder,
                    // Related Objects
                    AttributeID: this.record.ID,
                    AttributeKey: null,
                    AttributeName: null,
                    Attribute: null
                }).then(r => {
                    if (!r || !r.data) {
                        this.finishRunning(true, r && r.data as any);
                        return;
                    }
                    this.cvApi.attributes.GetGeneralAttributePredefinedOptionByID(r.data.Result).then(r2 => {
                        this.predefinedOptions.push(r2.data);
                        this.finishRunning();
                        this.forms["Details"].$setDirty();
                    }).catch(reason2 => this.finishRunning(true, reason2));
                }).catch(reason => this.finishRunning(true, reason));
            });
        }
        removeOption(index: number): void {
            if (!this.record || !this.record.ID) {
                // Must save first
                return;
            }
            const id = this.predefinedOptions[index].ID;
            this.setRunning();
            this.cvApi.attributes.DeactivateGeneralAttributePredefinedOptionByID(id).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, null, r && r.data && r.data.Messages);
                    return;
                }
                this.predefinedOptions.splice(index, 1);
                this.forms["Details"].$setDirty();
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $filter: ng.IFilterService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $state: ng.ui.IStateService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("attributeDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/inventory/attributeDetail.html", "ui"),
        controller: AttributeDetailController,
        controllerAs: "attributeDetailCtrl"
    }));
}
