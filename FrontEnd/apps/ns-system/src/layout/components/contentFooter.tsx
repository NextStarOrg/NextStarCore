import React from "react";
import { Space } from "antd";

import styles from "./sub.module.scss";

const ContentFooter = () => {
    return (
        <header className={styles.contentFooter}>
            <Space className={styles.footerSpace}>
                <img
                    src='https://static.cdn.spiritling.pub/images-PZagGrHhrXppbn4S/favicon/2020-06-01/favicon-32x32.png'
                    alt='footerIcon'
                />
                <a href='https://github.com/SpiritLing'>@SpiritLing</a>
            </Space>
        </header>
    );
};

export default ContentFooter;
