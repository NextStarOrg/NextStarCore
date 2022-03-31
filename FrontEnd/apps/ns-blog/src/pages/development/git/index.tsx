import React from "react";
import { Table, Tag, Alert } from "antd";
import { ColumnsType } from "antd/es/table";

import { labelData, labelItem } from "./index.module";

import CopyComponent from "components/CopyToClipboard";
import ChromePickerColor from "components/ChromePickerColor";

const columns: ColumnsType<labelItem> = [
    {
        title: "图标",
        dataIndex: "icon",
        key: "icon",
        render: (value: string, record: labelItem, index: number) => {
            return <CopyComponent value={value} />;
        },
    },
    {
        title: "英文",
        dataIndex: "value",
        key: "value",
        render: (value: string, record: labelItem, index: number) => {
            return <CopyComponent value={value} />;
        },
    },
    {
        title: "字符",
        dataIndex: "icon-chart",
        key: "icon-chart",
        render: (value: string, record: labelItem, index: number) => {
            return <CopyComponent value={`${record.chart}`} />;
        },
    },
    {
        title: "颜色",
        dataIndex: "color",
        key: "color",
        render: (value: string, record: labelItem, index: number) => {
            return <CopyComponent value={value} />;
        },
    },
    {
        title: "合并",
        dataIndex: "icon-value",
        key: "icon-value",
        render: (value: string, record: labelItem, index: number) => {
            return <CopyComponent value={`${record.icon} ${record.value}`} />;
        },
    },
    {
        title: "字符合并",
        dataIndex: "chart-value",
        key: "chart-value",
        render: (value: string, record: labelItem, index: number) => {
            return <CopyComponent value={`${record.chart} ${record.value}`} />;
        },
    },
    // {
    //     title: "coding是否支持",
    //     dataIndex: "icon-coding-comment",
    //     key: "icon-coding-comment",
    //     render: (value: string, record: labelItem, index: number) => {
    //         return <span>{record.coding.comment && "✅"}</span>;
    //     },
    // },
    {
        title: "说明",
        dataIndex: "description",
        key: "description",
        render: (value: string, record: labelItem, index: number) => {
            return <CopyComponent value={value} />;
        },
    },
    {
        title: "颜色组件",
        dataIndex: "color-component",
        key: "color-component",
        render: (value: string, record: labelItem, index: number) => {
            return <ChromePickerColor color={record.color} />;
        },
    },
    {
        title: "组合效果",
        dataIndex: "color-value-icon",
        key: "color-value-icon",
        render: (value: string, record: labelItem, index: number) => {
            return (
                <Tag
                    color={
                        record.color
                    }>{`${record.icon} ${record.value}`}</Tag>
            );
        },
    },
];

const Index = () => {
    return (
        <div>
            <Alert message='目前github评论支持所有的字符' type='info' />
            <br />
            <Alert
                message='coding 不支持 test :test_tube: 🧪 docs :receipt: 🧾 chore :microbe: 🦠 package :package: 📦 security :shield: 🛡️ '
                type='warning'
            />
            <br />
            <Alert
                message='bitbucket 不支持 test :test_tube: 🧪 docs :receipt: 🧾 chore :microbe: 🦠 package :package: 📦 '
                type='warning'
            />
            <br />
            <Table
                columns={columns}
                dataSource={labelData}
                bordered={true}
                pagination={false}
            />
        </div>
    );
};

export default Index;
