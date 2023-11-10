module cef.store.cart.modals {
    export class AddToCartModalController extends core.TemplatedControllerBase {
        // Properties
        // <None>
        // Functions
        itemsAdded(): number | string {
            if ((<api.AddCartItemsDto>this.dto).Items) {
                return ""; // intentionally empty
                // return _.sumBy((<api.AddCartItemsDto>this.dto).Items,
                //     x => x.Quantity + (x.QuantityBackOrdered || 0) + (x.QuantityPreSold || 0));
            }
            return (<api.AddCartItemDto>this.dto).Quantity;
        }
        // Events
        cancel(): void { this.$uibModalInstance.dismiss("cancel"); }
        ok(): void { this.$uibModalInstance.close(true); }
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvCartService: services.ICartService, // Used by UI
                // Properties, all used by UI
                private readonly url: string,
                private readonly dto: api.AddCartItemDto | api.AddStaticCartItemDto | api.AddCartItemsDto,
                private readonly type: string,
                private readonly cart: api.CartModel,
                private readonly item: any /* api.SalesItemBaseModel */,
                private readonly messages: string[]) {
            super(cefConfig);
        }
    }
}
