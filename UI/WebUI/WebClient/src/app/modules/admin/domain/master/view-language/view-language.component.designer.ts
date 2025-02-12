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
import { Language } from '../../../../../shared/models/domain/master/language.model';
import { LanguageService } from '../../../../../core/services/domain/master/language.service';
import { NgClass } from '@angular/common';

// Define the ViewLanguageComponent
@Component({
    // Selector for the component
    selector: 'app-view-language',
    // Template URL for the component
    templateUrl: './view-language.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewLanguageComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly languageService = inject(LanguageService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly modalService = inject(NgbModal);
    private readonly translateService = inject(TranslateService);

    readonly viewLanguagesTable = viewChild.required<TreeTable>('this.ViewLanguagesTable');  
    manageLanguage: CustomTreeTableModel<TreeNode<Language>> = new CustomTreeTableModel<TreeNode<Language>>({
        data: new Array<TreeNode<Language>>()
    });
    selectedDetailNode: Language = {} as Language;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    languages!: Array<Language>;
    languageToDelete!: Language;
    manageLanguageData!: Array<Language>;
    screenGroup!: string;
    searchLanguage!: string;
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
                 { field: 'LocalizedName', header: 'LocalizedName', class: 'text-left' },
                 { field: 'DisplayName', header: 'DisplayName', class: 'text-left' },
                 { field: 'Culture', header: 'Culture', class: 'text-left' },
                 { field: 'AzureCulture', header: 'AzureCulture', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'LocalizedName', type: 'string' },
                 { field: 'DisplayName', type: 'string' },
                 { field: 'Culture', type: 'string' },
                 { field: 'AzureCulture', type: 'string' },
        ];
        this.getMasterData();
        this.getLanguages();
      
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
	// Represents refreshLanguage method
    // Method to refresh grid data.
	// @returns {void} - refreshLanguage method returns void.
    */
    refreshLanguage(): void {
        if (this.languages && this.languages.length > 0) {
            this.buildLanguageTree(this.languages);
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

    getLanguages(): void {
       this.languageService.getLanguages()
            .subscribe((languages: Array<Language>): void => {
                if (languages && languages.length) {
                    this.languages = languages;
                    this.buildLanguageTree(languages);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildLanguageTree method
	// @param {Array<Language>} activeLanguages - activeLanguages method param.
	// @returns {void} - buildLanguageTree method returns void.
    */
    buildLanguageTree(activeLanguages: Array<Language>): void {
        const languages: Array<TreeNode> = this.getLanguageNode(activeLanguages);
        this.manageLanguage.data = languages;
        this.manageLanguage.data = [...this.customSortMultipleColumns(this.manageLanguage.data, this.sortCols)];
    }

    /**
	// Represents getLanguageNode method
	// @param {Array<Language>} arr - arr method param.
	// @returns {Array<TreeNode>} - getLanguageNode method returns Array<TreeNode>.
    */
    getLanguageNode(arr: Array<Language>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((language: Language): void => {
            const innerNode: TreeNode = {
                data: language
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
        this.searchLanguage = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addLanguage(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageLanguage'],
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
	// @param {TreeNode<Language>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<Language>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.LanguageUId, this.manageLanguage.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToLanguage method
	// @param {string} action - action method param.
	// @param {Language} nodeData - nodeData method param.
	// @returns {void} - navigateToLanguage method returns void.
    */
    navigateToLanguage(nodeData: Language, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageLanguage'],
                {
                    queryParams: {
                        action,
                        languageUId: nodeData.LanguageUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageLanguage'],
            {
                queryParams: {
                    action: 'Edit',
                    languageUId: this.selectedDetailNode.LanguageUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} languageUId - languageUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(languageUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.LanguageUId === languageUId))[0].data;
        }
    }
    /**
	// Represents deleteConfirmation method
	// @param {TemplateRef<string>} content - content method param.
	// @param {Language} rowData - rowData method param.
	// @returns void - deleteConfirmation method returns void.
    */
    deleteConfirmation(content: TemplateRef<string>, rowData: Language): void {
        if (rowData) {
            const languageToDelete: TreeNode = this.manageLanguage.data.filter((x: TreeNode): boolean => x.data.LanguageUId === rowData.LanguageUId)[0];
            if (languageToDelete && languageToDelete.data) {
                 this.languageToDelete = languageToDelete.data;
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
    deleteLanguage(): void {
        const serviceParameterIndexValue: number = this.languages.findIndex((x: Language): boolean => x.LanguageUId === this.languageToDelete.LanguageUId);
        if (this.languages && this.languages.length > 0) {
            this.languages[serviceParameterIndexValue].ItemState = ItemState.Deleted;
            this.languages[serviceParameterIndexValue].RowStatusUId = RowStatuses.Inactive;            
        }               
        this.languageService.mergeLanguages([this.languages[serviceParameterIndexValue]]).subscribe((): void => {
            let deleteSuccessfulMsg: string = '';
            this.translateService.get('DeleteSuccessMessage').subscribe((data: string): string => deleteSuccessfulMsg = data);
            CoreNotificationService.showSuccess(deleteSuccessfulMsg);
            this.getLanguages();
        }, (error: HttpErrorResponse): void => {            
            CoreNotificationService.handleError(error);            
        });
        this.modalService.dismissAll();  
    }
}
