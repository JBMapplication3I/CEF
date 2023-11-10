import { combineReducers } from "redux";
import { currentUserReducer } from "./currentUserReducer";
import { currentAccountReducer } from "./currentAccountReducer";
import { currentAccountAddressBookReducer } from "./currentAccountAddressBookReducer";
import { cartReducer } from "./cartReducer";
import { quoteCartReducer } from "./quoteCartReducer";
import { walletReducer } from "./walletReducer";
import { staticCartsReducer } from "./staticCartsReducer";
import { cefConfigReducer } from "./cefConfigReducer";
import { signalRReducer } from "./signalRReducer";

export const rootReducer = combineReducers({
  currentUser: currentUserReducer,
  currentAccount: currentAccountReducer,
  currentAccountAddressBook: currentAccountAddressBookReducer,
  cart: cartReducer,
  staticCarts: staticCartsReducer,
  quoteCart: quoteCartReducer,
  wallet: walletReducer,
  cefConfig: cefConfigReducer,
  signalR: signalRReducer
});
