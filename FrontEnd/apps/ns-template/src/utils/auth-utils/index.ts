import { loadUser } from "redux-oidc";
import { EnhancedStore } from "@reduxjs/toolkit";
import { User } from "oidc-client";
import { userManager } from "utils/auth-utils/user-manager";
import { debounce, once } from "lodash";

/**
 * @description Loads potentially existing user data into the redux store, thus eliminating a new authentication roundtrip to the authentication server when a tab is closed or a new tab is opened.
 * @param store
 * @link https://github.com/maxmantz/redux-oidc/blob/master/docs/API.md#loaduser
 */
export const loadOidcUser = (store: EnhancedStore): Promise<User> => {
    return loadUser(store, userManager);
};

/***
 * @description 重定向至登录页面
 */
export const redirectToLogin = debounce(() => {
    //resetLastRequestTime();
    userManager.signinRedirect();
}, 3000);

/***
 * @description 退出登录
 */
export const redirectToLogout = once(() => {
    //resetLastRequestTime();
    userManager.clearStaleState();
    userManager.signoutRedirect();
});
