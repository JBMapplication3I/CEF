/**
 * @file framework/admin/purchasing/steps/_shared/viewTitle.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: Titles to the "accordion" panes
 */
module cef.admin.purchasing.steps.shared {
    adminApp.directive("cefPurchaseStepViewTitle", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/_shared/viewTitle.html", "ui")
    }));
}
