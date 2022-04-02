import React, { useCallback, useEffect, useState } from "react";
import { Menu } from "antd";
import { NavLink, useLocation } from "react-router-dom";
import { getService } from "layout/utils/pathUtils";
import {
    GenerateServiceMenus,
    MenuItem,
    SafetyServiceMenus,
} from "assets/consts/MenuAboutName";
import { useSelector } from "react-redux";
import { selectLanguage } from "routes/commonService/rtk/selector";
import { useTranslation } from "react-i18next";

const SiderMenu = () => {
    const location = useLocation();
    const language = useSelector(selectLanguage);
    const { t } = useTranslation();
    const [selectKey, setSelectKey] = useState<string[]>([""]);
    const [menus, setMenus] = useState<MenuItem[]>([]);
    useEffect(() => {
        const init = function () {
            setSelectKey([location.pathname]);
            const serviceName = getService(location.pathname);
            switch (serviceName.toLocaleLowerCase()) {
                case "safety":
                    setMenus(SafetyServiceMenus);
                    break;
                case "generate":
                    setMenus(GenerateServiceMenus);
                    break;
                case "common":
                    // 进入到共通中后，菜单列表不在变动
                    break;
                default:
                    setMenus([]);
                    break;
            }
        };
        init();
    }, [location.pathname, language]);

    const handlerMenuSelect = useCallback(
        ({ item, key, keyPath, selectedKeys, domEvent }) => {
            setSelectKey(selectedKeys);
        },
        []
    );
    return (
        <Menu
            mode='inline'
            theme={"light"}
            style={{ height: "100%", borderRight: 0 }}
            selectedKeys={selectKey}
        >
            {menus.map((x) => {
                return (
                    <Menu.Item key={x.path}>
                        <NavLink to={x.path}>{t(`${x.name}`)}</NavLink>
                    </Menu.Item>
                );
            })}
        </Menu>
    );
};

export default SiderMenu;
