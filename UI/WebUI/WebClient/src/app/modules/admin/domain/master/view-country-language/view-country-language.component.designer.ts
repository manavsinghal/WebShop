/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, inject, viewChild } from '@angular/core';
import { Router, NavigationExtras, ActivatedRoute } from '@angular/router';
import { TranslateService, TranslateDirective, TranslatePipe } from '@ngx-translate/core';
import { CoreSessionService } from '../../../../../core/services/core.session.service';
import { CoreSubscriptionService } from '../../../../../core/services/core.subscription.service';
import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Guid } from '../../../../../core/helpers/guid';
import { SharedComponent } from '../../../../../shared/shared.component';
import { TreeNode, TreeTableNode, PrimeTemplate } from 'primeng/api';
import { TreeTable, TreeTableModule } from 'primeng/treetable';
import { CustomSortModel, CustomTreeTableModel } from '../../../../../shared/models/custom-tree-table.model';
import { TreeTableHeaderModel } from '../../../../../shared/models/tree-table.model';
import { CoreNotificationService } from '../../../../../core/services/core.notification.service';
import { CountryLanguage } from '../../../../../shared/models/domain/master/country-language.model';
import { CountryLanguageService } from '../../../../../core/services/domain/master/country-language.service';
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CountryService } from '../../../../../core/services/domain/master/country.service';
import { Language } from '../../../../../shared/models/domain/master/language.model';
import { LanguageService } from '../../../../../core/services/domain/master/language.service';
import { NgClass } from '@angular/common';

// Define the ViewCountryLanguageComponent
@Component({
    // Selector for the component
    selector: 'app-view-country-language',
    // Template URL for the component
    templateUrl: './view-country-language.component.html',
    imports: [TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewCountryLanguageComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly countryLanguageService = inject(CountryLanguageService);
    readonly countryService = inject(CountryService);
    readonly languageService = inject(LanguageService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewCountryLanguagesTable = viewChild.required<TreeTable>('this.ViewCountryLanguagesTable');  
    manageCountryLanguage: CustomTreeTableModel<TreeNode<CountryLanguage>> = new CustomTreeTableModel<TreeNode<CountryLanguage>>({
        data: new Array<TreeNode<CountryLanguage>>()
    });
    selectedDetailNode: CountryLanguage = {} as CountryLanguage;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    countries!: Array<Country>;
    countryLanguages!: Array<CountryLanguage>;
    doubleClicked!: boolean;
    languages!: Array<Language>;
    manageCountryLanguageData!: Array<CountryLanguage>;
    screenGroup!: string;
    searchCountryLanguage!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor() {
        const coreSessionService = inject(CoreSessionService);

        super();
        this.coreSessionService = coreSessionService;

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
        this.cols = [
                 { field: 'CountryUIdValue', header: 'Country', class:'text-left' },          
                 { field: 'LanguageUIdValue', header: 'Language', class:'text-left' },          
                 { field: 'Name', header: 'Name', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'CountryUIdValue', type: 'string' },
                 { field: 'LanguageUIdValue', type: 'string' },
                 { field: 'Name', type: 'string' },
        ];
        this.getMasterData();
        this.getCountryLanguages();
      
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
        this.countryService.getCountries().subscribe((countries: Array<Country>): void => {
            this.countries = countries;
        // On error, handle the error
        }, (error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
    }
 
    /**
	// Represents getMasterData method
    // Method to retrieve master data
	// @returns {void} - getMasterData method returns void.
    */
    getMasterData(): void {
           this.onLanguageChanged();
       this.languageService.getLanguages().subscribe((languages: Array<Language>): void => {
           this.languages = languages;
           this.refreshCountryLanguage();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshCountryLanguage method
    // Method to refresh grid data.
	// @returns {void} - refreshCountryLanguage method returns void.
    */
    refreshCountryLanguage(): void {
        if (this.countryLanguages && this.countryLanguages.length > 0) {
            this.buildCountryLanguageTree(this.countryLanguages);
        }
    }  

    navigateToManageEntity(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewCountry'], navigationExtras);
    }

    backToAdministration(): void {
        void this.router.navigate(['/app/administration']);
    }

    getCountryLanguages(): void {
       this.countryLanguageService.getCountryLanguages()
            .subscribe((countryLanguages: Array<CountryLanguage>): void => {
                if (countryLanguages && countryLanguages.length) {
                    this.countryLanguages = countryLanguages;
                    this.buildCountryLanguageTree(countryLanguages);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildCountryLanguageTree method
	// @param {Array<CountryLanguage>} activeCountryLanguages - activeCountryLanguages method param.
	// @returns {void} - buildCountryLanguageTree method returns void.
    */
    buildCountryLanguageTree(activeCountryLanguages: Array<CountryLanguage>): void {
        const countryLanguages: Array<TreeNode> = this.getCountryLanguageNode(activeCountryLanguages);
        this.manageCountryLanguage.data = countryLanguages;
        this.manageCountryLanguage.data = [...this.customSortMultipleColumns(this.manageCountryLanguage.data, this.sortCols)];
    }

    /**
	// Represents getCountryLanguageNode method
	// @param {Array<CountryLanguage>} arr - arr method param.
	// @returns {Array<TreeNode>} - getCountryLanguageNode method returns Array<TreeNode>.
    */
    getCountryLanguageNode(arr: Array<CountryLanguage>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((countryLanguage: CountryLanguage): void => {
            const innerNode: TreeNode = {
                data: countryLanguage
            };
            innerNode.data.CountryUIdValue = this.getDetailsForUId(this.countries, countryLanguage.CountryUId, 'CountryUId', 'Name');
                        
            innerNode.data.LanguageUIdValue = this.getDetailsForUId(this.languages, countryLanguage.LanguageUId, 'LanguageUId', 'DisplayName');
                        
            node.push(innerNode);
        });

        return node;
    }

    /**
	// Represents customSearch method
	// @param {TreeTable} content - content method param.
	// @param {Event} event - event method param.
	// @returns {void} - customSearch method returns void.
    */
    customSearch(event: Event, content: TreeTable): void {
        const eventValue = (event.target as HTMLInputElement).value;
        this.searchCountryLanguage = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addCountryLanguage(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCountryLanguage'],
            {
                queryParams: {
                    action: 'Add',
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents onNodeSelected method
	// @param {TreeNode<CountryLanguage>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<CountryLanguage>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.CountryLanguageUId, this.manageCountryLanguage.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToCountryLanguage method
	// @param {CountryLanguage} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToCountryLanguage method returns void.
    */
    navigateToCountryLanguage(nodeData: CountryLanguage, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCountryLanguage'],
                {
                    queryParams: {
                        action,
                        countryLanguageUId: nodeData.CountryLanguageUId,
                        screenGroup: this.screenGroup,
                        serviceGroup: this.serviceGroup
                    }
                });
        }
    }

    /**
	// Represents onRowDblclick method
	// @returns {void} - onRowDblclick method returns void.
    */
    onRowDblclick(): void {
        this.doubleClicked = true;

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCountryLanguage'],
            {
                queryParams: {
                    action: 'Edit',
                    countryLanguageUId: this.selectedDetailNode.CountryLanguageUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} countryLanguageUId - countryLanguageUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(countryLanguageUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.CountryLanguageUId === countryLanguageUId))[0].data;
        }
    }
}
