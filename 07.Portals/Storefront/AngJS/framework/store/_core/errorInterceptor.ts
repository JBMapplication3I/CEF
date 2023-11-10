module cef.store.core {
    export class ErrorInterceptor {
        // Functions
        // NOTE: This must remain an arrow function
        request = (config: ng.IRequestConfig): ng.IRequestConfig => {
            this.$rootScope.loading.loaded = false;
            this.$rootScope.loading.outstandingRequests++;
            return config;
        }
        // NOTE: This must remain an arrow function
        response = (resp) => {
            if (resp) {
                this.$rootScope.loading.loaded = --this.$rootScope.loading.outstandingRequests <= 0;
            }
            return resp || this.$q.when(resp);
        }
        // NOTE: This must remain an arrow function
        responseError = (response: ng.IHttpPromiseCallbackArgShort): ng.IPromise<any> => {
            this.$rootScope.loading.loaded = --this.$rootScope.loading.outstandingRequests <= 0;
            return this.$q.reject(response);
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService) {
            $rootScope.loading = {
                outstandingRequests: 0,
                loaded: true
            };
        }
    }
}
