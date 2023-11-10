/**
 * @file framework/store/widgets/inWhitelist/inWhiteList.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc This directive publishes a local, non-isolated, scope variable: isInWhitelist.
 * The value is set based on a string of comma-delimited values (inWhitelist,
 * the directive/attribute volue) that are compared to the property referenced by the
 * attribute: whitelistProperty.
 */
module cef.store.widgets.inWhitelist {
    cefApp.directive("inWhitelist", () => ({
        restrict: "A",
        link: function ($scope, el, attrs) {
            $scope.isInWhitelist = false;
            let valueWhitelist = [];

            function getWatchProperty(path) {
                return path.split(".").reduce((acc, cur, idx, arr) => {
                    if (idx === 0) {
                        return $scope[cur] || $scope;
                    }
                    if (!acc[cur]) {
                        arr.splice(1); // eject early
                        return;
                    }
                    return acc[cur];
                }, $scope)
            }

            attrs.$observe("inWhitelist", (value: string) => {
                valueWhitelist = value.split(",").map((item) => item.trim());
            });

            $scope.$watch(() => getWatchProperty(attrs.whitelistProperty), (value) => {
                if (angular.isArray(valueWhitelist)) {
                    $scope.isInWhitelist = value && (valueWhitelist.indexOf(value) > -1);
                }
            });
        }
    }));
}