import { connect } from "react-redux";
import { CEFConfig, IReduxStore } from "../_redux/_reduxTypes";
import { LanguageSelectorButton } from "./LanguageSelectorButton";
import { MiniMenu } from "./MiniMenu";
import { MicroCart } from "../Cart/views";
import { ExternalSearchBox } from "../Catalog/controls";

interface IHeaderMidProps {
  cefConfig?: CEFConfig; //redux
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    cefConfig: state.cefConfig
  };
};

export const HeaderMid = connect(mapStateToProps)((props: IHeaderMidProps) => {
  const { cefConfig } = props;

  const LogoImg = "/Portals/0/clarity-ecommerce-logo.png";

  let languagesEnabled = cefConfig?.featureSet?.languages?.enabled;

  return (
    <>
      <div className="col-12 text-center col-md-auto">
        <a className="navbar-brand" href="/" title="Clarity eCommerce Development Website">
          <img
            className="img-fluid lazyload"
            alt="Clarity eCommerce Development Website"
            width="175"
            height="38"
            src={LogoImg}
          />
        </a>
        {/* <div cef-brand-formatting-header></div> */}
      </div>
      <div className="col form-inline">
        <ExternalSearchBox />
      </div>
      <div
        className="col-12 col-xl-auto"
        style={{
          minHeight: cefConfig && cefConfig.featureSet.languages.enabled ? 70 : 0
        }}>
        <div className={`row align-items-center ${languagesEnabled ? "mt-2" : ""}`}>
          <div className="col-auto nav-item" id="react-microQuoteCart">
            <MicroCart type="Quote" />
          </div>
          <div className="col-auto nav-item" id="react-microCart">
            <MicroCart type="Cart" />
          </div>
          <div className="nav-item col-auto ml-auto dropdown">
            <MiniMenu />
          </div>
        </div>
        {cefConfig && cefConfig.featureSet.languages.enabled ? (
          <div className="row align-items-center mb-2">
            {/* <div className="nav-item col">
              <div cef-affiliate-account-selector></div>
            </div> */}
            <div className="col-auto nav-item">
              <LanguageSelectorButton />
            </div>
          </div>
        ) : null}
      </div>
    </>
  );
});
