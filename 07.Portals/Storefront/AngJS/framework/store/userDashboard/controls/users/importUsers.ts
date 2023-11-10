/**
 * @file framework/store/userDashboard/controls/users/importUsers.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Control for importing users to the current account
 */
module cef.store.userDashbord.controls.users {
    class ImportUsersController extends core.TemplatedControllerBase {
        // Properties
        StoredFiles: api.AmAStoredFileRelationshipTableModel[] = [];
        dontRedirect: boolean;
        // Functions
        import(): void {
            if (!this.StoredFiles || !this.StoredFiles.length) {
                return;
            }
            this.setRunning();
            const dto = <api.ImportUsersFromExcelToCurrentAccountDto>{
                FileName: this.StoredFiles[this.StoredFiles.length - 1].Slave.FileName
            };
            this.cvApi.contacts.ImportUsersFromExcelToCurrentAccount(dto).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, angular.toJson(r), r && r.data && r.data.Messages);
                    return;
                }
                this.StoredFiles = [];
                this.finishRunning();
                this.$rootScope.$broadcast(this.cvServiceStrings.events.users.importedToAccount, r.data.Result);
                if (this.dontRedirect) {
                    return;
                }
                this.$state.go("userDashboard.users.list");
            }, result => this.finishRunning(true, result))
            .catch(reason => this.finishRunning(true, reason));
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $state: ng.ui.IStateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefLocalAdminImportUsers", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { dontRedirect: "=?" },
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/users/importUsers.html", "ui"),
        controller: ImportUsersController,
        controllerAs: "importUsersCtrl",
        bindToController: true
    }));
}
