/**
 * @file framework/store/cart/modals/removeFromCartModal.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Remove from Cart modal (shared to any cart type when removing an item)
 */
module cef.store.cart.modals {
    export class RemoveFromCartModal extends core.TemplatedControllerBase {
        // Properties
        // <None>
        // Functions
        ok(): void {
            this.$uibModalInstance.close(true);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly itemName: string, // Used by UI
                private readonly type: string) { // Used by UI
            super(cefConfig);
        }
    }
}
