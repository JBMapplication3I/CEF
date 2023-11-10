import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMinus, faPlus } from "@fortawesome/free-solid-svg-icons";
import { Card, Button } from "react-bootstrap";
import classes from "./CatalogFilterHeaderButton.module.scss";
import { ICatalogFilterHeaderButtonProps } from "./_CatalogFilterComponentsTypes";
import { useAccordionButton } from "react-bootstrap";

export const CatalogFilterHeaderButton = (
  props: ICatalogFilterHeaderButtonProps
): JSX.Element => {
  const { icon, expandedFilterName, setExpandedFilterName, title } = props;
  const onHeaderClicked = useAccordionButton(title);

  return (
    <Card.Title className="catalog-filter-header mb-0">
      <Button
        variant="light"
        size="lg"
        className={`d-flex align-items-center justify-content-between w-100 p-0 text-decoration-none ${classes.catalogFilterHeaderButton}`}
        onClick={(e) => {
          onHeaderClicked(e);
          setExpandedFilterName(expandedFilterName === title ? null : title)
        }}
        type="button">
        <span>
          <FontAwesomeIcon icon={icon} className="fa-fw mr-2 small" />
          <span className="small">{title}</span>
        </span>
        <FontAwesomeIcon
          icon={expandedFilterName === title ? faMinus : faPlus}
          className="fa-fw float-right text-body toggle-icon"
        />
      </Button>
    </Card.Title>
  );
};
