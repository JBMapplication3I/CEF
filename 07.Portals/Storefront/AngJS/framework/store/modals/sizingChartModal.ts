/**
 * @file framework/store/modals/sizingChartModal.ts
 * @desc Sizing Chart Modal Class
 */
 module cef.store.modals {
    export class SizingChartModalController extends core.TemplatedControllerBase {
      // Properties
      ok(): void {
        this.$uibModalInstance.close();
      }
      // Constructor
      constructor(
        private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
        protected readonly cvApi: api.ICEFAPI,
        protected readonly $scope: ng.IScope,
        protected readonly cefConfig: core.CefConfig) {
        super(cefConfig);
      }
    }
  }
