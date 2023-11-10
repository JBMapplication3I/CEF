import { useCefConfig } from "../customHooks/useCefConfig";
import {
  THTTPResponse,
  TUseViewStateErrorMessage
} from "../customHooks/_customHooksTypes";
import { Alert } from "react-bootstrap";
interface IErrorViewProps {
  error:
    | TUseViewStateErrorMessage
    | THTTPResponse
    | Array<TUseViewStateErrorMessage>
    | string;
}

export const ErrorView = (props: IErrorViewProps): JSX.Element | null => {
  const { error } = props;
  const cefConfig = useCefConfig();

  if (!error) {
    return null;
  }

  if (cefConfig && cefConfig.debug) {
    console.error(error);
  }

  let errorText: string;
  if (typeof error === "string") {
    errorText = error;
  } else if (Array.isArray(error)) {
    errorText = error.join("\n ");
    // @ts-ignore
  } else if (error.ResponseStatus) {
    // @ts-ignore
    errorText = error.ResponseStatus.Message;
  }
  // TODO: handle error is Promise

  return (
    <Alert variant="danger">
      <span>{errorText}</span>
    </Alert>
  );
};
