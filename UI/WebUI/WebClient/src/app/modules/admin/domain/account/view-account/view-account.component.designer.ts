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
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
import { AccountStatus } from '../../../../../shared/models/domain/account/account-status.model';
import { AccountStatusService } from '../../../../../core/services/domain/account/account-status.service';
import { NgClass } from '@angular/common';

// Define the ViewAccountComponent
@Component({
    // Selector for the component
    selector: 'app-view-account',
    // Template URL for the component
    templateUrl: './view-account.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewAccountComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewAccountsTable', { static: false }) viewAccountsTable!: TreeTable;  
    manageAccount: CustomTreeTableModel<TreeNode<Account>> = new CustomTreeTableModel<TreeNode<Account>>({
        data: new Array<TreeNode<Account>>()
    });
    selectedDetailNode: Account = {} as Account;
    sortCols!: Array<CustomSortModel>;
    accounts!: Array<Account>;
    accountStatuses!: Array<AccountStatus>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageAccountData!: Array<Account>;
    screenGroup!: string;
    searchAccount!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly accountService: AccountService,
        readonly accountStatusService: AccountStatusService,
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
                 { field: 'EmailId', header: 'EmailId', class: 'text-left' },
                 { field: 'AccountStatusUIdValue', header: 'AccountStatus', class:'text-left' },          
                 { field: 'FirstName', header: 'FirstName', class: 'text-left' },
                 { field: 'LastName', header: 'LastName', class: 'text-left' },
                 { field: 'ImportSource', header: 'ImportSource', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'EmailId', type: 'string' },
                 { field: 'AccountStatusUIdValue', type: 'string' },
                 { field: 'FirstName', type: 'string' },
                 { field: 'LastName', type: 'string' },
                 { field: 'ImportSource', type: 'string' },
        ];
        this.getMasterData();
        this.getAccounts();
      
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
       this.accountStatusService.getAccountStatuses().subscribe((accountStatuses: Array<AccountStatus>): void => {
           this.accountStatuses = accountStatuses;
           this.refreshAccount();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshAccount method
    // Method to refresh grid data.
	// @returns {void} - refreshAccount method returns void.
    */
    refreshAccount(): void {
        if (this.accounts && this.accounts.length > 0) {
            this.buildAccountTree(this.accounts);
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

    getAccounts(): void {
       this.accountService.getAccounts()
            .subscribe((accounts: Array<Account>): void => {
                if (accounts && accounts.length) {
                    this.accounts = accounts;
                    this.buildAccountTree(accounts);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildAccountTree method
	// @param {Array<Account>} activeAccounts - activeAccounts method param.
	// @returns {void} - buildAccountTree method returns void.
    */
    buildAccountTree(activeAccounts: Array<Account>): void {
        const accounts: Array<TreeNode> = this.getAccountNode(activeAccounts);
        this.manageAccount.data = accounts;
        this.manageAccount.data = [...this.customSortMultipleColumns(this.manageAccount.data, this.sortCols)];
    }

    /**
	// Represents getAccountNode method
	// @param {Array<Account>} arr - arr method param.
	// @returns {Array<TreeNode>} - getAccountNode method returns Array<TreeNode>.
    */
    getAccountNode(arr: Array<Account>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((account: Account): void => {
            const innerNode: TreeNode = {
                data: account
            };
            innerNode.data.AccountStatusUIdValue = this.getDetailsForUId(this.accountStatuses, account.AccountStatusUId, 'AccountStatusUId', 'Name');
                        
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
        this.searchAccount = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addAccount(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageAccount'],
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
	// @param {TreeNode<Account>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<Account>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.AccountUId, this.manageAccount.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToAccount method
	// @param {Account} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToAccount method returns void.
    */
    navigateToAccount(nodeData: Account, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageAccount'],
                {
                    queryParams: {
                        action,
                        accountUId: nodeData.AccountUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageAccount'],
            {
                queryParams: {
                    action: 'Edit',
                    accountUId: this.selectedDetailNode.AccountUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} accountUId - accountUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(accountUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.AccountUId === accountUId))[0].data;
        }
    }
}
