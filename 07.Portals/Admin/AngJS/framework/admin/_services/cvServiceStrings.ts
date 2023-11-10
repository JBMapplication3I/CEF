/**
 * @file framework/admin/_services/cvServiceStrings.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Service Strings constant, provides un-changing strings for common use
 */
module cef.admin.services {
    /**
     * @desc Constant strings which must be used throughout the system
     * Note: All properties are required to be present
     * @export
     * @interface IServiceStrings
     * @example
     * $scope.$on(this.cvServiceStrings.events.stores.selectionUpdate, ...);
     */
    export interface IServiceStrings {
        attributes: {
            shipOption: string;
            selectedStoreID: string;
            shipToHome: string;
            inStorePickup: string;
            shipToStore: string;
        };
        auth: {
            providers: {
                identity: string;
                openIdConnect: string;
                dnnSSO: string;
                cobalt: string;
                logout: string;
            };
            errors: {
                unableToDetectDNNLogin: string;
            };
        };
        carts: {
            types: {
                cart: string;
                quote: string;
                samples: string;
                compare: string;
                notifyMe: string;
                wishList: string;
                favorites: string;
            };
            targetGroupingPrefix: string;
            kinds: {
                session: string;
                compare: string;
                static: string;
            };
            props: {
                hasASelectedRateQuote: string;
            };
        };
        checkout: {
            paymentMethods: {
                ach: string;
                creditCard: string;
                echeck: string;
                free: string;
                invoice: string;
                payoneer: string;
                payPal: string;
                quoteMe: string;
                storeCredit: string;
                wireTransfer: string;
            };
            steps: {
                billing: string;
                completed: string;
                confirmation: string;
                method: string;
                payment: string;
                shipping: string;
                splitShipping: string;
            };
        };
        events: {
            $scope: {
                $destroy: string;
            };
            $state: {
                notFound: string;
                changeError: string;
                locationChangeStart: string;
                changeStart: string;
                changeSuccess: string;
            };
            account: {
                loaded: string;
                updated: string;
                refreshRoles: string;
            };
            addressBook: {
                added: string;
                deleteConfirmed: string;
                deleteCancelled: string;
                editSave: string;
                editCancelled: string;
                loaded: string;
                reset: string;
                resetRegionDropdownNeeded: string;
            };
            associator: {
                added: string;
                removed: string;
            };
            attributes: {
                ready: string;
                changed: string;
            };
            auth: {
                signIn: string;
                signInFailed: string;
                signOut: string;
            };
            catalog: {
                change: string;
                categoryTreeReady: string;
                searchComplete: string;
            };
            carts: {
                updated: string;
                itemAdded: string;
                itemsAdded: string;
                itemRemoved: string;
                cleared: string;
                loaded: string;
                resetTargets: string;
                updateShoppingLists: string;
            };
            chat: {
                newMessage: string;
                history: string;
            };
            checkout: {
                nextStep: string;
            };
            contacts: {
                propertyChanged: string;
            };
            currency: {
                changed: string;
                changeFinished: string;
            };
            grid: {
                built: string;
            }
            invoices: {
                paymentMade: string;
            };
            lang: {
                changed: string;
                changeFinished: string;
            };
            lineItems: {
                updated: string;
            };
            location: {
                updated: string;
            };
            menus: {
                deactivateChildren: string;
                deactivateGroups: string;
                hoverToggleOff: string;
            };
            modals: {
                close: string;
            };
            orders: {
                complete: string;
            };
            paging: {
                searchComplete: string;
            };
            products: {
                detailProductLoaded: string;
                recordVersionCreated: string;
                refreshReviews: string;
                selectedVariantChanged: string;
                variantInfoLoaded: string;
            };
            sales: {
                totalsUpdated: string;
            };
            shipping: {
                loaded: string;
                rateQuoteSelected: string;
                ready: string;
                revalidateStep: string;
                setSelectedRateQuoteID: string;
                unready: string;
            };
            states: {
                loaded: string;
            };
            statuses: {
                loaded: string;
            };
            stores: {
                selectionUpdate: string;
                nearbyUpdate: string;
                cleared: string;
                ready: string;
            };
            types: {
                loaded: string;
            };
            users: {
                importedToAccount: string;
                updated: string;
                refreshRoles: string;
            };
            wallet: {
                addCancelled: string;
                loaded: string;
                updated: string;
                added: string;
                deleted: string;
                editSave: string;
            };
        };
        modalSizes: {
            lg: string;
            md: string;
            sm: string;
        };
        registration: {
            steps: {
                addressBook: string;
                basicInfo: string;
                completed: string;
                confirmation: string;
                custom: string;
                wallet: string;
            };
        };
    }

    export const serviceStrings: IServiceStrings = <IServiceStrings>{
        attributes: {
            shipOption: "ShipOption",
            selectedStoreID: "SelectedStoreID",
            shipToHome: "ShipToHome",
            inStorePickup: "InStorePickup",
            shipToStore: "ShipToStore"
        },
        auth: {
            providers: {
                identity: "identity",
                openIdConnect: "openId",
                dnnSSO: "dnnSSO",
                cobalt: "cobalt",
                logout: "logout"
            },
            errors: {
                unableToDetectDNNLogin: "Unable to detect DNN Login"
            }
        },
        carts: {
            types: {
                cart: "Cart",
                quote: "Quote Cart",
                samples: "Samples Cart",
                compare: "Compare Cart",
                notifyMe: "Notify Me When In Stock",
                wishList: "Wish List",
                favorites: "Favorites List"
            },
            targetGroupingPrefix: "Target-Grouping-",
            kinds: {
                session: "session",
                compare: "compare",
                static: "static"
            },
            props: {
                hasASelectedRateQuote: "hasASelectedRateQuote"
            }
        },
        checkout: {
            paymentMethods: {
                ach: "purchasePaymentMethodACH",
                creditCard: "purchasePaymentMethodCreditCard",
                echeck: "purchasePaymentMethodEcheck",
                free: "purchasePaymentMethodFree",
                invoice: "purchasePaymentMethodInvoice",
                payoneer: "purchasePaymentMethodPayoneer",
                payPal: "purchasePaymentMethodPayPal",
                quoteMe: "purchasePaymentMethodQuoteMe",
                storeCredit: "purchasePaymentMethodStoreCredit",
                wireTransfer: "purchasePaymentMethodWireTransfer"
            },
            steps: {
                billing: "purchaseStepBilling",
                completed: "purchaseStepCompleted",
                confirmation: "purchaseStepConfirmation",
                method: "purchaseStepMethod",
                payment: "purchaseStepPayment",
                shipping: "purchaseStepShipping",
                splitShipping: "purchaseStepSplitShipping"
            }
        },
        events: {
            $scope: {
                $destroy: "$destroy"
            },
            $state: {
                notFound: "$stateNotFound",
                changeError: "$stateChangeError",
                locationChangeStart: "$locationChangeStart",
                changeStart: "$stateChangeStart",
                changeSuccess: "$stateChangeSuccess"
            },
            account: {
                loaded: "accountLoaded",
                updated: "accountUpdated",
                refreshRoles: "refreshAccountRoles"
            },
            addressBook: {
                added: "addressBookAdded",
                deleteConfirmed: "addressBookDeleteConfirmed",
                deleteCancelled: "addressBookDeleteCancel",
                editSave: "addressEditor2Save",
                editCancelled: "addressEditor2Cancel",
                loaded: "addressBookLoaded",
                reset: "addressBookReset",
                resetRegionDropdownNeeded: "addressBookRsetRegionDropdownNeeded"
            },
            associator: {
                added: "associatorAdded",
                removed: "associatorRemoved"
            },
            attributes: {
                ready: "attributesReady",
                changed: "jsonAttributesChanged"
            },
            auth: {
                signIn: "signIn",
                signInFailed: "signInFailed",
                signOut: "logoutSuccess"
            },
            catalog: {
                change: "searchCatalogChange",
                categoryTreeReady: "categoryTreeReady",
                searchComplete: "searchCatalogProductsSearchComplete"
            },
            carts: {
                updated: "updateCart",
                itemAdded: "cartItemAdded",
                itemsAdded: "cartItemsAdded",
                itemRemoved: "cartItemRemoved",
                cleared: "cartCleared",
                loaded: "cartLoaded",
                resetTargets: "cartResetTargets",
                updateShoppingLists: "updateShoppingLists"
            },
            chat: {
                newMessage: "newConversationMessage",
                history: "populateConversationHistory",
            },
            checkout: {
                nextStep: "purchaseNextStep"
            },
            contacts: {
                propertyChanged: "contactPropertyChanged"
            },
            currency: {
                changed: "changeCurrency"
            },
            grid: {
                built: "cvGridBuilt"
            },
            invoices: {
                paymentMade: "invoicePaymentOccurred"
            },
            lang: {
                changed: "changeLanguage",
                changeFinished: "changeLanguageFinished"
            },
            lineItems: {
                updated: "lineItemsUpdated"
            },
            location: {
                updated: "mapUserLocationUpdate"
            },
            menus: {
                deactivateChildren: "childMenusDeactivate",
                deactivateGroups: "groupMenusDeactivate",
                hoverToggleOff: "menuHoverToggleOffEvent"
            },
            modals: {
                close: "closeModal"
            },
            orders: {
                complete: "orderCheckoutComplete"
            },
            paging: {
                searchComplete: "pagingSearchComplete"
            },
            products: {
                detailProductLoaded: "productDetailProductLoaded",
                recordVersionCreated: "productRecordVersionCreated",
                refreshReviews: "refreshReviews",
                selectedVariantChanged: "productDetailSelectedVariantChanged",
                variantInfoLoaded: "variantInfoLoaded"
            },
            sales: {
                totalsUpdated: "totalsUpdated"
            },
            shipping: {
                loaded: "loadShippingRateQuotesComplete",
                rateQuoteSelected: "shippingRateQuoteSelected",
                ready: "readyToLoadShippingRateQuotes",
                revalidateStep: "revalidateShippingStep",
                setSelectedRateQuoteID: "setSelectedRateQuoteID",
                unready: "unreadyToLoadShippingRateQuotes"
            },
            states: {
                loaded: "stateLoaded"
            },
            statuses: {
                loaded: "statusLoaded"
            },
            stores: {
                selectionUpdate: "globalStoreSelectionUpdate",
                nearbyUpdate: "globalNearbyStoresUpdate",
                cleared: "userSelectedStoreCleared",
                ready: "storesReady"
            },
            types: {
                loaded: "typeLoaded"
            },
            users: {
                importedToAccount: "usersImportedToAccount",
                updated: "userUpdated",
                refreshRoles: "refreshUserRoles"
            },
            wallet: {
                addCancelled: "walletAddCancelled",
                loaded: "walletLoaded",
                updated: "walletUpdated",
                added: "walletAdded",
                deleted: "walletDeleted",
                editSave: "walletEditSave"
            }
        },
        modalSizes: {
            lg: "lg",
            md: "md",
            sm: "sm"
        },
        registration: {
            steps: {
                addressBook: "registrationStepAddressBook",
                basicInfo: "registrationStepBasicInfo",
                completed: "registrationStepCompleted",
                confirmation: "registrationStepConfirmation",
                custom: "registrationStepCustom",
                wallet: "registrationStepWallet"
            }
        }
    };
}
