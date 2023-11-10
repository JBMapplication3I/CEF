import { currencyFormatter } from "../../_shared/common/Formatters";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { ListGroup } from "react-bootstrap";

export const SalesTab = (props: any): JSX.Element => {
  const { ID, classes, Totals, index, selectedSalesTab, onSelectedSalesTabChange, params } = props;

  const { t } = useTranslation();

  return (
    <ListGroup.Item
      key={ID}
      className={`p-2 border-right border-right-6 border-clear ${classes ? classes : ""} ${
        selectedSalesTab === ID
          ? "active border-primary border-left-0 border-top-0 border-bottom-0 rounded rounded-right bg-light"
          : ""
      }`}>
      <Link
        to={`/dashboard/sales-group/${params.salesGroupId}/${params.type}/${ID}`}
        onClick={() => onSelectedSalesTabChange(ID)}>
        <span id={`btnIndividualSalesGroupOrderMasterNumber_${index}`}>
          {t("ui.storefront.common.Number.Symbol")} {ID}
        </span>
        <span className="text-right pl-3" id={`IndividualSalesGroupOrderMasterPrice_${index}`}>
          {currencyFormatter.format(Totals.Total)}
        </span>
      </Link>
    </ListGroup.Item>
  );
};
