/**
 * @file framework/store/_core/uiRouterPlugins/uiSrefIf.ts
 * @desc UI SREF If directive. Only enable ui-sref if a condition is met, otherwise disable by removing the ui-sref
 * Original Code: https://stackoverflow.com/questions/28973530/disable-ui-sref-based-on-a-condition
 */
module cef.store.core.uiRouterPlugins {
    export const uiSrefIfFn = ($compile: ng.ICompileService): ng.IDirective => ({
        link: ($scope: ng.IScope, $element, $attrs) => {
            const uiSrefVal = $attrs["uiSrefVal"];
            const uiSrefIf  = $attrs["uiSrefIf"];
            $element.removeAttr("ui-sref-if");
            $element.removeAttr("ui-sref-val");
            $scope.$watch(
                () => $scope.$eval(uiSrefIf),
                (bool: boolean) => {
                    if (bool) {
                        $element.attr("ui-sref", uiSrefVal);
                    } else {
                        $element.removeAttr("ui-sref");
                        $element.removeAttr("href");
                    }
                    $compile($element)($scope);
                }
            );
        }
    });
}
