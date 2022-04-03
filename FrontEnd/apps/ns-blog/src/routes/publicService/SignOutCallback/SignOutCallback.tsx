import React, { useCallback, useEffect } from "react";
import { SignoutCallbackComponent } from "redux-oidc";
import { userManager } from "utils/auth-utils/user-manager";
import { changeLogoutStatus } from "routes/publicService/rtk/auth";
import { AsyncStatus } from "types";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import GlobalLoading from "components/GlobalLoading/GlobalLoading";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";

const SignOutCallback = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
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
            <GlobalLoading message={"退出登录中..."} />
        </SignoutCallbackComponent>
    );
};

export default SignOutCallback;
