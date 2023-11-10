/**
 * @file framework/store/_services/cvManufacturerService.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Manufacturer Service class, stores vendor data that has been loaded
 * to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface IManufacturerLookupInterface extends ILookupInterfaceBase {
    }

    export interface IManufacturerService extends IDataServiceBase<api.ManufacturerModel, IManufacturerLookupInterface> {
    }

    export class ManufacturerService extends DataServiceBase<api.ManufacturerModel, IManufacturerLookupInterface> implements IManufacturerService {
        // Properties
        // <None at this level>
        // Abstracts
        getByIDPromise              = this.cvApi.manufacturers.GetManufacturerByID;
        getByIDsPromise             = this.cvApi.manufacturers.GetManufacturers;
        checkExistsByIDPromise      = this.cvApi.manufacturers.CheckManufacturerExistsByID;
        checkExistsByKeyPromise     = this.cvApi.manufacturers.CheckManufacturerExistsByKey;
        checkExistsByNamePromise    = this.cvApi.manufacturers.CheckManufacturerExistsByName;
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
