import { useState, useEffect, Fragment } from "react";
import { connect } from "react-redux";
import cvApi from "../../../_api/cvApi";
import { LoadingWidget } from "../../../_shared/common/LoadingWidget";
import { useViewState } from "../../../_shared/customHooks/useViewState";
import { PurchaseRateQuotesManagerWidget } from "../../PurchaseRateQuotesManagerWidget";
import { ICheckoutSplitShippingStepProps } from "../../_checkoutTypes";
import { Row, Col, Button, Card } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { Dictionary, SalesItemBaseModel } from "../../../_api/cvApi.shared";
import {
  AccountContactModel,
  AppliedCartItemDiscountModel,
  CartModel,
  RateQuoteModel,
  SalesItemTargetBaseModel
} from "../../../_api/cvApi._DtoClasses";
import { INewAddressFormCallbackData } from "../../../_shared/forms/NewAddressForm";
import { ServiceStrings } from "../../../_shared/ServiceStrings";
import { PurchaseSplitShippingRateQuotesManagerWidget } from "../../PurchaseSplitShippingRateQuotesManagerWidget";
import { IReduxStore } from "../../../_redux/_reduxTypes";
import {
  AnalyzeCurrentCartToTargetCartsDto,
  ApplyCurrentCartShippingRateQuoteDto
} from "../../../_api";
import { ErrorView } from "../../../_shared/common/ErrorView";
import { AddressBlock } from "../../../Dashboard/AddressBook/AddressBlock";
import { useAddressFactory } from "../../../_shared/customHooks/useAddressFactory";
import { SplitShippingTarget } from "./SplitShippingTarget";
import { SplitShippingTotals } from "./SplitShippingTotals";
import { CheckoutRateQuotes } from "./CheckoutRateQuotes";
import { CheckoutTargets } from "./CheckoutTargets";

const mapStateToProps = (state: IReduxStore) => {
  return {
    cart: state.cart,
    cefConfig: state.cefConfig,
    addressBook: state.currentAccountAddressBook
  };
};

export type TGroup = TGroupObject[];
export type TItem = SalesItemBaseModel<AppliedCartItemDiscountModel>;

type TGroupObject = { key: string; salesItems: Array<TItem> };

const idTracking: { [key: string]: null } = {};

export const CheckoutSplitShippingStep = connect(mapStateToProps)(
  (props: ICheckoutSplitShippingStepProps): JSX.Element => {
    const {
      onCompleteCheckoutSplitShippingStep,
      cartBillingContact,
      accountContacts,
      cart,
      cefConfig,
      continueText,
      addressBook
    } = props;

    const [estimatedShippingCost, setEstimatedShippingCost] = useState<number>(null);
    const [cartShippingContact, setCartShippingContact] = useState<AccountContactModel>(null);
    const [targetedCarts, setTargetedCarts] = useState<CartModel[]>([]);
    const [defaultShippingContactID, setDefaultShippingContactID] = useState<number>(null);
    const [grouped, setGrouped] = useState<TGroup>(null);
    const [submitDisabled, setSubmitDisabled] = useState(false);
    const [readyToLoadShippingRateQuotes, setReadyToLoadShippingRateQuotes] = useState(false);

    const { setRunning, finishRunning, viewState } = useViewState();
    const { t } = useTranslation();

    // const showShipToStoreOption = cefConfig.featureSet.shipping.shipToStore.enabled;
    // const showInStorePickupOption = cefConfig.featureSet.shipping.inStorePickup.enabled;
    const usePhonePrefixLookups = cefConfig?.featureSet?.contacts?.phonePrefixLookups?.enabled;
    const hideAddAddressOption = cefConfig?.featureSet?.addressBook?.dashboardCanAddAddresses;
    const shippingRatesEstimatorEnabled =
      cefConfig?.featureSet?.shipping?.rates?.estimator?.enabled;

    useEffect(() => {
      if (itemsExist(addressBook)) {
        const defaultShippingContact = addressBook.find((ad) => ad.IsPrimary);
        if (defaultShippingContact) {
          setDefaultShippingContactID(defaultShippingContact.SlaveID);
        } else {
          finishRunning(
            true,
            "Missing default shipping contact. Visit your address book to assign one, then return."
          );
        }
      }
    }, [addressBook]);

    useEffect(() => {
      if (itemsExist(cart.SalesItems) && !grouped) {
        initializeSalesItems();
      }
    }, [cart, defaultShippingContactID, addressBook]);

    useEffect(() => {
      setTargetedCarts([]);
    }, [grouped]);

    useEffect(() => {
      setDisableSubmitAndGetRateQuotes();
    }, [cefConfig, grouped]);

    function getRandomId(): number {
      // TODO: tracks the existing ones privately
      const newID = Math.round(Math.random() * 100000000000);
      if (idTracking[newID.toString()]) {
        return getRandomId();
      }
      idTracking[newID.toString()] = null;
      return newID;
    }

    function initializeSalesItems(): void {
      if (!cart || !itemsExist(cart.SalesItems) || !defaultShippingContactID) {
        return;
      }

      let validatedSalesItems = validateTargetQuantities(
        cart.SalesItems,
        defaultShippingContactID
      ).map((item) => ({
        ...item,
        Targets: setTargetGrouping(item.Targets)
      }));

      if (addressBook) {
        // assign target destination contacts
        validatedSalesItems = validatedSalesItems.map((sItem: TItem) => {
          return {
            ...sItem,
            Targets: setTargetDestinations(sItem.Targets)
          };
        });
      }

      const validGrouped = getSalesItemsGroupedByTitle(validatedSalesItems);

      setGrouped(validGrouped);
    }

    function validateTargetQuantities(
      items: Array<TItem>,
      defaultShippingContactID: number
    ): Array<TItem> {
      return items.map((item: TItem) => {
        // Check if the quantity changed since the last time the targets were generated
        const totalQuantity =
          item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0);
        if (!itemsExist(item.Targets) || !quantitiesMatch(item.Targets, totalQuantity)) {
          // Reset targets because it changed
          // When there is no list, create one with a default Target that has the full
          // quantity. If we have a default shipping address to use, assign that.
          // WARNING! This is the only location where a targets list should be
          // initialized in the entire platform!
          item.Targets = [targetFactory(totalQuantity, defaultShippingContactID)];
        }
        return item;
      });
    }

    function setTargetGrouping(
      targets: Array<SalesItemTargetBaseModel>
    ): Array<SalesItemTargetBaseModel> {
      // Check for and collapse duplicates in the full list, this is a processing issue
      // that happens sometimes, but easily corrected by re-grouping
      // Dictionary<Array<SalesItemTargetBaseModel>>
      const newGrouped: any = groupBy(targets, (target: SalesItemTargetBaseModel) => {
        return JSON.stringify({
          typeKey: target?.Type?.CustomKey ?? target.TypeKey,
          storeID: target.OriginStoreProductID,
          vendorID: target.OriginVendorProductID,
          ilID: target.OriginProductInventoryLocationSectionID,
          destID: target.DestinationContactID,
          nothingToShip: target.NothingToShip
        });
      });
      const groupedList: Array<SalesItemTargetBaseModel> = [];
      let groupedObj = Object.fromEntries(newGrouped);
      Object.keys(groupedObj).forEach((key) => groupedList.push(groupedObj[key][0]));
      return groupedList;
    }

    function setTargetDestinations(
      targets: Array<SalesItemTargetBaseModel>
    ): Array<SalesItemTargetBaseModel> {
      if (!addressBook) {
        return targets;
      }
      return targets.map((target) => {
        if (target.DestinationContactID) {
          const destinationContact = addressBook.find(
            (accountContact) => accountContact.SlaveID === target.DestinationContactID
          );
          if (destinationContact) {
            target.DestinationContact = destinationContact.Slave;
          }
        }
        return target;
      });
    }

    const onChangeWithRatesReset = () => {
      setRunning();
      cvApi.shopping
        .ClearCurrentCartShippingRateQuote({ TypeName: "Cart" })
        .then((_res) => finishRunning())
        .catch((err) => {
          finishRunning(true, err.message || "Failed to clear current cart shipping rate quote");
        });
    };

    function setDisableSubmitAndGetRateQuotes(): boolean {
      if (
        viewState.running ||
        !grouped ||
        cefConfig?.featureSet?.shipping?.splitShipping?.onlyAllowOneDestination
      ) {
        setSubmitDisabled(true);
        return;
      }
      let disable = false;
      outerLoop: for (let i = 0; i < grouped.length; i++) {
        for (let j = 0; j < grouped[i].salesItems.length; j++) {
          const curSalesItem = grouped[i].salesItems[j];
          const targetsWithoutDestination = curSalesItem.Targets.filter(
            (t) => !t.NothingToShip && !t.DestinationContact
          );
          if (targetsWithoutDestination.length) {
            disable = true;
            break outerLoop;
          }
        }
      }
      setSubmitDisabled(disable);
    }

    function targetFactory(quantity: number, contactID: number = null): SalesItemTargetBaseModel {
      let model: SalesItemTargetBaseModel = {
        ID: getRandomId(),
        Active: true,
        NothingToShip: false, // weird. If there's a target, there's something to ship
        CreatedDate: new Date(),
        DestinationContactID: contactID,
        DestinationContact:
          accountContacts?.find(
            (accountContact: AccountContactModel) => accountContact.SlaveID === contactID
          )?.Slave ?? null,
        OriginProductInventoryLocationSectionID: null,
        MasterID: null,
        OriginStoreProductID: null,
        OriginVendorProductID: null,
        SelectedRateQuoteID: null,
        TypeID: null,
        TypeKey: ServiceStrings.attributes.shipToHome,
        Quantity: quantity
      };
      return model;
    }

    function getSalesItemsGroupedByTitle(
      salesItems: TItem[]
    ): { key: string; salesItems: TItem[] }[] {
      // TODO: implement caching?
      if (!salesItems) {
        console.log("No sales items to group with");
        return;
      }

      const titlesMatchedToItems: any = {};
      for (let i = 0; i < salesItems.length; i++) {
        // TODO: remove any by adding Stores and Vendors to TItem in TS
        const curSalesItem = salesItems[i] as any;
        const salesItemTitleValues = [
          curSalesItem.ProductNothingToShip,
          curSalesItem.Stores ? curSalesItem.Stores[0].MasterName : null,
          curSalesItem.Vendors ? curSalesItem.Vendors[0].MasterName : null
        ];

        const groupTitle = getSplitShippingGroupTitle(salesItemTitleValues);
        if (!titlesMatchedToItems[groupTitle]) {
          titlesMatchedToItems[groupTitle] = [];
        }
        titlesMatchedToItems[groupTitle].push(salesItems[i]);
      }
      const sortedKeys = Object.keys(titlesMatchedToItems).sort();
      return sortedKeys.map((key) => ({
        key,
        salesItems: titlesMatchedToItems[key]
      }));
    }

    function getSplitShippingGroupTitle(source: string[]): string {
      // TODO: implement caching
      if (!source || !Array.isArray(source)) {
        return source as any;
      }
      let title = "";
      const [nothingToShip, storeName, vendorName] = source;
      title += nothingToShip ? "Non-shippable products" : "Shippable products";
      if (storeName) {
        title += " sold by " + storeName;
      }
      if (vendorName) {
        if (storeName) {
          title += " and";
        }
        title += " supplied by " + vendorName;
      }
      return title;
    }

    function groupBy(list: Array<any>, keyGetter: Function) {
      const map = new Map();
      list.forEach((item) => {
        const key = keyGetter(item);
        const collection = map.get(key);
        if (!collection) {
          map.set(key, [item]);
        } else {
          collection.push(item);
        }
      });
      return map;
    }

    function quantitiesMatch(targets: SalesItemTargetBaseModel[], quantity: number): boolean {
      return targets.reduce((acc: number, target) => acc + target.Quantity, 0) === quantity;
    }

    function itemsExist(items: unknown): boolean {
      return Array.isArray(items) && items?.length > 0;
    }

    function onRateQuoteSelected(cartTypeName: string, selectedRateQuoteID: number): void {
      setRunning();
      const dto: ApplyCurrentCartShippingRateQuoteDto = {
        // TODO: RequestedShipDate
        TypeName: cartTypeName,
        RateQuoteID: selectedRateQuoteID
      };
      cvApi.shopping
        .ApplyCurrentCartShippingRateQuote(dto)
        .then((r) => {
          if (!r.data.ActionSucceeded) {
            // this.cvViewStateService.processCEFActionResponseMessages(r.data);
            finishRunning(true, null, r.data.Messages);
            return;
          }
          return cvApi.shopping.GetCurrentCart({
            TypeName: cartTypeName,
            Validate: cartTypeName === ServiceStrings.carts.types.cart
          });
        })
        .then((res) => {
          if (!res.data.ActionSucceeded) {
            finishRunning(
              true,
              null,
              res.data.Messages ?? ["Failed to get current cart after rate quote selected"]
            );
            return;
          }
          setTargetedCarts((prevTargetedCarts) => {
            return prevTargetedCarts.map((cart) => {
              if (cart.TypeName === cartTypeName) {
                // TODO: just return res.data.Result
                // RateQuotes should be present in response to GetCurrentCart
                return {
                  ...cart,
                  RateQuotes: res.data.Result.RateQuotes
                };
              }
              return cart;
            });
          });
          finishRunning();
        })
        .catch((reason) => finishRunning(true, reason));
    }

    const somethingToShip = Array.isArray(grouped)
      ? grouped.find((grp) => grp.salesItems.some((sItem) => !sItem.ProductNothingToShip)) != null
      : false;

    const shippingRatesNeeded = itemsExist(grouped) && somethingToShip;

    return (
      <Row>
        <Col xs={12}>
          <h4 className="mt-2" id="WhichShippingAddressText">
            {t("ui.storefront.checkout.shippingTitle")}
          </h4>
          <div className="form-group checkbox mb-0">
            <p id="SplitShippingText">{t("ui.storefront.checkout.SplitShippingText")}</p>
          </div>
        </Col>
        <Col xs={12}>
          <CheckoutTargets
            cart={cart}
            grouped={grouped}
            setGrouped={setGrouped}
            targetedCarts={targetedCarts}
            setTargetedCarts={setTargetedCarts}
            setReadyToLoadShippingRateQuotes={setReadyToLoadShippingRateQuotes}
            targetFactory={targetFactory}
            submitDisabled={submitDisabled}
            shippingRatesNeeded={shippingRatesNeeded}
            cefConfig={cefConfig}
            accountContacts={accountContacts}
            parentViewState={viewState}
            parentSetRunning={setRunning}
            parentFinishRunning={finishRunning}
            setEstimatedShippingCost={setEstimatedShippingCost}
            itemsExist={itemsExist}
          />
          {/* SplitShippingTargetedCarts */}
          {itemsExist(targetedCarts) ? (
            <>
              <hr className="my-2" />
              <Row>
                <Col xs={12}>
                  <h4 id="lbResolvedToTargetsMessage">
                    {t(
                      shippingRatesEstimatorEnabled
                        ? "ui.storefront.checkout.splitShipping.EachItemHasBeenResolvedToAShipment.Message"
                        : "ui.storefront.checkout.splitShipping.EachItemHasBeenResolvedToAShipmentNoEstimator.Message"
                    )}
                  </h4>
                </Col>
                <CheckoutRateQuotes
                  targetedCarts={targetedCarts}
                  shippingRatesEstimatorEnabled={shippingRatesEstimatorEnabled}
                  parentViewState={viewState}
                  setEstimatedShippingCost={setEstimatedShippingCost}
                  onRateQuoteSelected={onRateQuoteSelected}
                />
              </Row>
              {readyToLoadShippingRateQuotes && shippingRatesEstimatorEnabled ? (
                <SplitShippingTotals targetedCarts={targetedCarts} masterCart={cart} />
              ) : null}
            </>
          ) : null}
          <Row>
            <Col xs={12}>
              <div className="d-flex mb-4">
                <Button
                  variant="primary"
                  disabled={
                    (shippingRatesNeeded &&
                      !targetedCarts.every((cart: CartModel) =>
                        cart.RateQuotes?.some((quote: RateQuoteModel) => quote.Selected)
                      )) ||
                    !defaultShippingContactID ||
                    (somethingToShip && !targetedCarts?.length)
                  } // TODO: Same as below
                  onClick={() => {
                    // TODO: Replace check with making sure a rate quote is selected for each
                    if (
                      (shippingRatesNeeded &&
                        !targetedCarts.every((cart: CartModel) =>
                          cart.RateQuotes?.some((quote: RateQuoteModel) => quote.Selected)
                        )) ||
                      !targetedCarts?.length
                    ) {
                      return;
                    }
                    onCompleteCheckoutSplitShippingStep(cartShippingContact);
                  }}>
                  {continueText}
                </Button>
              </div>
            </Col>
          </Row>
        </Col>
      </Row>
    );
  }
);

/* 
  Split shipping concerns:
    Shipping rates
    Addresses
    -- adding
    -- filtering
    Address/Product pairing
    Is product shippable?
    Inventory
    -- on back order?
    Stores/vendors (where products come from)
    TOTALS: Discounts, Taxes/(calculating/validating) total
  
    grouped: [
      {
        // title
        key: "Non-shippable products",
        value: [
          // salesItem
          {
            ProductName: "1 x Apple® Final Cut Pro® (AFC10.1.2)",
            TotalQuantity: 1,
          }
        ],
      },
      {
        key: "Shippable products",
        value: [
        // salesItem
        {
          ID: 5190,
          ProductName: "1 x Apple® Final Cut Pro® (AFC10.1.2)",
          TotalQuantity: 4,
          Targets: [
            {
              ID: 7157,
              MasterID: SalesItemID,
              DestinationContactID: 95,
              Quantity: 3
            },
            {
              ID: Anonymous,
              MasterID: SalesItemID,
              DestinationContactID: 96,
              DestinationContact: {
                Address: {
                  ...someAddress
                }
              }
              Quantity: 1
            },
            // whatever added here must not be already in the array
          ]
        }
      ],
      }
    ]
    }
*/
