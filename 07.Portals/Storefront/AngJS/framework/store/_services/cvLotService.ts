/**
 * @file framework/store/_services/cvLotService.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Lot Service class, stores lot data that has been loaded
 * to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface ILotLookupInterface extends ILookupInterfaceBase {
    }

    export interface ILotService extends IDataServiceBase<api.LotModel, ILotLookupInterface> {
    }

    export class LotService extends DataServiceBase<api.LotModel, ILotLookupInterface> implements ILotService {
        // Properties
        // <None at this level>
        // Abstracts
        getByIDPromise              = this.cvApi.auctions.GetLotByID;
        getByIDsPromise             = this.cvApi.auctions.GetLots;
        checkExistsByIDPromise      = this.cvApi.auctions.CheckLotExistsByID;
        checkExistsByKeyPromise     = this.cvApi.auctions.CheckLotExistsByKey;
        checkExistsByNamePromise    = this.cvApi.auctions.CheckLotExistsByName;
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
