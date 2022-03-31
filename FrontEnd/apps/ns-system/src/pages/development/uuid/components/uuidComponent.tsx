import React, { useState } from "react";
import { InputNumber, Space, Button, List } from "antd";

import { v1 as uuidv1, v3 as uuidv3, v4 as uuidv4, v5 as uuidv5 } from "uuid";

import CopyToClipboard from "components/CopyToClipboard";

import { UUIDType } from "../index.module";

export default (props: { type: UUIDType }) => {
    const [count, setCount] = useState(5);
    const [uuidList, setUUIDList] = useState<string[]>([]);
    function getUUID() {
        switch (props.type) {
            case UUIDType.v1:
                return uuidv1();
            case UUIDType.v3:
                return uuidv3("tools.spiritling.pub", uuidv3.DNS);
            case UUIDType.v4:
                return uuidv4();
            case UUIDType.v5:
                return uuidv5("tools.spiritling.pub", uuidv5.DNS);
            default:
                return uuidv4();
        }
    }
    function generater() {
        let arr: string[] = [];
        for (let i = 0; i < count; i++) {
            arr.push(getUUID());
        }
        setUUIDList(arr);
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
