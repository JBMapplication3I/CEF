/**
 * @file framework/store/navigation/applicationsMenuBar.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Applications menu bar class
 */
 module cef.store.navigation {
    class CategoriesMenuBarController extends core.TemplatedControllerBase {
        // Properties
        render: string; // Populated by Scope using bindToController: true
        renderInner: string; // Populated by Scope using bindToController: true
        behavior: string; // Populated by Scope using bindToController: true
        text: string; // Populated by Scope using bindToController: true
        menuTree: Array<api.CategoryModel>; // Populated by web service call
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
            if (this.behavior.indexOf('fast-hover') === -1) { return; }
            this.isOpen = true;
        }
        // Constructor
        constructor(
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.setRunning();
            cvApi.categories.GetCategoriesThreeLevels({
                Active: true,
                // AsListing: true,
                IncludeInMenu: true,
                IncludeChildrenInResults: true,
                TypeID: 2,
            }).then(r => {
                this.menuTree = r.data;
                if (this.text) {
                    this.displayText = this.text;
                    this.finishRunning();
                    return;
                }
                $translate("ui.storefront.menu.cefNavigationProduct.shopApplications")
                    .then(translated => {
                        this.displayText = translated as string;
                        this.finishRunning();
                    })
                    .catch(err => {
                        this.displayText = "Shop Applications";
                    });
            });
        }
    }

    cefApp.directive("cefApplicationsMenuBar", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            render: "@",
            renderInner: "@?",
            behavior: "@",
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
        replace: true, // Required so the menu content is placed in the correct location for bootstrap nav management
        transclude: true, // Required for proper overriding to different menus for different clients
        templateUrl: $filter("corsLink")("/framework/store/navigation/applicationsMenuBar.html", "ui"),
        controller: CategoriesMenuBarController,
        controllerAs: "cefApplicationsMenuBarCtrl",
        bindToController: true
    }));
}
