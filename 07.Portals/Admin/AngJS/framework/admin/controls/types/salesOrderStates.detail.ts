/**
* @file framework/admin/controls/types/salesOrderState.detail.ts
* @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
* @desc sales order states detail/editor class
* @auto-generated This file was auto-generated by the T4 template types.tt in the UI project
*/
module cef.admin.controls.types {
class SalesOrderStateDetailController extends DetailBaseController<api.StateModel> {
// Forced overrides
detailName = "Sales Order State";
// Collections
// <None>
// UI Data
// <None>
// Required Functions
loadNewRecord(): ng.IPromise<api.StateModel> {
this.record = <api.StateModel>{
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
// Sales Order State Specific Properties
};
return this.$q.resolve(this.record);
}
constructorPreAction(): ng.IPromise<void> {
this.detailName = "Sales Order State";
return this.$q.resolve();
}
loadRecordCall(id: number): ng.IHttpPromise<api.StateModel> {
return this.cvApi.ordering.GetSalesOrderStateByID(id);
}
createRecordCall(routeParams?: api.StateModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.ordering.CreateSalesOrderState(routeParams);
}
updateRecordCall(routeParams?: api.StateModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.ordering.UpdateSalesOrderState(routeParams);
}
deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.ordering.DeactivateSalesOrderStateByID(id);
}
reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.ordering.ReactivateSalesOrderStateByID(id);
}
deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.ordering.DeleteSalesOrderStateByID(id);
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

adminApp.directive("salesOrderStateDetail", ($filter: ng.IFilterService): ng.IDirective => ({
restrict: "A",
templateUrl: $filter("corsLink")("/framework/admin/controls/types/salesOrderStates.detail.html", "ui"),
controller: SalesOrderStateDetailController,
controllerAs: "salesOrderStateDetailCtrl",
bindToController: true
}));
}
