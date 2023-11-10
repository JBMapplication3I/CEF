/* eslint-disable jsx-a11y/anchor-is-valid */
/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect } from "react";
import useCustomCompareEffect from "../_shared/customHooks/useCustomCompareEffect";
import { CatalogGridView, CatalogListView, CatalogTableView } from "./views";
import { Route, Switch, useHistory, useLocation, Link } from "react-router-dom";
import { CatalogFilters } from "./controls/CatalogFilters";
import { ErrorView } from "../_shared/common/ErrorView";
import { LoadingWidget } from "../_shared/common/LoadingWidget";
import { useViewState } from "../_shared/customHooks/useViewState";
import { useInventoryFactory } from "../_shared/customHooks/useInventoryFactory";
import { usePricingFactory } from "../_shared/customHooks/usePricingFactory";
import cvApi from "../_api/cvApi";
import { Compare } from "./views/Compare";
import { refreshCompareList } from "../_redux/actions";
import { useTranslation } from "react-i18next";
import { useCefConfig } from "../_shared/customHooks/useCefConfig";
import { CEFConfig } from "../_redux/_reduxTypes";
import {
  useAuctionSearchViewModel,
  compareAuctionCatalogSearchForm
} from "../_shared/customHooks/catalog/useAuctionSearchViewModel";
import {
  AuctionSearchViewModel,
  IHttpPromiseCallbackArg
} from "../_api/cvApi.shared";
import Interweave from "interweave";
import { CatalogPaginationControls } from "./controls/CatalogPaginationControls";
import {
  ProductCatalogSearchForm,
  ProductModel,
  CategoryModel,
  AggregateTree,
  AuctionCatalogSearchForm,
  AuctionIndexableModel,
  AuctionModel
} from "../_api/cvApi._DtoClasses";

const sizeOptions = [9, 18, 27];
const sortOptions = [
  "Popular",
  "Recent",
  "Name Ascending",
  "Name Descending",
  "Price Ascending",
  "Price Descending"
];

export const AuctionCatalog = (): JSX.Element => {
  const { setRunning, finishRunning, viewState } = useViewState();
  const cefConfig: CEFConfig = useCefConfig();
  const inventoryFactory = useInventoryFactory();
  const pricingFactory = usePricingFactory();
  const [auctions, setAuctions] = useState<Array<AuctionModel>>([]);
  const [breadcrumbs, setBreadcrumbs] = useState<string>(null);
  const [categories, setCategories] = useState<Array<any>>([]);
  const [searchViewModel, setSearchViewModel] = useAuctionSearchViewModel();
  const [PricingRanges, setPricingRanges] = useState<Array<any>>([]);

  const { t } = useTranslation();
  const location = useLocation();
  const history = useHistory();

  const usingInfiniteScroll =
    cefConfig && cefConfig.catalog.defaultPageSize === 0;

  useCustomCompareEffect(
    catalogSearch,
    searchViewModel?.Form,
    compareAuctionCatalogSearchForm
  );

  useEffect(() => {
    if (searchViewModel?.ResultIDs) {
      getAuctionsByIDs();
    }
  }, [cefConfig, searchViewModel?.ResultIDs]);

  useEffect(() => {
    getCategories();
  }, []);

  const getLevelsDeepForCategory = (
    catsSourceName: "categories" | "categories tree",
    category: CategoryModel
  ): number => {
    let levels = 1;
    if (catsSourceName === "categories") {
      let categoryToFind: CategoryModel = category;
      for (let i = 0; i < categories.length; i++) {
        if (categories[i].Name === categoryToFind.ParentName) {
          levels++;
          categoryToFind = categories[i];
        }
      }
    }
    if (
      catsSourceName === "categories tree" &&
      !searchViewModel?.CategoriesTree
    ) {
      return 0;
    }
    if (catsSourceName === "categories tree") {
      const keyToFind = `${category.Name}|${category.CustomKey}`;
      findCategoryByCustomKey(
        searchViewModel?.CategoriesTree?.Children,
        keyToFind,
        () => (levels += 1)
      );
      return levels;
    }
    return levels;
  };

  const shouldShowCategoryLandingPages = () => {
    if (!cefConfig || !cefConfig.catalog.showCategoriesForLevelsUpTo) {
      return false;
    }
    const paramsCategory: string = searchViewModel?.Form?.Category;
    if (!paramsCategory) {
      return null;
    }
    const [Name, CustomKey] = paramsCategory.split("|");
    const currentCat: CategoryModel = categories.find(
      (cat) => cat.Name === Name && cat.CustomKey === CustomKey
    );
    if (
      currentCat.Children &&
      currentCat.Children.length &&
      getLevelsDeepForCategory("categories", currentCat) <
        cefConfig.catalog.showCategoriesForLevelsUpTo
    ) {
      return true;
    } else {
      return false;
    }
  };

  function findCategoryByCustomKey(
    catTree: AggregateTree[],
    customKey: string,
    countFn?: Function
  ): AggregateTree {
    for (let category of catTree) {
      if (category.Key === customKey) {
        return category;
      } else if (category.Children) {
        let desiredNode: any = findCategoryByCustomKey(
          category.Children,
          customKey,
          countFn
        );
        if (countFn) {
          countFn();
        }
        if (desiredNode) {
          return desiredNode;
        }
      }
    }
    return null;
  }

  function catalogSearch(): void {
    setRunning();
    if (searchViewModel?.Form?.Category) {
      setBreadcrumbs(searchViewModel?.Form?.Category);
    } else if (searchViewModel?.Form?.Query) {
      setBreadcrumbs(searchViewModel?.Form?.Query);
    } else {
      setBreadcrumbs(null);
    }
    if (!searchViewModel?.Form) {
      return;
    }
    // TODO: need a way to cancel this promise if it's running and the searchViewModel gets modified again
    cvApi.providers
      .SearchAuctionCatalogWithProvider(searchViewModel.Form)
      .then((res: IHttpPromiseCallbackArg<AuctionSearchViewModel>) => {
        if (usingInfiniteScroll && searchViewModel.Form.Page !== 1) {
          setSearchViewModel((searchViewModel) => {
            searchViewModel.ResultIDs = { ...res.data.ResultIDs };
            return searchViewModel;
          });
        } else {
          setSearchViewModel(res.data);
        }
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "Failed to search product catalog with provider"
        );
      });
  }

  function getCategories(): void {
    setRunning();
    cvApi.categories
      .GetCategoriesThreeLevels()
      .then((res: any) => {
        setCategories(res.data);
        finishRunning(false);
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to get categories");
      });
  }

  function removeItemFromCompareCart(id: number): void {
    setRunning();
    cvApi.shopping
      .RemoveCompareCartItemByProductID(id)
      .then((res: any) => {
        finishRunning();
        refreshCompareList();
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "Failed to remove item from compare cart"
        );
      });
  }

  async function getAuctionsByIDs() {
    try {
      // Get base products
      // TODO@JS: REMOVE THIS BEFORE COMITTING
      // Update the toSearch value to match the auction ids in your data for the catalog to work
      // Use the toSearch var below

      // let toSearch;
      // if (searchViewModel && searchViewModel.ResultIDs && !searchViewModel.ResultIDs.length) {
      //   toSearch = [13, 14, 16, 17, 80, 81, 82, 83, 86];
      // }
      if (!searchViewModel?.ResultIDs?.length) {
        setAuctions([]);
        return;
      }
      setRunning();
      let newAuctions = (
        await cvApi.auctions.GetAuctionsByIDs({
          IDs: usingInfiniteScroll
            ? searchViewModel?.ResultIDs?.slice(
                (searchViewModel?.Form?.Page - 1) *
                  searchViewModel?.Form?.PageSetSize,
                (searchViewModel?.Form?.Page - 1) *
                  searchViewModel?.Form?.PageSetSize +
                  searchViewModel?.Form?.PageSetSize
              )
            : searchViewModel?.ResultIDs
        })
      )?.data as AuctionModel[];
      setAuctions(
        usingInfiniteScroll ? [...auctions, ...newAuctions] : newAuctions
      );
      finishRunning();
    } catch (err) {
      console.log(err);
      finishRunning(true, "unable to fetch products or product related data");
    }
  }

  const navigateToPage = (page: number): void => {
    setSearchViewModel({
      ...searchViewModel,
      Form: { ...searchViewModel?.Form, Page: page }
    });
  };

  const onChangeProductQuantity = (
    productId: number,
    newQuantity: number
  ): void => {
    const productsCopy = [...auctions];
    const productToUpdate = productsCopy.find(
      (pr: { ID: number }) => pr.ID === productId
    );
    //@ts-ignore: Object is possibly 'undefined'
    productToUpdate.quantity = newQuantity;
    setAuctions(productsCopy);
  };

  const onChangeQueryParam = <T,>(
    param: keyof ProductCatalogSearchForm,
    newValue: T
  ): void => {
    let form: ProductCatalogSearchForm;
    try {
      form = JSON.parse(JSON.stringify(searchViewModel?.Form));
    } catch (err) {
      return;
    }
    if (form[param] && !newValue) {
      delete form[param];
    }
    if (newValue) {
      (form[param] as unknown) = newValue as T;
    }
    if (param !== "Page") {
      form.Page = 1;
    }
    setSearchViewModel({ ...searchViewModel, Form: form });
  };

  const renderCategoryLandingPageView = (): JSX.Element => {
    if (!shouldShowCategoryLandingPages()) {
      return null;
    }
    const paramsCategory: string = searchViewModel?.Form?.Category;
    if (!paramsCategory) {
      return null;
    }
    const [Name, CustomKey] = paramsCategory.split("|");
    const currentCat: CategoryModel = categories.find(
      (cat) => cat.Name === Name && cat.CustomKey === CustomKey
    );
    return (
      <div className="row">
        {currentCat.Children.map((child) => {
          return (
            <div
              className="col-12 col-sm-6 col-lg-4 col-xl-3"
              key={child.CustomKey}>
              <Link
                style={{ fontWeight: "bold" }}
                to={`/catalog?category=${child.Name}|${child.CustomKey}`}>
                <div className="card">
                  <div className="card-body text-center py-5">{child.Name}</div>
                </div>
              </Link>
            </div>
          );
        })}
      </div>
    );
  };

  const renderView = (): React.ReactNode => {
    if (shouldShowCategoryLandingPages()) {
      return null;
    }
    const viewProps = {
      products: auctions,
      useInfinite: usingInfiniteScroll,
      onScrollToBottom: () => {
        if (!viewState.running) {
          navigateToPage(searchViewModel?.Form?.Page + 1);
        }
      },
      parentRunning: viewState.running,
      onChangeProductQuantity
    };
    let element = <LoadingWidget />;
    if (!usingInfiniteScroll && viewState.running) {
      return element;
    }
    // switch (searchViewModel?.Form?.PageFormat) {
    //   case "grid":
    //     element = <CatalogGridView {...viewProps} />;
    //     break;
    //   case "table":
    //     element = <CatalogTableView {...viewProps} />;
    //     break;
    //   case "list":
    //     element = <CatalogListView {...viewProps} />;
    //     break;
    // }
    return element;
  };

  return (
    <>
      <div className="container">
        <ErrorView error={viewState.errorMessage} />
      </div>
      <Switch>
        <Route exact path="/Auction-Catalog">
          <div className="container">
            <div className="catalog-wrap d-flex flex-wrap d-md-block clearfix">
              {/* @ts-ignore */}
              <CatalogFilters<AuctionCatalogSearchForm, AuctionIndexableModel>
                allCategories={searchViewModel?.Categories}
                allAttributes={searchViewModel?.Attributes}
                removeItemFromCompareCart={removeItemFromCompareCart}
                searchViewModel={searchViewModel}
                setSearchViewModel={setSearchViewModel}
              />
              <div className="catalog-content">
                {searchViewModel?.Form?.Query?.length ||
                searchViewModel?.Form?.Category?.length ? (
                  <h1>
                    {t("ui.storefront.catalog.ShopResultsFor")}{" "}
                    <strong>
                      {searchViewModel?.Form?.Query?.length
                        ? searchViewModel?.Form?.Query
                        : searchViewModel?.Form?.Category.split("|")[0]}
                    </strong>
                  </h1>
                ) : null}
                <div className="catalog-content-action d-md-flex justify-content-between align-items-end">
                  <div
                    className="catalog-popups collapse"
                    id="catalogGridCollapse">
                    <div className="d-md-none text-end mb-3">
                      <a
                        className="close"
                        data-bs-toggle="collapse"
                        href="#catalogGridCollapse"
                        role="button"
                        aria-expanded="false"
                        aria-controls="catalogGridCollapse">
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          width="14"
                          height="14"
                          viewBox="0 0 14 14">
                          <path
                            d="m8.485 7 4.487-4.487.925-.925a.35.35 0 0 0 0-.495L12.907.1a.35.35 0 0 0-.495 0L7 5.515 1.588.1a.35.35 0 0 0-.495 0L.1 1.092a.35.35 0 0 0 0 .495L5.515 7 .1 12.412a.35.35 0 0 0 0 .495l.99.99a.35.35 0 0 0 .495 0L7 8.485l4.487 4.487.925.925a.35.35 0 0 0 .495 0l.99-.99a.35.35 0 0 0 0-.495Z"
                            fill="#191e2a"></path>
                        </svg>
                      </a>
                    </div>
                    <ul className="catalog-view d-md-flex list-unstyled">
                      {[
                        {
                          format: "grid",
                          icon: `<svg
                        width="16"
                        height="16"
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 16 16"
                      >
                        <path
                          d="M8.857 14.286V8.857h5.429v5.429zm-7.143 0V8.857h5.429v5.429zM7.143 1.714v5.429H1.714V1.714zm7.143 0v5.429H8.857V1.714zM14.857 0H1.143C.512 0 0 .512 0 1.143v13.714C0 15.488.512 16 1.143 16h13.714c.631 0 1.143-.512 1.143-1.143V1.143C16 .512 15.488 0 14.857 0z"
                          fill="#191e2a"
                        ></path></svg
                      >`
                        },
                        {
                          format: "list",
                          icon: `<svg
                        width="20"
                        height="16"
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 20 16"
                      >
                        <path
                          d="M19.692 13.846v.616c0 .34-.275.615-.615.615H6.769a.615.615 0 0 1-.615-.615v-.616c0-.34.275-.615.615-.615h12.308c.34 0 .615.275.615.615zm0-6.154v.616c0 .34-.275.615-.615.615H6.769a.615.615 0 0 1-.615-.615v-.616c0-.34.275-.615.615-.615h12.308c.34 0 .615.275.615.615zm0-6.154v.616c0 .34-.275.615-.615.615H6.769a.615.615 0 0 1-.615-.615v-.616c0-.34.275-.615.615-.615h12.308c.34 0 .615.276.615.615zM1.846 0a1.846 1.846 0 1 1 0 3.692 1.846 1.846 0 0 1 0-3.692zm1.846 8c0 1-.826 1.846-1.846 1.846A1.859 1.859 0 0 1 0 8c0-1 .827-1.846 1.846-1.846C2.866 6.154 3.692 7 3.692 8zm0 6.154a1.846 1.846 0 1 1-3.692 0 1.846 1.846 0 0 1 3.692 0z"
                          fill="#191e2a"
                        ></path></svg
                      >`
                        }
                      ].map((option) => {
                        return (
                          <li
                            key={option.format}
                            className={`${
                              searchViewModel?.Form?.PageFormat ===
                              option.format
                                ? "active"
                                : ""
                            }`}>
                            <a
                              role="button"
                              className={`d-inline-flex
                              align-items-center
                              text-decoration-none`}
                              onClick={(e) =>
                                onChangeQueryParam<string>(
                                  "PageFormat",
                                  option.format
                                )
                              }>
                              <Interweave
                                content={option.icon}
                                allowAttributes={true}
                                allowElements={true}
                              />
                              <span className="text-capitalize">
                                &nbsp;{option.format}
                              </span>
                            </a>
                          </li>
                        );
                      })}
                    </ul>
                  </div>
                  <div
                    className="catalog-popups collapse"
                    id="catalogSortCollapse">
                    <div className="d-md-none text-end mb-3">
                      <a
                        className="close"
                        data-bs-toggle="collapse"
                        href="#catalogSortCollapse"
                        role="button"
                        aria-expanded="false"
                        aria-controls="catalogSortCollapse">
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          width="14"
                          height="14"
                          viewBox="0 0 14 14">
                          <path
                            d="m8.485 7 4.487-4.487.925-.925a.35.35 0 0 0 0-.495L12.907.1a.35.35 0 0 0-.495 0L7 5.515 1.588.1a.35.35 0 0 0-.495 0L.1 1.092a.35.35 0 0 0 0 .495L5.515 7 .1 12.412a.35.35 0 0 0 0 .495l.99.99a.35.35 0 0 0 .495 0L7 8.485l4.487 4.487.925.925a.35.35 0 0 0 .495 0l.99-.99a.35.35 0 0 0 0-.495Z"
                            fill="#191e2a"></path>
                        </svg>
                      </a>
                    </div>
                    <div className="sort-catalog-form ms-md-3">
                      <label className="form-label" htmlFor="sort">
                        {t(
                          "ui.storefront.product.catalog.controls.searchCatalogResultsSortCtrl.sortBy"
                        )}
                      </label>
                      <select
                        className="form-select w-100"
                        id="sort"
                        aria-label="sort"
                        defaultValue={searchViewModel?.Form?.Sort}
                        onChange={(e) =>
                          onChangeQueryParam("Sort", e.target.value)
                        }>
                        <option>Relevance</option>
                        {sortOptions.map((str): JSX.Element => {
                          return (
                            <option
                              key={str}
                              label={str}
                              value={str.split(" ").join("")}></option>
                          );
                        })}
                      </select>
                    </div>
                  </div>
                </div>
                <div className="row my-2">
                  <div className="col-12">
                    <ErrorView error={viewState.errorMessage} />
                  </div>
                </div>
                {/* {renderCategoryLandingPageView()} */}
                {renderView()}
                {/* // TODO */}
                {/* {!usingInfiniteScroll && !shouldShowCategoryLandingPages() && (
                  <CatalogPaginationControls
                    searchViewModel={searchViewModel}
                    navigateToPage={navigateToPage}
                  />
                )} */}
              </div>
            </div>
          </div>
        </Route>
        <Route path="/catalog/compare">
          <Compare removeItemFromCompareCart={removeItemFromCompareCart} />
        </Route>
      </Switch>
    </>
  );
};
