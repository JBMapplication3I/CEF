/**
 * @desc Creates a searchCatalog view for sub-categories
 */
module cef.store.category {
    class AbstractCategoriesController extends core.TemplatedControllerBase {
        // Properties
        categories: Array<api.CategoryModel> = [];
        currentCategory: api.CategoryModel;
        imagesArray: Array<api.CategoryImageModel> = [];
        // Functions
        checkForChildren(id: number): ng.IPromise<boolean> {
            if (!id) {
                return this.$q.reject("Invalid ID");
            }
            return this.$q((resolve, reject) => {
                this.cvApi.categories.GetCategoryTree(<api.GetCategoryTreeDto>{
                    Active: true,
                    ParentID: id,
                    IncludeChildrenInResults: true,
                    DisregardParents: false,
                    Sorts: [<api.Sort>{ dir: "asc", field: "Name", order: 0 }]
                }).then(r => {
                    if (!r || !r.data) {
                        reject("No data returned");
                        return;
                    }
                    resolve(r.data.length > 0);
                }).catch(reject);
            });
        }
        goToLink(cat?: api.CategoryModel, hasKids?: boolean, goToRootCat?: boolean): void {
            if (goToRootCat) {
                this.$filter("goToCORSLink")(
                    "",
                    "catalog",
                    "primary",
                    false
                );
            }
            if (!hasKids) {
                this.$filter("goToCORSLink")(
                    "SearchCatalogState:searchCatalog.products.results.both",
                    "catalog",
                    "primary",
                    false,
                    { "category": `${cat.Name}|${cat.CustomKey}`, "term": null });
                return;
            }
            this.$filter("goToCORSLink")(
                "/Categories:categories.level",
                "category",
                "primary",
                false,
                { "category": cat.SeoUrl });
        }
        goToChildrenOrResults(id?: number): void {
            if (!id) {
                this.goToLink(null, null, true);
                return;
            }
            this.setRunning();
            this.checkForChildren(id).then(has => {
                const c = _.find(this.flatMergeTreeAggregate(this.categories), x => x.ID === id);
                if (!c) {
                    this.finishRunning(true);
                    this.goToLink(this.currentCategory, false);
                    return;
                }
                if (!has) {
                    this.finishRunning(true);
                    this.goToLink(c, false);
                    return;
                }
                this.finishRunning(true);
                this.goToLink(c, true);
            });
        }
        currentCategoryString(): Array<string> {
            return this.$location.path().split("/").filter(x => x != "cat").filter(x => x != "");
        }
        getCategoryImages(categoryArr: Array<api.CategoryModel>): void {
            this.$q.all(categoryArr.map(cat =>
                this.cvApi.categories.GetCategoryImages({ Active: true, AsListing: true, MasterID: cat.ID })
                    .then(result => {
                        const images = result.data.Results;
                        if (images.length) {
                            images.map(x => this.imagesArray.push(x))
                        }
                    })
            )).then(() => {
                this.categories.map(x => {
                    const imageName = this.imagesArray.filter(y => y.MasterID == x.ID && y.OriginalFileName);
                    if (imageName.length) {
                        x.PrimaryImageFileName = imageName[0].OriginalFileName;
                    }
                })
            }).catch(reason => console.warn(reason));
        }
        load(): void {
            this.setRunning();
            // "Level 1", nothing in path after /cat
            if (!this.currentCategoryString().length) {
                this.cvApi.categories.GetCategoryTree(<api.GetCategoryTreeDto>{
                    Active: true,
                    ParentID: null,
                    IncludeChildrenInResults: true,
                    DisregardParents: false,
                    Sorts: [<api.Sort>{ dir: "asc", field: "Name", order: 0 }]
                }).then(r => {
                    this.categories = r.data as any;
                    this.getCategoryImages(this.categories)
                    this.finishRunning();
                }).catch(reason => this.finishRunning(true, reason));
                return;
            }
            const seoUrl = this.currentCategoryString().pop();
            this.cvApi.categories.CheckCategoryExistsBySeoUrl({ SeoUrl: seoUrl }).then(r1 => {
                if (!r1 || !r1.data) {
                    this.finishRunning(true, `No category by this seo url: ${seoUrl}`);
                    // Fail, go to /catalog with no params
                    if (!this.currentCategory || !this.currentCategory.ID) {
                        this.goToChildrenOrResults(null);
                        return;
                    }
                    this.goToChildrenOrResults(this.currentCategory.ID);
                    return;
                }
                this.cvApi.categories.GetCategoryByID({ ID: r1.data, ExcludeProductCategories: true }).then(r2 => {
                    if (!r2 || !r2.data) {
                        this.finishRunning(true, `No category by this id: ${r1.data}`);
                        if (!this.currentCategory || !this.currentCategory.ID) {
                            this.goToChildrenOrResults(null);
                            return;
                        }
                        this.goToChildrenOrResults(this.currentCategory.ID);
                        return;
                    }
                    this.currentCategory = r2.data;
                    this.categories = r2.data.Children;
                    if (this.categories && this.categories.length) {
                        this.getCategoryImages(this.categories);
                        this.finishRunning();
                        return;
                    }
                    this.cvApi.categories.GetCategoryTree(<api.GetCategoryTreeDto>{
                        Active: true,
                        ParentID: r1.data,
                        IncludeChildrenInResults: true,
                        DisregardParents: false,
                        Sorts: [<api.Sort>{ dir: "asc", field: "Name", order: 0 }]
                    }).then(r3 => {
                        this.categories = r3.data as any;
                        if (!this.categories || !this.categories.length) {
                            this.finishRunning(true);
                            if (!this.currentCategory || !this.currentCategory.ID) {
                                this.goToChildrenOrResults(null);
                                return;
                            }
                            this.goToChildrenOrResults(this.currentCategory.ID);
                            return;
                        }
                        this.getCategoryImages(this.categories);
                        this.finishRunning();
                    }).catch(reason3 => this.finishRunning(true, reason3));
                }).catch(reason2 => this.finishRunning(true, reason2));
            }).catch(reason1 => this.finishRunning(true, reason1));
        }
        private flatMergeTreeAggregate(categoriesTree: any, recurse: number = 1): any[] {
            categoriesTree = categoriesTree.reduce((accu, category) => {
                if (category.HasChildren && category.Children && category.Children.length) {
                    for (let i = 0; i < category.Children.length; i++) {
                        accu.unshift(category.Children.pop());
                    }
                }
                accu.unshift(category);
                return accu;
            }, []);
            if (!_.some(categoriesTree, c => c["Children"] && c["Children"].length)) {
                return categoriesTree;
            }
            return this.flatMergeTreeAggregate(categoriesTree, ++recurse);
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly $filter: ng.IFilterService,
                private readonly $location: ng.ILocationService,
                private readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefCategoriesView", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/category/abstractCategories.html", "ui"),
        controller: AbstractCategoriesController,
        controllerAs: "abstractCatCtrl"
    }));
}
