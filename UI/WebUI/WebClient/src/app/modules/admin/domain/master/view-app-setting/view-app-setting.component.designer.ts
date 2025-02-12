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
import { AppSetting } from '../../../../../shared/models/domain/master/app-setting.model';
import { AppSettingService } from '../../../../../core/services/domain/master/app-setting.service';
import { NgClass } from '@angular/common';

// Define the ViewAppSettingComponent
@Component({
    // Selector for the component
    selector: 'app-view-app-setting',
    // Template URL for the component
    templateUrl: './view-app-setting.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewAppSettingComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewAppSettingsTable', { static: false }) viewAppSettingsTable!: TreeTable;  
    manageAppSetting: CustomTreeTableModel<TreeNode<AppSetting>> = new CustomTreeTableModel<TreeNode<AppSetting>>({
        data: new Array<TreeNode<AppSetting>>()
    });
    selectedDetailNode: AppSetting = {} as AppSetting;
    sortCols!: Array<CustomSortModel>;
    appSettings!: Array<AppSetting>;
    appSettingToDelete!: AppSetting;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageAppSettingData!: Array<AppSetting>;
    screenGroup!: string;
    searchAppSetting!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly appSettingService: AppSettingService,
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
                 { field: 'ComponentName', header: 'ComponentName', class: 'text-left' },
                 { field: 'KeyName', header: 'KeyName', class: 'text-left' },
                 { field: 'Value', header: 'Value', class: 'text-left' },
                 { field: 'Description', header: 'Description', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'ComponentName', type: 'string' },
                 { field: 'KeyName', type: 'string' },
                 { field: 'Value', type: 'string' },
                 { field: 'Description', type: 'string' },
        ];
        this.getMasterData();
        this.getAppSettings();
      
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
	// Represents refreshAppSetting method
    // Method to refresh grid data.
	// @returns {void} - refreshAppSetting method returns void.
    */
    refreshAppSetting(): void {
        if (this.appSettings && this.appSettings.length > 0) {
            this.buildAppSettingTree(this.appSettings);
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

    getAppSettings(): void {
       this.appSettingService.getAppSettings()
            .subscribe((appSettings: Array<AppSetting>): void => {
                if (appSettings && appSettings.length) {
                    this.appSettings = appSettings;
                    this.buildAppSettingTree(appSettings);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildAppSettingTree method
	// @param {Array<AppSetting>} activeAppSettings - activeAppSettings method param.
	// @returns {void} - buildAppSettingTree method returns void.
    */
    buildAppSettingTree(activeAppSettings: Array<AppSetting>): void {
        const appSettings: Array<TreeNode> = this.getAppSettingNode(activeAppSettings);
        this.manageAppSetting.data = appSettings;
        this.manageAppSetting.data = [...this.customSortMultipleColumns(this.manageAppSetting.data, this.sortCols)];
    }

    /**
	// Represents getAppSettingNode method
	// @param {Array<AppSetting>} arr - arr method param.
	// @returns {Array<TreeNode>} - getAppSettingNode method returns Array<TreeNode>.
    */
    getAppSettingNode(arr: Array<AppSetting>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((appSetting: AppSetting): void => {
            const innerNode: TreeNode = {
                data: appSetting
            };
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
        this.searchAppSetting = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addAppSetting(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageAppSetting'],
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
	// @param {TreeNode<AppSetting>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<AppSetting>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.AppSettingUId, this.manageAppSetting.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToAppSetting method
	// @param {AppSetting} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToAppSetting method returns void.
    */
    navigateToAppSetting(nodeData: AppSetting, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageAppSetting'],
                {
                    queryParams: {
                        action,
                        appSettingUId: nodeData.AppSettingUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageAppSetting'],
            {
                queryParams: {
                    action: 'Edit',
                    appSettingUId: this.selectedDetailNode.AppSettingUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} appSettingUId - appSettingUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(appSettingUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.AppSettingUId === appSettingUId))[0].data;
        }
    }
    /**
	// Represents deleteConfirmation method
	// @param {TemplateRef<string>} content - content method param.
	// @param {AppSetting} rowData - rowData method param.
	// @returns void - deleteConfirmation method returns void.
    */
    deleteConfirmation(content: TemplateRef<string>, rowData: AppSetting): void {
        if (rowData) {
            const appSettingToDelete: TreeNode = this.manageAppSetting.data.filter((x: TreeNode): boolean => x.data.AppSettingUId === rowData.AppSettingUId)[0];
            if (appSettingToDelete && appSettingToDelete.data) {
                 this.appSettingToDelete = appSettingToDelete.data;
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
    deleteAppSetting(): void {
        const serviceParameterIndexValue: number = this.appSettings.findIndex((x: AppSetting): boolean => x.AppSettingUId === this.appSettingToDelete.AppSettingUId);
        if (this.appSettings && this.appSettings.length > 0) {
            this.appSettings[serviceParameterIndexValue].ItemState = ItemState.Deleted;
            this.appSettings[serviceParameterIndexValue].RowStatusUId = RowStatuses.Inactive;            
        }               
        this.appSettingService.mergeAppSettings([this.appSettings[serviceParameterIndexValue]]).subscribe((): void => {
            let deleteSuccessfulMsg: string = '';
            this.translateService.get('DeleteSuccessMessage').subscribe((data: string): string => deleteSuccessfulMsg = data);
            CoreNotificationService.showSuccess(deleteSuccessfulMsg);
            this.getAppSettings();
        }, (error: HttpErrorResponse): void => {            
            CoreNotificationService.handleError(error);            
        });
        this.modalService.dismissAll();  
    }
}
