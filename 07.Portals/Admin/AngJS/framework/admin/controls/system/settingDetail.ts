// <copyright file="settingDetail.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>setting detail class</summary>
module cef.admin {
    class SettingDetailController extends DetailBaseController<api.SettingModel> {
        // Forced overrides
        detailName = "Setting";
        // Collections
        types: api.TypeModel[];
        stores: api.StoreModel[] = [];
        // UI Data
        // <None>
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            this.cvApi.structure.GetSettingTypes({ Active: true, AsListing: true }).then(r => this.types = r.data.Results);
            this.cvApi.stores.GetStores({ Active: true, AsListing: true }).then(r => this.stores = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.SettingModel> {
            this.record = <api.SettingModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                Name: null,
                Description: null,
                TypeID: 0,
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Setting"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.SettingModel> { return this.cvApi.structure.GetSettingByID(id); }
        createRecordCall(routeParams: api.SettingModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.structure.CreateSetting(routeParams); }
        updateRecordCall(routeParams: api.SettingModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.structure.UpdateSetting(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.structure.DeactivateSettingByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.structure.ReactivateSettingByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.structure.DeleteSettingByID(id); }
        // Supportive Functions
        updateType = () => {
            angular.forEach(this.types, value => {
                if (value.ID === this.record.TypeID) {
                    this.record.Type = value;
                    return false;
                }
                return true;
            });
        }
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

    adminApp.directive("settingDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/settingDetail.html", "ui"),
        controller: SettingDetailController,
        controllerAs: "settingDetailCtrl"
    }));
}
