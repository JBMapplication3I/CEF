module cef.store.searchCatalog.controls {
    class SearchCatalogFilterByAttributesController extends core.TemplatedControllerBase {
        // Convenience Points (reduces HTML size)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Bound Scope Properties
        mode: string;
        limitAttrsTo: number;
        limitOptionsTo: number;
        sortAttributes: string[];
        // Properties
        showOptions = new cefalt.store.Dictionary<boolean>();
        isOpen: boolean;
        get searchIsRunning(): boolean {
            return this.service.searchIsRunning;
        }
        get attributes() {
            return this.service.activeSearchViewModel
                && this.service.activeSearchViewModel.Attributes;
        }
        // Functions
        attributeDisplayName(key: string): string {
            let attribute = _.find(this.service.allAttributes, attrs => attrs.CustomKey === key);
            if (!attribute) {
                return key;
            }
            return attribute.DisplayName || attribute.Name || attribute.CustomKey;
        }
        expandAll(): void {
            Object.keys(this.attributes)
                .forEach(x => this.showOptions[this.attributes[x].Key] = true);
        }
        collapseAll(): void {
            Object.keys(this.attributes)
                .forEach(x => this.showOptions[this.attributes[x].Key] = false);
        }
        toggleShowOptions(key: string): void {
            this.showOptions[key] = !this.showOptions[key];
        }
        pushAttributeOptionKey(key: string, option: string): void {
            if (this.searchIsRunning) {
                return;
            }
            this.service.pushAttributeOptionKey(this.mode, key, option);
        }
        popAttributeOptionKey(key: string, option: string): void {
            this.service.popAttributeOptionKey(this.mode, key, option);
        }
        optionIsActive(key: string, option: string): boolean {
            // this.service.activeSearchViewModel.Form.PricingRanges.indexOf(range.Label) > -1
            return false;
        }
        plusOrMinus(key: string): string {
            return this.showOptions[key] ? "fa-minus" : "fa-plus";
        }
        clearTicketsRange(): void {
            this.service.clearAttributeOption(this.mode, "TicketValue");
        }
        private _attributesArray;
        get attributesArray() {
            // This is a getter because it needs to be defined exactly once. AngularJS does not like iterating
            // over an object returned by a function, causes $digest issues.
            if (this._attributesArray !== undefined) {
                return this._attributesArray;
            }
            this._attributesArray = {};
            let attrs = Object.keys(this.attributes);
            for (let key of attrs) {
                if (!this.attributes[key]) {
                    continue;
                }
                if (_.includes(this.sortAttributes, key)) {
                    this._attributesArray[key] = this.objectToKeyValuePair(this.attributes[key],
                        this.sortImperialMetric);
                } else {
                    this._attributesArray[key] = this.objectToKeyValuePair(this.attributes[key]);
                }
            }
            return this._attributesArray;
        }
        isAttributeIncluded(key: string, option: string): boolean {
            if (this.service
                && this.service.activeSearchViewModel
                && this.service.activeSearchViewModel.Form
                && this.service.activeSearchViewModel.Form.AttributesAll) {
                return _.includes(this.service.activeSearchViewModel.Form.AttributesAll[key], option);
            }
            return false;
        }
        checkAttribute(key:string, option: string): void {
            if (this.isAttributeIncluded(key, option)) {
                this.popAttributeOptionKey(key, option);
                return;
            }
            if (this.service.activeSearchViewModel.Form.AttributesAll
                && this.service.activeSearchViewModel.Form.AttributesAll[key]
                && this.service.activeSearchViewModel.Form.AttributesAll[key].length) {
                return;
            }
            this.pushAttributeOptionKey(key, option);
        }
        objectToKeyValuePair(attrObj, sorter = (pair, pair2) => 0): object[] {
            return Object.keys(attrObj)
                .map(key => ({key,value: attrObj[key]}))
                .filter(pair => !/\$\$/.test(pair.key)) // remove $$hashkey
                .sort(sorter);
        }
        loadShowOptions = (): void => {
            if (!this.service.activeSearchViewModel.Form.AttributesAll) {
                return;
            }
            Object.keys(this.service.activeSearchViewModel.Form.AttributesAll)
                .forEach(attributeKey => {
                    this.showOptions[attributeKey] = true;
                });
            this.isOpen = true;
        }
        sortImperialMetric = (pair, pair2): number => {
            const metric1 = /mm/.test(pair.key),
                imperial1 = /"|\//.test(pair.key),
                metric2 = /mm/.test(pair2.key),
                imperial2 = /"|\//.test(pair2.key);
            const key1 = pair.key
                .replace(/\-\d+$/, '') // Remove thread size measurement
                .replace('-', '+') // Change - to + in cases of imperial
                .replace(/"|mm/g, ''); // remove all quotes and mm
            const key2 = pair2.key
                .replace(/\-\d+$/, '')
                .replace('-', '+')
                .replace(/"|mm/g, '');
            let retNum = 0;
            if (imperial1) {
                retNum = -1
            }
            if (metric1) {
                retNum = 1;
            }
            if (imperial1 && imperial2) {
                retNum = eval(key1) < eval(key2) ? -1 : 1;
            }
            if (metric1 && metric2) {
                retNum = parseInt(key1) > parseInt(key2) ? 1 : -1;
            }
            return retNum;
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogFilterByAttributes", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            mode: "@", // "multi-any", "multi-all" (default: "multi-all")
            limitAttrsTo: "@?",
            limitOptionsTo: "@?",
            sortAttributes: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filterByAttributes.html", "ui"),
        controller: SearchCatalogFilterByAttributesController,
        controllerAs: "scfbaCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));

    cefApp.filter("attributeAllowedFilterValues", (cefConfig: core.CefConfig) =>
        (attributesList: Array<{ Key: string; Value: Array<{ Key: string, Value: { Key: string, Value: string} }> }>): any => {
            /*
            if (!cefConfig.catalog.attributeAllowedFilterValues) {
                return attributesList;
            }
            if (angular.isUndefined(attributesList)) {
                return attributesList;
            }
            let attrFilterObject: Array<{ Key: string; Value: Array<{ Key: string, Value: { Key: string, Value: string} }> }>;
            try {
                attrFilterObject = JSON.parse(cefConfig.catalog.attributeAllowedFilterValues.replace(/'/g, "\""));
            } catch (e) {
                console.error(e);
                return attributesList;
            }
            attributesList = attributesList.map(attribute => {
                let foundFilter;
                if (foundFilter = _.find(attrFilterObject, afo => afo.Key === attribute.Key)) {
                    attribute.Value = attribute.Value.filter(av => _.find(foundFilter.Value, ffv => ffv["Key"] === av.Key));
                    return attribute;
                }
                return attribute;
            });
            */
            return attributesList;
        }
    );

    cefApp.filter("attributeNumericalSort", () =>
        (attributesList) => {
            return attributesList.sort((a, b) => {
                // try evaling them as numbers, if fails, return 0;
                try {
                    // the format of these keys will be two numbers, nnn-nnn, order by the first number only
                    let eval1 = parseInt(a.Key.replace(/(\d+)(.*)/, '$1'));
                    let eval2 = parseInt(b.Key.replace(/(\d+)(.*)/, '$1'));
                    if (eval1 < eval2) {
                        return -1;
                    }
                    if (eval1 > eval2) {
                        return 1;
                    }
                    return 0;
                } catch (err) {
                    if (a.Key < b.Key) {
                        return -1;
                    }
                    if (a.Key > b.Key) {
                        return 1;
                    }
                    return 0;
                }
            });
        }
    );
}
