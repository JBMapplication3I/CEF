module cef.store.messaging {
    cefApp.directive("contactSellerModalButton", (): ng.IDirective => ({
        restrict: "EA",
        scope: {
            product: "=",
            quantity: "=",
            classes: "@"
        },
        transclude: true, // Required
        // NOTE: This is a control specific template that must remain inline
        template:
`<span ng-disabled="!contactSellerModalButtonCtrl.ready"
        ng-class="{'disabled':contactSellerModalButtonCtrl.ready}"
        ng-click="contactSellerModalButtonCtrl.doModal()"
        login-modal
        ><ng-transclude></ng-transclude></span>`,
        controller: function (cefConfig: core.CefConfig, cvApi: api.ICEFAPI, cvAuthenticationService: services.IAuthenticationService, $uibModal: ng.ui.bootstrap.IModalService, $scope: ng.IScope) {
            this.ready = false;
            this.contacts = [];
            this.subject = null;
            $scope.$watch(() => this.product, (product) => {
                if (product && angular.isArray(product.Stores) && product.Stores.length) {
                    cvApi.stores.GetStoreAdministratorUser(product.Stores[0].StoreID).then((response) => {
                        this.contacts = [response.data.Result.ID];
                        this.subject= product.Name;
                        this.ready= true;
                    });
                }
            });
            this.doModal = function () {
                function modalOpen() {
                    $uibModal.open({
                        // NOTE: This is a control-specific template that must remain inline
                        template: `<cef-contact-message mode="compose" contacts="contactModalCtrl.contacts" subject="contactModalCtrl.subject" cancel-callback="$dismiss()"></cef-contact-message>`,
                        controller: function (contacts, subject) {
                            this.contacts = contacts;
                            this.subject = subject;
                        },
                        controllerAs: "contactModalCtrl",
                        resolve: {
                            contacts: () => this.contacts,
                            subject: () => this.subject
                        }
                    });
                }
                // TODO: Replace with LoginModalService
                if (cvAuthenticationService.isAuthenticated()) {
                    modalOpen.bind(this)();
                    return;
                }
                if ($scope.loginModalCtrl) {
                    $scope.loginModalCtrl.open(loginSuccess => {
                        if (loginSuccess) {
                            modalOpen.bind(this)();
                        }
                    }, "", false, true);
                    return;
                }
                this.$filter("goToCORSLink")("#!?returnUrl=[ReturnUrl]", "login");
            };
        },
        controllerAs: "contactSellerModalButtonCtrl",
        bindToController: true
    }));
}
