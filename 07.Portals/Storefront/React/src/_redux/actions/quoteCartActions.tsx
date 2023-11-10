import { store } from "../store/store";
import { ServiceStrings } from "../../_shared/ServiceStrings";

const { dispatch } = store;

export const setQuoteCart = (quoteCart: Array<any>): void => {
  dispatch({ type: ServiceStrings.redux.actionTypes.quoteCart.setQuoteCart, payload: quoteCart });
};
