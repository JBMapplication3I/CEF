/**
 * @file admin/controls/sales/salesQuoteResponseWizard.ts
 * @since 2018-03-25
 * @version 4.7
 * @author Clarity Ventures, Inc. Copyright 2018. All rights reserved.
 * @desc The page control (container) for the Sales Quote Response Wizard in CEF Admin
 */
module cef.admin.controls.sales {
    class SalesQuoteResponseWizardController extends core.TemplatedControllerBase {
        // Properties
        storeID: number;
        vendorID: number;
        supplierKind: string;
        stores: Array<api.StoreModel>;
        vendors: Array<api.VendorModel>;
        products: Array<api.ProductModel>;
        record: api.SalesQuoteModel;
        responseRecord: api.SalesQuoteModel;
        paging: core.Paging<api.SalesItemBaseModel<api.AppliedDiscountBaseModel>> = null;
        hasLineItemResponse: {
            [originalSalesItemID: number]: boolean;
        } = {};
        lineItemResponses: {
            [originalSalesItemID: number]: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>;
        } = {};
        fileContent: string = null;
        // Constructors
        constructor(
                readonly $filter: ng.IFilterService,
                private readonly $http: ng.IHttpService,
                private readonly $q: ng.IQService,
                private readonly $timeout: ng.ITimeoutService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $window: ng.IWindowService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly $state: ng.ui.IStateService,
                private readonly $stateParams: ng.ui.IStateParamsService,
                private readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: admin.services.IServiceStrings,
                private readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super(cefConfig);
            this.setRunning();
            this.paging = new core.Paging<api.SalesItemBaseModel<api.AppliedDiscountBaseModel>>($filter);
            this.paging.pageSize = 10;
            this.paging.pageSetSize = 5;
            this.loadAndAssignItems(this.$stateParams["OriginalSalesQuoteID"])
                .then(() => this.responseRecord = this.newRecord());
            this.$scope.$watch(
                () => this.fileContent,
                (newValue, oldValue, scope) =>
                    this.quickImportResponseForm(newValue, oldValue, scope));
        }
        // Functions
        loadAndAssignItems(id: number): ng.IPromise<boolean> {
            return this.$q.all([
                this.cvApi.quoting.GetSalesQuoteByID(id),
                this.cvApi.stores.GetStores({ Active: true, AsListing: true }),
                this.cvApi.vendors.GetVendors({ Active: true, AsListing: true })
            ]).then(rarr => {
                if (!rarr) {
                    this.$translate("ui.admin.controls.sales.salesQuoteResponseWizard.ThereWasAnErrorLoading.Message")
                        .then(translated => this.finishRunning(true, translated as string));
                    return false;
                }
                this.record = (rarr[0] as ng.IHttpPromiseCallbackArg<api.SalesQuoteModel>).data;
                this.paging.data = _.filter(this.record.SalesItems, x => String(x.ItemType) === "Item");
                this.stores = (rarr[1] as ng.IHttpPromiseCallbackArg<api.StorePagedResults>).data.Results;
                this.vendors = (rarr[2] as ng.IHttpPromiseCallbackArg<api.VendorPagedResults>).data.Results;
                this.finishRunning();
                return true;
            });
        }
        newRecord(): api.SalesQuoteModel {
            return <api.SalesQuoteModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                // SalesItemCollection Properties
                ItemQuantity: 0,
                BillingContact: null,
                ShippingContact: null,
                SubtotalItems: 0,
                Totals: this.generateNewTotals(),
                SalesItems: [],
                Discounts: [],
                ShippingSameAsBilling: false,
                // SalesQuote Properties
                Balance: 0,
                HasChildren: false,
                TypeID: 1,
                StateID: 1,
                StatusID: 1
            };
        }
        protected generateNewTotals(): api.CartTotals {
            return <api.CartTotals>{
                SubTotal: 0,
                Shipping: 0,
                Handling: 0,
                Fees: 0,
                Tax: 0,
                Discounts: 0,
                Total: 0
            };
        }
        setHasLineItemResponse(itemID: number, forceOnOff: boolean = undefined): void {
            if (!itemID) { return; }
            this.setRunning();
            if (forceOnOff === undefined) {
                if (!this.hasLineItemResponse[itemID]) {
                    this.clearLineItemResponse(itemID);
                    this.finishRunning();
                    return;
                }
                this.generateLineItemResponseIfNotSet(itemID);
                this.finishRunning();
                return;
            }
            if (forceOnOff === true && this.hasLineItemResponse[itemID]) {
                this.generateLineItemResponseIfNotSet(itemID);
            } else if (forceOnOff === true && !this.hasLineItemResponse[itemID]) {
                // Set on and Initialize it
                this.hasLineItemResponse[itemID] = true;
                this.generateLineItemResponseIfNotSet(itemID);
            } else if (forceOnOff === false && this.hasLineItemResponse[itemID]) {
                // Clear it
                if (this.hasLineItemResponse[itemID]) {
                    delete this.hasLineItemResponse[itemID];
                }
                this.clearLineItemResponse(itemID);
            } else if (forceOnOff === false && !this.hasLineItemResponse[itemID]) {
                this.clearLineItemResponse(itemID);
            }
            this.finishRunning();
        }
        private generateLineItemResponseIfNotSet(itemID: number): void {
            if (!this.lineItemResponses[itemID]) {
                this.lineItemResponses[itemID] = this.generateNewLineItemResponse(itemID);
            }
        }
        private clearLineItemResponse(itemID: number): void {
            if (this.lineItemResponses[itemID]) {
                delete this.lineItemResponses[itemID];
            }
        }
        private generateNewLineItemResponse(itemID: number): api.SalesItemBaseModel<api.AppliedDiscountBaseModel> {
            const originalData = _.find(this.record.SalesItems, x => x.ID === itemID);
            if (!originalData) { return null; };
            const key = `Supplier-${this.supplierKind}-${
                (this.supplierKind === "store" ? this.storeID : this.vendorID)}-Response-Item-to-${itemID}`;
            const attr = new api.SerializableAttributesDictionary();
            attr[`Supplier-${this.supplierKind}-Response-Original-Item`] = <api.SerializableAttributeObject> {
                ID: 0,
                Key: `Supplier-${this.supplierKind}-Response`,
                Value: String(itemID),
                ValueType: "System.Int32",
            };
            return <api.SalesItemBaseModel<api.AppliedDiscountBaseModel>>{
                ID: 0,
                CustomKey: key,
                Active: true,
                CreatedDate: new Date(),
                Quantity: originalData.Quantity,
                UnitCorePrice: originalData.UnitCorePrice,
                UnitSoldPrice: originalData.UnitSoldPrice,
                ExtendedPrice: originalData.ExtendedPrice,
                ItemType: api.ItemType.Item,
                SerializableAttributes: attr,
            };
        }
        refreshProducts(itemID: number): void {
            if (this.supplierKind === "store") {
                this.onStoreIDChange();
            } else if (this.supplierKind === "vendor") {
                this.onVendorIDChange();
            }
            this.lineItemResponses[itemID]["newClicked"] = false;
        }
        editLineItemAtIndex(
            index: number,
            item: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>,
            responseItem: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/sales/modals/attributesCompareModal.html", "ui"),
                size: this.cvServiceStrings.modalSizes.lg,
                controller: modals.AttributesCompareModalController,
                controllerAs: "attributesCompareModalCtrl",
                resolve: {
                    originalItem: () => item,
                    responseItem: () => responseItem
                }
            });
        }
        quickExportResponseForm(): void {
            this.setRunning();
            const arr = this.record.SalesItems.map(o => {
                var lineArray = [];
                lineArray.push(o.ID);
                lineArray.push("'" + o.CustomKey);
                lineArray.push("'" + o.Name);
                if (this.cefConfig.featureSet.salesQuotes.includeQuantity) { lineArray.push(o.Quantity); }
                lineArray.push(o.UnitCorePrice);
                lineArray.push(o.UnitSoldPrice || o.UnitCorePrice);
                if (this.cefConfig.featureSet.salesQuotes.includeQuantity) { lineArray.push(o.ExtendedPrice); }
                if (this.hasLineItemResponse[o.ID]
                    && this.lineItemResponses[o.ID]
                    && this.lineItemResponses[o.ID].ProductID) {
                    lineArray.push(true);
                    lineArray.push("'" + this.lineItemResponses[o.ID].ProductKey);
                    if (this.cefConfig.featureSet.salesQuotes.includeQuantity) {
                        lineArray.push(this.lineItemResponses[o.ID].Quantity);
                    }
                    lineArray.push(this.lineItemResponses[o.ID].UnitSoldPrice
                        || o.UnitSoldPrice
                        || o.UnitCorePrice);
                } else {
                    lineArray.push(false);
                    lineArray.push("");
                    if (this.cefConfig.featureSet.salesQuotes.includeQuantity) { lineArray.push(""); }
                    lineArray.push("");
                    if (this.cefConfig.featureSet.salesQuotes.includeQuantity) { lineArray.push(""); }
                }
                return lineArray;
            });
            if (this.cefConfig.featureSet.salesQuotes.includeQuantity) {
                arr.splice(0, 0, [
                    "Original Item ID","Original Item Key","Original Product","Original Quantity","Original Core Price","Original Selling Price","Original Extended Price",
                    "Respond","Response Product Key","Response Quantity","Response Selling Price"
                ]);
            } else {
                arr.splice(0, 0, [
                    "Original Item ID","Original Item Key","Original Product","Original Core Price","Original Selling Price",
                    "Respond","Response Product Key","Response Selling Price"
                ]);
            }
            const content = arr
                .map(row => row
                    .map(cell => `"${String(cell).replace(/"/, "\"\"")}"`)
                    .join(",")
                ).join("\r\n");
            // A this.$window.open("data:text/csv;filename=export-response-template.csv;charset=utf-8," + encodeURIComponent(content));
            this.download(content, "export-response-template.csv", "text/csv");
            this.finishRunning();
        }
        download(data, filename, type) {
            const file = new Blob([data], { type: type });
            if (window.navigator["msSaveOrOpenBlob"]) { // IE10+
                window.navigator["msSaveOrOpenBlob"](file, filename);
                return;
            }
            // Others
            var a = this.$window.document.createElement("a");
            var url = URL.createObjectURL(file);
            a.href = url;
            a.download = filename;
            document.body.appendChild(a);
            a.click();
            this.$timeout(() => {
                document.body.removeChild(a);
                window.URL.revokeObjectURL(url);
            }, 0);
        }
        quickImportResponseForm(newValue: string, oldValue: string, scope: ng.IScope) {
            if (!this.fileContent) { return; }
            this.setRunning();
            const arr = this.csvToArray(this.fileContent);
            var headers = arr.splice(0, 1)[0];
            arr.forEach(row => {
                if (!row || row.length < headers.length) {
                    return;
                }
                var itemID = Number(row[0]);
                for (let i = 1; i < headers.length; i++) {
                    switch (headers[i]) {
                        case "Original Item ID":
                        case "Original Item Key":
                        case "Original Product":
                        case "Original Quantity":
                        case "Original Core Price":
                        case "Original Selling Price":
                        case "Original Extended Price": {
                            continue;
                        }
                        case "Respond": {
                            const respond = row[i]
                                && (row[i].trim().toLowerCase() === "1"
                                    || row[i].trim().toLowerCase() === "true");
                            if (this.hasLineItemResponse[itemID]) {
                                if (!respond) {
                                    // Will clear the existing data
                                    this.setHasLineItemResponse(itemID, false);
                                    // Get out of the header loop and move on to next row
                                    return;
                                } else {
                                    // Will fill in with blank data to start
                                    this.setHasLineItemResponse(itemID, true);
                                    continue;
                                }
                            } else {
                                if (respond) {
                                    // Will fill in with blank data to start
                                    this.setHasLineItemResponse(itemID, true);
                                    continue;
                                } else {
                                    // Will clear the existing data
                                    this.setHasLineItemResponse(itemID, false);
                                    // Get out of the header loop and move on to next row
                                    return;
                                }
                            }
                        }
                        case "Response Product Key": {
                            // There could have originally been a zero-padded number as the key, check for that too
                            var key = row[i][0] === "'" ? row[i].substr(1) : row[i];
                            const product = _.find(this.products, x => x.CustomKey === key || x.CustomKey.endsWith("0" + key) );
                            if (!product) {
                                this.$translate("ui.admin.controls.sales.salesQuoteResponseWizard.CouldNotFindProductWithKey")
                                .then(translated => {
                                    this.finishRunning(true, `${translated}'${row[i]}'`);
                                    throw Error(`${translated}'${row[i]}'`);
                                });
                                return;
                            }
                            this.lineItemResponses[itemID].ProductID = product.ID;
                            this.onProductIDChange(itemID);
                            continue;
                        }
                        case "Response Quantity": {
                            this.lineItemResponses[itemID].Quantity = Number(row[i]);
                            continue;
                        }
                        case "Response Selling Price": {
                            this.lineItemResponses[itemID].UnitSoldPrice = Number(row[i]);
                            continue;
                        }
                        default: {
                            this.finishRunning(true, `Unknown header: '${headers[i]}'`);
                            throw Error(`Unknown header: '${headers[i]}'`);
                        }
                    }
                }
            });
            this.fileContent = null;
            this.finishRunning();
        }
        /**
         * ref: http://stackoverflow.com/a/1293163/2343
         * This will parse a delimited string into an array of
         * arrays. The default delimiter is the comma, but this
         * can be overridden in the second argument. */
        private csvToArray(strData: string, strDelimiter: string = null): string[][] {
            // Check to see if the delimiter is defined. If not, then default to comma
            strDelimiter = (strDelimiter || ",");
            // Create a regular expression to parse the CSV values.
            const objPattern = new RegExp((
                    // Delimiters
                    `(\\${strDelimiter}|\\r?\\n|\\r|^)`
                    // Quoted fields
                    + `(?:\"([^\"]*(?:\"\"[^\"]*)*)\"|`
                    // Standard fields
                    + `([^\"\\${strDelimiter}\\r\\n]*))`
                ),
                "gi");
            // Create an array to hold our data. Give the array a default empty first row
            const arrData: string[][] = [[]];
            // Create an array to hold our individual pattern matching groups
            let arrMatches: RegExpExecArray;
            // Keep looping over the regular expression matches until we can no longer find a match
            while ((arrMatches = objPattern.exec(strData))){
                // Get the delimiter that was found.
                const strMatchedDelimiter = arrMatches[ 1 ];
                /* Check to see if the given delimiter has a length (is not the start of string) and
                 * if it matches field delimiter. If id does not, then we know that this delimiter
                 * is a row delimiter. */
                if (strMatchedDelimiter.length && strMatchedDelimiter !== strDelimiter) {
                    // Since we have reached a new row of data, add an empty row to our data array
                    arrData.push([]);
                }
                let strMatchedValue;
                /* Now that we have our delimiter out of the way, let's check to see which kind of
                 * value we captured (quoted or unquoted). */
                if (arrMatches[2] && arrMatches[2] != undefined){
                    // We found a quoted value. When we capture this value, unescape any double quotes
                    strMatchedValue = arrMatches[2].replace(/\"\"/g, "\"");
                } else {
                    // We found a non-quoted value.
                    strMatchedValue = arrMatches[3];
                }
                // Now that we have our value string, let's add it to the data array
                arrData[arrData.length - 1].push(strMatchedValue);
            }
            // Return the parsed data
            return arrData;
        }
        save(): void {
            if (!this.lineItemResponses || !Object.keys(this.lineItemResponses).length) {
                // Nothing to push
                return;
            }
            if (!this.responseRecord.SalesItems.length) {
                // Just push all to it
                this.responseRecord.CustomKey = `Supplier-${this.supplierKind}-${
                    (this.supplierKind === "store" ? this.storeID : this.vendorID)}-Response-to-${this.record.ID}`;
                angular.forEach(Object.keys(this.lineItemResponses), key => {
                    var lineItem = this.lineItemResponses[key] as api.SalesItemBaseModel<api.AppliedSalesQuoteItemDiscountModel>;
                    this.responseRecord.SalesItems.push(lineItem);
                });
                this.cvApi.quoting.CreateSalesQuote(this.responseRecord).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        console.error("There was an error saving the sales quote response");
                        console.error(r && r.data);
                        return;
                    }
                    this.cvApi.quoting.GetSalesQuoteByID(r.data.Result).then(r2 => {
                        this.responseRecord = r2.data;
                    });
                });
                return;
            }
            var updatedOrAddedIndexes: number[] = [];
            angular.forEach(Object.keys(this.lineItemResponses), key => {
                var existing = _.find(this.responseRecord.SalesItems, x => x.CustomKey == key);
                var lineItem = this.lineItemResponses[key] as api.SalesItemBaseModel<api.AppliedSalesQuoteItemDiscountModel>;
                if (existing) {
                    const index = this.responseRecord.SalesItems.indexOf(existing);
                    this.responseRecord.SalesItems[index] = lineItem;
                    updatedOrAddedIndexes.push(index);
                } else {
                    this.responseRecord.SalesItems.push(lineItem);
                    updatedOrAddedIndexes.push(this.responseRecord.SalesItems.length - 1);
                }
            });
            updatedOrAddedIndexes = updatedOrAddedIndexes.sort();
            var indexesToRemove: number[] = [];
            this.responseRecord.SalesItems.forEach((x, index) => {
                if (updatedOrAddedIndexes.indexOf(index)) { return; }
                indexesToRemove.push(index);
            });
            indexesToRemove = indexesToRemove.sort();
            for (const i = 0; i < indexesToRemove.length; ) {
                const index = indexesToRemove.pop();
                this.responseRecord.SalesItems[index].Active = false;
            }
            this.cvApi.quoting.UpsertSalesQuote(this.responseRecord).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    console.error("There was an error saving the sales quote response");
                    console.error(r && r.data);
                    return;
                }
                this.cvApi.quoting.GetSalesQuoteByID(r.data.Result).then(r2 => {
                    this.responseRecord = r2.data;
                });
            });
        }
        private checkForExistingResponse(): void {
            var lookupKey = `Supplier-${this.supplierKind}-${
                this.supplierKind === "store" ? this.storeID : this.vendorID}-Response-to-${this.record.ID}`;
            this.cvApi.quoting.CheckSalesQuoteExistsByKey(lookupKey).then(r1 => {
                if (!r1 || !r1.data) {
                    console.log(`No existing quote response for '${lookupKey}'`);
                    return;
                }
                this.cvApi.quoting.GetSalesQuoteByID(r1.data).then(r2 => {
                    this.responseRecord = r2.data;
                    this.parseExistingResponseIntoMemory();
                });
            });
        }
        private parseExistingResponseIntoMemory(): void {
            if (!this.responseRecord) {
                return;
            }
            this.responseRecord.SalesItems.forEach(x => {
                if (!x.SerializableAttributes[`Supplier-${this.supplierKind}-Response-Original-Item`]) {
                    return; // Wrong/bad data
                }
                var itemID = Number(x.SerializableAttributes[`Supplier-${this.supplierKind}-Response-Original-Item`].Value);
                this.hasLineItemResponse[itemID] = true;
                this.lineItemResponses[itemID] = x;
            });
        }
        exportTheSalesQuoteForVendor(): void {
            this.setRunning();
            this.$translate("ui.admin.controls.sales.salesQuoteDetail.ExportQuote.Message")
                .then(translated => this.cvConfirmModalFactory(translated as string).then(() => {
                // TODO: Select a mapping config to export with
                /* Download a file stream and initiate a file save dialog per:
                 * http://jaliyaudagedara.blogspot.com/2016/05/angularjs-download-files-by-sending.html
                 */
                this.$http({
                    method: "POST",
                    url: this.$filter("corsLink")(["Providers", "SalesQuoteImporters", "Excel", "ExportSalesQuoteAsFile", this.record.ID].join("/"), "api"),
                    data: JSON.stringify({ "MappingKey": "mapping.vendor-export", }),
                    responseType: "arraybuffer"
                }).then(response/*(data, status, headers)*/ => {
                        if (response.status === 204) {
                            this.finishRunning(true, "ERROR: There was no content returned for the file.");
                            return;
                        }
                        var responseHeaders = response.headers();
                        var filename = responseHeaders["x-filename"];
                        var contentType = responseHeaders["content-type"];
                        var linkElement = document.createElement("a");
                        try {
                            const blob = new Blob([response.data as any], { type: contentType });
                            const url = window.URL.createObjectURL(blob);
                            linkElement.setAttribute("href", url);
                            linkElement.setAttribute("download", filename);
                            const clickEvent = new MouseEvent("click", {
                                "view": window,
                                "bubbles": true,
                                "cancelable": false
                            });
                            linkElement.dispatchEvent(clickEvent);
                            this.finishRunning();
                        } catch (ex) {
                            this.finishRunning(true, ex);
                        }
                    },
                    result => this.finishRunning(true, `ERROR: ${result.status} - ${result.statusText}`)
                ).catch(reason => this.finishRunning(true, reason));
            }));
        }
        // Events
        newProductClicked(itemID: number): void {
            this.lineItemResponses[itemID]["newClicked"] = true;
            const params = this.supplierKind === "store"
                ? { "storeID": this.storeID }
                : { "vendorID": this.vendorID };
            const link = this.$state.href(
                "inventory.products.newFromSupplier",
                params,
                <ng.ui.IHrefOptions>{ absolute: true });
            this.$window.open(link, "_blank");
        }
        onStoreIDChange(): void {
            if (this.supplierKind !== "store" || !this.storeID) {
                this.products = null;
                return;
            }
            this.cvApi.products.GetProducts({ Active: true, AsListing: true, StoreID: this.storeID })
                .then(r => this.products = r.data.Results);
            this.checkForExistingResponse();
        }
        onVendorIDChange(): void {
            if (this.supplierKind !== "vendor" || !this.vendorID) {
                this.products = null;
                return;
            }
            this.cvApi.products.GetProducts({ Active: true, AsListing: true, VendorID: this.vendorID })
                .then(r => this.products = r.data.Results);
                this.checkForExistingResponse();
        }
        onProductIDChange(itemID: number): void {
            const product = _.find(
                this.products,
                x => x.ID === this.lineItemResponses[itemID].ProductID);
            const response = this.lineItemResponses[itemID];
            response.ProductKey = product.CustomKey;
            response.ProductName = product.Name;
            const quantity = response.Quantity
                + (response.QuantityBackOrdered || 0)
                + (response.QuantityPreSold || 0);
            function doReadPrices(): void {
                const prices = product["readPrices"]();
                response.UnitCorePrice = prices.base;
                response.UnitSoldPrice = prices.sale || prices.base;
                response.ExtendedPrice = response.UnitSoldPrice * quantity;
            }
            if (angular.isFunction(product["readPrices"])) {
                doReadPrices();
                return;
            }
            this.cvApi.pricing.GetPricesForProductAsUser(product.ID, quantity, this.record.UserID).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    console.error(r.data);
                    return;
                }
                product["readPrices"] = (): { base: number, sale?: number } => {
                    return {
                        base: r.data.Result.BasePrice,
                        sale: r.data.Result.SalePrice,
                    };
                };
                doReadPrices();
            });
        }
        // Context Menu Managment
        dataItems: { [id: number]: any } = { };
        currentMenuDataItem: any = null;
        commandCellLineItem: { [id: number]: ng.IAugmentedJQuery } = { };
        // NOTE: This must remain an arrow function
        setCommandCell = (id: number): void => {
            this.commandCellLineItem[id] = angular.element(document.querySelector(`#commandCellLineItem${id}`));
        };
        // NOTE: This must remain an arrow function
        openMenu = (id: number, item: any): void => {
            this.currentMenuDataItem = item;
            this.dataItems[id] = item;
            var menu = $(`#menuActionsLineItem${id}`).data("kendoContextMenu");
            if (menu && angular.isFunction(menu.open)) {
                const rect = $(`#commandCellLineItem${id}`)[0].getBoundingClientRect();
                const x = rect.left;
                const y = rect.top + rect.height;
                menu.open(x, y);
            }
        };
        // NOTE: This must remain an arrow function
        onMenuOpen = (id: number, item: any): void => {
            this.currentMenuDataItem = item;
            this.dataItems[id] = item;
            // Close other menus
            if (this.dataItems) {
                angular.forEach(Object.keys(this.dataItems), diID => {
                    if (String(diID) === String(id)) { return; }
                    var menu = $(`#menuActionsLineItem${diID}`).data("kendoContextMenu");
                    if (menu && angular.isFunction(menu.close)) {
                        menu.close(null);
                    }
                    delete this.dataItems[diID];
                });
            }
        };
        // NOTE: This must remain an arrow function
        closeMenu = (id: number): void => {
            if (this.currentMenuDataItem && this.currentMenuDataItem.ID === id) {
                this.currentMenuDataItem = null;
            }
        };
    }

    adminApp.directive("cefSalesQuoteResponseWizard", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesQuoteResponseWizard.html", "ui"),
        controller: SalesQuoteResponseWizardController,
        controllerAs: "salesQuoteResponseWizardCtrl"
    }));

    adminApp.factory("readFile", ($window: ng.IWindowService, $q: ng.IQService) => (file): ng.IPromise<string> => $q<string>((resolve, reject) => {
        const reader = new $window.FileReader();
        reader.onload = (ev) => resolve(ev.target.result as string);
        reader.readAsText(file);
    }));

    adminApp.directive("fileBrowser", (readFile): ng.IDirective => ({
        scope: { content: "=" },
        transclude: true, // Required
        // Note: This is a control-specific inline template that is required
        template: `<input type="file" class="hide" /><ng-transclude></ng-transclude>`,
        link(scope: ng.IScope, element: ng.IAugmentedJQuery) {
            const fileInput = element.children("input[type=file]");
            fileInput.on("change", (event) => {
                const file = event.target["files"][0];
                readFile(file).then(content => scope["content"] = content);
            });
            element.on("click", () => fileInput[0].click());
        }
    }));
}
