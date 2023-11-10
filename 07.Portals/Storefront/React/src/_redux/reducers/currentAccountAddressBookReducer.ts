import { AccountContactModel } from "../../_api/cvApi._DtoClasses";
import { ServiceStrings } from "../../_shared/ServiceStrings";
const initialState: any = null;

export const currentAccountAddressBookReducer = (
  state = initialState,
  action: { type: string; payload: Array<AccountContactModel> }
): Array<AccountContactModel> | null => {
  switch (action.type) {
    case ServiceStrings.redux.actionTypes.currentAccountAddressBook.setCurrentAccountAddressBook:
      return action.payload;
    default:
      return state;
  }
};
