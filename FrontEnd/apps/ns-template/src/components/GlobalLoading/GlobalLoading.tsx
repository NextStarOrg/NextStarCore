import React from "react";
import styles from "./globalLoading.module.scss";
import loadingImag from "assets/images/loading.gif";

export interface IGlobalLoadingProps {
    message?: string;
    isLoading?: boolean;
}

const GlobalLoading = (props: IGlobalLoadingProps) => {
    const message = props.message ?? "加载中...";
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
