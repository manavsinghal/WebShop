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
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { MasterList } from '../../../../../shared/models/domain/master/master-list.model';
import { MasterListService } from '../../../../../core/services/domain/master/master-list.service';
import { NgClass } from '@angular/common';

// Define the ViewMasterListItemComponent
@Component({
    // Selector for the component
    selector: 'app-view-master-list-item',
    // Template URL for the component
    templateUrl: './view-master-list-item.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewMasterListItemComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewMasterListItemsTable', { static: false }) viewMasterListItemsTable!: TreeTable;  
    manageMasterListItem: CustomTreeTableModel<TreeNode<MasterListItem>> = new CustomTreeTableModel<TreeNode<MasterListItem>>({
        data: new Array<TreeNode<MasterListItem>>()
    });
    selectedDetailNode: MasterListItem = {} as MasterListItem;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageMasterListItemData!: Array<MasterListItem>;
    masterListItems!: Array<MasterListItem>;
    masterListItemToDelete!: MasterListItem;
    masterLists!: Array<MasterList>;
    screenGroup!: string;
    searchMasterListItem!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly masterListItemService: MasterListItemService,
        readonly masterListService: MasterListService,
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
                 { field: 'MasterListUIdValue', header: 'MasterList', class:'text-left' },          
                 { field: 'DisplayOrder', header: 'DisplayOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'Code', type: 'string' },
                 { field: 'Description', type: 'string' },
                 { field: 'MasterListUIdValue', type: 'string' },
                 { field: 'DisplayOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getMasterListItems();
      
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
       this.masterListService.getMasterLists().subscribe((masterLists: Array<MasterList>): void => {
           this.masterLists = masterLists;
           this.refreshMasterListItem();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshMasterListItem method
    // Method to refresh grid data.
	// @returns {void} - refreshMasterListItem method returns void.
    */
    refreshMasterListItem(): void {
        if (this.masterListItems && this.masterListItems.length > 0) {
            this.buildMasterListItemTree(this.masterListItems);
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

    getMasterListItems(): void {
       this.masterListItemService.getMasterListItems()
            .subscribe((masterListItems: Array<MasterListItem>): void => {
                if (masterListItems && masterListItems.length) {
                    this.masterListItems = masterListItems;
                    this.buildMasterListItemTree(masterListItems);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildMasterListItemTree method
	// @param {Array<MasterListItem>} activeMasterListItems - activeMasterListItems method param.
	// @returns {void} - buildMasterListItemTree method returns void.
    */
    buildMasterListItemTree(activeMasterListItems: Array<MasterListItem>): void {
        const masterListItems: Array<TreeNode> = this.getMasterListItemNode(activeMasterListItems);
        this.manageMasterListItem.data = masterListItems;
        this.manageMasterListItem.data = [...this.customSortMultipleColumns(this.manageMasterListItem.data, this.sortCols)];
    }

    /**
	// Represents getMasterListItemNode method
	// @param {Array<MasterListItem>} arr - arr method param.
	// @returns {Array<TreeNode>} - getMasterListItemNode method returns Array<TreeNode>.
    */
    getMasterListItemNode(arr: Array<MasterListItem>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((masterListItem: MasterListItem): void => {
            const innerNode: TreeNode = {
                data: masterListItem
            };
            innerNode.data.MasterListUIdValue = this.getDetailsForUId(this.masterLists, masterListItem.MasterListUId, 'MasterListUId', 'Name');
                        
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
        this.searchMasterListItem = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addMasterListItem(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageMasterListItem'],
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
	// @param {TreeNode<MasterListItem>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<MasterListItem>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.MasterListItemUId, this.manageMasterListItem.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToMasterListItem method
	// @param {MasterListItem} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToMasterListItem method returns void.
    */
    navigateToMasterListItem(nodeData: MasterListItem, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageMasterListItem'],
                {
                    queryParams: {
                        action,
                        masterListItemUId: nodeData.MasterListItemUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageMasterListItem'],
            {
                queryParams: {
                    action: 'Edit',
                    masterListItemUId: this.selectedDetailNode.MasterListItemUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} masterListItemUId - masterListItemUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(masterListItemUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.MasterListItemUId === masterListItemUId))[0].data;
        }
    }
    /**
	// Represents deleteConfirmation method
	// @param {TemplateRef<string>} content - content method param.
	// @param {MasterListItem} rowData - rowData method param.
	// @returns void - deleteConfirmation method returns void.
    */
    deleteConfirmation(content: TemplateRef<string>, rowData: MasterListItem): void {
        if (rowData) {
            const masterListItemToDelete: TreeNode = this.manageMasterListItem.data.filter((x: TreeNode): boolean => x.data.MasterListItemUId === rowData.MasterListItemUId)[0];
            if (masterListItemToDelete && masterListItemToDelete.data) {
                 this.masterListItemToDelete = masterListItemToDelete.data;
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
    deleteMasterListItem(): void {
        const serviceParameterIndexValue: number = this.masterListItems.findIndex((x: MasterListItem): boolean => x.MasterListItemUId === this.masterListItemToDelete.MasterListItemUId);
        if (this.masterListItems && this.masterListItems.length > 0) {
            this.masterListItems[serviceParameterIndexValue].ItemState = ItemState.Deleted;
            this.masterListItems[serviceParameterIndexValue].RowStatusUId = RowStatuses.Inactive;            
        }               
        this.masterListItemService.mergeMasterListItems([this.masterListItems[serviceParameterIndexValue]]).subscribe((): void => {
            let deleteSuccessfulMsg: string = '';
            this.translateService.get('DeleteSuccessMessage').subscribe((data: string): string => deleteSuccessfulMsg = data);
            CoreNotificationService.showSuccess(deleteSuccessfulMsg);
            this.getMasterListItems();
        }, (error: HttpErrorResponse): void => {            
            CoreNotificationService.handleError(error);            
        });
        this.modalService.dismissAll();  
    }
}
