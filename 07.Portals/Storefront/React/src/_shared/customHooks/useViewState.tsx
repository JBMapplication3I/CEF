import { useState } from "react";
import {
  THTTPResponse,
  TUseViewStateErrorMessage,
  TDebounce,
  IViewState,
  IUseViewStateResult
} from "./_customHooksTypes";

interface IViewStateSettings {
  debugMode: boolean;
}

export const useViewState = (settings?: IViewStateSettings): IUseViewStateResult => {
  let debugMode: boolean;
  if (settings && settings.debugMode) {
    debugMode = settings.debugMode;
  }

  const [viewState, setViewState] = useState<IViewState>({
    running: false,
    success: false,
    hasError: false,
    errorMessage: null,
    errorMessages: null,
    waitMessage: null
  });

  const debounce500: TDebounce = {
    updateOn: "default blur",
    debounce: { default: 500, blur: 0 },
    allowInvalid: false
  };

  function consoleDebug(...args: any): void {
    if (debugMode) {
      console.debug(...args);
    }
  }

  function setRunning(waitMessage: TUseViewStateErrorMessage = null): void {
    let newValue: string | null = null;
    if (waitMessage) {
      if (Object.prototype.toString.call(waitMessage) === "[object Promise]") {
        // waitMessage is a Promise
        Promise.resolve(waitMessage).then((msg: string) => {
          newValue = msg;
        });
      } else if (typeof waitMessage === "string") {
        newValue = waitMessage;
      }
    }

    setViewState({
      ...viewState,
      running: true,
      success: false,
      hasError: false,
      errorMessage: null,
      errorMessages: null,
      waitMessage: newValue
    });
  }

  function finishRunning(
    hasError = false,
    errorMessage: THTTPResponse | TUseViewStateErrorMessage = null,
    errorMessages: string[] | null = null
  ): void {
    let errorMsg = errorMessage;
    if (errorMessage) {
      if ((errorMessage as Promise<string>).then) {
        var errMessagePromise = errorMessage as Promise<string>;
        errMessagePromise.then((value) => (errorMsg = value));
      } else if ((errorMessage as THTTPResponse).status) {
        const asCallbackArg = errorMessage as THTTPResponse;
        errorMsg =
          asCallbackArg.status +
          ": " +
          asCallbackArg.statusText + // e.g. "Unauthorized"
          (!asCallbackArg.data ||
          !asCallbackArg.data.ResponseStatus ||
          !asCallbackArg.data.ResponseStatus.Message
            ? JSON.parse(JSON.stringify(asCallbackArg))
            : ": " + asCallbackArg.data.ResponseStatus.Message); // "No active user in session."
      }
    } else if (errorMessages && errorMessages.length) {
      errorMsg = errorMessages[0];
    }
    if (errorMsg) {
      console.log(errorMsg);
    }
    if (hasError && debugMode) {
      consoleDebug(errorMsg);
    }
    setViewState({
      ...viewState,
      running: false,
      waitMessage: null,
      hasError,
      success: !hasError,
      errorMessage: errorMsg,
      errorMessages
    });
  }

  return {
    setRunning,
    finishRunning,
    viewState,
    debounce500
  };
};
