/**
 * @file React/src/_shared/customHooks/useInventoryFactory.tsx
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Inventory Factory hook
 */
import { useState, useEffect } from "react";
import { CalculatedInventories, ProductModel } from "../../_api/cvApi.shared";
import { CEFConfig } from "../../_redux/_reduxTypes";
import { useCefConfig } from "./useCefConfig";
import cvApi from "../../_api/cvApi";

interface IInventoryFactory {
  factoryAssign(product: ProductModel): ProductModel;
  bulkFactoryAssign(products: ProductModel[]): Promise<ProductModel[]>;
}

class InventoryFactory implements IInventoryFactory {
  // Functions
  private genBlankInventoryObj(): CalculatedInventories {
    return {
      ProductID: null,
      IsDiscontinued: false,
      IsUnlimitedStock: false,
      IsOutOfStock: false,
      QuantityPresent: null,
      QuantityAllocated: null,
      QuantityOnHand: null,
      MaximumPurchaseQuantity: null,
      MaximumPurchaseQuantityIfPastPurchased: null,
      AllowBackOrder: false,
      MaximumBackOrderPurchaseQuantity: null,
      MaximumBackOrderPurchaseQuantityIfPastPurchased: null,
      MaximumBackOrderPurchaseQuantityGlobal: null,
      AllowPreSale: false,
      PreSellEndDate: null,
      QuantityPreSellable: null,
      QuantityPreSold: null,
      MaximumPrePurchaseQuantity: null,
      MaximumPrePurchaseQuantityIfPastPurchased: null,
      MaximumPrePurchaseQuantityGlobal: null,
      RelevantLocations: null,
      loading: true
    } as CalculatedInventories;
  }
  factoryAssign(product: ProductModel): ProductModel {
    if (!this.cefConfig.featureSet.inventory.enabled) {
      return product;
    }
    if (product.readInventory instanceof Function) {
      // Already processed
      return product;
    }
    product["$_rawInventory"] = this.genBlankInventoryObj();
    product.readInventory = () => product["$_rawInventory"];
    // TODO: MemCache the results by product ID so we can avoid repeat calls for same product
    cvApi.providers.CalculateInventory(product.ID).then((r) => {
      if (!r || !r.data || !r.data.ActionSucceeded) {
        console.error(r && r.data);
        return;
      }
      let inventory = product["$_rawInventory"] as CalculatedInventories;
      if (!inventory) {
        inventory = { loading: true } as any;
      }
      // Assign updated values
      /*
       * NOTE: Feature required settings have been run in the server to only assign values
       * That could be valid both globally and on this individual product's level. All
       * variables can be assigned here because they will have the correct flags on them
       * already.
       */
      inventory.ProductID = r.data.Result.ProductID;
      inventory.IsDiscontinued = r.data.Result.IsDiscontinued;
      inventory.IsUnlimitedStock = r.data.Result.IsUnlimitedStock;
      inventory.IsOutOfStock = r.data.Result.IsOutOfStock;
      inventory.QuantityPresent = r.data.Result.QuantityPresent;
      inventory.QuantityAllocated = r.data.Result.QuantityAllocated;
      inventory.QuantityOnHand = r.data.Result.QuantityOnHand;
      inventory.MaximumPurchaseQuantity = r.data.Result.MaximumPurchaseQuantity;
      inventory.MaximumPurchaseQuantityIfPastPurchased =
        r.data.Result.MaximumPurchaseQuantityIfPastPurchased;
      inventory.AllowBackOrder = r.data.Result.AllowBackOrder;
      inventory.MaximumBackOrderPurchaseQuantity =
        r.data.Result.MaximumBackOrderPurchaseQuantity;
      inventory.MaximumBackOrderPurchaseQuantityIfPastPurchased =
        r.data.Result.MaximumBackOrderPurchaseQuantityIfPastPurchased;
      inventory.MaximumBackOrderPurchaseQuantityGlobal =
        r.data.Result.MaximumBackOrderPurchaseQuantityGlobal;
      inventory.AllowPreSale = r.data.Result.AllowPreSale;
      inventory.PreSellEndDate = r.data.Result.PreSellEndDate;
      inventory.QuantityPreSellable = r.data.Result.QuantityPreSellable;
      inventory.QuantityPreSold = r.data.Result.QuantityPreSold;
      inventory.MaximumPrePurchaseQuantity =
        r.data.Result.MaximumPrePurchaseQuantity;
      inventory.MaximumPrePurchaseQuantityIfPastPurchased =
        r.data.Result.MaximumPrePurchaseQuantityIfPastPurchased;
      inventory.MaximumPrePurchaseQuantityGlobal =
        r.data.Result.MaximumPrePurchaseQuantityGlobal;
      inventory.RelevantLocations = r.data.Result.RelevantLocations;
      // Assign calculated values
      /*
        // PILS Stock (TODO: Rework this with variables for store only stock)
        if (r.data.Result.RelevantLocations && r.data.Result.RelevantLocations.length > 0
            && this.cefConfig.featureSet.inventory.advanced.enabled
            && this.cefConfig.featureSet.stores.enabled
            && this.cefConfig.featureSet.inventory.advanced.countOnlyThisStoresWarehouseStock) {
            const matrix = this.cvCurrentStoreFactory.getStoreInventoryLocationsMatrixImmediate();
            if (matrix && matrix.length) {
                const thisStoresWarehouses = r.data.Result.RelevantLocations
                    .filter(x => _.some(matrix,
                        y => y.InternalInventoryLocationKey ="== "x.InventoryLocationSectionInventoryLocationKey
                          || y.DistributionCenterInventoryLocationKey === x.InventoryLocationSectionInventoryLocationKey));
                return _.sumBy(thisStoresWarehouses,
                    x => (x.Quantity || 0) - (x.QuantityAllocated || 0));
            }
        }
      */
      // Finish
      inventory.loading = false;
      product["$_rawInventory"] = inventory;
    });
    return product;
  }
  bulkFactoryAssign(products: ProductModel[]): Promise<ProductModel[]> {
    if (!this.cefConfig.featureSet.inventory.enabled) {
      return new Promise((resolve, reject) => {
        resolve(products);
      });
    }
    const debugMsg = `inventoryFactory.bulkFactoryAssign(products)`;
    if (!products || !products.length) {
      console.warn(
        `${debugMsg} No products provided to check inventory against`
      );
      return new Promise((resolve, reject) => {
        reject("No products provided to check inventory against");
      });
    }
    const processed: ProductModel[] = [];
    const toProcess: ProductModel[] = [];
    products.forEach((product) => {
      if (product.readInventory instanceof Function) {
        processed.push(product);
      } else {
        product["$_rawInventory"] = this.genBlankInventoryObj();
        product.readInventory = () => product["$_rawInventory"];
        toProcess.push(product);
      }
    });
    if (!toProcess.length) {
      return new Promise((resolve, reject) => {
        resolve(processed);
      });
    }
    return new Promise((resolve, reject) => {
      cvApi.providers
        .BulkCalculateInventory({
          ProductIDs: toProcess.map((x) => x.ID)
        })
        .then((r) => {
          if (!r || !r.data || !r.data.ActionSucceeded) {
            console.error(r && r.data);
            reject(r && r.data);
            return;
          }
          Object.keys(r.data.Result).forEach((productID) => {
            const found = toProcess.find((x) => x.ID === Number(productID));
            let inventory = found["$_rawInventory"] as CalculatedInventories;
            if (!inventory) {
              inventory = { loading: true } as any;
            }
            // Assign updated values
            /*
             * NOTE: Feature required settings have been run in the server to only assign values
             * That could be valid both globally and on this individual product's level. All
             * variables can be assigned here because they will have the correct flags on them
             * already.
             */
            inventory.ProductID = r.data.Result[productID].ProductID;
            inventory.IsDiscontinued = r.data.Result[productID].IsDiscontinued;
            inventory.IsUnlimitedStock =
              r.data.Result[productID].IsUnlimitedStock;
            inventory.IsOutOfStock = r.data.Result[productID].IsOutOfStock;
            inventory.QuantityPresent =
              r.data.Result[productID].QuantityPresent;
            inventory.QuantityAllocated =
              r.data.Result[productID].QuantityAllocated;
            inventory.QuantityOnHand = r.data.Result[productID].QuantityOnHand;
            inventory.MaximumPurchaseQuantity =
              r.data.Result[productID].MaximumPurchaseQuantity;
            inventory.MaximumPurchaseQuantityIfPastPurchased =
              r.data.Result[productID].MaximumPurchaseQuantityIfPastPurchased;
            inventory.AllowBackOrder = r.data.Result[productID].AllowBackOrder;
            inventory.MaximumBackOrderPurchaseQuantity =
              r.data.Result[productID].MaximumBackOrderPurchaseQuantity;
            inventory.MaximumBackOrderPurchaseQuantityIfPastPurchased =
              r.data.Result[
                productID
              ].MaximumBackOrderPurchaseQuantityIfPastPurchased;
            inventory.MaximumBackOrderPurchaseQuantityGlobal =
              r.data.Result[productID].MaximumBackOrderPurchaseQuantityGlobal;
            inventory.AllowPreSale = r.data.Result[productID].AllowPreSale;
            inventory.PreSellEndDate = r.data.Result[productID].PreSellEndDate;
            inventory.QuantityPreSellable =
              r.data.Result[productID].QuantityPreSellable;
            inventory.QuantityPreSold =
              r.data.Result[productID].QuantityPreSold;
            inventory.MaximumPrePurchaseQuantity =
              r.data.Result[productID].MaximumPrePurchaseQuantity;
            inventory.MaximumPrePurchaseQuantityIfPastPurchased =
              r.data.Result[
                productID
              ].MaximumPrePurchaseQuantityIfPastPurchased;
            inventory.MaximumPrePurchaseQuantityGlobal =
              r.data.Result[productID].MaximumPrePurchaseQuantityGlobal;
            inventory.RelevantLocations =
              r.data.Result[productID].RelevantLocations;
            // Assign calculated values
            /*
                        // PILS Stock (TODO: Rework this with variables for store only stock)
                        if (r.data.Result.RelevantLocations && r.data.Result.RelevantLocations.length > 0
                            && this.cefConfig.featureSet.inventory.advanced.enabled
                            && this.cefConfig.featureSet.stores.enabled
                            && this.cefConfig.featureSet.inventory.advanced.countOnlyThisStoresWarehouseStock) {
                            const matrix = this.cvCurrentStoreService.getStoreInventoryLocationsMatrixImmediate();
                            if (matrix && matrix.length) {
                                const thisStoresWarehouses = r.data.Result.RelevantLocations
                                    .filter(x => _.some(matrix,
                                        y => y.InternalInventoryLocationKey === x.InventoryLocationSectionInventoryLocationKey
                                        || y.DistributionCenterInventoryLocationKey === x.InventoryLocationSectionInventoryLocationKey));
                                return _.sumBy(thisStoresWarehouses,
                                    x => (x.Quantity || 0) - (x.QuantityAllocated || 0));
                            }
                        }
                        */
            // Finish
            inventory.loading = false;
            found["$_rawInventory"] = inventory;
            processed.push(found);
          });
          resolve(processed);
        })
        .catch(reject);
    });
  }
  // Constructor
  constructor(private readonly cefConfig: CEFConfig) {}
}

export const useInventoryFactory = (): IInventoryFactory => {
  const [inventoryFactory, setInventoryFactory] = useState(null);
  const cefConfig = useCefConfig() as CEFConfig;
  useEffect(() => {
    if (cefConfig == null) {
      return;
    }
    // new up the class from above only once
    const instance = new InventoryFactory(cefConfig);
    setInventoryFactory(instance);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cefConfig]);
  return inventoryFactory;
};
