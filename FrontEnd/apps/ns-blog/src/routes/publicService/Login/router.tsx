import {IRoute} from "types";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import React from "react";

const LoginRouter: IRoute = {
    name: RouterAboutConfig.Login.Name,
    path: RouterAboutConfig.Login.Path,
    auth: RouterAboutConfig.Login.Auth,
    component: React.lazy(() => import("./Login")),
};

export default LoginRouter;
