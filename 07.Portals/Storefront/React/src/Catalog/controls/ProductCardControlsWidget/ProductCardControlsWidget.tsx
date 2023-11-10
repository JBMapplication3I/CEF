import { Fragment, useState } from "react";
import { faBell, faHeart, faStar } from "@fortawesome/free-regular-svg-icons";
import {
  faBell as faBellFilled,
  faClipboardList,
  faHeart as faHeartFilled,
  faSlidersH,
  faStar as faStarFilled
} from "@fortawesome/free-solid-svg-icons";
import cvApi from "../../../_api/cvApi";
import { useViewState } from "../../../_shared/customHooks/useViewState";
import { ConfirmationModal } from "../../../_shared/modals/ConfirmationModal";
import { useHistory } from "react-router";
import { PCCWOverlayLayout } from "./PCCWOverlayLayout";
import { PCCWHorizontalLayout } from "./PCCWHorizontalLayout";
import { PCCWVerticalLayout } from "./PCCWVerticalLayout";
import { connect } from "react-redux";
import { AddToShoppingListModal } from "../../../Dashboard/ShoppingList/AddToShoppingListModal";
import { CreateShoppingListModal } from "../../../Dashboard/ShoppingList/CreateShoppingListModal";
import {
  IProductCardControl,
  IProductCardControlsWidgetProps
} from "./_ProductCardControlsWidgetTypes";
import { LoginModal } from "../../../Authentication/LoginModal";
import { refreshCompareList, refreshStaticCart } from "../../../_redux/actions";
import { useTranslation } from "react-i18next";
import { IReduxStore } from "../../../_redux/_reduxTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const mapStateToProps = (state: IReduxStore) => {
  const { staticCarts } = state;
  return {
    favoritesList: staticCarts.favoritesList,
    wishList: staticCarts.wishList,
    notifyMeItems: staticCarts.notifyMeList,
    compareCart: staticCarts.compareList,
    shoppingLists: staticCarts.shoppingList,
    currentUser: state.currentUser,
    cefConfig: state.cefConfig
  };
};

export const ProductCardControlsWidget = connect(mapStateToProps)(
  (props: IProductCardControlsWidgetProps): JSX.Element => {
    const {
      product,
      inventory,
      render,
      hideFavoritesList,
      hideWishList,
      hideNotifyMe,
      hideCompare,
      hideI,
      hideShoppingList,
      currentUser, // From Redux
      favoritesList, // From Redux
      wishList, // From Redux
      notifyMe, // From Redux
      compareCart, // From Redux
      cefConfig // From Redux
    } = props;

    const [removeConfirmationModalData, setRemoveConfirmationModalData] = useState({
      show: false,
      type: "",
      onConfirm: () => {}
    });
    const [showLoginModal, setShowLoginModal] = useState<boolean>(false);
    const [showGoToCompareViewModal, setShowGoToCompareViewModal] = useState<boolean>(false);
    const [showAddToShoppingListModal, setShowAddToShoppingListModal] = useState<boolean>(false);
    const [showCreateShoppingListModal, setShowCreateShoppingListModal] = useState<boolean>(false);
    const history = useHistory();
    const { setRunning, finishRunning, viewState } = useViewState();
    const { t } = useTranslation();

    if (!product) {
      return <div></div>;
    }

    let isFavorite = false;
    let favoritesListTitleKey = "ui.storefront.common.Favorites.AddTo";
    let favoritesListIcon = faStar;
    if (favoritesList?.SalesItems && Array.isArray(favoritesList?.SalesItems)) {
      isFavorite = Boolean(favoritesList?.SalesItems.find((fi) => fi.ProductID === product.ID));
      if (isFavorite) {
        favoritesListTitleKey = "ui.storefront.common.Favorites.RemoveFrom";
        favoritesListIcon = faStarFilled;
      }
    }

    let isWish = false;
    let wishListTitleKey = "ui.storefront.common.WishList.AddTo";
    let wishListIcon = faHeart;
    if (wishList?.SalesItems && Array.isArray(wishList?.SalesItems)) {
      isWish = Boolean(wishList?.SalesItems.find((wi) => wi.ProductID === product.ID));
      if (isWish) {
        wishListTitleKey = "ui.storefront.common.WishList.RemoveFrom";
        wishListIcon = faHeartFilled;
      }
    }

    let isNotifyMeItem = false;
    let notifyMeTitleKey = "ui.storefront.common.InStockAlert.Add";
    let notifyMeIcon = faBell;
    if (notifyMe?.SalesItems && Array.isArray(notifyMe?.SalesItems)) {
      isNotifyMeItem = Boolean(notifyMe?.SalesItems.find((ni) => ni.ProductID === product.ID));
      if (isNotifyMeItem) {
        notifyMeTitleKey = "ui.storefront.common.InStockAlert.Remove";
        notifyMeIcon = faBellFilled;
      }
    }

    let isCompare = false;
    let compareTitleKey = "ui.storefront.common.Compare.AddTo";
    let compareIcon = faSlidersH;
    if (compareCart?.SalesItems && Array.isArray(compareCart?.SalesItems)) {
      isCompare = Boolean(compareCart?.SalesItems.find((ci) => ci.ProductID === product.ID));
      if (isCompare) {
        compareTitleKey = "ui.storefront.common.Compare.RemoveFrom";
      }
    }

    function resetRemoveConfirmationModal() {
      setRemoveConfirmationModalData({
        show: false,
        type: "",
        onConfirm: () => {}
      });
    }

    function addItemToList(TypeName: string) {
      setRunning();
      cvApi.shopping
        .AddStaticCartItem({
          ProductID: product.ID,
          Quantity: product.quantity,
          TypeName
        })
        .then((res: any) => {
          if (res.data.ActionSucceeded) {
            refreshStaticCart(TypeName);
            finishRunning();
          } else {
            finishRunning(true, ` failed to add item to ${TypeName}`);
          }
        })
        .catch((err: any) => {
          finishRunning(true, err.message || "Failed to add item to list");
        });
    }

    function removeItemFromList(TypeName: string) {
      setRunning();
      cvApi.shopping
        .RemoveStaticCartItemByProductIDAndType({
          ProductID: product.ID,
          TypeName
        })
        .then(() => {
          resetRemoveConfirmationModal();
          refreshStaticCart(TypeName);
          finishRunning();
        })
        .catch((err: any) => {
          resetRemoveConfirmationModal();
          console.log(err);
          finishRunning(true, err.message || "Failed to remove item from list");
        });
    }

    function toggle(isInList: boolean, TypeName: string) {
      if (isInList) {
        setRemoveConfirmationModalData({
          show: true,
          type: TypeName,
          onConfirm: () => removeItemFromList(TypeName)
        });
      } else if (currentUser.CustomKey) {
        addItemToList(TypeName);
      } else {
        setShowLoginModal(true);
      }
    }

    function addItemToCompareCart() {
      setRunning();
      cvApi.shopping
        .AddCompareCartItem({
          ProductID: product.ID,
          SerializableAttributes: {},
          StoreID: null
        })
        .then((res) => {
          refreshCompareList();
          if (compareCart) {
            setShowGoToCompareViewModal(true);
          }
          finishRunning();
        })
        .catch((err: any) => {
          console.log(err);
          finishRunning(true, err.message || "Failed to add item to compare cart");
        });
    }

    function removeItemFromCompareCart() {
      setRunning();
      cvApi.shopping
        .RemoveCompareCartItemByProductID(product.ID)
        .then((res) => {
          finishRunning();
          refreshCompareList();
        })
        .catch((err: any) => {
          console.log(err);
          finishRunning(true, err.message || "Failed to remove item from compare cart");
        });
    }

    function toggleCompareItem() {
      if (isCompare) {
        removeItemFromCompareCart();
      } else {
        addItemToCompareCart();
      }
    }

    const renderContent = () => {
      // remove title when implementing translations
      const items: IProductCardControl[] = [
        {
          hide: hideFavoritesList || !cefConfig.featureSet.carts.favoritesList.enabled,
          idPrefix: "btnAddToFavoritesListProduct",
          titleKey: favoritesListTitleKey,
          onClick: () => toggle(isFavorite, "Favorites List"),
          icon: <FontAwesomeIcon icon={favoritesListIcon} />,
          isSelected: isFavorite
        },
        {
          hide: hideWishList || !cefConfig.featureSet.carts.wishList.enabled,
          idPrefix: "btnAddToWishListProduct",
          titleKey: wishListTitleKey,
          onClick: () => toggle(isWish, "Wish List"),
          icon: <FontAwesomeIcon icon={wishListIcon} />,
          isSelected: isWish
        },
        {
          hide: hideNotifyMe || !cefConfig.featureSet.carts.notifyMeWhenInStock.enabled,
          idPrefix: "btnAddToNotifyMeListProduct",
          titleKey: notifyMeTitleKey,
          onClick: () => toggle(isNotifyMeItem, "Notify Me When In Stock"),
          icon: <FontAwesomeIcon icon={notifyMeIcon} />,
          isSelected: isNotifyMeItem
        },
        {
          hide: hideCompare || !cefConfig.featureSet.carts.compare.enabled,
          idPrefix: "btnAddToCompareListProduct",
          titleKey: compareTitleKey,
          onClick: () => toggleCompareItem(),
          icon: <FontAwesomeIcon icon={compareIcon} />,
          isSelected: isCompare
        },
        {
          hide: hideShoppingList,
          idPrefix: "btnAddToShoppingListProduct",
          titleKey: "ui.storefront.product.detail.productDetails.addToShoppingList",
          onClick: () => setShowAddToShoppingListModal(true),
          icon: <FontAwesomeIcon icon={faClipboardList} />,
          isSelected: false
        }
      ];

      const layoutProps = {
        product,
        items,
        running: viewState.running
      };

      switch (render) {
        case "overlay":
          return <PCCWOverlayLayout {...layoutProps} />;
        case "horizontal":
          return <PCCWHorizontalLayout {...layoutProps} />;
        case "vertical":
          return <PCCWVerticalLayout {...layoutProps} />;
        default:
          return <PCCWHorizontalLayout {...layoutProps} />;
      }
    };

    const hideShoppingListModal = () => setShowCreateShoppingListModal(false);

    return (
      <Fragment>
        <ConfirmationModal
          show={removeConfirmationModalData.show}
          title="Remove Item"
          confirmBtnLabel="Remove Item"
          onConfirm={removeConfirmationModalData.onConfirm}
          onCancel={resetRemoveConfirmationModal}>
          <p>
            {t("ui.storefront.cart.AreYouSureYouWantToRemove")} <strong>{product.Name}</strong> from
            your <strong className="text-lowercase">{removeConfirmationModalData.type}</strong>?
          </p>
        </ConfirmationModal>
        <ConfirmationModal
          show={showGoToCompareViewModal}
          title="Please Confirm"
          onConfirm={() => history.push("/catalog/compare")}
          onCancel={() => setShowGoToCompareViewModal(false)}>
          <p>{t("ui.storefront.product.widgets.compare.CompareModalConfirmationMessage")}</p>
        </ConfirmationModal>
        <AddToShoppingListModal
          show={showAddToShoppingListModal}
          onCancel={() => setShowAddToShoppingListModal(false)}
          product={product}
        />
        <CreateShoppingListModal
          show={showCreateShoppingListModal}
          onCancel={hideShoppingListModal}
          onConfirm={hideShoppingListModal}
        />
        {renderContent()}
        <LoginModal
          show={showLoginModal}
          onConfirm={() => {
            setShowLoginModal(false);
          }}
          onCancel={() => setShowLoginModal(false)}
        />
      </Fragment>
    );
  }
);
