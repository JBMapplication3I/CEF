function $translateAsyncPartialLoaderAdmin() {
    'use strict';
    var parts: { [name: string]: cef.admin.core.translations.IPart } = {};

    function hasPart(name: string): boolean {
        return Object.prototype.hasOwnProperty.call(parts, name);
    }

    function isStringValid(str: string): boolean {
        return angular.isString(str) && str !== '';
    }

    function isPartAvailable(name: string): boolean {
        if (!isStringValid(name)) {
            throw new TypeError('Invalid type of a first argument, a non-empty string expected.');
        }
        return (hasPart(name) && parts[name].isActive);
    }

    function deepExtend(dst: any, src: any): any {
        for (var property in src) {
            if (src[property] && src[property].constructor &&
                src[property].constructor === Object) {
                dst[property] = dst[property] || {};
                deepExtend(dst[property], src[property]);
            } else {
                dst[property] = src[property];
            }
        }
        return dst;
    }

    function getPrioritizedParts(): cef.admin.core.translations.IPart[] {
        var prioritizedParts: cef.admin.core.translations.IPart[] = [];
        for (var part in parts) {
            if (parts[part].isActive) {
                prioritizedParts.push(parts[part]);
            }
        }
        prioritizedParts.sort((a, b) => a.priority - b.priority);
        return prioritizedParts;
    }

    /**
     * @ngdoc function
     * @name pascalprecht.translate.$translatePartialLoaderProvider#addPart
     * @methodOf pascalprecht.translate.$translatePartialLoaderProvider
     *
     * @desc
     * Registers a new part of the translation table to be loaded once the
     * `angular-translate` gets into runtime phase. It does not actually load any
     * translation data, but only registers a part to be loaded in the future.
     *
     * @param {string} name A name of the part to add
     * @param {int} [priority=0] Sets the load priority of this part.
     *
     * @returns {object} $translatePartialLoaderProvider, so this method is chainable
     * @throws {TypeError} The method could throw a **TypeError** if you pass the param
     * of the wrong type. Please, note that the `name` param has to be a
     * non-empty **string**.
     */
    this.addPart = function (name: string, priority: number = 0, urlTemplate: string | ng.translate.IUrlTemplateFunc = "lang={lang}&part={part}") {
        if (!isStringValid(name)) {
            throw new TypeError("Couldn't add part, part name has to be a string!");
        }
        if (!hasPart(name)) {
            parts[name] = new cef.admin.core.translations.Part(name, priority, urlTemplate);
        }
        parts[name].isActive = true;
        return this;
    };

    /**
     * @ngdocs function
     * @name pascalprecht.translate.$translatePartialLoaderProvider#setPart
     * @methodOf pascalprecht.translate.$translatePartialLoaderProvider
     *
     * @desc
     * Sets a translation table to the specified part. This method does not make the
     * specified part available, but only avoids loading this part from the server.
     *
     * @param {string} lang A language of the given translation table
     * @param {string} part A name of the target part
     * @param {object} table A translation table to set to the specified part
     *
     * @return {object} $translatePartialLoaderProvider, so this method is chainable
     * @throws {TypeError} The method could throw a **TypeError** if you pass params
     * of the wrong type. Please, note that the `lang` and `part` params have to be a
     * non-empty **string**s and the `table` param has to be an object.
     */
    this.setPart = function (lang: string, part: string, table: ng.translate.ITranslationTable) {
        if (!isStringValid(lang)) {
            throw new TypeError('Couldn\'t set part.`lang` parameter has to be a string!');
        }
        if (!isStringValid(part)) {
            throw new TypeError('Couldn\'t set part.`part` parameter has to be a string!');
        }
        if (typeof table !== 'object' || table === null) {
            throw new TypeError('Couldn\'t set part. `table` parameter has to be an object!');
        }
        if (!hasPart(part)) {
            parts[part] = new cef.admin.core.translations.Part(part);
            parts[part].isActive = false;
        }
        parts[part].tables[lang] = table;
        return this;
    };

    /**
     * @ngdoc function
     * @name pascalprecht.translate.$translatePartialLoaderProvider#deletePart
     * @methodOf pascalprecht.translate.$translatePartialLoaderProvider
     *
     * @desc
     * Removes the previously added part of the translation data. So, `angular-translate` will not
     * load it at the startup.
     *
     * @param {string} name A name of the part to delete
     *
     * @returns {object} $translatePartialLoaderProvider, so this method is chainable
     *
     * @throws {TypeError} The method could throw a **TypeError** if you pass the param of the wrong
     * type. Please, note that the `name` param has to be a non-empty **string**.
     */
    this.deletePart = function (name: string/*, removeData?: boolean*/) {
        if (!isStringValid(name)) {
            throw new TypeError('Couldn\'t delete part, first arg has to be string.');
        }
        if (hasPart(name)) {
            parts[name].isActive = false;
        }
        return this;
    };

    /**
     * @ngdoc function
     * @name pascalprecht.translate.$translatePartialLoaderProvider#isPartAvailable
     * @methodOf pascalprecht.translate.$translatePartialLoaderProvider
     *
     * @desc
     * Checks if the specific part is available. A part becomes available after it was added by the
     * `addPart` method. Available parts would be loaded from the server once the `angular-translate`
     * asks the loader to that.
     *
     * @param {string} name A name of the part to check
     *
     * @returns {boolean} Returns **true** if the part is available now and **false** if not.
     *
     * @throws {TypeError} The method could throw a **TypeError** if you pass the param of the wrong
     * type. Please, note that the `name` param has to be a non-empty **string**.
     */
    this.isPartAvailable = isPartAvailable;

    /**
     * @ngdoc object
     * @name pascalprecht.translate.$translatePartialLoader
     *
     * @requires $q
     * @requires $http
     * @requires $injector
     * @requires $rootScope
     * @requires $translate
     *
     * @desc
     *
     * @param {object} options Options object
     *
     * @throws {TypeError}
     */
    this.$get = function (
        $rootScope: ng.IRootScopeService,
        $injector,
        $q: ng.IQService,
        $filter: ng.IFilterService,
        $http: ng.IHttpService,
        $log: ng.ILogService) {
        /**
         * @ngdoc event
         * @name pascalprecht.translate.$translatePartialLoader#$translatePartialLoaderStructureChanged
         * @eventOf pascalprecht.translate.$translatePartialLoader
         * @eventType broadcast on root scope
         *
         * @desc
         * A $translatePartialLoaderStructureChanged event is called when a state of the loader was
         * changed somehow. It could mean either some part is added or some part is deleted. Anyway when
         * you get this event the translation table is not longer current and has to be updated.
         *
         * @param {string} name A name of the part which is a reason why the event was fired
         */
        const service: any = (options: cef.admin.core.translations.IPartialTranslationLoaderOptions): ng.IPromise<ng.translate.ITranslationTable> => {
            if (!isStringValid(options.key)) {
                throw new TypeError("Unable to load data, a key is not a non-empty string.");
            }
            if (!isStringValid(options.urlTemplate as string) &&
                !angular.isFunction(options.urlTemplate)) {
                throw new TypeError("Unable to load data, a urlTemplate is not a non-empty string or not a function.");
            }
            var errorHandler = options.loadFailureHandler;
            if (errorHandler !== undefined) {
                if (!angular.isString(errorHandler)) {
                    throw new Error("Unable to load data, a loadFailureHandler is not a string.");
                } else {
                    errorHandler = $injector.get(errorHandler);
                }
            }
            var loaders = [],
                prioritizedParts = getPrioritizedParts();
            prioritizedParts.forEach(part => {
                loaders.push(part.getTable(
                    options.key,
                    options["KeyStartsWith"],
                    $q,
                    $filter,
                    $http,
                    options.urlTemplate,
                    errorHandler));
                part.urlTemplate = part.urlTemplate || options.urlTemplate;
            });
            // workaround for #1781
            var structureHasBeenChangedWhileLoading = false;
            var dirtyCheckEventCloser = $rootScope.$on("$translatePartialLoaderStructureChanged",
                () => structureHasBeenChangedWhileLoading = true);
            return $q.all(loaders)
                .then(() => {
                    dirtyCheckEventCloser();
                    if (structureHasBeenChangedWhileLoading) {
                        if (!options.__retries) {
                            // the part structure has been changed while loading (the origin ones)
                            // this can happen if an addPart/removePart has been invoked right after a $translate.use(lang)
                            // TODO maybe we can optimize this with the actual list of missing parts
                            options.__retries = (options.__retries || 0) + 1;
                            return service(options);
                        } else {
                            // the part structure has been changed again while loading (retried one)
                            // because this could an infinite loop, this will not load another one again
                            $log.warn('The partial loader has detected a multiple structure change (with addPort/removePart) ' +
                                'while loading translations. You should consider using promises of $translate.use(lang) and ' +
                                '$translate.refresh(). Also parts should be added/removed right before an explicit refresh ' +
                                'if possible.');
                        }
                    }
                    var table: ng.translate.ITranslationTable = {};
                    prioritizedParts = getPrioritizedParts();
                    prioritizedParts.forEach(part => deepExtend(table, part.tables[options.key]));
                    return table;
                }, function () {
                    dirtyCheckEventCloser();
                    return $q.reject(options.key);
                });
        };
        /**
         * @ngdoc function
         * @name pascalprecht.translate.$translatePartialLoader#addPart
         * @methodOf pascalprecht.translate.$translatePartialLoader
         *
         * @desc
         * Registers a new part of the translation table. This method does not actually perform any xhr
         * requests to get translation data. The new parts will be loaded in order of priority from the server next time
         * `angular-translate` asks the loader to load translations.
         *
         * @param {string} name A name of the part to add
         * @param {int} [priority=0] Sets the load priority of this part.
         *
         * @returns {object} $translatePartialLoader, so this method is chainable
         *
         * @fires {$translatePartialLoaderStructureChanged} The $translatePartialLoaderStructureChanged
         * event would be fired by this method in case the new part affected somehow on the loaders
         * state. This way it means that there are a new translation data available to be loaded from
         * the server.
         *
         * @throws {TypeError} The method could throw a **TypeError** if you pass the param of the wrong
         * type. Please, note that the `name` param has to be a non-empty **string**.
         */
        service.addPart = (name: string, priority: number = 0, urlTemplate: string | ng.translate.IUrlTemplateFunc = "lang={lang}&part={part}") => {
            if (!isStringValid(name)) {
                throw new TypeError("Couldn't add part, first arg has to be a string");
            }
            if (!hasPart(name)) {
                parts[name] = new cef.admin.core.translations.Part(name, priority, urlTemplate);
                $rootScope.$emit("$translatePartialLoaderStructureChanged", name);
            } else if (!parts[name].isActive) {
                parts[name].isActive = true;
                $rootScope.$emit("$translatePartialLoaderStructureChanged", name);
            }
            return service;
        };
        /**
         * @ngdoc function
         * @name pascalprecht.translate.$translatePartialLoader#deletePart
         * @methodOf pascalprecht.translate.$translatePartialLoader
         *
         * @desc
         * Deletes the previously added part of the translation data. The target part could be deleted
         * either logically or physically. When the data is deleted logically it is not actually deleted
         * from the browser, but the loader marks it as not active and prevents it from affecting on the
         * translations. If the deleted in such way part is added again, the loader will use the
         * previously loaded data rather than loading it from the server once more time. But if the data
         * is deleted physically, the loader will completely remove all information about it. So in case
         * of recycling this part will be loaded from the server again.
         *
         * @param {string} name A name of the part to delete
         * @param {boolean=} [removeData=false] An indicator if the loader has to remove a loaded
         * translation data physically. If the `removeData` if set to **false** the loaded data will not be
         * deleted physically and might be reused in the future to prevent an additional xhr requests.
         *
         * @returns {object} $translatePartialLoader, so this method is chainable
         *
         * @fires {$translatePartialLoaderStructureChanged} The $translatePartialLoaderStructureChanged
         * event would be fired by this method in case a part deletion process affects somehow on the
         * loaders state. This way it means that some part of the translation data is now deprecated and
         * the translation table has to be recompiled with the remaining translation parts.
         *
         * @throws {TypeError} The method could throw a **TypeError** if you pass some param of the
         * wrong type. Please, note that the `name` param has to be a non-empty **string** and
         * the `removeData` param has to be either **undefined** or **boolean**.
         */
        service.deletePart = (name: string, removeData: boolean = false): any => {
            if (!isStringValid(name)) {
                throw new TypeError('Couldn\'t delete part, first arg has to be string');
            }
            if (removeData === undefined) {
                removeData = false;
            } else if (typeof removeData !== "boolean") {
                throw new TypeError("Invalid type of a second argument, a boolean expected.");
            }
            if (hasPart(name)) {
                var wasActive = parts[name].isActive;
                if (removeData) {
                    var $translate = $injector.get('$translate');
                    var cache = $translate.loaderCache();
                    if (typeof (cache) === "string") {
                        // getting on-demand instance of loader
                        cache = $injector.get(cache);
                    }
                    // Purging items from cache...
                    if (typeof (cache) === "object") {
                        angular.forEach(parts[name].tables, (_, key) =>
                            cache.remove(parts[name].parseUrl(parts[name].urlTemplate, key)));
                    }
                    delete parts[name];
                } else {
                    parts[name].isActive = false;
                }
                if (wasActive) {
                    $rootScope.$emit("$translatePartialLoaderStructureChanged", name);
                }
            }
            return service;
        };
        /**
         * @ngdoc function
         * @name pascalprecht.translate.$translatePartialLoader#isPartLoaded
         * @methodOf pascalprecht.translate.$translatePartialLoader
         *
         * @desc
         * Checks if the registered translation part is loaded into the translation table.
         *
         * @param {string} name A name of the part
         * @param {string} lang A key of the language
         *
         * @returns {boolean} Returns **true** if the translation of the part is loaded to the translation table and **false** if not.
         *
         * @throws {TypeError} The method could throw a **TypeError** if you pass the param of the wrong
         * type. Please, note that the `name` and `lang` params have to be non-empty **string**.
         */
        service.isPartLoaded = (name: string, lang: string): boolean => {
            return angular.isDefined(parts[name]) && angular.isDefined(parts[name].tables[lang]);
        };
        /**
         * @ngdoc function
         * @name pascalprecht.translate.$translatePartialLoader#getRegisteredParts
         * @methodOf pascalprecht.translate.$translatePartialLoader
         *
         * @desc
         * Gets names of the parts that were added with the `addPart`.
         *
         * @returns {array} Returns array of registered parts, if none were registered then an empty array is returned.
         */
        service.getRegisteredParts = (): string[] => {
            var registeredParts = [];
            angular.forEach(parts, p => {
                if (p.isActive) {
                    registeredParts.push(p.name);
                }
            });
            return registeredParts;
        };
        /**
         * @ngdoc function
         * @name pascalprecht.translate.$translatePartialLoader#isPartAvailable
         * @methodOf pascalprecht.translate.$translatePartialLoader
         *
         * @desc
         * Checks if a target translation part is available. The part becomes available just after it was
         * added by the `addPart` method. Part's availability does not mean that it was loaded from the
         * server, but only that it was added to the loader. The available part might be loaded next
         * time the loader is called.
         *
         * @param {string} name A name of the part to delete
         *
         * @returns {boolean} Returns **true** if the part is available now and **false** if not.
         *
         * @throws {TypeError} The method could throw a **TypeError** if you pass the param of the wrong
         * type. Please, note that the `name` param has to be a non-empty **string**.
         */
        service.isPartAvailable = this.isPartAvailable;
        return service;
    };
    $translateAsyncPartialLoaderAdmin["displayName"] = "$translateAsyncPartialLoaderAdmin";
    return "pascalprecht.translate";
};

angular.module("pascalprecht.translate")
    /**
     * @ngdoc object
     * @name pascalprecht.translate.$translateAsyncPartialLoaderAdmin
     *
     * @desc
     * By using a $translateAsyncPartialLoaderAdmin you can configure a list of a needed
     * translation parts directly during the configuration phase of your application's
     * lifetime. All parts you add by using this provider would be loaded by
     * angular-translate at the startup as soon as possible.
     */
    .provider("$translateAsyncPartialLoaderAdmin", $translateAsyncPartialLoaderAdmin as any);
