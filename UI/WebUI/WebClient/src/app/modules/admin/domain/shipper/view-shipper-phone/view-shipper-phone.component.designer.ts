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
import { ShipperPhone } from '../../../../../shared/models/domain/shipper/shipper-phone.model';
import { ShipperPhoneService } from '../../../../../core/services/domain/shipper/shipper-phone.service';
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CountryService } from '../../../../../core/services/domain/master/country.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { Shipper } from '../../../../../shared/models/domain/shipper/shipper.model';
import { ShipperService } from '../../../../../core/services/domain/shipper/shipper.service';
import { NgClass } from '@angular/common';

// Define the ViewShipperPhoneComponent
@Component({
    // Selector for the component
    selector: 'app-view-shipper-phone',
    // Template URL for the component
    templateUrl: './view-shipper-phone.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewShipperPhoneComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewShipperPhonesTable', { static: false }) viewShipperPhonesTable!: TreeTable;  
    manageShipperPhone: CustomTreeTableModel<TreeNode<ShipperPhone>> = new CustomTreeTableModel<TreeNode<ShipperPhone>>({
        data: new Array<TreeNode<ShipperPhone>>()
    });
    selectedDetailNode: ShipperPhone = {} as ShipperPhone;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    countries!: Array<Country>;
    doubleClicked!: boolean;
    manageShipperPhoneData!: Array<ShipperPhone>;
    phoneTypes!: Array<MasterListItem>;
    screenGroup!: string;
    searchShipperPhone!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    shipperPhones!: Array<ShipperPhone>;
    shippers!: Array<Shipper>;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly countryService: CountryService,
        readonly masterListItemService: MasterListItemService,
        readonly shipperPhoneService: ShipperPhoneService,
        readonly shipperService: ShipperService,
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
                 { field: 'ShipperUIdValue', header: 'Shipper', class:'text-left' },          
                 { field: 'PhoneTypeUIdValue', header: 'PhoneType', class:'text-left' },          
                 { field: 'CountryUIdValue', header: 'Country', class:'text-left' },          
                 { field: 'PhoneNumber', header: 'PhoneNumber', class: 'text-left' },
                 { field: 'IsPreferred', header: 'IsPreferred', fieldType: 'boolean', class: 'text-center' },           
        ];
        this.sortCols = [
                 { field: 'ShipperUIdValue', type: 'string' },
                 { field: 'PhoneTypeUIdValue', type: 'string' },
                 { field: 'CountryUIdValue', type: 'string' },
                 { field: 'PhoneNumber', type: 'string' },
                 { field: 'IsPreferred', type: 'string' },
        ];
        this.getMasterData();
        this.getShipperPhones();
      
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
       this.shipperService.getShippers().subscribe((shippers: Array<Shipper>): void => {
           this.shippers = shippers;
           this.refreshShipperPhone();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('PhoneType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.phoneTypes = masterListItems;
           this.refreshShipperPhone();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshShipperPhone method
    // Method to refresh grid data.
	// @returns {void} - refreshShipperPhone method returns void.
    */
    refreshShipperPhone(): void {
        if (this.shipperPhones && this.shipperPhones.length > 0) {
            this.buildShipperPhoneTree(this.shipperPhones);
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

    getShipperPhones(): void {
       this.shipperPhoneService.getShipperPhones()
            .subscribe((shipperPhones: Array<ShipperPhone>): void => {
                if (shipperPhones && shipperPhones.length) {
                    this.shipperPhones = shipperPhones;
                    this.buildShipperPhoneTree(shipperPhones);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildShipperPhoneTree method
	// @param {Array<ShipperPhone>} activeShipperPhones - activeShipperPhones method param.
	// @returns {void} - buildShipperPhoneTree method returns void.
    */
    buildShipperPhoneTree(activeShipperPhones: Array<ShipperPhone>): void {
        const shipperPhones: Array<TreeNode> = this.getShipperPhoneNode(activeShipperPhones);
        this.manageShipperPhone.data = shipperPhones;
        this.manageShipperPhone.data = [...this.customSortMultipleColumns(this.manageShipperPhone.data, this.sortCols)];
    }

    /**
	// Represents getShipperPhoneNode method
	// @param {Array<ShipperPhone>} arr - arr method param.
	// @returns {Array<TreeNode>} - getShipperPhoneNode method returns Array<TreeNode>.
    */
    getShipperPhoneNode(arr: Array<ShipperPhone>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((shipperPhone: ShipperPhone): void => {
            const innerNode: TreeNode = {
                data: shipperPhone
            };
            innerNode.data.CountryUIdValue = this.getDetailsForUId(this.countries, shipperPhone.CountryUId, 'CountryUId', 'Name');
                        
            innerNode.data.PhoneTypeUIdValue = this.getDetailsForUId(this.phoneTypes, shipperPhone.PhoneTypeUId, 'MasterListItemUId', 'Name');
                        
            innerNode.data.ShipperUIdValue = this.getDetailsForUId(this.shippers, shipperPhone.ShipperUId, 'ShipperUId', 'Name');
                        
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
        this.searchShipperPhone = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addShipperPhone(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipperPhone'],
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
	// @param {TreeNode<ShipperPhone>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<ShipperPhone>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ShipperPhoneUId, this.manageShipperPhone.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToShipperPhone method
	// @param {ShipperPhone} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToShipperPhone method returns void.
    */
    navigateToShipperPhone(nodeData: ShipperPhone, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipperPhone'],
                {
                    queryParams: {
                        action,
                        shipperPhoneUId: nodeData.ShipperPhoneUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipperPhone'],
            {
                queryParams: {
                    action: 'Edit',
                    shipperPhoneUId: this.selectedDetailNode.ShipperPhoneUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} shipperPhoneUId - shipperPhoneUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(shipperPhoneUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ShipperPhoneUId === shipperPhoneUId))[0].data;
        }
    }
}
