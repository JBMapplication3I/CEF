import {
  AccountContactModel,
  AccountModel,
  CartModel,
  RateQuoteModel,
  UserModel,
  WalletModel
} from "../_api/cvApi._DtoClasses";
import { CEFConfig, ICart } from "../_redux/_reduxTypes";

export interface IPaymentMethodCreditCardBodyProps {
  continueText?: string;
  wallet: WalletModel[];
  onSubmit: Function;
  onSelectedWalletChanged?: Function;
  submitDisabled?: boolean;
  initialWalletID?: number;
}

export interface IPaymentMethodInvoiceBodyProps {
  continueText?: string;
  onSubmit: Function;
  submitDisabled?: boolean;
}

export interface IPaymentMethodQuoteBodyProps {
  continueText?: string;
  onSubmit: Function;
  submitDisabled?: boolean;
}

export interface ICreditCardFormData {
  txtCVV: string;
  txtCardHolderName: string;
  txtCardNumber: string;
  txtReferenceName: string;
  ddlExpirationMonth: string;
  ddlExpirationYear: string;
}

export interface IInvoiceFormData {
  txtPONumber: string;
}

export interface IMapStateToPaymentMethodCreditCardBodyProps {
  wallet: any;
}

/* PurchaseRateQuotesManagerWidget */
export interface IPurchaseRateQuotesManagerWidgetProps {
  quotes: RateQuoteModel[];
  onRateSelected: (rate: RateQuoteModel) => void;
}

export interface IPurchaseSplitShippingRateQuotesManagerWidgetProps {
  cefConfig: CEFConfig; // redux
  quotes: Array<RateQuoteModel>;
  onRateSelected: (quote: RateQuoteModel) => void;
}

/* Checkout */
export interface ICheckoutProps {
  cartType?: string;
  cart?: CartModel; // redux
  quoteCart?: CartModel; // redux
  cefConfig: CEFConfig; //redux
  currentAccount?: AccountModel; // redux
  currentAccountAddressBook?: Array<AccountContactModel>; // redux
  currentUser?: UserModel; // redux
}

export interface IMapStateToCheckoutProps {
  cart?: CartModel; // redux
  quoteCart?: CartModel; // redux
  cefConfig: CEFConfig; //redux
  currentAccount?: AccountModel; // redux
  currentAccountAddressBook?: Array<AccountContactModel>; // redux
  currentUser?: UserModel; // redux
}

/* Steps */

export interface ICheckoutMethodStepProps {
  continueText?: string;
  onCompleteCheckoutMethodStep: Function;
}

export interface ICheckoutBillingStepProps {
  continueText?: string;
  onCompleteCheckoutBillingStep: Function;
  currentUser?: UserModel;
  currentAccountAddressBook?: AccountContactModel[];
}

export interface ICheckoutShippingStepProps {
  continueText?: string;
  onCompleteCheckoutShippingStep: Function;
  cartBillingContact: any;
  accountContacts: Array<any>;
  cart?: ICart; // redux
}

export interface ICheckoutSplitShippingStepProps {
  continueText?: string;
  onCompleteCheckoutSplitShippingStep: Function;
  cartBillingContact: any;
  accountContacts: Array<AccountContactModel>;
  cart?: CartModel; // redux
  currentUser?: UserModel; // redux
  cefConfig?: CEFConfig; // redux
  addressBook?: AccountContactModel[]; // redux
}

export interface ICheckoutConfirmationStepProps {
  masterOrderID?: number;
  subOrderIDs?: Array<number>;
  orderID?: number;
  quoteIDs?: IQuoteIDs;
}

export interface ICheckoutPaymentStepProps {
  continueText?: string;
  onCompleteCheckoutPaymentStep: Function;
  currentAccountAddressBook?: AccountContactModel[];
  existingPaymentData?: any;
  initialBillingContact?: AccountContactModel;
}

export interface IQuoteIDs {
  master: number;
  slave: number;
}
