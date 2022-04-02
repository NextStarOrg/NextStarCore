import _ from "lodash";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import { pathPerfection } from "layout/utils/pathUtils";
import i18n from "locales/i18n";

export interface MenuItem {
    name: string;
    path: string;
    description: string;
}

export const SafetyServiceMenus: MenuItem[] = [
    {
        name: "SafetyService.Dashboard",
        path: JoinPath(
            RouterAboutConfig.SafetyService.BasePath,
            RouterAboutConfig.SafetyService.Dashboard.Path
        ),
        description: "SafetyService.Dashboard",
    },
    {
        name: "SafetyService.OneWayEncryption",
        path: JoinPath(
            RouterAboutConfig.SafetyService.BasePath,
            RouterAboutConfig.SafetyService.OneWayEncryption.Path
        ),
        description: "SafetyService.OneWayEncryption_Description",
    },
    {
        name: "SafetyService.HMAC",
        path: JoinPath(
            RouterAboutConfig.SafetyService.BasePath,
            RouterAboutConfig.SafetyService.Hmac.Path
        ),
        description: "SafetyService.HMAC_Description",
    },
];

export const GenerateServiceMenus: MenuItem[] = [
    {
        name: "GenerateService.Dashboard",
        path: JoinPath(
            RouterAboutConfig.GenerateService.BasePath,
            RouterAboutConfig.GenerateService.Dashboard.Path
        ),
        description: "GenerateService.Dashboard",
    },
    {
        name: "GenerateService.RandomString",
        path: JoinPath(
            RouterAboutConfig.GenerateService.BasePath,
            RouterAboutConfig.GenerateService.RandomString.Path
        ),
        description: "GenerateService.RandomString_Description",
    },
    {
        name: "GenerateService.UUID",
        path: JoinPath(
            RouterAboutConfig.GenerateService.BasePath,
            RouterAboutConfig.GenerateService.UUID.Path
        ),
        description: "GenerateService.UUID_Description",
    },
];

function JoinPath(...paths: string[]) {
    return pathPerfection(_.join(paths, "/"));
}
