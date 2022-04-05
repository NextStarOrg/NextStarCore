import {Table} from "antd";
import {ColumnsType} from "antd/es/table";
import {SorterResult, TablePaginationConfig} from "antd/lib/table/interface";
import _ from "lodash";
import {useState} from "react";
import {ConvertInputByAntTable, defaultInputAntTable} from "utils/antTableUtils";
import {ITableFilter} from "types";

interface CategoryItem {
    id: number,
    name: string,
    description: Nullable<string>,
    url?: Nullable<string>
}

const dataSource: CategoryItem[] = [
    {
        id: 1,
        name: "C#",
        description: null,
        url: null
    }, {
        id: 2,
        name: "JS",
        description: null,
        url: null
    }, {
        id: 3,
        name: "CSS",
        description: null,
        url: null
    }, {
        id: 4,
        name: "CSS",
        description: null,
        url: null
    }, {
        id: 5,
        name: "CSS",
        description: null,
        url: null
    }, {
        id: 6,
        name: "CSS",
        description: null,
        url: null
    }, {
        id: 7,
        name: "CSS",
        description: null,
        url: null
    }, {
        id: 8,
        name: "CSS",
        description: null,
        url: null
    }, {
        id: 9,
        name: "CSS",
        description: null,
        url: null
    }, {
        id: 10,
        name: "CSS",
        description: null,
        url: null
    }, {
        id: 11,
        name: "CSS",
        description: null,
        url: null
    }, {
        id: 12,
        name: "CSS",
        description: null,
        url: null
    }
];

const columns: ColumnsType<CategoryItem> = [
    {
        title: 'Id',
        dataIndex: 'id',
        key: 'id',
        sorter: {
            multiple: 1
        },
    },
    {
        title: '名称',
        dataIndex: 'name',
        key: 'name',
        sorter: {
            multiple: 2
        },
    },
    {
        title: '自定义访问地址',
        dataIndex: 'url',
        key: 'url',
    },
    {
        title: '描述',
        dataIndex: 'description',
        key: 'description',
    }
];

const Category = () => {
    const [tableFilter, setTableFilter] = useState<ITableFilter>(defaultInputAntTable)

    function onChange(pagination: TablePaginationConfig, filters: any, sorter: SorterResult<CategoryItem> | SorterResult<CategoryItem>[]) {
        setTableFilter(ConvertInputByAntTable(pagination, sorter))
    }

    const handleTableDoubleClick = function (item?: CategoryItem) {
        console.log(item);
    }

    return (
        <section>
            <Table dataSource={dataSource} columns={columns} bordered={true}
                   onRow={(record) => {
                       return {
                           onDoubleClick: () => handleTableDoubleClick(record)
                       }
                   }}
                   pagination={{
                       position: ["bottomCenter"],
                       pageSize: tableFilter.pageSize,
                       current: tableFilter.pageNumber
                   }} onChange={onChange} showSorterTooltip={true}/>
        </section>
    )
}

export default Category;
