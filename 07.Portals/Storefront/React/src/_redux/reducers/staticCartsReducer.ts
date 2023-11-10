import { CartModel } from "../../_api/cvApi._DtoClasses";
import { ServiceStrings } from "../../_shared/ServiceStrings";
import { IStaticCartsReducer } from "../_reduxTypes";

const initialState: IStaticCartsReducer = {
  shoppingList: null,
  favoritesList: null,
  wishList: null,
  notifyMeList: null,
  compareList: null
};

export const staticCartsReducer = (
  state: IStaticCartsReducer = initialState,
  action: { type: string; payload: CartModel }
): IStaticCartsReducer => {
  switch (action.type) {
    case ServiceStrings.redux.actionTypes.staticCarts.setFavoritesList:
      return {
        ...state,
        favoritesList: action.payload
      };
    case ServiceStrings.redux.actionTypes.staticCarts.setWishList:
      return {
        ...state,
        wishList: action.payload
      };
    case ServiceStrings.redux.actionTypes.staticCarts.setNotifyMeList:
      return {
        ...state,
        notifyMeList: action.payload
      };
    case ServiceStrings.redux.actionTypes.staticCarts.setCompareList:
      return {
        ...state,
        compareList: action.payload
      };
    default:
      return state;
  }
};
