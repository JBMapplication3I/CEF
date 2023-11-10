import { UserModel } from "../../_api/cvApi._DtoClasses";
import { store } from "../store/store";
import { ServiceStrings } from "../../_shared/ServiceStrings";

const { dispatch } = store;

export const logUserIn = (userData: UserModel): void => {
  dispatch({ type: ServiceStrings.redux.actionTypes.user.logUserIn, payload: userData });
};

export const logUserOut = (): void => {
  dispatch({ type: ServiceStrings.redux.actionTypes.user.logUserOut });
};
