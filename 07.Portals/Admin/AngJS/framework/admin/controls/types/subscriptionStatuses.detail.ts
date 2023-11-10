/**
* @file framework/admin/controls/types/subscriptionStatus.detail.ts
* @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
* @desc subscription statuses detail/editor class
* @auto-generated This file was auto-generated by the T4 template types.tt in the UI project
*/
module cef.admin.controls.types {
class SubscriptionStatusDetailController extends DetailBaseController<api.StatusModel> {
// Forced overrides
detailName = "Subscription Status";
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
// Subscription Status Specific Properties
};
return this.$q.resolve(this.record);
}
constructorPreAction(): ng.IPromise<void> {
this.detailName = "Subscription Status";
return this.$q.resolve();
}
loadRecordCall(id: number): ng.IHttpPromise<api.StatusModel> {
return this.cvApi.payments.GetSubscriptionStatusByID(id);
}
createRecordCall(routeParams?: api.StatusModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.payments.CreateSubscriptionStatus(routeParams);
}
updateRecordCall(routeParams?: api.StatusModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.payments.UpdateSubscriptionStatus(routeParams);
}
deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.payments.DeactivateSubscriptionStatusByID(id);
}
reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.payments.ReactivateSubscriptionStatusByID(id);
}
deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.payments.DeleteSubscriptionStatusByID(id);
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

adminApp.directive("subscriptionStatusDetail", ($filter: ng.IFilterService): ng.IDirective => ({
restrict: "A",
templateUrl: $filter("corsLink")("/framework/admin/controls/types/subscriptionStatuses.detail.html", "ui"),
controller: SubscriptionStatusDetailController,
controllerAs: "subscriptionStatusDetailCtrl",
bindToController: true
}));
}
