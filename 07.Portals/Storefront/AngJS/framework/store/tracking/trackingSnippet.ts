module cef.store.tracking {
    class TrackingSnippetController extends core.TemplatedControllerBase {
        // Properties
        visitorID: number;
        visitID: number;
        dto: api.EndUserEventModel;
        private readonly strings = {
            visitID: "cef_visit_id",
            visitKey: "cef_visit_key",
            visitorID: "cef_visitor_id",
            visitorKey: "cef_visitor_key"
        };
        // Functions
        getCookiesOptions(allowExpire: boolean): ng.cookies.ICookiesOptions {
            const options = <ng.cookies.ICookiesOptions>{
                // expires: never
                path: "/",
                domain: this.cefConfig.useSubDomainForCookies || !this.subdomain
                    ? this.$location.host()
                    : this.$location.host().replace(this.subdomain, "")
            };
            if (allowExpire) {
                const in120 = new Date();
                in120.setMinutes(in120.getMinutes()
                    + (this.cefConfig.featureSet.tracking.expires));
                options.expires = in120;
            }
            return options;
        }
        load(): void {
            if (!this.cefConfig.featureSet.tracking.enabled) {
                return;
            }
            if (!this.$cookies.get(this.strings.visitorKey)) {
                this.$cookies.put(this.strings.visitorKey, api.Guid.newGuid(), this.getCookiesOptions(true));
            }
            if (!this.$cookies.get(this.strings.visitKey)) {
                this.$cookies.put(this.strings.visitKey, api.Guid.newGuid(), this.getCookiesOptions(true));
            }
            const visitorID = this.$cookies.get(this.strings.visitorID);
            if (visitorID) { this.visitorID = Number(visitorID); }
            const visitID = this.$cookies.get(this.strings.visitID);
            if (visitID) { this.visitID = Number(visitID); }
            // Check for visitor and visit, if exists then reference ID
            this.dto = <api.EndUserEventModel>{
                PageView: this.createPageViewModel(),
                PageViewEvent: this.createPageViewEventModel()
            };
            if (this.visitorID) {
                this.dto.VisitorID = this.visitorID;
            } else {
                this.dto.Visitor = this.upsertVisitorModel();
            }
            if (this.visitID) {
                this.dto.VisitID = this.visitID;
            } else {
                this.dto.Visit = this.upsertVisitModel();
            }
            this.dto.Event = this.upsertEventModel();
            this.cvApi.tracking.CreateFullPageViewEvent(this.dto).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.consoleLog("Failed to store tracking data for page view");
                    return;
                }
                this.visitorID = r.data.Result.VisitorID;
                this.$cookies.put(this.strings.visitorID, this.visitorID.toString(), this.getCookiesOptions(false));
                this.visitID = r.data.Result.VisitID;
                this.$cookies.put(this.strings.visitID, this.visitID.toString(), this.getCookiesOptions(false));
            }).catch(reason => this.consoleLog(reason));
        }
        upsertVisitorModel(): api.VisitorModel {
            return <api.VisitorModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                CustomKey: this.$cookies.get(this.strings.visitorKey),
                StatusID: 1,
                TypeID: 1
            };
        }
        upsertVisitModel(): api.VisitModel {
            return <api.VisitModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                CustomKey: this.$cookies.get(this.strings.visitKey),
                StatusID: 1,
                TypeID: 1,
                VisitorID: this.visitorID || null,
            };
        }
        upsertEventModel(): api.EventModel {
            return <api.EventModel>{
                Active: true,
                CreatedDate: new Date(),
                CustomKey: "Page View",
                Name: "Page View",
                StatusID: 1,
                TypeID: 1,
                VisitID: this.visitID || null,
                VisitorID: this.visitorID || null
            };
        }
        createPageViewModel(): api.PageViewModel {
            return <api.PageViewModel>{
                Active: true,
                CreatedDate: new Date(),
                Name: document.title,
                StatusID: 1,
                TypeID: 1,
                VisitorID: this.visitorID || null,
                URI: window.location.href,
            };
        }
        createPageViewEventModel(): api.PageViewEventModel {
            return <api.PageViewEventModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                Name: "Page View Event",
                StatusID: 1,
                TypeID: 1,
                EventID: null,
                PageViewID: null
            };
        }
        // Constructor
        constructor(
                private readonly $location: ng.ILocationService,
                private readonly $cookies: ng.cookies.ICookiesService,
                protected readonly cefConfig: core.CefConfig,
                private readonly subdomain: string,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefTrackingSnippet", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/tracking/trackingSnippet.html", "ui"),
        controller: TrackingSnippetController,
        controllerAs: "trackingSnippetCtrl"
    }));
}
