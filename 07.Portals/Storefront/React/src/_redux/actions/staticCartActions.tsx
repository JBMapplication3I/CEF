import cvApi from "../../_api/cvApi";
import { store } from "../store/store";
import { ServiceStrings } from "../../_shared/ServiceStrings";

const { dispatch } = store;

export const setFavoritesList = (list: Array<any>): void => {
  dispatch({ type: ServiceStrings.redux.actionTypes.staticCarts.setFavoritesList, payload: list });
};

export const setWishList = (list: Array<any>): void => {
  dispatch({ type: ServiceStrings.redux.actionTypes.staticCarts.setWishList, payload: list });
};

export const setNotifyMeList = (list: Array<any>): void => {
  dispatch({ type: ServiceStrings.redux.actionTypes.staticCarts.setNotifyMeList, payload: list });
};

export const setProductAlertList = (list: Array<any>): void => {
  dispatch({
    type: ServiceStrings.redux.actionTypes.staticCarts.setProductAlertList,
    payload: list
  });
};

export const setCompareList = (list: Array<any>): void => {
  dispatch({ type: ServiceStrings.redux.actionTypes.staticCarts.setCompareList, payload: list });
};

export const refreshCompareList = () => {
  cvApi.shopping
    .GetCurrentCompareCart()
    .then((res) => {
      if (!res.data || !res.data.SalesItems) {
        console.log("Failed to get compare cart");
        return;
      }
      setCompareList(res.data.SalesItems);
    })
    .catch((err: any) => {
      console.log(err);
    });
};

export const refreshStaticCart = (TypeName: string) => {
  cvApi.shopping.GetCurrentStaticCart({ TypeName }).then((res: any) => {
    const typeSalesItems = res.data.SalesItems;
    let type: string;
    switch (TypeName) {
      case "Favorites List":
        type = ServiceStrings.redux.actionTypes.staticCarts.setFavoritesList;
        break;
      case "Wish List":
        type = ServiceStrings.redux.actionTypes.staticCarts.setWishList;
        break;
      case "Notify Me When In Stock":
        type = ServiceStrings.redux.actionTypes.staticCarts.setNotifyMeList;
        break;
    }
    if (!type) {
      console.log("Cannot refresh static cart of type " + TypeName);
      return;
    }
    dispatch({
      type,
      payload: typeSalesItems
    });
  });
};
