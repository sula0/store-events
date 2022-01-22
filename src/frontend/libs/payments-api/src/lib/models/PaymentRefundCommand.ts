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
 * @interface PaymentRefundCommand
 */
export interface PaymentRefundCommand {
    /**
     * 
     * @type {string}
     * @memberof PaymentRefundCommand
     */
    paymentId?: string;
    /**
     * 
     * @type {string}
     * @memberof PaymentRefundCommand
     */
    note?: string | null;
}

export function PaymentRefundCommandFromJSON(json: any): PaymentRefundCommand {
    return PaymentRefundCommandFromJSONTyped(json, false);
}

export function PaymentRefundCommandFromJSONTyped(json: any, ignoreDiscriminator: boolean): PaymentRefundCommand {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'paymentId': !exists(json, 'paymentId') ? undefined : json['paymentId'],
        'note': !exists(json, 'note') ? undefined : json['note'],
    };
}

export function PaymentRefundCommandToJSON(value?: PaymentRefundCommand | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'paymentId': value.paymentId,
        'note': value.note,
    };
}

