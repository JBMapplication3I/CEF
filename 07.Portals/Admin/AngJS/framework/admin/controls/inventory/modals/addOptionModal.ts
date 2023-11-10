/**
 * @file addOptionModal.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Add Option Modal class
 */
module cef.admin.controls.inventory.modals {
    export class AddOptionModalController extends core.TemplatedControllerBase {
        // Properties
        forms: {
            edit: ng.IFormController
        };
        value: string;
        uofm: string;
        sortOrder: number;
        // Functions
        ok(): void {
            this.$uibModalInstance.close({
                value: this.value,
                uofm: this.uofm,
                sortOrder: this.sortOrder
            });
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        onOpen(): void {
            // Do Nothing
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) {
            super(cefConfig);
            this.onOpen();
        }
    }
}
