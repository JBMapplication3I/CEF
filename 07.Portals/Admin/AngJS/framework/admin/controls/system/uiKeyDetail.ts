// <copyright file="uiKeyDetail.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>uiKey detail class</summary>
module cef.admin {
    class UiKeyDetailController extends DetailBaseController<api.UiKeyModel> {
        // Forced overrides
        detailName = "UI Key";
        // Collections
        // <None>
        // UI Data
        // <None>
        // Required Functions
        loadNewRecord(): ng.IPromise<api.UiKeyModel> {
            this.record = <api.UiKeyModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                Name: null,
                Description: null,
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "UI Key"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.UiKeyModel> { return this.cvApi.globalization.GetUiKeyByID(id); }
        createRecordCall(routeParams: api.UiKeyModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.globalization.CreateUiKey(routeParams); }
        updateRecordCall(routeParams: api.UiKeyModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.globalization.UpdateUiKey(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.globalization.DeactivateUiKeyByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.globalization.ReactivateUiKeyByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.globalization.DeleteUiKeyByID(id); }
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

    adminApp.directive("uiKeyDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/uiKeyDetail.html", "ui"),
        controller: UiKeyDetailController,
        controllerAs: "uiKeyDetailCtrl"
    }));
}
