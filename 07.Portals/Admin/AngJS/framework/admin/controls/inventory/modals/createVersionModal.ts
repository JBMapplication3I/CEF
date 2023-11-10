module cef.admin.controls.inventory.modals {
    export class CreateVersionModalController<TRecordModel extends api.BaseModel>
            extends core.TemplatedControllerBase {
        // Properties
        types: api.TypeModel[] = [];
        selectedRecordVersion: api.RecordVersionModel;
        get selectedRecordVersionName(): string {
            return this.selectedRecordVersion && this.selectedRecordVersion.Name;
        }
        // Functions
        ok(): void {
            this.setRunning();
            if (this.versionOnly && !this.record.ID) {
                this.finishRunning(true, "ERROR: Cannot save a version only for a record that hasn't had it's initial save yet.");
                return;
            }
            if (!this.record.ID) {
                this.cvApi.products.CreateProduct(this.record).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.finishRunning(true, "No data returned from Create call", r && r.data && r.data.Messages);
                        return;
                    }
                    this.selectedRecordVersion.RecordID = r.data.Result;
                    this.cvApi.structure.CreateRecordVersion(this.selectedRecordVersion).then(r2 => {
                        if (!r2 || !r2.data || !r2.data.ActionSucceeded) {
                            this.finishRunning(true, "No data returned from create version call", r2 && r2.data && r2.data.Messages);
                            return;
                        }
                        this.cvApi.structure.GetRecordVersionByID(r2.data.Result).then(r3 => {
                            this.selectedRecordVersion = r3.data;
                            this.cvApi.products.AdminGetProductFull(this.selectedRecordVersion.RecordID).then(r4 => {
                                if (!r4 || !r4.data) {
                                    this.finishRunning(true, "No data returned from get call");
                                    return;
                                }
                                const record = r4.data;
                                record["version"] = this.selectedRecordVersionName;
                                this.$uibModalInstance.close(<{ record: TRecordModel, version: api.RecordVersionModel }>{
                                    record: record as any,
                                    version: this.selectedRecordVersion
                                });
                            }).catch(reason4 => this.finishRunning(true, reason4));
                        }).catch(reason3 => this.finishRunning(true, reason3));
                    }).catch(reason2 => this.finishRunning(true, reason2));
                }).catch(reason => this.finishRunning(true, reason));
                return;
            }
            (this.versionOnly
                ? this.$q(resolve => resolve({ data: this.record }))
                : this.cvApi.products.UpdateProduct(this.record))
            .then((r: ng.IHttpPromiseCallbackArg<TRecordModel>) => {
                if (!r || !r.data) {
                    this.finishRunning(true, "No data returned from Update call");
                    return;
                }
                const newName = this.selectedRecordVersion.Name;
                const newDesc = this.selectedRecordVersion.Description;
                this.selectedRecordVersion = <api.RecordVersionModel>{
                    // Base Properties
                    ID: 0,
                    CustomKey: null,
                    Active: true,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    Hash: null,
                    SerializableAttributes: new api.SerializableAttributesDictionary(),
                    // NameableBase Properties
                    Name: newName,
                    Description: newDesc,
                    // IHaveATypeBase Properties
                    TypeID: this.types[0].ID,
                    Type: this.types[0],
                    TypeKey: this.types[0].CustomKey,
                    TypeName: this.types[0].Name,
                    TypeDisplayName: this.types[0].DisplayName,
                    TypeTranslationKey: this.types[0].TranslationKey,
                    TypeSortOrder: this.types[0].SortOrder,
                    // IAmFilterableByBrands Properties
                    BrandID: 0,
                    Brand: null,
                    BrandKey: null,
                    BrandName: null,
                    // IAmFilterableByStores Properties
                    StoreID: 0,
                    Store: null,
                    StoreKey: null,
                    StoreName: null,
                    StoreSeoUrl: null,
                    // RecordVersion Properties
                    IsDraft: false,
                    OriginalPublishDate: null,
                    MostRecentPublishDate: null,
                    RecordID: r.data.ID,
                    SerializedRecord: productModelToVersionJson(r.data as any),
                    // Related Objects
                    PublishedByUserID: 0,
                    PublishedByUser: null,
                    PublishedByUserKey: null,
                };
                this.cvApi.structure.CreateRecordVersion(this.selectedRecordVersion).then(r2 => {
                    if (!r2 || !r2.data) {
                        this.finishRunning(true, "No data returned from create version call");
                        return;
                    }
                    this.cvApi.products.AdminGetProductFull(r2.data.Result).then(r3 => {
                        if (!r3 || !r3.data) {
                            this.finishRunning(true, "No data returned from get call");
                            return;
                        }
                        const record = r3.data;
                        record["version"] = this.selectedRecordVersionName;
                        this.cvApi.structure.GetRecordVersionByID(r2.data.Result).then(r4 => {
                            this.$uibModalInstance.close(<{ record: TRecordModel, version: api.RecordVersionModel }>{
                                record: record as any,
                                version: r4.data
                            });
                        }).catch(reason4 => this.finishRunning(true, reason4));
                    }).catch(reason3 => this.finishRunning(true, reason3));
                }).catch(reason2 => this.finishRunning(true, reason2));
            }).catch(reason => this.finishRunning(true, reason));
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        onOpen(): void {
            this.setRunning();
            this.cvApi.structure.GetRecordVersionTypes({ Active: true, AsListing: true }).then(r => {
                this.types = r.data.Results;
                this.selectedRecordVersion = <api.RecordVersionModel>{
                    // Base Properties
                    ID: 0,
                    CustomKey: null,
                    Active: true,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    Hash: null,
                    SerializableAttributes: new api.SerializableAttributesDictionary(),
                    // NameableBase Properties
                    Name: (this.record.ID > 0 ? "New" : "Initial") + " Version " + this.$filter("date")(new Date(), "yyyy MMM dd hh:mm a"),
                    Description: null,
                    // IHaveATypeBase Properties
                    TypeID: this.types[0].ID,
                    Type: this.types[0],
                    TypeKey: this.types[0].CustomKey,
                    TypeName: this.types[0].Name,
                    TypeDisplayName: this.types[0].DisplayName,
                    TypeTranslationKey: this.types[0].TranslationKey,
                    TypeSortOrder: this.types[0].SortOrder,
                    // IAmFilterableByBrands Properties
                    BrandID: 0,
                    Brand: null,
                    BrandKey: null,
                    BrandName: null,
                    // IAmFilterableByStores Properties
                    StoreID: 0,
                    Store: null,
                    StoreKey: null,
                    StoreName: null,
                    StoreSeoUrl: null,
                    // RecordVersion Properties
                    IsDraft: false,
                    OriginalPublishDate: null,
                    MostRecentPublishDate: null,
                    RecordID: this.record.ID || null,
                    SerializedRecord: productModelToVersionJson(this.record),
                    // Related Objects
                    PublishedByUserID: 0,
                    PublishedByUser: null,
                    PublishedByUserKey: null,
                };
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Events
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly record: api.ProductModel,
                private readonly versionOnly: boolean) {
            super(cefConfig);
            this.onOpen();
        }
    }

    export interface ICreateVersionModalFactory<TRecordModel extends api.BaseModel> {
        (record?: TRecordModel, versionOnly?: boolean): ng.IPromise<{ record: TRecordModel, version: api.RecordVersionModel }>;
    }

    export const cvCreateVersionModalFactoryFn = <TRecordModel extends api.BaseModel>(
            $uibModal: ng.ui.bootstrap.IModalService,
            $filter: ng.IFilterService,
            cvServiceStrings: services.IServiceStrings)
            : ICreateVersionModalFactory<TRecordModel> =>
        (record?: TRecordModel, versionOnly: boolean = false): ng.IPromise<{ record: TRecordModel, version: api.RecordVersionModel }> => {
            const modalFunc = <TRecordModel extends api.BaseModel>() => {
                return $uibModal.open({
                    size: cvServiceStrings.modalSizes.lg,
                    backdrop: "static",
                    templateUrl: $filter("corsLink")("/framework/admin/controls/inventory/modals/createVersionModal.html", "ui"),
                    controller: CreateVersionModalController,
                    controllerAs: "createVersionModalCtrl",
                    resolve: {
                        record: () => record,
                        versionOnly: () => versionOnly
                    }
                }).result.then((result: { record: TRecordModel, version: api.RecordVersionModel }) => result);
            };
            return modalFunc<TRecordModel>();
        };

    adminApp.factory("cvCreateVersionModalFactory", modals.cvCreateVersionModalFactoryFn);
}
