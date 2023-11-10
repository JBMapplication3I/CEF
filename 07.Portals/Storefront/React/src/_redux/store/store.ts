import { createStore } from "redux";
import { rootReducer } from "../reducers/reducers";
import { CEFConfig } from "../_reduxTypes";
import { setupSignalRConnection } from "../../signalR/signalR";
import cvApi from "../../_api/cvApi";
import {
  AccountContactModel,
  AccountModel,
  CartModel,
  UserModel
} from "../../_api/cvApi._DtoClasses";
import { CEFActionResponseT, IHttpPromiseCallbackArg } from "../../_api/cvApi.shared";
import { HubConnection } from "@microsoft/signalr";
import { ServiceStrings } from "../../_shared/ServiceStrings";

export const store = createStore(rootReducer);

/* only functions for initializing store go in this file */
async function getCefConfigsForStore(): Promise<CEFConfig> {
  let res: Function;
  while (!cvApi) {
    await new Promise((resolve) => {
      const timeout = setTimeout(() => {
        resolve(true);
        clearTimeout(timeout);
      }, 100);
    });
  }
  try {
    // comes back as string initially;
    let str = "return " + (await cvApi.jsConfigs.GetStoreFrontCEFConfigAlt()).data;
    res = new Function(str);
  } catch (err) {
    console.log(err);
    return;
  }
  return new Promise((resolve, _reject) => {
    store.dispatch({
      type: ServiceStrings.redux.actionTypes.cefConfig.setCefConfig,
      payload: res() as CEFConfig
    });
    resolve(res() as CEFConfig);
  });
}

function getCurrentUser(): Promise<UserModel | boolean> {
  return new Promise((resolve, reject) => {
    cvApi.contacts
      .GetCurrentUser()
      .then((result) => {
        if (!result) {
          resolve(false);
          return;
        }
        store.dispatch({
          type: ServiceStrings.redux.actionTypes.user.logUserIn,
          payload: result.data
        });
        resolve(result.data);
      })
      .catch((err: any) => {
        console.error(`store.ts:getCurrentUser\n\t${err}`);
        resolve(false);
      });
  });
}

function getCurrentAccount(): Promise<AccountModel | boolean> {
  return new Promise((resolve, _reject) => {
    cvApi.accounts
      .GetCurrentAccount()
      .then((result) => {
        if (!result) {
          resolve(false);
          return;
        }
        store.dispatch({
          type: ServiceStrings.redux.actionTypes.currentAccount.setCurrentAccount,
          payload: result.data
        });
        resolve(result.data);
      })
      .catch((err: any) => {
        console.error(`store.ts:getCurrentAccount\n\t${err}`);
        resolve(false);
      });
  });
}

async function initializeSignalRConnection(): Promise<any> {
  let signalRConnection: HubConnection;
  console.log("store.ts:Attempting to establish SignalR connection");
  signalRConnection = await setupSignalRConnection();
  if (!signalRConnection) {
    console.error(`store.ts:initializeSignalRConnection failed`);
  }
  return new Promise((resolve, _reject) => {
    if (signalRConnection) {
      console.log("store.ts:signalRConnection state:", signalRConnection.state);
      store.dispatch({
        type: ServiceStrings.redux.actionTypes.signalR.setSignalRConnection,
        payload: signalRConnection
      });
      resolve(true);
      return;
    }
    resolve(false);
  });
}

export function getCurrentAccountAddressBook(): Promise<Array<AccountContactModel> | boolean> {
  return new Promise((resolve, _reject) => {
    cvApi.geography
      .GetCurrentAccountAddressBook()
      .then((result) => {
        if (!result) {
          resolve(false);
          return;
        }
        store.dispatch({
          type: ServiceStrings.redux.actionTypes.currentAccountAddressBook
            .setCurrentAccountAddressBook,
          payload: result.data
        });
        resolve(result.data);
      })
      .catch((err: any) => {
        console.error(`store.ts:getCurrentAccountAddressBook\n\t${err}`);
        resolve(false);
      });
  });
}

function getFavoritesListForStore(): void {
  cvApi.shopping
    .GetCurrentStaticCart({ TypeName: "Favorites List" })
    .then((res: IHttpPromiseCallbackArg<CartModel>) => {
      store.dispatch({
        type: ServiceStrings.redux.actionTypes.staticCarts.setFavoritesList,
        payload: res.data
      });
    })
    .catch((err: any) => {
      console.log(err);
    });
}
function getWishListForStore(): void {
  cvApi.shopping
    .GetCurrentStaticCart({ TypeName: "Wish List" })
    .then((res: IHttpPromiseCallbackArg<CartModel>) => {
      store.dispatch({
        type: ServiceStrings.redux.actionTypes.staticCarts.setWishList,
        payload: res.data
      });
    })
    .catch((err: any) => {
      console.log(err);
    });
}
function getNotifyMeListForStore(): void {
  cvApi.shopping
    .GetCurrentStaticCart({ TypeName: "Notify Me When In Stock" })
    .then((res: IHttpPromiseCallbackArg<CartModel>) => {
      store.dispatch({
        type: ServiceStrings.redux.actionTypes.staticCarts.setNotifyMeList,
        payload: res.data
      });
    })
    .catch((err: any) => {
      console.log(err);
    });
}

function getCartForStore(): void {
  cvApi.shopping
    .GetCurrentCart({
      TypeName: "Cart",
      Validate: true
    })
    .then((res: any) => {
      store.dispatch({
        type: ServiceStrings.redux.actionTypes.cart.setCart,
        payload: res.data.Result
      });
    })
    .catch((err: any) => {
      console.log(err);
    });
}

function getQuoteCartForStore(): void {
  cvApi.shopping
    .GetCurrentCart({
      TypeName: "Quote Cart",
      Validate: true
    })
    .then((res: IHttpPromiseCallbackArg<CEFActionResponseT<CartModel>>) => {
      store.dispatch({
        type: ServiceStrings.redux.actionTypes.quoteCart.setQuoteCart,
        payload: res.data.Result
      });
    })
    .catch((err: any) => {
      console.log(err);
    });
}

function getCompareCartForStore(): void {
  cvApi.shopping
    .GetCurrentCompareCart()
    .then((res: IHttpPromiseCallbackArg<CartModel>) => {
      if (res?.data?.SalesItems) {
        store.dispatch({
          type: ServiceStrings.redux.actionTypes.staticCarts.setCompareList,
          payload: res.data.SalesItems
        });
      }
    })
    .catch((err: any) => {
      console.log(err);
    });
}

function getCurrentUserCartTypesForStore(): void {
  cvApi.shopping
    .GetCurrentUserCartTypes({ IncludeNotCreated: false })
    .then((res: any) => {
      store.dispatch({
        type: ServiceStrings.redux.actionTypes.staticCarts.setShoppingLists,
        payload: res.data.Results
      });
    })
    .catch((err: any) => {
      console.log(err);
    });
}

function getCurrentUserWallet(): void {
  cvApi.payments
    .GetCurrentUserWallet()
    .then((res) => {
      if (res?.data?.Result) {
        store.dispatch({
          type: ServiceStrings.redux.actionTypes.wallet.setWallet,
          payload: res.data.Result
        });
      }
    })
    .catch((err: any) => {
      console.log(err);
    });
}

async function populateReduxStore() {
  const cefConfig: CEFConfig = (await getCefConfigsForStore()) as CEFConfig;
  if (!cefConfig) {
    console.error("Unable to load cefConfigs");
    return;
  }
  const currentUser = await getCurrentUser();
  if (currentUser) {
    getCurrentAccount();
    getCurrentAccountAddressBook();
  }
  getCartForStore();
  if (cefConfig.featureSet.salesQuotes.enabled) {
    getQuoteCartForStore();
  }
  if (currentUser && cefConfig.featureSet.carts.favoritesList.enabled) {
    getFavoritesListForStore();
  }
  if (currentUser && cefConfig.featureSet.carts.wishList.enabled) {
    getWishListForStore();
  }
  if (currentUser && cefConfig.featureSet.carts.notifyMeWhenInStock.enabled) {
    getNotifyMeListForStore();
  }
  if (currentUser && cefConfig.featureSet.carts.shoppingLists.enabled) {
    getCurrentUserCartTypesForStore();
  }
  if (cefConfig.featureSet.carts.compare.enabled) {
    getCompareCartForStore();
  }
  if (cefConfig.featureSet.signalR?.enabled) {
    initializeSignalRConnection();
  }
  if (cefConfig.featureSet.payments.wallet.enabled) {
    getCurrentUserWallet();
  }
}

populateReduxStore();
