import { CEFConfig } from "../../../_redux/_reduxTypes";
import { CartModel, UserModel } from "../../../_api/cvApi._DtoClasses";
import { CalculatedInventories } from "../../../_api/cvApi.shared";
export interface IProductCardControlsWidgetProps {
  product: any;
  inventory?: CalculatedInventories;
  render?: "overlay" | "horizontal" | "vertical";
  hideFavoritesList?: boolean;
  hideWishList?: boolean;
  hideNotifyMe?: boolean;
  hideCompare?: boolean;
  hideI?: boolean;
  hideShoppingList?: boolean;
  shoppingList?: CartModel; // redux
  favoritesList?: CartModel; // redux
  wishList?: CartModel; // redux
  notifyMe?: CartModel; // redux
  compareCart?: CartModel; // redux
  currentUser: UserModel; // redux
  cefConfig: CEFConfig; // redux
  refreshStaticCart?: Function;
  refreshCompareList?: Function;
}

export interface IPCCWLayoutProps {
  product: any;
  items: Array<any>;
}

export interface IProductCardControl {
  hide: boolean;
  idPrefix: string;
  titleKey: string;
  onClick: () => void;
  icon?: JSX.Element;
  isSelected: boolean;
}
