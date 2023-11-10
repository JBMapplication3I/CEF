module cef.store.memberships {
    class MembershipRegistrationController extends core.TemplatedControllerBase {
        // Properties
        isLoggedIn: boolean;
        isShippingSameAsBilling = true;
        billing: api.AddressModel;
        shipping: api.AddressModel;
        states: api.RegionModel[];
        countries: api.CountryModel[];
        accountID: number;
        createCefUser: boolean;
        returnUrl: string;
        userNameValidityState = "Empty";
        userNameError = "Empty";
        passwordValidityState = "Empty";
        termsAgreementAgreed: boolean;
        currentStep: number;
        showValidateAccountEmailMessage = false;
        paypalCancel = false;
        showDirectoryFields = false;
        membershipTypeName: string;
        newKey: string;
        regionList: api.RegionModel[] = [];
        categories: api.CategoryModel[] = [];
        categoriesGrouped: api.CategoryModel[] = [];
        storeCategory1: api.BrandCategoryModel;
        storeCategory2: api.BrandCategoryModel;
        storeCategory3: api.BrandCategoryModel;
        languageList: api.CategoryModel[] = [];
        usePhonePrefixLookups = false;
        validateAccountData = {
            FirstName: null,
            LastName: null,
            Email: null,
            Membership: null,
            MembershipType: null,
            SellerType: null,
            Token: null,
            ResetToken: null,
            RootURL: null
        };
        registrationData = {
            UserName: null,
            Password: null,
            ResetToken: null,
            FirstName: null,
            LastName: null,
            CompanyName: null,
            Phone: null,
            Email: null,
            RoleName: null,
            TypeName: null,
            ProfileType: null,
            Website: null,
            StoreCategories: [],
            StoreContacts: [],
            Address: <api.AddressModel>{
                ID: 0,
                CreatedDate: new Date(),
                Active: true,
                Name: null,
                Street1: null,
                Street2: null,
                City: null,
                RegionID: null,
                CountryID: null,
                PostalCode: null,
                IsBilling: true,
                IsPrimary: false
            }
        };

        initialize(): void {
            this.setRunning();
            if (this.$scope.membershipTypeName) {
                this.membershipTypeName = this.$scope.membershipTypeName;
                this.showDirectoryFields = this.membershipTypeName !== "default";
            }
            this.$scope.$watch("usePhonePrefixLookups",
                (newValue: boolean | string) => this.usePhonePrefixLookups = Boolean(newValue));
            this.currentStep = 0;
            this.paypalCancel = false;
            // When coming from anywhere, check for these values (we'll work with them some more later)
            const email = this.$location.search()["email"];
            const membership = this.$location.search()["membership"];
            // When coming back from PayPal we must restore these values
            const token = this.$location.search()["token"];
            const payerID = this.$location.search()["PayerID"];
            this.validateAccountData.SellerType = this.$location.search()["type"];
            this.validateAccountData.MembershipType = this.$location.search()["mtype"];
            this.validateAccountData.Membership = membership;
            if (token && payerID) {
                /*
                this.cvApi.providers.CompletePayPalForCurrentCart({ Token: token, PayerID: payerID }).then(() => {
                    this.registrationData = this.$cookies.getObject("regValues");
                    this.registrationData.ResetToken = this.$location.search()["reset-token"];
                    this.finishRegistration();
                });
                */
                return;
            } else if (token && !payerID) {
                this.registrationData = this.$cookies.getObject("regValues");
                this.registrationData.ResetToken = this.$location.search()["reset-token"];
                this.validateAccountData.Membership = membership;
                this.currentStep = 3;
                this.load();
                this.paypalCancel = true;
                this.finishRunning(true, this.$translate("ui.storefront.errors.checkout.paypal.CanceledOrUnsuccessful"));
                return;
            }
            // Validate the Incoming Email
            if (!email) {
                // Must provide the token from the invitation
                this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.MissingEmail"));
                return; // Must select a Seller Type
            }
            this.validateAccountData.Email = email;
            this.registrationData.Email = email;
            // Validate the incoming seller type
            const sellerType = this.$location.search()["type"];
            if (!sellerType) {
                // Must provide the token from the invitation
                this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.InvalidSellerType"));
                return; // Must select a Seller Type
            }
            this.validateAccountData.SellerType = sellerType;
            this.registrationData.TypeName = sellerType;
            // Validate the incoming Membership Level
            if (!membership) {
                this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.InvalidMembershipSelection"));
                return; // Must select a Membership Type
            }
            this.validateAccountData.Membership = membership;
            // Validate the incoming token
            const vtoken = this.$location.search()["vtoken"];
            if (!vtoken) {
                // Must provide the token from the invitation
                this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.InvalidToken"));
                return;
            }
            this.validateAccountData.Token = vtoken;
            const mtype = this.$location.search()["mtype"];
            if (!mtype && !this.membershipTypeName) {
                // Must provide the membership type name from the invitation
                this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.InvalidMembershipType"));
                return;
            }
            if (mtype) {
                this.membershipTypeName = mtype;
                this.showDirectoryFields = this.membershipTypeName.toLowerCase() !== "default";
            }
            this.validateAccountData.MembershipType = this.membershipTypeName;
            // Paypal token
            this.cvApi.authentication.ValidateInvitation({
                Email: this.validateAccountData.Email,
                Token: this.validateAccountData.Token
            }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.InvalidInvitationToken"));
                    return;
                }
                this.validateAccountData.Token = vtoken;
                // Validate the incoming reset token if set
                const resetToken = this.$location.search()["reset-token"];
                if (!resetToken) {
                    // Not Set, stay on step 1 and do load
                    this.currentStep = 1;
                    this.load();
                    return;
                }
                // Reset Token is available, need to switch to step 2
                this.validateAccountData.ResetToken = resetToken;
                this.registrationData.ResetToken = resetToken;
                this.currentStep = 2;
                this.load();
            }).catch(() => {
                this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.InvalidInvitationToken"));
            });
            this.validateAccountData.Email = (this.validateAccountData.Email.toLowerCase() === "signup") ? "" : this.validateAccountData.Email;
            // Read Invite Token and ready step 1
        }

        getStates(): void {
            if (this.registrationData && this.registrationData.Address && this.registrationData.Address.CountryID) {
                this.cvRegionService.search({
                    Active: true,
                    AsListing: true,
                    Sorts: [{ field: "Name", order: 0, dir: "asc" }],
                    CountryID: this.registrationData.Address.CountryID
                }).then(r => {
                    this.states = r;
                    this.regionList = r.length > 0
                        ? r.filter(el => (el.CustomKey ? (el.CustomKey.toLowerCase().indexOf("region") > -1) : null))
                        : null;
                    // Set all Regions in regionList to active == false
                    angular.forEach(this.regionList, (value) => { value.Active = false; });
                });
            } else {
                this.states = undefined;
            }
        }

        load() {
            this.validateAccountData.Email = this.validateAccountData.Email.toLowerCase() === "signup"
                ? ""
                : this.validateAccountData.Email;
            this.cvCountryService.search({ Active: true, AsListing: true, Sorts: [{ field: "Name", order: 0, dir: "asc" }] }).then(c => this.countries = c);
            this.cvApi.categories.GetCategories({
                Active: true,
                AsListing: true,
                IncludeChildrenInResults: false
            }).then(r => {
                this.categories = r.data.Results;
                this.categories = this.categories.length > 0
                    ? this.categories.filter(el => (el.TypeID ? (el.TypeID === 1002) : null))
                    : null;
                this.categoriesGrouped = this.categories.length > 0
                    ? this.categories.filter(el => (el.TypeID ? (typeof el.ParentID !== "undefined") : null))
                    : null;
                this.getCategoriesGrouped();
                this.languageList = r.data.Results;
                this.languageList = this.languageList.length > 0
                    ? this.languageList.filter(el => (el.TypeID ? (el.TypeID === 2) : null))
                    : null;
                this.languageList.forEach(
                    (__, index) => this.languageList[index].Active = false);
            });
            this.termsAgreementAgreed = false;
        }

        getCategoriesGrouped() {
            this.categoriesGrouped.forEach((el, index) => {
                if (angular.isUndefined(el.ParentID) || el.ParentID == null) { return; }
                const parent = _.find(this.categories, cat => cat.ID === el.ParentID);
                this.categoriesGrouped[index].Parent = parent ? parent : null;
            });
        }

        runPhonePrefixLookup(): void {
            if (!this.usePhonePrefixLookups // Don't use this
                || !this.registrationData.Phone // Must have content
                || /\d+/.exec(this.registrationData.Phone).length <= 0 // Must have a number in the value
            ) { return; }
            this.cvApi.geography.ReversePhonePrefixToCityRegionCountry({
                Prefix: `+${this.registrationData.Phone.trim().replace(/[a-zA-Z\s)(+_-]+/, "")}`
            }).then(r => {
                if (!r || !r.data || !r.data.Results) {
                    return;
                }
                if (r.data.Results.length <= 0) {
                    return;
                }
                this.registrationData.Address.CountryID = r.data.Results[0].CountryID;
                this.registrationData.Address.RegionID = r.data.Results[0].RegionID;
                this.registrationData.Address.City = r.data.Results[0].CityName;
            });
        }

        sendValidateAccountEmail(): void {
            this.setRunning();
            this.validateAccountData.RootURL = location.origin ? location.origin : location.protocol + "//" + location.host;
            this.cvApi.authentication.CreateLiteAccountAndSendValidationEmail(this.validateAccountData)
                .then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.ErrorSendingValidationEmail"));
                        return;
                    }
                    this.finishRunning();
                    this.showValidateAccountEmailMessage = true;
                }).catch(() => {
                    this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.ErrorSendingValidationEmail"));
                });
        }

        validateAccountTokens(): void {
            this.setRunning();
            this.cvApi.authentication.ValidateInvitation({ Email: this.validateAccountData.Email, Token: this.validateAccountData.Token })
                .then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.InvalidInvitationToken"));
                        return;
                    }
                    this.finishRunning();
                    // Switch to step 3
                    this.currentStep = 3;
                }).catch(() => {
                    this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.InvalidInvitationToken"));
                });
        }

        userNameChanged(): void {
            if (!this.registrationData || !this.registrationData.UserName || this.registrationData.UserName === "") {
                this.userNameValidityState = "Empty"; // NOTE: These string values are more of an Enum than text to be shown
                return;
            }
            if (this.registrationData.UserName.length < 5) {
                this.userNameValidityState = "Invalid";
                return;
            }
            this.cvApi.authentication.ValidateUserNameIsGood({ UserName: this.registrationData.UserName }).success(result => {
                if (!result || !result.ActionSucceeded) {
                    this.userNameValidityState = "Invalid";
                    this.userNameError = result.Messages[0];
                } else {
                    this.userNameValidityState = "Valid";
                    this.userNameError = "";
                    return;
                }
            });
        }

        passwordChanged(): void {
            if (!this.registrationData || !this.registrationData.Password || this.registrationData.Password === "") {
                this.passwordValidityState = "Empty"; // NOTE: These string values are more of an Enum than text to be shown
                return;
            }
            if (this.registrationData.Password.length < 7) {
                this.passwordValidityState = "Invalid";
                return;
            }
            // One Upper Check
            if (!/.*[A-Z].*/.test(this.registrationData.Password)) {
                this.passwordValidityState = "Invalid";
                return;
            }
            // One Lower Check
            if (!/.*[a-z].*/.test(this.registrationData.Password)) {
                this.passwordValidityState = "Invalid";
                return;
            }
            // One Number Check
            if (!/.*\d.*/.test(this.registrationData.Password)) {
                this.passwordValidityState = "Invalid";
                return;
            }
            this.passwordValidityState = "Valid";
        }

        assignRoles(): void {
            switch (this.validateAccountData.Membership.toLowerCase()) {
                case "bronze":
                    this.registrationData.RoleName = "Bronze";
                    this.registrationData.ProfileType = "Store";
                    break;
                case "silver":
                    this.registrationData.RoleName = "Silver";
                    this.registrationData.ProfileType = "Store";
                    break;
                case "gold":
                    this.registrationData.RoleName = "Gold";
                    this.registrationData.ProfileType = "Store";
                    break;
                case "basicdirectory":
                    this.registrationData.RoleName = "Directory Free";
                    this.registrationData.ProfileType = "Directory Free";
                    break;
                case "premiumdirectory":
                    this.registrationData.RoleName = "Directory Premium";
                    this.registrationData.ProfileType = "Directory Premium";
                    break;
                default:
                    this.registrationData.RoleName = "Directory Free";
                    this.registrationData.ProfileType = "Directory Free";
                    break;
            }
        }

        getLanguages(): void {
            // Loop through checked languageList records and ensure they're added as StoreCategory entries
            angular.forEach(this.languageList, item => {
                if (item.Active) {
                    // Add Category to StoreCategory List
                    this.registrationData.StoreCategories.push(this.newStoreCategory(item.ID));
                }
            });
        }

        getStoreContacts(): void {
            // Loop through checked regionsList records and ensure they're added as accountAddress entries
            angular.forEach(this.regionList, item => {
                if (!item.Active) { return; }
                // Add Address to AccountAddress List
                this.registrationData.StoreContacts.push(this.newStoreContact(item.ID));
            });
        }

        getStoreCategories(): void {
            if (this.storeCategory1) {
                this.storeCategory1.CustomKey = "StoreCategory1";
                this.registrationData.StoreCategories.push(this.storeCategory1);
            }
            if (this.storeCategory2) {
                this.storeCategory2.CustomKey = "StoreCategory2";
                this.registrationData.StoreCategories.push(this.storeCategory2);
            }
            if (this.storeCategory3) {
                this.storeCategory3.CustomKey = "StoreCategory3";
                this.registrationData.StoreCategories.push(this.storeCategory3);
            }
        }

        newStoreCategory(categoryID: number): api.BrandCategoryModel {
            return <api.BrandCategoryModel>{
                ID: 0,
                CustomKey: "LANGUAGE",
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SlaveID: categoryID,
            };
        }

        newStoreContact(regionID: number): api.StoreContactModel {
            return <api.StoreContactModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SlaveID: null,
                Slave: this.newContactGen(regionID)
            };
        }

        newContactGen(regionID: number): api.ContactModel {
            this.newKeyGen(`Contact-${regionID.toString()}`);
            return <api.ContactModel>{
                ID: 0,
                CreatedDate: new Date(),
                UpdatedDate: null,
                NotificationViaEmail: false,
                NotificationViaSMSPhone: false,
                Gender: null,
                SameAsBilling: true,
                Active: true,
                TypeID: 1004,
                Address: <api.AddressModel>{
                    ID: 0,
                    Active: true,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    CustomKey: this.newKey,
                    IsBilling: false,
                    IsPrimary: false,
                    RegionID: regionID
                }
            };
        }

        newKeyGen(keyType: string): void {
            this.newKey = `Store-${keyType}-${core.Utility.newGuid()}`;
        }

        finishRegistration() {
            this.registrationData.FirstName = this.registrationData.FirstName ? this.registrationData.FirstName : this.validateAccountData.FirstName;
            this.registrationData.LastName = this.registrationData.LastName ? this.registrationData.LastName : this.validateAccountData.LastName;
            this.registrationData.Email = this.registrationData.Email ? this.registrationData.Email : this.validateAccountData.Email;
            this.registrationData.Address.Company = this.registrationData.CompanyName;
            this.registrationData.ResetToken = this.registrationData.ResetToken ? this.registrationData.ResetToken : this.validateAccountData.ResetToken;
            this.registrationData.TypeName = this.registrationData.TypeName ? this.registrationData.TypeName : this.validateAccountData.SellerType;
            // Assign User Role(s), Store Contacts and Store Categories
            this.assignRoles();
            this.getLanguages();
            this.getStoreContacts();
            this.getStoreCategories();
            this.cvApi.authentication.CompleteRegistration(this.registrationData)
                .then(response => {
                    if (!response || !response.data || !response.data.ActionSucceeded) {
                        this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.GeneralError"));
                        return;
                    }
                    this.finishRunning();
                    this.currentStep = 4;
                }).catch(() => {
                    this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.GeneralError"));
                });
        }

        payPalCancelCheckout() {
            this.setRunning();
            this.cvApi.contacts.GetUserByKey(this.registrationData.Email).then(response => {
                const user = response.data;
                const billing = <api.ContactModel>{
                    ID: 0,
                    Active: true,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    FirstName: user.Contact.FirstName,
                    LastName: user.Contact.LastName,
                    Email1: this.registrationData.Email,
                    NotificationViaEmail: false,
                    NotificationViaSMSPhone: false,
                    Gender: null,
                    SameAsBilling: true,
                    TypeID: 0
                };
                const checkoutData = <api.CheckoutModel>{
                    Active: false,
                    Billing: billing,
                    Shipping: billing,
                    IsNewAccount: false,
                    IsSameAsBilling: true,
                    TestMode: true,
                    CreatedDate: null,
                    UpdatedDate: null,
                    ExternalUserID: this.registrationData.Email,
                    IsPartialPayment: false,
                    PayByPayPal: { },
                    PayByBillMeLater: { },
                };
                checkoutData.PayByPayPal.ReturnUrl = this.$filter("corsLink")("/", "membershipRegistration", null, false, {
                    "email": this.registrationData.Email,
                    "type": this.validateAccountData.SellerType,
                    "membership": this.validateAccountData.Membership,
                    "mtype": this.validateAccountData.MembershipType,
                    "reset-token": this.validateAccountData.ResetToken
                });
                checkoutData.PayByPayPal.CancelUrl = this.$filter("corsLink")("/", "membershipRegistration", null, false, {
                    "email": this.registrationData.Email,
                    "type": this.validateAccountData.SellerType,
                    "membership": this.validateAccountData.Membership,
                    "mtype": this.validateAccountData.MembershipType,
                    "reset-token": this.validateAccountData.ResetToken,
                    "cancel-paypal": true
                });
                /*
                this.cvApi.providers.InitiatePayPalForCurrentCart(checkoutData).then(r2 => {
                    if (!r2.data || !r2.data.Token)  {
                        this.finishRunning(true, this.$translate("ui.storefront.errors.checkout.paypal.NoTokenReturned"));
                        return;
                    }
                    if (!r2.data.Token.startsWith("http")) {
                        this.finishRunning(true, this.$translate("ui.storefront.errors.checkout.paypal.InvalidTokenReturned"));
                        return;
                    }
                    window.location.href = r2.data.Token;
                });
                */
            });
        }

        private doError(response: ng.IHttpPromiseCallbackArg<api.CEFActionResponse> | any) {
            this.finishRunning(true, null, response.data.Messages);
        }

        checkout(): void {
            this.setRunning();
            this.$cookies.putObject("regValues", this.registrationData, <ng.cookies.ICookiesOptions>{
                path: "/",
                expires: "Session",
                domain: this.cefConfig.useSubDomainForCookies || !this.subdomain
                    ? this.$location.host()
                    : this.$location.host().replace(this.subdomain, "")
            });
            this.cvAuthenticationService.forgotPasswordReturn({ Email: this.registrationData.Email, Password: this.registrationData.Password, Token: this.registrationData.ResetToken }).then(r1 => {
                if (!r1 || !r1.data || !r1.data.ActionSucceeded) {
                    this.doError(r1);
                    return;
                }
                this.cvAuthenticationService.login({ Username: r1.data.Result as string, Password: this.registrationData.Password }).finally(() => {
                    if (!this.cvAuthenticationService.isAuthenticated()) {
                        this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.CouldntSignInWithNew"));
                        return;
                    }
                    // Now that we are logged in
                    this.cvAuthenticationService.getCurrentUserPromise(true).then(user => {
                        if (!user) {
                            this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.CouldntReadNewSignInUserValues"));
                            return;
                        }
                        this.cvApi.products.CheckProductExistsByKey(this.validateAccountData.Membership).then(r2 => {
                            const productID = r2.data as number;
                            if (!productID) {
                                this.finishRunning(true, this.$translate("ui.storefront.errors.membershipRegistration.CouldntLocateMembershipProduct"));
                                return;
                            }
                            this.cvApi.shopping.AddCartItem({
                                ProductID: productID,
                                Quantity: 1,
                            }).then(r => {
                                if (!r || !r.data || !r.data.ActionSucceeded || !r.data.Result) {
                                    // Couldn't add the item
                                    return;
                                }
                                const address = <api.AddressModel>{
                                    ID: 0,
                                    Street1: this.registrationData.Address.Street1,
                                    Street2: this.registrationData.Address.Street2,
                                    City: this.registrationData.Address.City,
                                    RegionID: this.registrationData.Address.RegionID,
                                    CountryID: this.registrationData.Address.CountryID,
                                    IsBilling: true,
                                    Active: false,
                                    IsPrimary: true,
                                    CreatedDate: null,
                                    UpdatedDate: null
                                };
                                const billing = <api.ContactModel>(({
                                    FirstName: user.Contact.FirstName,
                                    LastName: user.Contact.LastName,
                                    Email1: this.registrationData.Email,
                                    Address: address,
                                    NotificationViaEmail: false,
                                    NotificationViaSMSPhone: false,
                                    Gender: null,
                                    SameAsBilling: true,
                                    Active: true,
                                    CreatedDate: null,
                                    UpdatedDate: null
                                }) as any);
                                const checkoutData = <api.CheckoutModel>{
                                    Active: false,
                                    Billing: billing,
                                    Shipping: billing,
                                    IsNewAccount: false,
                                    IsSameAsBilling: true,
                                    TestMode: true,
                                    CreatedDate: null,
                                    UpdatedDate: null,
                                    ExternalUserID: this.registrationData.Email,
                                    IsPartialPayment: false
                                };
                                this.cvApi.shopping.GetCartItemByID(r.data.Result).then(r2 => {
                                    if (!r2.data.UnitCorePrice) {
                                        checkoutData.PayByBillMeLater.CustomerPurchaseOrderNumber = "Membership";
                                        let promise: (routeParams: api.ProcessCurrentCartToSingleOrderDto | api.ProcessCurrentCartToTargetOrdersDto) => ng.IHttpPromise<api.CheckoutResult> = null;
                                        if (!this.cefConfig.checkout) {
                                            throw new Error("ERROR! No checkout config set up, please contact a Dev for support.");
                                        }
                                        if (this.cefConfig.featureSet.shipping.splitShipping.enabled) {
                                            promise = this.cvApi.providers.ProcessCurrentCartToTargetOrders;
                                        } else {
                                            promise = this.cvApi.providers.ProcessCurrentCartToSingleOrder;
                                        }
                                        promise(checkoutData).then(() => this.finishRegistration());
                                        return;
                                    }
                                    checkoutData.PayByPayPal.ReturnUrl = this.$filter("corsLink")("/", "membershipRegistration", null, false, {
                                        "email": this.registrationData.Email,
                                        "type": this.validateAccountData.SellerType,
                                        "membership": this.validateAccountData.Membership,
                                        "mtype": this.validateAccountData.MembershipType,
                                        "reset-token": this.validateAccountData.ResetToken
                                    });
                                    checkoutData.PayByPayPal.CancelUrl = this.$filter("corsLink")("/", "membershipRegistration", null, false, {
                                        "email": this.registrationData.Email,
                                        "type": this.validateAccountData.SellerType,
                                        "membership": this.validateAccountData.Membership,
                                        "mtype": this.validateAccountData.MembershipType,
                                        "reset-token": this.validateAccountData.ResetToken,
                                        "cancel-paypal": true
                                    });
                                    /*
                                    this.cvApi.providers.InitiatePayPalForCurrentCart(checkoutData).then(r3 => {
                                        if (!r3.data || !r3.data.Token) {
                                            this.finishRunning(true, this.$translate("ui.storefront.errors.checkout.paypal.NoTokenReturned"));
                                            return;
                                        }
                                        if (!r3.data.Token.startsWith("http")) {
                                            this.finishRunning(true, this.$translate("ui.storefront.errors.checkout.paypal.InvalidTokenReturned"));
                                            return;
                                        }
                                        window.location.href = r3.data.Token;
                                    });
                                    */
                                });
                            });
                        });
                    });
                });
            }, result => this.doError(result)).catch(reason => this.doError(reason));
        }

        constructor(
                private readonly $scope: ng.IScope,
                private readonly $location: ng.ILocationService,
                private readonly $filter: ng.IFilterService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $cookies: ng.cookies.ICookiesService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvCountryService: services.ICountryService,
                private readonly cvRegionService: services.IRegionService,
                private readonly subdomain: string) {
            super(cefConfig);
            this.initialize();
        }
    }

    cefApp.directive("cefMembershipRegistration", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { membershipTypeName: "@", usePhonePrefixLookups: "@?" },
        templateUrl: $filter("corsLink")("/framework/store/memberships/membershipRegistration.html", "ui"),
        controller: MembershipRegistrationController,
        controllerAs: "membershipRegistrationCtrl"
    }));
}
