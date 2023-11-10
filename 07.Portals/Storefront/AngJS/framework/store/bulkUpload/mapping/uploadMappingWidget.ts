module cef.store.widgets.uploadMapping {
    cefApp.directive("cefUploadMappingWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            mappingObject: "=",
            uploadType: "@",
            master: "=",
            uploadCallback: "=?",
            text: "=?",
            allowMultiple: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/widgets/uploadMapping/uploadMappingWidget.html", "ui"),
        controller($scope: ng.IScope) {
            $scope.$watch(this.data, (newVal, oldVal) => {
                if (newVal != oldVal) {
                    this.mappingObject.FileName = newVal;
                }
            });
        },
        controllerAs: "cefUploadMappingWidgetCtrl",
        bindToController: true
    }));
}
