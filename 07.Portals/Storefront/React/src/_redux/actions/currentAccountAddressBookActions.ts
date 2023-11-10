import { AccountContactModel } from "../../_api/cvApi._DtoClasses";
import { ServiceStrings } from "../../_shared/ServiceStrings";
import { store } from "../store/store";

const { dispatch } = store;

export const setCurrentAccountAddressBook = (addressBook: Array<AccountContactModel>): void => {
  dispatch({
    type: ServiceStrings.redux.actionTypes.currentAccountAddressBook.setCurrentAccountAddressBook,
    payload: addressBook
  });
};
