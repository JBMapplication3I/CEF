import { Button } from "react-bootstrap";

interface IRegistrationWalletStepProps {
  onCompleteWalletStep: Function;
  continueText: string;
}

export const RegistrationWalletStep = (
  props: IRegistrationWalletStepProps
): JSX.Element => {
  const { onCompleteWalletStep, continueText } = props;

  // TODO: implement
  return (
    <>
      <h2>RegistrationWalletStep</h2>
      <Button variant="primary" onClick={() => onCompleteWalletStep(null)}>
        {continueText}
      </Button>
    </>
  );
};
