/**
 * @file React/src/_shared/customHooks/useCefConfig.tsx
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc CefConfig hook
 */
import { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import { CEFConfig, IReduxStore } from "../../_redux/_reduxTypes";

export const useCefConfig = () => {
  const [cefConfig, setCefConfig] = useState<CEFConfig>(null);
  const result = useSelector((state: IReduxStore) => state.cefConfig);
  useEffect(() => {
    setCefConfig(result as CEFConfig);
  }, [result]);
  return cefConfig;
};
