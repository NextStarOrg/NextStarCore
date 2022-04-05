import { configureStore } from "@reduxjs/toolkit";
import logger from "redux-logger";
import { isDev } from "utils/envDetect";
import createAppReducer from "rtk/rtk";

const reducer = createAppReducer;

export const getStore = () => {
    return configureStore({
        reducer,
        middleware: (getDefaultMiddleware) => {
            const defaultMiddleware = getDefaultMiddleware({
                //close redux-toolkits serialize warning to redux-oidc state.
                //due to https://github.com/maxmantz/redux-oidc/issues/169
                //due to https://github.com/maxmantz/redux-oidc/issues/169#issuecomment-693474948
                serializableCheck: {
                    ignoredActions: ["redux-oidc/USER_FOUND"],
                    ignoredPaths: ["common.oidc"],
                },
            });
            if (isDev) {
                return defaultMiddleware.concat(logger);
            }
            return defaultMiddleware;
        },
        devTools: isDev,
        preloadedState: {},
    });
};
