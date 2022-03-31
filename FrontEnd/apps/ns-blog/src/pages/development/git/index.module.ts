export interface labelItem {
    icon: string;
    chart: string;
    value: string;
    color: string;
    description: string;
    coding: supportCoding;
}

export interface supportCoding {
    comment: boolean;
}

export const labelData: labelItem[] = [
    {
        icon: "🎉",
        chart: ":tada:",
        value: "init",
        color: "#52c41a",
        description: "初始化",
        coding: {
            comment: true,
        },
    },
    {
        icon: "⚡️",
        chart: ":zap:",
        value: "optimize",
        color: "#ffec3d",
        description: "项目优化",
        coding: {
            comment: true,
        },
    },
    {
        icon: "⚡️",
        chart: ":zap:",
        value: "update",
        color: "#ffec3d",
        description: "项目更新",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🔨",
        chart: ":hammer:",
        value: "refactor",
        color: "#fadb14",
        description: "项目重写",
        coding: {
            comment: true,
        },
    },
    {
        icon: "✨",
        chart: ":sparkles:",
        value: "research",
        color: "#d4b106",
        description: "研究",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🆕",
        chart: ":new:",
        value: "feat",
        color: "#40a9ff",
        description: "项目feature",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🧪",
        chart: ":test_tube:",
        value: "test",
        color: "#1890ff",
        description: "项目测试",
        coding: {
            comment: false,
        },
    },
    {
        icon: "🧾",
        chart: ":receipt:",
        value: "docs",
        color: "#096dd9",
        description: "文档",
        coding: {
            comment: false,
        },
    },
    {
        icon: "🌐",
        chart: ":globe_with_meridians:",
        value: "i18n",
        color: "#597ef7",
        description: "项目全球化",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🦠",
        chart: ":microbe:",
        value: "chore",
        color: "#2f54eb",
        description: "项目杂项",
        coding: {
            comment: false,
        },
    },
    {
        icon: "📦",
        chart: ":package:",
        value: "package",
        color: "#5cdbd3",
        description: "包修改或管理",
        coding: {
            comment: false,
        },
    },
    {
        icon: "🐳",
        chart: ":whale:",
        value: "docker",
        color: "#36cfc9",
        description: "docker",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🔧",
        chart: ":wrench:",
        value: "config",
        color: "#13c2c2",
        description: "配置文件",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🚚",
        chart: ":truck:",
        value: "mv",
        color: "#08979c",
        description: "移动文件",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🔖",
        chart: ":bookmark:",
        value: "tag",
        color: "#9254de",
        description: "标签或版本",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🔀",
        chart: ":twisted_rightwards_arrows:",
        value: "merge",
        color: "#722ed1",
        description: "分支合并",
        coding: {
            comment: true,
        },
    },
    {
        icon: "⏪",
        chart: ":rewind:",
        value: "reset",
        color: "#531dab",
        description: "回滚",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🐛",
        chart: ":bug:",
        value: "fix",
        color: "#f5222d",
        description: "项目bug修复",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🐞",
        chart: ":beetle:",
        value: "hotfix",
        color: "#cf1322",
        description: "项目紧急bug修复",
        coding: {
            comment: true,
        },
    },
    {
        icon: "🛡️",
        chart: ":shield:",
        value: "security",
        color: "#cf1322",
        description: "安全问题",
        coding: {
            comment: false,
        },
    },
    {
        icon: "🚀",
        chart: ":rocket:",
        value: "important",
        color: "#a8071a",
        description: "重要",
        coding: {
            comment: true,
        },
    },
];
/**
:tada:
:zap:
:hammer:
:sparkles:
:new:
:test_tube:
:receipt:
:globe_with_meridians:
:microbe:
:package:
:whale:
:wrench:
:truck:
:bookmark:
:twisted_rightwards_arrows:
:rewind:
:bug:
:beetle:
:shield:
:rocket:
 */
