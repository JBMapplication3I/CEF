module cef.store.product.controls.actions {
    export abstract class ActionButtonControllerBase extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        // Properties
        protected abstract cartType: string;
        protected abstract addFunc: Function;
        protected abstract removeFunc: Function;
        protected abstract addKey: string;
        protected abstract removeKey: string;
        protected abstract addIndex: string;
        protected abstract removeIndex: string;
        protected get func(): Function { return this.in() ? this.addFunc : this.removeFunc; }
        protected get key(): string { return this.in() ? this.removeKey : this.addKey; }
        protected get index(): string { return this.in() ? this.removeIndex : this.addIndex; }
        // Functions
        protected in(): boolean {
            if (!this.cartType || !this.product || !this.product.ID) { return false; }
            return this.cvCartService.cartContainsItem(this.product.ID, this.cartType);
        }
        // Events
        protected click(add: boolean): void {
            if (!this.cartType || !this.product || !this.product.ID) { return; }
            if (add) {
                this.cvCartService.addCartItem(this.product.ID, this.cartType);
                return;
            }
            this.cvCartService.removeCartItemByType(this.product.ID, this.cartType);
        }
        protected toggle(): void {
            if (this.in()) {
                this.removeFunc();
                return;
            }
            this.addFunc();
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig, // Used by UI
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCartService: services.ICartService) {
            super(cefConfig);
        }
    }
}