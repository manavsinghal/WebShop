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

// Define the CustomerAddressBase class, extending the ViewModelBase class
export class CustomerAddressBase extends ViewModelBase {    
    CustomerAddressUId!: Guid;
    CustomerAddressId!: number;
    CustomerUId!: Guid;
    AddressTypeUId!: Guid;
    Line1!: string;
    Line2!: string;
    Line3!: string;
    CountryUId!: Guid;
    StateCode!: string;
    ZipCode!: string;
    IsPreferred!: boolean;
    SortOrder!: number;

   // Constructor for the CustomerAddressBase class, calling the superclass constructor
   constructor() {
        super();
    }
}
