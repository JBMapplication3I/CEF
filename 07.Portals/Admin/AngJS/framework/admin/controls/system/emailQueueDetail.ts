/**
* @file framework/admin/controls/system/emailQueueDetail.ts
* @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
* @desc Email Queue detail view class
 */
module cef.admin {
    class EmailQueueDetailController extends DetailBaseController<api.EmailQueueModel> {
        // Forced overrides
        detailName = "Email Queue";
        // Collections
        types: Array<api.TypeModel>;
        statuses: Array<api.StatusModel>;
        templates: Array<api.EmailTemplateModel>;
        // UI Data
        // <None>
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            this.cvApi.messaging.GetEmailTypes({ Active: true, AsListing: true }).then(r => this.types = r.data.Results);
            this.cvApi.messaging.GetEmailStatuses({ Active: true, AsListing: true }).then(r => this.statuses = r.data.Results);
            this.cvApi.messaging.GetEmailTemplates({ Active: true, AsListing: true }).then(r => this.templates = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.EmailQueueModel> {
            this.record = <api.EmailQueueModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                Name: null,
                Description: null,
                Body: null,
                Subject: null,
                TypeID: 0,
                StatusID: 0,
                IsHtml: false,
                Attempts: 0,
                HasError: false
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "EmailQueue"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.EmailQueueModel> { return this.cvApi.messaging.GetEmailQueueByID(id); }
        createRecordCall(routeParams: api.EmailQueueModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.messaging.CreateEmailQueue(routeParams); }
        updateRecordCall(routeParams: api.EmailQueueModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.messaging.UpdateEmailQueue(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.messaging.DeactivateEmailQueueByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.messaging.ReactivateEmailQueueByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.messaging.DeleteEmailQueueByID(id); }
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

    adminApp.directive("emailQueueDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        transclude: true,
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/emailQueueDetail.html", "ui"),
        controller: EmailQueueDetailController,
        controllerAs: "emailQueueDetailCtrl",
        bindToController: true
    }));
}
