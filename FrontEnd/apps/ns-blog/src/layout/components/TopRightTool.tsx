import React, { useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Avatar, Button, Dropdown, Menu, Space, Modal } from "antd";
import { useLocation, useNavigate } from "react-router-dom";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import nsStorage from "utils/storage";
import { PrevAuthUrl } from "assets/consts/StoreCacheName";
import { useTranslation } from "react-i18next";
import {
    createFromIconfontCN,
    GithubOutlined,
    DownOutlined,
    LogoutOutlined,
    QuestionCircleOutlined,
} from "@ant-design/icons";
import { redirectToLogout } from "utils/auth-utils";
import { setLoadingStatus } from "routes/commonService/rtk/loading";
import {selectUserState} from "routes/commonService/rtk/selector";

const { confirm } = Modal;
const IconFont = createFromIconfontCN({
    scriptUrl: window._ICON_FONT_URL,
});

const TopRightTool = (props: { className?: string | undefined }) => {
    const user = useSelector(selectUserState);
    const dispatch = useDispatch();

    return (
        <Space>
            <Button size={"small"}>
                <a
                    target='_blank'
                    rel='noopener noreferrer'
                    href='https://github.com/NextStarOrg'
                >
                    <GithubOutlined /> Github
                </a>
            </Button>
            <Button size={"small"}>
                <a
                    target='_blank'
                    rel='noopener noreferrer'
                    href='https://youtrack.nextstar.org.cn'
                >
                    <IconFont type={"ns-youtrack"} /> YouTrack
                </a>
            </Button>
            {user.user == null || user.user?.expired ? (
                <NoExistLogin />
            ) : (
                <ExistLogin />
            )}
        </Space>
    );
};

const NoExistLogin = () => {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const location = useLocation();
    return (
        <Button
            size={"small"}
            onClick={() => {
                nsStorage(PrevAuthUrl, location.pathname);
                navigate(RouterAboutConfig.Login.Path);
            }}
        >
            {t("Login")}
        </Button>
    );
};

const ExistLogin = () => {
    const user = useSelector(selectUserState);
    const dispatch = useDispatch();
    const logoutConfirm = function () {
        confirm({
            title: "是否退出？",
            icon: <QuestionCircleOutlined color={"#fa8c16"} />,
            onOk() {
                dispatch(setLoadingStatus(true));
                redirectToLogout();
            },
        });
    };

    const menu = (
        <Menu>
            <Menu.ItemGroup
                title={`Signed in as ${user.user?.profile.nickname}`}
            />
            <Menu.Item onClick={logoutConfirm} icon={<LogoutOutlined />}>
                退出
            </Menu.Item>
        </Menu>
    );
    return (
        <>
            <Dropdown
                overlay={menu}
                placement='bottomRight'
                trigger={["click"]}
                arrow={true}
            >
                <a
                    className='ant-dropdown-link'
                    onClick={(e) => e.preventDefault()}
                >
                    <Avatar
                        src={user.user?.profile.picture}
                        style={{ marginRight: "4px" }}
                    />
                    <DownOutlined />
                </a>
            </Dropdown>
        </>
    );
};

export default TopRightTool;
