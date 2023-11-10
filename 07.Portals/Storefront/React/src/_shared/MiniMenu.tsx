import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { LoginModal } from "../Authentication/LoginModal";
import { logUserOut } from "../_redux/actions";
import { CEFConfig, IDashboardSettings, IReduxStore } from "../_redux/_reduxTypes";
import { useTranslation } from "react-i18next";
import { LoadingWidget } from "./common/LoadingWidget";
import axios from "../axios";
import cvApi from "../_api/cvApi";
import { UserModel } from "../_api/cvApi._DtoClasses";
import { Button } from "react-bootstrap";
import { useHistory } from "react-router";

interface IMiniMenuProps {
  currentUser: UserModel; // redux
  cefConfig: CEFConfig; // redux
}

interface IMapStateToMiniMenuProps {
  currentUser: UserModel;
  cefConfig: CEFConfig;
}

const mapStateToProps = (state: IReduxStore): IMapStateToMiniMenuProps => {
  return {
    currentUser: state.currentUser,
    cefConfig: state.cefConfig
  };
};

export const MiniMenu = connect(mapStateToProps)((props: IMiniMenuProps): JSX.Element => {
  const { currentUser, cefConfig } = props;

  const [showLoginModal, setShowLoginModal] = useState<boolean>(false);
  const [showDropdown, setShowDropdown] = useState<boolean>(false);
  const [miniMenuLinks, setMiniMenuLinks] = useState<Array<IDashboardSettings>>([]);

  const history = useHistory();

  const { t } = useTranslation();

  useEffect(() => {
    if (
      cefConfig &&
      cefConfig.dashboard &&
      cefConfig.featureSet.login.enabled &&
      currentUser &&
      currentUser.Contact
    ) {
      populateMiniMenuLinks();
    }
  }, [cefConfig, currentUser]);

  async function populateMiniMenuLinks() {
    let enabledLinks: Array<IDashboardSettings> = cefConfig.dashboard
      .reduce((curVal, newVal) => {
        const combined = [...curVal, newVal];
        if (newVal.children) {
          combined.push(...newVal.children);
        }
        return combined;
      }, [])
      .filter((x: IDashboardSettings) => x.enabled && x.href)
      .sort(
        (linkOne: IDashboardSettings, linkTwo: IDashboardSettings) => linkOne.order - linkTwo.order
      );
    const linksToSet: Array<IDashboardSettings> = await getUsableLinks(enabledLinks);
    setMiniMenuLinks(linksToSet);
  }

  async function getUsableLinks(links: Array<IDashboardSettings>) {
    const linksToSet: Array<IDashboardSettings> = [];
    for (let i = 0; i < links.length; i++) {
      const link = links[i];
      const roleRequired = link.reqAnyRoles && link.reqAnyRoles.length;
      const permissionRequired = link.reqAnyPerms && link.reqAnyPerms.length;
      if (!roleRequired && !permissionRequired) {
        linksToSet.push(link);
        continue;
      }
      if (roleRequired) {
        const roleResults: Array<boolean> = [];
        for (let j = 0; j < link.reqAnyRoles.length; j++) {
          const role = link.reqAnyRoles[j];
          let userHasRole;
          try {
            await cvApi.authentication.CurrentUserHasRole({
              Name: role
            });
            userHasRole = true;
          } catch (err) {
            userHasRole = false;
          }
          roleResults.push(userHasRole);
        }
        if (roleResults.includes(true)) {
          linksToSet.push(link);
          continue;
        }
      }
      if (permissionRequired) {
        const permissionResults = [];
        for (let j = 0; j < link.reqAnyPerms.length; j++) {
          const permission = link.reqAnyPerms[i];
          let userHasPermission;
          try {
            await cvApi.authentication.CurrentUserHasPermission({
              Name: permission
            });
            userHasPermission = true;
          } catch (err) {
            userHasPermission = false;
          }
          permissionResults.push(userHasPermission);
        }
        if (permissionResults.includes(true)) {
          linksToSet.push(link);
        }
      }
    }
    return linksToSet;
  }

  const logout = (e: React.MouseEvent<HTMLAnchorElement>) => {
    axios
      .post("/auth/logout")
      .then((_res: any) => {
        localStorage.removeItem("user");
        logUserOut();
        history.push("/");
        history.go(0);
      })
      .catch((err: any) => {
        console.log(err);
      });
    setShowDropdown(false);
    e.preventDefault();
  };

  if (cefConfig && cefConfig.featureSet && !cefConfig.featureSet.login.enabled) {
    return null;
  }

  if (!currentUser!.Contact) {
    return (
      <>
        <Button
          variant="secondary"
          className="pointer"
          onClick={() => setShowLoginModal(true)}>
          Sign In
        </Button>
        <LoginModal
          show={showLoginModal}
          onConfirm={() => {
            setShowLoginModal(false);
            setShowDropdown(false);
          }}
          onCancel={() => setShowLoginModal(false)}
        />
      </>
    );
  }

  return (
    <>
      <Button
        variant="secondary"
        className={`btn btn-secondary dropdown-toggle text-capitalize ${
          showDropdown ? "show" : ""
        }`}
        id="dropdownUser"
        onClick={() => setShowDropdown(!showDropdown)}
        aria-label="user-dropdown"
        aria-haspopup="true"
        aria-expanded={showDropdown}
        data-reference="parent">
        &nbsp;
        <span className="control-label">Hi, </span>
        {currentUser!.DisplayName ?? currentUser!.UserName}
      </Button>
      <div
        className={`dropdown-menu dropdown-menu-right ar-3 ${showDropdown ? "show" : ""}`}
        style={{ left: "unset" }}
        aria-labelledby="dropdownUser">
        {miniMenuLinks.length ? (
          miniMenuLinks.map((item) => {
            return (
              <div className="dropdown-item px-2" key={item.titleKey}>
                <a onClick={() => setShowDropdown(false)} href={item.href}>
                  {t(item.titleKey)}
                </a>
              </div>
            );
          })
        ) : (
          <LoadingWidget size="lg" />
        )}
        {/* {cefConfig.featureSet.stores.myStoreAdmin.enabled &&
          currentUser.Roles.includes("CEF Store Administrator") && (
            <>
              <div className="separator">
                <hr className="m-0" />
              </div>
              <div className="dropdown-item" role="presentation">
                <a
                  className="w-100"
                  rel="nofollow"
                  id="btnMiniMenuMyStoreAdmin"
                  name="btnMiniMenuMyStoreAdmin">
                  <span>{t("ui.storefront.location.myStore.myStore")}</span>
                </a>
              </div>
            </>
          )} */}
        {/* {cefConfig.featureSet.stores.myBrandAdmin.enabled &&
          currentUser.Roles.includes("CEF Brand Administrator") && (
            <>
              <div className="separator">
                <hr className="m-0" />
              </div>
              <div className="dropdown-item" role="presentation">
                <a
                  className="w-100"
                  rel="nofollow"
                  id="btnMiniMenuMyStoreAdmin"
                  name="btnMiniMenuMyStoreAdmin">
                  <span>{t("ui.storefront.location.myStore.myStore")}</span>
                </a>
              </div>
            </>
          )} */}
        <div className="separator">
          <hr className="m-0" />
        </div>
        <div className="log-item dropdown-item px-2">
          <a
            className="d-flex justify-content-between align-items-center text-red"
            href="/"
            onClick={logout}>
            <span className="text">{t("ui.storefront.common.Logout")}</span>
            <i className="icon-logout"></i>
          </a>
        </div>
      </div>
    </>
  );
});
