/**
 * @file framework/store/quotes/steps/_shared/viewTitle.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: Titles to the "accordion" panes
 */
module cef.store.quotes.steps.shared {
    cefApp.directive("cefSubmitQuoteStepViewTitle", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/quotes/steps/_shared/viewTitle.html", "ui")
    }));
}
