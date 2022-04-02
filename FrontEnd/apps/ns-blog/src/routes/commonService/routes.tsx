import {IRoute, IServiceRoute} from "types";
import NotFoundRouter from "routes/commonService/NotFound/router";
import ForbiddenRouter from "routes/commonService/Forbidden/router";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import React from "react";
import CommonIndex from "routes/commonService/CommonIndex/CommonIndex";

const CommonServicesRoutes: IServiceRoute = {
    key: RouterAboutConfig.CommonService.BasePath,
    path:RouterAboutConfig.CommonService.BasePath,
    indexComponent: CommonIndex,
    routes:[ForbiddenRouter,NotFoundRouter]
};

export default CommonServicesRoutes;
