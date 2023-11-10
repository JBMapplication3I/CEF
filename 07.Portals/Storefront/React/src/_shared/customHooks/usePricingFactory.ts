/**
 * @file React/src/_shared/customHooks/usePricingFactory.tsx
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Pricing Factory hook
 */

import { useState, useEffect } from "react";
import { CalculatedPrices, ProductModel } from "../../_api/cvApi.shared";
import { CEFConfig } from "../../_redux/_reduxTypes";
import { useCefConfig } from "./useCefConfig";
import cvApi from "../../_api/cvApi";

export interface IPricingFactory {
  factoryAssign(product: ProductModel): Promise<ProductModel>;
  bulkFactoryAssign(products: ProductModel[]): Promise<ProductModel[]>;
}

export class PricingFactory implements IPricingFactory {
  // Functions
  private genBlankPricesObj(): CalculatedPrices {
    return {
      base: null,
      sale: null,
      msrp: null,
      reduction: null,
      haveBase: false,
      haveSale: false,
      haveMsrp: false,
      haveReduction: false,
      isSale: false,
      amountOff: null,
      percentOff: null,
      loading: true
    };
  }
  async factoryAssign(product: ProductModel): Promise<ProductModel> {
    if (product.readPrices instanceof Function) {
      // Already processed
      return product;
    }
    product["$_rawPrices"] = this.genBlankPricesObj();
    product.readPrices = () => product["$_rawPrices"];
    // TODO: MemCache the results by product ID so we can avoid repeat calls for same product

    let r = await cvApi.pricing.CalculatePricesForProduct(product.ID, 1)
    if (!r || !r.data || !r.data.ActionSucceeded) {
      console.error(r && r.data);
      return;
    }
    const prices = product["$_rawPrices"] as CalculatedPrices;
    const { BasePrice, SalePrice, MsrpPrice, ReductionPrice } = r.data.Result;
    // Assign updated values
    prices.base = BasePrice;
    prices.sale = SalePrice;
    prices.msrp = MsrpPrice;
    prices.reduction = ReductionPrice;
    // Assign calculated values
    prices.haveBase = prices.base >= 0;
    prices.haveSale = prices.sale >= 0;
    prices.haveMsrp = prices.msrp >= 0;
    prices.haveReduction = prices.reduction >= 0;
    prices.isSale = prices.sale > 0 && prices.sale < prices.base;
    if (prices.isSale) {
      prices.amountOff = prices.base - prices.sale;
      prices.percentOff = (prices.amountOff / prices.base) * 100;
    }
    // Finish
    prices.loading = false;
    product["$_rawPrices"] = prices;
    return product;
  }
  bulkFactoryAssign(products: ProductModel[]): Promise<ProductModel[]> {
    if (!products || !products.length) {
      return new Promise((resolve, _reject) => {
        resolve(products);
      });
    }
    const processed: ProductModel[] = [];
    const toProcess: ProductModel[] = [];
    products.forEach((product) => {
      if (product.readPrices instanceof Function) {
        processed.push(product);
      } else {
        product["$_rawPrices"] = this.genBlankPricesObj();
        product.readPrices = () => product["$_rawPrices"];
        toProcess.push(product);
      }
    });
    return new Promise((resolve, reject) => {
      cvApi.pricing
        .CalculatePricesForProducts({
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
            const prices = found["$_rawPrices"] as CalculatedPrices;
            prices.base = r.data.Result[productID].BasePrice;
            prices.sale = r.data.Result[productID].SalePrice;
            prices.msrp = r.data.Result[productID].MsrpPrice;
            prices.reduction = r.data.Result[productID].ReductionPrice;
            prices.haveBase = prices.base >= 0;
            prices.haveSale = prices.sale >= 0;
            prices.haveMsrp = prices.msrp >= 0;
            prices.haveReduction = prices.reduction >= 0;
            prices.isSale = prices.sale > 0 && prices.sale < prices.base;
            if (prices.isSale) {
              prices.amountOff = prices.base - prices.sale;
              prices.percentOff = (prices.amountOff / prices.base) * 100;
            }
            prices.loading = false;
            found["$_rawPrices"] = prices;
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

export const usePricingFactory = (): IPricingFactory => {
  const [pricingFactory, setPricingFactory] = useState(null);
  const cefConfig = useCefConfig() as CEFConfig;
  useEffect(() => {
    if (cefConfig == null) {
      return;
    }
    // new up the class from above only once
    const instance = new PricingFactory(cefConfig);
    setPricingFactory(instance);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cefConfig]);
  return pricingFactory;
};
