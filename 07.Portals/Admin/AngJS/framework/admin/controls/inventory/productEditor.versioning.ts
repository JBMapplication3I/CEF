/**
 * @file framework/admin/controls/inventory/productEditor.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Product editor class for CEF Administrators
 */
module cef.admin.controls.inventory {
    export function productModelToVersionJson(record: api.ProductModel): string {
        const toSerialize = _.cloneDeep(record);
        //
        function processNames(obj: object, ...addNames: string[]): void {
            if (!addNames || !addNames.length) {
                return;
            }
            addNames.forEach(name => {
                if (!name.endsWith("*")) {
                    delete obj[name];
                    return;
                }
                Object.keys(obj).forEach(key => {
                    if (key.startsWith(name.substr(0, name.length - 1))) {
                        delete obj[key];
                    }
                });
            });
        }
        function processProductSpecificProperties(obj: api.ProductModel): void {
            delete obj["PrimaryImageFileName"];
            delete obj.Package;
            delete obj.PackageKey;
            delete obj.PackageName;
            delete obj.MasterPack;
            delete obj.MasterPackKey;
            delete obj.MasterPackName;
            delete obj.Pallet;
            delete obj.PalletKey;
            delete obj.PalletName;
            delete obj.VendorAdminID;
            delete obj.inventoryObject;
            delete obj.TotalPurchasedAmount;
            delete obj.TotalPurchasedQuantity;
            delete obj.RestockingFeeAmountCurrency;
            delete obj.RestockingFeeAmountCurrencyKey;
            delete obj.RestockingFeeAmountCurrencyName;
            delete obj.ProductNotifications;
            delete obj.Reviews;
            delete obj.ProductsAssociatedWith;
        }
        function processStatus(obj: api.HaveAStatusModel): void {
            delete obj.Status;
            delete obj.StatusKey;
            delete obj.StatusName;
            delete obj.StatusDisplayName;
            delete obj.StatusTranslationKey;
            delete obj.StatusSortOrder;
        }
        function processType(obj: api.HaveATypeModel<api.TypeModel>): void {
            delete obj.Type;
            delete obj.TypeKey;
            delete obj.TypeName;
            delete obj.TypeDisplayName;
            delete obj.TypeTranslationKey;
            delete obj.TypeSortOrder;
        }
        function processBasePropertiesOfObject(obj: api.BaseModel): void {
            // Remove properties that shouldn't be serialized to a version record
            delete obj.ID;
            delete obj.CreatedDate;
            delete obj.UpdatedDate;
            delete obj["JsonAttributes"];
            delete obj.Hash;
            if (!toSerialize.SerializableAttributes) {
                // Ensure always has a value
                toSerialize.SerializableAttributes = { };
            } else {
                // Remove null attributes, sort the remaining so it's always same order
                const keys = Object.keys(toSerialize.SerializableAttributes);
                for (let i = 0; i < keys.length; i++) {
                    if (!toSerialize.SerializableAttributes[keys[i]]
                        || !toSerialize.SerializableAttributes[keys[i]].Value) {
                        delete toSerialize.SerializableAttributes[keys[i]];
                    }
                }
                const orderedAttrs = { };
                keys.sort().forEach(key => {
                    if (toSerialize.SerializableAttributes[key] === null) { return; } // Don't save null
                    if (toSerialize.SerializableAttributes[key] === undefined) { return; } // Don't save "undefined"
                    orderedAttrs[key] = toSerialize.SerializableAttributes[key];
                });
                toSerialize.SerializableAttributes = orderedAttrs;
            }
        }
        function processRelationshipPropertiesOfObject<TSlaveModel extends api.BaseModel>(
                obj: api.AmARelationshipTableModel<TSlaveModel>,
                ...addNames: string[])
                : void {
            processBasePropertiesOfObject(obj);
            delete obj["Master"];
            delete obj.MasterKey;
            delete obj["MasterName"];
            delete obj.Slave;
            delete obj.SlaveKey;
            delete obj["SlaveName"];
            processNames(obj, ...addNames);
        }
        function processCollectionAssignments<TCollSlaveType extends api.BaseModel>(
                obj: object,
                collectionKey: string,
                ...addRelKeys: string[])
                : void {
            if (!obj[collectionKey]) {
                obj[collectionKey] = [];
            } else if (obj[collectionKey].length) {
                let newColl1 = [];
                obj[collectionKey].forEach(x => {
                    processRelationshipPropertiesOfObject<TCollSlaveType>(x, ...addRelKeys);
                    newColl1.push(sortProperties(x));
                });
                if (newColl1.length > 1
                    && (angular.isDefined(newColl1[0]["MasterID"])
                        || angular.isDefined(newColl1[0]["SlaveID"]))) {
                    newColl1 = _.sortBy(newColl1, ["MasterID","SlaveID"]);
                }
                obj[collectionKey] = newColl1;
            }
        }
        function sortProperties<T extends object>(obj: T): T {
            // Order the properties so it's always the same
            const ordered = <T>{ };
            Object.keys(obj).sort().forEach(key => {
                if (obj[key] === null) { return; } // Don't save null
                if (obj[key] === undefined) { return; } // Don't save "undefined"
                ordered[key] = obj[key];
            });
            return ordered;
        }
        // Clean up Stored Files Assignments
        processCollectionAssignments<api.StoredFileModel>(toSerialize, "StoredFiles", "StoredFile*", "Product*");
        // Clean up Vendor Assignments
        processCollectionAssignments<api.ProductModel>(toSerialize, "Vendors", "Vendor*", "Product*");
        // Clean up Manufacturer Assignments
        processCollectionAssignments<api.ProductModel>(toSerialize, "Manufacturers", "Manufacturer*", "Manufacturer*");
        // Clean up Brands Assignments
        processCollectionAssignments<api.ProductModel>(toSerialize, "Brands", "Brand*", "Product*");
        // Clean up Accounts Assignments
        processCollectionAssignments<api.ProductModel>(toSerialize, "Accounts", "Account*", "Product*");
        // Clean up Categories Assignments
        processCollectionAssignments<api.CategoryModel>(toSerialize, "ProductCategories", "Product*", "Category*");
        // Clean up Images Assignments
        if (!toSerialize.Images) {
            toSerialize.Images = [];
        } else if (toSerialize.Images.length) {
            const newColl2 = [];
            toSerialize.Images.forEach(x => {
                processBasePropertiesOfObject(x);
                processType(x);
                newColl2.push(sortProperties(x));
            });
            toSerialize.Images = newColl2;
        }
        // Clean up Associations Assignments
        if (!toSerialize.ProductAssociations) {
            toSerialize.ProductAssociations = [];
        } else if (toSerialize.ProductAssociations.length) {
            const newColl3 = [];
            toSerialize.ProductAssociations.forEach(x => {
                processRelationshipPropertiesOfObject<api.ProductModel>(
                    x,
                    "PrimaryProduct*",
                    "AssociatedProduct*");
                processType(x);
                processNames(x,
                    "MasterSeoUrl", "MasterPrimaryImageFileName", "MasterIsVisible", "MasterSerializableAttributes",
                    "SlaveSeoUrl",  "SlavePrimaryImageFileName",  "SlaveIsVisible",  "SlaveSerializableAttributes");
                newColl3.push(sortProperties(x));
            });
            toSerialize.ProductAssociations = newColl3;
        }
        // Clean up properties
        processNames(toSerialize,
            "StockQuantity*",
            "TotalPurchasedAmountCurrency*",
            "InCompareCart",
            "InFavoritesList",
            "InNotifyMeList",
            "InWishList",
            "HasChildren",
            "CategoryIDs",
            "version");
        processBasePropertiesOfObject(toSerialize);
        processType(toSerialize);
        processStatus(toSerialize);
        processProductSpecificProperties(toSerialize);
        // Order the properties so it's always the same
        const ordered = sortProperties(toSerialize);
        // Serialize to json and verify all hashKeys are removed
        const json = angular.toJson(ordered)
        const clean = json.replace(/,\s*"\$\$hashKey":\s*"object\:\d+"/, "");
        // Return the final value
        return clean;
    }

    export function mergeProductModelWithVersion(
            current: api.ProductModel,
            name: string,
            serializedRecord: string)
            : api.ProductModel {
        // The selected version to merge
        const toMerge: api.ProductModel = angular.fromJson(serializedRecord);
        // Remove data from the version to merge which should not be applied
        delete current.Package;
        delete current.PackageKey;
        delete current.PackageName;
        delete current.MasterPack;
        delete current.MasterPackKey;
        delete current.MasterPackName;
        delete current.Pallet;
        delete current.PalletKey;
        delete current.PalletName;
        delete current.Status;
        delete current.StatusKey;
        delete current.StatusName;
        delete current.StatusDisplayName;
        delete current.StatusTranslationKey;
        delete current.StatusSortOrder;
        delete current.Type;
        delete current.TypeKey;
        delete current.TypeName;
        delete current.TypeDisplayName;
        delete current.TypeTranslationKey;
        delete current.TypeSortOrder;
        // Merge the data
        angular.merge(current, toMerge, { version: name });
        // Finish and exit out
        return current;
    }
}
