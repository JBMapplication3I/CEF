// <copyright file="currencyDetail.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>currency detail class</summary>
module cef.admin {
    class CurrencyDetailController extends DetailBaseController<api.CurrencyModel> {
        // Forced overrides
        detailName = "Currency";
        // Collections
        imageTypes: api.TypeModel[] = [];
        // UI Data
        // <None>
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 50 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.cvApi.currencies.GetCurrencyImageTypes(standardDto).then(r => this.imageTypes = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.CurrencyModel> {
            this.record = <api.CurrencyModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                SerializableAttributes: {},
                Hash: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // Currency Properties
                UnicodeSymbolValue: null,
                HtmlCharacterCode: null,
                RawCharacter: null,
                // IHaveImagesBase
                Images: [],
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Currency"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.CurrencyModel> { return this.cvApi.currencies.GetCurrencyByID(id); }
        createRecordCall(routeParams: api.CurrencyModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.currencies.CreateCurrency(routeParams); }
        updateRecordCall(routeParams: api.CurrencyModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.currencies.UpdateCurrency(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.currencies.DeactivateCurrencyByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.currencies.ReactivateCurrencyByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.currencies.DeleteCurrencyByID(id); }
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

    adminApp.directive("currencyDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/currencyDetail.html", "ui"),
        controller: CurrencyDetailController,
        controllerAs: "currencyDetailCtrl"
    }));
}
