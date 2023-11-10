import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFilter, faTimes, faStar } from "@fortawesome/free-solid-svg-icons";
import { Button, ListGroup, Card, Col, Row } from "react-bootstrap";
import {
  SearchTermFilter,
  SellerRatingsFilter,
  CategoriesFilter,
  AttributesFilter,
  BrandsFilter,
  PriceRangesFilter,
  CompareFilter
} from "./CatalogFilterComponents";
import { useTranslation } from "react-i18next";
import { KeyValuePair, ProductSearchViewModel } from "../../_api/cvApi.shared";
import { CategoryModel, ProductCatalogSearchForm } from "../../_api/cvApi._DtoClasses";
import { useCefConfig } from "../../_shared/customHooks/useCefConfig";
import { CEFConfig } from "../../_redux/_reduxTypes";

interface ICatalogFiltersProps {
  allCategories: Array<CategoryModel>;
  allAttributes: Array<KeyValuePair<string, KeyValuePair<string, number>[]>>;
  compareCartItems?: Array<any>; // redux
  removeItemFromCompareCart: Function;
  brands: Array<[string, number]>;
  productSearchViewModel: ProductSearchViewModel;
  setProductSearchViewModel: React.Dispatch<React.SetStateAction<ProductSearchViewModel>>;
}

export const CatalogFilters = (props: ICatalogFiltersProps): JSX.Element => {
  const {
    allCategories,
    allAttributes,
    compareCartItems,
    brands,
    removeItemFromCompareCart,
    productSearchViewModel,
    setProductSearchViewModel
  } = props;

  const cefConfig: CEFConfig = useCefConfig();
  const { t } = useTranslation();
  const [expandedFilterName, setExpandedFilterName] = useState(null);

  const onClickRemoveParam = (
    param: keyof ProductCatalogSearchForm | Array<keyof ProductCatalogSearchForm>,
    paramKey?: string
  ): void => {
    let form: ProductCatalogSearchForm;
    try {
      form = JSON.parse(JSON.stringify(productSearchViewModel?.Form));
    } catch (err) {
      return;
    }
    if (!(param instanceof Array)) {
      if (form[param]) {
        delete form[param];
      }
    } else {
      for (let i = 0; i < param.length; i++) {
        const p = param[i];
        if (form[p]) {
          delete form[p];
        }
      }
    }
    form.Page = 1;
    setProductSearchViewModel({ ...productSearchViewModel, Form: form });
  };

  const filterProps = {
    setExpandedFilterName,
    expandedFilterName,
    productSearchViewModel,
    setProductSearchViewModel
  };

  const filterParamsExist =
    !!productSearchViewModel?.Form?.Category ||
    !!productSearchViewModel?.Form?.Query ||
    !!productSearchViewModel?.Form?.RatingRanges ||
    !!productSearchViewModel?.Form?.PricingRanges ||
    !!productSearchViewModel?.Form?.BrandName ||
    !!productSearchViewModel?.Form?.AttributesAny ||
    !!productSearchViewModel?.Form?.AttributesAll;

  return (
    <Col sm={12} md={4} lg={3} className="col-tk-2 col-fk-2">
      <Row className="clarity-filters">
        <Col xs={12} sm={6} md={12}>
          <Card className="applied-filters mb-3">
            <Card.Header className="p-2 bg-light text-body">
              <FontAwesomeIcon icon={faFilter} className="fa-fw mr-2" />
              <span className="filter-title">
                {t("ui.storefront.searchCatalog.filers.YourAppliedFilters")}
              </span>
            </Card.Header>
            {!filterParamsExist ? (
              <ListGroup
                as="ul"
                variant="flush"
                className="bg-light"
                style={{ maxHeight: "50vh", overflowY: "auto" }}>
                <ListGroup.Item className="p-2">
                  {t("ui.storefront.searchCatalog.filters.None")}
                </ListGroup.Item>
              </ListGroup>
            ) : null}
            {productSearchViewModel?.Form?.Category && (
              <ListGroup
                as="ul"
                variant="flush"
                className="bg-light"
                style={{ maxHeight: "50vh", overflowY: "auto" }}>
                <ListGroup.Item className="p-2">
                  <Card.Title className="mb-1">
                    {t("ui.storefront.common.Category.Plural")}
                  </Card.Title>
                </ListGroup.Item>
                <ListGroup.Item className="p-2">
                  <Button
                    variant=""
                    className="p-0 w-100 bg-white border-0 text-start"
                    onClick={(e) => onClickRemoveParam("Category")}>
                    <FontAwesomeIcon icon={faTimes} className="text-danger mr-2" />
                    {productSearchViewModel?.Form?.Category.split("|")[0]}
                  </Button>
                </ListGroup.Item>
              </ListGroup>
            )}
            {productSearchViewModel?.Form?.Query && (
              <ListGroup
                as="ul"
                variant="flush"
                className="bg-light"
                style={{ maxHeight: "50vh", overflowY: "visible" }}>
                <ListGroup.Item className="p-2">
                  <Card.Title className="mb-1">
                    {t("ui.storefront.searchCatalog.filters.SearchTerm")}
                  </Card.Title>
                </ListGroup.Item>
                <ListGroup.Item className="p-2">
                  <Button
                    variant=""
                    className="p-0 w-100 text-start"
                    onClick={() => onClickRemoveParam("Query")}>
                    <FontAwesomeIcon icon={faTimes} className="text-danger mr-2" />
                    {productSearchViewModel?.Form?.Query}
                  </Button>
                </ListGroup.Item>
              </ListGroup>
            )}
            {productSearchViewModel?.Form?.RatingRanges && (
              <ListGroup
                as="ul"
                variant="flush"
                className="bg-light"
                style={{ maxHeight: "50vh", overflowY: "auto" }}>
                <ListGroup.Item className="p-2">
                  <Card.Title className="mb-1">
                    {t("ui.storefront.searchCatalog.filters.RatingRanges.Plural")}
                  </Card.Title>
                </ListGroup.Item>
                {productSearchViewModel?.Form?.RatingRanges?.length
                  ? productSearchViewModel.Form.RatingRanges.map((range: string, index: number) => {
                      return (
                        <ListGroup.Item key={index}>
                          <Button
                            variant="" // intentionally blank
                            className="p-0"
                            onClick={() => onClickRemoveParam("RatingRanges")}>
                            <FontAwesomeIcon icon={faStar} className="text-danger mr-2" />
                            {range}
                          </Button>
                        </ListGroup.Item>
                      );
                    })
                  : null}
              </ListGroup>
            )}
            {productSearchViewModel?.Form?.PricingRanges && (
              <ListGroup
                as="ul"
                variant="flush"
                className="bg-light"
                style={{ maxHeight: "50vh", overflowY: "auto" }}>
                <ListGroup.Item className="p-2">
                  <Card.Title className="card-title mb-1">
                    {t("ui.storefront.searchCatalog.filters.PriceRange.Plural")}
                  </Card.Title>
                </ListGroup.Item>
                <ListGroup.Item className="p-2">
                  <Button
                    variant=""
                    style={{ textAlign: "left" }}
                    className="p-0 w-100 bg-white"
                    onClick={() => onClickRemoveParam("PricingRanges")}>
                    <FontAwesomeIcon icon={faTimes} className="text-danger mr-2" />
                    {productSearchViewModel?.Form?.PricingRanges}
                  </Button>
                </ListGroup.Item>
              </ListGroup>
            )}
            {productSearchViewModel?.Form?.BrandName && (
              <ListGroup
                as="ul"
                variant="flush"
                className="bg-light"
                style={{ maxHeight: "50vh", overflowY: "auto" }}>
                <ListGroup.Item className="p-2">
                  <Card.Title className="mb-1">
                    {t("ui.storefront.searchCatalog.filters.BrandName")}
                  </Card.Title>
                </ListGroup.Item>
                <ListGroup.Item className="p-2">
                  <Button
                    variant=""
                    style={{ textAlign: "left" }}
                    className="p-0 w-100 bg-white"
                    onClick={() => onClickRemoveParam("BrandName")}>
                    <FontAwesomeIcon icon={faTimes} className="text-danger mr-2" />
                    {productSearchViewModel?.Form?.BrandName}
                  </Button>
                </ListGroup.Item>
              </ListGroup>
            )}
            {(productSearchViewModel?.Form?.AttributesAll ||
              productSearchViewModel?.Form?.AttributesAny) && (
              <ListGroup
                as="ul"
                variant="flush"
                className="bg-light"
                style={{ maxHeight: "50vh", overflowY: "auto" }}>
                <ListGroup className="p-2">
                  <Card.Title className="mb-1">
                    {t("ui.storefront.common.Attribute.Plural")}
                  </Card.Title>
                </ListGroup>
                {Object.keys(productSearchViewModel?.Form?.AttributesAll).map(
                  (key): JSX.Element => {
                    return (
                      <ListGroup.Item className="p-2" key={key}>
                        <a
                          className="mr-1 pointer"
                          href="#"
                          key={key}
                          onClick={(e) => onClickRemoveParam("AttributesAll", key)}>
                          &nbsp;
                          <small className="position-relative" style={{ top: "-1px" }}>
                            <FontAwesomeIcon icon={faTimes} className="text-danger mr-1" />
                          </small>
                          {key}
                        </a>
                      </ListGroup.Item>
                    );
                  }
                )}
              </ListGroup>
            )}
            <Card.Footer className="p-1">
              <Button
                variant="secondary"
                size="sm"
                className="w-100"
                id="btnClearAllFilters"
                disabled={!filterParamsExist}
                onClick={() => {
                  const paramsToClear: Array<keyof ProductCatalogSearchForm> = [
                    "AttributesAll",
                    "Category",
                    "Query",
                    "RatingRanges",
                    "PricingRanges",
                    "BrandName"
                  ];
                  onClickRemoveParam(paramsToClear);
                }}>
                {t("ui.storefront.searchCatalog.filters.ClearAllFilters")}
              </Button>
            </Card.Footer>
          </Card>
        </Col>
        <SearchTermFilter {...filterProps} />
        {cefConfig?.featureSet?.reviews?.enabled && (
          <SellerRatingsFilter
            {...filterProps}
            ratingRanges={productSearchViewModel?.RatingRanges}
          />
        )}
        {cefConfig?.featureSet?.categories?.enabled && (
          <CategoriesFilter
            {...filterProps}
            categoriesTree={productSearchViewModel?.CategoriesTree}
            allCategories={allCategories}
          />
        )}
        <AttributesFilter {...filterProps} allAttributes={allAttributes} />
        {cefConfig?.featureSet?.pricing?.enabled && (
          <PriceRangesFilter
            {...filterProps}
            pricingRanges={productSearchViewModel?.PricingRanges}
          />
        )}
        {/* TODO */}
        <BrandsFilter {...filterProps} brands={brands} />
        {cefConfig?.featureSet?.carts?.compare?.enabled && (
          <CompareFilter {...filterProps} removeItemFromCompareCart={removeItemFromCompareCart} />
        )}
      </Row>
    </Col>
  );
};
