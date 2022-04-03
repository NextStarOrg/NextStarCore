import React from "react";
import {IServiceRoute} from "types";
import BlogDashboardRouter from "routes/blogService/Dashboard/router";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import BlogIndex from "routes/blogService/BlogIndex/BlogIndex";


const BlogServicesRoutes: IServiceRoute = {
    key: RouterAboutConfig.BlogService.BasePath,
    path: RouterAboutConfig.BlogService.BasePath,
    indexComponent: BlogIndex,
    routes: [BlogDashboardRouter]
};

export default BlogServicesRoutes;
