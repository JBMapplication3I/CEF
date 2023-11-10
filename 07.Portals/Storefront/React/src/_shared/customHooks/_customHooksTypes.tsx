/* useViewState */
export type THTTPResponse = {
  status: number;
  statusText: string;
  data: any;
  ResponseStatus: { Message: string };
};

export type TUseViewStateErrorMessage = string | null | Promise<string>;

export type TDebounce = {
  updateOn: string;
  debounce: { default: number; blur: number };
  allowInvalid: boolean;
};
export interface IViewState {
  running: boolean;
  success: boolean;
  hasError: boolean;
  errorMessage: TUseViewStateErrorMessage | THTTPResponse;
  errorMessages: Array<TUseViewStateErrorMessage> | null;
  waitMessage: string | null;
}

export interface IUseViewStateResult {
  setRunning: Function;
  finishRunning: Function;
  viewState: IViewState;
  debounce500: TDebounce;
}
