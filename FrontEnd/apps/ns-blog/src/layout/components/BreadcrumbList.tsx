import styles from "layout/LayoutIndex.module.scss";
import {Breadcrumb} from "antd";
import {Link, NavLink, useLocation} from "react-router-dom";
import React, {useCallback} from "react";
import {BlogManageMenus} from "assets/consts/MenuAboutName";
import _ from "lodash";

const BreadcrumbList = () => {
    const location = useLocation();
    const pathSnippets = location.pathname.split('/').filter(i => i);
    const extraBreadcrumbItems = pathSnippets.map((pathStr, index) => {
        const url = `/${pathSnippets.slice(0, index + 1).join('/')}`;
        const result = _.find(BlogManageMenus, function (item) {
            return item.url == url
        });
        if (result != undefined && result.id != 30000000) {
            return (
                <Breadcrumb.Item key={result.id}>
                    <Link to={result.url}>{result.name}</Link>
                </Breadcrumb.Item>
            );
        }
    });

    const buildBreadcrumbItems = useCallback(() => {
        const breadcrumbItems = [
            <Breadcrumb.Item key="30000000">
                <Link to="/">Dashboard</Link>
            </Breadcrumb.Item>,
        ];
        const result = extraBreadcrumbItems;
        const elms = result.filter(x => x != undefined) as (JSX.Element)[];
        return breadcrumbItems.concat(elms);

    }, [location]);
    return (
        <Breadcrumb className={styles.breadcrumb}>
            {buildBreadcrumbItems()}
        </Breadcrumb>
    )
}

export default BreadcrumbList;
