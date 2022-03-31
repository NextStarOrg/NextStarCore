import React, { useState } from "react";
import { InputNumber, Space, Button, List, Tooltip } from "antd";
import { QuestionCircleOutlined } from "@ant-design/icons";

import cryptoRandomString from "crypto-random-string";

import CopyToClipboard from "components/CopyToClipboard";

export default () => {
    const [count, setCount] = useState(5);
    const [length, setLength] = useState(16);
    const [uuidList, setUUIDList] = useState<string[]>(listFun());
    function listFun() {
        let arr: string[] = [];
        for (let i = 0; i < count; i++) {
            arr.push(cryptoRandomString({ length, type: "hex" }));
        }
        return arr;
    }
    function generater() {
        setUUIDList(listFun());
    }
    return (
        <div>
            <Space direction={"vertical"} style={{ width: "100%" }}>
                <Space>
                    <InputNumber
                        defaultValue={count}
                        onChange={(value) => {
                            typeof value === "number"
                                ? setCount(value)
                                : setCount(5);
                        }}
                    />
                    <Tooltip title={"生成数量"}>
                        <QuestionCircleOutlined />
                    </Tooltip>
                    <InputNumber
                        defaultValue={length}
                        onChange={(value) => {
                            typeof value === "number"
                                ? setLength(value)
                                : setLength(16);
                        }}
                    />
                    <Tooltip title={"生成长度"}>
                        <QuestionCircleOutlined />
                    </Tooltip>
                    <Button onClick={generater}>生成</Button>
                </Space>
                <List
                    bordered
                    dataSource={uuidList}
                    renderItem={(item) => (
                        <List.Item>
                            <CopyToClipboard value={item} />
                        </List.Item>
                    )}
                />
            </Space>
        </div>
    );
};
