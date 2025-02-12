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

// Define the CustomerBase class, extending the ViewModelBase class
export class CustomerBase extends ViewModelBase {    
    CustomerUId!: Guid;
    CustomerId!: number;
    Name!: string;
    Rating!: number;
    CustomerStatusUId!: Guid;
    SortOrder!: number;

   // Constructor for the CustomerBase class, calling the superclass constructor
   constructor() {
        super();
    }
}
