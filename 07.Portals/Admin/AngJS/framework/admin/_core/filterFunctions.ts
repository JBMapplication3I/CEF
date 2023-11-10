module cef.admin.core {
    export const globalizedCurrencyFilterFn = ($filter: ng.IFilterService, cvCurrencyService: services.ICurrencyService) => {
        var cached: { [currencyA: string]: { [currencyB: string]: { [startinValue: number]: number } } } = { };
        function format(
                converted: number,
                unicode: number,
                places: number,
                currencyKey: string,
                currencyHtml: string,
                currencyRaw: string
            ): string {
            return $filter("currency")(
                    Number(converted),
                    angular.isDefined(currencyRaw)
                        ? currencyRaw
                        : angular.isDefined(currencyHtml)
                            ? currencyHtml
                            : String.fromCharCode(unicode
                                ? Number("0x" + unicode)
                                : 0x24/*$*/),
                        places || 2)
                + (currencyKey == "CAD" ? " CAD" : "")
        }
        function filter(input: string | number, alreadyCurrency: string = null): string {
            if (angular.isUndefined(input)) {
                return input as string;
            }
            const alreadyCurrencyKey = alreadyCurrency || "USD";
            if (!cached[alreadyCurrencyKey]) {
                cached[alreadyCurrencyKey] = { };
            }
            const currentUserCurrency = cvCurrencyService.getCurrentCurrencyCached();
            if (!currentUserCurrency) {
                // Must wait for the service to load
                return input as string;
            }
            const currentUserCurrencyKey = currentUserCurrency.CustomKey;
            if (alreadyCurrencyKey === currentUserCurrencyKey) {
                // No need to convert, just return original formatted
                return format(
                    Number(input),
                    currentUserCurrency.UnicodeSymbolValue,
                    currentUserCurrency.DecimalPlaceAccuracy,
                    currentUserCurrency.ISO4217Alpha,
                    currentUserCurrency.HtmlCharacterCode,
                    currentUserCurrency.RawCharacter);
            }
            if (!cached[alreadyCurrencyKey][currentUserCurrencyKey]) {
                cached[alreadyCurrencyKey][currentUserCurrencyKey] = { };
            }
            if (input in cached[alreadyCurrencyKey][currentUserCurrencyKey]) {
                // Avoid returning a promise
                return typeof cached[alreadyCurrencyKey][currentUserCurrencyKey][input] === "string"
                    ? cached[alreadyCurrencyKey][currentUserCurrencyKey][input]
                    : "Loading...";
            }
            // Set the value into it so it knows a promise has already been generated
            cached[alreadyCurrencyKey][currentUserCurrencyKey][input]
                = cvCurrencyService.convertCurrencyAToB(input, alreadyCurrencyKey).then(converted => {
                    cached[alreadyCurrencyKey][currentUserCurrencyKey][input] = format(
                        converted,
                        currentUserCurrency.UnicodeSymbolValue,
                        currentUserCurrency.DecimalPlaceAccuracy,
                        currentUserCurrency.ISO4217Alpha,
                        currentUserCurrency.HtmlCharacterCode,
                        currentUserCurrency.RawCharacter);
                });
            return undefined;
        }
        (filter as any).$stateful = true;
        return filter;
    };

    export const decamelizeFilterFn = () => (str: string, sep: string) => {
        if (typeof str !== "string") {
            throw new TypeError("Expected a string");
        }
        sep = typeof sep === "undefined" ? "_" : sep;
        return str
            .replace(new RegExp("([a-z\d])([A-Z])"), `$1${sep}$2`)
            .replace(new RegExp("([A-Z]+)([A-Z][a-z\d]+)"), `$1${sep}$2`)
            .toLowerCase();
    };

    export const camelCaseToHumanFilterFn = () => (input: string, uppercaseFirst?: boolean) => {
        if (typeof input !== "string") {
            return input;
        }
        // Split the text
        let result = input
            .replace(new RegExp("([a-z\d])([A-Z])"), `$1 $2`)
            .replace(new RegExp("([A-Z]+)([A-Z][a-z\d]+)"), `$1 $2`);
        //
        if (uppercaseFirst) {
            result = result.charAt(0).toUpperCase() + result.slice(1);
        }
        // Exceptions
        result = result
            .replace(new RegExp("\bPay Pal\b"), "PayPal")
            .replace(new RegExp("\Upc\b"), "UPC");
        return result;
    };

    export const checkIfNotEmptyFilterFn = hideThoseDashesAndOtherThingsFilter => input => {
        if (typeof input !== "string") {
            return false;
        }
        const attributeValue = hideThoseDashesAndOtherThingsFilter(input);
        if (typeof attributeValue === "string" && attributeValue.length > 0) {
            return true;
        }
        return false;
    };

    export const dec2hexFilterFn = () => textString => (textString + 0).toString(16).toUpperCase();

    /**
     * Converts a single hex number to a character. Note that no checking is
     * performed to ensure that this is just a hex number, eg. no spaces, etc.
     * @param {string} hex The hex codepoint to be converted
     * @returns {string} The character result
     */
    export const hex2charFilterFn = $filter => hex => {
        let n = parseInt(hex, 16);
        if (n <= 0xFFFF) {
            return String.fromCharCode(n);
        }
        if (n <= 0x10FFFF) {
            n -= 0x10000;
            return String.fromCharCode(0xD800 | (n >> 10))
                 + String.fromCharCode(0xDC00 | (n & 0x3FF));
        }
        return $filter("dec2hex")(n);
    };

    export const moduloFilterFn = () => (array: Array<any>, divisor: number) =>
        array.filter((item, index) => index % divisor === 0);

    export const maxFilterFn = () => (array: Array<number>) => Math.max.apply(null, array);

    export const minFilterFn = () => (array: Array<number>) => Math.min.apply(null, array);

    export const boolNormFilterFn = () => {
        let tSyn = [true, "true", "yes", "y", "1", 1];
        let fSyn = [false, "false", "no", "n", "0", 0];
        function boolMatch(inVal, matchArr) {
            inVal = (inVal.valueOf) ? inVal.valueOf() : inVal;
            if (angular.isString(inVal)) {
                inVal = inVal.trim().toLowerCase();
            }
            return matchArr.reduce(function (acc, curr) {
                if (inVal === curr) { acc = true; }
                return acc;
            }, false);
        }
        function normalize(inVal, outObj) {
            if (boolMatch(inVal, tSyn)) { return outObj["t"]; }
            if (boolMatch(inVal, fSyn)) { return outObj["f"]; }
            return inVal;
        }
        return function (input, txtObj) { // {t:string, f:string}
            let useTxt = angular.copy({ t: "Yes", f: "No" });
            if (angular.isObject(txtObj)) {
                useTxt = (Object as any).assign(useTxt, txtObj);
            }
            return (input && normalize(input, useTxt)) || "";
        }
    };

    /** Returns every nth element of the array */
    export const nthElementFilterFn = () => (array: Array<any>, divisor: number) => {
        const result = [];
        let i = 0;
        while (i < array.length) {
            result.push(array[i]);
            i += divisor;
        };
        return result;
    };

    export const numberToTimeFilterFn = ($filter: ng.IFilterService) => (num: number | string) => {
        if (!num && num != "0") {
            return "";
        }
        const groups = num.toString().trim().match(new RegExp("(\d+)(\.\d+)?"));
        if (!groups) {
            return num;
        }
        let ampm = "AM";
        let hour = Number(groups[1]);   // Use the Hour Value
        if (hour > 12) { hour -= 12; ampm = "PM"; }
        let minutesAsHundredths = groups[2] ? Number(groups[2].replace(".", "")) : 0;
        minutesAsHundredths = (minutesAsHundredths.toString().length === 1) ? minutesAsHundredths * 10 : minutesAsHundredths;    // Account for no zero in decimal value after first digit
        return `${hour}:${$filter("zeroPadNumber")((minutesAsHundredths / 100) * 60, 2)} ${ampm}`;
    };

    export const objectKeysLimitToFilterFn = () => (obj, limit) => {
        if (!obj) { return obj; }
        const keys = Object.keys(obj);
        if (keys.length < 1) {
            return [];
        }
        const ret = new Object();
        let count = 0;
        angular.forEach(keys, key => {
            if (count >= limit) {
                return false;
            }
            ret[key] = obj[key];
            count++;
            return true;
        });
        return ret;
    };

    export const objectKeysFilterFilterFn = ($filter: ng.IFilterService) => (obj, filter) => {
        if (!obj) { return obj; }
        const keys = Object.keys(obj);
        if (keys.length < 1) {
            return [];
        }
        const arr = [];
        angular.forEach(keys, key => {
            arr.push({ key: key, value: obj[key] });
        });
        return $filter("filter")(arr, filter);
    };

    export const toArrayFilterFn = () => (obj, addKey) => {
        if (!angular.isObject(obj)) {
            return obj;
        }
        if (addKey === false) {
            return Object.keys(obj).map(key => obj[key]);
        }
        return Object.keys(obj).map(key => {
            return angular.isObject(obj[key])
                ? Object.defineProperty(obj[key], "$key", { enumerable: false, value: key })
                : { $key: key, $value: obj[key] };
        });
    };

    export const telFilterFn = () => tel => {
        if (!tel) { return ""; }
        const value = tel.toString().trim().replace(/^\+/, "");
        if (value.match(/[^0-9]/)) {
            return tel;
        }
        let country: number | string, city: string, number: string;
        switch (value.length) {
            case 10: { // +1PPP####### -> C (PPP) ###-####
                country = 1;
                city = value.slice(0, 3);
                number = value.slice(3);
                break;
            }
            case 11: { // +CPPP####### -> CCC (PP) ###-####
                country = value[0];
                city = value.slice(1, 4);
                number = value.slice(4);
                break;
            }
            case 12: { // +CCCPP####### -> CCC (PP) ###-####
                country = value.slice(0, 3);
                city = value.slice(3, 5);
                number = value.slice(5);
                break;
            }
            default: {
                return tel;
            }
        }
        if (country === 1) {
            country = "";
        }
        number = number.slice(0, 3) + "-" + number.slice(3);
        return (country + " (" + city + ") " + number).trim();
    };

    export const trustedHtmlFilterFn = ($sce: ng.ISCEService) => (text) => {
        if (!text) { return text; }
        let retVal = text["$$unwrapTrustedValue"]
            ? text["$$unwrapTrustedValue"]()
            : $sce.trustAsHtml(text);
        if (retVal && angular.isFunction(retVal.replace)) {
            retVal = retVal.replace("\r\n", "");
        }
        return retVal;
    };

    export const zeroPadNumberFilterFn = () => (num: number, len: number): string => {
        if (isNaN(num) || isNaN(len)) {
            return `${num}`;
        }
        if (`${num}`.indexOf(".") > -1) {
            const parts = `${num}`.split(".");
            let outDec = parts[0]; // Read the before decimal
            while (outDec.length < len) {
                outDec = `0${outDec}`;
            }
            return outDec + parts[1]; // Reappend the after decimal
        } else {
            let outInt = `${num}`;
            while (outInt.length < len) {
                outInt = `0${outInt}`;
            }
            return outInt;
        }
    };

    export const convertJsonDateFilterFn = () => (input: string): Date =>
        input && input.length > 0 && input.startsWith("/Date(")
            ? new Date(parseInt(input.substr(6)))
            : input as any;

    export const limitToEllipsesFilterFn = () => (input: string, length: number): string => {
        if (!input) { return input; }
        let valToUse = input;
        if (input["$$unwrapTrustedValue"]) {
            // trustedHtml
            valToUse = valToUse["$$unwrapTrustedValue"]();
        }
        return !valToUse || valToUse.length < length
            ? valToUse
            : valToUse.substr(0, length) + "...";
    }

    export const convertToNumberDirectiveFn = () => ({
        require: "ngModel",
        link: function(scope, element, attrs, ngModel) {
            ngModel.$parsers.push((val) => val != null ? parseInt(val, 10) : null);
            ngModel.$formatters.push((val) => val != null ? "" + val : null);
        }
    });

    export const modifiedValueFilterFn = () =>
        (startingValue: number, modifier: number, mode: number): number => {
            if (!mode) { mode = 0; }
            if (!modifier) { modifier = 0; }
            // 0 = No change
            // 1 = Override Price
            // 2 = +/- Amount
            // 3 = +/- Percent from -100% to positive 100000%
            switch (mode) {
                case 1: { return modifier; }
                case 2: { return startingValue + modifier; }
                case 3: { return startingValue * ((modifier + 100) / 100); }
            }
            return startingValue;
        }

    export const groupByFilterFn = () => {
        const cache: { [key: string]: any } = { };
        return (source: any[], by: string, cacheName: string): _.Dictionary<any[]> => {
            if (!source || !by || !cacheName) {
                return source as any;
            }
            if (cache[cacheName]) {
                return cache[cacheName];
            }
            return cache[cacheName] = _.groupBy(source, by);
        }
    }

    export const flatGroupByFilterFn = ($filter: ng.IFilterService) => {
        // const cache: { [key: string]: any } = { };
        return (source: any[], by: string[], cacheName: string): { key: string, value: (() => any)[] }[] => {
            if (!source || !by || !cacheName || !angular.isArray(by)) {
                return source as any;
            }
            // if (cache[cacheName]) {
            //    return cache[cacheName];
            // }
            function getDeepValue(object: object, propString: string): any {
                if (object.hasOwnProperty(propString)) {
                    return object[propString];
                }
                let nextProperty = "";
                let skipped = 0;
                for (let c = 0; c < propString.length; c++) {
                    const character = propString[c];
                    if (nextProperty !== "" && (character === "." || character === "[" || character === "]")) {
                        break;
                    }
                    if (nextProperty === "" && (character === "." || character === "[" || character === "]")) {
                        skipped++;
                        continue;
                    }
                    nextProperty = nextProperty + character;
                }
                if (!object.hasOwnProperty(nextProperty)) {
                    return undefined;
                }
                if (propString.substr(nextProperty.length + skipped) === ""
                    || propString.substr(nextProperty.length + skipped) === "]") {
                    return object[nextProperty];
                }
                return getDeepValue(object[nextProperty], propString.substr(nextProperty.length + skipped));
            }
            const temp: any = { };
            for (let i = 0; i < source.length; i++) {
                const keyParts = [];
                for (let j = 0; j < by.length; j++) {
                    const value = getDeepValue(source[i], by[j]);
                    keyParts.push(value);
                }
                let fullKey = $filter("splitShippingGroupTitle")(_.join(keyParts, "|"));
                if (!temp[fullKey]) { temp[fullKey] = []; }
                temp[fullKey].push(() => source[i]);
            }
            const sortedKeys = _.sortBy(Object.keys(temp), x => x);
            const temp2 = [];
            for (let i = 0; i < sortedKeys.length; i++) {
                temp2.push({ key: sortedKeys[i], value: temp[sortedKeys[i]] });
            }
            return /*cache[cacheName] =*/ temp2;
        };
    };

    export const splitShippingGroupTitleFilterFn = () => {
        const cache: { [key: string]: any } = { };
        return (source: string) => {
            if (!source) {
                return source as any;
            }
            if (cache[source]) {
                return cache[source];
            }
            let temp = "";
            const parts = source.split("|");
            // parts[0] // NothingToShip true/false
            // parts[1] // Store Name
            // parts[2] // Vendor Name
            if (parts[0] === "true") {
                temp += "Non-shippable products";
            } else {
                temp += "Shippable products";
            }
            if (parts[1]) {
                temp += " sold by " + parts[1];
            }
            if (parts[2]) {
                if (parts[1]) {
                    temp += " and";
                }
                temp += " supplied by " + parts[2];
            }
            return cache[source] = temp;
        };
    };

    export const sumByFilterFn = () =>
        (source: any[], by: string): number =>
            _.sumBy(source, by);

    export const statusIDToTextFilterFn = ($translate: ng.translate.ITranslateService, cvStatusesService: services.IStatusesService) =>
        (statusID: number, kind: string, textOrder: string[]) => {
            if (angular.isUndefined(statusID) || statusID === null) {
                return "Error: statusID is required";
            }
            if (angular.isUndefined(kind) || kind === null) {
                return "Error: kind is required";
            }
            if (angular.isUndefined(textOrder)
                || textOrder === null
                || !angular.isArray(textOrder)
                || !textOrder.length) {
                return "Error: textOrder is required";
            }
            // Load the data if not already loaded
            cvStatusesService.get({ kind: kind, id: statusID });
            // Can only return cached (non-promise) data, will return null until the data is loaded
            const status = cvStatusesService.getCached({ kind: kind, id: statusID });
            if (!status) { return null; }
            for (let i = 0; i < textOrder.length; i++) {
                if (textOrder[i] === "Translated" && !status["Translated"] && status.TranslationKey) {
                    const translated = $translate.instant(status.TranslationKey);
                    if (translated) {
                        status["Translated"] = translated;
                        return translated;
                    }
                }
                if (status[textOrder[i]]) {
                    return status[textOrder[i]];
                }
            }
            return status.Name;
        };
    export const typeIDToTextFilterFn = ($translate: ng.translate.ITranslateService, cvTypesService: services.ITypesService) =>
        (typeID: number, kind: string, textOrder: string[]) => {
            if (angular.isUndefined(typeID) || typeID === null) {
                return "Error: typeID is required";
            }
            if (angular.isUndefined(kind) || kind === null) {
                return "Error: kind is required";
            }
            if (angular.isUndefined(textOrder)
                || textOrder === null
                || !angular.isArray(textOrder)
                || !textOrder.length) {
                return "Error: textOrder is required";
            }
            // Load the data if not already loaded
            cvTypesService.get({ kind: kind, id: typeID });
            // Can only return cached (non-promise) data, will return null until the data is loaded
            const type = cvTypesService.getCached({ kind: kind, id: typeID });
            if (!type) { return null; }
            for (let i = 0; i < textOrder.length; i++) {
                if (textOrder[i] === "Translated" && !type["Translated"] && type.TranslationKey) {
                    const translated = $translate.instant(type.TranslationKey);
                    if (translated) {
                        type["Translated"] = translated;
                        return translated;
                    }
                }
                if (type[textOrder[i]]) {
                    return type[textOrder[i]];
                }
            }
            return type.Name;
        };
    export const stateIDToTextFilterFn = ($translate: ng.translate.ITranslateService, cvStatesService: services.IStatesService) =>
        (stateID: number, kind: string, textOrder: string[]) => {
            if (angular.isUndefined(stateID) || stateID === null) {
                return "Error: stateID is required";
            }
            if (angular.isUndefined(kind) || kind === null) {
                return "Error: kind is required";
            }
            if (angular.isUndefined(textOrder)
                || textOrder === null
                || !angular.isArray(textOrder)
                || !textOrder.length) {
                return "Error: textOrder is required";
            }
            // Load the data if not already loaded
            cvStatesService.get({ kind: kind, id: stateID });
            // Can only return cached (non-promise) data, will return null until the data is loaded
            const state = cvStatesService.getCached({ kind: kind, id: stateID });
            if (!state) { return null; }
            for (let i = 0; i < textOrder.length; i++) {
                if (textOrder[i] === "Translated" && !state["Translated"] && state.TranslationKey) {
                    const translated = $translate.instant(state.TranslationKey);
                    if (translated) {
                        state["Translated"] = translated;
                        return translated;
                    }
                }
                if (state[textOrder[i]]) {
                    return state[textOrder[i]];
                }
            }
            return state.Name;
        };

    export const attributeOrderFilterFn = () => (attributesArray: any[]) => {
        return attributesArray
            .map(attribute => {
                const priority = ['system of measurement', 'material', 'grade', 'finish'];
                Object.defineProperty(attribute, "priority", {
                    enumerable: false,
                    writable: true
                });
                if (_.includes(priority, attribute.$key.toLowerCase())) {
                    attribute.priority = priority.indexOf(attribute.$key.toLowerCase()) - priority.length;
                }
                else attribute.priority = 0;
                return attribute;
            })
            .sort((a, b) => a.priority - b.priority);
    };

    export const uniqueArrayValueFn = () => (arr, filterKey) => {
        return _.uniqBy(arr, x => x[filterKey]);
    }

    export const removeUrlHashFn =() => (url : string) => {
        return _.replace(url, '#!?', '?')
    }
}
