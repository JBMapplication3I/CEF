/**
 * @file framework/admin/controls/_shared/adminDetailHasProductAssociatorBase.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Admin detail has product associator base class
 */
module cef.admin.controls.shared {
    export class ProductSearchParams { ProductCode: string; ProductName: string; }
    export interface ProductListViewModel extends api.ProductModel { Selected?: boolean; Quantity?: number; }
    export class ProductDetailDisplaySetting { ShowAddProductsPanel: boolean; }

    export abstract class AdminDetailHasProductAssociatorBase<TRecordModel extends api.BaseModel>
        extends DetailBaseController<TRecordModel> {
        // Properties
        products: ProductListViewModel[] = [];
        productsToRemove: any[] = [];
        productSearchParams: ProductSearchParams = { ProductCode: null, ProductName: null };
        display: ProductDetailDisplaySetting = { ShowAddProductsPanel: false };
        abstract productCollectionPropertyName: string;
        // Store Product Management Events
        showAddProductsPanel(): void { this.display.ShowAddProductsPanel = true; }
        hideAddProductsPanel(): void { this.display.ShowAddProductsPanel = false; }
        searchProducts(): void {
            this.cvApi.products.GetProducts(<api.GetProductsDto>{
                Active: true,
                AsListing: true,
                Paging: <api.Paging>{ Size: 50, StartIndex: 1 },
                Name: this.productSearchParams.ProductName,
                CustomKey: this.productSearchParams.ProductCode
            }).then(r => this.products = r.data.Results);
        }
        addProducts(): void {
            if (!this.record[this.productCollectionPropertyName]) {
                this.record[this.productCollectionPropertyName] = [];
            }
            this.products.forEach(p => {
                if (p.Selected) {
                    this.addProduct(p.ID);
                }
            });
            this.hideAddProductsPanel();
            this.forms["Products"].$setDirty();
        }
        addProduct(id: number): void {
            if (!this.record[this.productCollectionPropertyName]) {
                this.record[this.productCollectionPropertyName] = [];
            }
            let changed = false;
            this.products.forEach(p => {
                if (p.ID !== id) { return; }
                const existing = _.some(this.record[this.productCollectionPropertyName], x => x["ProductID"] == p.ID);
                if (existing) { return; } // Prevent Duplicates
                // TODO: Setting to force allow duplicates, possibly by a cluster index style predicate checker
                this.record[this.productCollectionPropertyName].push({
                    // Base Properties
                    ID: 0,
                    CustomKey: null,
                    Active: true,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    // Relationship Properties
                    SlaveID: id,
                    SlaveKey: p.CustomKey,
                    SlaveName: p.Name,
                    ProductID: id,
                    ProductKey: p.CustomKey,
                    ProductName: p.Name,
                });
                changed = true;
            });
            if (changed) {
                this.forms["Products"].$setDirty();
            }
            this.hideAddProductsPanel();
        }
        removeProduct(index: number): void {
            const toRemove = (this.record[this.productCollectionPropertyName] as any[]).splice(index, 1);
            this.productsToRemove.push(toRemove[0]);
            this.forms["Products"].$setDirty();
        }
        // NOTE: This must remain a variable so it can be overridden
        saveHook = (id: number): ng.IPromise<number> => {
            // Do Nothing
            return this.$q.resolve(id);
        }
    }
}
