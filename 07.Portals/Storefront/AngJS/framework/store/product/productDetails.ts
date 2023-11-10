/**
 * @file framework/store/product/productDetails.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Product details class for the storefront
 */
module cef.store.product {
    export class selectedVariant {
        typeID: number;
        id: number;
    }

    export interface IVariantSelection {
        id: number;
        quantity: number;
        name: string;
        friendlyName: string;
        seoUrl: string;
        shortDescription: string;
        price: number;
        customKey: string;
        firstVariant: boolean;
        sortOrder: number;
        originalObject: api.ProductModel;
    }

    export class ProductDetailsController extends core.TemplatedControllerBase {
        // Properties

        /**
         * A model object which is merged between the base variant master and the selected variant
         * override so we only have to bind the UI to one object instead of adding extra HTML logic
         * to bind to two. Even if this isn't a variant setup for the current product, we still fill
         * in this object for UI binding.
         * NOTE: When the variant has data, it should supercede the master for each data point it has
         * @type {api.ProductModel}
         * @default null
         */
        productToDisplay: api.ProductModel = null;
        /**
         * Upon loading the product via SEO URL, if it is determined that this is a non-variant setup
         * product or that this is the master product, this will store that product's information.
         * @type {api.ProductModel}
         * @default null
         */
        productWhichIsTheMaster: api.ProductModel = null;
        /**
         * If it is determined that there is a Variants setup for the product, this is the currently
         * selected variant which should be merged over the master to the {@see productToDisplay}
         * @type {api.ProductModel}
         * @default null
         */
        productWhichIsTheCurrentVariant: api.ProductModel = null;
        /**
         * For single-select variant scenarios, this is the value to bind the UI against for
         * selecting a different variant. On Change, the variant which was newly selected should be
         * loaded and then replacing the {@see productToDisplay} with a new merge with the
         * {@see productWhichIsTheMaster}.
         * @type {api.ProductModel}
         * @default null
         */
        productIDForTheCurrentVariant: number = null;
        /**
         * If it is determined that there is a Variants setup for the product, this is each of the
         * Variants but not the Master
         * @type {api.ProductAssociationModel[]}
         * @default []
         */
        productVariants: api.ProductAssociationModel[] = [];
        /**
         * When the client needs each variant to be selectable in the UI with their own quantities for
         * Bulk Add to Cart. Quantity of 0 (the default) means the item is ignored. The end user will
         * have filled in their own quantity values per item they wish to add to the cart.
         * Single-select or non-variant scenarios will use {@see quantityForAddToCart} instead.
         * @type {IVariantSelection[]}
         * @default []
         */
        productVariantSelections: IVariantSelection[] = [];
        /**
         * While the initial product is loading via SEO URL, this is the promise which is running.
         * Multiple locations within this file are dependant on the product coming back before
         * continuing with their own processing so they listen for this single promise instead of
         * making their own calls to the server.
         * @type {ng.IPromise<api.ProductModel>}
         * @default null
         */
        loadingProductPromise: ng.IPromise<api.ProductModel> = null;

        /**
         * In a non-variant or single-variant selection scenario, this is the quantity value to use
         * for the Add to Cart call. (Multi-variant selection scenation will use
         * {@see productVariantSelections})
         * @type {number}
         * @default 1
         */
        quantityForAddToCart: number = 1;

        /**
         * When reviews are enabled in the site, this stores the review information about the
         * product. This information is pulled separately from the product itself and loaded after
         * the product is loaded. In a variants scenarion, reviews will only be applied to the master
         * to assist with consolidating the information (summaries, averages, etc).
         * @type {api.ProductReviewInformationModel}
         * @default null
         */
        reviewInfo: api.ProductReviewInformationModel = null;

        /**
         * After the product information has fully loaded in an advanced inventory setup, this is the
         * inventory information for each product which has been loaded (master, variants, associated,
         * etc.)
         * @type { { [productID: number]: api.ProductInventoryLocationSectionModel[] } }
         * @default {}
         */
        inventories: { [productID: number]: api.ProductInventoryLocationSectionModel[] } = {};

        associatedLocationSelections: string[] = [];
        associatedQuantitySelections: number[] = [];
        cartItemAttributes = {};
        inventoryLocations: api.InventoryLocationModel[];
        inventorySections: api.InventoryLocationSectionModel[];
        pagings: { [prop: string]: number; } = {};
        selectedShipToOption = core.ShippingOptions.ShipToHome;
        specifications;
        specificationsMobile;
        storeMoreProducts: api.ProductModel[] = null;
        storeProduct: api.StoreProductModel = null;
        usersSelectedStore: api.StoreModel = null;
        previewMode: boolean;
        get selectedShipToOptionResult(): string { return core.ShippingOptions[this.selectedShipToOption]; }
        accountKey: string;
        uomArray: api.SerializableAttributeObject[] = [];
        displayHCPC: boolean = false;
        hcpcCode: string;
        // Functions
        get isKit(): boolean {
            return this.productWhichIsTheMaster
                && ['Kit','Variant Kit'].indexOf(this.productWhichIsTheMaster.TypeName) !== -1;
        }
        random(): number { return 0.5 - Math.random(); }

        private readSeoUrl(seoUrl: string): { seoUrl: string, previewID: number } {
            let url: string = "";
            if (!seoUrl) {
                url = window.location.href;
                seoUrl = url.substring(url.lastIndexOf("/") + 1);
            }
            if (seoUrl.indexOf("#!") !== -1) {
                seoUrl = seoUrl.substr(0, seoUrl.indexOf("#!")); // Remove anchors and other possible arguments from the SEO URL
            }
            if (seoUrl.indexOf("#") !== -1) {
                seoUrl = seoUrl.substr(0, seoUrl.indexOf("#")); // Remove anchors and other possible arguments from the SEO URL
            }
            if (seoUrl === "product-detail" || seoUrl.startsWith("product-detail") || seoUrl === "product" || seoUrl.startsWith("product")) {
                seoUrl = null;
            }
            if (seoUrl == null || seoUrl === "") {
                seoUrl = url.substring(url.lastIndexOf("%2F") + 3);
            }
            if (seoUrl == null || seoUrl === "" || seoUrl.startsWith("tp://")) {
                return null;
            }
            let previewID: number = null;
            if (seoUrl.indexOf("?") !== -1) {
                // Remove query params, if any
                const search = window.location.search.substring(1);
                const queryParams: { [key: string]: any } = JSON.parse(
                    '{"' + search.replace(/&/g, '","').replace(/=/g,'":"') + '"}',
                    (key, value) => key === "" ? value : decodeURIComponent(value));
                previewID = Number(queryParams["preview"]);
                if (previewID > 0) {
                    this.previewMode = true;
                }
                seoUrl = seoUrl.substring(0, seoUrl.indexOf("?"));
            }
            return { seoUrl: seoUrl, previewID: previewID };
        }

        private initialLoadOfProductBySeoUrl(
                quantity: number,
                seoUrl: string,
                action: (promiseValue: api.ProductModel,
                    resolve: (param?: any) => ng.IPromise<any>) => any)
                : ng.IPromise<api.ProductModel> {
            return this.loadingProductPromise = this.$q<api.ProductModel>((resolve, reject) => {
                const read = this.readSeoUrl(seoUrl);
                seoUrl = read.seoUrl;
                if (!seoUrl) {
                    resolve(null);
                    return;
                }
                this.cvProductService.get({
                    seoUrl: seoUrl,
                    storeID: this.usersSelectedStore && this.usersSelectedStore.ID,
                    previewID: read.previewID
                }).then(cached => {
                    if (cached) {
                        action(cached, resolve);
                        return;
                    }
                    // Sometimes it fails to finish on first attempt (race condition)
                    this.cvProductService.get({
                        seoUrl: seoUrl,
                        storeID: this.usersSelectedStore && this.usersSelectedStore.ID
                    }).then(cached => {
                        if (cached) {
                            action(cached, resolve);
                            return;
                        }
                        // TODO: Redirect to 404 page?
                        reject("No product returned");
                    }).catch(reject);
                }).catch(reject);
            });
        }

        private loadProductSimple(quantity: number, seoUrl?: string): ng.IPromise<api.ProductModel> {
            return this.initialLoadOfProductBySeoUrl(
                quantity,
                seoUrl,
                (cached, resolve) => resolve(cached));
        }

        private loadProduct(quantity: number, seoUrl?: string): ng.IPromise<api.ProductModel> {
            return this.$q((resolve, reject) => {
                this.loadProductSimple(quantity, seoUrl).then(pre => {
                    // We have already validated that the returned data is not null
                    if (pre.TypeName.toLowerCase() === "variant"
                        || pre.TypeName.toLowerCase() === "variant kit") {
                        // Variants setup and this is a variant, not the master, determine
                        // and load the master
                        const masterProductSeoUrl = _.find(
                                pre.ProductsAssociatedWith,
                                x => x.TypeName.toLowerCase() === "variant of master")
                            .MasterSeoUrl;
                        this.loadProductSimple(quantity, masterProductSeoUrl).then(masterProduct => {
                            // We have already validated that the returned data is not null
                            resolve(this.getAssociatedProductSwatches(masterProduct));
                        }).catch(reject);
                        return;
                    }
                    if (pre.TypeName.toLowerCase() === "variant master")  {
                        // This is already the master
                        resolve(this.getAssociatedProductSwatches(pre));
                        return;
                    }
                    if (this.cefConfig.googleTagManager.enabled && pre != null) {
                        this.cvGoogleTagManagerService.detail(pre);
                    }
                    // Non-Variant setup for this product, no need to try for swatches
                    resolve(pre);
                }).catch(reason => { console.warn(reason); reject(reason); });
            });
        }

        private getAssociatedProductSwatches(masterProduct: api.ProductModel): ng.IPromise<api.ProductModel> {
            return this.$q((resolve, reject) => {
                if (!masterProduct || !masterProduct.ProductAssociations || !masterProduct.ProductAssociations.length) {
                    resolve(masterProduct);
                    return;
                }
                this.$q.all(masterProduct.ProductAssociations
                    .map(ap => this.cvApi.products.GetProductImages({
                        Active: true,
                        AsListing: true,
                        MasterID: ap.SlaveID,
                        TypeKey: "PRODUCT-SWATCH"
                    })))
                    .then((responseArr: ng.IHttpPromiseCallbackArg<api.ProductImagePagedResults>[]) => {
                        responseArr.map(r => r.data.Results).forEach(images => {
                            if (!images.length) { return; }
                            const ap = _.find(masterProduct.ProductAssociations,
                                x => x.SlaveID == images[0].MasterID);
                            if (!ap) { return; }
                            if (!ap.Slave) { ap.Slave = { } as any; }
                            ap.Slave.Images = images;
                        });
                        resolve(masterProduct);
                    }).catch(reject);
            });
        }

        listKitItems(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/store/product/controls/kits/kitComponents.html", "ui")
            });
        }

        selectedVariantChanged(newID: number): void {
            if (newID) {
                this.productIDForTheCurrentVariant = newID;
            }
            this.loadVariation(_.find(this.productVariants, x => x.SlaveID === this.productIDForTheCurrentVariant).SlaveSeoUrl);
        }

        loadVariation(seoUrl: string): ng.IPromise<api.ProductModel> {
            if (!seoUrl) {
                return this.$q.reject("No seo url, cannot detect variant to load");
            }
            return this.$q((resolve, reject) => {
                this.loadProductSimple(this.quantityForAddToCart, seoUrl).then(currentVariant => {
                    this.productWhichIsTheCurrentVariant = currentVariant;
                    this.productIDForTheCurrentVariant = currentVariant.ID;
                    if (this.$location.search().notifyMe) {
                        this.cvCartService.requireLoginForNotifyMe(currentVariant.ID, true)
                            .then(() => delete this.$location.search().notifyMe);
                    }
                    // Clone the object safely (no chance of modifying the original master object)
                    this.productToDisplay = angular.copy(this.productWhichIsTheMaster);
                    this.checkIfUserCanViewHCPC();
                    this.getInventoryInfoForProductVariants().then(() => {
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.products.variantInfoLoaded);
                    });
                    // Merge the attribute dictionaries
                    if (!this.productWhichIsTheMaster.SerializableAttributes) {
                        this.productWhichIsTheMaster.SerializableAttributes = {};
                    }
                    if (!this.productWhichIsTheCurrentVariant.SerializableAttributes) {
                        this.productWhichIsTheCurrentVariant.SerializableAttributes = {};
                    }
                    const consolidatedAttributes = angular.copy(this.productWhichIsTheMaster.SerializableAttributes);
                    Object.keys(this.productWhichIsTheCurrentVariant.SerializableAttributes)
                        .forEach(key => consolidatedAttributes[key] = this.productWhichIsTheCurrentVariant.SerializableAttributes[key]);
                    angular.merge(this.productToDisplay, this.productWhichIsTheCurrentVariant);
                    this.productToDisplay.SerializableAttributes = consolidatedAttributes;
                    // This is trusted HTML and has to be re-assigned so we don't see [object Object]
                    this.productToDisplay.Description = this.productWhichIsTheCurrentVariant.Description
                        ? this.productWhichIsTheCurrentVariant.Description
                        : this.productWhichIsTheMaster.Description;
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.products.detailProductLoaded);
                    resolve(currentVariant);
                }).catch(reject);
            });
        }

        refreshProduct(): void {
            this.setRunning("Loading Product...");
            this.loadProduct(this.quantityForAddToCart).then(cached => {
                // This product should be either a non-variant setup or the master itself
                // (determined in loadProduct)
                let currentProduct = cached;
                this.productWhichIsTheMaster = currentProduct;
                // Load the data for feature columns by reading the Serializable Attrbutes
                this.loadFeatureColumns(currentProduct.SerializableAttributes, 2, false);
                this.loadFeatureColumns(currentProduct.SerializableAttributes, 1, true);
                this.loadProductUOM(currentProduct.SerializableAttributes);
                // Load Reviews data
                // TODO: Move this to it's own directive so it only loads if enabled and used in the site
                this.cvApi.products.GetProductReview(currentProduct.ID).then(r => this.reviewInfo = r.data);
                // If this product is assigned to the store, try to load other products from
                // the store for specific UI use case
                // TODO: Move this to it's own directive so it doesn't have to load if not used
                if (currentProduct.Stores && currentProduct.Stores.length > 0) {
                    this.storeProduct = currentProduct.Stores[0];
                    // TODO: Move this to the cvProductService for caching the data
                    this.cvApi.products.GetProducts({
                        Active: true,
                        AsListing: true,
                        StoreID: this.storeProduct.MasterID,
                        ExcludedID: currentProduct.ID,
                        Paging: <api.Paging>{ Size: 10, StartIndex: 1 }
                    }).then(r => this.storeMoreProducts = r.data.Results);
                }
                // Perform special handling for variant and kit setups, loading extra data and setting up the UI
                switch (currentProduct.TypeName.toLowerCase()) {
                    case "variant master": {
                        // For Variant setups, look for the first variant so we can auto-select it for page load
                        this.productVariants = currentProduct.ProductAssociations.filter(
                            x => x.TypeName.toLowerCase() === "variant of master");
                        if (!this.productVariants.length) {
                            // TODO: This is a variant master but there are no variants, show warning?
                            this.finishRunning();
                            return;
                        }
                        this.doVariantMaster(null);
                        break;
                    }
                    case "variant-kit":
                    case "kit": {
                        this.getInventoryInfoForKitComponents(currentProduct).then(rs => {
                            this.inventoryLocations = (<api.InventoryLocationPagedResults>rs[0]).Results;
                            this.inventorySections = (<api.InventoryLocationSectionPagedResults>rs[1]).Results;
                            this.inventories = {};
                            for (let i = 2; i < rs.length; i++) {
                                if (!this.inventories[currentProduct.ID]) {
                                    this.inventories[currentProduct.ID] = [];
                                }
                                this.inventories[currentProduct.ID].push(...rs[i].Results); // Concat product inventory arrays
                            }
                            currentProduct.ProductAssociations
                                .filter(x => x.TypeName.toLowerCase() === "kit component")
                                .forEach(x => {
                                    const forSlave = () => {
                                        // Assign a new dynamic property with per-location inventory quantities.
                                        x.Slave["LocationQuantities"] = this.calculateLocationQuantities(
                                            x.Slave,
                                            this.inventories,
                                            this.inventoryLocations,
                                            this.inventorySections);
                                        x.Slave = this.cvInventoryService.factoryAssign(x.Slave);
                                        this.productToDisplay = currentProduct;
                                        this.$rootScope.$broadcast(this.cvServiceStrings.events.products.detailProductLoaded);
                                    };
                                    if (!x.Slave) {
                                        this.cvProductService.get({ id: x.SlaveID }).then(p => {
                                            x.Slave = p;
                                            forSlave();
                                        });
                                        return;
                                    }
                                    forSlave();
                                });
                            this.finishRunning();
                        }).catch(reason => console.error(reason));
                        break;
                    }
                    default: {
                        // If we are returning to this page from the login page with the notifyMe argument in the query
                        // string parameters, add this item to that cart
                        if (this.$location.search().notifyMe) {
                            this.cvCartService.requireLoginForNotifyMe(currentProduct.ID, true)
                                .then(() => delete this.$location.search().notifyMe);
                        }
                        this.productToDisplay = currentProduct;
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.products.detailProductLoaded);
                        this.finishRunning();
                        break;
                    }
                }
            });
        }

        doVariantMaster(seoUrlSrc: string): void {
            const read = this.readSeoUrl(seoUrlSrc);
            const seoUrl = read.seoUrl;
            let variantRelatedToSeoUrl = _.find(this.productVariants, x => seoUrl == x.SlaveSeoUrl);
            if (!variantRelatedToSeoUrl) {
                // Just read the first one
                variantRelatedToSeoUrl = _.sortBy(this.productVariants, x => x.SortOrder || x.SlaveName)[0]
            }
            if (!variantRelatedToSeoUrl) {
                this.consoleDebug("Still no variant by seo url!", seoUrl);
                this.finishRunning(true, "Still no variant by seo url!");
                return;
            }
            this.productIDForTheCurrentVariant = variantRelatedToSeoUrl.SlaveID;
            // Pick up the full data for the first variant and set up the variant multi-selection
            // scenario
            // Use the expansive call to set variant master, then update all sub-variants with pricing to construct grid
            this.loadVariation(variantRelatedToSeoUrl.SlaveSeoUrl).then(() => {
                this.productVariants.map(variant => {
                    this.cvProductService.get({
                        seoUrl: variant.SlaveSeoUrl,
                        storeID: this.usersSelectedStore && this.usersSelectedStore.ID
                    }).then(result => {
                        this.productVariantSelections = this.productVariants.map(variant => {
                            // Return the content for the multi-selection data
                            return <IVariantSelection>{
                                id: variant.SlaveID,
                                quantity: 0,
                                name: variant.SlaveName,
                                friendlyName: variant.UnitOfMeasure,
                                seoUrl: variant.SlaveSeoUrl,
                                ////shortDescription: variant.Slave.ShortDescription,
                                ////price: variant.Slave.readPrices().sale
                                ////    ? variant.Slave.readPrices().sale
                                ////    : variant.Slave.readPrices().base,
                                customKey: variant.SlaveKey,
                                firstVariant: variantRelatedToSeoUrl.SlaveID === variant.SlaveID,
                                sortOrder: variant.SortOrder,
                                originalObject: variant.Slave
                            };
                        });
                    });
                });
                this.finishRunning();
            });
        }

        assignVariation(variant: IVariantSelection): void {
            // Get Associated Variant
            const selectedVariant = _.find(this.productVariantSelections, v => v.id === variant.id);
            // Ensure max of one variant selected
            const selectedVariants = this.productVariantSelections
                .filter(v => v.quantity > 0);
            if (selectedVariants.length > 1 && selectedVariant.quantity > 0) {
                this.productVariantSelections
                    .filter(v => (v.id !== variant.id))
                    .forEach(variantMap => variantMap.quantity = 0);
            }
            let seoUrl: string;
            if (selectedVariant.quantity > 0) {
                this.productIDForTheCurrentVariant = variant.id;
                seoUrl = variant.seoUrl;
                this.quantityForAddToCart = variant.quantity;
            } else {
                const firstVariant = _.find(this.productVariantSelections, v => v.firstVariant === true);
                this.productIDForTheCurrentVariant = firstVariant.id;
                seoUrl = firstVariant.seoUrl;
                this.quantityForAddToCart = firstVariant.quantity;
            }
            this.loadVariation(seoUrl);
        }

        refreshUserStore(): void {
            if (!this.cefConfig.featureSet.stores.enabled) {
                this.refreshProduct();
                return;
            }
            this.setRunning("Getting User's Selected Store");
            this.cvStoreLocationService.getUserSelectedStore()
                .then(store => this.usersSelectedStore = store)
                .catch(reason => reason !== "No selected store ID found" && console.warn(reason))
                .finally(() => this.refreshProduct()); // Will call finishRunning
        }

        currentPage(key: string): number {
            if (!key) { return 0; }
            if (!this.pagings[key]) { this.pagings[key] = 0; }
            return this.pagings[key];
        }
        changePage(key: string, upOrDown: boolean): number {
            if (!key) { return 0; }
            if (!this.pagings[key]) { this.pagings[key] = 0; }
            this.pagings[key] = Math.max(0, this.pagings[key] + (upOrDown ? 1 : -1));
            return this.currentPage(key);
        }

        incrementQuantity(): void { this.quantityForAddToCart = this.quantityForAddToCart + 1; }
        decrementQuantity(): void { this.quantityForAddToCart = this.quantityForAddToCart - 1; }

        getSerializableAttributes(): api.SerializableAttributesDictionary {
            const ciaList = new api.SerializableAttributesDictionary();
            Object.keys(this.cartItemAttributes).forEach(
                key => ciaList[key] = this.cartItemAttributes[key]);
            return ciaList;
        }

        sumKitComponentProductsPriceBase(): number {
            return this.sumKitComponentsPrice(this.productToDisplay, false);
        }
        sumKitComponentProductsPriceSale(): number {
            return this.sumKitComponentsPrice(this.productToDisplay, true);
        }
        private validatProductIsKitWithComponents(p: api.ProductModel): boolean {
            return p != null
                && (p.TypeName.toLowerCase() === "kit" || p.TypeName.toLowerCase() === "variant-kit")
                && p.ProductAssociations
                && p.ProductAssociations.length > 0;
        }
        private sumKitComponentsPrice(p: api.ProductModel, readSale: boolean): number {
            if (!this.validatProductIsKitWithComponents(p)) {
                return 0;
            }
            return _.sumBy(
                p.ProductAssociations
                    .filter(x => x.TypeName === "Kit Component" && x.Slave)
                    .map(x => x.Slave)
                    .filter(x => angular.isFunction(x.readPrices)),
                x => readSale && x.readPrices().isSale ? x.readPrices().sale : x.readPrices().base);
        }

        sumNewVariantInventoryQuantities(p: api.ProductModel = this.productWhichIsTheCurrentVariant): number {
            return this.sumInventoryQuantities(p);
        }
        sumNewVariantInventoryQuantitiesForStore(p: api.ProductModel = this.productWhichIsTheCurrentVariant): number {
            return this.sumInventoryQuantitiesForStore(p);
        }
        sumInventoryQuantities(p: api.ProductModel = this.productToDisplay): number {
            if (!p) {
                return 0;
            }
            return p.readInventory().QuantityOnHand;
        }
        sumInventoryQuantitiesForStore(p: api.ProductModel = this.productToDisplay): number {
            if (!this.usersSelectedStore) {
                return this.sumInventoryQuantities(p);
            }
            if (!p) {
                return 0;
            }
            return p.readInventory().QuantityOnHand;
        }
        getInventoryLocations(): ng.IPromise<api.InventoryLocationPagedResults> {
            return this.$q((resolve, reject) =>
                this.cvApi.inventory.GetInventoryLocations()
                    .then(r => resolve(r.data))
                    .catch(reject));
        }
        getInventorySections(): ng.IPromise<api.InventoryLocationSectionPagedResults> {
            return this.$q((resolve, reject) =>
                this.cvApi.inventory.GetInventoryLocationSections()
                    .then(r => resolve(r.data))
                    .catch(reject));
        }
        getInventoryInfoForProduct(id: number): ng.IPromise<api.ProductInventoryLocationSectionPagedResults> {
            return this.$q((resolve, reject) =>
                this.cvApi.products.GetProductInventoryLocationSections({
                    Active: true,
                    AsListing: true,
                    ProductID: id
                }).then(r => resolve(r.data))
                    .catch(reject));
        }
        getProductInfoForSlave(id: number): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.cvProductService.get({ id: id }).then(product => {
                    _.find(this.productVariants, variant => variant.SlaveID === id).Slave = product;
                    resolve();
                }).catch(reject);
            });
        }
        getInventoryInfoForProductVariants(): ng.IPromise<any> {
            const productIDs = this.productVariants
                .map(x => x.SlaveID);
            const promises: ng.IPromise<any>[] = [];
            for (let i = 0; i < productIDs.length; i++) {
                promises.push(this.getProductInfoForSlave(productIDs[i]));
            }
            return this.$q.all(promises);
        }
        getInventoryInfoForKitComponents(product: api.ProductModel): ng.IPromise<any> {
            // Figure out which products to query for inventory.
            const productIDs = product.ProductAssociations
                .filter(x => x.TypeName === "Kit Component")
                .map(x => x.SlaveID);
            const promises: ng.IPromise<any>[] = [];
            promises.push(this.getInventoryLocations()); // Get location and section data.
            promises.push(this.getInventorySections());
            for (let i = 0; i < productIDs.length; i++) { // Get inventories for each component per location.
                promises.push(this.getInventoryInfoForProduct(productIDs[i]));
            }
            return this.$q.all(promises);
        }
        calculateLocationQuantities(
                product: api.ProductModel,
                productInventories: { [productID: number]: api.ProductInventoryLocationSectionModel[] },
                locations: api.InventoryLocationModel[],
                sections: api.InventoryLocationSectionModel[])
                : { [key: string]: number; } {
            var quantities: { [key: string]: number; } = { };
            locations.forEach(location => { // Loop each location
                quantities[location.CustomKey] = 0; // Initialize quantity for this location
                sections
                    .filter(section => section.InventoryLocationID === location.ID)
                    .forEach(section => { // Loop each inventory section.
                        if (!productInventories[product.ID]) {
                            return;
                        }
                        productInventories[product.ID]
                            .filter(inv => inv.MasterID === product.ID && inv.SlaveID === section.ID)
                            .forEach(inv => { // Loop each inventory record
                                // Increment quantity for this location.
                                quantities[location.CustomKey] += (inv.Quantity || 0) - (inv.QuantityAllocated || 0);
                            });
                    });
            });
            return quantities;
        }
        addSelectedVariantsToCart(): ng.IPromise<void> {
            if (!this.productVariantSelections
                || !this.productVariantSelections.length
                ////|| !_.some(this.productVariantSelections, x => x.selected)) {
                || !_.some(this.productVariantSelections, x => x.quantity > 0)) {
                return this.$q.reject();
            }
            return this.$q((resolve, reject) => {
                this.$q.all(this.productVariantSelections
                    ////.filter(x => x.selected)
                    .filter(x => x.quantity > 0)
                    // .map(x => this.addCurrentItemToCart(x.quantity, null, x.originalObject, true)))
                    .map(x => this.addCurrentItemToCart(x.quantity, null, x, true)))
                    .then(() => resolve(this.refreshProduct()))
                    .catch(reject);
            });
        }
        addCurrentItemToCart(quantity: number, shipOption?: any, item?: any, skipRefresh: boolean = false, forceItem: boolean = false, cartType: string = this.cvServiceStrings.carts.types.cart): ng.IPromise<void> {
            // Check for Discontinued first
            if (this.productToDisplay.IsDiscontinued) {
                return this.$q.reject();
            }
            this.setRunning(this.$translate("ui.storefront.common.AddingToCart.Ellipses"));
            let cartProduct = item;
            // TODO: This was a client-specific workflow that may longer supported in this manner, suggest review and remove
            if (shipOption && this.productWhichIsTheCurrentVariant) {
                cartProduct = shipOption === this.cvServiceStrings.attributes.inStorePickup
                    ? this.productWhichIsTheMaster.ProductAssociations[1].Slave
                    : this.productWhichIsTheCurrentVariant;
            }
            let itemIDToSend = this.productWhichIsTheCurrentVariant ? this.productWhichIsTheCurrentVariant.ID : cartProduct.ID;
            if (forceItem && !angular.isUndefined(item)) {
                itemIDToSend = item.SlaveID || item.ID;
            }
            return this.$q((resolve, reject) => {
                this.cvCartService.addCartItem(
                    itemIDToSend,
                    cartType,
                    quantity,
                    <services.IAddCartItemParams>{
                        storeID: this.usersSelectedStore ? this.usersSelectedStore.ID : null,
                        SerializableAttributes: this.getSerializableAttributes(),
                        selectedShipOption: shipOption
                    },
                    cartProduct
                ).then(() => {
                    if (!skipRefresh) {
                        resolve(this.refreshProduct()); // Will call finish running
                        return;
                    }
                    this.finishRunning();
                    resolve();
                }).catch(reason => { this.finishRunning(true, reason); reject(reason); });
            });
        }
        addRelatedProductToCart(id: number, type: string = this.cvServiceStrings.carts.types.cart, quantity: number = 1, item?: any, params?: services.IAddCartItemParams): void {
            this.cvCartService.addCartItem(id, type, quantity, params, item);
        }
        buyNow(quantity: number = this.quantityForAddToCart): void {
            this.addCurrentItemToCart(quantity).then(() => this.$filter("goToCORSLink")("", "cart"));
        }
        addAssociatedItemToCart(productId: number, quantity: number, locationKey: string): void {
            this.setRunning(this.$translate("ui.storefront.common.AddingToCart.Ellipses"));
            this.cvCartService.addCartItem(
                productId, this.cvServiceStrings.carts.types.cart, quantity,
                <services.IAddCartItemParams>{
                    storeID: this.usersSelectedStore ? this.usersSelectedStore.ID : null,
                    SerializableAttributes: this.getSerializableAttributes(),
                    selectedShipOption: this.selectedShipToOptionResult,
                    inventoryLocationCustomKey: locationKey // Not sure where this param goes.
                }
            ).finally(() => this.finishRunning());
        }
        loadFeatureColumns(
            serializableAttributes: api.SerializableAttributesDictionary,
            columns: number,
            mobile: boolean)
            : void {
            // Filter the attributes
            var features = new Array<api.SerializableAttributeObject>();
            Object.keys(serializableAttributes || {}).forEach(key => {
                if (key.indexOf("_UOM") > -1) { return; }
                if (!serializableAttributes[key].Value || serializableAttributes[key].Value === "") { return; }
                features.push(serializableAttributes[key]);
            });
            // Chunk the attributes into columns
            const rows = Math.ceil(features.length / columns);
            const chunkedFeatures = [];
            for (let r = 0; r < rows; ++r) {
                const cols: Array<Array<string>> = [];
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
            } else {
                this.specifications = chunkedFeatures;
            }
        }
        contactSeller(): void {
            if (angular.isArray(this.productWhichIsTheMaster.Stores) && this.productWhichIsTheMaster.Stores.length) {
                this.cvApi.stores.GetStoreAdministratorUser(this.productWhichIsTheMaster.Stores[0].MasterID)
                    .then(r => this.$state.go("userDashboard.inbox.modalCompose", { contacts: [r.data.Result.ID] }));
            }
        }
        loadProductUOM(serializableAttributes: api.SerializableAttributesDictionary): void {
            if (serializableAttributes && serializableAttributes["AvailableUOMs"]?.Value) {
                this.uomArray = [];
                let uoms = serializableAttributes["AvailableUOMs"]?.Value.toString().split(",");
                uoms.forEach(uom => {
                    let temp = serializableAttributes[uom];
                    temp.Value = temp.Value.toString();
                    this.uomArray.push(temp);
                });
            }
        }
        checkIfUserCanViewHCPC(): boolean {
            this.cvAuthenticationService.preAuth().finally(() => {
                if (this.cvAuthenticationService.isAuthenticated() ) {
                    this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                        this.cvAuthenticationService.getUserByUsername(user.username).then(r => {
                            this.displayHCPC = r.data.SerializableAttributes["CanViewHcpc"]?.Value?.toLowerCase() === "true" ? true : false;
                            if (this.productToDisplay.HCPCCode) {
                                this.hcpcCode = this.productToDisplay.HCPCCode;
                            }
                        });
                    });
                }
            });
            return this.displayHCPC;
        }
        productIsRX(): boolean {
            if (!this.productToDisplay
                || !this.productToDisplay.SerializableAttributes) {
                return false;
            }
            if (this.productToDisplay.SerializableAttributes["PrescriptionDevice"]?.Value 
                || this.productToDisplay.SerializableAttributes["PrescriptionDrug"]?.Value) {
                return true;
            }
            return false;
        }
        load(): void {
            this.refreshUserStore(); // Will load the product, even if stores are disabled
            const unbind1 = this.$scope.$on(this.cvServiceStrings.events.stores.selectionUpdate, () => {
                this.usersSelectedStore = null;
                this.refreshUserStore();
            });
            const unbind2 = this.$scope.$on(this.cvServiceStrings.events.stores.cleared,
                () => this.usersSelectedStore = null);
            const unbind3 = this.$scope.$on(this.cvServiceStrings.events.carts.itemAdded,
                () => this.refreshProduct());
            const unbind4 = this.$scope.$on(this.cvServiceStrings.events.products.selectedVariantChanged,
                (__, newID: number) => this.selectedVariantChanged(newID));
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
                if (angular.isFunction(unbind3)) { unbind3(); }
                if (angular.isFunction(unbind4)) { unbind4(); }
            });
        }

        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $location: ng.ILocationService,
                private readonly $window: ng.IWindowService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $state: ng.ui.IStateService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvCartService: services.ICartService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSecurityService: services.ISecurityService, // Used by UI
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvProductService: services.IProductService,
                private readonly cvProductReviewsService: services.IProductReviewsService, // Used by UI
                private readonly cvStoreLocationService: services.IStoreLocationService,
                private readonly cvInventoryService: services.IInventoryService,
                private readonly cvFacebookPixelService: services.IFacebookPixelService, // Used by UI
                private readonly cvGoogleTagManagerService: services.IGoogleTagManagerService) { // Used by UI
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefProductDetails", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/product/productDetails.html", "ui"),
        controller: ProductDetailsController,
        controllerAs: "productDetailsCtrl"
    }));
}

