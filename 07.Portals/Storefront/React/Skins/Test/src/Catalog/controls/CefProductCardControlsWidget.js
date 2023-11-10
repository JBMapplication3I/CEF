import React, { Fragment, useState } from "react";
import { faBell, faHeart, faStar } from "@fortawesome/free-regular-svg-icons";
import {
  faBell as faBellFilled,
  faHeart as faHeartFilled,
  faSlidersH,
  faStar as faStarFilled
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  AddCompareCartItem,
  AddStaticCartItem,
  RemoveCompareCartItemByProductID,
  RemoveStaticCartItemByProductIDAndType
} from "../../_api/cvApi.Shopping";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { ConfirmationModal } from "../../_shared/modals/ConfirmationModal";
import { useHistory } from "react-router";
// import classes from "./ProductCardControlsWidget.module.scss";

export const ProductCardControlsWidget = (props) => {
  const [removeConfirmationModalData, setRemoveConfirmationModalData] =
    useState({
      show: false,
      type: "",
      onConfirm: () => {}
    });
  const [showGoToCompareViewModal, setShowGoToCompareViewModal] =
    useState(false);
  const history = useHistory();
  const { setRunning, finishRunning, viewState } = useViewState();

  const {
    product,
    hideFavoritesList,
    hideWishList,
    hideNotifyMe,
    hideCompare,
    hideI,
    favoritesListItems,
    wishListItems,
    notifyMeItems,
    compareCartItems,
    getStaticCartItems,
    getCompareCart
  } = props;

  if (!product) {
    return <div></div>;
  }

  let isFavorite = false;
  let isWish = false;
  let isNotifyMeItem = false;
  let isCompare = false;
  if (favoritesListItems && Array.isArray(favoritesListItems)) {
    isFavorite = favoritesListItems
      .map((fi) => fi.ProductID)
      .includes(product.ID);
  }
  if (wishListItems && Array.isArray(wishListItems)) {
    isWish = wishListItems.map((wi) => wi.ProductID).includes(product.ID);
  }
  if (notifyMeItems && Array.isArray(notifyMeItems)) {
    isNotifyMeItem = notifyMeItems
      .map((nm) => nm.ProductID)
      .includes(product.ID);
  }
  if (compareCartItems && Array.isArray(compareCartItems)) {
    isCompare = compareCartItems.map((ci) => ci.ProductID).includes(product.ID);
  }

  function resetRemoveConfirmationModal() {
    setRemoveConfirmationModalData({
      show: false,
      type: "",
      onConfirm: () => {}
    });
  }

  function addItemToList(TypeName) {
    AddStaticCartItem({
      ProductID: product.ID,
      Quantity: product.quantity,
      TypeName
    })
      .then((res) => {
        if (res.data.ActionSucceeded) {
          getStaticCartItems(TypeName);
          finishRunning();
        } else {
          finishRunning(true, ` failed to add item to ${TypeName}`);
        }
      })
      .catch((err) => {
        finishRunning(true, err);
      });
  }

  function removeItemFromList(TypeName) {
    RemoveStaticCartItemByProductIDAndType({
      ProductID: product.ID,
      TypeName
    })
      .then((res) => {
        resetRemoveConfirmationModal();
        getStaticCartItems(TypeName);
      })
      .catch((err) => {
        resetRemoveConfirmationModal();
        console.log(err);
      });
  }

  function toggle(isInList, TypeName) {
    if (isInList) {
      setRemoveConfirmationModalData({
        show: true,
        type: TypeName,
        onConfirm: () => removeItemFromList(TypeName)
      });
    } else {
      addItemToList(TypeName);
    }
  }

  function addItemToCompareCart() {
    AddCompareCartItem({
      ProductID: product.ID,
      SerializeableAttributes: {},
      StoreID: null
    })
      .then((res) => {
        console.log(res);
        getCompareCart();
        setShowGoToCompareViewModal(true);
      })
      .catch((err) => console.log(err));
  }

  function removeItemFromCompareCart() {
    RemoveCompareCartItemByProductID(product.ID)
      .then((res) => {
        console.log(res);
        getCompareCart();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  function toggleCompareItem() {
    if (isCompare) {
      removeItemFromCompareCart();
    } else {
      addItemToCompareCart();
    }
  }

  let favoritesListTitle =
    "ui.storefront.product.catalog.results.grid.addToFavoritesList";
  if (isFavorite) {
    favoritesListTitle =
      "ui.storefront.product.catalog.results.grid.removeFromFavoritesList";
  }

  let wishListTitle = "ui.storefront.common.AddToWishList";
  if (isWish) {
    wishListTitle = "ui.storefront.common.RemoveFromWishList";
  }

  let notifyMeTitle = "ui.storefront.catalog.AddStockAlert";
  if (isNotifyMeItem) {
    notifyMeTitle = "ui.storefront.catalog.RemoveStockAlert";
  }

  let compareTitle = "ui.storefront.common.AddToCompare";
  if (isCompare) {
    compareTitle = "ui.storefront.common.RemoveFromCompare";
  }

  return (
    <Fragment>
      <ConfirmationModal
        show={removeConfirmationModalData.show}
        title="Remove Item"
        confirmBtnLabel="Remove Item"
        onConfirm={removeConfirmationModalData.onConfirm}
        onCancel={resetRemoveConfirmationModal}>
        <p>
          Are you sure you want to remove <strong>{product.Name}</strong> from
          your{" "}
          <strong className="text-lowercase">
            {removeConfirmationModalData.type}
          </strong>
          ?
        </p>
      </ConfirmationModal>
      <ConfirmationModal
        show={showGoToCompareViewModal}
        title="Please Confirm"
        onConfirm={() => history.push("/catalog/compare")}
        onCancel={() => setShowGoToCompareViewModal(false)}>
        <p>Would you like to compare products?</p>
      </ConfirmationModal>
      <div className="btn-group btn-group-justified w-100" role="group">
        {!hideFavoritesList && (
          <button
            className="btn btn-link wrap text-decoration-none"
            id={`btnGridAddToFavoritesListProduct${product.ID}`}
            title={favoritesListTitle}
            aria-label={favoritesListTitle}
            onClick={() => toggle(isFavorite, "Favorites List")}>
            <FontAwesomeIcon
              icon={isFavorite ? faStarFilled : faStar}
              className="fa-lg"
            />
            {!hideI && <br />}
          </button>
        )}
        {!hideWishList && (
          <button
            className="btn btn-link wrap"
            id={`btnGridAddToWishListProduct${product.ID}`}
            title={wishListTitle}
            aria-label={wishListTitle}
            onClick={() => toggle(isWish, "Wish List")}>
            <FontAwesomeIcon
              icon={isWish ? faHeartFilled : faHeart}
              className="fa-lg"
            />
            {!hideI && <br />}
          </button>
        )}
        {!hideNotifyMe && (
          <button
            className="btn btn-link wrap"
            id={`btnGridAddToNotifyMeListProduct${product.ID}`}
            title={notifyMeTitle}
            aria-label={notifyMeTitle}
            onClick={() => toggle(isNotifyMeItem, "Notify Me When In Stock")}>
            <FontAwesomeIcon
              icon={isNotifyMeItem ? faBellFilled : faBell}
              className="fa-lg"
            />
            {!hideI && <br />}
          </button>
        )}
        {!hideCompare && (
          <button
            className="btn btn-link wrap"
            id={`btnGridAddToCompareProduct${product.ID}`}
            title={compareTitle}
            aria-label={compareTitle}
            onClick={() => toggleCompareItem()}>
            <FontAwesomeIcon
              icon={faSlidersH}
              className={`fa-lg ${isCompare ? "text-dark" : "text-primary"}`}
            />
            {!hideI && <br />}
          </button>
        )}
      </div>
    </Fragment>
  );
};
