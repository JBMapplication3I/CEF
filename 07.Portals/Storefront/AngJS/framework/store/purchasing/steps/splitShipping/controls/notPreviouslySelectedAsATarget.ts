module cef.store.purchasing.steps.splitShipping.controls {
    cefApp.filter("notPreviouslySelectedAsATarget", () =>
        (sourceArr: api.ContactModel[], thisTarget: api.SalesItemTargetBaseModel, targets: api.SalesItemTargetBaseModel[]) => {
            if (!sourceArr || !angular.isArray(sourceArr)
                || !targets || !angular.isArray(targets)) {
                return sourceArr;
            }
            var selectedIDs = targets
                .filter(x => x.DestinationContact && x.DestinationContact.ID)
                .map(x => x.DestinationContact.ID);
            return sourceArr
                .filter(x => thisTarget.DestinationContact
                             && (thisTarget.DestinationContact.ID == x.ID
                                 || thisTarget.DestinationContact.ID === -1) // -1 is New Address, allow multiple
                          || selectedIDs.indexOf(x.ID) === -1);
        }
    );
}
