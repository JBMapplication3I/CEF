/**
 * @file framework/admin/controls/accounts/badgeDetail.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc badge detail class
 */
module cef.admin.controls.accounts {
    class BadgeDetailController extends DetailBaseController<api.BadgeModel> {
        // Forced overrides
        detailName = "Badge";
        // Collections
        badgeTypes: api.TypeModel[] = [];
        imageTypes: api.TypeModel[] = [];
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 50 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.cvApi.badges.GetBadgeTypes(standardDto).then(r => this.badgeTypes = r.data.Results);
            this.cvApi.badges.GetBadgeImageTypes(standardDto).then(r => this.imageTypes = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.BadgeModel> {
            this.record = <api.BadgeModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SerializableAttributes: { },
                Hash: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // IHaveImages Properties
                Images: [],
                // IHaveAType Properties
                TypeID: 0,
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Badge"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.BadgeModel> { return this.cvApi.badges.GetBadgeByID(id); }
        createRecordCall(routeParams: api.BadgeModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.badges.CreateBadge(routeParams); }
        updateRecordCall(routeParams: api.BadgeModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.badges.UpdateBadge(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.badges.DeactivateBadgeByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.badges.ReactivateBadgeByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.badges.DeleteBadgeByID(id); }
        // Supportive Functions
        // <None>
        // Image & Document Management Events V2
        removeImageNew(index: number) {
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
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory,
                protected readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("badgeDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/badgeDetail.html", "ui"),
        controller: BadgeDetailController,
        controllerAs: "badgeDetailCtrl"
    }));
}
