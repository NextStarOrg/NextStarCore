import React, {useCallback} from 'react';
import LoginBg from 'assets/images/login_bg.svg';
import styles from './login.module.scss';
import {Button} from "antd";
import {redirectToLogin} from "utils/auth-utils";
import {useDispatch} from "react-redux";
import {setLoadingStatus} from "routes/commonService/rtk/loading";

const Login = () => {
    const dispatch = useDispatch();
    const handleLogin = useCallback(()=>{
        dispatch(setLoadingStatus(true));
        redirectToLogin();
    },[]);
    return (
        <div className={styles.login} style={{backgroundImage:`url(${LoginBg})`}}>
            <div className={styles.loginMain}>
                <Button size={"large"} type={"primary"} onClick={handleLogin}>Login with Authing</Button>
            </div>
        </div>
    )
}

export default Login
