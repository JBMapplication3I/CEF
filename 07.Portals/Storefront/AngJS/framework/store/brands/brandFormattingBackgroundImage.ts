// NOTE: Brands don't have Images yet


//module cef.store.brands {
//    export class BrandFormattingBackgroundImageController extends BrandFormattingBaseController {
//        constructor(protected cvCurrentBrandService: services.ICurrentBrandService) { super(cvCurrentBrandService); }

//        getBackgroundImageCSS(): any {
//            if (!this.brand || !this.brand.BrandImages) { return null; }
//            const bg = _.find(this.brand.BrandImages, x => x.Library.Name === "background.jpg");
//            if (!bg) { return null; }
//            return bg.Library.Image.Bytes;
//        }
//    }

//    cefApp.directive("cefBrandFormattingBackgroundImage", () => ({
//        restrict: "A",
//        template: `<style>body { background-image: url('data:image/jpeg;base64,{{bfbic.getBackgroundImageCSS()}}') !important; }</style>`,
//        controller: BrandFormattingBackgroundImageController,
//        controllerAs: "bfbic",
//    }));
//}
