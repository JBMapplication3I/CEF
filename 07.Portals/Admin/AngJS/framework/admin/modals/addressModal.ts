/**
 * @file framework/admin/modals/addressModal.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc New address modal class
 */
module cef.admin.modals {
    export class AddressModalController extends widgets.forms.contact.ContactEditorControllerBase {
        // Properties
        currentContact: api.AccountContactModel;
        get contact(): api.ContactModel {
            return this.currentContact.Slave;
        }
        set contact(newValue: api.ContactModel) {
            this.currentContact.Slave = newValue;
        }
        // Functions
        loadAdditional(): void {
            if (this.existing) {
                // Clone by converting to json string and back, this way we can
                // cancel without editing original. Be sure to apply back on save
                // at the time of this modal's result.then(...) call
                this.currentContact = angular.fromJson(angular.toJson(this.existing));
            } else {
                this.newContact();
            }
        }

        newContact(): void {
            this.currentContact = this.cvAddressBookService.blankAccountContactModel();
            this.currentContact.MasterID = this.accountId;
            if (this.isBilling) {
                this.currentContact.IsBilling = true;
            }
            this.contact.TypeKey = "Account Address";
            this.cvCountryService.get({ code: this.cefConfig.countryCode || "USA" }).then(c => {
                if (!c) { return; }
                this.selectCountry(c);
            });
        }

        updateKeys(): void {
            if (!this.showKey && !this.contact.Address.CustomKey) {
                // Generate a key to use
                this.contact.Address.CustomKey = "new-" + api.Guid.create();
            }
            if (!this.contact.Address.CustomKey) {
                this.duplicateKey = false;
                return;
            }
            this.contact.AddressKey
                = this.contact.Address.CustomKey
                = this.contact.Address.CustomKey.toLocaleUpperCase();
            // See if this account has the same key already
            this.cvAddressBookService.getBook(this.accountId).then(book => {
                this.duplicateKey = _.some(book,
                    e => e.Slave.AddressKey === this.contact.AddressKey);
                const msg = "This is a duplicate Address Key.";
                if (!this.viewState.errorMessages) {
                    this.viewState.errorMessages = [];
                }
                if (this.duplicateKey) {
                    this.viewState.hasError = true;
                    if (!_.some(this.viewState.errorMessages, x => x === msg)) {
                        this.viewState.errorMessages.push(msg);
                    }
                    return;
                }
                while (_.some(this.viewState.errorMessages, x => x === msg)) {
                    const index = _.findIndex(this.viewState.errorMessages, msg);
                    this.viewState.errorMessages.splice(index, 1);
                }
                this.viewState.hasError = this.viewState.errorMessages.length > 0;
            });
        }

        ok(): void {
            this.setRunning();
            this.cvApi.providers.ValidateAddress({
                AccountContactID: this.currentContact.ID,
                ContactID: this.currentContact.SlaveID,
                AddressID: this.currentContact.Slave.AddressID,
                Address: this.currentContact.Slave.Address
            }).then(r => {
                if (!r.data.IsValid) {
                    let translated = "Address Validation Failed"; // Fallback value
                    this.$translate("ui.admin.checkout.validateAddress.Failed")
                        .then(t => translated = t)
                        .finally(() => this.cvMessageModalFactory(`<p>${translated}</p><p>${r.data.Message.replace("ResultCode: Error\r\n", "").replace(new RegExp("Error: "), "")}</p>`)
                            .then(() => this.finishRunning(true, null, [translated, r.data.Message.replace("ResultCode: Error\r\n", "").replace(new RegExp("Error: "), "")])));
                    return;
                }
                this.contact.Address = r.data.MergedAddress || r.data.SourceAddress;
                this.setRunning();
                if (this.restrictedShipping && this.currentContact) {
                    this.cvRestrictedRegionCheckService.validate(this.currentContact.Slave).then(restrict => {
                        if (restrict) {
                            this.cvRestrictedRegionCheckService.triggerModal(this.currentContact.Slave);
                        }
                    }).finally(() => this.saveInner(this.currentContact)); // Will eventually call $uibModalInstance.close
                    return;
                }
                this.saveInner(this.currentContact); // Will eventually call $uibModalInstance.close
            }).catch(reason => this.finishRunning(true, reason));
        }

        private saveInner(accountContact: api.AccountContactModel): void {
            (accountContact.ID > 0
                ? this.cvAddressBookService.updateEntry(this.accountId, accountContact)
                : this.cvAddressBookService.addEntry(this.accountId, accountContact)
            ).then(result => {
                if (!result) {
                    this.finishRunning(true);
                    return;
                }
                this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.editSave, {
                    restrictedShipping: this.restrictedShipping
                });
                this.$uibModalInstance.close(accountContact);
            }).catch(reason => this.finishRunning(true, reason));
        }

        cancel(): void {
            this.$uibModalInstance.dismiss();
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                protected readonly $timeout: ng.ITimeoutService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCountryService: services.ICountryService,
                protected readonly cvRegionService: services.IRegionService,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvRestrictedRegionCheckService: services.IRestrictedRegionCheckService,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory,
                private readonly modalTitle: string, // Used by UI
                private readonly buttonTitle: string, // Used by UI
                private readonly idSuffix: string, // Used by UI
                private readonly isBilling: boolean, // Used by UI
                protected readonly existing: api.AccountContactModel,
                private readonly accountId: number,
                private readonly restrictedShipping: boolean) {
            super($timeout, cefConfig, cvApi, cvCountryService, cvRegionService);
            this.load();
        }
    }

    export interface IAddressModalFactory {
        (
            modalTitle: string | ng.IPromise<string>,
            buttonTitle: string | ng.IPromise<string>,
            idSuffix: string,
            isBilling: boolean,
            existing: api.AccountContactModel | null,
            accountId: number,
            restrictedShipping: boolean
        ): ng.IPromise<api.AccountContactModel>;
    }

    export const cvAddressModalFactoryFn = (
            $uibModal: ng.ui.bootstrap.IModalService,
            $q: ng.IQService,
            $filter: ng.IFilterService,
            cvServiceStrings: services.IServiceStrings)
            : IAddressModalFactory =>
        (modalTitle: string | ng.IPromise<string>,
         buttonTitle: string | ng.IPromise<string>,
         idSuffix: string,
         isBilling: boolean,
         existing: api.AccountContactModel | null,
         accountId: number,
         restrictedShipping: boolean)
         : ng.IPromise<api.AccountContactModel> => {
            const modalFunc = (modalTtl: string, buttonTtl: string) => {
                return $uibModal.open({
                    templateUrl: $filter("corsLink")("/framework/admin/modals/addressModal.html", "ui"),
                    size: cvServiceStrings.modalSizes.lg,
                    backdrop: "static",
                    controller: AddressModalController,
                    controllerAs: "contactModalCtrl",
                    resolve: {
                        modalTitle: () => modalTtl,
                        buttonTitle: () => buttonTtl,
                        idSuffix: () => idSuffix,
                        isBilling: () => isBilling,
                        existing: () => existing,
                        accountId: () => accountId,
                        restrictedShipping: () => restrictedShipping
                    }
                }).result.then(result => {
                    return result;
                });
            };
            if (angular.isFunction((modalTitle as ng.IPromise<string>).then)
                && angular.isFunction((buttonTitle as ng.IPromise<string>).then)) {
                let test1 = "Translation 1 Failed",
                    test2 = "Translation 2 Failed";
                return $q((resolve, __) => {
                    $q.when(modalTitle as ng.IPromise<string>).then(translated1 => {
                        test1 = translated1;
                    }).finally(() => {
                        $q.when(buttonTitle as ng.IPromise<string>).then(translated2 => {
                            test2 = translated2;
                        }).finally(() => {
                            resolve(modalFunc(test1, test2));
                        });
                    });
                });
            }
            if (angular.isFunction((modalTitle as ng.IPromise<string>).then)) {
                let test1 = "Translation 1 Failed";
                return $q((resolve, __) => {
                    $q.when(modalTitle as ng.IPromise<string>).then(translated => {
                        test1 = translated;
                    }).finally(() => resolve(modalFunc(test1, buttonTitle as string)));
                });
            }
            if (angular.isFunction((buttonTitle as ng.IPromise<string>).then)) {
                let test2 = "Translation 2 Failed";
                return $q((resolve, __) => {
                    $q.when(buttonTitle as ng.IPromise<string>).then(translated => {
                        test2 = translated;
                    }).finally(() => resolve(modalFunc(modalTitle as string, test2)));
                });
            }
            return modalFunc(modalTitle as string, buttonTitle as string);
        };

    adminApp.factory("cvAddressModalFactory", modals.cvAddressModalFactoryFn);
}
