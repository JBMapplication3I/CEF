/**
* @file framework/admin/controls/types/referralCodeStatus.detail.ts
* @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
* @desc referral code statuses detail/editor class
* @auto-generated This file was auto-generated by the T4 template types.tt in the UI project
*/
module cef.admin.controls.types {
class ReferralCodeStatusDetailController extends DetailBaseController<api.StatusModel> {
// Forced overrides
detailName = "Referral Code Status";
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
// Referral Code Status Specific Properties
};
return this.$q.resolve(this.record);
}
constructorPreAction(): ng.IPromise<void> {
this.detailName = "Referral Code Status";
return this.$q.resolve();
}
loadRecordCall(id: number): ng.IHttpPromise<api.StatusModel> {
return this.cvApi.contacts.GetReferralCodeStatusByID(id);
}
createRecordCall(routeParams?: api.StatusModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.contacts.CreateReferralCodeStatus(routeParams);
}
updateRecordCall(routeParams?: api.StatusModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
return this.cvApi.contacts.UpdateReferralCodeStatus(routeParams);
}
deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.contacts.DeactivateReferralCodeStatusByID(id);
}
reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.contacts.ReactivateReferralCodeStatusByID(id);
}
deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
return this.cvApi.contacts.DeleteReferralCodeStatusByID(id);
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

adminApp.directive("referralCodeStatusDetail", ($filter: ng.IFilterService): ng.IDirective => ({
restrict: "A",
templateUrl: $filter("corsLink")("/framework/admin/controls/types/referralCodeStatuses.detail.html", "ui"),
controller: ReferralCodeStatusDetailController,
controllerAs: "referralCodeStatusDetailCtrl",
bindToController: true
}));
}
