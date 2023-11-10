/**
 * @file framework/admin/controls/accounts/modals/accountContactDetailModal.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc The Add Account Contact Modal for Administrators
 */
module cef.admin.controls.accounts.modals {
    class AccountContactDetailController {
        accountContact: api.AccountContactModel;
        countries: api.CountryModel[];
        regions: api.RegionModel[];
        // Functions
        private loadCollections() {
            this.cvApi.geography.GetRegions({ Active: true, AsListing: true }).then(r => this.regions = r.data.Results);
            this.cvApi.geography.GetCountries({ Active: true, AsListing: true }).then(r => this.countries = r.data.Results);
            this.accountContact = <api.AccountContactModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                //
                Name: null,
                //
                IsBilling: false,
                IsPrimary: false,
                TransmittedToERP: false,
                //
                MasterID: 0,
                SlaveID: 0,
                Slave: <api.ContactModel> {
                    //
                    ID: 0,
                    Active: true,
                    CreatedDate: new Date(),
                    //
                    NotificationViaEmail: false,
                    NotificationViaSMSPhone: false,
                    Gender: false,
                    SameAsBilling: false,
                    //
                    TypeID: 0,
                    TypeKey: "Address Book",
                    //
                    AddressID: 0,
                    Address: <api.AddressModel>{
                        //
                        ID: 0,
                        Active: true,
                        CreatedDate: new Date(),
                        //
                        Name: null,
                        //
                        CountryID: 0,
                        RegionID: 0,
                        IsBilling: false,
                        IsPrimary: false,
                    }
                }
            }
        }
        save(): void {
            this.accountContacts.push(this.accountContact);
            this.$uibModalInstance.dismiss("cancel");
        }
        close(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                private readonly cvApi: api.ICEFAPI,
                private readonly accountContacts: api.AccountContactModel[]) {
            this.loadCollections();
        }
    }

    adminApp.controller("AccountContactDetailController", AccountContactDetailController);

    adminApp.directive("accountContactDetailModal", ($filter: ng.IFilterService, $uibModal: ng.ui.bootstrap.IModalService): ng.IDirective => ({
        restrict: "EA",
        scope: { accountContacts: "=" },
        replace: true, // Required
        transclude: true, // Required
        template: `<button type="button" ng-click="Show()" class="btn btn-info"><ng-transclude></ng-transclude></button>`,
        link(scope: any) {
            scope.Show = () => {
                $uibModal.open({
                    templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/modals/accountContactDetailModal.html", "ui"),
                    controller: AccountContactDetailController,
                    controllerAs: "contactEditorModalCtrl",
                    size: "lg",
                    resolve: { accountContacts: () => scope.accountContacts }
                });
            };
        }
    }));
}
