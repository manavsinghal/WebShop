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
import { SellerRoutingModule } from './seller-routing.module';
import { ManageSellerComponent } from '../../domain/seller/manage-seller/manage-seller.component.designer';
import { ManageSellerAddressComponent } from '../../domain/seller/manage-seller-address/manage-seller-address.component.designer';
import { ManageSellerBankAccountComponent } from '../../domain/seller/manage-seller-bank-account/manage-seller-bank-account.component.designer';
import { ManageSellerPhoneComponent } from '../../domain/seller/manage-seller-phone/manage-seller-phone.component.designer';
import { ViewSellerComponent } from '../../domain/seller/view-seller/view-seller.component.designer';
import { ViewSellerAddressComponent } from '../../domain/seller/view-seller-address/view-seller-address.component.designer';
import { ViewSellerBankAccountComponent } from '../../domain/seller/view-seller-bank-account/view-seller-bank-account.component.designer';
import { ViewSellerPhoneComponent } from '../../domain/seller/view-seller-phone/view-seller-phone.component.designer';

@NgModule({
    imports: [
        SellerRoutingModule,
        SharedModule,
        ManageSellerComponent,
        ManageSellerAddressComponent,
        ManageSellerBankAccountComponent,
        ManageSellerPhoneComponent,
        ViewSellerComponent,
        ViewSellerAddressComponent,
        ViewSellerBankAccountComponent,
        ViewSellerPhoneComponent
    ],
    providers: [SortPipe]
})
	
	

// Export the SellerModule class
export class SellerModule { }
