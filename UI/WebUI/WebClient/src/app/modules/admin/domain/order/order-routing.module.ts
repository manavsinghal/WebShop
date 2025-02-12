/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageOrderComponent } from './manage-order/manage-order.component.designer';
import { ManageOrderItemComponent } from './manage-order-item/manage-order-item.component.designer';
import { ManageOrderItemShipmentComponent } from './manage-order-item-shipment/manage-order-item-shipment.component.designer';
import { ViewOrderComponent } from './view-order/view-order.component.designer';
import { ViewOrderItemComponent } from './view-order-item/view-order-item.component.designer';
import { ViewOrderItemShipmentComponent } from './view-order-item-shipment/view-order-item-shipment.component.designer';

// Define the routes for the OrderRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageOrder', component: ManageOrderComponent },          
            { path: 'manageOrderItem', component: ManageOrderItemComponent },          
            { path: 'manageOrderItemShipment', component: ManageOrderItemShipmentComponent },          
            { path: 'viewOrder', component: ViewOrderComponent },          
            { path: 'viewOrderItem', component: ViewOrderItemComponent },          
            { path: 'viewOrderItemShipment', component: ViewOrderItemShipmentComponent },          
        ]
    }  
];

// Define the  OrderRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the OrderRoutingModule class
export class OrderRoutingModule { }
