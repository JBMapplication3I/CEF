// <copyright file="socialProviderDetail.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>social provider detail class</summary>
module cef.admin.controls.accounts {
    class SocialProviderDetailController extends DetailBaseController<api.SocialProviderModel> {
        // Forced overrides
        detailName = "Social Provider";
        // Collections
        siteDomains: api.SiteDomainModel[] = [];
        // UI Data
        // <None>
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            this.cvApi.stores.GetSiteDomains().then(r => this.siteDomains = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.SocialProviderModel> {
            this.record = <api.SocialProviderModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                //
                Url: null,
                UrlFormat: null,
                //
                SiteDomainSocialProviders: []
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Social Provider"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.SocialProviderModel> { return this.cvApi.stores.GetSocialProviderByID(id); }
        createRecordCall(routeParams: api.SocialProviderModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.stores.CreateSocialProvider(routeParams); }
        updateRecordCall(routeParams: api.SocialProviderModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.stores.UpdateSocialProvider(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.stores.DeactivateSocialProviderByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.stores.ReactivateSocialProviderByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.stores.DeleteSocialProviderByID(id); }
        // Supportive Functions
        // <None>

        // Site Domain Management Events
        addSiteDomain(): void {
            if (!this.record.SiteDomainSocialProviders) { this.record.SiteDomainSocialProviders = []; }
            this.record.SiteDomainSocialProviders.push(<api.SiteDomainSocialProviderModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                SocialProviderID: 0,
                SiteDomainID: 0,
                //
                Script: null,
                UrlValues: null
            });
            this.forms["SiteDomains"].$setDirty();
        }
        removeSiteDomain(index: number): void {
            this.record.SiteDomainSocialProviders.splice(index, 1);
            this.forms["SiteDomains"].$setDirty();
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
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory,
                protected readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("socialProviderDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/socialProviderDetail.html", "ui"),
        controller: SocialProviderDetailController,
        controllerAs: "socialProviderDetailCtrl"
    }));
}
