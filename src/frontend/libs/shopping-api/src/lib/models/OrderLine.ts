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
    ProductEntity,
    ProductEntityFromJSON,
    ProductEntityFromJSONTyped,
    ProductEntityToJSON,
} from './ProductEntity';

/**
 * 
 * @export
 * @interface OrderLine
 */
export interface OrderLine {
    /**
     * 
     * @type {string}
     * @memberof OrderLine
     */
    productCatalogueNumber?: string | null;
    /**
     * 
     * @type {ProductEntity}
     * @memberof OrderLine
     */
    product?: ProductEntity;
    /**
     * 
     * @type {number}
     * @memberof OrderLine
     */
    count?: number;
    /**
     * 
     * @type {number}
     * @memberof OrderLine
     */
    totalAmount?: number;
}

export function OrderLineFromJSON(json: any): OrderLine {
    return OrderLineFromJSONTyped(json, false);
}

export function OrderLineFromJSONTyped(json: any, ignoreDiscriminator: boolean): OrderLine {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'productCatalogueNumber': !exists(json, 'productCatalogueNumber') ? undefined : json['productCatalogueNumber'],
        'product': !exists(json, 'product') ? undefined : ProductEntityFromJSON(json['product']),
        'count': !exists(json, 'count') ? undefined : json['count'],
        'totalAmount': !exists(json, 'totalAmount') ? undefined : json['totalAmount'],
    };
}

export function OrderLineToJSON(value?: OrderLine | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'productCatalogueNumber': value.productCatalogueNumber,
        'product': ProductEntityToJSON(value.product),
        'count': value.count,
        'totalAmount': value.totalAmount,
    };
}

