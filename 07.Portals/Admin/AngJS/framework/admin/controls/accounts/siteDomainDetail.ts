/**
 * @file framework/admin/controls/accounts/siteDomainDetail.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Site domain detail class
 */
module cef.admin.controls.accounts {
    class SiteDomainDetailController extends DetailBaseController<api.SiteDomainModel> {
        // Forced overrides
        detailName = "Site Domain";
        // Collections
        brands: api.BrandModel[] = [];
        socialProviders: api.SocialProviderModel[] = [];
        // UI Data
        // <None>
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            this.cvApi.brands.GetBrands().then(r => this.brands = r.data.Results);
            this.cvApi.stores.GetSocialProviders().then(r => this.socialProviders = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.SiteDomainModel> {
            this.record = <api.SiteDomainModel>{
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
                HeaderContent: null,
                FooterContent: null,
                SideBarContent: null,
                CatalogContent: null,
                Url: null,
                AlternateUrl1: null,
                AlternateUrl2: null,
                AlternateUrl3: null,
                Brands: [],
                SiteDomainSocialProviders: []
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Site Domain"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.SiteDomainModel> { return this.cvApi.stores.GetSiteDomainByID(id); }
        createRecordCall(routeParams: api.SiteDomainModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.stores.CreateSiteDomain(routeParams); }
        updateRecordCall(routeParams: api.SiteDomainModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.stores.UpdateSiteDomain(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.stores.DeactivateSiteDomainByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.stores.ReactivateSiteDomainByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.stores.DeleteSiteDomainByID(id); }
        // Supportive Functions
        // <None>

        // Brand Mangement Events
        addBrand(): void {
            if (!this.record.Brands) {
                this.record.Brands = [];
            }
            this.record.Brands.push(<api.BrandSiteDomainModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                BrandID: 0,
                SiteDomainID: 0
            });
            this.forms["Brands"].$setDirty();
        }
        removeBrand(index: number): void {
            this.record.Brands.splice(index, 1);
            this.forms["Brands"].$setDirty();
        }
        // Social Provider Management Events
        addSocialProvider(): void {
            if (!this.record.SiteDomainSocialProviders) {
                this.record.SiteDomainSocialProviders = [];
            }
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
            this.forms["SocialProviders"].$setDirty();
        }
        removeSocialProvider(index: number): void {
            this.record.SiteDomainSocialProviders.splice(index, 1);
            this.forms["SocialProviders"].$setDirty();
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

    adminApp.directive("siteDomainDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/siteDomainDetail.html", "ui"),
        controller: SiteDomainDetailController,
        controllerAs: "siteDomainDetailCtrl"
    }));
}
