import { useTranslation } from "react-i18next";
import { ConfirmationModal } from "../../_shared/modals/ConfirmationModal";
import { ServiceStrings } from "../../_shared/ServiceStrings";
import { useEffect, useState } from "react";
import { ContactSwitch } from "../AddressBook/ContactSwitch";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { Tabs } from "../../_shared/common/Tabs";
import { TabContent } from "../../_shared/common/TabContent";
import { INewAddressFormCallbackData, NewAddressForm } from "../../_shared/forms/NewAddressForm";
import { NewWalletForm } from "../../_shared/forms/NewWalletForm";
import { WalletSwitch } from "../Wallet/WalletSwitch";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { WalletBlock } from "../Wallet/WalletBlock";
import { connect } from "react-redux";
import { AddressBlock } from "../AddressBook/AddressBlock";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faDollarSign } from "@fortawesome/free-solid-svg-icons";
import { SalesInvoiceModel, AccountContactModel } from "../../_api/cvApi._DtoClasses";
import { currencyFormatter } from "../../_shared/common/Formatters";
import cvApi from "../../_api/cvApi";
import { Form, InputGroup } from "react-bootstrap";
import { useAddressFactory } from "../../_shared/customHooks/useAddressFactory";
import { IReduxStore } from "../../_redux/_reduxTypes";

const UseExistingWalletTabContent = (props: {
  onChange: Function;
  onCVVChange: Function;
}) => {
  const { onChange, onCVVChange } = props;
  return (
    <WalletSwitch
      onChange={onChange}
      onCVVChange={onCVVChange}
      classes="flex-grow-1"
    />
  );
};

const CreateNewWalletTabContent = (props: {
  onConfirm: Function;
  hasBeenCreated: boolean;
  createdWalletItem: any;
}) => {
  const { onConfirm, hasBeenCreated, createdWalletItem } = props;
  const [cvv, setCvv] = useState<string>("");

  const onNewWalletConfirmed = (walletData: any) => {
    setCvv(walletData.CreditCardCVV);
    onConfirm(walletData);
  };

  if (hasBeenCreated) {
    return (
      <div>
        <span>New card has been created</span>
        <WalletBlock
          walletItem={createdWalletItem}
          walletCVV={cvv}
          setWalletCVV={setCvv}
        />
      </div>
    );
  }
  return <NewWalletForm onConfirm={onNewWalletConfirmed} />;
};

const UseExistingBillingContactTabContent = (props: {
  addressBook: any;
  onChange: (contact: AccountContactModel) => any;
}) => {
  const { addressBook, onChange } = props;
  return (
    <ContactSwitch
      accountContacts={addressBook}
      onChange={onChange}
      classes="flex-grow-1"
    />
  );
};

const CreateNewBillingContactTabContent = (props: {
  onConfirm: (newAddressFormData: INewAddressFormCallbackData) => any;
  hasBeenCreated: boolean;
  createdBillingContact: any;
}) => {
  const { onConfirm, hasBeenCreated, createdBillingContact } = props;

  if (hasBeenCreated) {
    return (
      <div>
        <span>New address has been created</span>
        <AddressBlock
          address={{
            ...createdBillingContact.Slave,
            ...createdBillingContact.Slave.Address
          }}
        />
      </div>
    );
  }

  return <NewAddressForm type="billing" onConfirm={onConfirm} />;
};

interface IPayInvoiceModalProps {
  invoice: SalesInvoiceModel;
  show: boolean;
  onConfirm: Function;
  onCancel: Function;
  loading?: boolean;
  currentUser?: any; //redux
  currentAccountAddressBook?: AccountContactModel[]; //redux
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    currentUser: state.currentUser,
    cefConfig: state.cefConfig,
    currentAccountAddressBook: state.currentAccountAddressBook
  };
};

export const PayInvoiceModal = connect(mapStateToProps)(
  (props: IPayInvoiceModalProps) => {
    const [selectedWalletItem, setSelectedWalletItem] = useState<any>(null);
    const [selectedWalletItemCVV, setSelectedWalletItemCVV] =
      useState<string>("");
    const [selectedBillingContact, setSelectedBillingContact] =
      useState<any>(null);
    const [chosenAmount, setChosenAmount] = useState<string>("");
    const { t } = useTranslation();

    const { setRunning, finishRunning, viewState } = useViewState();
    const addressFactory = useAddressFactory();

    const { invoice, show, onConfirm, onCancel, loading, currentUser, currentAccountAddressBook } =
      props;

    useEffect(() => {
      if (!addressFactory) {
        return;
      }
      addressFactory.getAddressBook();
    }, [addressFactory]);

    function addAddressToAddressBookAndSelect(addressData: INewAddressFormCallbackData) {
      setRunning();
      addressFactory.createNewAddress(addressData)
        .then((newContact) => {
          setSelectedBillingContact(newContact);
          finishRunning();
        })
        .catch((err) => {
          finishRunning(true, err);
        });
    }

    function addCardToWalletAndSelect(cardData: any) {
      setRunning();
      const {
        CreditCardNumber,
        CreditCardCVV,
        CardNickName,
        CardHolderName,
        ExpirationMonth,
        ExpirationYear
      } = cardData;
      const walletData = {
        CreditCardNumber,
        Name: CardNickName,
        CardHolderName,
        ExpirationMonth,
        ExpirationYear
      };
      let newWalletItemID: number;
      cvApi.payments
        .CreateCurrentUserWalletEntry({
          ...walletData,
          IsDefault: false,
          ID: null,
          UserID: currentUser.ID,
          Active: true,
          CreatedDate: new Date()
        })
        .then((res) => {
          if (!res.data.ActionSucceeded) {
            finishRunning(true, " Failed to create wallet item");
            return;
          }
          newWalletItemID = res.data.Result.ID;
          return cvApi.payments.GetCurrentUserWallet();
        })
        .then((res) => {
          const desiredWalletItem = res.data.Result.find(
            (w: any) => w.ID === newWalletItemID
          );
          if (!desiredWalletItem) {
            finishRunning(true, " Failed to find added wallet item");
            return;
          }
          setSelectedWalletItem(desiredWalletItem);
          setSelectedWalletItemCVV(CreditCardCVV);
          finishRunning();
        })
        .catch((err) => {
          finishRunning(true, err);
        });
    }

    function payInvoice(): void {
      const {
        ID,
        CardHolderName,
        CardType,
        CreditCardNumber,
        ExpirationMonth,
        ExpirationYear
      } = selectedWalletItem;
      setRunning();
      cvApi.providers
        .PaySingleInvoiceByID({
          Billing: selectedBillingContact.Slave,
          InvoiceID: invoice.ID,
          Payment: {
            ID: 0,
            Active: true,
            CreatedDate: new Date(),
            CustomKey: null,
            CardNumber: CreditCardNumber,
            CardHolderName,
            CVV: selectedWalletItemCVV,
            PurchaseOrder: null,
            ExpirationMonth,
            ExpirationYear,
            // @ts-ignore
            Amount: chosenAmount + invoice.Totals.Fees,
            CardType,
            TypeID: invoice.TypeID,
            StatusID: invoice.StatusID,
            Zip: null,
            WalletID: ID
          }
        })
        .then((res) => {
          finishRunning();
          onConfirm();
        })
        .catch((err) => {
          console.log(err);
          finishRunning(true, err);
        });
    }

    return (
      <ConfirmationModal
        title={t("ui.storefront.userDashboard.controls.salesDetail.PayInvoice")}
        show={show}
        onConfirm={payInvoice}
        confirmDisabled={
          !selectedWalletItem ||
          selectedWalletItemCVV.length < 3 ||
          selectedWalletItemCVV.length > 4 ||
          !chosenAmount.length ||
          !selectedBillingContact ||
          viewState.running
        }
        onCancel={onCancel}
        size={ServiceStrings.modalSizes.lg}>
        <div className="text-body position-relative">
          {viewState.running ? <LoadingWidget overlay={true} /> : null}
          <span>
            {t("ui.storefront.checkout.views.paymentInformation.selectACard")}
          </span>
          <Tabs
            onSwitch={() => setSelectedWalletItem(null)}
            tabClasses="p-1 px-2">
            <TabContent label="Use Existing">
              <UseExistingWalletTabContent
                onChange={(walletItem: any) => {
                  setSelectedWalletItem(walletItem);
                }}
                onCVVChange={(cvv: string) => {
                  setSelectedWalletItemCVV(cvv);
                }}
              />
            </TabContent>
            <TabContent label="Create New">
              <CreateNewWalletTabContent
                onConfirm={addCardToWalletAndSelect}
                hasBeenCreated={selectedWalletItem != null}
                createdWalletItem={selectedWalletItem}
              />
            </TabContent>
          </Tabs>
          <span>
            {t(
              "ui.storefront.userDashboard2.controls.addressBook.PleaseSelectABillingAddress"
            )}
          </span>
          <Tabs
            onSwitch={() => setSelectedBillingContact(null)}
            tabClasses="p-1 px-2">
            <TabContent label="Use Existing">
              <UseExistingBillingContactTabContent
                addressBook={currentAccountAddressBook}
                onChange={(contact: any) => {
                  setSelectedBillingContact(contact);
                }}
              />
            </TabContent>
            <TabContent label="Create New">
              <CreateNewBillingContactTabContent
                onConfirm={addAddressToAddressBookAndSelect}
                hasBeenCreated={selectedBillingContact != null}
                createdBillingContact={selectedBillingContact}
              />
            </TabContent>
          </Tabs>
          <div className="bg-light p-3">
            <div className="d-flex">
              <div className="w-50">
                <p className="mb-1">
                  {t("ui.storefront.userDashboard.invoices.BalanceDue")}
                </p>
                <p className="h3 mt-0 mb-1">{invoice.BalanceDue}</p>
              </div>
              <div className="w-50">
                <p className="mb-1">
                  {t("ui.storefront.userDashboard.invoices.ChosenAmount")}
                </p>
                <InputGroup>
                  <InputGroup.Text>
                    <FontAwesomeIcon icon={faDollarSign} />
                  </InputGroup.Text>
                  <Form.Control
                    type="number"
                    className="text-right"
                    value={chosenAmount}
                    onChange={(e) => {
                      setChosenAmount(e.target.value);
                    }}
                  />
                </InputGroup>
              </div>
            </div>
            <div className="d-flex">
              <div className="w-50">
                <p className="mb-1">Total Fees</p>
                <p className="h3 mt-0 mb-1">
                  {/* @ts-ignore */}
                  {currencyFormatter.format(invoice.Totals.Fees)}
                </p>
              </div>
              <div className="w-50">
                <p className="mb-1">Total Payment Amount</p>
                <p className="h3 mt-0 mb-1">
                  {/* @ts-ignore */}
                  {currencyFormatter.format(invoice.Totals.Fees + chosenAmount)}
                </p>
              </div>
            </div>
            <div>
              <p className="mb-0">
                <small>
                  <b>NOTE:</b>A $1.49 processing fee is charged for credit card
                  transactions.
                </small>
              </p>
              <p className="mb-0">
                <small>
                  <b>NOTE:</b>A $1.49 processing fee is charged for credit card
                  transactions.
                </small>
              </p>
            </div>
          </div>
        </div>
      </ConfirmationModal>
    );
  }
);
