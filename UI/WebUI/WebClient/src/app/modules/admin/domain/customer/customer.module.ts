/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { NgModule } from '@angular/core';
import { SortPipe } from '../../../../shared/pipes/sort.pipe';
import { SharedModule } from '../../../../shared/shared.module';
import { CustomerRoutingModule } from './customer-routing.module';
import { ManageCustomerComponent } from '../../domain/customer/manage-customer/manage-customer.component.designer';
import { ManageCustomerAddressComponent } from '../../domain/customer/manage-customer-address/manage-customer-address.component.designer';
import { ManageCustomerCardComponent } from '../../domain/customer/manage-customer-card/manage-customer-card.component.designer';
import { ManageCustomerPhoneComponent } from '../../domain/customer/manage-customer-phone/manage-customer-phone.component.designer';
import { ViewCustomerComponent } from '../../domain/customer/view-customer/view-customer.component.designer';
import { ViewCustomerAddressComponent } from '../../domain/customer/view-customer-address/view-customer-address.component.designer';
import { ViewCustomerCardComponent } from '../../domain/customer/view-customer-card/view-customer-card.component.designer';
import { ViewCustomerPhoneComponent } from '../../domain/customer/view-customer-phone/view-customer-phone.component.designer';

@NgModule({
    imports: [
        CustomerRoutingModule,
        SharedModule,
        ManageCustomerComponent,
        ManageCustomerAddressComponent,
        ManageCustomerCardComponent,
        ManageCustomerPhoneComponent,
        ViewCustomerComponent,
        ViewCustomerAddressComponent,
        ViewCustomerCardComponent,
        ViewCustomerPhoneComponent
    ],
    providers: [SortPipe]
})
	
	

// Export the CustomerModule class
export class CustomerModule { }
