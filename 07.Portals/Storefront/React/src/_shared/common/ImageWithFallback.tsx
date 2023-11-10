/**
 * @file React/src/_shared/common/ImageWithFallback.tsx
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc CefConfig hook
 */
import { ImgHTMLAttributes, SyntheticEvent, useState } from "react";
import { CEFConfig } from "../../_redux/_reduxTypes";
import { connect } from "react-redux";
import { corsImageLink } from "./Cors";

interface IImageWithFallbackProps extends ImgHTMLAttributes<any> {
  cefConfig?: CEFConfig; //redux
  dispatch?: any; //a synthetic property from react
  kind?: keyof CEFConfig["images"];
}

const ImageWithFallback = ({
  cefConfig,
  src,
  alt,
  dispatch,
  kind,
  ...props
}: IImageWithFallbackProps) => {
  const [imgSrc, setImgSrc] = useState<string | undefined>(src);
  // This is to prevent infinite image loading if 'corsDefaultImageLink' also fails to load placeholder.jpg
  const [fallBackError, setFallBackError] = useState<boolean>(false);
  const onError = (event: SyntheticEvent<HTMLImageElement, Event>) => {
    if (fallBackError) {
      return;
    }
    setFallBackError(true);
    // Will cause corsImageLink to fallback to placeholder, see Cors.tsx
    return setImgSrc(undefined);
  };

  return (
    <img
      alt={alt}
      src={corsImageLink(cefConfig, imgSrc, kind)}
      onError={onError}
      {...props}
    />
  );
};

interface IMapStateToImageWithFallbackProps {
  cefConfig: CEFConfig;
}

const mapStateToProps = (state: any): IMapStateToImageWithFallbackProps => {
  return {
    cefConfig: state.cefConfig
  };
};

export default connect(mapStateToProps)(ImageWithFallback);
