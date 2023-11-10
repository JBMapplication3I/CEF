import { ServiceStrings } from "../../_shared/ServiceStrings";
import { store } from "../store/store";

const { dispatch } = store;

export const setCart = (cart: Array<any>): void => {
  dispatch({ type: ServiceStrings.redux.actionTypes.cart.setCart, payload: cart });
};
