/**
 * @file framework/admin/purchasing/purchase.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart
 */
module cef.admin.purchasing {
    class PurchaseController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        lookupKey: api.CartByIDLookupKey;
        // Properties
        // <None>
        // Convenience Properties (reduces HTML size)
        get service(): services.IPurchaseService {
            return this && this.cvPurchaseService;
        }
        // Functions
        protected goToStep(stepIndex: number): void {
            this.consoleDebug(`PurchaseController.goToStep(stepIndex: ${stepIndex})`);
            this.setRunning();
            this.cvPurchaseService.activateStep(this.lookupKey, stepIndex).then(success => {
                if (!success) {
                    this.finishRunning(true,
                        `Activating step ${stepIndex} for ${this.lookupKey.toString()} failed`);
                    return;
                }
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        protected submit(lookupKey: api.CartByIDLookupKey, step: steps.IPurchaseStep): void {
            const debug = `PurchaseController.submit(lookupKey: "${lookupKey.toString()
                }", step: "${step && step.name}")`;
            this.consoleDebug(debug);
            this.setRunning();
            /*
            if (this.interval) {
                this.$interval.cancel(this.interval);
            }
            */
            step.submit(lookupKey).then(success => {
                if (!success) {
                    this.consoleDebug(`${debug} An error occurred in step submit, it was not successful`);
                    this.finishRunning(
                        true,
                        "An error occurred in step submit, it was not successful");
                    return;
                }
                this.consoleDebug(`${debug} Submit current step succeeded, Going to next step`);
                step.complete = true;
                this.goToStep(this.cvPurchaseService.activeStep[this.lookupKey.toString()] + 1);
            }).catch(reason => this.finishRunning(
                true,
                reason || "An error occurred in step submit, it was not successful"));
        }
        // Events
        // <None>
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvPurchaseService: services.IPurchaseService) {
            super(cefConfig);
            this.consoleDebug(`PurchaseController.ctor()`);
            this.setRunning();
            this.cvPurchaseService.building[this.lookupKey.toString()] = true;
            this.cvPurchaseService.initialize(this.lookupKey)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
    }

    adminApp.directive("cefPurchase", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/purchase.html", "ui"),
        controller: PurchaseController,
        controllerAs: "pCtrl",
        bindToController: true
    }));
}
