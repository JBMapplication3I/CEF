/**
 * @file /framework/admin/controls/types/types.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc UI Router States for CEF Types, Statuses and States.
 */
module cef.admin.controls.types {
adminApp.config((
$stateProvider: ng.ui.IStateProvider,
cefConfig: core.CefConfig) =>
{
const fullInner = urlConfig => {
if (!urlConfig.host && !urlConfig.root) {
// We don't have a static url or root path to apply
return "";
}
if (!urlConfig.host && urlConfig.root) {
// We don't have a static url, but we do have a root path to apply
return urlConfig.root;
}
if (urlConfig.host && !urlConfig.root) {
// We don't have a root path, but we do have a static url to apply
return urlConfig.host;
}
// We have both a root path and a static url to apply
return urlConfig.host + urlConfig.root;
};
const templateRoot = fullInner(cefConfig.routes.ui) + "/framework/admin"; // <UI>/framework/admin
const makeSectionState = (rootStateName: string, name: string, permission: RegExp | string, url: string, translationKey: string) => {
const viewObj = {};
viewObj[rootStateName] = { template: `<div ui-view="${name}" class="full-height"></div>`, };
$stateProvider.state(`${rootStateName}.${name}`, <ng.ui.IState>{
resolve: { $title: () => translationKey },
abstract: true,
nomenu: true,
requiresPermission: permission,
url: url,
views: viewObj
});
};
const makeDashboardState = (rootStateName: string, name: string, permission: RegExp | string) => {
const viewObj = {};
viewObj[name] = { templateUrl: `${templateRoot}/controls/types/${name}.dashboard.html`, };
$stateProvider.state(`${rootStateName}.${name}.dashboard`, <ng.ui.IState>{
resolve: { $title: () => "ui.admin.common.Dashboard" },
requiresPermission: permission,
url: "/Dashboard",
views: viewObj
});
};
const makeListState = (rootStateName: string, name: string, permission: RegExp | string) => {
const viewObj = {};
viewObj[name] = { templateUrl: `${templateRoot}/controls/types/${name}.list.html`, };
$stateProvider.state(`${rootStateName}.${name}.list`, <ng.ui.IState>{
resolve: { $title: () => "ui.admin.common.Search" },
requiresPermission: permission,
url: "/List",
views: viewObj
});
};
const makeDetailState = (rootStateName: string, name: string, permission: RegExp | string) => {
const viewObj = {};
viewObj[name] = { templateUrl: `${templateRoot}/controls/types/${name}.view.html`, };
$stateProvider.state(`${rootStateName}.${name}.detail`, <ng.ui.IState>{
resolve: { $title: () => "ui.admin.common.NumberSymbolXDetail.Template" },
nomenu: true,
requiresPermission: permission,
url: "/Detail/:ID",
views: viewObj
});
};
const makeSetOfStates = (rootStateName: string, name: string, permission: RegExp | string, url: string, translationKey: string) => {
makeSectionState(rootStateName, name, permission, url, translationKey);
makeDashboardState(rootStateName, name, permission);
makeListState(rootStateName, name, permission);
makeDetailState(rootStateName, name, permission);
};
// Types
makeSetOfStates("types", "accountAssociationTypes", "Admin.Types", "/Account-Association-Types", "Account Association Types");
makeSetOfStates("types", "accountImageTypes", "Admin.Types", "/Account-Image-Types", "Account Image Types");
makeSetOfStates("types", "accountProductTypes", "Admin.Types", "/Account-Product-Types", "Account Product Types");
makeSetOfStates("types", "accountTypes", "Admin.Types", "/Account-Types", "Account Types");
makeSetOfStates("types", "adImageTypes", "Admin.Types", "/Ad-Image-Types", "Ad Image Types");
makeSetOfStates("types", "adTypes", "Admin.Types", "/Ad-Types", "Ad Types");
makeSetOfStates("types", "zoneTypes", "Admin.Types", "/Zone-Types", "Zone Types");
makeSetOfStates("types", "attributeTypes", "Admin.Types", "/Attribute-Types", "Attribute Types");
makeSetOfStates("types", "auctionTypes", "Admin.Types", "/Auction-Types", "Auction Types");
makeSetOfStates("types", "lotTypes", "Admin.Types", "/Lot-Types", "Lot Types");
makeSetOfStates("types", "badgeImageTypes", "Admin.Types", "/Badge-Image-Types", "Badge Image Types");
makeSetOfStates("types", "badgeTypes", "Admin.Types", "/Badge-Types", "Badge Types");
makeSetOfStates("types", "brandImageTypes", "Admin.Types", "/Brand-Image-Types", "Brand Image Types");
makeSetOfStates("types", "brandInventoryLocationTypes", "Admin.Types", "/Brand-Inventory-Location-Types", "Brand Inventory Location Types");
makeSetOfStates("types", "calendarEventImageTypes", "Admin.Types", "/Calendar-Event-Image-Types", "Calendar Event Image Types");
makeSetOfStates("types", "calendarEventTypes", "Admin.Types", "/Calendar-Event-Types", "Calendar Event Types");
makeSetOfStates("types", "userEventAttendanceTypes", "Admin.Types", "/User-Event-Attendance-Types", "User Event Attendance Types");
makeSetOfStates("types", "categoryImageTypes", "Admin.Types", "/Category-Image-Types", "Category Image Types");
makeSetOfStates("types", "categoryTypes", "Admin.Types", "/Category-Types", "Category Types");
makeSetOfStates("types", "contactImageTypes", "Admin.Types", "/Contact-Image-Types", "Contact Image Types");
makeSetOfStates("types", "contactTypes", "Admin.Types", "/Contact-Types", "Contact Types");
makeSetOfStates("types", "referralCodeTypes", "Admin.Types", "/Referral-Code-Types", "Referral Code Types");
makeSetOfStates("types", "userImageTypes", "Admin.Types", "/User-Image-Types", "User Image Types");
makeSetOfStates("types", "userTypes", "Admin.Types", "/User-Types", "User Types");
makeSetOfStates("types", "counterLogTypes", "Admin.Types", "/Counter-Log-Types", "Counter Log Types");
makeSetOfStates("types", "counterTypes", "Admin.Types", "/Counter-Types", "Counter Types");
makeSetOfStates("types", "currencyImageTypes", "Admin.Types", "/Currency-Image-Types", "Currency Image Types");
makeSetOfStates("types", "franchiseImageTypes", "Admin.Types", "/Franchise-Image-Types", "Franchise Image Types");
makeSetOfStates("types", "franchiseInventoryLocationTypes", "Admin.Types", "/Franchise-Inventory-Location-Types", "Franchise Inventory Location Types");
makeSetOfStates("types", "franchiseTypes", "Admin.Types", "/Franchise-Types", "Franchise Types");
makeSetOfStates("types", "countryImageTypes", "Admin.Types", "/Country-Image-Types", "Country Image Types");
makeSetOfStates("types", "districtImageTypes", "Admin.Types", "/District-Image-Types", "District Image Types");
makeSetOfStates("types", "regionImageTypes", "Admin.Types", "/Region-Image-Types", "Region Image Types");
makeSetOfStates("types", "languageImageTypes", "Admin.Types", "/Language-Image-Types", "Language Image Types");
makeSetOfStates("types", "groupTypes", "Admin.Types", "/Group-Types", "Group Types");
makeSetOfStates("types", "salesInvoiceEventTypes", "Admin.Types", "/Sales-Invoice-Event-Types", "Sales Invoice Event Types");
makeSetOfStates("types", "salesInvoiceTypes", "Admin.Types", "/Sales-Invoice-Types", "Sales Invoice Types");
makeSetOfStates("types", "manufacturerImageTypes", "Admin.Types", "/Manufacturer-Image-Types", "Manufacturer Image Types");
makeSetOfStates("types", "manufacturerTypes", "Admin.Types", "/Manufacturer-Types", "Manufacturer Types");
makeSetOfStates("types", "emailTypes", "Admin.Types", "/Email-Types", "Email Types");
makeSetOfStates("types", "salesOrderEventTypes", "Admin.Types", "/Sales-Order-Event-Types", "Sales Order Event Types");
makeSetOfStates("types", "salesOrderTypes", "Admin.Types", "/Sales-Order-Types", "Sales Order Types");
makeSetOfStates("types", "paymentTypes", "Admin.Types", "/Payment-Types", "Payment Types");
makeSetOfStates("types", "repeatTypes", "Admin.Types", "/Repeat-Types", "Repeat Types");
makeSetOfStates("types", "subscriptionTypes", "Admin.Types", "/Subscription-Types", "Subscription Types");
makeSetOfStates("types", "productAssociationTypes", "Admin.Types", "/Product-Association-Types", "Product Association Types");
makeSetOfStates("types", "productDownloadTypes", "Admin.Types", "/Product-Download-Types", "Product Download Types");
makeSetOfStates("types", "productImageTypes", "Admin.Types", "/Product-Image-Types", "Product Image Types");
makeSetOfStates("types", "productTypes", "Admin.Types", "/Product-Types", "Product Types");
makeSetOfStates("types", "purchaseOrderEventTypes", "Admin.Types", "/Purchase-Order-Event-Types", "Purchase Order Event Types");
makeSetOfStates("types", "purchaseOrderTypes", "Admin.Types", "/Purchase-Order-Types", "Purchase Order Types");
makeSetOfStates("types", "questionTypes", "Admin.Types", "/Question-Types", "Question Types");
makeSetOfStates("types", "salesQuoteEventTypes", "Admin.Types", "/Sales-Quote-Event-Types", "Sales Quote Event Types");
makeSetOfStates("types", "salesQuoteTypes", "Admin.Types", "/Sales-Quote-Types", "Sales Quote Types");
makeSetOfStates("types", "reportTypes", "Admin.Types", "/Report-Types", "Report Types");
makeSetOfStates("types", "salesReturnEventTypes", "Admin.Types", "/Sales-Return-Event-Types", "Sales Return Event Types");
makeSetOfStates("types", "salesReturnTypes", "Admin.Types", "/Sales-Return-Types", "Sales Return Types");
makeSetOfStates("types", "reviewTypes", "Admin.Types", "/Review-Types", "Review Types");
makeSetOfStates("types", "salesItemTargetTypes", "Admin.Types", "/Sales-Item-Target-Types", "Sales Item Target Types");
makeSetOfStates("types", "sampleRequestEventTypes", "Admin.Types", "/Sample-Request-Event-Types", "Sample Request Event Types");
makeSetOfStates("types", "sampleRequestTypes", "Admin.Types", "/Sample-Request-Types", "Sample Request Types");
makeSetOfStates("types", "appointmentTypes", "Admin.Types", "/Appointment-Types", "Appointment Types");
makeSetOfStates("types", "scoutCategoryTypes", "Admin.Types", "/Scout-Category-Types", "Scout Category Types");
makeSetOfStates("types", "packageTypes", "Admin.Types", "/Package-Types", "Package Types");
makeSetOfStates("types", "shipmentTypes", "Admin.Types", "/Shipment-Types", "Shipment Types");
makeSetOfStates("types", "cartEventTypes", "Admin.Types", "/Cart-Event-Types", "Cart Event Types");
makeSetOfStates("types", "cartTypes", "Admin.Types", "/Cart-Types", "Cart Types");
makeSetOfStates("types", "storeImageTypes", "Admin.Types", "/Store-Image-Types", "Store Image Types");
makeSetOfStates("types", "storeInventoryLocationTypes", "Admin.Types", "/Store-Inventory-Location-Types", "Store Inventory Location Types");
makeSetOfStates("types", "storeTypes", "Admin.Types", "/Store-Types", "Store Types");
makeSetOfStates("types", "noteTypes", "Admin.Types", "/Note-Types", "Note Types");
makeSetOfStates("types", "recordVersionTypes", "Admin.Types", "/Record-Version-Types", "Record Version Types");
makeSetOfStates("types", "settingTypes", "Admin.Types", "/Setting-Types", "Setting Types");
makeSetOfStates("types", "campaignTypes", "Admin.Types", "/Campaign-Types", "Campaign Types");
makeSetOfStates("types", "eventTypes", "Admin.Types", "/Event-Types", "Event Types");
makeSetOfStates("types", "pageViewTypes", "Admin.Types", "/Page-View-Types", "Page View Types");
makeSetOfStates("types", "vendorImageTypes", "Admin.Types", "/Vendor-Image-Types", "Vendor Image Types");
makeSetOfStates("types", "vendorTypes", "Admin.Types", "/Vendor-Types", "Vendor Types");
// Statuses
makeSetOfStates("statuses", "accountStatuses", "Admin.Statuses", "/Account-Statuses", "Account Statuses");
makeSetOfStates("statuses", "adStatuses", "Admin.Statuses", "/Ad-Statuses", "Ad Statuses");
makeSetOfStates("statuses", "zoneStatuses", "Admin.Statuses", "/Zone-Statuses", "Zone Statuses");
makeSetOfStates("statuses", "auctionStatuses", "Admin.Statuses", "/Auction-Statuses", "Auction Statuses");
makeSetOfStates("statuses", "bidStatuses", "Admin.Statuses", "/Bid-Statuses", "Bid Statuses");
makeSetOfStates("statuses", "lotStatuses", "Admin.Statuses", "/Lot-Statuses", "Lot Statuses");
makeSetOfStates("statuses", "calendarEventStatuses", "Admin.Statuses", "/Calendar-Event-Statuses", "Calendar Event Statuses");
makeSetOfStates("statuses", "referralCodeStatuses", "Admin.Statuses", "/Referral-Code-Statuses", "Referral Code Statuses");
makeSetOfStates("statuses", "userOnlineStatuses", "Admin.Statuses", "/User-Online-Statuses", "User Online Statuses");
makeSetOfStates("statuses", "userStatuses", "Admin.Statuses", "/User-Statuses", "User Statuses");
makeSetOfStates("statuses", "groupStatuses", "Admin.Statuses", "/Group-Statuses", "Group Statuses");
makeSetOfStates("statuses", "salesInvoiceStatuses", "Admin.Statuses", "/Sales-Invoice-Statuses", "Sales Invoice Statuses");
makeSetOfStates("statuses", "emailStatuses", "Admin.Statuses", "/Email-Statuses", "Email Statuses");
makeSetOfStates("statuses", "salesOrderStatuses", "Admin.Statuses", "/Sales-Order-Statuses", "Sales Order Statuses");
makeSetOfStates("statuses", "paymentStatuses", "Admin.Statuses", "/Payment-Statuses", "Payment Statuses");
makeSetOfStates("statuses", "subscriptionStatuses", "Admin.Statuses", "/Subscription-Statuses", "Subscription Statuses");
makeSetOfStates("statuses", "futureImportStatuses", "Admin.Statuses", "/Future-Import-Statuses", "Future Import Statuses");
makeSetOfStates("statuses", "productStatuses", "Admin.Statuses", "/Product-Statuses", "Product Statuses");
makeSetOfStates("statuses", "purchaseOrderStatuses", "Admin.Statuses", "/Purchase-Order-Statuses", "Purchase Order Statuses");
makeSetOfStates("statuses", "salesQuoteStatuses", "Admin.Statuses", "/Sales-Quote-Statuses", "Sales Quote Statuses");
makeSetOfStates("statuses", "salesReturnStatuses", "Admin.Statuses", "/Sales-Return-Statuses", "Sales Return Statuses");
makeSetOfStates("statuses", "sampleRequestStatuses", "Admin.Statuses", "/Sample-Request-Statuses", "Sample Request Statuses");
makeSetOfStates("statuses", "appointmentStatuses", "Admin.Statuses", "/Appointment-Statuses", "Appointment Statuses");
makeSetOfStates("statuses", "shipmentStatuses", "Admin.Statuses", "/Shipment-Statuses", "Shipment Statuses");
makeSetOfStates("statuses", "cartStatuses", "Admin.Statuses", "/Cart-Statuses", "Cart Statuses");
makeSetOfStates("statuses", "campaignStatuses", "Admin.Statuses", "/Campaign-Statuses", "Campaign Statuses");
makeSetOfStates("statuses", "eventStatuses", "Admin.Statuses", "/Event-Statuses", "Event Statuses");
makeSetOfStates("statuses", "iPOrganizationStatuses", "Admin.Statuses", "/I-P-Organization-Statuses", "I P Organization Statuses");
makeSetOfStates("statuses", "pageViewStatuses", "Admin.Statuses", "/Page-View-Statuses", "Page View Statuses");
makeSetOfStates("statuses", "visitStatuses", "Admin.Statuses", "/Visit-Statuses", "Visit Statuses");
// States
makeSetOfStates("states", "salesInvoiceStates", "Admin.States", "/Sales-Invoice-States", "Sales Invoice States");
makeSetOfStates("states", "salesOrderStates", "Admin.States", "/Sales-Order-States", "Sales Order States");
makeSetOfStates("states", "purchaseOrderStates", "Admin.States", "/Purchase-Order-States", "Purchase Order States");
makeSetOfStates("states", "salesQuoteStates", "Admin.States", "/Sales-Quote-States", "Sales Quote States");
makeSetOfStates("states", "salesReturnStates", "Admin.States", "/Sales-Return-States", "Sales Return States");
makeSetOfStates("states", "sampleRequestStates", "Admin.States", "/Sample-Request-States", "Sample Request States");
makeSetOfStates("states", "cartStates", "Admin.States", "/Cart-States", "Cart States");
// Other
makeSetOfStates("types", "attributeGroups", "Admin.Types", "/Attribute-Groups", "Attribute Groups");
makeSetOfStates("types", "attributeTabs", "Admin.Types", "/Attribute-Tabs", "Attribute Tabs");
makeSetOfStates("types", "lotGroups", "Admin.Types", "/Lot-Groups", "Lot Groups");
makeSetOfStates("types", "memberships", "Admin.Types", "/Memberships", "Memberships");
makeSetOfStates("types", "membershipLevels", "Admin.Types", "/Membership-Levels", "Membership Levels");
makeSetOfStates("types", "pricePoints", "Admin.Types", "/Price-Points", "Price Points");
makeSetOfStates("types", "salesReturnReasons", "Admin.Types", "/Sales-Return-Reasons", "Sales Return Reasons");
makeSetOfStates("types", "settingGroups", "Admin.Types", "/Setting-Groups", "Setting Groups");
});
}
