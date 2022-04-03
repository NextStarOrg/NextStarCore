import {IRoute} from "types";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import Dashboard from "routes/blogService/Dashboard/Dashboard";
import React from "react";

const BlogDashboardRouter: IRoute = {
    name: RouterAboutConfig.BlogService.Dashboard.Name,
    path: RouterAboutConfig.BlogService.Dashboard.Path,
    auth: RouterAboutConfig.BlogService.Dashboard.Auth,
    component: React.lazy(() => import("./Dashboard")),
};

export default BlogDashboardRouter;
