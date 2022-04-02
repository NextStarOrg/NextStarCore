import {IRoute} from "types";
import {Route} from "react-router-dom";
import React from "react";
import _ from "lodash";

export function BuildRouterComponent(route: IRoute): JSX.Element {
    //School Site暂时没有做公开页面的需求 全部先默认使用PrivateRoute在路由级别进行登录检查
    return (
        <Route
            key={route.path}
            path={route.path}
            element={<route.component />}
        />
    );
}
