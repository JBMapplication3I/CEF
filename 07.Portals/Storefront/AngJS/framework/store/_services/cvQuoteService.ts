module cef.store.services {
    export interface IQuoteService {
        getItems(): any[];
        observeItems(callback: (...args: any) => any): () => void;
        addItem(id: number, quantity?: number, product?: any): ng.IPromise<any>;
        addItemDetailed(itemParams: any): ng.IPromise<any>;
        addFiles(filesArr: any[]): void;
        baseQuoteItem(): any;
        removeItem(id: number): ng.IPromise<any>;
        addToCart(id: number, quantity?: number, product?: any): ng.IPromise<any>;
        checkout(props: any): ng.IPromise<any>;
        convertCartToQuote(): ng.IPromise<any>;
        doCallbacks(): void;
        setLoadExtended(): void;
        updateLocalItems(itemList: Array<any>): void;
        fetchItems(): ng.IPromise<any>;
        clearItems(): ng.IPromise<any>;
        loadCartExtended(): ng.IPromise<any>;
        loadCart(): ng.IPromise<any>;
    }

    export class QuoteService implements IQuoteService {
        // Constants
        readonly cartTypeName = this.cvServiceStrings.carts.types.quote;
        readonly observerCallbacks: Array<(...any) => any> = [];
        readonly quoteItemBase = <api.SalesItemBaseModel<api.AppliedDiscountBaseModel>>{
            ID: 0,
            Active: true,
            CreatedDate: new Date(),
            ProductID: null,
            Quantity: 1,
            TypeName: this.cartTypeName,
            SerializableAttributes: {},
            StoreID: null,
            ProductInventoryLocationSectionID: null,
            UnitCorePrice: null,
            ExtendedPrice: null,
            ItemType: api.ItemType.Item
        };
        // Properties
        loadExtended = false; // Can possibly configure later
        salesItems = [];
        productIds = [];
        quoteItems = [];
        quoteFiles = [];
        // Functions
        getItems(): any[] {
            return [...this.quoteItems];
        }
        observeItems(callback: (...args: any) => any): () => void {
            if (angular.isFunction(callback)) {
                const newLen = this.observerCallbacks.push(callback);
                callback([...this.quoteItems]);
                return () => { // deregister
                    this.observerCallbacks.splice(newLen - 1, 1);
                };
            }
            return () => { };
        }
        addItem(id: number, quantity: number = 1, product?: any): ng.IPromise<any> {
            return this.$q((resolve, reject) => {
                if (!id) {
                    reject("No ID was supplied.");
                    return;
                }
                const params = angular.extend({ }, this.quoteItemBase);
                if (angular.isObject(product)) {
                    angular.extend(params, product);
                }
                resolve(this.cvApi.shopping.AddCartItem(params).then(this.fetchItems));
            });
        }
        addItemDetailed(itemParams: any): ng.IPromise<any> {
            return this.$q((resolve, reject) => {
                if (!angular.isObject(itemParams) || !itemParams.ProductID && !itemParams.ID) {
                    reject("No Object or ID was supplied.");
                    return;
                }
                if (!itemParams.ProductID) {
                    itemParams.ProductID = itemParams.ID;
                }
                this.quoteItemBase.ProductID = itemParams.ProductID;
                resolve(this.cvApi.shopping.AddCartItem(angular.extend({ }, this.quoteItemBase, itemParams))
                    .then(this.fetchItems));
            });
        }
        addFiles(filesArr: any[]): void {
            if (angular.isArray(filesArr)) {
                this.quoteFiles = this.quoteFiles.concat(filesArr);
            }
        }
        baseQuoteItem(): any {
            return angular.extend({}, this.quoteItemBase);
        }
        removeItem(id: number): ng.IPromise<any> {
            return this.$q((resolve, reject) => {
                if (!id) {
                    reject("No ID was supplied.");
                    return;
                }
                this.cvApi.shopping.RemoveCartItemByProductIDAndType({
                    TypeName: this.cartTypeName,
                    ProductID: id
                }).then(r => {
                    if (r && r.data) {
                        this.fetchItems();
                    }
                    resolve(true);
                });
            });
        }
        addToCart(id: number, quantity: number = 1, product?: any): ng.IPromise<any> {
            return this.$q((resolve, reject) => {
                if (!id) {
                    reject("No ID was supplied.");
                    return;
                }
                const params = <api.AddCartItemDto>{
                    ProductID: id,
                    Quantity: quantity,
                    TypeName: this.cvServiceStrings.carts.types.cart,
                    SerializableAttributes: null,
                    ProductInventoryLocationSectionID: null,
                    UserID: null,
                    AccountID: null,
                    StoreID: null,
                };
                if (angular.isObject(product)) {
                    angular.extend(params, product);
                }
                resolve(this.cvApi.shopping.AddCartItem(params));
            });
        }
        checkout(props: any): ng.IPromise<any> {
            const requiredProps = {
                IsNewAccount: false,
                IsPartialPayment: false,
                IsSameAsBilling: true,
                TestMode: false
            };
            return this.$q.reject("NotYetImplemented");
            /*
            return this.cvApi.quoting.SalesQuoteCheckout(
                angular.extend(
                    {},
                    requiredProps,
                    { StoredFiles: this.quoteFiles.map(file => file.Name) },
                    props));
            */
        }
        convertCartToQuote(): ng.IPromise<any> {
            return this.cvCartService.loadCart(this.cvServiceStrings.carts.types.cart, false, "QuoteService.convertCartToQuote").then(r => {
                const cart = r.Result;
                if (cart == null) { return null; }
                return this.$q.all(cart.SalesItems
                        .map(item => this.addItem(
                            item.ID,
                            item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0)))).then(() => {
                    this.updateLocalItems(cart.SalesItems);
                    return true;
                });
            });
        }
        doCallbacks(): void {
            this.observerCallbacks.forEach((callback: (...args: any[]) => any) => callback([...this.quoteItems]));
        }
        setLoadExtended(): void {
            this.loadExtended = true;
            this.loadCartExtended();
        }
        updateLocalItems(itemList: Array<any>): void {
            if (!angular.isArray(itemList)) {
                return;
            }
            this.salesItems = itemList;
            this.productIds = this.salesItems.map(item => item.ID);
            if (this.loadExtended) {
                this.loadCartExtended();
            } else {
                this.quoteItems = this.salesItems;
                this.doCallbacks();
            }
        }
        fetchItems(): ng.IPromise<any> {
            return this.loadExtended ? this.loadCartExtended() : this.loadCart();
        }
        clearItems(): ng.IPromise<any> {
            //return cvApi.shopping.ClearCurrentCart({ TypeName: cartTypeName }).then(fetchItems); // No-worky!
            return this.$q.all(this.productIds.map(id => this.removeItem(id)));
        }
        loadCartExtended(): ng.IPromise<any> {
            return this.$q.all(this.productIds.map(id => this.cvApi.products.GetProductByID(id)))
                .then((ra: ng.IHttpPromiseCallbackArg<api.ProductModel>[]) => {
                    this.quoteItems = ra.map(r => r.data);
                    this.doCallbacks();
                });
        }
        loadCart(): ng.IPromise<any> {
            return this.cvCartService.loadCart(this.cartTypeName, false, "QuoteService.loadCart").then(response => {
                const cart = response.Result;
                if (!cart || !cart.SalesItems) {
                    return;
                }
                this.updateLocalItems(cart.SalesItems.map(item => item["Product"]));
                this.doCallbacks();
            });
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvCartService: services.ICartService) {
            this.loadCart().then(() => {
                if (this.loadExtended) { this.loadCartExtended(); }
            });
        }
    }
}
