import React from "react";
import classes from "./StoreLocatorMarkerTip.module.scss";
import { Card, Button } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTimes } from "@fortawesome/free-solid-svg-icons";

interface IStoreLocatorMarkerTipProps {
  children: React.ReactNode;
  onXButtonClicked: Function;
}

export const StoreLocatorMarkerTip = (props: IStoreLocatorMarkerTipProps) => {
  const { children, onXButtonClicked } = props;

  return (
    <Card className={classes.markerTooltipCard}>
      <Card.Header className="py-0 pe-1 d-flex justify-content-end bg-white border-0">
        <Button className="p-0" onClick={() => onXButtonClicked()}>
          <FontAwesomeIcon icon={faTimes} />
        </Button>
      </Card.Header>
      <Card.Body className="p-2 position-relative">{children}</Card.Body>
    </Card>
  );
};
