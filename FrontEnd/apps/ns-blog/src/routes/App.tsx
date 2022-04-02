import React, { useCallback, useEffect, useRef } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import PublicRoutes from "routes/publicService/routes";
import { BuildRouterComponent } from "utils/routerUtils";
import _ from "lodash";
import AllRoutes from "routes/routes";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import LayoutIndex from "layout/LayoutIndex";
import { AsyncStatus, IRoute } from "types";
import RequireAuth from "components/RequireAuth/RequireAuth";
import GlobalLoading from "components/GlobalLoading/GlobalLoading";
import nsStorage from "utils/storage";
import { SiteLanguage } from "assets/consts/StoreCacheName";
import i18n from "locales/i18n";
import { ConfigProvider, Spin } from "antd";
import zhCN from "antd/lib/locale/zh_CN";
import enUS from "antd/lib/locale/en_US";
import { useDispatch, useSelector } from "react-redux";
import { selectLanguage } from "routes/commonService/rtk/selector";
import { selectAuthState } from "routes/publicService/rtk/selector";
import { changeLoginStatus } from "routes/publicService/rtk/auth";
import { useMount } from "ahooks";
import { store } from "store/store";
import { loadOidcUser } from "utils/auth-utils";

import dayjs from "dayjs";
import relativeTime from "dayjs/plugin/relativeTime";
import LocalizedFormat from "dayjs/plugin/localizedFormat";
import "dayjs/locale/en";
import "dayjs/locale/zh";
import SpinLoading from "components/SpinLoading/SpinLoading";

dayjs.extend(relativeTime);
dayjs.extend(LocalizedFormat);

const App = () => {
    const authState = useSelector(selectAuthState);
    const authStateRef = useRef(authState);
    const dispatch = useDispatch();
    const language = useSelector(selectLanguage);

    useEffect(() => {
        if (language.lang == "zh") {
            document.title = "次星工具";
        } else {
            document.title = "NextStar Tools";
        }
    }, [language]);

    useEffect(() => {
        const init = async () => {
            let lang = nsStorage.get(SiteLanguage);
            if (lang == "zh") {
                lang = "zh";
            } else {
                lang = "en";
            }
            dayjs.locale(lang);
            await i18n.changeLanguage(lang);
            nsStorage.set(SiteLanguage, lang);
        };
        init();
    }, []);

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
                }
            }
        },
        [dispatch]
    );

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
                                <RequireAuth>
                                    <x.component />
                                </RequireAuth>
                            ) : (
                                <x.component />
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
                                <RequireAuth>
                                    <x.component />
                                </RequireAuth>
                            ) : (
                                <x.component />
                            )
                        }
                    />
                );
            }
        });
    };
    return (
        <ConfigProvider locale={language.lang == "zh" ? zhCN : enUS}>
            <SpinLoading />
            <React.Suspense fallback={<GlobalLoading />}>
                <Routes>
                    {/* 公开路由 */}
                    {_.map(PublicRoutes, BuildRouterComponent)}
                    <Route
                        key={RouterAboutConfig.RootPath}
                        path={RouterAboutConfig.RootPath}
                        element={<LayoutIndex />}
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
                                            element={<x.indexComponent />}
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
                                        element={<x.layoutComponent />}
                                    >
                                        <Route
                                            key={x.key + "_root"}
                                            index
                                            element={<x.indexComponent />}
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
                </Routes>
            </React.Suspense>
        </ConfigProvider>
    );
};

export default App;
