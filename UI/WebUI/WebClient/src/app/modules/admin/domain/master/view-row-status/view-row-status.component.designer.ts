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
import { RowStatus } from '../../../../../shared/models/domain/master/row-status.model';
import { RowStatusService } from '../../../../../core/services/domain/master/row-status.service';
import { NgClass } from '@angular/common';

// Define the ViewRowStatusComponent
@Component({
    // Selector for the component
    selector: 'app-view-row-status',
    // Template URL for the component
    templateUrl: './view-row-status.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewRowStatusComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly rowStatusService = inject(RowStatusService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly modalService = inject(NgbModal);
    private readonly translateService = inject(TranslateService);

    readonly viewRowStatusesTable = viewChild.required<TreeTable>('this.ViewRowStatusesTable');  
    manageRowStatus: CustomTreeTableModel<TreeNode<RowStatus>> = new CustomTreeTableModel<TreeNode<RowStatus>>({
        data: new Array<TreeNode<RowStatus>>()
    });
    selectedDetailNode: RowStatus = {} as RowStatus;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageRowStatusData!: Array<RowStatus>;
    rowStatuses!: Array<RowStatus>;
    rowStatusToDelete!: RowStatus;
    screenGroup!: string;
    searchRowStatus!: string;
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
        this.getRowStatuses();
      
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
	// Represents refreshRowStatus method
    // Method to refresh grid data.
	// @returns {void} - refreshRowStatus method returns void.
    */
    refreshRowStatus(): void {
        if (this.rowStatuses && this.rowStatuses.length > 0) {
            this.buildRowStatusTree(this.rowStatuses);
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

    getRowStatuses(): void {
       this.rowStatusService.getRowStatuses()
            .subscribe((rowStatuses: Array<RowStatus>): void => {
                if (rowStatuses && rowStatuses.length) {
                    this.rowStatuses = rowStatuses;
                    this.buildRowStatusTree(rowStatuses);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildRowStatusTree method
	// @param {Array<RowStatus>} activeRowStatuses - activeRowStatuses method param.
	// @returns {void} - buildRowStatusTree method returns void.
    */
    buildRowStatusTree(activeRowStatuses: Array<RowStatus>): void {
        const rowStatuses: Array<TreeNode> = this.getRowStatusNode(activeRowStatuses);
        this.manageRowStatus.data = rowStatuses;
        this.manageRowStatus.data = [...this.customSortMultipleColumns(this.manageRowStatus.data, this.sortCols)];
    }

    /**
	// Represents getRowStatusNode method
	// @param {Array<RowStatus>} arr - arr method param.
	// @returns {Array<TreeNode>} - getRowStatusNode method returns Array<TreeNode>.
    */
    getRowStatusNode(arr: Array<RowStatus>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((rowStatus: RowStatus): void => {
            const innerNode: TreeNode = {
                data: rowStatus
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
        this.searchRowStatus = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addRowStatus(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageRowStatus'],
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
	// @param {TreeNode<RowStatus>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<RowStatus>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.RowStatusUId, this.manageRowStatus.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToRowStatus method
	// @param {string} action - action method param.
	// @param {RowStatus} nodeData - nodeData method param.
	// @returns {void} - navigateToRowStatus method returns void.
    */
    navigateToRowStatus(nodeData: RowStatus, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageRowStatus'],
                {
                    queryParams: {
                        action,
                        rowStatusUId: nodeData.RowStatusUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageRowStatus'],
            {
                queryParams: {
                    action: 'Edit',
                    rowStatusUId: this.selectedDetailNode.RowStatusUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} rowStatusUId - rowStatusUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(rowStatusUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.RowStatusUId === rowStatusUId))[0].data;
        }
    }
    /**
	// Represents deleteConfirmation method
	// @param {TemplateRef<string>} content - content method param.
	// @param {RowStatus} rowData - rowData method param.
	// @returns void - deleteConfirmation method returns void.
    */
    deleteConfirmation(content: TemplateRef<string>, rowData: RowStatus): void {
        if (rowData) {
            const rowStatusToDelete: TreeNode = this.manageRowStatus.data.filter((x: TreeNode): boolean => x.data.RowStatusUId === rowData.RowStatusUId)[0];
            if (rowStatusToDelete && rowStatusToDelete.data) {
                 this.rowStatusToDelete = rowStatusToDelete.data;
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
    deleteRowStatus(): void {
        const serviceParameterIndexValue: number = this.rowStatuses.findIndex((x: RowStatus): boolean => x.RowStatusUId === this.rowStatusToDelete.RowStatusUId);
        if (this.rowStatuses && this.rowStatuses.length > 0) {
            this.rowStatuses[serviceParameterIndexValue].ItemState = ItemState.Deleted;
            this.rowStatuses[serviceParameterIndexValue].RowStatusUId = RowStatuses.Inactive;            
        }               
        this.rowStatusService.mergeRowStatuses([this.rowStatuses[serviceParameterIndexValue]]).subscribe((): void => {
            let deleteSuccessfulMsg: string = '';
            this.translateService.get('DeleteSuccessMessage').subscribe((data: string): string => deleteSuccessfulMsg = data);
            CoreNotificationService.showSuccess(deleteSuccessfulMsg);
            this.getRowStatuses();
        }, (error: HttpErrorResponse): void => {            
            CoreNotificationService.handleError(error);            
        });
        this.modalService.dismissAll();  
    }
}
