import { WebStorageStateStore } from "oidc-client";
import { createUserManager } from "redux-oidc";
import { getHostPath } from "../envDetect";

const config = {
    authority: window._AUTHORITY_ENDPOINT,
    client_id: "blogmanagefront",
    redirect_uri: getHostPath() + "/callback",
    response_type: "code",
    scope: "openid profile manageapi",
    post_logout_redirect_uri: getHostPath() + "/signoutcallback",
    loadUserInfo: true,
};

const userManager = createUserManager({
    ...config,
    userStore: new WebStorageStateStore({
        store: window.localStorage,
        prefix: "ns-tool",
    }),
});

export { userManager };
