import { AccountModel } from "../../_api/cvApi._DtoClasses";
import { ServiceStrings } from "../../_shared/ServiceStrings";
const initialState: any = null;

export const currentAccountReducer = (
  state = initialState,
  action: { type: string; payload: AccountModel }
): AccountModel | null => {
  switch (action.type) {
    case ServiceStrings.redux.actionTypes.currentAccount.setCurrentAccount:
      return action.payload;
    default:
      return state;
  }
};
