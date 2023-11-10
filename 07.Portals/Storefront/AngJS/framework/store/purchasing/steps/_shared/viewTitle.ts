/**
 * @file framework/store/purchasing/steps/_shared/viewTitle.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: Titles to the "accordion" panes
 */
module cef.store.purchasing.steps.shared {
    cefApp.directive("cefPurchaseStepViewTitle", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/_shared/viewTitle.html", "ui")
    }));
}
