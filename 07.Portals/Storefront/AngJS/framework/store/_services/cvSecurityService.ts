/**
 * @file framework/store/_services/cvSecurityService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Security Service, reads roles and permission data from the server
 * and stores results in temporary cookies for the current user
 */
module cef.store.services {
    export interface ISecurityService {
        hasRolePromise: (name: string) => ng.IPromise<boolean>;
        hasPermissionPromise: (name: string) => ng.IPromise<boolean>;
        /**
         * @warning This will return false until a separate promise can finish and
         * apply the cookie. It's much safer to use {@see hasRolePromise} instead
         */
        hasRole: (name: string) => boolean;
        /**
         * @warning This will return false until a separate promise can finish and
         * apply the cookie. It's much safer to use {@see hasPermissionPromise} instead
         */
        hasPermission: (name: string) => boolean;
    }

    export class SecurityService implements ISecurityService {
        // Properties
        private readonly getPromiseInstance: { [key: string]: ng.IPromise<boolean>; } = { };
        private readonly strings = {
            hasRolePrefix: "cef_hr_",
            hasAnyRolePrefix: "cef_har_",
            hasPermissionPrefix: "cef_hp_",
            hasAnyPermissionPrefix: "cef_hap_",
        };
        // Functions
        hasRolePromise(name: string): ng.IPromise<boolean> {
            if (!name || !this.cvAuthenticationService.isAuthenticated()) {
                return this.$q.resolve(false);
            }
            const fullName = this.getFullName(name, true);
            if (this.hasRoleCookie(fullName)) {
                return this.$q.resolve(this.getRoleCookie(fullName) === "1");
            }
            if (this.getPromiseInstance[fullName]) {
                return this.getPromiseInstance[fullName];
            }
            return (this.getPromiseInstance[fullName] = this.$q((resolve, reject) => {
                this.hasRoleInner(name)
                    .then(() => resolve(this.hasRole(name)), reject)
                    .catch(reject)
                    .finally(() => delete this.getPromiseInstance[fullName]);
            }));
        }
        hasPermissionPromise(name: string): ng.IPromise<boolean> {
            if (!name || !this.cvAuthenticationService.isAuthenticated()) {
                return this.$q.resolve(false);
            }
            const fullName = this.getFullName(name, false);
            if (this.hasPermissionCookie(fullName)) {
                return this.$q.resolve(this.getPermissionCookie(fullName) === "1");
            }
            if (this.getPromiseInstance[fullName]) {
                return this.getPromiseInstance[fullName];
            }
            return (this.getPromiseInstance[fullName] = this.$q((resolve, reject) => {
                this.hasPermissionInner(name)
                    .then(() => resolve(this.hasPermission(name)), reject)
                    .catch(reject)
                    .finally(() => delete this.getPromiseInstance[fullName]);
            }));
        }
        /**
         * @warning This will return false until a separate promise can finish and
         * apply the cookie. It's much safer to use {@see hasRolePromise} instead
         */
        hasRole(name: string): boolean {
            if (!name || !this.cvAuthenticationService.isAuthenticated()) {
                return false;
            }
            const fullName = this.getFullName(name, true);
            if (this.hasRoleCookie(fullName)) {
                return this.getRoleCookie(fullName) === "1";
            }
            this.hasRolePromise(name);
            return false;
        }
        /**
         * @warning This will return false until a separate promise can finish and
         * apply the cookie. It's much safer to use {@see hasPermissionPromise} instead
         */
        hasPermission(name: string): boolean {
            if (!name || !this.cvAuthenticationService.isAuthenticated()) {
                return false;
            }
            const fullName = this.getFullName(name, false);
            if (this.hasPermissionCookie(fullName)) {
                return this.getPermissionCookie(fullName) === "1";
            }
            this.hasPermissionPromise(name);
            return false;
        }
        private hasRoleInner(name: string): ng.IPromise<void> {
            return this.$q((resolve, _) => {
                (name.indexOf("/") >= 0
                    ? this.cvApi.authentication.CurrentUserHasAnyRole({ Regex: name })
                    : this.cvApi.authentication.CurrentUserHasRole({ Name: name }))
                .finally(() => resolve());
            });
        }
        private hasPermissionInner(name: string): ng.IPromise<void> {
            return this.$q((resolve, _) => {
                (name.indexOf("/") >= 0
                    ? this.cvApi.authentication.CurrentUserHasAnyPermission({ Regex: name })
                    : this.cvApi.authentication.CurrentUserHasPermission({ Name: name }))
                .finally(() => resolve());
            });
        }
        private hasRoleCookie(fullName: string): boolean {
            return this.getRoleCookie(fullName) !== "-1";
        }
        private hasPermissionCookie(fullName: string): boolean {
            return this.getPermissionCookie(fullName) !== "-1";
        }
        private getRoleCookie(fullName: string): string {
            return this.$cookies.get(fullName) || "-1";
        }
        private getPermissionCookie(fullName: string): string {
            return this.$cookies.get(fullName) || "-1";
        }
        private getFullName(name: string, isRole: boolean): string {
            return (isRole
                    ? name.indexOf("/") >= 0 ? this.strings.hasAnyRolePrefix : this.strings.hasRolePrefix
                    : name.indexOf("/") >= 0 ? this.strings.hasAnyPermissionPrefix : this.strings.hasPermissionPrefix)
                + name;
        }
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $cookies: ng.cookies.ICookiesService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            $rootScope.cvSecurityService = this;
        }
    }
}
