/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Guid } from '../../core/helpers/guid';

export class ViewModelBase {
    RowStatusUId!: Guid;
    CreatedByUser!: string;
    CreatedByApp!: string;
    CreatedOn!: string | Date;
    ModifiedByUser!: string;
    ModifiedByApp!: string;
    ModifiedOn!: string | Date;
    CorrelationUId!: Guid;
    RowVersion!: string;
    ItemState!: number;
    EffectiveFrom?: string | Date;
    EffectiveTo?: string | Date;
    ModifiedByAccountUId! : string;
    CreatedByAccountUIdEmailId! : string;
    ModifiedByAccountUIdEmailId! : string;
    CreatedByAccountUId! : string;
}

