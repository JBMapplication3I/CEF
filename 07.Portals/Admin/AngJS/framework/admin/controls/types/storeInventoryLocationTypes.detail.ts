/**
* @file framework/admin/controls/types/storeInventoryLocationType.detail.ts
* @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
* @desc store inventory location types detail/editor class
* @auto-generated This file was auto-generated by the T4 template types.tt in the UI project
*/
module cef.admin.controls.types {
class StoreInventoryLocationTypeDetailController extends DetailBaseController<api.TypeModel> {
// Forced overrides
detailName = "Store Inventory Location Type";
// Collections
// <None>
// UI Data
// <None>
// Required Functions
loadNewRecord(): ng.IPromise<api.TypeModel> {
this.record = <api.TypeModel>{
// Base Properties
ID: null,
CustomKey: null,
Active: true,
CreatedDate: new Date(),
UpdatedDate: null,
JsonAttributes: null,
SerializableAttributes: new api.SerializableAttributesDictionary(),
Hash: null,
// NameableBase Properties
Name: null,
Description: null,
// Displayable Base Properties
DisplayName: null,
SortOrder: null,
TranslationKey: null,
// Store Inventory Location Type Specific Properties
};
return this.$q.resolve(this.record);
}
constructorPreAction(): ng.IPromise<void> {
this.detailName = "Store Inventory Location Type";
return this.$q.resolve();
}
loadRecordCall(id: number): ng.IHttpPromise<api.TypeModel> {
return this.cvApi.stores.GetStoreInventoryLocationTypeByID(id);
}
createRecordCall(routeParams?: api.TypeModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.stores.CreateStoreInventoryLocationType(routeParams);
}
updateRecordCall(routeParams?: api.TypeModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.stores.UpdateStoreInventoryLocationType(routeParams);
}
deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.stores.DeactivateStoreInventoryLocationTypeByID(id);
}
reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.stores.ReactivateStoreInventoryLocationTypeByID(id);
}
deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.stores.DeleteStoreInventoryLocationTypeByID(id);
}
// Supportive Functions
// <None>
// Constructor
constructor(
protected readonly $scope: ng.IScope,
protected readonly $translate: ng.translate.ITranslateService,
protected readonly $stateParams: ng.ui.IStateParamsService,
protected readonly $state: ng.ui.IStateService,
protected readonly $window: ng.IWindowService,
protected readonly $q: ng.IQService,
protected readonly cefConfig: core.CefConfig,
protected readonly cvApi: api.ICEFAPI,
protected readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
protected readonly $filter: ng.IFilterService) {
super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
}
}

adminApp.directive("storeInventoryLocationTypeDetail", ($filter: ng.IFilterService): ng.IDirective => ({
restrict: "A",
templateUrl: $filter("corsLink")("/framework/admin/controls/types/storeInventoryLocationTypes.detail.html", "ui"),
controller: StoreInventoryLocationTypeDetailController,
controllerAs: "storeInventoryLocationTypeDetailCtrl",
bindToController: true
}));
}
