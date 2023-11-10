module cef.store.cart.controls {
    interface IMessageWithClass {
        message: string;
        cssclass: string
    }

    class CartValidatorController {
        // Bound Scope Properties
        type: string;
        shift: boolean;
        // Properties
        errorMessages: IMessageWithClass[] = [];
        validation: api.CEFActionResponseT<api.CartModel>;
        updating: boolean;
        // Functions
        addBufferSkuToCart(seoUrl: string, missingAmount: string): void {
            var dto = <api.AddBufferSkuCartItemDto>{ BufferSkuSEOURL: seoUrl, AmountToFill: missingAmount, TypeName: this.type };
            this.cvApi.shopping.AddBufferSkuCartItem(dto).finally(
                () => this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, this.cvServiceStrings.carts.types.cart));
        }
        addBufferFeeToCart(missingAmount: string): void {
            this.cvApi.shopping.CurrentCartAddBufferFee({
                AmountToFee: missingAmount,
                TypeName: this.type
            }).finally(() => this.$rootScope.$broadcast(
                this.cvServiceStrings.events.carts.updated,
                this.cvServiceStrings.carts.types.cart));
        }
        resolveMessage(item: string): ng.IPromise<IMessageWithClass[]> {
            return this.$q((resolve, __) => {
                switch ((item.substring(0, item.indexOf("!")).toLowerCase()) || "") {
                    case "success": {
                        resolve([{ message: item, cssclass: "alert-success" }]);
                        break;
                    }
                    case "warning": {
                        if (Boolean(this.shift)) {
                            resolve([{ message: item.replace("WARNING", "ACCEPTED"), cssclass: "alert-success" }]);
                        } else {
                            resolve([{ message: item, cssclass: "alert-warning" }]);
                        }
                        break;
                    }
                    case "error": {
                        var msgs: IMessageWithClass[] = [];
                        if (item !== "ERROR! There are no active items in this cart."
                            && item !== "ERROR! There is no cart to test") {
                            if (Boolean(this.shift)) {
                                msgs.push({ message: item.replace("ERROR", "WARNING"), cssclass: "alert-warning" });
                            } else {
                                msgs.push({ message: item, cssclass: "alert-danger" });
                            }
                        }
                        if (item === "ERROR! Some products have role restrictions.") {
                            msgs.pop();
                            this.$translate("ui.storefront.cartValidator.RoleRestrictions").then(translated => {
                                msgs.push({ message: translated, cssclass: "alert-danger" });
                                resolve(msgs);
                            });
                            return;
                        }
                        resolve(msgs);
                        break;
                    }
                    default: {
                        resolve([]);
                        break;
                    }
                }
            });
        }
        // NOTE: Must remain an arrow function for the watch function
        validateCart = async (
                newValue: api.CEFActionResponseT<api.CartModel>,
                oldValue: api.CEFActionResponseT<api.CartModel>
            ): Promise<void> => {
                this.validation = newValue;
                if (newValue
                    && (!newValue.ActionSucceeded
                        || newValue.Messages
                           && _.some(newValue.Messages, x => x.indexOf("ERROR!") !== -1))) {
                    if (this.type === "Quote Cart") {
                        this.cvCartService.validForSubmitQuote = false;
                    } else {
                        this.cvCartService.validForCheckout = false;
                    }
                } else {
                    // Anything that isn't invalid will be assumed valid
                    if (this.type === "Quote Cart") {
                        this.cvCartService.validForSubmitQuote = true;
                    } else {
                        this.cvCartService.validForCheckout = true;
                    }
                }
                this.errorMessages = []; // Clear the existing content
                if (!newValue) {
                    return;
                }
                if (!this.updating) {
                    this.updating = true;
                    await this.$timeout(() => {
                        this.$q.all(newValue.Messages.map(x => this.resolveMessage(x)))
                        .then((rarr: IMessageWithClass[]) => {
                            if (!rarr || !angular.isArray(rarr)) {
                                return;
                            }
                            this.errorMessages = rarr;
                            this.updating = false;
                        });
                    }, 250);
                }
        };
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $scope: ng.IScope,
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $timeout: ng.ITimeoutService,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvCartService: services.ICartService) {
            const unbind1 = this.$scope.$watch(() => this.cvCartService.validationResponse, this.validateCart);
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefCartValidator", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            type: "=",
            shift: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/cart/controls/cartValidator.html", "ui"),
        controller: CartValidatorController,
        controllerAs: "cartValidatorCtrl",
        bindToController: true
    }));
}
