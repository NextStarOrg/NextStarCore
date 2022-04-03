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

dayjs.extend(relativeTime);
dayjs.extend(LocalizedFormat);

const App = () => {
    const authState = useSelector(selectAuthState);
    const authStateRef = useRef(authState);
    const dispatch = useDispatch();
    const isLogined = authState.loginStatus === AsyncStatus.Fulfilled;
    const loadUserCallback = useCallback(
        async (user) => {
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
        authStateRef.current = authState;
    }, [authState]);
    useMount(() => {
        loadOidcUser(store).then(loadUserCallback);
    });

    const renderRoute = (serviceKey: string, routes: IRoute[]) => {
        return routes.map((x) => {
            if (x.routes != undefined && x.routes?.length > 0) {
                return (
                    <Route
                        key={serviceKey + "_" + x.name}
                        path={x.path}
                        element={
                            x.auth ? (
                                <RequireAuth functionalityIds={x.functionalityId}>
                                    <x.component/>
                                </RequireAuth>
                            ) : (
                                <x.component/>
                            )
                        }
                    >
                        {renderRoute(serviceKey, x.routes)}
                    </Route>
                );
            } else {
                return (
                    <Route
                        key={serviceKey + "_" + x.name}
                        path={x.path}
                        element={
                            x.auth ? (
                                <RequireAuth functionalityIds={x.functionalityId}>
                                    <x.component/>
                                </RequireAuth>
                            ) : (
                                <x.component/>
                            )
                        }
                    />
                );
            }
        });
    };
    return (
        <ConfigProvider locale={zhCN}>
            <SpinLoading/>
            <React.Suspense fallback={<GlobalLoading/>}>
                <Routes>
                    {/* 公开路由 */}
                    {_.map(PublicRoutes, BuildRouterComponent)}
                    {isLogined && <Route
                        key={RouterAboutConfig.RootPath}
                        path={RouterAboutConfig.RootPath}
                        element={<LayoutIndex/>}
                    >
                        <Route
                            key={"root_index"}
                            index
                            element={
                                <Navigate
                                    to={AllRoutes[0].path}
                                    replace={true}
                                />
                            }
                        />
                        {AllRoutes.map((x) => {
                            // 如果有自己的layout，则嵌套加载自己的layout，但是请注意需要移除掉上面的根路径的layout，否则会出现多层layout
                            if (x.layoutComponent == undefined) {
                                return (
                                    <Route key={x.key} path={x.path}>
                                        <Route
                                            key={x.key + "_root"}
                                            index
                                            element={<x.indexComponent/>}
                                        />
                                        {x.routes != undefined &&
                                            renderRoute(x.key, x.routes)}
                                    </Route>
                                );
                            } else {
                                return (
                                    <Route
                                        key={x.key}
                                        path={x.path}
                                        element={<x.layoutComponent/>}
                                    >
                                        <Route
                                            key={x.key + "_root"}
                                            index
                                            element={<x.indexComponent/>}
                                        />
                                        {x.routes != undefined &&
                                            renderRoute(x.key, x.routes)}
                                    </Route>
                                );
                            }
                        })}
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
                    }
                </Routes>
            </React.Suspense>
        </ConfigProvider>
    );
};

export default App;
