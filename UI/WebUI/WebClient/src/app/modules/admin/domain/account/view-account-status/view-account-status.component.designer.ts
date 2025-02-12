/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, ViewChild, TemplateRef } from '@angular/core';
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
import { AccountStatus } from '../../../../../shared/models/domain/account/account-status.model';
import { AccountStatusService } from '../../../../../core/services/domain/account/account-status.service';
import { NgClass } from '@angular/common';

// Define the ViewAccountStatusComponent
@Component({
    // Selector for the component
    selector: 'app-view-account-status',
    // Template URL for the component
    templateUrl: './view-account-status.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewAccountStatusComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewAccountStatusesTable', { static: false }) viewAccountStatusesTable!: TreeTable;  
    manageAccountStatus: CustomTreeTableModel<TreeNode<AccountStatus>> = new CustomTreeTableModel<TreeNode<AccountStatus>>({
        data: new Array<TreeNode<AccountStatus>>()
    });
    selectedDetailNode: AccountStatus = {} as AccountStatus;
    sortCols!: Array<CustomSortModel>;
    accountStatuses!: Array<AccountStatus>;
    accountStatusToDelete!: AccountStatus;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageAccountStatusData!: Array<AccountStatus>;
    screenGroup!: string;
    searchAccountStatus!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly accountStatusService: AccountStatusService,
        override readonly coreSessionService: CoreSessionService,
        readonly coreSubscriptionService: CoreSubscriptionService,
        readonly modalService: NgbModal, 
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
                 { field: 'Name', header: 'Name', class: 'text-left' },
                 { field: 'Code', header: 'Code', class: 'text-left' },
                 { field: 'Description', header: 'Description', class: 'text-left' },
                 { field: 'DisplayOrder', header: 'DisplayOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'Code', type: 'string' },
                 { field: 'Description', type: 'string' },
                 { field: 'DisplayOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getAccountStatuses();
      
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
	// Represents refreshAccountStatus method
    // Method to refresh grid data.
	// @returns {void} - refreshAccountStatus method returns void.
    */
    refreshAccountStatus(): void {
        if (this.accountStatuses && this.accountStatuses.length > 0) {
            this.buildAccountStatusTree(this.accountStatuses);
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

    getAccountStatuses(): void {
       this.accountStatusService.getAccountStatuses()
            .subscribe((accountStatuses: Array<AccountStatus>): void => {
                if (accountStatuses && accountStatuses.length) {
                    this.accountStatuses = accountStatuses;
                    this.buildAccountStatusTree(accountStatuses);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildAccountStatusTree method
	// @param {Array<AccountStatus>} activeAccountStatuses - activeAccountStatuses method param.
	// @returns {void} - buildAccountStatusTree method returns void.
    */
    buildAccountStatusTree(activeAccountStatuses: Array<AccountStatus>): void {
        const accountStatuses: Array<TreeNode> = this.getAccountStatusNode(activeAccountStatuses);
        this.manageAccountStatus.data = accountStatuses;
        this.manageAccountStatus.data = [...this.customSortMultipleColumns(this.manageAccountStatus.data, this.sortCols)];
    }

    /**
	// Represents getAccountStatusNode method
	// @param {Array<AccountStatus>} arr - arr method param.
	// @returns {Array<TreeNode>} - getAccountStatusNode method returns Array<TreeNode>.
    */
    getAccountStatusNode(arr: Array<AccountStatus>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((accountStatus: AccountStatus): void => {
            const innerNode: TreeNode = {
                data: accountStatus
            };
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
        this.searchAccountStatus = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addAccountStatus(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageAccountStatus'],
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
	// @param {TreeNode<AccountStatus>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<AccountStatus>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.AccountStatusUId, this.manageAccountStatus.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToAccountStatus method
	// @param {AccountStatus} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToAccountStatus method returns void.
    */
    navigateToAccountStatus(nodeData: AccountStatus, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageAccountStatus'],
                {
                    queryParams: {
                        action,
                        accountStatusUId: nodeData.AccountStatusUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageAccountStatus'],
            {
                queryParams: {
                    action: 'Edit',
                    accountStatusUId: this.selectedDetailNode.AccountStatusUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} accountStatusUId - accountStatusUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(accountStatusUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.AccountStatusUId === accountStatusUId))[0].data;
        }
    }
    /**
	// Represents deleteConfirmation method
	// @param {TemplateRef<string>} content - content method param.
	// @param {AccountStatus} rowData - rowData method param.
	// @returns void - deleteConfirmation method returns void.
    */
    deleteConfirmation(content: TemplateRef<string>, rowData: AccountStatus): void {
        if (rowData) {
            const accountStatusToDelete: TreeNode = this.manageAccountStatus.data.filter((x: TreeNode): boolean => x.data.AccountStatusUId === rowData.AccountStatusUId)[0];
            if (accountStatusToDelete && accountStatusToDelete.data) {
                 this.accountStatusToDelete = accountStatusToDelete.data;
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
    deleteAccountStatus(): void {
        const serviceParameterIndexValue: number = this.accountStatuses.findIndex((x: AccountStatus): boolean => x.AccountStatusUId === this.accountStatusToDelete.AccountStatusUId);
        if (this.accountStatuses && this.accountStatuses.length > 0) {
            this.accountStatuses[serviceParameterIndexValue].ItemState = ItemState.Deleted;
            this.accountStatuses[serviceParameterIndexValue].RowStatusUId = RowStatuses.Inactive;            
        }               
        this.accountStatusService.mergeAccountStatuses([this.accountStatuses[serviceParameterIndexValue]]).subscribe((): void => {
            let deleteSuccessfulMsg: string = '';
            this.translateService.get('DeleteSuccessMessage').subscribe((data: string): string => deleteSuccessfulMsg = data);
            CoreNotificationService.showSuccess(deleteSuccessfulMsg);
            this.getAccountStatuses();
        }, (error: HttpErrorResponse): void => {            
            CoreNotificationService.handleError(error);            
        });
        this.modalService.dismissAll();  
    }
}
