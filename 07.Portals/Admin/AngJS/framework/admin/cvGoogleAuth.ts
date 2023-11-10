/*
/// <amd-dependency path="gapi" />

interface GoogleApiOAuth2TokenObject {
    error_subtype: string;
    client_id: string;
    scope: string[];
    response_type: string;
    issued_at: number;
    expires_at: number;
    status: {
        google_logged_in: boolean;
        signed_in: boolean;
        method: any;
    }
}

module cef.admin {
    export interface GoogleAuthConfig {
        clientId: string;
        apiKey: string;
        scopes: string[];
    }

    export class GoogleAuthConfigProvider {
        clientId: string = "578720134496-hluv20kafd5ehifv799si0boi1vpujqc.apps.googleusercontent.com";
        apiKey: string = "AIzaSyDeBEmAkzeXjy6l_jGCqV-wkgsUs7o8I94";
        scopes: string[];

        public $get(): GoogleAuthConfig {
            return {
                clientId: this.clientId,
                apiKey: this.apiKey,
                scopes: this.scopes
            };
        }
    }

    export class GoogleLoginService {
        // Properties
        config: GoogleAuthConfig;
        google_access_token: string = null;
        private authDeferred = this.$q.defer();
        private callbacks = [];
        // Constructors
        constructor(
            private readonly $q: ng.IQService,
            private readonly $location: ng.ILocationService,
            private readonly ngDialog: ng.dialog.IDialogService) {
            this.handleClientLoad();
        }
        // Functions
        registerAuthCallback(callback) {
            this.callbacks.push(callback);
        }
        authorize() {
            this.authDeferred = this.$q.defer();
            gapi.auth.authorize({ client_id: this.config.clientId, scope: this.config.scopes, immediate: false }, this.handleAuthResult);
            return this.authDeferred.promise;
        }
        checkAuth() {
            this.authDeferred = this.$q.defer();
            gapi.auth.authorize({ client_id: this.config.clientId, scope: this.config.scopes, immediate: true }, this.handleAuthResult);
            return this.authDeferred.promise;
        }
        private handleClientLoad() {
            // We can be loaded even when not within a directive
            if (this.config != null) {
                gapi.auth.init(() => { });
                window.setTimeout(() => this.checkAuth(), 1);
            }
        }
        private handleAuthResult(authResult: GoogleApiOAuth2TokenObject) {
            if (authResult && !authResult.error) {
                this.google_access_token = authResult.access_token;
                this.authDeferred.resolve();
                this.callbacks.forEach(c => c(this.authDeferred.promise));
                return;
            }
            this.authDeferred.reject(authResult.error);
            if (!authResult.error) {
                this.callbacks.forEach(c => c(this.authDeferred.promise));
                return;
            }
            if (authResult.error_subtype === "access_denied" || authResult.error === "immediate_failed") {
                // Do nothing - they'll be asked to authorize
                this.callbacks.forEach(c => c(this.authDeferred.promise));
                return;
            }
            let errorMessage = authResult.error_subtype;
            if (errorMessage === "origin_mismatch") {
                errorMessage = `<span data-translate="ui.admin.cvGoogleAuth.ErrorMessage.Part1"></span>${this.$location.host()}<span data-translate="ui.admin.cvGoogleAuth.ErrorMessage.Part2"></span>`;
            }
            this.ngDialog.open({
                plain: true,
                template: `<span class="title" data-translate="ui.admin.cvGoogleAuth.ErrorTitle"></span><br/><span>${errorMessage}</span>`
            });
            console.error(JSON.stringify(authResult));
            this.callbacks.forEach(c => c(this.authDeferred.promise));
        }
    }

    adminApp.service("googleLogin", GoogleLoginService);
    adminApp.provider("googleAuthConfig", GoogleAuthConfigProvider);

    adminApp.directive("googleAuth", (googleLogin: GoogleLoginService, googleAuthConfig: GoogleAuthConfig, cefConfig: core.CefConfig): ng.IDirective => ({
        restrict: "A",
        scope: false,
        link: (scope, element, attributes) => {
            googleLogin.config = _.clone(googleAuthConfig);
            googleLogin.config.clientId = attributes["googleClientId"];
            if (!googleLogin.config.clientId) {
                if (cefConfig.google.apiClientKey) {
                    googleLogin.config.clientId = cefConfig.google.apiClientKey;
                } else {
                    if (googleAuthConfig.clientId) {
                        googleLogin.config.clientId = googleAuthConfig.clientId;
                    } else {
                        throw new Error("Directive google-auth: The attribute 'google-client-id' or setting googleAuthProvider.clientId is required.");
                    }
                }
            }
            googleLogin.config.apiKey = attributes["googleApiKey"];
            if (!googleLogin.config.apiKey) {
                if (cefConfig.google.apiKey) {
                    googleLogin.config.apiKey = cefConfig.google.apiKey;
                } else {
                    if (googleAuthConfig.apiKey) {
                        googleLogin.config.apiKey = googleAuthConfig.apiKey;
                    } else {
                        throw new Error("Directive google-auth: The attribute 'google-api-key' or setting googleAuthProvider.apiKey is required.");
                    }
                }
            }
            googleLogin.config.scopes = eval(attributes["googleScopes"]);
            if (!googleLogin.config.scopes) {
                if (googleAuthConfig.scopes) {
                    googleLogin.config.scopes = googleAuthConfig.scopes;
                } else {
                    throw new Error("Directive google-auth: The attribute 'google-scopes' is required.");
                }
            }
        }
    }));

    adminApp.directive("googleAuthButton", (): ng.IDirective => ({
        restrict: "EA",
        template:
`<button id="gapi-authorize-button" name="gapi-authorize-button"
        ng-disabled="googleAuthButtonCtrl.isAuthorizing"
        ng-click="googleAuthButtonCtrl.click()"
        ng-bind="!googleAuthButtonCtrl.isAuthorizing ? 'Authorize Clarity Ecommerce' : 'Authorizing...'"></button>`,
        controller(googleLogin: GoogleLoginService) {
            this.googleLogin = googleLogin;
            this.isAuthorizing = false;
            this.click = () => {
                this.isAuthorizing = true;
                googleLogin.authorize().then(() => this.isAuthorizing = false);
            };
        },
        controllerAs: "googleAuthButtonCtrl",
        require: "^googleAuth"
    }));
}
*/