module cef.store.product.controls.images {
    cefApp.directive("cefImageZoom", () => ({
        // Depends on the elevateZoom jQuery plugin
        // This will be a bit lame, but time is short.
        // The plugin wants an ID for gallery selector, so that's what it has to be for now
        restrict: "A",
        require: "^cefImageZoomContainer",
        link: (scope: any, el: any, attrs, ctrl: any) => {
            attrs.$observe("src", val => {
                // Might be overkill, but I really want to make sure all the dependent attributes have a chance to interpolate.
                if (val) {
                    scope.$evalAsync(() => ctrl.instance = el.elevateZoom(ctrl.config).data("elevateZoom"));
                }
                scope.advanceImage = ctrl.galleryStep;
            });
        }
    }));
}
