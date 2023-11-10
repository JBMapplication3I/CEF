import {
  AccountContactModel,
  AccountModel,
  CartModel,
  UserModel
} from "../../_api/cvApi._DtoClasses";
import { CEFConfig } from "../../_redux/_reduxTypes";

export interface IMapStateToRegistrationProps {
  cart?: CartModel; // redux
  cefConfig: CEFConfig; //redux
  currentAccount?: AccountModel; // redux
  currentAccountAddressBook?: Array<AccountContactModel>; // redux
  currentUser?: UserModel; // redux
}

export interface IRegistrationProps {
  logUserIn?: Function; // redux
  cefConfig?: CEFConfig; // redux
  currentUser?: UserModel; // redux
}

export interface IRegistrationStepBasicInfoData {
  firstNameRegistration: string;
  lastNameRegistration: string;
  emEmailRegistration: string;
  pwPasswordRegistration: string;
}
