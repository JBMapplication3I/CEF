/**
 * @file React/src/_shared/customHooks/useAddressFactory.tsx
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Pricing Factory hook
 */

import { useState, useEffect } from "react";
import { CEFConfig } from "../../_redux/_reduxTypes";
import { useCefConfig } from "./useCefConfig";
import cvApi from "../../_api/cvApi";
import { getCurrentAccountAddressBook, store } from "../../_redux/store/store";
import { AccountContactModel, UserModel } from "../../_api/cvApi._DtoClasses";
import { INewAddressFormCallbackData } from "../forms/NewAddressForm";

export interface IAddressFactory {
  getAddressBook: () => Promise<AccountContactModel[] | boolean>;
  validateAddress: (userAddressData: any) => Promise<boolean>;
  createNewAddress: (formData: any) => Promise<any>;
}

export class AddressFactory implements IAddressFactory {
  constructor(private readonly cefConfig: CEFConfig, private readonly currentUser: UserModel) {}

  getAddressBook = (): Promise<AccountContactModel[] | boolean> => {
    return getCurrentAccountAddressBook();
  };

  validateAddress = (userAddressData: INewAddressFormCallbackData): Promise<boolean> => {
    return new Promise((resolve, reject) => {
      cvApi.providers
        .ValidateAddress({
          Address: {
            ...userAddressData,
            Active: true,
            CountryCode: "USA",
            ID: null,
            // Street2: null,
            // Street3: null,
            // CountryCustom?: string;
            Region: userAddressData.Region,
            RegionCode: userAddressData.Region.Code,
            RegionID: userAddressData.Region.ID,
            RegionKey: userAddressData.Region.CustomKey,
            RegionName: userAddressData.Region.Name,
            Country: userAddressData.Country
          }
        })
        .then((res) => {
          resolve(res.data.IsValid);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  createNewAddress = (
    userAddressData: INewAddressFormCallbackData
  ): Promise<AccountContactModel> => {
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
      IsBilling,
      CreatedDate
    } = userAddressData;

    return new Promise((resolve, reject) => {
      const reduxStore = store.getState();
      if (!reduxStore.currentUser.AccountID) {
        reject("No user currently logged in");
        return;
      }
      let newAccountContactID: number;
      this.validateAddress(userAddressData)
        .then((res) => {
          if (!res) {
            reject("Address invalid");
            return;
          }
          return cvApi.accounts.CreateAccountContact({
            ID: null,
            Active: true,
            CreatedDate,
            CustomKey: null,
            IsBilling,
            IsPrimary: true,
            MasterID: store.getState().currentUser.AccountID,
            Name: null,
            Slave: {
              ID: null,
              TypeID: null,
              Active: true,
              Address: {
                Active: true,
                City,
                Company,
                Country,
                CountryCode: Country.Code,
                CountryKey: Country.CustomKey,
                CountryName: Country.Name,
                CreatedDate,
                CustomKey,
                PostalCode,
                Region,
                RegionCode: Region.Code,
                RegionID: Region.ID,
                RegionKey: Region.CustomKey,
                RegionName: Region.Name,
                Street1,
                ID: null
              },
              CreatedDate,
              Email1,
              Fax1,
              FirstName,
              LastName,
              Phone1,
              SameAsBilling: false,
              TypeKey: "Account Address"
            },
            SlaveID: null,
            TransmittedToERP: false,
            UpdatedDate: null
          });
        })
        .then((res) => {
          newAccountContactID = res.data.Result;
          return this.getAddressBook();
        })
        .then((addressBook) => {
          if (typeof addressBook === "boolean") {
            reject("Failed to get address book");
            return;
          }
          const newAccountContact = addressBook.find((ac) => ac.ID === newAccountContactID);
          resolve(newAccountContact);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };
}

export const useAddressFactory = (): IAddressFactory => {
  const [addressFactory, setAddressFactory] = useState(null);
  const cefConfig = useCefConfig() as CEFConfig;
  useEffect(() => {
    const reduxStore = store.getState();
    if (
      cefConfig == null ||
      !reduxStore?.currentUser?.userChecked ||
      !reduxStore?.currentUser?.ID
    ) {
      return;
    }
    // new up the class from above only once
    const instance = new AddressFactory(cefConfig, reduxStore.currentUser);
    setAddressFactory(instance);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cefConfig, store]);
  return addressFactory;
};
