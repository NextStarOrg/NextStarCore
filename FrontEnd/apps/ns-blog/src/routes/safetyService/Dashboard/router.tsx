import { IRoute } from "types";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import Dashboard from "routes/safetyService/Dashboard/Dashboard";
import React from "react";

const SafetyDashboardRouter: IRoute = {
    name: RouterAboutConfig.SafetyService.Dashboard.Name,
    path: RouterAboutConfig.SafetyService.Dashboard.Path,
    auth: RouterAboutConfig.SafetyService.Dashboard.Auth,
    component: React.lazy(()=> import("./Dashboard")),
};

export default SafetyDashboardRouter;
