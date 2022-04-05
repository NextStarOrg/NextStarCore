import React from "react";
import {IServiceRoute} from "types";
import BlogDashboardRouter from "routes/blogService/Dashboard/router";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import BlogIndex from "routes/blogService/BlogIndex/BlogIndex";
import BlogCategoryRouter from "routes/blogService/Category/router";


const BlogServicesRoutes: IServiceRoute = {
    key: RouterAboutConfig.BlogService.BasePath,
    path: RouterAboutConfig.BlogService.BasePath,
    indexComponent: BlogIndex,
    routes: [BlogDashboardRouter, ...BlogCategoryRouter]
};

export default BlogServicesRoutes;
