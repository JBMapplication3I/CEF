/**
 * @file framework/store/stores/storeHours.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Date Input Form Group class
 */
module cef.store.stores {
    class StoreHoursController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        store: api.StoreModel;
        storeMilitaryHours = {
            OperatingHoursMondayStart: 0,
            OperatingHoursMondayEnd: 0,
            OperatingHoursTuesdayStart: 0,
            OperatingHoursTuesdayEnd: 0,
            OperatingHoursWednesdayStart: 0,
            OperatingHoursWednesdayEnd: 0,
            OperatingHoursThursdayStart: 0,
            OperatingHoursThursdayEnd: 0,
            OperatingHoursFridayStart: 0,
            OperatingHoursFridayEnd: 0,
            OperatingHoursSaturdayStart: 0,
            OperatingHoursSaturdayEnd: 0,
            OperatingHoursSundayStart: 0,
            OperatingHoursSundayEnd: 0,
        };
        // Properties
        // Special Accessors
        // Store Hours
        // Functions
        private fixStoreHours() {
            let changeDecimalToMilitary = (decimal: number) => {
                let decimalString = this.$filter("zeroPadNumber")(decimal * 100, 4); // 0350, 1000
                decimalString = decimalString.substring(0, 2) + (parseInt(decimalString.substring(2)) * 0.6).toString(); // 0330, 1000
                decimalString = decimalString.length === 3 ? decimalString + "0" : decimalString; // 100 => 1000
                return parseInt(decimalString);
            }
            if (this.store.OperatingHoursMondayStart) {
                this.storeMilitaryHours.OperatingHoursMondayStart = changeDecimalToMilitary(this.store.OperatingHoursMondayStart);
                this.storeMilitaryHours.OperatingHoursMondayEnd = changeDecimalToMilitary(this.store.OperatingHoursMondayEnd);
            }
            if (this.store.OperatingHoursTuesdayStart) {
                this.storeMilitaryHours.OperatingHoursTuesdayStart = changeDecimalToMilitary(this.store.OperatingHoursTuesdayStart);
                this.storeMilitaryHours.OperatingHoursTuesdayEnd = changeDecimalToMilitary(this.store.OperatingHoursTuesdayEnd);
            }
            if (this.store.OperatingHoursWednesdayStart) {
                this.storeMilitaryHours.OperatingHoursWednesdayStart = changeDecimalToMilitary(this.store.OperatingHoursWednesdayStart);
                this.storeMilitaryHours.OperatingHoursWednesdayEnd = changeDecimalToMilitary(this.store.OperatingHoursWednesdayEnd);
            }
            if (this.store.OperatingHoursThursdayStart) {
                this.storeMilitaryHours.OperatingHoursThursdayStart = changeDecimalToMilitary(this.store.OperatingHoursThursdayStart);
                this.storeMilitaryHours.OperatingHoursThursdayEnd = changeDecimalToMilitary(this.store.OperatingHoursThursdayEnd);
            }
            if (this.store.OperatingHoursFridayStart) {
                this.storeMilitaryHours.OperatingHoursFridayStart = changeDecimalToMilitary(this.store.OperatingHoursFridayStart);
                this.storeMilitaryHours.OperatingHoursFridayEnd = changeDecimalToMilitary(this.store.OperatingHoursFridayEnd);
            }
            if (this.store.OperatingHoursSaturdayStart) {
                this.storeMilitaryHours.OperatingHoursSaturdayStart = changeDecimalToMilitary(this.store.OperatingHoursSaturdayStart);
                this.storeMilitaryHours.OperatingHoursSaturdayEnd = changeDecimalToMilitary(this.store.OperatingHoursSaturdayEnd);
            }
            if (this.store.OperatingHoursSundayStart) {
                this.storeMilitaryHours.OperatingHoursSundayStart = changeDecimalToMilitary(this.store.OperatingHoursSundayStart);
                this.storeMilitaryHours.OperatingHoursSundayEnd = changeDecimalToMilitary(this.store.OperatingHoursSundayEnd);
            }
        }
        // Events
        // <None at this level>
        // Constructor
        constructor(
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
            if (this.store) {
                this.fixStoreHours();
            }
        }
    }

    cefApp.directive("cefStoreHoursWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            store: "=?",
        },
        templateUrl: $filter("corsLink")("/framework/store/stores/storeHours.html", "ui"),
        controller: StoreHoursController,
        controllerAs: "storeHoursController",
        bindToController: true
    }));
}
