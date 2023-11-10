/**
 * @file framework/store/cart/controls/rateQuotes/rateQuotesManagerWidgetControllerBase.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc The abstract base class for rate quote manager widgets (cart and checkout)
 */
module cef.store.cart.controls.rateQuotes {
    export abstract class RateQuotesManagerWidgetControllerBase extends core.TemplatedControllerBase {
        // Bound Scope Properties
        type: string;
        apply: boolean;
        contact: api.ContactModel;
        hideAddress: boolean;
        hideTitle: boolean;
        // Properties
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
            return Boolean((this.viewState.running || this.rateQuotes && this.rateQuotes.length > 0)
                && this.cvCartService.accessCart(this.type)
                && this.cvCartService.accessCart(this.type).ShippingContact
                && this.cvCartService.accessCart(this.type).ShippingContact.Address
                && this.cvCartService.accessCart(this.type).ShippingContact.Address.PostalCode)
                && !this.hideTitle;
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
            this.setRunning();
            this.quantityRatesIterate = 6;
            this.quantityRatesShown = this.quantityRatesIterate;
            this.cvCartService.loadCart(this.type, false, "rateQuotesManagerWidget.load").then(r => {
                const cart = r.Result;
                if (cart == null || cart === ({} as any)) {
                    this.finishRunning(true, "No cart");
                    return;
                }
                if (cart.RateQuotes && cart.RateQuotes.length > 0) {
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
                    const selected = _.find(cart.RateQuotes, r => r.Selected);
                    let selectedID: number = null;
                    if (selected) {
                        selectedID = selected.ID;
                        const selectedIndex = _.findIndex(cart.RateQuotes, r => r.Selected);
                        while (selectedIndex > this.quantityRatesShown) {
                            this.showMore();
                        }
                        this.cvCartService.accessCart(this.type).Totals.Shipping = selected.Rate;
                        this.cvCartService.recalculateTotals(this.type);
                    }
                    this.rateQuotes = cart.RateQuotes;
                    this.selectedRateQuoteID = selectedID;
                }
                this.finishRunning();
                if (angular.isFunction(this.loadCallback)) {
                    this.loadCallback(cart);
                }
            }).catch(reason => this.finishRunning(true, reason));
            this.unbindReadyEvent = this.$scope.$on(this.cvServiceStrings.events.shipping.ready,
                (__: ng.IAngularEvent, cart: api.CartModel, reapply: boolean) => {
                    if (!cart || !cart.ShippingContact) {
                        this.cvCartService.loadCart(this.type, false, "rateQuotesManagerWidget.shippingReadyEvent").then(r => {
                            if (!r.ActionSucceeded) {
                                return;
                            }
                            this.contact = r.Result.ShippingContact;
                            this.getRateQuotes(reapply && this.apply);
                        });
                        return;
                    }
                    this.contact = cart.ShippingContact;
                    this.getRateQuotes(reapply && this.apply);
                });
            this.unbindUnreadyEvent = this.$scope.$on(this.cvServiceStrings.events.shipping.unready,
                (__: ng.IAngularEvent, cart: api.CartModel, reapply: boolean) => {
                    this.contact = undefined;
                    this.rateQuotes = undefined;
                });
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindReadyEvent)) { this.unbindReadyEvent(); }
                if (angular.isFunction(this.unbindUnreadyEvent)) { this.unbindUnreadyEvent(); }
            });
        }
        getRateQuotes(apply: boolean): void {
            if (this.isGettingRateQuotes) {
                // Don't fire twice
                return;
            }
            this.isGettingRateQuotes = true;
            this.setRunning();
            if (apply) {
                this.cvCartService.applyShippingContact(this.type)
                    .then(() => this.getRateQuotesInner())
                    .catch(reason => {
                        this.finishRunning(true, reason);
                        this.isGettingRateQuotes = false;
                    });
                return;
            }
            this.getRateQuotesInner();
        }
        getRateQuotesInner(): void {
            this.setRunning();
            this.cvApi.shopping.GetCurrentCartShippingRateQuotes({
                TypeName: this.type,
                Expedited: false
            }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.isGettingRateQuotes = false;
                    this.finishRunning(true, null, r && r.data && r.data.Messages);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.loaded,
                        this.rateQuotes,
                        this.selectedRateQuoteID);
                    return;
                }
                const rateQuotes = r.data.Result;
                if (rateQuotes && rateQuotes.length > 0) {
                    for (let i = 0; i < rateQuotes.length; i++) {
                        const r = rateQuotes[i];
                        r["original"] = r.Rate;
                        r["discounted"] = r.Rate;
                        const shippingDiscounts = (this.cvCartService.accessCart(this.type)
                            && this.cvCartService.accessCart(this.type).Discounts
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
                        this.cvCartService.accessCart(this.type).Totals.Shipping = selected.Rate;
                        this.cvCartService.recalculateTotals(this.type);
                    }
                    this.cvCartService.accessCart(this.type).RateQuotes = rateQuotes;
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
            const dto = <api.ApplyCurrentCartShippingRateQuoteDto>{
                TypeName: this.type,
                RateQuoteID: Number(this.selectedRateQuoteID),
                // TODO: RequestedShipDate
            };
            this.selectedRateQuoteID = Number(this.selectedRateQuoteID);
            this.cvApi.shopping.ApplyCurrentCartShippingRateQuote(dto).then(r => {
                if (!r.data.ActionSucceeded) {
                    this.cvViewStateService.processCEFActionResponseMessages(r.data);
                    this.finishRunning(true, null, r.data.Messages);
                    return;
                }
                this.finishRunning();
                this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.rateQuoteSelected,
                    this.type,
                    Number(this.selectedRateQuoteID));
                this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, this.type);
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Constructors
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $timeout: ng.ITimeoutService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvViewStateService: services.IViewStateService) {
            super(cefConfig);
            this.load();
        }
    }
}
