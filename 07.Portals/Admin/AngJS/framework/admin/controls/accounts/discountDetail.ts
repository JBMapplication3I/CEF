module cef.admin.controls.accounts {
    class DiscountDetailController extends DetailBaseController<api.DiscountModel> {
        // Forced overrides
        detailName = "Discount";
        // Collections
        readonly valueTypes: { ID: number; Name: string; }[] = [
            { ID: 0, Name: "%" },
            { ID: 1, Name: "$" }
        ];
        scopes: { ID: number; Name: string; }[] = [];
        readonly compareOperators: { ID: string; Name: string; }[] = [
            { ID: "Undefined", Name: "None" },
            { ID: "GreaterThan", Name: ">" },
            { ID: "GreaterThanOrEqualTo", Name: ">=" }
        ];
        // UI Data
        roles: cefalt.admin.Dictionary<number>;
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            this.scopes = [];
            this.scopes.push({ ID: 0, Name: "Applied to Order" });
            this.scopes.push({ ID: 1, Name: "Applied to Product" });
            if (this.cefConfig.featureSet.shipping.enabled
                && !this.cefConfig.featureSet.shipping.splitShipping.enabled) {
                this.scopes.push({ ID: 2, Name: "Applied to Shipping" });
            }
            this.scopes.push({ ID: 3, Name: "Buy X Get Y" });
            this.cvApi.authentication.GetRoles().then(r => this.roles = r.data);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.DiscountModel> {
            this.record = <api.DiscountModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                // NameableBase Properties
                Name: null,
                Description: null,
                // Discount Properties
                CanCombine: true,
                IsAutoApplied: false,
                RoundingOperation: 0,
                DiscountCompareOperator: api.CompareOperator.Undefined,
                DiscountTypeID: 0,
                ValueType: 1,
                RoundingType: null,
                Priority: 10,
                StartDate: null,
                EndDate: null,
                Value: 0,
                ThresholdAmount: 0,
                BuyXValue: 0,
                GetYValue: 0,
                DiscountTotal: 0,
                // Associated Objects
                Codes: [],
                Users: [],
                Brands: [],
                Stores: [],
                Vendors: [],
                Accounts: [],
                Products: [],
                Countries: [],
                Categories: [],
                AccountTypes: [],
                ProductTypes: [],
                Manufacturers: [],
                ShipCarrierMethods: [],
            };
            this.addCode();
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> {
            this.detailName = "Discount";
            return this.$q.resolve();
        }
        loadRecordCall(id: number): ng.IHttpPromise<api.DiscountModel> {
            return this.cvApi.discounts.GetDiscountByID(id);
        }
        createRecordCall(routeParams: api.DiscountModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.validateDiscount(this.cvApi.discounts.CreateDiscount);
        }
        updateRecordCall(routeParams: api.DiscountModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.validateDiscount(this.cvApi.discounts.UpdateDiscount);
        }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.discounts.DeactivateDiscountByID(id);
        }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.discounts.ReactivateDiscountByID(id);
        }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.discounts.DeleteDiscountByID(id);
        }
        loadRecordActionAfterSuccess(result: api.DiscountModel): ng.IPromise<api.DiscountModel> {
            this.cleanRecord(result);
            return this.$q.resolve(result);
        }
        cleanRecord(result: api.DiscountModel): void {
            this.fixDates();
        }
        // Supportive Functions
        addUserRole(name: string): void {
            if (!name) { return; }
            // Ensure the data is loaded
            if (!this.roles[name]) { return; }
            if (!this.record.UserRoles) {
                this.record.UserRoles = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.record.UserRoles, x => x.RoleName === name)) {
               return;
            }
            // Add it
            this.record.UserRoles.push(<api.DiscountUserRoleModel>{
               // Base Properties
               Active: true,
               CreatedDate: new Date(),
               MasterID: 0,
               // User Role
               RoleName: name,
            });
            this.forms["UserRoles"].$setDirty();
        }
        removeUserRole(toRemove: api.DiscountUserRoleModel): void {
            for (let i = 0; i < this.record.UserRoles.length; i++) {
                if (toRemove === this.record.UserRoles[i]) {
                    this.record.UserRoles.splice(i, 1);
                    this.forms["UserRoles"].$setDirty();
                    return;
                }
            }
        }

        // Discount Code Management Events
        codeToAdd: string = null;
        codeToAddIsDuplicate: boolean = false;
        checkCodeToAddForDuplicate(): void {
            if (!this.codeToAdd) {
                this.codeToAddIsDuplicate = false;
                return;
            }
            if (this.record.Codes.some(x => x.Code === this.codeToAdd)) {
                this.codeToAddIsDuplicate = true;
                return;
            }
            this.cvApi.discounts.CheckDiscountCodeExistsByCode({ Code: this.codeToAdd })
                .then(r => this.codeToAddIsDuplicate = r.data > 0);
        }
        addCode(): void {
            if (!this.codeToAdd || this.codeToAddIsDuplicate) {
                return;
            }
            if (!this.record.Codes) {
                this.record.Codes = [];
            }
            this.record.Codes.push({
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                DiscountID: 0,
                Code: this.codeToAdd
            });
            this.forms["Details"].$setDirty();
        }
        removeCode(toRemove: api.DiscountCodeModel): void {
            for (let i = 0; i < this.record.Codes.length; i++) {
                if (toRemove === this.record.Codes[i]) {
                    this.record.Codes.splice(i, 1);
                    this.forms["Details"].$setDirty();
                    return;
                }
            }
        }

        // NOTE: This must remain an arrow function
        fixDates = (): void => {
            if (this.record.StartDate != null) {
                const tmpStartDate = new Date(this.record.StartDate.toString());
                tmpStartDate.setTime(tmpStartDate.getTime() + tmpStartDate.getTimezoneOffset() * 60 * 1000);
                this.record.StartDate = tmpStartDate;
            }
            if (this.record.EndDate != null) {
                const tmpEndDate = new Date(this.record.EndDate.toString());
                tmpEndDate.setTime(tmpEndDate.getTime() + tmpEndDate.getTimezoneOffset() * 60 * 1000);
                this.record.EndDate = tmpEndDate;
            }
        }
        validateDiscount = (callback: Function): ng.IHttpPromise<api.CEFActionResponseT<number>> => {
            return this.$q((resolve, reject) => {
                if (this.record.Codes.length > 0) {
                    resolve(callback(this.record));
                } else {
                    reject("Please add a code");
                }
            });
        }
        protected exportRecordForQA(): void {
            this.$copyToClipboard.copy(angular.toJson(this.record)).then(() => {
                alert("The record has been serialized to json and copied into the clipboard for you");
            });
        }
        // NOTE: This must remain an arrow function for angular events
        onScopeSelectUpdated = ($event: ng.IAngularEvent, control: ng.IAugmentedJQuery, newValue: number) => {
            if (newValue && newValue == 3) {
                this.record.ThresholdAmount = 0;
            }
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $state: ng.ui.IStateService,
                protected readonly $window: ng.IWindowService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $q: ng.IQService,
                protected readonly $copyToClipboard: ICopyToClipboard,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("discountDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/discountDetail.html", "ui"),
        controller: DiscountDetailController,
        controllerAs: "deCtrl"
    }));

    export interface ICopyToClipboard {
        copy(stringToCopy: string): ng.IPromise<boolean>;
    }
}
