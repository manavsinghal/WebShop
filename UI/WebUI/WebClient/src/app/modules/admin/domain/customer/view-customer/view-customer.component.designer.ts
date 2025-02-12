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
import { Customer } from '../../../../../shared/models/domain/customer/customer.model';
import { CustomerService } from '../../../../../core/services/domain/customer/customer.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { NgClass } from '@angular/common';

// Define the ViewCustomerComponent
@Component({
    // Selector for the component
    selector: 'app-view-customer',
    // Template URL for the component
    templateUrl: './view-customer.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewCustomerComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly customerService = inject(CustomerService);
    readonly masterListItemService = inject(MasterListItemService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewCustomersTable = viewChild.required<TreeTable>('this.ViewCustomersTable');  
    manageCustomer: CustomTreeTableModel<TreeNode<Customer>> = new CustomTreeTableModel<TreeNode<Customer>>({
        data: new Array<TreeNode<Customer>>()
    });
    selectedDetailNode: Customer = {} as Customer;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    customers!: Array<Customer>;
    customerStatuses!: Array<MasterListItem>;
    doubleClicked!: boolean;
    manageCustomerData!: Array<Customer>;
    screenGroup!: string;
    searchCustomer!: string;
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
                 { field: 'Rating', header: 'Rating', class:'text-right' },           
                 { field: 'CustomerStatusUIdValue', header: 'CustomerStatus', class:'text-left' },          
                 { field: 'SortOrder', header: 'SortOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'Rating', type: 'number' },
                 { field: 'CustomerStatusUIdValue', type: 'string' },
                 { field: 'SortOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getCustomers();
      
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
        this.masterListItemService.getMasterListItemsByCode('CustomerStatus').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.customerStatuses = masterListItems;
           this.refreshCustomer();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshCustomer method
    // Method to refresh grid data.
	// @returns {void} - refreshCustomer method returns void.
    */
    refreshCustomer(): void {
        if (this.customers && this.customers.length > 0) {
            this.buildCustomerTree(this.customers);
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

    getCustomers(): void {
       this.customerService.getCustomers()
            .subscribe((customers: Array<Customer>): void => {
                if (customers && customers.length) {
                    this.customers = customers;
                    this.buildCustomerTree(customers);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildCustomerTree method
	// @param {Array<Customer>} activeCustomers - activeCustomers method param.
	// @returns {void} - buildCustomerTree method returns void.
    */
    buildCustomerTree(activeCustomers: Array<Customer>): void {
        const customers: Array<TreeNode> = this.getCustomerNode(activeCustomers);
        this.manageCustomer.data = customers;
        this.manageCustomer.data = [...this.customSortMultipleColumns(this.manageCustomer.data, this.sortCols)];
    }

    /**
	// Represents getCustomerNode method
	// @param {Array<Customer>} arr - arr method param.
	// @returns {Array<TreeNode>} - getCustomerNode method returns Array<TreeNode>.
    */
    getCustomerNode(arr: Array<Customer>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((customer: Customer): void => {
            const innerNode: TreeNode = {
                data: customer
            };
            innerNode.data.CustomerStatusUIdValue = this.getDetailsForUId(this.customerStatuses, customer.CustomerStatusUId, 'MasterListItemUId', 'Name');
                        
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
        this.searchCustomer = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addCustomer(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomer'],
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
	// @param {TreeNode<Customer>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<Customer>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.CustomerUId, this.manageCustomer.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToCustomer method
	// @param {Customer} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToCustomer method returns void.
    */
    navigateToCustomer(nodeData: Customer, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomer'],
                {
                    queryParams: {
                        action,
                        customerUId: nodeData.CustomerUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomer'],
            {
                queryParams: {
                    action: 'Edit',
                    customerUId: this.selectedDetailNode.CustomerUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} customerUId - customerUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(customerUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.CustomerUId === customerUId))[0].data;
        }
    }
}
