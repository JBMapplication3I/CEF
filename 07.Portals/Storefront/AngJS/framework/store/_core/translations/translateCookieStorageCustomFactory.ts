module cef.store.core.translations {
    let translateCookieStorageCustomDefaultOptions: ng.cookies.ICookiesOptions = { };

    export const translateCookieStorageCustomFactoryFn = ($cookies: ng.cookies.ICookiesService) => {
        const delegate = {
            setDefaultOptions : (options: ng.cookies.ICookiesOptions) =>
                translateCookieStorageCustomDefaultOptions = options,
            get: (key: string) => $cookies.get(key),
            put: (key: string, value: string, options: ng.cookies.ICookiesOptions) =>
                $cookies.put(key, value, options || translateCookieStorageCustomDefaultOptions)
        }
        const $translateCookieStorageCustom = {
            /**
             * @ngdoc function
             * @name pascalprecht.translate.$translateCookieStorageCustom#setDefaultOptions
             * @methodOf pascalprecht.translate.$translateCookieStorageCustom
             *
             * @desc
             * Sets the default options for the cookies when not otherwise passed
             *
             * @param {ng.cookies.ICookiesOptions} options The Cookie Options
             */
            setDefaultOptions: (options: ng.cookies.ICookiesOptions) => delegate.setDefaultOptions(options),

            /**
             * @ngdoc function
             * @name pascalprecht.translate.$translateCookieStorageCustom#get
             * @methodOf pascalprecht.translate.$translateCookieStorageCustom
             *
             * @desc
             * Returns an item from cookieStorage by given name.
             *
             * @param {string} name Item name
             * @return {string} Value of item name
             */
            get: (name: string): string => delegate.get(name),

            /**
             * @ngdoc function
             * @name pascalprecht.translate.$translateCookieStorageCustom#set
             * @methodOf pascalprecht.translate.$translateCookieStorageCustom
             *
             * @desc
             * Sets an item in cookieStorage by given name.
             *
             * @deprecated use #put
             *
             * @param {string} name Item name
             * @param {string} value Item value
             * @param {ng.cookies.ICookiesOptions} options The Cookie Options
             */
            set: (name: string, value: string, options: ng.cookies.ICookiesOptions) => delegate.put(name, value, options),

            /**
             * @ngdoc function
             * @name pascalprecht.translate.$translateCookieStorageCustom#put
             * @methodOf pascalprecht.translate.$translateCookieStorageCustom
             *
             * @desc
             * Sets an item in cookieStorage by given name.
             *
             * @param {string} name Item name
             * @param {string} value Item value
             * @param {ng.cookies.ICookiesOptions} options The Cookie Options
             */
            put: (name: string, value: string, options: ng.cookies.ICookiesOptions) => delegate.put(name, value, options)
        };
        return $translateCookieStorageCustom;
    };
}
