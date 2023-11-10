import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCheckCircle,
  faExclamationTriangle
} from "@fortawesome/free-solid-svg-icons";
import { Alert } from "react-bootstrap";

interface IMessageProps {
  error?: boolean;
  message: string;
}

export const Message = (props: IMessageProps) => {
  const { error, message } = props;

  let alertClass = "info";
  let icon = null;

  if (typeof error === "boolean") {
    alertClass = error ? "danger" : "success";
    icon = error ? faExclamationTriangle : faCheckCircle;
  }

  return (
    <Alert variant={alertClass} className="d-flex align-items-center">
      {icon ? (
        <FontAwesomeIcon icon={icon} className="fa-2x fa-fw mr-2" />
      ) : null}
      <b className="text-break">{message}</b>
    </Alert>
  );
};
