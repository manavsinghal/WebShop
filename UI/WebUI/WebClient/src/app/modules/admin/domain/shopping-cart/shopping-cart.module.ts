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
import { ShoppingCartRoutingModule } from './shopping-cart-routing.module';
import { ManageShoppingCartComponent } from '../../domain/shopping-cart/manage-shopping-cart/manage-shopping-cart.component.designer';
import { ManageShoppingCartWishListComponent } from '../../domain/shopping-cart/manage-shopping-cart-wish-list/manage-shopping-cart-wish-list.component.designer';
import { ViewShoppingCartComponent } from '../../domain/shopping-cart/view-shopping-cart/view-shopping-cart.component.designer';
import { ViewShoppingCartWishListComponent } from '../../domain/shopping-cart/view-shopping-cart-wish-list/view-shopping-cart-wish-list.component.designer';

@NgModule({
    imports: [
        ShoppingCartRoutingModule,
        SharedModule,
        ManageShoppingCartComponent,
        ManageShoppingCartWishListComponent,
        ViewShoppingCartComponent,
        ViewShoppingCartWishListComponent
    ],
    providers: [SortPipe]
})
	
	

// Export the ShoppingCartModule class
export class ShoppingCartModule { }
