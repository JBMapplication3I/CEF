/**
 * @file framework/store/ads/adDisplay.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Ad display class.
 */
module cef.store.ads {
    class AdDisplayController extends core.TemplatedControllerBase {
        // Bound By Properties
        get zoneName(): string { return this._zoneName; }
        set zoneName(newValue: string) { this._zoneName = newValue; }
        get zoneId(): number { return this._zoneId; }
        set zoneId(newValue: number) { this._zoneId = newValue; this.load(); }
        // Properties
        private _zoneName: string;
        private _zoneId: number;
        ad: api.AdModel;
        adZone: api.AdZoneModel;
        adZones: Array<api.AdZoneModel>;
        adZonesWithImages: Array<api.AdZoneModel>;
        adStore: api.AdStoreModel;
        // adSearch: api.AdSearchModel;
        zone: api.ZoneModel;
        adStyle: { [attribute: string]: string|number };
        adImageFileName: string;
        hasAd = false;
        loopCount = 0;
        Classname = "ad-container";
        Target = "_blank";
        adZoneSearch: api.AdZoneSearchModel = <api.AdZoneSearchModel>{
            MasterID: null,
            SlaveID: null,
            Active: true,
            AsListing: true
        };
        // Functions
        load(): void {
            if (angular.isUndefined(this.zoneId) || this.zoneId === null) { return; }
            this.adZoneSearch.SlaveID = this.zoneId;
            this.cvApi.advertising.GetAdZones(this.adZoneSearch).then(r => {
                this.adZones = r.data.Results;
                this.loopCount = 0;
                this.getAdWithImages();
                this.cvApi.advertising.GetZoneByID(this.zoneId).then(r2 => {
                    this.zone = r2.data;
                    this.adStyle = {
                        height: this.zone.Height,
                        width: this.zone.Width
                    }
                }).catch(re2 => console.warn(re2));
            }).catch(re => console.warn(re));
        }
        getAdWithImages(): void {
            // TODO@ME: Adjust logic to enable more robust selection of ad to load
            var index = Math.floor(Math.random() * this.adZones.length);
            this.adZone = this.adZones[index];
            this.cvApi.advertising.GetAdByID(this.adZone.MasterID).then(r => {
                this.ad = r.data;
                if (this.ad.Images.length > 0 || this.loopCount >= 10) {
                    return;
                }
                this.loopCount++;
                this.adZones.splice(index, 1);
                this.getAdWithImages();
            }).catch(re => this.consoleLog(re));
        }
        // Constructors
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefAdDisplay", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { zoneName: "@?", zoneId: "@" },
        templateUrl: $filter("corsLink")("/framework/store/ads/adDisplay.html", "ui"),
        controller: AdDisplayController,
        controllerAs: "adDisplayCtrl",
        bindToController: true
    }));
}
