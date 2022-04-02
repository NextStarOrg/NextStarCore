import React from 'react';
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import {Navigate} from "react-router-dom";

const GenerateIndex = () => {
    return (
        <Navigate to={RouterAboutConfig.GenerateService.Dashboard.Path} replace={true}/>
    )
}

export default GenerateIndex
