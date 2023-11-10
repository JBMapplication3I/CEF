// <copyright file="emailTemplateDetail.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>email template detail class</summary>
module cef.admin {
    class EmailTemplateDetailController extends DetailBaseController<api.EmailTemplateModel> {
        // Forced overrides
        detailName = "Email Template";
        // Collections
        // UI Data
        // <None>
        // Required Functions
        loadNewRecord(): ng.IPromise<api.EmailTemplateModel> {
            this.record = <api.EmailTemplateModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                Name: null,
                Description: null,
                Body: null,
                Subject: null
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "EmailTemplate"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.EmailTemplateModel> { return this.cvApi.messaging.GetEmailTemplateByID(id); }
        createRecordCall(routeParams: api.EmailTemplateModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.messaging.CreateEmailTemplate(routeParams); }
        updateRecordCall(routeParams: api.EmailTemplateModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.messaging.UpdateEmailTemplate(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.messaging.DeactivateEmailTemplateByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.messaging.ReactivateEmailTemplateByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.messaging.DeleteEmailTemplateByID(id); }
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

    adminApp.directive("emailTemplateDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        transclude: true,
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/emailTemplateDetail.html", "ui"),
        controller: EmailTemplateDetailController,
        controllerAs: "emailTemplateDetailCtrl",
        bindToController: true
    }));
}
