import React from "react";
import styles from "components/SpinLoading/spinLoading.module.scss";
import loadingImag from "assets/images/loading.gif";
import { useSelector } from "react-redux";
import { selectLoading } from "routes/commonService/rtk/selector";
import { useTranslation } from "react-i18next";

const SpinLoading = () => {
    const { t } = useTranslation();
    const loading = useSelector(selectLoading);
    const message = t(loading.message);
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
                        <span className={styles.text}>{message}</span>
                    </div>
                </div>
            )}
        </>
    );
};

export default SpinLoading;
