// <copyright file="uiTranslationDetail.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>uiTranslation detail class</summary>
module cef.admin {
    class UiTranslationDetailController extends DetailBaseController<api.UiTranslationModel> {
        // Forced overrides
        detailName = "UI Translation";
        // Collections
        // <None>
        // UI Data
        // <None>
        // Required Functions
        loadNewRecord(): ng.IPromise<api.UiTranslationModel> {
            this.record = <api.UiTranslationModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                Name: null,
                Description: null,
                UiKeyID: 0,
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "UI Translation"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.UiTranslationModel> { return this.cvApi.globalization.GetUiTranslationByID(id); }
        createRecordCall(routeParams: api.UiTranslationModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.globalization.CreateUiTranslation(routeParams); }
        updateRecordCall(routeParams: api.UiTranslationModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.globalization.UpdateUiTranslation(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.globalization.DeactivateUiTranslationByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.globalization.ReactivateUiTranslationByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.globalization.DeleteUiTranslationByID(id); }
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

    adminApp.directive("uiTranslationDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/uiTranslationDetail.html", "ui"),
        controller: UiTranslationDetailController,
        controllerAs: "uiTranslationDetailCtrl"
    }));
}
