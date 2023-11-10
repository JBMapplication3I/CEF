import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCog } from "@fortawesome/free-solid-svg-icons";
import { SizeProp } from "@fortawesome/fontawesome-svg-core";
import { Translate } from "../Translate";
import { Row, Col } from "react-bootstrap";

interface ILoadingWidgetProps {
  overlay?: boolean;
  padIn?: boolean;
  size?: SizeProp;
  message?: string;
  messageKey?: string;
  classes?: string;
  innerClasses?: string;
}

export const LoadingWidget = (props: ILoadingWidgetProps): JSX.Element => {
  const { overlay, padIn, size, message, messageKey, classes, innerClasses } = props;

  let additionalClasses = "";
  if (overlay) {
    additionalClasses += `bg-light overlay position-absolute align-items-center o-70 ay-0 ax-${
      padIn ? "0" : "3"
    }`;
  }
  if (classes && typeof classes === "string") {
    additionalClasses += " " + classes;
  }

  return (
    <Row className={additionalClasses} style={{ zIndex: overlay ? 10000 : "inherit" }}>
      <Col
        xs={12}
        className={`text-center o-100 ${!overlay && !innerClasses ? "p-5" : ""} ${
          innerClasses ? innerClasses : ""
        }`}>
        <FontAwesomeIcon icon={faCog} size={size ?? "5x"} className={`fa-spin fa-fw`} />
        {messageKey && (
          <p>
            <Translate translateKey={messageKey} />
          </p>
        )}
        {message && <p>{message}</p>}
        {message && messageKey && (
          <Translate translateKey={"ui.storefront.common.Loading.Ellipses"} />
        )}
      </Col>
    </Row>
  );
};
