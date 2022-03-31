import React from "react";
import { Spin } from "antd";
import styles from "./index.module.scss";

export default () => {
    return (
        <React.Fragment>
            <Spin className={styles.suspenseLoading} />
        </React.Fragment>
    );
};
