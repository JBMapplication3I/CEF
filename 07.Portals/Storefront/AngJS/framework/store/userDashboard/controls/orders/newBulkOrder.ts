/**
 * @file framework/store/userDashboard/controls/orders/newBulkOrder.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc New Bulk Order directive for generating an order using users in the same account
 */
module cef.store.userDashbord.controls.orders {
    class NewBulkOrderController extends core.TemplatedControllerBase {
        // Properties
        orderKey: string;
        certifications: { [key: string]: string } = { };
        allSelected: boolean = false;
        userPaging: core.ServerSidePaging<api.UserModel, api.UserPagedResults>
        allUsers: number[] = [];
        importedUsers: number[] = [];
        selections: interfaces.IOption[];
        selected: { [key: string]: interfaces.IOption | interfaces.IStaticOption };
        positions: interfaces.IPosition[];
        key: string;
        // Functions
        toggleChooseAdd(): void {
            this.applyProductToOrder().then(() => {
                this.viewState["addProduct"] = false;
                this.viewState["addProductSummary"] = true;
                this.viewState["chooseAdd"] = true;
                this.viewState["anyUserAdd"] = false;
            });
            this.cvApi.shopping.ClearCurrentCart();
        }
        toggleChooseAddUsers(mode: string): void {
            this.viewState["chooseAdd"] = false;
            this.viewState["anyUserAdd"] = true;
            switch (mode) {
                case "allExisting": {
                    this.viewState["addUsersAllExisting"] = true;
                    this.loadAllUsersAndAddToOrder();
                    break;
                }
                case "selectFromExisting": {
                    this.viewState["addUsersSelectExisting"] = true;
                    this.setupUsersPaging();
                    break;
                }
                case "selectByImport": {
                    this.viewState["addUsersImport"] = true;
                    break;
                }
            }
        }
        loadAllUsersAndAddToOrder(): void {
            this.setRunning();
            this.cvApi.accounts.GetAllUserIDsForCurrentAccount().then(r => {
                this.allUsers = r.data;
                this.addUsersToOrder();
                this.addToCart();
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        setupUsersPaging(): void {
            this.setRunning();
            this.userPaging = new core.ServerSidePaging<api.UserModel, api.UserPagedResults>(
                this.$rootScope,
                this.$scope,
                this.$filter,
                this.$q,
                this.cvServiceStrings,
                this.cvApi.accounts.GetUsersForCurrentAccount,
                8,
                "users",
                "IDOrUserNameOrCustomKeyOrEmailOrContactName");
        }
        toggleAllSelected(): void {
            if (this.viewState["addUsersAllExisting"]) {
                this.allSelected = true;
                return;
            }
            this.allSelected = !this.allSelected;
            this.userPaging.dataUnpaged.forEach(x => x["selected"] = this.allSelected);
            this.addUsersToOrder();
        }
        getSelectedUsers(): Array<number> {
            if (this.viewState["addUsersAllExisting"]) {
                return this.allUsers;
            }
            if (this.viewState["addUsersImport"]) {
                return this.importedUsers;
            }
            return this.userPaging.dataUnpaged.filter(x => x["selected"]).map(x => x.ID);
        }
        addUsersToOrder(): void {
            if (this.orderKey) {
                this.cvCreateBulkOrderService.merge(
                    this.orderKey,
                    { users: this.getSelectedUsers() });
            }
        }
        applyProductToOrder(): ng.IPromise<void> {
            if (!this.key) { return this.$q.reject(); }
            return this.$q((resolve, reject) => {
                this.setRunning();
                this.cvApi.products.CheckProductExistsByKey(this.key).then(r => {
                    if (!r || !r.data) {
                        this.finishRunning(true, this.$translate(`Product ${this.key} Not Found`));
                        reject();
                        return;
                    }
                    this.cvCreateBulkOrderService.merge(this.orderKey, { productID: r.data })
                    this.finishRunning();
                    resolve();
                }).catch(reason => { this.finishRunning(true, reason); reject(reason); });
            });
        }
        addToCart(): void {
            const data = this.cvCreateBulkOrderService.get(this.orderKey);
            if (!data.productID || !data.users || !data.users.length) {
                return;
            }
            this.setRunning();
            const array = data.users.map(userID => {
                return <services.IAddCartItemParams>{
                    userID: userID,
                    SerializableAttributes: {
                        "UseTargetUserPricing": {
                            ID: 0,
                            Key: "UseTargetUserPricing",
                            Value: "true"
                        },
                        "AddedViaBulkOrder": {
                            ID: 0,
                            Key: "AddedViaBulkOrder",
                            Value: "true"
                        }
                    },
                    ForceUniqueLineItemKey: this.key + "|" + userID
                };
            });
            this.cvCartService.addCartItems(data.productID, this.cvServiceStrings.carts.types.cart, 1, array)
                .then(() => { this.finishRunning(); this.viewState["usersAdded"] = true; })
                .catch(reasonArr => this.finishRunning(true, reasonArr));
        }
        shouldHideDropdown(positionIndex: number): boolean {
            if (this.positions[positionIndex].hidden) {
                // Hidden by definition
                return true;
            }
            if (!positionIndex) {
                return false; // We can't check a previous one, assume good to show
            }
            if (!this.selected[this.positions[positionIndex - 1].name]) {
                // Hidden because previous hasn't been set yet
                return true;
            }
            const s = this.selected[this.positions[positionIndex - 1].name] as interfaces.IOption;
            // Check if only one in list from previous selection
            return s.data && s.data.options && s.data.options.length <= 1;
        }
        private selectTheDefault(
                nextPosition: interfaces.IPosition,
                options: Array<interfaces.IOption | interfaces.IStaticOption>
                ): interfaces.IOption | interfaces.IStaticOption {
            return angular.isFunction(nextPosition.defaultBy)
                ? _.find(options, x => nextPosition.defaultBy(x, this))
                : nextPosition.defaultFirst
                    ? options[0]
                    : nextPosition.defaultLast
                        ? options[options.length - 1]
                        : _.find(options, x => x.value === nextPosition.default);
        }
        updateTree(positionUpdated: number): void {
            this.setRunning();
            // Check if we aren't at the last position
            if (positionUpdated < this.positions.length - 1) {
                // Set the next one to the default if it has one
                const newValue = positionUpdated >= 0 ? this.selected[this.positions[positionUpdated].name] : null;
                const nextPositionIndex = positionUpdated + 1;
                const nextPosition = this.positions[nextPositionIndex];
                if (nextPosition.defaultBy || nextPosition.default || nextPosition.defaultFirst || nextPosition.defaultLast) {
                    let td: interfaces.IOption | interfaces.IStaticOption;
                    if (nextPosition.staticOptions && nextPosition.staticOptions.length) {
                        td = this.selectTheDefault(nextPosition, nextPosition.staticOptions);
                    } else if (newValue) {
                        if ((newValue as interfaces.IOption).data) {
                            td = this.selectTheDefault(nextPosition, (newValue as interfaces.IOption).data.options);
                        } else {
                            // TODO: Find static to non-static link, will have to dig through selections tree
                        }
                    } else {
                        td = this.selectTheDefault(nextPosition, this.selections);
                    }
                    if (td) {
                        this.selected[nextPosition.name] = td;
                        this.updateTree(nextPositionIndex); // Run the update for the next one
                        return;
                    }
                }
            }
            // Construct the Key based on selections
            let constructed = "";
            for (const key in this.selected) {
                if (!this.selected.hasOwnProperty(key)) {
                    continue;
                }
                const s = this.selected[key];
                if (!s) {
                    break;
                }
                constructed = `${constructed}${constructed === "" ? "" : "."}${s.value}`;
            }
            this.key = constructed;
            this.finishRunning();
        }
        private parseCSVToSelectionsArray(): interfaces.IRawSelection[] {
            const rawSelections: interfaces.IRawSelection[] = [];
            for (let i = 0; i < data.overridableSelections.length; i++) {
                const element = data.overridableSelections[i];
                const row = <interfaces.IRawSelection>{
                    sku: element[0],
                    visible: Boolean(element[1]),
                };
                for (let j = 2; j < element.length; j++) {
                    if (j % 2 == 0) {
                        const k = ((j - 2) / 2) + 1;
                        row["pValue" + k] = element[j];
                    } else {
                        const k = ((j - 1) / 2);
                        row["pLabel" + k] = element[j];
                    }
                }
                rawSelections.push(row);
            }
            return rawSelections;
        }
        private recurse(options: interfaces.IOption[], raw: interfaces.IRawSelection, index: number, combined: number): void {
            const name = "pValue" + index;
            if (angular.isUndefined(raw[name])) {
                // Too deep, stop
                return;
            }
            // If not present, add it
            if (this.positions[index - 1 - combined].combineWithNext) {
                const altIndex = index + 1;
                const name2 = "pValue" + altIndex;
                const combinedValue = raw[name] + "." + raw[name2];
                if (!options.some(x => x.value === combinedValue)) {
                    options.push({ value: combinedValue, data: { titleKey: raw["pLabel" + altIndex], options: [] } });
                }
                // Try to go deeper
                let pos = _.find(options, x => x.value === combinedValue);
                this.recurse(pos.data.options, raw, altIndex + 1, ++combined);
            } else {
                if (!options.some(x => x.value === raw[name])) {
                    options.push({ value: raw[name], data: { titleKey: raw["pLabel" + index], options: [] } });
                }
                // Try to go deeper
                let pos = _.find(options, x => x.value === raw[name]);
                this.recurse(pos.data.options, raw, index + 1, combined);
            }
        }
        private convertRawSelectionsArrayToOptionsTree(): interfaces.IOption[] {
            const tree: interfaces.IOption[] = [];
            const arr = this.parseCSVToSelectionsArray();
            for (let i = 0; i < arr.length; i++) {
                if (!arr[i].visible) { continue; } // Skip hidden items
                this.recurse(tree, arr[i], 1, 0);
            }
            return tree;
        }
        load(): void {
            this.setRunning();
            this.viewState["addProduct"] = true;
            this.viewState["addProductSummary"] = false;
            this.viewState["chooseAdd"] = false;
            this.viewState["anyUserAdd"] = false;
            this.viewState["addUsersAllExisting"] = false;
            this.viewState["addUsersSelectExisting"] = false;
            this.viewState["addUsersImport"] = false;
            this.viewState["usersAdded"] = false;
            this.positions = data.overridablePositions;
            this.selections = this.convertRawSelectionsArrayToOptionsTree();
            this.selected = { };
            for (let i = 0; i < this.positions.length; i++) {
                this.selected[this.positions[i].name] = null;
                // TODO: this.hide[i] = this.positions[i].hidden || current selection only has one value in the options array;
            }
            this.orderKey = this.cvCreateBulkOrderService.create("myOrder");
            this.updateTree(-1); // Kick off initial selection, will eventually call finishRunning
            const unbind1 = this.$scope.$on(this.cvServiceStrings.events.paging.searchComplete,
                () => this.finishRunning());
            const unbind2 = this.$scope.$on(this.cvServiceStrings.events.users.importedToAccount,
                (event: angular.IAngularEvent, users: number[]) => this.importedUsers = users);
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvCartService: services.ICartService,
                private readonly cvCreateBulkOrderService: services.ICreateBulkOrderService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefLocalAdminNewBulkOrder", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/orders/newBulkOrder.html", "ui"),
        controller: NewBulkOrderController,
        controllerAs: "newBulkOrderCtrl",
        bindToController: true
    }));
}
