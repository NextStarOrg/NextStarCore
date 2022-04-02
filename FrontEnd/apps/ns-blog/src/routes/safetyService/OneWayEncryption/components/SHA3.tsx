import {
    Button,
    Checkbox,
    Divider,
    Input,
    Radio,
    Space,
    Typography,
} from "antd";
import React, { useCallback, useEffect, useState } from "react";
import CryptoJS from "crypto-js";
import { FormatType } from "utils/codeFormatUtils";
import { useTranslation } from "react-i18next";

const SHA3 = () => {
    const { t } = useTranslation("SafetyService");
    const [originText, setOriginText] = useState("");
    const [cipherText, setCipherText] = useState("");
    const [autoUpdate, setAutoUpdate] = useState<boolean>(true);
    const [codeType, setCodeType] = useState<FormatType>(FormatType.Text);
    const [outputLen, setOutputLen] = useState<number>(224);
    const [outCodeType, setOutCodeType] = useState<FormatType>(FormatType.Hex);

    const handleOutput = useCallback(() => {
        textEncodeMd5(originText, outputLen);
    }, [originText, codeType]);

    useEffect(() => {
        if (autoUpdate) {
            textEncodeMd5(originText, outputLen);
        }
    }, [originText, codeType, autoUpdate, outCodeType, outputLen]);

    const textEncodeMd5 = (text: string, len: number) => {
        try {
            if (text == "") {
                setCipherText("");
            } else {
                const result = CryptoJS.SHA3(
                    codeType == FormatType.Hex
                        ? CryptoJS.enc.Hex.parse(text)
                        : text,
                    { outputLength: len }
                );
                setCipherText(
                    result.toString(
                        outCodeType == FormatType.Hex
                            ? CryptoJS.enc.Hex
                            : CryptoJS.enc.Base64
                    )
                );
            }
        } catch (err) {
            setCipherText("格式错误");
        }
    };
    return (
        <>
            <Typography.Paragraph>
                SHA-3第三代安全散列算法(Secure Hash Algorithm
                3)，之前名为Keccak（念作/ˈkɛtʃæk/或/kɛtʃɑːk/)）算法，设计者宣称在
                Intel Core 2 的CPU上面，此算法的性能是12.6比特每时钟周期（cycles
                per byte）。
            </Typography.Paragraph>
            <Typography.Paragraph>
                -- 来自
                <Typography.Link
                    href={"https://zh.wikipedia.org/wiki/SHA-3"}
                    target={"_blank"}
                    rel={"nofollow noopener noreferrer"}
                >
                    维基百科
                </Typography.Link>
            </Typography.Paragraph>
            <Divider />
            <Space direction={"vertical"} style={{ width: "100%" }}>
                <Space>
                    <Typography.Text>{t("InputType")}：</Typography.Text>
                    <Radio.Group
                        value={codeType}
                        onChange={(e) => setCodeType(e.target.value)}
                    >
                        <Radio value={FormatType.Text}>Text</Radio>
                    </Radio.Group>
                </Space>
                <Input.TextArea
                    rows={4}
                    placeholder={t("OriginalArticle")}
                    value={originText}
                    onChange={(e) => setOriginText(e.target.value)}
                    allowClear={true}
                />
            </Space>
            <Divider>
                <Space>
                    <Button onClick={handleOutput}>{t("OutputButton")}</Button>
                    <Checkbox
                        checked={autoUpdate}
                        onChange={(e) => setAutoUpdate(e.target.checked)}
                    >
                        {t("AutoUpdate")}
                    </Checkbox>
                </Space>
            </Divider>
            <br />
            <Space direction={"vertical"} style={{ width: "100%" }}>
                <Space>
                    <Typography.Text>{t("OutputLength")}：</Typography.Text>
                    <Radio.Group
                        value={outputLen}
                        onChange={(e) => {
                            setOutputLen(e.target.value);
                            if (autoUpdate) {
                                textEncodeMd5(originText, e.target.value);
                            }
                        }}
                    >
                        <Radio value={224}>224</Radio>
                        <Radio value={256}>256</Radio>
                        <Radio value={384}>384</Radio>
                        <Radio value={512}>512</Radio>
                    </Radio.Group>
                </Space>
                <Space>
                    <Typography.Text>{t("OutputType")}：</Typography.Text>
                    <Radio.Group
                        value={outCodeType}
                        onChange={(e) => setOutCodeType(e.target.value)}
                    >
                        <Radio value={FormatType.Hex}>Hex</Radio>
                        <Radio value={FormatType.Base64}>Base64</Radio>
                    </Radio.Group>
                </Space>
                <Input.TextArea
                    rows={4}
                    placeholder={t("Ciphertext")}
                    readOnly={true}
                    value={cipherText}
                />
            </Space>
        </>
    );
};

export default SHA3;
