import { useSearchParams } from "react-router-dom";
import { useEffect } from "react";
import _ from "lodash";

/**
 * @param types Tab 类型名称
 * @param setType Tab 的设置 setState
 * **/
const useTabQuery = (
    types: string[],
    setType: React.Dispatch<React.SetStateAction<string>>
): [(activeKey: string) => void] => {
    const [searchParams, setSearchParams] = useSearchParams();
    const queryTabName = _.toUpper(searchParams.get("tab")?.toString());
    useEffect(() => {
        const init = function () {
            if (queryTabName != "") {
                const firstIndex = _.findIndex(
                    types,
                    (x) => _.toUpper(x) == _.toUpper(queryTabName)
                );
                setType(firstIndex == -1 ? types[0] : types[firstIndex]);
            }
        };
        init();
    }, [queryTabName]);

    const setTabActiveParams = (activeKey: string) => {
        const tempParams = new URLSearchParams();
        tempParams.append("tab", activeKey);
        setSearchParams(tempParams);
    };
    return [setTabActiveParams];
};

export default useTabQuery;
