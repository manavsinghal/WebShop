/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Component, inject } from '@angular/core';
import { TreeNode } from 'primeng/api';
import { TreeTable } from 'primeng/treetable';
import { Guid } from '../core/helpers/guid';
import { CoreSessionService } from '../core/services/core.session.service';
import { CustomSortModel, ErrorResponse } from './models/custom-tree-table.model';
import { ItemState } from './models/item-state.model';
import { ViewModelBase } from './models/view-model-base.model';

@Component({
    selector: 'app-shared',
    template: '<div></div>'
})
export class SharedComponent {
    protected coreSessionService = inject(CoreSessionService);


    onScrollTop(): void {
        const backToTop: HTMLElement | null = document.getElementById('backToTop');
        const goToBottom: HTMLElement | null = document.getElementById('goToBottom');

        if (backToTop) {
            if (document.body.scrollTop > 70 || document.documentElement.scrollTop > 70) {
                backToTop.style.display = 'block';
            } else {
                backToTop.style.display = 'none';
                if (goToBottom) {
                    goToBottom.style.display = 'block';
                }
            }
        }
    }

    onScrollBottom(): void {
        const goToBottom: HTMLElement | null = document.getElementById('goToBottom');

        if (goToBottom) {
            if (window.innerWidth > document.body.clientWidth) {
                goToBottom.style.display = 'block';
            } else {
                goToBottom.style.display = 'none';
            }
        }
    }

    onScrollBottomMove(): void {
        const goToBottom: HTMLElement | null = document.getElementById('goToBottom');

        if (goToBottom) {
            goToBottom.style.display = 'none';
        }

        window.scrollTo(0, document.body.scrollHeight);
    }

    onScrollMove(): void {
        if ((window.innerHeight + document.body.scrollTop) >= document.body.offsetHeight) {
            const goToBottom: HTMLElement | null = document.getElementById('goToBottom');
            if (goToBottom) {
                goToBottom.style.display = 'none';
            }
        }

        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
    }

    customSearchTreeTable = (value: string, content: TreeTable): void => {
        if (value && value.length >= 3) {
            content.filterGlobal(value, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    };

    setAuditFields(obj: ViewModelBase): void {
        if (obj.ItemState === ItemState.Added) {
            obj.CorrelationUId = Guid.newGuid();
            obj.CreatedByUser = this.coreSessionService.getCreatedByUser();
            obj.CreatedByApp = this.coreSessionService.getCreatedByApp();
            obj.CreatedOn = this.getUTCDate();
            obj.ModifiedByUser = this.coreSessionService.getCreatedByUser();
            obj.ModifiedByApp = this.coreSessionService.getCreatedByApp();
            obj.ModifiedOn = this.getUTCDate();
            obj.EffectiveFrom = this.getUTCDate();
            obj.EffectiveTo = this.getUTCDate(new Date(2999, 11, 31, 18, 30, 2, 500));
            obj.CreatedByAccountUId = '99999999-9999-9999-0010-000000000010';
            obj.ModifiedByAccountUId = '99999999-9999-9999-0010-000000000010';
        }
        if (obj.ItemState === ItemState.Modified || obj.ItemState === ItemState.Deleted || obj.ItemState === ItemState.HardDeleted) {
            obj.ModifiedByUser = this.coreSessionService.getCreatedByUser();
            obj.ModifiedByApp = this.coreSessionService.getCreatedByApp();
            obj.ModifiedOn = this.getUTCDate();
            obj.ModifiedByAccountUId = this.coreSessionService.getUserUId();
            obj.ModifiedByAccountUId = '99999999-9999-9999-0010-000000000010';
        }
    }

    getUTCDate(d?: Date): string {
        let date: Date;

        if (d) {
            date = new Date(d);
        } else {
            date = new Date();
        }

        const utcDate: string = date.toISOString();

        return utcDate.substring(0, utcDate.length - 1);
    }

    getDetailsForUId<T>(list: Array<T>, uid: Guid | null, key: string, searchField: string): string {
        let listItem: string = '' as string;

        if (uid) {
            if (list) {
                const selectedListItem: any = list.filter((x: any): boolean => x[key] === uid)[0];
                if (selectedListItem) {
                    listItem = selectedListItem[searchField] as string;
                } else {
                    if (uid === Guid.empty) {
                        listItem = '';
                    }
                }
            }
        }

        return listItem;
    }

    duplicateItemError(errorObj: ErrorResponse): boolean {
        let duplicate: boolean = false as boolean;

        if (errorObj) {
            if (errorObj.error && errorObj.error[0]) {
                const errorString: string | undefined = errorObj.error[0].StackTrace;
                const errorToSearch: string = 'Violation of UNIQUE KEY constraint' as string;
                let errorState: number;
                if (errorString) {
                    errorState = errorString.search(errorToSearch);
                    if (errorState === -1) {
                        duplicate = false;
                    } else {
                        duplicate = true;
                    }
                }
            }
        }

        return duplicate;
    }

    scrollToView(container: string) {
        const e = document.getElementById(container);
        var offset = e ? e.offsetTop - 52 : 0;
        window.scrollTo({ top: offset, behavior: 'smooth' })
    }

    toggleSearchComponent() {
        let e = document.querySelector(".c-mc-filter__search-icon"),
            t = document.querySelector(".c-mc-filter__input-field");
        if (e) {
            let n = e.parentElement;
            if (t && n) {
                if (n.classList.contains("-active")) {
                    n.classList.remove("-active");
                    t.classList.remove("-active");
                } else {
                    n.classList.add("-active");
                    t.classList.add("-active");
                }
            }
        }
    }

    customSortMultipleColumns(allRowData: Array<TreeNode>, sortCols: Array<CustomSortModel>): Array<TreeNode> {
        allRowData.sort((rowItem1, rowItem2): number => {
            for (const column of sortCols) {
                if (rowItem1.data && rowItem1.data[column.field] === undefined && rowItem2.data && rowItem2.data[column.field] === undefined) {
                    return 0;
                } else if (rowItem1.data && rowItem1.data[column.field] === undefined) {
                    return 1;
                } else if (rowItem2.data && rowItem2.data[column.field] === undefined) {
                    return -1;
                }
                if (column.type === 'string') {
                    if (rowItem1.data[column.field] && rowItem2.data[column.field]) {
                        if (rowItem1.data[column.field].toString().toLowerCase() > rowItem2.data[column.field].toString().toLowerCase()) {
                            return 1;
                        }
                        if (rowItem1.data[column.field].toString().toLowerCase() < rowItem2.data[column.field].toString().toLowerCase()) {
                            return -1;
                        }
                    }
                } else {
                    if (rowItem1.data[column.field] > rowItem2.data[column.field]) {
                        return 1;
                    }
                    if (rowItem1.data[column.field] < rowItem2.data[column.field]) {
                        return -1;
                    }
                }
            }

            return 0;
        });

        return allRowData;
    }

    setHomeTab(): void {
        this.coreSessionService.setSelectedTab('Home');
    }
}

