/**
 * @file framework/store/cart/controls/emailCart.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Email cart class
 */
module cef.store.cart.controls {
    class EmailCartController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        type: string;
        // Properties
        email: string;
        successMessage: string;
        // Functions
        shareEmails(): ng.IPromise<any> {
            const emails = this.email.split(";");
            if (!emails.length || !this.type) {
                return this.$q.reject(
                    this.$translate("ui.storefront.cart.widgets.emailCart.Errors.InputOrConfiguration"));
            }
            return this.$q.all(emails.map(email => this.cvApi.shopping.ShareStaticCartItemsByEmail({
                TypeName: this.type,
                Email: email
            })));
        }
        // Events
        onFormSubmit(): void {
            this.setRunning();
            this.shareEmails().then(r => {
                this.successMessage = this.$translate.instant("ui.storefront.cart.widgets.emailCart.EmailSent");
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason.data.ResponseStatus.Message));
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefEmailCart", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { type: "=" }, // The type of the cart e.g.- "Cart"
        templateUrl: $filter("corsLink")("/framework/store/cart/controls/emailCart.html", "ui"),
        controller: EmailCartController,
        controllerAs: "emailCartCtrl",
        bindToController: true
    }));
}
