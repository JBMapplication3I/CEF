/**
 * @file framework/store/navigation/categoriesMenuInner.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Categories menu inner class
 */
module cef.store.navigation {
    class CategoriesMenuInnerController extends core.TemplatedControllerBase {
        // Properties
        el: ng.IAugmentedJQuery; // Populated by Link Pre and Post functions
        get item(): ng.IAugmentedJQuery { return this.el; }; // Points to el
        set item(newValue: ng.IAugmentedJQuery) { this.el = newValue; }; // Points to el
        active = false; // Lets us know if this controller has been created or destroyed already
        activateCallback: () => void; // Do something after we activate, Populated by Linker based on Render mode
        deactivateCallback: () => void; // Do something after we deactivate, Populated by Linker based on Render mode
        group: string; // Populated by Scope using bindToController
        groupDeactivateCallback: () => void; // Do something when our group deactivates, Populated by Linker based on Render mode
        render: string; // Populated by Scope using bindToController
        behavior: string; // Populated by Scope using bindToController
        limitChildren: number; // Populated by Scope using bindToController
        limitGrandChildren: number; // Populated by Scope using bindToController
        activeTab: number; // Set active ID
        scope: ng.IScope;
        config: {
            render: string | boolean;
            behavior?: string;
            group?: string;
            paneTemplate?: string;
            paneId?: string;
            templateUrl?: string;
            limitChildren?: number;
            limitGrandChildren?: number;
        };
        menuConfig: any; // Extends the config with properties on this object, Populated by Scope using bindToController
        menuData: Array<api.CategoryModel>; // Populated by Scope using bindToController
        // Functions
        activate = (): void => {
            this.active = true;
            if (!this.config.render) { return; }
            if (this.config.render === "mega-tabs" || this.config.render === "category-grid") {
                // Do Nothing
            } else if (this.config.render === "dropdown") {
                // A basic UL that follows a very common pattern for CSS menus.
                // Find or Generate some styles at - http://cssmenumaker.com/
                this.activateCallback = () => {
                    this.el.addClass("active");
                    this.item.show();
                };
                this.deactivateCallback = () => {
                    this.el.removeClass("active");
                    this.item.hide();
                };
            } else if (this.config.render === "include") {
                /* This directive needs specific information in the menuConfig or it won't work.
                // templateUrl -- This string is a template url path.
                // It inserts the contents inline and does a basic show/hide.*/
                if (this.config.templateUrl && angular.isString(this.config.templateUrl)) {
                    this.$http.get<string>(this.config.templateUrl, { cache: this.$templateCache }).then(response => {
                        this.el.after(this.$compile(response.data)(this.$scope) as any);
                    });
                }
                this.activateCallback = () => {
                    this.el.addClass("active");
                    this.item.show();
                };
                this.deactivateCallback = () => {
                    this.el.removeClass("active");
                    this.item.hide();
                };
            } else if (this.config.render === "static") {
                // TODO: This adds a compile function that isn't in the other render types
            }
            if (!angular.isFunction(this.activateCallback)) { return; }
            this.activateCallback();
        };
        deactivate = (): void => {
            if (this.config.group) {
                this.$rootScope.$broadcast(this.cvServiceStrings.events.menus.deactivateGroups, this.config.group);
            }
            if (this.active) {
                this.active = false;
                this.$rootScope.$broadcast(this.cvServiceStrings.events.menus.deactivateChildren);
            }
            if (angular.isFunction(this.deactivateCallback)) {
                this.deactivateCallback();
            }
        };
        inGroup = (whatGroup: string): boolean => {
            return this.config.group
                && this.config.group === whatGroup;
        };
        navigate = (item: api.MenuCategoryModel, $event: ng.IAngularEvent, level: number = 1): void => {
            if ($event['view'] && $event['view']['outerWidth'] && $event['view']['outerWidth'] < 992) {
                return;
            }
            let builtPath = this.cefConfig.routes.catalog.root + ":";
            let params = {
                'term': null,
                categoriesAny: null,
                categoriesAll: null,
                pricingRanges: null,
                attributesAny: null,
                attributesAll: null,
                brandName: null
            };
            if (item.HasChildren) {
                builtPath += "searchCatalog.categories.results.both";
            } else {
                builtPath += "searchCatalog.products.results.both";
            }
            params["category"] = `${item.Name}|${item.CustomKey}`;
            this.$filter("goToCORSLink")(builtPath, "site", "primary", false, params);
        };
        // Constructors
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $http: ng.IHttpService,
                private readonly $compile: ng.ICompileService,
                private readonly $templateCache: ng.ITemplateCacheService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
            // Default configuration.
            this.config = {
                render: ""
            };
            // Merge passed in configuration
            if (angular.isFunction(this.menuConfig)) {
                const mc = this.menuConfig();
                if (mc && angular.isObject(mc)) {
                    angular.extend(this.config, mc);
                }
            }
            // Do some checking to make sure we have the directive names right
            // Defined attributes override passed in configuration, which overrides the defaults
            this.config.render = this.render || this.config.render || "mega-tabs";
            this.config.behavior = this.behavior || this.config.behavior || "['fast-hover','toggle']";
            this.config.group = this.group || this.config.group || null;
            this.config.limitChildren = this.limitChildren || this.config.limitChildren || 12;
            this.config.limitGrandChildren = this.limitGrandChildren || this.config.limitGrandChildren || 4;
            // Placeholders
            this.el = angular.element();
            this.scope = $scope;
            // State management
            $scope.$on(this.cvServiceStrings.events.menus.deactivateChildren, (): void => {
                if (!this.active) { return; }
                this.active = false;
                this.deactivate();
            });
            $scope.$on(this.cvServiceStrings.events.menus.deactivateGroups, (e, args): void => {
                if (!this.active || !this.inGroup(args)) { return; }
                this.active = false;
                this.deactivate();
            });
            this.activate();
        }
    }

    cefApp.directive("cefCategoriesMenuInner", ($filter: ng.IFilterService): ng.IDirective => ({
        require: "^^cefCategoriesMenuBar",
        restrict: "A",
        scope: {
            render: "@",
            behavior: "@",
            group: "@?",
            menuConfig: "@?",
            menuData: "=",
            limitChildren: "=?",
            limitGrandChildren: "=?"
        },
        transclude: true, // Required for overrides
        /* replace: true, // Required to place objects in the correct location for rendering */
        templateUrl: $filter("corsLink")("/framework/store/navigation/categoriesMenuInner.html", "ui"),
        controller: CategoriesMenuInnerController,
        controllerAs: "categoriesMenuInnerCtrl",
        bindToController: true,
        link: {
            pre($scope: ng.IScope, el: ng.IAugmentedJQuery, attr: ng.IAttributes, ctrl: CategoriesMenuInnerController) {
                ctrl.el = el;
            }
        }
    }));
}

