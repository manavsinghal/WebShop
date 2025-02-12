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
import { ShipperRoutingModule } from './shipper-routing.module';
import { ManageShipperComponent } from '../../domain/shipper/manage-shipper/manage-shipper.component.designer';
import { ManageShipperAddressComponent } from '../../domain/shipper/manage-shipper-address/manage-shipper-address.component.designer';
import { ManageShipperBankAccountComponent } from '../../domain/shipper/manage-shipper-bank-account/manage-shipper-bank-account.component.designer';
import { ManageShipperPhoneComponent } from '../../domain/shipper/manage-shipper-phone/manage-shipper-phone.component.designer';
import { ViewShipperComponent } from '../../domain/shipper/view-shipper/view-shipper.component.designer';
import { ViewShipperAddressComponent } from '../../domain/shipper/view-shipper-address/view-shipper-address.component.designer';
import { ViewShipperBankAccountComponent } from '../../domain/shipper/view-shipper-bank-account/view-shipper-bank-account.component.designer';
import { ViewShipperPhoneComponent } from '../../domain/shipper/view-shipper-phone/view-shipper-phone.component.designer';

@NgModule({
    imports: [
        ShipperRoutingModule,
        SharedModule,
        ManageShipperComponent,
        ManageShipperAddressComponent,
        ManageShipperBankAccountComponent,
        ManageShipperPhoneComponent,
        ViewShipperComponent,
        ViewShipperAddressComponent,
        ViewShipperBankAccountComponent,
        ViewShipperPhoneComponent
    ],
    providers: [SortPipe]
})
	
	

// Export the ShipperModule class
export class ShipperModule { }
