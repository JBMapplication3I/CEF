/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect } from "react";
import useCustomCompareEffect from "../_shared/customHooks/useCustomCompareEffect";
import { Route, Switch, useHistory } from "react-router-dom";
import { useTranslation } from "react-i18next";

import {
  CatalogGridView,
  CatalogListView,
  CatalogTableView,
  CategoryLandingPageView
} from "./views";
import { Compare } from "./views/Compare";
import { CatalogFilters } from "./controls/CatalogFilters";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTable, faThLarge, faThList } from "@fortawesome/free-solid-svg-icons";
import {
  Button,
  Container,
  Breadcrumb,
  Col,
  Row,
  Card,
  ListGroup,
  InputGroup,
  Form
} from "react-bootstrap";

import { ErrorView } from "../_shared/common/ErrorView";
import { LoadingWidget } from "../_shared/common/LoadingWidget";
import { Breadcrumbs } from "../_shared/Breadcrumbs";
import { useViewState } from "../_shared/customHooks/useViewState";
import { useInventoryFactory } from "../_shared/customHooks/useInventoryFactory";
import { usePricingFactory } from "../_shared/customHooks/usePricingFactory";
import { useCefConfig } from "../_shared/customHooks/useCefConfig";
import {
  useProductSearchViewModel,
  compareProductCatalogSearchForm
} from "../_shared/customHooks/catalog/useProductSearchViewModel";

import { refreshCompareList } from "../_redux/actions";

import { Dictionary } from "../_api/cvApi.shared";
import { SearchProductCatalogWithProviderDto } from "../_api";
import { CEFConfig } from "../_redux/_reduxTypes";
import { CatalogPaginationControls } from "./controls/CatalogPaginationControls";

import cvApi from "../_api/cvApi";
import {
  ProductCatalogSearchForm,
  ProductModel,
  CategoryModel,
  AggregateTree
} from "../_api/cvApi._DtoClasses";
import { ProductSearchViewModel } from "../_api/cvApi.shared";

import { IHttpPromiseCallbackArg } from "../_api/cvApi.shared";

const formatOptions = [
  { format: "grid", icon: faThLarge },
  { format: "table", icon: faTable },
  { format: "list", icon: faThList }
];
const sizeOptions = [9, 18, 27];
const sortOptions = [
  "Popular",
  "Recent",
  "Name Ascending",
  "Name Descending",
  "Price Ascending",
  "Price Descending"
];

export const Catalog = (): JSX.Element => {
  const { setRunning, finishRunning, viewState } = useViewState();
  const cefConfig: CEFConfig = useCefConfig();
  const inventoryFactory = useInventoryFactory();
  const pricingFactory = usePricingFactory();
  const [products, setProducts] = useState<Array<ProductModel>>([]);
  const [categories, setCategories] = useState<Array<any>>([]);
  const [brands, setBrands] = useState<Array<[string, number]>>();
  const [productSearchViewModel, setProductSearchViewModel] = useProductSearchViewModel();

  const { t } = useTranslation();
  const history = useHistory();

  const usingInfiniteScroll = cefConfig && cefConfig.catalog.defaultPageSize === 0;

  useCustomCompareEffect(
    catalogSearch,
    productSearchViewModel?.Form,
    compareProductCatalogSearchForm
  );

  useEffect(() => {
    if (productSearchViewModel?.ResultIDs) {
      getProductsByProductIDs();
    }
  }, [cefConfig, productSearchViewModel?.ResultIDs]);

  useEffect(() => {
    // TODO handle back button
    // console.log("history.action", history.action);
    if (history.action !== "PUSH") {
      // catalogSearch();
    }
  }, [history.action]);

  useEffect(() => {
    getCategories();
  }, []);

  useEffect(() => {
    if (productSearchViewModel?.BrandNames) {
      setBrands(Object.entries(productSearchViewModel.BrandNames));
    }
  }, [productSearchViewModel.BrandNames]);

  const shouldShowCategoryLandingPages = (): boolean => {
    if (!cefConfig || !cefConfig.catalog.showCategoriesForLevelsUpTo) {
      return false;
    }
    const paramsCategory: string = productSearchViewModel?.Form?.Category;
    if (!paramsCategory) {
      return false;
    }
    const [Name, CustomKey] = paramsCategory.split("|");
    const currentCat: CategoryModel = categories.find(
      (cat) => cat.Name === Name && cat.CustomKey === CustomKey
    );
    if (
      currentCat?.Children?.length &&
      getLevelsDeepForCategory("categories", currentCat) <
        cefConfig.catalog.showCategoriesForLevelsUpTo
    ) {
      return true;
    } else {
      return false;
    }
  };

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
    if (catsSourceName === "categories tree" && !productSearchViewModel?.CategoriesTree) {
      return 0;
    }
    if (catsSourceName === "categories tree") {
      const keyToFind = `${category.Name}|${category.CustomKey}`;
      findCategoryByCustomKey(
        productSearchViewModel?.CategoriesTree?.Children,
        keyToFind,
        () => (levels += 1)
      );
      return levels;
    }
    return levels;
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
        let desiredNode: any = findCategoryByCustomKey(category.Children, customKey, countFn);
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
    if (!productSearchViewModel?.Form) {
      return;
    }
    // TODO: need a way to cancel this promise if it's running and the productSearchViewModel gets modified again
    cvApi.providers
      .SearchProductCatalogWithProvider(productSearchViewModel.Form)
      .then((res: IHttpPromiseCallbackArg<ProductSearchViewModel>) => {
        if (usingInfiniteScroll && productSearchViewModel.Form.Page !== 1) {
          setProductSearchViewModel((productSearchViewModel) => {
            productSearchViewModel.ResultIDs = { ...res.data.ResultIDs };
            return productSearchViewModel;
          });
        } else {
          setProductSearchViewModel(res.data);
        }
        if (!productSearchViewModel.ResultIDs?.length) {
          finishRunning(true);
          return;
        }
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to search product catalog with provider");
      });
  }

  function getCategories(): void {
    // setRunning();
    cvApi.categories
      .GetCategoriesThreeLevels()
      .then((res) => {
        setCategories(res.data);
        // finishRunning(false);
      })
      .catch((err: any) => {
        // finishRunning(true, err.message || "Failed to get categories");
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
        finishRunning(true, err.message || "Failed to remove item from compare cart");
      });
  }
  async function getProductsByProductIDs() {
    try {
      // Get base products
      if (!productSearchViewModel?.ResultIDs?.length) {
        setProducts([]);
        return;
      }
      setRunning();
      let newProducts = (
        await cvApi.products.GetProductsByIDs({
          IDs: usingInfiniteScroll
            ? productSearchViewModel?.ResultIDs?.slice(
                (productSearchViewModel?.Form?.Page - 1) *
                  productSearchViewModel?.Form?.PageSetSize,
                (productSearchViewModel?.Form?.Page - 1) *
                  productSearchViewModel?.Form?.PageSetSize +
                  productSearchViewModel?.Form?.PageSetSize
              )
            : productSearchViewModel?.ResultIDs
        })
      )?.data as ProductModel[];
      if (cefConfig.featureSet.inventory.enabled) {
        // Assign inventory
        newProducts = await inventoryFactory.bulkFactoryAssign(newProducts);
      }
      if (cefConfig.featureSet.pricing.enabled) {
        // Assign prices
        newProducts = await pricingFactory.bulkFactoryAssign(newProducts);
      }
      setProducts(usingInfiniteScroll ? [...products, ...newProducts] : newProducts);
      finishRunning();
    } catch (err) {
      console.log(err);
      finishRunning(true, "unable to fetch products or product related data");
    }
  }

  const navigateToPage = (page: number): void =>
    setProductSearchViewModel({
      ...productSearchViewModel,
      Form: { ...productSearchViewModel?.Form, Page: page }
    });

  const onChangeProductQuantity = (productId: number, newQuantity: number): void => {
    const productsCopy = [...products];
    const productToUpdate = productsCopy.find((pr: { ID: number }) => pr.ID === productId);
    // TODO quantity needs to be tracked in its own element, addToCartQuantitySelector
    // productToUpdate.quantity = newQuantity;
    setProducts(productsCopy);
  };

  const onChangeQueryParam = <T,>(param: keyof ProductCatalogSearchForm, newValue: T): void => {
    let form: ProductCatalogSearchForm;
    try {
      form = JSON.parse(JSON.stringify(productSearchViewModel?.Form));
    } catch (err) {
      return;
    }
    if (form[param] && !newValue) {
      delete form[param];
    }
    (form[param] as unknown) = newValue as T;
    if (param !== "Page") {
      form.Page = 1;
    }
    setProductSearchViewModel({
      ...productSearchViewModel,
      Form: form
    });
  };

  const renderProductsView = (): React.ReactNode => {
    if (shouldShowCategoryLandingPages()) {
      return null;
    }
    const viewProps = {
      products,
      useInfinite: usingInfiniteScroll,
      onScrollToBottom: () => {
        if (!viewState.running) {
          navigateToPage(productSearchViewModel?.Form?.Page + 1);
        }
      },
      parentRunning: viewState.running,
      onChangeProductQuantity
    };
    let element = <LoadingWidget />;
    if (!usingInfiniteScroll && viewState.running) {
      return element;
    }
    switch (productSearchViewModel?.Form?.PageFormat) {
      case "grid":
        element = <CatalogGridView {...viewProps} />;
        break;
      case "table":
        element = <CatalogTableView {...viewProps} />;
        break;
      case "list":
        element = <CatalogListView {...viewProps} />;
        break;
    }
    return element;
  };

  return (
    <>
      <ErrorView error={viewState.errorMessage} />
      <Row>
        <div className="col-tk-10 offset-tk-1 col-fk-8 offset-fk-2 col-lg-12 col-md col-sm col">
          <Breadcrumbs
            currentCategory={productSearchViewModel?.Form?.Category}
            currentQuery={productSearchViewModel?.Form?.Query}
            categoriesTree={productSearchViewModel?.CategoriesTree}
            onCategoryClicked={(cat: string) => onChangeQueryParam("Category", cat)}
          />
        </div>
      </Row>
      <Switch>
        <Route exact path="/catalog">
          <Row>
            <Col xs sm md lg={12} className="col-tk-10 offset-tk-1 col-fk-8 offset-fk-2">
              <Row>
                <CatalogFilters
                  allCategories={productSearchViewModel?.Categories}
                  allAttributes={productSearchViewModel?.Attributes}
                  brands={brands}
                  removeItemFromCompareCart={removeItemFromCompareCart}
                  productSearchViewModel={productSearchViewModel}
                  setProductSearchViewModel={setProductSearchViewModel}
                />
                <Col sm={12} md={8} lg={9} className="col-tk-10 col-fk-10">
                  <Row>
                    <Col>
                      {productSearchViewModel?.Form?.Query?.length ||
                      productSearchViewModel?.Form?.Category?.length ? (
                        <h1>
                          {t("ui.storefront.catalog.ShopResultsFor")}{" "}
                          <strong>
                            {productSearchViewModel?.Form?.Query?.length
                              ? productSearchViewModel?.Form?.Query
                              : productSearchViewModel?.Form?.Category.split("|")[0]}
                          </strong>
                        </h1>
                      ) : null}
                    </Col>
                  </Row>
                  <Row
                    className={`justify-content-between ${
                      shouldShowCategoryLandingPages() ? "d-none" : ""
                    }`}>
                    <Col xs={12} sm={4} md={4} lg={6} style={{ textAlign: "left" }}>
                      <Form.Group>
                        <Form.Label>{t("ui.storefront.common.View")}</Form.Label>
                        <InputGroup>
                          {formatOptions.map((option) => {
                            const { icon } = option;
                            return (
                              <Button
                                type="button"
                                variant={
                                  productSearchViewModel?.Form?.PageFormat === option.format
                                    ? "outline-primary"
                                    : ""
                                }
                                className="rounded"
                                key={option.format}
                                onClick={(e) =>
                                  onChangeQueryParam<string>("PageFormat", option.format)
                                }>
                                <FontAwesomeIcon icon={icon} className="fa-lg" aria-hidden="true" />
                                <span className="text-capitalize">&nbsp;{option.format}</span>
                              </Button>
                            );
                          })}
                        </InputGroup>
                      </Form.Group>
                    </Col>
                    <Col
                      xs={6}
                      sm={4}
                      md={4}
                      lg={2}
                      className="xs-text-right sm-text-center md-text-center lg-text-left">
                      {!usingInfiniteScroll && (
                        <Form.Group>
                          <Form.Label>
                            {t("ui.storefront.product.catalog.controls.itemsPerPage.Show")}
                          </Form.Label>
                          <InputGroup className="flex-nowrap">
                            {sizeOptions.map((num): JSX.Element => {
                              return (
                                <Button
                                  variant={
                                    productSearchViewModel?.Form?.PageSize === num
                                      ? "outline-primary"
                                      : ""
                                  }
                                  className="rounded"
                                  type="button"
                                  key={num.toString()}
                                  onClick={() => onChangeQueryParam<number>("PageSize", num)}>
                                  {num}
                                </Button>
                              );
                            })}
                          </InputGroup>
                        </Form.Group>
                      )}
                    </Col>
                    <Col
                      xs={12}
                      sm={4}
                      md={4}
                      lg={3}
                      className="xs-text-center sm-text-right md-text-right lg-text-left">
                      <div className="items-searchCatalogResultsSortCtrl-option">
                        <Form.Group>
                          <Form.Label>
                            <span>
                              {t(
                                "ui.storefront.product.catalog.controls.searchCatalogResultsSortCtrl.sortBy"
                              )}
                            </span>
                          </Form.Label>
                          <InputGroup>
                            <select
                              className="form-control d-inline-block"
                              id="sort"
                              aria-label="sort"
                              value={productSearchViewModel?.Form?.Sort}
                              onChange={(e) => onChangeQueryParam<string>("Sort", e.target.value)}>
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
                          </InputGroup>
                        </Form.Group>
                      </div>
                    </Col>
                  </Row>
                  <Row className="my-2">
                    <Col>
                      <ErrorView error={viewState.errorMessage} />
                    </Col>
                  </Row>
                  {shouldShowCategoryLandingPages() && productSearchViewModel?.Form?.Category ? (
                    <CategoryLandingPageView
                      categories={categories}
                      paramsCategory={productSearchViewModel?.Form?.Category}
                      onCategoryClicked={(cat: string) => onChangeQueryParam("Category", cat)}
                    />
                  ) : null}
                  {renderProductsView()}
                  {!usingInfiniteScroll && !shouldShowCategoryLandingPages() && (
                    <CatalogPaginationControls
                      page={productSearchViewModel.Form.Page}
                      pageSize={productSearchViewModel.Form.PageSize}
                      total={productSearchViewModel.Total}
                      totalPages={productSearchViewModel.TotalPages}
                      navigateToPage={navigateToPage}
                    />
                  )}
                </Col>
              </Row>
            </Col>
          </Row>
        </Route>
        <Route path="/catalog/compare">
          <Compare removeItemFromCompareCart={removeItemFromCompareCart} />
        </Route>
      </Switch>
    </>
  );
};
