/**
 * @file framework/admin/controls/system.logDetail.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Log detail class
 */
module cef.admin {
    class LogDetailController extends DetailBaseController<api.EventLogModel> {
        // Forced overrides
        detailName = "Log";
        // Collections
        // <None>
        // UI Data
        // <None>
        // Required Functions
        loadNewRecord(): ng.IPromise<api.EventLogModel> {
            this.record = <api.EventLogModel>{
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
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Log"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.EventLogModel> { return this.cvApi.structure.GetEventLogByID(id); }
        createRecordCall(routeParams: api.EventLogModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.structure.CreateEventLog(routeParams); }
        updateRecordCall(routeParams: api.EventLogModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.structure.UpdateEventLog(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.structure.DeactivateEventLogByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.structure.ReactivateEventLogByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.structure.DeleteEventLogByID(id); }
        loadRecordActionAfterSuccess(result: api.EventLogModel): ng.IPromise<api.EventLogModel> {
            if (!result.Description) { return this.$q.resolve(result); }
            result.Description = result.Description
                // Exchange new lines, or escaped new lines with break tags
                .replace(/(\\r|\r)?(\\n|\n)/g, "<br/>")
                // Bold lines that contain file names with line numbers for easy spotting
                .replace(/([A-Za-z0-9_\.]+)\:line\s*(\d+)/g, "<b>$1:line $2</b>")
                // Bold some key lines for visual separation
                .replace(/(Errors|Source|Inner Exception|Stack Trace)\:?\s*\<br/g, "<b>$1:</b><br")
                ;
            return this.$q.resolve(result);
        }
        // Supportive Functions
        // <None>

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

    adminApp.directive("logDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/logDetail.html", "ui"),
        controller: LogDetailController,
        controllerAs: "logDetailCtrl"
    }));
}
