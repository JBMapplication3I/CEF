function stopZoomAndScroll() {
    if (window["MagicZoom"]) {
        window["MagicZoom"].stop("productDetailImage");
    }
    if (window["MagicScroll"]) {
        //window["MagicScroll"].stop("scroll-1");
        window["MagicScroll"].stop();
    }
}
let tries = 0;
//// let firstRun = true;
function loadZoom(options: string, cefConfig): void {
    tries++;
    //// if (firstRun) {
    ////     firstRun = false;
    ////     if (!window.location.origin) {
    ////         (window.location as any).origin = window.location.protocol + "//" + window.location.hostname
    ////             + (window.location.port ? ":" + window.location.port : "");
    ////     }
    ////     const head = document.getElementsByTagName("head")[0];
    ////     const newScript1 = document.createElement("script");
    ////     newScript1.type = "text/javascript";
    ////     newScript1.id = "magic-zoom-script";
    ////     newScript1.src = (cefConfig && cefConfig.routes.api.host || "") + "/js/magiczoomplus.js";
    ////     head.appendChild(newScript1);
    ////     const newScript2 = document.createElement("script");
    ////     newScript2.type = "text/javascript";
    ////     newScript2.id = "magic-scroll-script";
    ////     newScript2.src = (cefConfig && cefConfig.routes.api.host || "") + "/js/magicscroll.js";
    ////     head.appendChild(newScript2);
    //// }
    //// if (!window["MagicZoom"] || !window["MagicScroll"] /*|| !window["MagicZoom"]["init"]*/) {
    ////     // Wait for the scripts to load
    ////     window.setTimeout(() => loadZoom(options, cefConfig), 50);
    ////     return;
    //// }
    ////if (firstRunB) {
    ////    firstRunB = false;
    ////    window["MagicZoom"].init();
    ////}
    if (!window["MagicZoom"] || !window["MagicScroll"] /*|| !window["MagicZoom"]["init"]*/) {
        // Wait for the scripts to load
        if (tries > 10) {
            return;
        }
        window.setTimeout(() => loadZoom(options, cefConfig), 100);
        return;
    }
    window["MagicZoom"].start("productDetailImage");
    const opt: { [key: string]: any } = { };
    const split = options.split(";");
    for(let i = 0; i < split.length; i++) {
        if (!split[i]) { continue; }
        const inner = split[i].split(":");
        opt[inner[0].trim()] = inner[1].trim();
    }
    window["MagicScrollOptions"] = opt;
    window["MagicScroll"].start("scroll-1");
}

module cef.store.photos.magicImageCarousel {
    class MagicImageCarouselController extends core.TemplatedControllerBase {
        // Properties
        /** The array of image multi-size url objects, populated by this class */
        imagesResized: Array<{ largeImageUrl: string, mediumImageUrl: string, thumbImageUrl: string }>;
        /** Number of seconds between image rotation events, populated by Scope */
        interval: number;
        /** Turn off transition effects, populated by Scope */
        noTransition: boolean;
        /** Turn off ability to pause, populated by Scope */
        noPause: boolean;
        /** Turn off loop back around to beginning, populated by Scope */
        noWrap: boolean;
        /** String of magic zoom carousel options, populated by Scope */
        options: string;
        /** String of magic scroll options, populated by Scope */
        scrollOptions: string;
        /** String of magic zoom carousel options, populated by Scope */
        template: string;
        /** Array of images, populated by Scope */
        get images(): Array<api.ProductImageModel> { return this._images; };
        set images(newValue: Array<api.ProductImageModel>) { this._images = newValue; this.initImages(); }
        private _images: Array<api.ProductImageModel>;
        /** relative path to images, populated by Scope */
        imageDirectory: string;
        // Example image resizer settings:  {width:366,height:366,mode:'pad',scale:'both'}
        /** Object with image resizer parameters, populated by Scope */
        largeImageResize: any;
        /** Object with image resizer parameters, populated by Scope */
        mediumImageResize: any;
        /** Object with image resizer parameters, populated by Scope */
        thumbImageResize: any;
        thumbCount: number;
        thumbsPaging: core.Paging<{ largeImageUrl: string, mediumImageUrl: string, thumbImageUrl: string }>;
        // Functions
        makeQueryString(params): string {
            if (!angular.isObject(params)) { return ""; }
            let result = "";
            Object.keys(params).forEach(key => result += key + "=" + params[key] + "&");
            result = result.slice(0, result.length - 1);
            return result;
        }

        createImage(imgObj: api.ProductImageModel): void {
            const toPush = {};
            angular.extend(toPush, imgObj);
            angular.extend(toPush, {
                largeImageUrl:  (this.imageDirectory || "") + "/" + imgObj.OriginalFileName + "?" + this.makeQueryString(this.largeImageResize),
                mediumImageUrl: (this.imageDirectory || "") + "/" + imgObj.OriginalFileName + "?" + this.makeQueryString(this.mediumImageResize),
                thumbImageUrl:  (this.imageDirectory || "") + "/" + imgObj.OriginalFileName + "?" + this.makeQueryString(this.thumbImageResize)
            });
            this.imagesResized.push(toPush as any);
        }

        disposeMode = false;
        initImages(): void {
            // We have to put in delays because we are calling non-angular code and this
            // initializes in an order wherere $timeout might not be ready yet. Also, the
            // disposeMode destroys the existing HTML so that it can be regenerated and the
            // compile re-asserts the new images in a way that the magic zoom and scroll
            // can apply their scripting to them without being impacted by previous runs.
            if (!this.$timeout) { window.setTimeout(() => this.initImages(), 50); return; }
            stopZoomAndScroll();
            this.disposeMode = true;
            this.imagesResized = [];
            this.images.forEach(x => this.createImage(x));
            this.disposeMode = false;
            const scrollEl = angular.element("#scroll-1");
            scrollEl.remove(".pre-thumb");
            scrollEl.remove(".mcs-loader");
            scrollEl.remove(".mcs-bullets");
            scrollEl.remove(".mcs-wrapper");
            scrollEl.empty();
            const template = angular.element(angular.element("#magic-scroll-template")[0].innerHTML);
            scrollEl.append(template as any);
            this.$compile(template as any)(this.$scope);
            this.$timeout(() => loadZoom(this.scrollOptions, this.cefConfig), 50);
        }
        // Constructors
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $timeout: ng.ITimeoutService,
                private readonly $compile: ng.ICompileService,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefMagicImageCarousel", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            interval: "@?",             // Number of seconds between image rotation events
            noTransition: "@?",         // Turn off transition effects
            noPause: "@?",              // Turn off ability to pause
            noWrap: "@?",               // Turn off loop back around to beginning
            imageDirectory: "@",        // Relative path to images
            // Example options:         selectorTrigger: hover; transitionEffect: false; zoomWidth: 150%
            options: "@",               // String of magic zoom carousel options
            scrollOptions: "@",         // String of magic scroll options
            template: "@",              // String of magic zoom template
            // Example resize settings: {width:366,height:366,mode:'pad',scale:'both'}
            largeImageResize: "=",      // Bind to object with image resizer parameters
            mediumImageResize: "=",     // Bind to object with image resizer parameters
            thumbImageResize: "=",      // Bind to object with image resizer parameters
            images: "=",                // Bind to array of images
            thumbCount: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/photos/magicImageCarousel/magicImageCarousel.html", "ui"),
        controller: MagicImageCarouselController,
        controllerAs: "magicImageCarouselCtrl",
        bindToController: true
    }));
}
