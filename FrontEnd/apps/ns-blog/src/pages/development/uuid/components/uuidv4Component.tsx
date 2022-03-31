import React, { useState } from "react";
import { InputNumber, Space, Button, List } from "antd";

import { v4 as uuidv4 } from "uuid";

import CopyToClipboard from "components/CopyToClipboard";

export default () => {
    const [count, setCount] = useState(5);
    const [uuidList, setUUIDList] = useState<string[]>(uuidListFun());
    function uuidListFun() {
        let arr: string[] = [];
        for (let i = 0; i < count; i++) {
            arr.push(uuidv4());
        }
        return arr;
    }
    function generater() {
        setUUIDList(uuidListFun());
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
