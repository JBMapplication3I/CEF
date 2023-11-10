import { ServiceStrings } from "../../_shared/ServiceStrings";
// TODO: initialize as object { cartChecked: false }
const initialState: any = null;

export const quoteCartReducer = (
  state = initialState,
  action: { type: string; payload: any }
): any => {
  switch (action.type) {
    case ServiceStrings.redux.actionTypes.quoteCart.setQuoteCart:
      return {
        ...action.payload
      };
    default:
      return state;
  }
};
