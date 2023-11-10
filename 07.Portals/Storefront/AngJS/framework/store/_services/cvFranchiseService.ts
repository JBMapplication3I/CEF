/**
 * @file framework/store/_services/cvManufacturerService.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Manufacturer Service class, stores vendor data that has been loaded
 * to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface IFranchiseLookupInterface extends ILookupInterfaceBase {
    }

    export interface IFranchiseService extends IDataServiceBase<api.FranchiseModel, IFranchiseLookupInterface> {
    }

    export class FranchiseService extends DataServiceBase<api.FranchiseModel, IFranchiseLookupInterface> implements IFranchiseService {
        // Properties
        // <None at this level>
        // Abstracts
        getByIDPromise              = this.cvApi.franchises.GetFranchiseByID;
        getByIDsPromise             = this.cvApi.franchises.GetFranchises;
        checkExistsByIDPromise      = this.cvApi.franchises.CheckFranchiseExistsByID;
        checkExistsByKeyPromise     = this.cvApi.franchises.CheckFranchiseExistsByKey;
        checkExistsByNamePromise    = this.cvApi.franchises.CheckFranchiseExistsByName;
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
