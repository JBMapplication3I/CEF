/**
 * @file framework/store/quotes/modals/productRequestForQuoteModal.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com All rights reserved.
 * @desc Product request for quote modal class (only works with a product).
 */
module cef.store.quotes.modals {
    export class ProductRequestForQuoteModalController extends core.TemplatedControllerBase {
        // Properties
        doRecommend = false; // Populated by UI
        doShare = false; // Populated by UI
        message: string = null; // Populated by UI
        unitOfMeasure: string = null; // Populated by UI
        salesQuote: api.SalesQuoteModel; // Populated by this class
        // Functions
        private loadNewSalesItem(): api.SalesItemBaseModel<api.AppliedSalesQuoteItemDiscountModel> {
            const retVal = <api.SalesItemBaseModel<api.AppliedSalesQuoteItemDiscountModel>>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: null,
                SerializableAttributes: new api.SerializableAttributesDictionary(),
                // NameableBase Properties
                Name: null,
                Description: null,
                // SalesItem Properties
                Product: this.product,
                ProductID: this.product.ID,
                ItemType: api.ItemType.Item,
                Quantity: 1,
                UnitCorePrice: null,
                ExtendedPrice: null,
                // Related Objects
                TypeID: 1,
                StatusID: 1,
                StateID: 1
            };
            let prices: api.CalculatedPrices = { base: null, loading: true };
            if (angular.isFunction(this.product.readPrices)) {
                prices = this.product.readPrices();
            }
            retVal.UnitCorePrice = prices.base,
            retVal.UnitSoldPrice = prices.isSale ? prices.sale : prices.base,
            retVal.ExtendedPrice = retVal.UnitSoldPrice,
            retVal.SerializableAttributes["Unit-of-Measure"] = <api.SerializableAttributeObject>{
                ID: 0,
                Key: "Unit-of-Measure",
                Value: ""
            };
            return retVal;
        }
        private loadNew(): api.SalesQuoteModel {
            return <api.SalesQuoteModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                // HaveNotesBase Properties
                Notes: new Array<api.NoteModel>(),
                // SalesCollection Properties
                OriginalDate: new Date(),
                ItemQuantity: null,
                Totals: <api.CartTotals>{
                    SubTotal: 0,
                    Discounts: 0,
                    Handling: 0,
                    Shipping: 0,
                    Fees: 0,
                    Tax: 0,
                    Total: 0
                },
                // SalesQuote Properties
                // Related Objects
                StoreID: this.storeProduct ? this.storeProduct.MasterID : null,
                StoreKey: this.storeProduct ? this.storeProduct.MasterKey : null,
                StoreName: this.storeProduct ? this.storeProduct.MasterName : null,
                StoreSeoUrl: this.storeProduct ? this.storeProduct.MasterSeoUrl : null,
                ShippingSameAsBilling: true,
                TypeID: 1,
                StatusID: 1,
                StateID: 1,
                // Associated Objects
                SalesItems: new Array<api.SalesItemBaseModel<api.AppliedSalesQuoteItemDiscountModel>>(),
                StoredFiles: new Array<api.SalesQuoteFileModel>(),
                SalesQuoteCategories: new Array<api.SalesQuoteCategoryModel>(),
                Discounts: [],
                HasChildren: false
            };
        }
        sendInquiry(): void {
            this.setRunning();
            if (this.salesQuote.SalesItems && this.salesQuote.SalesItems.length && this.salesQuote.SalesItems[0]) {
                this.salesQuote.SalesItems[0].ProductID = this.product.ID;
                const currency = {
                    Name: "US Dollar",
                    CustomKey: "USD",
                    Active: true,
                    CreatedDate: undefined,
                    UnicodeSymbolValue: 24
                } as api.CurrencyModel;
                this.salesQuote.SalesItems[0].OriginalCurrency = currency;
                this.salesQuote.SalesItems[0].SellingCurrency = currency;
                if (this.salesQuote.SalesItems[0].SerializableAttributes["Unit-of-Measure"] != null) {
                    this.salesQuote.SalesItems[0].SerializableAttributes["Unit-of-Measure"].Value = this.unitOfMeasure;
                }
            }
            /*if (this.storeProduct) {
                this.salesQuote.StoreID = this.storeProduct.StoreID;
                if (this.salesQuote.SalesItems[0]) {
                    this.salesQuote.SalesItems[0].StoreProductID = this.storeProduct.ID;
                }
            }*/
            if (this.message) {
                this.salesQuote.Notes.push(<api.NoteModel>{
                    // Base Properties
                    CustomKey: null,
                    Active: true,
                    CreatedDate: new Date(),
                    // Note Properties
                    Note1: this.message,
                    // Related Properties
                    TypeID: null,
                    TypeKey: "Quote Note"
                });
            }
            this.salesQuote["DoRecommendOtherSuppliers"] = this.doRecommend;
            this.salesQuote["DoShareBusinessCardWithSupplier"] = this.doShare;
            this.cvApi.providers.SubmitRequestForQuoteForSingleProduct(this.salesQuote as any).then(r => {
                if (!r.data.ActionSucceeded) {
                    this.finishRunning(true,
                        r.data.Messages && r.data.Messages.length > 0
                            ? r.data.Messages[0]
                            : "An error occurred submitting the request for a quote");
                    return;
                }
                this.finishRunning();
                this.$uibModalInstance.close();
            });
        }
        removeAttachment(index: number): void {
            this.salesQuote.StoredFiles.splice(index, 1);
            this.forms["modalForm"].$setDirty();
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Constructors
        constructor(
                private readonly product: api.ProductModel,
                private readonly storeProduct: api.StoreProductModel,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvContactFactory: factories.IContactFactory) {
            super(cefConfig);
            this.salesQuote = this.loadNew();
            this.salesQuote.SalesItems.push(this.loadNewSalesItem());
        }
    }
}
