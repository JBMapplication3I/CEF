import { Form, Col, InputGroup, Button } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFilter, faUndo } from "@fortawesome/free-solid-svg-icons";
import { useTranslation } from "react-i18next";
import { ICVGridToolbarFilterBtns } from "./_CVGridToolbarFilterTypes";
export const CVGridToolbarFilterBtns = (props: ICVGridToolbarFilterBtns) => {
  const { onCallFilterClicked, onRefreshClicked, isFilterColumnsAvailable } =
    props;

  const { t } = useTranslation();

  return (
    isFilterColumnsAvailable && (
      <Form.Group as={Col} sm="auto" className="search-bar">
        <Form.Label>&nbsp;</Form.Label>
        <InputGroup>
          <Button
            variant="primary"
            className="p-1"
            id="btnGridFilter"
            onClick={() => onCallFilterClicked()}
            title="Filter">
            <FontAwesomeIcon icon={faFilter} className="fa-fw" />
            <span className="sr-only">{t("ui.storefront.common.Filter")}</span>
          </Button>
          <Button
            variant="secondary"
            className="p-1 ml-1"
            onClick={() => onRefreshClicked()}>
            <FontAwesomeIcon
              icon={faUndo}
              className="fa-fw"
              aria-hidden="true"
            />
            <span className="sr-only">
              {t("ui.storefront.userDashboard.passwordReset.Reset")}
            </span>
          </Button>
        </InputGroup>
      </Form.Group>
    )
  );
};
