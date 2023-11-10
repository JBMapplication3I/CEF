/**
 * @file framework/store/user/registration/steps/_shared/viewTitle.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Register for the site: The steps of the wizard
 */
module cef.store.user.registration.steps.shared {
    cefApp.directive("cefRegStepViewTitle", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/_shared/viewTitle.html", "ui")
    }));
}
