/**
* @file framework/admin/controls/types/pageViewType.detail.ts
* @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
* @desc page view types detail/editor class
* @auto-generated This file was auto-generated by the T4 template types.tt in the UI project
*/
module cef.admin.controls.types {
class PageViewTypeDetailController extends DetailBaseController<api.TypeModel> {
// Forced overrides
detailName = "Page View Type";
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
// Page View Type Specific Properties
};
return this.$q.resolve(this.record);
}
constructorPreAction(): ng.IPromise<void> {
this.detailName = "Page View Type";
return this.$q.resolve();
}
loadRecordCall(id: number): ng.IHttpPromise<api.TypeModel> {
return this.cvApi.tracking.GetPageViewTypeByID(id);
}
createRecordCall(routeParams?: api.TypeModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.tracking.CreatePageViewType(routeParams);
}
updateRecordCall(routeParams?: api.TypeModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.tracking.UpdatePageViewType(routeParams);
}
deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.tracking.DeactivatePageViewTypeByID(id);
}
reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.tracking.ReactivatePageViewTypeByID(id);
}
deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.tracking.DeletePageViewTypeByID(id);
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

adminApp.directive("pageViewTypeDetail", ($filter: ng.IFilterService): ng.IDirective => ({
restrict: "A",
templateUrl: $filter("corsLink")("/framework/admin/controls/types/pageViewTypes.detail.html", "ui"),
controller: PageViewTypeDetailController,
controllerAs: "pageViewTypeDetailCtrl",
bindToController: true
}));
}
