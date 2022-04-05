import React, {useContext, useEffect} from 'react';
import {NavLink, Outlet} from 'react-router-dom';
import {Layout, Breadcrumb, Typography, Button} from 'antd';
import SiderMenu from "layout/components/SiderMenu";
import styles from './LayoutIndex.module.scss';
import dayjs from "dayjs";
import TopRightTool from "layout/components/TopRightTool";
import {useDispatch} from "react-redux";
import {setLoadingStatus} from "routes/commonService/rtk/loading";
import BreadcrumbList from "layout/components/BreadcrumbList";

const {Header, Content, Sider, Footer} = Layout;

const LayoutIndex = () => {
    const dispatch = useDispatch();
    useEffect(()=>{
        dispatch(setLoadingStatus(false));
    },[])
    return (
        <Layout className={styles.layoutWrapper}>
            <Header className={styles.header}>
                <div></div>
                <TopRightTool className={styles.topRightTool}/>
            </Header>
            <Layout>
                <Sider className={styles.sider}>
                    <SiderMenu/>
                </Sider>
                <Layout className={styles.mainLayout}>
                    <BreadcrumbList />
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
