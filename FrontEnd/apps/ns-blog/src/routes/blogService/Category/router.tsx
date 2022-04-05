import {IRoute} from "types";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import React from "react";

const BlogCategoryRouter: IRoute = {
    name: RouterAboutConfig.BlogService.Category.Name,
    path: RouterAboutConfig.BlogService.Category.Path,
    auth: RouterAboutConfig.BlogService.Category.Auth,
    component: React.lazy(() => import("./Category")),
};


export default [BlogCategoryRouter];
