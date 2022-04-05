import {ISortDescriptor, ITableFilter, SortDirection} from "types";
import {SorterResult, TablePaginationConfig} from "antd/lib/table/interface";
import _ from "lodash";

export const ConvertInputByAntTable = <T>(pagination: TablePaginationConfig, sorter: SorterResult<T> | SorterResult<T>[]): ITableFilter => {
    const isMutlipeSort = _.isArray(sorter);
    let sorts: ISortDescriptor[] = [];
    if (isMutlipeSort) {
        sorter = sorter as SorterResult<T>[];
        sorts = _.map(sorter, function (x) {
            const multiple = 0;
            return {
                multiple: multiple,
                propertyName: x.field?.toString() ?? 'id',
                direction: x.order ? (x.order == "ascend" ? SortDirection.Asc : SortDirection.Desc) : SortDirection.Asc,
            }
        })
    } else {
        sorter = sorter as SorterResult<T>;
        sorts = [{
            multiple: 0,
            propertyName: sorter.field?.toString() ?? 'id',
            direction: sorter.order ? (sorter.order == "ascend" ? SortDirection.Asc : SortDirection.Desc) : SortDirection.Asc,
        }]
    }
    return {
        pageNumber: pagination.current ?? 1,
        pageSize: pagination.pageSize ?? 10,
        sorts: sorts,
    }
}

export const defaultInputAntTable: ITableFilter = {
    pageNumber: 1,
    pageSize: 10,
    sorts: []
}
