import React from "react";
import { Tooltip } from "antd";

import styles from "./index.module.scss";

import { ChromePicker, ColorResult } from "react-color";

export type Props = {
    color: string;
    isShow?: boolean;
    onChange?: (
        color: ColorResult,
        event: React.ChangeEvent<HTMLInputElement>
    ) => void;
};

const Index = (props: Props) => {
    let show: boolean;
    if (props.isShow === undefined || props.isShow === true) {
        show = true;
    } else {
        show = false;
    }
    return (
        <div>
            <Tooltip
                title={
                    <ChromePicker
                        className={styles.colorPicker}
                        disableAlpha={false}
                        color={props.color}
                        onChange={show ? undefined : props.onChange}
                    />
                }
                trigger={"click"}
                color={props.color}>
                <div className={styles.showBlock}>
                    <div
                        className={styles.color}
                        style={{ backgroundColor: props.color }}></div>
                </div>
            </Tooltip>
        </div>
    );
};

export default Index;
