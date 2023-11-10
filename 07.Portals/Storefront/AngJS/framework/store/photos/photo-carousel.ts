module cef.store.photos {
    cefApp.directive("cefPhotoCarousel", ($controller, $filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        replace: true,
        templateUrl: $filter("corsLink")("/framework/store/photos/photo-carousel.html", "ui"),
        scope: {
            interval: "=",
            noTransition: "=",
            noPause: "=",
            noWrap: "&",
            images: "=",
            imagesNew: "=",
            imageDirectory: "=",
            showIndicators: "=",
            showThumbnails: "=",
            showModal: "=",
            mainImageResize: "=",
            // Example: width:366,height:366,mode:'crop'
            thumbImageResize: "=",
            syncIndex: "=",
            useNew: "=",
        },
        controller($scope, $element: ng.IAugmentedJQuery, $interval: ng.IIntervalService, $animate: ng.IAnimateProvider, $compile: ng.ICompileService, $httpParamSerializer, $uibModal: ng.ui.bootstrap.IModalService) {
            const ctrl = $controller("CarouselController", { $scope, $element, $interval, $animate });
            const newCtrl = (Object as any).assign(ctrl, {
                thumbArr: (arr = [], num = 0) => arr.slice(num - 1).concat(arr.slice(num)),
                thumbnails: [],
                thumbLimit: 5, // Number of thumbnails to display at once. Already built custom CSS is required to have 5 thumbnails
                rotate: function (array, times) {
                    if (array.length >= this.slides.length + 1) array.shift();
                    while (times--) {
                        array.push(array.shift());
                    }
                    array.unshift(array[array.length - 1]);
                },
                nextThumb: function () {
                    this.rotate(this.thumbnails, 1);
                },
                prevThumb: function () {
                    this.rotate(this.thumbnails, this.slides.length + 2);
                },
                createSlideNew: function (imgObj: api.IImageBaseModel) {
                    const newScope = $scope.$new(true);
                    newScope.$watch("active", (active: boolean) => { if (active) { this.select(newScope); } });
                    const newEl = $compile(
`<div class="item text-center" ng-class="{'active': active}" id="${Math.random()}">
    <img src="${($scope.imageDirectory || "") + imgObj.OriginalFileName + "?" + this.makeQueryString($scope.mainImageResize)}" class="img-fluid d-block mx-auto" alt="{{product.Name}}" />
    <div class="hidden cef-carousel-magnify">
        <i class="far fa-search-plus" aria-hidden="true"></i>
        <span class="sr-only">Magnify</span>
    </div>
</div>`)(newScope);
                    $element.find(".carousel-inner").append(newEl as any);
                    this.addSlide(newScope, newEl);
                    this.createThumbnailNew(imgObj, newScope);
                    this.sync($scope.syncIndex);
                },
                createThumbnailNew: function (imgObj: api.IImageBaseModel, slideScope) {
                    this.thumbnails.push((Object as any).assign({}, imgObj, {
                        fullImageUrl: ($scope.imageDirectory || "") + imgObj.ThumbnailFileName + "?" + this.makeQueryString($scope.thumbImageResize),
                        slideScope: slideScope
                    }));
                },
                makeQueryString: params => {
                    if (angular.isObject(params)) {
                        return $httpParamSerializer(params);
                    }
                    return "";
                },
                sync: idx => {
                    if (angular.isNumber(idx)) {
                        $scope.slides[idx] && $scope.select($scope.slides[idx]);
                    }
                },
                initImages: function (images: Array<any>) {
                    if (angular.isArray(images) && images.length) {
                        if (images[0]["Library"]) {
                            images = images.map(someObj => someObj.Library.Image );
                        }
                        this.slides = [];
                        this.thumbnails = [];
                        images.forEach(this.createSlide.bind(this));
                    }
                },
                initImagesNew: function (images: Array<any>) {
                    if (angular.isArray(images) && images.length) {
                        if (images[0]) {
                            images = images.map(someObj => someObj);
                        }
                        this.slides = [];
                        this.thumbnails = [];
                        images.forEach(this.createSlideNew.bind(this));
                    }
                },
                init: function () {
                    $scope.$watch("images", this.initImages.bind(this));
                    $scope.$watch("imagesNew", this.initImagesNew.bind(this));
                    $scope.$watch("syncIndex", this.sync.bind(this));
                },
                launchModal: () => {
                    if ($scope.showModal) {
                        $scope.pause();
                        const instance = $uibModal.open({
                            size: "lg",
                            resolve: {
                                data: () => ({
                                    images: angular.copy($scope.images),
                                    imagesNew: angular.copy($scope.imagesNew),
                                    imageDirectory: $scope.imageDirectory,
                                    syncIndex: $scope.carousel.getCurrentIndex()
                                })
                            },
                            templateUrl: $filter("corsLink")("/framework/store/photos/photo-carousel.modal.html", "ui"),
                            controller: function (data) {
                                this.data = data;
                            },
                            controllerAs: "modalCarouselCtrl"
                        });
                        instance.result.finally(() => $scope.play());
                    }
                }
            });
            newCtrl.init();
            return newCtrl;
        },
        controllerAs: "carousel",
        bindToController: false,
    }));
}
