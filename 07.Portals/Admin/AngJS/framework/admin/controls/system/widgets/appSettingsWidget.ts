/**
 * @auto-generated
 * @file framework/admin/controls/system/appSettingsWidget.ts
 * @author Copyright(c) 2021-2022 clarity-ventures.com. All rights reserved.
 * @desc The AppSettings keys, values and descriptions and the ability to change them.
 *       NOTE: This file is generated via the appSettingsWidget.tt T4 in the UI Project
 */
module cef.admin {
	interface ICustomAttribute {
		DisplayName?: string;
		ConstructorArguments?: string[];
	}

	interface IParamDocs {
		Summary?: string,
		Remarks?: string,
		Example?: string,
	}

	interface IAppSettingDefinition {
		Index: number;
		Name?: string;
		ParamDocs?: IParamDocs;
		PropertyType?: string;
		CustomAttributes?: ICustomAttribute[],
	}

	class AppSettingsWidgetController extends core.TemplatedControllerBase {
		// Bound Scope Properties
		// <None>
		// Properties
		static readonly timeSpanParser = new RegExp("(?<negative>-)?[Pp]((?<days>\d*)[Dd])?[Tt]((?<hours>\d*)[Hh])?((?<min>\d*)[Mm])?((?<sec>\d*)[Ss])?");
		appSettingsDefinitions: IAppSettingDefinition[] = [];
		appSettingsValues: cefalt.admin.Dictionary<cefalt.admin.Dictionary<any>> = null;
		appSettingsValuesOriginals: cefalt.admin.Dictionary<cefalt.admin.Dictionary<any>> = null;
		toUpdates: cefalt.admin.Dictionary<cefalt.admin.Dictionary<any>> = { };
		pageSize = 8;
		page = 0;
		pages: number[] = [];
		get enableSave(): boolean { return this.toUpdates && Object.keys(this.toUpdates).length > 0; }
		// Functions
		convertTimeSpan(timeSpan: string): string {
			const matches = AppSettingsWidgetController.timeSpanParser.exec(timeSpan);
			const negative = matches["groups"]["negative"] === "-";
			const days = Number(matches["groups"]["days"]) || 0;
			const hours = Number(matches["groups"]["hours"]) || 0;
			const minutes = Number(matches["groups"]["mins"]) || 0;
			const seconds = Number(matches["groups"]["sec"]) || 0;
			return `${(negative ? "-" : "")}${days}:${hours}:${minutes}:${seconds}`;
		}
		cleanPropertyType(type: string): string {
			return type
				.replace('System.Nullable\`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]', 'HostLookupWhichUrl')
				.replace('System.Nullable\`1[System.Int32]', 'Int32?')
				.replace('System.Nullable\`1[System.Decimal]', 'Decimal?')
				.replace('System.', '').replace('Clarity.Ecommerce.Enums.', '');
		}
		currentValue(type: string, name: string): any {
			return this.appSettingsValues[type][name];
		}
		pushToUpdates(type: string, name: string, value: any): void {
			if (!this.toUpdates) {
				this.toUpdates = { };
			}
			if (!this.toUpdates[type]) {
				this.toUpdates[type] = { };
			}
			if (angular.isDefined(this.toUpdates[type][name])
				&& this.toUpdates[type][name] == value) {
				// Do Nothing
				return;
			}
			if (angular.isDefined(this.toUpdates[type][name])
				&& this.appSettingsValuesOriginals[type][name] == value) {
				// Matches original, don't try to update (remove from the update dictionary)
				delete this.toUpdates[type][name];
				return;
			}
			// New value, push to the update dictionary
			this.toUpdates[type][name] = value;
		}
		load(): void {
			this.setRunning();
			this.cvApi.jsConfigs.GetAppSettings().then(r => {
				if (!r || !r.data || !r.data.ActionSucceeded) {
					this.finishRunning(true, null, r && r.data && r.data.Messages);
					return;
				}
				Object.keys(r.data.Result).forEach(type => {
					Object.keys(r.data.Result[type]).forEach(name => {
						const found = _.find(this.appSettingsDefinitions, x => x.Name === name);
						if (found && found.PropertyType === "System.TimeSpan") {
							r.data.Result[type][name] = this.convertTimeSpan(r.data.Result[type][name]);
						}
					});
				});
				this.appSettingsValues = r.data.Result;
				this.appSettingsValuesOriginals = r.data.Result;
				const pages = [];
				let settingsCount = 0;
				Object.keys(this.appSettingsValues).forEach(type => {
					settingsCount += Object.keys(this.appSettingsValues[type]).length;
				});
				const pageCount = Math.ceil(settingsCount / (this.pageSize || 8));
				for (let i = 0; i < pageCount; i++) {
					pages.push(i);
				}
				this.pages = pages;
				this.finishRunning();
			}).catch(reason => this.finishRunning(true, reason));
		}
		save(): void {
			if (!this.enableSave) {
				return;
			}
			this.setRunning();
			this.cvApi.jsConfigs.UpdateAppSettings({ KeysToUpdate: this.toUpdates }).then(r => {
				if (!r || !r.data || !r.data.ActionSucceeded) {
					this.finishRunning(true, null, r && r.data && r.data.Messages);
					return;
				}
				this.load();
			}).catch(reason => this.finishRunning(true, reason));
		}
		// Events
		// <None>
		// Constructor
		constructor(
				protected readonly cefConfig: core.CefConfig,
				private readonly cvApi: api.ICEFAPI) {
			super(cefConfig);
			this.setRunning();
			var index = 0;
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseAddAddressesToBook",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the checkout add addresses to book.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.AddAddressesToBook",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PhonePrefixLookupsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the phone prefix lookups is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.PhonePrefixLookups.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MiniCartEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the mini cart is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Checkout.MiniCart.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SplitShippingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the split shipping is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Split.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SplitShippingOnlyAllowOneDestination",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the split shipping only allow one destination.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Split.OnlyAllowOneDestination",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SplitShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RestrictedShippingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the restricted shipping is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Restricted.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingShipToStoreEnabled",
ParamDocs: <IParamDocs>{
Summary: "Ship to Store: Buy products online from another store and have them moved to your local store.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.ShipToStore.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEstimatesEnabled\", \"StoresEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingInStorePickupEnabled",
ParamDocs: <IParamDocs>{
Summary: "In Store Pickup: Buy products online and get them from the store's service desk.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.InStorePickup.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEstimatesEnabled\", \"StoresEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "BillingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the billing is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Billing.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CheckoutSalesOrderDefaultTypeKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the checkout sales order default type key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Ordering.CheckoutDefaultOrderTypeKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Web",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesOrdersEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByACHEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by ACH is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByACH.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByACHTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by ACH title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByACH.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.ACH",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByACHEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByACHPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by ACH position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByACH.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)50",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByACHEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByACHUpliftUseWhicheverIsGreater",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by ACH uplift use whichever is greater (if not, will combine both).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByACH.Uplifts.UseGreater",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByACHUpliftPercent\", \"PaymentsByACHUpliftAmount\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByACHRestrictedAccountTypes",
ParamDocs: <IParamDocs>{
Summary: "A list of account types restricted from using PaymentsByACH.",
Example: "CUSTOMER,ORGANIZATION.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByACH.RestrictedAccountTypes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByACHEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByACHUpliftPercent",
ParamDocs: <IParamDocs>{
Summary: "A percentage uplift.",
Example: "0.03 = 3% increase, -0.03 = 3% decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByACH.Uplifts.Percent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByACHEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByACHUpliftAmount",
ParamDocs: <IParamDocs>{
Summary: "An amount uplift.",
Example: "5.00 = $5.00 increase, -5.00 = $5.00 decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByACH.Uplifts.Amount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByACHEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByCreditCardEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by credit card is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByCreditCard.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByCreditCardTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by credit card title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByCreditCard.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.CreditCard",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByCreditCardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByCreditCardPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by credit card position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByCreditCard.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByCreditCardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByCreditCardUpliftUseWhicheverIsGreater",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by credit card uplift use whichever is greater (if not, will combine both).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByCreditCard.Uplifts.UseGreater",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByCreditCardUpliftPercent\", \"PaymentsByCreditCardUpliftAmount\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByCreditCardRestrictedAccountTypes",
ParamDocs: <IParamDocs>{
Summary: "A list of account types restricted from using PaymentsByCreditCard.",
Example: "CUSTOMER,ORGANIZATION .",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByCreditCard.RestrictedAccountTypes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByCreditCardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByCreditCardUpliftPercent",
ParamDocs: <IParamDocs>{
Summary: "A percentage uplift.",
Example: "0.03 = 3% increase, -0.03 = 3% decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByCreditCard.Uplifts.Percent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByCreditCardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByCreditCardUpliftAmount",
ParamDocs: <IParamDocs>{
Summary: "An amount uplift.",
Example: "5.00 = $5.00 increase, -5.00 = $5.00 decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByCreditCard.Uplifts.Amount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByCreditCardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByEcheckEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by echeck is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByEcheck.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByEcheckTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by echeck title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByEcheck.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.Echeck",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByEcheckEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByEcheckPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by echeck position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByEcheck.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)20",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByEcheckEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByECheckUpliftUseWhicheverIsGreater",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by eCheck uplift use whichever is greater (if not, will combine both).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByEcheck.Uplifts.UseGreater",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByECheckUpliftPercent\", \"PaymentsByECheckUpliftAmount\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByECheckRestrictedAccountTypes",
ParamDocs: <IParamDocs>{
Summary: "A list of account types restricted from using PaymentsByECheck.",
Example: "CUSTOMER,ORGANIZATION.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByECheck.RestrictedAccountTypes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByEcheckEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByECheckUpliftPercent",
ParamDocs: <IParamDocs>{
Summary: "A percentage uplift.",
Example: "0.03 = 3% increase, -0.03 = 3% decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByEcheck.Uplifts.Percent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByEcheckEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByECheckUpliftAmount",
ParamDocs: <IParamDocs>{
Summary: "An amount uplift.",
Example: "5.00 = $5.00 increase, -5.00 = $5.00 decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByEcheck.Uplifts.Amount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByEcheckEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByFreeEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by free is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByFree.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByFreeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByFreeTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by free title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByFree.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.Free",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByFreeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByFreePosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by free position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByFree.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)60",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByFreeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayPalEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by pay palette is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayPal.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayPalTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by pay palette title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayPal.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.PayPal",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayPalEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayPalPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by pay palette position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayPal.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)100",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayPalEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayPalUpliftUseWhicheverIsGreater",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by PayPal uplift use whichever is greater (if not, will combine both).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayPal.Uplifts.UseGreater",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayPalUpliftPercent\", \"PaymentsByPayPalUpliftAmount\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayPalRestrictedAccountTypes",
ParamDocs: <IParamDocs>{
Summary: "A list of account types restricted from using PaymentsByPayPal.",
Example: "CUSTOMER,ORGANIZATION.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayPal.RestrictedAccountTypes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayPalEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayPalUpliftPercent",
ParamDocs: <IParamDocs>{
Summary: "A percentage uplift.",
Example: "0.03 = 3% increase, -0.03 = 3% decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayPal.Uplifts.Percent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayPalEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayPalUpliftAmount",
ParamDocs: <IParamDocs>{
Summary: "An amount uplift.",
Example: "5.00 = $5.00 increase, -5.00 = $5.00 decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayPal.Uplifts.Amount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayPalEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayoneerEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by payoneer is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayoneer.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayoneerTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by payoneer title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayoneer.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.Payoneer",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayoneerEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayoneerPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by payoneer position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayoneer.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)100",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayoneerEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayoneerUpliftUseWhicheverIsGreater",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by Payoneer uplift use whichever is greater (if not, will combine both).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayoneer.Uplifts.UseGreater",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayoneerUpliftPercent\", \"PaymentsByPayoneerUpliftAmount\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayoneerRestrictedAccountTypes",
ParamDocs: <IParamDocs>{
Summary: "A list of account types restricted from using PaymentsByPayoneer.",
Example: "CUSTOMER,ORGANIZATION.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayoneer.RestrictedAccountTypes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayoneerEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayoneerUpliftPercent",
ParamDocs: <IParamDocs>{
Summary: "A percentage uplift.",
Example: "0.03 = 3% increase, -0.03 = 3% decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayoneer.Uplifts.Percent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayoneerEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByPayoneerUpliftAmount",
ParamDocs: <IParamDocs>{
Summary: "An amount uplift.",
Example: "5.00 = $5.00 increase, -5.00 = $5.00 decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByPayoneer.Uplifts.Amount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByPayoneerEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByStoreCreditEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by store credit is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByStoreCredit.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByStoreCreditTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by store credit title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByStoreCredit.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.StoreCredit",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByStoreCreditEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByStoreCreditPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by store credit position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByStoreCredit.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)70",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByStoreCreditEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByStoreCreditUpliftUseWhicheverIsGreater",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by StoreCredit uplift use whichever is greater (if not, will combine both).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByStoreCredit.Uplifts.UseGreater",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByStoreCreditUpliftPercent\", \"PaymentsByStoreCreditUpliftAmount\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByStoreCreditRestrictedAccountTypes",
ParamDocs: <IParamDocs>{
Summary: "A list of account types restricted from using PaymentsByStoreCredit.",
Example: "CUSTOMER,ORGANIZATION.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByStoreCredit.RestrictedAccountTypes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByStoreCreditEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByStoreCreditUpliftPercent",
ParamDocs: <IParamDocs>{
Summary: "A percentage uplift.",
Example: "0.03 = 3% increase, -0.03 = 3% decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByStoreCredit.Uplifts.Percent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByStoreCreditEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByStoreCreditUpliftAmount",
ParamDocs: <IParamDocs>{
Summary: "An amount uplift.",
Example: "5.00 = $5.00 increase, -5.00 = $5.00 decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByStoreCredit.Uplifts.Amount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByStoreCreditEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByWireTransferEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by wire transfer is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByWireTransfer.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByWireTransferTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by wire transfer title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByWireTransfer.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.WireTransfer",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByWireTransferEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByWireTransferPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by wire transfer position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByWireTransfer.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)40",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByWireTransferEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByWireTransferUpliftUseWhicheverIsGreater",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by WireTransfer uplift use whichever is greater (if not, will combine both).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByWireTransfer.Uplifts.UseGreater",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByWireTransferUpliftPercent\", \"PaymentsByWireTransferUpliftAmount\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByWireTransferRestrictedAccountTypes",
ParamDocs: <IParamDocs>{
Summary: "A list of account types restricted from using PaymentsByWireTransfer.",
Example: "CUSTOMER,ORGANIZATION.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByWireTransfer.RestrictedAccountTypes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByWireTransferEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByWireTransferUpliftPercent",
ParamDocs: <IParamDocs>{
Summary: "A percentage uplift.",
Example: "0.03 = 3% increase, -0.03 = 3% decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByWireTransfer.Uplifts.Percent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByWireTransferEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByWireTransferUpliftAmount",
ParamDocs: <IParamDocs>{
Summary: "An amount uplift.",
Example: "5.00 = $5.00 increase, -5.00 = $5.00 decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByWireTransfer.Uplifts.Amount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByWireTransferEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByCustom",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by custom.",
},
PropertyType: "System.String[]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByCustom",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"",
]
},
<ICustomAttribute>{
DisplayName: "Split On",
ConstructorArguments: [
"','",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesMethodEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes method is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Method.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesMethodShow",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes method show.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Method.Show",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesMethodEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesMethodPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes method position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Method.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesMethodEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesMethodTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes method title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Method.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.checkoutPanels.CheckoutMethod",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesMethodEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesMethodContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes method continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Method.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesMethodEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesMethodShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes method show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Method.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesMethodEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesBillingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes billing is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Billing.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"BillingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesBillingShow",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes billing show.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Billing.Show",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesBillingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesBillingPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes billing position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Billing.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)1",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesBillingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesBillingTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes billing title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Billing.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.checkoutPanels.billingAndContactInformation",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesBillingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesBillingContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes billing continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Billing.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.cart.continueToBilling",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesBillingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesBillingShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes billing show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Billing.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesBillingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesShippingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes shipping is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Shipping.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesShippingShow",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes shipping show.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Shipping.Show",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesShippingPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes shipping position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Shipping.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)2",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesShippingTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes shipping title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Shipping.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.ShippingInformation",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesShippingContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes shipping continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Shipping.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.views.accountInformation.continueToShipping",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesShippingShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes shipping show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Shipping.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesPaymentsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes payments is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Payments.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesPaymentsShow",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes payments show.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Payments.Show",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesPaymentsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesPaymentsPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes payments position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Payments.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)3",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsEnabled\", \"PurchasePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesPaymentsTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes payments title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Payments.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.views.paymentInformation.selectAPaymentMethod",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesPaymentsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesPaymentsContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes payments continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Payments.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.views.shippingInformation.continueToPayment",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesPaymentsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesPaymentsShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes payments show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Payments.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesPaymentsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesConfirmationEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes confirmation is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Confirmation.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesConfirmationShow",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes confirmation show.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Confirmation.Show",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesConfirmationPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes confirmation position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Confirmation.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)4",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesConfirmationTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes confirmation title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Confirmation.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.checkoutPanels.orderConfirmation",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesConfirmationContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes confirmation continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Confirmation.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.views.paymentInformation.confirmOrderAndPurchase",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesConfirmationShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes confirmation show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Confirmation.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesCompletedEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes completed is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Completed.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesCompletedShow",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes completed show.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Completed.Show",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesCompletedPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes completed position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Completed.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)5",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesCompletedTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes completed title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Completed.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.checkoutPanels.Complete",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesCompletedContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase panes completed continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Completed.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.views.paymentInformation.confirmOrderAndPurchase",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasePanesCompletedShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase panes completed show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Panes.Completed.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasePanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseUsePreferredPaymentMethodForLaterPayments",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase use preferred payment method for later payments.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.UsePreferredPaymentMethodForLaterPayments",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseAllowAccountOnHoldOrders",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase allow account on hold orders.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.AllowAccountOnHoldOrders",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseRequireShipOption",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase require ship option.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.RequireShipOption",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseOverrideAndForceNoShipToOptionIfWhenShipOptionSelected",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase override and force no ship to option if when ship\
option selected.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.OverrideAndForceNoShipToOptionIfWhenShipOptionSelected",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Mutually Exclusive With",
ConstructorArguments: [
"\"PurchaseRequireShipOption\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseOverrideAndForceShipToHomeOptionIfNoShipOptionSelected",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase override and force ship to home option if no ship\
option selected.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.OverrideAndForceShipToHomeOptionIfNoShipOptionSelected",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Mutually Exclusive With",
ConstructorArguments: [
"\"PurchaseOverrideAndForceNoShipToOptionIfWhenShipOptionSelected\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseUseRecentlyUsedAddresses",
ParamDocs: <IParamDocs>{
Summary: "When populating the UI with Billing and Shipping details, re-use the last order's information when\
possible Note: Only possible when using Single and Split (not Targets) checkouts.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.UseRecentlyUsedAddresses",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"new String[3] { \"LoginEnabled\", \"PurchasePanesBillingEnabled\", \"PurchasePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseCreateAccountEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase create account is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.CreateAccount.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseGuestCreateAccountStartingValue",
ParamDocs: <IParamDocs>{
Summary: "Set the state of create account checkbox default.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.CreateAccount.StartingValue",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"PurchaseCreateAccountEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseEnterStepByClickEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase enter step by click is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.EnterStepByClick.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseSpecialInstructionsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase special instructions is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.SpecialInstructions.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseFinalActionButtonText",
ParamDocs: <IParamDocs>{
Summary: "Gets the purchase final action button text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.FinalActionButtonText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.cart.continueShopping",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseDefaultCartType",
ParamDocs: <IParamDocs>{
Summary: "Gets the type of the purchase default cart.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.DefaultCartType",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Cart",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "GuestPurchaseEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the guest purchase is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Guest.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Mutually Exclusive With",
ConstructorArguments: [
"\"SplitShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseMode",
ParamDocs: <IParamDocs>{
Summary: "Which endpoint to call when completing checkout.",
Example: "Single = 0, Targets = 2.",
},
PropertyType: "Clarity.Ecommerce.Enums.CheckoutModes",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Mode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.CheckoutModes)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideBillingFirstName",
ParamDocs: <IParamDocs>{
Summary: "Hide billing first name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Billing.HideFirstName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideBillingLastName",
ParamDocs: <IParamDocs>{
Summary: "Hide billing first name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Billing.HideLastName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideBillingEmail",
ParamDocs: <IParamDocs>{
Summary: "Hide billing last name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Billing.HideEmail",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideBillingPhone",
ParamDocs: <IParamDocs>{
Summary: "Hide billing phone.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Billing.HidePhone",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideBillingFax",
ParamDocs: <IParamDocs>{
Summary: "Hide billing fax.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Billing.HideFax",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseBillingShowMakeThisMyDefault",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase billing show make this my default.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Billing.ShowMakeThisMyDefault",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookAllowMakeThisMyNewDefaultBillingInCheckout\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideShippingFirstName",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping first name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Shipping.HideFirstName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideShippingLastName",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping last name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Shipping.HideLastName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideShippingEmail",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping email.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Shipping.HideEmail",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideShippingPhone",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping phone.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Shipping.HidePhone",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseHideShippingFax",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping fax.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Shipping.HideFax",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseShippingShowMakeThisMyDefault",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase shipping show make this my default.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Purchase.Inputs.Shipping.ShowMakeThisMyDefault",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookAllowMakeThisMyNewDefaultShippingInCheckout\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesInternalLocalPath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the stored files internal local file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SEOSiteMapsRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the seo site maps relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.SEO.SiteMaps",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathSuffix",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path suffix.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.Suffix",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Files",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathAccounts",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path accounts.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.Account",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Account",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathCalendarEvents",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path calendar events.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.CalendarEvent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/CalendarEvent",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathCarts",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path carts.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.Cart",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Cart",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathCategories",
ParamDocs: <IParamDocs>{
Summary: "Gets the categories the stored files path belongs to.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.Category",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Category",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathEmailQueueAttachments",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path email queue attachments.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.EmailQueueAttachment",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/EmailQueueAttachment",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathMessageAttachments",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path message attachments.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.MessageAttachment",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/MessageAttachment",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathProducts",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path products.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.Product",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Product",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathPurchaseOrders",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path purchase orders.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.PurchaseOrder",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/PurchaseOrder",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathSalesOrders",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path sales orders.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.SalesOrder",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/SalesOrder",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathSampleRequests",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path sample requests.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.SampleRequest",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/SampleRequest",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathUsers",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path users.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.User",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/User",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsPathSuffix",
ParamDocs: <IParamDocs>{
Summary: "Gets the imports path suffix.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Imports.Suffix",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Imports/",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsPathExcels",
ParamDocs: <IParamDocs>{
Summary: "Gets the imports path excels.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Excel",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Excel",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsPathProducts",
ParamDocs: <IParamDocs>{
Summary: "Gets the imports path products.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Product",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Product",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsPathProductPricePoints",
ParamDocs: <IParamDocs>{
Summary: "Gets the imports path product price points.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.ProductPricePoint",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/ProductPricePoint",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingTieredPricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsPathUsers",
ParamDocs: <IParamDocs>{
Summary: "Gets the imports path users.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.User",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/User",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportProductsProductCategoriesAllowResolveByName",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the products product categories allow resolve by name should be\
imported.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Importing.ProductCategories.AllowResolveByName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CategoriesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportProductsProductCategoriesAllowResolveBySeoUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the products product categories allow resolve by seo URL should be\
imported.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Importing.ProductCategories.AllowResolveBySeoUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CategoriesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportProductsAllowSaveBrandProductsWithBrand",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the products allow save brand products with brand should be\
imported.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Importing.BrandProducts.AllowSaveWithBrand",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"BrandsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportProductsAllowSaveFranchiseProductsWithFranchise",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the products allow save franchise products with franchise should be\
imported.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Importing.FranchiseProducts.AllowSaveWithFranchise",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"FranchisesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportProductsAllowSaveStoreProductsWithStore",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the products allow save store products with store should be\
imported.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Importing.StoreProducts.AllowSaveWithStore",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"StoresEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportUsersAlternateCustomKeyColumnName",
ParamDocs: <IParamDocs>{
Summary: "Gets the name of the import users alternate custom key column.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Importing.Users.AlternateCustomKeyColumnName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathSuffix",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path suffix.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Suffix",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Images/",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathAccounts",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path accounts.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Account",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Account",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathAds",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path ads.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Ad",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Ad",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AdsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathBrands",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path brands.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Brand",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Brand",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"BrandsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathFranchises",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path brands.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Franchise",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Franchise",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"FranchisesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathCalendarEvents",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path calendar events.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.CalendarEvent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/CalendarEvent",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CalendarEventsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathCategories",
ParamDocs: <IParamDocs>{
Summary: "Gets the categories the images path belongs to.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Category",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Category",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CategoriesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathCountries",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path countries.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Country",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Country",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathCurrencies",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path currencies.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Currency",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Currency",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathLanguages",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path languages.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Language",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Language",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathManufacturers",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path manufacturers.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Manufacturer",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Manufacturer",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ManufacturersEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathProducts",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path products.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Product",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Product",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathRegions",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path regions.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Region",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Region",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathStores",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path stores.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Store",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Store",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"StoresEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathUsers",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path users.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.User",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/User",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesPathVendors",
ParamDocs: <IParamDocs>{
Summary: "Gets the images path vendors.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.Vendor",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Vendor",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"VendorsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIStorefrontRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the API storefront route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-Storefront.Requests.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIStorefrontRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the API storefront route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-Storefront.Requests.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/API-Storefront",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIStoreAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the API store admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-StoreAdmin.Requests.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIStoreAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the API store admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-StoreAdmin.Requests.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/API-StoreAdmin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIBrandAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the API brand admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-BrandAdmin.Requests.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIBrandAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the API brand admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-BrandAdmin.Requests.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/API-BrandAdmin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIFranchiseAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the API franchise admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-FranchiseAdmin.Requests.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIFranchiseAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the API franchise admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-FranchiseAdmin.Requests.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/API-FranchiseAdmin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIManufacturerAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the API manufacturer admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-ManufacturerAdmin.Requests.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIManufacturerAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the API manufacturer admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-ManufacturerAdmin.Requests.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/API-ManufacturerAdmin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIVendorAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the API vendor admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-VendorAdmin.Requests.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIVendorAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the API vendor admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-VendorAdmin.Requests.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/API-VendorAdmin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the API admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-Admin.Requests.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the API admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API-Admin.Requests.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/API-Admin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SiteRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the site route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Site.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SiteRouteHostUrlSSL",
ParamDocs: <IParamDocs>{
Summary: "Gets the site route host URL ssl.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Site.RootUrlSSL",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SiteRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the site route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Site.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SiteRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the site route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Site.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SiteRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the site route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Site.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UI.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UI.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/UI-Storefront",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UI.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UI.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UI-Admin.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UI-Admin.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/UI-Admin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIAdminRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UI-Admin.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIAdminRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UI-Admin.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UITemplateOverrideRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the template override route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UITemplateOverride.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UITemplateOverrideRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the template override route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UITemplateOverride.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Portals/_default/Skins/Clarity/Ecommerce/framework",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UITemplateOverrideRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the template override route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UITemplateOverride.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UITemplateOverrideRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the template override route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UITemplateOverride.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIAdminTemplateOverrideRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the template override route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UIAdminTemplateOverride.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIAdminTemplateOverrideRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the template override route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UIAdminTemplateOverride.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Portals/_default/Skins/Clarity/Ecommerce/framework",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIAdminTemplateOverrideRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the template override route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UIAdminTemplateOverride.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UIAdminTemplateOverrideRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the template override route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.UIAdminTemplateOverride.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyStoreAdminEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether my store admin is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Stores.MyStoreAdmin.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"StoresEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyStoreAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my store admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyStoreAdmin.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyStoreAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyStoreAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of my store admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyStoreAdmin.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/My-Store",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyStoreAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyStoreAdminRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets my store admin route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyStoreAdmin.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyStoreAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyStoreAdminRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my store admin route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyStoreAdmin.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyStoreAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyBrandAdminEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether my brand admin is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Brands.MyBrandAdmin.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyBrandAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my brand admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyBrandAdmin.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyBrandAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyBrandAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of my brand admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyBrandAdmin.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/My-Brand",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyBrandAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyBrandAdminRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets my brand admin route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyBrandAdmin.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyBrandAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyBrandAdminRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my brand admin route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyBrandAdmin.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyBrandAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyFranchiseAdminEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether my franchise admin is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Franchises.MyFranchiseAdmin.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyFranchiseAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my franchise admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyFranchiseAdmin.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyFranchiseAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyFranchiseAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of my franchise admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyFranchiseAdmin.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/My-Franchise",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyFranchiseAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyFranchiseAdminRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets my franchise admin route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyFranchiseAdmin.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyFranchiseAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyFranchiseAdminRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my franchise admin route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyFranchiseAdmin.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyFranchiseAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyManufacturerAdminEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether my manufacturer admin is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Manufacturers.MyManufacturerAdmin.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyManufacturerAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my manufacturer admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyManufacturerAdmin.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyManufacturerAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyManufacturerAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of my manufacturer admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyManufacturerAdmin.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/My-Manufacturer",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyManufacturerAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyManufacturerAdminRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets my manufacturer admin route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyManufacturerAdmin.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyManufacturerAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyManufacturerAdminRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my manufacturer admin route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyManufacturerAdmin.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyManufacturerAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyVendorAdminEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether my vendor admin is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Vendors.MyVendorAdmin.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"VendorsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyVendorAdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my vendor admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyVendorAdmin.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyVendorAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyVendorAdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of my vendor admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyVendorAdmin.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyVendorAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyVendorAdminRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets my vendor admin route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyVendorAdmin.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyVendorAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyVendorAdminRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of my vendor admin route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MyVendorAdmin.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyVendorAdminEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ConnectLiveRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the connect live route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ConnectLive.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ConnectLiveRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the connect live route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ConnectLive.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Connect-Live",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ConnectLiveRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the connect live route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ConnectLive.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ConnectLiveRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the connect live route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ConnectLive.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AdminRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the admin route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Admin.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AdminRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the admin route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Admin.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Admin/Clarity-Ecommerce-Admin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AdminRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the admin route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Admin.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AdminRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the admin route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Admin.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CheckoutRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "The Relative URL to the Checkout page Do not leave a trailing slash.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Checkout.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CheckoutRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the checkout route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Checkout.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Checkout",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CheckoutRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the checkout route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Checkout.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CheckoutRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the checkout route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Checkout.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "The Relative URL to the root of the Catalog.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Catalog.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the catalog route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Catalog.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Catalog",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the catalog route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Catalog.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the catalog route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Catalog.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProductDetailRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the product detail route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ProductDetail.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProductDetailRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the product detail route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ProductDetail.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Product",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProductDetailRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the product detail route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ProductDetail.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProductDetailRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the product detail route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ProductDetail.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoreDetailRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the store detail route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoreDetail.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoreDetailRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the store detail route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoreDetail.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Store",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoreDetailRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the store detail route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoreDetail.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoreDetailRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the store detail route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoreDetail.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoreLocatorRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the store locator route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoreLocator.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoreLocatorRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the store locator route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoreLocator.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Store-Locator",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoreLocatorRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the store locator route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoreLocator.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoreLocatorRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the store locator route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoreLocator.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CategoryRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the category route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.CategoryDetail.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CategoryRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the category route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.CategoryDetail.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Categories",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CategoryRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the category route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.CategoryDetail.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CategoryRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the category route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.CategoryDetail.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the dashboard route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Dashboard.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the dashboard route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Dashboard.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Dashboard",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the dashboard route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Dashboard.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the dashboard route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Dashboard.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the cart route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Cart.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the cart route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Cart.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Cart",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the cart route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Cart.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the cart route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Cart.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TermsRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the terms route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Terms.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TermsRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the terms route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Terms.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Terms",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TermsRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the terms route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Terms.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TermsRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the terms route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Terms.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PrivacyRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the privacy route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Privacy.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PrivacyRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the privacy route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Privacy.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Privacy",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PrivacyRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the privacy route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Privacy.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PrivacyRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the privacy route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Privacy.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ContactUsRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the contact us route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ContactUs.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ContactUsRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the contact us route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ContactUs.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Info/Contact-Us",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ContactUsRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the contact us route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ContactUs.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ContactUsRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the contact us route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ContactUs.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoginRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the login route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Login.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoginRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the login route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Login.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Authentication/Sign-In",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoginRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the login route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Login.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoginRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the login route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Login.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the registration route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Registration.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the registration route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Registration.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Authentication/Registration",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Registration.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the registration route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Registration.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForcedPasswordResetRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the forced password reset route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForcedPasswordReset.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForcedPasswordResetRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the forced password reset route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForcedPasswordReset.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Authentication/Forced-Password-Reset",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForcedPasswordResetRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the forced password reset route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForcedPasswordReset.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForcedPasswordResetRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the forced password reset route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForcedPasswordReset.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForgotPasswordRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the forgot password route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForgotPassword.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForgotPasswordRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the forgot password route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForgotPassword.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Authentication/Forgot-Password",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForgotPasswordRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the forgot password route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForgotPassword.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForgotPasswordRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the forgot password route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForgotPassword.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForgotUsernameRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the forgot username route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForgotUsername.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForgotUsernameRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the forgot username route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForgotUsername.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Authentication/Forgot-Username",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForgotUsernameRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the forgot username route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForgotUsername.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForgotUsernameRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the forgot username route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.ForgotUsername.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MembershipRegistrationRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the membership registration route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MembershipRegistration.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MembershipRegistrationRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the membership registration route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MembershipRegistration.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Membership-Registration",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MembershipRegistrationRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the membership registration route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MembershipRegistration.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MembershipRegistrationRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the membership registration route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.MembershipRegistration.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReportingRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the reporting route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Reporting.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReportingRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the reporting route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Reporting.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/Reporting",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReportingRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the reporting route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Reporting.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReportingRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the reporting route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Reporting.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the scheduler route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Scheduler.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the scheduler route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Scheduler.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/DesktopModules/ClarityEcommerce/Scheduler",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the scheduler route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Scheduler.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the scheduler route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Scheduler.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ConnectRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the connect route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Connect.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ConnectRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the connect route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Connect.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ConnectRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the connect route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Connect.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ConnectRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the connect route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Connect.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CompanyLogoRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the company logo route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.CompanyLogo.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CompanyLogoRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the company logo route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.CompanyLogo.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Portals/0/clarity-ecommerce-logo.png",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CompanyLogoRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the company logo route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.CompanyLogo.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CompanyLogoRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the company logo route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.CompanyLogo.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesRootRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the stored files root route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoredFiles.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesRootRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the stored files root route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoredFiles.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/images/ecommerce",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesRootRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files root route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoredFiles.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesRootRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the stored files root route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.StoredFiles.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesRootRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the images root route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Images.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesRootRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the images root route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Images.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/images/ecommerce",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesRootRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the images root route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Images.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImagesRootRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the images root route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Images.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsRootRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the imports root route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Imports.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsRootRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the imports root route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Imports.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/images/ecommerce",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsRootRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the imports root route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Imports.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsRootRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the imports root route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.Imports.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailTemplateRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the template override route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.EmailTemplate.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailTemplateRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the template override route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.EmailTemplate.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Portals/_default/Skins/Clarity/Ecommerce/Email",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailTemplateRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the template override route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.EmailTemplate.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailTemplateRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the template override route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.EmailTemplate.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesAllowDueDate",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices due date is allowed.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.AllowDueDate",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesDueDateDefault",
ParamDocs: <IParamDocs>{
Summary: "Gets the default Invoice Due Date.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.DueDateDefault",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)30",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesInvoicesAllowDueDate\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesHasIntegratedKeys",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices has integrated keys.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.HasIntegratedKeys",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesEmailsPaymentRemindersOccurAt",
ParamDocs: <IParamDocs>{
Summary: "Gets the sales invoices emails payment reminders occur at days.",
},
PropertyType: "System.Int32[]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.Emails.PaymentReminders.OccurAt",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"-30,-14,-7,-3,-1,1,3,7,14,30",
]
},
<ICustomAttribute>{
DisplayName: "Split On",
ConstructorArguments: [
"','",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesLateFees",
ParamDocs: <IParamDocs>{
Summary: "Gets the sales invoices late fees.",
},
PropertyType: "Clarity.Ecommerce.JSConfigs.SalesInvoiceLateFee[]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"new Byte[2] { 2, 1",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.LateFees",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"\"[{ Day: 1, Amount: 3, Kind: 'p', { Day: 30, Amount: 3, Kind: 'p', { Day: 60, Amount: 3, Kind: 'p']\"",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesEmailsInvoiceCreatedFromConnectEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices emails invoice created from connect is\
enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromConnect.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesEmailsInvoiceCreatedFromBillMeLaterInCheckoutEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices emails invoice created from bill me later in\
checkout is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromBillMeLaterInCheckout.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesEmailsInvoiceCreatedFromNETXEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices emails invoice created from netx is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromNETX.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesEmailsInvoiceCreatedFromOrderEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices emails invoice created from order is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromOrder.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesEmailsInvoiceCreatedFromNewInvoiceWizardEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices emails invoice created from new invoice wizard is\
enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.Emails.InvoiceCreated.FromNewInvoiceWizard.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesEmailsPaymentRemindersEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices emails payment reminders is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.Emails.PaymentReminders.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesEmailsPaymentRecievedEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices emails payment recieved is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.Emails.PaymentRecieved.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesCustomerCanPayViaUserDashboardSingle",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices customer can pay via user dashboard single.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.CustomerCanPayViaUserDashboard.Single",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesCustomerCanPayViaUserDashboardSinglePartially",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices customer can pay via user dashboard single\
partially.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.CustomerCanPayViaUserDashboard.Single.Partially",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesCustomerCanPayViaUserDashboardMulti",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices customer can pay via user dashboard multi.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.CustomerCanPayViaUserDashboard.Multi",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesCustomerCanPayViaUserDashboardMultiPartially",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices customer can pay via user dashboard multi\
partially.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.CustomerCanPayViaUserDashboard.Multi.Partially",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesCSRCanPayViaAdminWithData",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices cursor can pay via admin with data.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.CSRCanPayViaAdmin.WithData",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesCSRCanPayViaAdminWithDataPartially",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices cursor can pay via admin with data partially.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.CSRCanPayViaAdmin.WithData.Partially",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesCSRCanPayViaAdminWithoutData",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices cursor can pay via admin without data.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.CSRCanPayViaAdmin.WithoutData",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesInvoicesCSRCanPayViaAdminWithoutDataPartially",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales invoices cursor can pay via admin without data\
partially.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesInvoices.CSRCanPayViaAdmin.WithoutData.Partially",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByInvoiceEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by invoice is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByInvoice.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByInvoiceTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by invoice title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByInvoice.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.Invoice",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByInvoiceEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByInvoicePosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by invoice position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByInvoice.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)30",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByInvoiceEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByInvoiceUpliftUseWhicheverIsGreater",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by Invoice uplift use whichever is greater (if not, will combine both).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByInvoice.Uplifts.UseGreater",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByInvoiceUpliftPercent\", \"PaymentsByInvoiceUpliftAmount\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByInvoiceRestrictedAccountTypes",
ParamDocs: <IParamDocs>{
Summary: "A list of account types restricted from using PaymentsByInvoice.",
Example: "CUSTOMER,ORGANIZATION.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByInvoice.RestrictedAccountTypes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByInvoiceEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByInvoiceUpliftPercent",
ParamDocs: <IParamDocs>{
Summary: "A percentage uplift.",
Example: "0.03 = 3% increase, -0.03 = 3% decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByInvoice.Uplifts.Percent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByInvoiceEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByInvoiceUpliftAmount",
ParamDocs: <IParamDocs>{
Summary: "An amount uplift.",
Example: "5.00 = $5.00 increase, -5.00 = $5.00 decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByInvoice.Uplifts.Amount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByInvoiceEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteSalesInvoicesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route sales invoices is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.SalesInvoices.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathSalesInvoices",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path sales invoices.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.SalesInvoice",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/SalesInvoice",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CheckByMailKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the check by mail key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.PaymentMethodCheckByMailKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "WireTransferKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the wire transfer key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.PaymentMethodWireTransferKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "OnlinePaymentRecordKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the online payment record key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.PaymentMethodOnlinePaymentRecordKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByInvoiceCreditCardLimit",
ParamDocs: <IParamDocs>{
Summary: "An credit card.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByInvoice.CreditCardLimit",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)1000000",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuotesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quotes is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SplitShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuotesHasIntegratedKeys",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quotes has integrated keys.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.HasIntegratedKeys",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesQuotesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuoteRequiresStoreID",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quote requires store identifier.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.RequiresStoreID",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuoteRequiresStoreIDOrKey",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quote requires store identifier or key.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.RequiresStoreIDOrKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuoteRequiresStoreProductID",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quote requires store product identifier.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.RequiresStoreProductID",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuoteRequiresBrandID",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quote requires brand identifier.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.RequiresBrandID",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuoteRequiresBrandIDOrKey",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quote requires brand identifier or key.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.RequiresBrandIDOrKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuoteRequiresBrandProductID",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quote requires brand product identifier.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.RequiresBrandProductID",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuoteRequiresFranchiseID",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quote requires franchise identifier.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.RequiresFranchiseID",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuoteRequiresFranchiseIDOrKey",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quote requires franchise identifier or key.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.RequiresFranchiseIDOrKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuoteRequiresFranchiseProductID",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quote requires franchise product identifier.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.RequiresFranchiseProductID",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuotesIncludeQuantityColumn",
ParamDocs: <IParamDocs>{
Summary: "Include the Quantities in valuation of quotes (adds extra columns to the UI and import/exports).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.IncludeQuantityColumn",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesQuotesUseQuoteCart",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales quotes use the quote cart.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesQuotes.UseQuoteCart",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "QuoteCartRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the quote cart route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.QuoteCart.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "QuoteCartRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the quote cart route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.QuoteCart.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Quote",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "QuoteCartRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the quote cart route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.QuoteCart.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "QuoteCartRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the quote cart route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.QuoteCart.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteRouteHostUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the submit quote route host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.SubmitQuote.RootUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteRouteRelativePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the submit quote route relative file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.SubmitQuote.RelativePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/Submit-Quote",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteRouteHostLookupMethod",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote route host lookup method.",
},
PropertyType: "Clarity.Ecommerce.Enums.HostLookupMethod",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.SubmitQuote.HostLookup.Method",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupMethod)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteRouteHostLookupWhichUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the submit quote route host lookup which.",
},
PropertyType: "System.Nullable`1[Clarity.Ecommerce.Enums.HostLookupWhichUrl]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Routes.SubmitQuote.HostLookup.WhichUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.HostLookupWhichUrl)0",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByQuoteMeEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by quote me is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByQuoteMe.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByQuoteMeTitleKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by quote me title key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByQuoteMe.TitleKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.QuoteMe",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByQuoteMeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByQuoteMePosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments by quote me position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByQuoteMe.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)90",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByQuoteMeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByQuoteMeUpliftUseWhicheverIsGreater",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments by QuoteMe uplift use whichever is greater (if not,\
will combine both).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByQuoteMe.Uplifts.UseGreater",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByQuoteMeUpliftPercent\", \"PaymentsByQuoteMeUpliftAmount\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByQuoteMeRestrictedAccountTypes",
ParamDocs: <IParamDocs>{
Summary: "A list of account types restricted from using PaymentsByQuoteMe.",
Example: "CUSTOMER,ORGANIZATION.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByQuoteMe.RestrictedAccountTypes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByQuoteMeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByQuoteMeUpliftPercent",
ParamDocs: <IParamDocs>{
Summary: "A percentage uplift.",
Example: "0.03 = 3% increase, -0.03 = 3% decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByQuoteMe.Uplifts.Percent",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByQuoteMeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsByQuoteMeUpliftAmount",
ParamDocs: <IParamDocs>{
Summary: "An amount uplift.",
Example: "5.00 = $5.00 increase, -5.00 = $5.00 decrease.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ByQuoteMe.Uplifts.Amount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsByQuoteMeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteSalesQuotesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route sales quotes is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.SalesQuotes.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"SalesQuotesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathSalesQuotes",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path sales quotes.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.SalesQuote",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/SalesQuote",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ImportsPathSalesQuotes",
ParamDocs: <IParamDocs>{
Summary: "Gets the imports path sales quotes.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Images.SalesQuote",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/SalesQuote",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesQuotesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteAddAddressesToBook",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the checkout add addresses to book.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.AddAddressesToBook",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteMiniCartEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the mini cart is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.MiniCart.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "GuestSubmitQuoteEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the guest submit quote is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Guest.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Mutually Exclusive With",
ConstructorArguments: [
"\"SplitShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitSalesQuoteDefaultTypeKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit sales quote default type key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Quoting.SubmitDefaultQuoteTypeKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Web",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesQuotesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesMethodEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the submit quote panes method is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Method.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesMethodPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes method position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Method.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesMethodEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesMethodTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes method title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Method.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.checkoutPanels.CheckoutMethod",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesMethodEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesMethodContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes method continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Method.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesMethodEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesMethodShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the submit quote panes method show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Method.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesMethodEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesShippingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the submit quote panes shipping is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Shipping.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesShippingPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes shipping position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Shipping.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)2",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesShippingTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes shipping title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Shipping.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.ShippingInformation",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesShippingContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes shipping continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Shipping.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.views.accountInformation.continueToShipping",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesShippingShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the submit quote panes shipping show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Shipping.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesConfirmationEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the submit quote panes confirmation is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Confirmation.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesConfirmationConditionallyEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the submit quote panes confirmation is conditionally enabled.",
Remarks: "Only show if there's no other mid-pane to show (free items that don't ship).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Confirmation.ConditionallyEnabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesConfirmationPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes confirmation position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Confirmation.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)3",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesConfirmationTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes confirmation title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Confirmation.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.submitQuote.QuoteConfirmation",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesConfirmationContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes confirmation continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Confirmation.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.SubmitQuote",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesConfirmationShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the submit quote panes confirmation show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Confirmation.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesCompletedEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the submit quote panes completed is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Completed.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesCompletedPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes completed position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Completed.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)4",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesCompletedTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes completed title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Completed.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.checkoutPanels.Complete",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesCompletedContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the submit quote panes completed continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Completed.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.SubmitQuote",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuotePanesCompletedShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the submit quote panes completed show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SubmitQuote.Panes.Completed.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SubmitQuotePanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideBillingFirstName",
ParamDocs: <IParamDocs>{
Summary: "Hide billing first name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.Billing.HideFirstName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideBillingLastName",
ParamDocs: <IParamDocs>{
Summary: "Hide billing last name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.Billing.HideLastName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideBillingEmail",
ParamDocs: <IParamDocs>{
Summary: "Hide billing email.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.Billing.HideEmail",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideBillingPhone",
ParamDocs: <IParamDocs>{
Summary: "Hide billing phone.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.ShippBillinging.HidePhone",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideBillingFax",
ParamDocs: <IParamDocs>{
Summary: "Hide billing fax.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.Billing.HideFax",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideShippingFirstName",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping first name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HideFirstName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideShippingLastName",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping last name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HideLastName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideShippingEmail",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping email.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HideEmail",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideShippingPhone",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping phone.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HidePhone",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SubmitQuoteHideShippingFax",
ParamDocs: <IParamDocs>{
Summary: "Hide shipping fax.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SubmitQuote.Inputs.Shipping.HideFax",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesReturnsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales returns is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesReturns.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesReturnsHasIntegratedKeys",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales returns has integrated keys.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesReturns.HasIntegratedKeys",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesReturnsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsAreSingleCreation",
ParamDocs: <IParamDocs>{
Summary: "One item per RMA vs multiple.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SalesReturns.SingleCreation",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsValidityPeriodInDays",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns validity period in days.",
},
PropertyType: "System.Nullable`1[System.Int32]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SalesReturns.ValidityPeriodInDays",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)45",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsNumberFormat",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns number format.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SalesReturns.NumberFormat",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"{{OrderID}}RMA{{ItemSku}}",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsDestinationAddressCompany",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns destination address company.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ReturnsDestination.Company",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Company Returns",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsDestinationAddressStreet1",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns destination address street 1.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ReturnsDestination.Street1",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"6805 N Capital of Texas Hwy",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsDestinationAddressStreet2",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns destination address street 2.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ReturnsDestination.Street2",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Suite 312",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsDestinationAddressStreet3",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns destination address street 3.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ReturnsDestination.Street3",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsDestinationAddressCity",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns destination address city.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ReturnsDestination.City",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Austin",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsDestinationAddressPostalCode",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns destination address postal code.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ReturnsDestination.PostalCode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"78731",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsDestinationAddressRegionCode",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns destination address region code.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ReturnsDestination.RegionCode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"TX",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsDestinationAddressCountryCode",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns destination address country code.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ReturnsDestination.CountryCode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"USA",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReturnsDestinationAddressPhone",
ParamDocs: <IParamDocs>{
Summary: "Gets the returns destination address phone.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ReturnsDestination.Phone",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteSalesReturnsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route sales returns is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.SalesReturns.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"SalesReturnsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoredFilesPathSalesReturns",
ParamDocs: <IParamDocs>{
Summary: "Gets the stored files path sales returns.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.Files.SalesReturn",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/SalesReturn",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIName",
ParamDocs: <IParamDocs>{
Summary: "Gets the name of the API.",
Remarks: "Sets the name of the API that ServiceStack launches itself as.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"APIName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Clarity eCommerce Platform API",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PluginsPath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the plugins folder.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Providers.PluginsPath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"{CEF_RootPath}\\Plugins",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ClientsPath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the clients folder.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Providers.ClientsPath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"{CEF_RootPath}\\ClientPlugins",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ScheduledTasksPath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the plugins folder.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Providers.SchedulerPath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"{CEF_RootPath}\\07.Portals\\Scheduler\\Scheduler\\bin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CompanyName",
ParamDocs: <IParamDocs>{
Summary: "The name of the company to display in SEO URLs and pages like Registration.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.CompanyName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Clarity eCommerce Demo",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CountryCode",
ParamDocs: <IParamDocs>{
Summary: "The default country code.",
Remarks: "If this is not set, default 'getRegions' calls will not run.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Globalization.CountryCode.Default",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"USA",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LogEveryRequest",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the log every request.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.LogEveryRequest",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIDisableMetadata",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the API disable metadata.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.DisableMetadata",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIUseUTC",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the API use UTC.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.UseUTC",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ServiceStackOriginsWhiteList",
ParamDocs: <IParamDocs>{
Summary: "For CORS: Have to include each of: no-prefix, http and https prefixes.",
Example: "develop.claritydemos.com http://develop.claritydemos.com https://develop.claritydemos.com .",
},
PropertyType: "System.String[]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Requests.OriginsWhiteList",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"new String[0] { ",
]
},
<ICustomAttribute>{
DisplayName: "Split On",
ConstructorArguments: [
"' '",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CORSResourceWhiteList",
ParamDocs: <IParamDocs>{
Summary: "Web locations where resources like CSS and JS files can be loaded from (a white list).",
Example: "// &quot;self&quot; is already included\
// Don't use any localhost with or without port numbers as it confuses Chrome\
// Allow loading from our assets domain. Notice the difference between * and **\
&quot;http://some.subdomain.website.com/**&quot;,\
&quot;https://some.subdomain.website.com/**&quot;,\
&quot;http://shop.my-website.com/**&quot;,\
&quot;https://shop.my-website.com/**&quot;,\
&quot;http://*.webdev.us/**&quot;,\
&quot;http://*.webdev.*/**&quot; .",
},
PropertyType: "System.String[]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.CORS.ResourceWhiteList",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"new String[0] { ",
]
},
<ICustomAttribute>{
DisplayName: "Split On",
ConstructorArguments: [
"' '",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AdminDebugMode",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the storefront debug mode.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Admin.DebugMode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StorefrontDebugMode",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the storefront debug mode.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Storefront.DebugMode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StorefrontHTML5Mode",
ParamDocs: <IParamDocs>{
Summary: "Gets the storefront HTML 5 mode.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Storefront.HTML5Mode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"{ enabled: false, requireBase: false }",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AdminHTML5Mode",
ParamDocs: <IParamDocs>{
Summary: "Gets the admin HTML 5 mode.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Admin.HTML5Mode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"true",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoreAdminHTML5Mode",
ParamDocs: <IParamDocs>{
Summary: "Gets the store admin HTML 5 mode.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.StoreAdmin.HTML5Mode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"{ enabled: false, requireBase: false }",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "BrandAdminHTML5Mode",
ParamDocs: <IParamDocs>{
Summary: "Gets the brand admin HTML 5 mode.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.BrandAdmin.HTML5Mode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"{ enabled: false, requireBase: false }",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "VendorAdminHTML5Mode",
ParamDocs: <IParamDocs>{
Summary: "Gets the vendor admin HTML 5 mode.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.VendorAdmin.HTML5Mode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"true",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AdminShowStorefrontButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the admin show storefront button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Admin.MenuButtons.Storefront.Show",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AdminShowDNNButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the admin show DotNetNuke button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Admin.MenuButtons.DNN.Show",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIRequestsValidateStore",
ParamDocs: <IParamDocs>{
Summary: "If true, every request that has a StoreID as part of it (based on an internal interface) will\
determine the appropriate store ID to place on it and do so, overriding any value that came over the wire.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Requests.ValidateStore",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"StoresEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIRequestsValidateFranchise",
ParamDocs: <IParamDocs>{
Summary: "If true, every request that has a FranchiseID as part of it (based on an internal interface) will\
determine the appropriate franchise ID to place on it and do so, overriding any value that came over the wire.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Requests.ValidateFranchise",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"FranchisesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIRequestsValidateBrand",
ParamDocs: <IParamDocs>{
Summary: "If true, every request that has a BrandID as part of it (based on an internal interface) will\
determine the appropriate brand ID to place on it and do so, overriding any value that came over the wire.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Requests.ValidateBrand",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"BrandsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIRequestsValidateManufacturer",
ParamDocs: <IParamDocs>{
Summary: "If true, every request that has a ManufacturerID as part of it (based on an internal interface) will\
determine the appropriate manufacturer ID to place on it and do so, overriding any value that came over the wire.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Requests.ValidateManufacturer",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ManufacturersEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIRequestsValidateVendor",
ParamDocs: <IParamDocs>{
Summary: "If true, every request that has a VendorID as part of it (based on an internal interface) will\
determine the appropriate vendor ID to place on it and do so, overriding any value that came over the wire.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Requests.ValidateVendor",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"VendorsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIRequestsRequireHTTPS",
ParamDocs: <IParamDocs>{
Summary: "If true, every request validates that is was sent over HTTPS, and returns 403 if it fails this check.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Requests.RequireHTTPS",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "APIRequestsAlwaysVeryByReferer",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the API requests always very by referer.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Requests.AlwaysVeryByReferer",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardAddAddressesToBook",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard add addresses to book.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.AddAddressesToBook",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookEnabled",
ParamDocs: <IParamDocs>{
Summary: "Site-wide activation of address book UI and functionality.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookHideKeys",
ParamDocs: <IParamDocs>{
Summary: "Address Book hide custom keys.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.Inputs.HideKeys",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookHideFirstName",
ParamDocs: <IParamDocs>{
Summary: "Address Book hide first name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.Inputs.HideFirstName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookHideLastName",
ParamDocs: <IParamDocs>{
Summary: "Address Book hide first name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.Inputs.HideLastName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookHideEmail",
ParamDocs: <IParamDocs>{
Summary: "Address Book hide last name.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.Inputs.HideEmail",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookHidePhone",
ParamDocs: <IParamDocs>{
Summary: "Address Book hide phone.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.Inputs.HidePhone",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookHideFax",
ParamDocs: <IParamDocs>{
Summary: "Address Book hide fax.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.Inputs.HideFax",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookAllowMakeThisMyNewDefaultBillingInCheckout",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the address book allow make this my new default billing in\
checkout.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.AllowMakeThisMyNewDefault.Billing.InCheckout",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookAllowMakeThisMyNewDefaultBillingInDashboard",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the address book allow make this my new default billing in\
dashboard.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.AllowMakeThisMyNewDefault.Billing.InDashboard",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\", \"PurchasePanesBillingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookAllowMakeThisMyNewDefaultShippingInCheckout",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the address book allow make this my new default shipping in\
checkout.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.AllowMakeThisMyNewDefault.Shipping.InCheckout",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"new String[3] { \"AddressBookEnabled\", \"ShippingEnabled\", \"PurchasePanesShippingEnabled\"",
]
},
<ICustomAttribute>{
DisplayName: "Mutually Exclusive With",
ConstructorArguments: [
"\"SplitShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookAllowMakeThisMyNewDefaultShippingInDashboard",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the address book allow make this my new default shipping in\
dashboard.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.AllowMakeThisMyNewDefault.Shipping.InDashboard",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\", \"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AdsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the ads is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Ads.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AffiliatesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the affiliates is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Affiliates.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuctionsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether auctions are enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Auctions.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PercentageOfGreatestBid",
ParamDocs: <IParamDocs>{
Summary: "Gets a value for the Global Bid Percentage.",
},
PropertyType: "System.Nullable`1[System.Decimal]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Auctions.GlobalBidPercentage",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoginEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the login is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Login.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviders",
ParamDocs: <IParamDocs>{
Summary: "Gets the authentication providers.",
Example: "identity\
dnnsso\
tokenized\
openid\
openid,identity .",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"identity",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderPasswordRequireDigit",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the authentication provider password require digit.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Passwords.RequireDigit",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderPasswordRequireLength",
ParamDocs: <IParamDocs>{
Summary: "Gets the length of the authentication provider password require.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Passwords.RequireLength",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)6",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderPasswordRequireLowercase",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the authentication provider password require lowercase.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Passwords.RequireLowercase",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderPasswordRequireUppercase",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the authentication provider password require uppercase.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Passwords.RequireUppercase",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderPasswordRequireNonLetterOrDigit",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the authentication provider password require non letter or digit.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Passwords.RequireNonLetterOrDigit",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderUsernameIsEmail",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the authentication provider username is email.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.Identity.UsernameIsEmail",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UseSpecialCharInEmail",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the authentication provider username is email.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.Email.UseSpecialCharInEmail",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TwoFactorEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether two factor authentication is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Login.TwoFactor.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TwoFactorForced",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the two factor forced.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Login.TwoFactor.Forced.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"TwoFactorEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TwoFactorByEmailEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the two factor by email is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Login.TwoFactor.ByEmail.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"TwoFactorEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TwoFactorBySMSEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the two factor by SMS is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Login.TwoFactor.BySMS.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"TwoFactorEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TwoFactorByEmailSubject",
ParamDocs: <IParamDocs>{
Summary: "Gets the two factor by email subject.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Login.TwoFactor.ByEmail.Subject",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"TwoFactorByEmailEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TwoFactorByEmailBody",
ParamDocs: <IParamDocs>{
Summary: "Gets the two factor by email body.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Login.TwoFactor.ByEmail.Body",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"TwoFactorByEmailEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TwoFactorBySMSBody",
ParamDocs: <IParamDocs>{
Summary: "Gets the two factor by SMS body.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Login.TwoFactor.BySMS.Body",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"TwoFactorBySMSEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderAuthorizeUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the authentication provider authorize.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.AuthorizeUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderClientId",
ParamDocs: <IParamDocs>{
Summary: "Gets the identifier of the authentication provider client.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.ClientID",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderClientSecret",
ParamDocs: <IParamDocs>{
Summary: "Gets the authentication provider client secret.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.ClientSecret",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderDiscoveryUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the authentication provider discovery.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.DiscoveryUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderEmailClaimName",
ParamDocs: <IParamDocs>{
Summary: "Gets the name of the authentication provider email claim.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.EmailClaimName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderIssuer",
ParamDocs: <IParamDocs>{
Summary: "Gets the authentication provider issuer.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.Issuer",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderLogoutUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the authentication provider logout.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.LogoutUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderRedirectUri",
ParamDocs: <IParamDocs>{
Summary: "Gets URI of the authentication provider redirect.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.RedirectUri",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderScope",
ParamDocs: <IParamDocs>{
Summary: "Gets the authentication provider scope.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.Scope",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderTokenUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the authentication provider token.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.TokenUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderUserInfoUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the authentication provider user information.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.OpenID.UserInfoUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"OpenIDAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AuthProviderTokenizedAlternativeCheckUrl",
ParamDocs: <IParamDocs>{
Summary: "Gets URL of the authentication provider tokenized alternative check.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Providers.Tokenized.AlternativeCheckUrl",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"TokenizedAuthProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UserApprovalToken",
ParamDocs: <IParamDocs>{
Summary: "Gets the user approval token.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.User.Approval.Token",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Mutually Exclusive With",
ConstructorArguments: [
"\"NewUsersAreDefaultApproved\", \"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InvitationToken",
ParamDocs: <IParamDocs>{
Summary: "Gets the invitation token.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.Invitation.Token",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReCaptchaSecret",
ParamDocs: <IParamDocs>{
Summary: "Gets the re captcha secret.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.ReCaptchaSecret",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CreateReferralCodeDuringRegistration",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the create referral code during registration.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Auth.CreateReferralCodeDuringRegistration",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "NewUsersAreDefaultActive",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the new users are default active.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UserRegistration.Active",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "NewUsersAreDefaultApproved",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the new users are default approved.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UserRegistration.IsApproved",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "NewUsersAreDefaultLockedOut",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the new users are default locked out.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UserRegistration.LockoutEnabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "NewUsersGainDefaultRole",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the new users gain default role.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UserRegistration.AddDefaultRole",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DefaultUserRole",
ParamDocs: <IParamDocs>{
Summary: "Gets the default user role.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UserRegistration.DefaultRole",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"CEF User",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"NewUsersGainDefaultRole\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "NewUsersAllowLookupExistingAccountOnRegistration",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the new users allow lookup existing account on registration.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UserRegistration.AllowLookupExistingAccount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShouldQueueEmailOnNewUserCreatedBackOffice",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the new users allow lookup existing account on registration.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UserRegistration.ShouldQueueEmailOnNewUserCreated.BackOffice",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShouldQueuePasswordResetEmailOnSetUserAsActive",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether we should queue password reset email on set user as active.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Users.ShouldQueuePasswordResetEmailOnSetUserAsActive",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RequireEmailVerificationForNewUsers",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether new users are required to verify email before logging in.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Users.RequireEmailVerificationForNewUsers",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UploadsAWSCredentialProfile",
ParamDocs: <IParamDocs>{
Summary: "Gets the uploads the ws credential profile.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.AWS.CredentialProfile",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Unused",
ConstructorArguments: [
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AWSFilesProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UploadsAWSDefaultAccessKeyId",
ParamDocs: <IParamDocs>{
Summary: "Gets the identifier of the uploads the ws default access key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.AWS.DefaultAccessKeyId",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AWSFilesProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UploadsAWSDefaultBucket",
ParamDocs: <IParamDocs>{
Summary: "Gets the uploads the ws default bucket.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.AWS.DefaultBucket",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AWSFilesProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UploadsAWSDefaultCannedACL",
ParamDocs: <IParamDocs>{
Summary: "Gets the uploads the ws default canned a cl.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.AWS.DefaultCannedACL",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AWSFilesProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UploadsAWSDefaultFolder",
ParamDocs: <IParamDocs>{
Summary: "Gets the pathname of the uploads the ws default folder.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.AWS.DefaultFolder",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AWSFilesProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UploadsAWSDefaultProfile",
ParamDocs: <IParamDocs>{
Summary: "Gets the uploads the ws default profile.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.AWS.DefaultProfile",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AWSFilesProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UploadsAWSDefaultRegionEndpoint",
ParamDocs: <IParamDocs>{
Summary: "Gets the uploads the ws default region endpoint.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.AWS.DefaultRegionEndpoint",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AWSFilesProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UploadsAWSDefaultSecretAccessKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the uploads the ws default secret access key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Uploads.AWS.DefaultSecretAccessKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AWSFilesProvider\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "BadgesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the badges is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Badges.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"StoresEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "BrandsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the brands is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Brands.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "BrandsSiteDomainsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the brands site domains is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Brands.SiteDomains.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"BrandsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CalendarEventsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the calendar events is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.CalendarEvents.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CalendarEventsChangePackageLimitInDays",
ParamDocs: <IParamDocs>{
Summary: "Gets the calendar events change package limit in days.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.CalendarEvents.ChangePackageLimitInDays",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)30",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CalendarEventsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UserEventAttendancesSendGroupLeaderEmailsOnCreate",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the user event attendances send group leader emails on create.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UserEventAttendances.SendGroupLeaderEmailsOnCreate",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CalendarEventsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartValidationDoProductRestrictions",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the cart validation do product restrictions.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.CartValidation.DoProductRestrictions",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartValidationSingleStoreInCartEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the cart validation limits products to a single store.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.CartValidation.SingleStoreInCartEnabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartValidationMaximumCartItems",
ParamDocs: <IParamDocs>{
Summary: "Gets the maximum allowed number of items in a cart.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.CartValidation.MaximumCartItems",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)9",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartValidationProductRestrictionsKeys",
ParamDocs: <IParamDocs>{
Summary: "Gets the cart validation product restrictions keys.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.CartValidation.ProductRestrictionsKeys",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartValidationDoProductRestrictions\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the carts is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Carts.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartsAngularServiceDebuggingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the carts angular service debugging is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Carts.NgServiceDebugging.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddToCartModalsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Site-wide enabling of all modals that result from adding an item to the cart.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UI.AddToCartModals.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddToQuoteCartModalIsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the confirm add to quote cart modal should be shown.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.UI.AddToQuoteCartModal.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesQuotesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartsWishListEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the carts wish list is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Carts.WishList.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"new String[3] { \"LoginEnabled\", \"CartsEnabled\", \"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartsFavoritesListEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the carts favorites list is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Carts.FavoritesList.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"new String[3] { \"LoginEnabled\", \"CartsEnabled\", \"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartsNotifyMeWhenInStockListEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the carts notify me when in stock list is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Carts.NotifyMeWhenInStock.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"new String[3] { \"LoginEnabled\", \"CartsEnabled\", \"InventoryEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartsShoppingListsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the carts shopping lists is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Carts.ShoppingLists.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"new String[3] { \"LoginEnabled\", \"CartsEnabled\", \"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CartsCompareEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the carts compare is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Carts.Compare.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ExplodeKitsAddedToCart",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the explode kits added to cart.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Carts.ExplodeKitsAddedToCart",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CartsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogShowProductCategoriesForLevelsUpToX",
ParamDocs: <IParamDocs>{
Summary: "Search Catalog: How many levels of depth into Categories to show before we can display products.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Catalog.ShowProductCategoriesForLevelsUpToX",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CategoriesEnabled\"",
]
},
<ICustomAttribute>{
DisplayName: "Mutually Exclusive With",
ConstructorArguments: [
"\"The separated Categories landing pages for DNN\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogDefaultPageSize",
ParamDocs: <IParamDocs>{
Summary: "Search Catalog: The default page size to start with.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Catalog.DefaultPageSize",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)9",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogDefaultFormat",
ParamDocs: <IParamDocs>{
Summary: "Search Catalog: The default format (layout) to start with.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Catalog.DefaultFormat",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"grid",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogDefaultSort",
ParamDocs: <IParamDocs>{
Summary: "Search Catalog: The default Sort method to start with.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Catalog.DefaultSort",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Relevance",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogOnlyApplyStoreToFilterByUI",
ParamDocs: <IParamDocs>{
Summary: "Search Catalog: Apply the Store ID to the search only if the user has selected it via UI. When false,\
Store ID will always be forced onto the search from the user's selected Store.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Catalog.OnlyApplyStoreToFilterByUI",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"StoresEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogDisplayImages",
ParamDocs: <IParamDocs>{
Summary: "Search Catalog: Sets the visibility setting for category landing page images.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Catalog.DisplayImages",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogGetFullAssociatedProductsInfo",
ParamDocs: <IParamDocs>{
Summary: "Search Catalog: If true, any associations on products returned from the search will have their data\
loaded to the services.cvProductService for use.",
Remarks: "Warning! This is a performance intensive action if there are many associations (such as variants) on\
each product.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Catalog.GetFullAssociatedProductsInfo",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CatalogTicketExchangeEnabled",
ParamDocs: <IParamDocs>{
Summary: "Search Catalog: Enables ticket exchange information on the search Catalog.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Catalog.TicketExchange",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MaxTopLevelCategoriesInCatalogFilter",
ParamDocs: <IParamDocs>{
Summary: "Search Catalog: Limits the number of top-level categories displayed in the catalog filter accordion\
on the catalog.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Catalog.MaxTopLevelCategoriesInFilter",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)20",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CategoriesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the categories is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Categories.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CategoriesDoRestrictionsByMinMax",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the categories do restrictions by minimum maximum (hard/soft stops).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Carts.Validation.DoCategoryRestrictionsByMinMax",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ChargesHandlingForNon0CostOrWeightOrders",
ParamDocs: <IParamDocs>{
Summary: "Gets the charges handling for non 0 cost or weight orders.",
},
PropertyType: "System.Nullable`1[System.Decimal]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Charges.HandlingForNon0CostOrWeightOrders",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ChatEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the chat is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Chat.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MessagingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the messaging is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Messaging.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesDomain",
ParamDocs: <IParamDocs>{
Summary: "For enforcing cookies to be sharable by not including the left-most sub-domain (or some other\
combination value).",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.Domain",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesPath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the cookies file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.Path",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"/",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesUseSubDomain",
ParamDocs: <IParamDocs>{
Summary: "When true, will not remove the subdomain from the domain when setting cookies.",
Example: "shop.mysite.com will stay as shop.mysite.com.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.UseSubDomain",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesUseSubDomainSegmentCount",
ParamDocs: <IParamDocs>{
Summary: "The number of segments to consider shared (counting from the right). If there are more segments in\
the domain than this count, they will be ignored in cookie domain paths for sharing.",
Remarks: "Only applicable if  is false.",
Example: "* with a value of 2, shop.mysite.com will become .mysite.com\
* with a value of 2, sub.shop.mysite.com will become .mysite.com\
* with a value of 3, sub.shop.mysite.com will become shop.mysite.com.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.UseSubDomain.SegmentCount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)2",
]
},
<ICustomAttribute>{
DisplayName: "Mutually Exclusive With",
ConstructorArguments: [
"\"CookiesUseSubDomain\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesRequireSecure",
ParamDocs: <IParamDocs>{
Summary: "If true, every request validates every cookie is set to Secure Only and returns 403 if any fail this\
check.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.RequireSecure",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesRequireSecureIgnoredCEF",
ParamDocs: <IParamDocs>{
Summary: "Gets the cookies require secure ignored cef.",
},
PropertyType: "System.String[]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.RequireSecure.IgnoredCEFCookies",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"cefCoords,cefGeo,cefLocationRequested,CURRENCY_KEY,NG_TRANSLATE_LANG_KEY,ss-id,ss-pid,ss-opt,x-uaid,_ga",
]
},
<ICustomAttribute>{
DisplayName: "Split On",
ConstructorArguments: [
"','",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesRequireHTTPOnly",
ParamDocs: <IParamDocs>{
Summary: "If true, every request validates every cookie is set to HTTP Only and returns 403 if any fail this\
check.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.RequireHTTPOnly",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesRequireHTTPOnlyIgnoredCEF",
ParamDocs: <IParamDocs>{
Summary: "Gets the cookies require HTTP only ignored cef.",
},
PropertyType: "System.String[]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.RequireHTTPOnly.IgnoredCEFCookies",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"cefCoords,cefGeo,cefLocationRequested,CURRENCY_KEY,NG_TRANSLATE_LANG_KEY,ss-id,ss-pid,ss-opt,x-uaid",
]
},
<ICustomAttribute>{
DisplayName: "Split On",
ConstructorArguments: [
"','",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CookiesRequireHTTPOnly\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesValidateAuthValuesEveryRequest",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the cookies validate authentication values every request.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.ValidateAuthValuesEveryRequest",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CookiesIgnoredNonCEF",
ParamDocs: <IParamDocs>{
Summary: "Gets the cookies ignored non cef.",
},
PropertyType: "System.String[]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"new Byte[2] { 2, 1",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.API.Cookies.IgnoredNonCEFCookies",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
".ASPXANONYMOUS,dnn_IsMobile,language,us_lang",
]
},
<ICustomAttribute>{
DisplayName: "Split On",
ConstructorArguments: [
"','",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyDashboardEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether my dashboard is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.MyDashboard.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteMyDashboardEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route my dashboard is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.MyDashboard.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"new String[4] { \"MyDashboardEnabled\", \"SalesOrdersEnabled\", \"SalesQuotesEnabled\", \"SalesInvoicesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteAccountSettingsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route account settings is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.AccountSettings.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteAccountSettingsMyProfileEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route account settings my profile is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.AccountSettings.MyProfile.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteAccountSettingsAccountProfileEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route account settings account profile is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.AccountSettings.AccountProfile.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteAccountSettingsAddressBookEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route account settings address book is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.AccountSettings.AddressBook.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteAccountSettingsWalletEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route account settings wallet is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.AccountSettings.Wallet.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"PaymentsWalletEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteUsersEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route users is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.Users.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteGroupsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route groups is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.Groups.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"SalesGroupsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteSalesOrdersEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route sales orders is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.SalesOrders.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"SalesOrdersEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteTicketsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route tickets is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.Tickets.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"CatalogTicketExchangeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteLabelGenerationEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route label generation is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.LabelGeneration.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteSubscriptionsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route subscriptions is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.Subscriptions.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteSampleRequestsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route sample requests is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.SampleRequests.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"SampleRequestsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteDownloadsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route downloads is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.Downloads.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"ProductStoredFilesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteWishListEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route wish list is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.WishList.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"CartsWishListEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteFavoritesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route favorites is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.Favorites.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"CartsFavoritesListEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteInStockAlertsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route in stock alerts is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.InStockAlerts.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"CartsNotifyMeWhenInStockListEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteShoppingListsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route shopping lists is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.ShoppingLists.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"CartsShoppingListsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DashboardRouteInboxEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the dashboard route inbox is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.Inbox.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\", \"MessagingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CustomDashboardRoutes",
ParamDocs: <IParamDocs>{
Summary: "Gets the custom dashboard routes.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Dashboard.Route.CustomRoutes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"MyDashboardEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyProfileImagesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether my profile images is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Profile.Images.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"DashboardRouteAccountSettingsMyProfileEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MyProfileStoredFilesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether my profile stored files is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Profile.StoredFiles.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"DashboardRouteAccountSettingsMyProfileEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AccountProfileImagesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the account profile images is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.AccountProfile.Images.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"DashboardRouteAccountSettingsAccountProfileEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AccountProfileStoredFilesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the account profile stored files is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.AccountProfile.StoredFiles.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"DashboardRouteAccountSettingsAccountProfileEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DiscountsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the discounts is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Discounts.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the emails is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Emails.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailsWithSplitTemplatesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the emails with split templates is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Emails.SplitTemplates.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"EmailsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailForgotUsernameSubject",
ParamDocs: <IParamDocs>{
Summary: "Gets the email forgot username subject.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Authentication.ForgotUsername.Subject",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Your {{CompanyName}} Username",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"EmailsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailForgotUsernameBodyTemplatePath",
ParamDocs: <IParamDocs>{
Summary: "Gets the full pathname of the email forgot username body template file.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Authentication.ForgotUsername.BodyTemplatePath",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ForgotUsername.html",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"EmailsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailNotificationsMessagingEmailCopiesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the email notifications messaging email copies is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Notifications.Messaging.EnableEmailCopies",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"EmailsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailNotificationsSalesOrderToCustomerByEmailOnSave",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the email notifications sales order to customer by email on save.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Notifications.SalesOrders.Customer.ByEmailOnSave",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"EmailsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailDefaultsBackOfficeEmailAddress",
ParamDocs: <IParamDocs>{
Summary: "Gets the email defaults back office email address.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Emails.Defaults.BackOfficeEmail",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"EmailsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailNotificationsUserProfileUpdatedToBackOfficeByEmailOnSave",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the email notifications user profile updated to backoffice by email on save.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Notifications.UserProfile.BackOffice.ByEmailOnSave",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"EmailsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "EmailDefaultsAttachmentLocation",
ParamDocs: <IParamDocs>{
Summary: "Gets the email defaults attachment location.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Emails.Defaults.AttachmentLocation",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"EmailsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ForgotUsernameEmailFrom",
ParamDocs: <IParamDocs>{
Summary: "Gets the forgot username email from.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.ForgotUsername.EmailFrom",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"no-reply@claritydemos.com",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "NonProductFavoritesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the non product favorites is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.NonProductFavorites.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "FranchisesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the brands is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Franchises.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "FranchisesSiteDomainsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the franchises site domains is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Franchises.SiteDomains.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"FranchisesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DoAutoUpdateLatitudeLongitude",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the do automatic update latitude longitude.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Addresses.DoAutoUpdateLatitudeLongitude",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "GoogleMapsAPIKey",
ParamDocs: <IParamDocs>{
Summary: "The Google Maps API Key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Google.Maps.APIKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"AIzaSyDgmQtEU6ODdXWW1mlliVNqFuwGGeBRoQU",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "GoogleAPIKey",
ParamDocs: <IParamDocs>{
Summary: "The Google general API Key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Google.APIKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"AIzaSyBVTDAWycjSqP_tGKLSXO66_K7JSoYF5VQ",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "GoogleAPIClientKey",
ParamDocs: <IParamDocs>{
Summary: "The Google API Client ID/Key/Secret.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Google.APIClientKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"585747101560-6ccfcfcpg89hoq560qtj8f5eklb0mi4l.apps.googleusercontent.com",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryBackOrderEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory back order is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.BackOrder.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryBackOrderMaxPerProductGlobalEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory back order maximum per product global is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.BackOrder.MaxPerProduct.Global.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryBackOrderEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryBackOrderMaxPerProductPerAccountEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory back order maximum per product per account is\
enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.BackOrder.MaxPerProduct.PerAccount.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryBackOrderEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryBackOrderMaxPerProductPerAccountAfterEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory back order maximum per product per account after is\
enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.BackOrder.MaxPerProduct.PerAccount.After.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryBackOrderMaxPerProductPerAccountEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryPreSaleEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory pre sale is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.PreSale.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryPreSaleMaxPerProductGlobalEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory pre sale maximum per product global is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.PreSale.MaxPerProduct.Global.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryPreSaleEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryPreSaleMaxPerProductPerAccountEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory pre sale maximum per product per account is\
enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.PreSale.MaxPerProduct.PerAccount.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryPreSaleEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryPreSaleMaxPerProductPerAccountAfterEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory pre sale maximum per product per account after is\
enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.PreSale.MaxPerProduct.PerAccount.After.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryPreSaleMaxPerProductPerAccountEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryAdvancedEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory advanced is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Inventory.Advanced.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoginForInventoryEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the login for inventory is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Inventory.LoginForInventory.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryEnabled\", \"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "GetClosestWarehouseWithStock",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether or not to get the closest warehouse with stock.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Inventory.Advanced.GetClosestWarehouseWithStock",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryAdvancedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "WarehouseRegionServesRegionsListJson",
ParamDocs: <IParamDocs>{
Summary: "Gets the warehouse priority matrix JSON.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Inventory.Advanced.ClosestWarehouseWithStock.RegionServesRegionsListJson",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryAdvancedEnabled\", \"GetClosestWarehouseWithStock\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "WarehousePriorityListByRegionMatrixJSON",
ParamDocs: <IParamDocs>{
Summary: "Gets the warehouse priority matrix JSON.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Inventory.Advanced.ClosestWarehouseWithStock.PriorityListByRegionJson",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"InventoryAdvancedEnabled\", \"GetClosestWarehouseWithStock\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "InventoryCountOnlyThisStoresWarehouseStock",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the inventory count only this stores warehouse stock.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Inventory.CountOnlyThisStoresWarehouseStock",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"StoresEnabled\", \"InventoryAdvancedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LanguagesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the languages is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Languages.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DefaultLanguage",
ParamDocs: <IParamDocs>{
Summary: "The default language to use before the user selects their own.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Globalization.Language.Default",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"en_US",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerApplicationName",
ParamDocs: <IParamDocs>{
Summary: "Gets the name of the logger application.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.ApplicationName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Clarity eCommerce Framework Application",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailAlertsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the logger email alerts on.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailAlertsOn",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailAlertsOnErrorOnly",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the logger email alerts on error only.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailAlertsOnErrorOnly",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoggerEmailAlertsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailUseSSLOrTLS",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the logger email use ssl or TLS.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailUseSSLOrTLS",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoggerEmailAlertsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailSMTPHost",
ParamDocs: <IParamDocs>{
Summary: "Gets the logger email SMTP host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailSMTPHost",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"10.10.30.82",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoggerEmailAlertsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailSMTPPort",
ParamDocs: <IParamDocs>{
Summary: "Gets the logger email SMTP port.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailSMTPPort",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)25",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoggerEmailAlertsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailAuthenticate",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the logger email authenticate.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailAuthenticate",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoggerEmailAlertsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailUsername",
ParamDocs: <IParamDocs>{
Summary: "Gets the logger email username.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailUsername",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoggerEmailAlertsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailPassword",
ParamDocs: <IParamDocs>{
Summary: "Gets the logger email password.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailPassword",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoggerEmailAlertsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailFromAddresses",
ParamDocs: <IParamDocs>{
Summary: "Gets the logger email from addresses.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailFromAddresses",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"no-reply@www.claritydemos.com",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoggerEmailAlertsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoggerEmailRecipientAddresses",
ParamDocs: <IParamDocs>{
Summary: "Gets the logger email recipient addresses.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Logger.EmailRecipientAddresses",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"alerts@www.claritydemos.com",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoggerEmailAlertsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ManufacturersEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the manufacturers is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Manufacturers.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ManufacturersDoRestrictionsByMinMax",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the manufacturers do restrictions by minimum maximum (hard/soft stops).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Carts.Validation.DoManufacturerRestrictionsByMinMax",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsWithMembershipsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments with memberships is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Payments.Memberships.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsWithSubscriptionsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments with subscriptions is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Payments.Subscriptions.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MembershipsRenewalPeriodBefore",
ParamDocs: <IParamDocs>{
Summary: "Gets the memberships renewal period before.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Memberships.Renewal.Period.AllowedUpToXDaysBefore",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)30",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MembershipsRenewalPeriodAfter",
ParamDocs: <IParamDocs>{
Summary: "Gets the memberships renewal period after.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Memberships.Renewal.Period.AllowedUpToXDaysAfter",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)15",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MembershipsUpgradePeriodBlackout",
ParamDocs: <IParamDocs>{
Summary: "Gets the memberships upgrade period blackout.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Memberships.Upgrade.Period.BlackoutXDaysBefore",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)30",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MultiCurrencyEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the multi currency is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.MultiCurrency.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "DefaultCurrency",
ParamDocs: <IParamDocs>{
Summary: "Gets the default currency.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Globalization.Currency.Default",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"USD",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesOrdersEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales orders is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesOrders.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesOrdersHasIntegratedKeys",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales orders has integrated keys.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesOrders.HasIntegratedKeys",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesOrdersEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesOrderOffHoldEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales order off hold is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Ordering.OffHold.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesOrdersEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Payments.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsWalletEnabled",
ParamDocs: <IParamDocs>{
Summary: "Site-wide activation of wallet UI and functionality.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Payments.Wallet.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"PaymentsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsWalletRequiredInRegistration",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments wallet required in registration.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Payments.Wallet.RequiredInRegistration",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsWalletEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsWalletCreditCardEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments wallet credit card is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Payments.Wallet.CreditCard.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsWalletEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsWalletEcheckEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the payments wallet echeck is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Payments.Wallet.eCheck.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsWalletEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsProviderMode",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments provider mode.",
},
PropertyType: "Clarity.Ecommerce.Enums.PaymentProviderMode",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.ProviderMode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.PaymentProviderMode)1",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PaymentsProcess",
ParamDocs: <IParamDocs>{
Summary: "Gets the payments process.",
},
PropertyType: "Clarity.Ecommerce.Enums.PaymentProcessMode",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.Process",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Clarity.Ecommerce.Enums.PaymentProcessMode)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PaymentsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "UseProviderGetWallet",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether to use a provider to get wallet.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.UseProviderGetWallet",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "WalletAccountNumberSerializableAttributeName",
ParamDocs: <IParamDocs>{
Summary: "Gets the name of the wallet account number serializable attribute.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Payments.Wallet.AccountNumberSerializableAttributeName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"EvoAccountNumber",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"UseProviderGetWallet\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the pricing is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Pricing.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoginForPricingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the login for pricing is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Pricing.LoginForPricing.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"PricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "LoginForPricingKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the login for pricing key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Login.LoginForPricing.Key",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.searchCatalog.loginToViewPricing",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginForPricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingMsrpEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the pricing msrp is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Pricing.Msrp.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingReductionEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the pricing reduction is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Pricing.Reduction.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingPriceRulesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the pricing price rules is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Pricing.PriceRules.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingTieredPricingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the pricing tiered pricing is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Pricing.TieredPricing.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingProviderTieredDefaultMarkupRate",
ParamDocs: <IParamDocs>{
Summary: "Gets the pricing provider tiered default markup rate.",
},
PropertyType: "System.Decimal",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Modes.TieredPricingMode.DefaultMarkupRate",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingTieredPricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingProviderTieredDefaultUnitOfMeasure",
ParamDocs: <IParamDocs>{
Summary: "Gets the pricing provider tiered default unit of measure.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Modes.TieredPricingMode.DefaultUnitOfMeasure",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"EACH",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingTieredPricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingProviderTieredDefaultPricePointKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the pricing provider tiered default price point key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Modes.TieredPricingMode.DefaultPricePointKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"WEB",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingTieredPricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingProviderTieredDefaultCurrencyKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the pricing provider tiered default currency key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Modes.TieredPricingMode.DefaultCurrencyKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"USD",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingTieredPricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingFlatWithStoreOverridePricingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the pricing tiered pricing is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Pricing.FlatWithStoreOverridePricingMode.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PricingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PricingProviderFlatWithStoreFranchiseOrBrandOverridePricingModeRequireInventoryToOverride",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the pricing provider flat with store override pricing mode require\
inventory to override.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Modes.FlatWithStoreOverridePricingMode.RequireInventoryToOverride",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProductNotificationsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the product notifications is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Products.Notifications.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProductRestrictionsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the product restrictions is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Products.Restrictions.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProductFutureImportsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the product future imports is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Products.FutureImports.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProductStoredFilesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the product stored files is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Products.StoredFiles.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProductCategoryAttributesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the product category attributes is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Products.CategoryAttributes.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CategoriesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ProfanityFilterEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the profanity filter is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.ProfanityFilter.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseOrdersEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase orders is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.PurchaseOrders.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseOrdersHasIntegratedKeys",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchase orders has integrated keys.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.PurchaseOrders.HasIntegratedKeys",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchaseOrdersEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasingMinMaxEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchasing minimum maximum is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.MinMax.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasingMinMaxAfterEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchasing minimum maximum after is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.MinMax.After.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\", \"PurchasingMinMaxEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasingAvailabilityDatesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchasing availability dates is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.AvailabilityDates.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasingRoleRequiredToPurchaseEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchasing role required to purchase is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.RoleRequiredToPurchase.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasingRoleRequiredToSeeEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchasing role required to see is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.RoleRequiredToSee.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasingDocumentRequiredEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchasing document required is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.DocumentRequired.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasingDocumentRequiredOverrideEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchasing document required override is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.DocumentRequired.Override.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasingDocumentRequiredEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasingMustPurchaseInMultiplesOfEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchasing must purchase in multiples of is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.MustPurchaseInMultiplesOf.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchasingMustPurchaseInMultiplesOfOverrideEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the purchasing must purchase in multiples of override is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.MustPurchaseInMultiplesOf.Override.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"PurchasingMustPurchaseInMultiplesOfEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "PurchaseShippingShowSpecialInstructions",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the special instructions field in shipping is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Purchasing.ShippingShowSpecialInstructions.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CachingRedisEnabled",
ParamDocs: <IParamDocs>{
Summary: "NOTE: This should never be false.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Caching.Redis.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CachingTimeoutTimeSpan",
ParamDocs: <IParamDocs>{
Summary: "Gets the caching timeout time span.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Caching.TimeoutTimeSpan",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"01:00:00",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CachingRedisHostUri",
ParamDocs: <IParamDocs>{
Summary: "Gets URI of the caching redis host.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Caching.Redis.Host.Uri",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"localhost",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CachingRedisEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CachingRedisHostPort",
ParamDocs: <IParamDocs>{
Summary: "Gets the caching redis host port.",
},
PropertyType: "System.Nullable`1[System.Int32]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Caching.Redis.Host.Port",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)6379",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CachingRedisEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CachingRedisUsername",
ParamDocs: <IParamDocs>{
Summary: "Gets the caching redis username.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Caching.Redis.Username",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CachingRedisEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CachingRedisPassword",
ParamDocs: <IParamDocs>{
Summary: "Gets the caching redis password.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Caching.Redis.Password",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CachingRedisEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CachingRedisRequiredSSL",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the caching redis required ssl.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Caching.Redis.RequireSSL",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CachingRedisEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "CachingRedisAbortConnect",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the caching redis abort connect.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Caching.Redis.AbortConnect",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"CachingRedisEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReferralRegistrationsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the referral registrations is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.ReferralRegistrations.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesBasicInfoEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes basic information is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.BasicInfo.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesBasicInfoPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes basic information position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.BasicInfo.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesBasicInfoEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesBasicInfoTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes basic information title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.BasicInfo.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.registration.BasicInformation",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesBasicInfoEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesBasicInfoContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes basic information continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.BasicInfo.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.registration.ContinueToBasicInformation",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesBasicInfoEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesBasicInfoShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes basic information show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.BasicInfo.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesBasicInfoEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "AddressBookRequiredInRegistration",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the address book required in registration.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.AddressBook.RequiredInRegistration",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"AddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesAddressBookEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes address book is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.AddressBook.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesAddressBookPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes address book position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.AddressBook.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)1",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesAddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesAddressBookTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes address book title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.AddressBook.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.common.AddressBook",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesAddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesAddressBookContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes address book continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.AddressBook.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.registration.ContinueToAddressBook",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesAddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesAddressBookShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes address book show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.AddressBook.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesAddressBookEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesWalletEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes wallet is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Wallet.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesWalletPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes wallet position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Wallet.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)2",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesWalletEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesWalletTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes wallet title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Wallet.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.userDashboard2.userDashboard.Wallet",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesWalletEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesWalletContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes wallet continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Wallet.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.registration.ContinueToWallet",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesWalletEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesWalletShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes wallet show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Wallet.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesWalletEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCustomEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes custom is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Custom.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCustomPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes custom position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Custom.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)3",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesCustomEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCustomTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes custom title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Custom.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Custom",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesCustomEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCustomContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes custom continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Custom.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Custom",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesCustomEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCustomShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes custom show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Custom.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesCustomEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesConfirmationEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes confirmation is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Confirmation.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesConfirmationPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes confirmation position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Confirmation.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)4",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesConfirmationTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes confirmation title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Confirmation.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.checkout.checkoutPanels.Confirmation",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesConfirmationContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes confirmation continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.WaConfirmationllet.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.registration.ContinueToConfirmation",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesConfirmationShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes confirmation show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Confirmation.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesConfirmationEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCompletedEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes completed is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Completed.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"LoginEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCompletedPosition",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes completed position.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Completed.Position",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)5",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCompletedTitle",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes completed title.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Completed.Title",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.registration.RegistrationComplete",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCompletedContinueText",
ParamDocs: <IParamDocs>{
Summary: "Gets the registration panes completed continue text.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Completed.ContinueText",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"ui.storefront.memberships.membershipRegistration.completeRegistration",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "RegistrationPanesCompletedShowButton",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the registration panes completed show button.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Registration.Panes.Completed.ShowButton",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"RegistrationPanesCompletedEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReportingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the reporting is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Reporting.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReportingDevExpressEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the DevExpress reporting is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Reporting.DevExpress.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReportingSyncFusionEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the SyncFusion reporting is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Reporting.SyncFusion.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ReviewsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the reviews is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Reviews.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesGroupsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales groups is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesGroups.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SplitShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SalesGroupsHasIntegratedKeys",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sales groups has integrated keys.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SalesGroups.HasIntegratedKeys",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SalesGroupsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SampleRequestsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sample requests is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SampleRequests.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SampleRequestsHasIntegratedKeys",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sample requests has integrated keys.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.SampleRequests.HasIntegratedKeys",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"SampleRequestsEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SampleRequestEnforcesFreeSampleType",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the sample request enforces free sample type.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.SampleRequests.EnforcesFreeSampleType",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerNodeName",
ParamDocs: <IParamDocs>{
Summary: "Gets the name of the scheduler node.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.NodeName",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"CEFSchedulerNode",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerWorkerCount",
ParamDocs: <IParamDocs>{
Summary: "Gets the number of scheduler workers.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.WorkerCount",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)1",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerServerTimeoutInSeconds",
ParamDocs: <IParamDocs>{
Summary: "Gets the scheduler server timeout in seconds.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.ServerTimeoutInSeconds",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)60",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerHeartBeatIntervalInSeconds",
ParamDocs: <IParamDocs>{
Summary: "Gets the scheduler heart beat interval in seconds.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.HeartBeatIntervalInSeconds",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)30",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerSchedulePollingIntervalInSeconds",
ParamDocs: <IParamDocs>{
Summary: "Gets the scheduler schedule polling interval in seconds.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.SchedulePollingIntervalInSeconds",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)30",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerServerCheckIntervalInSeconds",
ParamDocs: <IParamDocs>{
Summary: "Gets the scheduler server check interval in seconds.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.ServerCheckIntervalInSeconds",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)90",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerShutdownTimeout",
ParamDocs: <IParamDocs>{
Summary: "Gets the scheduler shutdown timeout.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.ShutdownTimeout",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)0",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerTimeZone",
ParamDocs: <IParamDocs>{
Summary: "Gets the scheduler time zone.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.TimeZone",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Central Standard Time",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerQueues",
ParamDocs: <IParamDocs>{
Summary: "Gets the scheduler queues.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.Queues",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"DEFAULT",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SchedulerDashboardStatsPollingIntervalInSeconds",
ParamDocs: <IParamDocs>{
Summary: "Gets the scheduler dashboard statistics polling interval in seconds.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Scheduler.Dashboard.StatsPollingIntervalInSeconds",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)5000",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingPackagesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping packages is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.Packages.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingMasterPacksEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping master packs is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.MasterPacks.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingPackagesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingPalletsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping pallets is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.Pallets.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingPackagesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingEventsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping events is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.Events.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingHandlingFeesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping handling fees is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.HandlingFees.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingRatesProductsCanBeFree",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping rates products can be individually free.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.ProductsCanBeFree",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingLeadTimesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping lead times is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.LeadTimes.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping company lead time is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursNormal",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours normal.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.Normal",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"16:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursExpedited",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours expedited.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.Expedited",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"4:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeEnabledMonday",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping company lead time enabled monday.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Monday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursStartHourMonday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours start hour monday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Monday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"08:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledMonday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursEndHourMonday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours end hour monday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Monday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"17:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledMonday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeEnabledTuesday",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping company lead time enabled tuesday.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Tuesday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursStartHourTuesday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours start hour tuesday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Tuesday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"08:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledTuesday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursEndHourTuesday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours end hour tuesday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Tuesday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"17:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledTuesday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeEnabledWednesday",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping company lead time enabled wednesday.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Wednesday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursStartHourWednesday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours start hour wednesday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Wednesday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"08:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledWednesday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursEndHourWednesday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours end hour wednesday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Wednesday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"17:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledWednesday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeEnabledThursday",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping company lead time enabled thursday.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Thursday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursStartHourThursday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours start hour thursday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Thursday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"08:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledThursday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursEndHourThursday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours end hour thursday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Thursday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"17:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledThursday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeEnabledFriday",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping company lead time enabled friday.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Friday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursStartHourFriday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours start hour friday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Friday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"08:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledFriday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursEndHourFriday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours end hour friday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Friday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"17:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledFriday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeEnabledSaturday",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping company lead time enabled saturday.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Saturday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursStartHourSaturday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours start hour saturday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Saturday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"08:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledSaturday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursEndHourSaturday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours end hour saturday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Saturday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"17:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledSaturday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeEnabledSunday",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping company lead time enabled sunday.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.Enabled.Sunday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursStartHourSunday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours start hour sunday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.StartHour.Sunday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"08:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledSunday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingCompanyLeadTimeInBusinessHoursEndHourSunday",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping company lead time in business hours end hour sunday.",
},
PropertyType: "System.TimeSpan",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.CompanyLeadTime.BusinessHours.EndHour.Sunday",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"17:00:00",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingCompanyLeadTimeEnabledSunday\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingRatesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping rates is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.Rates.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingEstimatesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping estimates is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.Rates.Estimator.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingRatesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingRatesFlatEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the shipping rates flat is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Shipping.Rates.Flat.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"ShippingRatesEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingTrackingDayRolling",
ParamDocs: <IParamDocs>{
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Tracking.DayRolling",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingOriginAddressStreet1",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping origin address street 1.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Origin.Street1",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"6805 N Capital of Texas Hwy",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingOriginAddressStreet2",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping origin address street 2.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Origin.Street2",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Suite 312",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingOriginAddressStreet3",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping origin address street 3.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Origin.Street3",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingOriginAddressCity",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping origin address city.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Origin.City",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"Austin",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingOriginAddressPostalCode",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping origin address postal code.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Origin.PostalCode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"78731",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingOriginAddressRegionCode",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping origin address region code.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Origin.RegionCode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"TX",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "ShippingOriginAddressCountryCode",
ParamDocs: <IParamDocs>{
Summary: "Gets the shipping origin address country code.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Shipping.Origin.CountryCode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"USA",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoresEnabled",
ParamDocs: <IParamDocs>{
Summary: "Site-wide activation of stores UI and functionality.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Stores.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "StoresDoRestrictionsByMinMax",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the stores do restrictions by minimum maximum (hard/soft stops).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Carts.Validation.DoStoreRestrictionsByMinMax",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "SendFinalStatementWithFinalPRandPDFInsertsAfterXDays",
ParamDocs: <IParamDocs>{
Summary: "Gets the send final statement with final p random PDF inserts after x coordinate days.",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tasks.SendFinalStatementWithFinalPRandPDFInsertsAfterXDays",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)90",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "MembershipLevelsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Utilizes membership levels for users.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tasks.Subscription.ProductMembershipLevels",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)True",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TaxesEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the taxes is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Taxes.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TaxesAvalaraEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the taxes avalara is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Taxes.Avalara.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TaxesAvalaraCompanyCode",
ParamDocs: <IParamDocs>{
Summary: "Gets the taxes avalara company code.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tax.Avalara.CompanyCode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TaxesAvalaraBusinessIdentificationNo",
ParamDocs: <IParamDocs>{
Summary: "Gets the taxes avalara business identification no.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tax.Avalara.BusinessIdentificationNo",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TaxesAvalaraCurrencyCode",
ParamDocs: <IParamDocs>{
Summary: "Gets the taxes avalara currency code.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tax.Avalara.CurrencyCode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TaxesAvalaraLicenseKey",
ParamDocs: <IParamDocs>{
Summary: "Gets the taxes avalara license key.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tax.Avalara.LicenseKey",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TaxesAvalaraEnableDevelopmentMode",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the taxes avalara enable development mode.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tax.Avalara.EnableDevelopmentMode",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TaxesAvalaraAccountNumber",
ParamDocs: <IParamDocs>{
Summary: "Gets the taxes avalara account number.",
},
PropertyType: "System.Nullable`1[System.Int32]",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tax.Avalara.AccountNumber",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TaxesUseOrigin",
ParamDocs: <IParamDocs>{
Summary: "Gets the taxes use origin.",
},
PropertyType: "System.String",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "Nullable",
ConstructorArguments: [
"(Byte)2",
]
},
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tax.UseOrigin",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(String)null",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TrackingEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the tracking is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Tracking.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "TrackingExpiresAfterXMinutes",
ParamDocs: <IParamDocs>{
Summary: "How long the visit cookies last before expiration (in minutes).",
},
PropertyType: "System.Int32",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Tracking.ExpiresAfterXMinutes",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Int32)120",
]
},
<ICustomAttribute>{
DisplayName: "Depends On",
ConstructorArguments: [
"\"TrackingEnabled\"",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "FacebookPixelServiceEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the facebook pixel service is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Analytics.FacebookPixelService.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "GoogleTagManagerEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the google tag manager is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Analytics.GoogleTagManager.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "VendorsEnabled",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the vendors is enabled.",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.FeatureSet.Vendors.Enabled",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
this.appSettingsDefinitions.push(<IAppSettingDefinition>{
Index: ++index,
Type: "Clarity.Ecommerce.JSConfigs.CEFConfigDictionary, Clarity.Ecommerce.RegistryLoader",
Name: "VendorsDoRestrictionsByMinMax",
ParamDocs: <IParamDocs>{
Summary: "Gets a value indicating whether the vendors do restrictions by minimum maximum (hard/soft stops).",
},
PropertyType: "System.Boolean",
CustomAttributes: [
<ICustomAttribute>{
DisplayName: "App Settings Key",
ConstructorArguments: [
"Clarity.Carts.Validation.DoVendorRestrictionsByMinMax",
]
},
<ICustomAttribute>{
DisplayName: "Default Value",
ConstructorArguments: [
"(Boolean)False",
]
},
],
});
			this.load();
		}
	}

	adminApp.directive("appSettingsWidget", (/*$filter: ng.IFilterService*/): ng.IDirective => ({
		restrict: "EA",
		// templateUrl: $filter("corsLink")("/framework/admin/controls/system/widgets/appSettingsWidget.html", "ui"),
		template: `<!--
// <auto-generated>
// <copyright file="appSettingsWidget.html" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>The AppSettings keys, values and descriptions and the ability to edit them.</summary>
// <remarks>This file was auto-generated by appSettingsWidget.tt in the UI Project</remarks>
// </auto-generated>
-->
<div class="row">
	<div class="col-sm-12">
		<h1 class="inline-block">App Settings Reference</h1>
		<button type="button" class="btn btn-primary pull-right"
			ng-disabled="aswCtrl.viewState.running || !aswCtrl.enableSave"
			ng-click="aswCtrl.save()">
			Save
		</button>
		{{aswCtrl.viewState.errorMessage}}
	</div>
	<div class="col-sm-12">
		<p>
			Each app setting will be listed with it's key, the general kinds of values it should have, and what general
			effects it will have on the system. In some cases, there are known <b>"mutual exclusivities"</b> (settings
			that cannot work together) and <b>"depends on"</b> (settings that must be put together to have a
			functioning feature result. This knowledge-base will take time to build up but will be entered directly
			into the Summary XML documentation tags of the <b>CEFConfigsDictionary.Properties*.cs</b> files and
			transcribed here.
		</p>
	</div>
	<div class="col-sm-12 col-md-6">
		<div class="form-group">
			<label>Quick filter</label>
			<input type="text" class="form-control"
				placeholder="Quick filter..."
				ng-change="aswCtrl.page = 0"
				ng-model="aswCtrl.quickFilter" />
		</div>
	</div>
	<div class="col-sm-6 col-md-3">
		<div class="form-group">
			<label>Page size</label>
			<select class="form-control custom-select"
				ng-change="aswCtrl.page = 0"
				ng-options="o as o for o in [8,16,32,64,128]"
				ng-model="aswCtrl.pageSize">
			</select>
		</div>
	</div>
	<div class="col-sm-6 col-md-3">
		<div class="form-group">
			<label>Page</label>
			<select class="form-control custom-select"
				ng-model="aswCtrl.page"
				ng-options="o as (o+1) for o in aswCtrl.pages">
			</select>
		</div>
	</div>
	<div class="col-sm-12" ng-if="!aswCtrl.viewState.loading">
		<div class="table-responsive">
			<table class="table table-hover table-striped table-condensed">
				<thead>
					<tr>
						<th>#</th>
						<th>Name / Key / Summary / Remarks / Example</th>
						<th>Default / Current Value / Property Type</th>
						<!--<th>Custom Attributes</th>-->
					</tr>
				</thead>
				<tbody>
					<tr ng-repeat="asd in aswCtrl.appSettingsDefinitions
									| filter: { $: aswCtrl.quickFilter }
									| orderBy: ['CustomAttributes']
									| limitTo: aswCtrl.pageSize
											 : (aswCtrl.page * aswCtrl.pageSize)">
						<td>
							<span ng-bind="asd.Index || '-'" class="block"></span>
							<span ng-repeat="ca in asd.CustomAttributes | filter: { $: 'Unused' }"
								class="block">
								<b>Unused</b>
							</span>
						</td>
						<td>
							<span class="block"><b>Name:</b> <span ng-bind="asd.Name || '-'"></span></span>
							<span ng-repeat="ca in asd.CustomAttributes | filter: { $: 'App Settings Key' }"
								class="block">
								<b>Key:</b> <span ng-repeat="cca in ca.ConstructorArguments" ng-bind="cca"></span>
						 	</span>
							<span ng-repeat="ca in asd.CustomAttributes | filter: { $: 'Depends On' }"
								class="block">
								<b>Depends On:</b> <span ng-repeat="cca in ca.ConstructorArguments" ng-bind="cca"></span>
							</span>
							<span ng-repeat="ca in asd.CustomAttributes | filter: { $: 'Mutually Exclusive With' }"
								class="block">
								<b>Mutually Exclusive With:</b> <span ng-repeat="cca in ca.ConstructorArguments" ng-bind="cca"></span>
							</span>
							<span class="block" ng-if="asd.ParamDocs.Summary"><b>Summary:</b> <span ng-bind="asd.ParamDocs.Summary"></span></span>
							<span class="block" ng-if="asd.ParamDocs.Remarks"><b>Remarks:</b> <span ng-bind="asd.ParamDocs.Remarks"></span></span>
							<span class="block" ng-if="asd.ParamDocs.Example"><b>Example:</b> <span ng-bind="asd.ParamDocs.Example"></span></span>
						</td>
						<td>
							<span ng-repeat="(a, asvType) in aswCtrl.appSettingsValues"
								class="block">
								<div ng-switch="asd.PropertyType">
									<div ng-switch-when="System.Boolean">
										<input type="checkbox"
											ng-change="aswCtrl.pushToUpdates(asd.Type, asd.Name, asvType[asd.Name])"
											ng-model="asvType[asd.Name]" />
									</div>
									<div ng-switch-when="System.Decimal">
										<input type="number" class="form-control"
											step="0.0001"
											ng-change="aswCtrl.pushToUpdates(asd.Type, asd.Name, asvType[asd.Name])"
											ng-model="asvType[asd.Name]" />
									</div>
									<div ng-switch-when="System.Double">
										<input type="number" class="form-control"
											step="0.0001"
											ng-change="aswCtrl.pushToUpdates(asd.Type, asd.Name, asvType[asd.Name])"
											ng-model="asvType[asd.Name]" />
									</div>
									<div ng-switch-when="System.Int32">
										<input type="number" class="form-control"
											step="1"
											ng-change="aswCtrl.pushToUpdates(asd.Type, asd.Name, asvType[asd.Name])"
											ng-model="asvType[asd.Name]" />
									</div>
									<div ng-switch-default>
										<input type="text" class="form-control"
											ng-change="aswCtrl.pushToUpdates(asd.Type, asd.Name, asvType[asd.Name])"
											ng-model="asvType[asd.Name]" />
									</div>
								</div>
							</span>
							<span ng-repeat="ca in asd.CustomAttributes | filter: { $: 'Default Value' }"
								class="block">
								<b>Default:</b> <span ng-repeat="cca in ca.ConstructorArguments" ng-bind="cca.replace('Clarity.Ecommerce.Enums', 'Enums')"></span>
							</span>
							<span><b>Property Type:</b>&nbsp;<span ng-bind="aswCtrl.cleanPropertyType(asd.PropertyType) || '-'"></span><!--
							--><span ng-repeat="ca in asd.CustomAttributes | filter: { $: 'Nullable' }">?</span></span>
							<span ng-repeat="ca in asd.CustomAttributes | filter: { $: 'Split On' }"
								class="block">
								<b>Split On:</b> <span ng-repeat="cca in ca.ConstructorArguments" ng-bind="cca"></span>
							</span>
						</td>
						<!--
						<td>
							<span ng-repeat="ca in asd.CustomAttributes"
								ng-if="ca.DisplayName != 'App Settings Key' && ca.DisplayName != 'Nullable' && ca.DisplayName != 'Default Value' && ca.DisplayName != 'Depends On' && ca.DisplayName != 'Split On' && ca.DisplayName != 'Mutually Exclusive With' && ca.DisplayName != 'Unused'"
								class="block">
								<b ng-bind="ca.DisplayName"></b>
								<span ng-repeat="cca in ca.ConstructorArguments" ng-bind="cca"></span>
							</span>
						</td>
						-->
					</tr>
				</tbody>
			</table>
		</div>
		<!--
		<table ng-repeat="(a, asvType) in aswCtrl.appSettingsValues | limitTo: 3 : 0"
			class="table table-hover table-striped table-condensed">
			<thead><tr><th colspan="2"><b ng-bind="a"></b></th></tr></thead>
			<tbody>
				<tr ng-repeat="(b, value) in asvType | limitTo: 3 : 0">
					<td><b ng-bind="b"></b></td>
					<td ng-bind="value"></td>
				</tr>
			</tbody>
		</table>
		-->
	</div>
</div>
`,
		controller: AppSettingsWidgetController,
		controllerAs: "aswCtrl"
	}));
}
