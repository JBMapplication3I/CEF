/**
 * @file framework/admin/controls/system.languageDetail.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Log detail class
 */
module cef.admin {
    class LanguageDetailController extends DetailBaseController<api.LanguageModel> {
        // Forced overrides
        detailName = "Language";
        // Collections
        imageTypes: api.TypeModel[] = [];
        // UI Data
        // <None>
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 50 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.cvApi.globalization.GetLanguageImageTypes(standardDto).then(r => this.imageTypes = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.LanguageModel> {
            this.record = <api.LanguageModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                SerializableAttributes: { },
                // NameableBase Properties
                Name: null,
                Description: null,
                // Language Properties
                Locale: null,
                UnicodeName: null,
                // IHaveImagesBase
                Images: [],
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Language"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.LanguageModel> { return this.cvApi.globalization.GetLanguageByID(id); }
        createRecordCall(routeParams: api.LanguageModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.globalization.CreateLanguage(routeParams); }
        updateRecordCall(routeParams: api.LanguageModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.globalization.UpdateLanguage(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.globalization.DeactivateLanguageByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.globalization.ReactivateLanguageByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.globalization.DeleteLanguageByID(id); }
        // Supportive Functions

        // Image Management Events V2
        removeImageNew(index: number): void {
            this.record.Images.splice(index, 1);
            this.forms["Images"].$setDirty();
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

    adminApp.directive("languageDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/languageDetail.html", "ui"),
        controller: LanguageDetailController,
        controllerAs: "languageDetailCtrl"
    }));
}
