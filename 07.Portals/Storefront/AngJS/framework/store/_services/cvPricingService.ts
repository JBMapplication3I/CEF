/**
 * @file framework/store/_services/cvPricingService.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Pricing service class
 */
module cef.store.services {
    export interface IPricingService {
        factoryAssign(product: api.ProductModel): api.ProductModel;
        bulkFactoryAssign(products: api.ProductModel[]): ng.IPromise<api.ProductModel[]>;
        factoryAssignWithUOMs(product: api.ProductModel): api.ProductModel;
    }

    export class PricingService {
        // Functions
        private genBlankPricesObj(): api.CalculatedPrices {
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
                loading: true,
            };
        }
        factoryAssign(product: api.ProductModel): api.ProductModel {
            if (angular.isFunction(product.readPrices)) {
                // Already processed
                return product;
            }
            product["$_rawPrices"] = this.genBlankPricesObj();
            product.readPrices = () => product["$_rawPrices"];
            // TODO: MemCache the results by product ID so we can avoid repeat calls for same product
            this.cvApi.pricing.CalculatePricesForProduct(product.ID, 1).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    console.error(r && r.data);
                    return;
                }
                const prices = product["$_rawPrices"] as api.CalculatedPrices;
                // Assign updated values
                prices.base = r.data.Result.BasePrice;
                prices.sale = r.data.Result.SalePrice;
                prices.msrp = r.data.Result.MsrpPrice;
                prices.reduction = r.data.Result.ReductionPrice;
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
            });
            return product;
        }
        bulkFactoryAssign(products: api.ProductModel[]): ng.IPromise<api.ProductModel[]> {
            if (!products || !products.length) {
                return this.$q.resolve(products);
            }
            const processed: api.ProductModel[] = [];
            const toProcess: api.ProductModel[] = [];
            products.forEach(product => {
                if (angular.isFunction(product.readPrices)) {
                    processed.push(product);
                } else {
                    product["$_rawPrices"] = this.genBlankPricesObj();
                    product.readPrices = () => product["$_rawPrices"];
                    toProcess.push(product);
                }
            });
            if (!toProcess.length) {
                return this.$q.resolve(processed);
            }
            return this.$q((resolve, reject) => {
                this.cvApi.pricing.CalculatePricesForProducts({
                    ProductIDs: toProcess.map(x => x.ID)
                }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        console.error(r && r.data);
                        reject(r && r.data);
                        return;
                    }
                    Object.keys(r.data.Result).forEach(productID => {
                        const found = _.find(toProcess, x => x.ID === Number(productID));
                        const prices = found["$_rawPrices"] as api.CalculatedPrices;
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
                }).catch(reject);
            });
        }
        factoryAssignWithUOMs(product: api.ProductModel): api.ProductModel {
            if (angular.isFunction(product.readPrices)) {
                // Already processed
                return product;
            }
            product["$_rawPrices"] = this.genBlankPricesObj();
            product.readPrices = () => product["$_rawPrices"];
            // TODO: MemCache the results by product ID so we can avoid repeat calls for same product
            this.cvApi.pricing.CalculatePricesForProduct(product.ID, 1).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    console.error(r && r.data);
                    return;
                }
                const prices = product["$_rawPrices"] as api.CalculatedPrices;
                // Assign updated values
                prices.base = r.data.Result.BasePrice;
                prices.sale = r.data.Result.SalePrice;
                prices.msrp = r.data.Result.MsrpPrice;
                prices.reduction = r.data.Result.ReductionPrice;
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
                let multiUOMObj = { };
                r.data.Result.MultiUOMPrices?.forEach(x => {
                    multiUOMObj[x.ProductUnitOfMeasure] = x
                })
                prices.loading = false;
                product["$_rawPrices"] = prices;
                product["$_rawPricesUOMs"] = multiUOMObj;
                product["$_priceListName"] = (r.data.Result.MultiUOMPrices ?? [{ PriceListName: null }])[0].PriceListName;
            });
            return product;
        }
        // Constructor
        constructor(
            private readonly $q: ng.IQService,
            private readonly cvApi: api.ICEFAPI) { }
    }
}
