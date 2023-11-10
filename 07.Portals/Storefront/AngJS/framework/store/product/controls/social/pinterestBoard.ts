module cef.store.product.controls.social {
    export class PinterestBoardController {
        // Properties
        p: HTMLScriptElement;
        f: HTMLScriptElement;
        // Constructors
        constructor($timeout: ng.ITimeoutService) {
            $timeout(() => {
                this.f = document.getElementsByTagName("script")[0];
                this.p = document.createElement("script");
                this.p.type = "text/javascript";
                this.p.async = true;
                this.p.src = "//assets.pinterest.com/js/pinit.js";
                this.f.parentNode.insertBefore(this.p, this.f);
            }, 2000);
        }
    }

    cefApp.directive("cefPinterestBoard", (): ng.IDirective => ({
        restrict: "EA",
        replace: true,
        transclude: true, // Required
        // Note: This is a control-specific template that must remain inline
        template: "<ng-transclude></ng-transclude>",
        controller: PinterestBoardController,
        controllerAs: "pin"
    }));
}
