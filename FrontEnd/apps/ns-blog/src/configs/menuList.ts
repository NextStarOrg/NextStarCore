export type MenuItem = {
    key: string;
    title: string;
    icon?: string;
    link?: string;
    childrens: MenuItem[];
};

const menuList: MenuItem[] = [
    {
        key: "/dashboard",
        title: "首页",
        link: "",
        childrens: [],
    },
    {
        key: "/git",
        title: "Git 规范",
        link: "git",
        childrens: [],
    },
    {
        key: "/uuid",
        title: "UUID",
        link: "uuid",
        childrens: [],
    },
    {
        key: "/randomString",
        title: "随机字符串",
        link: "randomString",
        childrens: [],
    },
];

export default menuList;
