import React from 'react';
import useAuth from "hooks/useAuth";
import {Navigate, useLocation} from "react-router-dom";
import nsStorage from "utils/storage";
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import {PrevAuthUrl} from "assets/consts/StoreCacheName";

const RequireAuth = ({children,functionalityIds}: { children: JSX.Element,functionalityIds?:number[] }) => {
    const [isLogin, userState] = useAuth();
    const location = useLocation();

    if (!isLogin) {
        nsStorage.set(PrevAuthUrl, location.pathname);
        // Redirect them to the /login page, but save the current location they were
        // trying to go to when they were redirected. This allows us to send them
        // along to that page after they login, which is a nicer user experience
        // than dropping them off on the home page.
        return <Navigate to={RouterAboutConfig.RootPath + RouterAboutConfig.CommonService.BasePath + RouterAboutConfig.CommonService.Forbidden.Path} replace/>;
    }
    return children;
}

export default RequireAuth
