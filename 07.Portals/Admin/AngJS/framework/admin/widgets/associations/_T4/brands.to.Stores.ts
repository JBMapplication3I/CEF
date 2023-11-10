/**
 * @file framework/admin/widgets/associations/_T4/brand.to.Stores.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Associator for Brand to Stores class
 * @auto-generated This file was auto-generated by the T4 template associators.tt in the UI project
 */
module cef.admin.widgets.associators.Brands {
	class AssociatorForBrandToStoresController extends core.TemplatedControllerBase {
		// Bound Scope Properties
		record: api.BrandModel;
		ctrl: core.TemplatedControllerBase;
		// Properties
		propertyName: string = "Stores";
		paging: core.ServerSidePaging<api.StoreModel, api.StorePagedResults>;
		assigned: core.ServerSidePaging<api.BrandStoreModel, api.BrandStorePagedResults>;
		// Convenience Redirects
		// <None>
		// Functions
		load(): void {
			this.setRunning();
			const paging = <api.Paging>{ Size: 500, StartIndex: 1 };
			const standardDto = {
				Active: true,
				AsListing: true,
				Paging: paging
			};
			this.$q.all([
				this.$q.resolve(null)
			]).then((rarr: ng.IHttpPromiseCallbackArg<any>[]) => {
				let index = 0;
				this.paging = new core.ServerSidePaging<api.StoreModel, api.StorePagedResults>(
					this.$rootScope,
					this.$scope,
					this.$filter,
					this.$q,
					this.cvServiceStrings,
					this.cvApi.stores.GetStores, 8, `brand-${this.record && this.record.ID}.to.Stores.available`, null,
					() => {
						return {
							Active: true,
							AsListing: true,
							__caller: `brand-${this.record && this.record.ID}.to.Stores.available`
						};
					},
					() => !this.record || !this.record.ID);
				this.assigned = new core.ServerSidePaging<api.BrandStoreModel, api.BrandStorePagedResults>(
					this.$rootScope,
					this.$scope,
					this.$filter,
					this.$q,
					this.cvServiceStrings,
					this.cvApi.brands.GetBrandStores, 8, `brand-${this.record && this.record.ID}.to.Stores.assigned`, null,
					() => {
						return {
							Active: true,
							AsListing: true,
							MasterID: this.record && this.record.ID || 0,
							__caller: `brand-${this.record && this.record.ID}.to.Stores.assigned`
						};
					},
					() => !this.record || !this.record.ID);
			}).finally(() => this.finishRunning());
		}
		add(id: number): void {
			if (!id) {
				return;
			}
			if (!this.record || !this.record.ID) {
				this.cvMessageModalFactory(this.$translate("ui.admin.associators.errors.YouMustSaveTheRecordFirst"))
					.finally(() => { /* Do Nothing */ });
				return;
			}
			// Ensure the data is loaded
			const model = _.find(this.paging.dataUnpaged, x => x.ID === id);
			if (!model) {
				return;
			}
			// Ensure it's not already in the collection
			if (_.find(this.assigned.dataUnpaged, x => x["SlaveID"] === model.ID)) {
				this.cvMessageModalFactory(this.$translate("ui.admin.associators.errors.ThisIsAlreadyInTheCollection"))
					.finally(() => { /* Do Nothing */ });
				return;
			}
			this.setRunning();
			const dupeCheckDto = {
				Active: true,
				AsListing: true,
				MasterID: this.record.ID,
				SlaveID: id,
				"__caller": `brand-${this.record && this.record.ID}.to.Stores.dupe-check`
			};
			this.cvApi.brands.GetBrandStores(dupeCheckDto).then(r => {
				if (!r || !r.data || !r.data.Results) {
					this.cvMessageModalFactory(this.$translate("ui.admin.associators.errors.CouldNotRetrieveDataFromDuplicateCheck"))
						.finally(() =>
							this.finishRunning(true, this.$translate("ui.admin.associators.errors.CouldNotRetrieveDataFromDuplicateCheck")));
					return;
				}
				if (r.data.Results.length) {
					this.cvMessageModalFactory(this.$translate("ui.admin.associators.errors.ThisIsAlreadyInTheCollection"))
						.finally(() =>
							this.finishRunning(true, this.$translate("ui.admin.associators.errors.ThisIsAlreadyInTheCollection")));
					return;
				}
				// Add it
				this.cvApi.brands.CreateBrandStore(<api.BrandStoreModel>{
					// Base Properties
					ID: null,
					Active: true,
					CreatedDate: new Date(),
					MasterID: this.record.ID,
					SlaveID: model.ID,
					// BrandStore Properties
					IsVisibleIn: false,
				}).then(rc => {
					if (!rc || !rc.data) {
						this.finishRunning(true, "ERROR! Failed to create the association in the server");
						return;
					}
					if (this.record[this.propertyName]) {
						this.record[this.propertyName] = null; // Ensure we are only setting the new way
					}
					this.assigned.resetAll(); // Pull updated data
					this.assigned.search(); // Pull updated data
					// this.forms[this.propertyName].$setDirty();
					this.finishRunning();
				}).catch(reason => this.finishRunning(true, reason));
			}).catch(reason => this.finishRunning(true, reason));
		}
		remove(toRemove: api.BrandStoreModel): void {
			this.cvConfirmModalFactory(
				this.$translate("ui.admin.common.AreYouSureYouWantToRemoveThisAssociation.Question")
			).then(result => {
				if (!result) {
					return;
				}
				this.cvApi.brands.DeactivateBrandStoreByID(toRemove.ID).then(r => {
					if (!r || !r.data) {
						this.finishRunning(true, "ERROR! Failed to disassociate the record in the server.");
						return;
					}
					if (this.record[this.propertyName]) {
						this.record[this.propertyName] = null; // Ensure we are only setting the new way
					}
					this.assigned.resetAll(); // Pull updated data
					this.assigned.search(); // Pull updated data
					// this.forms[this.propertyName].$setDirty();
					this.cvMessageModalFactory(this.$translate("ui.admin.associators.success.TheRecordHasBeenDisassociatedOnTheServer"))
						.finally(() => this.finishRunning());
				}).catch(reason => this.finishRunning(true, reason));
				/*
				for (let i = 0; i < this.record[this.propertyName].length; i++) {
					if (toRemove === this.record[this.propertyName][i]) {
						this.record[this.propertyName].splice(i, 1);
						this.forms[this.propertyName].$setDirty();
						return;
					}
				}
				*/
			});
		}
		// Events
		// <None>
		// Constructor
		constructor(
				private readonly $rootScope: ng.IRootScopeService,
				private readonly $scope: ng.IScope,
				private readonly $translate: ng.translate.ITranslateService,
				private readonly $q: ng.IQService,
				private readonly $filter: ng.IFilterService,
				protected readonly cefConfig: core.CefConfig,
				private readonly cvServiceStrings: services.IServiceStrings,
				private readonly cvApi: api.ICEFAPI,
				private readonly cvMessageModalFactory: modals.IMessageModalFactory,
				private readonly cvConfirmModalFactory: modals.IConfirmModalFactory) {
			super(cefConfig);
			this.load();
		}
	}

	adminApp.directive("cefBrandToStoresAssociator", ($filter: ng.IFilterService): ng.IDirective => ({
		restrict: "A",
		scope: {
			record: "=",
			ctrl: "="
		},
		templateUrl: $filter("corsLink")("/framework/admin/widgets/associations/_T4/brands.to.Stores.html", "ui"),
		controller: AssociatorForBrandToStoresController,
		controllerAs: "asCtrl",
		bindToController: true
	}));
}
