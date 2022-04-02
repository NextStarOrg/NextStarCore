import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import i18n from "locales/i18n";
import nsStorage from "utils/storage";
import {SiteLanguage} from "assets/consts/StoreCacheName";

export type ILanguageState = {
    lang: LanguageTypes;
};

const initialState: ILanguageState = {
    lang: i18n.language as LanguageTypes,
};

const languageSlice = createSlice({
    name: "prompt",
    initialState,
    reducers: {
        setLanguage: (state, action: PayloadAction<LanguageTypes>) => {
            i18n.changeLanguage(action.payload);
            nsStorage.set(SiteLanguage, action.payload);
            state.lang = action.payload;
            return state;
        },
    },
});

export type LanguageTypes = "zh" | "en";

export const {setLanguage} = languageSlice.actions;

export default languageSlice.reducer;
