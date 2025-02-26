/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, TemplateRef, inject, viewChild } from '@angular/core';
import { Router, NavigationExtras, ActivatedRoute, RouterLink } from '@angular/router';
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
import { ItemState } from '../../../../../shared/models/item-state.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RowStatuses } from '../../../../../shared/models/rowstatuses.model';
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CountryService } from '../../../../../core/services/domain/master/country.service';
import { NgClass } from '@angular/common';

// Define the ViewCountryComponent
@Component({
    // Selector for the component
    selector: 'app-view-country',
    // Template URL for the component
    templateUrl: './view-country.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewCountryComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly countryService = inject(CountryService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly modalService = inject(NgbModal);
    private readonly translateService = inject(TranslateService);

    readonly viewCountriesTable = viewChild.required<TreeTable>('this.ViewCountriesTable');  
    manageCountry: CustomTreeTableModel<TreeNode<Country>> = new CustomTreeTableModel<TreeNode<Country>>({
        data: new Array<TreeNode<Country>>()
    });
    selectedDetailNode: Country = {} as Country;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    countries!: Array<Country>;
    countryToDelete!: Country;
    doubleClicked!: boolean;
    manageCountryData!: Array<Country>;
    screenGroup!: string;
    searchCountry!: string;
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
                 { field: 'Name', header: 'Name', class: 'text-left' },
                 { field: 'Code', header: 'Code', class: 'text-left' },
                 { field: 'IsEmbargoed', header: 'IsEmbargoed', fieldType: 'boolean', class: 'text-center' },           
                 { field: 'Description', header: 'Description', class: 'text-left' },
                 { field: 'DisplayOrder', header: 'DisplayOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'Code', type: 'string' },
                 { field: 'IsEmbargoed', type: 'string' },
                 { field: 'Description', type: 'string' },
                 { field: 'DisplayOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getCountries();
      
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
	// Represents getMasterData method
    // Method to retrieve master data
	// @returns {void} - getMasterData method returns void.
    */
    getMasterData(): void {
    }
    
    /**
	// Represents refreshCountry method
    // Method to refresh grid data.
	// @returns {void} - refreshCountry method returns void.
    */
    refreshCountry(): void {
        if (this.countries && this.countries.length > 0) {
            this.buildCountryTree(this.countries);
        }
    }  

    navigateToManageEntity(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/manageAdminEntities'], navigationExtras);
    }

    backToAdministration(): void {
        void this.router.navigate(['/app/administration']);
    }

    getCountries(): void {
       this.countryService.getCountries()
            .subscribe((countries: Array<Country>): void => {
                if (countries && countries.length) {
                    this.countries = countries;
                    this.buildCountryTree(countries);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildCountryTree method
	// @param {Array<Country>} activeCountries - activeCountries method param.
	// @returns {void} - buildCountryTree method returns void.
    */
    buildCountryTree(activeCountries: Array<Country>): void {
        const countries: Array<TreeNode> = this.getCountryNode(activeCountries);
        this.manageCountry.data = countries;
        this.manageCountry.data = [...this.customSortMultipleColumns(this.manageCountry.data, this.sortCols)];
    }

    /**
	// Represents getCountryNode method
	// @param {Array<Country>} arr - arr method param.
	// @returns {Array<TreeNode>} - getCountryNode method returns Array<TreeNode>.
    */
    getCountryNode(arr: Array<Country>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((country: Country): void => {
            const innerNode: TreeNode = {
                data: country
            };
            node.push(innerNode);
        });

        return node;
    }

    /**
	// Represents customSearch method
	// @param {Event} event - event method param.
	// @param {TreeTable} content - content method param.
	// @returns {void} - customSearch method returns void.
    */
    customSearch(event: Event, content: TreeTable): void {
        const eventValue = (event.target as HTMLInputElement).value;
        this.searchCountry = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addCountry(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCountry'],
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
	// @param {TreeNode<Country>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<Country>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.CountryUId, this.manageCountry.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToCountry method
	// @param {string} action - action method param.
	// @param {Country} nodeData - nodeData method param.
	// @returns {void} - navigateToCountry method returns void.
    */
    navigateToCountry(nodeData: Country, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCountry'],
                {
                    queryParams: {
                        action,
                        countryUId: nodeData.CountryUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCountry'],
            {
                queryParams: {
                    action: 'Edit',
                    countryUId: this.selectedDetailNode.CountryUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} countryUId - countryUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(countryUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.CountryUId === countryUId))[0].data;
        }
    }

    NavigateToCountryTranslation(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewCountryLanguage'], navigationExtras);
    }
    /**
	// Represents deleteConfirmation method
	// @param {TemplateRef<string>} content - content method param.
	// @param {Country} rowData - rowData method param.
	// @returns void - deleteConfirmation method returns void.
    */
    deleteConfirmation(content: TemplateRef<string>, rowData: Country): void {
        if (rowData) {
            const countryToDelete: TreeNode = this.manageCountry.data.filter((x: TreeNode): boolean => x.data.CountryUId === rowData.CountryUId)[0];
            if (countryToDelete && countryToDelete.data) {
                 this.countryToDelete = countryToDelete.data;
            }     
        }
        this.modalService.open(content, {
            backdrop: 'static', keyboard: false, size: 'md'
        });          
    }
       
    /**
	// Represents delete method
	// @returns {void} - delete method returns void.
    */
    deleteCountry(): void {
        const serviceParameterIndexValue: number = this.countries.findIndex((x: Country): boolean => x.CountryUId === this.countryToDelete.CountryUId);
        if (this.countries && this.countries.length > 0) {
            this.countries[serviceParameterIndexValue].ItemState = ItemState.Deleted;
            this.countries[serviceParameterIndexValue].RowStatusUId = RowStatuses.Inactive;            
        }               
        this.countryService.mergeCountries([this.countries[serviceParameterIndexValue]]).subscribe((): void => {
            let deleteSuccessfulMsg: string = '';
            this.translateService.get('DeleteSuccessMessage').subscribe((data: string): string => deleteSuccessfulMsg = data);
            CoreNotificationService.showSuccess(deleteSuccessfulMsg);
            this.getCountries();
        }, (error: HttpErrorResponse): void => {            
            CoreNotificationService.handleError(error);            
        });
        this.modalService.dismissAll();  
    }
}
