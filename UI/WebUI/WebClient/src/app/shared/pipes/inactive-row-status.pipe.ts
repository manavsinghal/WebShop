/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Pipe, PipeTransform } from '@angular/core';
import { RowStatuses } from '../models/rowstatuses.model';
import { Guid } from '../../core/helpers/guid';

@Pipe({
    name: 'InactiveRowStatusFilter',
    pure: false
})

export class InactiveRowStatusFilterPipe implements PipeTransform {
    transform<T>(items: Array<T>, args?: string, value?: Guid, searchField?: string, isFromMultiSelectDropdown?: boolean, selectedvalues?: Array<T>): Array<T> {
        const filter: Guid = RowStatuses.Inactive;
        const rowStatusUId: string = 'RowStatusUId' as string;
        if (args && args === 'Edit' && items && items.length > 0 && value) {

            return items.filter((item: T): boolean => item !== undefined && item !== null
                && (item[rowStatusUId as keyof T] === RowStatuses.Active || item[searchField as keyof T] === value));
        }
        if (args && args === 'Edit' && items && items.length > 0 && isFromMultiSelectDropdown && selectedvalues && selectedvalues.length > 0) {


            const filteredItems: Array<T> = items.filter((item: T): boolean => item !== undefined && item !== null
                && (item[rowStatusUId as keyof T] === RowStatuses.Active));
            selectedvalues.forEach((selectedValue: T): void => {
                const filteredInactiveRecord: T | undefined = items.filter((item: T): boolean => item !== undefined && item !== null
                    && (item[rowStatusUId as keyof T] === RowStatuses.Inactive && item[searchField as keyof T] === selectedValue[searchField as keyof T]))[0];
                if (filteredInactiveRecord) {
                    filteredItems.push(filteredInactiveRecord);
                }
            });

            return filteredItems;
        }

        if (!items || !filter) {

            return items;
        }

        return items.filter((item: T): boolean => item !== undefined && item !== null
            && item[rowStatusUId as keyof T] !== filter);
    }
}

