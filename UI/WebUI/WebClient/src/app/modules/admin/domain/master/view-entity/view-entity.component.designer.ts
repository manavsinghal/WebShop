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
import { Entity } from '../../../../../shared/models/domain/master/entity.model';
import { EntityService } from '../../../../../core/services/domain/master/entity.service';
import { NgClass } from '@angular/common';

// Define the ViewEntityComponent
@Component({
    // Selector for the component
    selector: 'app-view-entity',
    // Template URL for the component
    templateUrl: './view-entity.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewEntityComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly entityService = inject(EntityService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly modalService = inject(NgbModal);
    private readonly translateService = inject(TranslateService);

    readonly viewEntitiesTable = viewChild.required<TreeTable>('this.ViewEntitiesTable');  
    manageEntity: CustomTreeTableModel<TreeNode<Entity>> = new CustomTreeTableModel<TreeNode<Entity>>({
        data: new Array<TreeNode<Entity>>()
    });
    selectedDetailNode: Entity = {} as Entity;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    entities!: Array<Entity>;
    entityParents!: Array<Entity>;
    entityToDelete!: Entity;
    manageEntityData!: Array<Entity>;
    screenGroup!: string;
    searchEntity!: string;
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
                 { field: 'EntityParentUIdValue', header: 'EntityParent', class:'text-left' },          
                 { field: 'Name', header: 'Name', class: 'text-left' },
                 { field: 'Code', header: 'Code', class: 'text-left' },
                 { field: 'Description', header: 'Description', class: 'text-left' },
                 { field: 'DisplayOrder', header: 'DisplayOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'EntityParentUIdValue', type: 'string' },
                 { field: 'Name', type: 'string' },
                 { field: 'Code', type: 'string' },
                 { field: 'Description', type: 'string' },
                 { field: 'DisplayOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getEntities();
      
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
       this.entityService.getEntities().subscribe((entities: Array<Entity>): void => {
           this.entityParents = entities;
           this.refreshEntity();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshEntity method
    // Method to refresh grid data.
	// @returns {void} - refreshEntity method returns void.
    */
    refreshEntity(): void {
        if (this.entities && this.entities.length > 0) {
            this.buildEntityTree(this.entities);
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

    getEntities(): void {
       this.entityService.getEntities()
            .subscribe((entities: Array<Entity>): void => {
                if (entities && entities.length) {
                    this.entities = entities;
                    this.buildEntityTree(entities);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildEntityTree method
	// @param {Array<Entity>} activeEntities - activeEntities method param.
	// @returns {void} - buildEntityTree method returns void.
    */
    buildEntityTree(activeEntities: Array<Entity>): void {
        const entities: Array<TreeNode> = this.getEntityNode(activeEntities);
        this.manageEntity.data = entities;
        this.manageEntity.data = [...this.customSortMultipleColumns(this.manageEntity.data, this.sortCols)];
    }

    /**
	// Represents getEntityNode method
	// @param {Array<Entity>} arr - arr method param.
	// @returns {Array<TreeNode>} - getEntityNode method returns Array<TreeNode>.
    */
    getEntityNode(arr: Array<Entity>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((entity: Entity): void => {
            const innerNode: TreeNode = {
                data: entity
            };
            innerNode.data.EntityParentUIdValue = this.getDetailsForUId(this.entityParents, entity.EntityParentUId, 'EntityUId', 'Name');
                        
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
        this.searchEntity = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addEntity(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageEntity'],
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
	// @param {TreeNode<Entity>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<Entity>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.EntityUId, this.manageEntity.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToEntity method
	// @param {string} action - action method param.
	// @param {Entity} nodeData - nodeData method param.
	// @returns {void} - navigateToEntity method returns void.
    */
    navigateToEntity(nodeData: Entity, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageEntity'],
                {
                    queryParams: {
                        action,
                        entityUId: nodeData.EntityUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageEntity'],
            {
                queryParams: {
                    action: 'Edit',
                    entityUId: this.selectedDetailNode.EntityUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} entityUId - entityUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(entityUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.EntityUId === entityUId))[0].data;
        }
    }
    /**
	// Represents deleteConfirmation method
	// @param {TemplateRef<string>} content - content method param.
	// @param {Entity} rowData - rowData method param.
	// @returns void - deleteConfirmation method returns void.
    */
    deleteConfirmation(content: TemplateRef<string>, rowData: Entity): void {
        if (rowData) {
            const entityToDelete: TreeNode = this.manageEntity.data.filter((x: TreeNode): boolean => x.data.EntityUId === rowData.EntityUId)[0];
            if (entityToDelete && entityToDelete.data) {
                 this.entityToDelete = entityToDelete.data;
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
    deleteEntity(): void {
        const serviceParameterIndexValue: number = this.entities.findIndex((x: Entity): boolean => x.EntityUId === this.entityToDelete.EntityUId);
        if (this.entities && this.entities.length > 0) {
            this.entities[serviceParameterIndexValue].ItemState = ItemState.Deleted;
            this.entities[serviceParameterIndexValue].RowStatusUId = RowStatuses.Inactive;            
        }               
        this.entityService.mergeEntities([this.entities[serviceParameterIndexValue]]).subscribe((): void => {
            let deleteSuccessfulMsg: string = '';
            this.translateService.get('DeleteSuccessMessage').subscribe((data: string): string => deleteSuccessfulMsg = data);
            CoreNotificationService.showSuccess(deleteSuccessfulMsg);
            this.getEntities();
        }, (error: HttpErrorResponse): void => {            
            CoreNotificationService.handleError(error);            
        });
        this.modalService.dismissAll();  
    }
}
