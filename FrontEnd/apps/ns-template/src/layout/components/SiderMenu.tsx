import React, {useCallback, useEffect, useState} from "react";
import {Menu} from "antd";
import {NavLink, useLocation} from "react-router-dom";
import {getService} from "layout/utils/pathUtils";
import {
    BlogManageMenus
} from "assets/consts/MenuAboutName";

const SiderMenu = () => {
    const [selectKey, setSelectKey] = useState<string[]>([""]);

    const handlerMenuSelect = useCallback(
        ({item, key, keyPath, selectedKeys, domEvent}) => {
            setSelectKey(selectedKeys);
        },
        []
    );
    return (
        <Menu
            mode='inline'
            theme={"light"}
            style={{height: "100%", borderRight: 0}}
            selectedKeys={selectKey}
            onSelect={handlerMenuSelect}
        >
            {BlogManageMenus.map((x) => {
                return (
                    <Menu.Item key={x.id}>
                        <NavLink to={x.url}>{x.name}</NavLink>
                    </Menu.Item>
                );
            })}
        </Menu>
    );
};

export default SiderMenu;
