import { Login } from "../../../_shared/Login";
import { ICheckoutMethodStepProps } from "../../_checkoutTypes";

export const CheckoutMethodStep = (props: ICheckoutMethodStepProps) => {
  const { onCompleteCheckoutMethodStep } = props;

  return (
    <Login
      onLoginSuccess={onCompleteCheckoutMethodStep}
      hideButtonsInFooter={true}
      customColumnSizes={{ md: 8, xl: 6 }}
    />
  );
};
