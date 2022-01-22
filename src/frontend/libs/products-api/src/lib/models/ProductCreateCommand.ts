/* tslint:disable */
/* eslint-disable */
/**
 * Store.Api.CatalogueManagement
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from '../runtime';
import {
    ProductApiModel,
    ProductApiModelFromJSON,
    ProductApiModelFromJSONTyped,
    ProductApiModelToJSON,
} from './ProductApiModel';

/**
 * 
 * @export
 * @interface ProductCreateCommand
 */
export interface ProductCreateCommand {
    /**
     * 
     * @type {ProductApiModel}
     * @memberof ProductCreateCommand
     */
    product?: ProductApiModel;
}

export function ProductCreateCommandFromJSON(json: any): ProductCreateCommand {
    return ProductCreateCommandFromJSONTyped(json, false);
}

export function ProductCreateCommandFromJSONTyped(json: any, ignoreDiscriminator: boolean): ProductCreateCommand {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'product': !exists(json, 'product') ? undefined : ProductApiModelFromJSON(json['product']),
    };
}

export function ProductCreateCommandToJSON(value?: ProductCreateCommand | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'product': ProductApiModelToJSON(value.product),
    };
}

