/**
 * @file React/src/_shared/customHooks/catalog/useLotSearchViewModel.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc useLotSearchViewModel hook
 */
import { useState } from 'react';
import useCustomCompareEffect from "../useCustomCompareEffect";
import { useCefConfig } from "../useCefConfig";
import {
  useQueryParams,
  StringParam,
  NumberParam,
  ArrayParam,
  DelimitedNumericArrayParam,
  withDefault,
  UrlUpdateType,
} from 'use-query-params';
import { QueryParamConfig, QueryParamConfigMap, DecodedValueMap } from 'serialize-query-params';
import { LotCatalogSearchForm, SearchSort } from '../../../_api/cvApi._DtoClasses';
import { LotSearchViewModel, Dictionary } from "../../../_api/cvApi.shared";
import { CEFConfig } from "../../../_redux/_reduxTypes";

const CustomJsonParam: QueryParamConfig<string, Dictionary<string[]>> = {
  encode: function(any) {
    if (any == null) {
        return any;
    }
    let result = JSON.stringify(any);
    // replace double quotes with single
    result = result.replace(/"/g, "'");
    return result;
  },
  decode: function(input) {
    function getEncodedValue(input: string | string[]): string {
      if (input === undefined) {
        return input as string;
      }
      if (input === null) {
          return input.toString();
      }
      if (input === '') {
          return null;
      }
      if (!(input instanceof Array)) {
        // replace single quotes with double quotes
        input = input.replace(/'/g, '"');
      }
      return input.toString();
    }
    let jsonStr = getEncodedValue(input);
    if (jsonStr == null)
        return jsonStr;
    let result = null;
    try {
        result = JSON.parse(jsonStr);
    }
    catch (e) {
        /* ignore errors, returning undefined */
    }
    return result;
  },
}

interface IParamsForSearchCatalogQPCMap extends QueryParamConfigMap {
  [paramName: string]: QueryParamConfig<any, any>;
  format: typeof StringParam;
  page: typeof NumberParam;
  size: typeof NumberParam;
  sort: typeof StringParam;
  name?: typeof StringParam;
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

export function areLotCatalogSearchFormsEqual(previousForm: LotCatalogSearchForm, currentForm: LotCatalogSearchForm): boolean {
  // TODO@JDW find a better way to compare object/array params
  return  previousForm &&
          previousForm.PageFormat                     ===  currentForm.PageFormat &&
          previousForm.Page                           ===  currentForm.Page &&
          previousForm.PageSize                       ===  currentForm.PageSize &&
          previousForm.Sort                           ===  currentForm.Sort &&
          previousForm.Query                          ===  currentForm.Query &&
          previousForm.Category                       ===  currentForm.Category &&
          JSON.stringify(previousForm.CategoriesAny)  ===  JSON.stringify(currentForm.CategoriesAny) &&
          JSON.stringify(previousForm.CategoriesAll)  ===  JSON.stringify(currentForm.CategoriesAll) &&
          previousForm.StoreID                        ===  currentForm.StoreID &&
          previousForm.Name                           ===  currentForm.Name &&
          JSON.stringify(previousForm.StoreIDsAny)    ===  JSON.stringify(currentForm.StoreIDsAny) &&
          JSON.stringify(previousForm.StoreIDsAll)    ===  JSON.stringify(currentForm.StoreIDsAll) &&
          JSON.stringify(previousForm.PricingRanges)  ===  JSON.stringify(currentForm.PricingRanges) &&
          JSON.stringify(previousForm.AttributesAny)  ===  JSON.stringify(currentForm.AttributesAny) &&
          JSON.stringify(previousForm.AttributesAll)  ===  JSON.stringify(currentForm.AttributesAll) &&
          previousForm.PageSetSize                    ===  currentForm.PageSetSize;
}

function defaultLotSearchViewModel(cefConfig: CEFConfig, params: DecodedValueMap<IParamsForSearchCatalogQPCMap>): LotSearchViewModel {
  const searchForm: LotCatalogSearchForm = {
    PageFormat: params.format || cefConfig?.catalog.defaultFormat || "grid",
    Page: params.page || 1,
    PageSize: params.size || cefConfig?.catalog.defaultPageSize || 9,
    // TODO: figure out how to type enum<>string
    Sort: params.sort as any || cefConfig.catalog.defaultSort || SearchSort.Relevance,
    Query: params.term,
    Category: params.category,
    CategoriesAny: params.categoriesAny,
    Name: params.name,
    CategoriesAll: params.categoriesAll,
    StoreID: params.storeId,
    StoreIDsAny: params.storeIdsAny,
    StoreIDsAll: params.storeIdsAll,
    PricingRanges: params.pricingRanges,
    AttributesAny: params.attributesAny,
    AttributesAll: params.attributesAll,
    PageSetSize: 5
  }
  Object.keys(searchForm).forEach(key => {
    // TODO:@JDW Figure out how to have solid types while still having a string indexer
    // @ts-ignore
    if ((searchForm[key]) === undefined || searchForm[key] === '') {
      // @ts-ignore
      delete (searchForm[key]);
    }
  });
  return {
    // Base
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
    BrandNames: null,
  }
}

function defaultViewModelParams(cefConfig: CEFConfig): IParamsForSearchCatalogQPCMap {
  return {
    format: withDefault(StringParam, cefConfig ? cefConfig.catalog.defaultFormat : "grid"),
    page: withDefault(NumberParam, 1),
    size: withDefault(NumberParam, cefConfig ? cefConfig.catalog.defaultPageSize : 9),
    sort: withDefault(StringParam, (cefConfig ? (cefConfig.catalog.defaultSort?.toString()) : SearchSort.Relevance.toString()) ),
    name: StringParam,
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
    location: StringParam,
  } as IParamsForSearchCatalogQPCMap;
}

function viewModelToParams(lotSearchViewModel: LotSearchViewModel) {
  // have access to lotSearchViewModel and latestValues
  return (latestParams: DecodedValueMap<IParamsForSearchCatalogQPCMap>): DecodedValueMap<IParamsForSearchCatalogQPCMap> => {
    if (!lotSearchViewModel) {
      return latestParams;
    }
    const configMap: DecodedValueMap<IParamsForSearchCatalogQPCMap> = {
      format: lotSearchViewModel?.Form?.PageFormat,
      page: lotSearchViewModel?.Form?.Page,
      size: lotSearchViewModel?.Form?.PageSize,
      sort: lotSearchViewModel?.Form?.Sort?.toString(),
      name: lotSearchViewModel?.Form?.Name,
      term: lotSearchViewModel?.Form?.Query,
      category: lotSearchViewModel?.Form?.Category,
      categoriesAny: lotSearchViewModel?.Form?.CategoriesAny,
      categoriesAll: lotSearchViewModel?.Form?.CategoriesAll,
      storeId: lotSearchViewModel?.Form?.StoreID,
      storeIdsAny: lotSearchViewModel?.Form?.StoreIDsAny,
      storeIdsAll: lotSearchViewModel?.Form?.StoreIDsAll,
      priceRanges: lotSearchViewModel?.Form?.PricingRanges,
      attributesAny: lotSearchViewModel?.Form?.AttributesAny,
      attributesAll: lotSearchViewModel?.Form?.AttributesAll,
    }
    return configMap;
  }; 
}

export const useLotSearchViewModel = (): [LotSearchViewModel, React.Dispatch<React.SetStateAction<LotSearchViewModel>>] => {
  const cefConfig: CEFConfig = useCefConfig();
  const [params, setParams] = useQueryParams(defaultViewModelParams(cefConfig));
  const [lotSearchViewModel, setLotSearchViewModel] = useState<LotSearchViewModel>(defaultLotSearchViewModel(cefConfig, params));

  useCustomCompareEffect(() => {
    // form to params
    setParams(viewModelToParams(lotSearchViewModel), "push" as UrlUpdateType);
  }, lotSearchViewModel, (previous, current): boolean => {
    let isEqual = JSON.stringify(previous) === JSON.stringify(current)
    return isEqual;
  }
  );
  return [lotSearchViewModel as LotSearchViewModel, setLotSearchViewModel];
};
