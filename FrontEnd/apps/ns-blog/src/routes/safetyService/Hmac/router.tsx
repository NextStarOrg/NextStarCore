import React from 'react';
import {IRoute} from "types";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";

const HmacRouter: IRoute = {
    name: RouterAboutConfig.SafetyService.Hmac.Name,
    path: RouterAboutConfig.SafetyService.Hmac.Path,
    auth: RouterAboutConfig.SafetyService.Hmac.Auth,
    component: React.lazy(() => import("./Hmac")),
};

export default HmacRouter;
