"use strict";

let documentTitleCallbackAdmin: (title: string) => string = undefined;
let defaultDocumentTitleAdmin = document.title;

angular
    .module("ui.router.title", ["ui.router"])
    .provider("$title", (): ng.ui.ITitleProvider => {
        const getTitleValue = ($q: ng.IQService, title: string | (() => ng.IPromise<string>), state: ng.ui.IResolvedState): ng.IPromise<{ value: string, state: ng.ui.IResolvedState }> => {
            return $q((resolve, reject) => {
                if (angular.isFunction(title)) {
                    (title as () => ng.IPromise<string>)()
                        .then(result => resolve({ value: result, state: state }), result => reject(result))
                        .catch(result => reject(result));
                    return;
                }
                resolve({ value: title as string, state: state });
            });
        }
        return {
            documentTitle: cb => {
                documentTitleCallbackAdmin = cb;
            },
            $get: ($q: ng.IQService, $state: ng.ui.IStateService, $translate: ng.translate.ITranslateService): ng.ui.ITitleService => {
                return {
                    title() {
                        return getTitleValue($q, $state.$current.locals.globals["$title"], $state.$current);
                    },
                    breadCrumbs(): ng.IPromise<Array<ng.ui.IBreadcrumb>> {
                        let state = $state.$current;
                        const promises: Array<ng.IPromise<ng.ui.IBreadcrumb>> = [];
                        while (state) {
                            if (state["resolve"] && state["resolve"].$title) {
                                promises.push((getTitleValue($q, state.locals.globals["$title"], state).then(result => {
                                    return $translate(result.value, result.state.locals.globals["$stateParams"]).then(translated => {
                                        return <ng.ui.IBreadcrumb>{
                                            title: result.value,
                                            translatedTitle: translated as string,
                                            state: result.state["self"].name,
                                            stateParams: result.state.locals.globals["$stateParams"]
                                        };
                                    });
                                })) as any);
                            }
                            state = state["parent"];
                        }
                        return $q.all<ng.ui.IBreadcrumb>(promises).then(results => results.reverse());
                    }
                };
            }
        };
    })
    .run(($rootScope: ng.IRootScopeService, $title: ng.ui.ITitleService, $injector, cvServiceStrings: cef.admin.services.IServiceStrings) => {
        $rootScope.$on(cvServiceStrings.events.$state.changeSuccess, () => {
            $title.breadCrumbs().then(results => {
                $rootScope.$breadcrumbs = results;
                $title.title().then(result => {
                    $rootScope.$title = result.value;
                    if (angular.isFunction(documentTitleCallbackAdmin)) {
                        $injector.invoke(documentTitleCallbackAdmin)
                            .then(docTitle => document.title = (docTitle || defaultDocumentTitleAdmin));
                        return;
                    }
                    document.title = $rootScope.$title || defaultDocumentTitleAdmin;
                });
            });
        });
    });
