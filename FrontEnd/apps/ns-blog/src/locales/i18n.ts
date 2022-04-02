import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";

import TranslationEn from "locales/en/translation.json";
import TranslationZh from "locales/zh/translation.json";
import SafetyServiceEn from "routes/safetyService/locales/en/SafetyService.json";
import SafetyServiceZh from "routes/safetyService/locales/zh/SafetyService.json";
import GenerateServiceEn from "routes/generateService/locales/en/GenerateService.json";
import GenerateServiceZh from "routes/generateService/locales/zh/GenerateService.json";
import PublicServiceEn from "routes/publicService/locales/en/PublicService.json";
import PublicServiceZh from "routes/publicService/locales/zh/PublicService.json";
import { isDev } from "utils/envDetect";
// don't want to use this?
// have a look at the Quick start guide
// for passing in lng and translations on init

const resources = {
    en: {
        translation: TranslationEn,
        SafetyService: SafetyServiceEn,
        GenerateService: GenerateServiceEn,
        PublicService: PublicServiceEn,
    },
    zh: {
        translation: TranslationZh,
        SafetyService: SafetyServiceZh,
        GenerateService: GenerateServiceZh,
        PublicService: PublicServiceZh,
    },
};

i18n
    // detect user language
    // learn more: https://github.com/i18next/i18next-browser-languageDetector
    .use(LanguageDetector)
    // pass the i18n instance to react-i18next.
    .use(initReactI18next)
    // init i18next
    // for all options read: https://www.i18next.com/overview/configuration-options
    .init({
        fallbackLng: "en",
        lng: "en",
        debug: isDev,
        resources: resources,
        interpolation: {
            escapeValue: true, // not needed for react as it escapes by default
        },
    });

export default i18n;
