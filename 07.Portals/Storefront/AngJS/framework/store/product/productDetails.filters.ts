/**
 * @file productDetails.filters.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc product details class
 */
module cef.store.product {
    cefApp.filter("urlEncode", (): Function => { return window.encodeURIComponent; });

    cefApp.filter("hideBaseProductAttributes", ($sce: ng.ISCEService) => {
        return (serializableAttributes: api.SerializableAttributesDictionary, notAllowedKeys: Array<string>) => {
            const results = new Array<api.SerializableAttributeObject>();
            angular.forEach(serializableAttributes, (value: api.SerializableAttributeObject, key: string) => {
                key = $sce.getTrustedHtml(key);
                if (key && key.indexOf("_UOM") > -1) { return; }
                if (notAllowedKeys.some && notAllowedKeys.some(y => y === key)) { return; }
                if (!value.Value || value.Value === "") { return; }
                results.push(value);
            });
            return results;
        }
    });

    cefApp.filter("showOnlyTheseProductAttributes", ($sce: ng.ISCEService) => {
        return (serializableAttributes: api.SerializableAttributesDictionary, allowedKeys: Array<string>) => {
            const results = new Array<api.SerializableAttributeObject>();
            angular.forEach(serializableAttributes, (value: api.SerializableAttributeObject, key: string) => {
                key = $sce.getTrustedHtml(key);
                if (key.indexOf("_UOM") > -1) { return; }
                if (!allowedKeys.some(y => y === key)) { return; }
                if (!value.Value || value.Value === "") { return; }
                results.push(value);
            });
            return results;
        }
    });
}
