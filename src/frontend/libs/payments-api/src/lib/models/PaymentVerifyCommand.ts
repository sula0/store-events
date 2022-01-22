/* tslint:disable */
/* eslint-disable */
/**
 * Store.Api.Payments
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface PaymentVerifyCommand
 */
export interface PaymentVerifyCommand {
    /**
     * 
     * @type {string}
     * @memberof PaymentVerifyCommand
     */
    paymentId?: string;
}

export function PaymentVerifyCommandFromJSON(json: any): PaymentVerifyCommand {
    return PaymentVerifyCommandFromJSONTyped(json, false);
}

export function PaymentVerifyCommandFromJSONTyped(json: any, ignoreDiscriminator: boolean): PaymentVerifyCommand {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'paymentId': !exists(json, 'paymentId') ? undefined : json['paymentId'],
    };
}

export function PaymentVerifyCommandToJSON(value?: PaymentVerifyCommand | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'paymentId': value.paymentId,
    };
}

