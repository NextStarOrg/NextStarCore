import React from "react";

import styles from "./sub.module.scss";

const Header = () => {
    return (
        <header className={styles.header}>
            <img
                className={styles.headerImage}
                src='https://static.cdn.spiritling.pub/images-PZagGrHhrXppbn4S/favicon/2020-06-01/mstile-70x70.png'
                alt='图片'
            />
            <span>SpiritLing</span>
        </header>
    );
};

export default Header;
