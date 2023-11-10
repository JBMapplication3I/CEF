import { Row, Col, Card, Button } from "react-bootstrap";
import {
  CartModel,
  RateQuoteModel,
  SalesItemTargetBaseModel,
  AccountContactModel
} from "../../../_api/cvApi._DtoClasses";
import { useTranslation } from "react-i18next";
import { ServiceStrings } from "../../../_shared/ServiceStrings";
import { IViewState } from "../../../_shared/customHooks/_customHooksTypes";
import { LoadingWidget } from "../../../_shared/common/LoadingWidget";
import { TGroup, TItem } from "./CheckoutSplitShippingStep";
import { Fragment, useState } from "react";
import { ErrorView } from "../../../_shared/common/ErrorView";
import { PurchaseSplitShippingRateQuotesManagerWidget } from "../../PurchaseSplitShippingRateQuotesManagerWidget";
import { SplitShippingTarget } from "./SplitShippingTarget";
import { INewAddressFormCallbackData } from "../../../_shared/forms/NewAddressForm";
import { useAddressFactory } from "../../../_shared/customHooks/useAddressFactory";
import { CEFConfig } from "../../../_redux/_reduxTypes";
import cvApi from "../../../_api/cvApi";
import { AnalyzeCurrentCartToTargetCartsDto } from "../../../_api";

interface ICheckoutTargetsProps {
  cart: CartModel;
  grouped: TGroup;
  setGrouped: Function;
  targetedCarts: CartModel[];
  setTargetedCarts: Function;
  setReadyToLoadShippingRateQuotes: Function;
  targetFactory: Function;
  submitDisabled: boolean;
  shippingRatesNeeded: boolean;
  cefConfig: CEFConfig;
  accountContacts: AccountContactModel[];
  parentViewState: IViewState;
  parentSetRunning: Function;
  parentFinishRunning: Function;
  setEstimatedShippingCost: Function;
  itemsExist: Function;
}

export const CheckoutTargets = (props: ICheckoutTargetsProps): JSX.Element => {
  const {
    cart,
    grouped,
    setGrouped,
    targetedCarts,
    setTargetedCarts,
    setReadyToLoadShippingRateQuotes,
    targetFactory,
    submitDisabled,
    shippingRatesNeeded,
    cefConfig,
    accountContacts,
    parentViewState,
    parentSetRunning,
    parentFinishRunning,
    setEstimatedShippingCost,
    itemsExist
  } = props;

  const [shippingRateQuotes, setShippingRateQuotes] = useState<RateQuoteModel[]>(null);
  const [gettingRateQuotes, setGettingRateQuotes] = useState(false);

  const addressFactory = useAddressFactory();
  const { t } = useTranslation();

  function getAvailableAccountContactsForTarget(
    thisTarget: SalesItemTargetBaseModel,
    targets: SalesItemTargetBaseModel[]
  ): AccountContactModel[] {
    if (!itemsExist(accountContacts) || !itemsExist(targets)) {
      return accountContacts;
    }
    const selectedContactIDs = targets
      .filter((x) => x.DestinationContact && x.DestinationContact.ID)
      .map((x) => x.DestinationContact.ID);
    return [...accountContacts]
      .filter((ac) => {
        return (
          !selectedContactIDs.includes(ac.Slave.ID) ||
          thisTarget.DestinationContact?.ID === ac.Slave.ID
        );
      })
      .sort((ac1, ac2) => {
        return ac1.Slave.CustomKey > ac2.Slave.CustomKey ? 1 : -1;
      });
  }

  function blankDestinationContactInfo(
    groupKey: string,
    salesItem: TItem,
    target: SalesItemTargetBaseModel
  ): void {
    const groupedCopy = [...grouped];
    const relevantGroup = groupedCopy.find((g) => g.key === groupKey);
    if (!relevantGroup) {
      return;
    }
    const relevantSalesItem = relevantGroup.salesItems.find(
      (sItem: TItem) => sItem.ID === salesItem.ID
    );
    if (!relevantSalesItem) {
      return;
    }
    for (let i = 0; i < relevantSalesItem.Targets.length; i++) {
      const currentTarget = relevantSalesItem.Targets[i];
      if (currentTarget.ID === target.ID) {
        currentTarget.DestinationContactID = null;
        currentTarget.DestinationContact = null;
        currentTarget.DestinationContactKey = null;
        break;
      }
    }
    setGrouped(groupedCopy);
    setTargetedCarts([]); // Wipe the list because it's going to need to be reanalyzed
  }

  function onNewAddressEntered(
    newAddressData: INewAddressFormCallbackData,
    targetData: {
      groupKey: string;
      salesItem: TItem;
      target: SalesItemTargetBaseModel;
    }
  ) {
    parentSetRunning(t("ui.storefront.checkout.splitShipping.SavingANewAddress.Ellipses"));
    cvApi.accounts
      .GetCurrentAccount() //serves as authentication
      .then((_curAccount) => {
        // NOTE: original code called cvApi.geography.CreateAddressInBook
        return addressFactory.createNewAddress(newAddressData);
      })
      .then((r) => {
        const { groupKey, salesItem, target } = targetData;
        const groupedCopy = [...grouped];
        const relevantGroup = groupedCopy.find((g) => g.key === groupKey);
        const relevantItem = relevantGroup.salesItems.find((item) => item.ID === salesItem.ID);
        for (let i = 0; i < relevantItem.Targets.length; i++) {
          if (relevantItem.Targets[i].ID === target.ID) {
            relevantItem.Targets[i] = {
              ...relevantItem.Targets[i],
              DestinationContactID: r.SlaveID,
              DestinationContact: r.Slave,
              DestinationContactKey: r.Slave.CustomKey,
              TypeID: null,
              TypeKey: ServiceStrings.attributes.shipToHome,
              TypeName: null,
              Type: null
            };
            break;
          }
        }
        setGrouped(groupedCopy);
        // $rootScope.$broadcast(ServiceStrings.events.shipping.revalidateStep);
        parentFinishRunning();
      })
      .catch((err) => {
        //   // If getting current account or create new address call fails, store in local memory instead
        //   newAC.SlaveID = newAC.Slave.ID =
        //     Math.min(_.minBy(addressOptions, (x) => x.ID).ID, -3) - 1;
        //   if (target) {
        //     // This will stack negative numbers so we have individual values to select by with the dropdowns
        //     target.DestinationContactID = newAC.SlaveID;
        //     target.DestinationContact = newAC.Slave;
        //     target.DestinationContactKey = newAC.Slave.CustomKey;
        //     target.TypeID = null;
        //     target.TypeKey = ServiceStrings.attributes.shipToHome;
        //     target.TypeName = null;
        //     target.Type = null;
        //   }
        //   parentFinishRunning();
        //   // $rootScope.$broadcast(ServiceStrings.events.shipping.revalidateStep);
        //   return;
        parentFinishRunning(true, err);
      });
  }

  function canAllocateQuantity(
    salesItem: TItem,
    target: SalesItemTargetBaseModel,
    adjustment: number
  ): boolean {
    if (!salesItem || !target || !adjustment || !itemsExist(salesItem.Targets)) {
      consoleDebug("This item has no targets list, cannot modify quantity");
      return false;
    }
    const totalItemQuantity =
      salesItem.Quantity + (salesItem.QuantityBackOrdered || 0) + (salesItem.QuantityPreSold || 0);
    const wouldBeQuantityIsLessThanTotal = target.Quantity + adjustment < totalItemQuantity;
    if (!wouldBeQuantityIsLessThanTotal) {
      consoleDebug(
        "New target quantity would be equal to or greater than total quantity for sales item, can't allocate"
      );
      return false;
    }
    const targetQuantityPlusAdjustmentIsGreaterThanZero = target.Quantity + adjustment > 0;
    if (!targetQuantityPlusAdjustmentIsGreaterThanZero) {
      consoleDebug("New target quantity would be 0 or less, can't allocate");
      return false;
    }
    return true;
  }

  function setDestinationContactForTarget(
    groupKey: string,
    salesItem: TItem,
    target: SalesItemTargetBaseModel,
    contact: AccountContactModel
  ): void {
    const groupedCopy = [...grouped];
    const relevantGroup = groupedCopy.find((g) => g.key === groupKey);
    if (!relevantGroup) {
      return;
    }
    const relevantSalesItem = relevantGroup.salesItems.find(
      (sItem: TItem) => sItem.ID === salesItem.ID
    );
    if (!relevantSalesItem) {
      return;
    }
    for (let i = 0; i < relevantSalesItem.Targets.length; i++) {
      const currentTarget = relevantSalesItem.Targets[i];
      if (currentTarget.ID === target.ID) {
        currentTarget.DestinationContactID = contact.Slave.ID;
        currentTarget.DestinationContact = contact.Slave;
        currentTarget.DestinationContactKey =
          currentTarget.DestinationContact && currentTarget.DestinationContact.CustomKey;
        currentTarget.TypeID = null;
        currentTarget.TypeKey = ServiceStrings.attributes.shipToHome;
        currentTarget.TypeName = null;
        currentTarget.Type = null;
        break;
      }
    }
    setGrouped(groupedCopy);
    setTargetedCarts([]); // Wipe the list because it's going to need to be reanalyzed
    // this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
  }

  function allocateQuantity(
    salesItem: TItem,
    toTarget: SalesItemTargetBaseModel,
    adjustment: number = 1
  ): boolean {
    if (!canAllocateQuantity(salesItem, toTarget, adjustment)) {
      return false;
    }
    consoleDebug("Val-0: Attempting to allocate");
    for (let i = 0; i < salesItem.Targets.length; i++) {
      const fromTarget = salesItem.Targets[i];
      if (fromTarget.ID === toTarget.ID) {
        consoleDebug("Val-1: Same target, can't allocate");
        continue;
      }
      if (adjustment >= fromTarget.Quantity) {
        consoleDebug("Val-2: can't leave fromTarget quantity at less than 1, can't allocate");
        consoleDebug(`before: ${fromTarget.Quantity}`);
        consoleDebug(`before: ${toTarget.Quantity}`);
        continue;
      }
      consoleDebug("Val-3: Allocation checks passed, allocating now");
      const groupedCopy = [...grouped];
      const relevantGroup = groupedCopy.find((group) =>
        group.salesItems.find((sItem) => sItem.ID === salesItem.ID)
      );
      relevantGroup.salesItems = relevantGroup.salesItems.map((sItem: TItem) => {
        if (sItem.ID === salesItem.ID) {
          return {
            ...sItem,
            Targets: sItem.Targets.map((t) => {
              if (t.ID === fromTarget.ID) {
                t.Quantity -= adjustment;
              } else if (t.ID === toTarget.ID) {
                t.Quantity += adjustment;
              }
              return t;
            })
          };
        } else {
          return sItem;
        }
      });
      setGrouped(groupedCopy);
      break;
    }
  }

  function addShippingTarget(groupKey: string, item: TItem): void {
    if (!itemsExist(item.Targets)) {
      // NOTE: This should never happen, before this point, the UI has at least
      // created a default target to use in initializeSalesItems
      throw Error(
        `Cannot add shipping target. \n Targets Exist: ${!!item.Targets} \n Targets Amount: ${
          item.Targets?.length ?? "null"
        }`
      );
    }
    const groupedCopy = [...grouped];
    const relevantGroup = groupedCopy.find((gp) => gp.key === groupKey);
    if (!relevantGroup || !relevantGroup.salesItems.find((salesItem) => salesItem.ID === item.ID)) {
      console.log("No item to take quantity from in addShippingTarget");
      return;
    }
    for (let i = 0; i < item.Targets.length; i++) {
      if (canAllocateQuantity(item, item.Targets[i], -1)) {
        const newTarget = targetFactory(1);
        relevantGroup.salesItems = relevantGroup.salesItems.map((salesItem: TItem) => {
          if (salesItem.ID === item.ID) {
            salesItem.Targets[i].Quantity -= 1;
            salesItem.Targets.push(newTarget);
          }
          return salesItem;
        });
        setGrouped(groupedCopy);
        break;
      }
    }
  }

  function removeShippingTarget(
    groupKey: string,
    item: TItem,
    target: SalesItemTargetBaseModel
  ): void {
    if (!groupKey || !itemsExist(item.Targets) || !target) {
      return;
    }
    const groupedCopy = [...grouped];
    const relevantGroup = groupedCopy.find((group) => group.key === groupKey);
    if (!relevantGroup) {
      return;
    }
    const relevantSalesItem = relevantGroup.salesItems.find((sItem) => sItem.ID === item.ID);
    if (!relevantSalesItem) {
      return;
    }
    const targetToRemove = relevantSalesItem.Targets.find((t: any) => t.ID === target.ID);
    const toAddQuantity: number = targetToRemove.Quantity;
    relevantSalesItem.Targets = relevantSalesItem.Targets.filter(
      (t: SalesItemTargetBaseModel) => t.ID !== target.ID
    );
    relevantSalesItem.Targets[0].Quantity += toAddQuantity;
    setGrouped(groupedCopy);
    // $rootScope.$broadcast(cvServiceStrings.events.shipping.revalidateStep);
  }

  function submitAndGetRateQuotes() {
    parentSetRunning(t("ui.storefront.common.Analyzing.Elipses"));
    let targetedCartsCopy = [...targetedCarts];
    cvApi.shopping
      .UpdateCartItems({
        TypeName: "Cart",
        Items: grouped.reduce((curVal, group) => {
          return [...curVal, ...group.salesItems];
        }, [])
      })
      .then((res) => {
        if (!res || !res.data || res.status !== 200) {
          throw new Error("Failed to update cart items");
        }
        const dto: AnalyzeCurrentCartToTargetCartsDto = {
          WithCartInfo: { CartTypeName: "Cart" }, // TODO: Implement dynamic cart type
          IsSameAsBilling: false,
          IsPartialPayment: false,
          ResetAnalysis: true // Clear previous target setups
        };
        return cvApi.providers.AnalyzeCurrentCartToTargetCarts(dto);
      })
      .then((res) => {
        if (!res.data.ActionSucceeded) {
          throw new Error(res.data.Messages[0]);
        }
        targetedCartsCopy = res.data.Result.filter((x) => x.ID != null && x.TypeName !== "Cart");
        return Promise.all(
          targetedCartsCopy
            .filter((x) => x.ID != null && x.TypeName !== "Cart")
            .map((x) =>
              cvApi.shopping.GetCartItems({
                Active: true,
                AsListing: true,
                MasterID: x.ID
              })
            )
        );
      })
      .then((resArr) => {
        resArr.forEach((pagedResult) => {
          if (!pagedResult?.data?.Results || !pagedResult?.data?.TotalCount) {
            console.warn("No results on one of the paged results that should have had children!");
            return;
          }
          for (let i = 0; i < targetedCartsCopy.length; i++) {
            const cart = targetedCartsCopy[i];
            if (cart.ID === pagedResult.data.Results[0].MasterID) {
              cart.SalesItems = pagedResult.data.Results;
            }
          }
        });
        return Promise.all(
          targetedCartsCopy.map((x) =>
            cvApi.shopping.CurrentCartGetShippingContact({
              TypeName: x.TypeName
            })
          )
        );
      })
      .then((resArr) => {
        for (let i = 0; i < resArr.length; i++) {
          if (!resArr[i].data.ActionSucceeded) {
            targetedCartsCopy[i].ShippingContactID = null;
            targetedCartsCopy[i].ShippingContact = null;
            continue;
          }
          targetedCartsCopy[i].ShippingContact = resArr[i].data.Result;
          targetedCartsCopy[i].ShippingContactID = resArr[i].data.Result.ID;
        }
        setReadyToLoadShippingRateQuotes(true);
        // $rootScope.$broadcast(cvServiceStrings.events.shipping.revalidateStep);
        // $rootScope.$broadcast(cvServiceStrings.events.shipping.ready,
        //     cvCartService.accessCart(type),
        //     false); // Don't reapply the shipping contact info to the cart
        return Promise.all(
          targetedCartsCopy.map((cart) =>
            cvApi.shopping.GetCurrentCartShippingRateQuotes({
              TypeName: cart.TypeName,
              Expedited: false
            })
          )
        );
      })
      .then((resArr) => {
        for (let i = 0; i < resArr.length; i++) {
          const response = resArr[i];
          if (!response.data || !response.data.ActionSucceeded) {
            setGettingRateQuotes(false);
            parentFinishRunning(
              true,
              null,
              response.data.Messages ?? ["Failed to get shipping rate quotes"]
            );
            return;
          }
          const rateQuotes = response.data.Result;
          if (itemsExist(rateQuotes)) {
            for (let j = 0; j < rateQuotes.length; j++) {
              const curRateQuote = rateQuotes[j];
              // @ts-ignore
              curRateQuote["original"] = curRateQuote.Rate;
              // @ts-ignore
              curRateQuote["discounted"] = curRateQuote.Rate;
              const shippingDiscounts = itemsExist(cart.Discounts)
                ? cart.Discounts.filter((discount) => discount.DiscountTypeID === 2)
                : [];
              if (!itemsExist(shippingDiscounts)) {
                continue;
              }
              let discounted = curRateQuote.Rate;
              shippingDiscounts
                .sort(
                  (discountOne, discountTwo) =>
                    discountOne.DiscountPriority - discountTwo.DiscountPriority
                )
                .forEach((discount) => {
                  if (discount.DiscountValueType === 0) {
                    // Percent
                    discounted = discounted * (1 - discount.DiscountValue);
                  } else if (discount.DiscountValueType === 1) {
                    // Dollar
                    discounted = discounted - discount.DiscountValue;
                  }
                });
              // @ts-ignore
              curRateQuote["discounted"] = discounted;
            }
            const selected = rateQuotes.find((q) => q.Selected);
            if (selected) {
              targetedCartsCopy[i].Totals.Shipping = selected.Rate;
              // cvCartService.recalculateTotals(type);
            }
            targetedCartsCopy[i].RateQuotes = rateQuotes;
            // selectedRateQuoteID = selectedID;
          }
        }
        targetedCartsCopy.sort((cartOne, cartTwo) => {
          if (cartOne.NothingToShip && cartTwo.NothingToShip) {
            return 0;
          }
          if (cartOne.NothingToShip) {
            return -1;
          }
          return 1;
        });
        setTargetedCarts(targetedCartsCopy);
        setGettingRateQuotes(false);
        parentFinishRunning();
      })
      .catch((err) => {
        parentFinishRunning(true, err ?? "Failed to get rate quotes");
      });
  }

  function consoleDebug(...args: unknown[]) {
    if (cefConfig?.debug) {
      console.debug(...args);
    }
  }

  return (
    <Row>
      <Col xs={12}>
        {grouped &&
          grouped.length &&
          grouped.map((group: { key: string; salesItems: Array<TItem> }) => {
            return (
              <Card
                className={`mb-3 ${!parentViewState.hasError ? "is-valid" : ""}`}
                key={group.key}>
                {parentViewState.running && <LoadingWidget overlay={true} padIn={true} />}
                <div id={`ShipmentGroupTitle_${group.key}`} className="card-header pl-3">
                  {group.key}
                </div>
                {group.salesItems.map((salesItem: TItem, outerIndex: number) => {
                  const {
                    ID,
                    ProductNothingToShip,
                    Quantity,
                    QuantityBackOrdered,
                    QuantityPreSold,
                    Name,
                    ProductName,
                    ProductKey,
                    Sku,
                    Targets
                  } = salesItem;
                  return (
                    <Fragment key={ID}>
                      <Card.Body className="px-3 pt-3 pb-1">
                        <Card.Title
                          as="div"
                          className={`mb-${
                            ProductNothingToShip && outerIndex === group.salesItems.length - 1
                              ? 2
                              : 0
                          }`}
                          id={`SplitShippingProductName_${outerIndex}`}>
                          {(ProductNothingToShip
                            ? Quantity + (QuantityBackOrdered || 0) + (QuantityPreSold || 0) + " x "
                            : "") +
                            (Name
                              ? Name + " (" + Sku + ")"
                              : ProductName + " (" + ProductKey + ")")}
                        </Card.Title>
                      </Card.Body>
                      {!ProductNothingToShip ? (
                        <div className="list-group list-group-flush border-0">
                          <div className="list-group-item px-3 pt-1 pb-3 border-bottom">
                            {Targets &&
                              Targets.map((target: SalesItemTargetBaseModel) => {
                                return (
                                  <SplitShippingTarget
                                    key={target.ID}
                                    target={target}
                                    accountContacts={getAvailableAccountContactsForTarget(
                                      target,
                                      Targets
                                    )}
                                    onContactChanged={(contact: AccountContactModel) => {
                                      if (!contact) {
                                        blankDestinationContactInfo(group.key, salesItem, target);
                                        return;
                                      }
                                      setDestinationContactForTarget(
                                        group.key,
                                        salesItem,
                                        target,
                                        contact
                                      );
                                    }}
                                    onAddressAdded={(
                                      newAddressData: INewAddressFormCallbackData
                                    ) => {
                                      onNewAddressEntered(newAddressData, {
                                        groupKey: group.key,
                                        salesItem,
                                        target
                                      });
                                    }}
                                    decreaseDisabled={
                                      target.Quantity <= 1 ||
                                      salesItem.Targets.length <= 1 ||
                                      parentViewState.running
                                    }
                                    increaseDisabled={
                                      target.Quantity >=
                                        salesItem.Quantity +
                                          (salesItem.QuantityBackOrdered || 0) +
                                          (salesItem.QuantityPreSold || 0) -
                                          (salesItem.Targets.length - 1) || parentViewState.running
                                    }
                                    onQuantityChanged={(val: number) => {
                                      let adjustment: number;
                                      if (val > target.Quantity) {
                                        adjustment = 1;
                                      } else {
                                        adjustment = -1;
                                      }
                                      allocateQuantity(salesItem, target, adjustment);
                                    }}
                                    removeDisabled={Targets.length <= 1 || parentViewState.running}
                                    addDisabled={
                                      Targets.length >=
                                        Quantity +
                                          (QuantityBackOrdered || 0) +
                                          (QuantityPreSold || 0) || parentViewState.running
                                    }
                                    onRemoveClicked={() => {
                                      removeShippingTarget(group.key, salesItem, target);
                                    }}
                                    onAddClicked={() => {
                                      addShippingTarget(group.key, salesItem);
                                    }}
                                    showAdd={
                                      Quantity +
                                        (QuantityBackOrdered || 0) +
                                        (QuantityPreSold || 0) >
                                      1
                                    }
                                  />
                                );
                              })}
                          </div>
                        </div>
                      ) : null}
                    </Fragment>
                  );
                })}
              </Card>
            );
          })}
        {shippingRatesNeeded ? (
          <Button
            variant="primary"
            className="mb-3"
            disabled={submitDisabled || parentViewState.running}
            onClick={submitAndGetRateQuotes}
            type="button">
            {t("ui.storefront.checkout.splitShipping.SubmitAndGetRateQuotesForShipments")}
          </Button>
        ) : null}
      </Col>
      {shippingRateQuotes && (
        <Col xs={12} className="my-4">
          <PurchaseSplitShippingRateQuotesManagerWidget
            quotes={shippingRateQuotes}
            onRateSelected={(quote) => setEstimatedShippingCost(quote.Rate)}
          />
        </Col>
      )}
      {parentViewState.hasError ? (
        <Col xs={12}>
          <ErrorView error={parentViewState.errorMessage} />
        </Col>
      ) : null}
      {/* HERE */}
    </Row>
  );
};
