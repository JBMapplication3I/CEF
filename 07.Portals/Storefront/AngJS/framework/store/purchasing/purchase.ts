/**
 * @file framework/store/purchasing/purchase.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart
 */
module cef.store.purchasing {
    class PurchaseController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        cartType: string;
        timeRemainingStr: string = "15:00";
        timeRemaining: number = 900000;
        interval: any;
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
            this.cvPurchaseService.activateStep(this.cartType, stepIndex).then(success => {
                if (!success) {
                    this.finishRunning(true,
                        `Activating step ${stepIndex} for ${this.cartType} failed`);
                    return;
                }
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        protected submit(cartType: string, step: steps.IPurchaseStep): void {
            const debug = `PurchaseController.submit(cartType: "${cartType
                }", step: "${step && step.name}")`;
            this.consoleDebug(debug);
            this.setRunning();
            if (this.interval) {
                this.$interval.cancel(this.interval);
            }
            step.submit(cartType).then(success => {
                if (!success) {
                    this.consoleDebug(`${debug} An error occurred in step submit, it was not successful`);
                    this.finishRunning(
                        true,
                        "An error occurred in step submit, it was not successful");
                    return;
                }
                this.consoleDebug(`${debug} Submit current step succeeded, Going to next step`);
                step.complete = true;
                this.goToStep(this.cvPurchaseService.activeStep[this.cartType] + 1);
            }).catch(reason => this.finishRunning(
                true,
                reason || "An error occurred in step submit, it was not successful"));
        }
        startTimer(): void {
            if (!this.interval) {
                this.interval = this.$interval(() => {
                    if (this.timeRemaining > 0) {
                        this.timeRemaining -= 1000;
                        const minutes = Math.floor(this.timeRemaining / 60000);
                        const seconds = ((this.timeRemaining % 60000) / 1000).toFixed(0);
                        this.timeRemainingStr = `${_.padStart(minutes.toString(), 2, "0")}:${_.padStart(seconds.toString(), 2, "0")}`;
                    } else {
                        this.clearTimer();
                        const translation = this.$translate.instant("ui.storefront.purchasing.reservationExpired");
                        this.cvMessageModalFactory(translation).finally(() => {
                            this.$filter("goToCORSLink")("/Cart");
                        });
                    }
                }, 1000);
            }
        }
        clearTimer(): void {
            this.$interval.cancel(this.interval);
        }
        // Events
        // <None>
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                private readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvPurchaseService: services.IPurchaseService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $filter: ng.IFilterService,
                private readonly $interval: ng.IIntervalService,
                private readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory,
                private readonly cvCartService: services.ICartService,
                private readonly $q: ng.IQService,
                private readonly cvApi: api.ICEFAPI,) {
            super(cefConfig);
            this.consoleDebug(`PurchaseController.ctor()`);
            if (!this.cartType) {
                this.cartType = cvServiceStrings.carts.types.cart;
            }
            this.setRunning();
            this.cvPurchaseService.building[this.cartType] = true;
            this.cvPurchaseService.initialize(this.cartType)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
            $rootScope.getCartType = () => this.cartType;
            $(window).bind("beforeunload", function() {
                // Clear the target carts so they can't cause issues with subsequent runs
                cvPurchaseService.clearAnalysis($rootScope.getCartType()).then(() => { /* Do Nothing */ });
                return undefined;
            });
            /* TODO: Push this behind a setting to trigger
            this.cvConfirmModalFactory(
                this.$translate("ui.storefront.user.registration.IAgreeToTheTermsOfUseForThisWebsite")
            ).then(accept => {
                if (!accept) {
                    this.$filter("goToCORSLink")("/Cart");
                }
            }).catch(() => this.$filter("goToCORSLink")("/Cart"));
            */
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (this.interval) { this.$interval.cancel(this.interval); }
            });
        }
    }

    cefApp.directive("cefPurchase", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/purchase.html", "ui"),
        controller: PurchaseController,
        controllerAs: "pCtrl",
        bindToController: true
    }));
}
