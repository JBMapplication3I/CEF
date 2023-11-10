module cef.store.core {
    /**
     * This is a generic controller that can be used to apply angular
     * to any random element that wouldn't otherwise have a controller,
     * like in the skin files above the CEF controls
     */
    export class GenericController {
        constructor(
            // All used by UI
            private readonly $rootScope: ng.IRootScopeService,
            protected readonly cefConfig: core.CefConfig,
            private readonly cvApi: api.ICEFAPI,
            private readonly cvAuthenticationService: services.IAuthenticationService) {
        }
    }
}
