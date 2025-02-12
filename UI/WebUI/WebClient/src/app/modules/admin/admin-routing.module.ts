/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { Routes } from '@angular/router';
import { routes as account_route } from './domain/account/account-routing.module';
import { routes as customer_route } from './domain/customer/customer-routing.module';
import { routes as master_route } from './domain/master/master-routing.module';
import { routes as order_route } from './domain/order/order-routing.module';
import { routes as product_route } from './domain/product/product-routing.module';
import { routes as seller_route } from './domain/seller/seller-routing.module';
import { routes as shipper_route } from './domain/shipper/shipper-routing.module';
import { routes as shopping_cart_route } from './domain/shopping-cart/shopping-cart-routing.module';




export const routes: Routes = [
    { path: 'manageGroupEntities', loadComponent: () => import('./manage-features/admin/admin.component').then(m => m.AdminComponent) },
    { path: 'manageAdminEntities', loadComponent: () => import('./manage-features/manage-admin-entities/manage-admin-entities.component').then(m => m.ManageAdminEntitiesComponent) },
    { path: '', loadComponent: () => import('./manage-features/manage-schema/manage-schema.component').then(m => m.ManageSchemaComponent) },
    { path: '', children: [...account_route] },
    { path: '', children: [...customer_route] },
    { path: '', children: [...master_route] },
    { path: '', children: [...order_route] },
    { path: '', children: [...product_route] },
    { path: '', children: [...seller_route] },
    { path: '', children: [...shipper_route] },
    { path: '', children: [...shopping_cart_route] },
];

