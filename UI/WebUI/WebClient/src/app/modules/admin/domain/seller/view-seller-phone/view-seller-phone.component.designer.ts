/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, inject, viewChild } from '@angular/core';
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
import { SellerPhone } from '../../../../../shared/models/domain/seller/seller-phone.model';
import { SellerPhoneService } from '../../../../../core/services/domain/seller/seller-phone.service';
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CountryService } from '../../../../../core/services/domain/master/country.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { Seller } from '../../../../../shared/models/domain/seller/seller.model';
import { SellerService } from '../../../../../core/services/domain/seller/seller.service';
import { NgClass } from '@angular/common';

// Define the ViewSellerPhoneComponent
@Component({
    // Selector for the component
    selector: 'app-view-seller-phone',
    // Template URL for the component
    templateUrl: './view-seller-phone.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewSellerPhoneComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly countryService = inject(CountryService);
    readonly masterListItemService = inject(MasterListItemService);
    readonly sellerPhoneService = inject(SellerPhoneService);
    readonly sellerService = inject(SellerService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewSellerPhonesTable = viewChild.required<TreeTable>('this.ViewSellerPhonesTable');  
    manageSellerPhone: CustomTreeTableModel<TreeNode<SellerPhone>> = new CustomTreeTableModel<TreeNode<SellerPhone>>({
        data: new Array<TreeNode<SellerPhone>>()
    });
    selectedDetailNode: SellerPhone = {} as SellerPhone;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    countries!: Array<Country>;
    doubleClicked!: boolean;
    manageSellerPhoneData!: Array<SellerPhone>;
    phoneTypes!: Array<MasterListItem>;
    screenGroup!: string;
    searchSellerPhone!: string;
    selectedNode!: TreeTableNode | null;
    sellerPhones!: Array<SellerPhone>;
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
                 { field: 'PhoneTypeUIdValue', header: 'PhoneType', class:'text-left' },          
                 { field: 'CountryUIdValue', header: 'Country', class:'text-left' },          
                 { field: 'PhoneNumber', header: 'PhoneNumber', class: 'text-left' },
                 { field: 'IsPreferred', header: 'IsPreferred', fieldType: 'boolean', class: 'text-center' },           
        ];
        this.sortCols = [
                 { field: 'SellerUIdValue', type: 'string' },
                 { field: 'PhoneTypeUIdValue', type: 'string' },
                 { field: 'CountryUIdValue', type: 'string' },
                 { field: 'PhoneNumber', type: 'string' },
                 { field: 'IsPreferred', type: 'string' },
        ];
        this.getMasterData();
        this.getSellerPhones();
      
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
        this.countryService.getCountries().subscribe((countries: Array<Country>): void => {
            this.countries = countries;
        // On error, handle the error
        }, (error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
    }
 
    /**
	// Represents getMasterData method
    // Method to retrieve master data
	// @returns {void} - getMasterData method returns void.
    */
    getMasterData(): void {
           this.onLanguageChanged();
       this.sellerService.getSellers().subscribe((sellers: Array<Seller>): void => {
           this.sellers = sellers;
           this.refreshSellerPhone();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('PhoneType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.phoneTypes = masterListItems;
           this.refreshSellerPhone();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshSellerPhone method
    // Method to refresh grid data.
	// @returns {void} - refreshSellerPhone method returns void.
    */
    refreshSellerPhone(): void {
        if (this.sellerPhones && this.sellerPhones.length > 0) {
            this.buildSellerPhoneTree(this.sellerPhones);
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

    getSellerPhones(): void {
       this.sellerPhoneService.getSellerPhones()
            .subscribe((sellerPhones: Array<SellerPhone>): void => {
                if (sellerPhones && sellerPhones.length) {
                    this.sellerPhones = sellerPhones;
                    this.buildSellerPhoneTree(sellerPhones);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildSellerPhoneTree method
	// @param {Array<SellerPhone>} activeSellerPhones - activeSellerPhones method param.
	// @returns {void} - buildSellerPhoneTree method returns void.
    */
    buildSellerPhoneTree(activeSellerPhones: Array<SellerPhone>): void {
        const sellerPhones: Array<TreeNode> = this.getSellerPhoneNode(activeSellerPhones);
        this.manageSellerPhone.data = sellerPhones;
        this.manageSellerPhone.data = [...this.customSortMultipleColumns(this.manageSellerPhone.data, this.sortCols)];
    }

    /**
	// Represents getSellerPhoneNode method
	// @param {Array<SellerPhone>} arr - arr method param.
	// @returns {Array<TreeNode>} - getSellerPhoneNode method returns Array<TreeNode>.
    */
    getSellerPhoneNode(arr: Array<SellerPhone>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((sellerPhone: SellerPhone): void => {
            const innerNode: TreeNode = {
                data: sellerPhone
            };
            innerNode.data.CountryUIdValue = this.getDetailsForUId(this.countries, sellerPhone.CountryUId, 'CountryUId', 'Name');
                        
            innerNode.data.PhoneTypeUIdValue = this.getDetailsForUId(this.phoneTypes, sellerPhone.PhoneTypeUId, 'MasterListItemUId', 'Name');
                        
            innerNode.data.SellerUIdValue = this.getDetailsForUId(this.sellers, sellerPhone.SellerUId, 'SellerUId', 'Name');
                        
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
        this.searchSellerPhone = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addSellerPhone(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSellerPhone'],
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
	// @param {TreeNode<SellerPhone>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<SellerPhone>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.SellerPhoneUId, this.manageSellerPhone.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToSellerPhone method
	// @param {string} action - action method param.
	// @param {SellerPhone} nodeData - nodeData method param.
	// @returns {void} - navigateToSellerPhone method returns void.
    */
    navigateToSellerPhone(nodeData: SellerPhone, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSellerPhone'],
                {
                    queryParams: {
                        action,
                        sellerPhoneUId: nodeData.SellerPhoneUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSellerPhone'],
            {
                queryParams: {
                    action: 'Edit',
                    sellerPhoneUId: this.selectedDetailNode.SellerPhoneUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} sellerPhoneUId - sellerPhoneUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(sellerPhoneUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.SellerPhoneUId === sellerPhoneUId))[0].data;
        }
    }
}
