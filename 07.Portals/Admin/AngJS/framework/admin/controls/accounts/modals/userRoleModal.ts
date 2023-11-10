module cef.admin.controls.accounts.modals {
    class AccountUserRoleController extends core.TemplatedControllerBase {
        // Properties
        roles: cefalt.admin.Dictionary<number>;
        role: api.RoleForUserModel;
        isVisible: boolean;
        // Functions
        private createNewInstance() {
            this.role = {
                RoleId: -1,
                UserId: -1,
                StartDate: null,
                EndDate: null,
                Name: null
            };
        }
        private loadCollections(): void {
            this.cvApi.authentication.GetRoles().then(r => this.roles = r.data);
        }
        private save(): void {
            this.setRunning();
            let promise: (dto: any) => ng.IHttpPromise<api.CEFActionResponse>;
            let dto: any;
            switch (this.dgType) {
                case "Add": {
                    promise = this.cvApi.authentication.AssignRoleToUser;
                    dto = {
                        UserId: this.role.UserId,
                        RoleId: this.role.RoleId,
                        StartDate: this.role.StartDate,
                        EndDate: this.role.EndDate
                    };
                    break;
                }
                case "Update": {
                    promise = this.cvApi.authentication.UpdateRoleForUser;
                    dto = {
                        UserId: this.role.UserId,
                        RoleId: this.role.RoleId,
                        StartDate: this.role.StartDate,
                        EndDate: this.role.EndDate
                    };
                    break;
                }
                case "Remove": {
                    promise = this.cvApi.authentication.RemoveRoleFromUser;
                    dto = {
                        UserId: this.role.UserId,
                        RoleId: this.role.RoleId
                    };
                    break;
                }
                default: {
                    throw Error("Unknown modal type");
                }
            }
            promise(dto).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, null, r && r.data && r.data.Messages);
                    return;
                }
                this.$rootScope.$broadcast(this.cvServiceStrings.events.users.updated);
                this.close();
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        private close(): void {
            this.$rootScope.$broadcast(this.cvServiceStrings.events.users.refreshRoles);
            this.role = null;
            this.$uibModalInstance.dismiss("cancel");
        }
        title: string;
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly dgType: string,
                private readonly userId: number,
                private readonly titleKey?: string,
                private readonly roleId?: number) {
            super(cefConfig);
            switch (this.dgType) {
                case "Add": {
                    if (titleKey) {
                        let translated = "Add Role";
                        $translate(titleKey).then(t => translated = t).finally(() => this.title = translated);
                    } else {
                        this.title = "Add Role";
                    }
                    break;
                }
                case "Update": {
                    if (titleKey) {
                        let translated = "Update Role";
                        $translate(titleKey).then(t => translated = t).finally(() => this.title = translated);
                    } else {
                        this.title = "Update Role";
                    }
                    break;
                }
                case "Remove": {
                    if (titleKey) {
                        let translated = "Remove Role";
                        $translate(titleKey).then(t => translated = t).finally(() => this.title = translated);
                    } else {
                        this.title = "Remove Role";
                    }
                    break;
                }
            }
            this.createNewInstance();
            this.role.UserId = userId;
            if (roleId) { this.role.RoleId = roleId; }
            this.loadCollections();
        }
    }

    adminApp.controller("AccountUserRoleController", AccountUserRoleController);

    adminApp.directive("cvAccountUserRoleModal", ($filter: ng.IFilterService, $uibModal: ng.ui.bootstrap.IModalService, cvServiceStrings: services.IServiceStrings): ng.IDirective => ({
        restrict: "EA",
        scope: { dgType: "=", userId: "=", roleId: "=?", titleKey: "@?" },
        transclude: true, // Required
        template: `<button type="button" class="btn btn-info" ng-click="Show()"><ng-transclude></ng-transclude></button>`,
        replace: true, // Required
        link(scope: any) {
            scope.Show = () => {
                $uibModal.open({
                    templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/modals/userRoleModal.html", "ui"),
                    controller: "AccountUserRoleController",
                    controllerAs: "userRoleDetails",
                    size: cvServiceStrings.modalSizes.sm,
                    resolve: {
                        userId: () => scope.userId,
                        roleId: () => scope.roleId,
                        titleKey: () => scope.titleKey,
                        dgType: () => scope.dgType
                    }
                });
            };
        }
    }));
}
