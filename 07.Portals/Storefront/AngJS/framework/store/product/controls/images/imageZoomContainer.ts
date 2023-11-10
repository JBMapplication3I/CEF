module cef.store.product.controls.images {
    cefApp.directive("cefImageZoomContainer", () => ({
        // Depends on the elevateZoom jQuery plugin
        // This will be a bit lame, but time is short.
        // The plugin wants an ID for gallery selector, so that's what it has to be for now
        restrict: "A",
        controller: function ($attributes: ng.IAttributes) {
            this.instance = null;
            this.config = {
                cursor: "pointer",
                galleryActiveClass: "active",
                imageCrossfade: true,
                responsive: true
            }
            if ($attributes["cefImageZoomGallery"]) {
                this.config.gallery = $attributes["cefImageZoomGallery"];
            }
            // Extra stuff for adding buttons
            this.galleryItems = [];
            this.galleryStep = function (backward) {
                this.galleryItems.forEach(function (item, idx, arr) {
                    const data = item.data();
                    if (data.zoomImage && data.image) {
                        if (item.hasClass("active")) {
                            const nextIdx = backward ? (idx - 1 < 0) ? arr.length : idx-- : (idx + 1 > arr.length) ? 0 : idx++;
                            if (arr[nextIdx]) {
                                this.instance.swaptheimage(arr[nextIdx].image, arr[nextIdx].zoomImage);
                                item.removeClass("active");
                                arr[nextIdx].addClass("active");
                            }
                        }
                    }
                }.bind(this));
            }.bind(this);
        }
    }));
}
