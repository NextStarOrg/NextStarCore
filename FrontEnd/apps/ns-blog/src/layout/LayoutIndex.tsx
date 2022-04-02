import React, {useContext} from 'react';
import {NavLink, Outlet} from 'react-router-dom';
import {Layout, Breadcrumb, Typography, Button} from 'antd';
import TopNav from "layout/components/TopNav";
import SiderMenu from "layout/components/SiderMenu";
import styles from './LayoutIndex.module.scss';
import dayjs from "dayjs";
import TopRightTool from "layout/components/TopRightTool";

const {Header, Content, Sider, Footer} = Layout;

const LayoutIndex = () => {
    return (
        <Layout className={styles.layoutWrapper}>
            <Header className={styles.header}>
                <TopNav className={styles.topNav}/>
                <TopRightTool className={styles.topRightTool}/>
            </Header>
            <Layout>
                <Sider className={styles.sider}>
                    <SiderMenu/>
                </Sider>
                <Layout className={styles.mainLayout}>
                    <Breadcrumb className={styles.breadcrumb}>
                        <Breadcrumb.Item><NavLink to={"/"}>Home</NavLink></Breadcrumb.Item>
                    </Breadcrumb>
                    <Content className={styles.mainContent}>
                        <Outlet/>
                    </Content>
                    <Footer className={styles.mainFooter}>
                        Copyright © {dayjs("2022").format("YYYY")}-{dayjs().format("YYYY")} <Typography.Link
                        href={"https://github.com/SpiritLing"} target={"_blank"}
                        rel={"nofollow noopener noreferrer"}>@SpiritLing</Typography.Link>
                    </Footer>
                </Layout>
            </Layout>
        </Layout>
    )
}

export default LayoutIndex;
