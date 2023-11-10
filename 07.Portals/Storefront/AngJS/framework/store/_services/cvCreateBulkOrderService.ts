/**
 * @file framework/store/_services/cvCreateBulkOrderService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Create Bulk Order Service for making orders as a bulk operation against
 * users in the same account
 */
module cef.store.services {
    export interface CreateOrderObject {
        productID?: number,
        users?: Array<number>
    }

    /**
     * A basic in-between service to aid component communication
     * @interface ICreateBulkOrderService
     * @export
     */
    export interface ICreateBulkOrderService {
        /**
         * Creates a placeholder for an order in the dictionary.
         * If an order object is also passed, will assign it as well
         * @param {string} key
         * @param {CreateOrderObject} [data=null]
         * @returns {string}
         * @memberof CreateBulkOrderService
         */
        create: (key: string, data?: CreateOrderObject) => string;
        /**
         * Removes the order from the dictionary by it"s key
         * @param {string} key
         * @returns {boolean}
         * @memberof CreateBulkOrderService
         */
        delete: (key: string) => boolean;
        /**
         * Updates the order in the dictionary with a new value.
         * @note Will not add it to the dictionary if it"s not there
         * @param {string} key
         * @param {CreateOrderObject} data
         * @returns {CreateOrderObject}
         * @memberof CreateBulkOrderService
         */
        set: (key: string, data: CreateOrderObject) => CreateOrderObject;
        /**
         * Merges the order into the orders dictionary
         * @param {string} key
         * @param {CreateOrderObject} data
         * @returns {CreateOrderObject}
         * @memberof CreateBulkOrderService
         */
        merge: (key: string, data: CreateOrderObject) => CreateOrderObject;
        /**
         * Gets an order by it"s key
         * @param {string} key
         * @returns {CreateOrderObject}
         * @memberof CreateBulkOrderService
         */
        get: (key: string) => CreateOrderObject;
        /**
         * Gets the list of orders
         * @returns {Array<string>}
         * @memberof CreateBulkOrderService
         */
        list: () => Array<string>;
    }

    export class CreateBulkOrderService implements ICreateBulkOrderService {
        private orders: { [keys: string]: CreateOrderObject } = { };
        create(key: string, data: CreateOrderObject = null): string {
            if (angular.isString(key)) {
                this.orders[key] = data;
                return key;
            }
            return null;
        }
        delete(key: string): boolean {
            if (key && this.orders.hasOwnProperty(key)) {
                delete this.orders[key];
                return true;
            }
            return false;
        }
        set(key: string, data: CreateOrderObject): CreateOrderObject {
            if (key && this.orders.hasOwnProperty(key)) {
                this.orders[key] = data;
            }
            return this.orders[key];
        }
        merge(key: string, data: CreateOrderObject): CreateOrderObject {
            if (key && this.orders.hasOwnProperty(key)) {
                this.orders[key] = angular.extend({}, this.orders[key], data);
            }
            return this.orders[key];
        }
        get(key: string): CreateOrderObject {
            if (key && this.orders.hasOwnProperty(key)) {
                return this.orders[key];
            }
            return null;
        }
        list(): Array<string> {
            return Object.keys(this.orders);
        }
    }
}
