module cef.admin.controls.sales.modals {
    export class EditATotalModalController extends core.TemplatedControllerBase {
        // EditATotalModalController Properties
        forms: {
            edit: ng.IFormController
        };
        newValue: number;
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly originalValue: number,
                private property: string) {
            super(cefConfig);
            this.onOpen();
        }
        // Functions
        ok() {
            this.$uibModalInstance.close(this.newValue);
        }
        cancel() {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        onOpen() {
            this.newValue = this.originalValue;
        }
    }
}
