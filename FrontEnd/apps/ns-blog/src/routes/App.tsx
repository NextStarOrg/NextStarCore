import React, {useCallback, useEffect, useRef} from "react";
import {Routes, Route, Navigate} from "react-router-dom";
import PublicRoutes from "routes/publicService/routes";
import {BuildRouterComponent} from "utils/routerUtils";
import _ from "lodash";
import AllRoutes from "routes/routes";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import LayoutIndex from "layout/LayoutIndex";
import {AsyncStatus, IRoute} from "types";
import RequireAuth from "components/RequireAuth/RequireAuth";
import GlobalLoading from "components/GlobalLoading/GlobalLoading";
import {ConfigProvider} from "antd";
import zhCN from "antd/lib/locale/zh_CN";
import {useDispatch, useSelector} from "react-redux";
import {selectAuthState} from "routes/publicService/rtk/selector";
import {changeLoginStatus} from "routes/publicService/rtk/auth";
import {useMount} from "ahooks";
import {store} from "store/store";
import {loadOidcUser, redirectToLogin} from "utils/auth-utils";

import dayjs from "dayjs";
import relativeTime from "dayjs/plugin/relativeTime";
import LocalizedFormat from "dayjs/plugin/localizedFormat";
import "dayjs/locale/zh";
import SpinLoading from "components/SpinLoading/SpinLoading";
import SignInCallbackRouter from "routes/publicService/SignInCallback/router";
import SignOutCallbackRouter from "routes/publicService/SignOutCallback/router";
import BlogServicesRoutes from "routes/blogService/routes";
import BlogDashboardRouter from "routes/blogService/Dashboard/router";
import CommonServicesRoutes from "routes/commonService/routes";
import NotFoundRouter from "routes/commonService/NotFound/router";
import ForbiddenRouter from "routes/commonService/Forbidden/router";
import {setLoadingStatus} from "routes/commonService/rtk/loading";

dayjs.extend(relativeTime);
dayjs.extend(LocalizedFormat);

const App = () => {
    const authState = useSelector(selectAuthState);
    const authStateRef = useRef(authState);
    const dispatch = useDispatch();
    const isLogin = authState.loginStatus === AsyncStatus.Fulfilled;
    const loadUserCallback = useCallback(
        async (user) => {
            console.log(authStateRef)
            //登录中或者登出中状态 不进行后续流程
            if (
                authStateRef.current.loginStatus === AsyncStatus.Pending ||
                authStateRef.current.logoutStatus === AsyncStatus.Pending
            ) {
                return;
            }

            if (user) {
                if (user.expires_in < -1) {
                    dispatch(changeLoginStatus(AsyncStatus.None));
                    if (
                        authStateRef.current.loginStatus === AsyncStatus.None ||
                        authStateRef.current.loginStatus ===
                        AsyncStatus.Rejected
                    ) {
                        // eslint-disable-next-line no-console
                        console.log(authStateRef.current);
                        redirectToLogin();
                    }
                } else {
                    dispatch(changeLoginStatus(AsyncStatus.Fulfilled));
                }
            } else {
                dispatch(changeLoginStatus(AsyncStatus.None));
                if (
                    authStateRef.current.loginStatus === AsyncStatus.None ||
                    authStateRef.current.loginStatus === AsyncStatus.Rejected
                ) {
                    // eslint-disable-next-line no-console
                    console.log(authStateRef.current);
                    redirectToLogin();
                }
            }
        },
        [dispatch]
    );
    useEffect(() => {
        console.log(authState)
        authStateRef.current = authState;
    }, [authState]);

    useMount(() => {
        loadOidcUser(store).then(loadUserCallback);
    });

    return (
        <ConfigProvider locale={zhCN}>
            <SpinLoading/>
            <React.Suspense fallback={<GlobalLoading/>}>
                <Routes>
                    {/* 公开路由 */}
                    <Route
                        key={SignInCallbackRouter.path}
                        path={RouterAboutConfig.RootPath + SignInCallbackRouter.path}
                        element={<SignInCallbackRouter.component/>}
                    />
                    <Route
                        key={SignOutCallbackRouter.path}
                        path={RouterAboutConfig.RootPath + SignOutCallbackRouter.path}
                        element={<SignOutCallbackRouter.component/>}
                    />
                </Routes>
                {isLogin && <AppAuth />}
            </React.Suspense>
        </ConfigProvider>
    );
};


const AppAuth = () => {
    return (
        <Routes>
            <Route
                key={RouterAboutConfig.RootPath}
                path={RouterAboutConfig.RootPath}
                element={<LayoutIndex/>}
            >
                <Route
                    key={"root_index"}
                    index
                    element={
                        <Navigate
                            to={BlogServicesRoutes.path}
                            replace={true}
                        />
                    }
                />
                <Route key={BlogServicesRoutes.key} path={BlogServicesRoutes.path}>
                    <Route
                        key={BlogServicesRoutes.key + "_root"}
                        index
                        element={<BlogServicesRoutes.indexComponent/>}
                    />
                    <Route
                        key={BlogDashboardRouter.name}
                        path={BlogDashboardRouter.path}
                        element={<BlogDashboardRouter.component/>}
                    />
                </Route>
                <Route key={CommonServicesRoutes.key} path={CommonServicesRoutes.path}>
                    <Route
                        key={CommonServicesRoutes.key + "_root"}
                        index
                        element={<CommonServicesRoutes.indexComponent/>}
                    />
                    <Route
                        key={NotFoundRouter.name}
                        path={NotFoundRouter.path}
                        element={<NotFoundRouter.component/>}
                    />
                    <Route
                        key={ForbiddenRouter.name}
                        path={ForbiddenRouter.path}
                        element={<ForbiddenRouter.component/>}
                    />
                </Route>
                <Route
                    path={"*"}
                    element={
                        <Navigate
                            to={
                                RouterAboutConfig.CommonService
                                    .BasePath +
                                "/" +
                                RouterAboutConfig.CommonService.NotFound
                                    .Path
                            }
                        />
                    }
                />
            </Route>
        </Routes>
    )
}

export default App;
