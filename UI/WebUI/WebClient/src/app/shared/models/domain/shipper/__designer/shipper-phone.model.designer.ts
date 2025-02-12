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

// Define the ShipperPhoneBase class, extending the ViewModelBase class
export class ShipperPhoneBase extends ViewModelBase {    
    ShipperPhoneUId!: Guid;
    ShipperPhoneId!: number;
    ShipperUId!: Guid;
    PhoneTypeUId!: Guid;
    CountryUId!: Guid;
    PhoneNumber!: string;
    IsPreferred!: boolean;
    SortOrder!: number;

   // Constructor for the ShipperPhoneBase class, calling the superclass constructor
   constructor() {
        super();
    }
}
