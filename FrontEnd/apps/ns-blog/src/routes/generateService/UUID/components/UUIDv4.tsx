import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { Button, Input, InputNumber, List, Space, Typography } from "antd";
import _ from "lodash";
import * as uuid from "uuid";

const UUIDv4 = () => {
    const { t } = useTranslation("GenerateService");
    const [count, setCount] = useState(10);
    const [strList, setStrList] = useState<string[]>([]);

    const generateCore = (): string[] => {
        const arr: string[] = [];
        for (let i = 0; i < count; i++) {
            arr.push(uuid.v4());
        }
        return arr;
    };

    const handleGenerate = () => {
        setStrList(generateCore());
    };
    return (
        <>
            <Space direction={"vertical"}>
                <Typography.Paragraph>
                    {t("UUID.v4.Description")}
                </Typography.Paragraph>
                <Input.Group compact>
                    <InputNumber
                        addonBefore={t("GeneratedNumber")}
                        defaultValue={count}
                        onChange={(v) =>
                            _.isNumber(v) ? setCount(v) : setCount(count)
                        }
                    />
                    <Button type='primary' onClick={handleGenerate}>
                        {t("GenerateButton")}
                    </Button>
                </Input.Group>
                <List
                    bordered
                    dataSource={strList}
                    style={{ overflow: "auth" }}
                    renderItem={(item) => (
                        <List.Item>
                            <Typography.Paragraph copyable>
                                {item}
                            </Typography.Paragraph>
                        </List.Item>
                    )}
                />
            </Space>
        </>
    );
};

export default UUIDv4;
