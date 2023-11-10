module cef.store.factories {
    export interface IPromiseFactory {
        race<T>(...promises: ng.IPromise<T>[]): ng.IPromise<T>;
        addTimeoutToPromises<T>(
            timeoutAfterMS: number,
            allowedRetryCount: number,
            ...promises: Array<{ key: string, promiseFn: () => ng.IPromise<T>, retryPromiseFn?: () => ng.IPromise<T> }>)
            : ng.IPromise<{ [key: number]: T; }>;
    }

    export const cvPromiseFactoryFn = ($q: ng.IQService, $timeout: ng.ITimeoutService, $filter: ng.IFilterService) => {
        function consoleDebugWithTimestamp(startedAt, ...params): void {
            const now = new Date();
            console.debug(
                $filter("date")(now, "yyyy-mm-dd hh:mm:ss.sss"),
                $filter("amDifference")(now, startedAt, "ms", true),
                ...params);
        };
        function consoleErrorWithTimestamp(startedAt, ...params): void {
            const now = new Date();
            console.error(
                $filter("date")(now, "yyyy-mm-dd hh:mm:ss.sss"),
                $filter("amDifference")(now, startedAt, "ms", true),
                ...params);
        };
        function race<T>(...promises: ng.IPromise<T>[]): ng.IPromise<T> {
            return $q((resolve, reject) => {
                promises.forEach(promise => {
                    if (!promise) {
                        reject("invalid promise");
                        return;
                    }
                    $q.when(promise)
                        .then(resolve)
                        .catch(reject);
                });
            });
        };
        function addTimeoutToPromises<T>(
            timeoutAfterMS: number,
            allowedRetryCount: number,
            ...promises: Array<{ key: string, promiseFn: () => ng.IPromise<T>, retryPromiseFn?: () => ng.IPromise<T> }>)
            : ng.IPromise<{ [key: number]: T; }> {
            const startedAt = new Date();
            consoleDebugWithTimestamp(startedAt, "addTimeoutToPromises: entered");
            consoleDebugWithTimestamp(startedAt, "addTimeoutToPromises: timeoutAfterMS", timeoutAfterMS);
            if (angular.isUndefined(allowedRetryCount)
                || allowedRetryCount === null
                || allowedRetryCount < 0) {
                // NOTE: We allow a retry count of 0 to be valid argument to disallow
                // anything else must be a positive integer or get replaced with 1
                allowedRetryCount = 1;
            }
            consoleDebugWithTimestamp(startedAt, "addTimeoutToPromises: allowedRetryCount", allowedRetryCount);
            consoleDebugWithTimestamp(startedAt, "addTimeoutToPromises: promises", promises);
            const promiseResults: { [key: string]: T; } = { };
            const promiseRejectCounts: { [key: string]: number; } = { };
            const mainDefer = $q.defer<{ [key: string]: T; }>();
            const defers: { [key: string]: ng.IDeferred<void> } = { };
            consoleDebugWithTimestamp(startedAt, "addTimeoutToPromises: created defers");
            promises.forEach(promise => {
                defers[promise.key] = $q.defer<void>();
            });
            function retryFailedPromises(): void {
                const debugPrefix = "addTimeoutToPromises.retryFailedPromises";
                consoleDebugWithTimestamp(startedAt, debugPrefix + "retryFailedPromises: entered");
                const newPromises: Array<{ key: string, promiseFn: () => ng.IPromise<T>, retryPromiseFn?: () => ng.IPromise<T> }> = [];
                Object.keys(promiseRejectCounts).forEach(key => {
                    const found = _.find(promises, x => x.key === key);
                    if (!found) {
                        consoleErrorWithTimestamp(startedAt, debugPrefix + ": promise not found by key", key, promises, promiseRejectCounts);
                        return;
                    }
                    // Push the retry fn instead if it exists
                    newPromises.push({ key: found.key, promiseFn: found.retryPromiseFn ?? found.promiseFn });
                });
                addTimeoutToPromises(
                    timeoutAfterMS,
                    allowedRetryCount - 1 <= 0 ? 0 : allowedRetryCount - 1,
                    ...newPromises)
                .then(robj => {
                    consoleDebugWithTimestamp(startedAt, debugPrefix + ".then", robj);
                    // Merge the new results onto the results we've made so far
                    angular.extend(promiseResults, robj);
                    consoleDebugWithTimestamp(startedAt, debugPrefix + ".then after extend", promiseResults);
                    mainDefer.resolve(promiseResults);
                }).catch(reason => {
                    consoleErrorWithTimestamp(startedAt, debugPrefix + ".addTimeoutToPromises.catch", reason);
                });
                consoleDebugWithTimestamp(startedAt, debugPrefix + ": exited");
            }
            const timeoutPromise = $timeout(() => {
                const debugPrefix = "addTimeoutToPromises.timeoutPromise";
                // Reject the each defer in case of timeout and then trigger retries if (still) allowed
                consoleDebugWithTimestamp(startedAt, debugPrefix + ": timed out");
                let needsRetry = false;
                Object.keys(defers).forEach(key => {
                    consoleDebugWithTimestamp(startedAt, debugPrefix + ".defersLoop", key);
                    const defer = defers[key];
                    if (angular.isUndefined(promiseResults[key])) {
                        consoleDebugWithTimestamp(startedAt, debugPrefix + ".defersLoop: no results detected, rejecting and marking for retry", key);
                        defer.reject("Timed out");
                        promiseRejectCounts[key] = (promiseRejectCounts[key] || 0) + 1;
                        if (!needsRetry && allowedRetryCount > promiseRejectCounts[key]) {
                            needsRetry = true;
                        }
                        return;
                    }
                    consoleDebugWithTimestamp(startedAt, debugPrefix + ".defersLoop: results detected, ignoring", key);
                    // Remove it from the retry counts
                    delete promiseRejectCounts[key];
                    try {
                        defer.notify("Results detected, not timing out");
                    } catch (err) {
                        // Do Nothing
                    }
                });
                if (!needsRetry) {
                    consoleDebugWithTimestamp(startedAt, debugPrefix + ".retryCheck: no need to do additional retries");
                    mainDefer.resolve(promiseResults);
                    return;
                }
                consoleDebugWithTimestamp(startedAt, debugPrefix + ".retryCheck: Need retry");
                retryFailedPromises();
            }, timeoutAfterMS);
            // in a real implementation, we would call an async function and
            // resolve the promise after the async function finishes
            const debugPrefixP = "addTimeoutToPromises.promiseRunner";
            $q.all<T>(promises.map(x => {
                const key = x.key;
                consoleDebugWithTimestamp(startedAt, debugPrefixP + ".single.init", x, key);
                x.promiseFn().then(rr => {
                    consoleDebugWithTimestamp(startedAt, debugPrefixP + "single.then", key, rr);
                    const element = _.find(promises, x => x.key === key);
                    promiseResults[element.key] = rr;
                    defers[key].resolve();
                }).catch(reason => {
                    consoleDebugWithTimestamp(startedAt, debugPrefixP + "single.catch", key, reason);
                    defers[key].reject(reason);
                });
                return defers[key].promise;
            })).finally(() => {
                // Cancel the timeout as we've processed all promises
                consoleDebugWithTimestamp(startedAt, debugPrefixP + "all.finally: cancelling the timout and sending final results (may be a subset if this is the retry)", promiseResults);
                $timeout.cancel(timeoutPromise);
                mainDefer.resolve(promiseResults);
            });
            return mainDefer.promise;
        };
        return <IPromiseFactory>{
            race,
            addTimeoutToPromises,
        };
    }
}
