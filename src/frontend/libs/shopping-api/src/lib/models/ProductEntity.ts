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
/**
 * 
 * @export
 * @interface ProductEntity
 */
export interface ProductEntity {
    /**
     * 
     * @type {string}
     * @memberof ProductEntity
     */
    catalogueNumber?: string | null;
    /**
     * 
     * @type {Date}
     * @memberof ProductEntity
     */
    createdAt?: Date;
    /**
     * 
     * @type {Date}
     * @memberof ProductEntity
     */
    updatedAt?: Date;
    /**
     * 
     * @type {string}
     * @memberof ProductEntity
     */
    name?: string | null;
    /**
     * 
     * @type {number}
     * @memberof ProductEntity
     */
    price?: number;
    /**
     * 
     * @type {boolean}
     * @memberof ProductEntity
     */
    available?: boolean;
}

export function ProductEntityFromJSON(json: any): ProductEntity {
    return ProductEntityFromJSONTyped(json, false);
}

export function ProductEntityFromJSONTyped(json: any, ignoreDiscriminator: boolean): ProductEntity {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'catalogueNumber': !exists(json, 'catalogueNumber') ? undefined : json['catalogueNumber'],
        'createdAt': !exists(json, 'createdAt') ? undefined : (new Date(json['createdAt'])),
        'updatedAt': !exists(json, 'updatedAt') ? undefined : (new Date(json['updatedAt'])),
        'name': !exists(json, 'name') ? undefined : json['name'],
        'price': !exists(json, 'price') ? undefined : json['price'],
        'available': !exists(json, 'available') ? undefined : json['available'],
    };
}

export function ProductEntityToJSON(value?: ProductEntity | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'catalogueNumber': value.catalogueNumber,
        'createdAt': value.createdAt === undefined ? undefined : (value.createdAt.toISOString()),
        'updatedAt': value.updatedAt === undefined ? undefined : (value.updatedAt.toISOString()),
        'name': value.name,
        'price': value.price,
        'available': value.available,
    };
}
