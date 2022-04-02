import React, { useCallback, useState } from "react";
import { useTranslation } from "react-i18next";
import {
    Button,
    Checkbox,
    Input,
    InputNumber,
    List,
    Space,
    Typography,
    message,
} from "antd";
import * as uuid from "uuid";

const DEFAULT_NAMESPACE = "8df52086-1a8f-43b0-9c68-6a284f02d7f9";
const DEFAULT_NAME = "nextstar";

const UUIDv3 = () => {
    const { t } = useTranslation("GenerateService");
    const [name, setName] = useState(DEFAULT_NAME);
    const [namespace, setNamespace] = useState(DEFAULT_NAMESPACE);
    const [strList, setStrList] = useState<string[]>([]);

    const generateCore = (): string[] => {
        const arr: string[] = [];
        if (uuid.validate(namespace)) {
            for (let i = 0; i < 1; i++) {
                arr.push(uuid.v3(name, namespace));
            }
        } else {
            message.error(t("UUID.NamespaceError"));
        }
        return arr;
    };

    const handleGenerate = useCallback(() => {
        setStrList(generateCore());
    }, [namespace, name]);
    return (
        <>
            <Space direction={"vertical"}>
                <Typography.Paragraph>
                    {t("UUID.v3.Description")}
                </Typography.Paragraph>
                <Space style={{ width: "100%" }}>
                    <Input
                        addonBefore={t("UUID.Name")}
                        style={{ width: "320px" }}
                        defaultValue={name}
                        value={name}
                        allowClear
                        onChange={(e) => setName(e.target.value)}
                    />
                </Space>
                <Space style={{ width: "100%" }}>
                    <Input
                        addonBefore={t("UUID.Namespace")}
                        style={{ width: "440px" }}
                        defaultValue={namespace}
                        value={namespace}
                        allowClear
                        onChange={(e) => setNamespace(e.target.value)}
                    />
                </Space>
                <Input.Group compact>
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

export default UUIDv3;
