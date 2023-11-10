/**
* @file framework/admin/controls/types/eventStatus.detail.ts
* @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
* @desc event statuses detail/editor class
* @auto-generated This file was auto-generated by the T4 template types.tt in the UI project
*/
module cef.admin.controls.types {
class EventStatusDetailController extends DetailBaseController<api.StatusModel> {
// Forced overrides
detailName = "Event Status";
// Collections
// <None>
// UI Data
// <None>
// Required Functions
loadNewRecord(): ng.IPromise<api.StatusModel> {
this.record = <api.StatusModel>{
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
// Event Status Specific Properties
};
return this.$q.resolve(this.record);
}
constructorPreAction(): ng.IPromise<void> {
this.detailName = "Event Status";
return this.$q.resolve();
}
loadRecordCall(id: number): ng.IHttpPromise<api.StatusModel> {
return this.cvApi.tracking.GetEventStatusByID(id);
}
createRecordCall(routeParams?: api.StatusModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.tracking.CreateEventStatus(routeParams);
}
updateRecordCall(routeParams?: api.StatusModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.tracking.UpdateEventStatus(routeParams);
}
deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.tracking.DeactivateEventStatusByID(id);
}
reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.tracking.ReactivateEventStatusByID(id);
}
deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.tracking.DeleteEventStatusByID(id);
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

adminApp.directive("eventStatusDetail", ($filter: ng.IFilterService): ng.IDirective => ({
restrict: "A",
templateUrl: $filter("corsLink")("/framework/admin/controls/types/eventStatuses.detail.html", "ui"),
controller: EventStatusDetailController,
controllerAs: "eventStatusDetailCtrl",
bindToController: true
}));
}
