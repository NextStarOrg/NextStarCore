import _ from "lodash";

export const pathPerfection = (path: string) => {
    if (path.indexOf("/") == 0) {
        return path;
    }
    return "/" + path;
}

export const getService = (pathname:string) => {
    var paths = _.filter(_.split(pathname, "/"), x => _.trim(x) != "");
    if(paths.length == 0){
        return "";
    }
    return paths[0];
}
