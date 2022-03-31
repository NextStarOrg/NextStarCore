import React from "react";
import { Space, Button, message } from "antd";
import { CopyOutlined } from "@ant-design/icons";
import CopyToClipboard from "react-copy-to-clipboard";

import styles from "./index.module.scss";

export type Props = {
    value: string;
};

const Index = (props: Props) => {
    const value = props.value;
    function copy(text: String, bol: boolean) {
        if (bol) {
            message.success(`${text} copy finish`);
        } else {
            message.success(`${text} copy failed`);
        }
    }

    return (
        <Space>
            <span>{value}</span>
            <CopyToClipboard text={value} onCopy={copy}>
                <Button type={"link"}>
                    <CopyOutlined className={styles.copyIcon} />
                </Button>
            </CopyToClipboard>
        </Space>
    );
};

export default Index;
