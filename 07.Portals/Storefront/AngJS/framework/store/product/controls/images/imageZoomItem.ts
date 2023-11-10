module cef.store.product.controls.images {
    cefApp.directive("cefImageZoomItem", () => ({
        restrict: "A",
        require: "^cefImageZoomContainer",
        link: (scope, el, attrs, ctrl) => {
            (ctrl as any).galleryItems.push(el);
        }
    }));
}
