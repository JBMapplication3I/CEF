// <copyright file="CEFConfig.Shared.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF configuration class, get Storefront CEF Config section</summary>
// ReSharper disable StringLiteralTypo, StyleCop.SA1027
#nullable enable
namespace Clarity.Ecommerce.JSConfigs
{
	using System.Linq;
	using DocumentFormat.OpenXml.Wordprocessing;

	public static partial class CEFConfigDictionary
	{
		private static string FormatRoute(
			string name,
			string? hostUrl,
			string? relativePath,
			Enums.HostLookupMethod lookupMethod,
			Enums.HostLookupWhichUrl? lookupWhichUrl,
			bool extraIndent = false)
		{
			return $@"{name}: {{
{(extraIndent ? "	" : string.Empty)}	host: {(hostUrl != null ? $"\"{hostUrl}\"" : "null")},
{(extraIndent ? "	" : string.Empty)}	hostIsProvidedByLookup: {(lookupMethod == Enums.HostLookupMethod.NotByLookup
		? "false"
		: $"{{ type: \"{lookupMethod}\", whichUrl: \"{lookupWhichUrl?.ToString().ToLower()}\" }}")},
{(extraIndent ? "	" : string.Empty)}	root: {(relativePath != null ? $"\"{relativePath}\"" : "null")}
{(extraIndent ? "	" : string.Empty)}}}";
		}

		private static string GetAuthBlock()
		{
			return $@"authProvider: ""{AuthProviders.ToLower()}"",
authProviderMFAEnabled: {TwoFactorEnabled.ToString().ToLower()},
authProviderMFAForcedEnabled: {TwoFactorForced.ToString().ToLower()},
authProviderMFAByEmailEnabled: {TwoFactorByEmailEnabled.ToString().ToLower()},
authProviderMFABySMSEnabled: {TwoFactorBySMSEnabled.ToString().ToLower()},
authProviderAuthorizeUrl: ""{AuthProviderAuthorizeUrl}"",
authProviderLogoutUrl: ""{AuthProviderLogoutUrl}"",
authProviderClientId: ""{AuthProviderClientId}"",
authProviderRedirectUri: ""{AuthProviderRedirectUri}"",
authProviderScope: ""{AuthProviderScope}"",";
		}

		private static string GetFeatureSet()
		{
			return
$@"featureSet: {{
	ads: {{
		enabled: {AdsEnabled.ToString().ToLower()},
	}},
	addressBook: {{
		enabled: {AddressBookEnabled.ToString().ToLower()},
		dashboardCanAddAddresses: {DashboardAddAddressesToBook.ToString().ToLower()},
		allowMakeThisMyNewDefaultBillingInCheckout: {AddressBookAllowMakeThisMyNewDefaultBillingInCheckout.ToString().ToLower()},
		allowMakeThisMyNewDefaultBillingInDashboard: {AddressBookAllowMakeThisMyNewDefaultBillingInDashboard.ToString().ToLower()},
		allowMakeThisMyNewDefaultShippingInCheckout: {AddressBookAllowMakeThisMyNewDefaultShippingInCheckout.ToString().ToLower()},
		allowMakeThisMyNewDefaultShippingInDashboard: {AddressBookAllowMakeThisMyNewDefaultShippingInDashboard.ToString().ToLower()},
	}},
	affiliates: {{
		enabled: {AffiliatesEnabled.ToString().ToLower()},
	}},
	badges: {{
		enabled: {BadgesEnabled.ToString().ToLower()},
	}},
	brands: {{
		enabled: {BrandsEnabled.ToString().ToLower()},
		removeBrandCatalogSearchFilter: {RemoveBrandCatalogSearchFilter.ToString().ToLower()},
	}},
	calendarEvents: {{
		enabled: {CalendarEventsEnabled.ToString().ToLower()},
	}},
	carts: {{
		enabled: {CartsEnabled.ToString().ToLower()},
		miniCart: {{
			enabled: {MiniCartEnabled.ToString().ToLower()}
		}},
		compare: {{
			enabled: {CartsCompareEnabled.ToString().ToLower()}
		}},
		favoritesList: {{
			enabled: {CartsFavoritesListEnabled.ToString().ToLower()}
		}},
		notifyMeWhenInStock: {{
			enabled: {CartsNotifyMeWhenInStockListEnabled.ToString().ToLower()}
		}},
		shoppingLists: {{
			enabled: {CartsShoppingListsEnabled.ToString().ToLower()},
			assignNameToAndShowDisplayName: {AssignNameToAndShowDisplayName.ToString().ToLower()},
		}},
		orderRequests: {{
			enabled: {CartsOrderRequestsEnabled.ToString().ToLower()}
		}},
		wishList: {{
			enabled: {CartsWishListEnabled.ToString().ToLower()}
		}},
		serviceDebug: {{
			enabled: {CartsAngularServiceDebuggingEnabled.ToString().ToLower()}
		}},
	}},
	categories: {{
		enabled: {CategoriesEnabled.ToString().ToLower()},
		minMax: {CategoriesDoRestrictionsByMinMax.ToString().ToLower()},
	}},
	chat: {{
		enabled: {ChatEnabled.ToString().ToLower()},
	}},
	contacts: {{
		phonePrefixLookups: {{
			enabled: {PhonePrefixLookupsEnabled.ToString().ToLower()},
		}},
	}},
	discounts: {{
		enabled: {DiscountsEnabled.ToString().ToLower()},
	}},
	emails: {{
		enabled: {EmailsEnabled.ToString().ToLower()},
		splitTemplates: {{
			enabled: {EmailsWithSplitTemplatesEnabled.ToString().ToLower()},
		}},
	}},
	franchises: {{
		enabled: {FranchisesEnabled.ToString().ToLower()},
	}},
	inventory: {{
		enabled: {InventoryEnabled.ToString().ToLower()},
		backOrder: {{
			enabled: {InventoryBackOrderEnabled.ToString().ToLower()},
			maxPerProductGlobal: {{
				enabled: {InventoryBackOrderMaxPerProductGlobalEnabled.ToString().ToLower()},
			}},
			maxPerProductPerAccount: {{
				enabled: {InventoryBackOrderMaxPerProductPerAccountEnabled.ToString().ToLower()},
				after: {{
					enabled: {InventoryBackOrderMaxPerProductPerAccountAfterEnabled.ToString().ToLower()},
				}},
			}},
		}},
		preSale: {{
			enabled: {InventoryPreSaleEnabled.ToString().ToLower()},
			maxPerProductGlobal: {{
				enabled: {InventoryPreSaleMaxPerProductGlobalEnabled.ToString().ToLower()},
			}},
			maxPerProductPerAccount: {{
				enabled: {InventoryPreSaleMaxPerProductPerAccountEnabled.ToString().ToLower()},
				after: {{
					enabled: {InventoryPreSaleMaxPerProductPerAccountAfterEnabled.ToString().ToLower()},
				}},
			}},
		}},
		advanced: {{
			enabled: {InventoryAdvancedEnabled.ToString().ToLower()},
			countOnlyThisStoresWarehouseStock: {InventoryCountOnlyThisStoresWarehouseStock.ToString().ToLower()},
		}},
		unlimitedInventory: {{
			enabled: {UnlimitedInventoryEnabled.ToString().ToLower()}
		}},
	}},
	languages: {{
		enabled: {LanguagesEnabled.ToString().ToLower()},
		default: ""{DefaultLanguage}"",
	}},
	login: {{
		enabled: {LoginEnabled.ToString().ToLower()},
		emailForForgot: ""{ForgotUsernameEmailFrom}"",
	}},
	manufacturers: {{
		enabled: {ManufacturersEnabled.ToString().ToLower()},
		minMax: {ManufacturersDoRestrictionsByMinMax.ToString().ToLower()},
	}},
	messaging: {{
		enabled: {MessagingEnabled.ToString().ToLower()},
	}},
	multiCurrency: {{
		enabled: {MultiCurrencyEnabled.ToString().ToLower()},
	}},
	nonProductFavorites: {{
		enabled: {NonProductFavoritesEnabled.ToString().ToLower()},
	}},
	payments: {{
		enabled: {PaymentsEnabled.ToString().ToLower()},
		memberships: {{
			enabled: {PaymentsWithMembershipsEnabled.ToString().ToLower()},
		}},
		methods: {{
			creditCard: {{
				uplifts: {{
					useGreater: {PaymentsByCreditCardUpliftUseWhicheverIsGreater.ToString().ToLower()},
					percent: {PaymentsByCreditCardUpliftPercent},
					amount: {PaymentsByCreditCardUpliftAmount},
				}}
			}},
			eCheck: {{
				uplifts: {{
					useGreater: {PaymentsByECheckUpliftUseWhicheverIsGreater.ToString().ToLower()},
					percent: {PaymentsByECheckUpliftPercent},
					amount: {PaymentsByECheckUpliftAmount},
				}},
			}},
			invoice: {{
				uplifts: {{
					useGreater: {PaymentsByInvoiceUpliftUseWhicheverIsGreater.ToString().ToLower()},
					percent: {PaymentsByInvoiceUpliftPercent},
					amount: {PaymentsByInvoiceUpliftAmount},
				}},
			}},
			payPal: {{
				uplifts: {{
					useGreater: {PaymentsByPayPalUpliftUseWhicheverIsGreater.ToString().ToLower()},
					percent: {PaymentsByPayPalUpliftPercent},
					amount: {PaymentsByPayPalUpliftAmount},
				}},
			}},
		}},
		subscriptions: {{
			enabled: {PaymentsWithSubscriptionsEnabled.ToString().ToLower()},
		}},
		wallet: {{
			enabled: {PaymentsWalletEnabled.ToString().ToLower()},
			creditCard: {{
				enabled: {PaymentsWalletCreditCardEnabled.ToString().ToLower()},
			}},
			eCheck: {{
				enabled: {PaymentsWalletEcheckEnabled.ToString().ToLower()},
			}},
		}},
	}},
	pricing: {{
		enabled: {PricingEnabled.ToString().ToLower()},
		priceRules: {{
			enabled: {PricingPriceRulesEnabled.ToString().ToLower()},
		}},
		pricePoints: {{
			enabled: {PricingTieredPricingEnabled.ToString().ToLower()},
		}},
		msrp: {{
			enabled: {PricingMsrpEnabled.ToString().ToLower()},
		}},
		reduction: {{
			enabled: {PricingReductionEnabled.ToString().ToLower()},
		}},
	}},
	products: {{
		categoryAttributes: {{
			enabled: {ProductCategoryAttributesEnabled.ToString().ToLower()},
		}},
		futureImports: {{
			enabled: {ProductFutureImportsEnabled.ToString().ToLower()},
		}},
		notifications: {{
			enabled: {ProductNotificationsEnabled.ToString().ToLower()},
		}},
		restrictions: {{
			enabled: {ProductRestrictionsEnabled.ToString().ToLower()},
		}},
		storedFiles: {{
			enabled: {ProductStoredFilesEnabled.ToString().ToLower()},
		}},
		filterByAccountRoles: {FilterByAccountRoles.ToString().ToLower()},
	}},
	profanityFilter: {{
		enabled: {ProfanityFilterEnabled.ToString().ToLower()},
	}},
	profile: {{
		enabled: {DashboardRouteAccountSettingsMyProfileEnabled.ToString().ToLower()},
		images: {{
			enabled: {MyProfileImagesEnabled.ToString().ToLower()},
		}},
		storedFiles: {{
			enabled: {MyProfileStoredFilesEnabled.ToString().ToLower()},
		}},
	}},
	accountProfile: {{
		enabled: {DashboardRouteAccountSettingsAccountProfileEnabled.ToString().ToLower()},
		images: {{
			enabled: {AccountProfileImagesEnabled.ToString().ToLower()},
		}},
		storedFiles: {{
			enabled: {AccountProfileStoredFilesEnabled.ToString().ToLower()},
		}},
	}},
	purchaseOrders: {{
		enabled: {PurchaseOrdersEnabled.ToString().ToLower()},
		hasIntegratedKeys: {PurchaseOrdersHasIntegratedKeys.ToString().ToLower()},
	}},
	purchasing: {{
		availabilityDates: {{
			enabled: {PurchasingAvailabilityDatesEnabled.ToString().ToLower()},
		}},
		documentRequired: {{
			enabled: {PurchasingDocumentRequiredEnabled.ToString().ToLower()},
			override: {{
				enabled: {PurchasingDocumentRequiredOverrideEnabled.ToString().ToLower()},
			}},
		}},
		minMax: {{
			enabled: {PurchasingMinMaxEnabled.ToString().ToLower()},
			after: {{
				enabled: {PurchasingMinMaxAfterEnabled.ToString().ToLower()},
			}},
		}},
		mustPurchaseInMultiplesOf: {{
			enabled: {PurchasingMustPurchaseInMultiplesOfEnabled.ToString().ToLower()},
			override: {{
				enabled: {PurchasingMustPurchaseInMultiplesOfOverrideEnabled.ToString().ToLower()},
			}},
		}},
		roleRequiredToPurchase: {{
			enabled: {PurchasingRoleRequiredToPurchaseEnabled.ToString().ToLower()},
		}},
		roleRequiredToSee: {{
			enabled: {PurchasingRoleRequiredToSeeEnabled.ToString().ToLower()},
		}},
		
	}},
	referralRegistrations: {{
		enabled: {ReferralRegistrationsEnabled.ToString().ToLower()},
	}},
	registration: {{
		useSpecialCharInEmail: {UseSpecialCharInEmail.ToString().ToLower()},
		usernameIsEmail: {AuthProviderUsernameIsEmail.ToString().ToLower()},
		addressBookIsRequired: {AddressBookRequiredInRegistration.ToString().ToLower()},
		walletIsRequired: {PaymentsWalletRequiredInRegistration.ToString().ToLower()},
		verificationIsRequired: {RequireEmailVerificationForNewUsers.ToString().ToLower()},
	}},
	reporting: {{
		enabled: {ReportingEnabled.ToString().ToLower()},
		devExpress: {ReportingDevExpressEnabled.ToString().ToLower()},
		syncFusion: {ReportingSyncFusionEnabled.ToString().ToLower()},
	}},
	reviews: {{
		enabled: {ReviewsEnabled.ToString().ToLower()},
	}},
	salesGroups: {{
		enabled: {SalesGroupsEnabled.ToString().ToLower()},
		hasIntegratedKeys: {SalesGroupsHasIntegratedKeys.ToString().ToLower()},
	}},
	salesInvoices: {{
		enabled: {SalesInvoicesEnabled.ToString().ToLower()},
		hasIntegratedKeys: {SalesInvoicesHasIntegratedKeys.ToString().ToLower()},
		lateFees: [ {SalesInvoicesLateFees
			?.Select(x => $"{{ \"day\": {x.Day}, \"amount\": {x.Amount}, \"kind\": \"{(x.Kind == 'p' ? "percentage" : "amount")}\" }}")
			.DefaultIfEmpty(string.Empty)
			.Aggregate((c, n) => c + ", " + n) ?? string.Empty} ],
		canPayViaUserDashboard: {{
			single: {{
				enabled: {SalesInvoicesCustomerCanPayViaUserDashboardSingle.ToString().ToLower()},
				partial: {SalesInvoicesCustomerCanPayViaUserDashboardSinglePartially.ToString().ToLower()},
			}},
			multiple: {{
				enabled: {SalesInvoicesCustomerCanPayViaUserDashboardMulti.ToString().ToLower()},
				partial: {SalesInvoicesCustomerCanPayViaUserDashboardMultiPartially.ToString().ToLower()},
			}},
		}},
		canPayViaCSR: {{
			single: {{
				enabled: {SalesInvoicesCSRCanPayViaAdminWithData.ToString().ToLower()},
				partial: {SalesInvoicesCSRCanPayViaAdminWithDataPartially.ToString().ToLower()},
			}},
			multiple: {{
				enabled: {SalesInvoicesCSRCanPayViaAdminWithoutData.ToString().ToLower()},
				partial: {SalesInvoicesCSRCanPayViaAdminWithoutDataPartially.ToString().ToLower()},
			}},
		}},
	}},
	salesOrders: {{
		enabled: {SalesOrdersEnabled.ToString().ToLower()},
		hasIntegratedKeys: {SalesOrdersHasIntegratedKeys.ToString().ToLower()},
		enforceOrderLimits: {EnforceOrderLimits.ToString().ToLower()},
	}},
	salesQuotes: {{
		enabled: {SalesQuotesEnabled.ToString().ToLower()},
		useQuoteCart: {SalesQuotesUseQuoteCart.ToString().ToLower()},
		hasIntegratedKeys: {SalesQuotesHasIntegratedKeys.ToString().ToLower()},
		includeQuantity: {SalesQuotesIncludeQuantityColumn.ToString().ToLower()}
	}},
	salesReturns: {{
		enabled: {SalesReturnsEnabled.ToString().ToLower()},
		hasIntegratedKeys: {SalesReturnsHasIntegratedKeys.ToString().ToLower()},
	}},
	sampleRequests: {{
		enabled: {SampleRequestsEnabled.ToString().ToLower()},
		hasIntegratedKeys: {SampleRequestsHasIntegratedKeys.ToString().ToLower()},
	}},
	shipping: {{
		enabled: {ShippingEnabled.ToString().ToLower()},
		events: {{
			enabled: {ShippingEventsEnabled.ToString().ToLower()},
		}},
		handlingFees: {{
			enabled: {ShippingHandlingFeesEnabled.ToString().ToLower()},
		}},
		leadTimes: {{
			enabled: {ShippingLeadTimesEnabled.ToString().ToLower()},
		}},
		packages: {{
			enabled: {ShippingPackagesEnabled.ToString().ToLower()},
		}},
		masterPacks: {{
			enabled: {ShippingMasterPacksEnabled.ToString().ToLower()},
		}},
		pallets: {{
			enabled: {ShippingPalletsEnabled.ToString().ToLower()},
		}},
		rates: {{
			enabled: {ShippingRatesEnabled.ToString().ToLower()},
			estimator: {{
				enabled: {ShippingEstimatesEnabled.ToString().ToLower()},
			}},
			flat: {{
				enabled: {ShippingRatesFlatEnabled.ToString().ToLower()},
			}},
			productsCanBeFreeShipping: {ShippingRatesProductsCanBeFree.ToString().ToLower()},
			freeShippingThreshold: {{
				global: {{
					enabled: {ShippingRatesFreeThresholdGlobalEnabled.ToString().ToLower()},
					amount: {ShippingRatesFreeThresholdGlobalAmount?.ToString("N3") ?? "null"},
				}},
			}},
		}},
		restrictions: {{
			enabled: {RestrictedShippingEnabled.ToString().ToLower()},
		}},
		splitShipping: {{
			enabled: {SplitShippingEnabled.ToString().ToLower()},
			onlyAllowOneDestination: {SplitShippingOnlyAllowOneDestination.ToString().ToLower()},
		}},
		trackingNumbers: {{
			fedex: ""{FedExTrackingLink}"",
		}},
	}},
	signalR: {{
		enabled: {SignalREnabled.ToString().ToLower()},
	}},
	stores: {{
		enabled: {StoresEnabled.ToString().ToLower()},
		myStoreAdmin: {{
			enabled: {MyStoreAdminEnabled.ToString().ToLower()},
		}},
		myBrandAdmin: {{
			enabled: {MyBrandAdminEnabled.ToString().ToLower()},
		}},
		siteDomains: {{
			enabled: {BrandsSiteDomainsEnabled.ToString().ToLower()},
		}},
		minMax: {StoresDoRestrictionsByMinMax.ToString().ToLower()},
	}},
	taxes: {{
		enabled: {TaxesEnabled.ToString().ToLower()},
		avalara: {{
			enabled: {TaxesAvalaraEnabled.ToString().ToLower()},
		}},
	}},
	tracking: {{
		enabled: {TrackingEnabled.ToString().ToLower()},
		expires: {TrackingExpiresAfterXMinutes},
	}},
	users: {{
		storedFiles: {{
			enabled: {UserStoredFilesEnabled.ToString().ToLower()},
		}},
	}},
	vendors: {{
		enabled: {VendorsEnabled.ToString().ToLower()},
		minMax: {VendorsDoRestrictionsByMinMax.ToString().ToLower()},
	}},
}},";
		}

		private static string GetPurchaseBlock()
		{
			return
$@"checkout: {{
	flags: {{
		createAccount: {PurchaseGuestCreateAccountStartingValue.ToString().ToLower()}
	}},
	useRecentlyUsedAddresses: {PurchaseUseRecentlyUsedAddresses.ToString().ToLower()},
	mode: {(PurchaseMode == Enums.CheckoutModes.Targets ? "1" : "0")},
	usernameIsEmail: {AuthProviderUsernameIsEmail.ToString().ToLower()},
	paymentOptions: {{
		creditCard: {PaymentsByCreditCardEnabled.ToString().ToLower()},
		echeck: {PaymentsByEcheckEnabled.ToString().ToLower()},
		invoice: {PaymentsByInvoiceEnabled.ToString().ToLower()},
		payPal: {PaymentsByPayPalEnabled.ToString().ToLower()}{
			(PaymentsByCustom.Any()
				? PaymentsByCustom.Aggregate(string.Empty, (c, n) => $"{c},\r\n\t\t{n}: true")
				: string.Empty)}
	}},
	cart: {{
		type: ""{PurchaseDefaultCartType}""
	}},
	store: {{
		showShipToStoreOption: {ShippingShipToStoreEnabled.ToString().ToLower()},
		showInStorePickupOption: {ShippingInStorePickupEnabled.ToString().ToLower()}
	}},
	finalActionButtonText: ""{PurchaseFinalActionButtonText}"",
	specialInstructions: {PurchaseSpecialInstructionsEnabled.ToString().ToLower()}
}},
purchase: {{
	allowGuest: {GuestPurchaseEnabled.ToString().ToLower()},
	showSpecialInstructions: {PurchaseShippingShowSpecialInstructions.ToString().ToLower()},
	paymentMethods: {{{(PaymentsByACHEnabled ? $@"
		purchasePaymentMethodACH: {{
			name: ""purchasePaymentMethodACH"",
			titleKey: ""{PaymentsByACHTitleKey}"",
			order: {PaymentsByACHPosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/ach/view.html"",
			uplifts: {{
				useGreater: {PaymentsByACHUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByACHUpliftPercent},
				amount: {PaymentsByACHUpliftAmount}
			}},
			restrictedByAccountTypes: ""{(!string.IsNullOrWhiteSpace(PaymentsByACHRestrictedAccountTypes) ? PaymentsByACHRestrictedAccountTypes : string.Empty)}""
		}}," : string.Empty)}{(PaymentsByCreditCardEnabled ? $@"
		purchasePaymentMethodCreditCard: {{
			name: ""purchasePaymentMethodCreditCard"",
			titleKey: ""{PaymentsByCreditCardTitleKey}"",
			order: {PaymentsByCreditCardPosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/creditCard/view.html"",
			uplifts: {{
				useGreater: {PaymentsByCreditCardUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByCreditCardUpliftPercent},
				amount: {PaymentsByCreditCardUpliftAmount}
			}},
			restrictedByAccountTypes: ""{(!string.IsNullOrWhiteSpace(PaymentsByCreditCardRestrictedAccountTypes) ? PaymentsByCreditCardRestrictedAccountTypes : string.Empty)}""
		}}," : string.Empty)}{(PaymentsByEcheckEnabled ? $@"
		purchasePaymentMethodEcheck: {{
			name: ""purchasePaymentMethodEcheck"",
			titleKey: ""{PaymentsByEcheckTitleKey}"",
			order: {PaymentsByEcheckPosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/echeck/view.html"",
			uplifts: {{
				useGreater: {PaymentsByECheckUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByECheckUpliftPercent},
				amount: {PaymentsByECheckUpliftAmount}
			}},
			restrictedByAccountTypes: ""{(!string.IsNullOrWhiteSpace(PaymentsByECheckRestrictedAccountTypes) ? PaymentsByECheckRestrictedAccountTypes : string.Empty)}""
		}}," : string.Empty)}{(PaymentsByFreeEnabled ? $@"
		purchasePaymentMethodFree: {{
			name: ""purchasePaymentMethodFree"",
			titleKey: ""{PaymentsByFreeTitleKey}"",
			order: {PaymentsByFreePosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/free/view.html"",
			uplifts: {{
				useGreater: false,
				percent: 0,
				amount: 0
			}}
		}}," : string.Empty)}{(PaymentsByInvoiceEnabled ? $@"
		purchasePaymentMethodInvoice: {{
			name: ""purchasePaymentMethodInvoice"",
			titleKey: ""{PaymentsByInvoiceTitleKey}"",
			order: {PaymentsByInvoicePosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/invoice/view.html"",
			uplifts: {{
				useGreater: {PaymentsByInvoiceUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByInvoiceUpliftPercent},
				amount: {PaymentsByInvoiceUpliftAmount}
			}},
			restrictedByAccountTypes: ""{(!string.IsNullOrWhiteSpace(PaymentsByInvoiceRestrictedAccountTypes) ? PaymentsByInvoiceRestrictedAccountTypes : string.Empty)}""
		}}," : string.Empty)}{(PaymentsByPayPalEnabled ? $@"
		purchasePaymentMethodPayPal: {{
			name: ""purchasePaymentMethodPayPal"",
			titleKey: ""{PaymentsByPayPalTitleKey}"",
			order: {PaymentsByPayPalPosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/payPal/view.html"",
			uplifts: {{
				useGreater: {PaymentsByPayPalUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByPayPalUpliftPercent},
				amount: {PaymentsByPayPalUpliftAmount}
			}},
			restrictedByAccountTypes: ""{(!string.IsNullOrWhiteSpace(PaymentsByPayPalRestrictedAccountTypes) ? PaymentsByPayPalRestrictedAccountTypes : string.Empty)}""
		}}," : string.Empty)}{(PaymentsByPayoneerEnabled ? $@"
		purchasePaymentMethodPayoneer: {{
			name: ""purchasePaymentMethodPayoneer"",
			titleKey: ""{PaymentsByPayoneerTitleKey}"",
			order: {PaymentsByPayoneerPosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/payoneer/view.html"",
			uplifts: {{
				useGreater: {PaymentsByPayoneerUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByPayoneerUpliftPercent},
				amount: {PaymentsByPayoneerUpliftAmount}
			}},
			restrictedByAccountTypes: ""{(!string.IsNullOrWhiteSpace(PaymentsByPayoneerRestrictedAccountTypes) ? PaymentsByPayoneerRestrictedAccountTypes : string.Empty)}""
		}}," : string.Empty)}{(PaymentsByQuoteMeEnabled ? $@"
		purchasePaymentMethodQuoteMe: {{
			name: ""purchasePaymentMethodQuoteMe"",
			titleKey: ""{PaymentsByQuoteMeTitleKey}"",
			order: {PaymentsByQuoteMePosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/quoteMe/view.html"",
			uplifts: {{
				useGreater: {PaymentsByQuoteMeUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByQuoteMeUpliftPercent},
				amount: {PaymentsByQuoteMeUpliftAmount}
			}},
			restrictedByAccountTypes: ""{(!string.IsNullOrWhiteSpace(PaymentsByQuoteMeRestrictedAccountTypes) ? PaymentsByQuoteMeRestrictedAccountTypes : string.Empty)}""
		}}," : string.Empty)}{(PaymentsByStoreCreditEnabled ? $@"
		purchasePaymentMethodStoreCredit: {{
			name: ""purchasePaymentMethodStoreCredit"",
			titleKey: ""{PaymentsByStoreCreditTitleKey}"",
			order: {PaymentsByStoreCreditPosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/storeCredit/view.html"",
			uplifts: {{
				useGreater: {PaymentsByStoreCreditUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByStoreCreditUpliftPercent},
				amount: {PaymentsByStoreCreditUpliftAmount}
			}},
			restrictedByAccountTypes: ""{(!string.IsNullOrWhiteSpace(PaymentsByStoreCreditRestrictedAccountTypes) ? PaymentsByStoreCreditRestrictedAccountTypes : string.Empty)}""
		}}," : string.Empty)}{(PaymentsByWireTransferEnabled ? $@"
		purchasePaymentMethodWireTransfer: {{
			name: ""purchasePaymentMethodWireTransfer"",
			titleKey: ""{PaymentsByWireTransferTitleKey}"",
			order: {PaymentsByWireTransferPosition},
			templateURL: ""/framework/store/purchasing/steps/payment/methods/wireTransfer/view.html"",
			uplifts: {{
				useGreater: {PaymentsByWireTransferUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByWireTransferUpliftPercent},
				amount: {PaymentsByWireTransferUpliftAmount}
			}},
			restrictedByAccountTypes: ""{(!string.IsNullOrWhiteSpace(PaymentsByWireTransferRestrictedAccountTypes) ? PaymentsByWireTransferRestrictedAccountTypes : string.Empty)}""
		}}" : string.Empty)}{
		(PaymentsByCustom.Any()
			? PaymentsByCustom.Aggregate(string.Empty, (c, n) => $"{c},\r\n\t\t{n}: true")
			: string.Empty)}
	}},
	sections: {{{(PurchasePanesMethodEnabled ? $@"
		purchaseStepMethod: {{
			name: ""purchaseStepMethod"",
			titleKey: ""{PurchasePanesMethodTitle}"",
			show: true,
			showButton: {PurchasePanesMethodShowButton.ToString().ToLower()},
			order: {PurchasePanesMethodPosition},
			templateURL: ""/framework/store/purchasing/steps/method/view.html"",
			continueTextKey: ""{PurchasePanesMethodContinueText}""
		}}," : string.Empty)}{(BillingEnabled ? $@"
		purchaseStepBilling: {{
			name: ""purchaseStepBilling"",
			titleKey: ""{PurchasePanesBillingTitle}"",
			show: {PurchasePanesBillingShow.ToString().ToLower()},
			showButton: {PurchasePanesBillingShowButton.ToString().ToLower()},
			order: {PurchasePanesBillingPosition},
			templateURL: ""/framework/store/purchasing/steps/billing/view.html"",
			continueTextKey: ""{PurchasePanesBillingContinueText}"",
			showMakeThisMyDefault: {PurchaseBillingShowMakeThisMyDefault.ToString().ToLower()}
		}}," : string.Empty)}{(PurchasePanesShippingEnabled && !SplitShippingEnabled ? $@"
		purchaseStepShipping: {{
			name: ""purchaseStepShipping"",
			titleKey: ""{PurchasePanesShippingTitle}"",
			show: {PurchasePanesShippingShow.ToString().ToLower()},
			showButton: {PurchasePanesShippingShowButton.ToString().ToLower()},
			order: {PurchasePanesShippingPosition},
			templateURL: ""/framework/store/purchasing/steps/shipping/view.html"",
			continueTextKey: ""{PurchasePanesShippingContinueText}"",
			showMakeThisMyDefault: {PurchaseShippingShowMakeThisMyDefault.ToString().ToLower()}
		}}," : string.Empty)}{(PurchasePanesShippingEnabled && SplitShippingEnabled ? $@"
		purchaseStepSplitShipping: {{
			name: ""purchaseStepSplitShipping"",
			titleKey: ""{PurchasePanesShippingTitle}"",
			show: {PurchasePanesShippingShow.ToString().ToLower()},
			showButton: {PurchasePanesShippingShowButton.ToString().ToLower()},
			order: {PurchasePanesShippingPosition},
			templateURL: ""/framework/store/purchasing/steps/splitShipping/view.html"",
			continueTextKey: ""{PurchasePanesShippingContinueText}""
		}}," : string.Empty)}{(PurchasePanesPaymentsEnabled ? $@"
		purchaseStepPayment: {{
			name: ""purchaseStepPayment"",
			titleKey: ""{PurchasePanesPaymentsTitle}"",
			show: {PurchasePanesPaymentsShow.ToString().ToLower()},
			showButton: {PurchasePanesPaymentsShowButton.ToString().ToLower()},
			order: {PurchasePanesPaymentsPosition},
			templateURL: ""/framework/store/purchasing/steps/payment/view.html"",
			continueTextKey: ""{PurchasePanesPaymentsContinueText}""
		}}," : string.Empty)}{(PurchasePanesConfirmationEnabled ? $@"
		purchaseStepConfirmation: {{
			name: ""purchaseStepConfirmation"",
			titleKey: ""{PurchasePanesConfirmationTitle}"",
			show: {PurchasePanesConfirmationShow.ToString().ToLower()},
			showButton: {PurchasePanesConfirmationShowButton.ToString().ToLower()},
			order: {PurchasePanesConfirmationPosition},
			templateURL: ""/framework/store/purchasing/steps/confirmation/view.html"",
			continueTextKey: ""{PurchasePanesConfirmationContinueText}""
		}}," : string.Empty)}{(PurchasePanesCompletedEnabled ? $@"
		purchaseStepCompleted: {{
			name: ""purchaseStepCompleted"",
			titleKey: ""{PurchasePanesCompletedTitle}"",
			show: {PurchasePanesCompletedShow.ToString().ToLower()},
			showButton: {PurchasePanesCompletedShowButton.ToString().ToLower()},
			order: {PurchasePanesCompletedPosition},
			templateURL: ""/framework/store/purchasing/steps/completed/view.html"",
			continueTextKey: ""{PurchasePanesCompletedContinueText}""
		}}" : string.Empty)}
	}},
}},";
		}

		private static string GetSubmitQuoteBlock()
		{
			return
$@"submitQuote: {{
	allowGuest: {GuestSubmitQuoteEnabled.ToString().ToLower()},
	miniCart: {{
		enabled: {SubmitQuoteMiniCartEnabled.ToString().ToLower()},
	}},
	paymentMethods: {{{(PaymentsByACHEnabled ? $@"
		purchasePaymentMethodACH: {{
			name: ""purchasePaymentMethodACH"",
			titleKey: ""{PaymentsByACHTitleKey}"",
			order: {PaymentsByACHPosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/ach/view.html"",
			uplifts: {{
				useGreater: {PaymentsByACHUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByACHUpliftPercent},
				amount: {PaymentsByACHUpliftAmount}
			}}
		}}," : string.Empty)}{(PaymentsByCreditCardEnabled ? $@"
		purchasePaymentMethodCreditCard: {{
			name: ""purchasePaymentMethodCreditCard"",
			titleKey: ""{PaymentsByCreditCardTitleKey}"",
			order: {PaymentsByCreditCardPosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/creditCard/view.html"",
			uplifts: {{
				useGreater: {PaymentsByCreditCardUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByCreditCardUpliftPercent},
				amount: {PaymentsByCreditCardUpliftAmount}
			}}
		}}," : string.Empty)}{(PaymentsByEcheckEnabled ? $@"
		purchasePaymentMethodEcheck: {{
			name: ""purchasePaymentMethodEcheck"",
			titleKey: ""{PaymentsByEcheckTitleKey}"",
			order: {PaymentsByEcheckPosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/echeck/view.html"",
			uplifts: {{
				useGreater: {PaymentsByECheckUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByECheckUpliftPercent},
				amount: {PaymentsByECheckUpliftAmount}
			}}
		}}," : string.Empty)}{(PaymentsByFreeEnabled ? $@"
		purchasePaymentMethodFree: {{
			name: ""purchasePaymentMethodFree"",
			titleKey: ""{PaymentsByFreeTitleKey}"",
			order: {PaymentsByFreePosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/free/view.html"",
			uplifts: {{
				useGreater: false,
				percent: 0,
				amount: 0
			}}
		}}," : string.Empty)}{(PaymentsByInvoiceEnabled ? $@"
		purchasePaymentMethodInvoice: {{
			name: ""purchasePaymentMethodInvoice"",
			titleKey: ""{PaymentsByInvoiceTitleKey}"",
			order: {PaymentsByInvoicePosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/invoice/view.html"",
			uplifts: {{
				useGreater: {PaymentsByInvoiceUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByInvoiceUpliftPercent},
				amount: {PaymentsByInvoiceUpliftAmount}
			}}
		}}," : string.Empty)}{(PaymentsByPayPalEnabled ? $@"
		purchasePaymentMethodPayPal: {{
			name: ""purchasePaymentMethodPayPal"",
			titleKey: ""{PaymentsByPayPalTitleKey}"",
			order: {PaymentsByPayPalPosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/payPal/view.html"",
			uplifts: {{
				useGreater: {PaymentsByPayPalUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByPayPalUpliftPercent},
				amount: {PaymentsByPayPalUpliftAmount}
			}}
		}}," : string.Empty)}{(PaymentsByPayoneerEnabled ? $@"
		purchasePaymentMethodPayoneer: {{
			name: ""purchasePaymentMethodPayoneer"",
			titleKey: ""{PaymentsByPayoneerTitleKey}"",
			order: {PaymentsByPayoneerPosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/payoneer/view.html"",
			uplifts: {{
				useGreater: {PaymentsByPayoneerUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByPayoneerUpliftPercent},
				amount: {PaymentsByPayoneerUpliftAmount}
			}}
		}}," : string.Empty)}{(PaymentsByQuoteMeEnabled ? $@"
		purchasePaymentMethodQuoteMe: {{
			name: ""purchasePaymentMethodQuoteMe"",
			titleKey: ""{PaymentsByQuoteMeTitleKey}"",
			order: {PaymentsByQuoteMePosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/quoteMe/view.html"",
			uplifts: {{
				useGreater: {PaymentsByQuoteMeUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByQuoteMeUpliftPercent},
				amount: {PaymentsByQuoteMeUpliftAmount}
			}}
		}}," : string.Empty)}{(PaymentsByStoreCreditEnabled ? $@"
		purchasePaymentMethodStoreCredit: {{
			name: ""purchasePaymentMethodStoreCredit"",
			titleKey: ""{PaymentsByStoreCreditTitleKey}"",
			order: {PaymentsByStoreCreditPosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/storeCredit/view.html"",
			uplifts: {{
				useGreater: {PaymentsByStoreCreditUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByStoreCreditUpliftPercent},
				amount: {PaymentsByStoreCreditUpliftAmount}
			}}
		}}," : string.Empty)}{(PaymentsByWireTransferEnabled ? $@"
		purchasePaymentMethodWireTransfer: {{
			name: ""purchasePaymentMethodWireTransfer"",
			titleKey: ""{PaymentsByWireTransferTitleKey}"",
			order: {PaymentsByWireTransferPosition},
			templateURL: ""/framework/store/quotes/steps/payment/methods/wireTransfer/view.html"",
			uplifts: {{
				useGreater: {PaymentsByWireTransferUpliftUseWhicheverIsGreater.ToString().ToLower()},
				percent: {PaymentsByWireTransferUpliftPercent},
				amount: {PaymentsByWireTransferUpliftAmount}
			}}
		}}" : string.Empty)}{
		(PaymentsByCustom.Any()
			? PaymentsByCustom.Aggregate(string.Empty, (c, n) => $"{c},\r\n\t\t{n}: true")
			: string.Empty)}
	}},
	sections: {{{(SubmitQuotePanesMethodEnabled ? $@"
		submitQuotePaneMethod: {{
			name: ""submitQuotePaneMethod"",
			titleKey: ""{SubmitQuotePanesMethodTitle}"",
			show: true,
			showButton: {SubmitQuotePanesMethodShowButton.ToString().ToLower()},
			order: {SubmitQuotePanesMethodPosition},
			templateURL: ""/framework/store/quotes/steps/method/view.html"",
			continueTextKey: ""{SubmitQuotePanesMethodContinueText}""
		}}," : string.Empty)}{(SubmitQuotePanesShippingEnabled && !SplitShippingEnabled ? $@"
		submitQuotePaneShipping: {{
			name: ""submitQuotePaneShipping"",
			titleKey: ""{SubmitQuotePanesShippingTitle}"",
			show: true,
			showButton: {SubmitQuotePanesShippingShowButton.ToString().ToLower()},
			order: {SubmitQuotePanesShippingPosition},
			templateURL: ""/framework/store/quotes/steps/shipping/view.html"",
			continueTextKey: ""{SubmitQuotePanesShippingContinueText}"",
			showMakeThisMyDefault: false
		}}," : string.Empty)}{(SubmitQuotePanesShippingEnabled && SplitShippingEnabled ? $@"
		submitQuotePaneSplitShipping: {{
			name: ""submitQuotePaneSplitShipping"",
			titleKey: ""{SubmitQuotePanesShippingTitle}"",
			show: true,
			showButton: {SubmitQuotePanesShippingShowButton.ToString().ToLower()},
			order: {SubmitQuotePanesShippingPosition},
			templateURL: ""/framework/store/quotes/steps/splitShipping/view.html"",
			continueTextKey: ""{SubmitQuotePanesShippingContinueText}""
		}}," : string.Empty)}{(SubmitQuotePanesConfirmationEnabled || SubmitQuotePanesConfirmationConditionallyEnabled ? $@"
		submitQuotePaneConfirmation: {{
			name: ""submitQuotePaneConfirmation"",
			titleKey: ""{SubmitQuotePanesConfirmationTitle}"",
			show: {SubmitQuotePanesConfirmationEnabled.ToString().ToLower()},
			showConditionally: {SubmitQuotePanesConfirmationConditionallyEnabled.ToString().ToLower()},
			showButton: {SubmitQuotePanesConfirmationShowButton.ToString().ToLower()},
			order: {SubmitQuotePanesConfirmationPosition},
			templateURL: ""/framework/store/quotes/steps/confirmation/view.html"",
			continueTextKey: ""{SubmitQuotePanesConfirmationContinueText}""
		}}," : string.Empty)}{(SubmitQuotePanesCompletedEnabled ? $@"
		submitQuotePaneCompleted: {{
			name: ""submitQuotePaneCompleted"",
			titleKey: ""{SubmitQuotePanesCompletedTitle}"",
			show: true,
			showButton: {SubmitQuotePanesCompletedShowButton.ToString().ToLower()},
			order: {SubmitQuotePanesCompletedPosition},
			templateURL: ""/framework/store/quotes/steps/completed/view.html"",
			continueTextKey: ""{SubmitQuotePanesCompletedContinueText}""
		}}" : string.Empty)}
	}},
}},";
		}

		private static string GetRegisterBlock()
		{
			return
$@"register: {{
	sections: {{{(RegistrationPanesBasicInfoEnabled ? $@"
		registrationStepBasicInfo: {{
			name: ""registrationStepBasicInfo"",
			titleKey: ""{RegistrationPanesBasicInfoTitle}"",
			show: true,
			showButton: {RegistrationPanesBasicInfoShowButton.ToString().ToLower()},
			order: {RegistrationPanesBasicInfoPosition},
			templateURL: ""/framework/store/user/registration/steps/basicInfo/view.html"",
			continueTextKey: ""{RegistrationPanesBasicInfoContinueText}""
		}}," : string.Empty)}{(RegistrationPanesAddressBookEnabled ? $@"
		registrationStepAddressBook: {{
			name: ""registrationStepAddressBook"",
			titleKey: ""{RegistrationPanesAddressBookTitle}"",
			show: true,
			showButton: {RegistrationPanesAddressBookShowButton.ToString().ToLower()},
			order: {RegistrationPanesAddressBookPosition},
			templateURL: ""/framework/store/user/registration/steps/addressBook/view.html"",
			continueTextKey: ""{RegistrationPanesAddressBookContinueText}""
		}}," : string.Empty)}{(RegistrationPanesWalletEnabled ? $@"
		registrationStepWallet: {{
			name: ""registrationStepWallet"",
			titleKey: ""{RegistrationPanesWalletTitle}"",
			show: true,
			showButton: {RegistrationPanesWalletShowButton.ToString().ToLower()},
			order: {RegistrationPanesWalletPosition},
			templateURL: ""/framework/store/user/registration/steps/Wallet/view.html"",
			continueTextKey: ""{RegistrationPanesWalletContinueText}""
		}}," : string.Empty)}{(RegistrationPanesCustomEnabled ? $@"
		registrationStepCustom: {{
			name: ""registrationStepCustom"",
			titleKey: ""{RegistrationPanesCustomTitle}"",
			show: true,
			showButton: {RegistrationPanesCustomShowButton.ToString().ToLower()},
			order: {RegistrationPanesCustomPosition},
			templateURL: ""/framework/store/user/registration/steps/custom/view.html"",
			continueTextKey: ""{RegistrationPanesCustomContinueText}""
		}}," : string.Empty)}{(RegistrationPanesConfirmationEnabled ? $@"
		registrationStepConfirmation: {{
			name: ""registrationStepConfirmation"",
			titleKey: ""{RegistrationPanesConfirmationTitle}"",
			show: true,
			showButton: {RegistrationPanesConfirmationShowButton.ToString().ToLower()},
			order: {RegistrationPanesConfirmationPosition},
			templateURL: ""/framework/store/user/registration/steps/confirmation/view.html"",
			continueTextKey: ""{RegistrationPanesConfirmationContinueText}""
		}}," : string.Empty)}{(RegistrationPanesCompletedEnabled ? $@"
		registrationStepCompleted: {{
			name: ""registrationStepCompleted"",
			titleKey: ""{RegistrationPanesCompletedTitle}"",
			show: true,
			showButton: {RegistrationPanesCompletedShowButton.ToString().ToLower()},
			order: {RegistrationPanesCompletedPosition},
			templateURL: ""/framework/store/user/registration/steps/completed/view.html"",
			continueTextKey: ""{RegistrationPanesCompletedContinueText}""
		}}" : string.Empty)}
	}},
}},";
		}

		private static string GetPersonalDetailsBlock()
		{
			return
$@"personalDetailsDisplay: {{
	hideAddressBookKeys: {AddressBookHideKeys.ToString().ToLower()},
	hideAddressBookFirstName: {AddressBookHideFirstName.ToString().ToLower()},
	hideAddressBookLastName: {AddressBookHideLastName.ToString().ToLower()},
	hideAddressBookEmail: {AddressBookHideEmail.ToString().ToLower()},
	hideAddressBookPhone: {AddressBookHidePhone.ToString().ToLower()},
	hideAddressBookFax: {AddressBookHideFax.ToString().ToLower()},
	hidePurchaseBillingFirstName: {PurchaseHideBillingFirstName.ToString().ToLower()},
	hidePurchaseBillingLastName: {PurchaseHideBillingLastName.ToString().ToLower()},
	hidePurchaseBillingEmail: {PurchaseHideBillingEmail.ToString().ToLower()},
	hidePurchaseBillingPhone: {PurchaseHideBillingPhone.ToString().ToLower()},
	hidePurchaseBillingFax: {PurchaseHideBillingFax.ToString().ToLower()},
	hidePurchaseShippingFirstName: {PurchaseHideShippingFirstName.ToString().ToLower()},
	hidePurchaseShippingLastName: {PurchaseHideShippingLastName.ToString().ToLower()},
	hidePurchaseShippingEmail: {PurchaseHideShippingEmail.ToString().ToLower()},
	hidePurchaseShippingPhone: {PurchaseHideShippingPhone.ToString().ToLower()},
	hidePurchaseShippingFax: {PurchaseHideShippingFax.ToString().ToLower()},
	hideSubmitQuoteBillingFirstName: {SubmitQuoteHideBillingFirstName.ToString().ToLower()},
	hideSubmitQuoteBillingLastName: {SubmitQuoteHideBillingLastName.ToString().ToLower()},
	hideSubmitQuoteBillingEmail: {SubmitQuoteHideBillingEmail.ToString().ToLower()},
	hideSubmitQuoteBillingPhone: {SubmitQuoteHideBillingPhone.ToString().ToLower()},
	hideSubmitQuoteBillingFax: {SubmitQuoteHideBillingFax.ToString().ToLower()},
	hideSubmitQuoteShippingFirstName: {SubmitQuoteHideShippingFirstName.ToString().ToLower()},
	hideSubmitQuoteShippingLastName: {SubmitQuoteHideShippingLastName.ToString().ToLower()},
	hideSubmitQuoteShippingEmail: {SubmitQuoteHideShippingEmail.ToString().ToLower()},
	hideSubmitQuoteShippingPhone: {SubmitQuoteHideShippingPhone.ToString().ToLower()},
	hideSubmitQuoteShippingFax: {SubmitQuoteHideShippingFax.ToString().ToLower()},
}},";
		}

		private static string GetStoredFilesImagesAndImports()
		{
			return
$@"storedFiles: {{
	suffix: ""{StoredFilesPathSuffix}"",
	accounts: ""{StoredFilesPathAccounts}"",
	calendarEvents: ""{StoredFilesPathCalendarEvents}"",
	carts: ""{StoredFilesPathCarts}"",
	categories: ""{StoredFilesPathCategories}"",
	emailQueueAttachments: ""{StoredFilesPathEmailQueueAttachments}"",
	messageAttachments: ""{StoredFilesPathMessageAttachments}"",
	products: ""{StoredFilesPathProducts}"",
	purchaseOrders: ""{StoredFilesPathPurchaseOrders}"",
	salesInvoices: ""{StoredFilesPathSalesInvoices}"",
	salesOrders: ""{StoredFilesPathSalesOrders}"",
	salesQuotes: ""{StoredFilesPathSalesQuotes}"",
	salesReturns: ""{StoredFilesPathSalesReturns}"",
	sampleRequests: ""{StoredFilesPathSampleRequests}"",
	users: ""{StoredFilesPathUsers}""
}},
images: {{
	suffix: ""{ImagesPathSuffix}"",
	accounts: ""{ImagesPathAccounts}"",
	ads: ""{ImagesPathAds}"",
	brands: ""{ImagesPathBrands}"",
	calendarEvents: ""{ImagesPathCalendarEvents}"",
	categories: ""{ImagesPathCategories}"",
	countries: ""{ImagesPathCountries}"",
	currencies: ""{ImagesPathCurrencies}"",
	languages: ""{ImagesPathLanguages}"",
	manufacturers: ""{ImagesPathManufacturers}"",
	products: ""{ImagesPathProducts}"",
	regions: ""{ImagesPathRegions}"",
	stores: ""{ImagesPathStores}"",
	users: ""{ImagesPathUsers}"",
	vendors: ""{ImagesPathVendors}""
}},
imports: {{
	suffix: ""{ImportsPathSuffix}"",
	excels: ""{ImportsPathExcels}"",
	products: ""{ImportsPathProducts}"",
	productPricePoints: ""{ImportsPathProductPricePoints}"",
	salesQuotes: ""{ImportsPathSalesQuotes}"",
	users: ""{ImportsPathUsers}""
}}";
		}

		private static string GetCatalog()
		{
			return $@"catalog: {{
	showCategoriesForLevelsUpTo: {CatalogShowProductCategoriesForLevelsUpToX},
	defaultFormat: ""{CatalogDefaultFormat}"",
	defaultPageSize: {CatalogDefaultPageSize},
	defaultSort: ""{CatalogDefaultSort}"",
	onlyApplyStoreToFilterByUI: {CatalogOnlyApplyStoreToFilterByUI.ToString().ToLower()},
	displayImages: {CatalogDisplayImages.ToString().ToLower()},
	getFullAssocs: {CatalogGetFullAssociatedProductsInfo.ToString().ToLower()},
	maxTopLevelCategoriesInFilter: {MaxTopLevelCategoriesInCatalogFilter}
}},";
		}

		private static string GetGoogleAPI()
		{
			return $@"google: {{
	maps: {{
		apiKey: ""{GoogleMapsAPIKey}""
	}},
	apiKey: ""{GoogleAPIKey}"",
	apiClientKey: ""{GoogleAPIClientKey}""
}},";
		}

		private static string GetCookies()
		{
			return $@"useSubDomainForCookies: {CookiesUseSubDomain.ToString().ToLower()},
usePartialSubDomainForCookiesRootSegmentCount: {CookiesUseSubDomainSegmentCount},
requireSecureForCookies: {CookiesRequireSecure.ToString().ToLower()},";
		}

		private static string GetCORSResourceWhiteList()
		{
			return $@"corsResourceWhiteList: [{(CORSResourceWhiteList.Any()
				? CORSResourceWhiteList.Aggregate(string.Empty, (c, n) => $"{c}\r\n\t\"{n}\",").TrimEnd(',') + "\r\n"
				: string.Empty)}],";
		}
	}
}
