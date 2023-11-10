/**
 * @file framework/store/navigation/priceListMenuBar.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Categories menu bar class
 */
module cef.store.navigation {
    class PriceListMenuBarController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        behavior: string;
        text: string;
        // Properties
        menuTree: Array<api.MenuCategoryModel>; // Populated by web service call
        displayText: string;
        selectedAccountKey: string;
        menuList: Array<string> = [];
        private accessibleAccounts: Array<api.AccountModel> = [];
        private _isOpen = false;
        get isOpen(): boolean {
            return this._isOpen;
        }
        set isOpen(newValue: boolean) {
            this._isOpen = newValue;
            if (!this.firstOpenDone && this._isOpen) {
                // This says the menu needs to generate now, dthe delay lets
                // the page load faster
                this.firstOpenDone = true;
            }
        }
        firstOpenDone = false;
        // Functions
        openMenuIfAllowed(): void {
            if (this.behavior.indexOf("fast-hover") === -1) {
                return;
            }
            this.isOpen = true;
        }
        parseStringToArray(str: string): Array<string> {
            str = str.trim();
            const values = str.split(',');
            const result = [];
            for (let i = 0; i < values.length; i++) {
            const value = values[i].trim();
            if (value.toLowerCase() == "medsurg retail price list" || value.toLowerCase() == "ems retail price list") {
                continue;
            }
            result.push(value);
            }
            return result;
        }
        populateMenu(): ng.IPromise<void> {
            // Set running
            this.setRunning();
            const genErrStr = "An error occured while populating the price list menu.";
            return this.$q((resolve, reject) => {
                this.selectedAccountKey = this.$cookies.get("cefSelectedAffiliateAccountKey");
                if (this.selectedAccountKey) {
                    this.cvApi.accounts.GetAccountsForCurrentAccount({Active: true, AsListing: true}).then(r1 => {
                        if (!r1 || !r1.data) {
                            return;
                        }
                        this.accessibleAccounts = r1.data.Results;
                        let selectedAccount = this.accessibleAccounts.find(x => x.CustomKey === this.selectedAccountKey);
                        if (selectedAccount) {
                            this.cvApi.accounts.GetAccounts({ID: selectedAccount.ID}).then(r2 => {
                                if (!r2 || !r2.data) {
                                    return;
                                }
                                let account = r2.data.Results[0];
                                if (account?.SerializableAttributes && account.SerializableAttributes["priceLists"]?.Value) {
                                    this.menuList = this.parseStringToArray(account.SerializableAttributes["priceLists"]?.Value);
                                }
                            })
                        }
                        console.log(selectedAccount)
                        console.log(this.menuList)
                        this.finishRunning();
                        resolve();
                    })
                    .catch((error) => {
                        this.consoleError(error);
                        this.finishRunning(true, genErrStr);
                        reject(error);
                    });
                } else {
                    this.cvAuthenticationService.getCurrentAccountPromise().then(account => {
                        if (account?.SerializableAttributes && account.SerializableAttributes["priceLists"]?.Value) {
                            this.menuList = this.parseStringToArray(account.SerializableAttributes["priceLists"]?.Value);
                        }
                        console.log(account)
                        console.log(this.menuList)
                        this.finishRunning();
                        resolve();
                    })
                    .catch((error) => {
                        this.consoleError(error);
                        this.finishRunning(true, genErrStr);
                        reject(error);
                    });
                }
            });
        }
        // Constructor
        constructor(
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                readonly cvApi: api.ICEFAPI,
                protected readonly $q: ng.IQService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly $cookies: ng.cookies.ICookiesService) {
            super(cefConfig);
            this.populateMenu().then(() => {
                if (this.text) {
                    this.displayText = this.text;
                    this.finishRunning();
                    return;
                }
                $translate("ui.storefront.common.MyPriceLists")
                    .then(t => this.displayText = t)
                    .catch(_ => this.displayText = "Price Lists")
                    .finally(() => this.finishRunning());
            }).catch(reason => this.consoleError(reason));
        }
    }

    cefApp.directive("cefPriceListMenuBar", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            /**
             * Determines how the menu opens
             * @type {string}
             */
            behavior: "@",
            /**
             * Overriding text to display. When not set, will display "Products"
             * @type {string}
             */
            text: "=?",
            /**
             * How many children to display
             * @default 9
             * @type {number}
             */
            limitChildren: "=?",
            /**
             * How many grand-children to display
             * @default 4
             * @type {number}
             */
            limitGrandChildren: "=?"
        },
        // replace: true, // Required so the menu content is placed in the correct location for bootstrap nav management
        transclude: true, // Required for proper overriding to different menus for different clients
        templateUrl: $filter("corsLink")("/framework/store/navigation/priceListMenuBar.html", "ui"),
        controller: PriceListMenuBarController,
        controllerAs: "cefPriceListMenuBarCtrl",
        bindToController: true
    }));
}
