import React from "react";
import styles from "components/SpinLoading/spinLoading.module.scss";
import loadingImag from "assets/images/loading.gif";
import { useSelector } from "react-redux";
import { selectLoading } from "routes/commonService/rtk/selector";

const SpinLoading = () => {
    const loading = useSelector(selectLoading);
    const isLoading = loading.isLoading;

    return (
        <>
            {isLoading && (
                <div className={styles.loadingWrapper}>
                    <div className={styles.loadingContainer}>
                        <img
                            src={loadingImag}
                            className={styles.image}
                            alt={"loading gif"}
                        />
                        <br />
                        <span className={styles.text}>加载中...</span>
                    </div>
                </div>
            )}
        </>
    );
};

export default SpinLoading;
