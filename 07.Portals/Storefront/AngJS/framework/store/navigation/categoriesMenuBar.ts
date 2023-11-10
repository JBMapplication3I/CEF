/**
 * @file framework/store/navigation/categoriesMenuBar.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Categories menu bar class
 */
module cef.store.navigation {
    class CategoriesMenuBarController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        render: string;
        renderInner: string;
        behavior: string;
        text: string;
        // Properties
        menuTree: Array<api.MenuCategoryModel>; // Populated by web service call
        otherBrandTopLevel: api.MenuCategoryModel[];
        displayText: string;
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
        populateMenu(): ng.IPromise<void> {
            // Set running
            this.setRunning();
            const genErrStr = "An error occured while populating the categories menu.";
            return this.$q((resolve, reject) => {
                // Get current brand key
                let currentBrandKey = this.$rootScope["globalBrand"]?.CustomKey;
                // this.cvApi.brands.GetCurrentBrand().then(r1 => {
                //     if (!r1.data.ActionSucceeded || !r1.data.Result.CustomKey) {
                //         this.consoleError("Current brand custom key could not be resolved.");
                //         this.finishRunning(true, genErrStr);
                //         reject("Current brand custom key could not be resolved.");
                //         return;
                //     }
                //     currentBrandKey = r1.data.Result.CustomKey;
                //     // Get all brand keys
                //     let allBrandKeys: string[];
                //     this.cvApi.brands.GetBrands({
                //         Active: true,
                //         AsListing: true
                //     }).then(r2 => {
                //         if (!r2.data.Results) {
                //             this.consoleError("Current brand custom key could not be resolved.");
                //             this.finishRunning(true, genErrStr);
                //             reject("Current brand custom key could not be resolved.");
                //             return;
                //         }
                let brandKey = ["ems", "medsurg"];
                // if (currentBrandKey === "ems") {
                //     brandKey.push("ems");
                // } else if (currentBrandKey === "medsurg") {
                //     brandKey.push("medsurg");
                // }
                // Get menu categories (three levels) for categories with keys matching that of the brands

                this.cvApi.categories.GetMenuCategoriesThreeLevels({
                    Active: true,
                    AsListing: true,
                    IncludeInMenu: true,
                    IncludeChildrenInResults: true,
                    CustomKeys: brandKey,
                }).then(r3 => {
                    // Check results for empty
                    if ((r3?.data?.length ?? 0) == 0) {
                        this.consoleError("cvApi.categories.GetMenuCategoriesThreeLevels returned no results.");
                        this.finishRunning(true, genErrStr);
                        reject("cvApi.categories.GetMenuCategoriesThreeLevels returned no results.");
                        return;
                    }
                    // Initialize menu tree with empty array
                    this.menuTree = [];
                    let menuTreeFirstSection = [];
                    let menuTreeSecondSection = [];
                    // Push children of child categories of current brand category to menu tree
                    const currentBrandCategoryTreeArr = r3.data
                        .filter(x => x.CustomKey.toLowerCase() == currentBrandKey.toLowerCase())
                        ?? [];
                    if (currentBrandCategoryTreeArr?.length != 1) {
                        this.consoleError(
                            "When attempting to filter category results down to category"
                            + " matching current brand category key length was not 1.");
                        this.finishRunning(true, genErrStr);
                        reject("When attempting to filter category results down to category"
                            + " matching current brand category key length was not 1.");
                        return;
                    }
                    const currentBrandCategoryTree = currentBrandCategoryTreeArr[0];
                    menuTreeFirstSection.push(...currentBrandCategoryTree.Children);
                    // Push categories that are NOT the current brand category to the end
                    const otherBrandsCategoryTreeArr = r3.data
                        .filter(x => x.CustomKey.toLowerCase() != currentBrandKey.toLowerCase())
                        ?? [];
                    menuTreeSecondSection.push(...otherBrandsCategoryTreeArr);
                    // otherBrandsCategoryTreeArr.forEach(obct => {
                    //     menuTreeFirstSection.push(...obct.Children);
                    // });
                    // todo: sort on a/b/ customkey
                    // menuTreeFirstSection.sort(a, b => {
                    //     return -1;
                    //     return 0;
                    //     return 1;
                    // });
                    this.menuTree.push(...menuTreeFirstSection, ...menuTreeSecondSection)
                    // Finish running
                    this.finishRunning();
                    resolve();
                }).catch(reason3 => {
                    this.consoleError("Error on cvApi.categories.GetMenuCategoriesThreeLevels call:");
                    this.consoleError(reason3);
                    this.finishRunning(true, genErrStr);
                    reject(reason3);
                });
                //     }).catch(reason2 => {
                //         this.consoleError("Error on cvApi.brands.GetBrands call:");
                //         this.consoleError(reason2);
                //         this.finishRunning(true, genErrStr);
                //         reject(reason2);
                //     });
                // }).catch(reason => {
                //     this.consoleError("Error on cvApi.brands.GetCurrentBrand call:");
                //     this.consoleError(reason);
                //     this.finishRunning(true, genErrStr);
                //     reject(reason);
                // });
            });
        }
        // Constructor
        constructor(
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                readonly cvApi: api.ICEFAPI,
                protected $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService) {
            super(cefConfig);
            this.populateMenu().then(() => {
                if (this.text) {
                    this.displayText = this.text;
                    this.finishRunning();
                    return;
                }
                $translate("ui.storefront.common.Product.Plural")
                    .then(t => this.displayText = t)
                    .catch(_ => this.displayText = "Products")
                    .finally(() => this.finishRunning());
            }).catch(reason => this.consoleError(reason));
        }
    }

    cefApp.directive("cefCategoriesMenuBar", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            /**
             * Determines the initial layout of the menu
             * * "across" to list the product categories across the menu bar, each with a popout
             * of sub-categories that may have popouts to additional levels
             * * "down" to list the product categories in a single dropdown menu with popout
             * menus for children to additional levels
             * * "mega" to list the product categories in the left pane of a mega menu
             * @type {string}
             */
            render: "@",
            /**
             * Determines the initial layout of the inner menu
             * * "category-grid" to display a grid of categories
             * @type {string}
             */
            renderInner: "@?",
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
        templateUrl: $filter("corsLink")("/framework/store/navigation/categoriesMenuBar.html", "ui"),
        controller: CategoriesMenuBarController,
        controllerAs: "cefCategoriesMenuBarCtrl",
        bindToController: true
    }));
}
