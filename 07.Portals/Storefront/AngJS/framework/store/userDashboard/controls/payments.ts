module cef.store.userDashboard.controls {
    export class PaymentsController extends core.TemplatedControllerBase {
        // PaymentsController Properties
        currentUser: api.UserModel;
        userHasPayoneerData = false;
        agreeToPayoneer = false;
        private _providedAccountID: number;
        get providedAccountID(): number { return this._providedAccountID; };
        set providedAccountID(newValue: number) { this._providedAccountID = Number(newValue); };
        private _providedCustomerID: number;
        get providedCustomerID(): number { return this._providedCustomerID; };
        set providedCustomerID(newValue: number) { this._providedCustomerID = Number(newValue); };
        // Functions
        refreshUser() {
            this.setRunning();
            this.cvAuthenticationService.getCurrentUserPromise().then(response => {
                this.currentUser = response;
                if (angular.isDefined(this.currentUser.SerializableAttributes)) {
                    const payoneerSetting = this.currentUser.SerializableAttributes["Payoneer-User-ID"];
                    this.userHasPayoneerData = !angular.isUndefined(payoneerSetting) && Number(payoneerSetting.Value) > 0;
                }
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }

        createPayoneerAccount() {
            this.setRunning();
            this.cvApi.payments.CreateAPayoneerAccountForCurrentUser().then(r => {
                if (!r ||!r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, null, r && r.data && r.data.Messages);
                    return;
                }
                this.refreshUser();
            });
        }

        associateExistingPayoneerAccount() {
            this.setRunning();
            this.currentUser.SerializableAttributes["Payoneer-Account-ID"] = <api.SerializableAttributeObject>{
                ID: 0,
                Key: "Payoneer-Account-ID",
                Group: "Payoneer",
                Value: String(this.providedAccountID),
                ValueType: "System.String",
            };
            this.currentUser.SerializableAttributes["Payoneer-Account-URI"] = <api.SerializableAttributeObject>{
                ID: 0,
                Key: "Payoneer-Account-URI",
                Group: "Payoneer",
                Value: `/accounts/${this.providedAccountID}`,
                ValueType: "System.String",
            };
            this.currentUser.SerializableAttributes["Payoneer-User-ID"] = <api.SerializableAttributeObject>{
                ID: 0,
                Key: "Payoneer-User-ID",
                Group: "Payoneer",
                Value: String(this.providedCustomerID),
                ValueType: "System.String",
            };
            this.currentUser.SerializableAttributes["Payoneer-User-URI"] = <api.SerializableAttributeObject>{
                ID: 0,
                Key: "Payoneer-User-URI",
                Group: "Payoneer",
                Value: `/accounts/${this.providedAccountID}/users/${this.providedCustomerID}`,
                ValueType: "System.String",
            };
            this.cvAuthenticationService.updateCurrentUser(this.currentUser).then(r => {
                if (!r || !r.ActionSucceeded) {
                    this.finishRunning(true, null, r && r.Messages);
                    return;
                }
                this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                    this.currentUser = user;
                    if (!angular.isUndefined(this.currentUser.SerializableAttributes)) {
                        const payoneerSetting = this.currentUser.SerializableAttributes["Payoneer-User-ID"];
                        this.userHasPayoneerData = !angular.isUndefined(payoneerSetting)
                            && Number(payoneerSetting.Value) > 0;
                    }
                    this.finishRunning();
                });
            }, reason => this.finishRunning(true, reason))
            .catch(result => this.finishRunning(true, result));
        }

        managePayoneerBankAccounts() {
            this.setRunning();
            this.cvApi.payments.GetAuthedURLForBuyerToSetUpPaymentAccounts().then(response => {
                if (!response.data.ActionSucceeded) {
                    this.finishRunning(true, null, response.data.Messages);
                    return;
                }
                this.finishRunning();
                // Use the URL returned from the request
                const url = response.data.Result;
                armor.openModal(url, false, null, null);
            }, reason => this.finishRunning(true, reason)).catch(result => this.finishRunning(true, result));
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.cvAuthenticationService.preAuth().finally(() => this.refreshUser());
        }
    }

    cefApp.directive("cefPayments", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/payments.html", "ui"),
        controller: PaymentsController,
        controllerAs: "paymentsCtrl"
    }));
}
