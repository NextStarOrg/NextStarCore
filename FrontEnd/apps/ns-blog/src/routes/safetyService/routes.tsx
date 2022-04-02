import React from "react";
import {IRoute, IServiceRoute} from "types";
import SafetyIndex from "routes/safetyService/SafetyIndex/SystemIndex";
import SafetyDashboardRouter from "routes/safetyService/Dashboard/router";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import OneWayEncryptionRouter from "routes/safetyService/OneWayEncryption/router";
import HmacRouter from "routes/safetyService/Hmac/router";


const SafetyServicesRoutes: IServiceRoute = {
    key: RouterAboutConfig.SafetyService.BasePath,
    path: RouterAboutConfig.SafetyService.BasePath,
    indexComponent: SafetyIndex,
    routes: [SafetyDashboardRouter, OneWayEncryptionRouter,HmacRouter]
};

export default SafetyServicesRoutes;
