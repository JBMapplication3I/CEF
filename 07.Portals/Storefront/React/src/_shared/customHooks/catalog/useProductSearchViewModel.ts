/**
 * @file React/src/_shared/customHooks/catalog/useProductSearchViewModel.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc useProductSearchViewModel hook
 */
import { useState } from "react";
import useCustomCompareEffect from "../useCustomCompareEffect";
import { useCefConfig } from "../useCefConfig";
import {
  useQueryParams,
  StringParam,
  NumberParam,
  ArrayParam,
  DelimitedNumericArrayParam,
  withDefault,
  UrlUpdateType
} from "use-query-params";
import {
  QueryParamConfig,
  QueryParamConfigMap,
  DecodedValueMap
} from "serialize-query-params";
import {
  ProductCatalogSearchForm,
  SearchSort
} from "../../../_api/cvApi._DtoClasses";
import { ProductSearchViewModel, Dictionary } from "../../../_api/cvApi.shared";
import { CEFConfig } from "../../../_redux/_reduxTypes";

const CustomJsonParam: QueryParamConfig<string, Dictionary<string[]>> = {
  encode: function (any) {
    if (any == null) {
      return any;
    }
    let result = JSON.stringify(any);
    // replace double quotes with single
    result = result.replace(/"/g, "'");
    return result;
  },
  decode: function (input) {
    function getEncodedValue(input: string | string[]): string {
      if (input === undefined) {
        return input as string;
      }
      if (input === null) {
        return input.toString();
      }
      if (input === "") {
        return null;
      }
      if (!(input instanceof Array)) {
        // replace single quotes with double quotes
        input = input.replace(/'/g, '"');
      }
      return input.toString();
    }
    let jsonStr = getEncodedValue(input);
    if (jsonStr == null) return jsonStr;
    let result = null;
    try {
      result = JSON.parse(jsonStr);
    } catch (e) {
      /* ignore errors, returning undefined */
    }
    return result;
  }
};

interface IParamsForSearchCatalogQPCMap extends QueryParamConfigMap {
  [paramName: string]: QueryParamConfig<any, any>;
  format: typeof StringParam;
  page: typeof NumberParam;
  size: typeof NumberParam;
  sort: typeof StringParam;
  term?: typeof StringParam;
  category?: typeof StringParam;
  brandName?: typeof StringParam;
  categoriesAny?: typeof ArrayParam;
  categoriesAll?: typeof ArrayParam;
  storeId?: typeof NumberParam;
  storeIdsAny?: typeof DelimitedNumericArrayParam;
  storeIdsAll?: typeof DelimitedNumericArrayParam;
  priceRanges?: typeof ArrayParam;
  attributesAny?: typeof CustomJsonParam;
  attributesAll?: typeof CustomJsonParam;
}

export function compareProductCatalogSearchForm(
  previousForm: ProductCatalogSearchForm,
  currentForm: ProductCatalogSearchForm
): boolean {
  // TODO@JDW find a better way to compare object/array params
  if (previousForm.PageFormat                               === currentForm.PageFormat
    && previousForm.Page                                  === currentForm.Page
    && previousForm.PageSize                              === currentForm.PageSize
    && previousForm.Sort                                  === currentForm.Sort
    && previousForm.Query                                 === currentForm.Query
    && previousForm.Category                              === currentForm.Category
    && previousForm.BrandName                             === currentForm.BrandName
    && JSON.stringify(previousForm.CategoriesAny)         === JSON.stringify(currentForm.CategoriesAny)
    && JSON.stringify(previousForm.CategoriesAll)         === JSON.stringify(currentForm.CategoriesAll)
    && previousForm.StoreID                               === currentForm.StoreID
    && JSON.stringify(previousForm.StoreIDsAny)           === JSON.stringify(currentForm.StoreIDsAny)
    && JSON.stringify(previousForm.StoreIDsAll)           === JSON.stringify(currentForm.StoreIDsAll)
    && JSON.stringify(previousForm.PricingRanges)           === JSON.stringify(currentForm.PricingRanges)
    && JSON.stringify(previousForm.RatingRanges)           === JSON.stringify(currentForm.RatingRanges)
    && JSON.stringify(previousForm.AttributesAny)         === JSON.stringify(currentForm.AttributesAny)
    && JSON.stringify(previousForm.AttributesAll)         === JSON.stringify(currentForm.AttributesAll)
    && previousForm.PageSetSize                           === currentForm.PageSetSize) {
    return true;
  }
  return false;
}

function defaultProductSearchViewModel(
  cefConfig: CEFConfig,
  params: DecodedValueMap<IParamsForSearchCatalogQPCMap>
): ProductSearchViewModel {
  const searchForm: ProductCatalogSearchForm = {
    PageFormat: params.format || cefConfig?.catalog.defaultFormat || "grid",
    Page: params.page || 1,
    PageSize: params.size || cefConfig?.catalog.defaultPageSize || 9,
    // TODO: figure out how to type enum<>string
    Sort:
      (params.sort as any) ||
      cefConfig.catalog.defaultSort ||
      SearchSort.Relevance,
    Query: params.term,
    Category: params.category,
    BrandName: params.brandName,
    CategoriesAny: params.categoriesAny,
    CategoriesAll: params.categoriesAll,
    StoreID: params.storeId,
    StoreIDsAny: params.storeIdsAny,
    StoreIDsAll: params.storeIdsAll,
    PricingRanges: params.pricingRanges,
    RatingRanges: params.ratingRanges,
    AttributesAny: params.attributesAny,
    AttributesAll: params.attributesAll,
    PageSetSize: 5
  };
  Object.keys(searchForm).forEach((key) => {
    // TODO:@JDW Figure out how to have solid types while still having a string indexer
    // @ts-ignore
    if (searchForm[key] === undefined || searchForm[key] === "") {
      // @ts-ignore
      delete searchForm[key];
    }
  });
  return {
    // Base
    BrandNames: null,
    Total: 0,
    TotalPages: 0,
    ServerError: null,
    DebugInformation: null,
    IsValid: false,
    Documents: [],
    ElapsedMilliseconds: 0,
    Form: searchForm,
    // Products-Specific
    Attributes: null,
    BrandIDs: null,
    CategoriesTree: null,
    FranchiseIDs: null,
    ManufacturerIDs: null,
    RatingRanges: null,
    PricingRanges: null,
    ResultIDs: null,
    ProductIDs: null,
    StoreIDs: null,
    Types: null,
    VendorIDs: null,
  };
}

function defaultViewModelParams(
  cefConfig: CEFConfig
): IParamsForSearchCatalogQPCMap {
  return {
    format: withDefault(StringParam, cefConfig ? cefConfig.catalog.defaultFormat : "grid"),
    page: withDefault(NumberParam, 1),
    size: withDefault(NumberParam, cefConfig ? cefConfig.catalog.defaultPageSize : 9),
    sort: withDefault(
      StringParam,
      cefConfig
        ? cefConfig.catalog.defaultSort?.toString()
        : SearchSort.Relevance.toString()
    ),
    term: StringParam,
    category: StringParam,
    brandName: StringParam,
    categoriesAny: ArrayParam,
    categoriesAll: ArrayParam,
    storeId: NumberParam,
    storeIdsAny: DelimitedNumericArrayParam,
    storeIdsAll: DelimitedNumericArrayParam,
    priceRanges: ArrayParam,
    attributesAny: CustomJsonParam,
    attributesAll: CustomJsonParam,
    location: StringParam
  } as IParamsForSearchCatalogQPCMap;
}

function viewModelToParams(productSearchViewModel: ProductSearchViewModel) {
  // have access to productSearchViewModel and latestValues
  return (
    latestParams: DecodedValueMap<IParamsForSearchCatalogQPCMap>
  ): DecodedValueMap<IParamsForSearchCatalogQPCMap> => {
    if (!productSearchViewModel) {
      return latestParams;
    }
    const configMap: DecodedValueMap<IParamsForSearchCatalogQPCMap> = {
      format: productSearchViewModel?.Form?.PageFormat,
      page: productSearchViewModel?.Form?.Page,
      size: productSearchViewModel?.Form?.PageSize,
      sort: productSearchViewModel?.Form?.Sort?.toString(),
      term: productSearchViewModel?.Form?.Query,
      category: productSearchViewModel?.Form?.Category,
      brandName: productSearchViewModel?.Form?.BrandName,
      categoriesAny: productSearchViewModel?.Form?.CategoriesAny,
      categoriesAll: productSearchViewModel?.Form?.CategoriesAll,
      storeId: productSearchViewModel?.Form?.StoreID,
      storeIdsAny: productSearchViewModel?.Form?.StoreIDsAny,
      storeIdsAll: productSearchViewModel?.Form?.StoreIDsAll,
      priceRanges: productSearchViewModel?.Form?.PricingRanges,
      ratingRanges: productSearchViewModel?.Form?.RatingRanges,
      attributesAny: productSearchViewModel?.Form?.AttributesAny,
      attributesAll: productSearchViewModel?.Form?.AttributesAll
    };
    return configMap;
  };
}

export const useProductSearchViewModel = (): [
  ProductSearchViewModel,
  React.Dispatch<React.SetStateAction<ProductSearchViewModel>>
] => {
  const cefConfig: CEFConfig = useCefConfig();
  const [params, setParams] = useQueryParams(defaultViewModelParams(cefConfig));
  const [productSearchViewModel, setProductSearchViewModel] =
    useState<ProductSearchViewModel>(
      defaultProductSearchViewModel(cefConfig, params)
    );

  useCustomCompareEffect(
    () => {
      // form to params
      setParams(viewModelToParams(productSearchViewModel), "push" as UrlUpdateType);
    },
    productSearchViewModel,
    (previous, current): boolean => {
      let isEqual = JSON.stringify(previous) === JSON.stringify(current);
      return isEqual;
    }
  );
  return [
    productSearchViewModel as ProductSearchViewModel,
    setProductSearchViewModel
  ];
};
