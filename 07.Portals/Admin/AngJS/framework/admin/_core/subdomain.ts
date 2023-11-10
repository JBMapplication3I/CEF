module cef.admin.core {
    export const subdomainFactoryFn = ($location: ng.ILocationService, cefConfig: CefConfig): string => {
        const host = $location.host();
        const count = (host.match(new RegExp("\\.", "g")) || []).length;
        const rootSegmentCount = (cefConfig.useSubDomainForCookies
            ? null
            : cefConfig.usePartialSubDomainForCookiesRootSegmentCount) || 2;
        if (count <= rootSegmentCount - 1) {
            // No subdomain available, like 'claritydev.us'
            return null;
        }
        // At least one subdomain available like 'local.claritydev.us' or 'api.local.claritydev.us'
        const array = host.split(".");
        let retval = "";
        for (let i = 0; i < array.length - rootSegmentCount; i++) {
            if (i > 0) {
                // E.g. 'api.local', put the '.' back in
                retval += ".";
            }
            retval += array[i];
        }
        // E.g.- 'local' or 'api.local'
        return retval;
    };
}
