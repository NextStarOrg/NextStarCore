import React, { useCallback, useEffect } from "react";
import { CallbackComponent } from "redux-oidc";
import { userManager } from "utils/auth-utils/user-manager";
import { changeLoginStatus } from "routes/publicService/rtk/auth";
import { AsyncStatus } from "types";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import GlobalLoading from "components/GlobalLoading/GlobalLoading";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import nsStorage from "utils/storage";
import { PrevAuthUrl } from "assets/consts/StoreCacheName";

const SignInCallback = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(changeLoginStatus(AsyncStatus.Pending));
    }, [dispatch]);

    const successCallback = useCallback(
        (user) => {
            const prevUrl = nsStorage.get(PrevAuthUrl);
            if (prevUrl != undefined) {
                navigate(prevUrl);
            } else {
                navigate(RouterAboutConfig.RootPath);
            }
        },
        [dispatch]
    );

    const errorCallback = useCallback(
        (error: Error): void => {
            navigate(RouterAboutConfig.RootPath);
        },
        [dispatch, navigate]
    );
    return (
        <CallbackComponent
            userManager={userManager}
            successCallback={successCallback}
            errorCallback={errorCallback}
        >
            <GlobalLoading message={"正在登录中..."} />
        </CallbackComponent>
    );
};

export default SignInCallback;
