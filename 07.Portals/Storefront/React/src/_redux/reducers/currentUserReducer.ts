import { UserModel } from "../../_api/cvApi._DtoClasses";
import { ServiceStrings } from "../../_shared/ServiceStrings";
const initialState = {
  userChecked: false
} as UserModel;

export const currentUserReducer = (
  state = initialState,
  action: { type: string; payload: UserModel }
): UserModel => {
  switch (action.type) {
    case ServiceStrings.redux.actionTypes.user.logUserIn:
      return {
        ...action.payload,
        userChecked: true
      };
      case ServiceStrings.redux.actionTypes.user.logUserOut:
      return { userChecked: true } as UserModel;
    default:
      return state;
  }
};
