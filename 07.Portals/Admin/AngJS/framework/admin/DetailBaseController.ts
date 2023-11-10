/**
 * @file framework/admin/DetailBaseController.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Detail Base Controller class. Handles a lot of the basic CRUD operations for you
 */
module cef.admin {
    export abstract class DetailBaseController<TRecordModel extends api.BaseModel>
            extends core.TemplatedControllerBase {
        // Properties
        record: TRecordModel;
        abstract detailName: string;
        // Required Functions
        protected constructorPreAction(): ng.IPromise<void> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve();
        }
        protected loadCollections(): ng.IPromise<void> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve();
        }

        protected fullReject(reject, a1: core.IErrorMessageArg = null, a2: string[] = null): void {
            this.finishRunning(true, a1, a2);
            reject();
        }

        abstract loadNewRecord(): ng.IPromise<TRecordModel>; // Must override
        protected loadRecordPreAction(): ng.IPromise<void> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve();
        }
        abstract loadRecordCall(id: number | string): ng.IHttpPromise<TRecordModel>; // Must override
        loadRecord(id: number | string): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.loadRecordPreAction().then(() => {
                    this.loadRecordCall(id).then(r1 => {
                        this.loadRecordActionBeforeAssign(r1.data).then(r2 => {
                            this.record = r2;
                            this.loadRecordActionAfterSuccess(this.record).then(() /*r3*/ => {
                                this.loadCollections().then(() => {
                                    this.finishRunning();
                                    resolve();
                                }).catch(reason4 => this.fullReject(reject, reason4));
                            }).catch(reason3 => this.fullReject(reject, reason3));
                        }).catch(reason2 => this.fullReject(reject, reason2));
                    }).catch(reason1 => this.fullReject(reject, reason1));
                }).catch(reason0 => this.fullReject(reject, reason0));
            });
        }
        protected loadRecordActionBeforeAssign(result: TRecordModel): ng.IPromise<TRecordModel> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(result);
        }
        protected loadRecordActionAfterSuccess(result: TRecordModel): ng.IPromise<TRecordModel> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(result);
        }

        protected createRecordPreAction(toSend: TRecordModel): ng.IPromise<TRecordModel> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(toSend);
        }
        abstract createRecordCall(routeParams?: TRecordModel): ng.IHttpPromise<api.CEFActionResponseT<number>>; // Must Override
        private doCreate(state: string = null, params = null): ng.IPromise<void> {
            return this.$q((__, reject) => {
                // Resolve is not used because the successful run will update the router state
                this.createRecordPreAction(this.record).then(r1 => {
                    this.createRecordCall(r1).then(r2 => {
                        this.createRecordActionAfterSuccess(r2.data).then(newID => {
                            if (state) {
                                this.$state.go(state, params, { reload: true });
                                return;
                            }
                            this.$stateParams.ID = newID;
                            const newState = this.$state.current;
                            newState.params = this.$stateParams;
                            this.$state.go(newState);
                        }).catch(reason3 => this.fullReject(reject, reason3));
                    }).catch(reason2 => this.fullReject(reject, reason2));
                }).catch(reason1 => this.fullReject(reject, reason1));
            });
        }
        protected createRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            if (!result.ActionSucceeded) {
                this.consoleLog(result);
                return this.$q.reject(result);
            }
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(result.Result);
        }

        protected updateRecordPreAction(toSend: TRecordModel): ng.IPromise<TRecordModel> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(toSend);
        }
        abstract updateRecordCall(routeParams?: TRecordModel): ng.IHttpPromise<api.CEFActionResponseT<number>>; // Must Override
        private doUpdate(state: string = null): ng.IPromise<void> {
            return this.$q((__, reject) => {
                // Resolve is not used because the successful run will update the router state
                this.updateRecordPreAction(this.record)
                    .then(r1 => this.updateRecordCall(r1))
                    .then(r2 => {
                        if (!r2 || !r2.data) {
                            return this.$q.reject("No data returned on update call");
                        }
                        return this.updateRecordActionAfterSuccess(r2.data)
                    })
                    .then(newID => {
                        if (state) {
                            this.$state.go(state);
                            return;
                        }
                        this.$state.go(this.$state.current, { ID: newID }, { reload: true });
                    })
                    .catch(reason1 => this.fullReject(reject, reason1));
            });
        }
        protected updateRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            if (!result.ActionSucceeded) {
                this.consoleLog(result);
                return this.$q.reject(result);
            }
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(result.Result);
        }

        protected deactivateRecordPreAction(toSend: TRecordModel): ng.IPromise<TRecordModel> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(toSend);
        }
        abstract deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse>; // Must Override
        deactivateRecord(): void {
            this.cvConfirmModalFactory(this.$translate("ui.admin.detailBaseController.ConfirmDeactivate.Template", { type: this.detailName })).then(accepted => {
                if (!accepted) { return; }
                this.setRunning(this.$translate("ui.admin.detailBaseController.RecyclingType.Ellipses.Template", { type: this.detailName }));
                this.deactivateRecordPreAction(this.record).then(r1 => {
                    this.deactivateRecordCall(r1.ID).then(r2 => {
                        if (!r2.data.ActionSucceeded) {
                            this.finishRunning(true, null, r2.data.Messages);
                            return;
                        }
                        this.deactivateRecordActionAfterSuccess(r2.data).then(r3 => {
                            if (!r3.ActionSucceeded) {
                                this.finishRunning(true, null, r3.Messages);
                                return;
                            }
                            this.$state.go(this.$state.current, { ID: r1.ID }, { reload: true });
                        }).catch(reason3 => this.finishRunning(true, reason3));
                    }).catch(reason2 => this.finishRunning(true, reason2));
                }).catch(reason1 => this.finishRunning(true, reason1));
            });
        }
        protected deactivateRecordActionAfterSuccess(result: api.CEFActionResponse): ng.IPromise<api.CEFActionResponse> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(result);
        }

        protected reactivateRecordPreAction(toSend: TRecordModel): ng.IPromise<TRecordModel> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(toSend);
        }
        abstract reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse>; // Must Override
        reactivateRecord(): void {
            this.cvConfirmModalFactory(this.$translate("ui.admin.detailBaseController.ConfirmReactivate.Template", { type: this.detailName })).then(accepted => {
                if (!accepted) { return; }
                this.setRunning(this.$translate("ui.admin.detailBaseController.RestoringType.Ellipses.Template", { type: this.detailName }));
                this.reactivateRecordPreAction(this.record).then(r1 => {
                    this.reactivateRecordCall(r1.ID).then(r2 => {
                        if (!r2.data.ActionSucceeded) {
                            this.finishRunning(true, null, r2.data.Messages);
                            return;
                        }
                        this.reactivateRecordActionAfterSuccess(r2.data).then(r3 => {
                            if (!r3.ActionSucceeded) {
                                this.finishRunning(true, null, r3.Messages);
                                return;
                            }
                            this.$state.go(this.$state.current, { ID: r1.ID }, { reload: true });
                        }).catch(reason3 => this.finishRunning(true, reason3));
                    }).catch(reason2 => this.finishRunning(true, reason2));
                }).catch(reason1 => this.finishRunning(true, reason1));
            });
        }
        protected reactivateRecordActionAfterSuccess(result: api.CEFActionResponse): ng.IPromise<api.CEFActionResponse> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(result);
        }

        protected deleteRecordPreAction(toSend: TRecordModel): ng.IPromise<TRecordModel> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(toSend);
        }
        abstract deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse>; // Must Override
        deleteRecord(state: string): void {
            this.cvConfirmModalFactory(this.$translate("ui.admin.detailBaseController.ConfirmDelete.Template", { type: this.detailName })).then(accepted => {
                if (!accepted) { return; }
                this.setRunning(this.$translate("ui.admin.detailBaseController.DeletingType.Ellipses.Template", { type: this.detailName }));
                this.deleteRecordPreAction(this.record).then(r1 => {
                    this.deleteRecordCall(r1.ID).then(r2 => {
                        if (!r2.data.ActionSucceeded) {
                            this.finishRunning(true, null, r2.data.Messages);
                            return;
                        }
                        this.deleteRecordActionAfterSuccess(r2.data).then(r3 => {
                            if (!r3.ActionSucceeded) {
                                this.finishRunning(true, null, r3.Messages);
                                return;
                            }
                            this.$state.go(state);
                        }).catch(reason3 => this.finishRunning(true, reason3));
                    }).catch(reason2 => this.finishRunning(true, reason2));
                }).catch(reason1 => this.finishRunning(true, reason1));
            });
        }
        protected deleteRecordActionAfterSuccess(result: api.CEFActionResponse): ng.IPromise<api.CEFActionResponse> {
            // Do Nothing by default, override to take additional actions
            return this.$q.resolve(result);
        }

        saveRecord(state: string = null): ng.IPromise<void> {
            this.setRunning(`Saving ${this.detailName}...`);
            // The actions will eventually either call finish running or reload the state
            if (this.record.ID > 0) {
                return this.doUpdate(state);
            }
            return this.doCreate(state);
        }
        cancel = (state: string): void => {
            this.$state.go(state);
        }
        sanitizeMarkup(e): void { e.html = e.html.replace(/style="([^"]*)"/g, ""); }

        // Convenience Functions
        createAddressModel = (): api.AddressModel => {
            return <api.AddressModel>{
                ID: 0,
                Active: true,
                CustomKey: null,
                CreatedDate: new Date(),
                UpdatedDate: null,
                Name: null,
                Description: null,
                Street1: null,
                Street2: null,
                Street3: null,
                City: null,
                RegionID: 0,
                CountryID: 0,
                PostalCode: null,
                IsBilling: false,
                IsPrimary: false,
                Phone: null,
                Fax: null,
                Email: null,
                Latitude: null,
                Longitude: null
            };
        }

        createContactModel = (): api.ContactModel => {
            return <api.ContactModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                FirstName: null,
                LastName: null,
                Phone1: null,
                Email1: null,
                NotificationViaEmail: false,
                NotificationViaSMSPhone: false,
                Gender: false,
                SameAsBilling: false,
                Address: this.createAddressModel(),
                TypeID: 1,
                TypeKey: this.detailName
            };
        }

        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $window: ng.IWindowService,
                protected readonly $state: ng.ui.IStateService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvConfirmModalFactory: modals.IConfirmModalFactory) {
            super(cefConfig);
            this.setRunning(`Loading...`);
            this.constructorPreAction().then(() => {
                if ($stateParams.ID > 0) {
                    // This calls loadCollections internally
                    return this.loadRecord($stateParams.ID);
                }
                return this.loadNewRecord().then(() => this.loadCollections());
            }).catch(reason => this.finishRunning(true, reason))
            .finally(() => {
                // This is a fallback to keep it from being stuck in finish running.
                // It's technically hiding potential issues but they are difficult
                // to reproduce
                if (this.viewState.running) {
                    this.finishRunning();
                }
            });
        }
    }

    adminApp.provider("$copyToClipboard", () => {
        return {
            $get: ($q: ng.IQService, $window: ng.IWindowService) => {
                const body = angular.element($window.document.body);
                const textarea = angular.element('<textarea/>');
                textarea.css({ "position": "fixed", "opacity": "0", "z-index": -5000 });
                return {
                    copy: (stringToCopy: string): ng.IPromise<boolean> => {
                        var defer = $q.defer<boolean>();
                        defer.notify("copying the text to clipboard");
                        textarea.val(stringToCopy);
                        body.append(textarea);
                        textarea[0]["select"]();
                        try {
                            const result = $window.document.execCommand("copy");
                            if (!result) {
                                throw result;
                            }
                            defer.resolve(result);
                        } catch (err) {
                            defer.reject(err);
                        } finally {
                            textarea.remove();
                        }
                        return defer.promise;
                    }
                };
            }
        };
    });
}
