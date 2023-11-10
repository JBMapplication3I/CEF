/**
 * @file framework/admin/controls/sales/salesReturnNewWizard.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc The New Sales Return wizard for Administrators
 */
module cef.admin.controls.sales {
    class StepDefinition {
        icon: string;
        title: string;
        form: () => ng.IFormController;
        onBeforePreviousStepGo: () => ng.IPromise<boolean>;
        onAfterPreviousStepGo: () => void;
        onStepStart: () => void;
        resetStep: () => void;
        onBeforeNextStepGo: () => ng.IPromise<boolean>;
        onAfterNextStepGo: () => void;
    }

    class SalesReturnNewWizardController extends core.TemplatedControllerBase {
        // Properties
        isSalesOrderPreSelected: boolean;
        salesOrder: api.SalesOrderModel;
        salesOrders: api.SalesOrderModel[] = [];
        reasons: api.SalesReturnReasonModel[];
        selectedSalesOrder: api.SalesOrderModel;
        existingReturns: api.SalesReturnModel[];
        itemsProducts: { [productID: number]: boolean } = {};
        itemsModel: { [index: number]: any/*api.SalesItemBaseModel*/ } = {};
        returnQty = {};
        orderSelectTxt: string;
        notesTxt: string;
        confirmationTxt: string;
        cartLineItemsTxt: string;
        userAccountTxt: string;
        itemCheckedCount: number;
        private readonly orderSelect = "ui.admin.controls.controls.sales.salesReturnNewWizard.OrderSelect";
        private readonly notes = "ui.admin.controls.controls.sales.salesReturnNewWizard.Notes";
        private readonly confirmation = "ui.admin.controls.controls.sales.salesReturnNewWizard.Confirmation";
        private readonly cartLineItems = "ui.admin.controls.controls.sales.salesReturnNewWizard.CartLineItems";
        private readonly userAccount = "ui.admin.controls.controls.sales.salesReturnNewWizard.UserAccount";

        // All Steps
        salesReturn: api.SalesReturnModel;
        currentStep = 0;
        stepDefinitions: StepDefinition[] = [];
        forms = {
            UserAndAccount: <ng.IFormController>{},
            SalesOrder: <ng.IFormController>{},
            SalesOrderItems: <ng.IFormController>{},
            Notes: <ng.IFormController>{},
            Confirmation: <ng.IFormController>{}
        };

        setupSteps(): void {
            this.stepDefinitions = [];
            // Step 1: User/Account Selection
            const step1 = new StepDefinition();
            step1.icon = "far fa-users";
            step1.title = this.userAccountTxt;
            step1.form = () => this.forms.UserAndAccount;
            step1.resetStep = () => this.resetUserAccountStep();
            step1.onAfterNextStepGo = () => this.setupSalesOrderStep();
            this.stepDefinitions.push(step1);
            // Step 2: Order Selection
            const step2 = new StepDefinition();
            step2.icon = "far fa-shopping-cart";
            step2.title = this.orderSelectTxt;
            step2.form = () => this.forms.SalesOrder;
            step2.resetStep = () => this.resetSalesOrderStep();
            step2.onStepStart = () => this.setupSalesOrderStep();
            step2.onAfterNextStepGo = () => this.setupSalesOrderItemsStep();
            this.stepDefinitions.push(step2);
            // Step 3: Order Items Selection
            const step3 = new StepDefinition();
            step3.icon = "far fa-shopping-cart";
            step3.title = this.cartLineItemsTxt;
            step3.form = () => this.forms.SalesOrderItems;
            step3.resetStep = () => this.resetSalesOrderItemsStep();
            step3.onStepStart = () => this.setupSalesOrderItemsStep();
            step3.onAfterNextStepGo = () => this.updateSelectedSalesItems();
            this.stepDefinitions.push(step3);
            // Step 4: Notes
            const step4 = new StepDefinition();
            step4.icon = "far fa-sticky-note";
            step4.title = this.notesTxt;
            step4.form = () => this.forms.Notes;
            step4.resetStep = () => this.resetNotesStep();
            step4.onStepStart = () => this.setupNotesStep();
            step4.onBeforeNextStepGo = (): ng.IPromise<boolean> => this.doSubmitSalesReturnCreation();
            this.stepDefinitions.push(step4);
            // Step 5: Confirmation
            const step5 = new StepDefinition();
            step5.icon = "far fa-check-square-o";
            step5.title = this.confirmationTxt;
            step5.form = () => this.forms.Confirmation;
            step5.resetStep = () => this.resetConfirmationStep();
            step5.onStepStart = () => this.setupConfirmationStep();
            this.stepDefinitions.push(step5);
        }

        validateCurrentStep = (): boolean => {
            return this.stepDefinitions[this.currentStep].form().$valid;
        }
        resetStep = (): void => {
            this.stepDefinitions[this.currentStep].resetStep();
        }
        prevStepGo = (): void => {
            if (this.stepDefinitions[this.currentStep] && this.stepDefinitions[this.currentStep].onBeforePreviousStepGo) {
                this.stepDefinitions[this.currentStep].onBeforePreviousStepGo().then(response => { if (response) { this.prevStepGoB(); } });
                return;
            }
            this.prevStepGoB();
        }
        private prevStepGoB = (): void => {
            this.currentStep--;
            if (this.stepDefinitions[this.currentStep + 1]
                && this.stepDefinitions[this.currentStep + 1].onAfterPreviousStepGo) {
                this.stepDefinitions[this.currentStep + 1].onAfterPreviousStepGo();
            }
        }
        nextStepGo = (): void => {
            if (this.stepDefinitions[this.currentStep] && this.stepDefinitions[this.currentStep].onBeforeNextStepGo) {
                this.stepDefinitions[this.currentStep].onBeforeNextStepGo().then(response => { if (response) { this.nextStepGoB(); } });
                return;
            }
            this.nextStepGoB();
        }
        private nextStepGoB = (): void => {
            this.currentStep++;
            if (this.stepDefinitions[this.currentStep - 1] && this.stepDefinitions[this.currentStep - 1].onAfterNextStepGo) {
                this.stepDefinitions[this.currentStep - 1].onAfterNextStepGo();
            }
        }

        // Pre-Step
        newSalesReturn = () => {
            this.selectedSalesOrder = null;
            this.salesReturn = <api.SalesReturnModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // SalesItemCollection Properties
                SourceType: null,
                SourceID: 0,
                SalesOrderTypeID: 1,
                ContactsUserID: 0,
                ItemQuantity: 0,
                SubtotalItems: 0,
                Totals: <api.CartTotals>{
                    SubTotal: 0,
                    Discounts: 0,
                    Shipping: 0,
                    Handling: 0,
                    Fees: 0,
                    Tax: 0,
                    Total: 0
                },
                SalesItems: [],
                SalesItemCollectionAttributes: null,
                FlattenedSalesItems: [],
                // SalesReturn Properties
                HasChildren: false,
                OrderDate: null,
                Balance: 0,
                ShippingSameAsBilling: false,
                AccountKey: null,
                TrackingNumber: null,
                UserID: 0,
                User: null,
                SalesOrderStatusID: 1,
                SalesOrderStatus: null,
                InvoiceIDs: [],
                PurchaseOrderIDs: [],
                PaymentDetails: [],
                SalesOrderItems: [],
                SalesOrderAttributes: [],
                SalesOrderOrderDiscounts: [],
                Notes: [],
                TypeName: null,
                Type: null,
                TypeID: 1,
                StatusName: null,
                Status: null,
                StatusID: 1,
                StateID: 1,
                ShipOption: null,
                ShippingAddress: null,
                Discounts: [],
                Attributes: [],
                RefundAmount: 0
            };
        }
        // Step 1: User/Account Step
        users: api.UserModel[] = [];
        accounts: api.AccountModel[] = [];
        accountToGrab: string;
        userToGrab: string;

        resetUserAccountStep(): void {
            this.selectedSalesOrder = null;
            this.salesReturn.UserID = null;
            this.salesReturn.User = null;
            this.salesReturn.UserUserName = null;
            this.salesReturn.UserContactFirstName = null;
            this.salesReturn.UserContactLastName = null;
            this.salesReturn.UserContactEmail = null;
            this.salesReturn.AccountID = null;
            this.salesReturn.Account = null;
            this.salesReturn.AccountKey = null;
            this.salesReturn.AccountName = null;
            this.salesOrders = null;
        }
        setAccountFromUser(): void {
            if (this.salesReturn.UserID == null) {
                // Do Nothing
                return;
            }
            const user = (this.users as any).find(u => u.ID === this.salesReturn.UserID);
            this.salesReturn.AccountID = user.AccountID;
        }
        grabAccounts = (search: string) => this.cvApi.accounts.GetAccounts({
            Active: true,
            AsListing: true,
            IDOrCustomKeyOrName: search,
            Paging: <api.Paging>{ Size: 50, StartIndex: 1 }
        }).then(r => this.accounts = r.data.Results);
        grabUsers = (search: string) => this.cvApi.contacts.GetUsers({
            Active: true,
            AsListing: true,
            AccountID: this.salesReturn.AccountID,
            IDOrUserNameOrCustomKeyOrEmailOrContactName: search,
            Paging: <api.Paging>{ Size: 50, StartIndex: 1 }
        }).then(r => this.users = r.data.Results);
        selectAccountFromTypeAhead = ($item, $model) => { this.salesReturn.AccountID = $model; }
        selectUserFromTypeAhead = ($item, $model) => { this.salesReturn.UserID = $model; }

        // Step 2: Sales Order Step
        resetSalesOrderStep() {
            this.selectedSalesOrder = null;
        }

        setupSalesOrderStep = (): void => {
            this.selectedSalesOrder = null;
            this.cvApi.ordering.GetSalesOrders({
                Active: true,
                AsListing: true,
                AccountID: this.salesReturn.Account.ID,
                StatusNames: ["Shipped", "Shipped from Vendor", "Completed"]
            }).then(r => this.salesOrders = r.data.Results);
        }

        // Step 3: Sales Order Items Step
        resetSalesOrderItemsStep() {
            this.initializeItemsModel();
        }

        initializeItemsModel(): void {
            this.salesReturn.SalesItems = [];
            this.itemsModel = {};
            this.itemsProducts = {};
            if (this.selectedSalesOrder == null || this.selectedSalesOrder.SalesItems == null) {
                return;
            }
            this.selectedSalesOrder.SalesItems.forEach(item => {
                this.itemsProducts[item.ID] = item.ProductIsEligibleForReturn;
                var retQty = this.returnQty[item.ProductID];
                if (!retQty) {
                    retQty = 0;
                }
                var qty = item.Quantity - retQty;
                this.itemsModel[item.ID] = {
                    ID: item.ID,
                    Quantity: item.Quantity,
                    ProductID: item.ProductID,
                    QuantityReturnable: qty < 0 ? 0 : qty,
                    SelectedQuantity: 1,
                    itemNote: null,
                    selectedReason: ""
                };
            });
        }

        setupSalesOrderItemsStep = (): void => {
            this.salesReturn.SalesItems = [];
            this.salesReturn.SalesOrderIDs = [];
            this.salesReturn.SalesOrderIDs.push(this.selectedSalesOrder.ID);
            this.returnQty = {};
            this.cvApi.returning.GetSalesReturns({
                Active: true,
                AsListing: true,
                SalesOrderID: this.selectedSalesOrder.ID
            }).then(r => {
                this.existingReturns = r.data.Results;
                this.existingReturns.forEach(sr => {
                    sr.SalesItems.forEach(item => {
                        if (!this.returnQty[item.ProductID]) {
                            this.returnQty[item.ProductID] = 0;
                        }
                        this.returnQty[item.ProductID] += item.Quantity;
                    });
                });
                this.initializeItemsModel();
            });
        }

        updateSelectedSalesItems = (): void => {
            this.salesReturn.SalesItems = [];
            angular.forEach(this.itemsModel, (item: any) => {
                if (!item.itemChecked) { return; }
                this.salesReturn.SalesItems.push({
                    ID: item.ID,
                    ExtendedPrice: 0.00,
                    ItemType: null,
                    RestockingFeeAmount: 0.00,
                    Quantity: item.SelectedQuantity,
                    UnitCorePrice: 0.00,
                    Active: true,
                    CreatedDate: new Date(),
                    ProductID: item.ProductID,
                    Description: item.itemNote,
                    SalesReturnReasonID: item.selectedReason
                });
            });
        }

        // Step 4: Notes
        resetNotesStep() { }

        setupNotesStep() { }

        // Step 5: Confirmation

        doSubmitSalesReturnCreation = (): ng.IPromise<boolean> => {
            var defer = this.$q.defer<boolean>();
            this.cvApi.providers.CreateSalesReturnAsAdmin(this.salesReturn).then(response => {
                if (response.data && response.data.ActionSucceeded) {
                    this.salesReturn.ID = response.data.Result;
                    defer.resolve(true);
                } else {
                    console.log("Transaction Error");
                    defer.reject(false);
                }
            });
            return defer.promise;
            /*
            this.cvApi.providers.CreateSalesReturnAsAdmin(this.salesReturn).success(result => {
                this.salesReturn.ID = result.Result;
                if (result && result.ActionSucceeded) {
                    //this.nextStepGo();
                    defer.resolve(true);
                } else {
                    console.log("Transaction Error");
                    defer.reject(false);
                }
            }).error(data => { console.log(data); defer.reject(false); });
            */
        };

        resetConfirmationStep() { }

        setupConfirmationStep() { }

        load(): void {
            this.itemCheckedCount = 0;
            this.cvApi.returning.GetSalesReturnReasons({ Active: true, AsListing: true }).then(r => this.reasons = r.data.Results);
        }

        checkDisableControl(itemID: number): boolean {
            if (this.itemsModel[itemID] && this.itemsModel[itemID].itemChecked) {
                return false;
            }
            if (this.itemsModel[itemID] && !this.itemsModel[itemID].itemChecked) {
                this.itemsModel[itemID].selectedQuantity = null;
            }
            return true;
        }

        hasRestockFee(itemID: number): boolean {
            const item = this.itemsModel[itemID];
            if (!item) { return false; }
            var reasonID = this.itemsModel[itemID].selectedReason;
            const reason = (<any>(this.reasons)).find(x => x.ID === reasonID);
            if (!reason) { return false; }
            if (reason.IsRestockingFeeApplicable) {
                return true;
            }
            return false;
        }

        isEligibleForReturn(itemID: number): boolean {
            if (!this.itemsModel[itemID] || this.itemsModel[itemID].QuantityReturnable <= 0) {
                return false;
            }
            return this.itemsProducts[itemID];
        }

        updateSelectedItems(itemID: number): boolean {
            if (!this.itemsModel[itemID].itemChecked) {
                this.itemsModel[itemID].selectedReason = null;
                this.itemsModel[itemID].selectedQuantity = null;
                this.itemsModel[itemID].Description = null;
                this.itemCheckedCount--;
                return true;
            }
            if (!this.itemsModel[itemID].selectedQuantity) {
                this.itemsModel[itemID].selectedQuantity = 1;
            }
            this.itemCheckedCount++;
            return false;
        }
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $q: ng.IQService,
                private readonly $stateParams: ng.ui.IStateParamsService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.$translate([
                this.orderSelect,
                this.notes,
                this.confirmation,
                this.cartLineItems,
                this.userAccount
            ]).then(x => {
                this.orderSelectTxt = x[this.orderSelect];
                this.notesTxt = x[this.notes];
                this.confirmationTxt = x[this.confirmation];
                this.cartLineItemsTxt = x[this.cartLineItems];
                this.userAccountTxt = x[this.userAccount];
                const id = this.$stateParams.ID;
                if (id > 0) {
                    this.isSalesOrderPreSelected = true;
                    this.setupSteps();
                    this.currentStep = 2;
                    this.cvApi.ordering.GetSalesOrderByID(id).success(data => {
                        this.newSalesReturn();
                        this.selectedSalesOrder = data;
                        this.salesReturn.CustomKey = this.selectedSalesOrder.CustomKey;
                        this.salesReturn.AccountKey = this.selectedSalesOrder.AccountKey;
                        this.salesReturn.Account = this.selectedSalesOrder.Account;
                        this.salesReturn.UserID = this.selectedSalesOrder.UserID;
                        this.salesReturn.UserUserName = this.selectedSalesOrder.UserUserName;
                        this.salesReturn.User = this.selectedSalesOrder.User;
                        this.setupSalesOrderItemsStep();
                    });
                    this.load();
                    return;
                }
                this.setupSteps();
                $scope.$watch(() => this.salesReturn.AccountID,
                    (newVal: number, oldVal: number) => {
                        if (newVal === oldVal) {
                            return;
                        }
                        if (!newVal) {
                            this.salesReturn.AccountID = null;
                            this.salesReturn.Account = null;
                            return;
                        }
                        this.cvApi.accounts.GetAccountByID(newVal)
                            .then(response => {
                                this.salesReturn.Account = response.data;
                                this.salesReturn.AccountKey = this.salesReturn.Account.CustomKey;
                                this.salesReturn.AccountName = this.salesReturn.Account.Name;
                                /*this.cvApi.ordering.GetSalesOrders({
                                    AccountID: this.salesOrder.Account.ID,
                                    Paging: <api.Paging>{ Size: 20, StartIndex: 1 }
                                }).then(r => {
                                    this.salesOrders = r.data.Results;
                                });*/
                            });
                    });
                $scope.$watch(() => this.salesReturn.UserID,
                    (newVal: number, oldVal: number) => {
                        if (newVal === oldVal) {
                            return;
                        }
                        if (!newVal) {
                            this.salesReturn.UserID = null;
                            this.salesReturn.User = null;
                            return;
                        }
                        this.cvApi.contacts.GetUserByID(newVal)
                            .then(response => {
                                this.salesReturn.User = response.data;
                                this.salesReturn.UserKey = this.salesReturn.User.CustomKey;
                                this.salesReturn.UserUserName = this.salesReturn.User.UserName;
                                this.salesReturn.UserContactFirstName = this.salesReturn.User.ContactFirstName;
                                this.salesReturn.UserContactLastName = this.salesReturn.User.ContactLastName;
                                this.salesReturn.UserContactEmail = this.salesReturn.User.ContactEmail;
                                this.salesReturn.AccountID = this.salesReturn.User.AccountID;
                                this.accountToGrab = this.salesReturn.AccountID.toString();
                            });
                    });
                this.newSalesReturn();
            });
        }
    }

    adminApp.directive("salesReturnNewWizard", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesReturnNewWizard.html", "ui"),
        controller: SalesReturnNewWizardController,
        controllerAs: "salesReturnNewWizardCtrl"
    }));
}
