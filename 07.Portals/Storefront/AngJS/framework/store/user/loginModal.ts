/**
 * @desc
 * This directive provides a controller method (loginModalCtrl.open) to the local scope that launches a login dialog box.
 * Pass a callback to the open method to be notified when it is resolved with a true/false value.
 * The second argument is a return url string to be passed to the cef-login return url.
 * The third argument is a boolean for whether or not to reload the page instead of using the returnUrl.
 *
 * Since creating the cvLoginModalFactory, the directive has been altered to use it. Everything should be backwards compatible.
 * A bonus is that a promise is now returned that can be used instead of the callback if you choose.
 */
module cef.store.user {
    cefApp.directive("loginModal", (): ng.IDirective => ({
        restrict: "A",
        controller(cvLoginModalFactory: ILoginModalFactory) {
            this.open = cvLoginModalFactory;
        },
        controllerAs: "loginModalCtrl"
    }));

    export interface ILoginModalFactory {
        /**
         * Calling this generates a modal that inclues the login control
         * @param {Function} [callback=null] - A function to perform on end
         * @param {string} [returnUrl=null] - A url to go to on success
         * @param {boolean} [reloadPage=false] - True to reload the page on success
         * @param {boolean} [noReturn=false] - True to ignore returnUrl if passed
         * @param {string} [size="md"] - The size of the modal: "sm", "md", "lg"
         * @param {boolean} [staticModal=false] - True to not include a background overlay for the page
         * @returns {ng.IPromise<boolean>} A promise to return a boolean that indicates success or failure
         */
        (callback?: (...args: any[]) => void,
         returnUrl?: string,
         reloadPage?: boolean,
         noReturn?: boolean,
         size?: string,
         staticModal?: boolean): ng.IPromise<boolean>;
    }

    class LoginModalWrapperController extends core.TemplatedControllerBase {
        // Properties
        isShaking: boolean;
        // Functions
        confirm(success: boolean): void {
            if (success) {
                this.$uibModalInstance.close(true);
            }
        }
        cancel(): void {
            this.$uibModalInstance.dismiss(false);
        }
        shake(shakes: number, distance: number, duration: number) {
            this.isShaking = true;
            this.$timeout(() => this.isShaking = false, duration);
            /*
            const el = angular.element("div[uib-modal-window] > modal-dialog");
            ////$(el).css("position", "relative");
            for (let x = 1; x <= shakes; x++) {
                el
                    .animate({ left: distance * -1 }, (duration / shakes) / 4)
                    .animate({ left: distance }, (duration / shakes) / 2)
                    .animate({ left: 0 }, (duration / shakes) / 4);
            }
            */
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $timeout: ng.ITimeoutService,
                readonly $scope: ng.IScope,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                // These are passed to the cef-login directive
                private readonly returnUrl: string,
                private readonly reloadPage: boolean,
                private readonly noReturn: boolean) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.auth.signInFailed, () => {
                this.shake(5, 20, 1 * 1000);
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    export const cvLoginModalFactoryFn =
       ($q: ng.IQService,
        $filter: ng.IFilterService,
        $uibModal: ng.ui.bootstrap.IModalService,
        cefConfig: core.CefConfig,
        cvAuthenticationService: services.IAuthenticationService) =>
           (callback: (...args: any[]) => void = null,
            returnUrl: string = null,
            reloadPage: boolean = false,
            noReturn: boolean = false,
            size: string = "md",
            staticModal: boolean = false)
           : ng.IPromise<boolean> =>
               $q((resolve, reject) => cvAuthenticationService.preAuth().finally(() => {
                   if (cvAuthenticationService.isAuthenticated()) {
                       resolve(true);
                       return;
                   }
                   if (cefConfig.authProvider.toLowerCase() === "openid") {
                       const url = `${cefConfig.authProviderAuthorizeUrl
                           }?response_type=code&client_id=${encodeURI(cefConfig.authProviderClientId)
                           }&redirect_uri=${encodeURI(cefConfig.authProviderRedirectUri)
                           }&scope=${encodeURI(cefConfig.authProviderScope)
                           }&nonce=${"N" + Math.random() + "" + Date.now()
                           }&response_mode=form_post&grant_type=authorization_code`;
                       window.location.href = url;
                       return;
                   }
                   $uibModal.open({
                       size: size,
                       backdrop: staticModal ? "static" : true,
                       templateUrl: $filter("corsLink")("/framework/store/user/loginModal.html", "ui"),
                       controller: LoginModalWrapperController,
                       controllerAs: "loginModalWrapperCtrl",
                       resolve: {
                           returnUrl: () => returnUrl,
                           reloadPage: () => reloadPage,
                           noReturn: () => noReturn
                       }
                   }).result.then(loginSuccess => {
                       if (angular.isFunction(callback)) { callback(loginSuccess); }
                       if (!loginSuccess) {
                           reject(loginSuccess);
                           return;
                       }
                       resolve(loginSuccess);
                       if (returnUrl) {
                           $filter("goToCORSLink")(returnUrl);
                       }
                   }).catch(() => {
                       if (angular.isFunction(callback)) { callback(false); }
                   });
               })
           );

    cefApp.factory("cvLoginModalFactory", user.cvLoginModalFactoryFn);
}
