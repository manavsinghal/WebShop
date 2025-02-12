/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary modules and classes
import { Guid } from '../../../../../core/helpers/guid';
import { ViewModelBase } from '../../../view-model-base.model';

// Define the LanguageBase class, extending the ViewModelBase class
export class LanguageBase extends ViewModelBase {    
    LanguageUId!: Guid;
    LanguageId!: number;
    Name!: string;
    LocalizedName!: string;
    DisplayName!: string;
    Culture!: string;
    AzureCulture!: string;
    ImageUrl!: string;
    ImageBinary!: string;
    Description!: string;
    IsRTL!: boolean;
    DisplayOrder!: number;

   // Constructor for the LanguageBase class, calling the superclass constructor
   constructor() {
        super();
    }
}
