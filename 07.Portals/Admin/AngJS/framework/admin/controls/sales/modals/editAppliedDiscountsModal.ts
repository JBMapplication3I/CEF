module cef.admin.controls.sales.modals {
    export class EditAppliedDiscountsModalController extends core.TemplatedControllerBase {
        // EditAppliedDiscountsModalController Properties
        forms: {
            edit: ng.IFormController
        };
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private record: api.SalesCollectionBaseModel,
                private discountItems: Array<any>) {
            super(cefConfig);
            this.onOpen();
        }
        // Functions
        ok() {
            this.$uibModalInstance.close(/*this.newValue*/);
        }
        cancel() {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        onOpen() {
            // Do Nothing
        }
    }
}
