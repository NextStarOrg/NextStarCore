import React from 'react';
import {useLocation} from "react-router-dom";
import nsStorage from "utils/storage";
import {PrevAuthUrl} from "assets/consts/StoreCacheName";
import {useSelector} from "react-redux";
import {selectUser} from "selector/selector";
import {redirectToLogin} from "utils/auth-utils";

const RequireAuth = ({children, functionalityIds}: { children: JSX.Element, functionalityIds?: number[] }) => {
    const userState = useSelector(selectUser);
    const location = useLocation();
    const isLogin = userState.user != undefined && !userState.user.expired;
    if (!isLogin) {
        nsStorage.set(PrevAuthUrl, location.pathname);
        redirectToLogin();
        // Redirect them to the /login page, but save the current location they were
        // trying to go to when they were redirected. This allows us to send them
        // along to that page after they login, which is a nicer user experience
        // than dropping them off on the home page.
        return <React.Fragment>
            <div/>
        </React.Fragment>;
    }
    return children;
}

export default RequireAuth
