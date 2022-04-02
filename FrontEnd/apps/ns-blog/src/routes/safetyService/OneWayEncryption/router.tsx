import React from 'react';
import {IRoute} from "types";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";

const OneWayEncryptionRouter: IRoute = {
    name: RouterAboutConfig.SafetyService.OneWayEncryption.Name,
    path: RouterAboutConfig.SafetyService.OneWayEncryption.Path,
    auth: RouterAboutConfig.SafetyService.OneWayEncryption.Auth,
    component: React.lazy(() => import("./OneWayEncryption")),
};

export default OneWayEncryptionRouter;
