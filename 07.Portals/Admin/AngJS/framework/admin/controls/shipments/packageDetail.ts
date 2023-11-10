module cef.admin.controls.shipments {
    class PackageDetailController extends DetailBaseController<api.PackageModel> {
        // Forced overrides
        detailName = "Package";
        // Collections
        types: api.TypeModel[] = [];
        // UI Data
        // <None>
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            this.cvApi.shipping.GetPackageTypes({ Active: true, AsListing: true }).then(r => this.types = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.PackageModel> {
            this.record = <api.PackageModel>{
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                Name: null,
                Description: null,
                ID: 0,
                Depth: null,
                DepthUnitOfMeasure: "in",
                Height: null,
                HeightUnitOfMeasure: "in",
                Width: null,
                WidthUnitOfMeasure: "in",
                Weight: null,
                WeightUnitOfMeasure: "lbs",
                DimensionalWeight: null,
                DimensionalWeightUnitOfMeasure: "lbs",
                IsCustom: false,
                PackageQuantity: null,
                TypeID: 0
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Package"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.PackageModel> { return this.cvApi.shipping.GetPackageByID(id); }
        createRecordCall(routeParams: api.PackageModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.shipping.CreatePackage(routeParams); }
        updateRecordCall(routeParams: api.PackageModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.shipping.UpdatePackage(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.shipping.DeactivatePackageByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.shipping.ReactivatePackageByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.shipping.DeletePackageByID(id); }
        // Supportive Functions
        // <None>
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $state: ng.ui.IStateService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
                protected readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("packageDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/shipments/packageDetail.html", "ui"),
        controller: PackageDetailController,
        controllerAs: "packageDetailCtrl"
    }));
}
