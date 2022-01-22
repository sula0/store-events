/* tslint:disable */
/* eslint-disable */
/**
 * Store.Api.Shopping
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
import {
    CartEntry,
    CartEntryFromJSON,
    CartEntryFromJSONTyped,
    CartEntryToJSON,
} from './CartEntry';

/**
 * 
 * @export
 * @interface Cart
 */
export interface Cart {
    /**
     * 
     * @type {{ [key: string]: CartEntry; }}
     * @memberof Cart
     */
    entries?: { [key: string]: CartEntry; } | null;
    /**
     * 
     * @type {number}
     * @memberof Cart
     */
    price?: number;
}

export function CartFromJSON(json: any): Cart {
    return CartFromJSONTyped(json, false);
}

export function CartFromJSONTyped(json: any, ignoreDiscriminator: boolean): Cart {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'entries': !exists(json, 'entries') ? undefined : (json['entries'] === null ? null : mapValues(json['entries'], CartEntryFromJSON)),
        'price': !exists(json, 'price') ? undefined : json['price'],
    };
}

export function CartToJSON(value?: Cart | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'entries': value.entries === undefined ? undefined : (value.entries === null ? null : mapValues(value.entries, CartEntryToJSON)),
        'price': value.price,
    };
}

