/**
 * @file framework/store/quotes/modals/productRequestForQuoteModal.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com All rights reserved.
 * @desc Product request for quote modal class (only works with a product).
 */
module cef.store.quotes.modals {
    export class RequestForQuoteModalController extends core.TemplatedControllerBase {
        // Properties
        doShare = false; // Populated by UI
        isUrgentRequest = false; // Populated by UI
        getExtraQuotes = false; // Populated by UI
        categories: Array<{ value: number, text: string }>; // Populated by this class on load
        categoryID: number = null; // Populated by UI
        keywords: string = null; // Populated by UI
        quantity = 1; // Populated by UI
        message: string = null; // Populated by UI
        unitOfMeasure: string = null; // Populated by UI
        preferredUnitPrice = 0; // Populated by UI
        currencies: Array<api.CurrencyModel>; // Populated by this class on load
        preferredCurrencyID: number = null; // Populated by UI
        preferredDestinationPort: string = null; // Populated by UI
        paymentMethods: Array<api.PaymentMethodModel>; // Populated by this class on load
        preferredPaymentMethodID: number = null; // Populated by UI
        preferredFOB: string = null; // Populated by UI
        salesQuote: api.SalesQuoteModel; // Populated by this class
        // Functions
        private load(): void {
            this.$q.all([
                this.cvApi.currencies.GetCurrencies({ Active: true, AsListing: true }),
                this.cvApi.payments.GetPaymentMethods({ Active: true, AsListing: true }),
                this.cvApi.categories.GetCategories({ Active: true, AsListing: true, IncludeInMenu: true, IncludeChildrenInResults: false })
            ]).then((rarr: ng.IHttpPromiseCallbackArg<any>[]) => {
                this.currencies = (rarr[0].data as api.CurrencyPagedResults).Results;
                this.paymentMethods = (rarr[1].data as api.PaymentMethodPagedResults).Results;
                this.categories = (rarr[2].data as api.CategoryPagedResults).Results
                    .map(x => { return { value: x.ID, text: x.DisplayName || x.Name }; });
                this.salesQuote = <api.SalesQuoteModel>{
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
                    SerializableAttributes: new api.SerializableAttributesDictionary(),
                    // Related Objects
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
                this.salesQuote.SalesItems.push(this.loadNewSalesItem());
            });
        }
        private newSalesQuoteCategory(id: number): api.SalesQuoteCategoryModel {
            return <api.SalesQuoteCategoryModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CustomKey: null,
                CreatedDate: new Date(),
                // Related Objects
                CategoryID: id,
                SalesQuoteID: 0
            };
        }
        private loadNewSalesItem(): api.SalesItemBaseModel<api.AppliedSalesQuoteItemDiscountModel> {
            const retVal = <api.SalesItemBaseModel<api.AppliedSalesQuoteItemDiscountModel>>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // SalesItem Properties
                ExtendedPrice: null,
                ExtendedShippingAmount: null,
                ItemType: api.ItemType.Item,
                SalesOrderDate: new Date(),
                SerializableAttributes: new api.SerializableAttributesDictionary(),
                Quantity: 1,
                UnitCorePrice: null,
                // Related Objects
                TypeID: 1,
                StatusID: 1,
                StateID: 1
                // Associated Objects
            };
            retVal.SerializableAttributes["Keywords"] = <api.SerializableAttributeObject>{
                ID: 0,
                Key: "Keywords",
                Value: ""
            };
            retVal.SerializableAttributes["Unit-of-Measure"] = <api.SerializableAttributeObject>{
                ID: 0,
                Key: "Unit-of-Measure",
                Value: ""
            };
            return retVal;
        }
        sendInquiry(): void {
            this.setRunning();
            if (this.preferredUnitPrice) {
                this.salesQuote.SalesItems[0].UnitSoldPriceInSellingCurrency = this.preferredUnitPrice;
                this.salesQuote.SalesItems[0].SellingCurrencyID = this.preferredCurrencyID;
            }
            this.salesQuote.SalesItems[0].SerializableAttributes["Keywords"].Value = this.keywords;
            this.salesQuote.SalesItems[0].SerializableAttributes["Unit-of-Measure"].Value = this.unitOfMeasure;
            if (this.categoryID) {
                this.salesQuote.SalesQuoteCategories.push(this.newSalesQuoteCategory(this.categoryID));
            }
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
            this.salesQuote["DoShareBusinessCardWithSupplier"] = this.doShare;
            if (this.getExtraQuotes) {
                this.salesQuote.SerializableAttributes["Get-Extra-Quotes"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Get-Extra-Quotes",
                    Value: String(true)
                };
            }
            if (this.isUrgentRequest) {
                this.salesQuote.SerializableAttributes["Urgent-Request"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Urgent-Request",
                    Value: String(true)
                };
            }
            if (this.preferredPaymentMethodID) {
                this.salesQuote.SerializableAttributes["Preferred-Payment-Method"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Preferred-Payment-Method",
                    Value: String(this.preferredPaymentMethodID)
                };
            }
            if (this.preferredFOB) {
                this.salesQuote.SerializableAttributes["Preferred-FOB"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Preferred-FOB",
                    Value: this.preferredFOB
                };
            }
            if (this.preferredDestinationPort) {
                this.salesQuote.SerializableAttributes["Preferred-Destination-Port"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Preferred-Destination-Port",
                    Value: this.preferredDestinationPort
                };
            }
            this.cvApi.providers.SubmitRequestForQuoteForGenericProducts(this.salesQuote as any).then(r => {
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
        // Constructors
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                private readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.load();
        }
    }
}
