/**
 * @file framework/store/_services/cvVendorService.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Vendor Service class, stores vendor data that has been loaded
 * to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface IVendorLookupInterface extends ILookupInterfaceBase {
    }

    export interface IVendorService extends IDataServiceBase<api.VendorModel, IVendorLookupInterface> {
    }

    export class VendorService extends DataServiceBase<api.VendorModel, IVendorLookupInterface> implements IVendorService {
        // Properties
        // <None at this level>
        // Abstracts
        getByIDPromise              = this.cvApi.vendors.GetVendorByID;
        getByIDsPromise             = this.cvApi.vendors.GetVendors;
        checkExistsByIDPromise      = this.cvApi.vendors.CheckVendorExistsByID;
        checkExistsByKeyPromise     = this.cvApi.vendors.CheckVendorExistsByKey;
        checkExistsByNamePromise    = this.cvApi.vendors.CheckVendorExistsByName;
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
