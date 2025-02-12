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
import { CustomerCard } from '../../../../../shared/models/domain/customer/customer-card.model';
import { CustomerCardService } from '../../../../../core/services/domain/customer/customer-card.service';
import { Customer } from '../../../../../shared/models/domain/customer/customer.model';
import { CustomerService } from '../../../../../core/services/domain/customer/customer.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { NgClass } from '@angular/common';

// Define the ViewCustomerCardComponent
@Component({
    // Selector for the component
    selector: 'app-view-customer-card',
    // Template URL for the component
    templateUrl: './view-customer-card.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewCustomerCardComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly customerCardService = inject(CustomerCardService);
    readonly customerService = inject(CustomerService);
    readonly masterListItemService = inject(MasterListItemService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewCustomerCardsTable = viewChild.required<TreeTable>('this.ViewCustomerCardsTable');  
    manageCustomerCard: CustomTreeTableModel<TreeNode<CustomerCard>> = new CustomTreeTableModel<TreeNode<CustomerCard>>({
        data: new Array<TreeNode<CustomerCard>>()
    });
    selectedDetailNode: CustomerCard = {} as CustomerCard;
    sortCols!: Array<CustomSortModel>;
    cardTypes!: Array<MasterListItem>;
    cols!: Array<TreeTableHeaderModel>;
    customerCards!: Array<CustomerCard>;
    customers!: Array<Customer>;
    doubleClicked!: boolean;
    manageCustomerCardData!: Array<CustomerCard>;
    screenGroup!: string;
    searchCustomerCard!: string;
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
                 { field: 'CardTypeUIdValue', header: 'CardType', class:'text-left' },          
                 { field: 'NameOnCard', header: 'NameOnCard', class: 'text-left' },
                 { field: 'Number', header: 'Number', class: 'text-left' },
                 { field: 'ExpirationDate', header: 'ExpirationDate', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'CustomerUIdValue', type: 'string' },
                 { field: 'CardTypeUIdValue', type: 'string' },
                 { field: 'NameOnCard', type: 'string' },
                 { field: 'Number', type: 'string' },
                 { field: 'ExpirationDate', type: 'string' },
        ];
        this.getMasterData();
        this.getCustomerCards();
      
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
           this.refreshCustomerCard();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('CardType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.cardTypes = masterListItems;
           this.refreshCustomerCard();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshCustomerCard method
    // Method to refresh grid data.
	// @returns {void} - refreshCustomerCard method returns void.
    */
    refreshCustomerCard(): void {
        if (this.customerCards && this.customerCards.length > 0) {
            this.buildCustomerCardTree(this.customerCards);
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

    getCustomerCards(): void {
       this.customerCardService.getCustomerCards()
            .subscribe((customerCards: Array<CustomerCard>): void => {
                if (customerCards && customerCards.length) {
                    this.customerCards = customerCards;
                    this.buildCustomerCardTree(customerCards);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildCustomerCardTree method
	// @param {Array<CustomerCard>} activeCustomerCards - activeCustomerCards method param.
	// @returns {void} - buildCustomerCardTree method returns void.
    */
    buildCustomerCardTree(activeCustomerCards: Array<CustomerCard>): void {
        const customerCards: Array<TreeNode> = this.getCustomerCardNode(activeCustomerCards);
        this.manageCustomerCard.data = customerCards;
        this.manageCustomerCard.data = [...this.customSortMultipleColumns(this.manageCustomerCard.data, this.sortCols)];
    }

    /**
	// Represents getCustomerCardNode method
	// @param {Array<CustomerCard>} arr - arr method param.
	// @returns {Array<TreeNode>} - getCustomerCardNode method returns Array<TreeNode>.
    */
    getCustomerCardNode(arr: Array<CustomerCard>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((customerCard: CustomerCard): void => {
            const innerNode: TreeNode = {
                data: customerCard
            };
            innerNode.data.CardTypeUIdValue = this.getDetailsForUId(this.cardTypes, customerCard.CardTypeUId, 'MasterListItemUId', 'Name');
                        
            innerNode.data.CustomerUIdValue = this.getDetailsForUId(this.customers, customerCard.CustomerUId, 'CustomerUId', 'Name');
                        
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
        this.searchCustomerCard = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addCustomerCard(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomerCard'],
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
	// @param {TreeNode<CustomerCard>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<CustomerCard>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.CustomerCardUId, this.manageCustomerCard.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToCustomerCard method
	// @param {CustomerCard} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToCustomerCard method returns void.
    */
    navigateToCustomerCard(nodeData: CustomerCard, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomerCard'],
                {
                    queryParams: {
                        action,
                        customerCardUId: nodeData.CustomerCardUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageCustomerCard'],
            {
                queryParams: {
                    action: 'Edit',
                    customerCardUId: this.selectedDetailNode.CustomerCardUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} customerCardUId - customerCardUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(customerCardUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.CustomerCardUId === customerCardUId))[0].data;
        }
    }
}
