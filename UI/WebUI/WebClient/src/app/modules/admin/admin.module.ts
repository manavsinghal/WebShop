/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { NgModule } from '@angular/core';
import { SortPipe } from '../../shared/pipes/sort.pipe';
import { SharedModule } from '../../shared/shared.module';
import { AdminRoutingModule } from './admin-routing.module';
	
import { AdminComponent } from './manage-features/admin/admin.component';
import { ManageAdminEntitiesComponent } from './manage-features/manage-admin-entities/manage-admin-entities.component';
import { ManageSchemaComponent } from './manage-features/manage-schema/manage-schema.component';
import { AccountModule  } from './domain/account/account.module';
import { ProductModule  } from './domain/product/product.module';
import { SellerModule  } from './domain/seller/seller.module';
import { ShoppingCartModule  } from './domain/shopping-cart/shopping-cart.module';
import { MasterModule  } from './domain/master/master.module';
import { OrderModule  } from './domain/order/order.module';
import { ShipperModule  } from './domain/shipper/shipper.module';
import { CustomerModule  } from './domain/customer/customer.module';

// Define the AdminModule using the NgModule decorator
@NgModule({
    imports: [
        AdminRoutingModule,
        AccountModule,
        ProductModule,
        SellerModule,
        ShoppingCartModule,
        MasterModule,
        OrderModule,
        ShipperModule,
        CustomerModule,
        SharedModule,
        AdminComponent,
        ManageAdminEntitiesComponent,
        ManageSchemaComponent
    ],
    providers: [SortPipe]
})
	

// Export the AdminModule class
export class AdminModule { }
