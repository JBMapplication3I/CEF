import { useEffect, useState } from "react";
import { connect } from "react-redux";
import { CEFConfig, IReduxStore, IFeatureSet } from "../_redux/_reduxTypes";
import { useTranslation } from "react-i18next";
import React from "react";

interface ICefConfigKeyEnabledProps {
  cefConfig: CEFConfig; // From Redux
  cefConfigKey: string;
  children?: JSX.Element[] | React.ReactNode[];
}

const CefConfigKeyEnabled = (props: ICefConfigKeyEnabledProps): JSX.Element => {
  const { cefConfigKey, children, cefConfig } = props;

  function getKeyEnabled(): boolean {
    if (!cefConfig) {
      return false;
    }
    let result = cefConfigKey.split(".").reduce(function (value, index) {
      return value[index];
    }, cefConfig as any);
    return result;
  }

  return <>{getKeyEnabled() ? children : null}</>;
};

const mapStateToProps = (state: IReduxStore) => {
  return {
    cefConfig: state.cefConfig
  };
};

export default connect(mapStateToProps)(CefConfigKeyEnabled);
