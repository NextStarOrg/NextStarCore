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

const MD5 = () => {
    const { t } = useTranslation("SafetyService");
    const [originText, setOriginText] = useState("");
    const [cipherText, setCipherText] = useState("");
    const [autoUpdate, setAutoUpdate] = useState<boolean>(true);
    const [codeType, setCodeType] = useState<FormatType>(FormatType.Text);
    const [outCodeType, setOutCodeType] = useState<FormatType>(FormatType.Hex);

    const handleOutput = useCallback(() => {
        textEncodeMd5(originText);
    }, [originText, codeType, outCodeType]);

    useEffect(() => {
        if (autoUpdate) {
            textEncodeMd5(originText);
        }
    }, [originText, codeType, autoUpdate, outCodeType]);

    const textEncodeMd5 = (text: string) => {
        try {
            if (text == "") {
                setCipherText("");
            } else {
                const result = CryptoJS.MD5(
                    codeType == FormatType.Hex
                        ? CryptoJS.enc.Hex.parse(text)
                        : text
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
                MD5消息摘要算法（英语：MD5 Message-Digest
                Algorithm），一种被广泛使用的密码散列函数，可以产生出一个128位（16个字符(BYTES)）的散列值（hash
                value），用于确保信息传输完整一致。MD5由美国密码学家罗纳德·李维斯特（Ronald
                Linn
                Rivest）设计，于1992年公开，用以取代MD4算法。这套算法的程序在
                RFC 1321 中被加以规范。
            </Typography.Paragraph>
            <Typography.Paragraph>
                将数据（如一段文字）运算变为另一固定长度值，是散列算法的基础原理。
            </Typography.Paragraph>
            <Typography.Paragraph>
                1996年后被证实存在弱点，可以被加以破解，对于需要高度安全性的资料，专家一般建议改用其他算法，如SHA-2。2004年，证实MD5算法无法防止碰撞攻击，因此不适用于安全性认证，如SSL公开密钥认证或是数字签名等用途。
            </Typography.Paragraph>
            <Typography.Paragraph>
                -- 来自
                <Typography.Link
                    href={"https://zh.wikipedia.org/wiki/MD5"}
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
            <Space direction={"vertical"} style={{ width: "100%" }}>
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

export default MD5;
