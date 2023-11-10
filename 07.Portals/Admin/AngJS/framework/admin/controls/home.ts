module cef.admin.controls {
    interface IPanelDef {
        headerKey: string;
        listState: string;
        listKey: string;
        newState?: string;
        newKey?: string;
        footerKey: string;
        keywords?: string[];
    }
    class HomeWidgetController extends core.TemplatedControllerBase {
        // Scope Properties
        // <None>
        // Properties
        panels: IPanelDef[] = [];
        keywordsCloud: string[] = [];
        currentKeyword: string = null;
        // Functions
        filterToKeyword(word: string): void {
            if (this.currentKeyword === word) {
                // Clicking the same word again toggles it off
                this.currentKeyword = null;
                return;
            }
            // Clicking a word when null or a different word changes to this word
            this.currentKeyword = word;
        }
        private load(): void {
            var tempPanels: IPanelDef[] = [];
            if (this.cefConfig.featureSet.salesOrders.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.home.SalesOrderLabel",
                    listState: "sales.salesOrders.list",
                    listKey: "ui.admin.common.Order.Plural",
                    // newState: "sales.salesOrders.create",
                    // newKey: "ui.admin.common.Order.New",
                    footerKey: "ui.admin.home.SalesOrderinstructions",
                    keywords: [null,"Frequent","Sales","Orders","Ordering","Payment","Payments","Inventory"]
                });
            }
            tempPanels.push({
                headerKey: "ui.admin.common.Product.Plural",
                listState: "inventory.products.list",
                listKey: "ui.admin.common.Product.Plural",
                newState: "inventory.products.detail({'ID':'New'})",
                newKey: "ui.admin.views.home.NewProduct",
                footerKey: "ui.admin.home.ProductInstructions",
                keywords: [null,"Frequent","Product","Products","Inventory","Shipping","Catalog","Catalogs","Index","Indexes"]
            });
            if (this.cefConfig.featureSet.categories.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Category.Plural",
                    listState: "inventory.categories.list",
                    listKey: "ui.admin.common.Category.Plural",
                    newState: "inventory.categories.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewCategory",
                    footerKey: "ui.admin.home.CategoryInstructions",
                    keywords: [null,"Frequent","Category","Categories","Inventory"]
                });
            }
            if (this.cefConfig.featureSet.discounts.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Discount.Plural",
                    listState: "accounts.discounts.list",
                    listKey: "ui.admin.common.Discount.Plural",
                    newState: "accounts.discounts.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewDiscount",
                    footerKey: "ui.admin.home.DiscountInstructions",
                    keywords: [null,"Frequent","Discount","Discounts","Account","Accounts","Pricing","Price"]
                });
            }
            tempPanels.push({
                headerKey: "ui.admin.home.AccountsLabel",
                listState: "accounts.accounts.list",
                listKey: "ui.admin.common.Account.Plural",
                newState: "accounts.accounts.detail({'ID':'New'})",
                newKey: "ui.admin.views.home.NewAccount",
                footerKey: "ui.admin.home.AccountsInstructions",
                keywords: [null,"Frequent","Account","Accounts","User","Users","Company","Companies","Organization","Organizations"]
            });
            if (this.cefConfig.featureSet.salesGroups.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.SalesGroup.Plural",
                    listState: "sales.salesGroups.list",
                    listKey: "ui.admin.common.SalesGroup.Plural",
                    footerKey: "ui.admin.home.SalesGroupInstructions",
                    keywords: [null,"Frequent","Sales","Orders","Ordering","Group","Groups","Inventory"]
                });
            }
            if (this.cefConfig.featureSet.salesInvoices.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Invoice.Plural",
                    listState: "sales.salesInvoices.list",
                    listKey: "ui.admin.common.Invoice.Plural",
                    footerKey: "ui.admin.home.SalesInvoiceInstructions",
                    keywords: [null,"Frequent","Sales","Invoice","Invoices","Invoicing","Payment","Payments"]
                });
            }
            if (this.cefConfig.featureSet.salesQuotes.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Quote.Plural",
                    listState: "sales.salesQuotes.list",
                    listKey: "ui.admin.common.Quote.Plural",
                    footerKey: "ui.admin.home.SalesQuoteInstructions",
                    keywords: [null,"Frequent","Sales","Quote","Quotes","Quoting"]
                });
            }
            if (this.cefConfig.featureSet.sampleRequests.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.home.SampleRequestLabel",
                    listState: "sales.sampleRequests.list",
                    listKey: "ui.admin.home.SampleRequests",
                    footerKey: "ui.admin.home.SampleRequestinstructions",
                    keywords: [null,"Sales","Sample","Samples","Sampling"]
                });
            }
            if (this.cefConfig.featureSet.salesReturns.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.home.SalesReturnsLabel",
                    listState: "sales.salesReturns.list",
                    listKey: "ui.admin.home.SalesReturns",
                    newState: "sales.salesReturns.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewSalesReturn",
                    footerKey: "ui.admin.home.SalesReturnInstructions",
                    keywords: [null,"Sales","Return","Returns","Returning","Inventory"]
                });
            }
            if (this.cefConfig.featureSet.purchaseOrders.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.PurchaseOrder.Plural",
                    listState: "sales.purchaseOrders.list",
                    listKey: "ui.admin.common.PurchaseOrder.Plural",
                    footerKey: "ui.admin.home.PurchaseOrderInstructions",
                    keywords: [null,"Sales","Purchase","Purchases","Purchasing","Inventory","Vendor","Vendors"]
                });
            }
            tempPanels.push({
                headerKey: "ui.admin.common.User.Plural",
                listState: "accounts.users.list",
                listKey: "ui.admin.common.User.Plural",
                newState: "accounts.users.detail({'ID':'New'})",
                newKey: "ui.admin.views.home.NewUser",
                footerKey: "ui.admin.home.UsersInstructions",
                keywords: [null,"Account","Accounts","User","Users"]
            });
            if (this.cefConfig.featureSet.reviews.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Review.Plural",
                    listState: "accounts.reviews.list",
                    listKey: "ui.admin.common.Review.Plural",
                    footerKey: "ui.admin.home.ReviewInstructions",
                    keywords: [null,"Product","Products","Review","Reviews","Reviewing","User","Users"]
                });
            }
            tempPanels.push({
                headerKey: "ui.admin.common.Attribute.Plural",
                listState: "inventory.attributes.list",
                listKey: "ui.admin.common.Attribute.Plural",
                newState: "inventory.attributes.detail({'ID':'New'})",
                newKey: "ui.admin.views.home.NewAttribute",
                footerKey: "ui.admin.home.AttributeInstructions",
                keywords: [null,"Attribute","Attributes","Product","Products","Category","Categories"]
            });
            if (this.cefConfig.featureSet.inventory.advanced.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Warehouse.Plural",
                    listState: "inventory.warehouses.list",
                    listKey: "ui.admin.common.Warehouse.Plural",
                    newState: "inventory.warehouses.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewWarehouse",
                    footerKey: "ui.admin.home.WarehouseInstruction",
                    keywords: [null,"Inventory","Warehouse","Location","Bin","Shelf","Product","Products"]
                });
            }
            if (this.cefConfig.featureSet.stores.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Store.Plural",
                    listState: "accounts.stores.list",
                    listKey: "ui.admin.common.Store.Plural",
                    newState: "accounts.stores.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewStore",
                    footerKey: "ui.admin.home.StoreInstructions",
                    keywords: [null,"Marketing","Store","Stores","Site","Sites","Catalog","Catalogs","Index","Indexes"]
                });
            }
            if (this.cefConfig.featureSet.vendors.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Vendor.Plural",
                    listState: "inventory.vendors.list",
                    listKey: "ui.admin.common.Vendor.Plural",
                    newState: "inventory.vendors.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewVendor",
                    footerKey: "ui.admin.home.VendorInstructions",
                    keywords: [null,"Inventory","Vendor","Vendors","Product","Products"]
                });
            }
            if (this.cefConfig.featureSet.manufacturers.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.home.ManuLabel",
                    listState: "inventory.manufacturers.list",
                    listKey: "ui.admin.views.home.Manufacturer",
                    newState: "inventory.manufacturers.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewManufacturer",
                    footerKey: "ui.admin.home.ManuInstructions",
                    keywords: [null,"Inventory","Manufacturer","Manufacturers","Product","Products"]
                });
            }
            if (this.cefConfig.featureSet.brands.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.home.BrandLabel",
                    listState: "accounts.brands.list",
                    listKey: "ui.admin.common.Brand.Plural",
                    newState: "accounts.brands.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewBrand",
                    footerKey: "ui.admin.home.BrandInstructions",
                    keywords: [null,"Marketing","Brand","Branding","Store","Stores","Site","Sites"]
                });
            }
            if (this.cefConfig.featureSet.pricing.priceRules.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.PriceRule.Plural",
                    listState: "accounts.priceRules.list",
                    listKey: "ui.admin.common.PriceRule.Plural",
                    newState: "accounts.priceRules.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewPriceRule",
                    footerKey: "ui.admin.home.PriceRuleInstructions",
                    keywords: [null,"Price","Pricing","Product","Products"]
                });
            }
            /*
            if (this.cefConfig.ticketExchange.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.home.TicketRuleLabel",
                    listState: "accounts.ticketRules.list",
                    listKey: "ui.admin.controls.adminSiteMenu2.TicketRules",
                    newState: "accounts.ticketRules.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewTicketRule",
                    footerKey: "ui.admin.home.TicketRuleInstructions",
                });
            }
            */
            tempPanels.push({
                headerKey: "ui.admin.common.Role.Plural",
                listState: "accounts.roles.list",
                listKey: "ui.admin.common.Role.Plural",
                newState: "accounts.roles.detail({'ID':'New'})",
                newKey: "ui.admin.views.home.NewRole",
                footerKey: "ui.admin.home.RolesInstructions",
                keywords: [null,"Security","Role","Roles","User","Users","Account","Accounts"]
            });
            /*
            if (this.cefConfig.featureSet.shipping.carrierAccounts.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.home.CarrierLabel",
                    listState: "shipments.carrierAccounts.list",
                    listKey: "ui.admin.views.home.CarrierAccounts",
                    newState: "shipments.carrierAccounts.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewCarrierAccount",
                    footerKey: "ui.admin.home.CarrierInstructions",
                });
            }
            */
            if (this.cefConfig.featureSet.shipping.packages.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.home.ShippingPackagesLabel",
                    listState: "shipments.shippingPackages.list",
                    listKey: "ui.admin.views.home.ShippingPackages",
                    newState: "shipments.shippingPackages.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewShippingPackage",
                    footerKey: "ui.admin.home.ShippingPackagesInstructions",
                    keywords: [null,"Shipping","Package","Packages","Product","Products"]
                });
            }
            /*
            if (this.cefConfig.featureSet.shipping.discounts.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.home.ShippingDiscountsLabel",
                    listState: "shipments.shippingDiscounts.list",
                    listKey: "ui.admin.views.home.ShippingDiscounts",
                    newState: "shipments.shippingDiscounts.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewShippingDiscount",
                    footerKey: "ui.admin.home.ShippingDiscountsInstructions",
                    keywords: [null,"Shipping","Discount","Discounts","Product","Products"]
                });
            }
            */
            if (this.cefConfig.featureSet.stores.siteDomains.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.SiteDomain.Plural",
                    listState: "accounts.siteDomains.list",
                    listKey: "ui.admin.common.SiteDomain.Plural",
                    newState: "accounts.siteDomains.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewSiteDomain",
                    footerKey: "ui.admin.home.SiteDomainInstructions",
                    keywords: [null,"Marketing","Site","Sites","Domain","Domains","Brand","Brands","Store","Stores"]
                });
            }
            /*
            if (this.cefConfig.featureSet.stores.socialProviders.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.SocialProvider.Plural",
                    listState: "accounts.socialProviders.list",
                    listKey: "ui.admin.common.SocialProvider.Plural",
                    newState: "accounts.socialProviders.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewSocialProvider",
                    footerKey: "ui.admin.home.SocialInstructions",
                });
            }
            */
            if (this.cefConfig.featureSet.badges.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Badge.Plural",
                    listState: "accounts.badges.list",
                    listKey: "ui.admin.common.Badge.Plural",
                    newState: "accounts.badges.detail({'ID':'New'})",
                    newKey: "ui.admin.views.home.NewBadge",
                    footerKey: "ui.admin.home.BadgeInstructions",
                    keywords: [null,"Marketing","Badge","Badges","Store","Stores"]
                });
            }
            if (this.cefConfig.featureSet.reporting.enabled) {
                tempPanels.push({
                    headerKey: "ui.admin.common.Reporting",
                    listState: "system.reporting.loader",
                    listKey: "ui.admin.common.Badge.Plural",
                    newState: "system.reporting.designer",
                    newKey: "ui.admin.views.home.ReportDesigner",
                    footerKey: "ui.admin.home.ReportingInstructions",
                    keywords: [null,"Sales","Report","Reporting","Metric","Metrics"]
                });
            }
            tempPanels.push({
                headerKey: "ui.admin.common.Setting.Plural",
                listState: "system.settings.list",
                listKey: "ui.admin.common.Setting.Plural",
                newState: "system.settings.detail({'ID':'New'})",
                newKey: "ui.admin.views.home.NewSetting",
                footerKey: "ui.admin.home.SettingsInstructions",
                keywords: [null,"System","Setting","Settings"]
            });
            tempPanels.push({
                headerKey: "ui.admin.home.SiteMaintLabel",
                listState: "system.siteMaintenance",
                listKey: "ui.admin.views.system.SiteMaintenance",
                footerKey: "ui.admin.home.SiteMaintInstructions",
                keywords: [null,"System","Maintenance","Cache","Caches","Site","Sites"]
            });
            if (this.cefConfig.routes.scheduler && (this.cefConfig.routes.scheduler.host || this.cefConfig.routes.scheduler.root)) {
                tempPanels.push({
                    headerKey: "ui.admin.common.ScheduledTask.Plural",
                    listState: "system.scheduledTasks",
                    listKey: "ui.admin.common.ScheduledTask.Plural",
                    footerKey: "ui.admin.home.ScheduledTaskInstructions",
                    keywords: [null,"System","Maintenance","Scheduler","Task","Tasks"]
                });
            }
            tempPanels.push({
                headerKey: "ui.admin.common.APIReference",
                listState: "system.apiReference",
                listKey: "ui.admin.common.APIReference",
                footerKey: "ui.admin.home.APIInstructions",
                keywords: [null,"System","Maintenance","API","Reference"]
            });
            if (this.cefConfig.routes.connect && (this.cefConfig.routes.connect.host || this.cefConfig.routes.connect.root)) {
                tempPanels.push({
                    headerKey: "ui.admin.home.ClarityConnectLabel",
                    listState: "system.connect",
                    listKey: "ui.admin.views.home.ClarityConnect",
                    footerKey: "ui.admin.home.ClarityConnectInstructions",
                    keywords: [null,"System","Connect","Integration","Integrations"]
                });
            }
            this.panels = tempPanels;
            this.keywordsCloud = _.uniq(
                _.flatMap(
                    _.filter(
                        this.panels,
                        x => x.keywords && x.keywords.length),
                    x => x.keywords))
                .sort();
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSecurityService: services.ISecurityService) {
            super(cefConfig);
            this.load();
        }
    }

    adminApp.directive("home", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        replace: true,
        templateUrl: $filter("corsLink")("/framework/admin/controls/home.html", "ui"),
        controller: HomeWidgetController,
        controllerAs: "homeCtrl",
        bindToController: true
    }));
}
