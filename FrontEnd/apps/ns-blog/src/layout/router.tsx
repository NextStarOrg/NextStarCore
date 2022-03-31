import React, { lazy } from "react";

export type IRoute = {
    component: React.LazyExoticComponent<any>;
    path: string;
    exact: boolean;
};
const IndexPage = lazy(
    () => import(/* webpackChunkName: "index-page" */ "pages/index/index")
);
// Git 页面
const GitPage = lazy(
    () =>
        import(/* webpackChunkName: "git-page" */ "pages/development/git/index")
);
// uuid 页面
const UUIDPage = lazy(
    () =>
        import(
            /* webpackChunkName: "uuid-page" */ "pages/development/uuid/index"
        )
);
// randomString 页面
const randomStringPage = lazy(
    () =>
        import(
            /* webpackChunkName: "random-string-page" */ "pages/development/randomString/index"
        )
);
// const NotFoundPage = lazy(
//   () => import(/* webpackChunkName: "not-found-page" */ 'pages/notFound/index'),
// );

//refer to https://reacttraining.com/react-router/web/example/route-config
const routeConfigs: IRoute[] = [
    {
        path: "dashboard",
        component: IndexPage,
        exact: true,
    },
    {
        path: "git",
        component: GitPage,
        exact: true,
    },
    {
        path: "uuid",
        component: UUIDPage,
        exact: true,
    },
    {
        path: "randomString",
        component: randomStringPage,
        exact: true,
    },
];

export default routeConfigs;
