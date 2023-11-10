module cef.admin.core.translations {
    export class Part implements IPart {
        name: string;
        isActive: boolean;
        priority: number;
        tables: { [lang: string]: ng.translate.ITranslationTable };
        langPromises: { [lang: string]: ng.IPromise<ng.translate.ITranslationTable> };
        urlTemplate: string | ng.translate.IUrlTemplateFunc;

        parseUrl(urlTemplate: string | ng.translate.IUrlTemplateFunc, targetLang: string): string {
            if (angular.isFunction(urlTemplate)) {
                return (urlTemplate as ng.translate.IUrlTemplateFunc)(this.name, targetLang);
            }
            return (urlTemplate as string).replace(/\{part\}/g, this.name).replace(/\{lang\}/g, targetLang);
        }

        getTable(
                lang: string,
                keyStartsWith: string,
                $q: ng.IQService,
                $filter: ng.IFilterService,
                $http: ng.IHttpService,
                urlTemplate: string | ng.translate.IUrlTemplateFunc,
                errorHandler: (name: string, lang: string, errorRespone: any) => ng.IPromise<any>
                ): ng.IPromise<ng.translate.ITranslationTable> {
            // locals
            const self = this;
            const lastLangPromise = this.langPromises[lang];
            const deferred = $q.defer<ng.translate.ITranslationTable>();
            // private helper helpers
            //const key = self.parseUrl(self.urlTemplate || urlTemplate, lang);
            const fetchData = (): ng.IPromise<ng.translate.ITranslationTable> =>
                $q((resolve, reject) => $http.get<cefalt.admin.Dictionary<string>>(
                    $filter("corsLink")("/lib/cef/js/i18n/" + keyStartsWith + lang + ".json", "ui"),
                    <ng.IRequestShortcutConfig>{
                        responseType: "json"
                    }).then(r => {
                        if (!r || !r.data/* || !r.data[lang.replace(/"/, "")-]*/
                            || !Object.keys(r.data/*[lang.replace(/"/, "")]*/).length) {
                            reject("Error pulling translations dictionary");
                            return;
                        }
                        resolve(r.data);
                    }).catch(reason => reject(`${lang/*key*/.replace(/"/, "")} ${reason}`)));
            /*const fetchData = (): ng.IHttpPromise<ng.translate.ITranslationTable> => $http(
                angular.extend({
                    method: "GET",
                    url: self.parseUrl(self.urlTemplate || urlTemplate, lang)
                },
                $httpOptions));*/
            // private helper
            const handleNewData = (data: ng.translate.ITranslationTable): void => {
                self.tables[lang] = data;
                deferred.resolve(data);
            };
            // private helper
            const rejectDeferredWithPartName = () => deferred.reject(self.name);
            // private helper, data fetching logic
            const badAnswer = result => {
                if (errorHandler) {
                    errorHandler(self.name, lang, result)
                        .then(handleNewData, rejectDeferredWithPartName);
                } else {
                    rejectDeferredWithPartName();
                }
            };
            ////const tryGettingThisTable = () => fetchData().then(
            ////    dict => handleNewData(dict),
            ////    badAnswer);
            const tryGettingThisTable = () => fetchData().then(
                dict => handleNewData(dict),
                () => {
                    // Retry #1
                    fetchData().then(
                        dict => handleNewData(dict),
                        () => fetchData().then(dict => handleNewData(dict), // Retry #2
                                badAnswer) // After 2 retries, still failed
                            .catch(badAnswer)) // After 2 retries, still failed
                    .catch(() => fetchData().then(dict => handleNewData(dict), // Retry #2
                            badAnswer) // After 2 retries, still failed
                        .catch(badAnswer)); // After 2 retries, still failed
                }).catch(() => {
                    // Retry #1
                    fetchData().then(
                        dict => handleNewData(dict),
                        () => fetchData().then(dict => handleNewData(dict), // Retry #2
                                badAnswer) // After 2 retries, still failed
                            .catch(badAnswer)) // After 2 retries, still failed
                    .catch(() => fetchData().then(dict => handleNewData(dict), // Retry #2
                            badAnswer) // After 2 retries, still failed
                        .catch(badAnswer)); // After 2 retries, still failed
                });
            // loading logic
            if (!this.tables[lang]) {
                // let's try loading the data
                if (!lastLangPromise) {
                    // this is the first request - just go ahead and hit the server
                    tryGettingThisTable();
                } else {
                    // this is an additional request after one or more unfinished or failed requests
                    // chain the deferred off the previous request's promise so that this request
                    // conditionally executes
                    // if the previous request succeeds then the result will be passed through, but
                    // if it fails then this request will try again and hit the server
                    lastLangPromise.then(deferred.resolve, tryGettingThisTable);
                }
                // retain a reference to the last promise so we can continue the chain if another
                // request is made before any succeed
                // you can picture the promise chain as a singly-linked list (formed by the .then
                // handler queues) that's traversed by the execution context
                this.langPromises[lang] = deferred.promise;
            } else {
                // the part has already been loaded - if lastLangPromise is also undefined then the
                // table has been populated using setPart
                // this breaks the promise chain because we're not tying langDeferred's outcome to a
                // previous call's promise handler queues, but we don't care because there's no
                // asynchronous execution context to keep track of anymore
                deferred.resolve(this.tables[lang]);
            }
            return deferred.promise;
        }

        /**
         * @constructor
         * @name Part
         * @desc Represents Part object to add and set parts at runtime.
         */
        constructor(name: string, priority: number = 0, urlTemplate: string | ng.translate.IUrlTemplateFunc = null) {
            this.name = name;
            this.isActive = true;
            this.tables = {};
            this.priority = priority || 0;
            this.langPromises = {};
            this.urlTemplate = urlTemplate;
        }
    }
}
