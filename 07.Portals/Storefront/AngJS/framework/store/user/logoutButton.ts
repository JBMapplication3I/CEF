// <copyright file="logoutButton.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>logout button class</summary>
module cef.store.user {
    cefApp.directive("cefLogoutButton", ($filter: ng.IFilterService, cvAuthenticationService: services.IAuthenticationService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/user/logoutButton.html", "ui"),
        controller: ($scope: ng.IScope) => {
            $scope["doLogout"] = () => {
                cvAuthenticationService.logout()
                    .then(() => $filter("goToCORSLink")("/"),
                          result => console.error(`logout failed: ${result}`))
                    .catch(result => console.error(`logout failed: ${result}`));
            }
        },
        controllerAs: "logoutButtonCtrl",
        bindToController: true
    }));
}
