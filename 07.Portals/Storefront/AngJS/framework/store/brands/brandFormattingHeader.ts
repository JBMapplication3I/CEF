module cef.store.brands {
    class BrandFormattingHeaderController extends BrandFormattingBaseController {
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvCurrentBrandService: services.ICurrentBrandService) {
            super($rootScope, cefConfig, cvCurrentBrandService);
        }
    }

    cefApp.directive("cefBrandFormattingHeader", () => ({
        restrict: "A",
        scope: true,
        template: `<div ng-bind-html="bfhc.siteDomain.HeaderContent | trustedHtml"></div>`,
        controller: BrandFormattingHeaderController,
        controllerAs: "bfhc"
    }));
}
