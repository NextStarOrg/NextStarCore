import React from 'react';
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import {Navigate} from "react-router-dom";

const SafetyIndex = () => {
    return (
        <Navigate to={RouterAboutConfig.SafetyService.Dashboard.Path} replace={true}/>
    )
}

export default SafetyIndex
