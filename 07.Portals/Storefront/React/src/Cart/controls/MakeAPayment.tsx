import { useState } from "react";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { Button, ButtonGroup } from "react-bootstrap";
interface IMakeAPaymentProps {
  classes?: string;
  icon?: boolean;
  excludeQuoteCart?: boolean;
  label?: string;
  disabled?: boolean;
  useConfirmModal?: boolean;
  product?: any;
  quantity?: number;
  balanceIsPassedDue(): boolean;
}

export const MakeAPayment = (props: IMakeAPaymentProps): JSX.Element => {
  const { classes, disabled, useConfirmModal, balanceIsPassedDue } = props;

  const [showConfirmationModal, setShowConfirmationModal] =
    useState<boolean>(false);

  const { setRunning, finishRunning, viewState } = useViewState();

  function onMakeAPayment() {
    /* TODO: handle making an invoice payment modal */
  }

  return (
    <div className="position-relative">
      <ButtonGroup className="w-100">
        <Button
          variant={balanceIsPassedDue() ? "danger" : "warning"}
          className={classes ?? `btn-block btn-md rounded-start text-nowrap`}
          disabled={disabled || false}
          onClick={() => onMakeAPayment()}>
          Make a Payment
          {/* data-translate="ui.storefront.cart.makeAPayment" */}
        </Button>
      </ButtonGroup>
    </div>
  );
};
