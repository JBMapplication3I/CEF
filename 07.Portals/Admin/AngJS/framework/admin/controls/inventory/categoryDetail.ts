/**
 * @file framework/admin/controls/inventory/categoryDetail.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Category detail class
 */
module cef.admin.controls.inventory {
    class CategoryDetailController extends DetailBaseController<api.CategoryModel> {
        // Forced overrides
        detailName = "Category";
        // Collections
        types: api.TypeModel[] = [];
        categories: api.CategoryModel[] = [];
        products: api.ProductModel[] = [];
        imageTypes: api.TypeModel[] = [];
        brands: api.BrandModel[] = [];
        stores: api.StoreModel[] = [];
        unbindAttributesChanged: Function;
        parent: api.CategoryModel;
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ Size: 500, StartIndex: 1 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            const altDto = { Active: true, AsListing: true, Paging: paging, IncludeChildrenInResults: false };
            return this.$q((resolve, reject) => {
                this.$q.all([
                    this.cvApi.categories.GetCategoryTypes(altDto),
                    this.cvApi.categories.GetCategoryImageTypes(standardDto),
                    /*this.cvApi.categories.GetCategories(altDto),
                    this.cvApi.products.GetProducts(standardDto),
                    this.cvApi.stores.GetStores(standardDto),*/
                    this.cefConfig.featureSet.brands.enabled ? this.cvApi.brands.GetBrands(standardDto) : this.$q.resolve(null),
                ]).then((rarr: ng.IHttpPromiseCallbackArg<any>[]) => {
                    let index = 0;
                    this.types      = rarr[index++].data.Results;
                    this.imageTypes = rarr[index++].data.Results;
                    /*this.categories = rarr[index++].data.Results;
                    this.products   = rarr[index++].data.Results;
                    this.stores     = rarr[index++].data.Results;*/
                    this.cefConfig.featureSet.brands.enabled ? this.brands     = rarr[index++].data.Results : rarr[index++];
                    this.loadCategoryTree();
                    if (!this.brands.length) {
                        resolve();
                        return;
                    }
                    if (!this.record.Brands) {
                        this.record.Brands = [];
                    }
                    /* TODO: Wrap this in a setting
                    this.brands.forEach(x => {
                        const found = _.find(this.record.Brands, y => y.BrandID === x.ID || y.MasterID === x.ID);
                        if (found) {
                            return;
                        }
                        this.record.Brands.push(<api.BrandCategoryModel>{
                            // Base Properties
                            ID: 0,
                            CustomKey: null,
                            Active: true,
                            CreatedDate: new Date(),
                            UpdatedDate: null,
                            //
                            BrandID: x.ID,
                            CategoryID: this.record.ID || null,
                            //
                            IsVisibleInBrand: true
                        });
                    });
                    // */
                    resolve();
                }).catch(reject);
            });
        }
        loadNewRecord(): ng.IPromise<api.CategoryModel> {
            this.record = <api.CategoryModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // IHaveSeoBase Properties
                SeoUrl: null,
                SeoPageTitle: null,
                MetaKeyWords: null,
                MetaDescription: null,
                // IHaveOrderMinimums Properties
                MinimumOrderDollarAmount: null,
                MinimumOrderDollarAmountAfter: null,
                MinimumOrderDollarAmountWarningMessage: null,
                MinimumOrderDollarAmountOverrideFee: null,
                MinimumOrderDollarAmountOverrideFeeIsPercent: false,
                MinimumOrderDollarAmountOverrideFeeWarningMessage: null,
                MinimumOrderQuantityAmount: null,
                MinimumOrderQuantityAmountAfter: null,
                MinimumOrderQuantityAmountWarningMessage: null,
                MinimumOrderQuantityAmountOverrideFee: null,
                MinimumOrderQuantityAmountOverrideFeeIsPercent: false,
                MinimumOrderQuantityAmountOverrideFeeWarningMessage: null,
                // HaveAParentBase Properties
                HasChildren: false,
                // IHaveATypeBase Properties
                TypeID: 0,
                // Category Properties
                IsVisible: true,
                IncludeInMenu: true,
            };
            this.listenToAttributes();
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> {
            this.detailName = "Category";
            return this.$q.resolve();
        }
        loadRecordCall(id: number): ng.IHttpPromise<api.CategoryModel> {
            return this.cvApi.categories.GetCategoryByID({ ID: id, ExcludeProductCategories: true });
        }
        protected loadRecordActionAfterSuccess(result: api.CategoryModel): ng.IPromise<api.CategoryModel> {
            this.listenToAttributes();
            if (result.ParentID) {
                this.cvApi.categories.GetCategoryByID({ ID: result.ParentID, ExcludeProductCategories: true }).then(r => this.parent = r.data);
            }
            return this.$q.resolve(result);
        }
        createRecordCall(routeParams: api.CategoryModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.categories.CreateCategory(routeParams);
        }
        updateRecordCall(routeParams: api.CategoryModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.categories.UpdateCategory(routeParams);
        }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.categories.DeactivateCategoryByID(id);
        }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.categories.ReactivateCategoryByID(id);
        }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.categories.DeleteCategoryByID(id);
        }
        // Supportive Functions
        private listenToAttributes() {
            this.unbindAttributesChanged = this.unbindAttributesChanged
                || this.$scope.$on(this.cvServiceStrings.events.attributes.changed,
                    ($event: ng.IAngularEvent, changedList: widgets.IAttributeChangedEventArg[]) => {
                        if (!this.forms || !this.forms["Attributes"]) {
                            return;
                        }
                        if (!_.some(changedList,
                                x => x.property == "Value"
                                && x.newValue !== null
                                && x.oldValue !== undefined)) {
                            return;
                        }
                        this.forms["Attributes"].$setDirty();
                    });
        }

        // Parent Category Management Events
        categoryTree: kendo.ui.TreeView;
        categoryTreeOptions = <kendo.ui.TreeViewOptions>{
            checkboxes: <kendo.ui.TreeViewCheckboxes>{
                checkChildren: true,
                // NOTE: This is a specific inline template that is required in this format
                template: `<input type="checkbox" disabled ng-model="dataItem.IsSelfSelected" />`
            },
            // NOTE: This is a specific inline template that is required in this format
            template: `<button type="button" ng-click="categoryDetailCtrl.setParentCategory(dataItem)"`
                    + ` ng-disabled="dataItem.IsSelfSelected"`
                    + ` ng-class="{disabled : dataItem.IsSelfSelected}"`
                    + ` class="btn btn-xs btn-success"><i class="far fa-fw fa-plus"></i><span class="sr-only">Add</span></button>`
                + ` {{dataItem.ID | zeroPadNumber: 6}}: {{dataItem.Name}}<span ng-if="dataItem.CustomKey">  [{{dataItem.CustomKey}}]</span>`,
            loadOnDemand: true
        };
        selectedCategory: api.ProductCategorySelectorModel;
        // NOTE: This must remain an arrow function
        categorySelected = dataItem => { this.selectedCategory = dataItem; }
        categoryTreeData: kendo.data.HierarchicalDataSource;
        loadCategoryTree(): void {
            // Get all Categories with no parents, we'll lazy-load children afterward
            this.categoryTreeData = new kendo.data.HierarchicalDataSource(<kendo.data.HierarchicalDataSourceOptions>{
                transport: <kendo.data.DataSourceTransport>{
                    read: ((options): void => {
                        this.cvApi.categories.GetCategoryTree(<api.GetCategoryTreeDto>{
                            ParentID: (options.data as api.ProductCategorySelectorModel).ID || null,
                            IncludeChildrenInResults: false,
                            DisregardParents: false,
                            ProductID: 0
                        }).then(r => options.success(r.data));
                    }) as kendo.data.DataSourceTransportRead
                },
                schema: <kendo.data.HierarchicalDataSourceSchema>{
                    model: {
                        id: "ID",
                        hasChildren: "HasChildren"
                    }
                }
            });
        }
        // NOTE: This must remain an arrow function
        setParentCategory = (pcs: api.ProductCategorySelectorModel): void => {
            if (this.record.ID === pcs.ID) {
                console.warn("Can't set category as a parent of itself");
                return;
            }
            this.record.ParentID = pcs.ID;
            this.parent = null; // Clear it until the full data loads
            this.cvApi.categories.GetCategoryByID({ ID: pcs.ID, ExcludeProductCategories: true }).then(r => this.parent = r.data);
            this.forms["ParentCategory"].$setDirty();
        }
        // NOTE: This must remain an arrow function
        removeParent = (): void => {
            this.record.ParentID = null;
            this.record.Parent = null;
            this.parent = null;
            this.forms["ParentCategory"].$setDirty();
        }

        // Brand Management Events
        addBrand(): void {
            let brandID = 0;
            if (this.brands.length > 0) { brandID = this.brands[0].ID; }
            if (!this.record.Brands) {
                this.record.Brands = [];
            }
            this.record.Brands.push(<api.BrandCategoryModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: brandID,
                SlaveID: this.record.ID,
                SlaveKey: this.record.CustomKey,
            });
            this.forms["Brands"].$setDirty();
        }
        removeBrand(index: number): void {
            this.record.Brands.splice(index, 1);
            this.forms["Brands"].$setDirty();
        }
        // Image & Document Management Events
        removeImageNew(index: number): void {
            this.record.Images.splice(index, 1);
            this.forms["Images"].$setDirty();
        }
        removeFileNew(index: number): void {
            this.record.StoredFiles.splice(index, 1);
            this.forms["StoredFiles"].$setDirty();
        }
        // Constructor
        constructor(
                public    readonly $scope: ng.IScope,
                public    readonly $translate: ng.translate.ITranslateService,
                public    readonly $stateParams: ng.ui.IStateParamsService,
                public    readonly $state: ng.ui.IStateService,
                public    readonly $window: ng.IWindowService,
                public    readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                public    readonly cvApi: api.ICEFAPI,
                public    readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory,
                public    readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindAttributesChanged)) { this.unbindAttributesChanged(); }
            });
        }
    }

    adminApp.directive("categoryDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/inventory/categoryDetail.html", "ui"),
        controller: CategoryDetailController,
        controllerAs: "categoryDetailCtrl"
    }));
}
