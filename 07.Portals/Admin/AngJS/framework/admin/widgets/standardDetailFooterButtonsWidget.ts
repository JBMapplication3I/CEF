/**
 * @file framework/admin/widgets/standardDetailFooterButtons.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Standard detail footer buttons class.
 */
module cef.admin {
    class StandardDetailFooterButtonsController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        /** @desc The state to go back to on Save or Cancel */
        returnState: string;
        /** @desc The master controller */
        masterController: DetailBaseController<api.BaseModel>;
        /**
         * @desc used to prepend Update, Create, Deactivate, etc
         * permissions checks for showing individual buttons
         * @example 'Admin.Inventory.Products'
         */
        permissionsBase: string;
        /**
         * @desc if set, will hide the recycle/restore options
         * (use with record types that can't do this)
         * @example false
         */
        hideRecycle: boolean;
        // Properties
        get includeSave(): boolean {
            if (!this.masterController.record) {
                return false;
            }
            return this.masterController.record.ID > 0
                ? this.cvSecurityService.hasPermission(this.permissionsBase + '.Update')
                : this.cvSecurityService.hasPermission(this.permissionsBase + '.Create');
        }
        get includeSaveAndClose(): boolean {
            if (!this.masterController.record) {
                return false;
            }
            return this.masterController.record.ID > 0
                ? this.cvSecurityService.hasPermission(this.permissionsBase + '.Update')
                : this.cvSecurityService.hasPermission(this.permissionsBase + '.Create');
        }
        get includeDeactivate(): boolean {
            return !this.hideRecycle
                && this.masterController.record
                && this.masterController.record.ID > 0
                && this.masterController.record.Active
                && this.cvSecurityService.hasPermission(this.permissionsBase + '.Deactivate');
        }
        get includeReactivate(): boolean {
            return !this.hideRecycle
                && this.masterController.record
                && this.masterController.record.ID > 0
                && !this.masterController.record.Active
                && this.cvSecurityService.hasPermission(this.permissionsBase + '.Reactivate');
        }
        get includeDelete(): boolean {
            return this.masterController.record
                && this.masterController.record.ID > 0
                && this.cvSecurityService.hasPermission(this.permissionsBase + '.Delete');
        }
        // Functions
        validForms(): boolean {
            if (!this.masterController.forms) {
                return false;
            }
            var valid = true;
            Object.keys(this.masterController.forms).forEach(key => {
                if (!this.masterController.forms[key].$valid) {
                    valid = false;
                    return false;
                }
                return true;
            });
            return valid;
        }
        dirtyForms(): boolean {
            if (!this.masterController.forms) {
                return false;
            }
            var dirty = false;
            Object.keys(this.masterController.forms).forEach(key => {
                if (this.masterController.forms[key]
                    && this.masterController.forms[key].$dirty) {
                    dirty = true;
                    return false;
                }
                return true;
            });
            return dirty;
        }
        save(): void {
            this.masterController.saveRecord();
        }
        saveAndClose(): void {
            this.masterController.saveRecord(this.returnState);
        }
        deactivate(): void {
            this.masterController.deactivateRecord();
        }
        reactivate(): void {
            this.masterController.reactivateRecord();
        }
        delete(): void {
            this.masterController.deleteRecord(this.returnState);
        }
        backCancel(): void {
            if (!this.dirtyForms()) {
                this.masterController.cancel(this.returnState);
                return;
            }
            // Ask the user if they really want to leave
            this.cvConfirmModalFactory(
                    this.$translate("ui.admin.controls.backCancel.UnsavedChangesConfirmation.Message"))
                .then(r => {
                    if (r) {
                        this.masterController.cancel(this.returnState);
                    }
                });
        }
        // Constructor
        constructor(
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSecurityService: services.ISecurityService,
                private readonly cvConfirmModalFactory: modals.IConfirmModalFactory) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefStandardDetailFooterButtons", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            returnState: "@",
            masterController: "=",
            permissionsBase: "@",
            hideRecycle: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/standardDetailFooterButtonsWidget.html", "ui"),
        controller: StandardDetailFooterButtonsController,
        controllerAs: "sdfbCtrl",
        bindToController: true
    }));
}
