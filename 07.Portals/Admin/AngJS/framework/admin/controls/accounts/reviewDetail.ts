/**
 * @file framework/admin/controls/accounts/reviewDetail.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Review detail class
 */
module cef.admin.controls.accounts {
    class ReviewDetailController extends DetailBaseController<api.ReviewModel> {
        // Forced overrides
        detailName = "Review";
        // Collections
        types: api.TypeModel[] = [];
        categories: api.CategoryModel[] = [];
        manufacturers: api.ManufacturerModel[] = [];
        products: api.ProductModel[] = [];
        stores: api.StoreModel[] = [];
        users: api.UserModel[] = [];
        vendors: api.VendorModel[] = [];
        // UI Data
        // <None>
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            this.cvApi.reviews.GetReviewTypes({ Active: true, AsListing: true }).then(r => this.types = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.ReviewModel> {
            this.record = <api.ReviewModel>{
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                Name: null,
                Description: null,
                //
                Title: null,
                Comment: null,
                Value: null,
                Location: null,
                SortOrder: null,
                Approved: false,
                ApprovedDate: null,
                // Related Objects
                TypeID: 0,
                Type: null,
                TypeKey: null,
                TypeName: null,
                SubmittedByUserID: 0,
                SubmittedByUser: null,
                SubmittedByUserUserName: null,
                ApprovedByUserID: 0,
                ApprovedByUser: null,
                ApprovedByUserUserName: null,
                //
                CategoryID: 0,
                Category: null,
                ManufacturerID: 0,
                Manufacturer: null,
                ProductID: 0,
                Product: null,
                StoreID: 0,
                Store: null,
                UserID: 0,
                User: null,
                VendorID: 0,
                Vendor: null,
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Review"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.ReviewModel> { return this.cvApi.reviews.GetReviewByID(id); }
        createRecordCall(routeParams?: api.ReviewModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.reviews.CreateReview(routeParams); }
        updateRecordCall(routeParams?: api.ReviewModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.reviews.UpdateReview(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.reviews.DeactivateReviewByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.reviews.ReactivateReviewByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.reviews.DeleteReviewByID(id); }
        loadRecordActionAfterSuccess(result: api.ReviewModel): ng.IPromise<api.ReviewModel> {
            if (!result) {
                return this.$q.resolve(this.record);
            }
            switch (this.record.TypeKey) {
                case "Category": {
                    this.cvApi.categories.GetCategories({
                        Active: true,
                        AsListing: true,
                        Paging: { Size: 50, StartIndex: 1 },
                        ID: this.record.CategoryID,
                        IncludeChildrenInResults: false
                    }).then(r => {
                        this.categories = r.data.Results;
                        this.record.Category = _.find(this.categories, value => value.ID === this.record.CategoryID);
                        this.finishRunning();
                    });
                    break;
                }
                case "Manufacturer": {
                    this.cvApi.manufacturers.GetManufacturers({ Active: true, AsListing: true, Paging: { Size: 50, StartIndex: 1 }, ID: this.record.ManufacturerID }).then(r => {
                        this.manufacturers = r.data.Results;
                        this.record.Manufacturer = _.find(this.manufacturers, value => value.ID === this.record.ManufacturerID);
                        this.finishRunning();
                    });
                    break;
                }
                case "Product": {
                    this.cvApi.products.GetProducts({ Active: true, AsListing: true, Paging: { Size: 50, StartIndex: 1 }, ID: this.record.ProductID }).then(r => {
                        this.products = r.data.Results;
                        this.record.Product = _.find(this.products, value => value.ID === this.record.ProductID);
                        this.finishRunning();
                    });
                    break;
                }
                case "Store": {
                    this.cvApi.stores.GetStores({ Active: true, AsListing: true, Paging: { Size: 50, StartIndex: 1 }, ID: this.record.StoreID }).then(r => {
                        this.stores = r.data.Results;
                        this.record.Store = _.find(this.stores, value => value.ID === this.record.StoreID);
                        this.finishRunning();
                    });
                    break;
                }
                case "User": {
                    this.cvApi.contacts.GetUsers({ Active: true, AsListing: true, Paging: { Size: 50, StartIndex: 1 }, ID: this.record.UserID }).then(r => {
                        this.users = r.data.Results;
                        this.record.User = _.find(this.users, value => value.ID === this.record.UserID);
                        this.finishRunning();
                    });
                    break;
                }
                case "Vendor": {
                    this.cvApi.vendors.GetVendors({ Active: true, AsListing: true, Paging: { Size: 50, StartIndex: 1 }, ID: this.record.VendorID }).then(r => {
                        this.vendors = r.data.Results;
                        this.record.Vendor = _.find(this.vendors, value => value.ID === this.record.VendorID);
                        this.finishRunning();
                    });
                    break;
                }
            }
            return this.$q.resolve(this.record);
        }
        // Supportive Functions
        typeChanged(): void {
            this.$translate("ui.admin.common.Updating.Ellipses").then(translated => {
                this.setRunning(translated as string);
                // Assign the new type that was selected
                var type = _.find(this.types, value => value.ID === this.record.TypeID);
                if (!this.record.Type && this.record.TypeID) {
                    this.record.Type = type;
                    this.record.TypeKey = this.record.Type.CustomKey;
                    this.record.TypeName = this.record.Type.Name;
                }
                if (this.record.Type.ID === this.record.TypeID) { return; } // Don't change to same
                this.record.Type = type;
                this.record.TypeKey = this.record.Type.CustomKey;
                this.record.TypeName = this.record.Type.Name;
                // Clear all the selected temp objects
                // Purge all the review lists
                this.record.CategoryID = null;
                this.record.Category = null;
                this.record.CategoryKey = null;
                this.record.CategoryName = null;
                this.record.ManufacturerID = null;
                this.record.Manufacturer = null;
                this.record.ManufacturerKey = null;
                this.record.ManufacturerName = null;
                this.record.ProductID = null;
                this.record.Product = null;
                this.record.ProductKey = null;
                this.record.ProductName = null;
                this.record.StoreID = null;
                this.record.Store = null;
                this.record.StoreKey = null;
                this.record.StoreName = null;
                this.record.UserID = null;
                this.record.User = null;
                this.record.UserKey = null;
                this.record.UserUserName = null;
                this.record.VendorID = null;
                this.record.Vendor = null;
                this.record.VendorKey = null;
                this.record.VendorName = null;
                /* Add to the review list for the type selected
                switch (this.record.TypeKey) {
                    case "Category": { this.record.Category = this.loadNewCategory(); break; }
                    case "Manufacturer": { this.record.Manufacturer = this.loadNewManufacturer(); break; }
                    case "Product": { this.record.Product = this.loadNewProduct(); break; }
                    case "Store": { this.record.Store = this.loadNewStore(); break; }
                    case "User": { this.record.User = this.loadNewUser(); break; }
                    case "Vendor": { this.record.Vendor = this.loadNewVendor(); break; }
                }*/
                this.forms["Details"].$setDirty();
                this.finishRunning();
            });
        }
        approve(): void {
            this.setRunning();
            this.cvApi.reviews.ApproveReview({ ID: this.record.ID }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, r && r.data as any);
                    return;
                }
                this.finishRunning();
                this.loadRecord(this.record.ID);
            }).catch(reason => this.finishRunning(true, reason));
        }
        unapprove(): void {
            this.setRunning();
            this.cvApi.reviews.UnapproveReview({ ID: this.record.ID }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, r && r.data as any);
                    return;
                }
                this.finishRunning();
                this.loadRecord(this.record.ID);
            }).catch(reason => this.finishRunning(true, reason));
        }
        //
        loadNewCategory(): api.CategoryModel {
            return <api.CategoryModel> {
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                IsVisible: false,
                IncludeInMenu: false,
                HasChildren: false,
                TypeID: 0,
            };
        }
        loadNewManufacturer(): api.ManufacturerModel {
            return <api.ManufacturerModel>{
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
            };
        }
        loadNewProduct(): api.ProductModel {
            return <api.ProductModel>{
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                IsVisible: false,
                IsShippingRestricted: false,
                IsUnlimitedStock: false,
                InWishList: false,
                InNotifyMeList: false,
                InFavoritesList: false,
                InCompareCart: false,
                IsEligibleForReturn: false,
                IsDiscontinued: false,
                AllowBackOrder: false,
                AllowPreSale: false,
                NothingToShip: false,
                DropShipOnly: false,
                ShippingLeadTimeIsCalendarDays: false,
                IsTaxable: true,
                IsFreeShipping: false,
                TypeID: 1,
                StatusID: 1,
                MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent: false,
                DocumentRequiredForPurchaseOverrideFeeIsPercent: false
            };
        }
        loadNewStore(): api.StoreModel {
            return <api.StoreModel>{
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                TypeID: 0
            };
        }
        loadNewUser(): api.UserModel {
            return <api.UserModel>{
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                EmailConfirmed: false,
                PhoneNumberConfirmed: false,
                TwoFactorEnabled: false,
                LockoutEnabled: false,
                AccessFailedCount: 0,
                IsDeleted: false,
                IsSuperAdmin: false,
                IsEmailSubscriber: false,
                IsCatalogSubscriber: false,
                StatusID: 0,
                TypeID: 0,
                ContactID: 0,
            };
        }
        loadNewVendor(): api.VendorModel {
            return <api.VendorModel>{
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                AllowDropShip: false,
            };
        }
        //
        categoryChanged(): void {
            if (this.viewState.running) { return; }
            var isNull = !this.record.Category;
            if (isNull) { return; }
            this.record.CategoryID = this.record.Category.ID;
            this.record.Category = this.record.Category;
            this.record.CategoryKey = this.record.Category.CustomKey;
            this.record.CategoryName = this.record.Category.Name;
            this.forms["Details"].$setDirty();
        }
        manufacturerChanged(): void {
            if (this.viewState.running) { return; }
            var isNull = !this.record.Manufacturer;
            if (isNull) { return; }
            this.record.ManufacturerID = this.record.Manufacturer.ID;
            this.record.Manufacturer = this.record.Manufacturer;
            this.record.ManufacturerKey = this.record.Manufacturer.CustomKey;
            this.record.ManufacturerName = this.record.Manufacturer.Name;
            this.forms["Details"].$setDirty();
        }
        productChanged(): void {
            if (this.viewState.running) { return; }
            var isNull = !this.record.Product;
            if (isNull) { return; }
            this.record.ProductID = this.record.Product.ID;
            this.record.Product = this.record.Product;
            this.record.ProductKey = this.record.Product.CustomKey;
            this.record.ProductName = this.record.Product.Name;
            this.forms["Details"].$setDirty();
        }
        storeChanged(): void {
            if (this.viewState.running) { return; }
            var isNull = !this.record.Category;
            if (isNull) { return; }
            this.record.StoreID = this.record.Store.ID;
            this.record.Store = this.record.Store;
            this.record.StoreKey = this.record.Store.CustomKey;
            this.record.StoreName = this.record.Store.Name;
            this.forms["Details"].$setDirty();
        }
        userChanged(): void {
            if (this.viewState.running) { return; }
            var isNull = !this.record.User;
            if (isNull) { return; }
            this.record.UserID = this.record.User.ID;
            this.record.User = this.record.User;
            this.record.UserKey = this.record.User.CustomKey;
            this.record.UserUserName = this.record.User.UserName;
            this.forms["Details"].$setDirty();
        }
        vendorChanged(): void {
            if (this.viewState.running) { return; }
            var isNull = !this.record.Vendor;
            if (isNull) { return; }
            this.record.VendorID = this.record.Vendor.ID;
            this.record.Vendor = this.record.Vendor;
            this.record.VendorKey = this.record.Vendor.CustomKey;
            this.record.VendorName = this.record.Vendor.Name;
            this.forms["Details"].$setDirty();
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $state: ng.ui.IStateService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory,
                protected readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("reviewDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/reviewDetail.html", "ui"),
        controller: ReviewDetailController,
        controllerAs: "reviewDetailCtrl"
    }));
}
