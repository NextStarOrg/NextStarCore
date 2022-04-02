import React, { useCallback, useEffect } from "react";
import { SignoutCallbackComponent } from "redux-oidc";
import { userManager } from "utils/auth-utils/user-manager";
import { changeLogoutStatus } from "routes/publicService/rtk/auth";
import { AsyncStatus } from "types";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { redirectToLogin } from "utils/auth-utils";
import GlobalLoading from "components/GlobalLoading/GlobalLoading";
import { reject } from "lodash";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import { useTranslation } from "react-i18next";

const SignOutCallback = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const { t } = useTranslation("PublicService");
    useEffect(() => {
        dispatch(changeLogoutStatus(AsyncStatus.Pending));
    }, [dispatch]);

    const successCallback = useCallback(
        (user) => {
            dispatch(changeLogoutStatus(AsyncStatus.Fulfilled));
            navigate(RouterAboutConfig.RootPath);
        },
        [dispatch, navigate]
    );

    const errorCallback = useCallback(
        (error: Error): void => {
            dispatch(changeLogoutStatus(AsyncStatus.Rejected));
            navigate(RouterAboutConfig.RootPath);
        },
        [dispatch, navigate]
    );
    return (
        <SignoutCallbackComponent
            userManager={userManager}
            successCallback={successCallback}
            errorCallback={errorCallback}
        >
            <GlobalLoading message={t("LogoutIn")} />
        </SignoutCallbackComponent>
    );
};

export default SignOutCallback;
