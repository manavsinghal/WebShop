/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

// Import necessary modules and services
import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { SharedComponent } from '../../shared/shared.component';

// Define the CoreSubscriptionService
@Injectable()
export class CoreSubscriptionService {
    /**
     * Unsubscribe all subscriptions in the given context.
     * @param context The SharedComponent instance to unsubscribe from.
     */
    unsubscribeSubscriptions(context: SharedComponent): void {
        // Get all property keys from the context object
        const keys: Array<keyof SharedComponent> = Object.keys(context) as Array<keyof SharedComponent>;

        // Iterate over each property key
        keys.forEach((key: keyof SharedComponent): void => {
            // Get the property value
            const property = context[key];

            // Check if the property is truthy and has an unsubscribe method
            if (property && typeof (property as unknown as Subscription).unsubscribe === 'function') {
                // Unsubscribe from the subscription
                (property as unknown as Subscription).unsubscribe();
            }
        });
    }
}

