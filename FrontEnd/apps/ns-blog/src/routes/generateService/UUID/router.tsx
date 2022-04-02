import React from "react";
import { IRoute } from "types";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";

const UUIDRouter: IRoute = {
    name: RouterAboutConfig.GenerateService.UUID.Name,
    path: RouterAboutConfig.GenerateService.UUID.Path,
    auth: RouterAboutConfig.GenerateService.UUID.Auth,
    component: React.lazy(() => import("./UUID")),
};

export default UUIDRouter;
