import React from "react";
import styles from "./globalLoading.module.scss";
import loadingImag from "assets/images/loading.gif";
import { useTranslation } from "react-i18next";

export interface IGlobalLoadingProps {
    message?: string;
    isLoading?: boolean;
}

const GlobalLoading = (props: IGlobalLoadingProps) => {
    const { t } = useTranslation();
    const message = props.message ?? t("LoadingText");
    const isLoading = props.isLoading ?? true;
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

export default GlobalLoading;
