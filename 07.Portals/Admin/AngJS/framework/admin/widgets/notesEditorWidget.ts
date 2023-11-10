module cef.admin {
    class NoteEditorWidgetController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        noteTypeName: string;
        notes: api.NoteModel[];
        // Properties
        comment: string;
        // Function
        add(): void {
            if (!this.comment) { return; }
            if (!this.notes) {
                this.notes = [];
            }
            this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                this.notes.push(<api.NoteModel>{
                    Active: true,
                    CustomKey: null,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    CreatedByUserID: user.userID,
                    CreatedByUserUsername: user.username,
                    CreatedByUserContactFirstName: user.Contact.FirstName,
                    CreatedByUserContactLastName: user.Contact.LastName,
                    TypeID: 0,
                    Type: <api.NoteTypeModel>{
                        Active: true,
                        CustomKey: this.noteTypeName,
                        CreatedDate: new Date(),
                        UpdatedDate: null,
                        Name: this.noteTypeName,
                        Description: null,
                        IsPublic: false,
                        IsCustomer: false
                    },
                    Note1: this.comment
                });
                this.comment = "";
            });
            this.forms["notes"].$setDirty();
        }
        remove(): void {
            // Not implemented
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefNotesEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            notes: "=",
            noteTypeName: "="
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/notesEditorWidget.html", "ui"),
        controller: NoteEditorWidgetController,
        controllerAs: "noteEditorWidgetCtrl",
        bindToController: true
    }));
}
