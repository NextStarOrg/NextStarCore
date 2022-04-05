import { useLocation, useSearchParams } from "react-router-dom";
import { useEffect, useState } from "react";
import _ from "lodash";
import nsStorage from "utils/storage";

/**
 * @param paramKey query 键名
 * @param initParamValue 初始值
 * **/
const useQuery = (
    paramKey: string,
    initParamValue: any,
    initIsRemember?: boolean
): [string, (activeKey: string, isRemember: boolean) => void] => {
    const location = useLocation();
    const [paramValue, setParamValue] = useState(initParamValue);
    const [searchParams, setSearchParams] = useSearchParams();
    const [isRemember, setIsRemember] = useState(initIsRemember ?? true);
    const currentParamValue = searchParams.get(paramKey)?.toString() ?? "";
    useEffect(() => {
        const init = function () {
            setParams(initParamValue, true);
        };
        init();
    }, [currentParamValue]);

    const setParams = (paramValue: string, isRemember: boolean) => {
        setParamValue(paramValue);
        const prefix = location.pathname.replace("/", "_");
        if (isRemember) {
            nsStorage.set(prefix + paramValue, paramValue);
        } else {
            nsStorage.remove(prefix + paramValue);
        }
        const queryCharacters = searchParams.get(paramKey)?.toString();
        if (queryCharacters != undefined) {
            searchParams.set(paramKey, paramValue);
        } else {
            searchParams.append(paramKey, paramValue);
        }
        setSearchParams(searchParams);
    };

    return [currentParamValue, setParams];
};

export default useQuery;
