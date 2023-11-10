import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import { CEFConfig, IReduxStore } from "../../_redux/_reduxTypes";
import { currencyFormatter } from "../../_shared/common/Formatters";
import { Table } from "react-bootstrap";
interface ITotalsWidgetProps {
  Totals: any;
  cefConfig?: CEFConfig;
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    cefConfig: state.cefConfig
  };
};

export const TotalsWidget = connect(mapStateToProps)((
  props: ITotalsWidgetProps
): JSX.Element => {
  const { Totals, cefConfig } = props;

  const { t } = useTranslation();

  if (!Totals) {
    return <table></table>;
  }

  return (
    <Table size="sm" hover className="mb-0 table-condensed w-100">
      <colgroup>
        <col className="w-auto" />
        <col className="w-auto" />
      </colgroup>
      <tbody>
        <tr></tr>
        <tr>
          <th className="pl-3" id="MiniCartSubtotalText">
            {t("ui.storefront.common.Subtotal")}
          </th>
          <td className="text-right pr-3" id="cartTotalsWidgetSubtotal">
            {currencyFormatter.format(Totals.SubTotal)}
          </td>
        </tr>
        {cefConfig.featureSet.shipping.enabled && Totals.Shipping > 0 ? (
          <tr>
            <th className="pl-3">{t("ui.storefront.common.Shipping")}</th>
            <td className="text-right pr-3" id="cartTotalsWidgetShipping">
              {currencyFormatter.format(Totals.Shipping)}
            </td>
          </tr>
        ) : null}
        {Totals.Handling > 0 ? (
          <tr>
            <th className="pl-3">{t("ui.storefront.common.Handling")}</th>
            <td className="text-right pr-3" id="cartTotalsWidgetHandling">
              {currencyFormatter.format(Totals.Handling)}
            </td>
          </tr>
        ) : null}
        {Totals.Fees > 0 ? (
          <tr>
            <th className="pl-3">
              {t(
                "ui.storefront.userDashboard2.modals.payMultipleInvoicesModal.ProcessingFee.Colon"
              )}
            </th>
            <td className="text-right pr-3" id="cartTotalsWidgetFees">
              {currencyFormatter.format(Totals.Fees)}
            </td>
          </tr>
        ) : null}
        {cefConfig.featureSet.shipping.enabled && Totals.Tax > 0 ? (
          <tr>
            <th className="pl-3" id="MiniCartTaxesText">
              {t("ui.storefront.cart.Taxes")}
            </th>
            <td className="text-right pr-3" id="cartTotalsWidgetTaxes">
              {currencyFormatter.format(Totals.Tax)}
            </td>
          </tr>
        ) : null}
      </tbody>
      <tfoot>
        <tr>
          <th className="big pl-3" id="MiniCartTotalText">
            {t("ui.storefront.common.Total")}
          </th>
          <th className="big text-right pr-3" id="cartTotalsWidgetTotal">
            {currencyFormatter.format(Totals.Total)}
          </th>
        </tr>
      </tfoot>
    </Table>
  );
});
