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
import { SellerBankAccount } from '../../../../../shared/models/domain/seller/seller-bank-account.model';
import { SellerBankAccountService } from '../../../../../core/services/domain/seller/seller-bank-account.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { Seller } from '../../../../../shared/models/domain/seller/seller.model';
import { SellerService } from '../../../../../core/services/domain/seller/seller.service';
import { NgClass } from '@angular/common';

// Define the ViewSellerBankAccountComponent
@Component({
    // Selector for the component
    selector: 'app-view-seller-bank-account',
    // Template URL for the component
    templateUrl: './view-seller-bank-account.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewSellerBankAccountComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly masterListItemService = inject(MasterListItemService);
    readonly sellerBankAccountService = inject(SellerBankAccountService);
    readonly sellerService = inject(SellerService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    @ViewChild('this.ViewSellerBankAccountsTable', { static: false }) viewSellerBankAccountsTable!: TreeTable;  
    manageSellerBankAccount: CustomTreeTableModel<TreeNode<SellerBankAccount>> = new CustomTreeTableModel<TreeNode<SellerBankAccount>>({
        data: new Array<TreeNode<SellerBankAccount>>()
    });
    selectedDetailNode: SellerBankAccount = {} as SellerBankAccount;
    sortCols!: Array<CustomSortModel>;
    bankAccountTypes!: Array<MasterListItem>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageSellerBankAccountData!: Array<SellerBankAccount>;
    screenGroup!: string;
    searchSellerBankAccount!: string;
    selectedNode!: TreeTableNode | null;
    sellerBankAccounts!: Array<SellerBankAccount>;
    sellers!: Array<Seller>;
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
                 { field: 'SellerUIdValue', header: 'Seller', class:'text-left' },          
                 { field: 'BankAccountTypeUIdValue', header: 'BankAccountType', class:'text-left' },          
                 { field: 'NameOnAccount', header: 'NameOnAccount', class: 'text-left' },
                 { field: 'Number', header: 'Number', class: 'text-left' },
                 { field: 'RoutingNumber', header: 'RoutingNumber', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'SellerUIdValue', type: 'string' },
                 { field: 'BankAccountTypeUIdValue', type: 'string' },
                 { field: 'NameOnAccount', type: 'string' },
                 { field: 'Number', type: 'string' },
                 { field: 'RoutingNumber', type: 'string' },
        ];
        this.getMasterData();
        this.getSellerBankAccounts();
      
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
       this.sellerService.getSellers().subscribe((sellers: Array<Seller>): void => {
           this.sellers = sellers;
           this.refreshSellerBankAccount();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('BankAccountType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.bankAccountTypes = masterListItems;
           this.refreshSellerBankAccount();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshSellerBankAccount method
    // Method to refresh grid data.
	// @returns {void} - refreshSellerBankAccount method returns void.
    */
    refreshSellerBankAccount(): void {
        if (this.sellerBankAccounts && this.sellerBankAccounts.length > 0) {
            this.buildSellerBankAccountTree(this.sellerBankAccounts);
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

    getSellerBankAccounts(): void {
       this.sellerBankAccountService.getSellerBankAccounts()
            .subscribe((sellerBankAccounts: Array<SellerBankAccount>): void => {
                if (sellerBankAccounts && sellerBankAccounts.length) {
                    this.sellerBankAccounts = sellerBankAccounts;
                    this.buildSellerBankAccountTree(sellerBankAccounts);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildSellerBankAccountTree method
	// @param {Array<SellerBankAccount>} activeSellerBankAccounts - activeSellerBankAccounts method param.
	// @returns {void} - buildSellerBankAccountTree method returns void.
    */
    buildSellerBankAccountTree(activeSellerBankAccounts: Array<SellerBankAccount>): void {
        const sellerBankAccounts: Array<TreeNode> = this.getSellerBankAccountNode(activeSellerBankAccounts);
        this.manageSellerBankAccount.data = sellerBankAccounts;
        this.manageSellerBankAccount.data = [...this.customSortMultipleColumns(this.manageSellerBankAccount.data, this.sortCols)];
    }

    /**
	// Represents getSellerBankAccountNode method
	// @param {Array<SellerBankAccount>} arr - arr method param.
	// @returns {Array<TreeNode>} - getSellerBankAccountNode method returns Array<TreeNode>.
    */
    getSellerBankAccountNode(arr: Array<SellerBankAccount>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((sellerBankAccount: SellerBankAccount): void => {
            const innerNode: TreeNode = {
                data: sellerBankAccount
            };
            innerNode.data.BankAccountTypeUIdValue = this.getDetailsForUId(this.bankAccountTypes, sellerBankAccount.BankAccountTypeUId, 'MasterListItemUId', 'Name');
                        
            innerNode.data.SellerUIdValue = this.getDetailsForUId(this.sellers, sellerBankAccount.SellerUId, 'SellerUId', 'Name');
                        
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
        this.searchSellerBankAccount = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addSellerBankAccount(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSellerBankAccount'],
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
	// @param {TreeNode<SellerBankAccount>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<SellerBankAccount>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.SellerBankAccountUId, this.manageSellerBankAccount.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToSellerBankAccount method
	// @param {SellerBankAccount} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToSellerBankAccount method returns void.
    */
    navigateToSellerBankAccount(nodeData: SellerBankAccount, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSellerBankAccount'],
                {
                    queryParams: {
                        action,
                        sellerBankAccountUId: nodeData.SellerBankAccountUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSellerBankAccount'],
            {
                queryParams: {
                    action: 'Edit',
                    sellerBankAccountUId: this.selectedDetailNode.SellerBankAccountUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} sellerBankAccountUId - sellerBankAccountUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(sellerBankAccountUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.SellerBankAccountUId === sellerBankAccountUId))[0].data;
        }
    }
}
