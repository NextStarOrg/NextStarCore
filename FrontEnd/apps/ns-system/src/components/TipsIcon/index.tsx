import React from "react";
import { Tooltip } from "antd";
import { QuestionCircleOutlined } from "@ant-design/icons";

interface Props {
    tips: string;
}

export default (props: Props) => {
    return (
        <Tooltip title={props.tips}>
            <QuestionCircleOutlined />
        </Tooltip>
    );
};
