module cef.admin.api {
    /** Short name for use everywhere */
    export interface ICEFAPI extends IClarityEcomService { }

    export class CEFAPIProvider {
        // Properties
        private generated: ClarityEcomService = null;
        // Functions
        private fullInner(urlConfig: core.IUrlConfig): string {
            if (!urlConfig.host && !urlConfig.root) {
                // We don't have a static url or root path to apply
                return "";
            }
            if (!urlConfig.host && urlConfig.root) {
                // We don't have a static url, but we do have a root path to apply
                return urlConfig.root;
            }
            if (urlConfig.host && !urlConfig.root) {
                // We don't have a root path, but we do have a static url to apply
                return urlConfig.host;
            }
            // We have both a root path and a static url to apply
            return urlConfig.host + urlConfig.root;
        }
        // Constructor
        public $get($http: ng.IHttpService, cefConfig: core.CefConfig) {
            return this.generated
                || (this.generated = new ClarityEcomService($http, this.fullInner(cefConfig.routes.api)));
        }
    }
}
