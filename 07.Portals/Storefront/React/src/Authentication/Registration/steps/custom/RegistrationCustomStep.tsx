import { Button } from "react-bootstrap";

interface IRegistrationCustomStepProps {
  onCompleteCustomStep: Function;
  continueText: string;
}

export const RegistrationCustomStep = (
  props: IRegistrationCustomStepProps
): JSX.Element => {
  const { onCompleteCustomStep, continueText } = props;
  // TODO: implement
  return (
    <>
      <h2>RegistrationWalletStep</h2>
      <Button variant="primary" onClick={() => onCompleteCustomStep(null)}>
        {continueText}
      </Button>
    </>
  );
};
