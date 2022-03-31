import React, { useState } from "react";
import { Statistic, Space, Button, List } from "antd";

import { v1 as uuidv1 } from "uuid";

import CopyToClipboard from "components/CopyToClipboard";

export default () => {
    const [timestamp, setTimestamp] = useState<number>(new Date().getTime());
    const [uuidList, setUUIDList] = useState<string[]>([uuidv1()]);
    function generater() {
        setUUIDList([uuidv1()]);
        setTimestamp(new Date().getTime());
    }
    return (
        <div>
            <Space direction={"vertical"} style={{ width: "100%" }}>
                <Space size={"large"}>
                    <Statistic title='当前毫秒值' value={timestamp} />
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
