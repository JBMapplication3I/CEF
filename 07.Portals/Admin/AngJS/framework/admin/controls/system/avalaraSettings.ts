// <copyright file="accountDetail.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>account detail class</summary>
module cef.admin {
    class AvalaraDetailController extends core.TemplatedControllerBase {
        // Properties
        accountNumber: string;
        licenseKey: string;
        serviceUrl: string;
        companyCode: string;
        taxServiceEnabled: boolean;
        addressServiceEnabled: boolean;
        loggingEnabled: boolean;
        documentCommitingEnabled: boolean;
        settings: api.SettingModel[] = [];
        countries: api.CountryModel[] = [];
        selectedCountries: api.CountryModel[] = [];
        selectOptions = {};
        adminURL: string;
        // Constructor
        constructor(
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory) {
            super(cefConfig);
            this.cvApi.geography.GetCountries({ Active: true, AsListing: true }).then(r => this.countries = r.data.Results);
            this.cvApi.structure.GetSettingsByGroupName("Avalara").then(r => {
                this.parseSettings(r);
                this.adminURL = this.serviceUrl.indexOf("development") >= 0
                    ? "https://admin-development.avalara.net/"
                    : "https://admin-avatax.avalara.net/";
            });
        }
        // Functions
        parseSettings(response: ng.IHttpPromiseCallbackArg<api.SettingModel[]>): void {
            this.settings = response.data;
            let val: api.SettingModel = _.find(response.data, { "TypeName": "AccountNumber" });
            this.accountNumber = val ? val.Value : "";
            val = _.find(response.data, { "TypeName": "LicenseKey" });
            this.licenseKey = val ? val.Value : "";
            val = _.find(response.data, { "TypeName": "ServiceURL" });
            this.serviceUrl = val ? val.Value : "";
            val = _.find(response.data, { "TypeName": "CompanyCode" });
            this.companyCode = val ? val.Value : "";
            val = _.find(response.data, { "TypeName": "TaxServiceEnabled" });
            this.taxServiceEnabled = val ? val.Value === "true" : false;
            val = _.find(response.data, { "TypeName": "AddressServiceEnabled" });
            this.addressServiceEnabled = val ? val.Value === "true" : false;
            val = _.find(response.data, { "TypeName": "LoggingEnabled" });
            this.loggingEnabled = val ? val.Value === "true" : false;
            val = _.find(response.data, { "TypeName": "DocumentCommitingEnabled" });
            this.documentCommitingEnabled = val ? val.Value === "true" : false;
            val = _.find(response.data, { "TypeName": "AddressServiceCountries" });
            if (val) {
                const countries = val.Value.split("|");
                countries.forEach(item => this.selectedCountries.push(_.find(this.countries, { 'Code': item })));
            }
        }

        save(): void {
            var promises: ng.IPromise<any>[] = [];
            this.settings.forEach(item => {
                switch (item.TypeName) {
                    case "AccountNumber": {
                        item.Value = this.accountNumber;
                        promises.push(this.cvApi.structure.UpdateSetting(item));
                       break;
                    }
                    case "LicenseKey": {
                        item.Value = this.licenseKey;
                        promises.push(this.cvApi.structure.UpdateSetting(item));
                        break;
                    }
                    case "ServiceURL": {
                        item.Value = this.serviceUrl;
                        promises.push(this.cvApi.structure.UpdateSetting(item));
                        break;
                    }
                    case "CompanyCode": {
                        item.Value = this.companyCode;
                        promises.push(this.cvApi.structure.UpdateSetting(item));
                        break;
                    }
                    case "TaxServiceEnabled": {
                        item.Value = this.taxServiceEnabled.toString();
                        promises.push(this.cvApi.structure.UpdateSetting(item));
                        break;
                    }
                    case "AddressServiceEnabled": {
                        item.Value = this.addressServiceEnabled.toString();
                        promises.push(this.cvApi.structure.UpdateSetting(item));
                        break;
                    }
                    case "LoggingEnabled": {
                        item.Value = this.loggingEnabled.toString();
                        promises.push(this.cvApi.structure.UpdateSetting(item));
                        break;
                    }
                    case "DocumentCommitingEnabled": {
                        item.Value = this.documentCommitingEnabled.toString();
                        promises.push(this.cvApi.structure.UpdateSetting(item));
                        break;
                    }
                    case "AddressServiceCountries": {
                        item.Value = "";
                        this.selectedCountries.forEach(c => { item.Value += c.Code + "|"; });
                        item.Value = item.Value.replace(/\|$/, "");
                        promises.push(this.cvApi.structure.UpdateSetting(item));
                        break;
                    }
                    default: {
                        break;
                    }
                }
            });
            this.$q.all(promises)
                .then(() => this.cvApi.structure.GetSettingsByGroupName("Avalara")
                    .then(response => this.parseSettings(response)));
        }

        testService(): void {
            this.setRunning();
            if (!this.accountNumber) {
                this.cvMessageModalFactory(
                    this.$translate("ui.admin.controls.system.avalaraSettings.Errors.ConnectionFailed.MissingAccountNumber"))
                        .finally(() => this.finishRunning(true));
                return;
            }
            if (!this.licenseKey) {
                this.cvMessageModalFactory(
                    this.$translate("ui.admin.controls.system.avalaraSettings.Errors.ConnectionFailed.MissingLicenseKey"))
                        .finally(() => this.finishRunning(true));
                return;
            }
            if (!this.serviceUrl) {
                this.cvMessageModalFactory(
                    this.$translate("ui.admin.controls.system.avalaraSettings.Errors.ConnectionFailed.MissingServiceURL"))
                        .finally(() => this.finishRunning(true));
                return;
            }
            if (!this.companyCode) {
                this.cvMessageModalFactory(
                    this.$translate("ui.admin.controls.system.avalaraSettings.Errors.ConnectionFailed.MissingCompanyCode"))
                        .finally(() => this.finishRunning(true));
                return;
            }
            this.cvApi.tax.TestConnection()
                .then(r => {
                    if (r.data.ActionSucceeded) {
                        this.cvMessageModalFactory(
                            this.$translate("ui.admin.controls.system.avalaraSettings.ConnectionSuccessful"))
                                .then(() => this.finishRunning());
                        return;
                    }
                    this.cvMessageModalFactory(
                            this.$translate.instant("ui.admin.controls.system.avalaraSettings.Errors.ConnectionFailed.Colon") + " " + r.data.Messages[0])
                        .then(() => this.finishRunning(true, null, r.data.Messages));
                }).catch(reason => this.cvMessageModalFactory(
                    this.$translate("ui.admin.controls.system.avalaraSettings.Errors.ConnectionFailed.CheckLogs"))
                        .then(() => this.finishRunning(true, reason)));
        }
    }

    adminApp.directive("avalaraDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/avalaraSettings.html", "ui"),
        controller: AvalaraDetailController,
        controllerAs: "avalaraDetailCtrl"
    }));
}
