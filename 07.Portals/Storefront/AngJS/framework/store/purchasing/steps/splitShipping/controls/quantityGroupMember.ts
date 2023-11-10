module cef.store.purchasing.steps.splitShipping.controls {
    cefApp.directive("quantityGroupMember", (): ng.IDirective => ({
        restrict: "A",
        require: ["^form", "ngModel"],
        scope: { quantity: "=quantityGroup" },
        link(scope, el, attrs, ctrl: any) {
            scope.fc = ctrl[0];
            scope.mc = ctrl[1];
        }
    }));
}
