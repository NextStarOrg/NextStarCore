import React from "react";
import { IRoute, IServiceRoute } from "types";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import GenerateDashboardRouter from "routes/generateService/Dashboard/router";
import GenerateIndex from "routes/generateService/GenerateIndex/SystemIndex";
import RandomStringRouter from "routes/generateService/RandomString/router";
import UUIDRouter from "routes/generateService/UUID/router";

const GenerateServicesRoutes: IServiceRoute = {
    key: RouterAboutConfig.GenerateService.BasePath,
    path: RouterAboutConfig.GenerateService.BasePath,
    indexComponent: GenerateIndex,
    routes: [GenerateDashboardRouter, RandomStringRouter, UUIDRouter],
};

export default GenerateServicesRoutes;
