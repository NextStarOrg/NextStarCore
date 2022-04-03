import _ from "lodash";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import { pathPerfection } from "layout/utils/pathUtils";

export interface MenuItem {
    id:number;
    name: string;
    url: string;
    description: string;
    subMenus?:MenuItem[]
}

export const BlogManageMenus: MenuItem[] = [
    {
        id:30000000,
        name:"Dashboard",
        url: JoinPath(RouterAboutConfig.BlogService.BasePath,RouterAboutConfig.BlogService.Dashboard.Path),
        description:"仪表板",
    },
    {
        id:30010000,
        name:"分类管理",
        url: JoinPath(RouterAboutConfig.BlogService.BasePath,RouterAboutConfig.BlogService.Category.Path),
        description:"分类管理",
    },
    {
        id:30020000,
        name:"文章管理",
        url: JoinPath(RouterAboutConfig.BlogService.BasePath,RouterAboutConfig.BlogService.Article.Path),
        description:"文章管理",
    }
];

function JoinPath(...paths: string[]) {
    return pathPerfection(_.join(paths, "/"));
}
