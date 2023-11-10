// <copyright file="_AddendumModelsToBuildOnly.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.MVC.Api.Models
{
	public partial class ProductModel : IHaveImagesBaseModel<ProductImageModel, TypeModel> { }
	public partial class ProductImageModel : IImageBaseModel<TypeModel> { }
	public partial class UserModel : IHaveImagesBaseModel<UserImageModel, TypeModel> { }
	public partial class UserImageModel : IImageBaseModel<TypeModel> { }
#if BRANDADMIN
	public partial class AdZoneAccessModel { }
	public partial class BadgeImageModel { }
	public partial class BadgeModel { }
	public partial class DistrictCurrencyModel { }
	public partial class DistrictImageModel { }
	public partial class DistrictLanguageModel { }
	public partial class DistrictModel { }
	public partial class MembershipAdZoneAccessModel { }
	public partial class SocialProviderModel { }
	public partial class StoredFilePagedResults { }
	public partial class StorePagedResults { }
#elif FRANCHISEADMIN
	public partial class AccountModel { }
	public partial class AuctionPagedResults { }
	public partial class BadgeImageModel { }
	public partial class BadgeModel { }
	public partial class BidModel { }
	public partial class CategoryFilePagedResults { }
	public partial class CategoryModel { }
	public partial class ContactModel { }
	public partial class CurrencyModel { }
	public partial class DistrictCurrencyModel { }
	public partial class DistrictImageModel { }
	public partial class DistrictLanguageModel { }
	public partial class DistrictModel { }
	public partial class LotGroupModel : TypableBaseModel { }
	public partial class LotPagedResults { }
	public partial class MembershipAdZoneAccessByLevelModel { }
	public partial class MembershipAdZoneAccessModel { }
	public partial class MembershipModel { }
	public partial class NoteModel { }
	public partial class PaymentMethodModel { }
	public partial class RateQuoteModel { }
	public partial class SalesInvoiceModel { }
	public partial class SalesItemTargetBaseModel { }
	public partial class SalesOrderModel { }
	public partial class SalesOrderShippingModel { }
	public partial class SalesQuoteModel { }
	public partial class SalesReturnModel { }
	public partial class SalesReturnReasonModel { }
	public partial class SampleRequestModel { }
	public partial class ShipmentModel { }
	public partial class SiteDomainSocialProviderModel { }
	public partial class SocialProviderModel { }
	public partial class StoredFilePagedResults { }
	public partial class StorePagedResults { }
	public partial class UserModel { }
#elif MANUFACTURERADMIN
	public partial class AppliedPurchaseOrderDiscountModel { }
	public partial class AppliedPurchaseOrderItemDiscountModel { }
	public partial class AppliedSalesInvoiceDiscountModel { }
	public partial class AppliedSalesInvoiceItemDiscountModel { }
	public partial class AppliedSalesOrderDiscountModel { }
	public partial class AppliedSalesOrderItemDiscountModel { }
	public partial class BadgeImageModel { }
	public partial class DiscountModel { }
	public partial class DistrictCurrencyModel { }
	public partial class DistrictImageModel { }
	public partial class DistrictLanguageModel { }
	public partial class InventoryLocationRegionModel { }
	public partial class InventoryLocationUserModel { }
	public partial class MembershipAdZoneAccessModel { }
	public partial class PaymentModel { }
	public partial class PriceRuleAccountTypeModel { }
	public partial class PriceRuleBrandModel { }
	public partial class PriceRuleCategoryModel { }
	public partial class PriceRuleCountryModel { }
	public partial class PriceRuleFranchiseModel { }
	public partial class PriceRuleManufacturerModel { }
	public partial class PriceRuleProductModel { }
	public partial class PriceRuleProductTypeModel { }
	public partial class PriceRuleStoreModel { }
	public partial class PriceRuleUserRoleModel { }
	public partial class ProductInventoryLocationSectionModel { }
	public partial class PurchaseOrderContactModel { }
	public partial class PurchaseOrderEventModel { }
	public partial class PurchaseOrderFileModel { }
	public partial class RateQuoteModel { }
	public partial class SalesEventBaseModel { }
	public partial class SalesInvoiceContactModel { }
	public partial class SalesInvoiceEventModel { }
	public partial class SalesInvoiceFileModel { }
	public partial class SalesInvoicePaymentModel { }
	public partial class SalesItemTargetBaseModel { }
	public partial class SalesOrderContactModel { }
	public partial class SalesOrderEventModel { }
	public partial class SalesOrderFileModel { }
	public partial class SalesOrderPaymentModel { }
	public partial class SalesOrderSalesInvoiceModel { }
	public partial class SalesOrderShippingModel { }
	public partial class SalesQuoteModel { }
	public partial class SalesQuoteSalesOrderModel { }
	public partial class SalesReturnModel { }
	public partial class SalesReturnPaymentModel { }
	public partial class SalesReturnReasonModel { }
	public partial class SalesReturnSalesOrderModel { }
	public partial class SampleRequestModel { }
	public partial class SiteDomainModel { }
	public partial class StoredFilePagedResults { }
	public partial class StorePagedResults { }
#elif STOREADMIN
	public partial class BadgeImageModel { }
	public partial class BadgeModel { }
	public partial class DistrictCurrencyModel { }
	public partial class DistrictImageModel { }
	public partial class DistrictLanguageModel { }
	public partial class DistrictModel { }
	public partial class LotGroupModel : TypableBaseModel { }
	public partial class MembershipAdZoneAccessByLevelModel { }
	public partial class MembershipAdZoneAccessModel { }
	public partial class MembershipModel { }
	public partial class SiteDomainSocialProviderModel { }
	public partial class SocialProviderModel { }
#elif VENDORADMIN
	public partial class AdZoneAccessModel { }
	public partial class AppliedSalesInvoiceDiscountModel { }
	public partial class AppliedSalesInvoiceItemDiscountModel { }
	public partial class AppliedSalesOrderDiscountModel { }
	public partial class AppliedSalesOrderItemDiscountModel { }
	public partial class AppliedSalesQuoteDiscountModel { }
	public partial class AppliedSalesQuoteItemDiscountModel { }
	public partial class AppliedSalesReturnDiscountModel { }
	public partial class AppliedSalesReturnItemDiscountModel { }
	public partial class AppliedSampleRequestDiscountModel { }
	public partial class AppliedSampleRequestItemDiscountModel { }
	public partial class BadgeImageModel { }
	public partial class BidModel { }
	public partial class DiscountAccountModel { }
	public partial class DiscountAccountTypeModel { }
	public partial class DiscountBrandModel { }
	public partial class DiscountCategoryModel { }
	public partial class DiscountCodeModel { }
	public partial class DiscountCountryModel { }
	public partial class DiscountFranchiseModel { }
	public partial class DiscountManufacturerModel { }
	public partial class DiscountProductModel { }
	public partial class DiscountProductTypeModel { }
	public partial class DiscountShipCarrierMethodModel { }
	public partial class DiscountStoreModel { }
	public partial class DiscountUserModel { }
	public partial class DiscountUserRoleModel { }
	public partial class DiscountVendorModel { }
	public partial class DistrictCurrencyModel { }
	public partial class DistrictImageModel { }
	public partial class DistrictLanguageModel { }
	public partial class LotPagedResults { }
	public partial class MembershipAdZoneAccessModel { }
	public partial class PaymentMethodModel { }
	public partial class PaymentModel { }
	public partial class SalesInvoiceContactModel { }
	public partial class SalesInvoiceEventModel { }
	public partial class SalesInvoiceFileModel { }
	public partial class SalesInvoicePagedResults { }
	public partial class SalesInvoicePaymentModel { }
	public partial class SalesItemTargetBaseModel { }
	public partial class SalesOrderContactModel { }
	public partial class SalesOrderEventModel { }
	public partial class SalesOrderFileModel { }
	public partial class SalesOrderPagedResults { }
	public partial class SalesOrderPaymentModel { }
	public partial class SalesOrderShippingModel { }
	public partial class SalesQuoteContactModel { }
	public partial class SalesQuoteEventModel { }
	public partial class SalesQuoteFileModel { }
	public partial class SalesQuotePaymentModel { }
	public partial class SalesReturnContactModel { }
	public partial class SalesReturnEventModel { }
	public partial class SalesReturnFileModel { }
	public partial class SalesReturnPaymentModel { }
	public partial class SalesReturnReasonModel { }
	public partial class SampleRequestContactModel { }
	public partial class SampleRequestEventModel { }
	public partial class SampleRequestFileModel { }
	public partial class SampleRequestPaymentModel { }
	public partial class SiteDomainSocialProviderModel { }
	public partial class SocialProviderModel { }
	public partial class StoredFilePagedResults { }
	public partial class StorePagedResults { }
#endif
}
