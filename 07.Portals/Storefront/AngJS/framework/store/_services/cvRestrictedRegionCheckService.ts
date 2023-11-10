module cef.store.services {
    export interface IRestrictedRegionCheckService {
        open(): void;
        validate(data: api.ContactModel): ng.IPromise<boolean>;
        triggerModal(data: api.ContactModel, type?: boolean): void;
    }

    export class RestrictedRegionCheckService implements IRestrictedRegionCheckService {
        // Properties
        title: string;
        message: string;
        buttonText: string;
        size: string;
        checkoutRestriction: boolean;
        viewstate = { restricted: false };
        // Functions
        open(): void {
            const modal = <ng.ui.bootstrap.IModalSettings>{
                templateUrl: this.$filter("corsLink")("/framework/store/widgets/regionCheck/restrictedRegionModal.html", "ui"),
                size: this.size,
                bindToController: true
            };
            if (this.viewstate && this.viewstate.restricted) {
                modal.backdrop = "static";
                modal.keyboard = false;
            }
            this.$uibModal.open(modal);
        };

        triggerModal(data: api.ContactModel, type?: boolean): void {
            if (type) { this.viewstate.restricted = true };
            this.open();
        }

        validate(data: api.ContactModel): ng.IPromise<boolean> {
            if (!data || !data.Address) {
                return this.$q.resolve(false);
            }
            // True = Blocked, False = NotBlocked
            return this.cvApi.geography.RestrictedRegionCheck({
                CountryID: data.Address.CountryID,
                Code: null,
                RegionID: data.Address.RegionID
            }).then(r => r.data.ActionSucceeded);
        }
        // Constructor
        constructor(
            private readonly cvApi: api.ICEFAPI,
            private readonly $filter: ng.IFilterService,
            private readonly $uibModal: ng.ui.bootstrap.IModalService,
            private readonly $q: ng.IQService,
            private readonly cvUserLocationService: IUserLocationService) { }
    }
}
