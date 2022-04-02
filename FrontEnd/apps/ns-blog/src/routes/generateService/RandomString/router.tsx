import React from 'react';
import {IRoute} from "types";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";

const RandomStringRouter: IRoute = {
    name: RouterAboutConfig.GenerateService.RandomString.Name,
    path: RouterAboutConfig.GenerateService.RandomString.Path,
    auth: RouterAboutConfig.GenerateService.RandomString.Auth,
    component: React.lazy(() => import("./RandomString")),
};

export default RandomStringRouter;
