/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, ViewChild, TemplateRef, inject } from '@angular/core';
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
import { MasterList } from '../../../../../shared/models/domain/master/master-list.model';
import { MasterListService } from '../../../../../core/services/domain/master/master-list.service';
import { NgClass } from '@angular/common';

// Define the ViewMasterListComponent
@Component({
    // Selector for the component
    selector: 'app-view-master-list',
    // Template URL for the component
    templateUrl: './view-master-list.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewMasterListComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly masterListService = inject(MasterListService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly modalService = inject(NgbModal);
    private readonly translateService = inject(TranslateService);

    @ViewChild('this.ViewMasterListsTable', { static: false }) viewMasterListsTable!: TreeTable;  
    manageMasterList: CustomTreeTableModel<TreeNode<MasterList>> = new CustomTreeTableModel<TreeNode<MasterList>>({
        data: new Array<TreeNode<MasterList>>()
    });
    selectedDetailNode: MasterList = {} as MasterList;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageMasterListData!: Array<MasterList>;
    masterLists!: Array<MasterList>;
    masterListToDelete!: MasterList;
    screenGroup!: string;
    searchMasterList!: string;
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
                 { field: 'Source', header: 'Source', class: 'text-left' },
                 { field: 'ImageUrl', header: 'ImageUrl', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'Code', type: 'string' },
                 { field: 'Description', type: 'string' },
                 { field: 'Source', type: 'string' },
                 { field: 'ImageUrl', type: 'string' },
        ];
        this.getMasterData();
        this.getMasterLists();
      
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
	// Represents refreshMasterList method
    // Method to refresh grid data.
	// @returns {void} - refreshMasterList method returns void.
    */
    refreshMasterList(): void {
        if (this.masterLists && this.masterLists.length > 0) {
            this.buildMasterListTree(this.masterLists);
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

    getMasterLists(): void {
       this.masterListService.getMasterLists()
            .subscribe((masterLists: Array<MasterList>): void => {
                if (masterLists && masterLists.length) {
                    this.masterLists = masterLists;
                    this.buildMasterListTree(masterLists);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildMasterListTree method
	// @param {Array<MasterList>} activeMasterLists - activeMasterLists method param.
	// @returns {void} - buildMasterListTree method returns void.
    */
    buildMasterListTree(activeMasterLists: Array<MasterList>): void {
        const masterLists: Array<TreeNode> = this.getMasterListNode(activeMasterLists);
        this.manageMasterList.data = masterLists;
        this.manageMasterList.data = [...this.customSortMultipleColumns(this.manageMasterList.data, this.sortCols)];
    }

    /**
	// Represents getMasterListNode method
	// @param {Array<MasterList>} arr - arr method param.
	// @returns {Array<TreeNode>} - getMasterListNode method returns Array<TreeNode>.
    */
    getMasterListNode(arr: Array<MasterList>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((masterList: MasterList): void => {
            const innerNode: TreeNode = {
                data: masterList
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
        this.searchMasterList = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addMasterList(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageMasterList'],
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
	// @param {TreeNode<MasterList>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<MasterList>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.MasterListUId, this.manageMasterList.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToMasterList method
	// @param {string} action - action method param.
	// @param {MasterList} nodeData - nodeData method param.
	// @returns {void} - navigateToMasterList method returns void.
    */
    navigateToMasterList(nodeData: MasterList, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageMasterList'],
                {
                    queryParams: {
                        action,
                        masterListUId: nodeData.MasterListUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageMasterList'],
            {
                queryParams: {
                    action: 'Edit',
                    masterListUId: this.selectedDetailNode.MasterListUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} masterListUId - masterListUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(masterListUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.MasterListUId === masterListUId))[0].data;
        }
    }
    /**
	// Represents deleteConfirmation method
	// @param {TemplateRef<string>} content - content method param.
	// @param {MasterList} rowData - rowData method param.
	// @returns void - deleteConfirmation method returns void.
    */
    deleteConfirmation(content: TemplateRef<string>, rowData: MasterList): void {
        if (rowData) {
            const masterListToDelete: TreeNode = this.manageMasterList.data.filter((x: TreeNode): boolean => x.data.MasterListUId === rowData.MasterListUId)[0];
            if (masterListToDelete && masterListToDelete.data) {
                 this.masterListToDelete = masterListToDelete.data;
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
    deleteMasterList(): void {
        const serviceParameterIndexValue: number = this.masterLists.findIndex((x: MasterList): boolean => x.MasterListUId === this.masterListToDelete.MasterListUId);
        if (this.masterLists && this.masterLists.length > 0) {
            this.masterLists[serviceParameterIndexValue].ItemState = ItemState.Deleted;
            this.masterLists[serviceParameterIndexValue].RowStatusUId = RowStatuses.Inactive;            
        }               
        this.masterListService.mergeMasterLists([this.masterLists[serviceParameterIndexValue]]).subscribe((): void => {
            let deleteSuccessfulMsg: string = '';
            this.translateService.get('DeleteSuccessMessage').subscribe((data: string): string => deleteSuccessfulMsg = data);
            CoreNotificationService.showSuccess(deleteSuccessfulMsg);
            this.getMasterLists();
        }, (error: HttpErrorResponse): void => {            
            CoreNotificationService.handleError(error);            
        });
        this.modalService.dismissAll();  
    }
}
