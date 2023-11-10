module cef.store.product.controls.variants {
    class ProductVariantsTableController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        productVariants: api.ProductAssociationModel[];
        possibleAttrValues = {};
        /** @desc The master controller */
        masterController: any;
        // Properties
        fullProductVariants: Function = (searchParams?: any): api.ProductModel[] => {
            if (angular.isUndefined(searchParams)) {
                return this._fullProductVariants;
            }
            if (searchParams && searchParams["JsonAttributes"]) {
                let searchAttrs = searchParams["JsonAttributes"];
                let searchAttrKeys = Object.keys(searchAttrs);
                // Uses the searchParams from cvGrid to return a filtered list of this._fullProductVariants, only
                // the ones that include all JsonAttributes with matching values
                return this._fullProductVariants.map(variant => {
                    let varAttrKeys = Object.keys(variant.SerializableAttributes || {})
                        .filter(x => _.includes(searchAttrKeys, x));
                    for (let i = 0; i < varAttrKeys.length; i++) {
                        const varAttrKey = varAttrKeys[i];
                        const varAttr = variant.SerializableAttributes[varAttrKey];
                        const searchAttrKey = searchAttrKeys[searchAttrKeys.indexOf(varAttrKey)];
                        const searchAttr = searchAttrs[searchAttrKey]["0"]; // value always returned in a 1 length array
                        if (!this._fullproductVariantsInner(varAttr, searchAttr)) {
                            return undefined;
                        }
                    }
                    return variant;
                }).filter(Boolean); // Filters out all of the 'undefined's
            }
            return this._fullProductVariants;
        }
        _fullProductVariants: api.ProductModel[];
        unbindVariantLoaded: Function;
        kDataAttributeColumns: kendo.ui.GridColumn[];
        buildingGrid: boolean = true;
        attributes: api.GeneralAttributeModel[];
        // Functions
        private _fullproductVariantsInner: Function = (varAttr: api.SerializableAttributeObject, searchAttr: string): boolean => {
            if (_.isNull(searchAttr)) {
                return true;
            }
            if (varAttr
                && varAttr.Value
                && varAttr.Value.toString().toLowerCase() === searchAttr.toLowerCase()) {
                return true;
            }
            return false;
        }
        resolveAttributes(): ng.IPromise<ng.IHttpPromiseCallbackArg<api.GeneralAttributePagedResults>> {
            return this.$q.when(
                this.cvApi.attributes.GetGeneralAttributes({
                    Active: true,
                    AsListing: true,
                    HideFromProductDetailView: false
                }),
            )
        }
        buildKGridAttributeColumns() {
            // Get object containing all serializable attributes from list of variants ( values don't matter );
            let kDataAttrs = this._fullProductVariants.reduce((kData, variant) => {
                return {...kData, ...(variant.SerializableAttributes || {})};
            }, { });
            // Build Attribute Columns;
            return this.kDataAttributeColumns = Object.keys(kDataAttrs).reduce((kDataColumn, attrKey) => {
                return [{
                    field: `SerializableAttributes["${attrKey}"].Value`,
                    title: attrKey
                }, ...kDataColumn ];
            }, []).filter(kDataColumn => _.find(this.attributes,
                attr => attr.CustomKey === kDataColumn.title
                     && attr.Active
                     && !attr.HideFromProductDetailView));
        }
        getPossibleAttrValues(attrKey: string) {
            // Needs to iterate over this.productVariants;
            let possibleAttrValues = [];
            for (let i = 0; i < this.productVariants.length; i++) {
                const variant = this.productVariants[i];
                if (!variant.SlaveSerializableAttributes[attrKey]) {
                    continue;
                }
                possibleAttrValues.push(variant.SlaveSerializableAttributes[attrKey].Value);
            }
            possibleAttrValues = _.uniq(possibleAttrValues);
            return possibleAttrValues;
        }
        getFirstSinglePack(dataItem: api.ProductModel) {
            return _.find(dataItem.ProductAssociations,
                x => x.CustomKey.toLowerCase().endsWith('-single')) || dataItem;
        }
        // Events
        // <None>
        // Constructor
        load(): void {
            if (!this.productVariants || !this.productVariants.length) {
                return;
            }
            this._fullProductVariants = this.productVariants.map(variant => variant.Slave);
            this.resolveAttributes().then(r => {
                this.attributes = r.data.Results;
                this.buildKGridAttributeColumns();
                for (let i = 0; i < this.kDataAttributeColumns.length; i++) {
                    const col = this.kDataAttributeColumns[i];
                    this.possibleAttrValues[`${col.title}`] = this.getPossibleAttrValues(col.title);
                }
                this.buildingGrid = false;
            });
        }
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.unbindVariantLoaded = $scope.$on(this.cvServiceStrings.events.products.variantInfoLoaded,
                () => this.load());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindVariantLoaded)) { this.unbindVariantLoaded(); }
            });
        }
    }

    cefApp.directive("cefProductVariantsTable", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            masterController: "=?",
            productVariants: "=?",
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/variants/variantTable.html", "ui"),
        controller: ProductVariantsTableController,
        controllerAs: "productVariantsTableCtrl",
        bindToController: true
    }));
}
