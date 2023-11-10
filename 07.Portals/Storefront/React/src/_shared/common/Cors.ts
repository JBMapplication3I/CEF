import {
  CEFConfig,
  IUrlConfig,
  IUrlHostConfig
} from "../../_redux/_reduxTypes";

export const corsLinkRootInner = (urlConfig: IUrlConfig): string => {
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

export const corsSplitAreaAsNeeded = (
  cefConfig: CEFConfig,
  area: keyof typeof cefConfig.routes
) => {
  // TODO: passing route and urlConfig as a single string
  // if (area.indexOf(".") !== -1) {
  //   let route = area.split(".")[0],
  //       urlConfig = area.split(".")[1];
  //   return cefConfig.routes[route][urlConfig];
  // }
  return cefConfig.routes[area];
};

export const corsLinkRoot = (
  cefConfig: CEFConfig,
  area: keyof typeof cefConfig.routes = "site",
  whichUrl: string = "primary"
): string => {
  const areaToUse = corsSplitAreaAsNeeded(cefConfig, area);
  if (!areaToUse) {
    throw Error(`Area '${area}' does not exist on cefConfig.`);
  }
  if (!areaToUse.hostIsProvidedByLookup) {
    // We don't need to inject a host URL
    return corsLinkRootInner(areaToUse);
  }
  // TODO: ByBrand, SiteDomains
  // We have to inject a host URL to it
  // let siteDomainToUse: SiteDomainModel;
  // if ((areaToUse.hostIsProvidedByLookup as IUrlHostConfig).type === "ByBrand") {
  //   if (!$rootScope["globalBrandSiteDomain"]) {
  //     // We failed to get data, so just try going to it with a relative path
  //     return $filter("corsLinkRootInner")(areaToUse);
  //   } else {
  //     siteDomainToUse = $rootScope["globalBrandSiteDomain"];
  //   }
  // } else {
  //   // We failed to get data, so just try going to it with a relative path
  //   return corsLinkRootInner(areaToUse);
  // }
  // We have the site domain data
  // let host: string;
  // switch (whichUrl.toLowerCase()) {
  //   case "alternate-1": {
  //     host = siteDomainToUse.AlternateUrl1;
  //     break;
  //   } // The ALternate Site
  //   case "alternate-2": {
  //     host = siteDomainToUse.AlternateUrl2;
  //     break;
  //   } // The CORS site
  //   case "alternate-3": {
  //     host = siteDomainToUse.AlternateUrl3;
  //     break;
  //   } // Unused
  //   case "primary":
  //   default: {
  //     host = siteDomainToUse.Url;
  //     break;
  //   } // This Shop site
  // }
  // while (/\/$/.test(host)) {
  //   // Make sure we don't end up with 'domain.com//some-path'
  //   host = host.substring(0, host.length - 1);
  // }
  // // Go to the new absolute URL
  // const altAreaToUse = JSON.parse(JSON.stringify(areaToUse));
  // const hostNoProtocol = host.replace("https://", "").replace("http://", "");
  // altAreaToUse.host = host;
  // const result = corsLinkRootInner(altAreaToUse);
  // return result.replace(hostNoProtocol + "/" + hostNoProtocol, hostNoProtocol);
};

// Specialized marker to inject this path as the Return URL
const swapReturnUrlToken = (path: string): string => {
  if (!path) {
    return "";
  }
  let retVal = path;
  if (retVal.indexOf("[ReturnUrl]") !== -1) {
    retVal = retVal.replace(
      /\[ReturnUrl\]/,
      encodeURI(window.location.pathname)
    );
  }
  // Assumes path contained "[ReturnUrl]", and was encoded by the above code block
  if (retVal.indexOf("%5BReturnUrl%5D") !== -1) {
    retVal = retVal.replace(
      /%5BReturnUrl%5D/,
      encodeURI(window.location.pathname)
    );
  }
  // Strip '/' from the end
  return retVal.replace(/\/+$/g, "");
};

const appendQueryStringParams = (
  basePlusPath: string,
  noCache: boolean,
  paramsBody: { [key: string]: any }
): string => {
  if (!paramsBody || paramsBody === {}) {
    // there's no paramsBody, just return
    return noCache
      ? basePlusPath + "?noCache=" + new Date().getTime()
      : basePlusPath;
  }
  // Convert paramsBody into query parameters
  if (noCache) {
    // Merge in noCache if set
    paramsBody["noCache"] = new Date().getTime();
  }
  let queryString = new URLSearchParams();
  for (const key in paramsBody) {
    if (Object.prototype.hasOwnProperty.call(paramsBody, key)) {
      const property = paramsBody[key];
      queryString.append(key, property);
    }
  }
  // Strip '/' from the end then appendQueryString
  return swapReturnUrlToken(
    basePlusPath.replace(/\/+$/g, "") + "?" + queryString
  );
};

export const corsLink = (
  cefConfig: CEFConfig,
  path: string,
  area: keyof typeof cefConfig.routes = "site",
  whichUrl: string = "primary",
  noCache: boolean = false,
  paramsBody: { [key: string]: any } = null
): string => {
  // Specialized marker to inject this path as the Return URL
  if (!cefConfig) {
    return;
  }
  const swapReturnUrlToken = (path: string): string => {
    if (!path) {
      return "";
    }
    let retVal = path;
    if (retVal.indexOf("[ReturnUrl]") !== -1) {
      retVal = retVal.replace(
        /\[ReturnUrl\]/,
        encodeURI(window.location.pathname)
      );
    }
    // Assumes path contained "[ReturnUrl]", and was encoded by the above code block
    if (retVal.indexOf("%5BReturnUrl%5D") !== -1) {
      retVal = retVal.replace(
        /%5BReturnUrl%5D/,
        encodeURI(window.location.pathname)
      );
    }
    // Strip '/' from the end
    return retVal.replace(/\/+$/g, "");
  };

  path = swapReturnUrlToken(path);
  if (path && path.indexOf("http") === 0) {
    // Absolute path, don't do anything else, just go
    return noCache ? path + "?noCache=" + new Date().getTime() : path;
  }

  const areaToUse = corsSplitAreaAsNeeded(cefConfig, area);

  if (!areaToUse) {
    throw Error(`Area '${area}' does not exist on cefConfig.`);
  }

  // the new absolute URL
  const root = corsLinkRoot(
    cefConfig,
    area,
    (areaToUse.hostIsProvidedByLookup as IUrlHostConfig)?.whichUrl || whichUrl
  );
  const base3 = root;
  let pathToUse3 = (path && /^\//.test(path) ? "" : "/") + (path || "");
  if (
    areaToUse.root &&
    pathToUse3 &&
    new RegExp("^" + areaToUse.root).test(pathToUse3)
  ) {
    pathToUse3 = pathToUse3.substring(areaToUse.root.length);
  }
  return swapReturnUrlToken(
    appendQueryStringParams(base3 + pathToUse3, noCache, paramsBody)
  );
};

export const corsImageLink = (
  cefConfig: CEFConfig,
  fileName?: string,
  kind: keyof typeof cefConfig.images = "products",
  imageResizerParamsBody: { [key: string]: any } = null,
  whichUrl: string = "primary"
): string => {
  if (!cefConfig) {
    return "";
  }
  if (!fileName) {
    return corsLink(cefConfig, "/Content/placeholder.jpg", "ui");
  }
  return corsLink(
    cefConfig,
    cefConfig.images[kind] +
      cefConfig.images.suffix +
      (fileName === "/" ? "" : (/^\//.test(fileName) ? "" : "/") + fileName),
    "images",
    whichUrl,
    false,
    imageResizerParamsBody
  );
};

