/**
 * @file framework/admin/controls/sales/widgets/rateQuotesManagerWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc The abstract base class for rate quote manager widgets (cart and checkout)
 */
module cef.admin.controls.sales.widgets {
    export abstract class RateQuotesManagerWidgetControllerBase extends core.TemplatedControllerBase {
        // Bound by Scope Properties
        lookupKey: api.CartByIDLookupKey;
        currentCartId: number;
        apply: boolean;
        contact: api.ContactModel;
        hideAddress: boolean;
        hideTitle: boolean;
        // Properties
        lookupKeyToUse: api.CartByIDLookupKey;
        selectedRateQuoteID: number;
        quantityRatesIterate: number;
        quantityRatesShown: number;
        rateQuotes: Array<api.RateQuoteModel>;
        unbindReadyEvent: Function;
        unbindUnreadyEvent: Function;
        isGettingRateQuotes = false;
        readonly sortedDirUn: number = -1;
        readonly sortedDirAsc: number = 0;
        readonly sortedDirDesc: number = 1;
        private _currentSortDir = this.sortedDirAsc;
        get currentSortDir(): number {
            return this._currentSortDir;
        }
        set currentSortDir(newValue: number) {
            this._currentSortDir = newValue;
        }
        readonly sortedByRate = 0;
        readonly sortedByEstDel = 1;
        private _currentSortBy = this.sortedByRate;
        get currentSortBy(): number {
            return this._currentSortBy;
        }
        set currentSortBy(newValue: number) {
            this._currentSortBy = newValue;
        }
        get showShowMoreButton() {
            return this.rateQuotes
                && this.rateQuotes.length > this.quantityRatesIterate
                && this.quantityRatesShown < this.rateQuotes.length;
        }
        get showShowLessButton() {
            return this.rateQuotes
                && this.rateQuotes.length > this.quantityRatesIterate
                && this.quantityRatesShown > this.quantityRatesIterate;
        }
        get showYourShippingRateQuotesHeader() {
            return Boolean((this.viewState.running || this.rateQuotes && this.rateQuotes.length > 0 && this.lookupKeyToUse)
                && this.cvCartService.accessCart(this.lookupKeyToUse)
                && this.cvCartService.accessCart(this.lookupKeyToUse).ShippingContact
                && this.cvCartService.accessCart(this.lookupKeyToUse).ShippingContact.Address
                && this.cvCartService.accessCart(this.lookupKeyToUse).ShippingContact.Address.PostalCode)
                && !this.hideTitle;;
        }
        // Functions
        sortBy(sortBy: number): void {
            if (sortBy === this.currentSortBy) {
                // Same "by", change direction
                switch (this.currentSortDir) {
                    case this.sortedDirAsc: { this.currentSortDir = this.sortedDirDesc; break; }
                    case this.sortedDirDesc: { this.currentSortDir = this.sortedDirUn; break; }
                    case this.sortedDirUn: { this.currentSortDir = this.sortedDirAsc; break; }
                }
                return;
            }
            // Different "by", switch to ascending and update the "by"
            this.currentSortBy = sortBy;
            this.currentSortDir = this.sortedDirAsc;
        }
        isSorted(by: number, dir: number): boolean {
            return by === this.currentSortBy && dir === this.currentSortDir;
        }
        getSorter(): string | string[] {
            const current = String(this.currentSortBy) + "|" + String(this.currentSortDir);
            switch (current) {
                case "0|-1":
                case "1|-1": {
                    // Unsorted
                    return "Name";
                }
                case "0|0": { return [ "Rate","EstimatedDeliveryDate","Name"]; }
                case "0|1": { return ["-Rate","EstimatedDeliveryDate","Name"]; }
                case "1|0": { return [ "EstimatedDeliveryDate","Rate","Name"]; }
                case "1|1": { return ["-EstimatedDeliveryDate","Rate","Name"]; }
            }
            // Unknown
            return "Name";
        }
        protected loadCallback: (cart: api.CartModel) => void = null;
        protected load(): void {
            const debugMsg = "RateQuotesManagerWidgetControllerBase.load():";
            this.consoleDebug(`${debugMsg} entered`);
            this.consoleDebug(`${debugMsg} set running`);
            this.setRunning();
            this.consoleDebug(`${debugMsg} set rates to show to 6`);
            this.quantityRatesIterate = 6;
            this.quantityRatesShown = this.quantityRatesIterate;
            this.consoleDebug(`${debugMsg} load cart`);
            this.lookupKeyToUse = this.lookupKey;
            if (this.currentCartId) {
                this.lookupKeyToUse = api.CartByIDLookupKey.newFromJson(this.lookupKeyToUse.toString());
                this.lookupKeyToUse.ID = this.currentCartId;
            }
            this.cvCartService.loadCart(this.lookupKeyToUse).then(r => {
                this.consoleDebug(`${debugMsg} cart load returned successfully`);
                const cart = r.Result;
                if (cart == null || cart === ({} as any)) {
                    this.consoleDebug(`${debugMsg} but the cart is null-ish, exiting`);
                    this.finishRunning(true, "No cart");
                    return;
                }
                this.consoleDebug(`${debugMsg} checking for existing rate quotes`);
                if (cart.RateQuotes && cart.RateQuotes.length > 0) {
                    this.consoleDebug(`${debugMsg} existing rate quotes detected! Looping...`);
                    for (let i = 0; i < cart.RateQuotes.length; i++) {
                        const r = cart.RateQuotes[i];
                        r["original"] = r.Rate;
                        r["discounted"] = r.Rate;
                        const shippingDiscounts = (cart.Discounts || []).filter(x => x.DiscountTypeID === 2); // "Applied to Shipping"
                        if (!shippingDiscounts || shippingDiscounts.length === 0) {
                            r["discounted"] = r.Rate;
                            continue;
                        }
                        let discounted = r.Rate;
                        shippingDiscounts
                            .sort((a, b) => { return a.DiscountPriority - b.DiscountPriority; })
                            .forEach(value => {
                                if (value.DiscountValueType === 0) { // Percent
                                    discounted = discounted * (1 - value.DiscountValue);
                                } else if (value.DiscountValueType === 1) { // Dollar
                                    discounted = discounted - value.DiscountValue;
                                }
                            });
                        r["discounted"] = discounted;
                    }
                    this.consoleDebug(`${debugMsg} existing rate quotes looped!`);
                    const selected = _.find(cart.RateQuotes, r => r.Selected);
                    let selectedID: number = null;
                    this.consoleDebug(`${debugMsg} checking for selected...`);
                    if (selected) {
                        this.consoleDebug(`${debugMsg} selected found!`);
                        selectedID = selected.ID;
                        const selectedIndex = _.findIndex(cart.RateQuotes, r => r.Selected);
                        while (selectedIndex > this.quantityRatesShown) {
                            this.showMore();
                        }
                        this.cvCartService.accessCart(this.lookupKeyToUse).Totals.Shipping = selected.Rate;
                        this.cvCartService.recalculateTotals(this.lookupKeyToUse);
                    }
                    this.consoleDebug(`${debugMsg} saving to UI variables!`);
                    this.rateQuotes = cart.RateQuotes;
                    this.selectedRateQuoteID = selectedID;
                }
                this.consoleDebug(`${debugMsg} finished successfully, exiting!`);
                this.finishRunning();
                if (angular.isFunction(this.loadCallback)) {
                    this.loadCallback(cart);
                }
            }).catch(reason => {
                this.consoleDebug(`${debugMsg} cart load failed, exiting!`, reason);
                this.finishRunning(true, reason);
            });
            this.unbindReadyEvent = this.$scope.$on(this.cvServiceStrings.events.shipping.ready,
                (__: ng.IAngularEvent, cart: api.CartModel, reapply: boolean) => {
                    const debugMsg = "RateQuotesManagerWidgetControllerBase.shippingReadyEvent:";
                    this.consoleDebug(`${debugMsg} detected!`);
                    if (!cart || !cart.ShippingContact) {
                        this.consoleDebug(`${debugMsg} no cart or no shipping contact on cart, load again to be sure`);
                        this.cvCartService.loadCart(this.lookupKeyToUse).then(r => {
                            this.consoleDebug(`${debugMsg} load returned`);
                            if (!r.ActionSucceeded) {
                                this.consoleDebug(`${debugMsg} load returned, but not successfully`);
                                return;
                            }
                            this.consoleDebug(`${debugMsg} load returned, reading shipping contact`);
                            this.contact = r.Result.ShippingContact;
                            this.consoleDebug(`${debugMsg} calling get rate quotes now that we've loaded contact data`);
                            this.getRateQuotes(reapply && this.apply);
                        }).catch(reason => this.consoleLog(reason));
                        return;
                    }
                    this.consoleDebug(`${debugMsg} reading contact data from cart in event`);
                    this.contact = cart.ShippingContact;
                    this.consoleDebug(`${debugMsg} calling get rate quotes`);
                    this.getRateQuotes(reapply && this.apply);
                });
            this.unbindUnreadyEvent = this.$scope.$on(this.cvServiceStrings.events.shipping.unready,
                (__: ng.IAngularEvent, cart: api.CartModel, reapply: boolean) => {
                    const debugMsg = "RateQuotesManagerWidgetControllerBase.shippingUnreadyEvent:";
                    this.consoleDebug(`${debugMsg} detected!`);
                    this.contact = undefined;
                    this.rateQuotes = undefined;
                });
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindReadyEvent)) { this.unbindReadyEvent(); }
                if (angular.isFunction(this.unbindUnreadyEvent)) { this.unbindUnreadyEvent(); }
            });
        }
        getRateQuotes(apply: boolean): void {
            const debugMsg = "RateQuotesManagerWidgetControllerBase.getRateQuotes:";
            this.consoleDebug(`${debugMsg} entered with '${String(apply)}'`);
            if (this.isGettingRateQuotes) {
                this.consoleDebug(`${debugMsg} already running, exiting`);
                // Don't fire twice
                return;
            }
            this.isGettingRateQuotes = true;
            this.consoleDebug(`${debugMsg} setting as running`);
            this.setRunning();
            if (apply) {
                this.consoleDebug(`${debugMsg} told to apply shipping contact, firing function`);
                this.cvCartService.applyShippingContact(this.lookupKeyToUse)
                    .then(() => {
                        this.consoleDebug(`${debugMsg} applied shipping contact, moving to inner`);
                        this.getRateQuotesInner();
                    }).catch(reason => {
                        this.consoleDebug(`${debugMsg} failed to apply shipping contact, exiting`, reason);
                        this.finishRunning(true, reason);
                        this.isGettingRateQuotes = false;
                    });
                return;
            }
            this.consoleDebug(`${debugMsg} not necessary to apply shipping contact, moving to inner`);
            this.getRateQuotesInner();
        }
        getRateQuotesInner(): void {
            const debugMsg = "RateQuotesManagerWidgetControllerBase.getRateQuotesInner:";
            this.consoleDebug(`${debugMsg} entered, setting running`);
            this.setRunning();
            this.consoleDebug(`${debugMsg} asking server for rate quotes`);
            this.cvApi.shopping.GetCartShippingRateQuotes({
                ID: this.lookupKeyToUse.ID,
                Expedited: false
            }).then(r => {
                this.consoleDebug(`${debugMsg} server returned ok`);
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.consoleDebug(`${debugMsg} server returned ok, but was unsuccessful`);
                    this.isGettingRateQuotes = false;
                    this.finishRunning(true, null, r && r.data && r.data.Messages);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.loaded,
                        this.rateQuotes,
                        this.selectedRateQuoteID);
                    return;
                }
                const rateQuotes = r.data.Result;
                this.consoleDebug(`${debugMsg} reading results`);
                if (rateQuotes && rateQuotes.length > 0) {
                    for (let i = 0; i < rateQuotes.length; i++) {
                        const r = rateQuotes[i];
                        r["original"] = r.Rate;
                        r["discounted"] = r.Rate;
                        const shippingDiscounts = (this.cvCartService.accessCart(this.lookupKeyToUse)
                            && this.cvCartService.accessCart(this.lookupKeyToUse).Discounts
                            || []).filter(x => x.DiscountTypeID === 2); // "Applied to Shipping"
                        if (!shippingDiscounts || shippingDiscounts.length === 0) {
                            r["discounted"] = r.Rate;
                            continue;
                        }
                        let discounted = r.Rate;
                        shippingDiscounts
                            .sort((a, b) => { return a.DiscountPriority - b.DiscountPriority; })
                            .forEach(value => {
                                if (value.DiscountValueType === 0) { // Percent
                                    discounted = discounted * (1 - value.DiscountValue);
                                } else if (value.DiscountValueType === 1) { // Dollar
                                    discounted = discounted - value.DiscountValue;
                                }
                            });
                        r["discounted"] = discounted;
                    }
                    const selected = _.find(rateQuotes, r => r.Selected);
                    let selectedID: number = null;
                    if (selected) {
                        selectedID = selected.ID;
                        const selectedIndex = _.findIndex(rateQuotes, r => r.Selected);
                        while (selectedIndex > this.quantityRatesShown) {
                            this.showMore();
                        }
                        this.cvCartService.accessCart(this.lookupKeyToUse).Totals.Shipping = selected.Rate;
                        this.cvCartService.recalculateTotals(this.lookupKeyToUse);
                    }
                    this.cvCartService.accessCart(this.lookupKeyToUse).RateQuotes = rateQuotes;
                    this.rateQuotes = rateQuotes;
                    this.selectedRateQuoteID = selectedID;
                }
                this.isGettingRateQuotes = false;
                this.finishRunning();
                this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.loaded,
                    this.rateQuotes,
                    this.selectedRateQuoteID);
            }).catch(reason => {
                this.finishRunning(true, reason);
                this.isGettingRateQuotes = false;
            });
        }
        showMore(): void {
            this.quantityRatesShown = this.quantityRatesShown + this.quantityRatesIterate;
        }
        showLess(): void {
            this.quantityRatesShown = Math.max(
                this.quantityRatesIterate,
                this.quantityRatesShown - this.quantityRatesIterate);
        }
        isRateDiscounted(rate: api.RateQuoteModel): boolean {
            return rate["original"] !== rate["discounted"];
        }
        // Events
        private onSelectRateQuote(): void {
            this.setRunning();
            const dto = <api.ApplyCartShippingRateQuoteDto>{
                ID: this.lookupKeyToUse.ID,
                RateQuoteID: Number(this.selectedRateQuoteID),
                // TODO: RequestedShipDate
            };
            this.cvApi.shopping.ApplyCartShippingRateQuote(dto).then(r => {
                if (!r.data.ActionSucceeded) {
                    this.cvViewStateService.processCEFActionResponseMessages(r.data);
                    this.finishRunning(true, null, r.data.Messages);
                    return;
                }
                this.finishRunning();
                this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.rateQuoteSelected,
                    this.lookupKeyToUse,
                    Number(this.selectedRateQuoteID));
                this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, this.lookupKeyToUse);
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Constructors
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly cvCartService: admin.services.ICartService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                protected readonly cvViewStateService: admin.services.IViewStateService) {
            super(cefConfig);
            this.load();
        }
    }
}
