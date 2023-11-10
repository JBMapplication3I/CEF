import { ServiceStrings } from "../../_shared/ServiceStrings";
import { HubConnection } from "@microsoft/signalr";

const initialState: HubConnection | null = null;

export interface ISignalRAction {
  type: string;
  payload: HubConnection | null;
}

export const signalRReducer = (
  state = initialState,
  action: ISignalRAction
): HubConnection | null => {
  switch (action.type) {
    case ServiceStrings.redux.actionTypes.signalR.setSignalRConnection:
      return action.payload;
    default:
      return state;
  }
};
