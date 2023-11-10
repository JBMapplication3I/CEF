import React, { useState } from "react";
import { connect } from "react-redux";
import { CEFConfig, IReduxStore } from "../_redux/_reduxTypes";
import { ConfirmationModal } from "./modals";
import { useViewState } from "./customHooks/useViewState";
import { LoadingWidget } from "./common/LoadingWidget";
import { useTranslation } from "react-i18next";
import cvApi from "../_api/cvApi";
import { ErrorView } from "./common/ErrorView";
import { Button } from "react-bootstrap";
interface ILanguageSelectorProps {
  cefConfig?: CEFConfig;
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    cefConfig: state.cefConfig
  };
};

export const LanguageSelectorButton = connect(mapStateToProps)((props: ILanguageSelectorProps) => {
  const { cefConfig } = props;

  const [showSelectLanguageModal, setShowSelectLanguageModal] = useState<boolean>(false);
  const [availableLanguages, setAvailableLanguages] = useState<Array<any>>([]);

  const { t, i18n } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const onConfirm = () => {
    setRunning();
    cvApi.globalization
      .GetLanguageByKey(i18n.language)
      .then((res) => {
        setShowSelectLanguageModal(false);
      })
      .catch((err) => {
        finishRunning(true, err);
      });
  };

  const onLanguageSelectorButtonClicked = () => {
    setShowSelectLanguageModal(true);
    setRunning();
    cvApi.globalization
      .GetLanguages()
      .then((res) => {
        if (!res.data || !res.data.Results) {
          finishRunning(true, "No languages found");
          return;
        }
        setAvailableLanguages(res.data.Results);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  };

  const currentLng = availableLanguages.length
    ? availableLanguages.find((lang) => lang.Locale === i18n.language)
    : null;

  return (
    <>
      <ConfirmationModal
        onConfirm={onConfirm}
        onCancel={() => setShowSelectLanguageModal(false)}
        title="" // intentionally empty
        show={showSelectLanguageModal}
        confirmDisabled={viewState.hasError}
        confirmBtnLabel="Select">
        {viewState.running ? (
          <LoadingWidget />
        ) : (
          <select
            value={i18n.language}
            onChange={(e: React.ChangeEvent<HTMLSelectElement>) =>
              i18n.changeLanguage(e.target.value)
            }>
            {availableLanguages && Array.isArray(availableLanguages)
              ? availableLanguages.map((lang) => {
                  return (
                    <option key={lang.ID} value={lang.Locale}>
                      {lang.UnicodeName}
                    </option>
                  );
                })
              : null}
          </select>
        )}
        {viewState.errorMessage ? <ErrorView error={viewState.errorMessage} /> : null}
      </ConfirmationModal>
      <Button variant="secondary" onClick={onLanguageSelectorButtonClicked}>
        {currentLng ? currentLng.UnicodeName : i18n.language}
      </Button>
    </>
  );
});
