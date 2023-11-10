module cef.store.memberships {
    export class MembershipLevelSelectorController extends core.TemplatedControllerBase {
        templateUrl: string;
        transclude: boolean;
        sellerType: string;
        hasError = false;
        errorMessage: string;
        email: string;
        token: string;
        showDefaultPricing = true;
        membershipTypeName: string;
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                readonly $location: ng.ILocationService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            if (this.$scope.membershipTypeName) {
                this.membershipTypeName = this.$scope.membershipTypeName;
                this.showDefaultPricing = this.membershipTypeName.toLowerCase() === "default";
            } else { this.membershipTypeName = null; }
            const email = $location.search()["email"];
            if (!email) {
                // Must provide the token from the invitation
                this.hasError = true;
                this.errorMessage = "Please use the link from your invitation email: Invalid token";
                return; // Must select a Seller Type
            }
            this.hasError = false;
            this.email = email;
            const type = $location.search()["type"];
            if (!type) {
                // Must provide the token from the invitation
                this.hasError = true;
                this.errorMessage = "Please use the link from your invitation email: Invalid seller type";
                return; // Must select a Seller Type
            }
            this.hasError = false;
            this.sellerType = type;
            const token = $location.search()["vtoken"];
            if (!token) {
                // Must provide the token from the invitation
                this.hasError = true;
                this.errorMessage = "Please use the link from your invitation email: Invalid token";
                return;
            }
            this.hasError = false;
            this.token = token;
            const mtype = $location.search()["mtype"];
            if (!mtype && !this.membershipTypeName) {
                // Must provide the membership type name from the invitation
                this.hasError = true;
                this.errorMessage = "Please use the link from your invitation email: Invalid Membership Type";
                return;
            }
            this.hasError = false;
            if (mtype) {
                this.membershipTypeName = mtype;
                this.showDefaultPricing = this.membershipTypeName.toLowerCase() === "default";
            }
            cvApi.authentication.ValidateInvitation({ Email: this.email, Token: this.token })
                .then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.hasError = true;
                        this.errorMessage = "Please use the link from your invitation email: We could not validate your invitation";
                        return;
                    }
                    this.hasError = false;
                }).catch(results => {
                    this.hasError = true;
                    this.errorMessage = "Please use the link from your invitation email: We could not validate your invitation";
                });
        }
    }

    cefApp.directive("cefMembershipLevelSelector", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { membershipTypeName: "@" },
        templateUrl: $filter("corsLink")("/framework/store/memberships/membershipLevelSelector.html", "ui"),
        controller: MembershipLevelSelectorController,
        controllerAs: "membershipLevelSelectorCtrl"
    }));
}
