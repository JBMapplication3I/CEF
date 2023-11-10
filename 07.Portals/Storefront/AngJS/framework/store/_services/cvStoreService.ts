/**
 * @file framework/store/_services/cvStoreService.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Store Service class, stores vendor data that has been loaded
 * to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface IStoreLookupInterface extends ILookupInterfaceBase {
    }

    export interface IStoreService extends IDataServiceBase<api.StoreModel, IStoreLookupInterface> {
    }

    export class StoreService extends DataServiceBase<api.StoreModel, IStoreLookupInterface> implements IStoreService {
        // Properties
        // <None at this level>
        // Abstracts
        getByIDPromise              = this.cvApi.stores.GetStoreByID;
        getByIDsPromise             = this.cvApi.stores.GetStores;
        checkExistsByIDPromise      = this.cvApi.stores.CheckStoreExistsByID;
        checkExistsByKeyPromise     = this.cvApi.stores.CheckStoreExistsByKey;
        checkExistsByNamePromise    = this.cvApi.stores.CheckStoreExistsByName;
        // Functions
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly $sce: ng.ISCEService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvInventoryService: IInventoryService,
                protected readonly cvPricingService: IPricingService,
                protected readonly cvPromiseFactory: factories.IPromiseFactory) {
            super($q, $sce, cvPromiseFactory);
        }
    }
}
