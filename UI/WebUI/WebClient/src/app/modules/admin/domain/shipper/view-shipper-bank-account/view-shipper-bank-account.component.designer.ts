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
import { ShipperBankAccount } from '../../../../../shared/models/domain/shipper/shipper-bank-account.model';
import { ShipperBankAccountService } from '../../../../../core/services/domain/shipper/shipper-bank-account.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { Shipper } from '../../../../../shared/models/domain/shipper/shipper.model';
import { ShipperService } from '../../../../../core/services/domain/shipper/shipper.service';
import { NgClass } from '@angular/common';

// Define the ViewShipperBankAccountComponent
@Component({
    // Selector for the component
    selector: 'app-view-shipper-bank-account',
    // Template URL for the component
    templateUrl: './view-shipper-bank-account.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewShipperBankAccountComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewShipperBankAccountsTable', { static: false }) viewShipperBankAccountsTable!: TreeTable;  
    manageShipperBankAccount: CustomTreeTableModel<TreeNode<ShipperBankAccount>> = new CustomTreeTableModel<TreeNode<ShipperBankAccount>>({
        data: new Array<TreeNode<ShipperBankAccount>>()
    });
    selectedDetailNode: ShipperBankAccount = {} as ShipperBankAccount;
    sortCols!: Array<CustomSortModel>;
    bankAccountTypes!: Array<MasterListItem>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageShipperBankAccountData!: Array<ShipperBankAccount>;
    screenGroup!: string;
    searchShipperBankAccount!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    shipperBankAccounts!: Array<ShipperBankAccount>;
    shippers!: Array<Shipper>;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly masterListItemService: MasterListItemService,
        readonly shipperBankAccountService: ShipperBankAccountService,
        readonly shipperService: ShipperService,
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
                 { field: 'ShipperUIdValue', header: 'Shipper', class:'text-left' },          
                 { field: 'BankAccountTypeUIdValue', header: 'BankAccountType', class:'text-left' },          
                 { field: 'NameOnAccount', header: 'NameOnAccount', class: 'text-left' },
                 { field: 'Number', header: 'Number', class: 'text-left' },
                 { field: 'RoutingNumber', header: 'RoutingNumber', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'ShipperUIdValue', type: 'string' },
                 { field: 'BankAccountTypeUIdValue', type: 'string' },
                 { field: 'NameOnAccount', type: 'string' },
                 { field: 'Number', type: 'string' },
                 { field: 'RoutingNumber', type: 'string' },
        ];
        this.getMasterData();
        this.getShipperBankAccounts();
      
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
       this.shipperService.getShippers().subscribe((shippers: Array<Shipper>): void => {
           this.shippers = shippers;
           this.refreshShipperBankAccount();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('BankAccountType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.bankAccountTypes = masterListItems;
           this.refreshShipperBankAccount();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshShipperBankAccount method
    // Method to refresh grid data.
	// @returns {void} - refreshShipperBankAccount method returns void.
    */
    refreshShipperBankAccount(): void {
        if (this.shipperBankAccounts && this.shipperBankAccounts.length > 0) {
            this.buildShipperBankAccountTree(this.shipperBankAccounts);
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

    getShipperBankAccounts(): void {
       this.shipperBankAccountService.getShipperBankAccounts()
            .subscribe((shipperBankAccounts: Array<ShipperBankAccount>): void => {
                if (shipperBankAccounts && shipperBankAccounts.length) {
                    this.shipperBankAccounts = shipperBankAccounts;
                    this.buildShipperBankAccountTree(shipperBankAccounts);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildShipperBankAccountTree method
	// @param {Array<ShipperBankAccount>} activeShipperBankAccounts - activeShipperBankAccounts method param.
	// @returns {void} - buildShipperBankAccountTree method returns void.
    */
    buildShipperBankAccountTree(activeShipperBankAccounts: Array<ShipperBankAccount>): void {
        const shipperBankAccounts: Array<TreeNode> = this.getShipperBankAccountNode(activeShipperBankAccounts);
        this.manageShipperBankAccount.data = shipperBankAccounts;
        this.manageShipperBankAccount.data = [...this.customSortMultipleColumns(this.manageShipperBankAccount.data, this.sortCols)];
    }

    /**
	// Represents getShipperBankAccountNode method
	// @param {Array<ShipperBankAccount>} arr - arr method param.
	// @returns {Array<TreeNode>} - getShipperBankAccountNode method returns Array<TreeNode>.
    */
    getShipperBankAccountNode(arr: Array<ShipperBankAccount>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((shipperBankAccount: ShipperBankAccount): void => {
            const innerNode: TreeNode = {
                data: shipperBankAccount
            };
            innerNode.data.BankAccountTypeUIdValue = this.getDetailsForUId(this.bankAccountTypes, shipperBankAccount.BankAccountTypeUId, 'MasterListItemUId', 'Name');
                        
            innerNode.data.ShipperUIdValue = this.getDetailsForUId(this.shippers, shipperBankAccount.ShipperUId, 'ShipperUId', 'Name');
                        
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
        this.searchShipperBankAccount = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addShipperBankAccount(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipperBankAccount'],
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
	// @param {TreeNode<ShipperBankAccount>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<ShipperBankAccount>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ShipperBankAccountUId, this.manageShipperBankAccount.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToShipperBankAccount method
	// @param {string} action - action method param.
	// @param {ShipperBankAccount} nodeData - nodeData method param.
	// @returns {void} - navigateToShipperBankAccount method returns void.
    */
    navigateToShipperBankAccount(nodeData: ShipperBankAccount, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipperBankAccount'],
                {
                    queryParams: {
                        action,
                        shipperBankAccountUId: nodeData.ShipperBankAccountUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipperBankAccount'],
            {
                queryParams: {
                    action: 'Edit',
                    shipperBankAccountUId: this.selectedDetailNode.ShipperBankAccountUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} shipperBankAccountUId - shipperBankAccountUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(shipperBankAccountUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ShipperBankAccountUId === shipperBankAccountUId))[0].data;
        }
    }
}
