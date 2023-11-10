﻿module cef.store.userDashboard.controls.addressBook {
    class AddressBookDeleteModalController extends core.TemplatedControllerBase {
        // Functions
        ok(): void {
            this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.deleteConfirmed, this.accountContact.ID);
            this.$uibModalInstance.close(true);
        }
        cancel(): void {
            this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.deleteCancelled);
            this.$uibModalInstance.dismiss("cancel");
        }
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                private readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly accountContact: api.AccountContactModel) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefAddressBookDeleteButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            accountContact: "=",
            index: "=?"
        },
        replace: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/addressBook/addressBook.deleteButton.html", "ui"),
        controller($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, cvServiceStrings: services.IServiceStrings) {
            this.open = () => {
                $uibModal.open({
                    size: cvServiceStrings.modalSizes.sm,
                    templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/addressBook/addressBook.delete.html", "ui"),
                    controller: AddressBookDeleteModalController,
                    controllerAs: "addressBookDeleteCtrl",
                    resolve: {
                        accountContact: () => this.accountContact,
                    }
                });
            };
        },
        controllerAs: "addressBookDeleteButtonCtrl",
        bindToController: true
    }));
}
