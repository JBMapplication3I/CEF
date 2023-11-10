/**
 * @file framework/admin/_core/cacheFactoryService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Cache Factory Service, stores objects of particular types with keys
 * for global app access
 */
module cef.admin.core {
    export interface ICacheFactoryService<T> {
        (loadFn: (...params: any[]) => ng.IPromise<T>, saveFn: (input, ...params: any[]) => ng.IPromise<T>, ...params: any[]);
        (loadFn: (...params: any[]) => ng.IHttpPromise<T>, saveFn: (input, ...params: any[]) => ng.IPromise<T>, ...params: any[]);
        getStatic(): () => T;
        get: () => ng.IPromise<T>;
        set: (input: T) => ng.IPromise<T>;
    }

    export const cacheFactoryServiceFactoryFn = ($q: ng.IQService) =>
        (loadFn: (...params: any[]) => ng.IPromise<any>, saveFn: (input, ...params: any[]) => ng.IPromise<any>, ...params: any[]) => {
            let data: any;
            let cached = false;
            let initRunning = false;
            let initComplete = false;
            let initPromise: ng.IPromise<any>;
            return {
                get() {
                    if (!initRunning && !initComplete) {
                        initRunning = true;
                        initPromise = loadFn(params);
                        initPromise.then((response) => {
                            data = response;
                            cached = true;
                            initComplete = true;
                            return data;
                        }, result => {
                            // This produces more console errors than it needs to
                            /* throw Error(result); */
                        });
                        return initPromise;
                    } else {
                        if (!cached) {
                            return initPromise;
                        } else {
                            return $q((resolve, reject) => { resolve(data) });
                        }
                    }
                },
                set(input: any): ng.IPromise<any> {
                    return $q((resolve, reject) => {
                        saveFn(input, params).then(response => {
                            data = response;
                            cached = true;
                            resolve(response);
                        }, reject);
                    });
                }
            };
        };
}
