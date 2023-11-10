/**
 * @file framework/store/product/controls/attributes/attributes.ts
 * @desc Attributes class
 */
module cef.store.product.controls.attributes {
    export class ProductDetailsAttributesController extends core.TemplatedControllerBase {
        // Properties
        attributes: string // Bound by Scope
        filterList: any; // Bound by Scope
        specifications;
        specificationsMobile;
        // Functions
        load(): void {
            const filterList = [];
            const atr = JSON.parse(this.attributes);
            this.loadFeatureColumns(atr, 2, filterList, false);
            this.loadFeatureColumns(atr, 1, filterList, true);
            ////this.loadFeatureColumns(this.attributes, 2, this.filterList, false);
            ////this.loadFeatureColumns(this.attributes, 1, this.filterList, true);
        }
        loadFeatureColumns(
            serializableAttributes: api.SerializableAttributesDictionary,
            columns: number,
            notAllowedKeys: Array<string>,
            mobile: boolean)
            : void {
            // Filter the attributes
            const features = new Array<api.SerializableAttributeObject>();
            angular.forEach(serializableAttributes, (value: api.SerializableAttributeObject, key: string) => {
                if (key.indexOf("_UOM") > -1) { return; }
                if (notAllowedKeys.some(y => y === key)) { return; }
                if (!value.Value || value.Value === "") { return; }
                features.push(value);
            });
            // Chunk the attributes into columns
            const rows = Math.ceil(features.length / columns);
            const chunkedFeatures = [];
            for (let r = 0; r < rows; ++r) {
                const cols: any[] = [];
                for (let c = 0; c < columns; ++c) {
                    cols[c] = [];
                }
                chunkedFeatures[r] = cols;
            }
            let featureIndex = 0;
            let rowIndex = 0;
            features.forEach((value: api.SerializableAttributeObject) => {
                chunkedFeatures[rowIndex][featureIndex % columns] = value;
                rowIndex = ((featureIndex % columns) === (columns - 1)) ? (rowIndex + 1) : rowIndex;
                featureIndex = featureIndex + 1;
            });
            // Return the attributes in chunked array
            if (mobile) {
                this.specificationsMobile = chunkedFeatures;
                return;
            }
            this.specifications = chunkedFeatures;
        }
        // Constructor
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefProductDetailsAttributes", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { attributes: "@", filterList: "@" },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/attributes/attributes.html", "ui"),
        controller: ProductDetailsAttributesController,
        controllerAs: "productDetailsAttributesCtrl",
        bindToController: true
    }));
}
