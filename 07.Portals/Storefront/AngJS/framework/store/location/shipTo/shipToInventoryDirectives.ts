module cef.store.locations.shipTo {
    cefApp.directive("cefShipToForProductDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            /** boolean [default: false] When ship to home would be otherwise hidden, show it with "Unavailable" text */
            showShipToHomeWhenUnavailable: "=?",
            /** boolean [default: false] When ship to home would be otherwise hidden due to shipping restrictions, show it with "Restricted" text */
            showShipToHomeWhenRestricted: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showShipToHomeStock: "=?",
            /** boolean [default: false] When pickup in store would be otherwise hidden, show it with "Unavailable" text */
            showInStorePickupWhenUnavailable: "=?",
            showInStorePickupAsChooseStoreLinkWhenNoStore: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showInStorePickupStock: "=?",
            /** boolean [default: false] When ship to store would be otherwise hidden, show it with "Unavailable" text */
            showShipToStoreWhenUnavailable: "=?",
            /** boolean [default: false] When In Store Pickup is available, hide Ship to Store */
            showShipToStoreWhenInStorePickupIsAvailable: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showShipToStoreStock: "=?",
            /** boolean [default: false] When set to true, phone numbers are displayed as labels instead of 'tel:' links */
            phonesAreNotLinks: "=?",
            /** boolean [default: false] When set to true, reduces the margin between radio buttons (for smaller cards) */
            condenseRadios: "=?",
            /** boolean [default: false] When set to true, an <hr> will be visible between the Model/SKU block and the radio buttons */
            separateSkuFromRadios: "=?",
            /** boolean [default: false] When set to true, includes a Model/SKU block of UI (information labels) */
            includeModelSkuBlock: "=?",
            /** boolean [default: false] When set to true, includes an Address block of UI (ship to Address or store location) */
            includeAddressBlock: "=?",
            /** boolean [default: false] When set to true, includes a Buy block of UI (Add to cart buttons) */
            includeBuyBlock: "=?",
            /** The callback function */
            callback: "&?",
            /** string [default: determined by stock availability] The current ship to option/value */
            shipToValue: "=",
            /** ProductModel [no default] The core product before analysis
             * e.g.- In the single product scenario, just the product
             * In the three-way split scenario, the variant master
             */
            product: "=",
            /** A unique identifier to add to the radio button name group to prevent overlaps */
            unique: "@?",
            editable: "=?",
            debug: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/location/shipTo/shipToForProductDetail.html", "ui"),
        controller: ShipToInventoryController,
        controllerAs: "cefShipToCtrl",
        bindToController: true
    }));

    cefApp.directive("cefShipToForCatalogGrid", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            /** boolean [default: false] When ship to home would be otherwise hidden, show it with "Unavailable" text */
            showShipToHomeWhenUnavailable: "=?",
            /** boolean [default: false] When ship to home would be otherwise hidden due to shipping restrictions, show it with "Restricted" text */
            showShipToHomeWhenRestricted: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showShipToHomeStock: "=?",
            /** boolean [default: false] When pickup in store would be otherwise hidden, show it with "Unavailable" text */
            showInStorePickupWhenUnavailable: "=?",
            showInStorePickupAsChooseStoreLinkWhenNoStore: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showInStorePickupStock: "=?",
            /** boolean [default: false] When ship to store would be otherwise hidden, show it with "Unavailable" text */
            showShipToStoreWhenUnavailable: "=?",
            /** boolean [default: false] When In Store Pickup is available, hide Ship to Store */
            showShipToStoreWhenInStorePickupIsAvailable: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showShipToStoreStock: "=?",
            /** boolean [default: false] When set to true, reduces the margin between radio buttons (for smaller cards) */
            condenseRadios: "=?",
            /** boolean [default: false] When set to true, an <hr> will be visible between the Model/SKU block and the radio buttons */
            separateSkuFromRadios: "=?",
            /** boolean [default: false] When set to true, includes a Name block of UI (link to product details) */
            includeNameLinkBlock: "=?",
            /** boolean [default: false] When set to true, includes a Model/SKU block of UI (information labels) */
            includeModelSkuBlock: "=?",
            /** boolean [default: false] When set to true, includes an Address block of UI (ship to Address or store location) */
            includeAddressBlock: "=?",
            /** boolean [default: false] When set to true, includes a Buy block of UI (Add to cart buttons) */
            includeBuyBlock: "=?",
            /** The callback function */
            callback: "&?",
            /** string [default: determined by stock availability] The current ship to option/value */
            shipToValue: "=",
            /** ProductModel [no default] The core product before analysis
             * e.g.- In the single product scenario, just the product
             * In the three-way split scenario, the variant master
             */
            product: "=",
            /** A unique identifier to add to the radio button name group to prevent overlaps */
            unique: "@?",
            /** number [default: 50] The maximum number of characters allowed to display for the name */
            nameLimit: "@?",
            editable: "=?",
            debug: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/location/shipTo/shipToForCatalogGrid.html", "ui"),
        controller: ShipToInventoryController,
        controllerAs: "cefShipToCtrl",
        bindToController: true
    }));

    cefApp.directive("cefShipToForCatalogList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            /** boolean [default: false] When ship to home would be otherwise hidden, show it with "Unavailable" text */
            showShipToHomeWhenUnavailable: "=?",
            /** boolean [default: false] When ship to home would be otherwise hidden due to shipping restrictions, show it with "Restricted" text */
            showShipToHomeWhenRestricted: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showShipToHomeStock: "=?",
            /** boolean [default: false] When pickup in store would be otherwise hidden, show it with "Unavailable" text */
            showInStorePickupWhenUnavailable: "=?",
            showInStorePickupAsChooseStoreLinkWhenNoStore: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showInStorePickupStock: "=?",
            /** boolean [default: false] When ship to store would be otherwise hidden, show it with "Unavailable" text */
            showShipToStoreWhenUnavailable: "=?",
            /** boolean [default: false] When In Store Pickup is available, hide Ship to Store */
            showShipToStoreWhenInStorePickupIsAvailable: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showShipToStoreStock: "=?",
            /** boolean [default: false] When set to true, reduces the margin between radio buttons (for smaller cards) */
            condenseRadios: "=?",
            /** boolean [default: false] When set to true, an <hr> will be visible between the Model/SKU block and the radio buttons */
            separateSkuFromRadios: "=?",
            /** boolean [default: false] When set to true, includes a Name block of UI (link to product details) */
            includeNameLinkBlock: "=?",
            /** boolean [default: false] When set to true, includes a Model/SKU block of UI (information labels) */
            includeModelSkuBlock: "=?",
            /** boolean [default: false] When set to true, includes an Address block of UI (ship to Address or store location) */
            includeAddressBlock: "=?",
            /** boolean [default: false] When set to true, includes a Buy block of UI (Add to cart buttons) */
            includeBuyBlock: "=?",
            /** The callback function */
            callback: "&?",
            /** string [default: determined by stock availability] The current ship to option/value */
            shipToValue: "=",
            /** ProductModel [no default] The core product before analysis
             * e.g.- In the single product scenario, just the product
             * In the three-way split scenario, the variant master
             */
            product: "=",
            /** A unique identifier to add to the radio button name group to prevent overlaps */
            unique: "@?",
            /** number [default: 50] The maximum number of characters allowed to display for the name */
            nameLimit: "@?",
            editable: "=?",
            debug: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/location/shipTo/shipToForCatalogList.html", "ui"),
        controller: ShipToInventoryController,
        controllerAs: "cefShipToCtrl",
        bindToController: true
    }));

    // These are sub-blocks for the Grid and List views. All the real logic is in them and not repeated
    // The only different between grid and list are where these blocks sit (vertically vs horizontally)
    cefApp.directive("cefShipToForCatalogNameLinkBlock", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        require: "cefShipToCtrl",
        templateUrl: $filter("corsLink")("/framework/store/location/shipTo/shipToForCatalogNameLinkBlock.html", "ui")
    }));

    cefApp.directive("cefShipToForCatalogModelSkuBlock", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        require: "cefShipToCtrl",
        templateUrl: $filter("corsLink")("/framework/store/location/shipTo/shipToForCatalogModelSkuBlock.html", "ui")
    }));

    cefApp.directive("cefShipToForCatalogBuyBlock", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        require: "cefShipToCtrl",
        templateUrl: $filter("corsLink")("/framework/store/location/shipTo/shipToForCatalogBuyBlock.html", "ui")
    }));

    cefApp.directive("cefShipToForCatalogRadiosBlock", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        require: "cefShipToCtrl",
        templateUrl: $filter("corsLink")("/framework/store/location/shipTo/shipToForCatalogRadiosBlock.html", "ui")
    }));

    cefApp.directive("cefShipToForCartItem", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            /** boolean [default: false] When ship to home would be otherwise hidden, show it with "Unavailable" text */
            showShipToHomeWhenUnavailable: "=?",
            /** boolean [default: false] When ship to home would be otherwise hidden due to shipping restrictions, show it with "Restricted" text */
            showShipToHomeWhenRestricted: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showShipToHomeStock: "=?",
            /** boolean [default: false] When pickup in store would be otherwise hidden, show it with "Unavailable" text */
            showInStorePickupWhenUnavailable: "=?",
            showInStorePickupAsChooseStoreLinkWhenNoStore: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showInStorePickupStock: "=?",
            /** boolean [default: false] When ship to store would be otherwise hidden, show it with "Unavailable" text */
            showShipToStoreWhenUnavailable: "=?",
            /** boolean [default: false] When In Store Pickup is available, hide Ship to Store */
            showShipToStoreWhenInStorePickupIsAvailable: "=?",
            /** boolean [default: false] Display the stock quantity itself */
            showShipToStoreStock: "=?",
            /** boolean [default: false] When set to true, phone numbers are displayed as labels instead of 'tel:' links */
            phonesAreNotLinks: "=?",
            /** boolean [default: false] When set to true, reduces the margin between radio buttons (for smaller cards) */
            condenseRadios: "=?",
            /** boolean [default: false] When set to true, an <hr> will be visible between the Model/SKU block and the radio buttons */
            separateSkuFromRadios: "=?",
            /** boolean [default: false] When set to true, includes a Model/SKU block of UI (information labels) */
            includeModelSkuBlock: "=?",
            /** boolean [default: false] When set to true, includes an Address block of UI (ship to Address or store location) */
            includeAddressBlock: "=?",
            /** boolean [default: false] When set to true, includes a Buy block of UI (Add to cart buttons) */
            includeBuyBlock: "=?",
            /** The callback function */
            callback: "&?",
            /** string [default: determined by stock availability] The current ship to option/value */
            shipToValue: "=",
            /** ProductModel [no default] The core product before analysis
             * e.g.- In the single product scenario, just the product
             * In the three-way split scenario, the variant master
             */
            product: "=",
            /** A unique identifier to add to the radio button name group to prevent overlaps */
            unique: "@?",
            editable: "=?",
            debug: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/location/shipTo/shipToForCartItem.html", "ui"),
        controller: ShipToInventoryController,
        controllerAs: "cefShipToCtrl",
        bindToController: true
    }));

    // Deprecated
    cefApp.directive("cefShipToCartSelectAll", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { cartItems: "=", },
        templateUrl: $filter("corsLink")("/framework/store/location/shipTo/shipToCartSelectAll.html", "ui"),
        controller($scope) {
            this.updateAll = (shipSelection: string): void =>
                $scope.cartItems.forEach((cartItem: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>) => {
                    if (!cartItem.SerializableAttributes) {
                        cartItem.SerializableAttributes = {};
                    }
                    cartItem.SerializableAttributes[this.cvServiceStrings.attributes.shipOption].Value = shipSelection;
                });
        },
        controllerAs: "cefShipToCartSelectAllCtrl",
        bindToController: true
    }));
}
