/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'sortBy' })
export class SortPipe implements PipeTransform {
    transform<T>(array: Array<T>, args?: string, ignoreCase: boolean = false): Array<T> {
        if (array && array.length > 0) {
            array.sort((a: T, b: T): number => {
                const sortCols = args ? args.split(",") : null;
                if (sortCols && sortCols.length > 0) {
                    for (const col of sortCols) {
                        let toCompareA: string | T;
                        let toCompareB: string | T;
                        if (ignoreCase || typeof (a[col as keyof T]) === 'string') {
                            const aValue: string = a[col as keyof T] as string;
                            const bValue: string = b[col as keyof T] as string;
                            toCompareA = aValue.toLowerCase();
                            toCompareB = bValue.toLowerCase();
                        } else {
                            toCompareA = a[col as keyof T] as T;
                            toCompareB = b[col as keyof T] as T;
                        }
                        if (toCompareA > toCompareB) {
                            return 1;
                        }
                        if (toCompareA < toCompareB) {
                            return -1;
                        }
                    }
                }

                return 0;
            });
        }

        return array;
    }
}

