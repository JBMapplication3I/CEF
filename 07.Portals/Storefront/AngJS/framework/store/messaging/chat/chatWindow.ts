/**
 * @file framework/store/messaging/chat/chatWindow.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc A controller for the Chat Window feature for the End user
 */
module cef.store.messaging.chat {
    export class ChatWindowController extends ChatWindowBaseController {
        // Properties
        forms: {
            modalForm: ng.IFormController
        };
        product: api.ProductModel = null;
        storeProduct: api.StoreProductModel = null;
        salesOrder: api.SalesOrderModel = null;
        salesReturn: api.SalesReturnModel = null;
        salesQuote: api.SalesQuoteModel = null;
        salesInvoice: api.SalesInvoiceModel = null;
        sampleRequest: api.SampleRequestModel = null;
        contextProduct: api.ProductModel = null;
        contextStoreProduct: api.StoreProductModel = null;
        contextSalesOrder: api.SalesOrderModel = null;
        contextSalesReturn: api.SalesReturnModel = null;
        contextSalesQuote: api.SalesQuoteModel = null;
        contextSalesInvoice: api.SalesInvoiceModel = null;
        contextSampleRequest: api.SampleRequestModel = null;
        hideContextProduct = true;
        hideContextSalesOrder = true;
        hideContextSalesReturn = true;
        hideContextSalesQuote = true;
        hideContextSalesInvoice = true;
        hideContextSampleRequest = true;
        hideForAnyContext  = () =>  this.hideContextProduct ||  this.hideContextSalesOrder ||  this.hideContextSalesReturn ||  this.hideContextSalesQuote ||  this.hideContextSalesInvoice ||  this.hideContextSampleRequest;
        hideForAllContexts = () =>  this.hideContextProduct &&  this.hideContextSalesOrder &&  this.hideContextSalesReturn &&  this.hideContextSalesQuote &&  this.hideContextSalesInvoice &&  this.hideContextSampleRequest;
        showForAnyContext  = () => !this.hideContextProduct || !this.hideContextSalesOrder || !this.hideContextSalesReturn || !this.hideContextSalesQuote || !this.hideContextSalesInvoice || !this.hideContextSampleRequest;
        showForAllContexts = () => !this.hideContextProduct && !this.hideContextSalesOrder && !this.hideContextSalesReturn && !this.hideContextSalesQuote && !this.hideContextSalesInvoice && !this.hideContextSampleRequest;
        // Constructors
        constructor(
                private readonly targetUserId: number,
                private readonly targetProductId: number,
                private readonly targetOrderId: number,
                private readonly targetReturnId: number,
                private readonly targetQuoteId: number,
                private readonly targetInvoiceId: number,
                private readonly targetSampleId: number,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $window: ng.IWindowService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvChatService: services.ChatService,
                protected readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvServiceStrings: services.IServiceStrings) {
            super($rootScope, $scope, $q, $translate, cefConfig, cvApi, cvChatService, cvConfirmModalFactory, cvAuthenticationService, cvServiceStrings);
            this.load();
        }
        // Functions
        load(): void {
            if (this.targetProductId) {
              this.cvApi.products.GetProductByID(this.targetProductId).then(response => {
                  if (!response || !response.data) {
                      return;
                  }
                  this.product = response.data;
                  if (this.product.Stores && this.product.Stores.length > 0) {
                      this.storeProduct = this.product.Stores[0];
                  }
                  this.hideContextProduct = false;
              });
            } else if (this.targetOrderId) {
                this.cvApi.ordering.GetSalesOrderByID(this.targetOrderId).then(response => {
                    if (!response || !response.data) {
                        return;
                    }
                    this.salesOrder = response.data;
                    this.hideContextSalesOrder = false;
                });
            } else if (this.targetReturnId) {
                this.cvApi.returning.GetSalesReturnByID(this.targetReturnId).then(response => {
                    if (!response || !response.data) {
                        return;
                    }
                    this.salesReturn = response.data;
                    this.hideContextSalesReturn = false;
                });
            } else if (this.targetQuoteId) {
                this.cvApi.quoting.GetSalesQuoteByID(this.targetQuoteId).then(response => {
                    if (!response || !response.data) {
                        return;
                    }
                    this.salesQuote = response.data;
                    this.hideContextSalesQuote = false;
                });
            } else if (this.targetInvoiceId) {
                this.cvApi.invoicing.GetSalesInvoiceByID(this.targetInvoiceId).then(response => {
                    if (!response || !response.data) {
                        return;
                    }
                    this.salesInvoice = response.data;
                    this.hideContextSalesInvoice = false;
                });
            } else if (this.targetSampleId) {
                this.cvApi.sampling.GetSampleRequestByID(this.targetSampleId).then(response => {
                    if (!response || !response.data) {
                        return;
                    }
                    this.sampleRequest = response.data;
                    this.hideContextSampleRequest = false;
                });
            }
        }
        selectProductAsContextProduct(): void {
            this.$translate("ui.storefront.messaging.chat.chatWindow.setContext.Product.Template", {
                    name: this.product.Name,
                    url: `${this.$window.location.protocol
                        }//${this.$window.location.host
                        }${this.cefConfig.routes.productDetail.root
                        }/${this.product.SeoUrl}`
                }).then(translated => {
                    this.selectObjectAsContextObjectBase(this.storeProduct.MasterID, translated as string, () => {
                        this.contextProduct = this.product;
                        this.hideContextProduct = true;
                    });
                });
        }
        selectSalesOrderAsContextSalesOrder(): void {
            this.$translate("ui.storefront.messaging.chat.chatWindow.setContext.SalesOrder.Template", { id: this.salesOrder.ID })
                .then(translated => {
                    this.selectObjectAsContextObjectBase(this.salesOrder.StoreID, translated as string, () => {
                        this.contextSalesOrder = this.salesOrder;
                        this.hideContextSalesOrder = true;
                    });
                });
        }
        selectSalesReturnAsContextSalesReturn(): void {
            this.$translate("ui.storefront.messaging.chat.chatWindow.setContext.SalesReturn.Template", { id: this.salesReturn.ID })
                .then(translated => {
                    this.selectObjectAsContextObjectBase(this.salesReturn.StoreID, translated as string, () => {
                        this.contextSalesReturn = this.salesReturn;
                        this.hideContextSalesReturn = true;
                    });
                });
        }
        selectSalesQuoteAsContextSalesQuote(): void {
            this.$translate("ui.storefront.messaging.chat.chatWindow.setContext.SalesQuote.Template", { id: this.salesQuote.ID })
                .then(translated => {
                    this.selectObjectAsContextObjectBase(this.salesQuote.StoreID, translated as string, () => {
                        this.contextSalesQuote = this.salesQuote;
                        this.hideContextSalesQuote = true;
                    });
                });
        }
        selectSalesInvoiceAsContextSalesInvoice(): void {
            this.$translate("ui.storefront.messaging.chat.chatWindow.setContext.SalesInvoice.Template", { id: this.salesInvoice.ID })
                .then(translated => {
                    this.selectObjectAsContextObjectBase(this.salesInvoice.StoreID, translated as string, () => {
                        this.contextSalesInvoice = this.salesInvoice;
                        this.hideContextSalesInvoice = true;
                    });
                });
        }
        selectSampleRequestAsContextSampleRequest(): void {
            this.$translate("ui.storefront.messaging.chat.chatWindow.setContext.SampleRequest.Template", { id: this.sampleRequest.ID })
                .then(translated => {
                    this.selectObjectAsContextObjectBase(this.sampleRequest.StoreID, translated as string, () => {
                        this.contextSampleRequest = this.sampleRequest;
                        this.hideContextSampleRequest = true;
                    });
                });
        }
        // Events
        // <None>
    }
}
