import { store } from "../store/store";
import { ServiceStrings } from "../../_shared/ServiceStrings";

const { dispatch } = store;

export const setWallet = (wallet: Array<any>): void => {
  dispatch({ type: ServiceStrings.redux.actionTypes.wallet.setWallet, payload: wallet });
};
