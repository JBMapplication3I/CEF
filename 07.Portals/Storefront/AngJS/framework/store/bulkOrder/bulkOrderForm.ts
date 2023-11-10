module cef.store.bulkOrder {
    export class BulkOrderFormController extends core.TemplatedControllerBase {
    }

    export class ChildProducts extends core.TemplatedControllerBase {
    }

    export class CategoryTable extends core.TemplatedControllerBase {
    }

    cefApp.directive("cefBulkOrderForm", (
            $filter: ng.IFilterService,
            $location: ng.ILocationService,
            $anchorScroll: ng.IAnchorScrollService,
            cvApi: api.ICEFAPI,
            cefConfig: core.CefConfig,
            cvServiceStrings: services.IServiceStrings)
            : ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/bulkOrder/bulkOrderForm.html", "ui"),
        controller: BulkOrderFormController,
        controllerAs: "bof",
        link: ($scope: any) => {
            // Info about what the user has selected, and the subtotal
            $scope.tempCart = {};
            $scope.formSubtotal = 0;
            // Calculated subtotal on each quantity change
            $scope.getSubtotal = () => {
                $scope.formSubtotal = $scope.formSubtotal = Object.keys($scope.tempCart)
                    .reduce((total, id) => (total + $scope.tempCart[id].Quantity * $scope.tempCart[id].Price), 0);
            }; // Featured Products category should not be shown
            $scope.filterCategories = () => category => (category.CategoryName !== "Featured Products");
            $scope.confirmation = false;
            // Request to add all selected items to cart
            $scope.addItemsToCart = () => {
                const addedItems = Object.keys($scope.tempCart).map(item => {
                    const requestItem = angular.extend({}, $scope.tempCart[item]);
                    delete requestItem.Price;
                    return requestItem;
                });
                cvApi.shopping.AddCartItems({ Items: addedItems, TypeName: cvServiceStrings.carts.types.cart })
                    .then(() => $filter("goToCORSLink")(cefConfig.routes.cart.root));
            };
            $(document).ready(() => {
                $(window).keydown(event => {
                    if (event.keyCode === 13) {
                        event.preventDefault();
                        return false;
                    }
                    return true;
                });
            });
            $scope.append = () => {
                $("#window").kendoWindow();
                const dialog = $("#window").data("kendoWindow");
                dialog.center();
            };
            // Used to scroll to appropriate section
            $scope.jumpTo = selected => {
                $location.hash(selected);
                $anchorScroll.yOffset = 75;
                $anchorScroll($scope.selected);
            };
            // Gets product info
            // TODO: Rework this to use a regular product search
            cvApi.products.GetProductsByCategory({ ProductTypeIDs: [2,6] }).then(response => {
                $scope["productData"] = response.data["ProductsByCategory"];
                //$scope["productData"].sort((a, b) => (a.CategoryName < b.CategoryName ? -1 : a.CategoryName > b.CategoryName ? 1 : 0));
            });
        }
    }))
    .directive("categoryTable", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            category: "=",
            tempCart: "=",
            formSubtotal: "=",
            getSubtotal: "=",
            jumpTo: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/bulkOrder/categoryTable.html", "ui"),
        controller: CategoryTable,
        controllerAs: "ct",
        link: ($scope: any, $element, $attributes: ng.IAttributes, controller: CategoryTable) => {
            $scope.loaded = false;
            // Settings for Kendo grid columns common to all categories
            $scope.baseColumns = [
                { field: "CustomKey", title: "SKU" }, //width: '8%' },
                { field: "Name", title: "Name", width: "16%",
                  template: `<a ui-sref-plus uisrp-is-product="true" uirsp-path="{{dataItem.SeoUrl}}" target="_blank" ng-bind="dataItem.Name"></a>` },
                { field: "PriceBase", title: "Cost", template: "<span>{{dataItem.readPrices().base | currency }}</span>" }
            ];
            $scope.baseColumnNames = {
                "CustomKey": "Key",
                "Name": "Name",
                "PriceBase": "Price"
            };
            // Sorts categories alphabetically, processes products within each category, and created appropriate kendo columns
            $scope["processCategory"] = () => {
                $scope.category.kendoColumns = $scope.baseColumns.slice();
                $scope.category.columnNames = angular.extend({}, $scope.basesColumnNames);
                $scope.category.Products.sort((a, b) => (a.Name < b.Name ? -1 : a.Name > b.Name ? 1 : 0));
                for (let i = 0; i < $scope.category.Products.length; i++) {
                    $scope.category.Products[i] = $scope.processProduct($scope.category.Products[i]);
                }
            };
            // Cuts skew up to {, adds a 'variety number'
            $scope.editSkew = (sku, children) => {
                const index = sku.indexOf("{");
                if (index !== -1) {
                    sku = sku.substring(0, index);
                }
                if (children.length > 1) {
                    const newSku = sku + children[0].CustomKey + " - " + sku + children[children.length - 1].CustomKey;
                    sku = newSku;
                    //for (let x = 0; x < children.length; ++x) {
                    //    const child = children[x];
                    //    if (newSku !== "") {
                    //        newSku += "-";
                    //    }
                    //    newSku += sku+child.CustomKey;
                    //}
                    //sku = newSku;
                }
                //if (number > 0) {
                //    skew = skew + "0-" + skew + number;
                //}
                return sku;
            };
            // Helper function to create kendo grid columns
            $scope.addColumn = (field, title) => {
                $scope.category.kendoColumns.push({
                    field: field,
                    title: title,
                    template: $scope.buildColumnTemplate(field)
                });
                $scope.category.columnNames[field] = field;
            };
            $scope.sortPriority = {
                CustomKey: 1,
                Name: 2,
                PriceBase: 3,
                FieldNameOneSize: 4,
                FieldNameSmall: 5,
                FieldNameMedium: 6,
                FieldNameLarge: 7,
                FieldNameXLarge: 8,
                FieldNameXXLarge: 9,
                FieldNameSM: 10,
                FieledNameLXL: 11
            };
            // Edits skew, de-nests child products, makes field name alphanumeric only, adds columns for child products
            $scope["processProduct"] = product => {
                product.CustomKey = $scope.editSkew(product.CustomKey, product.Children);
                for (let i = 0; i < product.Children.length; i++) {
                    const kendoName = ("FieldName" + product.Children[i].Name).replace(/[^a-zA-Z0-9]/g, "");
                    product[kendoName] = product.Children[i];
                    if (!$scope.category.columnNames[kendoName]) {
                        $scope.addColumn(kendoName, product.Children[i].Name);
                    }
                }
                // Make one-size column if no child products exist
                if (product.Children.length === 0 || !product.Children) {
                    product["FieldNameOneSize"] = {
                        ID: product.ID,
                        CustomKey: product.Customkey,
                        Name: "One Size",
                        PriceBase: 0
                    };
                    if (!$scope.category.columnNames["FieldNameOneSize"]) {
                        $scope.addColumn("FieldNameOneSize", "One Size");
                    }
                }
                return product;
            };
            // Used to Generate unique ng-model names
            $scope.modelNumber = 1;
            // Builds column template: shows quantity box only if data exists, has ng-change function, shows price if different from base-price
            $scope["buildColumnTemplate"] = name => {
                const uniqueNgModel = "model" + $scope.model + name;
                $scope.model++;
                return `<span ng-show="dataItem.${name}">`
                    + `<input type="number" min="0" style="width:30px"`
                    + ` ng-keydown="$scope.preventSubmit($event)" ng-model="${uniqueNgModel}"`
                    + ` ng-change="updateTempCart({{dataItem.${name}}}, ${uniqueNgModel}, {{dataItem.${name}.readPrices().base + dataItem.readPrices().base}})" />`
                    + `<span ng-show="dataItem.${name}.readPrices().base"`
                    + `  >&nbspx&nbsp;{{dataItem.${name}.readPrices().base + dataItem.readPrices().base | globalizedCurrency}}</span>`
                    + "</span>";
            };
            // Process categories at template start
            $scope.processCategory();
            // Allows kendo-grid data binding
            $scope["gridOptions"] = {
                dataSource: { data: $scope.category.Products },
                columns: $scope.category.kendoColumns.sort((a, b) => {
                    if ($scope.sortPriority[a.field] && $scope.sortPriority[b.field]) {
                        if ($scope.sortPriority[a.field] < $scope.sortPriority[b.field]) { return -1; }
                        if ($scope.sortPriority[a.field] > $scope.sortPriority[b.field]) { return 1; }
                    } else if ($scope.sortPriority[a.field]) {
                        return -1;
                    } else if ($scope.sortPriority[b.field]) {
                        return 1;
                    }
                    return 0;
                }),
                dataBound() {
                    $scope.loaded = true;
                }
            };
            // Tracks items/quantities selected on form
            $scope.updateTempCart = (dataItem, qty, price) => {
                //consoleLog("dataItem", dataItem);
                if (!dataItem.ParentID) {
                    $scope.tempCart[dataItem.ID] = { ProductID: dataItem.ID, VariantProductIDs: [], Quantity: qty, Price: price };
                } else {
                    $scope.tempCart[dataItem.ID] = { ProductID: dataItem.ParentID, VariantProductIDs: [dataItem.ID], Quantity: qty, Price: price };
                }
                $scope.formSubtotal = $scope.getSubtotal();
            };
        }
    }));
}
