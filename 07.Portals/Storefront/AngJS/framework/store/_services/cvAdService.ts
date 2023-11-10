/**
 * @file framework/store/_services/cvAdService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Ad Service, provides Ads based on where and how often they should show
 */
module cef.store.services {
    export interface IAdService {
        adStores: api.AdStoreModel[];
        zones: api.ZoneModel[];
        store: api.StoreModel;
        user: api.UserModel;
    }

    export class AdService implements IAdService {
        constructor(
                private readonly $q: ng.IQService,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $animate: ng.animate.IAnimateService,
                private readonly $state: ng.ui.IStateService,
                private readonly $sce: ng.ISCEService) {
            $rootScope["cvAdService"] = this;
        }

        private ignoring = false;
        private notifyControlsOnUpdate = true;

        adStores: api.AdStoreModel[] = [];
        zones: api.ZoneModel[] = [];
        store: api.StoreModel;
        user: api.UserModel;
    }
}
