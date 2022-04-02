import React, {useCallback, useEffect, useState} from 'react';
import {Menu} from "antd";
import {NavLink, useLocation} from 'react-router-dom';
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import {getService, pathPerfection} from "layout/utils/pathUtils";
import {useTranslation} from "react-i18next";

const TopNav = (props:{className?: string | undefined;}) => {
    const location = useLocation();
    const {t} = useTranslation();
    const [selectKey, setSelectKey] = useState<string[]>([""]);
    useEffect(() => {
        let init = function () {
            setSelectKey([getService(location.pathname)]);
        }
        init();
    }, [location.pathname])

    const handlerMenuSelect = useCallback(({item, key, keyPath, selectedKeys, domEvent}) => {
        setSelectKey(selectedKeys);
    }, []);

    return (
        <Menu theme="light" mode="horizontal" selectedKeys={selectKey} onSelect={handlerMenuSelect} className={props.className}>
            <Menu.Item key="safety">
                <NavLink to={pathPerfection(RouterAboutConfig.SafetyService.BasePath)}>
                    {t('SafetyService.Title')}
                </NavLink>
            </Menu.Item>
            <Menu.Item key="generate">
                <NavLink to={pathPerfection(RouterAboutConfig.GenerateService.BasePath)}>
                    {t('GenerateService.Title')}
                </NavLink>
            </Menu.Item>
        </Menu>
    )
}

export default TopNav
