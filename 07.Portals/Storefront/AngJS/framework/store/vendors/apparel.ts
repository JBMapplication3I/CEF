/**
 * @file teamEquipment.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Team Equipment directive class
 */
module cef.store.vendors {
  class ApparelController extends core.TemplatedControllerBase {
    // Properties
    categories: Array<api.CategoryModel>;
    // Functions
    // Events
    load() {
      this.cvApi.categories
        .GetCategories({
          Active: true,
          IncludeChildrenInResults: false,
        })
        .then((r) => {
          var results = r.data.Results;
          var categoriesArray = [];
          results.forEach((item) => {
            if (item.TypeKey === "APPAREL") {
              categoriesArray.push(item);
            }
          });
          this.categories = categoriesArray;
        });
    }

    // Constructor
    constructor(
      private readonly $filter: ng.IFilterService,
      private readonly $state: ng.ui.IStateService,
      private readonly cvApi: api.ICEFAPI,
      protected readonly cefConfig: core.CefConfig
    ) {
      super(cefConfig);
      this.load();
    }
  }

  cefApp.directive(
    "cefApparel",
    ($filter: ng.IFilterService): ng.IDirective => ({
      restrict: "EA",
      templateUrl: $filter("corsLink")(
        "/framework/store/vendors/apparel.html",
        "ui"
      ),
      controller: ApparelController,
      controllerAs: "apparelCtrl",
      bindToController: true,
    })
  );
}
