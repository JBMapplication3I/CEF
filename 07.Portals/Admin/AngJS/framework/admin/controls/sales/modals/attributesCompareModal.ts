module cef.admin.controls.sales.modals {
    export class AttributesCompareModalController extends core.TemplatedControllerBase {
        // Properties
        // <None>
        // Functions
        ok(): void {
            this.$uibModalInstance.close(true);
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
                private readonly $q: ng.IQService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private originalItem: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>,
                private responseItem: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>) {
            super(cefConfig);
            this.onOpen();
        }
    }
}
