/**
 * @file framework/store/cart/controls/addToCartQuantitySelector.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 */
module cef.store.cart.controls {
    export class AddToCartQuantitySelectorController extends AddToCartControllerBase {
        // Bound Scope Properties
        isRequired: boolean
        step: number;
        noButtons: boolean;
        withStyles: boolean;
        overrideMaximum: boolean;
        onBlur: Function;
        onStep: Function;
        // Properties
        get minimum(): number {
            if (!this.item) {
                return 1;
            }
            if (this.item.IsDiscontinued) {
                return 0;
            }
            if (!this.hasMinimumPurchaseQuantity()
                && !this.hasMinimumPurchaseQuantityIfPastPurchased()) {
                return 1;
            }
            if (this.hasMinimumPurchaseQuantityIfPastPurchased()
                && this.cvAuthenticationService.isAuthenticated()) {
                return this.getMinimumPurchaseQuantityIfPastPurchased() > 0
                    ? this.getMinimumPurchaseQuantityIfPastPurchased()
                    : 1;
            }
            if (this.hasMinimumPurchaseQuantity()) {
                return this.getMinimumPurchaseQuantity() > 0
                    ? this.getMinimumPurchaseQuantity()
                    : 1;
            }
            return 1;
        }
        get maximum(): number {
            if (!this.item || !angular.isFunction(this.item.readInventory)) {
                return 9999999; // Custom Scenario or not loaded yet
            }
            const inventory = this.item.readInventory();
            if (inventory.IsDiscontinued) {
                return 0;
            }
            if (this.alwaysAllowInput) {
                return 9999999;
            }
            if (inventory.IsUnlimitedStock) {
                return 9999999;
            }
            if (this.notifyMaximum) {
                return 9999999;
            }
            if (this.overrideMaximum) {
                return 9999999;
            }
            // Pre-sales may have an end date, if it does, ensure it's later than now
            if (inventory.AllowPreSale
                && (!inventory.PreSellEndDate || new Date() < inventory.PreSellEndDate)) {
                const minPreSales: number[] = [];
                if (this.hasMaximumPrePurchaseQuantityGlobal()) {
                    // TODO: Read global already pre-purchased quantity and verify we are under it
                    minPreSales.push(this.getMaximumPrePurchaseQuantityGlobal())
                }
                if (this.hasMaximumPrePurchaseQuantityIfPastPurchased()
                    && this.cvAuthenticationService.isAuthenticated()) {
                    // TODO: Read account's already pre-purchased quantity and verify we are under it
                    minPreSales.push(this.getMaximumPrePurchaseQuantityIfPastPurchased())
                }
                if (this.hasMaximumPrePurchaseQuantity()) {
                    minPreSales.push(this.getMaximumPrePurchaseQuantity())
                }
                // Note that this causes an override against AllowBackOrder. If there's no
                // other limits set above, it ignores ABO logic
                minPreSales.push(9999999);
                return _.min(minPreSales);
            }
            if (inventory.AllowBackOrder) {
                const minBackOrders: number[] = [];
                if (this.cefConfig.featureSet.inventory.backOrder.maxPerProductGlobal.enabled
                    && this.hasMaximumBackOrderPurchaseQuantityGlobal()) {
                    // TODO: Read global already back-ordered quantity and verify we are under it
                    minBackOrders.push(this.getMaximumBackOrderPurchaseQuantityGlobal())
                }
                if (this.cefConfig.featureSet.inventory.backOrder.maxPerProductPerAccount.enabled
                    && this.hasMaximumBackOrderPurchaseQuantityIfPastPurchased()
                    && this.cvAuthenticationService.isAuthenticated()) {
                    // TODO: Read account's already back-ordered quantity and verify we are under it
                    minBackOrders.push(this.getMaximumBackOrderPurchaseQuantityIfPastPurchased())
                }
                if (this.hasMaximumBackOrderPurchaseQuantity()) {
                    minBackOrders.push(this.getMaximumBackOrderPurchaseQuantity())
                }
                // Note that this causes an override against AllowBackOrder. If there's no
                // other limits set above, it ignores ABO logic
                minBackOrders.push(9999999);
                return _.min(minBackOrders);
            }
            if (!this.hasMaximumPurchaseQuantity() && !this.hasMaximumPurchaseQuantityIfPastPurchased()) {
                return inventory.QuantityOnHand;
            }
            if (this.hasMaximumPurchaseQuantityIfPastPurchased() && this.cvAuthenticationService.isAuthenticated()) {
                return this.getMaximumPurchaseQuantityIfPastPurchased() > 0
                    ? this.getMaximumPurchaseQuantityIfPastPurchased()
                    : 9999999;
            }
            if (this.hasMaximumPurchaseQuantity()) {
                return this.getMaximumPurchaseQuantity() > 0
                    ? this.getMaximumPurchaseQuantity()
                    : 9999999;
            }
            return inventory.QuantityOnHand;
        }
        get isDisabled(): boolean {
            return this.viewState.running
                || this.externalDisabled
                || !this.alwaysAllowInput
                    && (this.item
                        && angular.isFunction(this.item.readInventory)
                        && this.item.readInventory().IsOutOfStock
                        && !this.item.readInventory().AllowBackOrder
                        || !this.canReduce() && !this.canIncrease());
        }
        get isDisabledReduce(): boolean {
            return this.viewState.running
                || this.externalDisabled
                || !this.alwaysAllowInput
                    && (this.item
                        && angular.isFunction(this.item.readInventory)
                        && this.item.readInventory().IsOutOfStock
                        && !this.item.readInventory().AllowBackOrder
                        || !this.canReduce());
        }
        get isDisabledIncrease(): boolean {
            return this.viewState.running
                || this.externalDisabled
                || !this.alwaysAllowInput
                    && (this.item
                        && angular.isFunction(this.item.readInventory)
                        && this.item.readInventory().IsOutOfStock
                        && !this.item.readInventory().AllowBackOrder
                        || !this.canIncrease());
        }
        get currentValueEditable(): number {
            if (angular.isUndefined(this.currentValue)) {
                // this.consoleDebug(`AddToCartQuantitySelectorController.currentValue.get-${this.$scope.$id
                //     } is undefined, returning undefined for now`);
                return undefined;
            }
            if (this.currentValue === null) {
                // console.trace(`AddToCartQuantitySelectorController.currentValue.get-${this.$scope.$id
                //     } is having to use default value because it's null`);
                this.currentValue = this.defaultValue || this.minimum;
            }
            // this.consoleDebug(`AddToCartQuantitySelectorController.currentValue.get-${this.$scope.$id
            //     } is defined and returning a value of '${this.currentValue}'`);
            return this.currentValue;
        }
        set currentValueEditable(newValue: number) {
            if (newValue === undefined) {
                // Ignore it
                return;
            }
            this.currentValue = newValue;
            this.dirty = true;
            this.doOnStep(null);
        }
        // Functions
        // NOTE: This must remain an arrow function
        hasMinimumPurchaseQuantity = (): boolean => {
            return this.getMinimumPurchaseQuantity() != null;
        }
        // NOTE: This must remain an arrow function
        hasMinimumPurchaseQuantityIfPastPurchased = (): boolean => {
            return this.getMinimumPurchaseQuantityIfPastPurchased() != null;
        }
        // NOTE: This must remain an arrow function
        hasMaximumPurchaseQuantity = (): boolean => {
            return this.getMaximumPurchaseQuantity() != null;
        }
        // NOTE: This must remain an arrow function
        hasMaximumPrePurchaseQuantity = (): boolean => {
            return this.getMaximumPrePurchaseQuantity() != null;
        }
        // NOTE: This must remain an arrow function
        hasMaximumPrePurchaseQuantityGlobal = (): boolean => {
            return this.getMaximumPrePurchaseQuantityGlobal() != null;
        }
        // NOTE: This must remain an arrow function
        hasMaximumBackOrderPurchaseQuantity = (): boolean => {
            return this.getMaximumBackOrderPurchaseQuantity() != null;
        }
        // NOTE: This must remain an arrow function
        hasMaximumBackOrderPurchaseQuantityGlobal = (): boolean => {
            return this.getMaximumBackOrderPurchaseQuantityGlobal() != null;
        }
        // NOTE: This must remain an arrow function
        hasMaximumPurchaseQuantityIfPastPurchased = (): boolean => {
            return this.getMaximumPurchaseQuantityIfPastPurchased() != null;
        }
        // NOTE: This must remain an arrow function
        hasMaximumPrePurchaseQuantityIfPastPurchased = (): boolean => {
            return this.getMaximumPrePurchaseQuantityIfPastPurchased() != null;
        }
        // NOTE: This must remain an arrow function
        hasMaximumBackOrderPurchaseQuantityIfPastPurchased = (): boolean => {
            return this.getMaximumBackOrderPurchaseQuantityIfPastPurchased() != null;
        }
        // NOTE: This must remain an arrow function
        getMinimumPurchaseQuantity = (): number => {
            if (!this.cefConfig.featureSet.purchasing.minMax.enabled) {
                return null;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued) {
                return 0;
            }
            if (angular.isUndefined(this.item.MinimumPurchaseQuantity)
                || this.item.MinimumPurchaseQuantity == null) {
                return null;
            }
            return this.item.MinimumPurchaseQuantity;
        }
        // NOTE: This must remain an arrow function
        getMinimumPurchaseQuantityIfPastPurchased = (): number => {
            if (!this.cefConfig.featureSet.purchasing.minMax.after.enabled) {
                return null;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued
                || angular.isUndefined(this.item.MinimumPurchaseQuantityIfPastPurchased)
                || this.item.MinimumPurchaseQuantityIfPastPurchased == null) {
                return 0;
            }
            return this.item.MinimumPurchaseQuantityIfPastPurchased;
        }
        // NOTE: This must remain an arrow function
        getMaximumPurchaseQuantity = (): number => {
            if (!this.cefConfig.featureSet.purchasing.minMax.enabled) {
                return null;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued) {
                return 0;
            }
            if (angular.isUndefined(this.item.MaximumPurchaseQuantity)
                || this.item.MaximumPurchaseQuantity == null) {
                return null;
            }
            return this.item.MaximumPurchaseQuantity;
        }
        // NOTE: This must remain an arrow function
        getMaximumPrePurchaseQuantity = (): number => {
            if (!this.cefConfig.featureSet.inventory.preSale.enabled) {
                return 0;
            }
            if (!this.cefConfig.featureSet.inventory.preSale.maxPerProductPerAccount.enabled) {
                return null;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued || !this.item.AllowPreSale) {
                return 0;
            }
            if (angular.isUndefined(this.item.MaximumPrePurchaseQuantity)
                || this.item.MaximumPrePurchaseQuantity == null) {
                return null;
            }
            return this.item.MaximumPrePurchaseQuantity;
        }
        // NOTE: This must remain an arrow function
        getMaximumPrePurchaseQuantityGlobal = (): number => {
            if (!this.cefConfig.featureSet.inventory.preSale.enabled) {
                return 0;
            }
            if (!this.cefConfig.featureSet.inventory.preSale.maxPerProductGlobal.enabled) {
                return null;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued || !this.item.AllowPreSale) {
                return 0;
            }
            if (angular.isUndefined(this.item.MaximumPrePurchaseQuantityGlobal)
                || this.item.MaximumPrePurchaseQuantityGlobal == null) {
                return null;
            }
            return this.item.MaximumPrePurchaseQuantityGlobal;
        }
        // NOTE: This must remain an arrow function
        getMaximumBackOrderPurchaseQuantity = (): number => {
            if (!this.cefConfig.featureSet.inventory.backOrder.enabled) {
                return 0;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued || !this.item.AllowBackOrder) {
                return 0;
            }
            if (angular.isUndefined(this.item.MaximumBackOrderPurchaseQuantity)
                || this.item.MaximumBackOrderPurchaseQuantity == null) {
                return null;
            }
            return this.item.MaximumBackOrderPurchaseQuantity;
        }
        // NOTE: This must remain an arrow function
        getMaximumBackOrderPurchaseQuantityGlobal = (): number => {
            if (!this.cefConfig.featureSet.inventory.backOrder.enabled) {
                return 0;
            }
            if (!this.cefConfig.featureSet.inventory.backOrder.maxPerProductGlobal.enabled) {
                return null;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued || !this.item.AllowBackOrder) {
                return 0;
            }
            if (angular.isUndefined(this.item.MaximumBackOrderPurchaseQuantityGlobal)
                || this.item.MaximumBackOrderPurchaseQuantityGlobal == null) {
                return null;
            }
            return this.item.MaximumBackOrderPurchaseQuantityGlobal;
        }
        // NOTE: This must remain an arrow function
        getMaximumPurchaseQuantityIfPastPurchased = (): number => {
            if (!this.cefConfig.featureSet.purchasing.minMax.after.enabled) {
                return null;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued) {
                return 0;
            }
            if (angular.isUndefined(this.item.MaximumPurchaseQuantityIfPastPurchased)
                || this.item.MaximumPurchaseQuantityIfPastPurchased == null) {
                return null;
            }
            return this.item.MaximumPurchaseQuantityIfPastPurchased;
        }
        // NOTE: This must remain an arrow function
        getMaximumPrePurchaseQuantityIfPastPurchased = (): number => {
            if (!this.cefConfig.featureSet.inventory.preSale.enabled) {
                return 0;
            }
            if (!this.cefConfig.featureSet.inventory.preSale.maxPerProductPerAccount.enabled) {
                return null;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued || !this.item.AllowPreSale) {
                return 0;
            }
            if (angular.isUndefined(this.item.MaximumPrePurchaseQuantityIfPastPurchased)
                || this.item.MaximumPrePurchaseQuantityIfPastPurchased == null) {
                return null;
            }
            return this.item.MaximumPrePurchaseQuantityIfPastPurchased;
        }
        // NOTE: This must remain an arrow function
        getMaximumBackOrderPurchaseQuantityIfPastPurchased = (): number => {
            if (!this.cefConfig.featureSet.inventory.backOrder.enabled) {
                return 0;
            }
            if (!this.cefConfig.featureSet.inventory.backOrder.maxPerProductPerAccount.after.enabled) {
                return null;
            }
            if (!this.item || !this.item.ID) {
                return null;
            }
            if (this.item.IsDiscontinued || (!this.item.AllowBackOrder)) {
                return 0;
            }
            if (angular.isUndefined(this.item.MaximumBackOrderPurchaseQuantityIfPastPurchased)
                || this.item.MaximumBackOrderPurchaseQuantityIfPastPurchased == null) {
                return null;
            }
            return this.item.MaximumBackOrderPurchaseQuantityIfPastPurchased;
        }
        // NOTE: This must remain an arrow function
        canReduce = (): boolean => {
            if (this.externalDisabled) { return false; }
            if (this.alwaysAllowInput) { return this.currentValue > 1; }
            if (!this.item) { return false; }
            return this.currentValue > this.minimum;
        }
        // NOTE: This must remain an arrow function
        canIncrease = (): boolean => {
            if (this.externalDisabled) { return false; }
            if (this.alwaysAllowInput) { return true; }
            if (!this.item) { return false; }
            if (this.item.IsUnlimitedStock) { return true; }
            return this.currentValue < this.maximum; // Max reads the pre-sales and backorder cap logics
        }
        reduce($event: ng.IAngularEvent): void {
            if (!this.canReduce()) { return; }
            let val = this.currentValue;
            if (val === undefined || val === null || val <= 1) {
                val = 1;
            } else {
                val -= 1;
            }
            this.currentValue = val;
            this.doOnStep($event);
        }
        increase($event: ng.IAngularEvent): void {
            if (!this.canIncrease()) { return; }
            let val = this.currentValue;
            if (val === undefined || val === null) {
                val = 1;
            } else {
                val += 1;
            }
            this.currentValue = val;
            this.doOnStep($event);
        }
        extractProductId = (): number => {
            if (!this.item || !this.item.ID) {
                return null;
            }
            return this.item.ID;
        }
        // Events
        doOnBlur($event: ng.IAngularEvent): void {
            if ($event) {
                // this.consoleDebug($event);
                $event.preventDefault();
            }
            if (!this.onBlur || !angular.isFunction(this.onBlur) || !this.dirty) {
                return;
            }
            const returned = this.onBlur($event, this.extractProductId(), this.currentValue);
            if (!returned || !angular.isFunction(returned.then)) {
                return;
            }
            this.setRunning();
            this.$q.when(returned)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        doOnStep($event: ng.IAngularEvent): void {
            // const debugMsg = `doOnStep-${this.$scope.$id}`;
            // console.trace(`${debugMsg} entered`);
            if ($event) {
                // this.consoleDebug(`${debugMsg} has event:`);
                // this.consoleDebug($event);
                $event.preventDefault();
            } else {
                // this.consoleDebug(`${debugMsg} does not have an event (probably ng-change)`);
            }
            if ((Math.round(new Date().getTime()) - this.initializedAt) < 1000) {
                // this.consoleDebug(`${debugMsg} exited: too soon since initialization, prevent bad calls`);
                return;
            }
            if (!this.onStep || !angular.isFunction(this.onStep)) {
                // this.consoleDebug(`${debugMsg} exited: no external func detected`);
                return;
            }
            if (this.viewState.running) {
                // this.consoleDebug(`${debugMsg} exited: we're already running so this might be a dupe`);
                return;
            }
            this.setRunning();
            const returned = this.onStep()($event, this.extractProductId(), this.currentValue);
            if (!returned || !angular.isFunction(returned.then)) {
                // this.consoleDebug(`${debugMsg} exited: nothing returned from external func`);
                this.finishRunning();
                return;
            }
            this.$q.when(returned).then(() => {
                // this.consoleDebug(`${debugMsg} exited: external func returned successfully`);
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        setAlwaysAllowInput(): void {
            if (this.alwaysAllowInput !== undefined) {
                return;
            }
            if (this.cefConfig.featureSet.salesQuotes.enabled
                && this.cvServiceStrings.carts.types.quote) {
                this.alwaysAllowInput = true;
            }
        }
        // Constructors
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService) {
            super($scope, cefConfig, cvProductService);
            // const debugMsg = `AddToCartQuantitySelectorController.ctor-${$scope.$id}`;
            // this.consoleDebug(`${debugMsg}: entered`);
            // $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
            //     this.consoleDebug(`scope-${$scope.$id} is being destroyed`);
            // });
            // this.consoleDebug(`${debugMsg}: exited`);
            this.setAlwaysAllowInput();
        }
    }

    cefApp.directive("cefAddToCartQuantitySelector", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            // Item lookup
            item: "=?",
            itemId: "=?",
            itemSeoUrl: "=?",
            itemName: "=?",
            salesItem: "=?",
            // Shared with base
            cartType: "=",
            currentValue: "=",
            defaultValue: "=?",
            alwaysAllowInput: "=?",
            externalDisabled: "=?",
            debug: "=?",
            index: "=?",
            notifyMaximum: "=?",
            // NUD specific
            isRequired: "=?",
            step: "=?",
            onBlur: "&?",
            onStep: "&?",
            noButtons: "=?",
            withStyles: "=?",
            overrideMaximum: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/cart/controls/addToCartQuantitySelector.html", "ui"),
        controller: AddToCartQuantitySelectorController,
        controllerAs: "atcqsCtrl",
        bindToController: true
    }));
}
