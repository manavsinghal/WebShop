/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { Routes } from '@angular/router';




// Define the routes for the OrderRoutingModule module
export const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageOrder', loadComponent: () => import('./manage-order/manage-order.component.designer').then(m => m.ManageOrderComponent) },          
            { path: 'manageOrderItem', loadComponent: () => import('./manage-order-item/manage-order-item.component.designer').then(m => m.ManageOrderItemComponent) },          
            { path: 'manageOrderItemShipment', loadComponent: () => import('./manage-order-item-shipment/manage-order-item-shipment.component.designer').then(m => m.ManageOrderItemShipmentComponent) },          
            { path: 'viewOrder', loadComponent: () => import('./view-order/view-order.component.designer').then(m => m.ViewOrderComponent) },          
            { path: 'viewOrderItem', loadComponent: () => import('./view-order-item/view-order-item.component.designer').then(m => m.ViewOrderItemComponent) },          
            { path: 'viewOrderItemShipment', loadComponent: () => import('./view-order-item-shipment/view-order-item-shipment.component.designer').then(m => m.ViewOrderItemShipmentComponent) },          
        ]
    }  
];

