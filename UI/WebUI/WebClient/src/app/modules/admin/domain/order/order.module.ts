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
import { OrderRoutingModule } from './order-routing.module';
import { ManageOrderComponent } from '../../domain/order/manage-order/manage-order.component.designer';
import { ManageOrderItemComponent } from '../../domain/order/manage-order-item/manage-order-item.component.designer';
import { ManageOrderItemShipmentComponent } from '../../domain/order/manage-order-item-shipment/manage-order-item-shipment.component.designer';
import { ViewOrderComponent } from '../../domain/order/view-order/view-order.component.designer';
import { ViewOrderItemComponent } from '../../domain/order/view-order-item/view-order-item.component.designer';
import { ViewOrderItemShipmentComponent } from '../../domain/order/view-order-item-shipment/view-order-item-shipment.component.designer';

@NgModule({
    imports: [
        OrderRoutingModule,
        SharedModule,
        ManageOrderComponent,
        ManageOrderItemComponent,
        ManageOrderItemShipmentComponent,
        ViewOrderComponent,
        ViewOrderItemComponent,
        ViewOrderItemShipmentComponent
    ],
    providers: [SortPipe]
})
	
	

// Export the OrderModule class
export class OrderModule { }
