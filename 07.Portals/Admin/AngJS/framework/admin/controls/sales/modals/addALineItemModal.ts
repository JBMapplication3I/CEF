module cef.admin.controls.sales.modals {
    export class AddALineItemModalController extends core.TemplatedControllerBase {
        // Properties
        forms: { edit: ng.IFormController };
        newValue: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>;
        currencies: Array<api.CurrencyModel>;
        products: Array<api.ProductModel>;
        productFilter: string;
        get relevantResponses(): Array<api.SalesQuoteModel> {
            return this.cvLineItemAnalyzerService.relevantResponses[this.newValue.ID];
        }
        get relevantResponseItems(): Array<api.SalesItemBaseModel<api.AppliedDiscountBaseModel>> {
            return this.cvLineItemAnalyzerService.relevantResponseItems[this.newValue.ID];
        }
        // Functions
        subtotal(): number {
            return (this.newValue.Quantity + (this.newValue.QuantityBackOrdered || 0) + (this.newValue.QuantityPreSold || 0))
                * (this.newValue.UnitSoldPrice || this.newValue.UnitCorePrice);
        }
        updateProductsList($viewValue: string): ng.IPromise<Array<api.ProductModel>> {
            return this.$q((resolve, reject) => {
                const dto = <api.GetProductsDto>{
                    Active: true,
                    AsListing: true,
                    IDOrCustomKeyOrNameOrDescription: this.productFilter || $viewValue,
                    Paging: { StartIndex: 1, Size: 8 }
                };
                this.cvApi.products.GetProducts(dto)
                    .then(r => resolve(this.products = r.data.Results))
                    .catch(reject);
            });
        }
        updateOriginalCurrency(): void {
            this.newValue.UnitCorePriceInSellingCurrency = null;
            this.newValue.UnitSoldPriceInSellingCurrency = null;
            if (!this.newValue.OriginalCurrencyID) {
                this.newValue.OriginalCurrency = null;
                ////this.newValue.OriginalCurrencyID = null;
                this.newValue.OriginalCurrencyKey = null;
                this.newValue.OriginalCurrencyName = null;
                return;
            }
            const currency = _.find(this.currencies, x => x.ID === this.newValue.OriginalCurrencyID);
            this.newValue.OriginalCurrency = currency;
            ////this.newValue.OriginalCurrencyID = currency.ID;
            this.newValue.OriginalCurrencyKey = currency.CustomKey;
            this.newValue.OriginalCurrencyName = currency.Name;
        }
        updateSellingCurrency(): void {
            this.newValue.UnitCorePriceInSellingCurrency = null;
            this.newValue.UnitSoldPriceInSellingCurrency = null;
            if (!this.newValue.SellingCurrencyID) {
                this.newValue.SellingCurrency = null;
                ////this.newValue.SellingCurrencyID = null;
                this.newValue.SellingCurrencyKey = null;
                this.newValue.SellingCurrencyName = null;
                    return;
            }
            const currency = _.find(this.currencies, x => x.ID === this.newValue.SellingCurrencyID);
            this.newValue.SellingCurrency = currency;
            ////this.newValue.SellingCurrencyID = currency.ID;
            this.newValue.SellingCurrencyKey = currency.CustomKey;
            this.newValue.SellingCurrencyName = currency.Name;
        }
        calcSellCur(doSale: boolean): void {
            if (!this.newValue.OriginalCurrencyKey
                || !this.newValue.SellingCurrencyKey
                || this.newValue.OriginalCurrencyKey === this.newValue.SellingCurrencyKey) {
                return;
            }
            this.setRunning();
            this.cvApi.currencies.ConvertCurrencyValueAtoB({
                KeyA: this.newValue.OriginalCurrencyKey,
                KeyB: this.newValue.SellingCurrencyKey,
                Value: doSale ? this.newValue.UnitSoldPrice : this.newValue.UnitCorePrice
            }).then(r => {
                if (!r || angular.isUndefined(r.data) || r.data === null) {
                    // Bad data returned
                    return;
                }
                if (doSale) {
                    this.newValue.UnitSoldPriceInSellingCurrency = r.data;
                    return;
                }
                this.newValue.UnitCorePriceInSellingCurrency = r.data;
            }).finally(() => this.finishRunning());
        }
        ok(): void {
            this.newValue.ExtendedPrice = this.subtotal();
            this.$uibModalInstance.close(this.newValue);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        onOpen(): void {
            this.newValue = <api.SalesItemBaseModel<api.AppliedDiscountBaseModel>>{
                Active: true,
                CreatedDate: new Date(),
                ItemType: "Item" as any,
                QuantityBackOrdered: 0,
                QuantityPreSold: 0
            };
            if (this.cefConfig.featureSet.multiCurrency.enabled) {
                this.cvApi.currencies.GetCurrencies({ Active: true, AsListing: true })
                    .then(r => this.currencies = r.data.Results);
            }
            switch (this.kind) {
                case "Sales Quote": { break; }
                case "Sales Order": { break; }
                case "Sales Invoice": { break; }
                case "Sales Return": { break; }
                default: { throw new Error("Unsupported kind"); }
            }
        }
        onSelectProduct($item: api.ProductModel, $model: number, $label: string, $event: ng.IAngularEvent): void {
            this.cvApi.products.AdminGetProductFull($item.ID).then(r => {
                if (!r.data) {
                    return;
                }
                this.newValue["Product"] = r.data
                this.newValue.ProductID = r.data.ID;
                this.newValue.ProductKey = this.newValue.Sku = r.data.CustomKey;
                this.newValue.ProductName = this.newValue.Name = r.data.Name;
                this.newValue.ProductDescription = this.newValue.Description = r.data.ShortDescription || r.data.Description;
                this.newValue.ProductSeoUrl = r.data.SeoUrl;
                this.newValue.ProductIsUnlimitedStock = r.data.IsUnlimitedStock;
                this.newValue.ProductAllowBackOrder = r.data.AllowBackOrder;
                this.newValue.ProductIsEligibleForReturn = r.data.IsEligibleForReturn;
                this.newValue.ProductRestockingFeePercent = r.data.RestockingFeePercent;
                this.newValue.ProductRestockingFeeAmount = r.data.RestockingFeeAmount;
                this.newValue.ProductNothingToShip = r.data.NothingToShip;
                this.newValue.ProductIsTaxable = r.data.IsTaxable;
                this.newValue.ProductTaxCode = r.data.TaxCode;
                this.newValue.ProductShortDescription = r.data.ShortDescription;
                this.newValue.ProductUnitOfMeasure = r.data.UnitOfMeasure;
                this.newValue.ProductSerializableAttributes = r.data.SerializableAttributes;
                this.newValue.ProductTypeID = r.data.TypeID;
                this.newValue.ProductTypeKey = r.data.TypeKey;
                this.cvApi.pricing.GetPricesForProductAsUser($item.ID, 1, this.targetUserID).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        console.error(r.data);
                        return;
                    }
                    this.newValue.UnitCorePrice = r.data.Result.BasePrice;
                    this.newValue.UnitSoldPrice = r.data.Result.SalePrice || r.data.Result.BasePrice;
                    this.newValue.ExtendedPrice = this.subtotal();
                });
            });
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvLineItemAnalyzerService: services.ILineItemAnalyzerService,
                private kind: string,
                private responses: Array<api.SalesQuoteModel>,
                private readonly targetUserID: number) {
            super(cefConfig);
            this.onOpen();
        }
    }
}
