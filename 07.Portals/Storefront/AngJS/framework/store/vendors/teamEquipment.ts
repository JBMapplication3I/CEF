/**
 * @file teamEquipment.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Team Equipment directive class
 */
 module cef.store.vendors {
    class TeamEquipmentController extends core.TemplatedControllerBase {
        // Properties
        categories: Array<api.CategoryModel>;
        // Functions
        // Events
        load() {
            this.cvApi.categories.GetCategories({
                Active: true,
                IncludeChildrenInResults: false
            }).then(r => this.categories = r.data.Results);
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $state: ng.ui.IStateService,
                private readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefTeamEquipment", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/vendors/teamEquipment.html", "ui"),
        controller: TeamEquipmentController,
        controllerAs: "teamEquipmentCtrl",
        bindToController: true
    }));
}
