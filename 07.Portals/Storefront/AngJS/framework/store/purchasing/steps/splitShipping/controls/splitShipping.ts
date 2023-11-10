module cef.store.purchasing.steps.splitShipping.controls {
    class SplitShippingController extends cart.views.CartController {
        // Properties
        contacts: api.AccountContactModel[];
        addressOptions: api.ContactModel[];
        /* TODO: Restore In Store Pickup and Ship to Store options
        showShipToStoreOption: boolean;
        shipToStoreOption: api.ContactModel;
        showInStorePickupOption: boolean;
        inStorePickupOption: api.ContactModel;
        */
        hideAddAddressOption: boolean;
        usePhonePrefixLookups: boolean;
        readyToLoadShippingRateQuotes: boolean;
        keyToFormName(key: string): string {
            return key.replace(/( |-|,|\{|\}|\:|\")/g, '_');
        }
        keyToGroupFormName(key: string): string {
            return "groupForm" + key.replace(/( |-|,|\{|\}|\:|\")/g, '_');
        }
        keyAndIndexToTargetFormDropDown(key: string, itemID: number, index: number): any {
            return this.forms.shipping
                [this.keyToGroupFormName(key)]
                [`itemForm${itemID}`]
                [`ddlDestinationForTarget_${itemID}_${index}`];
        }
        grouped: {
            key: string;
            value: (() => api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>)[];
        }[];
        get targetedCarts(): api.CartModel[] {
            return this.cvCartService.accessTargetedCarts();
        }
        set targetedCarts(newValue: api.CartModel[]) {
            this.cvCartService.overrideTargetedCarts(newValue);
        }
        initialized: boolean = false;
        preselectedID: number = null;
        selectedFullNameAndStreet1: string;
        /** NOTE: Returning a -1 means the value is invalid */
        get totalShippingRaw(): number {
            return this.cvCartService.totalTargetedCartsShippingRaw();
        }
        get totalShipping(): string {
            return this.cvCartService.totalTargetedCartsShipping();
        }
        get totalTaxes(): string {
            return this.cvCartService.totalTaxes(this.type);
        }
        get grandTotal(): string {
            return this.cvCartService.grandTotal(this.type);
        }
        private get step(): PurchaseStepSplitShipping {
            if (!this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps["Cart"]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepSplitShipping>
                _.find(this.cvPurchaseService.steps["Cart"],
                    x => x.name === this.cvServiceStrings.checkout.steps.splitShipping);
        }
        protected get specialInstructions(): string {
            if (this.cefConfig.purchase.showSpecialInstructions) {
                return this.step.specialInstructions;
            }
            return undefined;
        }
        protected set specialInstructions(newValue: string) {
            if (this.cefConfig.purchase.showSpecialInstructions) {
                this.step.specialInstructions = newValue;
            }
        }
        // Functions
        setItemAttribute(param, cart): void {
            this.cartItems.forEach(item => {
                if (item.ProductID === param.ProductID) {
                    item.SerializableAttributes = param.SerializableAttributes || {};
                }
                return item;
            })
            this.cvCartService.updateCartTargets(this.type, this.cartItems);
            this.cvCartService.overrideTargetedCarts(cart);
            this.cvCartService.updateCartTargets(cart.TypeKey, cart.SalesItems);
        }
        preselectValues(selected: api.AccountContactModel): void {
            if (!selected || !this.selectedFullNameAndStreet1) {
                // User wants to add a new address for preselect, deal with that before assigning
                this.checkAddressOption(-1, null);
                // NOTE: No action taken on fail (ignored)
                // NOTE: That function will change preselectValues value which will trigger another
                // call in to this function
                return;
            }
            this.contacts.push(selected);
            this.addressOptions.unshift(selected.Slave);
            // TODO: Warning modal if any targets already set that they will be overidden?
            this.cartItems.forEach(item => {
                if (!item.Targets) {
                    // NOTE: This should never happen, before this point, the UI has at least
                    // created a default target to use in initializeSalesItems
                    throw Error("There was not a targets list on this item");
                }
                if (!item.Targets.length) {
                    // NOTE: This should never happen, before this point, the UI has at least
                    // created a default target to use in initializeSalesItems
                    throw Error("There were no targets on this item in it's list");
                }
                item.Targets.forEach(target => {
                    target.DestinationContactID = item.ProductNothingToShip
                        ? this.cvCartService.accessCart(this.type).BillingContact
                            ? this.cvCartService.accessCart(this.type).BillingContact.ID
                            : selected.Slave.ID
                        : selected.Slave.ID;
                    this.checkAddressOption(target.DestinationContactID, target);
                });
            });
        }
        addShippingTarget(item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>): number {
            this.setRunning(this.$translate("ui.storefront.common.Analyzing.Elipses"));
            if (!item.Targets) {
                // NOTE: This should never happen, before this point, the UI has at least
                // created a default target to use in initializeSalesItems
                throw Error("There was not a targets list on this item");
            }
            if (!item.Targets.length) {
                // NOTE: This should never happen, before this point, the UI has at least
                // created a default target to use in initializeSalesItems
                throw Error("There were no targets on this item in it's list");
            }
            // Generate a target with no quantity, the allocate method will assign a value if it can
            const newTarget = this.targetFactory(0);
            if (this.allocateQuantity(item, newTarget, 1)) {
                // We successfully allocated, so we can put it in the list of targets
                const resultA = item.Targets.push(newTarget);
                this.targetedCarts = []; // Wipe the list because it's going to need to be reanalyzed
                this.finishRunning();
                return resultA;
            }
            // We couldn't allocate, so just return the list as it was
            const resultB = item.Targets.length;
            this.finishRunning();
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
            return resultB;
        }
        removeShippingTarget(item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>, target: api.SalesItemTargetBaseModel): void {
            if (!item.Targets || !target) {
                return;
            }
            _.remove(item.Targets, item => item === target)
                .forEach((removed: any) => item.Targets[0].Quantity = item.Targets[0].Quantity + removed.Quantity);
            this.targetedCarts = []; // Wipe the list because it's going to need to be reanalyzed
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
        }
        targetFactory(quantity: number, contactID: number = null): api.SalesItemTargetBaseModel {
            return <api.SalesItemTargetBaseModel>{
                Active: true,
                ID: 0,
                NothingToShip: false,
                CreatedDate: new Date(),
                DestinationContactID: contactID || null,
                DestinationContact: null,
                OriginProductInventoryLocationSectionID: null,
                MasterID: null,
                OriginStoreProductID: null,
                OriginVendorProductID: null,
                SelectedRateQuoteID: null,
                TypeID: null,
                TypeKey: this.cvServiceStrings.attributes.shipToHome,
                Quantity: quantity
            };
        }
        allocateQuantity(
            item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>,
            target: api.SalesItemTargetBaseModel,
            byQuantity: number = 1
            ): boolean {
            const totalQuantity = item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0);
            if (!(byQuantity <= (totalQuantity - 1))) {
                this.consoleDebug("invalid byQuantity for allocateQuantity, would attempt to cause over-allocation, blocking modification");
                return false;
            }
            if (!item.Targets) {
                this.consoleDebug("This item has no targets list, cannot modify quantity");
                return false;
            }
            return item.Targets.reduce((allocated: boolean, thisTarget) => {
                this.consoleDebug("Val-0: Checking to allocate");
                this.consoleDebug(thisTarget);
                if (allocated) {
                    this.consoleDebug("Val-1: Reduce already allocated");
                    return allocated;
                }
                if (thisTarget["$$hashKey"] === target["$$hashKey"]) {
                    this.consoleDebug("Val-2: Same target, skipping allocate");
                    return allocated;
                }
                if (!((thisTarget.Quantity - 1) >= byQuantity)) {
                    this.consoleDebug("Val-3: Would not be able to adjust this item up to allocate the new target down");
                    this.consoleDebug(`before: ${thisTarget.Quantity}`);
                    this.consoleDebug(`before: ${target.Quantity}`);
                    return allocated;
                }
                if (!((target.Quantity + byQuantity) < totalQuantity)) {
                    this.consoleDebug("Val-4: Would not be able to adjust this target to allocate to another item");
                    this.consoleDebug(`before: ${thisTarget.Quantity}`);
                    this.consoleDebug(`before: ${target.Quantity}`);
                    return allocated;
                }
                if (!((thisTarget.Quantity - byQuantity) > 0)) {
                    this.consoleDebug("Val-5: Would not be able to adjust this item down to allocate the other target up");
                    this.consoleDebug(`before: ${thisTarget.Quantity}`);
                    this.consoleDebug(`before: ${target.Quantity}`);
                    return allocated;
                }
                if (!((target.Quantity + byQuantity) > 0)) {
                    this.consoleDebug("Val-6: Would not be able to adjust this target down to allocate the other item up");
                    this.consoleDebug(`before: ${thisTarget.Quantity}`);
                    this.consoleDebug(`before: ${target.Quantity}`);
                    return allocated;
                }
                // All validations pass, do the allocation
                this.consoleDebug("Pass: Will do the adjustment now");
                this.consoleDebug("Val-7.3: Adjusting plain");
                thisTarget.Quantity -= byQuantity;
                target.Quantity += byQuantity;
                // Update the totals
                this.consoleDebug("Val-8: Updating the totals");
                allocated = true;
                return allocated;
            }, false);
        }
        modifyQuantity(item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>, target: api.SalesItemTargetBaseModel, byQuantity = 1): void {
            if (!item.Targets || !target) {
                this.consoleDebug("This item had no targets or there wasn't a target specified to modify, cannot modify quantity");
                return;
            }
            this.allocateQuantity(item, target, byQuantity);
            this.targetedCarts = []; // Wipe the list because it's going to need to be reanalyzed
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
        }
        initializeSalesItems(items: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]): ng.IPromise<void> {
            const debugMsg = `SplitShippingController.initializeSalesItems(items: ${items && items.length}): `;
            this.consoleDebug(`${debugMsg}entered`);
            if (!angular.isArray(items) || !items.length) {
                const cart = this.cvCartService.accessCart(this.type);
                if (!cart || !cart.SalesItems || !cart.SalesItems.length) {
                    this.consoleDebug(`${debugMsg}no sales items, rejecting`);
                    return this.$q.reject();
                }
                items = cart.SalesItems;
            }
            // Billing step updates the cart and pulls a fresh copy, that copy may not have
            // targets in it yet, so have to build them (again)
            ////if (this.initialized) {
            ////    return this.$q.resolve();
            ////}
            return this.$q((resolve, reject) => {
                this.consoleDebug(`${debugMsg}starting resolve promise`);
                ////this.initialized = true;
                let defaultShippingContactID: number = null;
                if (!this.viewState["analyzing"]) {
                    this.consoleDebug(`${debugMsg}not analyzing so calling set running with 'Initializing`);
                    this.setRunning(this.$translate("ui.storefront.common.Analyzing.Elipses"));
                } else {
                    this.consoleDebug(`${debugMsg}analyzing so not calling set running`);
                }
                this.consoleDebug(`${debugMsg}load cart`);
                this.loadCurrentCart(false).then(() => {
                    this.consoleDebug(`${debugMsg}pre-auth`);
                    this.cvAuthenticationService.preAuth().finally(() => {
                        this.consoleDebug(`${debugMsg}pre-auth done`);
                        const doneFn = () => {
                            this.consoleDebug(`${debugMsg}doneFn`);
                            items.map(item => {
                                // Check if the quantity changed since the last time the targets were generated
                                const totalQuantity = item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0);
                                if (item.Targets
                                    && item.Targets.length
                                    && _.sumBy(item.Targets, x => x.Quantity) !== totalQuantity) {
                                    // Reset targets because it changed
                                    item.Targets = null;
                                }
                                if (!item.Targets || !item.Targets.length) {
                                    // When there is no list, create one with a default Target that has the full
                                    // quantity. If we have a default shipping address to use, assign that.
                                    // WARNING! This is the only location where a targets list should be
                                    // initialized in the entire platform!
                                    item.Targets = [
                                        this.targetFactory(totalQuantity, defaultShippingContactID)
                                    ];
                                    return item;
                                }
                                // Check for and collapse duplicates in the full list, this is a processing issue
                                // that happens sometimes, but easily corrected by re-grouping
                                const grouped = _.groupBy(item.Targets, x => {
                                    return angular.toJson({
                                        typeKey: x.Type && x.Type.CustomKey || x.TypeKey,
                                        destID: x.DestinationContactID,
                                        nothingToShip: x.NothingToShip
                                    });
                                });
                                const replacementList = [];
                                Object.keys(grouped).forEach(key => replacementList.push(grouped[key][0]));
                                item.Targets = replacementList;
                                return item;
                            });
                            this.grouped = this.$filter("flatGroupBy")(
                                this.cartItems,
                                ["ProductNothingToShip"],
                                "splitShipping");
                            if (!this.viewState["analyzing"]) {
                                this.finishRunning();
                            } else {
                                this.consoleDebug(`${debugMsg}analyzing so not calling finishRunning`);
                            }
                            this.consoleDebug(`${debugMsg}resolve`);
                            resolve();
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                        };
                        if (!this.cvAuthenticationService.isAuthenticated()) {
                            this.consoleDebug(`${debugMsg}not authed, moving to donFn`);
                            doneFn();
                            return;
                        }
                        this.consoleDebug(`${debugMsg}is authed, moving to contact checks`);
                        this.cvAddressBookService.refreshContactChecks(false, "purchasing.steps.splitShipping.control").finally(() => {
                            this.consoleDebug(`${debugMsg}contact checks finished, checking for default shipping`);
                            if (!this.cvAddressBookService.defaultShipping
                                || !this.cvAddressBookService.defaultShipping.SlaveID) {
                                this.consoleDebug(`${debugMsg}no default shipping, moving to doneFn`);
                                doneFn();
                                return;
                            }
                            this.consoleDebug(`${debugMsg}have default shipping, setting and then moving to doneFn`);
                            defaultShippingContactID = this.cvAddressBookService.defaultShipping.SlaveID;
                            doneFn();
                        });
                    });
                }).catch(reason => {
                    console.error("Initialize sales items failed");
                    console.error(reason);
                    this.finishRunning(true, reason);
                    reject(reason);
                });
            });
        }
        generateAddressOptions(opts: api.AccountContactModel[]): ng.IPromise<void> {
            console.warn("generateAddressOptions entered");
            if (!angular.isArray(opts)) { return this.$q.resolve(); }
            console.warn("generateAddressOptions is setting running with label Initializing");
            this.setRunning("Initializing (Address Options)..."/*this.$translate("ui.storefront.common.Analyzing.Elipses")*/);
            return this.$q((resolve, reject) => {
                /* TODO: Restore In Store Pickup and Ship to Store options
                var inStorePickup = this.cvContactFactory.new();
                inStorePickup.CustomKey = this.cvServiceStrings.attributes.inStorePickup;
                inStorePickup.Address = null;
                var shipToStore = this.cvContactFactory.new();
                shipToStore.CustomKey = this.cvServiceStrings.attributes.shipToStore;
                shipToStore.Address = null;
                this.$q.all([
                    this.cvContactFactory.upsert(inStorePickup),
                    this.cvContactFactory.upsert(shipToStore)
                ]).then((responseArr: ng.IHttpPromiseCallbackArg<api.ContactModel>[]) => {*/
                    this.addressOptions = [...opts.map(item => item.Slave)];
                    /* TODO: Restore In Store Pickup and Ship to Store options
                    if (Boolean(this.showInStorePickupOption) === true) {
                        this.inStorePickupOption = responseArr[0].data;
                        this.addressOptions.unshift(this.inStorePickupOption);
                    }
                    if (Boolean(this.showShipToStoreOption) === true) {
                        this.shipToStoreOption = responseArr[1].data;
                        this.addressOptions.unshift(this.shipToStoreOption);
                    }*/
                    if (this.cefConfig.featureSet.addressBook.dashboardCanAddAddresses) {
                        this.$translate("ui.storefront.userDashboard2.controls.addressEditor2.AddANewAddress").then(translated => {
                            if (_.find(this.addressOptions, x => x.CustomKey === translated)) {
                                // Already added
                                this.finishRunning();
                                resolve();
                                return;
                            }
                            this.addressOptions.push(<api.ContactModel>{
                                ID: -1,
                                Active: true,
                                CreatedDate: new Date(),
                                CustomKey: translated,
                                SameAsBilling: false
                            });
                            this.finishRunning();
                            resolve();
                        }).catch(reason => { this.finishRunning(true, reason); reject(reason); });
                        return;
                    }
                    this.finishRunning();
                    resolve();
                /* TODO: Restore In Store Pickup and Ship to Store options
                }).catch(reason => this.finishRunning(true, reason));*/
            });
        }
        checkAddressOption(optionID: number, target: api.SalesItemTargetBaseModel): ng.IPromise<void> {
            this.targetedCarts = []; // Wipe the list because it's going to need to be reanalyzed
            if (!optionID) {
                return this.$q.reject("No option ID supplied");
            }
            return this.$q((resolve, reject) => {
                switch (optionID) {
                    case -1: {
                        this.cvAddressModalFactory(
                            this.$translate("ui.storefront.checkout.splitShipping.addressModal.EnterShippingDestination"),
                            this.$translate("ui.storefront.checkout.splitShipping.addressModal.AddAddress"),
                            null,
                            "SplitShipping",
                            false,
                            null
                        ).then((newAC: api.AccountContactModel) => {
                            this.setRunning(this.$translate("ui.storefront.checkout.splitShipping.SavingANewAddress.Ellipses"));
                            this.cvAuthenticationService.preAuth().finally(() => {
                                if (!this.cvAuthenticationService.isAuthenticated()) {
                                    // Can't store in address book, store in local memory only instead
                                    this.addressOptions.splice(this.addressOptions.length - 1, 0, newAC.Slave);
                                    newAC.SlaveID = newAC.Slave.ID = Math.min(_.minBy(this.addressOptions, x => x.ID).ID, -3) - 1
                                    if (this.preselectedID === -1) {
                                        this.preselectedID = newAC.SlaveID;
                                    }
                                    if (target) {
                                        // This will stack negative numbers so we have individual values to select by with the dropdowns
                                        target.DestinationContactID = newAC.SlaveID;
                                        target.DestinationContact = newAC.Slave;
                                        target.DestinationContactKey = newAC.Slave.CustomKey;
                                        target.TypeID = null;
                                        target.TypeKey = this.cvServiceStrings.attributes.shipToHome;
                                        target.TypeName = null;
                                        target.Type = null;
                                    }
                                    this.finishRunning();
                                    resolve();
                                    this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                                    return;
                                }
                                // Store in Account's Address Book
                                this.cvAuthenticationService.getCurrentAccountPromise().then(account => {
                                    var dto = newAC as api.CreateAddressInBookDto;
                                    dto.MasterID = account.ID;
                                    this.cvApi.geography.CreateAddressInBook(dto).then(r => {
                                        this.addressOptions.splice(this.addressOptions.length - 1, 0, r.data.Slave);
                                        if (this.preselectedID === -1) {
                                            this.preselectedID = r.data.SlaveID;
                                        }
                                        if (target) {
                                            target.DestinationContactID = r.data.SlaveID;
                                            target.DestinationContact = r.data.Slave;
                                            target.DestinationContactKey = r.data.Slave.CustomKey;
                                            target.TypeID = null;
                                            target.TypeKey = this.cvServiceStrings.attributes.shipToHome;
                                            target.TypeName = null;
                                            target.Type = null;
                                        }
                                        this.finishRunning();
                                        resolve();
                                        this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                                    }).catch(reason3 => {
                                        this.blankDestinationContactInfo(target);
                                        this.finishRunning(true, reason3);
                                        reject();
                                    });
                                }).catch(reason2 => {
                                    this.blankDestinationContactInfo(target);
                                    this.finishRunning(true, reason2);
                                    reject();
                                });
                            });
                        }).catch(() => this.blankDestinationContactInfo(target));
                        break;
                    }
                    /* TODO: Restore In Store Pickup and Ship to Store options
                    case this.shipToStoreOption.ID: {
                        // TODO: Use the address of the store
                        this.selectDestinationAndTypeKey(target, this.shipToStoreOption.ID, this.cvServiceStrings.attributes.shipToStore);
                        break;
                    }
                    case this.inStorePickupOption.ID: {
                        // TODO: Use the address of the store
                        this.selectDestinationAndTypeKey(target, this.inStorePickupOption.ID, this.cvServiceStrings.attributes.inStorePickup);
                        break;
                    }
                    */
                    default: {
                        this.selectDestinationAndTypeKey(target, optionID, this.cvServiceStrings.attributes.shipToHome);
                        break;
                    }
                }
            });
        }
        protected add(): void {
            this.cvAddressModalFactory(
                this.$translate("ui.storefront.userDashboard2.controls.addressEditor2.AddANewAddress"),
                this.$translate("ui.storefront.checkout.splitShipping.addressModal.AddAddress"),
                null,
                "PurchasingShipping",
                false,
                null
            ).then((newEntry) => this.cvAddressBookService.addEntry(newEntry));
        }
        private blankDestinationContactInfo(target: api.SalesItemTargetBaseModel): void {
            if (!target) { return; }
            target.DestinationContactID = null;
            target.DestinationContact = null;
            target.DestinationContactKey = null;
        }
        private selectDestinationAndTypeKey(target: api.SalesItemTargetBaseModel, optionID: number, typeKey: string): void {
            target.DestinationContactID = optionID;
            target.DestinationContact = angular.fromJson(angular.toJson(_.find(this.addressOptions, x => x.ID == optionID)));
            target.DestinationContactKey = target.DestinationContact && target.DestinationContact.CustomKey;
            target.TypeID = null;
            target.TypeKey = typeKey;
            target.TypeName = null;
            target.Type = null;
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
        }
        submit(): void {
            this.viewState["analyzing"] = true;
            this.setRunning(this.$translate("ui.storefront.common.Analyzing.Elipses"));
            // Update the items
            const updatedItems = this.cartItems;
            for (let i = 0; i < updatedItems.length; i++) {
                for (let j = 0; j < this.grouped.length; j++) {
                    for (let k = 0; k < this.grouped[j].value.length; k++) {
                        const item = this.grouped[j].value[k]();
                        if (updatedItems[i].ID !== item.ID) { continue; }
                        updatedItems[i] = item;
                    }
                }
            }
            this.cvCartService.updateCartTargets(this.type, updatedItems).then(() => {
                const dto: api.AnalyzeCurrentCartToTargetCartsDto = {
                    WithCartInfo: { CartTypeName: this.type },
                    IsSameAsBilling: false,
                    IsPartialPayment: false,
                    ResetAnalysis: true, // Clear previous target setups
                };
                // In case the called finishRunning, set us back into running state
                this.setRunning(this.$translate("ui.storefront.common.Analyzing.Elipses"));
                // Analyze and make all the separate carts
                this.targetedCarts = []; // Clear so UI doesn't add carts on re-submit
                this.cvApi.providers.AnalyzeCurrentCartToTargetCarts(dto).then(r => {
                    if (!r.data.ActionSucceeded) {
                        this.finishRunning(true, null, r.data.Messages);
                        return;
                    }
                    this.targetedCarts = r.data.Result;
                    this.$q.all(this.targetedCarts
                            .filter(x => x.ID != null)
                            .map(x => this.cvApi.shopping.GetCartItems({ Active: true, AsListing: true, MasterID: x.ID })))
                        .then((rarr: ng.IHttpPromiseCallbackArg<api.CartItemPagedResults>[]) => {
                            rarr.forEach(pagedResult => {
                                if (!pagedResult || !pagedResult.data || !pagedResult.data.Results || !pagedResult.data.TotalCount) {
                                    console.warn("No results on one of the paged results that should have had children!");
                                    return;
                                }
                                const i = _.findIndex(this.targetedCarts, y => y.ID == pagedResult.data.Results[0].MasterID);
                                this.targetedCarts[i].SalesItems = pagedResult.data.Results;
                            });
                            this.$q.all(this.targetedCarts
                                    .map(x => this.cvApi.shopping.CurrentCartGetShippingContact({
                                        TypeName: x.TypeName
                                    })))
                                .then((rArr: ng.IHttpPromiseCallbackArg<api.CEFActionResponseT<api.ContactModel>>[]) => {
                                    for (let i = 0; i < rArr.length; i++) {
                                        if (!rArr[i].data.ActionSucceeded) {
                                            this.targetedCarts[i].ShippingContactID = null;
                                            this.targetedCarts[i].ShippingContact = null;
                                            continue;
                                        }
                                        this.targetedCarts[i].ShippingContact = rArr[i].data.Result;
                                        this.targetedCarts[i].ShippingContactID = rArr[i].data.Result.ID;
                                    }
                                    this.viewState["analyzing"] = false;
                                    this.finishRunning();
                                    this.readyToLoadShippingRateQuotes = true;
                                    this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                                    this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.ready,
                                        this.cvCartService.accessCart(this.type),
                                        false); // Don't reapply the shipping contact info to the cart
                                });
                        }).catch(reason3 => this.finishRunning(true, reason3));
                }).catch(reason2 => {
                    // Retry if specific error
                    if (reason2 && (angular.toJson(reason2).indexOf("[4]") !== -1 || angular.toJson(reason2).indexOf("[2]") !== -1)) {
                        this.submit();
                    } else {
                        this.finishRunning(true, reason2);
                    }
                });
            }).catch(reason => angular.isArray(reason) ? this.finishRunning(true, null, reason) : this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $window: ng.IWindowService,
                protected readonly $location: ng.ILocationService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvMessageModalFactory: modals.IMessageModalFactory,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvStoreLocationService: services.IStoreLocationService,
                protected readonly cvInventoryService: services.IInventoryService,
                protected readonly cvContactFactory: factories.IContactFactory,
                protected readonly cvAddressModalFactory: store.modals.IAddressModalFactory,
                protected readonly cvAddressBookService: services.IAddressBookService,
                protected readonly cvFacebookPixelService: services.IFacebookPixelService,
                protected readonly cvGoogleTagManagerService: services.IGoogleTagManagerService,
                protected readonly cvSecurityService: services.ISecurityService,
                private readonly cvPurchaseService: services.IPurchaseService) {
            super($rootScope, $scope, $q, $filter, $window, $translate, cvApi,
                cefConfig, cvServiceStrings, cvCartService, cvMessageModalFactory, cvAuthenticationService, cvStoreLocationService,
                cvInventoryService, cvFacebookPixelService, cvGoogleTagManagerService, cvSecurityService, cvAddressBookService);
            // this.showShipToStoreOption = this.cefConfig.featureSet.shipping.shipToStore.enabled;
            // this.showInStorePickupOption = this.cefConfig.featureSet.shipping.inStorePickup.enabled;
            this.usePhonePrefixLookups = this.cefConfig.featureSet.contacts.phonePrefixLookups.enabled;
            this.hideAddAddressOption = !this.cefConfig.featureSet.addressBook.dashboardCanAddAddresses;
            const cart = this.cvCartService.accessCart(this.type)
            const unbind1 = $scope.$watchCollection(() => this.cartItems, newValue => this.initializeSalesItems(newValue));
            const unbind2 = $scope.$watchCollection(() => this.contacts, newValue => {
                if (!cart.SerializableAttributes || !cart.SerializableAttributes["orderRequestShippingcontactID"] || !cart.SerializableAttributes["orderRequestShippingcontactID"].Value) {
                    this.generateAddressOptions(newValue)
                }
            });
            if (this.cartItems && this.cartItems.length) {
                this.initializeSalesItems(this.cartItems);
            }
            if (cart.SerializableAttributes && cart.SerializableAttributes["orderRequestShippingcontactID"] && cart.SerializableAttributes["orderRequestShippingcontactID"].Value) {
                let orderRequestShippingcontactID = parseInt(cart.SerializableAttributes["orderRequestShippingcontactID"].Value);
                this.cvApi.accounts.GetAccountContactByID(orderRequestShippingcontactID).then(r => {
                    if (!r || !r.data) {
                        return
                    }
                    let contactsOverrideForOrderRequest: api.AccountContactModel[] = [r.data];
                    this.generateAddressOptions(contactsOverrideForOrderRequest)
                });
            }
            else if (this.contacts && this.contacts.length) {
                this.generateAddressOptions(this.contacts);
            }
            // Event for rate quote selection already handled to revalidate step
            this.$scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
    }

    cefApp.directive("cefPurchaseSplitShipping", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            // Inherited
            type: "=",
            apply: "=",
            // This directive specific
            contacts: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/splitShipping/controls/splitShipping.html", "ui"),
        controller: SplitShippingController,
        controllerAs: "ssCtrl",
        bindToController: true
    }));
}
