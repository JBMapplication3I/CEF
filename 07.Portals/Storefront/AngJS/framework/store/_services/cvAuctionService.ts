/**
 * @file framework/store/_services/cvAuctionService.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Auction Service class, stores auction data that has been loaded
 * to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface IAuctionLookupInterface extends ILookupInterfaceBase {
    }

    export interface IAuctionService extends IDataServiceBase<api.AuctionModel, IAuctionLookupInterface> {
    }

    export class AuctionService extends DataServiceBase<api.AuctionModel, IAuctionLookupInterface> implements IAuctionService {
        // Properties
        // <None at this level>
        // Abstracts
        getByIDPromise              = this.cvApi.auctions.GetAuctionByID;
        getByIDsPromise             = this.cvApi.auctions.GetAuctions;
        checkExistsByIDPromise      = this.cvApi.auctions.CheckAuctionExistsByID;
        checkExistsByKeyPromise     = this.cvApi.auctions.CheckAuctionExistsByKey;
        checkExistsByNamePromise    = this.cvApi.auctions.CheckAuctionExistsByName;
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
