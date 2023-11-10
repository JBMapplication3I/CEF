/**
 * @file framework/admin/controls/types/types.searchController.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Types search controller class (admin)
 */
module cef.admin.controls.types {
export class SearchTypeController {
constructor(
private readonly $http: ng.IHttpService,
private readonly $scope,
private readonly cvApi: api.ICEFAPI) { }

// ===== Types & Statuses =====
// Account Association Types
getAccountAssociationTypes = this.cvApi.accounts.GetAccountAssociationTypes;
deactivateAccountAssociationType = (id: number) => { return this.cvApi.accounts.DeactivateAccountAssociationTypeByID(id); };
reactivateAccountAssociationType = (id: number) => { return this.cvApi.accounts.ReactivateAccountAssociationTypeByID(id); };
deleteAccountAssociationType = (id: number) => { return this.cvApi.accounts.DeleteAccountAssociationTypeByID(id); };
// Account Image Types
getAccountImageTypes = this.cvApi.accounts.GetAccountImageTypes;
deactivateAccountImageType = (id: number) => { return this.cvApi.accounts.DeactivateAccountImageTypeByID(id); };
reactivateAccountImageType = (id: number) => { return this.cvApi.accounts.ReactivateAccountImageTypeByID(id); };
deleteAccountImageType = (id: number) => { return this.cvApi.accounts.DeleteAccountImageTypeByID(id); };
// Account Product Types
getAccountProductTypes = this.cvApi.accounts.GetAccountProductTypes;
deactivateAccountProductType = (id: number) => { return this.cvApi.accounts.DeactivateAccountProductTypeByID(id); };
reactivateAccountProductType = (id: number) => { return this.cvApi.accounts.ReactivateAccountProductTypeByID(id); };
deleteAccountProductType = (id: number) => { return this.cvApi.accounts.DeleteAccountProductTypeByID(id); };
// Account Statuses
getAccountStatuses = this.cvApi.accounts.GetAccountStatuses;
deactivateAccountStatus = (id: number) => { return this.cvApi.accounts.DeactivateAccountStatusByID(id); };
reactivateAccountStatus = (id: number) => { return this.cvApi.accounts.ReactivateAccountStatusByID(id); };
deleteAccountStatus = (id: number) => { return this.cvApi.accounts.DeleteAccountStatusByID(id); };
// Account Types
getAccountTypes = this.cvApi.accounts.GetAccountTypes;
deactivateAccountType = (id: number) => { return this.cvApi.accounts.DeactivateAccountTypeByID(id); };
reactivateAccountType = (id: number) => { return this.cvApi.accounts.ReactivateAccountTypeByID(id); };
deleteAccountType = (id: number) => { return this.cvApi.accounts.DeleteAccountTypeByID(id); };
// Ad Image Types
getAdImageTypes = this.cvApi.advertising.GetAdImageTypes;
deactivateAdImageType = (id: number) => { return this.cvApi.advertising.DeactivateAdImageTypeByID(id); };
reactivateAdImageType = (id: number) => { return this.cvApi.advertising.ReactivateAdImageTypeByID(id); };
deleteAdImageType = (id: number) => { return this.cvApi.advertising.DeleteAdImageTypeByID(id); };
// Ad Statuses
getAdStatuses = this.cvApi.advertising.GetAdStatuses;
deactivateAdStatus = (id: number) => { return this.cvApi.advertising.DeactivateAdStatusByID(id); };
reactivateAdStatus = (id: number) => { return this.cvApi.advertising.ReactivateAdStatusByID(id); };
deleteAdStatus = (id: number) => { return this.cvApi.advertising.DeleteAdStatusByID(id); };
// Ad Types
getAdTypes = this.cvApi.advertising.GetAdTypes;
deactivateAdType = (id: number) => { return this.cvApi.advertising.DeactivateAdTypeByID(id); };
reactivateAdType = (id: number) => { return this.cvApi.advertising.ReactivateAdTypeByID(id); };
deleteAdType = (id: number) => { return this.cvApi.advertising.DeleteAdTypeByID(id); };
// Zone Statuses
getZoneStatuses = this.cvApi.advertising.GetZoneStatuses;
deactivateZoneStatus = (id: number) => { return this.cvApi.advertising.DeactivateZoneStatusByID(id); };
reactivateZoneStatus = (id: number) => { return this.cvApi.advertising.ReactivateZoneStatusByID(id); };
deleteZoneStatus = (id: number) => { return this.cvApi.advertising.DeleteZoneStatusByID(id); };
// Zone Types
getZoneTypes = this.cvApi.advertising.GetZoneTypes;
deactivateZoneType = (id: number) => { return this.cvApi.advertising.DeactivateZoneTypeByID(id); };
reactivateZoneType = (id: number) => { return this.cvApi.advertising.ReactivateZoneTypeByID(id); };
deleteZoneType = (id: number) => { return this.cvApi.advertising.DeleteZoneTypeByID(id); };
// Attribute Groups
getAttributeGroups = this.cvApi.attributes.GetAttributeGroups;
deactivateAttributeGroup = (id: number) => { return this.cvApi.attributes.DeactivateAttributeGroupByID(id); };
reactivateAttributeGroup = (id: number) => { return this.cvApi.attributes.ReactivateAttributeGroupByID(id); };
deleteAttributeGroup = (id: number) => { return this.cvApi.attributes.DeleteAttributeGroupByID(id); };
// Attribute Tabs
getAttributeTabs = this.cvApi.attributes.GetAttributeTabs;
deactivateAttributeTab = (id: number) => { return this.cvApi.attributes.DeactivateAttributeTabByID(id); };
reactivateAttributeTab = (id: number) => { return this.cvApi.attributes.ReactivateAttributeTabByID(id); };
deleteAttributeTab = (id: number) => { return this.cvApi.attributes.DeleteAttributeTabByID(id); };
// Attribute Types
getAttributeTypes = this.cvApi.attributes.GetAttributeTypes;
deactivateAttributeType = (id: number) => { return this.cvApi.attributes.DeactivateAttributeTypeByID(id); };
reactivateAttributeType = (id: number) => { return this.cvApi.attributes.ReactivateAttributeTypeByID(id); };
deleteAttributeType = (id: number) => { return this.cvApi.attributes.DeleteAttributeTypeByID(id); };
// Auction Statuses
getAuctionStatuses = this.cvApi.auctions.GetAuctionStatuses;
deactivateAuctionStatus = (id: number) => { return this.cvApi.auctions.DeactivateAuctionStatusByID(id); };
reactivateAuctionStatus = (id: number) => { return this.cvApi.auctions.ReactivateAuctionStatusByID(id); };
deleteAuctionStatus = (id: number) => { return this.cvApi.auctions.DeleteAuctionStatusByID(id); };
// Auction Types
getAuctionTypes = this.cvApi.auctions.GetAuctionTypes;
deactivateAuctionType = (id: number) => { return this.cvApi.auctions.DeactivateAuctionTypeByID(id); };
reactivateAuctionType = (id: number) => { return this.cvApi.auctions.ReactivateAuctionTypeByID(id); };
deleteAuctionType = (id: number) => { return this.cvApi.auctions.DeleteAuctionTypeByID(id); };
// Bid Statuses
getBidStatuses = this.cvApi.auctions.GetBidStatuses;
deactivateBidStatus = (id: number) => { return this.cvApi.auctions.DeactivateBidStatusByID(id); };
reactivateBidStatus = (id: number) => { return this.cvApi.auctions.ReactivateBidStatusByID(id); };
deleteBidStatus = (id: number) => { return this.cvApi.auctions.DeleteBidStatusByID(id); };
// Lot Groups
getLotGroups = this.cvApi.auctions.GetLotGroups;
deactivateLotGroup = (id: number) => { return this.cvApi.auctions.DeactivateLotGroupByID(id); };
reactivateLotGroup = (id: number) => { return this.cvApi.auctions.ReactivateLotGroupByID(id); };
deleteLotGroup = (id: number) => { return this.cvApi.auctions.DeleteLotGroupByID(id); };
// Lot Statuses
getLotStatuses = this.cvApi.auctions.GetLotStatuses;
deactivateLotStatus = (id: number) => { return this.cvApi.auctions.DeactivateLotStatusByID(id); };
reactivateLotStatus = (id: number) => { return this.cvApi.auctions.ReactivateLotStatusByID(id); };
deleteLotStatus = (id: number) => { return this.cvApi.auctions.DeleteLotStatusByID(id); };
// Lot Types
getLotTypes = this.cvApi.auctions.GetLotTypes;
deactivateLotType = (id: number) => { return this.cvApi.auctions.DeactivateLotTypeByID(id); };
reactivateLotType = (id: number) => { return this.cvApi.auctions.ReactivateLotTypeByID(id); };
deleteLotType = (id: number) => { return this.cvApi.auctions.DeleteLotTypeByID(id); };
// Badge Image Types
getBadgeImageTypes = this.cvApi.badges.GetBadgeImageTypes;
deactivateBadgeImageType = (id: number) => { return this.cvApi.badges.DeactivateBadgeImageTypeByID(id); };
reactivateBadgeImageType = (id: number) => { return this.cvApi.badges.ReactivateBadgeImageTypeByID(id); };
deleteBadgeImageType = (id: number) => { return this.cvApi.badges.DeleteBadgeImageTypeByID(id); };
// Badge Types
getBadgeTypes = this.cvApi.badges.GetBadgeTypes;
deactivateBadgeType = (id: number) => { return this.cvApi.badges.DeactivateBadgeTypeByID(id); };
reactivateBadgeType = (id: number) => { return this.cvApi.badges.ReactivateBadgeTypeByID(id); };
deleteBadgeType = (id: number) => { return this.cvApi.badges.DeleteBadgeTypeByID(id); };
// Brand Image Types
getBrandImageTypes = this.cvApi.brands.GetBrandImageTypes;
deactivateBrandImageType = (id: number) => { return this.cvApi.brands.DeactivateBrandImageTypeByID(id); };
reactivateBrandImageType = (id: number) => { return this.cvApi.brands.ReactivateBrandImageTypeByID(id); };
deleteBrandImageType = (id: number) => { return this.cvApi.brands.DeleteBrandImageTypeByID(id); };
// Brand Inventory Location Types
getBrandInventoryLocationTypes = this.cvApi.brands.GetBrandInventoryLocationTypes;
deactivateBrandInventoryLocationType = (id: number) => { return this.cvApi.brands.DeactivateBrandInventoryLocationTypeByID(id); };
reactivateBrandInventoryLocationType = (id: number) => { return this.cvApi.brands.ReactivateBrandInventoryLocationTypeByID(id); };
deleteBrandInventoryLocationType = (id: number) => { return this.cvApi.brands.DeleteBrandInventoryLocationTypeByID(id); };
// Calendar Event Image Types
getCalendarEventImageTypes = this.cvApi.calendarEvents.GetCalendarEventImageTypes;
deactivateCalendarEventImageType = (id: number) => { return this.cvApi.calendarEvents.DeactivateCalendarEventImageTypeByID(id); };
reactivateCalendarEventImageType = (id: number) => { return this.cvApi.calendarEvents.ReactivateCalendarEventImageTypeByID(id); };
deleteCalendarEventImageType = (id: number) => { return this.cvApi.calendarEvents.DeleteCalendarEventImageTypeByID(id); };
// Calendar Event Statuses
getCalendarEventStatuses = this.cvApi.calendarEvents.GetCalendarEventStatuses;
deactivateCalendarEventStatus = (id: number) => { return this.cvApi.calendarEvents.DeactivateCalendarEventStatusByID(id); };
reactivateCalendarEventStatus = (id: number) => { return this.cvApi.calendarEvents.ReactivateCalendarEventStatusByID(id); };
deleteCalendarEventStatus = (id: number) => { return this.cvApi.calendarEvents.DeleteCalendarEventStatusByID(id); };
// Calendar Event Types
getCalendarEventTypes = this.cvApi.calendarEvents.GetCalendarEventTypes;
deactivateCalendarEventType = (id: number) => { return this.cvApi.calendarEvents.DeactivateCalendarEventTypeByID(id); };
reactivateCalendarEventType = (id: number) => { return this.cvApi.calendarEvents.ReactivateCalendarEventTypeByID(id); };
deleteCalendarEventType = (id: number) => { return this.cvApi.calendarEvents.DeleteCalendarEventTypeByID(id); };
// User Event Attendance Types
getUserEventAttendanceTypes = this.cvApi.calendarEvents.GetUserEventAttendanceTypes;
deactivateUserEventAttendanceType = (id: number) => { return this.cvApi.calendarEvents.DeactivateUserEventAttendanceTypeByID(id); };
reactivateUserEventAttendanceType = (id: number) => { return this.cvApi.calendarEvents.ReactivateUserEventAttendanceTypeByID(id); };
deleteUserEventAttendanceType = (id: number) => { return this.cvApi.calendarEvents.DeleteUserEventAttendanceTypeByID(id); };
// Category Image Types
getCategoryImageTypes = this.cvApi.categories.GetCategoryImageTypes;
deactivateCategoryImageType = (id: number) => { return this.cvApi.categories.DeactivateCategoryImageTypeByID(id); };
reactivateCategoryImageType = (id: number) => { return this.cvApi.categories.ReactivateCategoryImageTypeByID(id); };
deleteCategoryImageType = (id: number) => { return this.cvApi.categories.DeleteCategoryImageTypeByID(id); };
// Category Types
getCategoryTypes = this.cvApi.categories.GetCategoryTypes;
deactivateCategoryType = (id: number) => { return this.cvApi.categories.DeactivateCategoryTypeByID(id); };
reactivateCategoryType = (id: number) => { return this.cvApi.categories.ReactivateCategoryTypeByID(id); };
deleteCategoryType = (id: number) => { return this.cvApi.categories.DeleteCategoryTypeByID(id); };
// Contact Image Types
getContactImageTypes = this.cvApi.contacts.GetContactImageTypes;
deactivateContactImageType = (id: number) => { return this.cvApi.contacts.DeactivateContactImageTypeByID(id); };
reactivateContactImageType = (id: number) => { return this.cvApi.contacts.ReactivateContactImageTypeByID(id); };
deleteContactImageType = (id: number) => { return this.cvApi.contacts.DeleteContactImageTypeByID(id); };
// Contact Types
getContactTypes = this.cvApi.contacts.GetContactTypes;
deactivateContactType = (id: number) => { return this.cvApi.contacts.DeactivateContactTypeByID(id); };
reactivateContactType = (id: number) => { return this.cvApi.contacts.ReactivateContactTypeByID(id); };
deleteContactType = (id: number) => { return this.cvApi.contacts.DeleteContactTypeByID(id); };
// Referral Code Statuses
getReferralCodeStatuses = this.cvApi.contacts.GetReferralCodeStatuses;
deactivateReferralCodeStatus = (id: number) => { return this.cvApi.contacts.DeactivateReferralCodeStatusByID(id); };
reactivateReferralCodeStatus = (id: number) => { return this.cvApi.contacts.ReactivateReferralCodeStatusByID(id); };
deleteReferralCodeStatus = (id: number) => { return this.cvApi.contacts.DeleteReferralCodeStatusByID(id); };
// Referral Code Types
getReferralCodeTypes = this.cvApi.contacts.GetReferralCodeTypes;
deactivateReferralCodeType = (id: number) => { return this.cvApi.contacts.DeactivateReferralCodeTypeByID(id); };
reactivateReferralCodeType = (id: number) => { return this.cvApi.contacts.ReactivateReferralCodeTypeByID(id); };
deleteReferralCodeType = (id: number) => { return this.cvApi.contacts.DeleteReferralCodeTypeByID(id); };
// User Image Types
getUserImageTypes = this.cvApi.contacts.GetUserImageTypes;
deactivateUserImageType = (id: number) => { return this.cvApi.contacts.DeactivateUserImageTypeByID(id); };
reactivateUserImageType = (id: number) => { return this.cvApi.contacts.ReactivateUserImageTypeByID(id); };
deleteUserImageType = (id: number) => { return this.cvApi.contacts.DeleteUserImageTypeByID(id); };
// User Online Statuses
getUserOnlineStatuses = this.cvApi.contacts.GetUserOnlineStatuses;
deactivateUserOnlineStatus = (id: number) => { return this.cvApi.contacts.DeactivateUserOnlineStatusByID(id); };
reactivateUserOnlineStatus = (id: number) => { return this.cvApi.contacts.ReactivateUserOnlineStatusByID(id); };
deleteUserOnlineStatus = (id: number) => { return this.cvApi.contacts.DeleteUserOnlineStatusByID(id); };
// User Statuses
getUserStatuses = this.cvApi.contacts.GetUserStatuses;
deactivateUserStatus = (id: number) => { return this.cvApi.contacts.DeactivateUserStatusByID(id); };
reactivateUserStatus = (id: number) => { return this.cvApi.contacts.ReactivateUserStatusByID(id); };
deleteUserStatus = (id: number) => { return this.cvApi.contacts.DeleteUserStatusByID(id); };
// User Types
getUserTypes = this.cvApi.contacts.GetUserTypes;
deactivateUserType = (id: number) => { return this.cvApi.contacts.DeactivateUserTypeByID(id); };
reactivateUserType = (id: number) => { return this.cvApi.contacts.ReactivateUserTypeByID(id); };
deleteUserType = (id: number) => { return this.cvApi.contacts.DeleteUserTypeByID(id); };
// Counter Log Types
getCounterLogTypes = this.cvApi.counters.GetCounterLogTypes;
deactivateCounterLogType = (id: number) => { return this.cvApi.counters.DeactivateCounterLogTypeByID(id); };
reactivateCounterLogType = (id: number) => { return this.cvApi.counters.ReactivateCounterLogTypeByID(id); };
deleteCounterLogType = (id: number) => { return this.cvApi.counters.DeleteCounterLogTypeByID(id); };
// Counter Types
getCounterTypes = this.cvApi.counters.GetCounterTypes;
deactivateCounterType = (id: number) => { return this.cvApi.counters.DeactivateCounterTypeByID(id); };
reactivateCounterType = (id: number) => { return this.cvApi.counters.ReactivateCounterTypeByID(id); };
deleteCounterType = (id: number) => { return this.cvApi.counters.DeleteCounterTypeByID(id); };
// Currency Image Types
getCurrencyImageTypes = this.cvApi.currencies.GetCurrencyImageTypes;
deactivateCurrencyImageType = (id: number) => { return this.cvApi.currencies.DeactivateCurrencyImageTypeByID(id); };
reactivateCurrencyImageType = (id: number) => { return this.cvApi.currencies.ReactivateCurrencyImageTypeByID(id); };
deleteCurrencyImageType = (id: number) => { return this.cvApi.currencies.DeleteCurrencyImageTypeByID(id); };
// Franchise Image Types
getFranchiseImageTypes = this.cvApi.franchises.GetFranchiseImageTypes;
deactivateFranchiseImageType = (id: number) => { return this.cvApi.franchises.DeactivateFranchiseImageTypeByID(id); };
reactivateFranchiseImageType = (id: number) => { return this.cvApi.franchises.ReactivateFranchiseImageTypeByID(id); };
deleteFranchiseImageType = (id: number) => { return this.cvApi.franchises.DeleteFranchiseImageTypeByID(id); };
// Franchise Inventory Location Types
getFranchiseInventoryLocationTypes = this.cvApi.franchises.GetFranchiseInventoryLocationTypes;
deactivateFranchiseInventoryLocationType = (id: number) => { return this.cvApi.franchises.DeactivateFranchiseInventoryLocationTypeByID(id); };
reactivateFranchiseInventoryLocationType = (id: number) => { return this.cvApi.franchises.ReactivateFranchiseInventoryLocationTypeByID(id); };
deleteFranchiseInventoryLocationType = (id: number) => { return this.cvApi.franchises.DeleteFranchiseInventoryLocationTypeByID(id); };
// Franchise Types
getFranchiseTypes = this.cvApi.franchises.GetFranchiseTypes;
deactivateFranchiseType = (id: number) => { return this.cvApi.franchises.DeactivateFranchiseTypeByID(id); };
reactivateFranchiseType = (id: number) => { return this.cvApi.franchises.ReactivateFranchiseTypeByID(id); };
deleteFranchiseType = (id: number) => { return this.cvApi.franchises.DeleteFranchiseTypeByID(id); };
// Country Image Types
getCountryImageTypes = this.cvApi.geography.GetCountryImageTypes;
deactivateCountryImageType = (id: number) => { return this.cvApi.geography.DeactivateCountryImageTypeByID(id); };
reactivateCountryImageType = (id: number) => { return this.cvApi.geography.ReactivateCountryImageTypeByID(id); };
deleteCountryImageType = (id: number) => { return this.cvApi.geography.DeleteCountryImageTypeByID(id); };
// District Image Types
getDistrictImageTypes = this.cvApi.geography.GetDistrictImageTypes;
deactivateDistrictImageType = (id: number) => { return this.cvApi.geography.DeactivateDistrictImageTypeByID(id); };
reactivateDistrictImageType = (id: number) => { return this.cvApi.geography.ReactivateDistrictImageTypeByID(id); };
deleteDistrictImageType = (id: number) => { return this.cvApi.geography.DeleteDistrictImageTypeByID(id); };
// Region Image Types
getRegionImageTypes = this.cvApi.geography.GetRegionImageTypes;
deactivateRegionImageType = (id: number) => { return this.cvApi.geography.DeactivateRegionImageTypeByID(id); };
reactivateRegionImageType = (id: number) => { return this.cvApi.geography.ReactivateRegionImageTypeByID(id); };
deleteRegionImageType = (id: number) => { return this.cvApi.geography.DeleteRegionImageTypeByID(id); };
// Language Image Types
getLanguageImageTypes = this.cvApi.globalization.GetLanguageImageTypes;
deactivateLanguageImageType = (id: number) => { return this.cvApi.globalization.DeactivateLanguageImageTypeByID(id); };
reactivateLanguageImageType = (id: number) => { return this.cvApi.globalization.ReactivateLanguageImageTypeByID(id); };
deleteLanguageImageType = (id: number) => { return this.cvApi.globalization.DeleteLanguageImageTypeByID(id); };
// Group Statuses
getGroupStatuses = this.cvApi.groups.GetGroupStatuses;
deactivateGroupStatus = (id: number) => { return this.cvApi.groups.DeactivateGroupStatusByID(id); };
reactivateGroupStatus = (id: number) => { return this.cvApi.groups.ReactivateGroupStatusByID(id); };
deleteGroupStatus = (id: number) => { return this.cvApi.groups.DeleteGroupStatusByID(id); };
// Group Types
getGroupTypes = this.cvApi.groups.GetGroupTypes;
deactivateGroupType = (id: number) => { return this.cvApi.groups.DeactivateGroupTypeByID(id); };
reactivateGroupType = (id: number) => { return this.cvApi.groups.ReactivateGroupTypeByID(id); };
deleteGroupType = (id: number) => { return this.cvApi.groups.DeleteGroupTypeByID(id); };
// Sales Invoice Event Types
getSalesInvoiceEventTypes = this.cvApi.invoicing.GetSalesInvoiceEventTypes;
deactivateSalesInvoiceEventType = (id: number) => { return this.cvApi.invoicing.DeactivateSalesInvoiceEventTypeByID(id); };
reactivateSalesInvoiceEventType = (id: number) => { return this.cvApi.invoicing.ReactivateSalesInvoiceEventTypeByID(id); };
deleteSalesInvoiceEventType = (id: number) => { return this.cvApi.invoicing.DeleteSalesInvoiceEventTypeByID(id); };
// Sales Invoice States
getSalesInvoiceStates = this.cvApi.invoicing.GetSalesInvoiceStates;
deactivateSalesInvoiceState = (id: number) => { return this.cvApi.invoicing.DeactivateSalesInvoiceStateByID(id); };
reactivateSalesInvoiceState = (id: number) => { return this.cvApi.invoicing.ReactivateSalesInvoiceStateByID(id); };
deleteSalesInvoiceState = (id: number) => { return this.cvApi.invoicing.DeleteSalesInvoiceStateByID(id); };
// Sales Invoice Statuses
getSalesInvoiceStatuses = this.cvApi.invoicing.GetSalesInvoiceStatuses;
deactivateSalesInvoiceStatus = (id: number) => { return this.cvApi.invoicing.DeactivateSalesInvoiceStatusByID(id); };
reactivateSalesInvoiceStatus = (id: number) => { return this.cvApi.invoicing.ReactivateSalesInvoiceStatusByID(id); };
deleteSalesInvoiceStatus = (id: number) => { return this.cvApi.invoicing.DeleteSalesInvoiceStatusByID(id); };
// Sales Invoice Types
getSalesInvoiceTypes = this.cvApi.invoicing.GetSalesInvoiceTypes;
deactivateSalesInvoiceType = (id: number) => { return this.cvApi.invoicing.DeactivateSalesInvoiceTypeByID(id); };
reactivateSalesInvoiceType = (id: number) => { return this.cvApi.invoicing.ReactivateSalesInvoiceTypeByID(id); };
deleteSalesInvoiceType = (id: number) => { return this.cvApi.invoicing.DeleteSalesInvoiceTypeByID(id); };
// Manufacturer Image Types
getManufacturerImageTypes = this.cvApi.manufacturers.GetManufacturerImageTypes;
deactivateManufacturerImageType = (id: number) => { return this.cvApi.manufacturers.DeactivateManufacturerImageTypeByID(id); };
reactivateManufacturerImageType = (id: number) => { return this.cvApi.manufacturers.ReactivateManufacturerImageTypeByID(id); };
deleteManufacturerImageType = (id: number) => { return this.cvApi.manufacturers.DeleteManufacturerImageTypeByID(id); };
// Manufacturer Types
getManufacturerTypes = this.cvApi.manufacturers.GetManufacturerTypes;
deactivateManufacturerType = (id: number) => { return this.cvApi.manufacturers.DeactivateManufacturerTypeByID(id); };
reactivateManufacturerType = (id: number) => { return this.cvApi.manufacturers.ReactivateManufacturerTypeByID(id); };
deleteManufacturerType = (id: number) => { return this.cvApi.manufacturers.DeleteManufacturerTypeByID(id); };
// Email Statuses
getEmailStatuses = this.cvApi.messaging.GetEmailStatuses;
deactivateEmailStatus = (id: number) => { return this.cvApi.messaging.DeactivateEmailStatusByID(id); };
reactivateEmailStatus = (id: number) => { return this.cvApi.messaging.ReactivateEmailStatusByID(id); };
deleteEmailStatus = (id: number) => { return this.cvApi.messaging.DeleteEmailStatusByID(id); };
// Email Types
getEmailTypes = this.cvApi.messaging.GetEmailTypes;
deactivateEmailType = (id: number) => { return this.cvApi.messaging.DeactivateEmailTypeByID(id); };
reactivateEmailType = (id: number) => { return this.cvApi.messaging.ReactivateEmailTypeByID(id); };
deleteEmailType = (id: number) => { return this.cvApi.messaging.DeleteEmailTypeByID(id); };
// Sales Order Event Types
getSalesOrderEventTypes = this.cvApi.ordering.GetSalesOrderEventTypes;
deactivateSalesOrderEventType = (id: number) => { return this.cvApi.ordering.DeactivateSalesOrderEventTypeByID(id); };
reactivateSalesOrderEventType = (id: number) => { return this.cvApi.ordering.ReactivateSalesOrderEventTypeByID(id); };
deleteSalesOrderEventType = (id: number) => { return this.cvApi.ordering.DeleteSalesOrderEventTypeByID(id); };
// Sales Order States
getSalesOrderStates = this.cvApi.ordering.GetSalesOrderStates;
deactivateSalesOrderState = (id: number) => { return this.cvApi.ordering.DeactivateSalesOrderStateByID(id); };
reactivateSalesOrderState = (id: number) => { return this.cvApi.ordering.ReactivateSalesOrderStateByID(id); };
deleteSalesOrderState = (id: number) => { return this.cvApi.ordering.DeleteSalesOrderStateByID(id); };
// Sales Order Statuses
getSalesOrderStatuses = this.cvApi.ordering.GetSalesOrderStatuses;
deactivateSalesOrderStatus = (id: number) => { return this.cvApi.ordering.DeactivateSalesOrderStatusByID(id); };
reactivateSalesOrderStatus = (id: number) => { return this.cvApi.ordering.ReactivateSalesOrderStatusByID(id); };
deleteSalesOrderStatus = (id: number) => { return this.cvApi.ordering.DeleteSalesOrderStatusByID(id); };
// Sales Order Types
getSalesOrderTypes = this.cvApi.ordering.GetSalesOrderTypes;
deactivateSalesOrderType = (id: number) => { return this.cvApi.ordering.DeactivateSalesOrderTypeByID(id); };
reactivateSalesOrderType = (id: number) => { return this.cvApi.ordering.ReactivateSalesOrderTypeByID(id); };
deleteSalesOrderType = (id: number) => { return this.cvApi.ordering.DeleteSalesOrderTypeByID(id); };
// Memberships
getMemberships = this.cvApi.payments.GetMemberships;
deactivateMembership = (id: number) => { return this.cvApi.payments.DeactivateMembershipByID(id); };
reactivateMembership = (id: number) => { return this.cvApi.payments.ReactivateMembershipByID(id); };
deleteMembership = (id: number) => { return this.cvApi.payments.DeleteMembershipByID(id); };
// Membership Levels
getMembershipLevels = this.cvApi.payments.GetMembershipLevels;
deactivateMembershipLevel = (id: number) => { return this.cvApi.payments.DeactivateMembershipLevelByID(id); };
reactivateMembershipLevel = (id: number) => { return this.cvApi.payments.ReactivateMembershipLevelByID(id); };
deleteMembershipLevel = (id: number) => { return this.cvApi.payments.DeleteMembershipLevelByID(id); };
// Payment Statuses
getPaymentStatuses = this.cvApi.payments.GetPaymentStatuses;
deactivatePaymentStatus = (id: number) => { return this.cvApi.payments.DeactivatePaymentStatusByID(id); };
reactivatePaymentStatus = (id: number) => { return this.cvApi.payments.ReactivatePaymentStatusByID(id); };
deletePaymentStatus = (id: number) => { return this.cvApi.payments.DeletePaymentStatusByID(id); };
// Payment Types
getPaymentTypes = this.cvApi.payments.GetPaymentTypes;
deactivatePaymentType = (id: number) => { return this.cvApi.payments.DeactivatePaymentTypeByID(id); };
reactivatePaymentType = (id: number) => { return this.cvApi.payments.ReactivatePaymentTypeByID(id); };
deletePaymentType = (id: number) => { return this.cvApi.payments.DeletePaymentTypeByID(id); };
// Repeat Types
getRepeatTypes = this.cvApi.payments.GetRepeatTypes;
deactivateRepeatType = (id: number) => { return this.cvApi.payments.DeactivateRepeatTypeByID(id); };
reactivateRepeatType = (id: number) => { return this.cvApi.payments.ReactivateRepeatTypeByID(id); };
deleteRepeatType = (id: number) => { return this.cvApi.payments.DeleteRepeatTypeByID(id); };
// Subscription Statuses
getSubscriptionStatuses = this.cvApi.payments.GetSubscriptionStatuses;
deactivateSubscriptionStatus = (id: number) => { return this.cvApi.payments.DeactivateSubscriptionStatusByID(id); };
reactivateSubscriptionStatus = (id: number) => { return this.cvApi.payments.ReactivateSubscriptionStatusByID(id); };
deleteSubscriptionStatus = (id: number) => { return this.cvApi.payments.DeleteSubscriptionStatusByID(id); };
// Subscription Types
getSubscriptionTypes = this.cvApi.payments.GetSubscriptionTypes;
deactivateSubscriptionType = (id: number) => { return this.cvApi.payments.DeactivateSubscriptionTypeByID(id); };
reactivateSubscriptionType = (id: number) => { return this.cvApi.payments.ReactivateSubscriptionTypeByID(id); };
deleteSubscriptionType = (id: number) => { return this.cvApi.payments.DeleteSubscriptionTypeByID(id); };
// Price Points
getPricePoints = this.cvApi.pricing.GetPricePoints;
deactivatePricePoint = (id: number) => { return this.cvApi.pricing.DeactivatePricePointByID(id); };
reactivatePricePoint = (id: number) => { return this.cvApi.pricing.ReactivatePricePointByID(id); };
deletePricePoint = (id: number) => { return this.cvApi.pricing.DeletePricePointByID(id); };
// Future Import Statuses
getFutureImportStatuses = this.cvApi.products.GetFutureImportStatuses;
deactivateFutureImportStatus = (id: number) => { return this.cvApi.products.DeactivateFutureImportStatusByID(id); };
reactivateFutureImportStatus = (id: number) => { return this.cvApi.products.ReactivateFutureImportStatusByID(id); };
deleteFutureImportStatus = (id: number) => { return this.cvApi.products.DeleteFutureImportStatusByID(id); };
// Product Association Types
getProductAssociationTypes = this.cvApi.products.GetProductAssociationTypes;
deactivateProductAssociationType = (id: number) => { return this.cvApi.products.DeactivateProductAssociationTypeByID(id); };
reactivateProductAssociationType = (id: number) => { return this.cvApi.products.ReactivateProductAssociationTypeByID(id); };
deleteProductAssociationType = (id: number) => { return this.cvApi.products.DeleteProductAssociationTypeByID(id); };
// Product Download Types
getProductDownloadTypes = this.cvApi.products.GetProductDownloadTypes;
deactivateProductDownloadType = (id: number) => { return this.cvApi.products.DeactivateProductDownloadTypeByID(id); };
reactivateProductDownloadType = (id: number) => { return this.cvApi.products.ReactivateProductDownloadTypeByID(id); };
deleteProductDownloadType = (id: number) => { return this.cvApi.products.DeleteProductDownloadTypeByID(id); };
// Product Image Types
getProductImageTypes = this.cvApi.products.GetProductImageTypes;
deactivateProductImageType = (id: number) => { return this.cvApi.products.DeactivateProductImageTypeByID(id); };
reactivateProductImageType = (id: number) => { return this.cvApi.products.ReactivateProductImageTypeByID(id); };
deleteProductImageType = (id: number) => { return this.cvApi.products.DeleteProductImageTypeByID(id); };
// Product Statuses
getProductStatuses = this.cvApi.products.GetProductStatuses;
deactivateProductStatus = (id: number) => { return this.cvApi.products.DeactivateProductStatusByID(id); };
reactivateProductStatus = (id: number) => { return this.cvApi.products.ReactivateProductStatusByID(id); };
deleteProductStatus = (id: number) => { return this.cvApi.products.DeleteProductStatusByID(id); };
// Product Types
getProductTypes = this.cvApi.products.GetProductTypes;
deactivateProductType = (id: number) => { return this.cvApi.products.DeactivateProductTypeByID(id); };
reactivateProductType = (id: number) => { return this.cvApi.products.ReactivateProductTypeByID(id); };
deleteProductType = (id: number) => { return this.cvApi.products.DeleteProductTypeByID(id); };
// Purchase Order Event Types
getPurchaseOrderEventTypes = this.cvApi.purchasing.GetPurchaseOrderEventTypes;
deactivatePurchaseOrderEventType = (id: number) => { return this.cvApi.purchasing.DeactivatePurchaseOrderEventTypeByID(id); };
reactivatePurchaseOrderEventType = (id: number) => { return this.cvApi.purchasing.ReactivatePurchaseOrderEventTypeByID(id); };
deletePurchaseOrderEventType = (id: number) => { return this.cvApi.purchasing.DeletePurchaseOrderEventTypeByID(id); };
// Purchase Order States
getPurchaseOrderStates = this.cvApi.purchasing.GetPurchaseOrderStates;
deactivatePurchaseOrderState = (id: number) => { return this.cvApi.purchasing.DeactivatePurchaseOrderStateByID(id); };
reactivatePurchaseOrderState = (id: number) => { return this.cvApi.purchasing.ReactivatePurchaseOrderStateByID(id); };
deletePurchaseOrderState = (id: number) => { return this.cvApi.purchasing.DeletePurchaseOrderStateByID(id); };
// Purchase Order Statuses
getPurchaseOrderStatuses = this.cvApi.purchasing.GetPurchaseOrderStatuses;
deactivatePurchaseOrderStatus = (id: number) => { return this.cvApi.purchasing.DeactivatePurchaseOrderStatusByID(id); };
reactivatePurchaseOrderStatus = (id: number) => { return this.cvApi.purchasing.ReactivatePurchaseOrderStatusByID(id); };
deletePurchaseOrderStatus = (id: number) => { return this.cvApi.purchasing.DeletePurchaseOrderStatusByID(id); };
// Purchase Order Types
getPurchaseOrderTypes = this.cvApi.purchasing.GetPurchaseOrderTypes;
deactivatePurchaseOrderType = (id: number) => { return this.cvApi.purchasing.DeactivatePurchaseOrderTypeByID(id); };
reactivatePurchaseOrderType = (id: number) => { return this.cvApi.purchasing.ReactivatePurchaseOrderTypeByID(id); };
deletePurchaseOrderType = (id: number) => { return this.cvApi.purchasing.DeletePurchaseOrderTypeByID(id); };
// Question Types
getQuestionTypes = this.cvApi.questionnaire.GetQuestionTypes;
deactivateQuestionType = (id: number) => { return this.cvApi.questionnaire.DeactivateQuestionTypeByID(id); };
reactivateQuestionType = (id: number) => { return this.cvApi.questionnaire.ReactivateQuestionTypeByID(id); };
deleteQuestionType = (id: number) => { return this.cvApi.questionnaire.DeleteQuestionTypeByID(id); };
// Sales Quote Event Types
getSalesQuoteEventTypes = this.cvApi.quoting.GetSalesQuoteEventTypes;
deactivateSalesQuoteEventType = (id: number) => { return this.cvApi.quoting.DeactivateSalesQuoteEventTypeByID(id); };
reactivateSalesQuoteEventType = (id: number) => { return this.cvApi.quoting.ReactivateSalesQuoteEventTypeByID(id); };
deleteSalesQuoteEventType = (id: number) => { return this.cvApi.quoting.DeleteSalesQuoteEventTypeByID(id); };
// Sales Quote States
getSalesQuoteStates = this.cvApi.quoting.GetSalesQuoteStates;
deactivateSalesQuoteState = (id: number) => { return this.cvApi.quoting.DeactivateSalesQuoteStateByID(id); };
reactivateSalesQuoteState = (id: number) => { return this.cvApi.quoting.ReactivateSalesQuoteStateByID(id); };
deleteSalesQuoteState = (id: number) => { return this.cvApi.quoting.DeleteSalesQuoteStateByID(id); };
// Sales Quote Statuses
getSalesQuoteStatuses = this.cvApi.quoting.GetSalesQuoteStatuses;
deactivateSalesQuoteStatus = (id: number) => { return this.cvApi.quoting.DeactivateSalesQuoteStatusByID(id); };
reactivateSalesQuoteStatus = (id: number) => { return this.cvApi.quoting.ReactivateSalesQuoteStatusByID(id); };
deleteSalesQuoteStatus = (id: number) => { return this.cvApi.quoting.DeleteSalesQuoteStatusByID(id); };
// Sales Quote Types
getSalesQuoteTypes = this.cvApi.quoting.GetSalesQuoteTypes;
deactivateSalesQuoteType = (id: number) => { return this.cvApi.quoting.DeactivateSalesQuoteTypeByID(id); };
reactivateSalesQuoteType = (id: number) => { return this.cvApi.quoting.ReactivateSalesQuoteTypeByID(id); };
deleteSalesQuoteType = (id: number) => { return this.cvApi.quoting.DeleteSalesQuoteTypeByID(id); };
// Report Types
getReportTypes = this.cvApi.reporting.GetReportTypes;
deactivateReportType = (id: number) => { return this.cvApi.reporting.DeactivateReportTypeByID(id); };
reactivateReportType = (id: number) => { return this.cvApi.reporting.ReactivateReportTypeByID(id); };
deleteReportType = (id: number) => { return this.cvApi.reporting.DeleteReportTypeByID(id); };
// Sales Return Event Types
getSalesReturnEventTypes = this.cvApi.returning.GetSalesReturnEventTypes;
deactivateSalesReturnEventType = (id: number) => { return this.cvApi.returning.DeactivateSalesReturnEventTypeByID(id); };
reactivateSalesReturnEventType = (id: number) => { return this.cvApi.returning.ReactivateSalesReturnEventTypeByID(id); };
deleteSalesReturnEventType = (id: number) => { return this.cvApi.returning.DeleteSalesReturnEventTypeByID(id); };
// Sales Return Reasons
getSalesReturnReasons = this.cvApi.returning.GetSalesReturnReasons;
deactivateSalesReturnReason = (id: number) => { return this.cvApi.returning.DeactivateSalesReturnReasonByID(id); };
reactivateSalesReturnReason = (id: number) => { return this.cvApi.returning.ReactivateSalesReturnReasonByID(id); };
deleteSalesReturnReason = (id: number) => { return this.cvApi.returning.DeleteSalesReturnReasonByID(id); };
// Sales Return States
getSalesReturnStates = this.cvApi.returning.GetSalesReturnStates;
deactivateSalesReturnState = (id: number) => { return this.cvApi.returning.DeactivateSalesReturnStateByID(id); };
reactivateSalesReturnState = (id: number) => { return this.cvApi.returning.ReactivateSalesReturnStateByID(id); };
deleteSalesReturnState = (id: number) => { return this.cvApi.returning.DeleteSalesReturnStateByID(id); };
// Sales Return Statuses
getSalesReturnStatuses = this.cvApi.returning.GetSalesReturnStatuses;
deactivateSalesReturnStatus = (id: number) => { return this.cvApi.returning.DeactivateSalesReturnStatusByID(id); };
reactivateSalesReturnStatus = (id: number) => { return this.cvApi.returning.ReactivateSalesReturnStatusByID(id); };
deleteSalesReturnStatus = (id: number) => { return this.cvApi.returning.DeleteSalesReturnStatusByID(id); };
// Sales Return Types
getSalesReturnTypes = this.cvApi.returning.GetSalesReturnTypes;
deactivateSalesReturnType = (id: number) => { return this.cvApi.returning.DeactivateSalesReturnTypeByID(id); };
reactivateSalesReturnType = (id: number) => { return this.cvApi.returning.ReactivateSalesReturnTypeByID(id); };
deleteSalesReturnType = (id: number) => { return this.cvApi.returning.DeleteSalesReturnTypeByID(id); };
// Review Types
getReviewTypes = this.cvApi.reviews.GetReviewTypes;
deactivateReviewType = (id: number) => { return this.cvApi.reviews.DeactivateReviewTypeByID(id); };
reactivateReviewType = (id: number) => { return this.cvApi.reviews.ReactivateReviewTypeByID(id); };
deleteReviewType = (id: number) => { return this.cvApi.reviews.DeleteReviewTypeByID(id); };
// Sales Item Target Types
getSalesItemTargetTypes = this.cvApi.sales.GetSalesItemTargetTypes;
deactivateSalesItemTargetType = (id: number) => { return this.cvApi.sales.DeactivateSalesItemTargetTypeByID(id); };
reactivateSalesItemTargetType = (id: number) => { return this.cvApi.sales.ReactivateSalesItemTargetTypeByID(id); };
deleteSalesItemTargetType = (id: number) => { return this.cvApi.sales.DeleteSalesItemTargetTypeByID(id); };
// Sample Request Event Types
getSampleRequestEventTypes = this.cvApi.sampling.GetSampleRequestEventTypes;
deactivateSampleRequestEventType = (id: number) => { return this.cvApi.sampling.DeactivateSampleRequestEventTypeByID(id); };
reactivateSampleRequestEventType = (id: number) => { return this.cvApi.sampling.ReactivateSampleRequestEventTypeByID(id); };
deleteSampleRequestEventType = (id: number) => { return this.cvApi.sampling.DeleteSampleRequestEventTypeByID(id); };
// Sample Request States
getSampleRequestStates = this.cvApi.sampling.GetSampleRequestStates;
deactivateSampleRequestState = (id: number) => { return this.cvApi.sampling.DeactivateSampleRequestStateByID(id); };
reactivateSampleRequestState = (id: number) => { return this.cvApi.sampling.ReactivateSampleRequestStateByID(id); };
deleteSampleRequestState = (id: number) => { return this.cvApi.sampling.DeleteSampleRequestStateByID(id); };
// Sample Request Statuses
getSampleRequestStatuses = this.cvApi.sampling.GetSampleRequestStatuses;
deactivateSampleRequestStatus = (id: number) => { return this.cvApi.sampling.DeactivateSampleRequestStatusByID(id); };
reactivateSampleRequestStatus = (id: number) => { return this.cvApi.sampling.ReactivateSampleRequestStatusByID(id); };
deleteSampleRequestStatus = (id: number) => { return this.cvApi.sampling.DeleteSampleRequestStatusByID(id); };
// Sample Request Types
getSampleRequestTypes = this.cvApi.sampling.GetSampleRequestTypes;
deactivateSampleRequestType = (id: number) => { return this.cvApi.sampling.DeactivateSampleRequestTypeByID(id); };
reactivateSampleRequestType = (id: number) => { return this.cvApi.sampling.ReactivateSampleRequestTypeByID(id); };
deleteSampleRequestType = (id: number) => { return this.cvApi.sampling.DeleteSampleRequestTypeByID(id); };
// Appointment Statuses
getAppointmentStatuses = this.cvApi.scheduling.GetAppointmentStatuses;
deactivateAppointmentStatus = (id: number) => { return this.cvApi.scheduling.DeactivateAppointmentStatusByID(id); };
reactivateAppointmentStatus = (id: number) => { return this.cvApi.scheduling.ReactivateAppointmentStatusByID(id); };
deleteAppointmentStatus = (id: number) => { return this.cvApi.scheduling.DeleteAppointmentStatusByID(id); };
// Appointment Types
getAppointmentTypes = this.cvApi.scheduling.GetAppointmentTypes;
deactivateAppointmentType = (id: number) => { return this.cvApi.scheduling.DeactivateAppointmentTypeByID(id); };
reactivateAppointmentType = (id: number) => { return this.cvApi.scheduling.ReactivateAppointmentTypeByID(id); };
deleteAppointmentType = (id: number) => { return this.cvApi.scheduling.DeleteAppointmentTypeByID(id); };
// Scout Category Types
getScoutCategoryTypes = this.cvApi.scouting.GetScoutCategoryTypes;
deactivateScoutCategoryType = (id: number) => { return this.cvApi.scouting.DeactivateScoutCategoryTypeByID(id); };
reactivateScoutCategoryType = (id: number) => { return this.cvApi.scouting.ReactivateScoutCategoryTypeByID(id); };
deleteScoutCategoryType = (id: number) => { return this.cvApi.scouting.DeleteScoutCategoryTypeByID(id); };
// Package Types
getPackageTypes = this.cvApi.shipping.GetPackageTypes;
deactivatePackageType = (id: number) => { return this.cvApi.shipping.DeactivatePackageTypeByID(id); };
reactivatePackageType = (id: number) => { return this.cvApi.shipping.ReactivatePackageTypeByID(id); };
deletePackageType = (id: number) => { return this.cvApi.shipping.DeletePackageTypeByID(id); };
// Shipment Statuses
getShipmentStatuses = this.cvApi.shipping.GetShipmentStatuses;
deactivateShipmentStatus = (id: number) => { return this.cvApi.shipping.DeactivateShipmentStatusByID(id); };
reactivateShipmentStatus = (id: number) => { return this.cvApi.shipping.ReactivateShipmentStatusByID(id); };
deleteShipmentStatus = (id: number) => { return this.cvApi.shipping.DeleteShipmentStatusByID(id); };
// Shipment Types
getShipmentTypes = this.cvApi.shipping.GetShipmentTypes;
deactivateShipmentType = (id: number) => { return this.cvApi.shipping.DeactivateShipmentTypeByID(id); };
reactivateShipmentType = (id: number) => { return this.cvApi.shipping.ReactivateShipmentTypeByID(id); };
deleteShipmentType = (id: number) => { return this.cvApi.shipping.DeleteShipmentTypeByID(id); };
// Cart Event Types
getCartEventTypes = this.cvApi.shopping.GetCartEventTypes;
deactivateCartEventType = (id: number) => { return this.cvApi.shopping.DeactivateCartEventTypeByID(id); };
reactivateCartEventType = (id: number) => { return this.cvApi.shopping.ReactivateCartEventTypeByID(id); };
deleteCartEventType = (id: number) => { return this.cvApi.shopping.DeleteCartEventTypeByID(id); };
// Cart States
getCartStates = this.cvApi.shopping.GetCartStates;
deactivateCartState = (id: number) => { return this.cvApi.shopping.DeactivateCartStateByID(id); };
reactivateCartState = (id: number) => { return this.cvApi.shopping.ReactivateCartStateByID(id); };
deleteCartState = (id: number) => { return this.cvApi.shopping.DeleteCartStateByID(id); };
// Cart Statuses
getCartStatuses = this.cvApi.shopping.GetCartStatuses;
deactivateCartStatus = (id: number) => { return this.cvApi.shopping.DeactivateCartStatusByID(id); };
reactivateCartStatus = (id: number) => { return this.cvApi.shopping.ReactivateCartStatusByID(id); };
deleteCartStatus = (id: number) => { return this.cvApi.shopping.DeleteCartStatusByID(id); };
// Cart Types
getCartTypes = this.cvApi.shopping.GetCartTypes;
deactivateCartType = (id: number) => { return this.cvApi.shopping.DeactivateCartTypeByID(id); };
reactivateCartType = (id: number) => { return this.cvApi.shopping.ReactivateCartTypeByID(id); };
deleteCartType = (id: number) => { return this.cvApi.shopping.DeleteCartTypeByID(id); };
// Store Image Types
getStoreImageTypes = this.cvApi.stores.GetStoreImageTypes;
deactivateStoreImageType = (id: number) => { return this.cvApi.stores.DeactivateStoreImageTypeByID(id); };
reactivateStoreImageType = (id: number) => { return this.cvApi.stores.ReactivateStoreImageTypeByID(id); };
deleteStoreImageType = (id: number) => { return this.cvApi.stores.DeleteStoreImageTypeByID(id); };
// Store Inventory Location Types
getStoreInventoryLocationTypes = this.cvApi.stores.GetStoreInventoryLocationTypes;
deactivateStoreInventoryLocationType = (id: number) => { return this.cvApi.stores.DeactivateStoreInventoryLocationTypeByID(id); };
reactivateStoreInventoryLocationType = (id: number) => { return this.cvApi.stores.ReactivateStoreInventoryLocationTypeByID(id); };
deleteStoreInventoryLocationType = (id: number) => { return this.cvApi.stores.DeleteStoreInventoryLocationTypeByID(id); };
// Store Types
getStoreTypes = this.cvApi.stores.GetStoreTypes;
deactivateStoreType = (id: number) => { return this.cvApi.stores.DeactivateStoreTypeByID(id); };
reactivateStoreType = (id: number) => { return this.cvApi.stores.ReactivateStoreTypeByID(id); };
deleteStoreType = (id: number) => { return this.cvApi.stores.DeleteStoreTypeByID(id); };
// Note Types
getNoteTypes = this.cvApi.structure.GetNoteTypes;
deactivateNoteType = (id: number) => { return this.cvApi.structure.DeactivateNoteTypeByID(id); };
reactivateNoteType = (id: number) => { return this.cvApi.structure.ReactivateNoteTypeByID(id); };
deleteNoteType = (id: number) => { return this.cvApi.structure.DeleteNoteTypeByID(id); };
// Record Version Types
getRecordVersionTypes = this.cvApi.structure.GetRecordVersionTypes;
deactivateRecordVersionType = (id: number) => { return this.cvApi.structure.DeactivateRecordVersionTypeByID(id); };
reactivateRecordVersionType = (id: number) => { return this.cvApi.structure.ReactivateRecordVersionTypeByID(id); };
deleteRecordVersionType = (id: number) => { return this.cvApi.structure.DeleteRecordVersionTypeByID(id); };
// Setting Groups
getSettingGroups = this.cvApi.structure.GetSettingGroups;
deactivateSettingGroup = (id: number) => { return this.cvApi.structure.DeactivateSettingGroupByID(id); };
reactivateSettingGroup = (id: number) => { return this.cvApi.structure.ReactivateSettingGroupByID(id); };
deleteSettingGroup = (id: number) => { return this.cvApi.structure.DeleteSettingGroupByID(id); };
// Setting Types
getSettingTypes = this.cvApi.structure.GetSettingTypes;
deactivateSettingType = (id: number) => { return this.cvApi.structure.DeactivateSettingTypeByID(id); };
reactivateSettingType = (id: number) => { return this.cvApi.structure.ReactivateSettingTypeByID(id); };
deleteSettingType = (id: number) => { return this.cvApi.structure.DeleteSettingTypeByID(id); };
// Campaign Statuses
getCampaignStatuses = this.cvApi.tracking.GetCampaignStatuses;
deactivateCampaignStatus = (id: number) => { return this.cvApi.tracking.DeactivateCampaignStatusByID(id); };
reactivateCampaignStatus = (id: number) => { return this.cvApi.tracking.ReactivateCampaignStatusByID(id); };
deleteCampaignStatus = (id: number) => { return this.cvApi.tracking.DeleteCampaignStatusByID(id); };
// Campaign Types
getCampaignTypes = this.cvApi.tracking.GetCampaignTypes;
deactivateCampaignType = (id: number) => { return this.cvApi.tracking.DeactivateCampaignTypeByID(id); };
reactivateCampaignType = (id: number) => { return this.cvApi.tracking.ReactivateCampaignTypeByID(id); };
deleteCampaignType = (id: number) => { return this.cvApi.tracking.DeleteCampaignTypeByID(id); };
// Event Statuses
getEventStatuses = this.cvApi.tracking.GetEventStatuses;
deactivateEventStatus = (id: number) => { return this.cvApi.tracking.DeactivateEventStatusByID(id); };
reactivateEventStatus = (id: number) => { return this.cvApi.tracking.ReactivateEventStatusByID(id); };
deleteEventStatus = (id: number) => { return this.cvApi.tracking.DeleteEventStatusByID(id); };
// Event Types
getEventTypes = this.cvApi.tracking.GetEventTypes;
deactivateEventType = (id: number) => { return this.cvApi.tracking.DeactivateEventTypeByID(id); };
reactivateEventType = (id: number) => { return this.cvApi.tracking.ReactivateEventTypeByID(id); };
deleteEventType = (id: number) => { return this.cvApi.tracking.DeleteEventTypeByID(id); };
// I P Organization Statuses
getIPOrganizationStatuses = this.cvApi.tracking.GetIPOrganizationStatuses;
deactivateIPOrganizationStatus = (id: number) => { return this.cvApi.tracking.DeactivateIPOrganizationStatusByID(id); };
reactivateIPOrganizationStatus = (id: number) => { return this.cvApi.tracking.ReactivateIPOrganizationStatusByID(id); };
deleteIPOrganizationStatus = (id: number) => { return this.cvApi.tracking.DeleteIPOrganizationStatusByID(id); };
// Page View Statuses
getPageViewStatuses = this.cvApi.tracking.GetPageViewStatuses;
deactivatePageViewStatus = (id: number) => { return this.cvApi.tracking.DeactivatePageViewStatusByID(id); };
reactivatePageViewStatus = (id: number) => { return this.cvApi.tracking.ReactivatePageViewStatusByID(id); };
deletePageViewStatus = (id: number) => { return this.cvApi.tracking.DeletePageViewStatusByID(id); };
// Page View Types
getPageViewTypes = this.cvApi.tracking.GetPageViewTypes;
deactivatePageViewType = (id: number) => { return this.cvApi.tracking.DeactivatePageViewTypeByID(id); };
reactivatePageViewType = (id: number) => { return this.cvApi.tracking.ReactivatePageViewTypeByID(id); };
deletePageViewType = (id: number) => { return this.cvApi.tracking.DeletePageViewTypeByID(id); };
// Visit Statuses
getVisitStatuses = this.cvApi.tracking.GetVisitStatuses;
deactivateVisitStatus = (id: number) => { return this.cvApi.tracking.DeactivateVisitStatusByID(id); };
reactivateVisitStatus = (id: number) => { return this.cvApi.tracking.ReactivateVisitStatusByID(id); };
deleteVisitStatus = (id: number) => { return this.cvApi.tracking.DeleteVisitStatusByID(id); };
// Vendor Image Types
getVendorImageTypes = this.cvApi.vendors.GetVendorImageTypes;
deactivateVendorImageType = (id: number) => { return this.cvApi.vendors.DeactivateVendorImageTypeByID(id); };
reactivateVendorImageType = (id: number) => { return this.cvApi.vendors.ReactivateVendorImageTypeByID(id); };
deleteVendorImageType = (id: number) => { return this.cvApi.vendors.DeleteVendorImageTypeByID(id); };
// Vendor Types
getVendorTypes = this.cvApi.vendors.GetVendorTypes;
deactivateVendorType = (id: number) => { return this.cvApi.vendors.DeactivateVendorTypeByID(id); };
reactivateVendorType = (id: number) => { return this.cvApi.vendors.ReactivateVendorTypeByID(id); };
deleteVendorType = (id: number) => { return this.cvApi.vendors.DeleteVendorTypeByID(id); };
}
adminApp.controller("SearchTypeController", SearchTypeController);
}
