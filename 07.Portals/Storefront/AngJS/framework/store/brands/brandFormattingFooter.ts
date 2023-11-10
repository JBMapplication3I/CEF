module cef.store.brands {
    class BrandFormattingFooterController extends BrandFormattingBaseController {
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvCurrentBrandService: services.ICurrentBrandService) {
            super($rootScope, cefConfig, cvCurrentBrandService);
        }
    }

    cefApp.directive("cefBrandFormattingFooter", () => ({
        restrict: "A",
        template: `<div ng-bind-html="bffc.siteDomain.FooterContent | trustedHtml"></div>`,
        controller: BrandFormattingFooterController,
        controllerAs: "bffc"
    }));
}
