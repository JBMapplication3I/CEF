import { useEffect, useState } from "react";
import { connect } from "react-redux";
import cvApi from "../../_api/cvApi";
import { setWallet } from "../../_redux/actions";
import { IReduxStore } from "../../_redux/_reduxTypes";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { WalletBlock } from "./WalletBlock";
import { Form } from "react-bootstrap";
interface IWalletSwitchProps {
  wallet: Array<any>; //redux
  onChange: Function;
  onCVVChange?: Function;
  title?: string;
  classes?: string;
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    wallet: state.wallet
  };
};

export const WalletSwitch = connect(mapStateToProps)((props: IWalletSwitchProps): JSX.Element => {
  const { wallet, onChange, onCVVChange, title, classes } = props;

  const [selectedWalletItem, setSelectedWalletItem] = useState(null);
  const [selectedWalletCVV, setSelectedWalletCVV] = useState<string>("");

  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    if (!wallet || !wallet.length) {
      getUserWallet();
    }
  }, []);

  function getUserWallet() {
    setRunning();
    cvApi.payments
      .GetCurrentUserWallet()
      .then((res: any) => {
        setWallet(res.data.Result);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  return (
    <div className={`wrap mb-2 ${classes ?? ""}`}>
      <strong className="d-block">{title ?? ""}</strong>
      <Form.Select
        id="headquarters"
        aria-label="headquarters"
        className={selectedWalletItem ? "mb-2" : ""}
        value={selectedWalletItem ? selectedWalletItem.ID : ""}
        required
        onChange={(e) => {
          const currentContactVal = wallet.find(
            (c: { ID: number }) => c.ID.toString() === e.target.value
          );
          if (currentContactVal) {
            onChange(currentContactVal);
          }
          setSelectedWalletItem(currentContactVal);
        }}>
        <option
          disabled={!!selectedWalletItem}
          className={`${selectedWalletItem ? "disabled" : ""}`}>
          Please select a card
        </option>
        {wallet &&
          wallet.map((walletItem: any) => {
            const { ID, CustomKey, Name } = walletItem;
            return (
              <option key={ID} value={ID}>
                {Name || CustomKey}
              </option>
            );
          })}
      </Form.Select>
      {selectedWalletItem ? (
        <WalletBlock
          walletItem={selectedWalletItem}
          walletCVV={selectedWalletCVV}
          setWalletCVV={(cvv: string) => {
            setSelectedWalletCVV(cvv);
            if (onCVVChange) {
              onCVVChange(cvv);
            }
          }}
        />
      ) : null}
    </div>
  );
});
