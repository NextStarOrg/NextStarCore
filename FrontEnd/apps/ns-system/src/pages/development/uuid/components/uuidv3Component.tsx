import React, { useState } from "react";
import { Input, Space, Button, List } from "antd";

import { v3 as uuidv3 } from "uuid";

import CopyToClipboard from "components/CopyToClipboard";

const str = "tools.spiritling.pub";

export default () => {
    const [namespace, setNamespace] = useState<string>(str);
    const [uuidList, setUUIDList] = useState<string[]>(function () {
        return [uuid()];
    });
    function uuid() {
        return uuidv3(namespace, uuidv3.DNS);
    }
    function generater() {
        let arr: string[] = [];
        arr.push(uuid());
        setUUIDList(arr);
    }
    return (
        <div>
            <Space direction={"vertical"} style={{ width: "100%" }}>
                <Space>
                    <Input
                        defaultValue={namespace}
                        onChange={(value) => {
                            setNamespace(value.target.value);
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
