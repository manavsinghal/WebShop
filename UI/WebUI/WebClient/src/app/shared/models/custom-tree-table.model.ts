/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { HttpErrorResponse } from '@angular/common/http';
import { Guid } from '../../core/helpers/guid';
import { TreeNode } from 'primeng/api';

export class CustomTreeTableModel<T> {
    data: Array<T>;
    constructor(values: {
        name?: string;
        data?: Array<T>;
    } = {}) {
        this.data = (values.data === undefined ? null : values.data) as T[];
    }
}

export interface CustomTreeNode<T> {
    node: TreeNode<T>;
    data?: CustomModel;
}

export interface CustomModel {
    [id: string]: Guid | number | string;
}

export class ErrorResponse extends HttpErrorResponse {
    override error: Array<ErrorArray> | null = [];
}

interface ErrorArray {
    StackTrace?: string;
    Message?: string;
}

export interface CustomSortModel {
    type: string;
    field: string;
}



