import { CartModel } from "../../_api/cvApi._DtoClasses";
import { ServiceStrings } from "../../_shared/ServiceStrings";
// TODO: initialize as object { cartChecked: false }
const initialState: CartModel | null = null;

export interface ICartAction {
  type: string;
  payload: any;
}

export const cartReducer = (
  state = initialState,
  action: ICartAction
): CartModel | null => {
  switch (action.type) {
    case ServiceStrings.redux.actionTypes.cart.setCart:
      return {
        ...action.payload
      };
    default:
      return state;
  }
};
