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
      [key: string]: string;
      billing?: string;
      completed?: string;
      confirmation?: string;
      method?: string;
      payment?: string;
      shipping?: string;
      splitShipping?: string;
    };
  };
  cvgrid: {
    filters: {
      id: string,
      input: string,
      autocomplete: string,
      attribute: string,
      select: string,
      boolean: string,
      minmaxnumber: string,
      minmaxdate: string,
    }
  };
  events: {
    account: {
      loaded: string;
      updated: string;
      refreshRoles: string;
    };
    addressBook: {
      added: string;
      deleted: string;
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
    catalog: {
      change: string;
      categoryTreeReady: string;
      searchComplete: string;
    };
    chat: {
      newMessage: string;
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
    };
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
      refreshReviews: string;
      selectedVariantChanged: string;
      variantInfoLoaded: string;
    };
    sales: {
      totalsUpdated: string;
    };
    salesQuotes: {
      complete: string;
    };
    shipping: {
      loaded: string;
      rateQuoteSelected: string;
      ready: string;
      revalidateStep: string;
      setSelectedRateQuoteID: string;
      unready: string;
    };
    signalR: {
      sendMaxAutoBidAsync: string;
      receiveMaxAutoBid: string;
      sendQuickBidAsync: string;
      receiveQuickBid: string;
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
      loaded: string;
      updated: string;
    };
  };
  modalSizes: {
    xl: string;
    lg: string;
    md: string;
    sm: string;
  };
  redux: {
    actionTypes: {
      cart: {
        setCart: string;
      };
      cefConfig: {
        setCefConfig: string;
      };
      currentAccount: {
        setCurrentAccount: string;
      };
      currentAccountAddressBook: {
        setCurrentAccountAddressBook: string;
      };
      quoteCart: {
        setQuoteCart: string;
      };
      signalR: {
        setSignalRConnection: string;
      };
      staticCarts: {
        setFavoritesList: string;
        setWishList: string;
        setNotifyMeList: string;
        setProductAlertList: string;
        setWatchList: string;
        setCompareList: string;
        setShoppingLists: string;
      };
      user: {
        logUserIn: string;
        logUserOut: string;
      };
      wallet: {
        setWallet: string;
      };
    }
  }
  registration: {
    steps: {
      [key: string]: string;
      addressBook?: string;
      basicInfo?: string;
      completed?: string;
      confirmation?: string;
      custom?: string;
      wallet?: string;
    };
  };
  submitQuote: {
    steps: {
      method: string;
      shipping: string;
      splitShipping: string;
      confirmation: string;
      completed: string;
    };
  };
  salesQuotes: {
    statuses: {
      submitted: {
        name: string;
        description: string;
      };
      inProcess: {
        name: string;
        description: string;
      };
      processed: {
        name: string;
        description: string;
      };
      approved: {
        name: string;
        description: string;
        confirmTemplate: string;
        actionTitle: string;
        actionTitleAlt: string;
      };
      rejected: {
        name: string;
        description: string;
        confirmTemplate: string;
        actionTitle: string;
      };
      expired: {
        name: string;
        description: string;
      };
      void: {
        name: string;
        description: string;
        confirmTemplate: string;
        actionTitle: string;
      };
    };
  };
}

export const ServiceStrings: IServiceStrings = {
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
  cvgrid: {
    filters: {
      id: "id",
      input: "input",
      autocomplete: "autocomplete",
      attribute: "attribute",
      select: "select",
      boolean: "boolean",
      minmaxnumber: "minmaxnumber",
      minmaxdate: "minmaxdate"
    }
  },
  events: {
    account: {
      loaded: "accountLoaded",
      updated: "accountUpdated",
      refreshRoles: "refreshAccountRoles"
    },
    addressBook: {
      added: "addressBookAddedEntry",
      deleted: "addressBookDeletedEntry",
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
      newMessage: "newConversationMessage"
    },
    checkout: {
      nextStep: "purchaseNextStep"
    },
    contacts: {
      propertyChanged: "contactPropertyChanged"
    },
    currency: {
      changed: "changeCurrency",
      changeFinished: "changeCurrencyFinished"
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
      refreshReviews: "refreshReviews",
      selectedVariantChanged: "productDetailSelectedVariantChanged",
      variantInfoLoaded: "variantInfoLoaded"
    },
    sales: {
      totalsUpdated: "totalsUpdated"
    },
    salesQuotes: {
      complete: "quoteSubmitComplete"
    },
    shipping: {
      loaded: "loadShippingRateQuotesComplete",
      rateQuoteSelected: "shippingRateQuoteSelected",
      ready: "readyToLoadShippingRateQuotes",
      revalidateStep: "revalidateShippingStep",
      setSelectedRateQuoteID: "setSelectedRateQuoteID",
      unready: "unreadyToLoadShippingRateQuotes"
    },
    signalR: {
      sendMaxAutoBidAsync: "SendMaxAutoBidAsync",
      receiveMaxAutoBid: "ReceiveMaxAutoBid",
      sendQuickBidAsync: "SendQuickBidAsync",
      receiveQuickBid: "ReceiveQuickBid"
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
      loaded: "walletLoaded",
      updated: "walletUpdated"
    }
  },
  modalSizes: {
    xl: "xl",
    lg: "lg",
    md: "md",
    sm: "sm"
  },
  redux: {
    actionTypes: {
      cart: {
        setCart: "setCart",
      },
      cefConfig: {
        setCefConfig: "setCefConfig"
      },
      currentAccount: {
        setCurrentAccount: "setCurrentAccount"
      },
      currentAccountAddressBook: {
        setCurrentAccountAddressBook: "setCurrentAccountAddressBook",
      },
      quoteCart: {
        setQuoteCart: "setQuoteCart"
      },
      signalR: {
        setSignalRConnection: "setSignalRConnection",
      },
      staticCarts: {
        setFavoritesList: "string",
        setWishList: "setWishList",
        setNotifyMeList: "setNotifyMeList",
        setProductAlertList: "setProductAlertList",
        setWatchList: "setWatchList",
        setCompareList: "setCompareList",
        setShoppingLists: "setShoppingLists",
      },
      user: {
        logUserIn: "logUserIn",
        logUserOut: "logUserOut"
      },
      wallet: {
        setWallet: "setWallet"
      }
    }
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
  },
  submitQuote: {
    steps: {
      method: "submitQuotePaneMethod",
      shipping: "submitQuotePaneShipping",
      splitShipping: "submitQuotePaneSplitShipping",
      confirmation: "submitQuotePaneConfirmation",
      completed: "submitQuotePaneCompleted"
    }
  },
  salesQuotes: {
    statuses: {
      submitted: {
        name: "ui.storefront.SalesQuoteStatuses.Submitted",
        description: "ui.storefront.SalesQuoteStatuses.Submitted.Description"
      },
      inProcess: {
        name: "ui.storefront.SalesQuoteStatuses.InProcess",
        description: "ui.storefront.SalesQuoteStatuses.InProcess.Description"
      },
      processed: {
        name: "ui.storefront.SalesQuoteStatuses.Processed",
        description: "ui.storefront.SalesQuoteStatuses.Processed.Description"
      },
      approved: {
        name: "ui.storefront.SalesQuoteStatuses.Approved",
        description: "ui.storefront.SalesQuoteStatuses.Approved.Description",
        confirmTemplate:
          "ui.storefront.SalesQuoteStatuses.Approved.Confirm.Template",
        actionTitle: "ui.storefront.SalesQuoteStatuses.Approve.ActionTitle",
        actionTitleAlt:
          "ui.storefront.SalesQuoteStatuses.Approve.ActionTitleAlt" // Approve and Pay
      },
      rejected: {
        name: "ui.storefront.SalesQuoteStatuses.Rejected",
        description: "ui.storefront.SalesQuoteStatuses.Rejected.Description",
        confirmTemplate:
          "ui.storefront.SalesQuoteStatuses.Rejected.Confirm.Template",
        actionTitle: "ui.storefront.SalesQuoteStatuses.Reject.ActionTitle"
      },
      expired: {
        name: "ui.storefront.SalesQuoteStatuses.Expired",
        description: "ui.storefront.SalesQuoteStatuses.Expired.Description"
      },
      void: {
        name: "ui.storefront.SalesQuoteStatuses.Void",
        description: "ui.storefront.SalesQuoteStatuses.Void.Description",
        confirmTemplate:
          "ui.storefront.SalesQuoteStatuses.Void.Confirm.Template",
        actionTitle: "ui.storefront.common.Cancel"
      }
    }
  }
};
