/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
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
import { CustomerAddress } from '../../../../../shared/models/domain/customer/customer-address.model';
import { CustomerAddressService } from '../../../../../core/services/domain/customer/customer-address.service';
import { Customer } from '../../../../../shared/models/domain/customer/customer.model';
import { CustomerService } from '../../../../../core/services/domain/customer/customer.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { NgClass } from '@angular/common';

// Define the ViewCustomerAddressComponent
@Component({
    // Selector for the component
    selector: 'app-view-customer-address',
    // Template URL for the component
    templateUrl: './view-customer-address.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewCustomerAddressComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewCustomerAddressesTable', { static: false }) viewCustomerAddressesTable!: TreeTable;  
    manageCustomerAddress: CustomTreeTableModel<TreeNode<CustomerAddress>> = new CustomTreeTableModel<TreeNode<CustomerAddress>>({
        data: new Array<TreeNode<CustomerAddress>>()
    });
    selectedDetailNode: CustomerAddress = {} as CustomerAddress;
    sortCols!: Array<CustomSortModel>;
    addressTypes!: Array<MasterListItem>;
    cols!: Array<TreeTableHeaderModel>;
    customerAddresses!: Array<CustomerAddress>;
    customers!: Array<Customer>;
    doubleClicked!: boolean;
    manageCustomerAddressData!: Array<CustomerAddress>;
    screenGroup!: string;
    searchCustomerAddress!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly customerAddressService: CustomerAddressService,
        readonly customerService: CustomerService,
        readonly masterListItemService: MasterListItemService,
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
        this.cols = [
                 { field: 'CustomerUIdValue', header: 'Customer', class:'text-left' },          
                 { field: 'AddressTypeUIdValue', header: 'AddressType', class:'text-left' },          
                 { field: 'Line1', header: 'Line1', class: 'text-left' },
                 { field: 'Line2', header: 'Line2', class: 'text-left' },
                 { field: 'Line3', header: 'Line3', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'CustomerUIdValue', type: 'string' },
                 { field: 'AddressTypeUIdValue', type: 'string' },
                 { field: 'Line1', type: 'string' },
                 { field: 'Line2', type: 'string' },
                 { field: 'Line3', type: 'string' },
        ];
        this.getMasterData();
        this.getCustomerAddresses();
      
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
       this.customerService.getCustomers().subscribe((customers: Array<Customer>): void => {
           this.customers = customers;
           this.refreshCustomerAddress();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('AddressType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.addressTypes = masterListItems;
           this.refreshCustomerAddress();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshCustomerAddress method
    // Method to refresh grid data.
	// @returns {void} - refreshCustomerAddress method returns void.
    */
    refreshCustomerAddress(): void {
        if (this.customerAddresses && this.customerAddresses.length > 0) {
            this.buildCustomerAddressTree(this.customerAddresses);
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

    getCustomerAddresses(): void {
       this.customerAddressService.getCustomerAddresses()
            .subscribe((customerAddresses: Array<CustomerAddress>): void => {
                if (customerAddresses && customerAddresses.length) {
                    this.customerAddresses = customerAddresses;
                    this.buildCustomerAddressTree(customerAddresses);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildCustomerAddressTree method
	// @param {Array<CustomerAddress>} activeCustomerAddresses - activeCustomerAddresses method param.
	// @returns {void} - buildCustomerAddressTree method returns void.
    */
    buildCustomerAddressTree(activeCustomerAddresses: Array<CustomerAddress>): void {
        const customerAddresses: Array<TreeNode> = this.getCustomerAddressNode(activeCustomerAddresses);
        this.manageCustomerAddress.data = customerAddresses;
        this.manageCustomerAddress.data = [...this.customSortMultipleColumns(this.manageCustomerAddress.data, this.sortCols)];
    }

    /**
	// Represents getCustomerAddressNode method
	// @param {Array<CustomerAddress>} arr - arr method param.
	// @returns {Array<TreeNode>} - getCustomerAddressNode method returns Array<TreeNode>.
    */
    getCustomerAddressNode(arr: Array<CustomerAddress>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((customerAddress: CustomerAddress): void => {
            const innerNode: TreeNode = {
                data: customerAddress
            };
            innerNode.data.AddressTypeUIdValue = this.getDetailsForUId(this.addressTypes, customerAddress.AddressTypeUId, 'MasterListItemUId', 'Name');
                        
            innerNode.data.CustomerUIdValue = this.getDetailsForUId(this.customers, customerAddress.CustomerUId, 'CustomerUId', 'Name');
                        
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
        this.searchCustomerAddress = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addCustomerAddress(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomerAddress'],
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
	// @param {TreeNode<CustomerAddress>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<CustomerAddress>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.CustomerAddressUId, this.manageCustomerAddress.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToCustomerAddress method
	// @param {CustomerAddress} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToCustomerAddress method returns void.
    */
    navigateToCustomerAddress(nodeData: CustomerAddress, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomerAddress'],
                {
                    queryParams: {
                        action,
                        customerAddressUId: nodeData.CustomerAddressUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomerAddress'],
            {
                queryParams: {
                    action: 'Edit',
                    customerAddressUId: this.selectedDetailNode.CustomerAddressUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} customerAddressUId - customerAddressUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(customerAddressUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.CustomerAddressUId === customerAddressUId))[0].data;
        }
    }
}
