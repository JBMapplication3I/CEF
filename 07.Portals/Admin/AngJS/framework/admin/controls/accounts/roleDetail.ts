// <copyright file="roleDetail.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>role detail class</summary>
module cef.admin.controls.accounts {
    export class RoleDetailController extends DetailBaseController<api.RoleUserModel> {
        // Forced overrides
        detailName = "Role";
        // Collections
        permissions: api.PermissionModel[] = [];
        // UI Data
        oldName: string;
        includedPermissions: api.PermissionModel[] = [];
        notIncludedPermissions: api.PermissionModel[] = [];
        selectedIncludedPermissions: api.PermissionModel[] = [];
        selectedNotIncludedPermissions: api.PermissionModel[] = [];
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            this.cvApi.authentication.GetPermissions().then(r => {
                this.permissions = r.data.sort((a, b) => a.Name > b.Name ? 1 : 0);
                this.$scope.$evalAsync(() => this.reloadPermissionsUI());
            });
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.RoleUserModel> {
            this.record = <api.RoleUserModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                Permissions: []
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Role"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.RoleUserModel> { return this.cvApi.authentication.GetRole(id); }
        createRecordCall(routeParams?: api.RoleUserModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.authentication.CreateRole({ Name: this.record.CustomKey, IncludedPermissions: this.includedPermissions }).then(r => {
                return this.$q.resolve({ data: <api.CEFActionResponseT<number>>{ ActionSucceeded: r.data && true, Result: r.data.ID } });
            });
        }
        updateRecordCall(routeParams?: api.RoleUserModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.authentication.UpdateRole({ OldName: this.oldName, NewName: this.record.CustomKey, IncludedPermissions: this.includedPermissions }).then(r => {
                return this.$q.resolve({ data: <api.CEFActionResponseT<number>>{ ActionSucceeded: r.data && true, Result: r.data.ID } });
            });
        }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { this.cvMessageModalFactory(this.$translate("ui.admin.controls.accounts.roleDetail.RolesCannotBeRecycled")); return this.$q.reject(); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { this.cvMessageModalFactory(this.$translate("ui.admin.controls.accounts.roleDetail.RolesCannotBeRestored")); return this.$q.reject(); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.authentication.DeleteRole({ Name: this.record.CustomKey }); }
        loadRecordActionAfterSuccess(result: api.RoleUserModel): ng.IPromise<api.RoleUserModel> {
            this.oldName = this.record.CustomKey;
            this.includedPermissions = this.record.Permissions;
            this.$scope.$evalAsync(() => this.reloadPermissionsUI());
            return this.$q.resolve(result);
        }
        // Supportive Functions
        // Permission Management Events
        includeMorePermissions(): void {
            this.selectedNotIncludedPermissions.forEach(value => this.includedPermissions.push(value));
            this.reloadPermissionsUI();
        }
        includeLessPermissions(): void {
            this.selectedIncludedPermissions.forEach(
                value => this.includedPermissions.splice(this.includedPermissions.indexOf(value), 1));
            this.reloadPermissionsUI();
        }
        reloadPermissionsUI(): void {
            this.notIncludedPermissions = _.filter(this.permissions, (value) => !_.some(this.includedPermissions, v => value.Name === v.Name));
        }
        //addPermission = (id: number, name: string) => { this.includedPermissions.push(<api.PermissionModel>{ Id: id, Name: name }); }
        //removePermission = (index: number) => { this.includedPermissions.splice(index, 1); }
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
                protected readonly cvMessageModalFactory: admin.modals.IMessageModalFactory,
                protected readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("roleDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/roleDetail.html", "ui"),
        controller: RoleDetailController,
        controllerAs: "roleDetailCtrl"
    }));
}
