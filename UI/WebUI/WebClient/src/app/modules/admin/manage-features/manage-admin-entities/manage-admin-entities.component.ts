/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationExtras, ActivatedRoute } from '@angular/router';
import { TranslateService, TranslateDirective, TranslatePipe } from '@ngx-translate/core';
import { CoreSessionService } from '../../../../core/services/core.session.service';
import { CoreSubscriptionService } from '../../../../core/services/core.subscription.service';
import { Subscription } from 'rxjs';
import { SharedComponent } from '../../../../shared/shared.component';

// Define the ManageAdminEntitiesComponent
@Component({
    // Selector for the component
    selector: 'app-manage-admin-entities',
    // Template URL for the component
    templateUrl: './manage-admin-entities.component.html',
    imports: [TranslateDirective, TranslatePipe]
})
export class ManageAdminEntitiesComponent extends SharedComponent implements OnInit, OnDestroy {
    screenGroup!: string;
    serviceGroup!: string;
    startIndex: number = 0;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        override readonly coreSessionService: CoreSessionService,
        readonly coreSubscriptionService: CoreSubscriptionService,
        private readonly translateService: TranslateService) {
        super(coreSessionService);
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
    }
 
    /**
	// Represents ngOnInit method
    // Lifecycle hook for initialization
	// @returns {void} - ngOnInit method returns void.
    */
    ngOnInit(): void {
        this.languageChangedSubscription = this.coreSessionService.languageChanged.subscribe((): void => {
            this.onLanguageChanged();
        });
        this.screenGroup = this.route.snapshot.queryParams['screenGroup'];
        this.serviceGroup = this.route.snapshot.queryParams['serviceGroup'];
        window.scroll(0, 0);
    }

    /**
	// Represents ngOnDestroy method
    // Lifecycle hook for destruction
	// @returns {void} - ngOnDestroy method returns void.
    */
    ngOnDestroy(): void {
		this.coreSubscriptionService.unsubscribeSubscriptions(this);
	}

    /**
	// Represents onLanguageChanged method
    // Method to handle language changes
	// @returns {void} - onLanguageChanged method returns void.
    */
    onLanguageChanged(): void {
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
    }

    /**
	// Represents navigateToManageEntity method
	// @param {string} selectedServiceGroup - selectedServiceGroup method param.
	// @returns {void} - navigateToManageEntity method returns void.
    */
    navigateToManageEntity(selectedServiceGroup: string): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };
        selectedServiceGroup = selectedServiceGroup.replace('Manage', '').trim().replace(' ', '_');

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/view'+ selectedServiceGroup], navigationExtras);
    }

    backToAdmin(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/manageGroupEntities'], navigationExtras);
    }

    /**
    // Represents searchTable method
    // @param {string} id - id method param.
    // @param {Event} event - event method param.
    // @returns {void} - searchTable method returns void.
    */
    searchTable(id: string, event: Event): void {
        let searchTxt = (event.target as HTMLInputElement).value;
        let tableRows;
        const table = document.getElementById(id);
        searchTxt = searchTxt.length >= 3 ? searchTxt : '';

        // checks table exists or not
        if (table !== null) {

            tableRows = table.getElementsByTagName('tr');

            // convert to an array using Array.from()
            Array.from(tableRows).forEach((tableRow: HTMLElement) => {
                const isSearchTextExists = this.searchRow(tableRow, searchTxt.toUpperCase());

                tableRow.style.display = isSearchTextExists || searchTxt === '' ? '' : 'none';
            });
        }
    }

    /**
    // Represents searchRow method
    // @param {HTMLElement} rowData - rowData method param.
    // @param {string} searchText - searchText method param.
    // @returns {boolean} - searchRow method returns boolean.
    */
    searchRow(rowData: HTMLElement, searchText: string): boolean {
        const tableDataList = rowData.getElementsByTagName('td');
        let isSearchTextExists = false;

        // convert to an array using Array.from()
        Array.from(tableDataList).forEach((tableData) => {

            // Execute only when innerHtML is not empty
            if (tableData.innerHTML !== null &&
                tableData.innerHTML.toUpperCase().indexOf(searchText) >= this.startIndex) {
                isSearchTextExists = true;
            }
        });

        return isSearchTextExists;
    }
}
