/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, ViewChild, inject } from '@angular/core';
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
import { CustomerPhone } from '../../../../../shared/models/domain/customer/customer-phone.model';
import { CustomerPhoneService } from '../../../../../core/services/domain/customer/customer-phone.service';
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CountryService } from '../../../../../core/services/domain/master/country.service';
import { Customer } from '../../../../../shared/models/domain/customer/customer.model';
import { CustomerService } from '../../../../../core/services/domain/customer/customer.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { NgClass } from '@angular/common';

// Define the ViewCustomerPhoneComponent
@Component({
    // Selector for the component
    selector: 'app-view-customer-phone',
    // Template URL for the component
    templateUrl: './view-customer-phone.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewCustomerPhoneComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly countryService = inject(CountryService);
    readonly customerPhoneService = inject(CustomerPhoneService);
    readonly customerService = inject(CustomerService);
    readonly masterListItemService = inject(MasterListItemService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    @ViewChild('this.ViewCustomerPhonesTable', { static: false }) viewCustomerPhonesTable!: TreeTable;  
    manageCustomerPhone: CustomTreeTableModel<TreeNode<CustomerPhone>> = new CustomTreeTableModel<TreeNode<CustomerPhone>>({
        data: new Array<TreeNode<CustomerPhone>>()
    });
    selectedDetailNode: CustomerPhone = {} as CustomerPhone;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    countries!: Array<Country>;
    customerPhones!: Array<CustomerPhone>;
    customers!: Array<Customer>;
    doubleClicked!: boolean;
    manageCustomerPhoneData!: Array<CustomerPhone>;
    phoneTypes!: Array<MasterListItem>;
    screenGroup!: string;
    searchCustomerPhone!: string;
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
                 { field: 'CustomerUIdValue', header: 'Customer', class:'text-left' },          
                 { field: 'PhoneTypeUIdValue', header: 'PhoneType', class:'text-left' },          
                 { field: 'CountryUIdValue', header: 'Country', class:'text-left' },          
                 { field: 'PhoneNumber', header: 'PhoneNumber', class: 'text-left' },
                 { field: 'IsPreferred', header: 'IsPreferred', fieldType: 'boolean', class: 'text-center' },           
        ];
        this.sortCols = [
                 { field: 'CustomerUIdValue', type: 'string' },
                 { field: 'PhoneTypeUIdValue', type: 'string' },
                 { field: 'CountryUIdValue', type: 'string' },
                 { field: 'PhoneNumber', type: 'string' },
                 { field: 'IsPreferred', type: 'string' },
        ];
        this.getMasterData();
        this.getCustomerPhones();
      
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
       this.customerService.getCustomers().subscribe((customers: Array<Customer>): void => {
           this.customers = customers;
           this.refreshCustomerPhone();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('PhoneType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.phoneTypes = masterListItems;
           this.refreshCustomerPhone();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshCustomerPhone method
    // Method to refresh grid data.
	// @returns {void} - refreshCustomerPhone method returns void.
    */
    refreshCustomerPhone(): void {
        if (this.customerPhones && this.customerPhones.length > 0) {
            this.buildCustomerPhoneTree(this.customerPhones);
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

    getCustomerPhones(): void {
       this.customerPhoneService.getCustomerPhones()
            .subscribe((customerPhones: Array<CustomerPhone>): void => {
                if (customerPhones && customerPhones.length) {
                    this.customerPhones = customerPhones;
                    this.buildCustomerPhoneTree(customerPhones);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildCustomerPhoneTree method
	// @param {Array<CustomerPhone>} activeCustomerPhones - activeCustomerPhones method param.
	// @returns {void} - buildCustomerPhoneTree method returns void.
    */
    buildCustomerPhoneTree(activeCustomerPhones: Array<CustomerPhone>): void {
        const customerPhones: Array<TreeNode> = this.getCustomerPhoneNode(activeCustomerPhones);
        this.manageCustomerPhone.data = customerPhones;
        this.manageCustomerPhone.data = [...this.customSortMultipleColumns(this.manageCustomerPhone.data, this.sortCols)];
    }

    /**
	// Represents getCustomerPhoneNode method
	// @param {Array<CustomerPhone>} arr - arr method param.
	// @returns {Array<TreeNode>} - getCustomerPhoneNode method returns Array<TreeNode>.
    */
    getCustomerPhoneNode(arr: Array<CustomerPhone>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((customerPhone: CustomerPhone): void => {
            const innerNode: TreeNode = {
                data: customerPhone
            };
            innerNode.data.CountryUIdValue = this.getDetailsForUId(this.countries, customerPhone.CountryUId, 'CountryUId', 'Name');
                        
            innerNode.data.CustomerUIdValue = this.getDetailsForUId(this.customers, customerPhone.CustomerUId, 'CustomerUId', 'Name');
                        
            innerNode.data.PhoneTypeUIdValue = this.getDetailsForUId(this.phoneTypes, customerPhone.PhoneTypeUId, 'MasterListItemUId', 'Name');
                        
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
        this.searchCustomerPhone = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addCustomerPhone(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomerPhone'],
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
	// @param {TreeNode<CustomerPhone>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<CustomerPhone>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.CustomerPhoneUId, this.manageCustomerPhone.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToCustomerPhone method
	// @param {string} action - action method param.
	// @param {CustomerPhone} nodeData - nodeData method param.
	// @returns {void} - navigateToCustomerPhone method returns void.
    */
    navigateToCustomerPhone(nodeData: CustomerPhone, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomerPhone'],
                {
                    queryParams: {
                        action,
                        customerPhoneUId: nodeData.CustomerPhoneUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomerPhone'],
            {
                queryParams: {
                    action: 'Edit',
                    customerPhoneUId: this.selectedDetailNode.CustomerPhoneUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} customerPhoneUId - customerPhoneUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(customerPhoneUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.CustomerPhoneUId === customerPhoneUId))[0].data;
        }
    }
}
