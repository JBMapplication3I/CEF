import { Col, Form, InputGroup } from "react-bootstrap";
import classes from "../CVGrid.module.scss";
import { DateTime } from "luxon";
import { useTranslation } from "react-i18next";
import { ICVGridToolbarMinMaxDateFilterProps } from "./_CVGridToolbarFilterTypes";

export const CVGridToolbarMinMaxDateFilter = (
  props: ICVGridToolbarMinMaxDateFilterProps
) => {
  const {
    toolbarFilterTitle,
    uniqueFieldID,
    onMinDateChange,
    onMaxDateChange
  } = props;

  const { t } = useTranslation();

  return (
    <Col lg={4} key={toolbarFilterTitle}>
      <Form.Label className="w-100" htmlFor={uniqueFieldID}>
        {toolbarFilterTitle}
      </Form.Label>
      <div
        className="datepicker d-flex align-items-center mb-2 mb-lg-0"
        data-datepicker='{"rangepicker": true}'>
        <InputGroup className="mr-2">
          <Form.Control
            className={classes.dateInput}
            id={uniqueFieldID}
            type="date"
            placeholder="00/00/0000"
            aria-label="date-in"
            data-datepicker-from='{"minDate":0}'
            onChange={(e) => {
              let newMinDate = "";
              if (DateTime.fromFormat(e.target.value, "y-LL-dd").isValid) {
                newMinDate = e.target.value;
              }
              onMinDateChange(newMinDate);
            }}
          />
          <InputGroup.Text>
            {t("ui.storefront.common.To.Lowercase")}
          </InputGroup.Text>
          <Form.Control
            className={classes.dateInput}
            type="date"
            placeholder="00/00/0000"
            aria-label="date-to"
            data-datepicker-to='{"maxDate":10}'
            onChange={(e) => {
              let newMaxDate = "";
              if (DateTime.fromFormat(e.target.value, "y-LL-dd").isValid) {
                newMaxDate = e.target.value;
              }
              onMaxDateChange(newMaxDate);
            }}
          />
        </InputGroup>
      </div>
    </Col>
  );
};
