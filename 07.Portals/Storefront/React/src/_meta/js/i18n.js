import i18n from "i18next";
import axios from "axios";
import { corsLink } from "../../_shared/common/Cors";
import cvApi from "../../_api/cvApi";
import { initReactI18next } from "react-i18next";
import resourcesToBackend from "i18next-resources-to-backend";
import LanguageDetector from "i18next-browser-languagedetector";

i18n
  // detect user language
  // learn more: https://github.com/i18next/i18next-browser-languageDetector
  .use(LanguageDetector)
  // pass the i18n instance to react-i18next.
  .use(initReactI18next)
  .use(
    resourcesToBackend((language, namespace, callback) => {
      cvApi.jsConfigs
        .GetStoreFrontCEFConfigAlt()
        .then((cefConfig) => {
          axios
            .get(
              corsLink(
                new Function("return" + cefConfig?.data)(),
                "/lib/cef/js/i18n/" + "ui.storefront." + language + ".json",
                "ui"
              )
            )
            .then((resources) => {
              callback(null, resources.data);
            })
            .catch((error) => {
              console.error("resourcesToBackEnd:Error\n", error);
              callback(error, null);
            });
        })
        .catch((err) => {
          console.error("resourcesToBackEnd:Error\n", err);
          callback(err, null);
        });
    })
  )
  // init i18next
  // for all options read: https://www.i18next.com/overview/configuration-options
  .init({
    debug: true,
    fallbackLng: "en_US",
    supportedLngs: [
      "en_US",
      "de_DE",
      "en_CA",
      "en_GB",
      "es_ES",
      "fr_FR",
      "it_IT",
      "ja_JP",
      "ko_KR",
      "pt_PT",
      "ru",
      "sw",
      "tr",
      "zh_Hans_CN",
      "zh_TW"
    ],
    interpolation: {
      escapeValue: false // not needed, react escapes by default
    }
  });

export default i18n;
