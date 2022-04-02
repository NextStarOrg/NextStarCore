import { IRoute } from "types";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import React from "react";

const GenerateDashboardRouter: IRoute = {
    name: RouterAboutConfig.GenerateService.Dashboard.Name,
    path: RouterAboutConfig.GenerateService.Dashboard.Path,
    auth: RouterAboutConfig.GenerateService.Dashboard.Auth,
    component: React.lazy(()=> import("./Dashboard")),
};

export default GenerateDashboardRouter;
