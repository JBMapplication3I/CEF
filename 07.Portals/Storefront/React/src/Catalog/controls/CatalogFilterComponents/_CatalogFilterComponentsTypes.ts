import { IconProp } from "@fortawesome/fontawesome-svg-core";
import {
  KeyValuePair,
  ProductSearchViewModel
} from "../../../_api/cvApi.shared";
import {
  AggregatePricingRange,
  CategoryModel,
  AggregateTree,
  AggregateRatingRange
} from "../../../_api/cvApi._DtoClasses";

export interface ICatalogFilterHeaderButtonProps {
  icon: IconProp;
  expandedFilterName: string | null;
  setExpandedFilterName: Function;
  title: string;
}

interface ICatalogFilterComponentProps {
  expandedFilterName: string | null;
  setExpandedFilterName: Function;
  productSearchViewModel: ProductSearchViewModel;
  setProductSearchViewModel: React.Dispatch<
    React.SetStateAction<ProductSearchViewModel>
  >;
}

export interface IAttributesFilterProps extends ICatalogFilterComponentProps {
  allAttributes: Array<KeyValuePair<string, KeyValuePair<string, number>[]>>;
}

export interface IBrandsFilterProps extends ICatalogFilterComponentProps {
  brands: Array<[string, number]>;
}

export interface ICategoriesFilterProps extends ICatalogFilterComponentProps {
  categoriesTree: AggregateTree;
  allCategories?: Array<CategoryModel>;
}

export interface ICompareFilterProps extends ICatalogFilterComponentProps {
  compareCartItems?: Array<any>; // redux
  removeItemFromCompareCart: Function;
}

export interface IPriceRangesFilterProps extends ICatalogFilterComponentProps {
  pricingRanges: AggregatePricingRange[];
}

export interface IRatingRangesFilterProps extends ICatalogFilterComponentProps {
  ratingRanges: AggregateRatingRange[];
}

export interface ISearchTermFilterProps extends ICatalogFilterComponentProps {}
