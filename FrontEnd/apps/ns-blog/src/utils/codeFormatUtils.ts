import CryptoJS from "crypto-js";

export const Base64ToHex = (str:string) => {
    return (CryptoJS.enc.Base64.parse(str)).toString(CryptoJS.enc.Hex);
}

export const HexToBase64 = (str:string) => {
    return (CryptoJS.enc.Hex.parse(str)).toString(CryptoJS.enc.Base64);
}


export enum FormatType {
    Text,
    Hex,
    Base64
}
