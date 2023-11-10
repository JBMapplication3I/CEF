/**
 * @file framework/store/product/controls/productDetailTabsWidget.ts
 * @desc Product detail tabs widget class
 */
module cef.store.product.controls {
    class ProductDetailTabsWidgetController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        // Properties
        defaultShippingContact: api.ContactModel;
        AvailableStartDate: null;
        AvailableEndDate: null;
        detailsAttrList: api.GeneralAttributeModel[];
        attributes: api.GeneralAttributeModel[];
        attributeTabs: api.AttributeTabModel[];
        attributeGroups: api.AttributeGroupModel[];
        keysTabbedAndGrouped: { [tab: string]: { [group: string]: string[] } } = { };
        fullTabSet: any[];
        showBillingCodes: boolean = false;
        // Functions
        loadUser(): void {
            this.setRunning();
            this.cvAuthenticationService.preAuth().finally(() => {
                if (this.cvAuthenticationService.isAuthenticated()) {
                    this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                        if (user.SerializableAttributes != null && user?.SerializableAttributes["ShowBillingCodeForUser"]?.Value?.toLowerCase() === "true") {
                            this.showBillingCodes = true;
                        }
                    })
                    .finally(() => {
                        this.finishRunning();
                    });
                }
                this.resolveAttributes();
            });
        }
        productAttr(key: string): api.SerializableAttributeObject {
            return this.product
                && this.product.SerializableAttributes
                && this.product.SerializableAttributes[key];
        }
        resolveAttributes(): ng.IPromise<void> {
            return this.$q && this.$q((resolve, reject) => {
                this.$q.all([
                    this.cvApi.attributes.GetAttributeGroups({ Active: true, AsListing: true }),
                    this.cvApi.attributes.GetAttributeTabs({ Active: true, AsListing: true }),
                    this.cvApi.attributes.GetGeneralAttributes({
                        Active: true,
                        AsListing: true,
                        HideFromProductDetailView: false
                    }),
                    this.$translate("ui.storefront.product.details.MoreInformation"),
                    this.$translate("ui.storefront.product.detail.productDetails.Specifications")
                ]).then(rArray => {
                    const response0 = rArray[0] as ng.IHttpPromiseCallbackArg<api.AttributeGroupPagedResults>;
                    const response1 = rArray[1] as ng.IHttpPromiseCallbackArg<api.AttributeTabPagedResults>;
                    const response2 = rArray[2] as ng.IHttpPromiseCallbackArg<api.GeneralAttributePagedResults>;
                    const defaultTabTitle = rArray[3] as string;
                    const defaultGroupTitle = rArray[4] as string;
                    this.attributeGroups = response0.data.Results;
                    this.attributeTabs = response1.data.Results;
                    if (!response2 || !response2.data || !response2.data.Results) {
                        reject();
                        return;
                    }
                    this.attributes = response2.data.Results;
                    const list = response2.data.Results
                        .filter(x => x.IsTab)
                        .filter(x => Object.keys(this.product.SerializableAttributes || {})
                            .some(y => y === x.CustomKey));
                    this.detailsAttrList = list;
                    // Set up the full tab set, allowing everything to be sorted and grouped together
                    this.fullTabSet = [];
                    const haveGroups = this.attributeGroups && this.attributeGroups.length;
                    const haveTabs = this.attributeTabs && this.attributeTabs.length;
                    const haveIsTabs = this.detailsAttrList && this.detailsAttrList.length;
                    Object.keys(this.product.SerializableAttributes || {}).forEach(key => {
                        const attribute = _.find(this.attributes, x => x.CustomKey == key);
                        if (angular.isUndefined(attribute)) {
                            return;
                        }
                        const keyHasTab = haveTabs && attribute.AttributeTabID
                            && this.attributeTabs.some(x => x.ID == attribute.AttributeTabID);
                        const keyIsTab = haveIsTabs && this.detailsAttrList.some(x => x.CustomKey == key);
                        const keyHasGroup = haveGroups && attribute.AttributeGroupID
                            && this.attributeGroups.some(x => x.ID == attribute.AttributeGroupID);
                        const thisTabKey = keyIsTab ? key : keyHasTab ? _.find(this.attributeTabs, x => x.ID == attribute.AttributeTabID).CustomKey : "default-tab";
                        const thisGroupKey = keyHasGroup ? _.find(this.attributeGroups, x => x.ID == attribute.AttributeGroupID).CustomKey : "default-group";
                        if (!this.keysTabbedAndGrouped) {
                            this.keysTabbedAndGrouped = { };
                        }
                        if (!this.keysTabbedAndGrouped[thisTabKey]) {
                            this.keysTabbedAndGrouped[thisTabKey] = { };
                        }
                        if (!this.keysTabbedAndGrouped[thisTabKey][thisGroupKey]) {
                            this.keysTabbedAndGrouped[thisTabKey][thisGroupKey] = [];
                        }
                        this.keysTabbedAndGrouped[thisTabKey][thisGroupKey].push(key);
                    });
                    Object.keys(this.keysTabbedAndGrouped).forEach(tabKey => {
                        let tab = _.find(this.attributeTabs, x => x.CustomKey == tabKey);
                        if (!tab) {
                            tab = <api.AttributeTabModel>{
                                Active: true,
                                CreatedDate: new Date(),
                                CustomKey: tabKey,
                                Name: tabKey === "default-tab" ? defaultTabTitle : tabKey,
                                SortOrder: tabKey === "default-tab" ? -999999 : _.find(this.attributes, x => x.CustomKey == tabKey).SortOrder
                            };
                        }
                        const fullTab = {
                            key: tab.CustomKey,
                            header: tab.DisplayName || tab.Name || tab.CustomKey,
                            sort: tab.SortOrder,
                            groups: [],
                        };
                        Object.keys(this.keysTabbedAndGrouped[tabKey]).forEach(groupKey => {
                            let group = _.find(this.attributeGroups, x => x.CustomKey == groupKey);
                            if (!group) {
                                group = <api.AttributeGroupModel>{
                                    Active: true,
                                    CreatedDate: new Date(),
                                    CustomKey: "default-group",
                                    Name: defaultGroupTitle,
                                    SortOrder: -999999
                                };
                            }
                            fullTab.groups.push({
                                key: group.CustomKey,
                                header: group.DisplayName || group.Name || group.CustomKey,
                                sort: group.SortOrder,
                                attrs: this.attributes
                                    .filter(x => this.keysTabbedAndGrouped[tabKey][groupKey].indexOf(x.CustomKey) !== -1)
                            });
                        });
                        this.fullTabSet.push(fullTab);
                    });
                    resolve();
                })
                .catch(reject)
                .finally(() => {
                    if (!this.showBillingCodes) {
                        this.fullTabSet = this.fullTabSet.filter(x => x.key.toLowerCase() !== "billing codes")
                    }
                })
            });
        }
        loadDefaultShippingAddress(): void {
            this.setRunning();
            this.cvApi.accounts.GetCurrentAccount().then(r => {
                if (r && r.data && r.data.AccountContacts && r.data.AccountContacts.length) {
                    const found = _.find(r.data.AccountContacts, x => x.IsPrimary);
                    if (found) {
                        this.defaultShippingContact = found.Slave;
                    }
                }
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.loadUser();
            this.loadDefaultShippingAddress();
        }
    }

    cefApp.directive("cefProductTabs", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/tabs.html", "ui"),
        controller: ProductDetailTabsWidgetController,
        controllerAs: "productDetailTabsCtrl",
        bindToController: true
    }));
}
