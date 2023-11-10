import { useEffect, useState } from "react";
import { faDollarSign, faExclamationTriangle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { ErrorView } from "../../_shared/common/ErrorView";
import { NewAddressModal } from "./NewAddressModal";
import cvApi from "../../_api/cvApi";
import { useTranslation } from "react-i18next";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { connect } from "react-redux";
import { AccountContactModel, UserModel } from "../../_api/cvApi._DtoClasses";
import { INewAddressFormCallbackData } from "../../_shared/forms/NewAddressForm";
import { Button, Card, Col, Row, Alert } from "react-bootstrap";
import { IReduxStore } from "../../_redux/_reduxTypes";
import { AddressBookCard } from "./AddressBookCard";
import { useAddressFactory } from "../../_shared/customHooks/useAddressFactory";

interface IAddressBookProps {
  currentUser?: UserModel; //redux
  currentAccountAddressBook?: AccountContactModel[]; // redux
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    currentUser: state.currentUser,
    currentAccountAddressBook: state.currentAccountAddressBook
  };
};

export const AddressBook = connect(mapStateToProps)((props: IAddressBookProps): JSX.Element => {
  const { currentUser, currentAccountAddressBook } = props;

  const [hasDefaultBilling, setHasDefaultBilling] = useState<boolean>(false);
  const [hasDefaultShipping, setHasDefaultShipping] = useState<boolean>(false);
  const [showAddAddressModal, setShowAddAddressModal] = useState(false);
  const [accountContactToEdit, setAccountContactToEdit] = useState<AccountContactModel>(null);
  const [showDeleteAddressConfirmationModalID, setShowDeleteAddressConfirmationModalID] =
    useState(null);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();
  const addressFactory = useAddressFactory();

  useEffect(() => {
    if (!addressFactory) {
      return;
    }
    getAddressBook(); // will refresh current account address book
  }, [addressFactory]);

  useEffect(() => {
    if (!currentAccountAddressBook?.length) {
      return;
    }
    let defaultBilling = currentAccountAddressBook?.find(
      (contact: AccountContactModel) => contact.IsBilling
    );
    let defaultShipping = currentAccountAddressBook?.find(
      (contact: AccountContactModel) => contact.IsPrimary
    );
    setHasDefaultBilling(!!defaultBilling);
    setHasDefaultShipping(!!defaultShipping);
  }, [currentAccountAddressBook]);

  function getAddressBook(): void {
    setRunning();
    addressFactory
      .getAddressBook()
      .then(() => {
        finishRunning();
      })
      .catch((err) => {
        finishRunning(true, err);
      });
  }

  function markAccountContactAsPrimaryShipping(contactID: number): void {
    setRunning();
    cvApi.accounts
      .MarkAccountContactAsPrimaryShipping(contactID)
      .then((result) => {
        if (!result?.data?.ActionSucceeded) {
          console.log("Mark account contact as primary shipping failed");
          return;
        }
        getAddressBook();
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  function markAccountContactAsDefaultBilling(contactID: number): void {
    setRunning();
    cvApi.accounts
      .MarkAccountContactAsDefaultBilling(contactID)
      .then((result) => {
        if (!result.data?.ActionSucceeded) {
          console.log("Mark account contact as default billing failed");
          return;
        }
        getAddressBook();
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  function editExistingAddress(userAddressData: INewAddressFormCallbackData): void {
    const {
      CustomKey,
      FirstName,
      LastName,
      Email1,
      Company,
      Country,
      Phone1,
      Street1,
      Street2,
      Fax1,
      City,
      Region,
      PostalCode,
      IsBilling
    } = userAddressData;
    setRunning();
    addressFactory
      .validateAddress(userAddressData)
      .then((res) => {
        if (!res) {
          alert("Address invalid");
          throw new Error("Address invalid");
        }
        const UpdatedDate = new Date();
        return cvApi.accounts.UpdateAccountContact({
          ...accountContactToEdit,
          Active: true,
          IsBilling: IsBilling,
          MasterID: currentUser.AccountID,
          Slave: {
            ...accountContactToEdit.Slave,
            Active: true,
            CustomKey,
            FirstName,
            LastName,
            Email1,
            Phone1,
            Fax1,
            Address: {
              ...accountContactToEdit.Slave.Address,
              Active: true,
              Company,
              Street1,
              City,
              PostalCode,
              Region,
              Country,
              RegionCode: Region.Code,
              RegionID: Region.ID,
              RegionKey: Region.CustomKey,
              RegionName: Region.Name,
            }
            // TransmittedToERP: false
          },
          TransmittedToERP: false,
          UpdatedDate
        });
      })
      .then((_res: any) => {
        getAddressBook();
        setShowAddAddressModal(false);
        setAccountContactToEdit(null);
        finishRunning();
      })
      .catch((err: any) => {
        setShowAddAddressModal(false);
        setAccountContactToEdit(null);
        finishRunning(true, err);
      });
  }

  function deleteAddress(ID: number): void {
    setRunning();
    cvApi.accounts
      .DeleteAccountContactByID(ID)
      .then((res) => {
        if (res.data.ActionSucceeded) {
          getAddressBook();
        } else {
          alert("Failed to delete address.");
          finishRunning(true, "Failed to delete address.");
        }
        setShowDeleteAddressConfirmationModalID(null);
      })
      .catch((err: any) => {
        setShowDeleteAddressConfirmationModalID(null);
        finishRunning(true, err);
      });
  }

  const handleEditAddressClicked = (accountContact: AccountContactModel) => {
    setShowAddAddressModal(true);
    setAccountContactToEdit({ ...accountContact });
  };

  const handleDefaultAddressChanged = (accountContactID: number, isBilling: boolean) => {
    if (isBilling) {
      markAccountContactAsDefaultBilling(accountContactID);
      return;
    }
    markAccountContactAsPrimaryShipping(accountContactID);
  };

  return (
    <Row>
      <Col>
        <section className="section-address">
          <div>
            <div className="section-title">
              <h1 className="title">{t("ui.storefront.common.AddressBook")}</h1>
            </div>
            <Row>
              <Col xs={12}>
                <section className="section-addresses">
                  <Row>
                    <Col xl="4" className="mb-3">
                      <Card className="h-100">
                        <Card.Header id="AddAddressText">
                          {t("ui.storefront.checkout.splitShipping.addressModal.AddAddress")}
                        </Card.Header>
                        <Card.Body className="p-2">
                          <Button
                            variant="outline-secondary"
                            className="p-3 h-100"
                            id="btnAddAddress"
                            name="btnAddAddress"
                            onClick={() => setShowAddAddressModal(true)}>
                            <svg
                              width="30px"
                              height="31px"
                              viewBox="0 0 30 31"
                              version="1.1"
                              xmlns="http://www.w3.org/2000/svg">
                              <g stroke="none" strokeWidth="1" fill="none" fillRule="evenodd">
                                <g transform="translate(-847.000000, -260.000000)" fill="#B6B6B6">
                                  <path d="M862,260.370117 C853.720465,260.370117 847,267.090582 847,275.370117 C847,283.649652 853.720465,290.370117 862,290.370117 C870.279535,290.370117 877,283.649652 877,275.370117 C877,267.090582 870.279535,260.370117 862,260.370117 Z M862,261.067792 C869.903256,261.067792 876.302326,267.466861 876.302326,275.370117 C876.302326,283.273373 869.903256,289.672443 862,289.672443 C854.096744,289.672443 847.697674,283.273373 847.697674,275.370117 C847.697674,267.466861 854.096744,261.067792 862,261.067792 Z M861.956279,267.336164 L861.956279,267.336164 C861.776744,267.357559 861.643721,267.513838 861.651163,267.695699 L861.651163,275.02128 L854.325581,275.02128 C854.303721,275.01942 854.28186,275.01942 854.26,275.02128 C853.794884,275.065001 853.860465,275.762675 854.325581,275.718954 L861.651163,275.718954 L861.651163,283.044536 C861.647442,283.238954 861.805581,283.398954 862,283.398954 C862.194419,283.398954 862.352558,283.238954 862.348837,283.044536 L862.348837,275.718954 L869.674419,275.718954 C869.868837,275.722675 870.028837,275.564536 870.028837,275.370117 C870.028837,275.175699 869.868837,275.017559 869.674419,275.02128 L862.348837,275.02128 L862.348837,267.695699 C862.35814,267.47942 862.170698,267.308722 861.956279,267.336164 Z"></path>
                                </g>
                              </g>
                            </svg>
                            <br />
                            <span>
                              {t("ui.storefront.userDashboard2.controls.addressBook2.AddAddress")}
                            </span>
                            <br />
                            <br />
                            <p className="mb-0">
                              <em>
                                <b>{t("ui.storefront.common.Note")}</b>
                                <br />
                                <span className="wrap">
                                  {t(
                                    "ui.storefront.userDashboard2.controls.addressBook2.OnlyOneAddressShouldBeSetAs.Message"
                                  )}
                                </span>
                              </em>
                            </p>
                          </Button>
                          <NewAddressModal
                            onConfirm={(addressData) => {
                              if (accountContactToEdit) {
                                editExistingAddress(addressData);
                                return;
                              }
                              addressFactory
                                .createNewAddress(addressData)
                                .then((res) => {
                                  setShowAddAddressModal(false);
                                  finishRunning();
                                })
                                .catch((err) => {
                                  setShowAddAddressModal(false);
                                  finishRunning(true, err);
                                });
                            }}
                            onCancel={() => {
                              setShowAddAddressModal(false);
                              setAccountContactToEdit(null);
                            }}
                            show={showAddAddressModal}
                            contact={accountContactToEdit?.Slave}
                            parentRunning={viewState.running}
                          />
                        </Card.Body>
                      </Card>
                    </Col>
                    <AddressBookCard
                      accountContacts={currentAccountAddressBook}
                      type="Billing"
                      showDropdown={true}
                      handleDefaultAddressChanged={handleDefaultAddressChanged}
                      markAccountContact={markAccountContactAsDefaultBilling}
                      showDeleteAddressConfirmationModalID={showDeleteAddressConfirmationModalID}
                      setShowDeleteAddressConfirmationModalID={
                        setShowDeleteAddressConfirmationModalID
                      }
                      handleEditAddressClicked={handleEditAddressClicked}
                      deleteAddress={deleteAddress}
                    />
                    <AddressBookCard
                      accountContacts={currentAccountAddressBook}
                      type="Shipping"
                      showDropdown={true}
                      handleDefaultAddressChanged={handleDefaultAddressChanged}
                      markAccountContact={markAccountContactAsDefaultBilling}
                      showDeleteAddressConfirmationModalID={showDeleteAddressConfirmationModalID}
                      setShowDeleteAddressConfirmationModalID={
                        setShowDeleteAddressConfirmationModalID
                      }
                      handleEditAddressClicked={handleEditAddressClicked}
                      deleteAddress={deleteAddress}
                    />
                    {!hasDefaultBilling || !hasDefaultShipping ? (
                      <Col xs={12}>
                        <Alert variant="danger">
                          <FontAwesomeIcon icon={faExclamationTriangle} />
                          &nbsp;
                          <b>
                            {!hasDefaultBilling
                              ? t(
                                  "ui.storefront.userDashboard2.controls.addressBook2.YouDontHaveABillingAddress"
                                )
                              : t(
                                  "ui.storefront.userDashboard2.controls.addressBook2.YouDontHaveADefaultShippingAddress"
                                )}
                          </b>
                          &nbsp;
                          <span>
                            {!hasDefaultBilling
                              ? t(
                                  "ui.storefront.userDashboard2.controls.addressBook2.PleaseClickSetAsBilling"
                                )
                              : t(
                                  "ui.storefront.userDashboard2.controls.addressBook2.PleaseClickSetAsDefault"
                                )}
                          </span>
                          &nbsp;
                          <FontAwesomeIcon icon={faDollarSign} />
                          &nbsp;
                          <span>
                            {t(
                              "ui.storefront.userDashboard2.controls.addressBook2.OnAnAddressBelowOrAddAddressFirstAndThenSelectOneNote"
                            )}
                          </span>
                        </Alert>
                      </Col>
                    ) : null}
                    {currentAccountAddressBook &&
                      currentAccountAddressBook.map((accountContact: AccountContactModel) => {
                        return (
                          <AddressBookCard
                            key={accountContact.ID}
                            accountContactID={accountContact.ID}
                            accountContacts={currentAccountAddressBook}
                            type="Other"
                            showDeleteAddressConfirmationModalID={
                              showDeleteAddressConfirmationModalID
                            }
                            setShowDeleteAddressConfirmationModalID={
                              setShowDeleteAddressConfirmationModalID
                            }
                            handleEditAddressClicked={handleEditAddressClicked}
                            deleteAddress={deleteAddress}
                          />
                        );
                      })}
                  </Row>
                </section>
              </Col>
            </Row>
            <ErrorView error={viewState.errorMessage} />
          </div>
        </section>
      </Col>
    </Row>
  );
});
