import { ServiceStrings } from "../../_shared/ServiceStrings";
import { CEFConfig } from "../_reduxTypes";
const initialState: any = null;

export const cefConfigReducer = (
  state = initialState,
  action: { type: string; payload?: CEFConfig }
): CEFConfig => {
  switch (action.type) {
    case ServiceStrings.redux.actionTypes.cefConfig.setCefConfig:
      return action.payload;
    default:
      return state;
  }
};
