import React from 'react';
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import {Navigate} from "react-router-dom";

const CommonIndex = () => {
    return (
        <Navigate to={RouterAboutConfig.SafetyService.BasePath} replace={true}/>
    )
}

export default CommonIndex
