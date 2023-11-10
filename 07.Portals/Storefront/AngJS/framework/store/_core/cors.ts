module cef.store.core {
    ////export var globalBrandSiteDomain: api.SiteDomainModel = null;

    export const corsLinkRootInnerFn = () => (urlConfig: IUrlConfig): string => {
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
    };

    export const corsSplitAreaAsNeededFn = (area: string, cefConfig: core.CefConfig): IUrlConfig =>
        area.indexOf(".") !== -1
            ? cefConfig.routes[area.split(".")[0]][area.split(".")[1]]
            : cefConfig.routes[area];

    export const corsLinkRootFn = (
            $filter: ng.IFilterService,
            $rootScope: ng.IRootScopeService,
            cefConfig: core.CefConfig) =>
            (area: string = "site", whichUrl: string = "primary"): string => {
        if (!area) { area = "site"; }
        const areaToUse = corsSplitAreaAsNeededFn(area, cefConfig);
        if (!areaToUse) {
            throw Error(`Area '${area}' does not exist on cefConfig.`);
        }
        if (!areaToUse.hostIsProvidedByLookup) {
            // We don't need to inject a host URL
            return $filter("corsLinkRootInner")(areaToUse);
        }
        // We have to inject a host URL to it
        let siteDomainToUse: api.SiteDomainModel;
        if ((areaToUse.hostIsProvidedByLookup as IUrlHostConfig).type === "ByBrand") {
            if (!$rootScope["globalBrandSiteDomain"]) {
                // We failed to get data, so just try going to it with a relative path
                return $filter("corsLinkRootInner")(areaToUse);
            } else {
                siteDomainToUse = $rootScope["globalBrandSiteDomain"];
            }
        } else {
            // We failed to get data, so just try going to it with a relative path
            return $filter("corsLinkRootInner")(areaToUse);
        }
        // We have the site domain data
        let host: string;
        switch (whichUrl.toLowerCase()) {
            case "alternate-1": { host = siteDomainToUse.AlternateUrl1; break; } // The ALternate Site
            case "alternate-2": { host = siteDomainToUse.AlternateUrl2; break; } // The CORS site
            case "alternate-3": { host = siteDomainToUse.AlternateUrl3; break; } // Unused
            case "primary": default: { host = siteDomainToUse.Url; break; } // This Shop site
        }
        while (host.endsWith("/")) {
            // Make sure we don't end up with 'domain.com//some-path'
            host = host.substring(0, host.length - 1);
        }
        // Go to the new absolute URL
        const altAreaToUse = angular.fromJson(angular.toJson(areaToUse));
        const hostNoProtocol = host.replace("https://", "").replace("http://", "");
        altAreaToUse.host = host;
        const result = $filter("corsLinkRootInner")(altAreaToUse);
        return result.replace(hostNoProtocol + "/" + hostNoProtocol, hostNoProtocol);
    };

    export const corsLinkFn = (
            $window: ng.IWindowService,
            $state: ng.ui.IStateService,
            $filter: ng.IFilterService,
            $rootScope: ng.IRootScopeService,
            cefConfig: core.CefConfig,
            $httpParamSerializer: (any) => string) =>
        (
            path: string,
            area: string = "site",
            whichUrl: string = "primary",
            noCache: boolean = false,
            stateParamsBody: { [key: string]: any } = null,
            state: ng.ui.IState = null
        ): string => {
            if (stateParamsBody && typeof(stateParamsBody) === "string") {
                // { 'category': 'something' } won't parse to an object
                // it has to be { "category": "something" }
                // However, sometimes there's ' or " in the text content which is
                // breaking the parse (unmatched open/close), so we need to handle
                // those by manually escaping them first, we'll re-inject them afterwards
                let alt = stateParamsBody as string;
                const regex1 = /'category':\s*'([A-Za-z0-9;:\/\s_®\%\$\#\@\!\&\*\(\)\[\]-]*)'([A-Za-z0-9:;\/\s_®\%\$\#\@\!\&\*\(\)\[\]-]+)\|([A-Za-z0-9:;\/\s_®\%\$\#\@\!\&\*\(\)\[\]-]*)'/g;
                const regex2 = /'category':\s*'([A-Za-z0-9;:\/\s_®\%\$\#\@\!\&\*\(\)\[\]-]*)(?:"|\")([A-Za-z0-9:;\/\s_®\%\$\#\@\!\&\*\(\)\[\]-]+)\|([A-Za-z0-9:;\/\s_®\%\$\#\@\!\&\*\(\)\[\]-]*)'/g;
                while (regex1.test(alt)) {
                    alt = alt.replace(regex1, `'category': '$1[apos]$2|$3'`);
                }
                while (regex2.test(alt)) {
                    alt = alt.replace(regex2, `"category": "$1[quot]$2|$3"`);
                }
                alt = alt
                    .replace(new RegExp("\'", "g"), "\"")
                    .replace(new RegExp("\\[apos\\]", "g"), "\'")
                    .replace(new RegExp("\\[quot\\]", "g"), "\"");
                try {
                    stateParamsBody = angular.fromJson(alt);
                } catch (e) {
                    console.error(e);
                }
            }
            let isState = false;
            // Specialized marker to inject this path as the Return URL
            const swapReturnUrlToken = (source: string): string => {
                if (!source) { return ""; }
                let retVal = source;
                if (retVal.indexOf("[ReturnUrl]") !== -1) {
                    if (state) {
                        retVal = retVal.replace(/\[ReturnUrl\]/, encodeURI($window.location.pathname + ':' + state.name));
                    } else {
                        retVal = retVal.replace(/\[ReturnUrl\]/, encodeURI($window.location.pathname));
                    }
                }
                if (retVal.indexOf("%5BReturnUrl%5D") !== -1) {
                    retVal = retVal.replace(/\%5BReturnUrl\%5D/, encodeURI($window.location.pathname));
                }
                // Strip '/' from the end
                while (retVal && retVal.endsWith("/")) {
                    retVal = retVal.substr(0, retVal.length - 1);
                }
                return retVal;
            };
            path = swapReturnUrlToken(path);
            if (path && path.indexOf("http") === 0) {
                // Absolute path, don't do anything else, just go
                return noCache ? (path + "?noCache=" + new Date().getTime()) : path;
            }
            if (path && path.indexOf(":") !== -1) {
                // Read UI States for the Dashboard/SearchCatalog
                const preStateUrl = path.split(":")[0];
                const stateName = path.split(":")[1];
                let stateToGoTo: ng.ui.IState = $state.get(stateName);
                if (!stateToGoTo && preStateUrl === "DashboardState") {
                    // Try again with 'userDashboard.' in front of it
                    stateToGoTo = $state.get(`userDashboard.${stateName}`);
                }
                if (!stateToGoTo && preStateUrl === "SearchCatalogState") {
                    // Try again with 'searchCatalog.' in front of it
                    stateToGoTo = $state.get(`searchCatalog.${stateName}`);
                }
                if (stateToGoTo) {
                    // Not in the Dashboard/SearchCatalog, create a Relative URL that could get into the Dashboard/SearchCatalog directly to the state
                    const url = $state.href(stateName, stateParamsBody);
                    if (preStateUrl === "DashboardState") {
                        path = `${cefConfig.routes.dashboard.root}${url}`;
                    } else if (preStateUrl === "SearchCatalogState") {
                        path = `${cefConfig.routes.catalog.root}${url}`;
                    } else {
                        path = (preStateUrl.startsWith("/") ? "" : "/") + `${preStateUrl}/${url}`;
                    }
                    isState = true;
                    // It's now a relative path, we may need to inject a host below, if we do, we can
                }
            }
            const areaToUse = corsSplitAreaAsNeededFn(area, cefConfig);
            if (!areaToUse) {
                throw Error(`Area '${area}' does not exist on cefConfig.`);
            }
            const appendQueryStringParams = (i: boolean, b: string, n: boolean, s, $httpParamSerializer: (any) => string): string => {
                if (i || !s || s == typeof(Object)) {
                    // There was a state, or there's no stateParamsBody, just return
                    return n ? (b + "?noCache=" + new Date().getTime()) : b;
                }
                // Convert stateParamsBody into query parameters instead
                if (n) {
                    // Merge in noCache if set
                    s["noCache"] = new Date().getTime();
                }
                const qs = $httpParamSerializer(s);
                return swapReturnUrlToken(b + (qs.startsWith("#!?") ? "" : (b.indexOf("images") !== -1 ? "?" : "#!?")) + qs).replace(".png/", ".png");
            };
            if (!areaToUse.hostIsProvidedByLookup) {
                // We don't need to inject a host URL from lookups
                const base2 = $filter("corsLinkRootInner")(areaToUse);
                let pathToUse2 = (path && path.startsWith("/") ? "" : "/") + (path || "")
                if (areaToUse.root && pathToUse2 && pathToUse2.startsWith(areaToUse.root)) {
                    pathToUse2 = pathToUse2.substring(areaToUse.root.length);
                }
                return swapReturnUrlToken(
                    appendQueryStringParams(
                        isState,
                        base2 + pathToUse2,
                        noCache,
                        stateParamsBody,
                        $httpParamSerializer));
            }
            // Go to the new absolute URL
            const root = corsLinkRootFn($filter, $rootScope, cefConfig)(area,
                (areaToUse.hostIsProvidedByLookup as IUrlHostConfig).whichUrl || whichUrl);
            const base3 = root;
            let pathToUse3 = (path && path.startsWith("/") ? "" : "/") + (path || "")
            if (areaToUse.root && pathToUse3 && pathToUse3.startsWith(areaToUse.root)) {
                pathToUse3 = pathToUse3.substring(areaToUse.root.length);
            }
            return swapReturnUrlToken(
                appendQueryStringParams(
                    isState,
                    base3 + pathToUse3,
                    noCache,
                    stateParamsBody,
                    $httpParamSerializer));
        };

    export const corsProductLinkFn = ($filter: ng.IFilterService) =>
        (seoUrl: string, whichUrl: string = "primary", noCache: boolean = false, stateParamsBody: { [key: string]: any } = null): string =>
            $filter("corsLink")(
                (seoUrl.startsWith("/") ? "" : "/") + seoUrl,
                "productDetail",
                whichUrl,
                noCache,
                stateParamsBody);

    export const corsStoreLinkFn = ($filter: ng.IFilterService) =>
        (seoUrl: string, whichUrl: string = "primary", noCache: boolean = false, stateParamsBody: { [key: string]: any } = null): string =>
            $filter("corsLink")(
                (seoUrl.startsWith("/") ? "" : "/") + seoUrl,
                "storeDetail",
                whichUrl,
                noCache,
                stateParamsBody);

    export const corsImageLinkFn = ($filter: ng.IFilterService, cefConfig: core.CefConfig) =>
        (
            fileName: string,
            kind: string = "products",
            imageResizerParamsBody: { [key: string]: any } = null,
            whichUrl: string = "primary"
        ): string => {
            if (!fileName) {
                return $filter("corsLink")(
                    cefConfig.images[kind] + cefConfig.images.suffix
                        + "/placeholder.jpg",
                    "images",
                    undefined,
                    undefined,
                    imageResizerParamsBody);
            }
            return $filter("corsLink")(
                cefConfig.images[kind] + cefConfig.images.suffix
                    + (fileName === "/" ? "" : ((fileName.startsWith("/") ? "" : "/") + fileName)),
                "images",
                whichUrl,
                false,
                imageResizerParamsBody);
        }

    export const corsStoredFilesLinkFn = ($filter: ng.IFilterService, cefConfig: core.CefConfig) =>
        (fileName: string, kind: string = "products", whichUrl: string = "primary"): string =>
            $filter("corsLink")(
                cefConfig.storedFiles[kind] + cefConfig.storedFiles.suffix
                    + ((fileName.startsWith("/") ? "" : "/") + fileName),
                "storedFiles",
                whichUrl,
                false,
                null);

    export const corsImportsLinkFn = ($filter: ng.IFilterService, cefConfig: core.CefConfig) =>
        (fileName: string, kind: string = "products", whichUrl: string = "primary"): string =>
            $filter("corsLink")(
                cefConfig.imports[kind] + cefConfig.imports.suffix
                    + ((fileName.startsWith("/") ? "" : "/") + fileName),
                "imports",
                whichUrl,
                false,
                null);

    export const goToCORSLinkFn = ($window: ng.IWindowService, $filter: ng.IFilterService) =>
        (
            path: string,
            area: string = "site",
            whichUrl: string = "primary",
            noCache: boolean = false,
            stateParamsBody: { [key: string]: any } = null,
            state: angular.ui.IState = null
        ): void => {
            $window.location.href = $filter("corsLink")(
                path,
                area,
                whichUrl,
                noCache,
                stateParamsBody,
                state);
        };
}
