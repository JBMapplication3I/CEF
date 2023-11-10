module cef.store.memberships {
    export class MembershipSellerTypeSelectorController extends core.TemplatedControllerBase {
        // ITemplateController Properties
        templateUrl: string;
        transclude: boolean;
        //
        sellerType: string;
        hasError = false;
        errorMessage: string;
        email: string;
        token: string;
        showTypeSelector = false;
        membershipTypeName = "default";

        constructor(
                private readonly $scope: ng.IScope,
                private readonly $location: ng.ILocationService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            if (this.$scope.membershipTypeName) {
                this.membershipTypeName = this.$scope.membershipTypeName;
                this.showTypeSelector = true;
            }
            const email = $location.search()["email"];
            if (!email) {
                // Must provide the email address from the invitation
                this.hasError = true;
                this.errorMessage = "Please use the link from your invitation email: Invalid Email";
                return;
            }
            this.email = email;
            this.hasError = false;
            const token = $location.search()["vtoken"];
            if (!token) {
                // Must provide the token from the invitation
                this.hasError = true;
                this.errorMessage = "Please use the link from your invitation email: Invalid token";
                return;
            }
            this.hasError = false;
            this.token = token;
            const membershipTypeName = $location.search()["typeName"];
            if (typeof membershipTypeName !== "undefined" && membershipTypeName !== null) {
                this.membershipTypeName = membershipTypeName;
                this.showTypeSelector = membershipTypeName.toLowerCase() === "directory";
            }
            cvApi.authentication.ValidateInvitation({ Email: this.email, Token: this.token })
                .then(result => {
                    if (!result || !result.data || !result.data.ActionSucceeded) {
                        ////consoleLog(result);
                        this.hasError = true;
                        this.errorMessage = "Please use the link from your invitation email: We could not validate your invitation";
                        return;
                    }
                    this.hasError = false;
                }).catch(() => {
                    ////consoleLog(results);
                    this.hasError = true;
                    this.errorMessage = "Please use the link from your invitation email: We could not validate your invitation";
                });
        }

        setMembershipType = (membershipTypeName) => {
            this.membershipTypeName = membershipTypeName;
            this.showTypeSelector = true;
        }
    }

    cefApp.directive("cefMembershipSellerTypeSelector", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { membershipTypeName: "@" },
        templateUrl: $filter("corsLink")("/framework/store/memberships/membershipSellerTypeSelector.html", "ui"),
        controller: MembershipSellerTypeSelectorController,
        controllerAs: "membershipSellerTypeSelectorCtrl"
    }));
}
