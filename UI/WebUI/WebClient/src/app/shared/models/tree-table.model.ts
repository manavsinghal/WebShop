/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

export interface TreeTableHeaderModel {
    field: string;
    header: string;
    class?: string;
    tdClass?: string;
    label?: string;
    IsDecimal?: boolean;
    fieldType?: string;
    checkbox?: boolean;
    type?: string;
    minValue?: number;
    maxValue?: number;
    isRequired?: boolean;
}

export class CustomEvent {
    currentTarget?: Checked;
}

interface Checked {
    checked: boolean;
}

export interface CommandsModel {

    [id: string]: () => void;
}

