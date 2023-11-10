module cef.store.product.controls.variants {
    class ProductVariantsWizardController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        /** @desc The master controller */
        masterController: any;
        // Properties
        listOfShvSize: { id: number, value: string }[];
        listOfShvBacker: { id: number, value: string }[];
        selectedShvSize: string;
        selectedShvBacker: string;
        // Functions

        private buildListFromSlaveSerializableAttributes(attributeName: string): { id: number, value: string }[] {
            return this.masterController.productVariants.reduce((listFromAttribute, variant) => {
                if (!variant || !variant.SlaveSerializableAttributes || !variant.SlaveSerializableAttributes[attributeName]) {
                    return listFromAttribute;
                }
                listFromAttribute.push({ id: variant.SlaveID, value: variant.SlaveSerializableAttributes[attributeName].Value });
                return listFromAttribute;
            }, []);
        }

        private load(): void {
            this.listOfShvSize = this.buildListFromSlaveSerializableAttributes("ShvSize").sort((a: { id: number, value: string }, b: { id: number, value: string }): number => {
                try {
                    return eval(a.value.replace("X", "*")) - eval(b.value.replace("X", "*"));
                } catch (error) {
                    this.consoleDebug("shvSizeOrderFunction could not eval sort size values\n", error);
                    return 0;
                }
            });
            this.listOfShvBacker = this.buildListFromSlaveSerializableAttributes("ShvBacker");
        }

        imageSrc = (size: { id: number, value: string }): string => {
            let localSize = _.cloneDeep(size);
            localSize.value = localSize.value.replace(/\./g, '_');
            localSize.value = localSize.value.replace(/\s/g, '');
            return this.$filter("corsImageLink")("icons/" + localSize.value + '.svg');
        }

        // NOTE: This must remain an arrow function
        filterBySelectedShvSizeFunction = (value: { id: number, value: string }): boolean => {
            if (!this.listOfShvSize || !this.selectedShvSize) {
                return false;
            }
            // Should only show the possible backers for selected ShvSize
            let filteredVariants = _.filter(this.masterController.productVariants,
                (variant: api.ProductAssociationModel) =>
                    _.some(variant.SlaveSerializableAttributes,
                        (attribute: api.SerializableAttributeObject) => attribute.Key === "ShvSize" && attribute.Value === this.selectedShvSize));
            if (_.some(filteredVariants, (variant: api.ProductAssociationModel) => variant.SlaveID === value.id)) {
                return true;
            }
            return false;
        }

        // Events
        changeVariant(newID: { id: number, value: string }): void {
            if (!this.masterController) {
                return this.consoleDebug("masterController undefined in cefProductVariantsWizard");
            }
            this.$rootScope.$broadcast(this.cvServiceStrings.events.products.selectedVariantChanged, newID.id);
        }
        // Constructor

        constructor(
            private readonly $rootScope: ng.IRootScopeService,
            private readonly $filter: ng.IFilterService,
            protected readonly cefConfig: core.CefConfig,
            private readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefProductVariantsWizard", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            masterController: "=",
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/variants/variantWizard.html", "ui"),
        controller: ProductVariantsWizardController,
        controllerAs: "pvwCtrl",
        bindToController: true
    }));
}
