import { WalletModel } from "../../_api/cvApi._DtoClasses";
import { ServiceStrings } from "../../_shared/ServiceStrings";
const initialState: any = null;

export const walletReducer = (
  state = initialState,
  action: { type: string; payload: Array<WalletModel> }
): Array<WalletModel> | null => {
  switch (action.type) {
    case ServiceStrings.redux.actionTypes.wallet.setWallet:
      return [...action.payload];
    default:
      return state;
  }
};
